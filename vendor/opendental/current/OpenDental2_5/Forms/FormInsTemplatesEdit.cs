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

	public class FormInsTemplatesEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.TextBox textCarrier;
		private System.Windows.Forms.TextBox textAddress;
		private System.Windows.Forms.TextBox textCity;
		private System.Windows.Forms.TextBox textST;
		private System.Windows.Forms.TextBox textZip;
		private System.Windows.Forms.TextBox textPhone;
		private System.Windows.Forms.TextBox textElectID;
		private System.Windows.Forms.TextBox textNote;
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
		private System.Windows.Forms.CheckBox checkWriteOff;
		private System.Windows.Forms.Button butCopayNone;
		private System.Windows.Forms.ListBox listCopay;
		private System.Windows.Forms.Label label25;
		public bool IsNew;

		public FormInsTemplatesEdit(){
			InitializeComponent();// Required for Windows Form Designer support
			Lan.C(this, new System.Windows.Forms.Control[] {
				label1,
				label2,
				label3,
				label4,
				label5,
				label6,
				checkNoSendElect,
				label8,
				label9,
				label10,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
					butOK,
					butCancel,
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
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.textCarrier = new System.Windows.Forms.TextBox();
			this.textAddress = new System.Windows.Forms.TextBox();
			this.textCity = new System.Windows.Forms.TextBox();
			this.textST = new System.Windows.Forms.TextBox();
			this.textZip = new System.Windows.Forms.TextBox();
			this.textPhone = new System.Windows.Forms.TextBox();
			this.textElectID = new System.Windows.Forms.TextBox();
			this.textNote = new System.Windows.Forms.TextBox();
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
			this.checkWriteOff = new System.Windows.Forms.CheckBox();
			this.butCopayNone = new System.Windows.Forms.Button();
			this.listCopay = new System.Windows.Forms.ListBox();
			this.label25 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(33, 13);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "Carrier";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(33, 33);
			this.label2.Name = "label2";
			this.label2.TabIndex = 1;
			this.label2.Text = "Address";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(31, 79);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "City";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(73, 101);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(58, 18);
			this.label4.TabIndex = 3;
			this.label4.Text = "ST";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(31, 123);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 18);
			this.label5.TabIndex = 4;
			this.label5.Text = "Zip";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(32, 143);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 18);
			this.label6.TabIndex = 5;
			this.label6.Text = "Phone";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(33, 463);
			this.label8.Name = "label8";
			this.label8.TabIndex = 7;
			this.label8.Text = "Carrier Note";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(45, 165);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(87, 14);
			this.label9.TabIndex = 8;
			this.label9.Text = "ElectID";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCarrier
			// 
			this.textCarrier.Location = new System.Drawing.Point(134, 9);
			this.textCarrier.MaxLength = 100;
			this.textCarrier.Name = "textCarrier";
			this.textCarrier.Size = new System.Drawing.Size(304, 20);
			this.textCarrier.TabIndex = 0;
			this.textCarrier.Text = "";
			// 
			// textAddress
			// 
			this.textAddress.Location = new System.Drawing.Point(134, 31);
			this.textAddress.MaxLength = 100;
			this.textAddress.Name = "textAddress";
			this.textAddress.Size = new System.Drawing.Size(244, 20);
			this.textAddress.TabIndex = 1;
			this.textAddress.Text = "";
			// 
			// textCity
			// 
			this.textCity.Location = new System.Drawing.Point(134, 75);
			this.textCity.MaxLength = 100;
			this.textCity.Name = "textCity";
			this.textCity.TabIndex = 3;
			this.textCity.Text = "";
			// 
			// textST
			// 
			this.textST.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.textST.Location = new System.Drawing.Point(134, 97);
			this.textST.MaxLength = 2;
			this.textST.Name = "textST";
			this.textST.Size = new System.Drawing.Size(28, 20);
			this.textST.TabIndex = 4;
			this.textST.Text = "";
			// 
			// textZip
			// 
			this.textZip.Location = new System.Drawing.Point(134, 119);
			this.textZip.MaxLength = 10;
			this.textZip.Name = "textZip";
			this.textZip.Size = new System.Drawing.Size(68, 20);
			this.textZip.TabIndex = 5;
			this.textZip.Text = "";
			// 
			// textPhone
			// 
			this.textPhone.Location = new System.Drawing.Point(134, 141);
			this.textPhone.MaxLength = 18;
			this.textPhone.Name = "textPhone";
			this.textPhone.TabIndex = 6;
			this.textPhone.Text = "";
			this.textPhone.TextChanged += new System.EventHandler(this.textPhone_TextChanged);
			// 
			// textElectID
			// 
			this.textElectID.Location = new System.Drawing.Point(134, 163);
			this.textElectID.MaxLength = 5;
			this.textElectID.Name = "textElectID";
			this.textElectID.Size = new System.Drawing.Size(44, 20);
			this.textElectID.TabIndex = 8;
			this.textElectID.Text = "";
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(134, 461);
			this.textNote.MaxLength = 256;
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(372, 86);
			this.textNote.TabIndex = 9;
			this.textNote.Text = "";
			// 
			// butOK
			// 
			this.butOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(574, 486);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 10;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(574, 522);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 11;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textAddress2
			// 
			this.textAddress2.Location = new System.Drawing.Point(134, 53);
			this.textAddress2.MaxLength = 100;
			this.textAddress2.Name = "textAddress2";
			this.textAddress2.Size = new System.Drawing.Size(244, 20);
			this.textAddress2.TabIndex = 2;
			this.textAddress2.Text = "";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(34, 55);
			this.label10.Name = "label10";
			this.label10.TabIndex = 21;
			this.label10.Text = "Address2";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkNoSendElect
			// 
			this.checkNoSendElect.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNoSendElect.Location = new System.Drawing.Point(187, 165);
			this.checkNoSendElect.Name = "checkNoSendElect";
			this.checkNoSendElect.Size = new System.Drawing.Size(206, 19);
			this.checkNoSendElect.TabIndex = 22;
			this.checkNoSendElect.Text = "Don\'t Usually Send Electronically";
			// 
			// listPlanType
			// 
			this.listPlanType.Location = new System.Drawing.Point(134, 185);
			this.listPlanType.Name = "listPlanType";
			this.listPlanType.Size = new System.Drawing.Size(120, 43);
			this.listPlanType.TabIndex = 96;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(34, 186);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(99, 14);
			this.label14.TabIndex = 95;
			this.label14.Text = "Plan Type";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(389, 291);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(121, 12);
			this.label23.TabIndex = 98;
			this.label23.Text = "Claim Form";
			// 
			// listClaimForm
			// 
			this.listClaimForm.Location = new System.Drawing.Point(388, 307);
			this.listClaimForm.Name = "listClaimForm";
			this.listClaimForm.Size = new System.Drawing.Size(147, 121);
			this.listClaimForm.TabIndex = 97;
			// 
			// checkClaimsUseUCR
			// 
			this.checkClaimsUseUCR.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkClaimsUseUCR.Location = new System.Drawing.Point(134, 269);
			this.checkClaimsUseUCR.Name = "checkClaimsUseUCR";
			this.checkClaimsUseUCR.Size = new System.Drawing.Size(309, 17);
			this.checkClaimsUseUCR.TabIndex = 100;
			this.checkClaimsUseUCR.Text = "Claims show UCR, not billed fee";
			// 
			// checkAlternateCode
			// 
			this.checkAlternateCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAlternateCode.Location = new System.Drawing.Point(134, 250);
			this.checkAlternateCode.Name = "checkAlternateCode";
			this.checkAlternateCode.Size = new System.Drawing.Size(310, 17);
			this.checkAlternateCode.TabIndex = 99;
			this.checkAlternateCode.Text = "Use Alternate Code (for some Medicaid plans)";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(134, 291);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(121, 12);
			this.label7.TabIndex = 102;
			this.label7.Text = "Fee Schedule";
			// 
			// listFeeSched
			// 
			this.listFeeSched.Location = new System.Drawing.Point(134, 307);
			this.listFeeSched.Name = "listFeeSched";
			this.listFeeSched.Size = new System.Drawing.Size(108, 121);
			this.listFeeSched.TabIndex = 101;
			// 
			// checkWriteOff
			// 
			this.checkWriteOff.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkWriteOff.Location = new System.Drawing.Point(134, 231);
			this.checkWriteOff.Name = "checkWriteOff";
			this.checkWriteOff.Size = new System.Drawing.Size(277, 17);
			this.checkWriteOff.TabIndex = 103;
			this.checkWriteOff.Text = "Usually write off unpaid insurance portions";
			// 
			// butCopayNone
			// 
			this.butCopayNone.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCopayNone.Location = new System.Drawing.Point(248, 429);
			this.butCopayNone.Name = "butCopayNone";
			this.butCopayNone.TabIndex = 108;
			this.butCopayNone.Text = "None";
			this.butCopayNone.Click += new System.EventHandler(this.butCopayNone_Click);
			// 
			// listCopay
			// 
			this.listCopay.Location = new System.Drawing.Point(246, 307);
			this.listCopay.Name = "listCopay";
			this.listCopay.Size = new System.Drawing.Size(108, 121);
			this.listCopay.TabIndex = 106;
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(246, 291);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(121, 17);
			this.label25.TabIndex = 107;
			this.label25.Text = "Co-pay Fee Schedule ";
			// 
			// FormInsTemplatesEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(678, 573);
			this.Controls.Add(this.listClaimForm);
			this.Controls.Add(this.butCopayNone);
			this.Controls.Add(this.listCopay);
			this.Controls.Add(this.label25);
			this.Controls.Add(this.checkWriteOff);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.listFeeSched);
			this.Controls.Add(this.checkClaimsUseUCR);
			this.Controls.Add(this.checkAlternateCode);
			this.Controls.Add(this.label23);
			this.Controls.Add(this.listPlanType);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.checkNoSendElect);
			this.Controls.Add(this.textAddress2);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.textElectID);
			this.Controls.Add(this.textPhone);
			this.Controls.Add(this.textZip);
			this.Controls.Add(this.textST);
			this.Controls.Add(this.textCity);
			this.Controls.Add(this.textAddress);
			this.Controls.Add(this.textCarrier);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormInsTemplatesEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Insurance Template";
			this.Load += new System.EventHandler(this.FormInsTemplatesEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormInsTemplatesEdit_Load(object sender, System.EventArgs e) {
			if(IsNew){
				InsTemplates.Cur.PlanType="I";
			}
			textCarrier.Text=InsTemplates.Cur.Carrier;
			textAddress.Text=InsTemplates.Cur.Address;
			textAddress2.Text=InsTemplates.Cur.Address2;
			textCity.Text=InsTemplates.Cur.City;
			textST.Text=InsTemplates.Cur.State;
			textZip.Text=InsTemplates.Cur.Zip;
			textPhone.Text=InsTemplates.Cur.Phone;
			//for(int i=0;i<Defs.Short[(int)DefCat.ClaimFormats].Length;i++){
			//	listClaimFormat.Items.Add(Defs.Short[(int)DefCat.ClaimFormats][i].ItemName);
			//	if(InsTemplates.Cur.ClaimFormat==Defs.Short[(int)DefCat.ClaimFormats][i].DefNum){
			//		listClaimFormat.SelectedIndex=i;
			//	}
			//}
			checkNoSendElect.Checked=InsTemplates.Cur.NoSendElect;
			textElectID.Text=InsTemplates.Cur.ElectID;
			textNote.Text=InsTemplates.Cur.Note;
			listPlanType.Items.Add(Lan.g(this,"Category Percentage"));
			listPlanType.Items.Add(Lan.g(this,"Flat Co-pay"));
			listPlanType.Items.Add(Lan.g(this,"Capitation"));
			switch(InsTemplates.Cur.PlanType){
				default: 
					listPlanType.SelectedIndex=0;
					break;
				case "f": 
					listPlanType.SelectedIndex=1;
					break;
				case "c": 
					listPlanType.SelectedIndex=2;
					break;
			}
			for(int i=0;i<ClaimForms.ListShort.Length;i++){
				listClaimForm.Items.Add(ClaimForms.ListShort[i].Description);
				if(ClaimForms.ListShort[i].ClaimFormNum==InsTemplates.Cur.ClaimFormNum){
					listClaimForm.SelectedIndex=i;
				}
			}
			if(listClaimForm.SelectedIndex==-1){
				listClaimForm.SelectedIndex=0;//this will let the user rearrange the default later
			}
			for(int i=0;i<Defs.Short[(int)DefCat.FeeSchedNames].Length;i++){
				listFeeSched.Items.Add(Defs.Short[(int)DefCat.FeeSchedNames][i].ItemName);
				if(Defs.Short[(int)DefCat.FeeSchedNames][i].DefNum==InsTemplates.Cur.FeeSched)
					listFeeSched.SelectedIndex=i;
			}
			for(int i=0;i<Defs.Short[(int)DefCat.FeeSchedNames].Length;i++){
				listCopay.Items.Add(Defs.Short[(int)DefCat.FeeSchedNames][i].ItemName);
				if(Defs.Short[(int)DefCat.FeeSchedNames][i].DefNum==InsTemplates.Cur.CopayFeeSched)
					listCopay.SelectedIndex=i;
			}
			checkWriteOff.Checked=InsTemplates.Cur.IsWrittenOff;
			checkAlternateCode.Checked=InsTemplates.Cur.UseAltCode;
			checkClaimsUseUCR.Checked=InsTemplates.Cur.ClaimsUseUCR;
		}

		private void butCopayNone_Click(object sender, System.EventArgs e) {
			listCopay.SelectedIndex=-1;
		}
		
		private void butOK_Click(object sender, System.EventArgs e) {
			InsTemplates.Cur.Carrier=textCarrier.Text;
			InsTemplates.Cur.Address=textAddress.Text;
			InsTemplates.Cur.Address2=textAddress2.Text;
			InsTemplates.Cur.City=textCity.Text;
			InsTemplates.Cur.State=textST.Text;
			InsTemplates.Cur.Zip=textZip.Text;
			InsTemplates.Cur.Phone=textPhone.Text;
			//if(listClaimFormat.SelectedIndex!=-1)
			//	InsTemplates.Cur.ClaimFormat=Defs.Short[(int)DefCat.ClaimFormats][listClaimFormat.SelectedIndex].DefNum;
			InsTemplates.Cur.NoSendElect=checkNoSendElect.Checked;
			InsTemplates.Cur.ElectID=textElectID.Text;
			InsTemplates.Cur.Note=textNote.Text;
			switch(listPlanType.SelectedIndex){
				case 0:
					InsTemplates.Cur.PlanType="";
					break;
				case 1:
					InsTemplates.Cur.PlanType="f";
					break;
				case 2:
					InsTemplates.Cur.PlanType="c";
					break;
			}
			if(listFeeSched.SelectedIndex==-1)
				InsTemplates.Cur.FeeSched=0;
			else
				InsTemplates.Cur.FeeSched=Defs.Short[(int)DefCat.FeeSchedNames][listFeeSched.SelectedIndex].DefNum;
			if(listCopay.SelectedIndex==-1)
				InsTemplates.Cur.CopayFeeSched=0;
			else
				InsTemplates.Cur.CopayFeeSched
					=Defs.Short[(int)DefCat.FeeSchedNames][listCopay.SelectedIndex].DefNum;
			InsTemplates.Cur.ClaimFormNum=ClaimForms.ListShort[listClaimForm.SelectedIndex].ClaimFormNum;
			InsTemplates.Cur.IsWrittenOff=checkWriteOff.Checked;
			InsTemplates.Cur.UseAltCode=checkAlternateCode.Checked;
			InsTemplates.Cur.ClaimsUseUCR=checkClaimsUseUCR.Checked;
			if(IsNew){
				InsTemplates.InsertCur();
			}
			else{
				InsTemplates.UpdateCur();
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void textPhone_TextChanged(object sender, System.EventArgs e) {
  		textPhone.Text=TelephoneNumbers.AutoFormat(textPhone.Text);
			textPhone.SelectionStart=textPhone.Text.Length;		
		}

		
		
	}
}
