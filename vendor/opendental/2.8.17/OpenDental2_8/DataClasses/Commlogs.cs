using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	/// <summary>Corresponds to the commlog table in the database.</summary>
	/// <remarks>Will eventually track all communications including emails, phonecalls, letters, etc.
	/// There is no user field yet to track who made the entry because we need to add a user table first to get a unique id.</remarks>
	public struct Commlog{
		///<summary>Primary key.</summary>
		public int CommlogNum;
		///<summary>Foreign key to patient.PatNum</summary>
		public int PatNum;
		///<summary>Date of entry</summary>
		public DateTime CommDate;
		///<summary>See the CommItemType enumeration.</summary>
		public CommItemType CommType;
		///<summary>Note for this commlog entry.</summary>
		public string Note;
	}

	/*=========================================================================================
	=================================== class Commlogs ==========================================*/

	///<summary></summary>
	public class Commlogs:DataClass{
		///<summary></summary>
		public static Commlog[] List;//for one patient
		///<summary></summary>
		public static Commlog Cur;
		//public static Hashtable HList;

		///<summary>Gets all items for the current patient ordered by date.</summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * FROM commlog"
				+" WHERE patnum = '"+Patients.Cur.PatNum+"'"
				+" ORDER BY commdate";
			FillTable();
			List=new Commlog[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i].CommlogNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum    = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].CommDate  = PIn.PDate  (table.Rows[i][2].ToString());
				List[i].CommType  = (CommItemType)PIn.PInt(table.Rows[i][3].ToString());
				List[i].Note      = PIn.PString(table.Rows[i][4].ToString());
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO commlog (patnum"
				+",commdate,commtype,note) VALUES("
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PDate  (Cur.CommDate)+"', "
				+"'"+POut.PInt   ((int)Cur.CommType)+"', "
				+"'"+POut.PString(Cur.Note)+"')";
			NonQ(false);
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE commlog SET "
				+"patnum = '"   +POut.PInt   (Cur.PatNum)+"', "
				+"commdate= '"  +POut.PDate  (Cur.CommDate)+"', "
				+"commtype = '" +POut.PInt   ((int)Cur.CommType)+"', "
				+"note = '"     +POut.PString(Cur.Note)+"' "
				+"WHERE commlognum = '"+POut.PInt(Cur.CommlogNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM commlog WHERE commlognum = '"+Cur.CommlogNum.ToString()+"'";
			NonQ(false);
		}

	}

	




}









