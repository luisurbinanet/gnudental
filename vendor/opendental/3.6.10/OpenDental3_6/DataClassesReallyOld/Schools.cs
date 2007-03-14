using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the school table in the database. Used in public health.</summary>
	public struct School{
		//<summary>Primary key, but not referenced by any other table.</summary>
		//public string SchoolNum;
		///<summary>Primary key, but allowed to change.  Change is programmatically synchronized.</summary>
		public string SchoolName;
		///<summary>Optional. Usage varies.</summary>
		public string SchoolCode;
		///<summary>Not a database field. This is the unaltered SchoolName. Used for Update.</summary>
		public string OldSchoolName;
	}

	/*=========================================================================================
		=================================== class Schools ===========================================*/
  ///<summary></summary>
	public class Schools:DataClass{
		///<summary></summary>
		public static School Cur;
		///<summary>This list is only refreshed as needed rather than being part of the local data.</summary>
		public static School[] List;
		///<summary>Used in screening window. Simpler interface.</summary>
		public static string[] ListNames;

		///<summary></summary>
		public static void Refresh(string name){
			cmd.CommandText =
				"SELECT * from school "
				+"WHERE SchoolName LIKE '"+name+"%' "
				+"ORDER BY SchoolName";
			FillTable();
			List=new School[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i].SchoolName =PIn.PString(table.Rows[i][0].ToString());
				List[i].SchoolCode =PIn.PString(table.Rows[i][1].ToString());
				List[i].OldSchoolName =PIn.PString(table.Rows[i][0].ToString());
			}
		}

		///<summary></summary>
		public static void Refresh(){
			Refresh("");
		}

		///<summary>Gets an array of strings containing all the schools in alphabetical order.  Used for the screening interface which must be simpler than the usual interface.</summary>
		public static void GetListNames(){
			cmd.CommandText =
				"SELECT SchoolName from school "
				+"ORDER BY SchoolName";
			FillTable();
			ListNames=new string[table.Rows.Count];
			for(int i=0;i<ListNames.Length;i++){
				ListNames[i]=PIn.PString(table.Rows[i][0].ToString());
			}
		}

		///<summary>Need to make sure schoolname not already in db.</summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO school (SchoolName,SchoolCode) "
				+"VALUES ("
				+"'"+POut.PString(Cur.SchoolName)+"', "
				+"'"+POut.PString(Cur.SchoolCode)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ();
		}

		///<summary>Updates the schoolname and code in the school table, and also updates all patients that were using the oldschool name.</summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE school SET "
				+"SchoolName ='"  +POut.PString(Cur.SchoolName)+"'"
				+",SchoolCode ='" +POut.PString(Cur.SchoolCode)+"'"
				+" WHERE SchoolName = '"+POut.PString(Cur.OldSchoolName)+"'";
			NonQ();
			//then, update all patients using that school
			cmd.CommandText = "UPDATE patient SET "
				+"GradeSchool ='"  +POut.PString(Cur.SchoolName)+"'"
				+" WHERE GradeSchool = '"+POut.PString(Cur.OldSchoolName)+"'";
			NonQ();
		}

		///<summary>Must run UsedBy before running this.</summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from school WHERE SchoolName = '"+POut.PString(Cur.SchoolName)+"'";
			NonQ();
		}

		///<summary>Use before DeleteCur to determine if this school name is in use. Returns a formatted string that can be used to quickly display the names of all patients using the schoolname.</summary>
		public static string UsedBy(string schoolName){
			cmd.CommandText =
				"SELECT LName,FName from patient "
				+"WHERE GradeSchool = '"+POut.PString(schoolName)+"' ";
			FillTable();
			if(table.Rows.Count==0)
				return "";
			string retVal="";
			for(int i=0;i<table.Rows.Count;i++){
				retVal+=PIn.PString(table.Rows[i][0].ToString())+", "
					+PIn.PString(table.Rows[i][1].ToString());
				if(i<table.Rows.Count-1){//if not the last row
					retVal+="\r";
				}
			}
			return retVal;
		}

		///<summary>Use before InsertCur to determine if this school name already exists. Also used when closing patient edit window to validate that the schoolname exists.</summary>
		public static bool DoesExist(string schoolName){
			cmd.CommandText =
				"SELECT * from school "
				+"WHERE SchoolName = '"+POut.PString(schoolName)+"' ";
			FillTable();
			if(table.Rows.Count==0)
				return false;
			else
				return true;
		}

	}

	

}












