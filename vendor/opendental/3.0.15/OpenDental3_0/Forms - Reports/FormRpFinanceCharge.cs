using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormRpFinanceCharge : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private OpenDental.ValidDate textDate;
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.Container components = null;
		private FormQuery FormQuery2;

		///<summary></summary>
		public FormRpFinanceCharge(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				label1,
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
			this.textDate = new OpenDental.ValidDate();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(336, 176);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 19;
			this.butCancel.Text = "&Cancel";
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(336, 142);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 18;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(154, 62);
			this.textDate.Name = "textDate";
			this.textDate.ReadOnly = true;
			this.textDate.Size = new System.Drawing.Size(116, 20);
			this.textDate.TabIndex = 16;
			this.textDate.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 66);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(136, 14);
			this.label1.TabIndex = 17;
			this.label1.Text = "Date of Charges";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormRpFinanceCharge
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(436, 230);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpFinanceCharge";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Finance Charge Report";
			this.Load += new System.EventHandler(this.FormRpFinanceCharge_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRpFinanceCharge_Load(object sender, System.EventArgs e) {
			textDate.Text=PIn.PDate(((Pref)Prefs.HList["FinanceChargeLastRun"]).ValueString).ToShortDateString();
			/*if(DateTime.Today.Day > 15){
				if(DateTime.Today.Month!=12){
					textDate.Text=(new DateTime(DateTime.Today.Year,DateTime.Today.Month+1,1)).ToShortDateString();		
				}
				else{ 
					textDate.Text=(new DateTime(DateTime.Today.Year+1,1,1)).ToShortDateString();	
				}
			}
			else{
				textDate.Text=(new DateTime(DateTime.Today.Year,DateTime.Today.Month,1)).ToShortDateString();
			}	*/	
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//if(textDate.errorProvider1.GetError(textDate)!=""){
			//	MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
			//	return;
			//}
			Queries.CurReport=new ReportOld();
			Queries.CurReport.Query=
				"SELECT CONCAT(patient.LName,', ',patient.FName,' ',patient.MiddleI),adjamt "
				+"FROM patient,adjustment "
				+"WHERE patient.patnum=adjustment.patnum "
				+"&& adjustment.adjdate = '"+((Pref)Prefs.HList["FinanceChargeLastRun"]).ValueString+"'"
				+"&& adjustment.adjtype = '"+((Pref)Prefs.HList["FinanceChargeAdjustmentType"]).ValueString+"'";
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			FormQuery2.SubmitReportQuery();		
			Queries.CurReport.Title="FINANCE CHARGE REPORT";
			Queries.CurReport.SubTitle=new string[4];
			Queries.CurReport.SubTitle[0]=((Pref)Prefs.HList["PracticeTitle"]).ValueString;
			Queries.CurReport.SubTitle[1]="Date of Charges: "
				+PIn.PDate(((Pref)Prefs.HList["FinanceChargeLastRun"]).ValueString).ToShortDateString();
			//Queries.CurReport.SubTitle[2]="Adjustment type: "+PIn.PDate(textDate.Text).ToShortDateString();

			Queries.CurReport.ColPos=new int[3];
			Queries.CurReport.ColCaption=new string[2];
			Queries.CurReport.ColAlign=new HorizontalAlignment[2];

			Queries.CurReport.ColPos[0]=20;
			Queries.CurReport.ColPos[1]=200;
			Queries.CurReport.ColPos[2]=300;

			Queries.CurReport.ColCaption[0]="Patient Name";
			Queries.CurReport.ColCaption[1]="Amount";

			Queries.CurReport.ColAlign[1]=HorizontalAlignment.Right;

			Queries.CurReport.Summary=new string[0];
			FormQuery2.ShowDialog();		
			DialogResult=DialogResult.OK;
		}

	}
}