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
	/// Collection of specific reportitems (e.g. TextBoxs, Images, ...)
	///</summary>
	[Serializable]
	internal class ReportItems : ReportLink
	{
		ArrayList _Items;				// list of report items

		[NonSerialized] float _XOffset;	// X offset of all items in list; reflects parent position

		internal ReportItems(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			ReportItem ri;
			_Items = new ArrayList();

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "Rectangle":
						ri = new Rectangle(r, this, xNodeLoop);
						break;
					case "Line":
						ri = new Line(r, this, xNodeLoop);
						break;
					case "Textbox":
						ri = new Textbox(r, this, xNodeLoop);
						break;
					case "Image":
						ri = new Image(r, this, xNodeLoop);
						break;
					case "Subreport":
						ri = new Subreport(r, this, xNodeLoop);
						break;
					// DataRegions: list, table, matrix, chart
					case "List": 
						ri = new List(r, this, xNodeLoop);
						break;
					case "Table": 
						ri = new Table(r, this, xNodeLoop);
						break;
					case "Matrix": 
						ri = new Matrix(r, this, xNodeLoop);
						break;
					case "Chart": 
						ri = new Chart(r, this, xNodeLoop);
						break;
					case "ChartExpression":		// For internal use only 
						ri = new ChartExpression(r, this, xNodeLoop);
						break;
					default:
						ri=null;		// don't know what this is
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown ReportItems element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
				if (ri != null)
				{
					_Items.Add(ri);
				}
			}
			if (_Items.Count == 0)
				OwnerReport.rl.LogError(8, "At least one item must be in the ReportItems.");
		}
		
		override internal void FinalPass()
		{
			foreach (ReportItem ri in _Items)
			{
				ri.FinalPass();
			}
			_Items.Sort();				// sort on ZIndex; y, x (see ReportItem compare routine)

			foreach (ReportItem ri in _Items)	
				ri.PositioningFinalPass(_Items);

			return;
		}

		internal void Run(IPresent ip, Row row)
		{
			foreach (ReportItem ri in _Items)
			{
				ri.Run(ip, row);
			}
			return;
		}

		internal void RunPage(Pages pgs, Row row, float xOffset)
		{
			_XOffset = xOffset;
			foreach (ReportItem ri in _Items)
			{
				ri.RunPage(pgs, row);
			}
			return;
		}

		internal ArrayList Items
		{
			get { return  _Items; }
		}

		internal float XOffset
		{
			get { return _XOffset; }
			set { _XOffset = value; }
		}

	}
}
