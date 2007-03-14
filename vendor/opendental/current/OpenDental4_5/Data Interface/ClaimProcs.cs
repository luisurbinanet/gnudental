using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary></summary>
	public class ClaimProcs{
		//<summary></summary>
		//public static ClaimProc Cur;
		//<summary></summary>
		//public static ClaimProc[] List;//all for Patients.Cur
		//<summary>ClaimProcs for Claims.Cur.ClaimNum. Fill using GetForClaim()</summary>
		//public static ClaimProc[] ForClaim;
		//<summary>ClaimProcs for Procedures.Cur.ProcNum. Fill using GetForProc()</summary>
		//public static ClaimProc[] ForProc;

		///<summary></summary>
		public static ClaimProc[] Refresh(int patNum){
			string command=
				"SELECT * from claimproc "
				+"WHERE PatNum = '"+patNum.ToString()+"' ";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			ClaimProc[] List=new ClaimProc[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new ClaimProc();
				List[i].ClaimProcNum   = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ProcNum        = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].ClaimNum       = PIn.PInt   (table.Rows[i][2].ToString());	
				List[i].PatNum         = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].ProvNum        = PIn.PInt   (table.Rows[i][4].ToString());
				List[i].FeeBilled      = PIn.PDouble(table.Rows[i][5].ToString());
				List[i].InsPayEst      = PIn.PDouble(table.Rows[i][6].ToString());
				List[i].DedApplied     = PIn.PDouble(table.Rows[i][7].ToString());
				List[i].Status         = (ClaimProcStatus)PIn.PInt(table.Rows[i][8].ToString());
				List[i].InsPayAmt      = PIn.PDouble(table.Rows[i][9].ToString());
				List[i].Remarks        = PIn.PString(table.Rows[i][10].ToString());
				List[i].ClaimPaymentNum= PIn.PInt   (table.Rows[i][11].ToString());
				List[i].PlanNum        = PIn.PInt   (table.Rows[i][12].ToString());
				List[i].DateCP         = PIn.PDate  (table.Rows[i][13].ToString());
				List[i].WriteOff       = PIn.PDouble(table.Rows[i][14].ToString());
				List[i].CodeSent       = PIn.PString(table.Rows[i][15].ToString());
				List[i].AllowedAmt     = PIn.PDouble(table.Rows[i][16].ToString());
				List[i].Percentage     = PIn.PInt   (table.Rows[i][17].ToString());
				List[i].PercentOverride= PIn.PInt   (table.Rows[i][18].ToString());
				List[i].CopayAmt       = PIn.PDouble(table.Rows[i][19].ToString());
				List[i].OverrideInsEst = PIn.PDouble(table.Rows[i][20].ToString());
				List[i].NoBillIns      = PIn.PBool  (table.Rows[i][21].ToString());
				List[i].DedBeforePerc  = PIn.PBool  (table.Rows[i][22].ToString());
				List[i].OverAnnualMax  = PIn.PDouble(table.Rows[i][23].ToString());
				List[i].PaidOtherIns   = PIn.PDouble(table.Rows[i][24].ToString());
				List[i].BaseEst        = PIn.PDouble(table.Rows[i][25].ToString());
				List[i].CopayOverride  = PIn.PDouble(table.Rows[i][26].ToString());
				List[i].ProcDate       = PIn.PDate  (table.Rows[i][27].ToString());
				List[i].DateEntry      = PIn.PDate  (table.Rows[i][28].ToString());
			}
			return List;
		}

		///<summary>Converts the supplied list into a list of ClaimProcs for one claim.</summary>
		public static ClaimProc[] GetForClaim(ClaimProc[] List,int claimNum){
			//MessageBox.Show(List.Length.ToString());
			ArrayList ALForClaim=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ClaimNum==claimNum){
					ALForClaim.Add(List[i]);  
				}
			}
			ClaimProc[] ForClaim=new ClaimProc[ALForClaim.Count];
			for(int i=0;i<ALForClaim.Count;i++){
				ForClaim[i]=(ClaimProc)ALForClaim[i];
			}
			return ForClaim;
		}

		///<summary>When sending or printing a claim, this converts the supplied list into a list of ClaimProcs that need to be sent.</summary>
		public static ClaimProc[] GetForSendClaim(ClaimProc[] List,int claimNum){
			//MessageBox.Show(List.Length.ToString());
			ArrayList ALForClaim=new ArrayList();
			bool includeThis;
			for(int i=0;i<List.Length;i++){
				if(List[i].ClaimNum!=claimNum){
					continue;
				}
				if(List[i].ProcNum==0){
					continue;//skip payments
				}
				includeThis=true;
				for(int j=0;j<ALForClaim.Count;j++){//loop through existing claimprocs
					if(((ClaimProc)ALForClaim[j]).ProcNum==List[i].ProcNum){
						includeThis=false;//skip duplicate procedures
					}
				}
				if(includeThis)
					ALForClaim.Add(List[i]);
			}
			ClaimProc[] ForClaim=new ClaimProc[ALForClaim.Count];
			for(int i=0;i<ALForClaim.Count;i++){
				ForClaim[i]=(ClaimProc)ALForClaim[i];
			}
			return ForClaim;
		}

		///<summary>Gets all ClaimProcs for the current Procedure. The List must be all ClaimProcs for this patient.</summary>
		public static ClaimProc[] GetForProc(ClaimProc[] List,int procNum){
			//MessageBox.Show(List.Length.ToString());
			ArrayList ALForProc=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum){
					ALForProc.Add(List[i]);  
				}
			}
			//need to sort by pri, sec, etc.  BUT,
			//the only way to do it would be to add an ordinal field to claimprocs or something similar.
			//Then a sorter could be built.  Otherwise, we don't know which order to put them in.
			//Maybe supply PatPlanList to this function, because it's ordered.
			//But, then if patient changes ins, it will 'forget' which is pri and which is sec.
			ClaimProc[] ForProc=new ClaimProc[ALForProc.Count];
			for(int i=0;i<ALForProc.Count;i++){
				ForProc[i]=(ClaimProc)ALForProc[i];
			}
			return ForProc;
		}

		///<summary>Used in TP module to get one estimate. The List must be all ClaimProcs for this patient. If estimate can't be found, then return null.  The procedure is always status TP, so there shouldn't be more than one estimate for one plan.</summary>
		public static ClaimProc GetEstimate(ClaimProc[] List,int procNum,int planNum) {
			for(int i=0;i<List.Length;i++) {
				if(List[i].ProcNum==procNum && List[i].PlanNum==planNum) {
					return List[i];
				}
			}
			return null;
		}

		///<summary>Used once in Account.  The insurance estimate based on all claimprocs with this procNum that are attached to claims. Includes status of NotReceived,Received, and Supplemental. The list can be all ClaimProcs for patient, or just those for this procedure.</summary>
		public static string ProcDisplayInsEst(ClaimProc[] List,int procNum){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum
					//adj ignored
					//capClaim has no insEst yet
					&& (List[i].Status==ClaimProcStatus.NotReceived
					|| List[i].Status==ClaimProcStatus.Received
					|| List[i].Status==ClaimProcStatus.Supplemental)
					){
					retVal+=List[i].InsPayEst;
				}
			}
			return retVal.ToString("F");
		}

		///<summary>Used in Account and in PaySplitEdit. The insurance estimate based on all claimprocs with this procNum, but only for those claimprocs that are not received yet. The list can be all ClaimProcs for patient, or just those for this procedure.</summary>
		public static double ProcEstNotReceived(ClaimProc[] List,int procNum){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum
					&& List[i].Status==ClaimProcStatus.NotReceived
					){
					retVal+=List[i].InsPayEst;
				}
			}
			return retVal;
		}
		
		///<summary>Used in Account and in PaySplitEdit. The insurance amount paid based on all claimprocs with this procNum. The list can be all ClaimProcs for patient, or just those for this procedure.</summary>
		public static double ProcInsPay(ClaimProc[] List,int procNum){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum
					//&& List[i].InsPayAmt > 0//ins paid
					&& List[i].Status!=ClaimProcStatus.Preauth
					&& List[i].Status!=ClaimProcStatus.CapEstimate
					&& List[i].Status!=ClaimProcStatus.CapComplete
					&& List[i].Status!=ClaimProcStatus.Estimate){
					retVal+=List[i].InsPayAmt;
				}
			}
			return retVal;
		}

		///<summary>Used in E-claims to get the amount paid by primary. The insurance amount paid by the planNum based on all claimprocs with this procNum. The list can be all ClaimProcs for patient, or just those for this procedure.</summary>
		public static double ProcInsPayPri(ClaimProc[] List,int procNum,int planNum){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum
					&& List[i].PlanNum==planNum
					&& List[i].Status!=ClaimProcStatus.Preauth
					&& List[i].Status!=ClaimProcStatus.CapEstimate
					&& List[i].Status!=ClaimProcStatus.CapComplete
					&& List[i].Status!=ClaimProcStatus.Estimate)
				{
					retVal+=List[i].InsPayAmt;
				}
			}
			return retVal;
		}

		///<summary>Used once in Account on the Claim line.  The amount paid on a claim only by total, not including by procedure.  The list can be all ClaimProcs for patient, or just those for this claim.</summary>
		public static double ClaimByTotalOnly(ClaimProc[] List,int claimNum){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].ClaimNum==claimNum
					&& List[i].ProcNum==0
					&& List[i].Status!=ClaimProcStatus.Preauth){
					retVal+=List[i].InsPayAmt;
				}
			}
			return retVal;
		}

		/*
		///<summary></summary>
		public static void DetachAllFromCheckk(int claimPaymentNum){
			string command= "UPDATE claimproc SET "
				+"ClaimPaymentNum = '0' "
				+"WHERE claimpaymentNum = '"+claimPaymentNum+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}*/

		///<summary>Attaches or detaches claimprocs from the specified claimPayment. Updates all claimprocs on a claim with one query.  It also updates their DateCP's to match the claimpayment date.</summary>
		public static void SetForClaim(int claimNum,int claimPaymentNum,DateTime date,bool setAttached){
			string command= "UPDATE claimproc SET ClaimPaymentNum = ";
			if(setAttached){
				command+="'"+claimPaymentNum+"' ";
			}
			else{
				command+="'0' ";
			}
			command+=",DateCP='"+POut.PDate(date)+"' "
				+"WHERE claimnum = '"+claimNum+"' AND "
				+"inspayamt != 0 AND ("
				+"claimpaymentNum = '"+claimPaymentNum+"' OR claimpaymentNum = '0')";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public static double ComputeBal(ClaimProc[] List){
			double retVal=0;
			//double pat;
			for(int i=0;i<List.Length;i++){
				if(List[i].Status==ClaimProcStatus.Adjustment//ins adjustments do not affect patient balance
					|| List[i].Status==ClaimProcStatus.Preauth//preauthorizations do not affect patient balance
					|| List[i].Status==ClaimProcStatus.Estimate//estimates do not affect patient balance
					|| List[i].Status==ClaimProcStatus.CapEstimate//CapEstimates do not affect patient balance
					){
					continue;
				}
				if(List[i].Status==ClaimProcStatus.Received
					|| List[i].Status==ClaimProcStatus.Supplemental//because supplemental are always received
					|| List[i].Status==ClaimProcStatus.CapClaim){//would only have a payamt if received
					retVal-=List[i].InsPayAmt;
				}
				else if(List[i].Status==ClaimProcStatus.NotReceived){
					if(!Prefs.GetBool("BalancesDontSubtractIns")){
						retVal-=List[i].InsPayEst;//this typically happens
					}
				}
				retVal-=List[i].WriteOff;
			}
			return retVal;
		}


	}

	


}









