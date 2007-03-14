/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.Windows.Forms;
using OpenDental.UI;

namespace OpenDental{

	///<summary></summary>
	public class ContrAccount : System.Windows.Forms.UserControl{
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.IContainer components=null;// Required designer variable.
		private Procedure[] arrayProc;
		///<summary>public because used in FormRpStatement.</summary>
		public ArrayList AcctLineAL;
		private OpenDental.TableAccount tbAccount;
		private OpenDental.TableAccountPat tbAcctPat;
		private PayInfo[] arrayPay;
		private Adjustment[] arrayAdj;
		private Claim[] arrayClaim;
		private Commlog[] arrayComm;
		private PayPlan[] arrayPayPlan;
		//private bool ControlDown;
		///<summary></summary>
		//public static string[,] StatementA;
		private System.Windows.Forms.Label labelUrgFinNote;
		private System.Windows.Forms.TextBox textUrgFinNote;
		private System.Windows.Forms.Panel panelTotal;
		///<summary>Set to true if this control is placed in the recall edit window. This affects the control behavior.</summary>
		public bool ViewingInRecall=false;
		//private double FamTotBal;
		//private double FamInsEst;
		//private double FamTotDue;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textOver90;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox text61_90;
		private System.Windows.Forms.TextBox text31_60;
		private System.Windows.Forms.TextBox text0_30;
		private System.Windows.Forms.TextBox textAgeTotal;
		private System.Windows.Forms.TextBox textAgeInsEst;
		private System.Windows.Forms.TextBox textAgeBalance;
		private FormPayment FormPayment2;
		private System.Windows.Forms.ContextMenu contextMenuIns;
		private System.Windows.Forms.MenuItem menuInsOther;
		private System.Windows.Forms.MenuItem menuInsPri;
		private System.Windows.Forms.MenuItem menuInsSec;
		private FormCommItem FormCommItem2;
		private FormPayPlan FormPayPlan2;
		private bool UrgFinNoteChanged;
		private System.Windows.Forms.CheckBox checkShowAll;
		private bool FinNoteChanged;
		private OpenDental.UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.ImageList imageListMain;
		private System.Windows.Forms.Panel panelSplitter;
		///<summary>Used only in printing reports that show subtotal only.</summary>
		public double SubTotal;
		private OpenDental.Main_Modules.TableCommLogAccount tbComm;
		private bool MouseIsDownOnSplitter;
		private int SplitterOriginalY;
		private OpenDental.UI.Button butComm;
		private System.Windows.Forms.Panel panelCommButs;
		private OpenDental.UI.Button butEmail;
		private int OriginalMousePos;
		private ClaimProc[] ClaimProcList;
		private OpenDental.ODtextBox textFinNotes;
		private System.Windows.Forms.ContextMenu menuPatient;
		private Procedure[] ProcList;
		///<summary>Indices of the items within CommLogs.List of items to actually show in the commlog list on this page. Right now, does not include Statement Sent entries.</summary>
		private ArrayList CommIndices;
		private Family FamCur;
		///<summary>public because used in FormRpStatement.</summary>
		public Patient PatCur;
		private OpenDental.UI.Button butLetterSimple;
		private System.Windows.Forms.GroupBox groupBox1;
		private OpenDental.UI.Button butLetterMerge;
		private OpenDental.UI.Button butLabel;
		private System.Windows.Forms.ContextMenu contextMenuStatement;
		private System.Windows.Forms.MenuItem menuItemStatementWalkout;
		private System.Windows.Forms.MenuItem menuItemStatementMore;
		private InsPlan[] PlanList;
		//private PaySplit[] PaySplitList;
		///<summary></summary>
		[Category("Data"),Description("Occurs when user changes current patient, usually by clicking on the Select Patient button.")]
		public event PatientSelectedEventHandler PatientSelected=null;
	
