using System;
using System.Collections;
//using System.Data;
//using System.Drawing;
//using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the patientnote table in the database.</summary>
	public struct PatientNote{
		///<summary></summary>
		public int PatNum;
		///<summary>Only one note per family stored with guarantor.</summary>
		public string FamFinancial;
		///<summary>No longer used.</summary>
		public string ApptPhone;
		///<summary>Medical Summary</summary>
		public string Medical;
		///<summary>Service notes</summary>
		public string Service;
		///<summary>Complete current Medical History</summary>
		public string MedicalComp;
	}

	/*=========================================================================================
		=================================== class PatientNotes ===========================================*/
	///<summary></summary>
	public class PatientNotes:DataClass{
		///<summary></summary>
		public static PatientNote Cur;
		
		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText = 
				"SELECT * FROM patientnote WHERE patnum = '"+POut.PInt(Patients.Cur.PatNum)+"'";
			FillTable();
			if(table.Rows.Count==0){
				InsertRow(Patients.Cur.PatNum);
			}
			cmd.CommandText = 
				"SELECT patnum,apptphone,medical,service,medicalcomp "
				+"FROM patientnote WHERE patnum ='"+POut.PInt(Patients.Cur.PatNum)+"'";
			FillTable();
			Cur.PatNum      = PIn.PInt   (table.Rows[0][0].ToString());
			Cur.ApptPhone   = PIn.PString(table.Rows[0][1].ToString());
			Cur.Medical     = PIn.PString(table.Rows[0][2].ToString());
			Cur.Service     = PIn.PString(table.Rows[0][3].ToString());
			Cur.MedicalComp = PIn.PString(table.Rows[0][4].ToString());
			//fam note:
			cmd.CommandText = 
				"SELECT * FROM patientnote WHERE patnum ='"+POut.PInt(Patients.Cur.Guarantor)+"'";
			FillTable();
			if(table.Rows.Count==0){
				InsertRow(Patients.Cur.Guarantor);
			}
			cmd.CommandText = 
				"SELECT famfinancial "
				+"FROM patientnote WHERE patnum ='"+POut.PInt(Patients.Cur.Guarantor)+"'";
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			Cur.FamFinancial= PIn.PString(table.Rows[0][0].ToString());
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE patientnote SET "
				//+ "apptphone = '"   +POut.PString(Cur.ApptPhone)+"'"
				+ "medical = '"    +POut.PString(Cur.Medical)+"'"
				+ ",service = '"    +POut.PString(Cur.Service)+"'"
				+ ",medicalcomp = '"+POut.PString(Cur.MedicalComp)+"'"
				+" WHERE patnum = '"+POut.PInt   (Cur.PatNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
			cmd.CommandText = "UPDATE patientnote SET "
				+ "famfinancial = '"+POut.PString(Cur.FamFinancial)+"'"
				+" WHERE patnum = '"+POut.PInt   (Patients.Cur.Guarantor)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertRow(int patNum){
			cmd.CommandText = "INSERT INTO patientnote (patnum"
				+") VALUES('"+patNum+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		//public static void ChangeGuarantor(int newGuarantor){
		//	 cmd.CommandText = "UPDATE familynote SET "
		//		+ "guarantor = '"+POut.PInt(newGuarantor)+"'"
		//		+" WHERE guarantor = '" +POut.PInt(Cur.Guarantor)+"'";
		//	//MessageBox.Show(cmd.CommandText);
		//	NonQ(false);
		//}

	}//end class FamilyNote

	

	

}










