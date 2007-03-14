using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	/// <summary>Each row represents one signed agreement to make payments. </summary>
	public class PayPlan{
		/// <summary>Primary key</summary>
		public int PayPlanNum;
		/// <summary>FK to patient.PatNum.  The patient who had the treatment done.</summary>
		public int PatNum;
		/// <summary>FK to patient.PatNum.  The person responsible for the payments.  Does not need to be in the same family as the patient.  Will be 0 if planNum has a value.</summary>
		public int Guarantor;
		/// <summary>Date that the payment plan will display in the account.</summary>
		public DateTime PayPlanDate;
		/// <summary>Annual percentage rate.  eg 18.  This does not take into consideration any late payments, but only the percentage used to calculate the amortization schedule.</summary>
		public double APR;
		///<summary>Generally used to archive the terms when the amortization schedule is created.</summary>
		public string Note;
		///<summary>Will be 0 if standard payment plan.  But if this is being used to track expected insurance payments, then this will be the foreign key to insplan.PlanNum and Guarantor will be 0.</summary>
		public int PlanNum;

		/*deleted columns from previous versions:
			TotalAmount
			PeriodPayment
			Term
			AccumulatedDue
			DateFirstPay
			DownPayment
			TotalCost
			LastPayment
		*/

		///<summary></summary>
		public PayPlan Copy(){
			PayPlan p=new PayPlan();
			p.PayPlanNum=PayPlanNum;
			p.PatNum=PatNum;
			p.Guarantor=Guarantor;
			p.PayPlanDate=PayPlanDate;
			p.APR=APR;
			p.Note=Note;
			p.PlanNum=PlanNum;
			return p;
		}

		///<summary></summary>
		public void InsertOrUpdate(bool isNew){
			//if(){
			//	throw new Exception(Lan.g(this,""));
			//}
			if(isNew){
				Insert();
			}
			else{
				Update();
			}
		}

		///<summary></summary>
		private void Update(){
			string command="UPDATE payplan SET " 
				+"PatNum = '"         +POut.PInt   (PatNum)+"'"
				+",Guarantor = '"     +POut.PInt   (Guarantor)+"'"
				+",PayPlanDate = '"   +POut.PDate  (PayPlanDate)+"'"
				+",APR = '"           +POut.PDouble(APR)+"'"
				+",Note = '"          +POut.PString(Note)+"'"
				+",PlanNum = '"       +POut.PInt   (PlanNum)+"'"
				+" WHERE PayPlanNum = '" +POut.PInt(PayPlanNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		private void Insert(){
			if(Prefs.RandomKeys){
				PayPlanNum=MiscData.GetKey("payplan","PayPlanNum");
			}
			string command="INSERT INTO payplan (";
			if(Prefs.RandomKeys){
				command+="PayPlanNum,";
			}
			command+="PatNum,Guarantor,PayPlanDate,APR,Note,PlanNum) VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(PayPlanNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (PatNum)+"', "
				+"'"+POut.PInt   (Guarantor)+"', "
				+"'"+POut.PDate  (PayPlanDate)+"', "
				+"'"+POut.PDouble(APR)+"', "
				+"'"+POut.PString(Note)+"', "
				+"'"+POut.PInt   (PlanNum)+"')";
			DataConnection dcon=new DataConnection();
			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				PayPlanNum=dcon.InsertID;
			}
		}

		///<summary>Called from FormPayPlan.  Also deletes all attached payplancharges.  Throws exception if there are any paysplits attached.</summary>
		public void Delete(){
			string command="SELECT COUNT(*) FROM paysplit WHERE PayPlanNum="+PayPlanNum.ToString();
			DataConnection dcon=new DataConnection();
			if(dcon.GetCount(command)!="0"){
				throw new ApplicationException
					(Lan.g(this,"You cannot delete a payment plan with payments attached.  Unattach the payments first."));
			}
			command="DELETE FROM payplancharge WHERE PayPlanNum="+PayPlanNum.ToString();
			dcon.NonQ(command);
			command="DELETE FROM payplan WHERE PayPlanNum ="+PayPlanNum.ToString();
			dcon.NonQ(command);
		}

		

	}

	/*=========================================================================================
	=================================== class PayPlans ==========================================*/

	///<summary></summary>
	public class PayPlans{
		///<summary>Gets a list of all payplans for a given patient, whether they are the guarantor or the patient.  This is also used in UpdateAll to store all payment plans in entire database.</summary>
		public static PayPlan[] Refresh(int guarantor,int patNum){
			string command="SELECT * from payplan"
				+" WHERE PatNum = "+patNum.ToString()
				+" OR Guarantor = "+guarantor.ToString()+" ORDER BY payplandate";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			PayPlan[] List=new PayPlan[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new PayPlan();
				List[i].PayPlanNum    = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum        = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].Guarantor     = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].PayPlanDate   = PIn.PDate  (table.Rows[i][3].ToString());
				List[i].APR           = PIn.PDouble(table.Rows[i][4].ToString());
				List[i].Note          = PIn.PString(table.Rows[i][5].ToString());
				List[i].PlanNum       = PIn.PInt   (table.Rows[i][6].ToString());
			}
			return List;
		}

		/// <summary>Gets info directly from database. Used from PayPlan and Account windows to get the amount paid so far on one payment plan.</summary>
		public static double GetAmtPaid(int payPlanNum){
			string command="SELECT SUM(paysplit.SplitAmt) FROM paysplit "
				+"WHERE paysplit.PayPlanNum = '"+payPlanNum.ToString()+"' "
				+"GROUP BY paysplit.PayPlanNum";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count==0){
				return 0;
			}
			return PIn.PDouble(table.Rows[0][0].ToString());
		}

		///<summary>Used from FormPayPlan, Account, and ComputeBal to get the accumulated amount due for a payment plan based on today's date.  Includes interest, but does not include payments made so far.  The chargelist must include all charges for this payplan, but it can include more as well.</summary>
		public static double GetAccumDue(int payPlanNum, PayPlanCharge[] chargeList){
			double retVal=0;
			for(int i=0;i<chargeList.Length;i++){
				if(chargeList[i].PayPlanNum!=payPlanNum){
					continue;
				}
				if(chargeList[i].ChargeDate>DateTime.Today){//not due yet
					continue;
				}
				retVal+=chargeList[i].Principal+chargeList[i].Interest;
			}
			return retVal;
		}

		/// <summary>Used from Account window to get the amount paid so far on one payment plan.  Must pass in the total amount paid and the returned value will not be more than this.  The chargelist must include all charges for this payplan, but it can include more as well.  It will loop sequentially through the charges to get just the principal portion.</summary>
		public static double GetPrincPaid(double amtPaid,int payPlanNum,PayPlanCharge[] chargeList){
			//amtPaid gets reduced to 0 throughout this loop.
			double retVal=0;
			for(int i=0;i<chargeList.Length;i++){
				if(chargeList[i].PayPlanNum!=payPlanNum){
					continue;
				}
				//For this charge, first apply payment to interest
				if(amtPaid>chargeList[i].Interest){
					amtPaid-=chargeList[i].Interest;
				}
				else{//interest will eat up the remainder of the payment
					amtPaid=0;
					break;
				}
				//Then, apply payment to principal
				if(amtPaid>chargeList[i].Principal){
					retVal+=chargeList[i].Principal;
					amtPaid-=chargeList[i].Principal;
				}
				else{//principal will eat up the remainder of the payment
					retVal+=amtPaid;
					amtPaid=0;
					break;
				}
			}
			return retVal;
		}

		/// <summary>Used from Account and ComputeBal to get the total amount of the original principal for one payment plan.  Does not include any interest.The chargelist must include all charges for this payplan, but it can include more as well.</summary>
		public static double GetTotalPrinc(int payPlanNum, PayPlanCharge[] chargeList){
			double retVal=0;
			for(int i=0;i<chargeList.Length;i++){
				if(chargeList[i].PayPlanNum!=payPlanNum){
					continue;
				}
				retVal+=chargeList[i].Principal;
			}
			return retVal;
		}

		///<summary>Returns the sum of all payment plan entries for guarantor and/or patient.</summary>
		public static double ComputeBal(int patNum,PayPlan[] list,PayPlanCharge[] chargeList){
			double retVal=0;
			for(int i=0;i<list.Length;i++){
				//one or both of these conditions may be met:
				if(list[i].Guarantor==patNum){
					retVal+=GetAccumDue(list[i].PayPlanNum,chargeList);
				}
				if(list[i].PatNum==patNum){
					retVal-=GetTotalPrinc(list[i].PayPlanNum,chargeList);
				}
			}
			return retVal;
		}

		///<summary>Refreshes the list for the specified guarantor, and then determines if there are any valid plans with that patient as the guarantor.  If more than one valid payment plan, displays list to select from.  If any valid plans, then it returns that plan, else returns null.</summary>
		public static PayPlan GetValidPlan(int guarNum,bool isIns){
			PayPlan[] PlanListAll=Refresh(guarNum,0);
			PayPlan[] PayPlanList=GetListOneType(PlanListAll,isIns);
			if(PayPlanList.Length==0){
				return null;
			}
			if(PayPlanList.Length==1){ //if there is only one valid payplan
				return PayPlanList[0].Copy();
			}
			PayPlanCharge[] ChargeList=PayPlanCharges.Refresh(guarNum);
			//enhancement needed to weed out payment plans that are all paid off
			//more than one valid PayPlan
			FormPayPlanSelect FormPPS=new FormPayPlanSelect(PayPlanList,ChargeList);
			FormPPS.ShowDialog();
			if(FormPPS.DialogResult==DialogResult.OK){
				return PayPlanList[FormPPS.IndexSelected].Copy();
			}
			else{
				return null;
			}
		}

		///<summary>Supply a list of all payment plans for a guarantor.  Based on the isIns setting, it will either return a list of all regular payment plans or only those for ins.  Used just before displaying FormPayPlanSelect.</summary>
		public static PayPlan[] GetListOneType(PayPlan[] payPlanList,bool isIns){
			ArrayList AL=new ArrayList();
			for(int i=0;i<payPlanList.Length;i++){
				if(isIns && payPlanList[i].PlanNum>0){
					AL.Add(payPlanList[i]);
				}
				else if(!isIns && payPlanList[i].PlanNum==0){
					AL.Add(payPlanList[i]);
				}
			}
			PayPlan[] retVal=new PayPlan[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}


	}

	

	


}










