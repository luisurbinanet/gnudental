using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Used in the accounting section in chart of accounts.  Not related to patient accounts in any way.</summary>
	public class Account{
		///<summary>Primary key.</summary>
		public int AccountNum;
		///<summary>.</summary>
		public string Description;
		///<summary>Enum:AccountType Asset, Liability, Equity,Revenue, Expense</summary>
		public AccountType AcctType;
		///<summary>For asset accounts, this would be the bank account number for deposit slips.</summary>
		public string BankNumber;
		///<summary>Set to true to not normally view this account in the list.</summary>
		public bool Inactive;
		///<summary>.</summary>
		public Color AccountColor;

		///<summary></summary>
		public Account Copy() {
			Account a=new Account();
			a.AccountNum=AccountNum;
			a.Description=Description;
			a.AcctType=AcctType;
			a.BankNumber=BankNumber;
			a.Inactive=Inactive;
			a.AccountColor=AccountColor;
			return a;
		}

		///<summary></summary>
		public void Insert() {
			if(Prefs.RandomKeys) {
				AccountNum=MiscData.GetKey("account","AccountNum");
			}
			string command="INSERT INTO account (";
			if(Prefs.RandomKeys) {
				command+="AccountNum,";
			}
			command+="Description,AcctType,BankNumber,Inactive,AccountColor) VALUES(";
			if(Prefs.RandomKeys) {
				command+="'"+POut.PInt(AccountNum)+"', ";
			}
			command+=
				 "'"+POut.PString(Description)+"', "
				+"'"+POut.PInt   ((int)AcctType)+"', "
				+"'"+POut.PString(BankNumber)+"', "
				+"'"+POut.PBool  (Inactive)+"', "
				+"'"+POut.PInt   (AccountColor.ToArgb())+"')";
			DataConnection dcon=new DataConnection();
			if(Prefs.RandomKeys) {
				dcon.NonQ(command);
			}
			else {
				dcon.NonQ(command,true);
				AccountNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void Update() {
			string command= "UPDATE account SET "
				+"Description = '"  +POut.PString(Description)+"' "
				+",AcctType = '"    +POut.PInt   ((int)AcctType)+"' "
				+",BankNumber = '"  +POut.PString(BankNumber)+"' "
				+",Inactive = '"    +POut.PBool  (Inactive)+"' "
				+",AccountColor = '"+POut.PInt   (AccountColor.ToArgb())+"' "
				+"WHERE AccountNum = '"+POut.PInt(AccountNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary>Throws exception if account is in use.</summary>
		public void Delete() {
			//check to see if account has any journal entries
			string command="SELECT COUNT(*) FROM journalentry WHERE AccountNum="+POut.PInt(AccountNum);
			DataConnection dcon=new DataConnection();
			if(dcon.GetCount(command)!="0"){
				throw new ApplicationException(Lan.g("FormAccountEdit",
					"Not allowed to delete an account with existing journal entries."));
			}
			//Check various preference entries
			command="SELECT ValueString FROM preference WHERE PrefName='AccountingDepositAccounts'";
			string result=dcon.GetCount(command);
			string[] strArray=result.Split(new char[] {','});
			for(int i=0;i<strArray.Length;i++){
				if(strArray[i]==AccountNum.ToString()){
					throw new ApplicationException(Lan.g("FormAccountEdit","Account is in use in the setup section."));
				}
			}
			command="SELECT ValueString FROM preference WHERE PrefName='AccountingIncomeAccount'";
			result=dcon.GetCount(command);
			if(result==AccountNum.ToString()) {
				throw new ApplicationException(Lan.g("FormAccountEdit","Account is in use in the setup section."));
			}
			command="SELECT ValueString FROM preference WHERE PrefName='AccountingCashIncomeAccount'";
			result=dcon.GetCount(command);
			if(result==AccountNum.ToString()) {
				throw new ApplicationException(Lan.g("FormAccountEdit","Account is in use in the setup section."));
			}
			//check AccountingAutoPay entries
			for(int i=0;i<AccountingAutoPays.AList.Count;i++){
				strArray=((AccountingAutoPay)AccountingAutoPays.AList[i]).PickList.Split(new char[] { ',' });
				for(int s=0;s<strArray.Length;s++){
					if(strArray[s]==AccountNum.ToString()){
						throw new ApplicationException(Lan.g("FormAccountEdit","Account is in use in the setup section."));
					}
				}
			}
			command="DELETE FROM account WHERE AccountNum = "+POut.PInt(AccountNum);
			dcon.NonQ(command);
		}

	}

	/*=========================================================================================
		=================================== class Accounts ==========================================*/

	///<summary>The two lists get refreshed the first time they are needed rather than at startup.</summary>
	public class Accounts{
		private static Account[] listLong;
		private static Account[] listShort;

		///<summary></summary>
		public static Account[] ListLong{
			get{
				if(listLong==null){
					Refresh();
				}
				return listLong;
			}
			/*set {
				title=value;
			}*/
		}

		///<summary>Used for display. Does not include inactive</summary>
		public static Account[] ListShort{
			get {
				if(listShort==null) {
					Refresh();
				}
				return listShort;
			}
		}

		///<summary></summary>
		public static void Refresh(){
			string command=
				"SELECT * from account "
				+" ORDER BY AcctType,Description";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			listLong=new Account[table.Rows.Count];
			ArrayList AL=new ArrayList();
			for(int i=0;i<listLong.Length;i++){
				listLong[i]=new Account();
				listLong[i].AccountNum  = PIn.PInt   (table.Rows[i][0].ToString());
				listLong[i].Description = PIn.PString(table.Rows[i][1].ToString());
				listLong[i].AcctType    = (AccountType)PIn.PInt   (table.Rows[i][2].ToString());
				listLong[i].BankNumber  = PIn.PString(table.Rows[i][3].ToString());
				listLong[i].Inactive    = PIn.PBool  (table.Rows[i][4].ToString());
				listLong[i].AccountColor= Color.FromArgb(PIn.PInt(table.Rows[i][5].ToString()));
				if(!listLong[i].Inactive){
					AL.Add(listLong[i].Copy());
				}
			}
			listShort=new Account[AL.Count];
			AL.CopyTo(listShort);
		}

		///<summary>Loops through listLong to find a description for the specified account.  0 returns an empty string.</summary>
		public static string GetDescript(int accountNum){
			if(accountNum==0) {
				return "";
			}
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].AccountNum==accountNum){
					return ListLong[i].Description;
				}
			}
			return "";
		}

		///<summary>Loops through listLong to find an account.  Will return null if accountNum is 0.</summary>
		public static Account GetAccount(int accountNum) {
			if(accountNum==0){
				return null;
			}
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].AccountNum==accountNum) {
					return ListLong[i].Copy();
				}
			}
			return null;//just in case
		}

		///<summary>Used to test the sign on debits and credits for the five different account types</summary>
		public static bool DebitIsPos(AccountType type){
			switch(type){
				case AccountType.Asset:
				case AccountType.Expense:
					return true;
				case AccountType.Liability:
				case AccountType.Equity://because liabilities and equity are treated the same
				case AccountType.Revenue:
					return false;
			}
			return true;//will never happen
		}

		///<summary>Gets the balance of an account directly from the database.</summary>
		public static double GetBalance(int accountNum,AccountType acctType){
			string command="SELECT SUM(DebitAmt),SUM(CreditAmt) FROM journalentry "
				+"WHERE AccountNum="+POut.PInt(accountNum)
				+" GROUP BY AccountNum";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			double debit=0;
			double credit=0;
			if(table.Rows.Count>0){
				debit=PIn.PDouble(table.Rows[0][0].ToString());
				credit=PIn.PDouble(table.Rows[0][1].ToString());
			}
			if(DebitIsPos(acctType)){
				return debit-credit;
			}
			else{
				return credit-debit;
			}
			/*}
			catch {
				Debug.WriteLine(command);
				MessageBox.Show(command);
			}
			return 0;*/
		}

		///<summary>Checks the loaded prefs to see if user has setup deposit linking.  Returns true if so.</summary>
		public static bool DepositsLinked(){
			string depAccounts=Prefs.GetString("AccountingDepositAccounts");
			if(depAccounts==""){
				return false;
			}
			if(Prefs.GetInt("AccountingIncomeAccount")==0){
				return false;
			}
			//might add a few more checks later.
			return true;
		}

		///<summary>Checks the loaded prefs and accountingAutoPays to see if user has setup auto pay linking.  Returns true if so.</summary>
		public static bool PaymentsLinked() {
			if(AccountingAutoPays.AList.Count==0){
				return false;
			}
			if(Prefs.GetInt("AccountingIncomeAccount")==0) {
				return false;
			}
			//might add a few more checks later.
			return true;
		}

		///<summary></summary>
		public static int[] GetDepositAccounts(){
			string depStr=Prefs.GetString("AccountingDepositAccounts");
			string[] depStrArray=depStr.Split(new char[] { ',' });
			ArrayList depAL=new ArrayList();
			for(int i=0;i<depStrArray.Length;i++) {
				if(depStrArray[i]=="") {
					continue;
				}
				depAL.Add(PIn.PInt(depStrArray[i]));
			}
			int[] retVal=new int[depAL.Count];
			depAL.CopyTo(retVal);
			return retVal;
		}

	}

	
}




