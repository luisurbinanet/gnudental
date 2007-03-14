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


namespace fyiReporting.RDL
{
	///<summary>
	/// The Textbox definition and runtime processing.  Inherits from ReportItem.
	///</summary>
	[Serializable]
	internal class Textbox : ReportItem
	{
		Expression _Value;	// (Variant) An expression, the value of which is
							// displayed in the text-box.
							// This can be a constant expression for constant labels.
		bool _CanGrow;		// Indicates the Textbox size can
							// increase to accommodate the contents
		bool _CanShrink;	// Indicates the Textbox size can
							// decrease to match the contents
		string _HideDuplicates;	// Indicates the item should be hidden
							//when the value of the expression
							//associated with the report item is the
							//same as the preceding instance. The
							//value of HideDuplicates is the name
							//of a grouping or data set over which
							//to apply the hiding. Each time a
							//new instance of that group is
							//encountered, the first instance of
							//this report item will not be hidden.
							//Rows on a previous page are
							//ignored for the purposes of hiding
							//duplicates. If the textbox is in a
							//table or matrix cell, only the text
							//will be hidden. The textbox will
							//remain to provide background and
							//border for the cell.
							//Ignored in matrix subtotals.
		ToggleImage _ToggleImage;	// Indicates the initial state of a
								// toggling image should one be
								// displayed as a part of the textbox.
		DataElementStyleEnum _DataElementStyle;	// Indicates whether textbox value
								// should render as an element or attribute: Auto (Default)
								// Auto uses the setting on the Report element.
		bool _IsToggle;		// Textbox is used to toggle a detail row
	
		// runtime
		[NonSerialized] int _RunCount;			// number of times TextBox is rendered at runtime;
							//    used to generate unique names for toggling visibility
		[NonSerialized] float _RunHeight=0;		// the runtime height (in points)
		[NonSerialized] string _PreviousText=null;	// previous text displayed
		[NonSerialized] Page _PreviousPage=null;	//  page previous text was shown on

