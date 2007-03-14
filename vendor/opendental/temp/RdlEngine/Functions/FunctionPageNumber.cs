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

using fyiReporting.RDL;


namespace fyiReporting.RDL
{
	/// <summary>
	/// <p>Page number operator.   Relies on render to set the proper page #.
	///	
	/// </summary>
	[Serializable]
	internal class FunctionPageNumber : IExpr
	{
		int _PageNumber;		// this is set in pageHeader & pageFooter
		/// <summary>
		/// Current page number
		/// </summary>
		public FunctionPageNumber() 
		{
			_PageNumber = 1;
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Double;
		}

		public bool IsConstant()
		{
			return false;
		}

		public IExpr ConstantOptimization()
		{	// not a constant expression
			return this;
		}

		// Evaluate is for interpretation  
		public object Evaluate(Row row)
		{
			return EvaluateDouble(row);
		}
		
		public double EvaluateDouble(Row row)
		{	
			return _PageNumber;
		}
		
		public decimal EvaluateDecimal(Row row)
		{
			double result = EvaluateDouble(row);

			return Convert.ToDecimal(result);
		}

		public string EvaluateString(Row row)
		{
			double result = EvaluateDouble(row);
			return result.ToString();
		}

		public DateTime EvaluateDateTime(Row row)
		{
			double result = EvaluateDouble(row);
			return Convert.ToDateTime(result);
		}

		public bool EvaluateBoolean(Row row)
		{
			return false;
		}

		public int RuntimePageNumber
		{
			get { return _PageNumber; }
			set { _PageNumber = value; }
		}
	}
}
