using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	/// <summary>Tracks all forms of communications with patients, including emails, phonecalls, postcards, etc.</summary>
	public class Commlog{
		///<summary>Primary key.</summary>
		public int CommlogNum;
		///<summary>FK to patient.PatNum.</summary>
		public int PatNum;
		///<summary>Date and time of entry</summary>
		public DateTime CommDateTime;
		///<summary>Enum:CommItemType .</summary>
		public CommItemType CommType;
		///<summary>Note for this commlog entry.</summary>
		public string Note;
		///<summary>Enum:CommItemMode Phone, email, etc.</summary>
		public CommItemMode Mode;
		///<summary>Enum:CommSentOrReceived Neither=0,Sent=1,Received=2.</summary>
		public CommSentOrReceived SentOrReceived;
		///<summary>FK to emailmessage.EmailMessageNum, if there is an associated email. Otherwise 0.</summary>
		public int EmailMessageNum;

		///<summary></summary>
		public Commlog Copy(){
			Commlog c=new Commlog();
			c.CommlogNum=CommlogNum;
			c.PatNum=PatNum;
			c.CommDateTime=CommDateTime;
			c.CommType=CommType;
			c.Note=Note;
			c.Mode=Mode;
			c.SentOrReceived=SentOrReceived;
			c.EmailMessageNum=EmailMessageNum;
			return c;
		}

		///<summary></summary>
		public void Insert(){
			if(Prefs.RandomKeys) {
				CommlogNum=MiscData.GetKey("commlog","CommlogNum");
			}
			string command="INSERT INTO commlog (";
			if(Prefs.RandomKeys) {
				command+="CommlogNum,";
			}
			command+="PatNum,CommDateTime,CommType,Note,Mode,SentOrReceived,EmailMessageNum) VALUES(";
			if(Prefs.RandomKeys) {
				command+="'"+POut.PInt(CommlogNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (PatNum)+"', "
				+"'"+POut.PDateT (CommDateTime)+"', "
				+"'"+POut.PInt   ((int)CommType)+"', "
				+"'"+POut.PString(Note)+"', "
				+"'"+POut.PInt   ((int)Mode)+"', "
				+"'"+POut.PInt   ((int)SentOrReceived)+"', "
				+"'"+POut.PInt   (EmailMessageNum)+"')";
			DataConnection dcon=new DataConnection();
			if(Prefs.RandomKeys) {
				dcon.NonQ(command);
			}
			else {
				dcon.NonQ(command,true);
				CommlogNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void Update() {
			string command= "UPDATE commlog SET "
				+"PatNum = '"         +POut.PInt   (PatNum)+"', "
				+"CommDateTime= '"    +POut.PDateT (CommDateTime)+"', "
				+"CommType = '"       +POut.PInt   ((int)CommType)+"', "
				+"Mode = '"           +POut.PInt   ((int)Mode)+"', "
				+"SentOrReceived = '" +POut.PInt   ((int)SentOrReceived)+"', "
				+"EmailMessageNum = '"+POut.PInt   ((int)EmailMessageNum)+"', "
				+"Note = '"           +POut.PString(Note)+"' "
				+"WHERE commlognum = '"+POut.PInt(CommlogNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Delete() {
			string command= "DELETE FROM commlog WHERE CommLogNum = '"+CommlogNum.ToString()+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}
		
	}

	/*=========================================================================================
	=================================== class Commlogs ==========================================*/

	///<summary></summary>
	public class Commlogs{

		///<summary>Gets all items for the current patient ordered by date.</summary>
		public static Commlog[] Refresh(int patNum){
			string command=
				"SELECT * FROM commlog"
				+" WHERE PatNum = '"+patNum+"'"
				+" ORDER BY CommDateTime";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			Commlog[] List=new Commlog[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new Commlog();
				List[i].CommlogNum     = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum         = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].CommDateTime   = PIn.PDate  (table.Rows[i][2].ToString());
				List[i].CommType       = (CommItemType)PIn.PInt(table.Rows[i][3].ToString());
				List[i].Note           = PIn.PString(table.Rows[i][4].ToString());
				List[i].Mode           = (CommItemMode)PIn.PInt   (table.Rows[i][5].ToString());
				List[i].SentOrReceived = (CommSentOrReceived)PIn.PInt   (table.Rows[i][6].ToString());
				List[i].EmailMessageNum= PIn.PInt   (table.Rows[i][7].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static int UndoStatements(DateTime date){
			string command="DELETE FROM commlog WHERE CommDateTime LIKE '"+POut.PDate(date)+"%' "
				+"AND CommType=1";
			DataConnection dcon=new DataConnection();
			int rowsAffected=dcon.NonQ(command);
			return rowsAffected;
		}

		///<summary>Used when printing recall cards to make a commlog entry for everyone at once.</summary>
		public static void InsertForRecallPostcard(int patNum){
			string command="SELECT COUNT(*) FROM  commlog WHERE DATE(CommDateTime) = CURDATE() AND PatNum="+POut.PInt(patNum)
				+" AND CommType=5 AND Mode=2 AND SentOrReceived=1";
			DataConnection dcon=new DataConnection();
			if(dcon.GetCount(command)!="0"){
				return;
			}
			Commlog com=new Commlog();
			com.PatNum=patNum;
			com.CommDateTime=DateTime.Now;
			com.CommType=CommItemType.Recall;
			com.Mode=CommItemMode.Mail;
			com.SentOrReceived=CommSentOrReceived.Sent;
			com.Note=Lan.g("FormRecallList","Sent recall postcard");
			com.Insert();
		}

	}

	




}

















