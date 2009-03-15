// 
// CSharpFormatter.cs
//  
// Author:
//       Mike Krüger <mkrueger@novell.com>
// 
// Copyright (c) 2009 Mike Krüger
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

using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using MonoDevelop.Ide.Gui.Content;
using MonoDevelop.Projects;
using MonoDevelop.Projects.Text;

using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory.Parser;
using ICSharpCode.NRefactory.Parser.CSharp;
using ICSharpCode.NRefactory.Visitors;
using ICSharpCode.NRefactory.PrettyPrinter;

namespace CSharpBinding.Parser
{
	public class CSharpFormatter : AbstractPrettyPrinter
	{
		const string MimeType = "text/x-csharp";
		public override bool CanFormat (string mimeType)
		{
			return mimeType == MimeType;
		}
		
		static int GetNextTabstop (int currentColumn, int tabSize)
		{
			int result = currentColumn + tabSize;
			return (result / tabSize) * tabSize;
		}

		protected override string InternalFormat (SolutionItem policyParent, string input, int startOffset, int endOffset)
		{
			if (string.IsNullOrEmpty (input))
				return input;
			TextStylePolicy currentPolicy = policyParent != null
					? policyParent.Policies.Get<TextStylePolicy> ()
					: MonoDevelop.Projects.Policies.PolicyService.GetDefaultPolicy<TextStylePolicy> ();
			CodeFormattingPolicy codePolicy = policyParent != null
					? policyParent.Policies.Get<CodeFormattingPolicy> ()
					: MonoDevelop.Projects.Policies.PolicyService.GetDefaultPolicy<CodeFormattingPolicy> ();
			
			CSharpOutputVisitor outputVisitor = new CSharpOutputVisitor ();
			outputVisitor.Options.IndentationChar = currentPolicy.TabsToSpaces ? ' ' : '\t';
			outputVisitor.Options.TabSize         = currentPolicy.TabWidth;
			outputVisitor.Options.IndentSize      = currentPolicy.TabWidth;
			CodeFormatSettings settings = codePolicy.GetSettings ();
			CodeFormatDescription descr = TextFileService.GetFormatDescription (MimeType);
			Type optionType = outputVisitor.Options.GetType ();
			
			foreach (CodeFormatOption option in descr.AllOptions) {
				KeyValuePair<string, string> val = settings.GetValue (descr, option);
				PropertyInfo info = optionType.GetProperty (option.Name);
				if (info == null) {
					System.Console.WriteLine("option : " + option.Name + " not found.");
					continue;
				}
				object cval = null;
				if (info.PropertyType.IsEnum) {
					cval = Enum.Parse (info.PropertyType, val.Key);
				} else if (info.PropertyType == typeof (bool)) {
					cval = Convert.ToBoolean (val.Key);
				} else {
					cval = Convert.ChangeType (val.Key, info.PropertyType);
				}
				//System.Console.WriteLine("set " + option.Name + " to " + cval);
				info.SetValue (outputVisitor.Options, cval, null);
			}
			
			using (IParser parser = ParserFactory.CreateParser (SupportedLanguage.CSharp, new StringReader (input))) {
				parser.Parse ();
				IList<ISpecial> specials = parser.Lexer.SpecialTracker.RetrieveSpecials ();
				if (parser.Errors.Count == 0) {
					using (SpecialNodesInserter.Install (specials, outputVisitor)) {
						parser.CompilationUnit.AcceptVisitor (outputVisitor, null);
					}
					return outputVisitor.Text;
				}
			}
			return input;
			
/*			input = input ?? "";
			int line = 0, col = 0;
			string eolMarker = currentPolicy.GetEolMarker ();
			StringBuilder result = new StringBuilder ();
			
			for (int i = startOffset; i <= endOffset; i++) {
				char ch = input[i];
				switch (ch) {
				case '\t':
					if (currentPolicy.TabsToSpaces) {
						int tabWidth = GetNextTabstop (col, currentPolicy.TabWidth) - col;
						result.Append (new string (' ', tabWidth));
						col += tabWidth;
					} else 
						goto default;
					break;
				case '\r':
					if (i + 1 < input.Length && input[i + 1] == '\n')
						i++;
					goto case '\n';
				case '\n':
					result.Append (eolMarker);
					line++;
					col = 0;
					break;
				default:
					result.Append (ch);
					col++;
					break;
				}
			}
			return result.ToString ();*/
		}
	}
}
