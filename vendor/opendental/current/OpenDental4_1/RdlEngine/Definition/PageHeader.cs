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

namespace fyiReporting.RDL
{
	///<summary>
	/// Defines the page header of the report
	///</summary>
	[Serializable]
	internal class PageHeader : ReportLink
	{
		RSize _Height;		// Height of the page header
		bool _PrintOnFirstPage;	// Indicates if the page header should be shown on
								// the first page of the report
		bool _PrintOnLastPage;	// Indicates if the page header should be shown on
							// the last page of the report. Not used in singlepage reports.
		ReportItems _ReportItems;	// The region that contains the elements of the header layout
							// No data regions or subreports are allowed in the page header
		Style _Style;		// Style information for the page header		
	
		internal PageHeader(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_Height=null;
			_PrintOnFirstPage=true;
			_PrintOnLastPage=true;
			_ReportItems=null;
			_Style=null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "Height":
						_Height = new RSize(r, xNodeLoop);
						break;
					case "PrintOnFirstPage":
						_PrintOnFirstPage = XmlUtil.Boolean(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					case "PrintOnLastPage":
						_PrintOnLastPage = XmlUtil.Boolean(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					case "ReportItems":
						_ReportItems = new ReportItems(r, this, xNodeLoop);
						break;
					case "Style":
						_Style = new Style(r, this, xNodeLoop);
						break;
					default:
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown PageHeader element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
			if (_Height == null)
				OwnerReport.rl.LogError(8, "PageHeader Height is required.");
		}
		
		override internal void FinalPass()
		{
			if (_ReportItems != null)
				_ReportItems.FinalPass();
			if (_Style != null)
				_Style.FinalPass();
			return;
		}

		internal void Run(IPresent ip, Row row)
		{
			if (OwnerReport.Subreport != null)
				return;		// don't process page headers for sub-reports
			ip.PageHeaderStart(this);
			if (_ReportItems != null)
				_ReportItems.Run(ip, row);
			ip.PageHeaderEnd(this);
		}

		internal void RunPage(Pages pgs)
		{
			if (OwnerReport.Subreport != null)
				return;		// don't process page headers for sub-reports
			if (_ReportItems == null)
				return;

			OwnerReport.TotalPages.RuntimePageCount = pgs.PageCount;
			foreach (Page p in pgs)
			{
				p.YOffset = OwnerReport.TopMargin.Points;
				p.XOffset = 0;
				pgs.CurrentPage = p;
				OwnerReport.PageNumber.RuntimePageNumber = p.PageNumber;
				if (p.PageNumber == 1 && pgs.Count > 1 && !_PrintOnFirstPage)
					continue;		// Don't put header on the first page
				if (p.PageNumber == pgs.Count && !_PrintOnLastPage)
					continue;		// Don't put header on the last page
				_ReportItems.RunPage(pgs, null, OwnerReport.LeftMargin.Points);
			}
		}

		internal RSize Height
		{
			get { return  _Height; }
			set {  _Height = value; }
		}

		internal bool PrintOnFirstPage
		{
			get { return  _PrintOnFirstPage; }
			set {  _PrintOnFirstPage = value; }
		}

		internal bool PrintOnLastPage
		{
			get { return  _PrintOnLastPage; }
			set {  _PrintOnLastPage = value; }
		}

		internal ReportItems ReportItems
		{
			get { return  _ReportItems; }
			set {  _ReportItems = value; }
		}

		internal Style Style
		{
			get { return  _Style; }
			set {  _Style = value; }
		}
	}
}
