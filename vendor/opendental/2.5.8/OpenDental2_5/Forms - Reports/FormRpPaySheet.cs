using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormRpPaySheet : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RadioButton radioRange;
		private System.Windows.Forms.RadioButton radioSingle;
		private System.Windows.Forms.MonthCalendar date2;
		private System.Windows.Forms.MonthCalendar date1;
		private System.Windows.Forms.Label labelTO;
		private System.ComponentModel.Container components = null;
		private FormQuery FormQuery2;

		///<summary></summary>
		public FormRpPaySheet(){
			InitializeComponent();
 			Lan.C(this, new System.Windows.Forms.Control[] {
				panel1,
				radioRange,
				radioSingle,
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
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
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
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.radioRange = new System.Windows.Forms.RadioButton();
			this.radioSingle = new System.Windows.Forms.RadioButton();
			this.date2 = new System.Windows.Forms.MonthCalendar();
			this.date1 = new System.Windows.Forms.MonthCalendar();
			this.labelTO = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
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
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(523, 293);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.radioRange);
			this.panel1.Controls.Add(this.radioSingle);
			this.panel1.Location = new System.Drawing.Point(19, 16);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(104, 60);
			this.panel1.TabIndex = 0;
			// 
			// radioRange
			// 
			this.radioRange.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioRange.Location = new System.Drawing.Point(8, 32);
			this.radioRange.Name = "radioRange";
			this.radioRange.Size = new System.Drawing.Size(88, 24);
			this.radioRange.TabIndex = 1;
			this.radioRange.Text = "Date Range";
			// 
			// radioSingle
			// 
			this.radioSingle.Checked = true;
			this.radioSingle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioSingle.Location = new System.Drawing.Point(8, 8);
			this.radioSingle.Name = "radioSingle";
			this.radioSingle.Size = new System.Drawing.Size(88, 24);
			this.radioSingle.TabIndex = 0;
			this.radioSingle.TabStop = true;
			this.radioSingle.Text = "Single Date";
			this.radioSingle.CheckedChanged += new System.EventHandler(this.radioSingle_CheckedChanged);
			// 
			// date2
			// 
			this.date2.Location = new System.Drawing.Point(291, 112);
			this.date2.Name = "date2";
			this.date2.TabIndex = 2;
			this.date2.Visible = false;
			// 
			// date1
			// 
			this.date1.Location = new System.Drawing.Point(35, 112);
			this.date1.Name = "date1";
			this.date1.TabIndex = 1;
			// 
			// labelTO
			// 
			this.labelTO.Location = new System.Drawing.Point(251, 120);
			this.labelTO.Name = "labelTO";
			this.labelTO.Size = new System.Drawing.Size(24, 23);
			this.labelTO.TabIndex = 22;
			this.labelTO.Text = "TO";
			this.labelTO.Visible = false;
			// 
			// FormRpPaySheet
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(616, 366);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.date2);
			this.Controls.Add(this.date1);
			this.Controls.Add(this.labelTO);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpPaySheet";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Daily Payments Report";
			this.Load += new System.EventHandler(this.FormPaymentSheet_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormPaymentSheet_Load(object sender, System.EventArgs e) {
			date1.SelectionStart=DateTime.Today;
			date2.SelectionStart=DateTime.Today;			
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			Queries.CurReport=new Report();
			Queries.CurReport.Query="SELECT payment.PayDate,"
				+"CONCAT(patient.LName,', ',patient.FName,' ',patient.MiddleI)"
				+",'                                                 '"//this is long so union won't get trunc.
				+",payment.PayType,payment.CheckNum,payment.PayAmt "
				+"FROM payment,patient ";
			if(radioRange.Checked){
				Queries.CurReport.Query+="WHERE "
					+"payment.PatNum = patient.PatNum && "
					+"payment.PayDate >= '" + date1.SelectionStart.ToString("yyyy-MM-dd")+"' "
					+"&& paydate <= '" + date2.SelectionStart.ToString("yyyy-MM-dd")+"' "
					+"UNION "
					+"SELECT claimpayment.CheckDate,CONCAT(patient.LName,', ',patient.FName,' ',patient.MiddleI),"
					+"insplan.Carrier,'',claimpayment.CheckNum,claimpayment.CheckAmt "
					+"FROM claimpayment,claimproc,insplan,patient "
					+"WHERE claimproc.ClaimPaymentNum = claimpayment.ClaimPaymentNum "
					+"&& claimproc.PlanNum = insplan.PlanNum "
					+"&& claimproc.PatNum = patient.PatNum "
					+"&& (claimproc.Status = '1' || claimproc.Status = '4') "//received or supplemental
					+"&& claimpayment.CheckDate >= '"+date1.SelectionStart.ToString("yyyy-MM-dd")+"' "
					+"&& claimpayment.CheckDate <= '"+date2.SelectionStart.ToString("yyyy-MM-dd")+"' "
					+"GROUP BY claimpayment.ClaimPaymentNum "
					+"ORDER BY payment.PayDate";//this last clause is run after the union
			}
			else{//single date
				Queries.CurReport.Query+="WHERE " // to display carrier with patient name
					+"payment.PatNum = patient.PatNum && "
					+"payment.PayDate = '"+date1.SelectionStart.ToString("yyyy-MM-dd")+"' UNION "
					+"SELECT claimpayment.CheckDate,"
					+"CONCAT(patient.LName,', ',patient.FName,' ',patient.MiddleI),"
					+"insplan.Carrier,'',"
					+"claimpayment.CheckNum,claimpayment.CheckAmt "
					+"FROM claimpayment,claimproc,insplan,patient "
					+"WHERE claimproc.ClaimPaymentNum = claimpayment.ClaimPaymentNum "
					+"&& claimproc.PlanNum = insplan.PlanNum "
					+"&& claimproc.PatNum = patient.PatNum "
					+"&& (claimproc.Status = '1' || claimproc.Status = '4') "
					+"&& claimpayment.CheckDate = '"+date1.SelectionStart.ToString("yyyy-MM-dd")+"' "
					+"GROUP BY claimpayment.ClaimPaymentNum "
					+"ORDER BY payment.PayDate";
				//MessageBox.Show(Queries.CurReport.Query);
			}
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			FormQuery2.SubmitReportQuery();			
			Queries.CurReport.Title="DAILY PAYMENTS";
			Queries.CurReport.SubTitle=new string[2];
			Queries.CurReport.SubTitle[0]=((Pref)Prefs.HList["PracticeTitle"]).ValueString;
			if(radioRange.Checked==true){
				Queries.CurReport.SubTitle[1]=date1.SelectionStart.ToString("d")+" - "+date2.SelectionStart.ToString("d");
			}
			else{
				Queries.CurReport.SubTitle[1]=date1.SelectionStart.ToString("d");
			}			
			Queries.CurReport.ColPos=new int[7];
			Queries.CurReport.ColCaption=new string[6];
			Queries.CurReport.ColAlign=new HorizontalAlignment[6];
			Queries.CurReport.ColPos[0]=20;
			Queries.CurReport.ColPos[1]=120;
			Queries.CurReport.ColPos[2]=300;
			Queries.CurReport.ColPos[3]=450;
			Queries.CurReport.ColPos[4]=560;
			Queries.CurReport.ColPos[5]=650;
			Queries.CurReport.ColPos[6]=730;
			Queries.CurReport.ColCaption[0]="DATE";
			Queries.CurReport.ColCaption[1]="PATIENT NAME";
			Queries.CurReport.ColCaption[2]="CARRIER";
			Queries.CurReport.ColCaption[3]="PAYMENT TYPE";
			Queries.CurReport.ColCaption[4]="CHECK #";
			Queries.CurReport.ColCaption[5]="AMOUNT";
			Queries.CurReport.ColAlign[5]=HorizontalAlignment.Right;
			Queries.CurReport.Summary=new string[0];
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;		
		}

		private void radioSingle_CheckedChanged(object sender, System.EventArgs e) {
			if(radioSingle.Checked==true){
				date2.Visible=false;
				labelTO.Visible=false;
			}
			else{
				date2.Visible=true;
				labelTO.Visible=true;
			}			
		}

	}
}