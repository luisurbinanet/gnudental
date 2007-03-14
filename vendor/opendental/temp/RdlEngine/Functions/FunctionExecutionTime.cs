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
	/// <p>DateTime Report started
	/// <p>
	///	
	/// </summary>
	[Serializable]
	internal class FunctionExecutionTime : IExpr
	{
		[NonSerialized] DateTime _StartReport;		// Date time report started
		/// <summary>
		/// DateTime report started
		/// </summary>
		public FunctionExecutionTime() 
		{
			_StartReport = DateTime.MinValue;
		}

		internal DateTime StartReport
		{
			get {return _StartReport;}
			set {_StartReport = value;}
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.DateTime;
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
			return EvaluateDateTime(row);
		}
		
		public double EvaluateDouble(Row row)
		{	
			DateTime result = EvaluateDateTime(row);
			return Convert.ToDouble(result);
		}
		
		public decimal EvaluateDecimal(Row row)
		{
			DateTime result = EvaluateDateTime(row);

			return Convert.ToDecimal(result);
		}

		public string EvaluateString(Row row)
		{
			DateTime result = EvaluateDateTime(row);
			return result.ToString();
		}

		public DateTime EvaluateDateTime(Row row)
		{
			if (_StartReport == DateTime.MinValue)
				_StartReport = DateTime.Now;
			return _StartReport;
		}

		
		public bool EvaluateBoolean(Row row)
		{
			return false;
		}
	}
}