		internal Textbox(Report r, ReportLink p, XmlNode xNode):base(r,p,xNode)
		{
			_Value=null;
			_CanGrow=false;
			_CanShrink=false;
			_HideDuplicates=null;
			_ToggleImage=null;
			_DataElementStyle=DataElementStyleEnum.Auto;
			_RunCount = 0;
		
			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "Value":
						_Value = new Expression(r, this, xNodeLoop, ExpressionType.Variant);
						break;
					case "CanGrow":
						_CanGrow = XmlUtil.Boolean(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					case "CanShrink":
						_CanShrink = XmlUtil.Boolean(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					case "HideDuplicates":
						_HideDuplicates = xNodeLoop.InnerText;
						break;
					case "ToggleImage":
						_ToggleImage = new ToggleImage(r, this, xNodeLoop);
						break;
					case "DataElementStyle":
						_DataElementStyle = fyiReporting.RDL.DataElementStyle.GetStyle(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					default:
						if (ReportItemElement(xNodeLoop))	// try at ReportItem level
							break;
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown Textbox element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
			
			if (_Value == null)
				OwnerReport.rl.LogError(8, "Textbox value not specified for " + (this.Name == null? "'name not specified'": this.Name.Nm));

			if (this.Name != null)
			{
				try
				{
					OwnerReport.LUReportItems.Add(this.Name.Nm, this);		// add to referenceable TextBoxes
				}
				catch		// Duplicate name
				{
					OwnerReport.rl.LogError(4, "Duplicate Textbox name '" + this.Name.Nm + "' ignored.");
				}
			}
		}

		// Handle parsing of function in final pass
		override internal void FinalPass()
		{
			base.FinalPass();
			_Value.FinalPass();

			if (this.DataElementName == null && this.Name == null)
			{
				// no name or dataelementname; try using expression
				FunctionField ff = _Value.Expr as FunctionField;
				if (ff != null && ff.Fld != null)
				{
					this.DataElementName = ff.Fld.DataField;
				}
			}

			if (_ToggleImage != null)
				_ToggleImage.FinalPass();

			if (_HideDuplicates != null)
			{
				object o = OwnerReport.LUAggrScope[_HideDuplicates];
				if (o == null)
				{
					OwnerReport.rl.LogError(4, "HideDuplicate '" +_HideDuplicates + "' is not a Group or DataSet name.   It will be ignored.");
					_HideDuplicates=null;
				}
				else if (o is Grouping)
				{	
					Grouping g = o as Grouping;
					g.AddHideDuplicates(this);
				}
				else if (o is DataSet)
				{
					DataSet ds = o as DataSet;
					ds.AddHideDuplicates(this);
				}
			}
			return;
		}

		internal void ResetPrevious()
		{
			_PreviousText=null;	
			_PreviousPage=null;	
		}

		override internal void Run(IPresent ip, Row row)
		{
			base.Run(ip, row);

			_RunCount++;		// Increment the run count
			string t = RunText(row);
			bool bDup =	RunTextIsDuplicate(t, null);
			if (bDup)
			{
				if (!(this.IsTableOrMatrixCell))	// don't put out anything if not in Table or Matrix
					return;
				t = "";		// still need to put out the cell
			}
			ip.Textbox(this, t, row);

			if (!bDup)
				_PreviousText=t;	// set for next time
		}

		override internal void RunPage(Pages pgs, Row row)
		{
			_RunCount++;		// Increment the run count

			if (IsHidden(row))
				return;

			SetPagePositionBegin(pgs);

			string t = RunText(row);	// get the text

			bool bDup =	RunTextIsDuplicate(t, pgs.CurrentPage);
			if (bDup)
			{
				if (!(this.IsTableOrMatrixCell))	// don't put out anything if not in Table or Matrix
					return;
				t = "";		// still need to put out the cell
			}

			PageText pt = new PageText(t);
			SetPagePositionAndStyle(pt, row);
			if (this.CanGrow && _RunHeight == 0)	// when textbox is in a DataRegion this will already be called
				this.RunTextCalcHeight(pgs.G, row);
			pt.H = Math.Max(pt.H, _RunHeight);		// reset height
			if (pt.SI.BackgroundImage != null)
				pt.SI.BackgroundImage.H = pt.H;		//   and in the background image
			pt.CanGrow = this.CanGrow;

			Page p = pgs.CurrentPage;
			p.AddObject(pt);
			if (!bDup)
			{
				_PreviousText=t;	// previous text displayed
				_PreviousPage=p;	//  page previous text was shown on
			}

			SetPagePositionEnd(pgs, pt.Y+pt.H);

		}

		// routine to determine if text is considered to be a duplicate;
		//  ie: same as previous text and on same page
		private bool RunTextIsDuplicate(string t, Page p)
		{
			if (this._HideDuplicates == null)
				return false;
			if (t == _PreviousText && p == _PreviousPage)
				return true;

			return false;
		}

		internal string RunText(Row row)
		{
			object o = _Value.Evaluate(row);
			string t = Style.GetFormatedString(this.Style, row, o, _Value.GetTypeCode());
			return t;
		}
		
		internal float RunTextCalcHeight(Graphics g, Row row)
		{	// normally only called when CanGrow is true
			Size s;

			if (IsHidden(row))
				return 0;

			object o = _Value.Evaluate(row);

			TypeCode tc = _Value.GetTypeCode();
			int width = this.WidthCalc(g);

			if (this.Style != null)
			{
				width -= (Style.EvalPaddingLeftPx(row) + Style.EvalPaddingRightPx(row));
				s = Style.MeasureString(g, o, tc, row, width);
			}
			else	// call the class static method
				s = Style.MeasureStringDefaults(g, o, tc, row, width);

			_RunHeight = RSize.PointsFromPixels(g, s.Height);
			if (Style != null)
				_RunHeight += (Style.EvalPaddingBottom(row) + Style.EvalPaddingTop(row));
			return _RunHeight;
		}

		internal Expression Value
		{
			get { return  _Value; }
			set {  _Value = value; }
		}

		internal bool CanGrow
		{
			get { return  _CanGrow; }
			set {  _CanGrow = value; }
		}

		internal bool CanShrink
		{
			get { return  _CanShrink; }
			set {  _CanShrink = value; }
		}

		internal string HideDuplicates
		{
			get { return  _HideDuplicates; }
			set {  _HideDuplicates = value; }
		}

		internal ToggleImage ToggleImage
		{
			get { return  _ToggleImage; }
			set {  _ToggleImage = value; }
		}

		internal bool IsToggle
		{
			get { return  _IsToggle; }
			set {  _IsToggle = value; }
		}

		internal int RunCount
		{
			get { return  _RunCount; }
		}

		internal DataElementStyleEnum DataElementStyle
		{
			get 
			{
				if (_DataElementStyle == DataElementStyleEnum.Auto)	// auto means use report
					return OwnerReport.DataElementStyle;
				else
					return  _DataElementStyle; 
			}
			set {  _DataElementStyle = value; }
		}

	}
}
