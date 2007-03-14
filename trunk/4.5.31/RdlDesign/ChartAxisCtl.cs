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

namespace fyiReporting.RdlDesign
{
	/// <summary>
	/// Summary description for ChartCtl.
	/// </summary>
	internal class ChartAxisCtl : System.Windows.Forms.UserControl, IProperty
	{
		private ArrayList _ReportItems;
		private DesignXmlDraw _Draw;
		// change flags
		bool fVisible, fMajorTickMarks, fMargin,fReverse,fInterlaced;
		bool fMajorGLWidth,fMajorGLColor,fMajorGLStyle;
		bool fMinorGLWidth,fMinorGLColor,fMinorGLStyle;
		bool fMajorInterval, fMinorInterval,fMax,fMin;
		bool fMinorTickMarks,fScalar,fLogScale,fMajorGLShow, fMinorGLShow;
		
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox chkVisible;
		private System.Windows.Forms.ComboBox cbMajorTickMarks;
		private System.Windows.Forms.CheckBox chkMargin;
		private System.Windows.Forms.CheckBox chkReverse;
		private System.Windows.Forms.CheckBox chkInterlaced;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox tbMajorGLWidth;
		private System.Windows.Forms.Button bMajorGLColor;
		private System.Windows.Forms.ComboBox cbMajorGLColor;
		private System.Windows.Forms.ComboBox cbMajorGLStyle;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox tbMinorGLWidth;
		private System.Windows.Forms.Button bMinorGLColor;
		private System.Windows.Forms.ComboBox cbMinorGLColor;
		private System.Windows.Forms.ComboBox cbMinorGLStyle;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox tbMajorInterval;
		private System.Windows.Forms.TextBox tbMinorInterval;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox tbMax;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox tbMin;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ComboBox cbMinorTickMarks;
		private System.Windows.Forms.CheckBox chkScalar;
		private System.Windows.Forms.CheckBox chkLogScale;
		private System.Windows.Forms.CheckBox chkMajorGLShow;
		private System.Windows.Forms.CheckBox chkMinorGLShow;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		internal ChartAxisCtl(DesignXmlDraw dxDraw, ArrayList ris)
		{
			_ReportItems = ris;
			_Draw = dxDraw;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Initialize form using the style node values
			InitValues();			
		}

		private void InitValues()
		{
			XmlNode node = (XmlNode) _ReportItems[0];

			chkVisible.Checked = _Draw.GetElementValue(node, "Visible", "False").ToLower() == "true"? true: false;
			chkMargin.Checked = _Draw.GetElementValue(node, "Margin", "False").ToLower() == "true"? true: false;
			chkReverse.Checked = _Draw.GetElementValue(node, "Reverse", "False").ToLower() == "true"? true: false;
			chkInterlaced.Checked = _Draw.GetElementValue(node, "Interlaced", "False").ToLower() == "true"? true: false;
			chkScalar.Checked = _Draw.GetElementValue(node, "Scalar", "False").ToLower() == "true"? true: false;
			chkLogScale.Checked = _Draw.GetElementValue(node, "LogScale", "False").ToLower() == "true"? true: false;
			cbMajorTickMarks.Text = _Draw.GetElementValue(node, "MajorTickMarks", "None");
			cbMinorTickMarks.Text = _Draw.GetElementValue(node, "MinorTickMarks", "None");
			// Major Grid Lines
			InitGridLines(node, "MajorGridLines", chkMajorGLShow, cbMajorGLColor, cbMajorGLStyle, tbMajorGLWidth);
			// Minor Grid Lines
			InitGridLines(node, "MinorGridLines", chkMinorGLShow, cbMinorGLColor, cbMinorGLStyle, tbMinorGLWidth);

			tbMajorInterval.Text = _Draw.GetElementValue(node, "MajorInterval", "");
			tbMinorInterval.Text = _Draw.GetElementValue(node, "MinorInterval", "");
			tbMax.Text = _Draw.GetElementValue(node, "Max", "");
			tbMin.Text = _Draw.GetElementValue(node, "Min", "");

			fVisible = fMajorTickMarks = fMargin=fReverse=fInterlaced=
				fMajorGLWidth=fMajorGLColor=fMajorGLStyle=
				fMinorGLWidth=fMinorGLColor=fMinorGLStyle=
				fMajorInterval= fMinorInterval=fMax=fMin=
				fMinorTickMarks=fScalar=fLogScale=fMajorGLShow=fMinorGLShow=false;
		}

