using System;

namespace OpenDental{
	public enum YN{Unknown, Yes, No}
	public enum Relat{Self,Spouse,Child,Employee,HandicapDep,SignifOther,InjuredPlaintiff,
		LifePartner,Dependent}//for insurance only
	public enum Month{Jan=1,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec}
	public enum AcctType{Proc=1,Adj,Pay,Claim,Disc,Comm}
	public enum ProgType{Proc=1,Rx}
	public enum PriSecTot{Pri,Sec,Tot}
	public enum ProcStat{TP=1,C,EC,EO,R}//(TreatPlan,Complete,ExistingCurrentProv,ExistingOtherProv,ReferredOut)(?new?)
	public enum DefCat{AccountColors,AdjTypes,ApptConfirmed,ApptProcsQuickAdd,BillingTypes,
		ClaimFormatss,DunningMessages,FeeSchedNames,MedicalNotes,Operatories,
		PaymentTypes,ProcCodeCats,ProgNoteColors,RecallUnschedStatus,ServiceNotes,
		DiscountTypes,Diagnosis,AppointmentColors,DocumentCats,ApptPhoneNotes,
		TxPriorities,MiscColors,ChartGraphicColors,ContactCategories}
	//public enum StudentStat{None,Full,Part};
	public enum FormProcMode{Edit,View,Select}
	public enum TreatmentArea{Surf=1,Tooth,Mouth,Quad,Sextant,Arch,ToothRange}
	public enum InvalidType{Date,LocalData}
	public enum ButtonType{ButPush,Text}
	public enum DentalSpecialty{General,Hygienist,Endodontics,Pediatric,
		Perio,Prosth,Ortho,Denturist,Surgery,Assistant,
		LabTech,Pathology,PublicHealth,Radiology}
	public enum ApptStatus{None,Scheduled,Complete,UnschedList,ASAP,Broken}
	public enum PatientStatus{Patient,NonPatient,Inactive,Archived,Deleted,Deceased}
	public enum PatientGender{Male,Female,Unknown}
	public enum PatientPosition{Single,Married,Child,Widowed}
	public enum ScheduleType{Practice,Provider,Blockout}
	public enum LabCase{None,Sent,Received};
	public enum PlaceOfService{Office,PatientsHome,InpatHospital,OutpatHospital,SkilledNursFac,
		AdultLivCareFac,OtherLocation}
	//public enum SelectRowsMode{None,OneToggle,MultiToggle,One,Multi}//variation on SelectionMode enum.
		//the toggle versions allow user to unselect a single selected row.
	public enum OtherResult{Cancel,CreateNew,GoTo,CopyToPinBoard,NewToPinBoard}
	//public enum SearchPatType{Lname,Fname,HmPhone,Address}
	public enum PaintType{Extraction,FillingSolid,FillingOutline,RCT,Post,CrownSolid
		,CrownOutline,CrownHatch,Implant,Sealant,PonticSolid,PonticOutline,PonticHatch
		,RetainerSolid,RetainerOutline,RetainerHatch}
  public enum SchedStatus{Open,Closed,Holiday}//used for schedule struct
  public enum BackupType{CopyFiles,CopyToServer,DataDump}
  public enum AutoCondition{Anterior,Posterior,Premolar,Molar,One_Surf,Two_Surf,Three_Surf,Four_Surf,Five_Surf,First,EachAdditional,Maxillary,Mandibular,Primary,Permanent,Pontic,Retainer}
	//public enum AgingType{A0_30,A31_60,A61_90,A90plus}
	public enum ClaimProcStatus{NotReceived,Received,Preauth,Adjustment,Supplemental}
		//supplemental means a second payment on an existing procedure.  Always received of course.
		
	public class ClassEnumerations{
		public ClassEnumerations(){

		}
	}
}
