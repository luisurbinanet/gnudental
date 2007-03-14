using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the paysplit table in the database.</summary>
	public class PaySplit{
		///<summary>Primary key.</summary>
		public int SplitNum;
		///<summary>Amount of split.</summary>
		public double SplitAmt;
		///<summary>Foreign key to patient.PatNum.</summary>
		public int PatNum;
		///<summary>Procedure date.  Not when the payment was made.  This is what the aging will be based on in a future version.</summary>
		public DateTime ProcDate;//
		///<summary>Foreign key to payment.PayNum.  Every paysplit must be linked to a payment.</summary>
		public int PayNum;
		///<summary>No longer used.</summary>
		public bool IsDiscountOLD;
		///<summary>No longer used</summary>
		public int DiscountTypeOLD;
		///<summary>Foreign key to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>Foreign key to payplan.PayPlanNum.  0 if not attached to a payplan.</summary>
		public int PayPlanNum;
		///<summary>Date always in perfect synch with Payment date.</summary>
		public DateTime DatePay;
		/// <summary></summary>
		public int ProcNum;
		///<summary>Date this paysplit was created.  User not allowed to edit.</summary>
		public DateTime DateEntry;

		/*///<summary>Returns a copy of this PaySplit.</summary>
		public PaySplit Copy(){
			PaySplit ps=new PaySplit();
			ps.SplitNum=SplitNum;
			//etc
			return ps;
		}*/

		///<summary></summary>
		private void Update(){
			string command="UPDATE paysplit SET " 
				+ "SplitAmt = '"     +POut.PDouble(SplitAmt)+"'"
				+ ",PatNum = '"      +POut.PInt   (PatNum)+"'"
				+ ",ProcDate = '"    +POut.PDate  (ProcDate)+"'"
				+ ",PayNum = '"      +POut.PInt   (PayNum)+"'"
				+ ",ProvNum = '"     +POut.PInt   (ProvNum)+"'"
				+ ",PayPlanNum = '"  +POut.PInt   (PayPlanNum)+"'"
				+ ",DatePay = '"     +POut.PDate  (DatePay)+"'"
				+ ",ProcNum = '"     +POut.PInt   (ProcNum)+"'"
				//+ ",DateEntry = '"   +POut.PDate  (DateEntry)+"'"//not allowed to change
				+" WHERE splitNum = '" +POut.PInt (SplitNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary>Inserts a </summary>
		private void Insert(){
			if(Prefs.RandomKeys){
				SplitNum=MiscData.GetKey("paysplit","SplitNum");
			}
			string command= "INSERT INTO paysplit (";
			if(Prefs.RandomKeys){
				command+="SplitNum,";
			}
			command+="SplitAmt,PatNum,ProcDate, "
				+"PayNum,ProvNum,PayPlanNum,DatePay,ProcNum,DateEntry) VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(SplitNum)+"', ";
			}
			command+=
				 "'"+POut.PDouble(SplitAmt)+"', "
				+"'"+POut.PInt   (PatNum)+"', "
				+"'"+POut.PDate  (ProcDate)+"', "
				+"'"+POut.PInt   (PayNum)+"', "
				+"'"+POut.PInt   (ProvNum)+"', "
				+"'"+POut.PInt   (PayPlanNum)+"', "
				+"'"+POut.PDate  (DatePay)+"', "
				+"'"+POut.PInt   (ProcNum)+"', "
				+"NOW())";//DateEntry: date of server
			DataConnection dcon=new DataConnection();
 			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				SplitNum=dcon.InsertID;
			}
			SplitNum=dcon.InsertID;
			SetSplit();
		}

		///<summary>Called from Insert and from Delete because both of these actions can change the number of splits in a payment.</summary>
		private void SetSplit(){
			string command="SELECT COUNT(*) FROM paysplit WHERE PayNum="+POut.PInt(PayNum);
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(table.Rows[0][0].ToString()=="1"){//only 1 paysplit
				command="UPDATE payment SET IsSplit=0 WHERE PayNum="+POut.PInt(PayNum);//set false
			}
			else{
				command="UPDATE payment SET IsSplit=1 WHERE PayNum="+POut.PInt(PayNum);//set true
			}
			dcon.NonQ(command);
		}

		///<summary>First forces the DatePay of this split to match that of the payment.  Then does the insert or update.  If insert, then it sets the payment.IsSplit accordingly.</summary>
		public void InsertOrUpdate(bool isNew){
			//if(){
			//	throw new Exception(Lan.g(this,""));
			//}
			//get the date of the payment and force this split to match
			string command="SELECT PayDate FROM payment WHERE PayNum="+POut.PInt(PayNum);
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			DatePay=PIn.PDate(table.Rows[0][0].ToString());
			if(isNew){
				Insert();
			}
			else{
				Update();
			}
		}

		///<summary>Deletes the paysplit and updates the corresponding EstBal. Called from Payment.Delete for each split.</summary>
		public void Delete(){
			string command= "DELETE from paysplit WHERE splitNum = '"+SplitNum.ToString()+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
			SetSplit();
			command="UPDATE patient SET EstBalance=EstBalance+"+POut.PDouble(SplitAmt)
				+" WHERE PatNum="+POut.PInt(PatNum);
			dcon.NonQ(command);
		}



	}

	/*=========================================================================================
	=================================== class PaySplits ==========================================*/

	///<summary></summary>
	public class PaySplits{

		///<summary>Returns all paySplits for the given patNum, organized by procDate.  WARNING! Also includes related paysplits that aren't actually attached to patient.  Includes any split where payment is for this patient.</summary>
		public static PaySplit[] Refresh(int patNum){
			string command=
				"SELECT paysplit.* FROM paysplit,payment "
				+"WHERE paysplit.PayNum=payment.PayNum "
				+"AND (paysplit.PatNum = '"+POut.PInt(patNum)+"' OR payment.PatNum = '"+POut.PInt(patNum)+"') "
				+"GROUP BY paysplit.SplitNum "
				+"ORDER BY ProcDate";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			PaySplit[] List=new PaySplit[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new PaySplit();
				List[i].SplitNum    = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].SplitAmt    = PIn.PDouble(table.Rows[i][1].ToString());
				List[i].PatNum      = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].ProcDate    = PIn.PDate  (table.Rows[i][3].ToString());
				List[i].PayNum      = PIn.PInt   (table.Rows[i][4].ToString());
				//List[i].IsDiscount  = PIn.PBool  (table.Rows[i][5].ToString());
				//List[i].DiscountType= PIn.PInt   (table.Rows[i][6].ToString());
				List[i].ProvNum     = PIn.PInt   (table.Rows[i][7].ToString());
				List[i].PayPlanNum  = PIn.PInt   (table.Rows[i][8].ToString());
				List[i].DatePay     = PIn.PDate  (table.Rows[i][9].ToString());
				List[i].ProcNum     = PIn.PInt   (table.Rows[i][10].ToString());
				List[i].DateEntry   = PIn.PDate  (table.Rows[i][11].ToString());
			}
			return List;
		}

		///<summary>Returns all PaySplits for the given paymentNum.</summary>
		public static PaySplit[] RefreshPaymentList(int payNum){
			string command=
				"SELECT * from paysplit"
				+" WHERE paynum = '"+payNum+"'";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			PaySplit[] PaymentList=new PaySplit[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				PaymentList[i]=new PaySplit();
				PaymentList[i].SplitNum    = PIn.PInt   (table.Rows[i][0].ToString());
				PaymentList[i].SplitAmt    = PIn.PDouble(table.Rows[i][1].ToString());
				PaymentList[i].PatNum      = PIn.PInt   (table.Rows[i][2].ToString());
				PaymentList[i].ProcDate    = PIn.PDate  (table.Rows[i][3].ToString());
				PaymentList[i].PayNum      = PIn.PInt   (table.Rows[i][4].ToString());
				//PaymentList[i].IsDiscount  = PIn.PBool  (table.Rows[i][5].ToString());
				//PaymentList[i].DiscountType= PIn.PInt   (table.Rows[i][6].ToString());
				PaymentList[i].ProvNum     = PIn.PInt   (table.Rows[i][7].ToString());
				PaymentList[i].PayPlanNum  = PIn.PInt   (table.Rows[i][8].ToString());
				PaymentList[i].DatePay     = PIn.PDate  (table.Rows[i][9].ToString());
				PaymentList[i].ProcNum     = PIn.PInt   (table.Rows[i][10].ToString());
				PaymentList[i].DateEntry   = PIn.PDate  (table.Rows[i][11].ToString());
			}
			return PaymentList;
		}

		///<summary>Returns all paySplits for the given procNum. Must supply a list of all paysplits for the patient.</summary>
		public static ArrayList GetForProc(int procNum,PaySplit[] List){
			ArrayList retVal=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum){
					retVal.Add(List[i]);
				}
			}
			return retVal;
		}

		///<summary>Used from ContrAccount and ProcEdit to display and calculate payments attached to procs. Used once in FormProcEdit</summary>
		public static double GetTotForProc(int procNum,PaySplit[] List){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum){
					retVal+=List[i].SplitAmt;
				}
			}
			return retVal;
		}

		///<summary>Used from FormPaySplitEdit.  Returns total payments for a procedure for all paysplits other than the supplied excluded paysplit.</summary>
		public static double GetTotForProc(int procNum,PaySplit[] List,int excludeSplitNum){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].SplitNum==excludeSplitNum){
					continue;
				}
				if(List[i].ProcNum==procNum){
					retVal+=List[i].SplitAmt;
				}
			}
			return retVal;
		}

		///<summary>Used once in ContrAccount.  WARNING!  The returned list of 'paysplits' are not real paysplits.  They are actually grouped by patient and date.  Only the ProcDate, SplitAmt, PatNum, and ProcNum(one of many) are filled. Must supply a list which would include all paysplits for this payment.</summary>
		public static ArrayList GetForPayment(int payNum,PaySplit[] List){
			ArrayList retVal=new ArrayList();
			int matchI;
			for(int i=0;i<List.Length;i++){
				if(List[i].PayNum==payNum){
					//find a 'paysplit' with matching procdate and patnum
					matchI=-1;
					for(int j=0;j<retVal.Count;j++){
						if(((PaySplit)retVal[j]).ProcDate==List[i].ProcDate && ((PaySplit)retVal[j]).PatNum==List[i].PatNum){
							matchI=j;
							break;
						}
					}
					if(matchI==-1){
						retVal.Add(new PaySplit());
						matchI=retVal.Count-1;
						((PaySplit)retVal[matchI]).ProcDate=List[i].ProcDate;
						((PaySplit)retVal[matchI]).PatNum=List[i].PatNum;
					}
					if(((PaySplit)retVal[matchI]).ProcNum==0 && List[i].ProcNum!=0){
						((PaySplit)retVal[matchI]).ProcNum=List[i].ProcNum;
					}
					((PaySplit)retVal[matchI]).SplitAmt+=List[i].SplitAmt;
				}
			}
			return retVal;
		}

		///<summary>Used once in ContrAccount to just get the splits for a single patient.  The supplied list also contains splits that are not necessarily for this one patient.</summary>
		public static PaySplit[] GetForPatient(int patNum,PaySplit[] List){
			ArrayList retVal=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].PatNum==patNum){
					retVal.Add(List[i]);
				}
			}
			PaySplit[] retList=new PaySplit[retVal.Count];
			retVal.CopyTo(retList);
			return retList;
		}

		///<summary>Used once in ContrAccount.  Usually returns 0 unless there is a payplan for this payment and patient.</summary>
		public static int GetPayPlanNum(int payNum,int patNum,PaySplit[] List){
			for(int i=0;i<List.Length;i++){
				if(List[i].PayNum==payNum && List[i].PatNum==patNum && List[i].PayPlanNum!=0){
					return List[i].PayPlanNum;
				}
			}
			return 0;
		}

		///<summary>Used in ComputeBalances to compute balance for a single patient. Supply a list of all paysplits for the patient.</summary>
		public static double ComputeBal(PaySplit[] list){//
			double retVal=0;
			for(int i=0;i<list.Length;i++){
				retVal+=list[i].SplitAmt;
			}
			return retVal;
		}

		

	}

	

	


}










