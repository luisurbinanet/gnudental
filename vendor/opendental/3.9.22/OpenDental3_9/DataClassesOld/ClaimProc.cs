using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the claimproc table in the database.</summary>
	///<remarks>Links procedures to claims.  Also links ins payments to procedures or claims.  Also used for estimating procedures even if no claim yet.  Warning: One proc might be linked twice to a given claim if insurance made two payments.  Many of the important fields are actually optional.  For instance, ProcNum is only required if itemizing ins payment, and ClaimNum is blank if Status=adjustment,cap,or estimate.</remarks>
	public class ClaimProc{
		///<summary>Primary key.</summary>
		public int ClaimProcNum;
		///<summary>Foreign key to procedurelog.ProcNum.</summary>
		public int ProcNum;
		///<summary>Foreign key to claim.ClaimNum.</summary>
		public int ClaimNum;
		///<summary>Foreign key to patient.PatNum.</summary>
		public int PatNum;
		///<summary>Foreign key to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>Fee billed to insurance. Might not be the same as the actual fee.</summary>
		///<remarks>The fee billed can be different than the actual procedure.  For instance, if you have set the insurance plan to bill insurance using UCR fees, then this field will contain the UCR fee instead of the fee that the patient was charged.</remarks>
		public double FeeBilled;
		///<summary>Actual amount this carrier is expected to pay, after taking everything else into account. Considers annual max, override, percentAmt, copayAmt, deductible, etc. In this version, not computed until sent to insurance.  In a future version, it will be computed automatically but not visible until it is sent to ins. It will be computed ahead of time so that it can be used in treatment plans.</summary>
		public double InsPayEst;
		///<summary>Deductible applied to this procedure only. Will be zero until sent to insurance.</summary>
		public double DedApplied;
		///<summary>See the ClaimProcStatus enumeration.</summary>
		public ClaimProcStatus Status;
		///<summary>Amount insurance actually paid.</summary>
		public double InsPayAmt;
		///<summary>The remarks that insurance sends in the EOB about procedures.</summary>
		public string Remarks;
		///<summary>Foreign key to ClaimPayment.ClaimPaymentNum(the insurance check).</summary>
		public int ClaimPaymentNum;
		///<summary>Foreign key to insplan.PlanNum</summary>
		public int PlanNum;
		///<summary>This is the date that is used for payment reports and tracks when the payment was actually made.  Always exactly matches the date of the ClaimPayment it's attached to.  See the note under Ledgers.ComputePayments.  This will eventually not be used for aging. The ProcDate will instead be used. See ProcDate.</summary>
		public DateTime DateCP;
		///<summary>Amount not covered by ins which is written off</summary>
		public double WriteOff;
		///<summary>The procedure code that was sent to insurance. This is not necessarily the usual procedure code.  It will already have been trimmed to 5 char if it started with "D", or it could be the alternate code.  Not allowed to be blank if it is procedure.</summary>
		public string CodeSent;
		///<summary>-1 if blank which indicates allowed is same as fee. This is the amount that the percentage is based on. Usually the same as the fee, unless this ins plan has lower UCR. Could also be different for ins substitutions, like posterior composites. If -1, then it might be changed during Procedure.ComputeEstimates/ClaimProc.ComputeBaseEst.  But once there is a value, it is guaranteed not to be changed unless user changes it.</summary>
		public double AllowedAmt;
		///<summary>-1 if blank.  Otherwise a number between 0 and 100.  The percentage that insurance pays on this procedure, as determined from insurance categories. Not user editable.</summary>
		public int Percentage;
		///<summary>-1 if blank.  Otherwise a number between 0 and 100.  Can only be changed by user.</summary>
		public int PercentOverride;
		///<summary>-1 if blank. Calculated automatically. User can not edit but can use CopayOverride instead.  Opposite of InsEst, because this is the patient portion estimate.  Two different uses: 1. For capitation, this automates calculation of writeoff. 2. For any other insurance, it gets deducted during calculation as shown in the edit window. Neither use directly affects patient balance.</summary>
		public double CopayAmt;
		///<summary>-1 if blank. Lets user override the percentAmt. This field is not updated when recalculating and is only changed by user.</summary>
		public double OverrideInsEst;
		///<summary>Set to true to not bill to this insurance plan.</summary>
		public bool NoBillIns;
		///<summary>Set true to apply the deductible before the percentage instead of the usual way of applying it after.</summary>
		public bool DedBeforePerc;
		///<summary>-1 if blank. The amount to subtract during estimating because annual benefits have maxed out.</summary>
		public double OverAnnualMax;
		///<summary>-1 if blank. The amount paid by another insurance. This amount is then subtracted from what the current insurance would pay. So, always blank for primary claims.</summary>
		public double PaidOtherIns;
		///<summary>Always has a value. Used in TP, etc. The base estimate is the ((fee or allowedAmt)-Copay) x (percentage or percentOverride). Does not include all the extras like ded, annualMax,and paidOtherIns that InsPayEst will hold in future estimating.</summary>
		public double BaseEst;
		///<summary>-1 if blank.  See description of CopayAmt.  This lets the user set a copay that will never be overwritten by automatic calculations.</summary>
		public double CopayOverride;
		///<summary>Date of the procedure.  Currently only used for tracking annual insurance benefits remaining. Important in Adjustments to benefits.  For total claim payments, MUST be the date of the procedures to correctly figure benefits.  Will eventually transition to use this field to actually calculate aging.  See the note under Ledgers.ComputePayments.</summary>
		public DateTime ProcDate;
		///<summary>Date that it was changed to status received or supplemental.  It is usually attached to a claimPayment at that point, but not if user forgets.  This is still the date that it becomes important financial data.  Only applies if Received or Supplemental.  Otherwise, the date is disregarded.  User may never edit. Important in audit trail.</summary>
		public DateTime DateEntry;

		///<summary>Returns a copy of this ClaimProc.</summary>
		public ClaimProc Copy(){
			ClaimProc cp=new ClaimProc();
			cp.ClaimProcNum=ClaimProcNum;
			cp.ProcNum=ProcNum;
			cp.ClaimNum=ClaimNum;
			cp.PatNum=PatNum;
			cp.ProvNum=ProvNum;
			cp.FeeBilled=FeeBilled;
			cp.InsPayEst=InsPayEst;
			cp.DedApplied=DedApplied;
			cp.Status=Status;
			cp.InsPayAmt=InsPayAmt;
			cp.Remarks=Remarks;
			cp.ClaimPaymentNum=ClaimPaymentNum;
			cp.PlanNum=PlanNum;
			cp.DateCP=DateCP;
			cp.WriteOff=WriteOff;
			cp.CodeSent=CodeSent;
			cp.AllowedAmt=AllowedAmt;
			cp.Percentage=Percentage;
			cp.PercentOverride=PercentOverride;
      cp.CopayAmt=CopayAmt;
			cp.OverrideInsEst=OverrideInsEst;
			cp.NoBillIns=NoBillIns;
			cp.DedBeforePerc=DedBeforePerc;
			cp.OverAnnualMax=OverAnnualMax;
			cp.PaidOtherIns=PaidOtherIns;
			cp.BaseEst=BaseEst;
			cp.CopayOverride=CopayOverride;
			cp.ProcDate=ProcDate;
			cp.DateEntry=DateEntry;
			return cp;
		}

		///<summary></summary>
		public void Insert(){
			if(Prefs.RandomKeys){
				ClaimProcNum=MiscData.GetKey("claimproc","ClaimProcNum");
			}
			string command= "INSERT INTO claimproc (";
			if(Prefs.RandomKeys){
				command+="ClaimProcNum,";
			}
			command+="ProcNum,ClaimNum,PatNum,ProvNum"
				+",FeeBilled,InsPayEst,DedApplied,Status,InsPayAmt,Remarks,ClaimPaymentNum"
				+",PlanNum,DateCP,WriteOff,CodeSent,AllowedAmt,Percentage,PercentOverride"
				+",CopayAmt,OverrideInsEst,NoBillIns,DedBeforePerc,OverAnnualMax"
				+",PaidOtherIns,BaseEst,CopayOverride,ProcDate,DateEntry) VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(ClaimProcNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (ProcNum)+"', "
				+"'"+POut.PInt   (ClaimNum)+"', "
				+"'"+POut.PInt   (PatNum)+"', "
				+"'"+POut.PInt   (ProvNum)+"', "
				+"'"+POut.PDouble(FeeBilled)+"', "
				+"'"+POut.PDouble(InsPayEst)+"', "
				+"'"+POut.PDouble(DedApplied)+"', "
				+"'"+POut.PInt   ((int)Status)+"', "
				+"'"+POut.PDouble(InsPayAmt)+"', "
				+"'"+POut.PString(Remarks)+"', "
				+"'"+POut.PInt   (ClaimPaymentNum)+"', "
				+"'"+POut.PInt   (PlanNum)+"', "
				+"'"+POut.PDate  (DateCP)+"', "
				+"'"+POut.PDouble(WriteOff)+"', "
				+"'"+POut.PString(CodeSent)+"', "
				+"'"+POut.PDouble(AllowedAmt)+"', "
				+"'"+POut.PInt   (Percentage)+"', "
				+"'"+POut.PInt   (PercentOverride)+"', "
				+"'"+POut.PDouble(CopayAmt)+"', "
				+"'"+POut.PDouble(OverrideInsEst)+"', "
				+"'"+POut.PBool  (NoBillIns)+"', "
				+"'"+POut.PBool  (DedBeforePerc)+"', "
				+"'"+POut.PDouble(OverAnnualMax)+"', "
				+"'"+POut.PDouble(PaidOtherIns)+"', "
				+"'"+POut.PDouble(BaseEst)+"', "
				+"'"+POut.PDouble(CopayOverride)+"', "
				+"'"+POut.PDate  (ProcDate)+"', "
				+"NOW())";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				ClaimProcNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void Update(){
			string command= "UPDATE claimproc SET "
				+"ProcNum = '"        +POut.PInt   (ProcNum)+"'"
				+",ClaimNum = '"      +POut.PInt   (ClaimNum)+"' "
				+",PatNum = '"        +POut.PInt   (PatNum)+"'"
				+",ProvNum = '"       +POut.PInt   (ProvNum)+"'"
				+",FeeBilled = '"     +POut.PDouble(FeeBilled)+"'"
				+",InsPayEst = '"     +POut.PDouble(InsPayEst)+"'"
				+",DedApplied = '"    +POut.PDouble(DedApplied)+"'"
				+",Status = '"        +POut.PInt   ((int)Status)+"'"
				+",InsPayAmt = '"     +POut.PDouble(InsPayAmt)+"'"
				+",Remarks = '"       +POut.PString(Remarks)+"'"
				+",ClaimPaymentNum= '"+POut.PInt   (ClaimPaymentNum)+"'"
				+",PlanNum= '"        +POut.PInt   (PlanNum)+"'"
				+",DateCP= '"         +POut.PDate  (DateCP)+"'"
				+",WriteOff= '"       +POut.PDouble(WriteOff)+"'"
				+",CodeSent= '"       +POut.PString(CodeSent)+"'"
				+",AllowedAmt= '"     +POut.PDouble(AllowedAmt)+"'"
				+",Percentage= '"     +POut.PInt   (Percentage)+"'"
				+",PercentOverride= '"+POut.PInt   (PercentOverride)+"'"
				+",CopayAmt= '"       +POut.PDouble(CopayAmt)+"'"
				+",OverrideInsEst= '" +POut.PDouble(OverrideInsEst)+"'"
				+",NoBillIns= '"      +POut.PBool  (NoBillIns)+"'"
				+",DedBeforePerc= '"  +POut.PBool  (DedBeforePerc)+"'"
				+",OverAnnualMax= '"  +POut.PDouble(OverAnnualMax)+"'"
				+",PaidOtherIns= '"   +POut.PDouble(PaidOtherIns)+"'"
				+",BaseEst= '"        +POut.PDouble(BaseEst)+"'"
				+",CopayOverride= '"  +POut.PDouble(CopayOverride)+"'"
				+",ProcDate= '"       +POut.PDate  (ProcDate)+"'"
				+",DateEntry= '"      +POut.PDate  (DateEntry)+"'"
				+" WHERE claimprocnum = '"+POut.PInt (ClaimProcNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Delete(){
			string command= "DELETE from claimproc WHERE claimprocNum = '"+POut.PInt(ClaimProcNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary>Calculates the Base estimate for a procedure.  This is not done on the fly.  Use Procedure.GetEst to later retrieve the estimate. This function duplicates/replaces all of the upper estimating logic that is within FormClaimProc.  BaseEst=((fee or allowedAmt)-Copay) x (percentage or percentOverride). The result is now stored in a claimProc.  The claimProcs do get updated frequently depending on certain actions the user takes.  The calling class must have already created the claimProc, and this function simply updates the BaseEst field of that claimproc. pst.Tot not used.  For Estimate and CapEstimate, all the estimate fields will be recalculated except the three overrides.</summary>
		public void ComputeBaseEst(Procedure proc,PriSecTot pst,InsPlan[] PlanList,PatPlan[] patPlans){//,bool resetValues){
			if(Status==ClaimProcStatus.CapClaim
				|| Status==ClaimProcStatus.CapComplete
				|| Status==ClaimProcStatus.Preauth
				|| Status==ClaimProcStatus.Supplemental)
			{
				return;//never compute estimates for those types listed above.
			}
			bool resetAll=false;
			if(Status==ClaimProcStatus.Estimate
				|| Status==ClaimProcStatus.CapEstimate)
			{
				resetAll=true;
			}
			//NoBillIns is only calculated when creating the claimproc, even if resetAll is true.
			//If user then changes a procCode, it does not cause an update of all procedures with that ADACode.
			if(NoBillIns){
				AllowedAmt=-1;
				CopayAmt=0;
				CopayOverride=-1;
				DedApplied=0;
				Percentage=-1;
				PercentOverride=-1;
				WriteOff=0;
				BaseEst=0;
				return;
			}
			//This function is called every time a ProcFee is changed,
			//so the BaseEst does reflect the new ProcFee.
			BaseEst=proc.ProcFee;
			//if(resetAll){
				//AllowedAmt=-1;
				//actually, this is a bad place for altering AllowedAmt.
				//Best to set it at the same time as the fee.
			//}
			if(AllowedAmt==-1){//If allowedAmt is blank, try to find an allowed amount.
				AllowedAmt=InsPlans.GetAllowed(proc.ADACode,PlanNum,PlanList);
				//later add posterior composite functionality. Needs to go here because the substitute fee changes.
			}
			if(AllowedAmt!=-1){
				BaseEst=AllowedAmt;
			}
			//dedApplied is never recalculated here
			//deductible is initially 0 anyway, so this calculation works.
			//Once there is a deductible included, this calculation would come out different, which is also ok.
			if(DedBeforePerc){
				BaseEst-=DedApplied;
			}
			//copayAmt
			//copayOverride never recalculated
			if(resetAll){
				if(pst==PriSecTot.Pri){
					CopayAmt=InsPlans.GetCopay(proc.ADACode,PatPlans.GetPlanNum(patPlans,1),PlanList);//also gets InsPlan
				}
				else if(pst==PriSecTot.Sec){
					CopayAmt=InsPlans.GetCopay(proc.ADACode,PatPlans.GetPlanNum(patPlans,2),PlanList);
				}
				else{//pst.Other
					CopayAmt=-1;
				}
				if(Status==ClaimProcStatus.CapEstimate){
					//this does automate the Writeoff. If user does not want writeoff automated,
					//then they will have to complete the procedure first. (very rare)
					if(CopayAmt==-1){
						CopayAmt=0;
					}
					if(CopayOverride!=-1){//override the copay
						WriteOff=proc.ProcFee-CopayOverride;
					}
					else if(CopayAmt!=-1){//use the calculated copay
						WriteOff=proc.ProcFee-CopayAmt;
					}
					//else{//no copay at all
					//	WriteOff=proc.ProcFee;
					//}
					if(WriteOff<0){
						WriteOff=0;
					}
					AllowedAmt=-1;
					DedApplied=0;
					Percentage=-1;
					PercentOverride=-1;
					BaseEst=0;
					return;
				}
			}
			if(CopayOverride!=-1){//subtract copay if override
				BaseEst-=CopayOverride;
			}
			else if(CopayAmt!=-1){//otherwise subtract calculated copay
				BaseEst-=CopayAmt;
			}
			//percentage
			//percentoverride never recalculated
			Percentage=CovPats.GetPercent(proc.ADACode,pst);//will never =-1
			if(PercentOverride==-1){//no override, so use calculated Percentage
				BaseEst=BaseEst*(double)Percentage/100;
			}
			else{//override, so use PercentOverride
				BaseEst=BaseEst*(double)PercentOverride/100;
			}
		}

		///<summary>Used when creating a claim to create any missing claimProcs. Also used in FormProcEdit if click button to add Estimate.  Inserts it into db. It will still be altered after this to fill in the fields that actually attach it to the claim.</summary>
		public void CreateEst(Procedure proc,InsPlan plan){
			ProcNum=proc.ProcNum;
			//claimnum
			PatNum=proc.PatNum;
			ProvNum=proc.ProvNum;
			if(plan.PlanType=="c"){//capitation
				if(proc.ProcStatus==ProcStat.C){//complete
					Status=ClaimProcStatus.CapComplete;//in this case, a copy will be made later.
				}
				else{//usually TP status
					Status=ClaimProcStatus.CapEstimate;
				}
			}
			else{
				Status=ClaimProcStatus.Estimate;
			}
			PlanNum=plan.PlanNum;
			DateCP=proc.ProcDate;
			//Writeoff=0
			AllowedAmt=-1;
			Percentage=-1;
			PercentOverride=-1;
			CopayAmt=-1;
			OverrideInsEst=-1;
			NoBillIns=false;
			OverAnnualMax=-1;
			PaidOtherIns=-1;
			BaseEst=0;
			CopayOverride=-1;
			ProcDate=proc.ProcDate;
			Insert();
		}





	}

}









