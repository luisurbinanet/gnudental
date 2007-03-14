/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
//#define TRIALONLY
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;


namespace OpenDental{
///<summary></summary>
	public class FormPatientSelect : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.Container components = null;
		private Patients Patients;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butAddPt;
		/// <summary>Selection mode only.
		/// Use when you want to specify a patient without changing the current patient.</summary>
		public bool OnlyChangingFam;//AKA selection mode only
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textLName;
		private System.Windows.Forms.TextBox textFName;
		private System.Windows.Forms.TextBox textAddress;
		private System.Windows.Forms.TextBox textHmPhone;
		private System.Windows.Forms.Label labelMore;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox checkHideInactive;
		private System.Windows.Forms.GroupBox groupAddPt;
		private System.Windows.Forms.CheckBox checkGuarantors;
		private System.Windows.Forms.TextBox textCity;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textState;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textPatNum;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textChartNumber;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox textSSN;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ListBox listBillingTypes;
		private System.Windows.Forms.CheckBox checkUseSearch;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button butSearch;
		private System.Windows.Forms.DataGrid grid2;
		///<summary>When closing the form, this indicates whether a new patient was added from within this form.</summary>
		public bool NewPatientAdded;

		///<summary></summary>
		public FormPatientSelect(){
			InitializeComponent();//required first
			//tb2.CellClicked += new OpenDental.ContrTable.CellEventHandler(tb2_CellClicked);
			//tb2.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tb2_CellDoubleClicked);
			Patients=new Patients();
			Lan.C(this, new System.Windows.Forms.Control[] 
			{
				label1,
				label2,
				label3,
				label4,
				label5,
				label7,
				label8,
				label9,
				label10,
				label11,
				label12,
				this.butAddPt,
				this.groupAddPt,
				this.groupBox2,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
			});

		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if (components != null){
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
			this.textLName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupAddPt = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.butAddPt = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.listBillingTypes = new System.Windows.Forms.ListBox();
			this.textSSN = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.textChartNumber = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textPatNum = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textState = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textCity = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.checkGuarantors = new System.Windows.Forms.CheckBox();
			this.checkHideInactive = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textAddress = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textHmPhone = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textFName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.labelMore = new System.Windows.Forms.Label();
			this.checkUseSearch = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butSearch = new System.Windows.Forms.Button();
			this.grid2 = new System.Windows.Forms.DataGrid();
			this.groupAddPt.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grid2)).BeginInit();
			this.SuspendLayout();
			// 
			// textLName
			// 
			this.textLName.Location = new System.Drawing.Point(111, 43);
			this.textLName.Name = "textLName";
			this.textLName.Size = new System.Drawing.Size(90, 20);
			this.textLName.TabIndex = 0;
			this.textLName.Text = "";
			this.textLName.TextChanged += new System.EventHandler(this.textLName_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(17, 46);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(95, 12);
			this.label1.TabIndex = 3;
			this.label1.Text = "Last Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupAddPt
			// 
			this.groupAddPt.Controls.Add(this.label2);
			this.groupAddPt.Controls.Add(this.butAddPt);
			this.groupAddPt.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupAddPt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupAddPt.Location = new System.Drawing.Point(729, 461);
			this.groupAddPt.Name = "groupAddPt";
			this.groupAddPt.Size = new System.Drawing.Size(207, 117);
			this.groupAddPt.TabIndex = 1;
			this.groupAddPt.TabStop = false;
			this.groupAddPt.Text = "Add New Family:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(188, 40);
			this.label2.TabIndex = 11;
			this.label2.Text = "This creates a new family.   Please make sure they are not already in the system " +
				"first";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// butAddPt
			// 
			this.butAddPt.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butAddPt.Location = new System.Drawing.Point(68, 26);
			this.butAddPt.Name = "butAddPt";
			this.butAddPt.TabIndex = 0;
			this.butAddPt.Text = "&Add";
			this.butAddPt.Click += new System.EventHandler(this.butAddPt_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(840, 585);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(76, 26);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(840, 621);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(76, 26);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textChartNumber);
			this.groupBox2.Controls.Add(this.listBillingTypes);
			this.groupBox2.Controls.Add(this.textSSN);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.textPatNum);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.textState);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.textCity);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.checkGuarantors);
			this.groupBox2.Controls.Add(this.checkHideInactive);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.textAddress);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.textHmPhone);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.textFName);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.textLName);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(729, 8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(207, 341);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Search by:";
			// 
			// listBillingTypes
			// 
			this.listBillingTypes.Location = new System.Drawing.Point(63, 226);
			this.listBillingTypes.Name = "listBillingTypes";
			this.listBillingTypes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listBillingTypes.Size = new System.Drawing.Size(138, 56);
			this.listBillingTypes.TabIndex = 9;
			this.listBillingTypes.SelectedIndexChanged += new System.EventHandler(this.listBillingTypes_SelectedIndexChanged);
			// 
			// textSSN
			// 
			this.textSSN.Location = new System.Drawing.Point(111, 163);
			this.textSSN.Name = "textSSN";
			this.textSSN.Size = new System.Drawing.Size(90, 20);
			this.textSSN.TabIndex = 6;
			this.textSSN.Text = "";
			this.textSSN.TextChanged += new System.EventHandler(this.textSSN_TextChanged);
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(18, 167);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(94, 12);
			this.label12.TabIndex = 24;
			this.label12.Text = "SSN";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(7, 227);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(52, 46);
			this.label11.TabIndex = 21;
			this.label11.Text = "Billing Types";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textChartNumber
			// 
			this.textChartNumber.Location = new System.Drawing.Point(111, 203);
			this.textChartNumber.Name = "textChartNumber";
			this.textChartNumber.Size = new System.Drawing.Size(90, 20);
			this.textChartNumber.TabIndex = 8;
			this.textChartNumber.Text = "";
			this.textChartNumber.TextChanged += new System.EventHandler(this.textChartNumber_TextChanged);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(19, 207);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(94, 12);
			this.label10.TabIndex = 20;
			this.label10.Text = "Chart Number";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPatNum
			// 
			this.textPatNum.Location = new System.Drawing.Point(111, 183);
			this.textPatNum.Name = "textPatNum";
			this.textPatNum.Size = new System.Drawing.Size(90, 20);
			this.textPatNum.TabIndex = 7;
			this.textPatNum.Text = "";
			this.textPatNum.TextChanged += new System.EventHandler(this.textPatNum_TextChanged);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(20, 187);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(94, 12);
			this.label9.TabIndex = 18;
			this.label9.Text = "Patient Number";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textState
			// 
			this.textState.Location = new System.Drawing.Point(111, 143);
			this.textState.Name = "textState";
			this.textState.Size = new System.Drawing.Size(90, 20);
			this.textState.TabIndex = 5;
			this.textState.Text = "";
			this.textState.TextChanged += new System.EventHandler(this.textState_TextChanged);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(17, 147);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(94, 12);
			this.label8.TabIndex = 16;
			this.label8.Text = "State";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCity
			// 
			this.textCity.Location = new System.Drawing.Point(111, 123);
			this.textCity.Name = "textCity";
			this.textCity.Size = new System.Drawing.Size(90, 20);
			this.textCity.TabIndex = 4;
			this.textCity.Text = "";
			this.textCity.TextChanged += new System.EventHandler(this.textCity_TextChanged);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(15, 125);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(94, 14);
			this.label7.TabIndex = 14;
			this.label7.Text = "City";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkGuarantors
			// 
			this.checkGuarantors.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkGuarantors.Location = new System.Drawing.Point(11, 293);
			this.checkGuarantors.Name = "checkGuarantors";
			this.checkGuarantors.Size = new System.Drawing.Size(163, 17);
			this.checkGuarantors.TabIndex = 10;
			this.checkGuarantors.Text = "Guarantors Only";
			this.checkGuarantors.CheckedChanged += new System.EventHandler(this.checkGuarantors_CheckedChanged);
			// 
			// checkHideInactive
			// 
			this.checkHideInactive.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkHideInactive.Location = new System.Drawing.Point(11, 313);
			this.checkHideInactive.Name = "checkHideInactive";
			this.checkHideInactive.Size = new System.Drawing.Size(161, 17);
			this.checkHideInactive.TabIndex = 11;
			this.checkHideInactive.Text = "Hide Inactive Patients";
			this.checkHideInactive.CheckedChanged += new System.EventHandler(this.checkHideInactive_CheckedChanged);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(4, 18);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(200, 16);
			this.label6.TabIndex = 10;
			this.label6.Text = "Hint: enter values in multiple boxes.";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textAddress
			// 
			this.textAddress.Location = new System.Drawing.Point(111, 103);
			this.textAddress.Name = "textAddress";
			this.textAddress.Size = new System.Drawing.Size(90, 20);
			this.textAddress.TabIndex = 3;
			this.textAddress.Text = "";
			this.textAddress.TextChanged += new System.EventHandler(this.textAddress_TextChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(17, 106);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(94, 12);
			this.label5.TabIndex = 9;
			this.label5.Text = "Address";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textHmPhone
			// 
			this.textHmPhone.Location = new System.Drawing.Point(111, 83);
			this.textHmPhone.Name = "textHmPhone";
			this.textHmPhone.Size = new System.Drawing.Size(90, 20);
			this.textHmPhone.TabIndex = 2;
			this.textHmPhone.Text = "";
			this.textHmPhone.TextChanged += new System.EventHandler(this.textHmPhone_TextChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(18, 86);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(95, 12);
			this.label4.TabIndex = 7;
			this.label4.Text = "Home Phone";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textFName
			// 
			this.textFName.Location = new System.Drawing.Point(111, 63);
			this.textFName.Name = "textFName";
			this.textFName.Size = new System.Drawing.Size(90, 20);
			this.textFName.TabIndex = 1;
			this.textFName.Text = "";
			this.textFName.TextChanged += new System.EventHandler(this.textFName_TextChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 67);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(95, 12);
			this.label3.TabIndex = 5;
			this.label3.Text = "First Name";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelMore
			// 
			this.labelMore.Location = new System.Drawing.Point(723, 646);
			this.labelMore.Name = "labelMore";
			this.labelMore.Size = new System.Drawing.Size(68, 16);
			this.labelMore.TabIndex = 5;
			this.labelMore.Text = "(more)";
			this.labelMore.Visible = false;
			// 
			// checkUseSearch
			// 
			this.checkUseSearch.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkUseSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkUseSearch.Location = new System.Drawing.Point(13, 60);
			this.checkUseSearch.Name = "checkUseSearch";
			this.checkUseSearch.Size = new System.Drawing.Size(184, 35);
			this.checkUseSearch.TabIndex = 6;
			this.checkUseSearch.Text = "Use Search Button (may take longer to load)";
			this.checkUseSearch.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkUseSearch.Click += new System.EventHandler(this.checkUseSearch_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butSearch);
			this.groupBox1.Controls.Add(this.checkUseSearch);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(729, 355);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(207, 100);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Search behavior";
			// 
			// butSearch
			// 
			this.butSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butSearch.Location = new System.Drawing.Point(68, 27);
			this.butSearch.Name = "butSearch";
			this.butSearch.TabIndex = 7;
			this.butSearch.Text = "&Search";
			this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
			// 
			// grid2
			// 
			this.grid2.AllowNavigation = false;
			this.grid2.DataMember = "";
			this.grid2.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.grid2.Location = new System.Drawing.Point(5, 5);
			this.grid2.Name = "grid2";
			this.grid2.ReadOnly = true;
			this.grid2.Size = new System.Drawing.Size(718, 659);
			this.grid2.TabIndex = 8;
			this.grid2.DoubleClick += new System.EventHandler(this.grid2_DoubleClick);
			this.grid2.CurrentCellChanged += new System.EventHandler(this.grid2_CurrentCellChanged);
			// 
			// FormPatientSelect
			// 
			this.AcceptButton = this.butOK;
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(944, 668);
			this.Controls.Add(this.grid2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.labelMore);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.groupAddPt);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPatientSelect";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Patient";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormSelectPatient_KeyDown);
			this.Load += new System.EventHandler(this.FormSelectPatient_Load);
			this.groupAddPt.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grid2)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		///<summary></summary>
		public void FormSelectPatient_Load(object sender, System.EventArgs e){
			if(OnlyChangingFam){
				groupAddPt.Visible=false;
			}
			for(int i=0;i<Defs.Short[(int)DefCat.BillingTypes].Length;i++){
				listBillingTypes.Items.Add(Defs.Short[(int)DefCat.BillingTypes][i].ItemName);
			}
			FillSearchOption();
			SetGridStyles();
			if(!checkUseSearch.Checked)
				//tb2.ResetRows(0);
				//tb2.LayoutTables();
			//}
			//else{
				FillList();
			//}
		}

		private void FillSearchOption(){
			checkUseSearch.Checked=((Pref)Prefs.HList["PatientSelectUsesSearchButton"]).ValueString=="1";
			if(checkUseSearch.Checked)
				butSearch.Enabled=true;
			else
				butSearch.Enabled=false;
		}

		private void SetGridStyles(){
			DataGridTableStyle ts=new DataGridTableStyle();
			ts.GridLineColor=Color.Silver;
			ts.ReadOnly=true;
			//grid2..PreferredRowHeight=13;
			//ts.PreferredRowHeight=25;			

			DataGridTextBoxColumn cstyle;

			cstyle=new DataGridTextBoxColumn();
			cstyle.MappingName="lname";
			cstyle.HeaderText=Lan.g(this,"Last Name");
			cstyle.Width=80;
			ts.GridColumnStyles.Add(cstyle);

			cstyle=new DataGridTextBoxColumn();
			cstyle.MappingName="fname";
			cstyle.HeaderText=Lan.g(this,"First Name");
			cstyle.Width=80;
			ts.GridColumnStyles.Add(cstyle);

			cstyle=new DataGridTextBoxColumn();
			cstyle.MappingName="middlei";
			cstyle.HeaderText=Lan.g(this,"MI");
			cstyle.Width=25;
			ts.GridColumnStyles.Add(cstyle);

			cstyle=new DataGridTextBoxColumn();
			cstyle.MappingName="preferred";
			cstyle.HeaderText=Lan.g(this,"Pref'd Name");
			cstyle.Width=80;
			ts.GridColumnStyles.Add(cstyle);

			cstyle=new DataGridTextBoxColumn();
			cstyle.MappingName="birthdate";
			cstyle.HeaderText=Lan.g(this,"Age");
			cstyle.Width=40;
			ts.GridColumnStyles.Add(cstyle);

			cstyle=new DataGridTextBoxColumn();
			cstyle.MappingName="ssn";
			cstyle.HeaderText=Lan.g(this,"SSN");
			cstyle.Width=65;
			ts.GridColumnStyles.Add(cstyle);

			cstyle=new DataGridTextBoxColumn();
			cstyle.MappingName="hmphone";
			cstyle.HeaderText=Lan.g(this,"Phone");
			cstyle.Width=90;
			ts.GridColumnStyles.Add(cstyle);

			cstyle=new DataGridTextBoxColumn();
			cstyle.MappingName="address";
			cstyle.HeaderText=Lan.g(this,"Address");
			cstyle.Width=100;
			ts.GridColumnStyles.Add(cstyle);

			cstyle=new DataGridTextBoxColumn();
			cstyle.MappingName="patstatus";
			cstyle.HeaderText=Lan.g(this,"Status");
			cstyle.Width=65;
			ts.GridColumnStyles.Add(cstyle);

			cstyle=new DataGridTextBoxColumn();
			cstyle.MappingName="billingtype";
			cstyle.HeaderText=Lan.g(this,"Bill Type");
			cstyle.Width=90;
			ts.GridColumnStyles.Add(cstyle);
			
			cstyle=new DataGridTextBoxColumn();
			cstyle.MappingName="patnum";
			cstyle.HeaderText=Lan.g(this,"PatNum");
			cstyle.Width=50;
			ts.GridColumnStyles.Add(cstyle);
			
			cstyle=new DataGridTextBoxColumn();
			cstyle.MappingName="chartnumber";
			cstyle.HeaderText=Lan.g(this,"ChartNum");
			cstyle.Width=60;
			ts.GridColumnStyles.Add(cstyle);
			
			cstyle=new DataGridTextBoxColumn();
			cstyle.MappingName="city";
			cstyle.HeaderText=Lan.g(this,"City");
			cstyle.Width=80;
			ts.GridColumnStyles.Add(cstyle);
			
			cstyle=new DataGridTextBoxColumn();
			cstyle.MappingName="state";
			cstyle.HeaderText=Lan.g(this,"State");
			cstyle.Width=55;
			ts.GridColumnStyles.Add(cstyle);

			grid2.TableStyles.Clear();
			grid2.TableStyles.Add(ts);
		}

		private void textLName_TextChanged(object sender, System.EventArgs e){
			//if(textLastName.TextLength==1 && !String.Equals(previousLetter,textLastName.Text)){
			//	previousLetter=textLastName.Text;
				//FillList();
			//}
			//ScrollList();	
			OnDataEntered();
		}

		private void textFName_TextChanged(object sender, System.EventArgs e) {
			OnDataEntered();
		}

		private void textHmPhone_TextChanged(object sender, System.EventArgs e) {
			OnDataEntered();
		}

		private void textAddress_TextChanged(object sender, System.EventArgs e) {
			OnDataEntered();
		}

		private void textCity_TextChanged(object sender, System.EventArgs e) {
			OnDataEntered();
		}

		private void textState_TextChanged(object sender, System.EventArgs e) {
			OnDataEntered();
		}

		private void textSSN_TextChanged(object sender, System.EventArgs e) {
			OnDataEntered();
		}

		private void textPatNum_TextChanged(object sender, System.EventArgs e) {
			OnDataEntered();
		}

		private void textChartNumber_TextChanged(object sender, System.EventArgs e) {
			OnDataEntered();
		}

		private void listBillingTypes_SelectedIndexChanged(object sender, System.EventArgs e) {
			OnDataEntered();
		}

		private void checkGuarantors_CheckedChanged(object sender, System.EventArgs e) {
			OnDataEntered();
		}

		private void checkHideInactive_CheckedChanged(object sender, System.EventArgs e) {
			OnDataEntered();
		}

		private void checkUseSearch_Click(object sender, System.EventArgs e) {
			Prefs.Cur.PrefName="PatientSelectUsesSearchButton";
			if(checkUseSearch.Checked)
				Prefs.Cur.ValueString="1";
			else
				Prefs.Cur.ValueString="0";
			Prefs.UpdateCur();
			Prefs.Refresh();
			//simply not important enough to send an update to the other computers.
			FillSearchOption();
			if(!checkUseSearch.Checked)
				FillList();
		}

		private void butSearch_Click(object sender, System.EventArgs e) {
			FillList();
		}

		private void OnDataEntered(){
			if(!checkUseSearch.Checked){
				FillList();
			}
		}

		private void FillList(){
			int[] selectedBillingTypes=new int[listBillingTypes.SelectedIndices.Count];
			for(int i=0;i<selectedBillingTypes.Length;i++){
				selectedBillingTypes[i]
					=Defs.Short[(int)DefCat.BillingTypes][listBillingTypes.SelectedIndices[i]].DefNum;
			}
			if(Patients.GetPtDataTable(!checkUseSearch.Checked,textLName.Text,textFName.Text
				,textHmPhone.Text,textAddress.Text,checkHideInactive.Checked,textCity.Text,textState.Text
				,textSSN.Text,textPatNum.Text,textChartNumber.Text,selectedBillingTypes,checkGuarantors.Checked))
			{//if limit and there are more rows
				labelMore.Visible=true;
			}
			else{
				labelMore.Visible=false;
			}
			/*
			tb2.ResetRows(Patients.PtList.Length);
			tb2.SetGridColor(Color.Gray);
			for(int i=0;i<Patients.PtList.Length;i++){
				tb2.Cell[0,i]=Patients.PtList[i].LName;
				tb2.Cell[1,i]=Patients.PtList[i].FName;
				tb2.Cell[2,i]=Patients.PtList[i].MiddleI;
				tb2.Cell[3,i]=Patients.PtList[i].Preferred;
				tb2.Cell[4,i]=Patients.PtList[i].Age;
				tb2.Cell[5,i]=Patients.PtList[i].SSN;
				tb2.Cell[6,i]=Patients.PtList[i].HmPhone;
				tb2.Cell[7,i]=Patients.PtList[i].Address;
				tb2.Cell[8,i]=Lan.g("enumPatientStatus",Patients.PtList[i].PatStatus.ToString());
				tb2.Cell[9,i]=Defs.GetName(DefCat.BillingTypes,Patients.PtList[i].BillingType);
			}
			//MessageBox.Show(tb2.Cell.GetLength(2).ToString());
			tb2.SelectedRow=-1;
			tb2.LayoutTables();*/
			grid2.SetDataBinding(Patients.PtDataTable,"");
			if(Patients.PtDataTable.Rows.Count>grid2.CurrentCell.RowNumber){
				grid2.Select(grid2.CurrentCell.RowNumber);
			}
			//MessageBox.Show(Patients.PtList[Patients.PtList.Length-1].LName);
		}

		//private void ScrollList(){
			//if(Patients.PtList.Length==0) return;
			//int newRow=0;
			//for(int i=Patients.PtList.Length-1;i>=0;i--){
			//	if(Patients.PtList[i].LName.Length>=textLastName.Text.Length){
			//		if(String.Compare(Patients.PtList[i].LName.Substring(0,textLastName.Text.Length)
			//			,textLastName.Text,true)>=0){
			//			newRow=i;
			//		}
			//check with shorter names
			//	}
			//}
			//SetRow(newRow);
			//going to need a lot of patients in DB to test scrolling
			//if(Patients.PtList
		//}

		//private void SetRow(int row){
			//if(tb2.SelectedRow!=-1)
			//	tb2.ColorRow(tb2.SelectedRow,Color.White);
			//tb2.SelectedRow=row;
			//tb2.ColorRow(row,Color.LightGray);
		//}

		private void grid2_CurrentCellChanged(object sender, System.EventArgs e) {
			grid2.Select(grid2.CurrentCell.RowNumber);
			//PatSelected();
		}

		private void grid2_DoubleClick(object sender, System.EventArgs e) {
			PatSelected();
		}

		private void FormSelectPatient_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			//if(tb2.SelectedRow==-1) return;
			//if (e.KeyCode==Keys.Up && tb2.SelectedRow!=0){
			//	SetRow(tb2.SelectedRow-1);
			//}
			//if (e.KeyCode==Keys.Down && tb2.SelectedRow!=Patients.PtList.Length-1){
			//	SetRow(tb2.SelectedRow+1);
			//}
		}

		/*
		private void textBoxes_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if(tb2.SelectedRow==-1) return;
			if (e.KeyCode==Keys.Up && tb2.SelectedRow!=0){
				SetRow(tb2.SelectedRow-1);
			}
			if (e.KeyCode==Keys.Down && tb2.SelectedRow!=Patients.PtList.Length-1){
				SetRow(tb2.SelectedRow+1);
			}
		}

		private void textBoxes_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (e.KeyCode==Keys.Up)
				textLastName.SelectionStart=20;
		}*/

		private void butAddPt_Click(object sender, System.EventArgs e){
			#if(TRIALONLY)
				MessageBox.Show("Trial version.  Maximum 30 patients");
				if(Patients.GetNumberPatients()>30){
					MessageBox.Show("Maximum reached");
					return;
				}
			#endif
			Patients.Cur=new Patient();
			//Later: Make it dummy proof by testing for whether patient is in DB already.
			Patients.Cur.PatStatus=PatientStatus.Patient;
			Patients.Cur.RecallInterval=6;
			FormPatientEdit FormPE=new FormPatientEdit();
			FormPE.IsNew=true;
			FormPE.ShowDialog();
			if(FormPE.DialogResult==DialogResult.OK){
				Patients.PatIsLoaded=true;
				//patnum set in dialog
				this.NewPatientAdded=true;
				DialogResult=DialogResult.OK;
			}
		}

		private void PatSelected(){
			Patients.PatIsLoaded=true;
			Patients.Cur.PatNum=PIn.PInt(Patients.PtDataTable.Rows[grid2.CurrentRowIndex][0].ToString());
				//=Patients.PtList[tb2.SelectedRow].PatNum;
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e){
			if(grid2.CurrentRowIndex!=-1){
				PatSelected();
			}
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			//there is no need to remember the original patNum because you can't change it without
			//causing the dialog to close
			DialogResult=DialogResult.Cancel;
		}

	

		

		

		
	

		

		

	}
}
