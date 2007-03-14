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
		///public static ClaimProc[] ForProc;

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

		///<summary>Gets all ClaimProcs for the current Procedure. The List must be all ClaimProcs for this patient.</summary>
		public static ClaimProc[] GetForProc(ClaimProc[] List,int procNum){
			//MessageBox.Show(List.Length.ToString());
			ArrayList ALForProc=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum){
					ALForProc.Add(List[i]);  
				}
			}
			ClaimProc[] ForProc=new ClaimProc[ALForProc.Count];
			for(int i=0;i<ALForProc.Count;i++){
				ForProc[i]=(ClaimProc)ALForProc[i];
			}
			return ForProc;
		}

		/*Obsolete. Replaced by better functions
		///<summary>Used in ProcEdit, and ContrAcct. The list can be all ClaimProcs for patient, or just those for this procedure.</summary>
		public static bool ProcIsAttachedToClaim(ClaimProc[] List,int procNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum 
					&& List[i].Status!=ClaimProcStatus.Preauth
					&& List[i].Status!=ClaimProcStatus.CapEstimate
					&& List[i].Status!=ClaimProcStatus.CapComplete
					&& List[i].Status!=ClaimProcStatus.Estimate){
					//adjustment skipped
					//so true if status=capclaim,NotReceived,Received, or Supplemental
					return true;
				}
			}
			return false;
		}*/

		/*///<summary>All claim procs for this procedure are received, not just some of them.</summary>
		public static bool ProcIsReceivedd(ClaimProc[] List,int procNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum
					&& List[i].Status!=ClaimProcStatus.Received
					&& List[i].Status!=ClaimProcStatus.Supplemental){
					return false;
				}
			}
			return true;
		}*/

		/*Obsolete
		///<summary>Only called from FormProcEdit.</summary>
		public static bool ProcIsSentt(ClaimProc[] List,int procNum){
			//Warning: In the future, the claim.hlist might not be already loaded and available.
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum){
					if(List[i].ClaimNum==0){
						return false;
					}
					if(((Claim)Claims.HList[List[i].ClaimNum]).ClaimStatus != "U"//not unsent
						&& ((Claim)Claims.HList[List[i].ClaimNum]).ClaimStatus != "H"){//not hold
						//must be sent
						return true;
					}
				}
			}
			return false;
		}*/

		/*Obsolete
		///<summary>Tests to see if any payment at all has been received for this proc. The list can be all ClaimProcs for patient, or just those for this procedure.</summary>
		public static bool ProcIsPaidd(ClaimProc[] List,int procNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum
					&& List[i].InsPayAmt > 0//ins paid
					&& List[i].Status!=ClaimProcStatus.Preauth){
					return true;
				}
			}
			return false;
		}*/

		///<summary>The insurance estimate based on all claimprocs with this procNum that are attached to claims. Includes status of NotReceived,Received, and Supplemental. The list can be all ClaimProcs for patient, or just those for this procedure.</summary>
		public static double ProcInsEst(ClaimProc[] List,int procNum){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum
					//adj ignored
					//capClaim has no insEst yet
					&& (List[i].Status==ClaimProcStatus.NotReceived
					|| List[i].Status!=ClaimProcStatus.Received
					|| List[i].Status!=ClaimProcStatus.Supplemental)
					){
					retVal+=List[i].InsPayEst;
				}
			}
			return retVal;
		}

		///<summary>The insurance estimate based on all claimprocs with this procNum, but only for those claimprocs that are not received yet. The list can be all ClaimProcs for patient, or just those for this procedure.</summary>
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
		

		///<summary>The insurance amount paid based on all claimprocs with this procNum. The list can be all ClaimProcs for patient, or just those for this procedure.</summary>
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

		///<summary>The amount paid on a claim only by total, not including by procedure.  The list can be all ClaimProcs for patient, or just those for this claim.</summary>
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

		///<summary></summary>
		public static void DetachAllFromCheck(int claimPaymentNum){
			string command= "UPDATE claimproc SET "
				+"ClaimPaymentNum = '0' "
				+"WHERE claimpaymentNum = '"+claimPaymentNum+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

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
				+"inspayamt > 0 AND ("
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
					retVal-=List[i].InsPayEst;
				}
				//else if(List[i].Status==ClaimProcStatus.CapClaim){//does not currently do estimating on cap
				//	if(List[i].InsPayAmt>0){
				//		
				//	}
				//	else{
				//		retVal-=List[i].InsPayEst;
				//	}
				//}
				//if Status=CapComplete, then only this next line has an effect
				retVal-=List[i].WriteOff;
			}
			return retVal;
		}


	}

	


}









