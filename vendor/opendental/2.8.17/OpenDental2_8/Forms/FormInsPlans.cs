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
	public class FormInsPlans : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.ComponentModel.Container components = null;// Required designer variable.
		//private InsTemplates InsTemplates;
		private System.Windows.Forms.Button butBlank;
		private OpenDental.XPButton butDelete;
		private OpenDental.XPButton butAdd;
		private OpenDental.XPButton butEdit;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private OpenDental.XPButton butCopy;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton radioOrderCarrier;
		private System.Windows.Forms.RadioButton radioOrderEmp;
		//<summary>Set to true if we are only using the list to select a template to link to rather than creating a new plan. If this is true, then IsSelectMode will be ignored.</summary>
		//public bool IsLinkMode;
		private OpenDental.TableTemplates Table2;
		private System.Windows.Forms.Button butCombine;
		private System.Windows.Forms.GroupBox groupEdit;
		private System.Windows.Forms.Label label3;
		///<summary>Set to true when selecting a plan for a patient and we want the current plan to be altered on closing</summary>
		public bool IsSelectMode;

		///<summary></summary>
		public FormInsPlans(){
			InitializeComponent();// Required for Windows Form Designer support
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label1,
				label2,
				//label3,
				butBlank,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butAdd,
				butDelete,
				butEdit,
				butCancel
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormInsPlans));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.butCancel = new System.Windows.Forms.Button();
			this.butBlank = new System.Windows.Forms.Button();
			this.butDelete = new OpenDental.XPButton();
			this.butAdd = new OpenDental.XPButton();
			this.butEdit = new OpenDental.XPButton();
			this.groupEdit = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.butCopy = new OpenDental.XPButton();
			this.butCombine = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.radioOrderCarrier = new System.Windows.Forms.RadioButton();
			this.radioOrderEmp = new System.Windows.Forms.RadioButton();
			this.Table2 = new OpenDental.TableTemplates();
			this.groupEdit.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(220, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Shortcut: Press a letter on your keyboard";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(300, 4);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(184, 12);
			this.label2.TabIndex = 5;
			this.label2.Text = "Double Click on List to Select Plan";
			// 
			// butCancel
			// 
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(871, 634);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(86, 26);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butBlank
			// 
			this.butBlank.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butBlank.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butBlank.Location = new System.Drawing.Point(535, 634);
			this.butBlank.Name = "butBlank";
			this.butBlank.Size = new System.Drawing.Size(87, 26);
			this.butBlank.TabIndex = 3;
			this.butBlank.Text = "Blank Plan";
			this.butBlank.Visible = false;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(101, 13);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(85, 26);
			this.butDelete.TabIndex = 8;
			this.butDelete.Text = "Delete";
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(13, 13);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(85, 26);
			this.butAdd.TabIndex = 7;
			this.butAdd.Text = "Add";
			// 
			// butEdit
			// 
			this.butEdit.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEdit.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butEdit.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butEdit.Image = ((System.Drawing.Image)(resources.GetObject("butEdit.Image")));
			this.butEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butEdit.Location = new System.Drawing.Point(189, 13);
			this.butEdit.Name = "butEdit";
			this.butEdit.Size = new System.Drawing.Size(85, 26);
			this.butEdit.TabIndex = 9;
			this.butEdit.Text = "Edit";
			// 
			// groupEdit
			// 
			this.groupEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupEdit.Controls.Add(this.label3);
			this.groupEdit.Controls.Add(this.butCopy);
			this.groupEdit.Controls.Add(this.butDelete);
			this.groupEdit.Controls.Add(this.butAdd);
			this.groupEdit.Controls.Add(this.butEdit);
			this.groupEdit.Controls.Add(this.butCombine);
			this.groupEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupEdit.Location = new System.Drawing.Point(135, 456);
			this.groupEdit.Name = "groupEdit";
			this.groupEdit.Size = new System.Drawing.Size(459, 70);
			this.groupEdit.TabIndex = 10;
			this.groupEdit.TabStop = false;
			this.groupEdit.Text = "Edit List";
			this.groupEdit.Visible = false;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(11, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(451, 23);
			this.label3.TabIndex = 11;
			this.label3.Text = "The concept of templates has been phased out. So this section does not make sense" +
				"";
			// 
			// butCopy
			// 
			this.butCopy.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCopy.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butCopy.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butCopy.Image = ((System.Drawing.Image)(resources.GetObject("butCopy.Image")));
			this.butCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butCopy.Location = new System.Drawing.Point(277, 13);
			this.butCopy.Name = "butCopy";
			this.butCopy.Size = new System.Drawing.Size(85, 26);
			this.butCopy.TabIndex = 10;
			this.butCopy.Text = "Copy";
			// 
			// butCombine
			// 
			this.butCombine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butCombine.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCombine.Location = new System.Drawing.Point(359, 11);
			this.butCombine.Name = "butCombine";
			this.butCombine.Size = new System.Drawing.Size(85, 26);
			this.butCombine.TabIndex = 11;
			this.butCombine.Text = "Combine";
			this.butCombine.Visible = false;
			// 
			// butOK
			// 
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(776, 634);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(86, 26);
			this.butOK.TabIndex = 11;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox2.Controls.Add(this.radioOrderCarrier);
			this.groupBox2.Controls.Add(this.radioOrderEmp);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(4, 621);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(115, 44);
			this.groupBox2.TabIndex = 12;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Order By";
			// 
			// radioOrderCarrier
			// 
			this.radioOrderCarrier.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioOrderCarrier.Location = new System.Drawing.Point(9, 27);
			this.radioOrderCarrier.Name = "radioOrderCarrier";
			this.radioOrderCarrier.Size = new System.Drawing.Size(99, 16);
			this.radioOrderCarrier.TabIndex = 1;
			this.radioOrderCarrier.Text = "Carrier";
			this.radioOrderCarrier.Click += new System.EventHandler(this.radioOrderCarrier_Click);
			// 
			// radioOrderEmp
			// 
			this.radioOrderEmp.Checked = true;
			this.radioOrderEmp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioOrderEmp.Location = new System.Drawing.Point(9, 13);
			this.radioOrderEmp.Name = "radioOrderEmp";
			this.radioOrderEmp.Size = new System.Drawing.Size(100, 16);
			this.radioOrderEmp.TabIndex = 0;
			this.radioOrderEmp.TabStop = true;
			this.radioOrderEmp.Text = "Employer";
			this.radioOrderEmp.Click += new System.EventHandler(this.radioOrderEmp_Click);
			// 
			// Table2
			// 
			this.Table2.BackColor = System.Drawing.SystemColors.Window;
			this.Table2.Location = new System.Drawing.Point(11, 28);
			this.Table2.Name = "Table2";
			this.Table2.ScrollValue = 1;
			this.Table2.SelectedIndices = new int[0];
			this.Table2.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.Table2.Size = new System.Drawing.Size(936, 594);
			this.Table2.TabIndex = 14;
			this.Table2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Table2_KeyPress);
			this.Table2.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(this.Table2_CellDoubleClicked);
			// 
			// FormInsPlans
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(962, 667);
			this.Controls.Add(this.groupEdit);
			this.Controls.Add(this.Table2);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butBlank);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "FormInsPlans";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Insurance Plans";
			this.Load += new System.EventHandler(this.FormInsTemplates_Load);
			this.groupEdit.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormInsTemplates_Load(object sender, System.EventArgs e) {
			if(IsSelectMode){
				//groupEdit.Visible=false;
				//butCombine.Visible=false;
			}
			else{
				//butBlank.Visible=false;
			}
			FillGrid();
		}

		private void FillGrid(){
			if(radioOrderEmp.Checked){//order by employer,carrier
				InsPlans.RefreshListAll(true);
			}
			else{//order by carrier only
				InsPlans.RefreshListAll(false);
			}
			int selectedRow=Table2.SelectedRow;//preserves the selected row.
			Table2.ResetRows(InsPlans.ListAll.Length);
			Table2.SetGridColor(Color.Gray);
			for(int i=0;i<InsPlans.ListAll.Length;i++){
				if(radioOrderEmp.Checked 
					&& i>0 && InsPlans.ListAll[i].EmployerNum==InsPlans.ListAll[i-1].EmployerNum)
					Table2.Cell[0,i]="";//suppresses duplicate employer names for easier reading
				else
					Table2.Cell[0,i]=Employers.GetName(InsPlans.ListAll[i].EmployerNum);
				//MessageBox.Show(InsPlans.ListAll[i].CarrierNum.ToString());
				Carriers.GetCur(InsPlans.ListAll[i].CarrierNum);
				Table2.Cell[1,i]=Carriers.Cur.CarrierName;
				Table2.Cell[2,i]=Carriers.Cur.Phone;
				Table2.Cell[3,i]=Carriers.Cur.Address;
				Table2.Cell[4,i]=Carriers.Cur.City;
				Table2.Cell[5,i]=Carriers.Cur.State;
				Table2.Cell[6,i]=Carriers.Cur.Zip;
				Table2.Cell[7,i]=InsPlans.ListAll[i].GroupNum;
				Table2.Cell[8,i]=InsPlans.ListAll[i].GroupName;
				if(Carriers.Cur.NoSendElect)
					Table2.Cell[9,i]="X";
				Table2.Cell[10,i]=Carriers.Cur.ElectID;
				Table2.Cell[11,i]=InsPlans.ListAll[i].NumberPlans.ToString();
			}
			//Table2.ColWidth[9]=Width-140-140-120-80-25-50-75-35-45-25;//25 is for scroll bar
			//Table2.Height=Height-105;
			Table2.LayoutTables(true);
			//Table2.SetSelected(selectedRow,true);
			Table2.SetSelectedRow(selectedRow);
			Table2.Select();
		}

		private void Table2_CellDoubleClicked(object sender, CellEventArgs e){
			if(Table2.SelectedRow==-1)
				return;//this might happen if user holds down control key while double clicking??
			if(IsSelectMode){
				AlterCurPlan();
				DialogResult=DialogResult.OK;
				return;
			}
			FormInsPlanEditAll FormIPE=new FormInsPlanEditAll();
			InsPlans.Cur=InsPlans.ListAll[e.Row];
			FormIPE.ShowDialog();
			if(FormIPE.DialogResult!=DialogResult.OK)
				return;
			FillGrid();
		}

		private void AlterCurPlan(){
			//this is called from OK and from doubleclick. Requires that a row is selected first
			/*
			InsPlans.Cur=InsPlans.ListAll[Table2.SelectedRow];
			InsPlans.Cur.Subscriber   =Patients.Cur.PatNum;
			InsPlans.Cur.SubscriberID =Patients.Cur.SSN;
			//Synchronized Information already handled
			InsPlans.Cur.AnnualMax=-1;
			InsPlans.Cur.OrthoMax=-1;
			InsPlans.Cur.RenewMonth=1;
			InsPlans.Cur.Deductible=-1;
			InsPlans.Cur.FloToAge=-1;
			InsPlans.Cur.ReleaseInfo=true;
			InsPlans.Cur.AssignBen=true;
			InsPlans.InsertCur();
			//note that currently some items for a new plan are set from within FormInsPlan
			FormInsPlan FormIP=new FormInsPlan();
			FormIP.IsNew=true;
			FormIP.ShowDialog();
			if(FormIP.DialogResult!=DialogResult.OK){
				FillGrid();
				return;
			}*/
			InsPlans.Cur.EmployerNum  =InsPlans.ListAll[Table2.SelectedRow].EmployerNum;
			InsPlans.Cur.GroupName    =InsPlans.ListAll[Table2.SelectedRow].GroupName;
			InsPlans.Cur.GroupNum     =InsPlans.ListAll[Table2.SelectedRow].GroupNum;
			InsPlans.Cur.CarrierNum   =InsPlans.ListAll[Table2.SelectedRow].CarrierNum;
			InsPlans.Cur.PlanType     =InsPlans.ListAll[Table2.SelectedRow].PlanType;
			InsPlans.Cur.UseAltCode   =InsPlans.ListAll[Table2.SelectedRow].UseAltCode;
			InsPlans.Cur.ClaimsUseUCR =InsPlans.ListAll[Table2.SelectedRow].ClaimsUseUCR;
			InsPlans.Cur.FeeSched     =InsPlans.ListAll[Table2.SelectedRow].FeeSched;
			InsPlans.Cur.CopayFeeSched=InsPlans.ListAll[Table2.SelectedRow].CopayFeeSched;
			InsPlans.Cur.ClaimFormNum =InsPlans.ListAll[Table2.SelectedRow].ClaimFormNum;
		}

		private void radioOrderEmp_Click(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void radioOrderCarrier_Click(object sender, System.EventArgs e) {
			FillGrid();
		}

		#region obsolete

		/*private void butAdd_Click(object sender, System.EventArgs e) {
			
			FormInsTemplateEdit FormITE=new FormInsTemplateEdit();
			FormITE.IsNew=true;
			InsTemplates.Cur=new InsTemplate();
			FormITE.ShowDialog();
			//if user hits cancel, then the template won't have been saved and there will be no current template
			if(FormITE.DialogResult!=DialogResult.OK)
				return;
			FillGrid();
		}*/

		/*private void butDelete_Click(object sender, System.EventArgs e) {
			
			if(Table2.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select items first."));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete Selected Templates?")
				,"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
				return;
			for(int i=0;i<Table2.SelectedIndices.Length;i++){
				InsTemplates.Cur=InsTemplates.List[Table2.SelectedIndices[i]];
				string[] dependentPlans=InsTemplates.LinkedPlans();
				if(dependentPlans.Length>0){
					MessageBox.Show(Lan.g(this
						,"Can not delete a selected template because it is linked to one or more insurance plans."));
					return;
				}
			}
			for(int i=0;i<Table2.SelectedIndices.Length;i++){
				InsTemplates.Cur=InsTemplates.List[Table2.SelectedIndices[i]];
				InsTemplates.DeleteCur();
			}
			FillGrid();
		}*/

		/*private void butEdit_Click(object sender, System.EventArgs e){
			if(Table2.SelectedIndices.Length!=1){
				MessageBox.Show(Lan.g(this,"Please select one item first."));
				return;
			}
			FormInsTemplateEdit FormITE=new FormInsTemplateEdit();
			InsTemplates.Cur=InsTemplates.List[Table2.SelectedIndices[0]];
			FormITE.ShowDialog();
			if(FormITE.DialogResult!=DialogResult.OK)
				return;
			FillGrid();
		}

		private void butCopy_Click(object sender, System.EventArgs e) {
			if(Table2.SelectedIndices.Length!=1){
				MessageBox.Show(Lan.g(this,"Please select one item first."));
				return;
			}
			InsTemplates.Cur=InsTemplates.List[Table2.SelectedIndices[0]];
			FormInsTemplateEdit FormITE=new FormInsTemplateEdit();
			FormITE.IsNew=true;
			FormITE.ShowDialog();
			if(FormITE.DialogResult!=DialogResult.OK)
				return;
			FillGrid();
		}*/

		/*private void butCombine_Click(object sender, System.EventArgs e) {
			if(Table2.SelectedIndices.Length<2){
				MessageBox.Show(Lan.g(this,"Please select multiple items first by clicking while holding down the control key."));
				return;
			}
			
		}*/

		/*private void butBlank_Click(object sender, System.EventArgs e) {
			//this button is not visible
			InsPlans.Cur = new InsPlan();
			InsPlans.Cur.Subscriber=Patients.Cur.PatNum;
			InsPlans.Cur.AnnualMax=-1;//blank
			InsPlans.Cur.OrthoMax=-1;
			InsPlans.Cur.RenewMonth=1;
			InsPlans.Cur.Deductible=-1;
			InsPlans.Cur.FloToAge=-1;
			InsPlans.Cur.ReleaseInfo=true;
			InsPlans.Cur.AssignBen=true;
			InsPlans.InsertCur();
			FormInsPlan FormIP=new FormInsPlan();
			FormIP.IsNew=true;
			FormIP.ShowDialog();
			if(FormIP.DialogResult!=DialogResult.OK){
				return;
			}
			DialogResult=DialogResult.OK;
		}*/
		#endregion

		private void Table2_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e) {
			int moveToRow=Table2.MaxRows;//in case there are no y's or z's
			for(int i=0;i<InsPlans.ListAll.Length;i++){
				if(String.Compare(e.KeyChar.ToString()
					,Carriers.GetName(InsPlans.ListAll[i].CarrierNum),true)<=0)
				{
					moveToRow=i;
					break;
				}
			}
			Table2.ScrollToLine(moveToRow);
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(IsSelectMode){
				if(Table2.SelectedRow==-1){
					MessageBox.Show(Lan.g(this,"Please select an item first."));
					return;
				}
				//CreateNewPlan();//if the user then hits cancel, then this will not close this window.
				AlterCurPlan();
				DialogResult=DialogResult.OK;
			}
			else{//just editing the list from the main menu
				DialogResult=DialogResult.OK;
			}
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	

		

		

		
		

		

		

	}
}


















