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
	/// Represents the report item for a line.
	///</summary>
	[Serializable]
	internal class Line : ReportItem
	{
		// Line has no additional elements/attributes beyond ReportItem
		internal Line(Report r, ReportLink p, XmlNode xNode) : base(r,p,xNode)
		{
			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				// nothing beyond reportitem for now
				if (!ReportItemElement(xNodeLoop))	// try at ReportItem level
				{					
					// don't know this element - log it
					OwnerReport.rl.LogError(4, "Unknown Textbox element " + xNodeLoop.Name + " ignored.");
				}
			}
		}
		override internal void Run(IPresent ip, Row row)
		{
			ip.Line(this, row);
		}

		override internal void RunPage(Pages pgs, Row row)
		{
			if (IsHidden(row))
				return;

			SetPagePositionBegin(pgs);
			PageLine pl = new PageLine();
			pl.X = OffsetCalc + LeftCalc;
			if (Top != null)
				pl.Y = Top.Points;
			pl.Y2 = Y2;
			pl.X2 = X2;

			if (Style != null)
				pl.SI = Style.GetStyleInfo(row);
			else
				pl.SI = new StyleInfo();	// this will just default everything

			pgs.CurrentPage.AddObject(pl);
			SetPagePositionEnd(pgs, pgs.CurrentPage.YOffset);
		}

		internal float X2
		{
			get 
			{
				float x2=OffsetCalc+LeftCalc;
				if (Width != null)
					x2 += Width.Points;
				return x2;
			}
		}		

		internal float Y2
		{
			get 
			{
				float y2=0;
				if (Top != null)
					y2 = Top.Points;
				if (Height != null)
					y2 += Height.Points;
				return y2;
			}
		}
	}
}
