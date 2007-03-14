using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Used in accounting to represent a single credit or debit entry.  There will always be at least 2 journal enties attached to every transaction.  All transactions balance to 0.</summary>
	public class JournalEntry{
		///<summary>Primary key.</summary>
		public int JournalEntryNum;
		///<summary>FK to transaction.TransactionNum</summary>
		public int TransactionNum;
		///<summary>FK to account.AccountNum</summary>
		public int AccountNum;
		///<summary>Always the same for all journal entries within one transaction.</summary>
		public DateTime DateDisplayed;
		///<summary>Negative numbers never allowed.</summary>
		public double DebitAmt;
		///<summary>Negative numbers never allowed.</summary>
		public double CreditAmt;
		///<summary>.</summary>
		public string Memo;
		///<summary>A human-readable description of the splits.  Used only for display purposes.</summary>
		public string Splits;
		///<summary>Any user-defined string.  Usually a check number, but can also be D for deposit, Adj, etc.</summary>
		public string CheckNumber;
		///<summary>FK to reconcile.ReconcileNum. 0 if not attached to a reconcile. Not allowed to alter amounts if attached.</summary>
		public int ReconcileNum;

		///<summary></summary>
		public JournalEntry Copy() {
			JournalEntry j=new JournalEntry();
			j.JournalEntryNum=JournalEntryNum;
			j.TransactionNum=TransactionNum;
			j.AccountNum=AccountNum;
			j.DateDisplayed=DateDisplayed;
			j.DebitAmt=DebitAmt;
			j.CreditAmt=CreditAmt;
			j.Memo=Memo;
			j.Splits=Splits;
			j.CheckNumber=CheckNumber;
			j.ReconcileNum=ReconcileNum;
			return j;
		}

		///<summary></summary>
		public void Insert() {
			if(DebitAmt<0 || CreditAmt<0){
				throw new ApplicationException(Lan.g(this,"Error. Credit and debit must both be positive."));
			}
			if(Prefs.RandomKeys) {
				JournalEntryNum=MiscData.GetKey("journalentry","JournalEntryNum");
			}
			string command="INSERT INTO journalentry (";
			if(Prefs.RandomKeys) {
				command+="JournalEntryNum,";
			}
			command+="TransactionNum,AccountNum,DateDisplayed,DebitAmt,CreditAmt,Memo,Splits,CheckNumber,"
				+"ReconcileNum) VALUES(";
			if(Prefs.RandomKeys) {
				command+="'"+POut.PInt(JournalEntryNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (TransactionNum)+"', "
				+"'"+POut.PInt   (AccountNum)+"', "
				+"'"+POut.PDate  (DateDisplayed)+"', "
				+"'"+POut.PDouble(DebitAmt)+"', "
				+"'"+POut.PDouble(CreditAmt)+"', "
				+"'"+POut.PString(Memo)+"', "
				+"'"+POut.PString(Splits)+"', "
				+"'"+POut.PString(CheckNumber)+"', "
				+"'"+POut.PInt   (ReconcileNum)+"')";
			DataConnection dcon=new DataConnection();
			if(Prefs.RandomKeys) {
				dcon.NonQ(command);
			}
			else {
				dcon.NonQ(command,true);
				JournalEntryNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void Update() {
			if(DebitAmt<0 || CreditAmt<0) {
				throw new ApplicationException(Lan.g(this,"Error. Credit and debit must both be positive."));
			}
			string command= "UPDATE journalentry SET "
				+"TransactionNum = '"+POut.PInt   (TransactionNum)+"' "
				+",AccountNum = '"   +POut.PInt   (AccountNum)+"' "
				+",DateDisplayed = '"+POut.PDate  (DateDisplayed)+"' "
				+",DebitAmt = '"     +POut.PDouble(DebitAmt)+"' "
				+",CreditAmt = '"    +POut.PDouble(CreditAmt)+"' "
				+",Memo = '"         +POut.PString(Memo)+"' "
				+",Splits = '"       +POut.PString(Splits)+"' "
				+",CheckNumber = '"  +POut.PString(CheckNumber)+"' "
				+",ReconcileNum = '" +POut.PInt   (ReconcileNum)+"' "
				+"WHERE JournalEntryNum = '"+POut.PInt(JournalEntryNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Delete() {
			string command= "DELETE FROM journalentry WHERE JournalEntryNum = "+POut.PInt(JournalEntryNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

	}

	/*=========================================================================================
		=================================== class JournalEntries ==========================================*/

	///<summary></summary>
	public class JournalEntries{

		///<summary>Used when displaying the splits for a transaction.</summary>
		public static ArrayList GetForTrans(int transactionNum){
			string command=
				"SELECT * FROM journalentry "
				+"WHERE TransactionNum="+POut.PInt(transactionNum);
			JournalEntry[] List=RefreshAndFill(command);
			ArrayList retVal=new ArrayList();
			for(int i=0;i<List.Length;i++){
				retVal.Add(List[i]);
			}
			return retVal;
		}

		///<summary>Used to display a list of entries for one account.</summary>
		public static JournalEntry[] GetForAccount(int accountNum) {
			string command=
				"SELECT * FROM journalentry "
				+"WHERE AccountNum="+POut.PInt(accountNum)
				+" ORDER BY DateDisplayed";
			return RefreshAndFill(command);
		}

		///<summary>Used in reconcile window.</summary>
		public static JournalEntry[] GetForReconcile(int accountNum,bool includeUncleared,int reconcileNum) {
			string command=
				"SELECT * FROM journalentry "
				+"WHERE AccountNum="+POut.PInt(accountNum)
				+" AND (ReconcileNum="+POut.PInt(reconcileNum);
			if(includeUncleared){
				command+=" OR ReconcileNum=0)";
			}
			else{
				command+=")";
			}
			command+=" ORDER BY DateDisplayed";
			return RefreshAndFill(command);
		}

		private static JournalEntry[] RefreshAndFill(string command){
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			JournalEntry[] List=new JournalEntry[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new JournalEntry();
				List[i].JournalEntryNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].TransactionNum = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].AccountNum     = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].DateDisplayed  = PIn.PDate  (table.Rows[i][3].ToString());
				List[i].DebitAmt       = PIn.PDouble(table.Rows[i][4].ToString());
				List[i].CreditAmt      = PIn.PDouble(table.Rows[i][5].ToString());
				List[i].Memo           = PIn.PString(table.Rows[i][6].ToString());
				List[i].Splits         = PIn.PString(table.Rows[i][7].ToString());
				List[i].CheckNumber    = PIn.PString(table.Rows[i][8].ToString());
				List[i].ReconcileNum   = PIn.PInt   (table.Rows[i][9].ToString());
			}
			return List;
		}

		///<summary>Used in FormTransactionEdit to synch database with changes user made to the journalEntry list for a transaction.  Must supply an old list for comparison.  Only the differences are saved.  Surround with try/catch, because it will thrown an exception if any entries are negative.</summary>
		public static void UpdateList(ArrayList oldJournalList,ArrayList newJournalList) {
			for(int i=0;i<newJournalList.Count;i++){
				if(((JournalEntry)newJournalList[i]).DebitAmt<0 || ((JournalEntry)newJournalList[i]).CreditAmt<0){
					throw new ApplicationException(Lan.g("JournalEntries","Error. Credit and debit must both be positive."));
				}
			}
			JournalEntry newJournalEntry;
			for(int i=0;i<oldJournalList.Count;i++) {//loop through the old list
				newJournalEntry=null;
				for(int j=0;j<newJournalList.Count;j++) {
					if(newJournalList[j]==null || ((JournalEntry)newJournalList[j]).JournalEntryNum==0) {
						continue;
					}
					if(((JournalEntry)oldJournalList[i]).JournalEntryNum==((JournalEntry)newJournalList[j]).JournalEntryNum) {
						newJournalEntry=(JournalEntry)newJournalList[j];
						break;
					}
				}
				if(newJournalEntry==null) {
					//journalentry with matching journalEntryNum was not found, so it must have been deleted
					((JournalEntry)oldJournalList[i]).Delete();
					continue;
				}
				//journalentry was found with matching journalEntryNum, so check for changes
				if(newJournalEntry.AccountNum != ((JournalEntry)oldJournalList[i]).AccountNum
					|| newJournalEntry.DateDisplayed != ((JournalEntry)oldJournalList[i]).DateDisplayed
					|| newJournalEntry.DebitAmt != ((JournalEntry)oldJournalList[i]).DebitAmt
					|| newJournalEntry.CreditAmt != ((JournalEntry)oldJournalList[i]).CreditAmt
					|| newJournalEntry.Memo != ((JournalEntry)oldJournalList[i]).Memo
					|| newJournalEntry.Splits != ((JournalEntry)oldJournalList[i]).Splits
					|| newJournalEntry.CheckNumber!= ((JournalEntry)oldJournalList[i]).CheckNumber) 
				{
					newJournalEntry.Update();
				}
			}
			for(int i=0;i<newJournalList.Count;i++) {//loop through the new list
				if(newJournalList[i]==null) {
					continue;
				}
				if(((JournalEntry)newJournalList[i]).JournalEntryNum!=0) {
					continue;
				}
				//entry with journalEntryNum=0, so it's new
				((JournalEntry)newJournalList[i]).Insert();
			}
		}

		///<summary>Called from FormTransactionEdit.</summary>
		public static bool AttachedToReconcile(ArrayList journalList){
			for(int i=0;i<journalList.Count;i++){
				if(((JournalEntry)journalList[i]).ReconcileNum!=0){
					return true;
				}
			}
			return false;
		}

		///<summary>Called once from FormReconcileEdit when closing.  Saves the reconcileNum for every item in the list.</summary>
		public static void SaveList(JournalEntry[] journalList,int reconcileNum) {
			DataConnection dcon=new DataConnection();
			string command="UPDATE journalentry SET ReconcileNum=0 WHERE";
			string str="";
			for(int i=0;i<journalList.Length;i++){
				if(journalList[i].ReconcileNum==0){
					if(str!=""){
						str+=" OR";
					}
					str+=" JournalEntryNum="+POut.PInt(journalList[i].JournalEntryNum);
				}
			}
			if(str!=""){
				command+=str;
				dcon.NonQ(command);
			}
			command="UPDATE journalentry SET ReconcileNum="+POut.PInt(reconcileNum)+" WHERE";
			str="";
			for(int i=0;i<journalList.Length;i++) {
				if(journalList[i].ReconcileNum==reconcileNum) {
					if(str!="") {
						str+=" OR";
					}
					str+=" JournalEntryNum="+POut.PInt(journalList[i].JournalEntryNum);
				}
			}
			if(str!=""){
				command+=str;
				dcon.NonQ(command);
			}
		}

		/*//<summary>Attempts to delete all journal entries for one transaction.  Will later throw an error if attached to any reconciles.</summary>
		public static void DeleteForTrans(int transactionNum){
			string command="DELETE FROM journalentry WHERE TransactionNum="+POut.PInt(transactionNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}*/




	}

	
}




