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
	public class FormBenefitEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label labelADACode;
		private System.Windows.Forms.Label labelAmount;
		private System.Windows.Forms.Label label2;
		private System.ComponentModel.Container components = null;// Required designer variable.
		///<summary></summary>
		public bool IsNew;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butDelete;
		private OpenDental.ValidDouble textAmount;
		private System.Windows.Forms.ListBox listCategory;
		private ArrayList PosIndex=new ArrayList();
		private ArrayList NegIndex=new ArrayList();
		///<summary></summary>
		public Benefit BenCur;
		private TextBox textADACode;
		private ListBox listType;
		private Label labelType;
		private Label labelPercent;
		private ValidNum textPercent;
		private ListBox listTimePeriod;
		private Label label4;
		private ValidNum textQuantity;
		private ListBox listQuantityQualifier;
		private Label label8;
		private GroupBox groupQuantity;
		private CheckBox checkPat;
		private int PlanNum;
		private int PatPlanNum;
		///<summary>This is only set upon load.</summary>
		private bool isAnnualMax;

		///<summary></summary>
		public FormBenefitEdit(int patPlanNum,int planNum){
			InitializeComponent();
			//BenCur=benCur.Copy();
			PatPlanNum=patPlanNum;
			PlanNum=planNum;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBenefitEdit));
			this.labelADACode = new System.Windows.Forms.Label();
			this.labelAmount = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.textAmount = new OpenDental.ValidDouble();
			this.listCategory = new System.Windows.Forms.ListBox();
			this.checkPat = new System.Windows.Forms.CheckBox();
			this.textADACode = new System.Windows.Forms.TextBox();
			this.listType = new System.Windows.Forms.ListBox();
			this.labelType = new System.Windows.Forms.Label();
			this.labelPercent = new System.Windows.Forms.Label();
			this.textPercent = new OpenDental.ValidNum();
			this.listTimePeriod = new System.Windows.Forms.ListBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textQuantity = new OpenDental.ValidNum();
			this.listQuantityQualifier = new System.Windows.Forms.ListBox();
			this.label8 = new System.Windows.Forms.Label();
			this.groupQuantity = new System.Windows.Forms.GroupBox();
			this.groupQuantity.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelADACode
			// 
			this.labelADACode.Location = new System.Drawing.Point(129,181);
			this.labelADACode.Name = "labelADACode";
			this.labelADACode.Size = new System.Drawing.Size(104,16);
			this.labelADACode.TabIndex = 0;
			this.labelADACode.Text = "or ADA Code";
			this.labelADACode.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelAmount
			// 
			this.labelAmount.Location = new System.Drawing.Point(375,67);
			this.labelAmount.Name = "labelAmount";
			this.labelAmount.Size = new System.Drawing.Size(100,16);
			this.labelAmount.TabIndex = 4;
			this.labelAmount.Text = "Amount";
			this.labelAmount.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(131,41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,16);
			this.label2.TabIndex = 10;
			this.label2.Text = "Category";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(587,313);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 8;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(587,351);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 9;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(12,351);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,26);
			this.butDelete.TabIndex = 17;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(476,64);
			this.textAmount.Name = "textAmount";
			this.textAmount.Size = new System.Drawing.Size(68,20);
			this.textAmount.TabIndex = 1;
			// 
			// listCategory
			// 
			this.listCategory.Location = new System.Drawing.Point(234,41);
			this.listCategory.Name = "listCategory";
			this.listCategory.Size = new System.Drawing.Size(100,134);
			this.listCategory.TabIndex = 5;
			// 
			// checkPat
			// 
			this.checkPat.CheckAlign = System.Drawing.ContentAlignment.TopRight;
			this.checkPat.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkPat.Location = new System.Drawing.Point(12,7);
			this.checkPat.Name = "checkPat";
			this.checkPat.Size = new System.Drawing.Size(237,31);
			this.checkPat.TabIndex = 4;
			this.checkPat.Text = "Patient Override (Rare. Usually if percentages are different for family members)";
			this.checkPat.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.checkPat.UseVisualStyleBackColor = true;
			// 
			// textADACode
			// 
			this.textADACode.Location = new System.Drawing.Point(234,177);
			this.textADACode.Name = "textADACode";
			this.textADACode.Size = new System.Drawing.Size(100,20);
			this.textADACode.TabIndex = 6;
			// 
			// listType
			// 
			this.listType.Location = new System.Drawing.Point(234,208);
			this.listType.Name = "listType";
			this.listType.Size = new System.Drawing.Size(100,82);
			this.listType.TabIndex = 7;
			this.listType.Click += new System.EventHandler(this.listType_Click);
			// 
			// labelType
			// 
			this.labelType.Location = new System.Drawing.Point(132,208);
			this.labelType.Name = "labelType";
			this.labelType.Size = new System.Drawing.Size(100,16);
			this.labelType.TabIndex = 26;
			this.labelType.Text = "Type";
			this.labelType.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelPercent
			// 
			this.labelPercent.Location = new System.Drawing.Point(371,45);
			this.labelPercent.Name = "labelPercent";
			this.labelPercent.Size = new System.Drawing.Size(104,16);
			this.labelPercent.TabIndex = 27;
			this.labelPercent.Text = "Percent";
			this.labelPercent.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPercent
			// 
			this.textPercent.Location = new System.Drawing.Point(476,41);
			this.textPercent.MaxVal = 100;
			this.textPercent.MinVal = 0;
			this.textPercent.Name = "textPercent";
			this.textPercent.Size = new System.Drawing.Size(68,20);
			this.textPercent.TabIndex = 0;
			// 
			// listTimePeriod
			// 
			this.listTimePeriod.Location = new System.Drawing.Point(476,87);
			this.listTimePeriod.Name = "listTimePeriod";
			this.listTimePeriod.Size = new System.Drawing.Size(100,69);
			this.listTimePeriod.TabIndex = 2;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(374,87);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100,16);
			this.label4.TabIndex = 30;
			this.label4.Text = "Time Period";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textQuantity
			// 
			this.textQuantity.Location = new System.Drawing.Point(67,17);
			this.textQuantity.MaxVal = 100;
			this.textQuantity.MinVal = 0;
			this.textQuantity.Name = "textQuantity";
			this.textQuantity.Size = new System.Drawing.Size(68,20);
			this.textQuantity.TabIndex = 0;
			// 
			// listQuantityQualifier
			// 
			this.listQuantityQualifier.Location = new System.Drawing.Point(67,41);
			this.listQuantityQualifier.Name = "listQuantityQualifier";
			this.listQuantityQualifier.Size = new System.Drawing.Size(100,69);
			this.listQuantityQualifier.TabIndex = 1;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(1,43);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(65,38);
			this.label8.TabIndex = 34;
			this.label8.Text = "Qualifier";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupQuantity
			// 
			this.groupQuantity.Controls.Add(this.textQuantity);
			this.groupQuantity.Controls.Add(this.listQuantityQualifier);
			this.groupQuantity.Controls.Add(this.label8);
			this.groupQuantity.Location = new System.Drawing.Point(409,167);
			this.groupQuantity.Name = "groupQuantity";
			this.groupQuantity.Size = new System.Drawing.Size(180,121);
			this.groupQuantity.TabIndex = 3;
			this.groupQuantity.TabStop = false;
			this.groupQuantity.Text = "Quantity";
			// 
			// FormBenefitEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(674,389);
			this.Controls.Add(this.groupQuantity);
			this.Controls.Add(this.listTimePeriod);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textPercent);
			this.Controls.Add(this.labelPercent);
			this.Controls.Add(this.listType);
			this.Controls.Add(this.labelType);
			this.Controls.Add(this.textADACode);
			this.Controls.Add(this.checkPat);
			this.Controls.Add(this.listCategory);
			this.Controls.Add(this.textAmount);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.labelAmount);
			this.Controls.Add(this.labelADACode);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormBenefitEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Benefit";
			this.Load += new System.EventHandler(this.FormBenefitEdit_Load);
			this.groupQuantity.ResumeLayout(false);
			this.groupQuantity.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormBenefitEdit_Load(object sender, System.EventArgs e) {
			if(BenCur==null){
				MessageBox.Show("Benefit cannot be null.");
			}
			if(BenCur.PatPlanNum==0){//attached to insplan
				checkPat.Checked=false;
			}
			else{
				checkPat.Checked=true;
			}
			listCategory.Items.Clear();
			for(int i=0;i<CovCats.ListShort.Length;i++){
				listCategory.Items.Add(CovCats.ListShort[i].Description);
				if(CovCats.ListShort[i].CovCatNum==BenCur.CovCatNum){
					listCategory.SelectedIndex=i;
				}
			}
			if(listCategory.SelectedIndex==-1 && CovCats.ListShort.Length>0){
				listCategory.SelectedIndex=0;
			}
			textADACode.Text=BenCur.ADACode;
			listType.Items.Clear();
			for(int i=0;i<Enum.GetNames(typeof(InsBenefitType)).Length;i++){
				listType.Items.Add(Lan.g("enumInsBenefitType",Enum.GetNames(typeof(InsBenefitType))[i]));
				if((int)BenCur.BenefitType==i){
					listType.SelectedIndex=i;
				}
			}
			textPercent.Text=BenCur.Percent.ToString();
			textAmount.Text=BenCur.MonetaryAmt.ToString("n");
			listTimePeriod.Items.Clear();
			for(int i=0;i<Enum.GetNames(typeof(BenefitTimePeriod)).Length;i++) {
				listTimePeriod.Items.Add(Lan.g("enumBenefitTimePeriod",Enum.GetNames(typeof(BenefitTimePeriod))[i]));
				if((int)BenCur.TimePeriod==i) {
					listTimePeriod.SelectedIndex=i;
				}
			}
			textQuantity.Text=BenCur.Quantity.ToString();
			listQuantityQualifier.Items.Clear();
			for(int i=0;i<Enum.GetNames(typeof(BenefitQuantity)).Length;i++) {
				listQuantityQualifier.Items.Add(Lan.g("enumBenefitQuantity",Enum.GetNames(typeof(BenefitQuantity))[i]));
				if((int)BenCur.QuantityQualifier==i) {
					listQuantityQualifier.SelectedIndex=i;
				}
			}
			//determine if this is an annual max
			if(textADACode.Text==""
				&& listType.SelectedIndex==(int)InsBenefitType.Limitations
				&& (listTimePeriod.SelectedIndex==(int)BenefitTimePeriod.CalendarYear
				|| listTimePeriod.SelectedIndex==(int)BenefitTimePeriod.ServiceYear)
				&& listQuantityQualifier.SelectedIndex==(int)BenefitQuantity.None)
			{
				isAnnualMax=true;
			}
			SetVisibilities();
		}

		///<summary></summary>
		private void SetVisibilities(){
			if(isAnnualMax){
				Text=Lan.g(this,"Edit Annual Max");
				listType.Visible=false;
				labelType.Visible=false;
				labelADACode.Visible=false;
				textADACode.Visible=false;
				labelPercent.Visible=false;
				textPercent.Visible=false;
				groupQuantity.Visible=false;
				listTimePeriod.Items.Clear();
				listTimePeriod.Items.Add(Lan.g("enumBenefitTimePeriod",BenefitTimePeriod.ServiceYear.ToString()));
				listTimePeriod.Items.Add(Lan.g("enumBenefitTimePeriod",BenefitTimePeriod.CalendarYear.ToString()));
				if(BenCur.TimePeriod==BenefitTimePeriod.ServiceYear) {
					listTimePeriod.SelectedIndex=0;
				}
				if(BenCur.TimePeriod==BenefitTimePeriod.CalendarYear) {
					listTimePeriod.SelectedIndex=1;
				}
				return;
			}
			if(listType.SelectedIndex==(int)InsBenefitType.Percentage){
				labelPercent.Visible=true;
				textPercent.Visible=true;
				labelAmount.Visible=false;
				textAmount.Visible=false;
				groupQuantity.Visible=false;
				textAmount.Text="0";
				textQuantity.Text="0";
				listQuantityQualifier.SelectedIndex=0;
			}
			else if(listType.SelectedIndex==(int)InsBenefitType.Deductible) {
				labelPercent.Visible=false;
				textPercent.Visible=false;
				labelAmount.Visible=true;
				textAmount.Visible=true;
				groupQuantity.Visible=false;
				textPercent.Text="0";
				textQuantity.Text="0";
				listQuantityQualifier.SelectedIndex=0;
			}
			else{
				labelPercent.Visible=false;
				textPercent.Visible=false;
				labelAmount.Visible=true;
				textAmount.Visible=true;
				groupQuantity.Visible=true;
				textPercent.Text="0";
			}
		}

		private void listType_Click(object sender,EventArgs e) {
			//selected index will already have changed
			SetVisibilities();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if( textPercent.errorProvider1.GetError(textPercent)!=""
				|| textAmount.errorProvider1.GetError(textAmount)!=""
				|| textQuantity.errorProvider1.GetError(textQuantity)!=""
				){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(listCategory.SelectedIndex==-1){
				//this can only happen if hidden categories changed
				MsgBox.Show(this,"Please select a category first.");
				return;
			}
			//not allowed to set to pat if editing plan only, and no patplanNum is available
			if(PatPlanNum==0 && checkPat.Checked){
				MsgBox.Show(this,"Not allowed to check the Pat box because no patient is available.");
				return;
			}
			if(listType.SelectedIndex != (int)InsBenefitType.Percentage && PIn.PInt(textPercent.Text)!=0){
				MsgBox.Show(this,"Not allowed to enter a percentage unless type is Percentage.");
				return;
			}
			if(checkPat.Checked){
				BenCur.PatPlanNum=PatPlanNum;
				BenCur.PlanNum=0;
			}
			else{
				BenCur.PatPlanNum=0;
				BenCur.PlanNum=PlanNum;
			}
			BenCur.CovCatNum=CovCats.ListShort[listCategory.SelectedIndex].CovCatNum;
			BenCur.ADACode=textADACode.Text;
			BenCur.BenefitType=(InsBenefitType)listType.SelectedIndex;
			BenCur.Percent=PIn.PInt(textPercent.Text);
			BenCur.MonetaryAmt=PIn.PDouble(textAmount.Text);
			if(isAnnualMax){
				if(listTimePeriod.SelectedIndex==0){
					BenCur.TimePeriod=BenefitTimePeriod.ServiceYear;
				}
				if(listTimePeriod.SelectedIndex==1){
					BenCur.TimePeriod=BenefitTimePeriod.CalendarYear;
				}
			}
			else{
				BenCur.TimePeriod=(BenefitTimePeriod)listTimePeriod.SelectedIndex;
			}
			BenCur.Quantity=PIn.PInt(textQuantity.Text);
			BenCur.QuantityQualifier=(BenefitQuantity)listQuantityQualifier.SelectedIndex;
			//if(IsNew){
			//	BenCur.Insert();
			//}
			//else{
			//	BenCur.Update();
			//}
			DialogResult=DialogResult.OK;
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			BenCur=null;
			if(IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			if(IsNew){
				BenCur=null;
			}
			DialogResult=DialogResult.Cancel;
		}

	

	}


}