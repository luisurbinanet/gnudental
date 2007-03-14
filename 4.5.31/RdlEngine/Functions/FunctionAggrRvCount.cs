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
	/// <p>Aggregate function: RunningValue count
	/// <p>
	///	
	/// </summary>
	[Serializable]
	internal class FunctionAggrRvCount : FunctionAggr, IExpr
	{
		private TypeCode _tc;		// type of result: decimal or double
		private int _count;			//
		/// <summary>
		/// Aggregate function: RunningValue Sum returns the sum of all values of the
		///		expression within the scope up to that row
		///	Return type is decimal for decimal expressions and double for all
		///	other expressions.	
		/// </summary>
		public FunctionAggrRvCount(IExpr e, object scp):base(e, scp) 
		{
			_count = -1;

			_tc = TypeCode.Double;		// force result to double
		}

		public TypeCode GetTypeCode()
		{
			return _tc;
		}

		// Evaluate is for interpretation  (and is relatively slow)
		public object Evaluate(Row row)
		{
			return _tc==TypeCode.Decimal? (object) EvaluateDecimal(row): (object) EvaluateDouble(row);
		}
		
		public double EvaluateDouble(Row row)
		{
			bool bSave=true;
			IEnumerable re = this.GetDataScope(row, out bSave);
			if (re == null)
				return double.NaN;

			Row startrow=null;
			foreach (Row r in re)
			{
				startrow = r;			// We just want the first row
				break;
			}

			object currentValue = _Expr.Evaluate(row);
			int incr = currentValue == null? 0: 1;
			if (row == startrow)
			{
				// must be the start of a new group
				_count = incr;
			}
			else
			{
				_count += incr;
			}

			return (double) _count;
		}
		
		public decimal EvaluateDecimal(Row row)
		{
			return (decimal) EvaluateDouble(row);
		}

		public string EvaluateString(Row row)
		{
			object result = Evaluate(row);
			return Convert.ToString(result);
		}

		public DateTime EvaluateDateTime(Row row)
		{
			object result = Evaluate(row);
			return Convert.ToDateTime(result);
		}
	}
}
