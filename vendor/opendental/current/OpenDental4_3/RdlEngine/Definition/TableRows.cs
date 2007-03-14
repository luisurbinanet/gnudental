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
using System.Collections;
using System.Xml;
using System.IO;

namespace fyiReporting.RDL
{
	///<summary>
	/// TableRows definition and processing.
	///</summary>
	[Serializable]
	internal class TableRows : ReportLink
	{
		ArrayList _Items;			// list of report items
		float _HeightOfRows;		// height of contained rows
		bool _CanGrow;				// if any TableRow contains a TextBox with CanGrow

		internal TableRows(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			TableRow t;
			_Items = new ArrayList();
			_CanGrow = false;
			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "TableRow":
						t = new TableRow(r, this, xNodeLoop);
						break;
					default:	
						t=null;		// don't know what this is
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown TableRows element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
				if (t != null)
					_Items.Add(t);
			}
			if (_Items.Count == 0)
				OwnerReport.rl.LogError(8, "For TableRows at least one TableRow is required.");
		}
		
		override internal void FinalPass()
		{
			_HeightOfRows = 0;
			foreach (TableRow t in _Items)
			{
				_HeightOfRows += t.Height.Points;
				t.FinalPass();
				_CanGrow |= t.CanGrow;
			}

			return;
		}

		internal void Run(IPresent ip, Row row)
		{
			foreach (TableRow t in _Items)
			{
				t.Run(ip, row);
			}
			return;
		}

		internal void RunPage(Pages pgs, Row row)
		{
			foreach (TableRow t in _Items)
			{
				t.RunPage(pgs, row);
			}
			return;
		}

		internal float DefnHeight()
		{
			float height=0;
			foreach (TableRow tr in this._Items)
			{
				height += tr.Height.Points;
			}
			return height;
		}

		internal float HeightOfRows(Pages pgs, Row r)
		{
			if (!this._CanGrow)
				return _HeightOfRows;
			
			float height=0;
			foreach (TableRow tr in this._Items)
			{
				height += tr.HeightOfRow(pgs, r);
			}

			return Math.Max(height, _HeightOfRows);
		}

		internal ArrayList Items
		{
			get { return  _Items; }
		}
	}
}
