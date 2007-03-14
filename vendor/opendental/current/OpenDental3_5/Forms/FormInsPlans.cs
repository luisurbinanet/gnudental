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
		private OpenDental.UI.Button butBlank;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton radioOrderCarrier;
		private System.Windows.Forms.RadioButton radioOrderEmp;
		//<summary>Set to true if we are only using the list to select a template to link to rather than creating a new plan. If this is true, then IsSelectMode will be ignored.</summary>
		//public bool IsLinkMode;
		private OpenDental.TableTemplates Table2;
		///<summary>Set to true when selecting a plan for a patient and we want the current plan to be altered on closing</summary>
		public bool IsSelectMode;
		///<summary>After closing this form, if IsSelectMode, then this will contain the selected Plan.</summary>
		public InsPlan SelectedPlan;
		private InsPlan[] ListAll;

		///<summary></summary>
		public FormInsPlans(){
			InitializeComponent();// Required for Windows Form Designer support
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.butCancel = new OpenDental.UI.Button();
			this.butBlank = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.radioOrderCarrier = new System.Windows.Forms.RadioButton();
			this.radioOrderEmp = new System.Windows.Forms.RadioButton();
			this.Table2 = new OpenDental.TableTemplates();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(290, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Shortcut: Press a letter on your keyboard";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(300, 4);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(334, 12);
			this.label2.TabIndex = 5;
			this.label2.Text = "Double Click on List to Select Plan";
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(871, 636);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(77, 26);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butBlank
			// 
			this.butBlank.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butBlank.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butBlank.Autosize = true;
			this.butBlank.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBlank.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBlank.Location = new System.Drawing.Point(535, 636);
			this.butBlank.Name = "butBlank";
			this.butBlank.Size = new System.Drawing.Size(87, 26);
			this.butBlank.TabIndex = 3;
			this.butBlank.Text = "Blank Plan";
			this.butBlank.Visible = false;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(776, 636);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(78, 26);
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
			this.groupBox2.Location = new System.Drawing.Point(4, 623);
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
			this.Table2.Size = new System.Drawing.Size(936, 595);
			this.Table2.TabIndex = 14;
			this.Table2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Table2_KeyPress);
			this.Table2.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(this.Table2_CellDoubleClicked);
			// 
			// FormInsPlans
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(962, 669);
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
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormInsTemplates_Load(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			if(radioOrderEmp.Checked){//order by employer,carrier
				ListAll=InsPlans.RefreshListAll(true);
			}
			else{//order by carrier only
				ListAll=InsPlans.RefreshListAll(false);
			}
			int selectedRow=Table2.SelectedRow;//preserves the selected row.
			Table2.ResetRows(ListAll.Length);
			Table2.SetGridColor(Color.Gray);
			Carrier carrier;
			for(int i=0;i<ListAll.Length;i++){
				if(radioOrderEmp.Checked 
					&& i>0 && ListAll[i].EmployerNum==ListAll[i-1].EmployerNum)
					Table2.Cell[0,i]="";//suppresses duplicate employer names for easier reading
				else
					Table2.Cell[0,i]=Employers.GetName(ListAll[i].EmployerNum);
				//MessageBox.Show(InsPlans.ListAll[i].CarrierNum.ToString());
				carrier=Carriers.GetCarrier(ListAll[i].CarrierNum);
				Table2.Cell[1,i]=carrier.CarrierName;
				Table2.Cell[2,i]=carrier.Phone;
				Table2.Cell[3,i]=carrier.Address;
				Table2.Cell[4,i]=carrier.City;
				Table2.Cell[5,i]=carrier.State;
				Table2.Cell[6,i]=carrier.Zip;
				Table2.Cell[7,i]=ListAll[i].GroupNum;
				Table2.Cell[8,i]=ListAll[i].GroupName;
				if(Carriers.Cur.NoSendElect)
					Table2.Cell[9,i]="X";
				Table2.Cell[10,i]=Carriers.Cur.ElectID;
				Table2.Cell[11,i]=ListAll[i].NumberPlans.ToString();
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
				SelectedPlan=ListAll[Table2.SelectedRow].Copy();
				DialogResult=DialogResult.OK;
				return;
			}
			InsPlan PlanCur=ListAll[e.Row].Copy();
			FormInsPlanEditAll FormIPE=new FormInsPlanEditAll(PlanCur,0);
			FormIPE.ShowDialog();
			if(FormIPE.DialogResult!=DialogResult.OK)
				return;
			FillGrid();
		}

		//<summary>This is called from OK and from doubleclick. Requires that a row is selected first</summary>
		/*private void AlterCurPlan(){
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
		}*/

		private void radioOrderEmp_Click(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void radioOrderCarrier_Click(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void Table2_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e) {
			int moveToRow=Table2.MaxRows;//in case there are no y's or z's
			for(int i=0;i<ListAll.Length;i++){
				if(String.Compare(e.KeyChar.ToString()
					,Carriers.GetName(ListAll[i].CarrierNum),true)<=0)
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
				//AlterCurPlan();
				SelectedPlan=ListAll[Table2.SelectedRow].Copy();
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


