		///<summary></summary>
		public ContrAccount(){
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
			tbAccount.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbAccount_CellClicked);
			tbAccount.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbAccount_CellDoubleClicked);
			tbAcctPat.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbAcctPat_CellClicked);
		}

		///<summary></summary>
		protected override void Dispose(bool disposing){
			if(disposing){
				if(components!= null){
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		private void InitializeComponent(){
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ContrAccount));
			this.label1 = new System.Windows.Forms.Label();
			this.labelUrgFinNote = new System.Windows.Forms.Label();
			this.textUrgFinNote = new System.Windows.Forms.TextBox();
			this.tbAccount = new OpenDental.TableAccount();
			this.tbAcctPat = new OpenDental.TableAccountPat();
			this.panelTotal = new System.Windows.Forms.Panel();
			this.checkShowAll = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textOver90 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.text61_90 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.text31_60 = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.text0_30 = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textAgeTotal = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textAgeInsEst = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textAgeBalance = new System.Windows.Forms.TextBox();
			this.contextMenuIns = new System.Windows.Forms.ContextMenu();
			this.menuInsPri = new System.Windows.Forms.MenuItem();
			this.menuInsSec = new System.Windows.Forms.MenuItem();
			this.menuInsOther = new System.Windows.Forms.MenuItem();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.tbComm = new OpenDental.Main_Modules.TableCommLogAccount();
			this.panelSplitter = new System.Windows.Forms.Panel();
			this.butLetterSimple = new OpenDental.UI.Button();
			this.butComm = new OpenDental.UI.Button();
			this.panelCommButs = new System.Windows.Forms.Panel();
			this.butLabel = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butLetterMerge = new OpenDental.UI.Button();
			this.butEmail = new OpenDental.UI.Button();
			this.textFinNotes = new OpenDental.ODtextBox();
			this.menuPatient = new System.Windows.Forms.ContextMenu();
			this.contextMenuStatement = new System.Windows.Forms.ContextMenu();
			this.menuItemStatementWalkout = new System.Windows.Forms.MenuItem();
			this.menuItemStatementMore = new System.Windows.Forms.MenuItem();
			this.panelTotal.SuspendLayout();
			this.panelCommButs.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(-2, 2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(154, 16);
			this.label1.TabIndex = 9;
			this.label1.Text = "Family Financial Notes";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// labelUrgFinNote
			// 
			this.labelUrgFinNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelUrgFinNote.Location = new System.Drawing.Point(766, 31);
			this.labelUrgFinNote.Name = "labelUrgFinNote";
			this.labelUrgFinNote.Size = new System.Drawing.Size(165, 21);
			this.labelUrgFinNote.TabIndex = 10;
			this.labelUrgFinNote.Text = "Fam Urgent Fin Note";
			this.labelUrgFinNote.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textUrgFinNote
			// 
			this.textUrgFinNote.BackColor = System.Drawing.Color.White;
			this.textUrgFinNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textUrgFinNote.ForeColor = System.Drawing.Color.Red;
			this.textUrgFinNote.Location = new System.Drawing.Point(768, 53);
			this.textUrgFinNote.Multiline = true;
			this.textUrgFinNote.Name = "textUrgFinNote";
			this.textUrgFinNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textUrgFinNote.Size = new System.Drawing.Size(163, 36);
			this.textUrgFinNote.TabIndex = 11;
			this.textUrgFinNote.Text = "";
			this.textUrgFinNote.TextChanged += new System.EventHandler(this.textUrgFinNote_TextChanged);
			this.textUrgFinNote.Leave += new System.EventHandler(this.textUrgFinNote_Leave);
			// 
			// tbAccount
			// 
			this.tbAccount.BackColor = System.Drawing.SystemColors.Window;
			this.tbAccount.Location = new System.Drawing.Point(0, 65);
			this.tbAccount.Name = "tbAccount";
			this.tbAccount.ScrollValue = 1;
			this.tbAccount.SelectedIndices = new int[0];
			this.tbAccount.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.tbAccount.Size = new System.Drawing.Size(769, 357);
			this.tbAccount.TabIndex = 20;
			// 
			// tbAcctPat
			// 
			this.tbAcctPat.BackColor = System.Drawing.SystemColors.Window;
			this.tbAcctPat.Location = new System.Drawing.Point(768, 90);
			this.tbAcctPat.Name = "tbAcctPat";
			this.tbAcctPat.ScrollValue = 32;
			this.tbAcctPat.SelectedIndices = new int[0];
			this.tbAcctPat.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbAcctPat.Size = new System.Drawing.Size(163, 143);
			this.tbAcctPat.TabIndex = 21;
			// 
			// panelTotal
			// 
			this.panelTotal.Controls.Add(this.label1);
			this.panelTotal.Location = new System.Drawing.Point(768, 234);
			this.panelTotal.Name = "panelTotal";
			this.panelTotal.Size = new System.Drawing.Size(163, 21);
			this.panelTotal.TabIndex = 26;
			// 
			// checkShowAll
			// 
			this.checkShowAll.Checked = true;
			this.checkShowAll.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowAll.Location = new System.Drawing.Point(4, 48);
			this.checkShowAll.Name = "checkShowAll";
			this.checkShowAll.Size = new System.Drawing.Size(146, 16);
			this.checkShowAll.TabIndex = 29;
			this.checkShowAll.Text = "Show entire history";
			this.checkShowAll.Click += new System.EventHandler(this.checkShowAll_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(161, 46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 18);
			this.label2.TabIndex = 30;
			this.label2.Text = "Family Aging";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textOver90
			// 
			this.textOver90.Location = new System.Drawing.Point(443, 44);
			this.textOver90.Name = "textOver90";
			this.textOver90.ReadOnly = true;
			this.textOver90.Size = new System.Drawing.Size(60, 20);
			this.textOver90.TabIndex = 31;
			this.textOver90.Text = "";
			this.textOver90.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(443, 31);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60, 13);
			this.label3.TabIndex = 32;
			this.label3.Text = "over 90";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(383, 31);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(60, 13);
			this.label5.TabIndex = 34;
			this.label5.Text = "61-90";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// text61_90
			// 
			this.text61_90.Location = new System.Drawing.Point(383, 44);
			this.text61_90.Name = "text61_90";
			this.text61_90.ReadOnly = true;
			this.text61_90.Size = new System.Drawing.Size(60, 20);
			this.text61_90.TabIndex = 33;
			this.text61_90.Text = "";
			this.text61_90.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(323, 31);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(60, 13);
			this.label6.TabIndex = 36;
			this.label6.Text = "31-60";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// text31_60
			// 
			this.text31_60.Location = new System.Drawing.Point(323, 44);
			this.text31_60.Name = "text31_60";
			this.text31_60.ReadOnly = true;
			this.text31_60.Size = new System.Drawing.Size(60, 20);
			this.text31_60.TabIndex = 35;
			this.text31_60.Text = "";
			this.text31_60.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(265, 31);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(60, 13);
			this.label7.TabIndex = 38;
			this.label7.Text = "0-30";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// text0_30
			// 
			this.text0_30.Location = new System.Drawing.Point(263, 44);
			this.text0_30.Name = "text0_30";
			this.text0_30.ReadOnly = true;
			this.text0_30.Size = new System.Drawing.Size(60, 20);
			this.text0_30.TabIndex = 37;
			this.text0_30.Text = "";
			this.text0_30.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(503, 31);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(60, 13);
			this.label8.TabIndex = 40;
			this.label8.Text = "Total";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textAgeTotal
			// 
			this.textAgeTotal.Location = new System.Drawing.Point(503, 44);
			this.textAgeTotal.Name = "textAgeTotal";
			this.textAgeTotal.ReadOnly = true;
			this.textAgeTotal.Size = new System.Drawing.Size(60, 20);
			this.textAgeTotal.TabIndex = 39;
			this.textAgeTotal.Text = "";
			this.textAgeTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(563, 31);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(60, 13);
			this.label9.TabIndex = 42;
			this.label9.Text = "- InsEst";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textAgeInsEst
			// 
			this.textAgeInsEst.Location = new System.Drawing.Point(563, 44);
			this.textAgeInsEst.Name = "textAgeInsEst";
			this.textAgeInsEst.ReadOnly = true;
			this.textAgeInsEst.Size = new System.Drawing.Size(60, 20);
			this.textAgeInsEst.TabIndex = 41;
			this.textAgeInsEst.Text = "";
			this.textAgeInsEst.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label10.Location = new System.Drawing.Point(626, 31);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(60, 13);
			this.label10.TabIndex = 44;
			this.label10.Text = "= Balance";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textAgeBalance
			// 
			this.textAgeBalance.Location = new System.Drawing.Point(623, 44);
			this.textAgeBalance.Name = "textAgeBalance";
			this.textAgeBalance.ReadOnly = true;
			this.textAgeBalance.Size = new System.Drawing.Size(65, 20);
			this.textAgeBalance.TabIndex = 43;
			this.textAgeBalance.Text = "";
			this.textAgeBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// contextMenuIns
			// 
			this.contextMenuIns.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																									 this.menuInsPri,
																																									 this.menuInsSec,
																																									 this.menuInsOther});
			// 
			// menuInsPri
			// 
			this.menuInsPri.Index = 0;
			this.menuInsPri.Text = "Primary";
			this.menuInsPri.Click += new System.EventHandler(this.menuInsPri_Click);
			// 
			// menuInsSec
			// 
			this.menuInsSec.Index = 1;
			this.menuInsSec.Text = "Secondary";
			this.menuInsSec.Click += new System.EventHandler(this.menuInsSec_Click);
			// 
			// menuInsOther
			// 
			this.menuInsOther.Index = 2;
			this.menuInsOther.Text = "Other";
			this.menuInsOther.Click += new System.EventHandler(this.menuInsOther_Click);
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListMain;
			this.ToolBarMain.Location = new System.Drawing.Point(0, 0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(939, 29);
			this.ToolBarMain.TabIndex = 47;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// imageListMain
			// 
			this.imageListMain.ImageSize = new System.Drawing.Size(22, 22);
			this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
			this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tbComm
			// 
			this.tbComm.BackColor = System.Drawing.SystemColors.Window;
			this.tbComm.Location = new System.Drawing.Point(0, 430);
			this.tbComm.Name = "tbComm";
			this.tbComm.ScrollValue = 433;
			this.tbComm.SelectedIndices = new int[0];
			this.tbComm.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbComm.Size = new System.Drawing.Size(769, 302);
			this.tbComm.TabIndex = 48;
			this.tbComm.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(this.tbComm_CellDoubleClicked);
			// 
			// panelSplitter
			// 
			this.panelSplitter.Cursor = System.Windows.Forms.Cursors.SizeNS;
			this.panelSplitter.Location = new System.Drawing.Point(0, 425);
			this.panelSplitter.Name = "panelSplitter";
			this.panelSplitter.Size = new System.Drawing.Size(769, 4);
			this.panelSplitter.TabIndex = 49;
			this.panelSplitter.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelSplitter_MouseUp);
			this.panelSplitter.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelSplitter_MouseMove);
			this.panelSplitter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelSplitter_MouseDown);
			// 
			// butLetterSimple
			// 
			this.butLetterSimple.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butLetterSimple.Autosize = true;
			this.butLetterSimple.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLetterSimple.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLetterSimple.Location = new System.Drawing.Point(4, 14);
			this.butLetterSimple.Name = "butLetterSimple";
			this.butLetterSimple.Size = new System.Drawing.Size(82, 25);
			this.butLetterSimple.TabIndex = 50;
			this.butLetterSimple.Text = "Simple";
			this.butLetterSimple.Click += new System.EventHandler(this.butLetterSimple_Click);
			// 
			// butComm
			// 
			this.butComm.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butComm.Autosize = true;
			this.butComm.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butComm.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butComm.Image = ((System.Drawing.Image)(resources.GetObject("butComm.Image")));
			this.butComm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butComm.Location = new System.Drawing.Point(0, 0);
			this.butComm.Name = "butComm";
			this.butComm.Size = new System.Drawing.Size(90, 26);
			this.butComm.TabIndex = 68;
			this.butComm.Text = "Comm";
			this.butComm.Click += new System.EventHandler(this.butComm_Click);
			// 
			// panelCommButs
			// 
			this.panelCommButs.Controls.Add(this.butLabel);
			this.panelCommButs.Controls.Add(this.groupBox1);
			this.panelCommButs.Controls.Add(this.butEmail);
			this.panelCommButs.Controls.Add(this.butComm);
			this.panelCommButs.Location = new System.Drawing.Point(769, 429);
			this.panelCommButs.Name = "panelCommButs";
			this.panelCommButs.Size = new System.Drawing.Size(163, 242);
			this.panelCommButs.TabIndex = 69;
			// 
			// butLabel
			// 
			this.butLabel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butLabel.Autosize = true;
			this.butLabel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLabel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLabel.Image = ((System.Drawing.Image)(resources.GetObject("butLabel.Image")));
			this.butLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butLabel.Location = new System.Drawing.Point(0, 105);
			this.butLabel.Name = "butLabel";
			this.butLabel.Size = new System.Drawing.Size(90, 25);
			this.butLabel.TabIndex = 71;
			this.butLabel.Text = "Label";
			this.butLabel.Click += new System.EventHandler(this.butLabel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butLetterMerge);
			this.groupBox1.Controls.Add(this.butLetterSimple);
			this.groupBox1.Location = new System.Drawing.Point(4, 28);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(111, 73);
			this.groupBox1.TabIndex = 70;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Letter";
			// 
			// butLetterMerge
			// 
			this.butLetterMerge.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butLetterMerge.Autosize = true;
			this.butLetterMerge.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLetterMerge.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLetterMerge.Location = new System.Drawing.Point(4, 41);
			this.butLetterMerge.Name = "butLetterMerge";
			this.butLetterMerge.Size = new System.Drawing.Size(82, 25);
			this.butLetterMerge.TabIndex = 51;
			this.butLetterMerge.Text = "Merge";
			this.butLetterMerge.Click += new System.EventHandler(this.butLetterMerge_Click);
			// 
			// butEmail
			// 
			this.butEmail.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEmail.Autosize = true;
			this.butEmail.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEmail.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEmail.Location = new System.Drawing.Point(0, 132);
			this.butEmail.Name = "butEmail";
			this.butEmail.Size = new System.Drawing.Size(90, 25);
			this.butEmail.TabIndex = 69;
			this.butEmail.Text = "E-mail";
			this.butEmail.Click += new System.EventHandler(this.butEmail_Click);
			// 
			// textFinNotes
			// 
			this.textFinNotes.AcceptsReturn = true;
			this.textFinNotes.Location = new System.Drawing.Point(768, 254);
			this.textFinNotes.Multiline = true;
			this.textFinNotes.Name = "textFinNotes";
			this.textFinNotes.QuickPasteType = OpenDental.QuickPasteType.FinancialNotes;
			this.textFinNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textFinNotes.Size = new System.Drawing.Size(162, 168);
			this.textFinNotes.TabIndex = 70;
			this.textFinNotes.Text = "";
			this.textFinNotes.Leave += new System.EventHandler(this.textFinNotes_Leave);
			this.textFinNotes.TextChanged += new System.EventHandler(this.textFinNotes_TextChanged);
			// 
			// contextMenuStatement
			// 
			this.contextMenuStatement.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																												 this.menuItemStatementWalkout,
																																												 this.menuItemStatementMore});
			// 
			// menuItemStatementWalkout
			// 
			this.menuItemStatementWalkout.Index = 0;
			this.menuItemStatementWalkout.Text = "Walkout";
			this.menuItemStatementWalkout.Click += new System.EventHandler(this.menuItemStatementWalkout_Click);
			// 
			// menuItemStatementMore
			// 
			this.menuItemStatementMore.Index = 1;
			this.menuItemStatementMore.Text = "More Options";
			this.menuItemStatementMore.Click += new System.EventHandler(this.menuItemStatementMore_Click);
			// 
			// ContrAccount
			// 
			this.Controls.Add(this.textFinNotes);
			this.Controls.Add(this.panelSplitter);
			this.Controls.Add(this.tbComm);
			this.Controls.Add(this.panelCommButs);
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.textAgeBalance);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.textAgeInsEst);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.textAgeTotal);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.text0_30);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.text31_60);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.text61_90);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textOver90);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.panelTotal);
			this.Controls.Add(this.tbAcctPat);
			this.Controls.Add(this.tbAccount);
			this.Controls.Add(this.textUrgFinNote);
			this.Controls.Add(this.labelUrgFinNote);
			this.Controls.Add(this.checkShowAll);
			this.Name = "ContrAccount";
			this.Size = new System.Drawing.Size(939, 732);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ContrAccount_Layout);
			this.panelTotal.ResumeLayout(false);
			this.panelCommButs.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		///<summary></summary>
		public void InstantClasses(){
			//can't use Lan.F(this);
			Lan.C(this,new Control[]
				{
          checkShowAll,
					label2,
					label7,
					label6,
					label5,
					label3,
					label8,
					label9,
					label10,
					labelUrgFinNote,
					label1,
					butComm,
					butLetterSimple,
					butLetterMerge,
					butLabel,
					butEmail
				});
			LayoutToolBar();
			if(ViewingInRecall){
				panelSplitter.Top=300;//start the splitter higher for recall window.
			}
		}

		///<summary>Causes the toolbar to be laid out again.</summary>
		public void LayoutToolBar(){
			ToolBarMain.Buttons.Clear();
			ODToolBarButton button;
			button=new ODToolBarButton(Lan.g(this,"Select Patient"),0,"","Patient");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=menuPatient;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Payment"),1,"","Payment"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Adjustment"),2,"","Adjustment"));
			button=new ODToolBarButton(Lan.g(this,"New Claim"),3,"","Insurance");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=contextMenuIns;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Payment Plan"),-1,"","PayPlan"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			button=new ODToolBarButton(Lan.g(this,"Statement"),4,"","Statement");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=contextMenuStatement;
			ToolBarMain.Buttons.Add(button);
			ArrayList toolButItems=ToolButItems.GetForToolBar(ToolBarsAvail.AccountModule);
			for(int i=0;i<toolButItems.Count;i++){
				ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
				ToolBarMain.Buttons.Add(new ODToolBarButton(((ToolButItem)toolButItems[i]).ButtonText
					,-1,"",((ToolButItem)toolButItems[i]).ProgramNum));
			}
			ToolBarMain.Invalidate();
		}

		private void ContrAccount_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
			tbAccount.InstantClasses();
			tbAccount.Height=panelSplitter.Top-tbAccount.Location.Y+1;
			tbAccount.LayoutTables();
			tbComm.InstantClasses();
			tbComm.Top=panelSplitter.Bottom-1;
			tbComm.Height=Height-tbComm.Top;
			panelCommButs.Top=panelSplitter.Bottom-1;
			tbComm.LayoutTables();
			if(panelSplitter.Top>=446){//Height>=446){
				textUrgFinNote.Height=72;//5 lines
			}
			else{
				textUrgFinNote.Height=46;//3 lines
			}
			tbAcctPat.InstantClasses();
			tbAcctPat.Location=new Point(768,textUrgFinNote.Location.Y+textUrgFinNote.Height);
			panelTotal.Location=new Point(768,tbAcctPat.Location.Y+tbAcctPat.Height);
			textFinNotes.Location=new Point(768,panelTotal.Location.Y+panelTotal.Height);
			textFinNotes.Height=panelSplitter.Top-textFinNotes.Top;
		}

		///<summary></summary>
		public void ModuleSelected(int patNum){
			RefreshModuleData(patNum);
			RefreshModuleScreen();
		}

		///<summary></summary>
		public void ModuleUnselected(){
			if(FamCur==null)
				return;
			if(UrgFinNoteChanged){
				//Patient tempPat=Patients.Cur;
				Patient patOld=FamCur.List[0].Copy();
				//Patients.CurOld=Patients.Cur.Copy();//important
				FamCur.List[0].FamFinUrgNote=textUrgFinNote.Text;
				FamCur.List[0].Update(patOld);
				//Patients.GetFamily(tempPat.PatNum);
				UrgFinNoteChanged=false;
			}
			if(FinNoteChanged){
				PatientNotes.Cur.FamFinancial=textFinNotes.Text;
				PatientNotes.UpdateCur(PatCur.Guarantor);
				FinNoteChanged=false;
			}
			FamCur=null;
			//InsPlans.List=null;
			CovPats.List=null;
			//from FillAcctLineAL(){
			//Procedures.List=null;
			//Procedures.HList=null;
			//Procedures.MissingTeeth=null;
			Claims.List=null;
			//Adjustments.List=null;
			//PaySplitList=null;
			Commlogs.List=null;
			PayPlans.List=null;
			ClaimProcList=null;
		}

		/// <summary>Public because called from FormRpStatement</summary>
		public void RefreshModuleData(int patNum){
			if(patNum==0){
				PatCur=null;
				FamCur=null;
				return;
			}
			FamCur=Patients.GetFamily(patNum);
			PatCur=FamCur.GetPatient(patNum);
			PlanList=InsPlans.Refresh(FamCur);
			CovPats.Refresh(PatCur,PlanList);
			PatientNotes.Refresh(PatCur.PatNum,PatCur.Guarantor);
			//other tables are refreshed in FillAcctLineAL
		}

		private void RefreshModuleScreen(){
			if(PatCur!=null){
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString+" - "
					+PatCur.GetNameLF();
				tbAccount.Enabled=true;
				ToolBarMain.Buttons["Payment"].Enabled=true;
				ToolBarMain.Buttons["Adjustment"].Enabled=true;
				ToolBarMain.Buttons["Insurance"].Enabled=true;
				ToolBarMain.Buttons["PayPlan"].Enabled=true;
				ToolBarMain.Buttons["Statement"].Enabled=true;
				ToolBarMain.Invalidate();
				textUrgFinNote.Enabled=true;
				textFinNotes.Enabled=true;
				//butEditUrg.Enabled=true;
				//butEditFin.Enabled=true;
			}
			else{
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString;
				tbAccount.Enabled=false;
				ToolBarMain.Buttons["Payment"].Enabled=false;
				ToolBarMain.Buttons["Adjustment"].Enabled=false;
				ToolBarMain.Buttons["Insurance"].Enabled=false;
				ToolBarMain.Buttons["PayPlan"].Enabled=false;
				ToolBarMain.Buttons["Statement"].Enabled=false;
				ToolBarMain.Invalidate();
				textUrgFinNote.Enabled=false;
				textFinNotes.Enabled=false;
				//butEditUrg.Enabled=false;
				//butEditFin.Enabled=false;
			}
			FillPatientButton();
			FillMain();
			FillPats();
			FillMisc();
			FillAging();
			FillComm();
		}

		private void FillPatientButton(){
			Patients.AddPatsToMenu(menuPatient,new EventHandler(menuPatient_Click),PatCur,FamCur);
		}

		private void menuPatient_Click(object sender,System.EventArgs e) {
			int newPatNum=Patients.ButtonSelect(menuPatient,sender,FamCur);
			OnPatientSelected(newPatNum);
			ModuleSelected(newPatNum);
		}

		private void FillPats(){
			if(PatCur==null){
				tbAcctPat.ResetRows(0);
				tbAcctPat.LayoutTables();
				return;
			}
			//double total=0;
			tbAcctPat.ResetRows(FamCur.List.Length);
			for(int i=0;i<FamCur.List.Length;i++){
				tbAcctPat.Cell[0,i]=FamCur.GetNameInFamLFI(i);
				tbAcctPat.Cell[1,i]=FamCur.List[i].EstBalance.ToString("F");
				if(i==0){
					tbAcctPat.FontBold[0,i]=true;
					tbAcctPat.FontBold[1,i]=true;
				}
				//total=total+Patients.FamilyList[i].EstBalance;
				if(FamCur.List[i].PatNum==PatCur.PatNum){
					tbAcctPat.ColorRow(i,Color.DarkSalmon);
					tbAcctPat.SelectedRow=i;
				}
			}
			tbAcctPat.LayoutTables();
		}

		private void FillMisc(){
			if(PatCur!=null){
				textUrgFinNote.Text=FamCur.List[0].FamFinUrgNote;
				textFinNotes.Text=PatientNotes.Cur.FamFinancial;	
				textFinNotes.Select(textFinNotes.Text.Length+2,1);
				textFinNotes.ScrollToCaret();
				textUrgFinNote.SelectionStart=0;
				textUrgFinNote.ScrollToCaret();
			}
			else{
				textUrgFinNote.Text="";
				textFinNotes.Text="";				
			}
			UrgFinNoteChanged=false;
			FinNoteChanged=false;
			if(ViewingInRecall){
				textUrgFinNote.ReadOnly=true;
				textFinNotes.ReadOnly=true;		
			}
			else{
				textUrgFinNote.ReadOnly=false;
				textFinNotes.ReadOnly=false;		
			}
		}

		private void FillAging(){
			if(PatCur!=null){
				textOver90.Text=FamCur.List[0].BalOver90.ToString("F");
				text61_90.Text=FamCur.List[0].Bal_61_90.ToString("F");
				text31_60.Text=FamCur.List[0].Bal_31_60.ToString("F");
				text0_30.Text=FamCur.List[0].Bal_0_30.ToString("F");
				double total=FamCur.List[0].BalTotal;
				textAgeTotal.Text=total.ToString("F");
				textAgeInsEst.Text=FamCur.List[0].InsEst.ToString("F");
				textAgeBalance.Text=(total-FamCur.List[0].InsEst).ToString("F");
			}
			else{
				textOver90.Text="";
				text61_90.Text="";
				text31_60.Text="";
				text0_30.Text="";
				textAgeTotal.Text="";
				textAgeInsEst.Text="";
				textAgeBalance.Text="";
			}
		}

		/// <summary>Fills the commlog table on this form.</summary>
		private void FillComm(){
			if(PatCur==null){
				tbComm.ResetRows(0);
				tbComm.LayoutTables();
				panelCommButs.Enabled=false;
				return;
			}
			panelCommButs.Enabled=true;
			if(PatCur.Email==""){
				butEmail.Enabled=false;
			}
			else{
				butEmail.Enabled=true;
			}
			//Commlogs.Refresh();//already done in FillMain().
			CommIndices=new ArrayList();
			for(int i=0;i<Commlogs.List.Length;i++){
				if(Commlogs.List[i].CommType==CommItemType.StatementSent){
					continue;
				}
				CommIndices.Add(i);
			}
			tbComm.ResetRows(CommIndices.Count);
			for(int i=0;i<CommIndices.Count;i++){
				tbComm.Cell[0,i]=Commlogs.List[(int)CommIndices[i]].CommDateTime.ToShortDateString();
				tbComm.Cell[1,i]=Commlogs.List[(int)CommIndices[i]].CommType.ToString();
				tbComm.Cell[2,i]=Commlogs.List[(int)CommIndices[i]].Mode.ToString();
				tbComm.Cell[3,i]=Commlogs.List[(int)CommIndices[i]].Note;
			}
			tbComm.SetGridColor(Color.Gray);
			tbComm.LayoutTables();
		}

		private void FillMain(DateTime fromDate, DateTime toDate,bool includeClaims,bool subtotalsOnly){
			//tbAccount.SelectedRowsAL=new ArrayList();
			if(PatCur==null){
				tbAccount.ResetRows(0);
				tbAccount.LayoutTables();
				return;
			}
			FillAcctLineAL(fromDate,toDate,includeClaims,subtotalsOnly);
			FilltbAccount();
		}

		private void FillMain(){
			if(checkShowAll.Checked){
				FillMain(DateTime.MinValue,DateTime.MaxValue,true,false);
			}
			else{
				FillMain(DateTime.Today.AddDays(-45),DateTime.MaxValue,true,false);
			}
		}

		///<summary>Used once in FillAcctLineAL. Returns a list of PayInfos organized by date.</summary>
		private PayInfo[] GetPayInfoList(PaySplit[] PaySplitList,Payment[] PaymentList){
			ArrayList retAL=new ArrayList();
			PayInfo payInfo;
			ArrayList summaries=new ArrayList();//a list of PayInfos for summaries.
			//double dailyAmt;
			ArrayList splitsForPayment;
			//first, loop through all payments.  All payments will be included, but only part of the amount might be included.
			for(int i=0;i<PaymentList.Length;i++){
				payInfo=new PayInfo();
				payInfo.Type=PayInfoType.Payment;
				payInfo.Date=PaymentList[i].PayDate;
				payInfo.Description=Defs.GetName(DefCat.PaymentTypes,PaymentList[i].PayType)
					+" "+PaymentList[i].PayAmt.ToString("c");
				payInfo.Amount=0;
				payInfo.PayNum=PaymentList[i].PayNum;
				payInfo.PayPlanNum=0;//??
				//loop through all paysplits for this payment (splits for this patient only).
				splitsForPayment=PaySplits.GetForPayment(PaymentList[i].PayNum,PaySplitList);
				for(int j=0;j<splitsForPayment.Count;j++){
					//only those amounts that have the same procDate and patNum as the payment,
					//and are not attached to procedures.
					//(The payment total amount will show in the description)
					if(((PaySplit)splitsForPayment[j]).ProcDate==PaymentList[i].PayDate
						&& ((PaySplit)splitsForPayment[j]).ProcNum==0)
					{
						payInfo.Amount+=((PaySplit)splitsForPayment[j]).SplitAmt;
					}
				}
				retAL.Add(payInfo);
			}
			//then, paysplits
			for(int i=0;i<PaySplitList.Length;i++){
				//don't include PaySplits with the same date as the payment.
				//Those will either be proc line items & summaries, or part of the payment
				//UNLESS payment is in a different patient.
				if(PaySplitList[i].ProcDate==PaySplitList[i].DatePay){
					bool isOtherPat=true;
					for(int j=0;j<PaymentList.Length;j++){
						if(PaymentList[j].PayNum==PaySplitList[i].PayNum){
							//if this split is attached to a payment by this pat
							isOtherPat=false;
						}
					}
					if(!isOtherPat){
						continue;
					}
					//otherwise, if isOtherPat, then the payment will show
				}
				//don't include PaySplits that are attached to procs
				if(PaySplitList[i].ProcNum>0){
					continue;
				}
				payInfo=new PayInfo();
				payInfo.Type=PayInfoType.PaySplit;
				payInfo.Date=PaySplitList[i].ProcDate;
				payInfo.Description=Lan.g(this,"(split)")+" "+PaySplitList[i].DatePay.ToShortDateString();
				payInfo.Amount=PaySplitList[i].SplitAmt;
				payInfo.PayNum=PaySplitList[i].PayNum;
				payInfo.PayPlanNum=PaySplitList[i].PayPlanNum;
				retAL.Add(payInfo);
			}
			//lastly, paysplit summaries.
			for(int i=0;i<PaySplitList.Length;i++){
				//don't include PaySplits with the same date as the payment.
				if(PaySplitList[i].ProcDate==PaySplitList[i].DatePay){
					continue;
				}
				//don't include PaySplits that are not attached to procs
				if(PaySplitList[i].ProcNum==0){
					continue;
				}
				payInfo=new PayInfo();
				//find a summary with the same date
				bool found=false;
				for(int j=0;j<summaries.Count;j++){
					if(((PayInfo)summaries[j]).Date==PaySplitList[i].ProcDate){
						payInfo=((PayInfo)summaries[j]).Copy();
						found=true;
						summaries.RemoveAt(j);//it will be added back in a moment
						break;
					}
				}
				if(!found){
					payInfo.Type=PayInfoType.DailySummary;
					payInfo.Date=PaySplitList[i].ProcDate;
					payInfo.Description=
						"( "+Lan.g(this,"daily split")
						+") "+PaySplitList[i].DatePay.ToShortDateString();						
					payInfo.Amount=0;
					payInfo.PayNum=PaySplitList[i].PayNum;
					payInfo.PayPlanNum=PaySplitList[i].PayPlanNum;
				}
				payInfo.Amount+=PaySplitList[i].SplitAmt;//this amount will be zeroed out shortly
				summaries.Add(payInfo);
			}
			//add the summaries to the result
			for(int i=0;i<summaries.Count;i++){
				payInfo=(PayInfo)summaries[i];	
				payInfo.Description=
					payInfo.Amount.ToString("c")+" "
					+payInfo.Description;
				payInfo.Amount=0;
				retAL.Add(payInfo);
			}
			//convert to array
			PayInfo[] retVal=new PayInfo[retAL.Count];
			DateTime[] dateArray=new DateTime[retAL.Count];
			for(int i=0;i<retVal.Length;i++){
				retVal[i]=(PayInfo)retAL[i];
				dateArray[i]=retVal[i].Date;
			}
			//order everything by date
			Array.Sort(dateArray,retVal);
			return retVal;
		}

		/// <summary>Public because called from FormRpStatement</summary>
		public void FillAcctLineAL(DateTime fromDate, DateTime toDate,bool includeClaims,bool subtotalsOnly){
			ProcList=Procedures.Refresh(PatCur.PatNum);
			Claims.Refresh(PatCur.PatNum);
			Adjustment[] AdjustmentList=Adjustments.Refresh(PatCur.PatNum);
			PaySplit[] PaySplitList=PaySplits.Refresh(PatCur.PatNum);
			Payment[] PaymentList=Payments.Refresh(PatCur.PatNum);//for display purposes only.
			PayInfo[] PayInfoList=GetPayInfoList(PaySplitList,PaymentList);
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			Commlogs.Refresh(PatCur.PatNum);
			PayPlans.Refresh(PatCur.PatNum,PatCur.PatNum);//where this patient is either guar or pat
			//if the computed balance does not match the balance on record,
			if(Shared.ComputeBalances(ProcList,ClaimProcList,PatCur,PaySplitList,AdjustmentList)){
				//then recompute aging for family. This is time consuming, about 1/2 second.
				//Compute aging involves about 10 to 12 database calls.
				Ledgers.ComputeAging(PatCur.Guarantor);
				FamCur=Patients.GetFamily(PatCur.PatNum);
				PatCur=FamCur.GetPatient(PatCur.PatNum);
			}
			arrayProc = new Procedure[ProcList.Length];
			arrayClaim=new Claim[Claims.List.Length];
			arrayAdj = new Adjustment[AdjustmentList.Length];
			arrayPay =new PayInfo[PayInfoList.Length];
			arrayComm =new Commlog[Commlogs.List.Length];
			arrayPayPlan=new PayPlan[PayPlans.List.Length];
			//step through all procedures for patient and move selected ones (completed) to
			//arrayProc, also arrayClaim, arrayAdj ,arrayPay, all ordered by date.
			//Pull from all 4 into AcctLineAL array for display.  Every AcctLineAL entry
			//contains type and index to access original array. Notes are handled like any
			//other line, just no numbers.(actually no notes yet)
			int countProc=0;
			int countClaim=0;
			int countAdj=0;
			int countPay=0;
			int countComm=0;
			int countPayPlan=0;
			for(int i=0;i<ProcList.Length;i++){
				if(ProcList[i].ProcStatus==ProcStat.C)//Only add if proc is Complete
				{
					arrayProc[countProc]=ProcList[i];
					countProc++;
				}
			}
			for(int i=0;i<Claims.List.Length;i++){
				if(Claims.List[i].ClaimStatus!="A"//don't show ins adjustments
					&& Claims.List[i].ClaimType!="PreAuth")//don't show preauthorizations.
				{
					arrayClaim[countClaim]=Claims.List[i];
					countClaim++;
				}
			}
			for(int i=0;i<AdjustmentList.Length;i++){
				if(AdjustmentList[i].ProcNum==0)//only if not attached to a proc
				{
					arrayAdj[countAdj]=AdjustmentList[i];
					countAdj++;
				}
			}
			for(int i=0;i<PayInfoList.Length;i++){
				arrayPay[countPay]=PayInfoList[i];
				countPay++;
			}
			for(int i=0;i<Commlogs.List.Length;i++){
				if(Commlogs.List[i].CommType==CommItemType.StatementSent){//only show statementSents.
					arrayComm[countComm]=Commlogs.List[i];
					countComm++;
				}
			}
			for(int i=0;i<PayPlans.List.Length;i++){
				arrayPayPlan[countPayPlan]=PayPlans.List[i];
				countPayPlan++;
			}
			int tempCountProc=0;
			int tempCountClaim=0;
			int tempCountAdj=0;
			int tempCountPay=0;
			int tempCountComm=0;
			int tempCountPayPlan=0;
			AcctLineAL=new ArrayList();
			AcctLine tempAcctLine=new AcctLine();
			//This is where to transfer arrays to AcctLineAL:
			DateTime lineDate=DateTime.MinValue;
				//tempAcctLine.Description="Starting Balance";
			double runBal=0;
				//tempAcctLine.Balance=runBal.ToString("F");
				//AcctLineAL.Add(tempAcctLine);
			for(int j=0;j<countProc+countClaim+countAdj+countPay+countComm+countPayPlan+1;j++){
			//for(int i=0;i<AcctLineAL.Length;i++){
				//set lineDate to the value of the first array that is not maxed out:
				if     (tempCountProc<countProc) lineDate=arrayProc[tempCountProc].ProcDate;
				else if(tempCountClaim<countClaim) lineDate=arrayClaim[tempCountClaim].DateService;
				else if(tempCountAdj<countAdj) lineDate=arrayAdj[tempCountAdj].AdjDate;
				else if(tempCountPay<countPay) lineDate=arrayPay[tempCountPay].Date;
				else if(tempCountComm<countComm) lineDate=arrayComm[tempCountComm].CommDateTime.Date;
				else if(tempCountPayPlan<countPayPlan) lineDate=arrayPayPlan[tempCountPayPlan].PayPlanDate;
				//find next date
				if(tempCountProc<countProc && DateTime.Compare(arrayProc[tempCountProc].ProcDate,lineDate)<=0)
					lineDate=arrayProc[tempCountProc].ProcDate;
				if(tempCountClaim<countClaim && DateTime.Compare(arrayClaim[tempCountClaim].DateService,lineDate)<0)
					lineDate=arrayClaim[tempCountClaim].DateService;
				if(tempCountAdj<countAdj && DateTime.Compare(arrayAdj[tempCountAdj].AdjDate,lineDate)<0)
					lineDate=arrayAdj[tempCountAdj].AdjDate;
				if(tempCountPay<countPay 
					&& DateTime.Compare(arrayPay[tempCountPay].Date,lineDate)<=0)
					lineDate=arrayPay[tempCountPay].Date;
				if(tempCountComm<countComm
					&& DateTime.Compare(arrayComm[tempCountComm].CommDateTime.Date,lineDate)<=0)
					lineDate=arrayComm[tempCountComm].CommDateTime.Date;
				if(tempCountPayPlan<countPayPlan
					&& DateTime.Compare(arrayPayPlan[tempCountPayPlan].PayPlanDate,lineDate)<=0)
					lineDate=arrayPayPlan[tempCountPayPlan].PayPlanDate;
				//1. Procedure
				if(tempCountProc<countProc && arrayProc[tempCountProc].ProcDate==lineDate){
					tempAcctLine=new AcctLine();
					tempAcctLine.Type=AcctType.Proc;
					tempAcctLine.Index=tempCountProc;
					tempAcctLine.Date=arrayProc[tempCountProc].ProcDate.ToString("d");
					tempAcctLine.Provider=Providers.GetAbbr(arrayProc[tempCountProc].ProvNum);
					tempAcctLine.Code=arrayProc[tempCountProc].ADACode;
					tempAcctLine.Tooth=Tooth.ToInternat(arrayProc[tempCountProc].ToothNum);
					tempAcctLine.Description=ProcedureCodes.GetProcCode(arrayProc[tempCountProc].ADACode).Descript;
					double fee=arrayProc[tempCountProc].ProcFee;
					double insEst=ClaimProcs.ProcEstNotReceived(ClaimProcList,arrayProc[tempCountProc].ProcNum);
					double insPay=ClaimProcs.ProcInsPay(ClaimProcList,arrayProc[tempCountProc].ProcNum);
					double pat=fee
						-arrayProc[tempCountProc].GetWriteOffC(ClaimProcList)//this is for CapComplete
						-insEst
						-insPay;						
					double adj=Adjustments.GetTotForProc(arrayProc[tempCountProc].ProcNum,AdjustmentList);
					double paid=PaySplits.GetTotForProc(arrayProc[tempCountProc].ProcNum,PaySplitList);
					double subtot=pat+adj-paid;
					tempAcctLine.Fee=fee.ToString("F");
					tempAcctLine.InsEst=ClaimProcs.ProcDisplayInsEst(ClaimProcList,arrayProc[tempCountProc].ProcNum);
					tempAcctLine.InsPay=insPay.ToString("F");
					tempAcctLine.Patient=pat.ToString("F");
					if(adj!=0){
						tempAcctLine.Adj=adj.ToString("F");
					}
					if(paid!=0){
						tempAcctLine.Paid=(-paid).ToString("F");
					}		
					if(!arrayProc[tempCountProc].IsCoveredIns(ClaimProcList)){//not covered by ins
						tempAcctLine.InsEst="";
						tempAcctLine.InsPay="";
					}
					else if(arrayProc[tempCountProc].NoBillIns(ClaimProcList)){//should not bill to ins
						tempAcctLine.InsEst="";
						tempAcctLine.InsPay="No Bill";
					}
					else if(arrayProc[tempCountProc].IsUnsent(ClaimProcList)
						&& arrayProc[tempCountProc].ProcFee>0)
					{
						tempAcctLine.InsPay="Unsent";
					}
					if(arrayProc[tempCountProc].ProcDate >= fromDate 
						&& arrayProc[tempCountProc].ProcDate <= toDate)//within date range
					{
						runBal+=subtot;
						tempAcctLine.Balance=runBal.ToString("F");
						AcctLineAL.Add(tempAcctLine);
					}
					else if(!subtotalsOnly){//out of date range, but show normal totals
						runBal+=subtot;//add to the running balance, but do not display it.
					}
					if(tempCountProc<countProc){
						tempCountProc++;
					}
				}//end Proc
				//2. Claim
				else if(tempCountClaim<countClaim && DateTime.Compare(arrayClaim[tempCountClaim].DateService,lineDate)==0){
					tempAcctLine=new AcctLine();
					tempAcctLine.Type=AcctType.Claim;
					tempAcctLine.Index=tempCountClaim;
					tempAcctLine.Date=arrayClaim[tempCountClaim].DateService.ToString("d");
					tempAcctLine.Provider=Providers.GetAbbr(arrayClaim[tempCountClaim].ProvTreat);
					tempAcctLine.Code="Ins";
					tempAcctLine.Tooth="";
					if(arrayClaim[tempCountClaim].ClaimType=="P"){
						tempAcctLine.Description="Pri Claim: ";
					}
					else if(arrayClaim[tempCountClaim].ClaimType=="S"){
						tempAcctLine.Description="Sec Claim: ";
					}
					tempAcctLine.Description
						+=InsPlans.GetCarrierName(arrayClaim[tempCountClaim].PlanNum,PlanList);
					if(arrayClaim[tempCountClaim].DedApplied>0){
						tempAcctLine.Description+=". Ded applied $"+arrayClaim[tempCountClaim].DedApplied.ToString("F");
					}
					double fee;
					double insEst;
					double insPay;
					double subTotal=0;
					Claims.Cur=arrayClaim[tempCountClaim];
					fee=Claims.Cur.ClaimFee;
					insEst=Claims.Cur.InsPayEst;
					//insPay=Claims.Cur.InsPayAmt;
					tempAcctLine.Fee=fee.ToString("F");
					tempAcctLine.InsEst=insEst.ToString("F");
					//insPay is always subtracted from bal no matter what is displayed.
					insPay=ClaimProcs.ClaimByTotalOnly(ClaimProcList,arrayClaim[tempCountClaim].ClaimNum);
					subTotal-=insPay;
					if(arrayClaim[tempCountClaim].ClaimStatus=="R"){//if claim is received
						//tempAcctLine.InsPay=insPay.ToString("F");//show the insurance payment
						//show the insurance payment. Just for display.
						//The byTotal is the only number that affects the balance.
						tempAcctLine.InsPay=Claims.Cur.InsPayAmt.ToString("F");
						//runBal-=insPay;
					}
					else{//claim not received, so it is an estimate
						switch(arrayClaim[tempCountClaim].ClaimStatus){
							case "U":
								tempAcctLine.InsPay="Unsent";
								break;
							case "H":
								tempAcctLine.InsPay="Hold";
								break;
							case "W":
								tempAcctLine.InsPay="In Queue";
								break;
							case "P":
								tempAcctLine.InsPay="In Queue";
								break;
							case "S":
								tempAcctLine.InsPay="Sent";
								break;
						}
						//subTotal-=insEst;
						//FamInsEst+=insEst;//for printing family
					}
					//runBal-=arrayClaim[tempCountClaim].WriteOff;
					if(arrayClaim[tempCountClaim].WriteOff>0){
						tempAcctLine.Adj="-"+arrayClaim[tempCountClaim].WriteOff.ToString("F");
						subTotal-=arrayClaim[tempCountClaim].WriteOff;
					}
					tempAcctLine.Patient="";
					if(arrayClaim[tempCountClaim].DateService >= fromDate
						&& arrayClaim[tempCountClaim].DateService <= toDate){//within date range
						runBal+=subTotal;
						tempAcctLine.Balance=runBal.ToString("F");
						AcctLineAL.Add(tempAcctLine);
					}
					else if(!subtotalsOnly){//out of date range, but show normal totals
						runBal+=subTotal;//add to the running balance, but do not display it.
					}
					//old claims that have been received only recently, or not at all:
					else if(includeClaims && arrayClaim[tempCountClaim].ClaimStatus != "R"){
						tempAcctLine.Balance="";//don't show running balance
						AcctLineAL.Add(tempAcctLine);
					}
					if(tempCountClaim<countClaim) tempCountClaim++;
				}//end Claim
				//3. Adjustment
				else if(tempCountAdj<countAdj && DateTime.Compare(arrayAdj[tempCountAdj].AdjDate,lineDate)==0){
					tempAcctLine=new AcctLine();
					tempAcctLine.Type=AcctType.Adj;
					tempAcctLine.Index=tempCountAdj;
					tempAcctLine.Date=arrayAdj[tempCountAdj].AdjDate.ToString("d");
					tempAcctLine.Provider=Providers.GetAbbr(arrayAdj[tempCountAdj].ProvNum);
					tempAcctLine.Code="Adjust";
					tempAcctLine.Tooth="";
					tempAcctLine.Description=Defs.GetName(DefCat.AdjTypes,arrayAdj[tempCountAdj].AdjType);
					tempAcctLine.Fee="";
					tempAcctLine.InsEst="";
					tempAcctLine.InsPay="";
					//can be a positive or negative number:
					tempAcctLine.Adj=arrayAdj[tempCountAdj].AdjAmt.ToString("F");
					if(arrayAdj[tempCountAdj].AdjDate >= fromDate
						&& arrayAdj[tempCountAdj].AdjDate <= toDate){
						runBal+=arrayAdj[tempCountAdj].AdjAmt;
						tempAcctLine.Balance=runBal.ToString("F");
						AcctLineAL.Add(tempAcctLine);
					}
					else if(!subtotalsOnly){//out of date range, but show normal totals
						runBal+=arrayAdj[tempCountAdj].AdjAmt;//add to the running balance, but do not display it.
					}
					if(tempCountAdj<countAdj) tempCountAdj++;
				}//end Adjustment
				//4. Payment:
				else if(tempCountPay<countPay && DateTime.Compare(arrayPay[tempCountPay].Date,lineDate)==0){
					tempAcctLine=new AcctLine();
					tempAcctLine.Type=AcctType.Pay;
					tempAcctLine.Code="Pay";
					tempAcctLine.Description=arrayPay[tempCountPay].Description;
						//Payments.GetInfo(arrayPay[tempCountPay].PayNum);
					tempAcctLine.Index=tempCountPay;
					tempAcctLine.Date=arrayPay[tempCountPay].Date.ToString("d");
					tempAcctLine.Provider="";//since payments are usually split, leave empty
					tempAcctLine.Tooth="";
					tempAcctLine.Fee="";
					tempAcctLine.InsEst="";
					tempAcctLine.InsPay="";
					tempAcctLine.Patient="";
					tempAcctLine.Adj="";
					if(arrayPay[tempCountPay].Amount!=0){
						tempAcctLine.Paid=(-arrayPay[tempCountPay].Amount).ToString("F");
					}
					if(arrayPay[tempCountPay].Date >= fromDate
						&& arrayPay[tempCountPay].Date <= toDate){
						runBal-=arrayPay[tempCountPay].Amount;
						tempAcctLine.Balance=runBal.ToString("F");
						AcctLineAL.Add(tempAcctLine);
					}
					else if(!subtotalsOnly){//out of date range, but show normal totals
						runBal-=arrayPay[tempCountPay].Amount;//add to the running balance, but do not display it.
					}
					if(tempCountPay<countPay) tempCountPay++;
				}//end Payment
				//5. Comm:
				else if(tempCountComm<countComm 
					&& arrayComm[tempCountComm].CommDateTime.Date==lineDate)
				{
					tempAcctLine=new AcctLine();
					tempAcctLine.Type=AcctType.Comm;
					tempAcctLine.Code="Comm";
					tempAcctLine.Description="Sent Statement";
					tempAcctLine.Index=tempCountComm;
					tempAcctLine.Date=arrayComm[tempCountComm].CommDateTime.ToShortDateString();
					tempAcctLine.Provider="";
					tempAcctLine.Tooth="";
					tempAcctLine.Fee="";
					tempAcctLine.InsEst="";
					tempAcctLine.InsPay="";
					tempAcctLine.Patient="";
					//adj
					//paid
					tempAcctLine.Balance="";
					if(arrayComm[tempCountComm].CommDateTime.Date >= fromDate
						&& arrayComm[tempCountComm].CommDateTime.Date <= toDate){
						AcctLineAL.Add(tempAcctLine);
					}
					if(tempCountComm<countComm) tempCountComm++;
				}//end Comm
				//6. PayPlan:
				else if(tempCountPayPlan<countPayPlan 
					&& DateTime.Compare(arrayPayPlan[tempCountPayPlan].PayPlanDate,lineDate)==0)
				{
					tempAcctLine=new AcctLine();
					tempAcctLine.Type=AcctType.PayPlan;
					tempAcctLine.Code="PayPlan";
					tempAcctLine.Description="Payment Plan";
					tempAcctLine.Index=tempCountPayPlan;
					tempAcctLine.Date=arrayPayPlan[tempCountPayPlan].PayPlanDate.ToShortDateString();
					tempAcctLine.Provider="";
					tempAcctLine.Tooth="";
					tempAcctLine.Fee=arrayPayPlan[tempCountPayPlan].TotalAmount.ToString("F");
					tempAcctLine.InsEst="";
					tempAcctLine.InsPay="";
					double subTotal=0;
					//either or both of these conditions might be true (3 possible scenarios)
					//1.If this is guarantor
					if(arrayPayPlan[tempCountPayPlan].Guarantor==PatCur.PatNum){
						tempAcctLine.Patient=arrayPayPlan[tempCountPayPlan].CurrentDue.ToString("F");
						//runBal+=arrayPayPlan[tempCountPayPlan].CurrentDue;
						subTotal+=arrayPayPlan[tempCountPayPlan].CurrentDue;
					}
					//2.If this is the patient
					if(arrayPayPlan[tempCountPayPlan].PatNum==PatCur.PatNum){
						//runBal-=arrayPayPlan[tempCountPayPlan].TotalAmount;
						subTotal-=arrayPayPlan[tempCountPayPlan].TotalAmount;
					}
					
					if(arrayPayPlan[tempCountPayPlan].PayPlanDate >= fromDate
						&& arrayPayPlan[tempCountPayPlan].PayPlanDate <= toDate)
						//|| arrayPayPlan[tempCountPayPlan].CurrentDue>0)
					{
						runBal+=subTotal;
						tempAcctLine.Balance=runBal.ToString("F");
						AcctLineAL.Add(tempAcctLine);
					}
					else if(!subtotalsOnly){//out of date range, but show normal totals
						runBal+=subTotal;//add to the running balance, but do not display it.
					}
					if(tempCountPayPlan<countPayPlan) tempCountPayPlan++;
				}//end PayPlan
			}//end for line
			SubTotal=runBal;
			//for (int i=0;i<countProc;i++){
			//}//end for i countProc
		}//end FillAcctLineAL

		private void FilltbAccount(){
			DateTime lineDate=DateTime.MinValue;
			tbAccount.ResetRows(AcctLineAL.Count);
			for(int i=0;i<AcctLineAL.Count;i++){
				//if (AcctLineAL[i].IsProc==true){
				tbAccount.SetNormRow(i);
				try{//error catch bad dates
					if(DateTime.Compare(DateTime.Parse(((AcctLine)AcctLineAL[i]).Date),lineDate)>0){
						tbAccount.Cell[0,i]=((AcctLine)AcctLineAL[i]).Date;
						tbAccount.SetTopRow(i);
						lineDate=DateTime.Parse(((AcctLine)AcctLineAL[i]).Date);
					}
					else tbAccount.Cell[0,i]="";
					}
				catch{
					tbAccount.Cell[0,i]="";
				}
				tbAccount.Cell[1,i]=((AcctLine)AcctLineAL[i]).Provider;
				tbAccount.Cell[2,i]=((AcctLine)AcctLineAL[i]).Code;
				tbAccount.Cell[3,i]=((AcctLine)AcctLineAL[i]).Tooth;
				tbAccount.Cell[4,i]=((AcctLine)AcctLineAL[i]).Description;
				Debug.WriteLine(tbAccount.Cell[4,i]);
				tbAccount.Cell[5,i]=((AcctLine)AcctLineAL[i]).Fee;
				tbAccount.Cell[6,i]=((AcctLine)AcctLineAL[i]).InsEst;
				tbAccount.Cell[7,i]=((AcctLine)AcctLineAL[i]).InsPay;
				tbAccount.Cell[8,i]=((AcctLine)AcctLineAL[i]).Patient;
				tbAccount.Cell[9,i]=((AcctLine)AcctLineAL[i]).Adj;
				tbAccount.Cell[10,i]=((AcctLine)AcctLineAL[i]).Paid;
				tbAccount.Cell[11,i]=((AcctLine)AcctLineAL[i]).Balance;
				//}//end if
				switch(((AcctLine)AcctLineAL[i]).Type){
					default:
						tbAccount.SetTextColorRow
							(i,Defs.Long[(int)DefCat.AccountColors][0].ItemColor);
						break;
					case AcctType.Claim:
						tbAccount.SetTextColorRow
							(i,Defs.Long[(int)DefCat.AccountColors][4].ItemColor);
						break;
					case AcctType.Adj:
						tbAccount.SetTextColorRow
							(i,Defs.Long[(int)DefCat.AccountColors][1].ItemColor);
						break;
					//case AcctType.Disc:
					//	tbAccount.SetTextColorRow
					//		(i,Defs.Long[(int)DefCat.AccountColors][2].ItemColor);
					//	break;
					case AcctType.Pay:
						tbAccount.SetTextColorRow
							(i,Defs.Long[(int)DefCat.AccountColors][3].ItemColor);
						break;
					case AcctType.Comm:
						tbAccount.SetTextColorRow
							(i,Defs.Long[(int)DefCat.AccountColors][5].ItemColor);
						break;
					case AcctType.PayPlan:
						tbAccount.SetTextColorRow
							(i,Defs.Long[(int)DefCat.AccountColors][6].ItemColor);
						break;
				}
				//else{//else note
				//	if(AcctLineAL[i-1].IsProc==true)
				//		tbAccount.SetFirstNoteRow(i);
				//	else
				//		tbAccount.SetNoteRow(i);
				//	tbAccount.Cell[2,i]=AcctLineAL[i].Note;
				//}//end else note
			}//end for
			tbAccount.LayoutTables();
		}//end FilltbAccount

		private void tbAccount_CellClicked(object sender, CellEventArgs e){
			//this seems to fire after a doubleclick, so this prevents error:
			if(e.Row>=AcctLineAL.Count){
				return;
			}
			if(ViewingInRecall) return;
			switch (((AcctLine)AcctLineAL[e.Row]).Type){
				default://procedure
					break;
				case AcctType.Claim:
					Claims.Cur=arrayClaim[((AcctLine)AcctLineAL[e.Row]).Index];
					for(int i=0;i<AcctLineAL.Count;i++){//loop through all rows
						if(((AcctLine)AcctLineAL[i]).Type!=AcctType.Proc)
							//if not a procedure, then skip
							continue;
						for(int j=0;j<ClaimProcList.Length;j++){//loop through all claimprocs
							//if there is a claim proc for this procedure that matches
							if(arrayProc[((AcctLine)AcctLineAL[i]).Index].ProcNum==ClaimProcList[j].ProcNum
								&& ClaimProcList[j].ClaimNum==Claims.Cur.ClaimNum)
							{
								tbAccount.SetSelected(i,true);
							}
						}
					}
					break;
				case AcctType.Adj:
					//Adjustments.Cur=arrayAdj[((AcctLine)AcctLineAL[e.Row]).Index];
					break;
				//case AcctType.Disc:
					//Payments.SetCur(arrayPay[((AcctLine)AcctLineAL[e.Row]).Index].PayNum);
				//	break;
				case AcctType.Pay:
					PaySplit[] PaySplitList=PaySplits.Refresh(PatCur.PatNum);
					for(int i=0;i<AcctLineAL.Count;i++){//loop through all rows
						if(((AcctLine)AcctLineAL[i]).Type!=AcctType.Proc){
							continue;//if not a procedure, then skip
						}
						for(int j=0;j<PaySplitList.Length;j++){//loop through all paysplits
							//if there is a paysplit for this procedure that matches
							if(arrayProc[((AcctLine)AcctLineAL[i]).Index].ProcNum==PaySplitList[j].ProcNum
								&& PaySplitList[j].PayNum==arrayPay[((AcctLine)AcctLineAL[e.Row]).Index].PayNum)
							{
								tbAccount.SetSelected(i,true);
							}
						}
					}
					break;
				case AcctType.Comm:
					//Commlogs.Cur=arrayComm[((AcctLine)AcctLineAL[e.Row]).Index];
					break;				
				case AcctType.PayPlan:
					PayPlans.Cur=arrayPayPlan[((AcctLine)AcctLineAL[e.Row]).Index];
					//ArrayList payPlanRows=new ArrayList();
					for(int i=0;i<AcctLineAL.Count;i++){
						if(((AcctLine)AcctLineAL[i]).Type==AcctType.Pay
							&& arrayPay[((AcctLine)AcctLineAL[i]).Index].PayPlanNum==PayPlans.Cur.PayPlanNum)
						{
							tbAccount.SetSelected(i,true);
						}
					}
					break;	
			}//end switch
		}

		private void tbAccount_CellDoubleClicked(object sender, CellEventArgs e){
			if(ViewingInRecall) return;
			switch (((AcctLine)AcctLineAL[e.Row]).Type){
				default:
					Procedure ProcCur=arrayProc[((AcctLine)AcctLineAL[e.Row]).Index];
					FormProcEdit FormPE=new FormProcEdit(ProcCur,PatCur,FamCur,PlanList);
					FormPE.ShowDialog();
					break;
				case AcctType.Claim:
					Claims.Cur=arrayClaim[((AcctLine)AcctLineAL[e.Row]).Index];
					FormClaimEdit FormClaimEdit2=new FormClaimEdit(PatCur,FamCur);
					FormClaimEdit2.IsNew=false;
					FormClaimEdit2.ShowDialog();
					break;
				case AcctType.Adj:
					Adjustment AdjustmentCur=arrayAdj[((AcctLine)AcctLineAL[e.Row]).Index];
					FormAdjust FormAdj=new FormAdjust(PatCur,AdjustmentCur);
					FormAdj.ShowDialog();
					break;
				case AcctType.Pay:
					Payment PaymentCur=Payments.GetPayment(arrayPay[((AcctLine)AcctLineAL[e.Row]).Index].PayNum);
					FormPayment2=new FormPayment(PatCur,FamCur,PaymentCur);
					FormPayment2.IsNew=false;
					FormPayment2.ShowDialog();
					break;
				case AcctType.Comm:
					Commlogs.Cur=arrayComm[((AcctLine)AcctLineAL[e.Row]).Index];
					FormCommItem2=new FormCommItem();
					FormCommItem2.IsNew=false;
					FormCommItem2.ShowDialog();
					break;				
				case AcctType.PayPlan:
					PayPlans.Cur=arrayPayPlan[((AcctLine)AcctLineAL[e.Row]).Index];
					FormPayPlan2=new FormPayPlan(PatCur);
					FormPayPlan2.IsNew=false;
					FormPayPlan2.ShowDialog();
					if(FormPayPlan2.GotoPatNum!=0){
						//Patient PatCur=PatCur;
						
						PatCur.PatNum=FormPayPlan2.GotoPatNum;//switches to other patient.
						//Patients.Cur=PatCur;
					}
					break;	
			}//end switch
			//Shared.ComputeBalances();//use whenever a change would affect the total
			ModuleSelected(PatCur.PatNum);
		}

		private void tbAcctPat_CellClicked(object sender, CellEventArgs e){
			if(ViewingInRecall) return;
			tbAcctPat.ColorRow(tbAcctPat.SelectedRow,Color.White);
			tbAcctPat.SelectedRow=e.Row;
			tbAcctPat.ColorRow(e.Row,Color.DarkSalmon);
			OnPatientSelected(FamCur.List[e.Row].PatNum);
			ModuleSelected(FamCur.List[e.Row].PatNum);
		}		

		private void ToolBarMain_ButtonClick(object sender, OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			if(e.Button.Tag.GetType()==typeof(string)){
				//standard predefined button
				switch(e.Button.Tag.ToString()){
					case "Patient":
						OnPat_Click();
						break;
					case "Payment":
						OnPay_Click();
						break;
					case "Adjustment":
						OnAdj_Click();
						break;
					case "Insurance":
						OnIns_Click();
						break;
					case "PayPlan":
						OnPayPlan_Click();
						break;
					case "Statement":
						OnStatement_Click();
						break;
				}
			}
			else if(e.Button.Tag.GetType()==typeof(int)){
				Programs.Execute((int)e.Button.Tag,PatCur);
			}
		}

		private void OnPat_Click(){
			FormPatientSelect FormPS=new FormPatientSelect();
			FormPS.ShowDialog();
			if(FormPS.DialogResult==DialogResult.OK){
				OnPatientSelected(FormPS.SelectedPatNum);
				ModuleSelected(FormPS.SelectedPatNum);
			}
		}

		///<summary></summary>
		private void OnPatientSelected(int patNum){
			PatientSelectedEventArgs eArgs=new OpenDental.PatientSelectedEventArgs(patNum);
			if(PatientSelected!=null)
				PatientSelected(this,eArgs);
		}

		private void OnPay_Click() {
			Payment PaymentCur=new Payment();
			PaymentCur.PayDate=DateTime.Now;
			PaymentCur.PatNum=PatCur.PatNum;
			PaymentCur.ClinicNum=PatCur.ClinicNum;
			PaymentCur.Insert();
			FormPayment FormPayment2=new FormPayment(PatCur,FamCur,PaymentCur);
			FormPayment2.IsNew=true;
			FormPayment2.ShowDialog();
			//Shared.ComputeBalances();
			ModuleSelected(PatCur.PatNum);
		}

		private void OnAdj_Click() {
			Adjustment AdjustmentCur=new Adjustment();
			AdjustmentCur.AdjDate=DateTime.Today;
			AdjustmentCur.ProcDate=DateTime.Today;
			AdjustmentCur.ProvNum=PatCur.PriProv;
			AdjustmentCur.PatNum=PatCur.PatNum;
			FormAdjust FormAdjust2=new FormAdjust(PatCur,AdjustmentCur);
			FormAdjust2.IsNew=true;
			FormAdjust2.ShowDialog();
			//Shared.ComputeBalances();
			ModuleSelected(PatCur.PatNum);
		}

		private void OnIns_Click() {
			if(PatCur.PriPlanNum==0){
				MessageBox.Show(Lan.g(this,"Patient does not have insurance."));
				return;
			}
			if(tbAccount.SelectedIndices.Length==0){
				//autoselect procedures
				for(int i=0;i<AcctLineAL.Count;i++){//loop through every line showing on screen
					if(((AcctLine)AcctLineAL[i]).Type!=AcctType.Proc){
						continue;//ignore non-procedures
					}
					if(arrayProc[((AcctLine)AcctLineAL[i]).Index].ProcFee==0){
						continue;//ignore zero fee procedures, but user can explicitly select them
					}
					if(arrayProc[((AcctLine)AcctLineAL[i]).Index].NeedsSent(ClaimProcList,PatCur.PriPlanNum)){
						tbAccount.SetSelected(i,true);
					}
				}
				if(tbAccount.SelectedIndices.Length==0){//if still none selected
					MessageBox.Show(Lan.g(this,"Please select procedures first."));
					return;
				}
			}
			bool allAreProcedures=true;
			for(int i=0;i<tbAccount.SelectedIndices.Length;i++){
				if(((AcctLine)AcctLineAL[tbAccount.SelectedIndices[i]]).Type!=AcctType.Proc){
					allAreProcedures=false;
				}
			}
			if(!allAreProcedures){
				MessageBox.Show(Lan.g(this,"You can only select procedures."));
				return;
			}
			//no longer tests for noBillIns here
			//no longer tests for ProcIsAttachedToClaim here
			//spk had made an alteration to disallow option to create a second claim on same procs. 
			Claim ClaimCur=CreateClaim("P");
			if(ClaimCur.ClaimNum==0){
				ModuleSelected(PatCur.PatNum);
				return;
			}
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			//ClaimProc[] ClaimProcsForClaim=ClaimProcs.GetForClaim(ClaimProcList,ClaimCur.ClaimNum);
			ClaimCur.ClaimStatus="W";
			ClaimCur.DateSent=DateTime.Today;
			Claims.CalculateAndUpdate(ClaimProcList,ProcList,PatCur,PlanList,ClaimCur);
			Claims.Cur=ClaimCur;
			FormClaimEdit FormCE=new FormClaimEdit(PatCur,FamCur);
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			if(FormCE.DialogResult!=DialogResult.OK){
				ModuleSelected(PatCur.PatNum);
				return;//will have already been deleted
			}
			//Claim priClaim=Claims.Cur;//for use a few lines down to display dialog
			if(PatCur.SecPlanNum>0){
				ClaimCur=CreateClaim("S");
				if(ClaimCur.ClaimNum==0){
					ModuleSelected(PatCur.PatNum);
					return;
				}
				ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
				ClaimCur.ClaimStatus="H";
				Claims.CalculateAndUpdate(ClaimProcList,ProcList,PatCur,PlanList,ClaimCur);
				//Claims.Cur=ClaimCur;
			}
			ModuleSelected(PatCur.PatNum);
		}

		///<summary>The only validation that's been done is just to make sure that only procedures are selected.  All validation on the procedures selected is done here.  Creates and saves claim initially, attaching all selected procedures.  But it does not refresh any data. Does not do a final update of the new claim.  Does not enter fee amounts.  claimType=P,S,or Other</summary>
		private Claim CreateClaim(string claimType){
			InsPlan PlanCur=new InsPlan();
			Relat RelatOther=Relat.Self;
			switch(claimType){
				case "P":
					PlanCur=InsPlans.GetPlan(PatCur.PriPlanNum,PlanList);
					break;
				case "S":
					PlanCur=InsPlans.GetPlan(PatCur.SecPlanNum,PlanList);
					break;
				case "Other":
					FormInsPlanSelect FormIPS=new FormInsPlanSelect(PatCur.PatNum);
					FormIPS.ViewRelat=true;
					FormIPS.ShowDialog();
					if(FormIPS.DialogResult!=DialogResult.OK){
						return new Claim();
					}
					PlanCur=FormIPS.SelectedPlan;
					RelatOther=FormIPS.PatRelat;
					break;
			}
			for(int i=0;i<tbAccount.SelectedIndices.Length;i++){
				if(arrayProc[((AcctLine)AcctLineAL[tbAccount.SelectedIndices[i]]).Index]
					.NoBillIns(ClaimProcList,PlanCur.PlanNum))
				{
					MessageBox.Show(Lan.g(this,"Not allowed to send procedures to insurance that are marked 'Do not bill to ins'."));
					return new Claim();
				}
			}
			for(int i=0;i<tbAccount.SelectedIndices.Length;i++){
				if(arrayProc[((AcctLine)AcctLineAL[tbAccount.SelectedIndices[i]]).Index]
					.IsAlreadyAttachedToClaim(ClaimProcList,PlanCur.PlanNum))
				{
					MessageBox.Show(Lan.g(this,"Not allowed to send a procedure to the same insurance company twice."));
					return new Claim();
				}
			}
			ClaimProc[] claimProcs=new ClaimProc[tbAccount.SelectedIndices.Length];
			for(int i=0;i<tbAccount.SelectedIndices.Length;i++){//loop through selected procs
				//and try to find an estimate that can be used
				claimProcs[i]=arrayProc[((AcctLine)AcctLineAL[tbAccount.SelectedIndices[i]]).Index]
					.GetClaimProcEstimate(ClaimProcList,PlanCur);
			}
			for(int i=0;i<claimProcs.Length;i++){//loop through each claimProc
				//and create any missing estimates. This handles claims to 3rd and 4th ins co's.
				if(claimProcs[i]==null){
					claimProcs[i]=new ClaimProc();
					claimProcs[i].CreateEst(arrayProc[((AcctLine)AcctLineAL[tbAccount.SelectedIndices[i]])
						.Index],PlanCur);
				}
			}
			//now, all claimProcs have a valid value
			//for any CapComplete, need to make a copy so that original doesn't get attached.
			for(int i=0;i<claimProcs.Length;i++){
				if(claimProcs[i].Status==ClaimProcStatus.CapComplete){
					claimProcs[i]=claimProcs[i].Copy();
					//status will get changed down below
					claimProcs[i].Insert();//this makes a duplicate in db with different claimProcNum
				}
			}
			Claims.Cur=new Claim();
			Claims.InsertCur();//to retreive a key for new Claim.ClaimNum
			Claim ClaimCur=Claims.Cur;
			ClaimCur.PatNum=PatCur.PatNum;
			ClaimCur.DateService=claimProcs[claimProcs.Length-1].ProcDate;
			//datesent
			ClaimCur.ClaimStatus="U";
			//datereceived
			switch(claimType){
				case "P":
					ClaimCur.PlanNum=PatCur.PriPlanNum;
					ClaimCur.PatRelat=PatCur.PriRelationship;
					ClaimCur.ClaimType="P";
					ClaimCur.PlanNum2=PatCur.SecPlanNum;//might be 0 if no sec ins
					ClaimCur.PatRelat2=PatCur.SecRelationship;
					break;
				case "S":
					ClaimCur.PlanNum=PatCur.SecPlanNum;
					ClaimCur.PatRelat=PatCur.SecRelationship;
					ClaimCur.ClaimType="S";
					ClaimCur.PlanNum2=PatCur.PriPlanNum;
					ClaimCur.PatRelat2=PatCur.PriRelationship;
					break;
				case "Other":
					//FormInsPlanSelect FormIPS=new FormInsPlanSelect();
					//FormIPS.ViewRelat=true;
					//FormIPS.ShowDialog();
					//if(FormIPS.DialogResult!=DialogResult.OK){
					//	Claims.Cur=ClaimCur;
					//	Claims.DeleteCur();
					//	return new Claim();
					//}
					//PlanCur=FormIPS.SelectedPlan;
					ClaimCur.PlanNum=PlanCur.PlanNum;
					ClaimCur.PatRelat=RelatOther;
					ClaimCur.ClaimType="Other";
					//plannum2 is not automatically filled in.
					break;
			}
			//InsPlans.GetCur(ClaimCur.PlanNum);
			if(PlanCur.PlanType=="c"){//if capitation
				ClaimCur.ClaimType="Cap";
			}
			ClaimCur.ProvTreat=arrayProc[((AcctLine)AcctLineAL[tbAccount.SelectedIndices[0]]).Index].ProvNum;
			for(int i=0;i<tbAccount.SelectedIndices.Length;i++){
				if(!Providers.GetIsSec//if not a hygeinist
					(arrayProc[((AcctLine)AcctLineAL[tbAccount.SelectedIndices[i]]).Index].ProvNum))
				{
					ClaimCur.ProvTreat
						=arrayProc[((AcctLine)AcctLineAL[tbAccount.SelectedIndices[i]]).Index].ProvNum;
				}
			}
			if(Providers.GetIsSec(ClaimCur.ProvTreat)){
				ClaimCur.ProvTreat=PatCur.PriProv;
				//OK if 0, because auto select first in list when open claim
			}
			//claimfee calcs in ClaimEdit
			//inspayest ''
			//inspayamt
			//ClaimCur.DedApplied=0;//calcs in ClaimEdit.
			//preauthstring, etc, etc
			ClaimCur.IsProsthesis="N";
			if(Prefs.GetInt("InsBillingProv")==0){//this can later be extended to include a 3rd option
				//default=0
				ClaimCur.ProvBill=Prefs.GetInt("PracticeDefaultProv");
			}
			else{
				//treat=1
				ClaimCur.ProvBill=Claims.Cur.ProvTreat;//OK if zero, because it will get fixed in claim
			}
			ClaimCur.EmployRelated=YN.No;
			//attach procedures
			Procedure ProcCur;
			//for(int i=0;i<tbAccount.SelectedIndices.Length;i++){
			for(int i=0;i<claimProcs.Length;i++){
				ProcCur=arrayProc[((AcctLine)AcctLineAL[tbAccount.SelectedIndices[i]]).Index];
				//ClaimProc ClaimProcCur=new ClaimProc();
				//ClaimProcCur.ProcNum=ProcCur.ProcNum;
				claimProcs[i].ClaimNum=ClaimCur.ClaimNum;
				//ClaimProcCur.PatNum=Patients.Cur.PatNum;
				//ClaimProcCur.ProvNum=ProcCur.ProvNum;
				//ClaimProcs.Cur.FeeBilled=;//handle in call to FormClaimEdit.CalculateEstimates
				//inspayest ''
				//dedapplied ''
				if(PlanCur.PlanType=="c")//if capitation
					claimProcs[i].Status=ClaimProcStatus.CapClaim;
				else
					claimProcs[i].Status=ClaimProcStatus.NotReceived;
				//inspayamt=0
				//remarks
				//claimpaymentnum=0
				//ClaimProcCur.PlanNum=Claims.Cur.PlanNum;
				//ClaimProcCur.DateCP=ProcCur.ProcDate;
				//writeoff
				if(PlanCur.UseAltCode 
					&& ((ProcedureCode)ProcedureCodes.HList[ProcCur.ADACode]).AlternateCode1!="")
				{
					claimProcs[i].CodeSent
						=((ProcedureCode)ProcedureCodes.HList[ProcCur.ADACode]).AlternateCode1;
				}
				else{
					claimProcs[i].CodeSent=ProcCur.ADACode;
					if(claimProcs[i].CodeSent.Length>5 && claimProcs[i].CodeSent.Substring(0,1)=="D"){
						claimProcs[i].CodeSent=claimProcs[i].CodeSent.Substring(0,5);
					}
				}
				claimProcs[i].Update();
			}//for proc
			return ClaimCur;
		}

		private void menuInsPri_Click(object sender, System.EventArgs e) {
			if(PatCur.PriPlanNum==0){
				MessageBox.Show(Lan.g(this,"Patient does not have insurance."));
				return;
			}
			if(tbAccount.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select procedures first."));
				return;
			}
			bool allAreProcedures=true;
			for(int i=0;i<tbAccount.SelectedIndices.Length;i++){
				if(((AcctLine)AcctLineAL[tbAccount.SelectedIndices[i]]).Type!=AcctType.Proc){
					allAreProcedures=false;
				}
			}
			if(!allAreProcedures){
				MessageBox.Show(Lan.g(this,"You can only select procedures."));
				return;
			}
			Claim ClaimCur=CreateClaim("P");
			if(ClaimCur.ClaimNum==0){
				ModuleSelected(PatCur.PatNum);
				return;
			}
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			//ClaimProc[] ClaimProcsForClaim=ClaimProcs.GetForClaim(ClaimProcList,ClaimCur.ClaimNum);
			ClaimCur.ClaimStatus="W";
			ClaimCur.DateSent=DateTime.Today;
			Claims.Cur=ClaimCur;
			//still have not saved some changes to the claim at this point
			FormClaimEdit FormCE=new FormClaimEdit(PatCur,FamCur);
			Claims.CalculateAndUpdate(ClaimProcList,ProcList,PatCur,PlanList,Claims.Cur);
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void menuInsSec_Click(object sender, System.EventArgs e) {
			if(PatCur.PriPlanNum==0 || PatCur.SecPlanNum==0){
				MessageBox.Show(Lan.g(this,"Patient does not have insurance."));
				return;
			}
			if(tbAccount.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select procedures first."));
				return;
			}
			bool allAreProcedures=true;
			for(int i=0;i<tbAccount.SelectedIndices.Length;i++){
				if(((AcctLine)AcctLineAL[tbAccount.SelectedIndices[i]]).Type!=AcctType.Proc){
					allAreProcedures=false;
				}
			}
			if(!allAreProcedures){
				MessageBox.Show(Lan.g(this,"You can only select procedures."));
				return;
			}
			Claim ClaimCur=CreateClaim("S");
			if(ClaimCur.ClaimNum==0){
				ModuleSelected(PatCur.PatNum);
				return;
			}
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			//ClaimProc[] ClaimProcsForClaim=ClaimProcs.GetForClaim(ClaimProcList,ClaimCur.ClaimNum);
			ClaimCur.ClaimStatus="W";
			ClaimCur.DateSent=DateTime.Today;
			Claims.CalculateAndUpdate(ClaimProcList,ProcList,PatCur,PlanList,ClaimCur);
			Claims.Cur=ClaimCur;
			FormClaimEdit FormCE=new FormClaimEdit(PatCur,FamCur);
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void menuInsOther_Click(object sender, System.EventArgs e) {
			if(tbAccount.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select procedures first."));
				return;
			}
			bool allAreProcedures=true;
			for(int i=0;i<tbAccount.SelectedIndices.Length;i++){
				if(((AcctLine)AcctLineAL[tbAccount.SelectedIndices[i]]).Type!=AcctType.Proc){
					allAreProcedures=false;
				}
			}
			if(!allAreProcedures){
				MessageBox.Show(Lan.g(this,"You can only select procedures."));
				return;
			}
			Claim ClaimCur=CreateClaim("Other");
			if(ClaimCur.ClaimNum==0){
				ModuleSelected(PatCur.PatNum);
				return;
			}
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			//ClaimProc[] ClaimProcsForClaim=ClaimProcs.GetForClaim(ClaimProcList,ClaimCur.ClaimNum);
			Claims.CalculateAndUpdate(ClaimProcList,ProcList,PatCur,PlanList,ClaimCur);
			Claims.Cur=ClaimCur;
			//still have not saved some changes to the claim at this point
			FormClaimEdit FormCE=new FormClaimEdit(PatCur,FamCur);
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void OnPayPlan_Click() {
			PayPlans.Cur=new PayPlan();
			PayPlans.Cur.PatNum=PatCur.PatNum;
			PayPlans.Cur.Guarantor=PatCur.Guarantor;
			PayPlans.Cur.PayPlanDate=DateTime.Today;
			PayPlans.Cur.DateFirstPay=DateTime.Today;
			PayPlans.Cur.TotalAmount=PatCur.EstBalance;
			FormPayPlan FormPP=new FormPayPlan(PatCur);
			FormPP.IsNew=true;
			FormPP.ShowDialog();
			if(FormPP.GotoPatNum!=0){
				ModuleSelected(FormPP.GotoPatNum);//switches to other patient.
			}
			else{
				ModuleSelected(PatCur.PatNum);
			}
		}

		private void OnStatement_Click() {
			int[] patNums=new int[FamCur.List.Length];
			for(int i=0;i<FamCur.List.Length;i++){
				patNums[i]=FamCur.List[i].PatNum;
			}
			DateTime fromDate;
			if(checkShowAll.Checked){
				fromDate=DateTime.MinValue;
			}
			else{
				fromDate=DateTime.Today.AddDays(-45);
			}
			PrintStatement(patNums,fromDate,DateTime.MaxValue,true,false,false,false,"");
			ModuleSelected(PatCur.PatNum);
		}
		
		private void menuItemStatementWalkout_Click(object sender, System.EventArgs e) {
			PrintStatement(new int[] {PatCur.PatNum},DateTime.Today,DateTime.Today,false,false,true,true,"");
			ModuleSelected(PatCur.PatNum);
		}

		private void menuItemStatementMore_Click(object sender, System.EventArgs e) {
			FormStatementOptions FormSO=new FormStatementOptions(PatCur,FamCur);
			if(checkShowAll.Checked){
				FormSO.FromDate=DateTime.MinValue;
			}
			else{
				FormSO.FromDate=DateTime.Today.AddDays(-45);
			}
			FormSO.ToDate=DateTime.MaxValue;
			FormSO.ShowDialog();
			if(FormSO.DialogResult!=DialogResult.OK){
				return;
			}
			//FillMain(FormSO.FromDate,FormSO.ToDate,FormSO.IncludeClaims,FormSO.SubtotalsOnly);
			PrintStatement(FormSO.PatNums,FormSO.FromDate,FormSO.ToDate,FormSO.IncludeClaims
				,FormSO.SubtotalsOnly,FormSO.HidePayment,FormSO.NextAppt,FormSO.Note);
			ModuleSelected(PatCur.PatNum);
		}

		private void textUrgFinNote_TextChanged(object sender, System.EventArgs e) {
			UrgFinNoteChanged=true;
		}

		private void textFinNotes_TextChanged(object sender, System.EventArgs e) {
			FinNoteChanged=true;
		}

		private void textUrgFinNote_Leave(object sender, System.EventArgs e) {
			//need to skip this if selecting another module. Handled in ModuleUnselected due to click event
			if(FamCur==null)
				return;
			if(UrgFinNoteChanged){
				Patient PatOld=FamCur.List[0].Copy();
				FamCur.List[0].FamFinUrgNote=textUrgFinNote.Text;
				FamCur.List[0].Update(PatOld);
				UrgFinNoteChanged=false;
			}
			ModuleSelected(PatCur.PatNum);
		}

		private void textFinNotes_Leave(object sender, System.EventArgs e) {
			if(FamCur==null)
				return;
			if(FinNoteChanged){
				PatientNotes.Cur.FamFinancial=textFinNotes.Text;
				PatientNotes.UpdateCur(PatCur.Guarantor);
				FinNoteChanged=false;
				ModuleSelected(PatCur.PatNum);
			}
		}

		private void checkShowAll_Click(object sender, System.EventArgs e) {
			RefreshModuleScreen();
		}

		private void panelSplitter_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			MouseIsDownOnSplitter=true;
			SplitterOriginalY=panelSplitter.Top;
			OriginalMousePos=panelSplitter.Top+e.Y;
		}

		private void panelSplitter_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(!MouseIsDownOnSplitter)
				return;
			int splitterNewLoc=SplitterOriginalY+(panelSplitter.Top+e.Y)-OriginalMousePos;
			if(splitterNewLoc<tbAcctPat.Bottom)
				splitterNewLoc=tbAcctPat.Bottom;//keeps it from going too high
			if(splitterNewLoc>Height)
				splitterNewLoc=Height-panelSplitter.Height;//keeps it from going off the bottom edge
			panelSplitter.Top=splitterNewLoc;
		}

		private void panelSplitter_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			MouseIsDownOnSplitter=false;
			tbAccount.LayoutTables();
			tbComm.LayoutTables();
		}

		/// <summary>Prints a single statement.</summary>
		private void PrintStatement(int[] famPatNums,DateTime fromDate,DateTime toDate,bool includeClaims, bool subtotalsOnly,bool hidePayment,bool nextAppt,string note){
			FormRpStatement FormST=new FormRpStatement();
			int[][] patNums=new int[1][];
			patNums[0]=new int[famPatNums.Length];
			for(int i=0;i<famPatNums.Length;i++){
				patNums[0][i]=famPatNums[i];
			}
			FormST.PrintStatements(patNums,fromDate,toDate,includeClaims,subtotalsOnly,hidePayment,nextAppt,note);
			#if DEBUG
				FormST.ShowDialog();
			#endif
		}

		private void butComm_Click(object sender, System.EventArgs e) {
			Commlogs.Cur=new Commlog();
			Commlogs.Cur.PatNum=PatCur.PatNum;
			Commlogs.Cur.CommDateTime=DateTime.Now;
			if(ViewingInRecall)
				Commlogs.Cur.CommType=CommItemType.Recall;
			else
				Commlogs.Cur.CommType=CommItemType.Financial;
			FormCommItem FormCI=new FormCommItem();
			FormCI.IsNew=true;
			FormCI.ShowDialog();
			Commlogs.Refresh(PatCur.PatNum);
			FillComm();
		}

		private void butLetterSimple_Click(object sender, System.EventArgs e) {
			FormLetters FormL=new FormLetters(PatCur);
			FormL.ShowDialog();
			Commlogs.Refresh(PatCur.PatNum);
			FillComm();
		}

		private void butLetterMerge_Click(object sender, System.EventArgs e) {
			FormLetterMerges FormL=new FormLetterMerges(PatCur);
			FormL.ShowDialog();
			Commlogs.Refresh(PatCur.PatNum);
			FillComm();
		}

		private void butLabel_Click(object sender, System.EventArgs e) {
			LabelSingle label=new LabelSingle();
			label.PrintPat(PatCur);
		}

		private void butEmail_Click(object sender, System.EventArgs e) {
			EmailMessages.Cur=new EmailMessage();
			EmailMessages.Cur.PatNum=PatCur.PatNum;
			EmailMessages.Cur.ToAddress=PatCur.Email;
			EmailMessages.Cur.FromAddress=((Pref)Prefs.HList["EmailSenderAddress"]).ValueString;
			FormEmailMessageEdit FormE=new FormEmailMessageEdit(PatCur.PatNum);
			FormE.ShowDialog();
			Commlogs.Refresh(PatCur.PatNum);
			FillComm();
		}

		private void tbComm_CellDoubleClicked(object sender, OpenDental.CellEventArgs e) {
			Commlogs.Cur=Commlogs.List[(int)CommIndices[e.Row]];
			FormCommItem FormCI=new FormCommItem();
			FormCI.ShowDialog();
			Commlogs.Refresh(PatCur.PatNum);
			FillComm();
		}

		

		

		

		

		

		

		/*private void butPendingClaims_Click(object sender, System.EventArgs e) {
			int OldPatNum=0;
			if(Patients.PatIsLoaded){
				OldPatNum=Patients.Cur.PatNum;
			}
			FormClaimsPending FormPending=new FormClaimsPending();
			FormPending.ShowDialog();
			if(Patients.PatIsLoaded){
				Patients.Cur.PatNum=OldPatNum;
				ModuleSelected();
			}
			else{
				Patients.Cur=new Patient();
			}
		}*/

	}//end class

	///<summary>A single line of data in ContrAccount.  Might change this to a class soon.</summary>
	public struct AcctLine{
		///<summary></summary>
		public AcctType Type;
		//public bool IsProc;
		///<summary></summary>
		public int Index;
		///<summary></summary>
		public string Date;
		///<summary></summary>
		public string Provider;
		///<summary></summary>
		public string Code;
		///<summary></summary>
		public string Tooth;
		///<summary></summary>
		public string Description;
		///<summary></summary>
		public string Fee;
		///<summary></summary>
		public string InsEst;
		///<summary></summary>
		public string InsPay;
		///<summary></summary>
		public string Patient;
		///<summary></summary>
		public string Adj;
		///<summary></summary>
		public string Paid;
		///<summary></summary>
		public string Balance;
		///<summary>only used for statements</summary>
		public string StateType;
	}

	///<summary>Account line type used when displaying lines in the Account module.</summary>
	public enum AcctType{
		///<summary>1</summary>
		Proc=1,
		///<summary>2</summary>
		Adj,
		///<summary>3</summary>
		Pay,
		///<summary>4</summary>
		Claim,
		///<summary>5</summary>
		Comm,
		///<summary>6</summary>
		PayPlan}

	///<summary>A single line of payment info in the Account.  Might be an actual payment, or an unattached paysplit, or a daily summary of attached paysplits.</summary>
	public class PayInfo{
		///<summary>Payment,PaySplit, or DailySummary.</summary>
		public PayInfoType Type;
		///<summary></summary>
		public DateTime Date;
		///<summary></summary>
		public string Description;
		///<summary>The amount that will affect the balance. Other amounts should be included in the description instead.</summary>
		public double Amount;
		///<summary>The payment to which this info refers.</summary>
		public int PayNum;
		///<summary></summary>
		public int PayPlanNum;

		///<summary></summary>
		public PayInfo Copy(){
			PayInfo pi=new PayInfo();
			pi.Type=Type;
			pi.Date=Date;
			pi.Description=Description;
			pi.Amount=Amount;
			pi.PayNum=PayNum;
			pi.PayPlanNum=PayPlanNum;
			return pi;
		}
	}

	///<summary>Used by PayInfo to display lines in Account.</summary>
	public enum PayInfoType{
		///<summary>A payment line. The entire payment amount does not necessarily affect the balance.  Splits only affect the balance here if they have the same date and patNum as the payment.  Whether or not they are attached to procedures, they will be included.  DailySummaries never show for paysplits with same date as payment.</summary>
		Payment,
		///<summary>A single paysplit. The date or patNum is different than the payment, but not attached to a procedure.  So balance is affected.</summary>
		PaySplit,
		///<summary>Informational only.  Does not affect balance. A group of all paysplits with the same date and patnum. All are attached to procedures.  Balance is affected by individual line item entries on the procedure lines.</summary>
		DailySummary
	}

}











