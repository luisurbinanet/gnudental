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
using System.Collections;

namespace fyiReporting.RDL
{
	///<summary>
	/// TableRow represents a Row in a table.  This can be part of a header, footer, or detail definition.
	///</summary>
	[Serializable]
	internal class TableRow : ReportLink
	{
		TableCells _TableCells;	// Contents of the row. One cell per column
		RSize _Height;				// Height of the row
		Visibility _Visibility;		// Indicates if the row should be hidden		
		bool _CanGrow;			// indicates that row height can increase in size
		ArrayList _GrowList;	// list of TextBox's that need to be checked for growth
		float _CalcHeight;		// dynamic when CanGrow true

		internal TableRow(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_TableCells=null;
			_Height=null;
			_Visibility=null;
			_CanGrow = false;
			_GrowList = null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "TableCells":
						_TableCells = new TableCells(r, this, xNodeLoop);
						break;
					case "Height":
						_Height = new RSize(r, xNodeLoop);
						break;
					case "Visibility":
						_Visibility = new Visibility(r, this, xNodeLoop);
						break;
					default:
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown TableRow element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
			if (_TableCells == null)
				OwnerReport.rl.LogError(8, "TableRow requires the TableCells element.");
			if (_Height == null)
				OwnerReport.rl.LogError(8, "TableRow requires the Height element.");
		}
		
		override internal void FinalPass()
		{
			_TableCells.FinalPass();
			if (_Visibility != null)
				_Visibility.FinalPass();

			_CalcHeight = this.Height.Points;

			foreach (TableCell tc in _TableCells.Items)
			{
				ReportItem ri = tc.ReportItems.Items[0] as ReportItem;
				if (!(ri is Textbox))
					continue;
				Textbox tb = ri as Textbox;
				if (tb.CanGrow)
				{
					if (this._GrowList == null)
						_GrowList = new ArrayList();
					_GrowList.Add(tb);
					_CanGrow = true;
				}
			}

			if (_CanGrow)				// shrink down the resulting list
				_GrowList.TrimToSize();

			return;
		}

		internal void Run(IPresent ip, Row row)
		{
			if (this.Visibility != null && Visibility.IsHidden(row))
				return;

			ip.TableRowStart(this, row);
			_TableCells.Run(ip, row);
			ip.TableRowEnd(this, row);
			return ;
		}
 
		internal void RunPage(Pages pgs, Row row)
		{
			if (this.Visibility != null && Visibility.IsHidden(row))
				return;

			_TableCells.RunPage(pgs, row);

			pgs.CurrentPage.YOffset += _CalcHeight;
			return ;
		}

		internal TableCells TableCells
		{
			get { return  _TableCells; }
			set {  _TableCells = value; }
		}

		internal RSize Height
		{
			get { return  _Height; }
			set {  _Height = value; }
		}

		internal float HeightOfRow(Pages pgs, Row r)
		{
			if (this.Visibility != null && Visibility.IsHidden(r))
			{
				_CalcHeight = 0;
				return 0;
			}

			float defnHeight = _Height.Points;
			if (!_CanGrow)
			{
				_CalcHeight = defnHeight;
				return defnHeight;
			}

			float height=0;
			foreach (Textbox tb in this._GrowList)
			{
				height = Math.Max(height, tb.RunTextCalcHeight(pgs.G, r));
			}
			_CalcHeight = Math.Max(height, defnHeight);
			return _CalcHeight;
		}

		internal float HeightCalc
		{
			get {return _CalcHeight;}
		}

		internal Visibility Visibility
		{
			get { return  _Visibility; }
			set {  _Visibility = value; }
		}

		internal bool CanGrow
		{
			get { return _CanGrow; }
		}

		internal ArrayList GrowList
		{
			get { return _GrowList; }
		}
	}
}
