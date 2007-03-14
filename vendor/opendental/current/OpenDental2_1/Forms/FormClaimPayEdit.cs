using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormClaimPayEdit : System.Windows.Forms.Form{
		private OpenDental.ValidDouble textAmount;
		private OpenDental.ValidDate textDate;
		private System.Windows.Forms.TextBox textBankBranch;
		private System.Windows.Forms.TextBox textCheckNum;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Label label1;
		private OpenDental.TableClaimPaySplits tb2;
		private System.ComponentModel.Container components = null;
		private bool ControlDown;
		public bool IsNew;
		private System.Windows.Forms.CheckBox checkShowUn;
		private System.Windows.Forms.Button butDelete;
		private double splitTot;

		public FormClaimPayEdit(){
			InitializeComponent();// Required for Windows Form Designer support
			tb2.CellClicked += new OpenDental.ContrTable.CellEventHandler(tb2_CellClicked);
			tb2.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tb2_CellDoubleClicked);
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label1,
				this.label2,
				this.label3,
				this.checkShowUn,
				this.label4,
				this.label5,
				this.label6,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
				butDelete,
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

		private void InitializeComponent(){
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormClaimPayEdit));
			this.textAmount = new OpenDental.ValidDouble();
			this.textDate = new OpenDental.ValidDate();
			this.textBankBranch = new System.Windows.Forms.TextBox();
			this.textCheckNum = new System.Windows.Forms.TextBox();
			this.textNote = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.butDelete = new System.Windows.Forms.Button();
			this.tb2 = new OpenDental.TableClaimPaySplits();
			this.checkShowUn = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(628, 41);
			this.textAmount.Name = "textAmount";
			this.textAmount.ReadOnly = true;
			this.textAmount.Size = new System.Drawing.Size(58, 20);
			this.textAmount.TabIndex = 44;
			this.textAmount.Text = "";
			this.textAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(628, 21);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(68, 20);
			this.textDate.TabIndex = 0;
			this.textDate.Text = "";
			this.textDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBankBranch
			// 
			this.textBankBranch.Location = new System.Drawing.Point(628, 81);
			this.textBankBranch.MaxLength = 25;
			this.textBankBranch.Name = "textBankBranch";
			this.textBankBranch.TabIndex = 2;
			this.textBankBranch.Text = "";
			// 
			// textCheckNum
			// 
			this.textCheckNum.Location = new System.Drawing.Point(628, 61);
			this.textCheckNum.MaxLength = 25;
			this.textCheckNum.Name = "textCheckNum";
			this.textCheckNum.TabIndex = 1;
			this.textCheckNum.Text = "";
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(558, 151);
			this.textNote.MaxLength = 255;
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(324, 70);
			this.textNote.TabIndex = 3;
			this.textNote.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(553, 25);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(76, 16);
			this.label6.TabIndex = 37;
			this.label6.Text = "Payment Date";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(569, 45);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(58, 16);
			this.label5.TabIndex = 36;
			this.label5.Text = "Amount";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(569, 63);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 16);
			this.label4.TabIndex = 35;
			this.label4.Text = "Check #";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(551, 83);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(76, 16);
			this.label3.TabIndex = 34;
			this.label3.Text = "Bank/Branch";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(560, 131);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 16);
			this.label2.TabIndex = 33;
			this.label2.Text = "Note";
			// 
			// butCancel
			// 
			this.butCancel.Location = new System.Drawing.Point(803, 631);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 7;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(803, 593);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 6;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butDelete
			// 
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.butDelete.Location = new System.Drawing.Point(564, 617);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75, 26);
			this.butDelete.TabIndex = 5;
			this.butDelete.Text = "         Delete";
			this.butDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// tb2
			// 
			this.tb2.BackColor = System.Drawing.SystemColors.Window;
			this.tb2.Location = new System.Drawing.Point(8, 20);
			this.tb2.Name = "tb2";
			this.tb2.SelectionMode = SelectionMode.None;//OpenDental.SelectRowsMode.None;
			this.tb2.Size = new System.Drawing.Size(539, 634);
			this.tb2.TabIndex = 49;
			this.tb2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb2_KeyUp);
			this.tb2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb2_KeyDown);
			// 
			// checkShowUn
			// 
			this.checkShowUn.Location = new System.Drawing.Point(564, 373);
			this.checkShowUn.Name = "checkShowUn";
			this.checkShowUn.Size = new System.Drawing.Size(132, 24);
			this.checkShowUn.TabIndex = 4;
			this.checkShowUn.Text = "Show Unattached";
			this.checkShowUn.Click += new System.EventHandler(this.checkShowUn_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(562, 647);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(108, 14);
			this.label1.TabIndex = 51;
			this.label1.Text = "(Deletes this Check)";
			// 
			// FormClaimPayEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(902, 676);
			this.ControlBox = false;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkShowUn);
			this.Controls.Add(this.tb2);
			this.Controls.Add(this.textAmount);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.textBankBranch);
			this.Controls.Add(this.textCheckNum);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butDelete);
			this.Name = "FormClaimPayEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = Lan.g(this,"Edit Insurance Claim Check");
			this.Load += new System.EventHandler(this.FormClaimPayEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormClaimPayEdit_Load(object sender, System.EventArgs e) {
			if(IsNew){
				ClaimPayments.Cur=new ClaimPayment();
				checkShowUn.Checked=true;
			}
			if(DateTime.Compare(ClaimPayments.Cur.CheckDate,DateTime.Parse("1/1/1880"))<0){
				textDate.Text=DateTime.Now.ToString("d");
			}
			else
				textDate.Text=ClaimPayments.Cur.CheckDate.ToString("d");
			//textAmount.Text=ClaimPayments.Cur.CheckAmt.ToString("F");
			textCheckNum.Text=ClaimPayments.Cur.CheckNum;
			textBankBranch.Text=ClaimPayments.Cur.BankBranch;
			textNote.Text=ClaimPayments.Cur.Note;
			FillTable();
		}

		private void FillTable(){
			int claimNum=Claims.Cur.ClaimNum;
			Claims.RefreshByCheck(ClaimPayments.Cur.ClaimPaymentNum,checkShowUn.Checked);
			tb2.ResetRows(Claims.List.Length);
			tb2.SelectedRowsAL=new ArrayList();
			tb2.SetGridColor(Color.LightGray);
			splitTot=0;
			for (int i=0;i<Claims.List.Length;i++){
				tb2.Cell[0,i]=Claims.List[i].DateService.ToString("d");
				tb2.Cell[1,i]=Providers.GetAbbr(Claims.List[i].ProvTreat);
				Patients.GetLim(Claims.List[i].PatNum);
				tb2.Cell[2,i]=Patients.LimName;
				tb2.Cell[3,i]=InsPlans.GetDesc(Claims.List[i].PlanNum);
				tb2.Cell[4,i]=Claims.List[i].ClaimFee.ToString("F");
				tb2.Cell[5,i]=Claims.List[i].InsPayAmt.ToString("F");
				if(Claims.List[i].ClaimPaymentNum==ClaimPayments.Cur.ClaimPaymentNum){
					tb2.SelectedRowsAL.Add(i);
					splitTot+=Claims.List[i].InsPayAmt;
					tb2.ColorRow(i,Color.Silver);
				}
				if(Claims.List[i].ClaimNum==claimNum){
					for(int j=0;j<tb2.FontBold.GetLength(0);j++){
						tb2.FontBold[j,i]=true;
					}
				}
			}
			tb2.LayoutTables();
			//textTotal.Text=splitTot.ToString("F");
			textAmount.Text=splitTot.ToString("F");
		}

		private void tb2_CellClicked(object sender, CellEventArgs e){
			if (!ControlDown){
				if(tb2.SelectedRowsAL.Count==1 && (int)tb2.SelectedRowsAL[0]==e.Row){
					tb2.SelectedRowsAL.Clear();
					tb2.ColorRow(e.Row,Color.White);
				}
				else{
					for(int i=0;i<tb2.SelectedRowsAL.Count;i++){
						tb2.ColorRow((int)tb2.SelectedRowsAL[i],Color.White);
					}
					tb2.SelectedRowsAL.Clear();
					tb2.SelectedRowsAL.Add(e.Row);
					tb2.ColorRow(e.Row,Color.Silver);
				}
			}
			else{
				if(tb2.SelectedRowsAL.Contains(e.Row)){
					tb2.SelectedRowsAL.Remove(e.Row);
					tb2.ColorRow(e.Row,Color.White);
				}
				else{
					tb2.SelectedRowsAL.Add(e.Row);
					tb2.ColorRow(e.Row,Color.Silver);
				}
			}
			splitTot=0;
			for (int i=0;i<tb2.SelectedRowsAL.Count;i++){
				splitTot+=Claims.List[(int)tb2.SelectedRowsAL[i]].InsPayAmt;
			}
			//textTotal.Text=splitTot.ToString("F");
			textAmount.Text=splitTot.ToString("F");
		}

		private void tb2_CellDoubleClicked(object sender, CellEventArgs e){

		}

		private void tb2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (e.KeyCode==Keys.ControlKey){
				ControlDown=true;
			}
		}

		private void tb2_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (e.KeyCode==Keys.ControlKey){
				ControlDown=false;
			}
		}

		private void checkShowUn_Click(object sender, System.EventArgs e) {
			FillTable();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"Delete this insurance check?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			if(IsNew){
				;
			}
			else{
				Claims.DetachAllFromCheck(ClaimPayments.Cur.ClaimPaymentNum);
				ClaimPayments.DeleteCur();
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(  textDate.errorProvider1.GetError(textDate)!=""
				//|| textAmount.errorProvider1.GetError(textAmount)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			//if(PIn.PDouble(textAmount.Text)!=splitTot){
			//	MessageBox.Show("Split totals must equal payment amount.");
			//	return;
			//}
			if(tb2.SelectedRowsAL.Count==0){
				MessageBox.Show(Lan.g(this,"At least one item must be selected, or use the delete button."));	
				return;
			}
			//if(textAmount.Text==""){
			//	MessageBox.Show("Please enter an amount.");	
			//	return;
			//}
			//else
				ClaimPayments.Cur.CheckAmt=PIn.PDouble(textAmount.Text);
			ClaimPayments.Cur.CheckDate=PIn.PDate(textDate.Text);
			ClaimPayments.Cur.CheckNum=textCheckNum.Text;
			ClaimPayments.Cur.BankBranch=textBankBranch.Text;
			ClaimPayments.Cur.Note=textNote.Text;
			if(IsNew){
				ClaimPayments.InsertCur();
			}
			else{
				ClaimPayments.UpdateCur();
			}
			//this could be optimized to only save changes.
			//Would require a starting AL to compare to.
			for(int i=0;i<Claims.List.Length;i++){
				Claims.Cur=Claims.List[i];
				if(tb2.SelectedRowsAL.Contains(i)){
					Claims.Cur.ClaimPaymentNum=ClaimPayments.Cur.ClaimPaymentNum;
				}
				else{
					Claims.Cur.ClaimPaymentNum=0;
				}
				Claims.UpdateCur();
				if(Claims.Cur.ClaimStatus=="S"){
					SecurityLogs.MakeLogEntry("Claims Sent Edit",Claims.cmd.CommandText);
				}
			}
			DialogResult=DialogResult.OK;
		}

	}
}
