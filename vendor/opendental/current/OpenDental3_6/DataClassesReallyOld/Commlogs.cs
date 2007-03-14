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
		public DateTime CommDateTime;
		///<summary>See the CommItemType enumeration.</summary>
		public CommItemType CommType;
		///<summary>Note for this commlog entry.</summary>
		public string Note;
		///<summary>eg email or phone.  See the CommItemMode enum.</summary>
		public CommItemMode Mode;
		///<summary>Neither=0,Sent=1,Received=2.</summary>
		public CommSentOrReceived SentOrReceived;
		///<summary>Foreign key to emailmessage.EmailMessageNum, if there is an associated email. Otherwise 0.</summary>
		public int EmailMessageNum;
	}

	/*=========================================================================================
	=================================== class Commlogs ==========================================*/

	///<summary></summary>
	public class Commlogs:DataClass{
		///<summary>All commlog items for one patient, ordered by date.</summary>
		public static Commlog[] List;
		///<summary></summary>
		public static Commlog Cur;
		//public static Hashtable HList;

		///<summary>Gets all items for the current patient ordered by date.</summary>
		public static void Refresh(int patNum){
			cmd.CommandText =
				"SELECT * FROM commlog"
				+" WHERE PatNum = '"+patNum+"'"
				+" ORDER BY CommDateTime";
			FillTable();
			List=new Commlog[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i].CommlogNum     = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum         = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].CommDateTime   = PIn.PDate  (table.Rows[i][2].ToString());
				List[i].CommType       = (CommItemType)PIn.PInt(table.Rows[i][3].ToString());
				List[i].Note           = PIn.PString(table.Rows[i][4].ToString());
				List[i].Mode           = (CommItemMode)PIn.PInt   (table.Rows[i][5].ToString());
				List[i].SentOrReceived = (CommSentOrReceived)PIn.PInt   (table.Rows[i][6].ToString());
				List[i].EmailMessageNum= PIn.PInt   (table.Rows[i][7].ToString());
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			if(Prefs.RandomKeys){
				Cur.CommlogNum=MiscData.GetKey("commlog","CommlogNum");
			}
			cmd.CommandText="INSERT INTO commlog (";
			if(Prefs.RandomKeys){
				cmd.CommandText+="CommlogNum,";
			}
			cmd.CommandText+="PatNum"
				+",CommDateTime,CommType,Note,Mode,SentOrReceived,EmailMessageNum) VALUES(";
			if(Prefs.RandomKeys){
				cmd.CommandText+="'"+POut.PInt(Cur.CommlogNum)+"', ";
			}
			cmd.CommandText+=
				 "'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PDateT (Cur.CommDateTime)+"', "
				+"'"+POut.PInt   ((int)Cur.CommType)+"', "
				+"'"+POut.PString(Cur.Note)+"', "
				+"'"+POut.PInt   ((int)Cur.Mode)+"', "
				+"'"+POut.PInt   ((int)Cur.SentOrReceived)+"', "
				+"'"+POut.PInt   (Cur.EmailMessageNum)+"')";
			if(Prefs.RandomKeys){
				NonQ();
			}
			else{
 				NonQ(true);
				Cur.CommlogNum=InsertID;
			}
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE commlog SET "
				+"PatNum = '"         +POut.PInt   (Cur.PatNum)+"', "
				+"CommDateTime= '"    +POut.PDateT (Cur.CommDateTime)+"', "
				+"CommType = '"       +POut.PInt   ((int)Cur.CommType)+"', "
				+"Mode = '"           +POut.PInt   ((int)Cur.Mode)+"', "
				+"SentOrReceived = '" +POut.PInt   ((int)Cur.SentOrReceived)+"', "
				+"EmailMessageNum = '"+POut.PInt   ((int)Cur.EmailMessageNum)+"', "
				+"Note = '"           +POut.PString(Cur.Note)+"' "
				+"WHERE commlognum = '"+POut.PInt(Cur.CommlogNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ();
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM commlog WHERE CommLogNum = '"+Cur.CommlogNum.ToString()+"'";
			NonQ();
		}

	}

	




}

















