using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the schoolclass table in the database. eg. Dental 2009 or Hygiene 2007.</summary>
	public class SchoolClass{
		///<summary>Primary key.</summary>
		public int SchoolClassNum;
		///<summary>The year this class will graduate</summary>
		public int GradYear;
		///<summary>Description of this class. eg Dental or Hygiene</summary>
		public string Descript;

		///<summary></summary>
		public SchoolClass Copy(){
			SchoolClass sc=new SchoolClass();
			sc.SchoolClassNum=SchoolClassNum;
			sc.GradYear=GradYear;
			sc.Descript=Descript;
			return sc;
		}

		///<summary></summary>
		private void Update(){
			string command= "UPDATE schoolclass SET " 
				+"SchoolClassNum = '" +POut.PInt   (SchoolClassNum)+"'"
				+",GradYear = '"      +POut.PInt   (GradYear)+"'"
				+",Descript = '"      +POut.PString(Descript)+"'"
				+" WHERE SchoolClassNum = '"+POut.PInt(SchoolClassNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		private void Insert(){
			if(Prefs.RandomKeys){
				SchoolClassNum=MiscData.GetKey("schoolclass","SchoolClassNum");
			}
			string command= "INSERT INTO schoolclass (";
			if(Prefs.RandomKeys){
				command+="SchoolClassNum,";
			}
			command+="GradYear,Descript) VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(SchoolClassNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (GradYear)+"', "
				+"'"+POut.PString(Descript)+"')";
			DataConnection dcon=new DataConnection();
 			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				SchoolClassNum=dcon.InsertID;
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

			string command= "DELETE from schoolclass WHERE SchoolClassNum = '"
				+POut.PInt(SchoolClassNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}


	}

	/*=========================================================================================
		=================================== class SchoolClasses ==========================================*/

	///<summary></summary>
	public class SchoolClasses{
		///<summary></summary>
		public static SchoolClass[] List;
			
		///<summary>Refreshes all SchoolClasses.</summary>
		public static void Refresh(){
			string command=
				"SELECT * FROM schoolclass "
				+"ORDER BY GradYear,Descript";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			List=new SchoolClass[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new SchoolClass();
				List[i].SchoolClassNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].GradYear      = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].Descript      = PIn.PString(table.Rows[i][2].ToString());
			}
		}

	
	}

	

	


}




















