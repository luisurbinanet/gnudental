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
	internal class FunctionReportParameter : IExpr
	{
		protected ReportParameter p;	

		/// <summary>
		/// obtain value of ReportParameter
		/// </summary>
		public FunctionReportParameter(ReportParameter parm) 
		{
			p=parm;
		}

		public virtual TypeCode GetTypeCode()
		{
			return p.dt;
		}

		public virtual bool IsConstant()
		{
			return false;
		}

		public virtual IExpr ConstantOptimization()
		{	// not a constant expression
			return this;
		}

		// Evaluate is for interpretation  (and is relatively slow)
		public virtual object Evaluate(Row row)
		{
			return p.RuntimeValue;
		}
		
		public virtual double EvaluateDouble(Row row)
		{	
			if (p.RuntimeValue == null)
				return Double.NaN;

			switch (p.dt)
			{
				case TypeCode.Double:
					return ((double) p.RuntimeValue);
				case TypeCode.Object:
					return Double.NaN;
				case TypeCode.Int32:
					return (double) ((int) p.RuntimeValue);
				case TypeCode.Boolean:
					return Convert.ToDouble((bool) p.RuntimeValue);
				case TypeCode.String:
					return Convert.ToDouble((string) p.RuntimeValue);
				case TypeCode.DateTime:
					return Convert.ToDouble((DateTime) p.RuntimeValue);
				default:
					return Double.NaN;
			}
		}
		
		public virtual decimal EvaluateDecimal(Row row)
		{
			if (p.RuntimeValue == null)
				return Decimal.MinValue;

			switch (p.dt)
			{
				case TypeCode.Double:
					return Convert.ToDecimal((double) p.RuntimeValue);
				case TypeCode.Object:
					return Decimal.MinValue;
				case TypeCode.Int32:
					return Convert.ToDecimal((int) p.RuntimeValue);
				case TypeCode.Boolean:
					return Convert.ToDecimal((bool) p.RuntimeValue);
				case TypeCode.String:
					return Convert.ToDecimal((string) p.RuntimeValue);
				case TypeCode.DateTime:
					return Convert.ToDecimal((DateTime) p.RuntimeValue);
				default:
					return Decimal.MinValue;
			}
		}

		public virtual string EvaluateString(Row row)
		{
			if (p.RuntimeValue == null)
				return null;

			return p.RuntimeValue.ToString();
//			switch (p.dt)
//			{
//				case TypeCode.Double:
//					return Convert.ToString((double) p.RuntimeValue);
//				case TypeCode.Object:
//					return p.RuntimeValue.ToString();
//				case TypeCode.Int32:
//					return Convert.ToString((int) p.RuntimeValue);
//				case TypeCode.Boolean:
//					return Convert.ToString((bool) p.RuntimeValue);
//				case TypeCode.String:
//					return (string) p.RuntimeValue;
//				case TypeCode.DateTime:
//					return Convert.ToString((DateTime) p.RuntimeValue);
//				default:
//					return null;
//			}
		}

		public virtual DateTime EvaluateDateTime(Row row)
		{
			if (p.RuntimeValue == null)
				return DateTime.MinValue;

			switch (p.dt)
			{
				case TypeCode.Double:
					return Convert.ToDateTime((double) p.RuntimeValue);
				case TypeCode.Object:
					return DateTime.MinValue;
				case TypeCode.Int32:
					return Convert.ToDateTime((int) p.RuntimeValue);
				case TypeCode.Boolean:
					return Convert.ToDateTime((bool) p.RuntimeValue);
				case TypeCode.String:
					return Convert.ToDateTime((string) p.RuntimeValue);
				case TypeCode.DateTime:
					return (DateTime) p.RuntimeValue;
				default:
					return DateTime.MinValue;
			}
		}

		public virtual bool EvaluateBoolean(Row row)
		{
			if (p.RuntimeValue == null)
				return false;

			switch (p.dt)
			{
				case TypeCode.Double:
					return Convert.ToBoolean((double) p.RuntimeValue);
				case TypeCode.Object:
					return false;
				case TypeCode.Int32:
					return Convert.ToBoolean((int) p.RuntimeValue);
				case TypeCode.Boolean:
					return (bool) p.RuntimeValue;
				case TypeCode.String:
					return Convert.ToBoolean((string) p.RuntimeValue);
				case TypeCode.DateTime:
					return Convert.ToBoolean((DateTime) p.RuntimeValue);
				default:
					return false;
			}
		}
	}
}
