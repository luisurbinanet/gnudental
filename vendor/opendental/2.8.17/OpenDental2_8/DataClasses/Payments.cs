using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary></summary>
	public struct Payment{
		///<summary></summary>
		public int PayNum;
		///<summary></summary>
		public int PayType;
		///<summary></summary>
		public DateTime PayDate;
		///<summary></summary>
		public double PayAmt;
		///<summary></summary>
		public string CheckNum;
		///<summary></summary>
		public string BankBranch;
		///<summary></summary>
		public string PayNote;
		///<summary></summary>
		public bool IsSplit;//(between patients.  Not including discounts)
		///<summary></summary>
		public int PatNum;//Selected automatically from the splits.  Just to make reports easier
	}

	/*=========================================================================================
		=================================== class Payments ==========================================*/

	///<summary></summary>
	public class Payments:DataClass{
		///<summary></summary>
		public static Payment Cur;

		///<summary></summary>
		public static void SetCur(int myPayNum){
			cmd.CommandText =
				"SELECT * from payment"
				+" WHERE PayNum = '"+myPayNum+"'";
			FillTable();
			Cur.PayNum    =PIn.PInt   (table.Rows[0][0].ToString());
			Cur.PayType   =PIn.PInt   (table.Rows[0][1].ToString());
			Cur.PayDate   =PIn.PDate  (table.Rows[0][2].ToString());
			Cur.PayAmt    =PIn.PDouble(table.Rows[0][3].ToString());
			Cur.CheckNum  =PIn.PString(table.Rows[0][4].ToString());
			Cur.BankBranch=PIn.PString(table.Rows[0][5].ToString());
			Cur.PayNote   =PIn.PString(table.Rows[0][6].ToString());
			Cur.IsSplit   =PIn.PBool  (table.Rows[0][7].ToString());
			Cur.PatNum    =PIn.PInt   (table.Rows[0][8].ToString());
		}

		///<summary></summary>
		public static void UpdateCur(){//updates Cur
			cmd.CommandText = "UPDATE payment SET " 
				+ "paytype = '"      +POut.PInt   (Cur.PayType)+"'"
				+ ",paydate = '"     +POut.PDate  (Cur.PayDate)+"'"
				+ ",payamt = '"      +POut.PDouble(Cur.PayAmt)+"'"
				+ ",checknum = '"    +POut.PString(Cur.CheckNum)+"'"
				+ ",bankbranch = '"  +POut.PString(Cur.BankBranch)+"'"
				+ ",paynote = '"     +POut.PString(Cur.PayNote)+"'"
				+ ",issplit = '"     +POut.PBool  (Cur.IsSplit)+"'"
				+ ",patnum = '"      +POut.PInt   (Cur.PatNum)+"'"
				+" WHERE payNum = '" +POut.PInt   (Cur.PayNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){//saves Cur
			cmd.CommandText = "INSERT INTO payment (paytype,paydate,payamt, "
				+"checknum,bankbranch,paynote,issplit,patnum) VALUES("
				+"'"+POut.PInt   (Cur.PayType)+"', "
				+"'"+POut.PDate  (Cur.PayDate)+"', "
				+"'"+POut.PDouble(Cur.PayAmt)+"', "
				+"'"+POut.PString(Cur.CheckNum)+"', "
				+"'"+POut.PString(Cur.BankBranch)+"', "
				+"'"+POut.PString(Cur.PayNote)+"', "
				+"'"+POut.PBool  (Cur.IsSplit)+"', "
				+"'"+POut.PInt   (Cur.PatNum)+"')";
			NonQ(true);
			Cur.PayNum=InsertID;
			//MessageBox.Show(Cur.PayNum.ToString());
		}

		///<summary></summary>
		public static void DeleteCur(){//deletes Cur
			cmd.CommandText = "DELETE from payment WHERE payNum = '"+Cur.PayNum.ToString()+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static string GetInfo(int payNum){
			string retStr;
			SetCur(payNum);
			retStr= Defs.GetName(DefCat.PaymentTypes,Cur.PayType);
			if(Cur.IsSplit) retStr=retStr
												+"  "+Cur.PayAmt.ToString("c")
												+"  "+Cur.PayDate.ToString("d")
												+" "+Lan.g("Payments","split between patients");
			return retStr;
		}
	}

	

	

}










