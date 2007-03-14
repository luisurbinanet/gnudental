/* ====================================================================
    Copyright (C) 2004-2005  fyiReporting Software, LLC

    This file is part of the fyiReporting RDL project.
	
    The RDL project is free software; you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation; either version 2 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

    For additional information, email info@fyireporting.com or visit
    the website www.fyiReporting.com.
*/

using System;
using System.Xml;
using System.Text;
using System.Drawing;
using System.Globalization;

namespace fyiReporting.RDL
{
	///<summary>
	/// The style of the border colors.  Expressions for all sides as well as default expression.
	///</summary>
	[Serializable]
	internal class StyleBorderColor : ReportLink
	{
		Expression _Default;	// (Color) Color of the border (unless overridden for a specific
								//   side). Default: Black.
		Expression _Left;		// (Color) Color of the left border
		Expression _Right;		// (Color) Color of the right border
		Expression _Top;		// (Color) Color of the top border
		Expression _Bottom;		// (Color) Color of the bottom border
	
		internal StyleBorderColor(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_Default=null;
			_Left=null;
			_Right=null;
			_Top=null;
			_Bottom=null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "Default":
						_Default = new Expression(r, this, xNodeLoop, ExpressionType.Color);
						break;
					case "Left":
						_Left = new Expression(r, this, xNodeLoop, ExpressionType.Color);
						break;
					case "Right":
						_Right = new Expression(r, this, xNodeLoop, ExpressionType.Color);
						break;
					case "Top":
						_Top = new Expression(r, this, xNodeLoop, ExpressionType.Color);
						break;
					case "Bottom":
						_Bottom = new Expression(r, this, xNodeLoop, ExpressionType.Color);
						break;
					default:
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown BorderColor element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
		}

		// Handle parsing of function in final pass
		override internal void FinalPass()
		{
			if (_Default != null)
				_Default.FinalPass();
			if (_Left != null)
				_Left.FinalPass();
			if (_Right != null)
				_Right.FinalPass();
			if (_Top != null)
				_Top.FinalPass();
			if (_Bottom != null)
				_Bottom.FinalPass();
			return;
		}

		// Generate a CSS string from the specified styles
		internal string GetCSS(Row row, bool bDefaults)
		{
			StringBuilder sb = new StringBuilder();

			if (_Default != null)
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "border-color:{0};",_Default.EvaluateString(row));
			else if (bDefaults)
				sb.Append("border-color:black;");

			if (_Left != null)
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "border-left:{0};",_Left.EvaluateString(row));

			if (_Right != null)
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "border-right:{0};",_Right.EvaluateString(row));

			if (_Top != null)
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "border-top:{0};",_Top.EvaluateString(row));

			if (_Bottom != null)
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "border-bottom:{0};",_Bottom.EvaluateString(row));

			return sb.ToString();
		}

		internal bool IsConstant()
		{
			bool rc = true;

			if (_Default != null)
				rc = _Default.IsConstant();

			if (!rc)
				return false;

			if (_Left != null)
				rc = _Left.IsConstant();

			if (!rc)
				return false;

			if (_Right != null)
				rc = _Right.IsConstant();

			if (!rc)
				return false;

			if (_Top != null)
				rc = _Top.IsConstant();

			if (!rc)
				return false;

			if (_Bottom != null)
				rc = _Bottom.IsConstant();

			return rc;
		}

		static internal string GetCSSDefaults()
		{
			return "border-color:black;";
		}

		internal Expression Default
		{
			get { return  _Default; }
			set {  _Default = value; }
		}

		internal Color EvalDefault(Row r)
		{
			if (_Default == null)
				return System.Drawing.Color.Black;
			
			string c = _Default.EvaluateString(r);
			return XmlUtil.ColorFromHtml(c, System.Drawing.Color.Black, this.OwnerReport);
		}

		internal Expression Left
		{
			get { return  _Left; }
			set {  _Left = value; }
		}

		internal Color EvalLeft(Row r)
		{
			if (_Left == null)
				return EvalDefault(r);
			
			string c = _Left.EvaluateString(r);
			return XmlUtil.ColorFromHtml(c, System.Drawing.Color.Black, this.OwnerReport);
		}

		internal Expression Right
		{
			get { return  _Right; }
			set {  _Right = value; }
		}

		internal Color EvalRight(Row r)
		{
			if (_Right == null)
				return EvalDefault(r);
			
			string c = _Right.EvaluateString(r);
			return XmlUtil.ColorFromHtml(c, System.Drawing.Color.Black, this.OwnerReport);
		}

		internal Expression Top
		{
			get { return  _Top; }
			set {  _Top = value; }
		}

		internal Color EvalTop(Row r)
		{
			if (_Top == null)
				return EvalDefault(r);
			
			string c = _Top.EvaluateString(r);
			return XmlUtil.ColorFromHtml(c, System.Drawing.Color.Black, this.OwnerReport);
		}

		internal Expression Bottom
		{
			get { return  _Bottom; }
			set {  _Bottom = value; }
		}

		internal Color EvalBottom(Row r)
		{
			if (_Bottom == null)
				return EvalDefault(r);
			
			string c = _Bottom.EvaluateString(r);
			return XmlUtil.ColorFromHtml(c, System.Drawing.Color.Black, this.OwnerReport);
		}
	}
}
