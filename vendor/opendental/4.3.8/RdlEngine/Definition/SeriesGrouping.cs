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

namespace fyiReporting.RDL
{
	///<summary>
	/// Chart Series grouping (both dynamic and static).
	///</summary>
	[Serializable]
	internal class SeriesGrouping : ReportLink
	{
		DynamicSeries _DynamicSeries;	// Dynamic Series headings for this grouping
		StaticSeries _StaticSeries;		// Static Series headings for this grouping		
	
		internal SeriesGrouping(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_DynamicSeries=null;
			_StaticSeries=null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "DynamicSeries":
						_DynamicSeries = new DynamicSeries(r, this, xNodeLoop);
						break;
					case "StaticSeries":
						_StaticSeries = new StaticSeries(r, this, xNodeLoop);
						break;
					default:	
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown SeriesGrouping element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
		}
		
		override internal void FinalPass()
		{
			if (_DynamicSeries != null)
				_DynamicSeries.FinalPass();
			if (_StaticSeries != null)
				_StaticSeries.FinalPass();
			return;
		}

		internal DynamicSeries DynamicSeries
		{
			get { return  _DynamicSeries; }
			set {  _DynamicSeries = value; }
		}

		internal StaticSeries StaticSeries
		{
			get { return  _StaticSeries; }
			set {  _StaticSeries = value; }
		}
	}
}
