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

	public class FormInsTemplatesEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
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
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ListBox listClaimFormat;// Required designer variable.
		private System.Windows.Forms.TextBox textAddress2;
		private System.Windows.Forms.Label label10;
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
				label7,
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
			this.label7 = new System.Windows.Forms.Label();
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
			this.listClaimFormat = new System.Windows.Forms.ListBox();
			this.textAddress2 = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
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
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(57, 165);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(78, 18);
			this.label7.TabIndex = 6;
			this.label7.Text = "Claim Format";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(33, 271);
			this.label8.Name = "label8";
			this.label8.TabIndex = 7;
			this.label8.Text = "Carrier Note";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(69, 249);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(62, 14);
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
			this.textElectID.Location = new System.Drawing.Point(134, 247);
			this.textElectID.MaxLength = 5;
			this.textElectID.Name = "textElectID";
			this.textElectID.Size = new System.Drawing.Size(44, 20);
			this.textElectID.TabIndex = 8;
			this.textElectID.Text = "";
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(134, 269);
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
			this.butOK.Location = new System.Drawing.Point(351, 372);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 10;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(437, 372);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 11;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// listClaimFormat
			// 
			this.listClaimFormat.Location = new System.Drawing.Point(134, 163);
			this.listClaimFormat.Name = "listClaimFormat";
			this.listClaimFormat.Size = new System.Drawing.Size(122, 82);
			this.listClaimFormat.TabIndex = 7;
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
			// FormInsTemplatesEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(525, 411);
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
			this.Controls.Add(this.listClaimFormat);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "FormInsTemplatesEdit";
			this.Text = Lan.g(this,"Edit Insurance Templates");
			this.Load += new System.EventHandler(this.FormInsTemplatesEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormInsTemplatesEdit_Load(object sender, System.EventArgs e) {
			textCarrier.Text=InsTemplates.Cur.Carrier;
			textAddress.Text=InsTemplates.Cur.Address;
			textAddress2.Text=InsTemplates.Cur.Address2;
			textCity.Text=InsTemplates.Cur.City;
			textST.Text=InsTemplates.Cur.State;
			textZip.Text=InsTemplates.Cur.Zip;
			textPhone.Text=InsTemplates.Cur.Phone;
			for(int i=0;i<Defs.Short[(int)DefCat.ClaimFormats].Length;i++){
				listClaimFormat.Items.Add(Defs.Short[(int)DefCat.ClaimFormats][i].ItemName);
				if(InsTemplates.Cur.ClaimFormat==Defs.Short[(int)DefCat.ClaimFormats][i].DefNum){
					listClaimFormat.SelectedIndex=i;
				}
			}
			textElectID.Text=InsTemplates.Cur.ElectID;
			textNote.Text=InsTemplates.Cur.Note;
		}
		
		private void butOK_Click(object sender, System.EventArgs e) {
			InsTemplates.Cur.Carrier=textCarrier.Text;
			InsTemplates.Cur.Address=textAddress.Text;
			InsTemplates.Cur.Address2=textAddress2.Text;
			InsTemplates.Cur.City=textCity.Text;
			InsTemplates.Cur.State=textST.Text;
			InsTemplates.Cur.Zip=textZip.Text;
			InsTemplates.Cur.Phone=textPhone.Text;
			if(listClaimFormat.SelectedIndex!=-1)
				InsTemplates.Cur.ClaimFormat=Defs.Short[(int)DefCat.ClaimFormats][listClaimFormat.SelectedIndex].DefNum;
			InsTemplates.Cur.ElectID=textElectID.Text;
			InsTemplates.Cur.Note=textNote.Text;
			if(IsNew){
				InsTemplates.InsertCur();
			}
			else{
				InsTemplates.UpdateCur();
			}
			DialogResult=DialogResult.OK;
			Close();
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
		
		}

		private void textPhone_TextChanged(object sender, System.EventArgs e) {
  		textPhone.Text=TelephoneNumbers.AutoFormat(textPhone.Text);
			textPhone.SelectionStart=textPhone.Text.Length;		
		}
		
	}
}
