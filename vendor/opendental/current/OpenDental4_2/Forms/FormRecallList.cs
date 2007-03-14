/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormRecallList : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butRefresh;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private OpenDental.UI.Button butSave;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label3;
		private OpenDental.TableRecallList tbMain;
		private ArrayList MainAL;
		private OpenDental.ValidNumber textDaysPast;
		private OpenDental.ValidNumber textDaysFuture;
		///<summary>Will be set to true when form closes if user click Send to Pinboard.</summary>
		public bool PinClicked=false;
		private OpenDental.UI.Button butReport;
		private System.Windows.Forms.PrintDialog printDialog2;
		private int pagesPrinted;
		private DataTable AddrTable;
		private int patientsPrinted;
		private OpenDental.UI.PrintPreview printPreview;
		private System.Windows.Forms.GroupBox groupBox3;
		private OpenDental.UI.Button butSetStatus;
		private System.Windows.Forms.ComboBox comboStatus;
		private OpenDental.UI.Button butLabels;
		private OpenDental.UI.Button butPostcards;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.TextBox textPostcardMessage;
		private System.Windows.Forms.Label label4;
		private PrintDocument pd;
		///<summary>When this form closes, this will be the patNum of the last patient viewed.  The calling form should then make use of this to refresh to that patient.  If 0, then calling form should not refresh.</summary>
		public int SelectedPatNum;

		///<summary></summary>
		public FormRecallList(){
			InitializeComponent();// Required for Windows Form Designer support
			tbMain.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbMain_CellClicked);
			tbMain.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbMain_CellDoubleClicked);
			Lan.F(this);
			//Lan.C(this,new Control[]
			//	{
			//		textBox1
			//	});
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

		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormRecallList));
			this.butClose = new OpenDental.UI.Button();
			this.tbMain = new OpenDental.TableRecallList();
			this.butRefresh = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textDaysFuture = new OpenDental.ValidNumber();
			this.textDaysPast = new OpenDental.ValidNumber();
			this.label3 = new System.Windows.Forms.Label();
			this.butSave = new OpenDental.UI.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.butReport = new OpenDental.UI.Button();
			this.butLabels = new OpenDental.UI.Button();
			this.printDialog2 = new System.Windows.Forms.PrintDialog();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.comboStatus = new System.Windows.Forms.ComboBox();
			this.butSetStatus = new OpenDental.UI.Button();
			this.butPostcards = new OpenDental.UI.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textPostcardMessage = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(873, 645);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 26);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// tbMain
			// 
			this.tbMain.BackColor = System.Drawing.SystemColors.Window;
			this.tbMain.Location = new System.Drawing.Point(9, 18);
			this.tbMain.Name = "tbMain";
			this.tbMain.ScrollValue = 1;
			this.tbMain.SelectedIndices = new int[0];
			this.tbMain.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.tbMain.Size = new System.Drawing.Size(754, 657);
			this.tbMain.TabIndex = 0;
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.Location = new System.Drawing.Point(79, 107);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(98, 26);
			this.butRefresh.TabIndex = 2;
			this.butRefresh.Text = "&Refresh List";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.textDaysFuture);
			this.groupBox1.Controls.Add(this.textDaysPast);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.butSave);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.butRefresh);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(771, 14);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(188, 180);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "View";
			// 
			// textDaysFuture
			// 
			this.textDaysFuture.Location = new System.Drawing.Point(124, 58);
			this.textDaysFuture.MaxVal = 255;
			this.textDaysFuture.MinVal = 0;
			this.textDaysFuture.Name = "textDaysFuture";
			this.textDaysFuture.Size = new System.Drawing.Size(51, 20);
			this.textDaysFuture.TabIndex = 1;
			this.textDaysFuture.Text = "";
			// 
			// textDaysPast
			// 
			this.textDaysPast.Location = new System.Drawing.Point(124, 31);
			this.textDaysPast.MaxVal = 255;
			this.textDaysPast.MinVal = 0;
			this.textDaysPast.Name = "textDaysPast";
			this.textDaysPast.Size = new System.Drawing.Size(51, 20);
			this.textDaysPast.TabIndex = 0;
			this.textDaysPast.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(13, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(162, 17);
			this.label3.TabIndex = 16;
			this.label3.Text = "(leave days blank to view all)";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butSave
			// 
			this.butSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butSave.Autosize = true;
			this.butSave.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSave.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSave.Location = new System.Drawing.Point(79, 141);
			this.butSave.Name = "butSave";
			this.butSave.Size = new System.Drawing.Size(98, 26);
			this.butSave.TabIndex = 3;
			this.butSave.Text = "&Save As Default";
			this.butSave.Click += new System.EventHandler(this.butSave_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(31, 61);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(91, 14);
			this.label2.TabIndex = 12;
			this.label2.Text = "Days Future";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(37, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 14);
			this.label1.TabIndex = 11;
			this.label1.Text = "Days Past";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butReport
			// 
			this.butReport.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butReport.Autosize = true;
			this.butReport.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butReport.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butReport.Location = new System.Drawing.Point(861, 571);
			this.butReport.Name = "butReport";
			this.butReport.Size = new System.Drawing.Size(87, 26);
			this.butReport.TabIndex = 13;
			this.butReport.Text = "R&un Report";
			this.butReport.Click += new System.EventHandler(this.butReport_Click);
			// 
			// butLabels
			// 
			this.butLabels.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butLabels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butLabels.Autosize = true;
			this.butLabels.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLabels.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLabels.Image = ((System.Drawing.Image)(resources.GetObject("butLabels.Image")));
			this.butLabels.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butLabels.Location = new System.Drawing.Point(861, 535);
			this.butLabels.Name = "butLabels";
			this.butLabels.Size = new System.Drawing.Size(87, 26);
			this.butLabels.TabIndex = 14;
			this.butLabels.Text = "Labels";
			this.butLabels.Click += new System.EventHandler(this.butLabels_Click);
			// 
			// printDialog2
			// 
			this.printDialog2.AllowPrintToFile = false;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.comboStatus);
			this.groupBox3.Controls.Add(this.butSetStatus);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(771, 203);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(188, 96);
			this.groupBox3.TabIndex = 15;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Set Status";
			// 
			// comboStatus
			// 
			this.comboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatus.Location = new System.Drawing.Point(17, 24);
			this.comboStatus.MaxDropDownItems = 40;
			this.comboStatus.Name = "comboStatus";
			this.comboStatus.Size = new System.Drawing.Size(160, 21);
			this.comboStatus.TabIndex = 15;
			// 
			// butSetStatus
			// 
			this.butSetStatus.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSetStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butSetStatus.Autosize = true;
			this.butSetStatus.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSetStatus.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSetStatus.Location = new System.Drawing.Point(110, 56);
			this.butSetStatus.Name = "butSetStatus";
			this.butSetStatus.Size = new System.Drawing.Size(67, 26);
			this.butSetStatus.TabIndex = 14;
			this.butSetStatus.Text = "Set";
			this.butSetStatus.Click += new System.EventHandler(this.butSetStatus_Click);
			// 
			// butPostcards
			// 
			this.butPostcards.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPostcards.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPostcards.Autosize = true;
			this.butPostcards.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPostcards.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPostcards.Image = ((System.Drawing.Image)(resources.GetObject("butPostcards.Image")));
			this.butPostcards.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPostcards.Location = new System.Drawing.Point(90, 162);
			this.butPostcards.Name = "butPostcards";
			this.butPostcards.Size = new System.Drawing.Size(87, 26);
			this.butPostcards.TabIndex = 16;
			this.butPostcards.Text = "Preview";
			this.butPostcards.Click += new System.EventHandler(this.butPostcards_Click);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.label4);
			this.groupBox4.Controls.Add(this.textPostcardMessage);
			this.groupBox4.Controls.Add(this.butPostcards);
			this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox4.Location = new System.Drawing.Point(771, 310);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(188, 200);
			this.groupBox4.TabIndex = 17;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Postcards";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(7, 11);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(158, 19);
			this.label4.TabIndex = 18;
			this.label4.Text = "Message";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textPostcardMessage
			// 
			this.textPostcardMessage.AcceptsReturn = true;
			this.textPostcardMessage.Location = new System.Drawing.Point(8, 32);
			this.textPostcardMessage.Multiline = true;
			this.textPostcardMessage.Name = "textPostcardMessage";
			this.textPostcardMessage.Size = new System.Drawing.Size(168, 119);
			this.textPostcardMessage.TabIndex = 17;
			this.textPostcardMessage.Text = "";
			// 
			// FormRecallList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(975, 691);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.butLabels);
			this.Controls.Add(this.butReport);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.tbMain);
			this.Controls.Add(this.butClose);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRecallList";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Recall List";
			this.Load += new System.EventHandler(this.FormRecallList_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRecallList_Load(object sender, System.EventArgs e) {
			textDaysPast.Text=((Pref)Prefs.HList["RecallDaysPast"]).ValueString;
			textDaysFuture.Text=((Pref)Prefs.HList["RecallDaysFuture"]).ValueString;
			if(textDaysPast.Text=="-1")
				textDaysPast.Text="";
			if(textDaysFuture.Text=="-1")
				textDaysFuture.Text="";
			textDaysPast.MaxVal=100000;
			textDaysFuture.MaxVal=100000;
			textPostcardMessage.Text=Prefs.GetString("RecallPostcardMessage");
			comboStatus.Items.Clear();
			for(int i=0;i<Defs.Short[(int)DefCat.RecallUnschedStatus].Length;i++){
				comboStatus.Items.Add(Defs.Short[(int)DefCat.RecallUnschedStatus][i].ItemName);
			}
			FillMain();
		}

		private void FillMain(){
			MainAL=new ArrayList();
			int daysFuture;
			int daysPast;
			try{
				if(textDaysFuture.Text=="") daysFuture=-1;
				else daysFuture=Convert.ToInt32(textDaysFuture.Text);
				if(textDaysPast.Text=="") daysPast=-1;
				else daysPast=Convert.ToInt32(textDaysPast.Text);
			}
			catch{
				return;	//this might happen if invalid number
			}
			DateTime fromDate;
			DateTime toDate;
			if(daysPast==-1){
				fromDate=DateTime.MinValue.AddDays(1);//because we don't want to include 010101
			}
			else{
				fromDate=DateTime.Today.AddDays(-daysPast);
			}
			if(daysFuture==-1){
				toDate=DateTime.MaxValue;
			}
			else{
				toDate=DateTime.Today.AddDays(daysFuture);
			}
			RecallItem[] RecallList=Recalls.GetRecallList(fromDate,toDate);
			//initial list also includes patients who already have appointments
			//MessageBox.Show(Patients.RecallList.Length.ToString());
			for(int i=0;i<RecallList.Length;i++){
				//test for existing appointment
				if(!Appointments.PatientHasFutureRecall(RecallList[i].PatNum)){
					MainAL.Add(RecallList[i]);
				}
			}
			//Fill list:
			tbMain.SelectedRow=-1;
			tbMain.ResetRows(MainAL.Count);
			tbMain.SetGridColor(Color.DarkGray);
			for (int i=0;i<MainAL.Count;i++){
				tbMain.Cell[0,i]=((RecallItem)MainAL[i]).DueDate.ToString("d");
				tbMain.Cell[1,i]=((RecallItem)MainAL[i]).PatientName;
				tbMain.Cell[2,i]=Shared.AgeToString(((RecallItem)MainAL[i]).Age);
				tbMain.Cell[3,i]=((RecallItem)MainAL[i]).RecallInterval.ToString();;
				tbMain.Cell[4,i]=Defs.GetName(DefCat.RecallUnschedStatus,((RecallItem)MainAL[i]).RecallStatus);
				tbMain.Cell[5,i]=((RecallItem)MainAL[i]).Note;
			}
			tbMain.LayoutTables();
		}

		private void tbMain_CellClicked(object sender, CellEventArgs e){
			SelectedPatNum=((RecallItem)MainAL[e.Row]).PatNum;
		}

		private void tbMain_CellDoubleClicked(object sender, CellEventArgs e){
			Recall[] recalls=Recalls.GetList(new int[] {((RecallItem)MainAL[e.Row]).PatNum});
			FormRecallListEdit FormRE=new FormRecallListEdit(recalls[0]);
			//FormRE.RecallCur=recalls[0];
			FormRE.ShowDialog();
			if(FormRE.PinClicked){
				PinClicked=true;
				DialogResult=DialogResult.OK;
			}
			else{
				FillMain();
			}
		}

		private void butReport_Click(object sender, System.EventArgs e) {
		  if(MainAL.Count < 1){
        MessageBox.Show(Lan.g(this,"There are no Patients in the Recall table.  Must have at least one to run report."));    
        return;
      }
      int[] PatNums;
      if(tbMain.SelectedIndices.Length < 1){
        PatNums=new int[MainAL.Count];
        for(int i=0;i<PatNums.Length;i++){
          PatNums[i]=((RecallItem)MainAL[i]).PatNum;
        }
      }
      else{
        PatNums=new int[tbMain.SelectedIndices.Length];
        for(int i=0;i<PatNums.Length;i++){
          PatNums[i]=((RecallItem)MainAL[tbMain.SelectedIndices[i]]).PatNum;
        }
      }
      FormRpRecall FormRPR=new FormRpRecall(PatNums);
      FormRPR.ShowDialog();      
		}

		private void butLabels_Click(object sender, System.EventArgs e) {
			if(MainAL.Count < 1){
        MessageBox.Show(Lan.g(this,"There are no Patients in the Recall table.  Must have at least one to print."));    
        return;
      }
			if(tbMain.SelectedIndices.Length==0){
				for(int i=0;i<MainAL.Count;i++){
					tbMain.SetSelected(i,true);
				}
			}
      int[] PatNums;
      PatNums=new int[tbMain.SelectedIndices.Length];
      for(int i=0;i<PatNums.Length;i++){
        PatNums[i]=((RecallItem)MainAL[tbMain.SelectedIndices[i]]).PatNum;
      }
			AddrTable=Recalls.GetAddrTable(PatNums);
			pagesPrinted=0;
			patientsPrinted=0;
			pd=new PrintDocument();
			pd.PrintPage+=new PrintPageEventHandler(this.pdLabels_PrintPage);
			pd.OriginAtMargins=true;
			pd.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			printPreview=new OpenDental.UI.PrintPreview(PrintSituation.LabelSheet
				,pd,(int)Math.Ceiling((double)AddrTable.Rows.Count/30));
			//printPreview.Document=pd;
			//printPreview.TotalPages=;
			printPreview.ShowDialog();
		}

		private void butPostcards_Click(object sender, System.EventArgs e) {
			if(MainAL.Count < 1){
        MessageBox.Show(Lan.g(this,"There are no Patients in the Recall table.  Must have at least one to print."));    
        return;
      }
			if(tbMain.SelectedIndices.Length==0){
				for(int i=0;i<MainAL.Count;i++){
					tbMain.SetSelected(i,true);
				}
			}
      int[] PatNums;
      PatNums=new int[tbMain.SelectedIndices.Length];
      for(int i=0;i<PatNums.Length;i++){
        PatNums[i]=((RecallItem)MainAL[tbMain.SelectedIndices[i]]).PatNum;
      }
			AddrTable=Recalls.GetAddrTable(PatNums);
			pagesPrinted=0;
			patientsPrinted=0;
			pd=new PrintDocument();
			pd.PrintPage+=new PrintPageEventHandler(this.pdCards_PrintPage);
			pd.OriginAtMargins=true;
			pd.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			if(Prefs.GetInt("RecallPostcardsPerSheet")==1){
				pd.DefaultPageSettings.PaperSize=new PaperSize("Postcard",400,600);
				pd.DefaultPageSettings.Landscape=true;
			}
			else if(Prefs.GetInt("RecallPostcardsPerSheet")==3){
				pd.DefaultPageSettings.PaperSize=new PaperSize("Postcard",850,1100);
			}
			else{//4
				pd.DefaultPageSettings.PaperSize=new PaperSize("Postcard",850,1100);
				pd.DefaultPageSettings.Landscape=true;
			}
			printPreview=new OpenDental.UI.PrintPreview(PrintSituation.Postcard,pd,
				(int)Math.Ceiling((double)AddrTable.Rows.Count/(double)Prefs.GetInt("RecallPostcardsPerSheet")));
			printPreview.ShowDialog();
		}

		///<summary>raised for each page to be printed.</summary>
		private void pdLabels_PrintPage(object sender, PrintPageEventArgs ev){
			int totalPages=(int)Math.Ceiling((double)AddrTable.Rows.Count/30);
			Graphics g=ev.Graphics;
			float yPos=75;
			float xPos=50;
			string text="";
			while(yPos<1000 && patientsPrinted<AddrTable.Rows.Count){
				text=AddrTable.Rows[patientsPrinted]["FName"].ToString()+" "
					+AddrTable.Rows[patientsPrinted]["MiddleI"].ToString()+" "
					+AddrTable.Rows[patientsPrinted]["LName"].ToString()+"\r\n"
					+AddrTable.Rows[patientsPrinted]["Address"].ToString()+"\r\n";
				if(AddrTable.Rows[patientsPrinted]["Address2"].ToString()!=""){
					text+=AddrTable.Rows[patientsPrinted]["Address2"].ToString()+"\r\n";
				}
				text+=AddrTable.Rows[patientsPrinted]["City"].ToString()+", "
					+AddrTable.Rows[patientsPrinted]["State"].ToString()+"   "
					+AddrTable.Rows[patientsPrinted]["Zip"].ToString()+"\r\n";
				g.DrawString(text,new Font(FontFamily.GenericSansSerif,11),Brushes.Black,xPos,yPos);
				//reposition for next label
				xPos+=275;
				if(xPos>850){//drop a line
					xPos=50;
					yPos+=100;
				}
				patientsPrinted++;
			}
			pagesPrinted++;
			if(pagesPrinted==totalPages){
				ev.HasMorePages=false;
				pagesPrinted=0;//because it has to print again from the print preview
				patientsPrinted=0;
			}
			else{
				ev.HasMorePages=true;
			}
		}

		///<summary>raised for each page to be printed.</summary>
		private void pdCards_PrintPage(object sender, PrintPageEventArgs ev){
			int totalPages=(int)Math.Ceiling((double)AddrTable.Rows.Count/(double)Prefs.GetInt("RecallPostcardsPerSheet"));
			Graphics g=ev.Graphics;
			float yPos=0;//these refer to the upper left origin of each postcard
			float xPos=0;
			string str;
			while(yPos<ev.PageBounds.Height-100 && patientsPrinted<AddrTable.Rows.Count){
				//Return Address--------------------------------------------------------------------------
				if(Prefs.GetBool("RecallCardsShowReturnAdd")){
					str=Prefs.GetString("PracticeTitle")+"\r\n";
					g.DrawString(str,new Font(FontFamily.GenericSansSerif,9,FontStyle.Bold),Brushes.Black,xPos+45,yPos+60);
					str=Prefs.GetString("PracticeAddress")+"\r\n";
					if(Prefs.GetString("PracticeAddress2")!=""){
						str+=Prefs.GetString("PracticeAddress2")+"\r\n";
					}
					str+=Prefs.GetString("PracticeCity")+",  "+Prefs.GetString("PracticeST")+"  "+Prefs.GetString("PracticeZip")+"\r\n";
					string phone=Prefs.GetString("PracticePhone");
					if(CultureInfo.CurrentCulture.Name=="en-US"&& phone.Length==10){
						str+="("+phone.Substring(0,3)+")"+phone.Substring(3,3)+"-"+phone.Substring(6);
					}
					else{//any other phone format
						str+=phone;
					}
					g.DrawString(str,new Font(FontFamily.GenericSansSerif,8),Brushes.Black,xPos+45,yPos+75);
				}
				//Body text-------------------------------------------------------------------------------
				str=textPostcardMessage.Text.Replace("?DueDate"
					,PIn.PDate(AddrTable.Rows[patientsPrinted]["DateDue"].ToString()).ToShortDateString());
				g.DrawString(str,new Font(FontFamily.GenericSansSerif,10),Brushes.Black,new RectangleF(xPos+45,yPos+180,250,190));
				//Patient's Address-----------------------------------------------------------------------
				str=AddrTable.Rows[patientsPrinted]["FName"].ToString()+" "
						+AddrTable.Rows[patientsPrinted]["MiddleI"].ToString()+" "
						+AddrTable.Rows[patientsPrinted]["LName"].ToString()+"\r\n"
						+AddrTable.Rows[patientsPrinted]["Address"].ToString()+"\r\n";
					if(AddrTable.Rows[patientsPrinted]["Address2"].ToString()!=""){
						str+=AddrTable.Rows[patientsPrinted]["Address2"].ToString()+"\r\n";
					}
					str+=AddrTable.Rows[patientsPrinted]["City"].ToString()+", "
						+AddrTable.Rows[patientsPrinted]["State"].ToString()+"   "
						+AddrTable.Rows[patientsPrinted]["Zip"].ToString()+"\r\n";
				g.DrawString(str,new Font(FontFamily.GenericSansSerif,11),Brushes.Black,xPos+320,yPos+240);
				if(Prefs.GetInt("RecallPostcardsPerSheet")==1){
					yPos+=400;
				}
				else if(Prefs.GetInt("RecallPostcardsPerSheet")==3){
					yPos+=366;
				}
				else{//4
					xPos+=550;
					if(xPos>1000){
						xPos=0;
						yPos+=425;
					}
				}
				patientsPrinted++;
			}//while
			pagesPrinted++;
			if(pagesPrinted==totalPages){
				ev.HasMorePages=false;
				pagesPrinted=0;
				patientsPrinted=0;
			}
			else{
				ev.HasMorePages=true;
			}
		}

		private void butRefresh_Click(object sender, System.EventArgs e) {
			FillMain();
		}

		private void butSetStatus_Click(object sender, System.EventArgs e) {
			if(comboStatus.SelectedIndex==-1){
				return;
			}
			int[] originalRecalls=new int[tbMain.SelectedIndices.Length];
			for(int i=0;i<tbMain.SelectedIndices.Length;i++){
				originalRecalls[i]=((RecallItem)MainAL[tbMain.SelectedIndices[i]]).RecallNum;
				Recalls.UpdateStatus(
					((RecallItem)MainAL[tbMain.SelectedIndices[i]]).RecallNum,
					Defs.Short[(int)DefCat.RecallUnschedStatus][comboStatus.SelectedIndex].DefNum);
				//((RecallItem)MainAL[tbMain.SelectedIndices[i]]).up
			}
			FillMain();
			for(int i=0;i<tbMain.MaxRows;i++){
				for(int j=0;j<originalRecalls.Length;j++){
					if(originalRecalls[j]==((RecallItem)MainAL[i]).RecallNum){
						tbMain.SetSelected(i,true);
					}
				}
			}
		}

		private void butSave_Click(object sender, System.EventArgs e) {
			if(  textDaysPast.errorProvider1.GetError(textDaysPast)!=""
				|| textDaysFuture.errorProvider1.GetError(textDaysFuture)!="")
			{
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			Prefs.Cur.PrefName="RecallDaysPast";
			Prefs.Cur.ValueString=textDaysPast.Text;
			Prefs.UpdateCur();
			Prefs.Cur.PrefName="RecallDaysFuture";
			Prefs.Cur.ValueString=textDaysFuture.Text;
			Prefs.UpdateCur();
			DataValid.SetInvalid(InvalidTypes.Prefs);
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		

		

		
	}


	///<summary>Mostly used just to display the recall list.</summary>
	public struct RecallItem{
		///<summary></summary>
		public DateTime DueDate;
		///<summary></summary>
		public string PatientName;
		///<summary></summary>
		public DateTime BirthDate;
		///<summary></summary>
		public Interval RecallInterval;
		///<summary></summary>
		public int RecallStatus;
		///<summary></summary>
		public int PatNum;
		///<summary></summary>
		public int Age;
		///<summary></summary>
		public string Note;
		///<summary></summary>
		public int RecallNum;
	}
}
