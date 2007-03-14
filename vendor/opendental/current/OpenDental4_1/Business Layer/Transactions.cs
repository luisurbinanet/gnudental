using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the Transaction table in the database.  Used in the Transactioning section in chart of Transactions.</summary>
	public class Transaction{
		///<summary>Primary key.</summary>
		public int TransactionNum;
		///<summary>Not user editable.  Server time.</summary>
		public DateTime DateTimeEntry;
		///<summary>Foreign key.</summary>
		public int UserNum;
		///<summary>Foreign key.  Will eventually be replaced by a source document table, and deposits will just be one of many types.</summary>
		public int DepositNum;
		///<summary>FK to payment.PayNum.  Like DepositNum, it will eventually be replaced by a source document table, and payments will just be one of many types.</summary>
		public int PayNum;

		///<summary></summary>
		public Transaction Copy() {
			Transaction t=new Transaction();
			t.TransactionNum=TransactionNum;
			t.DateTimeEntry=DateTimeEntry;
			t.UserNum=UserNum;
			t.DepositNum=DepositNum;
			t.PayNum=PayNum;
			return t;
		}

		///<summary></summary>
		public void Insert() {
			if(Prefs.RandomKeys) {
				TransactionNum=MiscData.GetKey("transaction","TransactionNum");
			}
			string command="INSERT INTO transaction (";
			if(Prefs.RandomKeys) {
				command+="TransactionNum,";
			}
			command+="DateTimeEntry,UserNum,DepositNum,PayNum) VALUES(";
			if(Prefs.RandomKeys) {
				command+="'"+POut.PInt(TransactionNum)+"', ";
			}
			command+=
				 "NOW(), "//DateTimeEntry set to current server time
				+"'"+POut.PInt   (UserNum)+"', "
				+"'"+POut.PInt   (DepositNum)+"', "
				+"'"+POut.PInt   (PayNum)+"')";
			DataConnection dcon=new DataConnection();
			if(Prefs.RandomKeys) {
				dcon.NonQ(command);
			}
			else {
				dcon.NonQ(command,true);
				TransactionNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void Update() {
			string command= "UPDATE transaction SET "
				+"DateTimeEntry = '" +POut.PDateT (DateTimeEntry)+"' "
				+",UserNum = '"      +POut.PInt   (UserNum)+"' "
				+",DepositNum = '"   +POut.PInt   (DepositNum)+"' "
				+",PayNum = '"       +POut.PInt   (PayNum)+"' "
				+"WHERE TransactionNum = '"+POut.PInt(TransactionNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary>Also deletes all journal entries for the transaction.  Will later throw an error if journal entries attached to any reconciles.  Be sure to surround with try-catch.</summary>
		public void Delete() {
			string command="DELETE FROM journalentry WHERE TransactionNum="+POut.PInt(TransactionNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
			command= "DELETE FROM transaction WHERE TransactionNum = "+POut.PInt(TransactionNum);
			dcon.NonQ(command);
		}

	}

	/*=========================================================================================
		=================================== class Transactions ==========================================*/

	///<summary></summary>
	public class Transactions{

		///<summary>Since transactions are always viewed individually, this function returns one transaction</summary>
		public static Transaction GetTrans(int transactionNum){
			string command=
				"SELECT * FROM transaction "
				+"WHERE TransactionNum="+POut.PInt(transactionNum);
			return RefreshAndFill(command);
		}

		///<summary>For now, all transactions are retrieved singly.  Might return null if db is inconsistent.</summary>
		private static Transaction RefreshAndFill(string command){
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count==0){
				return null;
			}
			Transaction trans=new Transaction();
			trans.TransactionNum= PIn.PInt   (table.Rows[0][0].ToString());
			trans.DateTimeEntry = PIn.PDateT (table.Rows[0][1].ToString());
			trans.UserNum       = PIn.PInt   (table.Rows[0][2].ToString());
			trans.DepositNum    = PIn.PInt   (table.Rows[0][3].ToString());
			trans.PayNum        = PIn.PInt   (table.Rows[0][4].ToString());
			return trans;
		}

		///<summary>Gets one transaction directly from the database which has this deposit attached to it.  If none exist, then returns null.</summary>
		public static Transaction GetAttachedToDeposit(int depositNum){
			string command=
				"SELECT * FROM transaction "
				+"WHERE DepositNum="+POut.PInt(depositNum);
			return RefreshAndFill(command);
		}

		///<summary>Gets one transaction directly from the database which has this payment attached to it.  If none exist, then returns null.</summary>
		public static Transaction GetAttachedToPayment(int payNum) {
			string command=
				"SELECT * FROM transaction "
				+"WHERE PayNum="+POut.PInt(payNum);
			return RefreshAndFill(command);
		}

		///<summary></summary>
		public static bool IsReconciled(Transaction trans){
			string command="SELECT COUNT(*) FROM journalentry WHERE ReconcileNum !=0"
				+" AND TransactionNum="+POut.PInt(trans.TransactionNum);
			DataConnection dcon=new DataConnection();
			if(dcon.GetCount(command)=="0") {
				return false;
			}
			return true;
		}



	}

	
}




