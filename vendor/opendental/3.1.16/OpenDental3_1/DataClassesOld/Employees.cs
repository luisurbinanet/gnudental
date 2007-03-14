using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the employee table in the database.</summary>
	public struct Employee{
		///<summary>Primary key.</summary>
		public int EmployeeNum;
		///<summary>Employee's last name.</summary>
		public string LName;
		///<summary>First name.</summary>
		public string FName;
		///<summary>Middle initial or name.</summary>
		public string MiddleI; 
		///<summary>Password hash.</summary>
		public string Password;
		///<summary>If hidden, the employee will not show on the list.</summary>
		public bool IsHidden;
		///<summary>The user name for use in logging in and out.</summary>
		public string UserName;
		///<summary>This is just text used to quickly display the clockstatus.  eg Working,Break,Lunch,Home, etc.</summary>
		public string ClockStatus;
		//public string Abbrev;//Not in use
		//public bool IsAdmin;//Not in use
		//public string TimePeriodType;//Not in use
	}
	
	/*=========================================================================================
	=================================== class Employees ==========================================*/
	///<summary></summary>
	public class Employees:DataClass{
		///<summary></summary>
		public static Employee[] ListLong;
		///<summary></summary>
		public static Employee[] ListShort;//does not include hidden employees
		///<summary></summary>
		public static Employee Cur;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText="SELECT * FROM employee";
			FillTable();
			ListLong=new Employee[table.Rows.Count];
			ArrayList tempList=new ArrayList();
			//Employee temp;
			for(int i=0;i<table.Rows.Count;i++){
				//temp=new Employee();
				ListLong[i].EmployeeNum = PIn.PInt   (table.Rows[i][0].ToString());
				ListLong[i].LName =       PIn.PString(table.Rows[i][1].ToString());
				ListLong[i].FName =       PIn.PString(table.Rows[i][2].ToString());
				ListLong[i].MiddleI =     PIn.PString(table.Rows[i][3].ToString());
				ListLong[i].Password =    PIn.PString(table.Rows[i][4].ToString());
				ListLong[i].IsHidden =    PIn.PBool  (table.Rows[i][5].ToString());
				ListLong[i].UserName =	  PIn.PString(table.Rows[i][6].ToString());
				ListLong[i].ClockStatus =	PIn.PString(table.Rows[i][7].ToString());
				if(!ListLong[i].IsHidden){
					tempList.Add(ListLong[i]);
				}
			}
			ListShort=new Employee[tempList.Count];
			for(int i=0;i<tempList.Count;i++){
				ListShort[i]=(Employee)tempList[i];
			}
		}

		///<summary></summary>
		public static void UpdateCur(){//updates Cur
			cmd.CommandText="UPDATE employee SET " 
				+ "lname = '"       +POut.PString(Cur.LName)+"' "
				+ ",fname = '"      +POut.PString(Cur.FName)+"' "
				+ ",middlei = '"    +POut.PString(Cur.MiddleI)+"' "
				+ ",password = '"   +POut.PString(Cur.Password)+"' "
				+ ",ishidden = '"   +POut.PBool  (Cur.IsHidden)+"' "
				+ ",username = '"   +POut.PString(Cur.UserName)+"' "
				+ ",ClockStatus = '"+POut.PString(Cur.ClockStatus)+"' "
				+"WHERE EmployeeNum = '"+POut.PInt(Cur.EmployeeNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ();
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO employee (lname,fname,middlei,password,ishidden,username"
				+",ClockStatus) "
				+"VALUES("
				+"'"+POut.PString(Cur.LName)+"', "
				+"'"+POut.PString(Cur.FName)+"', "
				+"'"+POut.PString(Cur.MiddleI)+"', "
				+"'"+POut.PString(Cur.Password)+"', "
				+"'"+POut.PBool  (Cur.IsHidden)+"', "
				+"'"+POut.PString(Cur.UserName)+"', "
				+"'"+POut.PString(Cur.ClockStatus)+"')";
			NonQ(true);
			Cur.EmployeeNum=InsertID;
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from employee WHERE EmployeeNum = '"+Cur.EmployeeNum.ToString()+"'";
			NonQ(false);
		}

		///<summary>Returns LName,FName MiddleI for the provided employee.</summary>
		public static string GetName(Employee emp){
			return(emp.LName+", "+emp.FName+" "+emp.MiddleI);
		}

		///<summary>Loops through List to find matching employee, and returns LName,FName MiddleI.</summary>
		public static string GetName(int employeeNum){
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].EmployeeNum==employeeNum){
					return GetName(ListLong[i]);
				}
			}
			return "";
		}

		///<summary>Loops through List to find matching employee, and returns first 2 letters of first name.  Will later be improved with abbr field.</summary>
		public static string GetAbbr(int employeeNum){
			string retVal="";
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].EmployeeNum==employeeNum){
					retVal=ListLong[i].FName;
					if(retVal.Length>2)
						retVal=retVal.Substring(0,2);
					return retVal;
				}
			}
			return "";
		}



	}

	

	
	

}













