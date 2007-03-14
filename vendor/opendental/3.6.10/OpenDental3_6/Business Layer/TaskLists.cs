using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the tasklist table in the database.</summary>
	public class TaskList{
		///<summary>Primary key.</summary>
		public int TaskListNum;
		///<summary>The description of this tasklist.  Might be very long, but not usually.</summary>
		public string Descript;
		///<summary>The parent task list to which this task list is assigned.  If zero, then this task list is on the main trunk of one of the sections.</summary>
		public int Parent;
		///<summary>Optional. Set to 0001-01-01 for no date.  If a date is assigned, then this list will also be available from the date section.</summary>
		public DateTime DateTL;
		///<summary>True if it is to show in the repeating section.  There should be no date.  All children should also be set to IsRepeating=true.</summary>
		public bool IsRepeating;
		///<summary>See enum TaskDateType: None,Day,Week,Month.  If IsRepeating, then setting to None effectively disables the repeating feature.</summary>
		public TaskDateType DateType;
		///<summary>If this is derived from a repeating list, then this will hold the ListNum of that list.  It helps automate the adding and deleting of lists.  It might be deleted automatically if no tasks are marked complete.</summary>
		public int FromNum;
		///<summary>See TaskObjectType enumeration.  0=none,1=Patient,2=Appointment.  More will be added later. If a type is selected, then this list will be visible in the appropriate places for attaching the correct type of object.  The type is not copied to a task when created.  Tasks in this list do not have to be of the same type.  You can only attach an object to a task, not a tasklist.</summary>
		public TaskObjectType ObjectType;

		///<summary></summary>
		public TaskList Copy(){
			TaskList t=new TaskList();
			t.TaskListNum=TaskListNum;
			t.Descript=Descript;
			t.Parent=Parent;
			t.DateTL=DateTL;
			t.IsRepeating=IsRepeating;
			t.DateType=DateType;
			t.FromNum=FromNum;
			t.ObjectType=ObjectType;
			return t;
		}

		///<summary></summary>
		private void Update(){
			string command= "UPDATE tasklist SET " 
				+"Descript = '"       +POut.PString(Descript)+"'"
				+",Parent = '"        +POut.PInt   (Parent)+"'"
				+",DateTL = '"        +POut.PDate  (DateTL)+"'"
				+",IsRepeating = '"   +POut.PBool  (IsRepeating)+"'"
				+",DateType = '"      +POut.PInt   ((int)DateType)+"'"
				+",FromNum = '"       +POut.PInt   (FromNum)+"'"
				+",ObjectType = '"    +POut.PInt   ((int)ObjectType)+"'"
				+" WHERE TaskListNum = '" +POut.PInt (TaskListNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		private void Insert(){
			if(Prefs.RandomKeys){
				TaskListNum=MiscData.GetKey("tasklist","TaskListNum");
			}
			string command= "INSERT INTO tasklist (";
			if(Prefs.RandomKeys){
				command+="TaskListNum,";
			}
			command+="Descript,Parent,DateTL,IsRepeating,DateType,"
				+"FromNum,ObjectType) VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(TaskListNum)+"', ";
			}
			command+=
				 "'"+POut.PString(Descript)+"', "
				+"'"+POut.PInt   (Parent)+"', "
				+"'"+POut.PDate  (DateTL)+"', "
				+"'"+POut.PBool  (IsRepeating)+"', "
				+"'"+POut.PInt   ((int)DateType)+"', "
				+"'"+POut.PInt   (FromNum)+"', "
				+"'"+POut.PInt   ((int)ObjectType)+"')";
			DataConnection dcon=new DataConnection();
 			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				TaskListNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void InsertOrUpdate(bool isNew){
			//check for duplicate trunk?
			if(IsRepeating && DateTL.Year>1880){
				throw new Exception(Lan.g(this,"TaskList cannot be tagged repeating and also have a date."));
			}
			if(Parent==0 && DateTL.Year>1880 && DateType==TaskDateType.None){//it would not show anywhere, so it would be 'lost'
				throw new Exception(Lan.g(this,"A TaskList with a date must also have a type selected."));
			}
			if(IsRepeating && Parent!=0 && DateType!=TaskDateType.None){//In repeating, children not allowed to repeat.
				throw new Exception(Lan.g(this,"In repeating tasklists, only the main parents can have a task status."));
			}
			if(isNew){
				Insert();
			}
			else{
				Update();
			}
		}

		///<summary>Throws exception if any child tasklists or tasks.</summary>
		public void Delete(){
			string command="SELECT COUNT(*) FROM tasklist WHERE Parent="+POut.PInt(TaskListNum);
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(table.Rows[0][0].ToString()!="0"){
				throw new Exception(Lan.g(this,"Not allowed to delete task list because it still has child lists attached."));
			}
			command="SELECT COUNT(*) FROM task WHERE TaskListNum="+POut.PInt(TaskListNum);
			table=dcon.GetTable(command);
			if(table.Rows[0][0].ToString()!="0"){
				throw new Exception(Lan.g(this,"Not allowed to delete task list because it still has child tasks attached."));
			}
			command= "DELETE from tasklist WHERE TaskListNum = '"
				+POut.PInt(TaskListNum)+"'";
 			dcon.NonQ(command);
		}


	}

	/*=========================================================================================
		=================================== class TaskLists ==========================================*/

	///<summary></summary>
	public class TaskLists{
	
		///<summary>Gets all tasklists for a given parent.  But the 5 trunks don't have parents: For main trunk use date.Min and Parent=0.  For Repeating trunk use date.Min isRepeating and Parent=0.  For the 3 dated trunks, use a date and a dateType.  Date and parent are mutually exclusive.  Also used to get all repeating lists for one dateType without any heirarchy: supply parent=-1.</summary>
		public static TaskList[] Refresh(int parent,DateTime date,TaskDateType dateType,bool isRepeating){
			DateTime dateFrom=DateTime.MinValue;
			DateTime dateTo=DateTime.MaxValue;
			string where="";
			if(date.Year>1880){
				//date supplied always indicates one of 3 dated trunks.
				//the value of parent is completely ignored
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
				where="DateTL >= '"+POut.PDate(dateFrom)
					+"' AND DateTL <= '"+POut.PDate(dateTo)+"' "
					+"AND DateType="+POut.PInt((int)dateType)+" ";
			}
			else{//no date supplied.
				if(parent==0){//main trunk or repeating trunk
					where="Parent="+POut.PInt(parent)
						+" AND DateTL < '1880-01-01'"
						+" AND IsRepeating="+POut.PBool(isRepeating)+" ";
				}
				else if(parent==-1 && isRepeating){//all repeating items with no heirarchy
					where="IsRepeating=1 "
						+"AND DateType="+POut.PInt((int)dateType)+" ";
				}
				else{//any child
					where="Parent="+POut.PInt(parent)+" ";
						//+" AND IsRepeating="+POut.PBool(isRepeating)+" ";
				}
			}
			string command=
				"SELECT * FROM tasklist "
				+"WHERE "
				+where
				+"ORDER BY Descript";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			TaskList[] List=new TaskList[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new TaskList();
				List[i].TaskListNum    = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Descript       = PIn.PString(table.Rows[i][1].ToString());
				List[i].Parent         = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].DateTL         = PIn.PDate  (table.Rows[i][3].ToString());
				List[i].IsRepeating    = PIn.PBool  (table.Rows[i][4].ToString());
				List[i].DateType       = (TaskDateType)PIn.PInt   (table.Rows[i][5].ToString());
				List[i].FromNum        = PIn.PInt   (table.Rows[i][6].ToString());
				List[i].ObjectType     = (TaskObjectType)PIn.PInt (table.Rows[i][7].ToString());
			}
			return List;
		}

		/// <summary>Gets all task lists with the give object type.  Used in TaskListSelect when assigning an object to a task list.</summary>
		public static TaskList[] GetForObjectType(TaskObjectType oType){
			string command=
				"SELECT * FROM tasklist "
				+"WHERE ObjectType="+POut.PInt((int)oType)
				+" ORDER BY Descript";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			TaskList[] List=new TaskList[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new TaskList();
				List[i].TaskListNum    = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Descript       = PIn.PString(table.Rows[i][1].ToString());
				List[i].Parent         = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].DateTL         = PIn.PDate  (table.Rows[i][3].ToString());
				List[i].IsRepeating    = PIn.PBool  (table.Rows[i][4].ToString());
				List[i].DateType       = (TaskDateType)PIn.PInt   (table.Rows[i][5].ToString());
				List[i].FromNum        = PIn.PInt   (table.Rows[i][6].ToString());
				List[i].ObjectType     = (TaskObjectType)PIn.PInt (table.Rows[i][7].ToString());
			}
			return List;
		}

		

	
	}

	

	


}













