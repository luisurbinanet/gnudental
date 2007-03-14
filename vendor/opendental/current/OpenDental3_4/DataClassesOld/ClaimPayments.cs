using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the claimpayment table in the database.  Each row represents a single check from the insurance company.  The amount may be split between patients using claimprocs.  The amount of the check must always exactly equal the sum of all the claimprocs attached to it.  There might be only one claimproc.</summary>
	public class ClaimPayment{
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
		///<summary>Foreign key to clinic.ClinicNum</summary>
		public int ClinicNum;

		///<summary>Returns a copy of this ClaimPayment.</summary>
		public ClaimPayment Copy(){
			ClaimPayment cp=new ClaimPayment();
			cp.ClaimPaymentNum =ClaimPaymentNum;
			cp.CheckDate=CheckDate;
			cp.CheckAmt=CheckAmt;
			cp.CheckNum=CheckNum;
			cp.BankBranch=BankBranch;
			cp.Note=Note;
			cp.ClinicNum=ClinicNum;
			return cp;
		}

		///<summary></summary>
		public void Insert(){
			string command="INSERT INTO claimpayment (checkdate,checkamt,checknum,"
				+"bankbranch,note,ClinicNum) VALUES("
				+"'"+POut.PDate  (CheckDate)+"', "
				+"'"+POut.PDouble(CheckAmt)+"', "
				+"'"+POut.PString(CheckNum)+"', "
				+"'"+POut.PString(BankBranch)+"', "
				+"'"+POut.PString(Note)+"', "
				+"'"+POut.PInt   (ClinicNum)+"')";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			ClaimPaymentNum=dcon.InsertID;
		}

		///<summary></summary>
		public void Update(){
			string command = "UPDATE claimpayment SET "
				+"checkdate = '"   +POut.PDate  (CheckDate)+"' "
				+",checkamt = '"   +POut.PDouble(CheckAmt)+"' "
				+",checknum = '"   +POut.PString(CheckNum)+"' "
				+",bankbranch = '" +POut.PString(BankBranch)+"' "
				+",note = '"       +POut.PString(Note)+"' "
				+",ClinicNum = '"  +POut.PInt   (ClinicNum)+"' "
				+"WHERE ClaimPaymentnum = '"+POut.PInt   (ClaimPaymentNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Delete(){
			string command= "DELETE FROM claimpayment "
				+"WHERE ClaimPaymentnum = '"+POut.PInt(ClaimPaymentNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}


	}

	/*=========================================================================================
		=================================== class ClaimPayments ==========================================*/
	///<summary></summary>
	public class ClaimPayments{
		//<summary></summary>
		//public static ClaimPayment[] List;
		//<summary></summary>
		//public static ClaimPayment Cur;

		///<summary></summary>
		public static ClaimPayment[] GetForClaim(){	
		//public static void GetCheck(int claimPaymentNum){
			string command=
				"SELECT claimpayment.claimpaymentnum,claimpayment.checkdate,SUM(claimproc.inspayamt)"
				//claimpayment.checkamt"
				+",claimpayment.checknum,claimpayment.bankbranch,claimpayment.note"
				+",claimpayment.ClinicNum"
				+" FROM claimpayment,claimproc"
				+" WHERE claimpayment.claimpaymentnum = claimproc.claimpaymentnum"
				+" && claimproc.claimnum = '"+Claims.Cur.ClaimNum.ToString()+"'"
				+" GROUP BY claimpayment.claimpaymentnum,claimproc.claimnum";
				//+" WHERE ClaimPaymentnum = '"+claimPaymentNum+"'";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			ClaimPayment[] List=new ClaimPayment[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new ClaimPayment();
				List[i].ClaimPaymentNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].CheckDate    = PIn.PDate  (table.Rows[i][1].ToString());
				//warning.  This is not the actual amount of the check, but only of a portion of it
				List[i].CheckAmt     = PIn.PDouble(table.Rows[i][2].ToString());
				List[i].CheckNum     = PIn.PString(table.Rows[i][3].ToString());
				List[i].BankBranch   = PIn.PString(table.Rows[i][4].ToString());
				List[i].Note         = PIn.PString(table.Rows[i][5].ToString());
				List[i].ClinicNum    = PIn.PInt   (table.Rows[i][6].ToString());
			}
			return List;
			//MessageBox.Show(List.Length.ToString()+cmd.CommandText);
			//if(List.Length==1)
			//	Cur=List[0];
			//else
			//	Cur=new ClaimPayment();
		}

		
	}

	

	


}









