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
	/// <p>Plus operator  of form lhs + rhs
	/// <p>
	///	
	/// </summary>
	[Serializable]
	internal class FunctionPlusString : FunctionBinary, IExpr
	{

		/// <summary>
		/// Do division on double data types
		/// </summary>
		public FunctionPlusString(IExpr lhs, IExpr rhs) 
		{
			_lhs = lhs;
			_rhs = rhs;
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.String;
		}

		// Evaluate is for interpretation  (and is relatively slow)
		public object Evaluate(Row row)
		{
			return EvaluateString(row);
		}
	
		public IExpr ConstantOptimization()
		{
			_lhs = _lhs.ConstantOptimization();
			_rhs = _rhs.ConstantOptimization();
			if (_lhs.IsConstant() && _rhs.IsConstant())
			{
				string s = EvaluateString(null);
				return new ConstantString(s);
			}

			return this;
		}
	
		public double EvaluateDouble(Row row)
		{
			string result = EvaluateString(row);

			return Convert.ToDouble(result);
		}
		
		public decimal EvaluateDecimal(Row row)
		{
			string result = EvaluateString(row);
			return Convert.ToDecimal(result);
		}

		public string EvaluateString(Row row)
		{
			string lhs = _lhs.EvaluateString(row);
			string rhs = _rhs.EvaluateString(row);

			if (lhs != null && rhs != null)
				return lhs + rhs;
			else
				return null;
		}

		public DateTime EvaluateDateTime(Row row)
		{
			string result = EvaluateString(row);
			return Convert.ToDateTime(result);
		}

		public bool EvaluateBoolean(Row row)
		{
			string result = EvaluateString(row);
			return Convert.ToBoolean(result);
		}
	}
}
