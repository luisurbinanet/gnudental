/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormClaimEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private OpenDental.TableClaimProc tbProc;
		private System.Windows.Forms.TextBox textReasonUnder;
		private System.Windows.Forms.TextBox textDateService;
		private System.Windows.Forms.TextBox textPreAuth;
		private OpenDental.ValidDate textDateRec;
		private OpenDental.ValidDate textDateSent;
		private System.ComponentModel.Container components = null;// Required designer variable.
		private OpenDental.UI.Button butOK;
		///<summary></summary>
		public bool IsNew;
		private System.Windows.Forms.RadioButton radioProsthN;
		private System.Windows.Forms.RadioButton radioProsthR;
		private System.Windows.Forms.RadioButton radioProsthI;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textPlan;
		private System.Windows.Forms.TextBox textClaimFee;
		private System.Windows.Forms.Label label4;
		private OpenDental.ValidDouble textDedApplied;
		private OpenDental.ValidDouble textInsPayAmt;
		private OpenDental.ValidDate textPriorDate;
		//private double ClaimFee;
		//private double PriInsPayEstSubtotal;
		//private double SecInsPayEstSubtotal;
		//private double PriInsPayEst;
		//private double SecInsPayEst;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.CheckBox checkIsOrtho;
		private OpenDental.ValidNum textOrthoRemainM;
		private OpenDental.ValidDate textOrthoDate;
		private System.Windows.Forms.Label label27;
		private FormClaimSupplemental FormCS=new FormClaimSupplemental();
		private System.Windows.Forms.Label labelPreAuthNum;
		private System.Windows.Forms.Label labelDateService;
		private OpenDental.UI.Button butSupp;
		private System.Windows.Forms.ComboBox comboProvBill;
		private System.Windows.Forms.ComboBox comboProvTreat;
		private System.Windows.Forms.GroupBox groupEnterPayment;
		private System.Windows.Forms.ListBox listClaimStatus;
		private System.Windows.Forms.Label label2;
		private OpenDental.TableClaimPay tbPay;
		private OpenDental.UI.Button butPayTotal;
		private OpenDental.UI.Button butPayProc;
		private System.Windows.Forms.Label label7;
		private OpenDental.UI.Button butCheckAdd;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.ListBox listClaimType;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ComboBox comboPatRelat;
		private System.Windows.Forms.ComboBox comboPatRelat2;
		private System.Windows.Forms.TextBox textPlan2;
		private OpenDental.UI.Button butRecalc;
		private System.Windows.Forms.TextBox textInsPayEst;
		private OpenDental.ValidDouble textWriteOff;
		private OpenDental.UI.Button butPaySupp;
		private OpenDental.UI.Button butPreview;
		private System.Windows.Forms.PrintDialog printDialog2;
		private OpenDental.UI.Button butPrint;
		private OpenDental.UI.Button butOtherNone;
		private System.Windows.Forms.Label label11;
		private OpenDental.ValidNum textRadiographs;
		private OpenDental.UI.Button butOtherCovChange;
		//private double DedAdjPerc;
		private ClaimProc[] ClaimProcsForClaim;
		///<summary>All claimprocs for the patient. Used to calculate remaining benefits, etc.</summary>
		private ClaimProc[] ClaimProcList;
		private OpenDental.ODtextBox textNote;
		/// <summary>List of all procedures for this patient.  Used to get descriptions, etc.</summary>
		private Procedure[] ProcList;
		private Patient PatCur;
		private Family FamCur;
		private InsPlan[] PlanList;

		///<summary></summary>
		public FormClaimEdit(Patient patCur,Family famCur){
			PatCur=patCur;
			FamCur=famCur;
			InitializeComponent();// Required for Windows Form Designer support
			tbPay.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbPay_CellDoubleClicked);
			tbProc.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbProc_CellClicked);
			tbPay.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbPay_CellClicked);
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormClaimEdit));
			this.label3 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.labelDateService = new System.Windows.Forms.Label();
			this.labelPreAuthNum = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textPriorDate = new OpenDental.ValidDate();
			this.label18 = new System.Windows.Forms.Label();
			this.radioProsthN = new System.Windows.Forms.RadioButton();
			this.radioProsthR = new System.Windows.Forms.RadioButton();
			this.radioProsthI = new System.Windows.Forms.RadioButton();
			this.label17 = new System.Windows.Forms.Label();
			this.textDateSent = new OpenDental.ValidDate();
			this.textDateRec = new OpenDental.ValidDate();
			this.tbProc = new OpenDental.TableClaimProc();
			this.textReasonUnder = new System.Windows.Forms.TextBox();
			this.textInsPayEst = new System.Windows.Forms.TextBox();
			this.textDateService = new System.Windows.Forms.TextBox();
			this.textPreAuth = new System.Windows.Forms.TextBox();
			this.butOK = new OpenDental.UI.Button();
			this.textClaimFee = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textPlan = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textDedApplied = new OpenDental.ValidDouble();
			this.textInsPayAmt = new OpenDental.ValidDouble();
			this.butCancel = new OpenDental.UI.Button();
			this.label20 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.textOrthoDate = new OpenDental.ValidDate();
			this.label27 = new System.Windows.Forms.Label();
			this.textOrthoRemainM = new OpenDental.ValidNum();
			this.checkIsOrtho = new System.Windows.Forms.CheckBox();
			this.label26 = new System.Windows.Forms.Label();
			this.butSupp = new OpenDental.UI.Button();
			this.comboProvBill = new System.Windows.Forms.ComboBox();
			this.tbPay = new OpenDental.TableClaimPay();
			this.comboProvTreat = new System.Windows.Forms.ComboBox();
			this.butPayTotal = new OpenDental.UI.Button();
			this.butPayProc = new OpenDental.UI.Button();
			this.groupEnterPayment = new System.Windows.Forms.GroupBox();
			this.butPaySupp = new OpenDental.UI.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.listClaimStatus = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.butCheckAdd = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.listClaimType = new System.Windows.Forms.ListBox();
			this.label9 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.comboPatRelat = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.butOtherNone = new OpenDental.UI.Button();
			this.butOtherCovChange = new OpenDental.UI.Button();
			this.comboPatRelat2 = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textPlan2 = new System.Windows.Forms.TextBox();
			this.butRecalc = new OpenDental.UI.Button();
			this.textWriteOff = new OpenDental.ValidDouble();
			this.butPrint = new OpenDental.UI.Button();
			this.butPreview = new OpenDental.UI.Button();
			this.printDialog2 = new System.Windows.Forms.PrintDialog();
			this.textRadiographs = new OpenDental.ValidNum();
			this.label11 = new System.Windows.Forms.Label();
			this.textNote = new OpenDental.ODtextBox();
			this.groupBox1.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupEnterPayment.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(241, 92);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(94, 15);
			this.label3.TabIndex = 2;
			this.label3.Text = "Billing Dentist";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(2, 135);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(108, 16);
			this.label6.TabIndex = 5;
			this.label6.Text = "Date Received";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(5, 114);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(104, 16);
			this.label8.TabIndex = 7;
			this.label8.Text = "DateSent";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelDateService
			// 
			this.labelDateService.Location = new System.Drawing.Point(3, 94);
			this.labelDateService.Name = "labelDateService";
			this.labelDateService.Size = new System.Drawing.Size(107, 16);
			this.labelDateService.TabIndex = 8;
			this.labelDateService.Text = "Date of Service";
			this.labelDateService.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelPreAuthNum
			// 
			this.labelPreAuthNum.Location = new System.Drawing.Point(230, 136);
			this.labelPreAuthNum.Name = "labelPreAuthNum";
			this.labelPreAuthNum.Size = new System.Drawing.Size(107, 16);
			this.labelPreAuthNum.TabIndex = 11;
			this.labelPreAuthNum.Text = "PreAuth Number";
			this.labelPreAuthNum.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(6, 40);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(157, 16);
			this.label16.TabIndex = 16;
			this.label16.Text = "Prior Date of Placement";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(349, 510);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(346, 16);
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
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(17, 512);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(254, 80);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Crown, Bridge, or Denture";
			// 
			// textPriorDate
			// 
			this.textPriorDate.Location = new System.Drawing.Point(168, 36);
			this.textPriorDate.Name = "textPriorDate";
			this.textPriorDate.Size = new System.Drawing.Size(66, 20);
			this.textPriorDate.TabIndex = 3;
			this.textPriorDate.Text = "";
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(6, 60);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(246, 18);
			this.label18.TabIndex = 29;
			this.label18.Text = "(Might need a note. Might need to attach x-ray)";
			// 
			// radioProsthN
			// 
			this.radioProsthN.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioProsthN.Location = new System.Drawing.Point(12, 18);
			this.radioProsthN.Name = "radioProsthN";
			this.radioProsthN.Size = new System.Drawing.Size(46, 16);
			this.radioProsthN.TabIndex = 0;
			this.radioProsthN.Text = "No";
			this.radioProsthN.Click += new System.EventHandler(this.radioProsthN_Click);
			// 
			// radioProsthR
			// 
			this.radioProsthR.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioProsthR.Location = new System.Drawing.Point(132, 18);
			this.radioProsthR.Name = "radioProsthR";
			this.radioProsthR.Size = new System.Drawing.Size(104, 16);
			this.radioProsthR.TabIndex = 2;
			this.radioProsthR.Text = "Replacement";
			this.radioProsthR.Click += new System.EventHandler(this.radioProsthR_Click);
			// 
			// radioProsthI
			// 
			this.radioProsthI.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioProsthI.Location = new System.Drawing.Point(64, 18);
			this.radioProsthI.Name = "radioProsthI";
			this.radioProsthI.Size = new System.Drawing.Size(64, 16);
			this.radioProsthI.TabIndex = 1;
			this.radioProsthI.Text = "Initial";
			this.radioProsthI.Click += new System.EventHandler(this.radioProsthI_Click);
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(17, 597);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(313, 32);
			this.label17.TabIndex = 28;
			this.label17.Text = "For bridges, dentures, and partials, missing teeth must have been correctly enter" +
				"ed in the Chart module. ";
			// 
			// textDateSent
			// 
			this.textDateSent.Location = new System.Drawing.Point(111, 112);
			this.textDateSent.Name = "textDateSent";
			this.textDateSent.Size = new System.Drawing.Size(82, 20);
			this.textDateSent.TabIndex = 6;
			this.textDateSent.Text = "";
			// 
			// textDateRec
			// 
			this.textDateRec.Location = new System.Drawing.Point(111, 133);
			this.textDateRec.Name = "textDateRec";
			this.textDateRec.Size = new System.Drawing.Size(82, 20);
			this.textDateRec.TabIndex = 7;
			this.textDateRec.Text = "";
			// 
			// tbProc
			// 
			this.tbProc.BackColor = System.Drawing.SystemColors.Window;
			this.tbProc.Location = new System.Drawing.Point(2, 166);
			this.tbProc.Name = "tbProc";
			this.tbProc.ScrollValue = 111;
			this.tbProc.SelectedIndices = new int[0];
			this.tbProc.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.tbProc.Size = new System.Drawing.Size(939, 204);
			this.tbProc.TabIndex = 25;
			this.tbProc.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(this.tbProc_CellDoubleClicked);
			// 
			// textReasonUnder
			// 
			this.textReasonUnder.Location = new System.Drawing.Point(734, 436);
			this.textReasonUnder.MaxLength = 255;
			this.textReasonUnder.Multiline = true;
			this.textReasonUnder.Name = "textReasonUnder";
			this.textReasonUnder.Size = new System.Drawing.Size(191, 56);
			this.textReasonUnder.TabIndex = 7;
			this.textReasonUnder.Text = "";
			// 
			// textInsPayEst
			// 
			this.textInsPayEst.Location = new System.Drawing.Point(469, 372);
			this.textInsPayEst.Name = "textInsPayEst";
			this.textInsPayEst.ReadOnly = true;
			this.textInsPayEst.Size = new System.Drawing.Size(65, 20);
			this.textInsPayEst.TabIndex = 40;
			this.textInsPayEst.Text = "";
			this.textInsPayEst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textDateService
			// 
			this.textDateService.Location = new System.Drawing.Point(111, 91);
			this.textDateService.Name = "textDateService";
			this.textDateService.Size = new System.Drawing.Size(82, 20);
			this.textDateService.TabIndex = 41;
			this.textDateService.Text = "";
			// 
			// textPreAuth
			// 
			this.textPreAuth.Location = new System.Drawing.Point(336, 132);
			this.textPreAuth.Name = "textPreAuth";
			this.textPreAuth.Size = new System.Drawing.Size(170, 20);
			this.textPreAuth.TabIndex = 1;
			this.textPreAuth.Text = "";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(766, 636);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 14;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textClaimFee
			// 
			this.textClaimFee.Location = new System.Drawing.Point(339, 372);
			this.textClaimFee.Name = "textClaimFee";
			this.textClaimFee.ReadOnly = true;
			this.textClaimFee.Size = new System.Drawing.Size(65, 20);
			this.textClaimFee.TabIndex = 51;
			this.textClaimFee.Text = "";
			this.textClaimFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(219, 375);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(116, 16);
			this.label1.TabIndex = 50;
			this.label1.Text = "Totals";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPlan
			// 
			this.textPlan.Location = new System.Drawing.Point(8, 20);
			this.textPlan.Name = "textPlan";
			this.textPlan.ReadOnly = true;
			this.textPlan.Size = new System.Drawing.Size(253, 20);
			this.textPlan.TabIndex = 1;
			this.textPlan.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(734, 395);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(190, 36);
			this.label4.TabIndex = 55;
			this.label4.Text = "Reasons underpaid:  (will show on patient bill in a future version)";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textDedApplied
			// 
			this.textDedApplied.Location = new System.Drawing.Point(404, 372);
			this.textDedApplied.Name = "textDedApplied";
			this.textDedApplied.ReadOnly = true;
			this.textDedApplied.Size = new System.Drawing.Size(65, 20);
			this.textDedApplied.TabIndex = 4;
			this.textDedApplied.Text = "";
			this.textDedApplied.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textInsPayAmt
			// 
			this.textInsPayAmt.Location = new System.Drawing.Point(534, 372);
			this.textInsPayAmt.Name = "textInsPayAmt";
			this.textInsPayAmt.ReadOnly = true;
			this.textInsPayAmt.Size = new System.Drawing.Size(65, 20);
			this.textInsPayAmt.TabIndex = 6;
			this.textInsPayAmt.Text = "";
			this.textInsPayAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(856, 636);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 15;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(824, 591);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(106, 33);
			this.label20.TabIndex = 92;
			this.label20.Text = "(does not cancel payment edits)";
			this.label20.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(234, 113);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(102, 15);
			this.label21.TabIndex = 93;
			this.label21.Text = "Treating Dentist";
			this.label21.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.textOrthoDate);
			this.groupBox4.Controls.Add(this.label27);
			this.groupBox4.Controls.Add(this.textOrthoRemainM);
			this.groupBox4.Controls.Add(this.checkIsOrtho);
			this.groupBox4.Controls.Add(this.label26);
			this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox4.Location = new System.Drawing.Point(734, 498);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(192, 88);
			this.groupBox4.TabIndex = 11;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Ortho";
			// 
			// textOrthoDate
			// 
			this.textOrthoDate.Location = new System.Drawing.Point(115, 36);
			this.textOrthoDate.Name = "textOrthoDate";
			this.textOrthoDate.Size = new System.Drawing.Size(66, 20);
			this.textOrthoDate.TabIndex = 1;
			this.textOrthoDate.Text = "";
			// 
			// label27
			// 
			this.label27.Location = new System.Drawing.Point(5, 40);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(109, 16);
			this.label27.TabIndex = 104;
			this.label27.Text = "Date of Placement";
			this.label27.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textOrthoRemainM
			// 
			this.textOrthoRemainM.Location = new System.Drawing.Point(116, 59);
			this.textOrthoRemainM.MinVal = 0;
			this.textOrthoRemainM.Name = "textOrthoRemainM";
			this.textOrthoRemainM.Size = new System.Drawing.Size(39, 20);
			this.textOrthoRemainM.TabIndex = 2;
			this.textOrthoRemainM.Text = "";
			// 
			// checkIsOrtho
			// 
			this.checkIsOrtho.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsOrtho.Location = new System.Drawing.Point(12, 16);
			this.checkIsOrtho.Name = "checkIsOrtho";
			this.checkIsOrtho.Size = new System.Drawing.Size(90, 18);
			this.checkIsOrtho.TabIndex = 0;
			this.checkIsOrtho.Text = "Is For Ortho";
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(3, 60);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(112, 18);
			this.label26.TabIndex = 102;
			this.label26.Text = "Months Remaining";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butSupp
			// 
			this.butSupp.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSupp.Autosize = true;
			this.butSupp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSupp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSupp.Location = new System.Drawing.Point(142, 636);
			this.butSupp.Name = "butSupp";
			this.butSupp.Size = new System.Drawing.Size(147, 26);
			this.butSupp.TabIndex = 95;
			this.butSupp.Text = "&View Supplemental Info";
			this.butSupp.Click += new System.EventHandler(this.butSupp_Click);
			// 
			// comboProvBill
			// 
			this.comboProvBill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProvBill.Location = new System.Drawing.Point(336, 88);
			this.comboProvBill.Name = "comboProvBill";
			this.comboProvBill.Size = new System.Drawing.Size(100, 21);
			this.comboProvBill.TabIndex = 97;
			// 
			// tbPay
			// 
			this.tbPay.BackColor = System.Drawing.SystemColors.Window;
			this.tbPay.Location = new System.Drawing.Point(2, 399);
			this.tbPay.Name = "tbPay";
			this.tbPay.ScrollValue = 209;
			this.tbPay.SelectedIndices = new int[0];
			this.tbPay.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbPay.Size = new System.Drawing.Size(549, 106);
			this.tbPay.TabIndex = 98;
			// 
			// comboProvTreat
			// 
			this.comboProvTreat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProvTreat.Location = new System.Drawing.Point(336, 110);
			this.comboProvTreat.Name = "comboProvTreat";
			this.comboProvTreat.Size = new System.Drawing.Size(100, 21);
			this.comboProvTreat.TabIndex = 99;
			// 
			// butPayTotal
			// 
			this.butPayTotal.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPayTotal.Autosize = true;
			this.butPayTotal.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPayTotal.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPayTotal.Location = new System.Drawing.Point(17, 21);
			this.butPayTotal.Name = "butPayTotal";
			this.butPayTotal.Size = new System.Drawing.Size(99, 23);
			this.butPayTotal.TabIndex = 100;
			this.butPayTotal.Text = "&Total";
			this.butPayTotal.Click += new System.EventHandler(this.butPayTotal_Click);
			// 
			// butPayProc
			// 
			this.butPayProc.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPayProc.Autosize = true;
			this.butPayProc.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPayProc.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPayProc.Location = new System.Drawing.Point(17, 51);
			this.butPayProc.Name = "butPayProc";
			this.butPayProc.Size = new System.Drawing.Size(99, 23);
			this.butPayProc.TabIndex = 101;
			this.butPayProc.Text = "&By Procedure";
			this.butPayProc.Click += new System.EventHandler(this.butPayProc_Click);
			// 
			// groupEnterPayment
			// 
			this.groupEnterPayment.Controls.Add(this.butPaySupp);
			this.groupEnterPayment.Controls.Add(this.butPayTotal);
			this.groupEnterPayment.Controls.Add(this.butPayProc);
			this.groupEnterPayment.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupEnterPayment.Location = new System.Drawing.Point(801, 13);
			this.groupEnterPayment.Name = "groupEnterPayment";
			this.groupEnterPayment.Size = new System.Drawing.Size(133, 127);
			this.groupEnterPayment.TabIndex = 102;
			this.groupEnterPayment.TabStop = false;
			this.groupEnterPayment.Text = "Enter Payment";
			// 
			// butPaySupp
			// 
			this.butPaySupp.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPaySupp.Autosize = true;
			this.butPaySupp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPaySupp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPaySupp.Location = new System.Drawing.Point(17, 97);
			this.butPaySupp.Name = "butPaySupp";
			this.butPaySupp.Size = new System.Drawing.Size(99, 23);
			this.butPaySupp.TabIndex = 102;
			this.butPaySupp.Text = "S&upplemental";
			this.butPaySupp.Click += new System.EventHandler(this.butPaySupp_Click);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(561, 432);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(154, 54);
			this.label7.TabIndex = 103;
			this.label7.Text = "Don\'t create a new check until payments for all claims have been entered.";
			// 
			// listClaimStatus
			// 
			this.listClaimStatus.Location = new System.Drawing.Point(111, 8);
			this.listClaimStatus.Name = "listClaimStatus";
			this.listClaimStatus.Size = new System.Drawing.Size(120, 82);
			this.listClaimStatus.TabIndex = 103;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 14);
			this.label2.TabIndex = 104;
			this.label2.Text = "Claim Status";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butCheckAdd
			// 
			this.butCheckAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCheckAdd.Autosize = true;
			this.butCheckAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCheckAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCheckAdd.Image = ((System.Drawing.Image)(resources.GetObject("butCheckAdd.Image")));
			this.butCheckAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butCheckAdd.Location = new System.Drawing.Point(561, 400);
			this.butCheckAdd.Name = "butCheckAdd";
			this.butCheckAdd.Size = new System.Drawing.Size(114, 26);
			this.butCheckAdd.TabIndex = 105;
			this.butCheckAdd.Text = "Create C&heck";
			this.butCheckAdd.Click += new System.EventHandler(this.butCheckAdd_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(8, 636);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(91, 26);
			this.butDelete.TabIndex = 106;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// listClaimType
			// 
			this.listClaimType.ForeColor = System.Drawing.SystemColors.WindowText;
			this.listClaimType.Location = new System.Drawing.Point(336, 18);
			this.listClaimType.Name = "listClaimType";
			this.listClaimType.Size = new System.Drawing.Size(98, 69);
			this.listClaimType.TabIndex = 108;
			this.listClaimType.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listClaimType_MouseUp);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(239, 20);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(95, 17);
			this.label9.TabIndex = 109;
			this.label9.Text = "Claim Type";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.comboPatRelat);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.textPlan);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(523, 5);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(269, 70);
			this.groupBox2.TabIndex = 110;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Insurance Plan";
			// 
			// comboPatRelat
			// 
			this.comboPatRelat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPatRelat.Location = new System.Drawing.Point(90, 43);
			this.comboPatRelat.Name = "comboPatRelat";
			this.comboPatRelat.Size = new System.Drawing.Size(151, 21);
			this.comboPatRelat.TabIndex = 3;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6, 46);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(85, 17);
			this.label5.TabIndex = 2;
			this.label5.Text = "Relationship";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.butOtherNone);
			this.groupBox3.Controls.Add(this.butOtherCovChange);
			this.groupBox3.Controls.Add(this.comboPatRelat2);
			this.groupBox3.Controls.Add(this.label10);
			this.groupBox3.Controls.Add(this.textPlan2);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(523, 77);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(269, 85);
			this.groupBox3.TabIndex = 111;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Other Coverage";
			// 
			// butOtherNone
			// 
			this.butOtherNone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOtherNone.Autosize = true;
			this.butOtherNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOtherNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOtherNone.Location = new System.Drawing.Point(183, 10);
			this.butOtherNone.Name = "butOtherNone";
			this.butOtherNone.Size = new System.Drawing.Size(78, 23);
			this.butOtherNone.TabIndex = 5;
			this.butOtherNone.Text = "None";
			this.butOtherNone.Click += new System.EventHandler(this.butOtherNone_Click);
			// 
			// butOtherCovChange
			// 
			this.butOtherCovChange.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOtherCovChange.Autosize = true;
			this.butOtherCovChange.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOtherCovChange.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOtherCovChange.Location = new System.Drawing.Point(101, 10);
			this.butOtherCovChange.Name = "butOtherCovChange";
			this.butOtherCovChange.Size = new System.Drawing.Size(78, 23);
			this.butOtherCovChange.TabIndex = 4;
			this.butOtherCovChange.Text = "Change";
			this.butOtherCovChange.Click += new System.EventHandler(this.butOtherCovChange_Click);
			// 
			// comboPatRelat2
			// 
			this.comboPatRelat2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPatRelat2.Location = new System.Drawing.Point(90, 57);
			this.comboPatRelat2.Name = "comboPatRelat2";
			this.comboPatRelat2.Size = new System.Drawing.Size(151, 21);
			this.comboPatRelat2.TabIndex = 3;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(6, 60);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(84, 17);
			this.label10.TabIndex = 2;
			this.label10.Text = "Relationship";
			// 
			// textPlan2
			// 
			this.textPlan2.Location = new System.Drawing.Point(8, 34);
			this.textPlan2.Name = "textPlan2";
			this.textPlan2.ReadOnly = true;
			this.textPlan2.Size = new System.Drawing.Size(253, 20);
			this.textPlan2.TabIndex = 1;
			this.textPlan2.Text = "";
			// 
			// butRecalc
			// 
			this.butRecalc.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRecalc.Autosize = true;
			this.butRecalc.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRecalc.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRecalc.Location = new System.Drawing.Point(735, 371);
			this.butRecalc.Name = "butRecalc";
			this.butRecalc.Size = new System.Drawing.Size(148, 25);
			this.butRecalc.TabIndex = 112;
			this.butRecalc.Text = "Recalculate &Estimates";
			this.butRecalc.Click += new System.EventHandler(this.butRecalc_Click);
			// 
			// textWriteOff
			// 
			this.textWriteOff.Location = new System.Drawing.Point(599, 372);
			this.textWriteOff.Name = "textWriteOff";
			this.textWriteOff.ReadOnly = true;
			this.textWriteOff.Size = new System.Drawing.Size(65, 20);
			this.textWriteOff.TabIndex = 113;
			this.textWriteOff.Text = "";
			this.textWriteOff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.Image = ((System.Drawing.Image)(resources.GetObject("butPrint.Image")));
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(326, 636);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(86, 26);
			this.butPrint.TabIndex = 114;
			this.butPrint.Text = "&Print";
			this.butPrint.Click += new System.EventHandler(this.ButPrint_Click);
			// 
			// butPreview
			// 
			this.butPreview.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPreview.Autosize = true;
			this.butPreview.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPreview.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPreview.Image = ((System.Drawing.Image)(resources.GetObject("butPreview.Image")));
			this.butPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPreview.Location = new System.Drawing.Point(428, 636);
			this.butPreview.Name = "butPreview";
			this.butPreview.Size = new System.Drawing.Size(92, 26);
			this.butPreview.TabIndex = 115;
			this.butPreview.Text = "P&review";
			this.butPreview.Click += new System.EventHandler(this.butPreview_Click);
			// 
			// textRadiographs
			// 
			this.textRadiographs.Location = new System.Drawing.Point(580, 606);
			this.textRadiographs.MinVal = 0;
			this.textRadiographs.Name = "textRadiographs";
			this.textRadiographs.Size = new System.Drawing.Size(39, 20);
			this.textRadiographs.TabIndex = 116;
			this.textRadiographs.Text = "";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(391, 608);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(187, 18);
			this.label11.TabIndex = 117;
			this.label11.Text = "Radiographs Attached";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(350, 528);
			this.textNote.MaxLength = 255;
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDental.QuickPasteType.Claim;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(319, 68);
			this.textNote.TabIndex = 118;
			this.textNote.Text = "";
			// 
			// FormClaimEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(947, 669);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.textRadiographs);
			this.Controls.Add(this.textWriteOff);
			this.Controls.Add(this.textInsPayEst);
			this.Controls.Add(this.textInsPayAmt);
			this.Controls.Add(this.textClaimFee);
			this.Controls.Add(this.textDedApplied);
			this.Controls.Add(this.textReasonUnder);
			this.Controls.Add(this.textPreAuth);
			this.Controls.Add(this.textDateService);
			this.Controls.Add(this.textDateSent);
			this.Controls.Add(this.textDateRec);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.butPreview);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.butRecalc);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.listClaimType);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCheckAdd);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listClaimStatus);
			this.Controls.Add(this.groupEnterPayment);
			this.Controls.Add(this.comboProvTreat);
			this.Controls.Add(this.tbPay);
			this.Controls.Add(this.comboProvBill);
			this.Controls.Add(this.butSupp);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.label21);
			this.Controls.Add(this.label20);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.tbProc);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.labelPreAuthNum);
			this.Controls.Add(this.labelDateService);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClaimEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Claim";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormClaimEdit_Closing);
			this.Load += new System.EventHandler(this.FormClaimEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupEnterPayment.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormClaimEdit_Load(object sender, System.EventArgs e) {
			if(IsNew){
				//butPayWizard.Enabled=false;
			}
			else{
				if(Claims.Cur.ClaimStatus=="S"//sent
					|| Claims.Cur.ClaimStatus=="R")//or received
				{
					if(!UserPermissions.CheckUserPassword("Claims Sent Edit",Claims.Cur.DateSent)){
						butOK.Enabled=false;
						butDelete.Enabled=false;
						butPrint.Enabled=false;
						groupEnterPayment.Enabled=false;
						tbPay.Enabled=false;
						tbProc.Enabled=false;
						listClaimStatus.Enabled=false;
						butCheckAdd.Enabled=false;
					}		
				}	
			}
			if(Claims.Cur.ClaimType=="PreAuth"){
				labelPreAuthNum.Visible=false;
				textPreAuth.Visible=false;
				textDateService.Visible=false;
				labelDateService.Visible=false;
				label20.Visible=false;//warning when delete
				textReasonUnder.Visible=false;
				label4.Visible=false;//reason under
				butPayTotal.Visible=false;	
      }
			listClaimType.Items.Add(Lan.g(this,"Primary"));
			listClaimType.Items.Add(Lan.g(this,"Secondary"));
			listClaimType.Items.Add(Lan.g(this,"PreAuth"));
			listClaimType.Items.Add(Lan.g(this,"Other"));
			listClaimType.Items.Add(Lan.g(this,"Capitation"));
			listClaimStatus.Items.Add(Lan.g(this,"Unsent"));
			listClaimStatus.Items.Add(Lan.g(this,"Hold until Pri received"));
			listClaimStatus.Items.Add(Lan.g(this,"Waiting to Send"));
			listClaimStatus.Items.Add(Lan.g(this,"Probably Sent"));
			listClaimStatus.Items.Add(Lan.g(this,"Sent - Verified"));
			listClaimStatus.Items.Add(Lan.g(this,"Received"));
			string[] enumRelat=Enum.GetNames(typeof(Relat));
			for(int i=0;i<enumRelat.Length;i++){;
				comboPatRelat.Items.Add(Lan.g("enumRelat",enumRelat[i]));
				comboPatRelat2.Items.Add(Lan.g("enumRelat",enumRelat[i]));
			}
      Claims.Refresh(PatCur.PatNum); 
      ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			ProcList=Procedures.Refresh(PatCur.PatNum);
			PlanList=InsPlans.Refresh(FamCur);
			FillForm();			
		}

		///<summary></summary>
		public void FillForm(){
			this.Text=Lan.g(this,"Edit Claim")+" - "+PatCur.GetNameLF();
			if(Claims.Cur.DateService.Year<1880)
				textDateService.Text="";
			else
				textDateService.Text=Claims.Cur.DateService.ToShortDateString();
			if(Claims.Cur.DateSent.Year<1880)
				textDateSent.Text="";
			else
				textDateSent.Text=Claims.Cur.DateSent.ToShortDateString();
			if(Claims.Cur.DateReceived.Year<1880)
				textDateRec.Text="";
			else
				textDateRec.Text=Claims.Cur.DateReceived.ToShortDateString();
			switch(Claims.Cur.ClaimStatus){
				case "U"://unsent
					listClaimStatus.SelectedIndex=0;
					break;
				case "H"://hold until pri received
					listClaimStatus.SelectedIndex=1;
					break;
				case "W"://waiting to be sent
					listClaimStatus.SelectedIndex=2;
					break;
				case "P"://probably sent
					listClaimStatus.SelectedIndex=3;
					break;
				case "S"://sent-verified
					listClaimStatus.SelectedIndex=4;
					break;
				case "R"://received
					listClaimStatus.SelectedIndex=5;
					break;
			}
			switch(Claims.Cur.ClaimType){
				case "P":
					listClaimType.SelectedIndex=0;
					break;
				case "S":
					listClaimType.SelectedIndex=1;
					break;
				case "PreAuth":
					listClaimType.SelectedIndex=2;
					break;
				case "Other":
					listClaimType.SelectedIndex=3;
					break;
				case "Cap":
					listClaimType.SelectedIndex=4;
					break;
			}
			comboProvBill.Items.Clear();
			for(int i=0;i<Providers.List.Length;i++){
				comboProvBill.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==Claims.Cur.ProvBill)
					comboProvBill.SelectedIndex=i;
			}
			if(comboProvBill.Items.Count>0 && comboProvBill.SelectedIndex==-1)
				comboProvBill.SelectedIndex=0;
			comboProvTreat.Items.Clear();
			for(int i=0;i<Providers.List.Length;i++){
				comboProvTreat.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==Claims.Cur.ProvTreat)
					comboProvTreat.SelectedIndex=i;
			}
			if(comboProvTreat.Items.Count>0 && comboProvTreat.SelectedIndex==-1)
				comboProvTreat.SelectedIndex=0;
			textPreAuth.Text=Claims.Cur.PreAuthString;
			textPlan.Text=InsPlans.GetDescript(Claims.Cur.PlanNum,FamCur,PlanList);
			comboPatRelat.SelectedIndex=(int)Claims.Cur.PatRelat;
			textPlan2.Text=InsPlans.GetDescript(Claims.Cur.PlanNum2,FamCur,PlanList);
			comboPatRelat2.SelectedIndex=(int)Claims.Cur.PatRelat2;
			if(textPlan2.Text==""){
				comboPatRelat2.Visible=false;
				label10.Visible=false;
			}
			else{
				comboPatRelat2.Visible=true;
				label10.Visible=true;
			}
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
			if(Claims.Cur.PriorDate.Year < 1860)
				textPriorDate.Text="";
			else
				textPriorDate.Text=Claims.Cur.PriorDate.ToShortDateString();
			textReasonUnder.Text=Claims.Cur.ReasonUnderPaid;
			textNote.Text=Claims.Cur.ClaimNote;
			checkIsOrtho.Checked=Claims.Cur.IsOrtho;
			textOrthoRemainM.Text=Claims.Cur.OrthoRemainM.ToString();
			if(Claims.Cur.OrthoDate.Year < 1860)
				textOrthoDate.Text="";
			else
				textOrthoDate.Text=Claims.Cur.OrthoDate.ToShortDateString();
			textRadiographs.Text=Claims.Cur.Radiographs.ToString();
			FillGrids();
		}

		private void listClaimType_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			//not allowed to change claim type
			switch(Claims.Cur.ClaimType){
				case "P":
					listClaimType.SelectedIndex=0;
					break;
				case "S":
					listClaimType.SelectedIndex=1;
					break;
				case "PreAuth":
					listClaimType.SelectedIndex=2;
					break;
				case "Other":
					listClaimType.SelectedIndex=3;
					break;
				case "Cap":
					listClaimType.SelectedIndex=4;
					break;
			}
		}

		private void butRecalc_Click(object sender, System.EventArgs e) {
			if(!ClaimIsValid()){
				return;
			}
			Claims.CalculateAndUpdate(ClaimProcList,ProcList,PatCur,PlanList,Claims.Cur);
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			FillGrids();
		}

		

		private void FillGrids(){
			//must run claimprocs.refresh separately beforehand
			//also recalculates totals because user might have changed certain items.
			double claimFee=0;
			double dedApplied=0;
			double insPayEst=0;
			double insPayAmt=0;
			double writeOff=0;
      ClaimProcsForClaim=ClaimProcs.GetForClaim(ClaimProcList,Claims.Cur.ClaimNum);
			tbProc.ResetRows(ClaimProcsForClaim.Length);
			Procedure ProcCur;
			for(int i=0;i<ClaimProcsForClaim.Length;i++){
				if(ClaimProcsForClaim[i].ProcNum==0){
					if(ClaimProcsForClaim[i].Status==ClaimProcStatus.NotReceived)
						tbProc.Cell[4,i]=Lan.g(this,"Estimate");
					else tbProc.Cell[4,i]=Lan.g(this,"Total Payment");
				}
				else{
					ProcCur=Procedures.GetProc(ProcList,ClaimProcsForClaim[i].ProcNum);
					if(ProcCur.ProcNum==0){
						ClaimProcsForClaim[i].Delete();//deletes unattached claimprocs which could cause crash.
						continue;
					}
					//if(!Procedures.HList.Contains(ClaimProcsForClaim[i].ProcNum)){
						//ClaimProcs.Cur=ClaimProcsForClaim[i];
					//	ClaimProcsForClaim[i].Delete();
					//	continue;
					//}
					//ProcCur=(Procedure)Procedures.HList[ClaimProcsForClaim[i].ProcNum];
					//Procedures.CurOld=Procedures.Cur;//may not be necessary
					tbProc.Cell[2,i]=ClaimProcsForClaim[i].CodeSent;
					tbProc.Cell[3,i]=ProcCur.ToothNum;
					tbProc.Cell[4,i]=ProcedureCodes.GetProcCode(ProcCur.ADACode).Descript;
				}
				tbProc.Cell[0,i]=ClaimProcsForClaim[i].ProcDate.ToShortDateString();
				tbProc.Cell[1,i]=Providers.GetAbbr(((ClaimProc)ClaimProcsForClaim[i]).ProvNum);
				tbProc.Cell[5,i]=ClaimProcsForClaim[i].FeeBilled.ToString("F");
				tbProc.Cell[6,i]=ClaimProcsForClaim[i].DedApplied.ToString("F");
				tbProc.Cell[7,i]=ClaimProcsForClaim[i].InsPayEst.ToString("F");
				tbProc.Cell[8,i]=ClaimProcsForClaim[i].InsPayAmt.ToString("F");
				tbProc.Cell[9,i]=ClaimProcsForClaim[i].WriteOff.ToString("F");
				switch(ClaimProcsForClaim[i].Status){
					case ClaimProcStatus.Received:
						tbProc.Cell[10,i]="Recd";
						break;
					case ClaimProcStatus.NotReceived:
						tbProc.Cell[10,i]="";
						break;
					//adjustment would never show here
					case ClaimProcStatus.Preauth:
						tbProc.Cell[10,i]="PreA";
						break;
					case ClaimProcStatus.Supplemental:
						tbProc.Cell[10,i]="Supp";
						break;
					case ClaimProcStatus.CapClaim:
						tbProc.Cell[10,i]="Cap";
						break;
					case ClaimProcStatus.Estimate:
						MessageBox.Show("error. Estimate loaded.");
						break;
					case ClaimProcStatus.CapEstimate:
						MessageBox.Show("error. CapEstimate loaded.");
						break;
					case ClaimProcStatus.CapComplete:
						MessageBox.Show("error. CapComplete loaded.");
						break;
					//Estimate would never show here
					//Cap would never show here
				}
				if(ClaimProcsForClaim[i].ClaimPaymentNum>0)
					tbProc.Cell[11,i]="X";
				tbProc.Cell[12,i]=ClaimProcsForClaim[i].Remarks;
				claimFee+=ClaimProcsForClaim[i].FeeBilled;
				dedApplied+=ClaimProcsForClaim[i].DedApplied;
				insPayEst+=ClaimProcsForClaim[i].InsPayEst;
				insPayAmt+=ClaimProcsForClaim[i].InsPayAmt;
				if(ClaimProcsForClaim[i].Status==ClaimProcStatus.Received
					|| ClaimProcsForClaim[i].Status==ClaimProcStatus.Supplemental)
				{
					writeOff+=ClaimProcsForClaim[i].WriteOff;
				}
			}
			tbProc.SetGridColor(Color.LightGray);
			tbProc.LayoutTables();
			if(Claims.Cur.ClaimType=="Cap"){
				//zero out ins info if Cap.  This keeps it from affecting the balance.  It could be slightly improved later if there is enough demand to show the inspayamt in the Account module.
				insPayEst=0;
				insPayAmt=0;
			}
			Claims.Cur.ClaimFee=claimFee;
			Claims.Cur.DedApplied=dedApplied;
			Claims.Cur.InsPayEst=insPayEst;
			Claims.Cur.InsPayAmt=insPayAmt;
			Claims.Cur.WriteOff=writeOff;
			textClaimFee.Text=Claims.Cur.ClaimFee.ToString("F");
			textDedApplied.Text=Claims.Cur.DedApplied.ToString("F");
			textInsPayEst.Text=Claims.Cur.InsPayEst.ToString("F");
			textInsPayAmt.Text=Claims.Cur.InsPayAmt.ToString("F");
			textWriteOff.Text=writeOff.ToString("F");
			//payments
      ClaimPayments.GetForClaim();
			tbPay.ResetRows(ClaimPayments.List.Length);
			for(int i=0;i<ClaimPayments.List.Length;i++){
				tbPay.Cell[0,i]=ClaimPayments.List[i].CheckDate.ToShortDateString();
				tbPay.Cell[1,i]=ClaimPayments.List[i].CheckAmt.ToString("F");
				tbPay.Cell[2,i]=ClaimPayments.List[i].CheckNum;
				tbPay.Cell[3,i]=ClaimPayments.List[i].BankBranch;
				tbPay.Cell[4,i]=ClaimPayments.List[i].Note;
			}
			tbPay.SetGridColor(Color.LightGray);
			tbPay.LayoutTables();
		}

		private void tbProc_CellDoubleClicked(object sender, CellEventArgs e){
			//ClaimProcs.Cur=(ClaimProc)ClaimProcs.ForClaim[e.Row];
			if(!MsgBox.Show(this,true,"If you are trying to enter payment information, please use the payments buttons at the upper right.  Then, don't forget to finish by creating the check using the button below this section. You should probably click cancel unless you are just editing estimates. Continue anyway?")){
				return;
			}
			FormClaimProc FormCP=new FormClaimProc(ClaimProcsForClaim[e.Row],null,FamCur,PlanList);
			FormCP.IsInClaim=true;
			FormCP.ShowDialog();
			if(FormCP.DialogResult!=DialogResult.OK){
				return;
			}
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			FillGrids();
		}

		private void tbPay_CellDoubleClicked(object sender, CellEventArgs e){
			int tempClaimNum=Claims.Cur.ClaimNum;
			FormClaimPayEdit FormCPE=new FormClaimPayEdit();
			ClaimPayments.Cur=ClaimPayments.List[e.Row];//remember that the claimpayment.List is not entirely accurate
			FormCPE.ShowDialog();
			Claims.Refresh(PatCur.PatNum);
			Claims.Cur=((Claim)Claims.HList[tempClaimNum]);
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			FillGrids();
		}

		private void tbProc_CellClicked(object sender, CellEventArgs e){
			if(tbPay.SelectedRow==-1)
				return;
			tbPay.ColorRow(tbPay.SelectedRow,Color.White);
			tbPay.SelectedRow=-1;
		}

		private void tbPay_CellClicked(object sender, CellEventArgs e){
			if(e.Row>ClaimPayments.List.Length-1){
				return;//prevents crash after deleting a check
			}
			for(int i=0;i<ClaimProcsForClaim.Length;i++){
				if(ClaimProcsForClaim[i].ClaimPaymentNum==ClaimPayments.List[e.Row].ClaimPaymentNum)
					tbProc.SetSelected(i,true);
				else
					tbProc.SetSelected(i,false);
			}
		}

		private void butOtherCovChange_Click(object sender, System.EventArgs e) {
			FormInsPlanSelect FormIPS=new FormInsPlanSelect(PatCur.PatNum);
			FormIPS.ShowDialog();
			if(FormIPS.DialogResult!=DialogResult.OK){
				return;
			}
			Claims.Cur.PlanNum2=FormIPS.SelectedPlan.PlanNum;
			textPlan2.Text=InsPlans.GetDescript(Claims.Cur.PlanNum2,FamCur,PlanList);
			if(textPlan2.Text==""){
				comboPatRelat2.Visible=false;
				label10.Visible=false;
			}
			else{
				comboPatRelat2.Visible=true;
				label10.Visible=true;
			}
		}

		private void butOtherNone_Click(object sender, System.EventArgs e) {
			Claims.Cur.PlanNum2=0;
			Claims.Cur.PatRelat2=Relat.Self;
			textPlan2.Text="";
			comboPatRelat2.Visible=false;
			label10.Visible=false;
		}

		private void butPayTotal_Click(object sender, System.EventArgs e) {
			//preauths are only allowed "payment" entry by procedure since a total would be meaningless
			if(Claims.Cur.ClaimType=="PreAuth"){
				MessageBox.Show(Lan.g(this,"PreAuthorizations can only be entered by procedure."));
				return;
			}
			if(Claims.Cur.ClaimType=="Cap"){
				if(MessageBox.Show(Lan.g(this,"If you enter by total, the insurance payment will affect the patient balance.  It is recommended to enter by procedure instead.  Continue anyway?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
				return;
			}
			Double dedEst=0;
			Double payEst=0;
			for(int i=0;i<ClaimProcsForClaim.Length;i++){
				if(ClaimProcsForClaim[i].Status!=ClaimProcStatus.NotReceived){
					continue;
				}
				if(ClaimProcsForClaim[i].ProcNum==0){
					continue;//also ignore non-procedures.
				}
				//ClaimProcs.Cur=ClaimProcs.ForClaim[i];
				dedEst+=ClaimProcsForClaim[i].DedApplied;
				payEst+=ClaimProcsForClaim[i].InsPayEst;
			}
			ClaimProc ClaimProcCur=new ClaimProc();
			//ClaimProcs.Cur.ProcNum 
			ClaimProcCur.ClaimNum=Claims.Cur.ClaimNum;
			ClaimProcCur.PatNum=Claims.Cur.PatNum;
			ClaimProcCur.ProvNum=Claims.Cur.ProvTreat;
			//ClaimProcs.Cur.FeeBilled
			//ClaimProcs.Cur.InsPayEst
			ClaimProcCur.DedApplied=dedEst;
			ClaimProcCur.Status=ClaimProcStatus.Received;
			ClaimProcCur.InsPayAmt=payEst;
			//remarks
			//ClaimProcs.Cur.ClaimPaymentNum
			ClaimProcCur.PlanNum=Claims.Cur.PlanNum;
			ClaimProcCur.DateCP=DateTime.Today;
			ClaimProcCur.ProcDate=Claims.Cur.DateService;
			ClaimProcCur.Insert();
			FormClaimProc FormCP=new FormClaimProc(ClaimProcCur,null,FamCur,PlanList);
			FormCP.IsInClaim=true;
			FormCP.ShowDialog();
			if(FormCP.DialogResult!=DialogResult.OK){
				ClaimProcCur.Delete();
			}
			else{
				for(int i=0;i<ClaimProcsForClaim.Length;i++){
					if(ClaimProcsForClaim[i].Status!=ClaimProcStatus.NotReceived){
						continue;
					}
					//ClaimProcs.Cur=ClaimProcs.ForClaim[i];
					ClaimProcsForClaim[i].Status=ClaimProcStatus.Received;
					if(ClaimProcsForClaim[i].DedApplied>0){
						ClaimProcsForClaim[i].InsPayEst+=ClaimProcsForClaim[i].DedApplied;
						ClaimProcsForClaim[i].DedApplied=0;//because ded will show as part of payment now.
					}
					ClaimProcsForClaim[i].Update();
				}
			}
			listClaimStatus.SelectedIndex=5;//Received
			if(textDateRec.Text==""){
				textDateRec.Text=DateTime.Today.ToShortDateString();
			}
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			FillGrids();
		}

		private void butPayProc_Click(object sender, System.EventArgs e) {
			//this will work for regular claims and for preauths.
			//it will enter edit mode if it can only find received procs not attached to payments yet.
			if(tbProc.SelectedIndices.Length==0){
				//first, autoselect rows if not received:
				for(int i=0;i<ClaimProcsForClaim.Length;i++){
					if(ClaimProcsForClaim[i].Status==ClaimProcStatus.NotReceived
						&& ClaimProcsForClaim[i].ProcNum>0){//and is procedure
						tbProc.SetSelected(i,true);
					}
				}
			}
			if(tbProc.SelectedIndices.Length==0){
				//then, autoselect rows if not payed on:
				for(int i=0;i<ClaimProcsForClaim.Length;i++){
					if(ClaimProcsForClaim[i].ClaimPaymentNum==0
						&& ClaimProcsForClaim[i].ProcNum>0){//and is procedure
						tbProc.SetSelected(i,true);
					}
				}
			}
			if(tbProc.SelectedIndices.Length==0){
				//if still no rows selected
				MessageBox.Show(Lan.g(this,"All procedures in the list have already been paid."));
				return;
			}
			bool allAreProcs=true;
			for(int i=0;i<tbProc.SelectedIndices.Length;i++){
				if(ClaimProcsForClaim[tbProc.SelectedIndices[i]].ProcNum==0)
					allAreProcs=false;
			}
			if(!allAreProcs){
				MessageBox.Show(Lan.g(this,"You can only select procedures."));
				return;
			}
			FormClaimPayTotal FormCPT=new FormClaimPayTotal(PatCur,FamCur,PlanList);
			FormCPT.ClaimProcsToEdit=new ClaimProc[tbProc.SelectedIndices.Length];
			for(int i=0;i<tbProc.SelectedIndices.Length;i++){
				//copy selected claimprocs to temporary array for editing.
				//no changes to the database will be made within that form.
				FormCPT.ClaimProcsToEdit[i]=ClaimProcsForClaim[tbProc.SelectedIndices[i]];
				if(Claims.Cur.ClaimType=="PreAuth"){
					FormCPT.ClaimProcsToEdit[i].Status=ClaimProcStatus.Preauth;
				}
				if(Claims.Cur.ClaimType=="Cap"){
					;//do nothing.  The claimprocstatus will remain Capitation.
				}
				else{
					FormCPT.ClaimProcsToEdit[i].Status=ClaimProcStatus.Received;
				}
				FormCPT.ClaimProcsToEdit[i].InsPayAmt=FormCPT.ClaimProcsToEdit[i].InsPayEst;
			}
			FormCPT.ShowDialog();
			if(FormCPT.DialogResult!=DialogResult.OK){
				return;
			}
			//save changes now
			for(int i=0;i<FormCPT.ClaimProcsToEdit.Length;i++){
				//ClaimProcs.Cur=
				FormCPT.ClaimProcsToEdit[i].Update();
				//ClaimProcs.UpdateCur();
			}
			listClaimStatus.SelectedIndex=5;//Received
			if(textDateRec.Text==""){
				textDateRec.Text=DateTime.Today.ToShortDateString();
			}
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			FillGrids();
		}

		private void butPaySupp_Click(object sender, System.EventArgs e) {
			if(tbProc.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"This is only for additional payments on procedures already marked received.  Please highlight procedures first."));
				return;
			}
			bool allAreRecd=true;
			for(int i=0;i<tbProc.SelectedIndices.Length;i++){
				if(ClaimProcsForClaim[tbProc.SelectedIndices[i]].Status!=ClaimProcStatus.Received)
					allAreRecd=false;
			}
			if(!allAreRecd){
				MessageBox.Show(Lan.g(this,"All selected procedures must be status received."));
				return;
			}
			for(int i=0;i<tbProc.SelectedIndices.Length;i++){
				ClaimProc ClaimProcCur=ClaimProcsForClaim[tbProc.SelectedIndices[i]];
				ClaimProcCur.FeeBilled=0;
				ClaimProcCur.ClaimPaymentNum=0;//no payment attached
				//claimprocnum will be overwritten
				ClaimProcCur.DedApplied=0;
				ClaimProcCur.InsPayAmt=0;
				ClaimProcCur.InsPayEst=0;
				ClaimProcCur.Remarks="";
				ClaimProcCur.Status=ClaimProcStatus.Supplemental;
				ClaimProcCur.WriteOff=0;
				ClaimProcCur.Insert();//this inserts a copy of the original with the changes as above.
			}
//fix: need to debug the recalculation feature to take this status into account.
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			FillGrids();
		}

		private void butCheckDelete_Click(object sender, System.EventArgs e) {
		
		}

		/// <summary>
		/// Creates insurance check
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void butCheckAdd_Click(object sender, System.EventArgs e) {
			if(tbProc.SelectedIndices.Length>0){
				if(MessageBox.Show(Lan.g(this,"All received payments for this claim will be grouped together in the Claim Payment window."),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;
				}
			}
			bool existsReceived=false;
			for(int i=0;i<ClaimProcsForClaim.Length;i++){
				if((ClaimProcsForClaim[i].Status==ClaimProcStatus.Received
					|| ClaimProcsForClaim[i].Status==ClaimProcStatus.Supplemental)
					&& ClaimProcsForClaim[i].InsPayAmt>0){
					existsReceived=true;
				}
			}
			if(!existsReceived){
				MessageBox.Show(Lan.g(this,"There are no valid received payments for this claim."));
				return;
			}
			int tempClaimNum=Claims.Cur.ClaimNum;
			FormClaimPayEdit FormCPE=new FormClaimPayEdit();
			FormCPE.IsNew=true;
			FormCPE.ShowDialog();//the new ClaimPayment is entirely created here
			Claims.Refresh(PatCur.PatNum);
			Claims.Cur=((Claim)Claims.HList[tempClaimNum]);
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			FillGrids();
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

		private void butSupp_Click(object sender, System.EventArgs e) {
			FormClaimSupplemental FormCS=new FormClaimSupplemental();
			FormCS.ShowDialog();
		}

		private void butSend_Click(object sender, System.EventArgs e) {
			if(!ClaimIsValid())
				return;
			listClaimStatus.SelectedIndex=2;//W
			textDateSent.Text=DateTime.Today.ToString("d");
			UpdateClaim();		
			DialogResult=DialogResult.OK;
		}

		private void ButPrint_Click(object sender, System.EventArgs e) {
			if(!ClaimIsValid())
				return;
			UpdateClaim();
			FormClaimPrint FormCP=new FormClaimPrint();
			printDialog2=new PrintDialog();
			printDialog2.PrinterSettings=new PrinterSettings();
			printDialog2.PrinterSettings.PrinterName=Computers.Cur.PrinterName;
			string printerName;
			if(printDialog2.ShowDialog()==DialogResult.OK)
				printerName=printDialog2.PrinterSettings.PrinterName;
			else return;
			FormCP.ThisPatNum=Claims.Cur.PatNum;
			FormCP.ThisClaimNum=Claims.Cur.ClaimNum;
			if(!FormCP.PrintImmediate(printerName)){
				MessageBox.Show(Lan.g(this,"Error printing."));
				return;
			}
			Claims.Cur.ClaimStatus="S";
			Claims.Cur.DateSent=DateTime.Today;
			Claims.UpdateCur();		
			DialogResult=DialogResult.OK;
		}

		private void butPreview_Click(object sender, System.EventArgs e) {
			if(!ClaimIsValid())
				return;
			UpdateClaim();
			FormClaimPrint FormCP=new FormClaimPrint();
			FormCP.ThisPatNum=Claims.Cur.PatNum;
			FormCP.ThisClaimNum=Claims.Cur.ClaimNum;
			FormCP.PrintImmediately=false;
			FormCP.ShowDialog();		
			Claims.Refresh(PatCur.PatNum); 
      ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			FillForm();		
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;//jump straight to Closing, where the claimprocs will be changed
				return;
			}
			if(!ClaimIsValid())
				return;
			UpdateClaim();
			bool paymentIsAttached=false;
			for(int i=0;i<ClaimProcsForClaim.Length;i++){
				if(ClaimProcsForClaim[i].ClaimPaymentNum>0){
					paymentIsAttached=true;
				}
			}
			if(paymentIsAttached){
				MessageBox.Show(Lan.g(this,"You cannot delete this claim while any insurance checks are attached.  You will have to detach all insurance checks first."));
				return;
			}
			if(Claims.Cur.ClaimStatus=="R"){
				MessageBox.Show(Lan.g(this,"You cannot delete this claim while status is Received.  You will have to change the status first."));
				return;
			}
      if(Claims.Cur.ClaimType=="PreAuth"){
				if(MessageBox.Show(Lan.g(this,"Delete PreAuthorization?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;
				}
			}
			else{
				if(MessageBox.Show(Lan.g(this,"Delete Claim?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;
				}
			}
			Procedure proc;
			if(Claims.Cur.ClaimType=="PreAuth"//all preauth claimprocs are just duplicates
				|| Claims.Cur.ClaimType=="Cap"){//all cap claimprocs are just duplicates
				for(int i=0;i<ClaimProcsForClaim.Length;i++){
					//ClaimProcs.Cur=ClaimProcs.ForClaim[i];
					ClaimProcsForClaim[i].Delete();
				}
			}
			else{//all other claim types use original estimate claimproc.
				for(int i=0;i<ClaimProcsForClaim.Length;i++){
					//ClaimProcs.Cur=ClaimProcs.ForClaim[i];
					if(ClaimProcsForClaim[i].Status==ClaimProcStatus.Supplemental//supplementals are duplicate
						|| ClaimProcsForClaim[i].ProcNum==0)//total payments get deleted
					{
						ClaimProcsForClaim[i].Delete();
						continue;
					}
					//so only changed back to estimate if attached to a proc
					ClaimProcsForClaim[i].Status=ClaimProcStatus.Estimate;
					ClaimProcsForClaim[i].ClaimNum=0;
					proc=Procedures.GetProc(ProcList,ClaimProcsForClaim[i].ProcNum);
					if(Claims.Cur.ClaimType=="P"){
						ClaimProcsForClaim[i].ComputeBaseEst(proc,PriSecTot.Pri,PatCur,PlanList);
					}
					else if(Claims.Cur.ClaimType=="S"){
						ClaimProcsForClaim[i].ComputeBaseEst(proc,PriSecTot.Sec,PatCur,PlanList);
					}
					ClaimProcsForClaim[i].Update();
				}
			}
      Claims.DeleteCur();
      DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(!ClaimIsValid())
				return;
			//if status is received, all claimprocs must also be received.
			if(listClaimStatus.SelectedIndex==5){
				bool allReceived=true;
				for(int i=0;i<ClaimProcsForClaim.Length;i++){
					if(((ClaimProc)ClaimProcsForClaim[i]).Status==ClaimProcStatus.NotReceived){
						allReceived=false;
					}
				}
				if(!allReceived){
					if(MessageBox.Show(Lan.g(this,"All items will be marked received.  Continue?")
						,"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
					return;
					for(int i=0;i<ClaimProcsForClaim.Length;i++){
						if(ClaimProcsForClaim[i].Status==ClaimProcStatus.NotReceived){
							//ClaimProcs.Cur=(ClaimProc)ClaimProcs.ForClaim[i];
							ClaimProcsForClaim[i].Status=ClaimProcStatus.Received;
							ClaimProcsForClaim[i].Update();
						}
					}
				}
			}
			//if status is received and there is no received date
			if(listClaimStatus.SelectedIndex==5 && textDateRec.Text==""){
				textDateRec.Text=DateTime.Today.ToShortDateString();
			}
			UpdateClaim();
			DialogResult=DialogResult.OK;
		}
		
		private bool ClaimIsValid(){
			if(  textDateSent.errorProvider1.GetError(textDateSent)!=""
				|| textDateRec.errorProvider1.GetError(textDateRec)!=""
				|| textPriorDate.errorProvider1.GetError(textPriorDate)!=""
				|| textDedApplied.errorProvider1.GetError(textDedApplied)!=""
				|| textInsPayAmt.errorProvider1.GetError(textInsPayAmt)!=""
				|| textOrthoDate.errorProvider1.GetError(textOrthoDate)!=""
				|| textRadiographs.errorProvider1.GetError(textRadiographs)!=""
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
			switch(listClaimStatus.SelectedIndex){
				case 0:
					Claims.Cur.ClaimStatus="U";
					break;
				case 1:
					Claims.Cur.ClaimStatus="H";
					break;
				case 2:
					Claims.Cur.ClaimStatus="W";
					break;
				case 3:
					Claims.Cur.ClaimStatus="P";
					break;
				case 4:
					Claims.Cur.ClaimStatus="S";
					break;
				case 5:
					Claims.Cur.ClaimStatus="R";
					break;
			}
			if(textDateRec.Text=="")
				Claims.Cur.DateReceived=DateTime.MinValue;
			else Claims.Cur.DateReceived=PIn.PDate(textDateRec.Text);
			//planNum
			//patRelats will always be selected
			Claims.Cur.PatRelat=(Relat)comboPatRelat.SelectedIndex;
			Claims.Cur.PatRelat2=(Relat)comboPatRelat.SelectedIndex;
			if(comboProvTreat.SelectedIndex!=-1)
				Claims.Cur.ProvTreat=Providers.List[comboProvTreat.SelectedIndex].ProvNum;
			Claims.Cur.PreAuthString=textPreAuth.Text;
			//isprosthesis handled earlier
			Claims.Cur.PriorDate=PIn.PDate(textPriorDate.Text);
			Claims.Cur.ReasonUnderPaid=textReasonUnder.Text;
			Claims.Cur.ClaimNote=textNote.Text;
			//ispreauth
			if(comboProvBill.SelectedIndex!=-1)
				Claims.Cur.ProvBill=Providers.List[comboProvBill.SelectedIndex].ProvNum;
			Claims.Cur.IsOrtho=checkIsOrtho.Checked;
			Claims.Cur.OrthoRemainM=PIn.PInt(textOrthoRemainM.Text);
			Claims.Cur.OrthoDate=PIn.PDate(textOrthoDate.Text);
			Claims.Cur.Radiographs=PIn.PInt(textRadiographs.Text);
			Claims.UpdateCur();
			if(Claims.Cur.ClaimStatus=="S"){
				SecurityLogs.MakeLogEntry("Claims Sent Edit",Claims.cmd.CommandText);
			}
		}

		//cancel does not cancel in some circumstances because cur gets updated in some areas.
		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormClaimEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNew){
				for(int i=0;i<ClaimProcsForClaim.Length;i++){
					if(ClaimProcsForClaim[i].Status==ClaimProcStatus.CapClaim){
						ClaimProcsForClaim[i].Delete();
					}
					else if(ClaimProcsForClaim[i].Status==ClaimProcStatus.NotReceived){
						ClaimProcsForClaim[i].Status=ClaimProcStatus.Estimate;
						ClaimProcsForClaim[i].ClaimNum=0;
						ClaimProcsForClaim[i].Update();
					}
				}
				Claims.DeleteCur();
			}
		}

		

		

		

		

		

		

		

		

		

		

	}
}
