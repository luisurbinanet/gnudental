using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	///<summary></summary>
	public class FormBillingOptions : System.Windows.Forms.Form{
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.GroupBox groupBox1;
		//private FormQuery FormQuery2;
		private System.Windows.Forms.ListBox listBillType;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton radio30;
		private System.Windows.Forms.RadioButton radio90;
		private System.Windows.Forms.RadioButton radio60;
		private System.Windows.Forms.Button butAll;
		private System.Windows.Forms.CheckBox checkBadAddress;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button butSaveDefault;
		private System.Windows.Forms.CheckBox checkExcludeNegative;
		private OpenDental.ValidDouble textExcludeLessThan;
		private System.Windows.Forms.CheckBox checkExcludeInactive;
		private System.Windows.Forms.RadioButton radioAny;

		///<summary></summary>
		public FormBillingOptions(){
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

		///<summary></summary>
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
			this.listBillType = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.butAll = new System.Windows.Forms.Button();
			this.checkBadAddress = new System.Windows.Forms.CheckBox();
			this.checkExcludeNegative = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butSaveDefault = new System.Windows.Forms.Button();
			this.textExcludeLessThan = new OpenDental.ValidDouble();
			this.checkExcludeInactive = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(472, 413);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(102, 23);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(472, 379);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(102, 23);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "Create &List";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radio30);
			this.groupBox1.Controls.Add(this.radio90);
			this.groupBox1.Controls.Add(this.radio60);
			this.groupBox1.Controls.Add(this.radioAny);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(53, 38);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(144, 120);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Age of Account";
			// 
			// radio30
			// 
			this.radio30.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radio30.Location = new System.Drawing.Point(12, 44);
			this.radio30.Name = "radio30";
			this.radio30.Size = new System.Drawing.Size(104, 16);
			this.radio30.TabIndex = 1;
			this.radio30.Text = "Over 30 Days";
			// 
			// radio90
			// 
			this.radio90.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radio90.Location = new System.Drawing.Point(12, 90);
			this.radio90.Name = "radio90";
			this.radio90.Size = new System.Drawing.Size(104, 18);
			this.radio90.TabIndex = 3;
			this.radio90.Text = "Over 90 Days";
			// 
			// radio60
			// 
			this.radio60.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radio60.Location = new System.Drawing.Point(12, 66);
			this.radio60.Name = "radio60";
			this.radio60.Size = new System.Drawing.Size(104, 18);
			this.radio60.TabIndex = 2;
			this.radio60.Text = "Over 60 Days";
			// 
			// radioAny
			// 
			this.radioAny.Checked = true;
			this.radioAny.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioAny.Location = new System.Drawing.Point(12, 20);
			this.radioAny.Name = "radioAny";
			this.radioAny.Size = new System.Drawing.Size(104, 18);
			this.radioAny.TabIndex = 0;
			this.radioAny.TabStop = true;
			this.radioAny.Text = "Any Balance";
			// 
			// listBillType
			// 
			this.listBillType.Location = new System.Drawing.Point(416, 44);
			this.listBillType.Name = "listBillType";
			this.listBillType.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listBillType.Size = new System.Drawing.Size(158, 186);
			this.listBillType.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(414, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 16);
			this.label2.TabIndex = 14;
			this.label2.Text = "Billing Types:";
			// 
			// butAll
			// 
			this.butAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butAll.Location = new System.Drawing.Point(417, 236);
			this.butAll.Name = "butAll";
			this.butAll.TabIndex = 15;
			this.butAll.Text = "&All";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// checkBadAddress
			// 
			this.checkBadAddress.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBadAddress.Location = new System.Drawing.Point(51, 190);
			this.checkBadAddress.Name = "checkBadAddress";
			this.checkBadAddress.Size = new System.Drawing.Size(251, 22);
			this.checkBadAddress.TabIndex = 16;
			this.checkBadAddress.Text = "Exclude bad addresses (no zipcode)";
			// 
			// checkExcludeNegative
			// 
			this.checkExcludeNegative.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkExcludeNegative.Location = new System.Drawing.Point(51, 242);
			this.checkExcludeNegative.Name = "checkExcludeNegative";
			this.checkExcludeNegative.Size = new System.Drawing.Size(251, 22);
			this.checkExcludeNegative.TabIndex = 17;
			this.checkExcludeNegative.Text = "Exclude negative balances (credits)";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(67, 284);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(275, 20);
			this.label1.TabIndex = 18;
			this.label1.Text = "Exclude if Balance is less than:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butSaveDefault
			// 
			this.butSaveDefault.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butSaveDefault.Location = new System.Drawing.Point(50, 406);
			this.butSaveDefault.Name = "butSaveDefault";
			this.butSaveDefault.Size = new System.Drawing.Size(119, 23);
			this.butSaveDefault.TabIndex = 20;
			this.butSaveDefault.Text = "&Save As Default";
			this.butSaveDefault.Click += new System.EventHandler(this.butSaveDefault_Click);
			// 
			// textExcludeLessThan
			// 
			this.textExcludeLessThan.Location = new System.Drawing.Point(68, 311);
			this.textExcludeLessThan.Name = "textExcludeLessThan";
			this.textExcludeLessThan.Size = new System.Drawing.Size(77, 20);
			this.textExcludeLessThan.TabIndex = 22;
			this.textExcludeLessThan.Text = "";
			// 
			// checkExcludeInactive
			// 
			this.checkExcludeInactive.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkExcludeInactive.Location = new System.Drawing.Point(51, 216);
			this.checkExcludeInactive.Name = "checkExcludeInactive";
			this.checkExcludeInactive.Size = new System.Drawing.Size(158, 22);
			this.checkExcludeInactive.TabIndex = 23;
			this.checkExcludeInactive.Text = "Exclude inactive patients";
			// 
			// FormBillingOptions
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(609, 458);
			this.Controls.Add(this.checkExcludeInactive);
			this.Controls.Add(this.textExcludeLessThan);
			this.Controls.Add(this.butSaveDefault);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkExcludeNegative);
			this.Controls.Add(this.checkBadAddress);
			this.Controls.Add(this.butAll);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listBillType);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormBillingOptions";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Billing Options";
			this.Load += new System.EventHandler(this.FormBillingOptions_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormBillingOptions_Load(object sender, System.EventArgs e) {
			if(PIn.PDate(((Pref)Prefs.HList["DateLastAging"]).ValueString) 
				< Ledgers.GetClosestFirst(DateTime.Today)){
				if(MessageBox.Show(Lan.g(this,"Update aging first?"),"",MessageBoxButtons.YesNo)
					==DialogResult.Yes){
					FormAging FormA=new FormAging();
					FormA.ShowDialog();
				}
			}
			for(int i=0;i<Defs.Short[(int)DefCat.BillingTypes].Length;i++){
				listBillType.Items.Add(Defs.Short[(int)DefCat.BillingTypes][i].ItemName);
			}
			string[] selectedBillTypes=((Pref)Prefs.HList["BillingSelectBillingTypes"]).ValueString.Split(',');
			for(int i=0;i<selectedBillTypes.Length;i++){
				try{
					if(Convert.ToInt32(selectedBillTypes[i])<listBillType.Items.Count){
						listBillType.SetSelected(Convert.ToInt32(selectedBillTypes[i]),true);
					}
				}
				catch{}
			}
			if(listBillType.SelectedIndices.Count==0)
				listBillType.SelectedIndex=0;
			switch(((Pref)Prefs.HList["BillingAgeOfAccount"]).ValueString){
				default:
					radioAny.Checked=true;
					break;
				case "30":
					radio30.Checked=true;
					break;
				case "60":
					radio60.Checked=true;
					break;
				case "90":
					radio90.Checked=true;
					break;
			}
			if(((Pref)Prefs.HList["BillingExcludeBadAddresses"]).ValueString=="1"){
				checkBadAddress.Checked=true;
			}
			if(((Pref)Prefs.HList["BillingExcludeInactive"]).ValueString=="1"){
				checkExcludeInactive.Checked=true;
			}
			if(((Pref)Prefs.HList["BillingExcludeNegative"]).ValueString=="1"){
				checkExcludeNegative.Checked=true;
			}
			//if(((Pref)Prefs.HList["BillingExcludeNegative"]).ValueString=="")
			//	textExcludeLessThan.Text="0";
			textExcludeLessThan.Text=((Pref)Prefs.HList["BillingExcludeLessThan"]).ValueString;
			//blank is allowed
//fix: need to add option to save the excludeInactive option as part of default.
		}

		private void butAll_Click(object sender, System.EventArgs e) {
			for(int i=0;i<listBillType.Items.Count;i++){
				listBillType.SetSelected(i,true);
			}
		}

		private void butSaveDefault_Click(object sender, System.EventArgs e) {
			if( textExcludeLessThan.errorProvider1.GetError(textExcludeLessThan)!=""
				//|| textAmount.errorProvider1.GetError(textAmount)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			Prefs.Cur.PrefName="BillingSelectBillingTypes";
			Prefs.Cur.ValueString="";
			for(int i=0;i<listBillType.SelectedIndices.Count;i++){//will always be at least 1
				if(i>0)
					Prefs.Cur.ValueString+=",";
				Prefs.Cur.ValueString+=listBillType.SelectedIndices[i].ToString();
			}
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="BillingAgeOfAccount";
			if(radioAny.Checked){
				Prefs.Cur.ValueString="";//the default
			}
			else if(radio30.Checked){
				Prefs.Cur.ValueString="30";
			}
			else if(radio60.Checked){
				Prefs.Cur.ValueString="60";
			}
			else if(radio90.Checked){
				Prefs.Cur.ValueString="90";
			}
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="BillingExcludeBadAddresses";
			if(checkBadAddress.Checked) Prefs.Cur.ValueString="1";
			else Prefs.Cur.ValueString="0";
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="BillingExcludeInactive";
			if(checkExcludeInactive.Checked) Prefs.Cur.ValueString="1";
			else Prefs.Cur.ValueString="0";
			Prefs.UpdateCur();
	
			Prefs.Cur.PrefName="BillingExcludeNegative";
			if(checkExcludeNegative.Checked) Prefs.Cur.ValueString="1";
			else Prefs.Cur.ValueString="0";
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="BillingExcludeLessThan";
			Prefs.Cur.ValueString=textExcludeLessThan.Text;
			Prefs.UpdateCur();

			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if( textExcludeLessThan.errorProvider1.GetError(textExcludeLessThan)!=""
				//|| textAmount.errorProvider1.GetError(textAmount)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			string getAge="";
			if(radio30.Checked) getAge="30";
			else if(radio60.Checked) getAge="60";
			else if(radio90.Checked) getAge="90";
			int[] billingIndices=new int[listBillType.SelectedIndices.Count];
			for(int i=0;i<billingIndices.Length;i++){
				billingIndices[i]=listBillType.SelectedIndices[i];
			}
			Patients.GetAgingList(getAge,billingIndices,checkBadAddress.Checked
				,checkExcludeNegative.Checked,PIn.PDouble(textExcludeLessThan.Text)
				,checkExcludeInactive.Checked);
			FormBilling FormB=new FormBilling();
			FormB.ShowDialog();
			DialogResult=DialogResult.OK;			
		}

		

		

	}




}
