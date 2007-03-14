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
	/// TableColumn definition and processing.
	///</summary>
	[Serializable]
	internal class TableColumn : ReportLink
	{
		RSize _Width;		// Width of the column
		Visibility _Visibility;	// Indicates if the column should be hidden	
		float _XPosition;	// Set at runtime by Page processing; potentially dynamic at runtime
							//  since visibility is an expression
	
		internal TableColumn(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_Width=null;
			_Visibility=null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "Width":
						_Width = new RSize(r, xNodeLoop);
						break;
					case "Visibility":
						_Visibility = new Visibility(r, this, xNodeLoop);
						break;
					default:
						break;
				}
			}
			if (_Width == null)
				OwnerReport.rl.LogError(8, "TableColumn requires the Width element.");
		}
		
		override internal void FinalPass()
		{
			if (_Visibility != null)
				_Visibility.FinalPass();
			return;
		}

		internal void Run(IPresent ip, Row row)
		{
		}

		internal RSize Width
		{
			get { return  _Width; }
			set {  _Width = value; }
		}

		internal float XPosition
		{
			get { return _XPosition; }
			set { _XPosition = value; }
		}

		internal Visibility Visibility
		{
			get { return  _Visibility; }
			set {  _Visibility = value; }
		}

		internal bool IsHidden(Row r)
		{
			if (_Visibility == null)
				return false;
			return _Visibility.IsHidden(r);
		}
	}
}
