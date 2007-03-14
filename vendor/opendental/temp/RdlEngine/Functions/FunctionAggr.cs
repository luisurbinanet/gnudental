/* ====================================================================
    Copyright (C) 2004-2005  fyiReporting Software, LLC

    This file is part of the fyiReporting RDL project.
	
    The RDL project is free software; you can redistribute it and/or modify
    it under the terms of the GNU General public License as published by
    the Free Software Foundation; either version 2 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General public License for more details.

    You should have received a copy of the GNU General public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

    For additional information, email info@fyireporting.com or visit
    the website www.fyiReporting.com.
*/
using System;
using System.Collections;
using System.IO;
using System.Reflection;

using fyiReporting.RDL;


namespace fyiReporting.RDL
{
	/// <summary>
	/// <p>Base class for all aggregate functions
	/// <p>
	///	
	/// </summary>
	[Serializable]
	internal abstract class FunctionAggr 
	{
		public IExpr _Expr;			// aggregate expression
		public object _Scope;		// DataSet or Grouping or DataRegion that contains (directly or
										//  indirectly) the report item that the aggregate
										//  function is used in
										// Can also hold the Matrix object
		bool _LevelCheck;				// row processing requires level check
										//   i.e. simple specified on recursive row check
		/// <summary>
		/// Base class of all aggregate functions
		/// </summary>

		public FunctionAggr(IExpr e, object scp) 
		{
			_Expr = e;
			_Scope = scp;
			_LevelCheck = false;
		}

		public bool IsConstant()
		{
			return false;
		}

		public IExpr ConstantOptimization()
		{
			if (_Expr != null)
				_Expr = _Expr.ConstantOptimization();
			return (IExpr) this;
		}

		public bool EvaluateBoolean(Row row)
		{
			return false;
		}

		public IExpr Expr
		{
			get { return  _Expr; }
		}

		public object Scope
		{
			get { return  _Scope; }
		}

		public bool LevelCheck
		{
			get { return  _LevelCheck; }
			set { _LevelCheck = value; }
		}

		// return an IEnumerable that represents the scope of the data
		protected RowEnumerable GetDataScope(Row row, out bool bSave)
			{
			bSave=true;
			RowEnumerable re=null;

			if (this._Scope != null)
			{
				Type t = this._Scope.GetType();
				if (t == typeof(Grouping))
				{
					if (row == null)
						return null;
					bSave=false;
					Grouping g = (Grouping) (this._Scope);
					GroupEntry ge = row.R.CurrentGroups[g.Index];
					re = new RowEnumerable (ge.StartRow, ge.EndRow, row.R.Data, _LevelCheck);
				}
				else if (t == typeof(Matrix))
				{
					bSave=false;
					Matrix m = (Matrix) (this._Scope);
					re = new RowEnumerable(0, m.Data.Data.Count-1, m.Data.Data, false);
				}
				else if (row != null)
				{
					re = new RowEnumerable (0, row.R.Data.Count-1, row.R.Data, false);
				}
				else
				{
					DataSet ds = this._Scope as DataSet;
					if (ds != null && ds.Query != null && ds.Query.Data != null)
					{
						Rows rows = ds.Query.Data;
						re = new RowEnumerable(0, rows.Data.Count-1, rows.Data, false);
					}
				}
//				else if (t == typeof(DataRegion))
//				{
//				}
			}
			else if (row != null)
			{
				re = new RowEnumerable (0, row.R.Data.Count-1, row.R.Data, false);
			}

			return re;
		}
	}

	internal class RowEnumerable : IEnumerable
	{
		int startRow;
		int endRow;
		ArrayList data;
		bool _LevelCheck;
		public RowEnumerable (int start, int end, ArrayList d, bool levelCheck)
		{
			startRow = start;
			endRow = end;
			data = d;
			_LevelCheck = levelCheck;
		}

		public ArrayList Data
		{
			get{return data;}
		}

		public int FirstRow
		{
			get{return startRow;}
		}

		public int LastRow
		{
			get{return endRow;}
		}

		public bool LevelCheck
		{
			get{return _LevelCheck;}
		}

		// Methods
		public IEnumerator GetEnumerator()
		{
			return new RowEnumerator(this);
		}
	}

	internal class RowEnumerator : IEnumerator
	{
		private RowEnumerable re;
		private int index = -1;
		public RowEnumerator(RowEnumerable rea)
		{
			re = rea;
		}
		//Methods
		public bool MoveNext()
		{
			index++;
			while (true)
			{
				if (index + re.FirstRow > re.LastRow)
					return false;
				else
				{
					if (re.LevelCheck)
					{	//
						Row r1 = re.Data[re.FirstRow] as Row;
						Row r2 = re.Data[index + re.FirstRow] as Row;
						if (r1.Level == r1.Level)
							return true;
						index++;
					}
					else
						return true;
				}
			}
		}

		public void Reset()
		{
			index=-1;
		}

		public object Current
		{
			get{return(re.Data[index + re.FirstRow]);}
		}
	}
}
