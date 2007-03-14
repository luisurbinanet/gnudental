using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the procedurelog table in the database.</summary>
	public class Procedure{
		///<summary>Primary key.</summary>
		public int ProcNum;//
		///<summary>Foreign key to patient.PatNum</summary>
		public int PatNum;
		///<summary>Foreign key to appointment.AptNum.  Only allowed to attach proc to one appt(not counting next apt)</summary>
		public int AptNum;
		///<summary>Foreign key to procedureCode.ADACode</summary>
		public string ADACode;
		///<summary>Procedure date.</summary>
		public DateTime ProcDate;
		///<summary>Procedure fee.</summary>
		public double ProcFee;
		///<summary>Not used anymore.  In version 3.0, this was moved to the claimproc table.</summary>
		public double OverridePriOLD;
		///<summary>Not used anymore.  In version 3.0, this was moved to the claimproc table.</summary>
		public double OverrideSecOLD;
		///<summary>Surfaces, or use "UL" etc for quadrant, "2" etc for sextant, "U","L" for arches.</summary>
		public string Surf;
		///<summary>May be blank, otherwise 1-32, 51-82, A-T, or AS-TS, 1 or 2 char.</summary>
		public string ToothNum;
		///<summary>May be blank, otherwise is series of toothnumbers separated by commas.</summary>
		public string ToothRange;
		///<summary>Not used anymore.  In version 3.0, this was moved to the claimproc table.</summary>
		public bool NoBillInsOLD;
		///<summary>Foreign key to definition.DefNum, which contains the text of the priority.</summary>
		public int Priority;
		///<summary>TP=1,Complete=2,Existing Cur Prov=3,Existing Other Prov=4,Referred=5.</summary>
		public ProcStat ProcStatus;
		///<summary>Procedure note.</summary>
		public string ProcNote;
		///<summary>Foreign key to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>Foreign key to definition.DefNum, which contains text of the Diagnosis.</summary>
		public int Dx;
		///<summary>Should be called PlannedAptNum.  Foreign key to appointment.AptNum.  Allows this procedure to be attached to a Planned appointment as well as a standard appointment.</summary>
		public int NextAptNum;
		///<summary>Not used anymore.  In version 3.0, this was moved to the claimproc table.</summary>
		public bool IsCovInsOLD;
		///<summary>Not used anymore.  In version 3.0, this was moved to the claimproc table.</summary>
		public double CapCoPayOLD;
		///<summary>Only used in Public Health. See the PlaceOfService enum. Zero(Office) until procedure set complete. Then it's set to the value of the DefaultProcedurePlaceService preference.</summary>
		public PlaceOfService PlaceService;
		///<summary>Not used anymore because of the new 3D tooth chart.  Will be enhanced later to a 3 state option: Always show graphics, Never show graphics, or Show if tooth is present.</summary>
		public bool HideGraphical;
		///<summary>Single char. Blank=no, or Initial, or Replacement.</summary>
		public string Prosthesis;
		///<summary>For a prosthesis Replacement, this is the original date.</summary>
		public DateTime DateOriginalProsth;
		///<summary>This note will go on e-claim. For By Report, prep dates, or initial endo date.</summary>
		public string ClaimNote;
		///<summary>The date that the note was locked.  If no date entered, then note is not locked.  If date entered, also prevents deletion and changing status.  This will be eliminated as soon as secure invalidation is in place.</summary>
		public DateTime DateLocked;
		///<summary>This is the date this procedure was entered or set complete.  If not status C, then the value is ignored, so it might be minValue 0001-01-01 or any other date.  It gets updated when set complete.  User never allowed to edit.  This will be enhanced later.</summary>
		public DateTime DateEntryC;
		///<summary>Foreign key to clinic.ClinicNum.  0 if no clinic.</summary>
		public int ClinicNum;
		///<summary>Optional. Foreign key to procedureCode.ADACode.</summary>
		public string MedicalCode;
		///<summary>Simple text for ICD-9 code. Gets sent with medical claims.</summary>
		public string DiagnosticCode;
		///<summary>Set true if this medical diagnostic code is the principal diagnosis for the visit.  If no principal diagnosis is marked for any procedures on a medical e-claim, then it won't be allowed to be sent.  If more than one is marked, then it will just use one at random.</summary>
		public bool IsPrincDiag;
		///<summary>This is only used in Canada because their insurance companies require this number.</summary>
		public double LabFee;


		///<summary>Returns a copy of the procedure.</summary>
    public Procedure Copy(){
			Procedure proc=new Procedure();
			proc.ProcNum=ProcNum;
			proc.PatNum=PatNum;
			proc.AptNum=AptNum;
			proc.ADACode=ADACode;
			proc.ProcDate=ProcDate;
			proc.ProcFee=ProcFee;
			proc.Surf=Surf;
			proc.ToothNum=ToothNum;
			proc.ToothRange=ToothRange;
			proc.Priority=Priority;
			proc.ProcStatus=ProcStatus;
			proc.ProcNote=ProcNote;
			proc.ProvNum=ProvNum;
			proc.Dx=Dx;
			proc.NextAptNum=NextAptNum;
			proc.PlaceService=PlaceService;
			proc.HideGraphical=HideGraphical;
			proc.Prosthesis=Prosthesis;
			proc.DateOriginalProsth=DateOriginalProsth;
			proc.ClaimNote=ClaimNote;
			proc.DateLocked=DateLocked;
			proc.DateEntryC=DateEntryC;
			proc.ClinicNum=ClinicNum;
			proc.MedicalCode=MedicalCode;
			proc.DiagnosticCode=DiagnosticCode;
			proc.IsPrincDiag=IsPrincDiag;
			proc.LabFee=LabFee;
			return proc;
		}

		/*
		///<summary>If IsNew, just supply null for oldProc.</summary>
		public void InsertOrUpdate(Procedure oldProc,bool IsNew){
			//if(){
				//throw new Exception(Lan.g(this,""));
			//}
			if(IsNew){
				insert();
			}
			else{
				Update(oldProc);
			}
		}*/

		///<summary>Inserts to db, and also sets the ProcNum.</summary>
		public void Insert(){
			if(Prefs.RandomKeys){
				ProcNum=MiscData.GetKey("procedurelog","ProcNum");
			}
			string command= "INSERT INTO procedurelog (";
			if(Prefs.RandomKeys){
				command+="ProcNum,";
			}
			command+="PatNum, AptNum, ADACode, ProcDate,"
				+"ProcFee,"
				+"Surf,"
				+"ToothNum,ToothRange,Priority, "
				+"ProcStatus, ProcNote, ProvNum,"
				+"Dx,NextAptNum,PlaceService,HideGraphical,Prosthesis,DateOriginalProsth,ClaimNote,"
				+"DateLocked,DateEntryC,ClinicNum,MedicalCode,DiagnosticCode,IsPrincDiag,LabFee) VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(ProcNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (PatNum)+"', "
				+"'"+POut.PInt   (AptNum)+"', "
				+"'"+POut.PString(ADACode)+"', "
				+"'"+POut.PDate  (ProcDate)+"', "
				+"'"+POut.PDouble(ProcFee)+"', "
				//+"'"+POut.PDouble(OverridePri)+"', "
				//+"'"+POut.PDouble(OverrideSec)+"', "
				+"'"+POut.PString(Surf)+"', "
				+"'"+POut.PString(ToothNum)+"', "
				+"'"+POut.PString(ToothRange)+"', "
				//+"'"+POut.PBool  (NoBillIns)+"', "
				+"'"+POut.PInt   (Priority)+"', "
				+"'"+POut.PInt   ((int)ProcStatus)+"', "
				+"'"+POut.PString(ProcNote)+"', "
				+"'"+POut.PInt   (ProvNum)+"', "
				+"'"+POut.PInt   (Dx)+"', "
				+"'"+POut.PInt   (NextAptNum)+"', "
				//+"'"+POut.PBool  (IsCovIns)+"', "
				//+"'"+POut.PDouble(CapCoPay)+"', "
				+"'"+POut.PInt   ((int)PlaceService)+"', "
				+"'"+POut.PBool  (HideGraphical)+"', "
				+"'"+POut.PString(Prosthesis)+"', "
				+"'"+POut.PDate  (DateOriginalProsth)+"', "
				+"'"+POut.PString(ClaimNote)+"', "
				+"'"+POut.PDate  (DateLocked)+"', "
				+"NOW(), "//DateEntryC
				+"'"+POut.PInt   (ClinicNum)+"', "
				+"'"+POut.PString(MedicalCode)+"', "
				+"'"+POut.PString(DiagnosticCode)+"', "
				+"'"+POut.PBool  (IsPrincDiag)+"', "
				+"'"+POut.PDouble(LabFee)+"')";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				ProcNum=dcon.InsertID;
			}
		}

		///<summary>Updates only the changed columns and returns the number of rows affected.</summary>
		public int Update(Procedure oldProc){
			bool comma=false;
			string c = "UPDATE procedurelog SET ";
			if(PatNum!=oldProc.PatNum){
				c+="PatNum = '"     +POut.PInt   (PatNum)+"'";
				comma=true;
			}
			if(AptNum!=oldProc.AptNum){
				if(comma) c+=",";
				c+="AptNum = '"		+POut.PInt   (AptNum)+"'";
				comma=true;
			}
			if(ADACode!=oldProc.ADACode){
				if(comma) c+=",";
				c+="ADACode = '"		+POut.PString(ADACode)+"'";
				comma=true;
			}
			if(ProcDate!=oldProc.ProcDate){
				if(comma) c+=",";
				c+="ProcDate = '"	+POut.PDate  (ProcDate)+"'";
				comma=true;
			}
			if(ProcFee!=oldProc.ProcFee){
				if(comma) c+=",";
				c+="ProcFee = '"		+POut.PDouble(ProcFee)+"'";
				comma=true;
			}
			if(Surf!=oldProc.Surf){
				if(comma) c+=",";
				c+="Surf = '"			+POut.PString(Surf)+"'";
				comma=true;
			}
			if(ToothNum!=oldProc.ToothNum){
				if(comma) c+=",";
				c+="ToothNum = '"	+POut.PString(ToothNum)+"'";
				comma=true;
			}
			if(ToothRange!=oldProc.ToothRange){
				if(comma) c+=",";
				c+="ToothRange = '"+POut.PString(ToothRange)+"'";
				comma=true;
			}
			if(Priority!=oldProc.Priority){
				if(comma) c+=",";
				c+="Priority = '"	+POut.PInt   (Priority)+"'";
				comma=true;
			}
			if(ProcStatus!=oldProc.ProcStatus){
				if(comma) c+=",";
				c+="ProcStatus = '"+POut.PInt   ((int)ProcStatus)+"'";
				comma=true;
			}
			if(ProcNote!=oldProc.ProcNote){
				if(comma) c+=",";
				c+="ProcNote = '"	+POut.PString(ProcNote)+"'";
				comma=true;
			}
			if(ProvNum!=oldProc.ProvNum){
				if(comma) c+=",";
				c+="ProvNum = '"		+POut.PInt   (ProvNum)+"'";
				comma=true;
			}
			if(Dx!=oldProc.Dx){
				if(comma) c+=",";
				c+="Dx = '"				+POut.PInt   (Dx)+"'";
				comma=true;
			}
			if(NextAptNum!=oldProc.NextAptNum){
				if(comma) c+=",";
				c+="NextAptNum = '"+POut.PInt   (NextAptNum)+"'";
				comma=true;
			}
			if(PlaceService!=oldProc.PlaceService){
				if(comma) c+=",";
				c+="PlaceService = '"	+POut.PInt   ((int)PlaceService)+"'";
				comma=true;
			}
			if(HideGraphical!=oldProc.HideGraphical){
				if(comma) c+=",";
				c+="HideGraphical = '"+POut.PBool  (HideGraphical)+"'";
				comma=true;
			}
			if(Prosthesis!=oldProc.Prosthesis){
				if(comma) c+=",";
				c+="Prosthesis = '"+POut.PString(Prosthesis)+"'";
				comma=true;
			}
			if(DateOriginalProsth!=oldProc.DateOriginalProsth){
				if(comma) c+=",";
				c+="DateOriginalProsth = '"+POut.PDate  (DateOriginalProsth)+"'";
				comma=true;
			}
			if(ClaimNote!=oldProc.ClaimNote){
				if(comma) c+=",";
				c+="ClaimNote = '"+POut.PString (ClaimNote)+"'";
				comma=true;
			}
			if(DateLocked!=oldProc.DateLocked){
				if(comma) c+=",";
				c+="DateLocked = '"+POut.PDate (DateLocked)+"'";
				comma=true;
			}
			if(DateEntryC!=oldProc.DateEntryC){
				if(comma) c+=",";
				c+="DateEntryC = NOW()";
				comma=true;
			}
			if(ClinicNum!=oldProc.ClinicNum){
				if(comma) c+=",";
				c+="ClinicNum = '"+POut.PInt   (ClinicNum)+"'";
				comma=true;
			}
			if(MedicalCode!=oldProc.MedicalCode){
				if(comma) c+=",";
				c+="MedicalCode = '"+POut.PString(MedicalCode)+"'";
				comma=true;
			}
			if(DiagnosticCode!=oldProc.DiagnosticCode){
				if(comma) c+=",";
				c+="DiagnosticCode = '"+POut.PString(DiagnosticCode)+"'";
				comma=true;
			}
			if(IsPrincDiag!=oldProc.IsPrincDiag){
				if(comma) c+=",";
				c+="IsPrincDiag = '"+POut.PBool(IsPrincDiag)+"'";
				comma=true;
			}
			if(LabFee!=oldProc.LabFee) {
				if(comma) c+=",";
				c+="LabFee = '"+POut.PDouble(LabFee)+"'";
				comma=true;
			}
			if(!comma)
				return 0;//this means no change is actually required.
			c+=" WHERE ProcNum = '"+POut.PInt(ProcNum)+"'";
			DataConnection dcon=new DataConnection();
 			int rowsChanged=dcon.NonQ(c);
			//MessageBox.Show(c);
			return rowsChanged;
		}

		///<summary>Also deletes any claimProcs. Must test to make sure claimProcs are not part of a payment first.</summary>
		public void Delete(){
//todo: all validation should be done here instead of before calling this method
			string command="DELETE from procedurelog WHERE ProcNum = '"+POut.PInt(ProcNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
			command="DELETE FROM adjustment WHERE ProcNum='"+POut.PInt(ProcNum)+"'";
			dcon.NonQ(command);
			command="DELETE from claimproc WHERE ProcNum = '"+POut.PInt(ProcNum)+"'";
 			dcon.NonQ(command);
		}

		///<summary>Base estimate or override is retrieved from supplied claimprocs. Does not take into consideration annual max or deductible.  If limitToTotal set to true, then it does limit total of pri+sec to not be more than total fee.  The claimProc array typically includes all claimProcs for the patient, but must at least include all claimprocs for this proc.</summary>
		public double GetEst(ClaimProc[] claimProcs,PriSecTot pst,PatPlan[] patPlans,bool limitToTotal){
			double priBaseEst=0;
			double secBaseEst=0;
			double priOverride=-1;
			double secOverride=-1;
			for(int i=0;i<claimProcs.Length;i++){
				//adjustments automatically ignored since no ProcNum
				if(claimProcs[i].Status==ClaimProcStatus.CapClaim
					|| claimProcs[i].Status==ClaimProcStatus.Preauth
					|| claimProcs[i].Status==ClaimProcStatus.Supplemental){
					continue;
				}
				if(claimProcs[i].ProcNum==ProcNum){
					if(PatPlans.GetPlanNum(patPlans,1)==claimProcs[i].PlanNum){
						//if this is a Cap, then this will still work. Est comes out 0.
						priBaseEst=claimProcs[i].BaseEst;
						priOverride=claimProcs[i].OverrideInsEst;
					}
					else if(PatPlans.GetPlanNum(patPlans,2)==claimProcs[i].PlanNum){
						secBaseEst=claimProcs[i].BaseEst;
						secOverride=claimProcs[i].OverrideInsEst;
					}
				}
			}
			if(priOverride!=-1){
				priBaseEst=priOverride;
			}
			if(secOverride!=-1){
				secBaseEst=secOverride;
			}
			if(limitToTotal && ProcFee-priBaseEst-secBaseEst < 0){
				secBaseEst=ProcFee-priBaseEst;
			}
			switch(pst){
				case PriSecTot.Pri:
					return priBaseEst;
				case PriSecTot.Sec:
					return secBaseEst;
				case PriSecTot.Tot:
					return priBaseEst+secBaseEst;
			}
			return 0;
		}

		///<summary>Gets total writeoff for this procedure based on supplied claimprocs. Includes all claimproc types.  Only used in main TP module. The claimProc array typically includes all claimProcs for the patient, but must at least include all claimprocs for this proc.</summary>
		public double GetWriteOff(ClaimProc[] claimProcs){
			double retVal=0;
			for(int i=0;i<claimProcs.Length;i++){
				if(claimProcs[i].ProcNum==ProcNum){
					retVal+=claimProcs[i].WriteOff;
				}
			}
			return retVal;
		}

		///<summary>WriteOff'Complete'. Only used in main Account module. Gets writeoff for this procedure based on supplied claimprocs. Only includes claimprocs with status of CapComplete,CapClaim,NotReceived,Received,or Supplemental. BUT only includes Writeoffs not attached to claims, because those will display on the claim line.  In practice, this means only writeoffs with CapComplete status get returned because they are to be subtracted from the patient portion on the proc line. The claimProc array typically includes all claimProcs for the patient, but must at least include all claimprocs for this proc.</summary>
		public double GetWriteOffC(ClaimProc[] claimProcs){
			double retVal=0;
			for(int i=0;i<claimProcs.Length;i++){
				if(claimProcs[i].ProcNum!=ProcNum){
					continue;
				}
				if(claimProcs[i].ClaimNum>0){
					continue;
				}
				if(
					//adj skipped
					claimProcs[i].Status==ClaimProcStatus.CapClaim
					|| claimProcs[i].Status==ClaimProcStatus.CapComplete
					//capEstimate would never happen because procedure is C.
					//estimate means not attached to claim, so don't count
					|| claimProcs[i].Status==ClaimProcStatus.NotReceived
					//preAuth -no
					|| claimProcs[i].Status==ClaimProcStatus.Received
					|| claimProcs[i].Status==ClaimProcStatus.Supplemental
					)
				{
					retVal+=claimProcs[i].WriteOff;
				}
			}
			return retVal;
		}

		///<summary>Used whenever a procedure changes or a plan changes.  All estimates for a given procedure must be updated. This frequently includes adding claimprocs, but can also just edit the appropriate existing claimprocs. Skips status=Adjustment,CapClaim,Preauth,Supplemental.  Also fixes date,status,and provnum if appropriate.  The claimProc array can be all claimProcs for the patient, but must at least include all claimprocs for this proc.  Only set IsInitialEntry true from Chart module; this is for cap procs.</summary>
		public void ComputeEstimates(int patNum,ClaimProc[] claimProcs,bool IsInitialEntry,InsPlan[] PlanList,PatPlan[] patPlans,Benefit[] benefitList){
			bool doCreate=true;
			if(ProcDate<DateTime.Today && ProcStatus==ProcStat.C){
				//don't automatically create an estimate for completed procedures
				//especially if they are older than today
				//this may need to be relaxed a little for offices that enter treatment a few days after it's done.
				doCreate=false;
			}
			//first test to see if each estimate matches an existing patPlan (current coverage),
			//delete any other estimates
			for(int i=0;i<claimProcs.Length;i++){
				if(claimProcs[i].ProcNum!=ProcNum){
					continue;
				}
				if(claimProcs[i].PlanNum==0){
					continue;
				}
				if(claimProcs[i].Status==ClaimProcStatus.CapClaim
					|| claimProcs[i].Status==ClaimProcStatus.Preauth
					|| claimProcs[i].Status==ClaimProcStatus.Supplemental)
				{
					continue;
					//ignored: adjustment
					//included: capComplete,CapEstimate,Estimate,NotReceived,Received
				}
				if(claimProcs[i].Status!=ClaimProcStatus.Estimate && claimProcs[i].Status!=ClaimProcStatus.CapEstimate){
					continue;
				}
				bool planIsCurrent=false;
				for(int p=0;p<patPlans.Length;p++){
					if(patPlans[p].PlanNum==claimProcs[i].PlanNum){
						planIsCurrent=true;
						break;
					}
				}
				//If claimProc estimate is for a plan that is not current, delete it
				if(!planIsCurrent){
					claimProcs[i].Delete();
				}
			}
			InsPlan PlanCur;
			bool estExists;
			bool cpAdded=false;
			//loop through all patPlans (current coverage), and add any missing estimates
			for(int p=0;p<patPlans.Length;p++){//typically, loop will only have length of 1 or 2
				if(!doCreate){
					break;
				}
				//test to see if estimate exists
				estExists=false;
				for(int i=0;i<claimProcs.Length;i++){
					if(claimProcs[i].ProcNum!=ProcNum){
						continue;
					}
					if(claimProcs[i].PlanNum==0){
						continue;
					}
					if(claimProcs[i].Status==ClaimProcStatus.CapClaim
						|| claimProcs[i].Status==ClaimProcStatus.Preauth
						|| claimProcs[i].Status==ClaimProcStatus.Supplemental) {
						continue;
						//ignored: adjustment
						//included: capComplete,CapEstimate,Estimate,NotReceived,Received
					}
					if(patPlans[p].PlanNum!=claimProcs[i].PlanNum){
						continue;
					}
					estExists=true;
					break;
				}
				if(estExists){
					continue;
				}
				//estimate is missing, so add it.
				ClaimProc cp=new ClaimProc();
				cp.ProcNum=ProcNum;
				cp.PatNum=patNum;
				cp.ProvNum=ProvNum;
				PlanCur=InsPlans.GetPlan(patPlans[p].PlanNum,PlanList);
				if(PlanCur==null){
					continue;//??
				}
				if(PlanCur.PlanType=="c"){
					if(ProcStatus==ProcStat.C){
						cp.Status=ClaimProcStatus.CapComplete;
					}
					else{
						cp.Status=ClaimProcStatus.CapEstimate;//this may be changed below
					}
				}
				else{
					cp.Status=ClaimProcStatus.Estimate;
				}
				cp.PlanNum=PlanCur.PlanNum;
				cp.DateCP=ProcDate;
				cp.AllowedAmt=-1;
				cp.PercentOverride=-1;
				cp.OverrideInsEst=-1;
				cp.NoBillIns=ProcedureCodes.GetProcCode(ADACode).NoBillIns;
				cp.OverAnnualMax=-1;
				cp.PaidOtherIns=-1;
				cp.CopayOverride=-1;
				cp.ProcDate=ProcDate;
				//ComputeBaseEst will fill AllowedAmt,Percentage,CopayAmt,BaseEst
				cp.Insert();
				cpAdded=true;
			}
			//if any were added, refresh the list
			if(cpAdded){
				claimProcs=ClaimProcs.Refresh(patNum);
			}
			for(int i=0;i<claimProcs.Length;i++){
				if(claimProcs[i].ProcNum!=ProcNum){
					continue;
				}
				claimProcs[i].DateCP=ProcDate;//dates MUST match, but I can't remember why. Claims?
				claimProcs[i].ProcDate=ProcDate;
				//capitation estimates are always forced to follow the status of the procedure
				PlanCur=InsPlans.GetPlan(claimProcs[i].PlanNum,PlanList);
				if(PlanCur!=null
					&& PlanCur.PlanType=="c"
					&& (claimProcs[i].Status==ClaimProcStatus.CapComplete
					|| claimProcs[i].Status==ClaimProcStatus.CapEstimate))
				{
					if(IsInitialEntry){
						//this will be switched to CapComplete further down if applicable.
						//This makes ComputeBaseEst work properly on new cap procs w status Complete
						claimProcs[i].Status=ClaimProcStatus.CapEstimate;
					}
					else if(ProcStatus==ProcStat.C){
						claimProcs[i].Status=ClaimProcStatus.CapComplete;
					}
					else{
						claimProcs[i].Status=ClaimProcStatus.CapEstimate;
					}
				}
				//ignored: adjustment
				//ComputeBaseEst automatically skips: capComplete,Preauth,capClaim,Supplemental
				//does recalc est on: CapEstimate,Estimate,NotReceived,Received
				if(claimProcs[i].PlanNum>0 && PatPlans.GetPlanNum(patPlans,1)==claimProcs[i].PlanNum){
					claimProcs[i].ComputeBaseEst(this,PriSecTot.Pri,PlanList,patPlans,benefitList);
				}
				if(claimProcs[i].PlanNum>0 && PatPlans.GetPlanNum(patPlans,2)==claimProcs[i].PlanNum){
					claimProcs[i].ComputeBaseEst(this,PriSecTot.Sec,PlanList,patPlans,benefitList);
				}
				if(IsInitialEntry
					&& claimProcs[i].Status==ClaimProcStatus.CapEstimate
					&& ProcStatus==ProcStat.C)
				{
					claimProcs[i].Status=ClaimProcStatus.CapComplete;
				}
				//prov only updated if still an estimate
				if(claimProcs[i].Status==ClaimProcStatus.Estimate
					|| claimProcs[i].Status==ClaimProcStatus.CapEstimate){
					claimProcs[i].ProvNum=ProvNum;
				}
				claimProcs[i].Update();
			}
		}

		///<summary>Used in deciding how to display procedures in Account. The claimProcList can be all claimProcs for the patient or only those attached to this proc. Will be true if any claimProcs at all are attached to this procedure.</summary>
		public bool IsCoveredIns(ClaimProc[] List){
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==ProcNum){
					return true;
				}
			}
			return false;
		}

		///<summary>Used in deciding how to display procedures in Account. The claimProcList can be all claimProcs for the patient or only those attached to this proc. Will be true if any claimProcs attached to this procedure are set NoBillIns.</summary>
		public bool NoBillIns(ClaimProc[] List){
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==ProcNum
					&& List[i].NoBillIns){
					return true;
				}
			}
			return false;
		}

		///<summary>Used in ContrAccount.CreateClaim when validating selected procedures. Returns true if there is any claimproc for this procedure and plan which is marked NoBillIns.  The claimProcList can be all claimProcs for the patient or only those attached to this proc. Will be true if any claimProcs attached to this procedure are set NoBillIns.</summary>
		public bool NoBillIns(ClaimProc[] List,int planNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==ProcNum
					&& List[i].PlanNum==planNum
					&& List[i].NoBillIns){
					return true;
				}
			}
			return false;
		}

		///<summary>Used in deciding how to display procedures in Account. The claimProcList can be all claimProcs for the patient or only those attached to this proc. Will be true if any claimProcs attached to this procedure are status estimate, which means they haven't been attached to a claim because their status would have been changed to NotReceived.  And if the patient doesn't have ins, then the estimates would have been deleted.</summary>
		public bool IsUnsent(ClaimProc[] List){
			//unsent if no claimprocs with claimNums
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==ProcNum
					&& List[i].Status==ClaimProcStatus.Estimate
					//&& List[i].ClaimNum>0
					//&& List[i].Status!=ClaimProcStatus.Preauth
					){
					return true;
				}
			}
			return false;
		}

		///<summary>Only called from FormProcEdit to signal when to disable much of the editing in that form. If the procedure is 'AttachedToClaim' then user should not change it very much.  The claimProcList can be all claimProcs for the patient or only those attached to this proc.</summary>
		public bool IsAttachedToClaim(ClaimProc[] List){
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==ProcNum
					&& List[i].ClaimNum>0
					&& (List[i].Status==ClaimProcStatus.CapClaim
					|| List[i].Status==ClaimProcStatus.NotReceived
					|| List[i].Status==ClaimProcStatus.Preauth
					|| List[i].Status==ClaimProcStatus.Received
					|| List[i].Status==ClaimProcStatus.Supplemental
					)){
					return true;
				}
			}
			return false;
		}

		///<summary>Used in ContrAccount.CreateClaim to validate that procedure is not already attached to a claim for this specific insPlan.  The claimProcList can be all claimProcs for the patient or only those attached to this proc.</summary>
		public bool IsAlreadyAttachedToClaim(ClaimProc[] List,int planNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==ProcNum
					&& List[i].PlanNum==planNum
					&& List[i].ClaimNum>0
					&& List[i].Status!=ClaimProcStatus.Preauth)
				{
					return true;
				}
			}
			return false;
		}

		///<summary>Only used in FormProcEdit.Delete to prevent deletion of procedures with payments attached.  Tests to see if any payment at all has been received for this proc. The list can be all ClaimProcs for patient, or just those for this procedure.</summary>
		public bool InsHasPaid(ClaimProc[] List){
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==ProcNum
					&& List[i].InsPayAmt > 0//ins paid
					&& List[i].Status!=ClaimProcStatus.Preauth){
					return true;
				}
			}
			return false;
		}
		
		///<summary>Only used in ContrAccount.OnInsClick to automate selection of procedures.  Returns true if this procedure should be selected.  This happens if there is at least one claimproc attached for this plan that is an estimate, and it is not set to NoBillIns.  The list can be all ClaimProcs for patient, or just those for this procedure. The plan is the primary plan.</summary>
		public bool NeedsSent(ClaimProc[] List,int planNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==ProcNum
					&& !List[i].NoBillIns
					&& List[i].PlanNum==planNum
					&& List[i].Status==ClaimProcStatus.Estimate){
					return true;
				}
			}
			return false;
		}

		///<summary>Only used in ContrAccount.CreateClaim to decide whether a given procedure has an estimate that can be used to attach to a claim for the specified plan.  Returns a valid claimProc if this procedure has an estimate attached that is not set to NoBillIns.  The list can be all ClaimProcs for patient, or just those for this procedure. Returns null if there are no claimprocs that would work.</summary>
		public ClaimProc GetClaimProcEstimate(ClaimProc[] List,InsPlan plan){
			//bool matchOfWrongType=false;
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==ProcNum
					&& !List[i].NoBillIns
					&& List[i].PlanNum==plan.PlanNum)
				{
					if(plan.PlanType=="c"){
						if(List[i].Status==ClaimProcStatus.CapComplete)
							return List[i];
					}
					else{//any type except capitation
						if(List[i].Status==ClaimProcStatus.Estimate)
							return List[i];
					}
				}
			}
			return null;
		}

		///<summary>Used when marking a procedure complete. Only called if this procedure is set to RemoveTooth.  Hides all procedures that were previously performed on this tooth.</summary>
		public void SetHideGraphical(){
			string command="UPDATE procedurelog SET HideGraphical=1 "
				+"WHERE ToothNum='"+ToothNum+"' "
				+"AND PatNum="+PatNum.ToString()
				+" AND ProcStatus !=1"
				+" AND ProcNum !="+ProcNum.ToString();
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary>Used do display procedure descriptions on appointments and in other places. The returned string also includes surf and toothNum.</summary>
		public string GetDescription() {
			return Procedures.ConvertProcToString(ADACode,Surf,ToothNum);
		}
		


	}
}








