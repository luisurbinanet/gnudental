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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Xml;
using System.Globalization;
using fyiReporting.RDL;
using fyiReporting.RdlViewer;

namespace fyiReporting.RdlDesign
{
	/// <summary>
	/// RdlDesigner is used for building and previewing RDL based reports.
	/// </summary>
	public class RdlDesigner : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private MDIChild printChild=null;
		SortedList _RecentFiles=null;
		ArrayList _CurrentFiles=null;		// temporary variable for current files
		private RDL.NeedPassword _GetPassword;
		private string _DataSourceReferencePassword=null;
		private bool bGotPassword=false;
		private bool bMono = false;

		// Status bar
		private System.Windows.Forms.StatusBar mainSB;
		private StatusBarPanel statusPrimary;
		private StatusBarPanel statusSelected;
		private StatusBarPanel statusPosition;

		// Tool bar
		bool bSuppressChange=false;
		private System.Windows.Forms.ToolBar mainTB;
		private SimpleToggle ctlBold;
		private SimpleToggle ctlItalic;
		private SimpleToggle ctlUnderline;
		private ComboBox ctlFont;
		private ComboBox ctlFontSize;
		private ComboBox ctlForeColor;
		private ComboBox ctlBackColor;
		private Button ctlNew;
		private Button ctlOpen;
		private Button ctlSave;
		private Button ctlCut;
		private Button ctlCopy;
		private Button ctlPaste;
		private Button ctlPrint;
		private ComboBox ctlZoom;

		// Insert items
		private SimpleToggle ctlInsertCurrent;
		private SimpleToggle ctlInsertTextbox;
		private SimpleToggle ctlInsertChart;
		private SimpleToggle ctlInsertRectangle;
		private SimpleToggle ctlInsertTable;
		private SimpleToggle ctlInsertMatrix;
		private SimpleToggle ctlInsertList;
		private SimpleToggle ctlInsertLine;
		private SimpleToggle ctlInsertImage;
		private SimpleToggle ctlInsertSubreport;

		// Menu Items
		MenuItem menuFSep1;
		MenuItem menuFSep2;
		MenuItem menuFSep3;
		MenuItem menuFSep4;
		// File
		MenuItem menuNew;
		MenuItem menuOpen;
		MenuItem menuClose;
		MenuItem menuSave;
		MenuItem menuSaveAs;
		MenuItem menuPrint;
		MenuItem menuRecentFile;
		MenuItem menuExit;
		// Edit
		MenuItem menuEdit;
		MenuItem menuEditUndo;
		MenuItem menuEditRedo;
		MenuItem menuEditCut;
		MenuItem menuEditCopy;
		MenuItem menuEditPaste;
		MenuItem menuEditDelete;
		MenuItem menuEditSelectAll;
		MenuItem menuEditProperties;
		MenuItem menuEditFind;
		MenuItem menuEditFindNext;
		MenuItem menuEditReplace;
		MenuItem menuEditGoto;
		MenuItem menuEditFormatXml;
		// Data
		MenuItem menuData;
		MenuItem menuDataSources;
		MenuItem menuDataSets;
		MenuItem menuEmbeddedImages;
		MenuItem menuNewDataSourceRef;

		// Window
		MenuItem menuCascade;
		MenuItem menuTileH;
		MenuItem menuTileV;
		MenuItem menuTile;
		private System.Windows.Forms.Button bTable;
		private System.Windows.Forms.Button bLine;
		private System.Windows.Forms.Button bImage;
		private System.Windows.Forms.Button bRectangle;
		private System.Windows.Forms.Button bSubreport;
		private System.Windows.Forms.Button bList;
		private System.Windows.Forms.Button bChart;
		private System.Windows.Forms.Button bText;
		private System.Windows.Forms.Button bMatrix;
		private System.Windows.Forms.Button bPrint;
		private System.Windows.Forms.Button bSave;
		private System.Windows.Forms.Button bOpen;
		private System.Windows.Forms.Button bPaste;
		private System.Windows.Forms.Button bCopy;
		private System.Windows.Forms.Button bCut;
		private System.Windows.Forms.Button bNew;
		MenuItem menuCloseAll;

		public RdlDesigner(bool mono)
		{
			bMono = mono;
			GetStartupState();
			BuildMenus();
			InitializeComponent();
			this.MdiChildActivate +=new EventHandler(RdlDesigner_MdiChildActivate);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.RdlDesigner_Closing);
			_GetPassword = new RDL.NeedPassword(this.GetPassword);

			InitToolbar();
			InitStatusBar();

			// open up the current files if any
			if (_CurrentFiles != null)
			{
				foreach (string file in _CurrentFiles)
				{
					MDIChild mc = new MDIChild(this.ClientRectangle.Width*3/5, this.ClientRectangle.Height*3/5);
					mc.OnSelectionChanged += new MDIChild.RdlChangeHandler(SelectionChanged);
					mc.OnSelectionMoved += new MDIChild.RdlChangeHandler(SelectionMoved);
					mc.OnReportItemInserted += new MDIChild.RdlChangeHandler(ReportItemInserted);
					mc.OnDesignTabChanged += new MDIChild.RdlChangeHandler(DesignTabChanged);

					mc.MdiParent = this;
					mc.Viewer.GetDataSourceReferencePassword = _GetPassword;
					mc.Viewer.Folder = Path.GetDirectoryName(file);
					mc.Viewer.ReportName = Path.GetFileNameWithoutExtension(file);
					mc.SourceFile = file;
					mc.Text = Path.GetFileName(file);
					mc.Show();
				}
				_CurrentFiles = null;		// don't need this any longer
			}

