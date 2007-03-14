using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the securitylog table in the database.  This table will soon be eliminated</summary>
	public class SecurityLog{
		///<summary>Primary key.</summary>
		public int SecurityLogNum;
		///<summary>Permission name in plain text.</summary>
		public string Permission;
		///<summary>User name in plain text.</summary>
		public string UserName;
		///<summary>The date and time of the entry.</summary>
		public DateTime LogDateTime;
		///<summary>The description of exactly what was done. Varies by permission type.</summary>
		public string LogText;

		///<summary></summary>
		public void Insert(){
			string command="INSERT INTO securitylog (permission,username,logdatetime,logtext) "
				+"VALUES ("
				+"'"+POut.PString(Permission)+"', "
				+"'"+POut.PString(UserName)+"', "
				+"'"+POut.PDateT (LogDateTime)+"', "
				+"'"+POut.PString(LogText)+"')";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			SecurityLogNum=dcon.InsertID;
		}

		//there are no methods for deleting or changing log entries because that will never be allowed.

		

	}

	/*=========================================================================================
	=================================== class SecurityLogs==========================================*/
  ///<summary></summary>
	public class SecurityLogs{
		//<summary></summary>
		//public static SecurityLog Cur;
		//<summary></summary>
		//public static SecurityLog[] List;

		/*public static void Refresh(){//this may be used later for reporting
			cmd.CommandText =
				"SELECT * from userlog";
			FillTable();
			List=new UserLog[table.Rows.Count];
			for(int i = 0;i<List.Length;i++){
				List[i].UserLogNum   = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PermissionNum= PIn.PInt   (table.Rows[i][1].ToString());
				List[i].EmployeeNum  = PIn.PInt   (table.Rows[i][2].ToString());	
				List[i].ProviderNum  = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].LogDate      = PIn.PDateT (table.Rows[i][4].ToString());
				List[i].Note         = PIn.PString(table.Rows[i][5].ToString());
			}
		}*/

		///<summary>It's ok if user==null.  It will simply disregard and not make a log entry.</summary>
		public static void MakeLogEntry(string permissionName,string logText,User user){
			if(user==null){
				return;//User should never be null.
			}
			bool IsLogged=false;
			Permission permission=Permissions.GetPermission(permissionName);
			if(permission!=null){//if permissionName is a recognized permission
				if(!permission.RequiresPassword){
					return;//no password required, so no logging either
				}
				UserPermission[] listForUser;
				if(user.EmployeeNum > 0){
					listForUser=UserPermissions.GetListForEmp(user.EmployeeNum);
				}
				else{
					listForUser=UserPermissions.GetListForProv(user.ProvNum);
				}
				for(int i=0;i<listForUser.Length;i++){
					if(listForUser[i].PermissionNum==permission.PermissionNum && listForUser[i].IsLogged){
						//?? UserPermissions.Cur=UserPermissions.ListForUser[i];
						IsLogged=true;
					}
				}
				if(!IsLogged){
					return;
				}
			}
			SecurityLog securityLog=new SecurityLog();
			//securityLog.UserName=user.UserName;
			if(user.EmployeeNum > 0){
				securityLog.UserName=Employees.GetName(user.EmployeeNum);
			}
			else{
				securityLog.UserName=Providers.GetAbbr(user.ProvNum);
			}
			securityLog.Permission=permissionName;
			securityLog.LogDateTime=DateTime.Now;
			securityLog.LogText=logText;
			securityLog.Insert();
		}

		

	}

	


}













