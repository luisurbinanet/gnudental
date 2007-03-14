using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the employer table in the database.</summary>
	public struct Employer{
		///<summary>Primary key.</summary>
		public int EmployerNum;
		///<summary>Name of the employer.</summary>
		public string EmpName;
		///<summary></summary>
		public string Address;
		///<summary>Second line of address.</summary>
		public string Address2;
		///<summary></summary>
		public string City;
		///<summary>2 char in the US.</summary>
		public string State;
		///<summary></summary>
		public string Zip;
		///<summary>Includes any punctuation.</summary>
		public string Phone;
	}
	
	/*=========================================================================================
		=================================== class Employers ===========================================*/
	///<summary>Employers are not refreshed as local data, but are refreshed as needed. A full refresh is frequently triggered if an employerNum cannot be found in the HList.  Important retrieval is done directly from the db.</summary>
	public class Employers:DataClass{
		///<summary></summary>
		public static Employer[] List;
		///<summary>A hashtable of all employers.</summary>
		public static Hashtable HList;
		///<summary></summary>
		public static Employer Cur;

		///<summary>The functions that use this are smart enought to refresh as needed.  So no need to invalidate local data for little stuff.</summary>
		public static void Refresh(){
			HList=new Hashtable();
			cmd.CommandText = 
				"SELECT * from employer ORDER BY EmpName";
			FillTable();
			//Employer temp=new Employer();
			List=new Employer[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].EmployerNum =PIn.PInt   (table.Rows[i][0].ToString());
				List[i].EmpName     =PIn.PString(table.Rows[i][1].ToString());
				List[i].Address     =PIn.PString(table.Rows[i][2].ToString());
				List[i].Address2    =PIn.PString(table.Rows[i][3].ToString());
				List[i].City        =PIn.PString(table.Rows[i][4].ToString());
				List[i].State       =PIn.PString(table.Rows[i][5].ToString());
				List[i].Zip         =PIn.PString(table.Rows[i][6].ToString());
				List[i].Phone       =PIn.PString(table.Rows[i][7].ToString());
				HList.Add(List[i].EmployerNum,List[i]);
			}
		}

		/*
		 * Not using this because it turned out to be more efficient to refresh the whole
		 * list if an empnum could not be found.
		///<summary>Just refreshes Cur from the db with info for one employer.</summary>
		public static void Refresh(int employerNum){
			Cur=new Employer();//just in case no rows are returned
			if(employerNum==0) return;
			cmd.CommandText="SELECT * FROM employer WHERE EmployerNum = '"+employerNum+"'";
			FillTable();
			for(int i=0;i<table.Rows.Count;i++){//almost always just 1 row, but sometimes 0
				Cur.EmployerNum   =PIn.PInt   (table.Rows[i][0].ToString());
				Cur.EmpName       =PIn.PString(table.Rows[i][1].ToString());
			}
		}*/

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText="UPDATE employer SET " 
				+ "EmpName= '"  +POut.PString(Cur.EmpName)+"' "
				+ ",Address= '"    +POut.PString(Cur.Address)+"' "
				+ ",Address2= '"   +POut.PString(Cur.Address2)+"' "
				+ ",City= '"       +POut.PString(Cur.City)+"' "
				+ ",State= '"      +POut.PString(Cur.State)+"' "
				+ ",Zip= '"        +POut.PString(Cur.Zip)+"' "
				+ ",Phone= '"      +POut.PString(Cur.Phone)+"' "
				+" WHERE EmployerNum = '"+POut.PInt(Cur.EmployerNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			if(Prefs.RandomKeys){
				Cur.EmployerNum=MiscData.GetKey("employer","EmployerNum");
			}
			cmd.CommandText="INSERT INTO employer (";
			if(Prefs.RandomKeys){
				cmd.CommandText+="EmployerNum,";
			}
			cmd.CommandText+="EmpName,Address,Address2,City,State,Zip,Phone) VALUES(";
			if(Prefs.RandomKeys){
				cmd.CommandText+="'"+POut.PInt(Cur.EmployerNum)+"', ";
			}
			cmd.CommandText+=
				 "'"+POut.PString(Cur.EmpName)+"', "
				+"'"+POut.PString(Cur.Address)+"', "
				+"'"+POut.PString(Cur.Address2)+"', "
				+"'"+POut.PString(Cur.City)+"', "
				+"'"+POut.PString(Cur.State)+"', "
				+"'"+POut.PString(Cur.Zip)+"', "
				+"'"+POut.PString(Cur.Phone)+"')";
			if(Prefs.RandomKeys){
				NonQ();
			}
			else{
 				NonQ(true);
				Cur.EmployerNum=InsertID;
			}
		}

		///<summary>There MUST not be any dependencies before calling this or there will be invalid foreign keys.  This is only called from FormEmployers after proper validation.</summary>
		public static void DeleteCur(){
			cmd.CommandText="DELETE from employer WHERE EmployerNum = '"+Cur.EmployerNum.ToString()+"'";
			NonQ(false);
		}

		///<summary>Returns a list of patients that are dependent on the Cur employer. The list includes carriage returns for easy display.  Used before deleting an employer to make sure employer is not in use.</summary>
		public static string DependentPatients(){
			cmd.CommandText="SELECT CONCAT(LName,', ',FName) FROM patient" 
				+" WHERE EmployerNum = '"+POut.PInt(Cur.EmployerNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			string retStr="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					retStr+="\r\n";//return, newline for multiple names.
				}
				retStr+=PIn.PString(table.Rows[i][0].ToString());
			}
			return retStr;
		}

		///<summary>Returns a list of insplans that are dependent on the Cur employer. The list includes carriage returns for easy display.  Used before deleting an employer to make sure employer is not in use.</summary>
		public static string DependentInsPlans(){
			cmd.CommandText="SELECT insplan.Carrier,CONCAT(patient.LName,patient.FName) FROM insplan,patient" 
				+" WHERE insplan.Subscriber=patient.PatNum"
				+" && insplan.EmployerNum = '"+POut.PInt(Cur.EmployerNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			string retStr="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					retStr+="\r\n";//return, newline for multiple names.
				}
				retStr+=PIn.PString(table.Rows[i][1].ToString())+": "+PIn.PString(table.Rows[i][0].ToString());
			}
			return retStr;
		}

		///<summary>Gets the name of an employer based on the employerNum.  This also refreshes the list if necessary, so it will work even if the list has not been refreshed recently.</summary>
		public static string GetName(int employerNum){
			if(HList.ContainsKey(employerNum)){
				return ((Employer)HList[employerNum]).EmpName;
			}
			//if the employerNum could not be found:
			Refresh();
			if(HList.ContainsKey(employerNum)){
				return ((Employer)HList[employerNum]).EmpName;
			}
			//this could only happen if corrupted:
			return "";
		}

		///<summary>Gets an employer based on the employerNum. This will work even if the list has not been refreshed recently, but if you are going to need a lot of names all at once, then it is faster to refresh first.</summary>
		public static Employer GetEmployer(int employerNum){
			if(HList.ContainsKey(employerNum)){
				return (Employer)HList[employerNum];
			}
			//if the employerNum could not be found:
			Refresh();
			if(HList.ContainsKey(employerNum)){
				return (Employer)HList[employerNum];
			}
			//this could only happen if corrupted:
			return new Employer();
			
		}

		///<summary>Gets an employerNum from the database based on the supplied name.  If that empName does not exist, then a new employer is created, and the employerNum for the new employer is returned.</summary>
		public static int GetEmployerNum(string empName){
			if(empName==""){
				return 0;
			}
			cmd.CommandText="SELECT EmployerNum FROM employer" 
				+" WHERE EmpName = '"+POut.PString(empName)+"'";
			FillTable();
			if(table.Rows.Count>0){
				return PIn.PInt(table.Rows[0][0].ToString());
			}
			Cur=new Employer();
			Cur.EmpName=empName;
			InsertCur();
			//MessageBox.Show(Cur.EmployerNum.ToString());
			return Cur.EmployerNum;
		}

		///<summary>Returns an arraylist of Employers with names similar to the supplied string.  Used in dropdown list from employer field for faster entry.  There is a small chance that the list will not be completely refreshed when this is run, but it won't really matter if one employer doesn't show in dropdown.</summary>
		public static ArrayList GetSimilarNames(string empName){
			ArrayList retVal=new ArrayList();
			for(int i=0;i<List.Length;i++){
				//if(Regex.IsMatch(List[i].EmpName,"^"+empName,RegexOptions.IgnoreCase))
				if(List[i].EmpName.ToUpper().IndexOf(empName.ToUpper())==0)
					retVal.Add(List[i]);
			}
			return retVal;
		}

		///<summary>Combines all the given employers into one. Updates patient and insplan. Then deletes all the others.</summary>
		public static void Combine(int[] employerNums){
			string newNum=employerNums[0].ToString();
			for(int i=1;i<employerNums.Length;i++){
				cmd.CommandText="UPDATE patient SET EmployerNum = '"+newNum
					+"' WHERE EmployerNum = '"+employerNums[i].ToString()+"'";
				//MessageBox.Show(cmd.CommandText);
				NonQ();
				cmd.CommandText="UPDATE insplan SET EmployerNum = '"+newNum
					+"' WHERE EmployerNum = '"+employerNums[i].ToString()+"'";
				NonQ();
				cmd.CommandText="DELETE FROM employer"
					+" WHERE EmployerNum = '"+employerNums[i].ToString()+"'";
				NonQ();
			}
		}

	}

	
	

}













