using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	/// <summary>Corresponds to the payplan table in the database.  Each row represents one signed agreement to make payments.</summary>
	public struct PayPlan{
		/// <summary>Primary key</summary>
		public int PayPlanNum;
		/// <summary>Foreign key to  patient.PatNum.  The patient who had the treatment done.</summary>
		public int PatNum;
		/// <summary>Foreign key to  patient.PatNum.  The person responsible for the payments.  
		/// Does not need to be in the same family as the patient.</summary>
		public int Guarantor;
		/// <summary>Date that the payment plan was started.</summary>
		public DateTime PayPlanDate;
		/// <summary>Total amount financed.</summary>
		public double TotalAmount;
		/// <summary>Annual percentage rate.  eg 18.  This does not take into consideration any late payments, but only the percentage used to determine the current amount due.</summary>
		public double APR;
		/// <summary>Amount of payment agreed to for each month</summary>
		public double MonthlyPayment;
		/// <summary>Number of months agreed for payment.</summary>
		public int Term;
		/// <summary>The current amount due not taking into account payments made.  Updated every time it's loaded.  Also updated using the update tool.</summary>
		public double CurrentDue;
		/// <summary>Date first payment is due.</summary>
		public DateTime DateFirstPay;
		/// <summary>The amount of downpayment not counting the first payment.</summary>
		public double DownPayment;
		///<summary>Generally used to archive the terms so they don't accidently get changed.</summary>
		public string Note;
	}

	/*=========================================================================================
	=================================== class PayPlans ==========================================*/

	///<summary></summary>
	public class PayPlans:DataClass{
		///<summary>List of all payplans for a given patient, whether they are the guarantor or the patient.  This is also used in UpdateAll to store all payment plans in entire database.</summary>
		public static PayPlan[] List;
		///<summary></summary>
		public static PayPlan Cur;

		///<summary>Refresh List for only the specified guarantor.</summary>
		public static void Refresh(int guarNum){
			if(guarNum==0){
				cmd.CommandText =
					"SELECT * from payplan"
					+" WHERE patnum = '"+Patients.Cur.PatNum+"'"
					+" || guarantor = '"+Patients.Cur.PatNum+"' ORDER BY payplandate";
			}
			else{
				cmd.CommandText =
					"SELECT * from payplan"
					+" WHERE guarantor = '"+guarNum+"' ORDER BY payplandate";
			}
			FillTable();
			List=new PayPlan[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].PayPlanNum    = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum        = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].Guarantor     = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].PayPlanDate   = PIn.PDate  (table.Rows[i][3].ToString());
				List[i].TotalAmount   = PIn.PDouble(table.Rows[i][4].ToString());
				List[i].APR           = PIn.PDouble(table.Rows[i][5].ToString());
				List[i].MonthlyPayment= PIn.PDouble(table.Rows[i][6].ToString());
				List[i].Term          = PIn.PInt   (table.Rows[i][7].ToString());
				List[i].CurrentDue    = PIn.PDouble(table.Rows[i][8].ToString());
				List[i].DateFirstPay  = PIn.PDate  (table.Rows[i][9].ToString());
				List[i].DownPayment   = PIn.PDouble(table.Rows[i][10].ToString());
				List[i].Note          = PIn.PString(table.Rows[i][11].ToString());
			}//end for
		}

		///<summary>Refresh for the cur patient, both for patnum and guarantor.</summary>
		public static void Refresh(){
			Refresh(0);
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE payplan SET " 
				+ "patnum = '"         +POut.PInt   (Cur.PatNum)+"'"
				+ ",guarantor = '"     +POut.PInt   (Cur.Guarantor)+"'"
				+ ",payplandate = '"   +POut.PDate  (Cur.PayPlanDate)+"'"
				+ ",totalamount = '"   +POut.PDouble(Cur.TotalAmount)+"'"
				+ ",apr = '"           +POut.PDouble(Cur.APR)+"'"
				+ ",monthlypayment = '"+POut.PDouble(Cur.MonthlyPayment)+"'"
				+ ",term = '"          +POut.PInt   (Cur.Term)+"'"
				+ ",currentdue = '"    +POut.PDouble(Cur.CurrentDue)+"'"
				+ ",datefirstpay = '"  +POut.PDate  (Cur.DateFirstPay)+"'"
				+ ",downpayment = '"   +POut.PDouble(Cur.DownPayment)+"'"
				+ ",note = '"          +POut.PString(Cur.Note)+"'"
				+" WHERE payplanNum = '" +POut.PInt   (Cur.PayPlanNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO payplan (patnum,guarantor,payplandate,totalamount,"
				+"apr,monthlypayment,term,currentdue,datefirstpay,downpayment,note) VALUES("
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PInt   (Cur.Guarantor)+"', "
				+"'"+POut.PDate  (Cur.PayPlanDate)+"', "
				+"'"+POut.PDouble(Cur.TotalAmount)+"', "
				+"'"+POut.PDouble(Cur.APR)+"', "
				+"'"+POut.PDouble(Cur.MonthlyPayment)+"', "
				+"'"+POut.PInt   (Cur.Term)+"', "
				+"'"+POut.PDouble(Cur.CurrentDue)+"', "
				+"'"+POut.PDate  (Cur.DateFirstPay)+"', "
				+"'"+POut.PDouble(Cur.DownPayment)+"', "
				+"'"+POut.PString(Cur.Note)+"')";
			NonQ(false);
		}

		///<summary>Must have already verified that there are no paysplits attached.  Called from FormPayPlan.</summary>
		public static void DeleteCur(){
			cmd.CommandText="DELETE FROM payplan WHERE payplannum = '"
				+Cur.PayPlanNum.ToString()+"'";
			NonQ(false);
		}

		/// <summary>Recalculates the CurrentDue for all payment plans based on the specified date.  Does not take into account any payments made.</summary>
		public static void UpdateAll(DateTime date){
			cmd.CommandText =
				"SELECT * FROM payplan";
			FillTable();
			List=new PayPlan[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].PayPlanNum    = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum        = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].Guarantor     = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].PayPlanDate   = PIn.PDate  (table.Rows[i][3].ToString());
				List[i].TotalAmount   = PIn.PDouble(table.Rows[i][4].ToString());
				List[i].APR           = PIn.PDouble(table.Rows[i][5].ToString());
				List[i].MonthlyPayment= PIn.PDouble(table.Rows[i][6].ToString());
				List[i].Term          = PIn.PInt   (table.Rows[i][7].ToString());
				List[i].CurrentDue    = PIn.PDouble(table.Rows[i][8].ToString());
				List[i].DateFirstPay  = PIn.PDate  (table.Rows[i][9].ToString());
				List[i].DownPayment   = PIn.PDouble(table.Rows[i][10].ToString());
				//List[i].Note          = PIn.PString(table.Rows[i][11].ToString());
			}//end for
			for(int i=0;i<List.Length;i++){
				Cur=List[i];
				cmd.CommandText="UPDATE payplan SET CurrentDue = '"
					+GetAmtDue().ToString()+"' WHERE PayPlanNum = '"+POut.PInt(Cur.PayPlanNum)+"'";
				NonQ(false);
			}
		}

		///<summary>Gets the amount due for the current payment plan based on today's date.  It is simply the number of months x monthly payment.  Includes interest, but does not include payments made so far.</summary>
		public static double GetAmtDue(){
			return Cur.DownPayment+Cur.MonthlyPayment*GetMonthsDue();
			//return retVal;
		}

		/// <summary>For the Cur payment plan, gets the number of months, rounded up, between the first payment date and today's date.  This is the number of payments that are due.  Used from GetAmtDue() and from FormPayPlan.</summary>
		public static int GetMonthsDue(){
			for(int i=0;i<100;i++){
				//MessageBox.Show(Cur.DateFirstPay.AddMonths(i).ToString()+","+DateTime.Today.ToString());
				if(Cur.DateFirstPay.AddMonths(i)>DateTime.Today){
					return i;
				}
			}
			return 0;
		}

		/// <summary>Used from PayPlan window to get the amount paid so far on one payment plan.</summary>
		/// <param name="payPlanNum"></param>
		public static double GetAmtPaid(int payPlanNum){
			if(payPlanNum==0){//for a new paymentPlan
				return 0;
			}
			cmd.CommandText="SELECT SUM(paysplit.splitamt) FROM paysplit "
				+"WHERE paysplit.payplannum = '"+payPlanNum.ToString()+"' "
				+"GROUP BY paysplit.payplannum";
			FillTable();
			if(table.Rows.Count==0){
				return 0;
			}
			return PIn.PDouble(table.Rows[0][0].ToString());
		}

		///<summary>Must make sure Refresh is done first.  Returns the sum of all payment plan entries for guarantor and/or patient.</summary>
		public static double ComputeBal(){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				//one or both of these conditions may be met:
				if(List[i].Guarantor==Patients.Cur.PatNum){
					retVal+=List[i].CurrentDue;
				}
				if(List[i].PatNum==Patients.Cur.PatNum){
					retVal-=List[i].TotalAmount;
				}
			}
			return retVal;
		}

		///<summary>Refreshes the list for the specified guarantor, and then determines if there are any valid plans with that patient as the guarantor.  If so, then it sets Cur, else returns false.</summary>
		public static bool GetValidPlan(int guarNum){
			Refresh(guarNum);
			if(List.Length==0){
				return false;
			}
			if(List.Length==1){ //if there is only one valid payplan
				Cur=List[0];
				return true;
			}
			//more than one valid PayPlan
			FormPayPlanSelect FormPPS=new FormPayPlanSelect();
			FormPPS.ValidPlans=List;
			FormPPS.ShowDialog();
			if(FormPPS.DialogResult==DialogResult.OK){
				Cur=List[FormPPS.IndexSelected];
				return true;
			}
			else{
				return false;
			}
		}


	}

	

	


}









