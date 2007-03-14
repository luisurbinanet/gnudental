using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormJournal:System.Windows.Forms.Form {
		private OpenDental.UI.ODToolBar ToolBarMain;
		private OpenDental.UI.ODGrid gridMain;
		private IContainer components;
		private Account AccountCur;
		private ImageList imageListMain;
		private JournalEntry[] JournalList;

		///<summary></summary>
		public FormJournal(Account accountCur)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			AccountCur=accountCur;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormJournal));
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.gridMain = new OpenDental.UI.ODGrid();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.SuspendLayout();
			// 
			// imageListMain
			// 
			this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
			this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListMain.Images.SetKeyName(0,"Add.gif");
			// 
			// gridMain
			// 
			this.gridMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(0,29);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(844,642);
			this.gridMain.TabIndex = 1;
			this.gridMain.Title = null;
			this.gridMain.TranslationName = "TableJournal";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListMain;
			this.ToolBarMain.Location = new System.Drawing.Point(0,0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(844,29);
			this.ToolBarMain.TabIndex = 0;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// FormJournal
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(844,671);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.ToolBarMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormJournal";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Transaction History";
			this.Load += new System.EventHandler(this.FormJournal_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormJournal_Load(object sender,EventArgs e) {
			LayoutToolBar();
			FillGrid();
		}

		///<summary>Causes the toolbar to be laid out again.</summary>
		public void LayoutToolBar() {
			ToolBarMain.Buttons.Clear();
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Add Entry"),0,"","Add"));
			if(AccountCur.AcctType==AccountType.Asset){
				ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Reconcile"),-1,"","Reconcile"));
			}
			//ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Compound"),0,"","Compound"));
			//ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			//ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Edit"),-1,Lan.g(this,"Edit Selected Account"),"Edit"));
			/*ODToolBarButton button=new ODToolBarButton("",-1,"","PageNum");
			button.Style=ODToolBarButtonStyle.Label;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton("",2,"Go Forward One Page","Fwd"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Close"),-1,"Close This Window","Close"));*/
		}

		private void ToolBarMain_ButtonClick(object sender,OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			switch(e.Button.Tag.ToString()) {
				case "Add":
					Add_Click();
					break;
				case "Reconcile":
					Reconcile_Click();
					break;
			}
			/*	case "Fwd":
					OnFwd_Click();
					break;
				case "Close":
					OnClose_Click();
					break;
			}*/
		}

		private void FillGrid(){
			JournalList=JournalEntries.GetForAccount(AccountCur.AccountNum);
			gridMain.BeginUpdate();
			gridMain.Title=AccountCur.Description+" ("+Lan.g("enumAccountType",AccountCur.AcctType.ToString())+")";
			gridMain.Columns.Clear();
			string str="";
			ODGridColumn col=new ODGridColumn(Lan.g("TableJournal","Chk #"),60,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableJournal","Date"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableJournal","Memo"),220);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableJournal","Splits"),220);
			gridMain.Columns.Add(col);
			str=Lan.g("TableJournal","Debit");
			if(Accounts.DebitIsPos(AccountCur.AcctType)){
				str+=Lan.g("TableJournal","(+)");
			}
			else{
				str+=Lan.g("TableJournal","(-)");
			}
			col=new ODGridColumn(str,70,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			str=Lan.g("TableJournal","Credit");
			if(Accounts.DebitIsPos(AccountCur.AcctType)) {
				str+=Lan.g("TableJournal","(-)");
			}
			else {
				str+=Lan.g("TableJournal","(+)");
			}
			col=new ODGridColumn(str,70,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableJournal","Balance"),70,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableJournal","Clear"),55,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			double bal=0;
			for(int i=0;i<JournalList.Length;i++){
				row=new ODGridRow();
				row.Cells.Add(JournalList[i].CheckNumber);
				row.Cells.Add(JournalList[i].DateDisplayed.ToShortDateString());
				row.Cells.Add(JournalList[i].Memo);
				row.Cells.Add(JournalList[i].Splits);
				if(JournalList[i].DebitAmt==0){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(JournalList[i].DebitAmt.ToString("n"));
					if(Accounts.DebitIsPos(AccountCur.AcctType)){//this one is used for checking account
						bal+=JournalList[i].DebitAmt;
					}
					else{
						bal-=JournalList[i].DebitAmt;
					}
				}
				if(JournalList[i].CreditAmt==0) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(JournalList[i].CreditAmt.ToString("n"));
					if(Accounts.DebitIsPos(AccountCur.AcctType)) {//this one is used for checking account
						bal-=JournalList[i].CreditAmt;
					}
					else {
						bal+=JournalList[i].CreditAmt;
					}
				}
				row.Cells.Add(bal.ToString("n"));
				if(JournalList[i].ReconcileNum==0){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add("X");
				}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			gridMain.ScrollToEnd();
		}

		private void Add_Click(){
			Transaction trans=new Transaction();
			trans.UserNum=Security.CurUser.UserNum;
			trans.Insert();//we now have a TransactionNum, and datetimeEntry has been set
			FormTransactionEdit FormT=new FormTransactionEdit(trans.TransactionNum,AccountCur.AccountNum);
			FormT.IsNew=true;
			FormT.ShowDialog();
			if(FormT.DialogResult==DialogResult.Cancel){
				//no need to try-catch, since no journal entries were saved.
				trans.Delete();
			}
			FillGrid();
		}

		private void Reconcile_Click() {
			FormReconciles FormR=new FormReconciles(AccountCur.AccountNum);
			FormR.ShowDialog();
			FillGrid();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormTransactionEdit FormT=new FormTransactionEdit(JournalList[e.Row].TransactionNum,AccountCur.AccountNum);
			FormT.ShowDialog();
			if(FormT.DialogResult==DialogResult.Cancel) {
				return;
			}
			FillGrid();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		


	}
}





















