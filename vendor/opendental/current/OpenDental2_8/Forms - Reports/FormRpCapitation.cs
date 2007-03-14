using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.Reporting;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormRpCapitation : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.MonthCalendar date2;
		private System.Windows.Forms.MonthCalendar date1;
		private System.Windows.Forms.Label labelTO;
		private System.Windows.Forms.TextBox textCarrier;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PrintDialog printDialog1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormRpCapitation()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormRpCapitation));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.date2 = new System.Windows.Forms.MonthCalendar();
			this.date1 = new System.Windows.Forms.MonthCalendar();
			this.labelTO = new System.Windows.Forms.Label();
			this.textCarrier = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.printDialog1 = new System.Windows.Forms.PrintDialog();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(510, 338);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(510, 297);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// date2
			// 
			this.date2.Location = new System.Drawing.Point(303, 110);
			this.date2.Name = "date2";
			this.date2.TabIndex = 5;
			// 
			// date1
			// 
			this.date1.Location = new System.Drawing.Point(57, 110);
			this.date1.Name = "date1";
			this.date1.TabIndex = 4;
			// 
			// labelTO
			// 
			this.labelTO.Location = new System.Drawing.Point(269, 120);
			this.labelTO.Name = "labelTO";
			this.labelTO.Size = new System.Drawing.Size(26, 23);
			this.labelTO.TabIndex = 6;
			this.labelTO.Text = "TO";
			// 
			// textCarrier
			// 
			this.textCarrier.Location = new System.Drawing.Point(57, 61);
			this.textCarrier.Name = "textCarrier";
			this.textCarrier.Size = new System.Drawing.Size(260, 20);
			this.textCarrier.TabIndex = 7;
			this.textCarrier.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(57, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(362, 23);
			this.label1.TabIndex = 8;
			this.label1.Text = "Enter a few letters of the name of the insurance carrier";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormRpCapitation
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(605, 386);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textCarrier);
			this.Controls.Add(this.date2);
			this.Controls.Add(this.date1);
			this.Controls.Add(this.labelTO);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpCapitation";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Capitation Utilization Report";
			this.Load += new System.EventHandler(this.FormRpCapitation_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRpCapitation_Load(object sender, System.EventArgs e) {
			DateTime DateTimefirst=new DateTime(DateTime.Today.Year,DateTime.Today.Month,1);
			date1.SelectionRange=new SelectionRange(DateTimefirst,DateTimefirst);
			date2.SelectionRange=new SelectionRange(DateTimefirst.AddMonths(1).AddDays(-1)
				,DateTimefirst.AddMonths(1).AddDays(-1));
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			ODReport report=new ODReport();
			report.IsLandscape=true;
			report.AddTitle("CAPITATION UTILIZATION");
			report.AddSubTitle(((Pref)Prefs.HList["PracticeTitle"]).ValueString);
			report.AddSubTitle(date1.SelectionStart.ToString("d")+" - "+date2.SelectionStart.ToString("d"));
			report.AddParameter("carrier",ParameterValueKind.StringParameter,textCarrier.Text);
			report.AddParameter("date1",ParameterValueKind.DateParameter,date1.SelectionStart);
			report.AddParameter("date2",ParameterValueKind.DateParameter,date2.SelectionStart);
			report.Query=@"SELECT insplan.Carrier,CONCAT(patSub.LName,', ',patSub.FName) 
				,patSub.SSN,CONCAT(patPat.LName,', ',patPat.FName)
				,patPat.Birthdate,procedurelog.ADACode,procedurecode.Descript
				,procedurelog.ToothNum,procedurelog.Surf,procedurelog.ProcDate
				,procedurelog.ProcFee,procedurelog.CapCoPay
				FROM procedurelog,patient AS patSub,patient AS patPat,insplan,procedurecode
				WHERE procedurelog.PatNum = patPat.PatNum
				&& patPat.PriPlanNum = insplan.PlanNum
				&& insplan.Subscriber = patSub.PatNum
				&& procedurelog.ADACode = procedurecode.ADACode
				&& insplan.Carrier LIKE '%?carrier%'
				&& procedurelog.ProcDate >= '?date1'
				&& procedurelog.ProcDate <= '?date2'
				&& insplan.PlanType = 'c'
				&& procedurelog.ProcFee > 0
				&& procedurelog.CapCoPay != -1
				&& procedurelog.ProcStatus = 2";
			report.AddColumn("Carrier",150,FieldValueType.StringField);
			((FieldObject)report.GetLastRO(ReportObjectKind.FieldObject)).SuppressIfDuplicate=true;
			report.AddColumn("Subscriber",120,FieldValueType.StringField);
			((FieldObject)report.GetLastRO(ReportObjectKind.FieldObject)).SuppressIfDuplicate=true;
			report.AddColumn("Subsc SSN",70,FieldValueType.StringField);
			((FieldObject)report.GetLastRO(ReportObjectKind.FieldObject)).SuppressIfDuplicate=true;
			report.AddColumn("Patient",120,FieldValueType.StringField);
			report.AddColumn("Pat DOB",80,FieldValueType.DateTimeField);
			report.AddColumn("Code",50,FieldValueType.StringField);
			report.AddColumn("Proc Description",120,FieldValueType.StringField);
			report.AddColumn("Tth",30,FieldValueType.StringField);
			report.AddColumn("Surf",40,FieldValueType.StringField);
			report.AddColumn("Date",80,FieldValueType.DateTimeField);
			report.AddColumn("UCR Fee",70,FieldValueType.DoubleField);
			report.AddColumn("Co-Pay",70,FieldValueType.DoubleField);
			report.AddPageNum();
			report.SubmitQuery();
			//this loop can be replaced with a formula once that feature is complete:
			for(int i=0;i<report.ReportTable.Rows.Count;i++){
				if(PIn.PDouble(report.ReportTable.Rows[i][11].ToString())==-1){
					report.ReportTable.Rows[i][11]="0";
				}
			}
			FormReport FormR=new FormReport();
			FormR.Report=report;
			FormR.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		


	}
}




















