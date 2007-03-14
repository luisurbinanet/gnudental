/*using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the userpermission table in the database.</summary>
	public class UserPermission{
		///<summary>Primary key.</summary>
		public int UserPermissionNum;
		///<summary>Foreign key to permission.PermissionNum.</summary>
		public int PermissionNum;
		///<summary>Foreign key to employee.EmployeeNum.  Will soon be eliminated.</summary>
		public int EmployeeNum;
		///<summary>Foreign key to provider.ProvNum.  Will soon be eliminated</summary>
		public int ProvNum;
		///<summary>If true, then activities of this permission type will be logged for this user.</summary>
		public bool IsLogged;

		///<summary></summary>
		public void Insert(){
			string command="INSERT INTO userpermission (PermissionNum,EmployeeNum,ProvNum,IsLogged) "
				+"VALUES ("
				+"'"+POut.PInt (PermissionNum)+"', "
				+"'"+POut.PInt (EmployeeNum)+"', "
				+"'"+POut.PInt (ProvNum)+"', "
				+"'"+POut.PBool(IsLogged)+"')";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			UserPermissionNum=dcon.InsertID;
		}

		///<summary></summary>
		public void Update(){
			string command="UPDATE userpermission SET "
				+"PermissionNum='" +POut.PInt (PermissionNum)+"'"
				+",EmployeeNum ='" +POut.PInt (EmployeeNum)+"'"
				+",ProvNum ='"     +POut.PInt (ProvNum)+"'"
				+",IsLogged    ='" +POut.PBool(IsLogged)+"'"
				+" WHERE UserPermissionNum = '"+POut.PInt(UserPermissionNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Delete(){
			//todo: error checking
			string command = "DELETE from userpermission WHERE UserPermissionNum = '"
				+POut.PInt(UserPermissionNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}


	}*/

	/*=========================================================================================
	=================================== class UserPermissions ==========================================*/
  /*///<summary></summary>
	public class UserPermissions:DataClass{
		///<summary>All user permissions for all users.</summary>
		private static UserPermission[] List;

		///<summary>Gets all userpermissions for all users</summary>
		public static void Refresh(){
			string command="SELECT * from userpermission";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			List=new UserPermission[table.Rows.Count];
			for(int i = 0;i<List.Length;i++){
				List[i]=new UserPermission();
				List[i].UserPermissionNum= PIn.PInt (table.Rows[i][0].ToString());
				List[i].PermissionNum    = PIn.PInt (table.Rows[i][1].ToString());
				List[i].EmployeeNum      = PIn.PInt (table.Rows[i][2].ToString());	
				List[i].ProvNum      = PIn.PInt (table.Rows[i][3].ToString());
				List[i].IsLogged         = PIn.PBool(table.Rows[i][4].ToString());
			}
		}

		///<summary></summary>
		public static void DeleteAllForEmp(int employeeNum){
			string command="DELETE from userpermission WHERE EmployeeNum = '"+POut.PInt(employeeNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}
		
		///<summary></summary>
		public static void DeleteAllForProv(int provNum){
			string command="DELETE from userpermission WHERE provnum = '"+POut.PInt(provNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary>Gets a subset of List including only for one user</summary>
		public static UserPermission[] GetListForEmp(int employeeNum){
			UserPermission[] retVal;
			ArrayList ALtemp=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].EmployeeNum==employeeNum){
					ALtemp.Add(List[i]);
				} 
			}
			retVal=new UserPermission[ALtemp.Count]; 
			if(ALtemp.Count>0){
				ALtemp.CopyTo(retVal);
			}
			return retVal;
		}

		///<summary>Gets a subset of List including only for one user</summary>
		public static UserPermission[] GetListForProv(int provNum){
			UserPermission[] retVal;
			ArrayList ALtemp=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ProvNum==provNum){
					ALtemp.Add(List[i]);
				} 
			}
			retVal=new UserPermission[ALtemp.Count]; 
			if(ALtemp.Count>0){
				ALtemp.CopyTo(retVal);
			}
			return retVal;  
		}

		///<summary></summary>
		public static int AdministratorCount(){//TestForAdminCount(){
			//returns number of provs with security administration permission
			string command=
				"SELECT userpermission.userpermissionnum FROM userpermission,permission "
				+"WHERE userpermission.permissionnum=permission.permissionnum && "
				+"permission.name='Security Administration'";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			return table.Rows.Count;
		}

		///<summary>Ok to pass null user.  Authorization will come back false.</summary>
		public static bool IsAuthorized(string permissionName,User user){
			if(user==null){
				return false;
			}
			Permission permission=Permissions.GetPermission(permissionName);
			UserPermission[] listForUser;
			if(user.EmployeeNum > 0){
				listForUser=GetListForEmp(user.EmployeeNum);
			}
			else{
				listForUser=GetListForProv(user.ProvNum);
			}
			for(int i=0;i<listForUser.Length;i++){
				if(listForUser[i].PermissionNum==permission.PermissionNum){
					return true;
				}
			}
			return false;
		}

		//public static void GetAdminProvider(){
			Providers.Cur=new Provider();
			for(int i=0;i<Providers.List.Length;i++){
				if(CheckHasPermission("Administer Passwords",Providers.List[i].ProvNum,false)){
					Providers.Cur=Providers.List[i];
					MessageBox.Show(Lan.g("Permissions","Found"));
				}
			}
		//}

	}//end class UserPermissions

	

	
}*/













