using System;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the patient table in the database.</summary>
	public struct Patient{
		///<summary>Primary key.</summary>
		public int    PatNum;
		///<summary>Last name.</summary>
		public string LName;
		///<summary>First name.</summary>
		public string FName;
		///<summary>Middle initial or name.</summary>
		public string MiddleI;
		///<summary>Preferred name.</summary>
		public string Preferred;
		///<summary>See the PatientStatus enumeration.</summary>
		public PatientStatus PatStatus;
		///<summary>See the PatientGender enumeration.</summary>
		public PatientGender Gender;
		///<summary>See the PatientPosition enumeration.</summary>
		public PatientPosition Position;
		///<summary></summary>
		public DateTime Birthdate;
		///<summary>In the US, this is 9 digits, no dashes. For all other countries, any punctuation or format is allowed.</summary>
		public string SSN;
		///<summary></summary>
		public string Address;
		///<summary></summary>
		public string Address2;
		///<summary></summary>
		public string City;
		///<summary>2 Char in USA</summary>
		public string State;
		///<summary></summary>
		public string Zip;
		///<summary>Includes any punctuation</summary>
		public string HmPhone;
		///<summary></summary>
		public string WkPhone;
		///<summary></summary>
		public string WirelessPhone;
		///<summary>Foreign key to patient.PatNum.  Head of household.</summary>
		public int    Guarantor;
		///<summary>Derived from Birthdate.  Not in the database table.</summary>
		public string Age;
		///<summary>Single char. Shows at upper left corner of appointments.  Suggested use is A,B,or C to designate creditworthiness, but it can actually be used for any purpose.</summary>
		public string CreditType;
		///<summary></summary>
		public string Email;
		///<summary></summary>
		public string Salutation;
		///<summary>Foreign key to insplan.PlanNum.  Primary insurance.</summary>
		public int PriPlanNum;//
		///<summary>Relationship to subscriber for primary insurance.  See the Relat enumeration.</summary>
		public Relat PriRelationship;
		///<summary>Foreign key to insplan.PlanNum.  Secondary insurance.</summary>
		public int SecPlanNum;//
		///<summary>Relationship to subscriber for secondary insurance.</summary>
		public Relat SecRelationship;
		///<summary>Current patient balance.(not family)</summary>
		public double EstBalance;
		///<summary>May be 0(none) or -1(done), otherwise it is the foreign key to appointment.AptNum.  This is the appointment that will show in the Chart module and in the Next appointment tracker.  It will never show in the Appointments module.</summary>
		public int NextAptNum;//
		///<summary>Foreign key to provider.ProvNum.  The patient's primary provider.</summary>
		public int PriProv;
		///<summary>Foreign key to provider.ProvNum.  Secondary provider (hygienist)</summary>
		public int SecProv;//
		///<summary>Foreign key to definition.DefNum.  Fee schedule for this patient.</summary>
		public int FeeSched;
		///<summary>Foreign key to definition.DefNum.  Must have a value, or the patient will not show on some reports.</summary>
		public int BillingType;
		///<summary>Months between recalls.</summary>
		public int RecallInterval;
		///<summary>Foreign key to Definition.DefNum, or 0 for none.</summary>
		public int RecallStatus;
		///<summary>Name of folder where images will be stored. Not editable for now.</summary>
		public string ImageFolder;
		///<summary>Address or phone note.</summary>
		public string AddrNote;
		///<summary>Family financial urgent note.  Only stored with guarantor, and shared for family.</summary>
		public string FamFinUrgNote;
		///<summary>Individual patient note for Urgent medical.</summary>
		public string MedUrgNote;
		///<summary>Individual patient note for Appointment module note.</summary>
		public string ApptModNote;
		///<summary>Single char for Nonstudent, Parttime, or Fulltime.  Blank=Nonstudent</summary>
		public string StudentStatus;
		///<summary></summary>
		public string SchoolName;
		///<summary>Max 15 char.  Used for reference to previous programs.</summary>
		public string ChartNumber;
		///<summary>Optional. The Medicaid ID for this patient.</summary>
		public string MedicaidID;
		///<summary>Aged balance from 0 to 30 days old. Aging numbers are for entire family.  Only stored with guarantor.</summary>
		public double Bal_0_30;
		///<summary>Aged balance from 31 to 60 days old. Aging numbers are for entire family.  Only stored with guarantor.</summary>
		public double Bal_31_60;
		///<summary>Aged balance from 61 to 90 days old. Aging numbers are for entire family.  Only stored with guarantor.</summary>
		public double Bal_61_90;
		///<summary>Aged balance over 90 days old. Aging numbers are for entire family.  Only stored with guarantor.</summary>
		public double BalOver90;
		///<summary>Insurance Estimate for entire family.</summary>
		public double InsEst;
		///<summary>Teeth to display in chart as primary. eg: "1,2,3,4,5,12,13"</summary>
		public string PrimaryTeeth;
		///<summary>Total balance for entire family before insurance estimate.  Not the same as the sum of the 4 aging balances because this can be negative.  Only stored with guarantor.</summary>
		public double BalTotal;
		///<summary>Foreign key to employer.EmployerNum.</summary>
		public int EmployerNum;
		///<summary>AKA occupation. This field was only present in version 2.8, but did not seem useful, so it has been hidden. It will very likely be deprecated.</summary>
		public string EmploymentNote;
		///<summary>Race and ethnicity. See the PatientRace enum.</summary>
		public PatientRace Race;
		///<summary>Foreign key to county.CountyName, although it will not crash if key absent.</summary>
		public string County;
		///<summary>Name of gradeschool or highschool. Foreign key to school.SchoolName, although it will not crash if key absent.</summary>
		public string GradeSchool;
		///<summary>See the PatientGrade enumeration.</summary>
		public PatientGrade GradeLevel;
		///<summary>See the TreatmentUrgency enumeration.</summary>
		public TreatmentUrgency Urgency;
		///<summary>The date that the patient first visited the office.</summary>
		public DateTime DateFirstVisit;
		///<summary>True if primary insurance is pending. This can be used with or without an insurance plan attached.</summary>
		public bool PriPending;
		///<summary>True if secondary insurance is pending. This can be used with or without an insurance plan attached.</summary>
		public bool SecPending;
	}

	/*=========================================================================================
	=================================== class Patients ===========================================*/
	///<summary></summary>
	public class Patients:DataClass{
		///<summary>Always test this to see if a patient is loaded, not patient.Cur.PatNum.</summary>
		public static bool PatIsLoaded=false;
		///<summary>Current patient.</summary>
		private static Patient cur;
		///<summary>When doing update, this Patient is the original before any changes were made. This allows only the changed fields to be updated, minimizing concurrency issues.</summary>
		public static Patient CurOld;
		///<summary>Stores limited information about a patient. Filled first via GetLim.</summary>
		public static Patient Lim;
		///<summary>Stores formatted LName, FName, MI for the Lim patient.</summary>
		public static string LimName;
		///<summary>A list of patients in one family (sharing a GuarantorNum)</summary>
		public static Patient[] FamilyList;
		///<summary>Used in the Aging window.</summary>
		public static PatAging[] AgingList;
		///<summary>The index within the FamilyList of the Guarantor. Might not be necessary anymore since the guarantor should always be index 0.</summary>
		public static int GuarIndex;
		///<summary></summary>
		public static RecallItem[] RecallList;
		///<summary>A list of all patient names. Key=patNum, value=formatted name.  Fill with GetHList.</summary>
		public static Hashtable HList;
		//<summary>Used in the Select Patient window.</summary>
		//public static Patient[] PtList;
		///<summary>This replaces the PtList for use in the Select Patient window.</summary>
		public static DataTable PtDataTable;

		public static Patient Cur{
			get{
				return cur;
			}
			set{
				cur=value;
				//curOld=value;
			}
		}

		///<summary></summary>
		public static void GetFamily(int patNum){
			cmd.CommandText= 
				"SELECT guarantor FROM patient "
				+"WHERE patnum = '"+POut.PInt(patNum)+"'";
			FillTable();
			if(table.Rows.Count==0){
				PatIsLoaded=false;
				cur=new Patient();
				FamilyList=new Patient[1];
				FamilyList[0]=cur;
				GuarIndex=0;
				return;
			}
			cmd.CommandText= 
				"SELECT * "
				+"FROM patient "
				+"WHERE guarantor = '"+table.Rows[0][0].ToString()+"'"
				+" ORDER BY (patnum!=guarantor), birthdate";
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			FamilyList = new Patient[table.Rows.Count];
			//MessageBox.Show(table.Rows.Count.ToString());
			for (int i=0;i<table.Rows.Count;i++){
				FamilyList[i].PatNum       = PIn.PInt   (table.Rows[i][0].ToString());
				FamilyList[i].LName        = PIn.PString(table.Rows[i][1].ToString());
				FamilyList[i].FName        = PIn.PString(table.Rows[i][2].ToString());
				FamilyList[i].MiddleI      = PIn.PString(table.Rows[i][3].ToString());
				FamilyList[i].Preferred    = PIn.PString(table.Rows[i][4].ToString());
				FamilyList[i].PatStatus    = (PatientStatus)PIn.PInt   (table.Rows[i][5].ToString());
				FamilyList[i].Gender       = (PatientGender)PIn.PInt   (table.Rows[i][6].ToString());
				FamilyList[i].Position     = (PatientPosition)PIn.PInt   (table.Rows[i][7].ToString());
				FamilyList[i].Birthdate    = PIn.PDate  (table.Rows[i][8].ToString());
				FamilyList[i].Age=Shared.DateToAge(FamilyList[i].Birthdate);
				FamilyList[i].SSN          = PIn.PString(table.Rows[i][9].ToString());
				FamilyList[i].Address      = PIn.PString(table.Rows[i][10].ToString());
				FamilyList[i].Address2     = PIn.PString(table.Rows[i][11].ToString());
				FamilyList[i].City         = PIn.PString(table.Rows[i][12].ToString());
				FamilyList[i].State        = PIn.PString(table.Rows[i][13].ToString());
				FamilyList[i].Zip          = PIn.PString(table.Rows[i][14].ToString());
				FamilyList[i].HmPhone      = PIn.PString(table.Rows[i][15].ToString());
				FamilyList[i].WkPhone      = PIn.PString(table.Rows[i][16].ToString());
				FamilyList[i].WirelessPhone= PIn.PString(table.Rows[i][17].ToString());
				FamilyList[i].Guarantor    = PIn.PInt   (table.Rows[i][18].ToString());
				FamilyList[i].CreditType   = PIn.PString(table.Rows[i][19].ToString());
				FamilyList[i].Email        = PIn.PString(table.Rows[i][20].ToString());
				FamilyList[i].Salutation   = PIn.PString(table.Rows[i][21].ToString());
				FamilyList[i].PriPlanNum   = PIn.PInt   (table.Rows[i][22].ToString());
				FamilyList[i].PriRelationship=(Relat)PIn.PInt(table.Rows[i][23].ToString());
				FamilyList[i].SecPlanNum   = PIn.PInt   (table.Rows[i][24].ToString());
				FamilyList[i].SecRelationship=(Relat)PIn.PInt(table.Rows[i][25].ToString());
				FamilyList[i].EstBalance   = PIn.PDouble(table.Rows[i][26].ToString());
				FamilyList[i].NextAptNum   = PIn.PInt   (table.Rows[i][27].ToString());
				FamilyList[i].PriProv      = PIn.PInt   (table.Rows[i][28].ToString());
				FamilyList[i].SecProv      = PIn.PInt   (table.Rows[i][29].ToString());
				FamilyList[i].FeeSched     = PIn.PInt   (table.Rows[i][30].ToString());
				FamilyList[i].BillingType  = PIn.PInt   (table.Rows[i][31].ToString());
				FamilyList[i].RecallInterval=PIn.PInt   (table.Rows[i][32].ToString());
				FamilyList[i].RecallStatus = PIn.PInt   (table.Rows[i][33].ToString());
				FamilyList[i].ImageFolder  = PIn.PString(table.Rows[i][34].ToString());
				FamilyList[i].AddrNote     = PIn.PString(table.Rows[i][35].ToString());
				FamilyList[i].FamFinUrgNote= PIn.PString(table.Rows[i][36].ToString());
				FamilyList[i].MedUrgNote   = PIn.PString(table.Rows[i][37].ToString());
				FamilyList[i].ApptModNote  = PIn.PString(table.Rows[i][38].ToString());
				FamilyList[i].StudentStatus= PIn.PString(table.Rows[i][39].ToString());
				FamilyList[i].SchoolName   = PIn.PString(table.Rows[i][40].ToString());
				FamilyList[i].ChartNumber  = PIn.PString(table.Rows[i][41].ToString());
				FamilyList[i].MedicaidID   = PIn.PString(table.Rows[i][42].ToString());
				FamilyList[i].Bal_0_30     = PIn.PDouble(table.Rows[i][43].ToString());
				FamilyList[i].Bal_31_60    = PIn.PDouble(table.Rows[i][44].ToString());
				FamilyList[i].Bal_61_90    = PIn.PDouble(table.Rows[i][45].ToString());
				FamilyList[i].BalOver90    = PIn.PDouble(table.Rows[i][46].ToString());
				FamilyList[i].InsEst       = PIn.PDouble(table.Rows[i][47].ToString());
				FamilyList[i].PrimaryTeeth = PIn.PString(table.Rows[i][48].ToString());
				FamilyList[i].BalTotal     = PIn.PDouble(table.Rows[i][49].ToString());
				FamilyList[i].EmployerNum  = PIn.PInt   (table.Rows[i][50].ToString());
				FamilyList[i].EmploymentNote=PIn.PString(table.Rows[i][51].ToString());
				FamilyList[i].Race         = (PatientRace)PIn.PInt(table.Rows[i][52].ToString());
				FamilyList[i].County       = PIn.PString(table.Rows[i][53].ToString());
				FamilyList[i].GradeSchool  = PIn.PString(table.Rows[i][54].ToString());
				FamilyList[i].GradeLevel   = (PatientGrade)PIn.PInt(table.Rows[i][55].ToString());
				FamilyList[i].Urgency      = (TreatmentUrgency)PIn.PInt(table.Rows[i][56].ToString());
				FamilyList[i].DateFirstVisit=PIn.PDate  (table.Rows[i][57].ToString());
				FamilyList[i].PriPending   = PIn.PBool  (table.Rows[i][58].ToString());
				FamilyList[i].SecPending   = PIn.PBool  (table.Rows[i][59].ToString());
				if(FamilyList[i].PatNum==patNum){
					cur=FamilyList[i];
					CurOld=FamilyList[i];
				}
				if(FamilyList[i].Guarantor==FamilyList[i].PatNum)
					GuarIndex=i;
			}
			PatIsLoaded=true;
			//InfoChanged=false;//unused?
		}

		///<summary>ONLY for new patients. Uses InsertID to fill PatNum.</summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO patient (lname,fname,middlei,preferred,patstatus,gender,"
				+"position,birthdate,ssn,address,address2,city,state,zip,hmphone,wkphone,wirelessphone,"
				+"guarantor,credittype,email,salutation,priplannum,prirelationship,secplannum,"
				+"secrelationship,estbalance,nextaptnum,priprov,secprov,feesched,billingtype,recallinterval,"
				+"recallstatus,imagefolder,addrnote,famfinurgnote,medurgnote,apptmodnote,"
				+"studentstatus,schoolname,chartnumber,medicaidid"
				+",Bal_0_30,Bal_31_60,Bal_61_90,BalOver90,insest,primaryteeth,BalTotal"
				+",EmployerNum,EmploymentNote,Race,County,GradeSchool,GradeLevel,Urgency,DateFirstVisit"
				+",PriPending,SecPending) VALUES("
				+"'"+POut.PString(cur.LName)+"', "
				+"'"+POut.PString(cur.FName)+"', "
				+"'"+POut.PString(cur.MiddleI)+"', "
				+"'"+POut.PString(cur.Preferred)+"', "
				+"'"+POut.PInt   ((int)cur.PatStatus)+"', "
				+"'"+POut.PInt   ((int)cur.Gender)+"', "
				+"'"+POut.PInt   ((int)cur.Position)+"', "
				+"'"+POut.PDate  (cur.Birthdate)+"', "
				+"'"+POut.PString(cur.SSN)+"', "
				+"'"+POut.PString(cur.Address)+"', "
				+"'"+POut.PString(cur.Address2)+"', "
				+"'"+POut.PString(cur.City)+"', "
				+"'"+POut.PString(cur.State)+"', "
				+"'"+POut.PString(cur.Zip)+"', "
				+"'"+POut.PString(cur.HmPhone)+"', "
				+"'"+POut.PString(cur.WkPhone)+"', "
				+"'"+POut.PString(cur.WirelessPhone)+"', "
				+"'"+POut.PInt   (cur.Guarantor)+"', "
				+"'"+POut.PString(cur.CreditType)+"', "
				+"'"+POut.PString(cur.Email)+"', "
				+"'"+POut.PString(cur.Salutation)+"', "
				+"'"+POut.PInt   (cur.PriPlanNum)+"', "
				+"'"+POut.PInt   ((int)cur.PriRelationship)+"', "
				+"'"+POut.PInt   (cur.SecPlanNum)+"', "
				+"'"+POut.PInt   ((int)cur.SecRelationship)+"', "
				+"'"+POut.PDouble(cur.EstBalance)+"', "
				+"'"+POut.PInt   (cur.NextAptNum)+"', "
				+"'"+POut.PInt   (cur.PriProv)+"', "
				+"'"+POut.PInt   (cur.SecProv)+"', "
				+"'"+POut.PInt   (cur.FeeSched)+"', "
				+"'"+POut.PInt   (cur.BillingType)+"', "
				+"'"+POut.PInt   (cur.RecallInterval)+"', "
				+"'"+POut.PInt   (cur.RecallStatus)+"', "
				+"'"+POut.PString(cur.ImageFolder)+"', "
				+"'"+POut.PString(cur.AddrNote)+"', "
				+"'"+POut.PString(cur.FamFinUrgNote)+"', "
				+"'"+POut.PString(cur.MedUrgNote)+"', "
				+"'"+POut.PString(cur.ApptModNote)+"', "
				+"'"+POut.PString(cur.StudentStatus)+"', "
				+"'"+POut.PString(cur.SchoolName)+"', "
				+"'"+POut.PString(cur.ChartNumber)+"', "
				+"'"+POut.PString(cur.MedicaidID)+"', "
				+"'"+POut.PDouble(cur.Bal_0_30)+"', "
				+"'"+POut.PDouble(cur.Bal_31_60)+"', "
				+"'"+POut.PDouble(cur.Bal_61_90)+"', "
				+"'"+POut.PDouble(cur.BalOver90)+"', "
				+"'"+POut.PDouble(cur.InsEst)+"', "
				+"'"+POut.PString(cur.PrimaryTeeth)+"', "
				+"'"+POut.PDouble(cur.BalTotal)+"', "
				+"'"+POut.PInt   (cur.EmployerNum)+"', "
				+"'"+POut.PString(cur.EmploymentNote)+"', "
				+"'"+POut.PInt   ((int)cur.Race)+"', "
				+"'"+POut.PString(cur.County)+"', "
				+"'"+POut.PString(cur.GradeSchool)+"', "
				+"'"+POut.PInt   ((int)cur.GradeLevel)+"', "
				+"'"+POut.PInt   ((int)cur.Urgency)+"', "
				+"'"+POut.PDate  (cur.DateFirstVisit)+"', "
				+"'"+POut.PBool  (cur.PriPending)+"', "
				+"'"+POut.PBool  (cur.SecPending)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			cur.PatNum=InsertID;
			//PatientNotes PatientNotes=new PatientNotes();
			//PatientNotes.SaveCur();
		}

		///<summary>Updates only the changed columns and returns the number of rows affected.</summary>
		public static int UpdateCur(){
			bool comma=false;
			string c = "UPDATE patient SET ";
			if(cur.LName!=CurOld.LName){
				c+="LName = '"     +POut.PString(cur.LName)+"'";
				comma=true;
			}
			if(cur.FName!=CurOld.FName){
				if(comma) c+=",";
				c+="FName = '"     +POut.PString(cur.FName)+"'";
				comma=true;
			}
			if(cur.MiddleI!=CurOld.MiddleI){
				if(comma) c+=",";
				c+="MiddleI = '"   +POut.PString(cur.MiddleI)+"'";
				comma=true;
			}
			if(cur.Preferred!=CurOld.Preferred){
				if(comma) c+=",";
				c+="Preferred = '" +POut.PString(cur.Preferred)+"'";
				comma=true;
			}
			if(cur.PatStatus!=CurOld.PatStatus){
				if(comma) c+=",";
				c+="PatStatus = '" +POut.PInt   ((int)cur.PatStatus)+"'";
				comma=true;
			}
			if(cur.Gender!=CurOld.Gender){
				if(comma) c+=",";
				c+="Gender = '"    +POut.PInt   ((int)cur.Gender)+"'";
				comma=true;
			}
			if(cur.Position!=CurOld.Position){
				if(comma) c+=",";
				c+="Position = '"  +POut.PInt   ((int)cur.Position)+"'";
				comma=true;
			}
			if(cur.Birthdate!=CurOld.Birthdate){
				if(comma) c+=",";
				c+="Birthdate = '" +POut.PDate  (cur.Birthdate)+"'";
				comma=true;
			}
			if(cur.SSN!=CurOld.SSN){
				if(comma) c+=",";
				c+="SSN = '"       +POut.PString(cur.SSN)+"'";
				comma=true;
			}
			if(cur.Address!=CurOld.Address){
				if(comma) c+=",";
				c+="Address = '"   +POut.PString(cur.Address)+"'";
				comma=true;
			}
			if(cur.Address2!=CurOld.Address2){
				if(comma) c+=",";
				c+="Address2 = '"  +POut.PString(cur.Address2)+"'";
				comma=true;
			}
			if(cur.City!=CurOld.City){
				if(comma) c+=",";
				c+="City = '"      +POut.PString(cur.City)+"'";
				comma=true;
			}
			if(cur.State!=CurOld.State){
				if(comma) c+=",";
				c+="State = '"     +POut.PString(cur.State)+"'";
				comma=true;
			}
			if(cur.Zip!=CurOld.Zip){
				if(comma) c+=",";
				c+="Zip = '"       +POut.PString(cur.Zip)+"'";
				comma=true;
			}
			if(cur.HmPhone!=CurOld.HmPhone){
				if(comma) c+=",";
				c+="HmPhone = '"   +POut.PString(cur.HmPhone)+"'";
				comma=true;
			}
			if(cur.WkPhone!=CurOld.WkPhone){
				if(comma) c+=",";
				c+="WkPhone = '"   +POut.PString(cur.WkPhone)+"'";
				comma=true;
			}
			if(cur.WirelessPhone!=CurOld.WirelessPhone){
				if(comma) c+=",";
				c+="WirelessPhone='"    +POut.PString(cur.WirelessPhone)+"'";
				comma=true;
			}
			if(cur.Guarantor!=CurOld.Guarantor){
				if(comma) c+=",";
				c+="Guarantor = '"      +POut.PInt   (cur.Guarantor)+"'";
				comma=true;
			}
			if(cur.CreditType!=CurOld.CreditType){
				if(comma) c+=",";
				c+="CreditType = '"     +POut.PString(cur.CreditType)+"'";
				comma=true;
			}
			if(cur.Email!=CurOld.Email){
				if(comma) c+=",";
				c+="Email = '"          +POut.PString(cur.Email)+"'";
				comma=true;
			}
			if(cur.Salutation!=CurOld.Salutation){
				if(comma) c+=",";
				c+="Salutation = '"     +POut.PString(cur.Salutation)+"'";
				comma=true;
			}
			if(cur.PriPlanNum!=CurOld.PriPlanNum){
				if(comma) c+=",";
				c+="PriPlanNum = '"     +POut.PInt   (cur.PriPlanNum)+"'";
				comma=true;
			}
			if(cur.PriRelationship!=CurOld.PriRelationship){
				if(comma) c+=",";
				c+="PriRelationship = '"+POut.PInt((int)cur.PriRelationship)+"'";
				comma=true;
			}
			if(cur.SecPlanNum!=CurOld.SecPlanNum){
				if(comma) c+=",";
				c+="SecPlanNum = '"     +POut.PInt   (cur.SecPlanNum)+"'";
				comma=true;
			}
			if(cur.SecRelationship!=CurOld.SecRelationship){
				if(comma) c+=",";
				c+="SecRelationship = '"+POut.PInt((int)cur.SecRelationship)+"'";
				comma=true;
			}
			if(cur.EstBalance!=CurOld.EstBalance){
				if(comma) c+=",";
				c+="EstBalance = '"     +POut.PDouble(cur.EstBalance)+"'";
				comma=true;
			}
			if(cur.NextAptNum!=CurOld.NextAptNum){
				if(comma) c+=",";
				c+="NextAptNum = '"     +POut.PInt   (cur.NextAptNum)+"'";
				comma=true;
			}
			if(cur.PriProv!=CurOld.PriProv){
				if(comma) c+=",";
				c+="PriProv = '"        +POut.PInt   (cur.PriProv)+"'";
				comma=true;
			}
			if(cur.SecProv!=CurOld.SecProv){
				if(comma) c+=",";
				c+="SecProv = '"        +POut.PInt   (cur.SecProv)+"'";
				comma=true;
			}
			if(cur.FeeSched!=CurOld.FeeSched){
				if(comma) c+=",";
				c+="FeeSched = '"       +POut.PInt   (cur.FeeSched)+"'";
				comma=true;
			}
			if(cur.BillingType!=CurOld.BillingType){
				if(comma) c+=",";
				c+="BillingType = '"    +POut.PInt   (cur.BillingType)+"'";
				comma=true;
			}
			if(cur.RecallInterval!=CurOld.RecallInterval){
				if(comma) c+=",";
				c+="RecallInterval = '" +POut.PInt   (cur.RecallInterval)+"'";
				comma=true;
			}
			if(cur.RecallStatus!=CurOld.RecallStatus){
				if(comma) c+=",";
				c+="RecallStatus = '"   +POut.PInt   (cur.RecallStatus)+"'";
				comma=true;
			}
			if(cur.ImageFolder!=CurOld.ImageFolder){
				if(comma) c+=",";
				c+="ImageFolder = '"    +POut.PString(cur.ImageFolder)+"'";
				comma=true;
			}
			if(cur.AddrNote!=CurOld.AddrNote){
				if(comma) c+=",";
				c+="AddrNote = '"       +POut.PString(cur.AddrNote)+"'";
				comma=true;
			}
			if(cur.FamFinUrgNote!=CurOld.FamFinUrgNote){
				if(comma) c+=",";
				c+="FamFinUrgNote = '"  +POut.PString(cur.FamFinUrgNote)+"'";
				comma=true;
			}
			if(cur.MedUrgNote!=CurOld.MedUrgNote){
				if(comma) c+=",";
				c+="MedUrgNote = '"     +POut.PString(cur.MedUrgNote)+"'";
				comma=true;
			}
			if(cur.ApptModNote!=CurOld.ApptModNote){
				if(comma) c+=",";
				c+="ApptModNote = '"    +POut.PString(cur.ApptModNote)+"'";
				comma=true;
			}
			if(cur.StudentStatus!=CurOld.StudentStatus){
				if(comma) c+=",";
				c+="StudentStatus = '"  +POut.PString(cur.StudentStatus)+"'";
				comma=true;
			}
			if(cur.SchoolName!=CurOld.SchoolName){
				if(comma) c+=",";
				c+="SchoolName = '"     +POut.PString(cur.SchoolName)+"'";
				comma=true;
			}
			if(cur.ChartNumber!=CurOld.ChartNumber){
				if(comma) c+=",";
				c+="ChartNumber = '"    +POut.PString(cur.ChartNumber)+"'";
				comma=true;
			}
			if(cur.MedicaidID!=CurOld.MedicaidID){
				if(comma) c+=",";
				c+="MedicaidID = '"     +POut.PString(cur.MedicaidID)+"'";
				comma=true;
			}
			if(cur.Bal_0_30!=CurOld.Bal_0_30){
				if(comma) c+=",";
				c+="Bal_0_30 = '"       +POut.PDouble(cur.Bal_0_30)+"'";
				comma=true;
			}
			if(cur.Bal_31_60!=CurOld.Bal_31_60){
				if(comma) c+=",";
				c+="Bal_31_60 = '"      +POut.PDouble(cur.Bal_31_60)+"'";
				comma=true;
			}
			if(cur.Bal_61_90!=CurOld.Bal_61_90){
				if(comma) c+=",";
				c+="Bal_61_90 = '"      +POut.PDouble(cur.Bal_61_90)+"'";
				comma=true;
			}
			if(cur.BalOver90!=CurOld.BalOver90){
				if(comma) c+=",";
				c+="BalOver90 = '"      +POut.PDouble(cur.BalOver90)+"'";
				comma=true;
			}
			if(cur.InsEst!=CurOld.InsEst){
				if(comma) c+=",";
				c+="InsEst    = '"      +POut.PDouble(cur.InsEst)+"'";
				comma=true;
			}
			if(cur.PrimaryTeeth!=CurOld.PrimaryTeeth){
				if(comma) c+=",";
				c+="PrimaryTeeth = '"   +POut.PString(cur.PrimaryTeeth)+"'";
				comma=true;
			}
			if(cur.BalTotal!=CurOld.BalTotal){
				if(comma) c+=",";
				c+="BalTotal = '"       +POut.PDouble(cur.BalTotal)+"'";
				comma=true;
			}
			if(cur.EmployerNum!=CurOld.EmployerNum){
				if(comma) c+=",";
				c+="EmployerNum = '"    +POut.PInt   (cur.EmployerNum)+"'";
				comma=true;
			}
			if(cur.EmploymentNote!=CurOld.EmploymentNote){
				if(comma) c+=",";
				c+="EmploymentNote = '" +POut.PString(cur.EmploymentNote)+"'";
				comma=true;
			}
			if(cur.Race!=CurOld.Race){
				if(comma) c+=",";
				c+="Race = '"           +POut.PInt   ((int)cur.Race)+"'";
				comma=true;
			}
			if(cur.County!=CurOld.County){
				if(comma) c+=",";
				c+="County = '"         +POut.PString(cur.County)+"'";
				comma=true;
			}
			if(cur.GradeSchool!=CurOld.GradeSchool){
				if(comma) c+=",";
				c+="GradeSchool = '"    +POut.PString(cur.GradeSchool)+"'";
				comma=true;
			}
			if(cur.GradeLevel!=CurOld.GradeLevel){
				if(comma) c+=",";
				c+="GradeLevel = '"     +POut.PInt   ((int)cur.GradeLevel)+"'";
				comma=true;
			}
			if(cur.Urgency!=CurOld.Urgency){
				if(comma) c+=",";
				c+="Urgency = '"        +POut.PInt   ((int)cur.Urgency)+"'";
				comma=true;
			}
			if(cur.DateFirstVisit!=CurOld.DateFirstVisit){
				if(comma) c+=",";
				c+="DateFirstVisit = '" +POut.PDate  (cur.DateFirstVisit)+"'";
				comma=true;
			}
			if(cur.PriPending!=CurOld.PriPending){
				if(comma) c+=",";
				c+="PriPending = '"     +POut.PBool  (cur.PriPending)+"'";
				comma=true;
			}
			if(cur.SecPending!=CurOld.SecPending){
				if(comma) c+=",";
				c+="SecPending = '"     +POut.PBool  (cur.SecPending)+"'";
				comma=true;
			}
			if(!comma)
				return 0;//this means no change is actually required.
			c+=" WHERE PatNum = '"   +POut.PInt   (cur.PatNum)+"'";
			cmd.CommandText=c;
			//MessageBox.Show(cmd.CommandText);
			return NonQ();
		}//end UpdatePatient

		///<summary>Only used when entering a new patient and user clicks cancel. To delete an existing patient, the PatStatus is simply changed to 4.</summary>
		public static void DeleteCur(){
			cmd.CommandText="DELETE FROM patient WHERE PatNum = '"+cur.PatNum.ToString()+"'";
			NonQ();
		}

		///<summary></summary>
		public static bool GetPtDataTable(bool limit,string lname,string fname,string hmphone,string address,bool hideInactive,string city,string state,string ssn,string patnum,string chartnumber,int[] billingtypes,bool guarOnly){
			//string psearchStr = POut.PString(searchStr)+"%";
			//Only used for the Select Patient dialog
			bool retval=false;
			string billingsnippet="";
			for(int i=0;i<billingtypes.Length;i++){
				if(i==0){
					billingsnippet+="AND (";
				}
				else{
					billingsnippet+="|| ";
				}
				billingsnippet+="BillingType ='"+billingtypes[i].ToString()+"' ";
				if(i==billingtypes.Length-1){//if there is only one row, this will also be triggered.
					billingsnippet+=") ";
				}
			}
			cmd.CommandText = 
				"SELECT PatNum,LName,FName,MiddleI,Preferred,Birthdate,SSN,HmPhone,Address,PatStatus,BillingType"
				+",ChartNumber,City,State "
				+"FROM patient "
				+"WHERE PatStatus != '4' "//not status 'deleted'
				+"AND LName LIKE '"      +POut.PString(lname)+"%' "
				+"AND FName LIKE '"      +POut.PString(fname)+"%' "
				+"AND HmPhone LIKE '"    +POut.PString(hmphone)+"%' "
				+"AND Address LIKE '"    +POut.PString(address)+"%' "
				+"AND City LIKE '"       +POut.PString(city)+"%' "
				+"AND State LIKE '"      +POut.PString(state)+"%' "
				+"AND PatNum LIKE '"     +POut.PString(patnum)+"%' "
				+"AND ChartNumber LIKE '"+POut.PString(chartnumber)+"%' "
				+billingsnippet;
			if(hideInactive){
				cmd.CommandText+="AND PatStatus = '0' ";
			}
			if(guarOnly){
				cmd.CommandText+="AND PatNum = Guarantor ";
			}
			cmd.CommandText+="ORDER BY LName,FName";
			if(limit)
				cmd.CommandText+=" LIMIT 36";//only need 35, but the extra will help indicate more rows
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			PtDataTable=table.Clone();
			//PtDataTable.Columns[0].DataType=typeof(int);//patnum
			for(int i=0;i<PtDataTable.Columns.Count;i++){
				PtDataTable.Columns[i].DataType=typeof(string);
			}
			if(limit && table.Rows.Count==36){
				retval=true;
			//	PtList=new Patient[44];
			}
			//else
				//PtList=new Patient[table.Rows.Count];
			DataRow r;
			for(int i=0;i<table.Rows.Count;i++){//table.Rows.Count && i<44;i++){
				r=PtDataTable.NewRow();
				r[0]=table.Rows[i][0].ToString();
				r[1]=table.Rows[i][1].ToString();
				r[2]=table.Rows[i][2].ToString();
				r[3]=table.Rows[i][3].ToString();
				r[4]=table.Rows[i][4].ToString();
				r[5]=Shared.DateToAge(PIn.PDate(table.Rows[i][5].ToString()));
				r[6]=table.Rows[i][6].ToString();
				r[7]=table.Rows[i][7].ToString();
				r[8]=table.Rows[i][8].ToString();
				r[9]=Lan.g("enumPatientStatus",
					((PatientStatus)PIn.PInt(table.Rows[i][9].ToString())).ToString());
				r[10]=Defs.GetName(DefCat.BillingTypes,PIn.PInt(table.Rows[i][10].ToString()));
				r[11]=table.Rows[i][11].ToString();
				r[12]=table.Rows[i][12].ToString();
				r[13]=table.Rows[i][13].ToString();
				PtDataTable.Rows.Add(r);

				/*
				PtList[i].PatNum     = PIn.PInt   (table.Rows[i][0].ToString());
				PtList[i].LName      = PIn.PString(table.Rows[i][1].ToString());
				PtList[i].FName      = PIn.PString(table.Rows[i][2].ToString());
				PtList[i].MiddleI    = PIn.PString(table.Rows[i][3].ToString());
				PtList[i].Preferred  = PIn.PString(table.Rows[i][4].ToString());
				PtList[i].Age        = Shared.DateToAge(PIn.PDate  (table.Rows[i][5].ToString()));
				PtList[i].SSN        = PIn.PString(table.Rows[i][6].ToString());
				PtList[i].HmPhone    = PIn.PString(table.Rows[i][7].ToString());
				PtList[i].Address    = PIn.PString(table.Rows[i][8].ToString());
				PtList[i].PatStatus  = (PatientStatus)PIn.PInt(table.Rows[i][9].ToString());
				PtList[i].BillingType= PIn.PInt(table.Rows[i][10].ToString());
				//chartnumber
				//city
				//state*/
			}
			return retval;//if true, there are more rows.
		}

		///<summary></summary>
		public static void GetRecallList(){
			cmd.CommandText = 
				"SELECT MAX(procedurelog.procdate) AS 'LastDate', "
				//+ INTERVAL patient.recallinterval MONTH AS 'DueDate', "
				+"CONCAT(patient.lname,', ',patient.fname,' ',patient.preferred,' ',"
				+"patient.middlei) AS 'Patient Name', "
				+"patient.birthdate,patient.recallinterval,patient.recallstatus,patient.patnum "
				//+"patient.nextaptnum,appointment.aptdatetime "
				+"FROM patient,procedurelog,procedurecode "
				//+"LEFT JOIN appointment ON appointment.nextaptnum = patient.nextaptnum "
				+"WHERE patient.patnum = procedurelog.patnum "
				+"AND procedurecode.adacode = procedurelog.adacode "
				+"AND procedurecode.setrecall = 1 "
				+"AND (procedurelog.procstatus = 2 "
				+"OR procedurelog.procstatus = 3 "
				+"OR procedurelog.procstatus = 4) "
				+"AND patient.patstatus = 0 "
				+"GROUP BY patient.patnum "
				+"ORDER BY LastDate";
				/* for future debugging:
SELECT MAX(procedurelog.procdate) + INTERVAL patient.recallinterval MONTH AS 'DueDate',
CONCAT(patient.lname,', ',patient.fname,' ',patient.preferred,' ',
patient.middlei) AS 'Patient Name',
patient.birthdate,patient.recallinterval,patient.recallstatus,patient.patnum,
patient.nextaptnum,appointment.aptdatetime
FROM patient,procedurelog,procedurecode
LEFT JOIN appointment ON appointment.nextaptnum = patient.nextaptnum
WHERE patient.patnum = procedurelog.patnum
AND procedurecode.adacode = procedurelog.adacode 
AND procedurecode.setrecall = 1
AND (procedurelog.procstatus = 2
OR procedurelog.procstatus = 3
OR procedurelog.procstatus = 4)
AND patient.patstatus = 0
GROUP BY patient.patnum
ORDER BY DueDate
			*/
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			RecallList = new RecallItem[table.Rows.Count];
			DateTime[] orderDate=new DateTime[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				RecallList[i].DueDate = (PIn.PDate  (table.Rows[i][0].ToString()))//last date
					.AddMonths(PIn.PInt(table.Rows[i][3].ToString()));//plus number of months
				RecallList[i].PatientName   = PIn.PString(table.Rows[i][1].ToString());
				RecallList[i].BirthDate     = PIn.PDate  (table.Rows[i][2].ToString());
				RecallList[i].RecallInterval= PIn.PInt   (table.Rows[i][3].ToString());
				RecallList[i].RecallStatus  = PIn.PInt   (table.Rows[i][4].ToString());
				RecallList[i].PatNum        = PIn.PInt   (table.Rows[i][5].ToString());
				RecallList[i].Age=Shared.DateToAge(RecallList[i].BirthDate);
				orderDate[i]=RecallList[i].DueDate;
			}
			Array.Sort(orderDate,RecallList);
		}

		///<summary></summary>
		public static string GetNameInFamLF(int myPatNum){
			string retStr="";
			for(int i=0;i<FamilyList.Length;i++){
				if(FamilyList[i].PatNum==myPatNum){
					if(FamilyList[i].Preferred==""){
						retStr=FamilyList[i].LName+", "+FamilyList[i].FName+" "+FamilyList[i].MiddleI; 
					}
					else{
						retStr=FamilyList[i].LName+", '"+FamilyList[i].Preferred+"' "+FamilyList[i].FName+" "+FamilyList[i].MiddleI;
					}
				}
			}
			return retStr;
		}

		///<summary>Gets last, (preferred) first middle</summary>
		public static string GetNameInFamLFI(int myi){
			string retStr="";
			if(FamilyList[myi].Preferred==""){
				retStr=FamilyList[myi].LName+", "+FamilyList[myi].FName+" "+FamilyList[myi].MiddleI; 
			}
			else{
				retStr=FamilyList[myi].LName+", '"+FamilyList[myi].Preferred+"' "+FamilyList[myi].FName+" "+FamilyList[myi].MiddleI;
			}
			return retStr;
		}

		///<summary></summary>
		public static string GetNameInFamFL(int myPatNum){
			string retStr="";
			for(int i=0;i<FamilyList.Length;i++){
				if(FamilyList[i].PatNum==myPatNum){
					if(FamilyList[i].Preferred==""){
						retStr=FamilyList[i].FName+" "+FamilyList[i].MiddleI+" "+FamilyList[i].LName; 
					}
					else{
						retStr="'"+FamilyList[i].Preferred+"' "+FamilyList[i].FName+" "+FamilyList[i].MiddleI+" "+FamilyList[i].LName;
					}
				}
			}
			return retStr;
		}

		///<summary>Gets (preferred)first middle last</summary>
		public static string GetNameInFamFIL(int myi){
			string retStr="";
			if(FamilyList[myi].Preferred==""){
				retStr=FamilyList[myi].FName+" "+FamilyList[myi].MiddleI+" "+FamilyList[myi].LName; 
			}
			else{
				retStr="'"+FamilyList[myi].Preferred+"' "+FamilyList[myi].FName+" "+FamilyList[myi].MiddleI+" "+FamilyList[myi].LName;
			}
			return retStr;
		}

		/// <summary>Gets nine of the most useful fields from the db for the given patnums, saving the data
		/// in Lim and LimName.</summary>
		/// <param name="patNum">The PatNum to use in retrieving the data.</param>
		public static void GetLim(int patNum){
			if(patNum==0){
				Lim=new Patient();
				LimName="";
				return;
			}
			cmd.CommandText = 
				"SELECT PatNum,lname,fname,middlei,preferred,credittype,guarantor,priplannum,SSN " 
				+"FROM patient "
				+"WHERE PatNum = '"+patNum.ToString()+"'";
			FillTable();
			if(table.Rows.Count==0){
				Lim=new Patient();
				LimName="";
				return;
			}
			Lim.PatNum     = PIn.PInt   (table.Rows[0][0].ToString());
			Lim.LName      = PIn.PString(table.Rows[0][1].ToString());
			Lim.FName      = PIn.PString(table.Rows[0][2].ToString());
			Lim.MiddleI    = PIn.PString(table.Rows[0][3].ToString());
			Lim.Preferred  = PIn.PString(table.Rows[0][4].ToString());
			Lim.CreditType = PIn.PString(table.Rows[0][5].ToString());
			Lim.Guarantor  = PIn.PInt   (table.Rows[0][6].ToString());
			Lim.PriPlanNum = PIn.PInt   (table.Rows[0][7].ToString());
			Lim.SSN        = PIn.PString(table.Rows[0][8].ToString());
			if (Lim.Preferred=="")
				LimName=Lim.LName+", "+Lim.FName+" "+Lim.MiddleI;
			else LimName=Lim.LName+", '"+Lim.Preferred+"' "+Lim.FName+" "+Lim.MiddleI;
		}

		///<summary></summary>
		public static string GetCurNameLF(){
			if(cur.Preferred=="")
				return cur.LName+", "+cur.FName+" "+cur.MiddleI;
			else
				return cur.LName+", '"+cur.Preferred+"' "+cur.FName+" "+cur.MiddleI;
		}

		///<summary></summary>
		public static string GetCurNameFL(){
			if(cur.Preferred=="")
				return cur.FName+" "+cur.MiddleI+" "+cur.LName;
			else
				return cur.FName+" '"+cur.Preferred+"' "+cur.MiddleI+" "+cur.LName;
		}

		///<summary></summary>
		public static string GetLimCreditIns(){
			string retStr="";
			if(Lim.CreditType=="")
				retStr+=" ";
			else retStr+=Lim.CreditType;
			if(Lim.PriPlanNum==0)
				retStr+=" ";
			else retStr+="I";
			return retStr;
		}

		///<summary></summary>
		public static string GetCreditIns(){
			string retStr="";
			if(cur.CreditType=="")
				retStr+=" ";
			else retStr+=cur.CreditType;
			if(cur.PriPlanNum==0)
				retStr+=" ";
			else retStr+="I";	
			return retStr;
		}

		///<summary></summary>
		public static int GetIndex(int patNum){
			int retVal=-1;//will return -1 if not found
			for(int i=0;i<FamilyList.Length;i++){
				if(FamilyList[i].PatNum==patNum){
					retVal=i;
				}
			}
			return retVal;
		}

		//public static void DeleteCur(){
		//this does not actually delete the patient, just changes status
		//and they will never show up on the selection list again
			
		//NonQ(false);
		//MessageBox.Show("Patient Deleted");
		//}

		///<summary></summary>
		public static void ChangeGuarantorToCur(){
			//Move famfinurgnote to current patient:
			cmd.CommandText = 
				"UPDATE patient SET "
				//+"famaddrnote = '"  +FamilyList[GuarIndex].FamAddrNote+"', "
				+"famfinurgnote = '"+FamilyList[GuarIndex].FamFinUrgNote+"' "
				+"WHERE patnum = '"+cur.PatNum.ToString()+"'";
			NonQ(false);
			cmd.CommandText = 
				"UPDATE patient SET "
				//+"famaddrnote = '', "
				+"famfinurgnote = '' "
				+"WHERE patnum = '"+cur.Guarantor.ToString()+"'";
			NonQ(false);
			//Move family financial note to current patient:
			cmd.CommandText="SELECT FamFinancial FROM patientnote "
				+"WHERE patnum = '"+cur.Guarantor.ToString()+"'";
			FillTable();
			if(table.Rows.Count==1){
				cmd.CommandText = 
					"UPDATE patientnote SET "
					+"famfinancial = '"+table.Rows[0][0].ToString()+"' "
					+"WHERE patnum = '"+cur.PatNum.ToString()+"'";
				NonQ(false);
			}
			cmd.CommandText = 
				"UPDATE patientnote SET "
				+"famfinancial = ''"
				+"WHERE patnum = '"+cur.Guarantor.ToString()+"'";
			NonQ(false);
			//change guarantor of all family members:
			cmd.CommandText = 
				"UPDATE patient SET "
				+"guarantor = '"+cur.PatNum.ToString()+"' "
				+"WHERE guarantor = '"+cur.Guarantor.ToString()+"'";
			NonQ(false);
		}
		
		///<summary></summary>
		public static void CombineGuarantors(){
			//concat cur notes with guarantor notes
			cmd.CommandText = 
				"UPDATE patient SET "
				//+"addrnote = '"+POut.PString(FamilyList[GuarIndex].FamAddrNote)
				//									+POut.PString(cur.FamAddrNote)+"', "
				+"famfinurgnote = '"+POut.PString(FamilyList[GuarIndex].FamFinUrgNote)
				+POut.PString(cur.FamFinUrgNote)+"' "
				+"WHERE patnum = '"+cur.Guarantor.ToString()+"'";
			NonQ(false);
			//delete cur notes
			cmd.CommandText = 
				"UPDATE patient SET "
				//+"famaddrnote = '', "
				+"famfinurgnote = '' "
				+"WHERE patnum = '"+cur.PatNum+"'";
			NonQ(false);
			//concat family financial notes
			PatientNotes PatientNotes=new PatientNotes();
			PatientNotes.Refresh();
			//patientnote table must have been refreshed for this to work.
			//Makes sure there are entries for patient and for guarantor.
			//Also, PatientNotes.cur.FamFinancial will now have the guar info in it.
			string strGuar=PatientNotes.Cur.FamFinancial;
			cmd.CommandText = 
				"SELECT famfinancial "
				+"FROM patientnote WHERE patnum ='"+POut.PInt(cur.PatNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			string strCur=PIn.PString(table.Rows[0][0].ToString());
			cmd.CommandText = 
				"UPDATE patientnote SET "
				+"famfinancial = '"+strGuar+strCur+"' "
				+"WHERE patnum = '"+cur.Guarantor.ToString()+"'";
			NonQ(false);
			//delete cur financial notes
			cmd.CommandText = 
				"UPDATE patientnote SET "
				+"famfinancial = ''"
				+"WHERE patnum = '"+cur.PatNum.ToString()+"'";
			NonQ(false);
		}

		///<summary>Gets names for all patients.</summary>
		public static void GetHList(){
			cmd.CommandText="SELECT patnum,lname,fname,middlei,preferred "
				+"FROM patient";
			FillTable();
			HList=new Hashtable(table.Rows.Count);
			int patnum;
			string lname,fname,middlei,preferred;
			for(int i=0;i<table.Rows.Count;i++){
				patnum=PIn.PInt(table.Rows[i][0].ToString());
				lname=PIn.PString(table.Rows[i][1].ToString());
				fname=PIn.PString(table.Rows[i][2].ToString());
				middlei=PIn.PString(table.Rows[i][3].ToString());
				preferred=PIn.PString(table.Rows[i][4].ToString());
				if(preferred==""){
					HList.Add(patnum,lname+", "+fname+" "+middlei);
				}
				else{
					HList.Add(patnum,lname+", '"+preferred+"' "+fname+" "+middlei);
				}
			}
		}

		///<summary></summary>
		public static void UpdateAddressForFam(){
			cmd.CommandText = "UPDATE patient SET " 
				+"Address = '"    +POut.PString(cur.Address)+"'"
				+",Address2 = '"   +POut.PString(cur.Address2)+"'"
				+",City = '"       +POut.PString(cur.City)+"'"
				+",State = '"      +POut.PString(cur.State)+"'"
				+",Zip = '"        +POut.PString(cur.Zip)+"'"
				+",HmPhone = '"    +POut.PString(cur.HmPhone)+"'"
				+",credittype = '" +POut.PString(cur.CreditType)+"'"
				+",priprov = '"    +POut.PInt   (cur.PriProv)+"'"
				+",secprov = '"    +POut.PInt   (cur.SecProv)+"'"
				+",feesched = '"   +POut.PInt   (cur.FeeSched)+"'"
				+",billingtype = '"+POut.PInt   (cur.BillingType)+"'"
				+" WHERE guarantor = '"+POut.PDouble(cur.Guarantor)+"'";
			NonQ(false);
			//MessageBox.Show(cmd.CommandText);
		}

		///<summary></summary>
		public static void UpdateNotesForFam(){
			cmd.CommandText = "UPDATE patient SET " 
				+"addrnote = '"   +POut.PString(cur.AddrNote)+"'"
				+" WHERE guarantor = '"+POut.PDouble(cur.Guarantor)+"'";
			NonQ(false);
			//MessageBox.Show(cmd.CommandText);
		}

		///<summary>This is only used in the Billing dialog</summary>
		public static void GetAgingList(string age,int[] billingIndices,bool excludeAddr
			,bool excludeNeg,double excludeLessThan,bool excludeInactive){
			cmd.CommandText=
				"SELECT patnum,Bal_0_30,Bal_31_60,Bal_61_90,BalOver90,BalTotal,InsEst,LName,FName,MiddleI "
				+"FROM patient WHERE ";//actually only gets guarantors since others are 0.
			if(excludeInactive){
				cmd.CommandText+="(patstatus != '2') && ";
			}
			cmd.CommandText+="(BalTotal - InsEst > '"+excludeLessThan.ToString()+"'";
			if(!excludeNeg){
				cmd.CommandText+=" || BalTotal - InsEst < '0')";
			}
			else{
				cmd.CommandText+=")";
			}
			switch(age){
				//where is age 0. Is it missing because no restriction
				case "30":
					cmd.CommandText+=" && (Bal_31_60 > '0' || Bal_61_90 > '0' || BalOver90 > '0')";
					break;
				case "60":
					cmd.CommandText+=" && (Bal_61_90 > '0' || BalOver90 > '0')";
					break;
				case "90":
					cmd.CommandText+=" && (BalOver90 > '0')";
					break;
			}
			for(int i=0;i<billingIndices.Length;i++){
				if(i==0){
					cmd.CommandText+=" && (billingtype = '";
				}
				else{
					cmd.CommandText+=" || billingtype = '";
				}
				cmd.CommandText+=
					Defs.Short[(int)DefCat.BillingTypes][billingIndices[i]].DefNum.ToString()+"'";
			}
			cmd.CommandText+=")";
			if(excludeAddr){
				cmd.CommandText+=" && (zip !='')";
			}	
			cmd.CommandText+=" ORDER BY LName,FName";
			FillTable();
			AgingList=new PatAging[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				AgingList[i].PatNum   = PIn.PInt   (table.Rows[i][0].ToString());
				AgingList[i].Bal_0_30 = PIn.PDouble(table.Rows[i][1].ToString());
				AgingList[i].Bal_31_60= PIn.PDouble(table.Rows[i][2].ToString());
				AgingList[i].Bal_61_90= PIn.PDouble(table.Rows[i][3].ToString());
				AgingList[i].BalOver90= PIn.PDouble(table.Rows[i][4].ToString());
				AgingList[i].BalTotal = PIn.PDouble(table.Rows[i][5].ToString());
				AgingList[i].InsEst   = PIn.PDouble(table.Rows[i][6].ToString());
				AgingList[i].PatName=PIn.PString(table.Rows[i][7].ToString())
					+", "+PIn.PString(table.Rows[i][8].ToString())
					+" "+PIn.PString(table.Rows[i][9].ToString());;
				//AgingList[i].Balance=AgingList[i].Bal_0_30+AgingList[i].Bal_31_60
				//	+AgingList[i].Bal_61_90+AgingList[i].BalOver90;
				AgingList[i].AmountDue=AgingList[i].BalTotal-AgingList[i].InsEst;
			}
		}

		///<summary></summary>
		public static void GetAgingList(){
			//used only to run finance charges, so it ignores negative balances
			cmd.CommandText =
				"SELECT patnum,Bal_0_30,Bal_31_60,Bal_61_90,BalOver90,BalTotal,InsEst,LName,FName,MiddleI,priprov "
				+"FROM patient "//actually only gets guarantors since others are 0.
				+" WHERE Bal_0_30 + Bal_31_60 + Bal_61_90 + BalOver90 - InsEst > '0.005'"//more that 1/2 cent
				+" ORDER BY LName,FName";
			FillTable();
			AgingList=new PatAging[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				AgingList[i].PatNum   = PIn.PInt   (table.Rows[i][0].ToString());
				AgingList[i].Bal_0_30 = PIn.PDouble(table.Rows[i][1].ToString());
				AgingList[i].Bal_31_60= PIn.PDouble(table.Rows[i][2].ToString());
				AgingList[i].Bal_61_90= PIn.PDouble(table.Rows[i][3].ToString());
				AgingList[i].BalOver90= PIn.PDouble(table.Rows[i][4].ToString());
				AgingList[i].BalTotal = PIn.PDouble(table.Rows[i][5].ToString());
				AgingList[i].InsEst   = PIn.PDouble(table.Rows[i][6].ToString());
				AgingList[i].PatName=PIn.PString(table.Rows[i][7].ToString())
					+", "+PIn.PString(table.Rows[i][8].ToString())
					+" "+PIn.PString(table.Rows[i][9].ToString());;
				//AgingList[i].Balance=AgingList[i].Bal_0_30+AgingList[i].Bal_31_60
				//	+AgingList[i].Bal_61_90+AgingList[i].BalOver90;
				AgingList[i].AmountDue=AgingList[i].BalTotal-AgingList[i].InsEst;
				AgingList[i].PriProv=PIn.PInt(table.Rows[i][10].ToString());
			}
		}

		///<summary></summary>
		public static void ResetAging(){//for entire database
			//need to zero everything out first because the update aging only inserts non-zero values
			cmd.CommandText="Update patient SET "
				+"Bal_0_30   = '0'"
				+",Bal_31_60 = '0'"
				+",Bal_61_90 = '0'"
				+",BalOver90 = '0'"
				+",InsEst    = '0'"
				+",BalTotal  = '0'";
			NonQ(false);		
		}

		///<summary></summary>
		public static void ResetAging(int guarantor){
			cmd.CommandText="Update patient SET "
				+"Bal_0_30   = '0'"
				+",Bal_31_60 = '0'"
				+",Bal_61_90 = '0'"
				+",BalOver90 = '0'"
				+",InsEst    = '0'"
				+",BalTotal  = '0'"
			  +" WHERE guarantor = '"+POut.PInt(guarantor)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void UpdateAging(int patnum,double Bal0,double Bal31
			,double Bal61,double Bal91,double InsEst,double BalTotal){
			cmd.CommandText="Update patient SET "
				+"Bal_0_30        = '" +POut.PDouble(Bal0)+"'"
				+",Bal_31_60      = '" +POut.PDouble(Bal31)+"'"
				+",Bal_61_90      = '" +POut.PDouble(Bal61)+"'"
				+",BalOver90      = '" +POut.PDouble(Bal91)+"'"
				+",InsEst         = '" +POut.PDouble(InsEst)+"'"
				+",BalTotal       = '" +POut.PDouble(BalTotal)+"'"
				+" WHERE patnum   = '" +POut.PInt   (patnum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static int GetProvForCur(){
			if(cur.PriProv!=0)
				return cur.PriProv;
			if(PIn.PInt(((Pref)Prefs.HList["PracticeDefaultProv"]).ValueString)==0){
				MessageBox.Show(Lan.g("Patients","Please set a default provider in the practice setup window."));
				return Providers.List[0].ProvNum;
			}
			return PIn.PInt(((Pref)Prefs.HList["PracticeDefaultProv"]).ValueString);
		}

		///<summary>Gets the next available integer chart number.  Willl later add a where clause based on preferred format.</summary>
		public static string GetNextChartNum(){
			cmd.CommandText="SELECT ChartNumber from patient WHERE"
				+" ChartNumber REGEXP '^[0-9]+$'"//matches any number of digits
				+" ORDER BY chartnumber DESC LIMIT 1";
			FillTable();
			if(table.Rows.Count==0){//no existing chart numbers
				return "1";
			}
			string lastChartNum=PIn.PString(table.Rows[0][0].ToString());
			//or could add more match conditions
			//if(Regex.IsMatch(lastChartNum,@"^\d+$")){//if is an integer
			return(PIn.PInt(lastChartNum)+1).ToString();
			//}
			//return "1";//if there are no integer chartnumbers yet
		}

		///<summary>Returns the name(only one) of the patient using this chartnumber.</summary>
		public static string ChartNumUsedBy(string chartNum,int excludePatNum){
			cmd.CommandText="SELECT LName,FName from patient WHERE "
				+"ChartNumber = '"+chartNum
				+"' && PatNum != '"+excludePatNum.ToString()+"'";
			FillTable();
			string retVal="";
			if(table.Rows.Count!=0){//found duplicate chart number
				retVal=PIn.PString(table.Rows[0][1].ToString())+" "+PIn.PString(table.Rows[0][0].ToString());
			}
			return retVal;
		}

		///<summary>Used in the patient select window to determine if a trial version user is over their limit.</summary>
		public static int GetNumberPatients(){
			cmd.CommandText="SELECT Count(*) FROM patient";
			FillTable();
			return PIn.PInt(table.Rows[0][0].ToString());
		}

	}

	

	///<summary>Not a database table.  Just used for running reports.</summary>
	public struct PatAging{
		///<summary></summary>
		public int PatNum;
		///<summary></summary>
		public double Bal_0_30;
		///<summary></summary>
		public double Bal_31_60;
		///<summary></summary>
		public double Bal_61_90;
		///<summary></summary>
		public double BalOver90;
		///<summary></summary>
		public double InsEst;
		///<summary></summary>
		public string PatName;
		///<summary></summary>
		public double BalTotal;
		///<summary></summary>
		public double AmountDue;
		///<summary>The patient priprov to assign the finance charge to.</summary>
		public int PriProv;
	}

	


}










