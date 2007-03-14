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
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		//private FormQuery FormQuery2;
		private System.Windows.Forms.ListBox listBillType;
		private System.Windows.Forms.Label label2;
		private OpenDental.UI.Button butAll;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butSaveDefault;
		private OpenDental.ValidDouble textExcludeLessThan;
		private System.Windows.Forms.CheckBox checkExcludeInactive;
		private System.Windows.Forms.GroupBox groupBox2;
		private OpenDental.UI.Button butAdd;
		private System.Windows.Forms.Label label3;
		private OpenDental.UI.ODGrid gridDun;
		private System.Windows.Forms.Label label4;
		private OpenDental.ODtextBox textNote;
		private System.Windows.Forms.CheckBox checkBadAddress;
		private System.Windows.Forms.CheckBox checkExcludeNegative;
		private System.Windows.Forms.RadioButton radio30;
		private System.Windows.Forms.RadioButton radio90;
		private System.Windows.Forms.RadioButton radio60;
		private System.Windows.Forms.RadioButton radioAny;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label5;
		private OpenDental.ValidDate textLastStatement2;
		private Dunning[] dunningList;

		///<summary></summary>
		public FormBillingOptions(){
			InitializeComponent();
			Lan.F(this);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormBillingOptions));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.listBillType = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.butAll = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.butSaveDefault = new OpenDental.UI.Button();
			this.textExcludeLessThan = new OpenDental.ValidDouble();
			this.checkExcludeInactive = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textLastStatement2 = new OpenDental.ValidDate();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radio30 = new System.Windows.Forms.RadioButton();
			this.radio90 = new System.Windows.Forms.RadioButton();
			this.radio60 = new System.Windows.Forms.RadioButton();
			this.radioAny = new System.Windows.Forms.RadioButton();
			this.checkExcludeNegative = new System.Windows.Forms.CheckBox();
			this.checkBadAddress = new System.Windows.Forms.CheckBox();
			this.gridDun = new OpenDental.UI.ODGrid();
			this.butAdd = new OpenDental.UI.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textNote = new OpenDental.ODtextBox();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(806, 631);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(79, 26);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(693, 631);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(92, 26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "Create &List";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// listBillType
			// 
			this.listBillType.Location = new System.Drawing.Point(21, 337);
			this.listBillType.Name = "listBillType";
			this.listBillType.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listBillType.Size = new System.Drawing.Size(158, 186);
			this.listBillType.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(20, 319);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(147, 16);
			this.label2.TabIndex = 14;
			this.label2.Text = "Billing Types:";
			// 
			// butAll
			// 
			this.butAll.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAll.Autosize = true;
			this.butAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAll.Location = new System.Drawing.Point(21, 527);
			this.butAll.Name = "butAll";
			this.butAll.Size = new System.Drawing.Size(68, 26);
			this.butAll.TabIndex = 15;
			this.butAll.Text = "&All";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(19, 261);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(232, 18);
			this.label1.TabIndex = 18;
			this.label1.Text = "Exclude if Balance is less than:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butSaveDefault
			// 
			this.butSaveDefault.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSaveDefault.Autosize = true;
			this.butSaveDefault.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSaveDefault.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSaveDefault.Location = new System.Drawing.Point(19, 574);
			this.butSaveDefault.Name = "butSaveDefault";
			this.butSaveDefault.Size = new System.Drawing.Size(108, 25);
			this.butSaveDefault.TabIndex = 20;
			this.butSaveDefault.Text = "&Save As Default";
			this.butSaveDefault.Click += new System.EventHandler(this.butSaveDefault_Click);
			// 
			// textExcludeLessThan
			// 
			this.textExcludeLessThan.Location = new System.Drawing.Point(21, 283);
			this.textExcludeLessThan.Name = "textExcludeLessThan";
			this.textExcludeLessThan.Size = new System.Drawing.Size(77, 20);
			this.textExcludeLessThan.TabIndex = 22;
			this.textExcludeLessThan.Text = "";
			// 
			// checkExcludeInactive
			// 
			this.checkExcludeInactive.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkExcludeInactive.Location = new System.Drawing.Point(22, 210);
			this.checkExcludeInactive.Name = "checkExcludeInactive";
			this.checkExcludeInactive.Size = new System.Drawing.Size(229, 22);
			this.checkExcludeInactive.TabIndex = 23;
			this.checkExcludeInactive.Text = "Exclude inactive patients";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textLastStatement2);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.groupBox1);
			this.groupBox2.Controls.Add(this.checkExcludeNegative);
			this.groupBox2.Controls.Add(this.checkBadAddress);
			this.groupBox2.Controls.Add(this.checkExcludeInactive);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.textExcludeLessThan);
			this.groupBox2.Controls.Add(this.butSaveDefault);
			this.groupBox2.Controls.Add(this.listBillType);
			this.groupBox2.Controls.Add(this.butAll);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(21, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(263, 609);
			this.groupBox2.TabIndex = 24;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Filter";
			// 
			// textLastStatement2
			// 
			this.textLastStatement2.Location = new System.Drawing.Point(22, 38);
			this.textLastStatement2.Name = "textLastStatement2";
			this.textLastStatement2.Size = new System.Drawing.Size(94, 20);
			this.textLastStatement2.TabIndex = 25;
			this.textLastStatement2.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(21, 18);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(232, 16);
			this.label5.TabIndex = 24;
			this.label5.Text = "Include anyone not billed since";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radio30);
			this.groupBox1.Controls.Add(this.radio90);
			this.groupBox1.Controls.Add(this.radio60);
			this.groupBox1.Controls.Add(this.radioAny);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(21, 77);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(144, 107);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Age of Account";
			// 
			// radio30
			// 
			this.radio30.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radio30.Location = new System.Drawing.Point(12, 41);
			this.radio30.Name = "radio30";
			this.radio30.Size = new System.Drawing.Size(120, 16);
			this.radio30.TabIndex = 1;
			this.radio30.Text = "Over 30 Days";
			// 
			// radio90
			// 
			this.radio90.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radio90.Location = new System.Drawing.Point(12, 83);
			this.radio90.Name = "radio90";
			this.radio90.Size = new System.Drawing.Size(120, 18);
			this.radio90.TabIndex = 3;
			this.radio90.Text = "Over 90 Days";
			// 
			// radio60
			// 
			this.radio60.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radio60.Location = new System.Drawing.Point(12, 61);
			this.radio60.Name = "radio60";
			this.radio60.Size = new System.Drawing.Size(117, 18);
			this.radio60.TabIndex = 2;
			this.radio60.Text = "Over 60 Days";
			// 
			// radioAny
			// 
			this.radioAny.Checked = true;
			this.radioAny.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioAny.Location = new System.Drawing.Point(12, 19);
			this.radioAny.Name = "radioAny";
			this.radioAny.Size = new System.Drawing.Size(117, 18);
			this.radioAny.TabIndex = 0;
			this.radioAny.TabStop = true;
			this.radioAny.Text = "Any Balance";
			// 
			// checkExcludeNegative
			// 
			this.checkExcludeNegative.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkExcludeNegative.Location = new System.Drawing.Point(22, 231);
			this.checkExcludeNegative.Name = "checkExcludeNegative";
			this.checkExcludeNegative.Size = new System.Drawing.Size(231, 22);
			this.checkExcludeNegative.TabIndex = 17;
			this.checkExcludeNegative.Text = "Exclude negative balances (credits)";
			// 
			// checkBadAddress
			// 
			this.checkBadAddress.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBadAddress.Location = new System.Drawing.Point(22, 189);
			this.checkBadAddress.Name = "checkBadAddress";
			this.checkBadAddress.Size = new System.Drawing.Size(231, 22);
			this.checkBadAddress.TabIndex = 16;
			this.checkBadAddress.Text = "Exclude bad addresses (no zipcode)";
			// 
			// gridDun
			// 
			this.gridDun.Columns.Add(new OpenDental.UI.ODGridColumn("Billing Type", 100, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridDun.Columns.Add(new OpenDental.UI.ODGridColumn("Aging", 70, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridDun.Columns.Add(new OpenDental.UI.ODGridColumn("Ins", 40, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridDun.Columns.Add(new OpenDental.UI.ODGridColumn("Message", 356, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridDun.HScrollVisible = false;
			this.gridDun.Location = new System.Drawing.Point(299, 31);
			this.gridDun.Name = "gridDun";
			this.gridDun.ScrollValue = 0;
			this.gridDun.Size = new System.Drawing.Size(585, 430);
			this.gridDun.TabIndex = 0;
			this.gridDun.Title = "Dunning Messages";
			this.gridDun.TranslationName = "TableBillingMessages";
			this.gridDun.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridDun_CellDoubleClick);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(298, 465);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(128, 26);
			this.butAdd.TabIndex = 5;
			this.butAdd.Text = "Add Dunning Msg";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(299, 10);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(581, 16);
			this.label3.TabIndex = 25;
			this.label3.Text = "Items higher in the list are more general.  Items lower in the list take preceden" +
				"ce .";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(298, 498);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(575, 16);
			this.label4.TabIndex = 26;
			this.label4.Text = "General Message (in addition to any dunning messages)";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(299, 518);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDental.QuickPasteType.Statement;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(584, 102);
			this.textNote.TabIndex = 28;
			this.textNote.Text = "";
			// 
			// FormBillingOptions
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(898, 666);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.gridDun);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormBillingOptions";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Billing Options";
			this.Load += new System.EventHandler(this.FormBillingOptions_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormBillingOptions_Load(object sender, System.EventArgs e) {
			if(PIn.PDate(Prefs.GetString("DateLastAging")) < DateTime.Today){
				if(MessageBox.Show(Lan.g(this,"Update aging first?"),"",MessageBoxButtons.YesNo)==DialogResult.Yes){
					FormAging FormA=new FormAging();
					FormA.ShowDialog();
				}
			}
			for(int i=0;i<Defs.Short[(int)DefCat.BillingTypes].Length;i++){
				listBillType.Items.Add(Defs.Short[(int)DefCat.BillingTypes][i].ItemName);
			}
			textLastStatement2.Text=DateTime.Today.AddMonths(-1).ToShortDateString();
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
			textExcludeLessThan.Text=((Pref)Prefs.HList["BillingExcludeLessThan"]).ValueString;
			//blank is allowed
			FillDunning();
		}

		private void butAll_Click(object sender, System.EventArgs e) {
			for(int i=0;i<listBillType.Items.Count;i++){
				listBillType.SetSelected(i,true);
			}
		}

		private void butSaveDefault_Click(object sender, System.EventArgs e) {
			if( textExcludeLessThan.errorProvider1.GetError(textExcludeLessThan)!=""
				|| textLastStatement2.errorProvider1.GetError(textLastStatement2)!=""
				){
				MsgBox.Show(this,"Please fix data entry errors first.");
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

			DataValid.SetInvalid(InvalidTypes.Prefs);
		}

		private void FillDunning(){
			dunningList=Dunnings.Refresh();
			gridDun.BeginUpdate();
			gridDun.Rows.Clear();
			OpenDental.UI.ODGridRow row;
			//string text;
			for(int i=0;i<dunningList.Length;i++){
				row=new OpenDental.UI.ODGridRow();
				if(dunningList[i].BillingType==0){
					row.Cells.Add(Lan.g(this,"all"));
				}
				else{
					row.Cells.Add(Defs.GetName(DefCat.BillingTypes,dunningList[i].BillingType));
				}
				if(dunningList[i].AgeAccount==0){
					row.Cells.Add(Lan.g(this,"any"));
				}
				else{
					row.Cells.Add(Lan.g(this,"Over ")+dunningList[i].AgeAccount.ToString());
				}
				if(dunningList[i].InsIsPending==YN.Unknown){
					row.Cells.Add(Lan.g(this,"any"));
				}
				else if(dunningList[i].InsIsPending==YN.Yes){
					row.Cells.Add(Lan.g(this,"Y"));
				}
				else if(dunningList[i].InsIsPending==YN.No){
					row.Cells.Add(Lan.g(this,"N"));
				}
				row.Cells.Add(dunningList[i].DunMessage);
				gridDun.Rows.Add(row);
			}
			gridDun.EndUpdate();
		}

		private void gridDun_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			FormDunningEdit formD=new FormDunningEdit(dunningList[e.Row]);
			formD.ShowDialog();
			FillDunning();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			Dunning dun=new Dunning();
			FormDunningEdit FormD=new FormDunningEdit(dun);
			FormD.IsNew=true;
			FormD.ShowDialog();
			if(FormD.DialogResult==DialogResult.Cancel){
				return;
			}
			FillDunning();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if( textExcludeLessThan.errorProvider1.GetError(textExcludeLessThan)!=""
				|| textLastStatement2.errorProvider1.GetError(textLastStatement2)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			DateTime lastStatement=PIn.PDate(textLastStatement2.Text);
			string getAge="";
			if(radio30.Checked) getAge="30";
			else if(radio60.Checked) getAge="60";
			else if(radio90.Checked) getAge="90";
			int[] billingIndices=new int[listBillType.SelectedIndices.Count];
			for(int i=0;i<billingIndices.Length;i++){
				billingIndices[i]=listBillType.SelectedIndices[i];
			}
			FormBilling FormB=new FormBilling();
			FormB.AgingList=Patients.GetAgingList(getAge,lastStatement,billingIndices,checkBadAddress.Checked
				,checkExcludeNegative.Checked,PIn.PDouble(textExcludeLessThan.Text)
				,checkExcludeInactive.Checked);
			FormB.GeneralNote=textNote.Text;
			FormB.ShowDialog();
			DialogResult=DialogResult.OK;			
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

	}




}
