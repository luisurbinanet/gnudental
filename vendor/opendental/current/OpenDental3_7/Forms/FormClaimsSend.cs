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
	public class FormClaimsSend : System.Windows.Forms.Form{
		private OpenDental.TableQueue tbQueue;
		private System.Windows.Forms.Label label6;
		private OpenDental.UI.Button butClose;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ContextMenu contextMenuStatus;
		private OpenDental.UI.ODToolBar ToolBarMain;
		///<summary>final list of eclaims(as Claim.ClaimNum) to send</summary>
		public static ArrayList eClaimList;
		private ClaimSendQueueItem[] listQueue;

		///<summary></summary>
		public FormClaimsSend(){
			InitializeComponent();
			tbQueue.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbQueue_CellDoubleClicked);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormClaimsSend));
			this.tbQueue = new OpenDental.TableQueue();
			this.label6 = new System.Windows.Forms.Label();
			this.butClose = new OpenDental.UI.Button();
			this.contextMenuStatus = new System.Windows.Forms.ContextMenu();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.SuspendLayout();
			// 
			// tbQueue
			// 
			this.tbQueue.BackColor = System.Drawing.SystemColors.Window;
			this.tbQueue.Location = new System.Drawing.Point(14, 44);
			this.tbQueue.Name = "tbQueue";
			this.tbQueue.ScrollValue = 1;
			this.tbQueue.SelectedIndices = new int[0];
			this.tbQueue.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.tbQueue.Size = new System.Drawing.Size(859, 538);
			this.tbQueue.TabIndex = 19;
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(107, -44);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(112, 44);
			this.label6.TabIndex = 21;
			this.label6.Text = "Insurance Claims";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(798, 596);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 25);
			this.butClose.TabIndex = 22;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(22, 22);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageList1;
			this.ToolBarMain.Location = new System.Drawing.Point(0, 0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(890, 29);
			this.ToolBarMain.TabIndex = 31;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// FormClaimsSend
			// 
			this.AcceptButton = this.butClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(890, 638);
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.tbQueue);
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
			contextMenuStatus.MenuItems.Add("Unsent",new EventHandler(StatusUnsent_Clicked));
			contextMenuStatus.MenuItems.Add("Hold until Pri received",new EventHandler(StatusHold_Clicked));
			contextMenuStatus.MenuItems.Add("Waiting in Send List",new EventHandler(StatusWaiting_Clicked));
			contextMenuStatus.MenuItems.Add("Probably sent",new EventHandler(StatusProbably_Clicked));
			contextMenuStatus.MenuItems.Add("Sent - Verified",new EventHandler(StatusSent_Clicked));
			//do not show received because that would mess up the balances
			//contextMenuStatus.MenuItems.Add("Received",new EventHandler(StatusReceived_Clicked));
			LayoutToolBar();
			FillTable();
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
			if(tbQueue.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select items first."));
				return;
			}
			for(int i=0;i<tbQueue.SelectedIndices.Length;i++){
				Claims.UpdateStatus(listQueue[tbQueue.SelectedIndices[i]].ClaimNum,claimStatus);
			}
			FillTable();
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

		//private void StatusReceived_Clicked(object sender, System.EventArgs e){
		//	ChangeSelectedTo("R");
		//}

		private void FillTable(){
			listQueue=Claims.GetQueueList();
			tbQueue.ResetRows(listQueue.Length);
			tbQueue.SetGridColor(Color.Gray);
			tbQueue.SetBackGColor(Color.White);   
			for(int i=0;i<listQueue.Length;i++){
				tbQueue.Cell[0,i]=listQueue[i].PatName;
				tbQueue.Cell[1,i]=listQueue[i].Carrier;
				if(listQueue[i].NoSendElect){
					tbQueue.Cell[2,i]="Paper";
				}
				else{
					tbQueue.Cell[2,i]=Clearinghouses.GetDescript(listQueue[i].ClearinghouseNum);
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
				tbQueue.Cell[3,i]=Lan.g(this,status);
				if(!listQueue[i].NoSendElect){
					tbQueue.Cell[4,i]=Eclaims.Eclaims.GetMissingData(listQueue[i]);
				}
			}
			tbQueue.LayoutTables(); 
		}

		private void tbQueue_CellDoubleClicked(object sender, CellEventArgs e){
			FormClaimPrint FormCP;
			FormCP=new FormClaimPrint();
			FormCP.ThisPatNum=listQueue[e.Row].PatNum;
			FormCP.ThisClaimNum=listQueue[e.Row].ClaimNum;
			FormCP.PrintImmediately=false;
			FormCP.ShowDialog();				
			FillTable();	
			/*Claims.CurQueue=Claims.ListQueue[e.Row];
			Patients.Cur=new Patient();
			Patients.Cur.PatNum=Claims.CurQueue.PatNum;
			Procedures.Refresh();
			Claims.GetProcsInClaim(Claims.CurQueue.ClaimNum);
			CovPats.Refresh();
			FormClaimView FormCV=new FormClaimView();
			FormCV.ShowDialog();
			Patients.Cur=new Patient();
			FillTable();*/
		}

		private void ToolBarMain_ButtonClick(object sender, OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			//if(e.Button.Tag.GetType()==typeof(string)){
				//standard predefined button
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
			/*}
			else if(e.Button.Tag.GetType()==typeof(int)){
				int programNum=(int)e.Button.Tag;
				Programs.Cur.ProgramNum=0;
				for(int i=0;i<Programs.List.Length;i++){
					if(Programs.List[i].ProgramNum==programNum){
						Programs.Cur=Programs.List[i];
					}
				}
				if(Programs.Cur.ProgramNum==0){//no match was found
					MessageBox.Show("Error, program entry not found in database.");
					return;
				}
				if(Programs.Cur.ProgName=="WebClaim"){
					OnWebClaim_Click();
					return;
				}
				if(Programs.Cur.ProgName=="Renaissance"){
					OnRenaissance_Click();
					return;
				}
				Programs.Execute((int)e.Button.Tag);
			}*/
		}

		private void OnPreview_Click(){
			FormClaimPrint FormCP;
			FormCP=new FormClaimPrint();
			if(tbQueue.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			if(tbQueue.SelectedIndices.Length > 1){
				MessageBox.Show(Lan.g(this,"Please select only one claim."));
				return;
			}
			FormCP.ThisPatNum=listQueue[tbQueue.SelectedRow].PatNum;
			FormCP.ThisClaimNum=listQueue[tbQueue.SelectedRow].ClaimNum;
			FormCP.PrintImmediately=false;
			FormCP.ShowDialog();				
			FillTable();	
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
			if(tbQueue.SelectedIndices.Length==0){
				for(int i=0;i<listQueue.Length;i++){
					if((listQueue[i].ClaimStatus=="W" || listQueue[i].ClaimStatus=="P")
						&& listQueue[i].NoSendElect)
					{
						tbQueue.SetSelected(i,true);		
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
			for(int i=0;i<tbQueue.SelectedIndices.Length;i++){
				FormCP.ThisPatNum=listQueue[tbQueue.SelectedIndices[i]].PatNum;
				FormCP.ThisClaimNum=listQueue[tbQueue.SelectedIndices[i]].ClaimNum;
				if(!FormCP.PrintImmediate(pd.PrinterSettings.PrinterName,1)){
					return;
				}
				Claims.UpdateStatus(listQueue[tbQueue.SelectedIndices[i]].ClaimNum,"P");
			}
			FillTable();
		}

		private void OnLabels_Click(){
			if(tbQueue.SelectedIndices.Length==0){
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
			for(int i=0;i<tbQueue.SelectedIndices.Length;i++){
				claim=Claims.GetClaim(listQueue[tbQueue.SelectedIndices[i]].ClaimNum);
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
			if(tbQueue.SelectedIndices.Length==0){
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
				for(int i=0;i<tbQueue.SelectedIndices.Length;i++){
					Claims.UpdateStatus(listQueue[tbQueue.SelectedIndices[i]].ClaimNum,"S");
				}
			}
			FillTable();	
		}

		private void OnEclaims_Click(){
			ArrayList queueItems=new ArrayList();//a list of queue items to send
			if(tbQueue.SelectedIndices.Length==0){
				for(int i=0;i<listQueue.Length;i++){
					if((listQueue[i].ClaimStatus=="W" || listQueue[i].ClaimStatus=="P")
						&& !listQueue[i].NoSendElect
						&& tbQueue.Cell[4,i]=="")//no Missing Info
					{
						tbQueue.SetSelected(i,true);
					}	
				}
				if(!MsgBox.Show(this,true,"Send all selected e-claims?")){
					return;
				}
			}
			for(int i=0;i<tbQueue.SelectedIndices.Length;i++){
				if(tbQueue.Cell[4,tbQueue.SelectedIndices[i]]!=""){
					MsgBox.Show(this,"Not allowed to send e-claims with missing information.");
					return;
				}
			}
			for(int i=0;i<tbQueue.SelectedIndices.Length;i++){
				queueItems.Add(listQueue[tbQueue.SelectedIndices[i]]);
			}
			Eclaims.Eclaims.SendBatches(queueItems);
			//statuses changed to P in SendBatches
			FillTable();
		}

		private void OnReports_Click(){
			FormClaimReports FormC=new FormClaimReports();
			FormC.ShowDialog();
		}

		/*private void OnGeneric_Click(){//will add this back in later
			string printerName;
			FormClaimPrint FormCP=new FormClaimPrint();
			FormCP.HideBackground=true;
			FormCP.ClaimFormCur=ClaimForms.GetClaimForm(Prefs.GetInt("GenericEClaimsForm"));
			printDialog2=new PrintDialog();
			printDialog2.PrinterSettings=new PrinterSettings();
			//printDialog2.PrinterSettings.PrinterName=Computers.Cur.PrinterName;
			if(tbQueue.SelectedIndices.Length==0){
				if(MessageBox.Show(Lan.g(this,"No items were selected.  Send all e-claims?"),""
					,MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;
				}
				if(printDialog2.ShowDialog()==DialogResult.OK)
					printerName=printDialog2.PrinterSettings.PrinterName;
				else return;
				for(int i=0;i<Claims.ListQueue.Length;i++){
					if((Claims.ListQueue[i].ClaimStatus=="W" || Claims.ListQueue[i].ClaimStatus=="P")
						&& !Claims.ListQueue[i].NoSendElect)
					{
						FormCP.ThisPatNum=Claims.ListQueue[i].PatNum;
						FormCP.ThisClaimNum=Claims.ListQueue[i].ClaimNum;
						if(!FormCP.PrintImmediate(printerName)){
							MessageBox.Show(Lan.g(this,"Error printing."));
							return;
						}
						Claims.UpdateStatus(Claims.ListQueue[i].ClaimNum,"P");
					}	
				}
			}
			else{
				if(printDialog2.ShowDialog()==DialogResult.OK)
					printerName=printDialog2.PrinterSettings.PrinterName;
				else return;
				for(int i=0;i<tbQueue.SelectedIndices.Length;i++){
					FormCP.ThisPatNum=Claims.ListQueue[tbQueue.SelectedIndices[i]].PatNum;
					FormCP.ThisClaimNum=Claims.ListQueue[tbQueue.SelectedIndices[i]].ClaimNum;
					if(!FormCP.PrintImmediate(printerName)){
						MessageBox.Show(Lan.g(this,"Error printing."));
						return;
					}
					Claims.UpdateStatus(Claims.ListQueue[tbQueue.SelectedIndices[i]].ClaimNum,"P");
				}
			}
			FillTable();
		}*/

		/*
		private void OnRenaissance_Click(){
			//get last batch number
			int batchNum=PIn.PInt(((Pref)Prefs.HList["RenaissanceLastBatchNumber"]).ValueString);
			//and increment it by one
			if(batchNum==999)
				batchNum=1;
			else
				batchNum++;
			//save the new batch number. Even if user cancels, it will have incremented.
			Prefs.Cur.PrefName="RenaissanceLastBatchNumber";
			Prefs.Cur.ValueString=batchNum.ToString();
			Prefs.UpdateCur();
			Prefs.Refresh();
			//no need to send refresh signal to other workstations
			//since eclaims are always sent from the same computer.
			if(tbQueue.SelectedIndices.Length==0){
				if(MessageBox.Show(Lan.g(this,"No items were selected.  Send all e-claims?"),""
					,MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;
				}
				for(int i=0;i<listQueue.Length;i++){
					if((listQueue[i].ClaimStatus=="W" || listQueue[i].ClaimStatus=="P")
						&& !listQueue[i].NoSendElect)
					{
						if(!Renaissance.CreateClaim
							(listQueue[i].PatNum,listQueue[i].ClaimNum)){
							MessageBox.Show(Lan.g(this,"Error creating claim."));
							return;
						}
						Claims.UpdateStatus(listQueue[i].ClaimNum,"P");
					}	
				}
			}
			else{
				for(int i=0;i<tbQueue.SelectedIndices.Length;i++){
					if(!Renaissance.CreateClaim
							(listQueue[tbQueue.SelectedIndices[i]].PatNum
							,listQueue[tbQueue.SelectedIndices[i]].ClaimNum))
					{
						MessageBox.Show(Lan.g(this,"Error creating claim."));
						return;
					}
					Claims.UpdateStatus(listQueue[tbQueue.SelectedIndices[i]].ClaimNum,"P");
				}
			}
			try{
				Process.Start(@"C:\Program Files\Renaissance\lite\RemoteLite.exe");
			}
			catch{
				MessageBox.Show("Could not launch RemoteLite. Your claims have not been sent.");
			}
			FillTable();
		}*/

		/*
		private void OnWebClaim_Click(){
			RegistryKey rk=Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WebClaim\Integration\FreeDental");
			if(rk==null){
				MessageBox.Show(Lan.g(this,"You do not have WebClaims installed. Key not present."));
			}
			else{
				string keyVal=(string)rk.GetValue("InstallDir");
				if(keyVal==null){
					MessageBox.Show(Lan.g(this,"You do not have WebClaims installed. Key value not present."));
				}
				else if(!File.Exists(keyVal+"WebClaim.exe")){
					MessageBox.Show(Lan.g(this,keyVal+"WebClaim.exe does not exist"));
				}
				else{
					Process myProcess = Process.Start(keyVal+"WebClaim.exe");
					//This is where the eclaims should be sent.  It is assumed that the status will be changed
					//to "P" or "S" within the eclaims module.
					myProcess.WaitForExit();
				}
			}
			//Then, the next line refreshes this screen.
			FillTable();	
		}*/

		private void butClose_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

					
				

	}
}







