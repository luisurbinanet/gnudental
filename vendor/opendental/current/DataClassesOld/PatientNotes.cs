using System;
using System.Collections;
//using System.Data;
//using System.Drawing;
//using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Essentially more columns in the patient table.  They are stored here because these fields can contain a lot of information, and we want to try to keep the size of the patient table a bit smaller.</summary>
	public struct PatientNote{
		///<summary>FK to patient.PatNum.  Also the primary key for this table. Always one to one relationship with patient table.  A new patient might not have an entry here until needed.</summary>
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
		///<summary>Shows in the Chart module just below the graphical tooth chart.</summary>
		public string Treatment;
	}

	/*=========================================================================================
		=================================== class PatientNotes ===========================================*/
	///<summary></summary>
	public class PatientNotes:DataClass{
		///<summary></summary>
		public static PatientNote Cur;
		
		///<summary></summary>
		public static void Refresh(int patNum,int guarantor){
			cmd.CommandText = 
				"SELECT * FROM patientnote WHERE patnum = '"+POut.PInt(patNum)+"'";
			FillTable();
			if(table.Rows.Count==0){
				InsertRow(patNum);
			}
			cmd.CommandText = 
				"SELECT PatNum,ApptPhone,Medical,Service,MedicalComp,Treatment "
				+"FROM patientnote WHERE patnum ='"+POut.PInt(patNum)+"'";
			FillTable();
			Cur.PatNum      = PIn.PInt   (table.Rows[0][0].ToString());
			Cur.ApptPhone   = PIn.PString(table.Rows[0][1].ToString());
			Cur.Medical     = PIn.PString(table.Rows[0][2].ToString());
			Cur.Service     = PIn.PString(table.Rows[0][3].ToString());
			Cur.MedicalComp = PIn.PString(table.Rows[0][4].ToString());
			Cur.Treatment   = PIn.PString(table.Rows[0][5].ToString());
			//fam financial note:
			cmd.CommandText = 
				"SELECT * FROM patientnote WHERE patnum ='"+POut.PInt(guarantor)+"'";
			FillTable();
			if(table.Rows.Count==0){
				InsertRow(guarantor);
			}
			cmd.CommandText = 
				"SELECT famfinancial "
				+"FROM patientnote WHERE patnum ='"+POut.PInt(guarantor)+"'";
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			Cur.FamFinancial= PIn.PString(table.Rows[0][0].ToString());
		}

		///<summary></summary>
		public static void UpdateCur(int guarantor){
			cmd.CommandText = "UPDATE patientnote SET "
				//+ "apptphone = '"   +POut.PString(Cur.ApptPhone)+"'"
				+ "Medical = '"     +POut.PString(Cur.Medical)+"'"
				+ ",Service = '"    +POut.PString(Cur.Service)+"'"
				+ ",MedicalComp = '"+POut.PString(Cur.MedicalComp)+"'"
				+ ",Treatment = '"  +POut.PString(Cur.Treatment)+"'"
				+" WHERE patnum = '"+POut.PInt   (Cur.PatNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
			cmd.CommandText = "UPDATE patientnote SET "
				+ "famfinancial = '"+POut.PString(Cur.FamFinancial)+"'"
				+" WHERE patnum = '"+POut.PInt   (guarantor)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		private static void InsertRow(int patNum){
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










