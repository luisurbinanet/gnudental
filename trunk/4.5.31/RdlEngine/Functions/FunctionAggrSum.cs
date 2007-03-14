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
	/// <p>Aggregate function: sum
	/// <p>
	///	
	/// </summary>
	[Serializable]
	internal class FunctionAggrSum : FunctionAggr, IExpr, ICacheData
	{
		private TypeCode _tc;		// type of result: decimal or double
		private object _value;		// when scope is dataset we can cache the result
		/// <summary>
		/// Aggregate function: Sum returns the sum of all values of the
		///		expression within the scope
		///	Return type is decimal for decimal expressions and double for all
		///	other expressions.	
		/// </summary>
		public FunctionAggrSum(ArrayList dataCache, IExpr e, object scp):base(e, scp) 
		{
			_value = null;

			// Determine the result
			_tc = e.GetTypeCode();
			if (_tc != TypeCode.Decimal)	// if not decimal
				_tc = TypeCode.Double;		// force result to double
			dataCache.Add(this);
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

			if (_value == null)
			{
				double sum=0;
				double temp;
				foreach (Row r in re)
				{
					temp = _Expr.EvaluateDouble(r);
					if (temp.CompareTo(double.NaN) != 0)
						sum += temp;
				}
				if (bSave)
					_value = (object) sum;
				else
					return sum;
			}
			return (double) _value;
		}
		
		public decimal EvaluateDecimal(Row row)
		{
			bool bSave;
			IEnumerable re = this.GetDataScope(row, out bSave);
			if (re == null)
				return decimal.MinValue;

			if (_value == null)
			{
				decimal sum=0;
				decimal temp;
				foreach (Row r in re)
				{
					temp = _Expr.EvaluateDecimal(r);
					if (temp != decimal.MinValue)		// indicate null value
						sum += temp;
				}
				if (bSave)
					_value = (object) sum;
				else
					return sum;
			}
			return (decimal) _value;
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
		#region ICacheData Members

		public void ClearCache()
		{
			_value = null;
		}

		#endregion
	}
}
