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
	/// In Charts, the collection of data values for a single data point.
	///</summary>
	[Serializable]
	internal class DataValues : ReportLink
	{
		ArrayList _Items;			// list of report items

		internal DataValues(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			DataValue dv;
			_Items = new ArrayList();
			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "DataValue":
						dv = new DataValue(r, this, xNodeLoop);
						break;
					default:	
						dv=null;		// don't know what this is
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown DataValues element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
				if (dv != null)
					_Items.Add(dv);
			}
			if (_Items.Count == 0)
				OwnerReport.rl.LogError(8, "For DataValues at least one DataValue is required.");
		}
		
		override internal void FinalPass()
		{
			foreach (DataValue dv in _Items)
			{
				dv.FinalPass();
			}
			return;
		}

		internal ArrayList Items
		{
			get { return  _Items; }
		}
	}
}
