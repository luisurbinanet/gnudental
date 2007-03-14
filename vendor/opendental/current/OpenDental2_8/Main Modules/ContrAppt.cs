/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
//using System.G
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Drawing.Printing;


namespace OpenDental{

	///<summary></summary>
	public class ContrAppt : System.Windows.Forms.UserControl{
		private OpenDental.ContrApptSheet ContrApptSheet2;
		private ContrApptSingle[] ContrApptSingle3;//the '3' has no significance
		private System.Windows.Forms.MonthCalendar Calendar2;
		private System.Windows.Forms.Label labelDate;
		private System.Windows.Forms.Label labelDate2;
		private System.ComponentModel.IContainer components;// Required designer variable.
		private bool mouseIsDown=false;
		private Point mouseOrigin = new Point();
		private Point contOrigin = new Point();
		private ContrApptSingle TempApptSingle;
		private ContrApptSingle PinApptSingle;
		private System.Windows.Forms.ImageList imageListMain;
		private System.Windows.Forms.ToolBarButton toolBarButUnsched;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ToolBarButton toolBarButRecall;
		private bool boolAptMoved=false;
		//private FormUnsched FormUnsched2;
		private System.Windows.Forms.Button butToday;
		private System.Windows.Forms.Button butTodayWk;
		private System.Windows.Forms.Panel panelPinBoard;
		//private FormRecall FormRecall2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Panel panelSheet;
		private System.Windows.Forms.Panel panelCalendar;
		private System.Windows.Forms.Panel panelArrows;
		private System.Windows.Forms.VScrollBar vScrollBar1;
		private System.Windows.Forms.Panel panelOps;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ListBox listConfirmed;
		private System.Windows.Forms.Button butComplete;
		private System.Windows.Forms.Button butUnsched;
		private System.Windows.Forms.Button butDelete;
		private System.Windows.Forms.Button butBreak;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.TextBox textMedicalNote;
		private System.Windows.Forms.TextBox textAddressNote;
		private System.Windows.Forms.TextBox textFinancialNote;
		//private bool pinIsOccupied=false;not used
		///<summary></summary>
		public static int SheetClickedonOp;
		///<summary></summary>
		public static int SheetClickedonHour;
		///<summary></summary>
		public static int SheetClickedonMin;
		private System.Windows.Forms.Panel panelNarrow;
		private System.Windows.Forms.Button butOther;
		///<summary></summary>
		public static InfoApt CurInfo;
		private System.Windows.Forms.ToolBarButton toolBarButPat;
		private System.Windows.Forms.ToolBarButton toolBarButPrint;
		private System.Windows.Forms.Panel panelNotes;
		private System.Windows.Forms.TextBox textApptModNote;
		private System.Windows.Forms.Button butAptModNoteEdit;
		private System.Drawing.Printing.PrintDocument pd2;
		private System.Windows.Forms.PrintDialog printDialog2;
		///<summary></summary>
		public static Size PinboardSize=new Size(106,92);
		private OpenDental.XPButton butBack;
		private OpenDental.XPButton butClearPin;
		private OpenDental.XPButton butBackWk;
		private OpenDental.XPButton butFwdWk;
		private OpenDental.XPButton butFwd;
		private System.Windows.Forms.ToolBar toolBar2;
		private System.Windows.Forms.Panel panelMainTools;
		private System.Windows.Forms.Button butPat;
		private System.Windows.Forms.Button butUnschedList;
		private System.Windows.Forms.Button butRecall;
		private System.Windows.Forms.Button butPrint;
		private System.Windows.Forms.Panel panelAptInfo;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelPhoneType;
		private System.Windows.Forms.TextBox textPhone;
		private System.Windows.Forms.ComboBox comboView;
		private System.Windows.Forms.Button butTrack;	
		///<summary></summary>
	  public FormRpPrintPreview pView = new FormRpPrintPreview();


