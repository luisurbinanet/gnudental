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

namespace OpenDental{

	public class FormClaimsSend : System.Windows.Forms.Form{
		private OpenDental.TableQueue tbQueue;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button butClose;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.PrintDialog printDialog2;
		private System.Windows.Forms.ToolBar toolBar2;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolBarButton toolButPrint;
		private System.Windows.Forms.ToolBarButton toolButPreview;
		private System.Windows.Forms.ToolBarButton toolButBlank;
		private System.Windows.Forms.ToolBarButton toolButDiv1;
		private System.Windows.Forms.ToolBarButton toolButDiv2;
		private System.Windows.Forms.ToolBarButton toolButSubmit;
		private System.Windows.Forms.ToolBarButton toolButStatus;
		private System.Windows.Forms.ContextMenu contextMenuStatus;
		public static ArrayList eClaimList;//final list of eclaims(as Claim.ClaimNum) to send

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
			this.toolBar2 = new System.Windows.Forms.ToolBar();
			this.toolButPreview = new System.Windows.Forms.ToolBarButton();
			this.toolButBlank = new System.Windows.Forms.ToolBarButton();
			this.toolButDiv1 = new System.Windows.Forms.ToolBarButton();
			this.toolButPrint = new System.Windows.Forms.ToolBarButton();
			this.toolButSubmit = new System.Windows.Forms.ToolBarButton();
			this.toolButDiv2 = new System.Windows.Forms.ToolBarButton();
			this.toolButStatus = new System.Windows.Forms.ToolBarButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.contextMenuStatus = new System.Windows.Forms.ContextMenu();
			this.SuspendLayout();
			// 
			// tbQueue
			// 
			this.tbQueue.BackColor = System.Drawing.SystemColors.Window;
			this.tbQueue.Location = new System.Drawing.Point(24, 64);
			this.tbQueue.Name = "tbQueue";
			this.tbQueue.SelectedIndices = new int[0];
			this.tbQueue.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.tbQueue.Size = new System.Drawing.Size(419, 462);
			this.tbQueue.TabIndex = 19;
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(107, -44);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(112, 29);
			this.label6.TabIndex = 21;
			this.label6.Text = "Insurance Claims";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// butClose
			// 
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.butClose.Location = new System.Drawing.Point(406, 550);
			this.butClose.Name = "butClose";
			this.butClose.TabIndex = 22;
			this.butClose.Text = "Close";
			// 
			// toolBar2
			// 
			this.toolBar2.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.toolBar2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.toolBar2.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																																								this.toolButPreview,
																																								this.toolButBlank,
																																								this.toolButDiv1,
																																								this.toolButPrint,
																																								this.toolButSubmit,
																																								this.toolButDiv2,
																																								this.toolButStatus});
			this.toolBar2.Cursor = System.Windows.Forms.Cursors.Default;
			this.toolBar2.DropDownArrows = true;
			this.toolBar2.ImageList = this.imageList1;
			this.toolBar2.Location = new System.Drawing.Point(0, 0);
			this.toolBar2.Name = "toolBar2";
			this.toolBar2.ShowToolTips = true;
			this.toolBar2.Size = new System.Drawing.Size(506, 36);
			this.toolBar2.TabIndex = 23;
			this.toolBar2.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
			this.toolBar2.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar2_ButtonClick);
			// 
			// toolButPreview
			// 
			this.toolButPreview.ImageIndex = 1;
			this.toolButPreview.Tag = "";
			this.toolButPreview.Text = "Preview";
			this.toolButPreview.ToolTipText = "Preview Claim";
			// 
			// toolButBlank
			// 
			this.toolButBlank.ImageIndex = 2;
			this.toolButBlank.Tag = "";
			this.toolButBlank.Text = "Blank";
			this.toolButBlank.ToolTipText = "Print Blank Claim Form";
			// 
			// toolButDiv1
			// 
			this.toolButDiv1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// toolButPrint
			// 
			this.toolButPrint.ImageIndex = 0;
			this.toolButPrint.Tag = "";
			this.toolButPrint.Text = "Print";
			this.toolButPrint.ToolTipText = "Print";
			// 
			// toolButSubmit
			// 
			this.toolButSubmit.ImageIndex = 3;
			this.toolButSubmit.Tag = "";
			this.toolButSubmit.Text = "Submit";
			this.toolButSubmit.ToolTipText = "Submit to WebClaim";
			// 
			// toolButDiv2
			// 
			this.toolButDiv2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// toolButStatus
			// 
			this.toolButStatus.DropDownMenu = this.contextMenuStatus;
			this.toolButStatus.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
			this.toolButStatus.Tag = "";
			this.toolButStatus.Text = "Status Sent";
			this.toolButStatus.ToolTipText = "Change Status of Selected Claims to Sent";
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(22, 22);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// FormClaimsSend
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(506, 594);
			this.Controls.Add(this.toolBar2);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.tbQueue);
			this.Controls.Add(this.label6);
			this.Name = "FormClaimsSend";
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
			contextMenuStatus.MenuItems.Add("Received",new EventHandler(StatusReceived_Clicked));
			FillTable();
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

		private void StatusReceived_Clicked(object sender, System.EventArgs e){
			ChangeSelectedTo("R");
		}

		private void FillTable(){
			Claims.GetQueueList();
			tbQueue.ResetRows(Claims.ListQueue.Length);
			tbQueue.SetGridColor(Color.Gray);
			tbQueue.SetBackGColor(Color.White);   
			for(int i=0;i<Claims.ListQueue.Length;i++){
				tbQueue.Cell[0,i]=Claims.ListQueue[i].PatName;
				tbQueue.Cell[1,i]=Defs.GetName(DefCat.ClaimFormats,Claims.ListQueue[i].ClaimFormat);
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

		private void toolBar2_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e) {
			//this is how hardcoded toolbars will be tested:
			//dynamic toolbars will be a little different.  First would test whether a hard-coded
			//or dynamic button, then which one.  Then run function.
			switch(toolBar2.Buttons.IndexOf(e.Button)){
				case 0:
					toolButPreview_Click();
					break;
				case 1:
					toolButBlank_Click();
					break;
				//2 is divider
				case 3:
					toolButPrint_Click();
					break;
				case 4:
					toolButSubmit_Click();
					break;
				//5 is divider
				case 6:
					toolButStatus_Click();
					break;
			}
		}

		private void toolButPreview_Click(){
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

		private void toolButBlank_Click(){
			FormClaimPrint FormCP;
			FormCP=new FormClaimPrint();
			FormCP.PrintBlank=true;
			if(!FormCP.PrintImmediate()){
				MessageBox.Show(Lan.g(this,"Error printing."));
			}		
		}

		private void toolButPrint_Click(){
			FormClaimPrint FormCP;
			FormCP=new FormClaimPrint();
			printDialog2=new PrintDialog();
			printDialog2.PrinterSettings=new PrinterSettings();
			printDialog2.PrinterSettings.PrinterName=Computers.Cur.PrinterName;
			if(tbQueue.SelectedIndices.Length==0){
				if(MessageBox.Show(Lan.g(this,"No items were selected.  Print all paper claims?"),""
					,MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;
				}
				if(printDialog2.ShowDialog()==DialogResult.OK)
					FormClaimPrint.PrinterName=printDialog2.PrinterSettings.PrinterName;
				else return;
				for(int i=0;i<Claims.ListQueue.Length;i++){
					if(Claims.ListQueue[i].ClaimStatus=="W"
						&& Defs.GetValue(DefCat.ClaimFormats,Claims.ListQueue[i].ClaimFormat)!="eclaim"){
						FormCP.ThisPatNum=Claims.ListQueue[i].PatNum;
						FormCP.ThisClaimNum=Claims.ListQueue[i].ClaimNum;
						if(!FormCP.PrintImmediate()){
							MessageBox.Show(Lan.g(this,"Error printing."));
							return;
						}
						Claims.UpdateStatus(Claims.ListQueue[i].ClaimNum,"P");
					}	
				}
			}
			else{
				if(printDialog2.ShowDialog()==DialogResult.OK)
					FormClaimPrint.PrinterName=printDialog2.PrinterSettings.PrinterName;
				else return;
				for(int i=0;i<tbQueue.SelectedIndices.Length;i++){
					FormCP.ThisPatNum=Claims.ListQueue[tbQueue.SelectedIndices[i]].PatNum;
					FormCP.ThisClaimNum=Claims.ListQueue[tbQueue.SelectedIndices[i]].ClaimNum;
					if(!FormCP.PrintImmediate()){
						MessageBox.Show(Lan.g(this,"Error printing."));
						return;
					}
					Claims.UpdateStatus(Claims.ListQueue[tbQueue.SelectedIndices[i]].ClaimNum,"P");
				}
			}
			FillTable();
		}

		private void toolButSubmit_Click(){
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
				}
			}
			//Then, the next line refreshes this screen.
			FillTable();	
		}

		private void toolButStatus_Click(){
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

					
				

	}
}







