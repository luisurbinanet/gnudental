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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using System.Globalization;

namespace fyiReporting.RdlDesign
{
	/// <summary>
	/// Summary description for StyleCtl.
	/// </summary>
	internal class StyleTextCtl : System.Windows.Forms.UserControl, IProperty
	{
		private ArrayList _ReportItems;
		private DesignXmlDraw _Draw;
		private bool fHorzAlign, fFormat, fDirection, fWritingMode, fTextDecoration;
		private bool fColor, fVerticalAlign, fFontStyle, fFontWeight, fFontSize, fFontFamily;
		private bool fValue;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label lFont;
		private System.Windows.Forms.Button bFont;
		private System.Windows.Forms.ComboBox cbHorzAlign;
		private System.Windows.Forms.ComboBox cbFormat;
		private System.Windows.Forms.ComboBox cbDirection;
		private System.Windows.Forms.ComboBox cbWritingMode;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbTextDecoration;
		private System.Windows.Forms.Button bColor;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox cbColor;
		private System.Windows.Forms.ComboBox cbVerticalAlign;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbFontStyle;
		private System.Windows.Forms.ComboBox cbFontWeight;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ComboBox cbFontSize;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ComboBox cbFontFamily;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblValue;
		private System.Windows.Forms.ComboBox cbValue;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		internal StyleTextCtl(DesignXmlDraw dxDraw, ArrayList styles)
		{
			_ReportItems = styles;
			_Draw = dxDraw;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Initialize form using the style node values
			InitTextStyles();
		}

