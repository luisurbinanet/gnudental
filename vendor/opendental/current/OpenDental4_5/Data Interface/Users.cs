using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Users are a completely separate entity from Providers and Employees.  A usernumber can never be changed, ensuring a permanent way to record database entries and leave an audit trail.  A provider or employee can have multiple user entries for different situations.  You can also have users who are neither providers or employees.</summary>
	public class User{
		///<summary>Primary key.</summary>
		public int UserNum;
		///<summary>.</summary>
		public string UserName;
		///<summary>The password hash, not the actual password.  If no password has been entered, then this will be blank.</summary>
		public string Password;
		///<summary>FK to usergroup.UserGroupNum.  Every user belongs to exactly one user group.  Th usergroup determines the permissions.</summary>
		public int UserGroupNum;
		///<summary>FK to employee.EmployeeNum.  Used for timecards to block access by other users.</summary>
		public int EmployeeNum;

		///<summary></summary>
		public User Copy(){
			User u=new User();
			u.UserNum=UserNum;
			u.UserName=UserName;
			u.Password=Password;
			u.UserGroupNum=UserGroupNum;
			u.EmployeeNum=EmployeeNum;
			return u;
		}

		///<summary></summary>
		private void Update(){
			string command= "UPDATE user SET " 
				+"UserName = '"      +POut.PString(UserName)+"'"
				+",Password = '"     +POut.PString(Password)+"'"
				+",UserGroupNum = '" +POut.PInt   (UserGroupNum)+"'"
				+",EmployeeNum = '"  +POut.PInt   (EmployeeNum)+"'"
				+" WHERE UserNum = '"+POut.PInt   (UserNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		private void Insert(){
			string command= "INSERT INTO user (UserName,Password,UserGroupNum,EmployeeNum) VALUES("
				+"'"+POut.PString(UserName)+"', "
				+"'"+POut.PString(Password)+"', "
				+"'"+POut.PInt   (UserGroupNum)+"', "
				+"'"+POut.PInt   (EmployeeNum)+"')";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			UserNum=dcon.InsertID;
		}

		///<summary></summary>
		public void InsertOrUpdate(bool isNew){
			//make sure username is not already taken
			string command;
			if(isNew){
				command="SELECT COUNT(*) FROM user WHERE UserName='"+POut.PString(UserName)+"'";
			}
			else{
				command="SELECT COUNT(*) FROM user WHERE UserName='"+POut.PString(UserName)+"' "
					+"AND UserNum !="+POut.PInt(UserNum);//it's ok if the name matches the current username
			}
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(table.Rows[0][0].ToString()!="0"){
				throw new Exception(Lan.g(this,"UserName already in use."));
			}
			//make sure that there would still be at least one user with security admin permissions
			if(!isNew){
				command="SELECT COUNT(*) FROM grouppermission "
					+"WHERE PermType='"+POut.PInt((int)Permissions.SecurityAdmin)+"' "
					+"AND UserGroupNum="+POut.PInt(UserGroupNum);
				if(dcon.GetCount(command)=="0"){//if this user would not have admin
					//make sure someone else has admin
					command="SELECT COUNT(*) FROM user,grouppermission "
						+"WHERE grouppermission.PermType='"+POut.PInt((int)Permissions.SecurityAdmin)+"'"
						+" AND user.UserGroupNum=grouppermission.UserGroupNum"
						+" AND user.UserNum != "+POut.PInt(UserNum);
					if(dcon.GetCount(command)=="0"){//there are no other users with this permission
						throw new Exception(Lan.g(this,"At least one user must have Security Admin permission."));
					}
				}
			}
			if(isNew){
				Insert();
			}
			else{
				Update();
			}
		}

		///<summary></summary>
		public void Delete(){
			//check to make sure this is not the last user with security admin permissions
			string command="SELECT COUNT(*) FROM user,grouppermission "
				+"WHERE grouppermission.PermType='"+POut.PInt((int)Permissions.SecurityAdmin)+"'"
				+" AND user.UserGroupNum=grouppermission.UserGroupNum"
				+" AND user.UserNum != "+POut.PInt(UserNum);
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(table.Rows[0][0].ToString()=="0"){//there are no other users with this permission
				throw new Exception(Lan.g(this,"At least one user must have Security Admin permission."));
			}
			//check to make sure this user has never been referenced in security log
			command="SELECT COUNT(*) FROM securitylog WHERE UserNum="+POut.PInt(UserNum);
			table=dcon.GetTable(command);
			if(table.Rows[0][0].ToString()!="0"){
				throw new Exception(Lan.g(this,"User cannot be deleted because they have already been recorded in the security log."));
			}
			command="DELETE from user WHERE UserNum = "+POut.PInt(UserNum);
 			dcon.NonQ(command);
		}



	}

	/*=========================================================================================
	=================================== class Users==========================================*/
	///<summary></summary>
	public class Users{
		///<summary>A list of all users.</summary>
		public static User[] List;   

		///<summary></summary>
		public static void Refresh(){
			string command="SELECT * from user ORDER BY UserName";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			List=new User[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new User();
				List[i].UserNum       = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].UserName      = PIn.PString(table.Rows[i][1].ToString());
				List[i].Password      = PIn.PString(table.Rows[i][2].ToString());	
				List[i].UserGroupNum  = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].EmployeeNum   = PIn.PInt   (table.Rows[i][4].ToString());
			}
		}

		/*
		///<summary>Displays user/password dialog.  Returns a valid authenticated user or null.  Pass in the English string to display.  It will get converted to other languages just before display.</summary>
		public static User Authenticate(string display){
			FormPassword FormP=new FormPassword(display);
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK){
				return null;//bad password
			}
			return FormP.AuthenticatedUser;
		}*/

		///<summary>Used in FormSecurity.FillTreeUsers</summary>
		public static User[] GetForGroup(int userGroupNum){
			ArrayList al=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].UserGroupNum==userGroupNum){
					al.Add(List[i]);
				}
			}
			User[] retVal=new User[al.Count];
			al.CopyTo(retVal);
			return retVal;
		}

		///<summary></summary>
		public static User GetUser(int userNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].UserNum==userNum){
					return List[i].Copy();
				}
			}
			return null;
		}

		///<summary>This always returns one admin user.  There must be one and there is rarely more than one.  Only used on startup to determine if security is being used.</summary>
		public static User GetAdminUser() {
			//just find any permission for security admin.  There has to be one.
			for(int i=0;i<GroupPermissions.List.Length;i++) {
				if(GroupPermissions.List[i].PermType!=Permissions.SecurityAdmin) {
					continue;
				}
				for(int j=0;j<Users.List.Length;j++) {
					if(Users.List[j].UserGroupNum==GroupPermissions.List[i].UserGroupNum) {
						return Users.List[j];
					}
				}
			}
			return null;//will never happen
		}

		

	}
 
	

	
}













