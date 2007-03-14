using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the clockevent table in the database.</summary>
	public struct ClockEvent{
		///<summary>Primary key.</summary>
		public int ClockEventNum;
		///<summary>Foreign key to employee.EmployeeNum</summary>
		public int EmployeeNum;
		///<summary>The actual time that this entry was entered.</summary>
		public DateTime TimeEntered;
		///<summary>The time to display and to use in all calculations.</summary>
		public DateTime TimeDisplayed;
		///<summary>True for ClockIn, and false for ClockOut.</summary>
		public bool ClockIn;
		///<summary>Enum TimeClockStatus.  Home, Lunch, or Break.</summary>
		public TimeClockStatus ClockStatus;
		///<summary></summary>
		public string Note;
	}

	/*=========================================================================================
		=================================== class ClockEvents ==========================================*/

	///<summary></summary>
	public class ClockEvents:DataClass{
		///<summary></summary>
		public static ClockEvent Cur;
		///<summary>For one employee only. Frequently, only a subset of the events for one employee.</summary>
		public static ClockEvent[] List;

		///<summary></summary>
		///<remarks>isBreaks is ignored if getAll is true.</remarks>
		public static void Refresh(DateTime fromDate,DateTime toDate,bool getAll,bool isBreaks){
			cmd.CommandText =
				"SELECT * from clockevent WHERE"
				+" EmployeeNum = '"+Employees.Cur.EmployeeNum.ToString()+"'"
				+" && TimeDisplayed >= '"+POut.PDate(fromDate)+"'"
				//adding a day takes it to midnight of the specified toDate
				+" && TimeDisplayed <= '"+POut.PDate(toDate.AddDays(1))+"'";
			if(!getAll){
				if(isBreaks)
					cmd.CommandText+=" && ClockStatus = '2'";
				else
					cmd.CommandText+=" && (ClockStatus = '0' OR ClockStatus = '1')";
			}
			cmd.CommandText+=
				" ORDER BY TimeDisplayed";
			FillTable();
			List=new ClockEvent[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i].ClockEventNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].EmployeeNum   = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].TimeEntered   = PIn.PDateT (table.Rows[i][2].ToString());
				List[i].TimeDisplayed = PIn.PDateT (table.Rows[i][3].ToString());
				List[i].ClockIn       = PIn.PBool  (table.Rows[i][4].ToString());
				List[i].ClockStatus   =(TimeClockStatus)PIn.PInt(table.Rows[i][5].ToString());
				List[i].Note          = PIn.PString(table.Rows[i][6].ToString());
			}
		}

		//<summary>This is stupid.  It takes too long.</summary>
		//public static void Refresh(){
		//	Refresh(DateTime.MinValue,new DateTime(3000,1,1),true,true);
		//}

		///<summary></summary>
		public static void InsertCur(){
			if(Prefs.RandomKeys){
				Cur.ClockEventNum=MiscData.GetKey("clockevent","ClockEventNum");
			}
			cmd.CommandText="INSERT INTO clockevent (";
			if(Prefs.RandomKeys){
				cmd.CommandText+="ClockEventNum,";
			}
			cmd.CommandText+="EmployeeNum,TimeEntered,TimeDisplayed,ClockIn"
				+",ClockStatus,Note) VALUES(";
			if(Prefs.RandomKeys){
				cmd.CommandText+="'"+POut.PInt(Cur.ClockEventNum)+"', ";
			}
			cmd.CommandText+=
				 "'"+POut.PInt   (Cur.EmployeeNum)+"', "
				+"'"+POut.PDateT (Cur.TimeEntered)+"', "
				+"'"+POut.PDateT (Cur.TimeDisplayed)+"', "
				+"'"+POut.PBool  (Cur.ClockIn)+"', "
				+"'"+POut.PInt   ((int)Cur.ClockStatus)+"', "
				+"'"+POut.PString(Cur.Note)+"')";
			if(Prefs.RandomKeys){
				NonQ();
			}
			else{
 				NonQ(true);
				Cur.ClockEventNum=InsertID;
			}
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE clockevent SET "
				+"EmployeeNum = '"    +POut.PInt   (Cur.EmployeeNum)+"' "
				+",TimeEntered = '"   +POut.PDateT (Cur.TimeEntered)+"' "
				+",TimeDisplayed = '" +POut.PDateT (Cur.TimeDisplayed)+"' "
				+",ClockIn = '"       +POut.PBool  (Cur.ClockIn)+"' "
				+",ClockStatus = '"   +POut.PInt   ((int)Cur.ClockStatus)+"' "
				+",Note = '"          +POut.PString(Cur.Note)+"' "
				+"WHERE ClockEventNum = '"+POut.PInt(Cur.ClockEventNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ();
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM clockevent WHERE ClockEventNum = '"+Cur.ClockEventNum.ToString()+"'";
			NonQ();
		}

		///<summary>Gets directly from the database.  Returns true if the last time clock entry for this employee was a clockin.</summary>
		public static bool IsClockedIn(int employeeNum){
			cmd.CommandText="SELECT ClockIn FROM clockevent WHERE EmployeeNum="+POut.PInt(employeeNum)
				+" ORDER BY TimeDisplayed DESC LIMIT 1";
			FillTable();
			if(table.Rows.Count==0)//if this employee has never clocked in or out.
				return false;
			if(PIn.PBool(table.Rows[0][0].ToString())){//if the last clockevent was a clockin
				return true;
			}
			return false;
		}

		///<summary>Gets info directly from database.  If the employee is clocked out, this gets the status for clockin is based on how they last clocked out.  Also used to determine how to initially display timecard.</summary>
		public static TimeClockStatus GetLastStatus(int employeeNum){
			cmd.CommandText="SELECT ClockStatus FROM clockevent WHERE EmployeeNum="+POut.PInt(employeeNum)
				+" ORDER BY TimeDisplayed DESC LIMIT 1";
			FillTable();
			if(table.Rows.Count==0)//if this employee has never clocked in or out.
				return TimeClockStatus.Home;
			return (TimeClockStatus)PIn.PInt(table.Rows[0][0].ToString());
		}

		///<summary></summary>
		public static DateTime GetServerTime(){
			cmd.CommandText="SELECT NOW()";
			FillTable();
			return PIn.PDateT(table.Rows[0][0].ToString());
		}




	}

	
}



