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
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRecallList : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butRefresh;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label3;
		//private ArrayList MainAL;
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
		private OpenDental.UI.ODGrid gridMain;
		private ValidDate textDateEnd;
		private ValidDate textDateStart;
		private CheckBox checkGroupFamilies;
		private Label labelFamilyMessage;
		private TextBox textFamilyMessage;
		///<summary>When this form closes, this will be the patNum of the last patient viewed.  The calling form should then make use of this to refresh to that patient.  If 0, then calling form should not refresh.</summary>
		public int SelectedPatNum;

		///<summary></summary>
		public FormRecallList(){
			InitializeComponent();// Required for Windows Form Designer support
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRecallList));
			this.butClose = new OpenDental.UI.Button();
			this.butRefresh = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkGroupFamilies = new System.Windows.Forms.CheckBox();
			this.textDateEnd = new OpenDental.ValidDate();
			this.textDateStart = new OpenDental.ValidDate();
			this.label3 = new System.Windows.Forms.Label();
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
			this.labelFamilyMessage = new System.Windows.Forms.Label();
			this.textFamilyMessage = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textPostcardMessage = new System.Windows.Forms.TextBox();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(873,645);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.Location = new System.Drawing.Point(79,107);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(98,26);
			this.butRefresh.TabIndex = 2;
			this.butRefresh.Text = "&Refresh List";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.checkGroupFamilies);
			this.groupBox1.Controls.Add(this.textDateEnd);
			this.groupBox1.Controls.Add(this.textDateStart);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.butRefresh);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(771,14);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(188,144);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "View";
			// 
			// checkGroupFamilies
			// 
			this.checkGroupFamilies.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkGroupFamilies.Location = new System.Drawing.Point(6,16);
			this.checkGroupFamilies.Name = "checkGroupFamilies";
			this.checkGroupFamilies.Size = new System.Drawing.Size(108,18);
			this.checkGroupFamilies.TabIndex = 19;
			this.checkGroupFamilies.Text = "Group Families";
			this.checkGroupFamilies.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkGroupFamilies.UseVisualStyleBackColor = true;
			this.checkGroupFamilies.Click += new System.EventHandler(this.checkGroupFamilies_Click);
			// 
			// textDateEnd
			// 
			this.textDateEnd.Location = new System.Drawing.Point(100,59);
			this.textDateEnd.Name = "textDateEnd";
			this.textDateEnd.Size = new System.Drawing.Size(77,20);
			this.textDateEnd.TabIndex = 18;
			// 
			// textDateStart
			// 
			this.textDateStart.Location = new System.Drawing.Point(100,36);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.Size = new System.Drawing.Size(77,20);
			this.textDateStart.TabIndex = 17;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(13,87);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(162,17);
			this.label3.TabIndex = 16;
			this.label3.Text = "(leave dates blank to view all)";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(7,62);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(91,14);
			this.label2.TabIndex = 12;
			this.label2.Text = "End Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(13,39);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84,14);
			this.label1.TabIndex = 11;
			this.label1.Text = "Start Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butReport
			// 
			this.butReport.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butReport.Autosize = true;
			this.butReport.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butReport.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butReport.Location = new System.Drawing.Point(861,588);
			this.butReport.Name = "butReport";
			this.butReport.Size = new System.Drawing.Size(87,26);
			this.butReport.TabIndex = 13;
			this.butReport.Text = "R&un Report";
			this.butReport.Click += new System.EventHandler(this.butReport_Click);
			// 
			// butLabels
			// 
			this.butLabels.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLabels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butLabels.Autosize = true;
			this.butLabels.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLabels.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLabels.Image = ((System.Drawing.Image)(resources.GetObject("butLabels.Image")));
			this.butLabels.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butLabels.Location = new System.Drawing.Point(861,552);
			this.butLabels.Name = "butLabels";
			this.butLabels.Size = new System.Drawing.Size(87,26);
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
			this.groupBox3.Location = new System.Drawing.Point(771,168);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(188,89);
			this.groupBox3.TabIndex = 15;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Set Status";
			// 
			// comboStatus
			// 
			this.comboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatus.Location = new System.Drawing.Point(17,24);
			this.comboStatus.MaxDropDownItems = 40;
			this.comboStatus.Name = "comboStatus";
			this.comboStatus.Size = new System.Drawing.Size(160,21);
			this.comboStatus.TabIndex = 15;
			// 
			// butSetStatus
			// 
			this.butSetStatus.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSetStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butSetStatus.Autosize = true;
			this.butSetStatus.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSetStatus.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSetStatus.Location = new System.Drawing.Point(110,56);
			this.butSetStatus.Name = "butSetStatus";
			this.butSetStatus.Size = new System.Drawing.Size(67,26);
			this.butSetStatus.TabIndex = 14;
			this.butSetStatus.Text = "Set";
			this.butSetStatus.Click += new System.EventHandler(this.butSetStatus_Click);
			// 
			// butPostcards
			// 
			this.butPostcards.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPostcards.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPostcards.Autosize = true;
			this.butPostcards.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPostcards.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPostcards.Image = ((System.Drawing.Image)(resources.GetObject("butPostcards.Image")));
			this.butPostcards.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPostcards.Location = new System.Drawing.Point(90,241);
			this.butPostcards.Name = "butPostcards";
			this.butPostcards.Size = new System.Drawing.Size(87,26);
			this.butPostcards.TabIndex = 16;
			this.butPostcards.Text = "Preview";
			this.butPostcards.Click += new System.EventHandler(this.butPostcards_Click);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.labelFamilyMessage);
			this.groupBox4.Controls.Add(this.textFamilyMessage);
			this.groupBox4.Controls.Add(this.label4);
			this.groupBox4.Controls.Add(this.textPostcardMessage);
			this.groupBox4.Controls.Add(this.butPostcards);
			this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox4.Location = new System.Drawing.Point(771,263);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(188,272);
			this.groupBox4.TabIndex = 17;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Postcards";
			// 
			// labelFamilyMessage
			// 
			this.labelFamilyMessage.Location = new System.Drawing.Point(8,124);
			this.labelFamilyMessage.Name = "labelFamilyMessage";
			this.labelFamilyMessage.Size = new System.Drawing.Size(158,19);
			this.labelFamilyMessage.TabIndex = 20;
			this.labelFamilyMessage.Text = "Family Message";
			this.labelFamilyMessage.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textFamilyMessage
			// 
			this.textFamilyMessage.AcceptsReturn = true;
			this.textFamilyMessage.Location = new System.Drawing.Point(9,145);
			this.textFamilyMessage.Multiline = true;
			this.textFamilyMessage.Name = "textFamilyMessage";
			this.textFamilyMessage.Size = new System.Drawing.Size(168,90);
			this.textFamilyMessage.TabIndex = 19;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(7,11);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(158,19);
			this.label4.TabIndex = 18;
			this.label4.Text = "Message";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textPostcardMessage
			// 
			this.textPostcardMessage.AcceptsReturn = true;
			this.textPostcardMessage.Location = new System.Drawing.Point(8,32);
			this.textPostcardMessage.Multiline = true;
			this.textPostcardMessage.Name = "textPostcardMessage";
			this.textPostcardMessage.Size = new System.Drawing.Size(168,90);
			this.textPostcardMessage.TabIndex = 17;
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(9,14);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(754,657);
			this.gridMain.TabIndex = 18;
			this.gridMain.Title = "Recall List";
			this.gridMain.TranslationName = "TableRecallList";
			this.gridMain.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellClick);
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// FormRecallList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(975,691);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.butLabels);
			this.Controls.Add(this.butReport);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butClose);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRecallList";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Recall List";
			this.Load += new System.EventHandler(this.FormRecallList_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRecallList_Load(object sender, System.EventArgs e) {
			checkGroupFamilies.Checked=PrefB.GetBool("RecallGroupByFamily");
			int daysPast=PrefB.GetInt("RecallDaysPast");
			int daysFuture=PrefB.GetInt("RecallDaysFuture");
			if(daysPast==-1){
				textDateStart.Text="";
			}
			else{
				textDateStart.Text=DateTime.Today.AddDays(-daysPast).ToShortDateString();
			}
			if(daysFuture==-1) {
				textDateEnd.Text="";
			}
			else {
				textDateEnd.Text=DateTime.Today.AddDays(daysFuture).ToShortDateString();
			}
			textPostcardMessage.Text=PrefB.GetString("RecallPostcardMessage");
			textFamilyMessage.Text=PrefB.GetString("RecallPostcardFamMsg");
			comboStatus.Items.Clear();
			for(int i=0;i<Defs.Short[(int)DefCat.RecallUnschedStatus].Length;i++){
				comboStatus.Items.Add(Defs.Short[(int)DefCat.RecallUnschedStatus][i].ItemName);
			}
			FillMain();
		}

		private void FillMain(){
			if(textDateStart.errorProvider1.GetError(textDateStart)!=""
				|| textDateEnd.errorProvider1.GetError(textDateEnd)!="")
			{
				//MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(checkGroupFamilies.Checked){
				textFamilyMessage.Visible=true;
				labelFamilyMessage.Visible=true;
			}
			else{
				textFamilyMessage.Visible=false;
				labelFamilyMessage.Visible=false;
			}
			DateTime fromDate;
			DateTime toDate;
			if(textDateStart.Text==""){
				fromDate=DateTime.MinValue.AddDays(1);//because we don't want to include 010101
			}
			else{
				fromDate=PIn.PDate(textDateStart.Text);
			}
			if(textDateEnd.Text=="") {
				toDate=DateTime.MaxValue;
			}
			else {
				toDate=PIn.PDate(textDateEnd.Text);
			}
			RecallItem[] RecallList=Recalls.GetRecallList(fromDate,toDate,checkGroupFamilies.Checked);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableRecallList","Due Date"),75);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecallList","Patient"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecallList","Age"),30);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecallList","Interval"),50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecallList","Status"),150);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecallList","Note"),310);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<RecallList.Length;i++){
				row=new ODGridRow();
				row.Cells.Add(RecallList[i].DueDate.ToString("d"));
				row.Cells.Add(RecallList[i].PatientName);
				row.Cells.Add(RecallList[i].Age);
				row.Cells.Add(RecallList[i].RecallInterval.ToString());
				row.Cells.Add(Defs.GetName(DefCat.RecallUnschedStatus,RecallList[i].RecallStatus));
				row.Cells.Add(RecallList[i].Note);
				row.Tag=RecallList[i];
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellClick(object sender,OpenDental.UI.ODGridClickEventArgs e) {
			//row selected before this event triggered
			SetFamilyColors();
			//comboStatus.SelectedIndex=-1;//mess with this later
		}

		private void SetFamilyColors() {
			if(gridMain.SelectedIndices.Length!=1) {
				for(int i=0;i<gridMain.Rows.Count;i++) {
					gridMain.Rows[i].ColorText=Color.Black;
				}
				gridMain.Invalidate();
				return;
			}
			int guar=((RecallItem)gridMain.Rows[gridMain.SelectedIndices[0]].Tag).Guarantor;
			int famCount=0;
			for(int i=0;i<gridMain.Rows.Count;i++) {
				if(((RecallItem)gridMain.Rows[i].Tag).Guarantor==guar) {
					famCount++;
					gridMain.Rows[i].ColorText=Color.Red;
				}
				else {
					gridMain.Rows[i].ColorText=Color.Black;
				}
			}
			if(famCount==1) {//only the highlighted patient is red at this point
				gridMain.Rows[gridMain.SelectedIndices[0]].ColorText=Color.Black;
			}
			gridMain.Invalidate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			SelectedPatNum=((RecallItem)gridMain.Rows[e.Row].Tag).PatNum;
			Recall[] recalls=Recalls.GetList(new int[] {((RecallItem)gridMain.Rows[e.Row].Tag).PatNum});
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
			for(int i=0;i<gridMain.Rows.Count;i++) {
				if(((RecallItem)gridMain.Rows[i].Tag).PatNum==SelectedPatNum) {
					gridMain.SetSelected(i,true);
				}
			}
			SetFamilyColors();
		}

		private void checkGroupFamilies_Click(object sender,EventArgs e) {
			FillMain();
		}

		private void butReport_Click(object sender, System.EventArgs e) {
		  if(gridMain.Rows.Count < 1){
        MessageBox.Show(Lan.g(this,"There are no Patients in the Recall table.  Must have at least one to run report."));    
        return;
      }
      int[] PatNums;
      if(gridMain.SelectedIndices.Length < 1){
        PatNums=new int[gridMain.Rows.Count];
        for(int i=0;i<PatNums.Length;i++){
          PatNums[i]=((RecallItem)gridMain.Rows[i].Tag).PatNum;
        }
      }
      else{
        PatNums=new int[gridMain.SelectedIndices.Length];
        for(int i=0;i<PatNums.Length;i++){
          PatNums[i]=((RecallItem)gridMain.Rows[gridMain.SelectedIndices[i]].Tag).PatNum;
        }
      }
      FormRpRecall FormRPR=new FormRpRecall(PatNums);
      FormRPR.ShowDialog();      
		}

		private void butLabels_Click(object sender, System.EventArgs e) {
			if(gridMain.Rows.Count < 1){
        MessageBox.Show(Lan.g(this,"There are no Patients in the Recall table.  Must have at least one to print."));    
        return;
      }
			if(gridMain.SelectedIndices.Length==0){
				gridMain.SetSelected(true);
			}
      int[] PatNums;
      PatNums=new int[gridMain.SelectedIndices.Length];
      for(int i=0;i<PatNums.Length;i++){
        PatNums[i]=((RecallItem)gridMain.Rows[gridMain.SelectedIndices[i]].Tag).PatNum;
      }
			AddrTable=Recalls.GetAddrTable(PatNums,false);//can never group by family because there's no room to display the list.
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
			if(gridMain.Rows.Count < 1){
        MessageBox.Show(Lan.g(this,"There are no Patients in the Recall table.  Must have at least one to print."));    
        return;
      }
			if(gridMain.SelectedIndices.Length==0){
				gridMain.SetSelected(true);
			}
      int[] PatNums;
      PatNums=new int[gridMain.SelectedIndices.Length];
			for(int i=0;i<PatNums.Length;i++) {
				PatNums[i]=((RecallItem)gridMain.Rows[gridMain.SelectedIndices[i]].Tag).PatNum;
			}
			if(MsgBox.Show(this,true,"Make a commlog entry of 'postcard sent' for all of the selected patients?")) {
				for(int i=0;i<PatNums.Length;i++){
					//make commlog entries for each patient
					Commlogs.InsertForRecallPostcard(PatNums[i]);
				}
			}
			AddrTable=Recalls.GetAddrTable(PatNums,checkGroupFamilies.Checked);
			pagesPrinted=0;
			patientsPrinted=0;
			pd=new PrintDocument();
			pd.PrintPage+=new PrintPageEventHandler(this.pdCards_PrintPage);
			pd.OriginAtMargins=true;
			pd.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			if(PrefB.GetInt("RecallPostcardsPerSheet")==1){
				pd.DefaultPageSettings.PaperSize=new PaperSize("Postcard",400,600);
				pd.DefaultPageSettings.Landscape=true;
			}
			else if(PrefB.GetInt("RecallPostcardsPerSheet")==3){
				pd.DefaultPageSettings.PaperSize=new PaperSize("Postcard",850,1100);
			}
			else{//4
				pd.DefaultPageSettings.PaperSize=new PaperSize("Postcard",850,1100);
				pd.DefaultPageSettings.Landscape=true;
			}
			int totalPages=(int)Math.Ceiling((double)AddrTable.Rows.Count/(double)PrefB.GetInt("RecallPostcardsPerSheet"));
			printPreview=new OpenDental.UI.PrintPreview(PrintSituation.Postcard,pd,totalPages);
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
			int totalPages=(int)Math.Ceiling((double)AddrTable.Rows.Count/(double)PrefB.GetInt("RecallPostcardsPerSheet"));
			Graphics g=ev.Graphics;
			int yAdj=(int)(PrefB.GetDouble("RecallAdjustDown")*100);
			int xAdj=(int)(PrefB.GetDouble("RecallAdjustRight")*100);
			float yPos=0+yAdj;//these refer to the upper left origin of each postcard
			float xPos=0+xAdj;
			string str;
			while(yPos<ev.PageBounds.Height-100 && patientsPrinted<AddrTable.Rows.Count){
				//Return Address--------------------------------------------------------------------------
				if(PrefB.GetBool("RecallCardsShowReturnAdd")){
					str=PrefB.GetString("PracticeTitle")+"\r\n";
					g.DrawString(str,new Font(FontFamily.GenericSansSerif,9,FontStyle.Bold),Brushes.Black,xPos+45,yPos+60);
					str=PrefB.GetString("PracticeAddress")+"\r\n";
					if(PrefB.GetString("PracticeAddress2")!=""){
						str+=PrefB.GetString("PracticeAddress2")+"\r\n";
					}
					str+=PrefB.GetString("PracticeCity")+",  "+PrefB.GetString("PracticeST")+"  "+PrefB.GetString("PracticeZip")+"\r\n";
					string phone=PrefB.GetString("PracticePhone");
					if(CultureInfo.CurrentCulture.Name=="en-US"&& phone.Length==10){
						str+="("+phone.Substring(0,3)+")"+phone.Substring(3,3)+"-"+phone.Substring(6);
					}
					else{//any other phone format
						str+=phone;
					}
					g.DrawString(str,new Font(FontFamily.GenericSansSerif,8),Brushes.Black,xPos+45,yPos+75);
				}
				//Body text-------------------------------------------------------------------------------
				if(checkGroupFamilies.Checked
					&& AddrTable.Rows[patientsPrinted]["FamList"].ToString()!="")//print family card
				{
					str=textFamilyMessage.Text.Replace("?FamilyList"
						,AddrTable.Rows[patientsPrinted]["FamList"].ToString());
				}
				else{//print single card
					str=textPostcardMessage.Text.Replace("?DueDate"
						,PIn.PDate(AddrTable.Rows[patientsPrinted]["DateDue"].ToString()).ToShortDateString());
				}
				g.DrawString(str,new Font(FontFamily.GenericSansSerif,10),Brushes.Black,new RectangleF(xPos+45,yPos+180,250,190));
				//Patient's Address-----------------------------------------------------------------------
				if(checkGroupFamilies.Checked
					&& AddrTable.Rows[patientsPrinted]["FamList"].ToString()!="")//print family card
				{
					str=AddrTable.Rows[patientsPrinted]["LName"].ToString()+" "+Lan.g(this,"Household")+"\r\n";
				}
				else{//print single card
					str=AddrTable.Rows[patientsPrinted]["FName"].ToString()+" "
						+AddrTable.Rows[patientsPrinted]["MiddleI"].ToString()+" "
						+AddrTable.Rows[patientsPrinted]["LName"].ToString()+"\r\n";
				}
				str+=AddrTable.Rows[patientsPrinted]["Address"].ToString()+"\r\n";
					if(AddrTable.Rows[patientsPrinted]["Address2"].ToString()!=""){
						str+=AddrTable.Rows[patientsPrinted]["Address2"].ToString()+"\r\n";
					}
					str+=AddrTable.Rows[patientsPrinted]["City"].ToString()+", "
						+AddrTable.Rows[patientsPrinted]["State"].ToString()+"   "
						+AddrTable.Rows[patientsPrinted]["Zip"].ToString()+"\r\n";
				g.DrawString(str,new Font(FontFamily.GenericSansSerif,11),Brushes.Black,xPos+320,yPos+240);
				if(PrefB.GetInt("RecallPostcardsPerSheet")==1){
					yPos+=400;
				}
				else if(PrefB.GetInt("RecallPostcardsPerSheet")==3){
					yPos+=366;
				}
				else{//4
					xPos+=550;
					if(xPos>1000){
						xPos=0+xAdj;
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
			int[] originalRecalls=new int[gridMain.SelectedIndices.Length];
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				originalRecalls[i]=((RecallItem)gridMain.Rows[gridMain.SelectedIndices[i]].Tag).RecallNum;
				Recalls.UpdateStatus(
					((RecallItem)gridMain.Rows[gridMain.SelectedIndices[i]].Tag).RecallNum,
					Defs.Short[(int)DefCat.RecallUnschedStatus][comboStatus.SelectedIndex].DefNum);
				//((RecallItem)MainAL[tbMain.SelectedIndices[i]]).up
			}
			FillMain();
			for(int i=0;i<gridMain.Rows.Count;i++){
				for(int j=0;j<originalRecalls.Length;j++){
					if(originalRecalls[j]==((RecallItem)gridMain.Rows[i].Tag).RecallNum){
						gridMain.SetSelected(i,true);
					}
				}
			}
		}

		/*private void butSave_Click(object sender, System.EventArgs e) {
			if(  textDateStart.errorProvider1.GetError(textDateStart)!=""
				|| textDateEnd.errorProvider1.GetError(textDateEnd)!="")
			{
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			int daysPast=((TimeSpan)(DateTime.Today-PIn.PDate(textDateStart.Text))).Days;//can be neg
			int daysFuture=((TimeSpan)(PIn.PDate(textDateEnd.Text)-DateTime.Today)).Days;//can be neg
			if(Prefs.UpdateBool("RecallGroupByFamily",checkGroupFamilies.Checked)
				| Prefs.UpdateInt("RecallDaysPast",daysPast)
				| Prefs.UpdateInt("RecallDaysFuture",daysFuture))
			{
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
		}*/

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

	

		

		

		

		
	}


	///<summary>Mostly used just to display the recall list.</summary>
	public class RecallItem{
		///<summary></summary>
		public DateTime DueDate;
		///<summary></summary>
		public string PatientName;
		//<summary></summary>
		//public DateTime BirthDate;
		///<summary></summary>
		public Interval RecallInterval;
		///<summary></summary>
		public int RecallStatus;
		///<summary></summary>
		public int PatNum;
		///<summary>Stored as a string because it might be blank.</summary>
		public string Age;
		///<summary></summary>
		public string Note;
		///<summary></summary>
		public int RecallNum;
		///<summary></summary>
		public int Guarantor;
	}
}
