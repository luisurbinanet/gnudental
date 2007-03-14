using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the appointment table in the database.</summary>
	public class Appointment{
		///<summary>Primary key.</summary>
		public int AptNum;
		///<summary>Foreign key to <see cref="Patient.PatNum">patient.PatNum</see>.</summary>
		public int PatNum;
		///<summary>See the <see cref="ApptStatus"/> enumeration.</summary>
		public ApptStatus AptStatus;
		///<summary>Time pattern, X for Dr time, / for assist time. Was previously in 10 minute increments, but is now in 5 minute increments.</summary>
		public string Pattern;
		///<summary>Foreign key to <see cref="Def.DefNum">definition.DefNum</see>.</summary>
		///<remarks>The <see cref="Def.Category">definition.Category</see> in the definition table is <see cref="DefCat.ApptConfirmed">DefCat.ApptConfirmed</see>.</remarks>
		public int Confirmed;
		///<summary>Amount of time to add to appointment.  Example: 2 would represent add 20 minutes.</summary>
		public int AddTime;
		///<summary>Operatory.  Foreign key to operatory.OperatoryNum.</summary>
		public int Op;
		///<summary>Note.</summary>
		public string Note;
		///<summary>Foreign key to <see cref="Provider.ProvNum">provider.ProvNum</see>.</summary>
		public int ProvNum;
		///<summary>Hygiene provider.  Foreign key to <see cref="Provider.ProvNum">provider.ProvNum</see>.</summary>
		public int ProvHyg;
		///<summary>Appointment Date and time.</summary>
		public DateTime AptDateTime;
		///<summary>A better description would be PlannedAptNum.  Only used to show that this apt is derived from specified planned apt. Otherwise, 0. Foreign key to appointment.AptNum.</summary>
		public int NextAptNum;
		///<summary>Foreign key to <see cref="Def.DefNum">definition.DefNum</see>.</summary>
		///<remarks>The <see cref="Def.Category">definition.Category</see> in the definition table is <see cref="DefCat.RecallUnschedStatus">DefCat.RecallUnschedStatus</see>.  Only used if this is an Unsched or Next appt.</remarks>
		public int UnschedStatus;
		///<summary>A lab case is expected for this appointment.</summary>
		public LabCase Lab;
		///<summary>This is the first appoinment this patient has had at this office.</summary>
		public bool IsNewPatient;
		///<summary>A one line summary of all procedures.  Can be used in various reports, Unscheduled list, and Next appointment tracker.  Not user editable right now.</summary>
		public string ProcDescript;
		///<summary>Foreign key to employee.EmployeeNum</summary>
		public int Assistant;
		///<summary>Dental School field. Foreign key to instructor.InstructorNum</summary>
		public int InstructorNum;
		///<summary>Dental School field. Foreign key to schoolclass.SchoolClassNum.</summary>
		public int SchoolClassNum;
		///<summary>Dental School field. Foreign key to schoolcourse.SchoolCourseNum.</summary>
		public int SchoolCourseNum;
		///<summary>Dental School field. eg 3.5. Only a grade for this single appointment. The course grade shows on a report.</summary>
		public float GradePoint;
		///<summary>Foreign key to clinic.ClinicNum.  0 if no clinic.</summary>
		public int ClinicNum;
		///<summary>Set true if this is a hygiene appt.  The hygiene provider's color will show.</summary>
		public bool IsHygiene;

		///<summary>Returns a copy of the appointment.</summary>
    public Appointment Copy(){
			Appointment a=new Appointment();
			a.AptNum=AptNum;
			a.PatNum=PatNum;
			a.AptStatus=AptStatus;
			a.Pattern=Pattern;
			a.Confirmed=Confirmed;
			a.AddTime=AddTime;
			a.Op=Op;
			a.Note=Note;
			a.ProvNum=ProvNum;
			a.ProvHyg=ProvHyg;
			a.AptDateTime=AptDateTime;
			a.NextAptNum=NextAptNum;
			a.UnschedStatus=UnschedStatus;
			a.Lab=Lab;
			a.IsNewPatient=IsNewPatient;
			a.ProcDescript=ProcDescript;
			a.Assistant=Assistant;
			a.InstructorNum=InstructorNum;
			a.SchoolClassNum=SchoolClassNum;
			a.SchoolCourseNum=SchoolCourseNum;
			a.GradePoint=GradePoint;
			a.ClinicNum=ClinicNum;
			a.IsHygiene=IsHygiene;
			return a;
		}

		///<summary>If IsNew, just supply null for oldApt.</summary>
		public void InsertOrUpdate(Appointment oldApt,bool IsNew){
			//if(){
				//throw new Exception(Lan.g(this,""));
			//}
			if(IsNew){
				Insert();
			}
			else{
				if(oldApt==null){
					throw new Exception("oldApt cannot be null if updating.");
				}
				Update(oldApt);
			}
		}

		///<summary></summary>
		private void Insert(){
			if(Prefs.RandomKeys){
				AptNum=MiscData.GetKey("appointment","AptNum");
			}
			string command="INSERT INTO appointment (";
			if(Prefs.RandomKeys){
				command+="AptNum,";
			}
			command+="patnum,aptstatus, "
				+"pattern,confirmed,addtime,op,note,provnum,"
				+"provhyg,aptdatetime,nextaptnum,unschedstatus,lab,isnewpatient,procdescript,"
				+"Assistant,InstructorNum,SchoolClassNum,SchoolCourseNum,GradePoint,ClinicNum,IsHygiene) VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(AptNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (PatNum)+"', "
				+"'"+POut.PInt   ((int)AptStatus)+"', "
				+"'"+POut.PString(Pattern)+"', "
				+"'"+POut.PInt   (Confirmed)+"', "
				+"'"+POut.PInt   (AddTime)+"', "
				+"'"+POut.PInt   (Op)+"', "
				+"'"+POut.PString(Note)+"', "
				+"'"+POut.PInt   (ProvNum)+"', "
				+"'"+POut.PInt   (ProvHyg)+"', "
				+"'"+POut.PDateT (AptDateTime)+"', "
				+"'"+POut.PInt   (NextAptNum)+"', "
				+"'"+POut.PInt   (UnschedStatus)+"', "
				+"'"+POut.PInt   ((int)Lab)+"', "
				+"'"+POut.PBool  (IsNewPatient)+"', "
				+"'"+POut.PString(ProcDescript)+"', "
				+"'"+POut.PInt   (Assistant)+"', "
				+"'"+POut.PInt   (InstructorNum)+"', "
				+"'"+POut.PInt   (SchoolClassNum)+"', "
				+"'"+POut.PInt   (SchoolCourseNum)+"', "
				+"'"+POut.PFloat (GradePoint)+"', "
				+"'"+POut.PInt   (ClinicNum)+"', "
				+"'"+POut.PBool  (IsHygiene)+"')";
			DataConnection dcon=new DataConnection();
			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
				dcon.NonQ(command,true);
				AptNum=dcon.InsertID;
			}
		}

		///<summary>Updates only the changed columns and returns the number of rows affected.  Supply an oldApt for comparison.</summary>
		private int Update(Appointment oldApt){
			bool comma=false;
			string c = "UPDATE appointment SET ";
			if(PatNum!=oldApt.PatNum){
				c+="PatNum = '"      +POut.PInt   (PatNum)+"'";
				comma=true;
			}
			if(AptStatus!=oldApt.AptStatus){
				if(comma) c+=",";
				c+="AptStatus = '"   +POut.PInt   ((int)AptStatus)+"'";
				comma=true;
			}
			if(Pattern!=oldApt.Pattern){
				if(comma) c+=",";
				c+="Pattern = '"     +POut.PString(Pattern)+"'";
				comma=true;
			}
			if(Confirmed!=oldApt.Confirmed){
				if(comma) c+=",";
				c+="Confirmed = '"   +POut.PInt   (Confirmed)+"'";
				comma=true;
			}
			if(AddTime!=oldApt.AddTime){
				if(comma) c+=",";
				c+="AddTime = '"     +POut.PInt   (AddTime)+"'";
				comma=true;
			}
			if(Op!=oldApt.Op){
				if(comma) c+=",";
				c+="Op = '"          +POut.PInt   (Op)+"'";
				comma=true;
			}
			if(Note!=oldApt.Note){
				if(comma) c+=",";
				c+="Note = '"        +POut.PString(Note)+"'";
				comma=true;
			}
			if(ProvNum!=oldApt.ProvNum){
				if(comma) c+=",";
				c+="ProvNum = '"     +POut.PInt   (ProvNum)+"'";
				comma=true;
			}
			if(ProvHyg!=oldApt.ProvHyg){
				if(comma) c+=",";
				c+="ProvHyg = '"     +POut.PInt   (ProvHyg)+"'";
				comma=true;
			}
			if(AptDateTime!=oldApt.AptDateTime){
				if(comma) c+=",";
				c+="AptDateTime = '" +POut.PDateT (AptDateTime)+"'";
				comma=true;
			}
			if(NextAptNum!=oldApt.NextAptNum){
				if(comma) c+=",";
				c+="NextAptNum = '"  +POut.PInt   (NextAptNum)+"'";
				comma=true;
			}
			if(UnschedStatus!=oldApt.UnschedStatus){
				if(comma) c+=",";
				c+="UnschedStatus = '" +POut.PInt(UnschedStatus)+"'";
				comma=true;
			}
			if(Lab!=oldApt.Lab){
				if(comma) c+=",";
				c+="Lab = '"         +POut.PInt   ((int)Lab)+"'";
				comma=true;
			}
			if(IsNewPatient!=oldApt.IsNewPatient){
				if(comma) c+=",";
				c+="IsNewPatient = '"+POut.PBool  (IsNewPatient)+"'";
				comma=true;
			}
			if(ProcDescript!=oldApt.ProcDescript){
				if(comma) c+=",";
				c+="ProcDescript = '"+POut.PString(ProcDescript)+"'";
				comma=true;
			}
			if(Assistant!=oldApt.Assistant){
				if(comma) c+=",";
				c+="Assistant = '"   +POut.PInt   (Assistant)+"'";
				comma=true;
			}
			if(InstructorNum!=oldApt.InstructorNum){
				if(comma) c+=",";
				c+="InstructorNum = '"   +POut.PInt   (InstructorNum)+"'";
				comma=true;
			}
			if(SchoolClassNum!=oldApt.SchoolClassNum){
				if(comma) c+=",";
				c+="SchoolClassNum = '"   +POut.PInt   (SchoolClassNum)+"'";
				comma=true;
			}
			if(SchoolCourseNum!=oldApt.SchoolCourseNum){
				if(comma) c+=",";
				c+="SchoolCourseNum = '"   +POut.PInt   (SchoolCourseNum)+"'";
				comma=true;
			}
			if(GradePoint!=oldApt.GradePoint){
				if(comma) c+=",";
				c+="GradePoint = '"   +POut.PFloat  (GradePoint)+"'";
				comma=true;
			}
			if(ClinicNum!=oldApt.ClinicNum){
				if(comma) c+=",";
				c+="ClinicNum = '"   +POut.PInt  (ClinicNum)+"'";
				comma=true;
			}
			if(IsHygiene!=oldApt.IsHygiene){
				if(comma) c+=",";
				c+="IsHygiene = '"   +POut.PBool (IsHygiene)+"'";
				comma=true;
			}
			if(!comma)
				return 0;//this means no change is actually required.
			c+=" WHERE AptNum = '"+POut.PInt(AptNum)+"'";
			DataConnection dcon=new DataConnection();
 			int rowsChanged=dcon.NonQ(c);
			//MessageBox.Show(c);
			return rowsChanged;
		}

		///<summary></summary>
		public void Delete(){
			Patient pat=Patients.GetPat(PatNum);
			Procedures.SetDateFirstVisit(DateTime.MinValue,3,pat);
			string command="DELETE from appointment WHERE "
				+"AptNum = '"+POut.PInt(AptNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

	}

	/*=========================================================================================
	=================================== class Appointments ==========================================*/

	///<summary>Handles database commands related to the appointment table in the db.</summary>
	public class Appointments{
		///<summary>The appointment on the pinboard.</summary>
		public static Appointment PinBoard;
		///<summary>The date currently selected in the appointment module.</summary>
		public static DateTime DateSelected;

		///<summary>Gets a list of appointments for one day in the schedule, whether hidden or not.</summary>
		public static Appointment[] Refresh(DateTime thisDay){
			DateSelected = thisDay;
			string command=
				"SELECT * from appointment "
				+"WHERE AptDateTime LIKE '"+POut.PDate(thisDay)+"%' "
				+"AND aptstatus != '"+(int)ApptStatus.UnschedList+"' "
				+"AND aptstatus != '"+(int)ApptStatus.Planned+"'";
			return FillList(command);
		}

		///<summary>Gets ListUn for both the unscheduled list and for planned appt tracker.  This is in transition, since the unscheduled list will probably eventually be phased out.  Set true if getting Planned appointments, false if getting Unscheduled appointments.</summary>
		public static Appointment[] RefreshUnsched(bool doGetPlanned){
			string command="";
			if(doGetPlanned){
				command="SELECT Tplanned.*,Tregular.aptnum FROM appointment AS Tplanned "
					+"LEFT JOIN appointment AS Tregular ON Tplanned.aptnum = Tregular.nextaptnum "
					+"WHERE Tplanned.aptstatus = '"+(int)ApptStatus.Planned+"' "
					+"AND Tregular.aptnum IS NULL "
					+"ORDER BY Tplanned.UnschedStatus,Tplanned.AptDateTime";
			}
			else{//unsched
				command="SELECT * FROM appointment "
					+"WHERE aptstatus = '"+(int)ApptStatus.UnschedList+"' "
					+"ORDER BY AptDateTime";
			}
			return FillList(command);
		}

		///<summary>Returns all appointments for the given patient, ordered from earliest to latest.  Used in statements, appt cards, OtherAppts window, etc.</summary>
		public static Appointment[] GetForPat(int patNum){
			string command=
				"SELECT * FROM appointment "
				+"WHERE patnum = '"+patNum.ToString()+"' "
				+"ORDER BY AptDateTime";
			return FillList(command);
		}

		///<summary>Fills the specified array of Appointments using the supplied SQL command.</summary>
		private static Appointment[] FillList(string command){
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			Appointment[] list=new Appointment[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				list[i]=new Appointment();
				list[i].AptNum         =PIn.PInt   (table.Rows[i][0].ToString());
				list[i].PatNum         =PIn.PInt   (table.Rows[i][1].ToString());
				list[i].AptStatus      =(ApptStatus)PIn.PInt(table.Rows[i][2].ToString());
				list[i].Pattern        =PIn.PString(table.Rows[i][3].ToString());
				list[i].Confirmed      =PIn.PInt   (table.Rows[i][4].ToString());
				list[i].AddTime        =PIn.PInt   (table.Rows[i][5].ToString());
				list[i].Op             =PIn.PInt   (table.Rows[i][6].ToString());
				list[i].Note           =PIn.PString(table.Rows[i][7].ToString());
				list[i].ProvNum        =PIn.PInt   (table.Rows[i][8].ToString());
				list[i].ProvHyg        =PIn.PInt   (table.Rows[i][9].ToString());
				list[i].AptDateTime    =PIn.PDateT (table.Rows[i][10].ToString());
				list[i].NextAptNum     =PIn.PInt   (table.Rows[i][11].ToString());
				list[i].UnschedStatus  =PIn.PInt  (table.Rows[i][12].ToString());
				list[i].Lab            =(LabCase)PIn.PInt   (table.Rows[i][13].ToString());
				list[i].IsNewPatient   =PIn.PBool  (table.Rows[i][14].ToString());
				list[i].ProcDescript   =PIn.PString(table.Rows[i][15].ToString());
				list[i].Assistant      =PIn.PInt   (table.Rows[i][16].ToString());
				list[i].InstructorNum  =PIn.PInt   (table.Rows[i][17].ToString());
				list[i].SchoolClassNum =PIn.PInt   (table.Rows[i][18].ToString());
				list[i].SchoolCourseNum=PIn.PInt   (table.Rows[i][19].ToString());
				list[i].GradePoint     =PIn.PFloat (table.Rows[i][20].ToString());
				list[i].ClinicNum      =PIn.PInt   (table.Rows[i][21].ToString());
				list[i].IsHygiene      =PIn.PBool  (table.Rows[i][22].ToString());
			}
			return list;
		}

		///<summary>Gets one appointment from db.</summary>
		public static Appointment GetOneApt(int aptNum){
			if(aptNum==0){
				return null;
			}
			string command="SELECT * FROM appointment "
				+"WHERE AptNum = '"+POut.PInt(aptNum)+"'";
			Appointment[] list=FillList(command);
			if(list.Length==0){
				return null;
			}
			return list[0];
		}

		///<summary>Used when generating the recall list to test whether a patient already has a future appointment scheduled.  It was not possible to incorporate this into the main query because it would have been too complex.  A single query is planned at some point.</summary>
		public static bool PatientHasFutureRecall(int patNum){
			string command="SELECT COUNT(*) FROM appointment,procedurelog,procedurecode "
				+"WHERE procedurelog.patnum = '"+patNum.ToString()+"' "
				+"AND appointment.patnum = '"+patNum.ToString()+"' "
				+"AND procedurelog.ADACode = procedurecode.ADACode "
				+"AND procedurelog.aptnum = appointment.aptnum "
				+"AND appointment.AptDateTime >= '"+DateTime.Today.ToString("yyyy-MM-dd")+"' "
				+"AND procedurecode.SetRecall = '1'";
			DataConnection dcon=new DataConnection();
			if(dcon.GetCount(command)=="0"){
				return false;
			}
			return true;
		}

		///<summary>Used in Chart module to test whether a procedure is attached to an appointment with today's date. The procedure might have a different date if still TP status.  ApptList should include all appointments for this patient. Does not make a call to db.</summary>
		public static bool ProcIsToday(Appointment[] apptList,Procedure proc){
			for(int i=0;i<apptList.Length;i++){
				if(apptList[i].AptDateTime.Date==DateTime.Today
					&& apptList[i].AptNum==proc.AptNum
					&& (apptList[i].AptStatus==ApptStatus.Scheduled
					|| apptList[i].AptStatus==ApptStatus.ASAP
					|| apptList[i].AptStatus==ApptStatus.Broken
					|| apptList[i].AptStatus==ApptStatus.Complete))
				{
					return true;
				}
			}
			return false;
		}

		

		
		






	}
	
	


}









