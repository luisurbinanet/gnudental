using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the paysplit table in the database.</summary>
	public struct PaySplit{
		///<summary>Primary key.</summary>
		public int SplitNum;
		///<summary>Amount of split.</summary>
		public double SplitAmt;
		///<summary>Foreign key to patient.PatNum.</summary>
		public int PatNum;
		///<summary>Procedure date.  Not necessarily when the payment was made.  This field will be eliminated when we add a ProcNum field because allowing splits to have different dates makes accurate aging almost impossible.</summary>
		public DateTime ProcDate;//
		///<summary>Foreign key to payment.PayNum.  Every paysplit must be linked to a payment.</summary>
		public int PayNum;
		///<summary>True if discount rather than a payment.</summary>
		public bool IsDiscount;
		///<summary>Foreign key to definition.DefNum if a discount.</summary>
		public int DiscountType;
		///<summary>Foreign key to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>Foreign key to payplan.PayPlanNum.  0 if not attached to a payplan.</summary>
		public int PayPlanNum;
	}

	/*=========================================================================================
	=================================== class PaySplits ==========================================*/

	///<summary></summary>
	public class PaySplits:DataClass{
		///<summary></summary>
		public static PaySplit[] List;
		///<summary>A list of paysplits for a single payment.</summary>
		public static PaySplit[] PaymentList;
		///<summary></summary>
		public static PaySplit Cur;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from paysplit"
				+" WHERE patnum = '"+Patients.Cur.PatNum+"' ORDER BY procdate";
			FillTable();
			List=new PaySplit[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].SplitNum    = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].SplitAmt    = PIn.PDouble(table.Rows[i][1].ToString());
				List[i].PatNum      = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].ProcDate    = PIn.PDate  (table.Rows[i][3].ToString());
				List[i].PayNum      = PIn.PInt   (table.Rows[i][4].ToString());
				List[i].IsDiscount  = PIn.PBool  (table.Rows[i][5].ToString());
				List[i].DiscountType= PIn.PInt   (table.Rows[i][6].ToString());
				List[i].ProvNum     = PIn.PInt   (table.Rows[i][7].ToString());
				List[i].PayPlanNum  = PIn.PInt   (table.Rows[i][8].ToString());
			}//end for
		}

		///<summary></summary>
		public static void UpdateCur(){//updates Cur
			cmd.CommandText = "UPDATE paysplit SET " 
				+ "splitamt = '"     +POut.PDouble(Cur.SplitAmt)+"'"
				+ ",patnum = '"      +POut.PInt   (Cur.PatNum)+"'"
				+ ",procdate = '"    +POut.PDate  (Cur.ProcDate)+"'"
				+ ",paynum = '"      +POut.PInt   (Cur.PayNum)+"'"
				+ ",isdiscount = '"  +POut.PBool  (Cur.IsDiscount)+"'"
				+ ",discounttype = '"+POut.PInt   (Cur.DiscountType)+"'"
				+ ",provnum = '"     +POut.PInt   (Cur.ProvNum)+"'"
				+ ",payplannum = '"  +POut.PInt   (Cur.PayPlanNum)+"'"
				+" WHERE splitNum = '" +POut.PInt (Cur.SplitNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){//saves Cur
			cmd.CommandText = "INSERT INTO paysplit (splitamt,patnum,procdate, "
				+"paynum,isdiscount,discounttype,provnum,payplannum) VALUES("
				+"'"+POut.PDouble(Cur.SplitAmt)+"', "
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PDate  (Cur.ProcDate)+"', "
				+"'"+POut.PInt   (Cur.PayNum)+"', "
				+"'"+POut.PBool  (Cur.IsDiscount)+"', "
				+"'"+POut.PInt   (Cur.DiscountType)+"', "
				+"'"+POut.PInt   (Cur.ProvNum)+"', "
				+"'"+POut.PInt   (Cur.PayPlanNum)+"')";
			NonQ(true);
			Cur.SplitNum=InsertID;
		}

		///<summary></summary>
		public static void RefreshPaymentList(int payNum){
			cmd.CommandText =
				"SELECT * from paysplit"
				+" WHERE paynum = '"+payNum+"'";
			FillTable();
			PaymentList=new PaySplit[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				PaymentList[i].SplitNum    = PIn.PInt   (table.Rows[i][0].ToString());
				PaymentList[i].SplitAmt    = PIn.PDouble(table.Rows[i][1].ToString());
				PaymentList[i].PatNum      = PIn.PInt   (table.Rows[i][2].ToString());
				PaymentList[i].ProcDate    = PIn.PDate  (table.Rows[i][3].ToString());
				PaymentList[i].PayNum      = PIn.PInt   (table.Rows[i][4].ToString());
				PaymentList[i].IsDiscount  = PIn.PBool  (table.Rows[i][5].ToString());
				PaymentList[i].DiscountType= PIn.PInt   (table.Rows[i][6].ToString());
				PaymentList[i].ProvNum     = PIn.PInt   (table.Rows[i][7].ToString());
				PaymentList[i].PayPlanNum  = PIn.PInt   (table.Rows[i][8].ToString());
			}//end for
		}

		///<summary></summary>
		public static void DeleteCur(){//deletes Cur
			//Cur=List[Selected];
			//PutBal(Cur.PatNum,Cur.ProcDate,-Cur.SplitAmt);
			cmd.CommandText = "DELETE from paysplit WHERE splitNum = '"+Cur.SplitNum.ToString()+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static double ComputeBal(){//must make sure Refresh is done first
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				retVal+=List[i].SplitAmt;
			}
			return retVal;
		}

	}

	

	


}










