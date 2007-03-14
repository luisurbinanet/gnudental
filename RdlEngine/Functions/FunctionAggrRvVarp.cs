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
	/// <p>Aggregate function: RunningValue varp
	/// <p>
	///	
	/// </summary>
	[Serializable]
	internal class FunctionAggrRvVarp : FunctionAggr, IExpr
	{
		private double _sum;		// 
		private double _sum2;		// 
		private int _count;			//
		/// <summary>
		/// Aggregate function: RunningValue var returns the variance of all values of the
		///		expression within the scope up to that row
		///	Return type is double for all expressions.	
		/// </summary>
		public FunctionAggrRvVarp(IExpr e, object scp):base(e, scp) 
		{
			_sum = _sum2 = 0;
			_count = -1;
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Double;
		}

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

			Row startrow=null;
			foreach (Row r in re)
			{
				startrow = r;			// We just want the first row
				break;
			}

			double currentValue = _Expr.EvaluateDouble(row);
			if (row == startrow)
			{
				// restart the group
				_sum = _sum2 = 0;
				_count = 0;
			}
			
			if (currentValue.CompareTo(double.NaN) != 0)
			{
				_sum += currentValue;
				_sum2 += (currentValue*currentValue);
				_count++;
			}

			double result;
			if (_count > 0)
				result = (_count * _sum2 - _sum*_sum) / (_count * _count);
			else		
				result = double.NaN;

			return result;
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
