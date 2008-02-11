// FoldMarkerMargin.cs
//
// Author:
//   Mike Krüger <mkrueger@novell.com>
//
// Copyright (c) 2007 Novell, Inc (http://www.novell.com)
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
using System.Collections.Generic;

using Gtk;

namespace Mono.TextEditor
{
	public class FoldMarkerMargin : AbstractMargin
	{
		TextEditor editor;
		LineSegment lineHover;
		
		const int foldSegmentSize = 8;
		
		public override int Width {
			get {
				return 13;
			}
		}
		
		public FoldMarkerMargin (TextEditor editor)
		{
			this.editor = editor;
		}
		
		public override void MousePressed (int button, int x, int y, bool doubleClick)
		{
			if (lineHover == null)
				return;
			foreach (FoldSegment segment in editor.Document.GetStartFoldings (lineHover)) {
				segment.IsFolded = !segment.IsFolded; 
			}
			
			editor.QueueDraw ();
		}
		
		public override void MouseHover (int x, int y, bool buttonPressed)
		{
			int lineNumber = editor.Document.VisualToLogicalLine ((int)(y + editor.TextEditorData.VAdjustment.Value) / editor.LineHeight);
			lineHover = null;
			if (lineNumber < editor.Splitter.LineCount) {
				LineSegment lineSegment = editor.Document.GetLine (lineNumber);
				if (lineHover != lineSegment) {
					lineHover = lineSegment;
					editor.QueueDraw ();
				}
			}
		}
		
		public override void MouseLeft ()
		{
			if (lineHover != null) {
				lineHover = null;
				editor.QueueDraw ();
			}
		}
		
		public override void OptionsChanged ()
		{
			foldBgGC = new Gdk.GC (editor.GdkWindow);
			foldBgGC.RgbFgColor = editor.ColorStyle.FoldBg;
			
			foldLineGC = new Gdk.GC (editor.GdkWindow);
			foldLineGC.RgbFgColor = editor.ColorStyle.FoldLine;
			
			foldLineHighlightedGC = new Gdk.GC (editor.GdkWindow);
			foldLineHighlightedGC.RgbFgColor = editor.ColorStyle.FoldLineHighlighted;
			
			foldToggleMarkerGC = new Gdk.GC (editor.GdkWindow);
			foldToggleMarkerGC.RgbFgColor = editor.ColorStyle.FoldToggleMarker;
		}
		
		Gdk.GC foldBgGC, foldLineGC, foldLineHighlightedGC, foldToggleMarkerGC;
		
		public override void Dispose ()
		{
			if (foldBgGC != null) {
				foldBgGC.Dispose ();
				foldBgGC = null;
			}
			if (foldLineGC != null) {
				foldLineGC.Dispose ();
				foldLineGC = null;
			}
			if (foldLineHighlightedGC != null) {
				foldLineHighlightedGC.Dispose ();
				foldLineHighlightedGC = null;
			}
			if (foldToggleMarkerGC != null) {
				foldToggleMarkerGC.Dispose ();
				foldToggleMarkerGC = null;
			}
		}
		
		void DrawFoldSegment (Gdk.Window win, int x, int y, bool isOpen, bool isSelected)
		{
			Gdk.Rectangle drawArea = new Gdk.Rectangle (x + 3, y + 3, foldSegmentSize, foldSegmentSize);
			win.DrawRectangle (foldBgGC, true, drawArea);
			win.DrawRectangle (isSelected ? foldLineHighlightedGC  : foldLineGC, false, drawArea);
			
			win.DrawLine (foldToggleMarkerGC, 
			              drawArea.Left  + 2,
			              drawArea.Top + 4,
			              drawArea.Right - 2,
			              drawArea.Top + 4);
			
			if (!isOpen)
				win.DrawLine (foldToggleMarkerGC, 
				              drawArea.Left  + 4,
				              drawArea.Top + 2,
				              drawArea.Left  + 4,
				              drawArea.Bottom - 2);
		}
		
