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

namespace fyiReporting.RDL
{
	///<summary>
	/// Runtime data structure representing the group hierarchy
	///</summary>
	[Serializable]
	internal class GroupEntry
	{
		Grouping _Group;		// Group this represents
		int _StartRow;			// Starting row of the group (inclusive)
		int _EndRow;			// Endding row of the group (inclusive)
		ArrayList _NestedGroup;	// group one hierarchy below
	
		internal GroupEntry(Grouping g, int start)
		{
			_Group = g;
			_StartRow = start;
			_EndRow = -1;
			_NestedGroup = new ArrayList();
		}

		internal int StartRow
		{
			get { return  _StartRow; }
			set {  _StartRow = value; }
		}

		internal int EndRow
		{
			get { return  _EndRow; }
			set {  _EndRow = value; }
		}

		internal ArrayList NestedGroup
		{
			get { return  _NestedGroup; }
			set {  _NestedGroup = value; }
		}

		internal Grouping Group
		{
			get { return  _Group; }
		}
	}
}
