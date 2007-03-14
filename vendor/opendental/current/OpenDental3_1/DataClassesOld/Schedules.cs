using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the schedule table in the database.</summary>
	public struct Schedule{
		///<summary>Primary key.</summary>
		public int ScheduleNum;
		///<summary>Date for this timeblock.</summary>
		public DateTime SchedDate;
		///<summary>Start time for this timeblock.</summary>
		public DateTime StartTime;
		///<summary>Stop time for this timeblock.</summary>
		public DateTime StopTime;
		///<summary>See the ScheduleType enumeration.</summary>
		public ScheduleType SchedType;
		///<summary>Not in use yet. Will be used for provider schedules.</summary>
		public int ProvNum;
		///<summary>(not in use yet)foreign key to definition.DefNum.  eg. HighProduction, RCT Only, Emerg.</summary>
		public int BlockoutType;
		///<summary>This contains various types of text entered by the user.</summary>
		public string Note;
		///<summary>See the SchedStatus enumeration.</summary>
		public SchedStatus Status;
	}

	/*=========================================================================================
		=================================== class Schedules ==========================================*/
///<summary></summary>
	public class Schedules:DataClass{
		///<summary></summary>
		public static Schedule[] ListMonth;
		///<summary></summary>
		public static Schedule[] ListDay;
		///<summary></summary>
		public static Schedule Cur;
		///<summary></summary>
		public static DateTime CurDate;
		//private static ArrayList AL;
	
		///<summary></summary>
		public static void RefreshMonth(){
			//used in the schedule setup window
			cmd.CommandText =
				"SELECT * FROM schedule WHERE MONTH(SchedDate)='"+CurDate.Month.ToString()
				+"' && YEAR(SchedDate)='"+CurDate.Year.ToString()+"' "
				+"ORDER BY starttime";
			FillTable();
			ListMonth=new Schedule[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				ListMonth[i].ScheduleNum    = PIn.PInt   (table.Rows[i][0].ToString());
				ListMonth[i].SchedDate      = PIn.PDate  (table.Rows[i][1].ToString());
				ListMonth[i].StartTime      = PIn.PDateT (table.Rows[i][2].ToString());
				ListMonth[i].StopTime       = PIn.PDateT (table.Rows[i][3].ToString());
				//schedtype
				//provnum
				//blockouttype
				ListMonth[i].Note           = PIn.PString(table.Rows[i][7].ToString());
				ListMonth[i].Status         = (SchedStatus)PIn.PInt(table.Rows[i][8].ToString());        
			}
		}

		///<summary></summary>
		public static void RefreshDay(DateTime thisDay){
			//Called every time the day is refreshed or changed in Appointments module
			CurDate=thisDay;//may revise later
			cmd.CommandText =
				"SELECT * FROM schedule WHERE SchedDate='"+POut.PDate(CurDate)+"'"
				+" ORDER BY starttime";
			FillTable();
			ListDay=new Schedule[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				ListDay[i].ScheduleNum    = PIn.PInt   (table.Rows[i][0].ToString());
				ListDay[i].SchedDate      = PIn.PDate  (table.Rows[i][1].ToString());
				ListDay[i].StartTime      = PIn.PDateT (table.Rows[i][2].ToString());
				ListDay[i].StopTime       = PIn.PDateT (table.Rows[i][3].ToString());
				//schedtype
				//provnum
				//blockouttype
				ListDay[i].Note           = PIn.PString(table.Rows[i][7].ToString());
				ListDay[i].Status         = (SchedStatus)PIn.PInt(table.Rows[i][8].ToString());        
			}
		}

		/*public static void GetDayList(){
			AL=new ArrayList();
			for(int i=0;i<table.Rows.Count;i++){
				if(CurDate.Day==List[i].Date.Day){
					AL.Add(List[i]);
				}
			}
			DayList=new Schedule[AL.Count];
			AL.CopyTo(DayList);
		}   */ 

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE schedule SET " 
				+ "scheddate = '"  +POut.PDate  (Cur.SchedDate)+"'"
				+ ",starttime = '" +POut.PDateT (Cur.StartTime)+"'"
				+ ",stoptime = '"  +POut.PDateT (Cur.StopTime)+"'"
				//schedtype
				//provnum
				//blockouttype
				+ ",note = '"      +POut.PString(Cur.Note)+"'"
				+ ",status = '"    +POut.PInt   ((int)Cur.Status)+"'"
				+" WHERE ScheduleNum = '" +POut.PInt (Cur.ScheduleNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO schedule (scheddate,starttime,stoptime,note,status"
				+") VALUES("
				+"'"+POut.PDate  (Cur.SchedDate)     +"', "
				+"'"+POut.PDateT (Cur.StartTime)+"', "
				+"'"+POut.PDateT (Cur.StopTime) +"', "
				//schedtype
				//provnum
				//blockouttype
				+"'"+POut.PString(Cur.Note)     +"', "
				+"'"+POut.PInt   ((int)Cur.Status)   +"')";
			NonQ(true);
			Cur.ScheduleNum=InsertID;
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from schedule WHERE schedulenum = '"+POut.PInt(Cur.ScheduleNum)+"'";
			NonQ(false);
		}
	}

	

	

}













