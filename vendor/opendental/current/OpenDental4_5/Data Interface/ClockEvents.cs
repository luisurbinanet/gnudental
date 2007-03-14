using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Each row is either a clock-in or a clock-out event.  This table will soon be significantly changed so that each row will contain both the clock-in and the clock-out.  As it is right now, it's nearly impossible to do queries that give you summary results.</summary>
	public class ClockEvent{
		///<summary>Primary key.</summary>
		public int ClockEventNum;
		///<summary>FK to employee.EmployeeNum</summary>
		public int EmployeeNum;
		///<summary>The actual time that this entry was entered.</summary>
		public DateTime TimeEntered;
		///<summary>The time to display and to use in all calculations.</summary>
		public DateTime TimeDisplayed;
		///<summary>True for ClockIn, and false for ClockOut.</summary>
		public bool ClockIn;
		///<summary>Enum:TimeClockStatus  Home, Lunch, or Break.</summary>
		public TimeClockStatus ClockStatus;
		///<summary>.</summary>
		public string Note;
		//<summary></summary>
		//public DateTime TimeEnteredTwo;
		//<summary></summary>
		//public DateTime TimeDisplayedTwo;

		///<summary></summary>
		public ClockEvent Copy() {
			ClockEvent c=new ClockEvent();
			c.ClockEventNum=ClockEventNum;
			c.EmployeeNum=EmployeeNum;
			c.TimeEntered=TimeEntered;
			c.TimeDisplayed=TimeDisplayed;
			c.ClockIn=ClockIn;
			c.ClockStatus=ClockStatus;
			c.Note=Note;
			return c;
		}

		///<summary></summary>
		public void Insert() {
			if(Prefs.RandomKeys) {
				ClockEventNum=MiscData.GetKey("clockevent","ClockEventNum");
			}
			string command="INSERT INTO clockevent (";
			if(Prefs.RandomKeys) {
				command+="ClockEventNum,";
			}
			command+="EmployeeNum,TimeEntered,TimeDisplayed,ClockIn"
				+",ClockStatus,Note) VALUES(";
			if(Prefs.RandomKeys) {
				command+="'"+POut.PInt(ClockEventNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (EmployeeNum)+"', "
				+"'"+POut.PDateT (TimeEntered)+"', "
				+"'"+POut.PDateT (TimeDisplayed)+"', "
				+"'"+POut.PBool  (ClockIn)+"', "
				+"'"+POut.PInt   ((int)ClockStatus)+"', "
				+"'"+POut.PString(Note)+"')";
			DataConnection dcon=new DataConnection();
			if(Prefs.RandomKeys) {
				dcon.NonQ(command);
			}
			else {
				dcon.NonQ(command,true);
				ClockEventNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void Update() {
			string command= "UPDATE clockevent SET "
				+"EmployeeNum = '"    +POut.PInt   (EmployeeNum)+"' "
				+",TimeEntered = '"   +POut.PDateT (TimeEntered)+"' "
				+",TimeDisplayed = '" +POut.PDateT (TimeDisplayed)+"' "
				+",ClockIn = '"       +POut.PBool  (ClockIn)+"' "
				+",ClockStatus = '"   +POut.PInt   ((int)ClockStatus)+"' "
				+",Note = '"          +POut.PString(Note)+"' "
				+"WHERE ClockEventNum = '"+POut.PInt(ClockEventNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Delete() {
			string command= "DELETE FROM clockevent WHERE ClockEventNum = "+POut.PInt(ClockEventNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

	}

	/*=========================================================================================
		=================================== class ClockEvents ==========================================*/

	///<summary></summary>
	public class ClockEvents{
		//<summary></summary>
		//public static ClockEvent Cur;
		//<summary>For one employee only. Frequently, only a subset of the events for one employee.</summary>
		//public static ClockEvent[] List;

		///<summary>isBreaks is ignored if getAll is true.</summary>
		public static ClockEvent[] Refresh(int empNum,DateTime fromDate,DateTime toDate,bool getAll,bool isBreaks){
			string command=
				"SELECT * from clockevent WHERE"
				+" EmployeeNum = '"+POut.PInt(empNum)+"'"
				+" && TimeDisplayed >= '"+POut.PDate(fromDate)+"'"
				//adding a day takes it to midnight of the specified toDate
				+" && TimeDisplayed <= '"+POut.PDate(toDate.AddDays(1))+"'";
			if(!getAll){
				if(isBreaks)
					command+=" AND ClockStatus = '2'";
				else
					command+=" AND (ClockStatus = '0' OR ClockStatus = '1')";
			}
			command+=" ORDER BY TimeDisplayed";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			ClockEvent[] List=new ClockEvent[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new ClockEvent();
				List[i].ClockEventNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].EmployeeNum   = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].TimeEntered   = PIn.PDateT (table.Rows[i][2].ToString());
				List[i].TimeDisplayed = PIn.PDateT (table.Rows[i][3].ToString());
				List[i].ClockIn       = PIn.PBool  (table.Rows[i][4].ToString());
				List[i].ClockStatus   =(TimeClockStatus)PIn.PInt(table.Rows[i][5].ToString());
				List[i].Note          = PIn.PString(table.Rows[i][6].ToString());
			}
			return List;
		}

		//<summary>This is stupid.  It takes too long.</summary>
		//public static void Refresh(){
		//	Refresh(DateTime.MinValue,new DateTime(3000,1,1),true,true);
		//}

		

		///<summary>Gets directly from the database.  Returns true if the last time clock entry for this employee was a clockin.</summary>
		public static bool IsClockedIn(int employeeNum){
			string command="SELECT ClockIn FROM clockevent WHERE EmployeeNum="+POut.PInt(employeeNum)
				+" ORDER BY TimeDisplayed DESC LIMIT 1";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count==0)//if this employee has never clocked in or out.
				return false;
			if(PIn.PBool(table.Rows[0][0].ToString())){//if the last clockevent was a clockin
				return true;
			}
			return false;
		}

		///<summary>Gets info directly from database.  If the employee is clocked out, this gets the status for clockin is based on how they last clocked out.  Also used to determine how to initially display timecard.</summary>
		public static TimeClockStatus GetLastStatus(int employeeNum){
			string command="SELECT ClockStatus FROM clockevent WHERE EmployeeNum="+POut.PInt(employeeNum)
				+" ORDER BY TimeDisplayed DESC LIMIT 1";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count==0)//if this employee has never clocked in or out.
				return TimeClockStatus.Home;
			return (TimeClockStatus)PIn.PInt(table.Rows[0][0].ToString());
		}

		///<summary></summary>
		public static DateTime GetServerTime(){
			string command="SELECT NOW()";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			return PIn.PDateT(table.Rows[0][0].ToString());
		}

		///<summary>Used in the timecard to track hours worked per week when the week started in a previous time period.  This gets all the hours of the first week before the date listed.  Also adds in any adjustments for that week.</summary>
		public static TimeSpan GetWeekTotal(int empNum,DateTime date){
			ClockEvent[] events=Refresh(empNum,date.AddDays(-6),date.AddDays(-1),false,false);
			//eg, if this is Thursday, then we are getting last Friday through this Wed.
			TimeSpan retVal=new TimeSpan(0);
			for(int i=0;i<events.Length;i++){
				if(events[i].TimeDisplayed.DayOfWeek > date.DayOfWeek){//eg, Friday > Thursday, so ignore
					continue;
				}
				if(i>0 && !events[i].ClockIn){
					retVal+=events[i].TimeDisplayed-events[i-1].TimeDisplayed;
				}
			}
			//now, adjustments
			TimeAdjust[] TimeAdjustList=TimeAdjusts.Refresh(empNum,date.AddDays(-6),date.AddDays(-1));
			for(int i=0;i<TimeAdjustList.Length;i++) {
				if(TimeAdjustList[i].TimeEntry.DayOfWeek > date.DayOfWeek) {//eg, Friday > Thursday, so ignore
					continue;
				}
				retVal+=TimeAdjustList[i].RegHours;
			}
			return retVal;
		}




	}

	
}




