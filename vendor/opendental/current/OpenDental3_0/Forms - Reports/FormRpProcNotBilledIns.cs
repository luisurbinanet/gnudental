using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormRpProcNotBilledIns : System.Windows.Forms.Form{
		private System.Windows.Forms.MonthCalendar date2;
		private System.Windows.Forms.MonthCalendar date1;
		private System.Windows.Forms.Label labelTO;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.ComponentModel.Container components = null;
		private FormQuery FormQuery2;

		///<summary></summary>
		public FormRpProcNotBilledIns(){
			InitializeComponent();
 			Lan.C(this, new System.Windows.Forms.Control[] {
				date2,
				date1,
				labelTO,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
			});  
		}

		///<summary></summary>
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
			this.date2 = new System.Windows.Forms.MonthCalendar();
			this.date1 = new System.Windows.Forms.MonthCalendar();
			this.labelTO = new System.Windows.Forms.Label();
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// date2
			// 
			this.date2.Location = new System.Drawing.Point(288, 112);
			this.date2.Name = "date2";
			this.date2.TabIndex = 2;
			// 
			// date1
			// 
			this.date1.Location = new System.Drawing.Point(32, 112);
			this.date1.Name = "date1";
			this.date1.TabIndex = 1;
			// 
			// labelTO
			// 
			this.labelTO.Location = new System.Drawing.Point(240, 120);
			this.labelTO.Name = "labelTO";
			this.labelTO.Size = new System.Drawing.Size(24, 23);
			this.labelTO.TabIndex = 10;
			this.labelTO.Text = "TO";
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(520, 328);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(520, 292);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// FormRpProcNotBilledIns
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(616, 366);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.date2);
			this.Controls.Add(this.date1);
			this.Controls.Add(this.labelTO);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpProcNotBilledIns";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Procedures Not Billed to Insurance";
			this.Load += new System.EventHandler(this.FormProcNotAttach_Load);
			this.ResumeLayout(false);

		}
		#endregion
		private void FormProcNotAttach_Load(object sender, System.EventArgs e) {
			date1.SelectionStart=DateTime.Today;
			date2.SelectionStart=DateTime.Today;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			Queries.CurReport=new ReportOld();
			//if(radioRange.Checked){
			Queries.CurReport.Query="SELECT CONCAT(patient.LName,', ',patient.FName,' ',patient.MiddleI),"
				+"procedurelog.ProcDate,procedurecode.Descript,procedurelog.ProcFee "
				+"FROM patient,procedurecode,procedurelog,claimproc "
				+"WHERE claimproc.procnum = procedurelog.procnum "
				+"AND patient.patnum = procedurelog.patnum "
				+"AND procedurelog.adacode = procedurecode.adacode "
				+"AND claimproc.NoBillIns=0 "
				+"AND procedurelog.ProcFee>0 "
				+"AND claimproc.Status=6 "//estimate
				+"AND patient.priplannum >0 "
				+"AND procedurelog.procstatus=2 "
				+"AND procedurelog.ProcDate >= '"+POut.PDate(date1.SelectionStart)+"' "
				+"AND procedurelog.ProcDate <= '"+POut.PDate(date2.SelectionStart)+"' "
				+"GROUP BY procedurelog.ProcNum";
			/*}
			else{
				Queries.CurReport.Query="SELECT CONCAT(patient.LName,', ',patient.FName,' ',patient.MiddleI),"
					+"procedurelog.ProcDate,procedurecode.Descript,procedurelog.ProcFee FROM patient,procedurecode,"
					+"procedurelog LEFT JOIN claimproc ON claimproc.procnum = procedurelog.procnum "
					+"WHERE claimproc.procnum IS NULL "
					+"&& patient.patnum=procedurelog.patnum && procedurelog.adacode=procedurecode.adacode "
					+"&& patient.priplannum > 0 "
					+"&& procedurelog.nobillins = 0 && procedurelog.procstatus = 2 "
					+"&& procedurelog.ProcDate = '" + date1.SelectionStart.ToString("yyyy-MM-dd")+"'";
			}*/
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			FormQuery2.SubmitReportQuery();
			Queries.CurReport.Title="Procedures Not Billed to Insurance";
			Queries.CurReport.SubTitle=new string[3];
			Queries.CurReport.SubTitle[0]=((Pref)Prefs.HList["PracticeTitle"]).ValueString;
				Queries.CurReport.SubTitle[1]=date1.SelectionStart.ToString("d")
					+" - "+date2.SelectionStart.ToString("d");
			Queries.CurReport.ColPos=new int[5];
			Queries.CurReport.ColCaption=new string[4];
			Queries.CurReport.ColAlign=new HorizontalAlignment[4];
			Queries.CurReport.ColPos[0]=20;
			Queries.CurReport.ColPos[1]=205;
			Queries.CurReport.ColPos[2]=390;
			Queries.CurReport.ColPos[3]=575;
			Queries.CurReport.ColPos[4]=760;
			Queries.CurReport.ColCaption[0]="Patient Name";
			Queries.CurReport.ColCaption[1]="Procedure Date";
			Queries.CurReport.ColCaption[2]="Procedure Description";
			Queries.CurReport.ColCaption[3]="Procedure Amount";
			Queries.CurReport.ColAlign[3]=HorizontalAlignment.Right;
			Queries.CurReport.Summary=new string[3];
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;
		}
		
	}
}
