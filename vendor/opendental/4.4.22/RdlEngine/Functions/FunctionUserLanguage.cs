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
using System.IO;
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
	internal class FunctionUserLanguage : IExpr
	{
		/// <summary>
		/// Client user language
		/// </summary>
		public FunctionUserLanguage() 
		{
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.String;
		}

		public bool IsConstant()
		{
			return false;
		}

		public IExpr ConstantOptimization()
		{	
			return this;
		}

		// Evaluate is for interpretation  
		public object Evaluate(Row row)
		{
			return EvaluateString(row);
		}
		
		public double EvaluateDouble(Row row)
		{	
			throw new Exception("Invalid conversion from Language to double.");
		}
		
		public decimal EvaluateDecimal(Row row)
		{
			throw new Exception("Invalid conversion from Language to Decimal.");
		}

		public string EvaluateString(Row row)
		{
			return CultureInfo.CurrentCulture.ThreeLetterISOLanguageName;
		}

		public DateTime EvaluateDateTime(Row row)
		{
			throw new Exception("Invalid conversion from Language to DateTime.");
		}

		public bool EvaluateBoolean(Row row)
		{
			throw new Exception("Invalid conversion from Language to boolean.");
		}
	}
}
