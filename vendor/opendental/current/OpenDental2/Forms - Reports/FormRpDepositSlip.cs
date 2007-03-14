using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormRpDepositSlip : System.Windows.Forms.Form{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.RadioButton radioSingle;
		private System.Windows.Forms.RadioButton radioRange;
		private System.Windows.Forms.Label labelTO;
		private System.Windows.Forms.ListBox listPayType;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.MonthCalendar date1;
		private System.Windows.Forms.MonthCalendar date2;
		private System.Windows.Forms.CheckBox checkBoxIns;
		private System.Windows.Forms.Button butNone;
		private FormQuery FormQuery2;

		public FormRpDepositSlip(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				labelTO,
				checkBoxIns,
				butNone,
				radioSingle,
				radioRange,
				label2,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
			}); 
		}

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
			this.panel1 = new System.Windows.Forms.Panel();
			this.radioRange = new System.Windows.Forms.RadioButton();
			this.radioSingle = new System.Windows.Forms.RadioButton();
			this.labelTO = new System.Windows.Forms.Label();
			this.listPayType = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.date1 = new System.Windows.Forms.MonthCalendar();
			this.date2 = new System.Windows.Forms.MonthCalendar();
			this.checkBoxIns = new System.Windows.Forms.CheckBox();
			this.butNone = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.radioRange);
			this.panel1.Controls.Add(this.radioSingle);
			this.panel1.Location = new System.Drawing.Point(20, 16);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(94, 60);
			this.panel1.TabIndex = 0;
			// 
			// radioRange
			// 
			this.radioRange.Location = new System.Drawing.Point(8, 30);
			this.radioRange.Name = "radioRange";
			this.radioRange.TabIndex = 1;
			this.radioRange.Text = "Date Range";
			// 
			// radioSingle
			// 
			this.radioSingle.Checked = true;
			this.radioSingle.Location = new System.Drawing.Point(8, 8);
			this.radioSingle.Name = "radioSingle";
			this.radioSingle.TabIndex = 0;
			this.radioSingle.TabStop = true;
			this.radioSingle.Text = "Single Date";
			this.radioSingle.CheckedChanged += new System.EventHandler(this.radioSingle_CheckedChanged);
			// 
			// labelTO
			// 
			this.labelTO.Location = new System.Drawing.Point(242, 128);
			this.labelTO.Name = "labelTO";
			this.labelTO.Size = new System.Drawing.Size(26, 23);
			this.labelTO.TabIndex = 3;
			this.labelTO.Text = "TO";
			this.labelTO.Visible = false;
			// 
			// listPayType
			// 
			this.listPayType.Location = new System.Drawing.Point(534, 54);
			this.listPayType.Name = "listPayType";
			this.listPayType.Size = new System.Drawing.Size(120, 173);
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
			this.butOK.Location = new System.Drawing.Point(604, 312);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 6;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(604, 346);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 7;
			this.butCancel.Text = "Cancel";
			// 
			// date1
			// 
			this.date1.Location = new System.Drawing.Point(30, 118);
			this.date1.Name = "date1";
			this.date1.TabIndex = 1;
			// 
			// date2
			// 
			this.date2.Location = new System.Drawing.Point(276, 118);
			this.date2.Name = "date2";
			this.date2.TabIndex = 2;
			this.date2.Visible = false;
			// 
			// checkBoxIns
			// 
			this.checkBoxIns.Location = new System.Drawing.Point(532, 6);
			this.checkBoxIns.Name = "checkBoxIns";
			this.checkBoxIns.Size = new System.Drawing.Size(154, 24);
			this.checkBoxIns.TabIndex = 3;
			this.checkBoxIns.Text = "Include Insurance Checks";
			// 
			// butNone
			// 
			this.butNone.Location = new System.Drawing.Point(536, 234);
			this.butNone.Name = "butNone";
			this.butNone.Size = new System.Drawing.Size(54, 20);
			this.butNone.TabIndex = 5;
			this.butNone.Text = "none";
			this.butNone.Click += new System.EventHandler(this.butNone_Click);
			// 
			// FormRpDepositSlip
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(696, 384);
			this.Controls.Add(this.butNone);
			this.Controls.Add(this.checkBoxIns);
			this.Controls.Add(this.date2);
			this.Controls.Add(this.date1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.listPayType);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.labelTO);
			this.Controls.Add(this.panel1);
			this.Name = "FormRpDepositSlip";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Deposit Slip";
			this.Load += new System.EventHandler(this.FormDepositSlip_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormDepositSlip_Load(object sender, System.EventArgs e) {
			date1.SelectionStart=DateTime.Today;
			date2.SelectionStart=DateTime.Today;
			for(int i=0;i<Defs.Short[(int)DefCat.PaymentTypes].Length;i++){
				this.listPayType.Items.Add(Defs.Short[(int)DefCat.PaymentTypes][i].ItemName);
			}
			listPayType.SelectedIndex=0;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
/*
SELECT PayDate,CONCAT(patient.LName,',',patient.FName,' ',patient.MiddleI),CheckNum,BankBranch,PayAmt
FROM payment,patient
WHERE payment.PatNum=patient.PatNum
GROUP BY PayDate
UNION
SELECT CheckDate,insplan.Carrier,CheckNum,BankBranch,CheckAmt
FROM claim,claimpayment,insplan
WHERE claim.ClaimPaymentNum=claimpayment.ClaimPaymentNum && claim.PlanNum=insplan.PlanNum 
GROUP BY CheckDate
ORDER BY PayDate
*/
			Queries.CurReport=new Report();
			Queries.CurReport.Query="";
			if(checkBoxIns.Checked){
				Queries.CurReport.Query="SELECT PayDate,CONCAT(patient.LName,', ',patient.FName,' ',"
					+"patient.MiddleI),CheckNum,BankBranch,PayAmt FROM payment,patient ";
				if(radioRange.Checked){
					if(listPayType.SelectedIndex>-1){
					   Queries.CurReport.Query+="WHERE "
							+"payment.PatNum = patient.PatNum && PayType = '"
							+Defs.Short[(int)DefCat.PaymentTypes][listPayType.SelectedIndex].DefNum
							+"' && PayDate >= '"+date1.SelectionStart.ToString("yyyy-MM-dd")+"' "
						+"AND PayDate <= '"+date2.SelectionStart.ToString("yyyy-MM-dd")+"' UNION ";
          }
					else{ 
						Queries.CurReport.Query+="WHERE 1=0 UNION ";
					}
					Queries.CurReport.Query+="SELECT CheckDate,insplan.Carrier,"
						+"CheckNum,BankBranch,CheckAmt FROM claimpayment,claim,insplan WHERE claim.ClaimPaymentNum = "
						+"claimpayment.ClaimPaymentNum && claim.PlanNum = insplan.PlanNum "
						+"&& claimstatus != 'A' "
						+"&& CheckDate >= '"+date1.SelectionStart.ToString("yyyy-MM-dd")+"' "
						+"&& CheckDate <= '"+date2.SelectionStart.ToString("yyyy-MM-dd")+"' ";
					Queries.CurReport.Query+="GROUP BY claimpayment.claimpaymentnum ORDER BY PayDate";
				}
				else{//range not checked
          if(listPayType.SelectedIndex>-1){
						 Queries.CurReport.Query+="WHERE "
							+"payment.PatNum = patient.PatNum && PayType = '"
							+Defs.Short[(int)DefCat.PaymentTypes][listPayType.SelectedIndex].DefNum+"' "
							+"AND PayDate = '"+date1.SelectionStart.ToString("yyyy-MM-dd")+"' UNION ";
					}
					else{ 
						Queries.CurReport.Query+="WHERE 1=0 UNION ";
					}
					Queries.CurReport.Query+="SELECT CheckDate,insplan.Carrier,"
						+"CheckNum,BankBranch,CheckAmt FROM claim,claimpayment,insplan WHERE claim.ClaimPaymentNum="
						+"claimpayment.ClaimPaymentNum && claim.PlanNum = insplan.PlanNum "
						+"&& claimstatus != 'A' "
						+"&& CheckDate = '"+date1.SelectionStart.ToString("yyyy-MM-dd")+"' ";
					Queries.CurReport.Query+="GROUP BY claimpayment.claimpaymentnum ORDER BY PayDate ";
				}
				//MessageBox.Show(Queries.CurReport.Query);
      }
      else{//no insurance checks
				if(radioRange.Checked){
          if(listPayType.SelectedIndex>-1){
						Queries.CurReport.Query="SELECT PayDate,CONCAT(patient.LName,', ',patient.FName,' ',"
							+"patient.MiddleI),CheckNum,BankBranch,PayAmt FROM payment,patient WHERE "
							+"payment.PatNum = patient.PatNum && PayType = '"
							+Defs.Short[(int)DefCat.PaymentTypes][listPayType.SelectedIndex].DefNum+"' "
							+"AND PayDate >= '"+date1.SelectionStart.ToString("yyyy-MM-dd")+"' "
							+"AND PayDate <= '"+date2.SelectionStart.ToString("yyyy-MM-dd")+"'";
						//MessageBox.Show(Queries.CurReport.Query);
          }
					else{
						MessageBox.Show("Must Select either a Payment Type and/or Include insurance Checks.");
						return;
					}
				}
				else{
          if(listPayType.SelectedIndex>-1)  {
						Queries.CurReport.Query="SELECT PayDate,CONCAT(patient.LName,', ',patient.FName,' ',"
							+"patient.MiddleI),CheckNum,BankBranch,PayAmt FROM payment,patient WHERE "
							+"payment.PatNum=patient.PatNum && PayType='"
							+Defs.Short[(int)DefCat.PaymentTypes][listPayType.SelectedIndex].DefNum+"' "
							+"AND PayDate = '"+date1.SelectionStart.ToString("yyyy-MM-dd")+"'";
          }
					else{
						MessageBox.Show("No check type selected.");
						return;
					}
				} 			
			}
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			FormQuery2.SubmitReportQuery();
			Queries.CurReport.Title="DEPOSIT SLIP";
			Queries.CurReport.SubTitle=new string[3];
			Queries.CurReport.SubTitle[0]=((Pref)Prefs.HList["PracticeTitle"]).ValueString;
			if(radioRange.Checked){
				Queries.CurReport.SubTitle[1]=date1.SelectionStart.ToString("d")+" - "+date2.SelectionStart.ToString("d");
			}
			else{
				Queries.CurReport.SubTitle[1]=date1.SelectionStart.ToString("d");
			}

			if (listPayType.SelectedIndex>-1)  {
			  Queries.CurReport.SubTitle[2]="Payment Type: "
					+Defs.Short[(int)DefCat.PaymentTypes][listPayType.SelectedIndex].ItemName;
				if(checkBoxIns.Checked)
					Queries.CurReport.SubTitle[2]+="/Insurance Claim Checks";
			}
			else  {
				if(checkBoxIns.Checked)  {
				 Queries.CurReport.SubTitle[2]="Payment Type: Insurance Claim Checks";
			  }
			}
			Queries.CurReport.ColPos=new int[6];
			Queries.CurReport.ColCaption=new string[5];
			Queries.CurReport.ColAlign=new HorizontalAlignment[5];
			Queries.CurReport.ColPos[0]=20;
			Queries.CurReport.ColPos[1]=100;
			Queries.CurReport.ColPos[2]=290;
			Queries.CurReport.ColPos[3]=410;
			Queries.CurReport.ColPos[4]=520;
			Queries.CurReport.ColPos[5]=580;
			Queries.CurReport.ColCaption[0]="DATE";
			Queries.CurReport.ColCaption[1]="PAYER";
			Queries.CurReport.ColCaption[2]="CHECK NUMBER";
			Queries.CurReport.ColCaption[3]="BANK / BRANCH";
			Queries.CurReport.ColCaption[4]="AMOUNT";
			Queries.CurReport.ColAlign[4]=HorizontalAlignment.Right;
			Queries.CurReport.Summary=new string[3];
			Queries.CurReport.Summary[0]="For Deposit to Account of "+((Pref)Prefs.HList["PracticeTitle"]).ValueString;
			Queries.CurReport.Summary[2]="Account number: "+((Pref)Prefs.HList["PracticeBankNumber"]).ValueString;
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

		private void butNone_Click(object sender, System.EventArgs e) {
			listPayType.SelectedIndex=-1;
		}



	}
}