		private void InitGridLines(XmlNode node, string type, CheckBox show, ComboBox color, ComboBox style, TextBox width)
		{
			XmlNode m = _Draw.GetNamedChildNode(node, type);
			if (m != null)
			{
				show.Checked = _Draw.GetElementValue(m, "ShowGridLines", "False").ToLower() == "true"? true: false;
				XmlNode st = _Draw.GetNamedChildNode(m, "Style");
				if (st != null)
				{
					XmlNode work = _Draw.GetNamedChildNode(st, "BorderColor");
					if (work != null)
						color.Text = _Draw.GetElementValue(work, "Default", "Black");
					work = _Draw.GetNamedChildNode(st, "BorderStyle");
					if (work != null)
						style.Text = _Draw.GetElementValue(work, "Default", "Solid");
					work = _Draw.GetNamedChildNode(st, "BorderWidth");
					if (work != null)
						width.Text = _Draw.GetElementValue(work, "Default", "1pt");
				}
			}
			if (color.Text.Length == 0)
				color.Text = "Black";
			if (style.Text.Length == 0)
				style.Text = "Solid";
			if (width.Text.Length == 0)
				width.Text = "1pt";
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cbMajorTickMarks = new System.Windows.Forms.ComboBox();
			this.cbMinorTickMarks = new System.Windows.Forms.ComboBox();
			this.chkVisible = new System.Windows.Forms.CheckBox();
			this.chkMargin = new System.Windows.Forms.CheckBox();
			this.chkReverse = new System.Windows.Forms.CheckBox();
			this.chkInterlaced = new System.Windows.Forms.CheckBox();
			this.chkScalar = new System.Windows.Forms.CheckBox();
			this.chkLogScale = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkMajorGLShow = new System.Windows.Forms.CheckBox();
			this.tbMajorGLWidth = new System.Windows.Forms.TextBox();
			this.bMajorGLColor = new System.Windows.Forms.Button();
			this.cbMajorGLColor = new System.Windows.Forms.ComboBox();
			this.cbMajorGLStyle = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.chkMinorGLShow = new System.Windows.Forms.CheckBox();
			this.tbMinorGLWidth = new System.Windows.Forms.TextBox();
			this.bMinorGLColor = new System.Windows.Forms.Button();
			this.cbMinorGLColor = new System.Windows.Forms.ComboBox();
			this.cbMinorGLStyle = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.tbMajorInterval = new System.Windows.Forms.TextBox();
			this.tbMinorInterval = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.tbMax = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.tbMin = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Major Tick Marks";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(224, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Minor Tick Marks";
			// 
			// cbMajorTickMarks
			// 
			this.cbMajorTickMarks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbMajorTickMarks.Items.AddRange(new object[] {
																  "None",
																  "Inside",
																  "Outside",
																  "Cross"});
			this.cbMajorTickMarks.Location = new System.Drawing.Point(128, 8);
			this.cbMajorTickMarks.Name = "cbMajorTickMarks";
			this.cbMajorTickMarks.Size = new System.Drawing.Size(80, 21);
			this.cbMajorTickMarks.TabIndex = 1;
			this.cbMajorTickMarks.SelectedIndexChanged += new System.EventHandler(this.cbMajorTickMarks_SelectedIndexChanged);
			// 
			// cbMinorTickMarks
			// 
			this.cbMinorTickMarks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbMinorTickMarks.Items.AddRange(new object[] {
																  "None",
																  "Inside",
																  "Outside",
																  "Cross"});
			this.cbMinorTickMarks.Location = new System.Drawing.Point(336, 8);
			this.cbMinorTickMarks.Name = "cbMinorTickMarks";
			this.cbMinorTickMarks.Size = new System.Drawing.Size(80, 21);
			this.cbMinorTickMarks.TabIndex = 3;
			this.cbMinorTickMarks.SelectedIndexChanged += new System.EventHandler(this.cbMinorTickMarks_SelectedIndexChanged);
			// 
			// chkVisible
			// 
			this.chkVisible.Location = new System.Drawing.Point(24, 224);
			this.chkVisible.Name = "chkVisible";
			this.chkVisible.Size = new System.Drawing.Size(88, 24);
			this.chkVisible.TabIndex = 14;
			this.chkVisible.Text = "Visible";
			this.chkVisible.CheckedChanged += new System.EventHandler(this.chkVisible_CheckedChanged);
			// 
			// chkMargin
			// 
			this.chkMargin.Location = new System.Drawing.Point(296, 224);
			this.chkMargin.Name = "chkMargin";
			this.chkMargin.Size = new System.Drawing.Size(120, 24);
			this.chkMargin.TabIndex = 16;
			this.chkMargin.Text = "Margin";
			this.chkMargin.CheckedChanged += new System.EventHandler(this.chkMargin_CheckedChanged);
			// 
			// chkReverse
			// 
			this.chkReverse.Location = new System.Drawing.Point(144, 248);
			this.chkReverse.Name = "chkReverse";
			this.chkReverse.Size = new System.Drawing.Size(120, 24);
			this.chkReverse.TabIndex = 18;
			this.chkReverse.Text = "Reverse Direction";
			this.chkReverse.CheckedChanged += new System.EventHandler(this.chkReverse_CheckedChanged);
			// 
			// chkInterlaced
			// 
			this.chkInterlaced.Location = new System.Drawing.Point(296, 248);
			this.chkInterlaced.Name = "chkInterlaced";
			this.chkInterlaced.Size = new System.Drawing.Size(120, 24);
			this.chkInterlaced.TabIndex = 19;
			this.chkInterlaced.Text = "Interlaced";
			this.chkInterlaced.CheckedChanged += new System.EventHandler(this.chkInterlaced_CheckedChanged);
			// 
			// chkScalar
			// 
			this.chkScalar.Location = new System.Drawing.Point(24, 248);
			this.chkScalar.Name = "chkScalar";
			this.chkScalar.Size = new System.Drawing.Size(88, 24);
			this.chkScalar.TabIndex = 17;
			this.chkScalar.Text = "Scalar";
			this.chkScalar.CheckedChanged += new System.EventHandler(this.chkScalar_CheckedChanged);
			// 
			// chkLogScale
			// 
			this.chkLogScale.Location = new System.Drawing.Point(144, 224);
			this.chkLogScale.Name = "chkLogScale";
			this.chkLogScale.Size = new System.Drawing.Size(120, 24);
			this.chkLogScale.TabIndex = 15;
			this.chkLogScale.Text = "Log Scale";
			this.chkLogScale.CheckedChanged += new System.EventHandler(this.chkLogScale_CheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkMajorGLShow);
			this.groupBox1.Controls.Add(this.tbMajorGLWidth);
			this.groupBox1.Controls.Add(this.bMajorGLColor);
			this.groupBox1.Controls.Add(this.cbMajorGLColor);
			this.groupBox1.Controls.Add(this.cbMajorGLStyle);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(16, 32);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(400, 48);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Major Grid Lines";
			// 
			// chkMajorGLShow
			// 
			this.chkMajorGLShow.Location = new System.Drawing.Point(8, 14);
			this.chkMajorGLShow.Name = "chkMajorGLShow";
			this.chkMajorGLShow.Size = new System.Drawing.Size(56, 24);
			this.chkMajorGLShow.TabIndex = 7;
			this.chkMajorGLShow.Text = "Show";
			this.chkMajorGLShow.CheckedChanged += new System.EventHandler(this.chkMajorGLShow_CheckedChanged);
			// 
			// tbMajorGLWidth
			// 
			this.tbMajorGLWidth.Location = new System.Drawing.Point(352, 16);
			this.tbMajorGLWidth.Name = "tbMajorGLWidth";
			this.tbMajorGLWidth.Size = new System.Drawing.Size(40, 20);
			this.tbMajorGLWidth.TabIndex = 6;
			this.tbMajorGLWidth.Text = "";
			this.tbMajorGLWidth.TextChanged += new System.EventHandler(this.tbMajorGLWidth_TextChanged);
			// 
			// bMajorGLColor
			// 
			this.bMajorGLColor.Location = new System.Drawing.Point(288, 14);
			this.bMajorGLColor.Name = "bMajorGLColor";
			this.bMajorGLColor.Size = new System.Drawing.Size(24, 24);
			this.bMajorGLColor.TabIndex = 4;
			this.bMajorGLColor.Text = "...";
			this.bMajorGLColor.Click += new System.EventHandler(this.bMajorGLColor_Click);
			// 
			// cbMajorGLColor
			// 
			this.cbMajorGLColor.Items.AddRange(new object[] {
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
			this.cbMajorGLColor.Location = new System.Drawing.Point(208, 16);
			this.cbMajorGLColor.Name = "cbMajorGLColor";
			this.cbMajorGLColor.Size = new System.Drawing.Size(72, 21);
			this.cbMajorGLColor.TabIndex = 3;
			this.cbMajorGLColor.SelectedIndexChanged += new System.EventHandler(this.cbMajorGLColor_SelectedIndexChanged);
			// 
			// cbMajorGLStyle
			// 
			this.cbMajorGLStyle.Items.AddRange(new object[] {
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
			this.cbMajorGLStyle.Location = new System.Drawing.Point(96, 16);
			this.cbMajorGLStyle.Name = "cbMajorGLStyle";
			this.cbMajorGLStyle.Size = new System.Drawing.Size(72, 21);
			this.cbMajorGLStyle.TabIndex = 1;
			this.cbMajorGLStyle.SelectedIndexChanged += new System.EventHandler(this.cbMajorGLStyle_SelectedIndexChanged);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(176, 18);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(32, 16);
			this.label7.TabIndex = 2;
			this.label7.Text = "Color";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(320, 18);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(36, 16);
			this.label6.TabIndex = 5;
			this.label6.Text = "Width";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(64, 18);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(36, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "Style";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.chkMinorGLShow);
			this.groupBox2.Controls.Add(this.tbMinorGLWidth);
			this.groupBox2.Controls.Add(this.bMinorGLColor);
			this.groupBox2.Controls.Add(this.cbMinorGLColor);
			this.groupBox2.Controls.Add(this.cbMinorGLStyle);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Location = new System.Drawing.Point(16, 88);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(400, 48);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Minor Grid Lines";
			// 
			// chkMinorGLShow
			// 
			this.chkMinorGLShow.Location = new System.Drawing.Point(8, 14);
			this.chkMinorGLShow.Name = "chkMinorGLShow";
			this.chkMinorGLShow.Size = new System.Drawing.Size(56, 24);
			this.chkMinorGLShow.TabIndex = 8;
			this.chkMinorGLShow.Text = "Show";
			this.chkMinorGLShow.CheckedChanged += new System.EventHandler(this.chkMinorGLShow_CheckedChanged);
			// 
			// tbMinorGLWidth
			// 
			this.tbMinorGLWidth.Location = new System.Drawing.Point(352, 16);
			this.tbMinorGLWidth.Name = "tbMinorGLWidth";
			this.tbMinorGLWidth.Size = new System.Drawing.Size(40, 20);
			this.tbMinorGLWidth.TabIndex = 6;
			this.tbMinorGLWidth.Text = "";
			this.tbMinorGLWidth.TextChanged += new System.EventHandler(this.tbMinorGLWidth_TextChanged);
			// 
			// bMinorGLColor
			// 
			this.bMinorGLColor.Location = new System.Drawing.Point(288, 14);
			this.bMinorGLColor.Name = "bMinorGLColor";
			this.bMinorGLColor.Size = new System.Drawing.Size(24, 24);
			this.bMinorGLColor.TabIndex = 4;
			this.bMinorGLColor.Text = "...";
			this.bMinorGLColor.Click += new System.EventHandler(this.bMinorGLColor_Click);
			// 
			// cbMinorGLColor
			// 
			this.cbMinorGLColor.Items.AddRange(new object[] {
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
			this.cbMinorGLColor.Location = new System.Drawing.Point(208, 16);
			this.cbMinorGLColor.Name = "cbMinorGLColor";
			this.cbMinorGLColor.Size = new System.Drawing.Size(72, 21);
			this.cbMinorGLColor.TabIndex = 3;
			this.cbMinorGLColor.SelectedIndexChanged += new System.EventHandler(this.cbMinorGLColor_SelectedIndexChanged);
			// 
			// cbMinorGLStyle
			// 
			this.cbMinorGLStyle.Items.AddRange(new object[] {
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
			this.cbMinorGLStyle.Location = new System.Drawing.Point(96, 16);
			this.cbMinorGLStyle.Name = "cbMinorGLStyle";
			this.cbMinorGLStyle.Size = new System.Drawing.Size(72, 21);
			this.cbMinorGLStyle.TabIndex = 1;
			this.cbMinorGLStyle.SelectedIndexChanged += new System.EventHandler(this.cbMinorGLStyle_SelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(176, 18);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 16);
			this.label4.TabIndex = 2;
			this.label4.Text = "Color";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(320, 18);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(36, 16);
			this.label5.TabIndex = 5;
			this.label5.Text = "Width";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(64, 18);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(40, 16);
			this.label8.TabIndex = 0;
			this.label8.Text = "Style";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(16, 152);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(80, 16);
			this.label9.TabIndex = 6;
			this.label9.Text = "Major Interval";
			// 
			// tbMajorInterval
			// 
			this.tbMajorInterval.Location = new System.Drawing.Point(104, 152);
			this.tbMajorInterval.Name = "tbMajorInterval";
			this.tbMajorInterval.Size = new System.Drawing.Size(40, 20);
			this.tbMajorInterval.TabIndex = 7;
			this.tbMajorInterval.Text = "";
			this.tbMajorInterval.TextChanged += new System.EventHandler(this.tbMajorInterval_TextChanged);
			// 
			// tbMinorInterval
			// 
			this.tbMinorInterval.Location = new System.Drawing.Point(248, 152);
			this.tbMinorInterval.Name = "tbMinorInterval";
			this.tbMinorInterval.Size = new System.Drawing.Size(40, 20);
			this.tbMinorInterval.TabIndex = 9;
			this.tbMinorInterval.Text = "";
			this.tbMinorInterval.TextChanged += new System.EventHandler(this.tbMinorInterval_TextChanged);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(160, 152);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(80, 16);
			this.label10.TabIndex = 8;
			this.label10.Text = "Minor Interval";
			// 
			// tbMax
			// 
			this.tbMax.Location = new System.Drawing.Point(248, 184);
			this.tbMax.Name = "tbMax";
			this.tbMax.Size = new System.Drawing.Size(40, 20);
			this.tbMax.TabIndex = 13;
			this.tbMax.Text = "";
			this.tbMax.TextChanged += new System.EventHandler(this.tbMax_TextChanged);
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(160, 184);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(88, 16);
			this.label11.TabIndex = 12;
			this.label11.Text = "Maximum Value";
			// 
			// tbMin
			// 
			this.tbMin.Location = new System.Drawing.Point(104, 184);
			this.tbMin.Name = "tbMin";
			this.tbMin.Size = new System.Drawing.Size(40, 20);
			this.tbMin.TabIndex = 11;
			this.tbMin.Text = "";
			this.tbMin.TextChanged += new System.EventHandler(this.tbMin_TextChanged);
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(16, 184);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(88, 16);
			this.label12.TabIndex = 10;
			this.label12.Text = "Minimum Value";
			// 
			// ChartAxisCtl
			// 
			this.Controls.Add(this.tbMax);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.tbMin);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.tbMinorInterval);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.chkLogScale);
			this.Controls.Add(this.chkScalar);
			this.Controls.Add(this.chkInterlaced);
			this.Controls.Add(this.chkReverse);
			this.Controls.Add(this.chkMargin);
			this.Controls.Add(this.chkVisible);
			this.Controls.Add(this.cbMinorTickMarks);
			this.Controls.Add(this.cbMajorTickMarks);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbMajorInterval);
			this.Controls.Add(this.label9);
			this.Name = "ChartAxisCtl";
			this.Size = new System.Drawing.Size(440, 288);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public bool IsValid()
		{
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

			fVisible = fMajorTickMarks = fMargin=fReverse=fInterlaced=
				fMajorGLWidth=fMajorGLColor=fMajorGLStyle=
				fMinorGLWidth=fMinorGLColor=fMinorGLStyle=
				fMajorInterval= fMinorInterval=fMax=fMin=
				fMinorTickMarks=fScalar=fLogScale=fMajorGLShow=fMinorGLShow=false;
		}

		public void ApplyChanges(XmlNode node)
		{
			if (fVisible)
			{
				_Draw.SetElement(node, "Visible", this.chkVisible.Checked? "True": "False");
			}
			if (fMajorTickMarks)
			{
				_Draw.SetElement(node, "MajorTickMarks", this.cbMajorTickMarks.Text);
			}
			if (fMargin)
			{
				_Draw.SetElement(node, "Margin", this.chkMargin.Checked? "True": "False");
			}
			if (fReverse)
			{
				_Draw.SetElement(node, "Reverse", this.chkReverse.Checked? "True": "False");
			}
			if (fInterlaced)
			{
				_Draw.SetElement(node, "Interlaced", this.chkInterlaced.Checked? "True": "False");
			}
			if (fMajorGLShow || fMajorGLWidth || fMajorGLColor || fMajorGLStyle)
			{
				ApplyGridLines(node, "MajorGridLines", chkMajorGLShow, cbMajorGLColor, cbMajorGLStyle, tbMajorGLWidth);
			}
			if (fMinorGLShow || fMinorGLWidth || fMinorGLColor || fMinorGLStyle)
			{
				ApplyGridLines(node, "MinorGridLines", chkMinorGLShow, cbMinorGLColor, cbMinorGLStyle, tbMinorGLWidth);
			}
			if (fMajorInterval)
			{
				_Draw.SetElement(node, "MajorInterval", this.tbMajorInterval.Text);
			}
			if (fMinorInterval)
			{
				_Draw.SetElement(node, "MinorInterval", this.tbMinorInterval.Text);
			}
			if (fMax)
			{
				_Draw.SetElement(node, "Max", this.tbMax.Text);
			}
			if (fMin)
			{
				_Draw.SetElement(node, "Min", this.tbMin.Text);
			}
			if (fMinorTickMarks)
			{
				_Draw.SetElement(node, "MinorTickMarks", this.cbMinorTickMarks.Text);
			}
			if (fScalar)
			{
				_Draw.SetElement(node, "Scalar", this.chkScalar.Checked? "True": "False");
			}
			if (fLogScale)
			{
				_Draw.SetElement(node, "LogScale", this.chkLogScale.Checked? "True": "False");
			}
		}

		private void ApplyGridLines(XmlNode node, string type, CheckBox show, ComboBox color, ComboBox style, TextBox width)
		{
			XmlNode m = _Draw.GetNamedChildNode(node, type);
			if (m == null)
			{
				m = _Draw.CreateElement(node, type, null);
			}

			_Draw.SetElement(m, "ShowGridLines", show.Checked? "True": "False");
			XmlNode st = _Draw.GetNamedChildNode(m, "Style");
			if (st == null)
				st = _Draw.CreateElement(m, "Style", null);

			XmlNode work = _Draw.GetNamedChildNode(st, "BorderColor");
			if (work == null)
				work = _Draw.CreateElement(st, "BorderColor", null);
			_Draw.SetElement(work, "Default", color.Text);

			work = _Draw.GetNamedChildNode(st, "BorderStyle");
			if (work == null)
				work = _Draw.CreateElement(st, "BorderStyle", null);
			_Draw.SetElement(work, "Default", style.Text);
			
			work = _Draw.GetNamedChildNode(st, "BorderWidth");
			if (work == null)
				work = _Draw.CreateElement(st, "BorderWidth", null);
			_Draw.SetElement(work, "Default", width.Text);
		}

		private void cbMajorTickMarks_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fMajorTickMarks = true;
		}

		private void cbMinorTickMarks_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fMinorTickMarks = true;
		}

		private void cbMajorGLStyle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fMajorGLStyle = true;
		}

		private void cbMajorGLColor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fMajorGLColor = true;
		}

		private void tbMajorGLWidth_TextChanged(object sender, System.EventArgs e)
		{
			fMajorGLWidth = true;
		}

		private void cbMinorGLStyle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fMinorGLStyle = true;
		}

