/*=============================================================================================================
FreeDental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormProcEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textProc;
		private System.Windows.Forms.TextBox textSurfaces;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textDesc;
		private System.Windows.Forms.TextBox textNotes;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butDelete;
		private System.Windows.Forms.TextBox textRange;
		private System.Windows.Forms.CheckBox checkNoBillIns;
		private System.ComponentModel.Container components = null;// Required designer variable.
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
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textPriEst;
		private System.Windows.Forms.TextBox textSecEst;
		private System.Windows.Forms.GroupBox groupSextant;
		private System.Windows.Forms.RadioButton radioS1;
		private System.Windows.Forms.RadioButton radioS3;
		private System.Windows.Forms.RadioButton radioS2;
		private System.Windows.Forms.RadioButton radioS4;
		private System.Windows.Forms.RadioButton radioS5;
		private System.Windows.Forms.RadioButton radioS6;
		private System.Windows.Forms.ListBox listDx;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ListBox listProvNum;
		private OpenDental.ValidDate textDate;
		private OpenDental.ValidDouble textAmount;
		private System.Windows.Forms.TextBox textPriPercent;
		private System.Windows.Forms.TextBox textSecPercent;
		public bool IsNew;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private OpenDental.ValidDouble textOverrideSec;
		private OpenDental.ValidDouble textOverridePri;
		private System.Windows.Forms.RadioButton radioStatusEO;
		private System.Windows.Forms.RadioButton radioStatusEC;
		private System.Windows.Forms.RadioButton radioStatusC;
		private System.Windows.Forms.RadioButton radioStatusTP;
		private System.Windows.Forms.RadioButton radioStatusR;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupStatus;
		private DateTime OriginalDate;
		private double OriginalFee;
		private System.Windows.Forms.CheckBox checkIsCovIns;
		private System.Windows.Forms.GroupBox groupIns;
		private System.Windows.Forms.Label labelClaim;
		private System.Windows.Forms.ListBox listBoxTeeth;
		private System.Windows.Forms.ListBox listBoxTeeth2;
		private System.Windows.Forms.Button butChange;
		private ProcStat OriginalStatus;
		private System.Windows.Forms.TextBox textTooth;
		private System.Windows.Forms.ErrorProvider errorProvider2;
		private System.Windows.Forms.Button butEditAnyway;
		private ProcedureCode ProcedureCode2;

		public FormProcEdit(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				label1,
				label2,
				label5,
				groupBox1,
				label6,
				label7,
				checkNoBillIns,
				labelTooth,
				labelRange,
				labelSurfaces,
				radioUR,
				radioUL,
				radioLL,
				radioLR,
				groupQuadrant,
				groupArch,
				radioU,
				radioL,
				groupBox4,
				label3,
				label4,
				label8,
				groupSextant,
				radioS1,
				radioS3,
				radioS2,
				radioS4,
				radioS5,
				radioS6,
				label9,
				label10,
				label11,
				label12,
				radioStatusEO,
				radioStatusEC,
				radioStatusC,
				radioStatusTP,
				radioStatusR,
				groupStatus,
				checkIsCovIns,
				groupIns,
				labelClaim,
				butChange,
				butEditAnyway
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
				butDelete,
			});
		}

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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.textOverrideSec = new OpenDental.ValidDouble();
			this.textOverridePri = new OpenDental.ValidDouble();
			this.label6 = new System.Windows.Forms.Label();
			this.groupStatus = new System.Windows.Forms.GroupBox();
			this.radioStatusR = new System.Windows.Forms.RadioButton();
			this.radioStatusEO = new System.Windows.Forms.RadioButton();
			this.radioStatusEC = new System.Windows.Forms.RadioButton();
			this.radioStatusC = new System.Windows.Forms.RadioButton();
			this.radioStatusTP = new System.Windows.Forms.RadioButton();
			this.textDesc = new System.Windows.Forms.TextBox();
			this.checkNoBillIns = new System.Windows.Forms.CheckBox();
			this.textNotes = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.butDelete = new System.Windows.Forms.Button();
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
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.textSecPercent = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textPriPercent = new System.Windows.Forms.TextBox();
			this.textSecEst = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textPriEst = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupIns = new System.Windows.Forms.GroupBox();
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
			this.label10 = new System.Windows.Forms.Label();
			this.textDate = new OpenDental.ValidDate();
			this.textAmount = new OpenDental.ValidDouble();
			this.panel1 = new System.Windows.Forms.Panel();
			this.listBoxTeeth2 = new System.Windows.Forms.ListBox();
			this.listBoxTeeth = new System.Windows.Forms.ListBox();
			this.butChange = new System.Windows.Forms.Button();
			this.labelClaim = new System.Windows.Forms.Label();
			this.checkIsCovIns = new System.Windows.Forms.CheckBox();
			this.errorProvider2 = new System.Windows.Forms.ErrorProvider();
			this.butEditAnyway = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupStatus.SuspendLayout();
			this.groupQuadrant.SuspendLayout();
			this.groupArch.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupIns.SuspendLayout();
			this.groupSextant.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(42, 2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "Date:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(20, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 12);
			this.label2.TabIndex = 1;
			this.label2.Text = "Procedure:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelTooth
			// 
			this.labelTooth.Location = new System.Drawing.Point(46, 58);
			this.labelTooth.Name = "labelTooth";
			this.labelTooth.Size = new System.Drawing.Size(36, 12);
			this.labelTooth.TabIndex = 2;
			this.labelTooth.Text = "Tooth:";
			this.labelTooth.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.labelTooth.Visible = false;
			// 
			// labelSurfaces
			// 
			this.labelSurfaces.Location = new System.Drawing.Point(28, 86);
			this.labelSurfaces.Name = "labelSurfaces";
			this.labelSurfaces.Size = new System.Drawing.Size(56, 16);
			this.labelSurfaces.TabIndex = 3;
			this.labelSurfaces.Text = "Surfaces:";
			this.labelSurfaces.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.labelSurfaces.Visible = false;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(24, 114);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(60, 16);
			this.label5.TabIndex = 4;
			this.label5.Text = "Amount:";
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
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.textOverrideSec);
			this.groupBox1.Controls.Add(this.textOverridePri);
			this.groupBox1.Location = new System.Drawing.Point(238, 24);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(144, 96);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Override Estimate";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(16, 66);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(48, 16);
			this.label12.TabIndex = 15;
			this.label12.Text = "Sec. Ins";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(12, 42);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(52, 16);
			this.label11.TabIndex = 14;
			this.label11.Text = "Prim. Ins";
			// 
			// textOverrideSec
			// 
			this.textOverrideSec.Location = new System.Drawing.Point(64, 62);
			this.textOverrideSec.Name = "textOverrideSec";
			this.textOverrideSec.Size = new System.Drawing.Size(68, 20);
			this.textOverrideSec.TabIndex = 1;
			this.textOverrideSec.Text = "";
			// 
			// textOverridePri
			// 
			this.textOverridePri.Location = new System.Drawing.Point(64, 38);
			this.textOverridePri.Name = "textOverridePri";
			this.textOverridePri.Size = new System.Drawing.Size(68, 20);
			this.textOverridePri.TabIndex = 0;
			this.textOverridePri.Text = "";
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
			this.groupStatus.Location = new System.Drawing.Point(642, 14);
			this.groupStatus.Name = "groupStatus";
			this.groupStatus.Size = new System.Drawing.Size(148, 154);
			this.groupStatus.TabIndex = 4;
			this.groupStatus.TabStop = false;
			this.groupStatus.Text = "Procedure Status";
			// 
			// radioStatusR
			// 
			this.radioStatusR.Location = new System.Drawing.Point(12, 114);
			this.radioStatusR.Name = "radioStatusR";
			this.radioStatusR.Size = new System.Drawing.Size(98, 24);
			this.radioStatusR.TabIndex = 4;
			this.radioStatusR.Text = "Referred Out";
			this.radioStatusR.Click += new System.EventHandler(this.radioStatusR_Click);
			// 
			// radioStatusEO
			// 
			this.radioStatusEO.Location = new System.Drawing.Point(12, 90);
			this.radioStatusEO.Name = "radioStatusEO";
			this.radioStatusEO.Size = new System.Drawing.Size(128, 24);
			this.radioStatusEO.TabIndex = 3;
			this.radioStatusEO.Text = "Existing-Other Prov";
			this.radioStatusEO.Click += new System.EventHandler(this.radioEO_Click);
			// 
			// radioStatusEC
			// 
			this.radioStatusEC.Location = new System.Drawing.Point(12, 66);
			this.radioStatusEC.Name = "radioStatusEC";
			this.radioStatusEC.Size = new System.Drawing.Size(132, 24);
			this.radioStatusEC.TabIndex = 2;
			this.radioStatusEC.Text = "Existing-Current Prov";
			this.radioStatusEC.Click += new System.EventHandler(this.radioExist_Click);
			// 
			// radioStatusC
			// 
			this.radioStatusC.Location = new System.Drawing.Point(12, 42);
			this.radioStatusC.Name = "radioStatusC";
			this.radioStatusC.TabIndex = 1;
			this.radioStatusC.Text = "Completed";
			this.radioStatusC.Click += new System.EventHandler(this.radioC_Click);
			// 
			// radioStatusTP
			// 
			this.radioStatusTP.Location = new System.Drawing.Point(12, 18);
			this.radioStatusTP.Name = "radioStatusTP";
			this.radioStatusTP.TabIndex = 0;
			this.radioStatusTP.Text = "Treatment Plan";
			this.radioStatusTP.Click += new System.EventHandler(this.radioTP_Click);
			// 
			// textDesc
			// 
			this.textDesc.BackColor = System.Drawing.SystemColors.Control;
			this.textDesc.Location = new System.Drawing.Point(168, 28);
			this.textDesc.Name = "textDesc";
			this.textDesc.Size = new System.Drawing.Size(180, 20);
			this.textDesc.TabIndex = 16;
			this.textDesc.Text = "";
			// 
			// checkNoBillIns
			// 
			this.checkNoBillIns.Location = new System.Drawing.Point(20, 128);
			this.checkNoBillIns.Name = "checkNoBillIns";
			this.checkNoBillIns.Size = new System.Drawing.Size(208, 24);
			this.checkNoBillIns.TabIndex = 1;
			this.checkNoBillIns.Text = "Do Not Bill to Dental Insurance";
			// 
			// textNotes
			// 
			this.textNotes.Location = new System.Drawing.Point(50, 434);
			this.textNotes.Multiline = true;
			this.textNotes.Name = "textNotes";
			this.textNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNotes.Size = new System.Drawing.Size(508, 136);
			this.textNotes.TabIndex = 7;
			this.textNotes.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(50, 416);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(60, 16);
			this.label7.TabIndex = 19;
			this.label7.Text = "Notes:";
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(716, 546);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 9;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.Location = new System.Drawing.Point(716, 580);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(76, 24);
			this.butCancel.TabIndex = 10;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butDelete
			// 
			this.butDelete.Location = new System.Drawing.Point(50, 584);
			this.butDelete.Name = "butDelete";
			this.butDelete.TabIndex = 8;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// labelRange
			// 
			this.labelRange.Location = new System.Drawing.Point(2, 58);
			this.labelRange.Name = "labelRange";
			this.labelRange.Size = new System.Drawing.Size(82, 16);
			this.labelRange.TabIndex = 33;
			this.labelRange.Text = "Tooth Range:";
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
			this.radioLR.Location = new System.Drawing.Point(12, 36);
			this.radioLR.Name = "radioLR";
			this.radioLR.Size = new System.Drawing.Size(40, 16);
			this.radioLR.TabIndex = 3;
			this.radioLR.Text = "LR";
			this.radioLR.Click += new System.EventHandler(this.radioLR_Click);
			// 
			// radioLL
			// 
			this.radioLL.Location = new System.Drawing.Point(64, 36);
			this.radioLL.Name = "radioLL";
			this.radioLL.Size = new System.Drawing.Size(40, 16);
			this.radioLL.TabIndex = 1;
			this.radioLL.Text = "LL";
			this.radioLL.Click += new System.EventHandler(this.radioLL_Click);
			// 
			// radioUL
			// 
			this.radioUL.Location = new System.Drawing.Point(64, 16);
			this.radioUL.Name = "radioUL";
			this.radioUL.Size = new System.Drawing.Size(40, 16);
			this.radioUL.TabIndex = 0;
			this.radioUL.Text = "UL";
			this.radioUL.Click += new System.EventHandler(this.radioUL_Click);
			// 
			// radioUR
			// 
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
			this.radioL.Location = new System.Drawing.Point(12, 36);
			this.radioL.Name = "radioL";
			this.radioL.Size = new System.Drawing.Size(28, 16);
			this.radioL.TabIndex = 1;
			this.radioL.Text = "L";
			this.radioL.Click += new System.EventHandler(this.radioL_Click);
			// 
			// radioU
			// 
			this.radioU.Location = new System.Drawing.Point(12, 16);
			this.radioU.Name = "radioU";
			this.radioU.Size = new System.Drawing.Size(32, 16);
			this.radioU.TabIndex = 0;
			this.radioU.Text = "U";
			this.radioU.Click += new System.EventHandler(this.radioU_Click);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.textSecPercent);
			this.groupBox4.Controls.Add(this.label8);
			this.groupBox4.Controls.Add(this.textPriPercent);
			this.groupBox4.Controls.Add(this.textSecEst);
			this.groupBox4.Controls.Add(this.label4);
			this.groupBox4.Controls.Add(this.textPriEst);
			this.groupBox4.Controls.Add(this.label3);
			this.groupBox4.Location = new System.Drawing.Point(20, 24);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(182, 96);
			this.groupBox4.TabIndex = 40;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Estimate";
			// 
			// textSecPercent
			// 
			this.textSecPercent.Location = new System.Drawing.Point(60, 64);
			this.textSecPercent.Name = "textSecPercent";
			this.textSecPercent.ReadOnly = true;
			this.textSecPercent.Size = new System.Drawing.Size(40, 20);
			this.textSecPercent.TabIndex = 6;
			this.textSecPercent.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(58, 24);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(46, 12);
			this.label8.TabIndex = 5;
			this.label8.Text = "Percent";
			// 
			// textPriPercent
			// 
			this.textPriPercent.Location = new System.Drawing.Point(60, 40);
			this.textPriPercent.Name = "textPriPercent";
			this.textPriPercent.ReadOnly = true;
			this.textPriPercent.Size = new System.Drawing.Size(40, 20);
			this.textPriPercent.TabIndex = 4;
			this.textPriPercent.Text = "";
			// 
			// textSecEst
			// 
			this.textSecEst.Location = new System.Drawing.Point(110, 64);
			this.textSecEst.Name = "textSecEst";
			this.textSecEst.ReadOnly = true;
			this.textSecEst.Size = new System.Drawing.Size(60, 20);
			this.textSecEst.TabIndex = 3;
			this.textSecEst.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(10, 66);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 16);
			this.label4.TabIndex = 2;
			this.label4.Text = "Sec. Ins";
			// 
			// textPriEst
			// 
			this.textPriEst.Location = new System.Drawing.Point(110, 40);
			this.textPriEst.Name = "textPriEst";
			this.textPriEst.ReadOnly = true;
			this.textPriEst.Size = new System.Drawing.Size(60, 20);
			this.textPriEst.TabIndex = 1;
			this.textPriEst.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 42);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(52, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "Prim. Ins";
			// 
			// groupIns
			// 
			this.groupIns.Controls.Add(this.groupBox4);
			this.groupIns.Controls.Add(this.groupBox1);
			this.groupIns.Controls.Add(this.checkNoBillIns);
			this.groupIns.Location = new System.Drawing.Point(396, 240);
			this.groupIns.Name = "groupIns";
			this.groupIns.Size = new System.Drawing.Size(396, 160);
			this.groupIns.TabIndex = 6;
			this.groupIns.TabStop = false;
			this.groupIns.Text = "Insurance";
			// 
			// groupSextant
			// 
			this.groupSextant.Controls.Add(this.radioS6);
			this.groupSextant.Controls.Add(this.radioS5);
			this.groupSextant.Controls.Add(this.radioS4);
			this.groupSextant.Controls.Add(this.radioS2);
			this.groupSextant.Controls.Add(this.radioS3);
			this.groupSextant.Controls.Add(this.radioS1);
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
			this.radioS6.Location = new System.Drawing.Point(12, 36);
			this.radioS6.Name = "radioS6";
			this.radioS6.Size = new System.Drawing.Size(36, 16);
			this.radioS6.TabIndex = 5;
			this.radioS6.Text = "6";
			this.radioS6.Click += new System.EventHandler(this.radioS6_Click);
			// 
			// radioS5
			// 
			this.radioS5.Location = new System.Drawing.Point(60, 36);
			this.radioS5.Name = "radioS5";
			this.radioS5.Size = new System.Drawing.Size(36, 16);
			this.radioS5.TabIndex = 4;
			this.radioS5.Text = "5";
			this.radioS5.Click += new System.EventHandler(this.radioS5_Click);
			// 
			// radioS4
			// 
			this.radioS4.Location = new System.Drawing.Point(108, 36);
			this.radioS4.Name = "radioS4";
			this.radioS4.Size = new System.Drawing.Size(36, 16);
			this.radioS4.TabIndex = 1;
			this.radioS4.Text = "4";
			this.radioS4.Click += new System.EventHandler(this.radioS4_Click);
			// 
			// radioS2
			// 
			this.radioS2.Location = new System.Drawing.Point(60, 16);
			this.radioS2.Name = "radioS2";
			this.radioS2.Size = new System.Drawing.Size(36, 16);
			this.radioS2.TabIndex = 2;
			this.radioS2.Text = "2";
			this.radioS2.Click += new System.EventHandler(this.radioS2_Click);
			// 
			// radioS3
			// 
			this.radioS3.Location = new System.Drawing.Point(108, 16);
			this.radioS3.Name = "radioS3";
			this.radioS3.Size = new System.Drawing.Size(36, 16);
			this.radioS3.TabIndex = 0;
			this.radioS3.Text = "3";
			this.radioS3.Click += new System.EventHandler(this.radioS3_Click);
			// 
			// radioS1
			// 
			this.radioS1.Location = new System.Drawing.Point(12, 16);
			this.radioS1.Name = "radioS1";
			this.radioS1.Size = new System.Drawing.Size(36, 16);
			this.radioS1.TabIndex = 0;
			this.radioS1.Text = "1";
			this.radioS1.Click += new System.EventHandler(this.radioS1_Click);
			// 
			// listProvNum
			// 
			this.listProvNum.Location = new System.Drawing.Point(216, 176);
			this.listProvNum.Name = "listProvNum";
			this.listProvNum.Size = new System.Drawing.Size(120, 225);
			this.listProvNum.TabIndex = 3;
			// 
			// listDx
			// 
			this.listDx.Location = new System.Drawing.Point(52, 176);
			this.listDx.Name = "listDx";
			this.listDx.Size = new System.Drawing.Size(120, 225);
			this.listDx.TabIndex = 2;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(216, 160);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(100, 14);
			this.label9.TabIndex = 45;
			this.label9.Text = "Provider";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(52, 158);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(100, 14);
			this.label10.TabIndex = 46;
			this.label10.Text = "Diagnosis";
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(84, 2);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(76, 20);
			this.textDate.TabIndex = 0;
			this.textDate.Text = "";
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(84, 114);
			this.textAmount.Name = "textAmount";
			this.textAmount.Size = new System.Drawing.Size(66, 20);
			this.textAmount.TabIndex = 6;
			this.textAmount.Text = "";
			this.textAmount.TextChanged += new System.EventHandler(this.textAmount_TextChanged);
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
			this.panel1.Controls.Add(this.textAmount);
			this.panel1.Controls.Add(this.textDesc);
			this.panel1.Controls.Add(this.textTooth);
			this.panel1.Controls.Add(this.labelRange);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.textProc);
			this.panel1.Location = new System.Drawing.Point(12, 8);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(576, 140);
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
			this.butChange.Location = new System.Drawing.Point(368, 36);
			this.butChange.Name = "butChange";
			this.butChange.Size = new System.Drawing.Size(74, 22);
			this.butChange.TabIndex = 37;
			this.butChange.Text = "Change";
			this.butChange.Click += new System.EventHandler(this.butChange_Click);
			// 
			// labelClaim
			// 
			this.labelClaim.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelClaim.Location = new System.Drawing.Point(144, 582);
			this.labelClaim.Name = "labelClaim";
			this.labelClaim.Size = new System.Drawing.Size(480, 32);
			this.labelClaim.TabIndex = 50;
			this.labelClaim.Text = "This procedure is attached to a claim, so certain fields should not be edited.  Y" +
				"ou should reprint the claim if any significant changes are made.";
			this.labelClaim.Visible = false;
			// 
			// checkIsCovIns
			// 
			this.checkIsCovIns.Location = new System.Drawing.Point(396, 210);
			this.checkIsCovIns.Name = "checkIsCovIns";
			this.checkIsCovIns.Size = new System.Drawing.Size(136, 18);
			this.checkIsCovIns.TabIndex = 5;
			this.checkIsCovIns.Text = "Patient has insurance";
			this.checkIsCovIns.CheckedChanged += new System.EventHandler(this.checkIsCovIns_CheckedChanged);
			// 
			// errorProvider2
			// 
			this.errorProvider2.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.errorProvider2.ContainerControl = this;
			// 
			// butEditAnyway
			// 
			this.butEditAnyway.Location = new System.Drawing.Point(582, 546);
			this.butEditAnyway.Name = "butEditAnyway";
			this.butEditAnyway.Size = new System.Drawing.Size(104, 24);
			this.butEditAnyway.TabIndex = 51;
			this.butEditAnyway.Text = "Edit Anyway";
			this.butEditAnyway.Visible = false;
			this.butEditAnyway.Click += new System.EventHandler(this.butEditAnyway_Click);
			// 
			// FormProcEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(816, 628);
			this.Controls.Add(this.butEditAnyway);
			this.Controls.Add(this.butChange);
			this.Controls.Add(this.checkIsCovIns);
			this.Controls.Add(this.labelClaim);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.listDx);
			this.Controls.Add(this.listProvNum);
			this.Controls.Add(this.groupIns);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textNotes);
			this.Controls.Add(this.groupStatus);
			this.Name = "FormProcEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Procedure Info";
			this.Load += new System.EventHandler(this.FormProcInfo_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupStatus.ResumeLayout(false);
			this.groupQuadrant.ResumeLayout(false);
			this.groupArch.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupIns.ResumeLayout(false);
			this.groupSextant.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormProcInfo_Load(object sender, System.EventArgs e){
			Claims.Refresh();
			ProcedureCode2 = ProcCodes.GetProcCode(Procedures.Cur.ADACode);
			if (IsNew){
				
			}
			else{
				if(Procedures.Cur.ProcStatus==ProcStat.C){
					if(!UserPermissions.CheckUserPassword("Procedure Completed Edit",Procedures.Cur.ProcDate)){
						//MessageBox.Show(Lan.g(this,"You only have permission to view the Procedure. No changes will be saved"));
						butOK.Enabled=false;
						butDelete.Enabled=false;
						butChange.Enabled=false;
						butEditAnyway.Enabled=false;
					}					
				}
  			OriginalDate=Procedures.Cur.ProcDate;
				OriginalFee=Procedures.Cur.ProcFee;
				OriginalStatus=Procedures.Cur.ProcStatus;
			}
			if(Procedures.Cur.ClaimNum!=0){//attached to claim
				checkIsCovIns.Enabled=false;
				if( ((Claim)Claims.HList[Procedures.Cur.ClaimNum]).ClaimStatus != "U"
					&& ((Claim)Claims.HList[Procedures.Cur.ClaimNum]).ClaimStatus != "H" ){
					panel1.Enabled=false;
					groupStatus.Enabled=false;
					checkNoBillIns.Enabled=false;
					butDelete.Enabled=false;
					butChange.Enabled=false;
					butEditAnyway.Visible=true;
					labelClaim.Visible=true;
				}
			}
			SetControls();
		}		

		private void SetControls(){
			textDate.Text=Procedures.Cur.ProcDate.ToString("d");
			textProc.Text=Procedures.Cur.ADACode;
			textDesc.Text=ProcedureCode2.Descript;
			switch (ProcedureCode2.TreatArea){
				case TreatmentArea.Surf:
					this.textTooth.Visible=true;
					this.labelTooth.Visible=true;
					this.textSurfaces.Visible=true;
					this.labelSurfaces.Visible=true;
					if(!Tooth.IsValidDB(Procedures.Cur.ToothNum))
						errorProvider2.SetError(textTooth,Lan.g(this,"Invalid tooth number."));
					else
						errorProvider2.SetError(textTooth,"");
					textTooth.Text=Procedures.Cur.ToothNum;
					textSurfaces.Text=Procedures.Cur.Surf;
					textSurfaces.Text=Tooth.SurfTidy(textSurfaces.Text,textTooth.Text);
					if(textSurfaces.Text=="")
						errorProvider2.SetError(textSurfaces,"No surfaces selected.");
					else
						errorProvider2.SetError(textSurfaces,"");
					break;
				case TreatmentArea.Tooth:
					this.textTooth.Visible=true;
					this.labelTooth.Visible=true;
					if(!Tooth.IsValidDB(Procedures.Cur.ToothNum))
						errorProvider2.SetError(textTooth,Lan.g(this,"Invalid tooth number."));
					else
						errorProvider2.SetError(textTooth,"");
					textTooth.Text=Procedures.Cur.ToothNum;
					break;
				case TreatmentArea.Mouth:
						break;
				case TreatmentArea.Quad:
					this.groupQuadrant.Visible=true;
					switch (Procedures.Cur.Surf){
						case "UR": this.radioUR.Checked=true; break;
						case "UL": this.radioUL.Checked=true; break;
						case "LR": this.radioLR.Checked=true; break;
						case "LL": this.radioLL.Checked=true; break;
						//default : 
					}
					break;
				case TreatmentArea.Sextant:
					this.groupSextant.Visible=true;
					switch (Procedures.Cur.Surf){
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
					switch (Procedures.Cur.Surf){
						case "U": this.radioU.Checked=true; break;
						case "L": this.radioL.Checked=true; break;
					}
					break;
				case TreatmentArea.ToothRange:
					this.labelRange.Visible=true;
					this.listBoxTeeth.Visible=true;
					this.listBoxTeeth2.Visible=true;
					if(Procedures.Cur.ToothRange==null){
						break;
					}
   			  string[] sArray=Procedures.Cur.ToothRange.Split(',');
          for(int i=0;i<sArray.Length;i++)  {
            for(int j=0;j<listBoxTeeth.Items.Count;j++)  {
              if(sArray[i]==listBoxTeeth.Items[j].ToString())
				 		    listBoxTeeth.SelectedItem=sArray[i];
					  }
  			    for(int j=0;j<listBoxTeeth2.Items.Count;j++)  {
              if(sArray[i]==listBoxTeeth2.Items[j].ToString())
				 		    listBoxTeeth2.SelectedItem=sArray[i];
            }
					} 
					break;
			}//end switch
			textAmount.Text=Procedures.Cur.ProcFee.ToString("F");
			switch (Procedures.Cur.ProcStatus){
				case ProcStat.TP: radioStatusTP.Checked=true; break;
				case ProcStat.C: radioStatusC.Checked=true; break;
				case ProcStat.EC: radioStatusEC.Checked=true; break;
				case ProcStat.EO: radioStatusEO.Checked=true; break;
				case ProcStat.R: radioStatusR.Checked=true; break;
			}
			for(int i=0;i<Defs.Short[(int)DefCat.Diagnosis].Length;i++){
				this.listDx.Items.Add(Defs.Short[(int)DefCat.Diagnosis][i].ItemName);
				if(Defs.Short[(int)DefCat.Diagnosis][i].DefNum==Procedures.Cur.Dx)
					listDx.SelectedIndex=i;
			}
			for(int i=0;i<Providers.List.Length;i++){
				this.listProvNum.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==Procedures.Cur.ProvNum)
					listProvNum.SelectedIndex=i;
			}
			if(Procedures.Cur.OverridePri==-1)
				textOverridePri.Text="";
			else
				textOverridePri.Text=Procedures.Cur.OverridePri.ToString("F");
			if(Procedures.Cur.OverrideSec==-1)
				textOverrideSec.Text="";
			else 
				textOverrideSec.Text=Procedures.Cur.OverrideSec.ToString("F");
			checkNoBillIns.Checked=Procedures.Cur.NoBillIns;
			textNotes.Text=Procedures.Cur.ProcNote;
			checkIsCovIns.Checked=Procedures.Cur.IsCovIns;
			if(checkIsCovIns.Checked){
				groupIns.Visible=true;
			}
			else groupIns.Visible=false;
		}//end SetControls

		private void computeEstimates(){
			try{
				double amt=Convert.ToDouble(textAmount.Text.ToString());
				Procedures.Cur.ProcFee=PIn.PDouble(textAmount.Text.ToString());
			}
			catch{
				return;
			}
			double priPercent=CovPats.GetPercent(Procedures.Cur.ADACode,PriSecTot.Pri);
			double secPercent=CovPats.GetPercent(Procedures.Cur.ADACode,PriSecTot.Sec);
			textPriPercent.Text=(priPercent*100).ToString();
			textSecPercent.Text=(secPercent*100).ToString();
			double priEst=Procedures.Cur.ProcFee*priPercent;
			double secEst=Procedures.Cur.ProcFee*secPercent;
			textPriEst.Text=(priEst).ToString("F");
			if(Procedures.Cur.ProcFee-priEst < secEst)
				secEst=Procedures.Cur.ProcFee-priEst;
			textSecEst.Text=secEst.ToString("F");
		}

		private void textAmount_TextChanged(object sender, System.EventArgs e) {
			computeEstimates();
		}

		private void checkIsCovIns_CheckedChanged(object sender, System.EventArgs e) {
			if(checkIsCovIns.Checked){
				groupIns.Visible=true;
			}
			else groupIns.Visible=false;
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
			textSurfaces.Text=Tooth.SurfTidy(textSurfaces.Text,textTooth.Text);
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
      if(FormP.DialogResult!=DialogResult.Cancel){
        Procedures.Cur.ADACode=FormP.SelectedADA;
        ProcedureCode2 = ProcCodes.GetProcCode(FormP.SelectedADA);
        textDesc.Text=ProcedureCode2.Descript;
        Fees Fees=new Fees();
        Procedures.Cur.ProcFee=Fees.GetAmount(FormP.SelectedADA,ContrChart.GetFeeSched());
        SetControls();
      }
		}

		private void butEditAnyway_Click(object sender, System.EventArgs e) {
			panel1.Enabled=true;
			groupStatus.Enabled=true;
			checkNoBillIns.Enabled=true;
			butDelete.Enabled=true;
			butChange.Enabled=true;
		}

		private void butOK_Click(object sender, System.EventArgs e){
			if(  textDate.errorProvider1.GetError(textDate)!=""
				|| textAmount.errorProvider1.GetError(textAmount)!=""
				|| textOverridePri.errorProvider1.GetError(textOverridePri)!=""
				|| textOverrideSec.errorProvider1.GetError(textOverrideSec)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(errorProvider2.GetError(textSurfaces)!=""
				|| errorProvider2.GetError(textTooth)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(textAmount.Text==""){
				textAmount.Text="0";
			}
			Procedures.Cur.PatNum=Patients.Cur.PatNum;
			Procedures.Cur.ADACode=this.textProc.Text;
			Procedures.Cur.ProcDate=PIn.PDate(this.textDate.Text);
			Procedures.Cur.ProcFee=System.Convert.ToDouble(textAmount.Text);
			if(textOverridePri.Text=="")
				Procedures.Cur.OverridePri=-1;
			else
				Procedures.Cur.OverridePri=System.Convert.ToDouble(textOverridePri.Text);
			if(textOverrideSec.Text=="")
				Procedures.Cur.OverrideSec=-1;
			else
				Procedures.Cur.OverrideSec=System.Convert.ToDouble(textOverrideSec.Text);
			//Dx taken care of when radio pushed
			switch (ProcedureCode2.TreatArea){
				case TreatmentArea.Surf:
					Procedures.Cur.Surf=textSurfaces.Text;
					Procedures.Cur.ToothNum=textTooth.Text;
					break;
				case TreatmentArea.Tooth:
					Procedures.Cur.ToothNum=textTooth.Text;
					break;
				case TreatmentArea.Mouth:
						Procedures.Cur.Surf="";
						break;
				case TreatmentArea.Quad:
					//value set when radio pushed
					break;
				case TreatmentArea.Sextant:
					//taken care of when radio pushed
					break;
				case TreatmentArea.Arch:
					//don't HAVE to select arch
					//taken care of when radio pushed
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
            range+=listBoxTeeth.SelectedItems[j].ToString();
          }
		      for(int j=0;j<listBoxTeeth2.SelectedItems.Count;j++){
						if(j!=0)
							range+=",";
            range+=listBoxTeeth2.SelectedItems[j].ToString();
          }
			    Procedures.Cur.ToothRange=range;
					break;
			}
			Procedures.Cur.NoBillIns=this.checkNoBillIns.Checked;
			//Status taken care of when radio pushed
			Procedures.Cur.ProcNote=this.textNotes.Text;
			if(listProvNum.SelectedIndex!=-1)
				Procedures.Cur.ProvNum=Providers.List[listProvNum.SelectedIndex].ProvNum;
			if(listDx.SelectedIndex!=-1)
				Procedures.Cur.Dx=Defs.Short[(int)DefCat.Diagnosis][listDx.SelectedIndex].DefNum;
			Procedures.Cur.IsCovIns=checkIsCovIns.Checked;
			if (IsNew){
				Procedures.InsertCur();
			}
			else{
				Procedures.UpdateCur();
				if(Procedures.Cur.ProcStatus==ProcStat.C){
					SecurityLogs.MakeLogEntry("Procedure Completed Edit",Procedures.cmd.CommandText);
				}
			}
			if(ProcedureCode2.TreatArea==TreatmentArea.Mouth
				|| ProcedureCode2.TreatArea==TreatmentArea.Quad
				|| ProcedureCode2.TreatArea==TreatmentArea.Sextant){
				DialogResult = DialogResult.OK;
				return;
			}
			string verifyADA;
			if(ProcedureCode2.TreatArea==TreatmentArea.Arch){
				if(Procedures.Cur.Surf==""){
					DialogResult = DialogResult.OK;
					return;
				}
				if(Procedures.Cur.Surf=="U"){
					verifyADA=AutoCodeItems.VerifyCode
						(Procedures.Cur.ADACode,"1","",false);//max
				}
				else{
					verifyADA=AutoCodeItems.VerifyCode
						(Procedures.Cur.ADACode,"32","",false);//mand
				}
			}
			else if(ProcedureCode2.TreatArea==TreatmentArea.ToothRange){
				//test for max or mand.
				if(listBoxTeeth.SelectedItems.Count<1)
					verifyADA=AutoCodeItems.VerifyCode
						(Procedures.Cur.ADACode,"32","",false);//mand
				else
					verifyADA=AutoCodeItems.VerifyCode
						(Procedures.Cur.ADACode,"1","",false);//max
			}
			else{//surf or tooth
				verifyADA=AutoCodeItems.VerifyCode
					(Procedures.Cur.ADACode,Procedures.Cur.ToothNum,Procedures.Cur.Surf,false);
			}
			if(Procedures.Cur.ADACode!=verifyADA){
				if(MessageBox.Show(verifyADA+" "+Lan.g(this,"is the recommended procedure code for this procedure.  Change procedure code and fee?"
					),"",MessageBoxButtons.YesNo)==DialogResult.Yes){
					Procedures.Cur.ADACode=verifyADA;
					Procedures.Cur.ProcFee=Fees.GetAmount(Procedures.Cur.ADACode,ContrChart.GetFeeSched());
					Procedures.UpdateCur();
					if(Procedures.Cur.ProcStatus==ProcStat.C){
						SecurityLogs.MakeLogEntry("Procedure Completed Edit",Procedures.cmd.CommandText);
					}
				}
			}
      DialogResult=DialogResult.OK;
		}//end method save data


		private void radioTP_Click(object sender, System.EventArgs e) {
			Procedures.Cur.ProcStatus=ProcStat.TP;
		}

		private void radioC_Click(object sender, System.EventArgs e) {
			Procedures.Cur.ProcStatus=ProcStat.C;
		}

		private void radioExist_Click(object sender, System.EventArgs e) {
			Procedures.Cur.ProcStatus=ProcStat.EC;
		}

		private void radioEO_Click(object sender, System.EventArgs e) {
			Procedures.Cur.ProcStatus=ProcStat.EO;
		}

		private void radioStatusR_Click(object sender, System.EventArgs e) {
			Procedures.Cur.ProcStatus=ProcStat.R;
		}

		private void radioUR_Click(object sender, System.EventArgs e) {
			Procedures.Cur.Surf="UR";
		}

		private void radioUL_Click(object sender, System.EventArgs e) {
			Procedures.Cur.Surf="UL";
		}

		private void radioLR_Click(object sender, System.EventArgs e) {
			Procedures.Cur.Surf="LR";
		}

		private void radioLL_Click(object sender, System.EventArgs e) {
			Procedures.Cur.Surf="LL";
		}

		private void radioU_Click(object sender, System.EventArgs e) {
			Procedures.Cur.Surf="U";
		}

		private void radioL_Click(object sender, System.EventArgs e) {
			Procedures.Cur.Surf="L";
		}

		private void radioS1_Click(object sender, System.EventArgs e) {
			Procedures.Cur.Surf="1";
		}

		private void radioS2_Click(object sender, System.EventArgs e) {
			Procedures.Cur.Surf="2";
		}

		private void radioS3_Click(object sender, System.EventArgs e) {
			Procedures.Cur.Surf="3";
		}

		private void radioS4_Click(object sender, System.EventArgs e) {
			Procedures.Cur.Surf="4";
		}

		private void radioS5_Click(object sender, System.EventArgs e) {
			Procedures.Cur.Surf="5";
		}

		private void radioS6_Click(object sender, System.EventArgs e) {
			Procedures.Cur.Surf="6";
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"Delete Procedure?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			if(IsNew){
				DialogResult=DialogResult.Cancel;
			}
			else{
				Procedures.DeleteCur();
				DialogResult=DialogResult.OK;
			}	
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		
		

	}
}
