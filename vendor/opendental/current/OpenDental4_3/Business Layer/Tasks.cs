using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the task table in the database.</summary>
	public class Task{
		///<summary>Primary key.</summary>
		public int TaskNum;
		///<summary>Foreign key to tasklist.TaskListNum.  If 0, then it will show in the trunk of a section.  </summary>
		public int TaskListNum;
		///<summary>Only used if this task is assigned to a dated category.  Children are NOT dated.  Only dated if they should show in the trunk for a date category.  They can also have a parent if they are in the main list as well.</summary>
		public DateTime DateTask;
		///<summary>Foreign key to patient.PatNum or appointment.AptNum. Only used when ObjectType is not 0.</summary>
		public int KeyNum;
		///<summary>The description of this task.  Might be very long.</summary>
		public string Descript;
		///<summary>True if the task has been completed. This could later be turned into an enumeration if more statuses are needed.</summary>
		public bool TaskStatus;
		///<summary>True if it is to show in the repeating section.  There should be no date.  All children and parents should also be set to IsRepeating=true.</summary>
		public bool IsRepeating;
		///<summary>See enum TaskDateType: None,Day,Week,Month.  If IsRepeating, then setting to None effectively disables the repeating feature.</summary>
		public TaskDateType DateType;
		///<summary>If this is derived from a repeating task, then this will hold the TaskNum of that task.  It helps automate the adding and deleting of tasks.  It might be deleted automatically if not are marked complete.</summary>
		public int FromNum;
		///<summary>See TaskObjectType enumeration.  0=none,1=Patient,2=Appointment.  More will be added later. If a type is selected, then the KeyNum will contain the primary key of the corresponding Patient or Appointment.  Does not really have anything to do with the ObjectType of the parent tasklist, although they tend to match.</summary>
		public TaskObjectType ObjectType;
		///<summary>The date and time that this task was added.  Used to sort the list by the order entered.</summary>
		public DateTime DateTimeEntry;
		
		///<summary></summary>
		public Task Copy(){
			Task t=new Task();
			t.TaskNum=TaskNum;
			t.TaskListNum=TaskListNum;
			t.DateTask=DateTask;
			t.KeyNum=KeyNum;
			t.Descript=Descript;
			t.TaskStatus=TaskStatus;
			t.IsRepeating=IsRepeating;
			t.DateType=DateType;
			t.FromNum=FromNum;
			t.ObjectType=ObjectType;
			t.DateTimeEntry=DateTimeEntry;
			return t;
		}

		///<summary></summary>
		private void Update(){
			string command= "UPDATE task SET " 
				+"TaskListNum = '"    +POut.PInt   (TaskListNum)+"'"
				+",DateTask = '"      +POut.PDate  (DateTask)+"'"
				+",KeyNum = '"        +POut.PInt   (KeyNum)+"'"
				+",Descript = '"      +POut.PString(Descript)+"'"
				+",TaskStatus = '"    +POut.PBool  (TaskStatus)+"'"
				+",IsRepeating = '"   +POut.PBool  (IsRepeating)+"'"
				+",DateType = '"      +POut.PInt   ((int)DateType)+"'"
				+",FromNum = '"       +POut.PInt   (FromNum)+"'"
				+",ObjectType = '"    +POut.PInt   ((int)ObjectType)+"'"
				+",DateTimeEntry = '" +POut.PDateT (DateTimeEntry)+"'"
				+" WHERE TaskNum = '"+POut.PInt(TaskNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		private void Insert(){
			if(Prefs.RandomKeys){
				TaskNum=MiscData.GetKey("task","TaskNum");
			}
			string command= "INSERT INTO task (";
			if(Prefs.RandomKeys){
				command+="TaskNum,";
			}
			command+="TaskListNum,DateTask,KeyNum,Descript,TaskStatus,"
				+"IsRepeating,DateType,FromNum,ObjectType,DateTimeEntry) VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(TaskNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (TaskListNum)+"', "
				+"'"+POut.PDate  (DateTask)+"', "
				+"'"+POut.PInt   (KeyNum)+"', "
				+"'"+POut.PString(Descript)+"', "
				+"'"+POut.PBool  (TaskStatus)+"', "
				+"'"+POut.PBool  (IsRepeating)+"', "
				+"'"+POut.PInt   ((int)DateType)+"', "
				+"'"+POut.PInt   (FromNum)+"', "
				+"'"+POut.PInt   ((int)ObjectType)+"', "
				+"'"+POut.PDateT (DateTimeEntry)+"')";
				//+"NOW())";//DateTimeEntry set to current server time
			DataConnection dcon=new DataConnection();
 			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				TaskNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void InsertOrUpdate(bool isNew){
			if(IsRepeating && DateTask.Year>1880){
				throw new Exception(Lan.g(this,"Task cannot be tagged repeating and also have a date."));
			}
			if(IsRepeating && TaskStatus){//and complete
				throw new Exception(Lan.g(this,"Task cannot be tagged repeating and also be marked complete."));
			}
			if(IsRepeating && TaskListNum!=0 && DateType!=TaskDateType.None){//In repeating, children not allowed to repeat.
				throw new Exception(Lan.g(this,"In repeating tasks, only the main parents can have a task status."));
			}
			if(isNew){
				Insert();
			}
			else{
				Update();
			}
		}

		///<summary>Deleting a task never causes a problem, so no dependencies are checked.</summary>
		public void Delete(){
			string command= "DELETE from task WHERE TaskNum = '"
				+POut.PInt(TaskNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}


	}

	/*=========================================================================================
		=================================== class Tasks ==========================================*/

	///<summary></summary>
	public class Tasks{
		///<summary></summary>
		public static ArrayList LastOpenList;
		///<summary></summary>
		public static int LastOpenGroup;
		///<summary></summary>
		public static DateTime LastOpenDate;
			
		///<summary>Gets all tasks for a given taskList.  But the 5 trunks don't have parents: For main trunk use date.Min and TaskListNum=0.  For Repeating trunk use date.Min isRepeating and TaskListNum=0.  For the 3 dated trunks, use a date and a dateType.  Date and TaskListNum are mutually exclusive.  Also used to get all repeating tasks for one dateType without any heirarchy: supply listNum=-1.</summary>
		public static Task[] Refresh(int listNum,DateTime date,TaskDateType dateType,bool isRepeating){
			DateTime dateFrom=DateTime.MinValue;
			DateTime dateTo=DateTime.MaxValue;
			string where="";
			if(date.Year>1880){
				//date supplied always indicates one of 3 dated trunks.
				//the value of listNum is completely ignored
				if(dateType==TaskDateType.Day){
					dateFrom=date;
					dateTo=date;
				}
				else if(dateType==TaskDateType.Week){
					dateFrom=date.AddDays(-(int)date.DayOfWeek);
					dateTo=dateFrom.AddDays(6);
				}
				else if(dateType==TaskDateType.Month){
					dateFrom=new DateTime(date.Year,date.Month,1);
					dateTo=dateFrom.AddMonths(1).AddDays(-1);
				}
				where="DateTask >= '"+POut.PDate(dateFrom)
					+"' AND DateTask <= '"+POut.PDate(dateTo)+"' "
					+"AND DateType="+POut.PInt((int)dateType)+" ";
			}
			else{//no date supplied.
				if(listNum==0){//main trunk or repeating trunk
					where="TaskListNum="+POut.PInt(listNum)
						+" AND DateTask < '1880-01-01'"
						+" AND IsRepeating="+POut.PBool(isRepeating)+" ";
				}
				else if(listNum==-1 && isRepeating){//all repeating items with no heirarchy
					where="IsRepeating=1 "
						+"AND DateType="+POut.PInt((int)dateType)+" ";
				}
				else{//any child
					where="TaskListNum="+POut.PInt(listNum)+" ";
						//+" AND IsRepeating="+POut.PBool(isRepeating)+" ";
				}
			}
			string command=
				"SELECT * FROM task "
				+"WHERE "+where
				+"ORDER BY DateTimeEntry";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			Task[] List=new Task[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Task();
				List[i].TaskNum        = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].TaskListNum    = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].DateTask       = PIn.PDate  (table.Rows[i][2].ToString());
				List[i].KeyNum         = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].Descript       = PIn.PString(table.Rows[i][4].ToString());
				List[i].TaskStatus     = PIn.PBool  (table.Rows[i][5].ToString());
				List[i].IsRepeating    = PIn.PBool  (table.Rows[i][6].ToString());
				List[i].DateType       = (TaskDateType)PIn.PInt(table.Rows[i][7].ToString());
				List[i].FromNum        = PIn.PInt   (table.Rows[i][8].ToString());
				List[i].ObjectType     = (TaskObjectType)PIn.PInt(table.Rows[i][9].ToString());
				List[i].DateTimeEntry  = PIn.PDateT (table.Rows[i][10].ToString());
			}
			return List;
		}

	
	}

	

	


}




















