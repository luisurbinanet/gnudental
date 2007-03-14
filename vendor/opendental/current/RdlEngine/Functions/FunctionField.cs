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
using System.Globalization;

using fyiReporting.RDL;


namespace fyiReporting.RDL
{
	/// <summary>
	/// <p>Unary minus operator
	/// <p>
	///	
	/// </summary>
	[Serializable]
	internal class FunctionField : IExpr
	{
		protected Field f;	
		private string _Name;		// when we have an unresolved field;

		/// <summary>
		/// obtain value of Field
		/// </summary>
		public FunctionField(Field fld) 
		{
			f = fld;
		}

		public FunctionField(string name) 
		{
			_Name = name;
		}

		public string Name
		{
			get {return _Name;}
		}

		public virtual TypeCode GetTypeCode()
		{
			return f.RunType;
		}

		public virtual Field Fld
		{
			get { return f; }
			set { f = value; }
		}

		public virtual bool IsConstant()
		{
			return false;
		}

		public virtual IExpr ConstantOptimization()
		{	
			if (f.Value != null)
				return 	f.Value.ConstantOptimization();

			return this;	// not a constant
		}

		// 
		public virtual object Evaluate(Row row)
		{
			if (row == null)
				return null;
			object o;
			if (f.Value != null)
				o = f.Value.Evaluate(row);
			else
				o = row.Data[f.ColumnNumber];

			if (o == DBNull.Value)
				return null;

			if (f.RunType == TypeCode.String && o is char)	// work around; mono odbc driver confuses string and char
				o = Convert.ChangeType(o, TypeCode.String);
			
			return o;
		}
		
		public virtual double EvaluateDouble(Row row)
		{
			if (row == null)
				return Double.NaN;
			return Convert.ToDouble(Evaluate(row), NumberFormatInfo.InvariantInfo);
		}
		
		public virtual decimal EvaluateDecimal(Row row)
		{
			if (row == null)
				return decimal.MinValue;
			return Convert.ToDecimal(Evaluate(row), NumberFormatInfo.InvariantInfo);
		}

		public virtual string EvaluateString(Row row)
		{
			if (row == null)
				return null;
			return Convert.ToString(Evaluate(row));
		}

		public virtual DateTime EvaluateDateTime(Row row)
		{
			if (row == null)
				return DateTime.MinValue;
			return Convert.ToDateTime(Evaluate(row));
		}

		public virtual bool EvaluateBoolean(Row row)
		{
			if (row == null)
				return false;
			return Convert.ToBoolean(Evaluate(row));
		}
	}
}
