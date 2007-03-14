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
	public class FormCarrierEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textCarrierName;
		private System.Windows.Forms.TextBox textPhone;
		private System.Windows.Forms.TextBox textAddress;
		private System.Windows.Forms.TextBox textAddress2;
		private System.Windows.Forms.TextBox textElectID;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox comboPlans;
		private System.Windows.Forms.TextBox textPlans;
		private System.Windows.Forms.Label label9;
		private OpenDental.XPButton butDelete;
		private System.Windows.Forms.CheckBox checkNoSendElect;
		private System.Windows.Forms.TextBox textCity;
		private System.Windows.Forms.TextBox textState;
		private System.Windows.Forms.TextBox textZip;
		private System.Windows.Forms.Label label11;
		///<summary></summary>
		public bool IsNew;

		///<summary></summary>
		public FormCarrierEdit()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[]
			{
				this.label2,
				this.label3,
				this.label4,
				this.label6,
				this.label7, //*Ann
				this.label9, //*Ann
				this.label11, //*Ann
				this.groupBox1, //*Ann
				this.checkNoSendElect //*Ann
			});
			Lan.C("All", new System.Windows.Forms.Control[] 
			{
				butOK,
				butCancel,
				butDelete //*Ann
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormCarrierEdit));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.textCarrierName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textPhone = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textAddress = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textAddress2 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.textElectID = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.comboPlans = new System.Windows.Forms.ComboBox();
			this.textPlans = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.XPButton();
			this.checkNoSendElect = new System.Windows.Forms.CheckBox();
			this.textCity = new System.Windows.Forms.TextBox();
			this.textState = new System.Windows.Forms.TextBox();
			this.textZip = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(527, 271);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(78, 26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(527, 232);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(78, 26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textCarrierName
			// 
			this.textCarrierName.Location = new System.Drawing.Point(177, 19);
			this.textCarrierName.MaxLength = 255;
			this.textCarrierName.Name = "textCarrierName";
			this.textCarrierName.Size = new System.Drawing.Size(226, 20);
			this.textCarrierName.TabIndex = 3;
			this.textCarrierName.Text = "";
			this.textCarrierName.TextChanged += new System.EventHandler(this.textCarrierName_TextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(25, 42);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(151, 17);
			this.label2.TabIndex = 6;
			this.label2.Text = "Phone";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPhone
			// 
			this.textPhone.Location = new System.Drawing.Point(177, 39);
			this.textPhone.MaxLength = 255;
			this.textPhone.Name = "textPhone";
			this.textPhone.Size = new System.Drawing.Size(157, 20);
			this.textPhone.TabIndex = 5;
			this.textPhone.Text = "";
			this.textPhone.TextChanged += new System.EventHandler(this.textPhone_TextChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(24, 61);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(151, 17);
			this.label3.TabIndex = 8;
			this.label3.Text = "Address";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textAddress
			// 
			this.textAddress.Location = new System.Drawing.Point(177, 59);
			this.textAddress.MaxLength = 255;
			this.textAddress.Name = "textAddress";
			this.textAddress.Size = new System.Drawing.Size(291, 20);
			this.textAddress.TabIndex = 7;
			this.textAddress.Text = "";
			this.textAddress.TextChanged += new System.EventHandler(this.textAddress_TextChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(24, 83);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(151, 17);
			this.label4.TabIndex = 10;
			this.label4.Text = "Address2";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textAddress2
			// 
			this.textAddress2.Location = new System.Drawing.Point(177, 79);
			this.textAddress2.MaxLength = 255;
			this.textAddress2.Name = "textAddress2";
			this.textAddress2.Size = new System.Drawing.Size(291, 20);
			this.textAddress2.TabIndex = 9;
			this.textAddress2.Text = "";
			this.textAddress2.TextChanged += new System.EventHandler(this.textAddress2_TextChanged);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(24, 22);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(151, 17);
			this.label6.TabIndex = 14;
			this.label6.Text = "Carrier Name";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(24, 126);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(151, 17);
			this.label7.TabIndex = 20;
			this.label7.Text = "Electronic ID";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textElectID
			// 
			this.textElectID.Location = new System.Drawing.Point(177, 121);
			this.textElectID.Name = "textElectID";
			this.textElectID.Size = new System.Drawing.Size(59, 20);
			this.textElectID.TabIndex = 19;
			this.textElectID.Text = "";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.comboPlans);
			this.groupBox1.Controls.Add(this.textPlans);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(30, 155);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(448, 74);
			this.groupBox1.TabIndex = 23;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "In Use By";
			// 
			// comboPlans
			// 
			this.comboPlans.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPlans.Location = new System.Drawing.Point(188, 28);
			this.comboPlans.MaxDropDownItems = 30;
			this.comboPlans.Name = "comboPlans";
			this.comboPlans.Size = new System.Drawing.Size(238, 21);
			this.comboPlans.TabIndex = 68;
			// 
			// textPlans
			// 
			this.textPlans.BackColor = System.Drawing.Color.White;
			this.textPlans.Location = new System.Drawing.Point(149, 28);
			this.textPlans.Name = "textPlans";
			this.textPlans.ReadOnly = true;
			this.textPlans.Size = new System.Drawing.Size(35, 20);
			this.textPlans.TabIndex = 67;
			this.textPlans.Text = "";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(4, 30);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(142, 17);
			this.label9.TabIndex = 66;
			this.label9.Text = "Ins Plan Subscribers";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(31, 272);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(90, 26);
			this.butDelete.TabIndex = 24;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// checkNoSendElect
			// 
			this.checkNoSendElect.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNoSendElect.Location = new System.Drawing.Point(244, 124);
			this.checkNoSendElect.Name = "checkNoSendElect";
			this.checkNoSendElect.Size = new System.Drawing.Size(246, 17);
			this.checkNoSendElect.TabIndex = 93;
			this.checkNoSendElect.Text = "Don\'t Usually Send Electronically";
			// 
			// textCity
			// 
			this.textCity.Location = new System.Drawing.Point(177, 100);
			this.textCity.MaxLength = 255;
			this.textCity.Name = "textCity";
			this.textCity.Size = new System.Drawing.Size(155, 20);
			this.textCity.TabIndex = 94;
			this.textCity.Text = "";
			this.textCity.TextChanged += new System.EventHandler(this.textCity_TextChanged);
			// 
			// textState
			// 
			this.textState.Location = new System.Drawing.Point(332, 100);
			this.textState.MaxLength = 255;
			this.textState.Name = "textState";
			this.textState.Size = new System.Drawing.Size(65, 20);
			this.textState.TabIndex = 96;
			this.textState.Text = "";
			this.textState.TextChanged += new System.EventHandler(this.textState_TextChanged);
			// 
			// textZip
			// 
			this.textZip.Location = new System.Drawing.Point(397, 100);
			this.textZip.MaxLength = 255;
			this.textZip.Name = "textZip";
			this.textZip.Size = new System.Drawing.Size(71, 20);
			this.textZip.TabIndex = 97;
			this.textZip.Text = "";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(12, 104);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(163, 15);
			this.label11.TabIndex = 95;
			this.label11.Text = "City, ST, Zip";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormCarrierEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(635, 315);
			this.Controls.Add(this.textCity);
			this.Controls.Add(this.textState);
			this.Controls.Add(this.textZip);
			this.Controls.Add(this.textElectID);
			this.Controls.Add(this.textAddress2);
			this.Controls.Add(this.textAddress);
			this.Controls.Add(this.textPhone);
			this.Controls.Add(this.textCarrierName);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.checkNoSendElect);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormCarrierEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Carrier";
			this.Load += new System.EventHandler(this.FormCarrierEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormCarrierEdit_Load(object sender, System.EventArgs e) {
			textCarrierName.Text=Carriers.Cur.CarrierName;
			textPhone.Text=Carriers.Cur.Phone;
			textAddress.Text=Carriers.Cur.Address;
			textAddress2.Text=Carriers.Cur.Address2;
			textCity.Text=Carriers.Cur.City;
			textState.Text=Carriers.Cur.State;
			textZip.Text=Carriers.Cur.Zip;
			textElectID.Text=Carriers.Cur.ElectID;
			checkNoSendElect.Checked=Carriers.Cur.NoSendElect;
			string[] dependentPlans=Carriers.DependentPlans();
			textPlans.Text=dependentPlans.Length.ToString();
			comboPlans.Items.Clear();
			for(int i=0;i<dependentPlans.Length;i++){
				comboPlans.Items.Add(dependentPlans[i]);
			}
			if(dependentPlans.Length>0)
				comboPlans.SelectedIndex=0;
			//textTemplates.Text=Carriers.DependentTemplates().ToString();
		}

		private void textCarrierName_TextChanged(object sender, System.EventArgs e) {
			if(textCarrierName.Text.Length==1){
				textCarrierName.Text=textCarrierName.Text.ToUpper();
				textCarrierName.SelectionStart=1;
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

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(Carriers.DependentPlans().Length>0){
				MessageBox.Show(Lan.g(this,"Not allowed to delete carrier because it is in use.  Try combining carriers instead."));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete Carrier?"),"",MessageBoxButtons.OKCancel)
				!=DialogResult.OK){
				return;
			}
			Carriers.DeleteCur();
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textCarrierName.Text==""){
				MessageBox.Show(Lan.g(this,"Carrier Name cannot be blank."));
				return;
			}
			Carriers.Cur.CarrierName=textCarrierName.Text;
			Carriers.Cur.Phone=textPhone.Text;
			Carriers.Cur.Address=textAddress.Text;
			Carriers.Cur.Address2=textAddress2.Text;
			Carriers.Cur.City=textCity.Text;
			Carriers.Cur.State=textState.Text;
			Carriers.Cur.Zip=textZip.Text;
			Carriers.Cur.ElectID=textElectID.Text;
			Carriers.Cur.NoSendElect=checkNoSendElect.Checked;
			if(IsNew){
				Carriers.InsertCur();
			}
			else{
				Carriers.UpdateCur();
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		


		

		

		

		

		

		

		


	}
}





















