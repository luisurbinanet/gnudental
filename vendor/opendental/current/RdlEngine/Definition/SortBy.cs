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
	/// A single sort expression and direction.
	///</summary>
	[Serializable]
	internal class SortBy : ReportLink
	{
			Expression _SortExpression;	// (Variant) The expression to sort the groups by.
						// The functions RunningValue and RowNumber
						// are not allowed in SortExpression.
						// References to report items are not allowed.
			SortDirectionEnum _Direction;	// Indicates the direction of the sort
										// Ascending (Default) | Descending
	
		internal SortBy(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_SortExpression=null;
			_Direction=SortDirectionEnum.Ascending;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "SortExpression":
						_SortExpression = new Expression(r, this, xNodeLoop, ExpressionType.Variant);
						break;
					case "Direction":
						_Direction = SortDirection.GetStyle(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					default:
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown SortBy element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
			if (_SortExpression == null)
				OwnerReport.rl.LogError(8, "SortBy requires the SortExpression element.");
		}

		// Handle parsing of function in final pass
		override internal void FinalPass()
		{
			if (_SortExpression != null)
				_SortExpression.FinalPass();
			return;
		}

		internal Expression SortExpression
		{
			get { return  _SortExpression; }
			set {  _SortExpression = value; }
		}

		internal SortDirectionEnum Direction
		{
			get { return  _Direction; }
			set {  _Direction = value; }
		}
	}
}
