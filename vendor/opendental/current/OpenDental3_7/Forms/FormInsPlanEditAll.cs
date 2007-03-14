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
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label9;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
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
		private System.Windows.Forms.CheckBox checkClaimsUseUCR;
		private System.Windows.Forms.CheckBox checkAlternateCode;
		private System.Windows.Forms.TextBox textGroupNum;
		private System.Windows.Forms.TextBox textGroupName;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private OpenDental.UI.Button butChangeEmp;
		private System.Windows.Forms.TextBox textEmployer;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label4;
		private OpenDental.UI.Button butChangeCarrier;
		private System.Windows.Forms.TextBox textState;
		private System.Windows.Forms.ComboBox comboLinked;
		private System.Windows.Forms.TextBox textLinkedNum;
		private OpenDental.UI.Button butEdit;
		///<summary>Keeps track of the original settings of the plan before any changes are made.  That way, the update can be applied to all plans that are the same as the original.</summary>
		private InsPlan OriginalPlan;
		///<summary>Might be set to 0 if no relevant current patient.  Maybe need to get rid of this field.</summary>
		private int PatNum;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox comboClaimForm;
		private System.Windows.Forms.ComboBox comboFeeSched;
		private System.Windows.Forms.GroupBox groupCoPay;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboAllowedFeeSched;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox comboCopay;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label15;
		private InsPlan PlanCur;

		///<summary></summary>
		public FormInsPlanEditAll(InsPlan originalPlan,int patNum){
			OriginalPlan=originalPlan;
			PatNum=patNum;
			InitializeComponent();// Required for Windows Form Designer support
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
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textAddress2 = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.checkNoSendElect = new System.Windows.Forms.CheckBox();
			this.listPlanType = new System.Windows.Forms.ListBox();
			this.label14 = new System.Windows.Forms.Label();
			this.checkClaimsUseUCR = new System.Windows.Forms.CheckBox();
			this.checkAlternateCode = new System.Windows.Forms.CheckBox();
			this.textGroupNum = new System.Windows.Forms.TextBox();
			this.textGroupName = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.butChangeEmp = new OpenDental.UI.Button();
			this.textEmployer = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.butEdit = new OpenDental.UI.Button();
			this.butChangeCarrier = new OpenDental.UI.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.comboLinked = new System.Windows.Forms.ComboBox();
			this.textLinkedNum = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.comboClaimForm = new System.Windows.Forms.ComboBox();
			this.comboFeeSched = new System.Windows.Forms.ComboBox();
			this.groupCoPay = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.comboAllowedFeeSched = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.comboCopay = new System.Windows.Forms.ComboBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.groupBox2.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupCoPay.SuspendLayout();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(3, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 15);
			this.label2.TabIndex = 1;
			this.label2.Text = "Address";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(3, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 15);
			this.label3.TabIndex = 2;
			this.label3.Text = "City";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(3, 28);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 15);
			this.label6.TabIndex = 5;
			this.label6.Text = "Phone";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(3, 108);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(100, 15);
			this.label9.TabIndex = 8;
			this.label9.Text = "ElectID";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCarrier
			// 
			this.textCarrier.Location = new System.Drawing.Point(108, 8);
			this.textCarrier.MaxLength = 255;
			this.textCarrier.Name = "textCarrier";
			this.textCarrier.ReadOnly = true;
			this.textCarrier.Size = new System.Drawing.Size(230, 20);
			this.textCarrier.TabIndex = 0;
			this.textCarrier.Text = "";
			// 
			// textAddress
			// 
			this.textAddress.Location = new System.Drawing.Point(108, 48);
			this.textAddress.MaxLength = 255;
			this.textAddress.Name = "textAddress";
			this.textAddress.ReadOnly = true;
			this.textAddress.Size = new System.Drawing.Size(291, 20);
			this.textAddress.TabIndex = 1;
			this.textAddress.Text = "";
			// 
			// textCity
			// 
			this.textCity.Location = new System.Drawing.Point(108, 88);
			this.textCity.MaxLength = 255;
			this.textCity.Name = "textCity";
			this.textCity.ReadOnly = true;
			this.textCity.Size = new System.Drawing.Size(155, 20);
			this.textCity.TabIndex = 3;
			this.textCity.Text = "";
			// 
			// textState
			// 
			this.textState.Location = new System.Drawing.Point(263, 88);
			this.textState.MaxLength = 255;
			this.textState.Name = "textState";
			this.textState.ReadOnly = true;
			this.textState.Size = new System.Drawing.Size(65, 20);
			this.textState.TabIndex = 4;
			this.textState.Text = "";
			// 
			// textZip
			// 
			this.textZip.Location = new System.Drawing.Point(328, 88);
			this.textZip.MaxLength = 255;
			this.textZip.Name = "textZip";
			this.textZip.ReadOnly = true;
			this.textZip.Size = new System.Drawing.Size(71, 20);
			this.textZip.TabIndex = 5;
			this.textZip.Text = "";
			// 
			// textPhone
			// 
			this.textPhone.Location = new System.Drawing.Point(108, 28);
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
			this.textElectID.Location = new System.Drawing.Point(108, 108);
			this.textElectID.MaxLength = 255;
			this.textElectID.Name = "textElectID";
			this.textElectID.ReadOnly = true;
			this.textElectID.Size = new System.Drawing.Size(44, 20);
			this.textElectID.TabIndex = 5;
			this.textElectID.Text = "";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(853, 594);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 25);
			this.butOK.TabIndex = 1;
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
			this.butCancel.Location = new System.Drawing.Point(853, 630);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 25);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textAddress2
			// 
			this.textAddress2.Location = new System.Drawing.Point(108, 68);
			this.textAddress2.MaxLength = 255;
			this.textAddress2.Name = "textAddress2";
			this.textAddress2.ReadOnly = true;
			this.textAddress2.Size = new System.Drawing.Size(291, 20);
			this.textAddress2.TabIndex = 2;
			this.textAddress2.Text = "";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(3, 68);
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
			this.checkNoSendElect.Location = new System.Drawing.Point(156, 106);
			this.checkNoSendElect.Name = "checkNoSendElect";
			this.checkNoSendElect.Size = new System.Drawing.Size(206, 19);
			this.checkNoSendElect.TabIndex = 6;
			this.checkNoSendElect.Text = "Don\'t Usually Send Electronically";
			// 
			// listPlanType
			// 
			this.listPlanType.Location = new System.Drawing.Point(110, 228);
			this.listPlanType.Name = "listPlanType";
			this.listPlanType.Size = new System.Drawing.Size(120, 43);
			this.listPlanType.TabIndex = 7;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(7, 228);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(100, 15);
			this.label14.TabIndex = 95;
			this.label14.Text = "Plan Type";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkClaimsUseUCR
			// 
			this.checkClaimsUseUCR.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkClaimsUseUCR.Location = new System.Drawing.Point(110, 289);
			this.checkClaimsUseUCR.Name = "checkClaimsUseUCR";
			this.checkClaimsUseUCR.Size = new System.Drawing.Size(286, 17);
			this.checkClaimsUseUCR.TabIndex = 9;
			this.checkClaimsUseUCR.Text = "Claims show UCR, not billed fee";
			// 
			// checkAlternateCode
			// 
			this.checkAlternateCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAlternateCode.Location = new System.Drawing.Point(110, 272);
			this.checkAlternateCode.Name = "checkAlternateCode";
			this.checkAlternateCode.Size = new System.Drawing.Size(285, 17);
			this.checkAlternateCode.TabIndex = 8;
			this.checkAlternateCode.Text = "Use Alternate Code (for some Medicaid plans)";
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
			this.butChangeEmp.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butChangeEmp.Autosize = true;
			this.butChangeEmp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butChangeEmp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
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
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.label15);
			this.groupBox2.Controls.Add(this.groupBox1);
			this.groupBox2.Controls.Add(this.listPlanType);
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Controls.Add(this.checkClaimsUseUCR);
			this.groupBox2.Controls.Add(this.checkAlternateCode);
			this.groupBox2.Controls.Add(this.textGroupNum);
			this.groupBox2.Controls.Add(this.textGroupName);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.butChangeEmp);
			this.groupBox2.Controls.Add(this.textEmployer);
			this.groupBox2.Controls.Add(this.label16);
			this.groupBox2.Controls.Add(this.comboClaimForm);
			this.groupBox2.Controls.Add(this.comboFeeSched);
			this.groupBox2.Controls.Add(this.groupCoPay);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(7, 4);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(424, 481);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Synchronized Information";
			// 
			// butEdit
			// 
			this.butEdit.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEdit.Autosize = true;
			this.butEdit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEdit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEdit.Location = new System.Drawing.Point(336, 28);
			this.butEdit.Name = "butEdit";
			this.butEdit.Size = new System.Drawing.Size(75, 21);
			this.butEdit.TabIndex = 4;
			this.butEdit.Text = "Edit";
			this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
			// 
			// butChangeCarrier
			// 
			this.butChangeCarrier.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butChangeCarrier.Autosize = true;
			this.butChangeCarrier.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butChangeCarrier.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butChangeCarrier.Location = new System.Drawing.Point(336, 7);
			this.butChangeCarrier.Name = "butChangeCarrier";
			this.butChangeCarrier.Size = new System.Drawing.Size(75, 21);
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
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textCity);
			this.groupBox1.Controls.Add(this.textZip);
			this.groupBox1.Controls.Add(this.textState);
			this.groupBox1.Controls.Add(this.checkNoSendElect);
			this.groupBox1.Controls.Add(this.textAddress);
			this.groupBox1.Controls.Add(this.butEdit);
			this.groupBox1.Controls.Add(this.butChangeCarrier);
			this.groupBox1.Controls.Add(this.textAddress2);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.textElectID);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.textCarrier);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.textPhone);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(2, 79);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(418, 145);
			this.groupBox1.TabIndex = 121;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Carrier";
			// 
			// comboClaimForm
			// 
			this.comboClaimForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClaimForm.Location = new System.Drawing.Point(110, 329);
			this.comboClaimForm.MaxDropDownItems = 30;
			this.comboClaimForm.Name = "comboClaimForm";
			this.comboClaimForm.Size = new System.Drawing.Size(212, 21);
			this.comboClaimForm.TabIndex = 124;
			// 
			// comboFeeSched
			// 
			this.comboFeeSched.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboFeeSched.Location = new System.Drawing.Point(110, 307);
			this.comboFeeSched.MaxDropDownItems = 30;
			this.comboFeeSched.Name = "comboFeeSched";
			this.comboFeeSched.Size = new System.Drawing.Size(212, 21);
			this.comboFeeSched.TabIndex = 123;
			// 
			// groupCoPay
			// 
			this.groupCoPay.Controls.Add(this.label1);
			this.groupCoPay.Controls.Add(this.comboAllowedFeeSched);
			this.groupCoPay.Controls.Add(this.label5);
			this.groupCoPay.Controls.Add(this.label8);
			this.groupCoPay.Controls.Add(this.comboCopay);
			this.groupCoPay.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupCoPay.Location = new System.Drawing.Point(17, 363);
			this.groupCoPay.Name = "groupCoPay";
			this.groupCoPay.Size = new System.Drawing.Size(392, 104);
			this.groupCoPay.TabIndex = 122;
			this.groupCoPay.TabStop = false;
			this.groupCoPay.Text = "Other Fee Schedules";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 75);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(168, 16);
			this.label1.TabIndex = 111;
			this.label1.Text = "Carrier Allowed Amounts";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboAllowedFeeSched
			// 
			this.comboAllowedFeeSched.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboAllowedFeeSched.Location = new System.Drawing.Point(176, 72);
			this.comboAllowedFeeSched.MaxDropDownItems = 30;
			this.comboAllowedFeeSched.Name = "comboAllowedFeeSched";
			this.comboAllowedFeeSched.Size = new System.Drawing.Size(209, 21);
			this.comboAllowedFeeSched.TabIndex = 110;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6, 53);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(168, 16);
			this.label5.TabIndex = 109;
			this.label5.Text = "Patient Co-pay Amounts";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(3, 19);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(381, 29);
			this.label8.TabIndex = 106;
			this.label8.Text = "Don\'t use these unless you understand how they will affect your estimates";
			// 
			// comboCopay
			// 
			this.comboCopay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboCopay.Location = new System.Drawing.Point(176, 50);
			this.comboCopay.MaxDropDownItems = 30;
			this.comboCopay.Name = "comboCopay";
			this.comboCopay.Size = new System.Drawing.Size(209, 21);
			this.comboCopay.TabIndex = 108;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(11, 309);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(96, 15);
			this.label13.TabIndex = 125;
			this.label13.Text = "Fee Schedule";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(10, 333);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(95, 14);
			this.label15.TabIndex = 126;
			this.label15.Text = "Claim Form";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.groupBox1.ResumeLayout(false);
			this.groupCoPay.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormInsPlanEditAll_Load(object sender, System.EventArgs e) {
			PlanCur=OriginalPlan.Copy();
			if(((Pref)Prefs.HList["EasyHideCapitation"]).ValueString=="1"){
				//labelCopayFee.Visible=false;
				//listCopay.Visible=false;
				//butCopayNone.Visible=false;
				//labelCopayAdvice.Visible=false;
			}
			if(((Pref)Prefs.HList["EasyHideCapitation"]).ValueString=="1"){
				checkAlternateCode.Visible=false;
			}
			textEmployer.Text=Employers.GetName(PlanCur.EmployerNum);
			textGroupName.Text=PlanCur.GroupName;
			textGroupNum.Text=PlanCur.GroupNum;
			listPlanType.Items.Add(Lan.g(this,"Category Percentage"));
			if(PlanCur.PlanType=="")
				listPlanType.SelectedIndex=0;
			listPlanType.Items.Add(Lan.g(this,"Flat Co-pay"));
			if(PlanCur.PlanType=="f")
				listPlanType.SelectedIndex=1;
			if(((Pref)Prefs.HList["EasyHideCapitation"]).ValueString!="1"){
				listPlanType.Items.Add(Lan.g(this,"Capitation"));
				if(PlanCur.PlanType=="c")
					listPlanType.SelectedIndex=2;
			}
			checkAlternateCode.Checked=PlanCur.UseAltCode;
			checkClaimsUseUCR.Checked=PlanCur.ClaimsUseUCR;
			comboFeeSched.Items.Clear();
			comboFeeSched.Items.Add(Lan.g(this,"none"));
			comboFeeSched.SelectedIndex=0;
			for(int i=0;i<Defs.Short[(int)DefCat.FeeSchedNames].Length;i++){
				comboFeeSched.Items.Add(Defs.Short[(int)DefCat.FeeSchedNames][i].ItemName);
				if(Defs.Short[(int)DefCat.FeeSchedNames][i].DefNum==PlanCur.FeeSched)
					comboFeeSched.SelectedIndex=i+1;
			}
			comboCopay.Items.Clear();
			comboCopay.Items.Add(Lan.g(this,"none"));
			comboCopay.SelectedIndex=0;
			for(int i=0;i<Defs.Short[(int)DefCat.FeeSchedNames].Length;i++){
				comboCopay.Items.Add(Defs.Short[(int)DefCat.FeeSchedNames][i].ItemName);
				if(Defs.Short[(int)DefCat.FeeSchedNames][i].DefNum==PlanCur.CopayFeeSched)
					comboCopay.SelectedIndex=i+1;
			}
			comboAllowedFeeSched.Items.Clear();
			comboAllowedFeeSched.Items.Add(Lan.g(this,"none"));
			comboAllowedFeeSched.SelectedIndex=0;
			for(int i=0;i<Defs.Short[(int)DefCat.FeeSchedNames].Length;i++){
				comboAllowedFeeSched.Items.Add(Defs.Short[(int)DefCat.FeeSchedNames][i].ItemName);
				if(Defs.Short[(int)DefCat.FeeSchedNames][i].DefNum==PlanCur.AllowedFeeSched)
					comboAllowedFeeSched.SelectedIndex=i+1;
			}
			comboClaimForm.Items.Clear();
			for(int i=0;i<ClaimForms.ListShort.Length;i++){
				comboClaimForm.Items.Add(ClaimForms.ListShort[i].Description);
				if(ClaimForms.ListShort[i].ClaimFormNum==PlanCur.ClaimFormNum){
					comboClaimForm.SelectedIndex=i;
				}
			}
			if(comboClaimForm.Items.Count>0 && comboClaimForm.SelectedIndex==-1){
				comboClaimForm.SelectedIndex=0;//this will let the user rearrange the default later
			}
			//textNote.Text=InsPlans.Cur.Note;
//need entire right column
			
			FillCarrier();
			LayoutSynch();
		}

		private void FillCarrier(){
			Carriers.GetCur(PlanCur.CarrierNum);
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
			Employers.Cur=Employers.GetEmployer(PlanCur.EmployerNum);
			FormE.ShowDialog();
			if(FormE.DialogResult!=DialogResult.OK){
				return;
			}
			PlanCur.EmployerNum=Employers.Cur.EmployerNum;
			textEmployer.Text=Employers.Cur.EmpName;
		}

		private void butChangeCarrier_Click(object sender, System.EventArgs e) {
			FormCarriers FormC=new FormCarriers();
			FormC.IsSelectMode=true;
			FormC.ShowDialog();
			if(FormC.DialogResult!=DialogResult.OK){
				return;
			}
			PlanCur.CarrierNum=Carriers.Cur.CarrierNum;
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

		//private void butCopayNone_Click(object sender, System.EventArgs e) {
		//	listCopay.SelectedIndex=-1;
		//}

		private void LayoutSynch(){
			string[] samePlans=PlanCur.SamePlans();
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
			PlanCur.GroupName=textGroupName.Text;
			PlanCur.GroupNum=textGroupNum.Text;
			//Carrier already handled
			switch(listPlanType.SelectedIndex){
				case 0:
					PlanCur.PlanType="";
					break;
				case 1:
					PlanCur.PlanType="f";
					break;
				case 2:
					PlanCur.PlanType="c";
					break;
			}
			if(comboClaimForm.SelectedIndex!=-1)
				PlanCur.ClaimFormNum=ClaimForms.ListShort[comboClaimForm.SelectedIndex].ClaimFormNum;	
			PlanCur.UseAltCode=checkAlternateCode.Checked;
			PlanCur.ClaimsUseUCR=checkClaimsUseUCR.Checked;
			if(comboFeeSched.SelectedIndex==0)
				PlanCur.FeeSched=0;
			else
				PlanCur.FeeSched=Defs.Short[(int)DefCat.FeeSchedNames][comboFeeSched.SelectedIndex-1].DefNum;
			if(comboCopay.SelectedIndex==0)
				PlanCur.CopayFeeSched=0;
			else
				PlanCur.CopayFeeSched=Defs.Short[(int)DefCat.FeeSchedNames][comboCopay.SelectedIndex-1].DefNum;
			if(comboAllowedFeeSched.SelectedIndex==0)
				PlanCur.AllowedFeeSched=0;
			else
				PlanCur.AllowedFeeSched
					=Defs.Short[(int)DefCat.FeeSchedNames][comboAllowedFeeSched.SelectedIndex-1].DefNum;
			PlanCur.UpdateForLike(OriginalPlan);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			//warning. Don't delete if IsNew, because of the copy function in FormInsTemplates
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		
		
	}
}
