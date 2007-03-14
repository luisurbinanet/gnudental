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
	/// Ordered list of values used as a default for a parameter
	///</summary>
	[Serializable]
	internal class Values : ReportLink, ICollection
	{
		ArrayList _Items;			// list of expression items

		internal Values(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			Expression v;
			_Items = new ArrayList();
			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "Value":
						v = new Expression(r, this, xNodeLoop, ExpressionType.Variant);
						break;
					default:	
						v=null;	
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown Value element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
				if (v != null)
					_Items.Add(v);
			}
		}

		// Handle parsing of function in final pass
		override internal void FinalPass()
		{
			foreach (Expression e in _Items)
			{	
				e.FinalPass();
			}
			return;
		}

		internal ArrayList Items
		{
			get { return  _Items; }
		}
		#region ICollection Members

		public bool IsSynchronized
		{
			get
			{
				return _Items.IsSynchronized;
			}
		}

		public int Count
		{
			get
			{
				return _Items.Count;
			}
		}

		public void CopyTo(Array array, int index)
		{
			_Items.CopyTo(array, index);
		}

		public object SyncRoot
		{
			get
			{
				return _Items.SyncRoot;
			}
		}

		#endregion

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return _Items.GetEnumerator();
		}

		#endregion
	}
}
