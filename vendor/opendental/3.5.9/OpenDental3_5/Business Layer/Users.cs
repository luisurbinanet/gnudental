using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{

	//<summary>Table user.  Users are now a completely separate entity from Providers and Employees.  A usernumber can never be changed, ensuring a permanent way to record transactions.</summary>
	/// <summary>Not a database table yet.</summary>
	public class User{
		//<summary>Primary key.</summary>
		//public int UserNum;
		///<summary></summary>
		public string UserName;
		///<summary>The password hash, not the actual password.</summary>
		public string Password;
		///<summary>Foreign key to employee.employeeNum.  Soon to be obsolete.</summary>
		public int EmployeeNum;//
		///<summary>Foreign key to provider.ProvNum. Either Emp or Prov, not both.  Soon to be obsolete.</summary>
		public int ProvNum;
	}

	/*=========================================================================================
	=================================== class Users==========================================*/
	///<summary></summary>
	public class Users{
		//<summary></summary>
		//public static User Cur;
		///<summary>A merged list of employees an providers.  This will soon be a list from the new user table.</summary>
		public static User[] List;   

		///<summary>MUST have refreshed providers and Employees first.</summary>
		public static void Refresh(){
			ArrayList AL=new ArrayList();
			for(int i=0;i<Providers.List.Length;i++){
				User user=new User();
				user.UserName=Providers.List[i].UserName;
				user.Password=Providers.List[i].Password;
				user.ProvNum=Providers.List[i].ProvNum;
				if(user.UserName!="")
					AL.Add(user);
			}
			for(int i=0;i<Employees.ListLong.Length;i++){
				User user=new User();
				user.UserName=Employees.ListLong[i].UserName;
				user.Password=Employees.ListLong[i].Password;
				user.EmployeeNum=Employees.ListLong[i].EmployeeNum;
				if(user.UserName!="")
					AL.Add(user);
			}
			if(AL.Count==0){
				List=new User[0];  
			}
			else{
				List=new User[AL.Count];
				AL.CopyTo(List);
			}  
		}

		///<summary></summary>
		public static bool UserNameTakenn(string userName){
			for(int i=0;i<List.Length;i++){
				if(List[i].UserName==userName)
					return true;
			}
			return false;
		}

		///<summary>Displays user/password dialog.  Returns a valid authenticated user or null.  Pass in the English string to display.  It will get converted to other languages just before display.</summary>
		public static User Authenticate(string display){
			FormPassword FormP=new FormPassword(display);
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK){
				return null;//bad password
			}
			return FormP.AuthenticatedUser;
		}

		///<summary></summary>
		public static User GetUserEmp(int employeeNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].EmployeeNum==employeeNum)
					return List[i];
			}
			return null;//will never happen
		}

		///<summary></summary>
		public static User GetUserProv(int provNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].ProvNum==provNum)
					return List[i];
			}
			return null;//will never happen
		}
		

	}
 
	

	
}













