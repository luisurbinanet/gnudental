/* ====================================================================
    Copyright (C) 2004-2005  fyiReporting Software, LLC

    This file is part of the fyiReporting RDL project.
	
    The RDL project is free software; you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation; either version 2 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

    For additional information, email info@fyireporting.com or visit
    the website www.fyiReporting.com.
*/

using System;
using System.Xml;
using System.IO;
using System.Drawing;
using System.Collections;


namespace fyiReporting.RDL
{
	///<summary>
	/// Base class of all display items in a report.  e.g. Textbox, Matrix, Table, ...
	///</summary>
	[Serializable]
	internal class ReportItem : ReportLink, IComparable
	{
		Name _Name;			// Name of the report item
		Style _Style;		// Style information for the element
		Action _Action;		// An action (e.g. a hyperlink) associated with
							// the ReportItem
		RSize _Top;			// The distance of the item from the top of the
							// containing object.
							// Defaults to 0 if omitted.
		RSize _Left;		// The distance of the item from the left of the
							// containing object.  Defaults to 0 if omitted.
		RSize _Height;		// Height of the item. Negative sizes allowed
							// only for lines (The height/width gives the
							// offset of the endpoint of the line from the start
							// point).
							//Defaults to the height of the containing object
							//minus Top if omitted.
		RSize _Width;		// Width of the item. Negative sizes allowed
							// only for lines.
							// Defaults to the width of the containing object
							// minus Left if omitted.
		int _ZIndex;		// Drawing order of the report item within the
							// containing object. Items with lower indices
							// are drawn first (appearing behind items with
							// higher indices). Items with equal indices
							// have an unspecified order.
							// Default: 0 Min: 0 Max: 2147483647
		Visibility _Visibility;	// Indicates if the item should be hidden.
		Expression _ToolTip;	// (string) A textual label for the report item. Used for
							// such things as including TITLE and ALT
							// attributes in HTML reports.
		Expression _Label;	// A label to identify an instance of a report item
							// (Variant) within the client UI (to provide a user-friendly
							// label for searching)
							//Hierarchical listing of report item and group
							//labels within the UI (the Document Map)
							//should reflect the object containment
							//hierarchy in the report definition. Peer items
							//should be listed in left-to-right top-to-bottom
							//order.
							//If the expression returns null, no item is added
							//to the Document Map. Not used for report
							//items in the page header or footer.
		string _LinkToChild;	// The name of a report item contained directly
							//within this report item that is the target
							//location for the Document Map label (if any).
							//Ignored if Label is not present. Used only for
							//Rectangle.
		Expression _Bookmark; // (string)A bookmark that can be linked to via a
							// Bookmark action
		string _RepeatWith;	// The name of a data region that this report item
							// should be repeated with if that data region
							// spans multiple pages.
							//The data region must be in the same
							//ReportItems collection as this ReportItem
							//(Since data regions are not allowed in page
							//headers/footers, this means RepeatWith will
							//be unusable in page headers/footers).
							//Not allowed if this report item is a data
							//region, subreport or rectangle that contains a
							//data region or subreport.
		Custom _Custom;		// Custom information to be handed to a report
							//  output component.
		string _DataElementName;	//The name to use for the data element/attribute
							// for this report item.
							// Default: Name of the report item
		DataElementOutputEnum _DataElementOutput;	// should item appear in data rendering?

		TableCell _TC;		// TableCell- if part of a Table
		ArrayList _YParents;	// calculated: when calculating the y position these are the items above it
		// runtime
		[NonSerialized] MatrixCellEntry _MC;	// matrix cell entry
		[NonSerialized] float _BottomPosition=float.NaN;	// used when calculating position of objects below this one.
												// this must be initialized by the inheriting class.
		[NonSerialized]	Page _CurrentPage;		// the page this reportitem was last put on; 
		internal ReportItem(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_Name=null;
			_Style=null;
			_Action=null;
			_Top=null;
			_Left=null;
			_Height=null;
			_Width=null;
			_ZIndex=0;
			_Visibility=null;
			_ToolTip=null;
			_Label=null;
			_LinkToChild=null;
			_Bookmark=null;
			_RepeatWith=null;
			_Custom=null;
			_DataElementName=null;
			_DataElementOutput=DataElementOutputEnum.Auto;
			// Run thru the attributes
			foreach(XmlAttribute xAttr in xNode.Attributes)
			{
				switch (xAttr.Name)
				{
					case "Name":
						_Name = new Name(xAttr.Value);
						break;
				}
			}
		}

