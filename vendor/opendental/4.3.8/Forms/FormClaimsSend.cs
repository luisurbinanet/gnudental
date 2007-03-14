using Microsoft.Win32;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.IO;
using System.Windows.Forms;
using OpenDental.UI;

namespace OpenDental{
///<summary></summary>
	public class FormClaimsSend:System.Windows.Forms.Form {
		private System.Windows.Forms.Label label6;
		private OpenDental.UI.Button butClose;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ContextMenu contextMenuStatus;
		private OpenDental.UI.ODToolBar ToolBarMain;
		///<summary>final list of eclaims(as Claim.ClaimNum) to send</summary>
		public static ArrayList eClaimList;
		private ODGrid gridMain;
		private ClaimSendQueueItem[] listQueue;
		///<summary></summary>
		public int GotoPatNum;
		///<summary></summary>
		public int GotoClaimNum;

		///<summary></summary>
		public FormClaimsSend(){
			InitializeComponent();
			//tbQueue.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbQueue_CellDoubleClicked);
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClaimsSend));
			this.label6 = new System.Windows.Forms.Label();
			this.butClose = new OpenDental.UI.Button();
			this.contextMenuStatus = new System.Windows.Forms.ContextMenu();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.SuspendLayout();
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label6.Location = new System.Drawing.Point(107,-44);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(112,44);
			this.label6.TabIndex = 21;
			this.label6.Text = "Insurance Claims";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(798,600);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,25);
			this.butClose.TabIndex = 22;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0,"");
			this.imageList1.Images.SetKeyName(1,"");
			this.imageList1.Images.SetKeyName(2,"");
			this.imageList1.Images.SetKeyName(3,"");
			this.imageList1.Images.SetKeyName(4,"");
			this.imageList1.Images.SetKeyName(5,"");
			this.imageList1.Images.SetKeyName(6,"");
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageList1;
			this.ToolBarMain.Location = new System.Drawing.Point(0,0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(890,29);
			this.ToolBarMain.TabIndex = 31;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(14,32);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(859,558);
			this.gridMain.TabIndex = 32;
			this.gridMain.Title = "Claims";
			this.gridMain.TranslationName = "TableQueue";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// FormClaimsSend
			// 
			this.AcceptButton = this.butClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(890,638);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.label6);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClaimsSend";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Send Claims";
			this.Load += new System.EventHandler(this.FormPendingClaims_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormPendingClaims_Load(object sender, System.EventArgs e) {
			contextMenuStatus.MenuItems.Add("Go to Account",new EventHandler(GotoAccount_Clicked));
			contextMenuStatus.MenuItems.Add("-");
			contextMenuStatus.MenuItems.Add("Unsent",new EventHandler(StatusUnsent_Clicked));
			contextMenuStatus.MenuItems.Add("Hold until Pri received",new EventHandler(StatusHold_Clicked));
			contextMenuStatus.MenuItems.Add("Waiting in Send List",new EventHandler(StatusWaiting_Clicked));
			contextMenuStatus.MenuItems.Add("Probably sent",new EventHandler(StatusProbably_Clicked));
			contextMenuStatus.MenuItems.Add("Sent - Verified",new EventHandler(StatusSent_Clicked));
			gridMain.ContextMenu=contextMenuStatus;
			//do not show received because that would mess up the balances
			//contextMenuStatus.MenuItems.Add("Received",new EventHandler(StatusReceived_Clicked));
			LayoutToolBar();
			FillGrid();
		}

		///<summary></summary>
		public void LayoutToolBar(){
			ToolBarMain.Buttons.Clear();
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Preview"),0,Lan.g(this,"Preview the Selected Claim"),"Preview"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Blank"),1,Lan.g(this,"Print a Blank Claim Form"),"Blank"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Print"),2,Lan.g(this,"Print Selected Claims"),"Print"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Labels"),6,Lan.g(this,"Print Single Labels"),"Labels"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ODToolBarButton button=new ODToolBarButton(Lan.g(this,"Status Sent"),3,Lan.g(this,"Changes Status of Selected Claims to Sent"),"Status");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=contextMenuStatus;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Send E-Claims"),4,Lan.g(this,"Send claims Electronically"),"Eclaims"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"View Reports"),5,Lan.g(this,"View Reports from Clearinghouses"),"Reports"));
			/*ArrayList toolButItems=ToolButItems.GetForToolBar(ToolBarsAvail.ClaimsSend);
			for(int i=0;i<toolButItems.Count;i++){
				ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
				ToolBarMain.Buttons.Add(new ODToolBarButton(((ToolButItem)toolButItems[i]).ButtonText
					,-1,"",((ToolButItem)toolButItems[i]).ProgramNum));
			}*/
			ToolBarMain.Invalidate();
		}

		private void ChangeSelectedTo(string claimStatus){
			if(gridMain.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select items first."));
				return;
			}
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				Claims.UpdateStatus(listQueue[gridMain.SelectedIndices[i]].ClaimNum,claimStatus);
			}
			FillGrid();
		}

		private void StatusUnsent_Clicked(object sender, System.EventArgs e){
			ChangeSelectedTo("U");
		}

		private void StatusHold_Clicked(object sender, System.EventArgs e){
			ChangeSelectedTo("H");
		}

		private void StatusWaiting_Clicked(object sender, System.EventArgs e){
			ChangeSelectedTo("W");
		}

		private void StatusProbably_Clicked(object sender, System.EventArgs e){
			ChangeSelectedTo("P");
		}

		private void StatusSent_Clicked(object sender, System.EventArgs e){
			ChangeSelectedTo("S");
		}

		private void GotoAccount_Clicked(object sender, System.EventArgs e){
			if(gridMain.SelectedIndices.Length!=1) {
				MsgBox.Show(this,"Please select exactly one item first.");
				return;
			}
			GotoPatNum=listQueue[gridMain.SelectedIndices[0]].PatNum;
			GotoClaimNum=listQueue[gridMain.SelectedIndices[0]].ClaimNum;
			DialogResult=DialogResult.OK;
		}

		private void FillGrid(){
			listQueue=Claims.GetQueueList();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableQueue","Patient Name"),130);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableQueue","Carrier Name"),170);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableQueue","Clearinghouse"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableQueue","Status"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableQueue","Missing Info"),300);
			gridMain.Columns.Add(col);			 
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listQueue.Length;i++){
				row=new ODGridRow();
				row.Cells.Add(listQueue[i].PatName);
				row.Cells.Add(listQueue[i].Carrier);
				if(listQueue[i].NoSendElect){
					row.Cells.Add("Paper");
				}
				else{
					row.Cells.Add(Clearinghouses.GetDescript(listQueue[i].ClearinghouseNum));
				}
				string status="";
				switch(listQueue[i].ClaimStatus){
					case "U"://unsent
						status="Unsent";
						break;
					case "H"://hold until pri received
						status="Hold";
						break;
					case "W"://waiting to be sent
						status="Waiting to Send";
						break;
					case "P"://probably sent
						status="Probably Sent";
						break;
					case "S"://sent-verified
						status="Sent-Verified";
						break;
					case "R"://received
						status="Received";
						break;
				}
				row.Cells.Add(Lan.g(this,status));
				if(listQueue[i].NoSendElect){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(Eclaims.Eclaims.GetMissingData(listQueue[i]));
				}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender, ODGridClickEventArgs e){
			FormClaimPrint FormCP;
			FormCP=new FormClaimPrint();
			FormCP.ThisPatNum=listQueue[e.Row].PatNum;
			FormCP.ThisClaimNum=listQueue[e.Row].ClaimNum;
			FormCP.PrintImmediately=false;
			FormCP.ShowDialog();				
			FillGrid();	
		}

		private void ToolBarMain_ButtonClick(object sender, OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			switch(e.Button.Tag.ToString()){
				case "Preview":
					OnPreview_Click();
					break;
				case "Blank":
					OnBlank_Click();
					break;
				case "Print":
					OnPrint_Click();
					break;
				case "Labels":
					OnLabels_Click();
					break;
				case "Status":
					OnStatus_Click();
					break;
				case "Eclaims":
					OnEclaims_Click();
					break;
				case "Reports":
					OnReports_Click();
					break;
			}
		}

		private void OnPreview_Click(){
			FormClaimPrint FormCP;
			FormCP=new FormClaimPrint();
			if(gridMain.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			if(gridMain.SelectedIndices.Length > 1){
				MessageBox.Show(Lan.g(this,"Please select only one claim."));
				return;
			}
			FormCP.ThisPatNum=listQueue[gridMain.GetSelectedIndex()].PatNum;
			FormCP.ThisClaimNum=listQueue[gridMain.GetSelectedIndex()].ClaimNum;
			FormCP.PrintImmediately=false;
			FormCP.ShowDialog();				
			FillGrid();	
		}

		private void OnBlank_Click(){
			PrintDocument pd=new PrintDocument();
			if(!Printers.SetPrinter(pd,PrintSituation.Claim)){
				return;
			}
			FormClaimPrint FormCP=new FormClaimPrint();
			FormCP.PrintBlank=true;
			FormCP.PrintImmediate(pd.PrinterSettings.PrinterName,pd.PrinterSettings.Copies);
		}

		private void OnPrint_Click(){
			FormClaimPrint FormCP=new FormClaimPrint();
			if(gridMain.SelectedIndices.Length==0){
				for(int i=0;i<listQueue.Length;i++){
					if((listQueue[i].ClaimStatus=="W" || listQueue[i].ClaimStatus=="P")
						&& listQueue[i].NoSendElect)
					{
						gridMain.SetSelected(i,true);		
					}	
				}
				if(MessageBox.Show(Lan.g(this,"No items were selected.  Print all selected paper claims?"),""
					,MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;
				}
			}
			PrintDocument pd=new PrintDocument();
			if(!Printers.SetPrinter(pd,PrintSituation.Claim)){
				return;
			}
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				FormCP.ThisPatNum=listQueue[gridMain.SelectedIndices[i]].PatNum;
				FormCP.ThisClaimNum=listQueue[gridMain.SelectedIndices[i]].ClaimNum;
				if(!FormCP.PrintImmediate(pd.PrinterSettings.PrinterName,1)){
					return;
				}
				Claims.UpdateStatus(listQueue[gridMain.SelectedIndices[i]].ClaimNum,"P");
			}
			FillGrid();
		}

		private void OnLabels_Click(){
			if(gridMain.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			PrintDocument pd=new PrintDocument();//only used to pass printerName
			if(!Printers.SetPrinter(pd,PrintSituation.LabelSingle)){
				return;
			}
			Carrier carrier;
			Claim claim;
			InsPlan plan;
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				claim=Claims.GetClaim(listQueue[gridMain.SelectedIndices[i]].ClaimNum);
				plan=InsPlans.GetPlan(claim.PlanNum,new InsPlan[] {});
				carrier=Carriers.GetCarrier(plan.CarrierNum);
				LabelSingle label=new LabelSingle();
				if(!label.PrintIns(carrier,pd.PrinterSettings.PrinterName)){
					return;
				}
			}
		}

		private void OnStatus_Click(){
			//this changes the status of claims from P to S.
			if(gridMain.SelectedIndices.Length==0){
				if(MessageBox.Show(Lan.g(this,"Change all 'Probably Sent' claims to 'Sent'?"),""
					,MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;
				}
				for(int i=0;i<listQueue.Length;i++){
					if(listQueue[i].ClaimStatus=="P"){
						Claims.UpdateStatus(listQueue[i].ClaimNum,"S");
					}	
				}
			}
			else{
				if(MessageBox.Show(Lan.g(this,"Change selected claims to 'Sent'?"),""
					,MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;
				}
				for(int i=0;i<gridMain.SelectedIndices.Length;i++){
					Claims.UpdateStatus(listQueue[gridMain.SelectedIndices[i]].ClaimNum,"S");
				}
			}
			FillGrid();	
		}

		private void OnEclaims_Click(){
			ArrayList queueItems=new ArrayList();//a list of queue items to send
			if(gridMain.SelectedIndices.Length==0){
				for(int i=0;i<listQueue.Length;i++){
					if((listQueue[i].ClaimStatus=="W" || listQueue[i].ClaimStatus=="P")
						&& !listQueue[i].NoSendElect
						&& gridMain.Rows[i].Cells[4].Text=="")//no Missing Info
					{
						gridMain.SetSelected(i,true);
					}	
				}
				if(!MsgBox.Show(this,true,"Send all selected e-claims?")){
					return;
				}
			}
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				if(gridMain.Rows[gridMain.SelectedIndices[i]].Cells[4].Text!=""){
					//tbQueue.Cell[4,tbQueue.SelectedIndices[i]]!=""){
					MsgBox.Show(this,"Not allowed to send e-claims with missing information.");
					return;
				}
			}
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				queueItems.Add(listQueue[gridMain.SelectedIndices[i]]);
			}
			Eclaims.Eclaims.SendBatches(queueItems);
			//statuses changed to P in SendBatches
			FillGrid();
		}

		private void OnReports_Click(){
			FormClaimReports FormC=new FormClaimReports();
			FormC.ShowDialog();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


		

		

		

		

		

					
				

	}
}







