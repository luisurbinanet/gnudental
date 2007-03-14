/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;

namespace OpenDental{
///<summary></summary>
	public class FormInsPlans:System.Windows.Forms.Form {
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
		///<summary>Set to true when selecting a plan for a patient and we want SelectedPlan to be filled upon closing.</summary>
		public bool IsSelectMode;
		///<summary>After closing this form, if IsSelectMode, then this will contain the selected Plan.</summary>
		public InsPlan SelectedPlan;
		private Label label1;
		private TextBox textEmployer;
		private TextBox textCarrier;
		private Label label2;
		private OpenDental.UI.ODGrid gridMain;
		private InsPlan[] ListAll;
		///<summary>Supply a string here to start off the search with filtered employers.</summary>
		public string empText;
		///<summary>Supply a string here to start off the search with filtered carriers.</summary>
		public string carrierText;

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
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.radioOrderCarrier = new System.Windows.Forms.RadioButton();
			this.radioOrderEmp = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.textEmployer = new System.Windows.Forms.TextBox();
			this.textCarrier = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butOK = new OpenDental.UI.Button();
			this.butBlank = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox2.Controls.Add(this.radioOrderCarrier);
			this.groupBox2.Controls.Add(this.radioOrderEmp);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(740,3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(207,33);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Order By";
			// 
			// radioOrderCarrier
			// 
			this.radioOrderCarrier.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioOrderCarrier.Location = new System.Drawing.Point(98,13);
			this.radioOrderCarrier.Name = "radioOrderCarrier";
			this.radioOrderCarrier.Size = new System.Drawing.Size(84,16);
			this.radioOrderCarrier.TabIndex = 1;
			this.radioOrderCarrier.Text = "Carrier";
			this.radioOrderCarrier.Click += new System.EventHandler(this.radioOrderCarrier_Click);
			// 
			// radioOrderEmp
			// 
			this.radioOrderEmp.Checked = true;
			this.radioOrderEmp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioOrderEmp.Location = new System.Drawing.Point(9,13);
			this.radioOrderEmp.Name = "radioOrderEmp";
			this.radioOrderEmp.Size = new System.Drawing.Size(83,16);
			this.radioOrderEmp.TabIndex = 0;
			this.radioOrderEmp.TabStop = true;
			this.radioOrderEmp.Text = "Employer";
			this.radioOrderEmp.Click += new System.EventHandler(this.radioOrderEmp_Click);
			// 
			// label1
			// 
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(12,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,17);
			this.label1.TabIndex = 15;
			this.label1.Text = "Employer";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textEmployer
			// 
			this.textEmployer.Location = new System.Drawing.Point(118,6);
			this.textEmployer.Name = "textEmployer";
			this.textEmployer.Size = new System.Drawing.Size(140,20);
			this.textEmployer.TabIndex = 1;
			this.textEmployer.TextChanged += new System.EventHandler(this.textEmployer_TextChanged);
			// 
			// textCarrier
			// 
			this.textCarrier.Location = new System.Drawing.Point(390,6);
			this.textCarrier.Name = "textCarrier";
			this.textCarrier.Size = new System.Drawing.Size(140,20);
			this.textCarrier.TabIndex = 0;
			this.textCarrier.TextChanged += new System.EventHandler(this.textCarrier_TextChanged);
			// 
			// label2
			// 
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(284,9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,17);
			this.label2.TabIndex = 17;
			this.label2.Text = "Carrier";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(11,47);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(936,583);
			this.gridMain.TabIndex = 19;
			this.gridMain.Title = "Insurance Plans";
			this.gridMain.TranslationName = "TableTemplates";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(776,636);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(78,26);
			this.butOK.TabIndex = 4;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butBlank
			// 
			this.butBlank.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBlank.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butBlank.Autosize = true;
			this.butBlank.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBlank.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBlank.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butBlank.Location = new System.Drawing.Point(427,636);
			this.butBlank.Name = "butBlank";
			this.butBlank.Size = new System.Drawing.Size(87,26);
			this.butBlank.TabIndex = 3;
			this.butBlank.Text = "Blank Plan";
			this.butBlank.Visible = false;
			this.butBlank.Click += new System.EventHandler(this.butBlank_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(871,636);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(77,26);
			this.butCancel.TabIndex = 5;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormInsPlans
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(962,669);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.textCarrier);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textEmployer);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butBlank);
			this.Controls.Add(this.butCancel);
			this.Name = "FormInsPlans";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Insurance Plans";
			this.Load += new System.EventHandler(this.FormInsTemplates_Load);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormInsTemplates_Load(object sender, System.EventArgs e) {
			textEmployer.Text=empText;
			textCarrier.Text=carrierText;
			FillGrid();
		}

