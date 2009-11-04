// 
// IPhoneDebuggerSession.cs
//  
// Author:
//       Michael Hutchinson <mhutchinson@novell.com>
// 
// Copyright (c) 2009 Novell, Inc. (http://www.novell.com)
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
using Mono.Debugger;
using Mono.Debugging;
using Mono.Debugging.Client;
using System.Threading;
using System.Diagnostics;
using MonoDevelop.IPhone;
using System.IO;
using MonoDevelop.Core;
using System.Net.Sockets;
using System.Net;

namespace MonoDevelop.Debugger.Soft.IPhone
{


	public class IPhoneDebuggerSession : SoftDebuggerSession
	{
		string appName;
		ProcessInfo[] procs;
		Process simProcess;
		Gtk.Dialog dialog;
		
		protected override void OnRun (DebuggerStartInfo startInfo)
		{
			var dsi = (IPhoneDebuggerStartInfo) startInfo;
			appName = dsi.ExecutionCommand.AppPath.FileNameWithoutExtension;
			
			var debugSock = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			var outputSock = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			debugSock.Bind (new IPEndPoint (dsi.Address, dsi.DebugPort));
			outputSock.Bind (new IPEndPoint (dsi.Address, dsi.OutputPort));
			outputSock.Listen (1000);
			debugSock.Listen (1000);
			
			VirtualMachine vm = null;
			var listenThread = new Thread (delegate () {
				try {
					vm = VirtualMachineManager.Listen (outputSock, debugSock);
					OnConnected (vm, false);

					Gtk.Application.Invoke (delegate {
						if (dialog != null)
							dialog.Respond (Gtk.ResponseType.Ok);
					});
				} catch (Exception ex) {
					if (!(ex is SocketException && ((SocketException)ex).ErrorCode != (int)SocketError.Shutdown))
						LoggingService.LogError ("Unexpected error in iphone soft debugger listening thread", ex);
					EndSession ();
				}
			});
			listenThread.Start ();
			
			if (dsi.ExecutionCommand.Simulator) {
				StartSimulatorProcess (dsi.ExecutionCommand);
			} else {
				//FIXME: upload the app
			}
			
			Gtk.Application.Invoke (delegate {
				if (vm != null || Exited)
					return;
				
				dialog = new Gtk.Dialog () {
					Title = "Waiting for debugger"
				};
				
				string message = GettextCatalog.GetString ("Waiting for debugger to connect on {0}:{1}...", dsi.Address, dsi.DebugPort);
				if (!dsi.ExecutionCommand.Simulator)
					message += "\n" + GettextCatalog.GetString ("Please start the application on the device.");
				
				var label = new Gtk.Alignment (0.5f, 0.5f, 1f, 1f) {
					Child = new Gtk.Label (message),
					BorderWidth = 12
				};
				dialog.VBox.PackStart (label);
				label.ShowAll ();	
				
				dialog.AddButton ("Cancel", Gtk.ResponseType.Cancel);
				
				int response = MonoDevelop.Core.Gui.MessageService.ShowCustomDialog (dialog);
				dialog.Destroy ();
				
				if (response != (int) Gtk.ResponseType.Ok) {
					EndSession ();
					if (listenThread != null && listenThread.IsAlive) {
						listenThread = null;
						debugSock.Close (200);
						outputSock.Close (200);
						debugSock = outputSock = null;
					}
				}
				dialog = null;
			});
		}
		
		protected override void EndSession ()
		{
			if (dialog != null) {
				Gtk.Application.Invoke (delegate {
					if (dialog != null)
						dialog.Respond (Gtk.ResponseType.Cancel);
				});
			}
			EndSimProcess ();
			base.EndSession ();
		}
		
		protected override ProcessInfo[] OnGetProcesses ()
		{
			if (procs == null)
				procs = new ProcessInfo[] { new ProcessInfo (0, appName) };
			return procs;
		}

		//FIXME: hook up the app's stdin and stdout
		void StartSimulatorProcess (IPhoneExecutionCommand cmd)
		{
			string mtouchPath = cmd.Runtime.GetToolPath (cmd.Framework, "mtouch");
			if (string.IsNullOrEmpty (mtouchPath))
				throw new InvalidOperationException ("Cannot execute iPhone application. mtouch tool is missing.");
			
			var psi = new ProcessStartInfo () {
				FileName = mtouchPath,
				UseShellExecute = false,
				Arguments = string.Format ("-launchsim='{0}'", cmd.AppPath),
				RedirectStandardInput = true,
			};
			simProcess = Process.Start (psi);
			
			simProcess.Exited += delegate {
				EndSession ();
				simProcess = null;
			};
			
			TargetExited += delegate {
				EndSimProcess ();
			};
		}
		
		void EndSimProcess ()
		{
			if (simProcess == null)
				return;
			if (!simProcess.HasExited) {
				try {
					simProcess.StandardInput.WriteLine ();
				} catch {}
			}
			GLib.Timeout.Add (10000, delegate {
				if (!simProcess.HasExited)
					simProcess.Kill ();
				return false;
			});
		}
		
		
		protected override void OnExit ()
		{
			base.OnExit ();
			EndSimProcess ();
		}
	}
}
