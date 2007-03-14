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
	/// <p>Aggregate function: RunningValue min
	/// <p>
	///	
	/// </summary>
	[Serializable]
	internal class FunctionAggrRvMin : FunctionAggr, IExpr, ICacheData
	{
		private TypeCode _tc;		// type of result: decimal or double
		private object _value;		// when scope is dataset we can cache the result
		/// <summary>
		/// Aggregate function: Running Max returns the highest value to date within a group
		///	Return type is same as input expression	
		/// </summary>
		public FunctionAggrRvMin(ArrayList dataCache, IExpr e, object scp):base(e, scp) 
		{
			_value = null;

			// Determine the result
			_tc = e.GetTypeCode();
			dataCache.Add(this);
		}

		public TypeCode GetTypeCode()
		{
			return _tc;
		}

		public object Evaluate(Row row)
		{
			bool bSave=true;
			IEnumerable re = this.GetDataScope(row, out bSave);
			if (re == null)
				return null;

			Row startrow=null;
			foreach (Row r in re)
			{
				startrow = r;			// We just want the first row
				break;
			}

			object current_value = _Expr.Evaluate(row);
			if (row == startrow)
			{
				_value = current_value;
			}
			else
			{
				if (current_value == null)
					{}		// nothing to do in this case
				else if (_value == null)
					_value = current_value;
				else if (Filter.ApplyCompare(_tc, _value, current_value) > 0)
					_value = current_value;
			}
			return _value;
		}
		
		public double EvaluateDouble(Row row)
		{
			object result = Evaluate(row);
			return Convert.ToDouble(result);
		}
		
		public decimal EvaluateDecimal(Row row)
		{
			object result = Evaluate(row);
			return Convert.ToDecimal(result);
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
