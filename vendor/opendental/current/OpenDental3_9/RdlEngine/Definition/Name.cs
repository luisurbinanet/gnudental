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
	/// A report object name.   CLS comliant identifier.
	///</summary>
	[Serializable]
	internal class Name
	{
		string _Name;			// name CLS compliant identifier; www.unicode.org/unicode/reports/tr15/tr15-18.html
	
		internal Name(string name)
		{
			_Name=name;
		}

		internal string Nm
		{
			get { return  _Name; }
			set {  _Name = value; }
		}

		public override string ToString()
		{
			return _Name;
		}
	}
}
