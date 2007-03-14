using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary></summary>
	public class Payment{
		///<summary></summary>
		public int PayNum;
		///<summary>Foreign key to definition.DefNum.</summary>
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
		///<summary>Set to true to indicate that a payment is split between patients.  Just makes a few functions easier.  Might be eliminated.</summary>
		public bool IsSplit;
		///<summary>Foreign Key to patient.PatNum.  The patient where the payment entry will show.  But only the splits affect accounts.</summary>
		public int PatNum;
		///<summary>Foreign Key to clinic.ClinicNum.  Can be 0. Copied from patient.ClinicNum when creating payment, but user can override.</summary>
		public int ClinicNum;


		///<summary>Updates this payment.  Also updates the datePay of all attached paysplits so that they are always in synch.  Updates IsSplit.  DatePay and IsSplit are also updated whenever a paysplit is added or removed, so no need to run this again.</summary>
		private void Update(){
			string command="UPDATE payment SET " 
				+ "paytype = '"      +POut.PInt   (PayType)+"'"
				+ ",paydate = '"     +POut.PDate  (PayDate)+"'"
				+ ",payamt = '"      +POut.PDouble(PayAmt)+"'"
				+ ",checknum = '"    +POut.PString(CheckNum)+"'"
				+ ",bankbranch = '"  +POut.PString(BankBranch)+"'"
				+ ",paynote = '"     +POut.PString(PayNote)+"'"
				+ ",issplit = '"     +POut.PBool  (IsSplit)+"'"
				+ ",patnum = '"      +POut.PInt   (PatNum)+"'"
				+ ",ClinicNum = '"   +POut.PInt   (ClinicNum)+"'"
				+" WHERE payNum = '" +POut.PInt   (PayNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
			command="UPDATE paysplit SET DatePay='"+POut.PDate(PayDate)
				+"' WHERE PayNum = "+POut.PInt(PayNum);
 			dcon.NonQ(command);
			//set IsSplit
			command="SELECT COUNT(*) FROM paysplit WHERE PayNum="+POut.PInt(PayNum);
			DataTable table=dcon.GetTable(command);
			if(table.Rows[0][0].ToString()=="1"){
				command="UPDATE payment SET IsSplit=0 WHERE PayNum="+POut.PInt(PayNum);
			}
			else{
				command="UPDATE payment SET IsSplit=1 WHERE PayNum="+POut.PInt(PayNum);
			}
			dcon.NonQ(command);
		}

		///<summary></summary>
		private void Insert(){
			string command= "INSERT INTO payment (paytype,paydate,payamt, "
				+"checknum,bankbranch,paynote,issplit,patnum,ClinicNum) VALUES("
				+"'"+POut.PInt   (PayType)+"', "
				+"'"+POut.PDate  (PayDate)+"', "
				+"'"+POut.PDouble(PayAmt)+"', "
				+"'"+POut.PString(CheckNum)+"', "
				+"'"+POut.PString(BankBranch)+"', "
				+"'"+POut.PString(PayNote)+"', "
				+"'"+POut.PBool  (IsSplit)+"', "
				+"'"+POut.PInt   (PatNum)+"', "
				+"'"+POut.PInt   (ClinicNum)+"')";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			PayNum=dcon.InsertID;
			//MessageBox.Show(PayNum.ToString());
		}

		///<summary></summary>
		public void InsertOrUpdate(bool isNew){
			if(PayDate.Date>DateTime.Today){
				throw new Exception(Lan.g(this,"Date must not be a future date."));
			}
			if(PayDate.Year<1880){
				throw new Exception(Lan.g(this,"Invalid date"));
			}
			if(isNew){
				Insert();
			}
			else{
				Update();
			}
		}

		///<summary>Deletes the payment as well as all splits.  Also updates all necessary EstBals</summary>
		public void Delete(){
			string command= "DELETE from payment WHERE payNum = '"+PayNum.ToString()+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
			PaySplit[] splitList=PaySplits.RefreshPaymentList(PayNum);
			for(int i=0;i<splitList.Length;i++){
				splitList[i].Delete();
			}
		}

		/// <summary>Only Called only from FormPayment.butOK click.  Only called if the user did not enter any splits.  Usually just adds one split for the current patient.  But if that would take the balance negative, then it loops through all other family members and creates splits for them.  It might still take the current patient negative once all other family members are zeroed out.</summary>
		public void Allocate(){//double amtTot,int patNum,Payment payNum){
			string command= 
				"SELECT Guarantor FROM patient "
				+"WHERE PatNum = "+POut.PInt(PatNum);
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count==0){
				return;
			}
			command= 
				"SELECT PatNum,EstBalance,PriProv FROM patient "
				+"WHERE Guarantor = "+table.Rows[0][0].ToString()
				+" ORDER BY PatNum!="+POut.PInt(PatNum);//puts current patient in position 0
 			table=dcon.GetTable(command);
			Patient[] pats=new Patient[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				pats[i]=new Patient();
				pats[i].PatNum    = PIn.PInt   (table.Rows[i][0].ToString());
				pats[i].EstBalance= PIn.PDouble(table.Rows[i][1].ToString());
				pats[i].PriProv   = PIn.PInt   (table.Rows[i][2].ToString());
			}
			//first calculate all the amounts
			double amtRemain=PayAmt;//start off with the full amount
			double[] amtSplits=new double[pats.Length];
			//loop through each family member, starting with current
			for(int i=0;i<pats.Length;i++){
				if(pats[i].EstBalance==0 || pats[i].EstBalance<0){
					continue;//don't apply paysplits to anyone with a negative balance
				}
				if(amtRemain<pats[i].EstBalance){//entire remainder can be allocated to this patient
					amtSplits[i]=amtRemain;
					amtRemain=0;
					break;
				}
				else{//amount remaining is more than or equal to the estBal for this family member
					amtSplits[i]=pats[i].EstBalance;
					amtRemain-=pats[i].EstBalance;
				}
			}
			//add any remainder to the split for this patient
			amtSplits[0]+=amtRemain;
			//now create a split for each non-zero amount
			PaySplit PaySplitCur;
			for(int i=0;i<pats.Length;i++){
				if(amtSplits[i]==0){
					continue;
				}
				PaySplitCur=new PaySplit();
				PaySplitCur.PatNum=pats[i].PatNum;
				PaySplitCur.PayNum=PayNum;
				PaySplitCur.ProcDate=PayDate;
				PaySplitCur.DatePay=PayDate;
				PaySplitCur.ProvNum=pats[i].GetProvNum();
				PaySplitCur.SplitAmt=amtSplits[i];
				PaySplitCur.InsertOrUpdate(true);
			}
			//finally, adjust each EstBalance, but no need to do current patient
			for(int i=1;i<pats.Length;i++){
				if(amtSplits[i]==0){
					continue;
				}
				command="UPDATE patient SET EstBalance=EstBalance-"+POut.PDouble(amtSplits[i])
					+" WHERE PatNum="+POut.PInt(pats[i].PatNum);
				dcon.NonQ(command);
			}
		}

		

	}

	/*=========================================================================================
		=================================== class Payments ==========================================*/

	///<summary></summary>
	public class Payments{

		///<summary>Gets all payments for the specified patient. This has NOTHING to do with pay splits.  Must use pay splits for accounting.  This is only for display in Account module.</summary>
		public static Payment[] Refresh(int patNum){
			string command=
				"SELECT * from payment"
				+" WHERE PatNum="+patNum.ToString();
			return RefreshAndFill(command);
		}

		///<summary>Get one specific payment from db.</summary>
		public static Payment GetPayment(int payNum){
			string command=
				"SELECT * from payment"
				+" WHERE PayNum = '"+payNum+"'";
			return RefreshAndFill(command)[0];
		}

		///<summary>Get all specified payments.</summary>
		public static Payment[] GetPayments(int[] payNums){
			if(payNums.Length==0){
				return new Payment[0];
			}
			string command=
				"SELECT * from payment"
				+" WHERE";
			for(int i=0;i<payNums.Length;i++){
				if(i>0){
					command+=" OR";
				}
				command+=" PayNum="+payNums[i].ToString();
			}
			return RefreshAndFill(command);
		}

		private static Payment[] RefreshAndFill(string command){
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			Payment[] List=new Payment[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new Payment();
				List[i].PayNum    =PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PayType   =PIn.PInt   (table.Rows[i][1].ToString());
				List[i].PayDate   =PIn.PDate  (table.Rows[i][2].ToString());
				List[i].PayAmt    =PIn.PDouble(table.Rows[i][3].ToString());
				List[i].CheckNum  =PIn.PString(table.Rows[i][4].ToString());
				List[i].BankBranch=PIn.PString(table.Rows[i][5].ToString());
				List[i].PayNote   =PIn.PString(table.Rows[i][6].ToString());
				List[i].IsSplit   =PIn.PBool  (table.Rows[i][7].ToString());
				List[i].PatNum    =PIn.PInt   (table.Rows[i][8].ToString());
				List[i].ClinicNum =PIn.PInt   (table.Rows[i][9].ToString());
			}
			return List;
		}

		///<summary>Used for display in ProcEdit. List MUST include the requested payment. Use GetPayments to get the list.</summary>
		public static Payment GetFromList(int payNum,Payment[] List){
			for(int i=0;i<List.Length;i++){
				if(List[i].PayNum==payNum){
					return List[i];
				}
			}
			return null;//should never happen
		}

		/*
		///<summary></summary>
		public static string GetInfo(int payNum){
			string retStr;
			Payment Cur=GetPayment(payNum);
			retStr=Defs.GetName(DefCat.PaymentTypes,Cur.PayType);
			if(Cur.IsSplit) retStr=retStr
				+"  "+Cur.PayAmt.ToString("c")
				+"  "+Cur.PayDate.ToString("d")
				+" "+Lan.g("Payments","split between patients");
			return retStr;
		}*/

		

	}

	

	

}










