using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the schedule table in the database.</summary>
	public class Schedule{
		///<summary>Primary key.</summary>
		public int ScheduleNum;
		///<summary>Date for this timeblock.</summary>
		public DateTime SchedDate;
		///<summary>Start time for this timeblock.</summary>
		public DateTime StartTime;
		///<summary>Stop time for this timeblock.</summary>
		public DateTime StopTime;
		///<summary>See the ScheduleType enumeration. Practice,Provider,Blockout.</summary>
		public ScheduleType SchedType;
		///<summary>If a provider schedule, Foreign key to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>foreign key to definition.DefNum.  eg. HighProduction, RCT Only, Emerg.</summary>
		public int BlockoutType;
		///<summary>This contains various types of text entered by the user.</summary>
		public string Note;
		///<summary>See the SchedStatus enumeration. Open,Closed,Holiday.  All blockouts have a status of Open, but user doesn't see the status.  There is one hidden blockout with a status of closed for when user deletes the last default blockout on a day.</summary>
		public SchedStatus Status;
		///<summary>Foreign key to definition.DefNum.  Only used right now for Blockouts.  Will later add practice type.  If 0, then it applies to all ops.</summary>
		public int Op;

		///<summary></summary>
		private void Update(){
			string command= "UPDATE schedule SET " 
				+ "scheddate = '"    +POut.PDate  (SchedDate)+"'"
				+ ",starttime = '"   +POut.PDateT (StartTime)+"'"
				+ ",stoptime = '"    +POut.PDateT (StopTime)+"'"
				+ ",SchedType = '"   +POut.PInt   ((int)SchedType)+"'"
				+ ",ProvNum = '"     +POut.PInt   (ProvNum)+"'"
				+ ",BlockoutType = '"+POut.PInt   (BlockoutType)+"'"
				+ ",note = '"        +POut.PString(Note)+"'"
				+ ",status = '"      +POut.PInt   ((int)Status)+"'"
				+ ",Op = '"          +POut.PInt   (Op)+"'"
				+" WHERE ScheduleNum = '" +POut.PInt (ScheduleNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		private void Insert(){
			string command= "INSERT INTO schedule (scheddate,starttime,stoptime,"
				+"SchedType,ProvNum,BlockoutType,Note,Status,Op"
				+") VALUES("
				+"'"+POut.PDate  (SchedDate)+"', "
				+"'"+POut.PDateT (StartTime)+"', "
				+"'"+POut.PDateT (StopTime)+"', "
				+"'"+POut.PInt   ((int)SchedType)+"', "
				+"'"+POut.PInt   (ProvNum)+"', "
				+"'"+POut.PInt   (BlockoutType)+"', "
				+"'"+POut.PString(Note)+"', "
				+"'"+POut.PInt   ((int)Status)+"', "
				+"'"+POut.PInt   (Op)+"')";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			ScheduleNum=dcon.InsertID;
		}

		///<summary></summary>
		public void InsertOrUpdate(bool isNew){
			if(StartTime.TimeOfDay > StopTime.TimeOfDay){
				throw new Exception(Lan.g("Schedule","Stop time must be later than start time."));
			}
			if(StartTime.TimeOfDay+TimeSpan.FromMinutes(5) > StopTime.TimeOfDay
				&& Status==SchedStatus.Open)
			{
				throw new Exception(Lan.g("Schedule","Stop time cannot be the same as the start time."));
			}
			if(Overlaps()){
				throw new Exception(Lan.g("Schedule","Cannot overlap another time block."));
			}
			if(isNew){
				Insert();
			}
			else{
				Update();
			}
		}

		///<summary></summary>
		private bool Overlaps(){
			Schedule[] SchedListDay=Schedules.RefreshDay(SchedDate);
			Schedule[] ListForType=Schedules.GetForType(SchedListDay,SchedType,ProvNum);
			for(int i=0;i<ListForType.Length;i++){
				if(ListForType[i].SchedType==ScheduleType.Blockout){
					//skip if blockout, and ops don't interfere
					if(Op!=0 && ListForType[i].Op!=0){//neither op can be zero, or they will interfere
						if(Op != ListForType[i].Op){
							continue;
						}
					}
				}
				if(ScheduleNum!=ListForType[i].ScheduleNum
					&& StartTime.TimeOfDay >= ListForType[i].StartTime.TimeOfDay
					&& StartTime.TimeOfDay < ListForType[i].StopTime.TimeOfDay)
				{
					return true;
				}
				if(ScheduleNum!=ListForType[i].ScheduleNum
					&& StopTime.TimeOfDay > ListForType[i].StartTime.TimeOfDay
					&& StopTime.TimeOfDay <= ListForType[i].StopTime.TimeOfDay)
				{
					return true;
				}
				if(ScheduleNum!=ListForType[i].ScheduleNum
					&& StartTime.TimeOfDay <= ListForType[i].StartTime.TimeOfDay
					&& StopTime.TimeOfDay >= ListForType[i].StopTime.TimeOfDay)
				{
					return true;
				}
			}
			return false;
		}

		///<summary></summary>
		public void Delete(){
			string command= "DELETE from schedule WHERE schedulenum = '"+POut.PInt(ScheduleNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
			//if this was the last blockout for a day, then create a blockout for 'closed'
			if(SchedType==ScheduleType.Blockout){
				command="SELECT COUNT(*) FROM schedule WHERE SchedType='"+POut.PInt((int)ScheduleType.Blockout)+"' "
					+"AND SchedDate='"+POut.PDate(SchedDate)+"'";
				DataTable table=dcon.GetTable(command);
				if(table.Rows[0][0].ToString()=="0"){
					Schedule sched=new Schedule();
					sched.SchedDate=SchedDate;
					sched.SchedType=ScheduleType.Blockout;
					sched.Status=SchedStatus.Closed;
					sched.Insert();
				}
			}
		}

	}

	/*=========================================================================================
		=================================== class Schedules ==========================================*/
///<summary></summary>
	public class Schedules{	

		///<summary>Used in the schedule setup window</summary>
		public static Schedule[] RefreshMonth(DateTime CurDate,ScheduleType schedType,int provNum){
			string command=
				//"SELECT * FROM schedule WHERE SchedDate > '"+POut.PDate(startDate.AddDays(-1))+"' "
				//+"AND SchedDate < '"+POut.PDate(stopDate.AddDays(1))+"' "
				"SELECT * FROM schedule WHERE MONTH(SchedDate)='"+CurDate.Month.ToString()
				+"' AND YEAR(SchedDate)='"+CurDate.Year.ToString()+"' "
				+"AND SchedType="+POut.PInt((int)schedType)
				+" AND ProvNum="+POut.PInt(provNum)
				+" ORDER BY starttime";
			return RefreshAndFill(command);
		}

		///<summary>Called every time the day is refreshed or changed in Appointments module.  Gets the data directly from the database.</summary>
		public static Schedule[] RefreshDay(DateTime thisDay){
			string command=
				"SELECT * FROM schedule WHERE SchedDate='"+POut.PDate(thisDay)+"'"
				+" ORDER BY starttime";
			return RefreshAndFill(command);
		}

		///<summary>Used in the check database integrity tool.</summary>
		public static Schedule[] RefreshAll(){
			string command="SELECT * FROM schedule";
			return RefreshAndFill(command);
		}

		private static Schedule[] RefreshAndFill(string command){
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			Schedule[] List=new Schedule[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Schedule();
				List[i].ScheduleNum    = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].SchedDate      = PIn.PDate  (table.Rows[i][1].ToString());
				List[i].StartTime      = PIn.PDateT (table.Rows[i][2].ToString());
				List[i].StopTime       = PIn.PDateT (table.Rows[i][3].ToString());
				List[i].SchedType      = (ScheduleType)PIn.PInt (table.Rows[i][4].ToString());
				List[i].ProvNum        = PIn.PInt   (table.Rows[i][5].ToString());
				List[i].BlockoutType   = PIn.PInt   (table.Rows[i][6].ToString());
				List[i].Note           = PIn.PString(table.Rows[i][7].ToString());
				List[i].Status         = (SchedStatus)PIn.PInt(table.Rows[i][8].ToString());
				List[i].Op             = PIn.PInt   (table.Rows[i][9].ToString());
			}
			return List;
		}

		///<summary>Supply a list of all Schedule for one day. Then, this filters out for one type.</summary>
		public static Schedule[] GetForType(Schedule[] ListDay,ScheduleType schedType,int provNum){
			ArrayList AL=new ArrayList();
			for(int i=0;i<ListDay.Length;i++){
				if(ListDay[i].SchedType==schedType && ListDay[i].ProvNum==provNum){
					AL.Add(ListDay[i]);
				}
			}
			Schedule[] retVal=new Schedule[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		///<summary>If a particular day already has some non-default schedule items, then this does nothing and returns false.  But if the day is all default, then it converts each default entry into an actual schedule entry and returns true.  The user would not notice this change, but now they can edit or add.</summary>
		public static bool ConvertFromDefault(DateTime forDate,ScheduleType schedType,int provNum){
			Schedule[] List=RefreshDay(forDate);
			Schedule[] ListForType=GetForType(List,schedType,provNum);
			if(ListForType.Length>0){
				return false;//do nothing, since it has already been converted from default.
				//it is also possible there will be no default entries to convert, but that's ok.
			}
			SchedDefault[] ListDefaults=SchedDefaults.GetForType(schedType,provNum);
			for(int i=0;i<ListDefaults.Length;i++){
				if(ListDefaults[i].DayOfWeek!=(int)forDate.DayOfWeek){
					continue;//if day of week doesn't match, then skip
				}
				Schedule SchedCur=new Schedule();
				SchedCur.Status=SchedStatus.Open;
				SchedCur.SchedDate=forDate;
				SchedCur.StartTime=ListDefaults[i].StartTime;
				SchedCur.StopTime=ListDefaults[i].StopTime; 
				SchedCur.SchedType=schedType;
				SchedCur.ProvNum=provNum;
				SchedCur.Op=ListDefaults[i].Op;
				SchedCur.BlockoutType=ListDefaults[i].BlockoutType;
				SchedCur.InsertOrUpdate(true);     
			}
			return true;
		}

		///<summary></summary>
		public static void SetAllDefault(DateTime forDate,ScheduleType schedType,int provNum){
			string command="DELETE from schedule WHERE "
				+"SchedDate='"    +POut.PDate (forDate)+"' "
				+"AND SchedType='"+POut.PInt((int)schedType)+"' "
				+"AND ProvNum='"  +POut.PInt(provNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		
	}

	

	

}













