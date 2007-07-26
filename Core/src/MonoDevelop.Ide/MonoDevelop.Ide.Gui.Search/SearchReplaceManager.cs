// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version value="$version"/>
// </file>

using System;
using System.Collections;
using System.Diagnostics;

using MonoDevelop.Core.Gui;

using MonoDevelop.Core;
using MonoDevelop.Core.Gui.Dialogs;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Ide.Gui.Content;
using MonoDevelop.Ide.Gui.Dialogs;

namespace MonoDevelop.Ide.Gui.Search
{
	public enum DocumentIteratorType {
		None,
		CurrentDocument,
		AllOpenFiles,
		WholeCombine,
		Directory // only used for search in files
	}
	
	public enum SearchStrategyType {
		None,
		Normal,
		RegEx,
		Wildcard
	}
	
	public class SearchReplaceManager
	{
		internal static ReplaceDialog ReplaceDialog = null;

		static IFind find = new DefaultFind();
		static SearchOptions searchOptions = new SearchOptions("SharpDevelop.SearchAndReplace.SearchAndReplaceProperties");
		static ISearchResult lastResult = null;

		public static SearchOptions SearchOptions {
			get {
				return searchOptions;
			}
		}
		
		static SearchReplaceManager()
		{
			searchOptions.SearchStrategyTypeChanged   += new EventHandler(InitializeSearchStrategy);
			searchOptions.DocumentIteratorTypeChanged += new EventHandler(InitializeDocumentIterator);
			InitializeDocumentIterator(null, null);
			InitializeSearchStrategy(null, null);
		}	
		
		static void InitializeSearchStrategy(object sender, EventArgs e)
		{
			find.SearchStrategy = SearchReplaceUtilities.CreateSearchStrategy(SearchOptions.SearchStrategyType);
		}
		
		static void InitializeDocumentIterator(object sender, EventArgs e)
		{
			find.DocumentIterator = SearchReplaceUtilities.CreateDocumentIterator(SearchOptions.DocumentIteratorType);
		}
		
		public static void ShowFindReplaceWindow ()
		{
			ShowWindow (true);
		}
		
		public static void ShowFindWindow ()
		{
			ShowWindow (false);
		}
		
		static void ShowWindow (bool mode)
		{
			if (ReplaceDialog != null) {
				if (ReplaceDialog.replaceMode == mode) {
					ReplaceDialog.SetSearchPattern (SearchOptions.SearchPattern);
					ReplaceDialog.Present ();
				} else {
					ReplaceDialog.Destroy ();
					ReplaceDialog rd = new ReplaceDialog (mode);
					rd.ShowAll ();
				}
			} else {
				ReplaceDialog rd = new ReplaceDialog (mode);
				rd.ShowAll();
			}
		}
		
		public static void Replace()
		{
			if (IdeApp.Workbench.ActiveDocument != null) {
				IEditableTextBuffer editable = IdeApp.Workbench.ActiveDocument.GetContent<IEditableTextBuffer> ();
				if (editable == null) return;
				
				if (lastResult != null && find.DocumentIterator.CurrentFileName == lastResult.FileName) {
					int s1 = editable.SelectionStartPosition;
					int s2 = editable.SelectionEndPosition;
					if (lastResult.DocumentOffset == s1 && lastResult.DocumentOffset + lastResult.Length == s2)
						editable.SelectedText = SearchOptions.ReplacePattern;
				}
			}
			FindNext();
		}
		
		public static void MarkAll()
		{
			find.Reset();
			try {
				find.SearchStrategy.CompilePattern(searchOptions);
			} catch {
				Services.MessageService.ShowMessage (GettextCatalog.GetString ("Search pattern is invalid"), DialogPointer);
				return;
			}
			while (true) {
				ISearchResult result = SearchReplaceManager.find.FindNext(searchOptions);
				
				if (result == null) {
					Services.MessageService.ShowMessage(GettextCatalog.GetString ("Mark all completed"), DialogPointer ); 
					find.Reset();
					return;
				} else {
					IBookmarkBuffer textArea = OpenView (result.FileName, typeof(IBookmarkBuffer)) as IBookmarkBuffer; 
					if (textArea != null) {
						textArea.SetBookmarked (result.DocumentOffset, true);
					}
				}
			}
		}
		
