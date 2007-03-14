using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the scheddefault table in the database.</summary>
	public struct SchedDefault{
		///<summary>Primary key.</summary>
		public int SchedDefaultNum;
		///<summary>Sun=0, Mon=1, etc.</summary>
		public int DayOfWeek;
		///<summary>Start time for this timeblock.</summary>
		public DateTime StartTime;
		///<summary>Stop time for this timeblock.</summary>
		public DateTime StopTime;
		///<summary>See the ScheduleType enumeration.</summary>
		public ScheduleType SchedType;
		///<summary>Foreign key to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>Not in use yet. Will be Foreign key to definition.DefNum.</summary>
		public int BlockoutType;
	}

	/*=========================================================================================
		=================================== class SchedDefaults ==========================================*/

	///<summary></summary>
	public class SchedDefaults:DataClass{
		///<summary></summary>
		public static SchedDefault[] List;
		///<summary></summary>
		public static SchedDefault Cur;
	
		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from scheddefault";
			FillTable();
			List=new SchedDefault[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].SchedDefaultNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].DayOfWeek      = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].StartTime      = PIn.PDateT (table.Rows[i][2].ToString());
				List[i].StopTime       = PIn.PDateT (table.Rows[i][3].ToString());
			}
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE scheddefault SET " 
				+ "dayofweek = '"  +POut.PInt   (Cur.DayOfWeek)+"'"
				+ ",starttime = '" +POut.PDateT (Cur.StartTime)+"'"
				+ ",stoptime = '"  +POut.PDateT (Cur.StopTime)+"'"
				+" WHERE SchedDefaultNum = '" +POut.PInt (Cur.SchedDefaultNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO scheddefault (dayofweek,starttime,stoptime"
				+") VALUES("
				+"'"+POut.PInt   (Cur.DayOfWeek)+"', "
				+"'"+POut.PDateT (Cur.StartTime)+"', "
				+"'"+POut.PDateT (Cur.StopTime)+"')";
			NonQ(true);
			Cur.SchedDefaultNum=InsertID;
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from scheddefault WHERE scheddefaultnum = '"+POut.PInt(Cur.SchedDefaultNum)+"'";
			NonQ(false);
		}
	}

	

	


}













