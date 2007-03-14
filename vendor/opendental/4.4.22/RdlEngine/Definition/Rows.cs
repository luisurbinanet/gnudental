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
using System.Collections;

namespace fyiReporting.RDL
{
	///<summary>
	/// A collection of rows.
	///</summary>
	[Serializable]
	internal class Rows : IComparer
	{
		ArrayList _Data;	// array of Row object;
		ArrayList _SortBy;	// array of expressions used to sort the data
		GroupEntry[] _CurrentGroups;	// group

		internal Rows()
		{
			_SortBy = null;
			_CurrentGroups = null;
		}

		// Constructor that takes existing Rows; a start, end and bitArray with the rows wanted
		internal Rows(Rows r, int start, int end, BitArray ba)
		{
			_SortBy = null;
			_CurrentGroups = null;
			if (end - start < 0)			// null set?
			{
				_Data = new ArrayList(1);
				_Data.TrimToSize();
				return;
			}
			_Data = new ArrayList(end - start + 1);

			for (int iRow = start; iRow <= end; iRow++)
			{
				if (ba == null || ba.Get(iRow))
				{
					Row or = (Row) (r.Data[iRow]); 
					Row nr = new Row(this, or);
					nr.RowNumber = or.RowNumber;
					_Data.Add(nr);
				}
			}

			_Data.TrimToSize();
		}

		// Constructor that takes existing Rows
		internal Rows(Rows r)
		{
			_SortBy = null;
			_CurrentGroups = null;
			if (r.Data.Count <= 0)			// null set?
			{
				_Data = new ArrayList(1);
				_Data.TrimToSize();
				return;
			}
			_Data = new ArrayList(r.Data.Count);

			for (int iRow = 0; iRow < r.Data.Count; iRow++)
			{
				Row or = (Row) (r.Data[iRow]); 
				Row nr = new Row(this, or);
				nr.RowNumber = or.RowNumber;
				_Data.Add(nr);
			}

			_Data.TrimToSize();
		}

		internal Rows(TableGroups tg, Grouping g, Sorting s)
		{
			_SortBy = new ArrayList();
			// Pull all the sort expression together
			if (tg != null)
			{
				foreach(TableGroup t in tg.Items)
				{
					foreach(GroupExpression ge in t.Grouping.GroupExpressions.Items)
					{
						_SortBy.Add(new RowsSortExpression(ge.Expression));
					}
					// TODO what to do with the sort expressions!!!!
				}
			}
			if (g != null)
			{
				if (g.ParentGroup != null)
					_SortBy.Add(new RowsSortExpression(g.ParentGroup));
				else if (g.GroupExpressions != null)
				{
					foreach (GroupExpression ge in g.GroupExpressions.Items)
					{
						_SortBy.Add(new RowsSortExpression(ge.Expression));
					}
				}
			}
			if (s != null)
			{
				foreach (SortBy sb in s.Items)
				{
					_SortBy.Add(new RowsSortExpression(sb.SortExpression, sb.Direction == SortDirectionEnum.Ascending));
				}
			}
			if (_SortBy.Count > 0)
				_SortBy.TrimToSize();
			else
				_SortBy = null;
		}

		internal void Sort()
		{
			// sort the data array by the data.
			_Data.Sort(this);
		}

		internal ArrayList Data
		{
			get { return  _Data; }
			set 
			{ 
				_Data = value;			// Assign the new value
				foreach(Row r in _Data)	// Updata all rows
				{
					r.R = this;
				}
		
			}
		}

		internal ArrayList SortBy
		{
			get { return  _SortBy; }
			set { _SortBy = value; }
		}

		internal GroupEntry[] CurrentGroups
		{
			get { return  _CurrentGroups; }
			set { _CurrentGroups = value; }
		}

		#region IComparer Members

		public int Compare(object x, object y)
		{
			if (x == y)				// why does the sort routine do this??
				return 0;

			Row r1 = (Row) x;
			Row r2 = (Row) y;

			object o1=null,o2=null;
			TypeCode tc = TypeCode.Object;
			int rc;
			try 
			{
				foreach (RowsSortExpression se in _SortBy)
				{
					o1 = se.expr.Evaluate(r1);
					o2 = se.expr.Evaluate(r2);
					tc = se.expr.GetTypeCode();
					rc = Filter.ApplyCompare(tc, o1, o2);
					if (rc != 0)
						return se.bAscending? rc: -rc;
				}
			}
			catch (Exception e)		// this really shouldn't happen
			{
				Console.WriteLine("Sort rows exception\r\nArguments: {0} {1}\r\nTypecode: {2}\r\n{3}\r\n{4}", o1, o2, tc.ToString(), e.Message, e.StackTrace);
			}
			return r1.RowNumber - r2.RowNumber;		// in case of tie use original row number
		}

		#endregion
	}

	class RowsSortExpression
	{
		internal Expression expr;
		internal bool bAscending;

		internal RowsSortExpression(Expression e, bool asc)
		{
			expr = e;
			bAscending = asc;
		}

		internal RowsSortExpression(Expression e)
		{
			expr = e;
			bAscending = true;
		}
	}
}
