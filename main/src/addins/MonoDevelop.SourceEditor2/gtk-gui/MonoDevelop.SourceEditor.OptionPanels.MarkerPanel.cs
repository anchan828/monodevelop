
// This file has been generated by the GUI designer. Do not modify.
namespace MonoDevelop.SourceEditor.OptionPanels
{
	public partial class MarkerPanel
	{
		private global::Gtk.VBox vbox1;
		private global::Gtk.Label GtkLabel9;
		private global::Gtk.Alignment alignment1;
		private global::Gtk.VBox vbox3;
		private global::Gtk.CheckButton showLineNumbersCheckbutton;
		private global::Gtk.CheckButton underlineErrorsCheckbutton;
		private global::Gtk.CheckButton highlightMatchingBracketCheckbutton;
		private global::Gtk.CheckButton highlightCurrentLineCheckbutton;
		private global::Gtk.CheckButton showRulerCheckbutton;
		private global::Gtk.CheckButton enableAnimationCheckbutton1;
		private global::Gtk.CheckButton enableHighlightUsagesCheckbutton;
		private global::Gtk.CheckButton drawIndentMarkersCheckbutton;
		private global::Gtk.CheckButton enableQuickDiffCheckbutton;
		private global::Gtk.HBox hbox1;
		private global::Gtk.Label label1;
		private global::Gtk.ComboBox showWhitespacesCombobox;
		
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget MonoDevelop.SourceEditor.OptionPanels.MarkerPanel
			global::Stetic.BinContainer.Attach (this);
			this.Name = "MonoDevelop.SourceEditor.OptionPanels.MarkerPanel";
			// Container child MonoDevelop.SourceEditor.OptionPanels.MarkerPanel.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.GtkLabel9 = new global::Gtk.Label ();
			this.GtkLabel9.Name = "GtkLabel9";
			this.GtkLabel9.Xalign = 0F;
			this.GtkLabel9.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>General</b>");
			this.GtkLabel9.UseMarkup = true;
			this.vbox1.Add (this.GtkLabel9);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.GtkLabel9]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.alignment1 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment1.Name = "alignment1";
			this.alignment1.LeftPadding = ((uint)(12));
			// Container child alignment1.Gtk.Container+ContainerChild
			this.vbox3 = new global::Gtk.VBox ();
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			// Container child vbox3.Gtk.Box+BoxChild
			this.showLineNumbersCheckbutton = new global::Gtk.CheckButton ();
			this.showLineNumbersCheckbutton.CanFocus = true;
			this.showLineNumbersCheckbutton.Name = "showLineNumbersCheckbutton";
			this.showLineNumbersCheckbutton.Label = global::Mono.Unix.Catalog.GetString ("_Show line numbers");
			this.showLineNumbersCheckbutton.DrawIndicator = true;
			this.showLineNumbersCheckbutton.UseUnderline = true;
			this.vbox3.Add (this.showLineNumbersCheckbutton);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.showLineNumbersCheckbutton]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.underlineErrorsCheckbutton = new global::Gtk.CheckButton ();
			this.underlineErrorsCheckbutton.CanFocus = true;
			this.underlineErrorsCheckbutton.Name = "underlineErrorsCheckbutton";
			this.underlineErrorsCheckbutton.Label = global::Mono.Unix.Catalog.GetString ("_Underline errors");
			this.underlineErrorsCheckbutton.DrawIndicator = true;
			this.underlineErrorsCheckbutton.UseUnderline = true;
			this.vbox3.Add (this.underlineErrorsCheckbutton);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.underlineErrorsCheckbutton]));
			w3.Position = 1;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.highlightMatchingBracketCheckbutton = new global::Gtk.CheckButton ();
			this.highlightMatchingBracketCheckbutton.CanFocus = true;
			this.highlightMatchingBracketCheckbutton.Name = "highlightMatchingBracketCheckbutton";
			this.highlightMatchingBracketCheckbutton.Label = global::Mono.Unix.Catalog.GetString ("_Highlight matching braces");
			this.highlightMatchingBracketCheckbutton.DrawIndicator = true;
			this.highlightMatchingBracketCheckbutton.UseUnderline = true;
			this.vbox3.Add (this.highlightMatchingBracketCheckbutton);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.highlightMatchingBracketCheckbutton]));
			w4.Position = 2;
			w4.Expand = false;
			w4.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.highlightCurrentLineCheckbutton = new global::Gtk.CheckButton ();
			this.highlightCurrentLineCheckbutton.CanFocus = true;
			this.highlightCurrentLineCheckbutton.Name = "highlightCurrentLineCheckbutton";
			this.highlightCurrentLineCheckbutton.Label = global::Mono.Unix.Catalog.GetString ("Highlight _current line");
			this.highlightCurrentLineCheckbutton.DrawIndicator = true;
			this.highlightCurrentLineCheckbutton.UseUnderline = true;
			this.vbox3.Add (this.highlightCurrentLineCheckbutton);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.highlightCurrentLineCheckbutton]));
			w5.Position = 3;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.showRulerCheckbutton = new global::Gtk.CheckButton ();
			this.showRulerCheckbutton.CanFocus = true;
			this.showRulerCheckbutton.Name = "showRulerCheckbutton";
			this.showRulerCheckbutton.Label = global::Mono.Unix.Catalog.GetString ("Show _column ruler");
			this.showRulerCheckbutton.DrawIndicator = true;
			this.showRulerCheckbutton.UseUnderline = true;
			this.vbox3.Add (this.showRulerCheckbutton);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.showRulerCheckbutton]));
			w6.Position = 4;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.enableAnimationCheckbutton1 = new global::Gtk.CheckButton ();
			this.enableAnimationCheckbutton1.CanFocus = true;
			this.enableAnimationCheckbutton1.Name = "enableAnimationCheckbutton1";
			this.enableAnimationCheckbutton1.Label = global::Mono.Unix.Catalog.GetString ("Enable _animations");
			this.enableAnimationCheckbutton1.DrawIndicator = true;
			this.enableAnimationCheckbutton1.UseUnderline = true;
			this.vbox3.Add (this.enableAnimationCheckbutton1);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.enableAnimationCheckbutton1]));
			w7.Position = 5;
			w7.Expand = false;
			w7.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.enableHighlightUsagesCheckbutton = new global::Gtk.CheckButton ();
			this.enableHighlightUsagesCheckbutton.CanFocus = true;
			this.enableHighlightUsagesCheckbutton.Name = "enableHighlightUsagesCheckbutton";
			this.enableHighlightUsagesCheckbutton.Label = global::Mono.Unix.Catalog.GetString ("Highlight _identifier references");
			this.enableHighlightUsagesCheckbutton.DrawIndicator = true;
			this.enableHighlightUsagesCheckbutton.UseUnderline = true;
			this.vbox3.Add (this.enableHighlightUsagesCheckbutton);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.enableHighlightUsagesCheckbutton]));
			w8.Position = 6;
			w8.Expand = false;
			w8.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.drawIndentMarkersCheckbutton = new global::Gtk.CheckButton ();
			this.drawIndentMarkersCheckbutton.CanFocus = true;
			this.drawIndentMarkersCheckbutton.Name = "drawIndentMarkersCheckbutton";
			this.drawIndentMarkersCheckbutton.Label = global::Mono.Unix.Catalog.GetString ("_Show indentation guides");
			this.drawIndentMarkersCheckbutton.DrawIndicator = true;
			this.drawIndentMarkersCheckbutton.UseUnderline = true;
			this.vbox3.Add (this.drawIndentMarkersCheckbutton);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.drawIndentMarkersCheckbutton]));
			w9.Position = 7;
			w9.Expand = false;
			w9.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.enableQuickDiffCheckbutton = new global::Gtk.CheckButton ();
			this.enableQuickDiffCheckbutton.CanFocus = true;
			this.enableQuickDiffCheckbutton.Name = "enableQuickDiffCheckbutton";
			this.enableQuickDiffCheckbutton.Label = global::Mono.Unix.Catalog.GetString ("_Visualize changed lines");
			this.enableQuickDiffCheckbutton.DrawIndicator = true;
			this.enableQuickDiffCheckbutton.UseUnderline = true;
			this.vbox3.Add (this.enableQuickDiffCheckbutton);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.enableQuickDiffCheckbutton]));
			w10.Position = 8;
			w10.Expand = false;
			w10.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("_Show invisible characters:");
			this.label1.UseUnderline = true;
			this.hbox1.Add (this.label1);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.label1]));
			w11.Position = 0;
			w11.Expand = false;
			w11.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.showWhitespacesCombobox = global::Gtk.ComboBox.NewText ();
			this.showWhitespacesCombobox.Name = "showWhitespacesCombobox";
			this.hbox1.Add (this.showWhitespacesCombobox);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.showWhitespacesCombobox]));
			w12.Position = 1;
			w12.Expand = false;
			w12.Fill = false;
			this.vbox3.Add (this.hbox1);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.hbox1]));
			w13.Position = 9;
			w13.Expand = false;
			w13.Fill = false;
			this.alignment1.Add (this.vbox3);
			this.vbox1.Add (this.alignment1);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.alignment1]));
			w15.Position = 1;
			w15.Expand = false;
			w15.Fill = false;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Show ();
		}
	}
}
