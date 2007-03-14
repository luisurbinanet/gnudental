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
	/// <p>Division operator  of form lhs / rhs
	/// <p>
	///	
	/// </summary>
	[Serializable]
	internal class FunctionMultDecimal : FunctionBinary, IExpr
	{

		/// <summary>
		/// Do division on double data types
		/// </summary>
		public FunctionMultDecimal() 
		{
			_lhs = null;
			_rhs = null;
		}

		public FunctionMultDecimal(IExpr lhs, IExpr rhs) 
		{
			_lhs = lhs;
			_rhs = rhs;
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Decimal;
		}

		public IExpr ConstantOptimization()
		{
			_lhs = _lhs.ConstantOptimization();
			_rhs = _rhs.ConstantOptimization();
			bool bLeftConst = _lhs.IsConstant();
			bool bRightConst = _rhs.IsConstant();
			if (bLeftConst && bRightConst)
			{
				decimal d = EvaluateDecimal(null);
				return new ConstantDecimal(d);
			}
			else if (bLeftConst)
			{
				decimal d = _lhs.EvaluateDecimal(null);
				if (d == 1m)
					return _rhs;
				else if (d == 0m)
					return new ConstantDecimal(0m);
			}
			else if (bRightConst)
			{
				decimal d = _rhs.EvaluateDecimal(null);
				if (d == 1m)
					return _lhs;
				else if (d == 0m)
					return new ConstantDecimal(0m);
			}

			return this;
		}

		// Evaluate is for interpretation  (and is relatively slow)
		public object Evaluate(Row row)
		{
			return EvaluateDecimal(row);
		}
		
		public double EvaluateDouble(Row row)
		{
			decimal result = EvaluateDecimal(row);

			return Convert.ToDouble(result);
		}
		
		public decimal EvaluateDecimal(Row row)
		{
			decimal lhs = _lhs.EvaluateDecimal(row);
			decimal rhs = _rhs.EvaluateDecimal(row);

			return (decimal) (lhs*rhs);
		}

		public string EvaluateString(Row row)
		{
			decimal result = EvaluateDecimal(row);
			return result.ToString();
		}

		public DateTime EvaluateDateTime(Row row)
		{
			decimal result = EvaluateDecimal(row);
			return Convert.ToDateTime(result);
		}

		public bool EvaluateBoolean(Row row)
		{
			decimal result = EvaluateDecimal(row);
			return Convert.ToBoolean(result);
		}
	}
}
