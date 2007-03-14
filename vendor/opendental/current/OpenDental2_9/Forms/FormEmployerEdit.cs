using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormEmployerEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.TextBox textEmp;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textCity;
		private System.Windows.Forms.TextBox textState;
		private System.Windows.Forms.TextBox textZip;
		private System.Windows.Forms.TextBox textAddress2;
		private System.Windows.Forms.TextBox textAddress;
		private System.Windows.Forms.TextBox textPhone;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		///<summary></summary>
		public bool IsNew;

		///<summary></summary>
		public FormEmployerEdit()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
			});
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormEmployerEdit));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.textEmp = new System.Windows.Forms.TextBox();
			this.textCity = new System.Windows.Forms.TextBox();
			this.textState = new System.Windows.Forms.TextBox();
			this.textZip = new System.Windows.Forms.TextBox();
			this.textAddress2 = new System.Windows.Forms.TextBox();
			this.textAddress = new System.Windows.Forms.TextBox();
			this.textPhone = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(439, 295);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(439, 254);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textEmp
			// 
			this.textEmp.Location = new System.Drawing.Point(200, 49);
			this.textEmp.Name = "textEmp";
			this.textEmp.Size = new System.Drawing.Size(290, 20);
			this.textEmp.TabIndex = 0;
			this.textEmp.Text = "";
			// 
			// textCity
			// 
			this.textCity.Location = new System.Drawing.Point(167, 85);
			this.textCity.MaxLength = 255;
			this.textCity.Name = "textCity";
			this.textCity.Size = new System.Drawing.Size(155, 20);
			this.textCity.TabIndex = 3;
			this.textCity.Text = "";
			this.textCity.TextChanged += new System.EventHandler(this.textCity_TextChanged);
			// 
			// textState
			// 
			this.textState.Location = new System.Drawing.Point(322, 85);
			this.textState.MaxLength = 255;
			this.textState.Name = "textState";
			this.textState.Size = new System.Drawing.Size(65, 20);
			this.textState.TabIndex = 4;
			this.textState.Text = "";
			this.textState.TextChanged += new System.EventHandler(this.textState_TextChanged);
			// 
			// textZip
			// 
			this.textZip.Location = new System.Drawing.Point(387, 85);
			this.textZip.MaxLength = 255;
			this.textZip.Name = "textZip";
			this.textZip.Size = new System.Drawing.Size(71, 20);
			this.textZip.TabIndex = 5;
			this.textZip.Text = "";
			// 
			// textAddress2
			// 
			this.textAddress2.Location = new System.Drawing.Point(167, 64);
			this.textAddress2.MaxLength = 255;
			this.textAddress2.Name = "textAddress2";
			this.textAddress2.Size = new System.Drawing.Size(291, 20);
			this.textAddress2.TabIndex = 2;
			this.textAddress2.Text = "";
			this.textAddress2.TextChanged += new System.EventHandler(this.textAddress2_TextChanged);
			// 
			// textAddress
			// 
			this.textAddress.Location = new System.Drawing.Point(167, 44);
			this.textAddress.MaxLength = 255;
			this.textAddress.Name = "textAddress";
			this.textAddress.Size = new System.Drawing.Size(291, 20);
			this.textAddress.TabIndex = 1;
			this.textAddress.Text = "";
			this.textAddress.TextChanged += new System.EventHandler(this.textAddress_TextChanged);
			// 
			// textPhone
			// 
			this.textPhone.Location = new System.Drawing.Point(167, 24);
			this.textPhone.MaxLength = 255;
			this.textPhone.Name = "textPhone";
			this.textPhone.Size = new System.Drawing.Size(157, 20);
			this.textPhone.TabIndex = 0;
			this.textPhone.Text = "";
			this.textPhone.TextChanged += new System.EventHandler(this.textPhone_TextChanged);
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(68, 87);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(95, 15);
			this.label11.TabIndex = 105;
			this.label11.Text = "City, ST, Zip";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 66);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(151, 17);
			this.label4.TabIndex = 103;
			this.label4.Text = "Address2";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 44);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(151, 17);
			this.label3.TabIndex = 101;
			this.label3.Text = "Address";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(13, 25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(151, 17);
			this.label2.TabIndex = 99;
			this.label2.Text = "Phone";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textCity);
			this.groupBox1.Controls.Add(this.textState);
			this.groupBox1.Controls.Add(this.textZip);
			this.groupBox1.Controls.Add(this.textAddress2);
			this.groupBox1.Controls.Add(this.textAddress);
			this.groupBox1.Controls.Add(this.textPhone);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(32, 89);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(480, 132);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Optional";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(47, 51);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(151, 17);
			this.label1.TabIndex = 109;
			this.label1.Text = "Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormEmployerEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(534, 344);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textEmp);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEmployerEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Employer";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormEmployerEdit_Closing);
			this.Load += new System.EventHandler(this.FormEmployerEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormEmployerEdit_Load(object sender, System.EventArgs e) {
			textEmp.Text=Employers.Cur.EmpName;
			textPhone.Text=Employers.Cur.Phone;
			textAddress.Text=Employers.Cur.Address;
			textAddress2.Text=Employers.Cur.Address2;
			textCity.Text=Employers.Cur.City;
			textState.Text=Employers.Cur.State;
			textZip.Text=Employers.Cur.Zip;
		}

		private void textEmp_TextChanged(object sender, System.EventArgs e) {
			if(textEmp.Text.Length==1){
				textEmp.Text=textEmp.Text.ToUpper();
				textEmp.SelectionStart=1;
			}
		}

		private void textPhone_TextChanged(object sender, System.EventArgs e) {
			int cursor=textPhone.SelectionStart;
			int length=textPhone.Text.Length;
			textPhone.Text=TelephoneNumbers.AutoFormat(textPhone.Text);
			if(textPhone.Text.Length>length)
				cursor++;
			textPhone.SelectionStart=cursor;		
		}

		private void textAddress_TextChanged(object sender, System.EventArgs e) {
			if(textAddress.Text.Length==1){
				textAddress.Text=textAddress.Text.ToUpper();
				textAddress.SelectionStart=1;
			}
		}

		private void textAddress2_TextChanged(object sender, System.EventArgs e) {
			if(textAddress2.Text.Length==1){
				textAddress2.Text=textAddress2.Text.ToUpper();
				textAddress2.SelectionStart=1;
			}
		}

		private void textCity_TextChanged(object sender, System.EventArgs e) {
			if(textCity.Text.Length==1){
				textCity.Text=textCity.Text.ToUpper();
				textCity.SelectionStart=1;
			}
		}

		private void textState_TextChanged(object sender, System.EventArgs e) {
			int cursor=textState.SelectionStart;
			//for all countries, capitalize the first letter
			if(textState.Text.Length==1){
				textState.Text=textState.Text.ToUpper();
				textState.SelectionStart=cursor;
				return;
			}
			//for US and Canada, capitalize second letter as well.
			if(CultureInfo.CurrentCulture.Name=="en-US"
				|| CultureInfo.CurrentCulture.Name=="en-CA"){
				if(textState.Text.Length==2){
					textState.Text=textState.Text.ToUpper();
					textState.SelectionStart=cursor;
				}
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			Employers.Cur.EmpName=textEmp.Text;
			Employers.Cur.Phone=textPhone.Text;
			Employers.Cur.Address=textAddress.Text;
			Employers.Cur.Address2=textAddress2.Text;
			Employers.Cur.City=textCity.Text;
			Employers.Cur.State=textState.Text;
			Employers.Cur.Zip=textZip.Text;
			if(IsNew){
				Employers.InsertCur();
			}
			else{
				Employers.UpdateCur();
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormEmployerEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNew)
				Employers.DeleteCur();
		}

		


	}
}





















