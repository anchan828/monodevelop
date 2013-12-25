// MSBuildFileFormat.cs
//
// Author:
//   Lluis Sanchez Gual <lluis@novell.com>
//
// Copyright (c) 2008 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//

using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MonoDevelop.Core;
using MonoDevelop.Core.Assemblies;
using MonoDevelop.Projects.Extensions;

namespace MonoDevelop.Projects.Formats.MSBuild
{
	public abstract class MSBuildFileFormat: IFileFormat
	{
		readonly SlnFileFormat slnFileFormat = new SlnFileFormat ();
		
		public string Name {
			get { return "MSBuild"; }
		}
		
		public SlnFileFormat SlnFileFormat {
			get { return slnFileFormat; }
		}
		
		public bool SupportsMonikers { get { return SupportedFrameworks == null; } }
		
		public bool SupportsFramework (TargetFramework fx)
		{
			return SupportsMonikers || ((IList<TargetFrameworkMoniker>)SupportedFrameworks).Contains (fx.Id);
		}

		internal virtual bool SupportsSlnVersion (string version)
		{
			return version == SlnVersion;
		}

		internal virtual bool SupportsToolsVersion (string version)
		{
			return version == ToolsVersion;
		}

		public FilePath GetValidFormatName (object obj, FilePath fileName)
		{
			if (slnFileFormat.CanWriteFile (obj, this))
				return slnFileFormat.GetValidFormatName (obj, fileName, this);
			else {
				string ext = MSBuildProjectService.GetExtensionForItem ((SolutionEntityItem)obj);
				if (!string.IsNullOrEmpty (ext))
					return fileName.ChangeExtension ("." + ext);
				else
					return fileName;
			}
		}

		public bool CanReadFile (FilePath file, Type expectedType)
		{
			if (expectedType.IsAssignableFrom (typeof(Solution)) && slnFileFormat.CanReadFile (file, this))
				return true;
			else if (expectedType.IsAssignableFrom (typeof(SolutionEntityItem))) {
				ItemTypeNode node = MSBuildProjectService.FindHandlerForFile (file);
				if (node == null)
					return false;
				//TODO: check ProductVersion first
				return SupportsToolsVersion (ReadToolsVersion (file));
			}
			return false;
		}

		public bool CanWriteFile (object obj)
		{
			if (slnFileFormat.CanWriteFile (obj, this)) {
				Solution sol = (Solution) obj;
				foreach (SolutionEntityItem si in sol.GetAllSolutionItems<SolutionEntityItem> ())
					if (!CanWriteFile (si))
						return false;
				return true;
			}
			else if (obj is SolutionEntityItem) {
				DotNetProject p = obj as DotNetProject;
				// Check the framework only if the project is not loading, since otherwise the
				// project may not yet have the framework info set.
				if (p != null && !p.Loading && !SupportsFramework (p.TargetFramework))
					return false;
				
				// This file format can write all types of projects. If there isn't a handler for a project,
				// it will use a generic handler.
				return true;
			} else
				return false;
		}

		public virtual IEnumerable<string> GetCompatibilityWarnings (object obj)
		{
			if (obj is Solution) {
				List<string> msg = new List<string> ();
				foreach (SolutionEntityItem si in ((Solution)obj).GetAllSolutionItems<SolutionEntityItem> ()) {
					IEnumerable<string> ws = GetCompatibilityWarnings (si);
					if (ws != null)
						msg.AddRange (ws);
				}
				return msg;
			}
			var prj = obj as DotNetProject;
			if (prj != null && !SupportsMonikers && !((IList)SupportedFrameworks).Contains (prj.TargetFramework.Id))
				return new [] { GettextCatalog.GetString (
					"The project '{0}' is being saved using the file format '{1}', but this version of Visual Studio " +
					"does not support the framework that the project is targetting ({2})",
					prj.Name, ProductDescription, prj.TargetFramework.Name)
				};
			return null;
		}

		public void WriteFile (FilePath file, object obj, IProgressMonitor monitor)
		{
			if (slnFileFormat.CanWriteFile (obj, this)) {
				slnFileFormat.WriteFile (file, obj, this, true, monitor);
			} else {
				SolutionEntityItem item = (SolutionEntityItem) obj;
				if (!(item.ItemHandler is MSBuildProjectHandler))
					MSBuildProjectService.InitializeItemHandler (item);
				MSBuildProjectHandler handler = (MSBuildProjectHandler) item.ItemHandler;
				handler.SetTargetFormat (this);
				handler.Save (monitor);
			}
		}

		public object ReadFile (FilePath file, Type expectedType, MonoDevelop.Core.IProgressMonitor monitor)
		{
			if (slnFileFormat.CanReadFile (file, this))
				return slnFileFormat.ReadFile (file, this, monitor);
			else
				return MSBuildProjectService.LoadItem (monitor, file, null, null, null);
		}

		public List<FilePath> GetItemFiles (object obj)
		{
			return new List<FilePath> ();
		}