		internal bool ReportItemElement(XmlNode xNodeLoop)
		{
			switch (xNodeLoop.Name)
			{
				case "Style":
					_Style = new Style(OwnerReport, this, xNodeLoop);
					break;
				case "Action":
					_Action = new Action(OwnerReport, this, xNodeLoop);
					break;
				case "Top":
					_Top = new RSize(OwnerReport, xNodeLoop);
					break;
				case "Left":
					_Left = new RSize(OwnerReport, xNodeLoop);
					break;
				case "Height":
					_Height = new RSize(OwnerReport, xNodeLoop);
					break;
				case "Width":
					_Width = new RSize(OwnerReport, xNodeLoop);
					break;
				case "ZIndex":
					_ZIndex = XmlUtil.Integer(xNodeLoop.InnerText);
					break;
				case "Visibility":
					_Visibility = new Visibility(OwnerReport, this, xNodeLoop);
					break;
				case "ToolTip":
					_ToolTip = new Expression(OwnerReport, this, xNodeLoop, ExpressionType.String);
					break;
				case "Label":
					_Label = new Expression(OwnerReport, this, xNodeLoop, ExpressionType.Variant);
					break;
				case "LinkToChild":
					_LinkToChild = xNodeLoop.InnerText;
					break;
				case "Bookmark":
					_Bookmark = new Expression(OwnerReport, this, xNodeLoop, ExpressionType.String);
					break;
				case "RepeatWith":
					_RepeatWith = xNodeLoop.InnerText;
					break;
				case "Custom":
					_Custom = new Custom(OwnerReport, this, xNodeLoop);
					break;
				case "DataElementName":
					_DataElementName = xNodeLoop.InnerText;
					break;
				case "DataElementOutput":
					_DataElementOutput = fyiReporting.RDL.DataElementOutput.GetStyle(xNodeLoop.InnerText, OwnerReport.rl);
					break;
				default:  
					return false;	// Not a report item element
			}
			return true;
		}

		// Handle parsing of function in final pass
		override internal void FinalPass()
		{
			if (_Style != null)
				_Style.FinalPass();
			if (_Action != null)
				_Action.FinalPass();
			if (_Visibility != null)
				_Visibility.FinalPass();
			if (_ToolTip != null)
				_ToolTip.FinalPass();
			if (_Label != null)
				_Label.FinalPass();
			if (_Bookmark != null)
				_Bookmark.FinalPass();
			if (_Custom != null)
				_Custom.FinalPass();

			if (Parent.Parent is TableCell)	// This is part of a table
			{
				_TC = Parent.Parent as TableCell;
			}
			else
			{
				_TC = null;
			}
			return;
		}

		internal void PositioningFinalPass(ArrayList items)
		{
			if (items.Count == 1)		// Nothing to do if only one item in list
				return;

			int x = this.Left == null? 0: this.Left.Size;
			int w = PositioningWidth(this);
			int right = x + w;
			int y = (this.Top == null? 0: this.Top.Size);

			this._YParents = new ArrayList();
			foreach (ReportItem ri in items)
			{
				if (ri == this)
					break;
				// if ReportItem is fixed in height then we can ignore it
				if (ri is Textbox)
				{
					Textbox tb = ri as Textbox;
					if (!(tb.CanGrow || tb.CanShrink))
						continue;
				}
				else if (ri is Rectangle || ri is CustomReportItem ||
					ri is Line || ri is Image || ri is Chart)
					continue;

				int x2 = ri.Left == null? 0: ri.Left.Size;
				int w2 = PositioningWidth(ri);
				if (x2 > right || x > x2 + w2)
					continue;
				if (y < (ri.Top == null? 0: ri.Top.Size) + (ri.Height == null? 0: ri.Height.Size))
					continue;
				_YParents.Add(ri);		// X coordinate overlap
			}

			// Reduce the overhead
			if (this._YParents.Count == 0)
				this._YParents = null;
			else
				this._YParents.TrimToSize();

			return;
		}

		int PositioningWidth(ReportItem ri)
		{
			int w;
			if (ri.Width == null)
			{
				if (ri is Table)
				{
					Table t = ri as Table;
					w = t.WidthInUnits;
				}
				else
					w = int.MaxValue/2;	// MaxValue/2 is just meant to be a large number (but won't overflow when adding in the x)
			}
			else
				w = ri.Width.Size;

			return w;
		}

		internal virtual void Run(IPresent ip, Row row)
		{
			return;
		}

		internal virtual void RunPage(Pages pgs, Row row)
		{
			return;
		}

		internal bool IsTableOrMatrixCell
		{
			get 
			{
				if (_TC != null || _MC != null) 
					return true;
				else
					return false;
			}
		}