		public static void ReplaceAll()
		{
			find.Reset();
			try {
				find.SearchStrategy.CompilePattern(searchOptions);
			} catch {
				Services.MessageService.ShowMessage (GettextCatalog.GetString ("Search pattern is invalid"), DialogPointer);
				return;
			}
			
			while (true) {
				ISearchResult result = SearchReplaceManager.find.FindNext(SearchReplaceManager.searchOptions);
				
				if (result == null) {
					string message = string.Format(GettextCatalog.GetPluralString("Replace all finished. {0} match found.", "Replace all finished. {0} matches found.", find.MatchCount), find.MatchCount);
					Services.MessageService.ShowMessage(message, DialogPointer);
					find.Reset();
					return;
				} else {
					string transformedPattern = result.TransformReplacePattern(SearchOptions.ReplacePattern);
					find.Replace (result, transformedPattern);
				}
			}
		}
		
		public static void FindNext ()
		{
			Find (false);
		}
		
		public static void FindPrevious ()
		{
			Find (true);
		}
		
		public static void Find (bool reverse)
		{
			if (find == null || 
			    searchOptions.SearchPattern == null || 
			    searchOptions.SearchPattern.Length == 0) {
				return;
			}
			
			// Restart the search if the file or cursor position has changed
			// since the last FindNext.

			if (lastResult != null) {
				if (find.DocumentIterator.CurrentFileName != lastResult.FileName)
					find.Reset ();
				else {
					ITextBuffer textArea = OpenView (lastResult.FileName, typeof(ITextBuffer)) as ITextBuffer;
					if (textArea == null || (lastResult != null && textArea.CursorPosition != lastResult.DocumentOffset + lastResult.Length))
						find.Reset();
				}
			}
			else
				find.Reset ();
				
			try {
				find.SearchStrategy.CompilePattern(searchOptions);
			} catch {
				Services.MessageService.ShowMessage (GettextCatalog.GetString ("Search pattern is invalid"), DialogPointer);
				return;
			}
			
			FindInternal (reverse);
		}
		
		static void FindInternal (bool reverse)
		{
			try {
				find.SearchStrategy.CompilePattern(searchOptions);
			} catch {
				Services.MessageService.ShowMessage (GettextCatalog.GetString ("Search pattern is invalid"), DialogPointer);
				return;
			}

			ISearchResult result;
			if (!reverse)
				result = find.FindNext (searchOptions);
			else
				result = find.FindPrevious (searchOptions);
			
			lastResult = result;
			
			if (result == null) {
				Services.MessageService.ShowMessage (GettextCatalog.GetString ("Search string not found:") + "\n" + GLib.Markup.EscapeText (SearchOptions.SearchPattern), DialogPointer ); 
				find.Reset();
			} else {
				ITextBuffer textArea = OpenView (result.FileName, typeof(ITextBuffer)) as ITextBuffer;
				if (textArea != null) {
					int startPos = Math.Min (textArea.Text.Length, Math.Max(0, result.DocumentOffset));
					int endPos   = Math.Min (textArea.Text.Length, startPos + result.Length);
					
					if (startPos == textArea.SelectionStartPosition && endPos == textArea.SelectionEndPosition) {
						// If the result is the same of what we have selected, search again.
						FindInternal (reverse);
						return;
					}
					
					textArea.ShowPosition (endPos);
					textArea.Select (endPos, startPos);
				}
			}
		}
		
		static object OpenView (string fileName, Type contentType) 
		{
			Document doc = IdeApp.Workbench.OpenDocument (fileName);
			if (doc != null)
				return doc.GetContent (contentType);
			else
				return null;
		}
		
		internal static Gtk.Dialog DialogPointer 
		{
			get {
				if ( ReplaceDialog != null ) { 
					return ReplaceDialog.DialogPointer;
				}
				return null;
			}
		}
		
	}	
}
