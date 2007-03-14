using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the adjustment table in the database.</summary>
	public struct Adjustment{
		///<summary>Primary key.</summary>
		public int AdjNum;
		///<summary>Date of adjustment.</summary>
		public DateTime AdjDate;
		///<summary>Amount of adjustment.</summary>
		public double AdjAmt;
		///<summary>Foreign key to <see cref="Patient.PatNum">patient.PatNum</see>.  Can be pos or neg.</summary>
		public int PatNum;
		///<summary>Foreign key to <see cref="Def.DefNum">definition.DefNum</see>.</summary>
		public int AdjType;
		///<summary>Foreign key to <see cref="Provider.ProvNum">provider.ProvNum</see>.</summary>
		public int ProvNum;
		///<summary>Note for this adjustment.</summary>
		public string AdjNote;
	}

	/*=========================================================================================
	=================================== class Adjustments ==========================================*/

	///<summary>Handles database commands related to the adjustment table in the db.</summary>
	public class Adjustments:DataClass{
		///<summary>A list of adjustments for a single patient.</summary>
		public static Adjustment[] List;
		///<summary>Current. A single row of data.</summary>
		public static Adjustment Cur;
		//<summary></summary>
		//public static Adjustment[] PaymentList;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT adjnum,adjdate,adjamt,patnum, "
				+"adjtype,provnum,adjnote"
				+" from adjustment"
				+" WHERE patnum = '"+Patients.Cur.PatNum+"' ORDER BY adjdate";
			FillTable();
			List=new Adjustment[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].AdjNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].AdjDate= PIn.PDate  (table.Rows[i][1].ToString());
				List[i].AdjAmt = PIn.PDouble(table.Rows[i][2].ToString());
				List[i].PatNum = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].AdjType= PIn.PInt   (table.Rows[i][4].ToString());
				List[i].ProvNum= PIn.PInt   (table.Rows[i][5].ToString());
				List[i].AdjNote= PIn.PString(table.Rows[i][6].ToString());
			}//end for
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE adjustment SET " 
				+ "adjdate = '"      +POut.PDate  (Cur.AdjDate)+"'"
				+ ",adjamt = '"      +POut.PDouble(Cur.AdjAmt)+"'"
				+ ",patnum = '"      +POut.PInt   (Cur.PatNum)+"'"
				+ ",adjtype = '"     +POut.PInt   (Cur.AdjType)+"'"
				+ ",provnum = '"     +POut.PInt   (Cur.ProvNum)+"'"
				+ ",adjnote = '"     +POut.PString(Cur.AdjNote)+"'"
				+" WHERE adjNum = '" +POut.PInt   (Cur.AdjNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO adjustment (adjdate,adjamt,patnum, "
				+"adjtype,provnum,adjnote) VALUES("
				+"'"+POut.PDate  (Cur.AdjDate)+"', "
				+"'"+POut.PDouble(Cur.AdjAmt)+"', "
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PInt   (Cur.AdjType)+"', "
				+"'"+POut.PInt   (Cur.ProvNum)+"', "
				+"'"+POut.PString(Cur.AdjNote)+"')";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText="DELETE FROM adjustment "
				+"WHERE adjnum = '"+Cur.AdjNum.ToString()+"'";
			NonQ(false);
		}

		///<summary>Must make sure Refresh is done first.  Returns the sum of all adjustments for this patient.  Amount might be pos or neg.</summary>
		public static double ComputeBal(){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				retVal+=List[i].AdjAmt;
			}
			return retVal;
		}
	}

	


	


}









