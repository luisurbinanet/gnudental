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
	/// <p>Aggregate function: CountRows
	/// <p>
	///	
	/// </summary>
	[Serializable]
	internal class FunctionAggrCountRows : FunctionAggr, IExpr
	{
		/// <summary>
		/// Aggregate function: CountRows
		/// 
		///	Return type is double
		/// </summary>
		public FunctionAggrCountRows(object scp):base(null, scp) 
		{
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Double;
		}

		// Evaluate is for interpretation
		public object Evaluate(Row row)
		{
			return (object) EvaluateDouble(row);
		}
		
		public double EvaluateDouble(Row row)
		{
			bool bSave=true;
			RowEnumerable re = this.GetDataScope(row, out bSave);
			if (re == null)
				return 0;

			int count = re.LastRow - re.FirstRow + 1;

			return count;
		}
		
		public decimal EvaluateDecimal(Row row)
		{
			double d = EvaluateDouble(row);

			return Convert.ToDecimal(d);
		}

		public string EvaluateString(Row row)
		{
			double result = EvaluateDouble(row);
			return Convert.ToString(result);
		}

		public DateTime EvaluateDateTime(Row row)
		{
			object result = Evaluate(row);
			return Convert.ToDateTime(result);
		}
	}
}
