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

	public class FormClaimEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private OpenDental.TableClaimProc tbProc;
		private System.Windows.Forms.TextBox textReasonUnder;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.TextBox textDateService;
		private System.Windows.Forms.TextBox textPreAuth;
		private OpenDental.ValidDate textDateRec;
		private OpenDental.ValidDate textDateSent;
		private System.ComponentModel.Container components = null;// Required designer variable.
		private System.Windows.Forms.Button butOK;
		public bool IsNew;
    //private bool IsPreAuth;
		private System.Windows.Forms.Button butDelete;
		private System.Windows.Forms.RadioButton radioProsthN;
		private System.Windows.Forms.RadioButton radioProsthR;
		private System.Windows.Forms.RadioButton radioProsthI;
		private System.Windows.Forms.RadioButton radioStatusR;
		private System.Windows.Forms.RadioButton radioStatusS;
		private System.Windows.Forms.RadioButton radioStatusW;
		private System.Windows.Forms.RadioButton radioStatusP;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button butSend;
		private System.Windows.Forms.Button butPrint;
		private System.Windows.Forms.TextBox textPlan;
		private System.Windows.Forms.TextBox textInsPayEst;
		private System.Windows.Forms.TextBox textClaimFee;
		private System.Windows.Forms.Label label4;
		private OpenDental.ValidDouble textDedApplied;
		private OpenDental.ValidDouble textInsPayAmt;
		private OpenDental.ValidDouble textOverMax;
		private OpenDental.ValidDate textPriorDate;
		private System.Windows.Forms.TextBox textInsPayEstSubtotal;
		private System.Windows.Forms.Label labelPat;
		private System.Windows.Forms.Label labelPriSec;
		private double ClaimFee;
		private double PriInsPayEstSubtotal;
		private double SecInsPayEstSubtotal;
		private System.Windows.Forms.Button butCreateSec;
		private double PriInsPayEst;
		private double SecInsPayEst;
		private System.Windows.Forms.RadioButton radioStatusH;
		private bool IsPrimary;
		private System.Windows.Forms.Button butViewCheck;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Button butPayWizard;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.TextBox textDedAdj;
		private System.Windows.Forms.RadioButton radioStatusU;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.ListBox listProvBill;
		private System.Windows.Forms.ListBox listProvTreat;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Button butRefer;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Button butSupplemental;
		private System.Windows.Forms.Panel panelSup;
		private System.Windows.Forms.ComboBox comboPlaceService;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.ListBox listAccident;
		private OpenDental.ValidDate textAccidentDate;
		private System.Windows.Forms.TextBox textAccidentST;
		private System.Windows.Forms.ListBox listEmploy;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.CheckBox checkIsOrtho;
		private OpenDental.ValidNum textOrthoRemainM;
		private OpenDental.ValidDate textOrthoDate;
		private System.Windows.Forms.Label label27;
		private FormClaimReferral FormCRef=new FormClaimReferral();
		private System.Windows.Forms.Label labelPaymentAmt;
		private System.Windows.Forms.Label labelPreAuthNum;
		private System.Windows.Forms.Label labelDateService;
		private System.Windows.Forms.Panel panelFinancial;
		private System.Windows.Forms.GroupBox groupStatus;
		private double DedAdjPerc;

		public FormClaimEdit(){
			InitializeComponent();// Required for Windows Form Designer support
			tbProc.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbProc_CellDoubleClicked);
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label1,
				this.label10,
				this.label11,
				this.labelPreAuthNum,
				this.label13,
				this.label14,
				this.label15,
				this.label16,
				this.label17,
				this.label18,
				this.label19,
				this.labelPaymentAmt,
				this.label20,
				this.label21,
				this.label22,
				this.label23,
				this.label24,
				this.label25,
				this.label26,
				this.label27,
				this.label3,
				this.label4,
				this.label5,
				this.label6,
				this.label7,
				this.label8,
				this.labelDateService,
				this.labelPat,
				this.labelPriSec,
				this.butCreateSec,
				this.butPayWizard,
				this.butRefer,
				this.butSend,
				this.butSupplemental,
				this.butViewCheck,
				this.radioProsthI,
				this.radioProsthN,
				this.radioStatusH,
				this.radioStatusP,
				this.radioStatusR,
				this.radioStatusS,
				this.radioStatusU,
				this.radioStatusW,
				this.checkIsOrtho,
				this.panelSup,
				this.groupBox1,
				this.groupBox2,
				this.groupStatus
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
				butDelete,
				butPrint,
				
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormClaimEdit));
			this.labelPaymentAmt = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.labelDateService = new System.Windows.Forms.Label();
			this.labelPreAuthNum = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.butViewCheck = new System.Windows.Forms.Button();
			this.label16 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textPriorDate = new OpenDental.ValidDate();
			this.label18 = new System.Windows.Forms.Label();
			this.radioProsthN = new System.Windows.Forms.RadioButton();
			this.radioProsthR = new System.Windows.Forms.RadioButton();
			this.radioProsthI = new System.Windows.Forms.RadioButton();
			this.label17 = new System.Windows.Forms.Label();
			this.groupStatus = new System.Windows.Forms.GroupBox();
			this.radioStatusH = new System.Windows.Forms.RadioButton();
			this.radioStatusP = new System.Windows.Forms.RadioButton();
			this.radioStatusW = new System.Windows.Forms.RadioButton();
			this.textDateSent = new OpenDental.ValidDate();
			this.textDateRec = new OpenDental.ValidDate();
			this.radioStatusU = new System.Windows.Forms.RadioButton();
			this.radioStatusR = new System.Windows.Forms.RadioButton();
			this.radioStatusS = new System.Windows.Forms.RadioButton();
			this.label7 = new System.Windows.Forms.Label();
			this.tbProc = new OpenDental.TableClaimProc();
			this.label11 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.listProvBill = new System.Windows.Forms.ListBox();
			this.textReasonUnder = new System.Windows.Forms.TextBox();
			this.textNote = new System.Windows.Forms.TextBox();
			this.textInsPayEst = new System.Windows.Forms.TextBox();
			this.textInsPayEstSubtotal = new System.Windows.Forms.TextBox();
			this.textDateService = new System.Windows.Forms.TextBox();
			this.textPreAuth = new System.Windows.Forms.TextBox();
			this.butOK = new System.Windows.Forms.Button();
			this.butDelete = new System.Windows.Forms.Button();
			this.textClaimFee = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butSend = new System.Windows.Forms.Button();
			this.butPrint = new System.Windows.Forms.Button();
			this.textPlan = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textDedApplied = new OpenDental.ValidDouble();
			this.textInsPayAmt = new OpenDental.ValidDouble();
			this.textOverMax = new OpenDental.ValidDouble();
			this.labelPat = new System.Windows.Forms.Label();
			this.labelPriSec = new System.Windows.Forms.Label();
			this.butCreateSec = new System.Windows.Forms.Button();
			this.label15 = new System.Windows.Forms.Label();
			this.butPayWizard = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.textDedAdj = new System.Windows.Forms.TextBox();
			this.label20 = new System.Windows.Forms.Label();
			this.listProvTreat = new System.Windows.Forms.ListBox();
			this.label21 = new System.Windows.Forms.Label();
			this.butRefer = new System.Windows.Forms.Button();
			this.comboPlaceService = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.panelSup = new System.Windows.Forms.Panel();
			this.listEmploy = new System.Windows.Forms.ListBox();
			this.label25 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label24 = new System.Windows.Forms.Label();
			this.textAccidentST = new System.Windows.Forms.TextBox();
			this.textAccidentDate = new OpenDental.ValidDate();
			this.label23 = new System.Windows.Forms.Label();
			this.listAccident = new System.Windows.Forms.ListBox();
			this.butSupplemental = new System.Windows.Forms.Button();
			this.label22 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.textOrthoDate = new OpenDental.ValidDate();
			this.label27 = new System.Windows.Forms.Label();
			this.textOrthoRemainM = new OpenDental.ValidNum();
			this.checkIsOrtho = new System.Windows.Forms.CheckBox();
			this.label26 = new System.Windows.Forms.Label();
			this.panelFinancial = new System.Windows.Forms.Panel();
			this.groupBox1.SuspendLayout();
			this.groupStatus.SuspendLayout();
			this.panelSup.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.panelFinancial.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelPaymentAmt
			// 
			this.labelPaymentAmt.Location = new System.Drawing.Point(132, 131);
			this.labelPaymentAmt.Name = "labelPaymentAmt";
			this.labelPaymentAmt.Size = new System.Drawing.Size(100, 16);
			this.labelPaymentAmt.TabIndex = 1;
			this.labelPaymentAmt.Text = "Payment Amount";
			this.labelPaymentAmt.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(203, 130);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(82, 15);
			this.label3.TabIndex = 2;
			this.label3.Text = "Billing Dentist";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(258, 81);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 16);
			this.label5.TabIndex = 4;
			this.label5.Text = "Ins Plan";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(4, 148);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(80, 16);
			this.label6.TabIndex = 5;
			this.label6.Text = "Date Received";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(32, 128);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(52, 16);
			this.label8.TabIndex = 7;
			this.label8.Text = "DateSent";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelDateService
			// 
			this.labelDateService.Location = new System.Drawing.Point(16, 9);
			this.labelDateService.Name = "labelDateService";
			this.labelDateService.Size = new System.Drawing.Size(82, 16);
			this.labelDateService.TabIndex = 8;
			this.labelDateService.Text = "Date of Service";
			this.labelDateService.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelPreAuthNum
			// 
			this.labelPreAuthNum.Location = new System.Drawing.Point(234, 105);
			this.labelPreAuthNum.Name = "labelPreAuthNum";
			this.labelPreAuthNum.Size = new System.Drawing.Size(100, 16);
			this.labelPreAuthNum.TabIndex = 11;
			this.labelPreAuthNum.Text = "PreAuth Number";
			this.labelPreAuthNum.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(109, 45);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(121, 16);
			this.label13.TabIndex = 12;
			this.label13.Text = "- Deductible Applied";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butViewCheck
			// 
			this.butViewCheck.Location = new System.Drawing.Point(13, 123);
			this.butViewCheck.Name = "butViewCheck";
			this.butViewCheck.Size = new System.Drawing.Size(116, 23);
			this.butViewCheck.TabIndex = 14;
			this.butViewCheck.Text = "View Ins Check";
			this.butViewCheck.Click += new System.EventHandler(this.butViewCheck_Click);
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(6, 40);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(126, 16);
			this.label16.TabIndex = 16;
			this.label16.Text = "Prior Date of Placement";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(343, 480);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(302, 16);
			this.label19.TabIndex = 19;
			this.label19.Text = "Claim Note (this will show on the claim when submitted)";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textPriorDate);
			this.groupBox1.Controls.Add(this.label18);
			this.groupBox1.Controls.Add(this.radioProsthN);
			this.groupBox1.Controls.Add(this.radioProsthR);
			this.groupBox1.Controls.Add(this.radioProsthI);
			this.groupBox1.Controls.Add(this.label16);
			this.groupBox1.Location = new System.Drawing.Point(31, 473);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(254, 88);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Crown, Bridge, or Denture";
			// 
			// textPriorDate
			// 
			this.textPriorDate.Location = new System.Drawing.Point(130, 36);
			this.textPriorDate.Name = "textPriorDate";
			this.textPriorDate.Size = new System.Drawing.Size(66, 20);
			this.textPriorDate.TabIndex = 3;
			this.textPriorDate.Text = "";
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(6, 64);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(246, 18);
			this.label18.TabIndex = 29;
			this.label18.Text = "(Might need a note. Might need to attach x-ray)";
			// 
			// radioProsthN
			// 
			this.radioProsthN.Location = new System.Drawing.Point(12, 18);
			this.radioProsthN.Name = "radioProsthN";
			this.radioProsthN.Size = new System.Drawing.Size(44, 16);
			this.radioProsthN.TabIndex = 0;
			this.radioProsthN.Text = "No";
			this.radioProsthN.Click += new System.EventHandler(this.radioProsthN_Click);
			// 
			// radioProsthR
			// 
			this.radioProsthR.Location = new System.Drawing.Point(132, 18);
			this.radioProsthR.Name = "radioProsthR";
			this.radioProsthR.Size = new System.Drawing.Size(104, 16);
			this.radioProsthR.TabIndex = 2;
			this.radioProsthR.Text = "Replacement";
			this.radioProsthR.Click += new System.EventHandler(this.radioProsthR_Click);
			// 
			// radioProsthI
			// 
			this.radioProsthI.Location = new System.Drawing.Point(64, 18);
			this.radioProsthI.Name = "radioProsthI";
			this.radioProsthI.Size = new System.Drawing.Size(56, 16);
			this.radioProsthI.TabIndex = 1;
			this.radioProsthI.Text = "Initial";
			this.radioProsthI.Click += new System.EventHandler(this.radioProsthI_Click);
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(31, 579);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(232, 44);
			this.label17.TabIndex = 28;
			this.label17.Text = "For bridges, dentures, and partials, missing teeth must have been correctly enter" +
				"ed in the Chart module. ";
			// 
			// groupStatus
			// 
			this.groupStatus.Controls.Add(this.radioStatusH);
			this.groupStatus.Controls.Add(this.radioStatusP);
			this.groupStatus.Controls.Add(this.radioStatusW);
			this.groupStatus.Controls.Add(this.textDateSent);
			this.groupStatus.Controls.Add(this.textDateRec);
			this.groupStatus.Controls.Add(this.radioStatusU);
			this.groupStatus.Controls.Add(this.radioStatusR);
			this.groupStatus.Controls.Add(this.radioStatusS);
			this.groupStatus.Controls.Add(this.label8);
			this.groupStatus.Controls.Add(this.label6);
			this.groupStatus.Location = new System.Drawing.Point(10, 73);
			this.groupStatus.Name = "groupStatus";
			this.groupStatus.Size = new System.Drawing.Size(178, 174);
			this.groupStatus.TabIndex = 0;
			this.groupStatus.TabStop = false;
			this.groupStatus.Text = "Claim Status";
			// 
			// radioStatusH
			// 
			this.radioStatusH.Location = new System.Drawing.Point(4, 34);
			this.radioStatusH.Name = "radioStatusH";
			this.radioStatusH.Size = new System.Drawing.Size(133, 16);
			this.radioStatusH.TabIndex = 1;
			this.radioStatusH.Text = "Hold until Pri received";
			this.radioStatusH.Click += new System.EventHandler(this.radioStatusH_Click);
			// 
			// radioStatusP
			// 
			this.radioStatusP.Location = new System.Drawing.Point(4, 66);
			this.radioStatusP.Name = "radioStatusP";
			this.radioStatusP.Size = new System.Drawing.Size(106, 16);
			this.radioStatusP.TabIndex = 3;
			this.radioStatusP.Text = "Probably sent";
			this.radioStatusP.Click += new System.EventHandler(this.radioStatusP_Click);
			// 
			// radioStatusW
			// 
			this.radioStatusW.Location = new System.Drawing.Point(4, 50);
			this.radioStatusW.Name = "radioStatusW";
			this.radioStatusW.Size = new System.Drawing.Size(134, 16);
			this.radioStatusW.TabIndex = 2;
			this.radioStatusW.Text = "Waiting in Queue";
			this.radioStatusW.Click += new System.EventHandler(this.radioStatusW_Click);
			// 
			// textDateSent
			// 
			this.textDateSent.Location = new System.Drawing.Point(84, 124);
			this.textDateSent.Name = "textDateSent";
			this.textDateSent.Size = new System.Drawing.Size(82, 20);
			this.textDateSent.TabIndex = 6;
			this.textDateSent.Text = "";
			// 
			// textDateRec
			// 
			this.textDateRec.Location = new System.Drawing.Point(84, 144);
			this.textDateRec.Name = "textDateRec";
			this.textDateRec.Size = new System.Drawing.Size(82, 20);
			this.textDateRec.TabIndex = 7;
			this.textDateRec.Text = "";
			// 
			// radioStatusU
			// 
			this.radioStatusU.Location = new System.Drawing.Point(4, 18);
			this.radioStatusU.Name = "radioStatusU";
			this.radioStatusU.Size = new System.Drawing.Size(68, 16);
			this.radioStatusU.TabIndex = 0;
			this.radioStatusU.Text = "Unsent";
			this.radioStatusU.Click += new System.EventHandler(this.radioStatusU_Click);
			// 
			// radioStatusR
			// 
			this.radioStatusR.Location = new System.Drawing.Point(4, 98);
			this.radioStatusR.Name = "radioStatusR";
			this.radioStatusR.Size = new System.Drawing.Size(72, 16);
			this.radioStatusR.TabIndex = 5;
			this.radioStatusR.Text = "Received";
			this.radioStatusR.Click += new System.EventHandler(this.radioStatusR_Click);
			// 
			// radioStatusS
			// 
			this.radioStatusS.Location = new System.Drawing.Point(4, 82);
			this.radioStatusS.Name = "radioStatusS";
			this.radioStatusS.Size = new System.Drawing.Size(100, 16);
			this.radioStatusS.TabIndex = 4;
			this.radioStatusS.Text = "Sent - Verified";
			this.radioStatusS.Click += new System.EventHandler(this.radioStatusS_Click);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(114, 25);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(116, 16);
			this.label7.TabIndex = 24;
			this.label7.Text = "Estimated Payment";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// tbProc
			// 
			this.tbProc.BackColor = System.Drawing.SystemColors.Window;
			this.tbProc.Location = new System.Drawing.Point(4, 252);
			this.tbProc.Name = "tbProc";
			this.tbProc.SelectedIndices = new int[0];
			this.tbProc.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbProc.Size = new System.Drawing.Size(437, 202);
			this.tbProc.TabIndex = 25;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(106, 85);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(122, 16);
			this.label11.TabIndex = 26;
			this.label11.Text = "- Over Annual Max";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(89, 105);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(142, 16);
			this.label14.TabIndex = 27;
			this.label14.Text = "Total Estimated Payment";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listProvBill
			// 
			this.listProvBill.Location = new System.Drawing.Point(204, 148);
			this.listProvBill.Name = "listProvBill";
			this.listProvBill.ScrollAlwaysVisible = true;
			this.listProvBill.Size = new System.Drawing.Size(100, 95);
			this.listProvBill.TabIndex = 2;
			// 
			// textReasonUnder
			// 
			this.textReasonUnder.Location = new System.Drawing.Point(550, 204);
			this.textReasonUnder.MaxLength = 255;
			this.textReasonUnder.Multiline = true;
			this.textReasonUnder.Name = "textReasonUnder";
			this.textReasonUnder.Size = new System.Drawing.Size(342, 39);
			this.textReasonUnder.TabIndex = 7;
			this.textReasonUnder.Text = "";
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(343, 500);
			this.textNote.MaxLength = 255;
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(270, 94);
			this.textNote.TabIndex = 10;
			this.textNote.Text = "";
			// 
			// textInsPayEst
			// 
			this.textInsPayEst.Location = new System.Drawing.Point(235, 104);
			this.textInsPayEst.Name = "textInsPayEst";
			this.textInsPayEst.ReadOnly = true;
			this.textInsPayEst.Size = new System.Drawing.Size(66, 20);
			this.textInsPayEst.TabIndex = 37;
			this.textInsPayEst.Text = "";
			this.textInsPayEst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textInsPayEstSubtotal
			// 
			this.textInsPayEstSubtotal.Location = new System.Drawing.Point(235, 24);
			this.textInsPayEstSubtotal.Name = "textInsPayEstSubtotal";
			this.textInsPayEstSubtotal.ReadOnly = true;
			this.textInsPayEstSubtotal.Size = new System.Drawing.Size(66, 20);
			this.textInsPayEstSubtotal.TabIndex = 40;
			this.textInsPayEstSubtotal.Text = "";
			this.textInsPayEstSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textDateService
			// 
			this.textDateService.Location = new System.Drawing.Point(98, 7);
			this.textDateService.Name = "textDateService";
			this.textDateService.ReadOnly = true;
			this.textDateService.Size = new System.Drawing.Size(76, 20);
			this.textDateService.TabIndex = 41;
			this.textDateService.Text = "";
			// 
			// textPreAuth
			// 
			this.textPreAuth.Location = new System.Drawing.Point(331, 101);
			this.textPreAuth.Name = "textPreAuth";
			this.textPreAuth.Size = new System.Drawing.Size(170, 20);
			this.textPreAuth.TabIndex = 1;
			this.textPreAuth.Text = "";
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(747, 645);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 14;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butDelete
			// 
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(386, 645);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75, 26);
			this.butDelete.TabIndex = 12;
			this.butDelete.Text = "         Delete";
			this.butDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// textClaimFee
			// 
			this.textClaimFee.Location = new System.Drawing.Point(235, 4);
			this.textClaimFee.Name = "textClaimFee";
			this.textClaimFee.ReadOnly = true;
			this.textClaimFee.Size = new System.Drawing.Size(66, 20);
			this.textClaimFee.TabIndex = 51;
			this.textClaimFee.Text = "";
			this.textClaimFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(113, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(116, 16);
			this.label1.TabIndex = 50;
			this.label1.Text = "Claim Total";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butSend
			// 
			this.butSend.Image = ((System.Drawing.Image)(resources.GetObject("butSend.Image")));
			this.butSend.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butSend.Location = new System.Drawing.Point(576, 645);
			this.butSend.Name = "butSend";
			this.butSend.Size = new System.Drawing.Size(117, 26);
			this.butSend.TabIndex = 13;
			this.butSend.Text = "         Send to Queue";
			this.butSend.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butSend.Click += new System.EventHandler(this.butSend_Click);
			// 
			// butPrint
			// 
			this.butPrint.Image = ((System.Drawing.Image)(resources.GetObject("butPrint.Image")));
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(481, 612);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(75, 26);
			this.butPrint.TabIndex = 53;
			this.butPrint.Text = "         Print";
			this.butPrint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Visible = false;
			// 
			// textPlan
			// 
			this.textPlan.Location = new System.Drawing.Point(331, 78);
			this.textPlan.Name = "textPlan";
			this.textPlan.ReadOnly = true;
			this.textPlan.Size = new System.Drawing.Size(232, 20);
			this.textPlan.TabIndex = 1;
			this.textPlan.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(547, 186);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(370, 23);
			this.label4.TabIndex = 55;
			this.label4.Text = "Other reasons underpaid:  (will show on patient bill in a future version)";
			// 
			// textDedApplied
			// 
			this.textDedApplied.Location = new System.Drawing.Point(235, 44);
			this.textDedApplied.Name = "textDedApplied";
			this.textDedApplied.Size = new System.Drawing.Size(66, 20);
			this.textDedApplied.TabIndex = 4;
			this.textDedApplied.Text = "";
			this.textDedApplied.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textDedApplied.TextChanged += new System.EventHandler(this.textDedApplied_TextChanged);
			// 
			// textInsPayAmt
			// 
			this.textInsPayAmt.Location = new System.Drawing.Point(235, 131);
			this.textInsPayAmt.Name = "textInsPayAmt";
			this.textInsPayAmt.Size = new System.Drawing.Size(66, 20);
			this.textInsPayAmt.TabIndex = 6;
			this.textInsPayAmt.Text = "";
			this.textInsPayAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textInsPayAmt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textInsPayAmt_MouseDown);
			this.textInsPayAmt.TextChanged += new System.EventHandler(this.textInsPayAmt_TextChanged);
			// 
			// textOverMax
			// 
			this.textOverMax.Location = new System.Drawing.Point(235, 84);
			this.textOverMax.Name = "textOverMax";
			this.textOverMax.Size = new System.Drawing.Size(66, 20);
			this.textOverMax.TabIndex = 5;
			this.textOverMax.Text = "";
			this.textOverMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textOverMax.TextChanged += new System.EventHandler(this.textOverMax_TextChanged);
			// 
			// labelPat
			// 
			this.labelPat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelPat.Location = new System.Drawing.Point(329, 7);
			this.labelPat.Name = "labelPat";
			this.labelPat.Size = new System.Drawing.Size(216, 20);
			this.labelPat.TabIndex = 59;
			this.labelPat.Text = "Patient Name";
			// 
			// labelPriSec
			// 
			this.labelPriSec.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelPriSec.Location = new System.Drawing.Point(329, 35);
			this.labelPriSec.Name = "labelPriSec";
			this.labelPriSec.Size = new System.Drawing.Size(124, 16);
			this.labelPriSec.TabIndex = 60;
			this.labelPriSec.Text = "Primary or Secondary";
			// 
			// butCreateSec
			// 
			this.butCreateSec.Location = new System.Drawing.Point(454, 30);
			this.butCreateSec.Name = "butCreateSec";
			this.butCreateSec.Size = new System.Drawing.Size(110, 23);
			this.butCreateSec.TabIndex = 61;
			this.butCreateSec.Text = "Create Secondary";
			this.butCreateSec.Click += new System.EventHandler(this.butCreateSec_Click);
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(59, 65);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(173, 16);
			this.label15.TabIndex = 62;
			this.label15.Text = "+ Deductible adjustment factor";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butPayWizard
			// 
			this.butPayWizard.Location = new System.Drawing.Point(153, 154);
			this.butPayWizard.Name = "butPayWizard";
			this.butPayWizard.Size = new System.Drawing.Size(147, 23);
			this.butPayWizard.TabIndex = 64;
			this.butPayWizard.Text = "Payment Entry Wizard";
			this.butPayWizard.Click += new System.EventHandler(this.butPayWizard_Click);
			// 
			// butCancel
			// 
			this.butCancel.Location = new System.Drawing.Point(831, 645);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 15;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textDedAdj
			// 
			this.textDedAdj.Location = new System.Drawing.Point(235, 64);
			this.textDedAdj.Name = "textDedAdj";
			this.textDedAdj.ReadOnly = true;
			this.textDedAdj.Size = new System.Drawing.Size(66, 20);
			this.textDedAdj.TabIndex = 91;
			this.textDedAdj.Text = "";
			this.textDedAdj.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(826, 606);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(88, 29);
			this.label20.TabIndex = 92;
			this.label20.Text = "(does not cancel payment edits)";
			// 
			// listProvTreat
			// 
			this.listProvTreat.Location = new System.Drawing.Point(310, 148);
			this.listProvTreat.Name = "listProvTreat";
			this.listProvTreat.ScrollAlwaysVisible = true;
			this.listProvTreat.Size = new System.Drawing.Size(100, 95);
			this.listProvTreat.TabIndex = 3;
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(309, 130);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(95, 15);
			this.label21.TabIndex = 93;
			this.label21.Text = "Treating Dentist";
			// 
			// butRefer
			// 
			this.butRefer.Location = new System.Drawing.Point(4, 23);
			this.butRefer.Name = "butRefer";
			this.butRefer.Size = new System.Drawing.Size(109, 24);
			this.butRefer.TabIndex = 95;
			this.butRefer.Text = "Referring Provider";
			this.butRefer.Click += new System.EventHandler(this.butRefer_Click);
			// 
			// comboPlaceService
			// 
			this.comboPlaceService.BackColor = System.Drawing.SystemColors.Window;
			this.comboPlaceService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPlaceService.Items.AddRange(new object[] {
																													 "Office",
																													 "Patient\'s Home",
																													 "Inpatient Hospital",
																													 "Outpatient Hospital",
																													 "Skilled Nursing Facility",
																													 "Adult Living Care Facility",
																													 "Other Location"});
			this.comboPlaceService.Location = new System.Drawing.Point(4, 68);
			this.comboPlaceService.Name = "comboPlaceService";
			this.comboPlaceService.Size = new System.Drawing.Size(128, 21);
			this.comboPlaceService.TabIndex = 0;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(3, 52);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(100, 17);
			this.label10.TabIndex = 97;
			this.label10.Text = "Place of Service";
			// 
			// panelSup
			// 
			this.panelSup.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panelSup.Controls.Add(this.listEmploy);
			this.panelSup.Controls.Add(this.label25);
			this.panelSup.Controls.Add(this.groupBox2);
			this.panelSup.Controls.Add(this.butSupplemental);
			this.panelSup.Controls.Add(this.label22);
			this.panelSup.Controls.Add(this.comboPlaceService);
			this.panelSup.Controls.Add(this.label10);
			this.panelSup.Controls.Add(this.butRefer);
			this.panelSup.Location = new System.Drawing.Point(608, 254);
			this.panelSup.Name = "panelSup";
			this.panelSup.Size = new System.Drawing.Size(279, 193);
			this.panelSup.TabIndex = 8;
			// 
			// listEmploy
			// 
			this.listEmploy.Items.AddRange(new object[] {
																										"Unknown",
																										"Yes",
																										"No"});
			this.listEmploy.Location = new System.Drawing.Point(165, 42);
			this.listEmploy.Name = "listEmploy";
			this.listEmploy.Size = new System.Drawing.Size(69, 43);
			this.listEmploy.TabIndex = 1;
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(158, 26);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(114, 17);
			this.label25.TabIndex = 101;
			this.label25.Text = "Employment Related";
			this.label25.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label24);
			this.groupBox2.Controls.Add(this.textAccidentST);
			this.groupBox2.Controls.Add(this.textAccidentDate);
			this.groupBox2.Controls.Add(this.label23);
			this.groupBox2.Controls.Add(this.listAccident);
			this.groupBox2.Location = new System.Drawing.Point(11, 98);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(209, 85);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Accident Related";
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(90, 47);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(32, 17);
			this.label24.TabIndex = 8;
			this.label24.Text = "State";
			this.label24.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textAccidentST
			// 
			this.textAccidentST.Location = new System.Drawing.Point(124, 43);
			this.textAccidentST.Name = "textAccidentST";
			this.textAccidentST.Size = new System.Drawing.Size(30, 20);
			this.textAccidentST.TabIndex = 2;
			this.textAccidentST.Text = "";
			// 
			// textAccidentDate
			// 
			this.textAccidentDate.Location = new System.Drawing.Point(124, 20);
			this.textAccidentDate.Name = "textAccidentDate";
			this.textAccidentDate.Size = new System.Drawing.Size(75, 20);
			this.textAccidentDate.TabIndex = 1;
			this.textAccidentDate.Text = "";
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(89, 24);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(32, 17);
			this.label23.TabIndex = 5;
			this.label23.Text = "Date";
			this.label23.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listAccident
			// 
			this.listAccident.Items.AddRange(new object[] {
																											"No",
																											"Auto",
																											"Employment",
																											"Other"});
			this.listAccident.Location = new System.Drawing.Point(8, 19);
			this.listAccident.Name = "listAccident";
			this.listAccident.Size = new System.Drawing.Size(69, 56);
			this.listAccident.TabIndex = 0;
			// 
			// butSupplemental
			// 
			this.butSupplemental.Location = new System.Drawing.Point(117, 0);
			this.butSupplemental.Name = "butSupplemental";
			this.butSupplemental.Size = new System.Drawing.Size(18, 22);
			this.butSupplemental.TabIndex = 99;
			this.butSupplemental.Text = "V";
			this.butSupplemental.Click += new System.EventHandler(this.butSupplemental_Click);
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(2, 3);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(108, 15);
			this.label22.TabIndex = 98;
			this.label22.Text = "(supplemental info)";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.textOrthoDate);
			this.groupBox4.Controls.Add(this.label27);
			this.groupBox4.Controls.Add(this.textOrthoRemainM);
			this.groupBox4.Controls.Add(this.checkIsOrtho);
			this.groupBox4.Controls.Add(this.label26);
			this.groupBox4.Location = new System.Drawing.Point(675, 479);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(177, 88);
			this.groupBox4.TabIndex = 11;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Ortho";
			// 
			// textOrthoDate
			// 
			this.textOrthoDate.Location = new System.Drawing.Point(106, 36);
			this.textOrthoDate.Name = "textOrthoDate";
			this.textOrthoDate.Size = new System.Drawing.Size(66, 20);
			this.textOrthoDate.TabIndex = 1;
			this.textOrthoDate.Text = "";
			// 
			// label27
			// 
			this.label27.Location = new System.Drawing.Point(7, 40);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(100, 16);
			this.label27.TabIndex = 104;
			this.label27.Text = "Date of Placement";
			this.label27.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textOrthoRemainM
			// 
			this.textOrthoRemainM.Location = new System.Drawing.Point(107, 59);
			this.textOrthoRemainM.Name = "textOrthoRemainM";
			this.textOrthoRemainM.Size = new System.Drawing.Size(39, 20);
			this.textOrthoRemainM.TabIndex = 2;
			this.textOrthoRemainM.Text = "";
			// 
			// checkIsOrtho
			// 
			this.checkIsOrtho.Location = new System.Drawing.Point(12, 16);
			this.checkIsOrtho.Name = "checkIsOrtho";
			this.checkIsOrtho.Size = new System.Drawing.Size(90, 18);
			this.checkIsOrtho.TabIndex = 0;
			this.checkIsOrtho.Text = "Is For Ortho";
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(9, 61);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(100, 18);
			this.label26.TabIndex = 102;
			this.label26.Text = "Months Remaining";
			// 
			// panelFinancial
			// 
			this.panelFinancial.Controls.Add(this.textClaimFee);
			this.panelFinancial.Controls.Add(this.textDedAdj);
			this.panelFinancial.Controls.Add(this.label7);
			this.panelFinancial.Controls.Add(this.label1);
			this.panelFinancial.Controls.Add(this.label11);
			this.panelFinancial.Controls.Add(this.label14);
			this.panelFinancial.Controls.Add(this.label13);
			this.panelFinancial.Controls.Add(this.textDedApplied);
			this.panelFinancial.Controls.Add(this.textInsPayEst);
			this.panelFinancial.Controls.Add(this.textInsPayAmt);
			this.panelFinancial.Controls.Add(this.labelPaymentAmt);
			this.panelFinancial.Controls.Add(this.textInsPayEstSubtotal);
			this.panelFinancial.Controls.Add(this.textOverMax);
			this.panelFinancial.Controls.Add(this.butViewCheck);
			this.panelFinancial.Controls.Add(this.label15);
			this.panelFinancial.Controls.Add(this.butPayWizard);
			this.panelFinancial.Location = new System.Drawing.Point(591, -1);
			this.panelFinancial.Name = "panelFinancial";
			this.panelFinancial.Size = new System.Drawing.Size(311, 179);
			this.panelFinancial.TabIndex = 94;
			// 
			// FormClaimEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(920, 684);
			this.ControlBox = false;
			this.Controls.Add(this.panelFinancial);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.panelSup);
			this.Controls.Add(this.listProvTreat);
			this.Controls.Add(this.label21);
			this.Controls.Add(this.label20);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butCreateSec);
			this.Controls.Add(this.labelPriSec);
			this.Controls.Add(this.labelPat);
			this.Controls.Add(this.textReasonUnder);
			this.Controls.Add(this.textPlan);
			this.Controls.Add(this.textPreAuth);
			this.Controls.Add(this.textDateService);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.butSend);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.listProvBill);
			this.Controls.Add(this.tbProc);
			this.Controls.Add(this.groupStatus);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.labelPreAuthNum);
			this.Controls.Add(this.labelDateService);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label17);
			this.Name = "FormClaimEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Claim";
			this.Load += new System.EventHandler(this.FormClaimEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupStatus.ResumeLayout(false);
			this.panelSup.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.panelFinancial.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormClaimEdit_Load(object sender, System.EventArgs e) {
			if(IsNew){
				butPayWizard.Enabled=false;
			}
			else{
				if(Claims.Cur.ClaimStatus=="S"){
					if(!UserPermissions.CheckUserPassword("Claims Sent Edit",Claims.Cur.DateSent)){
						//MessageBox.Show(Lan.g(this,"You only have permission to view the Claim. No changes will be saved."));
						butOK.Enabled=false;
						butDelete.Enabled=false;
						butPrint.Enabled=false;
						butRefer.Enabled=false;
						butSend.Enabled=false;
						butSupplemental.Enabled=false;
						butViewCheck.Enabled=false;
						butCreateSec.Enabled=false;
						butPayWizard.Enabled=false;
						groupStatus.Enabled=false;
					}		
				}	
			}
			if(Claims.Cur.ClaimType=="PreAuth"){
				Text=Lan.g(this,"Edit PreAuthorization");
        butCreateSec.Visible=false;
				panelSup.Visible=false;
				panelFinancial.Visible=false;
				labelPreAuthNum.Visible=false;
				textPreAuth.Visible=false;
				labelPriSec.Visible=false;
				textDateService.Visible=false;
				labelDateService.Visible=false;
				label20.Visible=false;//warning when delete
				textReasonUnder.Visible=false;
				label4.Visible=false;//reason under
      }
			panelSup.Height=26;
			panelSup.Width=139;
      Claims.Refresh(); 
      ClaimProcs.Refresh();
			FillForm();			
		}

		public void FillForm(){
			if(Claims.Cur.ClaimPaymentNum>0){
				textInsPayAmt.ReadOnly=true;
			}
			else{
				textInsPayAmt.ReadOnly=false;
			}
			if(Claims.Cur.ClaimNum==Claims.Cur.PriClaimNum){
				IsPrimary=true;
			}
			labelPat.Text=Patients.GetCurNameLF();
			textDateService.Text=Claims.Cur.DateService.ToString("d");
			if(DateTime.Compare(Claims.Cur.DateSent,new DateTime(1860,1,1))<0)
				textDateSent.Text="";
			else
				textDateSent.Text=Claims.Cur.DateSent.ToString("d");
			switch(Claims.Cur.ClaimStatus){
				case "U"://unsent
					radioStatusU.Checked=true;
					break;
				case "H"://hold until pri received
					radioStatusH.Checked=true;
					break;
				case "W"://waiting to be sent
					radioStatusW.Checked=true;
					break;
				case "P"://probably sent
					radioStatusP.Checked=true;
					break;
				case "S"://sent-verified
					radioStatusS.Checked=true;
					break;
				case "R"://received
					radioStatusR.Checked=true;
					break;
			}
			if(DateTime.Compare(Claims.Cur.DateReceived,new DateTime(1860,1,1))<0)
				textDateRec.Text="";
			else
				textDateRec.Text=Claims.Cur.DateReceived.ToString("d");
			textPlan.Text=InsPlans.GetCarrierInFam(Claims.Cur.PlanNum);
			for(int i=0;i<Providers.List.Length;i++){
				listProvTreat.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==Claims.Cur.ProvTreat)
					listProvTreat.SelectedIndex=i;
			}
			if(listProvTreat.Items.Count>0 && listProvTreat.SelectedIndex==-1)
				listProvTreat.SelectedIndex=0;
			for(int i=0;i<Providers.List.Length;i++){
				listProvBill.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==Claims.Cur.ProvBill)
					listProvBill.SelectedIndex=i;
			}
			if(listProvBill.Items.Count>0 && listProvBill.SelectedIndex==-1)
				listProvBill.SelectedIndex=0;
			FormCRef.ReferringProv=Claims.Cur.ReferringProv;
			FormCRef.RefNumString=Claims.Cur.RefNumString;
			//these values are computed every time the window is opened in case procedure overrides change.
			//fees not allowed to change, because that is what is submitted.
			//If cancel, overrides do not affect claim estimates.
			FillTable();//(procedures)
      textClaimFee.Text=ClaimFee.ToString("F");
			if(IsPrimary){
				textInsPayEstSubtotal.Text=PriInsPayEstSubtotal.ToString("F");
			}
			else{
				textInsPayEstSubtotal.Text=SecInsPayEstSubtotal.ToString("F");
			}
			textDedApplied.Text=Claims.Cur.DedApplied.ToString("F");
			textOverMax.Text=Claims.Cur.OverMax.ToString("F");
			textInsPayAmt.Text=Claims.Cur.InsPayAmt.ToString("F");
			ComputeTotals();//fills in textDedAdj,textInsPayEst, etc
			if(Claims.Cur.ClaimPaymentNum>0)
				butViewCheck.Text="View Ins Check";
			else butViewCheck.Text="Create Ins Check";
			textPreAuth.Text=Claims.Cur.PreAuthString;
			switch(Claims.Cur.IsProsthesis){
				case "N"://no
					radioProsthN.Checked=true;
					break;
				case "I"://initial
					radioProsthI.Checked=true;
					break;
				case "R"://replacement
					radioProsthR.Checked=true;
					break;
			}
			if(DateTime.Compare(Claims.Cur.PriorDate,new DateTime(1860,1,1))<0)
				textPriorDate.Text="";
			else
				textPriorDate.Text=Claims.Cur.PriorDate.ToString("d");
			textReasonUnder.Text=Claims.Cur.ReasonUnderPaid;
			textNote.Text=Claims.Cur.ClaimNote;
			comboPlaceService.SelectedIndex=(int)Claims.Cur.PlaceService;
			switch(Claims.Cur.AccidentRelated){
				case "":
					listAccident.SelectedIndex=0;
					break;
				case "A":
					listAccident.SelectedIndex=1;
					break;
				case "E":
					listAccident.SelectedIndex=2;
					break;
				case "O":
					listAccident.SelectedIndex=3;
					break;
			}
			if(Claims.Cur.AccidentDate.CompareTo(new DateTime(1860,1,1))>0)
				textAccidentDate.Text=Claims.Cur.AccidentDate.ToString("d");
			textAccidentST.Text=Claims.Cur.AccidentST;
			switch(Claims.Cur.EmployRelated){
				case YN.Unknown:
					listEmploy.SelectedIndex=0;
					break;
				case YN.Yes:
					listEmploy.SelectedIndex=1;
					break;
				case YN.No:
					listEmploy.SelectedIndex=2;
					break;
			}
			checkIsOrtho.Checked=Claims.Cur.IsOrtho;
			textOrthoRemainM.Text=Claims.Cur.OrthoRemainM.ToString();
			if(Claims.Cur.OrthoDate.CompareTo(new DateTime(1860,1,1))>0)
				textOrthoDate.Text=Claims.Cur.OrthoDate.ToString("d");
			SetSecControls();
			if(IsNew && !IsPrimary){
				UpdateClaim();
				//dialog never even opens if auto secondary
			}
		}

		private void ComputeTotals(){//must have already run FillTable()
			if(textDedApplied.Text=="")
				Claims.Cur.DedApplied=0;
			else{
				try{
					Claims.Cur.DedApplied=PIn.PDouble(textDedApplied.Text);
				}
				catch{
					MessageBox.Show(Lan.g(this,"Invalid character"));
					return;
				}
			}
			if(textOverMax.Text=="")
				Claims.Cur.OverMax=0;
			else{
				try{
					Claims.Cur.OverMax=PIn.PDouble(textOverMax.Text);
				}
				catch{
					MessageBox.Show(Lan.g(this,"Invalid character"));
					return;
				}
			}
			double qualifiesD=0;
			DedAdjPerc=0;//typical .2, but sometimes 0
			double procPercent; 
      if(Claims.Cur.ClaimType=="PreAuth"){//calculations irrelevant for preauths.
				//Compute Deductible, specifically qualifiesD and DedAdjPerc.
        /*for(int i=0;i<ClaimProcs.ForClaim.Count;i++){//this was filled earlier in FillTable()
          Procedures.Cur=(Procedure)Procedures.HList[((ClaimProc)ClaimProcs.ForClaim[i]).ProcNum];
					PriSecTot pst;
					if(IsPrimary) pst=PriSecTot.Pri;
					else pst=PriSecTot.Sec;
					if(CovCats.GetIsPrev(Procedures.Cur.ADACode)){//prev
						if(((InsPlan)InsPlans.HList[Claims.Cur.PlanNum]).DeductWaivPrev==YN.No){//but ded not waived
							qualifiesD+=Procedures.Cur.ProcFee;
							procPercent=CovPats.GetPercent(Procedures.Cur.ADACode,pst)*100;//eg 80
							if(procPercent < 100-DedAdjPerc){
								DedAdjPerc=1-procPercent/100;
							}
						}
					}
					else{//not prev
						qualifiesD+=Procedures.Cur.ProcFee;
						procPercent=CovPats.GetPercent(Procedures.Cur.ADACode,pst)*100;//eg 80
						if(procPercent < 100-DedAdjPerc){
							DedAdjPerc=1-procPercent/100;
						}
					}
				}//end for procs*/
			}
			else{
				//Compute Deductible, specifically qualifiesD and DedAdjPerc.
				for(int i=0;i<Claims.ProcsInClaim.Count;i++){//ProcsInClaim was filled earlier in FillTable()
					PriSecTot pst;
					if(IsPrimary) pst=PriSecTot.Pri;
					else pst=PriSecTot.Sec;
					if(CovCats.GetIsPrev(((Procedure)Claims.ProcsInClaim[i]).ADACode)){//prev
						if(((InsPlan)InsPlans.HList[Claims.Cur.PlanNum]).DeductWaivPrev==YN.No){//but ded not waived
							qualifiesD+=((Procedure)Claims.ProcsInClaim[i]).ProcFee;
							procPercent=CovPats.GetPercent(((Procedure)Claims.ProcsInClaim[i]).ADACode,pst)*100;//eg 80
							if(procPercent < 100-DedAdjPerc){
								DedAdjPerc=1-procPercent/100;
							}
						}
					}
					else{//not prev
						qualifiesD+=((Procedure)Claims.ProcsInClaim[i]).ProcFee;
						procPercent=CovPats.GetPercent(((Procedure)Claims.ProcsInClaim[i]).ADACode,pst)*100;//eg 80
						if(procPercent < 100-DedAdjPerc){
							DedAdjPerc=1-procPercent/100;
						}
					}
				}//end for procs
      }
			//Compute DedAdj
			if(IsNew){
				double dedRem=InsPlans.GetDedRem(Claims.Cur.DateService,Claims.Cur.PlanNum);
				if(qualifiesD < dedRem)
					Claims.Cur.DedApplied=qualifiesD;
				else
					Claims.Cur.DedApplied=dedRem;
			}
			double dedAdj=Claims.Cur.DedApplied*DedAdjPerc;
			if(IsNew && !IsPrimary){
				if(Claims.Cur.DedApplied > SecInsPayEstSubtotal){
					Claims.Cur.DedApplied=SecInsPayEstSubtotal;
					dedAdj=0;
				}
			}
			if(!IsPrimary){
				if(Claims.Cur.DedApplied==SecInsPayEstSubtotal){
					dedAdj=0;
				}
			}
			textDedAdj.Text=dedAdj.ToString("F");
			//Compute overMax:
			PriInsPayEst=PriInsPayEstSubtotal-Claims.Cur.DedApplied+dedAdj;//-Claims.Cur.OverMax;
			SecInsPayEst=SecInsPayEstSubtotal-Claims.Cur.DedApplied+dedAdj;//-Claims.Cur.OverMax;
			if(PriInsPayEst < 0) PriInsPayEst=0;
			if(SecInsPayEst < 0) SecInsPayEst=0;
			//if(IsNew){
			double insRem=InsPlans.GetInsRem(Claims.Cur.DateService,Claims.Cur.PlanNum,Claims.Cur.ClaimNum);
			double overMax;
			if(IsPrimary){
				if(insRem > PriInsPayEst)	overMax=0;//enough ins to cover
				else overMax=PriInsPayEst-insRem;//ran out of ins
			}
			else{//sec
				if(insRem > SecInsPayEst)	overMax=0;
				else overMax=SecInsPayEst-insRem;
			}
			Claims.Cur.OverMax=overMax;
			//}
//might need to run extensive test to determine what happens if PriInsPayEst < 0 
			if(IsPrimary){
				PriInsPayEst-=Claims.Cur.OverMax;
				textInsPayEst.Text=PriInsPayEst.ToString("F");
			}
			else{
				SecInsPayEst-=Claims.Cur.OverMax;
				textInsPayEst.Text=SecInsPayEst.ToString("F");
			}
			textDedApplied.Text=Claims.Cur.DedApplied.ToString("F");
			textOverMax.Text=Claims.Cur.OverMax.ToString("F");
			textInsPayAmt.Text=Claims.Cur.InsPayAmt.ToString("F");
		}//end ComputeTotals

		private void SetSecControls(){
			if(IsPrimary){//primary claim
				labelPriSec.Text="Primary Claim";
				if(IsNew){
					butCreateSec.Enabled=false;
				}
				else if(Claims.Cur.SecClaimNum==0
				//else if(((Claim)Claims.HList[Claims.Cur.SecClaimNum]).ClaimNum==0//no secondary claim exists
					&& Patients.Cur.SecPlanNum!=0){//and has secondary coverage
					butCreateSec.Enabled=true;
				}
				else{//secondary claim exists or no secondary coverage
					butCreateSec.Enabled=false;
				}
			}
			else{//secondary claim
				labelPriSec.Text="Secondary Claim";
				butCreateSec.Visible=false;
			}
		}

		private void FillTable(){
			ClaimFee=0;
			double priInsEst;
			double secInsEst;
			PriInsPayEstSubtotal=0;
			SecInsPayEstSubtotal=0;
      if(Claims.Cur.ClaimType=="PreAuth"){
        IsPrimary=true;//this might be irrelevant.
        ClaimProcs.GetForClaim();
				tbProc.ResetRows(ClaimProcs.ForClaim.Count);
				for(int i=0;i<ClaimProcs.ForClaim.Count;i++){
          Procedures.Cur=(Procedure)Procedures.HList[((ClaimProc)ClaimProcs.ForClaim[i]).ProcNum];
					tbProc.Cell[0,i]=Procedures.Cur.ADACode;
					tbProc.Cell[1,i]=Procedures.Cur.ToothNum;
					tbProc.Cell[2,i]=ProcCodes.GetProcCode(Procedures.Cur.ADACode).Descript;
					double fee=Procedures.Cur.ProcFee;
					priInsEst=Procedures.GetEstForCur(PriSecTot.Pri);
					secInsEst=Procedures.GetEstForCur(PriSecTot.Sec);
					ClaimFee+=fee;
					PriInsPayEstSubtotal+=priInsEst;
					SecInsPayEstSubtotal+=secInsEst;
					tbProc.Cell[3,i]=fee.ToString("F");
					if(IsPrimary){
						tbProc.Cell[4,i]=priInsEst.ToString("F");
					}
					else{
						tbProc.Cell[4,i]=secInsEst.ToString("F");		
					}			
					tbProc.Cell[5,i]=(fee-priInsEst-secInsEst).ToString("F");
				}
				tbProc.SetGridColor(Color.LightGray);
				tbProc.LayoutTables();        
      }
      else{
				if(IsPrimary){
					Claims.GetProcsInClaim(Claims.Cur.ClaimNum);
				}
				else{
					Claims.GetProcsInClaim(Claims.Cur.PriClaimNum);
				}
				DedAdjPerc=0;
				tbProc.ResetRows(Claims.ProcsInClaim.Count);
				for(int i=0;i<Claims.ProcsInClaim.Count;i++){
					tbProc.Cell[0,i]=((Procedure)Claims.ProcsInClaim[i]).ADACode;
					tbProc.Cell[1,i]=((Procedure)Claims.ProcsInClaim[i]).ToothNum;
					tbProc.Cell[2,i]=ProcCodes.GetProcCode(((Procedure)Claims.ProcsInClaim[i]).ADACode).Descript;
					double fee=((Procedure)Claims.ProcsInClaim[i]).ProcFee;
					Procedures.Cur=(Procedure)Claims.ProcsInClaim[i];
					priInsEst=Procedures.GetEstForCur(PriSecTot.Pri);
					secInsEst=Procedures.GetEstForCur(PriSecTot.Sec);
					ClaimFee+=fee;
					PriInsPayEstSubtotal+=priInsEst;
					SecInsPayEstSubtotal+=secInsEst;
					tbProc.Cell[3,i]=fee.ToString("F");
					if(IsPrimary){
						tbProc.Cell[4,i]=priInsEst.ToString("F");
					}
					else{
						tbProc.Cell[4,i]=secInsEst.ToString("F");		
					}			
					tbProc.Cell[5,i]=(fee-priInsEst-secInsEst).ToString("F");
				}
				tbProc.SetGridColor(Color.LightGray);
				tbProc.LayoutTables();
      }
		}

		private void textDedApplied_TextChanged(object sender, System.EventArgs e) {
			ComputeTotals();
		}

		private void textOverMax_TextChanged(object sender, System.EventArgs e) {
			ComputeTotals();
		}

		private void textInsPayAmt_TextChanged(object sender, System.EventArgs e) {
			//not needed?  computeTotals();
		}

		private void tbProc_CellDoubleClicked(object sender, CellEventArgs e){
			if(Claims.Cur.ClaimType=="PreAuth"){
				Procedures.Cur=(Procedure)Procedures.HList[((ClaimProc)ClaimProcs.ForClaim[e.Row]).ProcNum];
			}
			else{
				Procedures.Cur=(Procedure)Claims.ProcsInClaim[e.Row];
			}
			FormProcEdit FormPE = new FormProcEdit();
			FormPE.IsNew=false;
			FormPE.ShowDialog();
			if(FormPE.DialogResult==DialogResult.OK){
				Procedures.Refresh();
				FillTable();
			}
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
      if(Claims.Cur.ClaimType=="PreAuth"){
				if(MessageBox.Show(Lan.g(this,"Delete PreAuthorization?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;
				}
        for(int i=0;i<ClaimProcs.ForClaim.Count;i++){
          ClaimProcs.Cur=(ClaimProc)ClaimProcs.ForClaim[i];
          ClaimProcs.DeleteCur();
        }
        Claims.DeleteCur();
        DialogResult=DialogResult.OK;
        return;
      }
			if(IsPrimary){
				if(Claims.Cur.SecClaimNum!=0){//if secondary exists.
					MessageBox.Show(Lan.g(this,"Can not delete primary claim until secondary is deleted."));
					return;
				}
			}
			if(Claims.Cur.ClaimPaymentNum>0){
				MessageBox.Show(Lan.g(this,"Can not delete claim while insurance check is attached. "
					+"You will have to detach this claim from the check first."));
				return;
			}
			if(Claims.Cur.ClaimStatus=="R"){
				MessageBox.Show(Lan.g(this,"Can not delete claim while status is Received.  You will have to change the status first."));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete Claim?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			if(!IsPrimary){//is secondary
				int secClaimNum=Claims.Cur.ClaimNum;
				Claims.Cur=(Claim)Claims.HList[Claims.Cur.PriClaimNum];
				Claims.Cur.SecClaimNum=0;
				Claims.UpdateCur();
				Claims.Cur=(Claim)Claims.HList[secClaimNum];
			}
			Claims.DetachProcsFromClaim();
			//Claims.PutBalIns(Claims.Cur.DateService,-OriginalEst,-OriginalPay);
			Claims.DeleteCur();
			DialogResult=DialogResult.OK;
		}

		private void butViewCheck_Click(object sender, System.EventArgs e) {
			//+" WHERE claimpaymentnum = '"+checkNum+"'";
			//if(showUnattached){
			//	cmd.CommandText+=" || (inspayamt != 0 AND claimpaymentnum = '0')";
			if(!ClaimIsValid())
				return;
			if(PIn.PDouble(textInsPayAmt.Text)==0 && Claims.Cur.ClaimPaymentNum==0){
				MessageBox.Show(Lan.g(this,"You must enter a payment amount before you can create a check."));
				return;																																						
			}
			UpdateClaim();
			int tempClaimNum=Claims.Cur.ClaimNum;
			FormClaimPayEdit FormE=new FormClaimPayEdit();
			if(Claims.Cur.ClaimPaymentNum==0){//not attached
				FormE.IsNew=true;
			}
			else{//attached to check
				FormE.IsNew=false;
				ClaimPayments.GetCheck(Claims.Cur.ClaimPaymentNum);
				if(ClaimPayments.Cur.ClaimPaymentNum==0){//check not found.  Handles corruption smoothly.
					Claims.Cur.ClaimPaymentNum=0;
					UpdateClaim();
					FormE.IsNew=true;
				}
			}
			FormE.ShowDialog();
			Claims.Refresh();
			//MessageBox.Show(tempClaimNum.ToString());
			//MessageBox.Show(Claims.HList.Count.ToString());
			Claims.Cur=((Claim)Claims.HList[tempClaimNum]);
			if(Claims.Cur.ClaimPaymentNum>0){
				butViewCheck.Text="View Ins Check";
				textInsPayAmt.ReadOnly=true;
			}
			else{
				butViewCheck.Text="Create Ins Check";
				textInsPayAmt.ReadOnly=false;
			}
		}

		private void butPayWizard_Click(object sender, System.EventArgs e) {
			if(Claims.Cur.ClaimPaymentNum>0){
				MessageBox.Show(Lan.g(this,"You can not edit the payment information without first detaching this claim from the insurance check."));
				return;
			}
			if(!ClaimIsValid())
				return;
			UpdateClaim();
			FormClaimPayWizard FormW=new FormClaimPayWizard();
			FormW.ShowDialog();
			if(FormW.DialogResult!=DialogResult.OK){
				return;
			}
			FillForm();
			//UpdateClaim();
		}

		private void butCreateSec_Click(object sender, System.EventArgs e) {
			//the only time this is used is if secondary coverage is entered after the primary claim has been created.
			//otherwise, both claims are always created at the same time in Account module.
			if(!ClaimIsValid())
				return;
			UpdateClaim();
			CreateSecondary();
			FormClaimEdit FormCE=new FormClaimEdit();
			FormCE.IsNew=true;//made this true to trigger calc of ded and overmax
			FormCE.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		private void CreateSecondary(){
			//using Claims.Cur as a template
			//fields not changed below are exactly the same in pri and sec claims:
			Claims.Cur.DateSent=DateTime.MinValue;
			Claims.Cur.ClaimStatus="H";
			Claims.Cur.DateReceived=DateTime.MinValue;
			Claims.Cur.PlanNum=Patients.Cur.SecPlanNum;
			Claims.Cur.InsPayEst=SecInsPayEst;
			Claims.Cur.InsPayAmt=0;
			Claims.Cur.ClaimPaymentNum=0;
			Claims.Cur.DedApplied=0;
			Claims.Cur.OverMax=0;
			Claims.Cur.PreAuthString="";
			Claims.Cur.ReasonUnderPaid="";
			Claims.Cur.PriClaimNum=Claims.Cur.ClaimNum;
			Claims.PutBalIns(Claims.Cur.DateService,Claims.Cur.InsPayEst,0);
			//OriginalEst does not need to be reset because new window will open to edit secondary claim
			//and this window will close.
			Claims.InsertCur();//now, ClaimNum and PriClaimNum are different.
			Claims.Cur.SecClaimNum=Claims.Cur.ClaimNum;
			Claims.UpdateCur();
			Claims.Refresh();
			//now fix secclaimnum for priclaim
			int SecClaimNum=Claims.Cur.ClaimNum;
			Claims.Cur=(Claim)Claims.HList[Claims.Cur.PriClaimNum];
			Claims.Cur.SecClaimNum=SecClaimNum;
			Claims.UpdateCur();
			Claims.Cur=(Claim)Claims.HList[Claims.Cur.SecClaimNum];
		}

		private void radioStatusU_Click(object sender, System.EventArgs e) {
			Claims.Cur.ClaimStatus="U";
			SetSecControls();
		}

		private void radioStatusH_Click(object sender, System.EventArgs e) {
			Claims.Cur.ClaimStatus="H";
			SetSecControls();
		}

		private void radioStatusW_Click(object sender, System.EventArgs e) {
			Claims.Cur.ClaimStatus="W";
			SetSecControls();
		}

		private void radioStatusP_Click(object sender, System.EventArgs e) {
			Claims.Cur.ClaimStatus="P";
			SetSecControls();
		}

		private void radioStatusS_Click(object sender, System.EventArgs e) {
			Claims.Cur.ClaimStatus="S";
			SetSecControls();
		}

		private void radioStatusR_Click(object sender, System.EventArgs e) {
			Claims.Cur.ClaimStatus="R";
			SetSecControls();
		}

		private void radioProsthN_Click(object sender, System.EventArgs e) {
			Claims.Cur.IsProsthesis="N";
		}

		private void radioProsthI_Click(object sender, System.EventArgs e) {
			Claims.Cur.IsProsthesis="I";
		}

		private void radioProsthR_Click(object sender, System.EventArgs e) {
			Claims.Cur.IsProsthesis="R";
		}

		private void textInsPayAmt_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(Claims.Cur.ClaimPaymentNum>0){
				MessageBox.Show(Lan.g(this,"You can not edit payment amount without deleting the insurance check first."));
				//textInsPayAmt.
			}
		}

		private void butSupplemental_Click(object sender, System.EventArgs e) {
			if(panelSup.Height==26){
				panelSup.Height=189;
				panelSup.Width=282;
			}
			else{
				panelSup.Height=26;
				panelSup.Width=139;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(!ClaimIsValid())
				return;
			UpdateClaim();
			if(IsNew && Patients.Cur.SecPlanNum!=0 && IsPrimary && Claims.Cur.ClaimType!="PreAuth"){
				CreateSecondary();
				FormClaimEdit FormCE=new FormClaimEdit();
				FormCE.IsNew=true;//made this true to trigger calc of ded and overmax
				FormCE.FillForm();
			}
			DialogResult=DialogResult.OK;
		}
		
		private bool ClaimIsValid(){
			if(  textDateSent.errorProvider1.GetError(textDateSent)!=""
				|| textDateRec.errorProvider1.GetError(textDateRec)!=""
				|| textPriorDate.errorProvider1.GetError(textPriorDate)!=""
				|| textDedApplied.errorProvider1.GetError(textDedApplied)!=""
				|| textOverMax.errorProvider1.GetError(textOverMax)!=""
				|| textInsPayAmt.errorProvider1.GetError(textInsPayAmt)!=""
				|| textAccidentDate.errorProvider1.GetError(textAccidentDate)!=""
				|| textOrthoDate.errorProvider1.GetError(textOrthoDate)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return false;
			}
			else
				return true;
		}

		private void UpdateClaim(){
			//patnum
			//dateservice
			if(textDateSent.Text=="")
				Claims.Cur.DateSent=DateTime.MinValue;
			else Claims.Cur.DateSent=PIn.PDate(textDateSent.Text);
			//claimstatus handled previously
			if(textDateRec.Text=="")
				Claims.Cur.DateReceived=DateTime.MinValue;
			else Claims.Cur.DateReceived=PIn.PDate(textDateRec.Text);
			//planNum
			if(listProvTreat.SelectedIndex!=-1)
				Claims.Cur.ProvTreat=Providers.List[listProvTreat.SelectedIndex].ProvNum;
			Claims.Cur.ClaimFee=ClaimFee;
			if(IsPrimary)
				Claims.Cur.InsPayEst=PriInsPayEst;
			else
				Claims.Cur.InsPayEst=SecInsPayEst;
			if(textInsPayAmt.Text=="")
				Claims.Cur.InsPayAmt=0;
			else
				Claims.Cur.InsPayAmt=PIn.PDouble(textInsPayAmt.Text);
			//claimchecknum
			if(textDedApplied.Text=="")
				Claims.Cur.DedApplied=0;
			else
				Claims.Cur.DedApplied=PIn.PDouble(textDedApplied.Text);
			if(textOverMax.Text=="")
				Claims.Cur.OverMax=0;
			else
				Claims.Cur.OverMax=PIn.PDouble(textOverMax.Text);
			Claims.Cur.PreAuthString=textPreAuth.Text;
			//isprosthesis handled earlier
			if(textPriorDate.Text=="")
				Claims.Cur.PriorDate=DateTime.MinValue;
			else
				Claims.Cur.PriorDate=PIn.PDate(textPriorDate.Text);
			Claims.Cur.ReasonUnderPaid=textReasonUnder.Text;
			Claims.Cur.ClaimNote=textNote.Text;
			//priclaimnum
			//secclaimnum
			//ispreauth
			if(listProvBill.SelectedIndex!=-1)
				Claims.Cur.ProvBill=Providers.List[listProvBill.SelectedIndex].ProvNum;
			Claims.Cur.ReferringProv=FormCRef.ReferringProv;
			Claims.Cur.RefNumString=FormCRef.RefNumString;
			Claims.Cur.PlaceService=(PlaceOfService)comboPlaceService.SelectedIndex;
			switch(listAccident.SelectedIndex){
				case 0:
					Claims.Cur.AccidentRelated="";
					break;
				case 1:
					Claims.Cur.AccidentRelated="A";
					break;
				case 2:
					Claims.Cur.AccidentRelated="E";
					break;
				case 3:
					Claims.Cur.AccidentRelated="O";
					break;
			}
			Claims.Cur.AccidentDate=PIn.PDate(textAccidentDate.Text);
			Claims.Cur.AccidentST=textAccidentST.Text;
      switch(listEmploy.SelectedIndex){
				case 0:
					Claims.Cur.EmployRelated=YN.Unknown;
					break;
				case 1:
					Claims.Cur.EmployRelated=YN.Yes;
					break;
				case 2:
					Claims.Cur.EmployRelated=YN.No;
					break;				
			}		
			Claims.Cur.IsOrtho=checkIsOrtho.Checked;
			Claims.Cur.OrthoRemainM=PIn.PInt(textOrthoRemainM.Text);
			Claims.Cur.OrthoDate=PIn.PDate(textOrthoDate.Text);
			Claims.UpdateCur();
			if(Claims.Cur.ClaimStatus=="S"){
				SecurityLogs.MakeLogEntry("Claims Sent Edit",Claims.cmd.CommandText);
			}
		}

		private void butRefer_Click(object sender, System.EventArgs e) {
			FormCRef.ShowDialog();
		}

		//cancel does not cancel in some circumstances because cur gets updated in some areas.
		private void butCancel_Click(object sender, System.EventArgs e) {
			if(IsNew){
				if(IsPrimary){
					Claims.DetachProcsFromClaim();
				}
        if(Claims.Cur.ClaimType=="PreAuth"){
					for(int i=0;i<ClaimProcs.ForClaim.Count;i++){
						ClaimProcs.Cur=(ClaimProc)ClaimProcs.ForClaim[i];
						ClaimProcs.DeleteCur();
					}
        }
				//Claims.PutBalEst(Claims.Cur.DateService,-OriginalEst);
				//Claims.PutBalPay(Claims.Cur.DateService,-OriginalPay);
				Claims.DeleteCur();
			}
			else{
				;
			}
			DialogResult=DialogResult.Cancel;
		}

		private void butSend_Click(object sender, System.EventArgs e) {
			if(!ClaimIsValid())
				return;
			Claims.Cur.ClaimStatus="W";
			textDateSent.Text=DateTime.Today.ToString("d");
			UpdateClaim();		
			if(IsNew && Patients.Cur.SecPlanNum!=0 && Claims.Cur.ClaimType!="PreAuth"){//if pt has secondary ins
				CreateSecondary();
				FormClaimEdit FormCE=new FormClaimEdit();
				FormCE.IsNew=true;//made this true to trigger calc of ded and overmax
				FormCE.FillForm();
			}
			DialogResult=DialogResult.OK;
		}

		

	}
}