		///<summary></summary>
		public ContrAppt(){
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
			
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code

		private void InitializeComponent(){
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ContrAppt));
			this.panelPinBoard = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.toolBar2 = new System.Windows.Forms.ToolBar();
			this.toolBarButPat = new System.Windows.Forms.ToolBarButton();
			this.toolBarButUnsched = new System.Windows.Forms.ToolBarButton();
			this.toolBarButRecall = new System.Windows.Forms.ToolBarButton();
			this.toolBarButPrint = new System.Windows.Forms.ToolBarButton();
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.ContrApptSheet2 = new OpenDental.ContrApptSheet();
			this.Calendar2 = new System.Windows.Forms.MonthCalendar();
			this.labelDate = new System.Windows.Forms.Label();
			this.labelDate2 = new System.Windows.Forms.Label();
			this.butToday = new System.Windows.Forms.Button();
			this.butTodayWk = new System.Windows.Forms.Button();
			this.panelArrows = new System.Windows.Forms.Panel();
			this.butBack = new OpenDental.XPButton();
			this.butFwd = new OpenDental.XPButton();
			this.butBackWk = new OpenDental.XPButton();
			this.butFwdWk = new OpenDental.XPButton();
			this.textApptModNote = new System.Windows.Forms.TextBox();
			this.textMedicalNote = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textAddressNote = new System.Windows.Forms.TextBox();
			this.textFinancialNote = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.panelSheet = new System.Windows.Forms.Panel();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.panelAptInfo = new System.Windows.Forms.Panel();
			this.listConfirmed = new System.Windows.Forms.ListBox();
			this.butComplete = new System.Windows.Forms.Button();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.butUnsched = new System.Windows.Forms.Button();
			this.butDelete = new System.Windows.Forms.Button();
			this.butBreak = new System.Windows.Forms.Button();
			this.butOther = new System.Windows.Forms.Button();
			this.panelCalendar = new System.Windows.Forms.Panel();
			this.comboView = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.butClearPin = new OpenDental.XPButton();
			this.panelOps = new System.Windows.Forms.Panel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.panelNarrow = new System.Windows.Forms.Panel();
			this.panelNotes = new System.Windows.Forms.Panel();
			this.textPhone = new System.Windows.Forms.TextBox();
			this.labelPhoneType = new System.Windows.Forms.Label();
			this.butAptModNoteEdit = new System.Windows.Forms.Button();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.printDialog2 = new System.Windows.Forms.PrintDialog();
			this.butPat = new System.Windows.Forms.Button();
			this.panelMainTools = new System.Windows.Forms.Panel();
			this.butPrint = new System.Windows.Forms.Button();
			this.butRecall = new System.Windows.Forms.Button();
			this.butUnschedList = new System.Windows.Forms.Button();
			this.butTrack = new System.Windows.Forms.Button();
			this.panelPinBoard.SuspendLayout();
			this.panelArrows.SuspendLayout();
			this.panelSheet.SuspendLayout();
			this.panelAptInfo.SuspendLayout();
			this.panelCalendar.SuspendLayout();
			this.panelNotes.SuspendLayout();
			this.panelMainTools.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelPinBoard
			// 
			this.panelPinBoard.BackColor = System.Drawing.SystemColors.Window;
			this.panelPinBoard.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panelPinBoard.Controls.Add(this.pictureBox1);
			this.panelPinBoard.Location = new System.Drawing.Point(101, 180);
			this.panelPinBoard.Name = "panelPinBoard";
			this.panelPinBoard.Size = new System.Drawing.Size(101, 92);
			this.panelPinBoard.TabIndex = 6;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(80, 2);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(20, 20);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// toolBar2
			// 
			this.toolBar2.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.toolBar2.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																																								this.toolBarButPat,
																																								this.toolBarButUnsched,
																																								this.toolBarButRecall,
																																								this.toolBarButPrint});
			this.toolBar2.ButtonSize = new System.Drawing.Size(29, 28);
			this.toolBar2.Dock = System.Windows.Forms.DockStyle.None;
			this.toolBar2.DropDownArrows = true;
			this.toolBar2.ImageList = this.imageListMain;
			this.toolBar2.Location = new System.Drawing.Point(532, 692);
			this.toolBar2.Name = "toolBar2";
			this.toolBar2.ShowToolTips = true;
			this.toolBar2.Size = new System.Drawing.Size(303, 34);
			this.toolBar2.TabIndex = 0;
			this.toolBar2.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
			this.toolBar2.Visible = false;
			this.toolBar2.Wrappable = false;
			this.toolBar2.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
			// 
			// toolBarButPat
			// 
			this.toolBarButPat.ImageIndex = 0;
			this.toolBarButPat.Tag = "Pat";
			this.toolBarButPat.ToolTipText = "Select Patient";
			// 
			// toolBarButUnsched
			// 
			this.toolBarButUnsched.ImageIndex = 1;
			this.toolBarButUnsched.Tag = "Unsched";
			this.toolBarButUnsched.Text = "Unsched";
			this.toolBarButUnsched.ToolTipText = "Unscheduled List";
			// 
			// toolBarButRecall
			// 
			this.toolBarButRecall.ImageIndex = 2;
			this.toolBarButRecall.Tag = "Recall";
			this.toolBarButRecall.ToolTipText = "Recall List";
			// 
			// toolBarButPrint
			// 
			this.toolBarButPrint.ImageIndex = 3;
			this.toolBarButPrint.Tag = "Print";
			this.toolBarButPrint.ToolTipText = "Print Schedule";
			// 
			// imageListMain
			// 
			this.imageListMain.ImageSize = new System.Drawing.Size(22, 22);
			this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
			this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// ContrApptSheet2
			// 
			this.ContrApptSheet2.Location = new System.Drawing.Point(2, -550);
			this.ContrApptSheet2.Name = "ContrApptSheet2";
			this.ContrApptSheet2.Size = new System.Drawing.Size(60, 1728);
			this.ContrApptSheet2.TabIndex = 22;
			this.ContrApptSheet2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ContrApptSheet2_MouseUp);
			this.ContrApptSheet2.DoubleClick += new System.EventHandler(this.ContrApptSheet2_DoubleClick);
			this.ContrApptSheet2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ContrApptSheet2_MouseMove);
			this.ContrApptSheet2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ContrApptSheet2_MouseDown);
			// 
			// Calendar2
			// 
			this.Calendar2.Location = new System.Drawing.Point(2, 24);
			this.Calendar2.Name = "Calendar2";
			this.Calendar2.ScrollChange = 1;
			this.Calendar2.TabIndex = 23;
			this.Calendar2.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.Calendar2_DateSelected);
			// 
			// labelDate
			// 
			this.labelDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelDate.Location = new System.Drawing.Point(2, 4);
			this.labelDate.Name = "labelDate";
			this.labelDate.Size = new System.Drawing.Size(56, 16);
			this.labelDate.TabIndex = 24;
			this.labelDate.Text = "Wed";
			// 
			// labelDate2
			// 
			this.labelDate2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelDate2.Location = new System.Drawing.Point(46, 4);
			this.labelDate2.Name = "labelDate2";
			this.labelDate2.Size = new System.Drawing.Size(100, 20);
			this.labelDate2.TabIndex = 25;
			this.labelDate2.Text = "-  Oct 20";
			// 
			// butToday
			// 
			this.butToday.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butToday.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butToday.Location = new System.Drawing.Point(17, 0);
			this.butToday.Name = "butToday";
			this.butToday.Size = new System.Drawing.Size(65, 22);
			this.butToday.TabIndex = 29;
			this.butToday.Text = "Today";
			this.butToday.Click += new System.EventHandler(this.butToday_Click);
			// 
			// butTodayWk
			// 
			this.butTodayWk.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butTodayWk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butTodayWk.Location = new System.Drawing.Point(17, 24);
			this.butTodayWk.Name = "butTodayWk";
			this.butTodayWk.Size = new System.Drawing.Size(65, 22);
			this.butTodayWk.TabIndex = 31;
			this.butTodayWk.Text = "Week";
			this.butTodayWk.Click += new System.EventHandler(this.butTodayWk_Click);
			// 
			// panelArrows
			// 
			this.panelArrows.Controls.Add(this.butTodayWk);
			this.panelArrows.Controls.Add(this.butToday);
			this.panelArrows.Controls.Add(this.butBack);
			this.panelArrows.Controls.Add(this.butFwd);
			this.panelArrows.Controls.Add(this.butBackWk);
			this.panelArrows.Controls.Add(this.butFwdWk);
			this.panelArrows.Location = new System.Drawing.Point(2, 182);
			this.panelArrows.Name = "panelArrows";
			this.panelArrows.Size = new System.Drawing.Size(100, 48);
			this.panelArrows.TabIndex = 32;
			// 
			// butBack
			// 
			this.butBack.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butBack.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butBack.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butBack.Image = ((System.Drawing.Image)(resources.GetObject("butBack.Image")));
			this.butBack.Location = new System.Drawing.Point(-1, 0);
			this.butBack.Name = "butBack";
			this.butBack.Size = new System.Drawing.Size(19, 22);
			this.butBack.TabIndex = 51;
			this.butBack.Click += new System.EventHandler(this.butBack_Click);
			// 
			// butFwd
			// 
			this.butFwd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butFwd.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butFwd.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butFwd.Image = ((System.Drawing.Image)(resources.GetObject("butFwd.Image")));
			this.butFwd.Location = new System.Drawing.Point(81, 0);
			this.butFwd.Name = "butFwd";
			this.butFwd.Size = new System.Drawing.Size(19, 22);
			this.butFwd.TabIndex = 53;
			this.butFwd.Click += new System.EventHandler(this.butFwd_Click);
			// 
			// butBackWk
			// 
			this.butBackWk.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butBackWk.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butBackWk.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butBackWk.Image = ((System.Drawing.Image)(resources.GetObject("butBackWk.Image")));
			this.butBackWk.Location = new System.Drawing.Point(-1, 24);
			this.butBackWk.Name = "butBackWk";
			this.butBackWk.Size = new System.Drawing.Size(19, 22);
			this.butBackWk.TabIndex = 51;
			this.butBackWk.Click += new System.EventHandler(this.butBackWk_Click);
			// 
			// butFwdWk
			// 
			this.butFwdWk.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butFwdWk.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butFwdWk.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butFwdWk.Image = ((System.Drawing.Image)(resources.GetObject("butFwdWk.Image")));
			this.butFwdWk.Location = new System.Drawing.Point(81, 24);
			this.butFwdWk.Name = "butFwdWk";
			this.butFwdWk.Size = new System.Drawing.Size(19, 22);
			this.butFwdWk.TabIndex = 52;
			this.butFwdWk.Click += new System.EventHandler(this.butFwdWk_Click);
			// 
			// textApptModNote
			// 
			this.textApptModNote.BackColor = System.Drawing.Color.White;
			this.textApptModNote.ForeColor = System.Drawing.Color.Red;
			this.textApptModNote.Location = new System.Drawing.Point(0, 182);
			this.textApptModNote.Multiline = true;
			this.textApptModNote.Name = "textApptModNote";
			this.textApptModNote.ReadOnly = true;
			this.textApptModNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textApptModNote.Size = new System.Drawing.Size(202, 36);
			this.textApptModNote.TabIndex = 34;
			this.textApptModNote.Text = "";
			// 
			// textMedicalNote
			// 
			this.textMedicalNote.BackColor = System.Drawing.Color.White;
			this.textMedicalNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textMedicalNote.ForeColor = System.Drawing.Color.Red;
			this.textMedicalNote.Location = new System.Drawing.Point(0, 142);
			this.textMedicalNote.Multiline = true;
			this.textMedicalNote.Name = "textMedicalNote";
			this.textMedicalNote.ReadOnly = true;
			this.textMedicalNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textMedicalNote.Size = new System.Drawing.Size(202, 20);
			this.textMedicalNote.TabIndex = 35;
			this.textMedicalNote.Text = "";
			this.textMedicalNote.TextChanged += new System.EventHandler(this.textMedicalNote_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 168);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(160, 18);
			this.label1.TabIndex = 36;
			this.label1.Text = "Appointment module notes:";
			// 
			// textAddressNote
			// 
			this.textAddressNote.BackColor = System.Drawing.Color.White;
			this.textAddressNote.ForeColor = System.Drawing.Color.Red;
			this.textAddressNote.Location = new System.Drawing.Point(0, 90);
			this.textAddressNote.Multiline = true;
			this.textAddressNote.Name = "textAddressNote";
			this.textAddressNote.ReadOnly = true;
			this.textAddressNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textAddressNote.Size = new System.Drawing.Size(202, 36);
			this.textAddressNote.TabIndex = 39;
			this.textAddressNote.Text = "";
			// 
			// textFinancialNote
			// 
			this.textFinancialNote.BackColor = System.Drawing.Color.White;
			this.textFinancialNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textFinancialNote.ForeColor = System.Drawing.Color.Red;
			this.textFinancialNote.Location = new System.Drawing.Point(0, 18);
			this.textFinancialNote.Multiline = true;
			this.textFinancialNote.Name = "textFinancialNote";
			this.textFinancialNote.ReadOnly = true;
			this.textFinancialNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textFinancialNote.Size = new System.Drawing.Size(202, 36);
			this.textFinancialNote.TabIndex = 40;
			this.textFinancialNote.Text = "";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(0, 2);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(194, 14);
			this.label4.TabIndex = 41;
			this.label4.Text = "Urgent Financial Notes";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(0, 128);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(134, 14);
			this.label5.TabIndex = 42;
			this.label5.Text = "Urgent medical notes:";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(0, 76);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 14);
			this.label6.TabIndex = 43;
			this.label6.Text = "Phone/Addr notes:";
			// 
			// panelSheet
			// 
			this.panelSheet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelSheet.Controls.Add(this.vScrollBar1);
			this.panelSheet.Controls.Add(this.ContrApptSheet2);
			this.panelSheet.Location = new System.Drawing.Point(0, 17);
			this.panelSheet.Name = "panelSheet";
			this.panelSheet.Size = new System.Drawing.Size(676, 726);
			this.panelSheet.TabIndex = 44;
			this.panelSheet.Resize += new System.EventHandler(this.panelSheet_Resize);
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
			this.vScrollBar1.Location = new System.Drawing.Point(657, 0);
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(17, 724);
			this.vScrollBar1.TabIndex = 23;
			this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
			// 
			// panelAptInfo
			// 
			this.panelAptInfo.Controls.Add(this.listConfirmed);
			this.panelAptInfo.Controls.Add(this.butComplete);
			this.panelAptInfo.Controls.Add(this.butUnsched);
			this.panelAptInfo.Controls.Add(this.butDelete);
			this.panelAptInfo.Controls.Add(this.butBreak);
			this.panelAptInfo.Location = new System.Drawing.Point(680, 330);
			this.panelAptInfo.Name = "panelAptInfo";
			this.panelAptInfo.Size = new System.Drawing.Size(204, 142);
			this.panelAptInfo.TabIndex = 45;
			// 
			// listConfirmed
			// 
			this.listConfirmed.Location = new System.Drawing.Point(38, 2);
			this.listConfirmed.Name = "listConfirmed";
			this.listConfirmed.Size = new System.Drawing.Size(73, 134);
			this.listConfirmed.TabIndex = 75;
			this.listConfirmed.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listConfirmed_MouseDown);
			this.listConfirmed.SelectedIndexChanged += new System.EventHandler(this.listConfirmed_SelectedIndexChanged);
			// 
			// butComplete
			// 
			this.butComplete.BackColor = System.Drawing.SystemColors.Control;
			this.butComplete.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
			this.butComplete.ImageIndex = 4;
			this.butComplete.ImageList = this.imageList1;
			this.butComplete.Location = new System.Drawing.Point(2, 57);
			this.butComplete.Name = "butComplete";
			this.butComplete.Size = new System.Drawing.Size(28, 28);
			this.butComplete.TabIndex = 69;
			this.butComplete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butComplete.Click += new System.EventHandler(this.butComplete_Click);
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(22, 22);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// butUnsched
			// 
			this.butUnsched.BackColor = System.Drawing.SystemColors.Control;
			this.butUnsched.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
			this.butUnsched.ImageIndex = 5;
			this.butUnsched.ImageList = this.imageList1;
			this.butUnsched.Location = new System.Drawing.Point(2, 1);
			this.butUnsched.Name = "butUnsched";
			this.butUnsched.Size = new System.Drawing.Size(28, 28);
			this.butUnsched.TabIndex = 68;
			this.butUnsched.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUnsched.Click += new System.EventHandler(this.butUnsched_Click);
			// 
			// butDelete
			// 
			this.butDelete.BackColor = System.Drawing.SystemColors.Control;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
			this.butDelete.Location = new System.Drawing.Point(2, 85);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(28, 28);
			this.butDelete.TabIndex = 66;
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butBreak
			// 
			this.butBreak.BackColor = System.Drawing.SystemColors.Control;
			this.butBreak.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
			this.butBreak.ImageIndex = 1;
			this.butBreak.ImageList = this.imageList1;
			this.butBreak.Location = new System.Drawing.Point(2, 29);
			this.butBreak.Name = "butBreak";
			this.butBreak.Size = new System.Drawing.Size(28, 28);
			this.butBreak.TabIndex = 65;
			this.butBreak.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butBreak.Click += new System.EventHandler(this.butBreak_Click);
			// 
			// butOther
			// 
			this.butOther.BackColor = System.Drawing.SystemColors.Control;
			this.butOther.Image = ((System.Drawing.Image)(resources.GetObject("butOther.Image")));
			this.butOther.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
			this.butOther.Location = new System.Drawing.Point(682, 443);
			this.butOther.Name = "butOther";
			this.butOther.Size = new System.Drawing.Size(28, 28);
			this.butOther.TabIndex = 72;
			this.butOther.Click += new System.EventHandler(this.butOther_Click);
			// 
			// panelCalendar
			// 
			this.panelCalendar.Controls.Add(this.comboView);
			this.panelCalendar.Controls.Add(this.label2);
			this.panelCalendar.Controls.Add(this.butClearPin);
			this.panelCalendar.Controls.Add(this.Calendar2);
			this.panelCalendar.Controls.Add(this.labelDate);
			this.panelCalendar.Controls.Add(this.labelDate2);
			this.panelCalendar.Controls.Add(this.panelPinBoard);
			this.panelCalendar.Controls.Add(this.panelArrows);
			this.panelCalendar.Location = new System.Drawing.Point(680, 28);
			this.panelCalendar.Name = "panelCalendar";
			this.panelCalendar.Size = new System.Drawing.Size(204, 301);
			this.panelCalendar.TabIndex = 46;
			// 
			// comboView
			// 
			this.comboView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboView.Location = new System.Drawing.Point(0, 277);
			this.comboView.Name = "comboView";
			this.comboView.Size = new System.Drawing.Size(203, 21);
			this.comboView.TabIndex = 35;
			this.comboView.SelectedIndexChanged += new System.EventHandler(this.comboView_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(0, 255);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(90, 19);
			this.label2.TabIndex = 34;
			this.label2.Text = "View:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butClearPin
			// 
			this.butClearPin.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClearPin.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butClearPin.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butClearPin.Image = ((System.Drawing.Image)(resources.GetObject("butClearPin.Image")));
			this.butClearPin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butClearPin.Location = new System.Drawing.Point(27, 228);
			this.butClearPin.Name = "butClearPin";
			this.butClearPin.Size = new System.Drawing.Size(75, 26);
			this.butClearPin.TabIndex = 33;
			this.butClearPin.Text = "Clear";
			this.butClearPin.Click += new System.EventHandler(this.butClearPin_Click);
			// 
			// panelOps
			// 
			this.panelOps.Location = new System.Drawing.Point(0, 0);
			this.panelOps.Name = "panelOps";
			this.panelOps.Size = new System.Drawing.Size(676, 17);
			this.panelOps.TabIndex = 48;
			// 
			// toolTip1
			// 
			this.toolTip1.AutoPopDelay = 5000;
			this.toolTip1.InitialDelay = 100;
			this.toolTip1.ReshowDelay = 100;
			// 
			// panelNarrow
			// 
			this.panelNarrow.Location = new System.Drawing.Point(877, 18);
			this.panelNarrow.Name = "panelNarrow";
			this.panelNarrow.Size = new System.Drawing.Size(34, 716);
			this.panelNarrow.TabIndex = 49;
			this.panelNarrow.Visible = false;
			// 
			// panelNotes
			// 
			this.panelNotes.Controls.Add(this.textFinancialNote);
			this.panelNotes.Controls.Add(this.textPhone);
			this.panelNotes.Controls.Add(this.labelPhoneType);
			this.panelNotes.Controls.Add(this.butAptModNoteEdit);
			this.panelNotes.Controls.Add(this.textApptModNote);
			this.panelNotes.Controls.Add(this.textMedicalNote);
			this.panelNotes.Controls.Add(this.label1);
			this.panelNotes.Controls.Add(this.textAddressNote);
			this.panelNotes.Controls.Add(this.label5);
			this.panelNotes.Controls.Add(this.label6);
			this.panelNotes.Controls.Add(this.label4);
			this.panelNotes.Location = new System.Drawing.Point(680, 473);
			this.panelNotes.Name = "panelNotes";
			this.panelNotes.Size = new System.Drawing.Size(204, 223);
			this.panelNotes.TabIndex = 50;
			// 
			// textPhone
			// 
			this.textPhone.Location = new System.Drawing.Point(102, 56);
			this.textPhone.Name = "textPhone";
			this.textPhone.ReadOnly = true;
			this.textPhone.TabIndex = 53;
			this.textPhone.Text = "";
			// 
			// labelPhoneType
			// 
			this.labelPhoneType.Location = new System.Drawing.Point(4, 58);
			this.labelPhoneType.Name = "labelPhoneType";
			this.labelPhoneType.Size = new System.Drawing.Size(100, 14);
			this.labelPhoneType.TabIndex = 52;
			this.labelPhoneType.Text = "Home Phone:";
			this.labelPhoneType.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butAptModNoteEdit
			// 
			this.butAptModNoteEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butAptModNoteEdit.Location = new System.Drawing.Point(150, 166);
			this.butAptModNoteEdit.Name = "butAptModNoteEdit";
			this.butAptModNoteEdit.Size = new System.Drawing.Size(52, 17);
			this.butAptModNoteEdit.TabIndex = 51;
			this.butAptModNoteEdit.Text = "Edit";
			this.butAptModNoteEdit.Click += new System.EventHandler(this.butAptModNoteEdit_Click);
			// 
			// pd2
			// 
			this.pd2.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pd2_PrintPage);
			// 
			// butPat
			// 
			this.butPat.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butPat.ImageIndex = 0;
			this.butPat.ImageList = this.imageListMain;
			this.butPat.Location = new System.Drawing.Point(0, 0);
			this.butPat.Name = "butPat";
			this.butPat.Size = new System.Drawing.Size(28, 28);
			this.butPat.TabIndex = 51;
			this.butPat.Click += new System.EventHandler(this.butPat_Click);
			// 
			// panelMainTools
			// 
			this.panelMainTools.Controls.Add(this.butTrack);
			this.panelMainTools.Controls.Add(this.butPrint);
			this.panelMainTools.Controls.Add(this.butRecall);
			this.panelMainTools.Controls.Add(this.butUnschedList);
			this.panelMainTools.Controls.Add(this.butPat);
			this.panelMainTools.Location = new System.Drawing.Point(679, 0);
			this.panelMainTools.Name = "panelMainTools";
			this.panelMainTools.Size = new System.Drawing.Size(206, 28);
			this.panelMainTools.TabIndex = 52;
			// 
			// butPrint
			// 
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butPrint.ImageIndex = 3;
			this.butPrint.ImageList = this.imageListMain;
			this.butPrint.Location = new System.Drawing.Point(112, 0);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(28, 28);
			this.butPrint.TabIndex = 54;
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// butRecall
			// 
			this.butRecall.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butRecall.ImageIndex = 2;
			this.butRecall.ImageList = this.imageListMain;
			this.butRecall.Location = new System.Drawing.Point(56, 0);
			this.butRecall.Name = "butRecall";
			this.butRecall.Size = new System.Drawing.Size(28, 28);
			this.butRecall.TabIndex = 53;
			this.butRecall.Click += new System.EventHandler(this.butRecall_Click);
			// 
			// butUnschedList
			// 
			this.butUnschedList.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butUnschedList.ImageIndex = 1;
			this.butUnschedList.ImageList = this.imageListMain;
			this.butUnschedList.Location = new System.Drawing.Point(28, 0);
			this.butUnschedList.Name = "butUnschedList";
			this.butUnschedList.Size = new System.Drawing.Size(28, 28);
			this.butUnschedList.TabIndex = 52;
			this.butUnschedList.Click += new System.EventHandler(this.butUnschedList_Click);
			// 
			// butTrack
			// 
			this.butTrack.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butTrack.ImageIndex = 4;
			this.butTrack.ImageList = this.imageListMain;
			this.butTrack.Location = new System.Drawing.Point(84, 0);
			this.butTrack.Name = "butTrack";
			this.butTrack.Size = new System.Drawing.Size(28, 28);
			this.butTrack.TabIndex = 55;
			this.butTrack.Click += new System.EventHandler(this.butTrack_Click);
			// 
			// ContrAppt
			// 
			this.Controls.Add(this.toolBar2);
			this.Controls.Add(this.butOther);
			this.Controls.Add(this.panelNotes);
			this.Controls.Add(this.panelOps);
			this.Controls.Add(this.panelCalendar);
			this.Controls.Add(this.panelAptInfo);
			this.Controls.Add(this.panelSheet);
			this.Controls.Add(this.panelNarrow);
			this.Controls.Add(this.panelMainTools);
			this.Name = "ContrAppt";
			this.Size = new System.Drawing.Size(939, 872);
			this.Resize += new System.EventHandler(this.ContrAppt_Resize);
			this.Load += new System.EventHandler(this.ContrAppt_Load);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ContrAppt_Layout);
			this.panelPinBoard.ResumeLayout(false);
			this.panelArrows.ResumeLayout(false);
			this.panelSheet.ResumeLayout(false);
			this.panelAptInfo.ResumeLayout(false);
			this.panelCalendar.ResumeLayout(false);
			this.panelNotes.ResumeLayout(false);
			this.panelMainTools.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e) {
			//if(Environment.
		}

		private void ContrApptSheet2_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e){
			int max=vScrollBar1.Maximum-vScrollBar1.LargeChange;//panelTable.Height-panelScroll.Height+3;
			int newScrollVal=vScrollBar1.Value-(int)(e.Delta/4);
			if(newScrollVal > max){
				vScrollBar1.Value=max;
			}
			else if(newScrollVal < vScrollBar1.Minimum){
				vScrollBar1.Value=vScrollBar1.Minimum;
			}
			else{
				vScrollBar1.Value=newScrollVal;
			}
			ContrApptSheet2.Location=new Point(0,-vScrollBar1.Value);
			//panelTable.Location=new Point(0,-vScrollBar1.Value);
    }

		///<summary></summary>
		public void ModuleSelected(){
			//the scrollbar logic cannot be moved to someplace where it will be activated while working in apptbook
			this.SuspendLayout();
			vScrollBar1.Enabled=true;
			vScrollBar1.Minimum=0;
			vScrollBar1.LargeChange=6*ContrApptSheet.Lh*2;
			vScrollBar1.Maximum=ContrApptSheet2.Height-panelSheet.Height+vScrollBar1.LargeChange;
			//Max is set again in Resize event
			vScrollBar1.SmallChange=6*ContrApptSheet.Lh;//(1 hour)
			if(vScrollBar1.Value==0)
				vScrollBar1.Value=8*6*ContrApptSheet.Lh;//8am
			ContrApptSheet2.MouseWheel+=new MouseEventHandler(ContrApptSheet2_MouseWheel);
			ContrApptSheet2.Location=new Point(0,-vScrollBar1.Value);
			panelOps.Controls.Clear();
			for(int i=0;i<ContrApptSheet.ProvCount;i++){
				System.Windows.Forms.Button butProv=new System.Windows.Forms.Button();
				butProv.Text="";
				butProv.BackColor=Providers.List[i].ProvColor;
				butProv.Location=new Point(2+ContrApptSheet.TimeWidth+ContrApptSheet.ProvWidth*i,0);
				butProv.Width=ContrApptSheet.ProvWidth;
				if(i==0){//just looks a little nicer:
					butProv.Location=new Point(butProv.Location.X-1,butProv.Location.Y);
					butProv.Width=butProv.Width+1;
				}				
				butProv.Height=18;
				panelOps.Controls.Add(butProv);
				toolTip1.SetToolTip(butProv,Providers.List[i].Abbr);
			}
			for(int i=0;i<ContrApptSheet.ColCount;i++){
				System.Windows.Forms.Button opName=new System.Windows.Forms.Button();
				opName.Text=Defs.Short[(int)DefCat.Operatories][ApptViewItems.VisOps[i]].ItemName;
				opName.Location=new Point(2+ContrApptSheet.TimeWidth+ContrApptSheet.ProvWidth*ContrApptSheet.ProvCount+i*ContrApptSheet.ColWidth,0);
				opName.Width=ContrApptSheet.ColWidth;
				opName.Height=18;
				panelOps.Controls.Add(opName);
			}
			this.ResumeLayout();
			listConfirmed.Items.Clear();
			for(int i=0;i<Defs.Short[(int)DefCat.ApptConfirmed].Length;i++){
				this.listConfirmed.Items.Add(Defs.Short[(int)DefCat.ApptConfirmed][i].ItemValue);
				//if(Defs.Defns[(int)DefCat.ApptConfirmed][i].DefNum==Appointments.Cur.Confirmed)
				//	listConfirmed.SelectedIndex=i;
			}
			RefreshModuleData();
			RefreshModuleScreen();
		}

		///<summary></summary>
		public void ModuleUnselected(){
			Patients.FamilyList=null;
			InsPlans.List=null;
			CovPats.List=null;
			//from RefreshDay:
			Appointments.ListDay=null;
			Schedules.ListDay=null;
			ContrApptSheet2.Shadow=null;
			if(ContrApptSingle3!=null){//too complex?
				for(int i=0;i<ContrApptSingle3.Length;i++){
					ContrApptSingle3[i].Dispose();
					ContrApptSingle3[i]=null;
				}
				ContrApptSingle3=null;
			}
		}

		///<summary>Gets the data for the current patient, just like in the other modules.  Gets patients.GetFamily, InsPlans, and CovPats.  Does not refresh any appointment data.  This function is not always called.  Sometimes, Patients.GetFamily is run directly.</summary>
		private void RefreshModuleData(){
			if(Patients.PatIsLoaded){
				Patients.GetFamily(Patients.Cur.PatNum);
				InsPlans.Refresh();
				CovPats.Refresh();
			}
		}

		///<summary>Refreshes the main window title and then calls RefreshDay which gets the appointment data from the database.</summary>
		public void RefreshModuleScreen(){
			//MessageBox.Show("Refreshed");
			if(Patients.PatIsLoaded){
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString+" - "+Patients.GetCurNameLF();
			}
			else{
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString;
			}
			RefreshDay(Appointments.DateSelected);
		}

		///<summary>Triggered every time a control(including appt) is added or removed.</summary>
		private void ContrAppt_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
			//This event actually happens quite frequently: once for every appointment placed on the screen.
			//Would like to rework it somehow to only be called when needed.
			//Assumes widths of the first 4 panels were set the same in the designer,
			panelMainTools.Location=new Point(ClientSize.Width-panelAptInfo.Width-2,0);
			panelCalendar.Location=new Point(ClientSize.Width-panelAptInfo.Width-2,panelMainTools.Height);
			panelAptInfo.Location=new Point(ClientSize.Width-panelAptInfo.Width-2
				,panelMainTools.Height+panelCalendar.Height);
			butOther.Location=new Point(panelAptInfo.Location.X+2,panelAptInfo.Location.Y+113);
			panelNotes.Location=new Point(ClientSize.Width-panelAptInfo.Width-2
				,panelMainTools.Height+panelCalendar.Height+panelAptInfo.Height);
			panelSheet.Width=ClientSize.Width-panelAptInfo.Width-2;
			panelSheet.Height=ClientSize.Height-panelSheet.Location.Y;
			if(Defs.Short!=null){
				ApptViews.SetCur(comboView.SelectedIndex);
				ApptViewItems.GetForCurView();
				ContrApptSheet2.ComputeColWidth(panelSheet.Width-vScrollBar1.Width);
			}
			//ContrApptSheet2.Height=panelSheet.Height;
			panelOps.Width=panelSheet.Width;
		}

		///<summary>Not used right now</summary>
		private void ContrAppt_Resize(object sender, System.EventArgs e) {
			//This didn't work so well.  Very slow and caused program to not be able to unminimize
			try{//so it doesn't crash if not connected to DB
				//ModuleSelected();
			}
			catch{}
		}

		///<summary>Called from FormOpenDental upon startup.</summary>
		public void InstantClasses(){
			PinApptSingle = new ContrApptSingle();
			PinApptSingle.Visible=false;
			PinApptSingle.ThisIsPinBoard=true;
			this.Controls.Add(PinApptSingle);
			PinApptSingle.MouseDown += new System.Windows.Forms.MouseEventHandler(PinApptSingle_MouseDown);
			PinApptSingle.MouseUp += new System.Windows.Forms.MouseEventHandler(PinApptSingle_MouseUp);
			PinApptSingle.MouseMove += new System.Windows.Forms.MouseEventHandler(PinApptSingle_MouseMove);
			Appointments.DateSelected=DateTime.Now;
			ContrApptSingle.SelectedAptNum=-1;
			RefreshModuleScreen();
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.butToday,
				this.butTodayWk,
				this.butClearPin,
				this.butAptModNoteEdit,
				this.butBreak,
				this.butComplete,
				this.butDelete,
				this.butOther,
				this.label4,
				this.labelDate,
				this.labelDate2,	
				this.labelPhoneType,
				label6,
				label5,
				label1,
				this.panelAptInfo,
				this.panelArrows,
				this.panelCalendar,
				this.panelNarrow,
				this.panelNotes,
				this.panelOps,
				this.panelPinBoard,
				this.panelSheet,
			});
			butAptModNoteEdit.Text=Lan.g("All","Edit");
			//this.toolBarButPat.ToolTipText = Lan.g(this,"Select Patient");
			//this.toolBarButUnsched.ToolTipText = Lan.g(this,"Unscheduled List");
			//this.toolBarButRecall.ToolTipText = Lan.g(this,"Recall List");
			//this.toolbar
			//this.toolBarButPrint.ToolTipText = Lan.g(this,"Print Schedule");
			toolTip1.SetToolTip(this.butPat, Lan.g(this,"SelectPatient"));
			toolTip1.SetToolTip(this.butUnschedList, Lan.g(this,"Unscheduled List"));
			toolTip1.SetToolTip(this.butRecall, Lan.g(this,"Recall List"));
			toolTip1.SetToolTip(this.butTrack, Lan.g(this,"Track Next Appointments"));
			toolTip1.SetToolTip(this.butPrint, Lan.g(this,"Print Schedule"));
			//Appointment action buttons
			toolTip1.SetToolTip(butUnsched, Lan.g(this,"Send to Unscheduled List"));
      toolTip1.SetToolTip(butBreak, Lan.g(this,"Break"));
			toolTip1.SetToolTip(butComplete, Lan.g(this,"Set Complete"));
			toolTip1.SetToolTip(butDelete, Lan.g(this,"Delete"));
			toolTip1.SetToolTip(butOther, Lan.g(this,"Other Appointments"));
		}

		///<summary>Not in use.  See InstantClasses instead.</summary>
		private void ContrAppt_Load(object sender, System.EventArgs e){
			//ContrApptInfo2.DataChanged += new EventHandler(ReactionDataChanged);
		}

		/// <summary>
		/// Can be triggered by user or in FunctionKeyPress, SetView, or  FillViews
		/// </summary>
		private void comboView_SelectedIndexChanged(object sender, System.EventArgs e) {
			//first two lines might be redundant:
			ApptViews.SetCur(comboView.SelectedIndex);
			ApptViewItems.GetForCurView();
			ContrApptSheet2.ComputeColWidth(panelSheet.Width-vScrollBar1.Width);
			ModuleSelected();
		}

		/// <summary>
		/// The key press from the main form is passed down to this module.
		/// </summary>
		/// <param name="keys"></param>
		public void FunctionKeyPress(Keys keys){
			//MessageBox.Show("keydown");
			switch(keys){
				case Keys.F1: SetView(0); break;
				case Keys.F2: SetView(1); break;
				case Keys.F3: SetView(2); break;
				case Keys.F4: SetView(3); break;
				case Keys.F5: SetView(4); break;
				case Keys.F6: SetView(5); break;
				case Keys.F7: SetView(6); break;
				case Keys.F8: SetView(7); break;
				case Keys.F9: SetView(8); break;
				case Keys.F10: SetView(9); break;
				case Keys.F11: SetView(10); break;
				case Keys.F12: SetView(11); break;
			}
		}

		/// <summary>
		/// Sets the view to the specified index, checking for validity in the process.
		/// </summary>
		/// <param name="viewIndex"></param>
		private void SetView(int viewIndex){
			if(viewIndex >= ApptViews.List.Length){
				return;
			}
			comboView.SelectedIndex=viewIndex;//this also triggers SelectedIndexChanged
		}

		///<summary>Fills the comboView with the current list of views and then tries to reselect the previous selection.  Also called from FormOpenDental.RefreshLocalData().</summary>
		public void FillViews(){
			int selected=comboView.SelectedIndex;
			comboView.Items.Clear();
			string f="";
			for(int i=0;i<ApptViews.List.Length;i++){
				if(i<=12)
					f="F"+(i+1).ToString()+"-";
				else
					f="";
				comboView.Items.Add(f+ApptViews.List[i].Description);
			}
			if(selected<comboView.Items.Count){
				comboView.SelectedIndex=selected;//this also triggers SelectedIndexChanged
			}
		}

		private void FillPanelPatient(){
			if(Patients.PatIsLoaded){
				butOther.Enabled=true;//this button is handled entirely separately from the others
			}
			else{
				butOther.Enabled=false;
			}
			if(ContrApptSingle.PinBoardIsSelected
				|| ContrApptSingle.SelectedAptNum!=-1){
				panelNotes.Enabled=true;
			}
			else{
				panelNotes.Enabled=false;
			}
			if(ContrApptSingle.SelectedAptNum==-1){//apt not selected
				panelAptInfo.Enabled=false;
			}
			else{//apt selected
				bool isPresent=false;
				for(int i=0;i<Appointments.ListDay.Length;i++){
					if(Appointments.ListDay[i].AptNum==ContrApptSingle.SelectedAptNum){
						isPresent=true;
						Appointments.Cur=Appointments.ListDay[i];
					}
				}
				if(isPresent)	panelAptInfo.Enabled=true;//apt selected and present
				else panelAptInfo.Enabled=false;//apt selected but not present
			}
			if(panelNotes.Enabled){
				textFinancialNote.Text=Patients.FamilyList[Patients.GuarIndex].FamFinUrgNote;
				textAddressNote.Text=Patients.FamilyList[Patients.GuarIndex].AddrNote;
				textMedicalNote.Text=Patients.Cur.MedUrgNote;
				textApptModNote.Text=Patients.Cur.ApptModNote;
				if(Patients.Cur.HmPhone!=""){
					labelPhoneType.Text=Lan.g(this,"Home Phone:");
					textPhone.Text=Patients.Cur.HmPhone;
				}
				else if(Patients.Cur.WirelessPhone!=""){
					labelPhoneType.Text=Lan.g(this,"Wireless Phone:");
					textPhone.Text=Patients.Cur.WirelessPhone;
				}
				else if(Patients.Cur.WkPhone!=""){
					labelPhoneType.Text=Lan.g(this,"Work Phone:");
					textPhone.Text=Patients.Cur.WkPhone;
				}
				else{
					labelPhoneType.Text=Lan.g(this,"(no phone listed)");
					textPhone.Text="";
				}
			}
			else{
				textFinancialNote.Text="";
				textAddressNote.Text="";
				textMedicalNote.Text="";
				textApptModNote.Text="";
				labelPhoneType.Text="";
				textPhone.Text="";
			}
		}

		///<summary>Sets appointment data invalid on all other computers, causing them to refresh.
		///Does NOT refresh the data for this computer which must be done separately.</summary>
		private void SetInvalid(){
			DataValid DataValid2=new DataValid();
			DataValid.IType=InvalidType.Date;
			DataValid.DateViewing=Appointments.DateSelected;
			DataValid2.SetInvalid();
		}

		///<summary>Gets all new day info from db and redraws screen</summary>
		private void RefreshDay(DateTime myDate){
			ApptViews.SetCur(comboView.SelectedIndex);
			ApptViewItems.GetForCurView();
			ContrApptSingle.ProvBar = new int[ApptViewItems.VisProvs.Length][];
			for(int i=0;i<ApptViewItems.VisProvs.Length;i++){//Providers.List.Length;i++){
				ContrApptSingle.ProvBar[i] = new int[144];
			}
			if(ContrApptSingle3!=null){
				for(int i=0;i<ContrApptSingle3.Length;i++){
					ContrApptSingle3[i].Dispose();
					ContrApptSingle3[i]=null;
				}
				ContrApptSingle3=null;
			}
			Appointments.Refresh(myDate);
			Schedules.RefreshDay(myDate);
			labelDate.Text = myDate.ToString("ddd");
			labelDate2.Text = myDate.ToString("-  MMM d");
			Calendar2.SetDate(myDate);
			ContrApptSheet2.Controls.Clear();
			ContrApptSingle3 = new ContrApptSingle[Appointments.ListDay.Length];
			int[] AptNums = new int[Appointments.ListDay.Length];
			for(int i=0;i<Appointments.ListDay.Length;i++){
				AptNums[i]=Appointments.ListDay[i].AptNum;
			}
			Procedures.GetProcsMultApts(AptNums);
			for (int i=0;i<Appointments.ListDay.Length;i++){
				CurInfo=new InfoApt();
				CurInfo.MyApt=Appointments.ListDay[i];
				Patients.GetLim(Appointments.ListDay[i].PatNum);
				CurInfo.CreditAndIns=Patients.GetLimCreditIns();
				CurInfo.PatientName=Patients.LimName;
				Procedures.GetProcsOneApt(Appointments.ListDay[i].AptNum);
				CurInfo.Procs=Procedures.ProcsOneApt;
				AssembleInfo();
				ContrApptSingle3[i]=new ContrApptSingle();
				ContrApptSingle3[i].Visible=false;
				ContrApptSingle3[i].Info=CurInfo;
				//copy time pattern to provBar[]:
				if(ApptViewItems.GetIndexProv(ContrApptSingle3[i].Info.MyApt.ProvNum)!=-1
				//if(Providers.GetIndex(ContrApptSingle3[i].Info.MyApt.ProvNum)!=-1
					&& ContrApptSingle3[i].Info.MyApt.AptStatus!=ApptStatus.Broken){
					for(int k=0;k<ContrApptSingle3[i].Info.MyApt.Pattern.Length; k++){
						if(ContrApptSingle3[i].Info.MyApt.Pattern.Substring(k,1)=="X"){
							int timeBarInc = ContrApptSingle3[i].ConvertToY()/ContrApptSheet.Lh+k;
							ContrApptSingle.ProvBar
								[ApptViewItems.GetIndexProv(ContrApptSingle3[i].Info.MyApt.ProvNum)][timeBarInc]+=1;
						}
					}
				}

				ContrApptSingle3[i].SetLocation();
				ContrApptSheet2.Controls.Add(ContrApptSingle3[i]);
			}//end for
			//if(ContrApptSingle.SelectedAptNum!=1
			PinApptSingle.Refresh();
			ContrApptSheet2.CreateShadow();
			CreateAptShadows();
			ContrApptSheet2.DrawShadow();
			FillPanelPatient();

		}

		///<summary>Creates one bitmap image for each appointment if visible.</summary>
		private void CreateAptShadows(){
			//if(==null)
			//	return;
			Graphics grfx=Graphics.FromImage(ContrApptSheet2.Shadow);
			for(int i=0;i<Appointments.ListDay.Length;i++){
				//MessageBox.Show("i:"+i.ToString()+",width:"+ContrApptSheet2.Width.ToString());
				ContrApptSingle3[i].CreateShadow();
				if(ContrApptSingle3[i].Location.X>=0){
					grfx.DrawImage(ContrApptSingle3[i].Shadow
						,ContrApptSingle3[i].Location.X,ContrApptSingle3[i].Location.Y);
				}
				ContrApptSingle3[i].Shadow=null;
			}
			grfx.Dispose();
		}

		///<summary>Assembles the lines of text to be displayed on a single appointment.</summary>
		private void AssembleInfo(){
			//must do the following before running method:
				//CurInfo=new InfoApt();
				//CurInfo.MyApt=...
				//CurInfo.CreditAndIns=...
				//CurInfo.PatientName=...
				//Procedures.GetProcsOneApt(Appointments.List[i].AptNum);(or GetProcsForSingle, or similar)
				//CurInfo.Procs=Procedures.ProcsOneApt; (or similar)
			CurInfo.Lines = new string[CurInfo.MyApt.Pattern.Length];
			if(CurInfo.MyApt.IsNewPatient)
        CurInfo.Lines[0]="NP-"+CurInfo.PatientName;
			else
				CurInfo.Lines[0]=CurInfo.PatientName;
			int nextLine=1;
			if(CurInfo.Lines.Length>1){
				switch(CurInfo.MyApt.Lab){
					case LabCase.None:
						nextLine=1;
						break;
					case LabCase.Sent:
						CurInfo.Lines[1]=" LAB SENT";
						nextLine=2;
						break;
					case LabCase.Received:
						CurInfo.Lines[1]=" LAB RECEIVED";
						nextLine=2;
						break;
				}
				for(int j=0; j<CurInfo.Procs.Length; j++){
					if(j+nextLine<CurInfo.Lines.Length)
						CurInfo.Lines[j+nextLine]=CurInfo.Procs[j];
				}
			}
			//turn note into arraylist:
			ArrayList AListNote=new ArrayList();
			Graphics grfx=this.CreateGraphics();
			int iChars=0;
			int iLines=0;
			Font myFont=new Font("Arial",8);//Small Font",7f);
			RectangleF rectf 
				=new Rectangle(0,0
				,System.Convert.ToInt32((ContrApptSheet.ColWidth-12)/.95)//.95 is arbitrary
				,(int)myFont.GetHeight(grfx));
			string remainNote = CurInfo.MyApt.Note;
			while (remainNote.Length>0){
				grfx.MeasureString(remainNote,myFont,rectf.Size,new StringFormat(),out iChars, out iLines);
				if (iChars!=remainNote.Length)
					while (iChars!=0&&(remainNote.Substring(iChars-1,1)!=" ")){
						iChars=iChars-1;
					}
				if (iChars==0){//this will wrap even if no spaces at all, whether short or very long
					grfx.MeasureString(remainNote,myFont,rectf.Size,new StringFormat(),out iChars, out iLines);
				}
				AListNote.Add(remainNote.Substring(0,iChars));
				remainNote = remainNote.Substring(iChars);
			}
			grfx.Dispose();
			//add note arraylist to appointment lines[]:
			int noteLine=0;
			for (int j=nextLine+CurInfo.Procs.Length;j<nextLine+CurInfo.Procs.Length+AListNote.Count;j++){
				if (j<CurInfo.Lines.Length){
					CurInfo.Lines[j]=(string)AListNote[noteLine];
					noteLine++;
				}
			}
		}//end assembleinfo

		///<summary>Gets the index within the array of appointment controls, based on the supplied primary key.</summary>
		private int GetIndex(int myAptNum){
			int retVal=-1;
			for(int i=0;i<ContrApptSingle3.Length;i++){
				if(ContrApptSingle3[i].Info.MyApt.AptNum==myAptNum){
					retVal=i;
				}
			}
			return retVal;
		}

		///<summary>Copies all info for for current appointment into the control that displays the pinboard appointment. Sets pinboard appointment as selected.</summary>
		private void CurToPinBoard(){
			PinApptSingle.Visible=false;
			PinApptSingle.Info=CurInfo;
			PinApptSingle.SetLocation();//MUST come before next line
			PinApptSingle.Location=new Point(panelCalendar.Location.X+panelPinBoard.Location.X+2
				,panelCalendar.Location.Y+panelPinBoard.Location.Y+2);
			PinApptSingle.SetSize();
			Appointments.PinBoard=CurInfo.MyApt;//Appointments.List[ContrApptSingle.SelectedIndex];
			PinApptSingle.Visible=true;
			PinApptSingle.BringToFront();
			mouseIsDown=false;
			boolAptMoved=false;
			ContrApptSingle.PinBoardIsSelected=true;
			ContrApptSingle.SelectedAptNum=-1;//CurInfo.MyApt.AptNum;
			PinApptSingle.CreateShadow();
			PinApptSingle.Refresh();
		}

		///<summary>Mouse down event for the pinboard appointment.
		///Sets selected and prepares for drag.</summary>
		private void PinApptSingle_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e){
			mouseIsDown = true;
			ContrApptSingle.PinBoardIsSelected=true;
			TempApptSingle=new ContrApptSingle();
			TempApptSingle.Info=PinApptSingle.Info;
			TempApptSingle.Visible=false;
			Controls.Add(TempApptSingle);
			TempApptSingle.SetLocation();
			TempApptSingle.CreateShadow();
			TempApptSingle.BringToFront();
			ContrApptSingle.SelectedAptNum=-1;//PinApptSingle.Info.MyApt.AptNum;
			Patients.GetFamily(PinApptSingle.Info.MyApt.PatNum);
			ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString+" - "+Patients.GetCurNameLF();
			FillPanelPatient();
			PinApptSingle.CreateShadow();
			PinApptSingle.Refresh();
			CreateAptShadows();
			ContrApptSheet2.DrawShadow();
			//mouseOrigin is in Appt sheet coordinates
			mouseOrigin.X=e.X+PinApptSingle.Location.X;
			mouseOrigin.Y=e.Y+PinApptSingle.Location.Y;
			contOrigin=PinApptSingle.Location;
		}//end PinApptSingle_MouseDown

		///<summary>Mouse move event for pinboard appt. Moves pinboard appt if mouse is down.</summary>
		private void PinApptSingle_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e){
			if (mouseIsDown==false) return;
			if ((Math.Abs(e.X+PinApptSingle.Location.X-mouseOrigin.X)<1)
				&(Math.Abs(e.Y+PinApptSingle.Location.Y-mouseOrigin.Y)<1)){
				return;
			}
			if(TempApptSingle.Location==new Point(0,0)){
				TempApptSingle.Height=1;//to prevent flicker in UL corner
			}
			TempApptSingle.Visible=true;
			boolAptMoved=true;
			Point tempPoint = new Point();
			tempPoint.X=contOrigin.X+(e.X+PinApptSingle.Location.X)-mouseOrigin.X;
			tempPoint.Y=contOrigin.Y+(e.Y+PinApptSingle.Location.Y)-mouseOrigin.Y;
			TempApptSingle.Location=tempPoint;
			if(TempApptSingle.Height==1){
				TempApptSingle.SetSize();
			}
		}

		///<summary>Mouse up event for pinboard appt
		///Usually happens after pinboard appt has been dragged onto main appt sheet.</summary>
		private void PinApptSingle_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e){
			if(!boolAptMoved){
				mouseIsDown=false;
				TempApptSingle.Dispose();
				//PinApptSingle.Refresh();//?
				//ContrApptSheet2.Refresh();//?
				return;
			}
			if(TempApptSingle.Location.X>ContrApptSheet2.Width){//
				mouseIsDown=false;
				boolAptMoved=false;
				//pinIsOccupied=true;
				TempApptSingle.Dispose();
				//ContrApptSheet2.Refresh();//?
			}
			else if (MessageBox.Show(Lan.g(this,"Move Appointment?"),"",MessageBoxButtons.OKCancel)==DialogResult.OK){
				//convert loc to new time
				Appointments.Cur=Appointments.PinBoard;
				int tHr=ContrApptSheet2.ConvertToHour
					(TempApptSingle.Location.Y-ContrApptSheet2.Location.Y-panelSheet.Location.Y);
				int tMin=ContrApptSheet2.ConvertToMin
					(TempApptSingle.Location.Y-ContrApptSheet2.Location.Y-panelSheet.Location.Y);
				DateTime tDate=Appointments.DateSelected;
				DateTime fromDate=Appointments.Cur.AptDateTime.Date;
				Appointments.Cur.AptDateTime=new DateTime(tDate.Year,tDate.Month,tDate.Day,tHr,tMin,0);
				Appointments.Cur.Op=Defs.Short[(int)DefCat.Operatories]
					[ApptViewItems.VisOps
					[ContrApptSheet2.ConvertToOp(TempApptSingle.Location.X-ContrApptSheet2.Location.X)]]
					.DefNum;
				if(DoesOverlap()){
					int startingOp=ApptViewItems.GetIndexOp(Appointments.Cur.Op);
						//Defs.GetOrder(DefCat.Operatories,Appointments.Cur.Op);
					bool stillOverlaps=true;
					for(int i=startingOp;i<ApptViewItems.VisOps.Length;i++){
						//Defs.Short[(int)DefCat.Operatories].Length
						Appointments.Cur.Op=Defs.Short[(int)DefCat.Operatories][ApptViewItems.VisOps[i]].DefNum;
						if(!DoesOverlap()){
							stillOverlaps=false;
							break;
						}
					}
					if(stillOverlaps){
						for(int i=startingOp;i>=0;i--){
							Appointments.Cur.Op=Defs.Short[(int)DefCat.Operatories][ApptViewItems.VisOps[i]].DefNum;
							if(!DoesOverlap()){
								stillOverlaps=false;
								break;
							}
						}
					}
					if(stillOverlaps){
						MessageBox.Show(Lan.g(this,"Appointment overlaps existing appointment."));
						mouseIsDown=false;
						boolAptMoved=false;
						TempApptSingle.Dispose();
						return;
					}
				}
				if(Appointments.Cur.AptStatus==ApptStatus.Broken){
					Appointments.Cur.AptStatus=ApptStatus.Scheduled;
				}
				if(Appointments.Cur.AptStatus==ApptStatus.UnschedList){
					Appointments.Cur.AptStatus=ApptStatus.Scheduled;
				}
				if(Appointments.Cur.AptStatus==ApptStatus.Next){//if Next appt is on pinboard
					Appointments.Cur.NextAptNum=Appointments.Cur.AptNum;
					Appointments.Cur.AptStatus=ApptStatus.Scheduled;
					Appointments.InsertCur();//now, aptnum is different.
					Procedures.Refresh();
					for(int i=0;i<Procedures.List.Length;i++){
						if(Procedures.List[i].NextAptNum==Patients.Cur.NextAptNum){
							Procedures.Cur=Procedures.List[i];
							Procedures.Cur.AptNum=Appointments.Cur.AptNum;
							Procedures.UpdateCur();
						}
					}
					Procedures.GetProcsForSingle(Appointments.Cur.AptNum,true);
					ContrAppt.CurInfo.Procs=Procedures.ProcsForSingle;
				}
				else{
					Appointments.UpdateCur();
				}
				TempApptSingle.Dispose();
				PinApptSingle.Visible=false;
				ContrApptSingle.PinBoardIsSelected=false;
				ContrApptSingle.SelectedAptNum=Appointments.Cur.AptNum;
				RefreshModuleScreen();//date moving to for this computer
				SetInvalid();//date moving to for other computers
				Appointments.DateSelected=fromDate;
				SetInvalid();//for date moved from for other computers.
				Appointments.DateSelected=Appointments.Cur.AptDateTime;
				mouseIsDown = false;
				boolAptMoved=false;
			}
			else{//responds no, don't move off pinboard.
				mouseIsDown = false;
				boolAptMoved=false;
				//pinIsOccupied=true;
				TempApptSingle.Dispose();
			}
		}//end PinApptSingle_mouseup


		///<summary>Called when releasing an appointment to make sure it does not overlap any other appointment.  Tests all appts for the day, even if not visible.</summary>
		private bool DoesOverlap(){
			bool retVal=false;
			for(int i=0;i<Appointments.ListDay.Length;i++){
				if(Appointments.ListDay[i].AptNum==Appointments.Cur.AptNum){
					continue;
				}
				if(Appointments.ListDay[i].Op!=Appointments.Cur.Op){
					continue;
				}
				//tests start time
				if(Appointments.Cur.AptDateTime.TimeOfDay
					>= Appointments.ListDay[i].AptDateTime.TimeOfDay
					&& Appointments.Cur.AptDateTime.TimeOfDay
					< Appointments.ListDay[i].AptDateTime.TimeOfDay.Add
					(TimeSpan.FromMinutes(Appointments.ListDay[i].Pattern.Length*10))){
					retVal=true;
				}
				//tests stop time
				if(Appointments.Cur.AptDateTime.TimeOfDay.Add
					(TimeSpan.FromMinutes(Appointments.Cur.Pattern.Length*10))
					> Appointments.ListDay[i].AptDateTime.TimeOfDay
					&& Appointments.Cur.AptDateTime.TimeOfDay.Add
					(TimeSpan.FromMinutes(Appointments.Cur.Pattern.Length*10))
					<= Appointments.ListDay[i].AptDateTime.TimeOfDay.Add
					(TimeSpan.FromMinutes(Appointments.ListDay[i].Pattern.Length*10))){
					retVal=true;
				}
				//tests engulf
				if(Appointments.Cur.AptDateTime.TimeOfDay
					<= Appointments.ListDay[i].AptDateTime.TimeOfDay
					&& Appointments.Cur.AptDateTime.TimeOfDay.Add
					(TimeSpan.FromMinutes(Appointments.Cur.Pattern.Length*10))
					>= Appointments.ListDay[i].AptDateTime.TimeOfDay.Add
					(TimeSpan.FromMinutes(Appointments.ListDay[i].Pattern.Length*10))){
					retVal=true;
				}
			}
			return retVal;
		}

		///<summary>Clicked today.</summary>
		private void butToday_Click(object sender, System.EventArgs e) {
			//ContrApptSingle.ApptIsSelected=false;
			Appointments.DateSelected=DateTime.Now;
			RefreshModuleScreen();
		}

		///<summary>Clicked back one day.</summary>
		private void butBack_Click(object sender, System.EventArgs e) {
			//ContrApptSingle.ApptIsSelected=false;
			Appointments.DateSelected=Appointments.DateSelected.AddDays(-1);
			RefreshModuleScreen();
		}

		///<summary>Clicked forward one day.</summary>
		private void butFwd_Click(object sender, System.EventArgs e) {
			//ContrApptSingle.ApptIsSelected=false;
			Appointments.DateSelected=Appointments.DateSelected.AddDays(1);
			RefreshModuleScreen();
		}

		///<summary>Clicked week button, setting the date to the current week, but not necessarily to today.</summary>
		private void butTodayWk_Click(object sender, System.EventArgs e) {
			//ContrApptSingle.ApptIsSelected=false;
			int dayChange = Appointments.DateSelected.DayOfWeek-DateTime.Now.DayOfWeek;
			Appointments.DateSelected=DateTime.Now.AddDays(dayChange);
			RefreshModuleScreen();
		}

		///<summary>Clicked back one week.</summary>
		private void butBackWk_Click(object sender, System.EventArgs e) {
			//ContrApptSingle.ApptIsSelected=false;
			Appointments.DateSelected=Appointments.DateSelected.AddDays(-7);
			RefreshModuleScreen();
		}

		///<summary>Clicked forward one week.</summary>
		private void butFwdWk_Click(object sender, System.EventArgs e) {
			//ContrApptSingle.ApptIsSelected=false;
			Appointments.DateSelected=Appointments.DateSelected.AddDays(7);
			RefreshModuleScreen();
		}

		///<summary>Clicked a date on the calendar.</summary>
		private void Calendar2_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e) {
			//ContrApptSingle.ApptIsSelected=false;
			Appointments.DateSelected=Calendar2.SelectionStart;
			RefreshModuleScreen();
		}

		///<summary>Mouse down event anywhere on the sheet.
		///Could be a blank space or on an actual appointment.</summary>
		private void ContrApptSheet2_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			SheetClickedonOp=Defs.Short[(int)DefCat.Operatories]
				[ApptViewItems.VisOps[ContrApptSheet2.DoubleClickToOp(e.X)]].DefNum;
			SheetClickedonHour=ContrApptSheet2.DoubleClickToHour(e.Y);
			SheetClickedonMin=ContrApptSheet2.DoubleClickToMin(e.Y);
			//date is irrelevant. This is just for the time:
			DateTime SheetClickedonTime=new DateTime(2000,1,1,SheetClickedonHour,SheetClickedonMin,0);
			ContrApptSingle.ClickedAptNum=0;
			for(int i=0;i<Appointments.ListDay.Length;i++){
				if(SheetClickedonOp==Appointments.ListDay[i].Op
					&& Appointments.ListDay[i].AptDateTime.TimeOfDay <= SheetClickedonTime.TimeOfDay
					&& SheetClickedonTime.TimeOfDay < Appointments.ListDay[i].AptDateTime.TimeOfDay
					+TimeSpan.FromMinutes(Appointments.ListDay[i].Pattern.Length*10))
				{
					ContrApptSingle.ClickedAptNum=Appointments.ListDay[i].AptNum;
				}
			}
			Graphics grfx=ContrApptSheet2.CreateGraphics();
			if(ContrApptSingle.ClickedAptNum!=0){//mouse down on appt
				mouseIsDown = true;
				int thisIndex=GetIndex(ContrApptSingle.ClickedAptNum);
				ContrApptSingle.PinBoardIsSelected=false;
				TempApptSingle=new ContrApptSingle();
				TempApptSingle.Visible=false;//otherwise I get a phantom appt while holding mouse down
				Appointments.Cur=ContrApptSingle3[thisIndex].Info.MyApt;
				TempApptSingle.Info=ContrApptSingle3[thisIndex].Info;
				Controls.Add(TempApptSingle);
				TempApptSingle.SetLocation();
				TempApptSingle.BringToFront();
				//mouseOrigin is in Appt sheet coordinates
				mouseOrigin.X=e.X+ContrApptSingle3[thisIndex].Location.X;
				mouseOrigin.Y=e.Y+ContrApptSingle3[thisIndex].Location.Y;
				contOrigin=ContrApptSingle3[thisIndex].Location;
				if(ContrApptSingle.SelectedAptNum!=-1//unselects previously selected unless it's the same appt
					&& ContrApptSingle.SelectedAptNum!=ContrApptSingle.ClickedAptNum){
					int prevSel=GetIndex(ContrApptSingle.SelectedAptNum);
					ContrApptSingle.SelectedAptNum=ContrApptSingle.ClickedAptNum;//has to be done before refresh prev
					if(prevSel!=-1){
						ContrApptSingle3[prevSel].CreateShadow();
						grfx.DrawImage(ContrApptSingle3[prevSel].Shadow,ContrApptSingle3[prevSel].Location.X
							,ContrApptSingle3[prevSel].Location.Y);
					}
				}
				ContrApptSingle.SelectedAptNum=ContrApptSingle.ClickedAptNum;//again, in case missed in loop aboveContrApptSingle.SelectedAptNum=ContrApptSingle.ClickedAptNum;
				TempApptSingle.CreateShadow();
				Patients.GetFamily(Appointments.ListDay[thisIndex].PatNum);
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString+" - "+Patients.GetCurNameLF();
				ContrApptSingle3[thisIndex].CreateShadow();
				grfx.DrawImage(ContrApptSingle3[thisIndex].Shadow,ContrApptSingle3[thisIndex].Location.X
					,ContrApptSingle3[thisIndex].Location.Y);
				FillPanelPatient();
				}
			else{//not on appt
				ContrApptSingle.PinBoardIsSelected=false;
				Patients.PatIsLoaded=false;
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString;
				if(ContrApptSingle.SelectedAptNum!=-1){
					int prev=GetIndex(ContrApptSingle.SelectedAptNum);
					ContrApptSingle.SelectedAptNum=-1;
					if(prev!=-1){
						ContrApptSingle3[prev].CreateShadow();
						grfx.DrawImage(ContrApptSingle3[prev].Shadow,ContrApptSingle3[prev].Location.X
							,ContrApptSingle3[prev].Location.Y);
					}
				}
				FillPanelPatient();
			}
			grfx.Dispose();
			if(PinApptSingle.Visible){
				PinApptSingle.CreateShadow();
				PinApptSingle.Refresh();
			}
			CreateAptShadows();
			//CreateHundredShadows();
		}

		///<summary>Dragging an appointment.</summary>
		private void ContrApptSheet2_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(!mouseIsDown) return;
			int thisIndex=GetIndex(ContrApptSingle.SelectedAptNum);
			//this enhances double clicking, but I would like to find another way:
			if ((Math.Abs(e.X+ContrApptSingle3[thisIndex].Location.X-mouseOrigin.X)<3)//enhances double clicking
				&(Math.Abs(e.Y+ContrApptSingle3[thisIndex].Location.Y-mouseOrigin.Y)<3)){
				boolAptMoved=false;
				return;
			}
			boolAptMoved=true;
			Point tempPoint=new Point();
			tempPoint.X=contOrigin.X+(e.X+ContrApptSingle3[thisIndex].Location.X)
				-mouseOrigin.X+ContrApptSheet2.Location.X+panelSheet.Location.X;
			tempPoint.Y=contOrigin.Y+(e.Y+ContrApptSingle3[thisIndex].Location.Y)
				-mouseOrigin.Y+ContrApptSheet2.Location.Y+panelSheet.Location.Y;
			TempApptSingle.Location=tempPoint;
			TempApptSingle.Visible=true;
		}

		///<summary>Usually dropping an appointment to a new location.</summary>
		private void ContrApptSheet2_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(!mouseIsDown) return;
			int thisIndex=GetIndex(ContrApptSingle.SelectedAptNum);
			if((Math.Abs(e.X+ContrApptSingle3[thisIndex].Location.X-mouseOrigin.X)<7)
				&&(Math.Abs(e.Y+ContrApptSingle3[thisIndex].Location.Y-mouseOrigin.Y)<7)){
				boolAptMoved=false;
			}
			if(!boolAptMoved){//it was a click with no drag
				mouseIsDown=false;
				TempApptSingle.Dispose();
				//PinApptSingle.Refresh();
				return;
			}
			if(TempApptSingle.Location.X>ContrApptSheet2.Width){//place a copy on pinboard
				int prevSel=GetIndex(ContrApptSingle.SelectedAptNum);
				CurInfo=TempApptSingle.Info;
				CurToPinBoard();//sets selectedAptNum=-1. do before refresh prev
				if(prevSel!=-1){
					CreateAptShadows();
					ContrApptSheet2.DrawShadow();
					//CreateHundredShadows();
				}
				FillPanelPatient();
				TempApptSingle.Dispose();
			}
			else if(MessageBox.Show(Lan.g(this,"Move Appointment?"),"",MessageBoxButtons.OKCancel)
				==DialogResult.OK)
			{
				//convert loc to new time
				//Appointments.Cur = Appointments.List[ContrApptSingle.SelectedIndex];
				int tHr=ContrApptSheet2.ConvertToHour
					(TempApptSingle.Location.Y-ContrApptSheet2.Location.Y-panelSheet.Location.Y);
				int tMin=ContrApptSheet2.ConvertToMin
					(TempApptSingle.Location.Y-ContrApptSheet2.Location.Y-panelSheet.Location.Y);
				DateTime tDate=Appointments.Cur.AptDateTime.Date;
				Appointments.Cur=TempApptSingle.Info.MyApt;
				Appointments.Cur.AptDateTime=new DateTime(tDate.Year,tDate.Month,tDate.Day,tHr,tMin,0);
				Appointments.Cur.Op=Defs.Short[(int)DefCat.Operatories]
					[ApptViewItems.VisOps
					[ContrApptSheet2.ConvertToOp(TempApptSingle.Location.X-ContrApptSheet2.Location.X)]].DefNum;
				if(DoesOverlap()){
					int startingOp=ApptViewItems.GetIndexOp(Appointments.Cur.Op);
					bool stillOverlaps=true;
					for(int i=startingOp;i<ApptViewItems.VisOps.Length;i++){
						//Defs.Short[(int)DefCat.Operatories]
						Appointments.Cur.Op=Defs.Short[(int)DefCat.Operatories][ApptViewItems.VisOps[i]].DefNum;
						if(!DoesOverlap()){
							stillOverlaps=false;
							break;
						}
					}
					if(stillOverlaps){
						for(int i=startingOp;i>=0;i--){
							Appointments.Cur.Op=Defs.Short[(int)DefCat.Operatories][ApptViewItems.VisOps[i]].DefNum;
							if(!DoesOverlap()){
								stillOverlaps=false;
								break;
							}
						}
					}
					if(stillOverlaps){
						MessageBox.Show(Lan.g(this,"Appointment overlaps existing appointment."));
						mouseIsDown=false;
						boolAptMoved=false;
						TempApptSingle.Dispose();
						return;
					}
				}//end if DoesOverlap
				if(Appointments.Cur.AptStatus==ApptStatus.Broken){
					Appointments.Cur.AptStatus=ApptStatus.Scheduled;
				}
				Appointments.UpdateCur();
				RefreshModuleScreen();
				SetInvalid();
				mouseIsDown = false;
				boolAptMoved=false;
				TempApptSingle.Dispose();
			}//end if move appt
			else{
				mouseIsDown = false;
				boolAptMoved=false;
				TempApptSingle.Dispose();
			}
		}

		///<summary>Double click on appt sheet or on a single appointment.</summary>
		private void ContrApptSheet2_DoubleClick(object sender, System.EventArgs e) {
			mouseIsDown=false;
			//this logic is a little different than mouse down for now because on the first click of a 
			//double click, an appointment control is created under the mouse.
			if(ContrApptSingle.ClickedAptNum!=0){//on appt	
				TempApptSingle.Dispose();
				FormApptEdit FormApptEdit2 = new FormApptEdit();
				FormApptEdit2.ShowDialog();
				if(FormApptEdit2.DialogResult==DialogResult.OK){
					if(DoesOverlap()){
						MessageBox.Show(Lan.g(this,"Appointment is too long and would overlap another appointment.  Automatically shortened to fit."));
						while(DoesOverlap()){
							Appointments.Cur.Pattern
								=Appointments.Cur.Pattern.Substring(0,Appointments.Cur.Pattern.Length-1);
							if(Appointments.Cur.Pattern.Length==1){
								break;
							}
						}
						Appointments.UpdateCur();
					}
					RefreshModuleScreen();
					SetInvalid();
				}
			}
			else{//not on apt
				FormPatientSelect FormPS=new FormPatientSelect();
				//FormPS.
				FormPS.ShowDialog();
				if(FormPS.DialogResult!=DialogResult.OK){
					return;
				}
				RefreshModuleData();
				if(FormPS.NewPatientAdded){
					Appointments.Cur=new Appointment();
					Appointments.Cur.PatNum=Patients.Cur.PatNum;
					Appointments.Cur.Pattern="/X/";
					if(Patients.Cur.PriProv==0){
						Appointments.Cur.ProvNum=PIn.PInt(((Pref)Prefs.HList["PracticeDefaultProv"]).ValueString);
					}
					else{			
						Appointments.Cur.ProvNum=Patients.Cur.PriProv;
					}
					Appointments.Cur.ProvHyg=Patients.Cur.SecProv;
					Appointments.Cur.AptStatus=ApptStatus.Scheduled;
					DateTime d=Appointments.DateSelected;
					Appointments.Cur.AptDateTime=new DateTime(d.Year,d.Month,d.Day
						,ContrAppt.SheetClickedonHour,ContrAppt.SheetClickedonMin,0);
					Appointments.Cur.Op=ContrAppt.SheetClickedonOp;
					Appointments.InsertCur();
					FormApptEdit FormAE=new FormApptEdit();
					FormAE.IsNew=true;
					FormAE.ShowDialog();
					if(FormAE.DialogResult==DialogResult.OK){
						RefreshModuleScreen();
						SetInvalid();
					}
				}
				else{
					DisplayOtherDlg(true);//this also refreshes screen if needed.
				}
			}
		}

		///<summary>Displays the Other Appointments for the current patient, then refreshes screen as needed.</summary>
		/// <param name="initialClick">Specifies whether the user doubleclicked on a blank time to get to this dialog.</param>
		private void DisplayOtherDlg(bool initialClick){
			if(!Patients.PatIsLoaded)
				return;
			FormApptsOther FormAO=new FormApptsOther();
			FormAO.InitialClick=initialClick;
			FormAO.ShowDialog();
			if(FormAO.OResult==OtherResult.Cancel){
				return;
			}
			switch(FormAO.OResult){
				case OtherResult.CopyToPinBoard:
					AssembleInfo();
					CurToPinBoard();
					RefreshModuleScreen();
					break;
				case OtherResult.NewToPinBoard:
					AssembleInfo();
					CurToPinBoard();
					RefreshModuleScreen();
					break;
				case OtherResult.CreateNew:
					ContrApptSingle.SelectedAptNum=Appointments.Cur.AptNum;
					RefreshModuleScreen();
					SetInvalid();
					break;
				case OtherResult.GoTo:
					ContrApptSingle.SelectedAptNum=Appointments.Cur.AptNum;
					Appointments.DateSelected=Appointments.Cur.AptDateTime;
					RefreshModuleScreen();
					break;
			}
		}

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e) {
			//using buttons for now
			/*switch (toolBar1.Buttons[toolBar1.Buttons.IndexOf(e.Button)].Tag.ToString()){
				case "Pat":
					
				break;
				case "Unsched":*/
		}

		private void butPat_Click(object sender, System.EventArgs e) {
			FormPatientSelect formSelectPatient2 = new FormPatientSelect();
			formSelectPatient2.ShowDialog();
			if(formSelectPatient2.DialogResult != DialogResult.OK){
				return;
			}
			RefreshModuleData();//refreshes to current pt
			DisplayOtherDlg(false);
		}

		private void butUnschedList_Click(object sender, System.EventArgs e) {
			FormUnsched FormUnsched2=new FormUnsched();
			FormUnsched2.ShowDialog();
			if(FormUnsched2.PinClicked){
				AssembleInfo();
				CurToPinBoard();
				RefreshModuleScreen();
			}
		}

		private void butRecall_Click(object sender, System.EventArgs e) {
			FormRecall FormRecall2=new FormRecall();
			FormRecall2.ShowDialog();
			if(FormRecall2.PinClicked){
				AssembleInfo();
				CurToPinBoard();
				RefreshModuleScreen();
			}
		}

		private void butTrack_Click(object sender, System.EventArgs e) {
			Cursor=Cursors.WaitCursor;
			FormTrackNext FormTN=new FormTrackNext();
			FormTN.ShowDialog();
			if(FormTN.PinClicked){
				AssembleInfo();
				CurToPinBoard();
				//RefreshModuleScreen();
			}
			Cursor=Cursors.Default;
			ModuleSelected();
		}

		private void butPrint_Click(object sender, System.EventArgs e) {
			if(PrinterSettings.InstalledPrinters.Count==0){
				MessageBox.Show(Lan.g(this,"Printer not installed"));
			}
			else{
				PrintReport(false);
			}
		}

		///<summary></summary>
		public void PrintReport(bool justPreview){
			pd2=new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			//pd2.DefaultPageSettings.Margins= new Margins(10,40,40,60);
			PrintDocument tempPD = new PrintDocument();
			tempPD.PrinterSettings.PrinterName=Computers.Cur.PrinterName;
			if(tempPD.PrinterSettings.IsValid){
				pd2.PrinterSettings.PrinterName=Computers.Cur.PrinterName;
			}
			//uses default printer if selected printer not valid
			tempPD.Dispose();
			try  {
				if(justPreview){
					pView.printPreviewControl2.Document=pd2;
  				pView.ShowDialog();
				}
				else{
					printDialog2=new PrintDialog();
					printDialog2.Document=pd2;
					printDialog2.AllowPrintToFile=false;
					printDialog2.AllowSelection=false;
					printDialog2.AllowSomePages=false;
					printDialog2.ShowHelp=false;
					if(printDialog2.ShowDialog()==DialogResult.OK){
						//MessageBox.Show(Lan.g(this,printDialog2.PrinterSettings.Copies.ToString()));
						//for(int i=0;i<printDialog2.PrinterSettings.Copies;i++){
						pd2.Print();
						//}
					}
				}
			}
			catch{
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
	}

		private void pd2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
			//MessageBox.Show(e.PageSettings.PrinterSettings.Copies.ToString());
      int xPos=0;//starting pos
			int yPos=(int)27.5;//starting pos
      //Print Title
 			string title = "Daily Appointments";
			Font titleFont=new Font("Arial",14,FontStyle.Bold);
			float xTitle = (float)(400-((e.Graphics.MeasureString(title,titleFont).Width/2)));
			e.Graphics.DrawString(title,titleFont,Brushes.Black,xTitle,yPos);//centered
			//Print Date
 			string date = Appointments.DateSelected.DayOfWeek.ToString()+"   "
				+Appointments.DateSelected.ToShortDateString();
			Font dateFont=new Font("Arial",10,FontStyle.Regular);
			float xDate = (float)(400-((e.Graphics.MeasureString(date,dateFont).Width/2)));
			yPos+=25;
			e.Graphics.DrawString(Lan.g(this,date),dateFont,Brushes.Black,xDate,yPos);//centered

			//FIGURING OUT SIZE OF IMAGE

			int recHeight=0;
      int recWidth=0;
			int recX=0;
			int recY=0;
//NEW change to include non-defaults
      ArrayList AListStart=new ArrayList();
      ArrayList AListStop=new ArrayList();
      DateTime StartTime;
      DateTime StopTime;  
      Rectangle imageRect;  //holds new dimensions for temp image
		  Bitmap imageTemp;  //clone of shadow image with correct dimensions depending on day of week
      bool IsDefault=true; 	
      //Schedules.CurDate=Appointments.DateSelected;
			Schedules.RefreshDay(Appointments.DateSelected);
      //Schedules.GetDayList();
      if(Schedules.ListDay.Length > 0){
        for(int i=0;i<Schedules.ListDay.Length;i++){
          AListStart.Add(Schedules.ListDay[i].StartTime);
          AListStop.Add(Schedules.ListDay[i].StopTime); 
        } 
        IsDefault=false;
      }
      if(IsDefault){	
				for(int i=0;i<SchedDefaults.List.Length;i++){
					if(SchedDefaults.List[i].DayOfWeek==(int)Appointments.DateSelected.DayOfWeek){
            AListStart.Add(SchedDefaults.List[i].StartTime);
            AListStop.Add(SchedDefaults.List[i].StopTime); 
					}
				}
      }
			if (AListStart.Count > 0)  {  //makes sure there is a value, if none, then day off
        StartTime=(DateTime)AListStart[0]; 
				for (int i=0; i<AListStart.Count;i++)  {
          //if (A) OR (B AND C)
					if((((DateTime)(AListStart[i])).Hour < StartTime.Hour) || (((DateTime)(AListStart[i])).Hour==StartTime.Hour && ((DateTime)(AListStart[i])).Minute < StartTime.Minute)){
            StartTime=(DateTime)AListStart[i];   
					}
				}
      }
      else{
        StartTime=new DateTime(Appointments.DateSelected.Year,Appointments.DateSelected.Month,Appointments.DateSelected.Day,0,0,0); 
      }
			if (AListStop.Count > 0)  {  //makes sure there is a value, if none, then day off
        StopTime=(DateTime)AListStop[0]; 
				for (int i=0; i<AListStop.Count;i++)  {
          //if (A) OR (B AND C)
					if((((DateTime)(AListStop[i])).Hour > StopTime.Hour) || (((DateTime)(AListStop[i])).Hour==StopTime.Hour && ((DateTime)(AListStop[i])).Minute > StopTime.Minute)){
            StopTime=(DateTime)AListStop[i];   
					}
				}
      } 
      else{
        StopTime=new DateTime(Appointments.DateSelected.Year,Appointments.DateSelected.Month,Appointments.DateSelected.Day,0,0,0); 
      }
      if (StartTime.Hour > 0 && StopTime.Hour > 0)  {
			  recY=(int)(ContrApptSheet.Lh*6*StartTime.Hour);
			  recWidth=(int)ContrApptSheet2.Shadow.Width;
        if(StopTime.Minute > 0) 
				  recHeight=(int)((ContrApptSheet.Lh*6*(StopTime.Hour-StartTime.Hour+1)));
        else{
				  recHeight=(int)((ContrApptSheet.Lh*6*(StopTime.Hour-StartTime.Hour)));
        }
				recHeight+=ContrApptSheet.Lh*6+1;//add extra hour to end
//END OF CHANGES
			}
			else  {
			  recY=(int)(-ContrApptSheet2.Location.Y);
			  recWidth=(int)ContrApptSheet2.Shadow.Width;
				if ((int)((-ContrApptSheet2.Location.Y)+(ContrApptSheet.Lh*78)+1)>(ContrApptSheet.Lh*6*24))
    		  recHeight=(int)(ContrApptSheet.Lh*6*24)-(-ContrApptSheet2.Location.Y);     
        else
			    recHeight=(int)(ContrApptSheet.Lh*78)+1;  //figured to show 13 hours of sched time (6+13) lh=10 min block
      } 
      imageRect = new Rectangle(recX,recY,recWidth,recHeight);
			
			imageTemp=ContrApptSheet2.Shadow.Clone(imageRect,PixelFormat.DontCare);  //clones image and sets size to only show the time open for that day

			int horRes=100;
			int vertRes=100;

			if(imageTemp.Width>775)  {
				horRes+=(int)((imageTemp.Width-775)/8);
				if((imageTemp.Width-750)%8!=0)
				  horRes+=1;
      }
	    if (imageTemp.Height>960)  {
        vertRes+=((imageTemp.Height-960)/8);
				if((imageTemp.Height-960)%8!=0)
				  vertRes+=1;
			}
      imageTemp.SetResolution(horRes,vertRes);  //sets resolution to fit image on screen
      
			//HEADER POSITION AND PRINTING		 
			
			string[] headers = new string[Defs.Short[(int)DefCat.Operatories].Length];
			Font headerFont=new Font("Arial",8);
      yPos+=30;   //y Position  
			//need to size to horizontal resolution if bigger than 100
      xPos+=(int)(ContrApptSheet.TimeWidth+(ContrApptSheet.ProvWidth*ContrApptSheet.ProvCount)*(100/imageTemp.HorizontalResolution));  // x position
			int xCenter=0;

			for (int i=0;i<Defs.Short[(int)DefCat.Operatories].Length;i++){
				headers[i]=Defs.Short[(int)DefCat.Operatories][i].ItemName;	
				xCenter=(int)((ContrApptSheet.ColWidth/2)-(e.Graphics.MeasureString(headers[i],headerFont).Width/2));
			  e.Graphics.DrawString(Lan.g(this,headers[i]),headerFont,Brushes.Black,(int)((xPos+xCenter)*(100/imageTemp.HorizontalResolution)),yPos);
        xPos+=ContrApptSheet.ColWidth;
			}   
			//DRAW IMAGE:
      xPos=0;
			yPos+=12;

      e.Graphics.DrawImage(imageTemp,xPos,yPos); 
		}

		///<summary>Clears the pinboard.</summary>
		private void butClearPin_Click(object sender, System.EventArgs e) {
			if(!PinApptSingle.Visible){
				return;
			}
			PinApptSingle.Visible=false;
			Appointments.Cur=PinApptSingle.Info.MyApt;
			Patients.Cur.PatNum=Appointments.Cur.PatNum;
			RefreshModuleData();
			ContrApptSingle.SelectedAptNum=-1;
			ContrApptSingle.PinBoardIsSelected=false;
			if(Appointments.Cur.AptStatus==ApptStatus.UnschedList){//on unscheduled list
				//do nothing to database
			}
			else if(Appointments.Cur.AptDateTime.Year<1880){//not already scheduled
				if(Appointments.Cur.AptNum==Patients.Cur.NextAptNum){//if next apt
					//do nothing except remove it from pinboard
				}
				else{//for normal appt:
					//this gets rid of new appointments that never made it off the pinvboard
					Procedures.UnattachProcsInAppt(Appointments.Cur.AptNum);
					Appointments.DeleteCur();
				}
			}
			Patients.PatIsLoaded=false;
			RefreshModuleScreen();
		}

		///<summary>The scrollbar has been moved by the user.</summary>
		private void vScrollBar1_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e) {
			if(e.Type==ScrollEventType.ThumbTrack){//moving
				ContrApptSheet2.IsScrolling=true;
				ContrApptSheet2.Location=new Point(0,-e.NewValue);
			}
			if(e.Type==ScrollEventType.EndScroll){//done moving
				ContrApptSheet2.IsScrolling=true;
				ContrApptSheet2.Location=new Point(0,-e.NewValue);
				ContrApptSheet2.IsScrolling=false;
				ContrApptSheet2.Select();
			}			
		}

		///<summary>Occurs whenever the panel holding the appt sheet is resized.</summary>
		private void panelSheet_Resize(object sender, System.EventArgs e) {
			vScrollBar1.Maximum=ContrApptSheet2.Height-panelSheet.Height+vScrollBar1.LargeChange;
		}

		///<summary>Sends current appointment to unscheduled list.</summary>
		private void butUnsched_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"Send Appointment to Unscheduled List?")
				,"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			Appointments.Cur.AptStatus=ApptStatus.UnschedList;
			Appointments.UpdateCur();
			RefreshModuleScreen();
			SetInvalid();
		}

		private void butBreak_Click(object sender, System.EventArgs e) {
			int thisIndex=GetIndex(ContrApptSingle.SelectedAptNum);
			Appointments.Cur.AptStatus=ApptStatus.Broken;
			Appointments.UpdateCur();
			RefreshModuleScreen();
			SetInvalid();
			FormAdjust FormAdjust2=new FormAdjust();
			FormAdjust2.IsNew=true;
			FormAdjust2.ShowDialog();
		}

		private void butComplete_Click(object sender, System.EventArgs e) {
			int thisIndex=GetIndex(ContrApptSingle.SelectedAptNum);
			Appointments.Cur.AptStatus=ApptStatus.Complete;
			Procedures.SetCompleteInAppt();//loops through each proc
			Appointments.UpdateCur();
			RefreshModuleScreen();
			SetInvalid();
			//ContrApptSingle3[thisIndex].Info.MyApt.AptStatus=ApptStatus.Complete;
			//ContrApptSingle3[thisIndex].Refresh();
		}
	
		private void butDelete_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"Delete Appointment?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			Procedures.UnattachProcsInAppt(Appointments.Cur.AptNum);
			Appointments.DeleteCur();
			ContrApptSingle.SelectedAptNum=-1;
			ContrApptSingle.PinBoardIsSelected=false;
			Patients.PatIsLoaded=false;
			RefreshModuleScreen();
			SetInvalid();
		}

		private void butOther_Click(object sender, System.EventArgs e) {
			DisplayOtherDlg(false);
		}

		private void textMedicalNote_TextChanged(object sender, System.EventArgs e) {
		
		}

		private void listConfirmed_SelectedIndexChanged(object sender, System.EventArgs e) {
			listConfirmed.SelectedIndex=-1;
		}

		private void listConfirmed_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listConfirmed.IndexFromPoint(e.X,e.Y)==-1){
				return;
			}
			Appointments.Cur.Confirmed
				=Defs.Short[(int)DefCat.ApptConfirmed][listConfirmed.IndexFromPoint(e.X,e.Y)].DefNum;
			Appointments.UpdateCur();
			RefreshModuleScreen();
			SetInvalid();
		}

		private void butAptModNoteEdit_Click(object sender, System.EventArgs e) {
			FormNoteApptMod FormNAM=new FormNoteApptMod();
			FormNAM.ShowDialog();
			textApptModNote.Text=Patients.Cur.ApptModNote;
		}

		private void button1_Click_1(object sender, System.EventArgs e) {
			MessageBox.Show(Lan.g(this,this.GetType().Name));
		}

		//private void timerTimeIndic_Tick(object sender, System.EventArgs e) {
			//if(FormOpenDental.ActiveForm.WindowState!=FormWindowState.Minimized){
		//}

		///<summary></summary>
		public void TickRefresh(){
			try{
				ContrApptSheet2.CreateShadow();
				CreateAptShadows();
				ContrApptSheet2.DrawShadow();
			}
			catch{
				//prevents rare malfunctions. For instance, during editing of views, it tickrefresh happens.
			}
			//GC.Collect();	
		}

		

		

	


		

	}//end class

	///<summary></summary>
	public struct InfoApt{
		///<summary></summary>
		public bool IsNext;
		///<summary></summary>
		public string PatientName;
		///<summary></summary>
		public string CreditAndIns;
		///<summary></summary>
		public string[] Procs;
		//public int MyIndex;
		///<summary></summary>
		public string[] Lines;
		///<summary></summary>
		public Appointment MyApt;
	}


}//end namespace