		internal Name Name
		{
			get { return  _Name; }
			set {  _Name = value; }
		}

		internal Style Style
		{
			get { return  _Style; }
			set {  _Style = value; }
		}

		internal Action Action
		{
			get { return  _Action; }
			set {  _Action = value; }
		}

		internal RSize Top
		{
			get { return  _Top; }
			set {  _Top = value; }
		}

		internal RSize Left
		{
			get { return  _Left; }
			set {  _Left = value; }
		}

		internal float LeftCalc
		{
			get 
			{ 
				if (_TC != null || _MC != null || _Left == null)
					return 0;
				else
					return _Left.Points;
			}
		}

		internal float OffsetCalc
		{
			get 
			{
				float x;
				if (this._TC != null)		
				{	// must be part of a table
					Table t = _TC.OwnerTable;
					int colindex = _TC.ColIndex;

					TableColumn tc;
					tc = (TableColumn) (t.TableColumns.Items[colindex]);
					x = tc.XPosition;
				}
				else if (this._MC != null)
				{	// must be part of a matrix
					x = _MC.XPosition;
				}
				else
				{
					ReportItems ris = this.Parent as ReportItems;
					x = ris.XOffset;
				}
				
				return x;
			}
		}

		internal RSize Height
		{
			get { return  _Height; }
			set {  _Height = value; }
		}

		// routine returns the height; If not specified go up the owner chain
		//   to find an appropriate containing object
		internal float HeightOrOwnerHeight
		{
			get
			{
				if (_Height != null)
					return _Height.Points;

				float yloc = this.Top == null? 0: this.Top.Points;

				for (ReportLink rl = this.Parent; rl != null; rl = rl.Parent)
				{
					if (rl is ReportItem)
					{
						ReportItem ri = rl as ReportItem;
						if (ri.Height != null)
							return ri.Height.Points - yloc;
						continue;
					}
					if (rl is PageHeader)
					{
						PageHeader ph = rl as PageHeader;
						if (ph.Height != null)
							return ph.Height.Points - yloc;
						continue;
					}
					if (rl is PageFooter)
					{
						PageFooter pf = rl as PageFooter;
						if (pf.Height != null)
							return pf.Height.Points - yloc;
						continue;
					}
					if (rl is TableRow)
					{
						TableRow tr = rl as TableRow;
						if (tr.Height != null)
							return tr.Height.Points - yloc;
						continue;
					}
					if (rl is Body)
					{
						Body b = rl as Body;
						if (b.Height != null)
							return b.Height.Points - yloc;
						continue;
					}
				}
				return OwnerReport.PageHeight.Points;
			}
		}
		
		internal bool IsHidden(Row r)
		{
			if (this._Visibility == null)
				return false;
			return _Visibility.IsHidden(r);
		}

		internal void SetPageLeft()
		{
			if (this._TC != null)		
			{	// must be part of a table
				Table t = _TC.OwnerTable;
				int colindex = _TC.ColIndex;
				TableColumn tc = (TableColumn) (t.TableColumns.Items[colindex]);
				Left = new RSize(OwnerReport, tc.XPosition.ToString() + "pt");
			}
			else if (Left == null)
				Left = new RSize(OwnerReport, "0pt");
		}

		internal void SetPagePositionAndStyle(PageItem pi, Row row)
		{
			pi.X = this.OffsetCalc + LeftCalc;
			if (this._TC != null)		
			{	// must be part of a table
				Table t = _TC.OwnerTable;
				int colindex = _TC.ColIndex;

				// Calculate width: add up all columns within the column span
				float width=0;
				TableColumn tc;
				for (int ci=colindex; ci < colindex + _TC.ColSpan; ci++)
				{
					tc = (TableColumn) (t.TableColumns.Items[ci]);
					width += tc.Width.Points;
				}
				pi.W = width;
				pi.Y = 0;

				TableRow tr = (TableRow) (_TC.Parent.Parent);
				pi.H = tr.HeightCalc;	// this is a cached item; note tr.HeightOfRow must already be called on row
			}
			else if (this._MC != null)
			{	// must be part of a matrix
				pi.W = _MC.Width;
				pi.Y = 0;
				pi.H = _MC.Height;
			}
			else
			{	// not part of a table or matrix
				if (Top != null)
					pi.Y = this.Gap;		 //  y will get adjusted when pageitem added to page
				if (Height != null)
					pi.H = Height.Points;
				else
					pi.H = this.HeightOrOwnerHeight;
				if (Width != null)
					pi.W = Width.Points;
				else
					pi.W = this.WidthOrOwnerWidth;
			}
			if (Style != null)
				pi.SI = Style.GetStyleInfo(row);
			else
				pi.SI = new StyleInfo();	// this will just default everything
		}

