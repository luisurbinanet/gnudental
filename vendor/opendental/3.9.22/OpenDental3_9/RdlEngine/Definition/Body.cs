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
	/// Body definition and processing.  Contains the elements of the report body.
	///</summary>
	[Serializable]
	internal class Body : ReportLink
	{
		ReportItems _ReportItems;		// The region that contains the elements of the report body
		RSize _Height;		// Height of the body
		int _Columns;		// Number of columns for the report
							// Default: 1. Min: 1. Max: 1000
		RSize _ColumnSpacing; // Size Spacing between each column in multi-column
							// output 	Default: 0.5 in
		Style _Style;		// Default style information for the body	
	
		[NonSerialized] int _CurrentColumn;	// used at runtime to control columns
		
		internal Body(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_ReportItems = null;
			_Height = null;
			_Columns = 1;
			_ColumnSpacing=null;
			_Style=null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "ReportItems":
						_ReportItems = new ReportItems(r, this, xNodeLoop);	// need a class for this
						break;
					case "Height":
						_Height = new RSize(r, xNodeLoop);
						break;
					case "Columns":
						_Columns = XmlUtil.Integer(xNodeLoop.InnerText);
						break;
					case "ColumnSpacing":
						_ColumnSpacing = new RSize(r, xNodeLoop);
						break;
					case "Style":
						_Style = new Style(r, this, xNodeLoop);
						break;
					default:	
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown Body element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
			if (_Height == null)
				OwnerReport.rl.LogError(8, "Body Height not specified.");
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
			ip.BodyStart(this);

			if (_ReportItems != null)
				_ReportItems.Run(ip, null);	// not sure about the row here?

			ip.BodyEnd(this);
			return ;
		}

		internal void RunPage(Pages pgs)
		{
			if (OwnerReport.Subreport == null)
			{	// Only set bottom of pages when on top level report
				pgs.BottomOfPage = OwnerReport.BottomOfPage;
				pgs.CurrentPage.YOffset = OwnerReport.TopOfPage;
			}
			_CurrentColumn = 0;

			if (_ReportItems != null)
				_ReportItems.RunPage(pgs, null, OwnerReport.LeftMargin.Points);

			return ;
		}

		internal ReportItems ReportItems
		{
			get { return  _ReportItems; }
		}

		internal RSize Height
		{
			get { return  _Height; }
			set {  _Height = value; }
		}

		internal int Columns
		{
			get { return  _Columns; }
			set {  _Columns = value; }
		}

		internal int CurrentColumn
		{
			get { return  _CurrentColumn; }
			set {  _CurrentColumn = value; }
		}

		internal RSize ColumnSpacing
		{
			get 
			{
				if (_ColumnSpacing == null)
					_ColumnSpacing = new RSize(this.OwnerReport, ".5 in");

				return  _ColumnSpacing; 
			}
			set {  _ColumnSpacing = value; }
		}

		internal Style Style
		{
			get { return  _Style; }
			set {  _Style = value; }
		}
	}
}
