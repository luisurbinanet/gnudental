using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the instructor table in the database.</summary>
	public class Instructor{
		///<summary>Primary key.</summary>
		public int InstructorNum;
		///<summary></summary>
		public string LName;
		///<summary></summary>
		public string FName;
		///<summary>eg DMD, DDS, RDH.</summary>
		public string Suffix;
		//<summary></summary>
		//public bool IsHidden;//do this later
		
		///<summary></summary>
		public Instructor Copy(){
			Instructor i=new Instructor();
			i.InstructorNum=InstructorNum;
			i.LName=LName;
			i.FName=FName;
			i.Suffix=Suffix;
			return i;
		}

		///<summary></summary>
		private void Update(){
			string command= "UPDATE instructor SET " 
				+"InstructorNum = '"  +POut.PInt   (InstructorNum)+"'"
				+",LName = '"         +POut.PString(LName)+"'"
				+",FName = '"         +POut.PString(FName)+"'"
				+",Suffix = '"        +POut.PString(Suffix)+"'"
				+" WHERE InstructorNum = '"+POut.PInt(InstructorNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		private void Insert(){
			string command= "INSERT INTO instructor (LName,FName,Suffix) VALUES("
				+"'"+POut.PString(LName)+"', "
				+"'"+POut.PString(FName)+"', "
				+"'"+POut.PString(Suffix)+"')";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			InstructorNum=dcon.InsertID;
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

			string command= "DELETE from instructor WHERE InstructorNum = '"
				+POut.PInt(InstructorNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}


	}

	/*=========================================================================================
		=================================== class Instructors ==========================================*/

	///<summary></summary>
	public class Instructors{
		///<summary></summary>
		public static Instructor[] List;
			
		///<summary>Refreshes all instructors.</summary>
		public static void Refresh(){
			string command=
				"SELECT * FROM instructor "
				+"ORDER BY LName,FName";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			List=new Instructor[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Instructor();
				List[i].InstructorNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].LName        = PIn.PString(table.Rows[i][1].ToString());
				List[i].FName        = PIn.PString(table.Rows[i][2].ToString());
				List[i].Suffix       = PIn.PString(table.Rows[i][3].ToString());
			}
		}

	
	}

	

	


}




















