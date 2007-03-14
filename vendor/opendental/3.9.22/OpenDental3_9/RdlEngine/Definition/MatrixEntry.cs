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
	internal class MatrixEntry
	{
		Hashtable _HashData;	// Hash table of data values
		SortedList _SortedData;	//  SortedList version of the data 
		BitArray _Rows;			// rows 
		MatrixEntry _Parent;	// parent
		ColumnGrouping _ColumnGroup;	//   Column grouping
		RowGrouping _RowGroup;	// Row grouping
		int _FirstRow;			// First row in _Rows marked true
		int _LastRow;			// Last row in _Rows marked true
		int _rowCount;			//   we save the rowCount so we can delay creating bitArray
	
		internal MatrixEntry(MatrixEntry p, int rowCount)
		{
			_HashData = new Hashtable();
			_ColumnGroup = null;
			_RowGroup = null;
			_SortedData = null;
			_rowCount = rowCount;
			_Rows = null;
			_Parent = p;
			_FirstRow = -1;
			_LastRow = -1;
		}

		internal Hashtable HashData
		{
			get { return  _HashData; }
		}

		internal SortedList SortedData
		{
			get 
			{
				if (_SortedData == null && _HashData != null)
				{
					if (_HashData.Count > 0)
						_SortedData = new SortedList(_HashData);	// TODO provide comparer
					_HashData = null;		// we only keep one
				}

				return  _SortedData; 
			}
		}

		internal MatrixEntry Parent
		{
			get { return  _Parent; }
		}

		internal ColumnGrouping ColumnGroup
		{
			get { return  _ColumnGroup; }
			set {  _ColumnGroup = value; }
		}

		internal RowGrouping RowGroup
		{
			get { return  _RowGroup; }
			set {  _RowGroup = value; }
		}

		internal int FirstRow
		{
			get { return  _FirstRow; }
			set 
			{
				if (_FirstRow == -1)
					_FirstRow = value; 
			}
		}

		internal int LastRow
		{
			get { return  _LastRow; }
			set 
			{
				if (value >= _LastRow)
					_LastRow = value; 
			}
		}

		internal BitArray Rows
		{
			get 
			{
				if (_Rows == null)
					_Rows = new BitArray(_rowCount);

				return  _Rows; 
			}
			set {  _Rows = value; }
		}
	}
}
