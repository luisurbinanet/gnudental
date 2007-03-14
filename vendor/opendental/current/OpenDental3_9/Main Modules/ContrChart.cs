/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Data;
using System.IO;
using System.Windows.Forms;
using OpenDental.UI;

namespace OpenDental{
///<summary></summary>
	public class ContrChart : System.Windows.Forms.UserControl	{
		private OpenDental.UI.Button butAddProc;
		private OpenDental.UI.Button butM;
		private OpenDental.UI.Button butO;
		private OpenDental.UI.Button butD;
		private OpenDental.UI.Button butB;
		private OpenDental.UI.Button butL;
		private OpenDental.UI.Button butI;
		private OpenDental.UI.Button butF;
		private System.Windows.Forms.TextBox textSurf;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton radioEntryTP;
		private System.Windows.Forms.RadioButton radioEntryEO;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.RadioButton radioEntryEC;
		//private string[] newToothNum2= new string[1];
		private ProcStat newStatus;
		//private bool ControlDown=false;
		private OpenDental.UI.Button button1;
		private ArrayList ProcAL;
		//private TPChartLines[] TPChartLines2;
		private System.Windows.Forms.RadioButton radioEntryC;
		private ArrayList ProgLineAL;
		private OpenDental.TableProg tbProg;
		private bool dataValid=false;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private OpenDental.UI.Button butMedicalService;
		private System.Windows.Forms.ListBox listDx;
		private int[] hiLightedRows=new int[1];
		private ContrApptSingle ApptPlanned;
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
		private OpenDental.UI.Button butShowNone;
		private OpenDental.UI.Button butShowAll;
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
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butNew;
		private OpenDental.UI.Button butClear;
		private OpenDental.UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.Label labelDx;
		private System.Windows.Forms.Label labelNewProcHint;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textCreditType;
		private System.Windows.Forms.TextBox textIns;
		private System.Windows.Forms.Panel panelABCins;
		private System.Windows.Forms.TextBox textReferral;
		private System.Windows.Forms.TextBox textBillingType;
		private System.Windows.Forms.TextBox textDateFirstVisit;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ComboBox comboPriority;
		private System.Windows.Forms.ContextMenu menuProgRight;
		private System.Windows.Forms.MenuItem menuItemPrintProg;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TabControl tabControlImages;
		private System.Windows.Forms.Panel panelImages;
//fix: use AL for tbProg.SelectedRowsAL
		//public	VisiQuick VQLink;  // TJE
		private bool TreatmentNoteChanged;
		///<summary>Keeps track of which tab is selected. It's the index of the selected tab.</summary>
		private int selectedImageTab=0;
		private bool MouseIsDownOnImageSplitter;
		private int ImageSplitterOriginalY;
		private int OriginalImageMousePos;
		private System.Windows.Forms.ImageList imageListThumbnails;
		private System.Windows.Forms.ListView listViewImages;
		///<summary>The indices of the image categories which are visible in Chart.</summary>
		private ArrayList visImageCats;
		///<summary>The indices within Documents.List[i] of docs which are visible in Chart.</summary>
		private ArrayList visImages;
		///<summary>Full path to the patient folder, including \ on the end</summary>
		private string patFolder;
		private OpenDental.ODtextBox textTreatmentNotes;
		private System.Windows.Forms.ContextMenu menuPatient;
		private OpenDental.ValidDate textDate;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox checkToday;
		private FormImageViewer formImageViewer;
		private Procedure[] ProcList;
		private Family FamCur;
		private Patient PatCur;
		private InsPlan[] PlanList;
		private System.Windows.Forms.GroupBox groupPlanned;
		private System.Windows.Forms.ListBox listProcButtons;
		///<summary></summary>
		[Category("Data"),Description("Occurs when user changes current patient, usually by clicking on the Select Patient button.")]
		public event PatientSelectedEventHandler PatientSelected=null;
		///<summary>For one patient. Allows highlighting rows.</summary>
		private Appointment[] ApptList;
		private System.Drawing.Printing.PrintDocument pd2;
		private int linesPrinted;
		private System.Windows.Forms.CheckBox checkShowTeeth;//used in printing progress notes
		private bool headingPrinted;
		private Document[] DocumentList;
		private PatPlan[] PatPlanList;
			
