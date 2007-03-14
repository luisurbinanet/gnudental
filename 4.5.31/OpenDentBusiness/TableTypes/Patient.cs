using System;
using System.Collections;

namespace OpenDentBusiness{
	
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
		///<summary>Enum:ContactMethod The same enum will probably also be used later for PreferContactMethod and PreferRecallMethod.</summary>
		public ContactMethod PreferConfirmMethod;
		///<summary>Only the time portion of the DateTime is used.</summary>
		public DateTime SchedBeforeTime;
		///<summary></summary>
		public DateTime SchedAfterTime;
		///<summary>We do not use this, but some users do, so here it is. 0=none. Otherwise, 1-7 for day.</summary>
		public int SchedDayOfWeek;
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
			p.PreferConfirmMethod=PreferConfirmMethod;
			p.SchedBeforeTime=SchedBeforeTime;
			p.SchedAfterTime=SchedAfterTime;
			p.SchedDayOfWeek=SchedDayOfWeek;
			return p;
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

		


		
		
	}

	
}