		internal MatrixCellEntry MC
		{
			get { return _MC; }
			set { _MC = value; }
		}

		internal RSize Width
		{
			get { return  _Width; }
			set {  _Width = value; }
		}

		internal float WidthOrOwnerWidth
		{
			get 
			{
				if (_Width != null)
					return _Width.Points;
				float xloc = this.LeftCalc;

				for (ReportLink rl = this.Parent; rl != null; rl = rl.Parent)
				{
					if (rl is ReportItem)
					{
						ReportItem ri = rl as ReportItem;
						if (ri.Width != null)
							return ri.Width.Points - xloc;
						continue;
					}
					if (rl is PageHeader ||
						rl is PageFooter ||
						rl is Body)
					{
						return OwnerReport.Width.Points - xloc;
					}
				}

				return OwnerReport.Width.Points - xloc;
			}
		}

		internal int WidthCalc(Graphics g)
		{
			int width;
			if (this._TC != null)
			{	// must be part of a table
				Table t = _TC.OwnerTable;
				int colindex = _TC.ColIndex;

				// Calculate width: add up all columns within the column span
				width=0;
				TableColumn tc;
				for (int ci=colindex; ci < colindex + _TC.ColSpan; ci++)
				{
					tc = (TableColumn) (t.TableColumns.Items[ci]);
					width += tc.Width.PixelsX;
				}
			}
			else if (this._MC != null)
			{	// must be part of a matrix
				width = g==null? RSize.PixelsFromPoints(_MC.Width): RSize.PixelsFromPoints(g, _MC.Width);
			}
			else
			{	// not part of a table or matrix
				if (Width != null)
					width = Width.PixelsX;
				else
					width = 0;					// Not defined? assume 0
			}
			return width;
		}

		internal Page RunPageNew(Pages pgs, Page p)
		{
			// Do we need a new page or have should we fill out more body columns
			Body b = OwnerReport.Body;
			b.CurrentColumn++;		// bump to next column

			float top = OwnerReport.TopOfPage;	// calc top of page

			if (b.CurrentColumn < b.Columns)
			{		// Stay on same page but move to new column
				p.XOffset = 
					((OwnerReport.Width.Points + b.ColumnSpacing.Points) * b.CurrentColumn);
				p.YOffset = top;
				p.SetEmpty();			// consider this page empty
			}
			else
			{		// Go to new page
				b.CurrentColumn=0;
				pgs.NextOrNew();
				p = pgs.CurrentPage;
				p.YOffset = top;
				p.XOffset = 0;
			}

			return p;
		}
		
		/// <summary>
		/// Updates the current page and location based on the ReportItems 
		/// that are above it in the report.
		/// </summary>
		/// <param name="pgs"></param>
		internal void SetPagePositionBegin(Pages pgs)
		{
			// Update the current page
			if (this._YParents != null)
			{	
				ReportItem saveri=GetReportItemAbove();
				if (saveri != null)
				{
					pgs.CurrentPage = saveri._CurrentPage;
					pgs.CurrentPage.YOffset = saveri._BottomPosition;
				}
			}
			else if (this.Parent.Parent is PageHeader)
			{
				pgs.CurrentPage.YOffset = OwnerReport.TopMargin.Points;

			}
			else if (this.Parent.Parent is PageFooter)
			{
				pgs.CurrentPage.YOffset = OwnerReport.PageHeight.Points 
					- OwnerReport.BottomMargin.Points
					- OwnerReport.PageFooter.Height.Points;
			}
			else if (!(this.Parent.Parent is Body))
			{	// if not body then we don't need to do anything
			}
			else if (this.OwnerReport.Subreport != null)
			{
				//				pgs.CurrentPage = this.OwnerReport.Subreport.FirstPage;
				//				pgs.CurrentPage.YOffset = top;
			}
			else
			{
				pgs.CurrentPage =  pgs.FirstPage;	// if nothing above it (in body) then it goes on first page
				pgs.CurrentPage.YOffset = OwnerReport.TopOfPage;
			}

			return;
		}

		internal void SetPagePositionEnd(Pages pgs, float pos)
		{
			if (_TC != null)			// don't mess with page if part of a table
				return;
			this._CurrentPage = pgs.CurrentPage;
			_BottomPosition = pos;
		}

