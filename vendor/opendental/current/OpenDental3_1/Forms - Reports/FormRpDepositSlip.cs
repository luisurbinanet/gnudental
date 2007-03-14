using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormRpDepositSlip : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Label labelTO;
		private System.Windows.Forms.ListBox listPayType;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.MonthCalendar date1;
		private System.Windows.Forms.MonthCalendar date2;
		private System.Windows.Forms.CheckBox checkBoxIns;
		private System.Windows.Forms.Button butNone;
		private System.Windows.Forms.Button butAll;
		private FormQuery FormQuery2;

		///<summary></summary>
		public FormRpDepositSlip(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				labelTO,
				checkBoxIns,
				butNone,
				//radioSingle,
				//radioRange,
				label2,
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
			this.labelTO = new System.Windows.Forms.Label();
			this.listPayType = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.date1 = new System.Windows.Forms.MonthCalendar();
			this.date2 = new System.Windows.Forms.MonthCalendar();
			this.checkBoxIns = new System.Windows.Forms.CheckBox();
			this.butNone = new System.Windows.Forms.Button();
			this.butAll = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labelTO
			// 
			this.labelTO.Location = new System.Drawing.Point(235, 111);
			this.labelTO.Name = "labelTO";
			this.labelTO.Size = new System.Drawing.Size(26, 23);
			this.labelTO.TabIndex = 3;
			this.labelTO.Text = "TO";
			// 
			// listPayType
			// 
			this.listPayType.Location = new System.Drawing.Point(534, 54);
			this.listPayType.Name = "listPayType";
			this.listPayType.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listPayType.Size = new System.Drawing.Size(134, 173);
			this.listPayType.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(534, 38);
			this.label2.Name = "label2";
			this.label2.TabIndex = 5;
			this.label2.Text = "Payment Type";
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(593, 345);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 6;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(593, 379);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 7;
			this.butCancel.Text = "&Cancel";
			// 
			// date1
			// 
			this.date1.Location = new System.Drawing.Point(30, 101);
			this.date1.Name = "date1";
			this.date1.TabIndex = 1;
			// 
			// date2
			// 
			this.date2.Location = new System.Drawing.Point(276, 101);
			this.date2.Name = "date2";
			this.date2.TabIndex = 2;
			// 
			// checkBoxIns
			// 
			this.checkBoxIns.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBoxIns.Location = new System.Drawing.Point(532, 6);
			this.checkBoxIns.Name = "checkBoxIns";
			this.checkBoxIns.Size = new System.Drawing.Size(154, 24);
			this.checkBoxIns.TabIndex = 3;
			this.checkBoxIns.Text = "Include Insurance Checks";
			// 
			// butNone
			// 
			this.butNone.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butNone.Location = new System.Drawing.Point(602, 232);
			this.butNone.Name = "butNone";
			this.butNone.Size = new System.Drawing.Size(66, 26);
			this.butNone.TabIndex = 5;
			this.butNone.Text = "&None";
			this.butNone.Click += new System.EventHandler(this.butNone_Click);
			// 
			// butAll
			// 
			this.butAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butAll.Location = new System.Drawing.Point(534, 232);
			this.butAll.Name = "butAll";
			this.butAll.Size = new System.Drawing.Size(66, 26);
			this.butAll.TabIndex = 8;
			this.butAll.Text = "&All";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// FormRpDepositSlip
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(701, 422);
			this.Controls.Add(this.butAll);
			this.Controls.Add(this.butNone);
			this.Controls.Add(this.checkBoxIns);
			this.Controls.Add(this.date2);
			this.Controls.Add(this.date1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.listPayType);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.labelTO);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpDepositSlip";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Deposit Slip";
			this.Load += new System.EventHandler(this.FormDepositSlip_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormDepositSlip_Load(object sender, System.EventArgs e) {
			date1.SelectionStart=DateTime.Today;
			date2.SelectionStart=DateTime.Today;
			for(int i=0;i<Defs.Short[(int)DefCat.PaymentTypes].Length;i++){
				this.listPayType.Items.Add(Defs.Short[(int)DefCat.PaymentTypes][i].ItemName);
				listPayType.SetSelected(i,true);
			}
			checkBoxIns.Checked=true;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
/*
SELECT PayDate,CONCAT(patient.LName,',',patient.FName,' ',patient.MiddleI),CheckNum,BankBranch,PayAmt
FROM payment,patient
WHERE payment.PatNum=patient.PatNum
GROUP BY PayDate
UNION
SELECT CheckDate,insplan.Carrier,CheckNum,BankBranch,CheckAmt
FROM claimproc,claimpayment,insplan
WHERE claimproc.ClaimPaymentNum=claimpayment.ClaimPaymentNum && claimproc.PlanNum=insplan.PlanNum 
GROUP BY CheckDate
ORDER BY PayDate
*/
			if(listPayType.SelectedIndices.Count==0
				&& !checkBoxIns.Checked)
			{
				MessageBox.Show("Must either select a payment type and/or include insurance checks.");
				return;
			}
			Queries.CurReport=new ReportOld();
			string cmd="";
			//if(checkBoxIns.Checked){
			cmd="SELECT PayDate,CONCAT(patient.LName,', ',patient.FName,' ',"
				+"patient.MiddleI) AS plfname,'                          ',PayType,"
				+"PayNum,CheckNum,BankBranch,PayAmt "
				//+"CheckNum,BankBranch,PayAmt,PayNum "
				+"FROM payment,patient WHERE ";//added plfname,paynum spk 4/14/04
			if(listPayType.SelectedIndices.Count==0){ 
				cmd+="1=0 ";//none
			}
			else{
				cmd+="payment.PatNum = patient.PatNum AND (";
				for(int i=0;i<listPayType.SelectedIndices.Count;i++){
					if(i>0) cmd+=" OR "; 
					cmd+="PayType = '"
						+Defs.Short[(int)DefCat.PaymentTypes][listPayType.SelectedIndices[i]].DefNum+"'";
				}
				cmd+=
					") AND PayDate >= '"+POut.PDate(date1.SelectionStart)+"' "
					+"AND PayDate <= '"+POut.PDate(date2.SelectionStart)+"' ";
      }
			if(checkBoxIns.Checked){
				cmd+="UNION SELECT CheckDate,CONCAT(patient.LName,', ',patient.FName,' ',"
				+"patient.MiddleI) AS plfname,CarrierName,'Ins',"
					+"claimpayment.ClaimPaymentNum,"
					+"CheckNum,BankBranch,CheckAmt "//spk added claimpaymentnum
					//+"claimpayment.ClaimPaymentNum "
					+"FROM claimpayment,claimproc,insplan,carrier,patient "
					+"WHERE claimproc.ClaimPaymentNum = claimpayment.ClaimPaymentNum "
					+"AND claimproc.PlanNum = insplan.PlanNum "
					+"AND claimproc.PatNum=patient.PatNum "
					+"AND insplan.CarrierNum = carrier.CarrierNum "
					+"AND (claimproc.status = '1' OR claimproc.status = '4') "
					+"AND CheckDate >= '"+POut.PDate(date1.SelectionStart)+"' "
					+"AND CheckDate <= '"+POut.PDate(date2.SelectionStart)+"' ";//added plfname, spk 4/30/04
				cmd+="GROUP BY claimpayment.ClaimPaymentNum ";
				//MessageBox.Show(Queries.CurReport.Query);
      }
			cmd+="ORDER BY PayDate, plfname";
			Queries.CurReport.Query=cmd;
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			FormQuery2.SubmitReportQuery();
			Queries.CurReport.Title="Deposit Slip";
			Queries.CurReport.SubTitle=new string[3];
			Queries.CurReport.SubTitle[0]=((Pref)Prefs.HList["PracticeTitle"]).ValueString;
			Queries.CurReport.SubTitle[1]=date1.SelectionStart.ToString("d")+" - "
				+date2.SelectionStart.ToString("d");
			if(listPayType.SelectedIndices.Count>0)  {
			  Queries.CurReport.SubTitle[2]="Payment Type(s): ";
					for(int i=0;i<listPayType.SelectedIndices.Count;i++){
						if(i>0) Queries.CurReport.SubTitle[2]+=", ";
						Queries.CurReport.SubTitle[2]
							+=Defs.Short[(int)DefCat.PaymentTypes][listPayType.SelectedIndices[i]].ItemName;
					}
				if(checkBoxIns.Checked)
					Queries.CurReport.SubTitle[2]+=" Insurance Claim Checks";
			}
			else  {
				if(checkBoxIns.Checked)  {
				 Queries.CurReport.SubTitle[2]="Payment Type: Insurance Claim Checks";
			  }
			}
			Queries.CurReport.ColPos=new int[9];
			Queries.CurReport.ColCaption=new string[8];
			Queries.CurReport.ColAlign=new HorizontalAlignment[8];
			Queries.CurReport.ColPos[0]=20;
			Queries.CurReport.ColPos[1]=100;
			Queries.CurReport.ColPos[2]=210;
			Queries.CurReport.ColPos[3]=320;
			Queries.CurReport.ColPos[4]=420;
			Queries.CurReport.ColPos[5]=480;
			Queries.CurReport.ColPos[6]=590;
			Queries.CurReport.ColPos[7]=680;
			Queries.CurReport.ColPos[8]=760;
			Queries.CurReport.ColCaption[0]="Date";
			Queries.CurReport.ColCaption[1]="Patient";
			Queries.CurReport.ColCaption[2]="Carrier";
			Queries.CurReport.ColCaption[3]="Type";
			//this column can be eliminated when the new reporting framework is complete:
			Queries.CurReport.ColCaption[4]="Pay #";
			Queries.CurReport.ColCaption[5]="Check Number";
			Queries.CurReport.ColCaption[6]="Bank-Branch";
			Queries.CurReport.ColCaption[7]="Amount";
			//Queries.CurReport.ColAlign[4]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[7]=HorizontalAlignment.Right;
			Queries.CurReport.Summary=new string[3];
			Queries.CurReport.Summary[0]="For Deposit to Account of "+((Pref)Prefs.HList["PracticeTitle"]).ValueString;
			Queries.CurReport.Summary[2]="Account number: "+((Pref)Prefs.HList["PracticeBankNumber"]).ValueString;
			FormQuery2.ShowDialog();

			DialogResult=DialogResult.OK;
		}

		private void butAll_Click(object sender, System.EventArgs e) {
			for(int i=0;i<listPayType.Items.Count;i++){
				listPayType.SetSelected(i,true);
			}
			checkBoxIns.Checked=true;
		}

		private void butNone_Click(object sender, System.EventArgs e) {
			listPayType.ClearSelected();
			checkBoxIns.Checked=false;
		}

		



	}
}
