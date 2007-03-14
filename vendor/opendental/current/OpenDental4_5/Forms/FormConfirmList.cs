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
	public class FormConfirmList : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butRefresh;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.ComponentModel.Container components = null;
		///<summary>Will be set to true when form closes if user click Send to Pinboard.</summary>
		public bool PinClicked=false;
		private OpenDental.UI.Button butReport;
		private System.Windows.Forms.PrintDialog printDialog2;
		private int pagesPrinted;
		private DataTable AddrTable;
		private int patientsPrinted;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ComboBox comboStatus;
		private OpenDental.UI.Button butLabels;
		private OpenDental.UI.Button butPostcards;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.TextBox textPostcardMessage;
		private System.Windows.Forms.Label label4;
		private OpenDental.ValidDate textDateFrom;
		private OpenDental.ValidDate textDateTo;
		private OpenDental.UI.ODGrid grid;
		//<summary>When this form closes, this will be the patNum of the last patient viewed.  The calling form should then make use of this to refresh to that patient.  If 0, then calling form should not refresh.</summary>
		//public int SelectedPatNum;
		///<summary>This list of appointments displayed</summary>
		private DataTable table;
		private PrintDocument pd;
		private OpenDental.UI.PrintPreview printPreview;

		///<summary></summary>
		public FormConfirmList(){
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

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfirmList));
			this.butClose = new OpenDental.UI.Button();
			this.butRefresh = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textDateTo = new OpenDental.ValidDate();
			this.textDateFrom = new OpenDental.ValidDate();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.butReport = new OpenDental.UI.Button();
			this.butLabels = new OpenDental.UI.Button();
			this.printDialog2 = new System.Windows.Forms.PrintDialog();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.comboStatus = new System.Windows.Forms.ComboBox();
			this.butPostcards = new OpenDental.UI.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textPostcardMessage = new System.Windows.Forms.TextBox();
			this.grid = new OpenDental.UI.ODGrid();
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
			this.butClose.Location = new System.Drawing.Point(895,656);
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
			this.butRefresh.Location = new System.Drawing.Point(98,62);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(84,26);
			this.butRefresh.TabIndex = 2;
			this.butRefresh.Text = "&Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.textDateTo);
			this.groupBox1.Controls.Add(this.textDateFrom);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.butRefresh);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(5,4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(188,93);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "View";
			// 
			// textDateTo
			// 
			this.textDateTo.Location = new System.Drawing.Point(88,38);
			this.textDateTo.Name = "textDateTo";
			this.textDateTo.Size = new System.Drawing.Size(94,20);
			this.textDateTo.TabIndex = 14;
			// 
			// textDateFrom
			// 
			this.textDateFrom.Location = new System.Drawing.Point(88,16);
			this.textDateFrom.Name = "textDateFrom";
			this.textDateFrom.Size = new System.Drawing.Size(94,20);
			this.textDateFrom.TabIndex = 13;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8,40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77,14);
			this.label2.TabIndex = 12;
			this.label2.Text = "To Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8,19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77,14);
			this.label1.TabIndex = 11;
			this.label1.Text = "From Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butReport
			// 
			this.butReport.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butReport.Autosize = true;
			this.butReport.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butReport.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butReport.Location = new System.Drawing.Point(860,53);
			this.butReport.Name = "butReport";
			this.butReport.Size = new System.Drawing.Size(87,26);
			this.butReport.TabIndex = 13;
			this.butReport.Text = "R&un Report";
			this.butReport.Click += new System.EventHandler(this.butReport_Click);
			// 
			// butLabels
			// 
			this.butLabels.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLabels.Autosize = true;
			this.butLabels.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLabels.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLabels.Image = ((System.Drawing.Image)(resources.GetObject("butLabels.Image")));
			this.butLabels.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butLabels.Location = new System.Drawing.Point(860,17);
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
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(203,4);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(143,93);
			this.groupBox3.TabIndex = 15;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Set Status";
			// 
			// comboStatus
			// 
			this.comboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatus.Location = new System.Drawing.Point(7,22);
			this.comboStatus.MaxDropDownItems = 40;
			this.comboStatus.Name = "comboStatus";
			this.comboStatus.Size = new System.Drawing.Size(128,21);
			this.comboStatus.TabIndex = 15;
			this.comboStatus.SelectionChangeCommitted += new System.EventHandler(this.comboStatus_SelectionChangeCommitted);
			this.comboStatus.SelectedIndexChanged += new System.EventHandler(this.comboStatus_SelectedIndexChanged);
			// 
			// butPostcards
			// 
			this.butPostcards.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPostcards.Autosize = true;
			this.butPostcards.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPostcards.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPostcards.Image = ((System.Drawing.Image)(resources.GetObject("butPostcards.Image")));
			this.butPostcards.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPostcards.Location = new System.Drawing.Point(184,62);
			this.butPostcards.Name = "butPostcards";
			this.butPostcards.Size = new System.Drawing.Size(87,26);
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
			this.groupBox4.Location = new System.Drawing.Point(356,4);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(279,93);
			this.groupBox4.TabIndex = 17;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Postcards";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(7,12);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(158,17);
			this.label4.TabIndex = 18;
			this.label4.Text = "Message";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textPostcardMessage
			// 
			this.textPostcardMessage.AcceptsReturn = true;
			this.textPostcardMessage.Location = new System.Drawing.Point(10,30);
			this.textPostcardMessage.Multiline = true;
			this.textPostcardMessage.Name = "textPostcardMessage";
			this.textPostcardMessage.Size = new System.Drawing.Size(168,58);
			this.textPostcardMessage.TabIndex = 17;
			this.textPostcardMessage.Text = "We would like to confirm your appointment on ?date at ?time";
			// 
			// grid
			// 
			this.grid.HScrollVisible = false;
			this.grid.Location = new System.Drawing.Point(4,103);
			this.grid.Name = "grid";
			this.grid.ScrollValue = 0;
			this.grid.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.grid.Size = new System.Drawing.Size(963,546);
			this.grid.TabIndex = 0;
			this.grid.Title = "Confirmation List";
			this.grid.TranslationName = "TableConfirmList";
			this.grid.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.grid_CellClick);
			this.grid.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.grid_CellDoubleClick);
			// 
			// FormConfirmList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(975,688);
			this.Controls.Add(this.grid);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.butLabels);
			this.Controls.Add(this.butReport);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butClose);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormConfirmList";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Confirmation List";
			this.Load += new System.EventHandler(this.FormConfirmList_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private void FormConfirmList_Load(object sender, System.EventArgs e) {
			textDateFrom.Text=AddWorkDays(1,DateTime.Today).ToShortDateString();
			textDateTo.Text=AddWorkDays(2,DateTime.Today).ToShortDateString();
			textPostcardMessage.Text=PrefB.GetString("ConfirmPostcardMessage");
			comboStatus.Items.Clear();
			for(int i=0;i<Defs.Short[(int)DefCat.ApptConfirmed].Length;i++){
				comboStatus.Items.Add(Defs.Short[(int)DefCat.ApptConfirmed][i].ItemName);
			}
			FillMain();
		}

		///<summary>Adds the specified number of work days, skipping saturday and sunday.</summary>
		private DateTime AddWorkDays(int days,DateTime date){
			DateTime retVal=date;
			for(int i=0;i<days;i++){
				retVal=retVal.AddDays(1);
				//then, this part jumps to monday if on a sat or sun
				while(retVal.DayOfWeek==DayOfWeek.Saturday || retVal.DayOfWeek==DayOfWeek.Sunday){
					retVal=retVal.AddDays(1);
				}
			}
			return retVal;
		}

		private void FillMain(){
			DateTime dateFrom=PIn.PDate(textDateFrom.Text);
			DateTime dateTo=PIn.PDate(textDateTo.Text);
			table=Appointments.GetConfirmList(dateFrom,dateTo);
			int scrollVal=grid.ScrollValue;
			grid.BeginUpdate();
			grid.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Date Time",70);
			grid.Columns.Add(col);
			col=new ODGridColumn("Patient",80);
			grid.Columns.Add(col);
			col=new ODGridColumn("Age",30);
			grid.Columns.Add(col);
			col=new ODGridColumn("Hm Phone",85);
			grid.Columns.Add(col);
			col=new ODGridColumn("Wk Phone",85);
			grid.Columns.Add(col);
			col=new ODGridColumn("Wireless",85);
			grid.Columns.Add(col);
			col=new ODGridColumn("Addr/Ph Note",100);
			grid.Columns.Add(col);
			col=new ODGridColumn("Status",80);
			grid.Columns.Add(col);
			col=new ODGridColumn("Procs",110);
			grid.Columns.Add(col);
			col=new ODGridColumn("Medical",80);
			grid.Columns.Add(col);
			col=new ODGridColumn("Appt Note",204);
			grid.Columns.Add(col);
			grid.Rows.Clear();
			ODGridRow row;
			DateTime aptDateTime;
			ODGridCell cell;
			for(int i=0;i<table.Rows.Count;i++){
				row=new ODGridRow();
				aptDateTime=PIn.PDateT(table.Rows[i][4].ToString());
				row.Cells.Add(aptDateTime.ToShortDateString()+"\r\n"+aptDateTime.ToShortTimeString());
				row.Cells.Add(PIn.PString(table.Rows[i][1].ToString())+",\r\n"+PIn.PString(table.Rows[i][2].ToString()));//patient
				row.Cells.Add(Shared.DateToAgeString(PIn.PDate(table.Rows[i][5].ToString())));//age
				row.Cells.Add(PIn.PString(table.Rows[i][6].ToString()));//Hm
				row.Cells.Add(PIn.PString(table.Rows[i][7].ToString()));//Wk
				row.Cells.Add(PIn.PString(table.Rows[i][8].ToString()));//Wireless
				row.Cells.Add(PIn.PString(table.Rows[i][12].ToString()));//AddrNote
				row.Cells.Add(Defs.GetName(DefCat.ApptConfirmed,PIn.PInt(table.Rows[i][10].ToString())));//status	
				row.Cells.Add(PIn.PString(table.Rows[i][9].ToString()));//procs
				cell=new ODGridCell(PIn.PString(table.Rows[i][14].ToString()));
				cell.ColorText=Color.Red;
				row.Cells.Add(cell);//med note
				row.Cells.Add(PIn.PString(table.Rows[i][11].ToString()));//note
				grid.Rows.Add(row);
			}
			grid.EndUpdate();
			grid.ScrollValue=scrollVal;
		}

		private void grid_CellClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			//row selected before this event triggered
			SetFamilyColors();
			comboStatus.SelectedIndex=-1;
		}

		private void SetFamilyColors(){
			if(grid.SelectedIndices.Length!=1){
				for(int i=0;i<grid.Rows.Count;i++){
					grid.Rows[i].ColorText=Color.Black;
				}
				grid.Invalidate();
				return;
			}
			int guar=PIn.PInt(table.Rows[grid.SelectedIndices[0]][3].ToString());
			int famCount=0;
			for(int i=0;i<grid.Rows.Count;i++){
				if(PIn.PInt(table.Rows[i][3].ToString())==guar){
					famCount++;
					grid.Rows[i].ColorText=Color.Red;
				}
				else{
					grid.Rows[i].ColorText=Color.Black;
				}
			}
			if(famCount==1){//only the highlighted patient is red at this point
				grid.Rows[grid.SelectedIndices[0]].ColorText=Color.Black;
			}
			grid.Invalidate();
		}

		private void grid_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			Cursor=Cursors.WaitCursor;
			int selectedApt=PIn.PInt(table.Rows[e.Row][13].ToString());
			Appointment apt=Appointments.GetOneApt(selectedApt);
			FormApptEdit FormA=new FormApptEdit(apt);
			FormA.ShowDialog();
			//if(FormA.DialogResult==DialogResult.Cancel){
			//	Cursor=Cursors.Default;
			FillMain();
			for(int i=0;i<table.Rows.Count;i++){
				if(PIn.PInt(table.Rows[i][13].ToString())==selectedApt){
					grid.SetSelected(i,true);
				}
			}
			SetFamilyColors();
			Cursor=Cursors.Default;
		}

		private void comboStatus_SelectionChangeCommitted(object sender, System.EventArgs e) {
			if(grid.SelectedIndices.Length==0){
				return;//because user could never initiate this action.
			}
			Appointment apt;
			Cursor=Cursors.WaitCursor;
			int[] selectedApts=new int[grid.SelectedIndices.Length];
			for(int i=0;i<grid.SelectedIndices.Length;i++){
				selectedApts[i]=PIn.PInt(table.Rows[grid.SelectedIndices[i]][13].ToString());//aptNum
			}
			for(int i=0;i<grid.SelectedIndices.Length;i++){
				apt=Appointments.GetOneApt(PIn.PInt(table.Rows[grid.SelectedIndices[i]][13].ToString()));
				Appointment aptOld=apt.Copy();
				int selectedI=comboStatus.SelectedIndex;
				apt.Confirmed=Defs.Short[(int)DefCat.ApptConfirmed][selectedI].DefNum;
				try{
					Appointments.InsertOrUpdate(apt,aptOld,false);
				}
				catch(ApplicationException ex){
					Cursor=Cursors.Default;
					MessageBox.Show(ex.Message);
					return;
				}
			}
			FillMain();
			//reselect all the apts
			for(int i=0;i<table.Rows.Count;i++){
				for(int j=0;j<selectedApts.Length;j++){
					if(PIn.PInt(table.Rows[i][13].ToString())==selectedApts[j]){
						grid.SetSelected(i,true);
					}
				}
			}
			SetFamilyColors();
			comboStatus.SelectedIndex=-1;
			Cursor=Cursors.Default;
		}

		private void comboStatus_SelectedIndexChanged(object sender, System.EventArgs e) {
			//?
		}

		private void butReport_Click(object sender, System.EventArgs e) {
		  if(table.Rows.Count==0){
        MessageBox.Show(Lan.g(this,"There are no appointments in the list.  Must have at least one to run report."));    
        return;
      }
      int[] aptNums;
      if(grid.SelectedIndices.Length==0){
        aptNums=new int[table.Rows.Count];
        for(int i=0;i<aptNums.Length;i++){
          aptNums[i]=PIn.PInt(table.Rows[i][13].ToString());
        }
      }
      else{
        aptNums=new int[grid.SelectedIndices.Length];
        for(int i=0;i<aptNums.Length;i++){
          aptNums[i]=PIn.PInt(table.Rows[grid.SelectedIndices[i]][13].ToString());
        }
      }
      FormRpConfirm FormC=new FormRpConfirm(aptNums);
      FormC.ShowDialog(); 
		}

		private void butLabels_Click(object sender, System.EventArgs e) {
			if(table.Rows.Count==0){
        MessageBox.Show(Lan.g(this,"There are no appointments in the list.  Must have at least one to print."));    
        return;
      }
			if(grid.SelectedIndices.Length==0){
				for(int i=0;i<table.Rows.Count;i++){
					grid.SetSelected(i,true);
				}
			}
      int[] aptNums=new int[grid.SelectedIndices.Length];
      for(int i=0;i<aptNums.Length;i++){
        aptNums[i]=PIn.PInt(table.Rows[grid.SelectedIndices[i]][13].ToString());
      }
			AddrTable=Appointments.GetAddrTable(aptNums);
			pagesPrinted=0;
			patientsPrinted=0;
			pd=new PrintDocument();
			pd.PrintPage+=new PrintPageEventHandler(this.pdLabels_PrintPage);
			pd.OriginAtMargins=true;
			pd.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			printPreview=new OpenDental.UI.PrintPreview(PrintSituation.LabelSheet
				,pd,(int)Math.Ceiling((double)AddrTable.Rows.Count/30));
			printPreview.ShowDialog();
		}

		private void butPostcards_Click(object sender, System.EventArgs e) {
			if(table.Rows.Count==0){
				MessageBox.Show(Lan.g(this,"There are no appointments in the list.  Must have at least one to print."));    
				return;
			}
			if(grid.SelectedIndices.Length==0){
				for(int i=0;i<table.Rows.Count;i++){
					grid.SetSelected(i,true);
				}
			}
      int[] aptNums=new int[grid.SelectedIndices.Length];
      for(int i=0;i<aptNums.Length;i++){
        aptNums[i]=PIn.PInt(table.Rows[grid.SelectedIndices[i]][13].ToString());
      }
			AddrTable=Appointments.GetAddrTable(aptNums);
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
			printPreview=new OpenDental.UI.PrintPreview(PrintSituation.Postcard,pd,
				(int)Math.Ceiling((double)AddrTable.Rows.Count/(double)PrefB.GetInt("RecallPostcardsPerSheet")));
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
			if(pagesPrinted>=totalPages){
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
			float yPos=0;//these refer to the upper left origin of each postcard
			float xPos=0;
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
				str=textPostcardMessage.Text;
				str=str.Replace("?date",PIn.PDate(AddrTable.Rows[patientsPrinted]["AptDateTime"].ToString()).ToShortDateString());
				str=str.Replace("?time",PIn.PDate(AddrTable.Rows[patientsPrinted]["AptDateTime"].ToString()).ToShortTimeString());
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
				if(PrefB.GetInt("RecallPostcardsPerSheet")==1){
					yPos+=400;
				}
				else if(PrefB.GetInt("RecallPostcardsPerSheet")==3){
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
			/*if(comboStatus.SelectedIndex==-1){
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
			}*/
		}

		private void butSave_Click(object sender, System.EventArgs e) {
			/*if(  textDaysPast.errorProvider1.GetError(textDaysPast)!=""
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
			DataValid.SetInvalid(InvalidTypes.Prefs);*/
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

	

		

		

		

		

		
	}

}
