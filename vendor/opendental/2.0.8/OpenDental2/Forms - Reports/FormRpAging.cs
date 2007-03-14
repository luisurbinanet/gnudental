using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	public class FormRpAging : System.Windows.Forms.Form{
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.GroupBox groupBox1;
		private FormQuery FormQuery2;
		private System.Windows.Forms.Label label1;
		private OpenDental.ValidDate textDate;
		private System.Windows.Forms.ListBox listBillType;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton radio30;
		private System.Windows.Forms.RadioButton radio90;
		private System.Windows.Forms.RadioButton radio60;
		private System.Windows.Forms.Button butAll;
		private System.Windows.Forms.RadioButton radioAny;

		public FormRpAging(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				label1,
				label2,
				radio30,
				radio60,
				radio90,
				radioAny,
				groupBox1,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
			}); 
		}

		protected override void Dispose(bool disposing){
			if(disposing){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radio30 = new System.Windows.Forms.RadioButton();
			this.radio90 = new System.Windows.Forms.RadioButton();
			this.radio60 = new System.Windows.Forms.RadioButton();
			this.radioAny = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.textDate = new OpenDental.ValidDate();
			this.listBillType = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.butAll = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(350, 326);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "Cancel";
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(350, 292);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 3;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radio30);
			this.groupBox1.Controls.Add(this.radio90);
			this.groupBox1.Controls.Add(this.radio60);
			this.groupBox1.Controls.Add(this.radioAny);
			this.groupBox1.Location = new System.Drawing.Point(57, 109);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(144, 120);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Age of Account";
			// 
			// radio30
			// 
			this.radio30.Location = new System.Drawing.Point(12, 44);
			this.radio30.Name = "radio30";
			this.radio30.Size = new System.Drawing.Size(104, 16);
			this.radio30.TabIndex = 1;
			this.radio30.Text = "Over 30 Days";
			// 
			// radio90
			// 
			this.radio90.Location = new System.Drawing.Point(12, 90);
			this.radio90.Name = "radio90";
			this.radio90.Size = new System.Drawing.Size(104, 18);
			this.radio90.TabIndex = 3;
			this.radio90.Text = "Over 90 Days";
			// 
			// radio60
			// 
			this.radio60.Location = new System.Drawing.Point(12, 66);
			this.radio60.Name = "radio60";
			this.radio60.Size = new System.Drawing.Size(104, 18);
			this.radio60.TabIndex = 2;
			this.radio60.Text = "Over 60 Days";
			// 
			// radioAny
			// 
			this.radioAny.Checked = true;
			this.radioAny.Location = new System.Drawing.Point(12, 20);
			this.radioAny.Name = "radioAny";
			this.radioAny.Size = new System.Drawing.Size(104, 18);
			this.radioAny.TabIndex = 0;
			this.radioAny.TabStop = true;
			this.radioAny.Text = "Any Balance";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(11, 52);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(114, 14);
			this.label1.TabIndex = 11;
			this.label1.Text = "Current through";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(124, 48);
			this.textDate.Name = "textDate";
			this.textDate.ReadOnly = true;
			this.textDate.Size = new System.Drawing.Size(78, 20);
			this.textDate.TabIndex = 0;
			this.textDate.Text = "";
			// 
			// listBillType
			// 
			this.listBillType.Location = new System.Drawing.Point(266, 45);
			this.listBillType.Name = "listBillType";
			this.listBillType.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listBillType.Size = new System.Drawing.Size(158, 186);
			this.listBillType.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(264, 25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 16);
			this.label2.TabIndex = 14;
			this.label2.Text = "Billing Types:";
			// 
			// butAll
			// 
			this.butAll.Location = new System.Drawing.Point(266, 237);
			this.butAll.Name = "butAll";
			this.butAll.TabIndex = 16;
			this.butAll.Text = "All";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// FormRpAging
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(448, 366);
			this.Controls.Add(this.butAll);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listBillType);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Name = "FormRpAging";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Aging Report";
			this.Load += new System.EventHandler(this.FormAging_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormAging_Load(object sender, System.EventArgs e) {
			textDate.Text=(PIn.PDate(((Pref)Prefs.HList["DateLastAging"]).ValueString)).ToShortDateString();
			if(PIn.PDate(((Pref)Prefs.HList["DateLastAging"]).ValueString) 
				< Ledgers.GetClosestFirst(DateTime.Today)){
				if(MessageBox.Show(Lan.g(this,"Update aging first?"),"",MessageBoxButtons.OKCancel)
					==DialogResult.OK){
					FormAging FormA=new FormAging();
					FormA.ShowDialog();
					if(FormA.DialogResult==DialogResult.OK){
						textDate.Text=Ledgers.GetClosestFirst(DateTime.Today).ToShortDateString();
					}
				}
			}
			for(int i=0;i<Defs.Short[(int)DefCat.BillingTypes].Length;i++){
				listBillType.Items.Add(Defs.Short[(int)DefCat.BillingTypes][i].ItemName);
			}
			listBillType.SelectedIndex=0;
		}

		private void butAll_Click(object sender, System.EventArgs e) {
			for(int i=0;i<listBillType.Items.Count;i++){
				listBillType.SetSelected(i,true);
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(listBillType.SelectedIndices.Count==0){
				MessageBox.Show(Lan.g(this,"At least one billing type must be selected."));
				return;
			}
			Queries.CurReport=new Report();
			Queries.CurReport.Query="SELECT CONCAT(LName,', ',FName,' ',MiddleI)"
				+",Bal_0_30,Bal_31_60,Bal_61_90,BalOver90"
				+",Bal_0_30+Bal_31_60+Bal_61_90+BalOver90 AS $total "
				+",InsEst"
				+",Bal_0_30+Bal_31_60+Bal_61_90+BalOver90-insEst AS $pat "
				+"FROM patient ";
			if(radioAny.Checked){
				Queries.CurReport.Query+=
					"WHERE (Bal_0_30 > '0' || Bal_31_60 > '0' || Bal_61_90 > '0' || BalOver90 > '0')";
			}
			else if(radio30.Checked){
				Queries.CurReport.Query+=
					"WHERE (Bal_31_60 > '0' || Bal_61_90 > '0' || BalOver90 > '0')";
			}
			else if(radio60.Checked){
				Queries.CurReport.Query+=
					"WHERE (Bal_61_90 > '0' || BalOver90 > '0')";
			}
			else if(radio90.Checked){
				Queries.CurReport.Query+=
					"WHERE (BalOver90 > '0')";
			}
			for(int i=0;i<listBillType.SelectedIndices.Count;i++){
				if(i==0){
					Queries.CurReport.Query+=" && (billingtype = '";
				}
				else{
					Queries.CurReport.Query+=" || billingtype = '";
				}
				Queries.CurReport.Query+=
					Defs.Short[(int)DefCat.BillingTypes][listBillType.SelectedIndices[i]].DefNum.ToString()+"'";
			}
			Queries.CurReport.Query+=") ORDER BY LName,FName";


			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			FormQuery2.SubmitReportQuery();		
			Queries.CurReport.Title="AGING REPORT";
			Queries.CurReport.SubTitle=new string[4];
			Queries.CurReport.SubTitle[0]=((Pref)Prefs.HList["PracticeTitle"]).ValueString;
			Queries.CurReport.SubTitle[1]="As of "+textDate.Text;
			if(radioAny.Checked){
				Queries.CurReport.SubTitle[2]="Any Balance";
			}
			if(radio30.Checked){
				Queries.CurReport.SubTitle[2]="Over 30 Days";
			}
			if(radio60.Checked){
				Queries.CurReport.SubTitle[2]="Over 60 Days";
			}
			if(radio90.Checked){
				Queries.CurReport.SubTitle[2]="Over 90 Days";
			}
			if(listBillType.SelectedIndices.Count==Defs.Short[(int)DefCat.BillingTypes].Length){
				Queries.CurReport.SubTitle[3]="All Billing Types";
			}
			else{
				Queries.CurReport.SubTitle[3]=Defs.Short[(int)DefCat.BillingTypes][listBillType.SelectedIndices[0]].ItemName;
				for(int i=1;i<listBillType.SelectedIndices.Count;i++){
					Queries.CurReport.SubTitle[3]+=", "+Defs.Short[(int)DefCat.BillingTypes][listBillType.SelectedIndices[i]].ItemName;
				}
			}
			Queries.CurReport.ColPos=new int[9];
			Queries.CurReport.ColCaption=new string[8];
			Queries.CurReport.ColAlign=new HorizontalAlignment[8];

			Queries.CurReport.ColPos[0]=20;
			Queries.CurReport.ColPos[1]=180;
			Queries.CurReport.ColPos[2]=250;
			Queries.CurReport.ColPos[3]=320;
			Queries.CurReport.ColPos[4]=390;
			Queries.CurReport.ColPos[5]=460;
			Queries.CurReport.ColPos[6]=530;
			Queries.CurReport.ColPos[7]=600;
			Queries.CurReport.ColPos[8]=670;

			Queries.CurReport.ColCaption[0]="GUARANTOR";
			Queries.CurReport.ColCaption[1]="0-30 DAYS";
			Queries.CurReport.ColCaption[2]="30-60 DAYS";
			Queries.CurReport.ColCaption[3]="60-90 DAYS";
			Queries.CurReport.ColCaption[4]="> 90 DAYS";
			Queries.CurReport.ColCaption[5]="TOTAL";
			Queries.CurReport.ColCaption[6]="-INS EST";
			Queries.CurReport.ColCaption[7]="=PATIENT";

			Queries.CurReport.ColAlign[1]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[2]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[3]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[4]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[5]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[6]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[7]=HorizontalAlignment.Right;

			Queries.CurReport.Summary=new string[0];
			FormQuery2.ShowDialog();		
			DialogResult=DialogResult.OK;
		}

		

		

	}

	public struct Aging{
		public double Charges;
		public double Payments;
		public double Aged;
	}



}
