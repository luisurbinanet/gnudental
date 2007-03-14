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
	/// <p>Format function: Format(expr, string expr format)
	/// <p>
	///	
	/// </summary>
	[Serializable]
	internal class FunctionFormat : IExpr
	{
		IExpr _Formatee;	// object to format
		IExpr _Format;		// format string
		/// <summary>
		/// Format an object
		/// </summary>
		public FunctionFormat(IExpr formatee, IExpr format) 
		{
			_Formatee = formatee;
			_Format= format;
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.String;
		}

		// 
		public object Evaluate(Row row)
		{
			return EvaluateString(row);
		}

		public bool IsConstant()
		{
			if (_Formatee.IsConstant())
				return _Format.IsConstant();

			return false;
		}
	
		public IExpr ConstantOptimization()
		{
			_Formatee = _Formatee.ConstantOptimization();
			_Format = _Format.ConstantOptimization();
			if (_Formatee.IsConstant() && _Format.IsConstant())
			{
				string s = EvaluateString(null);
				return new ConstantString(s);
			}

			return this;
		}
		
		public bool EvaluateBoolean(Row row)
		{
			string result = EvaluateString(row);

			return Convert.ToBoolean(result);
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
			object o = _Formatee.Evaluate(row);
			if (o == null)
				return null;
			string format = _Format.EvaluateString(row);
			if (format == null)
				return o.ToString();	// just return string version of object

			string result=null;
			try
			{
				result = String.Format("{0:" + format + "}", o);
			}
			catch 		// invalid format string specified
			{			//    treat as a weak error
				result = o.ToString();
			}
			return result;
		}

		public DateTime EvaluateDateTime(Row row)
		{
			string result = EvaluateString(row);
			return Convert.ToDateTime(result);
		}
	}
}
