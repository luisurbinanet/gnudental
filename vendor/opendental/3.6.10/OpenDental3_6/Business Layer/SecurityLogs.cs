using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the securitylog table in the database.</summary>
	public class SecurityLog{
		///<summary>Primary key.</summary>
		public int SecurityLogNum;
		///<summary>See the Permissions enum.</summary>
		public Permissions PermType;
		///<summary>Foreign key to user.UserNum</summary>
		public int UserNum;
		///<summary>The date and time of the entry.  It's value is set when inserting and can never change.  Even if a user changes the date on thier ocmputer, this remains accurate because it uses server time.</summary>
		public DateTime LogDateTime;
		///<summary>The description of exactly what was done. Varies by permission type.</summary>
		public string LogText;

		///<summary></summary>
		public void Insert(){
			if(Prefs.RandomKeys){
				SecurityLogNum=MiscData.GetKey("securitylog","SecurityLogNum");
			}
			string command= "INSERT INTO securitylog (";
			if(Prefs.RandomKeys){
				command+="SecurityLogNum,";
			}
			command+="PermType,UserNum,LogDateTime,LogText) VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(SecurityLogNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   ((int)PermType)+"', "
				+"'"+POut.PInt   (UserNum)+"', "
				+"NOW(), "//LogDateTime set to current server time
				+"'"+POut.PString(LogText)+"')";
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

		///<summary></summary>
		public static SecurityLog[] Refresh(DateTime dateFrom,DateTime dateTo){
			string command="SELECT * from securitylog "
				+"WHERE LogDateTime >= '"+POut.PDate(dateFrom)+"' "
				+"AND LogDateTime <= '"+POut.PDate(dateTo.AddDays(1))+"'";
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
			}
			return List;
		}

		///<summary>User may never be null.</summary>
		public static void MakeLogEntry(Permissions permType, string logText){
			SecurityLog securityLog=new SecurityLog();
			securityLog.PermType=permType;
			securityLog.UserNum=Security.CurUser.UserNum;
			securityLog.LogText=logText;
			securityLog.Insert();
		}

		

	}

	


}












