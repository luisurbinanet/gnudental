using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the medication table in the database.</summary>
	public struct Medication{
		///<summary>Primary key.</summary>
		public int MedicationNum;
		///<summary>Name of the medication.</summary>
		public string MedName;
		///<summary>Foreign key to medication.MedicationNum.  If this is a generic drug, then the GenericNum will be the same as the MedicationNum.</summary>
		public int GenericNum;
		///<summary>Notes.</summary>
		public string Notes;
	}
	
	/*=========================================================================================
		=================================== class Medications ==========================================*/
	///<summary></summary>
	public class Medications:DataClass{
		//not refreshed with local data.  Only refreshed as needed.
		///<summary></summary>
		public static Medication Cur;
		///<summary></summary>
		public static Medication[] List;
		///<summary></summary>
		public static Hashtable HList;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from medication ORDER BY MedName";
			FillList();
		}

		///<summary></summary>
		public static void RefreshGeneric(){
			cmd.CommandText =
				"SELECT * from medication WHERE medicationnum = genericnum ORDER BY MedName";
			FillList();
		}

		private static void FillList(){
			FillTable();
			HList=new Hashtable();
			List=new Medication[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].MedicationNum=PIn.PInt   (table.Rows[i][0].ToString());
				List[i].MedName      =PIn.PString(table.Rows[i][1].ToString());
				List[i].GenericNum   =PIn.PInt   (table.Rows[i][2].ToString());
				List[i].Notes        =PIn.PString(table.Rows[i][3].ToString());
				HList.Add(List[i].MedicationNum,List[i]);
			}
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE medication SET " 
				+ "medname = '"      +POut.PString(Cur.MedName)+"'"
				+ ",genericnum = '"  +POut.PInt   (Cur.GenericNum)+"'"
				+ ",notes = '"       +POut.PString(Cur.Notes)+"'"
				+" WHERE medicationnum = '" +POut.PInt   (Cur.MedicationNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO medication (medname,genericnum,notes"
				+") VALUES("
				+"'"+POut.PString(Cur.MedName)+"', "
				+"'"+POut.PInt   (Cur.GenericNum)+"', "
				+"'"+POut.PString(Cur.Notes)+"')";
			NonQ(true);
			Cur.MedicationNum=InsertID;
			//MessageBox.Show(Cur.PayNum.ToString());
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from medication WHERE medicationNum = '"+Cur.MedicationNum.ToString()+"'";
			NonQ(false);
		}
		
	}

	




}










