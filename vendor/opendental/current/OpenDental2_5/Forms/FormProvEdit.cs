using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormProvEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label labelColor;
		private System.Windows.Forms.Button butColor;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.CheckBox checkIsSecondary;
		private System.Windows.Forms.ListBox listFeeSched;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.TextBox textAbbr;
		private System.Windows.Forms.TextBox textStateLicense;
		private System.Windows.Forms.TextBox textSSN;
		private System.Windows.Forms.TextBox textTitle;
		private System.Windows.Forms.TextBox textMI;
		private System.Windows.Forms.TextBox textFName;
		private System.Windows.Forms.TextBox textLName;
		private System.Windows.Forms.TextBox textDEANum;
		private System.ComponentModel.Container components = null;// Required designer variable.
		private System.Windows.Forms.CheckBox checkIsHidden;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.ListBox listSpecialty;
		///<summary></summary>
		public bool IsNew;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioSSN;
		private System.Windows.Forms.RadioButton radioTIN;
		private System.Windows.Forms.TextBox textBlueCrossID;
		private System.Windows.Forms.Label label2;
		private OpenDental.TableUserPermissions tbUserPerm;
		private System.Windows.Forms.TextBox textUserName;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textPassword;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Button butAll;
		private System.Windows.Forms.Button butNone;
		private System.Windows.Forms.GroupBox groupSecurity;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox textMedicaidID;
		private System.Windows.Forms.CheckBox checkSigOnFile;

		///<summary></summary>
		public FormProvEdit(){
			InitializeComponent();// Required for Windows Form Designer support
			Lan.C(this, new System.Windows.Forms.Control[] {
				labelColor,
				butColor,
				label1,
				label3,
				label5,
				label6,
				label7,
				label8,
				label9,
				label10,
				label11,
				checkIsSecondary,
				checkIsHidden,
				groupBox1,
				radioSSN,
				radioTIN,
				label12,
				groupSecurity,
				label4,
				label2,
				butAll,
				butNone,
				checkSigOnFile,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
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
			this.checkIsHidden = new System.Windows.Forms.CheckBox();
			this.labelColor = new System.Windows.Forms.Label();
			this.butColor = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.checkIsSecondary = new System.Windows.Forms.CheckBox();
			this.listFeeSched = new System.Windows.Forms.ListBox();
			this.listSpecialty = new System.Windows.Forms.ListBox();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.textAbbr = new System.Windows.Forms.TextBox();
			this.textStateLicense = new System.Windows.Forms.TextBox();
			this.textSSN = new System.Windows.Forms.TextBox();
			this.textTitle = new System.Windows.Forms.TextBox();
			this.textMI = new System.Windows.Forms.TextBox();
			this.textFName = new System.Windows.Forms.TextBox();
			this.textLName = new System.Windows.Forms.TextBox();
			this.textDEANum = new System.Windows.Forms.TextBox();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioTIN = new System.Windows.Forms.RadioButton();
			this.radioSSN = new System.Windows.Forms.RadioButton();
			this.textBlueCrossID = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.checkSigOnFile = new System.Windows.Forms.CheckBox();
			this.groupSecurity = new System.Windows.Forms.GroupBox();
			this.tbUserPerm = new OpenDental.TableUserPermissions();
			this.textUserName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textPassword = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.butAll = new System.Windows.Forms.Button();
			this.butNone = new System.Windows.Forms.Button();
			this.textMedicaidID = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupSecurity.SuspendLayout();
			this.SuspendLayout();
			// 
			// checkIsHidden
			// 
			this.checkIsHidden.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsHidden.Location = new System.Drawing.Point(136, 398);
			this.checkIsHidden.Name = "checkIsHidden";
			this.checkIsHidden.Size = new System.Drawing.Size(70, 24);
			this.checkIsHidden.TabIndex = 11;
			this.checkIsHidden.Text = "Hidden";
			// 
			// labelColor
			// 
			this.labelColor.Location = new System.Drawing.Point(36, 432);
			this.labelColor.Name = "labelColor";
			this.labelColor.Size = new System.Drawing.Size(100, 16);
			this.labelColor.TabIndex = 10;
			this.labelColor.Text = "Appointment Color";
			this.labelColor.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butColor
			// 
			this.butColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butColor.Location = new System.Drawing.Point(136, 429);
			this.butColor.Name = "butColor";
			this.butColor.Size = new System.Drawing.Size(30, 20);
			this.butColor.TabIndex = 12;
			this.butColor.Click += new System.EventHandler(this.butColor_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(46, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(90, 14);
			this.label1.TabIndex = 12;
			this.label1.Text = "Abbreviation";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(21, 247);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(117, 14);
			this.label3.TabIndex = 14;
			this.label3.Text = "State License Number";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(10, 461);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(66, 14);
			this.label5.TabIndex = 16;
			this.label5.Text = "Specialty";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(205, 458);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 14);
			this.label6.TabIndex = 17;
			this.label6.Text = "Fee Schedule";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(70, 107);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(61, 14);
			this.label7.TabIndex = 18;
			this.label7.Text = "MI";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(46, 80);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(90, 14);
			this.label8.TabIndex = 19;
			this.label8.Text = "First Name";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(46, 134);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(90, 14);
			this.label9.TabIndex = 20;
			this.label9.Text = "Title (DMD,DDS)";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(46, 53);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(90, 14);
			this.label10.TabIndex = 21;
			this.label10.Text = "Last Name";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(38, 273);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(100, 14);
			this.label11.TabIndex = 22;
			this.label11.Text = "DEA Number";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkIsSecondary
			// 
			this.checkIsSecondary.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsSecondary.Location = new System.Drawing.Point(136, 352);
			this.checkIsSecondary.Name = "checkIsSecondary";
			this.checkIsSecondary.Size = new System.Drawing.Size(155, 17);
			this.checkIsSecondary.TabIndex = 9;
			this.checkIsSecondary.Text = "Secondary Provider (Hyg)";
			// 
			// listFeeSched
			// 
			this.listFeeSched.Location = new System.Drawing.Point(232, 475);
			this.listFeeSched.Name = "listFeeSched";
			this.listFeeSched.Size = new System.Drawing.Size(108, 173);
			this.listFeeSched.TabIndex = 13;
			// 
			// listSpecialty
			// 
			this.listSpecialty.Items.AddRange(new object[] {
																											 "Dental General Practice",
																											 "Dental Hygienist",
																											 "Endodontics",
																											 "Pediatric Dentistry",
																											 "Periodontics",
																											 "Prosthodontics",
																											 "Orthodontics",
																											 "Denturist",
																											 "Surgery, Oral & Maxillofacial",
																											 "Dental Assistant",
																											 "Dental Laboratory Technician",
																											 "Pathology, Oral & MaxFac",
																											 "Public Health"});
			this.listSpecialty.Location = new System.Drawing.Point(29, 475);
			this.listSpecialty.Name = "listSpecialty";
			this.listSpecialty.Size = new System.Drawing.Size(154, 173);
			this.listSpecialty.TabIndex = 14;
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(732, 562);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 15;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(732, 595);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 16;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textAbbr
			// 
			this.textAbbr.Location = new System.Drawing.Point(136, 22);
			this.textAbbr.MaxLength = 5;
			this.textAbbr.Name = "textAbbr";
			this.textAbbr.Size = new System.Drawing.Size(58, 20);
			this.textAbbr.TabIndex = 0;
			this.textAbbr.Text = "";
			// 
			// textStateLicense
			// 
			this.textStateLicense.Location = new System.Drawing.Point(136, 243);
			this.textStateLicense.MaxLength = 15;
			this.textStateLicense.Name = "textStateLicense";
			this.textStateLicense.TabIndex = 6;
			this.textStateLicense.Text = "";
			// 
			// textSSN
			// 
			this.textSSN.Location = new System.Drawing.Point(8, 54);
			this.textSSN.Name = "textSSN";
			this.textSSN.TabIndex = 2;
			this.textSSN.Text = "";
			// 
			// textTitle
			// 
			this.textTitle.Location = new System.Drawing.Point(136, 130);
			this.textTitle.MaxLength = 100;
			this.textTitle.Name = "textTitle";
			this.textTitle.Size = new System.Drawing.Size(104, 20);
			this.textTitle.TabIndex = 4;
			this.textTitle.Text = "";
			// 
			// textMI
			// 
			this.textMI.Location = new System.Drawing.Point(136, 103);
			this.textMI.MaxLength = 100;
			this.textMI.Name = "textMI";
			this.textMI.Size = new System.Drawing.Size(63, 20);
			this.textMI.TabIndex = 3;
			this.textMI.Text = "";
			// 
			// textFName
			// 
			this.textFName.Location = new System.Drawing.Point(136, 76);
			this.textFName.MaxLength = 100;
			this.textFName.Name = "textFName";
			this.textFName.Size = new System.Drawing.Size(161, 20);
			this.textFName.TabIndex = 2;
			this.textFName.Text = "";
			// 
			// textLName
			// 
			this.textLName.Location = new System.Drawing.Point(136, 49);
			this.textLName.MaxLength = 100;
			this.textLName.Name = "textLName";
			this.textLName.Size = new System.Drawing.Size(161, 20);
			this.textLName.TabIndex = 1;
			this.textLName.Text = "";
			// 
			// textDEANum
			// 
			this.textDEANum.Location = new System.Drawing.Point(136, 269);
			this.textDEANum.MaxLength = 15;
			this.textDEANum.Name = "textDEANum";
			this.textDEANum.TabIndex = 7;
			this.textDEANum.Text = "";
			// 
			// colorDialog1
			// 
			this.colorDialog1.FullOpen = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioTIN);
			this.groupBox1.Controls.Add(this.radioSSN);
			this.groupBox1.Controls.Add(this.textSSN);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(128, 155);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(156, 82);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "SSN or TIN (no dashes)";
			// 
			// radioTIN
			// 
			this.radioTIN.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioTIN.Location = new System.Drawing.Point(9, 34);
			this.radioTIN.Name = "radioTIN";
			this.radioTIN.Size = new System.Drawing.Size(135, 15);
			this.radioTIN.TabIndex = 1;
			this.radioTIN.Text = "TIN";
			this.radioTIN.Click += new System.EventHandler(this.radioTIN_Click);
			// 
			// radioSSN
			// 
			this.radioSSN.Checked = true;
			this.radioSSN.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioSSN.Location = new System.Drawing.Point(9, 17);
			this.radioSSN.Name = "radioSSN";
			this.radioSSN.Size = new System.Drawing.Size(104, 14);
			this.radioSSN.TabIndex = 0;
			this.radioSSN.TabStop = true;
			this.radioSSN.Text = "SSN";
			this.radioSSN.Click += new System.EventHandler(this.radioSSN_Click);
			// 
			// textBlueCrossID
			// 
			this.textBlueCrossID.Location = new System.Drawing.Point(136, 295);
			this.textBlueCrossID.MaxLength = 25;
			this.textBlueCrossID.Name = "textBlueCrossID";
			this.textBlueCrossID.TabIndex = 8;
			this.textBlueCrossID.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(50, 299);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(86, 14);
			this.label2.TabIndex = 39;
			this.label2.Text = "Blue Cross ID";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkSigOnFile
			// 
			this.checkSigOnFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkSigOnFile.Location = new System.Drawing.Point(136, 375);
			this.checkSigOnFile.Name = "checkSigOnFile";
			this.checkSigOnFile.Size = new System.Drawing.Size(121, 20);
			this.checkSigOnFile.TabIndex = 10;
			this.checkSigOnFile.Text = "Signature on File";
			// 
			// groupSecurity
			// 
			this.groupSecurity.Controls.Add(this.tbUserPerm);
			this.groupSecurity.Controls.Add(this.textUserName);
			this.groupSecurity.Controls.Add(this.label4);
			this.groupSecurity.Controls.Add(this.textPassword);
			this.groupSecurity.Controls.Add(this.label12);
			this.groupSecurity.Controls.Add(this.butAll);
			this.groupSecurity.Controls.Add(this.butNone);
			this.groupSecurity.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupSecurity.Location = new System.Drawing.Point(417, 22);
			this.groupSecurity.Name = "groupSecurity";
			this.groupSecurity.Size = new System.Drawing.Size(396, 483);
			this.groupSecurity.TabIndex = 40;
			this.groupSecurity.TabStop = false;
			this.groupSecurity.Text = "Security";
			// 
			// tbUserPerm
			// 
			this.tbUserPerm.BackColor = System.Drawing.SystemColors.Window;
			this.tbUserPerm.Location = new System.Drawing.Point(12, 84);
			this.tbUserPerm.Name = "tbUserPerm";
			this.tbUserPerm.ScrollValue = 1;
			this.tbUserPerm.SelectedIndices = new int[0];
			this.tbUserPerm.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbUserPerm.Size = new System.Drawing.Size(369, 356);
			this.tbUserPerm.TabIndex = 36;
			this.tbUserPerm.CellClicked += new OpenDental.ContrTable.CellEventHandler(this.tbUserPerm_CellClicked);
			// 
			// textUserName
			// 
			this.textUserName.Location = new System.Drawing.Point(76, 22);
			this.textUserName.MaxLength = 100;
			this.textUserName.Name = "textUserName";
			this.textUserName.Size = new System.Drawing.Size(302, 20);
			this.textUserName.TabIndex = 22;
			this.textUserName.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(10, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(60, 14);
			this.label4.TabIndex = 27;
			this.label4.Text = "UserName";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPassword
			// 
			this.textPassword.Location = new System.Drawing.Point(76, 46);
			this.textPassword.MaxLength = 100;
			this.textPassword.Name = "textPassword";
			this.textPassword.PasswordChar = '*';
			this.textPassword.Size = new System.Drawing.Size(302, 20);
			this.textPassword.TabIndex = 26;
			this.textPassword.Text = "";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(10, 48);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(60, 14);
			this.label12.TabIndex = 30;
			this.label12.Text = "Password";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butAll
			// 
			this.butAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butAll.Location = new System.Drawing.Point(14, 448);
			this.butAll.Name = "butAll";
			this.butAll.Size = new System.Drawing.Size(75, 26);
			this.butAll.TabIndex = 37;
			this.butAll.Text = "&All";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// butNone
			// 
			this.butNone.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butNone.Location = new System.Drawing.Point(104, 448);
			this.butNone.Name = "butNone";
			this.butNone.Size = new System.Drawing.Size(75, 26);
			this.butNone.TabIndex = 38;
			this.butNone.Text = "&None";
			this.butNone.Click += new System.EventHandler(this.butNone_Click);
			// 
			// textMedicaidID
			// 
			this.textMedicaidID.Location = new System.Drawing.Point(136, 320);
			this.textMedicaidID.MaxLength = 20;
			this.textMedicaidID.Name = "textMedicaidID";
			this.textMedicaidID.TabIndex = 41;
			this.textMedicaidID.Text = "";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(50, 324);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(86, 14);
			this.label13.TabIndex = 42;
			this.label13.Text = "Medicaid ID";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormProvEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(837, 666);
			this.Controls.Add(this.textMedicaidID);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.groupSecurity);
			this.Controls.Add(this.checkSigOnFile);
			this.Controls.Add(this.textBlueCrossID);
			this.Controls.Add(this.textDEANum);
			this.Controls.Add(this.textLName);
			this.Controls.Add(this.textFName);
			this.Controls.Add(this.textMI);
			this.Controls.Add(this.textTitle);
			this.Controls.Add(this.textStateLicense);
			this.Controls.Add(this.textAbbr);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.listSpecialty);
			this.Controls.Add(this.listFeeSched);
			this.Controls.Add(this.checkIsSecondary);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkIsHidden);
			this.Controls.Add(this.labelColor);
			this.Controls.Add(this.butColor);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProvEdit";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Provider";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormProvEdit_Closing);
			this.Load += new System.EventHandler(this.FormProvEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupSecurity.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormProvEdit_Load(object sender, System.EventArgs e) {
			groupSecurity.Visible=false;
			Permissions.GetCur("Security Administration");
			if(!Permissions.Cur.RequiresPassword){
				groupSecurity.Visible=true;
			}
			if(Users.Cur.EmployeeNum > 0){
				//if(UserPermissions.CheckHasPermission("Security Administration",Users.Cur.EmployeeNum,true))
				//	groupSecurity.Visible=true;
			}
			else{
				if(UserPermissions.CheckHasPermission("Security Administration",Users.Cur.ProvNum,false))
					groupSecurity.Visible=true;
			}
			if(IsNew){
				Providers.Cur.SigOnFile=true;
				Providers.InsertCur();
				//one field handled from previous form
			}
			textAbbr.Text=Providers.Cur.Abbr;
			textLName.Text=Providers.Cur.LName;
			textFName.Text=Providers.Cur.FName;
			textMI.Text=Providers.Cur.MI;
			textTitle.Text=Providers.Cur.Title;
			textSSN.Text=Providers.Cur.SSN;
			textUserName.Text=Providers.Cur.UserName;
			if(Providers.Cur.UserName!=""){
				textPassword.Text="*****";
			}
			if(Providers.Cur.UsingTIN){
				radioTIN.Checked=true;
			}
			else {
				radioSSN.Checked=true;
			}
			textStateLicense.Text=Providers.Cur.StateLicense;
			textDEANum.Text=Providers.Cur.DEANum;
			textBlueCrossID.Text=Providers.Cur.BlueCrossID;
			textMedicaidID.Text=Providers.Cur.MedicaidID;
			checkIsSecondary.Checked=Providers.Cur.IsSecondary;
			checkSigOnFile.Checked=Providers.Cur.SigOnFile;
			checkIsHidden.Checked=Providers.Cur.IsHidden;
			butColor.BackColor=Providers.Cur.ProvColor;
			for(int i=0;i<Defs.Short[(int)DefCat.FeeSchedNames].Length;i++){
				this.listFeeSched.Items.Add(Defs.Short[(int)DefCat.FeeSchedNames][i].ItemName);
				if(Defs.Short[(int)DefCat.FeeSchedNames][i].DefNum==Providers.Cur.FeeSched){
					listFeeSched.SelectedIndex=i;
				}
			}
			if(listFeeSched.SelectedIndex<0){
				listFeeSched.SelectedIndex=0;
			}
			listSpecialty.SelectedIndex=(int)Providers.Cur.Specialty;
			FillSecurity();
		}

		private void FillSecurity(){
			UserPermissions.Refresh();
			tbUserPerm.ResetRows(Permissions.List.Length);
			tbUserPerm.SetGridColor(Color.Gray);
			UserPermissions.GetListForProv(Providers.Cur.ProvNum);//emp not allowed security permission
			for(int i=0;i<Permissions.List.Length;i++){
				tbUserPerm.Cell[0,i]=Permissions.List[i].Name;
				for(int j=0;j<UserPermissions.ListForUser.Length;j++){
					if(UserPermissions.ListForUser[j].PermissionNum==Permissions.List[i].PermissionNum){
						tbUserPerm.Cell[1,i]="X";
						if(UserPermissions.ListForUser[j].IsLogged){
							tbUserPerm.Cell[2,i]="X";
						}
					}
				}//j
			}//i
			tbUserPerm.LayoutTables();
		}

		private void butColor_Click(object sender, System.EventArgs e) {
			colorDialog1.Color=butColor.BackColor;
			colorDialog1.ShowDialog();
			butColor.BackColor=colorDialog1.Color;
		}

		private void radioSSN_Click(object sender, System.EventArgs e) {
			Providers.Cur.UsingTIN=false;
		}

		private void radioTIN_Click(object sender, System.EventArgs e) {
			Providers.Cur.UsingTIN=true;
		}

		private void butAll_Click(object sender, System.EventArgs e) {
			for(int i=0;i<Permissions.List.Length;i++){
				if(!UserPermissions.CheckHasPermission(Permissions.List[i].Name,Providers.Cur.ProvNum,false)){
					UserPermissions.Cur=new UserPermission();
					UserPermissions.Cur.ProvNum=Providers.Cur.ProvNum;
					UserPermissions.Cur.PermissionNum=Permissions.List[i].PermissionNum;
					UserPermissions.Cur.IsLogged=true;
					UserPermissions.InsertCur();
				}
			}
			FillSecurity();
		}

		private void butNone_Click(object sender, System.EventArgs e) {
			/*UserPermissions.GetListForProv(Providers.Cur.ProvNum);
			for(int i=0;i<UserPermissions.ListForUser.Length;i++){
				Permissions.GetCur(UserPermissions.ListForUser[i].PermissionNum);
				if(Permissions.Cur.Name!="Security Administration"){
					UserPermissions.Cur=UserPermissions.ListForUser[i];
					UserPermissions.DeleteCur();
				}
			}*/
			UserPermissions.DeleteAllForProv(Providers.Cur.ProvNum);
			FillSecurity();
		}

		private void tbUserPerm_CellClicked(object sender, OpenDental.CellEventArgs e) {
			if(e.Col==1){//permission
				if(tbUserPerm.Cell[1,e.Row]!="X"){//add X
					UserPermissions.Cur=new UserPermission();
					UserPermissions.Cur.ProvNum=Providers.Cur.ProvNum;
					UserPermissions.Cur.PermissionNum=Permissions.List[e.Row].PermissionNum;
					UserPermissions.InsertCur();
				}
				else{//remove X
					if(Permissions.List[e.Row].Name=="Security Administration"){
						if(UserPermissions.AdministratorCount()==1){
							if(MessageBox.Show(Lan.g(this,"This is the only provider with Security Administration permission.  Are you sure you really want to remove this permission?"),""
								,MessageBoxButtons.OKCancel)!=DialogResult.OK){
								return;
							}
						}
					}
					for(int i=0;i<UserPermissions.ListForUser.Length;i++){
						if(UserPermissions.ListForUser[i].PermissionNum==Permissions.List[e.Row].PermissionNum){
							UserPermissions.Cur=UserPermissions.ListForUser[i];
							UserPermissions.DeleteCur();
						}	
					}//i
				}
				FillSecurity();
				return;
			}//e.Col==1	
			else if(e.Col==2){//logging
				if(tbUserPerm.Cell[2,e.Row]!="X" && tbUserPerm.Cell[1,e.Row]!="X"){//add X
					UserPermissions.Cur=new UserPermission();
					UserPermissions.Cur.ProvNum=Providers.Cur.ProvNum;
					UserPermissions.Cur.PermissionNum=Permissions.List[e.Row].PermissionNum;
					UserPermissions.Cur.IsLogged=true;
					UserPermissions.InsertCur();
				}
				else if(tbUserPerm.Cell[2,e.Row]!="X" && tbUserPerm.Cell[1,e.Row]=="X"){
					for(int i=0;i<UserPermissions.ListForUser.Length;i++){
						if(UserPermissions.ListForUser[i].PermissionNum==Permissions.List[e.Row].PermissionNum){
							UserPermissions.Cur=UserPermissions.ListForUser[i];
							UserPermissions.Cur.IsLogged=true;
							UserPermissions.UpdateCur();
						}	
					}
				}

				else{//remove X
					for(int i=0;i<UserPermissions.ListForUser.Length;i++){
						if(UserPermissions.ListForUser[i].PermissionNum==Permissions.List[e.Row].PermissionNum){
							UserPermissions.Cur=UserPermissions.ListForUser[i];
							UserPermissions.Cur.IsLogged=false;
							UserPermissions.UpdateCur();
						}	
					}
				}
				FillSecurity();
				return;
			}//end else if(e.Col==2)
			else{
				return;//if e.Col isn't 1 or 2 nothing needs to be done
			}		
		}

		private void butOK_Click(object sender, System.EventArgs e){
			Providers.Cur.Abbr=textAbbr.Text;
			Providers.Cur.LName=textLName.Text;
			Providers.Cur.FName=textFName.Text;
			Providers.Cur.MI=textMI.Text;
			Providers.Cur.Title=textTitle.Text;
			Providers.Cur.SSN=textSSN.Text;
			Providers.Cur.StateLicense=textStateLicense.Text;
			Providers.Cur.DEANum=textDEANum.Text;
			Providers.Cur.BlueCrossID=textBlueCrossID.Text;
			Providers.Cur.MedicaidID=textMedicaidID.Text;
			Providers.Cur.IsSecondary=checkIsSecondary.Checked;
			Providers.Cur.SigOnFile=checkSigOnFile.Checked;
			Providers.Cur.IsHidden=checkIsHidden.Checked;
			Providers.Cur.ProvColor=butColor.BackColor;
			if(textUserName.Text==""){
				if(UserPermissions.ListForUser.Length>0){
					if(MessageBox.Show(Lan.g(this,
						"UserName is blank.  Are you sure you want to delete this user's permissions?"),"",
						MessageBoxButtons.OKCancel)!=DialogResult.OK){
						return;
					}
					UserPermissions.DeleteAllForProv(Providers.Cur.ProvNum);
					if(UserPermissions.AdministratorCount()==0){
						if(MessageBox.Show(Lan.g(this,"There will be no users with Security Administration permission.  Do you wish to continue anyway?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
							FillSecurity();
							return;
						}
						Permissions.DisableSecurity();
					}
				}
			}
			if(textUserName.Text!="" && UserPermissions.AdministratorCount()==0){
				if(MessageBox.Show(Lan.g(this,
					"It is strongly recommended to have at least one provider with Security Administration permission.  Do you wish to continue anyway?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;
				}
				Permissions.DisableSecurity();
			}
			Providers.Cur.UserName=textUserName.Text;//if this is blank, then no 'user' exists
			if(textPassword.Text!="*****")
				Providers.Cur.Password=Passwords.EncryptPassword(textPassword.Text);
			if(listFeeSched.SelectedIndex!=-1)
				Providers.Cur.FeeSched=Defs.Short[(int)DefCat.FeeSchedNames][listFeeSched.SelectedIndex].DefNum;
			Providers.Cur.Specialty=(DentalSpecialty)listSpecialty.SelectedIndex;
			Providers.UpdateCur();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormProvEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNew){
				UserPermissions.DeleteAllForProv(Providers.Cur.ProvNum);
				Providers.DeleteCur();
			}
		}


	}
}




