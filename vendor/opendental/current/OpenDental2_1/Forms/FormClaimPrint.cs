using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormClaimPrint : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butClose;
		private System.Windows.Forms.PrintPreviewControl Preview2;
		private System.Drawing.Printing.PrintDocument pd2;
		private System.Windows.Forms.Button butPrint;
		private System.ComponentModel.Container components = null;
		private Image backGround;
		private ArrayList AL;
		private Relat thisRelat;
		public int ThisClaimNum;
		public int ThisPatNum;
		public bool PrintBlank;
		private System.Windows.Forms.PrintDialog printDialog2;
		public bool PrintImmediately;
		public static string PrinterName;
    private bool isPreAuth;
		private bool isSupplemental;//preAuth is just one kind of supplemental claim

		public struct PL{//abbr for PrintLine
			public double x;
			public double y;
			public int length;
			public string str;
			public PL(double x1, double y1, int length1, string str1){
				x=x1;
				y=y1;
				length=length1;
				str=str1;
			}
		}
		public FormClaimPrint(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butClose,
				butPrint,
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
			this.butClose = new System.Windows.Forms.Button();
			this.Preview2 = new System.Windows.Forms.PrintPreviewControl();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.butPrint = new System.Windows.Forms.Button();
			this.printDialog2 = new System.Windows.Forms.PrintDialog();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.Location = new System.Drawing.Point(770, 768);
			this.butClose.Name = "butClose";
			this.butClose.TabIndex = 0;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// Preview2
			// 
			this.Preview2.AutoZoom = false;
			this.Preview2.Name = "Preview2";
			this.Preview2.Size = new System.Drawing.Size(738, 798);
			this.Preview2.TabIndex = 1;
			this.Preview2.Zoom = 1;
			// 
			// butPrint
			// 
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
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																																	this.butPrint,
																																	this.Preview2,
																																	this.butClose});
			this.Name = "FormClaimPrint";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = Lan.g(this,"FormClaimPrint");
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
			backGround=Image.FromFile(((Pref)Prefs.HList["DocPath"]).ValueString+"ADA2002.emf");
			pd2 = new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			FillArray();
			Preview2.Document=pd2;//display document
		}

		public bool PrintImmediate(){
			backGround=Image.FromFile(((Pref)Prefs.HList["DocPath"]).ValueString+"ADA2002.emf");
			pd2 = new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			if(PrintBlank) AL=new ArrayList();
			else FillArray();
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
			//ev.
			ev.Graphics.DrawImage(backGround,-20,0,(float)(backGround.Width),(float)(backGround.Height));
			for(int i=0;i<AL.Count;i++){
				//for(int j=0;j<((PL)AL[i]).str.Length;j++){
					ev.Graphics.DrawString(Lan.g(this,((PL)AL[i]).str),new Font("Arial",9f),new SolidBrush(Color.Black)
						,(float)(12+9.6*(((PL)AL[i]).x)),(float)(68+15.96*((PL)AL[i]).y));
				//}
			}
			ev.HasMorePages=false;
		}

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

		private void FillArray(){
			Patients.GetFamily(ThisPatNum);
			Claims.Refresh();
			Claims.Cur=(Claim)Claims.HList[ThisClaimNum];
			InsPlans.Refresh();
			InsPlans.Cur=(InsPlan)InsPlans.HList[Claims.Cur.PlanNum];
			Procedures.Refresh();
      ClaimProcs.Refresh();
      if(Claims.Cur.ClaimType!=""){
        ClaimProcs.GetForClaim();
        isSupplemental=true;
      }
			if(Claims.Cur.ClaimType=="PreAuth"){//there can be other types of supplemental claims
        isPreAuth=true;
      }   
			AL=new ArrayList();
			//HEADER INFORMATION
			//1. Type of transaction
			if(isPreAuth)
			  AL.Add(new PL(17,0,1,"X"));//preauth
			else  
			  AL.Add(new PL(1,0,1,"X"));//claim
			//AL.Add(new PL(1,1,1,"X"));//never checked
			//2. Predetermination
      AL.Add(new PL(1,3,10,Claims.Cur.PreAuthString));
 			//PRIMARY PAYER INFORMATION
			//3. Name, address, etc.
			AL.Add(new PL(5,6,34,InsPlans.Cur.Carrier));
			AL.Add(new PL(5,7,34,InsPlans.Cur.Address));
			AL.Add(new PL(5,8,34,InsPlans.Cur.Address2));
			AL.Add(new PL(5,9,15,InsPlans.Cur.City));
			AL.Add(new PL(22,9,2,InsPlans.Cur.State));
			AL.Add(new PL(27,9,10,InsPlans.Cur.Zip));
			//OTHER COVERAGE
			//4. Other coverage?
			bool isPri=false;
			InsPlan otherPlan=new InsPlan();
			int iPat;
			thisRelat=new Relat();
			if(Claims.Cur.PriClaimNum==Claims.Cur.ClaimNum) 
			  isPri=true;
			if(isSupplemental){
				AL.Add(new PL(16.1,11,1,"X"));//no other coverage
			}
			else if(isPri){
				if(Patients.Cur.SecPlanNum==0){
					AL.Add(new PL(16.1,11,1,"X"));//no other coverage.
				}
				else{
          AL.Add(new PL(26.1,11,1,"X"));//yes
					otherPlan=((InsPlan)InsPlans.HList[Patients.Cur.SecPlanNum]);
				}
			}
			else{//is secondary
				AL.Add(new PL(26.1,11,1,"X"));//yes
				otherPlan=((InsPlan)InsPlans.HList[Patients.Cur.PriPlanNum]);
			}
			if(otherPlan.PlanNum!=0){//if other coverage:
				//5. Subscriber Name
				iPat=Patients.GetIndex(otherPlan.Subscriber);
				AL.Add(new PL(1,13,35,Patients.FamilyList[iPat].LName+", "+Patients.FamilyList[iPat].FName+", "+Patients.FamilyList[iPat].MiddleI));
				//6. DOB
				AL.Add(new PL(1,15,10,Patients.FamilyList[iPat].Birthdate.ToString("d")));
				//7. Gender
				if(Patients.FamilyList[iPat].Gender==PatientGender.Male)
					AL.Add(new PL(15,15,1,"X"));
				else 
					AL.Add(new PL(18,15,1,"X"));
				//8. SSN
				if(Patients.FamilyList[iPat].SSN.Length==9){
					AL.Add(new PL(23,15,11,Patients.FamilyList[iPat].SSN.Substring(0,3)
						+"-"+Patients.FamilyList[iPat].SSN.Substring(3,2)
						+"-"+Patients.FamilyList[iPat].SSN.Substring(5)));
				}
				//9. Group Number
				AL.Add(new PL(1,17,11,otherPlan.GroupNum));
				//10. Relationship
				if(isPri) 
					thisRelat=Patients.Cur.SecRelationship;
				else 
					thisRelat=Patients.Cur.PriRelationship;
				switch(thisRelat){
					case Relat.Self:
						AL.Add(new PL(15.1,17.05,1,"X")); 
						break;
					case Relat.Spouse:
						AL.Add(new PL(20.1,17.05,1,"X"));
						break;
					case Relat.Child:
						AL.Add(new PL(26.1,17.05,1,"X"));
						break;
					case Relat.Dependent:
					case Relat.Employee:
					case Relat.HandicapDep:
					case Relat.InjuredPlaintiff:
					case Relat.LifePartner:
					case Relat.SignifOther:
						AL.Add(new PL(33.1,17.05,1,"X"));
						break;
				}
				//11. Other Carrier Name, Address, etc.
				AL.Add(new PL(1,19,34,otherPlan.Carrier));
				AL.Add(new PL(1,20,34,otherPlan.Address));
				AL.Add(new PL(1,21,15,otherPlan.City));
				AL.Add(new PL(18,21,2,otherPlan.State));
				AL.Add(new PL(23,21,10,otherPlan.Zip));			
			}
			//PRIMARY SUBSCRIBER INFORMATION
			//12. Name, Address, etc.
			iPat=Patients.GetIndex(InsPlans.Cur.Subscriber);
			AL.Add(new PL(42,4,34,Patients.FamilyList[iPat].LName+", "+Patients.FamilyList[iPat].FName+", "+Patients.FamilyList[iPat].MiddleI));
			AL.Add(new PL(42,5,34,Patients.FamilyList[iPat].Address));
			AL.Add(new PL(42,6,34,Patients.FamilyList[iPat].Address2));
			AL.Add(new PL(42,7,15,Patients.FamilyList[iPat].City));
			AL.Add(new PL(59,7,2,Patients.FamilyList[iPat].State));
			AL.Add(new PL(64,7,10,Patients.FamilyList[iPat].Zip));
			//13. DOB
			AL.Add(new PL(42,9,10,Patients.FamilyList[iPat].Birthdate.ToString("MM/dd/yyyy")));
			//14. Gender
			if(Patients.FamilyList[iPat].Gender==PatientGender.Male)
			  AL.Add(new PL(56,9,1,"X"));
			else 
        AL.Add(new PL(59,9,1,"X"));
			//15. SSN
			if(Patients.FamilyList[iPat].SSN.Length==9){
				AL.Add(new PL(64,9,11,Patients.FamilyList[iPat].SSN.Substring(0,3)
					+"-"+Patients.FamilyList[iPat].SSN.Substring(3,2)
					+"-"+Patients.FamilyList[iPat].SSN.Substring(5)));	
			}
			//16. GroupNum
			AL.Add(new PL(42,11,11,InsPlans.Cur.GroupNum));
			//17. Employer
      AL.Add(new PL(55,11,25,InsPlans.Cur.Employer));
			//PATIENT INFORMATION
			//18. Relationship
			if(isSupplemental)
				thisRelat=Claims.Cur.PatRelat;
			if(isPri) 
				thisRelat=Patients.Cur.PriRelationship;
			else 
				thisRelat=Patients.Cur.SecRelationship;
			switch(thisRelat){
				case Relat.Self:
		    	AL.Add(new PL(42,14,1,"X"));
					break;
				case Relat.Spouse:
			    AL.Add(new PL(47,14,1,"X"));
					break;
				case Relat.Child:
			    AL.Add(new PL(53,14,1,"X"));
					break;
				case Relat.Employee:
				case Relat.HandicapDep:
				case Relat.SignifOther:
				case Relat.InjuredPlaintiff:
				case Relat.LifePartner:
				case Relat.Dependent:
          AL.Add(new PL(62,14,1,"X"));//other
					break;
			}
			//19. Student Status
			if(Patients.Cur.StudentStatus=="F")
			  AL.Add(new PL(70,14,1,"X"));
			if(Patients.Cur.StudentStatus=="P")
			  AL.Add(new PL(75,14,1,"X"));
			//20. Name, etc.
			AL.Add(new PL(42,16,34,Patients.Cur.LName+", "+Patients.Cur.FName+", "+Patients.Cur.MiddleI));
			AL.Add(new PL(42,17,34,Patients.Cur.Address));
			AL.Add(new PL(42,18,34,Patients.Cur.Address2));
			AL.Add(new PL(42,19,15,Patients.Cur.City));
			AL.Add(new PL(59,19,2,Patients.Cur.State));
			AL.Add(new PL(64,19,10,Patients.Cur.Zip));
			//21. DOB
			AL.Add(new PL(42,21,10,Patients.Cur.Birthdate.ToString("MM/dd/yyyy")));
			//22. Gender			
			if(Patients.Cur.Gender==PatientGender.Male)
			  AL.Add(new PL(56,21,1,"X"));
			else 
				AL.Add(new PL(59,21,1,"X"));
			//23. SSN
			if(Patients.Cur.SSN.Length==9){
				AL.Add(new PL(63,21,11,Patients.Cur.SSN.Substring(0,3)
					+"-"+Patients.Cur.SSN.Substring(3,2)
					+"-"+Patients.Cur.SSN.Substring(5)));
			}
			//RECORD OF SERVICES PROVIDED
			ArrayList procs=new ArrayList();
			if(isSupplemental){//supplemental types, including preauth
				for(int i=0;i<ClaimProcs.ForClaim.Count;i++){
          Procedures.Cur=(Procedure)Procedures.HList[((ClaimProc)ClaimProcs.ForClaim[i]).ProcNum]; 
					procs.Add(Procedures.Cur);						
				}				
			}
			else if(isPri){//primary claim
				for(int i=0;i<Procedures.List.Length;i++){
					if(Procedures.List[i].ClaimNum==Claims.Cur.ClaimNum){//limit claim to 10 procs when creating claim
						procs.Add(Procedures.List[i]);
					}
				}
			}
			else{//secondary claim
				for(int i=0;i<Procedures.List.Length;i++){
					if(Procedures.List[i].ClaimNum==Claims.Cur.PriClaimNum){
						procs.Add(Procedures.List[i]);
					}
				}
			}
			for(int i=0;i<procs.Count;i++){
			//for(int i=0;i<10;i++){
				//24. Procedure Date
        if(!isPreAuth){//no date on preauth procedures
				  AL.Add(new PL(1,25+i,10,((Procedure)procs[i]).ProcDate.ToString("MM/dd/yyyy")));
        }
				//26. Syst
				AL.Add(new PL(15,25+i,2,"JP"));
				//25. Area
				//27. Tooth Num(s)
				//28. Tooth Surf
				//AL.Add(new PL(12,25+i,2,"XX"));//area
				//AL.Add(new PL(18,25+i,11,"XXXXX"));//num
//fix: test for long tooth range.  Or limit when entering procedure.
				//AL.Add(new PL(30,25+i,5,"XXXXX"));//surf
				switch(((ProcedureCode)ProcCodes.HList[((Procedure)procs[i]).ADACode]).TreatArea){
					case TreatmentArea.Surf:
						//area blank
						AL.Add(new PL(18,25+i,11,((Procedure)procs[i]).ToothNum));
						AL.Add(new PL(30,25+i,5,((Procedure)procs[i]).Surf));
						break;
					case TreatmentArea.Tooth:
						//area blank
						AL.Add(new PL(18,25+i,11,((Procedure)procs[i]).ToothNum));
						//surf blank
						break;
					case TreatmentArea.Quad:
						AL.Add(new PL(12,25+i,2,((Procedure)procs[i]).Surf));//area "UL" etc
						//num blank
						//surf blank
						break;
					case TreatmentArea.Sextant:
						AL.Add(new PL(12,25+i,2,"S"+((Procedure)procs[i]).Surf));//area
						//num blank
						//surf blank
						break;
					case TreatmentArea.Arch:
							AL.Add(new PL(12,25+i,2,((Procedure)procs[i]).Surf));//area "U", etc
						//num blank
						//surf blank
						break;
					case TreatmentArea.ToothRange:
						//area blank
						AL.Add(new PL(18,25+i,11,((Procedure)procs[i]).ToothRange));
						//surf blank
						break;
					default://mouth
						//area?
						break;
				}
				//29. ProcCode
				AL.Add(new PL(36,25+i,5,((Procedure)procs[i]).ADACode.Substring(0,5)));    
				//30. Description
				AL.Add(new PL(42,25+i,31,((ProcedureCode)ProcCodes.HList[((Procedure)procs[i]).ADACode]).Descript));
				//31. Fee
				string pfee=((Procedure)procs[i]).ProcFee.ToString("F2");
				pfee=pfee.PadLeft(7,' ');
				AL.Add(new PL(74,25+i,1,pfee.Substring(0,1)));//Thou
				AL.Add(new PL(75.5,25+i,3,pfee.Substring(1,3)));//Hun Ten One
				AL.Add(new PL(78.2,25+i,2,pfee.Substring(5,2)));// Tenth Hundreth
			}
			//32. Other Fee-leave blank
			//AL.Add(new PL(74,35,1,"1"));    
			//AL.Add(new PL(75.5,35,3,"234"));    
			//AL.Add(new PL(78.5,35,2,"56")); 
			//AL.Add(new PL(74,36,1,"1"));    
			//AL.Add(new PL(75.5,36,3,"234"));    
			//AL.Add(new PL(78.5,36,2,"56"));   
			//33. Total Fee
			string sfee=Claims.Cur.ClaimFee.ToString("F2");
			sfee=sfee.PadLeft(8,' ');
			AL.Add(new PL(73.1,37,2,sfee.Substring(0,1)));
			AL.Add(new PL(74,37,2,sfee.Substring(1,1)));
			AL.Add(new PL(75.5,37,3,sfee.Substring(2,3)));
			AL.Add(new PL(78.2,37,2,sfee.Substring(6,2)));		
			//Missing Teeth Information
			//34. X on missing teeth
			for(int i=0;i<16;i++){
				if(Procedures.MissingTeeth.Contains(Tooth.FromInt(i+1))){
					AL.Add(new PL(16+2*i,36,1,"X"));
				}
			}
			for(int i=0;i<16;i++){
				if(Procedures.MissingTeeth.Contains(Tooth.FromInt(i+17))){
				//if(Procedures.MissingTeeth[i+17])
					AL.Add(new PL(46-2*i,37,1,"X"));
				}
			}
			//for(int i=0;i<10;i++){//don't bother with primary missing teeth for now
			//	AL.Add(new PL(48+2*i,36,1,"X"));
			//}
			//for(int i=0;i<10;i++){
			//	AL.Add(new PL(66-2*i,37,1,"X"));
			//}
			//35. Remarks
			if(Claims.Cur.ClaimNote.Length>0){
				if(Claims.Cur.ClaimNote.Length<=75){
					AL.Add(new PL(5,38,75,Claims.Cur.ClaimNote));								 
				}
				else{
					AL.Add(new PL(5,38,75,Claims.Cur.ClaimNote.Substring(0,75)));
					AL.Add(new PL(0,39,80,Claims.Cur.ClaimNote.Substring(75)));
				}																						
			}
			//AUTHORIZATIONS
			//36. Patient/Guardian Signature and date
			if(InsPlans.Cur.ReleaseInfo){
				AL.Add(new PL(1,44.25,24,"Signature on File")); 
				if(Claims.Cur.DateSent.CompareTo(DateTime.Parse("1/1/1860"))>0)
					AL.Add(new PL(28,44.25,10,Claims.Cur.DateSent.ToString("MM/dd/yyyy"))); 
			}
			//37. Subscriber Signature and date
			if(InsPlans.Cur.AssignBen){
				AL.Add(new PL(1,48.25,24,"Signature on File")); 
				if(Claims.Cur.DateSent.CompareTo(DateTime.Parse("1/1/1860"))>0)
					AL.Add(new PL(28,48.25,10,Claims.Cur.DateSent.ToString("MM/dd/yyyy"))); 
			}
			//ANCILLARY CLAIM/TREATMENT INFORMATION
			//38. Place of Treatment
			switch(Claims.Cur.PlaceService){
				case PlaceOfService.Office:
					AL.Add(new PL(42,42,1,"X"));//provider's office
					break;
				case PlaceOfService.InpatHospital:
				case PlaceOfService.OutpatHospital:
					AL.Add(new PL(50,42,1,"X"));//Hospital
					break;
				case PlaceOfService.AdultLivCareFac:
				case PlaceOfService.SkilledNursFac:
					AL.Add(new PL(55,42,1,"X"));//Extended Care Facility
					break;
				case PlaceOfService.PatientsHome:
				case PlaceOfService.OtherLocation:
					AL.Add(new PL(59,42,1,"X"));//Other
					break;
			}
      //39. Number of Enclosures
			AL.Add(new PL(67.2,42,2,"00"));
			AL.Add(new PL(72.2,42,2,"00"));
			AL.Add(new PL(77.2,42,2,"00"));
			//40. Is Treatment for Orthodontics?
			if(Claims.Cur.IsOrtho)
				AL.Add(new PL(51,44,1,"X"));//yes		
			else
				AL.Add(new PL(42,44,1,"X"));//no
			//41. Date Appliance Placed
			if(Claims.Cur.OrthoDate.CompareTo(DateTime.Parse("1/1/1860"))>0)
				AL.Add(new PL(65,44.1,10,Claims.Cur.OrthoDate.ToString("MM/dd/yyyy")));    
			//42. Months of Treatment Remaining
			if(Claims.Cur.OrthoRemainM > 0)
				AL.Add(new PL(42,46.1,2,Claims.Cur.OrthoRemainM.ToString()));
			//43. Replacement of Prosthesis?
			if(Claims.Cur.IsProsthesis=="R")
				AL.Add(new PL(54,46,1,"X"));//yes
			else
				AL.Add(new PL(51,46,1,"X"));//no
	 		//44. Date Prior Placement
			if(Claims.Cur.PriorDate.CompareTo(DateTime.Parse("1/1/1860"))>0)
				AL.Add(new PL(65,46.1,10,Claims.Cur.PriorDate.ToString("MM/dd/yyyy")));   
			//45. Resulting From
			switch(Claims.Cur.AccidentRelated){//a e o
				case "E"://employment
					AL.Add(new PL(42,48,1,"X"));//occupational illness
					break;
				case "A"://auto
					AL.Add(new PL(57.05,48,1,"X"));//auto accident
					break;
				case "O":
					AL.Add(new PL(67,48,1,"X"));//other accident
					break;
			}
      //46. Date of Accident
			if(Claims.Cur.AccidentDate.CompareTo(DateTime.Parse("1/1/1860"))>0)
				AL.Add(new PL(56,49.1,10,Claims.Cur.AccidentDate.ToString("MM/dd/yyyy")));    
			//47. Auto Accident State
			AL.Add(new PL(77,49.1,2,Claims.Cur.AccidentST));
			//BILLING DENTIST
			//48. Name, Address, City, State, Zip
			AL.Add(new PL(2,53,34,((Pref)Prefs.HList["PracticeTitle"]).ValueString));
			AL.Add(new PL(2,54,34,((Pref)Prefs.HList["PracticeAddress"]).ValueString));
			AL.Add(new PL(2,55,34,((Pref)Prefs.HList["PracticeAddress2"]).ValueString));
			AL.Add(new PL(2,56,15,((Pref)Prefs.HList["PracticeCity"]).ValueString));
			AL.Add(new PL(19,56,2,((Pref)Prefs.HList["PracticeST"]).ValueString));
			AL.Add(new PL(24,56,10,((Pref)Prefs.HList["PracticeZip"]).ValueString));
			//49. Provider ID
			//AL.Add(new PL(1,58,10,"0123456789"));//not used
			//50. License Number
			AL.Add(new PL(14,58,10,Providers.ListLong[Providers.GetIndexLong(Claims.Cur.ProvBill)].StateLicense));
			//51. SSN
			AL.Add(new PL(27,58,11,Providers.ListLong[Providers.GetIndexLong(Claims.Cur.ProvBill)].SSN));
			//52. Phone Number
			string phone=((Pref)Prefs.HList["PracticePhone"]).ValueString;
			if(phone.Length==10){
				AL.Add(new PL(8.3,59.05,3,phone.Substring(0,3)));
				AL.Add(new PL(12.1,59.05,3,phone.Substring(3,3)));
				AL.Add(new PL(16.4,59.05,4,phone.Substring(6)));
			}
			//TREATING DENTIST
			//53. Signature and Date
			Provider P=Providers.ListLong[Providers.GetIndexLong(Claims.Cur.ProvTreat)];
			if(P.SigOnFile){
				AL.Add(new PL(42,53.1,25,P.FName+" "+P.MI+" "+P.LName+" "+P.Title+"-Sig on file"));
				if(Claims.Cur.DateSent.CompareTo(DateTime.Parse("1/1/1860"))>0)
					AL.Add(new PL(69,53.1,10,Claims.Cur.DateSent.ToString("MM/dd/yyyy")));
			}
			//54. Provider ID
			//AL.Add(new PL(47,55,10,"0123456789"));//not used
			//55. License Number
			AL.Add(new PL(68,55,10,Providers.ListLong[Providers.GetIndexLong(Claims.Cur.ProvTreat)].StateLicense));
			//56. Address, City, State, Zip
			AL.Add(new PL(43,57,34,((Pref)Prefs.HList["PracticeAddress"]).ValueString));
			AL.Add(new PL(43,58,15,((Pref)Prefs.HList["PracticeCity"]).ValueString));
			AL.Add(new PL(60,58,2,((Pref)Prefs.HList["PracticeST"]).ValueString));
			AL.Add(new PL(65,58,10,((Pref)Prefs.HList["PracticeZip"]).ValueString));
			//57. Phone Number
			phone=((Pref)Prefs.HList["PracticePhone"]).ValueString;
			if(phone.Length==10){
				AL.Add(new PL(48.3,59.05,3,phone.Substring(0,3)));
				AL.Add(new PL(52.1,59.05,3,phone.Substring(3,3)));
				AL.Add(new PL(56.4,59.05,4,phone.Substring(6)));
			}
			//58. Specialty
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
			AL.Add(new PL(70,59,10,Lan.g("enumDentalSpecialty",spec)));
		}

		private void butPrint_Click(object sender, System.EventArgs e){
			if(PrintClaim()){
				Claims Claims=new Claims();
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
