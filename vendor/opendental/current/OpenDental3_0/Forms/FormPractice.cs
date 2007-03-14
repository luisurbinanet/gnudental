using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormPractice : System.Windows.Forms.Form{
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textBankNumber;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox textZip;
		private System.Windows.Forms.TextBox textST;
		private System.Windows.Forms.TextBox textCity;
		private System.Windows.Forms.TextBox textAddress2;
		private System.Windows.Forms.TextBox textAddress;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textPracticeTitle;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ListBox listBillType;
		private System.Windows.Forms.ListBox listProvider;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Button butTreatProv;
		private System.Windows.Forms.ListBox listBillProv;
		private System.Windows.Forms.TextBox textTreatNote;
		private System.Windows.Forms.CheckBox checkShowCC;
		private System.Windows.Forms.CheckBox checkAutoRefresh;
		private System.Windows.Forms.TextBox textMainWindowTitle;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.ComboBox comboPatientPictureCat;
		private System.Windows.Forms.CheckBox checkITooth;
		private System.Windows.Forms.Label labelPlaceService;
		private System.Windows.Forms.ListBox listPlaceService;
		private System.Windows.Forms.TextBox textPhone;
		private System.Windows.Forms.CheckBox checkTreatPlanShowGraphics;
		private System.Windows.Forms.CheckBox checkTreatPlanShowCompleted;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkTreatPlanShowIns;
		private System.Windows.Forms.Label label1;// Required designer variable.

		///<summary></summary>
		public FormPractice(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[]
			{
				groupBox3,
				label12,
				label10,
				label4,
				groupBox2,
				label5,
				label6,
				label7,
				label8,
				label9,
				label3,
				label14,
				label16,
				butTreatProv,
				label1,
				checkITooth,
				checkAutoRefresh,
				checkShowCC
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
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.butTreatProv = new System.Windows.Forms.Button();
			this.listBillProv = new System.Windows.Forms.ListBox();
			this.listBillType = new System.Windows.Forms.ListBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.listProvider = new System.Windows.Forms.ListBox();
			this.textBankNumber = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textPhone = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.textZip = new System.Windows.Forms.TextBox();
			this.textST = new System.Windows.Forms.TextBox();
			this.textCity = new System.Windows.Forms.TextBox();
			this.textAddress2 = new System.Windows.Forms.TextBox();
			this.textAddress = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.textPracticeTitle = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.textTreatNote = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkShowCC = new System.Windows.Forms.CheckBox();
			this.checkAutoRefresh = new System.Windows.Forms.CheckBox();
			this.textMainWindowTitle = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.comboPatientPictureCat = new System.Windows.Forms.ComboBox();
			this.checkITooth = new System.Windows.Forms.CheckBox();
			this.labelPlaceService = new System.Windows.Forms.Label();
			this.listPlaceService = new System.Windows.Forms.ListBox();
			this.checkTreatPlanShowGraphics = new System.Windows.Forms.CheckBox();
			this.checkTreatPlanShowCompleted = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkTreatPlanShowIns = new System.Windows.Forms.CheckBox();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.butTreatProv);
			this.groupBox3.Controls.Add(this.listBillProv);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(477, 214);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(188, 242);
			this.groupBox3.TabIndex = 6;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Default Insurance Billing Dentist";
			this.groupBox3.Visible = false;
			// 
			// butTreatProv
			// 
			this.butTreatProv.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butTreatProv.Location = new System.Drawing.Point(24, 202);
			this.butTreatProv.Name = "butTreatProv";
			this.butTreatProv.Size = new System.Drawing.Size(110, 26);
			this.butTreatProv.TabIndex = 1;
			this.butTreatProv.Text = "Treating Provider";
			this.butTreatProv.Click += new System.EventHandler(this.butTreatProv_Click);
			// 
			// listBillProv
			// 
			this.listBillProv.Items.AddRange(new object[] {
																											""});
			this.listBillProv.Location = new System.Drawing.Point(24, 26);
			this.listBillProv.Name = "listBillProv";
			this.listBillProv.Size = new System.Drawing.Size(110, 160);
			this.listBillProv.TabIndex = 0;
			// 
			// listBillType
			// 
			this.listBillType.Items.AddRange(new object[] {
																											""});
			this.listBillType.Location = new System.Drawing.Point(571, 48);
			this.listBillType.Name = "listBillType";
			this.listBillType.Size = new System.Drawing.Size(160, 147);
			this.listBillType.TabIndex = 5;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(570, 31);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(140, 23);
			this.label12.TabIndex = 29;
			this.label12.Text = "Default Billing Type";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(452, 30);
			this.label10.Name = "label10";
			this.label10.TabIndex = 26;
			this.label10.Text = "Default Provider";
			// 
			// listProvider
			// 
			this.listProvider.Items.AddRange(new object[] {
																											""});
			this.listProvider.Location = new System.Drawing.Point(453, 48);
			this.listProvider.Name = "listProvider";
			this.listProvider.Size = new System.Drawing.Size(110, 147);
			this.listProvider.TabIndex = 4;
			// 
			// textBankNumber
			// 
			this.textBankNumber.Location = new System.Drawing.Point(144, 255);
			this.textBankNumber.Name = "textBankNumber";
			this.textBankNumber.Size = new System.Drawing.Size(212, 20);
			this.textBankNumber.TabIndex = 2;
			this.textBankNumber.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(-9, 249);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(153, 32);
			this.label4.TabIndex = 22;
			this.label4.Text = "Bank Deposit Acct Number";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textPhone);
			this.groupBox2.Controls.Add(this.label16);
			this.groupBox2.Controls.Add(this.textZip);
			this.groupBox2.Controls.Add(this.textST);
			this.groupBox2.Controls.Add(this.textCity);
			this.groupBox2.Controls.Add(this.textAddress2);
			this.groupBox2.Controls.Add(this.textAddress);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(59, 73);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(349, 170);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Address";
			// 
			// textPhone
			// 
			this.textPhone.Location = new System.Drawing.Point(85, 139);
			this.textPhone.Name = "textPhone";
			this.textPhone.Size = new System.Drawing.Size(121, 20);
			this.textPhone.TabIndex = 20;
			this.textPhone.Text = "";
			this.textPhone.TextChanged += new System.EventHandler(this.textPhone_TextChanged);
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(27, 48);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(58, 16);
			this.label16.TabIndex = 19;
			this.label16.Text = "Address 2";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textZip
			// 
			this.textZip.Location = new System.Drawing.Point(85, 116);
			this.textZip.Name = "textZip";
			this.textZip.Size = new System.Drawing.Size(121, 20);
			this.textZip.TabIndex = 4;
			this.textZip.Text = "";
			// 
			// textST
			// 
			this.textST.Location = new System.Drawing.Point(85, 92);
			this.textST.Name = "textST";
			this.textST.Size = new System.Drawing.Size(127, 20);
			this.textST.TabIndex = 3;
			this.textST.Text = "";
			// 
			// textCity
			// 
			this.textCity.Location = new System.Drawing.Point(85, 68);
			this.textCity.Name = "textCity";
			this.textCity.Size = new System.Drawing.Size(215, 20);
			this.textCity.TabIndex = 2;
			this.textCity.Text = "";
			// 
			// textAddress2
			// 
			this.textAddress2.Location = new System.Drawing.Point(85, 44);
			this.textAddress2.Name = "textAddress2";
			this.textAddress2.Size = new System.Drawing.Size(250, 20);
			this.textAddress2.TabIndex = 1;
			this.textAddress2.Text = "";
			// 
			// textAddress
			// 
			this.textAddress.Location = new System.Drawing.Point(85, 20);
			this.textAddress.Name = "textAddress";
			this.textAddress.Size = new System.Drawing.Size(250, 20);
			this.textAddress.TabIndex = 0;
			this.textAddress.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(30, 24);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(53, 14);
			this.label5.TabIndex = 3;
			this.label5.Text = "Address";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(33, 72);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(50, 15);
			this.label6.TabIndex = 4;
			this.label6.Text = "City";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(42, 96);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(41, 18);
			this.label7.TabIndex = 5;
			this.label7.Text = "ST";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(38, 118);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(46, 17);
			this.label8.TabIndex = 6;
			this.label8.Text = "Zip";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(33, 140);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(52, 17);
			this.label9.TabIndex = 7;
			this.label9.Text = "Phone";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPracticeTitle
			// 
			this.textPracticeTitle.Location = new System.Drawing.Point(144, 29);
			this.textPracticeTitle.Name = "textPracticeTitle";
			this.textPracticeTitle.Size = new System.Drawing.Size(249, 20);
			this.textPracticeTitle.TabIndex = 0;
			this.textPracticeTitle.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(50, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 28);
			this.label3.TabIndex = 19;
			this.label3.Text = "Dentist Name or Practice Title";
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(810, 561);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 7;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(810, 599);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 8;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textTreatNote
			// 
			this.textTreatNote.AcceptsReturn = true;
			this.textTreatNote.Location = new System.Drawing.Point(17, 40);
			this.textTreatNote.Multiline = true;
			this.textTreatNote.Name = "textTreatNote";
			this.textTreatNote.Size = new System.Drawing.Size(346, 114);
			this.textTreatNote.TabIndex = 3;
			this.textTreatNote.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(15, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(268, 15);
			this.label1.TabIndex = 35;
			this.label1.Text = "Note";
			// 
			// checkShowCC
			// 
			this.checkShowCC.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowCC.Location = new System.Drawing.Point(75, 511);
			this.checkShowCC.Name = "checkShowCC";
			this.checkShowCC.Size = new System.Drawing.Size(334, 17);
			this.checkShowCC.TabIndex = 36;
			this.checkShowCC.Text = "Show Credit Card Info on Statements";
			// 
			// checkAutoRefresh
			// 
			this.checkAutoRefresh.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAutoRefresh.Location = new System.Drawing.Point(75, 533);
			this.checkAutoRefresh.Name = "checkAutoRefresh";
			this.checkAutoRefresh.Size = new System.Drawing.Size(603, 16);
			this.checkAutoRefresh.TabIndex = 37;
			this.checkAutoRefresh.Text = "Disable AutoRefresh.  Warning!  You would not normally do this except for testing" +
				".";
			// 
			// textMainWindowTitle
			// 
			this.textMainWindowTitle.Location = new System.Drawing.Point(170, 556);
			this.textMainWindowTitle.Name = "textMainWindowTitle";
			this.textMainWindowTitle.Size = new System.Drawing.Size(333, 20);
			this.textMainWindowTitle.TabIndex = 38;
			this.textMainWindowTitle.Text = "";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(20, 558);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(149, 17);
			this.label14.TabIndex = 39;
			this.label14.Text = "Main Window Title";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(469, 487);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(218, 17);
			this.label15.TabIndex = 40;
			this.label15.Text = "Image Category for Patient Picture";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.label15.Visible = false;
			// 
			// comboPatientPictureCat
			// 
			this.comboPatientPictureCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPatientPictureCat.Location = new System.Drawing.Point(688, 483);
			this.comboPatientPictureCat.Name = "comboPatientPictureCat";
			this.comboPatientPictureCat.Size = new System.Drawing.Size(261, 21);
			this.comboPatientPictureCat.TabIndex = 41;
			this.comboPatientPictureCat.Visible = false;
			// 
			// checkITooth
			// 
			this.checkITooth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkITooth.Location = new System.Drawing.Point(75, 580);
			this.checkITooth.Name = "checkITooth";
			this.checkITooth.Size = new System.Drawing.Size(334, 24);
			this.checkITooth.TabIndex = 42;
			this.checkITooth.Text = "Use International Tooth Numbers (11-48)";
			// 
			// labelPlaceService
			// 
			this.labelPlaceService.Location = new System.Drawing.Point(735, 30);
			this.labelPlaceService.Name = "labelPlaceService";
			this.labelPlaceService.Size = new System.Drawing.Size(140, 23);
			this.labelPlaceService.TabIndex = 44;
			this.labelPlaceService.Text = "Default Proc Place Service";
			// 
			// listPlaceService
			// 
			this.listPlaceService.Location = new System.Drawing.Point(737, 48);
			this.listPlaceService.Name = "listPlaceService";
			this.listPlaceService.Size = new System.Drawing.Size(145, 147);
			this.listPlaceService.TabIndex = 45;
			// 
			// checkTreatPlanShowGraphics
			// 
			this.checkTreatPlanShowGraphics.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTreatPlanShowGraphics.Location = new System.Drawing.Point(16, 159);
			this.checkTreatPlanShowGraphics.Name = "checkTreatPlanShowGraphics";
			this.checkTreatPlanShowGraphics.Size = new System.Drawing.Size(339, 17);
			this.checkTreatPlanShowGraphics.TabIndex = 46;
			this.checkTreatPlanShowGraphics.Text = "Show Graphical Tooth Chart";
			// 
			// checkTreatPlanShowCompleted
			// 
			this.checkTreatPlanShowCompleted.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTreatPlanShowCompleted.Location = new System.Drawing.Point(16, 179);
			this.checkTreatPlanShowCompleted.Name = "checkTreatPlanShowCompleted";
			this.checkTreatPlanShowCompleted.Size = new System.Drawing.Size(334, 17);
			this.checkTreatPlanShowCompleted.TabIndex = 47;
			this.checkTreatPlanShowCompleted.Text = "Show Completed Work on Graphical Tooth Chart";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkTreatPlanShowIns);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.textTreatNote);
			this.groupBox1.Controls.Add(this.checkTreatPlanShowGraphics);
			this.groupBox1.Controls.Add(this.checkTreatPlanShowCompleted);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(59, 285);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(381, 222);
			this.groupBox1.TabIndex = 48;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Treatment Plans";
			// 
			// checkTreatPlanShowIns
			// 
			this.checkTreatPlanShowIns.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTreatPlanShowIns.Location = new System.Drawing.Point(16, 199);
			this.checkTreatPlanShowIns.Name = "checkTreatPlanShowIns";
			this.checkTreatPlanShowIns.Size = new System.Drawing.Size(334, 17);
			this.checkTreatPlanShowIns.TabIndex = 48;
			this.checkTreatPlanShowIns.Text = "Show Insurance Estimates";
			// 
			// FormPractice
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(899, 637);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.listPlaceService);
			this.Controls.Add(this.labelPlaceService);
			this.Controls.Add(this.checkITooth);
			this.Controls.Add(this.comboPatientPictureCat);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.textMainWindowTitle);
			this.Controls.Add(this.textBankNumber);
			this.Controls.Add(this.textPracticeTitle);
			this.Controls.Add(this.checkAutoRefresh);
			this.Controls.Add(this.checkShowCC);
			this.Controls.Add(this.listProvider);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.listBillType);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.groupBox3);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPractice";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Practice Info";
			this.Load += new System.EventHandler(this.FormPractice_Load);
			this.groupBox3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormPractice_Load(object sender, System.EventArgs e) {
			if(((Pref)Prefs.HList["EasyHidePublicHealth"]).ValueString=="1"){
				labelPlaceService.Visible=false;
				listPlaceService.Visible=false;
			}
			textPracticeTitle.Text=((Pref)Prefs.HList["PracticeTitle"]).ValueString;
			textAddress.Text=((Pref)Prefs.HList["PracticeAddress"]).ValueString;
			textAddress2.Text=((Pref)Prefs.HList["PracticeAddress2"]).ValueString;
			textCity.Text=((Pref)Prefs.HList["PracticeCity"]).ValueString;
			textST.Text=((Pref)Prefs.HList["PracticeST"]).ValueString;
			textZip.Text=((Pref)Prefs.HList["PracticeZip"]).ValueString;
			string phone=((Pref)Prefs.HList["PracticePhone"]).ValueString;
			if(phone.Length==10 && Application.CurrentCulture.Name=="en-US"){
				textPhone.Text="("+phone.Substring(0,3)+")"+phone.Substring(3,3)+"-"+phone.Substring(6);
			}
			else{
				textPhone.Text=phone;
			}
			textBankNumber.Text=((Pref)Prefs.HList["PracticeBankNumber"]).ValueString;
			listProvider.Items.Clear();
			for(int i=0;i<Providers.List.Length;i++){
				listProvider.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum.ToString()==((Pref)Prefs.HList["PracticeDefaultProv"]).ValueString)
					listProvider.SelectedIndex=i;
			}
			listBillType.Items.Clear();
			for(int i=0;i<Defs.Short[(int)DefCat.BillingTypes].Length;i++){
				listBillType.Items.Add(Defs.Short[(int)DefCat.BillingTypes][i].ItemName);
				if(Defs.Short[(int)DefCat.BillingTypes][i].DefNum.ToString()
					==((Pref)Prefs.HList["PracticeDefaultBillType"]).ValueString)
					listBillType.SelectedIndex=i;
			}
			textTreatNote.Text=((Pref)Prefs.HList["TreatmentPlanNote"]).ValueString;
			checkTreatPlanShowGraphics.Checked=Prefs.GetBool("TreatPlanShowGraphics");
			checkTreatPlanShowCompleted.Checked=Prefs.GetBool("TreatPlanShowCompleted");
			checkTreatPlanShowIns.Checked=Prefs.GetBool("TreatPlanShowIns");
			if(((Pref)Prefs.HList["StatementShowCreditCard"]).ValueString=="1"){
				checkShowCC.Checked=true;
			}
			else checkShowCC.Checked=false;
			checkAutoRefresh.Checked=((Pref)Prefs.HList["AutoRefreshIsDisabled"]).ValueString=="1";
			textMainWindowTitle.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString;
			checkITooth.Checked=((Pref)Prefs.HList["UseInternationalToothNumbers"]).ValueString=="1";
			listPlaceService.Items.AddRange(Enum.GetNames(typeof(PlaceOfService)));
			listPlaceService.SelectedIndex
				=PIn.PInt(((Pref)Prefs.HList["DefaultProcedurePlaceService"]).ValueString);
			//listBillProv.Items.Clear(); //might use later
			//for(int i=0;i<Providers.List.Length;i++){
			//	listBillProv.Items.Add(Providers.List[i].Abbr);
			//	if(Providers.List[i].ProvNum.ToString()==((Pref)Prefs.HList["PracticeBillProv"]).ValueString)
			//		listBillProv.SelectedIndex=i;
			//}
		}

		private void textPhone_TextChanged(object sender, System.EventArgs e) {
			int cursor=textPhone.SelectionStart;
			int length=textPhone.Text.Length;
			textPhone.Text=TelephoneNumbers.AutoFormat(textPhone.Text);
			if(textPhone.Text.Length>length)
				cursor++;
			textPhone.SelectionStart=cursor;		
		}

		private void butTreatProv_Click(object sender, System.EventArgs e) {
			listBillProv.SelectedIndex=-1;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			string phone=textPhone.Text;
			if(Application.CurrentCulture.Name=="en-US"){
				phone=phone.Replace("(","");
				phone=phone.Replace(")","");
				phone=phone.Replace(" ","");
				phone=phone.Replace("-","");
				if(phone.Length!=0 && phone.Length!=10){
					MessageBox.Show(Lan.g(this,"Invalid phone"));
					return;
				}
			}
			Prefs.Cur.PrefName="PracticeTitle";
			Prefs.Cur.ValueString=textPracticeTitle.Text;
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="PracticeAddress";
			Prefs.Cur.ValueString=textAddress.Text;
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="PracticeAddress2";
			Prefs.Cur.ValueString=textAddress2.Text;
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="PracticeCity";
			Prefs.Cur.ValueString=textCity.Text;
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="PracticeST";
			Prefs.Cur.ValueString=textST.Text;
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="PracticeZip";
			Prefs.Cur.ValueString=textZip.Text;
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="PracticePhone";
			Prefs.Cur.ValueString=phone;
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="PracticeBankNumber";
			Prefs.Cur.ValueString=textBankNumber.Text;
			Prefs.UpdateCur();

			if(listProvider.SelectedIndex==-1//practice really needs a default prov
				&& Providers.List.Length > 0){
				listProvider.SelectedIndex=0;
			}

			if(listProvider.SelectedIndex!=-1){
				Prefs.Cur.PrefName="PracticeDefaultProv";
				Prefs.Cur.ValueString=Providers.List[listProvider.SelectedIndex].ProvNum.ToString();
				Prefs.UpdateCur();
			}
			
			if(listBillType.SelectedIndex!=-1){
				Prefs.Cur.PrefName="PracticeDefaultBillType";
				Prefs.Cur.ValueString=Defs.Short[(int)DefCat.BillingTypes][listBillType.SelectedIndex].DefNum.ToString();
				Prefs.UpdateCur();
			}

			Prefs.Cur.PrefName="TreatmentPlanNote";
			Prefs.Cur.ValueString=textTreatNote.Text;
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="TreatPlanShowGraphics";
			if(checkTreatPlanShowGraphics.Checked) Prefs.Cur.ValueString="1";
			else Prefs.Cur.ValueString="0";
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="TreatPlanShowCompleted";
			if(checkTreatPlanShowCompleted.Checked) Prefs.Cur.ValueString="1";
			else Prefs.Cur.ValueString="0";
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="TreatPlanShowIns";
			if(checkTreatPlanShowIns.Checked) Prefs.Cur.ValueString="1";
			else Prefs.Cur.ValueString="0";
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="StatementShowCreditCard";
			if(checkShowCC.Checked) Prefs.Cur.ValueString="1";
			else Prefs.Cur.ValueString="0";
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="AutoRefreshIsDisabled";
			if(checkAutoRefresh.Checked) Prefs.Cur.ValueString="1";
			else Prefs.Cur.ValueString="0";
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="MainWindowTitle";
			Prefs.Cur.ValueString=textMainWindowTitle.Text;
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="UseInternationalToothNumbers";
			if(checkITooth.Checked) Prefs.Cur.ValueString="1";
			else Prefs.Cur.ValueString="0";
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="DefaultProcedurePlaceService";
			Prefs.Cur.ValueString=listPlaceService.SelectedIndex.ToString();
			Prefs.UpdateCur();

			//Prefs.Cur.PrefName="PracticeBillProv"; //might use later
			//if(listBillProv.SelectedIndex==-1){
			//	Prefs.Cur.ValueString="0";
			//}
			//else{
			//	Prefs.Cur.ValueString=Providers.List[listBillProv.SelectedIndex].ProvNum.ToString();
			//}
			//Prefs.UpdateCur();

			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	

	}
}
