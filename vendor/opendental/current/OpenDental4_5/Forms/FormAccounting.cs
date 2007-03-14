using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormAccounting:System.Windows.Forms.Form {
		private OpenDental.UI.ODToolBar ToolBarMain;
		private OpenDental.UI.ODGrid gridMain;
		private IContainer components;
		private CheckBox checkInactive;
		private MainMenu mainMenu1;
		private MenuItem menuItemSetup;
		private ImageList imageListMain;
		//private Account[] AccountList;

		///<summary></summary>
		public FormAccounting()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAccounting));
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.checkInactive = new System.Windows.Forms.CheckBox();
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuItemSetup = new System.Windows.Forms.MenuItem();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.SuspendLayout();
			// 
			// imageListMain
			// 
			this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
			this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListMain.Images.SetKeyName(0,"Add.gif");
			this.imageListMain.Images.SetKeyName(1,"editPencil.gif");
			// 
			// checkInactive
			// 
			this.checkInactive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkInactive.AutoSize = true;
			this.checkInactive.Location = new System.Drawing.Point(12,639);
			this.checkInactive.Name = "checkInactive";
			this.checkInactive.Size = new System.Drawing.Size(150,17);
			this.checkInactive.TabIndex = 2;
			this.checkInactive.Text = "Include Inactive Accounts";
			this.checkInactive.UseVisualStyleBackColor = true;
			this.checkInactive.Click += new System.EventHandler(this.checkInactive_Click);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemSetup});
			// 
			// menuItemSetup
			// 
			this.menuItemSetup.Index = 0;
			this.menuItemSetup.Text = "Setup";
			this.menuItemSetup.Click += new System.EventHandler(this.menuItemSetup_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(0,30);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(475,603);
			this.gridMain.TabIndex = 1;
			this.gridMain.Title = "Chart of Accounts";
			this.gridMain.TranslationName = "TableChartOfAccounts";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListMain;
			this.ToolBarMain.Location = new System.Drawing.Point(0,0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(475,29);
			this.ToolBarMain.TabIndex = 0;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// FormAccounting
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(475,657);
			this.Controls.Add(this.checkInactive);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.ToolBarMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Menu = this.mainMenu1;
			this.MinimizeBox = false;
			this.Name = "FormAccounting";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Accounting";
			this.Load += new System.EventHandler(this.FormAccounting_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormAccounting_Load(object sender,EventArgs e) {
			LayoutToolBar();
			FillGrid();
		}

		///<summary>Causes the toolbar to be laid out again.</summary>
		public void LayoutToolBar() {
			ToolBarMain.Buttons.Clear();
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Add"),0,"","Add"));
			//ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Edit"),1,Lan.g(this,"Edit Selected Account"),"Edit"));
			/*ODToolBarButton button=new ODToolBarButton("",-1,"","PageNum");
			button.Style=ODToolBarButtonStyle.Label;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton("",2,"Go Forward One Page","Fwd"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));*/
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Close"),-1,"Close This Window","Close"));
		}

		private void menuItemSetup_Click(object sender,EventArgs e) {
			FormAccountingSetup FormA=new FormAccountingSetup();
			FormA.ShowDialog();
		}
		
		private void ToolBarMain_ButtonClick(object sender,OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			switch(e.Button.Tag.ToString()) {
				case "Add":
					Add_Click();
					break;
				case "Edit":
					Edit_Click();
					break;
				case "Close":
					Close();
					break;
			}
			/*	case "Fwd":
					OnFwd_Click();
					break;
				
			}*/
		}

		private void FillGrid(){
			Accounts.Refresh();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableChartOfAccounts","Type"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableChartOfAccounts","Description"),170);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableChartOfAccounts","Balance"),65,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableChartOfAccounts","Bank Number"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableChartOfAccounts","Inactive"),70,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<Accounts.ListLong.Length;i++){
				if(!checkInactive.Checked && Accounts.ListLong[i].Inactive){
					continue;
				}
				row=new ODGridRow();
				row.Cells.Add(Lan.g("enumAccountType",Accounts.ListLong[i].AcctType.ToString()));
				row.Cells.Add(Accounts.ListLong[i].Description);
				if(Accounts.ListLong[i].AcctType==AccountType.Asset){
					row.Cells.Add(Accounts.GetBalance(Accounts.ListLong[i].AccountNum,Accounts.ListLong[i].AcctType).ToString("n"));
				}
				else{
					row.Cells.Add("");
				}
				row.Cells.Add(Accounts.ListLong[i].BankNumber);
				if(Accounts.ListLong[i].Inactive){
					row.Cells.Add("X");
				}
				else{
					row.Cells.Add("");
				}
				if(i<Accounts.ListLong.Length-1//if not the last row
					&& Accounts.ListLong[i].AcctType != Accounts.ListLong[i+1].AcctType){
					row.ColorLborder=Color.Black;
				}
				row.Tag=Accounts.ListLong[i].Copy();
				row.ColorBackG=Accounts.ListLong[i].AccountColor;
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void Add_Click() {
			Account acct=new Account();
			acct.AcctType=AccountType.Asset;
			acct.AccountColor=Color.White;
			FormAccountEdit FormA=new FormAccountEdit(acct);
			FormA.IsNew=true;
			FormA.ShowDialog();
			FillGrid();
		}

		private void Edit_Click() {
			if(gridMain.GetSelectedIndex()==-1){
				MsgBox.Show(this,"Please pick an account first.");
				return;
			}
			FormAccountEdit FormA=new FormAccountEdit((Account)gridMain.Rows[gridMain.GetSelectedIndex()].Tag);
			FormA.ShowDialog();
			FillGrid();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormJournal FormJ=new FormJournal((Account)gridMain.Rows[gridMain.GetSelectedIndex()].Tag);
			FormJ.ShowDialog();
			FillGrid();
		}

		private void checkInactive_Click(object sender,EventArgs e) {
			FillGrid();
		}

	

		


		

	

		

		

	


	}
}





















