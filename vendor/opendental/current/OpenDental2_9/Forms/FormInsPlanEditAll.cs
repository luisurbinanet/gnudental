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
	public class FormInsPlanEditAll : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.TextBox textCarrier;
		private System.Windows.Forms.TextBox textAddress;
		private System.Windows.Forms.TextBox textCity;
		private System.Windows.Forms.TextBox textZip;
		private System.Windows.Forms.TextBox textPhone;
		private System.Windows.Forms.TextBox textElectID;
		private System.ComponentModel.Container components = null;// Required designer variable.
		private System.Windows.Forms.TextBox textAddress2;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.CheckBox checkNoSendElect;
		private System.Windows.Forms.ListBox listPlanType;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.ListBox listClaimForm;
		private System.Windows.Forms.CheckBox checkClaimsUseUCR;
		private System.Windows.Forms.CheckBox checkAlternateCode;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ListBox listFeeSched;
		private System.Windows.Forms.Button butCopayNone;
		private System.Windows.Forms.ListBox listCopay;
		private System.Windows.Forms.TextBox textGroupNum;
		private System.Windows.Forms.TextBox textGroupName;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Button butChangeEmp;
		private System.Windows.Forms.TextBox textEmployer;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label labelCopayFee;
		private System.Windows.Forms.Label labelCopayAdvice;
		private System.Windows.Forms.Button butChangeCarrier;
		private System.Windows.Forms.TextBox textState;
		private System.Windows.Forms.ComboBox comboLinked;
		private System.Windows.Forms.TextBox textLinkedNum;
		private System.Windows.Forms.Button butEdit;
		///<summary>Keeps track of the original settings of the plan before any changes are made.  That way, the update can be applied to all plans that are the same as the original.</summary>
		private InsPlan originalPlan;

		///<summary></summary>
		public FormInsPlanEditAll(){
			InitializeComponent();// Required for Windows Form Designer support
			Lan.C(this, new System.Windows.Forms.Control[]
			{
				label1,
				label2,
				label3,
				label4,
				label6,
				label7,
				checkNoSendElect,
				checkAlternateCode,
				checkClaimsUseUCR,
				label9,
				label10,
				label11,
				label12,
				label14,
				label16,
				label23,
				labelCopayAdvice,
				labelCopayFee,
				butCopayNone
				/*				
				this.radioDedNo,
				this.radioDedYes,
				this.radioDedUnkn,
				this.radioMissNo,
				this.radioMissUnkn,
				this.radioMissYes,
				this.radioWaitNo,
				this.radioWaitUnkn,
				this.radioWaitYes,
				groupBox1,
				checkRelease,
				checkAssign*/
			});
			Lan.C("All", new System.Windows.Forms.Control[] 
			{
				butOK,
				butCancel,
			});
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
			this.label3 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.textCarrier = new System.Windows.Forms.TextBox();
			this.textAddress = new System.Windows.Forms.TextBox();
			this.textCity = new System.Windows.Forms.TextBox();
			this.textState = new System.Windows.Forms.TextBox();
			this.textZip = new System.Windows.Forms.TextBox();
			this.textPhone = new System.Windows.Forms.TextBox();
			this.textElectID = new System.Windows.Forms.TextBox();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.textAddress2 = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.checkNoSendElect = new System.Windows.Forms.CheckBox();
			this.listPlanType = new System.Windows.Forms.ListBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.listClaimForm = new System.Windows.Forms.ListBox();
			this.checkClaimsUseUCR = new System.Windows.Forms.CheckBox();
			this.checkAlternateCode = new System.Windows.Forms.CheckBox();
			this.label7 = new System.Windows.Forms.Label();
			this.listFeeSched = new System.Windows.Forms.ListBox();
			this.butCopayNone = new System.Windows.Forms.Button();
			this.listCopay = new System.Windows.Forms.ListBox();
			this.labelCopayFee = new System.Windows.Forms.Label();
			this.textGroupNum = new System.Windows.Forms.TextBox();
			this.textGroupName = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.butChangeEmp = new System.Windows.Forms.Button();
			this.textEmployer = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.labelCopayAdvice = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.butEdit = new System.Windows.Forms.Button();
			this.butChangeCarrier = new System.Windows.Forms.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.comboLinked = new System.Windows.Forms.ComboBox();
			this.textLinkedNum = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox2.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 80);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Carrier";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(7, 118);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 15);
			this.label2.TabIndex = 1;
			this.label2.Text = "Address";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(7, 158);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 15);
			this.label3.TabIndex = 2;
			this.label3.Text = "City";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(7, 98);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 15);
			this.label6.TabIndex = 5;
			this.label6.Text = "Phone";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(7, 178);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(100, 15);
			this.label9.TabIndex = 8;
			this.label9.Text = "ElectID";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCarrier
			// 
			this.textCarrier.Location = new System.Drawing.Point(110, 76);
			this.textCarrier.MaxLength = 255;
			this.textCarrier.Name = "textCarrier";
			this.textCarrier.ReadOnly = true;
			this.textCarrier.Size = new System.Drawing.Size(230, 20);
			this.textCarrier.TabIndex = 0;
			this.textCarrier.Text = "";
			// 
			// textAddress
			// 
			this.textAddress.Location = new System.Drawing.Point(110, 116);
			this.textAddress.MaxLength = 255;
			this.textAddress.Name = "textAddress";
			this.textAddress.ReadOnly = true;
			this.textAddress.Size = new System.Drawing.Size(291, 20);
			this.textAddress.TabIndex = 1;
			this.textAddress.Text = "";
			// 
			// textCity
			// 
			this.textCity.Location = new System.Drawing.Point(110, 156);
			this.textCity.MaxLength = 255;
			this.textCity.Name = "textCity";
			this.textCity.ReadOnly = true;
			this.textCity.Size = new System.Drawing.Size(155, 20);
			this.textCity.TabIndex = 3;
			this.textCity.Text = "";
			// 
			// textState
			// 
			this.textState.Location = new System.Drawing.Point(265, 156);
			this.textState.MaxLength = 255;
			this.textState.Name = "textState";
			this.textState.ReadOnly = true;
			this.textState.Size = new System.Drawing.Size(65, 20);
			this.textState.TabIndex = 4;
			this.textState.Text = "";
			// 
			// textZip
			// 
			this.textZip.Location = new System.Drawing.Point(330, 156);
			this.textZip.MaxLength = 255;
			this.textZip.Name = "textZip";
			this.textZip.ReadOnly = true;
			this.textZip.Size = new System.Drawing.Size(71, 20);
			this.textZip.TabIndex = 5;
			this.textZip.Text = "";
			// 
			// textPhone
			// 
			this.textPhone.Location = new System.Drawing.Point(110, 96);
			this.textPhone.MaxLength = 255;
			this.textPhone.Name = "textPhone";
			this.textPhone.ReadOnly = true;
			this.textPhone.Size = new System.Drawing.Size(148, 20);
			this.textPhone.TabIndex = 6;
			this.textPhone.Text = "";
			this.textPhone.TextChanged += new System.EventHandler(this.textPhone_TextChanged);
			// 
			// textElectID
			// 
			this.textElectID.Location = new System.Drawing.Point(110, 176);
			this.textElectID.MaxLength = 255;
			this.textElectID.Name = "textElectID";
			this.textElectID.ReadOnly = true;
			this.textElectID.Size = new System.Drawing.Size(44, 20);
			this.textElectID.TabIndex = 5;
			this.textElectID.Text = "";
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(853, 594);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(853, 630);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textAddress2
			// 
			this.textAddress2.Location = new System.Drawing.Point(110, 136);
			this.textAddress2.MaxLength = 255;
			this.textAddress2.Name = "textAddress2";
			this.textAddress2.ReadOnly = true;
			this.textAddress2.Size = new System.Drawing.Size(291, 20);
			this.textAddress2.TabIndex = 2;
			this.textAddress2.Text = "";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(7, 138);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(100, 15);
			this.label10.TabIndex = 21;
			this.label10.Text = "Address2";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkNoSendElect
			// 
			this.checkNoSendElect.AutoCheck = false;
			this.checkNoSendElect.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNoSendElect.Location = new System.Drawing.Point(160, 176);
			this.checkNoSendElect.Name = "checkNoSendElect";
			this.checkNoSendElect.Size = new System.Drawing.Size(206, 19);
			this.checkNoSendElect.TabIndex = 6;
			this.checkNoSendElect.Text = "Don\'t Usually Send Electronically";
			// 
			// listPlanType
			// 
			this.listPlanType.Location = new System.Drawing.Point(110, 196);
			this.listPlanType.Name = "listPlanType";
			this.listPlanType.Size = new System.Drawing.Size(120, 43);
			this.listPlanType.TabIndex = 7;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(7, 196);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(100, 15);
			this.label14.TabIndex = 95;
			this.label14.Text = "Plan Type";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(258, 278);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(121, 12);
			this.label23.TabIndex = 98;
			this.label23.Text = "Claim Form";
			// 
			// listClaimForm
			// 
			this.listClaimForm.Location = new System.Drawing.Point(259, 296);
			this.listClaimForm.Name = "listClaimForm";
			this.listClaimForm.Size = new System.Drawing.Size(147, 82);
			this.listClaimForm.TabIndex = 12;
			// 
			// checkClaimsUseUCR
			// 
			this.checkClaimsUseUCR.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkClaimsUseUCR.Location = new System.Drawing.Point(110, 257);
			this.checkClaimsUseUCR.Name = "checkClaimsUseUCR";
			this.checkClaimsUseUCR.Size = new System.Drawing.Size(286, 17);
			this.checkClaimsUseUCR.TabIndex = 9;
			this.checkClaimsUseUCR.Text = "Claims show UCR, not billed fee";
			// 
			// checkAlternateCode
			// 
			this.checkAlternateCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAlternateCode.Location = new System.Drawing.Point(110, 240);
			this.checkAlternateCode.Name = "checkAlternateCode";
			this.checkAlternateCode.Size = new System.Drawing.Size(285, 17);
			this.checkAlternateCode.TabIndex = 8;
			this.checkAlternateCode.Text = "Use Alternate Code (for some Medicaid plans)";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(23, 278);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(110, 12);
			this.label7.TabIndex = 102;
			this.label7.Text = "Fee Schedule";
			// 
			// listFeeSched
			// 
			this.listFeeSched.Location = new System.Drawing.Point(25, 296);
			this.listFeeSched.Name = "listFeeSched";
			this.listFeeSched.Size = new System.Drawing.Size(108, 82);
			this.listFeeSched.TabIndex = 10;
			// 
			// butCopayNone
			// 
			this.butCopayNone.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCopayNone.Location = new System.Drawing.Point(135, 380);
			this.butCopayNone.Name = "butCopayNone";
			this.butCopayNone.TabIndex = 108;
			this.butCopayNone.Text = "&None";
			this.butCopayNone.Click += new System.EventHandler(this.butCopayNone_Click);
			// 
			// listCopay
			// 
			this.listCopay.Location = new System.Drawing.Point(137, 296);
			this.listCopay.Name = "listCopay";
			this.listCopay.Size = new System.Drawing.Size(108, 82);
			this.listCopay.TabIndex = 11;
			// 
			// labelCopayFee
			// 
			this.labelCopayFee.Location = new System.Drawing.Point(135, 278);
			this.labelCopayFee.Name = "labelCopayFee";
			this.labelCopayFee.Size = new System.Drawing.Size(121, 17);
			this.labelCopayFee.TabIndex = 107;
			this.labelCopayFee.Text = "Co-pay Fee Schedule ";
			// 
			// textGroupNum
			// 
			this.textGroupNum.Location = new System.Drawing.Point(110, 56);
			this.textGroupNum.MaxLength = 20;
			this.textGroupNum.Name = "textGroupNum";
			this.textGroupNum.Size = new System.Drawing.Size(129, 20);
			this.textGroupNum.TabIndex = 2;
			this.textGroupNum.Text = "";
			// 
			// textGroupName
			// 
			this.textGroupName.Location = new System.Drawing.Point(110, 36);
			this.textGroupName.MaxLength = 50;
			this.textGroupName.Name = "textGroupName";
			this.textGroupName.Size = new System.Drawing.Size(193, 20);
			this.textGroupName.TabIndex = 1;
			this.textGroupName.Text = "";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(7, 58);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(100, 15);
			this.label11.TabIndex = 112;
			this.label11.Text = "Group Num";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(7, 38);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(100, 15);
			this.label12.TabIndex = 111;
			this.label12.Text = "Group Name";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butChangeEmp
			// 
			this.butChangeEmp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butChangeEmp.Location = new System.Drawing.Point(340, 15);
			this.butChangeEmp.Name = "butChangeEmp";
			this.butChangeEmp.Size = new System.Drawing.Size(75, 22);
			this.butChangeEmp.TabIndex = 0;
			this.butChangeEmp.Text = "Change";
			this.butChangeEmp.Click += new System.EventHandler(this.butChangeEmp_Click);
			// 
			// textEmployer
			// 
			this.textEmployer.Location = new System.Drawing.Point(110, 16);
			this.textEmployer.MaxLength = 40;
			this.textEmployer.Name = "textEmployer";
			this.textEmployer.ReadOnly = true;
			this.textEmployer.Size = new System.Drawing.Size(231, 20);
			this.textEmployer.TabIndex = 117;
			this.textEmployer.Text = "";
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(7, 18);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(100, 15);
			this.label16.TabIndex = 118;
			this.label16.Text = "Employer";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelCopayAdvice
			// 
			this.labelCopayAdvice.Location = new System.Drawing.Point(210, 380);
			this.labelCopayAdvice.Name = "labelCopayAdvice";
			this.labelCopayAdvice.Size = new System.Drawing.Size(206, 40);
			this.labelCopayAdvice.TabIndex = 120;
			this.labelCopayAdvice.Text = "To indicate 100% coverage, set plan type to flat co-pay, and do not select a co-p" +
				"ay fee schedule.";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.butEdit);
			this.groupBox2.Controls.Add(this.butChangeCarrier);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.textAddress);
			this.groupBox2.Controls.Add(this.textCity);
			this.groupBox2.Controls.Add(this.textState);
			this.groupBox2.Controls.Add(this.textZip);
			this.groupBox2.Controls.Add(this.textPhone);
			this.groupBox2.Controls.Add(this.textElectID);
			this.groupBox2.Controls.Add(this.textAddress2);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.checkNoSendElect);
			this.groupBox2.Controls.Add(this.listPlanType);
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Controls.Add(this.label23);
			this.groupBox2.Controls.Add(this.listClaimForm);
			this.groupBox2.Controls.Add(this.checkClaimsUseUCR);
			this.groupBox2.Controls.Add(this.checkAlternateCode);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.listFeeSched);
			this.groupBox2.Controls.Add(this.butCopayNone);
			this.groupBox2.Controls.Add(this.listCopay);
			this.groupBox2.Controls.Add(this.labelCopayFee);
			this.groupBox2.Controls.Add(this.textGroupNum);
			this.groupBox2.Controls.Add(this.textGroupName);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.butChangeEmp);
			this.groupBox2.Controls.Add(this.textEmployer);
			this.groupBox2.Controls.Add(this.label16);
			this.groupBox2.Controls.Add(this.labelCopayAdvice);
			this.groupBox2.Controls.Add(this.textCarrier);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(7, 4);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(424, 424);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Synchronized Information";
			// 
			// butEdit
			// 
			this.butEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butEdit.Location = new System.Drawing.Point(340, 95);
			this.butEdit.Name = "butEdit";
			this.butEdit.Size = new System.Drawing.Size(75, 22);
			this.butEdit.TabIndex = 4;
			this.butEdit.Text = "Edit";
			this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
			// 
			// butChangeCarrier
			// 
			this.butChangeCarrier.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butChangeCarrier.Location = new System.Drawing.Point(340, 73);
			this.butChangeCarrier.Name = "butChangeCarrier";
			this.butChangeCarrier.Size = new System.Drawing.Size(75, 22);
			this.butChangeCarrier.TabIndex = 3;
			this.butChangeCarrier.Text = "Change";
			this.butChangeCarrier.Click += new System.EventHandler(this.butChangeCarrier_Click);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.comboLinked);
			this.groupBox4.Controls.Add(this.textLinkedNum);
			this.groupBox4.Controls.Add(this.label4);
			this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox4.Location = new System.Drawing.Point(461, 5);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(466, 70);
			this.groupBox4.TabIndex = 145;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Synchronization";
			// 
			// comboLinked
			// 
			this.comboLinked.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboLinked.Location = new System.Drawing.Point(234, 21);
			this.comboLinked.MaxDropDownItems = 30;
			this.comboLinked.Name = "comboLinked";
			this.comboLinked.Size = new System.Drawing.Size(197, 21);
			this.comboLinked.TabIndex = 65;
			// 
			// textLinkedNum
			// 
			this.textLinkedNum.BackColor = System.Drawing.Color.White;
			this.textLinkedNum.Location = new System.Drawing.Point(195, 21);
			this.textLinkedNum.Name = "textLinkedNum";
			this.textLinkedNum.ReadOnly = true;
			this.textLinkedNum.Size = new System.Drawing.Size(35, 20);
			this.textLinkedNum.TabIndex = 64;
			this.textLinkedNum.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(15, 23);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(177, 17);
			this.label4.TabIndex = 63;
			this.label4.Text = "These plans are the same:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormInsPlanEditAll
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(961, 682);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormInsPlanEditAll";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Insurance Plan For All";
			this.Load += new System.EventHandler(this.FormInsPlanEditAll_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormInsPlanEditAll_Load(object sender, System.EventArgs e) {
			originalPlan=InsPlans.Cur;
			if(((Pref)Prefs.HList["EasyHideCapitation"]).ValueString=="1"){
				labelCopayFee.Visible=false;
				listCopay.Visible=false;
				butCopayNone.Visible=false;
				labelCopayAdvice.Visible=false;
			}
			if(((Pref)Prefs.HList["EasyHideCapitation"]).ValueString=="1"){
				checkAlternateCode.Visible=false;
			}
			textEmployer.Text=Employers.GetName(InsPlans.Cur.EmployerNum);
			textGroupName.Text=InsPlans.Cur.GroupName;
			textGroupNum.Text=InsPlans.Cur.GroupNum;
			listPlanType.Items.Add(Lan.g(this,"Category Percentage"));
			if(InsPlans.Cur.PlanType=="")
				listPlanType.SelectedIndex=0;
			listPlanType.Items.Add(Lan.g(this,"Flat Co-pay"));
			if(InsPlans.Cur.PlanType=="f")
				listPlanType.SelectedIndex=1;
			if(((Pref)Prefs.HList["EasyHideCapitation"]).ValueString!="1"){
				listPlanType.Items.Add(Lan.g(this,"Capitation"));
				if(InsPlans.Cur.PlanType=="c")
					listPlanType.SelectedIndex=2;
			}
			checkAlternateCode.Checked=InsPlans.Cur.UseAltCode;
			checkClaimsUseUCR.Checked=InsPlans.Cur.ClaimsUseUCR;
			for(int i=0;i<Defs.Short[(int)DefCat.FeeSchedNames].Length;i++){
				listFeeSched.Items.Add(Defs.Short[(int)DefCat.FeeSchedNames][i].ItemName);
				if(Defs.Short[(int)DefCat.FeeSchedNames][i].DefNum==InsPlans.Cur.FeeSched)
					listFeeSched.SelectedIndex=i;
			}
			for(int i=0;i<Defs.Short[(int)DefCat.FeeSchedNames].Length;i++){
				listCopay.Items.Add(Defs.Short[(int)DefCat.FeeSchedNames][i].ItemName);
				if(Defs.Short[(int)DefCat.FeeSchedNames][i].DefNum==InsPlans.Cur.CopayFeeSched)
					listCopay.SelectedIndex=i;
			}
			for(int i=0;i<ClaimForms.ListShort.Length;i++){
				listClaimForm.Items.Add(ClaimForms.ListShort[i].Description);
				if(ClaimForms.ListShort[i].ClaimFormNum==InsPlans.Cur.ClaimFormNum){
					listClaimForm.SelectedIndex=i;
				}
			}
			if(listClaimForm.SelectedIndex==-1){
				listClaimForm.SelectedIndex=0;//this will let the user rearrange the default later
			}
			//textNote.Text=InsPlans.Cur.Note;
//need entire right column
			
			FillCarrier();
			LayoutSynch();
		}

		private void FillCarrier(){
			Carriers.GetCur(InsPlans.Cur.CarrierNum);
			textCarrier.Text=Carriers.Cur.CarrierName;
			textPhone.Text=Carriers.Cur.Phone;
			textAddress.Text=Carriers.Cur.Address;
			textAddress2.Text=Carriers.Cur.Address2;
			textCity.Text=Carriers.Cur.City;
			textState.Text=Carriers.Cur.State;
			textZip.Text=Carriers.Cur.Zip;
			textElectID.Text=Carriers.Cur.ElectID;
			checkNoSendElect.Checked=Carriers.Cur.NoSendElect;
		}

		private void textPhone_TextChanged(object sender, System.EventArgs e) {
  		textPhone.Text=TelephoneNumbers.AutoFormat(textPhone.Text);
			textPhone.SelectionStart=textPhone.Text.Length;		
		}

		private void butChangeEmp_Click(object sender, System.EventArgs e) {
			FormEmployers FormE=new FormEmployers();
			FormE.IsSelectMode=true;
			Employers.Cur=Employers.GetEmployer(InsPlans.Cur.EmployerNum);
			FormE.ShowDialog();
			if(FormE.DialogResult!=DialogResult.OK){
				return;
			}
			InsPlans.Cur.EmployerNum=Employers.Cur.EmployerNum;
			textEmployer.Text=Employers.Cur.EmpName;
		}

		private void butChangeCarrier_Click(object sender, System.EventArgs e) {
			FormCarriers FormC=new FormCarriers();
			FormC.IsSelectMode=true;
			FormC.ShowDialog();
			if(FormC.DialogResult!=DialogResult.OK){
				return;
			}
			InsPlans.Cur.CarrierNum=Carriers.Cur.CarrierNum;
			FillCarrier();
		}

		private void butEdit_Click(object sender, System.EventArgs e) {
			//Carriers.Cur was already set.
			FormCarrierEdit FormCE=new FormCarrierEdit();
			FormCE.ShowDialog();
			if(FormCE.DialogResult!=DialogResult.OK){
				return;
			}
			Carriers.Refresh();
			FillCarrier();
		}

		private void butCopayNone_Click(object sender, System.EventArgs e) {
			listCopay.SelectedIndex=-1;
		}

		private void LayoutSynch(){
			string[] samePlans=InsPlans.SamePlans();
			textLinkedNum.Text=samePlans.Length.ToString();
			comboLinked.Items.Clear();
			for(int i=0;i<samePlans.Length;i++){
				comboLinked.Items.Add(samePlans[i]);
			}
			if(samePlans.Length>0)
				comboLinked.SelectedIndex=0;
		}
		
		private void butOK_Click(object sender, System.EventArgs e) {
			//Employer already handled
			InsPlans.Cur.GroupName=textGroupName.Text;
			InsPlans.Cur.GroupNum=textGroupNum.Text;
			//Carrier already handled
			switch(listPlanType.SelectedIndex){
				case 0:
					InsPlans.Cur.PlanType="";
					break;
				case 1:
					InsPlans.Cur.PlanType="f";
					break;
				case 2:
					InsPlans.Cur.PlanType="c";
					break;
			}
			InsPlans.Cur.UseAltCode=checkAlternateCode.Checked;
			InsPlans.Cur.ClaimsUseUCR=checkClaimsUseUCR.Checked;
			if(listFeeSched.SelectedIndex==-1)
				InsPlans.Cur.FeeSched=0;
			else
				InsPlans.Cur.FeeSched=Defs.Short[(int)DefCat.FeeSchedNames][listFeeSched.SelectedIndex].DefNum;
			if(listCopay.SelectedIndex==-1)
				InsPlans.Cur.CopayFeeSched=0;
			else
				InsPlans.Cur.CopayFeeSched
					=Defs.Short[(int)DefCat.FeeSchedNames][listCopay.SelectedIndex].DefNum;
			InsPlans.Cur.ClaimFormNum=ClaimForms.ListShort[listClaimForm.SelectedIndex].ClaimFormNum;
			InsPlans.UpdateForLike(originalPlan);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			//warning. Don't delete if IsNew, because of the copy function in FormInsTemplates
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		
		
	}
}