		void DrawDashedVLine (Gdk.Window win, int x, int top, int bottom)
		{
			for (int i = top; i <= bottom; i++) {
				if (i % 2 == 0)
					win.DrawPoint (foldLineGC, x, i);
			}
		}
		
		bool IsMouseHover (List<FoldSegment> foldings)
		{
			foreach (FoldSegment segment in foldings) {
				if (this.lineHover == segment.StartLine)
					return true;
			}
			return false;
		}
		
		public override void Draw (Gdk.Window win, Gdk.Rectangle area, int line, int x, int y)
		{
			Gdk.Rectangle drawArea = new Gdk.Rectangle (x, y, Width, editor.LineHeight);
			win.DrawRectangle (foldBgGC, true, drawArea);
			DrawDashedVLine (win, x, drawArea.Top, drawArea.Bottom);
			
			if (line < editor.Splitter.LineCount) {
				LineSegment lineSegment = editor.Document.GetLine (line);
				List<FoldSegment> startFoldings      = editor.Document.GetStartFoldings (lineSegment);
				List<FoldSegment> containingFoldings = editor.Document.GetFoldingContaining (lineSegment);
				List<FoldSegment> endFoldings        = editor.Document.GetEndFoldings (lineSegment);
				
				bool isFoldStart  = startFoldings.Count > 0;
				bool isContaining = containingFoldings.Count > 0;
				bool isFoldEnd    = endFoldings.Count > 0;
				
				bool isStartSelected      = IsMouseHover (startFoldings);
				bool isContainingSelected = IsMouseHover (containingFoldings);
				bool isEndSelected        = IsMouseHover (endFoldings);
										
				int foldSegmentYPos = y + 3;
				int xPos = x + 3 + 4;
				
				if (isFoldStart) {
					bool isVisible         = true;
					bool moreLinedOpenFold = false;
					foreach (FoldSegment foldSegment in startFoldings) {
						if (foldSegment.IsFolded) {
							isVisible = false;
						} else {
							moreLinedOpenFold = foldSegment.EndLine.Offset > foldSegment.StartLine.Offset;
						}
					}
					bool isFoldEndFromUpperFold = false;
					foreach (FoldSegment foldSegment in endFoldings) {
						if (foldSegment.EndLine.Offset > foldSegment.StartLine.Offset && !foldSegment.IsFolded) 
							isFoldEndFromUpperFold = true;
					}
					DrawFoldSegment (win, x, y, isVisible, isStartSelected);
					if (isContaining || isFoldEndFromUpperFold) 
						win.DrawLine (isContainingSelected ? foldLineHighlightedGC : foldLineGC, xPos, drawArea.Top, xPos, foldSegmentYPos - 1);
					if (isContaining || moreLinedOpenFold) 
						win.DrawLine (isEndSelected || (isStartSelected && isVisible) || isContainingSelected ? foldLineHighlightedGC : foldLineGC, xPos, foldSegmentYPos + foldSegmentSize + 1, xPos, drawArea.Bottom);
				} else {
					if (isFoldEnd) {
						int yMid = drawArea.Top + drawArea.Height / 2;
						win.DrawLine (isEndSelected ? foldLineHighlightedGC : foldLineGC, xPos, yMid, xPos + foldSegmentSize / 2, yMid);
						win.DrawLine (isContainingSelected || isEndSelected ? foldLineHighlightedGC : foldLineGC, xPos, drawArea.Top, xPos, yMid);
						if (isContaining) 
							win.DrawLine (isContainingSelected ? foldLineHighlightedGC : foldLineGC, xPos, yMid + 1, xPos, drawArea.Bottom);
					} else if (isContaining) {
						win.DrawLine (isContainingSelected ? foldLineHighlightedGC : foldLineGC, xPos, drawArea.Top, xPos, drawArea.Bottom);
					}
				}
			}
		}
	}
}
