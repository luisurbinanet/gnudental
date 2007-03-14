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
	internal class StyleBorderCtl : System.Windows.Forms.UserControl, IProperty
	{
		private ArrayList _ReportItems;
		private DesignXmlDraw _Draw;
		// flags for controlling whether syntax changed for a particular property
		private bool fStyleDefault, fStyleLeft, fStyleRight, fStyleTop, fStyleBottom;
		private bool fColorDefault, fColorLeft, fColorRight, fColorTop, fColorBottom;
		private bool fWidthDefault, fWidthLeft, fWidthRight, fWidthTop, fWidthBottom;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox cbStyleLeft;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox cbStyleBottom;
		private System.Windows.Forms.ComboBox cbStyleTop;
		private System.Windows.Forms.ComboBox cbStyleRight;
		private System.Windows.Forms.Button bColorLeft;
		private System.Windows.Forms.ComboBox cbColorLeft;
		private System.Windows.Forms.Button bColorRight;
		private System.Windows.Forms.ComboBox cbColorRight;
		private System.Windows.Forms.Button bColorTop;
		private System.Windows.Forms.ComboBox cbColorTop;
		private System.Windows.Forms.Button bColorBottom;
		private System.Windows.Forms.ComboBox cbColorBottom;
		private System.Windows.Forms.TextBox tbWidthLeft;
		private System.Windows.Forms.TextBox tbWidthRight;
		private System.Windows.Forms.TextBox tbWidthTop;
		private System.Windows.Forms.TextBox tbWidthBottom;
		private System.Windows.Forms.TextBox tbWidthDefault;
		private System.Windows.Forms.Button bColorDefault;
		private System.Windows.Forms.ComboBox cbColorDefault;
		private System.Windows.Forms.ComboBox cbStyleDefault;
		private System.Windows.Forms.Label lLeft;
		private System.Windows.Forms.Label lBottom;
		private System.Windows.Forms.Label lTop;
		private System.Windows.Forms.Label lRight;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		internal StyleBorderCtl(DesignXmlDraw dxDraw, ArrayList reportItems)
		{
			_ReportItems = reportItems;
			_Draw = dxDraw;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Initialize form using the style node values
			InitBorders((XmlNode)reportItems[0]);			
		}

		private void InitBorders(XmlNode node)
		{
			XmlNode sNode = _Draw.GetNamedChildNode(node, "Style");

			// Handle BorderStyle
			XmlNode bsNode = _Draw.SetElement(sNode, "BorderStyle", null);
			cbStyleDefault.Text = _Draw.GetElementValue(bsNode, "Default", "None");
			cbStyleLeft.Text = _Draw.GetElementValue(bsNode, "Left", cbStyleDefault.Text);
			cbStyleRight.Text = _Draw.GetElementValue(bsNode, "Right", cbStyleDefault.Text);
			cbStyleTop.Text = _Draw.GetElementValue(bsNode, "Top", cbStyleDefault.Text);
			cbStyleBottom.Text = _Draw.GetElementValue(bsNode, "Bottom", cbStyleDefault.Text);

			// Handle BorderColor
			XmlNode bcNode = _Draw.SetElement(sNode, "BorderColor", null);
			cbColorDefault.Text = _Draw.GetElementValue(bcNode, "Default", "Black");
			cbColorLeft.Text = _Draw.GetElementValue(bcNode, "Left", cbColorDefault.Text);
			cbColorRight.Text = _Draw.GetElementValue(bcNode, "Right", cbColorDefault.Text);
			cbColorTop.Text = _Draw.GetElementValue(bcNode, "Top", cbColorDefault.Text);
			cbColorBottom.Text = _Draw.GetElementValue(bcNode, "Bottom", cbColorDefault.Text);

			// Handle BorderWidth
			XmlNode bwNode = _Draw.SetElement(sNode, "BorderWidth", null);
			tbWidthDefault.Text = _Draw.GetElementValue(bwNode, "Default", "1pt");
			tbWidthLeft.Text = _Draw.GetElementValue(bwNode, "Left", tbWidthDefault.Text);
			tbWidthRight.Text = _Draw.GetElementValue(bwNode, "Right", tbWidthDefault.Text);
			tbWidthTop.Text = _Draw.GetElementValue(bwNode, "Top", tbWidthDefault.Text);
			tbWidthBottom.Text = _Draw.GetElementValue(bwNode, "Bottom", tbWidthDefault.Text);
		
			if (node.Name == "Line")
			{
				cbColorLeft.Visible =
					cbColorRight.Visible =
					cbColorTop.Visible =
					cbColorBottom.Visible =
					bColorLeft.Visible =
					bColorRight.Visible =
					bColorTop.Visible =
					bColorBottom.Visible =
					cbStyleLeft.Visible =
					cbStyleRight.Visible =
					cbStyleTop.Visible =
					cbStyleBottom.Visible =
					lLeft.Visible =
					lRight.Visible =
					lTop.Visible =
					lBottom.Visible =
					tbWidthLeft.Visible =
					tbWidthRight.Visible =
					tbWidthTop.Visible =
					tbWidthBottom.Visible = false;
			}
			fStyleDefault = fStyleLeft = fStyleRight = fStyleTop = fStyleBottom =
				fColorDefault = fColorLeft = fColorRight = fColorTop = fColorBottom =
				fWidthDefault = fWidthLeft = fWidthRight = fWidthTop = fWidthBottom= false;
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
			this.lLeft = new System.Windows.Forms.Label();
			this.lBottom = new System.Windows.Forms.Label();
			this.lTop = new System.Windows.Forms.Label();
			this.lRight = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.cbStyleLeft = new System.Windows.Forms.ComboBox();
			this.cbStyleBottom = new System.Windows.Forms.ComboBox();
			this.cbStyleTop = new System.Windows.Forms.ComboBox();
			this.cbStyleRight = new System.Windows.Forms.ComboBox();
			this.bColorLeft = new System.Windows.Forms.Button();
			this.cbColorLeft = new System.Windows.Forms.ComboBox();
			this.bColorRight = new System.Windows.Forms.Button();
			this.cbColorRight = new System.Windows.Forms.ComboBox();
			this.bColorTop = new System.Windows.Forms.Button();
			this.cbColorTop = new System.Windows.Forms.ComboBox();
			this.bColorBottom = new System.Windows.Forms.Button();
			this.cbColorBottom = new System.Windows.Forms.ComboBox();
			this.tbWidthLeft = new System.Windows.Forms.TextBox();
			this.tbWidthRight = new System.Windows.Forms.TextBox();
			this.tbWidthTop = new System.Windows.Forms.TextBox();
			this.tbWidthBottom = new System.Windows.Forms.TextBox();
			this.tbWidthDefault = new System.Windows.Forms.TextBox();
			this.bColorDefault = new System.Windows.Forms.Button();
			this.cbColorDefault = new System.Windows.Forms.ComboBox();
			this.cbStyleDefault = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lLeft
			// 
			this.lLeft.Location = new System.Drawing.Point(16, 74);
			this.lLeft.Name = "lLeft";
			this.lLeft.Size = new System.Drawing.Size(40, 16);
			this.lLeft.TabIndex = 0;
			this.lLeft.Text = "Left";
			this.lLeft.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lBottom
			// 
			this.lBottom.Location = new System.Drawing.Point(16, 170);
			this.lBottom.Name = "lBottom";
			this.lBottom.Size = new System.Drawing.Size(40, 16);
			this.lBottom.TabIndex = 2;
			this.lBottom.Text = "Bottom";
			this.lBottom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lTop
			// 
			this.lTop.Location = new System.Drawing.Point(16, 138);
			this.lTop.Name = "lTop";
			this.lTop.Size = new System.Drawing.Size(40, 16);
			this.lTop.TabIndex = 3;
			this.lTop.Text = "Top";
			this.lTop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lRight
			// 
			this.lRight.Location = new System.Drawing.Point(16, 106);
			this.lRight.Name = "lRight";
			this.lRight.Size = new System.Drawing.Size(40, 16);
			this.lRight.TabIndex = 4;
			this.lRight.Text = "Right";
			this.lRight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(72, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 16);
			this.label2.TabIndex = 5;
			this.label2.Text = "Style";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(320, 16);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(56, 16);
			this.label6.TabIndex = 6;
			this.label6.Text = "Width";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(200, 16);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(56, 16);
			this.label7.TabIndex = 7;
			this.label7.Text = "Color";
			// 
			// cbStyleLeft
			// 
			this.cbStyleLeft.Items.AddRange(new object[] {
															 "None",
															 "Dotted",
															 "Dashed",
															 "Solid",
															 "Double",
															 "Groove",
															 "Ridge",
															 "Inset",
															 "WindowInset",
															 "Outset"});
			this.cbStyleLeft.Location = new System.Drawing.Point(56, 72);
			this.cbStyleLeft.Name = "cbStyleLeft";
			this.cbStyleLeft.Size = new System.Drawing.Size(88, 21);
			this.cbStyleLeft.TabIndex = 4;
			this.cbStyleLeft.SelectedIndexChanged += new System.EventHandler(this.cbStyle_SelectedIndexChanged);
			// 
			// cbStyleBottom
			// 
			this.cbStyleBottom.Items.AddRange(new object[] {
															   "None",
															   "Dotted",
															   "Dashed",
															   "Solid",
															   "Double",
															   "Groove",
															   "Ridge",
															   "Inset",
															   "WindowInset",
															   "Outset"});
			this.cbStyleBottom.Location = new System.Drawing.Point(56, 168);
			this.cbStyleBottom.Name = "cbStyleBottom";
			this.cbStyleBottom.Size = new System.Drawing.Size(88, 21);
			this.cbStyleBottom.TabIndex = 16;
			this.cbStyleBottom.SelectedIndexChanged += new System.EventHandler(this.cbStyle_SelectedIndexChanged);
			// 
			// cbStyleTop
			// 
			this.cbStyleTop.Items.AddRange(new object[] {
															"None",
															"Dotted",
															"Dashed",
															"Solid",
															"Double",
															"Groove",
															"Ridge",
															"Inset",
															"WindowInset",
															"Outset"});
			this.cbStyleTop.Location = new System.Drawing.Point(56, 136);
			this.cbStyleTop.Name = "cbStyleTop";
			this.cbStyleTop.Size = new System.Drawing.Size(88, 21);
			this.cbStyleTop.TabIndex = 12;
			this.cbStyleTop.SelectedIndexChanged += new System.EventHandler(this.cbStyle_SelectedIndexChanged);
			// 
			// cbStyleRight
			// 
			this.cbStyleRight.Items.AddRange(new object[] {
															  "None",
															  "Dotted",
															  "Dashed",
															  "Solid",
															  "Double",
															  "Groove",
															  "Ridge",
															  "Inset",
															  "WindowInset",
															  "Outset"});
			this.cbStyleRight.Location = new System.Drawing.Point(56, 104);
			this.cbStyleRight.Name = "cbStyleRight";
			this.cbStyleRight.Size = new System.Drawing.Size(88, 21);
			this.cbStyleRight.TabIndex = 8;
			this.cbStyleRight.SelectedIndexChanged += new System.EventHandler(this.cbStyle_SelectedIndexChanged);
			// 
			// bColorLeft
			// 
			this.bColorLeft.Location = new System.Drawing.Point(264, 72);
			this.bColorLeft.Name = "bColorLeft";
			this.bColorLeft.Size = new System.Drawing.Size(24, 24);
			this.bColorLeft.TabIndex = 6;
			this.bColorLeft.Text = "...";
			this.bColorLeft.Click += new System.EventHandler(this.bColor_Click);
			// 
			// cbColorLeft
			// 
			this.cbColorLeft.Items.AddRange(new object[] {
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
			this.cbColorLeft.Location = new System.Drawing.Point(160, 72);
			this.cbColorLeft.Name = "cbColorLeft";
			this.cbColorLeft.Size = new System.Drawing.Size(96, 21);
			this.cbColorLeft.TabIndex = 5;
			this.cbColorLeft.SelectedIndexChanged += new System.EventHandler(this.cbColor_SelectedIndexChanged);
			// 
			// bColorRight
			// 
			this.bColorRight.Location = new System.Drawing.Point(264, 104);
			this.bColorRight.Name = "bColorRight";
			this.bColorRight.Size = new System.Drawing.Size(24, 24);
			this.bColorRight.TabIndex = 10;
			this.bColorRight.Text = "...";
			this.bColorRight.Click += new System.EventHandler(this.bColor_Click);
			// 
			// cbColorRight
			// 
			this.cbColorRight.Items.AddRange(new object[] {
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
			this.cbColorRight.Location = new System.Drawing.Point(160, 104);
			this.cbColorRight.Name = "cbColorRight";
			this.cbColorRight.Size = new System.Drawing.Size(96, 21);
			this.cbColorRight.TabIndex = 9;
			this.cbColorRight.SelectedIndexChanged += new System.EventHandler(this.cbColor_SelectedIndexChanged);
			// 
			// bColorTop
			// 
			this.bColorTop.Location = new System.Drawing.Point(264, 136);
			this.bColorTop.Name = "bColorTop";
			this.bColorTop.Size = new System.Drawing.Size(24, 24);
			this.bColorTop.TabIndex = 14;
			this.bColorTop.Text = "...";
			this.bColorTop.Click += new System.EventHandler(this.bColor_Click);
			// 
			// cbColorTop
			// 
			this.cbColorTop.Items.AddRange(new object[] {
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
			this.cbColorTop.Location = new System.Drawing.Point(160, 136);
			this.cbColorTop.Name = "cbColorTop";
			this.cbColorTop.Size = new System.Drawing.Size(96, 21);
			this.cbColorTop.TabIndex = 13;
			this.cbColorTop.SelectedIndexChanged += new System.EventHandler(this.cbColor_SelectedIndexChanged);
			// 
			// bColorBottom
			// 
			this.bColorBottom.Location = new System.Drawing.Point(264, 168);
			this.bColorBottom.Name = "bColorBottom";
			this.bColorBottom.Size = new System.Drawing.Size(24, 24);
			this.bColorBottom.TabIndex = 18;
			this.bColorBottom.Text = "...";
			this.bColorBottom.Click += new System.EventHandler(this.bColor_Click);
			// 
			// cbColorBottom
			// 
			this.cbColorBottom.Items.AddRange(new object[] {
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
			this.cbColorBottom.Location = new System.Drawing.Point(160, 168);
			this.cbColorBottom.Name = "cbColorBottom";
			this.cbColorBottom.Size = new System.Drawing.Size(96, 21);
			this.cbColorBottom.TabIndex = 17;
			this.cbColorBottom.SelectedIndexChanged += new System.EventHandler(this.cbColor_SelectedIndexChanged);
			// 
			// tbWidthLeft
			// 
			this.tbWidthLeft.Location = new System.Drawing.Point(312, 72);
			this.tbWidthLeft.Name = "tbWidthLeft";
			this.tbWidthLeft.Size = new System.Drawing.Size(64, 20);
			this.tbWidthLeft.TabIndex = 7;
			this.tbWidthLeft.Text = "";
			this.tbWidthLeft.TextChanged += new System.EventHandler(this.tbWidth_Changed);
			// 
			// tbWidthRight
			// 
			this.tbWidthRight.Location = new System.Drawing.Point(312, 104);
			this.tbWidthRight.Name = "tbWidthRight";
			this.tbWidthRight.Size = new System.Drawing.Size(64, 20);
			this.tbWidthRight.TabIndex = 11;
			this.tbWidthRight.Text = "";
			this.tbWidthRight.TextChanged += new System.EventHandler(this.tbWidth_Changed);
			// 
			// tbWidthTop
			// 
			this.tbWidthTop.Location = new System.Drawing.Point(312, 136);
			this.tbWidthTop.Name = "tbWidthTop";
			this.tbWidthTop.Size = new System.Drawing.Size(64, 20);
			this.tbWidthTop.TabIndex = 15;
			this.tbWidthTop.Text = "";
			this.tbWidthTop.TextChanged += new System.EventHandler(this.tbWidth_Changed);
			// 
			// tbWidthBottom
			// 
			this.tbWidthBottom.Location = new System.Drawing.Point(312, 168);
			this.tbWidthBottom.Name = "tbWidthBottom";
			this.tbWidthBottom.Size = new System.Drawing.Size(64, 20);
			this.tbWidthBottom.TabIndex = 19;
			this.tbWidthBottom.Text = "";
			this.tbWidthBottom.TextChanged += new System.EventHandler(this.tbWidth_Changed);
			// 
			// tbWidthDefault
			// 
			this.tbWidthDefault.Location = new System.Drawing.Point(312, 40);
			this.tbWidthDefault.Name = "tbWidthDefault";
			this.tbWidthDefault.Size = new System.Drawing.Size(64, 20);
			this.tbWidthDefault.TabIndex = 3;
			this.tbWidthDefault.Text = "";
			this.tbWidthDefault.TextChanged += new System.EventHandler(this.tbWidthDefault_TextChanged);
			// 
			// bColorDefault
			// 
			this.bColorDefault.Location = new System.Drawing.Point(264, 40);
			this.bColorDefault.Name = "bColorDefault";
			this.bColorDefault.Size = new System.Drawing.Size(24, 24);
			this.bColorDefault.TabIndex = 2;
			this.bColorDefault.Text = "...";
			this.bColorDefault.Click += new System.EventHandler(this.bColor_Click);
			// 
			// cbColorDefault
			// 
			this.cbColorDefault.Items.AddRange(new object[] {
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
			this.cbColorDefault.Location = new System.Drawing.Point(160, 40);
			this.cbColorDefault.Name = "cbColorDefault";
			this.cbColorDefault.Size = new System.Drawing.Size(96, 21);
			this.cbColorDefault.TabIndex = 1;
			this.cbColorDefault.SelectedIndexChanged += new System.EventHandler(this.cbColorDefault_SelectedIndexChanged);
			// 
			// cbStyleDefault
			// 
			this.cbStyleDefault.Items.AddRange(new object[] {
																"None",
																"Dotted",
																"Dashed",
																"Solid",
																"Double",
																"Groove",
																"Ridge",
																"Inset",
																"WindowInset",
																"Outset"});
			this.cbStyleDefault.Location = new System.Drawing.Point(56, 40);
			this.cbStyleDefault.Name = "cbStyleDefault";
			this.cbStyleDefault.Size = new System.Drawing.Size(88, 21);
			this.cbStyleDefault.TabIndex = 0;
			this.cbStyleDefault.SelectedIndexChanged += new System.EventHandler(this.cbStyleDefault_SelectedIndexChanged);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(16, 42);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(40, 16);
			this.label8.TabIndex = 36;
			this.label8.Text = "Default";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// StyleBorderCtl
			// 
			this.Controls.Add(this.tbWidthDefault);
			this.Controls.Add(this.bColorDefault);
			this.Controls.Add(this.cbColorDefault);
			this.Controls.Add(this.cbStyleDefault);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.tbWidthBottom);
			this.Controls.Add(this.tbWidthTop);
			this.Controls.Add(this.tbWidthRight);
			this.Controls.Add(this.tbWidthLeft);
			this.Controls.Add(this.bColorBottom);
			this.Controls.Add(this.cbColorBottom);
			this.Controls.Add(this.bColorTop);
			this.Controls.Add(this.cbColorTop);
			this.Controls.Add(this.bColorRight);
			this.Controls.Add(this.cbColorRight);
			this.Controls.Add(this.bColorLeft);
			this.Controls.Add(this.cbColorLeft);
			this.Controls.Add(this.cbStyleRight);
			this.Controls.Add(this.cbStyleTop);
			this.Controls.Add(this.cbStyleBottom);
			this.Controls.Add(this.cbStyleLeft);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lRight);
			this.Controls.Add(this.lTop);
			this.Controls.Add(this.lBottom);
			this.Controls.Add(this.lLeft);
			this.Name = "StyleBorderCtl";
			this.Size = new System.Drawing.Size(472, 312);
			this.ResumeLayout(false);

		}
		#endregion

		public bool IsValid()
		{
			string name="";
			try
			{
				if (fWidthDefault && !this.tbWidthDefault.Text.StartsWith("="))
				{
					name = "Default Width";
					DesignerUtility.ValidateSize(this.tbWidthDefault.Text, true, false);
				}
				if (fWidthLeft && !this.tbWidthLeft.Text.StartsWith("="))
				{
					name = "Left Width";
					DesignerUtility.ValidateSize(this.tbWidthLeft.Text, true, false);
				}
				if (fWidthTop && !this.tbWidthTop.Text.StartsWith("="))
				{
					name = "Top Width";
					DesignerUtility.ValidateSize(this.tbWidthTop.Text, true, false);
				}
				if (fWidthBottom && !this.tbWidthBottom.Text.StartsWith("="))
				{
					name = "Bottom Width";
					DesignerUtility.ValidateSize(this.tbWidthBottom.Text, true, false);
				}
				if (fWidthRight && !this.tbWidthRight.Text.StartsWith("="))
				{
					name = "Right Width";
					DesignerUtility.ValidateSize(this.tbWidthRight.Text, true, false);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, name + " Size Invalid");
				return false;
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

			fStyleDefault = fStyleLeft = fStyleRight = fStyleTop = fStyleBottom =
				fColorDefault = fColorLeft = fColorRight = fColorTop = fColorBottom =
				fWidthDefault = fWidthLeft = fWidthRight = fWidthTop = fWidthBottom= false;
		}

		private void ApplyChanges(XmlNode xNode)
		{
			bool bLine = xNode.Name == "Line";
			XmlNode sNode = _Draw.GetNamedChildNode(xNode, "Style");

			// Handle BorderStyle
			XmlNode bsNode = _Draw.SetElement(sNode, "BorderStyle", null);
			if (fStyleDefault)
				_Draw.SetElement(bsNode, "Default", cbStyleDefault.Text);
			if (fStyleLeft && !bLine)
				_Draw.SetElement(bsNode, "Left", cbStyleLeft.Text);
			if (fStyleRight && !bLine)
				_Draw.SetElement(bsNode, "Right", cbStyleRight.Text);
			if (fStyleTop && !bLine)
				_Draw.SetElement(bsNode, "Top", cbStyleTop.Text);
			if (fStyleBottom && !bLine)
				_Draw.SetElement(bsNode, "Bottom", cbStyleBottom.Text);

			// Handle BorderColor
			XmlNode csNode = _Draw.SetElement(sNode, "BorderColor", null);
			if (fColorDefault)
				_Draw.SetElement(csNode, "Default", cbColorDefault.Text);
			if (fColorLeft && !bLine)
				_Draw.SetElement(csNode, "Left", cbColorLeft.Text);
			if (fColorRight && !bLine)
				_Draw.SetElement(csNode, "Right", cbColorRight.Text);
			if (fColorTop && !bLine)
				_Draw.SetElement(csNode, "Top", cbColorTop.Text);
			if (fColorBottom && !bLine)
				_Draw.SetElement(csNode, "Bottom", cbColorBottom.Text);

			// Handle BorderWidth
			XmlNode bwNode = _Draw.SetElement(sNode, "BorderWidth", null);
			if (fWidthDefault)
				_Draw.SetElement(bwNode, "Default", GetSize(tbWidthDefault.Text));
			if (fWidthLeft && !bLine)
				_Draw.SetElement(bwNode, "Left", GetSize(tbWidthLeft.Text));
			if (fWidthRight && !bLine)
				_Draw.SetElement(bwNode, "Right", GetSize(tbWidthRight.Text));
			if (fWidthTop && !bLine)
				_Draw.SetElement(bwNode, "Top", GetSize(tbWidthTop.Text));
			if (fWidthBottom && !bLine)
				_Draw.SetElement(bwNode, "Bottom", GetSize(tbWidthBottom.Text));
		}

		private string GetSize(string sz)
		{
			if (sz.Trim().StartsWith("="))		// Don't mess with expressions
				return sz;

			float size = DesignXmlDraw.GetSize(sz);
			if (size <= 0)
			{
				size = DesignXmlDraw.GetSize(sz+"pt");	// Try assuming pt
				if (size <= 0)	// still no good
					size = 10;	// just set default value
			}
			string rs = string.Format(NumberFormatInfo.InvariantInfo, "{0:0.#}pt", size);
			return rs;
		}

		private void bColor_Click(object sender, System.EventArgs e)
		{
			ColorDialog cd = new ColorDialog();
			cd.AnyColor = true;
			cd.FullOpen = true;
			
			cd.CustomColors = RdlDesigner.GetCustomColors();

			if (cd.ShowDialog() != DialogResult.OK)
				return;

			RdlDesigner.SetCustomColors(cd.CustomColors);
			if (sender == this.bColorDefault)
			{
				cbColorDefault.Text = ColorTranslator.ToHtml(cd.Color);
				cbColorLeft.Text = ColorTranslator.ToHtml(cd.Color);
				cbColorRight.Text = ColorTranslator.ToHtml(cd.Color);
				cbColorTop.Text = ColorTranslator.ToHtml(cd.Color);
				cbColorBottom.Text = ColorTranslator.ToHtml(cd.Color);
			}	
			else if (sender == this.bColorLeft)
				cbColorLeft.Text = ColorTranslator.ToHtml(cd.Color);
			else if (sender == this.bColorRight)
				cbColorRight.Text = ColorTranslator.ToHtml(cd.Color);
			else if (sender == this.bColorTop)
				cbColorTop.Text = ColorTranslator.ToHtml(cd.Color);
			else if (sender == this.bColorBottom)
				cbColorBottom.Text = ColorTranslator.ToHtml(cd.Color);
					
			return;
		}

		private void cbStyleDefault_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			cbStyleLeft.Text = cbStyleRight.Text = 
				cbStyleTop.Text = cbStyleBottom.Text = cbStyleDefault.Text;
			fStyleDefault = fStyleLeft = fStyleRight = fStyleTop = fStyleBottom = true;
		}

		private void cbColorDefault_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			cbColorLeft.Text = cbColorRight.Text = 
				cbColorTop.Text = cbColorBottom.Text = cbColorDefault.Text;
			fColorDefault = fColorLeft = fColorRight = fColorTop = fColorBottom = true;
		}

		private void tbWidthDefault_TextChanged(object sender, System.EventArgs e)
		{
			tbWidthLeft.Text = tbWidthRight.Text = 
				tbWidthTop.Text = tbWidthBottom.Text = tbWidthDefault.Text;
			fWidthDefault = fWidthLeft = fWidthRight = fWidthTop = fWidthBottom = true;
		}

		private void cbStyle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (sender == cbStyleLeft)
				fStyleLeft = true;
			else if (sender == cbStyleRight)
				fStyleRight = true;
			else if (sender == cbStyleTop)
				fStyleTop = true;
			else if (sender == cbStyleBottom)
				fStyleBottom = true;
		}

		private void cbColor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (sender == cbColorLeft)
				fColorLeft = true;
			else if (sender == cbColorRight)
				fColorRight = true;
			else if (sender == cbColorTop)
				fColorTop = true;
			else if (sender == cbColorBottom)
				fColorBottom = true;
		}

		private void tbWidth_Changed(object sender, System.EventArgs e)
		{
			if (sender == tbWidthLeft)
				fWidthLeft = true;
			else if (sender == tbWidthRight)
				fWidthRight = true;
			else if (sender == tbWidthTop)
				fWidthTop = true;
			else if (sender == tbWidthBottom)
				fWidthBottom = true;
		}
	}
}
