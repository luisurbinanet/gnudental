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
	/// <p>Binary operator
	/// <p>
	///	
	/// </summary>
	[Serializable]
	internal abstract class FunctionBinary
	{
		public IExpr _lhs;			// lhs 
		public IExpr _rhs;			// rhs

		/// <summary>
		/// Arbitrary binary operater; might be a
		/// </summary>
		public FunctionBinary() 
		{
			_lhs = null;
			_rhs = null;
		}

		public FunctionBinary(IExpr l, IExpr r) 
		{
			_lhs = l;
			_rhs = r;
		}

		public bool IsConstant()
		{
			if (_lhs.IsConstant())
				return _rhs.IsConstant();

			return false;
		}

//		virtual public bool EvaluateBoolean(Row row)
//		{
//			return false;
//		}

		public IExpr Lhs
		{
			get { return  _lhs; }
			set {  _lhs = value; }
		}

		public IExpr Rhs
		{
			get { return  _rhs; }
			set {  _rhs = value; }
		}
	}
}
