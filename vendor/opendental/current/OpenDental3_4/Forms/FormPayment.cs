/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormPayment : System.Windows.Forms.Form{
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textCheckNum;
		private System.Windows.Forms.TextBox textBankBranch;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textTotal;
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew=false;
		private OpenDental.ValidDate textDate;
		private OpenDental.ValidDouble textAmount;
		private Adjustments Adjustments=new Adjustments();
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ListBox listPayType;
		private double tot=0;
		private System.Windows.Forms.Label label9;
		private OpenDental.UI.Button butDeleteAll;
		private OpenDental.TablePaySplits tbSplits;
		private double[] startBal;
		private double[] newBal;
		private int patI;
		private System.Windows.Forms.Label label10;
		private int paymentCount;
		private OpenDental.UI.Button butAdd;
		private System.Windows.Forms.CheckBox checkPayPlan;
		private OpenDental.ODtextBox textNote;//(not including discounts)
		//private bool NoPermission=false;
		private Patient PatCur;
		private Family FamCur;

		///<summary></summary>
		public FormPayment(Patient patCur,Family famCur){
			InitializeComponent();// Required for Windows Form Designer support
			PatCur=patCur;
			FamCur=famCur;
			tbSplits.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbSplits_CellDoubleClicked);
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormPayment));
			this.butDeleteAll = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textCheckNum = new System.Windows.Forms.TextBox();
			this.textBankBranch = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textTotal = new System.Windows.Forms.TextBox();
			this.textDate = new OpenDental.ValidDate();
			this.textAmount = new OpenDental.ValidDouble();
			this.label8 = new System.Windows.Forms.Label();
			this.listPayType = new System.Windows.Forms.ListBox();
			this.tbSplits = new OpenDental.TablePaySplits();
			this.butAdd = new OpenDental.UI.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.checkPayPlan = new System.Windows.Forms.CheckBox();
			this.textNote = new OpenDental.ODtextBox();
			this.SuspendLayout();
			// 
			// butDeleteAll
			// 
			this.butDeleteAll.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDeleteAll.Autosize = true;
			this.butDeleteAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDeleteAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDeleteAll.Image = ((System.Drawing.Image)(resources.GetObject("butDeleteAll.Image")));
			this.butDeleteAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDeleteAll.Location = new System.Drawing.Point(42, 592);
			this.butDeleteAll.Name = "butDeleteAll";
			this.butDeleteAll.Size = new System.Drawing.Size(84, 26);
			this.butDeleteAll.TabIndex = 7;
			this.butDeleteAll.Text = "&Delete";
			this.butDeleteAll.Click += new System.EventHandler(this.butDeleteAll_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(638, 554);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 8;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(638, 590);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 9;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(242, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 16);
			this.label1.TabIndex = 7;
			this.label1.Text = "Payment Type";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 116);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(92, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "Note";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(4, 75);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Bank/Branch";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(4, 56);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 16);
			this.label4.TabIndex = 10;
			this.label4.Text = "Check #";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6, 37);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 16);
			this.label5.TabIndex = 11;
			this.label5.Text = "Amount";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(4, 18);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 16);
			this.label6.TabIndex = 12;
			this.label6.Text = "Payment Date";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCheckNum
			// 
			this.textCheckNum.Location = new System.Drawing.Point(106, 54);
			this.textCheckNum.Name = "textCheckNum";
			this.textCheckNum.TabIndex = 2;
			this.textCheckNum.Text = "";
			// 
			// textBankBranch
			// 
			this.textBankBranch.Location = new System.Drawing.Point(106, 74);
			this.textBankBranch.Name = "textBankBranch";
			this.textBankBranch.TabIndex = 3;
			this.textBankBranch.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(346, 484);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(200, 14);
			this.label7.TabIndex = 18;
			this.label7.Text = "(must match total amount of payment)";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textTotal
			// 
			this.textTotal.Location = new System.Drawing.Point(468, 458);
			this.textTotal.Name = "textTotal";
			this.textTotal.ReadOnly = true;
			this.textTotal.Size = new System.Drawing.Size(70, 20);
			this.textTotal.TabIndex = 19;
			this.textTotal.Text = "";
			this.textTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(106, 14);
			this.textDate.Name = "textDate";
			this.textDate.TabIndex = 0;
			this.textDate.Text = "";
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(106, 34);
			this.textAmount.Name = "textAmount";
			this.textAmount.TabIndex = 1;
			this.textAmount.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(366, 462);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100, 16);
			this.label8.TabIndex = 22;
			this.label8.Text = "Total Splits:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listPayType
			// 
			this.listPayType.Location = new System.Drawing.Point(346, 14);
			this.listPayType.Name = "listPayType";
			this.listPayType.Size = new System.Drawing.Size(120, 95);
			this.listPayType.TabIndex = 4;
			// 
			// tbSplits
			// 
			this.tbSplits.BackColor = System.Drawing.SystemColors.Window;
			this.tbSplits.Location = new System.Drawing.Point(106, 206);
			this.tbSplits.Name = "tbSplits";
			this.tbSplits.ScrollValue = 1;
			this.tbSplits.SelectedIndices = new int[0];
			this.tbSplits.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbSplits.Size = new System.Drawing.Size(449, 248);
			this.tbSplits.TabIndex = 26;
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(106, 470);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(92, 26);
			this.butAdd.TabIndex = 30;
			this.butAdd.Text = "&Add Split";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(42, 626);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(126, 48);
			this.label9.TabIndex = 28;
			this.label9.Text = "Deletes entire payment and all splits";
			// 
			// label10
			// 
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label10.Location = new System.Drawing.Point(598, 624);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(116, 46);
			this.label10.TabIndex = 29;
			this.label10.Text = "Cancel does not undo changes to Splits";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkPayPlan
			// 
			this.checkPayPlan.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkPayPlan.Location = new System.Drawing.Point(322, 610);
			this.checkPayPlan.Name = "checkPayPlan";
			this.checkPayPlan.Size = new System.Drawing.Size(216, 18);
			this.checkPayPlan.TabIndex = 30;
			this.checkPayPlan.Text = "Attached to Payment Plan";
			this.checkPayPlan.Click += new System.EventHandler(this.checkPayPlan_Click);
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(106, 116);
			this.textNote.MaxLength = 255;
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDental.QuickPasteType.Payment;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(360, 74);
			this.textNote.TabIndex = 31;
			this.textNote.Text = "";
			// 
			// FormPayment
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(750, 682);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.textAmount);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.textTotal);
			this.Controls.Add(this.textBankBranch);
			this.Controls.Add(this.textCheckNum);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butDeleteAll);
			this.Controls.Add(this.checkPayPlan);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.tbSplits);
			this.Controls.Add(this.listPayType);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butAdd);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPayment";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Payment";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormPayment_Closing);
			this.Load += new System.EventHandler(this.FormPayment_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormPayment_Load(object sender, System.EventArgs e) {
			if(IsNew){//the only way to get here is from Account window, so many lists will be refreshed
				Payments.Cur=new Payment();
				Payments.Cur.PayDate=DateTime.Now;
				Payments.InsertCur();
				if(PayPlans.GetValidPlan(PatCur.PatNum)){//a valid payPlan was located
					AddOneSplit();
					PaySplits.Cur.PayPlanNum=PayPlans.Cur.PayPlanNum;
					PaySplits.UpdateCur();
				}
			}
			else{
				if(!UserPermissions.CheckUserPassword("Payment Edit",Payments.Cur.PayDate)){
					butOK.Enabled=false;
					butDeleteAll.Enabled=false;
					butAdd.Enabled=false;
					tbSplits.Enabled=false;
					//butDeleteSplit.Enabled=false;
					//NoPermission=true;
				}				
			}
			textDate.Text=Payments.Cur.PayDate.ToString("d");
			textAmount.Text=Payments.Cur.PayAmt.ToString("F");
			textCheckNum.Text=Payments.Cur.CheckNum;
			textBankBranch.Text=Payments.Cur.BankBranch;
			for(int i=0;i<Defs.Short[(int)DefCat.PaymentTypes].Length;i++){
				listPayType.Items.Add(Defs.Short[(int)DefCat.PaymentTypes][i].ItemName);
				if(Defs.Short[(int)DefCat.PaymentTypes][i].DefNum==Payments.Cur.PayType)
					listPayType.SelectedIndex=i;
			}
			if(listPayType.SelectedIndex==-1)
				listPayType.SelectedIndex=0;
			textNote.Text=Payments.Cur.PayNote;
			PaySplits.RefreshPaymentList(Payments.Cur.PayNum);
			startBal=new double[FamCur.List.Length];
			for(int i=0;i<FamCur.List.Length;i++){
				startBal[i]=FamCur.List[i].EstBalance;
			}
			for(int i=0;i<PaySplits.PaymentList.Length;i++){
				if(!IsNew){ 
					patI=FamCur.GetIndex(PaySplits.PaymentList[i].PatNum);
					if(patI!=-1)startBal[patI]+=PaySplits.PaymentList[i].SplitAmt;
				}
			}
			FillTable(false);
		}

		private void FillTable(bool refreshPayList){
			if(refreshPayList)
				PaySplits.RefreshPaymentList(Payments.Cur.PayNum);
			tbSplits.ResetRows(PaySplits.PaymentList.Length);
			tbSplits.SelectedRow=-1;
			tbSplits.SetGridColor(Color.LightGray);
			newBal=new double[FamCur.List.Length];
			tot=0;
			paymentCount=0;
			for(int i=0;i<FamCur.List.Length;i++){
				newBal[i]=startBal[i];
			}
			for(int i=0;i<PaySplits.PaymentList.Length;i++){
				tbSplits.Cell[0,i]=Providers.GetAbbr(PaySplits.PaymentList[i].ProvNum);
				tbSplits.Cell[1,i]=FamCur.GetNameInFamLF(PaySplits.PaymentList[i].PatNum);
				tbSplits.Cell[2,i]="";
				tbSplits.Cell[3,i]=PaySplits.PaymentList[i].SplitAmt.ToString("F");
				tot+=PaySplits.PaymentList[i].SplitAmt;
				paymentCount++;
				patI=FamCur.GetIndex(PaySplits.PaymentList[i].PatNum);
				/*if(patI==-1){//handles patient not found in family (maybe got moved to another family?)
					tbSplits.Cell[5,i]="";
				}
				else{
					newBal[patI]-=PaySplits.PaymentList[i].SplitAmt;
					tbSplits.Cell[5,i]=newBal[patI].ToString("F");
				}	*/				
			}
			tbSplits.LayoutTables();
			textTotal.Text=tot.ToString("F");
			if(PaySplits.PaymentList.Length==1){
				checkPayPlan.Enabled=true;
				if(PaySplits.PaymentList[0].PayPlanNum>0){
					checkPayPlan.Checked=true;
				}
				else{
					checkPayPlan.Checked=false;
				}
			}
			else{
				checkPayPlan.Checked=false;
				checkPayPlan.Enabled=false;
			}
		}

		private void tbSplits_CellDoubleClicked(object sender, CellEventArgs e){
			//if(NoPermission){
			//	return;
			//}
			//PaySplits.Cur=PaySplits.PaymentList[e.Row];
			FormPaySplitEdit FormPSE=new FormPaySplitEdit(PaySplits.PaymentList[e.Row],FamCur);
			FormPSE.Remain=Payments.Cur.PayAmt-PIn.PDouble(textTotal.Text)+PaySplits.Cur.SplitAmt;
			if(FormPSE.ShowDialog()==DialogResult.OK){
				FillTable(true);
			}
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			PaySplit PaySplitCur=new PaySplit();
			PaySplitCur.PayNum=Payments.Cur.PayNum;
			PaySplitCur.IsDiscount=false;
			PaySplitCur.ProvNum=Providers.List[0].ProvNum;
			PaySplitCur.DiscountType=Defs.Short[(int)DefCat.DiscountTypes][0].DefNum;
			PaySplitCur.PatNum=PatCur.PatNum;
			FormPaySplitEdit FormPSE=new FormPaySplitEdit(PaySplitCur,FamCur);
			FormPSE.IsNew=true;
			FormPSE.Remain=Payments.Cur.PayAmt-PIn.PDouble(textTotal.Text);
			if(FormPSE.ShowDialog()!=DialogResult.Cancel){
				FillTable(true);
			}
		}

		/*private void butDeleteSplit_Click(object sender, System.EventArgs e) {
			if(tbSplits.SelectedRow==-1){
				MessageBox.Show(Lan.g(this,"Please select item first"));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete Item?"),"Delete?",MessageBoxButtons.YesNo)
				!=DialogResult.Yes){
				return;
			}
			PaySplits.Cur=PaySplits.PaymentList[tbSplits.SelectedRow];
			PaySplits.DeleteCur();
			tbSplits.SelectedRow=-1;
			FillTable(true);
		}*/

		private void checkPayPlan_Click(object sender, System.EventArgs e) {
			//if there is more than one split, then this checkbox is not even available.
			if(PaySplits.PaymentList.Length==0){
				AddOneSplit();
				FillTable(true);
				checkPayPlan.Checked=true;
				//now there is exactly one.  The amount will be updated as the form closes.
			}
			PaySplits.Cur=PaySplits.PaymentList[0];
			if(checkPayPlan.Checked){
				if(!PayPlans.GetValidPlan(PaySplits.Cur.PatNum)){//no valid plans
					//if(PayPlans.List.Length==0){
					MsgBox.Show(this,"The selected patient is not the guarantor for any payment plans.");
					//}
					checkPayPlan.Checked=false;
					return;
				}
				PaySplits.Cur.PayPlanNum=PayPlans.Cur.PayPlanNum;
				PaySplits.UpdateCur();
			}
			else{//payPlan unchecked
				PaySplits.Cur.PayPlanNum=0;
				PaySplits.UpdateCur();
			}
			FillTable(true);
		}

		/// <summary>Adds one split to work with.  Called when butOK click, or checkPayPlan click, or upon load if auto attaching to payplan.</summary>
		private void AddOneSplit(){
			PaySplits.Cur=new PaySplit();
			PaySplits.Cur.PatNum=PatCur.PatNum;
			PaySplits.Cur.PayNum=Payments.Cur.PayNum;
			PaySplits.Cur.ProcDate=Payments.Cur.PayDate;
			PaySplits.Cur.ProvNum=PatCur.GetProvNum();
			PaySplits.Cur.SplitAmt=PIn.PDouble(textAmount.Text);
			PaySplits.InsertCur();//also gets the insertID
		}

		///<summary>This checks all the dates first and only updates them if needed. Typically if the user changed the date of the payment. So this keeps the splits in synch with the same date.</summary>
		private void SetDatesSame(){
			bool datesSame=true;
			for(int i=0;i<PaySplits.PaymentList.Length;i++){
				if(PaySplits.PaymentList[i].ProcDate!=Payments.Cur.PayDate){
					datesSame=false;
					break;
				}
			}
      if(datesSame)
				return;
			PaySplits.SetDateInPayment(Payments.Cur.PayNum,Payments.Cur.PayDate);
		}

		///<summary>Deletes the payment and all splits.</summary>
		private void DeleteAll(){
			for(int i=0;i<PaySplits.PaymentList.Length;i++){
				PaySplits.Cur=PaySplits.PaymentList[i];
				//putbal taken care of in deletesplit
				PaySplits.DeleteCur();
			}
			Payments.DeleteCur();
		}

		private void butDeleteAll_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"This will delete the entire payment and all splits."),"",MessageBoxButtons.OKCancel)==DialogResult.Cancel){
				return;
			}
			DeleteAll();
			DialogResult=DialogResult.OK;
		}

		private void FormPayment_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNew){ 
				DeleteAll();
			}
			else if(Payments.Cur.PayAmt!=tot){
				MessageBox.Show(Lan.g(this,"Splits have been altered.  Payment must match splits."));
				e.Cancel=true;
				return;
			}	
		}

		private void butOK_Click(object sender, System.EventArgs e){
			if(  textDate.errorProvider1.GetError(textDate)!=""
				|| textAmount.errorProvider1.GetError(textAmount)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(textAmount.Text==""){
				MessageBox.Show(Lan.g(this,"Please enter an amount."));	
				return;
			}
			if(PaySplits.PaymentList.Length==0){
				AddOneSplit();
				textTotal.Text=textAmount.Text;
			}
			if(PaySplits.PaymentList.Length==1
				&& PIn.PDouble(textAmount.Text) != PaySplits.PaymentList[0].SplitAmt){
				PaySplits.Cur=PaySplits.PaymentList[0];
				PaySplits.Cur.SplitAmt=PIn.PDouble(textAmount.Text);
				PaySplits.UpdateCur();
				textTotal.Text=textAmount.Text;
			}
			if(PIn.PDouble(textAmount.Text)!=PIn.PDouble(textTotal.Text)){
				MessageBox.Show(Lan.g(this,"Split totals must equal payment amount."));
				return;
			}
			Payments.Cur.PayAmt=PIn.PDouble(textAmount.Text);
			Payments.Cur.PayDate=PIn.PDate(textDate.Text);
			Payments.Cur.CheckNum=textCheckNum.Text;
			Payments.Cur.BankBranch=textBankBranch.Text;
			Payments.Cur.PayNote=textNote.Text;
			Payments.Cur.PayType=Defs.Short[10][listPayType.SelectedIndex].DefNum;
			Payments.Cur.PatNum=PatCur.PatNum;
			if(paymentCount>1)
				Payments.Cur.IsSplit=true;
			else
				Payments.Cur.IsSplit=false;
			Payments.UpdateCur();
			SetDatesSame();
			if(!IsNew){
			  SecurityLogs.MakeLogEntry("Payment Edit",Payments.cmd.CommandText);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {			
			DialogResult=DialogResult.Cancel;
		}

		
		
	}
}
