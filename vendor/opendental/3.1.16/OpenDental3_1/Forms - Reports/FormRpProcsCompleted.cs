using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormRpPHRawProc : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.MonthCalendar date2;
		private System.Windows.Forms.MonthCalendar date1;
		private System.Windows.Forms.Label labelTO;
		private System.ComponentModel.Container components = null;
		private FormQuery FormQuery2;

		///<summary></summary>
		public FormRpPHRawProc(){
			InitializeComponent();
			Lan.F(this);
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
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.date2 = new System.Windows.Forms.MonthCalendar();
			this.date1 = new System.Windows.Forms.MonthCalendar();
			this.labelTO = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(523, 328);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(523, 296);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// date2
			// 
			this.date2.Location = new System.Drawing.Point(290, 67);
			this.date2.MaxSelectionCount = 1;
			this.date2.Name = "date2";
			this.date2.TabIndex = 2;
			// 
			// date1
			// 
			this.date1.Location = new System.Drawing.Point(34, 67);
			this.date1.MaxSelectionCount = 1;
			this.date1.Name = "date1";
			this.date1.TabIndex = 1;
			// 
			// labelTO
			// 
			this.labelTO.Location = new System.Drawing.Point(250, 75);
			this.labelTO.Name = "labelTO";
			this.labelTO.Size = new System.Drawing.Size(24, 23);
			this.labelTO.TabIndex = 28;
			this.labelTO.Text = "TO";
			// 
			// FormRpPHRawProc
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(616, 366);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.date2);
			this.Controls.Add(this.date1);
			this.Controls.Add(this.labelTO);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpPHRawProc";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Raw Procedure Data";
			this.Load += new System.EventHandler(this.FormRpPHRawScreen_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRpPHRawScreen_Load(object sender, System.EventArgs e) {
			DateTime today=DateTime.Today;
			//will start out 1st through 30th of previous month
			date1.SelectionStart=new DateTime(today.Year,today.Month,1).AddMonths(-1);
			date2.SelectionStart=new DateTime(today.Year,today.Month,1).AddDays(-1);
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			Queries.CurReport=new ReportOld();
			Queries.CurReport.Query=@"SELECT ScreenDate,ProvName,County,county.CountyCode,
				GradeSchool,school.SchoolCode,PlaceService,GradeLevel,Age,Birthdate,Race,Gender,Urgency,
				HasCaries,EarlyChildCaries,CariesExperience,ExistingSealants,NeedsSealants,MissingAllTeeth,
				Comments FROM screen
				LEFT JOIN school ON screen.GradeSchool=school.SchoolName
				LEFT JOIN county ON screen.County=county.CountyName
				WHERE ScreenDate >= '"+POut.PDate(date1.SelectionStart)+"' "
				+"&& ScreenDate <= '" +POut.PDate(date2.SelectionStart)+"'";
			FormQuery2=new FormQuery();
			FormQuery2.textTitle.Text="RawProcedureData"+DateTime.Today.ToString("MMddyyyy");
			//FormQuery2.IsReport=true;
			//FormQuery2.SubmitReportQuery();			
			FormQuery2.SubmitQuery();
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}



	}
}
