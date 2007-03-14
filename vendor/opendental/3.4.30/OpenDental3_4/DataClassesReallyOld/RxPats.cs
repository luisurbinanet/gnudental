using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the rxpat table in the database.  One Rx for one patient. Copied from rxdef rather than linked to it.</summary>
	public struct RxPat{
		///<summary>Primary key.</summary>
		public int RxNum;
		///<summary>Foreign key to patient.PatNum.</summary>
		public int PatNum;
		///<summary>Date of Rx.</summary>
		public DateTime RxDate;
		///<summary>Drug name.</summary>
		public string Drug;
		///<summary>Directions.</summary>
		public string Sig;
		///<summary>Amount to dispense.</summary>
		public string Disp;
		///<summary>Number of refills.</summary>
		public string Refills;
		///<summary>Foreign key to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>Notes specific to this Rx.</summary>
		public string Notes;
	}

	/*=========================================================================================
	=================================== class RxPats ==========================================*/
///<summary></summary>
	public class RxPats:DataClass{
		///<summary></summary>
		public static RxPat[] List;
		///<summary></summary>
		public static RxPat Cur;

		///<summary></summary>
		public static void Refresh(int patNum){
			cmd.CommandText =
				"SELECT * from rxpat"
				+" WHERE patnum = '"+POut.PInt(patNum)+"'"
				+" ORDER BY rxdate";
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			List=new RxPat[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].RxNum      = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum     = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].RxDate     = PIn.PDate  (table.Rows[i][2].ToString());
				List[i].Drug       = PIn.PString(table.Rows[i][3].ToString());
				List[i].Sig        = PIn.PString(table.Rows[i][4].ToString());
				List[i].Disp       = PIn.PString(table.Rows[i][5].ToString());
				List[i].Refills    = PIn.PString(table.Rows[i][6].ToString());
				List[i].ProvNum    = PIn.PInt   (table.Rows[i][7].ToString());
				List[i].Notes      = PIn.PString(table.Rows[i][8].ToString());
			}
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE rxpat SET " 
				+ "patnum = '"      +POut.PInt   (Cur.PatNum)+"'"
				+ ",rxdate = '"     +POut.PDate  (Cur.RxDate)+"'"
				+ ",drug = '"       +POut.PString(Cur.Drug)+"'"
				+ ",sig = '"        +POut.PString(Cur.Sig)+"'"
				+ ",disp = '"       +POut.PString(Cur.Disp)+"'"
				+ ",refills = '"    +POut.PString(Cur.Refills)+"'"
				+ ",provnum = '"    +POut.PInt   (Cur.ProvNum)+"'"
				+ ",notes = '"      +POut.PString(Cur.Notes)+"'"
				+" WHERE RxNum = '" +POut.PInt (Cur.RxNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO rxpat (patnum,rxdate,drug,sig,"
				+"disp,refills,provnum,notes) VALUES("
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PDate  (Cur.RxDate)+"', "
				+"'"+POut.PString(Cur.Drug)+"', "
				+"'"+POut.PString(Cur.Sig)+"', "
				+"'"+POut.PString(Cur.Disp)+"', "
				+"'"+POut.PString(Cur.Refills)+"', "
				+"'"+POut.PInt   (Cur.ProvNum)+"', "
				+"'"+POut.PString(Cur.Notes)+"')";
			NonQ(true);
			Cur.RxNum=InsertID;
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from rxpat WHERE RxNum = '"+POut.PInt(Cur.RxNum)+"'";
			NonQ();
		}
	}

	


}













