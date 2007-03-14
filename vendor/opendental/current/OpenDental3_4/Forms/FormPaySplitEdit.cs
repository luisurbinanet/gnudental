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
		private OpenDental.UI.Button ButCancel;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butRemainder;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ListBox listProvider;
		private System.Windows.Forms.ListBox listPatient;
		private System.Windows.Forms.ListBox listType;
		private System.Windows.Forms.Label labelAmount;
		private OpenDental.ValidDouble textAmount;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label labelType;// Required designer variable.
		///<summary></summary>
		public bool IsNew;
		private System.Windows.Forms.Label labelRemainder;
		///<summary></summary>
		public double Remain;
		private System.Windows.Forms.CheckBox checkPayPlan;
		private PaySplit PaySplitCur;
		private OpenDental.UI.Button butDelete;
		//private Patient PatCur;
		private Family FamCur;

		///<summary></summary>
		public FormPaySplitEdit(PaySplit paySplitCur,Family famCur){
			InitializeComponent();
			PaySplitCur=paySplitCur;
			//PatCur=patCur;//only used for new paysplits. Not the patNum of this split
			FamCur=famCur;
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

		private void InitializeComponent(){
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormPaySplitEdit));
			this.ButCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butRemainder = new OpenDental.UI.Button();
			this.labelRemainder = new System.Windows.Forms.Label();
			this.labelType = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.listProvider = new System.Windows.Forms.ListBox();
			this.listPatient = new System.Windows.Forms.ListBox();
			this.listType = new System.Windows.Forms.ListBox();
			this.textAmount = new OpenDental.ValidDouble();
			this.labelAmount = new System.Windows.Forms.Label();
			this.checkPayPlan = new System.Windows.Forms.CheckBox();
			this.butDelete = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// ButCancel
			// 
			this.ButCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.ButCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButCancel.Autosize = true;
			this.ButCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.ButCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.ButCancel.Location = new System.Drawing.Point(591, 269);
			this.ButCancel.Name = "ButCancel";
			this.ButCancel.Size = new System.Drawing.Size(75, 26);
			this.ButCancel.TabIndex = 6;
			this.ButCancel.Text = "&Cancel";
			this.ButCancel.Click += new System.EventHandler(this.ButCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(591, 239);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 5;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butRemainder
			// 
			this.butRemainder.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRemainder.Autosize = true;
			this.butRemainder.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRemainder.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRemainder.Location = new System.Drawing.Point(86, 87);
			this.butRemainder.Name = "butRemainder";
			this.butRemainder.Size = new System.Drawing.Size(92, 26);
			this.butRemainder.TabIndex = 7;
			this.butRemainder.Text = "&Remainder";
			this.butRemainder.Click += new System.EventHandler(this.butRemainder_Click);
			// 
			// labelRemainder
			// 
			this.labelRemainder.Location = new System.Drawing.Point(86, 119);
			this.labelRemainder.Name = "labelRemainder";
			this.labelRemainder.Size = new System.Drawing.Size(119, 88);
			this.labelRemainder.TabIndex = 5;
			this.labelRemainder.Text = "The Remainder button will calculate the value needed to make the splits balance.";
			// 
			// labelType
			// 
			this.labelType.Location = new System.Drawing.Point(533, 29);
			this.labelType.Name = "labelType";
			this.labelType.Size = new System.Drawing.Size(134, 16);
			this.labelType.TabIndex = 8;
			this.labelType.Text = "Discount Type";
			this.labelType.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(323, 31);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(116, 16);
			this.label4.TabIndex = 9;
			this.label4.Text = "Patient";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(213, 32);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(95, 16);
			this.label5.TabIndex = 10;
			this.label5.Text = "Provider";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listProvider
			// 
			this.listProvider.Location = new System.Drawing.Point(214, 49);
			this.listProvider.Name = "listProvider";
			this.listProvider.Size = new System.Drawing.Size(92, 108);
			this.listProvider.TabIndex = 2;
			// 
			// listPatient
			// 
			this.listPatient.Location = new System.Drawing.Point(325, 49);
			this.listPatient.Name = "listPatient";
			this.listPatient.Size = new System.Drawing.Size(192, 108);
			this.listPatient.TabIndex = 3;
			// 
			// listType
			// 
			this.listType.Location = new System.Drawing.Point(535, 49);
			this.listType.Name = "listType";
			this.listType.Size = new System.Drawing.Size(136, 108);
			this.listType.TabIndex = 4;
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(86, 49);
			this.textAmount.Name = "textAmount";
			this.textAmount.Size = new System.Drawing.Size(92, 20);
			this.textAmount.TabIndex = 1;
			this.textAmount.Text = "";
			// 
			// labelAmount
			// 
			this.labelAmount.Location = new System.Drawing.Point(6, 51);
			this.labelAmount.Name = "labelAmount";
			this.labelAmount.Size = new System.Drawing.Size(78, 16);
			this.labelAmount.TabIndex = 15;
			this.labelAmount.Text = "Amount";
			this.labelAmount.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkPayPlan
			// 
			this.checkPayPlan.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkPayPlan.Location = new System.Drawing.Point(300, 273);
			this.checkPayPlan.Name = "checkPayPlan";
			this.checkPayPlan.Size = new System.Drawing.Size(198, 18);
			this.checkPayPlan.TabIndex = 20;
			this.checkPayPlan.Text = "Attached to Payment Plan";
			this.checkPayPlan.Click += new System.EventHandler(this.checkPayPlan_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(47, 269);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(85, 26);
			this.butDelete.TabIndex = 21;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormPaySplitEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(694, 323);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.checkPayPlan);
			this.Controls.Add(this.labelAmount);
			this.Controls.Add(this.textAmount);
			this.Controls.Add(this.butRemainder);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.ButCancel);
			this.Controls.Add(this.listType);
			this.Controls.Add(this.listPatient);
			this.Controls.Add(this.listProvider);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.labelType);
			this.Controls.Add(this.labelRemainder);
			this.Location = new System.Drawing.Point(0, 400);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPaySplitEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Split";
			this.Load += new System.EventHandler(this.FormPaySplitEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormPaySplitEdit_Load(object sender, System.EventArgs e) {
			Location=new Point(Location.X,Location.Y+150);
			if(IsNew){

			}
			else{//editing existing paysplit or discount
				Text=Lan.g(this,"Edit Split");
			}
			textAmount.Text=PaySplitCur.SplitAmt.ToString("F");
			for(int i=0;i<Providers.List.Length;i++){
				listProvider.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==PaySplitCur.ProvNum)
					listProvider.SelectedIndex=i;
			}
			for(int i=0;i<Defs.Short[(int)DefCat.DiscountTypes].Length;i++){
				this.listType.Items.Add(Defs.Short[(int)DefCat.DiscountTypes][i].ItemName);
				if(Defs.Short[(int)DefCat.DiscountTypes][i].DefNum==PaySplitCur.DiscountType)
					listType.SelectedIndex=i;
			}
			for(int i=0;i<FamCur.List.Length;i++){
				listPatient.Items.Add(FamCur.GetNameInFamLFI(i));
				if(FamCur.List[i].PatNum==PaySplitCur.PatNum)
					listPatient.SelectedIndex=i;
			}
			if(PaySplitCur.PayPlanNum==0){
				checkPayPlan.Checked=false;
			}
			else{
				checkPayPlan.Checked=true;
			}
			if(PaySplitCur.IsDiscount){//discount
				butRemainder.Visible=false;
				labelRemainder.Visible=false;
				checkPayPlan.Visible=false;
			}
			else{//payment
				labelType.Visible=false;
				listType.Visible=false;
			}
		}

		private void checkPayPlan_Click(object sender, System.EventArgs e) {
			if(checkPayPlan.Checked){
				if(!PayPlans.GetValidPlan(FamCur.List[listPatient.SelectedIndex].PatNum)){//no valid plans
					if(PayPlans.List.Length==0){
						MessageBox.Show(Lan.g(this,
							"The selected patient is not the guarantor for any payment plans."));
					}
					checkPayPlan.Checked=false;
					return;
				}
				PaySplitCur.PayPlanNum=PayPlans.Cur.PayPlanNum;
			}
			else{//payPlan unchecked
				PaySplitCur.PayPlanNum=0;
			}
		}

		private void butRemainder_Click(object sender, System.EventArgs e) {
			textAmount.Text=Remain.ToString("F");
		}

		/*private void listType_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(!IsNew) return;
			PaySplitCur.SplitAmt=DiscountBasis.SplitAmt
				*PIn.PDouble(Defs.Short[(int)DefCat.DiscountTypes][listType.SelectedIndex].ItemValue)
				/100;
			textAmount.Text=PaySplitCur.SplitAmt.ToString("F");
		}*/

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(!MsgBox.Show(this,true,"Delete Item?")){
				return;
			}
			PaySplits.Cur=PaySplitCur;
			PaySplits.DeleteCur();
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textAmount.errorProvider1.GetError(textAmount)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(textAmount.Text==""){
				MessageBox.Show(Lan.g(this,"Please enter an amount."));	
				return;
			}
			PaySplitCur.SplitAmt=PIn.PDouble(textAmount.Text);
			if(listProvider.SelectedIndex!=-1)
				PaySplitCur.ProvNum=Providers.List[listProvider.SelectedIndex].ProvNum;
			if(listType.SelectedIndex!=-1)
				PaySplitCur.DiscountType=Defs.Short[15][listType.SelectedIndex].DefNum;
			if(listPatient.SelectedIndex!=-1)
				PaySplitCur.PatNum=FamCur.List[listPatient.SelectedIndex].PatNum;
			//PayPlanNum already handled
			PaySplits.Cur=PaySplitCur;
			if(IsNew){
				//if(IsNewDisc){//save new discount
				//	PaySplits.InsertCur();
				//}
				//else{//save new payment
				PaySplits.InsertCur();
				//}
			}
			else{//update discount or payment
				PaySplits.UpdateCur();
			}
			DialogResult=DialogResult.OK;
		}

		private void ButCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		


	}
}
