using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>One row for each patient.  Includes deleted patients.</summary>
	public class Patient{
		///<summary>Primary key.</summary>
		public int    PatNum;
		///<summary>Last name.</summary>
		public string LName;
		///<summary>First name.</summary>
		public string FName;
		///<summary>Middle initial or name.</summary>
		public string MiddleI;
		///<summary>Preferred name, aka nickname.</summary>
		public string Preferred;
		///<summary>Enum:PatientStatus</summary>
		public PatientStatus PatStatus;
		///<summary>Enum:PatientGender</summary>
		public PatientGender Gender;
		///<summary>Enum:PatientPosition Marital status would probably be a better name for this column.</summary>
		public PatientPosition Position;
		///<summary>Age is not stored in the database.  Age is always calculated as needed from birthdate.</summary>
		public DateTime Birthdate;
		///<summary>In the US, this is 9 digits, no dashes. For all other countries, any punctuation or format is allowed.</summary>
		public string SSN;
		///<summary>.</summary>
		public string Address;
		///<summary>.</summary>
		public string Address2;
		///<summary>.</summary>
		public string City;
		///<summary>2 Char in USA</summary>
		public string State;
		///<summary>Postal code.</summary>
		public string Zip;
		///<summary>Home phone. Includes any punctuation</summary>
		public string HmPhone;
		///<summary>.</summary>
		public string WkPhone;
		///<summary>.</summary>
		public string WirelessPhone;
		///<summary>FK to patient.PatNum.  Head of household.</summary>
		public int    Guarantor;
		///<summary>Derived from Birthdate.  Not in the database table.</summary>
		public int    Age;
		///<summary>Single char. Shows at upper left corner of appointments.  Suggested use is A,B,or C to designate creditworthiness, but it can actually be used for any purpose.</summary>
		public string CreditType;
		///<summary>.</summary>
		public string Email;
		///<summary>For example: Dear Mr. Smith.  Not used by the program in any way.</summary>
		public string Salutation;
		///<summary>Current patient balance.(not family). If user has checked BalancesDontSubtractIns in setup, then this will not take into account insurance.  Otherwise, the insurance estimate pending will have already been subtracted.</summary>
		public double EstBalance;
		///<summary>May be 0. Also see the PlannedIsDone field. Otherwise it is the foreign key to appointment.AptNum.  This is the appointment that will show in the Chart module and in the Planned appointment tracker.  It will never show in the Appointments module. In other words, it is the suggested next appoinment rather than an appointment that has already been scheduled.</summary>
		public int NextAptNum;
		///<summary>FK to provider.ProvNum.  The patient's primary provider.  Required, although the program is robust enough to handle a missing provNum, and will use the practice default instead.</summary>
		public int PriProv;
		///<summary>FK to provider.ProvNum.  Secondary provider (hygienist). Optional.</summary>
		public int SecProv;//
		///<summary>FK to definition.DefNum.  Fee schedule for this patient.  Usually not used.  If missing, the practice default fee schedule is used. If patient has insurance, then the fee schedule for the insplan is used.</summary>
		public int FeeSched;
		///<summary>FK to definition.DefNum.  Must have a value, or the patient will not show on some reports.</summary>
		public int BillingType;
		///<summary>Name of folder where images will be stored. Not editable for now.</summary>
		public string ImageFolder;
		///<summary>Address or phone note.  Unlimited length in order to handle data from other programs during a conversion.</summary>
		public string AddrNote;
		///<summary>Family financial urgent note.  Only stored with guarantor, and shared for family.</summary>
		public string FamFinUrgNote;
		///<summary>Individual patient note for Urgent medical.</summary>
		public string MedUrgNote;
		///<summary>Individual patient note for Appointment module note.</summary>
		public string ApptModNote;
		///<summary>Single char for Nonstudent, Parttime, or Fulltime.  Blank=Nonstudent</summary>
		public string StudentStatus;
		///<summary>College name.</summary>
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
		///<summary>No longer used.  See toothinital table instead.</summary>
		public string PrimaryTeethOld;
		///<summary>Total balance for entire family before insurance estimate.  Not the same as the sum of the 4 aging balances because this can be negative.  Only stored with guarantor.</summary>
		public double BalTotal;
		///<summary>FK to employer.EmployerNum.</summary>
		public int EmployerNum;
		///<summary>Not used since version 2.8.</summary>
		public string EmploymentNote;
		///<summary>Enum:PatientRace Race and ethnicity.</summary>
		public PatientRace Race;
		///<summary>FK to county.CountyName, although it will not crash if key absent.</summary>
		public string County;
		///<summary>FK to school.SchoolName, although it will not crash if key absent.  Name of gradeschool or highschool.</summary>
		public string GradeSchool;
		///<summary>Enum:PatientGrade Gradelevel.</summary>
		public PatientGrade GradeLevel;
		///<summary>Enum:TreatmentUrgency Used in public health screenings.</summary>
		public TreatmentUrgency Urgency;
		///<summary>The date that the patient first visited the office.  Automated.</summary>
		public DateTime DateFirstVisit;
		///<summary>FK to clinic.ClinicNum. Can be zero if not attached to a clinic or no clinics set up.</summary>
		public int ClinicNum;
		///<summary>For now, an 'I' indicates that the patient has insurance.  This is only used when displaying appointments.  It will later be expanded.  User can't edit.</summary>
		public string HasIns;
		///<summary>The Trophy bridge is inadequate.  This is an attempt to make it usable for offices that have already invested in Trophy hardware.</summary>
		public string TrophyFolder;
		///<summary>This simply indicates whether the 'done' box is checked in the chart module.  Used to be handled as a -1 in the NextAptNum field, but now that field is unsigned.</summary>
		public bool PlannedIsDone;
		///<summary>Set to true if patient needs to be premedicated for appointments, includes PAC, halcion, etc.</summary>
		public bool Premed;
		///<summary>Only used in hospitals.</summary>
		public string Ward;
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
			//p.PrimaryTeeth=PrimaryTeeth;
			p.BalTotal=BalTotal;
			p.EmployerNum=EmployerNum;
			p.EmploymentNote=EmploymentNote;
			p.Race=Race;
			p.County=County;
			p.GradeSchool=GradeSchool;
			p.GradeLevel=GradeLevel;
			p.Urgency=Urgency;
			p.DateFirstVisit=DateFirstVisit;
			p.ClinicNum=ClinicNum;
			p.HasIns=HasIns;
			p.TrophyFolder=TrophyFolder;
			p.PlannedIsDone=PlannedIsDone;
			p.Premed=Premed;
			p.Ward=Ward;
			return p;
		}
	
		///<summary>ONLY for new patients. Set includePatNum to true for use the patnum from the import function.  Otherwise, uses InsertID to fill PatNum.</summary>
		public void Insert(bool includePatNum){
			if(!includePatNum && Prefs.RandomKeys){
				PatNum=MiscData.GetKey("patient","PatNum");
			}
			string command= "INSERT INTO patient (";
			if(includePatNum || Prefs.RandomKeys){
				command+="PatNum,";
			}
			command+="lname,fname,middlei,preferred,patstatus,gender,"
				+"position,birthdate,ssn,address,address2,city,state,zip,hmphone,wkphone,wirelessphone,"
				+"guarantor,credittype,email,salutation,"
				+"estbalance,nextaptnum,priprov,secprov,feesched,billingtype,"
				+"imagefolder,addrnote,famfinurgnote,medurgnote,apptmodnote,"
				+"studentstatus,schoolname,chartnumber,medicaidid"
				+",Bal_0_30,Bal_31_60,Bal_61_90,BalOver90,insest,BalTotal"
				+",EmployerNum,EmploymentNote,Race,County,GradeSchool,GradeLevel,Urgency,DateFirstVisit"
				+",ClinicNum,HasIns,TrophyFolder,PlannedIsDone,Premed,Ward) VALUES(";
			if(includePatNum || Prefs.RandomKeys){
				command+="'"+POut.PInt(PatNum)+"', ";
			}
			command+="'"+POut.PString(LName)+"', "
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
				+"'"+POut.PDouble(EstBalance)+"', "
				+"'"+POut.PInt   (NextAptNum)+"', "
				+"'"+POut.PInt   (PriProv)+"', "
				+"'"+POut.PInt   (SecProv)+"', "
				+"'"+POut.PInt   (FeeSched)+"', "
				+"'"+POut.PInt   (BillingType)+"', "
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
				//+"'"+POut.PString(PrimaryTeeth)+"', "
				+"'"+POut.PDouble(BalTotal)+"', "
				+"'"+POut.PInt   (EmployerNum)+"', "
				+"'"+POut.PString(EmploymentNote)+"', "
				+"'"+POut.PInt   ((int)Race)+"', "
				+"'"+POut.PString(County)+"', "
				+"'"+POut.PString(GradeSchool)+"', "
				+"'"+POut.PInt   ((int)GradeLevel)+"', "
				+"'"+POut.PInt   ((int)Urgency)+"', "
				+"'"+POut.PDate  (DateFirstVisit)+"', "
				+"'"+POut.PInt   (ClinicNum)+"', "
				+"'"+POut.PString(HasIns)+"', "
				+"'"+POut.PString(TrophyFolder)+"', "
				+"'"+POut.PBool  (PlannedIsDone)+"', "
				+"'"+POut.PBool  (Premed)+"', "
				+"'"+POut.PString(Ward)+"')";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				PatNum=dcon.InsertID;
			}
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
			//if(PrimaryTeeth!=CurOld.PrimaryTeeth){
			//	if(comma) c+=",";
			//	c+="PrimaryTeeth = '"   +POut.PString(PrimaryTeeth)+"'";
			//	comma=true;
			//}
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
			if(ClinicNum!=CurOld.ClinicNum){
				if(comma) c+=",";
				c+="ClinicNum = '"     +POut.PInt   (ClinicNum)+"'";
				comma=true;
			}
			if(HasIns!=CurOld.HasIns){
				if(comma) c+=",";
				c+="HasIns = '"     +POut.PString (HasIns)+"'";
				comma=true;
			}
			if(TrophyFolder!=CurOld.TrophyFolder) {
				if(comma) c+=",";
				c+="TrophyFolder = '"     +POut.PString(TrophyFolder)+"'";
				comma=true;
			}
			if(PlannedIsDone!=CurOld.PlannedIsDone) {
				if(comma) c+=",";
				c+="PlannedIsDone = '"     +POut.PBool(PlannedIsDone)+"'";
				comma=true;
			}
			if(Premed!=CurOld.Premed) {
				if(comma) c+=",";
				c+="Premed = '"     +POut.PBool(Premed)+"'";
				comma=true;
			}
			if(Ward!=CurOld.Ward) {
				if(comma) c+=",";
				c+="Ward = '"     +POut.PString(Ward)+"'";
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
			retStr+=HasIns;
			return retStr;
		}

		///<summary>Gets the provider for this patient.  If provNum==0, then it gets the practice default prov.</summary>
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










