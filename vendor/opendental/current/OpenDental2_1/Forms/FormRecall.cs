/*=============================================================================================================
FreeDental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormRecall : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butClose;
		private System.Windows.Forms.Button butRefresh;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button butSave;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ListBox listProcs;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label3;
		private OpenDental.TableRecall tbMain;
		private ArrayList MainAL;
		private OpenDental.ValidNumber textDaysPast;
		private OpenDental.ValidNumber textDaysFuture;
		private Patients Patients=new Patients();
		public static RecallItem Cur;
		public bool PinClicked=false;
		private System.Windows.Forms.Button butReport;
		private Appointments Appointments=new Appointments();

		public FormRecall(){
			InitializeComponent();// Required for Windows Form Designer support
			tbMain.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbMain_CellClicked);
			tbMain.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbMain_CellDoubleClicked);
			Lan.C(this, new System.Windows.Forms.Control[] {
				label1,
				label2,
				butRefresh,
				groupBox1,
				groupBox2,
				label3,
				butReport,
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

		private void InitializeComponent()
		{
			this.butClose = new System.Windows.Forms.Button();
			this.tbMain = new OpenDental.TableRecall();
			this.butRefresh = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textDaysFuture = new OpenDental.ValidNumber();
			this.textDaysPast = new OpenDental.ValidNumber();
			this.label3 = new System.Windows.Forms.Label();
			this.butSave = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.listProcs = new System.Windows.Forms.ListBox();
			this.butReport = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.Location = new System.Drawing.Point(626, 668);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 27);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// tbMain
			// 
			this.tbMain.BackColor = System.Drawing.SystemColors.Window;
			this.tbMain.Location = new System.Drawing.Point(9, 22);
			this.tbMain.Name = "tbMain";
			this.tbMain.SelectedIndices = new int[0];
			this.tbMain.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.tbMain.Size = new System.Drawing.Size(429, 638);
			this.tbMain.TabIndex = 0;
			// 
			// butRefresh
			// 
			this.butRefresh.Location = new System.Drawing.Point(13, 111);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.TabIndex = 2;
			this.butRefresh.Text = "Refresh List";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textDaysFuture);
			this.groupBox1.Controls.Add(this.textDaysPast);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.butSave);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.butRefresh);
			this.groupBox1.Location = new System.Drawing.Point(501, 30);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 145);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "View";
			// 
			// textDaysFuture
			// 
			this.textDaysFuture.Location = new System.Drawing.Point(99, 58);
			this.textDaysFuture.Name = "textDaysFuture";
			this.textDaysFuture.Size = new System.Drawing.Size(51, 20);
			this.textDaysFuture.TabIndex = 1;
			this.textDaysFuture.Text = "";
			// 
			// textDaysPast
			// 
			this.textDaysPast.Location = new System.Drawing.Point(99, 31);
			this.textDaysPast.Name = "textDaysPast";
			this.textDaysPast.Size = new System.Drawing.Size(51, 20);
			this.textDaysPast.TabIndex = 0;
			this.textDaysPast.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(20, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(162, 17);
			this.label3.TabIndex = 16;
			this.label3.Text = "(leave days blank to view all)";
			// 
			// butSave
			// 
			this.butSave.Location = new System.Drawing.Point(96, 111);
			this.butSave.Name = "butSave";
			this.butSave.Size = new System.Drawing.Size(94, 23);
			this.butSave.TabIndex = 3;
			this.butSave.Text = "Save As Default";
			this.butSave.Click += new System.EventHandler(this.butSave_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(19, 61);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(78, 14);
			this.label2.TabIndex = 12;
			this.label2.Text = "Days Future";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(28, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 14);
			this.label1.TabIndex = 11;
			this.label1.Text = "Days Past";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.listProcs);
			this.groupBox2.Location = new System.Drawing.Point(501, 325);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(158, 289);
			this.groupBox2.TabIndex = 12;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Procedures that Trigger Recall - You can change these in procedure code setup";
			// 
			// listProcs
			// 
			this.listProcs.BackColor = System.Drawing.SystemColors.Control;
			this.listProcs.Location = new System.Drawing.Point(16, 69);
			this.listProcs.Name = "listProcs";
			this.listProcs.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.listProcs.Size = new System.Drawing.Size(124, 199);
			this.listProcs.TabIndex = 0;
			// 
			// butReport
			// 
			this.butReport.Location = new System.Drawing.Point(501, 235);
			this.butReport.Name = "butReport";
			this.butReport.TabIndex = 13;
			this.butReport.Text = "Run Report";
			this.butReport.Click += new System.EventHandler(this.butReport_Click);
			// 
			// FormRecall
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(726, 719);
			this.Controls.Add(this.butReport);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.tbMain);
			this.Controls.Add(this.butClose);
			this.Name = "FormRecall";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Recall List";
			this.Load += new System.EventHandler(this.FormRecall_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRecall_Load(object sender, System.EventArgs e) {
			textDaysPast.Text=((Pref)Prefs.HList["RecallDaysPast"]).ValueString;
			textDaysFuture.Text=((Pref)Prefs.HList["RecallDaysFuture"]).ValueString;
			if(textDaysPast.Text=="-1")
				textDaysPast.Text="";
			if(textDaysFuture.Text=="-1")
				textDaysFuture.Text="";
			textDaysPast.MaxVal=100000;
			textDaysFuture.MaxVal=100000;
			FillProcs();
			FillMain();
		}

		private void FillProcs(){
			listProcs.Items.Clear();
			for(int i=0;i<ProcCodes.RecallAL.Count;i++){
				listProcs.Items.Add(((ProcedureCode)ProcCodes.RecallAL[i]).Descript);
			}
		}

		private void FillMain(){
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
			MainAL=new ArrayList();
			Patients.GetRecallList();
			//initial list also includes patients who already have appointments
			bool doAdd;
			//MessageBox.Show(Patients.RecallList.Length.ToString());
			for(int i=0;i<Patients.RecallList.Length;i++){
				doAdd=false;
				if(DateTime.Compare(Patients.RecallList[i].DueDate,DateTime.Today)<=0){//due date today or earlier
					if(daysPast==-1){
						doAdd=true;
					}
					else if(DateTime.Compare(Patients.RecallList[i].DueDate,DateTime.Today.AddDays(-daysPast))>=0){
						doAdd=true;
					}
				}
				else{//due date tomorrow or later
					if(daysFuture==-1){
						doAdd=true;
					}
					else if(DateTime.Compare(Patients.RecallList[i].DueDate,DateTime.Today.AddDays(daysFuture))<=0){
						doAdd=true;
					}
				}
				if(doAdd){//test for existing appointment
					if(Appointments.PatientHasFutureRecall(Patients.RecallList[i].PatNum)){
						doAdd=false;
					}
				}
				if(doAdd){
					MainAL.Add(Patients.RecallList[i]);
				}
			}
			//Fill list:
			tbMain.SelectedRow=-1;
			tbMain.ResetRows(MainAL.Count);
			tbMain.SetGridColor(Color.DarkGray);
			for (int i=0;i<MainAL.Count;i++){
				tbMain.Cell[0,i]=((RecallItem)MainAL[i]).DueDate.ToString("d");
				tbMain.Cell[1,i]=((RecallItem)MainAL[i]).PatientName;
				tbMain.Cell[2,i]=((RecallItem)MainAL[i]).Age;
				tbMain.Cell[3,i]=((RecallItem)MainAL[i]).RecallInterval.ToString();;
				tbMain.Cell[4,i]=Defs.GetName(DefCat.RecallUnschedStatus,((RecallItem)MainAL[i]).RecallStatus);
				/*
				switch(((RecallItem)MainAL[i]).NextAptNum){
					case 0:
						tbMain.Cell[5,i]="";
						break;
					case -1:
						tbMain.Cell[5,i]="done";
						break;
					default :
						if(DateTime.Compare(((RecallItem)MainAL[i]).AptDateTime,DateTime.Parse("1/1/1860"))<0){
							tbMain.Cell[5,i]="unscheduled";
						}
						else{
							tbMain.Cell[5,i]=((RecallItem)MainAL[i]).AptDateTime.ToString("d");
						}
						break;
				}*/
			}
			//if(tbApts.SelectedRow!=-1){
			//	tbApts.ColorRow(tbApts.SelectedRow,Color.LightGray);
			//}
			tbMain.LayoutTables();
		}

		private void tbMain_CellClicked(object sender, CellEventArgs e){
			//if(tbMain.SelectedRow!=-1){
			//	tbMain.ColorRow(e.Row,Color.White);
			//}
			//tbMain.SelectedRow=e.Row;
			//tbMain.ColorRow(e.Row,Color.LightGray);
			Cur=(RecallItem)MainAL[e.Row];
		}

		private void tbMain_CellDoubleClicked(object sender, CellEventArgs e){
			FormRecallEdit FormRE=new FormRecallEdit();
			if(Cur.DueDate.CompareTo(DateTime.Parse("1/1/1880"))>0){
				FormRE.DueDate=Cur.DueDate;
			}
			FormRE.ShowDialog();
			if(FormRE.PinClicked){
				PinClicked=true;
				DialogResult=DialogResult.OK;
			}
			else{
				FillMain();
			}
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			/*  not used
			FormProcedures FormP = new FormProcedures();
			FormP.Mode=FormProcMode.Select;
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK) return;
			ProcCodes.Cur=(ProcedureCode)ProcCodes.HList[FormP.SelectedADA];
			ProcCodes.Cur.SetRecall=true;
			ProcCodes.UpdateCur();
			DataValid.IType=InvalidType.LocalData;//refreshes ProcCodes on other computers
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
			FillProcs();
			FillMain();*/
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			/* not used
			if(listProcs.SelectedIndex==-1){
				MessageBox.Show("Please select a procedure first.");
				return;
			}
			ProcCodes.Cur=(ProcedureCode)ProcCodes.RecallAL[listProcs.SelectedIndex];
			ProcCodes.Cur.SetRecall=false;
			ProcCodes.UpdateCur();
			DataValid.IType=InvalidType.LocalData;//refreshes ProcCodes on other computers
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
			FillProcs();
			FillMain();*/
		}

		private void butRefresh_Click(object sender, System.EventArgs e) {
			FillMain();
		}

		private void butSave_Click(object sender, System.EventArgs e) {
			if(  textDaysPast.errorProvider1.GetError(textDaysPast)!=""
				|| textDaysFuture.errorProvider1.GetError(textDaysFuture)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			Prefs.Cur.PrefName="RecallDaysPast";
			Prefs.Cur.ValueString=textDaysPast.Text;
			Prefs.UpdateCur();
			Prefs.Cur.PrefName="RecallDaysFuture";
			Prefs.Cur.ValueString=textDaysFuture.Text;
			Prefs.UpdateCur();
			DataValid.IType=InvalidType.LocalData;//refreshes local data on other computers
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.OK;
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
	}

	public struct RecallItem{
		public DateTime DueDate;
		public string PatientName;
		public DateTime BirthDate;
		public int RecallInterval;
		public int RecallStatus;
		public int PatNum;
		//public int NextAptNum;
		//public DateTime AptDateTime;
		public string Age;
	}
}
