using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace OpenDental{
	///<summary></summary>
	public class FormClaimPrint : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butClose;
		private System.Windows.Forms.PrintPreviewControl Preview2;
		private System.Drawing.Printing.PrintDocument pd2;
		private System.Windows.Forms.Button butPrint;
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public int ThisClaimNum;
		///<summary></summary>
		public int ThisPatNum;
		///<summary></summary>
		public bool PrintBlank;
		private System.Windows.Forms.PrintDialog printDialog2;
		///<summary></summary>
		public bool PrintImmediately;
		///<summary></summary>
		public static string PrinterName;
    private string[] displayStrings;
		private ArrayList claimprocs;

		///<summary></summary>
		public FormClaimPrint(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butClose,
				butPrint,
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
			this.butClose = new System.Windows.Forms.Button();
			this.Preview2 = new System.Windows.Forms.PrintPreviewControl();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.butPrint = new System.Windows.Forms.Button();
			this.printDialog2 = new System.Windows.Forms.PrintDialog();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClose.Location = new System.Drawing.Point(770, 768);
			this.butClose.Name = "butClose";
			this.butClose.TabIndex = 0;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// Preview2
			// 
			this.Preview2.AutoZoom = false;
			this.Preview2.Location = new System.Drawing.Point(0, 0);
			this.Preview2.Name = "Preview2";
			this.Preview2.Size = new System.Drawing.Size(738, 798);
			this.Preview2.TabIndex = 1;
			this.Preview2.Zoom = 1;
			// 
			// butPrint
			// 
			this.butPrint.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butPrint.Location = new System.Drawing.Point(768, 728);
			this.butPrint.Name = "butPrint";
			this.butPrint.TabIndex = 2;
			this.butPrint.Text = "Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// FormClaimPrint
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(860, 816);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.Preview2);
			this.Controls.Add(this.butClose);
			this.Name = "FormClaimPrint";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FormClaimPrint";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormClaimPrint_Load);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.FormClaimPrint_Layout);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormClaimPrint_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
			Preview2.Height=ClientRectangle.Height;
			Preview2.Width=ClientRectangle.Width-160;
			butClose.Location=new Point(ClientRectangle.Width-100,ClientRectangle.Height-70);
			butPrint.Location=new Point(ClientRectangle.Width-100,ClientRectangle.Height-140);
		}
		private void FormClaimPrint_Load(object sender, System.EventArgs e) {
			if(PrinterSettings.InstalledPrinters.Count==0){
				MessageBox.Show(Lan.g(this,"No printer installed"));
				return;
			}
			pd2 = new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			Preview2.Document=pd2;//display document
		}

		///<summary></summary>
		public bool PrintImmediate(){
			pd2 = new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			try{
				pd2.PrinterSettings.PrinterName=PrinterName;
				pd2.Print();
			}
			catch{
				return false;
			}
			//if(PrintClaim()) return true;//printed successfully
			return true;
		}

		private void pd2_PrintPage(object sender, PrintPageEventArgs ev){//raised for each page to be printed.
			FillDisplayStrings();
			Graphics grfx=ev.Graphics;
			float xPosText;
			for(int i=0;i<ClaimFormItems.ListForForm.Length;i++){
				if(ClaimFormItems.ListForForm[i].ImageFileName==""){//field
					xPosText=ClaimFormItems.ListForForm[i].XPos;
					if(ClaimFormItems.ListForForm[i].FieldName=="P1Fee"
						|| ClaimFormItems.ListForForm[i].FieldName=="P2Fee"
						|| ClaimFormItems.ListForForm[i].FieldName=="P3Fee"
						|| ClaimFormItems.ListForForm[i].FieldName=="P4Fee"
						|| ClaimFormItems.ListForForm[i].FieldName=="P5Fee"
						|| ClaimFormItems.ListForForm[i].FieldName=="P6Fee"
						|| ClaimFormItems.ListForForm[i].FieldName=="P7Fee"
						|| ClaimFormItems.ListForForm[i].FieldName=="P8Fee"
						|| ClaimFormItems.ListForForm[i].FieldName=="P9Fee"
						|| ClaimFormItems.ListForForm[i].FieldName=="P10Fee"
						|| ClaimFormItems.ListForForm[i].FieldName=="TotalFee")
					{
						//this aligns it to the right
						xPosText-=grfx.MeasureString(displayStrings[i]
							,new Font(ClaimForms.Cur.FontName,ClaimForms.Cur.FontSize)).Width;
					}
					grfx.DrawString(displayStrings[i]
						,new Font(ClaimForms.Cur.FontName,ClaimForms.Cur.FontSize)
						,new SolidBrush(Color.Black)
						,new RectangleF(xPosText,ClaimFormItems.ListForForm[i].YPos
						,ClaimFormItems.ListForForm[i].Width,ClaimFormItems.ListForForm[i].Height));
				}
				else{//image
					string fileName=((Pref)Prefs.HList["DocPath"]).ValueString+@"\"
						+ClaimFormItems.ListForForm[i].ImageFileName;
					if(!File.Exists(fileName)){
						MessageBox.Show("File not found.");
						continue;
					}
					Image thisImage=Image.FromFile(fileName);
					if(fileName.Substring(fileName.Length-3)=="jpg"){
						grfx.DrawImage(thisImage,ClaimFormItems.ListForForm[i].XPos,ClaimFormItems.ListForForm[i].YPos
							,(int)(thisImage.Width/thisImage.HorizontalResolution*100)
							,(int)(thisImage.Height/thisImage.VerticalResolution*100));
					}
					else if(fileName.Substring(fileName.Length-3)=="emf"){
						grfx.DrawImage(thisImage,ClaimFormItems.ListForForm[i].XPos,ClaimFormItems.ListForForm[i].YPos
							,thisImage.Width,thisImage.Height);
					}
				}
			}
			ev.HasMorePages=false;
		}

		///<summary></summary>
		public bool PrintClaim(){
			PrintDocument tempPD = new PrintDocument();
			tempPD.PrinterSettings.PrinterName=Computers.Cur.PrinterName;
			if(tempPD.PrinterSettings.IsValid){
				pd2.PrinterSettings.PrinterName=Computers.Cur.PrinterName;
			}
			//uses default printer if selected printer not valid
			tempPD.Dispose();
			try{
				printDialog2=new PrintDialog();
				printDialog2.Document=pd2;
				if(printDialog2.ShowDialog()==DialogResult.OK){
					pd2.Print();
				}
			}
			catch{
				MessageBox.Show(Lan.g(this,"Printer not available."));
				return false;
			}
			return true;
		}

		private void FillDisplayStrings(){
			if(PrintBlank){
				ClaimForms.SetCur(1);//hard coded to ADA claimform for now.
				ClaimFormItems.GetListForForm();
				displayStrings=new string[ClaimFormItems.ListForForm.Length];
				return;
			}
			Patients.GetFamily(ThisPatNum);
			Claims.Refresh();
			Claims.Cur=(Claim)Claims.HList[ThisClaimNum];
			InsPlans.Refresh();
			//get other plan first to clear up Cur
			InsPlan otherPlan;
			InsPlans.GetCur(Claims.Cur.PlanNum2);
			otherPlan=InsPlans.Cur;
			InsPlans.GetCur(Claims.Cur.PlanNum);
			Patient subsc;
			if(Patients.GetIndex(InsPlans.Cur.Subscriber)==-1){//from another family
				Patients.GetFamily(InsPlans.Cur.Subscriber);
				subsc=Patients.Cur;
				Patients.GetFamily(ThisPatNum);//return to current family
			}
			else{
				subsc=Patients.FamilyList[Patients.GetIndex(InsPlans.Cur.Subscriber)];
			}
			Patient otherSubsc=new Patient();
			if(otherPlan.PlanNum!=0){//if secondary insurance exists
				if(Patients.GetIndex(otherPlan.Subscriber)==-1){//from another family
					Patients.GetFamily(otherPlan.Subscriber);
					otherSubsc=Patients.Cur;
					Patients.GetFamily(ThisPatNum);//return to current family
				}
				else{
					otherSubsc=Patients.FamilyList[Patients.GetIndex(otherPlan.Subscriber)];
				}				
			}	
			Procedures.Refresh();
      ClaimProcs.Refresh();
      ClaimProcs.GetForClaim(); 
			claimprocs=new ArrayList();
			bool includeThis;
			for(int i=0;i<ClaimProcs.ForClaim.Length;i++){//fill the arraylist
				if(ClaimProcs.ForClaim[i].ProcNum==0){
					continue;//skip payments
				}
				includeThis=true;
				for(int j=0;j<claimprocs.Count;j++){//loop through existing claimprocs
					if(((ClaimProc)claimprocs[j]).ProcNum==ClaimProcs.ForClaim[i].ProcNum){
						includeThis=false;//skip duplicate procedures
					}
				}
				if(includeThis)
					claimprocs.Add(ClaimProcs.ForClaim[i]);						
			}
			Provider treatDent=Providers.ListLong[Providers.GetIndexLong(Claims.Cur.ProvTreat)];
			//claimformitems will already be refreshed in local data.  A normal user will never change items.
			ClaimForms.SetCur(InsPlans.Cur.ClaimFormNum);
			ClaimFormItems.GetListForForm();
			displayStrings=new string[ClaimFormItems.ListForForm.Length];
			//a value is set for every item, but not every case will have a matching claimform item.
			for(int i=0;i<ClaimFormItems.ListForForm.Length;i++){
				switch(ClaimFormItems.ListForForm[i].FieldName){
					default://usually just an image
						displayStrings[i]="";
						break;
					case "IsPreAuth":
						if(Claims.Cur.ClaimType=="PreAuth")
							displayStrings[i]="X";
						break;
					case "IsStandardClaim":
						if(Claims.Cur.ClaimType!="PreAuth")
							displayStrings[i]="X";
						break;
					case "PreAuthString":
						displayStrings[i]=Claims.Cur.PreAuthString;
						break;
					case "PriInsCarrierName":
						displayStrings[i]=InsPlans.Cur.Carrier;
						break;
					case "PriInsAddress":
						displayStrings[i]=InsPlans.Cur.Address;
						break;
					case "PriInsAddress2":
						displayStrings[i]=InsPlans.Cur.Address2;
						break;
					case "PriInsCity":
						displayStrings[i]=InsPlans.Cur.City;
						break;
					case "PriInsST":
						displayStrings[i]=InsPlans.Cur.State;
						break;
					case "PriInsZip":
						displayStrings[i]=InsPlans.Cur.Zip;
						break;
					case "OtherInsExists":
						if(otherPlan.PlanNum!=0)
							displayStrings[i]="X";
						break;
					case "OtherInsNotExists":
						if(otherPlan.PlanNum==0)
							displayStrings[i]="X";
						break;
					case "OtherInsSubscrLastFirst":
						if(otherPlan.PlanNum!=0)
							displayStrings[i]=otherSubsc.LName+", "+otherSubsc.FName+", "+otherSubsc.MiddleI;
						break;
					case "OtherInsSubscrDOB":
						if(otherPlan.PlanNum!=0)
							displayStrings[i]=otherSubsc.Birthdate.ToShortDateString();
						break;
					case "OtherInsSubscrIsMale":
						if(otherPlan.PlanNum!=0 && otherSubsc.Gender==PatientGender.Male)
							displayStrings[i]="X";
						break;
					case "OtherInsSubscrIsFemale":
						if(otherPlan.PlanNum!=0 && otherSubsc.Gender==PatientGender.Female)
							displayStrings[i]="X";
						break;
					case "OtherInsSubscrID":
						if(otherPlan.PlanNum!=0)
							displayStrings[i]=otherPlan.SubscriberID;
						break;
						//if(otherPlan.PlanNum!=0 && otherSubsc.SSN.Length==9){
						//	displayStrings[i]=otherSubsc.SSN.Substring(0,3)
						//		+"-"+otherSubsc.SSN.Substring(3,2)
						//		+"-"+otherSubsc.SSN.Substring(5);
						//}
						//break;
					case "OtherInsGroupNum":
						if(otherPlan.PlanNum!=0)
							displayStrings[i]=otherPlan.GroupNum;
						break;
					case "OtherInsRelatIsSelf":
						if(otherPlan.PlanNum!=0 && Claims.Cur.PatRelat2==Relat.Self)
							displayStrings[i]="X";
						break;
					case "OtherInsRelatIsSpouse":
						if(otherPlan.PlanNum!=0 && Claims.Cur.PatRelat2==Relat.Spouse)
							displayStrings[i]="X";
						break;
					case "OtherInsRelatIsChild":
						if(otherPlan.PlanNum!=0 && Claims.Cur.PatRelat2==Relat.Child)
							displayStrings[i]="X";
						break;
					case "OtherInsRelatIsOther":
						if(otherPlan.PlanNum!=0 && (
							Claims.Cur.PatRelat2==Relat.Dependent
							|| Claims.Cur.PatRelat2==Relat.Employee
							|| Claims.Cur.PatRelat2==Relat.HandicapDep
							|| Claims.Cur.PatRelat2==Relat.InjuredPlaintiff
							|| Claims.Cur.PatRelat2==Relat.LifePartner
							|| Claims.Cur.PatRelat2==Relat.SignifOther
							))
							displayStrings[i]="X";
						break;
					case "OtherInsCarrierName":
						if(otherPlan.PlanNum!=0)
							displayStrings[i]=otherPlan.Carrier;
						break;
					case "OtherInsAddress":
						if(otherPlan.PlanNum!=0)
							displayStrings[i]=otherPlan.Address;
						break;
					case "OtherInsCity":
						if(otherPlan.PlanNum!=0)
							displayStrings[i]=otherPlan.City;
						break;
					case "OtherInsST":
						if(otherPlan.PlanNum!=0)
							displayStrings[i]=otherPlan.State;
						break;
					case "OtherInsZip":
						if(otherPlan.PlanNum!=0)
							displayStrings[i]=otherPlan.Zip;
						break;
					case "SubscrLastFirst":
						displayStrings[i]=subsc.LName+", "+subsc.FName+", "+subsc.MiddleI;
						break;
					case "SubscrAddress":
						displayStrings[i]=subsc.Address;
						break;
					case "SubscrAddress2":
						displayStrings[i]=subsc.Address2;
						break;
					case "SubscrCity":
						displayStrings[i]=subsc.City;
						break;
					case "SubscrST":
						displayStrings[i]=subsc.State;
						break;
					case "SubscrZip":
						displayStrings[i]=subsc.Zip;
						break;
					case "SubscrDOB":
						displayStrings[i]=subsc.Birthdate.ToString("MM/dd/yyyy");
						break;
					case "SubscrIsMale":
						if(subsc.Gender==PatientGender.Male)
							displayStrings[i]="X";
						break;
					case "SubscrIsFemale":
						if(subsc.Gender==PatientGender.Female)
							displayStrings[i]="X";
						break;
					case "SubscrID":
						displayStrings[i]=InsPlans.Cur.SubscriberID;
						//if(subsc.SSN.Length==9){
						//	displayStrings[i]=subsc.SSN.Substring(0,3)
						//		+"-"+subsc.SSN.Substring(3,2)
						//		+"-"+subsc.SSN.Substring(5);	
						//}
						break;
					case "GroupNum":
						displayStrings[i]=InsPlans.Cur.GroupNum;
						break;
					case "EmployerName":
						displayStrings[i]=InsPlans.Cur.Employer;
						break;
					case "RelatIsSelf":
						if(Claims.Cur.PatRelat==Relat.Self)
							displayStrings[i]="X";
						break;
					case "RelatIsSpouse":
						if(Claims.Cur.PatRelat==Relat.Spouse)
							displayStrings[i]="X";
						break;
					case "RelatIsChild":
						if(Claims.Cur.PatRelat==Relat.Child)
							displayStrings[i]="X";
						break;
					case "RelatIsOther":
						if(Claims.Cur.PatRelat==Relat.Dependent
							|| Claims.Cur.PatRelat==Relat.Employee
							|| Claims.Cur.PatRelat==Relat.HandicapDep
							|| Claims.Cur.PatRelat==Relat.InjuredPlaintiff
							|| Claims.Cur.PatRelat==Relat.LifePartner
							|| Claims.Cur.PatRelat==Relat.SignifOther)
							displayStrings[i]="X";
						break;
					case "IsFTStudent":
						if(Patients.Cur.StudentStatus=="F")
							displayStrings[i]="X";
						break;
					case "IsPTStudent":
						if(Patients.Cur.StudentStatus=="P")
							displayStrings[i]="X";
						break;
					case "PatientLastFirst":
						displayStrings[i]=Patients.Cur.LName+", "+Patients.Cur.FName+", "+Patients.Cur.MiddleI;
						break;
					case "PatientAddress":
						displayStrings[i]=Patients.Cur.Address;
						break;
					case "PatientAddress2":
						displayStrings[i]=Patients.Cur.Address2;
						break;
					case "PatientCity":
						displayStrings[i]=Patients.Cur.City;
						break;
					case "PatientST":
						displayStrings[i]=Patients.Cur.State;
						break;
					case "PatientZip":
						displayStrings[i]=Patients.Cur.Zip;
						break;
					case "PatientDOB":
						displayStrings[i]=Patients.Cur.Birthdate.ToString("MM/dd/yyyy");
						break;
					case "PatientDOBMonth":
						displayStrings[i]=Patients.Cur.Birthdate.ToString("MM");
						break;
					case "PatientDOBDay":
						displayStrings[i]=Patients.Cur.Birthdate.ToString("dd");
						break;
					case "PatientDOBYear":
						displayStrings[i]=Patients.Cur.Birthdate.ToString("yy");
						break;
					case "PatientIsMale":
						if(Patients.Cur.Gender==PatientGender.Male)
							displayStrings[i]="X";
						break;
					case "PatientIsFemale":
						if(Patients.Cur.Gender==PatientGender.Female)
							displayStrings[i]="X";
						break;
					case "PatientSSN":
						if(Patients.Cur.SSN.Length==9){
							displayStrings[i]=Patients.Cur.SSN.Substring(0,3)
								+"-"+Patients.Cur.SSN.Substring(3,2)
								+"-"+Patients.Cur.SSN.Substring(5);
						}
						break;
					case "PatientMedicaidID":
						displayStrings[i]=Patients.Cur.MedicaidID;
						break;
					case "P1Date":
						displayStrings[i]=GetProcInfo("Date",1,ClaimFormItems.ListForForm[i].FormatString);
						break;
					case "P1Area":
						displayStrings[i]=GetProcInfo("Area",1);
						break;
					case "P1System":
						displayStrings[i]=GetProcInfo("System",1);
						break;
					case "P1ToothNumber":
						displayStrings[i]=GetProcInfo("ToothNum",1);
						break;
					case "P1Surface":
						displayStrings[i]=GetProcInfo("Surface",1);
						break;
					case "P1Code":
						displayStrings[i]=GetProcInfo("Code",1);
						break;
					case "P1Description":
						displayStrings[i]=GetProcInfo("Desc",1);
						break;
					case "P1Fee":
						displayStrings[i]=GetProcInfo("Fee",1);
						break;
					case "P1TreatDentMedicaidID":
						displayStrings[i]=GetProcInfo("TreatDentMedicaidID",1);
						break;
					case "P2Date":
						displayStrings[i]=GetProcInfo("Date",2,ClaimFormItems.ListForForm[i].FormatString);
						break;
					case "P2Area":
						displayStrings[i]=GetProcInfo("Area",2);
						break;
					case "P2System":
						displayStrings[i]=GetProcInfo("System",2);
						break;
					case "P2ToothNumber":
						displayStrings[i]=GetProcInfo("ToothNum",2);
						break;
					case "P2Surface":
						displayStrings[i]=GetProcInfo("Surface",2);
						break;
					case "P2Code":
						displayStrings[i]=GetProcInfo("Code",2);
						break;
					case "P2Description":
						displayStrings[i]=GetProcInfo("Desc",2);
						break;
					case "P2Fee":
						displayStrings[i]=GetProcInfo("Fee",2);
						break;
					case "P2TreatDentMedicaidID":
						displayStrings[i]=GetProcInfo("TreatDentMedicaidID",2);
						break;
					case "P3Date":
						displayStrings[i]=GetProcInfo("Date",3,ClaimFormItems.ListForForm[i].FormatString);
						break;
					case "P3Area":
						displayStrings[i]=GetProcInfo("Area",3);
						break;
					case "P3System":
						displayStrings[i]=GetProcInfo("System",3);
						break;
					case "P3ToothNumber":
						displayStrings[i]=GetProcInfo("ToothNum",3);
						break;
					case "P3Surface":
						displayStrings[i]=GetProcInfo("Surface",3);
						break;
					case "P3Code":
						displayStrings[i]=GetProcInfo("Code",3);
						break;
					case "P3Description":
						displayStrings[i]=GetProcInfo("Desc",3);
						break;
					case "P3Fee":
						displayStrings[i]=GetProcInfo("Fee",3);
						break;
					case "P3TreatDentMedicaidID":
						displayStrings[i]=GetProcInfo("TreatDentMedicaidID",3);
						break;
					case "P4Date":
						displayStrings[i]=GetProcInfo("Date",4,ClaimFormItems.ListForForm[i].FormatString);
						break;
					case "P4Area":
						displayStrings[i]=GetProcInfo("Area",4);
						break;
					case "P4System":
						displayStrings[i]=GetProcInfo("System",4);
						break;
					case "P4ToothNumber":
						displayStrings[i]=GetProcInfo("ToothNum",4);
						break;
					case "P4Surface":
						displayStrings[i]=GetProcInfo("Surface",4);
						break;
					case "P4Code":
						displayStrings[i]=GetProcInfo("Code",4);
						break;
					case "P4Description":
						displayStrings[i]=GetProcInfo("Desc",4);
						break;
					case "P4Fee":
						displayStrings[i]=GetProcInfo("Fee",4);
						break;
					case "P4TreatDentMedicaidID":
						displayStrings[i]=GetProcInfo("TreatDentMedicaidID",4);
						break;
					case "P5Date":
						displayStrings[i]=GetProcInfo("Date",5,ClaimFormItems.ListForForm[i].FormatString);
						break;
					case "P5Area":
						displayStrings[i]=GetProcInfo("Area",5);
						break;
					case "P5System":
						displayStrings[i]=GetProcInfo("System",5);
						break;
					case "P5ToothNumber":
						displayStrings[i]=GetProcInfo("ToothNum",5);
						break;
					case "P5Surface":
						displayStrings[i]=GetProcInfo("Surface",5);
						break;
					case "P5Code":
						displayStrings[i]=GetProcInfo("Code",5);
						break;
					case "P5Description":
						displayStrings[i]=GetProcInfo("Desc",5);
						break;
					case "P5Fee":
						displayStrings[i]=GetProcInfo("Fee",5);
						break;
					case "P5TreatDentMedicaidID":
						displayStrings[i]=GetProcInfo("TreatDentMedicaidID",5);
						break;
					case "P6Date":
						displayStrings[i]=GetProcInfo("Date",6,ClaimFormItems.ListForForm[i].FormatString);
						break;
					case "P6Area":
						displayStrings[i]=GetProcInfo("Area",6);
						break;
					case "P6System":
						displayStrings[i]=GetProcInfo("System",6);
						break;
					case "P6ToothNumber":
						displayStrings[i]=GetProcInfo("ToothNum",6);
						break;
					case "P6Surface":
						displayStrings[i]=GetProcInfo("Surface",6);
						break;
					case "P6Code":
						displayStrings[i]=GetProcInfo("Code",6);
						break;
					case "P6Description":
						displayStrings[i]=GetProcInfo("Desc",6);
						break;
					case "P6Fee":
						displayStrings[i]=GetProcInfo("Fee",6);
						break;
					case "P6TreatDentMedicaidID":
						displayStrings[i]=GetProcInfo("TreatDentMedicaidID",6);
						break;
					case "P7Date":
						displayStrings[i]=GetProcInfo("Date",7,ClaimFormItems.ListForForm[i].FormatString);
						break;
					case "P7Area":
						displayStrings[i]=GetProcInfo("Area",7);
						break;
					case "P7System":
						displayStrings[i]=GetProcInfo("System",7);
						break;
					case "P7ToothNumber":
						displayStrings[i]=GetProcInfo("ToothNum",7);
						break;
					case "P7Surface":
						displayStrings[i]=GetProcInfo("Surface",7);
						break;
					case "P7Code":
						displayStrings[i]=GetProcInfo("Code",7);
						break;
					case "P7Description":
						displayStrings[i]=GetProcInfo("Desc",7);
						break;
					case "P7Fee":
						displayStrings[i]=GetProcInfo("Fee",7);
						break;
					case "P7TreatDentMedicaidID":
						displayStrings[i]=GetProcInfo("TreatDentMedicaidID",7);
						break;
					case "P8Date":
						displayStrings[i]=GetProcInfo("Date",8,ClaimFormItems.ListForForm[i].FormatString);
						break;
					case "P8Area":
						displayStrings[i]=GetProcInfo("Area",8);
						break;
					case "P8System":
						displayStrings[i]=GetProcInfo("System",8);
						break;
					case "P8ToothNumber":
						displayStrings[i]=GetProcInfo("ToothNum",8);
						break;
					case "P8Surface":
						displayStrings[i]=GetProcInfo("Surface",8);
						break;
					case "P8Code":
						displayStrings[i]=GetProcInfo("Code",8);
						break;
					case "P8Description":
						displayStrings[i]=GetProcInfo("Desc",8);
						break;
					case "P8Fee":
						displayStrings[i]=GetProcInfo("Fee",8);
						break;
					case "P8TreatDentMedicaidID":
						displayStrings[i]=GetProcInfo("TreatDentMedicaidID",8);
						break;
					case "P9Date":
						displayStrings[i]=GetProcInfo("Date",9,ClaimFormItems.ListForForm[i].FormatString);
						break;
					case "P9Area":
						displayStrings[i]=GetProcInfo("Area",9);
						break;
					case "P9System":
						displayStrings[i]=GetProcInfo("System",9);
						break;
					case "P9ToothNumber":
						displayStrings[i]=GetProcInfo("ToothNum",9);
						break;
					case "P9Surface":
						displayStrings[i]=GetProcInfo("Surface",9);
						break;
					case "P9Code":
						displayStrings[i]=GetProcInfo("Code",9);
						break;
					case "P9Description":
						displayStrings[i]=GetProcInfo("Desc",9);
						break;
					case "P9Fee":
						displayStrings[i]=GetProcInfo("Fee",9);
						break;
					case "P9TreatDentMedicaidID":
						displayStrings[i]=GetProcInfo("TreatDentMedicaidID",9);
						break;
					case "P10Date":
						displayStrings[i]=GetProcInfo("Date",10,ClaimFormItems.ListForForm[i].FormatString);
						break;
					case "P10Area":
						displayStrings[i]=GetProcInfo("Area",10);
						break;
					case "P10System":
						displayStrings[i]=GetProcInfo("System",10);
						break;
					case "P10ToothNumber":
						displayStrings[i]=GetProcInfo("ToothNum",10);
						break;
					case "P10Surface":
						displayStrings[i]=GetProcInfo("Surface",10);
						break;
					case "P10Code":
						displayStrings[i]=GetProcInfo("Code",10);
						break;
					case "P10Description":
						displayStrings[i]=GetProcInfo("Desc",10);
						break;
					case "P10Fee":
						displayStrings[i]=GetProcInfo("Fee",10);
						break;
					case "P10TreatDentMedicaidID":
						displayStrings[i]=GetProcInfo("TreatDentMedicaidID",10);
						break;
					case "TotalFee":
						displayStrings[i]=Claims.Cur.ClaimFee.ToString("F");
						break;
					case "Miss1":
						if(Procedures.MissingTeeth.Contains("1"))
							displayStrings[i]="X";
						break;
					case "Miss2":
						if(Procedures.MissingTeeth.Contains("2"))
							displayStrings[i]="X";
						break;
					case "Miss3":
						if(Procedures.MissingTeeth.Contains("3"))
							displayStrings[i]="X";
						break;
					case "Miss4":
						if(Procedures.MissingTeeth.Contains("4"))
							displayStrings[i]="X";
						break;
					case "Miss5":
						if(Procedures.MissingTeeth.Contains("5"))
							displayStrings[i]="X";
						break;
					case "Miss6":
						if(Procedures.MissingTeeth.Contains("6"))
							displayStrings[i]="X";
						break;
					case "Miss7":
						if(Procedures.MissingTeeth.Contains("7"))
							displayStrings[i]="X";
						break;
					case "Miss8":
						if(Procedures.MissingTeeth.Contains("8"))
							displayStrings[i]="X";
						break;
					case "Miss9":
						if(Procedures.MissingTeeth.Contains("9"))
							displayStrings[i]="X";
						break;
					case "Miss10":
						if(Procedures.MissingTeeth.Contains("10"))
							displayStrings[i]="X";
						break;
					case "Miss11":
						if(Procedures.MissingTeeth.Contains("11"))
							displayStrings[i]="X";
						break;
					case "Miss12":
						if(Procedures.MissingTeeth.Contains("12"))
							displayStrings[i]="X";
						break;
					case "Miss13":
						if(Procedures.MissingTeeth.Contains("13"))
							displayStrings[i]="X";
						break;
					case "Miss14":
						if(Procedures.MissingTeeth.Contains("14"))
							displayStrings[i]="X";
						break;
					case "Miss15":
						if(Procedures.MissingTeeth.Contains("15"))
							displayStrings[i]="X";
						break;
					case "Miss16":
						if(Procedures.MissingTeeth.Contains("16"))
							displayStrings[i]="X";
						break;
					case "Miss17":
						if(Procedures.MissingTeeth.Contains("17"))
							displayStrings[i]="X";
						break;
					case "Miss18":
						if(Procedures.MissingTeeth.Contains("18"))
							displayStrings[i]="X";
						break;
					case "Miss19":
						if(Procedures.MissingTeeth.Contains("19"))
							displayStrings[i]="X";
						break;
					case "Miss20":
						if(Procedures.MissingTeeth.Contains("20"))
							displayStrings[i]="X";
						break;
					case "Miss21":
						if(Procedures.MissingTeeth.Contains("21"))
							displayStrings[i]="X";
						break;
					case "Miss22":
						if(Procedures.MissingTeeth.Contains("22"))
							displayStrings[i]="X";
						break;
					case "Miss23":
						if(Procedures.MissingTeeth.Contains("23"))
							displayStrings[i]="X";
						break;
					case "Miss24":
						if(Procedures.MissingTeeth.Contains("24"))
							displayStrings[i]="X";
						break;
					case "Miss25":
						if(Procedures.MissingTeeth.Contains("25"))
							displayStrings[i]="X";
						break;
					case "Miss26":
						if(Procedures.MissingTeeth.Contains("26"))
							displayStrings[i]="X";
						break;
					case "Miss27":
						if(Procedures.MissingTeeth.Contains("27"))
							displayStrings[i]="X";
						break;
					case "Miss28":
						if(Procedures.MissingTeeth.Contains("28"))
							displayStrings[i]="X";
						break;
					case "Miss29":
						if(Procedures.MissingTeeth.Contains("29"))
							displayStrings[i]="X";
						break;
					case "Miss30":
						if(Procedures.MissingTeeth.Contains("30"))
							displayStrings[i]="X";
						break;
					case "Miss31":
						if(Procedures.MissingTeeth.Contains("31"))
							displayStrings[i]="X";
						break;
					case "Miss32":
						if(Procedures.MissingTeeth.Contains("32"))
							displayStrings[i]="X";
						break;
					case "Remarks":
						displayStrings[i]=Claims.Cur.ClaimNote;
						break;
					case "PatientRelease":
						if(InsPlans.Cur.ReleaseInfo)
							displayStrings[i]="Signature on File"; 
						break;
					case "PatientReleaseDate":
						if(InsPlans.Cur.ReleaseInfo && Claims.Cur.DateSent.Year > 1860)
							displayStrings[i]=Claims.Cur.DateSent.ToString("MM/dd/yyyy"); 
						break;
					case "PatientAssignment":
						if(InsPlans.Cur.AssignBen)
							displayStrings[i]="Signature on File"; 
						break;
					case "PatientAssignmentDate":
						if(InsPlans.Cur.AssignBen && Claims.Cur.DateSent.Year > 1860)
							displayStrings[i]=Claims.Cur.DateSent.ToString("MM/dd/yyyy"); 
						break;
					case "PlaceIsOffice":
						if(Claims.Cur.PlaceService==PlaceOfService.Office)
							displayStrings[i]="X";
						break;
					case "PlaceIsHospADA2002":
						if(Claims.Cur.PlaceService==PlaceOfService.InpatHospital
							|| Claims.Cur.PlaceService==PlaceOfService.OutpatHospital)
							displayStrings[i]="X";
						break;
					case "PlaceIsExtCareFacilityADA2002":
						if(Claims.Cur.PlaceService==PlaceOfService.AdultLivCareFac
							|| Claims.Cur.PlaceService==PlaceOfService.SkilledNursFac)
							displayStrings[i]="X";
						break;
					case "PlaceIsOtherADA2002":
						if(Claims.Cur.PlaceService==PlaceOfService.PatientsHome
							|| Claims.Cur.PlaceService==PlaceOfService.OtherLocation)
							displayStrings[i]="X";
						break;
					case "PlaceIsInpatHosp":
						if(Claims.Cur.PlaceService==PlaceOfService.InpatHospital)
							displayStrings[i]="X";
						break;
					case "PlaceIsOutpatHosp":
						if(Claims.Cur.PlaceService==PlaceOfService.OutpatHospital)
							displayStrings[i]="X";
						break;
					case "PlaceIsAdultLivCareFac":
						if(Claims.Cur.PlaceService==PlaceOfService.AdultLivCareFac)
							displayStrings[i]="X";
						break;
					case "PlaceIsSkilledNursFac":
						if(Claims.Cur.PlaceService==PlaceOfService.SkilledNursFac)
							displayStrings[i]="X";
						break;
					case "PlaceIsPatientsHome":
						if(Claims.Cur.PlaceService==PlaceOfService.PatientsHome)
							displayStrings[i]="X";
						break;
					case "PlaceIsOtherLocation":
						if(Claims.Cur.PlaceService==PlaceOfService.OtherLocation)
							displayStrings[i]="X";
						break;
					//"RadiographsEnclosed":
					//"ImagesEnclosed":
					//"ModelsEnclosed":
					case "IsNotOrtho":
						if(!Claims.Cur.IsOrtho)
							displayStrings[i]="X";
						break;
					case "IsOrtho":
						if(Claims.Cur.IsOrtho)
							displayStrings[i]="X";
						break;
					case "DateOrthoPlaced":
						if(Claims.Cur.OrthoDate.Year > 1860)
							displayStrings[i]=Claims.Cur.OrthoDate.ToString("MM/dd/yyyy"); 
						break;
					case "MonthsOrthoRemaining":
						if(Claims.Cur.OrthoRemainM > 0)
							displayStrings[i]=Claims.Cur.OrthoRemainM.ToString();
						break;
					case "IsNotReplacementProsth":
						if(Claims.Cur.IsProsthesis!="R")//=='I'nitial or 'N'o
							displayStrings[i]="X";
						break;
					case "IsReplacementProsth":
						if(Claims.Cur.IsProsthesis=="R")
							displayStrings[i]="X";
						break;
					case "DatePriorProsthPlaced":
						if(Claims.Cur.PriorDate.Year > 1860)
							displayStrings[i]=Claims.Cur.PriorDate.ToString("MM/dd/yyyy"); 
						break;
					case "IsOccupational":
						if(Claims.Cur.AccidentRelated=="E")
							displayStrings[i]="X";
						break;
					case "IsAutoAccident":
						if(Claims.Cur.AccidentRelated=="A")
							displayStrings[i]="X";
						break;
					case "IsOtherAccident":
						if(Claims.Cur.AccidentRelated=="O")
							displayStrings[i]="X";
						break;
					case "AccidentDate":
						if(Claims.Cur.AccidentDate.Year > 1860)
							displayStrings[i]=Claims.Cur.AccidentDate.ToString("MM/dd/yyyy"); 
						break;
					case "AccidentST":
						displayStrings[i]=Claims.Cur.AccidentST;
						break;
					case "BillingDentist":
						Provider P=Providers.ListLong[Providers.GetIndexLong(Claims.Cur.ProvBill)];
						displayStrings[i]=P.FName+" "+P.MI+" "+P.LName+" "+P.Title;
						break;
					case "BillingDentistAddress":
						displayStrings[i]=((Pref)Prefs.HList["PracticeAddress"]).ValueString;
						break;
					case "BillingDentistAddress2":
						displayStrings[i]=((Pref)Prefs.HList["PracticeAddress2"]).ValueString;
						break;
					case "BillingDentistCity":
						displayStrings[i]=((Pref)Prefs.HList["PracticeCity"]).ValueString;
						break;
					case "BillingDentistST":
						displayStrings[i]=((Pref)Prefs.HList["PracticeST"]).ValueString;
						break;
					case "BillingDentistZip":
						displayStrings[i]=((Pref)Prefs.HList["PracticeZip"]).ValueString;
						break;
					case "BillingDentistMedicaidID":
						displayStrings[i]=Providers.ListLong[Providers.GetIndexLong(Claims.Cur.ProvBill)].MedicaidID;
						break;
					//case "BillingDentistProvID":
					case "BillingDentistLicenseNum":
						displayStrings[i]=Providers.ListLong[Providers.GetIndexLong(Claims.Cur.ProvBill)].StateLicense;
						break;
					case "BillingDentistSSNorTIN":
						displayStrings[i]=Providers.ListLong[Providers.GetIndexLong(Claims.Cur.ProvBill)].SSN;
						break;
					case "BillingDentistPh123":
						if(((Pref)Prefs.HList["PracticePhone"]).ValueString.Length==10){
							displayStrings[i]=((Pref)Prefs.HList["PracticePhone"]).ValueString.Substring(0,3);
						}
						break;
					case "BillingDentistPh456":
						if(((Pref)Prefs.HList["PracticePhone"]).ValueString.Length==10){
							displayStrings[i]=((Pref)Prefs.HList["PracticePhone"]).ValueString.Substring(3,3);
						}
						break;
					case "BillingDentistPh78910":
						if(((Pref)Prefs.HList["PracticePhone"]).ValueString.Length==10){
							displayStrings[i]=((Pref)Prefs.HList["PracticePhone"]).ValueString.Substring(6);
						}
						break;
					case "TreatingDentistSignature":
						if(treatDent.SigOnFile){
							displayStrings[i]=treatDent.FName+" "+treatDent.MI+" "+treatDent.LName+" "
								+treatDent.Title+"-Sig on file";
						}
						break;
					case "TreatingDentistSigDate":
						if(treatDent.SigOnFile && Claims.Cur.DateSent.Year > 1860){
							displayStrings[i]=Claims.Cur.DateSent.ToString("MM/dd/yyyy");
						}
						break;
					case "TreatingDentistMedicaidID":
						displayStrings[i]=GetProcInfo("TreatingDentistMedicaidID",0);
						break;
					//case "TreatingDentistProvID":
					case "TreatingDentistLicense":
						displayStrings[i]=treatDent.StateLicense;
						break;
					case "TreatingDentistAddress":
						displayStrings[i]=((Pref)Prefs.HList["PracticeAddress"]).ValueString;
						break;
					case "TreatingDentistCity":
						displayStrings[i]=((Pref)Prefs.HList["PracticeCity"]).ValueString;
						break;
					case "TreatingDentistST":
						displayStrings[i]=((Pref)Prefs.HList["PracticeST"]).ValueString;
						break;
					case "TreatingDentistZip":
						displayStrings[i]=((Pref)Prefs.HList["PracticeZip"]).ValueString;
						break;
					case "TreatingDentistPh123":
						if(((Pref)Prefs.HList["PracticePhone"]).ValueString.Length==10){
							displayStrings[i]=((Pref)Prefs.HList["PracticePhone"]).ValueString.Substring(0,3);
						}
						break;
					case "TreatingDentistPh456":
						if(((Pref)Prefs.HList["PracticePhone"]).ValueString.Length==10){
							displayStrings[i]=((Pref)Prefs.HList["PracticePhone"]).ValueString.Substring(3,3);
						}
						break;
					case "TreatingDentistPh78910":
						if(((Pref)Prefs.HList["PracticePhone"]).ValueString.Length==10){
							displayStrings[i]=((Pref)Prefs.HList["PracticePhone"]).ValueString.Substring(6);
						}
						break;
					case "TreatingProviderSpecialty":
						string spec="";
						switch(Providers.ListLong[Providers.GetIndexLong(Claims.Cur.ProvTreat)].Specialty){
							case DentalSpecialty.General:       spec="1223G0001X"; break;
							case DentalSpecialty.PublicHealth:  spec="1223D0001X"; break;
							case DentalSpecialty.Endodontics:   spec="1223E0200X"; break;
							case DentalSpecialty.Pathology:     spec="1223P0106X"; break;
							case DentalSpecialty.Radiology:     spec="1223D0008X"; break;
							case DentalSpecialty.Surgery:       spec="1223S0112X"; break;
							case DentalSpecialty.Ortho:         spec="1223X0400X"; break;
							case DentalSpecialty.Pediatric:     spec="1223P0221X"; break;
							case DentalSpecialty.Perio:         spec="1223P0300X"; break;
							case DentalSpecialty.Prosth:        spec="1223P0700X"; break;
							//non-dentist codes not permitted.
						}
						displayStrings[i]=spec;
						break;
				}//switch
			}//for
		}

		private string GetProcInfo(string field,int procIndex){
			return GetProcInfo(field,procIndex,"");
		}

		/// <summary>Gets the string to be used for this field and index.</summary>
		/// <param name="field"></param>
		/// <param name="procIndex"></param>
		/// <returns></returns>
		private string GetProcInfo(string field,int procIndex, string stringFormat){
			//remember that procIndex is 1 based, not 0 based, 
			procIndex--;//so convert to 0 based
			if(claimprocs.Count <= procIndex){
				return "";
			}
			if(field=="System")
				return "JP";
			if(field=="Code")
				return ((ClaimProc)claimprocs[procIndex]).CodeSent;
			if(field=="System")
				return "JP";
			if(field=="Fee"){
				return ((ClaimProc)claimprocs[procIndex]).FeeBilled.ToString("F");
			}
			if(field=="Desc")
				return ProcCodes.GetProcCode(
					((Procedure)Procedures.HList[((ClaimProc)claimprocs[procIndex]).ProcNum]).ADACode).Descript;
			if(field=="Date"){
				if(Claims.Cur.ClaimType=="PreAuth")//no date on preauth procedures
					return "";
				if(stringFormat=="")
					return ((ClaimProc)claimprocs[procIndex]).DateCP.ToString("MM/dd/yyyy");
				return ((ClaimProc)claimprocs[procIndex]).DateCP.ToString(stringFormat);
			}
			if(field=="TreatDentMedicaidID"){
				if(((ClaimProc)claimprocs[procIndex]).ProvNum==0){
					return "";
				}
				else return
					Providers.ListLong[Providers.GetIndexLong(((ClaimProc)claimprocs[procIndex]).ProvNum)].MedicaidID;
			}
			Procedures.Cur=(Procedure)Procedures.HList[ClaimProcs.ForClaim[procIndex].ProcNum];
			string area="";
			string toothNum="";
			string surf="";
			switch(((ProcedureCode)ProcCodes.HList[Procedures.Cur.ADACode]).TreatArea){
				case TreatmentArea.Surf:
					//area blank
					toothNum=Procedures.Cur.ToothNum;
					surf=Procedures.Cur.Surf;
					break;
				case TreatmentArea.Tooth:
					//area blank
					toothNum=Procedures.Cur.ToothNum;
					//surf blank
					break;
				case TreatmentArea.Quad:
					area=Procedures.Cur.Surf;//area "UL" etc
					//num blank
					//surf blank
					break;
				case TreatmentArea.Sextant:
					area="S"+Procedures.Cur.Surf;//area
					//num blank
					//surf blank
					break;
				case TreatmentArea.Arch:
					area=Procedures.Cur.Surf;//area "U", etc
					//num blank
					//surf blank
					break;
				case TreatmentArea.ToothRange:
					//area blank
					//since not enough room for long tooth ranges, only use U or L
					if(Tooth.IsValidDB(Procedures.Cur.ToothRange.Split(',')[0])){
						if(Tooth.IsMaxillary(Procedures.Cur.ToothRange.Split(',')[0]))
							toothNum="U";
						else
							toothNum="L";
					}
					//surf blank
					break;
				default://mouth
					//area?
					break;
			}//switch treatarea
      switch(field){
				case "Area":
					return area;
				case "ToothNum":
					return toothNum;
				case "Surface":
					return surf;
			}
			MessageBox.Show("error in getprocinfo");
			return "";//should never get to here
		}

		private void butPrint_Click(object sender, System.EventArgs e){
			if(PrintClaim()){
				Claims.UpdateStatus(ThisClaimNum,"P");
				DialogResult=DialogResult.OK;
			}
			else{
				DialogResult=DialogResult.Cancel;
			}
			//Close();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.OK;
		}
	}

}








