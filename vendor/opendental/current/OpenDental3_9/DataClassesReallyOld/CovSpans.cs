using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the covspan table in the database.</summary>
	public struct CovSpan{
		///<summary>Primary key.</summary>
		public int CovSpanNum;
		///<summary>Foreign key</summary>
		public int CovCatNum;
		///<summary>Foreign key to procedurecode.ADACode.</summary>
		public string FromCode;
		///<summary>Foreign key to procedurecode.ADACode.</summary>
		public string ToCode;
	}

	/*=========================================================================================
		=================================== class CovSpans ==========================================*/

	///<summary></summary>
	public class CovSpans:DataClass{
		///<summary></summary>
		public static CovSpan[] List;
		///<summary></summary>
		public static CovSpan Cur;
		
		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from covspan"
				+" ORDER BY FromCode";
			FillTable();
			List=new CovSpan[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].CovSpanNum  = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].CovCatNum   = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].FromCode    = PIn.PString(table.Rows[i][2].ToString());
				List[i].ToCode      = PIn.PString(table.Rows[i][3].ToString());
			}//end for
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE covspan SET "
				+ "covcatnum = '" +POut.PInt   (Cur.CovCatNum)+"'"
				+",fromcode = '"  +POut.PString(Cur.FromCode)+"'"
				+",tocode = '"    +POut.PString(Cur.ToCode)+"'"
				+" WHERE covspannum = '"+POut.PInt(Cur.CovSpanNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO covspan (covcatnum,"
				+"fromcode,tocode) VALUES("
				+"'"+POut.PInt   (Cur.CovCatNum)+"', "
				+"'"+POut.PString(Cur.FromCode)+"', "
				+"'"+POut.PString(Cur.ToCode)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText="DELETE FROM covspan"
				+" WHERE covspannum = '"+POut.PInt(Cur.CovSpanNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static int GetCat(string myADACode){
			int retVal=0;
			for(int i=0;i<List.Length;i++){
				if(String.Compare(myADACode,List[i].FromCode)>=0
					&& String.Compare(myADACode,List[i].ToCode)<=0){
					retVal=List[i].CovCatNum;
				}
			}
			return retVal;
		}

	}

	


}









