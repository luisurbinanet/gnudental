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
	/// <p>Aggregate function: Varp
	/// <p>
	///	
	/// </summary>
	[Serializable]
	internal class FunctionAggrVarp : FunctionAggr, IExpr, ICacheData
	{
		private object _value;		// when scope is dataset we can cache the result
		/// <summary>
		/// Aggregate function: Varp = (n sum(square(x)) - square((sum(x))) / n*n
		/// Stdev assumes values are a sample of the population of data.  If the data
		/// is the entire representation then use Stdevp.
		/// 
		///	Return type is decimal for decimal expressions and double for all
		///	other expressions.	
		/// </summary>
		public FunctionAggrVarp(ArrayList dataCache, IExpr e, object scp):base(e, scp) 
		{
			_value = null;
			dataCache.Add(this);
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Double;
		}

		// Evaluate is for interpretation  (and is relatively slow)
		public object Evaluate(Row row)
		{
			return (object) EvaluateDouble(row);
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
				double sum2=0;
				int count=0;
				double temp;
				foreach (Row r in re)
				{
					temp = _Expr.EvaluateDouble(r);
					if (temp.CompareTo(double.NaN) != 0)
					{
						sum += temp;
						sum2 += (temp*temp);
						count++;
					}
				}

				double result;
				if (count > 0)
					result = ((count * sum2 - sum*sum) / (count * count));
				else
					result = double.NaN;
				if (bSave)
					_value = result;
				else
					return result;
			}
			return (double) _value;
		}
		
		public decimal EvaluateDecimal(Row row)
		{
			double d = EvaluateDouble(row);
			if (d.CompareTo(double.NaN) == 0)
				return decimal.MinValue;

			return Convert.ToDecimal(d);
		}

		public string EvaluateString(Row row)
		{
			double result = EvaluateDouble(row);
			if (result.CompareTo(double.NaN) == 0)
				return null;
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
			// TODO:  Add FunctionAggrVarp.ClearCache implementation
		}

		#endregion
	}
}
