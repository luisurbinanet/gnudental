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
	/// TableColumns definition and processing.
	///</summary>
	[Serializable]
	internal class TableColumns : ReportLink
	{
		ArrayList _Items;			// list of report items

		internal TableColumns(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			TableColumn tc;
			_Items = new ArrayList();
			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "TableColumn":
						tc = new TableColumn(r, this, xNodeLoop);
						break;
					default:	
						tc=null;		// don't know what this is
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown TableColumns element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
				if (tc != null)
					_Items.Add(tc);
			}
			if (_Items.Count == 0)
				OwnerReport.rl.LogError(8, "For TableColumns at least one TableColumn is required.");
		}

		internal TableColumn this[int ci]
		{
			get
			{
				return _Items[ci] as TableColumn;
			}
		}
		
		override internal void FinalPass()
		{
			foreach (TableColumn tc in _Items)
			{
				tc.FinalPass();
			}
			return;
		}

		internal void Run(IPresent ip, Row row)
		{
			foreach (TableColumn tc in _Items)
			{
				tc.Run(ip, row);
			}
			return;
		}

		// calculate the XPositions of all the columns
		internal void CalculateXPositions(float startpos, Row row)
		{
			float x = startpos;

			foreach (TableColumn tc in _Items)
			{
				if (tc.IsHidden(row))
					continue;
				tc.XPosition = x;
				x += tc.Width.Points;
			}
			return;
		}

		internal ArrayList Items
		{
			get { return  _Items; }
		}
	}
}
