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
	/// <p>Expression definition
	/// <p>
	///	
	/// </summary>
	internal interface IExpr
	{
		TypeCode GetTypeCode();			// return the type of the expression
		bool IsConstant();				// expression returns a constant
		IExpr ConstantOptimization();	// constant optimization

		// Evaluate is for interpretation
		object Evaluate(Row row);				// return an object
		string EvaluateString(Row row);			// return a string
		double EvaluateDouble(Row row);			// return a double
		decimal EvaluateDecimal(Row row);		// return a decimal
		DateTime EvaluateDateTime(Row row);		// return a DateTime
		bool EvaluateBoolean(Row row);			// return boolean
	}
}
