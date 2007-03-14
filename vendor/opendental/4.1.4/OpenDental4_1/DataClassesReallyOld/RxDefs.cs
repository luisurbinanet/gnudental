using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the rxdef table in the database.</summary>
	public struct RxDef{
		///<summary>Primary key.</summary>
		public int RxDefNum;
		///<summary>The name of the drug.</summary>
		public string Drug;
		///<summary>Directions.</summary>
		public string Sig;
		///<summary>Amount to dispense.</summary>
		public string Disp;
		///<summary>Number of refills.</summary>
		public string Refills;
		///<summary>Notes about this drug. Will not be copied to the rxpat.</summary>
		public string Notes;
	}

	/*=========================================================================================
		=================================== class RxDefs ==========================================*/
///<summary></summary>
	public class RxDefs:DataClass{
		///<summary></summary>
		public static RxDef[] List;
		///<summary></summary>
		public static RxDef Cur;
	
		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from rxdef"
				+" ORDER BY drug";
			FillTable();
			List=new RxDef[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].RxDefNum   = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Drug       = PIn.PString(table.Rows[i][1].ToString());
				List[i].Sig        = PIn.PString(table.Rows[i][2].ToString());
				List[i].Disp       = PIn.PString(table.Rows[i][3].ToString());
				List[i].Refills    = PIn.PString(table.Rows[i][4].ToString());
				List[i].Notes      = PIn.PString(table.Rows[i][5].ToString());
			}
		}
	
		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE rxdef SET " 
				+ "drug = '"       +POut.PString(Cur.Drug)+"'"
				+ ",sig = '"        +POut.PString(Cur.Sig)+"'"
				+ ",disp = '"       +POut.PString(Cur.Disp)+"'"
				+ ",refills = '"    +POut.PString(Cur.Refills)+"'"
				+ ",notes = '"      +POut.PString(Cur.Notes)+"'"
				+" WHERE RxDefNum = '" +POut.PInt (Cur.RxDefNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO rxdef (drug,sig,"
				+"disp,refills,notes) VALUES("
				//+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PString(Cur.Drug)+"', "
				+"'"+POut.PString(Cur.Sig)+"', "
				+"'"+POut.PString(Cur.Disp)+"', "
				+"'"+POut.PString(Cur.Refills)+"', "
				+"'"+POut.PString(Cur.Notes)+"')";
			NonQ(true);
			Cur.RxDefNum=InsertID;
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM rxdef "
				+"WHERE rxdefnum = '"+Cur.RxDefNum+"'";
			NonQ(false);
		}
	}

	

	


}













