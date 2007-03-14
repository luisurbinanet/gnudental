using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Globalization;
using System.Windows.Forms;
using OpenDental.ReportingOld2;

namespace OpenDental
{
	/// <summary>
	/// Summary description for FormRpApptWithPhones.
	/// </summary>
	public class FormRpBirthday : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.GroupBox groupBox1;
		private OpenDental.UI.Button butRight;
		private OpenDental.UI.Button butLeft;
		private OpenDental.UI.Button butMonth;
		private System.Windows.Forms.TextBox textDateFrom;
		private System.Windows.Forms.TextBox textDateTo;
		private System.Windows.Forms.ErrorProvider errorProvider1;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormRpBirthday()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormRpBirthday));
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butRight = new OpenDental.UI.Button();
			this.butMonth = new OpenDental.UI.Button();
			this.butLeft = new OpenDental.UI.Button();
			this.textDateFrom = new System.Windows.Forms.TextBox();
			this.textDateTo = new System.Windows.Forms.TextBox();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(20, 99);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(82, 18);
			this.label3.TabIndex = 39;
			this.label3.Text = "To";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(22, 73);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(82, 18);
			this.label2.TabIndex = 37;
			this.label2.Text = "From";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(428, 228);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 44;
			this.butCancel.Text = "&Cancel";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(428, 188);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 43;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textDateTo);
			this.groupBox1.Controls.Add(this.butRight);
			this.groupBox1.Controls.Add(this.butMonth);
			this.groupBox1.Controls.Add(this.butLeft);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.textDateFrom);
			this.groupBox1.Location = new System.Drawing.Point(73, 24);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(286, 158);
			this.groupBox1.TabIndex = 45;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Date Range (without the year)";
			// 
			// butRight
			// 
			this.butRight.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRight.Autosize = true;
			this.butRight.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRight.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRight.Image = ((System.Drawing.Image)(resources.GetObject("butRight.Image")));
			this.butRight.Location = new System.Drawing.Point(206, 28);
			this.butRight.Name = "butRight";
			this.butRight.Size = new System.Drawing.Size(45, 26);
			this.butRight.TabIndex = 49;
			this.butRight.Click += new System.EventHandler(this.butRight_Click);
			// 
			// butMonth
			// 
			this.butMonth.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butMonth.Autosize = true;
			this.butMonth.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butMonth.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butMonth.Location = new System.Drawing.Point(96, 28);
			this.butMonth.Name = "butMonth";
			this.butMonth.Size = new System.Drawing.Size(101, 26);
			this.butMonth.TabIndex = 48;
			this.butMonth.Text = "Next Month";
			this.butMonth.Click += new System.EventHandler(this.butMonth_Click);
			// 
			// butLeft
			// 
			this.butLeft.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butLeft.Autosize = true;
			this.butLeft.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLeft.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLeft.Image = ((System.Drawing.Image)(resources.GetObject("butLeft.Image")));
			this.butLeft.Location = new System.Drawing.Point(42, 28);
			this.butLeft.Name = "butLeft";
			this.butLeft.Size = new System.Drawing.Size(45, 26);
			this.butLeft.TabIndex = 47;
			this.butLeft.Click += new System.EventHandler(this.butLeft_Click);
			// 
			// textDateFrom
			// 
			this.textDateFrom.Location = new System.Drawing.Point(110, 70);
			this.textDateFrom.Name = "textDateFrom";
			this.textDateFrom.Size = new System.Drawing.Size(71, 20);
			this.textDateFrom.TabIndex = 46;
			this.textDateFrom.Text = "";
			this.textDateFrom.Validating += new System.ComponentModel.CancelEventHandler(this.textDateFrom_Validating);
			// 
			// textDateTo
			// 
			this.textDateTo.Location = new System.Drawing.Point(110, 97);
			this.textDateTo.Name = "textDateTo";
			this.textDateTo.Size = new System.Drawing.Size(71, 20);
			this.textDateTo.TabIndex = 50;
			this.textDateTo.Text = "";
			this.textDateTo.Validating += new System.ComponentModel.CancelEventHandler(this.textDateTo_Validating);
			// 
			// errorProvider1
			// 
			this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.errorProvider1.ContainerControl = this;
			// 
			// FormRpBirthday
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(532, 276);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Name = "FormRpBirthday";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Birthday Report";
			this.Load += new System.EventHandler(this.FormRpBirthday_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRpBirthday_Load(object sender, System.EventArgs e){
			SetNextMonth();
		}

		private void butLeft_Click(object sender, System.EventArgs e) {
			if(errorProvider1.GetError(textDateFrom) != ""
				|| errorProvider1.GetError(textDateTo) != "") 
			{
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			DateTime dateFrom=PIn.PDate(textDateFrom.Text);
			DateTime dateTo=PIn.PDate(textDateTo.Text);
			bool toLastDay=false;
			if(CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(dateTo.Year,dateTo.Month)==dateTo.Day){
				toLastDay=true;
			}
			textDateFrom.Text=dateFrom.AddMonths(-1).ToString("MM/dd");
			textDateTo.Text=dateTo.AddMonths(-1).ToString("MM/dd");
			if(toLastDay){
				dateTo=PIn.PDate(textDateTo.Text);
				textDateTo.Text=new DateTime(dateTo.Year,dateTo.Month,
					CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(dateTo.Year,dateTo.Month))
					.ToString("MM/dd");
			}
		}

		private void butMonth_Click(object sender, System.EventArgs e) {
			SetNextMonth();
		}

		private void butRight_Click(object sender, System.EventArgs e) {
			if(errorProvider1.GetError(textDateFrom) != ""
				|| errorProvider1.GetError(textDateTo) != "") 
			{
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			DateTime dateFrom=PIn.PDate(textDateFrom.Text);
			DateTime dateTo=PIn.PDate(textDateTo.Text);
			textDateFrom.Text=dateFrom.AddMonths(-1).ToShortDateString();
			textDateTo.Text=dateTo.AddMonths(-1).ToShortDateString();
			bool toLastDay=false;
			if(CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(dateTo.Year,dateTo.Month)==dateTo.Day){
				toLastDay=true;
			}
			textDateFrom.Text=dateFrom.AddMonths(1).ToString("MM/dd");
			textDateTo.Text=dateTo.AddMonths(1).ToString("MM/dd");
			if(toLastDay){
				dateTo=PIn.PDate(textDateTo.Text);
				textDateTo.Text=new DateTime(dateTo.Year,dateTo.Month,
					CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(dateTo.Year,dateTo.Month))
					.ToString("MM/dd");
			}
		}

		private void SetNextMonth(){
			textDateFrom.Text
				=new DateTime(DateTime.Today.AddMonths(1).Year,DateTime.Today.AddMonths(1).Month,1)
				.ToString("MM/dd");
			textDateTo.Text
				=new DateTime(DateTime.Today.AddMonths(2).Year,DateTime.Today.AddMonths(2).Month,1).AddDays(-1)
				.ToString("MM/dd");
			errorProvider1.SetError(textDateFrom,"");
			errorProvider1.SetError(textDateTo,"");
		}

		private void textDateFrom_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			try{
				DateTime date=DateTime.Parse(textDateFrom.Text);
				textDateFrom.Text=date.ToString("MM/dd");
				errorProvider1.SetError(textDateFrom,"");
			}
			catch{
				errorProvider1.SetError(textDateFrom,"Invalid Date");	
			}
		}

		private void textDateTo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			try{
				DateTime date=DateTime.Parse(textDateTo.Text);
				textDateTo.Text=date.ToString("MM/dd");
				errorProvider1.SetError(textDateTo,"");
			}
			catch{
				errorProvider1.SetError(textDateTo,"Invalid Date");	
			}
		}


		private void butOK_Click(object sender, System.EventArgs e){
			if(errorProvider1.GetError(textDateFrom) != ""
				|| errorProvider1.GetError(textDateTo) != "") 
			{
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			DateTime dateFrom=PIn.PDate(textDateFrom.Text);
			DateTime dateTo=PIn.PDate(textDateTo.Text);
			if(dateTo < dateFrom) 
			{
				MessageBox.Show(Lan.g(this,"To date cannot be before From date."));
				return;
			}
			ReportOld2 report=new ReportOld2();
			report.ReportName=Lan.g(this,"Birthdays");
			report.AddTitle(Lan.g(this,"Birthdays"));
			report.AddSubTitle(Prefs.GetString("PracticeTitle"));
			report.AddSubTitle(dateFrom.ToString("MM/dd")+" - "+dateTo.ToString("MM/dd"));
			report.Query=@"SELECT LName,FName,Address,Address2,City,State,Zip,Birthdate,Birthdate
				FROM patient 
				WHERE SUBSTRING(Birthdate,6,5) >= '"+dateFrom.ToString("MM-dd")+"' "
				+"AND SUBSTRING(Birthdate,6,5) <= '"+dateTo.ToString("MM-dd")+"' "
				+"AND PatStatus=0	ORDER BY LName,FName";
			report.AddColumn("LName",90,FieldValueType.String);
			report.AddColumn("FName",90,FieldValueType.String);
			report.AddColumn("Address",90,FieldValueType.String);
			report.AddColumn("Address2",90,FieldValueType.String);
			report.AddColumn("City",75,FieldValueType.String);
			report.AddColumn("State",60,FieldValueType.String);
			report.AddColumn("Zip",75,FieldValueType.String);
			report.AddColumn("Birthdate", 75, FieldValueType.Date);
			report.GetLastRO(ReportObjectKind.FieldObject).FormatString="d";
			report.AddColumn("Age", 45, FieldValueType.Age);
			report.AddPageNum();
			if(!report.SubmitQuery()){
				return;
			}
			FormReportOld2 FormR=new FormReportOld2(report);
			FormR.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		













		

		

		
	}
}
