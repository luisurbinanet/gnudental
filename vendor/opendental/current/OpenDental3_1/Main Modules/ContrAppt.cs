/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
//using System.G
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;
using OpenDental.UI;

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
		private System.Windows.Forms.PictureBox pictureBox1;
		private bool boolAptMoved=false;
		//private FormUnsched FormUnsched2;
		private OpenDental.UI.Button butToday;
		private OpenDental.UI.Button butTodayWk;
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
		///<summary></summary>
		public static InfoApt CurInfo;
		private System.Windows.Forms.Panel panelNotes;
		private System.Windows.Forms.TextBox textApptModNote;
		private OpenDental.UI.Button butAptModNoteEdit;
		private System.Drawing.Printing.PrintDocument pd2;
		private System.Windows.Forms.PrintDialog printDialog2;
		///<summary></summary>
		public static Size PinboardSize=new Size(106,92);
		private OpenDental.UI.Button butBack;
		private OpenDental.UI.Button butClearPin;
		private OpenDental.UI.Button butBackWk;
		private OpenDental.UI.Button butFwdWk;
		private OpenDental.UI.Button butFwd;
		private System.Windows.Forms.Panel panelAptInfo;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelPhoneType;
		private System.Windows.Forms.TextBox textPhone;
		private OpenDental.UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.ContextMenu menuApt;
		private System.Windows.Forms.TextBox textLab;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textProduction;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox comboView;
		private System.Windows.Forms.ContextMenu menuPatient;	
		///<summary></summary>
	  public FormRpPrintPreview pView = new FormRpPrintPreview();
		private OpenDental.UI.Button butOther;
		private bool cardPrintFamily;
		private Patient PatCur;
		private Family FamCur;
		private InsPlan[] PlanList;
		///<summary></summary>
		[Category("Data"),Description("Occurs when user changes current patient, usually by clicking on the Select Patient button.")]
		public event PatientSelectedEventHandler PatientSelected=null;

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
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.ContrApptSheet2 = new OpenDental.ContrApptSheet();
			this.Calendar2 = new System.Windows.Forms.MonthCalendar();
			this.labelDate = new System.Windows.Forms.Label();
			this.labelDate2 = new System.Windows.Forms.Label();
			this.butToday = new OpenDental.UI.Button();
			this.butTodayWk = new OpenDental.UI.Button();
			this.panelArrows = new System.Windows.Forms.Panel();
			this.butBack = new OpenDental.UI.Button();
			this.butFwd = new OpenDental.UI.Button();
			this.butBackWk = new OpenDental.UI.Button();
			this.butFwdWk = new OpenDental.UI.Button();
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
			this.butOther = new OpenDental.UI.Button();
			this.panelCalendar = new System.Windows.Forms.Panel();
			this.textProduction = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textLab = new System.Windows.Forms.TextBox();
			this.comboView = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.butClearPin = new OpenDental.UI.Button();
			this.panelOps = new System.Windows.Forms.Panel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.panelNarrow = new System.Windows.Forms.Panel();
			this.panelNotes = new System.Windows.Forms.Panel();
			this.textPhone = new System.Windows.Forms.TextBox();
			this.labelPhoneType = new System.Windows.Forms.Label();
			this.butAptModNoteEdit = new OpenDental.UI.Button();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.printDialog2 = new System.Windows.Forms.PrintDialog();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.menuApt = new System.Windows.Forms.ContextMenu();
			this.menuPatient = new System.Windows.Forms.ContextMenu();
			this.panelPinBoard.SuspendLayout();
			this.panelArrows.SuspendLayout();
			this.panelSheet.SuspendLayout();
			this.panelAptInfo.SuspendLayout();
			this.panelCalendar.SuspendLayout();
			this.panelNotes.SuspendLayout();
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
			this.butToday.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butToday.Autosize = false;
			this.butToday.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butToday.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
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
			this.butTodayWk.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butTodayWk.Autosize = false;
			this.butTodayWk.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTodayWk.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
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
			this.butBack.Autosize = true;
			this.butBack.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBack.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
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
			this.butFwd.Autosize = true;
			this.butFwd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFwd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
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
			this.butBackWk.Autosize = true;
			this.butBackWk.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBackWk.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
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
			this.butFwdWk.Autosize = true;
			this.butFwdWk.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFwdWk.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
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
			this.textApptModNote.Location = new System.Drawing.Point(0, 148);
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
			this.textMedicalNote.Location = new System.Drawing.Point(0, 108);
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
			this.label1.Location = new System.Drawing.Point(0, 134);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(160, 18);
			this.label1.TabIndex = 36;
			this.label1.Text = "Appointment module notes:";
			// 
			// textAddressNote
			// 
			this.textAddressNote.BackColor = System.Drawing.Color.White;
			this.textAddressNote.ForeColor = System.Drawing.Color.Red;
			this.textAddressNote.Location = new System.Drawing.Point(0, 72);
			this.textAddressNote.Multiline = true;
			this.textAddressNote.Name = "textAddressNote";
			this.textAddressNote.ReadOnly = true;
			this.textAddressNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textAddressNote.Size = new System.Drawing.Size(202, 20);
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
			this.textFinancialNote.Size = new System.Drawing.Size(202, 20);
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
			this.label5.Location = new System.Drawing.Point(0, 94);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(134, 14);
			this.label5.TabIndex = 42;
			this.label5.Text = "Urgent medical notes:";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(0, 58);
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
			this.panelAptInfo.Location = new System.Drawing.Point(680, 365);
			this.panelAptInfo.Name = "panelAptInfo";
			this.panelAptInfo.Size = new System.Drawing.Size(204, 142);
			this.panelAptInfo.TabIndex = 45;
			// 
			// listConfirmed
			// 
			this.listConfirmed.Location = new System.Drawing.Point(38, 2);
			this.listConfirmed.Name = "listConfirmed";
			this.listConfirmed.Size = new System.Drawing.Size(73, 108);
			this.listConfirmed.TabIndex = 75;
			this.listConfirmed.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listConfirmed_MouseDown);
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
			this.butOther.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOther.Autosize = true;
			this.butOther.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOther.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOther.Image = ((System.Drawing.Image)(resources.GetObject("butOther.Image")));
			this.butOther.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butOther.Location = new System.Drawing.Point(680, 478);
			this.butOther.Name = "butOther";
			this.butOther.Size = new System.Drawing.Size(118, 28);
			this.butOther.TabIndex = 76;
			this.butOther.Text = "Other Appts";
			this.butOther.Click += new System.EventHandler(this.butOther_Click);
			// 
			// panelCalendar
			// 
			this.panelCalendar.Controls.Add(this.textProduction);
			this.panelCalendar.Controls.Add(this.label7);
			this.panelCalendar.Controls.Add(this.textLab);
			this.panelCalendar.Controls.Add(this.comboView);
			this.panelCalendar.Controls.Add(this.label3);
			this.panelCalendar.Controls.Add(this.label2);
			this.panelCalendar.Controls.Add(this.butClearPin);
			this.panelCalendar.Controls.Add(this.Calendar2);
			this.panelCalendar.Controls.Add(this.labelDate);
			this.panelCalendar.Controls.Add(this.labelDate2);
			this.panelCalendar.Controls.Add(this.panelPinBoard);
			this.panelCalendar.Controls.Add(this.panelArrows);
			this.panelCalendar.Location = new System.Drawing.Point(680, 28);
			this.panelCalendar.Name = "panelCalendar";
			this.panelCalendar.Size = new System.Drawing.Size(204, 337);
			this.panelCalendar.TabIndex = 46;
			// 
			// textProduction
			// 
			this.textProduction.BackColor = System.Drawing.Color.White;
			this.textProduction.Location = new System.Drawing.Point(148, 318);
			this.textProduction.Name = "textProduction";
			this.textProduction.ReadOnly = true;
			this.textProduction.Size = new System.Drawing.Size(53, 20);
			this.textProduction.TabIndex = 38;
			this.textProduction.Text = "$100";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(1, 320);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(141, 15);
			this.label7.TabIndex = 39;
			this.label7.Text = "Daily Production";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textLab
			// 
			this.textLab.BackColor = System.Drawing.Color.White;
			this.textLab.Location = new System.Drawing.Point(75, 298);
			this.textLab.Name = "textLab";
			this.textLab.ReadOnly = true;
			this.textLab.Size = new System.Drawing.Size(128, 20);
			this.textLab.TabIndex = 36;
			this.textLab.Text = "All Received";
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
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(2, 300);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(71, 15);
			this.label3.TabIndex = 37;
			this.label3.Text = "Lab Cases";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.butClearPin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butClearPin.Autosize = true;
			this.butClearPin.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClearPin.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
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
			this.panelNarrow.Location = new System.Drawing.Point(903, 18);
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
			this.panelNotes.Location = new System.Drawing.Point(680, 508);
			this.panelNotes.Name = "panelNotes";
			this.panelNotes.Size = new System.Drawing.Size(204, 188);
			this.panelNotes.TabIndex = 50;
			// 
			// textPhone
			// 
			this.textPhone.Location = new System.Drawing.Point(102, 38);
			this.textPhone.Name = "textPhone";
			this.textPhone.ReadOnly = true;
			this.textPhone.TabIndex = 53;
			this.textPhone.Text = "";
			// 
			// labelPhoneType
			// 
			this.labelPhoneType.Location = new System.Drawing.Point(4, 40);
			this.labelPhoneType.Name = "labelPhoneType";
			this.labelPhoneType.Size = new System.Drawing.Size(100, 14);
			this.labelPhoneType.TabIndex = 52;
			this.labelPhoneType.Text = "Home Phone:";
			this.labelPhoneType.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butAptModNoteEdit
			// 
			this.butAptModNoteEdit.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAptModNoteEdit.Autosize = true;
			this.butAptModNoteEdit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAptModNoteEdit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAptModNoteEdit.Location = new System.Drawing.Point(150, 129);
			this.butAptModNoteEdit.Name = "butAptModNoteEdit";
			this.butAptModNoteEdit.Size = new System.Drawing.Size(52, 20);
			this.butAptModNoteEdit.TabIndex = 51;
			this.butAptModNoteEdit.Text = "Edit";
			this.butAptModNoteEdit.Click += new System.EventHandler(this.butAptModNoteEdit_Click);
			// 
			// pd2
			// 
			this.pd2.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pd2_PrintPage);
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.ImageList = this.imageListMain;
			this.ToolBarMain.Location = new System.Drawing.Point(680, 2);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(203, 29);
			this.ToolBarMain.TabIndex = 73;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// ContrAppt
			// 
			this.Controls.Add(this.butOther);
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.panelNotes);
			this.Controls.Add(this.panelOps);
			this.Controls.Add(this.panelCalendar);
			this.Controls.Add(this.panelAptInfo);
			this.Controls.Add(this.panelSheet);
			this.Controls.Add(this.panelNarrow);
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
		public void ModuleSelected(int patNum){
			//the scrollbar logic cannot be moved to someplace where it will be activated while working in apptbook
			//MessageBox.Show("about to refresh");
			RefreshVisops();//forces reset after changing databases
			this.SuspendLayout();
			vScrollBar1.Enabled=true;
			vScrollBar1.Minimum=0;
			vScrollBar1.LargeChange=12*ContrApptSheet.Lh;//12 rows
			vScrollBar1.Maximum=ContrApptSheet2.Height-panelSheet.Height+vScrollBar1.LargeChange;
			//Max is set again in Resize event
			vScrollBar1.SmallChange=6*ContrApptSheet.Lh;//6 rows
			if(vScrollBar1.Value==0 
				&& 8*ContrApptSheet.RowsPerHr*ContrApptSheet.Lh<vScrollBar1.Maximum-vScrollBar1.LargeChange)
			{
				vScrollBar1.Value=8*ContrApptSheet.RowsPerHr*ContrApptSheet.Lh;//8am
			}
			if(vScrollBar1.Value>vScrollBar1.Maximum-vScrollBar1.LargeChange){
				vScrollBar1.Value=vScrollBar1.Maximum-vScrollBar1.LargeChange;
			}
			ContrApptSheet2.MouseWheel+=new MouseEventHandler(ContrApptSheet2_MouseWheel);
			ContrApptSheet2.Location=new Point(0,-vScrollBar1.Value);
			panelOps.Controls.Clear();
			for(int i=0;i<ContrApptSheet.ProvCount;i++){
				System.Windows.Forms.Button butProv=new System.Windows.Forms.Button();
				butProv.Text="";
				butProv.BackColor=Providers.List[ApptViewItems.VisProvs[i]].ProvColor;
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
			RefreshModuleData(patNum);
			RefreshModuleScreen();
		}

		///<summary></summary>
		public void ModuleUnselected(){
			FamCur=null;
			PatCur=null;
			PlanList=null;
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

		///<summary>Gets the data for the current patient, just like in the other modules.  Gets patients.GetFamily, InsPlans, and CovPats.  Does not refresh any appointment data.  This function is not always called.  Sometimes, Patients.GetFamily is run directly. In that case, RefreshModuleScreen is usually called.  Also does FillPatientButton.</summary>
		private void RefreshModuleData(int patNum){
			if(patNum==0){
				PatCur=null;
				FamCur=null;
				return;
			}
			else{
				FamCur=Patients.GetFamily(patNum);
				PatCur=FamCur.GetPatient(patNum);
				PlanList=InsPlans.Refresh(FamCur);
				CovPats.Refresh(PatCur,PlanList);
			}
			FillPatientButton();
		}

		///<summary>Refreshes the main window title and then calls RefreshDay which gets the appointment data from the database.</summary>
		public void RefreshModuleScreen(){
			//MessageBox.Show("Refreshed");
			if(PatCur!=null){
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString+" - "
					+PatCur.GetNameLF();
			}
			else{
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString;
			}
			RefreshDay(Appointments.DateSelected);
		}

		///<summary>Should be called anytime a new patient is loaded.  This is not done quite the same as in the other modules.</summary>
		private void FillPatientButton(){
			Patients.AddPatsToMenu(menuPatient,new EventHandler(menuPatient_Click),PatCur,FamCur);
		}

		///<summary>Activated anytime a Patient menu item is clicked.</summary>
		private void menuPatient_Click(object sender,System.EventArgs e) {
			int newPatNum=Patients.ButtonSelect(menuPatient,sender,FamCur);
			OnPatientSelected(newPatNum);
			ModuleSelected(newPatNum);
		}

		///<summary>Triggered every time a control(including appt) is added or removed.</summary>
		private void ContrAppt_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
			//MessageBox.Show("appt layout");
			//This event actually happens quite frequently: once for every appointment placed on the screen.
			//Would like to rework it somehow to only be called when needed.
			//Assumes widths of the first 4 panels were set the same in the designer,
			ToolBarMain.Location=new Point(ClientSize.Width-panelAptInfo.Width-2,0);
			panelCalendar.Location=new Point(ClientSize.Width-panelAptInfo.Width-2,ToolBarMain.Height);
			panelAptInfo.Location=new Point(ClientSize.Width-panelAptInfo.Width-2
				,ToolBarMain.Height+panelCalendar.Height);
			butOther.Location=new Point(panelAptInfo.Location.X+2,panelAptInfo.Location.Y+113);
			panelNotes.Location=new Point(ClientSize.Width-panelAptInfo.Width-2
				,ToolBarMain.Height+panelCalendar.Height+panelAptInfo.Height);
			panelSheet.Width=ClientSize.Width-panelAptInfo.Width-2;
			panelSheet.Height=ClientSize.Height-panelSheet.Location.Y;
			RefreshVisops();
			//ContrApptSheet2.Height=panelSheet.Height;
			panelOps.Width=panelSheet.Width;
		}

		///<summary>Also refreshes some other display values. Needs to be called when switching databases.</summary>
		private void RefreshVisops(){
			if(Defs.Short!=null){
				ApptViews.SetCur(comboView.SelectedIndex-1);
				ApptViewItems.GetForCurView();//refreshes visops,etc
				ContrApptSheet2.ComputeColWidth(panelSheet.Width-vScrollBar1.Width);
			}
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
			PinApptSingle=new ContrApptSingle();
			PinApptSingle.Visible=false;
			PinApptSingle.ThisIsPinBoard=true;
			this.Controls.Add(PinApptSingle);
			PinApptSingle.MouseDown += new System.Windows.Forms.MouseEventHandler(PinApptSingle_MouseDown);
			PinApptSingle.MouseUp += new System.Windows.Forms.MouseEventHandler(PinApptSingle_MouseUp);
			PinApptSingle.MouseMove += new System.Windows.Forms.MouseEventHandler(PinApptSingle_MouseMove);
			ContrApptSheet.RowsPerIncr=1;
			Appointments.DateSelected=DateTime.Now;
			ContrApptSingle.SelectedAptNum=-1;
			RefreshModuleScreen();
			//MessageBox.Show(ApptViews.List.Length.ToString());
			if(ApptViews.List.Length>0){//if any views
				SetView(1);//default to first view
			}
			menuApt.MenuItems.Clear();
			menuApt.MenuItems.Add(Lan.g(this,"Send to Unscheduled List"),new EventHandler(menuApt_Click));
			menuApt.MenuItems.Add(Lan.g(this,"Break Appointment"),new EventHandler(menuApt_Click));
			menuApt.MenuItems.Add(Lan.g(this,"Set Complete"),new EventHandler(menuApt_Click));
			menuApt.MenuItems.Add(Lan.g(this,"Delete"),new EventHandler(menuApt_Click));
			menuApt.MenuItems.Add(Lan.g(this,"Other Appointments"),new EventHandler(menuApt_Click));
			menuApt.MenuItems.Add("-");
			menuApt.MenuItems.Add(Lan.g(this,"Print Card"),new EventHandler(menuApt_Click));
			menuApt.MenuItems.Add(Lan.g(this,"Print Card for Entire Family")
				,new EventHandler(menuApt_Click));
			Lan.C(this,new Control[]
				{
				butToday,
				butTodayWk,
				butClearPin,
				label2,
				label3,
				label7,
				butOther,
				label4,
				labelPhoneType,
				label6,
				label5,
				label1,
				butAptModNoteEdit
				});
			LayoutToolBar();
			//Appointment action buttons
			toolTip1.SetToolTip(butUnsched, Lan.g(this,"Send to Unscheduled List"));
      toolTip1.SetToolTip(butBreak, Lan.g(this,"Break"));
			toolTip1.SetToolTip(butComplete, Lan.g(this,"Set Complete"));
			toolTip1.SetToolTip(butDelete, Lan.g(this,"Delete"));
			toolTip1.SetToolTip(butOther, Lan.g(this,"Other Appointments"));
		}

		///<summary></summary>
		public void LayoutToolBar(){
			ToolBarMain.Buttons.Clear();
			ODToolBarButton button;
			button=new ODToolBarButton("",0,Lan.g(this,"Select Patient"),"Patient");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=menuPatient;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton("",1,Lan.g(this,"Unscheduled List"),"Unsched"));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",2,Lan.g(this,"Recall List"),"Recall"));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",4,Lan.g(this,"Track Planned Appointments"),"Track"));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",3,Lan.g(this,"Print Schedule"),"Print"));
			ArrayList toolButItems=ToolButItems.GetForToolBar(ToolBarsAvail.ApptModule);
			for(int i=0;i<toolButItems.Count;i++){
				ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
				ToolBarMain.Buttons.Add(new ODToolBarButton(((ToolButItem)toolButItems[i]).ButtonText
					,-1,"",((ToolButItem)toolButItems[i]).ProgramNum));
			}
			ToolBarMain.Invalidate();
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
			ApptViews.SetCur(comboView.SelectedIndex-1);//also works for none (0-1);
			ApptViewItems.GetForCurView();
			ContrApptSheet2.ComputeColWidth(panelSheet.Width-vScrollBar1.Width);
			if(PatCur==null){
				ModuleSelected(0);
			}
			else{
				ModuleSelected(PatCur.PatNum);
			}
		}

		/// <summary>
		/// The key press from the main form is passed down to this module.
		/// </summary>
		/// <param name="keys"></param>
		public void FunctionKeyPress(Keys keys){
			//MessageBox.Show("keydown");
			switch(keys){
				case Keys.F1: SetView(1); break;
				case Keys.F2: SetView(2); break;
				case Keys.F3: SetView(3); break;
				case Keys.F4: SetView(4); break;
				case Keys.F5: SetView(5); break;
				case Keys.F6: SetView(6); break;
				case Keys.F7: SetView(7); break;
				case Keys.F8: SetView(8); break;
				case Keys.F9: SetView(9); break;
				case Keys.F10: SetView(10); break;
				case Keys.F11: SetView(11); break;
				case Keys.F12: SetView(12); break;
			}
		}

		/// <summary>Sets the view to the specified index, checking for validity in the process.</summary>
		private void SetView(int viewIndex){
			if(viewIndex > ApptViews.List.Length){
				return;
			}
			comboView.SelectedIndex=viewIndex;//this also triggers SelectedIndexChanged
		}

		///<summary>Fills the comboView with the current list of views and then tries to reselect the previous selection.  Also called from FormOpenDental.RefreshLocalData().</summary>
		public void FillViews(){
			int selected=comboView.SelectedIndex;
			comboView.Items.Clear();
			comboView.Items.Add(Lan.g(this,"none"));
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
			if(comboView.SelectedIndex==-1){
				comboView.SelectedIndex=0;
			}
		}

		///<summary>Uses FamCur and PatCur to fill appropriate data on screen. Makes no calls to db.</summary>
		private void FillPanelPatient(){
			if(PatCur!=null){
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
						Appointments.CurOld=Appointments.Cur;
					}
				}
				if(isPresent)	panelAptInfo.Enabled=true;//apt selected and present
				else panelAptInfo.Enabled=false;//apt selected but not present
			}
			if(panelNotes.Enabled){
				textFinancialNote.Text=FamCur.List[0].FamFinUrgNote;
				textAddressNote.Text=FamCur.List[0].AddrNote;
				textMedicalNote.Text=PatCur.MedUrgNote;
				textApptModNote.Text=PatCur.ApptModNote;
				if(PatCur.HmPhone!=""){
					labelPhoneType.Text=Lan.g(this,"Home Phone:");
					textPhone.Text=PatCur.HmPhone;
				}
				else if(PatCur.WirelessPhone!=""){
					labelPhoneType.Text=Lan.g(this,"Wireless Phone:");
					textPhone.Text=PatCur.WirelessPhone;
				}
				else if(PatCur.WkPhone!=""){
					labelPhoneType.Text=Lan.g(this,"Work Phone:");
					textPhone.Text=PatCur.WkPhone;
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
			if(panelAptInfo.Enabled){
				listConfirmed.SelectedIndex=Defs.GetOrder(DefCat.ApptConfirmed,Appointments.Cur.Confirmed);
			}
			else{
				listConfirmed.SelectedIndex=-1;
			}
		}

		///<summary>Sends the PatientSelected event on up to the main form. Does nothing else.</summary>
		private void OnPatientSelected(int patNum){
			PatientSelectedEventArgs eArgs=new OpenDental.PatientSelectedEventArgs(patNum);
			if(PatientSelected!=null)
				PatientSelected(this,eArgs);
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
			if(myDate.Year<1880){
				return;
			}
			if(PatCur==null){
				//there cannot be a selected appointment if no patient is loaded.
				ContrApptSingle.SelectedAptNum=-1;//fixes a minor bug.
			}
			//else if(ContrApptSingle.se
			ApptViews.SetCur(comboView.SelectedIndex-1);
			ApptViewItems.GetForCurView();
			ContrApptSingle.ProvBar=new int[ApptViewItems.VisProvs.Length][];
			for(int i=0;i<ApptViewItems.VisProvs.Length;i++){//Providers.List.Length;i++){
				ContrApptSingle.ProvBar[i]
					=new int[24*ContrApptSheet.RowsPerHr];//[144]; or 24*6
			}
			if(ContrApptSingle3!=null){//I think this is not needed.
				for(int i=0;i<ContrApptSingle3.Length;i++){
					if(ContrApptSingle3[i]!=null){
						ContrApptSingle3[i].Dispose();
					}
					ContrApptSingle3[i]=null;
				}
				ContrApptSingle3=null;
			}
			Appointments.Refresh(myDate);
			Schedules.RefreshDay(myDate);
			labelDate.Text=myDate.ToString("ddd");
			labelDate2.Text=myDate.ToString("-  MMM d");
			Calendar2.SetDate(myDate);
			ContrApptSheet2.Controls.Clear();
			ContrApptSingle3=new ContrApptSingle[Appointments.ListDay.Length];
			int[] aptNums=new int[Appointments.ListDay.Length];
			int[] patNums=new int[Appointments.ListDay.Length];
			for(int i=0;i<Appointments.ListDay.Length;i++){
				aptNums[i]=Appointments.ListDay[i].AptNum;
				patNums[i]=Appointments.ListDay[i].PatNum;
			}
			Procedures.GetProcsMultApts(aptNums);
			Patient[] multPats=Patients.GetMultPats(patNums);
			for(int i=0;i<Appointments.ListDay.Length;i++){
				ContrApptSingle3[i]=new ContrApptSingle();
				ContrApptSingle3[i].Visible=false;
				ContrApptSingle3[i].Info=new InfoApt();
				ContrApptSingle3[i].Info.MyApt=Appointments.ListDay[i];
				if(ContrApptSingle.SelectedAptNum==Appointments.ListDay[i].AptNum){//if this is the selected apt
					//if the selected patient was changed from another module, then deselect the apt.
					if(PatCur.PatNum!=Appointments.ListDay[i].PatNum){
						ContrApptSingle.SelectedAptNum=-1;
					}
				}
				Procedures.GetProcsOneApt(Appointments.ListDay[i].AptNum);
				ContrApptSingle3[i].Info.Procs=Procedures.ProcsOneApt;
				ContrApptSingle3[i].Info.Production
					=Procedures.GetProductionOneApt(Appointments.ListDay[i].AptNum);
				ContrApptSingle3[i].Info.MyPatient
					=Patients.GetOnePat(multPats,Appointments.ListDay[i].PatNum);
				//copy time pattern to provBar[]:
				if(ApptViewItems.GetIndexProv(Appointments.ListDay[i].ProvNum)!=-1
				//if(Providers.GetIndex(ContrApptSingle3[i].Info.MyApt.ProvNum)!=-1
					&& Appointments.ListDay[i].AptStatus!=ApptStatus.Broken){
					string pattern=ContrApptSingle.GetPatternShowing(Appointments.ListDay[i].Pattern);
					int startIndex=ContrApptSingle3[i].ConvertToY()/ContrApptSheet.Lh;//rounds down
					for(int k=0;k<pattern.Length;k++){
						if(pattern.Substring(k,1)=="X"){
							//int timeBarInc=ContrApptSingle3[i].ConvertToY()/ContrApptSheet.Lh+k;
							ContrApptSingle.ProvBar
								[ApptViewItems.GetIndexProv(Appointments.ListDay[i].ProvNum)][startIndex+k]++;
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
			FillLab();
			FillProduction();
		}

		///<summary>Fills the lab summary for the day.</summary>
		private void FillLab(){
			int notRec=0;
			for(int i=0;i<Appointments.ListDay.Length;i++){
				if(Appointments.ListDay[i].Lab==LabCase.Sent){
					notRec++;
				}
			}
			if(notRec==0){
				textLab.Font=new Font(FontFamily.GenericSansSerif,8,FontStyle.Regular);
				textLab.ForeColor=Color.Black;
				textLab.Text="All Received";
			}
			else{
				textLab.Font=new Font(FontFamily.GenericSansSerif,8,FontStyle.Bold);
				textLab.ForeColor=Color.DarkRed;
				textLab.Text=notRec.ToString()+" NOT RECEIVED";
			}
		}

		///<summary>Fills the production summary for the day.</summary>
		private void FillProduction(){
			double production=0;
			for(int i=0;i<Appointments.ListDay.Length;i++){
				production+=Procedures.GetProductionOneApt(Appointments.ListDay[i].AptNum);
			}
			textProduction.Text=production.ToString("c0");
		}

		///<summary>Creates one bitmap image for each appointment if visible.</summary>
		private void CreateAptShadows(){
			if(ContrApptSheet2.Shadow==null)//if user resizes window to be very narrow
				return;
			Graphics grfx=Graphics.FromImage(ContrApptSheet2.Shadow);
			for(int i=0;i<Appointments.ListDay.Length;i++){
				//MessageBox.Show("i:"+i.ToString()+",height:"+ContrApptSingle3[i].Height.ToString());
				ContrApptSingle3[i].CreateShadow();
				if(ContrApptSingle3[i].Location.X>=0 && ContrApptSingle3[i].Width>3
					&& ContrApptSingle3[i].Shadow!=null)
				{
					grfx.DrawImage(ContrApptSingle3[i].Shadow
						,ContrApptSingle3[i].Location.X,ContrApptSingle3[i].Location.Y);
				}
				ContrApptSingle3[i].Shadow=null;
			}
			grfx.Dispose();
		}

		//<summary>Assembles the lines of text to be displayed on a single appointment.</summary>
		/*private void AssembleInfo(){
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
					case LabCase.QualityChecked:
						CurInfo.Lines[1]=" LAB QUALITY CHECKED";
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
			Font myFont=new Font("Arial",8);
			RectangleF rectf 
				=new Rectangle(0,0
				,System.Convert.ToInt32((ContrApptSheet.ColWidth-12)/.95)//.95 is arbitrary
				,(int)myFont.GetHeight(grfx));
			string remainNote = CurInfo.MyApt.Note;
			while (remainNote.Length>0){
				grfx.MeasureString(remainNote,myFont,rectf.Size,new StringFormat(),out iChars, out iLines);
				if(iChars!=remainNote.Length)
					while(iChars!=0&&(remainNote.Substring(iChars-1,1)!=" ")){
						iChars=iChars-1;
					}
				if(iChars==0){//this will wrap even if no spaces at all, whether short or very long
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
		}//end assembleinfo*/

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

		///<summary>Mouse down event for the pinboard appointment. Sets selected and prepares for drag.</summary>
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
			FillPatientButton();
			ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString+" - "
				+PatCur.GetNameLF();
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
			if(mouseIsDown==false) return;
			if((Math.Abs(e.X+PinApptSingle.Location.X-mouseOrigin.X)<1)
				&&(Math.Abs(e.Y+PinApptSingle.Location.Y-mouseOrigin.Y)<1)){
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
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Move Appointment?"),"",MessageBoxButtons.OKCancel)
				!=DialogResult.OK)
			{//responds no, don't move off pinboard.
				mouseIsDown = false;
				boolAptMoved=false;
				//pinIsOccupied=true;
				TempApptSingle.Dispose();
			}
			
			//convert loc to new time
			Appointments.Cur=Appointments.PinBoard;
			Appointments.CurOld=Appointments.Cur;
			if(Appointments.Cur.IsNewPatient
				&& Appointments.DateSelected!=Appointments.Cur.AptDateTime){
				Procedures.SetDateFirstVisit(Appointments.DateSelected,4,PatCur);
			}
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
			if(Appointments.Cur.AptStatus==ApptStatus.Planned){//if Planned appt is on pinboard
				Appointments.Cur.NextAptNum=Appointments.Cur.AptNum;
				Appointments.Cur.AptStatus=ApptStatus.Scheduled;
				Appointments.InsertCur();//now, aptnum is different.
				Procedure[] ProcList=Procedures.Refresh(PatCur.PatNum);
				bool procAlreadyAttached=false;
				Procedure ProcCur;
				Procedure ProcOld;
				for(int i=0;i<ProcList.Length;i++){
					if(ProcList[i].NextAptNum==PatCur.NextAptNum){//if on the planned apt
						if(ProcList[i].AptNum>0){//already attached to another appt
							procAlreadyAttached=true;
						}
						else{//only update procedures not already attached to another apt
							ProcCur=ProcList[i];
							ProcOld=ProcCur.Copy();
							ProcCur.AptNum=Appointments.Cur.AptNum;
							ProcCur.Update(ProcOld);//recall synch not required.
						}
					}
				}
				if(procAlreadyAttached){
					MessageBox.Show(Lan.g(this,"One or more procedures could not be scheduled because they were already attached to another appointment. Someone probably forgot to update the Next appointment in the Chart module."));
				}
				ProcDesc procDesc=Procedures.GetProcsForSingle(Appointments.Cur.AptNum,true);
				CurInfo.Procs=procDesc.ProcLines;
				CurInfo.Production=procDesc.Production;
//might be missing some CurInfo.
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
					(TimeSpan.FromMinutes(Appointments.ListDay[i].Pattern.Length*5)))
				{
					Debug.WriteLine(Appointments.Cur.AptDateTime.TimeOfDay.ToString());
					Debug.WriteLine(Appointments.ListDay[i].AptDateTime.TimeOfDay.ToString());
					Debug.WriteLine(TimeSpan.FromMinutes(Appointments.ListDay[i].Pattern.Length*5).ToString());
					retVal=true;
				}
				//tests stop time
				if(Appointments.Cur.AptDateTime.TimeOfDay.Add
					(TimeSpan.FromMinutes(Appointments.Cur.Pattern.Length*5))
					> Appointments.ListDay[i].AptDateTime.TimeOfDay
					&& Appointments.Cur.AptDateTime.TimeOfDay.Add
					(TimeSpan.FromMinutes(Appointments.Cur.Pattern.Length*5))
					<= Appointments.ListDay[i].AptDateTime.TimeOfDay.Add
					(TimeSpan.FromMinutes(Appointments.ListDay[i].Pattern.Length*5))){
					retVal=true;
				}
				//tests engulf
				if(Appointments.Cur.AptDateTime.TimeOfDay
					<= Appointments.ListDay[i].AptDateTime.TimeOfDay
					&& Appointments.Cur.AptDateTime.TimeOfDay.Add
					(TimeSpan.FromMinutes(Appointments.Cur.Pattern.Length*5))
					>= Appointments.ListDay[i].AptDateTime.TimeOfDay.Add
					(TimeSpan.FromMinutes(Appointments.ListDay[i].Pattern.Length*5))){
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

		///<summary>Mouse down event anywhere on the sheet.  Could be a blank space or on an actual appointment.</summary>
		private void ContrApptSheet2_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			SheetClickedonOp=Defs.Short[(int)DefCat.Operatories]
				[ApptViewItems.VisOps[ContrApptSheet2.DoubleClickToOp(e.X)]].DefNum;
			SheetClickedonHour=ContrApptSheet2.DoubleClickToHour(e.Y);
			SheetClickedonMin=ContrApptSheet2.DoubleClickToMin(e.Y);
			//MessageBox.Show(SheetClickedonHour.ToString()+","+SheetClickedonMin.ToString());
			//date is irrelevant. This is just for the time:
			DateTime SheetClickedonTime=new DateTime(2000,1,1,SheetClickedonHour,SheetClickedonMin,0);
			ContrApptSingle.ClickedAptNum=0;
			for(int i=0;i<Appointments.ListDay.Length;i++){
				if(SheetClickedonOp==Appointments.ListDay[i].Op
					&& Appointments.ListDay[i].AptDateTime.TimeOfDay <= SheetClickedonTime.TimeOfDay
					&& SheetClickedonTime.TimeOfDay < Appointments.ListDay[i].AptDateTime.TimeOfDay
					+TimeSpan.FromMinutes(Appointments.ListDay[i].Pattern.Length*5))
				{
					ContrApptSingle.ClickedAptNum=Appointments.ListDay[i].AptNum;
				}
			}
			Graphics grfx=ContrApptSheet2.CreateGraphics();
			if(ContrApptSingle.ClickedAptNum!=0){//mouse down on appt
				int thisIndex=GetIndex(ContrApptSingle.ClickedAptNum);
				ContrApptSingle.PinBoardIsSelected=false;
				Appointments.Cur=ContrApptSingle3[thisIndex].Info.MyApt;
				Appointments.CurOld=Appointments.Cur;
				if(ContrApptSingle.SelectedAptNum!=-1//unselects previously selected unless it's the same appt
					&& ContrApptSingle.SelectedAptNum!=ContrApptSingle.ClickedAptNum){
					int prevSel=GetIndex(ContrApptSingle.SelectedAptNum);
					//has to be done before refresh prev:
					ContrApptSingle.SelectedAptNum=ContrApptSingle.ClickedAptNum;
					if(prevSel!=-1){
						ContrApptSingle3[prevSel].CreateShadow();
						grfx.DrawImage(ContrApptSingle3[prevSel].Shadow,ContrApptSingle3[prevSel].Location.X
							,ContrApptSingle3[prevSel].Location.Y);
					}
				}
				//again, in case missed in loop above:
				ContrApptSingle.SelectedAptNum=ContrApptSingle.ClickedAptNum;
				//RefreshModuleData(//this would take too long
				//best way to do this would be to spawn a separate thread to do db stuff.
				//beware that PlanList and CovPats have NOT been refreshed
				FamCur=Patients.GetFamily(Appointments.ListDay[thisIndex].PatNum);
				PatCur=FamCur.GetPatient(Appointments.ListDay[thisIndex].PatNum);
				FillPatientButton();
				OnPatientSelected(PatCur.PatNum);
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString+" - "
					+PatCur.GetNameLF();
				ContrApptSingle3[thisIndex].CreateShadow();
				grfx.DrawImage(ContrApptSingle3[thisIndex].Shadow,ContrApptSingle3[thisIndex].Location.X
					,ContrApptSingle3[thisIndex].Location.Y);
				FillPanelPatient();
				if(e.Button==MouseButtons.Right){
					menuApt.Show(ContrApptSheet2,new Point(e.X,e.Y));
				}
				else{
					mouseIsDown = true;
					TempApptSingle=new ContrApptSingle();
					TempApptSingle.Visible=false;//otherwise I get a phantom appt while holding mouse down
					TempApptSingle.Info=ContrApptSingle3[thisIndex].Info;
					Controls.Add(TempApptSingle);
					TempApptSingle.SetLocation();
					TempApptSingle.BringToFront();
					//mouseOrigin is in Appt sheet coordinates
					mouseOrigin.X=e.X+ContrApptSingle3[thisIndex].Location.X;
					mouseOrigin.Y=e.Y+ContrApptSingle3[thisIndex].Location.Y;
					contOrigin=ContrApptSingle3[thisIndex].Location;
					TempApptSingle.CreateShadow();
				}
			}
			else{//not on appt. This was disabled so you can double click on empty area to sched
				/*ContrApptSingle.PinBoardIsSelected=false;
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
				FillPanelPatient();*/
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
				PlanList=InsPlans.Refresh(FamCur);
				CovPats.Refresh(PatCur,PlanList);
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
				}
				PlanList=InsPlans.Refresh(FamCur);
				CovPats.Refresh(PatCur,PlanList);
				FillPanelPatient();
				TempApptSingle.Dispose();
				return;
			}
			int tHr=ContrApptSheet2.ConvertToHour
				(TempApptSingle.Location.Y-ContrApptSheet2.Location.Y-panelSheet.Location.Y);
			int tMin=ContrApptSheet2.ConvertToMin
				(TempApptSingle.Location.Y-ContrApptSheet2.Location.Y-panelSheet.Location.Y);
			bool timeWasMoved=tHr!=Appointments.Cur.AptDateTime.Hour
				|| tMin!=Appointments.Cur.AptDateTime.Minute;
			if(timeWasMoved){
				if(MessageBox.Show(Lan.g(this,"Move Appointment?"),"",MessageBoxButtons.OKCancel)
					!=DialogResult.OK)
				{
					mouseIsDown=false;
					boolAptMoved=false;
					TempApptSingle.Dispose();
					PlanList=InsPlans.Refresh(FamCur);
					CovPats.Refresh(PatCur,PlanList);
					return;
				}
			}
			//convert loc to new time
			//Appointments.Cur = Appointments.List[ContrApptSingle.SelectedIndex];
			DateTime tDate=Appointments.Cur.AptDateTime.Date;
			Appointments.Cur=TempApptSingle.Info.MyApt;
			Appointments.CurOld=Appointments.Cur;
			Appointments.Cur.AptDateTime=new DateTime(tDate.Year,tDate.Month,tDate.Day,tHr,tMin,0);
			//MessageBox.Show(Appointments.Cur.AptDateTime.ToString());
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
					PlanList=InsPlans.Refresh(FamCur);
					CovPats.Refresh(PatCur,PlanList);
					return;
				}
			}//end if DoesOverlap
			if(Appointments.Cur.AptStatus==ApptStatus.Broken && timeWasMoved){
				Appointments.Cur.AptStatus=ApptStatus.Scheduled;
			}
			Appointments.UpdateCur();
			RefreshModuleScreen();
			SetInvalid();
			mouseIsDown = false;
			boolAptMoved=false;
			TempApptSingle.Dispose();
			PlanList=InsPlans.Refresh(FamCur);
			CovPats.Refresh(PatCur,PlanList);
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
					Appointments.CurOld=Appointments.Cur;
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
				//bool patWasLoaded=(PatCur!=null);//because we will need to know this further down
				//int oldPatNum=0;
				FormPatientSelect FormPS=new FormPatientSelect();
				if(PatCur!=null){//patWasLoaded){
					//oldPatNum=PatCur.PatNum;
					FormPS.InitialPatNum=PatCur.PatNum;
				}
				FormPS.ShowDialog();
				if(FormPS.DialogResult!=DialogResult.OK){
					return;
				}
				if(PatCur==null || FormPS.SelectedPatNum!=PatCur.PatNum){//if the patient was changed
					OnPatientSelected(FormPS.SelectedPatNum);
					RefreshModuleData(FormPS.SelectedPatNum);//then pull up all the new info.
				}
				if(FormPS.NewPatientAdded){
					Appointments.Cur=new Appointment();
					Appointments.Cur.PatNum=PatCur.PatNum;
					Appointments.Cur.IsNewPatient=true;
					Appointments.Cur.Pattern="/X/";
					if(PatCur.PriProv==0){
						Appointments.Cur.ProvNum=Prefs.GetInt("PracticeDefaultProv");
					}
					else{			
						Appointments.Cur.ProvNum=PatCur.PriProv;
					}
					Appointments.Cur.ProvHyg=PatCur.SecProv;
					Appointments.Cur.AptStatus=ApptStatus.Scheduled;
					DateTime d=Appointments.DateSelected;
					//minutes always rounded down.
					int minutes=(int)(ContrAppt.SheetClickedonMin/ContrApptSheet.MinPerIncr)
						*ContrApptSheet.MinPerIncr;
					Appointments.Cur.AptDateTime=new DateTime(d.Year,d.Month,d.Day
						,ContrAppt.SheetClickedonHour,minutes,0);
					Appointments.Cur.Op=ContrAppt.SheetClickedonOp;
					Appointments.InsertCur();
					Appointments.CurOld=Appointments.Cur;
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
			if(PatCur==null)
				return;
			FormApptsOther FormAO=new FormApptsOther(PatCur,FamCur);
			FormAO.InitialClick=initialClick;
			FormAO.ShowDialog();
			if(FormAO.OResult==OtherResult.Cancel){
				return;
			}
			switch(FormAO.OResult){
				case OtherResult.CopyToPinBoard:
					//AssembleInfo();
					CurToPinBoard();
					RefreshModuleScreen();
					break;
				case OtherResult.NewToPinBoard:
					//AssembleInfo();
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

		private void ToolBarMain_ButtonClick(object sender, OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			if(e.Button.Tag.GetType()==typeof(string)){
				//standard predefined button
				switch(e.Button.Tag.ToString()){
					case "Patient":
						OnPat_Click();
						break;
					case "Unsched":
						OnUnschedList_Click();
						break;
					case "Recall":
						OnRecall_Click();
						break;
					case "Track":
						OnTrack_Click();
						break;
					case "Print":
						OnPrint_Click();
						break;
				}
			}
			else if(e.Button.Tag.GetType()==typeof(int)){
				Programs.Execute((int)e.Button.Tag,PatCur);
			}
		}

		private void OnPat_Click() {
			FormPatientSelect formPS = new FormPatientSelect();
			formPS.ShowDialog();
			if(formPS.DialogResult!=DialogResult.OK){
				return;
			}
			OnPatientSelected(formPS.SelectedPatNum);
			RefreshModuleData(formPS.SelectedPatNum);
			DisplayOtherDlg(false);
		}

		private void OnUnschedList_Click() {
			FormUnsched FormUnsched2=new FormUnsched();
			FormUnsched2.ShowDialog();
			if(FormUnsched2.PinClicked){
				//AssembleInfo();
				CurToPinBoard();
				RefreshModuleScreen();
			}
		}

		private void OnRecall_Click() {
			FormRecallList FormRL=new FormRecallList();
			FormRL.ShowDialog();
			if(FormRL.PinClicked){
				//AssembleInfo();
				CurToPinBoard();
				//RefreshModuleScreen();
			}
			if(FormRL.SelectedPatNum!=0){
				OnPatientSelected(FormRL.SelectedPatNum);
				ModuleSelected(FormRL.SelectedPatNum);
			}
		}

		private void OnTrack_Click() {
			Cursor=Cursors.WaitCursor;
			FormTrackNext FormTN=new FormTrackNext();
			FormTN.ShowDialog();
			if(FormTN.PinClicked){
				//AssembleInfo();
				CurToPinBoard();
				//RefreshModuleScreen();
			}
			Cursor=Cursors.Default;
			if(PatCur==null){
				ModuleSelected(0);
			}
			else{
				ModuleSelected(PatCur.PatNum);
			}
		}

		private void OnPrint_Click() {
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
			//try  {
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
			//}
			//catch{
			//	MessageBox.Show(Lan.g(this,"Printer not available"));
			//}
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
			e.Graphics.DrawString(date,dateFont,Brushes.Black,xDate,yPos);//centered
			//FIGURING OUT SIZE OF IMAGE
			int recHeight=0;
      int recWidth=0;
			int recX=0;
			int recY=0;
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
			if(AListStart.Count > 0){//makes sure there is at least one timeblock
        StartTime=(DateTime)AListStart[0]; 
				for(int i=0;i<AListStart.Count;i++){
          //if (A) OR (B AND C)
					if((((DateTime)(AListStart[i])).Hour < StartTime.Hour) 
						|| (((DateTime)(AListStart[i])).Hour==StartTime.Hour 
						&& ((DateTime)(AListStart[i])).Minute < StartTime.Minute)){
            StartTime=(DateTime)AListStart[i];   
					}
				}
				StopTime=(DateTime)AListStop[0]; 
				for(int i=0;i<AListStop.Count;i++){
          //if (A) OR (B AND C)
					if((((DateTime)(AListStop[i])).Hour > StopTime.Hour) 
						|| (((DateTime)(AListStop[i])).Hour==StopTime.Hour 
						&& ((DateTime)(AListStop[i])).Minute > StopTime.Minute)){
            StopTime=(DateTime)AListStop[i];   
					}
				}
      }
      else{//office is closed
				StartTime=new DateTime(Appointments.DateSelected.Year,Appointments.DateSelected.Month
					,Appointments.DateSelected.Day
					,ContrApptSheet2.ConvertToHour(-ContrApptSheet2.Location.Y)
					,ContrApptSheet2.ConvertToMin(-ContrApptSheet2.Location.Y)
					,0);
				if(ContrApptSheet2.ConvertToHour(-ContrApptSheet2.Location.Y)+12<23){
					//we will be adding an extra hour later
					StopTime=new DateTime(Appointments.DateSelected.Year,Appointments.DateSelected.Month
						,Appointments.DateSelected.Day
						,ContrApptSheet2.ConvertToHour(-ContrApptSheet2.Location.Y)+12//add 12 hours
						,ContrApptSheet2.ConvertToMin(-ContrApptSheet2.Location.Y)
						,0);
				}
				else{
					StopTime=new DateTime(Appointments.DateSelected.Year,Appointments.DateSelected.Month
						,Appointments.DateSelected.Day
						,22
						,ContrApptSheet2.ConvertToMin(-ContrApptSheet2.Location.Y)
						,0);
				}
			}
			recY=(int)(ContrApptSheet.Lh*ContrApptSheet.RowsPerHr*StartTime.Hour);
			recWidth=(int)ContrApptSheet2.Shadow.Width;
			recHeight=(int)((ContrApptSheet.Lh*ContrApptSheet.RowsPerHr
				*(StopTime.Hour-StartTime.Hour+1)));
			//recHeight+=ContrApptSheet.Lh*6;
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
			string[] headers = new string[ContrApptSheet.ColCount];
			Font headerFont=new Font("Arial",8);
      yPos+=30;   //y Position  
			//need to size to horizontal resolution if bigger than 100
      xPos+=(int)(ContrApptSheet.TimeWidth+(ContrApptSheet.ProvWidth*ContrApptSheet.ProvCount)*(100/imageTemp.HorizontalResolution));  // x position
			int xCenter=0;
			for(int i=0;i<ContrApptSheet.ColCount;i++){
				headers[i]=Defs.Short[(int)DefCat.Operatories][ApptViewItems.VisOps[i]].ItemName;	
				xCenter=(int)((ContrApptSheet.ColWidth/2)-(e.Graphics.MeasureString(headers[i],headerFont).Width/2));
			  e.Graphics.DrawString(headers[i],headerFont,Brushes.Black,(int)((xPos+xCenter)*(100/imageTemp.HorizontalResolution)),yPos);
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
			//get the patient associate with the pinboard appt so we can test for next apt.
			//Patient PatCur=Patients.Cur;//but we don't really care
			//PatCur.PatNum=Appointments.Cur.PatNum;
			//Patients.Cur=PatCur;
			RefreshModuleData(Appointments.Cur.PatNum);
			ContrApptSingle.SelectedAptNum=-1;
			ContrApptSingle.PinBoardIsSelected=false;
			if(Appointments.Cur.AptStatus==ApptStatus.UnschedList){//on unscheduled list
				//do nothing to database
			}
			else if(Appointments.Cur.AptDateTime.Year<1880){//not already scheduled
				if(Appointments.Cur.AptNum==PatCur.NextAptNum){//if next apt
					//do nothing except remove it from pinboard
				}
				else{//for normal appt:
					//this gets rid of new appointments that never made it off the pinboard
					Procedures.UnattachProcsInAppt(Appointments.Cur.AptNum);
					Appointments.DeleteCur(PatCur);
				}
			}
			PatCur=null;
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

		private void menuApt_Click(object sender, System.EventArgs e) {
			switch(((MenuItem)sender).Index){
				case 0:
					OnUnsched_Click();
					break;
				case 1:
					OnBreak_Click();
					break;
				case 2:
					OnComplete_Click();
					break;
				case 3:
					OnDelete_Click();
					break;
				case 4:
					DisplayOtherDlg(false);
					break;
				//5: divider
				case 6:
					cardPrintFamily=false;
					PrintApptCard();
					break;
				case 7:
					cardPrintFamily=true;
					PrintApptCard();
					break;
			}
		}

		///<summary>Sends current appointment to unscheduled list.</summary>
		private void butUnsched_Click(object sender, System.EventArgs e) {
			OnUnsched_Click();
		}

		private void butBreak_Click(object sender, System.EventArgs e) {
			OnBreak_Click();
		}

		private void butComplete_Click(object sender, System.EventArgs e) {
			OnComplete_Click();
		}
	
		private void butDelete_Click(object sender, System.EventArgs e) {
			OnDelete_Click();
		}

		private void butOther_Click(object sender, System.EventArgs e) {
			DisplayOtherDlg(false);
		}

		private void OnUnsched_Click(){
			if(MessageBox.Show(Lan.g(this,"Send Appointment to Unscheduled List?")
				,"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			Appointments.Cur.AptStatus=ApptStatus.UnschedList;
			Appointments.UpdateCur();
			RefreshModuleScreen();
			SetInvalid();
		}

		private void OnBreak_Click(){
			int thisIndex=GetIndex(ContrApptSingle.SelectedAptNum);
			//Appointments.CurOld
			Appointments.Cur.AptStatus=ApptStatus.Broken;
			Appointments.UpdateCur();
			RefreshModuleScreen();
			SetInvalid();
			FormAdjust FormA=new FormAdjust(PatCur);
			FormA.IsNew=true;
			Adjustments.Cur=new Adjustment();
			Adjustments.Cur.AdjDate=DateTime.Today;
			Adjustments.Cur.ProvNum=Appointments.Cur.ProvNum;
			Adjustments.Cur.PatNum=PatCur.PatNum;
			FormA.ShowDialog();
		}

		private void OnComplete_Click(){
			int thisIndex=GetIndex(ContrApptSingle.SelectedAptNum);
			Appointments.Cur.AptStatus=ApptStatus.Complete;
			//Procedures.SetDateFirstVisit(Appointments.Cur.AptDateTime.Date);//done when making appt instead
			Procedures.SetCompleteInAppt(Appointments.Cur,PatCur,PlanList);//loops through each proc
			Appointments.UpdateCur();
			RefreshModuleScreen();
			SetInvalid();
			//ContrApptSingle3[thisIndex].Info.MyApt.AptStatus=ApptStatus.Complete;
			//ContrApptSingle3[thisIndex].Refresh();
		}

		private void OnDelete_Click(){
			if(MessageBox.Show(Lan.g(this,"Delete Appointment?"),"",MessageBoxButtons.OKCancel)
				!=DialogResult.OK){
				return;
			}
			Procedures.UnattachProcsInAppt(Appointments.Cur.AptNum);
			Appointments.DeleteCur(PatCur);
			ContrApptSingle.SelectedAptNum=-1;
			ContrApptSingle.PinBoardIsSelected=false;
			PatCur=null;
			RefreshModuleScreen();
			SetInvalid();
		}		

		private void textMedicalNote_TextChanged(object sender, System.EventArgs e) {
		
		}

		/*private void listConfirmed_SelectedIndexChanged(object sender, System.EventArgs e) {
			listConfirmed.SelectedIndex=-1;
		}*/

		private void listConfirmed_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listConfirmed.IndexFromPoint(e.X,e.Y)==-1){
				return;
			}
			Appointments.Cur.Confirmed
				=Defs.Short[(int)DefCat.ApptConfirmed][listConfirmed.IndexFromPoint(e.X,e.Y)].DefNum;
			Appointments.UpdateCur();
			RefreshModuleScreen();//this is used because we are not changing the patient.
			//ModuleSelected();
			SetInvalid();
		}

		private void butAptModNoteEdit_Click(object sender, System.EventArgs e) {
			FormNoteApptMod FormNAM=new FormNoteApptMod(PatCur);
			FormNAM.ShowDialog();
			textApptModNote.Text=PatCur.ApptModNote;
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
				//prevents rare malfunctions. For instance, during editing of views, if tickrefresh happens.
			}
			//GC.Collect();	
		}
		
		///<summary>"Ganga's Code: Printing the Appointment Card - 9/9/2004"</summary>
		private void PrintApptCard(){
			pd2=new PrintDocument();
			pd2.PrintPage+=new PrintPageEventHandler(this.pd2_PrintApptCard);
			pd2.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			pd2.OriginAtMargins=true;//forces origin to upper left of actual page
			pd2.PrinterSettings.PrinterName=Computers.Cur.PrinterName;
			if(!pd2.PrinterSettings.IsValid){
				pd2.PrinterSettings.PrinterName="";//use default printer
			}
			pd2.Print();	
		}
			
		private void pd2_PrintApptCard(object sender, PrintPageEventArgs ev){
			Graphics g=ev.Graphics;
			//Return Address--------------------------------------------------------------------------
			string str=Prefs.GetString("PracticeTitle")+"\r\n";
			g.DrawString(str,new Font(FontFamily.GenericSansSerif,9,FontStyle.Bold),Brushes.Black,60,60);
			str=Prefs.GetString("PracticeAddress")+"\r\n";
			if(Prefs.GetString("PracticeAddress2")!=""){
				str+=Prefs.GetString("PracticeAddress2")+"\r\n";
			}
			str+=Prefs.GetString("PracticeCity")+"  "
				+Prefs.GetString("PracticeST")+"  "
				+Prefs.GetString("PracticeZip")+"\r\n";
			string phone=Prefs.GetString("PracticePhone");
			if(CultureInfo.CurrentCulture.Name=="en-US"
				&& phone.Length==10)
			{
				str+="("+phone.Substring(0,3)+")"+phone.Substring(3,3)+"-"+phone.Substring(6);
			}
			else{//any other phone format
				str+=phone;
			}
			g.DrawString(str,new Font(FontFamily.GenericSansSerif,8),Brushes.Black,60,75);
			//Body text-------------------------------------------------------------------------------
			string name;
			str="Appointment Reminders:"+"\r\n\r\n";
			Appointment[] aptsOnePat;
			for(int i=0;i<FamCur.List.Length;i++){
				if(!cardPrintFamily && FamCur.List[i].PatNum!=PatCur.PatNum){
					continue;
				}
				name=FamCur.List[i].FName;
				if(name.Length>15){//trim name so it won't be too long
					name=name.Substring(0,15);
				}
				aptsOnePat=Appointments.GetForPat(FamCur.List[i].PatNum);
				for(int a=0;a<aptsOnePat.Length;a++){
					if(aptsOnePat[a].AptDateTime.Date<=DateTime.Today){
						continue;//ignore old appts
					}
					str+=name+": "+aptsOnePat[a].AptDateTime.ToString()+"\r\n";
				}
			}
			g.DrawString(str,new Font(FontFamily.GenericSansSerif,9),Brushes.Black,40,180);
			//Patient's Address-----------------------------------------------------------------------
			Patient pat;
			if(cardPrintFamily){
				pat=FamCur.List[0].Copy();
			}
			else{
				pat=PatCur.Copy();
			}
			str=pat.FName+" "+pat.LName+"\r\n"
				+pat.Address+"\r\n";
			if(pat.Address2!=""){
				str+=pat.Address2+"\r\n";
			}
			str+=pat.City+"  "+pat.State+"  "+pat.Zip;
			g.DrawString(str,new Font(FontFamily.GenericSansSerif,11),Brushes.Black,300,240);		
			//CommLog entry---------------------------------------------------------------------------
			Commlogs.Cur=new Commlog();
			Commlogs.Cur.CommDateTime=DateTime.Now;
			Commlogs.Cur.CommType=CommItemType.Misc;
			Commlogs.Cur.Note="Appointment card sent";
			Commlogs.Cur.PatNum=PatCur.PatNum;
			//there is no dialog here because it is just a simple entry
			Commlogs.InsertCur();
			ev.HasMorePages = false;
		}

		

		

	


		

	}//end class

	///<summary>This is the info to display on an appointment. This will gradually become more complex as the user gains more control over what shows on each appt.</summary>
	public struct InfoApt{
		///<summary>Set to true if this appointment is simply being displayed in the Chart module Next apt section rather than int the Appointments module.</summary>
		public bool IsNext;
		///<summary>A list of formatted proc strings to display.  Later, this will be changed to an actual procedure list.</summary>
		public string[] Procs;
		///<summary>The appointment struct holding the actual db info for this displayed appointment.</summary>
		public Appointment MyApt;
		///<summary>The patient associated with this appt.</summary>
		public Patient MyPatient;
		///<summary>The amount of money generated from this appointment.</summary>
		public double Production;
	}


}//end namespace













