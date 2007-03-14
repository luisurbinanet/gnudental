/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormProcEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textProc;
		private System.Windows.Forms.TextBox textSurfaces;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textDesc;
		private System.Windows.Forms.Label label7;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.TextBox textRange;
		private System.ComponentModel.IContainer components;// Required designer variable.
		private System.Windows.Forms.Label labelTooth;
		private System.Windows.Forms.Label labelRange;
		private System.Windows.Forms.Label labelSurfaces;
		private System.Windows.Forms.GroupBox groupQuadrant;
		private System.Windows.Forms.RadioButton radioUR;
		private System.Windows.Forms.RadioButton radioUL;
		private System.Windows.Forms.RadioButton radioLL;
		private System.Windows.Forms.RadioButton radioLR;
		private System.Windows.Forms.GroupBox groupArch;
		private System.Windows.Forms.RadioButton radioU;
		private System.Windows.Forms.RadioButton radioL;
		private System.Windows.Forms.GroupBox groupSextant;
		private System.Windows.Forms.RadioButton radioS1;
		private System.Windows.Forms.RadioButton radioS3;
		private System.Windows.Forms.RadioButton radioS2;
		private System.Windows.Forms.RadioButton radioS4;
		private System.Windows.Forms.RadioButton radioS5;
		private System.Windows.Forms.RadioButton radioS6;
		private System.Windows.Forms.ListBox listDx;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ListBox listProvNum;
		private OpenDental.ValidDate textDate;
		///<summary>Mostly used for permissions.</summary>
		public bool IsNew;
		private System.Windows.Forms.RadioButton radioStatusEO;
		private System.Windows.Forms.RadioButton radioStatusEC;
		private System.Windows.Forms.RadioButton radioStatusC;
		private System.Windows.Forms.RadioButton radioStatusTP;
		private System.Windows.Forms.RadioButton radioStatusR;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupStatus;
		private System.Windows.Forms.Label labelClaim;
		private System.Windows.Forms.ListBox listBoxTeeth;
		private System.Windows.Forms.ListBox listBoxTeeth2;
		private OpenDental.UI.Button butChange;
		//private ProcStat OriginalStatus;
		private System.Windows.Forms.TextBox textTooth;
		private System.Windows.Forms.ErrorProvider errorProvider2;
		private OpenDental.UI.Button butEditAnyway;
		private System.Windows.Forms.Label labelDx;
		private System.Windows.Forms.ComboBox comboPlaceService;
		private System.Windows.Forms.Label labelPlaceService;
		private OpenDental.UI.Button butSetComplete;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ListBox listPriority;
		private ProcedureCode ProcedureCode2;
		private System.Windows.Forms.Label label13;
		private OpenDental.TableProcIns tbIns;
		private OpenDental.UI.Button butAddEstimate;
		private Procedure ProcCur;
		private Procedure ProcOld;
		private ClaimProc[] ClaimProcList;
		private OpenDental.ValidDouble textProcFee;
		private System.Windows.Forms.CheckBox checkNoBillIns;
		private OpenDental.ODtextBox textNotes;
		private System.Windows.Forms.CheckBox checkHideGraphical;
		private System.Windows.Forms.GroupBox groupProsth;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label labelClaimNote;
		private System.Windows.Forms.ListBox listProsth;
		private OpenDental.ValidDate textDateOriginalProsth;
		private OpenDental.ODtextBox textClaimNote;
		private System.Windows.Forms.TextBox textDateLocked;
		private OpenDental.UI.Button butLock;
		private System.Windows.Forms.Label labelLocked;
		private ClaimProc[] ClaimProcsForProc;
		private Patient PatCur;
		private Family FamCur;
		private InsPlan[] PlanList;

		///<summary>Inserts are no longer done within this dialog, but must be done ahead of time from outside.You must specify a procedure to edit, and only the changes that are made in this dialog get saved.  Only used when double click in Account, Chart, TP, and in ContrChart.AddProcedure().  The procedure may be deleted if new and user hits Cancel.</summary>
		public FormProcEdit(Procedure proc,Patient patCur,Family famCur,InsPlan[] planList){
			ProcCur=proc;
			ProcOld=proc.Copy();
			PatCur=patCur;
			FamCur=famCur;
			PlanList=planList;
			InitializeComponent();
			Lan.F(this);
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

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.labelTooth = new System.Windows.Forms.Label();
			this.labelSurfaces = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textProc = new System.Windows.Forms.TextBox();
			this.textTooth = new System.Windows.Forms.TextBox();
			this.textSurfaces = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.groupStatus = new System.Windows.Forms.GroupBox();
			this.radioStatusR = new System.Windows.Forms.RadioButton();
			this.radioStatusEO = new System.Windows.Forms.RadioButton();
			this.radioStatusEC = new System.Windows.Forms.RadioButton();
			this.radioStatusC = new System.Windows.Forms.RadioButton();
			this.radioStatusTP = new System.Windows.Forms.RadioButton();
			this.textDesc = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.labelRange = new System.Windows.Forms.Label();
			this.textRange = new System.Windows.Forms.TextBox();
			this.groupQuadrant = new System.Windows.Forms.GroupBox();
			this.radioLR = new System.Windows.Forms.RadioButton();
			this.radioLL = new System.Windows.Forms.RadioButton();
			this.radioUL = new System.Windows.Forms.RadioButton();
			this.radioUR = new System.Windows.Forms.RadioButton();
			this.groupArch = new System.Windows.Forms.GroupBox();
			this.radioL = new System.Windows.Forms.RadioButton();
			this.radioU = new System.Windows.Forms.RadioButton();
			this.groupSextant = new System.Windows.Forms.GroupBox();
			this.radioS6 = new System.Windows.Forms.RadioButton();
			this.radioS5 = new System.Windows.Forms.RadioButton();
			this.radioS4 = new System.Windows.Forms.RadioButton();
			this.radioS2 = new System.Windows.Forms.RadioButton();
			this.radioS3 = new System.Windows.Forms.RadioButton();
			this.radioS1 = new System.Windows.Forms.RadioButton();
			this.listProvNum = new System.Windows.Forms.ListBox();
			this.listDx = new System.Windows.Forms.ListBox();
			this.label9 = new System.Windows.Forms.Label();
			this.labelDx = new System.Windows.Forms.Label();
			this.textDate = new OpenDental.ValidDate();
			this.textProcFee = new OpenDental.ValidDouble();
			this.panel1 = new System.Windows.Forms.Panel();
			this.listBoxTeeth2 = new System.Windows.Forms.ListBox();
			this.listBoxTeeth = new System.Windows.Forms.ListBox();
			this.butChange = new OpenDental.UI.Button();
			this.labelClaim = new System.Windows.Forms.Label();
			this.errorProvider2 = new System.Windows.Forms.ErrorProvider();
			this.butEditAnyway = new OpenDental.UI.Button();
			this.comboPlaceService = new System.Windows.Forms.ComboBox();
			this.labelPlaceService = new System.Windows.Forms.Label();
			this.butSetComplete = new OpenDental.UI.Button();
			this.label10 = new System.Windows.Forms.Label();
			this.listPriority = new System.Windows.Forms.ListBox();
			this.label13 = new System.Windows.Forms.Label();
			this.tbIns = new OpenDental.TableProcIns();
			this.butAddEstimate = new OpenDental.UI.Button();
			this.checkNoBillIns = new System.Windows.Forms.CheckBox();
			this.textNotes = new OpenDental.ODtextBox();
			this.checkHideGraphical = new System.Windows.Forms.CheckBox();
			this.groupProsth = new System.Windows.Forms.GroupBox();
			this.listProsth = new System.Windows.Forms.ListBox();
			this.textDateOriginalProsth = new OpenDental.ValidDate();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textClaimNote = new OpenDental.ODtextBox();
			this.labelClaimNote = new System.Windows.Forms.Label();
			this.labelLocked = new System.Windows.Forms.Label();
			this.textDateLocked = new System.Windows.Forms.TextBox();
			this.butLock = new OpenDental.UI.Button();
			this.groupStatus.SuspendLayout();
			this.groupQuadrant.SuspendLayout();
			this.groupArch.SuspendLayout();
			this.groupSextant.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupProsth.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(70, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(5, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(79, 12);
			this.label2.TabIndex = 1;
			this.label2.Text = "Procedure";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelTooth
			// 
			this.labelTooth.Location = new System.Drawing.Point(46, 58);
			this.labelTooth.Name = "labelTooth";
			this.labelTooth.Size = new System.Drawing.Size(36, 12);
			this.labelTooth.TabIndex = 2;
			this.labelTooth.Text = "Tooth";
			this.labelTooth.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.labelTooth.Visible = false;
			// 
			// labelSurfaces
			// 
			this.labelSurfaces.Location = new System.Drawing.Point(11, 86);
			this.labelSurfaces.Name = "labelSurfaces";
			this.labelSurfaces.Size = new System.Drawing.Size(73, 16);
			this.labelSurfaces.TabIndex = 3;
			this.labelSurfaces.Text = "Surfaces";
			this.labelSurfaces.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.labelSurfaces.Visible = false;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(9, 113);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(75, 16);
			this.label5.TabIndex = 4;
			this.label5.Text = "Amount";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textProc
			// 
			this.textProc.Location = new System.Drawing.Point(84, 28);
			this.textProc.Name = "textProc";
			this.textProc.ReadOnly = true;
			this.textProc.Size = new System.Drawing.Size(76, 20);
			this.textProc.TabIndex = 6;
			this.textProc.Text = "";
			// 
			// textTooth
			// 
			this.textTooth.Location = new System.Drawing.Point(84, 56);
			this.textTooth.Name = "textTooth";
			this.textTooth.Size = new System.Drawing.Size(28, 20);
			this.textTooth.TabIndex = 7;
			this.textTooth.Text = "";
			this.textTooth.Visible = false;
			this.textTooth.Validating += new System.ComponentModel.CancelEventHandler(this.textTooth_Validating);
			// 
			// textSurfaces
			// 
			this.textSurfaces.Location = new System.Drawing.Point(84, 84);
			this.textSurfaces.Name = "textSurfaces";
			this.textSurfaces.Size = new System.Drawing.Size(68, 20);
			this.textSurfaces.TabIndex = 4;
			this.textSurfaces.Text = "";
			this.textSurfaces.Visible = false;
			this.textSurfaces.Validating += new System.ComponentModel.CancelEventHandler(this.textSurfaces_Validating);
			this.textSurfaces.TextChanged += new System.EventHandler(this.textSurfaces_TextChanged);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(166, 10);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(140, 16);
			this.label6.TabIndex = 13;
			this.label6.Text = "Procedure Description:";
			// 
			// groupStatus
			// 
			this.groupStatus.Controls.Add(this.radioStatusR);
			this.groupStatus.Controls.Add(this.radioStatusEO);
			this.groupStatus.Controls.Add(this.radioStatusEC);
			this.groupStatus.Controls.Add(this.radioStatusC);
			this.groupStatus.Controls.Add(this.radioStatusTP);
			this.groupStatus.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupStatus.Location = new System.Drawing.Point(656, 3);
			this.groupStatus.Name = "groupStatus";
			this.groupStatus.Size = new System.Drawing.Size(148, 130);
			this.groupStatus.TabIndex = 4;
			this.groupStatus.TabStop = false;
			this.groupStatus.Text = "Procedure Status";
			// 
			// radioStatusR
			// 
			this.radioStatusR.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioStatusR.Location = new System.Drawing.Point(12, 106);
			this.radioStatusR.Name = "radioStatusR";
			this.radioStatusR.Size = new System.Drawing.Size(131, 16);
			this.radioStatusR.TabIndex = 4;
			this.radioStatusR.Text = "Referred Out";
			this.radioStatusR.Click += new System.EventHandler(this.radioStatusR_Click);
			// 
			// radioStatusEO
			// 
			this.radioStatusEO.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioStatusEO.Location = new System.Drawing.Point(12, 84);
			this.radioStatusEO.Name = "radioStatusEO";
			this.radioStatusEO.Size = new System.Drawing.Size(128, 16);
			this.radioStatusEO.TabIndex = 3;
			this.radioStatusEO.Text = "Existing-Other Prov";
			this.radioStatusEO.Click += new System.EventHandler(this.radioEO_Click);
			// 
			// radioStatusEC
			// 
			this.radioStatusEC.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioStatusEC.Location = new System.Drawing.Point(12, 62);
			this.radioStatusEC.Name = "radioStatusEC";
			this.radioStatusEC.Size = new System.Drawing.Size(132, 16);
			this.radioStatusEC.TabIndex = 2;
			this.radioStatusEC.Text = "Existing-Current Prov";
			this.radioStatusEC.Click += new System.EventHandler(this.radioExist_Click);
			// 
			// radioStatusC
			// 
			this.radioStatusC.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioStatusC.Location = new System.Drawing.Point(12, 40);
			this.radioStatusC.Name = "radioStatusC";
			this.radioStatusC.Size = new System.Drawing.Size(132, 15);
			this.radioStatusC.TabIndex = 1;
			this.radioStatusC.Text = "Completed";
			this.radioStatusC.Click += new System.EventHandler(this.radioC_Click);
			// 
			// radioStatusTP
			// 
			this.radioStatusTP.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioStatusTP.Location = new System.Drawing.Point(12, 18);
			this.radioStatusTP.Name = "radioStatusTP";
			this.radioStatusTP.Size = new System.Drawing.Size(127, 17);
			this.radioStatusTP.TabIndex = 0;
			this.radioStatusTP.Text = "Treatment Plan";
			this.radioStatusTP.Click += new System.EventHandler(this.radioTP_Click);
			// 
			// textDesc
			// 
			this.textDesc.BackColor = System.Drawing.SystemColors.Control;
			this.textDesc.Location = new System.Drawing.Point(168, 28);
			this.textDesc.Name = "textDesc";
			this.textDesc.Size = new System.Drawing.Size(216, 20);
			this.textDesc.TabIndex = 16;
			this.textDesc.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(498, 164);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(89, 16);
			this.label7.TabIndex = 19;
			this.label7.Text = "Notes";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(778, 549);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(76, 26);
			this.butOK.TabIndex = 9;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(869, 549);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(76, 26);
			this.butCancel.TabIndex = 10;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.Location = new System.Drawing.Point(23, 545);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75, 26);
			this.butDelete.TabIndex = 8;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// labelRange
			// 
			this.labelRange.Location = new System.Drawing.Point(2, 58);
			this.labelRange.Name = "labelRange";
			this.labelRange.Size = new System.Drawing.Size(82, 16);
			this.labelRange.TabIndex = 33;
			this.labelRange.Text = "Tooth Range";
			this.labelRange.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.labelRange.Visible = false;
			// 
			// textRange
			// 
			this.textRange.Location = new System.Drawing.Point(84, 56);
			this.textRange.Name = "textRange";
			this.textRange.TabIndex = 34;
			this.textRange.Text = "";
			this.textRange.Visible = false;
			// 
			// groupQuadrant
			// 
			this.groupQuadrant.Controls.Add(this.radioLR);
			this.groupQuadrant.Controls.Add(this.radioLL);
			this.groupQuadrant.Controls.Add(this.radioUL);
			this.groupQuadrant.Controls.Add(this.radioUR);
			this.groupQuadrant.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupQuadrant.Location = new System.Drawing.Point(82, 50);
			this.groupQuadrant.Name = "groupQuadrant";
			this.groupQuadrant.Size = new System.Drawing.Size(108, 56);
			this.groupQuadrant.TabIndex = 36;
			this.groupQuadrant.TabStop = false;
			this.groupQuadrant.Text = "Quadrant";
			this.groupQuadrant.Visible = false;
			// 
			// radioLR
			// 
			this.radioLR.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioLR.Location = new System.Drawing.Point(12, 36);
			this.radioLR.Name = "radioLR";
			this.radioLR.Size = new System.Drawing.Size(40, 16);
			this.radioLR.TabIndex = 3;
			this.radioLR.Text = "LR";
			this.radioLR.Click += new System.EventHandler(this.radioLR_Click);
			// 
			// radioLL
			// 
			this.radioLL.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioLL.Location = new System.Drawing.Point(64, 36);
			this.radioLL.Name = "radioLL";
			this.radioLL.Size = new System.Drawing.Size(40, 16);
			this.radioLL.TabIndex = 1;
			this.radioLL.Text = "LL";
			this.radioLL.Click += new System.EventHandler(this.radioLL_Click);
			// 
			// radioUL
			// 
			this.radioUL.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioUL.Location = new System.Drawing.Point(64, 16);
			this.radioUL.Name = "radioUL";
			this.radioUL.Size = new System.Drawing.Size(40, 16);
			this.radioUL.TabIndex = 0;
			this.radioUL.Text = "UL";
			this.radioUL.Click += new System.EventHandler(this.radioUL_Click);
			// 
			// radioUR
			// 
			this.radioUR.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioUR.Location = new System.Drawing.Point(12, 16);
			this.radioUR.Name = "radioUR";
			this.radioUR.Size = new System.Drawing.Size(40, 16);
			this.radioUR.TabIndex = 0;
			this.radioUR.Text = "UR";
			this.radioUR.Click += new System.EventHandler(this.radioUR_Click);
			// 
			// groupArch
			// 
			this.groupArch.Controls.Add(this.radioL);
			this.groupArch.Controls.Add(this.radioU);
			this.groupArch.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupArch.Location = new System.Drawing.Point(82, 50);
			this.groupArch.Name = "groupArch";
			this.groupArch.Size = new System.Drawing.Size(60, 56);
			this.groupArch.TabIndex = 3;
			this.groupArch.TabStop = false;
			this.groupArch.Text = "Arch";
			this.groupArch.Visible = false;
			// 
			// radioL
			// 
			this.radioL.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioL.Location = new System.Drawing.Point(12, 36);
			this.radioL.Name = "radioL";
			this.radioL.Size = new System.Drawing.Size(28, 16);
			this.radioL.TabIndex = 1;
			this.radioL.Text = "L";
			this.radioL.Click += new System.EventHandler(this.radioL_Click);
			// 
			// radioU
			// 
			this.radioU.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioU.Location = new System.Drawing.Point(12, 16);
			this.radioU.Name = "radioU";
			this.radioU.Size = new System.Drawing.Size(32, 16);
			this.radioU.TabIndex = 0;
			this.radioU.Text = "U";
			this.radioU.Click += new System.EventHandler(this.radioU_Click);
			// 
			// groupSextant
			// 
			this.groupSextant.Controls.Add(this.radioS6);
			this.groupSextant.Controls.Add(this.radioS5);
			this.groupSextant.Controls.Add(this.radioS4);
			this.groupSextant.Controls.Add(this.radioS2);
			this.groupSextant.Controls.Add(this.radioS3);
			this.groupSextant.Controls.Add(this.radioS1);
			this.groupSextant.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupSextant.Location = new System.Drawing.Point(82, 50);
			this.groupSextant.Name = "groupSextant";
			this.groupSextant.Size = new System.Drawing.Size(156, 56);
			this.groupSextant.TabIndex = 5;
			this.groupSextant.TabStop = false;
			this.groupSextant.Text = "Sextant";
			this.groupSextant.Visible = false;
			// 
			// radioS6
			// 
			this.radioS6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioS6.Location = new System.Drawing.Point(12, 36);
			this.radioS6.Name = "radioS6";
			this.radioS6.Size = new System.Drawing.Size(36, 16);
			this.radioS6.TabIndex = 5;
			this.radioS6.Text = "6";
			this.radioS6.Click += new System.EventHandler(this.radioS6_Click);
			// 
			// radioS5
			// 
			this.radioS5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioS5.Location = new System.Drawing.Point(60, 36);
			this.radioS5.Name = "radioS5";
			this.radioS5.Size = new System.Drawing.Size(36, 16);
			this.radioS5.TabIndex = 4;
			this.radioS5.Text = "5";
			this.radioS5.Click += new System.EventHandler(this.radioS5_Click);
			// 
			// radioS4
			// 
			this.radioS4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioS4.Location = new System.Drawing.Point(108, 36);
			this.radioS4.Name = "radioS4";
			this.radioS4.Size = new System.Drawing.Size(36, 16);
			this.radioS4.TabIndex = 1;
			this.radioS4.Text = "4";
			this.radioS4.Click += new System.EventHandler(this.radioS4_Click);
			// 
			// radioS2
			// 
			this.radioS2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioS2.Location = new System.Drawing.Point(60, 16);
			this.radioS2.Name = "radioS2";
			this.radioS2.Size = new System.Drawing.Size(36, 16);
			this.radioS2.TabIndex = 2;
			this.radioS2.Text = "2";
			this.radioS2.Click += new System.EventHandler(this.radioS2_Click);
			// 
			// radioS3
			// 
			this.radioS3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioS3.Location = new System.Drawing.Point(108, 16);
			this.radioS3.Name = "radioS3";
			this.radioS3.Size = new System.Drawing.Size(36, 16);
			this.radioS3.TabIndex = 0;
			this.radioS3.Text = "3";
			this.radioS3.Click += new System.EventHandler(this.radioS3_Click);
			// 
			// radioS1
			// 
			this.radioS1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioS1.Location = new System.Drawing.Point(12, 16);
			this.radioS1.Name = "radioS1";
			this.radioS1.Size = new System.Drawing.Size(36, 16);
			this.radioS1.TabIndex = 0;
			this.radioS1.Text = "1";
			this.radioS1.Click += new System.EventHandler(this.radioS1_Click);
			// 
			// listProvNum
			// 
			this.listProvNum.Location = new System.Drawing.Point(136, 179);
			this.listProvNum.Name = "listProvNum";
			this.listProvNum.Size = new System.Drawing.Size(92, 160);
			this.listProvNum.TabIndex = 3;
			// 
			// listDx
			// 
			this.listDx.Location = new System.Drawing.Point(12, 179);
			this.listDx.Name = "listDx";
			this.listDx.Size = new System.Drawing.Size(114, 160);
			this.listDx.TabIndex = 2;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(136, 163);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(100, 14);
			this.label9.TabIndex = 45;
			this.label9.Text = "Provider";
			// 
			// labelDx
			// 
			this.labelDx.Location = new System.Drawing.Point(12, 161);
			this.labelDx.Name = "labelDx";
			this.labelDx.Size = new System.Drawing.Size(100, 14);
			this.labelDx.TabIndex = 46;
			this.labelDx.Text = "Diagnosis";
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(84, 2);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(76, 20);
			this.textDate.TabIndex = 0;
			this.textDate.Text = "";
			// 
			// textProcFee
			// 
			this.textProcFee.Location = new System.Drawing.Point(84, 110);
			this.textProcFee.Name = "textProcFee";
			this.textProcFee.Size = new System.Drawing.Size(66, 20);
			this.textProcFee.TabIndex = 6;
			this.textProcFee.Text = "";
			this.textProcFee.Validating += new System.ComponentModel.CancelEventHandler(this.textProcFee_Validating);
			// 
			// panel1
			// 
			this.panel1.AllowDrop = true;
			this.panel1.Controls.Add(this.listBoxTeeth2);
			this.panel1.Controls.Add(this.listBoxTeeth);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.labelTooth);
			this.panel1.Controls.Add(this.labelSurfaces);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.textSurfaces);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.groupArch);
			this.panel1.Controls.Add(this.textDate);
			this.panel1.Controls.Add(this.textRange);
			this.panel1.Controls.Add(this.groupQuadrant);
			this.panel1.Controls.Add(this.groupSextant);
			this.panel1.Controls.Add(this.textProcFee);
			this.panel1.Controls.Add(this.textDesc);
			this.panel1.Controls.Add(this.textTooth);
			this.panel1.Controls.Add(this.labelRange);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.textProc);
			this.panel1.Location = new System.Drawing.Point(12, 8);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(493, 140);
			this.panel1.TabIndex = 1;
			// 
			// listBoxTeeth2
			// 
			this.listBoxTeeth2.ColumnWidth = 16;
			this.listBoxTeeth2.Items.AddRange(new object[] {
																											 "32",
																											 "31",
																											 "30",
																											 "29",
																											 "28",
																											 "27",
																											 "26",
																											 "25",
																											 "24",
																											 "23",
																											 "22",
																											 "21",
																											 "20",
																											 "19",
																											 "18",
																											 "17"});
			this.listBoxTeeth2.Location = new System.Drawing.Point(84, 66);
			this.listBoxTeeth2.MultiColumn = true;
			this.listBoxTeeth2.Name = "listBoxTeeth2";
			this.listBoxTeeth2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listBoxTeeth2.Size = new System.Drawing.Size(272, 17);
			this.listBoxTeeth2.TabIndex = 2;
			this.listBoxTeeth2.Visible = false;
			this.listBoxTeeth2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBoxTeeth2_MouseDown);
			// 
			// listBoxTeeth
			// 
			this.listBoxTeeth.AllowDrop = true;
			this.listBoxTeeth.ColumnWidth = 16;
			this.listBoxTeeth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.listBoxTeeth.Items.AddRange(new object[] {
																											"1",
																											"2",
																											"3",
																											"4",
																											"5",
																											"6",
																											"7",
																											"8",
																											"9",
																											"10",
																											"11",
																											"12",
																											"13",
																											"14",
																											"15",
																											"16"});
			this.listBoxTeeth.Location = new System.Drawing.Point(84, 52);
			this.listBoxTeeth.MultiColumn = true;
			this.listBoxTeeth.Name = "listBoxTeeth";
			this.listBoxTeeth.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.listBoxTeeth.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listBoxTeeth.Size = new System.Drawing.Size(272, 17);
			this.listBoxTeeth.TabIndex = 1;
			this.listBoxTeeth.Visible = false;
			this.listBoxTeeth.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBoxTeeth_MouseDown);
			// 
			// butChange
			// 
			this.butChange.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butChange.Autosize = true;
			this.butChange.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butChange.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butChange.Location = new System.Drawing.Point(401, 33);
			this.butChange.Name = "butChange";
			this.butChange.Size = new System.Drawing.Size(74, 25);
			this.butChange.TabIndex = 37;
			this.butChange.Text = "C&hange";
			this.butChange.Click += new System.EventHandler(this.butChange_Click);
			// 
			// labelClaim
			// 
			this.labelClaim.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelClaim.Location = new System.Drawing.Point(110, 530);
			this.labelClaim.Name = "labelClaim";
			this.labelClaim.Size = new System.Drawing.Size(480, 44);
			this.labelClaim.TabIndex = 50;
			this.labelClaim.Text = "This procedure is attached to a claim, so certain fields should not be edited.  Y" +
				"ou should reprint the claim if any significant changes are made.";
			this.labelClaim.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.labelClaim.Visible = false;
			// 
			// errorProvider2
			// 
			this.errorProvider2.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.errorProvider2.ContainerControl = this;
			// 
			// butEditAnyway
			// 
			this.butEditAnyway.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEditAnyway.Autosize = true;
			this.butEditAnyway.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEditAnyway.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEditAnyway.Location = new System.Drawing.Point(593, 549);
			this.butEditAnyway.Name = "butEditAnyway";
			this.butEditAnyway.Size = new System.Drawing.Size(104, 26);
			this.butEditAnyway.TabIndex = 51;
			this.butEditAnyway.Text = "&Edit Anyway";
			this.butEditAnyway.Visible = false;
			this.butEditAnyway.Click += new System.EventHandler(this.butEditAnyway_Click);
			// 
			// comboPlaceService
			// 
			this.comboPlaceService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPlaceService.Location = new System.Drawing.Point(318, 179);
			this.comboPlaceService.MaxDropDownItems = 30;
			this.comboPlaceService.Name = "comboPlaceService";
			this.comboPlaceService.Size = new System.Drawing.Size(177, 21);
			this.comboPlaceService.TabIndex = 52;
			// 
			// labelPlaceService
			// 
			this.labelPlaceService.Location = new System.Drawing.Point(316, 164);
			this.labelPlaceService.Name = "labelPlaceService";
			this.labelPlaceService.Size = new System.Drawing.Size(133, 16);
			this.labelPlaceService.TabIndex = 53;
			this.labelPlaceService.Text = "Place of Service";
			// 
			// butSetComplete
			// 
			this.butSetComplete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSetComplete.Autosize = true;
			this.butSetComplete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSetComplete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSetComplete.Location = new System.Drawing.Point(809, 38);
			this.butSetComplete.Name = "butSetComplete";
			this.butSetComplete.Size = new System.Drawing.Size(98, 25);
			this.butSetComplete.TabIndex = 54;
			this.butSetComplete.Text = "Set Complete";
			this.butSetComplete.Click += new System.EventHandler(this.butSetComplete_Click);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(237, 162);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(72, 16);
			this.label10.TabIndex = 56;
			this.label10.Text = "Priority";
			this.label10.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listPriority
			// 
			this.listPriority.Location = new System.Drawing.Point(238, 179);
			this.listPriority.Name = "listPriority";
			this.listPriority.Size = new System.Drawing.Size(76, 160);
			this.listPriority.TabIndex = 57;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(809, 66);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(110, 33);
			this.label13.TabIndex = 58;
			this.label13.Text = "Also changes date and adds note.";
			// 
			// tbIns
			// 
			this.tbIns.BackColor = System.Drawing.SystemColors.Window;
			this.tbIns.Location = new System.Drawing.Point(1, 411);
			this.tbIns.Name = "tbIns";
			this.tbIns.ScrollValue = 197;
			this.tbIns.SelectedIndices = new int[0];
			this.tbIns.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbIns.Size = new System.Drawing.Size(959, 118);
			this.tbIns.TabIndex = 59;
			this.tbIns.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(this.tbIns_CellDoubleClicked);
			// 
			// butAddEstimate
			// 
			this.butAddEstimate.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddEstimate.Autosize = true;
			this.butAddEstimate.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddEstimate.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddEstimate.Location = new System.Drawing.Point(13, 379);
			this.butAddEstimate.Name = "butAddEstimate";
			this.butAddEstimate.Size = new System.Drawing.Size(104, 26);
			this.butAddEstimate.TabIndex = 60;
			this.butAddEstimate.Text = "Add Estimate";
			this.butAddEstimate.Click += new System.EventHandler(this.butAddEstimate_Click);
			// 
			// checkNoBillIns
			// 
			this.checkNoBillIns.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNoBillIns.Location = new System.Drawing.Point(141, 387);
			this.checkNoBillIns.Name = "checkNoBillIns";
			this.checkNoBillIns.Size = new System.Drawing.Size(152, 18);
			this.checkNoBillIns.TabIndex = 61;
			this.checkNoBillIns.Text = "Do Not Bill to Ins";
			this.checkNoBillIns.ThreeState = true;
			this.checkNoBillIns.Click += new System.EventHandler(this.checkNoBillIns_Click);
			// 
			// textNotes
			// 
			this.textNotes.AcceptsReturn = true;
			this.textNotes.Location = new System.Drawing.Point(500, 179);
			this.textNotes.Multiline = true;
			this.textNotes.Name = "textNotes";
			this.textNotes.QuickPasteType = OpenDental.QuickPasteType.Procedure;
			this.textNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNotes.Size = new System.Drawing.Size(454, 160);
			this.textNotes.TabIndex = 0;
			this.textNotes.Text = "";
			// 
			// checkHideGraphical
			// 
			this.checkHideGraphical.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkHideGraphical.Location = new System.Drawing.Point(325, 327);
			this.checkHideGraphical.Name = "checkHideGraphical";
			this.checkHideGraphical.Size = new System.Drawing.Size(139, 14);
			this.checkHideGraphical.TabIndex = 63;
			this.checkHideGraphical.Text = "Hide Graphics";
			// 
			// groupProsth
			// 
			this.groupProsth.Controls.Add(this.listProsth);
			this.groupProsth.Controls.Add(this.textDateOriginalProsth);
			this.groupProsth.Controls.Add(this.label4);
			this.groupProsth.Controls.Add(this.label3);
			this.groupProsth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupProsth.Location = new System.Drawing.Point(319, 204);
			this.groupProsth.Name = "groupProsth";
			this.groupProsth.Size = new System.Drawing.Size(176, 110);
			this.groupProsth.TabIndex = 64;
			this.groupProsth.TabStop = false;
			this.groupProsth.Text = "Prosthesis Replacement";
			// 
			// listProsth
			// 
			this.listProsth.Location = new System.Drawing.Point(5, 33);
			this.listProsth.Name = "listProsth";
			this.listProsth.Size = new System.Drawing.Size(163, 43);
			this.listProsth.TabIndex = 6;
			// 
			// textDateOriginalProsth
			// 
			this.textDateOriginalProsth.Location = new System.Drawing.Point(96, 83);
			this.textDateOriginalProsth.Name = "textDateOriginalProsth";
			this.textDateOriginalProsth.Size = new System.Drawing.Size(73, 20);
			this.textDateOriginalProsth.TabIndex = 5;
			this.textDateOriginalProsth.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(7, 86);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(84, 16);
			this.label4.TabIndex = 4;
			this.label4.Text = "Original Date";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(2, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(171, 15);
			this.label3.TabIndex = 0;
			this.label3.Text = "Crown, Bridge, Denture, or RPD";
			// 
			// textClaimNote
			// 
			this.textClaimNote.AcceptsReturn = true;
			this.textClaimNote.Location = new System.Drawing.Point(500, 359);
			this.textClaimNote.MaxLength = 80;
			this.textClaimNote.Multiline = true;
			this.textClaimNote.Name = "textClaimNote";
			this.textClaimNote.QuickPasteType = OpenDental.QuickPasteType.Procedure;
			this.textClaimNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textClaimNote.Size = new System.Drawing.Size(265, 44);
			this.textClaimNote.TabIndex = 66;
			this.textClaimNote.Text = "";
			// 
			// labelClaimNote
			// 
			this.labelClaimNote.Location = new System.Drawing.Point(500, 343);
			this.labelClaimNote.Name = "labelClaimNote";
			this.labelClaimNote.Size = new System.Drawing.Size(272, 16);
			this.labelClaimNote.TabIndex = 65;
			this.labelClaimNote.Text = "Claim Note (keep it very short)";
			// 
			// labelLocked
			// 
			this.labelLocked.Location = new System.Drawing.Point(775, 161);
			this.labelLocked.Name = "labelLocked";
			this.labelLocked.Size = new System.Drawing.Size(102, 12);
			this.labelLocked.TabIndex = 67;
			this.labelLocked.Text = "Date Locked";
			this.labelLocked.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateLocked
			// 
			this.textDateLocked.Location = new System.Drawing.Point(881, 157);
			this.textDateLocked.Name = "textDateLocked";
			this.textDateLocked.ReadOnly = true;
			this.textDateLocked.Size = new System.Drawing.Size(74, 20);
			this.textDateLocked.TabIndex = 68;
			this.textDateLocked.Text = "";
			// 
			// butLock
			// 
			this.butLock.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butLock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butLock.Autosize = true;
			this.butLock.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLock.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLock.Location = new System.Drawing.Point(879, 155);
			this.butLock.Name = "butLock";
			this.butLock.Size = new System.Drawing.Size(76, 24);
			this.butLock.TabIndex = 69;
			this.butLock.Text = "Lock Note";
			this.butLock.Click += new System.EventHandler(this.butLock_Click);
			// 
			// FormProcEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(962, 596);
			this.Controls.Add(this.labelLocked);
			this.Controls.Add(this.textClaimNote);
			this.Controls.Add(this.labelClaimNote);
			this.Controls.Add(this.groupProsth);
			this.Controls.Add(this.checkHideGraphical);
			this.Controls.Add(this.textNotes);
			this.Controls.Add(this.comboPlaceService);
			this.Controls.Add(this.checkNoBillIns);
			this.Controls.Add(this.butAddEstimate);
			this.Controls.Add(this.tbIns);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.listPriority);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.butSetComplete);
			this.Controls.Add(this.labelPlaceService);
			this.Controls.Add(this.butEditAnyway);
			this.Controls.Add(this.butChange);
			this.Controls.Add(this.labelClaim);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.labelDx);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.listDx);
			this.Controls.Add(this.listProvNum);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.groupStatus);
			this.Controls.Add(this.butLock);
			this.Controls.Add(this.textDateLocked);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProcEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Procedure Info";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormProcEdit_Closing);
			this.Load += new System.EventHandler(this.FormProcInfo_Load);
			this.groupStatus.ResumeLayout(false);
			this.groupQuadrant.ResumeLayout(false);
			this.groupArch.ResumeLayout(false);
			this.groupSextant.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.groupProsth.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormProcInfo_Load(object sender, System.EventArgs e){
			//as long as Procedures.CurOld is set every single time Procedures.Cur is before opening this form,
			//we shouldn't do that here.
			//ProcCur=Procedures.Cur;
			if(Prefs.GetBool("EasyHidePublicHealth")){
				labelPlaceService.Visible=false;
				comboPlaceService.Visible=false;
			}
			if(Prefs.GetBool("UseInternationalToothNumbers")){
				listBoxTeeth.Items.Clear();
				listBoxTeeth.Items.AddRange(new string[] {"18","17","16","15","14","13","12","11","21","22","23","24","25","26","27","28"});
				listBoxTeeth2.Items.Clear();
				listBoxTeeth2.Items.AddRange(new string[] {"48","47","46","45","44","43","42","41","31","32","33","34","35","36","37","38"});
			}
			Claims.Refresh(PatCur.PatNum);
			ProcedureCode2=ProcedureCodes.GetProcCode(ProcCur.ADACode);
			if(IsNew){
				//SetControls();
				//return;
			}
			else{
				if(ProcCur.ProcStatus==ProcStat.C){
					if(!UserPermissions.CheckUserPassword("Procedure Completed Edit",ProcCur.ProcDate)){
						//MessageBox.Show(Lan.g(this,"You only have permission to view the Procedure. No changes will be saved"));
						butOK.Enabled=false;
						butDelete.Enabled=false;
						butChange.Enabled=false;
						butEditAnyway.Enabled=false;
					}					
				}
			}
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			//if(ClaimProcs.ProcIsAttachedToClaim(ClaimProcList,ProcCur.ProcNum)){//attached to claim
			//	if(ClaimProcs.ProcIsSent(ClaimProcList,ProcCur.ProcNum)){//if also sent
			if(ProcCur.IsAttachedToClaim(ClaimProcList)){
				panel1.Enabled=false;
				groupStatus.Enabled=false;
				checkNoBillIns.Enabled=false;
				butChange.Enabled=false;
				butDelete.Enabled=false;
				butEditAnyway.Visible=true;
				labelClaim.Visible=true;
				//if(ClaimProcs.ProcIsPaid(Procedures.Cur.ProcNum)){//if also paid on
				//	butDelete.Enabled=false;
				//	labelClaim.Text+="  "
				//		+Lan.g(this,"You cannot delete a procedure that is attached to a payment.");
				//}
			}
			if(((Pref)Prefs.HList["EasyHideClinical"]).ValueString=="1"){
				labelDx.Visible=false;
				listDx.Visible=false;
				radioStatusEC.Visible=false;
				radioStatusEO.Visible=false;
				radioStatusR.Visible=false;
			}
			FillControls();
			SetControls();
			FillIns(false);
		}		

		///<summary>Only run on startup. Fills the basic controls, except not the ones in the upper left panel which are handled in SetControls.</summary>
		private void FillControls(){
			switch (ProcCur.ProcStatus){
				case ProcStat.TP: 
					radioStatusTP.Checked=true; 
					break;
				case ProcStat.C: 
					radioStatusC.Checked=true;
					break;
				case ProcStat.EC: 
					radioStatusEC.Checked=true; 
					break;
				case ProcStat.EO: 
					radioStatusEO.Checked=true; 
					break;
				case ProcStat.R: 
					radioStatusR.Checked=true; 
					break;
			}
			listDx.Items.Clear();
			for(int i=0;i<Defs.Short[(int)DefCat.Diagnosis].Length;i++){
				this.listDx.Items.Add(Defs.Short[(int)DefCat.Diagnosis][i].ItemName);
				if(Defs.Short[(int)DefCat.Diagnosis][i].DefNum==ProcCur.Dx)
					listDx.SelectedIndex=i;
			}
			listProvNum.Items.Clear();
			for(int i=0;i<Providers.List.Length;i++){
				this.listProvNum.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==ProcCur.ProvNum)
					listProvNum.SelectedIndex=i;
			}
			listPriority.Items.Clear();
			listPriority.Items.Add(Lan.g(this,"no priority"));
			listPriority.SelectedIndex=0;
			for(int i=0;i<Defs.Short[(int)DefCat.TxPriorities].Length;i++){
				listPriority.Items.Add(Defs.Short[(int)DefCat.TxPriorities][i].ItemName);
				if(Defs.Short[(int)DefCat.TxPriorities][i].DefNum==ProcCur.Priority)
					listPriority.SelectedIndex=i+1;
			}
			textNotes.Text=ProcCur.ProcNote;
			textNotes.Select(textNotes.Text.Length,0);
			comboPlaceService.Items.Clear();
			comboPlaceService.Items.AddRange(Enum.GetNames(typeof(PlaceOfService)));
			comboPlaceService.SelectedIndex=(int)ProcCur.PlaceService;
			checkHideGraphical.Checked=ProcCur.HideGraphical;
			if(ProcedureCode2.IsProsth){
				listProsth.Items.Add(Lan.g(this,"No"));
				listProsth.Items.Add(Lan.g(this,"Initial"));
				listProsth.Items.Add(Lan.g(this,"Replacement"));
				switch(ProcCur.Prosthesis){
					case "":
						listProsth.SelectedIndex=0;
						break;
					case "I":
						listProsth.SelectedIndex=1;
						break;
					case "R":
						listProsth.SelectedIndex=2;
						break;
				}
				if(ProcCur.DateOriginalProsth.Year>1880){
					textDateOriginalProsth.Text=ProcCur.DateOriginalProsth.ToShortDateString();
				}
			}
			else{
				groupProsth.Visible=false;
			}
			textClaimNote.Text=ProcCur.ClaimNote;
			if(ProcCur.DateLocked.Year>1880){//if locked
				butLock.Visible=false;
				textNotes.ReadOnly=true;
				butDelete.Enabled=false;
				groupStatus.Enabled=false;
				butSetComplete.Enabled=false;
				textDateLocked.Text=ProcCur.DateLocked.ToShortDateString();
			}
			else{
				labelLocked.Visible=false;
				textDateLocked.Visible=false;
			}
		}

		///<summary>Called on open and after changing code.  Sets the visibilities and the data of all the fields in the upper left panel.</summary>
		private void SetControls(){
			textDate.Text=ProcCur.ProcDate.ToString("d");
			textProc.Text=ProcCur.ADACode;
			textDesc.Text=ProcedureCode2.Descript;
			switch (ProcedureCode2.TreatArea){
				case TreatmentArea.Surf:
					this.textTooth.Visible=true;
					this.labelTooth.Visible=true;
					this.textSurfaces.Visible=true;
					this.labelSurfaces.Visible=true;
					if(Tooth.IsValidDB(ProcCur.ToothNum)){
						errorProvider2.SetError(textTooth,"");
						textTooth.Text=Tooth.ToInternat(ProcCur.ToothNum);
						textSurfaces.Text=Tooth.SurfTidy(ProcCur.Surf,ProcCur.ToothNum);
					}
					else{
						errorProvider2.SetError(textTooth,Lan.g(this,"Invalid tooth number."));
						textTooth.Text=ProcCur.ToothNum;
						textSurfaces.Text=Tooth.SurfTidy(ProcCur.Surf,"");//only valid toothnums allowed
					}
					if(textSurfaces.Text=="")
						errorProvider2.SetError(textSurfaces,"No surfaces selected.");
					else
						errorProvider2.SetError(textSurfaces,"");
					break;
				case TreatmentArea.Tooth:
					this.textTooth.Visible=true;
					this.labelTooth.Visible=true;
					if(Tooth.IsValidDB(ProcCur.ToothNum)){
						errorProvider2.SetError(textTooth,"");
						textTooth.Text=Tooth.ToInternat(ProcCur.ToothNum);
					}
					else{
						errorProvider2.SetError(textTooth,Lan.g(this,"Invalid tooth number."));
						textTooth.Text=ProcCur.ToothNum;
					}
					break;
				case TreatmentArea.Mouth:
						break;
				case TreatmentArea.Quad:
					this.groupQuadrant.Visible=true;
					switch (ProcCur.Surf){
						case "UR": this.radioUR.Checked=true; break;
						case "UL": this.radioUL.Checked=true; break;
						case "LR": this.radioLR.Checked=true; break;
						case "LL": this.radioLL.Checked=true; break;
						//default : 
					}
					break;
				case TreatmentArea.Sextant:
					this.groupSextant.Visible=true;
					switch (ProcCur.Surf){
						case "1": this.radioS1.Checked=true; break;
						case "2": this.radioS2.Checked=true; break;
						case "3": this.radioS3.Checked=true; break;
						case "4": this.radioS4.Checked=true; break;
						case "5": this.radioS5.Checked=true; break;
						case "6": this.radioS6.Checked=true; break;
						//default:
					}
					break;
				case TreatmentArea.Arch:
					this.groupArch.Visible=true;
					switch (ProcCur.Surf){
						case "U": this.radioU.Checked=true; break;
						case "L": this.radioL.Checked=true; break;
					}
					break;
				case TreatmentArea.ToothRange:
					this.labelRange.Visible=true;
					this.listBoxTeeth.Visible=true;
					this.listBoxTeeth2.Visible=true;
					if(ProcCur.ToothRange==null){
						break;
					}
   			  string[] sArray=ProcCur.ToothRange.Split(',');
          for(int i=0;i<sArray.Length;i++)  {
            for(int j=0;j<listBoxTeeth.Items.Count;j++)  {
              if(Tooth.ToInternat(sArray[i])==listBoxTeeth.Items[j].ToString())
				 		    listBoxTeeth.SelectedItem=Tooth.ToInternat(sArray[i]);
					  }
  			    for(int j=0;j<listBoxTeeth2.Items.Count;j++)  {
              if(Tooth.ToInternat(sArray[i])==listBoxTeeth2.Items[j].ToString())
				 		    listBoxTeeth2.SelectedItem=Tooth.ToInternat(sArray[i]);
            }
					} 
					break;
			}//end switch
			textProcFee.Text=ProcCur.ProcFee.ToString("n");
		}//end SetControls

		private void FillIns(){
			FillIns(true);
		}

		private void FillIns(bool refreshClaimProcsFirst){
			if(refreshClaimProcsFirst){
				ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			}
			ClaimProcsForProc=ClaimProcs.GetForProc(ClaimProcList,ProcCur.ProcNum);
			tbIns.ResetRows(ClaimProcsForProc.Length);
			checkNoBillIns.CheckState=CheckState.Unchecked;
			bool allNoBillIns=true;
			for(int i=0;i<ClaimProcsForProc.Length;i++){
				if(ClaimProcsForProc[i].NoBillIns){
					checkNoBillIns.CheckState=CheckState.Indeterminate;
				}
				else{
					allNoBillIns=false;
				}
				tbIns.Cell[0,i]=InsPlans.GetDescript(ClaimProcsForProc[i].PlanNum,FamCur,PlanList);
				if(ClaimProcsForProc[i].PlanNum==PatCur.PriPlanNum){
					tbIns.Cell[1,i]="Pri";
				}
				else if(ClaimProcsForProc[i].PlanNum==PatCur.SecPlanNum){
					tbIns.Cell[1,i]="Sec";
				}
				switch(ClaimProcsForProc[i].Status){
					case ClaimProcStatus.Received:
						tbIns.Cell[2,i]="Recd";
						break;
					case ClaimProcStatus.NotReceived:
						tbIns.Cell[2,i]="NotRec";
						break;
					//adjustment would never show here
					case ClaimProcStatus.Preauth:
						tbIns.Cell[2,i]="PreA";
						break;
					case ClaimProcStatus.Supplemental:
						tbIns.Cell[2,i]="Supp";
						break;
					case ClaimProcStatus.CapClaim:
						tbIns.Cell[2,i]="CapClaim";
						break;
					case ClaimProcStatus.Estimate:
						tbIns.Cell[2,i]="Est";
						break;
					case ClaimProcStatus.CapEstimate:
						tbIns.Cell[2,i]="CapEst";
						break;
					case ClaimProcStatus.CapComplete:
						tbIns.Cell[2,i]="CapComp";
						break;
				}
				if(ClaimProcsForProc[i].NoBillIns){
					tbIns.Cell[3,i]="X";
					if(ClaimProcsForProc[i].Status!=ClaimProcStatus.CapComplete
						&& ClaimProcsForProc[i].Status!=ClaimProcStatus.CapEstimate)
					{					
						tbIns.Cell[4,i]="";
						tbIns.Cell[5,i]="";
						tbIns.Cell[6,i]="";
						tbIns.Cell[7,i]="";
						tbIns.Cell[8,i]="";
						tbIns.Cell[9,i]="";
						tbIns.Cell[10,i]="";
						tbIns.Cell[11,i]="";
						tbIns.Cell[12,i]="";
						continue;
					}
				}
				int percent=0;
				if(ClaimProcsForProc[i].PercentOverride==-1){
					if(ClaimProcsForProc[i].Percentage==-1){
						//blank?
					}
					else{
						percent=ClaimProcsForProc[i].Percentage;
					}
				}
				else{
					percent=ClaimProcsForProc[i].PercentOverride;
				}
				tbIns.Cell[4,i]=percent.ToString();
				if(ClaimProcsForProc[i].CopayOverride!=-1){
					tbIns.Cell[5,i]=ClaimProcsForProc[i].CopayOverride.ToString("n");
				}
				else if(ClaimProcsForProc[i].CopayAmt!=-1){
					tbIns.Cell[5,i]=ClaimProcsForProc[i].CopayAmt.ToString("n");
				}
				tbIns.Cell[6,i]=ClaimProcsForProc[i].BaseEst.ToString("n");
				if(ClaimProcsForProc[i].OverrideInsEst!=-1){
					tbIns.Cell[7,i]=ClaimProcsForProc[i].OverrideInsEst.ToString("n");
					tbIns.FontBold[7,i]=true;
				}
				else{
					tbIns.FontBold[6,i]=true;
				}
				tbIns.Cell[8,i]=ClaimProcsForProc[i].DedApplied.ToString("n");
				tbIns.Cell[9,i]=ClaimProcsForProc[i].InsPayEst.ToString("n");
				tbIns.Cell[10,i]=ClaimProcsForProc[i].InsPayAmt.ToString("n");
				tbIns.Cell[11,i]=ClaimProcsForProc[i].WriteOff.ToString("n");
				tbIns.Cell[12,i]=ClaimProcsForProc[i].Remarks;
				if(ClaimProcsForProc[i].Status==ClaimProcStatus.CapEstimate
					|| ClaimProcsForProc[i].Status==ClaimProcStatus.CapComplete){
					tbIns.Cell[4,i]="";//percent
					tbIns.Cell[6,i]="";//baseEst
					tbIns.Cell[8,i]="";//deduct
					tbIns.Cell[9,i]="";//insest
					tbIns.Cell[10,i]="";//inspay
				}
				if(ClaimProcsForProc[i].Status==ClaimProcStatus.Estimate){
					tbIns.Cell[8,i]="";//deduct
					tbIns.Cell[9,i]="";//insest
					tbIns.Cell[10,i]="";//inspay
					tbIns.Cell[11,i]="";//writeoff
				}
			}
			if(ClaimProcsForProc.Length==0)
				checkNoBillIns.CheckState=CheckState.Unchecked;
			else if(allNoBillIns){
				checkNoBillIns.CheckState=CheckState.Checked;
			}
			tbIns.SetGridColor(Color.LightGray);
			tbIns.LayoutTables();
		}

		private void tbIns_CellDoubleClicked(object sender, OpenDental.CellEventArgs e) {
			FormClaimProc FormC=new FormClaimProc(ClaimProcsForProc[e.Row],ProcCur,FamCur,PlanList);
			FormC.ShowDialog();
			FillIns();
		}

		private void butAddEstimate_Click(object sender, System.EventArgs e) {
			FormInsPlanSelect FormIS=new FormInsPlanSelect(PatCur.PatNum);
			FormIS.ShowDialog();
			if(FormIS.DialogResult==DialogResult.Cancel){
				return;
			}
			ClaimProc cp=new ClaimProc();
			cp.CreateEst(ProcCur,FormIS.SelectedPlan);
			if(FormIS.SelectedPlan.PlanNum==PatCur.PriPlanNum){
				cp.ComputeBaseEst(ProcCur,PriSecTot.Pri,PatCur,PlanList);
			}
			else if(FormIS.SelectedPlan.PlanNum==PatCur.SecPlanNum){
				cp.ComputeBaseEst(ProcCur,PriSecTot.Sec,PatCur,PlanList);
			}
			FormClaimProc FormC=new FormClaimProc(cp,ProcCur,FamCur,PlanList);
			FormC.ShowDialog();
			if(FormC.DialogResult==DialogResult.Cancel){
				cp.Delete();
			}
			FillIns();
		}

		private void textProcFee_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			if(textProcFee.errorProvider1.GetError(textProcFee)!=""){
				return;
			}
			double procFee;
			if(textProcFee.Text==""){
				procFee=0;
			}
			else{
				procFee=PIn.PDouble(textProcFee.Text);
			}
			if(ProcCur.ProcFee==procFee){
				return;
			}
			ProcCur.ProcFee=procFee;
			ProcCur.ComputeEstimates(PatCur.PatNum,PatCur.PriPlanNum
					,PatCur.SecPlanNum,ClaimProcList,false,PatCur,PlanList);
			FillIns();
		}

		private void textTooth_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			if(!Tooth.IsValidEntry(textTooth.Text))
				errorProvider2.SetError(textTooth,Lan.g(this,"Invalid tooth number."));
			else
				errorProvider2.SetError(textTooth,"");
		}

		private void textSurfaces_TextChanged(object sender, System.EventArgs e) {
			int cursorPos = textSurfaces.SelectionStart;
			textSurfaces.Text=textSurfaces.Text.ToUpper();
			textSurfaces.SelectionStart=cursorPos;
		}

		private void textSurfaces_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			if(Tooth.IsValidEntry(textTooth.Text)){
				textSurfaces.Text=Tooth.SurfTidy(textSurfaces.Text,Tooth.FromInternat(textTooth.Text));
			}
			else{
				textSurfaces.Text=Tooth.SurfTidy(textSurfaces.Text,"");
			}
			if(textSurfaces.Text=="")
				errorProvider2.SetError(textSurfaces,"No surfaces selected.");
			else
				errorProvider2.SetError(textSurfaces,"");
		}

		private void listBoxTeeth2_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
		  listBoxTeeth.SelectedIndex=-1;
		}

		private void listBoxTeeth_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
		  listBoxTeeth2.SelectedIndex=-1;
		}

		private void butChange_Click(object sender, System.EventArgs e) {
			FormProcedures FormP=new FormProcedures();
      FormP.Mode=FormProcMode.Select;
      FormP.ShowDialog();
      if(FormP.DialogResult!=DialogResult.OK){
				return;
			}
      ProcCur.ADACode=FormP.SelectedADA;
      ProcedureCode2=ProcedureCodes.GetProcCode(FormP.SelectedADA);
      textDesc.Text=ProcedureCode2.Descript;
      ProcCur.ProcFee=Fees.GetAmount(FormP.SelectedADA,Fees.GetFeeSched(PatCur,PlanList));
			switch(ProcedureCode2.TreatArea){ 
				case TreatmentArea.Quad:
					ProcCur.Surf="UR";
					radioUR.Checked=true;
					break;
				case TreatmentArea.Sextant:
					ProcCur.Surf="1";
					radioS1.Checked=true;
					break;
				case TreatmentArea.Arch:
					ProcCur.Surf="U";
					radioU.Checked=true;
					break;
			}
			ProcCur.ComputeEstimates(PatCur.PatNum,PatCur.PriPlanNum
					,PatCur.SecPlanNum,ClaimProcList,false,PatCur,PlanList);
			FillIns();
      SetControls();
		}

		private void butEditAnyway_Click(object sender, System.EventArgs e) {
			panel1.Enabled=true;
			groupStatus.Enabled=true;
			checkNoBillIns.Enabled=true;
			butDelete.Enabled=true;
			butChange.Enabled=true;
			//checkIsCovIns.Enabled=true;
		}

		private void radioTP_Click(object sender, System.EventArgs e) {
			ProcCur.ProcStatus=ProcStat.TP;
			//fee starts out 0 if EO, EC, etc.  This updates fee if changing to TP so it won't stay 0.
			if(ProcCur.ProcFee==0){
				ProcCur.ProcFee=Fees.GetAmount(ProcCur.ADACode,Fees.GetFeeSched(PatCur,PlanList));
				textProcFee.Text=ProcCur.ProcFee.ToString("f");
			}
		}

		private void radioC_Click(object sender, System.EventArgs e) {
			Procedures.SetDateFirstVisit(DateTime.Today,2,PatCur);
			ProcCur.ProcStatus=ProcStat.C;
		}

		private void radioExist_Click(object sender, System.EventArgs e) {
			ProcCur.ProcStatus=ProcStat.EC;
		}

		private void radioEO_Click(object sender, System.EventArgs e) {
			ProcCur.ProcStatus=ProcStat.EO;
		}

		private void radioStatusR_Click(object sender, System.EventArgs e) {
			ProcCur.ProcStatus=ProcStat.R;
		}

		private void butSetComplete_Click(object sender, System.EventArgs e) {
			Procedures.SetDateFirstVisit(DateTime.Today,2,PatCur);
			if(ProcCur.AptNum!=0){//if attached to an appointment
				Appointments.RefreshCur(ProcCur.AptNum);
				textDate.Text=Appointments.Cur.AptDateTime.ToShortDateString();
			}
			else{
				textDate.Text=DateTime.Today.ToShortDateString();
			}
			if(ProcedureCode2.RemoveTooth){//if an extraction, then mark previous procs hidden
				ProcCur.SetHideGraphical();
			}
			textNotes.Text+=ProcedureCode2.DefaultNote;
			radioStatusC.Checked=true;
			ProcCur.ProcStatus=ProcStat.C;
			comboPlaceService.SelectedIndex
				=PIn.PInt(((Pref)Prefs.HList["DefaultProcedurePlaceService"]).ValueString);
			if(EntriesAreValid()){
				SaveAndClose();
			}
		}

		private void radioUR_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="UR";
		}

		private void radioUL_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="UL";
		}

		private void radioLR_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="LR";
		}

		private void radioLL_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="LL";
		}

		private void radioU_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="U";
		}

		private void radioL_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="L";
		}

		private void radioS1_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="1";
		}

		private void radioS2_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="2";
		}

		private void radioS3_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="3";
		}

		private void radioS4_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="4";
		}

		private void radioS5_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="5";
		}

		private void radioS6_Click(object sender, System.EventArgs e) {
			ProcCur.Surf="6";
		}

		private void butLock_Click(object sender, System.EventArgs e) {
			if(ProcCur.ProcStatus!=ProcStat.C){
				MsgBox.Show(this,"Status must be complete before note may be locked");
				return;
			}
			butLock.Visible=false;
			labelLocked.Visible=true;
			textDateLocked.Visible=true;
			textDateLocked.Text=DateTime.Today.ToShortDateString();
			ProcCur.DateLocked=DateTime.Today;
			//textNotes.ReadOnly=true;//can still edit until the next time you open
		}

		private void checkNoBillIns_Click(object sender, System.EventArgs e) {
			if(checkNoBillIns.CheckState==CheckState.Indeterminate){
				//not allowed to set to indeterminate, so move on
				checkNoBillIns.CheckState=CheckState.Unchecked;
			}
			for(int i=0;i<ClaimProcsForProc.Length;i++){
				//ignore CapClaim,NotReceived,PreAuth,Recieved,Supplemental
				if(ClaimProcsForProc[i].Status==ClaimProcStatus.Estimate
					|| ClaimProcsForProc[i].Status==ClaimProcStatus.CapComplete
					|| ClaimProcsForProc[i].Status==ClaimProcStatus.CapEstimate)
				{
					if(checkNoBillIns.CheckState==CheckState.Checked){
						ClaimProcsForProc[i].NoBillIns=true;
					}
					else{//unchecked
						ClaimProcsForProc[i].NoBillIns=false;
					}
					ClaimProcsForProc[i].Update();
				}
			}
			//next line is needed to recalc BaseEst, etc, for claimprocs that are no longer NoBillIns
			//also, if they are NoBillIns, then it clears out the other values.
			ProcCur.ComputeEstimates(PatCur.PatNum,PatCur.PriPlanNum
					,PatCur.SecPlanNum,ClaimProcList,false,PatCur,PlanList);
			FillIns();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"Delete Procedure?"),"",MessageBoxButtons.OKCancel)
				!=DialogResult.OK)
			{
				return;
			}
			//if(IsNew){
			//	DialogResult=DialogResult.Cancel;
			//	return;
			//}
			//if(ClaimProcs.ProcIsAttachedToClaim(ClaimProcsForProc,ProcCur.ProcNum)){//attached to claim
			//	if(ClaimProcs.ProcIsPaid(ClaimProcsForProc,ProcCur.ProcNum)){//if also paid on
			if(ProcCur.InsHasPaid(ClaimProcsForProc)){
				MessageBox.Show
					(Lan.g(this,"Not allowed to delete a procedure that is attached to a payment."));
				return;
			}
			ProcCur.Delete();//also deletes the claimProcs
			Recalls.Synch(ProcCur.PatNum);
			DialogResult=DialogResult.OK;	
		}

		/*private void DeleteClaimProcs(){
			for(int i=0;i<ClaimProcList.Length;i++){
				if(ClaimProcList[i].ProcNum==ProcCur.ProcNum){
					ClaimProcList[i].Delete();
				}
			}
		}*/

		private void butOK_Click(object sender, System.EventArgs e){
			if(EntriesAreValid()){
				SaveAndClose();
			}
		}

		private bool EntriesAreValid(){
			if(  textDate.errorProvider1.GetError(textDate)!=""
				|| textProcFee.errorProvider1.GetError(textProcFee)!=""
				|| textDateOriginalProsth.errorProvider1.GetError(textDateOriginalProsth)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return false;
			}
			if(errorProvider2.GetError(textSurfaces)!=""
				|| errorProvider2.GetError(textTooth)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return false;
			}
			if(ProcedureCode2.IsProsth){
				if(listProsth.SelectedIndex==0
					|| (listProsth.SelectedIndex==2 && textDateOriginalProsth.Text==""))
				{
					if(MessageBox.Show(Lan.g(this,"Prosthesis date not entered. Continue anyway?")
						,"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
					{
						return false;
					}
				}
			}
			return true;
		}

		///<summary>MUST call EntriesAreValid first.  Used from OK_Click and from butSetComplete_Click</summary>
		private void SaveAndClose(){
			if(textProcFee.Text==""){
				textProcFee.Text="0";
			}
			ProcCur.PatNum=PatCur.PatNum;
			ProcCur.ADACode=this.textProc.Text;
			ProcCur.ProcDate=PIn.PDate(this.textDate.Text);
			ProcCur.ProcFee=PIn.PDouble(textProcFee.Text);
			//MessageBox.Show(ProcCur.ProcFee.ToString());
			//Dx taken care of when radio pushed
			switch(ProcedureCode2.TreatArea){
				case TreatmentArea.Surf:
					ProcCur.Surf=textSurfaces.Text;
					ProcCur.ToothNum=Tooth.FromInternat(textTooth.Text);
					break;
				case TreatmentArea.Tooth:
					ProcCur.Surf="";
					ProcCur.ToothNum=Tooth.FromInternat(textTooth.Text);
					break;
				case TreatmentArea.Mouth:
					ProcCur.Surf="";
					ProcCur.ToothNum="";	
					break;
				case TreatmentArea.Quad:
					//surf set when radio pushed
					ProcCur.ToothNum="";	
					break;
				case TreatmentArea.Sextant:
					//surf taken care of when radio pushed
					ProcCur.ToothNum="";	
					break;
				case TreatmentArea.Arch:
					//don't HAVE to select arch
					//taken care of when radio pushed
					ProcCur.ToothNum="";	
					break;
				case TreatmentArea.ToothRange:
					if (listBoxTeeth.SelectedItems.Count<1 && listBoxTeeth2.SelectedItems.Count<1) {
						MessageBox.Show(Lan.g(this,"Must pick at least 1 tooth"));
						return;
					}
          string range="";
		      for(int j=0;j<listBoxTeeth.SelectedItems.Count;j++){
						if(j!=0)
							range+=",";
            range+=Tooth.FromInternat(listBoxTeeth.SelectedItems[j].ToString());
          }
		      for(int j=0;j<listBoxTeeth2.SelectedItems.Count;j++){
						if(j!=0)
							range+=",";
            range+=Tooth.FromInternat(listBoxTeeth2.SelectedItems[j].ToString());
          }
			    ProcCur.ToothRange=range;
					ProcCur.Surf="";
					ProcCur.ToothNum="";	
					break;
			}
			//Status taken care of when radio pushed
			ProcCur.ProcNote=this.textNotes.Text;
			if(listProvNum.SelectedIndex!=-1)
				ProcCur.ProvNum=Providers.List[listProvNum.SelectedIndex].ProvNum;
			if(listDx.SelectedIndex!=-1)
				ProcCur.Dx=Defs.Short[(int)DefCat.Diagnosis][listDx.SelectedIndex].DefNum;
			if(listPriority.SelectedIndex==0)
				ProcCur.Priority=0;
			else
				ProcCur.Priority=Defs.Short[(int)DefCat.TxPriorities][listPriority.SelectedIndex-1].DefNum;
			ProcCur.PlaceService=(PlaceOfService)comboPlaceService.SelectedIndex;
			ProcCur.HideGraphical=checkHideGraphical.Checked;
			if(ProcedureCode2.IsProsth){
				switch(listProsth.SelectedIndex){
					case 0:
						ProcCur.Prosthesis="";
						break;
					case 1:
						ProcCur.Prosthesis="I";
						break;
					case 2:
						ProcCur.Prosthesis="R";
						break;
				}
				ProcCur.DateOriginalProsth=PIn.PDate(textDateOriginalProsth.Text);
			}
			else{
				ProcCur.Prosthesis="";
				ProcCur.DateOriginalProsth=DateTime.MinValue;
			}
			ProcCur.ClaimNote=textClaimNote.Text;
			ProcCur.Update(ProcOld);
			Recalls.Synch(ProcCur.PatNum);
			if(!IsNew){
				if(ProcOld.ProcStatus==ProcStat.C){
					SecurityLogs.MakeLogEntry("Procedure Completed Edit","PatNum:"+ProcCur.PatNum.ToString());
				}
			}
			ProcOld=ProcCur.Copy();//in case we now make more changes.
			//these areas have no autocodes
			if(ProcedureCode2.TreatArea==TreatmentArea.Mouth
				|| ProcedureCode2.TreatArea==TreatmentArea.Quad
				|| ProcedureCode2.TreatArea==TreatmentArea.Sextant)
			{
				DialogResult=DialogResult.OK;
				return;
			}
			//this represents the suggested ADAcode based on the autocodes set up.
			string verifyADA;
			if(ProcedureCode2.TreatArea==TreatmentArea.Arch){
				if(ProcCur.Surf==""){
					DialogResult=DialogResult.OK;
					return;
				}
				if(ProcCur.Surf=="U"){
					verifyADA=AutoCodeItems.VerifyCode
						(ProcCur.ADACode,"1","",false,PatCur.PatNum);//max
				}
				else{
					verifyADA=AutoCodeItems.VerifyCode
						(ProcCur.ADACode,"32","",false,PatCur.PatNum);//mand
				}
			}
			else if(ProcedureCode2.TreatArea==TreatmentArea.ToothRange){
				//test for max or mand.
				if(listBoxTeeth.SelectedItems.Count<1)
					verifyADA=AutoCodeItems.VerifyCode
						(ProcCur.ADACode,"32","",false,PatCur.PatNum);//mand
				else
					verifyADA=AutoCodeItems.VerifyCode
						(ProcCur.ADACode,"1","",false,PatCur.PatNum);//max
			}
			else{//surf or tooth
				verifyADA=AutoCodeItems.VerifyCode
					(ProcCur.ADACode,ProcCur.ToothNum,ProcCur.Surf,false,PatCur.PatNum);
			}
			if(ProcCur.ADACode!=verifyADA){
				string desc=ProcedureCodes.GetProcCode(verifyADA).Descript;
				FormAutoCodeLessIntrusive FormA=new FormAutoCodeLessIntrusive();
				FormA.mainText=verifyADA+" ("+desc+") "+Lan.g(this,"is the recommended procedure code for this procedure.  Change procedure code and fee?");
				FormA.ShowDialog();
				if(FormA.DialogResult!=DialogResult.OK){
					DialogResult=DialogResult.OK;
					return;
				}
				if(FormA.CheckedBox){
					AutoCodes.Cur.LessIntrusive=true;
					AutoCodes.UpdateCur();
					DataValid.IType=InvalidType.LocalData;
					DataValid DataValid2=new DataValid();
					DataValid2.SetInvalid();
				}
				ProcCur.ADACode=verifyADA;
				ProcCur.ProcFee=Fees.GetAmount(ProcCur.ADACode,Fees.GetFeeSched(PatCur,PlanList));
				//Procedures.Cur=ProcCur;
				ProcCur.Update(ProcOld);
				Recalls.Synch(ProcCur.PatNum);
				if(ProcCur.ProcStatus==ProcStat.C){
					SecurityLogs.MakeLogEntry("Procedure Completed Edit","PatNum:"+ProcCur.PatNum.ToString());
				}
			}
      DialogResult=DialogResult.OK;
			//it is assumed that we will do an immediate refresh after closing this window.
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormProcEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK){
				//this catches date,prov,fee,status,etc for all claimProcs attached to this proc:
				ProcCur.ComputeEstimates(PatCur.PatNum,PatCur.PriPlanNum
					,PatCur.SecPlanNum,ClaimProcList,false,PatCur,PlanList);
				return;
			}
			if(IsNew){//if cancelling on a new procedure
				//delete any newly created claimprocs
				for(int i=0;i<ClaimProcList.Length;i++){
					if(ClaimProcList[i].ProcNum==ProcCur.ProcNum){
						ClaimProcList[i].Delete();
					}
				}
			}
		}

		

		

	

		

		

		

		

	

		

		

		
		

		
		

	}
}