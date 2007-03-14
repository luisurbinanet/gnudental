using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	/// <summary>Corresponds to the patplan table in the database.  Each row represents the linking of one insplan to one patient for current coverage.  Dropping a plan will delete the entry in this table.  Deleting a plan will delte the actual insplan (if no dependencies).</summary>
	public class PatPlan{
		/// <summary>Primary key</summary>
		public int PatPlanNum;
		/// <summary>Foreign key to  patient.PatNum.  The patient who currently has the insurance.  Not the same as the subscriber.</summary>
		public int PatNum;
		///<summary>Foreign key to insplan.PlanNum.  The insurance plan attached to the patient.</summary>
		public int PlanNum;
		///<summary>Number like 1, 2, 3, etc.  Represents primary ins, secondary ins, tertiary ins, etc. 0 is not used</summary>
		public int Ordinal;
		///<summary>For informational purposes only. For now, we lose the previous feature which let us set isPending without entering a plan.  Now, you have to enter the plan in order to check this box.</summary>
		public bool IsPending;
		///<summary>See the Relat enum.</summary>
		public Relat Relationship;
		///<summary>An optional patient ID which will override the SSN on eclaims if present.</summary>
		public string PatID;

		///<summary></summary>
		public PatPlan Copy(){
			PatPlan p=new PatPlan();
			p.PatPlanNum=PatPlanNum;
			p.PatNum=PatNum;
			p.PlanNum=PlanNum;
			p.Ordinal=Ordinal;
			p.IsPending=IsPending;
			p.Relationship=Relationship;
			p.PatID=PatID;
			return p;
		}

		///<summary></summary>
		public void Update(){
			string command="UPDATE patplan SET " 
				+"PatNum = '"       +POut.PInt   (PatNum)+"'"
				+",PlanNum = '"     +POut.PInt   (PlanNum)+"'"
				//+",Ordinal = '"     +POut.PInt   (Ordinal)+"'"//ordinal always set using SetOrdinal
				+",IsPending = '"   +POut.PBool  (IsPending)+"'"
				+",Relationship = '"+POut.PInt   ((int)Relationship)+"'"
				+",PatID = '"       +POut.PString(PatID)+"'"
				+" WHERE PatPlanNum = '" +POut.PInt(PatPlanNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Insert(){
			if(Prefs.RandomKeys){
				PatPlanNum=MiscData.GetKey("patplan","PatPlanNum");
			}
			string command="INSERT INTO patplan (";
			if(Prefs.RandomKeys){
				command+="PatPlanNum,";
			}
			command+="PatNum,PlanNum,Ordinal,IsPending,Relationship,PatID) VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(PatPlanNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (PatNum)+"', "
				+"'"+POut.PInt   (PlanNum)+"', "
				+"'"+POut.PInt   (Ordinal)+"', "
				+"'"+POut.PBool  (IsPending)+"', "
				+"'"+POut.PInt   ((int)Relationship)+"', "
				+"'"+POut.PString(PatID)+"')";
			DataConnection dcon=new DataConnection();
			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				PatPlanNum=dcon.InsertID;
			}
		}

		/*  Do NOT use this.  Use PatPlans.Delete() instead.
		///<summary></summary>
		public void Delete(){
			string command="DELETE FROM patplan WHERE PatPlanNum ="+POut.PInt(PatPlanNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}*/

	}

	/*=========================================================================================
	=================================== class PatPlans ==========================================*/

	///<summary></summary>
	public class PatPlans{
		///<summary>Gets a list of all patplans for a given patient</summary>
		public static PatPlan[] Refresh(int patNum){
			string command="SELECT * from patplan"
				+" WHERE PatNum = "+patNum.ToString()
				+" ORDER BY Ordinal";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			PatPlan[] List=new PatPlan[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new PatPlan();
				List[i].PatPlanNum  = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum      = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].PlanNum     = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].Ordinal     = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].IsPending   = PIn.PBool  (table.Rows[i][4].ToString());
				List[i].Relationship= (Relat)PIn.PInt   (table.Rows[i][5].ToString());
				List[i].PatID       = PIn.PString(table.Rows[i][6].ToString());
			}
			return List;
		}

		///<summary>Supply a PatPlan list.  This function loops through the list and returns the plan num of the specified ordinal.  If ordinal not valid, then it returns 0.  The main purpose of this function is so we don't have to check the length of the list.</summary>
		public static int GetPlanNum(PatPlan[] list,int ordinal){
			for(int i=0;i<list.Length;i++){
				if(list[i].Ordinal==ordinal){
					return list[i].PlanNum;
				}
			}
			return 0;
		}

		///<summary>Supply a PatPlan list.  This function loops through the list and returns the relationship of the specified ordinal.  If ordinal not valid, then it returns self (0).</summary>
		public static Relat GetRelat(PatPlan[] list,int ordinal){
			for(int i=0;i<list.Length;i++){
				if(list[i].Ordinal==ordinal){
					return list[i].Relationship;
				}
			}
			return Relat.Self;
		}

		///<summary>Deletes the patplan with the specified patPlanNum.  Rearranges the other patplans for the patient to keep the ordinal sequence contiguous.  Then, recomputes all estimates for this patient because their coverage is now different.  Also sets patient.HasIns to the correct value.</summary>
		public static void Delete(int patPlanNum){
			string command="SELECT PatNum FROM patplan WHERE PatPlanNum="+POut.PInt(patPlanNum);
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count==0){
				return;
			}
			int patNum=PIn.PInt(table.Rows[0][0].ToString());
			PatPlan[] patPlans=Refresh(patNum);
			bool doDecrement=false;
			for(int i=0;i<patPlans.Length;i++){
				if(doDecrement){//patPlan has already been deleted, so decrement the rest.
					command="UPDATE patplan SET Ordinal="+POut.PInt(patPlans[i].Ordinal-1)
						+" WHERE PatPlanNum="+POut.PInt(patPlans[i].PatPlanNum);
					dcon.NonQ(command);
					continue;
				}
				if(patPlans[i].PatPlanNum==patPlanNum){
					command="DELETE FROM patplan WHERE PatPlanNum="+POut.PInt(patPlanNum);
					dcon.NonQ(command);
					command="DELETE FROM benefit WHERE PatPlanNum=" +POut.PInt(patPlanNum);
					dcon.NonQ(command);
					doDecrement=true;
				}
			}
			Family fam=Patients.GetFamily(patNum);
			Patient pat=fam.GetPatient(patNum);
			ClaimProc[] claimProcs=ClaimProcs.Refresh(patNum);
			Procedure[] procs=Procedures.Refresh(patNum);
			patPlans=PatPlans.Refresh(patNum);
			InsPlan[] planList=InsPlans.Refresh(fam);
			Benefit[] benList=Benefits.Refresh(patPlans);
			Procedures.ComputeEstimatesForAll(patNum,claimProcs,procs,planList,patPlans,benList);
			Patients.SetHasIns(patNum);
		}

		///<summary>Sets the ordinal of the specified patPlan.  Rearranges the other patplans for the patient to keep the ordinal sequence contiguous.  Estimates must be recomputed after this.  FormInsPlan currently updates estimates every time it closes.</summary>
		public static void SetOrdinal(int patPlanNum,int newOrdinal){
			string command="SELECT PatNum FROM patplan WHERE PatPlanNum="+POut.PInt(patPlanNum);
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count==0){
				return;
			}
			int patNum=PIn.PInt(table.Rows[0][0].ToString());
			PatPlan[] patPlans=Refresh(patNum);
			//int oldOrdinal=GetFromList(patPlans,patPlanNum).Ordinal;
			if(newOrdinal>patPlans.Length){
				newOrdinal=patPlans.Length;
			}
			if(newOrdinal<1){
				newOrdinal=1;
			}
			int curOrdinal=1;
			for(int i=0;i<patPlans.Length;i++){//Loop through each patPlan.
				if(patPlans[i].PatPlanNum==patPlanNum){
					continue;//the one we are setting will be handled later
				}
				if(curOrdinal==newOrdinal){
					curOrdinal++;//skip the newOrdinal when setting the sequence for the others.
				}
				command="UPDATE patplan SET Ordinal="+POut.PInt(curOrdinal)
					+" WHERE PatPlanNum="+POut.PInt(patPlans[i].PatPlanNum);
				dcon.NonQ(command);
				curOrdinal++;
			}
			command="UPDATE patplan SET Ordinal="+POut.PInt(newOrdinal)
				+" WHERE PatPlanNum="+POut.PInt(patPlanNum);
			dcon.NonQ(command);
		}

		///<summary>Loops through the supplied list to find the one patplan needed.</summary>
		public static PatPlan GetFromList(PatPlan[] patPlans,int patPlanNum){
			for(int i=0;i<patPlans.Length;i++){
				if(patPlans[i].PatPlanNum==patPlanNum){
					return patPlans[i].Copy();
				}
			}
			return null;
		}

		///<summary>Loops through the supplied list to find the one patplanNum needed based on the planNum.  Returns 0 if patient is not currently covered by the planNum supplied.  Only used once in Claims.cs.</summary>
		public static int GetPatPlanNum(PatPlan[] patPlans,int planNum) {
			for(int i=0;i<patPlans.Length;i++) {
				if(patPlans[i].PlanNum==planNum) {
					return patPlans[i].PatPlanNum;
				}
			}
			return 0;
		}



		
	}

	

	


}