		/// <summary>
		/// Calculates the runtime y position of the object based on the height of objects 
		/// above it vertically.
		/// </summary>
		internal float Gap
		{
			get 
			{
				float top = _Top == null? 0: _Top.Points;
				ReportItem saveri=GetReportItemAbove();
				if (saveri == null)
					return top;

				float gap = top;
				if (saveri.Top != null)
					gap -= saveri.Top.Points;
				if (saveri.Height != null)
					gap -= saveri.Height.Points;

				return gap;
			}
		}

		/// <summary>
		/// Calculates the runtime y position of the object based on the height of objects 
		/// above it vertically.
		/// </summary>
		internal float RelativeY
		{
			get 
			{
				float top = _Top == null? 0: _Top.Points;
				ReportItem saveri=GetReportItemAbove();
				if (saveri == null)
					return top;

				float gap = top;
				if (saveri.Top != null)
					gap -= saveri.Top.Points;
				if (saveri.Height != null)
					gap -= saveri.Height.Points;

				return gap;
//				float y = (saveri.Top == null? 0: saveri.Top.Points) + 
//					(saveri.Height == null? 0: saveri.Height.Points) + gap;
//
//				return y;
			}
		}

		private ReportItem GetReportItemAbove()
		{
			if (this._YParents == null)
				return null;

			float maxy=float.MinValue;
			ReportItem saveri=null;
			int pgno=0;

			foreach (ReportItem ri in this._YParents)
			{
				if (ri._BottomPosition.CompareTo(float.NaN) == 0 ||
					ri._CurrentPage == null ||
					pgno > ri._CurrentPage.PageNumber)
					continue;
				if (maxy < ri._BottomPosition || pgno < ri._CurrentPage.PageNumber)
				{
					pgno = ri._CurrentPage.PageNumber;
					maxy = ri._BottomPosition;
					saveri = ri;
				}
			}
			return saveri;
		}

		internal int ZIndex
		{
			get { return  _ZIndex; }
			set {  _ZIndex = value; }
		}

		internal Visibility Visibility
		{
			get { return  _Visibility; }
			set {  _Visibility = value; }
		}

		internal Expression ToolTip
		{
			get { return  _ToolTip; }
			set {  _ToolTip = value; }
		}

		internal string ToolTipValue(Row r)
		{
			if (_ToolTip == null)
				return null;

			return _ToolTip.EvaluateString(r);
		}

		internal Expression Label
		{
			get { return  _Label; }
			set {  _Label = value; }
		}

		internal string LinkToChild
		{
			get { return  _LinkToChild; }
			set {  _LinkToChild = value; }
		}

		internal Expression Bookmark
		{
			get { return  _Bookmark; }
			set {  _Bookmark = value; }
		}

		internal string BookmarkValue(Row r)
		{
			if (_Bookmark == null)
				return null;

			return _Bookmark.EvaluateString(r);
		}

		internal TableCell TC
		{
			get {return _TC;}
		}

		internal string RepeatWith
		{
			get { return  _RepeatWith; }
			set {  _RepeatWith = value; }
		}

		internal Custom Custom
		{
			get { return  _Custom; }
			set {  _Custom = value; }
		}

		internal string DataElementName
		{
			get 
			{
				if (_DataElementName != null)
					return  _DataElementName; 
				else if (_Name != null)
					return _Name.Nm;
				else
					return null;
			}
			set {  _DataElementName = value; }
		}

		internal ArrayList YParents
		{
			get { return this._YParents; }
		}

		virtual internal DataElementOutputEnum DataElementOutput
		{
			get 
			{
				if (_DataElementOutput == DataElementOutputEnum.Auto)
				{
					if (this is Textbox)
					{
						Textbox tb = this as Textbox;
						if (tb.Value.IsConstant())
							return DataElementOutputEnum.NoOutput;
						else
							return DataElementOutputEnum.Output;
					}
					if (this is Rectangle)
						return DataElementOutputEnum.ContentsOnly;

					return DataElementOutputEnum.Output;
				}
				else
					return  _DataElementOutput; 
			}
			set {  _DataElementOutput = value; }
		}
		#region IComparable Members

		// Sort report items based on the ZIndex, top down, left to right
		public int CompareTo(object obj)
		{
			ReportItem ri = obj as ReportItem;

			int rc = this.ZIndex - ri.ZIndex;
			if (rc != 0)
				return rc;

			int t1 = this.Top == null? 0: this.Top.Size;
			int t2 = ri.Top == null? 0: ri.Top.Size;

			rc = t1 - t2;
			if (rc != 0)
				return rc;

			int l1 = this.Left == null? 0: this.Left.Size;
			int l2 = ri.Left == null? 0: ri.Left.Size;

			return l1 - l2;
		}

		#endregion
	}
}
