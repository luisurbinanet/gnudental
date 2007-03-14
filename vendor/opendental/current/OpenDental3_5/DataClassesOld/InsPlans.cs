using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	
	///<summary></summary>
	public class InsPlans{
		//<summary>Used in FormQuery.SubmitQuery to allow display of carrier names. Fill with GetHListAll()</summary>
		//public static Hashtable HListAll;
		//<summary>Used to display a list of all non duplicate plans. Like the old template table.</summary>
		//public static InsPlan[] ListAll;

		
		///<summary>It's fastest if you supply a plan list that contains the plan, but it also works just fine if it can't initally locate the plan in the list.  You can supply an array of length 0.  If still not found, returns null.</summary>
		public static InsPlan GetPlan(int planNum,InsPlan[] planList){
			InsPlan retPlan=new InsPlan();
			if(planNum==0){
				return null;
			}
			bool found=false;
			for(int i=0;i<planList.Length;i++){
				if(planList[i].PlanNum==planNum){
					found=true;
					retPlan=planList[i];
				}
			}
			if(!found){
				retPlan=Refresh(planNum);//retPlan will now be null if not found
			}
			if(retPlan==null){
				MessageBox.Show(Lan.g("InsPlans","Database is slightly corrupted.  Please run the database integrity tool."));
				return new InsPlan();
			}
			if(retPlan==null){
				return null;
			}
			if(retPlan.PlanNum>0 && retPlan.RenewMonth<1){//0 or -1
				//fix small db error:
				retPlan.RenewMonth=1;
				retPlan.Update();
				//don't worry about refreshing plan list for something this minor.
			}
			return retPlan;
		}

		///<summary>Only loads one plan from db. Can return null.</summary>
		private static InsPlan Refresh(int planNum){
			if(planNum==0)
				return null;
			string command="SELECT * FROM insplan WHERE plannum = '"+planNum+"'";
			InsPlan[] PlanList=RefreshFill(command);
			if(PlanList.Length>0){
				return PlanList[0];
			}
			else{
				return null;
			}
		}

		///<summary>Gets new List for the specified family.</summary>
		public static InsPlan[] Refresh(Family Fam){
			//subscribers in family
			string s="subscriber='"+Fam.List[0].PatNum+"'";
			for(int i=1;i<Fam.List.Length;i++){
				s+=" || subscriber='"+Fam.List[i].PatNum+"'";
			}
			//plans in family(usually lots of duplicates of subscribers, but this also allows mixing families
			//the only plans it misses are for claims with no current coverage.  These are handled as needed.
			string plans="";//="subscriber='"+Patients.FamilyList[0].PatNum+"'";
			for(int i=0;i<Fam.List.Length;i++){
				//if(i>0) plans+=" ||";
				if(Fam.List[i].PriPlanNum > 0)
					plans+=" || plannum = '"+Fam.List[i].PriPlanNum+"'";
				if(Fam.List[i].SecPlanNum > 0)
					plans+=" || plannum = '"+Fam.List[i].SecPlanNum+"'";
			}
			//MessageBox.Show(plans);
			string command=
				"SELECT * from insplan "
				+"WHERE "+s+plans
				+" ORDER BY dateeffective";
			return RefreshFill(command);
		}

		private static InsPlan[] RefreshFill(string command){
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			InsPlan[] PlanList=new InsPlan[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				PlanList[i]=new InsPlan();
				PlanList[i].PlanNum        = PIn.PInt   (table.Rows[i][0].ToString());
				PlanList[i].Subscriber     = PIn.PInt   (table.Rows[i][1].ToString());
				//PlanList[i].Carrier        = PIn.PString(table.Rows[i][2].ToString());
				PlanList[i].DateEffective  = PIn.PDate  (table.Rows[i][3].ToString());
				PlanList[i].DateTerm       = PIn.PDate  (table.Rows[i][4].ToString());
				PlanList[i].Phone          = PIn.PString(table.Rows[i][5].ToString());
				PlanList[i].GroupName      = PIn.PString(table.Rows[i][6].ToString());
				PlanList[i].GroupNum       = PIn.PString(table.Rows[i][7].ToString());
				//PlanList[i].Address        = PIn.PString(table.Rows[i][8].ToString());
				//PlanList[i].Address2       = PIn.PString(table.Rows[i][9].ToString());
				//PlanList[i].City           = PIn.PString(table.Rows[i][10].ToString());
				//PlanList[i].State          = PIn.PString(table.Rows[i][11].ToString());
				//PlanList[i].Zip            = PIn.PString(table.Rows[i][12].ToString());
				//PlanList[i].NoSendElect    = PIn.PBool  (table.Rows[i][13].ToString());
				//PlanList[i].ElectID        = PIn.PString(table.Rows[i][14].ToString());
				//PlanList[i].Employer       = PIn.PString(table.Rows[i][15].ToString());
				PlanList[i].AnnualMax      = PIn.PInt   (table.Rows[i][16].ToString());
				PlanList[i].RenewMonth     = PIn.PInt   (table.Rows[i][17].ToString());
				PlanList[i].Deductible     = PIn.PInt   (table.Rows[i][18].ToString());
				PlanList[i].DeductWaivPrev =(YN)PIn.PInt(table.Rows[i][19].ToString());
				PlanList[i].OrthoMax       = PIn.PInt    (table.Rows[i][20].ToString());
				PlanList[i].FloToAge       = PIn.PInt    (table.Rows[i][21].ToString());
				PlanList[i].PlanNote       = PIn.PString (table.Rows[i][22].ToString());
				PlanList[i].MissToothExcl  = (YN)PIn.PInt(table.Rows[i][23].ToString());
				PlanList[i].MajorWait      = (YN)PIn.PInt(table.Rows[i][24].ToString());
				PlanList[i].FeeSched       = PIn.PInt    (table.Rows[i][25].ToString());
				PlanList[i].ReleaseInfo    = PIn.PBool   (table.Rows[i][26].ToString());
				PlanList[i].AssignBen      = PIn.PBool   (table.Rows[i][27].ToString());
				PlanList[i].PlanType       = PIn.PString (table.Rows[i][28].ToString());
				PlanList[i].ClaimFormNum   = PIn.PInt    (table.Rows[i][29].ToString());
				PlanList[i].UseAltCode     = PIn.PBool   (table.Rows[i][30].ToString());
				PlanList[i].ClaimsUseUCR   = PIn.PBool   (table.Rows[i][31].ToString());
				PlanList[i].IsWrittenOff   = PIn.PBool   (table.Rows[i][32].ToString());
				PlanList[i].CopayFeeSched  = PIn.PInt    (table.Rows[i][33].ToString());
				PlanList[i].SubscriberID   = PIn.PString (table.Rows[i][34].ToString());
				PlanList[i].EmployerNum    = PIn.PInt    (table.Rows[i][35].ToString());
				PlanList[i].CarrierNum     = PIn.PInt    (table.Rows[i][36].ToString());
				PlanList[i].AllowedFeeSched= PIn.PInt    (table.Rows[i][37].ToString());
			}
			return PlanList;
		}

		///<summary>Gets all plans in the database, organized by carrier name or by employer.</summary>
		public static InsPlan[] RefreshListAll(bool byEmployer){
			string command;
			if(byEmployer){
				command=
					"SELECT insplan.EmployerNum,insplan.GroupName,insplan.GroupNum,insplan.CarrierNum"
					+",insplan.PlanType,insplan.UseAltCode"
					+",insplan.ClaimsUseUCR,insplan.FeeSched,insplan.CopayFeeSched,insplan.ClaimFormNum"
					+",COUNT(*),employer.EmpName,carrier.CarrierName "
					+"FROM insplan "
					+"LEFT JOIN employer ON employer.EmployerNum = insplan.EmployerNum "
					+"LEFT JOIN carrier ON carrier.CarrierNum = insplan.CarrierNum "
					+"GROUP BY insplan.EmployerNum,insplan.GroupName,insplan.GroupNum,insplan.CarrierNum"
					+",insplan.PlanType,insplan.UseAltCode"
					+",insplan.ClaimsUseUCR,insplan.FeeSched,insplan.CopayFeeSched,insplan.ClaimFormNum "
					+"ORDER BY employer.EmpName IS NULL,employer.EmpName,carrier.CarrierName ASC";
				//MessageBox.Show(cmd.CommandText);
			}
			else{
				command=
					"SELECT insplan.EmployerNum,insplan.GroupName,insplan.GroupNum,insplan.CarrierNum"
					+",insplan.PlanType,insplan.UseAltCode"
					+",insplan.ClaimsUseUCR,insplan.FeeSched,insplan.CopayFeeSched,insplan.ClaimFormNum"
					+",COUNT(*),carrier.CarrierName FROM insplan "
					+"LEFT JOIN carrier USING(CarrierNum) "
					+"GROUP BY insplan.EmployerNum,insplan.GroupName,insplan.GroupNum,insplan.CarrierNum"
					+",insplan.PlanType,insplan.UseAltCode"
					+",insplan.ClaimsUseUCR,insplan.FeeSched,insplan.CopayFeeSched,insplan.ClaimFormNum "
					+"ORDER BY carrier.CarrierName ASC";
			}
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			InsPlan[] ListAll=new InsPlan[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				ListAll[i]=new InsPlan();
				ListAll[i].EmployerNum  = PIn.PInt   (table.Rows[i][0].ToString());
				ListAll[i].GroupName    = PIn.PString(table.Rows[i][1].ToString());
				ListAll[i].GroupNum     = PIn.PString(table.Rows[i][2].ToString());
				ListAll[i].CarrierNum   = PIn.PInt   (table.Rows[i][3].ToString());
				ListAll[i].PlanType     = PIn.PString(table.Rows[i][4].ToString());
				ListAll[i].UseAltCode   = PIn.PBool  (table.Rows[i][5].ToString());
				ListAll[i].ClaimsUseUCR = PIn.PBool  (table.Rows[i][6].ToString());
				ListAll[i].FeeSched     = PIn.PInt   (table.Rows[i][7].ToString());
				ListAll[i].CopayFeeSched= PIn.PInt   (table.Rows[i][8].ToString());
				ListAll[i].ClaimFormNum = PIn.PInt   (table.Rows[i][9].ToString());
				ListAll[i].NumberPlans  = PIn.PInt   (table.Rows[i][10].ToString());
				//ListAll[i].PlanNum      = PIn.PInt   (table.Rows[i][11].ToString());//random
			}
			return ListAll;
		}	

		///<summary>Gets a description of the specified plan, including carrier name and subscriber. It's fastest if you supply a plan list that contains the plan, but it also works just fine if it can't initally locate the plan in the list.  You can supply an array of length 0.</summary>
		public static string GetDescript(int planNum,Family family,InsPlan[] PlanList){
			if(planNum==0)
				return "";
			InsPlan plan=GetPlan(planNum,PlanList);
			if(plan==null || plan.PlanNum==0){
				return "";
			}
			string subscriber=family.GetNameInFamFL(plan.Subscriber);
			if(subscriber==""){//subscriber from another family
				subscriber=Patients.GetLim(plan.Subscriber).GetNameLF();
			}
			string retStr="";
			//loop just to get the index of the plan in the family list
			bool otherFam=true;
			for(int i=0;i<PlanList.Length;i++){
				if(PlanList[i].PlanNum==planNum){
					otherFam=false;
					//retStr += (i+1).ToString()+": ";
				}
			}
			if(otherFam)//retStr=="")
				retStr="(other fam):";
			Carrier carrier=Carriers.GetCarrier(plan.CarrierNum);
			string carrierName=carrier.CarrierName;
			if(carrierName.Length>20){
				carrierName=carrierName.Substring(0,20)+"...";
			}
			retStr+=carrierName;
			retStr+=" ("+subscriber+")";
			return retStr;
		}

		///<summary>Used in Ins lines in Account module.</summary>
		public static string GetCarrierName(int planNum,InsPlan[] PlanList){
			InsPlan plan=GetPlan(planNum,PlanList);
			if(plan==null){
				return "";
			}
			Carrier carrier=Carriers.GetCarrier(plan.CarrierNum);
			if(carrier.CarrierNum==0){//if corrupted
				return "";
			}
			return carrier.CarrierName;
		}

		/// <summary>Get insurance benefits remaining for one benefit year.  InsPlans must be refreshed first.  Returns actual remaining insurance based on ClaimProc data, taking into account inspayed and ins pending. Must supply all claimprocs for the patient.  Date used to determine which benefit year to calc.  Usually today's date.  The insplan.PlanNum is the plan to get value for.  ExcludeClaim is the ClaimNum to exclude, or enter -1 to include all.</summary>
		public static double GetInsRem(ClaimProc[] ClaimProcList,DateTime date,int planNum,int excludeClaim,InsPlan[] PlanList){
			InsPlan curPlan=GetPlan(planNum,PlanList);
			if(curPlan==null){
				return 0;
			}
			if(curPlan.AnnualMax==0){
				return 0;
			}
			/*it used to look like this: Changed on 4/2/04
			 * Now, regardless of the plan type, a blank ins max will be equivalent to no maximum,
			 * just in case a new user forgets to enter it.
			if(((InsPlan)HList[planNum]).PlanType==""){//percentage category
				if(((InsPlan)HList[planNum]).AnnualMax<0){
					return 0;
				}
			}
			else{//flat copay or capitation
				if(((InsPlan)HList[planNum]).AnnualMax<0){
					return 999999;
				}
			}*/
			if(curPlan.AnnualMax<0){
				return 999999;
			}
			double retVal=curPlan.AnnualMax;
			DateTime startDate;//for benefit year
			DateTime stopDate;
			if(date < new DateTime(date.Year,curPlan.RenewMonth,1)){
				startDate=new DateTime(date.Year-1,curPlan.RenewMonth,1);
				stopDate=new DateTime(date.Year,curPlan.RenewMonth,1);
			}
			else{
				startDate=new DateTime(date.Year,curPlan.RenewMonth,1);
				stopDate=new DateTime(date.Year+1,curPlan.RenewMonth,1);
			}
			for(int i=0;i<ClaimProcList.Length;i++){
				if(ClaimProcList[i].PlanNum==planNum
					&& ClaimProcList[i].ClaimNum != excludeClaim
					&& ClaimProcList[i].ProcDate < stopDate
					&& ClaimProcList[i].ProcDate >= startDate
					//enum ClaimProcStatus{NotReceived,Received,Preauth,Adjustment,Supplemental}
					&& ClaimProcList[i].Status!=ClaimProcStatus.Preauth)
				{
					if(ClaimProcList[i].Status==ClaimProcStatus.Received 
						|| ClaimProcList[i].Status==ClaimProcStatus.Adjustment
						|| ClaimProcList[i].Status==ClaimProcStatus.Supplemental)
					{
						retVal-=ClaimProcList[i].InsPayAmt;
					}
					else
					{//NotReceived
						retVal-=ClaimProcList[i].InsPayEst;
					}
				}
			}
			if(retVal<0) return 0;
			return retVal;
		}

		/// <summary>Get pending insurance for a given plan for one benefit year. InsPlans must be refreshed first. Include a ClaimProcList which is all claimProcs for the patient.</summary>
		/// <param name="ClaimProcList"></param>
		/// <param name="date">Used to determine which benefit year to calc.  Usually today's date.</param>
		/// <param name="planNum">The insplan.PlanNum to retreive insurance info for.</param>
		/// <param name="PlanList"></param>
		/// <returns>Returns the amount of insurance pending based on ClaimProc data.</returns>
		public static double GetPending(ClaimProc[] ClaimProcList,DateTime date,int planNum,InsPlan[] PlanList){//
			//These 3 lines were eliminated because we can still return pending whether or not annual max blank
			//if(((InsPlan)HList[planNum]).AnnualMax<=0){
			//	return 0;
			//}
			InsPlan curPlan=GetPlan(planNum,PlanList);
			if(curPlan==null){
				return 0;
			}
			double retVal=0;
			DateTime startDate;//for benefit year
			DateTime stopDate;
			if(date < new DateTime(date.Year,curPlan.RenewMonth,1)){
				startDate=new DateTime(date.Year-1,curPlan.RenewMonth,1);
				stopDate=new DateTime(date.Year,curPlan.RenewMonth,1);
			}
			else{
				startDate=new DateTime(date.Year,curPlan.RenewMonth,1);
				stopDate=new DateTime(date.Year+1,curPlan.RenewMonth,1);
			}
			for(int i=0;i<ClaimProcList.Length;i++){
				if(ClaimProcList[i].PlanNum==planNum
					&& ClaimProcList[i].ProcDate < stopDate
					&& ClaimProcList[i].ProcDate >= startDate
					//enum ClaimProcStatus{NotReceived,Received,Preauth,Adjustment,Supplemental}
					&& ClaimProcList[i].Status==ClaimProcStatus.NotReceived
					//Status Adjustment has no insPayEst, so can ignore it here.
					){
					retVal+=ClaimProcList[i].InsPayEst;
				}
			}
			return retVal;
		}

		///<summary>Gets the deductible remaining for an insurance plan for one benefit year which includes the given date.  InsPlans must be refreshed first.  ClaimProcList should be the entire list of claimprocs for the patient. You can exclude a claim or use -1 to include all.</summary>
		public static double GetDedRem(ClaimProc[] ClaimProcList,DateTime date,int planNum,int excludeClaim,InsPlan[] PlanList){
			InsPlan curPlan=GetPlan(planNum,PlanList);
			if(curPlan==null){
				return 0;
			}
			double retVal=0;
			if(curPlan.Deductible!=-1){
				retVal=curPlan.Deductible;
			}
			DateTime startDate;//for benefit year
			DateTime stopDate;
			if(date < new DateTime(date.Year,curPlan.RenewMonth,1)){
				startDate=new DateTime(date.Year-1,curPlan.RenewMonth,1);
				stopDate=new DateTime(date.Year,curPlan.RenewMonth,1);
			}
			else{
				startDate=new DateTime(date.Year,curPlan.RenewMonth,1);
				stopDate=new DateTime(date.Year+1,curPlan.RenewMonth,1);
			}
			for(int i=0;i<ClaimProcList.Length;i++){
				if(ClaimProcList[i].PlanNum==planNum
					&& ClaimProcList[i].ClaimNum!=excludeClaim
					&& ClaimProcList[i].ProcDate < stopDate
					&& ClaimProcList[i].ProcDate >= startDate
					//enum ClaimProcStatus{NotReceived,Received,Preauth,Adjustment,Supplemental}
					//preauth does not affect deductibles,
					//but received, not received, and adjustments to affect it.
					&& ClaimProcList[i].Status!=ClaimProcStatus.Preauth
					){
					retVal-=ClaimProcList[i].DedApplied;
				}
			}
			if(retVal<0) return 0;
			return retVal;
		}

		///<summary>Returns -1 if no copay feeschedule or fee unknown for this adaCode. Otherwise, returns 0 or more for a patient copay. Can handle a planNum of 0.</summary>
		public static double GetCopay(string adaCode,int planNum,InsPlan[] PlanList){
			if(planNum==0){
				return -1;
			}
			InsPlan plan=GetPlan(planNum,PlanList);
			if(plan==null){
				return -1;
			}
			if(plan.CopayFeeSched==0){
				return -1;
			}
			return Fees.GetAmount(adaCode,plan.CopayFeeSched);
		}

		///<summary>Returns -1 if no allowed feeschedule or fee unknown for this adaCode. Otherwise, returns the allowed fee including 0. Can handle a planNum of 0.</summary>
		public static double GetAllowed(string adaCode,int planNum,InsPlan[] PlanList){
			if(planNum==0){
				return -1;
			}
			InsPlan plan=GetPlan(planNum,PlanList);
			if(plan==null){
				return -1;
			}
			if(plan.AllowedFeeSched==0){
				return -1;
			}
			return Fees.GetAmount(adaCode,plan.AllowedFeeSched);
		}

		///<summary>This is used in FormQuery.SubmitQuery to allow display of carrier names.</summary>
		public static Hashtable GetHListAll(){
			string command="SELECT insplan.PlanNum,carrier.CarrierName "
				+"FROM insplan,carrier "
				+"WHERE insplan.CarrierNum=carrier.CarrierNum";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			Hashtable HListAll=new Hashtable(table.Rows.Count);
			int plannum;
			string carrierName;
			for(int i=0;i<table.Rows.Count;i++){
				plannum=PIn.PInt(table.Rows[i][0].ToString());
				carrierName=PIn.PString(table.Rows[i][1].ToString());
				HListAll.Add(plannum,carrierName);
			}
			return HListAll;
		}





	}

	

	

	


}