		private void cbMinorGLColor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fMinorGLColor = true;
		}

		private void tbMinorGLWidth_TextChanged(object sender, System.EventArgs e)
		{
			fMinorGLWidth = true;
		}

		private void tbMajorInterval_TextChanged(object sender, System.EventArgs e)
		{
			fMajorInterval = true;
		}

		private void tbMinorInterval_TextChanged(object sender, System.EventArgs e)
		{
			fMinorInterval = true;
		}

		private void tbMin_TextChanged(object sender, System.EventArgs e)
		{
			fMin = true;
		}

		private void tbMax_TextChanged(object sender, System.EventArgs e)
		{
			fMax = true;
		}

		private void chkVisible_CheckedChanged(object sender, System.EventArgs e)
		{
			fVisible = true;
		}

		private void chkLogScale_CheckedChanged(object sender, System.EventArgs e)
		{
			fLogScale = true;
		}

		private void chkMargin_CheckedChanged(object sender, System.EventArgs e)
		{
			fMargin = true;
		}

		private void chkScalar_CheckedChanged(object sender, System.EventArgs e)
		{
			fScalar = true;
		}

		private void chkReverse_CheckedChanged(object sender, System.EventArgs e)
		{
			fReverse = true;
		}

		private void chkInterlaced_CheckedChanged(object sender, System.EventArgs e)
		{
			fInterlaced = true;
		}

		private void chkMajorGLShow_CheckedChanged(object sender, System.EventArgs e)
		{
			fMajorGLShow = true;
		}

		private void chkMinorGLShow_CheckedChanged(object sender, System.EventArgs e)
		{
			fMinorGLShow = true;
		}

		private void bMajorGLColor_Click(object sender, System.EventArgs e)
		{
			SetColor(this.cbMajorGLColor);		
		}

		private void bMinorGLColor_Click(object sender, System.EventArgs e)
		{
			SetColor(this.cbMinorGLColor);		
		}

		private void SetColor(ComboBox cbColor)
		{
			ColorDialog cd = new ColorDialog();
			cd.AnyColor = true;
			cd.FullOpen = true;
			
			cd.CustomColors = RdlDesigner.GetCustomColors();
			cd.Color = DesignerUtility.ColorFromHtml(cbColor.Text, System.Drawing.Color.Empty);

			if (cd.ShowDialog() != DialogResult.OK)
				return;

			RdlDesigner.SetCustomColors(cd.CustomColors);
			cbColor.Text = ColorTranslator.ToHtml(cd.Color);
					
			return;
		}
	}
}
