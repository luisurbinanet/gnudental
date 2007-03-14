using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the claimpayment table in the database.</summary>
	///<remarks>Each row represents a single check from the insurance company.  The amount may be split between patients using claimprocs.  The amount of the check must always exactly equal the sum of all the claimprocs attached to it.  There may be only one claimproc.</remarks>
	public struct ClaimPayment{
		///<summary>Primary key.</summary>
		public int ClaimPaymentNum;
		///<summary>Date the check was entered into this system, not the date on the check.</summary>
		public DateTime CheckDate;
		///<summary>The amount of the check.</summary>
		public Double CheckAmt;
		///<summary>The check number.</summary>
		public string CheckNum;
		///<summary>Bank and branch.</summary>
		public string BankBranch;
		///<summary>Note for this check if needed.</summary>
		public string Note;
	}

	/*=========================================================================================
		=================================== class ClaimPayments ==========================================*/
	///<summary></summary>
	public class ClaimPayments:DataClass{
		///<summary></summary>
		public static ClaimPayment[] List;
		///<summary></summary>
		public static ClaimPayment Cur;

		///<summary></summary>
		public static void GetForClaim(){	
		//public static void GetCheck(int claimPaymentNum){
			cmd.CommandText =
				"SELECT claimpayment.claimpaymentnum,claimpayment.checkdate,SUM(claimproc.inspayamt)"
				//claimpayment.checkamt"
				+",claimpayment.checknum,claimpayment.bankbranch,claimpayment.note"
				+" FROM claimpayment,claimproc"
				+" WHERE claimpayment.claimpaymentnum = claimproc.claimpaymentnum"
				+" && claimproc.claimnum = '"+Claims.Cur.ClaimNum.ToString()+"'"
				+" GROUP BY claimpayment.claimpaymentnum,claimproc.claimnum";
				//+" WHERE ClaimPaymentnum = '"+claimPaymentNum+"'";
			FillTable();
			List=new ClaimPayment[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].ClaimPaymentNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].CheckDate    = PIn.PDate  (table.Rows[i][1].ToString());
				//warning.  This is not the actual amount of the check, but only of a portion of it
				List[i].CheckAmt     = PIn.PDouble(table.Rows[i][2].ToString());
				List[i].CheckNum     = PIn.PString(table.Rows[i][3].ToString());
				List[i].BankBranch   = PIn.PString(table.Rows[i][4].ToString());
				List[i].Note         = PIn.PString(table.Rows[i][5].ToString());
			}
			//MessageBox.Show(List.Length.ToString()+cmd.CommandText);
			//if(List.Length==1)
			//	Cur=List[0];
			//else
			//	Cur=new ClaimPayment();
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO claimpayment (checkdate,checkamt,checknum,"
				+"bankbranch,note) VALUES("
				+"'"+POut.PDate  (Cur.CheckDate)+"', "
				+"'"+POut.PDouble(Cur.CheckAmt)+"', "
				+"'"+POut.PString(Cur.CheckNum)+"', "
				+"'"+POut.PString(Cur.BankBranch)+"', "
				+"'"+POut.PString(Cur.Note)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.ClaimPaymentNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE claimpayment SET "
				+"checkdate = '"   +POut.PDate  (Cur.CheckDate)+"' "
				+",checkamt = '"   +POut.PDouble(Cur.CheckAmt)+"' "
				+",checknum = '"   +POut.PString(Cur.CheckNum)+"' "
				+",bankbranch = '" +POut.PString(Cur.BankBranch)+"' "
				+",note = '"       +POut.PString(Cur.Note)+"' "
				+"WHERE ClaimPaymentnum = '"+POut.PInt   (Cur.ClaimPaymentNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM claimpayment "
				+"WHERE ClaimPaymentnum = '"+POut.PInt(Cur.ClaimPaymentNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}
	}//end class ClaimPayments

	

	


}









