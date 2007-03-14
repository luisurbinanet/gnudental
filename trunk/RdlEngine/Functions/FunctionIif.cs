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
	/// <p>iif function of the form iif(boolean, expr1, expr2)
	/// <p>
	///	
	/// </summary>
	[Serializable]
	internal class FunctionIif : IExpr
	{
		IExpr _If;		// boolean expression
		IExpr _IfTrue;		// result if true
		IExpr _IfFalse;		// result if false

		/// <summary>
		/// Do division on double data types
		/// </summary>
		public FunctionIif(IExpr ife, IExpr ifTrue, IExpr ifFalse) 
		{
			_If = ife;
			_IfTrue = ifTrue;
			_IfFalse = ifFalse;
		}

		public TypeCode GetTypeCode()
		{
			return _IfTrue.GetTypeCode();
		}

		public bool IsConstant()
		{
			return _If.IsConstant() && _IfTrue.IsConstant() && _IfFalse.IsConstant();
		}

		public IExpr ConstantOptimization()
		{
			_If = _If.ConstantOptimization();
			_IfTrue = _IfTrue.ConstantOptimization();
			_IfFalse = _IfFalse.ConstantOptimization();

			if (_If.IsConstant())
			{
				bool result = _If.EvaluateBoolean(null);
				return result? _IfTrue: _IfFalse;
			}

			return this;
		}

		// Evaluate is for interpretation  (and is relatively slow)
		public object Evaluate(Row row)
		{
			bool result = _If.EvaluateBoolean(row);
			if (result)
				return _IfTrue.Evaluate(row);

			object o = _IfFalse.Evaluate(row);
			// We may need to convert IfFalse to same type as IfTrue
			if (_IfTrue.GetTypeCode() == _IfFalse.GetTypeCode())
				return o;

			return Convert.ChangeType(o, _IfTrue.GetTypeCode());
		}

		public bool EvaluateBoolean(Row row)
		{
			object result = Evaluate(row);
			return Convert.ToBoolean(result);
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
	}
}
