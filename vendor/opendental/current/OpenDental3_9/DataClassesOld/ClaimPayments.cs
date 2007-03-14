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
		///<summary>Foreign key to deposit.DepositNum.  0 if not attached to any deposits.</summary>
		public int DepositNum;
		///<summary>Descriptive name just for reporting purposes.</summary>
		public string CarrierName;

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
			cp.DepositNum=DepositNum;
			cp.CarrierName=CarrierName;
			return cp;
		}

		///<summary></summary>
		public void Insert(){
			if(Prefs.RandomKeys){
				ClaimPaymentNum=MiscData.GetKey("claimpayment","ClaimPaymentNum");
			}
			string command= "INSERT INTO claimpayment (";
			if(Prefs.RandomKeys){
				command+="ClaimPaymentNum,";
			}
			command+="CheckDate,CheckAmt,CheckNum,"
				+"BankBranch,Note,ClinicNum,DepositNum,CarrierName) VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(ClaimPaymentNum)+"', ";
			}
			command+=
				 "'"+POut.PDate  (CheckDate)+"', "
				+"'"+POut.PDouble(CheckAmt)+"', "
				+"'"+POut.PString(CheckNum)+"', "
				+"'"+POut.PString(BankBranch)+"', "
				+"'"+POut.PString(Note)+"', "
				+"'"+POut.PInt   (ClinicNum)+"', "
				+"'"+POut.PInt   (DepositNum)+"', "
				+"'"+POut.PString(CarrierName)+"')";
			DataConnection dcon=new DataConnection();
			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				ClaimPaymentNum=dcon.InsertID;
			}
		}

		///<summary>If trying to change the amount and attached to a deposit, it will throw an error, so surround with try catch.</summary>
		public void Update(){
			string command="SELECT DepositNum,CheckAmt FROM claimpayment "
				+"WHERE ClaimPaymentNum="+POut.PInt(ClaimPaymentNum);
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count==0){
				return;
			}
			if(table.Rows[0][0].ToString()!="0"//if claimpayment is already attached to a deposit
				&& PIn.PDouble(table.Rows[0][1].ToString())!=CheckAmt)//and checkAmt changes
			{
				throw new ApplicationException(Lan.g("ClaimPayments","Not allowed to change the amount on checks attached to deposits."));
			}
			command="UPDATE claimpayment SET "
				+"checkdate = '"   +POut.PDate  (CheckDate)+"' "
				+",checkamt = '"   +POut.PDouble(CheckAmt)+"' "
				+",checknum = '"   +POut.PString(CheckNum)+"' "
				+",bankbranch = '" +POut.PString(BankBranch)+"' "
				+",note = '"       +POut.PString(Note)+"' "
				+",ClinicNum = '"  +POut.PInt   (ClinicNum)+"' "
				+",DepositNum = '" +POut.PInt   (DepositNum)+"' "
				+",CarrierName = '"+POut.PString(CarrierName)+"' "
				+"WHERE ClaimPaymentnum = '"+POut.PInt   (ClaimPaymentNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			
 			dcon.NonQ(command);
		}

		///<summary>Surround by try catch, because it will throw an exception if trying to delete a claimpayment attached to a deposit.</summary>
		public void Delete(){
			string command="SELECT DepositNum FROM claimpayment "
				+"WHERE ClaimPaymentNum="+POut.PInt(ClaimPaymentNum);
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count==0){
				return;
			}
			if(table.Rows[0][0].ToString()!="0"){//if claimpayment is already attached to a deposit
				throw new ApplicationException(Lan.g("ClaimPayments","Not allowed to delete a payment attached to a deposit."));
			}
			command= "UPDATE claimproc SET "
				+"ClaimPaymentNum=0 "
				+"WHERE claimpaymentNum="+POut.PInt(ClaimPaymentNum);
			//MessageBox.Show(cmd.CommandText);
			dcon.NonQ(command);
			command= "DELETE FROM claimpayment "
				+"WHERE ClaimPaymentnum ="+POut.PInt(ClaimPaymentNum);
			//MessageBox.Show(cmd.CommandText);
 			dcon.NonQ(command);
		}


	}

	/*=========================================================================================
		=================================== class ClaimPayments ==========================================*/
	///<summary></summary>
	public class ClaimPayments{

		///<summary></summary>
		public static ClaimPayment[] GetForClaim(){	
			string command=
				"SELECT claimpayment.claimpaymentnum,claimpayment.checkdate,SUM(claimproc.inspayamt)"
				//warning.  The payAmt is not the actual amount of the check, but only of a portion of it
				+",claimpayment.checknum,claimpayment.bankbranch,claimpayment.note"
				+",claimpayment.ClinicNum,DepositNum,CarrierName"
				+" FROM claimpayment,claimproc"
				+" WHERE claimpayment.claimpaymentnum = claimproc.claimpaymentnum"
				+" && claimproc.claimnum = '"+Claims.Cur.ClaimNum.ToString()+"'"
				+" GROUP BY claimpayment.claimpaymentnum,claimproc.claimnum";
			return RefreshAndFill(command);
		}

		///<summary>Gets all unattached claimpayments for display in a new deposit.  Excludes payments before dateStart.</summary>
		public static ClaimPayment[] GetForDeposit(DateTime dateStart,int clinicNum){	
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
		public static ClaimPayment[] GetForDeposit(int depositNum){	
			string command=
				"SELECT ClaimPaymentNum,CheckDate,CheckAmt,"
				+"Checknum,BankBranch,Note,"
				+"ClinicNum,DepositNum,CarrierName "
				+"FROM claimpayment "
				+"WHERE DepositNum = "+POut.PInt(depositNum);
			return RefreshAndFill(command);
		}

		private static ClaimPayment[] RefreshAndFill(string command){
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			ClaimPayment[] List=new ClaimPayment[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new ClaimPayment();
				List[i].ClaimPaymentNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].CheckDate      = PIn.PDate  (table.Rows[i][1].ToString());
				List[i].CheckAmt       = PIn.PDouble(table.Rows[i][2].ToString());
				List[i].CheckNum       = PIn.PString(table.Rows[i][3].ToString());
				List[i].BankBranch     = PIn.PString(table.Rows[i][4].ToString());
				List[i].Note           = PIn.PString(table.Rows[i][5].ToString());
				List[i].ClinicNum      = PIn.PInt   (table.Rows[i][6].ToString());
				List[i].DepositNum     = PIn.PInt   (table.Rows[i][7].ToString());
				List[i].CarrierName    = PIn.PString(table.Rows[i][8].ToString());
			}
			return List;
		}

		
	}

	

	


}









