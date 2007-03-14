/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace OpenDental{

	///<summary></summary>
	public class ContrFamily : System.Windows.Forms.UserControl{
		private System.Windows.Forms.ImageList imageListToolBar;
		private System.ComponentModel.IContainer components;
		private OpenDental.TableInsPlans tbPlans;
		private OpenDental.TablePatient tbPatient;
		private OpenDental.TableFamily tbFamily;
		private System.Windows.Forms.TextBox textAddrNotes;
		private OpenDental.TableCoverage tbCoverage;
		private System.Windows.Forms.TextBox textPriPlanNote;
		private System.Windows.Forms.TextBox textSecPlanNote;
		private System.Windows.Forms.Button butDeletePt;
		private System.Windows.Forms.Button butAddPt;
		private System.Windows.Forms.Panel panelFamily;
		private System.Windows.Forms.Button butPat;
		private System.Windows.Forms.Button butSetGuar;
		private System.Windows.Forms.Button butMovePat;
		private OpenDental.TablePercent tbPercent1;
		private System.Windows.Forms.Button butOutlook;
		private OpenDental.TablePercent tbPercent2;

		///<summary></summary>
		public ContrFamily(){
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
			tbPlans.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbPlans_CellClicked);
			tbPlans.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbPlans_CellDoubleClicked);
			tbFamily.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbFamily_CellClicked);
			tbPatient.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbPatient_CellDoubleClicked);
			tbCoverage.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbCoverage_CellDoubleClicked);
			tbPercent1.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbPercent1_CellDoubleClicked);
			tbPercent2.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbPercent2_CellDoubleClicked);
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

		#region Component Designer generated code

		private void InitializeComponent(){
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ContrFamily));
			this.imageListToolBar = new System.Windows.Forms.ImageList(this.components);
			this.tbPlans = new OpenDental.TableInsPlans();
			this.tbPatient = new OpenDental.TablePatient();
			this.textAddrNotes = new System.Windows.Forms.TextBox();
			this.tbFamily = new OpenDental.TableFamily();
			this.tbCoverage = new OpenDental.TableCoverage();
			this.textPriPlanNote = new System.Windows.Forms.TextBox();
			this.textSecPlanNote = new System.Windows.Forms.TextBox();
			this.butPat = new System.Windows.Forms.Button();
			this.butDeletePt = new System.Windows.Forms.Button();
			this.butAddPt = new System.Windows.Forms.Button();
			this.panelFamily = new System.Windows.Forms.Panel();
			this.butMovePat = new System.Windows.Forms.Button();
			this.butSetGuar = new System.Windows.Forms.Button();
			this.tbPercent1 = new OpenDental.TablePercent();
			this.tbPercent2 = new OpenDental.TablePercent();
			this.butOutlook = new System.Windows.Forms.Button();
			this.panelFamily.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageListToolBar
			// 
			this.imageListToolBar.ImageSize = new System.Drawing.Size(22, 22);
			this.imageListToolBar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListToolBar.ImageStream")));
			this.imageListToolBar.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tbPlans
			// 
			this.tbPlans.BackColor = System.Drawing.SystemColors.Window;
			this.tbPlans.Location = new System.Drawing.Point(1, 492);
			this.tbPlans.Name = "tbPlans";
			this.tbPlans.SelectedIndices = new int[0];
			this.tbPlans.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbPlans.Size = new System.Drawing.Size(459, 100);
			this.tbPlans.TabIndex = 1;
			// 
			// tbPatient
			// 
			this.tbPatient.BackColor = System.Drawing.SystemColors.Window;
			this.tbPatient.Location = new System.Drawing.Point(0, 32);
			this.tbPatient.Name = "tbPatient";
			this.tbPatient.SelectedIndices = new int[0];
			this.tbPatient.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbPatient.Size = new System.Drawing.Size(252, 429);
			this.tbPatient.TabIndex = 2;
			// 
			// textAddrNotes
			// 
			this.textAddrNotes.BackColor = System.Drawing.Color.White;
			this.textAddrNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textAddrNotes.ForeColor = System.Drawing.Color.Red;
			this.textAddrNotes.Location = new System.Drawing.Point(2, 400);
			this.textAddrNotes.Multiline = true;
			this.textAddrNotes.Name = "textAddrNotes";
			this.textAddrNotes.ReadOnly = true;
			this.textAddrNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textAddrNotes.Size = new System.Drawing.Size(248, 59);
			this.textAddrNotes.TabIndex = 3;
			this.textAddrNotes.Text = "";
			this.textAddrNotes.DoubleClick += new System.EventHandler(this.textAddrNotes_DoubleClick);
			// 
			// tbFamily
			// 
			this.tbFamily.BackColor = System.Drawing.SystemColors.Window;
			this.tbFamily.Location = new System.Drawing.Point(0, 26);
			this.tbFamily.Name = "tbFamily";
			this.tbFamily.SelectedIndices = new int[0];
			this.tbFamily.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbFamily.Size = new System.Drawing.Size(409, 99);
			this.tbFamily.TabIndex = 7;
			// 
			// tbCoverage
			// 
			this.tbCoverage.BackColor = System.Drawing.SystemColors.Window;
			this.tbCoverage.Location = new System.Drawing.Point(254, 32);
			this.tbCoverage.Name = "tbCoverage";
			this.tbCoverage.SelectedIndices = new int[0];
			this.tbCoverage.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbCoverage.Size = new System.Drawing.Size(542, 406);
			this.tbCoverage.TabIndex = 8;
			// 
			// textPriPlanNote
			// 
			this.textPriPlanNote.BackColor = System.Drawing.Color.White;
			this.textPriPlanNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textPriPlanNote.ForeColor = System.Drawing.Color.Red;
			this.textPriPlanNote.Location = new System.Drawing.Point(256, 307);
			this.textPriPlanNote.Multiline = true;
			this.textPriPlanNote.Name = "textPriPlanNote";
			this.textPriPlanNote.ReadOnly = true;
			this.textPriPlanNote.Size = new System.Drawing.Size(270, 129);
			this.textPriPlanNote.TabIndex = 9;
			this.textPriPlanNote.Text = "";
			this.textPriPlanNote.DoubleClick += new System.EventHandler(this.textPriPlanNote_DoubleClick);
			// 
			// textSecPlanNote
			// 
			this.textSecPlanNote.BackColor = System.Drawing.Color.White;
			this.textSecPlanNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textSecPlanNote.ForeColor = System.Drawing.Color.Red;
			this.textSecPlanNote.Location = new System.Drawing.Point(526, 307);
			this.textSecPlanNote.Multiline = true;
			this.textSecPlanNote.Name = "textSecPlanNote";
			this.textSecPlanNote.ReadOnly = true;
			this.textSecPlanNote.Size = new System.Drawing.Size(267, 129);
			this.textSecPlanNote.TabIndex = 10;
			this.textSecPlanNote.Text = "";
			this.textSecPlanNote.DoubleClick += new System.EventHandler(this.textSecPlanNote_DoubleClick);
			// 
			// butPat
			// 
			this.butPat.Image = ((System.Drawing.Image)(resources.GetObject("butPat.Image")));
			this.butPat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPat.Location = new System.Drawing.Point(2, 3);
			this.butPat.Name = "butPat";
			this.butPat.Size = new System.Drawing.Size(134, 26);
			this.butPat.TabIndex = 11;
			this.butPat.Text = "Select Patient";
			this.butPat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butPat.Click += new System.EventHandler(this.butPat_Click);
			// 
			// butDeletePt
			// 
			this.butDeletePt.Image = ((System.Drawing.Image)(resources.GetObject("butDeletePt.Image")));
			this.butDeletePt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDeletePt.Location = new System.Drawing.Point(104, 0);
			this.butDeletePt.Name = "butDeletePt";
			this.butDeletePt.Size = new System.Drawing.Size(100, 26);
			this.butDeletePt.TabIndex = 13;
			this.butDeletePt.Text = "Delete";
			this.butDeletePt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butDeletePt.Click += new System.EventHandler(this.butDeletePt_Click);
			// 
			// butAddPt
			// 
			this.butAddPt.Image = ((System.Drawing.Image)(resources.GetObject("butAddPt.Image")));
			this.butAddPt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddPt.Location = new System.Drawing.Point(0, 0);
			this.butAddPt.Name = "butAddPt";
			this.butAddPt.Size = new System.Drawing.Size(100, 26);
			this.butAddPt.TabIndex = 12;
			this.butAddPt.Text = "Add";
			this.butAddPt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butAddPt.Click += new System.EventHandler(this.butAddPt_Click);
			// 
			// panelFamily
			// 
			this.panelFamily.Controls.Add(this.butMovePat);
			this.panelFamily.Controls.Add(this.butSetGuar);
			this.panelFamily.Controls.Add(this.butDeletePt);
			this.panelFamily.Controls.Add(this.butAddPt);
			this.panelFamily.Controls.Add(this.tbFamily);
			this.panelFamily.Location = new System.Drawing.Point(471, 466);
			this.panelFamily.Name = "panelFamily";
			this.panelFamily.Size = new System.Drawing.Size(413, 129);
			this.panelFamily.TabIndex = 15;
			// 
			// butMovePat
			// 
			this.butMovePat.Image = ((System.Drawing.Image)(resources.GetObject("butMovePat.Image")));
			this.butMovePat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butMovePat.Location = new System.Drawing.Point(309, 0);
			this.butMovePat.Name = "butMovePat";
			this.butMovePat.Size = new System.Drawing.Size(100, 26);
			this.butMovePat.TabIndex = 15;
			this.butMovePat.Text = "Move";
			this.butMovePat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butMovePat.Click += new System.EventHandler(this.butMovePat_Click);
			// 
			// butSetGuar
			// 
			this.butSetGuar.Image = ((System.Drawing.Image)(resources.GetObject("butSetGuar.Image")));
			this.butSetGuar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butSetGuar.Location = new System.Drawing.Point(207, 0);
			this.butSetGuar.Name = "butSetGuar";
			this.butSetGuar.Size = new System.Drawing.Size(100, 26);
			this.butSetGuar.TabIndex = 14;
			this.butSetGuar.Text = "Guarantor";
			this.butSetGuar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butSetGuar.Click += new System.EventHandler(this.butSetGuar_Click);
			// 
			// tbPercent1
			// 
			this.tbPercent1.BackColor = System.Drawing.SystemColors.Window;
			this.tbPercent1.Location = new System.Drawing.Point(254, 167);
			this.tbPercent1.Name = "tbPercent1";
			this.tbPercent1.SelectedIndices = new int[0];
			this.tbPercent1.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbPercent1.Size = new System.Drawing.Size(272, 86);
			this.tbPercent1.TabIndex = 16;
			// 
			// tbPercent2
			// 
			this.tbPercent2.BackColor = System.Drawing.SystemColors.Window;
			this.tbPercent2.Location = new System.Drawing.Point(525, 167);
			this.tbPercent2.Name = "tbPercent2";
			this.tbPercent2.SelectedIndices = new int[0];
			this.tbPercent2.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbPercent2.Size = new System.Drawing.Size(269, 86);
			this.tbPercent2.TabIndex = 17;
			// 
			// butOutlook
			// 
			this.butOutlook.Image = ((System.Drawing.Image)(resources.GetObject("butOutlook.Image")));
			this.butOutlook.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butOutlook.Location = new System.Drawing.Point(140, 3);
			this.butOutlook.Name = "butOutlook";
			this.butOutlook.Size = new System.Drawing.Size(84, 26);
			this.butOutlook.TabIndex = 18;
			this.butOutlook.Text = "Outlook";
			this.butOutlook.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butOutlook.Click += new System.EventHandler(this.butOutlook_Click);
			// 
			// ContrFamily
			// 
			this.Controls.Add(this.butOutlook);
			this.Controls.Add(this.tbPercent1);
			this.Controls.Add(this.tbPercent2);
			this.Controls.Add(this.panelFamily);
			this.Controls.Add(this.butPat);
			this.Controls.Add(this.textSecPlanNote);
			this.Controls.Add(this.textPriPlanNote);
			this.Controls.Add(this.tbCoverage);
			this.Controls.Add(this.textAddrNotes);
			this.Controls.Add(this.tbPatient);
			this.Controls.Add(this.tbPlans);
			this.Name = "ContrFamily";
			this.Size = new System.Drawing.Size(939, 772);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ContrFamily_Layout);
			this.panelFamily.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		///<summary></summary>
		public void ModuleSelected(){
			RefreshModuleData();
			RefreshModuleScreen();
		}

		///<summary></summary>
		public void ModuleUnselected(){
			Patients.FamilyList=null;
			InsPlans.List=null;
			CovPats.List=null;
			RefAttaches.List=null;
			RefAttaches.HList=null;
		}

		private void RefreshModuleData(){
			if(Patients.PatIsLoaded){
				Patients.GetFamily(Patients.Cur.PatNum);
				InsPlans.Refresh();
				CovPats.Refresh();
				RefAttaches.Refresh();
			}
		}

		private void RefreshModuleScreen(){
			if(Patients.PatIsLoaded){
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString+" - "+Patients.GetCurNameLF();
				tbPatient.Enabled=true;
				tbCoverage.Enabled=true;
				//panelPlans.Enabled=true;
				panelFamily.Enabled=true;              
			}
			else{
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString;
				tbPatient.Enabled=false;
				tbCoverage.Enabled=false;
				//panelPlans.Enabled=false;
				panelFamily.Enabled=false;
				Patients.Cur=new Patient();
			}
			butOutlook.Visible=Programs.IsEnabled("Outlook");
			FillPatientData();
			FillFamilyData();
			FillPlanData();
			FillCoverageData();
		} 

		///<summary></summary>
		public void InstantClasses(){
			tbPatient.InstantClasses();
			tbCoverage.InstantClasses();
			tbPlans.InstantClasses();
			tbFamily.InstantClasses();
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.butPat,
				this.butAddPt,
				this.butDeletePt,
				this.butSetGuar,
				this.butMovePat,
				this.panelFamily,
			});
		}

		private void ContrFamily_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
			tbPlans.LayoutTables();
		}		

		private void butPat_Click(object sender, System.EventArgs e) {
			FormPatientSelect formSelectPatient2=new FormPatientSelect();
			formSelectPatient2.ShowDialog();
			if (formSelectPatient2.DialogResult==DialogResult.OK){
				ModuleSelected();
			}
		}

		private void butOutlook_Click(object sender, System.EventArgs e) {
			/*Process[] procsOutlook = Process.GetProcessesByName("outlook");
			if(procsOutlook.Length==0){
				try{
					Process.Start("Outlook");
				}
				catch{}
			}*/
		}

		#region tbPatient

		private void tbPatient_CellDoubleClicked(object sender, CellEventArgs e){
			FormPatientEdit FormPatientEdit2=new FormPatientEdit();
			FormPatientEdit2.IsNew=false;
			FormPatientEdit2.ShowDialog();
			ModuleSelected();
		}

		private void textAddrNotes_DoubleClick(object sender, System.EventArgs e) {
			FormPatientEdit FormPatientEdit2=new FormPatientEdit();
			FormPatientEdit2.IsNew=false;
			FormPatientEdit2.ShowDialog();
			ModuleSelected();
		}

		private void FillPatientData(){
			tbPatient.Cell[1,1]=Patients.Cur.LName;
			tbPatient.Cell[1,2]=Patients.Cur.FName;
			tbPatient.Cell[1,3]=Patients.Cur.MiddleI;
			tbPatient.Cell[1,4]=Patients.Cur.Preferred;
			tbPatient.Cell[1,5]=Patients.Cur.Salutation;
			tbPatient.Cell[1,6]=Lan.g("enum PatientStatus",Patients.Cur.PatStatus.ToString());
			tbPatient.Cell[1,7]=Lan.g("enum PatientGender",Patients.Cur.Gender.ToString());
			tbPatient.Cell[1,8]=Lan.g("enum PatientPosition",Patients.Cur.Position.ToString());
			//switch (Patients.Cur.PatStatus){
			//	case PatientStatus.Patient : tbPatient.Cell[1,6]=Lan.g("enum PatientStatus","Patient"); break;
			//	case PatientStatus.NonPatient : tbPatient.Cell[1,6]=Lan.g("enum PatientStatus","Non-patient"); break;
			//	case PatientStatus.Inactive : tbPatient.Cell[1,6]=Lan.g("enum PatientStatus","Inactive"); break;
			//	case PatientStatus.Archived : tbPatient.Cell[1,6]=Lan.g("enum PatientStatus","Archived"); break;}
			/*switch (Patients.Cur.Gender){
				case PatientGender.Male : tbPatient.Cell[1,7]=Lan.g("enum PatientGender","Male"); break;
				case PatientGender.Female : tbPatient.Cell[1,7]=Lan.g("enum PatientGender","Female"); break;
				case PatientGender.Unknown: tbPatient.Cell[1,7]=Lan.g("enum PatientGender","Unknown"); break;}
			switch (Patients.Cur.Position){
				case PatientPosition.Single : tbPatient.Cell[1,8]=Lan.g("enum PatientPosition","Single"); break;
				case PatientPosition.Married : tbPatient.Cell[1,8]=Lan.g("enum PatientPosition","Married"); break;
				case PatientPosition.Child : tbPatient.Cell[1,8]=Lan.g("enum PatientPosition","Child"); break;}*/
			if(Patients.Cur.Birthdate.Year < 1880)
				tbPatient.Cell[1,9]="";
			else
				tbPatient.Cell[1,9]=Patients.Cur.Birthdate.ToString("d");
			tbPatient.Cell[1,10]=Patients.Cur.Age;
			if(Patients.Cur.SSN !=null && Patients.Cur.SSN.Length==9)
				tbPatient.Cell[1,11]=Patients.Cur.SSN.Substring(0,3)+"-"
					+Patients.Cur.SSN.Substring(3,2)+"-"+Patients.Cur.SSN.Substring(5,4);
			else tbPatient.Cell[1,11]=Patients.Cur.SSN;
			tbPatient.Cell[1,12]=Patients.Cur.Address;
			tbPatient.Cell[1,13]=Patients.Cur.Address2;
			tbPatient.Cell[1,14]=Patients.Cur.City;
			tbPatient.Cell[1,15]=Patients.Cur.State;
			tbPatient.Cell[1,16]=Patients.Cur.Zip;
			tbPatient.Cell[1,17]=Patients.Cur.HmPhone;
			tbPatient.Cell[1,18]=Patients.Cur.WkPhone;
			tbPatient.Cell[1,19]=Patients.Cur.WirelessPhone;
			tbPatient.Cell[1,20]=Patients.Cur.Email;
			tbPatient.Cell[1,21]=Patients.Cur.CreditType;
			tbPatient.Cell[1,22]=Patients.Cur.RecallInterval.ToString();
			tbPatient.Cell[1,23]=Patients.Cur.ChartNumber;
			tbPatient.Cell[1,24]=Defs.GetName(DefCat.BillingTypes,Patients.Cur.BillingType);
			tbPatient.Cell[1,25]="";
			if(Patients.PatIsLoaded){
				for(int i=0;i<RefAttaches.List.Length;i++){
					if(RefAttaches.List[i].IsFrom){
						Referrals.GetCur(RefAttaches.List[i].ReferralNum);
						tbPatient.Cell[1,25]=Referrals.Cur.LName+", "+Referrals.Cur.FName+" "+Referrals.Cur.MName;
						break;
					}				
				}
			}			
			if(Patients.PatIsLoaded){
				textAddrNotes.Text=Patients.Cur.AddrNote;
			}
			else{
				textAddrNotes.Text="";
				tbPatient.Cell[1,6]="";
				tbPatient.Cell[1,7]="";
				tbPatient.Cell[1,8]="";
				tbPatient.Cell[1,9]="";
			}
			tbPatient.Refresh();
		}

		#endregion tbPatient 

		#region tbPlans
		private void FillPlanData(){
			if(!Patients.PatIsLoaded){
				tbPlans.ResetRows(0);
				tbPlans.LayoutTables();
				return;
			}
			//InsPlans.Refresh();
			tbPlans.ResetRows(InsPlans.List.Length);
			tbPlans.SetGridColor(Color.Gray);
			tbPlans.SetBackGColor(Color.White);
			for(int i=0;i<InsPlans.List.Length;i++){
				tbPlans.Cell[0,i]=(i+1).ToString();
				tbPlans.Cell[1,i]=Patients.GetNameInFamLF(InsPlans.List[i].Subscriber);
				if(tbPlans.Cell[1,i]==""){//subscriber from another family
					Patients.GetLim(InsPlans.List[i].Subscriber);
					tbPlans.Cell[1,i]=Patients.LimName;
				}
				tbPlans.Cell[2,i]=InsPlans.List[i].Carrier;
				if(InsPlans.List[i].DateEffective.Year<1880)
					tbPlans.Cell[3,i]="";
				else
					tbPlans.Cell[3,i]=InsPlans.List[i].DateEffective.ToString("d");
				if(InsPlans.List[i].DateTerm.Year<1880)
					tbPlans.Cell[4,i]="";
				else
					tbPlans.Cell[4,i]=InsPlans.List[i].DateTerm.ToString("d");
				//tbPlans.Cell[5,i]=InsPlans.List[i].PlanNote;
			}
			tbPlans.LayoutTables();
			
			
		}

		/*private void butAddInsPlan_Click(object sender, System.EventArgs e) {//obsolete
			FormInsTemplates FormInsTemplates = new FormInsTemplates();
			//FormInsTemplates.IsPrimary
			FormInsTemplates.ShowDialog();
			ModuleSelected();
		}*/

		/*private void butDeletePlan_Click(object sender, System.EventArgs e) {
			if(InsPlans.Selected==-1){
				MessageBox.Show(Lan.g(this,"Please select a plan first."));
				return;
			}
			if (MessageBox.Show(Lan.g(this,"Delete Plan?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
				return;
			InsPlans.DeleteSelected();//checks dependencies first
			InsPlans.Selected=-1;
			ModuleSelected();
		}*/

		

		private void tbPlans_CellClicked(object sender, CellEventArgs e){
			/*if(InsPlans.Selected==e.Row){
				tbPlans.ColorRow(e.Row,Color.White);
				InsPlans.Selected=-1;
			}
			else{
				if(InsPlans.Selected!=-1){
					tbPlans.ColorRow(InsPlans.Selected,Color.White);
				}
				tbPlans.ColorRow(e.Row,Color.LightGray);
				InsPlans.Selected=e.Row;
			}*/
		}

		private void tbPlans_CellDoubleClicked(object sender, CellEventArgs e){
			FormInsPlan FormInsPLan = new FormInsPlan();
			InsPlans.Cur=InsPlans.List[e.Row];
			FormInsPLan.ShowDialog();
			if(FormInsPLan.DialogResult!=DialogResult.OK){
				return;
			}	
			ModuleSelected();
		}

		

		#endregion

		#region tbFamily

		private void FillFamilyData(){
			if (!Patients.PatIsLoaded){
				tbFamily.SelectedRow=-1;
				tbFamily.ResetRows(0);
				tbFamily.LayoutTables();
				return;
			}
			tbFamily.ResetRows(Patients.FamilyList.Length);
			tbFamily.SetGridColor(Color.Gray);
			tbFamily.SetBackGColor(Color.White);
			for (int i=0;i<Patients.FamilyList.Length; i++){
				if (Patients.FamilyList[i].PatNum==Patients.FamilyList[i].Guarantor){
					for(int j=0;j<5;j++){
						tbFamily.FontBold[j,i]=true;
					}
					//tbFamily.Cell[0,i]=Lan.g(this,"Guar");
				}
				tbFamily.Cell[0,i]=Patients.GetNameInFamLFI(i);
				tbFamily.Cell[1,i]=Lan.g("enum PatientPosition",Patients.FamilyList[i].Position.ToString());
				tbFamily.Cell[2,i]=Lan.g("enum PatientGender",Patients.FamilyList[i].Gender.ToString());
				tbFamily.Cell[3,i]=Lan.g("enum PatientStatus",Patients.FamilyList[i].PatStatus.ToString());
				tbFamily.Cell[4,i]=Patients.FamilyList[i].Age;
				if (Patients.FamilyList[i].PatNum==Patients.Cur.PatNum){
					tbFamily.SelectedRow=i;
					tbFamily.ColorRow(i,Color.DarkSalmon);
				}
			}//end for
			tbFamily.LayoutTables();
		}//end FillFamilyData

		private void tbFamily_CellClicked(object sender, CellEventArgs e){
			if (tbFamily.SelectedRow != -1){
				tbFamily.ColorRow(tbFamily.SelectedRow,Color.White);
			}
			tbFamily.SelectedRow=e.Row;
			tbFamily.ColorRow(e.Row,Color.DarkSalmon);
			Patients.Cur=Patients.FamilyList[e.Row];
			ModuleSelected();
		}

		private void butAddPt_Click(object sender, System.EventArgs e) {
			Patient tempPat=Patients.Cur;
			Patients.Cur=new Patient();
			Patients.Cur.LName=tempPat.LName;
			Patients.Cur.PatStatus=PatientStatus.Patient;
			Patients.Cur.Address=tempPat.Address;
			Patients.Cur.Address2=tempPat.Address2;
			Patients.Cur.City=tempPat.City;
			Patients.Cur.State=tempPat.State;
			Patients.Cur.Zip=tempPat.Zip;
			Patients.Cur.HmPhone=tempPat.HmPhone;
			Patients.Cur.Guarantor=tempPat.Guarantor;
			Patients.Cur.CreditType=tempPat.CreditType;
			Patients.Cur.PriProv=tempPat.PriProv;
			Patients.Cur.SecProv=tempPat.SecProv;
			Patients.Cur.FeeSched=tempPat.FeeSched;
			Patients.Cur.BillingType=tempPat.BillingType;
			Patients.Cur.RecallInterval=6;
			Patients.Cur.AddrNote=tempPat.AddrNote;
			FormPatientEdit FormPE=new FormPatientEdit();
			FormPE.IsNew=true;
			FormPE.ShowDialog();
			if(FormPE.DialogResult!=DialogResult.OK){
				Patients.Cur=tempPat;
			}
			ModuleSelected();
		}

		private void butDeletePt_Click(object sender, System.EventArgs e) {
			//this doesn't actually delete the patient, just changes their status
			//and they will never show again in the patient selection list.
			//later: check for plans, appointments, procedures, etc.  Would this be intrusive?
			if(Patients.Cur.EstBalance!=0){
				MessageBox.Show(Lan.g(this,"You can not delete a patient with a balance."));
				return;
			}
			Patient tempPat=Patients.Cur;
			if(Patients.Cur.PatNum==Patients.Cur.Guarantor){//if selecting guarantor
				if(Patients.FamilyList.Length==1){
					if(MessageBox.Show(Lan.g(this,"Delete Patient?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
						return;
					Patients.Cur.PatStatus=PatientStatus.Deleted;
					Patients.UpdateCur();
					Patients.PatIsLoaded=false;
					//does not delete notes or plans, etc.
				}
				else{
					MessageBox.Show(Lan.g(this,"You can not delete the guarantor if there are other family members. You would have to make a different family member the guarantor first."));
				}
			}
			else{//not selecting guarantor
				if(MessageBox.Show(Lan.g(this,"Delete Patient?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
						return;
				Patients.Cur.PatStatus=PatientStatus.Deleted;
				Patients.Cur.Guarantor=Patients.Cur.PatNum;
				Patients.UpdateCur();
				Patients.Cur.PatNum=tempPat.Guarantor;
			}
			ModuleSelected();
		}

		private void butSetGuar_Click(object sender,System.EventArgs e){     
			Patient tempPat=Patients.Cur;
			if(Patients.Cur.PatNum==Patients.Cur.Guarantor){
				MessageBox.Show(Lan.g(this
					,"Patient is already the guarantor.  Please select a different family member."));
			}
			else{
				if(MessageBox.Show(Lan.g(this,"Make the selected patient the guarantor?")
					,"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
						return;
				Patients.ChangeGuarantorToCur();
			}
			ModuleSelected();
		}

		private void butMovePat_Click(object sender, System.EventArgs e) {
			Patient tempPat=Patients.Cur;
			if(Patients.Cur.PatNum==Patients.Cur.Guarantor){//if guarantor selected
				if(Patients.FamilyList.Length==1){//and no other family members
					//no need to check insurance.  It will follow.
					if(MessageBox.Show(Lan.g(this,"Moving the guarantor will cause two families to be combined.  The financial notes for both families will be combined and may need to be edited.  The address notes will also be combined and may need to be edited. Do you wish to continue?")
						,"",MessageBoxButtons.OKCancel)!=DialogResult.OK) return;
					if(MessageBox.Show(Lan.g(this
						,"Select the family to move this patient to from the list that will come up next.")
						,"",MessageBoxButtons.OKCancel)
						!=DialogResult.OK) return;
					Patient patTemp=Patients.Cur;
					FormPatientSelect FormSP=new FormPatientSelect();
					FormSP.OnlyChangingFam=true;
					FormSP.ShowDialog();
					if(FormSP.DialogResult!=DialogResult.OK){
						Patients.Cur=patTemp;
						return;
					}
					Patients.GetLim(Patients.Cur.PatNum);
					Patients.Cur=patTemp;
					Patients.Cur.Guarantor=Patients.Lim.Guarantor;
					Patients.UpdateCur();
					Patients.GetFamily(Patients.Cur.PatNum);
					Patients.CombineGuarantors();
				}
				else{//there are other family members
					MessageBox.Show(Lan.g(this,"You can not move the guarantor.  If you wish to move the guarantor, you must make another family member the guarantor first."));
				}
			}
			else{//guarantor not selected
				//if(InsPlans.HasDependencies(Patients.Cur.PatNum)){
				//	return;
				//}
				if(MessageBox.Show(Lan.g(this,"Preparing to move family member.  Financial notes and address notes will not be transferred.  Proceed to next step?")
					,"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;
				}
				switch(MessageBox.Show(Lan.g(this,"Create new family instead of moving to an existing family?")
					,"",MessageBoxButtons.YesNoCancel)){
					case DialogResult.Cancel:
						return;
					case DialogResult.Yes://new family
						Patients.Cur.Guarantor=Patients.Cur.PatNum;
						Patients.UpdateCur();
						break;
					case DialogResult.No://move to an existing family
						if(MessageBox.Show(Lan.g(this
							,"Select the family to move this patient to from the list that will come up next.")
							,"",MessageBoxButtons.OKCancel) !=DialogResult.OK) return;
						Patient patTemp=Patients.Cur;
						FormPatientSelect FormSP=new FormPatientSelect();
						FormSP.OnlyChangingFam=true;
						FormSP.ShowDialog();
						if(FormSP.DialogResult!=DialogResult.OK){
							Patients.Cur=patTemp;
							return;
						}
						Patients.GetLim(Patients.Cur.PatNum);
						Patients.Cur=patTemp;
						Patients.Cur.Guarantor=Patients.Lim.Guarantor;
						Patients.UpdateCur();
						break;
				}//end switch
			}//end guarantor not selected
			ModuleSelected();
		}

		#endregion
		
		#region tbCoverage

		private void tbCoverage_CellDoubleClicked(object sender, CellEventArgs e){
			if(e.Row>=0 && e.Row<=2){
				OpenCovEdit();
			}
			else{
				if(e.Col==0 || e.Col==1){
					OpenPriPlanEdit();
				}
				else if(e.Col==2 || e.Col==3){
					OpenSecPlanEdit();
				}
			}
			ModuleSelected();
		}

		private void textPriPlanNote_DoubleClick(object sender, System.EventArgs e) {
			OpenPriPlanEdit();
		}

		private void textSecPlanNote_DoubleClick(object sender, System.EventArgs e) {
			OpenSecPlanEdit();
		}

		private void tbPercent1_CellDoubleClicked(object sender, CellEventArgs e){
			OpenPriPlanEdit();
		}
		
		private void tbPercent2_CellDoubleClicked(object sender, CellEventArgs e){
			OpenSecPlanEdit();
		}

		private void OpenPriPlanEdit() {
			if(Patients.Cur.PriPlanNum==0){
				return;
			}
			FormInsPlan FormP = new FormInsPlan();
			for(int i=0;i<InsPlans.List.Length;i++){
				if(InsPlans.List[i].PlanNum==Patients.Cur.PriPlanNum){
					InsPlans.Cur=InsPlans.List[i];
				}
			}
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK){
				return;
			}
			ModuleSelected();
		}

		private void OpenSecPlanEdit() {
			if(Patients.Cur.SecPlanNum==0){
				return;
			}
			FormInsPlan FormP = new FormInsPlan();
			for(int i=0;i<InsPlans.List.Length;i++){
				if(InsPlans.List[i].PlanNum==Patients.Cur.SecPlanNum){
					InsPlans.Cur=InsPlans.List[i];
				}
			}
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK){
				return;
			}
			ModuleSelected();
		}

		private void OpenCovEdit(){
			FormInsCovEdit FormInsCovEdit2=new FormInsCovEdit();
			FormInsCovEdit2.ShowDialog();
			ModuleSelected();
		}

		private void FillCoverageData(){
			Color covColor=//Color.FromArgb(212,232,199);
				Defs.Long[(int)DefCat.MiscColors][0].ItemColor; //Color.FromName("Highlight");//
			for(int i=0;i<3;i++){
				tbCoverage.SetBackColorRow(i,covColor);
			}
			if(!Patients.PatIsLoaded){
				for(int i=1;i<16;i++){
					tbCoverage.Cell[1,i]="";
					tbCoverage.Cell[3,i]="";
				}
				tbCoverage.Refresh();
				textPriPlanNote.Text="";
				tbPercent1.ResetRows(6);
				tbPercent1.LayoutTables();
				tbPercent2.ResetRows(6);
				tbPercent2.LayoutTables();
				return;
			}
			//CovPats CovPats=new CovPats();
			tbPercent1.ResetRows(CovCats.ListShort.Length);
			tbPercent1.SetGridColor(Color.LightGray);
			for(int i=0;i<CovCats.ListShort.Length;i++){
				tbPercent1.Cell[0,i]=CovCats.ListShort[i].Description;
				if(CovPats.PriList[i]==-1)
					tbPercent1.Cell[1,i]="";
				else
					tbPercent1.Cell[1,i]=CovPats.PriList[i].ToString();
			}
			tbPercent1.LayoutTables();
			tbPercent2.ResetRows(CovCats.ListShort.Length);
			tbPercent2.SetGridColor(Color.LightGray);
			for(int i=0;i<CovCats.ListShort.Length;i++){
				tbPercent2.Cell[0,i]=CovCats.ListShort[i].Description;
				if(CovPats.SecList[i]==-1)
					tbPercent2.Cell[1,i]="";
				else
					tbPercent2.Cell[1,i]=CovPats.SecList[i].ToString();
			}
			tbPercent2.LayoutTables();
			
			if(Patients.Cur.PriPlanNum==0){
				for(int i=1;i<17;i++){
					tbCoverage.Cell[1,i]="";
				}
				textPriPlanNote.Text="";
			}
			else{
				for(int i=0;i<InsPlans.List.Length;i++){
					if(InsPlans.List[i].PlanNum==Patients.Cur.PriPlanNum){
						InsPlans.Cur=InsPlans.List[i];
					}
				}
				tbCoverage.Cell[1,1]=InsPlans.GetDescript(Patients.Cur.PriPlanNum);
				tbCoverage.Cell[1,2]=Patients.Cur.PriRelationship.ToString();
				if(InsPlans.Cur.AnnualMax==-1)
					tbCoverage.Cell[1,4]="";
				else
					tbCoverage.Cell[1,4]=InsPlans.Cur.AnnualMax.ToString();
				if(InsPlans.Cur.OrthoMax==-1)
					tbCoverage.Cell[1,5]="";
				else
					tbCoverage.Cell[1,5]=InsPlans.Cur.OrthoMax.ToString();
				if(InsPlans.Cur.RenewMonth==-1)
					tbCoverage.Cell[1,6]="";
				else
					tbCoverage.Cell[1,6]=InsPlans.Cur.RenewMonth.ToString();
				if(InsPlans.Cur.Deductible==-1)
					tbCoverage.Cell[1,7]="";
				else
					tbCoverage.Cell[1,7]=InsPlans.Cur.Deductible.ToString();
				if(InsPlans.Cur.DeductWaivPrev==YN.Unknown)
					tbCoverage.Cell[1,8]="";
				else
					tbCoverage.Cell[1,8]=InsPlans.Cur.DeductWaivPrev.ToString();
				if(InsPlans.Cur.FloToAge==-1)
					tbCoverage.Cell[1,14]="";
				else
					tbCoverage.Cell[1,14]=InsPlans.Cur.FloToAge.ToString();
				if(InsPlans.Cur.MissToothExcl==YN.Unknown)
					tbCoverage.Cell[1,15]="";
				else
					tbCoverage.Cell[1,15]=InsPlans.Cur.MissToothExcl.ToString();
				if(InsPlans.Cur.MajorWait==YN.Unknown)
					tbCoverage.Cell[1,16]="";
				else
					tbCoverage.Cell[1,16]=InsPlans.Cur.MajorWait.ToString();
				textPriPlanNote.Text=InsPlans.Cur.PlanNote;
			}
			if(Patients.Cur.SecPlanNum==0){
				for(int i=1;i<17;i++){
					tbCoverage.Cell[3,i]="";
				}
				textSecPlanNote.Text="";
			}
			else{
				for(int i=0;i<InsPlans.List.Length;i++){
					if(InsPlans.List[i].PlanNum==Patients.Cur.SecPlanNum){
						InsPlans.Cur=InsPlans.List[i];
					}
				}
				tbCoverage.Cell[3,1]=InsPlans.GetDescript(Patients.Cur.SecPlanNum);
				tbCoverage.Cell[3,2]=Patients.Cur.SecRelationship.ToString();
				if(InsPlans.Cur.AnnualMax==-1)
					tbCoverage.Cell[3,4]="";
				else
					tbCoverage.Cell[3,4]=InsPlans.Cur.AnnualMax.ToString();
				if(InsPlans.Cur.OrthoMax==-1)
					tbCoverage.Cell[3,5]="";
				else
					tbCoverage.Cell[3,5]=InsPlans.Cur.OrthoMax.ToString();
				if(InsPlans.Cur.RenewMonth==-1)
					tbCoverage.Cell[3,6]="";
				else
					tbCoverage.Cell[3,6]=InsPlans.Cur.RenewMonth.ToString();
				if(InsPlans.Cur.Deductible==-1)
					tbCoverage.Cell[3,7]="";
				else
					tbCoverage.Cell[3,7]=InsPlans.Cur.Deductible.ToString();
				if(InsPlans.Cur.DeductWaivPrev==YN.Unknown)
					tbCoverage.Cell[3,8]="";
				else
					tbCoverage.Cell[3,8]=InsPlans.Cur.DeductWaivPrev.ToString();
				if(InsPlans.Cur.FloToAge==-1)
					tbCoverage.Cell[3,14]="";
				else
					tbCoverage.Cell[3,14]=InsPlans.Cur.FloToAge.ToString();
				if(InsPlans.Cur.MissToothExcl==YN.Unknown)
					tbCoverage.Cell[3,15]="";
				else
					tbCoverage.Cell[3,15]=InsPlans.Cur.MissToothExcl.ToString();
				if(InsPlans.Cur.MajorWait==YN.Unknown)
					tbCoverage.Cell[3,16]="";
				else
					tbCoverage.Cell[3,16]=InsPlans.Cur.MajorWait.ToString();
				//MessageBox.Show(InsPlans.Cur.PlanNum.ToString());
				textSecPlanNote.Text=InsPlans.Cur.PlanNote;
			}
			tbCoverage.Refresh();
		}

		

		#endregion tbCoverage

		

		
	}
}
