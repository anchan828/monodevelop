// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace MonoDevelop.Projects.Gui.Dialogs.OptionPanels {
    
    
    internal partial class CombineInformationWidget {
        
        private Gtk.VBox vbox86;
        
        private Gtk.Table table14;
        
        private Gtk.Label descLabel;
        
        private Gtk.ScrolledWindow scrolledwindow14;
        
        private Gtk.TextView descView;
        
        private Gtk.Entry versEntry;
        
        private Gtk.Label versLabel;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget MonoDevelop.Projects.Gui.Dialogs.OptionPanels.CombineInformationWidget
            Stetic.BinContainer.Attach(this);
            this.Name = "MonoDevelop.Projects.Gui.Dialogs.OptionPanels.CombineInformationWidget";
            // Container child MonoDevelop.Projects.Gui.Dialogs.OptionPanels.CombineInformationWidget.Gtk.Container+ContainerChild
            this.vbox86 = new Gtk.VBox();
            this.vbox86.Name = "vbox86";
            // Container child vbox86.Gtk.Box+BoxChild
            this.table14 = new Gtk.Table(((uint)(2)), ((uint)(2)), false);
            this.table14.Name = "table14";
            this.table14.RowSpacing = ((uint)(6));
            this.table14.ColumnSpacing = ((uint)(6));
            // Container child table14.Gtk.Table+TableChild
            this.descLabel = new Gtk.Label();
            this.descLabel.Name = "descLabel";
            this.descLabel.Xalign = 0F;
            this.descLabel.Yalign = 0F;
            this.descLabel.LabelProp = MonoDevelop.Core.GettextCatalog.GetString("_Description:");
            this.descLabel.UseUnderline = true;
            this.table14.Add(this.descLabel);
            Gtk.Table.TableChild w1 = ((Gtk.Table.TableChild)(this.table14[this.descLabel]));
            w1.TopAttach = ((uint)(1));
            w1.BottomAttach = ((uint)(2));
            w1.XOptions = ((Gtk.AttachOptions)(0));
            w1.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table14.Gtk.Table+TableChild
            this.scrolledwindow14 = new Gtk.ScrolledWindow();
            this.scrolledwindow14.WidthRequest = 350;
            this.scrolledwindow14.HeightRequest = 100;
            this.scrolledwindow14.Name = "scrolledwindow14";
            this.scrolledwindow14.ShadowType = ((Gtk.ShadowType)(1));
            // Container child scrolledwindow14.Gtk.Container+ContainerChild
            this.descView = new Gtk.TextView();
            this.descView.Name = "descView";
            this.scrolledwindow14.Add(this.descView);
            this.table14.Add(this.scrolledwindow14);
            Gtk.Table.TableChild w3 = ((Gtk.Table.TableChild)(this.table14[this.scrolledwindow14]));
            w3.TopAttach = ((uint)(1));
            w3.BottomAttach = ((uint)(2));
            w3.LeftAttach = ((uint)(1));
            w3.RightAttach = ((uint)(2));
            // Container child table14.Gtk.Table+TableChild
            this.versEntry = new Gtk.Entry();
            this.versEntry.Name = "versEntry";
            this.versEntry.IsEditable = true;
            this.versEntry.InvisibleChar = '●';
            this.table14.Add(this.versEntry);
            Gtk.Table.TableChild w4 = ((Gtk.Table.TableChild)(this.table14[this.versEntry]));
            w4.LeftAttach = ((uint)(1));
            w4.RightAttach = ((uint)(2));
            w4.YOptions = ((Gtk.AttachOptions)(0));
            // Container child table14.Gtk.Table+TableChild
            this.versLabel = new Gtk.Label();
            this.versLabel.Name = "versLabel";
            this.versLabel.Xalign = 0F;
            this.versLabel.LabelProp = MonoDevelop.Core.GettextCatalog.GetString("_Version:");
            this.versLabel.UseUnderline = true;
            this.table14.Add(this.versLabel);
            Gtk.Table.TableChild w5 = ((Gtk.Table.TableChild)(this.table14[this.versLabel]));
            w5.XOptions = ((Gtk.AttachOptions)(4));
            w5.YOptions = ((Gtk.AttachOptions)(0));
            this.vbox86.Add(this.table14);
            Gtk.Box.BoxChild w6 = ((Gtk.Box.BoxChild)(this.vbox86[this.table14]));
            w6.Position = 0;
            w6.Expand = false;
            this.Add(this.vbox86);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.descLabel.MnemonicWidget = this.scrolledwindow14;
            this.versLabel.MnemonicWidget = this.versEntry;
            this.Show();
        }
    }
}
