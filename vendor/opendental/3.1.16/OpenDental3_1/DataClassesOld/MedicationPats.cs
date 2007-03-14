using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the medicationpat table in the database. It links medications to patients.</summary>
	public struct MedicationPat{
		///<summary>Primary key.</summary>
		public int MedicationPatNum;
		///<summary>Foreign key to patient.PatNum.</summary>
		public int PatNum;
		///<summary>Foreign key to medication.MedicationNum.</summary>
		public int MedicationNum;
		///<summary>Medication notes specific to this patient.</summary>
		public string PatNote;
	}

/*=========================================================================================
		=================================== class MedicationPats ==========================================*/

	///<summary></summary>
	public class MedicationPats:DataClass{
		///<summary></summary>
		public static MedicationPat Cur;
		///<summary></summary>
		public static MedicationPat[] List;//for current pat

		///<summary></summary>
		public static void Refresh(int patNum){
			cmd.CommandText =
				"SELECT * from medicationpat WHERE patnum = '"+patNum+"'";
			FillTable();
			List=new MedicationPat[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].MedicationPatNum=PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum          =PIn.PInt   (table.Rows[i][1].ToString());
				List[i].MedicationNum   =PIn.PInt   (table.Rows[i][2].ToString());
				List[i].PatNote         =PIn.PString(table.Rows[i][3].ToString());
				//HList.Add(List[i].MedicationNum,List[i]);
			}
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE medicationpat SET " 
				+ "patnum = '"        +POut.PInt   (Cur.PatNum)+"'"
				+ ",medicationnum = '"+POut.PInt   (Cur.MedicationNum)+"'"
				+ ",patnote = '"      +POut.PString(Cur.PatNote)+"'"
				+" WHERE medicationpatnum = '" +POut.PInt   (Cur.MedicationPatNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO medicationpat (patnum,medicationnum,patnote"
				+") VALUES("
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PInt   (Cur.MedicationNum)+"', "
				+"'"+POut.PString(Cur.PatNote)+"')";
			NonQ(true);
			Cur.MedicationPatNum=InsertID;
			//MessageBox.Show(Cur.PayNum.ToString());
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from medicationpat WHERE medicationpatNum = '"
				+Cur.MedicationPatNum.ToString()+"'";
			NonQ(false);
		}
		
	}

	





}