		private void InitTextStyles()
		{
			XmlNode sNode = (XmlNode) _ReportItems[0];
			if (_ReportItems.Count > 1)
			{
				cbValue.Text = "Group Selected";
				cbValue.Enabled = false;
				lblValue.Enabled = false;
			}
			else if (sNode.Name == "Textbox")
			{
				XmlNode vNode = _Draw.GetNamedChildNode(sNode, "Value");
				if (vNode != null)
					cbValue.Text = vNode.InnerText;
				// now populate the combo box
				// Find the dataregion that contains the Textbox (if any)
				for (XmlNode pNode = sNode.ParentNode; pNode != null; pNode = pNode.ParentNode)
				{
					if (pNode.Name == "List" ||
						pNode.Name == "Table" ||
						pNode.Name == "Matrix" ||
						pNode.Name == "Chart")
					{
						string dsname = _Draw.GetDataSetNameValue(pNode);
						if (dsname != null)	// found it
						{
							string[] f = _Draw.GetFields(dsname, true);
							cbValue.Items.AddRange(f);
						}
					}
				}
				// parameters
				string[] ps = _Draw.GetReportParameters(true);
				if (ps != null)
					cbValue.Items.AddRange(ps);
				// globals
				cbValue.Items.AddRange(StaticLists.GlobalList);
			}
			else if (sNode.Name == "Title")
			{
				lblValue.Text = "Caption";		// Note: label needs to equal the element name
				XmlNode vNode = _Draw.GetNamedChildNode(sNode, "Caption");
				if (vNode != null)
					cbValue.Text = vNode.InnerText;
			}
			else
			{
				lblValue.Visible = false;
				cbValue.Visible = false;
			}

			sNode = _Draw.GetNamedChildNode(sNode, "Style");

			string sFontStyle="Normal";
			string sFontFamily="Arial";
			string sFontWeight="Normal";
			string sFontSize="10pt";
			string sTextDecoration="None";
			string sHorzAlign="General";
			string sVerticalAlign="Top";
			string sColor="Black";
			string sFormat="";
			string sDirection="LTR";
			string sWritingMode="lr-tb";
			foreach (XmlNode lNode in sNode)
			{
				if (lNode.NodeType != XmlNodeType.Element)
					continue;
				switch (lNode.Name)
				{
					case "FontStyle":
						sFontStyle = lNode.InnerText;
						break;
					case "FontFamily":
						sFontFamily = lNode.InnerText;
						break;
					case "FontWeight":
						sFontWeight = lNode.InnerText;
						break;
					case "FontSize":
						sFontSize = lNode.InnerText;
						break;
					case "TextDecoration":
						sTextDecoration = lNode.InnerText;
						break;
					case "TextAlign":
						sHorzAlign = lNode.InnerText;
						break;
					case "VerticalAlign":
						sVerticalAlign = lNode.InnerText;
						break;
					case "Color":
						sColor = lNode.InnerText;
						break;
					case "Format":
						sFormat = lNode.InnerText;
						break;
					case "Direction":
						sDirection = lNode.InnerText;
						break;
					case "WritingMode":
						sWritingMode = lNode.InnerText;
						break;
				}
			}

			// Population Font Family dropdown
			foreach (FontFamily ff in FontFamily.Families)
			{
				cbFontFamily.Items.Add(ff.Name);
			}

			this.cbFontStyle.Text = sFontStyle;
			this.cbFontFamily.Text = sFontFamily;
			this.cbFontWeight.Text = sFontWeight;
			this.cbFontSize.Text = sFontSize;
			this.cbTextDecoration.Text = sTextDecoration;
			this.cbHorzAlign.Text = sHorzAlign;
			this.cbVerticalAlign.Text = sVerticalAlign;
			this.cbColor.Text = sColor;
			this.cbFormat.Text = sFormat;
			this.cbDirection.Text = sDirection;
			this.cbWritingMode.Text = sWritingMode;

			fHorzAlign = fFormat = fDirection = fWritingMode = fTextDecoration =
				fColor = fVerticalAlign = fFontStyle = fFontWeight = fFontSize = fFontFamily =
				fValue = false;

			return;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.lFont = new System.Windows.Forms.Label();
			this.bFont = new System.Windows.Forms.Button();
			this.cbVerticalAlign = new System.Windows.Forms.ComboBox();
			this.cbHorzAlign = new System.Windows.Forms.ComboBox();
			this.cbFormat = new System.Windows.Forms.ComboBox();
			this.cbDirection = new System.Windows.Forms.ComboBox();
			this.cbWritingMode = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cbTextDecoration = new System.Windows.Forms.ComboBox();
			this.bColor = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.cbColor = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cbFontStyle = new System.Windows.Forms.ComboBox();
			this.cbFontWeight = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.cbFontSize = new System.Windows.Forms.ComboBox();
			this.label11 = new System.Windows.Forms.Label();
			this.cbFontFamily = new System.Windows.Forms.ComboBox();
			this.lblValue = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cbValue = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 232);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 16);
			this.label4.TabIndex = 3;
			this.label4.Text = "Format";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(208, 168);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 16);
			this.label5.TabIndex = 4;
			this.label5.Text = "Vertical";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(16, 168);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(56, 16);
			this.label6.TabIndex = 5;
			this.label6.Text = "Alignment";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16, 200);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(56, 16);
			this.label7.TabIndex = 6;
			this.label7.Text = "Direction";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(208, 200);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(80, 16);
			this.label8.TabIndex = 7;
			this.label8.Text = "Writing Mode";
			// 
			// lFont
			// 
			this.lFont.Location = new System.Drawing.Point(16, 16);
			this.lFont.Name = "lFont";
			this.lFont.Size = new System.Drawing.Size(40, 16);
			this.lFont.TabIndex = 8;
			this.lFont.Text = "Family";
			// 
			// bFont
			// 
			this.bFont.Location = new System.Drawing.Point(376, 16);
			this.bFont.Name = "bFont";
			this.bFont.Size = new System.Drawing.Size(24, 23);
			this.bFont.TabIndex = 2;
			this.bFont.Text = "...";
			this.bFont.Click += new System.EventHandler(this.bFont_Click);
			// 
			// cbVerticalAlign
			// 
			this.cbVerticalAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbVerticalAlign.Items.AddRange(new object[] {
																 "Top",
																 "Middle",
																 "Bottom"});
			this.cbVerticalAlign.Location = new System.Drawing.Point(296, 168);
			this.cbVerticalAlign.Name = "cbVerticalAlign";
			this.cbVerticalAlign.Size = new System.Drawing.Size(72, 21);
			this.cbVerticalAlign.TabIndex = 3;
			this.cbVerticalAlign.TextChanged += new System.EventHandler(this.cbVerticalAlign_TextChanged);
			this.cbVerticalAlign.SelectedIndexChanged += new System.EventHandler(this.cbVerticalAlign_TextChanged);
			// 
			// cbHorzAlign
			// 
			this.cbHorzAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbHorzAlign.Items.AddRange(new object[] {
															 "Left",
															 "Center",
															 "Right",
															 "General"});
			this.cbHorzAlign.Location = new System.Drawing.Point(80, 168);
			this.cbHorzAlign.Name = "cbHorzAlign";
			this.cbHorzAlign.Size = new System.Drawing.Size(64, 21);
			this.cbHorzAlign.TabIndex = 2;
			this.cbHorzAlign.TextChanged += new System.EventHandler(this.cbHorzAlign_TextChanged);
			this.cbHorzAlign.SelectedIndexChanged += new System.EventHandler(this.cbHorzAlign_TextChanged);
			// 
			// cbFormat
			// 
			this.cbFormat.Items.AddRange(new object[] {
														  "None",
														  "",
														  "#,##0",
														  "#,##0.00",
														  "0",
														  "0.00",
														  "",
														  "MM/dd/yyyy",
														  "dddd, MMMM dd, yyyy",
														  "dddd, MMMM dd, yyyy HH:mm",
														  "dddd, MMMM dd, yyyy HH:mm:ss",
														  "MM/dd/yyyy HH:mm",
														  "MM/dd/yyyy HH:mm:ss",
														  "MMMM dd",
														  "Ddd, dd MMM yyyy HH\':\'mm\'\"ss \'GMT\'",
														  "yyyy-MM-dd HH:mm:ss",
														  "yyyy-MM-dd HH:mm:ss GMT",
														  "HH:mm",
														  "HH:mm:ss",
														  "yyyy-MM-dd HH:mm:ss"});
			this.cbFormat.Location = new System.Drawing.Point(80, 232);
			this.cbFormat.Name = "cbFormat";
			this.cbFormat.Size = new System.Drawing.Size(288, 21);
			this.cbFormat.TabIndex = 6;
			this.cbFormat.TextChanged += new System.EventHandler(this.cbFormat_TextChanged);
			// 
			// cbDirection
			// 
			this.cbDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbDirection.Items.AddRange(new object[] {
															 "LTR",
															 "RTL"});
			this.cbDirection.Location = new System.Drawing.Point(80, 200);
			this.cbDirection.Name = "cbDirection";
			this.cbDirection.Size = new System.Drawing.Size(64, 21);
			this.cbDirection.TabIndex = 4;
			this.cbDirection.TextChanged += new System.EventHandler(this.cbDirection_TextChanged);
			this.cbDirection.SelectedIndexChanged += new System.EventHandler(this.cbDirection_TextChanged);
			// 
			// cbWritingMode
			// 
			this.cbWritingMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbWritingMode.Items.AddRange(new object[] {
															   "lr-tb",
															   "tb-rl"});
			this.cbWritingMode.Location = new System.Drawing.Point(296, 200);
			this.cbWritingMode.Name = "cbWritingMode";
			this.cbWritingMode.Size = new System.Drawing.Size(72, 21);
			this.cbWritingMode.TabIndex = 5;
			this.cbWritingMode.TextChanged += new System.EventHandler(this.cbWritingMode_TextChanged);
			this.cbWritingMode.SelectedIndexChanged += new System.EventHandler(this.cbWritingMode_TextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(208, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 23);
			this.label2.TabIndex = 19;
			this.label2.Text = "Decoration";
			// 
			// cbTextDecoration
			// 
			this.cbTextDecoration.Items.AddRange(new object[] {
																  "None",
																  "Underline",
																  "Overline",
																  "LineThrough"});
			this.cbTextDecoration.Location = new System.Drawing.Point(280, 80);
			this.cbTextDecoration.Name = "cbTextDecoration";
			this.cbTextDecoration.Size = new System.Drawing.Size(80, 21);
			this.cbTextDecoration.TabIndex = 7;
			this.cbTextDecoration.TextChanged += new System.EventHandler(this.cbTextDecoration_TextChanged);
			this.cbTextDecoration.SelectedIndexChanged += new System.EventHandler(this.cbTextDecoration_TextChanged);
			// 
			// bColor
			// 
			this.bColor.Location = new System.Drawing.Point(176, 80);
			this.bColor.Name = "bColor";
			this.bColor.Size = new System.Drawing.Size(24, 24);
			this.bColor.TabIndex = 6;
			this.bColor.Text = "...";
			this.bColor.Click += new System.EventHandler(this.bColor_Click);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(16, 80);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(48, 16);
			this.label9.TabIndex = 22;
			this.label9.Text = "Color";
			// 
			// cbColor
			// 
			this.cbColor.Items.AddRange(new object[] {
														 "Aliceblue",
														 "Antiquewhite",
														 "Aqua",
														 "Aquamarine",
														 "Azure",
														 "Beige",
														 "Bisque",
														 "Black",
														 "Blanchedalmond",
														 "Blue",
														 "Blueviolet",
														 "Brown",
														 "Burlywood",
														 "Cadetblue",
														 "Chartreuse",
														 "Chocolate",
														 "Coral",
														 "Cornflowerblue",
														 "Cornsilk",
														 "Crimson",
														 "Cyan",
														 "Darkblue",
														 "Darkcyan",
														 "Darkgoldenrod",
														 "Darkgray",
														 "Darkgreen",
														 "Darkkhaki",
														 "Darkmagenta",
														 "Darkolivegreen",
														 "Darkorange",
														 "Darkorchid",
														 "Darkred",
														 "Darksalmon",
														 "Darkseagreen",
														 "Darkslateblue",
														 "Darkslategray",
														 "Darkturquoise",
														 "Darkviolet",
														 "Deeppink",
														 "Deepskyblue",
														 "Dimgray",
														 "Dodgerblue",
														 "Firebrick",
														 "Floralwhite",
														 "Forestgreen",
														 "Fuchsia",
														 "Gainsboro",
														 "Ghostwhite",
														 "Gold",
														 "Goldenrod",
														 "Gray",
														 "Green",
														 "Greenyellow",
														 "Honeydew",
														 "Hotpink",
														 "Indianred Indigo",
														 "Ivory",
														 "Khaki",
														 "Lavender",
														 "Lavenderblush",
														 "Lawngreen",
														 "Lemonchiffon",
														 "Lightblue",
														 "Lightcoral",
														 "Lightcyan",
														 "Lightgoldenrodyellow",
														 "Lightgreen",
														 "Lightgrey",
														 "Lightpink",
														 "Lightsalmon",
														 "Lightseagreen",
														 "Lightskyblue",
														 "Lightslategrey",
														 "Lightsteelblue",
														 "Lightyellow",
														 "Lime",
														 "Limegreen",
														 "Linen",
														 "Magenta",
														 "Maroon",
														 "Mediumaquamarine",
														 "Mediumblue",
														 "Mediumorchid",
														 "Mediumpurple",
														 "Mediumseagreen",
														 "Mediumslateblue",
														 "Mediumspringgreen",
														 "Mediumturquoise",
														 "Mediumvioletred",
														 "Midnightblue",
														 "Mintcream",
														 "Mistyrose",
														 "Moccasin",
														 "Navajowhite",
														 "Navy",
														 "Oldlace",
														 "Olive",
														 "Olivedrab",
														 "Orange",
														 "Orangered",
														 "Orchid",
														 "Palegoldenrod",
														 "Palegreen",
														 "Paleturquoise",
														 "Palevioletred",
														 "Papayawhip",
														 "Peachpuff",
														 "Peru",
														 "Pink",
														 "Plum",
														 "Powderblue",
														 "Purple",
														 "Red",
														 "Rosybrown",
														 "Royalblue",
														 "Saddlebrown",
														 "Salmon",
														 "Sandybrown",
														 "Seagreen",
														 "Seashell",
														 "Sienna",
														 "Silver",
														 "Skyblue",
														 "Slateblue",
														 "Slategray",
														 "Snow",
														 "Springgreen",
														 "Steelblue",
														 "Tan",
														 "Teal",
														 "Thistle",
														 "Tomato",
														 "Turquoise",
														 "Violet",
														 "Wheat",
														 "White",
														 "Whitesmoke",
														 "Yellow",
														 "Yellowgreen"});
			this.cbColor.Location = new System.Drawing.Point(72, 80);
			this.cbColor.Name = "cbColor";
			this.cbColor.Size = new System.Drawing.Size(96, 21);
			this.cbColor.TabIndex = 5;
			this.cbColor.TextChanged += new System.EventHandler(this.cbColor_TextChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 16);
			this.label3.TabIndex = 25;
			this.label3.Text = "Style";
			// 
			// cbFontStyle
			// 
			this.cbFontStyle.Items.AddRange(new object[] {
															 "Normal",
															 "Italic"});
			this.cbFontStyle.Location = new System.Drawing.Point(72, 48);
			this.cbFontStyle.Name = "cbFontStyle";
			this.cbFontStyle.Size = new System.Drawing.Size(96, 21);
			this.cbFontStyle.TabIndex = 3;
			this.cbFontStyle.TextChanged += new System.EventHandler(this.cbFontStyle_TextChanged);
			// 
			// cbFontWeight
			// 
			this.cbFontWeight.Items.AddRange(new object[] {
															  "Lighter",
															  "Normal",
															  "Bold",
															  "Bolder",
															  "100",
															  "200",
															  "300",
															  "400",
															  "500",
															  "600",
															  "700",
															  "800",
															  "900"});
			this.cbFontWeight.Location = new System.Drawing.Point(256, 48);
			this.cbFontWeight.Name = "cbFontWeight";
			this.cbFontWeight.Size = new System.Drawing.Size(104, 21);
			this.cbFontWeight.TabIndex = 4;
			this.cbFontWeight.TextChanged += new System.EventHandler(this.cbFontWeight_TextChanged);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(208, 48);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(40, 16);
			this.label10.TabIndex = 27;
			this.label10.Text = "Weight";
			// 
			// cbFontSize
			// 
			this.cbFontSize.Items.AddRange(new object[] {
															"8pt",
															"9pt",
															"10pt",
															"11pt",
															"12pt",
															"14pt",
															"16pt",
															"18pt",
															"20pt",
															"22pt",
															"24pt",
															"26pt",
															"28pt",
															"36pt",
															"48pt",
															"72pt"});
			this.cbFontSize.Location = new System.Drawing.Point(256, 16);
			this.cbFontSize.Name = "cbFontSize";
			this.cbFontSize.Size = new System.Drawing.Size(104, 21);
			this.cbFontSize.TabIndex = 1;
			this.cbFontSize.TextChanged += new System.EventHandler(this.cbFontSize_TextChanged);
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(208, 16);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(40, 16);
			this.label11.TabIndex = 29;
			this.label11.Text = "Size";
			// 
			// cbFontFamily
			// 
			this.cbFontFamily.Items.AddRange(new object[] {
															  "Arial"});
			this.cbFontFamily.Location = new System.Drawing.Point(72, 16);
			this.cbFontFamily.Name = "cbFontFamily";
			this.cbFontFamily.Size = new System.Drawing.Size(96, 21);
			this.cbFontFamily.TabIndex = 0;
			this.cbFontFamily.TextChanged += new System.EventHandler(this.cbFontFamily_TextChanged);
			// 
			// lblValue
			// 
			this.lblValue.Location = new System.Drawing.Point(8, 16);
			this.lblValue.Name = "lblValue";
			this.lblValue.Size = new System.Drawing.Size(56, 23);
			this.lblValue.TabIndex = 30;
			this.lblValue.Text = "Value";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lFont);
			this.groupBox1.Controls.Add(this.bFont);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.cbTextDecoration);
			this.groupBox1.Controls.Add(this.bColor);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.cbColor);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.cbFontStyle);
			this.groupBox1.Controls.Add(this.cbFontWeight);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.cbFontSize);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.cbFontFamily);
			this.groupBox1.Location = new System.Drawing.Point(8, 40);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(424, 112);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Font";
			// 
			// cbValue
			// 
			this.cbValue.Location = new System.Drawing.Point(64, 16);
			this.cbValue.Name = "cbValue";
			this.cbValue.Size = new System.Drawing.Size(368, 21);
			this.cbValue.TabIndex = 31;
			this.cbValue.Text = "comboBox1";
			this.cbValue.TextChanged += new System.EventHandler(this.cbValue_TextChanged);
			// 
			// StyleTextCtl
			// 
			this.Controls.Add(this.cbValue);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.lblValue);
			this.Controls.Add(this.cbWritingMode);
			this.Controls.Add(this.cbDirection);
			this.Controls.Add(this.cbFormat);
			this.Controls.Add(this.cbHorzAlign);
			this.Controls.Add(this.cbVerticalAlign);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Name = "StyleTextCtl";
			this.Size = new System.Drawing.Size(456, 280);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public bool IsValid()
		{
			if (fFontSize)
			{
				try 
				{
					if (!this.cbFontSize.Text.Trim().StartsWith("="))
						DesignerUtility.ValidateSize(this.cbFontSize.Text, false, false);
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message, "Invalid Font Size");
					return false;
				}

			}
			return true;
		}

		public void Apply()
		{
			// take information in control and apply to all the style nodes
			//  Only change information that has been marked as modified;
			//   this way when group is selected it is possible to change just
			//   the items you want and keep the rest the same.
			
			foreach (XmlNode riNode in this._ReportItems)
				ApplyChanges(riNode);

			fHorzAlign = fFormat = fDirection = fWritingMode = fTextDecoration =
				fColor = fVerticalAlign = fFontStyle = fFontWeight = fFontSize = fFontFamily =
				fValue = false;
		}

		public void ApplyChanges(XmlNode node)
		{
			if (cbValue.Enabled)
			{
				if (fValue)
					_Draw.SetElement(node, lblValue.Text, cbValue.Text);		// only adjust value when single item selected
			}

			XmlNode sNode = _Draw.GetNamedChildNode(node, "Style");

			if (fFontStyle)
				_Draw.SetElement(sNode, "FontStyle", cbFontStyle.Text);
			if (fFontFamily)
				_Draw.SetElement(sNode, "FontFamily", cbFontFamily.Text);
			if (fFontWeight)
				_Draw.SetElement(sNode, "FontWeight", cbFontWeight.Text);

			if (fFontSize)
			{
				float size=10;
				size = DesignXmlDraw.GetSize(cbFontSize.Text);
				if (size <= 0)
				{
					size = DesignXmlDraw.GetSize(cbFontSize.Text+"pt");	// Try assuming pt
					if (size <= 0)	// still no good
						size = 10;	// just set default value
				}
				string rs = string.Format(NumberFormatInfo.InvariantInfo, "{0:0.#}pt", size);

				_Draw.SetElement(sNode, "FontSize", rs);	// force to string
			}
			if (fTextDecoration)
				_Draw.SetElement(sNode, "TextDecoration", cbTextDecoration.Text);    
			if (fHorzAlign)
				_Draw.SetElement(sNode, "TextAlign", cbHorzAlign.Text);
			if (fVerticalAlign)
				_Draw.SetElement(sNode, "VerticalAlign", cbVerticalAlign.Text);
			if (fColor)
				_Draw.SetElement(sNode, "Color", cbColor.Text);
			if (fFormat)
			{
				if (cbFormat.Text.Length == 0)		// Don't put out a format if no format value
					_Draw.RemoveElement(sNode, "Format");
				else
					_Draw.SetElement(sNode, "Format", cbFormat.Text);
			}
			if (fDirection)
				_Draw.SetElement(sNode, "Direction", cbDirection.Text);
			if (fWritingMode)
				_Draw.SetElement(sNode, "WritingMode", cbWritingMode.Text);
			
			return;
		}

		private void bFont_Click(object sender, System.EventArgs e)
		{
			FontDialog fd = new FontDialog();
			fd.ShowColor = true;

			// STYLE
			System.Drawing.FontStyle fs = 0;
			if (cbFontStyle.Text == "Italic")
				fs |= System.Drawing.FontStyle.Italic;

			if (cbTextDecoration.Text == "Underline")
				fs |= FontStyle.Underline;
			else if (cbTextDecoration.Text == "LineThrough")
				fs |= FontStyle.Strikeout;

			// WEIGHT
			switch (cbFontWeight.Text)
			{
				case "Bold":
				case "Bolder":
				case "500":
				case "600":
				case "700":
				case "800":
				case "900":
					fs |= System.Drawing.FontStyle.Bold;
					break;
				default:
					break;
			}
			float size=10;
			size = DesignXmlDraw.GetSize(cbFontSize.Text);
			if (size <= 0)
			{
				size = DesignXmlDraw.GetSize(cbFontSize.Text+"pt");	// Try assuming pt
				if (size <= 0)	// still no good
					size = 10;	// just set default value
			}
			Font drawFont = new Font(cbFontFamily.Text, size, fs);	// si.FontSize already in points


			fd.Font = drawFont;
			fd.Color = 
				DesignerUtility.ColorFromHtml(cbColor.Text, System.Drawing.Color.Black);
			DialogResult dr = fd.ShowDialog();
			if (dr != DialogResult.OK)
			{
				drawFont.Dispose();
				return;
			}

			// Apply all the font info
			cbFontWeight.Text = fd.Font.Bold? "Bold": "Normal";
			cbFontStyle.Text = fd.Font.Italic? "Italic": "Normal";
			cbFontFamily.Text = fd.Font.FontFamily.Name;
			cbFontSize.Text = fd.Font.Size.ToString() + "pt";
			cbColor.Text = ColorTranslator.ToHtml(fd.Color);
			if (fd.Font.Underline)
				this.cbTextDecoration.Text = "Underline";
			else if (fd.Font.Strikeout)
				this.cbTextDecoration.Text = "LineThrough";
			else
				this.cbTextDecoration.Text = "None";
			drawFont.Dispose();

			return;
		}

		private void bColor_Click(object sender, System.EventArgs e)
		{
			ColorDialog cd = new ColorDialog();
			cd.AnyColor = true;
			cd.FullOpen = true;
			
			cd.CustomColors = RdlDesigner.GetCustomColors();
			cd.Color = 
				DesignerUtility.ColorFromHtml(cbColor.Text, System.Drawing.Color.Black);

			if (cd.ShowDialog() != DialogResult.OK)
				return;

			RdlDesigner.SetCustomColors(cd.CustomColors);
			if (sender == this.bColor)
				cbColor.Text = ColorTranslator.ToHtml(cd.Color);
					
			return;
		}

		private void cbValue_TextChanged(object sender, System.EventArgs e)
		{
			fValue = true;
		}

		private void cbFontFamily_TextChanged(object sender, System.EventArgs e)
		{
			fFontFamily = true;
		}

		private void cbFontSize_TextChanged(object sender, System.EventArgs e)
		{
			fFontSize = true;
		}

		private void cbFontStyle_TextChanged(object sender, System.EventArgs e)
		{
			fFontStyle = true;
		}

		private void cbFontWeight_TextChanged(object sender, System.EventArgs e)
		{
			fFontWeight = true;
		}

		private void cbColor_TextChanged(object sender, System.EventArgs e)
		{
			fColor = true;
		}

		private void cbTextDecoration_TextChanged(object sender, System.EventArgs e)
		{
			fTextDecoration = true;
		}

		private void cbHorzAlign_TextChanged(object sender, System.EventArgs e)
		{
			fHorzAlign = true;
		}

		private void cbVerticalAlign_TextChanged(object sender, System.EventArgs e)
		{
			fVerticalAlign = true;
		}

		private void cbDirection_TextChanged(object sender, System.EventArgs e)
		{
			fDirection = true;
		}

		private void cbWritingMode_TextChanged(object sender, System.EventArgs e)
		{
			fWritingMode = true;
		}

		private void cbFormat_TextChanged(object sender, System.EventArgs e)
		{
			fFormat = true;
		}

	}
}
