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
	/// <p>Division operator  of form lhs ^ rhs
	/// <p>
	///	
	/// </summary>
	[Serializable]
	internal class FunctionExp : FunctionBinary, IExpr
	{

		/// <summary>
		/// Do exponentiation on double data types
		/// </summary>
		public FunctionExp() 
		{
		}

		public FunctionExp(IExpr lhs, IExpr rhs) 
		{
			_lhs = lhs;
			_rhs = rhs;
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Double;
		}

		public IExpr ConstantOptimization()
		{
			_lhs = _lhs.ConstantOptimization();
			_rhs = _rhs.ConstantOptimization();
			bool bLeftConst = _lhs.IsConstant();
			bool bRightConst = _rhs.IsConstant();
			if (bLeftConst && bRightConst)
			{
				double d = EvaluateDouble(null);
				return new ConstantDouble(d);
			}
			else if (bRightConst)
			{
				double d = _rhs.EvaluateDouble(null);
				if (d == 1)
					return _lhs;
			}
			else if (bLeftConst)
			{
				double d = _lhs.EvaluateDouble(null);
				if (d == 0)
					return new ConstantDouble(0);
			}

			return this;
		}

		// Evaluate is for interpretation  (and is relatively slow)
		public object Evaluate(Row row)
		{
			return EvaluateDouble(row);
		}
		
		public double EvaluateDouble(Row row)
		{
			double lhs = _lhs.EvaluateDouble(row);
			double rhs = _rhs.EvaluateDouble(row);

			return Math.Pow(lhs,rhs);
		}
		
		public decimal EvaluateDecimal(Row row)
		{
			double result = EvaluateDouble(row);

			return Convert.ToDecimal(result);
		}

		public string EvaluateString(Row row)
		{
			double result = EvaluateDouble(row);
			return result.ToString();
		}

		public DateTime EvaluateDateTime(Row row)
		{
			double result = EvaluateDouble(row);
			return Convert.ToDateTime(result);
		}

		public bool EvaluateBoolean(Row row)
		{
			double result = EvaluateDouble(row);
			return Convert.ToBoolean(result);
		}
	}
}
