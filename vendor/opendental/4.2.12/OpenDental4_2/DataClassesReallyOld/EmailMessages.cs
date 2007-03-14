using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the emailmessage table in the database.</summary>
	public struct EmailMessage{
		///<summary>Primary key.</summary>
		public int EmailMessageNum;
		///<summary>Foreign key to patient.PatNum</summary>
		public int PatNum;
		///<summary>Single valid email address. Bcc field will be added later.</summary>
		public string ToAddress;
		///<summary>Valid email address.</summary>
		public string FromAddress;
    ///<summary>Subject line.</summary>
		public string Subject;
		///<summary>Body of the email</summary>
		public string BodyText;
		///<summary>Date and time the message was sent. Automated field.</summary>
		public DateTime MsgDateTime;
	}
	
	/*=========================================================================================
		=================================== class EmailMessages ===========================================*/
	///<summary>An email message is always attached to a patient.</summary>
	public class EmailMessages:DataClass{
		//<summary>List of</summary>
		//public static EmailTemplate[] List;
		///<summary>There is no List for patient, just individual emails loaded as needed.</summary>
		public static EmailMessage Cur;

		///<summary>Loads the specified emailMessage into cur.</summary>
		public static void Refresh(int msgNum){
			//HList=new Hashtable();
			cmd.CommandText = 
				"SELECT * from emailmessage "
				+"WHERE EmailMessageNum = '"+msgNum.ToString()+"'";
			FillTable();
			Cur=new EmailMessage();
			if(table.Rows.Count==0)
				return;
			//for(int i=0;i<table.Rows.Count;i++){
			Cur.EmailMessageNum=PIn.PInt   (table.Rows[0][0].ToString());
			Cur.PatNum         =PIn.PInt   (table.Rows[0][1].ToString());
			Cur.ToAddress      =PIn.PString(table.Rows[0][2].ToString());
			Cur.FromAddress    =PIn.PString(table.Rows[0][3].ToString());
			Cur.Subject        =PIn.PString(table.Rows[0][4].ToString());
			Cur.BodyText       =PIn.PString(table.Rows[0][5].ToString());
			Cur.MsgDateTime    =PIn.PDateT (table.Rows[0][6].ToString());
			//}
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText="UPDATE emailmessage SET "
				+ "PatNum = '"      +POut.PInt   (Cur.PatNum)+"' "
				+ ",ToAddress = '"  +POut.PString(Cur.ToAddress)+"' "
				+ ",FromAddress = '"+POut.PString(Cur.FromAddress)+"' "
				+ ",Subject = '"    +POut.PString(Cur.Subject)+"' "
				+ ",BodyText = '"   +POut.PString(Cur.BodyText)+"' "
				+ ",MsgDateTime = '"+POut.PDateT (Cur.MsgDateTime)+"' "
				+"WHERE EmailMessageNum = '"+POut.PInt(Cur.EmailMessageNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ();
		}

		///<summary></summary>
		public static void InsertCur(){
			if(Prefs.RandomKeys){
				Cur.EmailMessageNum=MiscData.GetKey("emailmessage","EmailMessageNum");
			}
			cmd.CommandText="INSERT INTO emailmessage (";
			if(Prefs.RandomKeys){
				cmd.CommandText+="EmailMessageNum,";
			}
			cmd.CommandText+="PatNum,ToAddress,FromAddress,Subject,BodyText,"
				+"MsgDateTime) VALUES(";
			if(Prefs.RandomKeys){
				cmd.CommandText+="'"+POut.PInt(Cur.EmailMessageNum)+"', ";
			}
			cmd.CommandText+=
				 "'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PString(Cur.ToAddress)+"', "
				+"'"+POut.PString(Cur.FromAddress)+"', "
				+"'"+POut.PString(Cur.Subject)+"', "
				+"'"+POut.PString(Cur.BodyText)+"', "
				+"'"+POut.PDateT (Cur.MsgDateTime)+"')";
			if(Prefs.RandomKeys){
				NonQ();
			}
			else{
 				NonQ(true);
				Cur.EmailMessageNum=InsertID;
			}
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText="DELETE from emailmessage WHERE EmailMessageNum = '"
				+Cur.EmailMessageNum.ToString()+"'";
			NonQ();
		}

		
	}

	
	

}













