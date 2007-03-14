using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental
{
	/// <summary>
	/// Summary description for FormPaySplitEdit.
	/// </summary>
	public class FormPaySplitEdit : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button ButCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butRemainder;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private OpenDental.ValidDate textDate;
		private System.Windows.Forms.ListBox listProvider;
		private System.Windows.Forms.ListBox listPatient;
		private System.Windows.Forms.ListBox listType;
		private System.Windows.Forms.Label labelAmount;
		private OpenDental.ValidDouble textAmount;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label labelType;// Required designer variable.
		public bool IsNew;
		public bool IsNewDisc;
		private System.Windows.Forms.Label labelAuto;
		public PaySplit DiscountBasis;
		private System.Windows.Forms.Label labelRemainder;
		public double Remain;
		private double OriginalAmt;
		private DateTime OriginalDate;

		public FormPaySplitEdit(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				labelAuto,
				labelRemainder,
				label2,
				label4,
				label5,
				labelAmount,
				butRemainder,
				this.ButCancel,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
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
			this.ButCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.butRemainder = new System.Windows.Forms.Button();
			this.labelRemainder = new System.Windows.Forms.Label();
			this.textDate = new OpenDental.ValidDate();
			this.label2 = new System.Windows.Forms.Label();
			this.labelType = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.listProvider = new System.Windows.Forms.ListBox();
			this.listPatient = new System.Windows.Forms.ListBox();
			this.listType = new System.Windows.Forms.ListBox();
			this.textAmount = new OpenDental.ValidDouble();
			this.labelAmount = new System.Windows.Forms.Label();
			this.labelAuto = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// ButCancel
			// 
			this.ButCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ButCancel.Location = new System.Drawing.Point(514, 332);
			this.ButCancel.Name = "ButCancel";
			this.ButCancel.TabIndex = 6;
			this.ButCancel.Text = "Cancel";
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(514, 302);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 5;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butRemainder
			// 
			this.butRemainder.Location = new System.Drawing.Point(232, 280);
			this.butRemainder.Name = "butRemainder";
			this.butRemainder.TabIndex = 7;
			this.butRemainder.Text = "Remainder";
			this.butRemainder.Click += new System.EventHandler(this.butRemainder_Click);
			// 
			// labelRemainder
			// 
			this.labelRemainder.Location = new System.Drawing.Point(232, 310);
			this.labelRemainder.Name = "labelRemainder";
			this.labelRemainder.Size = new System.Drawing.Size(176, 48);
			this.labelRemainder.TabIndex = 5;
			this.labelRemainder.Text = "The Remainder button will calculate the value needed to make the splits balance.";
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(86, 32);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(92, 20);
			this.textDate.TabIndex = 0;
			this.textDate.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(34, 34);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(50, 16);
			this.label2.TabIndex = 7;
			this.label2.Text = "Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelType
			// 
			this.labelType.Location = new System.Drawing.Point(436, 26);
			this.labelType.Name = "labelType";
			this.labelType.Size = new System.Drawing.Size(84, 16);
			this.labelType.TabIndex = 8;
			this.labelType.Text = "Discount Type";
			this.labelType.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(206, 30);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(50, 16);
			this.label4.TabIndex = 9;
			this.label4.Text = "Patient";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(34, 110);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(50, 16);
			this.label5.TabIndex = 10;
			this.label5.Text = "Provider";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listProvider
			// 
			this.listProvider.Location = new System.Drawing.Point(86, 110);
			this.listProvider.Name = "listProvider";
			this.listProvider.Size = new System.Drawing.Size(92, 95);
			this.listProvider.TabIndex = 2;
			// 
			// listPatient
			// 
			this.listPatient.Location = new System.Drawing.Point(218, 48);
			this.listPatient.Name = "listPatient";
			this.listPatient.Size = new System.Drawing.Size(192, 121);
			this.listPatient.TabIndex = 3;
			// 
			// listType
			// 
			this.listType.Location = new System.Drawing.Point(446, 46);
			this.listType.Name = "listType";
			this.listType.Size = new System.Drawing.Size(136, 160);
			this.listType.TabIndex = 4;
			this.listType.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listType_MouseDown);
			this.listType.SelectedIndexChanged += new System.EventHandler(this.listType_SelectedIndexChanged);
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(86, 58);
			this.textAmount.Name = "textAmount";
			this.textAmount.Size = new System.Drawing.Size(92, 20);
			this.textAmount.TabIndex = 1;
			this.textAmount.Text = "";
			// 
			// labelAmount
			// 
			this.labelAmount.Location = new System.Drawing.Point(6, 60);
			this.labelAmount.Name = "labelAmount";
			this.labelAmount.Size = new System.Drawing.Size(78, 16);
			this.labelAmount.TabIndex = 15;
			this.labelAmount.Text = "Amount";
			this.labelAmount.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelAuto
			// 
			this.labelAuto.Location = new System.Drawing.Point(448, 218);
			this.labelAuto.Name = "labelAuto";
			this.labelAuto.Size = new System.Drawing.Size(154, 48);
			this.labelAuto.TabIndex = 16;
			this.labelAuto.Text = "Clicking on a Discount Type above will automatically calculate the amount.";
			this.labelAuto.Visible = false;
			// 
			// FormPaySplitEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(638, 390);
			this.ControlBox = false;
			this.Controls.Add(this.labelAuto);
			this.Controls.Add(this.labelAmount);
			this.Controls.Add(this.textAmount);
			this.Controls.Add(this.listType);
			this.Controls.Add(this.listPatient);
			this.Controls.Add(this.listProvider);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.labelType);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.labelRemainder);
			this.Controls.Add(this.butRemainder);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.ButCancel);
			this.Location = new System.Drawing.Point(0, 400);
			this.Name = "FormPaySplitEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = Lan.g(this,"Edit Split");
			this.Load += new System.EventHandler(this.FormPaySplitEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormPaySplitEdit_Load(object sender, System.EventArgs e) {
			Location=new Point(Location.X,Location.Y+150);
			if(IsNew){
				PaySplits.Cur=new PaySplit();
				PaySplits.Cur.PayNum=Payments.Cur.PayNum;
				if(IsNewDisc){//new discount
					Text=Lan.g(this,"Discount");
					labelAuto.Visible=true;
					PaySplits.Cur.IsDiscount=true;
					PaySplits.Cur.ProcDate=DiscountBasis.ProcDate;
					PaySplits.Cur.PatNum=DiscountBasis.PatNum;
					PaySplits.Cur.ProvNum=DiscountBasis.ProvNum;
				}
				else{//new paysplit
					Text=Lan.g(this,"New Paysplit");
					PaySplits.Cur.IsDiscount=false;
					PaySplits.Cur.ProcDate=DateTime.Now;
					PaySplits.Cur.ProvNum=Providers.List[0].ProvNum;
					PaySplits.Cur.DiscountType=Defs.Short[(int)DefCat.DiscountTypes][0].DefNum;
					PaySplits.Cur.PatNum=Patients.Cur.PatNum;
				}
			}
			else{//editing existing paysplit or discount
				Text=Lan.g(this,"Edit Split");
			}
			OriginalDate=PaySplits.Cur.ProcDate;
			OriginalAmt=PaySplits.Cur.SplitAmt;
			textDate.Text=PaySplits.Cur.ProcDate.ToString("d");
			textAmount.Text=PaySplits.Cur.SplitAmt.ToString("F");
			for(int i=0;i<Providers.List.Length;i++){
				listProvider.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==PaySplits.Cur.ProvNum)
					listProvider.SelectedIndex=i;
			}
			for(int i=0;i<Defs.Short[(int)DefCat.DiscountTypes].Length;i++){
				this.listType.Items.Add(Defs.Short[(int)DefCat.DiscountTypes][i].ItemName);
				if(Defs.Short[(int)DefCat.DiscountTypes][i].DefNum==PaySplits.Cur.DiscountType)
					listType.SelectedIndex=i;
			}
			for(int i=0;i<Patients.FamilyList.Length;i++){
				listPatient.Items.Add(Patients.GetNameInFamLFI(i));
				if(Patients.FamilyList[i].PatNum==PaySplits.Cur.PatNum)
					listPatient.SelectedIndex=i;
			}
			if(PaySplits.Cur.IsDiscount){//discount
				butRemainder.Visible=false;
				labelRemainder.Visible=false;
			}
			else{//payment
				labelType.Visible=false;
				listType.Visible=false;
			}
		}
		
		private void butOK_Click(object sender, System.EventArgs e) {
			if(  textDate.errorProvider1.GetError(textDate)!=""
				|| textAmount.errorProvider1.GetError(textAmount)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(textAmount.Text==""){
				MessageBox.Show("Please enter an amount.");	
				return;
			}
			else
				PaySplits.Cur.SplitAmt=PIn.PDouble(textAmount.Text);
			PaySplits.Cur.ProcDate=PIn.PDate(textDate.Text);
			if(listProvider.SelectedIndex!=-1)
				PaySplits.Cur.ProvNum=Providers.List[listProvider.SelectedIndex].ProvNum;
			if(listType.SelectedIndex!=-1)
				PaySplits.Cur.DiscountType=Defs.Short[15][listType.SelectedIndex].DefNum;
			if(listPatient.SelectedIndex!=-1)
				PaySplits.Cur.PatNum=Patients.FamilyList[listPatient.SelectedIndex].PatNum;
			if(IsNew){
				PaySplits.PutBal(PaySplits.Cur.PatNum,PaySplits.Cur.ProcDate,PaySplits.Cur.SplitAmt);
				if(IsNewDisc){//save new discount
					PaySplits.InsertCur();
				}
				else{//save new payment
					PaySplits.InsertCur();
				}
			}
			else{//update discount or payment
				if(PaySplits.Cur.ProcDate.Year!=OriginalDate.Year || PaySplits.Cur.ProcDate.Month!=OriginalDate.Month){
					PaySplits.PutBal(PaySplits.Cur.PatNum,OriginalDate,-OriginalAmt);
					PaySplits.PutBal(PaySplits.Cur.PatNum,PaySplits.Cur.ProcDate,PaySplits.Cur.SplitAmt);
				}
				else{//date not changed
					PaySplits.PutBal(PaySplits.Cur.PatNum,PaySplits.Cur.ProcDate,PaySplits.Cur.SplitAmt-OriginalAmt);
				}
				PaySplits.UpdateCur();
			}
			DialogResult=DialogResult.OK;
			Close();
		}

		private void listType_SelectedIndexChanged(object sender, System.EventArgs e) {
			//hate this event
		}

		private void butRemainder_Click(object sender, System.EventArgs e) {
			textAmount.Text=Remain.ToString("F");
		}

		private void listType_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(!IsNew) return;
			PaySplits.Cur.SplitAmt=DiscountBasis.SplitAmt
				*PIn.PDouble(Defs.Short[(int)DefCat.DiscountTypes][listType.SelectedIndex].ItemValue)
				/100;
			textAmount.Text=PaySplits.Cur.SplitAmt.ToString("F");
		}	


	}
}
