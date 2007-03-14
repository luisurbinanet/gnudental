using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the covpat table in the database.</summary>
	///<remarks>Coverage percentage for a patient.  Each entry in this table is a single percentage value.  A covpat can have a value in ONLY ONE of these three fields: PlanNum, PriPatNum, or SecPatNum.  If it is for a PlanNum, then the percentage is attached to an insurance plan.  If it is one of the others, then it is attached to the coverage for a patient, either primary or secondary, and overrides the plan percentage.</remarks>
	public struct CovPat{  
		///<summary>Primary key.</summary>
		public int CovPatNum;
		///<summary>Foreign key to covcat.CovCatNum.</summary>
		public int CovCatNum;
		///<summary>OPT 1: Foreign key to insplan.PlanNum.</summary>
		public int PlanNum;
		///<summary>OPT 2: Foreign key to patient.PatNum for primary coverage.</summary>
		public int PriPatNum;
		///<summary>OPT 3: Foreign key to patient.PatNum for secondary coverage.</summary>
		public int SecPatNum;
		///<summary>Valid values are 0 to 100. If unknown, the covpat is simply deleted.</summary>
		public int Percent;
	}

	/*=========================================================================================
		=================================== class CovPats ==========================================*/

	///<summary></summary>
	public class CovPats:DataClass{
		//the first two are the usual lists of interest
		///<summary></summary>
		public static int[] PriList;//filled during refresh
		///<summary></summary>
		public static int[] SecList;//filled during refresh
		///<summary></summary>
		public static CovPat[] List;
		///<summary></summary>
		public static CovPat Cur;
		///<summary></summary>
		public static CovPat[] ListForPlan;

		///<summary>Gets the PriList and SecList based on the combination of 4 possible percentages.</summary>
		public static void Refresh(Patient pat,InsPlan[] PlanList){
			cmd.CommandText =
				"SELECT * from covpat"
				+" WHERE PlanNum = '"+pat.PriPlanNum+"'"
				+" || PlanNum = '"+pat.SecPlanNum+"'"
				+" || PriPatNum = '"+pat.PatNum+"'"
				+" || SecPatNum = '"+pat.PatNum+"'";
			FillTable();
			List = new CovPat[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i].CovPatNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].CovCatNum = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].PlanNum   = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].PriPatNum = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].SecPatNum = PIn.PInt   (table.Rows[i][4].ToString());
				List[i].Percent   = PIn.PInt   (table.Rows[i][5].ToString());
			}
			PriList=new int[CovCats.ListShort.Length];
			SecList=new int[CovCats.ListShort.Length];
			for(int i=0;i<CovCats.ListShort.Length;i++){
				PriList[i]=-1;//sets each item to -1(unknown) unless a covpat match is made
				SecList[i]=-1;
			}
			for(int i=0;i<List.Length;i++){//plans first
				if(CovCats.GetOrderShort(List[i].CovCatNum)!=-1
					&& List[i].PlanNum != 0){
					if(List[i].PlanNum==pat.PriPlanNum){
						PriList[CovCats.GetOrderShort(List[i].CovCatNum)]=List[i].Percent;
					}
					if(List[i].PlanNum==pat.SecPlanNum){
						SecList[CovCats.GetOrderShort(List[i].CovCatNum)]=List[i].Percent;
					}
				}
			}
			for(int i=0;i<List.Length;i++){//then Pri & Sec (ok to overwrite plans)
				if(CovCats.GetOrderShort(List[i].CovCatNum)!=-1){
					if(List[i].PriPatNum==pat.PatNum){
						PriList[CovCats.GetOrderShort(List[i].CovCatNum)]=List[i].Percent;
					}
					if(List[i].SecPatNum==pat.PatNum){
						SecList[CovCats.GetOrderShort(List[i].CovCatNum)]=List[i].Percent;
					}
				}
			}
			//flat copay ins plans are always 100% coverage regardless of any percentages present.
			InsPlan PlanCur=InsPlans.GetPlan(pat.PriPlanNum,PlanList);
			if(PlanCur!=null && PlanCur.PlanType=="f"){//flat copay
				for(int i=0;i<PriList.Length;i++){
					PriList[i]=100;//sets all 6 or so categories for this patient to 100%
				}
			}
			PlanCur=InsPlans.GetPlan(pat.SecPlanNum,PlanList);
			if(PlanCur!=null && PlanCur.PlanType=="f"){//flat copay
				for(int i=0;i<SecList.Length;i++){
					SecList[i]=100;//sets all 6 or so categories for this patient to 100%
				}
			}
		}//end method refresh 
		
		///<summary></summary>
		public static void RefreshForPlan(InsPlan PlanCur){
			cmd.CommandText =
				"SELECT * from covpat"
				+" WHERE PlanNum = '"+PlanCur.PlanNum+"'";
			FillTable();
			ListForPlan = new CovPat[table.Rows.Count];
			for(int i=0;i<ListForPlan.Length;i++){
				ListForPlan[i].CovPatNum = PIn.PInt   (table.Rows[i][0].ToString());
				ListForPlan[i].CovCatNum = PIn.PInt   (table.Rows[i][1].ToString());
				ListForPlan[i].PlanNum   = PIn.PInt   (table.Rows[i][2].ToString());
				ListForPlan[i].PriPatNum = PIn.PInt   (table.Rows[i][3].ToString());
				ListForPlan[i].SecPatNum = PIn.PInt   (table.Rows[i][4].ToString());
				ListForPlan[i].Percent   = PIn.PInt   (table.Rows[i][5].ToString());
			}
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
			cmd.CommandText+="CovCatNum,PlanNum,PriPatNum,"
				+"SecPatNum,Percent) VALUES(";
			if(Prefs.RandomKeys){
				cmd.CommandText+="'"+POut.PInt(Cur.CovPatNum)+"', ";
			}
			cmd.CommandText+=
				 "'"+POut.PInt   (Cur.CovCatNum)+"', "
				+"'"+POut.PInt   (Cur.PlanNum)+"', "
				+"'"+POut.PInt   (Cur.PriPatNum)+"', "
				+"'"+POut.PInt   (Cur.SecPatNum)+"', "
				+"'"+POut.PInt   (Cur.Percent)+"')";
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
				+",pripatnum = '" +POut.PInt   (Cur.PriPatNum)+"' "
				+",secpatnum = '" +POut.PInt   (Cur.SecPatNum)+"' "
				+",percent = '"   +POut.PInt   (Cur.Percent)+"' "
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









