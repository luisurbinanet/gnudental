using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the accountingautopay table in the database.</summary>
	public class AccountingAutoPay{
		///<summary>Primary key.</summary>
		public int AccountingAutoPayNum;
		///<summary>FK to definitions.DefNum.</summary>
		public int PayType;
		///<summary>AccountNums separated by commas.  No spaces.</summary>
		public string PickList;

		///<summary>Returns a copy of this AccountingAutoPay.</summary>
		public AccountingAutoPay Copy(){
			AccountingAutoPay a=new AccountingAutoPay();
			a.AccountingAutoPayNum=AccountingAutoPayNum;
			a.PayType=PayType;
			a.PickList=PickList;
			return a;
		}

		
		///<summary></summary>
		public void Insert(){
			string command= "INSERT INTO accountingautopay (PayType,PickList) VALUES("
				+"'"+POut.PInt   (PayType)+"', "
				+"'"+POut.PString(PickList)+"')";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			AccountingAutoPayNum=dcon.InsertID;
		}

		/*///<summary></summary>
		public void Update(){
			string command= "UPDATE accountingautopay SET " 
				+ "PayType = '"     +POut.PInt   (PayType)+"'"
				+ ",PickList = '"   +POut.PString(PickList)+"'"
				+" WHERE AccountingAutoPayNum = '" +POut.PInt   (AccountingAutoPayNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}*/

		/*//<summary></summary>
		public void Delete(){
			string command="DELETE FROM accountingautoaay" 
				+" WHERE AccountingAutoPayNum = "+POut.PInt(AccountingAutoPayNum);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}*/

		///<summary>Converts the comma delimited list of AccountNums into full descriptions separated by carriage returns.</summary>
		public string GetPickListDesc(){
			string[] numArray=PickList.Split(new char[] { ',' });
			string retVal="";
			for(int i=0;i<numArray.Length;i++) {
				if(numArray[i]=="") {
					continue;
				}
				if(retVal!=""){
					retVal+="\r\n";
				}
				retVal+=Accounts.GetDescript(PIn.PInt(numArray[i]));
			}
			return retVal;
		}

		///<summary>Converts the comma delimited list of AccountNums into an array of AccountNums.</summary>
		public int[] GetPickListAccounts() {
			string[] numArray=PickList.Split(new char[] { ',' });
			ArrayList AL=new ArrayList();
			for(int i=0;i<numArray.Length;i++) {
				if(numArray[i]=="") {
					continue;
				}
				AL.Add(PIn.PInt(numArray[i]));
			}
			int[] retVal=new int[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

    
	}

	/*=========================================================================================
	=================================== class AccountingAutoPays ==========================================*/
	///<summary></summary>
	public class AccountingAutoPays{
		///<summary></summary>
		public static ArrayList AList;

		///<summary>Gets a list of all AccountingAutoPays.</summary>
		public static void Refresh(){
			string command="SELECT * FROM accountingautopay";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			AccountingAutoPay[] List=new AccountingAutoPay[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new AccountingAutoPay();
				List[i].AccountingAutoPayNum = PIn.PInt(table.Rows[i][0].ToString());
				List[i].PayType              = PIn.PInt(table.Rows[i][1].ToString());
				List[i].PickList             = PIn.PString(table.Rows[i][2].ToString());
			}
			//AccountingAutoPay[] List=RefreshAndFill(command);
			AList=new ArrayList();
			for(int i=0;i<List.Length;i++){
				AList.Add(List[i]);
			}
		}

		//private static AccountingAutoPay[] RefreshAndFill(string command){	
		//	return List;
		//}

		///<summary>Loops through the AList to find one with the specified payType (defNum).  If none is found, then it returns null.</summary>
		public static AccountingAutoPay GetForPayType(int payType){
			for(int i=0;i<AList.Count;i++){
				if(((AccountingAutoPay)AList[i]).PayType==payType){
					return (AccountingAutoPay)AList[i];
				}
			}
			return null;
		}

		///<summary>Saves the list of accountingAutoPays to the database.  Deletes all existing ones first.</summary>
		public static void SaveList(ArrayList AL) {
			string command="DELETE FROM accountingautopay";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
			for(int i=0;i<AL.Count;i++){
				((AccountingAutoPay)AL[i]).Insert();
			}


			/*PaySplit newPaySplit;
			for(int i=0;i<oldSplitList.Count;i++) {//loop through the old list
				newPaySplit=null;
				for(int j=0;j<newSplitList.Count;j++) {
					if(newSplitList[j]==null || ((PaySplit)newSplitList[j]).SplitNum==0) {
						continue;
					}
					if(((PaySplit)oldSplitList[i]).SplitNum==((PaySplit)newSplitList[j]).SplitNum) {
						newPaySplit=(PaySplit)newSplitList[j];
						break;
					}
				}
				if(newPaySplit==null) {
					//PaySplit with matching SplitNum was not found, so it must have been deleted
					((PaySplit)oldSplitList[i]).Delete();
					continue;
				}
				//PaySplit was found with matching SplitNum, so check for changes
				if(newPaySplit.DateEntry != ((PaySplit)oldSplitList[i]).DateEntry
					|| newPaySplit.DatePay != ((PaySplit)oldSplitList[i]).DatePay
					|| newPaySplit.PatNum != ((PaySplit)oldSplitList[i]).PatNum
					|| newPaySplit.PayNum != ((PaySplit)oldSplitList[i]).PayNum
					|| newPaySplit.PayPlanNum != ((PaySplit)oldSplitList[i]).PayPlanNum
					|| newPaySplit.ProcDate != ((PaySplit)oldSplitList[i]).ProcDate
					|| newPaySplit.ProcNum != ((PaySplit)oldSplitList[i]).ProcNum
					|| newPaySplit.ProvNum != ((PaySplit)oldSplitList[i]).ProvNum
					|| newPaySplit.SplitAmt != ((PaySplit)oldSplitList[i]).SplitAmt) {
					newPaySplit.Update();
				}
			}
			for(int i=0;i<newSplitList.Count;i++) {//loop through the new list
				if(newSplitList[i]==null) {
					continue;
				}
				if(((PaySplit)newSplitList[i]).SplitNum!=0) {
					continue;
				}
				//entry with SplitNum=0, so it's new
				((PaySplit)newSplitList[i]).Insert();
			}*/
		}

		

	}
	


}













