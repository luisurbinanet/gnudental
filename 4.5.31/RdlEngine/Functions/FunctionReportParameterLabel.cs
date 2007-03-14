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
	internal class FunctionReportParameterLabel : FunctionReportParameter
	{
		/// <summary>
		/// obtain value of ReportParameter
		/// </summary>
		public FunctionReportParameterLabel(ReportParameter parm): base(parm) 
		{
		}

		public override TypeCode GetTypeCode()
		{
			return TypeCode.String;
		}

		public override bool IsConstant()
		{
			return false;
		}

		public override IExpr ConstantOptimization()
		{	// not a constant expression
			return this;
		}

		// Evaluate is for interpretation  (and is relatively slow)
		public override object Evaluate(Row row)
		{
			string v = base.EvaluateString(row);

			if (p.ValidValues == null)
				return v;

			string[] displayValues = p.ValidValues.DisplayValues();
			object[] dataValues = p.ValidValues.DataValues();

			for (int i=0; i < dataValues.Length; i++)
			{
				if (dataValues[i].ToString() == v)
					return displayValues[i];
			}

			return v;
		}
		
		public override double EvaluateDouble(Row row)
		{	
			string r = EvaluateString(row);

			return r == null? double.MinValue: Convert.ToDouble(r);
		}
		
		public override decimal EvaluateDecimal(Row row)
		{
			string r = EvaluateString(row);

			return r == null? decimal.MinValue: Convert.ToDecimal(r);
		}

		public override string EvaluateString(Row row)
		{
			return (string) Evaluate(row);
		}

		public override DateTime EvaluateDateTime(Row row)
		{
			string r = EvaluateString(row);

			return r == null? DateTime.MinValue: Convert.ToDateTime(r);
		}

		public override bool EvaluateBoolean(Row row)
		{
			string r = EvaluateString(row);

			return r.ToLower() == "true"? true: false;
		}
	}
}
