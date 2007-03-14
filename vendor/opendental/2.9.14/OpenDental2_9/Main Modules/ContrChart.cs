/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using OpenDental.UI;

namespace OpenDental{
///<summary></summary>
	public class ContrChart : System.Windows.Forms.UserControl	{
		private System.Windows.Forms.Button butAddProc;
		private System.Windows.Forms.Button butM;
		private System.Windows.Forms.Button butO;
		private System.Windows.Forms.Button butD;
		private System.Windows.Forms.Button butB;
		private System.Windows.Forms.Button butL;
		private System.Windows.Forms.Button butI;
		private System.Windows.Forms.Button butF;
		private System.Windows.Forms.TextBox textSurf;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton radioEntryTP;
		private System.Windows.Forms.RadioButton radioEntryEO;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.RadioButton radioEntryEC;
		//private string[] newToothNum2= new string[1];
		private ProcStat newStatus;
		//private bool ControlDown=false;
		private System.Windows.Forms.Button button1;
		private ArrayList ProcAL;
		//private TPChartLines[] TPChartLines2;
		private System.Windows.Forms.RadioButton radioEntryC;
		private ArrayList ProgLineAL;
		private OpenDental.TableProg tbProg;
		private bool dataValid=false;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button butMedicalService;
		private System.Windows.Forms.ListBox listDx;
		private int[] hiLightedRows=new int[1];
		private System.Windows.Forms.GroupBox groupNext;
		private ContrApptSingle ApptNext;
		private System.Windows.Forms.CheckBox checkDone;
		private System.Windows.Forms.Label labelMinutes;
		private System.Windows.Forms.Button butEnterTx;
		private System.Windows.Forms.ImageList imageListUpDown;
		private System.Windows.Forms.Panel panelMedical;
		private System.Windows.Forms.Panel panelEnterTx;
		private ArrayList RxAL;
		private System.Windows.Forms.RadioButton radioEntryR;
		private System.Windows.Forms.TextBox textService;
		private System.Windows.Forms.TextBox textMedUrgNote;
		private System.Windows.Forms.TextBox textMedical;
		private System.Windows.Forms.Label label13;
		private OpenDental.ContrTeeth cTeeth;
		private System.Windows.Forms.CheckBox checkNotes;
		private System.Windows.Forms.CheckBox checkShowR;
		private System.Windows.Forms.Button butShowNone;
		private System.Windows.Forms.Button butShowAll;
		private System.Windows.Forms.CheckBox checkShowE;
		private System.Windows.Forms.CheckBox checkShowC;
		private System.Windows.Forms.CheckBox checkShowTP;
		private System.Windows.Forms.CheckBox checkRx;
		private System.Windows.Forms.GroupBox groupShow;
		private System.Windows.Forms.ImageList imageListMain;
		private System.Windows.Forms.ContextMenu menuPrimary;
		private System.Windows.Forms.MenuItem menuPrimaryToggle;
		private System.Windows.Forms.MenuItem menuPrimaryAll;
		private System.Windows.Forms.MenuItem menuPrimaryNone;
		private System.Windows.Forms.TextBox textADACode;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Button butOK;
		private OpenDental.XPButton butNew;
		private OpenDental.XPButton butClear;
		private OpenDental.UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.Label labelDx;
		private System.Windows.Forms.Label labelNewProcHint;
		private System.Windows.Forms.TextBox textTreatmentNotes;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ListBox listProcButtons;
		private System.Windows.Forms.TextBox textCreditType;
		private System.Windows.Forms.TextBox textIns;
		private System.Windows.Forms.Panel panelABCins;
		private System.Windows.Forms.TextBox textReferral;
		private System.Windows.Forms.TextBox textBillingType;
		private System.Windows.Forms.TextBox textDateFirstVisit;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ComboBox comboPriority;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ContextMenu menuProgRight;
		private System.Windows.Forms.MenuItem menuItemPrintProg;
//fix: use AL for tbProg.SelectedRowsAL
		//public	VisiQuick VQLink;  // TJE
		private bool TreatmentNoteChanged;
			
