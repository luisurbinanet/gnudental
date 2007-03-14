using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the userpermission table in the database.</summary>
	public struct UserPermission{
		///<summary>Primary key.</summary>
		public int UserPermissionNum;
		///<summary>Foreign key to permission.PermissionNum.</summary>
		public int PermissionNum;
		///<summary>Foreign key to employee.EmployeeNum.</summary>
		public int EmployeeNum;
		///<summary>Foreign key to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>If true, then activities of this permission type will be logged for this user.</summary>
		public bool IsLogged;
	}

	/*=========================================================================================
	=================================== class UserPermissions ==========================================*/
  ///<summary></summary>
	public class UserPermissions:DataClass{
		///<summary></summary>
		public static UserPermission Cur;
		///<summary></summary>
		public static UserPermission[] List;//all user permissions for all users.
		///<summary></summary>
		public static UserPermission[] ListForUser;//user permissions for a single user

		///<summary></summary>
		public static void Refresh(){
			//gets all userpermissions for all users
			cmd.CommandText =
				"SELECT * from userpermission";
			FillTable();
			List=new UserPermission[table.Rows.Count];
			for(int i = 0;i<List.Length;i++){
				List[i].UserPermissionNum= PIn.PInt (table.Rows[i][0].ToString());
				List[i].PermissionNum    = PIn.PInt (table.Rows[i][1].ToString());
				List[i].EmployeeNum      = PIn.PInt (table.Rows[i][2].ToString());	
				List[i].ProvNum      = PIn.PInt (table.Rows[i][3].ToString());
				List[i].IsLogged         = PIn.PBool(table.Rows[i][4].ToString());
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO userpermission (permissionnum,employeenum,provnum,islogged) "
				+"VALUES ("
				+"'"+POut.PInt (Cur.PermissionNum)+"', "
				+"'"+POut.PInt (Cur.EmployeeNum)+"', "
				+"'"+POut.PInt (Cur.ProvNum)+"', "
				+"'"+POut.PBool(Cur.IsLogged)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.UserPermissionNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE userpermission SET "
				+"permissionnum='" +POut.PInt (Cur.PermissionNum)+"'"
				+",employeenum ='" +POut.PInt (Cur.EmployeeNum)+"'"
				+",provnum ='"     +POut.PInt (Cur.ProvNum)+"'"
				+",islogged    ='" +POut.PBool(Cur.IsLogged)+"'"
				+" WHERE userpermissionnum = '"+POut.PInt(Cur.UserPermissionNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from userpermission WHERE userpermissionnum = '"
				+POut.PInt(Cur.UserPermissionNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteAllForEmp(int employeeNum){
			cmd.CommandText = "DELETE from userpermission WHERE employeenum = '"+POut.PInt(employeeNum)+"'";
			NonQ(false);
		}
		
		///<summary></summary>
		public static void DeleteAllForProv(int provNum){
			cmd.CommandText = "DELETE from userpermission WHERE provnum = '"+POut.PInt(provNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void GetListForEmp(int employeeNum){
			//subset of List including only for one user
			ArrayList ALtemp=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].EmployeeNum==employeeNum){
					ALtemp.Add(List[i]);
				} 
			}
			if(ALtemp.Count==0){
				ListForUser=new UserPermission[0];  
			}
			else{
				ListForUser=new UserPermission[ALtemp.Count];
				ALtemp.CopyTo(ListForUser);
			}    
		}

		///<summary></summary>
		public static void GetListForProv(int provNum){
			//subset of List including only for one user
			ArrayList ALtemp=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ProvNum==provNum){
					ALtemp.Add(List[i]);
				} 
			}
			if(ALtemp.Count==0){
				ListForUser=new UserPermission[0];  
			}
			else{
				ListForUser=new UserPermission[ALtemp.Count];
				ALtemp.CopyTo(ListForUser);
			}    
		}

		///<summary></summary>
		public static int AdministratorCount(){//TestForAdminCount(){
			//returns number of provs with security administration permission
			cmd.CommandText=
				"SELECT userpermission.userpermissionnum FROM userpermission,permission "
				+"WHERE userpermission.permissionnum=permission.permissionnum && "
				+"permission.name='Security Administration'";
			FillTable();
			return table.Rows.Count;
		}

		/*public static void ResetPassword(){
			Permissions.GetCur("Security Administration");
			cmd.CommandText="DELETE from userpermission where permissionnum = '"
				+Permissions.Cur.PermissionNum+"'";
			NonQ(false);
		}*/

		///<summary>Used for most security checks in program. Displays user/password dialog only if necessary.</summary>
		public static bool CheckUserPassword(string permissionName){
			Permissions.GetCur(permissionName);
			if(!Permissions.Cur.RequiresPassword){
				return true;//password not required.
			}
			FormPassword FormP=new FormPassword();
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK){
				return false;//bad password
			}
			if(Users.Cur.EmployeeNum > 0){
				GetListForEmp(Users.Cur.EmployeeNum);
			}
			else{
				GetListForProv(Users.Cur.ProvNum);
			}
			for(int i=0;i<ListForUser.Length;i++){
				if(ListForUser[i].PermissionNum==Permissions.Cur.PermissionNum){
					Cur=ListForUser[i];//not sure why this is necessary.  might be in use.
					return true;//allow access
				}
			}		
			return false;
		}

		///<summary></summary>
		public static bool CheckUserPassword(string permissionName,DateTime myDate){
			//only checks password if before a certain date or number of days
			Permissions.GetCur(permissionName);
			bool doCheck=false;
			if(myDate <= Permissions.Cur.BeforeDate){//if date is before specified date
				doCheck=true;
			}
			if(myDate <= DateTime.Today.AddDays(-Permissions.Cur.BeforeDays)){//if date is older than # of days
				doCheck=true;
			}
			if(doCheck){
				return CheckUserPassword(permissionName);
			}
			else return true;//allow access if newer
		}

		///<summary></summary>
		public static bool CheckHasPermission(string permissionName,int num,bool IsEmployee){
			//used when selecting all permissions in form prov or emp.
			//also used whenever we have already displayed a dialog and just want to check permission for a user
			Permissions.GetCur(permissionName);
			if(IsEmployee)
				GetListForEmp(num);
			else
				GetListForProv(num);
			for(int i=0;i<ListForUser.Length;i++){
				if(ListForUser[i].PermissionNum==Permissions.Cur.PermissionNum){
					Cur=ListForUser[i];
					return true;
				}
			}
			return false;
		}

		//public static void GetAdminProvider(){
			/*Providers.Cur=new Provider();
			for(int i=0;i<Providers.List.Length;i++){
				if(CheckHasPermission("Administer Passwords",Providers.List[i].ProvNum,false)){
					Providers.Cur=Providers.List[i];
					MessageBox.Show(Lan.g("Permissions","Found"));
				}
			}*/
		//}

	}//end class UserPermissions

	

	
}













