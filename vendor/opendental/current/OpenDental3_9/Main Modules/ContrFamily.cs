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
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;

namespace OpenDental{

	///<summary></summary>
	public class ContrFamily : System.Windows.Forms.UserControl{
		private System.Windows.Forms.ImageList imageListToolBar;
		private System.ComponentModel.IContainer components;
		private OpenDental.TableInsPlans tbPlans;
		private OpenDental.TablePatient tbPatient;
		private OpenDental.TableFamily tbFamily;
		private System.Windows.Forms.TextBox textAddrNotes;
		private OpenDental.UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.ContextMenu menuPatient;
		///<summary>All recalls for this entire family.</summary>
		private Recall[] RecallList;
		///<summary></summary>
		[Category("Data"),Description("Occurs when user changes current patient, usually by clicking on the Select Patient button.")]
		public event PatientSelectedEventHandler PatientSelected=null;
		private Patient PatCur;
		private Family FamCur;
		private OpenDental.UI.PictureBox picturePat;
		private InsPlan[] PlanList;
		private OpenDental.UI.ODGrid gridIns;
		private PatPlan[] PatPlanList;

		///<summary></summary>
		public ContrFamily(){
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
			tbPlans.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbPlans_CellDoubleClicked);
			tbFamily.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbFamily_CellClicked);
			tbPatient.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbPatient_CellDoubleClicked);
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
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.menuPatient = new System.Windows.Forms.ContextMenu();
			this.picturePat = new OpenDental.UI.PictureBox();
			this.gridIns = new OpenDental.UI.ODGrid();
			this.SuspendLayout();
			// 
			// imageListToolBar
			// 
			this.imageListToolBar.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageListToolBar.ImageSize = new System.Drawing.Size(22, 22);
			this.imageListToolBar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListToolBar.ImageStream")));
			this.imageListToolBar.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tbPlans
			// 
			this.tbPlans.BackColor = System.Drawing.SystemColors.Window;
			this.tbPlans.Location = new System.Drawing.Point(1, 604);
			this.tbPlans.Name = "tbPlans";
			this.tbPlans.ScrollValue = 1;
			this.tbPlans.SelectedIndices = new int[0];
			this.tbPlans.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbPlans.Size = new System.Drawing.Size(459, 100);
			this.tbPlans.TabIndex = 1;
			// 
			// tbPatient
			// 
			this.tbPatient.BackColor = System.Drawing.SystemColors.Window;
			this.tbPatient.Location = new System.Drawing.Point(0, 133);
			this.tbPatient.Name = "tbPatient";
			this.tbPatient.ScrollValue = 150;
			this.tbPatient.SelectedIndices = new int[0];
			this.tbPatient.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbPatient.Size = new System.Drawing.Size(252, 417);
			this.tbPatient.TabIndex = 2;
			// 
			// textAddrNotes
			// 
			this.textAddrNotes.BackColor = System.Drawing.Color.White;
			this.textAddrNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textAddrNotes.ForeColor = System.Drawing.Color.Red;
			this.textAddrNotes.Location = new System.Drawing.Point(2, 489);
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
			this.tbFamily.Location = new System.Drawing.Point(104, 31);
			this.tbFamily.Name = "tbFamily";
			this.tbFamily.ScrollValue = 1;
			this.tbFamily.SelectedIndices = new int[0];
			this.tbFamily.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbFamily.Size = new System.Drawing.Size(489, 100);
			this.tbFamily.TabIndex = 7;
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListToolBar;
			this.ToolBarMain.Location = new System.Drawing.Point(0, 0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(939, 29);
			this.ToolBarMain.TabIndex = 19;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// picturePat
			// 
			this.picturePat.Location = new System.Drawing.Point(1, 31);
			this.picturePat.Name = "picturePat";
			this.picturePat.Size = new System.Drawing.Size(100, 100);
			this.picturePat.TabIndex = 28;
			this.picturePat.Text = "picturePat";
			this.picturePat.TextNullImage = "Patient Picture Unavailable";
			// 
			// gridIns
			// 
			this.gridIns.Columns.Add(new OpenDental.UI.ODGridColumn("", 120, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridIns.Columns.Add(new OpenDental.UI.ODGridColumn("Primary", 150, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridIns.Columns.Add(new OpenDental.UI.ODGridColumn("Secondary", 150, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridIns.Columns.Add(new OpenDental.UI.ODGridColumn("Medical", 150, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridIns.HScrollVisible = true;
			this.gridIns.Location = new System.Drawing.Point(256, 133);
			this.gridIns.Name = "gridIns";
			this.gridIns.ScrollValue = 0;
			this.gridIns.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.gridIns.Size = new System.Drawing.Size(607, 417);
			this.gridIns.TabIndex = 29;
			this.gridIns.Title = "Insurance Plans";
			this.gridIns.TranslationName = "TableCoverage";
			this.gridIns.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridIns_CellDoubleClick);
			// 
			// ContrFamily
			// 
			this.Controls.Add(this.gridIns);
			this.Controls.Add(this.picturePat);
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.textAddrNotes);
			this.Controls.Add(this.tbPatient);
			this.Controls.Add(this.tbPlans);
			this.Controls.Add(this.tbFamily);
			this.Name = "ContrFamily";
			this.Size = new System.Drawing.Size(939, 708);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ContrFamily_Layout);
			this.ResumeLayout(false);

		}
		#endregion

		///<summary></summary>
		public void ModuleSelected(int patNum){
			RefreshModuleData(patNum);
			RefreshModuleScreen();
		}

		///<summary></summary>
		public void ModuleUnselected(){
			FamCur=null;
			PlanList=null;
		}

		private void RefreshModuleData(int patNum){
			if(patNum==0){
				PatCur=null;
				FamCur=null;
				PatPlanList=new PatPlan[0]; 
				return;
			}
			FamCur=Patients.GetFamily(patNum);
			PatCur=FamCur.GetPatient(patNum);
			PlanList=InsPlans.Refresh(FamCur);
			PatPlanList=PatPlans.Refresh(patNum);
			CovPats.Refresh(PlanList,PatPlanList);
			//RefAttaches.Refresh();
			RecallList=Recalls.GetList(FamCur.List);
		}

		private void RefreshModuleScreen(){
			if(PatCur!=null){
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString+" - "
					+PatCur.GetNameLF();
				tbPatient.Enabled=true;
				gridIns.Enabled=true;
				ToolBarMain.Buttons["Recall"].Enabled=true;
				ToolBarMain.Buttons["Add"].Enabled=true;
				ToolBarMain.Buttons["Delete"].Enabled=true;
				ToolBarMain.Buttons["Guarantor"].Enabled=true;
				ToolBarMain.Buttons["Move"].Enabled=true;
				ToolBarMain.Invalidate();
			}
			else{
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString;
				tbPatient.Enabled=false;
				gridIns.Enabled=false;
				ToolBarMain.Buttons["Recall"].Enabled=false;
				ToolBarMain.Buttons["Add"].Enabled=false;
				ToolBarMain.Buttons["Delete"].Enabled=false;
				ToolBarMain.Buttons["Guarantor"].Enabled=false;
				ToolBarMain.Buttons["Move"].Enabled=false;
				ToolBarMain.Invalidate();
				//Patients.Cur=new Patient();
			}
			FillPatientButton();
			FillPatientPicture();
			FillPatientData();
			FillFamilyData();
			FillPlanData();
			FillInsData();
		} 

		private void FillPatientButton(){
			Patients.AddPatsToMenu(menuPatient,new EventHandler(menuPatient_Click),PatCur,FamCur);
		}

		private void FillPatientPicture(){
			picturePat.TextNullImage=Lan.g(this,"Patient Picture Unavailable");
			if(PatCur==null){
				picturePat.Image=null;
				return;
			}
			string fileName=Documents.GetPatPict(PatCur.PatNum);
			if(fileName==""){
				picturePat.Image=null;
				return;
			}
			string fullName=((Pref)Prefs.HList["DocPath"]).ValueString
				+PatCur.ImageFolder.Substring(0,1)+@"\"
				+PatCur.ImageFolder+@"\"
				+fileName;
			if(!File.Exists(fullName)){
				picturePat.Image=null;
				return;
			}
			try{
				picturePat.Image=Image.FromFile(fullName);
			}
			catch{
				picturePat.Image=null;
			}
		}

		private void menuPatient_Click(object sender,System.EventArgs e) {
			int newPatNum=Patients.ButtonSelect(menuPatient,sender,FamCur);
			OnPatientSelected(newPatNum);
			ModuleSelected(newPatNum);
		}

		///<summary></summary>
		public void InstantClasses(){
			tbPatient.InstantClasses();
			tbPlans.InstantClasses();
			tbFamily.InstantClasses();
			//cannot use Lan.F(this);
			Lan.C(this,new Control[]
				{
				//butPatEdit,
				//butEditPriCov,
				//butEditPriPlan,
				//butEditSecCov,
				//butEditSecPlan
				});
			LayoutToolBar();
		}

		///<summary>Causes the toolbar to be laid out again.</summary>
		public void LayoutToolBar(){
			ToolBarMain.Buttons.Clear();
			ODToolBarButton button;
			button=new ODToolBarButton(Lan.g(this,"Select Patient"),0,"","Patient");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=menuPatient;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Recall"),1,"","Recall"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			button=new ODToolBarButton(Lan.g(this,"Family Members:"),-1,"","");
			button.Style=ODToolBarButtonStyle.Label;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Add"),2,"Add Family Member","Add"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Delete"),3,Lan.g(this,"Delete Family Member"),"Delete"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Set Guarantor"),4,Lan.g(this,"Set as Guarantor"),"Guarantor"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Move"),5,Lan.g(this,"Move to Another Family"),"Move"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Add Insurance"),-1,"","Ins"));
			ArrayList toolButItems=ToolButItems.GetForToolBar(ToolBarsAvail.FamilyModule);
			for(int i=0;i<toolButItems.Count;i++){
				ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
				ToolBarMain.Buttons.Add(new ODToolBarButton(((ToolButItem)toolButItems[i]).ButtonText
					,-1,"",((ToolButItem)toolButItems[i]).ProgramNum));
			}
			ToolBarMain.Invalidate();
		}

		private void ContrFamily_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
			tbPlans.LayoutTables();
		}		

		//private void butOutlook_Click(object sender, System.EventArgs e) {
			/*Process[] procsOutlook = Process.GetProcessesByName("outlook");
			if(procsOutlook.Length==0){
				try{
					Process.Start("Outlook");
				}
				catch{}
			}*/
		//}

		private void ToolBarMain_ButtonClick(object sender, OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			if(e.Button.Tag.GetType()==typeof(string)){
				//standard predefined button
				switch(e.Button.Tag.ToString()){
					case "Patient":
						OnPat_Click();
						break;
					case "Recall":
						OnRecall_Click();
						break;
					case "Add":
						OnAdd_Click();
						break;
					case "Delete":
						OnDelete_Click();
						break;
					case "Guarantor":
						OnGuarantor_Click();
						break;
					case "Move":
						OnMove_Click();
						break;
					case "Ins":
						ToolButIns_Click();
						break;
				}
			}
			else if(e.Button.Tag.GetType()==typeof(int)){
				Programs.Execute((int)e.Button.Tag,PatCur);
			}
		}

		private void OnPat_Click() {
			FormPatientSelect formPS=new FormPatientSelect();
			formPS.ShowDialog();
			if(formPS.DialogResult==DialogResult.OK){
				OnPatientSelected(formPS.SelectedPatNum);
				ModuleSelected(formPS.SelectedPatNum);
			}
		}

		///<summary></summary>
		private void OnPatientSelected(int patNum){
			PatientSelectedEventArgs eArgs=new OpenDental.PatientSelectedEventArgs(patNum);
			if(PatientSelected!=null)
				PatientSelected(this,eArgs);
		}

		private void OnRecall_Click() {
			//patient may or may not have an existing recall.
			Recall recallCur=null;
			for(int i=0;i<RecallList.Length;i++){
				if(RecallList[i].PatNum==PatCur.PatNum){
					recallCur=RecallList[i];
				}
			}
			//for testing purposes and because synchronization might have bugs, always synch here:
			//This might add a recall.
			Recalls.Synch(PatCur.PatNum,recallCur);			
			FormRecallEdit FormRE=new FormRecallEdit(PatCur);
			if(recallCur==null){
				recallCur=new Recall();
				recallCur.PatNum=PatCur.PatNum;
				recallCur.RecallInterval=new Interval(0,0,6,0);
				FormRE.IsNew=true;
			}
			FormRE.RecallCur=recallCur;
			FormRE.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		#region tbPatient

		private void tbPatient_CellDoubleClicked(object sender, CellEventArgs e){
			FormPatientEdit FormPatientEdit2=new FormPatientEdit(PatCur,FamCur);
			FormPatientEdit2.IsNew=false;
			FormPatientEdit2.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void textAddrNotes_DoubleClick(object sender, System.EventArgs e) {
			FormPatientEdit FormPatientEdit2=new FormPatientEdit(PatCur,FamCur);
			FormPatientEdit2.IsNew=false;
			FormPatientEdit2.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void FillPatientData(){
			if(PatCur==null){
				//butPatEdit.Enabled=false;
				return;
			}
			//butPatEdit.Enabled=true;
			tbPatient.Cell[1,1]=PatCur.LName;
			tbPatient.Cell[1,2]=PatCur.FName;
			tbPatient.Cell[1,3]=PatCur.MiddleI;
			tbPatient.Cell[1,4]=PatCur.Preferred;
			tbPatient.Cell[1,5]=PatCur.Salutation;
			tbPatient.Cell[1,6]=Lan.g("enum PatientStatus",PatCur.PatStatus.ToString());
			if(PatCur.PatStatus==PatientStatus.Deceased){
				tbPatient.FontColor[1,6]=Color.Red;
			}
			else{
				tbPatient.FontColor[1,6]=Color.Black;
			}
			tbPatient.Cell[1,7]=Lan.g("enum PatientGender",PatCur.Gender.ToString());
			tbPatient.Cell[1,8]=Lan.g("enum PatientPosition",PatCur.Position.ToString());
			if(PatCur.Birthdate.Year < 1880)
				tbPatient.Cell[1,9]="";
			else
				tbPatient.Cell[1,9]=PatCur.Birthdate.ToString("d");
			tbPatient.Cell[1,10]=Shared.AgeToString(PatCur.Age);
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="US"){
				if(PatCur.SSN !=null && PatCur.SSN.Length==9)
					tbPatient.Cell[1,11]=PatCur.SSN.Substring(0,3)+"-"
						+PatCur.SSN.Substring(3,2)+"-"+PatCur.SSN.Substring(5,4);
				else tbPatient.Cell[1,11]=PatCur.SSN;
			}
			else{
				tbPatient.Cell[1,11]=PatCur.SSN;
			}
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="CA"){
				tbPatient.Cell[0,11]="SIN";
				tbPatient.Cell[0,16]="Postal Code";
				tbPatient.Cell[0,15]="Province";
			}
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="GB"){
				tbPatient.Cell[0,11]="";
				tbPatient.Cell[0,16]="Postcode";
				tbPatient.Cell[0,15]="";
			}
			tbPatient.Cell[1,12]=PatCur.Address;
			tbPatient.Cell[1,13]=PatCur.Address2;
			tbPatient.Cell[1,14]=PatCur.City;
			tbPatient.Cell[1,15]=PatCur.State;
			tbPatient.Cell[1,16]=PatCur.Zip;
			tbPatient.Cell[1,17]=PatCur.HmPhone;
			tbPatient.Cell[1,18]=PatCur.WkPhone;
			tbPatient.Cell[1,19]=PatCur.WirelessPhone;
			tbPatient.Cell[1,20]=PatCur.Email;
			tbPatient.Cell[1,21]=PatCur.CreditType;
			//tbPatient.Cell[1,22]=PatCur.RecallInterval.ToString();
			tbPatient.Cell[1,22]=PatCur.ChartNumber;
			tbPatient.Cell[1,23]=Defs.GetName(DefCat.BillingTypes,PatCur.BillingType);
			tbPatient.Cell[1,24]="";
			if(PatCur!=null){
				//butPatEdit.Enabled=true;
				RefAttach[] RefList=RefAttaches.Refresh(PatCur.PatNum);
				for(int i=0;i<RefList.Length;i++){
					if(RefList[i].IsFrom){
						tbPatient.Cell[1,24]=Referrals.GetReferral(RefList[i].ReferralNum).GetName();
						break;
					}				
				}
				textAddrNotes.Text=PatCur.AddrNote;
			}
			else{
				//butPatEdit.Enabled=false;
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
			if(PatCur==null){
				tbPlans.ResetRows(0);
				tbPlans.LayoutTables();
				return;
			}
			//InsPlans.Refresh();
			tbPlans.ResetRows(PlanList.Length);
			tbPlans.SetGridColor(Color.Gray);
			tbPlans.SetBackGColor(Color.White);
			for(int i=0;i<PlanList.Length;i++){
				tbPlans.Cell[0,i]=(i+1).ToString();
				tbPlans.Cell[1,i]=FamCur.GetNameInFamLF(PlanList[i].Subscriber);
				if(tbPlans.Cell[1,i]==""){//subscriber from another family
					tbPlans.Cell[1,i]=Patients.GetLim(PlanList[i].Subscriber).GetNameLF();
				}
				tbPlans.Cell[2,i]=Carriers.GetName(PlanList[i].CarrierNum);
				if(PlanList[i].DateEffective.Year<1880)
					tbPlans.Cell[3,i]="";
				else
					tbPlans.Cell[3,i]=PlanList[i].DateEffective.ToString("d");
				if(PlanList[i].DateTerm.Year<1880)
					tbPlans.Cell[4,i]="";
				else
					tbPlans.Cell[4,i]=PlanList[i].DateTerm.ToString("d");
				//tbPlans.Cell[5,i]=PlanList[i].PlanNote;
			}
			tbPlans.LayoutTables();
			
			
		}

		private void tbPlans_CellDoubleClicked(object sender, CellEventArgs e){
			FormInsPlan FormIP=new FormInsPlan(PlanList[e.Row],null);
			FormIP.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		#endregion

		#region tbFamily

		private void FillFamilyData(){
			if(PatCur==null){
				tbFamily.SelectedRow=-1;
				tbFamily.ResetRows(0);
				tbFamily.LayoutTables();
				return;
			}
			tbFamily.ResetRows(FamCur.List.Length);
			tbFamily.SetGridColor(Color.Gray);
			tbFamily.SetBackGColor(Color.White);
			for(int i=0;i<FamCur.List.Length; i++){
				if(FamCur.List[i].PatNum==FamCur.List[i].Guarantor){
					for(int j=0;j<5;j++){
						tbFamily.FontBold[j,i]=true;
					}
					//tbFamily.Cell[0,i]=Lan.g(this,"Guar");
				}
				tbFamily.Cell[0,i]=FamCur.GetNameInFamLFI(i);
				tbFamily.Cell[1,i]=Lan.g("enumPatientPosition",FamCur.List[i].Position.ToString());
				tbFamily.Cell[2,i]=Lan.g("enumPatientGender",FamCur.List[i].Gender.ToString());
				tbFamily.Cell[3,i]=Lan.g("enumPatientStatus",FamCur.List[i].PatStatus.ToString());
				tbFamily.Cell[4,i]=Shared.AgeToString(FamCur.List[i].Age);
				for(int j=0;j<RecallList.Length;j++){
					if(RecallList[j].PatNum==FamCur.List[i].PatNum){
						if(RecallList[j].DateDue.Year>1880){
							tbFamily.Cell[5,i]=RecallList[j].DateDue.ToShortDateString();
						}
					}
				}
				if(FamCur.List[i].PatNum==PatCur.PatNum){
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
			OnPatientSelected(FamCur.List[e.Row].PatNum);
			ModuleSelected(FamCur.List[e.Row].PatNum);
		}

		//private void butAddPt_Click(object sender, System.EventArgs e) {
		private void OnAdd_Click(){
			Patient tempPat=new Patient();
			tempPat.LName      =PatCur.LName;
			tempPat.PatStatus  =PatientStatus.Patient;
			tempPat.Address    =PatCur.Address;
			tempPat.Address2   =PatCur.Address2;
			tempPat.City       =PatCur.City;
			tempPat.State      =PatCur.State;
			tempPat.Zip        =PatCur.Zip;
			tempPat.HmPhone    =PatCur.HmPhone;
			tempPat.Guarantor  =PatCur.Guarantor;
			tempPat.CreditType =PatCur.CreditType;
			tempPat.PriProv    =PatCur.PriProv;
			tempPat.SecProv    =PatCur.SecProv;
			tempPat.FeeSched   =PatCur.FeeSched;
			tempPat.BillingType=PatCur.BillingType;
			tempPat.AddrNote   =PatCur.AddrNote;
			tempPat.Insert(false);
			FormPatientEdit FormPE=new FormPatientEdit(tempPat,FamCur);
			FormPE.IsNew=true;
			FormPE.ShowDialog();
			if(FormPE.DialogResult==DialogResult.OK){
				ModuleSelected(tempPat.PatNum);
			}
			else{
				ModuleSelected(PatCur.PatNum);
			}
		}

		//private void butDeletePt_Click(object sender, System.EventArgs e) {
		private void OnDelete_Click(){
			//this doesn't actually delete the patient, just changes their status
			//and they will never show again in the patient selection list.
			//check for plans, appointments, procedures, etc.
			Procedure[] procList=Procedures.Refresh(PatCur.PatNum);
			Claims.Refresh(PatCur.PatNum);
			Adjustment[] AdjustmentList=Adjustments.Refresh(PatCur.PatNum);
			PaySplit[] PaySplitList=PaySplits.Refresh(PatCur.PatNum);//
			ClaimProc[] claimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			Commlogs.Refresh(PatCur.PatNum);
			PayPlan[] payPlanList=PayPlans.Refresh(PatCur.Guarantor,PatCur.PatNum);
			InsPlan[] planList=InsPlans.Refresh(FamCur);
			PatPlanList=PatPlans.Refresh(PatCur.PatNum);
			CovPats.Refresh(planList,PatPlanList);
			RefAttach[] RefAttachList=RefAttaches.Refresh(PatCur.PatNum);
			bool hasProcs=procList.Length>0;
			bool hasClaims=Claims.List.Length>0;
			bool hasAdj=AdjustmentList.Length>0;
			bool hasPay=PaySplitList.Length>0;
			bool hasClaimProcs=claimProcList.Length>0;
			bool hasComm=Commlogs.List.Length>0;
			bool hasPayPlans=payPlanList.Length>0;
			bool hasInsPlans=false;
			for(int i=0;i<planList.Length;i++){
				if(planList[i].Subscriber==PatCur.PatNum){
					hasInsPlans=true;
				}
			}
			bool hasRef=RefAttachList.Length>0;
			if(hasProcs || hasClaims || hasAdj || hasPay || hasClaimProcs || hasComm || hasPayPlans
				|| hasInsPlans || hasRef)
			{
				string message=Lan.g(this,
					"You cannot delete this patient without first deleting the following data:")+"\r";
				if(hasProcs)
					message+=Lan.g(this,"Procedures")+"\r";
				if(hasClaims)
					message+=Lan.g(this,"Claims")+"\r";
				if(hasAdj)
					message+=Lan.g(this,"Adjustments")+"\r";
				if(hasPay)
					message+=Lan.g(this,"Payments")+"\r";
				if(hasClaimProcs)
					message+=Lan.g(this,"Procedures attached to claims")+"\r";
				if(hasComm)
					message+=Lan.g(this,"Commlog entries")+"\r";
				if(hasPayPlans)
					message+=Lan.g(this,"Payment plans")+"\r";
				if(hasInsPlans)
					message+=Lan.g(this,"Insurance plans")+"\r";
				if(hasRef)
					message+=Lan.g(this,"References")+"\r";
				MessageBox.Show(message);
				return;
			}
			Patient PatOld=PatCur.Copy();
			if(PatCur.PatNum==PatCur.Guarantor){//if selecting guarantor
				if(FamCur.List.Length==1){
					if(!MsgBox.Show(this,true,"Delete Patient?"))
						return;
					PatCur.PatStatus=PatientStatus.Deleted;
					PatCur.ChartNumber="";
					PatCur.Update(PatOld);
					for(int i=0;i<RecallList.Length;i++){
						if(RecallList[i].PatNum==PatCur.PatNum){
							RecallList[i].IsDisabled=true;
							RecallList[i].DateDue=DateTime.MinValue;
							RecallList[i].Update();
						}
					}
					ModuleSelected(0);
					//does not delete notes or plans, etc.
				}
				else{
					MessageBox.Show(Lan.g(this,"You cannot delete the guarantor if there are other family members. You would have to make a different family member the guarantor first."));
				}
			}
			else{//not selecting guarantor
				if(!MsgBox.Show(this,true,"Delete Patient?"))
					return;
				PatCur.PatStatus=PatientStatus.Deleted;
				PatCur.ChartNumber="";
				PatCur.Guarantor=PatCur.PatNum;
				PatCur.Update(PatOld);
				for(int i=0;i<RecallList.Length;i++){
					if(RecallList[i].PatNum==PatCur.PatNum){
						RecallList[i].IsDisabled=true;
						RecallList[i].DateDue=DateTime.MinValue;
						RecallList[i].Update();
					}
				}
				ModuleSelected(PatOld.Guarantor);
			}
		}

		//private void butSetGuar_Click(object sender,System.EventArgs e){
    private void OnGuarantor_Click(){
			//Patient tempPat=PatCur;
			if(PatCur.PatNum==PatCur.Guarantor){
				MessageBox.Show(Lan.g(this
					,"Patient is already the guarantor.  Please select a different family member."));
			}
			else{
				if(MessageBox.Show(Lan.g(this,"Make the selected patient the guarantor?")
					,"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
						return;
				Patients.ChangeGuarantorToCur(FamCur,PatCur);
			}
			ModuleSelected(PatCur.PatNum);
		}

		//private void butMovePat_Click(object sender, System.EventArgs e) {
		private void OnMove_Click(){
			Patient PatOld=PatCur.Copy();
			//Patient PatCur;
			if(PatCur.PatNum==PatCur.Guarantor){//if guarantor selected
				if(FamCur.List.Length==1){//and no other family members
					//no need to check insurance.  It will follow.
					if(!MsgBox.Show(this,true,"Moving the guarantor will cause two families to be combined.  The financial notes for both families will be combined and may need to be edited.  The address notes will also be combined and may need to be edited. Do you wish to continue?"))
						return;
					if(!MsgBox.Show(this,true,"Select the family to move this patient to from the list that will come up next."))
						return;
					FormPatientSelect FormPS=new FormPatientSelect();
					FormPS.SelectionModeOnly=true;
					FormPS.ShowDialog();
					if(FormPS.DialogResult!=DialogResult.OK){
						return;
					}
					Patient Lim=Patients.GetLim(FormPS.SelectedPatNum);
					PatCur.Guarantor=Lim.Guarantor;
					PatCur.Update(PatOld);
					FamCur=Patients.GetFamily(PatCur.PatNum);
					Patients.CombineGuarantors(FamCur,PatCur);
				}
				else{//there are other family members
					MessageBox.Show(Lan.g(this,"You cannot move the guarantor.  If you wish to move the guarantor, you must make another family member the guarantor first."));
				}
			}
			else{//guarantor not selected
				if(!MsgBox.Show(this,true,"Preparing to move family member.  Financial notes and address notes will not be transferred.  Proceed to next step?")){
					return;
				}
				switch(MessageBox.Show(Lan.g(this,"Create new family instead of moving to an existing family?")
					,"",MessageBoxButtons.YesNoCancel)){
					case DialogResult.Cancel:
						return;
					case DialogResult.Yes://new family
						PatCur.Guarantor=PatCur.PatNum;
						PatCur.Update(PatOld);
						break;
					case DialogResult.No://move to an existing family
						if(!MsgBox.Show(this,true,"Select the family to move this patient to from the list that will come up next.")){
							return;
						}
						FormPatientSelect FormPS=new FormPatientSelect();
						FormPS.SelectionModeOnly=true;
						FormPS.ShowDialog();
						if(FormPS.DialogResult!=DialogResult.OK){
							return;
						}
						Patient Lim=Patients.GetLim(FormPS.SelectedPatNum);
						PatCur.Guarantor=Lim.Guarantor;
						PatCur.Update(PatOld);
						break;
				}//end switch
			}//end guarantor not selected
			ModuleSelected(PatCur.PatNum);
		}

		#endregion
		
		#region gridIns
		private void ToolButIns_Click(){
			DialogResult result=MessageBox.Show(Lan.g(this,"Is this patient the subscriber?"),"",MessageBoxButtons.YesNoCancel);
			if(result==DialogResult.Cancel){
				return;
			}
			InsPlan plan;
			bool planIsNew=false;
			if(result==DialogResult.Yes){//current patient is subscriber
				plan=new InsPlan();
				//Optional: pick template
				if(Prefs.GetBool("InsurancePlansShared")){
					FormInsPlans FormIP=new FormInsPlans();
					FormIP.IsSelectMode=true;
					FormIP.ShowDialog();
					if(FormIP.DialogResult!=DialogResult.OK){
						return;
					}
					plan.EmployerNum    =FormIP.SelectedPlan.EmployerNum;
					plan.GroupName      =FormIP.SelectedPlan.GroupName;
					plan.GroupNum       =FormIP.SelectedPlan.GroupNum;
					plan.DivisionNo     =FormIP.SelectedPlan.DivisionNo;
					plan.CarrierNum     =FormIP.SelectedPlan.CarrierNum;
					plan.PlanType       =FormIP.SelectedPlan.PlanType;
					plan.UseAltCode     =FormIP.SelectedPlan.UseAltCode;
					plan.IsMedical      =FormIP.SelectedPlan.IsMedical;
					plan.ClaimsUseUCR   =FormIP.SelectedPlan.ClaimsUseUCR;
					plan.FeeSched       =FormIP.SelectedPlan.FeeSched;
					plan.CopayFeeSched  =FormIP.SelectedPlan.CopayFeeSched;
					plan.ClaimFormNum   =FormIP.SelectedPlan.ClaimFormNum;
					plan.AllowedFeeSched=FormIP.SelectedPlan.AllowedFeeSched;
				}
				else{
					//plan.PlanType="";
					plan.EmployerNum=PatCur.EmployerNum;
				}
				plan.Subscriber=PatCur.PatNum;
				plan.SubscriberID=PatCur.SSN;
				plan.AnnualMax=-1;//blank
				plan.OrthoMax=-1;
				plan.RenewMonth=1;
				plan.Deductible=-1;
				plan.FloToAge=-1;
				plan.ReleaseInfo=true;
				plan.AssignBen=true;
				plan.Insert();
				//Then attach plan
				PatPlan patplan=new PatPlan();
				patplan.Ordinal=PatPlanList.Length+1;//so the ordinal of the first entry will be 1, NOT 0.
				patplan.PatNum=PatCur.PatNum;
				patplan.PlanNum=plan.PlanNum;
				patplan.Relationship=Relat.Self;
				patplan.Insert();
				//Then, display insPlanEdit to user
				FormInsPlan FormI=new FormInsPlan(plan,patplan);
				FormI.IsNewPlan=true;
				FormI.IsNewPatPlan=true;
				FormI.ShowDialog();//this updates estimates also.
				//if cancel, then plan and patplan are deleted from within that dialog.
				ModuleSelected(PatCur.PatNum);
			}
			if(result==DialogResult.No){//patient is not subscriber
				//show list of patients in this family
				FormSubscriberSelect FormS=new FormSubscriberSelect(FamCur);
				FormS.ShowDialog();
				if(FormS.DialogResult==DialogResult.Cancel){
					return;
				}
				//patient was selected, so show list of plans for this subscriber
				Patient subscriber=Patients.GetPat(FormS.SelectedPatNum);
				FormInsSelectSubscr FormISS=new FormInsSelectSubscr(subscriber.PatNum);
				FormISS.ShowDialog();
				if(FormISS.DialogResult==DialogResult.Cancel){
					return;
				}
				if(FormISS.SelectedPlanNum==0){//'New' option selected.
					plan=new InsPlan();
					//Optional: pick template
					if(Prefs.GetBool("InsurancePlansShared")){
						FormInsPlans FormIP=new FormInsPlans();
						FormIP.IsSelectMode=true;
						FormIP.ShowDialog();
						if(FormIP.DialogResult!=DialogResult.OK){
							return;
						}
						plan.EmployerNum    =FormIP.SelectedPlan.EmployerNum;
						plan.GroupName      =FormIP.SelectedPlan.GroupName;
						plan.GroupNum       =FormIP.SelectedPlan.GroupNum;
						plan.DivisionNo     =FormIP.SelectedPlan.DivisionNo;
						plan.CarrierNum     =FormIP.SelectedPlan.CarrierNum;
						plan.PlanType       =FormIP.SelectedPlan.PlanType;
						plan.UseAltCode     =FormIP.SelectedPlan.UseAltCode;
						plan.IsMedical      =FormIP.SelectedPlan.IsMedical;
						plan.ClaimsUseUCR   =FormIP.SelectedPlan.ClaimsUseUCR;
						plan.FeeSched       =FormIP.SelectedPlan.FeeSched;
						plan.CopayFeeSched  =FormIP.SelectedPlan.CopayFeeSched;
						plan.ClaimFormNum   =FormIP.SelectedPlan.ClaimFormNum;
						plan.AllowedFeeSched=FormIP.SelectedPlan.AllowedFeeSched;
					}
					plan.Subscriber=subscriber.PatNum;
					plan.SubscriberID=subscriber.SSN;
					plan.AnnualMax=-1;//blank
					plan.OrthoMax=-1;
					plan.RenewMonth=1;
					plan.Deductible=-1;
					plan.FloToAge=-1;
					plan.ReleaseInfo=true;
					plan.AssignBen=true;
					plan.Insert();
					planIsNew=true;
				}//new
				else{
					plan=InsPlans.GetPlan(FormISS.SelectedPlanNum,PlanList);
				}
				//Then attach plan
				PatPlan patplan=new PatPlan();
				patplan.Ordinal=PatPlanList.Length+1;//so the ordinal of the first entry will be 1, NOT 0.
				patplan.PatNum=PatCur.PatNum;
				patplan.PlanNum=plan.PlanNum;
				patplan.Relationship=Relat.Self;
				patplan.Insert();
				//Then, display insPlanEdit to user
				FormInsPlan FormI=new FormInsPlan(plan,patplan);
				FormI.IsNewPlan=planIsNew;
				FormI.IsNewPatPlan=true;
				FormI.ShowDialog();//this updates estimates also.
				//if cancel, then plan and patplan are deleted from within that dialog.
				ModuleSelected(PatCur.PatNum);
			}//patient not subscriber
		}

		private void FillInsData(){
			InsPlan[] planArray=new InsPlan[PatPlanList.Length];//prevents repeated calls to db.
			for(int i=0;i<PatPlanList.Length;i++){
				planArray[i]=InsPlans.GetPlan(PatPlanList[i].PlanNum,PlanList);
			}
			gridIns.BeginUpdate();
			gridIns.Columns.Clear();
			gridIns.Rows.Clear();
			OpenDental.UI.ODGridColumn col;
			col=new ODGridColumn("",120);
			gridIns.Columns.Add(col);
			for(int i=0;i<PatPlanList.Length;i++){
				if(planArray[i].IsMedical){
					col=new ODGridColumn(Lan.g("TableCoverage","Medical"),170);
					gridIns.Columns.Add(col);
				}
				else if(i==0){
					col=new ODGridColumn(Lan.g("TableCoverage","Primary"),170);
					gridIns.Columns.Add(col);
				}
				else if(i==1){
					col=new ODGridColumn(Lan.g("TableCoverage","Secondary"),170);
					gridIns.Columns.Add(col);
				}
				else{
					col=new ODGridColumn(Lan.g("TableCoverage","Other"),170);
					gridIns.Columns.Add(col);
				}
			}
			OpenDental.UI.ODGridRow row=new ODGridRow();
			//carrier
			row.Cells.Add(Lan.g("TableCoverage","Carrier"));
			for(int i=0;i<PatPlanList.Length;i++){
				row.Cells.Add(InsPlans.GetCarrierName(PatPlanList[i].PlanNum,planArray));
			}
			row.ColorBackG=Defs.Long[(int)DefCat.MiscColors][0].ItemColor;
			gridIns.Rows.Add(row);
			//subscriber
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Subscriber"));
			for(int i=0;i<PatPlanList.Length;i++){
				row.Cells.Add(FamCur.GetNameInFamFL(planArray[i].Subscriber));
			}
			row.ColorBackG=Defs.Long[(int)DefCat.MiscColors][0].ItemColor;
			gridIns.Rows.Add(row);
			//relationship
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Rel'ship to Sub"));
			for(int i=0;i<PatPlanList.Length;i++){
				row.Cells.Add(Lan.g("enumRelat",PatPlanList[i].Relationship.ToString()));
			}
			row.ColorBackG=Defs.Long[(int)DefCat.MiscColors][0].ItemColor;
			gridIns.Rows.Add(row);
			//patient ID
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Patient ID"));
			for(int i=0;i<PatPlanList.Length;i++){
				row.Cells.Add(PatPlanList[i].PatID);
			}
			row.ColorBackG=Defs.Long[(int)DefCat.MiscColors][0].ItemColor;
			gridIns.Rows.Add(row);
			//pending
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Pending"));
			for(int i=0;i<PatPlanList.Length;i++){
				if(PatPlanList[i].IsPending){
					row.Cells.Add("X");
				}
				else{
					row.Cells.Add("");
				}
			}
			row.ColorBackG=Defs.Long[(int)DefCat.MiscColors][0].ItemColor;
			row.ColorLborder=Color.Black;
			gridIns.Rows.Add(row);
			//annual max
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Annual Max $"));
			for(int i=0;i<PatPlanList.Length;i++){
				if(planArray[i].AnnualMax==-1){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(planArray[i].AnnualMax.ToString());
				}
			}
			gridIns.Rows.Add(row);
			//ortho max
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Ortho Max $"));
			for(int i=0;i<PatPlanList.Length;i++){
				if(planArray[i].OrthoMax==-1){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(planArray[i].OrthoMax.ToString());
				}
			}
			gridIns.Rows.Add(row);
			//renewal month
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Renewal Month"));
			for(int i=0;i<PatPlanList.Length;i++){
				row.Cells.Add((new DateTime(2000,planArray[i].RenewMonth,1)).ToString("MMMM"));
			}
			gridIns.Rows.Add(row);
			//deductible
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Deductible $"));
			for(int i=0;i<PatPlanList.Length;i++){
				if(planArray[i].Deductible==-1){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(planArray[i].Deductible.ToString());
				}
			}
			gridIns.Rows.Add(row);
			//deductible waived on preventive
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Waived on Prev?"));
			for(int i=0;i<PatPlanList.Length;i++){
				if(planArray[i].DeductWaivPrev==YN.Unknown){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(Lan.g("enumYN",planArray[i].DeductWaivPrev.ToString()));
				}
			}
			row.ColorLborder=Color.Black;
			gridIns.Rows.Add(row);
			//percentages
			for(int c=0;c<CovCats.ListShort.Length;c++){
				row=new ODGridRow();
				row.Cells.Add(CovCats.ListShort[c].Description);
				for(int i=0;i<PatPlanList.Length;i++){
					if(i==0){//primary
						if(CovPats.PriList[c]==-1)
							row.Cells.Add("");
						else
							row.Cells.Add(CovPats.PriList[c].ToString());
					}
					else if(i==1){//secondary
						if(CovPats.SecList[c]==-1)
							row.Cells.Add("");
						else
							row.Cells.Add(CovPats.SecList[c].ToString());
					}
					else{//we don't support percentages very well yet other than primary and secondary
						row.Cells.Add("");
					}
				}
				if(c==CovCats.ListShort.Length-1){
					row.ColorLborder=Color.Black;
				}
				gridIns.Rows.Add(row);
			}
			//Fluoride
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Flo to Age:"));
			for(int i=0;i<PatPlanList.Length;i++){
				if(planArray[i].FloToAge==-1){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(planArray[i].FloToAge.ToString());
				}
			}
			gridIns.Rows.Add(row);
			//Missing tooth exclusion
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Miss Tooth Excl?"));
			for(int i=0;i<PatPlanList.Length;i++){
				if(planArray[i].MissToothExcl==YN.Unknown){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(Lan.g("enumYN",planArray[i].MissToothExcl.ToString()));
				}
			}
			gridIns.Rows.Add(row);
			//Wait on major
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Wait on Major?"));
			for(int i=0;i<PatPlanList.Length;i++){
				if(planArray[i].MajorWait==YN.Unknown){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(Lan.g("enumYN",planArray[i].MajorWait.ToString()));
				}
			}
			gridIns.Rows.Add(row);
			//note
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Ins Plan Note"));
			OpenDental.UI.ODGridCell cell;
			for(int i=0;i<PatPlanList.Length;i++){
				cell=new ODGridCell();
				cell.Text=planArray[i].PlanNote;
				cell.ColorText=Color.Red;
				cell.Bold=YN.Yes;
				row.Cells.Add(cell);
			}
			gridIns.Rows.Add(row);
			gridIns.EndUpdate();
		}

		private void gridIns_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			if(e.Col==0){
				return;
			}
			Cursor=Cursors.WaitCursor;
			PatPlan patPlan=PatPlanList[e.Col-1];
			InsPlan insPlan=InsPlans.GetPlan(patPlan.PlanNum,PlanList);
			FormInsPlan FormIP=new FormInsPlan(insPlan,patPlan);
			FormIP.ShowDialog();
			Cursor=Cursors.Default;
			ModuleSelected(PatCur.PatNum);
		}

		#endregion gridIns



		

		

		

		

		
		

		

		
	}
}
