// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.42
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace MonoDevelop.VersionControl.Subversion.Gui {
    
    
    public partial class ClientCertificatePasswordDialog {
        
        private Gtk.VBox vbox2;
        
        private Gtk.Label label1;
        
        private Gtk.Label labelRealm;
        
        private Gtk.Label label2;
        
        private Gtk.HBox hbox1;
        
        private Gtk.Label label3;
        
        private Gtk.Entry entryPwd;
        
        private Gtk.CheckButton checkSave;
        
        private Gtk.Button button23;
        
        private Gtk.Button button28;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize();
            // Widget MonoDevelop.VersionControl.Subversion.Gui.ClientCertificatePasswordDialog
            this.Events = ((Gdk.EventMask)(256));
            this.Name = "MonoDevelop.VersionControl.Subversion.Gui.ClientCertificatePasswordDialog";
            this.Title = Mono.Unix.Catalog.GetString("ClientCertificatePasswordDialog");
            this.WindowPosition = ((Gtk.WindowPosition)(4));
            this.HasSeparator = false;
            // Internal child MonoDevelop.VersionControl.Subversion.Gui.ClientCertificatePasswordDialog.VBox
            Gtk.VBox w1 = this.VBox;
            w1.Events = ((Gdk.EventMask)(256));
            w1.Name = "dialog_VBox";
            w1.BorderWidth = ((uint)(2));
            // Container child dialog_VBox.Gtk.Box+BoxChild
            this.vbox2 = new Gtk.VBox();
            this.vbox2.Name = "vbox2";
            this.vbox2.Spacing = 6;
            this.vbox2.BorderWidth = ((uint)(6));
            // Container child vbox2.Gtk.Box+BoxChild
            this.label1 = new Gtk.Label();
            this.label1.Name = "label1";
            this.label1.Xalign = 0F;
            this.label1.LabelProp = Mono.Unix.Catalog.GetString("<b>A client certificate is needed to connect to the repository</b>");
            this.label1.UseMarkup = true;
            this.vbox2.Add(this.label1);
            Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(this.vbox2[this.label1]));
            w2.Position = 0;
            w2.Expand = false;
            w2.Fill = false;
            // Container child vbox2.Gtk.Box+BoxChild
            this.labelRealm = new Gtk.Label();
            this.labelRealm.Name = "labelRealm";
            this.labelRealm.Xalign = 0F;
            this.labelRealm.LabelProp = "Realm";
            this.vbox2.Add(this.labelRealm);
            Gtk.Box.BoxChild w3 = ((Gtk.Box.BoxChild)(this.vbox2[this.labelRealm]));
            w3.Position = 1;
            w3.Expand = false;
            w3.Fill = false;
            // Container child vbox2.Gtk.Box+BoxChild
            this.label2 = new Gtk.Label();
            this.label2.Name = "label2";
            this.label2.Xalign = 0F;
            this.label2.LabelProp = Mono.Unix.Catalog.GetString("Please provide the passphrase required to access to the certificate:");
            this.vbox2.Add(this.label2);
            Gtk.Box.BoxChild w4 = ((Gtk.Box.BoxChild)(this.vbox2[this.label2]));
            w4.Position = 2;
            w4.Expand = false;
            w4.Fill = false;
            w4.Padding = ((uint)(6));
            // Container child vbox2.Gtk.Box+BoxChild
            this.hbox1 = new Gtk.HBox();
            this.hbox1.Name = "hbox1";
            this.hbox1.Spacing = 6;
            // Container child hbox1.Gtk.Box+BoxChild
            this.label3 = new Gtk.Label();
            this.label3.Name = "label3";
            this.label3.LabelProp = Mono.Unix.Catalog.GetString("Password:");
            this.hbox1.Add(this.label3);
            Gtk.Box.BoxChild w5 = ((Gtk.Box.BoxChild)(this.hbox1[this.label3]));
            w5.Position = 0;
            w5.Expand = false;
            w5.Fill = false;
            // Container child hbox1.Gtk.Box+BoxChild
            this.entryPwd = new Gtk.Entry();
            this.entryPwd.CanFocus = true;
            this.entryPwd.Name = "entryPwd";
            this.entryPwd.IsEditable = true;
            this.entryPwd.Visibility = false;
            this.entryPwd.InvisibleChar = '●';
            this.hbox1.Add(this.entryPwd);
            Gtk.Box.BoxChild w6 = ((Gtk.Box.BoxChild)(this.hbox1[this.entryPwd]));
            w6.Position = 1;
            this.vbox2.Add(this.hbox1);
            Gtk.Box.BoxChild w7 = ((Gtk.Box.BoxChild)(this.vbox2[this.hbox1]));
            w7.Position = 3;
            w7.Expand = false;
            w7.Fill = false;
            // Container child vbox2.Gtk.Box+BoxChild
            this.checkSave = new Gtk.CheckButton();
            this.checkSave.CanFocus = true;
            this.checkSave.Name = "checkSave";
            this.checkSave.Label = Mono.Unix.Catalog.GetString("Remember password");
            this.checkSave.DrawIndicator = true;
            this.checkSave.UseUnderline = true;
            this.vbox2.Add(this.checkSave);
            Gtk.Box.BoxChild w8 = ((Gtk.Box.BoxChild)(this.vbox2[this.checkSave]));
            w8.Position = 4;
            w8.Expand = false;
            w8.Fill = false;
            w1.Add(this.vbox2);
            Gtk.Box.BoxChild w9 = ((Gtk.Box.BoxChild)(w1[this.vbox2]));
            w9.Position = 0;
            w9.Expand = false;
            w9.Fill = false;
            // Internal child MonoDevelop.VersionControl.Subversion.Gui.ClientCertificatePasswordDialog.ActionArea
            Gtk.HButtonBox w10 = this.ActionArea;
            w10.Events = ((Gdk.EventMask)(256));
            w10.Name = "MonoDevelop.VersionControl.Subversion.ClientCertificatePasswordDialog_ActionArea";
            w10.Spacing = 10;
            w10.BorderWidth = ((uint)(5));
            w10.LayoutStyle = ((Gtk.ButtonBoxStyle)(4));
            // Container child MonoDevelop.VersionControl.Subversion.ClientCertificatePasswordDialog_ActionArea.Gtk.ButtonBox+ButtonBoxChild
            this.button23 = new Gtk.Button();
            this.button23.CanDefault = true;
            this.button23.CanFocus = true;
            this.button23.Name = "button23";
            this.button23.UseStock = true;
            this.button23.UseUnderline = true;
            this.button23.Label = "gtk-cancel";
            this.AddActionWidget(this.button23, -6);
            Gtk.ButtonBox.ButtonBoxChild w11 = ((Gtk.ButtonBox.ButtonBoxChild)(w10[this.button23]));
            w11.Expand = false;
            w11.Fill = false;
            // Container child MonoDevelop.VersionControl.Subversion.ClientCertificatePasswordDialog_ActionArea.Gtk.ButtonBox+ButtonBoxChild
            this.button28 = new Gtk.Button();
            this.button28.CanDefault = true;
            this.button28.CanFocus = true;
            this.button28.Name = "button28";
            this.button28.UseStock = true;
            this.button28.UseUnderline = true;
            this.button28.Label = "gtk-ok";
            this.AddActionWidget(this.button28, -5);
            Gtk.ButtonBox.ButtonBoxChild w12 = ((Gtk.ButtonBox.ButtonBoxChild)(w10[this.button28]));
            w12.Position = 1;
            w12.Expand = false;
            w12.Fill = false;
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.DefaultWidth = 414;
            this.DefaultHeight = 217;
            this.Show();
        }
    }
}
