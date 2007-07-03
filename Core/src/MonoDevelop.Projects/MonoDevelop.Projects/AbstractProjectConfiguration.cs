// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version value="$version"/>
// </file>

using System;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;
using System.Xml;

using MonoDevelop.Projects;
using MonoDevelop.Projects.Serialization;

namespace MonoDevelop.Projects
{
	/// <summary>
	/// External language bindings may choose to extend this class.
	/// It makes things a bit easier.
	/// </summary>
	public abstract class AbstractProjectConfiguration : AbstractConfiguration
	{
		[ProjectPathItemProperty ("Output/directory")]
		string directory = "." + Path.DirectorySeparatorChar.ToString();
		
		[ProjectPathItemProperty ("Build/executeBeforeBuild", DefaultValue = "")]
		string executeBeforeBuild = String.Empty;
		
		[ProjectPathItemProperty ("Build/executeAfterBuild", DefaultValue = "")]
		string executeAfterBuild = String.Empty;
		
		[ItemProperty ("Build/debugmode")]
		bool debugmode = true;
		
		[ItemProperty ("Output/signAssembly")]
		bool signAssembly = false;
		
		[ProjectPathItemProperty ("Output/assemblyKeyFile")]
		string assemblyKeyFile = String.Empty;
		
		[ProjectPathItemProperty ("Execution/executeScript", DefaultValue = "")]
		string executeScript = String.Empty;
		
		[ItemProperty ("Execution/runwithwarnings")]
		protected bool runWithWarnings = true;
		
		[ItemProperty ("Execution/commandlineparameters", DefaultValue = "")]
		public string commandLineParameters = String.Empty;
		
		[ItemProperty ("Execution/externalconsole", DefaultValue=false)]
		public bool externalConsole = false;

		[ItemProperty ("Execution/consolepause")]
		public bool pauseconsoleoutput = true;

		public AbstractProjectConfiguration()
		{
		}
		
		public virtual string OutputDirectory {
			get { return directory; }
			set { directory = value; }
		}
		
		public virtual string ExecuteScript {
			get { return executeScript; }
			set { executeScript = value; }
		}
		
		public virtual bool RunWithWarnings {
			get { return runWithWarnings; }
			set { runWithWarnings = value; }
		}
		
		public bool DebugMode {
			get { return debugmode; }
			set { debugmode = value; }
		}
		
		public string CommandLineParameters {
			get { return commandLineParameters; }
			set { commandLineParameters = value; }
		}
		
		public bool ExternalConsole {
			get { return externalConsole; }
			set { externalConsole = value; }
		}
		
		public bool PauseConsoleOutput {
			get { return pauseconsoleoutput; }
			set { pauseconsoleoutput = value; }
		}
		
		public bool SignAssembly {
			get { return signAssembly; }
			set { signAssembly = value; }
		}
		public string AssemblyKeyFile {
			get { return assemblyKeyFile; }
			set { assemblyKeyFile = value; }
		}
		
		
		public override void CopyFrom (IConfiguration configuration)
		{
			base.CopyFrom (configuration);
			AbstractProjectConfiguration conf = (AbstractProjectConfiguration) configuration;
			
			directory = conf.directory;
			executeScript = conf.executeScript;
			executeBeforeBuild = conf.executeBeforeBuild;
			executeAfterBuild = conf.executeAfterBuild;
			runWithWarnings = conf.runWithWarnings;
			debugmode = conf.debugmode;
			commandLineParameters = conf.commandLineParameters;
			externalConsole = conf.externalConsole;
			pauseconsoleoutput = conf.pauseconsoleoutput;
			signAssembly = conf.signAssembly;
			assemblyKeyFile = conf.assemblyKeyFile;
		}
	}
}
