using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the patient table in the database.</summary>
	public class Patient{
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
		public int    Age;
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
		///<summary>May be 0(none) or -1(done), otherwise it is the foreign key to appointment.AptNum.  This is the appointment that will show in the Chart module and in the Next appointment tracker.  It will never show in the Appointments module. In other words, it is the suggested next appoinment rather than an appointment that has already been scheduled.</summary>
		public int NextAptNum;//
		///<summary>Foreign key to provider.ProvNum.  The patient's primary provider.</summary>
		public int PriProv;
		///<summary>Foreign key to provider.ProvNum.  Secondary provider (hygienist)</summary>
		public int SecProv;//
		///<summary>Foreign key to definition.DefNum.  Fee schedule for this patient.</summary>
		public int FeeSched;
		///<summary>Foreign key to definition.DefNum.  Must have a value, or the patient will not show on some reports.</summary>
		public int BillingType;
		///<summary>No longer used.  See the recall table instead.</summary>
		public int OldRecallInterval;
		///<summary>No longer used.  See the recall table instead.</summary>
		public int OldRecallStatus;
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
		//<summary>Decided not to add since this data is already available and synchronizing would take too much time.  Will add later.  Not editable. If the patient happens to have a future appointment, this will contain the date of that appointment.  Once appointment is set complete, this date is deleted.  If there is more than one appointment scheduled, this will only contain the earliest one.  Used mostly to exclude patients from recall lists.  If you want all future appointments, use Appointments.GetForPat() instead. You can loop through that list and exclude appointments with dates earlier than today.</summary>
		//public DateTime DateScheduled;

		///<summary>Returns a copy of this Patient.</summary>
		public Patient Copy(){
			Patient p=new Patient();
			p.PatNum=PatNum;
			p.LName=LName;
			p.FName=FName;
			p.MiddleI=MiddleI;
			p.Preferred=Preferred;
			p.PatStatus=PatStatus;
			p.Gender=Gender;
			p.Position=Position;
			p.Birthdate=Birthdate;
			p.SSN=SSN;
			p.Address=Address;
			p.Address2=Address2;
			p.City=City;
			p.State=State;
			p.Zip=Zip;
			p.HmPhone=HmPhone;
			p.WkPhone=WkPhone;
			p.WirelessPhone=WirelessPhone;
			p.Guarantor=Guarantor;
			p.Age=Age;
			p.CreditType=CreditType;
			p.Email=Email;
			p.Salutation=Salutation;
			p.PriPlanNum=PriPlanNum;
			p.PriRelationship=PriRelationship;
			p.SecPlanNum=SecPlanNum;
			p.SecRelationship=SecRelationship;
			p.EstBalance=EstBalance;
			p.NextAptNum=NextAptNum;
			p.PriProv=PriProv;
			p.SecProv=SecProv;
			p.FeeSched=FeeSched;
			p.BillingType=BillingType;
			p.ImageFolder=ImageFolder;
			p.AddrNote=AddrNote;
			p.FamFinUrgNote=FamFinUrgNote;
			p.MedUrgNote=MedUrgNote;
			p.ApptModNote=ApptModNote;
			p.StudentStatus=StudentStatus;
			p.SchoolName=SchoolName;
			p.ChartNumber=ChartNumber;
			p.MedicaidID=MedicaidID;
			p.Bal_0_30=Bal_0_30;
			p.Bal_31_60=Bal_31_60;
			p.Bal_61_90=Bal_61_90;
			p.BalOver90=BalOver90;
			p.InsEst=InsEst;
			p.PrimaryTeeth=PrimaryTeeth;
			p.BalTotal=BalTotal;
			p.EmployerNum=EmployerNum;
			p.EmploymentNote=EmploymentNote;
			p.Race=Race;
			p.County=County;
			p.GradeSchool=GradeSchool;
			p.GradeLevel=GradeLevel;
			p.Urgency=Urgency;
			p.DateFirstVisit=DateFirstVisit;
			p.PriPending=PriPending;
			p.SecPending=SecPending;
			return p;
		}
	
		///<summary>ONLY for new patients. Uses InsertID to fill PatNum.</summary>
		public void Insert(){
			string command= "INSERT INTO patient (lname,fname,middlei,preferred,patstatus,gender,"
				+"position,birthdate,ssn,address,address2,city,state,zip,hmphone,wkphone,wirelessphone,"
				+"guarantor,credittype,email,salutation,priplannum,prirelationship,secplannum,"
				+"secrelationship,estbalance,nextaptnum,priprov,secprov,feesched,billingtype,"
				+"imagefolder,addrnote,famfinurgnote,medurgnote,apptmodnote,"
				+"studentstatus,schoolname,chartnumber,medicaidid"
				+",Bal_0_30,Bal_31_60,Bal_61_90,BalOver90,insest,primaryteeth,BalTotal"
				+",EmployerNum,EmploymentNote,Race,County,GradeSchool,GradeLevel,Urgency,DateFirstVisit"
				+",PriPending,SecPending) VALUES("
				+"'"+POut.PString(LName)+"', "
				+"'"+POut.PString(FName)+"', "
				+"'"+POut.PString(MiddleI)+"', "
				+"'"+POut.PString(Preferred)+"', "
				+"'"+POut.PInt   ((int)PatStatus)+"', "
				+"'"+POut.PInt   ((int)Gender)+"', "
				+"'"+POut.PInt   ((int)Position)+"', "
				+"'"+POut.PDate  (Birthdate)+"', "
				+"'"+POut.PString(SSN)+"', "
				+"'"+POut.PString(Address)+"', "
				+"'"+POut.PString(Address2)+"', "
				+"'"+POut.PString(City)+"', "
				+"'"+POut.PString(State)+"', "
				+"'"+POut.PString(Zip)+"', "
				+"'"+POut.PString(HmPhone)+"', "
				+"'"+POut.PString(WkPhone)+"', "
				+"'"+POut.PString(WirelessPhone)+"', "
				+"'"+POut.PInt   (Guarantor)+"', "
				+"'"+POut.PString(CreditType)+"', "
				+"'"+POut.PString(Email)+"', "
				+"'"+POut.PString(Salutation)+"', "
				+"'"+POut.PInt   (PriPlanNum)+"', "
				+"'"+POut.PInt   ((int)PriRelationship)+"', "
				+"'"+POut.PInt   (SecPlanNum)+"', "
				+"'"+POut.PInt   ((int)SecRelationship)+"', "
				+"'"+POut.PDouble(EstBalance)+"', "
				+"'"+POut.PInt   (NextAptNum)+"', "
				+"'"+POut.PInt   (PriProv)+"', "
				+"'"+POut.PInt   (SecProv)+"', "
				+"'"+POut.PInt   (FeeSched)+"', "
				+"'"+POut.PInt   (BillingType)+"', "
				//+"'"+POut.PInt   (RecallInterval)+"', "
				//+"'"+POut.PInt   (RecallStatus)+"', "
				+"'"+POut.PString(ImageFolder)+"', "
				+"'"+POut.PString(AddrNote)+"', "
				+"'"+POut.PString(FamFinUrgNote)+"', "
				+"'"+POut.PString(MedUrgNote)+"', "
				+"'"+POut.PString(ApptModNote)+"', "
				+"'"+POut.PString(StudentStatus)+"', "
				+"'"+POut.PString(SchoolName)+"', "
				+"'"+POut.PString(ChartNumber)+"', "
				+"'"+POut.PString(MedicaidID)+"', "
				+"'"+POut.PDouble(Bal_0_30)+"', "
				+"'"+POut.PDouble(Bal_31_60)+"', "
				+"'"+POut.PDouble(Bal_61_90)+"', "
				+"'"+POut.PDouble(BalOver90)+"', "
				+"'"+POut.PDouble(InsEst)+"', "
				+"'"+POut.PString(PrimaryTeeth)+"', "
				+"'"+POut.PDouble(BalTotal)+"', "
				+"'"+POut.PInt   (EmployerNum)+"', "
				+"'"+POut.PString(EmploymentNote)+"', "
				+"'"+POut.PInt   ((int)Race)+"', "
				+"'"+POut.PString(County)+"', "
				+"'"+POut.PString(GradeSchool)+"', "
				+"'"+POut.PInt   ((int)GradeLevel)+"', "
				+"'"+POut.PInt   ((int)Urgency)+"', "
				+"'"+POut.PDate  (DateFirstVisit)+"', "
				+"'"+POut.PBool  (PriPending)+"', "
				+"'"+POut.PBool  (SecPending)+"')";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			PatNum=dcon.InsertID;
		}

		///<summary>Updates only the changed columns and returns the number of rows affected.  Supply the old Patient object to compare for changes.</summary>
		public int Update(Patient CurOld){
			bool comma=false;
			string c = "UPDATE patient SET ";
			if(LName!=CurOld.LName){
				c+="LName = '"     +POut.PString(LName)+"'";
				comma=true;
			}
			if(FName!=CurOld.FName){
				if(comma) c+=",";
				c+="FName = '"     +POut.PString(FName)+"'";
				comma=true;
			}
			if(MiddleI!=CurOld.MiddleI){
				if(comma) c+=",";
				c+="MiddleI = '"   +POut.PString(MiddleI)+"'";
				comma=true;
			}
			if(Preferred!=CurOld.Preferred){
				if(comma) c+=",";
				c+="Preferred = '" +POut.PString(Preferred)+"'";
				comma=true;
			}
			if(PatStatus!=CurOld.PatStatus){
				if(comma) c+=",";
				c+="PatStatus = '" +POut.PInt   ((int)PatStatus)+"'";
				comma=true;
			}
			if(Gender!=CurOld.Gender){
				if(comma) c+=",";
				c+="Gender = '"    +POut.PInt   ((int)Gender)+"'";
				comma=true;
			}
			if(Position!=CurOld.Position){
				if(comma) c+=",";
				c+="Position = '"  +POut.PInt   ((int)Position)+"'";
				comma=true;
			}
			if(Birthdate!=CurOld.Birthdate){
				if(comma) c+=",";
				c+="Birthdate = '" +POut.PDate  (Birthdate)+"'";
				comma=true;
			}
			if(SSN!=CurOld.SSN){
				if(comma) c+=",";
				c+="SSN = '"       +POut.PString(SSN)+"'";
				comma=true;
			}
			if(Address!=CurOld.Address){
				if(comma) c+=",";
				c+="Address = '"   +POut.PString(Address)+"'";
				comma=true;
			}
			if(Address2!=CurOld.Address2){
				if(comma) c+=",";
				c+="Address2 = '"  +POut.PString(Address2)+"'";
				comma=true;
			}
			if(City!=CurOld.City){
				if(comma) c+=",";
				c+="City = '"      +POut.PString(City)+"'";
				comma=true;
			}
			if(State!=CurOld.State){
				if(comma) c+=",";
				c+="State = '"     +POut.PString(State)+"'";
				comma=true;
			}
			if(Zip!=CurOld.Zip){
				if(comma) c+=",";
				c+="Zip = '"       +POut.PString(Zip)+"'";
				comma=true;
			}
			if(HmPhone!=CurOld.HmPhone){
				if(comma) c+=",";
				c+="HmPhone = '"   +POut.PString(HmPhone)+"'";
				comma=true;
			}
			if(WkPhone!=CurOld.WkPhone){
				if(comma) c+=",";
				c+="WkPhone = '"   +POut.PString(WkPhone)+"'";
				comma=true;
			}
			if(WirelessPhone!=CurOld.WirelessPhone){
				if(comma) c+=",";
				c+="WirelessPhone='"    +POut.PString(WirelessPhone)+"'";
				comma=true;
			}
			if(Guarantor!=CurOld.Guarantor){
				if(comma) c+=",";
				c+="Guarantor = '"      +POut.PInt   (Guarantor)+"'";
				comma=true;
			}
			if(CreditType!=CurOld.CreditType){
				if(comma) c+=",";
				c+="CreditType = '"     +POut.PString(CreditType)+"'";
				comma=true;
			}
			if(Email!=CurOld.Email){
				if(comma) c+=",";
				c+="Email = '"          +POut.PString(Email)+"'";
				comma=true;
			}
			if(Salutation!=CurOld.Salutation){
				if(comma) c+=",";
				c+="Salutation = '"     +POut.PString(Salutation)+"'";
				comma=true;
			}
			if(PriPlanNum!=CurOld.PriPlanNum){
				if(comma) c+=",";
				c+="PriPlanNum = '"     +POut.PInt   (PriPlanNum)+"'";
				comma=true;
			}
			if(PriRelationship!=CurOld.PriRelationship){
				if(comma) c+=",";
				c+="PriRelationship = '"+POut.PInt((int)PriRelationship)+"'";
				comma=true;
			}
			if(SecPlanNum!=CurOld.SecPlanNum){
				if(comma) c+=",";
				c+="SecPlanNum = '"     +POut.PInt   (SecPlanNum)+"'";
				comma=true;
			}
			if(SecRelationship!=CurOld.SecRelationship){
				if(comma) c+=",";
				c+="SecRelationship = '"+POut.PInt((int)SecRelationship)+"'";
				comma=true;
			}
			if(EstBalance!=CurOld.EstBalance){
				if(comma) c+=",";
				c+="EstBalance = '"     +POut.PDouble(EstBalance)+"'";
				comma=true;
			}
			if(NextAptNum!=CurOld.NextAptNum){
				if(comma) c+=",";
				c+="NextAptNum = '"     +POut.PInt   (NextAptNum)+"'";
				comma=true;
			}
			if(PriProv!=CurOld.PriProv){
				if(comma) c+=",";
				c+="PriProv = '"        +POut.PInt   (PriProv)+"'";
				comma=true;
			}
			if(SecProv!=CurOld.SecProv){
				if(comma) c+=",";
				c+="SecProv = '"        +POut.PInt   (SecProv)+"'";
				comma=true;
			}
			if(FeeSched!=CurOld.FeeSched){
				if(comma) c+=",";
				c+="FeeSched = '"       +POut.PInt   (FeeSched)+"'";
				comma=true;
			}
			if(BillingType!=CurOld.BillingType){
				if(comma) c+=",";
				c+="BillingType = '"    +POut.PInt   (BillingType)+"'";
				comma=true;
			}
			/*if(RecallInterval!=CurOld.RecallInterval){
				if(comma) c+=",";
				c+="RecallInterval = '" +POut.PInt   (RecallInterval)+"'";
				comma=true;
			}
			if(RecallStatus!=CurOld.RecallStatus){
				if(comma) c+=",";
				c+="RecallStatus = '"   +POut.PInt   (RecallStatus)+"'";
				comma=true;
			}*/
			if(ImageFolder!=CurOld.ImageFolder){
				if(comma) c+=",";
				c+="ImageFolder = '"    +POut.PString(ImageFolder)+"'";
				comma=true;
			}
			if(AddrNote!=CurOld.AddrNote){
				if(comma) c+=",";
				c+="AddrNote = '"       +POut.PString(AddrNote)+"'";
				comma=true;
			}
			if(FamFinUrgNote!=CurOld.FamFinUrgNote){
				if(comma) c+=",";
				c+="FamFinUrgNote = '"  +POut.PString(FamFinUrgNote)+"'";
				comma=true;
			}
			if(MedUrgNote!=CurOld.MedUrgNote){
				if(comma) c+=",";
				c+="MedUrgNote = '"     +POut.PString(MedUrgNote)+"'";
				comma=true;
			}
			if(ApptModNote!=CurOld.ApptModNote){
				if(comma) c+=",";
				c+="ApptModNote = '"    +POut.PString(ApptModNote)+"'";
				comma=true;
			}
			if(StudentStatus!=CurOld.StudentStatus){
				if(comma) c+=",";
				c+="StudentStatus = '"  +POut.PString(StudentStatus)+"'";
				comma=true;
			}
			if(SchoolName!=CurOld.SchoolName){
				if(comma) c+=",";
				c+="SchoolName = '"     +POut.PString(SchoolName)+"'";
				comma=true;
			}
			if(ChartNumber!=CurOld.ChartNumber){
				if(comma) c+=",";
				c+="ChartNumber = '"    +POut.PString(ChartNumber)+"'";
				comma=true;
			}
			if(MedicaidID!=CurOld.MedicaidID){
				if(comma) c+=",";
				c+="MedicaidID = '"     +POut.PString(MedicaidID)+"'";
				comma=true;
			}
			if(Bal_0_30!=CurOld.Bal_0_30){
				if(comma) c+=",";
				c+="Bal_0_30 = '"       +POut.PDouble(Bal_0_30)+"'";
				comma=true;
			}
			if(Bal_31_60!=CurOld.Bal_31_60){
				if(comma) c+=",";
				c+="Bal_31_60 = '"      +POut.PDouble(Bal_31_60)+"'";
				comma=true;
			}
			if(Bal_61_90!=CurOld.Bal_61_90){
				if(comma) c+=",";
				c+="Bal_61_90 = '"      +POut.PDouble(Bal_61_90)+"'";
				comma=true;
			}
			if(BalOver90!=CurOld.BalOver90){
				if(comma) c+=",";
				c+="BalOver90 = '"      +POut.PDouble(BalOver90)+"'";
				comma=true;
			}
			if(InsEst!=CurOld.InsEst){
				if(comma) c+=",";
				c+="InsEst    = '"      +POut.PDouble(InsEst)+"'";
				comma=true;
			}
			if(PrimaryTeeth!=CurOld.PrimaryTeeth){
				if(comma) c+=",";
				c+="PrimaryTeeth = '"   +POut.PString(PrimaryTeeth)+"'";
				comma=true;
			}
			if(BalTotal!=CurOld.BalTotal){
				if(comma) c+=",";
				c+="BalTotal = '"       +POut.PDouble(BalTotal)+"'";
				comma=true;
			}
			if(EmployerNum!=CurOld.EmployerNum){
				if(comma) c+=",";
				c+="EmployerNum = '"    +POut.PInt   (EmployerNum)+"'";
				comma=true;
			}
			if(EmploymentNote!=CurOld.EmploymentNote){
				if(comma) c+=",";
				c+="EmploymentNote = '" +POut.PString(EmploymentNote)+"'";
				comma=true;
			}
			if(Race!=CurOld.Race){
				if(comma) c+=",";
				c+="Race = '"           +POut.PInt   ((int)Race)+"'";
				comma=true;
			}
			if(County!=CurOld.County){
				if(comma) c+=",";
				c+="County = '"         +POut.PString(County)+"'";
				comma=true;
			}
			if(GradeSchool!=CurOld.GradeSchool){
				if(comma) c+=",";
				c+="GradeSchool = '"    +POut.PString(GradeSchool)+"'";
				comma=true;
			}
			if(GradeLevel!=CurOld.GradeLevel){
				if(comma) c+=",";
				c+="GradeLevel = '"     +POut.PInt   ((int)GradeLevel)+"'";
				comma=true;
			}
			if(Urgency!=CurOld.Urgency){
				if(comma) c+=",";
				c+="Urgency = '"        +POut.PInt   ((int)Urgency)+"'";
				comma=true;
			}
			if(DateFirstVisit!=CurOld.DateFirstVisit){
				if(comma) c+=",";
				c+="DateFirstVisit = '" +POut.PDate  (DateFirstVisit)+"'";
				comma=true;
			}
			if(PriPending!=CurOld.PriPending){
				if(comma) c+=",";
				c+="PriPending = '"     +POut.PBool  (PriPending)+"'";
				comma=true;
			}
			if(SecPending!=CurOld.SecPending){
				if(comma) c+=",";
				c+="SecPending = '"     +POut.PBool  (SecPending)+"'";
				comma=true;
			}
			if(!comma)
				return 0;//this means no change is actually required.
			c+=" WHERE PatNum = '"   +POut.PInt   (PatNum)+"'";
			//cmd.CommandText=c;
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			return dcon.NonQ(c);
		}//end UpdatePatient

		///<summary>Only used when entering a new patient and user clicks cancel. To delete an existing patient, the PatStatus is simply changed to 4.</summary>
		public void Delete(){
			string command="DELETE FROM patient WHERE PatNum ="+PatNum.ToString();
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary>Returns a formatted name, Last, First.</summary>
		public string GetNameLF(){
			if(Preferred=="")
				return LName+", "+FName+" "+MiddleI;
			else
				return LName+", '"+Preferred+"' "+FName+" "+MiddleI;
		}

		///<summary></summary>
		public string GetNameFL(){
			if(Preferred=="")
				return FName+" "+MiddleI+" "+LName;
			else
				return FName+" '"+Preferred+"' "+MiddleI+" "+LName;
		}

		///<summary></summary>
		public string GetCreditIns(){
			string retStr="";
			if(CreditType=="")
				retStr+=" ";
			else retStr+=CreditType;
			if(PriPlanNum==0)
				retStr+=" ";
			else retStr+="I";	
			return retStr;
		}

		///<summary></summary>
		public int GetProvNum(){
			if(PriProv!=0)
				return PriProv;
			if(Prefs.GetInt("PracticeDefaultProv")==0){
				MessageBox.Show(Lan.g("Patients","Please set a default provider in the practice setup window."));
				return Providers.List[0].ProvNum;
			}
			return Prefs.GetInt("PracticeDefaultProv");
		}


		
		
	}

	
}










