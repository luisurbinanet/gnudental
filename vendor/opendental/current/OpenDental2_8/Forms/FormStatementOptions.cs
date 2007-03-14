using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	///<summary></summary>
	public class FormStatementOptions : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public int[] PatNums;
		///<summary>This is the only parameter that gets passes to this form. But all the parameters are sent back out from this form.</summary>
		public DateTime FromDate;
		private System.Windows.Forms.Label label1;
		private OpenDental.ValidDate textDateFrom;
		private OpenDental.ValidDate textDateTo;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton radioFamAll;
		private System.Windows.Forms.RadioButton radioFamCur;
		private System.Windows.Forms.Button butAll;
		private System.Windows.Forms.Button but30;
		private System.Windows.Forms.Button but45;
		///<summary></summary>
		public DateTime ToDate;
		private System.Windows.Forms.CheckBox checkIncludeClaims;
		private System.Windows.Forms.Button butToday;
		private System.Windows.Forms.CheckBox checkSubtotals;
		///<summary></summary>
		public bool IncludeClaims;
		///<summary></summary>
		public bool SubtotalsOnly;

		///<summary></summary>
		public FormStatementOptions()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormStatementOptions));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textDateFrom = new OpenDental.ValidDate();
			this.textDateTo = new OpenDental.ValidDate();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioFamCur = new System.Windows.Forms.RadioButton();
			this.radioFamAll = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.butToday = new System.Windows.Forms.Button();
			this.but45 = new System.Windows.Forms.Button();
			this.but30 = new System.Windows.Forms.Button();
			this.butAll = new System.Windows.Forms.Button();
			this.checkIncludeClaims = new System.Windows.Forms.CheckBox();
			this.checkSubtotals = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(336, 258);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(336, 217);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 17);
			this.label1.TabIndex = 2;
			this.label1.Text = "From Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateFrom
			// 
			this.textDateFrom.Location = new System.Drawing.Point(113, 27);
			this.textDateFrom.Name = "textDateFrom";
			this.textDateFrom.Size = new System.Drawing.Size(84, 20);
			this.textDateFrom.TabIndex = 3;
			this.textDateFrom.Text = "";
			// 
			// textDateTo
			// 
			this.textDateTo.Location = new System.Drawing.Point(113, 55);
			this.textDateTo.Name = "textDateTo";
			this.textDateTo.Size = new System.Drawing.Size(85, 20);
			this.textDateTo.TabIndex = 5;
			this.textDateTo.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 17);
			this.label2.TabIndex = 4;
			this.label2.Text = "To Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioFamCur);
			this.groupBox1.Controls.Add(this.radioFamAll);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(25, 212);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 71);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Family Members";
			// 
			// radioFamCur
			// 
			this.radioFamCur.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioFamCur.Location = new System.Drawing.Point(17, 43);
			this.radioFamCur.Name = "radioFamCur";
			this.radioFamCur.Size = new System.Drawing.Size(144, 19);
			this.radioFamCur.TabIndex = 1;
			this.radioFamCur.Text = "Current Patient";
			// 
			// radioFamAll
			// 
			this.radioFamAll.Checked = true;
			this.radioFamAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioFamAll.Location = new System.Drawing.Point(17, 20);
			this.radioFamAll.Name = "radioFamAll";
			this.radioFamAll.Size = new System.Drawing.Size(144, 19);
			this.radioFamAll.TabIndex = 0;
			this.radioFamAll.TabStop = true;
			this.radioFamAll.Text = "All";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.butToday);
			this.groupBox2.Controls.Add(this.but45);
			this.groupBox2.Controls.Add(this.but30);
			this.groupBox2.Controls.Add(this.butAll);
			this.groupBox2.Controls.Add(this.textDateFrom);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.textDateTo);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(26, 8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(380, 121);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Date Range";
			// 
			// butToday
			// 
			this.butToday.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butToday.Location = new System.Drawing.Point(261, 13);
			this.butToday.Name = "butToday";
			this.butToday.Size = new System.Drawing.Size(96, 23);
			this.butToday.TabIndex = 9;
			this.butToday.Text = "Today Only";
			this.butToday.Click += new System.EventHandler(this.butToday_Click);
			// 
			// but45
			// 
			this.but45.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.but45.Location = new System.Drawing.Point(261, 65);
			this.but45.Name = "but45";
			this.but45.Size = new System.Drawing.Size(96, 23);
			this.but45.TabIndex = 8;
			this.but45.Text = "Last 45 Days";
			this.but45.Click += new System.EventHandler(this.but45_Click);
			// 
			// but30
			// 
			this.but30.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.but30.Location = new System.Drawing.Point(261, 39);
			this.but30.Name = "but30";
			this.but30.Size = new System.Drawing.Size(96, 23);
			this.but30.TabIndex = 7;
			this.but30.Text = "Last 30 Days";
			this.but30.Click += new System.EventHandler(this.but30_Click);
			// 
			// butAll
			// 
			this.butAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butAll.Location = new System.Drawing.Point(261, 91);
			this.butAll.Name = "butAll";
			this.butAll.Size = new System.Drawing.Size(96, 23);
			this.butAll.TabIndex = 6;
			this.butAll.Text = "All Dates";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// checkIncludeClaims
			// 
			this.checkIncludeClaims.Checked = true;
			this.checkIncludeClaims.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkIncludeClaims.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIncludeClaims.Location = new System.Drawing.Point(26, 141);
			this.checkIncludeClaims.Name = "checkIncludeClaims";
			this.checkIncludeClaims.Size = new System.Drawing.Size(257, 20);
			this.checkIncludeClaims.TabIndex = 8;
			this.checkIncludeClaims.Text = "Include Uncleared Claims";
			// 
			// checkSubtotals
			// 
			this.checkSubtotals.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkSubtotals.Location = new System.Drawing.Point(26, 167);
			this.checkSubtotals.Name = "checkSubtotals";
			this.checkSubtotals.Size = new System.Drawing.Size(257, 20);
			this.checkSubtotals.TabIndex = 9;
			this.checkSubtotals.Text = "Subtotals Only";
			// 
			// FormStatementOptions
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(443, 306);
			this.Controls.Add(this.checkSubtotals);
			this.Controls.Add(this.checkIncludeClaims);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormStatementOptions";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Statement Options";
			this.Load += new System.EventHandler(this.FormStatementOptions_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormStatementOptions_Load(object sender, System.EventArgs e) {
			if(FromDate.Year<1880)
				textDateFrom.Text="";
			else
				textDateFrom.Text=FromDate.ToShortDateString();
			textDateTo.Text=DateTime.Today.ToShortDateString();
		}

		private void butToday_Click(object sender, System.EventArgs e) {
			textDateFrom.Text=DateTime.Today.ToShortDateString();
			textDateTo.Text=DateTime.Today.ToShortDateString();
		}

		private void but30_Click(object sender, System.EventArgs e) {
			textDateFrom.Text=DateTime.Today.AddDays(-30).ToShortDateString();
			textDateTo.Text=DateTime.Today.ToShortDateString();
		}

		private void but45_Click(object sender, System.EventArgs e) {
			textDateFrom.Text=DateTime.Today.AddDays(-45).ToShortDateString();
			textDateTo.Text=DateTime.Today.ToShortDateString();
		}

		private void butAll_Click(object sender, System.EventArgs e) {
			textDateFrom.Text="";
			textDateTo.Text=DateTime.Today.ToShortDateString();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(  textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateTo.errorProvider1.GetError(textDateTo)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(textDateFrom.Text=="")
				FromDate=DateTime.MinValue;
			else
				FromDate=PIn.PDate(textDateFrom.Text);
			if(textDateTo.Text=="")
				ToDate=DateTime.Today;
			else
				ToDate=PIn.PDate(textDateTo.Text);
			IncludeClaims=checkIncludeClaims.Checked;
			SubtotalsOnly=checkSubtotals.Checked;
			if(radioFamAll.Checked){
				PatNums=new int[Patients.FamilyList.Length];
				for(int i=0;i<Patients.FamilyList.Length;i++){
					PatNums[i]=Patients.FamilyList[i].PatNum;
				}
			}
			else{
				PatNums=new int[1];
				PatNums[0]=Patients.Cur.PatNum;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		


	}
}





















