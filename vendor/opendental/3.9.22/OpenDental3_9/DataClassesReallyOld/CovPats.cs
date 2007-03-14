using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the covpat table in the database.</summary>
	///<remarks>Coverage percentage for a patient.  Each entry in this table is a single percentage value.  A covpat can have a value in EITHER PlanNum OR PatPlanNum.  If it is for a PlanNum, then the percentage is attached to an insurance plan.  If it is for a PatPlanNum, then it overrides the plan percentage.</remarks>
	public struct CovPat{  
		///<summary>Primary key.</summary>
		public int CovPatNum;
		///<summary>Foreign key to covcat.CovCatNum.</summary>
		public int CovCatNum;
		///<summary>Foreign key to insplan.PlanNum.  If this is used, then PatPlanNum should be 0.</summary>
		public int PlanNum;
		///<summary>Valid values are 0 to 100. If unknown, the covpat is simply deleted.</summary>
		public int Percent;
		///<summary>Foreign key to patplan.PatPlanNum.  Only used when overriding a plan percentage.  In that case, PlanNum should be 0.</summary>
		public int PatPlanNum;
	}

	/*=========================================================================================
		=================================== class CovPats ==========================================*/

	///<summary></summary>
	public class CovPats:DataClass{
		//the first two are the usual lists of interest
		///<summary>Filled during refresh</summary>
		public static int[] PriList;
		///<summary>Filled during refresh</summary>
		public static int[] SecList;
		///<summary></summary>
		public static CovPat Cur;

		///<summary>Gets a list of all CovPat entries which are in use by one patient.  Only for insplans which are currently attached to this patient.  Use GetListForPlan for a plan that is not attached to a patient.  The supplied patPlans list should be all patPlans for the patient.  Used in Refresh.  Also used in FormInsPlan.</summary>
		public static CovPat[] GetList(PatPlan[] patPlans){
			if(patPlans.Length==0){
				return new CovPat[] {};
			}
			cmd.CommandText="SELECT * FROM covpat WHERE";
			for(int i=0;i<patPlans.Length;i++){
				//PlanNum
				if(i>0){
					cmd.CommandText+=" OR";
				}
				cmd.CommandText+=" PlanNum="+POut.PInt(patPlans[i].PlanNum);
				//PatPlanNum
				cmd.CommandText+=" OR PatPlanNum="+POut.PInt(patPlans[i].PatPlanNum);
			}
			FillTable();
			CovPat[] List = new CovPat[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i].CovPatNum  = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].CovCatNum  = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].PlanNum    = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].Percent    = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].PatPlanNum = PIn.PInt   (table.Rows[i][4].ToString());
			}
			return List;
		}

		///<summary>Gets the PriList and SecList based on the combination of percentages from covpat entries.</summary>
		public static void Refresh(InsPlan[] PlanList,PatPlan[] patPlans){
			CovPat[] List=GetList(patPlans);			
			PriList=new int[CovCats.ListShort.Length];
			SecList=new int[CovCats.ListShort.Length];
			for(int i=0;i<CovCats.ListShort.Length;i++){
				PriList[i]=-1;//sets each item to -1(unknown) unless a covpat match is made
				SecList[i]=-1;
			}
			for(int i=0;i<List.Length;i++){//plans first
				if(List[i].PlanNum==0){
					continue;
				}
				if(CovCats.GetOrderShort(List[i].CovCatNum)==-1){
					continue;
				}
				if(List[i].PlanNum==PatPlans.GetPlanNum(patPlans,1)){
					PriList[CovCats.GetOrderShort(List[i].CovCatNum)]=List[i].Percent;
				}
				if(List[i].PlanNum==PatPlans.GetPlanNum(patPlans,2)){
					SecList[CovCats.GetOrderShort(List[i].CovCatNum)]=List[i].Percent;
				}
			}
			PatPlan tempPatPlan;
			for(int i=0;i<List.Length;i++){//then Pri & Sec (ok to overwrite plans)
				if(List[i].PatPlanNum==0){
					continue;
				}
				if(CovCats.GetOrderShort(List[i].CovCatNum)==-1){
					continue;
				}
				tempPatPlan=PatPlans.GetFromList(patPlans,List[i].PatPlanNum);
				if(tempPatPlan==null){
					continue;
				}
				if(tempPatPlan.Ordinal==1){
					PriList[CovCats.GetOrderShort(List[i].CovCatNum)]=List[i].Percent;
				}
				if(tempPatPlan.Ordinal==2){
					SecList[CovCats.GetOrderShort(List[i].CovCatNum)]=List[i].Percent;
				}
			}
			//flat copay ins plans are always 100% coverage regardless of any percentages present.
			InsPlan PlanCur=InsPlans.GetPlan(PatPlans.GetPlanNum(patPlans,1),PlanList);
			if(PlanCur!=null && PlanCur.PlanType=="f"){//flat copay
				for(int i=0;i<PriList.Length;i++){
					PriList[i]=100;//sets all 6 or so categories for this patient to 100%
				}
			}
			PlanCur=InsPlans.GetPlan(PatPlans.GetPlanNum(patPlans,2),PlanList);
			if(PlanCur!=null && PlanCur.PlanType=="f"){//flat copay
				for(int i=0;i<SecList.Length;i++){
					SecList[i]=100;//sets all 6 or so categories for this patient to 100%
				}
			}
		}//end method refresh 
		
		///<summary>Gets a list directly from the database of all covpats for this plan.</summary>
		public static CovPat[] GetListForPlan(int planNum){
			cmd.CommandText=
				"SELECT * FROM covpat"
				+" WHERE PlanNum="+POut.PInt(planNum);
			FillTable();
			CovPat[] ListForPlan=new CovPat[table.Rows.Count];
			for(int i=0;i<ListForPlan.Length;i++){
				ListForPlan[i].CovPatNum = PIn.PInt   (table.Rows[i][0].ToString());
				ListForPlan[i].CovCatNum = PIn.PInt   (table.Rows[i][1].ToString());
				ListForPlan[i].PlanNum   = PIn.PInt   (table.Rows[i][2].ToString());
				ListForPlan[i].Percent   = PIn.PInt   (table.Rows[i][3].ToString());
				ListForPlan[i].PatPlanNum= PIn.PInt   (table.Rows[i][4].ToString());
			}
			return ListForPlan;
		}

		///<summary></summary>
		public static void InsertCur(){
			if(Prefs.RandomKeys){
				Cur.CovPatNum=MiscData.GetKey("covpat","CovPatNum");
			}
			cmd.CommandText="INSERT INTO covpat (";
			if(Prefs.RandomKeys){
				cmd.CommandText+="CovPatNum,";
			}
			cmd.CommandText+="CovCatNum,PlanNum,Percent,PatPlanNum) VALUES(";
			if(Prefs.RandomKeys){
				cmd.CommandText+="'"+POut.PInt(Cur.CovPatNum)+"', ";
			}
			cmd.CommandText+=
				 "'"+POut.PInt   (Cur.CovCatNum)+"', "
				+"'"+POut.PInt   (Cur.PlanNum)+"', "
				+"'"+POut.PInt   (Cur.Percent)+"', "
				+"'"+POut.PInt   (Cur.PatPlanNum)+"')";
			if(Prefs.RandomKeys){
				NonQ();
			}
			else{
 				NonQ(true);
				Cur.CovPatNum=InsertID;
			}
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE covpat SET "
				+"covcatnum = '" +POut.PInt   (Cur.CovCatNum)+"' "
				+",plannum = '"   +POut.PInt   (Cur.PlanNum)+"' "
				+",percent = '"   +POut.PInt   (Cur.Percent)+"' "
				+",PatPlanNum = '"+POut.PInt   (Cur.PatPlanNum)+"' "
				+"WHERE covpatnum = '"+POut.PInt  (Cur.CovPatNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM covpat WHERE covpatnum = '"+Cur.CovPatNum.ToString()+"'";
			NonQ(false);
		}

		//public static void DeleteAllInCurPlan(){//obsolete
		//	cmd.CommandText = "DELETE FROM covpat WHERE plannum = '"+InsPlans.Cur.PlanNum.ToString()+"'";
		//	NonQ(false);
		//}

		///<summary>Only use pri or sec, not tot.  Used from ClaimProc.ComputeBaseEst. This is a low level function to get the percent to store in a claimproc.  It does not consider any percentOverride.  Always returns a number between 0 and 100.</summary>
		public static int GetPercent(string myADACode, PriSecTot pst){
			int retVal=0;
			int covCatNum=0;
			for(int i=0;i<CovSpans.List.Length;i++){
				if(String.Compare(myADACode,CovSpans.List[i].FromCode)>=0
					&& String.Compare(myADACode,CovSpans.List[i].ToCode)<=0){
					covCatNum=CovSpans.List[i].CovCatNum;
				}
			}
			int priPercent=0;
			int secPercent=0;
			for(int i=0;i<CovCats.ListShort.Length;i++){
				if(covCatNum==CovCats.ListShort[i].CovCatNum){
					if(PriList[i]==-1)
						priPercent=0;
					else
						priPercent=PriList[i];
					if(SecList[i]==-1)
						secPercent=0;
					else
						secPercent=SecList[i];
				}
			}
			if(pst==PriSecTot.Pri){
				retVal=priPercent;
			}
			else{
				retVal=secPercent;
			}
			return retVal;	
		}
		
		/*public double GetCatPercent(int myCovCatNum){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(myCovCatNum==List[i].CovCatNum){
					if(List[i].Percent!=0) retVal=(double)List[i].Percent/100;
				}
			}
			return retVal;	
		}*/

	}

	

	

}









