using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the appointment table in the database.</summary>
	public struct Appointment{
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
		///<summary>Operatory.  Foreign key to <see cref="Def.DefNum">definition.DefNum</see>.</summary>
		///<remarks>The <see cref="Def.Category">definition.Category</see> in the definition table is <see cref="DefCat.Operatories">DefCat.Operatories</see>.</remarks>
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
	}

	/*=========================================================================================
	=================================== class Appointments ==========================================*/

	///<summary>Handles database commands related to the appointment table in the db.</summary>
	public class Appointments:DataClass{
		//<summary>This is a temporary private list of appointments which then gets copied to one of the three public lists.</summary>
		//private static Appointment[] List;
		///<summary>A list of appointments for one day in the schedule, whether hidden or not.</summary>
		public static Appointment[] ListDay;
		///<summary>A list of appointments for use on the Unscheduled list or the Next appointment tracker.</summary>
		public static Appointment[] ListUn;
		//<summary>A list of appointments for use on the Other appointments list for a single patient.</summary>
		//public static Appointment[] ListOthh;
		///<summary>Current appointment.  A single row of data.</summary>
		public static Appointment Cur;
		///<summary>When doing update, this Appointment is the original before any changes were made. This allows only the changed fields to be updated, minimizing concurrency issues.</summary>
		public static Appointment CurOld;
		///<summary>The appointment on the pinboard.</summary>
		public static Appointment PinBoard;
		///<summary>The date currently selected in the appointment module.</summary>
		public static DateTime DateSelected;

		///<summary>Gets the ListDay for a given date.</summary>
		public static void Refresh(DateTime thisDay){
			DateSelected = thisDay;
			string command=
				"SELECT * from appointment "
				+"WHERE AptDateTime LIKE '"+POut.PDate(thisDay)+"%' "
				+"AND aptstatus != '"+(int)ApptStatus.UnschedList+"' "
				+"AND aptstatus != '"+(int)ApptStatus.Planned+"'";
			ListDay=FillList(command);
		}

		///<summary>Gets ListUn for both the unscheduled list and for planned appt tracker.  This is in transition, since the unscheduled list will probably eventually be phased out.  Set true if getting Planned appointments, false if getting Unscheduled appointments.</summary>
		public static void RefreshUnsched(bool doGetPlanned){
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
			ListUn=FillList(command);
		}

		///<summary>Returns all appointments for the given patient, ordered from earliest to latest.  Used in statements, appt cards, OtherAppts window, etc.</summary>
		public static Appointment[] GetForPat(int patNum){
			string command=
				"SELECT * FROM appointment "
				+"WHERE patnum = '"+patNum.ToString()+"' "
				+"ORDER BY AptDateTime";
			return FillList(command);
		}

		/*
		///<summary>Returns all future appts for a patient, ordered from  .</summary>
		public static Appointment[] GetFutureScheduled(int patNum){
			string command="SELECT AptDateTime FROM appointment "
				+"WHERE "
				+"AptDateTime > CURDATE() AND "
				+"PatNum="+patNum.ToString()
				+" ORDER BY AptDateTime";
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			DateTime[] retVal=new DateTime[table.Rows.Count];
			//MessageBox.Show(table.Rows.Count.ToString());
			for(int i=0;i<table.Rows.Count;i++){
				retVal[i]=PIn.PDateT(table.Rows[i][0].ToString());
			}
			return retVal;
		}*/

		///<summary>Fills the specified array of Appointments using the supplied SQL command.</summary>
		private static Appointment[] FillList(string command){
			cmd.CommandText=command;
			FillTable();
			Appointment[] list=new Appointment[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
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
			}
			return list;
		}

		///<summary></summary>
		public static void InsertCur(){
			if(Prefs.RandomKeys){
				Cur.AptNum=MiscData.GetKey("appointment","AptNum");
			}
			cmd.CommandText="INSERT INTO appointment (";
			if(Prefs.RandomKeys){
				cmd.CommandText+="AptNum,";
			}
			cmd.CommandText+="patnum,aptstatus, "
				+"pattern,confirmed,addtime,op,note,provnum,"
				+"provhyg,aptdatetime,nextaptnum,unschedstatus,lab,isnewpatient,procdescript,"
				+"Assistant,InstructorNum,SchoolClassNum,SchoolCourseNum,GradePoint) VALUES(";
			if(Prefs.RandomKeys){
				cmd.CommandText+="'"+POut.PInt(Cur.AptNum)+"', ";
			}
			cmd.CommandText+=
				 "'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PInt   ((int)Cur.AptStatus)+"', "
				+"'"+POut.PString(Cur.Pattern)+"', "
				+"'"+POut.PInt   (Cur.Confirmed)+"', "
				+"'"+POut.PInt   (Cur.AddTime)+"', "
				+"'"+POut.PInt   (Cur.Op)+"', "
				+"'"+POut.PString(Cur.Note)+"', "
				+"'"+POut.PInt   (Cur.ProvNum)+"', "
				+"'"+POut.PInt   (Cur.ProvHyg)+"', "
				+"'"+POut.PDateT (Cur.AptDateTime)+"', "
				+"'"+POut.PInt   (Cur.NextAptNum)+"', "
				+"'"+POut.PInt   (Cur.UnschedStatus)+"', "
				+"'"+POut.PInt   ((int)Cur.Lab)+"', "
				+"'"+POut.PBool  (Cur.IsNewPatient)+"', "
				+"'"+POut.PString(Cur.ProcDescript)+"', "
				+"'"+POut.PInt   (Cur.Assistant)+"', "
				+"'"+POut.PInt   (Cur.InstructorNum)+"', "
				+"'"+POut.PInt   (Cur.SchoolClassNum)+"', "
				+"'"+POut.PInt   (Cur.SchoolCourseNum)+"', "
				+"'"+POut.PFloat (Cur.GradePoint)+"')";
			if(Prefs.RandomKeys){
				NonQ();
			}
			else{
 				NonQ(true);
				Cur.AptNum=InsertID;
			}
		}

		///<summary>Updates only the changed columns and returns the number of rows affected.</summary>
		public static int UpdateCur(){
			bool comma=false;
			string c = "UPDATE appointment SET ";
			if(Cur.PatNum!=CurOld.PatNum){
				c+="PatNum = '"      +POut.PInt   (Cur.PatNum)+"'";
				comma=true;
			}
			if(Cur.AptStatus!=CurOld.AptStatus){
				if(comma) c+=",";
				c+="AptStatus = '"   +POut.PInt   ((int)Cur.AptStatus)+"'";
				comma=true;
			}
			if(Cur.Pattern!=CurOld.Pattern){
				if(comma) c+=",";
				c+="Pattern = '"     +POut.PString(Cur.Pattern)+"'";
				comma=true;
			}
			if(Cur.Confirmed!=CurOld.Confirmed){
				if(comma) c+=",";
				c+="Confirmed = '"   +POut.PInt   (Cur.Confirmed)+"'";
				comma=true;
			}
			if(Cur.AddTime!=CurOld.AddTime){
				if(comma) c+=",";
				c+="AddTime = '"     +POut.PInt   (Cur.AddTime)+"'";
				comma=true;
			}
			if(Cur.Op!=CurOld.Op){
				if(comma) c+=",";
				c+="Op = '"          +POut.PInt   (Cur.Op)+"'";
				comma=true;
			}
			if(Cur.Note!=CurOld.Note){
				if(comma) c+=",";
				c+="Note = '"        +POut.PString(Cur.Note)+"'";
				comma=true;
			}
			if(Cur.ProvNum!=CurOld.ProvNum){
				if(comma) c+=",";
				c+="ProvNum = '"     +POut.PInt   (Cur.ProvNum)+"'";
				comma=true;
			}
			if(Cur.ProvHyg!=CurOld.ProvHyg){
				if(comma) c+=",";
				c+="ProvHyg = '"     +POut.PInt   (Cur.ProvHyg)+"'";
				comma=true;
			}
			if(Cur.AptDateTime!=CurOld.AptDateTime){
				if(comma) c+=",";
				c+="AptDateTime = '" +POut.PDateT (Cur.AptDateTime)+"'";
				comma=true;
			}
			if(Cur.NextAptNum!=CurOld.NextAptNum){
				if(comma) c+=",";
				c+="NextAptNum = '"  +POut.PInt   (Cur.NextAptNum)+"'";
				comma=true;
			}
			if(Cur.UnschedStatus!=CurOld.UnschedStatus){
				if(comma) c+=",";
				c+="UnschedStatus = '" +POut.PInt(Cur.UnschedStatus)+"'";
				comma=true;
			}
			if(Cur.Lab!=CurOld.Lab){
				if(comma) c+=",";
				c+="Lab = '"         +POut.PInt   ((int)Cur.Lab)+"'";
				comma=true;
			}
			if(Cur.IsNewPatient!=CurOld.IsNewPatient){
				if(comma) c+=",";
				c+="IsNewPatient = '"+POut.PBool  (Cur.IsNewPatient)+"'";
				comma=true;
			}
			if(Cur.ProcDescript!=CurOld.ProcDescript){
				if(comma) c+=",";
				c+="ProcDescript = '"+POut.PString(Cur.ProcDescript)+"'";
				comma=true;
			}
			if(Cur.Assistant!=CurOld.Assistant){
				if(comma) c+=",";
				c+="Assistant = '"   +POut.PInt   (Cur.Assistant)+"'";
				comma=true;
			}
			if(Cur.InstructorNum!=CurOld.InstructorNum){
				if(comma) c+=",";
				c+="InstructorNum = '"   +POut.PInt   (Cur.InstructorNum)+"'";
				comma=true;
			}
			if(Cur.SchoolClassNum!=CurOld.SchoolClassNum){
				if(comma) c+=",";
				c+="SchoolClassNum = '"   +POut.PInt   (Cur.SchoolClassNum)+"'";
				comma=true;
			}
			if(Cur.SchoolCourseNum!=CurOld.SchoolCourseNum){
				if(comma) c+=",";
				c+="SchoolCourseNum = '"   +POut.PInt   (Cur.SchoolCourseNum)+"'";
				comma=true;
			}
			if(Cur.GradePoint!=CurOld.GradePoint){
				if(comma) c+=",";
				c+="GradePoint = '"   +POut.PFloat  (Cur.GradePoint)+"'";
				comma=true;
			}
			if(!comma)
				return 0;//this means no change is actually required.
			c+=" WHERE AptNum = '"+POut.PInt(Cur.AptNum)+"'";
			cmd.CommandText=c;
			//MessageBox.Show(cmd.CommandText);
			return NonQ();
		}

		///<summary>Gets one appointment and stores the info in Cur and CurOld.</summary>
		public static void RefreshCur(int aptNum){
			cmd.CommandText =
				"SELECT * "
				+"FROM appointment "
				+"WHERE aptnum = '"+POut.PInt(aptNum)+"'";
			FillTable();
			Cur=new Appointment();
			if(table.Rows.Count==0){
				return;
			}
			Cur.AptNum         =PIn.PInt   (table.Rows[0][0].ToString());
			Cur.PatNum         =PIn.PInt   (table.Rows[0][1].ToString());
			Cur.AptStatus      =(ApptStatus)PIn.PInt(table.Rows[0][2].ToString());
			Cur.Pattern        =PIn.PString(table.Rows[0][3].ToString());
			Cur.Confirmed      =PIn.PInt   (table.Rows[0][4].ToString());
			Cur.AddTime        =PIn.PInt   (table.Rows[0][5].ToString());
			Cur.Op             =PIn.PInt   (table.Rows[0][6].ToString());
			Cur.Note           =PIn.PString(table.Rows[0][7].ToString());
			Cur.ProvNum        =PIn.PInt   (table.Rows[0][8].ToString());
			Cur.ProvHyg        =PIn.PInt   (table.Rows[0][9].ToString());
			Cur.AptDateTime    =PIn.PDateT (table.Rows[0][10].ToString());
			Cur.NextAptNum     =PIn.PInt   (table.Rows[0][11].ToString());
			Cur.UnschedStatus  =PIn.PInt (table.Rows[0][12].ToString());
			Cur.Lab            =(LabCase)PIn.PInt(table.Rows[0][13].ToString());
			Cur.IsNewPatient   =PIn.PBool  (table.Rows[0][14].ToString());
			Cur.ProcDescript   =PIn.PString(table.Rows[0][15].ToString());
			Cur.Assistant      =PIn.PInt   (table.Rows[0][16].ToString());
			Cur.InstructorNum  =PIn.PInt   (table.Rows[0][17].ToString());
			Cur.SchoolClassNum =PIn.PInt   (table.Rows[0][18].ToString());
			Cur.SchoolCourseNum=PIn.PInt   (table.Rows[0][19].ToString());
			Cur.GradePoint     =PIn.PFloat (table.Rows[0][20].ToString());
			CurOld=Cur;
		}
	
		///<summary>The patient object is needed to update the date first visit.</summary>
		public static void DeleteCur(Patient pat){
			Procedures.SetDateFirstVisit(DateTime.MinValue,3,pat);
			cmd.CommandText="DELETE from appointment WHERE "
				+"aptnum = '"+POut.PInt(Cur.AptNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary>Used when generating the recall list to test whether a patient already has a future appointment scheduled.  It was not possible to incorporate this into the main query because it would have been too complex.  A single query is planned at some point.</summary>
		public static bool PatientHasFutureRecall(int patNum){
			cmd.CommandText="SELECT appointment.patNum FROM appointment,procedurelog,procedurecode "
				+"WHERE procedurelog.patnum = '"+patNum.ToString()+"' "
				+"AND appointment.patnum = '"+patNum.ToString()+"' "
				+"AND procedurelog.ADACode = procedurecode.ADACode "
				+"AND procedurelog.aptnum = appointment.aptnum "
				+"AND appointment.AptDateTime >= '"+DateTime.Today.ToString("yyyy-MM-dd")+"' "
				+"AND procedurecode.SetRecall = '1'";
			FillTable();
			if(table.Rows.Count==0){
				return false;
			}
			else{
				return true;
			}
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








