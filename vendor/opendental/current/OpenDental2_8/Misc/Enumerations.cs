using System;

namespace OpenDental{
	///<summary>Yes,No or Unknown.</summary>
	public enum YN{
		///<summary>0</summary>
		Unknown,
		///<summary>1</summary>
		Yes,
		///<summary>2</summary>
		No}
	///<summary>Relationship to subscriber for insurance.</summary>
	public enum Relat{
		///<summary>0</summary>
		Self,
		///<summary>1</summary>
		Spouse,
		///<summary>2</summary>
		Child,
		///<summary>3</summary>
		Employee,
		///<summary>4</summary>
		HandicapDep,
		///<summary>5</summary>
		SignifOther,
		///<summary>6</summary>
		InjuredPlaintiff,
		///<summary>7</summary>
		LifePartner,
		///<summary>8</summary>
		Dependent}
	///<summary></summary>
	public enum Month{
		///<summary>1</summary>
		Jan=1,
		///<summary>2</summary>
		Feb,
		///<summary>3</summary>
		Mar,
		///<summary>4</summary>
		Apr,
		///<summary>5</summary>
		May,
		///<summary>6</summary>
		Jun,
		///<summary>7</summary>
		Jul,
		///<summary>8</summary>
		Aug,
		///<summary>9</summary>
		Sep,
		///<summary>10</summary>
		Oct,
		///<summary>11</summary>
		Nov,
		///<summary>12</summary>
		Dec}
	///<summary>Account line type used when displaying lines in the Account module.</summary>
	public enum AcctType{
		///<summary>1</summary>
		Proc=1,
		///<summary>2</summary>
		Adj,
		///<summary>3</summary>
		Pay,
		///<summary>4</summary>
		Claim,
		///<summary>5</summary>
		Disc,
		///<summary>6</summary>
		Comm,
		///<summary>7</summary>
		PayPlan}
	///<summary>Progress notes line type. Used when displaying lines in the Chart module.</summary>
	public enum ProgType{
		///<summary>1</summary>
		Proc=1,
		///<summary>2</summary>
		Rx}
	///<summary>Primary, secondary, or total. Used in some insurance estimates to specify which kind of estimate is needed.</summary>
	public enum PriSecTot{
		///<summary>0</summary>
		Pri,
		///<summary>1</summary>
		Sec,
		///<summary>2</summary>
		Tot}
	///<summary>Procedure Status.</summary>
	public enum ProcStat{
		///<summary>1- Treatment Plan.</summary>
		TP=1,
		///<summary>2- Complete.</summary>
		C,
		///<summary>3- Existing Current Provider.</summary>
		EC,
		///<summary>4- Existing Other Provider.</summary>
		EO,
		///<summary>5- Referred Out.</summary>
		R}
		//?new?
	///<summary>Definition Category. Go to the definition setup window in the program to see how each of these categories is used.</summary>
	public enum DefCat{
		///<summary>0- Colors to display in Account module.</summary>
		AccountColors,
		///<summary>1- Adjustment types.</summary>
		AdjTypes,
		///<summary>2- Appointment confirmed types.</summary>
		ApptConfirmed,
		///<summary>3- Procedure quick add list for appointments.</summary>
		ApptProcsQuickAdd,
		///<summary>4- Billing types.</summary>
		BillingTypes,
		///<summary>5- Not used.</summary>
		ClaimFormats,
		///<summary>6- Not used.</summary>
		DunningMessages,
		///<summary>7- Fee schedule names.</summary>
		FeeSchedNames,
		///<summary>8- Medical notes for quick paste.</summary>
		MedicalNotes,
		///<summary>9- Operatory names.</summary>
		Operatories,
		///<summary>10- Payment types.</summary>
		PaymentTypes,
		///<summary>11- Procedure code categories.</summary>
		ProcCodeCats,
		///<summary>12- Progress note colors.</summary>
		ProgNoteColors,
		///<summary>13- Statuses for recall, unscheduled, and next appointments.</summary>
		RecallUnschedStatus,
		///<summary>14- Service notes for quick paste.</summary>
		ServiceNotes,
		///<summary>15- Discount types.</summary>
		DiscountTypes,
		///<summary>16- Diagnosis types.</summary>
		Diagnosis,
		///<summary>17- Colors to display in the Appointments module.</summary>
		AppointmentColors,
		///<summary>18- Document categories.</summary>
		DocumentCats,
		///<summary>19- Quick add notes for the ApptPhoneNotes, which is getting phased out.</summary>
		ApptPhoneNotes,
		///<summary>20- Treatment plan priority names.</summary>
		TxPriorities,
		///<summary>21- Miscellaneous color options.</summary>
		MiscColors,
		///<summary>22- Colors for the graphical tooth chart.</summary>
		ChartGraphicColors,
		///<summary>23- Categories for the Contact list.</summary>
		ContactCategories}
	//public enum StudentStat{None,Full,Part};
	///<summary>Used in procedurecode setup to specify the treatment area for a procedure.  This determines what fields are available when editing an appointment.</summary>
	public enum TreatmentArea{
		///<summary>1</summary>
		Surf=1,
		///<summary>2</summary>
		Tooth,
		///<summary>3</summary>
		Mouth,
		///<summary>4</summary>
		Quad,
		///<summary>5</summary>
		Sextant,
		///<summary>6</summary>
		Arch,
		///<summary>7</summary>
		ToothRange}
	///<summary>When the autorefresh message is sent to the other computers, this is the type.</summary>
	public enum InvalidType{
		///<summary>0</summary>
		Date,
		///<summary>1</summary>
		LocalData}
	//<summary></summary>
	/*public enum ButtonType{
		///<summary></summary>
		ButPush,
		///<summary></summary>
		Text}*/
	///<summary></summary>
	public enum DentalSpecialty{
		///<summary>0</summary>
		General,
		///<summary>1</summary>
		Hygienist,
		///<summary>2</summary>
		Endodontics,
		///<summary>3</summary>
		Pediatric,
		///<summary>4</summary>
		Perio,
		///<summary>5</summary>
		Prosth,
		///<summary>6</summary>
		Ortho,
		///<summary>7</summary>
		Denturist,
		///<summary>8</summary>
		Surgery,
		///<summary>9</summary>
		Assistant,
		///<summary>10</summary>
		LabTech,
		///<summary>11</summary>
		Pathology,
		///<summary>12</summary>
		PublicHealth,
		///<summary>13</summary>
		Radiology}
	///<summary>Appointment status.</summary>
	public enum ApptStatus{
		///<summary>0- No appointment should ever have this status.</summary>
		None,
		///<summary>1- Shows as a regularly scheduled appointment.</summary>
		Scheduled,
		///<summary>2- Shows greyed out.</summary>
		Complete,
		///<summary>3- Only shows on unscheduled list.</summary>
		UnschedList,
		///<summary>4- Functions the same as Scheduled for now.</summary>
		ASAP,
		///<summary>5- Shows with a big X on it.</summary>
		Broken,
		///<summary>6- Next appointment.  Only shows in Chart module. User not allowed to change this status, and it does not display as one of the options.</summary>
		Next}
	///<summary></summary>
	public enum PatientStatus{
		///<summary>0</summary>
		Patient,
		///<summary>1</summary>
		NonPatient,
		///<summary>2</summary>
		Inactive,
		///<summary>3</summary>
		Archived,
		///<summary>4</summary>
		Deleted,
		///<summary>5</summary>
		Deceased}
	///<summary></summary>
	public enum PatientGender{
		///<summary>0</summary>
		Male,
		///<summary>1</summary>
		Female,
		///<summary>2- This is not a joke. Required by HIPAA for privacy.</summary>
		Unknown}
	///<summary></summary>
	public enum PatientPosition{
		///<summary>0</summary>
		Single,
		///<summary>1</summary>
		Married,
		///<summary>2</summary>
		Child,
		///<summary>3</summary>
		Widowed}
	///<summary>For schedule timeblocks.</summary>
	public enum ScheduleType{
		///<summary>0</summary>
		Practice,
		///<summary>1- Not used yet.</summary>
		Provider,
		///<summary>2- Not used yet.</summary>
		Blockout}
	///<summary>Used in the appointment edit window.</summary>
	public enum LabCase{
		///<summary>0</summary>
		None,
		///<summary>1</summary>
		Sent,
		///<summary>2</summary>
		Received};
	///<summary></summary>
	public enum PlaceOfService{
		///<summary>0</summary>
		Office,
		///<summary>1</summary>
		PatientsHome,
		///<summary>2</summary>
		InpatHospital,
		///<summary>3</summary>
		OutpatHospital,
		///<summary>4</summary>
		SkilledNursFac,
		///<summary>5</summary>
		AdultLivCareFac,
		///<summary>6</summary>
		OtherLocation}
	///<summary>Used in the other appointments window to keep track of the result when closing.</summary>
	public enum OtherResult{
		///<summary></summary>
		Cancel,
		///<summary></summary>
		CreateNew,
		///<summary></summary>
		GoTo,
		///<summary></summary>
		CopyToPinBoard,
		///<summary></summary>
		NewToPinBoard}
	//public enum SearchPatType{Lname,Fname,HmPhone,Address}
	///<summary></summary>
	public enum PaintType{
		///<summary>0</summary>
		Extraction,
		///<summary>1</summary>
		FillingSolid,
		///<summary>2</summary>
		FillingOutline,
		///<summary>3</summary>
		RCT,
		///<summary>4</summary>
		Post,
		///<summary>5</summary>
		CrownSolid,
		///<summary>6</summary>
		CrownOutline,
		///<summary>7</summary>
		CrownHatch,
		///<summary>8</summary>
		Implant,
		///<summary>9</summary>
		Sealant,
		///<summary>10</summary>
		PonticSolid,
		///<summary>11</summary>
		PonticOutline,
		///<summary>12</summary>
		PonticHatch,
		///<summary>13</summary>
		RetainerSolid,
		///<summary>14</summary>
		RetainerOutline,
		///<summary>15</summary>
		RetainerHatch}
	///<summary>Schedule status.</summary>
  public enum SchedStatus{
		///<summary></summary>
		Open,
		///<summary></summary>
		Closed,
		///<summary></summary>
		Holiday}
	//<summary></summary>
  /*public enum BackupType{
		///<summary></summary>
		CopyFiles,
		///<summary></summary>
		CopyToServer,
		///<summary></summary>
		DataDump}*/
	///<summary></summary>
  public enum AutoCondition{
		///<summary>0</summary>
		Anterior,
		///<summary>1</summary>
		Posterior,
		///<summary>2</summary>
		Premolar,
		///<summary>3</summary>
		Molar,
		///<summary>4</summary>
		One_Surf,
		///<summary>5</summary>
		Two_Surf,
		///<summary>6</summary>
		Three_Surf,
		///<summary>7</summary>
		Four_Surf,
		///<summary>8</summary>
		Five_Surf,
		///<summary>9</summary>
		First,
		///<summary>10</summary>
		EachAdditional,
		///<summary>11</summary>
		Maxillary,
		///<summary>12</summary>
		Mandibular,
		///<summary>13</summary>
		Primary,
		///<summary>14</summary>
		Permanent,
		///<summary>15</summary>
		Pontic,
		///<summary>16</summary>
		Retainer}
	///<summary>Claimproc Status.  The status must generally be the same as the claim, although it is sometimes not strictly enforced.</summary>
	public enum ClaimProcStatus{
		///<summary>0: For claims that have been created or sent, but have not been received.</summary>
		NotReceived,
		///<summary>1: For claims that have been received.</summary>
		Received,
		///<summary>2: For preauthorizations.</summary>
		Preauth,
		///<summary>3: The only place that this status is used is to make adjustments to benefits from the coverage window.  It is never attached to a claim.</summary>
		Adjustment,
		///<summary>4:This differs from received only slightly.  It's for additional payments on claims already received.  It might be superfluous.</summary>
		Supplemental,
		///<summary>5: Capitation claimprocs do not affect the patient balance and can not be attached to a check.</summary>
		Capitation
	}
	///<summary></summary>
	public enum CommItemType{
		///<summary>1</summary>
		StatementSent=1,
		///<summary>2- This will be replacing some of the existing note sections.</summary>
		AppointmentScheduling}
		
	





}





