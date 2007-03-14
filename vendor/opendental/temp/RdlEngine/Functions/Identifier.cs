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
	/// <p>Identifier
	/// <p>
	///	
	/// </summary>
	[Serializable]
	internal class Identifier : IExpr
	{
		string _Value;		// value of the identifier

		/// <summary>
		/// passed class name, function name, and args for evaluation
		/// </summary>
		public Identifier(string v) 
		{
			_Value = v;
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Object;			// TODO
		}

		public bool IsConstant()
		{
			return false;
		}

		public IExpr ConstantOptimization()
		{	
			return this;
		}

		public object Evaluate(Row row)
		{	// TODO   -- not sure what yet
			return _Value;
		}

		public double EvaluateDouble(Row row)
		{
			return Convert.ToDouble(Evaluate(row));
		}
		
		public decimal EvaluateDecimal(Row row)
		{
			return Convert.ToDecimal(Evaluate(row));
		}

		public string EvaluateString(Row row)
		{
			return Convert.ToString(Evaluate(row));
		}

		public DateTime EvaluateDateTime(Row row)
		{
			return Convert.ToDateTime(Evaluate(row));
		}

		public bool EvaluateBoolean(Row row)
		{
			return Convert.ToBoolean(Evaluate(row));
		}
	}
}
