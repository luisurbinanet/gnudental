using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormRpAdjSheet : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.MonthCalendar date2;
		private System.Windows.Forms.MonthCalendar date1;
		private System.Windows.Forms.Label labelTO;
		private System.ComponentModel.Container components = null;
		private FormQuery FormQuery2;

		///<summary></summary>
		public FormRpAdjSheet(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				labelTO,
				date1,
				date2,
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
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
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
			this.date2.Location = new System.Drawing.Point(291, 112);
			this.date2.Name = "date2";
			this.date2.TabIndex = 2;
			// 
			// date1
			// 
			this.date1.Location = new System.Drawing.Point(35, 112);
			this.date1.Name = "date1";
			this.date1.TabIndex = 1;
			// 
			// labelTO
			// 
			this.labelTO.Location = new System.Drawing.Point(245, 120);
			this.labelTO.Name = "labelTO";
			this.labelTO.Size = new System.Drawing.Size(24, 23);
			this.labelTO.TabIndex = 28;
			this.labelTO.Text = "TO";
			// 
			// FormRpAdjSheet
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
			this.Name = "FormRpAdjSheet";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Daily Adjustment Report";
			this.Load += new System.EventHandler(this.FormDailyAdjustment_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormDailyAdjustment_Load(object sender, System.EventArgs e) {
			date1.SelectionStart=DateTime.Today;
			date2.SelectionStart=DateTime.Today;			
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			Queries.CurReport=new ReportOld();
			Queries.CurReport.Query="SELECT adjustment.AdjDate,"
				+"CONCAT(patient.LName,', ',patient.FName,' ',patient.MiddleI),"
				+"definition.ItemName,adjustment.AdjNote,adjustment.AdjAmt FROM "
				+"adjustment,patient,definition WHERE adjustment.AdjType=definition.DefNum "
			  +"AND patient.PatNum=adjustment.PatNum "
				+"AND adjustment.AdjDate >= '"+POut.PDate(date1.SelectionStart)+"' "
				+"AND adjustment.AdjDate <= '"+POut.PDate(date2.SelectionStart)+"'";
			Queries.CurReport.Query += " ORDER BY adjustment.AdjDate";
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			FormQuery2.SubmitReportQuery();			
			Queries.CurReport.Title="Daily Adjustments";
			Queries.CurReport.SubTitle=new string[2];
			Queries.CurReport.SubTitle[0]=((Pref)Prefs.HList["PracticeTitle"]).ValueString;
			Queries.CurReport.SubTitle[1]=date1.SelectionStart.ToString("d")+" - "+date2.SelectionStart.ToString("d");	
			Queries.CurReport.ColPos=new int[6];
			Queries.CurReport.ColCaption=new string[5];
			Queries.CurReport.ColAlign=new HorizontalAlignment[5];
			Queries.CurReport.ColPos[0]=20;
			Queries.CurReport.ColPos[1]=120;
			Queries.CurReport.ColPos[2]=270;
			Queries.CurReport.ColPos[3]=495;
			Queries.CurReport.ColPos[4]=645;
			Queries.CurReport.ColPos[5]=720;
			Queries.CurReport.ColCaption[0]="Date";
			Queries.CurReport.ColCaption[1]="Patient Name";			
			Queries.CurReport.ColCaption[2]="Adjustment Type";
			Queries.CurReport.ColCaption[3]="Adjustment Note";
			Queries.CurReport.ColCaption[4]="Amount";
			Queries.CurReport.ColAlign[4]=HorizontalAlignment.Right;
			Queries.CurReport.Summary=new string[0];
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;
		}
		

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}