			DesignTabChanged(this, new EventArgs());		// force toolbar to get updated
		}

		private void InitStatusBar()
		{
			mainSB = new StatusBar();

			mainSB.Parent = this;
			mainSB.Dock = DockStyle.Bottom;
			mainSB.Anchor = AnchorStyles.Top | AnchorStyles.Left;

			statusPrimary = new StatusBarPanel();
			statusPrimary.Width = 260;
			statusPrimary.MinWidth = 10;
			statusPrimary.AutoSize = StatusBarPanelAutoSize.Spring;
			statusPrimary.Text = "";
			statusPrimary.BorderStyle = StatusBarPanelBorderStyle.None;

			statusSelected = new StatusBarPanel();
			statusSelected.AutoSize = StatusBarPanelAutoSize.Contents;
			statusSelected.Alignment = HorizontalAlignment.Center;
			statusSelected.ToolTipText = "Name of selected ReportItem";
			statusSelected.Width = 192;
			statusSelected.MinWidth = 10;
			statusSelected.Text = "";
			statusSelected.BorderStyle = StatusBarPanelBorderStyle.Sunken;

			statusPosition = new StatusBarPanel();
			statusPosition.AutoSize = StatusBarPanelAutoSize.Contents;
			statusPosition.ToolTipText = "Position of selected ReportItem";
			statusPosition.Width = 96;
			statusPosition.MinWidth = 10;
			statusPosition.Alignment = HorizontalAlignment.Center;
			statusPosition.Text = "";
			statusPosition.BorderStyle = StatusBarPanelBorderStyle.Raised;
			
			mainSB.Size = new System.Drawing.Size(472, 22);

			mainSB.ShowPanels = true;
			mainSB.Location = new System.Drawing.Point(0, 282);
			mainSB.Name = "mainSB";
			mainSB.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																			  this.statusPrimary,
																			  this.statusSelected,
																			  this.statusPosition});
			this.Controls.Add(mainSB);
		}

		private void InitToolbar()
		{
			mainTB = new ToolBar();
			// This is the default toolbar setup:  TODO - put this in the config file
			string[] items = new string[] {
				"New", "Open", "Save", "Space", "Cut", "Copy", "Paste", "Space",
				"Textbox", "Chart", "Table", "List", "Image", "Matrix", "Subreport", "Rectangle", "Line","\n",
				"Bold", "Italic", "Underline", "Space",
				"Font", "FontSize", "Space", "ForeColor", "BackColor", 
				"Space", "Print", "Space", "Zoom"};

			const int LINEHEIGHT = 22;
			const int LEFTMARGIN = 5;
			int y = 2;
			int x = LEFTMARGIN;

			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(RdlDesigner));

			// Build the controls the user wants
			foreach (string tbi in items)
			{
				switch (tbi)
				{
					case "\n":
						y += LINEHEIGHT;
						x = LEFTMARGIN;
						break;
					case "Bold":
						x += InitToolbarBold(x,y);
						break;
					case "Italic":
						x += InitToolbarItalic(x,y);
						break;
					case "Underline":
						x += InitToolbarUnderline(x,y);
						break;
					case "Space":
						x += 5;
						break;
					case "Font":
						x += InitToolbarFont(x,y);
						break;
					case "FontSize":
						x += InitToolbarFontSize(x,y);
						break;
					case "ForeColor":
						ctlForeColor = InitToolbarColor(ref x, y, "Fore Color");
						ctlForeColor.SelectedValueChanged +=new EventHandler(ctlForeColor_Change);
						ctlForeColor.Validated +=new EventHandler(ctlForeColor_Change);
						break;
					case "BackColor":
						ctlBackColor = InitToolbarColor(ref x, y, "Back Color");
						ctlBackColor.SelectedValueChanged +=new EventHandler(ctlBackColor_Change);
						ctlBackColor.Validated +=new EventHandler(ctlBackColor_Change);
						break;
					case "New":
						ctlNew = InitToolbarMenu(ref x, y, "New", bNew.Image, new EventHandler(this.menuFileNewReport_Click));
						break;
					case "Open":
						ctlOpen = InitToolbarMenu(ref x, y, "Open", bOpen.Image, new EventHandler(this.menuFileOpen_Click));
						break;
					case "Save":
						ctlSave = InitToolbarMenu(ref x, y, "Save", bSave.Image, new EventHandler(this.menuFileSave_Click));
						break;
					case "Cut":
						ctlCut = InitToolbarMenu(ref x, y, "Cut", bCut.Image, new EventHandler(this.menuEditCut_Click));
						break;
					case "Copy":
						ctlCopy = InitToolbarMenu(ref x, y, "Copy", bCopy.Image, new EventHandler(this.menuEditCopy_Click));
						break;
					case "Paste":
						ctlPaste = InitToolbarMenu(ref x, y, "Paste", bPaste.Image, new EventHandler(this.menuEditPaste_Click));
						break;
					case "Print":
						ctlPrint = InitToolbarMenu(ref x, y, "Print", bPrint.Image, new EventHandler(this.menuFilePrint_Click));
						break;
					case "Textbox":
						ctlInsertTextbox = InitToolbarInsertToogle(ref x, y, "Textbox", bText.Image);
						break;
					case "Chart":
						ctlInsertChart = InitToolbarInsertToogle(ref x, y, "Chart", bChart.Image);
						break;
					case "Rectangle":
						ctlInsertRectangle = InitToolbarInsertToogle(ref x, y, "Rectangle", bRectangle.Image);
						break;
					case "Table":
						ctlInsertTable = InitToolbarInsertToogle(ref x, y, "Table",	bTable.Image);
						break;
					case "Matrix":
						ctlInsertMatrix = InitToolbarInsertToogle(ref x, y, "Matrix", bMatrix.Image);
						break;
					case "List":
						ctlInsertList = InitToolbarInsertToogle(ref x, y, "List", bList.Image);
						break;
					case "Line":
						ctlInsertLine = InitToolbarInsertToogle(ref x, y, "Line", bLine.Image);
						break;
					case "Image":
						ctlInsertImage = InitToolbarInsertToogle(ref x, y, "Image", bImage.Image);
						break;
					case "Subreport":
						ctlInsertSubreport = InitToolbarInsertToogle(ref x, y, "Subreport", bSubreport.Image);
						break;
					case "Zoom":
						ctlZoom = InitToolbarZoom(ref x, y);
						ctlZoom.SelectedValueChanged +=new EventHandler(ctlZoom_Change);
						ctlZoom.Validated +=new EventHandler(ctlZoom_Change);
						break;
					default:
						break;
				}
			}

			mainTB.Parent = this;
			mainTB.BorderStyle = BorderStyle.None;
			mainTB.DropDownArrows = true;
			mainTB.Name = "mainTB";
			mainTB.ShowToolTips = true;
			//			mainTB.Size = new Size(440,20);
			mainTB.TabIndex = 1;
			mainTB.AutoSize = false;
			mainTB.Height = y + LINEHEIGHT + 4;	
		}

		private Button InitToolbarMenu(ref int x, int y, string t, System.EventHandler handler)
		{
			return InitToolbarMenu(ref x, y, t, null, handler);
		}

		private Button InitToolbarMenu(ref int x, int y, string t, Image img, System.EventHandler handler)
		{
			SimpleButton ctl = new SimpleButton(this);	 
			
			ctl.Click += handler;
			if (img == null)
			{
				ctl.Text = t;
				using ( Graphics g = ctl.CreateGraphics() )
				{
					SizeF fs = g.MeasureString( ctl.Text, ctl.Font);
					ctl.Height = (int) fs.Height + 8;	// 8 is for margins
					ctl.Width = (int) fs.Width + 12;		
				}
			}
			else
			{
				ctl.Image = img;
				ctl.ImageAlign = ContentAlignment.MiddleCenter;
				ctl.Height = img.Height + 5;
				ctl.Width = img.Width + 8;
				ctl.Text = "";
			}
			ctl.Tag = t;
			ctl.Left = x;
			ctl.Top = y;
			ctl.FlatStyle = FlatStyle.Flat;
			ToolTip tip = new ToolTip();
			tip.AutomaticDelay = 500;
			tip.ShowAlways = true;
			tip.SetToolTip(ctl, t);
			mainTB.Controls.Add(ctl);

			x = x+ ctl.Width;

			return ctl;
		}

		private SimpleToggle InitToolbarInsertToogle(ref int x, int y, string t)
		{
			return InitToolbarInsertToogle(ref x, y, t, null);
		}

		private SimpleToggle InitToolbarInsertToogle(ref int x, int y, string t,
			Image bImg)
		{
			SimpleToggle ctl = new SimpleToggle();
			ctl.UpColor = this.BackColor;
			ctl.Click +=new EventHandler(this.Insert_Click);	// click handler for all inserts

			if (bImg == null)
			{
				ctl.Text = t;
				using ( Graphics g = ctl.CreateGraphics() )
				{
					SizeF fs = g.MeasureString( ctl.Text, ctl.Font);
					ctl.Height = (int) fs.Height + 8;	// 8 is for margins
					ctl.Width = (int) fs.Width + 12;		
				}
			}
			else
			{
				ctl.Image = bImg;
				ctl.ImageAlign = ContentAlignment.MiddleCenter;
				ctl.Height = bImg.Height + 5;
				ctl.Width = bImg.Width + 8;
				ctl.Text = "";
			}

			ctl.Tag = t;
			ctl.Left = x;
			ctl.Top = y;
			ctl.FlatStyle = FlatStyle.Flat;
			ToolTip tipb = new ToolTip();
			tipb.AutomaticDelay = 500;
			tipb.ShowAlways = true;
			tipb.SetToolTip(ctl, t);
			mainTB.Controls.Add(ctl);

			x = x+ ctl.Width;

			return ctl;
		}

		private int InitToolbarBold(int x, int y)
		{
			ctlBold = new SimpleToggle();
			ctlBold.UpColor = this.BackColor;

			ctlBold.Click +=new EventHandler(ctlBold_Click);
			
			ctlBold.Font = new Font("Courier New", 10, FontStyle.Bold);
			ctlBold.Text = "B";
			using ( Graphics g = ctlBold.CreateGraphics() )
			{
				SizeF fs = g.MeasureString( ctlBold.Text, ctlBold.Font );
				ctlBold.Height = (int) fs.Height + 6;	// 6 is for margins
				ctlBold.Width = ctlBold.Height;		
			}

			ctlBold.Tag = "bold";
			ctlBold.Left = x;
			ctlBold.Top = y;
			ctlBold.FlatStyle = FlatStyle.Flat;
			ToolTip tipb = new ToolTip();
			tipb.AutomaticDelay = 500;
			tipb.ShowAlways = true;
			tipb.SetToolTip(ctlBold, "Bold");
			mainTB.Controls.Add(ctlBold);

			return ctlBold.Width;
		}

		private int InitToolbarItalic(int x, int y)
		{
			ctlItalic = new SimpleToggle();
			ctlItalic.UpColor = this.BackColor;
			ctlItalic.Click +=new EventHandler(ctlItalic_Click);

			ctlItalic.Font = new Font("Courier New", 10, FontStyle.Italic | FontStyle.Bold);
			ctlItalic.Text = "I";
			using ( Graphics g = ctlItalic.CreateGraphics() )
			{
				SizeF fs = g.MeasureString( ctlItalic.Text, ctlItalic.Font );
				ctlItalic.Height = (int) fs.Height + 6;	// 6 is for margins
				ctlItalic.Width = ctlItalic.Height;		
			}

			ctlItalic.Tag = "italic";
			ctlItalic.Left = x;
			ctlItalic.Top = y;
			ctlItalic.FlatStyle = FlatStyle.Flat;
			ToolTip tipb = new ToolTip();
			tipb.AutomaticDelay = 500;
			tipb.ShowAlways = true;
			tipb.SetToolTip(ctlItalic, "Italic");
			mainTB.Controls.Add(ctlItalic);

			return ctlItalic.Width;
		}

		private int InitToolbarUnderline(int x, int y)
		{
			ctlUnderline = new SimpleToggle();
			ctlUnderline.UpColor = this.BackColor;
			ctlUnderline.Click +=new EventHandler(ctlUnderline_Click);
			ctlUnderline.Font = new Font("Courier New", 10, FontStyle.Underline | FontStyle.Bold);
			ctlUnderline.Text = "U";
			using ( Graphics g = ctlUnderline.CreateGraphics() )
			{
				SizeF fs = g.MeasureString( ctlUnderline.Text, ctlUnderline.Font );
				ctlUnderline.Height = (int) fs.Height + 6;	// 6 is for margins
				ctlUnderline.Width = ctlUnderline.Height;		
			}

			ctlUnderline.Tag = "italic";
			ctlUnderline.Left = x;
			ctlUnderline.Top = y;
			ctlUnderline.FlatStyle = FlatStyle.Flat;
			ToolTip tipb = new ToolTip();
			tipb.AutomaticDelay = 500;
			tipb.ShowAlways = true;
			tipb.SetToolTip(ctlUnderline, "Underline");
			mainTB.Controls.Add(ctlUnderline);

			return ctlUnderline.Width;
		}

		private int InitToolbarFont(int x, int y)
		{
			// Create the font
			ctlFont = new ComboBox();
			ctlFont.SelectedValueChanged +=new EventHandler(ctlFont_Change);
			ctlFont.Validated +=new EventHandler(ctlFont_Change);
			ctlFont.Left = x;
			ctlFont.Top = y;
			ctlFont.DropDownStyle = ComboBoxStyle.DropDown;

			foreach (FontFamily ff in FontFamily.Families)
			{
				ctlFont.Items.Add(ff.Name);
			}
			ToolTip tip = new ToolTip();
			tip.AutomaticDelay = 500;
			tip.ShowAlways = true;
			tip.SetToolTip(ctlFont, "Font");
			mainTB.Controls.Add(ctlFont);
			
			return ctlFont.Width;
		}

		private int InitToolbarFontSize(int x, int y)
		{
			// Create the font
			ctlFontSize = new ComboBox();
			ctlFontSize.SelectedValueChanged +=new EventHandler(ctlFontSize_Change);
			ctlFontSize.Validated +=new EventHandler(ctlFontSize_Change);
			ctlFontSize.Width = 42;
			ctlFontSize.Left = x;
			ctlFontSize.Top = y;
			ctlFontSize.DropDownStyle = ComboBoxStyle.DropDown;

			string[] sizes = new string[] {"8","9","10","11","12","14","16","18","20","22","24","26","28","36","48","72"};
			ctlFontSize.Items.AddRange(sizes);
			ToolTip tip = new ToolTip();
			tip.AutomaticDelay = 500;
			tip.ShowAlways = true;
			tip.SetToolTip(ctlFontSize, "Font Size");
			mainTB.Controls.Add(ctlFontSize);

			return ctlFontSize.Width;
		}

		private ComboBox InitToolbarColor(ref int x, int y, string t)
		{
			// Create the font
			ComboBox ctl = new ComboBox();
			ctl.Width = 100;
			ctl.Left = x;
			ctl.Top = y;
			ctl.Tag = t;
			ctl.DropDownStyle = ComboBoxStyle.DropDown;

			ctl.Items.AddRange(StaticLists.ColorList);
			ToolTip tip = new ToolTip();
			tip.AutomaticDelay = 500;
			tip.ShowAlways = true;
			tip.SetToolTip(ctl, t);
			mainTB.Controls.Add(ctl);

			x += ctl.Width;
			return ctl;
		}

		private ComboBox InitToolbarZoom(ref int x, int y)
		{
			ComboBox ctl = new ComboBox();
			ctl.Width = 85;
			ctl.Left = x;
			ctl.Top = y;
			ctl.Tag = "Zoom";
			ctl.DropDownStyle = ComboBoxStyle.DropDownList;

			ctl.Items.AddRange(StaticLists.ZoomList);
			ToolTip tip = new ToolTip();
			tip.AutomaticDelay = 500;
			tip.ShowAlways = true;
			tip.SetToolTip(ctl, "Zoom");
			mainTB.Controls.Add(ctl);

			x += ctl.Width;
			return ctl;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(RdlDesigner));
			this.bTable = new System.Windows.Forms.Button();
			this.bLine = new System.Windows.Forms.Button();
			this.bImage = new System.Windows.Forms.Button();
			this.bRectangle = new System.Windows.Forms.Button();
			this.bSubreport = new System.Windows.Forms.Button();
			this.bList = new System.Windows.Forms.Button();
			this.bChart = new System.Windows.Forms.Button();
			this.bText = new System.Windows.Forms.Button();
			this.bMatrix = new System.Windows.Forms.Button();
			this.bPrint = new System.Windows.Forms.Button();
			this.bSave = new System.Windows.Forms.Button();
			this.bOpen = new System.Windows.Forms.Button();
			this.bPaste = new System.Windows.Forms.Button();
			this.bCopy = new System.Windows.Forms.Button();
			this.bCut = new System.Windows.Forms.Button();
			this.bNew = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// bTable
			// 
			this.bTable.Image = ((System.Drawing.Image)(resources.GetObject("bTable.Image")));
			this.bTable.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.bTable.Location = new System.Drawing.Point(608, 32);
			this.bTable.Name = "bTable";
			this.bTable.TabIndex = 0;
			this.bTable.Text = "Table";
			this.bTable.Visible = false;
			// 
			// bLine
			// 
			this.bLine.Image = ((System.Drawing.Image)(resources.GetObject("bLine.Image")));
			this.bLine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.bLine.Location = new System.Drawing.Point(608, 288);
			this.bLine.Name = "bLine";
			this.bLine.TabIndex = 1;
			this.bLine.Text = "Line";
			this.bLine.Visible = false;
			// 
			// bImage
			// 
			this.bImage.Image = ((System.Drawing.Image)(resources.GetObject("bImage.Image")));
			this.bImage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.bImage.Location = new System.Drawing.Point(608, 256);
			this.bImage.Name = "bImage";
			this.bImage.TabIndex = 2;
			this.bImage.Text = "Image";
			this.bImage.Visible = false;
			// 
			// bRectangle
			// 
			this.bRectangle.Image = ((System.Drawing.Image)(resources.GetObject("bRectangle.Image")));
			this.bRectangle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.bRectangle.Location = new System.Drawing.Point(608, 224);
			this.bRectangle.Name = "bRectangle";
			this.bRectangle.TabIndex = 3;
			this.bRectangle.Text = "Rect";
			this.bRectangle.Visible = false;
			// 
			// bSubreport
			// 
			this.bSubreport.Image = ((System.Drawing.Image)(resources.GetObject("bSubreport.Image")));
			this.bSubreport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.bSubreport.Location = new System.Drawing.Point(608, 192);
			this.bSubreport.Name = "bSubreport";
			this.bSubreport.TabIndex = 4;
			this.bSubreport.Text = "Subreport";
			this.bSubreport.Visible = false;
			// 
			// bList
			// 
			this.bList.Image = ((System.Drawing.Image)(resources.GetObject("bList.Image")));
			this.bList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.bList.Location = new System.Drawing.Point(608, 160);
			this.bList.Name = "bList";
			this.bList.TabIndex = 5;
			this.bList.Text = "List";
			this.bList.Visible = false;
			// 
			// bChart
			// 
			this.bChart.Image = ((System.Drawing.Image)(resources.GetObject("bChart.Image")));
			this.bChart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.bChart.Location = new System.Drawing.Point(608, 128);
			this.bChart.Name = "bChart";
			this.bChart.TabIndex = 6;
			this.bChart.Text = "Chart";
			this.bChart.Visible = false;
			// 
			// bText
			// 
			this.bText.Image = ((System.Drawing.Image)(resources.GetObject("bText.Image")));
			this.bText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.bText.Location = new System.Drawing.Point(608, 96);
			this.bText.Name = "bText";
			this.bText.TabIndex = 7;
			this.bText.Text = "Text";
			this.bText.Visible = false;
			// 
			// bMatrix
			// 
			this.bMatrix.Image = ((System.Drawing.Image)(resources.GetObject("bMatrix.Image")));
			this.bMatrix.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.bMatrix.Location = new System.Drawing.Point(608, 64);
			this.bMatrix.Name = "bMatrix";
			this.bMatrix.TabIndex = 8;
			this.bMatrix.Text = "Matrix";
			this.bMatrix.Visible = false;
			// 
			// bPrint
			// 
			this.bPrint.Image = ((System.Drawing.Image)(resources.GetObject("bPrint.Image")));
			this.bPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.bPrint.Location = new System.Drawing.Point(504, 192);
			this.bPrint.Name = "bPrint";
			this.bPrint.TabIndex = 9;
			this.bPrint.Text = "Print";
			this.bPrint.Visible = false;
			// 
			// bSave
			// 
			this.bSave.Image = ((System.Drawing.Image)(resources.GetObject("bSave.Image")));
			this.bSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.bSave.Location = new System.Drawing.Point(504, 160);
			this.bSave.Name = "bSave";
			this.bSave.TabIndex = 10;
			this.bSave.Text = "Save";
			this.bSave.Visible = false;
			// 
			// bOpen
			// 
			this.bOpen.Image = ((System.Drawing.Image)(resources.GetObject("bOpen.Image")));
			this.bOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.bOpen.Location = new System.Drawing.Point(504, 128);
			this.bOpen.Name = "bOpen";
			this.bOpen.TabIndex = 11;
			this.bOpen.Text = "Open";
			this.bOpen.Visible = false;
			// 
			// bPaste
			// 
			this.bPaste.Image = ((System.Drawing.Image)(resources.GetObject("bPaste.Image")));
			this.bPaste.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.bPaste.Location = new System.Drawing.Point(504, 96);
			this.bPaste.Name = "bPaste";
			this.bPaste.TabIndex = 12;
			this.bPaste.Text = "Paste";
			this.bPaste.Visible = false;
			// 
			// bCopy
			// 
			this.bCopy.Image = ((System.Drawing.Image)(resources.GetObject("bCopy.Image")));
			this.bCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.bCopy.Location = new System.Drawing.Point(504, 64);
			this.bCopy.Name = "bCopy";
			this.bCopy.TabIndex = 13;
			this.bCopy.Text = "Copy";
			this.bCopy.Visible = false;
			// 
			// bCut
			// 
			this.bCut.Image = ((System.Drawing.Image)(resources.GetObject("bCut.Image")));
			this.bCut.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.bCut.Location = new System.Drawing.Point(504, 32);
			this.bCut.Name = "bCut";
			this.bCut.TabIndex = 14;
			this.bCut.Text = "Cut";
			this.bCut.Visible = false;
			// 
			// bNew
			// 
			this.bNew.Image = ((System.Drawing.Image)(resources.GetObject("bNew.Image")));
			this.bNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.bNew.Location = new System.Drawing.Point(504, 224);
			this.bNew.Name = "bNew";
			this.bNew.TabIndex = 15;
			this.bNew.Text = "New";
			this.bNew.Visible = false;
			// 
			// RdlDesigner
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(712, 470);
			this.Controls.Add(this.bNew);
			this.Controls.Add(this.bCut);
			this.Controls.Add(this.bCopy);
			this.Controls.Add(this.bPaste);
			this.Controls.Add(this.bOpen);
			this.Controls.Add(this.bSave);
			this.Controls.Add(this.bPrint);
			this.Controls.Add(this.bMatrix);
			this.Controls.Add(this.bText);
			this.Controls.Add(this.bChart);
			this.Controls.Add(this.bList);
			this.Controls.Add(this.bSubreport);
			this.Controls.Add(this.bRectangle);
			this.Controls.Add(this.bImage);
			this.Controls.Add(this.bLine);
			this.Controls.Add(this.bTable);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "RdlDesigner";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "fyiReporting Designer";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.ResumeLayout(false);

		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			bool bMono;
			string[] args = Environment.GetCommandLineArgs();
			if (args.Length >= 2 &&
				(args[1].ToLower() == "/m" || args[1].ToLower() == "-m"))
			{	// user want to run with mono simplifications
				bMono = true;
			}
			else
			{
				bMono = false;
				Application.EnableVisualStyles();
				Application.DoEvents();				// when Mono this goes into a loop
			}
			Application.Run(new RdlDesigner(bMono));
		}

		private void BuildMenus()
		{
			// FILE MENU
			menuNew = new MenuItem("&New Report...", new EventHandler(this.menuFileNewReport_Click), Shortcut.CtrlN);
			menuOpen = new MenuItem("&Open...", new EventHandler(this.menuFileOpen_Click), Shortcut.CtrlO);
			menuClose = new MenuItem("&Close", new EventHandler(this.menuFileClose_Click), Shortcut.CtrlW);
			menuFSep1 = new MenuItem("-");
			menuSave = new MenuItem("&Save", new EventHandler(this.menuFileSave_Click), Shortcut.CtrlS);
			menuSaveAs = new MenuItem("Save &As...", new EventHandler(this.menuFileSaveAs_Click));
			menuPrint = new MenuItem("Print...", new EventHandler(this.menuFilePrint_Click), Shortcut.CtrlP);
			menuFSep2 = new MenuItem("-");
			MenuItem menuRecentItem = new MenuItem("");
			menuRecentFile = new MenuItem("Recent &Files");
			menuRecentFile.MenuItems.AddRange(new MenuItem[] { menuRecentItem });
			menuFSep3 = new MenuItem("-");
			menuExit = new MenuItem("E&xit", new EventHandler(this.menuFileExit_Click), Shortcut.CtrlQ);
			   
			// Create file menu and add array of sub-menu items
			MenuItem menuFile = new MenuItem("&File");
			menuFile.Popup +=new EventHandler(this.menuFile_Popup);
			menuFile.MenuItems.AddRange(
				new MenuItem[] { menuNew, menuOpen, menuClose, menuFSep1, menuSave, menuSaveAs, 
								   menuPrint, menuFSep2, menuRecentFile, menuFSep3, menuExit });

			// Intialize the recent file menu
			RecentFilesMenu();

			// EDIT MENU
			menuEditUndo = new MenuItem("&Undo", new EventHandler(this.menuEditUndo_Click), Shortcut.CtrlZ);
			menuEditRedo = new MenuItem("&Redo", new EventHandler(this.menuEditRedo_Click), Shortcut.CtrlY);
			menuFSep1 = new MenuItem("-");
			menuEditCut = new MenuItem("Cu&t", new EventHandler(this.menuEditCut_Click), Shortcut.CtrlX);
			menuEditCopy = new MenuItem("&Copy", new EventHandler(this.menuEditCopy_Click), Shortcut.CtrlC);
			menuEditPaste = new MenuItem("&Paste", new EventHandler(this.menuEditPaste_Click), Shortcut.CtrlV);
			menuEditDelete = new MenuItem("&Delete", new EventHandler(this.menuEditDelete_Click), Shortcut.Del);
			menuFSep2 = new MenuItem("-");
			menuEditSelectAll = new MenuItem("Select &All", new EventHandler(this.menuEditSelectAll_Click), Shortcut.CtrlA);
			menuEditProperties = new MenuItem("Properties...", new EventHandler(this.menuEditProperties_Click));
			menuFSep3 = new MenuItem("-");
			menuEditFind = new MenuItem("&Find...", new EventHandler(this.menuEditFind_Click), Shortcut.CtrlF);
			menuEditFindNext = new MenuItem("Find Next", new EventHandler(this.menuEditFindNext_Click), Shortcut.F3);
			menuEditReplace = new MenuItem("&Replace...", new EventHandler(this.menuEditReplace_Click), Shortcut.CtrlH);
			menuEditGoto = new MenuItem("&Go To...", new EventHandler(this.menuEditGoto_Click), Shortcut.CtrlG);
			menuFSep4 = new MenuItem("-");
			menuEditFormatXml = new MenuItem("Format XM&L", new EventHandler(this.menuEdit_FormatXml), Shortcut.CtrlL);
			// Create edit menu and add array of sub-menu items
			menuEdit = new MenuItem("&Edit");
			menuEdit.Popup +=new EventHandler(this.menuEdit_Popup);
			
			menuEdit.MenuItems.AddRange(
				new MenuItem[] { menuEditUndo, menuEditRedo, menuFSep1, menuEditCut, menuEditPaste, 
								   menuEditDelete, menuFSep2, menuEditSelectAll, menuFSep3,
								   menuEditFind, menuEditFindNext, menuEditReplace, menuEditGoto,
								   menuFSep4, menuEditFormatXml});
			// Data Window
			menuNewDataSourceRef = new MenuItem("&Create Shared Data Source...", new EventHandler(this.menuFileNewDataSourceRef_Click));
			menuDataSources = new MenuItem("Data &Sources...", new EventHandler(this.menuDataSources_Click));
			menuDataSets = new MenuItem("&Data Sets");
			menuDataSets.MenuItems.Add(new MenuItem());	// this will actually get build dynamically

			menuEmbeddedImages = new MenuItem("&Embedded Images...", new EventHandler(this.menuEmbeddedImages_Click));
			menuData = new MenuItem("&Data");
			menuData.Popup +=new EventHandler(this.menuData_Popup);
			menuData.MenuItems.AddRange(
				new MenuItem[] { menuDataSets, menuDataSources, new MenuItem("-"), menuEmbeddedImages, new MenuItem("-"), menuNewDataSourceRef });

			// WINDOW MENU
			menuCascade = new MenuItem("&Cascade", new EventHandler(this.menuWndCascade_Click), Shortcut.CtrlShiftJ);

			menuTileH  = new MenuItem("&Horizontally", new EventHandler(this.menuWndTileH_Click), Shortcut.CtrlShiftK);
			menuTileV  = new MenuItem("&Vertically", new EventHandler(this.menuWndTileV_Click), Shortcut.CtrlShiftL);
			menuTile = new MenuItem("&Tile");
			menuTile.MenuItems.AddRange(new MenuItem[] { menuTileH, menuTileV });

			menuCloseAll = new MenuItem("Close &All", new EventHandler(this.menuWndCloseAll_Click), Shortcut.CtrlShiftW);

			// Add the Window menu
			MenuItem menuWindow = new MenuItem("&Window");
			menuWindow.Popup +=new EventHandler(this.menuWnd_Popup);
			menuWindow.MdiList = true;
			menuWindow.MenuItems.AddRange(new MenuItem[] { menuCascade, menuTile, menuCloseAll });

			// HELP MENU
			MenuItem menuAbout = new MenuItem("&About...", new EventHandler(this.menuHelpAbout_Click));
			MenuItem menuHelp = new MenuItem("&Help");
			menuHelp.MenuItems.AddRange(new MenuItem[] {menuAbout } );

			MainMenu menuMain = new MainMenu(new MenuItem[]{menuFile, menuEdit, menuData, menuWindow, menuHelp});	
			IsMdiContainer = true;
			this.Menu = menuMain;   
		}
  
		private bool OkToSave()
		{
			foreach (MDIChild mc in this.MdiChildren)
			{
				if (!mc.OkToClose())
					return false;
			}
			return true;
		}

		private void menuFile_Popup(object sender, EventArgs e)
		{
			// These menus require an MDIChild in order to work
			bool bEnable = this.MdiChildren.Length > 0? true: false;
			menuClose.Enabled = bEnable;
			menuSave.Enabled = bEnable;
			menuSaveAs.Enabled = bEnable;
			menuPrint.Enabled = bEnable;

			// Recent File is enabled when there exists some files 
			menuRecentFile.Enabled = this._RecentFiles.Count <= 0? false: true;
		}

		private void menuFileClose_Click(object sender, EventArgs e)
		{
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			if (mc != null)
				mc.Close();
		}

		private void menuFileExit_Click(object sender, EventArgs e)
		{
			if (!OkToSave())
				return;
			Environment.Exit(0);
		}

		private void menuFileOpen_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "Report files (*.rdl)|*.rdl|" +
				"All files (*.*)|*.*";
			ofd.FilterIndex = 1;
			ofd.CheckFileExists = true;
			ofd.Multiselect = true;
			if (ofd.ShowDialog(this) == DialogResult.OK)
			{
				foreach (string file in ofd.FileNames)
				{
					CreateMDIChild(file, null, false);
				}
				RecentFilesMenu();		// update the menu for recent files
			}
		}

		// Create an MDI child.   Only creates it if not already open.
		private MDIChild CreateMDIChild(string file, string rdl, bool bMenuUpdate)
		{
			MDIChild mcOpen=null;
			if (file != null)
			{
				file = file.Trim();

				foreach (MDIChild mc in this.MdiChildren)
				{
					if (mc.SourceFile != null && file == mc.SourceFile.Trim())	
					{							// we found it
						mcOpen = mc;
						break;
					}
				}
			}
			if (mcOpen == null)
			{
				MDIChild mc = new MDIChild(this.ClientRectangle.Width*3/5, this.ClientRectangle.Height*3/5);
				mc.OnSelectionChanged += new MDIChild.RdlChangeHandler(SelectionChanged);
				mc.OnSelectionMoved += new MDIChild.RdlChangeHandler(SelectionMoved);
				mc.OnReportItemInserted += new MDIChild.RdlChangeHandler(ReportItemInserted);
				mc.OnDesignTabChanged += new MDIChild.RdlChangeHandler(DesignTabChanged);
				mc.WindowState=FormWindowState.Maximized;//added by js
				mc.MdiParent = this;
				mc.Viewer.GetDataSourceReferencePassword = _GetPassword;
				if (file != null)
				{
					mc.Viewer.Folder = Path.GetDirectoryName(file);
					mc.SourceFile = file;
					mc.Text = Path.GetFileName(file);
					mc.Viewer.Folder = Path.GetDirectoryName(file);
					mc.Viewer.ReportName = Path.GetFileNameWithoutExtension(file);
					NoteRecentFiles(file, bMenuUpdate);
				}
				else
				{
					mc.SourceRdl = rdl;
					mc.Viewer.ReportName = mc.Text = "Untitled";
				}
				mc.Show();
				mcOpen = mc;
			}
			else
				mcOpen.Activate();
			return mcOpen;
		}

		private void DesignTabChanged(object sender, System.EventArgs e)
		{
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			string tab="";
			if (mc != null)
				tab = mc.DesignTab;
			bool bEnableEdit=false;
			bool bEnableDesign=false;
			bool bEnablePreview=false;
			switch (tab)
			{
				case "edit":
					bEnableEdit=true;
					break;
				case "design":
					bEnableDesign=true;
					break;
				case "preview":
					bEnablePreview=true;
					break;
			}
			if (ctlBold != null)
				ctlBold.Enabled = bEnableDesign;
			if (ctlItalic != null)
				ctlItalic.Enabled = bEnableDesign;
			if (ctlUnderline != null)
				ctlUnderline.Enabled = bEnableDesign;
			if (ctlFont != null)
				ctlFont.Enabled = bEnableDesign;
			if (ctlFontSize != null)
				ctlFontSize.Enabled = bEnableDesign;
			if (ctlForeColor != null)
				ctlForeColor.Enabled = bEnableDesign;
			if (ctlBackColor != null)
				ctlBackColor.Enabled = bEnableDesign;
			if (ctlCut != null)
				ctlCut.Enabled = bEnableDesign | bEnableEdit;
			if (ctlCopy != null)
				ctlCopy.Enabled = bEnableDesign | bEnableEdit;
			if (ctlPaste != null)
				ctlPaste.Enabled = bEnableDesign | bEnableEdit;
			if (ctlPrint != null)
				ctlPrint.Enabled = bEnablePreview;

			if (ctlInsertTextbox != null)
				ctlInsertTextbox.Enabled = bEnableDesign;
			if (ctlInsertChart != null)
				ctlInsertChart.Enabled = bEnableDesign;
			if (ctlInsertRectangle != null)
				ctlInsertRectangle.Enabled = bEnableDesign;
			if (ctlInsertTable != null)
				ctlInsertTable.Enabled = bEnableDesign;
			if (ctlInsertMatrix != null)
				ctlInsertMatrix.Enabled = bEnableDesign;
			if (ctlInsertList != null)
				ctlInsertList.Enabled = bEnableDesign;
			if (ctlInsertLine != null)
				ctlInsertLine.Enabled = bEnableDesign;
			if (ctlInsertImage != null)
				ctlInsertImage.Enabled = bEnableDesign;
			if (ctlInsertSubreport != null)
				ctlInsertSubreport.Enabled = bEnableDesign;
			if (ctlZoom != null)
			{
				ctlZoom.Enabled = bEnablePreview;
				string zText="Actual Size";
				if (mc != null)
				{
					switch(mc.ZoomMode)
					{
						case ZoomEnum.FitWidth:
							zText = "Fit Width";
							break;
						case ZoomEnum.FitPage:
							zText = "Fit Page";
							break;
						case ZoomEnum.UseZoom:
							if (mc.Zoom == 1)
								zText = "Actual Size";
							else
								zText = string.Format("{0:0}", mc.Zoom*100f);
							break;
					}
					ctlZoom.Text = zText;
				}
			}
			// when no active sheet
			this.ctlSave.Enabled = mc != null;
			
			// Update the status and position information
			SetStatusNameAndPosition();
		}

		private void ReportItemInserted(object sender, System.EventArgs e)
		{
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			if (mc == null)
				return;

			// turn off the current selection after an item is inserted
			if (ctlInsertCurrent != null)
			{
				ctlInsertCurrent.Checked = false;
				mc.CurrentInsert = null;
				ctlInsertCurrent = null;
			}
		}
		
		private void SelectionMoved(object sender, System.EventArgs e)
		{
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			if (mc == null)
				return;

			SetStatusNameAndPosition();
		}
  
		private void SelectionChanged(object sender, System.EventArgs e)
		{
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			if (mc == null)
				return;

			bSuppressChange = true;			// don't process changes in status bar

			SetStatusNameAndPosition();

			StyleInfo si = mc.SelectedStyle;
			if (si == null)
				return;

			if (ctlBold != null)
				ctlBold.Checked = si.IsFontBold()? true: false;
			if (ctlItalic != null)
				ctlItalic.Checked = si.FontStyle == FontStyleEnum.Italic? true: false;
			if (ctlUnderline != null)
				ctlUnderline.Checked = si.TextDecoration == TextDecorationEnum.Underline? true: false;
			if (ctlFont != null)
				ctlFont.Text = si.FontFamily;
			if (ctlFontSize != null)
			{
				string rs = string.Format(NumberFormatInfo.InvariantInfo, "{0:0.#}", si.FontSize);
				ctlFontSize.Text = rs;
			}
			if (ctlForeColor != null)
				ctlForeColor.Text = ColorTranslator.ToHtml(si.Color);
			if (ctlBackColor != null)
				ctlBackColor.Text = ColorTranslator.ToHtml(si.BackgroundColor);

			bSuppressChange = false;
		}

		private void menuData_Popup(object sender, EventArgs ea)
		{
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			bool bEnable = false;
			if (mc != null && mc.DesignTab == "design")
				bEnable = true;

			this.menuDataSources.Enabled = this.menuDataSets.Enabled = this.menuEmbeddedImages.Enabled = bEnable;
			if (!bEnable)
				return;

			// Run thru all the existing DataSets
			menuDataSets.MenuItems.Clear();
			menuDataSets.MenuItems.Add(new MenuItem("New...", 
						new EventHandler(this.menuDataSets_Click)));

			DesignXmlDraw draw = mc.DrawCtl;
			XmlNode rNode = draw.GetReportNode();
			XmlNode dsNode = draw.GetNamedChildNode(rNode, "DataSets");
			if (dsNode != null)
			{
				foreach (XmlNode dNode in dsNode)
				{	
					if (dNode.Name != "DataSet")
						continue;
					XmlAttribute nAttr = dNode.Attributes["Name"];
					if (nAttr == null)	// shouldn't really happen
						continue;
					menuDataSets.MenuItems.Add(new MenuItem(nAttr.Value, 
						new EventHandler(this.menuDataSets_Click)));
				}
			}
		}
 
		private void menuDataSources_Click(object sender, System.EventArgs e)
		{
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			if (mc == null)
				return;

			DialogDataSources dlgDS = new DialogDataSources(mc.DrawCtl);
			dlgDS.StartPosition = FormStartPosition.CenterParent;
			DialogResult dr = dlgDS.ShowDialog();
			if (dr == DialogResult.OK)
				mc.Modified = true;
		}

		private void menuDataSets_Click(object sender, System.EventArgs e)
		{
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			if (mc == null || mc.DrawCtl == null || mc.ReportDocument == null)
				return;

			MenuItem menu = sender as MenuItem;
			if (menu == null)
				return;
			string dsname = menu.Text;

			// Find the dataset we need
			ArrayList ds = new ArrayList();
			DesignXmlDraw draw = mc.DrawCtl;
			XmlNode rNode = draw.GetReportNode();
			XmlNode dsNode = draw.GetCreateNamedChildNode(rNode, "DataSets");
			XmlNode dataset=null;
			{
				foreach (XmlNode dNode in dsNode)
				{	
					if (dNode.Name != "DataSet")
						continue;
					XmlAttribute nAttr = dNode.Attributes["Name"];
					if (nAttr == null)	// shouldn't really happen
						continue;
					if (nAttr.Value == dsname)
					{
						dataset = dNode;
						break;	
					}
				}
			}
			bool bNew = false;
			if (dataset == null)	// This must be the new menu item
			{
				dataset = draw.CreateElement(dsNode, "DataSet", null);	// create empty node
				bNew = true;
			}
			ds.Add(dataset);

			PropertyDialog pd = new PropertyDialog(mc.DrawCtl, ds, PropertyTypeEnum.DataSets);
			DialogResult dr = pd.ShowDialog();
			if (pd.Changed || dr == DialogResult.OK)
			{
				mc.Modified = true;
			}
			else if (bNew)	// if canceled and new DataSet get rid of temp node
			{
				dsNode.RemoveChild(dataset);
			}
			if (pd.Delete)	// user must have hit a delete button for this to get set
				dsNode.RemoveChild(dataset);

			if (!dsNode.HasChildNodes)		// If no dataset exists we remove DataSets
				draw.RemoveElement(rNode, "DataSets");
		}
 
		private void menuEmbeddedImages_Click(object sender, System.EventArgs e)
		{
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			if (mc == null)
				return;

			DialogEmbeddedImages dlgEI = new DialogEmbeddedImages(mc.DrawCtl);
			dlgEI.StartPosition = FormStartPosition.CenterParent;
			DialogResult dr = dlgEI.ShowDialog();
			if (dr == DialogResult.OK)
				mc.Modified = true;
		}

		private void menuFileNewDataSourceRef_Click(object sender, System.EventArgs e)
		{
			DialogDataSourceRef dlgDS = new DialogDataSourceRef();
			dlgDS.StartPosition = FormStartPosition.CenterParent;
			dlgDS.ShowDialog();
			if (dlgDS.DialogResult == DialogResult.Cancel)
				return;
		}

		private void menuFileNewReport_Click(object sender, System.EventArgs e)
		{
			DialogDatabase dlgDB = new DialogDatabase();
			dlgDB.StartPosition = FormStartPosition.CenterParent;
			dlgDB.FormBorderStyle = FormBorderStyle.SizableToolWindow;

			// show modally
			dlgDB.ShowDialog();
			if (dlgDB.DialogResult == DialogResult.Cancel)
				return;
			string rdl = dlgDB.ResultReport;

			// Create the MDI child using the RDL syntax the wizard generates
			MDIChild mc = CreateMDIChild(null, rdl, false);
			mc.Modified = true;
		}

		private void menuFilePrint_Click(object sender, EventArgs e)
		{
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			if (mc == null)
				return;
			if (printChild != null)			// already printing
			{
				MessageBox.Show("Can only print one file at a time.", "RDL Design");
				return;
			}

			printChild = mc;

			PrintDocument pd = new PrintDocument();
			pd.DocumentName = mc.SourceFile;
			pd.PrinterSettings.FromPage = 1;
			pd.PrinterSettings.ToPage = mc.PageCount;
			pd.PrinterSettings.MaximumPage = mc.PageCount;
			pd.PrinterSettings.MinimumPage = 1;
			pd.DefaultPageSettings.Landscape = mc.PageWidth > mc.PageHeight? true: false;

			PrintDialog dlg = new PrintDialog();
			dlg.Document = pd;
			dlg.AllowSelection = true;
			dlg.AllowSomePages = true;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				try
				{
					if (pd.PrinterSettings.PrintRange == PrintRange.Selection)
					{
						pd.PrinterSettings.FromPage = mc.PageCurrent;
					}
					mc.Print(pd);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Print error: " + ex.Message, "RDL Design");
				}
			}
			printChild = null;
		}

		private void menuFileSave_Click(object sender, EventArgs e)
		{
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			if (mc == null)
				return;

			if (!mc.FileSave())
				return;

			NoteRecentFiles(mc.SourceFile, true);

			return;
		}

		private void menuFileSaveAs_Click(object sender, EventArgs e)
		{
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			if (mc == null)
				return;

			if (!mc.FileSaveAs())
				return;

			mc.Viewer.Folder = Path.GetDirectoryName(mc.SourceFile);
			mc.Viewer.ReportName = Path.GetFileNameWithoutExtension(mc.SourceFile);
			mc.Text = Path.GetFileName(mc.SourceFile);
			
			NoteRecentFiles(mc.SourceFile, true);

			return;
		}

		private void menuEdit_Popup(object sender, EventArgs ea)
		{
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			// These menus require an MDIChild in order to work
			RdlEditPreview e = GetEditor();
			menuEdit.MenuItems.Clear();
			if (e == null || e.DesignTab != "edit")
			{
				menuEdit.MenuItems.AddRange(
					new MenuItem[] { /* menuEditUndo, menuEditRedo, menuFSep1, */ menuEditCut, menuEditCopy,
									   menuEditPaste, menuEditDelete, menuFSep2, menuEditSelectAll, new MenuItem("-"), menuEditProperties});

				if (mc == null || e == null)
				{
					menuEditUndo.Enabled = menuEditRedo.Enabled = menuEditCut.Enabled = menuEditCopy.Enabled = 
						menuEditPaste.Enabled = menuEditDelete.Enabled = menuEditSelectAll.Enabled =
						menuEditProperties.Enabled = false;
					return;
				}
				menuEditProperties.Enabled = true;
			}
			else
			{
				menuEdit.MenuItems.AddRange(
					new MenuItem[] { menuEditUndo, menuEditRedo, menuFSep1, menuEditCut, menuEditCopy,
									   menuEditPaste, menuEditDelete, menuFSep2, menuEditSelectAll, menuFSep3,
									   menuEditFind, menuEditFindNext, menuEditReplace, menuEditGoto,
									   menuFSep4, menuEditFormatXml});
			}
			menuEditUndo.Enabled = e.CanUndo;
			menuEditRedo.Enabled = e.CanRedo;
			bool bSelection = e.SelectionLength > 0;	// any text selected?
			menuEditCut.Enabled = bSelection;
			menuEditCopy.Enabled = bSelection;
			menuEditPaste.Enabled = Clipboard.GetDataObject().GetDataPresent(DataFormats.Text);
			menuEditDelete.Enabled = bSelection;
			menuEditSelectAll.Enabled = true;

			bool bAnyText = e.Text.Length > 0;			// any text to search at all?
			menuEditFind.Enabled = menuEditFindNext.Enabled = 
				menuEditReplace.Enabled = menuEditGoto.Enabled = bAnyText;
		}

		private void menuEditUndo_Click(object sender, System.EventArgs ea)
		{
			RdlEditPreview e = GetEditor();
			if (e == null)
				return;

			if (e.CanUndo == true)
			{
				e.Undo();
			}
		}

		private void menuEditRedo_Click(object sender, System.EventArgs ea)
		{
			RdlEditPreview e = GetEditor();
			if (e == null)
				return;

			if (e.CanRedo == true)
			{
				e.Redo();
			}
		}

		private void menuEditCut_Click(object sender, System.EventArgs ea)
		{
			RdlEditPreview e = GetEditor();
			if (e == null)
				return;

			if (e.SelectionLength > 0)
				e.Cut();
		}

		private void menuEditCopy_Click(object sender, System.EventArgs ea)
		{
			RdlEditPreview e = GetEditor();
			if (e == null)
				return;

			if (e.SelectionLength > 0)
				e.Copy();
		}

		private void menuEditPaste_Click(object sender, System.EventArgs ea)
		{
			RdlEditPreview e = GetEditor();
			if (e == null)
				return;

			if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text) == true ||
				Clipboard.GetDataObject().GetDataPresent(DataFormats.Bitmap) == true)
				e.Paste();
		}

		private void menuEditDelete_Click(object sender, System.EventArgs ea)
		{
			RdlEditPreview e = GetEditor();
			if (e == null)
				return;

			if (e.SelectionLength > 0)
				e.SelectedText="";
		}

		private void menuEditProperties_Click(object sender, System.EventArgs ea)
		{
			RdlEditPreview e = GetEditor();
			if (e == null)
				return;

			e.DesignCtl.menuProperties_Click();
		}

		private void menuEditSelectAll_Click(object sender, System.EventArgs ea)
		{
			RdlEditPreview e = GetEditor();
			if (e == null)
				return;

			e.SelectAll();
		}

		private void menuEditFind_Click(object sender, System.EventArgs ea)
		{
			RdlEditPreview e = GetEditor();
			if (e == null)
				return;

			FindTab tab = new FindTab(e);
			tab.Show();
		}

		private void menuEditFindNext_Click(object sender, System.EventArgs ea)
		{
			RdlEditPreview e = GetEditor();
			if (e == null)
				return;

			FindTab tab = new FindTab(e);
			tab.Show();		
		} 

		private void menuEdit_FormatXml(object sender, System.EventArgs ea)
		{
			RdlEditPreview e = GetEditor();
			if (e == null)
				return;

			if (e.Text.Length > 0)
			{
				try
				{
					e.Text = DesignerUtility.FormatXml(e.Text);
					e.Modified = true;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Format XML");
				}
			}
		} 

		private void menuEditReplace_Click(object sender, System.EventArgs ea)
		{
			RdlEditPreview e = GetEditor();
			if (e == null)
				return;
			FindTab tab = new FindTab(e);
			tab.tcFRG.SelectedTab = tab.tabReplace;
			tab.Show();		
		}

		private void menuEditGoto_Click(object sender, System.EventArgs ea)
		{
			RdlEditPreview e = GetEditor();
			if (e == null)
				return;

			FindTab tab = new FindTab(e);
			tab.tcFRG.SelectedTab = tab.tabGoTo;
			tab.Show();
		}

		private void menuHelpAbout_Click(object sender, System.EventArgs ea)
		{
			DialogAbout dlg = new DialogAbout();
			dlg.ShowDialog();
		}

		private RdlEditPreview GetEditor()
		{
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			if (mc == null)
				return null;
			return mc.Editor;
		}

		private void menuWnd_Popup(object sender, EventArgs e)
		{
			// These menus require an MDIChild in order to work
			bool bEnable = this.MdiChildren.Length > 0? true: false;

			menuCascade.Enabled = bEnable;
			menuTile.Enabled = bEnable;
			menuCloseAll.Enabled = bEnable;
		}

		private void menuWndCascade_Click(object sender, EventArgs e)
		{
			this.LayoutMdi(MdiLayout.Cascade);
		}

		private void menuWndCloseAll_Click(object sender, EventArgs e)
		{
			foreach (Form f in this.MdiChildren)
			{
				f.Close();
			}
		}

		private void menuWndTileH_Click(object sender, EventArgs e)
		{
			this.LayoutMdi(MdiLayout.TileHorizontal);
		}

		private void menuWndTileV_Click(object sender, EventArgs e)
		{
			this.LayoutMdi(MdiLayout.TileVertical);
		}

		private void menuRecentItem_Click(object sender, System.EventArgs e)
		{
			MenuItem m = (MenuItem) sender;
			string file = m.Text.Substring(2);

			CreateMDIChild(file, null, true);
		}
				
		private void DoPropertyDialog(PropertyTypeEnum type)
		{
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			if (mc == null || mc.DrawCtl == null || mc.ReportDocument == null)
				return;

			PropertyDialog pd = new PropertyDialog(mc.DrawCtl, null, type);
			DialogResult dr = pd.ShowDialog();
			if (pd.Changed || dr == DialogResult.OK)
			{
				mc.Modified = true;
			}
		}

		private void RdlDesigner_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			SaveStartupState();
		}
 
		private void NoteRecentFiles(string name, bool bResetMenu)
		{
			if (name == null)
				return;

			name = name.Trim();
			if (_RecentFiles.ContainsValue(name))
			{	// need to move it to top of list; so remove old one
				int loc = _RecentFiles.IndexOfValue(name);
				_RecentFiles.RemoveAt(loc);
			}
			if (_RecentFiles.Count >= 5)
			{
				_RecentFiles.RemoveAt(0);	// remove the first entry
			}
			_RecentFiles.Add(DateTime.Now, name);
			if (bResetMenu)
				RecentFilesMenu();
			return;
		}

		private void RecentFilesMenu()
		{
			menuRecentFile.MenuItems.Clear();
			int mi = 1;
			for (int i=_RecentFiles.Count-1; i >= 0; i--)
			{
				string menuText = string.Format("&{0} {1}", mi++, (string) (_RecentFiles.GetValueList()[i]));
				MenuItem m = new MenuItem(menuText);
				m.Click += new EventHandler(this.menuRecentItem_Click);
				menuRecentFile.MenuItems.Add(m);
			}
		}
 
		private string GetPassword()
		{
			if (bGotPassword)
				return _DataSourceReferencePassword;

			DataSourcePassword dlg = new DataSourcePassword();
			DialogResult rc = dlg.ShowDialog();
			bGotPassword = true;
			if (rc == DialogResult.OK)
				_DataSourceReferencePassword = dlg.PassPhrase;

			return _DataSourceReferencePassword;
		}

		private void GetStartupState()
		{
			string optFileName = AppDomain.CurrentDomain.BaseDirectory + "designerstate.xml";
			_RecentFiles = new SortedList();
			_CurrentFiles = new ArrayList();
			
			try
			{
				XmlDocument xDoc = new XmlDocument();
				xDoc.PreserveWhitespace = false;
				xDoc.Load(optFileName);
				XmlNode xNode;
				xNode = xDoc.SelectSingleNode("//designerstate");

				string[] args = Environment.GetCommandLineArgs();
				for (int i=1; i < args.Length; i++)
				{
					string larg = args[i].ToLower();
					if (larg == "/m" || larg == "-m")
						continue;

					if (File.Exists(args[i]))			// only add it if it exists
						_CurrentFiles.Add(args[i]);
				}

				// Loop thru all the child nodes
				foreach(XmlNode xNodeLoop in xNode.ChildNodes)
				{
					switch (xNodeLoop.Name)
					{
						case "RecentFiles":
							DateTime now = DateTime.Now;
							now = now.Subtract(new TimeSpan(0,1,0,0,0));	// subtract an hour
							foreach (XmlNode xN in xNodeLoop.ChildNodes)
							{
								string file = xN.InnerText.Trim();
								if (File.Exists(file))			// only add it if it exists
								{
									_RecentFiles.Add(now, file);
									now = now.AddSeconds(1);
								}
							}
							break;
						case "CurrentFiles":
							if (_CurrentFiles.Count > 0)	// don't open other current files if opened with argument
								break;
							foreach (XmlNode xN in xNodeLoop.ChildNodes)
							{
								string file = xN.InnerText.Trim();
								if (File.Exists(file))			// only add it if it exists
									_CurrentFiles.Add(file);
							}
							break;
						default:
							break;
					}
				}
			}
			catch (Exception ex)
			{		// Didn't sucessfully get the startup state but don't really care
				Console.WriteLine(string.Format("Exception in GetStartupState ignored.\n{0}\n{1}", ex.Message, ex.StackTrace));
			}

			return;
		}

		private void SaveStartupState()
		{
			try
			{
				int[] colors = GetCustomColors();		// get custom colors

				XmlDocument xDoc = new XmlDocument();
				XmlProcessingInstruction xPI;
				xPI = xDoc.CreateProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
				xDoc.AppendChild(xPI);

				XmlNode xDS = xDoc.CreateElement("designerstate");
				xDoc.AppendChild(xDS);

				XmlNode xN;
				// Loop thru the current files
				XmlNode xFiles = xDoc.CreateElement("CurrentFiles");
				xDS.AppendChild(xFiles);
				foreach (MDIChild mc in this.MdiChildren)
				{
					string file = mc.SourceFile;
					if (file == null)
						continue;
					xN = xDoc.CreateElement("file");
					xN.InnerText = file;
					xFiles.AppendChild(xN);
				}

				// Loop thru recent files list
				xFiles = xDoc.CreateElement("RecentFiles");
				xDS.AppendChild(xFiles);
				foreach(string f in _RecentFiles.Values)
				{
					xN = xDoc.CreateElement("file");
					xN.InnerText = f;
					xFiles.AppendChild(xN);
				}

				// Save the custom colors
				StringBuilder sb = new StringBuilder();
				foreach (int c in colors)
				{
					sb.Append(c.ToString());
					sb.Append(",");
				}
				sb.Remove(sb.Length-1, 1);	// remove last ","
				
				xN = xDoc.CreateElement("CustomColors");
				xN.InnerText = sb.ToString();
				xDS.AppendChild(xN);

				// Save the file
				string optFileName = AppDomain.CurrentDomain.BaseDirectory + "designerstate.xml";

				xDoc.Save(optFileName);
			}
			catch{}		// still want to leave even on error

			return ;
		}

		static internal int[] GetCustomColors()
		{
			string optFileName = AppDomain.CurrentDomain.BaseDirectory + "designerstate.xml";
			int white = 16777215;	// default to white (magic number)
			int[] cArray = new int[] {white, white, white, white,white, white, white, white,
								    white, white, white, white, white, white, white, white};
			try
			{
				XmlDocument xDoc = new XmlDocument();
				xDoc.PreserveWhitespace = false;
				xDoc.Load(optFileName);
				XmlNode xNode;
				xNode = xDoc.SelectSingleNode("//designerstate");

				string tcolors="";
				// Loop thru all the child nodes
				foreach(XmlNode xNodeLoop in xNode.ChildNodes)
				{
					if (xNodeLoop.Name != "CustomColors")
						continue;
					tcolors = xNodeLoop.InnerText;
					break;
				}
				string[] colorList= tcolors.Split(',');
				int i=0;

				foreach (string c in colorList)
				{
					try {cArray[i] = int.Parse(c); }
					catch {cArray[i] = white;}
					i++;
					if (i >= cArray.Length)		// Only allow 16 custom colors
						break;
				}
			}
			catch
			{		// Didn't sucessfully get the startup state but don't really care
			}
			return cArray;
		}

		static internal void SetCustomColors(int[] colors)
		{
			string optFileName = AppDomain.CurrentDomain.BaseDirectory + "designerstate.xml";

			StringBuilder sb = new StringBuilder();
			foreach (int c in colors)
			{
				sb.Append(c.ToString());
				sb.Append(",");
			}

			sb.Remove(sb.Length-1, 1);	// remove last ","
			try
			{
				XmlDocument xDoc = new XmlDocument();
				xDoc.PreserveWhitespace = false;
				xDoc.Load(optFileName);
				XmlNode xNode;
				xNode = xDoc.SelectSingleNode("//designerstate");

				// Loop thru all the child nodes
				XmlNode cNode = null;
				foreach(XmlNode xNodeLoop in xNode.ChildNodes)
				{
					if (xNodeLoop.Name == "CustomColors")
					{
						cNode = xNodeLoop;
						break;
					}
				}

				if (cNode == null)
				{
					cNode = xDoc.CreateElement("CustomColors");
					xNode.AppendChild(cNode);
				}

				cNode.InnerText = sb.ToString();

				xDoc.Save(optFileName);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Custom Color Save Failed");
			}
			return;
		}

		private void Insert_Click(object sender, EventArgs e)
		{
			if (ctlInsertCurrent != null)
				ctlInsertCurrent.Checked = false;

			SimpleToggle ctl = (SimpleToggle) sender;
			ctlInsertCurrent = ctl.Checked? ctl: null;

			MDIChild mc = this.ActiveMdiChild as MDIChild;
			if (mc == null)
				return;
			mc.SetFocus();

			mc.CurrentInsert = ctlInsertCurrent == null? null: (string) ctlInsertCurrent.Tag;		
		}

		private void ctlBold_Click(object sender, EventArgs e)
		{
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			if (mc == null)
				return;
			mc.SetFocus();
			
			mc.ApplyStyleToSelected("FontWeight", ctlBold.Checked? "Bold": "Normal");
		}

		private void ctlItalic_Click(object sender, EventArgs e)
		{
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			if (mc == null)
				return;
			mc.SetFocus();
			
			mc.ApplyStyleToSelected("FontStyle", ctlItalic.Checked? "Italic": "Normal");
		}

		private void ctlUnderline_Click(object sender, EventArgs e)
		{
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			if (mc == null)
				return;
			mc.SetFocus();
			
			mc.ApplyStyleToSelected("TextDecoration", ctlUnderline.Checked? "Underline": "None");
		}

		private void ctlForeColor_Change(object sender, EventArgs e)
		{
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			if (mc == null)
				return;
			mc.SetFocus();
			
			if (!bSuppressChange)
				mc.ApplyStyleToSelected("Color", ctlForeColor.Text);
		}

		private void ctlBackColor_Change(object sender, EventArgs e)
		{
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			if (mc == null)
				return;
			mc.SetFocus();
			if (!bSuppressChange)
				mc.ApplyStyleToSelected("BackgroundColor", ctlBackColor.Text);
		}

		private void ctlFont_Change(object sender, EventArgs e)
		{
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			if (mc == null)
				return;
			mc.SetFocus();
			
			if (!bSuppressChange)
				mc.ApplyStyleToSelected("FontFamily", ctlFont.Text);
		}

		private void ctlFontSize_Change(object sender, EventArgs e)
		{
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			if (mc == null)
				return;
			mc.SetFocus();
			
			if (!bSuppressChange)
				mc.ApplyStyleToSelected("FontSize", ctlFontSize.Text + "pt");
		}

		private void ctlZoom_Change(object sender, EventArgs e)
		{
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			if (mc == null)
				return;
			mc.SetFocus();
			
			switch (ctlZoom.Text)
			{
				case "Actual Size":
					mc.Zoom = 1;
					break;
				case "Fit Page":
					mc.ZoomMode = ZoomEnum.FitPage;
					break;
				case "Fit Width":
					mc.ZoomMode = ZoomEnum.FitWidth;
					break;
				default:
					string s = ctlZoom.Text.Substring(0, ctlZoom.Text.Length-1);
					float z;
					try
					{
						z = Convert.ToSingle(s) / 100f;
						mc.Zoom = z;
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "Zoom Value Invalid");
					}
					break;
			}
		}

		private void RdlDesigner_MdiChildActivate(object sender, EventArgs e)
		{
			DesignTabChanged(sender, e);
			SelectionChanged(sender, e);
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			if (mc == null)
				return;
			mc.SetFocus();
		}
		
		private void SetStatusNameAndPosition()
		{
			MDIChild mc = this.ActiveMdiChild as MDIChild;
			if (mc == null || mc.DesignTab != "design" || mc.DrawCtl.SelectedCount <= 0)
			{
				statusPosition.Text = "";
				statusSelected.Text = "";
				return;
			}

			// Handle position
			PointF pos = mc.SelectionPosition;
			string spos;
			if (pos.X == float.MinValue)
				spos = "";
			else
			{
				RegionInfo rinfo = new RegionInfo( CultureInfo.CurrentCulture.LCID );
				if (rinfo.IsMetric)
					spos =  string.Format("   x={0:0.00}cm, y={1:0.00}cm        ", pos.X / (72 / 2.54d), pos.Y / (72 / 2.54d));
				else
					spos =  string.Format("   x={0:0.00}\", y={1:0.00}\"        ", pos.X/72, pos.Y/72);
			}
			if (spos != statusPosition.Text)
				statusPosition.Text = spos;

			// Handle text
			string sname = mc.SelectionName;
			if (sname != statusSelected.Text)
				statusSelected.Text = sname;
			return;
		}
	}
}
