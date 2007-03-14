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
	/// <p>Expression definition
	/// <p>
	///	
	/// </summary>
	[Serializable]
	internal class FunctionCustomInstance : IExpr
	{
		string _Cls;		// class name
		string _Func;		// function/operator
		IExpr[] _Args;		// arguments 
		ReportClass _Rc;	// ReportClass
		TypeCode _ReturnTypeCode;	// the return type

		/// <summary>
		/// passed ReportClass, function name, and args for evaluation
		/// </summary>
		public FunctionCustomInstance(ReportClass rc, string f, IExpr[] a, TypeCode type) 
		{
			_Cls = null;
			_Func = f;
			_Args = a;
			_Rc = rc;
			_ReturnTypeCode = type;
		}

		public TypeCode GetTypeCode()
		{
			return _ReturnTypeCode;
		}

		public bool IsConstant()
		{
			return false;		// Can't know what the function does
		}

		public IExpr ConstantOptimization()
		{
			// Do constant optimization on all the arguments
			for (int i=0; i < _Args.GetLength(0); i++)
			{
				IExpr e = (IExpr)_Args[i];
				_Args[i] = e.ConstantOptimization();
			}

			// Can't assume that the function doesn't vary
			//   based on something other than the args e.g. Now()
			return this;
		}

		// Evaluate is for interpretation  (and is relatively slow)
		public object Evaluate(Row row)
		{
			// get the results
			object[] argResults = new object[_Args.Length];
			int i=0;
			foreach(IExpr a  in _Args)
			{
				argResults[i++] = a.Evaluate(row);
			}
			Type[] argTypes = Type.GetTypeArray(argResults);

			// We can definitely optimize this by caching some info TODO

			// Get ready to call the function
			Object returnVal;

			Type theClassType=_Rc.Instance.GetType();
			MethodInfo mInfo = theClassType.GetMethod(_Func, argTypes);
			returnVal = mInfo.Invoke(_Rc.Instance, argResults);

			return returnVal;
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

		public string Cls
		{
			get { return  _Cls; }
			set {  _Cls = value; }
		}

		public string Func
		{
			get { return  _Func; }
			set {  _Func = value; }
		}

		public IExpr[] Args
		{
			get { return  _Args; }
			set {  _Args = value; }
		}
	}

#if DEBUG
	internal class TestFunction		// for testing CodeModules, Classes, and the Function class
	{
		int counter=0;
		public TestFunction()		
		{
			counter=0;
		}

		public int count()
		{
			return counter++;
		}
		
		public int count(string s)
		{
			counter++;
			return Convert.ToInt32(s) + counter; 
		}

		static public double sqrt(double x)
		{
			return Math.Sqrt(x);
		}
	}
#endif

}
