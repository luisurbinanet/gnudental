using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
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
		//private bool ControlDown;
		///<summary></summary>
		public bool IsNew;
		private System.Windows.Forms.CheckBox checkShowUn;
		private OpenDental.XPButton butDelete;
		private double splitTot;

		///<summary></summary>
		public FormClaimPayEdit(){
			InitializeComponent();// Required for Windows Form Designer support
			tb2.CellClicked += new OpenDental.ContrTable.CellEventHandler(tb2_CellClicked);
			tb2.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tb2_CellDoubleClicked);
			Lan.C(this, new System.Windows.Forms.Control[] {
				this, //*Ann
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
			this.tb2 = new OpenDental.TableClaimPaySplits();
			this.checkShowUn = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.XPButton();
			this.SuspendLayout();
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(646, 41);
			this.textAmount.Name = "textAmount";
			this.textAmount.ReadOnly = true;
			this.textAmount.Size = new System.Drawing.Size(58, 20);
			this.textAmount.TabIndex = 44;
			this.textAmount.Text = "";
			this.textAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(646, 21);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(68, 20);
			this.textDate.TabIndex = 0;
			this.textDate.Text = "";
			this.textDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBankBranch
			// 
			this.textBankBranch.Location = new System.Drawing.Point(646, 81);
			this.textBankBranch.MaxLength = 25;
			this.textBankBranch.Name = "textBankBranch";
			this.textBankBranch.TabIndex = 2;
			this.textBankBranch.Text = "";
			// 
			// textCheckNum
			// 
			this.textCheckNum.Location = new System.Drawing.Point(646, 61);
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
			this.label6.Location = new System.Drawing.Point(551, 25);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(96, 16);
			this.label6.TabIndex = 37;
			this.label6.Text = "Payment Date";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(550, 45);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(95, 16);
			this.label5.TabIndex = 36;
			this.label5.Text = "Amount";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(553, 63);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(90, 16);
			this.label4.TabIndex = 35;
			this.label4.Text = "Check #";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(554, 83);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(91, 16);
			this.label3.TabIndex = 34;
			this.label3.Text = "Bank/Branch";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(560, 131);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(132, 16);
			this.label2.TabIndex = 33;
			this.label2.Text = "Note";
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(803, 631);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 7;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(803, 593);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 6;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// tb2
			// 
			this.tb2.BackColor = System.Drawing.SystemColors.Window;
			this.tb2.Location = new System.Drawing.Point(8, 20);
			this.tb2.Name = "tb2";
			this.tb2.ScrollValue = 1;
			this.tb2.SelectedIndices = new int[0];
			this.tb2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.tb2.Size = new System.Drawing.Size(539, 634);
			this.tb2.TabIndex = 49;
			// 
			// checkShowUn
			// 
			this.checkShowUn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowUn.Location = new System.Drawing.Point(564, 373);
			this.checkShowUn.Name = "checkShowUn";
			this.checkShowUn.Size = new System.Drawing.Size(215, 24);
			this.checkShowUn.TabIndex = 4;
			this.checkShowUn.Text = "Show Unattached";
			this.checkShowUn.Click += new System.EventHandler(this.checkShowUn_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(562, 632);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(129, 35);
			this.label1.TabIndex = 51;
			this.label1.Text = "(Deletes this Check, but not any splits)";
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(565, 597);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(92, 26);
			this.butDelete.TabIndex = 52;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormClaimPayEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(902, 676);
			this.Controls.Add(this.butDelete);
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
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClaimPayEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Insurance Claim Check";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormClaimPayEdit_Closing);
			this.Load += new System.EventHandler(this.FormClaimPayEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormClaimPayEdit_Load(object sender, System.EventArgs e) {
			if(IsNew){
				ClaimPayments.Cur=new ClaimPayment();
				ClaimPayments.InsertCur();//assigns a ClaimPaymentNum for use below
				checkShowUn.Checked=true;
			}
			if(ClaimPayments.Cur.CheckDate.Year < 1880){
				textDate.Text=DateTime.Today.ToShortDateString();
			}
			else
				textDate.Text=ClaimPayments.Cur.CheckDate.ToShortDateString();
			//textAmount is handled in FillTable
			textCheckNum.Text=ClaimPayments.Cur.CheckNum;
			textBankBranch.Text=ClaimPayments.Cur.BankBranch;
			textNote.Text=ClaimPayments.Cur.Note;
			FillTable();
			if(IsNew){
				tb2.SetSelected(true);
				splitTot=0;
				for(int i=0;i<tb2.SelectedIndices.Length;i++){
					splitTot+=Claims.ListQueue[tb2.SelectedIndices[i]].InsPayAmt;
				}
				textAmount.Text=splitTot.ToString("F");
			}
		}

		private void FillTable(){
			Claims.RefreshByCheck(ClaimPayments.Cur.ClaimPaymentNum,checkShowUn.Checked);
			tb2.ResetRows(Claims.ListQueue.Length);
			tb2.SetGridColor(Color.LightGray);
			splitTot=0;
			for (int i=0;i<Claims.ListQueue.Length;i++){
				tb2.Cell[0,i]=Claims.ListQueue[i].DateClaim.ToShortDateString();
				tb2.Cell[1,i]=Claims.ListQueue[i].ProvAbbr;
				tb2.Cell[2,i]=Claims.ListQueue[i].PatName;
				tb2.Cell[3,i]=Claims.ListQueue[i].Carrier;
				tb2.Cell[4,i]=Claims.ListQueue[i].FeeBilled.ToString("F");
				tb2.Cell[5,i]=Claims.ListQueue[i].InsPayAmt.ToString("F");
				if(Claims.ListQueue[i].ClaimPaymentNum==ClaimPayments.Cur.ClaimPaymentNum){
					tb2.SetSelected(i,true);
					splitTot+=Claims.ListQueue[i].InsPayAmt;
				}
				if(Claims.ListQueue[i].ClaimNum==Claims.Cur.ClaimNum){
					for(int j=0;j<tb2.MaxCols;j++){
						tb2.FontBold[j,i]=true;
					}
				}
			}
			tb2.LayoutTables();
			textAmount.Text=splitTot.ToString("F");
		}

		private void tb2_CellClicked(object sender, CellEventArgs e){
			splitTot=0;
			for (int i=0;i<tb2.SelectedIndices.Length;i++){
				splitTot+=Claims.ListQueue[tb2.SelectedIndices[i]].InsPayAmt;
			}
			textAmount.Text=splitTot.ToString("F");
		}

		private void tb2_CellDoubleClicked(object sender, CellEventArgs e){

		}

		private void checkShowUn_Click(object sender, System.EventArgs e) {
			FillTable();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"Delete this insurance check?"),"",MessageBoxButtons.OKCancel)
				!=DialogResult.OK){
				return;
			}
			if(IsNew){
				ClaimPayments.DeleteCur();
				DialogResult=DialogResult.Cancel;
			}
			else{
				ClaimProcs.DetachAllFromCheck(ClaimPayments.Cur.ClaimPaymentNum);
				DialogResult=DialogResult.OK;
			}
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDate.errorProvider1.GetError(textDate)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(tb2.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"At least one item must be selected, or use the delete button."));	
				return;
			}
			ClaimPayments.Cur.CheckAmt=PIn.PDouble(textAmount.Text);
			ClaimPayments.Cur.CheckDate=PIn.PDate(textDate.Text);
			ClaimPayments.Cur.CheckNum=textCheckNum.Text;
			ClaimPayments.Cur.BankBranch=textBankBranch.Text;
			ClaimPayments.Cur.Note=textNote.Text;
			ClaimPayments.UpdateCur();
			//this could be optimized to only save changes.
			//Would require a starting AL to compare to.
			ArrayList ALselected=new ArrayList();
			for(int i=0;i<tb2.SelectedIndices.Length;i++){
				ALselected.Add(tb2.SelectedIndices[i]);
			}
			for(int i=0;i<Claims.ListQueue.Length;i++){
				Claims.CurQueue=Claims.ListQueue[i];
				if(ALselected.Contains(i)){//row is selected
					ClaimProcs.SetForClaim(Claims.CurQueue.ClaimNum,ClaimPayments.Cur.ClaimPaymentNum,
						ClaimPayments.Cur.CheckDate,true);
				}
				else{//row not selected
					ClaimProcs.SetForClaim(Claims.CurQueue.ClaimNum,ClaimPayments.Cur.ClaimPaymentNum,
						ClaimPayments.Cur.CheckDate,false);
				}
			}
			DialogResult=DialogResult.OK;
		}

		private void FormClaimPayEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNew){
				//ClaimProcs never saved in the first place
				ClaimPayments.DeleteCur();
			}
		}



	}
}













