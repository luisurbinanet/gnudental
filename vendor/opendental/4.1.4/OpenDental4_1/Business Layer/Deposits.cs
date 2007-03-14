using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the deposit table in the database.</summary>
	public class Deposit{
		///<summary>Primary key.</summary>
		public int DepositNum;
		///<summary>The date of the deposit.</summary>
		public DateTime DateDeposit;
		///<summary>User editable.  Usually includes name on the account and account number.  Possibly the bank name as well.</summary>
		public string BankAccountInfo;
		///<summary>Total amount of the deposit. User not allowed to directly edit.</summary>
		public double Amount;
		
		///<summary></summary>
		public Deposit Copy(){
			Deposit d=new Deposit();
			d.DepositNum=DepositNum;
			d.DateDeposit=DateDeposit;
			d.BankAccountInfo=BankAccountInfo;
			d.Amount=Amount;
			return d;
		}

		///<summary></summary>
		public void Update(){
			string command= "UPDATE deposit SET "
				+"DateDeposit = '"     +POut.PDate  (DateDeposit)+"'"
				+",BankAccountInfo = '"+POut.PString(BankAccountInfo)+"'"
				+",Amount = '"         +POut.PDouble(Amount)+"'"
				+" WHERE DepositNum ='"+POut.PInt   (DepositNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Insert(){
			if(Prefs.RandomKeys){
				DepositNum=MiscData.GetKey("deposit","DepositNum");
			}
			string command= "INSERT INTO deposit (";
			if(Prefs.RandomKeys){
				command+="DepositNum,";
			}
			command+="DateDeposit,BankAccountInfo,Amount) VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(DepositNum)+"', ";
			}
			command+=
				 "'"+POut.PDate  (DateDeposit)+"', "
				+"'"+POut.PString(BankAccountInfo)+"', "
				+"'"+POut.PDouble(Amount)+"')";
			DataConnection dcon=new DataConnection();
 			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				DepositNum=dcon.InsertID;
			}
		}

		/*//<summary></summary>
		public void InsertOrUpdate(bool isNew){
			if(isNew){
				Insert();
			}
			else{
				Update();
			}
		}*/

		///<summary>Also handles detaching all payments and claimpayments.  Throws exception if deposit is attached as a source document to a transaction.  The program should have detached the deposit from the transaction ahead of time, so I would never expect the program to throw this exception unless there was a bug.</summary>
		public void Delete(){
			//check dependencies
			string command="";
			DataConnection dcon=new DataConnection();
			if(DepositNum !=0){
				command="SELECT COUNT(*) FROM transaction WHERE DepositNum ="+POut.PInt(DepositNum);
				if(PIn.PInt(dcon.GetCount(command))>0) {
					throw new ApplicationException(Lan.g("Deposits","Cannot delete deposit because it is attached to a transaction."));
				}
			}
			/*/check claimpayment 
			command="SELECT COUNT(*) FROM claimpayment WHERE DepositNum ="+POut.PInt(DepositNum);
			if(PIn.PInt(dcon.GetCount(command))>0){
				throw new InvalidProgramException(Lan.g("Deposits","Cannot delete deposit because it has payments attached"));
			}*/
			//ready to delete
			command="UPDATE payment SET DepositNum=0 WHERE DepositNum="+POut.PInt(DepositNum);
			dcon.NonQ(command);
			command="UPDATE claimpayment SET DepositNum=0 WHERE DepositNum="+POut.PInt(DepositNum);
			dcon.NonQ(command);
			command= "DELETE FROM deposit WHERE DepositNum="+POut.PInt(DepositNum);
 			dcon.NonQ(command);
		}


	}

	/*=========================================================================================
		=================================== class Deposits ==========================================*/

	///<summary></summary>
	public class Deposits{
			
		///<summary>Gets all Deposits, ordered by date.  </summary>
		public static Deposit[] Refresh(){
			string command="SELECT * FROM deposit "
				+"ORDER BY DateDeposit";
			return RefreshAndFill(command);
		}

		///<summary>Gets only Deposits which are not attached to transactions.</summary>
		public static Deposit[] GetUnattached() {
			string command="SELECT * FROM deposit "
				+"WHERE NOT EXISTS(SELECT * FROM transaction WHERE deposit.DepositNum=transaction.DepositNum) "
				+"ORDER BY DateDeposit";
			return RefreshAndFill(command);
		}

		///<summary>Gets a single deposit directly from the database.</summary>
		public static Deposit GetOne(int depositNum) {
			string command="SELECT * FROM deposit "
				+"WHERE DepositNum="+POut.PInt(depositNum);
			return RefreshAndFill(command)[0];
		}

		private static Deposit[] RefreshAndFill(string command){
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			Deposit[] List=new Deposit[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new Deposit();
				List[i].DepositNum     = PIn.PInt(table.Rows[i][0].ToString());
				List[i].DateDeposit    = PIn.PDate(table.Rows[i][1].ToString());
				List[i].BankAccountInfo= PIn.PString(table.Rows[i][2].ToString());
				List[i].Amount         = PIn.PDouble(table.Rows[i][3].ToString());
			}
			return List;
		}



	
	}

	

	


}




















