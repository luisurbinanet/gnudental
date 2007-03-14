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
		private System.Windows.Forms.Button butClose;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.PrintDialog printDialog2;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ContextMenu contextMenuStatus;
		private OpenDental.UI.ODToolBar ToolBarMain;
		///<summary>final list of eclaims(as Claim.ClaimNum) to send</summary>
		public static ArrayList eClaimList;

		///<summary></summary>
		public FormClaimsSend(){
			InitializeComponent();
			tbQueue.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbQueue_CellDoubleClicked);
			Lan.C(this, new System.Windows.Forms.Control[] {
				label6,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butClose,
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormClaimsSend));
			this.tbQueue = new OpenDental.TableQueue();
			this.label6 = new System.Windows.Forms.Label();
			this.butClose = new System.Windows.Forms.Button();
			this.printDialog2 = new System.Windows.Forms.PrintDialog();
			this.contextMenuStatus = new System.Windows.Forms.ContextMenu();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.SuspendLayout();
			// 
			// tbQueue
			// 
			this.tbQueue.BackColor = System.Drawing.SystemColors.Window;
			this.tbQueue.Location = new System.Drawing.Point(28, 56);
			this.tbQueue.Name = "tbQueue";
			this.tbQueue.ScrollValue = 1;
			this.tbQueue.SelectedIndices = new int[0];
			this.tbQueue.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.tbQueue.Size = new System.Drawing.Size(489, 516);
			this.tbQueue.TabIndex = 19;
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(107, -44);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(112, 39);
			this.label6.TabIndex = 21;
			this.label6.Text = "Insurance Claims";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// butClose
			// 
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(538, 548);
			this.butClose.Name = "butClose";
			this.butClose.TabIndex = 22;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// imageList1
			// 
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
			this.ToolBarMain.Size = new System.Drawing.Size(632, 29);
			this.ToolBarMain.TabIndex = 31;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// FormClaimsSend
			// 
			this.AcceptButton = this.butClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(632, 594);
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
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Generic"),-1,Lan.g(this,"Send Generic E-Claims Using Print Capture"),"Generic"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ODToolBarButton button=new ODToolBarButton(Lan.g(this,"Status Sent"),3,Lan.g(this,"Changes Status of Selected Claims to Sent"),"Status");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=contextMenuStatus;
			ToolBarMain.Buttons.Add(button);
			ArrayList toolButItems=ToolButItems.GetForToolBar(ToolBarsAvail.ClaimsSend);
			for(int i=0;i<toolButItems.Count;i++){
				ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
				ToolBarMain.Buttons.Add(new ODToolBarButton(((ToolButItem)toolButItems[i]).ButtonText
					,-1,"",((ToolButItem)toolButItems[i]).ProgramNum));
			}
			ToolBarMain.Invalidate();
		}

		private void ChangeSelectedTo(string claimStatus){
			if(tbQueue.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select items first."));
				return;
			}
			for(int i=0;i<tbQueue.SelectedIndices.Length;i++){
				Claims.UpdateStatus(Claims.ListQueue[tbQueue.SelectedIndices[i]].ClaimNum,claimStatus);
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
			Claims.GetQueueList();
			tbQueue.ResetRows(Claims.ListQueue.Length);
			tbQueue.SetGridColor(Color.Gray);
			tbQueue.SetBackGColor(Color.White);   
			for(int i=0;i<Claims.ListQueue.Length;i++){
				tbQueue.Cell[0,i]=Claims.ListQueue[i].PatName;
				if(Claims.ListQueue[i].NoSendElect)
					tbQueue.Cell[1,i]="X";
				tbQueue.Cell[2,i]=Claims.ListQueue[i].Carrier;
				string status="";
				switch(Claims.ListQueue[i].ClaimStatus){
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
			}
			tbQueue.LayoutTables(); 
		}

		private void tbQueue_CellDoubleClicked(object sender, CellEventArgs e){
			FormClaimPrint FormCP;
			FormCP=new FormClaimPrint();
			FormCP.ThisPatNum=Claims.ListQueue[e.Row].PatNum;
			FormCP.ThisClaimNum=Claims.ListQueue[e.Row].ClaimNum;
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
			if(e.Button.Tag.GetType()==typeof(string)){
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
					case "Generic":
						OnGeneric_Click();
						break;
					case "Status":
						OnStatus_Click();
						break;
				}
			}
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
			}
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
			FormCP.ThisPatNum=Claims.ListQueue[tbQueue.SelectedRow].PatNum;
			FormCP.ThisClaimNum=Claims.ListQueue[tbQueue.SelectedRow].ClaimNum;
			FormCP.PrintImmediately=false;
			FormCP.ShowDialog();				
			FillTable();	
		}

		private void OnBlank_Click(){
			string printerName="";
			FormClaimPrint FormCP=new FormClaimPrint();
			FormCP.PrintBlank=true;
			if(!FormCP.PrintImmediate(printerName)){
				MessageBox.Show(Lan.g(this,"Error printing."));
			}		
		}

		private void OnPrint_Click(){
			string printerName;
			FormClaimPrint FormCP=new FormClaimPrint();
			printDialog2=new PrintDialog();
			printDialog2.PrinterSettings=new PrinterSettings();
			printDialog2.PrinterSettings.PrinterName=Computers.Cur.PrinterName;
			if(tbQueue.SelectedIndices.Length==0){
				if(MessageBox.Show(Lan.g(this,"No items were selected.  Print all paper claims?"),""
					,MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;
				}
				if(printDialog2.ShowDialog()==DialogResult.OK)
					printerName=printDialog2.PrinterSettings.PrinterName;
				else return;
				for(int i=0;i<Claims.ListQueue.Length;i++){
					if(Claims.ListQueue[i].ClaimStatus=="W"
						&& Claims.ListQueue[i].NoSendElect){
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
		}

		private void OnGeneric_Click(){
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
		}

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
				for(int i=0;i<Claims.ListQueue.Length;i++){
					if((Claims.ListQueue[i].ClaimStatus=="W" || Claims.ListQueue[i].ClaimStatus=="P")
						&& !Claims.ListQueue[i].NoSendElect)
					{
						if(!Bridges.Renaissance.CreateClaim
							(Claims.ListQueue[i].PatNum,Claims.ListQueue[i].ClaimNum)){
							MessageBox.Show(Lan.g(this,"Error creating claim."));
							return;
						}
						Claims.UpdateStatus(Claims.ListQueue[i].ClaimNum,"P");
					}	
				}
			}
			else{
				for(int i=0;i<tbQueue.SelectedIndices.Length;i++){
					if(!Bridges.Renaissance.CreateClaim
							(Claims.ListQueue[tbQueue.SelectedIndices[i]].PatNum
							,Claims.ListQueue[tbQueue.SelectedIndices[i]].ClaimNum))
					{
						MessageBox.Show(Lan.g(this,"Error creating claim."));
						return;
					}
					Claims.UpdateStatus(Claims.ListQueue[tbQueue.SelectedIndices[i]].ClaimNum,"P");
				}
			}
			try{
				Process.Start(@"C:\Program Files\Renaissance\lite\RemoteLite.exe");
			}
			catch{
				MessageBox.Show("Could not launch RemoteLite. Your claims have not been sent.");
			}
			FillTable();
		}

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
		}

		

		private void OnStatus_Click(){
			//this changes the status of claims from P to S.
			if(tbQueue.SelectedIndices.Length==0){
				if(MessageBox.Show(Lan.g(this,"Change all 'Probably Sent' claims to 'Sent'?"),""
					,MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;
				}
				for(int i=0;i<Claims.ListQueue.Length;i++){
					if(Claims.ListQueue[i].ClaimStatus=="P"){
						Claims.UpdateStatus(Claims.ListQueue[i].ClaimNum,"S");
					}	
				}
			}
			else{
				if(MessageBox.Show(Lan.g(this,"Change selected claims to 'Sent'?"),""
					,MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;
				}
				for(int i=0;i<tbQueue.SelectedIndices.Length;i++){
					Claims.UpdateStatus(Claims.ListQueue[tbQueue.SelectedIndices[i]].ClaimNum,"S");
				}
			}
			FillTable();	
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

					
				

	}
}







