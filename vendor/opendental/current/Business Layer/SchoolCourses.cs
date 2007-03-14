using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Used in dental schools.  eg OP 732 Operative Dentistry Clinic II.</summary>
	public class SchoolCourse{
		///<summary>Primary key.</summary>
		public int SchoolCourseNum;
		///<summary>Alphanumeric.  eg PEDO 732.</summary>
		public string CourseID;
		///<summary>eg: Pediatric Dentistry Clinic II</summary>
		public string Descript;
		
		///<summary></summary>
		public SchoolCourse Copy(){
			SchoolCourse sc=new SchoolCourse();
			sc.SchoolCourseNum=SchoolCourseNum;
			sc.CourseID=CourseID;
			sc.Descript=Descript;
			return sc;
		}

		///<summary></summary>
		private void Update(){
			string command= "UPDATE schoolcourse SET " 
				+"SchoolCourseNum = '" +POut.PInt   (SchoolCourseNum)+"'"
				+",CourseID = '"       +POut.PString(CourseID)+"'"
				+",Descript = '"       +POut.PString(Descript)+"'"
				+" WHERE SchoolCourseNum = '"+POut.PInt(SchoolCourseNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		private void Insert(){
			if(Prefs.RandomKeys){
				SchoolCourseNum=MiscData.GetKey("schoolcourse","SchoolCourseNum");
			}
			string command= "INSERT INTO schoolcourse (";
			if(Prefs.RandomKeys){
				command+="SchoolCourseNum,";
			}
			command+="CourseID,Descript) VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(SchoolCourseNum)+"', ";
			}
			command+=
				 "'"+POut.PString(CourseID)+"', "
				+"'"+POut.PString(Descript)+"')";
			DataConnection dcon=new DataConnection();
 			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				SchoolCourseNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void InsertOrUpdate(bool isNew){
			//if(IsRepeating && DateTask.Year>1880){
			//	throw new Exception(Lan.g(this,"Task cannot be tagged repeating and also have a date."));
			//}
			if(isNew){
				Insert();
			}
			else{
				Update();
			}
		}

		///<summary></summary>
		public void Delete(){
			//todo: check for dependencies

			string command= "DELETE from schoolcourse WHERE SchoolCourseNum = '"
				+POut.PInt(SchoolCourseNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}


	}

	/*=========================================================================================
		=================================== Course SchoolCourses ==========================================*/

	///<summary></summary>
	public class SchoolCourses{
		///<summary></summary>
		public static SchoolCourse[] List;
			
		///<summary>Refreshes all SchoolCourses.</summary>
		public static void Refresh(){
			string command=
				"SELECT * FROM schoolcourse "
				+"ORDER BY CourseID";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			List=new SchoolCourse[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new SchoolCourse();
				List[i].SchoolCourseNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].CourseID       = PIn.PString(table.Rows[i][1].ToString());
				List[i].Descript       = PIn.PString(table.Rows[i][2].ToString());
			}
		}

	
	}

	

	


}




















