using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Stores an ongoing record of database activity for security purposes.  User not allowed to edit.</summary>
	public class SecurityLog{
		///<summary>Primary key.</summary>
		public int SecurityLogNum;
		///<summary>Enum:Permissions</summary>
		public Permissions PermType;
		///<summary>FK to user.UserNum</summary>
		public int UserNum;
		///<summary>The date and time of the entry.  It's value is set when inserting and can never change.  Even if a user changes the date on their ocmputer, this remains accurate because it uses server time.</summary>
		public DateTime LogDateTime;
		///<summary>The description of exactly what was done. Varies by permission type.</summary>
		public string LogText;
		///<summary>FK to patient.PatNum.  Can be 0 if not applicable.</summary>
		public int PatNum;

		///<summary></summary>
		public void Insert(){
			if(Prefs.RandomKeys){
				SecurityLogNum=MiscData.GetKey("securitylog","SecurityLogNum");
			}
			string command= "INSERT INTO securitylog (";
			if(Prefs.RandomKeys){
				command+="SecurityLogNum,";
			}
			command+="PermType,UserNum,LogDateTime,LogText,PatNum) VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(SecurityLogNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   ((int)PermType)+"', "
				+"'"+POut.PInt   (UserNum)+"', "
				+"NOW(), "//LogDateTime set to current server time
				+"'"+POut.PString(LogText)+"', "
				+"'"+POut.PInt   (PatNum)+"')";
			DataConnection dcon=new DataConnection();
 			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				SecurityLogNum=dcon.InsertID;
			}
		}

		//there are no methods for deleting or changing log entries because that will never be allowed.

		

	}

	/*=========================================================================================
	=================================== class SecurityLogs==========================================*/
  ///<summary></summary>
	public class SecurityLogs{

		///<summary>Used when viewing securityLog from the security admin window.  PermTypes can be length 0 to get all types.</summary>
		public static SecurityLog[] Refresh(DateTime dateFrom,DateTime dateTo,Permissions permType,int patNum,
			int userNum)
		{
			string command="SELECT * FROM securitylog "
				+"WHERE LogDateTime >= '"+POut.PDate(dateFrom)+"' "
				+"AND LogDateTime <= '"+POut.PDate(dateTo.AddDays(1))+"'";
			if(patNum !=0){
				command+=" AND PatNum= '"+POut.PInt(patNum)+"'";
			}
			if(permType!=Permissions.None){
				command+=" AND PermType="+POut.PInt((int)permType);
			}
			if(userNum!=0){
				command+=" AND UserNum="+POut.PInt(userNum);
			}
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			SecurityLog[] List=new SecurityLog[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new SecurityLog();
				List[i].SecurityLogNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PermType      = (Permissions)PIn.PInt(table.Rows[i][1].ToString());
				List[i].UserNum       = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].LogDateTime   = PIn.PDateT (table.Rows[i][3].ToString());	
				List[i].LogText       = PIn.PString(table.Rows[i][4].ToString());
				List[i].PatNum        = PIn.PInt   (table.Rows[i][5].ToString());
			}
			return List;
		}

		///<summary>Used when viewing various audit trails of specific types.</summary>
		public static SecurityLog[] Refresh(int patNum,Permissions[] permTypes){
			string types="";
			for(int i=0;i<permTypes.Length;i++){
				if(i>0){
					types+=" OR";
				}
				types+=" PermType="+POut.PInt((int)permTypes[i]);
			}
			string command="SELECT * FROM securitylog "
				+"WHERE PatNum= '"+POut.PInt(patNum)+"' "
				+"AND ("+types+")";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			SecurityLog[] List=new SecurityLog[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new SecurityLog();
				List[i].SecurityLogNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PermType      = (Permissions)PIn.PInt(table.Rows[i][1].ToString());
				List[i].UserNum       = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].LogDateTime   = PIn.PDateT (table.Rows[i][3].ToString());	
				List[i].LogText       = PIn.PString(table.Rows[i][4].ToString());
				List[i].PatNum        = PIn.PInt   (table.Rows[i][5].ToString());
			}
			return List;
		}

		///<summary>PatNum can be 0.</summary>
		public static void MakeLogEntry(Permissions permType,int patNum, string logText){
			SecurityLog securityLog=new SecurityLog();
			securityLog.PermType=permType;
			securityLog.UserNum=Security.CurUser.UserNum;
			securityLog.LogText=logText;
			securityLog.PatNum=patNum;
			securityLog.Insert();
		}

		

	}

	


}