		///<summary></summary>
		public ContrChart(){
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
			//tbProg.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbProg_CellClicked);
			tbProg.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbProg_CellDoubleClicked);
			//tbTeeth.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbTeeth_CellClicked);
			listProcButtons.Click += new System.EventHandler(this.listProcButtons_Click);
			//VQLink=new VisiQuick(Handle);		// TJE
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			//if (VQLink != null)					// TJE
			//	VQLink.DoneVQ();				// TJE
			if( disposing ){
				if(components != null){
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
		private void InitializeComponent(){
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ContrChart));
			this.butAddProc = new System.Windows.Forms.Button();
			this.butM = new System.Windows.Forms.Button();
			this.butO = new System.Windows.Forms.Button();
			this.butD = new System.Windows.Forms.Button();
			this.butB = new System.Windows.Forms.Button();
			this.butL = new System.Windows.Forms.Button();
			this.butI = new System.Windows.Forms.Button();
			this.butF = new System.Windows.Forms.Button();
			this.textSurf = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.radioEntryR = new System.Windows.Forms.RadioButton();
			this.radioEntryC = new System.Windows.Forms.RadioButton();
			this.radioEntryEO = new System.Windows.Forms.RadioButton();
			this.radioEntryEC = new System.Windows.Forms.RadioButton();
			this.radioEntryTP = new System.Windows.Forms.RadioButton();
			this.button1 = new System.Windows.Forms.Button();
			this.panelMedical = new System.Windows.Forms.Panel();
			this.butMedicalService = new System.Windows.Forms.Button();
			this.textMedUrgNote = new System.Windows.Forms.TextBox();
			this.textService = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textMedical = new System.Windows.Forms.TextBox();
			this.listDx = new System.Windows.Forms.ListBox();
			this.labelDx = new System.Windows.Forms.Label();
			this.tbProg = new OpenDental.TableProg();
			this.groupNext = new System.Windows.Forms.GroupBox();
			this.butClear = new OpenDental.XPButton();
			this.butNew = new OpenDental.XPButton();
			this.labelMinutes = new System.Windows.Forms.Label();
			this.checkDone = new System.Windows.Forms.CheckBox();
			this.panelEnterTx = new System.Windows.Forms.Panel();
			this.label5 = new System.Windows.Forms.Label();
			this.comboPriority = new System.Windows.Forms.ComboBox();
			this.butOK = new System.Windows.Forms.Button();
			this.textADACode = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.listProcButtons = new System.Windows.Forms.ListBox();
			this.label13 = new System.Windows.Forms.Label();
			this.butEnterTx = new System.Windows.Forms.Button();
			this.imageListUpDown = new System.Windows.Forms.ImageList(this.components);
			this.cTeeth = new OpenDental.ContrTeeth();
			this.groupShow = new System.Windows.Forms.GroupBox();
			this.checkNotes = new System.Windows.Forms.CheckBox();
			this.checkRx = new System.Windows.Forms.CheckBox();
			this.checkShowR = new System.Windows.Forms.CheckBox();
			this.butShowNone = new System.Windows.Forms.Button();
			this.butShowAll = new System.Windows.Forms.Button();
			this.checkShowE = new System.Windows.Forms.CheckBox();
			this.checkShowC = new System.Windows.Forms.CheckBox();
			this.checkShowTP = new System.Windows.Forms.CheckBox();
			this.menuPrimary = new System.Windows.Forms.ContextMenu();
			this.menuPrimaryToggle = new System.Windows.Forms.MenuItem();
			this.menuPrimaryAll = new System.Windows.Forms.MenuItem();
			this.menuPrimaryNone = new System.Windows.Forms.MenuItem();
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.labelNewProcHint = new System.Windows.Forms.Label();
			this.textTreatmentNotes = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textCreditType = new System.Windows.Forms.TextBox();
			this.textIns = new System.Windows.Forms.TextBox();
			this.panelABCins = new System.Windows.Forms.Panel();
			this.textReferral = new System.Windows.Forms.TextBox();
			this.textBillingType = new System.Windows.Forms.TextBox();
			this.textDateFirstVisit = new System.Windows.Forms.TextBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.menuProgRight = new System.Windows.Forms.ContextMenu();
			this.menuItemPrintProg = new System.Windows.Forms.MenuItem();
			this.groupBox2.SuspendLayout();
			this.panelMedical.SuspendLayout();
			this.groupNext.SuspendLayout();
			this.panelEnterTx.SuspendLayout();
			this.groupShow.SuspendLayout();
			this.panelABCins.SuspendLayout();
			this.SuspendLayout();
			// 
			// butAddProc
			// 
			this.butAddProc.BackColor = System.Drawing.SystemColors.Control;
			this.butAddProc.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butAddProc.Location = new System.Drawing.Point(192, 2);
			this.butAddProc.Name = "butAddProc";
			this.butAddProc.Size = new System.Drawing.Size(105, 20);
			this.butAddProc.TabIndex = 17;
			this.butAddProc.Text = "Procedure List";
			this.butAddProc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddProc.Click += new System.EventHandler(this.butAddProc_Click);
			// 
			// butM
			// 
			this.butM.BackColor = System.Drawing.SystemColors.Control;
			this.butM.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butM.Location = new System.Drawing.Point(4, 50);
			this.butM.Name = "butM";
			this.butM.Size = new System.Drawing.Size(24, 20);
			this.butM.TabIndex = 18;
			this.butM.Text = "M";
			this.butM.Click += new System.EventHandler(this.butM_Click);
			// 
			// butO
			// 
			this.butO.BackColor = System.Drawing.SystemColors.Control;
			this.butO.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butO.Location = new System.Drawing.Point(28, 50);
			this.butO.Name = "butO";
			this.butO.Size = new System.Drawing.Size(16, 20);
			this.butO.TabIndex = 19;
			this.butO.Text = "O";
			this.butO.Click += new System.EventHandler(this.butO_Click);
			// 
			// butD
			// 
			this.butD.BackColor = System.Drawing.SystemColors.Control;
			this.butD.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butD.Location = new System.Drawing.Point(60, 50);
			this.butD.Name = "butD";
			this.butD.Size = new System.Drawing.Size(24, 20);
			this.butD.TabIndex = 20;
			this.butD.Text = "D";
			this.butD.Click += new System.EventHandler(this.butD_Click);
			// 
			// butB
			// 
			this.butB.BackColor = System.Drawing.SystemColors.Control;
			this.butB.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butB.Location = new System.Drawing.Point(28, 30);
			this.butB.Name = "butB";
			this.butB.Size = new System.Drawing.Size(16, 20);
			this.butB.TabIndex = 21;
			this.butB.Text = "B";
			this.butB.Click += new System.EventHandler(this.butB_Click);
			// 
			// butL
			// 
			this.butL.BackColor = System.Drawing.SystemColors.Control;
			this.butL.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butL.Location = new System.Drawing.Point(32, 70);
			this.butL.Name = "butL";
			this.butL.Size = new System.Drawing.Size(24, 20);
			this.butL.TabIndex = 22;
			this.butL.Text = "L";
			this.butL.Click += new System.EventHandler(this.butL_Click);
			// 
			// butI
			// 
			this.butI.BackColor = System.Drawing.SystemColors.Control;
			this.butI.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butI.Location = new System.Drawing.Point(44, 50);
			this.butI.Name = "butI";
			this.butI.Size = new System.Drawing.Size(16, 20);
			this.butI.TabIndex = 23;
			this.butI.Text = "I";
			this.butI.Click += new System.EventHandler(this.butI_Click);
			// 
			// butF
			// 
			this.butF.BackColor = System.Drawing.SystemColors.Control;
			this.butF.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butF.Location = new System.Drawing.Point(44, 30);
			this.butF.Name = "butF";
			this.butF.Size = new System.Drawing.Size(16, 20);
			this.butF.TabIndex = 24;
			this.butF.Text = "F";
			this.butF.Click += new System.EventHandler(this.butF_Click);
			// 
			// textSurf
			// 
			this.textSurf.Location = new System.Drawing.Point(9, 8);
			this.textSurf.Name = "textSurf";
			this.textSurf.ReadOnly = true;
			this.textSurf.Size = new System.Drawing.Size(72, 20);
			this.textSurf.TabIndex = 25;
			this.textSurf.Text = "";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.radioEntryR);
			this.groupBox2.Controls.Add(this.radioEntryC);
			this.groupBox2.Controls.Add(this.radioEntryEO);
			this.groupBox2.Controls.Add(this.radioEntryEC);
			this.groupBox2.Controls.Add(this.radioEntryTP);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(2, 95);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(88, 100);
			this.groupBox2.TabIndex = 35;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Entry Status";
			// 
			// radioEntryR
			// 
			this.radioEntryR.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioEntryR.Location = new System.Drawing.Point(8, 79);
			this.radioEntryR.Name = "radioEntryR";
			this.radioEntryR.Size = new System.Drawing.Size(75, 16);
			this.radioEntryR.TabIndex = 4;
			this.radioEntryR.Text = "Referred";
			this.radioEntryR.CheckedChanged += new System.EventHandler(this.radioEntryR_CheckedChanged);
			// 
			// radioEntryC
			// 
			this.radioEntryC.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioEntryC.Location = new System.Drawing.Point(8, 31);
			this.radioEntryC.Name = "radioEntryC";
			this.radioEntryC.Size = new System.Drawing.Size(74, 16);
			this.radioEntryC.TabIndex = 3;
			this.radioEntryC.Text = "C";
			this.radioEntryC.CheckedChanged += new System.EventHandler(this.radioEntryC_CheckedChanged);
			// 
			// radioEntryEO
			// 
			this.radioEntryEO.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioEntryEO.Location = new System.Drawing.Point(8, 63);
			this.radioEntryEO.Name = "radioEntryEO";
			this.radioEntryEO.Size = new System.Drawing.Size(72, 16);
			this.radioEntryEO.TabIndex = 2;
			this.radioEntryEO.Text = "Ex Other";
			this.radioEntryEO.CheckedChanged += new System.EventHandler(this.radioEntryEO_CheckedChanged);
			// 
			// radioEntryEC
			// 
			this.radioEntryEC.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioEntryEC.Location = new System.Drawing.Point(8, 47);
			this.radioEntryEC.Name = "radioEntryEC";
			this.radioEntryEC.Size = new System.Drawing.Size(77, 16);
			this.radioEntryEC.TabIndex = 1;
			this.radioEntryEC.Text = "Ex Cur";
			this.radioEntryEC.CheckedChanged += new System.EventHandler(this.radioEntryEC_CheckedChanged);
			// 
			// radioEntryTP
			// 
			this.radioEntryTP.Checked = true;
			this.radioEntryTP.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioEntryTP.Location = new System.Drawing.Point(8, 15);
			this.radioEntryTP.Name = "radioEntryTP";
			this.radioEntryTP.Size = new System.Drawing.Size(77, 16);
			this.radioEntryTP.TabIndex = 0;
			this.radioEntryTP.TabStop = true;
			this.radioEntryTP.Text = "TP";
			this.radioEntryTP.CheckedChanged += new System.EventHandler(this.radioEntryTP_CheckedChanged);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(127, 692);
			this.button1.Name = "button1";
			this.button1.TabIndex = 36;
			this.button1.Text = "invisible";
			this.button1.Visible = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// panelMedical
			// 
			this.panelMedical.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(192)), ((System.Byte)(192)));
			this.panelMedical.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelMedical.Controls.Add(this.butMedicalService);
			this.panelMedical.Controls.Add(this.textMedUrgNote);
			this.panelMedical.Controls.Add(this.textService);
			this.panelMedical.Controls.Add(this.label3);
			this.panelMedical.Controls.Add(this.label2);
			this.panelMedical.Controls.Add(this.label1);
			this.panelMedical.Controls.Add(this.textMedical);
			this.panelMedical.Location = new System.Drawing.Point(1, 581);
			this.panelMedical.Name = "panelMedical";
			this.panelMedical.Size = new System.Drawing.Size(411, 97);
			this.panelMedical.TabIndex = 39;
			// 
			// butMedicalService
			// 
			this.butMedicalService.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butMedicalService.Location = new System.Drawing.Point(279, 1);
			this.butMedicalService.Name = "butMedicalService";
			this.butMedicalService.Size = new System.Drawing.Size(128, 23);
			this.butMedicalService.TabIndex = 43;
			this.butMedicalService.Text = "Edit Medical/Service";
			this.butMedicalService.Click += new System.EventHandler(this.butMedicalService_Click);
			// 
			// textMedUrgNote
			// 
			this.textMedUrgNote.BackColor = System.Drawing.Color.White;
			this.textMedUrgNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textMedUrgNote.ForeColor = System.Drawing.Color.Red;
			this.textMedUrgNote.Location = new System.Drawing.Point(66, 1);
			this.textMedUrgNote.Multiline = true;
			this.textMedUrgNote.Name = "textMedUrgNote";
			this.textMedUrgNote.ReadOnly = true;
			this.textMedUrgNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textMedUrgNote.Size = new System.Drawing.Size(202, 20);
			this.textMedUrgNote.TabIndex = 41;
			this.textMedUrgNote.Text = "";
			// 
			// textService
			// 
			this.textService.BackColor = System.Drawing.Color.White;
			this.textService.Location = new System.Drawing.Point(205, 37);
			this.textService.Multiline = true;
			this.textService.Name = "textService";
			this.textService.ReadOnly = true;
			this.textService.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textService.Size = new System.Drawing.Size(202, 57);
			this.textService.TabIndex = 45;
			this.textService.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(203, 23);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 16);
			this.label3.TabIndex = 44;
			this.label3.Text = "Service Notes";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(3, 4);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 14);
			this.label2.TabIndex = 42;
			this.label2.Text = "Med Urgent";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(2, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(182, 13);
			this.label1.TabIndex = 40;
			this.label1.Text = "Medical Summary";
			// 
			// textMedical
			// 
			this.textMedical.BackColor = System.Drawing.Color.White;
			this.textMedical.Location = new System.Drawing.Point(2, 37);
			this.textMedical.Multiline = true;
			this.textMedical.Name = "textMedical";
			this.textMedical.ReadOnly = true;
			this.textMedical.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textMedical.Size = new System.Drawing.Size(202, 57);
			this.textMedical.TabIndex = 39;
			this.textMedical.Text = "";
			// 
			// listDx
			// 
			this.listDx.Location = new System.Drawing.Point(92, 18);
			this.listDx.Name = "listDx";
			this.listDx.Size = new System.Drawing.Size(94, 173);
			this.listDx.TabIndex = 46;
			this.listDx.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listDx_MouseDown);
			// 
			// labelDx
			// 
			this.labelDx.Location = new System.Drawing.Point(90, 3);
			this.labelDx.Name = "labelDx";
			this.labelDx.Size = new System.Drawing.Size(100, 18);
			this.labelDx.TabIndex = 47;
			this.labelDx.Text = "Diagnosis";
			// 
			// tbProg
			// 
			this.tbProg.BackColor = System.Drawing.SystemColors.Window;
			this.tbProg.Location = new System.Drawing.Point(415, 281);
			this.tbProg.Name = "tbProg";
			this.tbProg.ScrollValue = 1;
			this.tbProg.SelectedIndices = new int[0];
			this.tbProg.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.tbProg.Size = new System.Drawing.Size(498, 397);
			this.tbProg.TabIndex = 40;
			this.tbProg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbProg_MouseUp);
			this.tbProg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbProg_KeyDown);
			// 
			// groupNext
			// 
			this.groupNext.Controls.Add(this.butClear);
			this.groupNext.Controls.Add(this.butNew);
			this.groupNext.Controls.Add(this.labelMinutes);
			this.groupNext.Controls.Add(this.checkDone);
			this.groupNext.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupNext.Location = new System.Drawing.Point(0, 30);
			this.groupNext.Name = "groupNext";
			this.groupNext.Size = new System.Drawing.Size(198, 114);
			this.groupNext.TabIndex = 43;
			this.groupNext.TabStop = false;
			this.groupNext.Text = "Next Appointment";
			// 
			// butClear
			// 
			this.butClear.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClear.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butClear.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butClear.Image = ((System.Drawing.Image)(resources.GetObject("butClear.Image")));
			this.butClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butClear.Location = new System.Drawing.Point(117, 58);
			this.butClear.Name = "butClear";
			this.butClear.Size = new System.Drawing.Size(75, 26);
			this.butClear.TabIndex = 5;
			this.butClear.Text = "Clear";
			this.butClear.Click += new System.EventHandler(this.butClear_Click);
			// 
			// butNew
			// 
			this.butNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butNew.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butNew.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butNew.Image = ((System.Drawing.Image)(resources.GetObject("butNew.Image")));
			this.butNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butNew.Location = new System.Drawing.Point(117, 30);
			this.butNew.Name = "butNew";
			this.butNew.Size = new System.Drawing.Size(75, 26);
			this.butNew.TabIndex = 4;
			this.butNew.Text = "New";
			this.butNew.Click += new System.EventHandler(this.butNew_Click);
			// 
			// labelMinutes
			// 
			this.labelMinutes.Location = new System.Drawing.Point(118, 90);
			this.labelMinutes.Name = "labelMinutes";
			this.labelMinutes.Size = new System.Drawing.Size(66, 14);
			this.labelMinutes.TabIndex = 3;
			// 
			// checkDone
			// 
			this.checkDone.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkDone.Location = new System.Drawing.Point(124, 12);
			this.checkDone.Name = "checkDone";
			this.checkDone.Size = new System.Drawing.Size(56, 16);
			this.checkDone.TabIndex = 0;
			this.checkDone.Text = "Done";
			this.checkDone.Click += new System.EventHandler(this.checkDone_Click);
			// 
			// panelEnterTx
			// 
			this.panelEnterTx.BackColor = System.Drawing.SystemColors.Control;
			this.panelEnterTx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelEnterTx.Controls.Add(this.listDx);
			this.panelEnterTx.Controls.Add(this.label5);
			this.panelEnterTx.Controls.Add(this.comboPriority);
			this.panelEnterTx.Controls.Add(this.butOK);
			this.panelEnterTx.Controls.Add(this.butAddProc);
			this.panelEnterTx.Controls.Add(this.textADACode);
			this.panelEnterTx.Controls.Add(this.label14);
			this.panelEnterTx.Controls.Add(this.listProcButtons);
			this.panelEnterTx.Controls.Add(this.label13);
			this.panelEnterTx.Controls.Add(this.labelDx);
			this.panelEnterTx.Controls.Add(this.butO);
			this.panelEnterTx.Controls.Add(this.butF);
			this.panelEnterTx.Controls.Add(this.butI);
			this.panelEnterTx.Controls.Add(this.butM);
			this.panelEnterTx.Controls.Add(this.butL);
			this.panelEnterTx.Controls.Add(this.butB);
			this.panelEnterTx.Controls.Add(this.textSurf);
			this.panelEnterTx.Controls.Add(this.butD);
			this.panelEnterTx.Controls.Add(this.groupBox2);
			this.panelEnterTx.Location = new System.Drawing.Point(416, 51);
			this.panelEnterTx.Name = "panelEnterTx";
			this.panelEnterTx.Size = new System.Drawing.Size(496, 229);
			this.panelEnterTx.TabIndex = 44;
			this.panelEnterTx.Visible = false;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(3, 202);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(88, 18);
			this.label5.TabIndex = 55;
			this.label5.Text = "Entry Priority";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboPriority
			// 
			this.comboPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPriority.Location = new System.Drawing.Point(92, 200);
			this.comboPriority.MaxDropDownItems = 40;
			this.comboPriority.Name = "comboPriority";
			this.comboPriority.Size = new System.Drawing.Size(96, 21);
			this.comboPriority.TabIndex = 54;
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(447, 2);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(44, 20);
			this.butOK.TabIndex = 52;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textADACode
			// 
			this.textADACode.Location = new System.Drawing.Point(337, 3);
			this.textADACode.Name = "textADACode";
			this.textADACode.Size = new System.Drawing.Size(108, 20);
			this.textADACode.TabIndex = 50;
			this.textADACode.Text = "Type ADA Code";
			this.textADACode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textADACode_KeyDown);
			this.textADACode.TextChanged += new System.EventHandler(this.textADACode_TextChanged);
			this.textADACode.Enter += new System.EventHandler(this.textADACode_Enter);
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(303, 7);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(42, 17);
			this.label14.TabIndex = 51;
			this.label14.Text = "Or";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// listProcButtons
			// 
			this.listProcButtons.Location = new System.Drawing.Point(192, 40);
			this.listProcButtons.MultiColumn = true;
			this.listProcButtons.Name = "listProcButtons";
			this.listProcButtons.Size = new System.Drawing.Size(300, 186);
			this.listProcButtons.TabIndex = 48;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(191, 20);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(128, 18);
			this.label13.TabIndex = 49;
			this.label13.Text = "Or Single Click:";
			this.label13.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butEnterTx
			// 
			this.butEnterTx.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.butEnterTx.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butEnterTx.ImageIndex = 1;
			this.butEnterTx.ImageList = this.imageListUpDown;
			this.butEnterTx.Location = new System.Drawing.Point(416, 32);
			this.butEnterTx.Name = "butEnterTx";
			this.butEnterTx.Size = new System.Drawing.Size(79, 20);
			this.butEnterTx.TabIndex = 40;
			this.butEnterTx.Text = "Enter Tx";
			this.butEnterTx.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butEnterTx.Click += new System.EventHandler(this.butEnterTx_Click);
			// 
			// imageListUpDown
			// 
			this.imageListUpDown.ImageSize = new System.Drawing.Size(10, 10);
			this.imageListUpDown.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListUpDown.ImageStream")));
			this.imageListUpDown.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// cTeeth
			// 
			this.cTeeth.BackColor = System.Drawing.Color.White;
			this.cTeeth.Location = new System.Drawing.Point(0, 146);
			this.cTeeth.Name = "cTeeth";
			this.cTeeth.Size = new System.Drawing.Size(410, 259);
			this.cTeeth.TabIndex = 171;
			this.cTeeth.Zoom = 0.15F;
			this.cTeeth.Click += new System.EventHandler(this.cTeeth_Click);
			// 
			// groupShow
			// 
			this.groupShow.Controls.Add(this.checkNotes);
			this.groupShow.Controls.Add(this.checkRx);
			this.groupShow.Controls.Add(this.checkShowR);
			this.groupShow.Controls.Add(this.butShowNone);
			this.groupShow.Controls.Add(this.butShowAll);
			this.groupShow.Controls.Add(this.checkShowE);
			this.groupShow.Controls.Add(this.checkShowC);
			this.groupShow.Controls.Add(this.checkShowTP);
			this.groupShow.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupShow.Location = new System.Drawing.Point(200, 30);
			this.groupShow.Name = "groupShow";
			this.groupShow.Size = new System.Drawing.Size(211, 114);
			this.groupShow.TabIndex = 38;
			this.groupShow.TabStop = false;
			this.groupShow.Text = "Show:";
			// 
			// checkNotes
			// 
			this.checkNotes.AllowDrop = true;
			this.checkNotes.Checked = true;
			this.checkNotes.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkNotes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNotes.Location = new System.Drawing.Point(104, 30);
			this.checkNotes.Name = "checkNotes";
			this.checkNotes.Size = new System.Drawing.Size(102, 16);
			this.checkNotes.TabIndex = 11;
			this.checkNotes.Text = "Proc Notes";
			this.checkNotes.Click += new System.EventHandler(this.checkNotes_Click);
			// 
			// checkRx
			// 
			this.checkRx.Checked = true;
			this.checkRx.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkRx.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkRx.Location = new System.Drawing.Point(104, 15);
			this.checkRx.Name = "checkRx";
			this.checkRx.Size = new System.Drawing.Size(95, 16);
			this.checkRx.TabIndex = 8;
			this.checkRx.Text = "Rx";
			this.checkRx.Click += new System.EventHandler(this.checkRx_Click);
			// 
			// checkShowR
			// 
			this.checkShowR.Checked = true;
			this.checkShowR.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowR.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowR.Location = new System.Drawing.Point(4, 60);
			this.checkShowR.Name = "checkShowR";
			this.checkShowR.Size = new System.Drawing.Size(104, 16);
			this.checkShowR.TabIndex = 14;
			this.checkShowR.Text = "Referred";
			this.checkShowR.Click += new System.EventHandler(this.checkShowR_Click);
			// 
			// butShowNone
			// 
			this.butShowNone.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butShowNone.Location = new System.Drawing.Point(69, 84);
			this.butShowNone.Name = "butShowNone";
			this.butShowNone.Size = new System.Drawing.Size(58, 22);
			this.butShowNone.TabIndex = 13;
			this.butShowNone.Text = "None";
			this.butShowNone.Click += new System.EventHandler(this.butShowNone_Click);
			// 
			// butShowAll
			// 
			this.butShowAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butShowAll.Location = new System.Drawing.Point(5, 84);
			this.butShowAll.Name = "butShowAll";
			this.butShowAll.Size = new System.Drawing.Size(53, 22);
			this.butShowAll.TabIndex = 12;
			this.butShowAll.Text = "All";
			this.butShowAll.Click += new System.EventHandler(this.butShowAll_Click);
			// 
			// checkShowE
			// 
			this.checkShowE.Checked = true;
			this.checkShowE.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowE.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowE.Location = new System.Drawing.Point(4, 45);
			this.checkShowE.Name = "checkShowE";
			this.checkShowE.Size = new System.Drawing.Size(100, 16);
			this.checkShowE.TabIndex = 10;
			this.checkShowE.Text = "Existing";
			this.checkShowE.Click += new System.EventHandler(this.checkShowE_Click);
			// 
			// checkShowC
			// 
			this.checkShowC.Checked = true;
			this.checkShowC.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowC.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowC.Location = new System.Drawing.Point(4, 30);
			this.checkShowC.Name = "checkShowC";
			this.checkShowC.Size = new System.Drawing.Size(100, 16);
			this.checkShowC.TabIndex = 9;
			this.checkShowC.Text = "Completed";
			this.checkShowC.Click += new System.EventHandler(this.checkShowC_Click);
			// 
			// checkShowTP
			// 
			this.checkShowTP.Checked = true;
			this.checkShowTP.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowTP.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowTP.Location = new System.Drawing.Point(4, 15);
			this.checkShowTP.Name = "checkShowTP";
			this.checkShowTP.Size = new System.Drawing.Size(101, 16);
			this.checkShowTP.TabIndex = 8;
			this.checkShowTP.Text = "Treat Plan";
			this.checkShowTP.Click += new System.EventHandler(this.checkShowTP_Click);
			// 
			// menuPrimary
			// 
			this.menuPrimary.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																								this.menuPrimaryToggle,
																																								this.menuPrimaryAll,
																																								this.menuPrimaryNone});
			// 
			// menuPrimaryToggle
			// 
			this.menuPrimaryToggle.Index = 0;
			this.menuPrimaryToggle.Text = "Toggle Selected";
			this.menuPrimaryToggle.Click += new System.EventHandler(this.menuPrimaryToggle_Click);
			// 
			// menuPrimaryAll
			// 
			this.menuPrimaryAll.Index = 1;
			this.menuPrimaryAll.Text = "Set All Primary";
			this.menuPrimaryAll.Click += new System.EventHandler(this.menuPrimaryAll_Click);
			// 
			// menuPrimaryNone
			// 
			this.menuPrimaryNone.Index = 2;
			this.menuPrimaryNone.Text = "Set All Permanent";
			this.menuPrimaryNone.Click += new System.EventHandler(this.menuPrimaryNone_Click);
			// 
			// imageListMain
			// 
			this.imageListMain.ImageSize = new System.Drawing.Size(22, 22);
			this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
			this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListMain;
			this.ToolBarMain.Location = new System.Drawing.Point(0, 0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(939, 29);
			this.ToolBarMain.TabIndex = 177;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// labelNewProcHint
			// 
			this.labelNewProcHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelNewProcHint.Location = new System.Drawing.Point(608, 32);
			this.labelNewProcHint.Name = "labelNewProcHint";
			this.labelNewProcHint.Size = new System.Drawing.Size(294, 16);
			this.labelNewProcHint.TabIndex = 178;
			this.labelNewProcHint.Text = "Enter New Procedures Here:";
			// 
			// textTreatmentNotes
			// 
			this.textTreatmentNotes.Location = new System.Drawing.Point(0, 427);
			this.textTreatmentNotes.Multiline = true;
			this.textTreatmentNotes.Name = "textTreatmentNotes";
			this.textTreatmentNotes.Size = new System.Drawing.Size(411, 104);
			this.textTreatmentNotes.TabIndex = 179;
			this.textTreatmentNotes.Text = "";
			this.textTreatmentNotes.TextChanged += new System.EventHandler(this.textTreatmentNotes_TextChanged);
			this.textTreatmentNotes.Leave += new System.EventHandler(this.textTreatmentNotes_Leave);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(1, 411);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(384, 18);
			this.label4.TabIndex = 180;
			this.label4.Text = "Treatment Notes (for items that do not display above)";
			// 
			// textCreditType
			// 
			this.textCreditType.Location = new System.Drawing.Point(0, 0);
			this.textCreditType.Name = "textCreditType";
			this.textCreditType.ReadOnly = true;
			this.textCreditType.Size = new System.Drawing.Size(22, 20);
			this.textCreditType.TabIndex = 181;
			this.textCreditType.Text = "A";
			this.toolTip1.SetToolTip(this.textCreditType, "Credit Type");
			// 
			// textIns
			// 
			this.textIns.Location = new System.Drawing.Point(69, 20);
			this.textIns.Name = "textIns";
			this.textIns.ReadOnly = true;
			this.textIns.Size = new System.Drawing.Size(342, 20);
			this.textIns.TabIndex = 182;
			this.textIns.Text = "Ins";
			this.toolTip1.SetToolTip(this.textIns, "Insurance");
			// 
			// panelABCins
			// 
			this.panelABCins.Controls.Add(this.textReferral);
			this.panelABCins.Controls.Add(this.textCreditType);
			this.panelABCins.Controls.Add(this.textIns);
			this.panelABCins.Controls.Add(this.textBillingType);
			this.panelABCins.Controls.Add(this.textDateFirstVisit);
			this.panelABCins.Location = new System.Drawing.Point(0, 537);
			this.panelABCins.Name = "panelABCins";
			this.panelABCins.Size = new System.Drawing.Size(429, 40);
			this.panelABCins.TabIndex = 183;
			// 
			// textReferral
			// 
			this.textReferral.Location = new System.Drawing.Point(208, 0);
			this.textReferral.Name = "textReferral";
			this.textReferral.ReadOnly = true;
			this.textReferral.Size = new System.Drawing.Size(202, 20);
			this.textReferral.TabIndex = 183;
			this.textReferral.Text = "Referral";
			this.toolTip1.SetToolTip(this.textReferral, "Referral");
			// 
			// textBillingType
			// 
			this.textBillingType.Location = new System.Drawing.Point(22, 0);
			this.textBillingType.Name = "textBillingType";
			this.textBillingType.ReadOnly = true;
			this.textBillingType.Size = new System.Drawing.Size(186, 20);
			this.textBillingType.TabIndex = 184;
			this.textBillingType.Text = "Billing Type";
			this.toolTip1.SetToolTip(this.textBillingType, "BillingType");
			// 
			// textDateFirstVisit
			// 
			this.textDateFirstVisit.Location = new System.Drawing.Point(0, 20);
			this.textDateFirstVisit.Name = "textDateFirstVisit";
			this.textDateFirstVisit.ReadOnly = true;
			this.textDateFirstVisit.Size = new System.Drawing.Size(69, 20);
			this.textDateFirstVisit.TabIndex = 184;
			this.textDateFirstVisit.Text = "Date";
			this.toolTip1.SetToolTip(this.textDateFirstVisit, "Date of First Visit");
			// 
			// menuProgRight
			// 
			this.menuProgRight.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																									this.menuItemPrintProg});
			// 
			// menuItemPrintProg
			// 
			this.menuItemPrintProg.Index = 0;
			this.menuItemPrintProg.Text = "Print Progress Notes ...";
			this.menuItemPrintProg.Click += new System.EventHandler(this.menuItemPrintProg_Click);
			// 
			// ContrChart
			// 
			this.Controls.Add(this.tbProg);
			this.Controls.Add(this.panelABCins);
			this.Controls.Add(this.textTreatmentNotes);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.labelNewProcHint);
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.cTeeth);
			this.Controls.Add(this.panelEnterTx);
			this.Controls.Add(this.panelMedical);
			this.Controls.Add(this.groupNext);
			this.Controls.Add(this.butEnterTx);
			this.Controls.Add(this.groupShow);
			this.Controls.Add(this.button1);
			this.Name = "ContrChart";
			this.Size = new System.Drawing.Size(939, 762);
			this.Resize += new System.EventHandler(this.ContrChart_Resize);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ContrChart_Layout);
			this.groupBox2.ResumeLayout(false);
			this.panelMedical.ResumeLayout(false);
			this.groupNext.ResumeLayout(false);
			this.panelEnterTx.ResumeLayout(false);
			this.groupShow.ResumeLayout(false);
			this.panelABCins.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		///<summary></summary>
		public bool DataValid{
      get{
        return dataValid;
      }
      set{
        dataValid=value;
      }
    }

		private void ContrChart_Layout(object sender, System.Windows.Forms.LayoutEventArgs e){
			tbProg.Height=this.ClientSize.Height-tbProg.Location.Y-2;
		}

		private void ContrChart_Resize(object sender, System.EventArgs e) {
			tbProg.LayoutTables();
		}

		///<summary></summary>
		public void InstantClasses(){
			newStatus=ProcStat.TP;
			ApptNext = new ContrApptSingle();
			ApptNext.Info.IsNext=true;
			ApptNext.Location=new Point(1,17);
			ApptNext.Visible=false;
			groupNext.Controls.Add(ApptNext);
			ApptNext.DoubleClick += new System.EventHandler(ApptNext_DoubleClick);
			panelEnterTx.Visible=false;
			butEnterTx.ImageIndex=1;//down arrow
			tbProg.Location=panelEnterTx.Location;
			//MessageBox.Show((panelEnterTx.Location.X+5).ToString());
			cTeeth.SetWidth(421);//if I had time, I would perfect this
			cTeeth.CreateBackShadow();
			tbProg.Height=this.ClientSize.Height-tbProg.Location.Y-2;
						Lan.C(this, new System.Windows.Forms.Control[] {
				this.butAddProc,
				this.butB,
				this.butClear,
				this.butD,
				//this.butDel,
				this.butEnterTx,
				this.butF,
				this.butI,
				this.butL,
				this.butM,
				this.butMedicalService,
				this.butNew,
				this.butO,
				this.butShowAll,
				this.butShowNone,
				this.label1,
				this.label13,
				this.label2,
				this.label3,
				this.labelDx,
				this.labelMinutes,
				this.groupBox2,
				this.groupShow,
				this.groupNext,
				this.panelEnterTx,
				this.panelMedical,
				//this.panelVQ,
				this.radioEntryC,
				this.radioEntryEC,
				this.radioEntryEO,
				this.radioEntryR,
				this.radioEntryTP,
				this.checkDone,
				this.checkNotes,
				this.checkRx,
				this.checkShowC,
				this.checkShowE,
				this.checkShowR,
				this.checkShowTP,
				this.butOK,
				this.label14,
				this.label13,
				this.textADACode
				});
			LayoutToolBar();
		}

		///<summary>Causes the toolbar to be laid out again.</summary>
		public void LayoutToolBar(){
			ToolBarMain.Buttons.Clear();
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Select Patient"),0,"","Patient"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"New Rx"),1,"","Rx"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ToolBarButtonStyle.Separator));
			ODToolBarButton button=new ODToolBarButton(Lan.g(this,"Primary"),-1
				,Lan.g(this,"Change the Primary/Permanent status of teeth"),"Primary");
			button.Style=ToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=menuPrimary;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton(ToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Perio Chart"),-1,"","Perio"));
			ArrayList toolButItems=ToolButItems.GetForToolBar(ToolBarsAvail.ChartModule);
			for(int i=0;i<toolButItems.Count;i++){
				ToolBarMain.Buttons.Add(new ODToolBarButton(ToolBarButtonStyle.Separator));
				ToolBarMain.Buttons.Add(new ODToolBarButton(((ToolButItem)toolButItems[i]).ButtonText
					,-1,"",((ToolButItem)toolButItems[i]).ProgramNum));
			}
			ToolBarMain.Invalidate();
		}

		///<summary></summary>
		public void ModuleSelected(){
			HideClinicalData();
			RefreshModuleData();
			RefreshModuleScreen();
		}

		///<summary></summary>
		public void ModuleUnselected(){
			if(Patients.FamilyList==null)
				return;
			if(TreatmentNoteChanged){
				PatientNotes.Cur.Treatment=textTreatmentNotes.Text;
				PatientNotes.UpdateCur();
				TreatmentNoteChanged=false;
			}
			Patients.FamilyList=null;
			InsPlans.List=null;
			CovPats.List=null;
			ClaimProcs.List=null;
			//from FillProgNotes:
			Procedures.List=null;
			Procedures.HList=null;
			Procedures.MissingTeeth=null;
			RxPats.List=null;
			RefAttaches.List=null;
		}

		private void RefreshModuleData(){
			if (Patients.PatIsLoaded){
				Patients.GetFamily(Patients.Cur.PatNum);
				InsPlans.Refresh();
				CovPats.Refresh();
				PatientNotes.Refresh();
        ClaimProcs.Refresh();
				RefAttaches.Refresh();
			}
		}

		private void RefreshModuleScreen(){
			if(Patients.PatIsLoaded){
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString+" - "+Patients.GetCurNameLF();
				groupShow.Enabled=true;
				panelMedical.Enabled=true;
				groupNext.Enabled=true;
				cTeeth.Enabled=true;
				tbProg.Enabled=true;
				ToolBarMain.Buttons["Rx"].Enabled=true;
				ToolBarMain.Buttons["Primary"].Enabled=true;
				ToolBarMain.Buttons["Perio"].Enabled=true;
				ToolBarMain.Invalidate();
				panelEnterTx.Enabled=true;//?
			}
			else{
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString;
				groupShow.Enabled=false;
				panelMedical.Enabled=false;
				groupNext.Enabled=false;
				cTeeth.Enabled=false;
				tbProg.Enabled=false;
				ToolBarMain.Buttons["Rx"].Enabled=false;
				ToolBarMain.Buttons["Primary"].Enabled=false;
				ToolBarMain.Buttons["Perio"].Enabled=false;
				ToolBarMain.Invalidate();
				panelEnterTx.Enabled=false;//?
			}
			ClearButtons();
			FillProgNotes();
			FillNext();
			FillMedical();
      FillDxAndProcLists();
			
		}

		private void HideClinicalData(){
			if(((Pref)Prefs.HList["EasyHideClinical"]).ValueString=="1"){
				panelMedical.Visible=false;
				checkShowE.Visible=false;
				checkShowR.Visible=false;
				checkRx.Visible=false;
				checkNotes.Visible=false;
				butShowNone.Visible=false;
				butShowAll.Visible=false;
				butEnterTx.Visible=false;
				panelEnterTx.Visible=false;//next line changes it, though
				OnClickEnterTx();
				radioEntryEC.Visible=false;
				radioEntryEO.Visible=false;
				radioEntryR.Visible=false;
				labelDx.Visible=false;
				listDx.Visible=false;
				labelNewProcHint.Visible=true;
			}
			else{
				panelMedical.Visible=true;
				checkShowE.Visible=true;
				checkShowR.Visible=true;
				checkRx.Visible=true;
				checkNotes.Visible=true;
				butShowNone.Visible=true;
				butShowAll.Visible=true;
				butEnterTx.Visible=true;
				radioEntryEC.Visible=true;
				radioEntryEO.Visible=true;
				radioEntryR.Visible=true;
				labelDx.Visible=true;
				listDx.Visible=true;
				labelNewProcHint.Visible=false;
			}
		}

		/*
		private void butPat_Click(object sender, System.EventArgs e) {
			toolButPatient_Click();
		}

		private void butRx_Click(object sender, System.EventArgs e) {
			toolButRx_Click();
		}

		private void butPrimary_Click_1(object sender, System.EventArgs e) {
			menuPrimary.Show(butPrimary,new Point(0,26));
		}*/

		private void ToolBarMain_ButtonClick(object sender, OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			if(e.Button.Tag.GetType()==typeof(string)){
				//standard predefined button
				switch(e.Button.Tag.ToString()){
					case "Patient":
						OnPatient_Click();
						break;
					case "Rx":
						OnRx_Click();
						break;
					case "Primary":
						//only respond to dropdown
						//OnPrimary_Click();
						break;
					case "Perio":
						OnPerio_Click();
						break;
				}
			}
			else if(e.Button.Tag.GetType()==typeof(int)){
				Programs.Execute((int)e.Button.Tag);
			}
		}

		private void OnPatient_Click(){
			FormPatientSelect formSelectPatient2 = new FormPatientSelect();
			formSelectPatient2.ShowDialog();
			if (formSelectPatient2.DialogResult == DialogResult.OK){
				ModuleSelected();
			}
		}

		private void OnRx_Click(){
			if(!UserPermissions.CheckUserPassword("Prescription Create")){
				MessageBox.Show(Lan.g(this,"You do not have Permission to Add Prescriptions"));
				return;
			}
			FormRxSelect FormRS=new FormRxSelect();
			FormRS.ShowDialog();
			if(FormRS.DialogResult!=DialogResult.OK) return;
			ModuleSelected();
		}

		private void OnPerio_Click(){
			FormPerio FormP=new FormPerio();
			FormP.ShowDialog();
		}

		private void FillNext(){
			if(!Patients.PatIsLoaded){
				ApptNext.Visible=false;
				checkDone.Checked=false;
				labelMinutes.Text="";
				return;
			}
			if(Patients.Cur.NextAptNum==0){
				ApptNext.Visible=false;
				checkDone.Checked=false;
				labelMinutes.Text="";
				return;
			}
			if(Patients.Cur.NextAptNum==-1){
				ApptNext.Visible=false;
				checkDone.Checked=true;
				labelMinutes.Text="";
				return;
			}
			checkDone.Checked=false;
			//MessageBox.Show
			Appointments.RefreshCur(Patients.Cur.NextAptNum);
			if(Appointments.Cur.AptNum==0){//next appointment not found
				Patient PatCur=Patients.Cur;
				PatCur.NextAptNum=0;
				Patients.Cur=PatCur;
				Patients.UpdateCur();//no need to refresh
				Patients.CurOld=Patients.Cur;//because no refresh
				ApptNext.Visible=false;
				checkDone.Checked=false;
				labelMinutes.Text="";
				return;
			}
			ApptNext.Info.MyApt=Appointments.Cur;
			string hasIns="";
			if(Patients.Cur.PriPlanNum!=0) hasIns="i";
			ApptNext.Info.CreditAndIns=Patients.Cur.CreditType+hasIns;
			ApptNext.Info.Lines=new string[ApptNext.Info.MyApt.Pattern.Length];
			ApptNext.Info.Lines[0]=Patients.GetCurNameLF();
			Procedures.GetProcsForSingle(ApptNext.Info.MyApt.AptNum,true);
			int nextLine=1;
			for(int j=0;j<Procedures.ProcsForSingle.Length;j++){
				if(j+nextLine<ApptNext.Info.Lines.Length)
					ApptNext.Info.Lines[j+nextLine]=Procedures.ProcsForSingle[j];
			}
			ArrayList AListNote=new ArrayList();
			Graphics grfx=this.CreateGraphics();
			int iChars=0;
			int iLines=0;
			Font myFont=new Font("Small Font",7f);
			RectangleF rectf 
				=new Rectangle(0,0
				,System.Convert.ToInt32((110-12)/.95)//.95 is arbitrary
				,(int)myFont.GetHeight(grfx));
			string remainNote = ApptNext.Info.MyApt.Note;
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
			}//end while
			grfx.Dispose();
			int noteLine=0;
			for (int j=nextLine+Procedures.ProcsForSingle.Length;
				j<nextLine+Procedures.ProcsForSingle.Length+AListNote.Count;j++){
				if(j<ApptNext.Info.Lines.Length){
					ApptNext.Info.Lines[j]=(string)AListNote[noteLine];
					noteLine++;
				}
			}
			ApptNext.SetSize();
			ApptNext.Width=114;
			ApptNext.CreateShadow();
			ApptNext.Visible=true;
			ApptNext.Refresh();
			labelMinutes.Text=ApptNext.Info.MyApt.Pattern.Length.ToString()+"0 minutes";
			//ContrApptSingle.ApptIsSelected=false;
		}

		private void FillMedical(){
			panelMedical.BackColor=Defs.Long[(int)DefCat.MiscColors][3].ItemColor;
			if(!Patients.PatIsLoaded){
				textMedUrgNote.Text="";
				textMedical.Text="";
				textService.Text="";
				textTreatmentNotes.Text="";
				textTreatmentNotes.Enabled=false;
				panelABCins.Enabled=false;
				textCreditType.Text="";
				textBillingType.Text="";
				textReferral.Text="";
				textDateFirstVisit.Text="";
				textIns.Text="";
				return;
			}
			textMedUrgNote.Text=Patients.Cur.MedUrgNote;
			textMedical.Text=PatientNotes.Cur.Medical;
			textService.Text=PatientNotes.Cur.Service;
			textTreatmentNotes.Text=PatientNotes.Cur.Treatment;
			textTreatmentNotes.Enabled=true;
			textTreatmentNotes.Select(textTreatmentNotes.Text.Length+2,1);
			textTreatmentNotes.ScrollToCaret();
			TreatmentNoteChanged=false;
			panelABCins.Enabled=true;
			textCreditType.Text=Patients.Cur.CreditType;
			textBillingType.Text=Defs.GetName(DefCat.BillingTypes,Patients.Cur.BillingType);
			textReferral.Text="";
			for(int i=0;i<RefAttaches.List.Length;i++){
				if(RefAttaches.List[i].IsFrom){
					Referrals.GetCur(RefAttaches.List[i].ReferralNum);
					textReferral.Text=Referrals.GetCurName();
					break;
				}				
			}
			if(textReferral.Text==""){
				textReferral.Text="referral ??";
			}
			if(Patients.Cur.DateFirstVisit.Year<1880)
				textDateFirstVisit.Text="date ??";
			else if(Patients.Cur.DateFirstVisit==DateTime.Today)
				textDateFirstVisit.Text="NEW PAT";
			else
				textDateFirstVisit.Text=Patients.Cur.DateFirstVisit.ToShortDateString();
			textIns.Text="";
			string name="";
			if(Patients.Cur.PriPlanNum!=0)
				name=InsPlans.GetCarrierName(Patients.Cur.PriPlanNum);
			if(name.Length>20)
				name=name.Substring(0,20)+"...";
			textIns.Text+=name+" ";
			if(Patients.Cur.PriPending)
				textIns.Text+="(pending) ";
			name="";
			if(Patients.Cur.SecPlanNum!=0)
				name=InsPlans.GetCarrierName(Patients.Cur.SecPlanNum);
			if(name.Length>20)
				name=name.Substring(0,20)+"...";
			textIns.Text+=name+" ";
			if(Patients.Cur.SecPending)
				textIns.Text+="(pending)";
			if(textIns.Text=="  ")
				textIns.Text="No Insurance";
		}

		private void textTreatmentNotes_TextChanged(object sender, System.EventArgs e) {
			TreatmentNoteChanged=true;
		}

		private void textTreatmentNotes_Leave(object sender, System.EventArgs e) {
			//need to skip this if selecting another module. Handled in ModuleUnselected due to click event
			if(Patients.FamilyList==null)
				return;
			if(TreatmentNoteChanged){
				PatientNotes.Cur.Treatment=textTreatmentNotes.Text;
				PatientNotes.UpdateCur();
				TreatmentNoteChanged=false;
			}
		}

		private void FillProgNotes(){
			cTeeth.ClearProcs();
			if(!Patients.PatIsLoaded){
				tbProg.ResetRows(0);
				tbProg.LayoutTables();
				cTeeth.DrawShadow();
				return;
			}
			tbProg.SelectedRows=new int[0];
			Procedures.Refresh();
			RxPats.Refresh();
			ProcAL = new ArrayList();
			RxAL = new ArrayList();
			hiLightedRows=new int[0];
			//step through all procedures for patient and move selected ones to
			//ProcAL and RxAL arrays as intermediate, each ordered by date.
			//Pull from both into ProgLineAL array for display,
			//and draw teeth graphics by sending Procedure array directly to cTeeth.
			//Every ProgLineAL entry contains type and index to access original array.
			for(int i=0;i<Procedures.List.Length;i++){
				switch(Procedures.List[i].ProcStatus){
					case ProcStat.TP :
						if (checkShowTP.Checked){
							ProcAL.Add(Procedures.List[i]);
						}
						break;
					case ProcStat.C :
						if (checkShowC.Checked){
							ProcAL.Add(Procedures.List[i]);
						}
						break;
					case ProcStat.EC :
						if (checkShowE.Checked){
							ProcAL.Add(Procedures.List[i]);
						}
						break;
					case ProcStat.EO :
						if (checkShowE.Checked){
							ProcAL.Add(Procedures.List[i]);
						}
						break;
					case ProcStat.R :
						if (checkShowR.Checked){
							ProcAL.Add(Procedures.List[i]);
						}
						break;
				}
			}//for i
			cTeeth.DrawProcs(ProcAL);
			cTeeth.DrawShadow();
			for(int i=0;i<RxPats.List.Length;i++){
				if(checkRx.Checked){
					RxAL.Add(RxPats.List[i]);
				}
			}
			//This is where to convert ProcAL, RxAL to ProgLineAL:
			int tempCountProc=0;
			int tempCountRx=0;
			ProgLineAL = new ArrayList();
			DateTime lineDate=DateTime.MinValue;
			ProgLine tempProgLine;
			int j=0;//using while instead of for because length of notes is unknown
			//j always == sum(tempCount..)
			//MessageBox.Show(ProcAL.Count+","+RxAL.Count);
			while(j<(ProcAL.Count+RxAL.Count)){
				//set lineDate to the value of the first array that is not maxed out:
				if(tempCountProc < ProcAL.Count)
					lineDate=((Procedure)ProcAL[tempCountProc]).ProcDate;
				else if(tempCountRx < RxAL.Count)
					lineDate=((RxPat)RxAL[tempCountRx]).RxDate;
				//find next date
				if(tempCountProc<ProcAL.Count 
					&& DateTime.Compare(((Procedure)ProcAL[tempCountProc]).ProcDate,lineDate)<=0)
					lineDate=((Procedure)ProcAL[tempCountProc]).ProcDate;
				if(tempCountRx<RxAL.Count && DateTime.Compare(((RxPat)RxAL[tempCountRx]).RxDate,lineDate)<0)
					lineDate=((RxPat)RxAL[tempCountRx]).RxDate;	
				//1.Procedure:
				if(tempCountProc<ProcAL.Count 
					&& DateTime.Compare(((Procedure)ProcAL[tempCountProc]).ProcDate,lineDate)==0){
					//always give procedure line:
					tempProgLine=new ProgLine();
					tempProgLine.Type=ProgType.Proc;
					tempProgLine.IsNote=false;
					tempProgLine.Index=tempCountProc;
					tempProgLine.Date=((Procedure)ProcAL[tempCountProc]).ProcDate.ToString("d");
					tempProgLine.Th=Tooth.ToInternat(((Procedure)ProcAL[tempCountProc]).ToothNum);
					tempProgLine.Surf=((Procedure)ProcAL[tempCountProc]).Surf;
					tempProgLine.Dx=Defs.GetValue(DefCat.Diagnosis,((Procedure)ProcAL[tempCountProc]).Dx);
					tempProgLine.Description
						=ProcedureCodes.GetProcCode(((Procedure)ProcAL[tempCountProc]).ADACode).Descript;
					switch (((Procedure)ProcAL[tempCountProc]).ProcStatus){
						case ProcStat.TP :
							tempProgLine.Stat=Lan.g("enumProcStat","TP");
							tempProgLine.LineColor=Defs.Long[(int)DefCat.ProgNoteColors][0].ItemColor;
							break;
						case ProcStat.C :
							tempProgLine.Stat=Lan.g("enumProcStat","C");
							tempProgLine.LineColor=Defs.Long[(int)DefCat.ProgNoteColors][1].ItemColor;
							break;
						case ProcStat.EC :
							tempProgLine.Stat=Lan.g("enumProcStat","EC");
							tempProgLine.LineColor=Defs.Long[(int)DefCat.ProgNoteColors][2].ItemColor;
							break;
						case ProcStat.EO :
							tempProgLine.Stat=Lan.g("enumProcStat","EO");
							tempProgLine.LineColor=Defs.Long[(int)DefCat.ProgNoteColors][3].ItemColor;
							break;
						case ProcStat.R :
							tempProgLine.Stat=Lan.g("enumProcStat","R");
							tempProgLine.LineColor=Defs.Long[(int)DefCat.ProgNoteColors][4].ItemColor;
							break;
					}//end switch ProcStatus
					tempProgLine.Prov=Providers.GetAbbr(((Procedure)ProcAL[tempCountProc]).ProvNum);
					tempProgLine.Amount=((Procedure)ProcAL[tempCountProc]).ProcFee.ToString("F");
					ProgLineAL.Add(tempProgLine);
					//Next section is for the procedure note only/////////////
					tempProgLine=new ProgLine();
					Graphics grfx=this.CreateGraphics();
					int iChars=0;
					int iLines=0;
					Font myFont=new Font("Microsoft Sans Serif",8.5f);
					RectangleF rectf = new Rectangle(0,0,(int)tbProg.NoteWidth,(int)myFont.GetHeight(grfx));
					if (checkNotes.Checked 
						&& tempCountProc<ProcAL.Count && ((Procedure)ProcAL[tempCountProc]).ProcNote!=""){
						string remainNote=((Procedure)ProcAL[tempCountProc]).ProcNote;
						int overrunstop=0;//this handles unanticipated characters from conversions
						while(remainNote.Length>0 && overrunstop<20){
							overrunstop++;
							tempProgLine.IsNote=true;
							tempProgLine.Type=ProgType.Proc;
							tempProgLine.Index=tempCountProc;
							tempProgLine.LineColor=((ProgLine)ProgLineAL[ProgLineAL.Count-1]).LineColor;
							grfx.MeasureString(remainNote,myFont,rectf.Size,new StringFormat(),out iChars, out iLines);
							if(iChars!=remainNote.Length)
								while(iChars!=0 && remainNote.Substring(iChars-1,1)!=" "//last char not space 
									&& remainNote.Substring(iChars-1,1)!="\r" ){//last char not return
									iChars=iChars-1;
								}
							if(iChars==0){//this will wrap even if no spaces at all, whether short or very long
								grfx.MeasureString
									(remainNote,myFont,rectf.Size,new StringFormat(),out iChars, out iLines);
							}
							tempProgLine.Note=remainNote.Substring(0,iChars);
							//at this point, the last character will generally be a space or a return
							if(tempProgLine.Note.Substring(iChars-1,1)=="\r"){
								try{
									tempProgLine.Note=tempProgLine.Note.Remove(iChars-1,1);
									//seems to need this to prevent extra cr's
									remainNote=remainNote.Substring(iChars+1);
								} 
								catch{
								}
							}
							else{
								remainNote=remainNote.Substring(iChars);//does not include the trailing space or return
							}
							ProgLineAL.Add(tempProgLine);
						}//end while
					}//end if checkNotes.checked
					grfx.Dispose();
					if(tempCountProc<ProcAL.Count){
						tempCountProc++;
						j++;
					}
				}//end if (for Procedures)
				//2. Rx:
				else if(j<=tempCountRx+tempCountProc 
					&& tempCountRx<RxAL.Count 
					&& DateTime.Compare(((RxPat)RxAL[tempCountRx]).RxDate,lineDate)==0){
					tempProgLine=new ProgLine();
					tempProgLine.Type=ProgType.Rx;
					tempProgLine.IsNote=false;
					tempProgLine.Index=tempCountRx;
					tempProgLine.Date=((RxPat)RxAL[tempCountRx]).RxDate.ToString("d");
					tempProgLine.Th="";
					tempProgLine.Surf="";
					tempProgLine.Dx="";
					tempProgLine.Description="Rx - "
						+((RxPat)RxAL[tempCountRx]).Drug+" - #"
						+((RxPat)RxAL[tempCountRx]).Disp;
					tempProgLine.Stat="";
					tempProgLine.LineColor=Defs.Long[(int)DefCat.ProgNoteColors][5].ItemColor;
					tempProgLine.Prov=Providers.GetAbbr(((RxPat)RxAL[tempCountRx]).ProvNum);
					tempProgLine.Amount="";
					ProgLineAL.Add(tempProgLine);
					//Next section is for the rx note only/////////////
					tempProgLine=new ProgLine();
					Graphics grfx=this.CreateGraphics();
					int iChars=0;
					int iLines=0;
					Font myFont=new Font("Microsoft Sans Serif",8.5f);
					RectangleF rectf = new Rectangle(0,0,(int)tbProg.NoteWidth,(int)myFont.GetHeight(grfx));
					if(checkNotes.Checked 
						&& tempCountRx<RxAL.Count && ((RxPat)RxAL[tempCountRx]).Notes!=""){
						string remainNote=((RxPat)RxAL[tempCountRx]).Notes;
						while(remainNote.Length>0){
							tempProgLine.IsNote=true;
							tempProgLine.Type=ProgType.Rx;
							tempProgLine.Index=tempCountRx;
							tempProgLine.LineColor=((ProgLine)ProgLineAL[ProgLineAL.Count-1]).LineColor;
							grfx.MeasureString(remainNote,myFont,rectf.Size,new StringFormat(),out iChars, out iLines);
							if(iChars!=remainNote.Length)
								while(iChars!=0 && remainNote.Substring(iChars-1,1)!=" "//last char not space 
									&& remainNote.Substring(iChars-1,1)!="\r" ){//last char not return
									iChars=iChars-1;
								}
							if(iChars==0){//this will wrap even if no spaces at all, whether short or very long
								grfx.MeasureString
									(remainNote,myFont,rectf.Size,new StringFormat(),out iChars, out iLines);
							}
							tempProgLine.Note=remainNote.Substring(0,iChars);
							//at this point, the last character will generally be a space or a return
							if(tempProgLine.Note.Substring(iChars-1,1)=="\r"){
								try{
									tempProgLine.Note=tempProgLine.Note.Remove(iChars-1,1);
									//seems to need this to prevent extra cr's
									remainNote=remainNote.Substring(iChars+1);
								} 
								catch{
								}
							}
							else{
								remainNote=remainNote.Substring(iChars);//does not include the trailing space or return
							}
							ProgLineAL.Add(tempProgLine);
						}//end while
					}//end if checkNotes.checked
					grfx.Dispose();
					if(tempCountRx<RxAL.Count){
						tempCountRx++;
						j++;
					}
				}
				//MessageBox.Show(j.ToString());
				//else j++;
			}//end for
			
			//Then, fill tbProg from ProgLineAL:
				tbProg.ResetRows(ProgLineAL.Count);
				for(int i=0;i<ProgLineAL.Count;i++){
					if(!((ProgLine)ProgLineAL[i]).IsNote){
						tbProg.SetProcRow(i);
						tbProg.Cell[0,i]=((ProgLine)ProgLineAL[i]).Date;
						tbProg.Cell[1,i]=((ProgLine)ProgLineAL[i]).Th;
						tbProg.Cell[2,i]=((ProgLine)ProgLineAL[i]).Surf;
						tbProg.Cell[3,i]=((ProgLine)ProgLineAL[i]).Dx;
						tbProg.Cell[4,i]=((ProgLine)ProgLineAL[i]).Description;
						tbProg.Cell[5,i]=((ProgLine)ProgLineAL[i]).Stat;
						tbProg.Cell[6,i]=((ProgLine)ProgLineAL[i]).Prov;
						tbProg.Cell[7,i]=((ProgLine)ProgLineAL[i]).Amount;
					}//end if
					else{//else note
						if(!((ProgLine)ProgLineAL[i-1]).IsNote)
							tbProg.SetFirstNoteRow(i);
						else
							tbProg.SetNoteRow(i);
						tbProg.Cell[2,i]=((ProgLine)ProgLineAL[i]).Note;
					}//end else note
					tbProg.SetTextColorRow(i,((ProgLine)ProgLineAL[i]).LineColor);
				}//end for
				tbProg.LayoutTables();
		}//end FillProgNotes

    private void FillDxAndProcLists(){
			listDx.Items.Clear();
			for(int i=0;i<Defs.Short[(int)DefCat.Diagnosis].Length;i++){//move to instantClasses?
				this.listDx.Items.Add(Defs.Short[(int)DefCat.Diagnosis][i].ItemName);
			}
			int selectedPriority=comboPriority.SelectedIndex;//retain current selection
			comboPriority.Items.Clear();
			comboPriority.Items.Add(Lan.g(this,"no priority"));
			for(int i=0;i<Defs.Short[(int)DefCat.TxPriorities].Length;i++){
				this.comboPriority.Items.Add(Defs.Short[(int)DefCat.TxPriorities][i].ItemName);
			}
			if(selectedPriority>0 && selectedPriority<comboPriority.Items.Count)
				//set the selected to what it was before.
				comboPriority.SelectedIndex=selectedPriority;
			else
				comboPriority.SelectedIndex=0;
				//or just set to no priority
      listProcButtons.Items.Clear();
			for(int i=0;i<ProcButtons.List.Length;i++){
        listProcButtons.Items.Add(ProcButtons.List[i].Description);  
			}   
    }

		private void UpdateSurf (){
			textSurf.Text="";
			if (butM.BackColor==Color.White) textSurf.AppendText("M");
			if (butO.BackColor==Color.White) textSurf.AppendText("O");
			if (butI.BackColor==Color.White) textSurf.AppendText("I");
			if (butD.BackColor==Color.White) textSurf.AppendText("D");
			if (butF.BackColor==Color.White) textSurf.AppendText("F");
			if (butB.BackColor==Color.White) textSurf.AppendText("B");
			if (butL.BackColor==Color.White) textSurf.AppendText("L");
		}

		private void butB_Click(object sender, System.EventArgs e){
			if (butB.BackColor==Color.White){
				butB.BackColor=SystemColors.Control;
			}
			else{
				butB.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void butF_Click(object sender, System.EventArgs e){
			if (butF.BackColor==Color.White){
				butF.BackColor=SystemColors.Control;
			}
			else{
				butF.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void butM_Click(object sender, System.EventArgs e){
			if (butM.BackColor==Color.White){
				butM.BackColor=SystemColors.Control;
			}
			else{
				butM.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void butO_Click(object sender, System.EventArgs e){
			if (butO.BackColor==Color.White){
				butO.BackColor=SystemColors.Control;
			}
			else{
				butO.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void butI_Click(object sender, System.EventArgs e){
			if (butI.BackColor==Color.White){
				butI.BackColor=SystemColors.Control;
			}
			else{
				butI.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void butD_Click(object sender, System.EventArgs e){
			if (butD.BackColor==Color.White){
				butD.BackColor=SystemColors.Control;
			}
			else{
				butD.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void butL_Click(object sender, System.EventArgs e){
			if (butL.BackColor==Color.White){
				butL.BackColor=SystemColors.Control;
			}
			else{
				butL.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void tbProg_CellDoubleClicked(object sender, CellEventArgs e){
			switch(((ProgLine)ProgLineAL[e.Row]).Type){
				case ProgType.Proc:
					Procedures.Cur=((Procedure)ProcAL[((ProgLine)ProgLineAL[e.Row]).Index]);
					Procedures.CurOld=Procedures.Cur;
					FormProcEdit FormPE=new FormProcEdit();
					FormPE.IsNew=false;
					FormPE.ShowDialog();
					if(FormPE.DialogResult!=DialogResult.OK) return;
					break;
				case ProgType.Rx:
					//MessageBox.Show(((ProgLine)ProgLineAL[e.Row]).Index.ToString());
					RxPats.Cur=((RxPat)RxAL[((ProgLine)ProgLineAL[e.Row]).Index]);
					FormRxEdit FormRxE = new FormRxEdit();
					FormRxE.IsNew=false;
					FormRxE.ShowDialog();
					if(FormRxE.DialogResult!=DialogResult.OK) return;
					break;
			}
			ModuleSelected();
			//MessageBox.Show(e.Col.ToString()+","+e.Row.ToString()+" Double Clicked");
		}

		//private void RefreshForm(){
		//	ClearButtons();
		//	FillProgNotes();
		//}

		private void ClearButtons(){
			//unfortuantely, these colors no longer show since the XP button style was introduced.
			butM.BackColor=Color.FromName("Control");;
			butO.BackColor=Color.FromName("Control");
			butI.BackColor=Color.FromName("Control");
			butD.BackColor=Color.FromName("Control");
			butL.BackColor=Color.FromName("Control");
			butB.BackColor=Color.FromName("Control");
			butF.BackColor=Color.FromName("Control");
			textSurf.Text="";
			butB.Text="B";
			butO.Text="O";
			butB.Enabled=true;
			butO.Enabled=true;
			butF.Text="F";
			butI.Text="I";
			butF.Enabled=true;
			butI.Enabled=true;
			listDx.SelectedIndex=-1;
			listProcButtons.SelectedIndex=-1;
			textADACode.Text=Lan.g(this,"Type ADA Code");
		}

		///<summary>Gets the fee schedule from the priinsplan, the patient, or the provider in that order.  Either returns a fee schedule (fk to definition.DefNum) or 0.</summary>
		public static int GetFeeSched(){
			//there's not really a good place to put this function, so it's here.
			int retVal=0;
			if(Patients.Cur.PriPlanNum!=0){
				InsPlans.GetCur(Patients.Cur.PriPlanNum);
				retVal=InsPlans.Cur.FeeSched;
			}
			if(retVal==0){
				if(Patients.Cur.FeeSched!=0){
					retVal=Patients.Cur.FeeSched;
				}
				else{
					if(Patients.Cur.PriProv==0){
						retVal=Providers.List[0].FeeSched;
					}
					else{
            //MessageBox.Show(Providers.GetIndex(Patients.Cur.PriProv).ToString());   
						retVal=Providers.ListLong[Providers.GetIndexLong(Patients.Cur.PriProv)].FeeSched;
					}
				}
			}
			return retVal;
		}

		///<summary>Sets many fields for a new procedure, then displays it for editing before inserting it into the db.</summary>
		private void AddProcedure(){
			Procedure ProcCur=Procedures.Cur;
			//procnum
			ProcCur.PatNum=Patients.Cur.PatNum;
			//aptnum
			//adacode
			ProcCur.ProcDate=DateTime.Today;
			if(newStatus==ProcStat.R || newStatus==ProcStat.EO || newStatus==ProcStat.EC)
				ProcCur.ProcFee=0;
			else
				ProcCur.ProcFee=Fees.GetAmount(ProcCur.ADACode,GetFeeSched());
			ProcCur.OverridePri=-1;
			ProcCur.OverrideSec=-1;
			//surf
			//ToothNum
			//Procedures.Cur.ToothRange
			ProcCur.NoBillIns=ProcedureCodes.GetProcCode(ProcCur.ADACode).NoBillIns;
			if(comboPriority.SelectedIndex==0)
				ProcCur.Priority=0;
			else
				ProcCur.Priority=Defs.Short[(int)DefCat.TxPriorities][comboPriority.SelectedIndex-1].DefNum;
			ProcCur.ProcStatus=newStatus;
			ProcCur.ProcNote="";
			if(ProcedureCodes.GetProcCode(ProcCur.ADACode).IsHygiene
				&& Patients.Cur.SecProv != 0){
				ProcCur.ProvNum=Patients.Cur.SecProv;
			}
			else{
				ProcCur.ProvNum=Patients.Cur.PriProv;
			}
			if(listDx.SelectedIndex!=-1)
				ProcCur.Dx=Defs.Short[(int)DefCat.Diagnosis][listDx.SelectedIndex].DefNum;
			//nextaptnum
			ProcCur.CapCoPay=-1;
			if(Patients.Cur.PriPlanNum!=0){//if patient has insurance
				ProcCur.IsCovIns=true;
				InsPlans.GetCur(Patients.Cur.PriPlanNum);
				if(InsPlans.Cur.PlanType=="c"){
					//also handles fine if copayfeesched=0:
					ProcCur.CapCoPay=Fees.GetAmount(ProcCur.ADACode,InsPlans.Cur.CopayFeeSched);
				}
			}
			Procedures.Cur=ProcCur;
			FormProcEdit FormPE=new FormProcEdit();
			FormPE.IsNew=true;
			FormPE.ShowDialog();
			//insertcur is done in dialog
		}
			
		private void AddQuick(){
			Procedure ProcCur=Procedures.Cur;
			//procnum
			ProcCur.PatNum=Patients.Cur.PatNum;
			//aptnum
			//adacode
			ProcCur.ProcDate=DateTime.Now;
			if(newStatus==ProcStat.R || newStatus==ProcStat.EO || newStatus==ProcStat.EC)
				ProcCur.ProcFee=0;
			else
				ProcCur.ProcFee=Fees.GetAmount(ProcCur.ADACode,GetFeeSched());
			ProcCur.OverridePri=-1;
			ProcCur.OverrideSec=-1;
			//surf
			//toothnum
			//ToothRange
			ProcCur.NoBillIns=ProcedureCodes.GetProcCode(ProcCur.ADACode).NoBillIns;
			if(comboPriority.SelectedIndex==0)
				ProcCur.Priority=0;
			else
				ProcCur.Priority=Defs.Short[(int)DefCat.TxPriorities][comboPriority.SelectedIndex-1].DefNum;
			ProcCur.ProcStatus=newStatus;
			ProcCur.ProcNote="";
			if(ProcedureCodes.GetProcCode(ProcCur.ADACode).IsHygiene
				&& Patients.Cur.SecProv != 0){
				ProcCur.ProvNum=Patients.Cur.SecProv;
			}
			else{
				ProcCur.ProvNum=Patients.Cur.PriProv;
			}
			if(listDx.SelectedIndex!=-1)
				ProcCur.Dx=Defs.Short[(int)DefCat.Diagnosis][listDx.SelectedIndex].DefNum;
			//nextaptnum
			ProcCur.CapCoPay=-1;
			if(Patients.Cur.PriPlanNum!=0){//if patient has insurance
				ProcCur.IsCovIns=true;
				InsPlans.GetCur(Patients.Cur.PriPlanNum);
				if(InsPlans.Cur.PlanType=="c"){
					//also handles fine if copayfeesched=0:
					ProcCur.CapCoPay=Fees.GetAmount(ProcCur.ADACode,InsPlans.Cur.CopayFeeSched);
				}
			}
			//MessageBox.Show(Procedures.NewProcedure.ProcFee.ToString());
			//MessageBox.Show(Procedures.NewProcedure.ProcDate);
			//if(Procedures.Cur.ProcStatus==ProcStat.C){
			//	Procedures.PutBal(Procedures.Cur.ProcDate,Procedures.Cur.ProcFee);
			//}
			Procedures.Cur=ProcCur;
			Procedures.InsertCur();
		}

		private void butAddProc_Click(object sender, System.EventArgs e){
			bool isValid;
			TreatmentArea tArea;
			FormProcedures FormP=new FormProcedures();
			FormP.Mode=FormProcMode.Select;
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK) return;
			Procedures.SetDateFirstVisit(DateTime.Today,1);
			Procedure ProcCur;
			for(int n=0;n==0 || n<cTeeth.SelectedTeeth.Length;n++){
				isValid=true;
				ProcCur=new Procedure();//going to be an insert, so no need to set Procedures.CurOld
				//Procedure
				ProcCur.ADACode = FormP.SelectedADA;
				//Procedures.Cur.ADACode=ProcButtonItems.adaCodeList[i];
				tArea=ProcedureCodes.GetProcCode(ProcCur.ADACode).TreatArea;
				if((tArea==TreatmentArea.Arch
					|| tArea==TreatmentArea.Mouth
					|| tArea==TreatmentArea.Quad
					|| tArea==TreatmentArea.Sextant
					|| tArea==TreatmentArea.ToothRange)
					&& n>0){//the only two left are tooth and surf
					continue;//only entered if n=0, so they don't get entered more than once.
				}
				else if(tArea==TreatmentArea.Quad){
				//	switch(quadCount){
				//		case 0: Procedures.Cur.Surf="UR"; break;
				//		case 1: Procedures.Cur.Surf="UL"; break;
				//		case 2: Procedures.Cur.Surf="LL"; break;
				//		case 3: Procedures.Cur.Surf="LR"; break;
				//		default: Procedures.Cur.Surf="UR"; break;//this could happen.
				//	}
				//	quadCount++;
				//	AddQuick();
					Procedures.Cur=ProcCur;
					AddProcedure();
				}
				else if(tArea==TreatmentArea.Surf){
					if(textSurf.Text=="")
						isValid=false;
					else
						ProcCur.Surf=textSurf.Text;
					if(cTeeth.SelectedTeeth.Length==0)
						isValid=false;
					else
						ProcCur.ToothNum=cTeeth.SelectedTeeth[n];
					Procedures.Cur=ProcCur;
					if(isValid)
						AddQuick();
					else
						AddProcedure();
				}
				else if(tArea==TreatmentArea.Tooth){
					if(cTeeth.SelectedTeeth.Length==0){
						Procedures.Cur=ProcCur;
						AddProcedure();
					}
					else{
						ProcCur.ToothNum=cTeeth.SelectedTeeth[n];
						Procedures.Cur=ProcCur;
						AddQuick();
					}
				}
				else if(tArea==TreatmentArea.ToothRange){
					if(cTeeth.SelectedTeeth.Length==0){
						Procedures.Cur=ProcCur;
						AddProcedure();
					}
					else{
						ProcCur.ToothRange="";
						for(int b=0;b<cTeeth.SelectedTeeth.Length;b++){
							if(b!=0) ProcCur.ToothRange+=",";
							ProcCur.ToothRange+=cTeeth.SelectedTeeth[b];
						}
						Procedures.Cur=ProcCur;
						AddProcedure();//it's nice to see the procedure to verify the range
					}
				}
				else if(tArea==TreatmentArea.Arch){
					if(cTeeth.SelectedTeeth.Length==0){
						Procedures.Cur=ProcCur;
						AddProcedure();
						continue;
					}
					if(Tooth.IsMaxillary(cTeeth.SelectedTeeth[0])){
						ProcCur.Surf="U";
					}
					else{
						ProcCur.Surf="L";
					}
					Procedures.Cur=ProcCur;
					AddQuick();
				}
				else if(tArea==TreatmentArea.Sextant){
					Procedures.Cur=ProcCur;
					AddProcedure();
				}
				else{//mouth
					Procedures.Cur=ProcCur;
					AddQuick();
				}
			}//for n
			ModuleSelected();
		}
		
		private void listDx_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//newDx=Defs.Defns[(int)DefCat.Diagnosis][listDx.IndexFromPoint(e.X,e.Y)].DefNum;
		}

		private void tbProg_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if(e.KeyCode==Keys.Delete || e.KeyCode==Keys.Back){
				DeleteRows();
			}
		}

		private void butDel_Click(object sender, System.EventArgs e) {
			DeleteRows();
		}

		private void DeleteRows(){
			if (tbProg.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			if (MessageBox.Show(Lan.g(this,"Delete Selected Item(s)?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
				return;
			int skipped=0;
			int rxSkipped=0;
			for(int i=0;i<tbProg.SelectedIndices.Length;i++){
				switch(((ProgLine)ProgLineAL[tbProg.SelectedIndices[i]]).Type){
					case ProgType.Proc:
						if (((Procedure)ProcAL[((ProgLine)ProgLineAL[tbProg.SelectedIndices[i]]).Index]).ProcStatus==ProcStat.C){
							skipped++;
						}
						else{
							Procedures.Cur=(Procedure)ProcAL[((ProgLine)ProgLineAL[tbProg.SelectedIndices[i]]).Index];
							Procedures.DeleteCur();
						}
						break;
					case ProgType.Rx:		
						RxPats.Cur=(RxPat)RxAL[((ProgLine)ProgLineAL[tbProg.SelectedIndices[i]]).Index];
						if(UserPermissions.CheckUserPassword("Prescription Edit",RxPats.Cur.RxDate)){		
							RxPats.DeleteCur();
						}
						else{
							rxSkipped++;
						}
						break;
				}//switch
			}
			if(skipped>0){
				MessageBox.Show(Lan.g(this,"Not allowed to delete completed procedures from here.")+"\r"
					+skipped.ToString()+" "+Lan.g(this,"item(s) skipped."));
			}
			if(rxSkipped>0){
				MessageBox.Show(Lan.g(this,"You do not have permission to delete prescriptions."));	
			}
			ModuleSelected();
		}

		private void button1_Click(object sender, System.EventArgs e) {
			//sometimes used for testing purposes
		}

		/*private void tbProg_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (e.KeyCode==Keys.ControlKey){
				ControlDown=true;
			}
		}

		private void tbProg_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (e.KeyCode==Keys.ControlKey){
				ControlDown=false;
			}
		}*/

		private void checkShowTP_Click(object sender, System.EventArgs e) {
			FillProgNotes();
		}

		private void checkShowC_Click(object sender, System.EventArgs e) {
			FillProgNotes();
		}

		private void checkShowE_Click(object sender, System.EventArgs e) {
			FillProgNotes();
		}

		private void checkShowR_Click(object sender, System.EventArgs e) {
			FillProgNotes();
		}

		private void checkNotes_Click(object sender, System.EventArgs e) {
			FillProgNotes();
		}

		private void checkRx_Click(object sender, System.EventArgs e) {
			FillProgNotes();
		}

		private void butShowAll_Click(object sender, System.EventArgs e) {
			checkShowTP.Checked=true;
			checkShowC.Checked=true;
			checkShowE.Checked=true;
			checkShowR.Checked=true;
			checkNotes.Checked=true;
			checkRx.Checked=true;
			FillProgNotes();
		}

		private void butShowNone_Click(object sender, System.EventArgs e) {
			checkShowTP.Checked=false;
			checkShowC.Checked=false;
			checkShowE.Checked=false;
			checkShowR.Checked=false;
			checkNotes.Checked=false;
			checkRx.Checked=false;
			FillProgNotes();
		}

		private void radioEntryEO_CheckedChanged(object sender, System.EventArgs e) {
			newStatus=ProcStat.EO;
		}

		private void radioEntryEC_CheckedChanged(object sender, System.EventArgs e) {
			newStatus=ProcStat.EC;
		}

		private void radioEntryTP_CheckedChanged(object sender, System.EventArgs e) {
			newStatus=ProcStat.TP;
		}

		private void radioEntryC_CheckedChanged(object sender, System.EventArgs e) {
			newStatus=ProcStat.C;
		}

		private void radioEntryR_CheckedChanged(object sender, System.EventArgs e) {
			newStatus=ProcStat.R;
		}

		private void butMedicalService_Click(object sender, System.EventArgs e) {
			FormMedical FormMedical2=new FormMedical();
			FormMedical2.ShowDialog();
			FillMedical();
		}

		private void checkDone_Click(object sender, System.EventArgs e) {
			Patient PatCur;
			if(checkDone.Checked){
				if(ApptNext.Visible){
					if(MessageBox.Show(Lan.g(this,"Existing next appointment will be deleted"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
						return; 
					Appointments.Cur=ApptNext.Info.MyApt;
					Procedures.UnattachProcsInNextAppt(Appointments.Cur.AptNum);
					Appointments.DeleteCur();
				}
				PatCur=Patients.Cur;
				PatCur.NextAptNum=-1;
				Patients.Cur=PatCur;
				Patients.UpdateCur();
			}
			else{
				PatCur=Patients.Cur;
				PatCur.NextAptNum=0;
				Patients.Cur=PatCur;
				Patients.UpdateCur();
			}
			FillNext();
		}

		private void butNew_Click(object sender, System.EventArgs e) {
			if(ApptNext.Visible){
				if(MessageBox.Show(Lan.g(this,"Replace existing next appointment?")
					,"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
					return;
				Appointments.Cur=ApptNext.Info.MyApt;
				Procedures.UnattachProcsInNextAppt(Appointments.Cur.AptNum);
				Appointments.DeleteCur();
			}
			Appointments.Cur=new Appointment();
			Appointments.Cur.PatNum=Patients.Cur.PatNum;
			Appointments.Cur.ProvNum=Patients.Cur.PriProv;
			Appointments.Cur.AptStatus=ApptStatus.Next;
			Appointments.Cur.AptDateTime=DateTime.Today;
			Appointments.Cur.Pattern="/X/";
			Appointments.InsertCur();
			Appointments.CurOld=Appointments.Cur;
			FormApptEdit FormApptEdit2=new FormApptEdit();
			FormApptEdit2.IsNew=true;
			FormApptEdit2.ShowDialog();
			if(FormApptEdit2.DialogResult!=DialogResult.OK){
				//delete new appt and unattach procs already handled in dialog
				//not needed: Patients.Cur.NextAptNum=0;
				FillNext();
				return;
			}
			Patient PatCur=Patients.Cur;
			PatCur.NextAptNum=Appointments.Cur.AptNum;
			Patients.Cur=PatCur;
			Patients.UpdateCur();
			ModuleSelected();//if procs were added in appt, then this will display them
		}

		private void butClear_Click(object sender, System.EventArgs e) {
			if(!ApptNext.Visible){
				MessageBox.Show(Lan.g(this,"No next appointment is present."));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete Next Appointment?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
				return;
			Appointments.Cur=ApptNext.Info.MyApt;
			Procedures.UnattachProcsInNextAppt(Appointments.Cur.AptNum);
			Appointments.DeleteCur();
			Patient PatCur=Patients.Cur;
			PatCur.NextAptNum=0;
			Patients.Cur=PatCur;
			Patients.UpdateCur();
			FillNext();
		}

		private void ApptNext_DoubleClick(object sender, System.EventArgs e){
			Appointments.Cur=ApptNext.Info.MyApt;
			Appointments.CurOld=Appointments.Cur;
			FormApptEdit FormApptEdit2 = new FormApptEdit();
			FormApptEdit2.ShowDialog();
			ModuleSelected();//if procs were added in appt, then this will display them
		}

		private void butEnterTx_Click(object sender, System.EventArgs e) {
			OnClickEnterTx();
		}

		private void OnClickEnterTx(){
			if(panelEnterTx.Visible){
				panelEnterTx.Visible=false;
				tbProg.Location=panelEnterTx.Location;
				tbProg.Height=this.ClientSize.Height-tbProg.Location.Y-2;
				tbProg.LayoutTables();
				butEnterTx.ImageIndex=1;//down arrow
			}
			else{
				panelEnterTx.Visible=true;
				tbProg.Location=new Point(panelEnterTx.Location.X,panelEnterTx.Location.Y+panelEnterTx.Height+2);
				tbProg.Height=this.ClientSize.Height-tbProg.Location.Y-2;
				tbProg.LayoutTables();
				butEnterTx.ImageIndex=0;//up arrow
			}
		}

		/*private bool AddNewProc(string adaCode,string toothNum,string surf,string range){
			MessageBox.Show(adaCode+","+toothNum+","+surf+","+range);
			return;
			bool invalid=false;
			bool needSurf=false;
			bool needTooth=false;
			//procnum
			Procedures.Cur.PatNum=Patients.Cur.PatNum;
			//aptnum
			Procedures.Cur.ADACode=adaCode;
			Procedures.Cur.ProcDate=DateTime.Now;
			Procedures.Cur.ProcFee = Fees.GetAmount(Procedures.Cur.ADACode,GetFeeSched());
			Procedures.Cur.OverridePri=-1;
			Procedures.Cur.OverrideSec=-1;
			Procedures.Cur.Surf=surf;
			Procedures.Cur.ToothNum=toothNum;
			if(((ProcedureCode)ProcCodes.HList[Procedures.Cur.ADACode]).TreatArea==TreatmentArea.Tooth
				|| ((ProcedureCode)ProcCodes.HList[Procedures.Cur.ADACode]).TreatArea==TreatmentArea.Surf){
				if(newToothNum2[0] != null)
					Procedures.Cur.ToothNum=newToothNum2[0];
			}
			//string range="";
			//if(((ProcedureCode)ProcCodes.HList[Procedures.Cur.ADACode]).TreatArea==TreatmentArea.ToothRange){
			//	for(int i=0;i<newToothNum2.Length;i++){
			//		range+=newToothNum2[i]+","; 
			//	}
			//}
			Procedures.Cur.ToothRange=range;
			Procedures.Cur.NoBillIns=ProcCodes.GetProcCode(Procedures.Cur.ADACode).NoBillIns;
			//priority
			Procedures.Cur.ProcStatus=newStatus;
			Procedures.Cur.ProcNote="";
			//Procedures.Cur.PriEstim=
			//Procedures.Cur.SecEstim=
			//claimnum
			Procedures.Cur.ProvNum=Patients.Cur.PriProv;
			if(listDx.SelectedIndex!=-1)
				Procedures.Cur.Dx=Defs.Short[(int)DefCat.Diagnosis][listDx.SelectedIndex].DefNum;
			//nextaptnum
			if(Patients.Cur.PriPlanNum!=0){//if patient has insurance
				Procedures.Cur.IsCovIns=true;
			}
			FormProcEdit FormPE = new FormProcEdit();
			FormPE.IsNew=true;
			FormPE.ShowDialog();
			//insertcur is done in dialog
		}*/

		private void listProcButtons_Click(object sender, System.EventArgs e) {
			Procedures.SetDateFirstVisit(DateTime.Today,1);
			bool isValid;
			TreatmentArea tArea;
			int quadCount=0;//automates quadrant codes.
		  ProcButtons.Cur=ProcButtons.List[listProcButtons.SelectedIndex];
			ProcButtonItems.GetListsForButton(ProcButtons.Cur.ProcButtonNum);
			Procedure ProcCur;
			for(int i=0;i<ProcButtonItems.adaCodeList.Length;i++){
				//needs to loop at least once, regardless of whether any teeth are selected.	
				for(int n=0;n==0 || n<cTeeth.SelectedTeeth.Length;n++){
					isValid=true;
					ProcCur=new Procedure();//insert, so no need to set CurOld
					ProcCur.ADACode=ProcButtonItems.adaCodeList[i];
					tArea=ProcedureCodes.GetProcCode(ProcCur.ADACode).TreatArea;
					if((tArea==TreatmentArea.Arch
						|| tArea==TreatmentArea.Mouth
						|| tArea==TreatmentArea.Quad
						|| tArea==TreatmentArea.Sextant
						|| tArea==TreatmentArea.ToothRange)
						&& n>0){//the only two left are tooth and surf
						continue;//only entered if n=0, so they don't get entered more than once.
					}
					else if(tArea==TreatmentArea.Quad){
						switch(quadCount){
							case 0: ProcCur.Surf="UR"; break;
							case 1: ProcCur.Surf="UL"; break;
							case 2: ProcCur.Surf="LL"; break;
							case 3: ProcCur.Surf="LR"; break;
							default: ProcCur.Surf="UR"; break;//this could happen.
						}
						quadCount++;
						Procedures.Cur=ProcCur;
						AddQuick();
					}
					else if(tArea==TreatmentArea.Surf){
						if(textSurf.Text=="")
							isValid=false;
						else
							ProcCur.Surf=textSurf.Text;
						if(cTeeth.SelectedTeeth.Length==0)
							isValid=false;
						else
							ProcCur.ToothNum=cTeeth.SelectedTeeth[n];
						Procedures.Cur=ProcCur;
						if(isValid)
							AddQuick();
						else
							AddProcedure();
					}
					else if(tArea==TreatmentArea.Tooth){
						if(cTeeth.SelectedTeeth.Length==0){
							Procedures.Cur=ProcCur;
							AddProcedure();
						}
						else{
							ProcCur.ToothNum=cTeeth.SelectedTeeth[n];
							Procedures.Cur=ProcCur;
							AddQuick();
						}
					}
					else if(tArea==TreatmentArea.ToothRange){
						if(cTeeth.SelectedTeeth.Length==0){
							Procedures.Cur=ProcCur;
							AddProcedure();
						}
						else{
							ProcCur.ToothRange="";
							for(int b=0;b<cTeeth.SelectedTeeth.Length;b++){
								if(b!=0) ProcCur.ToothRange+=",";
								ProcCur.ToothRange+=cTeeth.SelectedTeeth[b];
							}
							Procedures.Cur=ProcCur;
							AddQuick();
						}
					}
					else if(tArea==TreatmentArea.Arch){
						if(cTeeth.SelectedTeeth.Length==0){
							Procedures.Cur=ProcCur;
							AddProcedure();
							continue;
						}
						if(Tooth.IsMaxillary(cTeeth.SelectedTeeth[0])){
							ProcCur.Surf="U";
						}
						else{
							ProcCur.Surf="L";
						}
						Procedures.Cur=ProcCur;
						AddQuick();
					}
					else if(tArea==TreatmentArea.Sextant){
						Procedures.Cur=ProcCur;
						AddProcedure();
					}
					else{//mouth
						Procedures.Cur=ProcCur;
						AddQuick();
					}
				}//n selected teeth
			}//end Part 1 checking for AdaCodes, now will check for AutoCodes
			string toothNum;
			string surf;
			bool isAdditional;
			for(int i=0;i<ProcButtonItems.autoCodeList.Length;i++){
				for(int n=0;n==0 || n<cTeeth.SelectedTeeth.Length;n++){
					isValid=true;
					if(cTeeth.SelectedTeeth.Length!=0)
						toothNum=cTeeth.SelectedTeeth[n];
					else
						toothNum="";
					surf=textSurf.Text;
					isAdditional= n!=0;
					ProcCur=new Procedure();//this will be an insert, so no need to set CurOld
					ProcCur.ADACode=AutoCodeItems.GetADA
							(ProcButtonItems.autoCodeList[i],toothNum
							,surf,isAdditional);
					tArea=ProcedureCodes.GetProcCode(ProcCur.ADACode).TreatArea;
					if((tArea==TreatmentArea.Arch
						|| tArea==TreatmentArea.Mouth
						|| tArea==TreatmentArea.Quad
						|| tArea==TreatmentArea.Sextant
						|| tArea==TreatmentArea.ToothRange)
						&& n>0){//the only two left are tooth and surf
						continue;//only entered if n=0, so they don't get entered more than once.
					}
					else if(tArea==TreatmentArea.Quad){
						switch(quadCount){
							case 0: ProcCur.Surf="UR"; break;
							case 1: ProcCur.Surf="UL"; break;
							case 2: ProcCur.Surf="LL"; break;
							case 3: ProcCur.Surf="LR"; break;
							default: ProcCur.Surf="UR"; break;//this could happen.
						}
						quadCount++;
						Procedures.Cur=ProcCur;
						AddQuick();
					}
					else if(tArea==TreatmentArea.Surf){
						if(textSurf.Text=="")
							isValid=false;
						else
							ProcCur.Surf=textSurf.Text;
						if(cTeeth.SelectedTeeth.Length==0)
							isValid=false;
						else
							ProcCur.ToothNum=cTeeth.SelectedTeeth[n];
						Procedures.Cur=ProcCur;
						if(isValid)
							AddQuick();
						else
							AddProcedure();
					}
					else if(tArea==TreatmentArea.Tooth){
						if(cTeeth.SelectedTeeth.Length==0){
							Procedures.Cur=ProcCur;
							AddProcedure();
						}
						else{
							ProcCur.ToothNum=cTeeth.SelectedTeeth[n];
							Procedures.Cur=ProcCur;
							AddQuick();
						}
					}
					else if(tArea==TreatmentArea.ToothRange){
						if(cTeeth.SelectedTeeth.Length==0){
							Procedures.Cur=ProcCur;
							AddProcedure();
						}
						else{
							ProcCur.ToothRange="";
							for(int b=0;b<cTeeth.SelectedTeeth.Length;b++){
								if(b!=0) ProcCur.ToothRange+=",";
								ProcCur.ToothRange+=cTeeth.SelectedTeeth[b];
							}
							Procedures.Cur=ProcCur;
							AddQuick();
						}
					}
					else if(tArea==TreatmentArea.Arch){
						if(cTeeth.SelectedTeeth.Length==0){
							Procedures.Cur=ProcCur;
							AddProcedure();
							continue;
						}
						if(Tooth.IsMaxillary(cTeeth.SelectedTeeth[0])){
							ProcCur.Surf="U";
						}
						else{
							ProcCur.Surf="L";
						}
						Procedures.Cur=ProcCur;
						AddQuick();
					}
					else if(tArea==TreatmentArea.Sextant){
						Procedures.Cur=ProcCur;
						AddProcedure();
					}
					else{//mouth
						Procedures.Cur=ProcCur;
						AddQuick();
					}
				}//n selected teeth
			}//for i
			ModuleSelected();
		}

		private void butPrimary_Click(object sender, System.EventArgs e) {
			//contextMenu1.Show(butPrimary,new Point(0,30));
		}

		private void menuPrimaryAll_Click(object sender, System.EventArgs e) {
			Patient PatCur=Patients.Cur;
			PatCur.PrimaryTeeth="";
			for(int i=1;i<=32;i++){
				PatCur.PrimaryTeeth+=i.ToString()+",";
			}
			Patients.Cur=PatCur;
			Patients.UpdateCur();
			ModuleSelected();
			//RefreshModuleScreen();
		}

		private void menuPrimaryNone_Click(object sender, System.EventArgs e) {
			Patient PatCur=Patients.Cur;
			PatCur.PrimaryTeeth="";
			Patients.Cur=PatCur;
			Patients.UpdateCur();
			ModuleSelected();
			//RefreshModuleScreen();
		}

		private void menuPrimaryToggle_Click(object sender, System.EventArgs e) {
			if(cTeeth.SelectedTeeth.Length==0){
				return;
			}
			string[] oldPriTeeth=Patients.Cur.PrimaryTeeth.Split(',');
			ArrayList ALpri=new ArrayList();
			for(int i=0;i<oldPriTeeth.Length;i++){
				if(Tooth.IsValidDB(oldPriTeeth[i])
					&& !Tooth.IsPrimary(oldPriTeeth[i])
					&& !ALpri.Contains(oldPriTeeth[i])){
					ALpri.Add(oldPriTeeth[i]);
				}
			}
			for(int i=0;i<cTeeth.SelectedTeeth.Length;i++){
				int toothInt=Tooth.ToInt(cTeeth.SelectedTeeth[i]);
				if(ALpri.Contains(toothInt.ToString())){
					ALpri.Remove(toothInt.ToString());
				}
				else{
					ALpri.Add(toothInt.ToString());
				}
			}
			Patient PatCur=Patients.Cur;
			PatCur.PrimaryTeeth="";
			for(int i=0;i<ALpri.Count;i++){
				PatCur.PrimaryTeeth+=ALpri[i]+",";
			}
			Patients.Cur=PatCur;
			Patients.UpdateCur();
			ModuleSelected();
			//RefreshModuleScreen();
		}

		private void textADACode_Enter(object sender, System.EventArgs e) {
			if(textADACode.Text==Lan.g(this,"Type ADA Code")){
				//MessageBox.Show("match");
				textADACode.Text="";
			}
			//else{
				//MessageBox.Show("no match");
			//}
		}

		private void textADACode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if(e.KeyCode==Keys.Return){
				EnterTypedCode();
			}
		}

		private void textADACode_TextChanged(object sender, System.EventArgs e) {
			if(textADACode.Text=="d"){
				textADACode.Text="D";
				textADACode.SelectionStart=1;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			EnterTypedCode();
		}

		private void EnterTypedCode(){
			if(!ProcedureCodes.HList.ContainsKey(textADACode.Text)){
				MessageBox.Show(Lan.g(this,"Invalid code."));
				//textADACode.Text="";
				textADACode.SelectionStart=textADACode.Text.Length;
				return;
			}
			Procedures.SetDateFirstVisit(DateTime.Today,1);
			TreatmentArea tArea;
			Procedure ProcCur;
			int quadCount=0;//automates quadrant codes.
			for(int n=0;n==0 || n<cTeeth.SelectedTeeth.Length;n++){//always loops at least once.
				ProcCur=new Procedure();//this will be an insert, so no need to set CurOld
				ProcCur.ADACode=textADACode.Text;
				bool isValid=true;
				tArea=ProcedureCodes.GetProcCode(ProcCur.ADACode).TreatArea;
				if((tArea==TreatmentArea.Arch
					|| tArea==TreatmentArea.Mouth
					|| tArea==TreatmentArea.Quad
					|| tArea==TreatmentArea.Sextant
					|| tArea==TreatmentArea.ToothRange)
					&& n>0){//the only two left are tooth and surf
					continue;//only entered if n=0, so they don't get entered more than once.
				}
				else if(tArea==TreatmentArea.Quad){
					switch(quadCount){
						case 0: ProcCur.Surf="UR"; break;
						case 1: ProcCur.Surf="UL"; break;
						case 2: ProcCur.Surf="LL"; break;
						case 3: ProcCur.Surf="LR"; break;
						default: ProcCur.Surf="UR"; break;//this could happen.
					}
					quadCount++;
					Procedures.Cur=ProcCur;
					AddQuick();
				}
				else if(tArea==TreatmentArea.Surf){
					if(textSurf.Text=="")
						isValid=false;
					else
						ProcCur.Surf=textSurf.Text;
					if(cTeeth.SelectedTeeth.Length==0)
						isValid=false;
					else
						ProcCur.ToothNum=cTeeth.SelectedTeeth[n];
					Procedures.Cur=ProcCur;
					if(isValid)
						AddQuick();
					else
						AddProcedure();
				}
				else if(tArea==TreatmentArea.Tooth){
					if(cTeeth.SelectedTeeth.Length==0){
						Procedures.Cur=ProcCur;
						AddProcedure();
					}
					else{
						ProcCur.ToothNum=cTeeth.SelectedTeeth[n];
						Procedures.Cur=ProcCur;
						AddQuick();
					}
				}
				else if(tArea==TreatmentArea.ToothRange){
					if(cTeeth.SelectedTeeth.Length==0){
						Procedures.Cur=ProcCur;
						AddProcedure();
					}
					else{
						ProcCur.ToothRange="";
						for(int b=0;b<cTeeth.SelectedTeeth.Length;b++){
							if(b!=0) ProcCur.ToothRange+=",";
							ProcCur.ToothRange+=cTeeth.SelectedTeeth[b];
						}
						Procedures.Cur=ProcCur;
						AddQuick();
					}
				}
				else if(tArea==TreatmentArea.Arch){
					if(cTeeth.SelectedTeeth.Length==0){
						Procedures.Cur=ProcCur;
						AddProcedure();
						continue;
					}
					if(Tooth.IsMaxillary(cTeeth.SelectedTeeth[0])){
						ProcCur.Surf="U";
					}
					else{
						ProcCur.Surf="L";
					}
					Procedures.Cur=ProcCur;
					AddQuick();
				}
				else if(tArea==TreatmentArea.Sextant){
					Procedures.Cur=ProcCur;
					AddProcedure();
				}
				else{//mouth
					Procedures.Cur=ProcCur;
					AddQuick();
				}
			}//n selected teeth
			ModuleSelected();
			textADACode.Text="";
			textADACode.Select();
		}

		private void cTeeth_Click(object sender, System.EventArgs e) {
			if(cTeeth.SelectedTeeth.Length!=1)
				return;
			butO.BackColor=SystemColors.Control;
			butI.BackColor=SystemColors.Control;
			butL.BackColor=SystemColors.Control;
			butB.BackColor=SystemColors.Control;
			butF.BackColor=SystemColors.Control;
			textSurf.Text="";
			if(Tooth.IsAnterior(cTeeth.SelectedTeeth[0])){
				butB.Text="";
				butO.Text="";
				butB.Enabled=false;
				butO.Enabled=false;
				butF.Text="F";
				butI.Text="I";
				butF.Enabled=true;
				butI.Enabled=true;
			}
			else{
				butB.Text="B";
				butO.Text="O";
				butB.Enabled=true;
				butO.Enabled=true;
				butF.Text="";
				butI.Text="";
				butF.Enabled=false;
				butI.Enabled=false;
			}
		}

		private void tbProg_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(e.Button==MouseButtons.Right){
				menuProgRight.Show(tbProg,new Point(e.X,e.Y));
			}
		}

		private void menuItemPrintProg_Click(object sender, System.EventArgs e) {
			//print progress notes
		}

		

		

		

		

		

		

		

		

		

		#region VisiQuick integration code written by Thomas Jensen tje@thomsystems.com 
		/*
		private void XrayLinkBtn_Click(object sender, System.EventArgs e)	// TJE
		{
			if (!Patients.PatIsLoaded || Patients.Cur.PatNum<1)
				return;
			VQLink.VQStart(false,"",0,0);
		}

		private void SetPanelCol(Panel p, char c)	// TJE
		{
			if (c != '0')
				p.BackColor=SystemColors.ActiveCaption;
			else
				p.BackColor=SystemColors.ActiveBorder;
		}

		private void VQUpdatePatient()	// TJE
		{
			String	s;
			if (!Patients.PatIsLoaded || Patients.Cur.PatNum<1)	
				s="";
			else
				s=VQLink.SearchTStatus(Patients.Cur.PatNum);
			if (s.Length>=32) 
			{
				SetPanelCol(tooth11,s[0]);
				SetPanelCol(tooth12,s[1]);
				SetPanelCol(tooth13,s[2]);
				SetPanelCol(tooth14,s[3]);
				SetPanelCol(tooth15,s[4]);
				SetPanelCol(tooth16,s[5]);
				SetPanelCol(tooth17,s[6]);
				SetPanelCol(tooth18,s[7]);
				SetPanelCol(tooth21,s[8]);
				SetPanelCol(tooth22,s[9]);
				SetPanelCol(tooth23,s[10]);
				SetPanelCol(tooth24,s[11]);
				SetPanelCol(tooth25,s[12]);
				SetPanelCol(tooth26,s[13]);
				SetPanelCol(tooth27,s[14]);
				SetPanelCol(tooth28,s[15]);
				SetPanelCol(tooth31,s[16]);
				SetPanelCol(tooth32,s[17]);
				SetPanelCol(tooth33,s[18]);
				SetPanelCol(tooth34,s[19]);
				SetPanelCol(tooth35,s[20]);
				SetPanelCol(tooth36,s[21]);
				SetPanelCol(tooth37,s[22]);
				SetPanelCol(tooth38,s[23]);
				SetPanelCol(tooth41,s[24]);
				SetPanelCol(tooth42,s[25]);
				SetPanelCol(tooth43,s[26]);
				SetPanelCol(tooth44,s[27]);
				SetPanelCol(tooth45,s[28]);
				SetPanelCol(tooth46,s[29]);
				SetPanelCol(tooth47,s[30]);
				SetPanelCol(tooth48,s[31]);
			}
			if (s.Length>=32+6) 
			{
				SetPanelCol(toothpanos,s[32]);
				SetPanelCol(toothcephs,s[33]);
				if (s[34]!='0' | s[35]!='0' | s[36]!='0' | s[37]!='0') 
				{
					SetPanelCol(toothbw,'1');
					SetPanelCol(toothbwfloat,'1');
				}
				else
				{
					SetPanelCol(toothbw,'0');
					SetPanelCol(toothbwfloat,'0');
				}
			}
			if (s.Length>=32+6+9) 
			{
				if (s[39]!='0' | s[40]!='0' | s[41]!='0' | s[43]!='0') 
					SetPanelCol(toothcolors,'1');
				else
					SetPanelCol(toothcolors,'0');
				SetPanelCol(toothxrays,s[42]);
				SetPanelCol(toothpanos,s[44]);
				SetPanelCol(toothcephs,s[45]);
				SetPanelCol(toothdocs,s[46]);
			}
			if (s.Length>=32+6+9+1) 
			{
				SetPanelCol(toothfiles,s[47]);
			}
		}

		private void tooth18_Click(object sender, System.EventArgs e)	// TJE
		{
			VQLink.SearchPhotos(((Panel)sender).Name.Substring(5,2),VisiQuick.spf_tinymode+VisiQuick.spf_single,0);	
		}

		private void toothbwfloat_Click(object sender, System.EventArgs e)	// TJE
		{
			VQLink.SearchPhotos("",VisiQuick.spf_tinymode+VisiQuick.spf_2horizontal,VisiQuick.spi_bitewings);
		}

		private void toothbw_Click(object sender, System.EventArgs e)	// TJE
		{
			VQLink.SearchPhotos("",VisiQuick.npi_xrayview,VisiQuick.spi_bitewings);
		}

		private void toothxrays_Click(object sender, System.EventArgs e)	// TJE
		{
			VQLink.VQStart(false,"",0,VisiQuick.npi_xrayview);
		}

		private void toothcolors_Click(object sender, System.EventArgs e)	// TJE
		{
			VQLink.VQStart(false,"",0,VisiQuick.npi_colorview);
		}

		private void toothpanos_Click(object sender, System.EventArgs e)	// TJE
		{
			VQLink.SearchPhotos("",VisiQuick.spf_single,VisiQuick.spi_panview);
		}

		private void toothcephs_Click(object sender, System.EventArgs e)	// TJE
		{
			VQLink.SearchPhotos("",VisiQuick.spf_single,VisiQuick.spi_cephview);
		}

		private void toothdocs_Click(object sender, System.EventArgs e)	// TJE
		{
			VQLink.SearchPhotos("",VisiQuick.spf_single,VisiQuick.spi_docview);
		}

		private void toothfiles_Click(object sender, System.EventArgs e)	// TJE
		{
			VQLink.SearchPhotos("",VisiQuick.spf_single,VisiQuick.spi_fileview);
		}
		*/
		#endregion
	}//end class

	///<summary></summary>
	public struct TPChartLines{
		//public string Pr;
		///<summary></summary>
		public string Surf;
		///<summary></summary>
		public string Dx;
		///<summary></summary>
		public string Tx;
		///<summary></summary>
		public string Complete;
	}//end struct TPChartLines

	///<summary></summary>
	public struct ProgLine{
		///<summary></summary>
		public ProgType Type;
		///<summary></summary>
		public Color LineColor;
		///<summary></summary>
		public bool IsNote;
		///<summary></summary>
		public int Index;
		///<summary></summary>
		public string Date;
		///<summary></summary>
		public string Th;
		///<summary></summary>
		public string Surf;
		///<summary></summary>
		public string Dx;
		///<summary></summary>
		public string Description;
		///<summary></summary>
		public string Stat;
		///<summary></summary>
		public string Prov;
		///<summary></summary>
		public string Amount;
		///<summary></summary>
		public string Note;
	}//end struct ProgLines






}//end namespace
