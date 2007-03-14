using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the securitylog table in the database.</summary>
	public struct SecurityLog{
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
	}

	/*=========================================================================================
	=================================== class SecurityLogs==========================================*/
  ///<summary></summary>
	public class SecurityLogs:DataClass{
		///<summary></summary>
		public static SecurityLog Cur;
		///<summary></summary>
		public static SecurityLog[] List;

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

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO securitylog (permission,username,logdatetime,logtext) "
				+"VALUES ("
				+"'"+POut.PString(Cur.Permission)+"', "
				+"'"+POut.PString(Cur.UserName)+"', "
				+"'"+POut.PDateT (Cur.LogDateTime)+"', "
				+"'"+POut.PString(Cur.LogText)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.SecurityLogNum=InsertID;
		}

		//there are no methods for deleting or changing log entries because that will never be allowed.

		///<summary></summary>
		public static void MakeLogEntry(string permissionName,string logText){
			bool IsLogged=false;
			if(Permissions.GetCur(permissionName)){//if permissionName is a recognized permission
				if(!Permissions.Cur.RequiresPassword){
					return;//no password required, so no logging either
				}
				if(Users.Cur.EmployeeNum > 0){
					UserPermissions.GetListForEmp(Users.Cur.EmployeeNum);
				}
				else{
					UserPermissions.GetListForProv(Users.Cur.ProvNum);
				}
				for(int i=0;i<UserPermissions.ListForUser.Length;i++){
					if(UserPermissions.ListForUser[i].PermissionNum==Permissions.Cur.PermissionNum 
						&& UserPermissions.ListForUser[i].IsLogged){
						UserPermissions.Cur=UserPermissions.ListForUser[i];
						IsLogged=true;
					}
				}
				if(!IsLogged){
					return;
				}
			}
			Cur=new SecurityLog();
			if(Users.Cur.EmployeeNum > 0){
				Cur.UserName=Employees.GetName(UserPermissions.Cur.EmployeeNum);
			}
			else{
				Cur.UserName=Providers.GetAbbr(UserPermissions.Cur.ProvNum);
			}
			Cur.Permission=permissionName;
			Cur.LogDateTime=DateTime.Now;
			Cur.LogText=logText;
			InsertCur();
		}

	}

	


}













