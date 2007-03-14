using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{

	//<summary>Table user.  Every provider and employee is assigned a unique user number,
	//whether or not they have been assigned a username and password. A usernumber can never
	//be changed, ensuring a permanent way to record transactions.</summary>
	/// <summary>Not a database table yet.</summary>
	public struct User{
		//<summary>Primary key.</summary>
		//public int UserNum;
		///<summary></summary>
		public string UserName;
		///<summary></summary>
		public string Password;
		///<summary></summary>
		public int EmployeeNum;//Foreign key to employee.employeeNum
		///<summary></summary>
		public int ProvNum;//Foreign key to provider.ProvNum. Either Emp or Prov, not both.
	}

	/*=========================================================================================
	=================================== class Users==========================================*/
	///<summary></summary>
	public class Users{
		///<summary></summary>
		public static User Cur;
		///<summary></summary>
		public static User[] List;   

		///<summary></summary>
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
		public static bool UserNameTaken(string userName){
			for(int i=0;i<List.Length;i++){
				if(List[i].UserName==userName)
					return true;
			}
			return false;
		}

	}
 
	

	
}













