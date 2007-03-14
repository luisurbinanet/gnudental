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
	/// <p>OR operator  of form lhs || rhs
	/// <p>
	///	
	/// </summary>
	[Serializable]
	internal class FunctionOr : FunctionBinary, IExpr
	{

		/// <summary>
		/// Or two boolean expressions together of the form a || b
		/// </summary>
		public FunctionOr(IExpr lhs, IExpr rhs) 
		{
			_lhs = lhs;
			_rhs = rhs;
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Boolean;
		}

		public IExpr ConstantOptimization()
		{
			_lhs = _lhs.ConstantOptimization();
			_rhs = _rhs.ConstantOptimization();
			bool bLeftConst = _lhs.IsConstant();
			bool bRightConst = _rhs.IsConstant();
			if (bLeftConst && bRightConst)
			{
				bool b = EvaluateBoolean(null);
				return new ConstantBoolean(b);
			}
			else if (bRightConst)
			{
				bool b = _rhs.EvaluateBoolean(null);
				if (b)
					return new ConstantBoolean(true);
				else 
					return _lhs;
			}
			else if (bLeftConst)
			{
				bool b = _lhs.EvaluateBoolean(null);
				if (b)
					return new ConstantBoolean(true);
				else
					return _rhs;
			}

			return this;
		}

		// Evaluate is for interpretation  (and is relatively slow)
		public object Evaluate(Row row)
		{
			return EvaluateBoolean(row);
		}
		
		public double EvaluateDouble(Row row)
		{
			return Double.NaN;
		}
		
		public decimal EvaluateDecimal(Row row)
		{
			return decimal.MinValue;
		}

		public string EvaluateString(Row row)
		{
			bool result = EvaluateBoolean(row);
			return result.ToString();
		}

		public DateTime EvaluateDateTime(Row row)
		{
			return DateTime.MinValue;
		}

		public bool EvaluateBoolean(Row row)
		{
			bool r = _lhs.EvaluateBoolean(row);
			if (r)
				return true;
			return  _rhs.EvaluateBoolean(row);
		}
	}
}
