// ChangeLogAddInOptionPanelWidget.cs
//
// Author:
//   Jacob Ilsø Christensen
//
// Copyright (C) 2006  Jacob Ilsø Christensen
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
using MonoDevelop.Core;
using MonoDevelop.Core.Properties;

namespace MonoDevelop.ChangeLogAddIn
{
	public partial class ChangeLogAddInOptionPanelWidget : Gtk.Bin
	{
		public ChangeLogAddInOptionPanelWidget()
		{
			Build ();
		}
		
		public void LoadPanelContents()
		{
			nameEntry.Text = Runtime.Properties.GetProperty ("ChangeLogAddIn.Name", "Full Name");
			emailEntry.Text = Runtime.Properties.GetProperty ("ChangeLogAddIn.Email", "Email Address");
			integrationCheck.Active = Runtime.Properties.GetProperty ("ChangeLogAddIn.VersionControlIntegration", true);
		}
		
		public bool StorePanelContents()
		{
			Runtime.Properties.SetProperty("ChangeLogAddIn.Name", nameEntry.Text);
			Runtime.Properties.SetProperty("ChangeLogAddIn.Email", emailEntry.Text);
			Runtime.Properties.SetProperty("ChangeLogAddIn.VersionControlIntegration", integrationCheck.Active);
			Runtime.Properties.SaveProperties ();
			return true;
		}
	}
}
