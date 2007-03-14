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
	/// <p>Unary minus operator
	/// <p>
	///	
	/// </summary>
	[Serializable]
	internal class FunctionFieldIsMissing : FunctionField
	{
		/// <summary>
		/// obtain value of Field
		/// </summary>
		public FunctionFieldIsMissing(Field fld) : base(fld)
		{
		}
		public FunctionFieldIsMissing(string method) : base(method)
		{
		}

		public override TypeCode GetTypeCode()
		{
			return TypeCode.Boolean;
		}

		public override bool IsConstant()
		{
			return false;
		}

		public override IExpr ConstantOptimization()
		{	
			return this;	// not a constant
		}

		// 
		public override object Evaluate(Row row)
		{
			return EvaluateBoolean(row);
		}
		
		public override double EvaluateDouble(Row row)
		{
			return EvaluateBoolean(row)? 1: 0;
		}
		
		public override decimal EvaluateDecimal(Row row)
		{
			return EvaluateBoolean(row)? 1m: 0m;
		}

		public override string EvaluateString(Row row)
		{
			return EvaluateBoolean(row)? "True": "False";
		}

		public override DateTime EvaluateDateTime(Row row)
		{
			return DateTime.MinValue;
		}

		public override bool EvaluateBoolean(Row row)
		{
			object o = base.Evaluate(row);
			return o == null? true: false;
		}
	}
}
