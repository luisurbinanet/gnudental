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
		///<summary>Time pattern, X for Dr time, / for assist time.</summary>
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
		///<summary>Only used to show that this apt is derived from specified next apt. Otherwise, 0. Foreign key to appointment.AptNum.</summary>
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
	}

	/*=========================================================================================
	=================================== class Appointments ==========================================*/

	///<summary>Handles database commands related to the appointment table in the db.</summary>
	public class Appointments:DataClass{
		///<summary>This is a temporary private list of appointments which then gets copied to one of the three public lists.</summary>
		private static Appointment[] List;
		///<summary>A list of appointments for one day in the schedule, whether hidden or not.</summary>
		public static Appointment[] ListDay;
		///<summary>A list of appointments for use on the Unscheduled list or the Next appointment tracker.</summary>
		public static Appointment[] ListUn;
		///<summary>A list of appointments for use on the Other appointments list for a single patient.</summary>
		public static Appointment[] ListOth;
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
			cmd.CommandText =
				"SELECT * from appointment "
				+"WHERE AptDateTime LIKE '"+POut.PDate(thisDay)+"%' "
				+"&& aptstatus != '"+(int)ApptStatus.UnschedList+"' "
				+"&& aptstatus != '"+(int)ApptStatus.Next+"'";
			FillList();
			ListDay=List;
			List=null;
		}

		///<summary>Gets ListUn for both the unscheduled list and for next appt tracker.
		///This is in transition, since the unscheduled list will probably eventually be phased out.</summary>
		///<param name="doGetNext">True if getting Next appointments, false if getting Unscheduled appointments.</param>
		public static void RefreshUnsched(bool doGetNext){
			if(doGetNext){
				cmd.CommandText="SELECT Tnext.*,Tregular.aptnum FROM appointment AS Tnext "
					+"LEFT JOIN appointment AS Tregular ON Tnext.aptnum = Tregular.nextaptnum "
					+"WHERE Tnext.aptstatus = '"+(int)ApptStatus.Next+"' "
					+"AND Tregular.aptnum IS NULL "
					+"ORDER BY Tnext.UnschedStatus,Tnext.AptDateTime";
			}
			else{//unsched
				cmd.CommandText="SELECT * FROM appointment "
					+"WHERE aptstatus = '"+(int)ApptStatus.UnschedList+"' "
					+"ORDER BY AptDateTime";
			}
			FillList();
			ListUn=List;
			List=null;
		}

		///<summary>Gets the ListOth for the current patient.</summary>
		public static void RefreshOther(){
			cmd.CommandText =
				"SELECT * FROM appointment "
				+"WHERE patnum = '"+Patients.Cur.PatNum.ToString()+"' "
				+"ORDER BY AptDateTime";
			FillList();
			ListOth=List;
			List=null;
		}

		private static void FillList(){
			FillTable();
			List = new Appointment[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].AptNum      =PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum      =PIn.PInt   (table.Rows[i][1].ToString());
				List[i].AptStatus   =(ApptStatus)PIn.PInt(table.Rows[i][2].ToString());
				List[i].Pattern     =PIn.PString(table.Rows[i][3].ToString());
				List[i].Confirmed   =PIn.PInt   (table.Rows[i][4].ToString());
				List[i].AddTime     =PIn.PInt   (table.Rows[i][5].ToString());
				List[i].Op          =PIn.PInt   (table.Rows[i][6].ToString());
				List[i].Note        =PIn.PString(table.Rows[i][7].ToString());
				List[i].ProvNum     =PIn.PInt   (table.Rows[i][8].ToString());
				List[i].ProvHyg     =PIn.PInt   (table.Rows[i][9].ToString());
				List[i].AptDateTime =PIn.PDateT (table.Rows[i][10].ToString());
				List[i].NextAptNum  =PIn.PInt   (table.Rows[i][11].ToString());
				List[i].UnschedStatus=PIn.PInt  (table.Rows[i][12].ToString());
				List[i].Lab         =(LabCase)PIn.PInt   (table.Rows[i][13].ToString());
				List[i].IsNewPatient=PIn.PBool  (table.Rows[i][14].ToString());
				List[i].ProcDescript=PIn.PString(table.Rows[i][15].ToString());
				List[i].Assistant   =PIn.PInt   (table.Rows[i][16].ToString());
			}
		}

		///<summary>Also fills AptNum with the insertID.</summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO appointment (patnum,aptstatus, "
				+"pattern,confirmed,addtime,op,note,provnum,"
				+"provhyg,aptdatetime,nextaptnum,unschedstatus,lab,isnewpatient,procdescript,"
				+"Assistant) VALUES("
				+"'"+POut.PInt   (Cur.PatNum)+"', "
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
				+"'"+POut.PInt   (Cur.Assistant)+"')";
			NonQ(true);
			Cur.AptNum=InsertID;
			//MessageBox.Show(Cur.AptNum.ToString());
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
			Cur.AptNum      =PIn.PInt   (table.Rows[0][0].ToString());
			Cur.PatNum      =PIn.PInt   (table.Rows[0][1].ToString());
			Cur.AptStatus   =(ApptStatus)PIn.PInt(table.Rows[0][2].ToString());
			Cur.Pattern     =PIn.PString(table.Rows[0][3].ToString());
			Cur.Confirmed   =PIn.PInt   (table.Rows[0][4].ToString());
			Cur.AddTime     =PIn.PInt   (table.Rows[0][5].ToString());
			Cur.Op          =PIn.PInt   (table.Rows[0][6].ToString());
			Cur.Note        =PIn.PString(table.Rows[0][7].ToString());
			Cur.ProvNum     =PIn.PInt   (table.Rows[0][8].ToString());
			Cur.ProvHyg     =PIn.PInt   (table.Rows[0][9].ToString());
			Cur.AptDateTime =PIn.PDateT (table.Rows[0][10].ToString());
			Cur.NextAptNum  =PIn.PInt   (table.Rows[0][11].ToString());
			Cur.UnschedStatus =PIn.PInt (table.Rows[0][12].ToString());
			Cur.Lab         =(LabCase)PIn.PInt(table.Rows[0][13].ToString());
			Cur.IsNewPatient=PIn.PBool  (table.Rows[0][14].ToString());
			Cur.ProcDescript=PIn.PString(table.Rows[0][15].ToString());
			Cur.Assistant   =PIn.PInt   (table.Rows[0][16].ToString());
			CurOld=Cur;
		}
	
		///<summary></summary>
		public static void DeleteCur(){
			Procedures.SetDateFirstVisit(DateTime.MinValue,3);
			cmd.CommandText="DELETE from appointment WHERE "
				+"aptnum = '"+POut.PInt(Cur.AptNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary>Used when generating the recall list to test whether a patient already has a future appointment scheduled.</summary>
		///<param name="patNum"></param>
		///<returns></returns>
		///<remarks>It was not possible to incorporate this into the main query because it would have been too complex.  A single query might be a good idea at some point.</remarks>
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

		
		






	}
	
	


}









