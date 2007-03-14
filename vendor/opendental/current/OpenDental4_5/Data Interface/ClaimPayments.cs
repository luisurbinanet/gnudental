using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class ClaimPayments {

		///<summary></summary>
		public static ClaimPayment[] GetForClaim(int ClaimNum) {
			string command=
				"SELECT claimpayment.claimpaymentnum,claimpayment.checkdate,SUM(claimproc.inspayamt)"
				//warning.  The payAmt is not the actual amount of the check, but only of a portion of it
				+",claimpayment.checknum,claimpayment.bankbranch,claimpayment.note"
				+",claimpayment.ClinicNum,DepositNum,CarrierName"
				+" FROM claimpayment,claimproc"
				+" WHERE claimpayment.claimpaymentnum = claimproc.claimpaymentnum"
				+" && claimproc.claimnum = '"+POut.PInt(ClaimNum)+"'"
				+" GROUP BY claimpayment.claimpaymentnum,claimproc.claimnum";
			return RefreshAndFill(command);
		}

		///<summary>Gets all unattached claimpayments for display in a new deposit.  Excludes payments before dateStart.</summary>
		public static ClaimPayment[] GetForDeposit(DateTime dateStart,int clinicNum) {
			string command=
				"SELECT ClaimPaymentNum,CheckDate,CheckAmt,"
				+"Checknum,BankBranch,Note,"
				+"ClinicNum,DepositNum,CarrierName "
				+"FROM claimpayment "
				+"WHERE DepositNum = 0 "
				+"AND CheckDate >= '"+POut.PDate(dateStart)+"' "
				+"AND ClinicNum="+POut.PInt(clinicNum);
			return RefreshAndFill(command);
		}

		///<summary>Gets all claimpayments for one specific deposit.</summary>
		public static ClaimPayment[] GetForDeposit(int depositNum) {
			string command=
				"SELECT ClaimPaymentNum,CheckDate,CheckAmt,"
				+"Checknum,BankBranch,Note,"
				+"ClinicNum,DepositNum,CarrierName "
				+"FROM claimpayment "
				+"WHERE DepositNum = "+POut.PInt(depositNum);
			return RefreshAndFill(command);
		}

		private static ClaimPayment[] RefreshAndFill(string command) {
			DataTable table=General.GetTable(command);
			ClaimPayment[] List=new ClaimPayment[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new ClaimPayment();
				List[i].ClaimPaymentNum= PIn.PInt(table.Rows[i][0].ToString());
				List[i].CheckDate      = PIn.PDate(table.Rows[i][1].ToString());
				List[i].CheckAmt       = PIn.PDouble(table.Rows[i][2].ToString());
				List[i].CheckNum       = PIn.PString(table.Rows[i][3].ToString());
				List[i].BankBranch     = PIn.PString(table.Rows[i][4].ToString());
				List[i].Note           = PIn.PString(table.Rows[i][5].ToString());
				List[i].ClinicNum      = PIn.PInt(table.Rows[i][6].ToString());
				List[i].DepositNum     = PIn.PInt(table.Rows[i][7].ToString());
				List[i].CarrierName    = PIn.PString(table.Rows[i][8].ToString());
			}
			return List;
		}


		///<summary></summary>
		public static void Insert(ClaimPayment cp){
			if(PrefB.RandomKeys){
				cp.ClaimPaymentNum=MiscData.GetKey("claimpayment","ClaimPaymentNum");
			}
			string command= "INSERT INTO claimpayment (";
			if(PrefB.RandomKeys){
				command+="ClaimPaymentNum,";
			}
			command+="CheckDate,CheckAmt,CheckNum,"
				+"BankBranch,Note,ClinicNum,DepositNum,CarrierName) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(cp.ClaimPaymentNum)+"', ";
			}
			command+=
				 "'"+POut.PDate  (cp.CheckDate)+"', "
				+"'"+POut.PDouble(cp.CheckAmt)+"', "
				+"'"+POut.PString(cp.CheckNum)+"', "
				+"'"+POut.PString(cp.BankBranch)+"', "
				+"'"+POut.PString(cp.Note)+"', "
				+"'"+POut.PInt   (cp.ClinicNum)+"', "
				+"'"+POut.PInt   (cp.DepositNum)+"', "
				+"'"+POut.PString(cp.CarrierName)+"')";
			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				cp.ClaimPaymentNum=General.NonQ(command,true);
			}
		}

		///<summary>If trying to change the amount and attached to a deposit, it will throw an error, so surround with try catch.</summary>
		public static void Update(ClaimPayment cp){
			string command="SELECT DepositNum,CheckAmt FROM claimpayment "
				+"WHERE ClaimPaymentNum="+POut.PInt(cp.ClaimPaymentNum);
			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0){
				return;
			}
			if(table.Rows[0][0].ToString()!="0"//if claimpayment is already attached to a deposit
				&& PIn.PDouble(table.Rows[0][1].ToString())!=cp.CheckAmt)//and checkAmt changes
			{
				throw new ApplicationException(Lan.g("ClaimPayments","Not allowed to change the amount on checks attached to deposits."));
			}
			command="UPDATE claimpayment SET "
				+"checkdate = '"   +POut.PDate  (cp.CheckDate)+"' "
				+",checkamt = '"   +POut.PDouble(cp.CheckAmt)+"' "
				+",checknum = '"   +POut.PString(cp.CheckNum)+"' "
				+",bankbranch = '" +POut.PString(cp.BankBranch)+"' "
				+",note = '"       +POut.PString(cp.Note)+"' "
				+",ClinicNum = '"  +POut.PInt   (cp.ClinicNum)+"' "
				+",DepositNum = '" +POut.PInt   (cp.DepositNum)+"' "
				+",CarrierName = '"+POut.PString(cp.CarrierName)+"' "
				+"WHERE ClaimPaymentnum = '"+POut.PInt   (cp.ClaimPaymentNum)+"'";
			//MessageBox.Show(string command);
 			General.NonQ(command);
		}

		///<summary>Surround by try catch, because it will throw an exception if trying to delete a claimpayment attached to a deposit.</summary>
		public static void Delete(ClaimPayment cp){
			string command="SELECT DepositNum FROM claimpayment "
				+"WHERE ClaimPaymentNum="+POut.PInt(cp.ClaimPaymentNum);
			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0){
				return;
			}
			if(table.Rows[0][0].ToString()!="0"){//if claimpayment is already attached to a deposit
				throw new ApplicationException(Lan.g("ClaimPayments","Not allowed to delete a payment attached to a deposit."));
			}
			command= "UPDATE claimproc SET "
				+"ClaimPaymentNum=0 "
				+"WHERE claimpaymentNum="+POut.PInt(cp.ClaimPaymentNum);
			//MessageBox.Show(string command);
			General.NonQ(command);
			command= "DELETE FROM claimpayment "
				+"WHERE ClaimPaymentnum ="+POut.PInt(cp.ClaimPaymentNum);
			//MessageBox.Show(string command);
 			General.NonQ(command);
		}


	
		
	}

	

	


}









