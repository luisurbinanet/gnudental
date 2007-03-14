using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Used on employee timecards to make adjustments.</summary>
	public class TimeAdjust{
		///<summary>Primary key.</summary>
		public int TimeAdjustNum;
		///<summary>FK to employee.EmployeeNum</summary>
		public int EmployeeNum;
		///<summary>The date and time that this entry will show on timecard.</summary>
		public DateTime TimeEntry;
		///<summary>The number of regular hours to adjust timecard by.  Can be + or -.</summary>
		public TimeSpan RegHours;
		///<summary>Overtime hours. Usually +.  Usually combined with a - adj to RegHours.</summary>
		public TimeSpan OTimeHours;
		///<summary>.</summary>
		public string Note;
		
		///<summary></summary>
		public TimeAdjust Copy() {
			TimeAdjust t=new TimeAdjust();
			t.TimeAdjustNum=TimeAdjustNum;
			t.EmployeeNum=EmployeeNum;
			t.TimeEntry=TimeEntry;
			t.RegHours=RegHours;
			t.OTimeHours=OTimeHours;
			t.Note=Note;
			return t;
		}

		///<summary></summary>
		public void Insert() {
			if(Prefs.RandomKeys) {
				TimeAdjustNum=MiscData.GetKey("timeadjust","TimeAdjustNum");
			}
			string command="INSERT INTO timeadjust (";
			if(Prefs.RandomKeys) {
				command+="TimeAdjustNum,";
			}
			command+="EmployeeNum,TimeEntry,RegHours,OTimeHours,Note) VALUES(";
			if(Prefs.RandomKeys) {
				command+="'"+POut.PInt(TimeAdjustNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (EmployeeNum)+"', "
				+"'"+POut.PDateT (TimeEntry)+"', "
				+"'"+POut.PDouble(RegHours.TotalHours)+"', "
				+"'"+POut.PDouble(OTimeHours.TotalHours)+"', "
				+"'"+POut.PString(Note)+"')";
			DataConnection dcon=new DataConnection();
			if(Prefs.RandomKeys) {
				dcon.NonQ(command);
			}
			else {
				dcon.NonQ(command,true);
				TimeAdjustNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void Update() {
			string command= "UPDATE timeadjust SET "
				+"EmployeeNum = '"+POut.PInt   (EmployeeNum)+"' "
				+",TimeEntry = '" +POut.PDateT (TimeEntry)+"' "
				+",RegHours = '"  +POut.PDouble(RegHours.TotalHours)+"' "
				+",OTimeHours = '"+POut.PDouble(OTimeHours.TotalHours)+"' "
				+",Note = '"      +POut.PString(Note)+"' "
				+"WHERE TimeAdjustNum = '"+POut.PInt(TimeAdjustNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Delete() {
			string command= "DELETE FROM timeadjust WHERE TimeAdjustNum = "+POut.PInt(TimeAdjustNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

	}

	/*=========================================================================================
		=================================== class TimeAdjusts ==========================================*/

	///<summary></summary>
	public class TimeAdjusts{

		///<summary></summary>
		public static TimeAdjust[] Refresh(int empNum,DateTime fromDate,DateTime toDate){
			string command=
				"SELECT * from timeadjust WHERE"
				+" EmployeeNum = '"+POut.PInt(empNum)+"'"
				+" && TimeEntry >= '"+POut.PDate(fromDate)+"'"
				//adding a day takes it to midnight of the specified toDate
				+" && TimeEntry <= '"+POut.PDate(toDate.AddDays(1))+"'";
			command+=" ORDER BY TimeEntry";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			TimeAdjust[] List=new TimeAdjust[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new TimeAdjust();
				List[i].TimeAdjustNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].EmployeeNum   = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].TimeEntry     = PIn.PDateT (table.Rows[i][2].ToString());
				List[i].RegHours      =TimeSpan.FromHours(PIn.PDouble(table.Rows[i][3].ToString()));
				List[i].OTimeHours    =TimeSpan.FromHours(PIn.PDouble(table.Rows[i][4].ToString()));
				List[i].Note          = PIn.PString(table.Rows[i][5].ToString());
			}
			return List;
		}


		




	}

	
}




