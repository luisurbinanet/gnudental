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
		///<summary>AKA Entry date.  Date always in perfect synch with Payment date.  Used in all reports of payments.</summary>
		public DateTime DatePay;
		/// <summary></summary>
		public int ProcNum;

		/*///<summary>Returns a copy of this PaySplit.</summary>
		public PaySplit Copy(){
			PaySplit ps=new PaySplit();
			ps.SplitNum=SplitNum;
			//etc
			return ps;
		}*/

		///<summary></summary>
		public void Update(){
			string command="UPDATE paysplit SET " 
				+ "SplitAmt = '"     +POut.PDouble(SplitAmt)+"'"
				+ ",PatNum = '"      +POut.PInt   (PatNum)+"'"
				+ ",ProcDate = '"    +POut.PDate  (ProcDate)+"'"
				+ ",PayNum = '"      +POut.PInt   (PayNum)+"'"
				//+ ",IsDiscount = '"  +POut.PBool  (IsDiscount)+"'"
				//+ ",DiscountType = '"+POut.PInt   (DiscountType)+"'"
				+ ",ProvNum = '"     +POut.PInt   (ProvNum)+"'"
				+ ",PayPlanNum = '"  +POut.PInt   (PayPlanNum)+"'"
				+ ",DatePay = '"     +POut.PDate  (DatePay)+"'"
				+ ",ProcNum = '"     +POut.PInt   (ProcNum)+"'"
				+" WHERE splitNum = '" +POut.PInt (SplitNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Insert(){
			string command="INSERT INTO paysplit (splitamt,patnum,procdate, "
				+"paynum,provnum,payplannum,DatePay,ProcNum) VALUES("
				+"'"+POut.PDouble(SplitAmt)+"', "
				+"'"+POut.PInt   (PatNum)+"', "
				+"'"+POut.PDate  (ProcDate)+"', "
				+"'"+POut.PInt   (PayNum)+"', "
				//+"'"+POut.PBool  (IsDiscount)+"', "
				//+"'"+POut.PInt   (DiscountType)+"', "
				+"'"+POut.PInt   (ProvNum)+"', "
				+"'"+POut.PInt   (PayPlanNum)+"', "
				+"'"+POut.PDate  (DatePay)+"', "
				+"'"+POut.PInt   (ProcNum)+"')";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			SplitNum=dcon.InsertID;
		}

		///<summary></summary>
		public void Delete(){
			//Cur=List[Selected];
			//PutBal(Cur.PatNum,Cur.ProcDate,-Cur.SplitAmt);
			string command= "DELETE from paysplit WHERE splitNum = '"+SplitNum.ToString()+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}



	}

	/*=========================================================================================
	=================================== class PaySplits ==========================================*/

	///<summary></summary>
	public class PaySplits{

		///<summary>Returns all paySplits for the given patNum, organized by procDate.</summary>
		public static PaySplit[] Refresh(int patNum){
			string command=
				"SELECT * FROM paysplit"
				+" WHERE PatNum = '"+patNum+"' ORDER BY ProcDate";
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

		///<summary>Used from ContrAccount and ProcEdit to display and calculate payments attached to procs.</summary>
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

		///<summary>Used twice in ContrAccount.  Returns all paySplits for the given payment, but an incomplete list of PaySplits can be supplied here, so only a partial list of attached PaySplits will be returned.</summary>
		public static ArrayList GetForPayment(int payNum,PaySplit[] List){
			ArrayList retVal=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].PayNum==payNum){
					retVal.Add(List[i]);
				}
			}
			return retVal;
		}

		///<summary>Used in FormPayment to keep all dates on PaySplits synchronized with the Payment.</summary>
		public static void SetDateInPayment(int payNum,DateTime date){
			string command="UPDATE paysplit SET DatePay='"+POut.PDate(date)
				+"' WHERE PayNum = '"+payNum.ToString()+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
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