		///<summary></summary>
		public ContrChart(){
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
			//tbProg.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbProg_CellClicked);
			tbProg.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbProg_CellDoubleClicked);
			//tbTeeth.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbTeeth_CellClicked);
			listProcButtons.Click += new System.EventHandler(this.listProcButtons_Click);
			//VQLink=new VisiQuick(Handle);		// TJE
			tabControlImages.DrawItem += new DrawItemEventHandler(OnDrawItem);
			//EventHandler onClick=new EventHandler(menuItem_Click);
			
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
			this.butAddProc = new OpenDental.UI.Button();
			this.butM = new OpenDental.UI.Button();
			this.butO = new OpenDental.UI.Button();
			this.butD = new OpenDental.UI.Button();
			this.butB = new OpenDental.UI.Button();
			this.butL = new OpenDental.UI.Button();
			this.butI = new OpenDental.UI.Button();
			this.butF = new OpenDental.UI.Button();
			this.textSurf = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.radioEntryR = new System.Windows.Forms.RadioButton();
			this.radioEntryC = new System.Windows.Forms.RadioButton();
			this.radioEntryEO = new System.Windows.Forms.RadioButton();
			this.radioEntryEC = new System.Windows.Forms.RadioButton();
			this.radioEntryTP = new System.Windows.Forms.RadioButton();
			this.button1 = new OpenDental.UI.Button();
			this.panelMedical = new System.Windows.Forms.Panel();
			this.butMedicalService = new OpenDental.UI.Button();
			this.textMedUrgNote = new System.Windows.Forms.TextBox();
			this.textService = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textMedical = new System.Windows.Forms.TextBox();
			this.listDx = new System.Windows.Forms.ListBox();
			this.labelDx = new System.Windows.Forms.Label();
			this.tbProg = new OpenDental.TableProg();
			this.groupPlanned = new System.Windows.Forms.GroupBox();
			this.butClear = new OpenDental.UI.Button();
			this.butNew = new OpenDental.UI.Button();
			this.labelMinutes = new System.Windows.Forms.Label();
			this.checkDone = new System.Windows.Forms.CheckBox();
			this.panelEnterTx = new System.Windows.Forms.Panel();
			this.comboPriority = new System.Windows.Forms.ComboBox();
			this.textDate = new OpenDental.ValidDate();
			this.checkToday = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.butOK = new OpenDental.UI.Button();
			this.textADACode = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.listProcButtons = new System.Windows.Forms.ListBox();
			this.label13 = new System.Windows.Forms.Label();
			this.butEnterTx = new System.Windows.Forms.Button();
			this.imageListUpDown = new System.Windows.Forms.ImageList(this.components);
			this.cTeeth = new OpenDental.ContrTeeth();
			this.groupShow = new System.Windows.Forms.GroupBox();
			this.checkShowTeeth = new System.Windows.Forms.CheckBox();
			this.checkNotes = new System.Windows.Forms.CheckBox();
			this.checkRx = new System.Windows.Forms.CheckBox();
			this.checkShowR = new System.Windows.Forms.CheckBox();
			this.butShowNone = new OpenDental.UI.Button();
			this.butShowAll = new OpenDental.UI.Button();
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
			this.tabControlImages = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.panelImages = new System.Windows.Forms.Panel();
			this.listViewImages = new System.Windows.Forms.ListView();
			this.imageListThumbnails = new System.Windows.Forms.ImageList(this.components);
			this.textTreatmentNotes = new OpenDental.ODtextBox();
			this.menuPatient = new System.Windows.Forms.ContextMenu();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.groupBox2.SuspendLayout();
			this.panelMedical.SuspendLayout();
			this.groupPlanned.SuspendLayout();
			this.panelEnterTx.SuspendLayout();
			this.groupShow.SuspendLayout();
			this.panelABCins.SuspendLayout();
			this.tabControlImages.SuspendLayout();
			this.panelImages.SuspendLayout();
			this.SuspendLayout();
			// 
			// butAddProc
			// 
			this.butAddProc.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddProc.Autosize = true;
			this.butAddProc.BackColor = System.Drawing.SystemColors.Control;
			this.butAddProc.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddProc.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddProc.Location = new System.Drawing.Point(192, 1);
			this.butAddProc.Name = "butAddProc";
			this.butAddProc.Size = new System.Drawing.Size(89, 23);
			this.butAddProc.TabIndex = 17;
			this.butAddProc.Text = "Procedure List";
			this.butAddProc.Click += new System.EventHandler(this.butAddProc_Click);
			// 
			// butM
			// 
			this.butM.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butM.Autosize = true;
			this.butM.BackColor = System.Drawing.SystemColors.Control;
			this.butM.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butM.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butM.Location = new System.Drawing.Point(4, 43);
			this.butM.Name = "butM";
			this.butM.Size = new System.Drawing.Size(24, 20);
			this.butM.TabIndex = 18;
			this.butM.Text = "M";
			this.butM.Click += new System.EventHandler(this.butM_Click);
			// 
			// butO
			// 
			this.butO.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butO.Autosize = true;
			this.butO.BackColor = System.Drawing.SystemColors.Control;
			this.butO.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butO.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butO.Location = new System.Drawing.Point(28, 43);
			this.butO.Name = "butO";
			this.butO.Size = new System.Drawing.Size(18, 20);
			this.butO.TabIndex = 19;
			this.butO.Text = "O";
			this.butO.Click += new System.EventHandler(this.butO_Click);
			// 
			// butD
			// 
			this.butD.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butD.Autosize = true;
			this.butD.BackColor = System.Drawing.SystemColors.Control;
			this.butD.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butD.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butD.Location = new System.Drawing.Point(62, 43);
			this.butD.Name = "butD";
			this.butD.Size = new System.Drawing.Size(24, 20);
			this.butD.TabIndex = 20;
			this.butD.Text = "D";
			this.butD.Click += new System.EventHandler(this.butD_Click);
			// 
			// butB
			// 
			this.butB.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butB.Autosize = true;
			this.butB.BackColor = System.Drawing.SystemColors.Control;
			this.butB.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butB.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butB.Location = new System.Drawing.Point(28, 23);
			this.butB.Name = "butB";
			this.butB.Size = new System.Drawing.Size(17, 20);
			this.butB.TabIndex = 21;
			this.butB.Text = "B";
			this.butB.Click += new System.EventHandler(this.butB_Click);
			// 
			// butL
			// 
			this.butL.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butL.Autosize = true;
			this.butL.BackColor = System.Drawing.SystemColors.Control;
			this.butL.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butL.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butL.Location = new System.Drawing.Point(33, 63);
			this.butL.Name = "butL";
			this.butL.Size = new System.Drawing.Size(24, 20);
			this.butL.TabIndex = 22;
			this.butL.Text = "L";
			this.butL.Click += new System.EventHandler(this.butL_Click);
			// 
			// butI
			// 
			this.butI.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butI.Autosize = true;
			this.butI.BackColor = System.Drawing.SystemColors.Control;
			this.butI.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butI.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butI.Location = new System.Drawing.Point(45, 43);
			this.butI.Name = "butI";
			this.butI.Size = new System.Drawing.Size(17, 20);
			this.butI.TabIndex = 23;
			this.butI.Text = "I";
			this.butI.Click += new System.EventHandler(this.butI_Click);
			// 
			// butF
			// 
			this.butF.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butF.Autosize = true;
			this.butF.BackColor = System.Drawing.SystemColors.Control;
			this.butF.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butF.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butF.Location = new System.Drawing.Point(45, 23);
			this.butF.Name = "butF";
			this.butF.Size = new System.Drawing.Size(17, 20);
			this.butF.TabIndex = 24;
			this.butF.Text = "F";
			this.butF.Click += new System.EventHandler(this.butF_Click);
			// 
			// textSurf
			// 
			this.textSurf.Location = new System.Drawing.Point(9, 2);
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
			this.groupBox2.Location = new System.Drawing.Point(2, 87);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(88, 96);
			this.groupBox2.TabIndex = 35;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Entry Status";
			// 
			// radioEntryR
			// 
			this.radioEntryR.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioEntryR.Location = new System.Drawing.Point(8, 76);
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
			this.radioEntryEO.Location = new System.Drawing.Point(8, 61);
			this.radioEntryEO.Name = "radioEntryEO";
			this.radioEntryEO.Size = new System.Drawing.Size(72, 16);
			this.radioEntryEO.TabIndex = 2;
			this.radioEntryEO.Text = "Ex Other";
			this.radioEntryEO.CheckedChanged += new System.EventHandler(this.radioEntryEO_CheckedChanged);
			// 
			// radioEntryEC
			// 
			this.radioEntryEC.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioEntryEC.Location = new System.Drawing.Point(8, 46);
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
			this.radioEntryTP.Location = new System.Drawing.Point(8, 16);
			this.radioEntryTP.Name = "radioEntryTP";
			this.radioEntryTP.Size = new System.Drawing.Size(77, 16);
			this.radioEntryTP.TabIndex = 0;
			this.radioEntryTP.TabStop = true;
			this.radioEntryTP.Text = "TP";
			this.radioEntryTP.CheckedChanged += new System.EventHandler(this.radioEntryTP_CheckedChanged);
			// 
			// button1
			// 
			this.button1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button1.Autosize = true;
			this.button1.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button1.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
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
			this.panelMedical.Location = new System.Drawing.Point(1, 572);
			this.panelMedical.Name = "panelMedical";
			this.panelMedical.Size = new System.Drawing.Size(411, 97);
			this.panelMedical.TabIndex = 39;
			// 
			// butMedicalService
			// 
			this.butMedicalService.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butMedicalService.Autosize = true;
			this.butMedicalService.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butMedicalService.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
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
			this.listDx.Location = new System.Drawing.Point(92, 16);
			this.listDx.Name = "listDx";
			this.listDx.Size = new System.Drawing.Size(94, 173);
			this.listDx.TabIndex = 46;
			this.listDx.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listDx_MouseDown);
			// 
			// labelDx
			// 
			this.labelDx.Location = new System.Drawing.Point(90, -2);
			this.labelDx.Name = "labelDx";
			this.labelDx.Size = new System.Drawing.Size(100, 18);
			this.labelDx.TabIndex = 47;
			this.labelDx.Text = "Diagnosis";
			this.labelDx.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// tbProg
			// 
			this.tbProg.BackColor = System.Drawing.SystemColors.Window;
			this.tbProg.Location = new System.Drawing.Point(415, 281);
			this.tbProg.Name = "tbProg";
			this.tbProg.ScrollValue = 1;
			this.tbProg.SelectedIndices = new int[0];
			this.tbProg.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.tbProg.Size = new System.Drawing.Size(498, 386);
			this.tbProg.TabIndex = 40;
			this.tbProg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbProg_MouseUp);
			this.tbProg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbProg_KeyDown);
			// 
			// groupPlanned
			// 
			this.groupPlanned.Controls.Add(this.butClear);
			this.groupPlanned.Controls.Add(this.butNew);
			this.groupPlanned.Controls.Add(this.labelMinutes);
			this.groupPlanned.Controls.Add(this.checkDone);
			this.groupPlanned.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupPlanned.Location = new System.Drawing.Point(0, 30);
			this.groupPlanned.Name = "groupPlanned";
			this.groupPlanned.Size = new System.Drawing.Size(198, 114);
			this.groupPlanned.TabIndex = 43;
			this.groupPlanned.TabStop = false;
			this.groupPlanned.Text = "Planned Appointment";
			// 
			// butClear
			// 
			this.butClear.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClear.Autosize = true;
			this.butClear.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClear.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
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
			this.butNew.Autosize = true;
			this.butNew.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNew.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
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
			this.panelEnterTx.Controls.Add(this.comboPriority);
			this.panelEnterTx.Controls.Add(this.textDate);
			this.panelEnterTx.Controls.Add(this.checkToday);
			this.panelEnterTx.Controls.Add(this.label6);
			this.panelEnterTx.Controls.Add(this.listDx);
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
			// comboPriority
			// 
			this.comboPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPriority.Location = new System.Drawing.Point(92, 205);
			this.comboPriority.MaxDropDownItems = 40;
			this.comboPriority.Name = "comboPriority";
			this.comboPriority.Size = new System.Drawing.Size(96, 21);
			this.comboPriority.TabIndex = 54;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(1, 205);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(89, 20);
			this.textDate.TabIndex = 55;
			this.textDate.Text = "";
			// 
			// checkToday
			// 
			this.checkToday.Checked = true;
			this.checkToday.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkToday.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkToday.Location = new System.Drawing.Point(2, 188);
			this.checkToday.Name = "checkToday";
			this.checkToday.Size = new System.Drawing.Size(80, 18);
			this.checkToday.TabIndex = 58;
			this.checkToday.Text = "Today";
			this.checkToday.CheckedChanged += new System.EventHandler(this.checkToday_CheckedChanged);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(90, 188);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(79, 17);
			this.label6.TabIndex = 57;
			this.label6.Text = "Priority";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(447, 1);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(44, 23);
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
			this.label14.Location = new System.Drawing.Point(284, 6);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(51, 17);
			this.label14.TabIndex = 51;
			this.label14.Text = "Or";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.label13.Location = new System.Drawing.Point(191, 21);
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
			this.imageListUpDown.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
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
			this.groupShow.Controls.Add(this.checkShowTeeth);
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
			// checkShowTeeth
			// 
			this.checkShowTeeth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowTeeth.Location = new System.Drawing.Point(104, 48);
			this.checkShowTeeth.Name = "checkShowTeeth";
			this.checkShowTeeth.Size = new System.Drawing.Size(104, 13);
			this.checkShowTeeth.TabIndex = 15;
			this.checkShowTeeth.Text = "Selected Teeth";
			this.checkShowTeeth.Click += new System.EventHandler(this.checkShowTeeth_Click);
			// 
			// checkNotes
			// 
			this.checkNotes.AllowDrop = true;
			this.checkNotes.Checked = true;
			this.checkNotes.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkNotes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNotes.Location = new System.Drawing.Point(104, 32);
			this.checkNotes.Name = "checkNotes";
			this.checkNotes.Size = new System.Drawing.Size(102, 13);
			this.checkNotes.TabIndex = 11;
			this.checkNotes.Text = "Proc Notes";
			this.checkNotes.Click += new System.EventHandler(this.checkNotes_Click);
			// 
			// checkRx
			// 
			this.checkRx.Checked = true;
			this.checkRx.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkRx.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkRx.Location = new System.Drawing.Point(104, 16);
			this.checkRx.Name = "checkRx";
			this.checkRx.Size = new System.Drawing.Size(95, 13);
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
			this.checkShowR.Size = new System.Drawing.Size(98, 16);
			this.checkShowR.TabIndex = 14;
			this.checkShowR.Text = "Referred";
			this.checkShowR.Click += new System.EventHandler(this.checkShowR_Click);
			// 
			// butShowNone
			// 
			this.butShowNone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butShowNone.Autosize = true;
			this.butShowNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butShowNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butShowNone.Location = new System.Drawing.Point(69, 84);
			this.butShowNone.Name = "butShowNone";
			this.butShowNone.Size = new System.Drawing.Size(58, 23);
			this.butShowNone.TabIndex = 13;
			this.butShowNone.Text = "None";
			this.butShowNone.Click += new System.EventHandler(this.butShowNone_Click);
			// 
			// butShowAll
			// 
			this.butShowAll.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butShowAll.Autosize = true;
			this.butShowAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butShowAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butShowAll.Location = new System.Drawing.Point(5, 84);
			this.butShowAll.Name = "butShowAll";
			this.butShowAll.Size = new System.Drawing.Size(53, 23);
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
			this.checkShowTP.Location = new System.Drawing.Point(4, 16);
			this.checkShowTP.Name = "checkShowTP";
			this.checkShowTP.Size = new System.Drawing.Size(101, 13);
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
			this.imageListMain.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
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
			this.panelABCins.Location = new System.Drawing.Point(0, 528);
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
			// tabControlImages
			// 
			this.tabControlImages.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.tabControlImages.Controls.Add(this.tabPage1);
			this.tabControlImages.Controls.Add(this.tabPage2);
			this.tabControlImages.Controls.Add(this.tabPage4);
			this.tabControlImages.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tabControlImages.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.tabControlImages.ItemSize = new System.Drawing.Size(42, 22);
			this.tabControlImages.Location = new System.Drawing.Point(0, 681);
			this.tabControlImages.Name = "tabControlImages";
			this.tabControlImages.SelectedIndex = 0;
			this.tabControlImages.Size = new System.Drawing.Size(939, 27);
			this.tabControlImages.TabIndex = 185;
			this.tabControlImages.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControlImages_MouseDown);
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 4);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(931, 0);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "BW\'s";
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 4);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(931, -3);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Pano";
			// 
			// tabPage4
			// 
			this.tabPage4.Location = new System.Drawing.Point(4, 4);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(931, -3);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "tabPage4";
			// 
			// panelImages
			// 
			this.panelImages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelImages.Controls.Add(this.listViewImages);
			this.panelImages.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelImages.DockPadding.Top = 4;
			this.panelImages.ForeColor = System.Drawing.SystemColors.ControlText;
			this.panelImages.Location = new System.Drawing.Point(0, 441);
			this.panelImages.Name = "panelImages";
			this.panelImages.Size = new System.Drawing.Size(939, 240);
			this.panelImages.TabIndex = 186;
			this.panelImages.Visible = false;
			this.panelImages.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelImages_MouseUp);
			this.panelImages.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelImages_MouseMove);
			this.panelImages.MouseLeave += new System.EventHandler(this.panelImages_MouseLeave);
			this.panelImages.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelImages_MouseDown);
			// 
			// listViewImages
			// 
			this.listViewImages.Activation = System.Windows.Forms.ItemActivation.TwoClick;
			this.listViewImages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewImages.HideSelection = false;
			this.listViewImages.LargeImageList = this.imageListThumbnails;
			this.listViewImages.Location = new System.Drawing.Point(0, 4);
			this.listViewImages.MultiSelect = false;
			this.listViewImages.Name = "listViewImages";
			this.listViewImages.Size = new System.Drawing.Size(937, 234);
			this.listViewImages.TabIndex = 0;
			this.listViewImages.DoubleClick += new System.EventHandler(this.listViewImages_DoubleClick);
			// 
			// imageListThumbnails
			// 
			this.imageListThumbnails.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageListThumbnails.ImageSize = new System.Drawing.Size(100, 100);
			this.imageListThumbnails.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// textTreatmentNotes
			// 
			this.textTreatmentNotes.AcceptsReturn = true;
			this.textTreatmentNotes.Location = new System.Drawing.Point(0, 425);
			this.textTreatmentNotes.Multiline = true;
			this.textTreatmentNotes.Name = "textTreatmentNotes";
			this.textTreatmentNotes.QuickPasteType = OpenDental.QuickPasteType.ChartTreatment;
			this.textTreatmentNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textTreatmentNotes.Size = new System.Drawing.Size(411, 102);
			this.textTreatmentNotes.TabIndex = 187;
			this.textTreatmentNotes.Text = "";
			this.textTreatmentNotes.Leave += new System.EventHandler(this.textTreatmentNotes_Leave);
			this.textTreatmentNotes.TextChanged += new System.EventHandler(this.textTreatmentNotes_TextChanged);
			// 
			// pd2
			// 
			this.pd2.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pd2_PrintPage);
			// 
			// ContrChart
			// 
			this.Controls.Add(this.panelEnterTx);
			this.Controls.Add(this.panelImages);
			this.Controls.Add(this.tabControlImages);
			this.Controls.Add(this.tbProg);
			this.Controls.Add(this.panelABCins);
			this.Controls.Add(this.labelNewProcHint);
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.cTeeth);
			this.Controls.Add(this.panelMedical);
			this.Controls.Add(this.groupPlanned);
			this.Controls.Add(this.butEnterTx);
			this.Controls.Add(this.groupShow);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textTreatmentNotes);
			this.Controls.Add(this.label4);
			this.Name = "ContrChart";
			this.Size = new System.Drawing.Size(939, 708);
			this.Resize += new System.EventHandler(this.ContrChart_Resize);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ContrChart_Layout);
			this.groupBox2.ResumeLayout(false);
			this.panelMedical.ResumeLayout(false);
			this.groupPlanned.ResumeLayout(false);
			this.panelEnterTx.ResumeLayout(false);
			this.groupShow.ResumeLayout(false);
			this.panelABCins.ResumeLayout(false);
			this.tabControlImages.ResumeLayout(false);
			this.panelImages.ResumeLayout(false);
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
			tbProg.Height=ClientSize.Height-tabControlImages.Height-tbProg.Location.Y+1;
			if(panelImages.Visible){
				tbProg.Height-=(panelImages.Height+2);
			}
		}

		private void ContrChart_Resize(object sender, System.EventArgs e) {
			tbProg.LayoutTables();
		}

		///<summary></summary>
		public void InstantClasses(){
			newStatus=ProcStat.TP;
			ApptPlanned=new ContrApptSingle();
			ApptPlanned.Info.IsNext=true;
			ApptPlanned.Location=new Point(1,17);
			ApptPlanned.Visible=false;
			groupPlanned.Controls.Add(ApptPlanned);
			ApptPlanned.DoubleClick += new System.EventHandler(ApptPlanned_DoubleClick);
			panelEnterTx.Visible=false;
			butEnterTx.ImageIndex=1;//down arrow
			tbProg.Location=panelEnterTx.Location;
			//MessageBox.Show((panelEnterTx.Location.X+5).ToString());
			cTeeth.SetWidth(421);//if I had time, I would perfect this
			cTeeth.CreateBackShadow(true);
			tbProg.Height=this.ClientSize.Height-tbProg.Location.Y-2;
			//can't use Lan.F
			Lan.C(this,new Control[]
				{
				groupPlanned,
				checkDone,
				butNew,
				butClear,
				checkShowTP,
				checkShowC,
				checkShowE,
				checkShowR,
				checkRx,
				checkNotes,
				butEnterTx,
				labelNewProcHint,
				labelDx,
				butM,
				butO,
				butD,
				butL,
				butB,
				butI,
				butF,
				groupBox2,
				radioEntryTP,
				radioEntryC,
				radioEntryEC,
				radioEntryEO,
				radioEntryR,
				checkToday,
				labelDx,
				label6,
				butAddProc,
				label14,
				//textADACode is handled in ClearButtons()
				butOK,
				label13,
				label4,
				label2,
				label1,
				label3,
				butMedicalService
				});
			Lan.C(this,menuPrimary.MenuItems[0]);
			Lan.C(this,menuPrimary.MenuItems[1]);
			Lan.C(this,menuPrimary.MenuItems[2]);
			LayoutToolBar();
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
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"New Rx"),1,"","Rx"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			button=new ODToolBarButton(Lan.g(this,"Primary"),-1
				,Lan.g(this,"Change the Primary/Permanent status of teeth"),"Primary");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=menuPrimary;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Perio Chart"),2,"","Perio"));
			ArrayList toolButItems=ToolButItems.GetForToolBar(ToolBarsAvail.ChartModule);
			for(int i=0;i<toolButItems.Count;i++){
				ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
				ToolBarMain.Buttons.Add(new ODToolBarButton(((ToolButItem)toolButItems[i]).ButtonText
					,-1,"",((ToolButItem)toolButItems[i]).ProgramNum));
			}
			ToolBarMain.Invalidate();
		}

		///<summary></summary>
		public void ModuleSelected(int patNum){
			EasyHideClinicalData();
			RefreshModuleData(patNum);
			RefreshModuleScreen();
		}

		///<summary></summary>
		public void ModuleUnselected(){
			if(FamCur==null)
				return;
			if(TreatmentNoteChanged){
				PatientNotes.Cur.Treatment=textTreatmentNotes.Text;
				PatientNotes.UpdateCur(PatCur.Guarantor);
				TreatmentNoteChanged=false;
			}
			FamCur=null;
			PatCur=null;
			PlanList=null;
			//from FillProgNotes:
			ProcList=null;
			//Procedures.HList=null;
			//Procedures.MissingTeeth=null;
			RxPats.List=null;
			//RefAttaches.List=null;
		}

		private void RefreshModuleData(int patNum){
			if(patNum==0){
				PatCur=null;
				FamCur=null;
				return;
			}
			FamCur=Patients.GetFamily(patNum);
			PatCur=FamCur.GetPatient(patNum);
			PlanList=InsPlans.Refresh(FamCur);
			PatPlanList=PatPlans.Refresh(patNum);
			CovPats.Refresh(PlanList,PatPlanList);
			PatientNotes.Refresh(patNum,PatCur.Guarantor);
      //ClaimProcs.Refresh();
			//RefAttaches.Refresh();
			GetImageFolder();
			DocAttaches.Refresh(patNum);
			DocumentList=Documents.Refresh(DocAttaches.List);
			//Procs get refreshed in FillProgNotes
			ApptList=Appointments.GetForPat(patNum);
		}

		private void GetImageFolder(){
			//this is the same code as in the Images module
			Patient patOld=PatCur.Copy();
			if(PatCur.ImageFolder==""){//creates new folder for patient if none present
				string name=PatCur.LName+PatCur.FName;
				string folder="";
				for(int i=0;i<name.Length;i++){
					if(Char.IsLetter(name,i)){
						folder+=name.Substring(i,1);
					}
				}
				folder+=PatCur.PatNum.ToString();//ensures unique name
				try{
					PatCur.ImageFolder=folder;
					patFolder=((Pref)Prefs.HList["DocPath"]).ValueString
						+PatCur.ImageFolder.Substring(0,1)+@"\"
						+PatCur.ImageFolder+@"\";
					Directory.CreateDirectory(patFolder);
					PatCur.Update(patOld);
					//ModuleSelected(PatCur.PatNum);
				}
				catch{
					MessageBox.Show(Lan.g(this,"Error.  Could not create folder for patient. "));
					patFolder="";
					return;
				}
			}
			else{//patient folder already created once
				patFolder=((Pref)Prefs.HList["DocPath"]).ValueString
					+PatCur.ImageFolder.Substring(0,1)+@"\"
					+PatCur.ImageFolder+@"\";
			}
			if(!Directory.Exists(patFolder)){//this makes it more resiliant and allows copies
					//of the opendentaldata folder to be used in read-only situations.
				try{
					Directory.CreateDirectory(patFolder);
				}
				catch{
					MessageBox.Show(Lan.g(this,"Error.  Could not create folder for patient. "));
					patFolder="";
					return;
				}
			}
		}

		private void RefreshModuleScreen(){
			if(PatCur!=null){
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString+" - "
					+PatCur.GetNameLF();
				groupShow.Enabled=true;
				panelMedical.Enabled=true;
				groupPlanned.Enabled=true;
				cTeeth.Enabled=true;
				tbProg.Enabled=true;
				ToolBarMain.Buttons["Rx"].Enabled=true;
				ToolBarMain.Buttons["Primary"].Enabled=true;
				ToolBarMain.Buttons["Perio"].Enabled=true;
				panelEnterTx.Enabled=true;//?
			}
			else{
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString;
				groupShow.Enabled=false;
				panelMedical.Enabled=false;
				groupPlanned.Enabled=false;
				cTeeth.Enabled=false;
				tbProg.Enabled=false;
				ToolBarMain.Buttons["Rx"].Enabled=false;
				ToolBarMain.Buttons["Primary"].Enabled=false;
				ToolBarMain.Buttons["Perio"].Enabled=false;
				panelEnterTx.Enabled=false;//?
			}
			FillPatientButton();
			ToolBarMain.Invalidate();
			ClearButtons();
			FillProgNotes();
			FillPlanned();
			FillMedical();
      FillDxProcImage();
			FillImages();
		}

		private void FillPatientButton(){
			Patients.AddPatsToMenu(menuPatient,new EventHandler(menuPatient_Click),PatCur,FamCur);
		}

		private void menuPatient_Click(object sender,System.EventArgs e) {
			int newPatNum=Patients.ButtonSelect(menuPatient,sender,FamCur);
			OnPatientSelected(newPatNum);
			ModuleSelected(newPatNum);
		}

		private void EasyHideClinicalData(){
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
				Programs.Execute((int)e.Button.Tag,PatCur);
			}
		}

		private void OnPatient_Click(){
			FormPatientSelect formPS=new FormPatientSelect();
			formPS.ShowDialog();
			if(formPS.DialogResult==DialogResult.OK){
				OnPatientSelected(formPS.SelectedPatNum);
				ModuleSelected(formPS.SelectedPatNum);
			}
		}

		///<summary></summary>
		private void OnPatientSelected(int patNum){
			PatientSelectedEventArgs eArgs=new OpenDental.PatientSelectedEventArgs(patNum);
			if(PatientSelected!=null)
				PatientSelected(this,eArgs);
		}

		private void OnRx_Click(){
			if(!Security.IsAuthorized(Permissions.RxCreate)){
				return;
			}
			FormRxSelect FormRS=new FormRxSelect(PatCur);
			FormRS.ShowDialog();
			if(FormRS.DialogResult!=DialogResult.OK) return;
			ModuleSelected(PatCur.PatNum);
			SecurityLogs.MakeLogEntry(Permissions.RxCreate,PatCur.PatNum,PatCur.GetNameLF());
		}

		private void OnPerio_Click(){
			FormPerio FormP=new FormPerio(PatCur);
			FormP.ShowDialog();
		}

		private void FillPlanned(){
			if(PatCur==null){
				ApptPlanned.Visible=false;
				checkDone.Checked=false;
				labelMinutes.Text="";
				return;
			}
			if(PatCur.NextAptNum==0){
				ApptPlanned.Visible=false;
				checkDone.Checked=false;
				labelMinutes.Text="";
				return;
			}
			if(PatCur.NextAptNum==-1){
				ApptPlanned.Visible=false;
				checkDone.Checked=true;
				labelMinutes.Text="";
				return;
			}
			checkDone.Checked=false;
			//MessageBox.Show
			Appointment apt=Appointments.GetOneApt(PatCur.NextAptNum);
			if(apt==null){//next appointment not found
				Patient patOld=PatCur.Copy();
				PatCur.NextAptNum=0;
				PatCur.Update(patOld);
				FamCur=Patients.GetFamily(PatCur.PatNum);//might be overkill
				ApptPlanned.Visible=false;
				checkDone.Checked=false;
				labelMinutes.Text="";
				return;
			}
			ApptPlanned.Info.MyApt=apt.Copy();
			ProcDesc procDesc=Procedures.GetProcsForSingle(ApptPlanned.Info.MyApt.AptNum,true);
			ApptPlanned.Info.Procs=procDesc.ProcLines;
			ApptPlanned.Info.Production=procDesc.Production;
			ApptPlanned.Info.MyPatient=PatCur.Copy();
			ApptPlanned.SetSize();
			ApptPlanned.Width=114;
			ApptPlanned.CreateShadow();
			ApptPlanned.Visible=true;
			ApptPlanned.Refresh();
			labelMinutes.Text=(ApptPlanned.Info.MyApt.Pattern.Length*5).ToString()+" minutes";
			//ContrApptSingle.ApptIsSelected=false;
		}

		private void FillMedical(){
			panelMedical.BackColor=Defs.Long[(int)DefCat.MiscColors][3].ItemColor;
			if(PatCur==null){
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
			textMedUrgNote.Text=PatCur.MedUrgNote;
			textMedical.Text=PatientNotes.Cur.Medical;
			textService.Text=PatientNotes.Cur.Service;
			textTreatmentNotes.Text=PatientNotes.Cur.Treatment;
			textTreatmentNotes.Enabled=true;
			textTreatmentNotes.Select(textTreatmentNotes.Text.Length+2,1);
			textTreatmentNotes.ScrollToCaret();
			TreatmentNoteChanged=false;
			panelABCins.Enabled=true;
			textCreditType.Text=PatCur.CreditType;
			textBillingType.Text=Defs.GetName(DefCat.BillingTypes,PatCur.BillingType);
			textReferral.Text="";
			RefAttach[] RefAttachList=RefAttaches.Refresh(PatCur.PatNum);
			for(int i=0;i<RefAttachList.Length;i++){
				if(RefAttachList[i].IsFrom){
					textReferral.Text=Referrals.GetReferral(RefAttachList[i].ReferralNum).GetName();
					break;
				}				
			}
			if(textReferral.Text==""){
				textReferral.Text="referral ??";
			}
			if(PatCur.DateFirstVisit.Year<1880)
				textDateFirstVisit.Text="date ??";
			else if(PatCur.DateFirstVisit==DateTime.Today)
				textDateFirstVisit.Text="NEW PAT";
			else
				textDateFirstVisit.Text=PatCur.DateFirstVisit.ToShortDateString();
			textIns.Text="";
			string name="";
			if(PatPlanList.Length>0){
				name=InsPlans.GetCarrierName(PatPlans.GetPlanNum(PatPlanList,1),PlanList);
				if(name.Length>20)
					name=name.Substring(0,20)+"...";
				textIns.Text+=name+" ";
				if(PatPlanList[0].IsPending)
					textIns.Text+="(pending) ";
			}
			name="";
			if(PatPlanList.Length>1){
				name=InsPlans.GetCarrierName(PatPlans.GetPlanNum(PatPlanList,2),PlanList);
				if(name.Length>20)
					name=name.Substring(0,20)+"...";
				textIns.Text+=name+" ";
				if(PatPlanList[1].IsPending)
					textIns.Text+="(pending)";
			}
			if(textIns.Text=="  ")
				textIns.Text=Lan.g(this,"No Insurance");
		}

		private void textTreatmentNotes_TextChanged(object sender, System.EventArgs e) {
			TreatmentNoteChanged=true;
		}

		private void textTreatmentNotes_Leave(object sender, System.EventArgs e) {
			//need to skip this if selecting another module. Handled in ModuleUnselected due to click event
			if(FamCur==null)
				return;
			if(TreatmentNoteChanged){
				PatientNotes.Cur.Treatment=textTreatmentNotes.Text;
				PatientNotes.UpdateCur(PatCur.Guarantor);
				TreatmentNoteChanged=false;
			}
		}

		private void FillProgNotes(){
			//remember which teeth were selected
			ArrayList selectedTeeth=new ArrayList();//integers 1-32
			for(int i=0;i<cTeeth.SelectedTeeth.Length;i++){
				selectedTeeth.Add(Tooth.ToInt(cTeeth.SelectedTeeth[i]));
			}
			cTeeth.ClearProcs();
			if(PatCur==null){
				tbProg.ResetRows(0);
				tbProg.LayoutTables();
				cTeeth.DrawShadow();
				return;
			}
			if(checkShowTeeth.Checked){
				for(int i=0;i<selectedTeeth.Count;i++){
					cTeeth.SetSelected((int)selectedTeeth[i],true);
				}
			}
			tbProg.SelectedRows=new int[0];
			ProcList=Procedures.Refresh(PatCur.PatNum);
			RxPats.Refresh(PatCur.PatNum);
			ProcAL = new ArrayList();
			RxAL = new ArrayList();
			bool showProc;
			//step through all procedures for patient and move selected ones to
			//ProcAL and RxAL arrays as intermediate, each ordered by date.
			//Pull from both into ProgLineAL array for display,
			//and draw teeth graphics by sending Procedure array directly to cTeeth.
			//Every ProgLineAL entry contains type and index to access original array.
			for(int i=0;i<ProcList.Length;i++){
				//always show missing teeth if C,EC,or EO
				if(ProcedureCodes.GetProcCode(ProcList[i].ADACode).RemoveTooth
					&& (ProcList[i].ProcStatus==ProcStat.C || ProcList[i].ProcStatus==ProcStat.EC || ProcList[i].ProcStatus==ProcStat.EO)
					){
					ProcAL.Add(ProcList[i]);
					continue;
				}
				//if showing only selected teeth, then skip all other teeth
				if(checkShowTeeth.Checked){
					showProc=false;
					switch(ProcedureCodes.GetProcCode(ProcList[i].ADACode).TreatArea){
					  case TreatmentArea.Arch:
							for(int s=0;s<selectedTeeth.Count;s++){
								if(ProcList[i].Surf=="U" && (int)selectedTeeth[s]<17){
									showProc=true;
								}
								else if(ProcList[i].Surf=="L" && (int)selectedTeeth[s]>16){
									showProc=true;
								}
							}
							break;
						case TreatmentArea.Mouth:
						case TreatmentArea.None:
						case TreatmentArea.Sextant://nobody will miss it
							showProc=false;
							break;
						case TreatmentArea.Quad:
							for(int s=0;s<selectedTeeth.Count;s++){
								if(ProcList[i].Surf=="UR" && (int)selectedTeeth[s]<=8){
									showProc=true;
								}
								else if(ProcList[i].Surf=="UL" && (int)selectedTeeth[s]>=9 && (int)selectedTeeth[s]<=16){
									showProc=true;
								}
								else if(ProcList[i].Surf=="LL" && (int)selectedTeeth[s]>=17 && (int)selectedTeeth[s]<=24){
									showProc=true;
								}
								else if(ProcList[i].Surf=="LR" && (int)selectedTeeth[s]>=25 && (int)selectedTeeth[s]<=32){
									showProc=true;
								}
							}
							break;
						case TreatmentArea.Surf:
						case TreatmentArea.Tooth:
							for(int s=0;s<selectedTeeth.Count;s++){
								if(Tooth.ToInt(ProcList[i].ToothNum)==(int)selectedTeeth[s]){
									showProc=true;
								}
							}
							break;
						case TreatmentArea.ToothRange:
							string[] range=ProcList[i].ToothRange.Split(',');
							for(int s=0;s<selectedTeeth.Count;s++){
								for(int r=0;r<range.Length;r++){
									if(Tooth.ToInt(range[r])==(int)selectedTeeth[s]){
										showProc=true;
									}
								}
							}
							break;
					}
					if(!showProc){
						continue;
					}
				}
				switch(ProcList[i].ProcStatus){
					case ProcStat.TP :
						if (checkShowTP.Checked){
							ProcAL.Add(ProcList[i]);
						}
						break;
					case ProcStat.C :
						if (checkShowC.Checked){
							ProcAL.Add(ProcList[i]);
						}
						break;
					case ProcStat.EC :
						if (checkShowE.Checked){
							ProcAL.Add(ProcList[i]);
						}
						break;
					case ProcStat.EO :
						if (checkShowE.Checked){
							ProcAL.Add(ProcList[i]);
						}
						break;
					case ProcStat.R :
						if (checkShowR.Checked){
							ProcAL.Add(ProcList[i]);
						}
						break;
				}
			}//for i
			cTeeth.PatCur=PatCur;
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
					}
					else{//else note
						if(!((ProgLine)ProgLineAL[i-1]).IsNote)
							tbProg.SetFirstNoteRow(i);
						else
							tbProg.SetNoteRow(i);
						tbProg.Cell[2,i]=((ProgLine)ProgLineAL[i]).Note;
					}
					tbProg.SetTextColorRow(i,((ProgLine)ProgLineAL[i]).LineColor);
					//proc could still be highlighted if date not today because TP date.
					//only requirement is that it's attached to a procedure for today
					if(((ProgLine)ProgLineAL[i]).Type==ProgType.Proc){
						if(Appointments.ProcIsToday(ApptList,
							(Procedure)ProcAL[((ProgLine)ProgLineAL[i]).Index]))
						{
							tbProg.SetBackColorRow(i,Defs.Long[(int)DefCat.MiscColors][6].ItemColor);
						}
					}
				}//end for
				tbProg.LayoutTables();
		}//end FillProgNotes

		private void checkToday_CheckedChanged(object sender, System.EventArgs e) {
			if(checkToday.Checked){
				textDate.Text=DateTime.Today.ToShortDateString();
			}
			else{
				//
			}
		}

		///<summary>Gets run with each ModuleSelected.  Fills Dx, Priorities, ProcButtons, Date, and Image categories</summary>
    private void FillDxProcImage(){
			//if(textDate.errorProvider1.GetError(textDate)==""){
			if(checkToday.Checked){//textDate.Text=="" || 
				textDate.Text=DateTime.Today.ToShortDateString();
			}
			//}
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
			int selectedImageTab=tabControlImages.SelectedIndex;//retains current selection
			tabControlImages.TabPages.Clear();
			TabPage page;
			page=new TabPage();
			page.Text=Lan.g(this,"All");
			tabControlImages.TabPages.Add(page);
			visImageCats=new ArrayList();
			for(int i=0;i<Defs.Short[(int)DefCat.ImageCats].Length;i++){
				if(Defs.Short[(int)DefCat.ImageCats][i].ItemValue=="X"){//tagged to show in Chart
					visImageCats.Add(i);
					page=new TabPage();
					page.Text=Defs.Short[(int)DefCat.ImageCats][i].ItemName;
					tabControlImages.TabPages.Add(page);
				}
			}
			if(selectedImageTab<tabControlImages.TabCount){
				tabControlImages.SelectedIndex=selectedImageTab;
			}
    }

		///<summary>Gets run on ModuleSelected and each time a different images tab is selected. It first creates any missing thumbnails, then displays them. So it will be faster after the first time.</summary>
		private void FillImages(){
			visImages=new ArrayList();
			listViewImages.Items.Clear();
			imageListThumbnails.Images.Clear();
			if(PatCur==null){
				return;
			}
			if(patFolder==""){
				return;
			}
			if(!panelImages.Visible){
				return;
			}
			//create Thumbnails folder
			if(!Directory.Exists(patFolder+@"\Thumbnails")){
				try{
					Directory.CreateDirectory(patFolder+@"\Thumbnails");
				}
				catch{
					MessageBox.Show(Lan.g(this,"Error.  Could not create thumbnails folder. "));
					return;
				}
			}
			StringFormat notAvailFormat=new StringFormat();
			notAvailFormat.Alignment=StringAlignment.Center;
			notAvailFormat.LineAlignment=StringAlignment.Center;
			for(int i=0;i<DocumentList.Length;i++){
				if(!visImageCats.Contains(Defs.GetOrder(DefCat.ImageCats,DocumentList[i].DocCategory))){
					continue;//if category not visible, continue
				}
				if(tabControlImages.SelectedIndex>0){//any category except 'all'
					if(DocumentList[i].DocCategory!=Defs.Short[(int)DefCat.ImageCats]
						[(int)visImageCats[tabControlImages.SelectedIndex-1]].DefNum)
					{
						continue;//if not in category, continue
					}
				}
				//Documents.Cur=DocumentList[i];
				string thumbFileName=patFolder+@"Thumbnails\"+DocumentList[i].FileName;
				//Thumbs.db has nothing to do with Open Dental. It is a hidden Windows file.
				if(File.Exists(thumbFileName) 
					&& (Path.GetExtension(DocumentList[i].FileName).ToLower()==".jpg"
					|| Path.GetExtension(DocumentList[i].FileName).ToLower()==".gif"))
				{//use existing thumbnail
					imageListThumbnails.Images.Add(Bitmap.FromFile(thumbFileName));
				}
				else{//add thumbnail or create generic "not available"
					int thumbSize=imageListThumbnails.ImageSize.Width;//All thumbnails are square.
					Bitmap thumbBitmap=new Bitmap(thumbSize,thumbSize);
					Graphics g=Graphics.FromImage(thumbBitmap);
					g.InterpolationMode=InterpolationMode.High;
					if(File.Exists(patFolder+DocumentList[i].FileName)
						&& (Path.GetExtension(DocumentList[i].FileName).ToLower()==".jpg"
						|| Path.GetExtension(DocumentList[i].FileName).ToLower()==".gif"))
					{//create and save thumbnail
						Image fullImage=Image.FromFile(patFolder+DocumentList[i].FileName);
						float resizeRatio;
						if(fullImage.Width>fullImage.Height){//size by width
							resizeRatio=(float)thumbSize/(float)fullImage.Width;
						}
						else{//size by height
							resizeRatio=(float)thumbSize/(float)fullImage.Height;
						}
						int newWidth;
						int newHeight;
						int newX;
						int newY;
						newWidth=(int)((float)fullImage.Width*resizeRatio);
						newHeight=(int)((float)fullImage.Height*resizeRatio);
						newX=0;
						if(newWidth<thumbSize){
							newX=(thumbSize-newWidth)/2;
						}
						newY=0;
						if(newHeight<thumbSize){
							newY=(thumbSize-newHeight)/2;
						}
						switch(DocumentList[i].DegreesRotated){
							case 0:
								//
								break;
							case 90:
								g.RotateTransform(90,MatrixOrder.Append);
								g.TranslateTransform((float)thumbSize,0,MatrixOrder.Append);
								
								break;
							case 180:
								g.RotateTransform(180,MatrixOrder.Append);
								g.TranslateTransform((float)thumbSize,(float)thumbSize,MatrixOrder.Append);
								break;
							case 270:
								g.RotateTransform(270,MatrixOrder.Append);
								g.TranslateTransform(0,(float)thumbSize,MatrixOrder.Append);
								break;
						}
						if(DocumentList[i].IsFlipped){
							g.DrawImage(fullImage,newX+newWidth,newY,-newWidth,newHeight);
							g.ResetTransform();
							//handles annoying blue artifact:
							switch(DocumentList[i].DegreesRotated){
								case 0:
									g.DrawRectangle(new Pen(new SolidBrush(SystemColors.Control))
										,newX,newY,newWidth,newHeight);
									break;
								case 90:
									g.DrawRectangle(new Pen(new SolidBrush(SystemColors.Control))
										,newY+1,newX,newHeight,newWidth);
									break;
								case 180:
									g.DrawRectangle(new Pen(new SolidBrush(SystemColors.Control))
										,newX,newY,newWidth,newHeight);
									break;
								case 270:
									g.DrawRectangle(new Pen(new SolidBrush(SystemColors.Control))
										,newY+1,newX,newHeight,newWidth);
									break;
							}
						}
						else{
							g.DrawImage(fullImage,newX,newY,newWidth,newHeight);
							g.ResetTransform();
							//handles annoying blue artifact:
							switch(DocumentList[i].DegreesRotated){
								case 0:
									//g.DrawRectangle(new Pen(new SolidBrush(SystemColors.Control))
									//	,newX,newY,newWidth,newHeight);
									break;
								case 90:
									//g.DrawRectangle(new Pen(new SolidBrush(SystemColors.Control))
									//	,newY+1,newX,newHeight,newWidth);
									break;
								case 180:
									g.DrawRectangle(new Pen(new SolidBrush(SystemColors.Control))
										,newX,newY,newWidth,newHeight);
									break;
								case 270:
									g.DrawRectangle(new Pen(new SolidBrush(SystemColors.Control))
										,newY+1,newX,newHeight,newWidth);
									break;
							}
						}
						thumbBitmap.Save(thumbFileName);
					}//if File.Exists(original image)
					else{//original image not even present, or is not jpg or gif, so show default thumbnail
						g.FillRectangle(Brushes.Gray,0,0,thumbBitmap.Width,thumbBitmap.Height);
						g.DrawString("Thumbnail not available",Font,Brushes.Black,
							new RectangleF(0,0,thumbSize,thumbSize),notAvailFormat);
					}
					g.Dispose();
					imageListThumbnails.Images.Add(thumbBitmap);
				}//else add thumbnail
				visImages.Add(i);
				ListViewItem item=new ListViewItem(DocumentList[i].DateCreated.ToShortDateString()+": "
					+DocumentList[i].Description,imageListThumbnails.Images.Count-1);
        listViewImages.Items.Add(item);
			}//for
			
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
					Procedure ProcCur=((Procedure)ProcAL[((ProgLine)ProgLineAL[e.Row]).Index]).Copy();
					FormProcEdit FormPE=new FormProcEdit(ProcCur,PatCur.Copy(),FamCur,PlanList);
					FormPE.ShowDialog();
					if(FormPE.DialogResult!=DialogResult.OK)
						return;
					break;
				case ProgType.Rx:
					//MessageBox.Show(((ProgLine)ProgLineAL[e.Row]).Index.ToString());
					RxPats.Cur=((RxPat)RxAL[((ProgLine)ProgLineAL[e.Row]).Index]);
					FormRxEdit FormRxE = new FormRxEdit(PatCur);
					FormRxE.IsNew=false;
					FormRxE.ShowDialog();
					if(FormRxE.DialogResult!=DialogResult.OK) return;
					break;
			}
			ModuleSelected(PatCur.PatNum);
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

		///<summary>Sets many fields for a new procedure, then displays it for editing before inserting it into the db.  No need to worry about ProcOld because it's an insert, not an update.</summary>
		private void AddProcedure(Procedure ProcCur){
			//procnum
			ProcCur.PatNum=PatCur.PatNum;
			//aptnum
			//adacode
			if(newStatus==ProcStat.EO){
				ProcCur.ProcDate=DateTime.MinValue;
			}
			else if(textDate.errorProvider1.GetError(textDate)!=""){
				ProcCur.ProcDate=DateTime.Today;
			}
			else{
				ProcCur.ProcDate=PIn.PDate(textDate.Text);
			}
			if(newStatus==ProcStat.R || newStatus==ProcStat.EO || newStatus==ProcStat.EC)
				ProcCur.ProcFee=0;
			else
				ProcCur.ProcFee=Fees.GetAmount0(ProcCur.ADACode,Fees.GetFeeSched(PatCur,PlanList,PatPlanList));
			//ProcCur.OverridePri=-1;
			//ProcCur.OverrideSec=-1;
			//surf
			//ToothNum
			//Procedures.Cur.ToothRange
			//ProcCur.NoBillIns=ProcedureCodes.GetProcCode(ProcCur.ADACode).NoBillIns;
			if(comboPriority.SelectedIndex==0)
				ProcCur.Priority=0;
			else
				ProcCur.Priority=Defs.Short[(int)DefCat.TxPriorities][comboPriority.SelectedIndex-1].DefNum;
			ProcCur.ProcStatus=newStatus;
			ProcCur.ProcNote="";
			if(ProcedureCodes.GetProcCode(ProcCur.ADACode).IsHygiene
				&& PatCur.SecProv != 0){
				ProcCur.ProvNum=PatCur.SecProv;
			}
			else{
				ProcCur.ProvNum=PatCur.PriProv;
			}
			ProcCur.ClinicNum=PatCur.ClinicNum;
			if(listDx.SelectedIndex!=-1)
				ProcCur.Dx=Defs.Short[(int)DefCat.Diagnosis][listDx.SelectedIndex].DefNum;
			//nextaptnum
			ProcCur.DateEntryC=DateTime.Now;
			ProcCur.MedicalCode=ProcedureCodes.GetProcCode(ProcCur.ADACode).MedicalCode;
			ProcCur.Insert();
			ProcCur.ComputeEstimates(PatCur.PatNum,new ClaimProc[0],true,PlanList,PatPlanList);
			FormProcEdit FormPE=new FormProcEdit(ProcCur,PatCur.Copy(),FamCur,PlanList);
			FormPE.IsNew=true;
			FormPE.ShowDialog();
			if(FormPE.DialogResult==DialogResult.Cancel){
				//any created claimprocs are automatically deleted from within procEdit window.
				ProcCur.Delete();//also deletes the claimprocs
			}
			else{
				Recalls.Synch(PatCur.PatNum);
			}
		}
			
		private void AddQuick(Procedure ProcCur){
			//procnum
			ProcCur.PatNum=PatCur.PatNum;
			//aptnum
			//adacode
			if(newStatus==ProcStat.EO){
				ProcCur.ProcDate=DateTime.MinValue;
			}
			else if(textDate.errorProvider1.GetError(textDate)!=""){
				ProcCur.ProcDate=DateTime.Today;
			}
			else{
				ProcCur.ProcDate=PIn.PDate(textDate.Text);
			}
			if(newStatus==ProcStat.R || newStatus==ProcStat.EO || newStatus==ProcStat.EC)
				ProcCur.ProcFee=0;
			else
				ProcCur.ProcFee=Fees.GetAmount0(ProcCur.ADACode,Fees.GetFeeSched(PatCur,PlanList,PatPlanList));
			//ProcCur.OverridePri=-1;
			//ProcCur.OverrideSec=-1;
			//surf
			//toothnum
			//ToothRange
			//ProcCur.NoBillIns=ProcedureCodes.GetProcCode(ProcCur.ADACode).NoBillIns;
			if(comboPriority.SelectedIndex==0)
				ProcCur.Priority=0;
			else
				ProcCur.Priority=Defs.Short[(int)DefCat.TxPriorities][comboPriority.SelectedIndex-1].DefNum;
			ProcCur.ProcStatus=newStatus;
			ProcCur.ProcNote="";
			if(ProcedureCodes.GetProcCode(ProcCur.ADACode).IsHygiene
				&& PatCur.SecProv != 0){
				ProcCur.ProvNum=PatCur.SecProv;
			}
			else{
				ProcCur.ProvNum=PatCur.PriProv;
			}
			ProcCur.ClinicNum=PatCur.ClinicNum;
			if(listDx.SelectedIndex!=-1)
				ProcCur.Dx=Defs.Short[(int)DefCat.Diagnosis][listDx.SelectedIndex].DefNum;
			ProcCur.MedicalCode=ProcedureCodes.GetProcCode(ProcCur.ADACode).MedicalCode;
			//nextaptnum
			//ProcCur.CapCoPay=-1;
			//if(Patients.Cur.PriPlanNum!=0){//if patient has insurance
			//ProcCur.IsCovIns=true;
				//InsPlans.GetCur(Patients.Cur.PriPlanNum);
				//if(InsPlans.Cur.PlanType=="c"){
					//also handles fine if copayfeesched=0:
				//ProcCur.CapCoPay=Fees.GetAmount(ProcCur.ADACode,InsPlans.Cur.CopayFeeSched);
				//}
			//}
			//MessageBox.Show(Procedures.NewProcedure.ProcFee.ToString());
			//MessageBox.Show(Procedures.NewProcedure.ProcDate);
			//if(Procedures.Cur.ProcStatus==ProcStat.C){
			//	Procedures.PutBal(Procedures.Cur.ProcDate,Procedures.Cur.ProcFee);
			//}
			ProcCur.Insert();
			Recalls.Synch(PatCur.PatNum);
			ProcCur.ComputeEstimates(PatCur.PatNum,new ClaimProc[0],true,PlanList,PatPlanList);
		}

		private void butAddProc_Click(object sender, System.EventArgs e){
			if(newStatus==ProcStat.C){
				if(!Security.IsAuthorized(Permissions.ProcComplCreate)){
					return;
				}
			}
			bool isValid;
			TreatmentArea tArea;
			FormProcedures FormP=new FormProcedures();
			FormP.Mode=FormProcMode.Select;
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK) return;
			Procedures.SetDateFirstVisit(DateTime.Today,1,PatCur);
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
					//Procedures.Cur=ProcCur;
					AddProcedure(ProcCur);
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
					//Procedures.Cur=ProcCur;
					if(isValid)
						AddQuick(ProcCur);
					else
						AddProcedure(ProcCur);
				}
				else if(tArea==TreatmentArea.Tooth){
					if(cTeeth.SelectedTeeth.Length==0){
						//Procedures.Cur=ProcCur;
						AddProcedure(ProcCur);
					}
					else{
						ProcCur.ToothNum=cTeeth.SelectedTeeth[n];
						//Procedures.Cur=ProcCur;
						AddQuick(ProcCur);
					}
				}
				else if(tArea==TreatmentArea.ToothRange){
					if(cTeeth.SelectedTeeth.Length==0){
						//Procedures.Cur=ProcCur;
						AddProcedure(ProcCur);
					}
					else{
						ProcCur.ToothRange="";
						for(int b=0;b<cTeeth.SelectedTeeth.Length;b++){
							if(b!=0) ProcCur.ToothRange+=",";
							ProcCur.ToothRange+=cTeeth.SelectedTeeth[b];
						}
						//Procedures.Cur=ProcCur;
						AddProcedure(ProcCur);//it's nice to see the procedure to verify the range
					}
				}
				else if(tArea==TreatmentArea.Arch){
					if(cTeeth.SelectedTeeth.Length==0){
						//Procedures.Cur=ProcCur;
						AddProcedure(ProcCur);
						continue;
					}
					if(Tooth.IsMaxillary(cTeeth.SelectedTeeth[0])){
						ProcCur.Surf="U";
					}
					else{
						ProcCur.Surf="L";
					}
					//Procedures.Cur=ProcCur;
					AddQuick(ProcCur);
				}
				else if(tArea==TreatmentArea.Sextant){
					//Procedures.Cur=ProcCur;
					AddProcedure(ProcCur);
				}
				else{//mouth
					//Procedures.Cur=ProcCur;
					AddQuick(ProcCur);
				}
			}//for n
			ModuleSelected(PatCur.PatNum);
			if(newStatus==ProcStat.C){
				SecurityLogs.MakeLogEntry(Permissions.ProcComplCreate,PatCur.PatNum,
					PatCur.GetNameLF()+", "
					+DateTime.Today.ToShortDateString());
			}
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
			if(tbProg.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete Selected Item(s)?"),"",MessageBoxButtons.OKCancel)
				!=DialogResult.OK){
				return;
			}
			int skipped=0;
			for(int i=0;i<tbProg.SelectedIndices.Length;i++){
				switch(((ProgLine)ProgLineAL[tbProg.SelectedIndices[i]]).Type){
					case ProgType.Proc:
						if(((Procedure)ProcAL[((ProgLine)ProgLineAL[tbProg.SelectedIndices[i]]).Index]).ProcStatus==ProcStat.C){
							skipped++;
						}
						else{
							//also deletes the claimprocs:
							((Procedure)ProcAL[((ProgLine)ProgLineAL[tbProg.SelectedIndices[i]]).Index]).Delete();
						}
						break;
					case ProgType.Rx:		
						RxPats.Cur=(RxPat)RxAL[((ProgLine)ProgLineAL[tbProg.SelectedIndices[i]]).Index];
						RxPats.DeleteCur();
						break;
				}//switch
			}
			Recalls.Synch(PatCur.PatNum);
			if(skipped>0){
				MessageBox.Show(Lan.g(this,"Not allowed to delete completed procedures from here.")+"\r"
					+skipped.ToString()+" "+Lan.g(this,"item(s) skipped."));
			}
			ModuleSelected(PatCur.PatNum);
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

		private void checkShowTeeth_Click(object sender, System.EventArgs e) {
			FillProgNotes();
		}

		private void butShowAll_Click(object sender, System.EventArgs e) {
			checkShowTP.Checked=true;
			checkShowC.Checked=true;
			checkShowE.Checked=true;
			checkShowR.Checked=true;
			checkNotes.Checked=true;
			checkRx.Checked=true;
			checkShowTeeth.Checked=false;
			FillProgNotes();
		}

		private void butShowNone_Click(object sender, System.EventArgs e) {
			checkShowTP.Checked=false;
			checkShowC.Checked=false;
			checkShowE.Checked=false;
			checkShowR.Checked=false;
			checkNotes.Checked=false;
			checkRx.Checked=false;
			checkShowTeeth.Checked=false;
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
			FormMedical FormMedical2=new FormMedical(PatCur);
			FormMedical2.ShowDialog();
			ModuleSelected(PatCur.PatNum);
			//FillMedical();
		}

		private void checkDone_Click(object sender, System.EventArgs e) {
			Patient oldPat=PatCur.Copy();
			if(checkDone.Checked){
				if(ApptPlanned.Visible){
					if(MessageBox.Show(Lan.g(this,"Existing planned appointment will be deleted"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
						return; 
					Procedures.UnattachProcsInPlannedAppt(ApptPlanned.Info.MyApt.AptNum);
					ApptPlanned.Info.MyApt.Delete();
				}
				PatCur.NextAptNum=-1;
				PatCur.Update(oldPat);
			}
			else{
				PatCur.NextAptNum=0;
				PatCur.Update(oldPat);
			}
			ModuleSelected(PatCur.PatNum);
		}

		private void butNew_Click(object sender, System.EventArgs e) {
			if(ApptPlanned.Visible){
				if(MessageBox.Show(Lan.g(this,"Replace existing planned appointment?")
					,"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
					return;
				Procedures.UnattachProcsInPlannedAppt(ApptPlanned.Info.MyApt.AptNum);
				ApptPlanned.Info.MyApt.Delete();
			}
			Appointment AptCur=new Appointment();
			AptCur.PatNum=PatCur.PatNum;
			AptCur.ProvNum=PatCur.PriProv;
			AptCur.ClinicNum=PatCur.ClinicNum;
			AptCur.AptStatus=ApptStatus.Planned;
			AptCur.AptDateTime=DateTime.Today;
			AptCur.Pattern="/X/";
			AptCur.InsertOrUpdate(null,true);
			FormApptEdit FormApptEdit2=new FormApptEdit(AptCur);
			FormApptEdit2.IsNew=true;
			FormApptEdit2.ShowDialog();
			if(FormApptEdit2.DialogResult!=DialogResult.OK){
				//delete new appt and unattach procs already handled in dialog
				//not needed: Patients.Cur.NextAptNum=0;
				FillPlanned();
				return;
			}
			ProcList=Procedures.Refresh(PatCur.PatNum);
			bool allProcsHyg=true;
			for(int i=0;i<ProcList.Length;i++){
				if(ProcList[i].NextAptNum!=AptCur.AptNum)
					continue;//only concerned with procs on this nextApt
				if(!ProcedureCodes.GetProcCode(ProcList[i].ADACode).IsHygiene){
					allProcsHyg=false;
					break;
				}
			}
			if(allProcsHyg && PatCur.SecProv!=0){
				Appointment aptOld=AptCur.Copy();
				AptCur.ProvNum=PatCur.SecProv;
				AptCur.InsertOrUpdate(aptOld,false);
			}
			Patient patOld=PatCur.Copy();
			PatCur.NextAptNum=AptCur.AptNum;
			PatCur.Update(patOld);
			ModuleSelected(PatCur.PatNum);//if procs were added in appt, then this will display them
		}

		private void butClear_Click(object sender, System.EventArgs e) {
			if(!ApptPlanned.Visible){
				MessageBox.Show(Lan.g(this,"No planned appointment is present."));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete planned appointment?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
				return;
			Procedures.UnattachProcsInPlannedAppt(ApptPlanned.Info.MyApt.AptNum);
			ApptPlanned.Info.MyApt.Delete();
			Patient patOld=PatCur.Copy();
			PatCur.NextAptNum=0;
			PatCur.Update(patOld);
			ModuleSelected(PatCur.PatNum);
			//FillNext();
		}

		private void ApptPlanned_DoubleClick(object sender, System.EventArgs e){
			FormApptEdit FormAE=new FormApptEdit(ApptPlanned.Info.MyApt);
			FormAE.ShowDialog();
			ModuleSelected(PatCur.PatNum);//if procs were added in appt, then this will display them
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

		private void listProcButtons_Click(object sender, System.EventArgs e) {
			if(newStatus==ProcStat.C){
				if(!Security.IsAuthorized(Permissions.ProcComplCreate)){
					return;
				}
			}
			if(listProcButtons.SelectedIndex==-1){
				return;
			}
			Procedures.SetDateFirstVisit(DateTime.Today,1,PatCur);
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
						AddQuick(ProcCur);
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
						if(isValid)
							AddQuick(ProcCur);
						else
							AddProcedure(ProcCur);
					}
					else if(tArea==TreatmentArea.Tooth){
						if(cTeeth.SelectedTeeth.Length==0){
							AddProcedure(ProcCur);
						}
						else{
							ProcCur.ToothNum=cTeeth.SelectedTeeth[n];
							AddQuick(ProcCur);
						}
					}
					else if(tArea==TreatmentArea.ToothRange){
						if(cTeeth.SelectedTeeth.Length==0){
							AddProcedure(ProcCur);
						}
						else{
							ProcCur.ToothRange="";
							for(int b=0;b<cTeeth.SelectedTeeth.Length;b++){
								if(b!=0) ProcCur.ToothRange+=",";
								ProcCur.ToothRange+=cTeeth.SelectedTeeth[b];
							}
							AddQuick(ProcCur);
						}
					}
					else if(tArea==TreatmentArea.Arch){
						if(cTeeth.SelectedTeeth.Length==0){
							AddProcedure(ProcCur);
							continue;
						}
						if(Tooth.IsMaxillary(cTeeth.SelectedTeeth[0])){
							ProcCur.Surf="U";
						}
						else{
							ProcCur.Surf="L";
						}
						AddQuick(ProcCur);
					}
					else if(tArea==TreatmentArea.Sextant){
						AddProcedure(ProcCur);
					}
					else{//mouth
						AddQuick(ProcCur);
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
							,surf,isAdditional,PatCur.PatNum,PatCur.Age);
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
						AddQuick(ProcCur);
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
						if(isValid)
							AddQuick(ProcCur);
						else
							AddProcedure(ProcCur);
					}
					else if(tArea==TreatmentArea.Tooth){
						if(cTeeth.SelectedTeeth.Length==0){
							AddProcedure(ProcCur);
						}
						else{
							ProcCur.ToothNum=cTeeth.SelectedTeeth[n];
							AddQuick(ProcCur);
						}
					}
					else if(tArea==TreatmentArea.ToothRange){
						if(cTeeth.SelectedTeeth.Length==0){
							AddProcedure(ProcCur);
						}
						else{
							ProcCur.ToothRange="";
							for(int b=0;b<cTeeth.SelectedTeeth.Length;b++){
								if(b!=0) ProcCur.ToothRange+=",";
								ProcCur.ToothRange+=cTeeth.SelectedTeeth[b];
							}
							AddQuick(ProcCur);
						}
					}
					else if(tArea==TreatmentArea.Arch){
						if(cTeeth.SelectedTeeth.Length==0){
							AddProcedure(ProcCur);
							continue;
						}
						if(Tooth.IsMaxillary(cTeeth.SelectedTeeth[0])){
							ProcCur.Surf="U";
						}
						else{
							ProcCur.Surf="L";
						}
						AddQuick(ProcCur);
					}
					else if(tArea==TreatmentArea.Sextant){
						AddProcedure(ProcCur);
					}
					else{//mouth
						AddQuick(ProcCur);
					}
				}//n selected teeth
			}//for i
			ModuleSelected(PatCur.PatNum);
			if(newStatus==ProcStat.C){
				SecurityLogs.MakeLogEntry(Permissions.ProcComplCreate,PatCur.PatNum,
					PatCur.GetNameLF()+", "
					+DateTime.Today.ToShortDateString());
			}
		}

		private void butPrimary_Click(object sender, System.EventArgs e) {
			//contextMenu1.Show(butPrimary,new Point(0,30));
		}

		private void menuPrimaryAll_Click(object sender, System.EventArgs e) {
			Patient patOld=PatCur.Copy();
			PatCur.PrimaryTeeth="";
			for(int i=1;i<=32;i++){
				if(i>1){
					PatCur.PrimaryTeeth+=",";
				}
				PatCur.PrimaryTeeth+=i.ToString();
			}
			PatCur.Update(patOld);
			ModuleSelected(PatCur.PatNum);
		}

		private void menuPrimaryNone_Click(object sender, System.EventArgs e) {
			Patient patOld=PatCur.Copy();
			PatCur.PrimaryTeeth="";
			PatCur.Update(patOld);
			ModuleSelected(PatCur.PatNum);
		}

		private void menuPrimaryToggle_Click(object sender, System.EventArgs e) {
			if(cTeeth.SelectedTeeth.Length==0){
				return;
			}
			string[] oldPriTeeth=PatCur.PrimaryTeeth.Split(',');
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
			Patient patOld=PatCur.Copy();
			PatCur.PrimaryTeeth="";
			for(int i=0;i<ALpri.Count;i++){
				if(i>0){
					PatCur.PrimaryTeeth+=",";
				}
				PatCur.PrimaryTeeth+=ALpri[i];
			}
			PatCur.Update(patOld);
			ModuleSelected(PatCur.PatNum);
		}

		private void textADACode_Enter(object sender, System.EventArgs e) {
			if(textADACode.Text==Lan.g(this,"Type ADA Code")){
				textADACode.Text="";
			}
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
			if(newStatus==ProcStat.C){
				if(!Security.IsAuthorized(Permissions.ProcComplCreate)){
					return;
				}
			}
			if(!ProcedureCodes.HList.ContainsKey(textADACode.Text)){
				MessageBox.Show(Lan.g(this,"Invalid code."));
				//textADACode.Text="";
				textADACode.SelectionStart=textADACode.Text.Length;
				return;
			}
			Procedures.SetDateFirstVisit(DateTime.Today,1,PatCur);
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
					AddQuick(ProcCur);
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
					if(isValid)
						AddQuick(ProcCur);
					else
						AddProcedure(ProcCur);
				}
				else if(tArea==TreatmentArea.Tooth){
					if(cTeeth.SelectedTeeth.Length==0){
						AddProcedure(ProcCur);
					}
					else{
						ProcCur.ToothNum=cTeeth.SelectedTeeth[n];
						AddQuick(ProcCur);
					}
				}
				else if(tArea==TreatmentArea.ToothRange){
					if(cTeeth.SelectedTeeth.Length==0){
						AddProcedure(ProcCur);
					}
					else{
						ProcCur.ToothRange="";
						for(int b=0;b<cTeeth.SelectedTeeth.Length;b++){
							if(b!=0) ProcCur.ToothRange+=",";
							ProcCur.ToothRange+=cTeeth.SelectedTeeth[b];
						}
						AddQuick(ProcCur);
					}
				}
				else if(tArea==TreatmentArea.Arch){
					if(cTeeth.SelectedTeeth.Length==0){
						AddProcedure(ProcCur);
						continue;
					}
					if(Tooth.IsMaxillary(cTeeth.SelectedTeeth[0])){
						ProcCur.Surf="U";
					}
					else{
						ProcCur.Surf="L";
					}
					AddQuick(ProcCur);
				}
				else if(tArea==TreatmentArea.Sextant){
					AddProcedure(ProcCur);
				}
				else{//mouth
					AddQuick(ProcCur);
				}
			}//n selected teeth
			ModuleSelected(PatCur.PatNum);
			textADACode.Text="";
			textADACode.Select();
			if(newStatus==ProcStat.C){
				SecurityLogs.MakeLogEntry(Permissions.ProcComplCreate,PatCur.PatNum,
					PatCur.GetNameLF()+", "
					+DateTime.Today.ToShortDateString());
			}
		}

		private void cTeeth_Click(object sender, System.EventArgs e) {
			if(cTeeth.SelectedTeeth.Length==1){
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
			if(checkShowTeeth.Checked){
				FillProgNotes();
			}
		}

		private void tbProg_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(e.Button==MouseButtons.Right){
				menuProgRight.Show(tbProg,new Point(e.X,e.Y));
			}
		}

		private void menuItemPrintProg_Click(object sender, System.EventArgs e) {
			linesPrinted=0;
			headingPrinted=false;
			#if DEBUG
				PrintReport(true);
			#else
				PrintReport(false);	
			#endif
		}

		///<summary>Preview is only used for debugging.</summary>
		public void PrintReport(bool justPreview){
			pd2=new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			pd2.DefaultPageSettings.Margins=new Margins(25,25,40,25);
			try{
				if(justPreview){
					FormRpPrintPreview pView = new FormRpPrintPreview();
					pView.printPreviewControl2.Document=pd2;
					pView.ShowDialog();				
			  }
				else{
					if(Printers.SetPrinter(pd2,PrintSituation.Default)){
						pd2.Print();
					}
				}
			}
			catch{
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
		}

		private void pd2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
			//printable area is w=800.
			//Height is 1035 for standard paper.  Some printers can handle up to 1042.
			Graphics g=e.Graphics;
			string text;
			Font headingFont=new Font("Arial",13,FontStyle.Bold);
			Font subHeadingFont=new Font("Arial",10,FontStyle.Bold);
			Font bodyFont=new Font("Arial",9);
			Font totalFont=new Font("Arial",9,FontStyle.Bold);
			int yPos=44;
			//int xPos=50;
			#region printHeading
			if(!headingPrinted){
				text="Chart Progress Notes";
				g.DrawString(text,headingFont,Brushes.Black,400-g.MeasureString(text,headingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,headingFont).Height;
				text=PatCur.GetNameFL();
				g.DrawString(text,subHeadingFont,Brushes.Black,400-g.MeasureString(text,subHeadingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
				text=DateTime.Today.ToShortDateString();
				g.DrawString(text,subHeadingFont,Brushes.Black,400-g.MeasureString(text,subHeadingFont).Width/2,yPos);
				yPos+=30;
				headingPrinted=true;
			}
			#endregion
			#region ColDefs
			//always defines and always prints on each page
			int rowH=(int)g.MeasureString("anything",bodyFont).Height;
			int[] colW=new int[8];
			colW[0]=80;
			colW[1]=30;
			colW[2]=53;
			colW[3]=30;
			colW[4]=325;
			colW[5]=30;
			colW[6]=55;
			colW[7]=60;
			int[] colPos=new int[colW.Length+1];//last entry represents the right side of the last col
			for(int i=0;i<colW.Length;i++){
				if(i==0){
					colPos[i]=75;
					continue;
				}
				colPos[i]=colPos[i-1]+colW[i-1];
				if(i==colW.Length-1){
					colPos[i+1]=colPos[i]+colW[i];
				}
			}
			HorizontalAlignment[] colAlign=new HorizontalAlignment[8];
			colAlign[7]=HorizontalAlignment.Right;
			string[] ColCaption=new string[8];
			ColCaption[0]=Lan.g("TableProg","Date");
			ColCaption[1]=Lan.g("TableProg","Th");
			ColCaption[2]=Lan.g("TableProg","Surf");
			ColCaption[3]=Lan.g("TableProg","Dx");
			ColCaption[4]=Lan.g("TableProg","Description");
			ColCaption[5]=Lan.g("TableProg","Stat");
			ColCaption[6]=Lan.g("TableProg","Prov");
			ColCaption[7]=Lan.g("TableProg","Amount");
			g.FillRectangle(Brushes.LightGray,colPos[0],yPos,colPos[colPos.Length-1]-colPos[0],16);
			g.DrawRectangle(Pens.Black,colPos[0],yPos,colPos[colPos.Length-1]-colPos[0],16);  
			for(int i=1;i<colPos.Length;i++) 
				g.DrawLine(new Pen(Color.Black),colPos[i],yPos,colPos[i],yPos+16);
			//Prints the Column Titles
			for(int i=0;i<ColCaption.Length;i++){ 
				if(colAlign[i]==HorizontalAlignment.Right){
					g.DrawString(ColCaption[i],totalFont,Brushes.Black,colPos[i+1]-g.MeasureString(ColCaption[i],totalFont).Width-1,yPos);
				}
				else 
					g.DrawString(Lan.g(this,ColCaption[i]),totalFont,Brushes.Black,colPos[i]+1,yPos);
			}
			yPos+=16;
			#endregion
			#region printBody
			while(linesPrinted < ProgLineAL.Count 
				&& yPos+g.MeasureString(((ProgLine)ProgLineAL[linesPrinted]).Note,bodyFont,colPos[5]-colPos[4]).Height < e.MarginBounds.Height)
			{
				if(((ProgLine)ProgLineAL[linesPrinted]).IsNote){
					text=((ProgLine)ProgLineAL[linesPrinted]).Note;
					g.DrawString(text,bodyFont,Brushes.Black,new RectangleF(colPos[2]+1,yPos,colPos[8]-colPos[2]-4,bodyFont.GetHeight(g)));
					//Column lines		
					for(int i=0;i<colPos.Length-1;i++){
						//left vertical
						if(i<3){
	  					g.DrawLine(Pens.Gray,colPos[i],yPos+rowH,colPos[i],yPos);
						}
						//lower
						if(linesPrinted==ProgLineAL.Count || !((ProgLine)ProgLineAL[linesPrinted+1]).IsNote){
							g.DrawLine(Pens.Gray,colPos[i],yPos+rowH,colPos[i+1],yPos+rowH);
						}
					}
					//right vertical
					g.DrawLine(Pens.Gray,colPos[colPos.Length-1],yPos+rowH,colPos[colPos.Length-1],yPos);
					yPos+=rowH;
					linesPrinted++;
					continue;
				}
				for(int i=0;i<colPos.Length-1;i++){
					switch(i){
						case 0:
							text=((ProgLine)ProgLineAL[linesPrinted]).Date;
							break;
						case 1:
							text=((ProgLine)ProgLineAL[linesPrinted]).Th;
							break;
						case 2:
							text=((ProgLine)ProgLineAL[linesPrinted]).Surf;
							break;
						case 3:
							text=((ProgLine)ProgLineAL[linesPrinted]).Dx;
							break;
						case 4:
							text=((ProgLine)ProgLineAL[linesPrinted]).Description;
							break;
						case 5:
							text=((ProgLine)ProgLineAL[linesPrinted]).Stat;
							break;
						case 6:
							text=((ProgLine)ProgLineAL[linesPrinted]).Prov;
							break;
						case 7:
							text=((ProgLine)ProgLineAL[linesPrinted]).Amount;
							break;
						default:
							text="";
							break;
					}
  				if(colAlign[i]==HorizontalAlignment.Right){
						g.DrawString(text,bodyFont,Brushes.Black,colPos[i+1]-g.MeasureString(text,bodyFont).Width-1,yPos);
					}
					else{
						g.DrawString(text,bodyFont,Brushes.Black,new RectangleF(colPos[i]+1,yPos,colPos[i+1]-colPos[i]-4,bodyFont.GetHeight(g)));
					}
					//left vertical
					g.DrawLine(Pens.Gray,colPos[i],yPos+rowH,colPos[i],yPos);
					//lower
					g.DrawLine(Pens.Gray,colPos[i],yPos+rowH,colPos[i+1],yPos+rowH);
				} 
				//right vertical
				g.DrawLine(Pens.Gray,colPos[colPos.Length-1],yPos+rowH,colPos[colPos.Length-1],yPos);
				yPos+=rowH;
				linesPrinted++;
			}
			#endregion
			if(linesPrinted < ProgLineAL.Count){
				e.HasMorePages=true;
			}
			else{
				e.HasMorePages=false;
			}
		}

		///<summary>Draws one button for the tabControlImages.</summary>
		private void OnDrawItem(object sender, DrawItemEventArgs e){
      Graphics g=e.Graphics;
      Pen penBlue=new Pen(Color.FromArgb(97,136,173));
			Pen penRed=new Pen(Color.FromArgb(140,51,46));
			Pen penOrange=new Pen(Color.FromArgb(250,176,3),2);
			Pen penDkOrange=new Pen(Color.FromArgb(227,119,4));
			SolidBrush brBlack=new SolidBrush(Color.Black);
			int selected=tabControlImages.TabPages.IndexOf(tabControlImages.SelectedTab);
			Rectangle bounds=e.Bounds;
			Rectangle rect=new Rectangle(bounds.X+2,bounds.Y+1,bounds.Width-5,bounds.Height-4);
			if(e.Index==selected){
				g.FillRectangle(new SolidBrush(Color.White),rect);
				//g.DrawRectangle(penBlue,rect);
				g.DrawLine(penOrange,rect.X,rect.Bottom-1,rect.Right,rect.Bottom-1);
				g.DrawLine(penDkOrange,rect.X+1,rect.Bottom,rect.Right-2,rect.Bottom);
				g.DrawString(tabControlImages.TabPages[e.Index].Text,Font,brBlack,bounds.X+3,bounds.Y+6);
			}
			else{
				g.DrawString(tabControlImages.TabPages[e.Index].Text,Font,brBlack,bounds.X,bounds.Y);
			}
    }

		private void panelImages_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(e.Y>3){
				return;
			}
			MouseIsDownOnImageSplitter=true;
			ImageSplitterOriginalY=panelImages.Top;
			OriginalImageMousePos=panelImages.Top+e.Y;
		}

		private void panelImages_MouseLeave(object sender, System.EventArgs e) {
			//not needed.
		}

		private void panelImages_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(!MouseIsDownOnImageSplitter){
				if(e.Y<=3){
					panelImages.Cursor=Cursors.HSplit;
				}
				else{
					panelImages.Cursor=Cursors.Default;
				}
				return;
			}
			//panelNewTop
			int panelNewH=panelImages.Bottom
				-(ImageSplitterOriginalY+(panelImages.Top+e.Y)-OriginalImageMousePos);//-top
			if(panelNewH<10)//cTeeth.Bottom)
				panelNewH=10;//cTeeth.Bottom;//keeps it from going too low
			if(panelNewH>panelImages.Bottom-cTeeth.Bottom)
				panelNewH=panelImages.Bottom-cTeeth.Bottom;//keeps it from going too high
			panelImages.Height=panelNewH;
			//tbProg.LayoutTables();//too much flicker
		}

		private void panelImages_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(!MouseIsDownOnImageSplitter){
				return;
			}
			MouseIsDownOnImageSplitter=false;
			//tbProg.Height=ClientSize.Height-panelImages.Top-tbProg.Location.Y-2;
			tbProg.LayoutTables();
		}

		private void tabControlImages_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(selectedImageTab==-1){
				selectedImageTab=tabControlImages.SelectedIndex;
				return;
			}
			Rectangle rect=tabControlImages.GetTabRect(selectedImageTab);
			if(rect.Contains(e.X,e.Y)){//clicked on the already selected tab
				if(panelImages.Visible){
					panelImages.Visible=false;
				}
				else{
					panelImages.Visible=true;
				}
				tbProg.LayoutTables();
			}
			else{//clicked on a new tab
				if(!panelImages.Visible){
					panelImages.Visible=true;
					tbProg.LayoutTables();
				}
			}
			selectedImageTab=tabControlImages.SelectedIndex;
			FillImages();//it will not actually fill the images unless panelImages is visible
		}

		private void listViewImages_DoubleClick(object sender, System.EventArgs e) {
			if(listViewImages.SelectedIndices.Count==0){
				return;//clicked on white space.
			}
			Document DocCur=DocumentList[(int)visImages[listViewImages.SelectedIndices[0]]];
			if(Path.GetExtension(DocCur.FileName).ToLower()!=".jpg"
				&& Path.GetExtension(DocCur.FileName).ToLower()!=".gif")
			{
				try{
					Process.Start(patFolder+DocCur.FileName);
				}
				catch(Exception ex){
					MessageBox.Show(ex.Message);
				}
				return;
			}
			if(formImageViewer==null || !formImageViewer.Visible){
				formImageViewer=new FormImageViewer();
				formImageViewer.Show();
			}
			if(formImageViewer.WindowState==FormWindowState.Minimized){
				formImageViewer.WindowState=FormWindowState.Normal;
			}
			formImageViewer.BringToFront();
			formImageViewer.SetImage(DocCur,PatCur.GetNameLF()+" - "
				+DocCur.DateCreated.ToShortDateString()+": "+DocCur.Description);
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
