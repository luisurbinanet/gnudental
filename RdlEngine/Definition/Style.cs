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
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace fyiReporting.RDL
{
	///<summary>
	/// Style (borders, fonts, background, padding, ...) of a ReportItem.
	///</summary>
	[Serializable]
	internal class Style : ReportLink
	{
		StyleBorderColor _BorderColor;	// Color of the border
		StyleBorderStyle _BorderStyle;	// Style of the border
		StyleBorderWidth _BorderWidth;	// Width of the border
		Expression _BackgroundColor;	//(Color) Color of the background
		// If omitted, the background is transparent
		Expression _BackgroundGradientType;	// The type of background gradient
		Expression _BackgroundGradientEndColor;	//(Color) End color for the background gradient. If
		// omitted, there is no gradient.
		StyleBackgroundImage _BackgroundImage;	// A background image for the report item.
		// If omitted, there is no background image.
		Expression _FontStyle;		// (Enum FontStyle) Font style Default: Normal
		Expression _FontFamily;		//(string)Name of the font family Default: Arial
		Expression _FontSize;		//(Size) Point size of the font
		// Default: 10 pt. Min: 1 pt. Max: 200 pt.
		Expression _FontWeight;		//(Enum FontWeight) Thickness of the font
		Expression _Format;			//(string) .NET Framework formatting string1
		//	Note: Locale-dependent currency
		//	formatting (format code “C”) is based on
		//	the language setting for the report item
		//	Locale-dependent date formatting is
		//	supported and should be based on the
		//	language property of the ReportItem.
		//	Default: No formatting.
		Expression _TextDecoration;	// (Enum TextDecoration) Special text formatting Default: none
		Expression _TextAlign;		// (Enum TextAlign) Horizontal alignment of the text Default: General
		
		Expression _VerticalAlign;	// (Enum VerticalAlign)	Vertical alignment of the text Default: Top
		Expression _Color;			// (Color) The foreground color	Default: Black
		Expression _PaddingLeft;	// (Size)Padding between the left edge of the
		// report item and its contents1
		// Default: 0 pt. Max: 1000 pt.
		Expression _PaddingRight;	// (Size) Padding between the right edge of the
		// report item and its contents
		// Default: 0 pt. Max: 1000 pt.
		Expression _PaddingTop;		// (Size) Padding between the top edge of the
		// report item and its contents
		// Default: 0 pt. Max: 1000 pt.
		Expression _PaddingBottom;	// (Size) Padding between the top edge of the
		//	report item and its contents
		// Default: 0 pt. Max: 1000 pt
		Expression _LineHeight;		// (Size) Height of a line of text
		// Default: Report output format determines
		// line height based on font size
		// Min: 1 pt. Max: 1000 pt.
		Expression _Direction;		// (Enum Direction) Indicates whether text is written left-to-right (default)
		// or right-to-left.
		// Does not impact the alignment of text
		// unless using General alignment.
		Expression _WritingMode;	// (Enum WritingMode) Indicates whether text is written
		// horizontally or vertically.
		Expression _Language;		// (Language) The primary language of the text.
		// Default is Report.Language.
		Expression _UnicodeBiDirectional;	// (Enum UnicodeBiDirection) 
		// Indicates the level of embedding with
		// respect to the Bi-directional algorithm. Default: normal
		Expression _Calendar;		// (Enum Calendar)
		//	Indicates the calendar to use for
		//	formatting dates. Must be compatible in
		//	.NET framework with the Language
		//	setting.
		Expression _NumeralLanguage;	// (Language) The digit format to use as described by its
		// primary language. Any language is legal.
		// Default is the Language property.
		Expression _NumeralVariant;	//(Integer) The variant of the digit format to use.
		// Currently defined values are:
		// 1: default, follow Unicode context rules
		// 2: 0123456789
		// 3: traditional digits for the script as
		//     defined in GDI+. Currently supported for:
		//		ar | bn | bo | fa | gu | hi | kn | kok | lo | mr |
		//		ms | or | pa | sa | ta | te | th | ur and variants.
		// 4: ko, ja, zh-CHS, zh-CHT only
		// 5: ko, ja, zh-CHS, zh-CHT only
		// 6: ko, ja, zh-CHS, zh-CHT only [Wide
		//     versions of regular digits]
		// 7: ko only
		bool _ConstantStyle;		//  true if all Style elements are constant
		string _CssStyle;			// When ConstantStyle is true; this will hold cache of css
		StyleInfo _StyleInfo;		// When ConstantStyle is true; this will hold cache of StyleInfo

		internal Style(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_BorderColor=null;
			_BorderStyle=null;
			_BorderWidth=null;
			_BackgroundColor=null;
			_BackgroundGradientType=null;
			_BackgroundGradientEndColor=null;
			_BackgroundImage=null;
			_FontStyle=null;
			_FontFamily=null;
			_FontSize=null;
			_FontWeight=null;
			_Format=null;
			_TextDecoration=null;
			_TextAlign=null;
			_VerticalAlign=null;
			_Color=null;
			_PaddingLeft=null;
			_PaddingRight=null;
			_PaddingTop=null;
			_PaddingBottom=null;
			_LineHeight=null;
			_Direction=null;
			_WritingMode=null;
			_Language=null;
			_UnicodeBiDirectional=null;
			_Calendar=null;
			_NumeralLanguage=null;
			_NumeralVariant=null;
			_CssStyle=null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "BorderColor":
						_BorderColor = new StyleBorderColor(r, this, xNodeLoop);
						break;
					case "BorderStyle":
						_BorderStyle = new StyleBorderStyle(r, this, xNodeLoop);
						break;
					case "BorderWidth":
						_BorderWidth = new StyleBorderWidth(r, this, xNodeLoop);
						break;
					case "BackgroundColor":
						_BackgroundColor = new Expression(r, this, xNodeLoop, ExpressionType.Color);
						break;
					case "BackgroundGradientType": 
						_BackgroundGradientType = new Expression(r, this, xNodeLoop, ExpressionType.Enum);
						break;
					case "BackgroundGradientEndColor":
						_BackgroundGradientEndColor = new Expression(r, this, xNodeLoop, ExpressionType.Color);
						break;
					case "BackgroundImage":
						_BackgroundImage = new StyleBackgroundImage(r, this, xNodeLoop);
						break;
					case "FontStyle":
						_FontStyle = new Expression(r, this, xNodeLoop, ExpressionType.Enum);
						break;
					case "FontFamily":
						_FontFamily = new Expression(r, this, xNodeLoop, ExpressionType.String);
						break;
					case "FontSize":
						_FontSize = new Expression(r, this, xNodeLoop, ExpressionType.ReportUnit);
						break;
					case "FontWeight":
						_FontWeight = new Expression(r, this, xNodeLoop, ExpressionType.Enum);
						break;
					case "Format":
						_Format =  new Expression(r, this, xNodeLoop, ExpressionType.String);
						break;
					case "TextDecoration":
						_TextDecoration = new Expression(r, this, xNodeLoop, ExpressionType.Enum);
						break;
					case "TextAlign":
						_TextAlign = new Expression(r, this, xNodeLoop, ExpressionType.Enum);
						break;
					case "VerticalAlign":
						_VerticalAlign = new Expression(r, this, xNodeLoop, ExpressionType.Enum);
						break;
					case "Color":
						_Color =  new Expression(r, this, xNodeLoop, ExpressionType.Color);
						break;
					case "PaddingLeft":
						_PaddingLeft = new Expression(r, this, xNodeLoop, ExpressionType.ReportUnit);
						break;
					case "PaddingRight":
						_PaddingRight = new Expression(r, this, xNodeLoop, ExpressionType.ReportUnit);
						break;
					case "PaddingTop":
						_PaddingTop =  new Expression(r, this, xNodeLoop, ExpressionType.ReportUnit);
						break;
					case "PaddingBottom":
						_PaddingBottom = new Expression(r, this, xNodeLoop, ExpressionType.ReportUnit);
						break;
					case "LineHeight":
						_LineHeight = new Expression(r, this, xNodeLoop, ExpressionType.ReportUnit);
						break;
					case "Direction":
						_Direction = new Expression(r, this, xNodeLoop, ExpressionType.Enum);
						break;
					case "WritingMode":
						_WritingMode = new Expression(r, this, xNodeLoop, ExpressionType.Enum);
						break;
					case "Language":
						_Language = new Expression(r, this, xNodeLoop, ExpressionType.Language);
						break;
					case "UnicodeBiDirectional":
						_UnicodeBiDirectional = new Expression(r, this, xNodeLoop, ExpressionType.Enum);
						break;
					case "Calendar":
						_Calendar = new Expression(r, this, xNodeLoop, ExpressionType.Enum);
						break;
					case "NumeralLanguage":
						_NumeralLanguage = new Expression(r, this, xNodeLoop, ExpressionType.Language);
						break;
					case "NumeralVariant":
						_NumeralVariant = new Expression(r, this, xNodeLoop, ExpressionType.Integer);
						break;
					default:	
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown Style element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
		}

		// Handle parsing of function in final pass
		override internal void FinalPass()
		{
			if (_BorderColor != null)
				_BorderColor.FinalPass();
			if (_BorderStyle != null)
				_BorderStyle.FinalPass();
			if (_BorderWidth != null)
				_BorderWidth.FinalPass();
			if (_BackgroundColor != null)
				_BackgroundColor.FinalPass();
			if (_BackgroundGradientType != null)
				_BackgroundGradientType.FinalPass();
			if (_BackgroundGradientEndColor != null)
				_BackgroundGradientEndColor.FinalPass();
			if (_BackgroundImage != null)
				_BackgroundImage.FinalPass();
			if (_FontStyle != null)
				_FontStyle.FinalPass();
			if (_FontFamily != null)
				_FontFamily.FinalPass();
			if (_FontSize != null)
				_FontSize.FinalPass();
			if (_FontWeight != null)
				_FontWeight.FinalPass();
			if (_Format != null)
				_Format.FinalPass();
			if (_TextDecoration != null)
				_TextDecoration.FinalPass();
			if (_TextAlign != null)
				_TextAlign.FinalPass();
			if (_VerticalAlign != null)
				_VerticalAlign.FinalPass();
			if (_Color != null)
				_Color.FinalPass();
			if (_PaddingLeft != null)
				_PaddingLeft.FinalPass();
			if (_PaddingRight != null)
				_PaddingRight.FinalPass();
			if (_PaddingTop != null)
				_PaddingTop.FinalPass();
			if (_PaddingBottom != null)
				_PaddingBottom.FinalPass();
			if (_LineHeight != null)
				_LineHeight.FinalPass();
			if (_Direction != null)
				_Direction.FinalPass();
			if (_WritingMode != null)
				_WritingMode.FinalPass();
			if (_Language != null)
				_Language.FinalPass();
			if (_UnicodeBiDirectional != null)
				_UnicodeBiDirectional.FinalPass();
			if (_Calendar != null)
				_Calendar.FinalPass();
			if (_NumeralLanguage != null)
				_NumeralLanguage.FinalPass();
			if (_NumeralVariant != null)
				_NumeralVariant.FinalPass();

			_ConstantStyle = this.IsConstant();
			return;
		}

		internal void DrawBackground(Graphics g, Row r, System.Drawing.Rectangle rect)
		{
			LinearGradientBrush linGrBrush = null;

			if (this.BackgroundGradientType != null &&
				this.BackgroundGradientEndColor != null &&
				this.BackgroundColor != null)
			{
				string bgt = this.BackgroundGradientType.EvaluateString(r);
				string bgc = this.BackgroundColor.EvaluateString(r);
				
				Color c = XmlUtil.ColorFromHtml(bgc, System.Drawing.Color.White, this.OwnerReport);

				string bgec = this.BackgroundGradientEndColor.EvaluateString(r);
				Color ec = XmlUtil.ColorFromHtml(bgec, System.Drawing.Color.White, this.OwnerReport);

				switch (bgt)
				{
					case "LeftRight":
						linGrBrush = new LinearGradientBrush(rect, c, ec, LinearGradientMode.Horizontal); 
						break;
					case "TopBottom":
						linGrBrush = new LinearGradientBrush(rect, c, ec, LinearGradientMode.Vertical); 
						break;
					case "Center":	//??
						linGrBrush = new LinearGradientBrush(rect, c, ec, LinearGradientMode.Horizontal); 
						break;
					case "DiagonalLeft":
						linGrBrush = new LinearGradientBrush(rect, c, ec, LinearGradientMode.ForwardDiagonal); 
						break;
					case "DiagonalRight":
						linGrBrush = new LinearGradientBrush(rect, c, ec, LinearGradientMode.BackwardDiagonal); 
						break;
					case "HorizontalCenter":
						linGrBrush = new LinearGradientBrush(rect, c, ec, LinearGradientMode.Horizontal); 
						break;
					case "VerticalCenter":
						linGrBrush = new LinearGradientBrush(rect, c, ec, LinearGradientMode.Vertical); 
						break;
					case "None":
					default:
						break;
				}
			}

			if (linGrBrush != null)
			{
				g.FillRectangle(linGrBrush, rect);
				linGrBrush.Dispose();
			}
			else
			{
				if (this.BackgroundColor != null)
				{
					string bgc = this.BackgroundColor.EvaluateString(r);
					Color c = XmlUtil.ColorFromHtml(bgc, System.Drawing.Color.White, this.OwnerReport);

					SolidBrush sb = new SolidBrush(c);
					g.FillRectangle(sb, rect);
					sb.Dispose();
				}
			}
			return;
		}
 
		internal void DrawBackgroundCircle(Graphics g, Row r, System.Drawing.Rectangle rect)
		{
			// Don't use the gradient in this case (since it won't match) the rest of the 
			//    background.  (Routine is only used by ChartPie in the doughnut case.)
			if (this.BackgroundColor != null)
			{
				string bgc = this.BackgroundColor.EvaluateString(r);
				Color c = XmlUtil.ColorFromHtml(bgc, System.Drawing.Color.White, this.OwnerReport);

				SolidBrush sb = new SolidBrush(c);
				g.FillEllipse(sb, rect);
				g.DrawEllipse(Pens.Black, rect);
				sb.Dispose();
			}
			return;
		}

		// Draw a border using the current style
		internal void DrawBorder(Graphics g, Row r, System.Drawing.Rectangle rect)
		{
			if (this.BorderStyle == null)
				return;

			StyleBorderStyle bs = this.BorderStyle;

			// Create points for each part of rectangular border
			Point tl = new Point(rect.Left, rect.Top);
			Point tr = new Point(rect.Right, rect.Top);
			Point bl = new Point(rect.Left, rect.Bottom);
			Point br = new Point(rect.Right, rect.Bottom);
			// Determine characteristics for each line to be drawn
			BorderStyleEnum topBS, bottomBS, leftBS, rightBS;
			topBS = bottomBS = leftBS = rightBS = BorderStyleEnum.None;
			string v;			// temporary work value
			if (BorderStyle != null)
			{
				if (BorderStyle.Default != null)
				{
					v = BorderStyle.Default.EvaluateString(r);
					topBS = bottomBS = leftBS = rightBS = StyleBorderStyle.GetBorderStyle(v, BorderStyleEnum.None);
				}
				if (BorderStyle.Top != null)
				{
					v = BorderStyle.Top.EvaluateString(r);
					topBS = StyleBorderStyle.GetBorderStyle(v, topBS);
				}
				if (BorderStyle.Bottom != null)
				{
					v = BorderStyle.Bottom.EvaluateString(r);
					bottomBS = StyleBorderStyle.GetBorderStyle(v, bottomBS);
				}
				if (BorderStyle.Left != null)
				{
					v = BorderStyle.Left.EvaluateString(r);
					leftBS = StyleBorderStyle.GetBorderStyle(v, leftBS);
				}
				if (BorderStyle.Right != null)
				{
					v = BorderStyle.Right.EvaluateString(r);
					rightBS = StyleBorderStyle.GetBorderStyle(v, rightBS);
				}
			}

			Color topColor, bottomColor, leftColor, rightColor;
			topColor = bottomColor = leftColor = rightColor = System.Drawing.Color.Black;
			if (BorderColor != null)
			{
				if (BorderColor.Default != null)
				{
					v = BorderColor.Default.EvaluateString(r);
					topColor = bottomColor = leftColor = rightColor = 
						XmlUtil.ColorFromHtml(v, System.Drawing.Color.Black, this.OwnerReport);
				}
				if (BorderColor.Top != null)
				{
					v = BorderColor.Top.EvaluateString(r);
					topColor = XmlUtil.ColorFromHtml(v, System.Drawing.Color.Black, this.OwnerReport);
				}
				if (BorderColor.Bottom != null)
				{
					v = BorderColor.Bottom.EvaluateString(r);
					bottomColor = XmlUtil.ColorFromHtml(v, System.Drawing.Color.Black, this.OwnerReport);
				}
				if (BorderColor.Left != null)
				{
					v = BorderColor.Left.EvaluateString(r);
					leftColor = XmlUtil.ColorFromHtml(v, System.Drawing.Color.Black, this.OwnerReport);
				}
				if (BorderColor.Right != null)
				{
					v = BorderColor.Right.EvaluateString(r);
					rightColor = XmlUtil.ColorFromHtml(v, System.Drawing.Color.Black, this.OwnerReport);
				}
			}

			int topWidth, bottomWidth, leftWidth, rightWidth;
			topWidth = bottomWidth = leftWidth = rightWidth = 1;
			if (BorderWidth != null)
			{
				if (BorderWidth.Default != null)
				{
					topWidth = bottomWidth = leftWidth = rightWidth = (int) new RSize(this.OwnerReport, BorderWidth.Default.EvaluateString(r)).PixelsX;
				}
				if (BorderWidth.Top != null)
				{
					topWidth = (int) new RSize(this.OwnerReport, BorderWidth.Top.EvaluateString(r)).PixelsX;
				}
				if (BorderWidth.Bottom != null)
				{
					bottomWidth = (int) new RSize(this.OwnerReport, BorderWidth.Bottom.EvaluateString(r)).PixelsX;
				}
				if (BorderWidth.Left != null)
				{
					leftWidth = (int) new RSize(this.OwnerReport, BorderWidth.Left.EvaluateString(r)).PixelsY;
				}
				if (BorderWidth.Right != null)
				{
					rightWidth = (int) new RSize(this.OwnerReport, BorderWidth.Right.EvaluateString(r)).PixelsY;
				}
			}

			Pen p = null;
			Brush b = null;
			try
			{
				// top line
				if (topBS != BorderStyleEnum.None)
				{
					b = new SolidBrush(topColor);
					p = new Pen(b, topWidth);
					DrawBorderDashStyle(p, topBS);
					g.DrawLine(p, tl, tr);
					b.Dispose();  b = null;
					p.Dispose();  p = null;
				}
				// right line
				if (rightBS != BorderStyleEnum.None)
				{
					b = new SolidBrush(rightColor);
					p = new Pen(b, rightWidth);
					DrawBorderDashStyle(p, rightBS);
					g.DrawLine(p, tr, br);
					b.Dispose();  b = null;
					p.Dispose();  p = null;
				}
				// bottom line
				if (bottomBS != BorderStyleEnum.None)
				{
					b = new SolidBrush(bottomColor);
					p = new Pen(b, bottomWidth);
					DrawBorderDashStyle(p, bottomBS);
					g.DrawLine(p, br, bl);		// bottom line
					b.Dispose();  b = null;
					p.Dispose();  p = null;
				}
				// left line
				if (leftBS != BorderStyleEnum.None)
				{
					b = new SolidBrush(leftColor);
					p = new Pen(b, leftWidth);
					DrawBorderDashStyle(p, leftBS);
					g.DrawLine(p, bl, tl);
					b.Dispose();  b = null;
					p.Dispose();  p = null;
				}
			}
			finally
			{
				if (p != null)
					p.Dispose();
				if (b != null)
					b.Dispose();
			}
		}

		private void DrawBorderDashStyle(Pen p, BorderStyleEnum bs)
		{
			switch (bs)
			{
				case BorderStyleEnum.Dashed:
					p.DashStyle = DashStyle.Dash;
					break;
				case BorderStyleEnum.Dotted:
					p.DashStyle = DashStyle.Dot;
					break;
				case BorderStyleEnum.Double:
					p.DashStyle = DashStyle.Solid;		// TODO:	really need to create custom?
					break;
				case BorderStyleEnum.Groove:
					p.DashStyle = DashStyle.Solid;		// TODO:
					break;
				case BorderStyleEnum.Inset:
					p.DashStyle = DashStyle.Solid;		// TODO:
					break;
				case BorderStyleEnum.None:
					p.DashStyle = DashStyle.Solid;		// only happens for lines
					break;
				case BorderStyleEnum.Outset:
					p.DashStyle = DashStyle.Solid;		// TODO:
					break;
				case BorderStyleEnum.Ridge:
					p.DashStyle = DashStyle.Solid;		// TODO:
					break;
				case BorderStyleEnum.Solid:
					p.DashStyle = DashStyle.Solid;
					break;
				case BorderStyleEnum.WindowInset:
					p.DashStyle = DashStyle.Solid;		// TODO:
					break;
				default:
					p.DashStyle = DashStyle.Solid;		// really an error
					break;
			}
			return;
		}

		// Draw a line into the specified graphics object using the current style
		internal void DrawStyleLine(Graphics g, Row r, Point s, Point e)
		{
			Pen p = null;
			Brush b = null;
			try
			{
				int width;
				System.Drawing.Color color;
				BorderStyleEnum bs;

				// Border Width default is used for the line width
				if (BorderWidth != null && BorderWidth.Default != null)
					width = (int) new RSize(this.OwnerReport, BorderWidth.Default.EvaluateString(r)).PixelsX;
				else
					width = 1;

				// Border Color default is used for the line color
				if (BorderColor != null && BorderColor.Default != null)
				{
					string v = BorderColor.Default.EvaluateString(r);
					color = XmlUtil.ColorFromHtml(v, System.Drawing.Color.Black, this.OwnerReport);
				}
				else
					color = System.Drawing.Color.Black;

				//
				if (BorderStyle != null && BorderStyle.Default != null)
				{
					string v = BorderStyle.Default.EvaluateString(r);
					bs = StyleBorderStyle.GetBorderStyle(v, BorderStyleEnum.None);
				}
				else
					bs = BorderStyleEnum.Solid;

				b = new SolidBrush(color);
				p = new Pen(b, width);
				DrawBorderDashStyle(p, bs);
				g.DrawLine(p, s, e);
			}
			finally
			{
				if (p != null)
					p.Dispose();
				if (b != null)
					b.Dispose();
			}
		}

		// Draw a string into the specified graphics object using the current style
		//  information
		internal void DrawString(Graphics g, object o, TypeCode tc, Row r, System.Drawing.Rectangle rect)
		{
			Font drawFont=null;				// Font we'll draw with
			Brush drawBrush=null;			// Brush we'll draw with
			StringFormat drawFormat=null;	// StringFormat we'll draw with
			string s;						// the string to draw

			try			// Want to make sure we dispose of the font and brush (no matter what)
			{
				s = Style.GetFormatedString(this, r, o, tc);

				drawFont = GetFont(r);

				drawBrush = GetBrush(r);

				drawFormat = GetStringFormat(r);

				// Draw string
				drawFormat.FormatFlags |= StringFormatFlags.NoWrap;
				g.DrawString(s, drawFont, drawBrush, rect, drawFormat);
			}
			finally
			{
				if (drawFont != null)
					drawFont.Dispose();
				if (drawBrush != null)
					drawBrush.Dispose();
				if (drawFormat != null)
					drawFormat.Dispose();
			}
		}

		static internal void DrawStringDefaults(Graphics g, object o, System.Drawing.Rectangle rect)
		{
			Font drawFont=null;
			SolidBrush drawBrush=null;
			StringFormat drawFormat=null;
			try
			{
				// Just use defaults to Create font and brush.
				drawFont = new Font("Arial", 10);
				drawBrush = new SolidBrush(System.Drawing.Color.Black);
				// Set format of string.
				drawFormat = new StringFormat();
				drawFormat.Alignment = StringAlignment.Center;
				// Draw string to image
				g.DrawString(o.ToString(), drawFont, drawBrush, rect, drawFormat);
			}
			finally
			{
				if (drawFont != null)
					drawFont.Dispose();
				if (drawBrush != null)
					drawBrush.Dispose();
				if (drawFormat != null)
					drawFormat.Dispose();
			}

		}

		// Calc size of a string with the specified graphics object using the current style
		//  information
		internal Size MeasureString(Graphics g, object o, TypeCode tc, Row r, int maxWidth)
		{
			Font drawFont=null;				// Font we'll draw with
			StringFormat drawFormat=null;	// StringFormat we'll draw with
			string s;						// the string to draw

			Size size = Size.Empty;
			try			// Want to make sure we dispose of the font and brush (no matter what)
			{
				s = Style.GetFormatedString(this, r, o, tc);

				drawFont = GetFont(r);

				drawFormat = GetStringFormat(r);

				// Measure string
				if (maxWidth == int.MaxValue)
					drawFormat.FormatFlags |= StringFormatFlags.NoWrap;

				SizeF ms = g.MeasureString(s, drawFont, maxWidth, drawFormat);
				size = new Size((int) Math.Ceiling(ms.Width), 
					(int) Math.Ceiling(ms.Height));
			}
			finally
			{
				if (drawFont != null)
					drawFont.Dispose();
				if (drawFormat != null)
					drawFormat.Dispose();
			}

			return size;
		}

		// Measure a string using the defaults for a Style font
		static internal Size MeasureStringDefaults(Graphics g, object o, TypeCode tc, Row r, int maxWidth)
		{
			Font drawFont=null;				// Font we'll draw with
			StringFormat drawFormat=null;	// StringFormat we'll draw with
			string s;						// the string to draw

			Size size = Size.Empty;
			try			// Want to make sure we dispose of the font and brush (no matter what)
			{
				s = Style.GetFormatedString(null, r, o, tc);

				drawFont = new Font("Arial", 10);
				drawFormat = new StringFormat();
				drawFormat.Alignment = StringAlignment.Near;

				// Measure string
				if (maxWidth == int.MaxValue)
					drawFormat.FormatFlags |= StringFormatFlags.NoWrap;
				SizeF ms = g.MeasureString(s, drawFont, maxWidth, drawFormat);
				size = new Size((int) Math.Ceiling(ms.Width), 
					(int) Math.Ceiling(ms.Height));
			}
			finally
			{
				if (drawFont != null)
					drawFont.Dispose();
				if (drawFormat != null)
					drawFormat.Dispose();
			}

			return size;
		}

		internal Brush GetBrush(Row r)
		{
			Brush drawBrush;
			// Get the brush information
			if (this.Color != null)
			{
				string c = this.Color.EvaluateString(r);
				Color color = XmlUtil.ColorFromHtml(c, System.Drawing.Color.Black, this.OwnerReport);
				drawBrush = new SolidBrush(color);
			}
			else
				drawBrush = new SolidBrush(System.Drawing.Color.Black);
			return drawBrush;
		}

		internal Font GetFont(Row r)
		{
			// Get the font information
			// FAMILY
			string ff;
			if (this.FontFamily != null)
				ff = this.FontFamily.EvaluateString(r);
			else
				ff = "Arial";

			// STYLE
			System.Drawing.FontStyle fs = 0;
			if (this.FontStyle != null)
			{
				string fStyle = this.FontStyle.EvaluateString(r);
				if (fStyle == "Italic")
					fs |= System.Drawing.FontStyle.Italic;
			}
			if (this.TextDecoration != null)
			{
				string td = this.TextDecoration.EvaluateString(r);
				switch (td)
				{
					case "Underline":
						fs |= System.Drawing.FontStyle.Underline;
						break;
					case "Overline":	// Don't support this
						break;
					case "LineThrough":
						fs |= System.Drawing.FontStyle.Strikeout;
						break;
					case "None":
					default:
						break;
				}
			}

			// WEIGHT
			if (this.FontWeight != null)
			{
				string weight = this.FontWeight.EvaluateString(r);
				switch(weight.ToLower())
				{
					case "bold":
					case "bolder":
					case "500":
					case "600":
					case "700":
					case "800":
					case "900":
						fs |= System.Drawing.FontStyle.Bold;
						break;
						// Nothing to do otherwise since we don't have finer gradations
					case "normal":
					case "lighter":
					case "100":
					case "200":
					case "300":
					case "400":
					default:
						break;
				}
			}

			// SIZE
			float size;			// Value is in points
			if (this.FontSize != null)
			{
				string lsize = this.FontSize.EvaluateString(r);
				RSize rs = new RSize(this.OwnerReport, lsize);
				size = rs.Points;
			}
			else
				size = 10;
			
			return new Font(ff, size, fs);
		}

		internal StringFormat GetStringFormat(Row r)
		{
			// Set format of string.
			StringFormat drawFormat = new StringFormat();
			
			if (this.Direction != null)
			{
				string dir = this.Direction.EvaluateString(r);
				if (dir == "RTL")
					drawFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
			}
			if (this.WritingMode != null)
			{
				string wm = this.WritingMode.EvaluateString(r);
				if (wm == "tb-rl")
					drawFormat.FormatFlags |= StringFormatFlags.DirectionVertical;
			}

			if (this.TextAlign != null)
			{
				string ta = this.TextAlign.EvaluateString(r);
				switch (ta.ToLower())
				{
					case "left":
						drawFormat.Alignment = StringAlignment.Near;
						break;
					case "right":
						drawFormat.Alignment = StringAlignment.Far;
						break;
					case "center":
					case "general":
					default:
						drawFormat.Alignment = StringAlignment.Center;
						break;
				}
			}
			else
				drawFormat.Alignment = StringAlignment.Center;

			if (this.VerticalAlign != null)
			{
				string va = this.VerticalAlign.EvaluateString(r);
				switch (va.ToLower())
				{
					case "top":
					default:
						drawFormat.LineAlignment = StringAlignment.Near;
						break;
					case "bottom":
						drawFormat.LineAlignment = StringAlignment.Far;
						break;
					case "middle":
						drawFormat.LineAlignment = StringAlignment.Center;
						break;
				}
			}
			else
				drawFormat.LineAlignment = StringAlignment.Near;

			drawFormat.Trimming = StringTrimming.None;
			return drawFormat;
		}

		// Generate a CSS string from the specified styles
		internal string GetCSS(Row row, bool bDefaults)
		{
			if (_CssStyle != null)		// When CssStyle is available; style is a constant
				return _CssStyle;		//   The first time called bDefaults will affect all subsequant calls

			StringBuilder sb = new StringBuilder();

			if (this.Parent is Table || this.Parent is Matrix)
				sb.Append("border-collapse:collapse;");	// collapse the borders

			if (_BorderColor != null)
				sb.Append(_BorderColor.GetCSS(row, bDefaults));
			else if (bDefaults)
				sb.Append(StyleBorderColor.GetCSSDefaults());

			if (_BorderStyle != null)
				sb.Append(_BorderStyle.GetCSS(row, bDefaults));
			else if (bDefaults)
				sb.Append(StyleBorderStyle.GetCSSDefaults());

			if (_BorderWidth != null)
				sb.Append(_BorderWidth.GetCSS(row, bDefaults));
			else if (bDefaults)
				sb.Append(StyleBorderWidth.GetCSSDefaults());

			if (_BackgroundColor != null)
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "background-color:{0};",_BackgroundColor.EvaluateString(row));
			else if (bDefaults)
				sb.Append("background-color:transparent;");

			if (_BackgroundImage != null)
				sb.Append(_BackgroundImage.GetCSS(row, bDefaults));
			else if (bDefaults)
				sb.Append(StyleBackgroundImage.GetCSSDefaults());

			if (_FontStyle != null)
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "font-style:{0};",_FontStyle.EvaluateString(row));
			else if (bDefaults)
				sb.Append("font-style:normal;");

			if (_FontFamily != null)
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "font-family:{0};",_FontFamily.EvaluateString(row));
			else if (bDefaults)
				sb.Append("font-family:Arial;");

			if (_FontSize != null)
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "font-size:{0};",_FontSize.EvaluateString(row));
			else if (bDefaults)
				sb.Append("font-size:10pt;");

			if (_FontWeight != null)
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "font-weight:{0};",_FontWeight.EvaluateString(row));
			else if (bDefaults)
				sb.Append("font-weight:normal;");

			if (_TextDecoration != null)
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "text-decoration:{0};",_TextDecoration.EvaluateString(row));
			else if (bDefaults)
				sb.Append("text-decoration:none;");

			if (_TextAlign != null)
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "text-align:{0};",_TextAlign.EvaluateString(row));
			else if (bDefaults)
				sb.Append("");	// no CSS default for text align

			if (_VerticalAlign != null)
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "vertical-align:{0};",_VerticalAlign.EvaluateString(row));
			else if (bDefaults)
				sb.Append("vertical-align:top;");

			if (_Color != null)
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "color:{0};",_Color.EvaluateString(row));
			else if (bDefaults)
				sb.Append("color:black;");

			if (_PaddingLeft != null)
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "padding-left:{0};",_PaddingLeft.EvaluateString(row));
			else if (bDefaults)
				sb.Append("padding-left:0pt;");

			if (_PaddingRight != null)
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "padding-right:{0};",_PaddingRight.EvaluateString(row));
			else if (bDefaults)
				sb.Append("padding-right:0pt;");

			if (_PaddingTop != null)
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "padding-top:{0};",_PaddingTop.EvaluateString(row));
			else if (bDefaults)
				sb.Append("padding-top:0pt;");

			if (_PaddingBottom != null)
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "padding-bottom:{0};",_PaddingBottom.EvaluateString(row));
			else if (bDefaults)
				sb.Append("padding-bottom:0pt;");

			if (_LineHeight != null)
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "line-height:{0};",_LineHeight.EvaluateString(row));
			else if (bDefaults)
				sb.Append("line-height:normal;");

			if (this._ConstantStyle)		// We'll only do this work once
			{								//   when all are constant
				_CssStyle = sb.ToString();
				return _CssStyle;
			}

			return sb.ToString();
		}

		// Generate an evaluated version of all the style parameters; used for page processing
		internal StyleInfo GetStyleInfo(Row r)
		{
			if (_StyleInfo != null)		// When StyleInfo is available; style is a constant
				return _StyleInfo;		

			StyleInfo si = new StyleInfo();

			if (this.BorderColor != null)
			{
				StyleBorderColor bc = this.BorderColor;
				si.BColorLeft = bc.EvalLeft(r);
				si.BColorRight = bc.EvalRight(r);
				si.BColorTop = bc.EvalTop(r);
				si.BColorBottom = bc.EvalBottom(r);
			}

			if (this.BorderStyle != null)
			{
				StyleBorderStyle bs = this.BorderStyle;
				si.BStyleLeft = bs.EvalLeft(r);
				si.BStyleRight = bs.EvalRight(r);
				si.BStyleTop = bs.EvalTop(r);
				si.BStyleBottom = bs.EvalBottom(r);
			}

			if (this.BorderWidth != null)
			{
				StyleBorderWidth bw = this.BorderWidth;
				si.BWidthLeft = bw.EvalLeft(r);
				si.BWidthRight = bw.EvalRight(r);
				si.BWidthTop = bw.EvalTop(r);
				si.BWidthBottom = bw.EvalBottom(r);
			}

			si.BackgroundColor = this.EvalBackgroundColor(r);
			// When background color not specified; and reportitem part of table
			//   use the tables background color
			if (si.BackgroundColor == System.Drawing.Color.Empty)
			{
				ReportItem ri = this.Parent as ReportItem;
				if (ri != null)
				{
					if (ri.TC != null)
					{
						Table t = ri.TC.OwnerTable;
						if (t.Style != null)
							si.BackgroundColor = t.Style.EvalBackgroundColor(r);
					}
				}
			}
			si.BackgroundGradientType = this.EvalBackgroundGradientType(r);
			si.BackgroundGradientEndColor = this.EvalBackgroundGradientEndColor(r);
			if (this._BackgroundImage != null)
			{
				si.BackgroundImage = _BackgroundImage.GetPageImage(r);
			}
			else
				si.BackgroundImage = null;

			si.FontStyle = this.EvalFontStyle(r);
			si.FontFamily = this.EvalFontFamily(r);
			si.FontSize = this.EvalFontSize(r);
			si.FontWeight = this.EvalFontWeight(r);
			//		Expression _Format;			//(string) .NET Framework formatting string1
			si.TextDecoration = this.EvalTextDecoration(r);
			si.TextAlign = this.EvalTextAlign(r);
			si.VerticalAlign = this.EvalVerticalAlign(r);
			si.Color = this.EvalColor(r);
			si.PaddingLeft = this.EvalPaddingLeft(r);
			si.PaddingRight = this.EvalPaddingRight(r);
			si.PaddingTop = this.EvalPaddingTop(r);
			si.PaddingBottom = this.EvalPaddingBottom(r);
			si.LineHeight = this.EvalLineHeight(r);
			si.Direction = this.EvalDirection(r);
			si.WritingMode = this.EvalWritingMode(r);
			si.Language = this.EvalLanguage(r);
			si.UnicodeBiDirectional = this.EvalUnicodeBiDirectional(r);
			si.Calendar = this.EvalCalendar(r);
			si.NumeralLanguage = this.EvalNumeralLanguage(r);
			si.NumeralVariant = this.EvalNumeralVariant(r);

			if (this._ConstantStyle)		// We'll only do this work once
				_StyleInfo = si;			//   when all are constant

			return si;
		}

		// Format a string; passed a style but style may be null;
		static internal string GetFormatedString(Style s, Row row, object o, TypeCode tc)
		{
			string t = null;
			if (o == null)
				return "";

			try 
			{
				if (s != null && s.Format != null)
				{
					string format = s.Format.EvaluateString(row);
					if (format != null && format.Length > 0)
					{
						switch (tc)
						{
							case TypeCode.DateTime:
								t = ((DateTime) o).ToString(format);
								break;
							case TypeCode.Int16:
								t = ((short) o).ToString(format);
								break;
							case TypeCode.UInt16:
								t = ((ushort) o).ToString(format);
								break;
							case TypeCode.Int32:
								t = ((int) o).ToString(format);
								break;
							case TypeCode.UInt32:
								t = ((uint) o).ToString(format);
								break;
							case TypeCode.Int64:
								t = ((long) o).ToString(format);
								break;
							case TypeCode.UInt64:
								t = ((ulong) o).ToString(format);
								break;
							case TypeCode.String:
								t = (string) o;
								break;
							case TypeCode.Decimal:
								t = ((decimal) o).ToString(format);
								break;
							case TypeCode.Single:
								t = ((float) o).ToString(format);
								break;
							case TypeCode.Double:
								t = ((double) o).ToString(format);
								break;
							default:
								t = o.ToString();
								break;
						}
					}
					else
						t = o.ToString();
				}
				else
					t = o.ToString();
			}
			catch
			{
				t = o.ToString();
			}
			return t;
		}

		private bool IsConstant()
		{
			bool rc = true;

			if (_BorderColor != null)
				rc = _BorderColor.IsConstant();

			if (!rc)
				return false;

			if (_BorderStyle != null)
				rc = _BorderStyle.IsConstant();

			if (!rc)
				return false;

			if (_BorderWidth != null)
				rc = _BorderWidth.IsConstant();

			if (!rc)
				return false;

			if (_BackgroundColor != null)
				rc = _BackgroundColor.IsConstant();

			if (!rc)
				return false;

			if (_BackgroundImage != null)
				rc = _BackgroundImage.IsConstant();

			if (!rc)
				return false;

			if (_FontStyle != null)
				rc = _FontStyle.IsConstant();

			if (!rc)
				return false;

			if (_FontFamily != null)
				rc = _FontFamily.IsConstant();

			if (!rc)
				return false;

			if (_FontSize != null)
				rc = _FontSize.IsConstant();

			if (!rc)
				return false;

			if (_FontWeight != null)
				rc = _FontWeight.IsConstant();

			if (!rc)
				return false;

			if (_TextDecoration != null)
				rc = _TextDecoration.IsConstant();

			if (!rc)
				return false;

			if (_TextAlign != null)
				rc = _TextAlign.IsConstant();

			if (!rc)
				return false;

			if (_VerticalAlign != null)
				rc = _VerticalAlign.IsConstant();

			if (!rc)
				return false;

			if (_Color != null)
				rc = _Color.IsConstant();

			if (!rc)
				return false;

			if (_PaddingLeft != null)
				rc = _PaddingLeft.IsConstant();

			if (!rc)
				return false;

			if (_PaddingRight != null)
				rc = _PaddingRight.IsConstant();

			if (!rc)
				return false;

			if (_PaddingTop != null)
				rc = _PaddingTop.IsConstant();

			if (!rc)
				return false;

			if (_PaddingBottom != null)
				rc = _PaddingBottom.IsConstant();

			if (!rc)
				return false;

			if (_LineHeight != null)
				rc = _LineHeight.IsConstant();

			if (!rc)
				return false;

			return rc;
		}

		internal System.Drawing.Rectangle PaddingAdjust(Row r, System.Drawing.Rectangle rect, bool bAddIn)
		{
			int pbottom = this.EvalPaddingBottomPx(r);
			int ptop = this.EvalPaddingTopPx(r);
			int pleft = this.EvalPaddingLeftPx(r);
			int pright = this.EvalPaddingRightPx(r);

			System.Drawing.Rectangle rt;
			if (bAddIn)		// add in when trying to size the object
				rt = new System.Drawing.Rectangle(rect.Left - pleft, rect.Top - ptop, 
					rect.Width + pleft + pright, rect.Height + ptop + pbottom);
			else			// otherwise you want the rectangle of the embedded object
				rt = new System.Drawing.Rectangle(rect.Left + pleft, rect.Top + ptop, 
					rect.Width - pleft - pright, rect.Height - ptop - pbottom);
			return rt;
		}

		internal StyleBorderColor BorderColor
		{
			get { return  _BorderColor; }
			set {  _BorderColor = value; }
		}

		internal StyleBorderStyle BorderStyle
		{
			get { return  _BorderStyle; }
			set {  _BorderStyle = value; }
		}

		internal StyleBorderWidth BorderWidth
		{
			get { return  _BorderWidth; }
			set {  _BorderWidth = value; }
		}

		internal Expression BackgroundColor
		{
			get { return  _BackgroundColor; }
			set {  _BackgroundColor = value; }
		}

		internal Color EvalBackgroundColor(Row row)
		{
			if (_BackgroundColor == null)
				return System.Drawing.Color.Empty;

			string c = _BackgroundColor.EvaluateString(row);
			return XmlUtil.ColorFromHtml(c, System.Drawing.Color.Empty, this.OwnerReport);
		}

		internal Expression BackgroundGradientType
		{
			get { return  _BackgroundGradientType; }
			set {  _BackgroundGradientType = value; }
		}

		internal BackgroundGradientTypeEnum EvalBackgroundGradientType(Row r)
		{
			if (_BackgroundGradientType == null)
				return BackgroundGradientTypeEnum.None;

			string bgt = _BackgroundGradientType.EvaluateString(r);
			return 	StyleInfo.GetBackgroundGradientType(bgt, BackgroundGradientTypeEnum.None);
		}

		internal Expression BackgroundGradientEndColor
		{
			get { return  _BackgroundGradientEndColor; }
			set {  _BackgroundGradientEndColor = value; }
		}

		internal Color EvalBackgroundGradientEndColor(Row r)
		{
			if (_BackgroundGradientEndColor == null)
				return System.Drawing.Color.Empty;

			string c = _BackgroundGradientEndColor.EvaluateString(r);
			return XmlUtil.ColorFromHtml(c, System.Drawing.Color.Empty, this.OwnerReport);
		}

		internal StyleBackgroundImage BackgroundImage
		{
			get { return  _BackgroundImage; }
			set {  _BackgroundImage = value; }
		}

		internal bool ConstantStyle
		{
			get { return _ConstantStyle; }
		}

		internal Expression FontStyle
		{
			get { return  _FontStyle; }
			set {  _FontStyle = value; }
		}

		internal bool IsFontItalic(Row r)
		{
			if (EvalFontStyle(r) == FontStyleEnum.Italic)
				return true;

			return false;
		}

		internal FontStyleEnum EvalFontStyle(Row row)
		{
			if (_FontStyle == null)
				return FontStyleEnum.Normal;

			string fs = _FontStyle.EvaluateString(row);
			return StyleInfo.GetFontStyle(fs, FontStyleEnum.Normal);
		}

		internal Expression FontFamily
		{
			get { return  _FontFamily; }
			set {  _FontFamily = value; }
		}

		internal string EvalFontFamily(Row row)
		{
			if (_FontFamily == null)
				return "Arial";

			return _FontFamily.EvaluateString(row);
		}

		internal Expression FontSize
		{
			get { return  _FontSize; }
			set {  _FontSize = value; }
		}

		internal float EvalFontSize(Row row)
		{
			if (_FontSize == null)
				return 10;

			string pts;
			pts = _FontSize.EvaluateString(row);
			RSize sz = new RSize(this.OwnerReport, pts);

			return sz.Points;
		}

		internal Expression FontWeight
		{
			get { return  _FontWeight; }
			set {  _FontWeight = value; }
		}

		internal FontWeightEnum EvalFontWeight(Row row)
		{
			if (_FontWeight == null)
				return FontWeightEnum.Normal;

			string weight = this.FontWeight.EvaluateString(row);
			return StyleInfo.GetFontWeight(weight, FontWeightEnum.Normal);
		}

		internal bool IsFontBold(Row r)
		{
			if (this.FontWeight == null)
				return false;

			string weight = this.FontWeight.EvaluateString(r);
			switch(weight.ToLower())
			{
				case "bold":
				case "bolder":
				case "500":
				case "600":
				case "700":
				case "800":
				case "900":
					return true;
				default:
					return false;
			}
		}

		internal Expression Format
		{
			get { return  _Format; }
			set {  _Format = value; }
		}

		internal Expression TextDecoration
		{
			get { return  _TextDecoration; }
			set {  _TextDecoration = value; }
		}

		internal TextDecorationEnum EvalTextDecoration(Row r)
		{
			if (_TextDecoration == null)
				return TextDecorationEnum.None;

			string td = _TextDecoration.EvaluateString(r);
			return StyleInfo.GetTextDecoration(td, TextDecorationEnum.None);
		}

		internal Expression TextAlign
		{
			get { return  _TextAlign; }
			set {  _TextAlign = value; }
		}

		internal TextAlignEnum EvalTextAlign(Row row)
		{
			if (_TextAlign == null)
				return TextAlignEnum.General;
	
			string a = _TextAlign.EvaluateString(row);
			return StyleInfo.GetTextAlign(a, TextAlignEnum.General);
		}

		internal Expression VerticalAlign
		{
			get { return  _VerticalAlign; }
			set {  _VerticalAlign = value; }
		}

		internal VerticalAlignEnum EvalVerticalAlign(Row row)
		{
			if (_VerticalAlign == null)
				return VerticalAlignEnum.Top;

			string v = _VerticalAlign.EvaluateString(row);
			return StyleInfo.GetVerticalAlign(v, VerticalAlignEnum.Top);
		}

		internal Expression Color
		{
			get { return  _Color; }
			set {  _Color = value; }
		}

		internal Color EvalColor(Row row)
		{
			if (_Color == null)
				return System.Drawing.Color.Black;

			string c = _Color.EvaluateString(row);
			return XmlUtil.ColorFromHtml(c, System.Drawing.Color.Black, this.OwnerReport);
		}

		internal Expression PaddingLeft
		{
			get { return  _PaddingLeft; }
			set {  _PaddingLeft = value; }
		}

		internal float EvalPaddingLeft(Row row)
		{
			if (_PaddingLeft == null)
				return 0;

			string v = _PaddingLeft.EvaluateString(row);
			RSize rz = new RSize(OwnerReport, v);
			return rz.Points;
		}

		internal int EvalPaddingLeftPx(Row row)
		{
			if (_PaddingLeft == null)
				return 0;

			string v = _PaddingLeft.EvaluateString(row);
			RSize rz = new RSize(OwnerReport, v);
			return rz.PixelsX;
		}

		internal Expression PaddingRight
		{
			get { return  _PaddingRight; }
			set {  _PaddingRight = value; }
		}

		internal float EvalPaddingRight(Row row)
		{
			if (_PaddingRight == null)
				return 0;

			string v = _PaddingRight.EvaluateString(row);
			RSize rz = new RSize(OwnerReport, v);
			return rz.Points;
		}

		internal int EvalPaddingRightPx(Row row)
		{
			if (_PaddingRight == null)
				return 0;

			string v = _PaddingRight.EvaluateString(row);
			RSize rz = new RSize(OwnerReport, v);
			return rz.PixelsX;
		}

		internal Expression PaddingTop
		{
			get { return  _PaddingTop; }
			set {  _PaddingTop = value; }
		}

		internal float EvalPaddingTop(Row row)
		{
			if (_PaddingTop == null)
				return 0;

			string v = _PaddingTop.EvaluateString(row);
			RSize rz = new RSize(OwnerReport, v);
			return rz.Points;
		}

		internal int EvalPaddingTopPx(Row row)
		{
			if (_PaddingTop == null)
				return 0;

			string v = _PaddingTop.EvaluateString(row);
			RSize rz = new RSize(OwnerReport, v);
			return rz.PixelsY;
		}

		internal Expression PaddingBottom
		{
			get { return  _PaddingBottom; }
			set {  _PaddingBottom = value; }
		}

		internal float EvalPaddingBottom(Row row)
		{
			if (_PaddingBottom == null)
				return 0;

			string v = _PaddingBottom.EvaluateString(row);
			RSize rz = new RSize(OwnerReport, v);
			return rz.Points;
		}

		internal int EvalPaddingBottomPx(Row row)
		{
			if (_PaddingBottom == null)
				return 0;

			string v = _PaddingBottom.EvaluateString(row);
			RSize rz = new RSize(OwnerReport, v);
			return rz.PixelsY;
		}

		internal Expression LineHeight
		{
			get { return  _LineHeight; }
			set {  _LineHeight = value; }
		}

		internal float EvalLineHeight(Row r)
		{
			if (_LineHeight == null)
				return float.NaN;

			string sz = _LineHeight.EvaluateString(r);
			RSize rz = new RSize(OwnerReport, sz);
			return rz.Points;
		}

		internal Expression Direction
		{
			get { return  _Direction; }
			set {  _Direction = value; }
		}

		internal DirectionEnum EvalDirection(Row r)
		{
			if (_Direction == null)
				return DirectionEnum.LTR;

			string d = _Direction.EvaluateString(r);
			return StyleInfo.GetDirection(d, DirectionEnum.LTR);
		}

		internal Expression WritingMode
		{
			get { return  _WritingMode; }
			set {  _WritingMode = value; }
		}

		internal WritingModeEnum EvalWritingMode(Row r)
		{
			if (_WritingMode == null)
				return WritingModeEnum.lr_tb;

			string w = _WritingMode.EvaluateString(r);

			return StyleInfo.GetWritingMode(w, WritingModeEnum.lr_tb);
		}

		internal Expression Language
		{
			get { return  _Language; }
			set {  _Language = value; }
		}

		internal string EvalLanguage(Row r)
		{
			if (_Language == null)
				return OwnerReport.EvalLanguage(r);

			return _Language.EvaluateString(r);
		}

		internal Expression UnicodeBiDirectional
		{
			get { return  _UnicodeBiDirectional; }
			set {  _UnicodeBiDirectional = value; }
		}

		internal UnicodeBiDirectionalEnum EvalUnicodeBiDirectional(Row r)
		{
			if (_UnicodeBiDirectional == null)
				return UnicodeBiDirectionalEnum.Normal;

			string u = _UnicodeBiDirectional.EvaluateString(r);
			return StyleInfo.GetUnicodeBiDirectional(u, UnicodeBiDirectionalEnum.Normal);
		}

		internal Expression Calendar
		{
			get { return  _Calendar; }
			set {  _Calendar = value; }
		}

		internal CalendarEnum EvalCalendar(Row r)
		{
			if (_Calendar == null)
				return CalendarEnum.Gregorian;

			string c = _Calendar.EvaluateString(r);
			return StyleInfo.GetCalendar(c, CalendarEnum.Gregorian);
		}

		internal Expression NumeralLanguage
		{
			get { return  _NumeralLanguage; }
			set {  _NumeralLanguage = value; }
		}

		internal string EvalNumeralLanguage(Row r)
		{
			if (_NumeralLanguage == null)
				return EvalLanguage(r);

			return _NumeralLanguage.EvaluateString(r);
		}

		internal Expression NumeralVariant
		{
			get { return  _NumeralVariant; }
			set {  _NumeralVariant = value; }
		}

		internal int EvalNumeralVariant(Row r)
		{
			if (_NumeralVariant == null)
				return 1;

			int v = (int) _NumeralVariant.EvaluateDouble(r);
			if (v < 1 || v > 7)		// correct for bad data
				v = 1;
			return v;
		}

	}
}
