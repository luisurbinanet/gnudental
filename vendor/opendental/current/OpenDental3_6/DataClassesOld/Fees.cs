
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the fee table in the database.</summary>
	public class Fee{
		///<summary>Primary key.</summary>
		public int FeeNum;
		///<summary>The amount usually charged.  If an amount is unknown, then the entire Fee entry is deleted from the database.  The absence of a fee is sometimes shown in the user interface as a blank entry, and sometimes as 0.00.</summary>
		public double Amount;
		///<summary>Foreign key to procedurelog.ADACode.</summary>
		public string ADACode;
		///<summary>Foreign key to definition.DefNum.</summary>
		public int FeeSched;
		///<summary>Not used.</summary>
		public bool UseDefaultFee;
		///<summary>Not used.</summary>
		public bool UseDefaultCov;

		///<summary></summary>
		public void Update(){
			string command= "UPDATE fee SET " 
				+ "amount = '"        +POut.PDouble(Amount)+"'"
				+ ",adacode = '"      +POut.PString(ADACode)+"'"
				+ ",feesched = '"     +POut.PInt   (FeeSched)+"'"
				+ ",usedefaultfee = '"+POut.PBool  (UseDefaultFee)+"'"
				+ ",usedefaultcov = '"+POut.PBool  (UseDefaultCov)+"'"
				+" WHERE feenum = '"  +POut.PInt   (FeeNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Insert(){
			string command= "INSERT INTO fee (amount,adacode,"
				+"feesched,usedefaultfee,usedefaultcov) VALUES("
				+"'"+POut.PDouble(Amount)+"', "
				+"'"+POut.PString(ADACode)+"', "
				+"'"+POut.PInt   (FeeSched)+"', "
				+"'"+POut.PBool  (UseDefaultFee)+"', "
				+"'"+POut.PBool  (UseDefaultCov)+"')";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			FeeNum=dcon.InsertID;
		}

		///<summary></summary>
		public void Delete(){
			string command="DELETE FROM fee WHERE FeeNum="+FeeNum;
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

	}
	
	/*=========================================================================================
		=================================== class Fees ===========================================*/
	///<summary></summary>
	public class Fees{
		//<summary></summary>
		//public static Fee Cur;
		///<summary>An array of hashtables, one for each non-hidden fee schedule.  For each hashtable, key is adacode, value is Fee object.</summary>
		private static Hashtable[] HList;
		
		///<summary>Refreshes all fees and loads them into HList array.  </summary>
		public static void Refresh(){
			HList=new Hashtable[Defs.Short[(int)DefCat.FeeSchedNames].Length];
			for(int i=0;i<HList.Length;i++){
				HList[i]=new Hashtable();
			}
			Fee fee;
			string command= 
				"SELECT * from fee";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++){
				fee=new Fee();
				fee.FeeNum       =PIn.PInt   (table.Rows[i][0].ToString());
				fee.Amount       =PIn.PDouble(table.Rows[i][1].ToString());
				fee.ADACode      =PIn.PString(table.Rows[i][2].ToString());
				fee.FeeSched     =PIn.PInt   (table.Rows[i][3].ToString());
				fee.UseDefaultFee=PIn.PBool  (table.Rows[i][4].ToString());
				fee.UseDefaultCov=PIn.PBool  (table.Rows[i][5].ToString());
				if(Defs.GetOrder(DefCat.FeeSchedNames,fee.FeeSched)!=-1){//if fee sched is visible
					if(HList[Defs.GetOrder(DefCat.FeeSchedNames,fee.FeeSched)].ContainsKey(fee.ADACode)){
						//if fee was already loaded for this adacode, delete this duplicate.
						command="DELETE FROM fee WHERE feenum = '"+fee.FeeNum+"'";
						dcon.NonQ(command);
					}
					else{
						HList[Defs.GetOrder(DefCat.FeeSchedNames,fee.FeeSched)].Add(fee.ADACode,fee);
					}
				}
			}
		}

		///<summary>Used in FormProcCodeEdit,FormProcedures, and FormClaimProc to get Fees for display and for editing. Returns null if no matching fee found.</summary>
		public static Fee GetFeeByOrder(string adacode, int order){
			if(adacode==null)
				return null;
			if(HList[order].Contains(adacode)){
				return (Fee)HList[order][adacode];
			}
			return null;
			//else{
				//MessageBox.Show("code not found: "+myADA);
			//	return new Fee();
			//}
		}

		///<summary>Returns an amount if a fee has been entered.  Otherwise returns -1.  Not usually used directly.</summary>
		public static double GetAmount(string adacode, int feeSched){
			if(adacode==null)
				return -1;//was 0
			if(feeSched==0)
				return -1;//was 0
			int i=Defs.GetOrder(DefCat.FeeSchedNames,feeSched);
			if(i==-1){
				return -1;//you cannot obtain fees for hidden fee schedules
			}
			if(HList[i].Contains(adacode)){
				return ((Fee)HList[i][adacode]).Amount;
			}
			return -1;//code not found
		}

		///<summary>Almost the same as GetAmount.  But never returns -1;  Returns 0 if code can't be found.
		///Returns an amount if a fee has been entered.  Otherwise returns -1.</summary>
		public static double GetAmount0(string adacode, int feeSched){
			double retVal=GetAmount(adacode,feeSched);
			if(retVal==-1){
				return 0;
			}
			return retVal;															 
		}

		///<summary>Gets the fee schedule from the priinsplan, the patient, or the provider in that order.  Either returns a fee schedule (fk to definition.DefNum) or 0.</summary>
		public static int GetFeeSched(Patient pat,InsPlan[] PlanList){
			//there's not really a good place to put this function, so it's here.
			int retVal=0;
			if(pat.PriPlanNum!=0){
				InsPlan PlanCur=InsPlans.GetPlan(pat.PriPlanNum,PlanList);
				if(PlanCur==null){
					retVal=0;
				}
				else{
					retVal=PlanCur.FeeSched;
				}
			}
			if(retVal==0){
				if(pat.FeeSched!=0){
					retVal=pat.FeeSched;
				}
				else{
					if(pat.PriProv==0){
						retVal=Providers.List[0].FeeSched;
					}
					else{
            //MessageBox.Show(Providers.GetIndex(Patients.Cur.PriProv).ToString());   
						retVal=Providers.ListLong[Providers.GetIndexLong(pat.PriProv)].FeeSched;
					}
				}
			}
			return retVal;
		}


	}

	

}