		public void InitializeSolutionItem (SolutionItem item)
		{
		}

		public void ConvertToFormat (object obj)
		{
			if (obj == null)
				return;
			
			MSBuildHandler handler;
			SolutionItem item = obj as SolutionItem;
			if (item != null) {
				handler = item.GetItemHandler() as MSBuildHandler;
				if (handler != null) {
					handler.SetTargetFormat (this);
					return;
				}
			}
			
			MSBuildProjectService.InitializeItemHandler (item);
			handler = (MSBuildHandler) item.ItemHandler;
			handler.SetTargetFormat (this);
		}
		
		public bool SupportsMixedFormats {
			get { return false; }
		}

		public abstract string ToolsVersion { get; }

		public abstract string SlnVersion { get; }

		public abstract string ProductVersion { get; }

		public abstract string ProductDescription { get; }

		public virtual TargetFrameworkMoniker[] SupportedFrameworks {
			get { return null; }
		}

		static string ReadToolsVersion (FilePath file)
		{
			try {
				using (XmlTextReader tr = new XmlTextReader (new StreamReader (file))) {
					if (tr.MoveToContent () == XmlNodeType.Element) {
						if (tr.LocalName != "Project" || tr.NamespaceURI != "http://schemas.microsoft.com/developer/msbuild/2003")
							return string.Empty;
						string tv = tr.GetAttribute ("ToolsVersion");
						if (string.IsNullOrEmpty (tv))
							return "2.0"; // Some old VS versions don't specify the tools version, so assume 2.0
						else
							return tv;
					}
				}
			} catch {
				// Ignore
			}
			return string.Empty;
		}
		
		public abstract string Id { get; }

		// Used to load projects independently of a solution
		public static MSBuildFileFormat GetFormatForToolsVersion (string toolsVersion)
		{
			switch (toolsVersion) {
			case "2.0":
				return new MSBuildFileFormatVS05 ();
			case "3.5":
				return new MSBuildFileFormatVS08 ();
				// since both VS2010 and 2012 support ToolVersion 4.0, just use the newer format
			case "4.0":
			case "12.0":
				return new MSBuildFileFormatVS12 ();
			}
			throw new Exception ("Unknown ToolsVersion '" + toolsVersion + "'");
		}
	}
	
	class MSBuildFileFormatVS05: MSBuildFileFormat
	{
		static readonly TargetFrameworkMoniker[] supportedFrameworks = {
			TargetFrameworkMoniker.NET_2_0,
		};

		public override string Id {
			get { return "MSBuild05"; }
		}

		public override string ProductVersion {
			get { return "8.0.50727"; }
		}

		public override string ToolsVersion {
			get { return "2.0"; }
		}

		public override string SlnVersion {
			get { return "9.00"; }
		}

		public override string ProductDescription {
			get { return "Visual Studio 2005"; }
		}

		public override TargetFrameworkMoniker[] SupportedFrameworks {
			get { return supportedFrameworks; }
		}
	}
	
	class MSBuildFileFormatVS08: MSBuildFileFormat
	{
		static readonly TargetFrameworkMoniker[] supportedFrameworks = {
			TargetFrameworkMoniker.NET_2_0,
			TargetFrameworkMoniker.NET_3_0,
			TargetFrameworkMoniker.NET_3_5,
			TargetFrameworkMoniker.SL_2_0,
			TargetFrameworkMoniker.SL_3_0,
			TargetFrameworkMoniker.MONOTOUCH_1_0,
		};

		public override string Id {
			get { return "MSBuild08"; }
		}

		public override string ProductVersion {
			get { return "9.0.21022"; }
		}

		public override string ToolsVersion {
			get { return "3.5"; }
		}

		public override string SlnVersion {
			get { return "10.00"; }
		}

		public override string ProductDescription {
			get { return "Visual Studio 2008"; }
		}

		public override TargetFrameworkMoniker[] SupportedFrameworks {
			get { return supportedFrameworks; }
		}
	}
	
	class MSBuildFileFormatVS10: MSBuildFileFormat
	{
		public override string Id {
			get { return "MSBuild10"; }
		}

		public override string ProductVersion {
			get { return "10.0.0"; }
		}

		public override string ToolsVersion {
			get { return "4.0"; }
		}

		public override string SlnVersion {
			get { return "11.00"; }
		}

		public override string ProductDescription {
			get { return "Visual Studio 2010"; }
		}
	}

	// this is actually VS2010 SP1 and later
	class MSBuildFileFormatVS12: MSBuildFileFormat
	{
		public override string Id {
			get { return "MSBuild12"; }
		}

		public override string ProductVersion {
			get { return "12.0.0"; }
		}

		public override string ToolsVersion {
			get { return "4.0"; }
		}

		public override string SlnVersion {
			get { return "12.00"; }
		}

		public override string ProductDescription {
			get { return "Visual Studio 2012"; }
		}

		internal override bool SupportsToolsVersion (string version)
		{
			return version == "4.0" || version == ToolsVersion;
		}
	}
}
