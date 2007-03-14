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

namespace fyiReporting.RDL
{
	///<summary>
	/// CategoryGroupings definition and processing.
	///</summary>
	[Serializable]
	internal class CategoryGroupings : ReportLink
	{
		ArrayList _Items;			// list of report items

		internal CategoryGroupings(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			CategoryGrouping cg;
			_Items = new ArrayList();
			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "CategoryGrouping":
						cg = new CategoryGrouping(r, this, xNodeLoop);
						break;
					default:
						cg=null;		// don't know what this is
						break;
				}
				if (cg != null)
					_Items.Add(cg);
			}
			if (_Items.Count == 0)
				OwnerReport.rl.LogError(8, "For CategoryGroupings at least one CategoryGrouping is required.");
		}
		
		override internal void FinalPass()
		{
			foreach (CategoryGrouping cg in _Items)
			{
				cg.FinalPass();
			}
			return;
		}

		internal ArrayList Items
		{
			get { return  _Items; }
		}
	}
}