		private void FillGrid(){
			Cursor=Cursors.WaitCursor;
			if(radioOrderEmp.Checked){//order by employer,carrier
				ListAll=InsPlans.RefreshListAll(true,textEmployer.Text,textCarrier.Text);
			}
			else{//order by carrier only
				ListAll=InsPlans.RefreshListAll(false,textEmployer.Text,textCarrier.Text);
			}
			if(IsSelectMode){
				butBlank.Visible=true;
			}
			int selectedRow;//preserves the selected row.
			if(gridMain.SelectedIndices.Length==1){
				selectedRow=gridMain.SelectedIndices[0];
			}
			else{
				selectedRow=-1;
			}
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Employer",140);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Carrier",140);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Phone",82);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Address",120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("City",80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("ST",25);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Zip",50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Group#",70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Group Name",90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("noE",35);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("ElectID",45);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Plans",40);
			gridMain.Columns.Add(col);
			//TrojanID and PlanNote not shown
			gridMain.Rows.Clear();
			ODGridRow row;
			Carrier carrier;
			for(int i=0;i<ListAll.Length;i++) {
				row=new ODGridRow();
				if(radioOrderEmp.Checked && i>0 && ListAll[i].EmployerNum==ListAll[i-1].EmployerNum)
					row.Cells.Add("");//suppresses duplicate employer names for easier reading
				else
					row.Cells.Add(Employers.GetName(ListAll[i].EmployerNum));
				//MessageBox.Show(InsPlans.ListAll[i].CarrierNum.ToString());
				carrier=Carriers.GetCarrier(ListAll[i].CarrierNum);
				row.Cells.Add(carrier.CarrierName);
				row.Cells.Add(carrier.Phone);
				row.Cells.Add(carrier.Address);
				row.Cells.Add(carrier.City);
				row.Cells.Add(carrier.State);
				row.Cells.Add(carrier.Zip);
				row.Cells.Add(ListAll[i].GroupNum);
				row.Cells.Add(ListAll[i].GroupName);
				if(Carriers.Cur.NoSendElect)
					row.Cells.Add("X");
				else
					row.Cells.Add("");
				row.Cells.Add(Carriers.Cur.ElectID);
				row.Cells.Add(ListAll[i].NumberPlans.ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			gridMain.SetSelected(selectedRow,true);
			Cursor=Cursors.Default;
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e){
			if(IsSelectMode) {
				SelectedPlan=ListAll[e.Row].Copy();
				DialogResult=DialogResult.OK;
				return;
			}
			InsPlan PlanCur=ListAll[e.Row].Copy();
			FormInsPlan FormIP=new FormInsPlan(PlanCur,null);
			FormIP.IsForAll=true;
			FormIP.ShowDialog();
			if(FormIP.DialogResult!=DialogResult.OK)
				return;
			FillGrid();
		}

		private void radioOrderEmp_Click(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void radioOrderCarrier_Click(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void textEmployer_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void textCarrier_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void butBlank_Click(object sender, System.EventArgs e) {
			SelectedPlan=new InsPlan();
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(IsSelectMode){
				if(gridMain.SelectedIndices.Length==0){
					MessageBox.Show(Lan.g(this,"Please select an item first."));
					return;
				}
				//CreateNewPlan();//if the user then hits cancel, then this will not close this window.
				//AlterCurPlan();
				SelectedPlan=ListAll[gridMain.SelectedIndices[0]].Copy();
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


















