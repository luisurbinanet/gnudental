
using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the insplan table in the database. All insPlans MUST be linked to an insTemplate.</summary>
	public struct InsPlan{
		///<summary>Primary key.</summary>
		public int PlanNum;
		///<summary>Foreign key to patient.PatNum.</summary>
		public int Subscriber;
		///<summary>No longer used. See CarrierNum column instead.</summary>
		public string Carrier;
		///<summary>Date plan became effective</summary>
		public DateTime DateEffective;
		///<summary>Date plan was terminated</summary>
		public DateTime DateTerm;
		///<summary>No longer used. See CarrierNum column instead.</summary>
		public string Phone;
		///<summary>Optional</summary>
		public string GroupName;
		///<summary></summary>
		public string GroupNum;
		///<summary>No longer used. See CarrierNum column instead.</summary>
		public string Address;
		///<summary>No longer used. See CarrierNum column instead.</summary>
		public string Address2;
		///<summary>No longer used. See CarrierNum column instead.</summary>
		public string City;
		///<summary>No longer used. See CarrierNum column instead.</summary>
		public string State;
		///<summary>No longer used. See CarrierNum column instead.</summary>
		public string Zip;
		///<summary>No longer used. See CarrierNum column instead.</summary>
		public bool NoSendElect;
		///<summary>No longer used. See CarrierNum column instead.</summary>
		public string ElectID;
		///<summary>No longer used! Must use the EmployerNum column instead.</summary>
		public string Employer;
		///<summary>Annual maximum.</summary>
		public int AnnualMax;
		///<summary>Renewal month. Valid 1-12.  For instance, 1 means January.</summary>
		public int RenewMonth;
		///<summary>Amount of deductible per year.</summary>
		public int Deductible;
		///<summary>See the YN enum. 0=unknown, 1=Yes-covered,2=No-not covered.</summary>
		public YN DeductWaivPrev;
		///<summary>Annual maximum for ortho.</summary>
		public int OrthoMax;
		///<summary>Fluoride covered to age...  Use 99 for all ages covered.</summary>
		public int FloToAge;
		///<summary>Note for this plan.  Any other info that affects coverage.</summary>
		public string PlanNote;
		///<summary>Is there a missing tooth exclusion.</summary>
		public YN MissToothExcl;
		///<summary>Is there a waiting period on major treatment.  Specifics can go in notes.</summary>
		public YN MajorWait;
		///<summary>Foreign key to definition.DefNum.  Name of fee schedule is stored in definition.ItemName.</summary>
		public int FeeSched;
		///<summary>Release of information signature is on file.</summary>
		public bool ReleaseInfo;
		///<summary>Assignment of benefits signature is on file.</summary>
		public bool AssignBen;
		///<summary>""=percentage(the default),"f"=flatCopay,"c"=capitation.</summary>
		public string PlanType;
		///<summary>Foreign key to claimform.ClaimFormNum. eg. "0" for ADA2002</summary>
		public int ClaimFormNum;
		///<summary>0=no,1=yes.  could later be extended if more alternates required</summary>
		public bool UseAltCode;
		///<summary>Fee billed on claim should be the UCR fee for the patient's provider.</summary>
		public bool ClaimsUseUCR;
		///<summary>This field is not used at all.</summary>
		public bool IsWrittenOff;//
		///<summary>Foreign key to Definition.DefNum. This fee schedule holds only co-pays(patient portions).</summary>
		public int CopayFeeSched;
		///<summary>Usually SSN, but can also be changed by user.  No dashes. Not allowed to be blank.</summary>
		public string SubscriberID;
		///<summary>Foreign key to employer.EmployerNum.</summary>
		public int EmployerNum;
		///<summary>Foreign key to carrier.CarrierNum.</summary>
		public int CarrierNum;
		///<summary>This is NOT a database column.  It is just used to display the number of plans with the same info.</summary>
		public int NumberPlans;
	}

	/*=========================================================================================
		=================================== Class InsPlans ===========================================*/
	///<summary></summary>
	public class InsPlans:DataClass{
		///<summary>List for one family</summary>
		public static InsPlan[] List;
		private static Hashtable HList;//this will have to be private because we can never guarantee
					//that a given insplan will already be loaded and available
		///<summary></summary>
		public static Hashtable HListAll;
		///<summary></summary>
		public static InsPlan Cur;
		///<summary>Used to display a list of all non duplicate plans. Like the old template table.</summary>
		public static InsPlan[] ListAll;

		///<summary>Leaves the List intact, and only loads one plan from db into Cur.</summary>
		public static void Refresh(int planNum){
			Cur=new InsPlan();//just in case no rows are returned
			if(planNum==0) return;
			cmd.CommandText="SELECT * FROM insplan WHERE plannum = '"+planNum+"'";
			RefreshFill(true);
		}

		///<summary>Gets new List for the current family.  Family must have been loaded properly first.</summary>
		public static void Refresh(){
			//subscribers in family
			string s="subscriber='"+Patients.FamilyList[0].PatNum+"'";
			for(int i=1;i<Patients.FamilyList.Length;i++){
				s+=" || subscriber='"+Patients.FamilyList[i].PatNum+"'";
			}
			//plans in family(usually lots of duplicates of subscribers, but this also allows mixing families
			//the only plans it misses are for claims with no current coverage.  These are handled as needed.
			string plans="";//="subscriber='"+Patients.FamilyList[0].PatNum+"'";
			for(int i=0;i<Patients.FamilyList.Length;i++){
				//if(i>0) plans+=" ||";
				if(Patients.FamilyList[i].PriPlanNum > 0)
					plans+=" || plannum = '"+Patients.FamilyList[i].PriPlanNum+"'";
				if(Patients.FamilyList[i].SecPlanNum > 0)
					plans+=" || plannum = '"+Patients.FamilyList[i].SecPlanNum+"'";
			}
			//MessageBox.Show(plans);
			cmd.CommandText =
				"SELECT * from insplan "
				+"WHERE "+s+plans
				+" ORDER BY dateeffective";
			RefreshFill(false);
		}

		private static void RefreshFill(bool isOnePlan){
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			if(!isOnePlan){
				HList=new Hashtable();
				List =new InsPlan[table.Rows.Count];
			}
			InsPlan tempPlan=new InsPlan();
			if(isOnePlan && table.Rows.Count==0){//plan not found
				Cur=new InsPlan();
				return;
			}
			for (int i=0;i<table.Rows.Count;i++){
				tempPlan=new InsPlan();
				tempPlan.PlanNum      = PIn.PInt   (table.Rows[i][0].ToString());
				tempPlan.Subscriber   = PIn.PInt   (table.Rows[i][1].ToString());
				tempPlan.Carrier      = PIn.PString(table.Rows[i][2].ToString());
				tempPlan.DateEffective= PIn.PDate  (table.Rows[i][3].ToString());
				tempPlan.DateTerm     = PIn.PDate  (table.Rows[i][4].ToString());
				tempPlan.Phone        = PIn.PString(table.Rows[i][5].ToString());
				tempPlan.GroupName    = PIn.PString(table.Rows[i][6].ToString());
				tempPlan.GroupNum     = PIn.PString(table.Rows[i][7].ToString());
				tempPlan.Address      = PIn.PString(table.Rows[i][8].ToString());
				tempPlan.Address2     = PIn.PString(table.Rows[i][9].ToString());
				tempPlan.City         = PIn.PString(table.Rows[i][10].ToString());
				tempPlan.State        = PIn.PString(table.Rows[i][11].ToString());
				tempPlan.Zip          = PIn.PString(table.Rows[i][12].ToString());
				tempPlan.NoSendElect  = PIn.PBool  (table.Rows[i][13].ToString());
				tempPlan.ElectID      = PIn.PString(table.Rows[i][14].ToString());
				tempPlan.Employer     = PIn.PString(table.Rows[i][15].ToString());
				tempPlan.AnnualMax    = PIn.PInt   (table.Rows[i][16].ToString());
				tempPlan.RenewMonth   = PIn.PInt   (table.Rows[i][17].ToString());
				tempPlan.Deductible   = PIn.PInt   (table.Rows[i][18].ToString());
				tempPlan.DeductWaivPrev=(YN)PIn.PInt(table.Rows[i][19].ToString());
				tempPlan.OrthoMax     = PIn.PInt    (table.Rows[i][20].ToString());
				tempPlan.FloToAge     = PIn.PInt    (table.Rows[i][21].ToString());
				tempPlan.PlanNote     = PIn.PString (table.Rows[i][22].ToString());
				tempPlan.MissToothExcl= (YN)PIn.PInt(table.Rows[i][23].ToString());
				tempPlan.MajorWait    = (YN)PIn.PInt(table.Rows[i][24].ToString());
				tempPlan.FeeSched     = PIn.PInt    (table.Rows[i][25].ToString());
				tempPlan.ReleaseInfo  = PIn.PBool   (table.Rows[i][26].ToString());
				tempPlan.AssignBen    = PIn.PBool   (table.Rows[i][27].ToString());
				tempPlan.PlanType     = PIn.PString (table.Rows[i][28].ToString());
				tempPlan.ClaimFormNum = PIn.PInt    (table.Rows[i][29].ToString());
				tempPlan.UseAltCode   = PIn.PBool   (table.Rows[i][30].ToString());
				tempPlan.ClaimsUseUCR = PIn.PBool   (table.Rows[i][31].ToString());
				tempPlan.IsWrittenOff = PIn.PBool   (table.Rows[i][32].ToString());
				tempPlan.CopayFeeSched= PIn.PInt    (table.Rows[i][33].ToString());
				tempPlan.SubscriberID = PIn.PString (table.Rows[i][34].ToString());
				tempPlan.EmployerNum  = PIn.PInt    (table.Rows[i][35].ToString());
				//tempPlan.TemplateNum  = PIn.PInt    (table.Rows[i][36].ToString());
				tempPlan.CarrierNum   = PIn.PInt    (table.Rows[i][36].ToString());
				if(isOnePlan){
					Cur=tempPlan;
				}
				else{
					List[i]=tempPlan;
					HList.Add(List[i].PlanNum,List[i]);
				}
			}//for
		}//RefreshFill

		///<summary></summary>
		public static void RefreshListAll(bool byEmployer){
			if(byEmployer){
				cmd.CommandText =
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
				cmd.CommandText =
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
			FillTable();
			ListAll=new InsPlan[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
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
		}

		//To decide whether to use InsertCur or UpdateCur, you have to test first
		//for value of PlanNum
		///<summary></summary>
		public static void InsertCur(){//only for new plans
			cmd.CommandText = "INSERT INTO insplan (subscriber, carrier, "
				+"dateeffective,dateterm,phone,groupname,groupnum,address,address2,city,state,zip,"
				+"nosendelect,electid,employer,annualmax,renewmonth,deductible,"
				+"deductwaivprev,orthomax,"
				+"flotoage,plannote,misstoothexcl,majorwait,feesched,"
				+"releaseinfo,assignben,plantype,claimformnum,usealtcode,"
				+"claimsuseucr,iswrittenoff,copayfeesched,subscriberid,"
				+"EmployerNum,CarrierNum) VALUES("
				//+"'"+POut.PInt   (Cur.PlanOrder)+"', "
				+"'"+POut.PInt   (Cur.Subscriber)+"', "
				+"'"+POut.PString(Cur.Carrier)+"', "
				+"'"+POut.PDate  (Cur.DateEffective)+"', "
				+"'"+POut.PDate  (Cur.DateTerm)+"', "
				+"'"+POut.PString(Cur.Phone)+"', "
				+"'"+POut.PString(Cur.GroupName)+"', "
				+"'"+POut.PString(Cur.GroupNum)+"', "
				+"'"+POut.PString(Cur.Address)+"', "
				+"'"+POut.PString(Cur.Address2)+"', "
				+"'"+POut.PString(Cur.City)+"', "
				+"'"+POut.PString(Cur.State)+"', "
				+"'"+POut.PString(Cur.Zip)+"', "
				+"'"+POut.PBool  (Cur.NoSendElect)+"', "
				+"'"+POut.PString(Cur.ElectID)+"', "
				+"'"+POut.PString(Cur.Employer)+"', "
				+"'"+POut.PInt   (Cur.AnnualMax)+"', "
				+"'"+POut.PInt   (Cur.RenewMonth)+"', "
				+"'"+POut.PInt   (Cur.Deductible)+"', "
				+"'"+POut.PInt   ((int)Cur.DeductWaivPrev)+"', "
				+"'"+POut.PInt   (Cur.OrthoMax)+"', "
				+"'"+POut.PInt   (Cur.FloToAge)+"', "
				+"'"+POut.PString(Cur.PlanNote)+"', "
				+"'"+POut.PInt   ((int)Cur.MissToothExcl)+"', "
				+"'"+POut.PInt   ((int)Cur.MajorWait)+"', "
				+"'"+POut.PInt   (Cur.FeeSched)+"', "
				+"'"+POut.PBool  (Cur.ReleaseInfo)+"', "
				+"'"+POut.PBool  (Cur.AssignBen)+"', "
				+"'"+POut.PString(Cur.PlanType)+"', "
				+"'"+POut.PInt   (Cur.ClaimFormNum)+"', "
				+"'"+POut.PBool  (Cur.UseAltCode)+"', "
				+"'"+POut.PBool  (Cur.ClaimsUseUCR)+"', "
				+"'"+POut.PBool  (Cur.IsWrittenOff)+"', "
				+"'"+POut.PInt   (Cur.CopayFeeSched)+"', "
				+"'"+POut.PString(Cur.SubscriberID)+"', "
				+"'"+POut.PInt   (Cur.EmployerNum)+"', "
				//+"'"+POut.PInt   (Cur.TemplateNum)+"', "
				+"'"+POut.PInt   (Cur.CarrierNum)+"')";
			NonQ(true);
			Cur.PlanNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE insplan SET " 
				//+ "PlanOrder = '"     +POut.PInt   (Cur.PlanOrder)+"'"
				+ "Subscriber = '"   +POut.PInt   (Cur.Subscriber)+"'"
				+ ",Carrier = '"      +POut.PString(Cur.Carrier)+"'"
				+ ",DateEffective = '"+POut.PDate  (Cur.DateEffective)+"'"
				+ ",DateTerm = '"     +POut.PDate  (Cur.DateTerm)+"'"
				+ ",Phone = '"        +POut.PString(Cur.Phone)+"'"
				+ ",GroupName = '"    +POut.PString(Cur.GroupName)+"'"
				+ ",GroupNum = '"     +POut.PString(Cur.GroupNum)+"'"
				+ ",Address = '"      +POut.PString(Cur.Address)+"'"
				+ ",Address2 = '"     +POut.PString(Cur.Address2)+"'"
				+ ",City = '"         +POut.PString(Cur.City)+"'"
				+ ",State = '"        +POut.PString(Cur.State)+"'"
				+ ",Zip = '"          +POut.PString(Cur.Zip)+"'"
				+ ",NoSendElect = '"  +POut.PBool  (Cur.NoSendElect)+"'"
				+ ",ElectID = '"      +POut.PString(Cur.ElectID)+"'"
				+ ",Employer = '"     +POut.PString(Cur.Employer)+"'"
				+ ",AnnualMax = '"    +POut.PInt   (Cur.AnnualMax)+"'"
				+ ",RenewMonth = '"   +POut.PInt   (Cur.RenewMonth)+"'"
				+ ",Deductible = '"   +POut.PInt   (Cur.Deductible)+"'"
				+ ",DeductWaivPrev= '"+POut.PInt   ((int)Cur.DeductWaivPrev)+"'"
				+ ",OrthoMax = '"     +POut.PInt   (Cur.OrthoMax)+"'"
				+ ",FloToAge = '"     +POut.PInt   (Cur.FloToAge)+"'"
				+ ",PlanNote = '"     +POut.PString(Cur.PlanNote)+"'"
				+ ",MissToothExcl = '"+POut.PInt   ((int)Cur.MissToothExcl)+"'"
				+ ",MajorWait = '"    +POut.PInt   ((int)Cur.MajorWait)+"'"
				+ ",feesched = '"     +POut.PInt   (Cur.FeeSched)+"'"
				+ ",releaseinfo = '"  +POut.PBool  (Cur.ReleaseInfo)+"'"
				+ ",assignben = '"    +POut.PBool  (Cur.AssignBen)+"'"
				+ ",plantype = '"     +POut.PString(Cur.PlanType)+"'"
				+ ",claimformnum = '" +POut.PInt   (Cur.ClaimFormNum)+"'"
				+ ",usealtcode = '"   +POut.PBool  (Cur.UseAltCode)+"'"
				+ ",claimsuseucr = '" +POut.PBool  (Cur.ClaimsUseUCR)+"'"
				+ ",iswrittenoff = '" +POut.PBool  (Cur.IsWrittenOff)+"'"
				+ ",copayfeesched = '"+POut.PInt   (Cur.CopayFeeSched)+"'"
				+ ",subscriberid = '" +POut.PString(Cur.SubscriberID)+"'"
				+ ",EmployerNum = '"  +POut.PInt   (Cur.EmployerNum)+"'"
				//+ ",TemplateNum = '"  +POut.PInt   (Cur.TemplateNum)+"'"
				+ ",CarrierNum = '"   +POut.PInt   (Cur.CarrierNum)+"'"
				+" WHERE PlanNum = '" +POut.PInt(Cur.PlanNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ();
		}

		///<summary>Called from FormInsPlanEditAll. This updates the synchronized fields for all plans like the specified insPlan.  Cur must be set to the new values that we want.</summary>
		public static void UpdateForLike(InsPlan like){
			cmd.CommandText = "UPDATE insplan SET "
				+"EmployerNum = '"   +POut.PInt   (Cur.EmployerNum)+"'"
				+",GroupName = '"    +POut.PString(Cur.GroupName)+"'"
				+",GroupNum = '"     +POut.PString(Cur.GroupNum)+"'"
				+",CarrierNum = '"   +POut.PInt   (Cur.CarrierNum)+"'"
				+",PlanType = '"     +POut.PString(Cur.PlanType)+"'"
				+",UseAltCode = '"   +POut.PBool  (Cur.UseAltCode)+"'"
				+",ClaimsUseUcr = '" +POut.PBool  (Cur.ClaimsUseUCR)+"'"
				+",FeeSched = '"     +POut.PInt   (Cur.FeeSched)+"'"
				+",CopayFeeSched = '"+POut.PInt   (Cur.CopayFeeSched)+"'"
				+",ClaimFormNum = '" +POut.PInt   (Cur.ClaimFormNum)+"'"
				+" WHERE "
				+"EmployerNum = '"     +POut.PInt   (like.EmployerNum)+"' "
				+"&& GroupName = '"    +POut.PString(like.GroupName)+"' "
				+"&& GroupNum = '"     +POut.PString(like.GroupNum)+"' "
				+"&& CarrierNum = '"   +POut.PInt   (like.CarrierNum)+"' "
				+"&& PlanType = '"     +POut.PString(like.PlanType)+"' "
				+"&& UseAltCode = '"   +POut.PBool  (like.UseAltCode)+"' "
				+"&& ClaimsUseUCR = '" +POut.PBool  (like.ClaimsUseUCR)+"' "
				+"&& FeeSched = '"     +POut.PInt   (like.FeeSched)+"' "
				+"&& CopayFeeSched = '"+POut.PInt   (like.CopayFeeSched)+"' "
				+"&& ClaimFormNum = '" +POut.PInt   (like.ClaimFormNum)+"'";
			NonQ();
		}

		/// <summary>Only used from FormInsPlan. Returns true if successful. This is quite complex, because it also must update all claimprocs for all patients affected by the deletion. It does change the Patient.Cur, so need to reset afterwards.</summary>
		public static bool DeleteCur(){
			//first, check claims
			cmd.CommandText="SELECT patnum FROM claim "
				+"WHERE plannum = '"+Cur.PlanNum.ToString()+"' LIMIT 1";
			FillTable();
			if(table.Rows.Count!=0){
				MessageBox.Show(Lan.g("FormInsPlan","Not allowed to delete a plan with existing claims."));
				return false;
			}
			//then, find any primary coverage for this ins.
			cmd.CommandText="SELECT patnum,secplannum,secrelationship FROM patient "
				+"WHERE priplannum = '"+Cur.PlanNum.ToString()+"'";
			FillTable();
			//and move the existing secondary into primary. This also works if secondary is 0.
			int patNum=0;
			for(int i=0;i<table.Rows.Count;i++){
				patNum=PIn.PInt(table.Rows[i][0].ToString());
				//if both primary and secondary are set to this plan:
				if(Cur.PlanNum.ToString()==table.Rows[i][1].ToString()){
					cmd.CommandText="UPDATE patient SET "
						+"priplannum = '0'"
						+",prirelationship = '0'"
						+",secplannum = '0'"
						+",secrelationship = '0' "
						+"WHERE patnum = '"+patNum.ToString()+"'";
				}
				else{//only the primary
					cmd.CommandText="UPDATE patient SET "
						+"priplannum = '"+table.Rows[i][1].ToString()+"' "
						+",prirelationship = '"+table.Rows[i][2].ToString()+"' "
						+",secplannum = '0' "
						+",secrelationship = '0' "
						+"WHERE patnum = '"+patNum.ToString()+"'";
				}
				NonQ();
				Patients.GetFamily(patNum);
				ClaimProc[] claimProcs=ClaimProcs.Refresh(Patients.Cur.PatNum);
				Procedure[] procs=Procedures.Refresh(Patients.Cur.PatNum);
				Procedures.ComputeEstimatesForAll(Patients.Cur.PatNum,Patients.Cur.PriPlanNum,
					Patients.Cur.SecPlanNum,claimProcs,procs);
			}
			//then secondary only
			cmd.CommandText="SELECT patnum FROM patient "
				+"WHERE secplannum = '"+Cur.PlanNum.ToString()+"'";
			FillTable();
			for(int i=0;i<table.Rows.Count;i++){
				patNum=PIn.PInt(table.Rows[i][0].ToString());
				cmd.CommandText = "UPDATE patient SET "
					+"SecPlanNum = '0'"
					+",SecRelationship = '0' "
					+"WHERE PatNum = '"+patNum.ToString()+"'";
				NonQ();
				Patients.GetFamily(patNum);
				ClaimProc[] claimProcs=ClaimProcs.Refresh(Patients.Cur.PatNum);
				Procedure[] procs=Procedures.Refresh(Patients.Cur.PatNum);
				Procedures.ComputeEstimatesForAll(Patients.Cur.PatNum,Patients.Cur.PriPlanNum,
					Patients.Cur.SecPlanNum,claimProcs,procs);
			}			
			cmd.CommandText = "DELETE FROM covpat WHERE plannum = '"+Cur.PlanNum.ToString()+"'";
			NonQ();
			cmd.CommandText = "DELETE FROM insplan "
				+"WHERE planNum = '"+Cur.PlanNum.ToString()+"'";
			NonQ();
			return true;
			//one unfinished detail is that if the secondary gets moved to primary,
			//it still does not move the percentages over.
		}

		///<summary>It's fastest if the HList has been refreshed first with all necessary plans.  But also works just fine if it can't initally locate the plan in hlist.</summary>
		public static void GetCur(int planNum){
			if(HList !=null && HList.Contains(planNum)){
				Cur=(InsPlan)HList[planNum];
			}
			else{
				Refresh(planNum);//planNum will now be 0 if not found
			}
			if(Cur.PlanNum>0 && Cur.RenewMonth==0){
				Cur.RenewMonth=1;
				UpdateCur();
				//don't worry about refreshing plan list for something this minor.
			}
		}

		///<summary>It's fastest if the HList has been refreshed first with all necessary plans.  But also works just fine if it can't initally locate the plan in hlist.</summary>
		public static InsPlan GetPlan(int planNum){
			InsPlan retPlan;
			if(HList !=null && HList.Contains(planNum)){
				retPlan=(InsPlan)HList[planNum];
			}
			else{
				Refresh(planNum);
				retPlan=Cur;
			}
			if(retPlan.PlanNum>0 && retPlan.RenewMonth<1){//0 or -1
				Cur=retPlan;
				Cur.RenewMonth=1;
				UpdateCur();
				retPlan.RenewMonth=1;
				//don't worry about refreshing plan list for something this minor.
			}
			return retPlan;
		}

		///<summary>Gets a description of the specified plan, including carrier name and subscriber. Works even if plan is from another family.</summary>
		public static string GetDescript(int planNum){
			if(planNum==0)
				return "";
			GetCur(planNum);
			if(planNum==0){//if not found, it will convert planNum to 0
				return "";
			}
			string subscriber=Patients.GetNameInFamFL(Cur.Subscriber);
			if(subscriber==""){//subscriber from another family
				Patients.GetLim(Cur.Subscriber);
				subscriber=Patients.LimName;
			}
			string retStr="";
			//loop just to get the index of the plan in the family list
			bool otherFam=true;
			for(int i=0;i<List.Length;i++){
				if(List[i].PlanNum==planNum){
					otherFam=false;
					//retStr += (i+1).ToString()+": ";
				}
			}
			if(otherFam)//retStr=="")
				retStr="(other fam):";
			Carriers.GetCur(Cur.CarrierNum);
			string carrier=Carriers.Cur.CarrierName;
			if(carrier.Length>20){
				carrier=carrier.Substring(0,20)+"...";
			}
			retStr+=carrier;
			retStr+=" ("+subscriber+")";
			return retStr;
		}

		///<summary>Used in Ins lines in Account module.</summary>
		public static string GetCarrierName(int planNum){
			GetCur(planNum);
			Carriers.GetCur(Cur.CarrierNum);
			if(Cur.CarrierNum==0){//if corrupted
				return "";
			}
			return Carriers.Cur.CarrierName;
		}

		/// <summary>Get insurance benefits remaining for one benefit year.  InsPlans must be refreshed first.  Returns actual remaining insurance based on ClaimProc data, taking into account inspayed and ins pending. Must supply all claimprocs for the patient.  Date used to determine which benefit year to calc.  Usually today's date.  The insplan.PlanNum is the plan to get value for.  ExcludeClaim is the ClaimNum to exclude, or enter -1 to include all.</summary>
		public static double GetInsRem(ClaimProc[] ClaimProcList,DateTime date,int planNum,int excludeClaim){
			InsPlan curPlan=GetPlan(planNum);
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
		/// <returns>Returns the amount of insurance pending based on ClaimProc data.</returns>
		public static double GetPending(ClaimProc[] ClaimProcList,DateTime date,int planNum){//
			//These 3 lines were eliminated because we can still return pending whether or not annual max blank
			//if(((InsPlan)HList[planNum]).AnnualMax<=0){
			//	return 0;
			//}
			InsPlan curPlan=GetPlan(planNum);
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
		public static double GetDedRem(ClaimProc[] ClaimProcList,DateTime date,int planNum,int excludeClaim){
			InsPlan curPlan=GetPlan(planNum);
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

		///<summary>Returns -1 for no copay feeschedule, or 0 or more for a patient copay. Can handle a planNum of 0.</summary>
		public static double GetCopay(string adaCode,int planNum){
			if(planNum==0)
				return -1;
			GetCur(planNum);
			if(Cur.CopayFeeSched==0)
				return -1;
			return Fees.GetAmount(adaCode,Cur.CopayFeeSched);
		}

		/*
		///<summary>Not used anymore since insplans and claims can use information from other families.</summary>
		public static bool HasDependencies(int patNum){
			//get insplans for this subscriber.
			cmd.CommandText="SELECT planNum FROM insplan WHERE "
				+"subscriber = '"+patNum.ToString()+"'";
			FillTable();
			if(table.Rows.Count==0){
				return false;
			}
			string planNum;
			for (int i=0;i<table.Rows.Count;i++){
				planNum = PIn.PString(table.Rows[i][0].ToString());
				cmd.CommandText="SELECT patnum FROM patient "
					+"WHERE (priplannum = '"+planNum+"' "
					+"|| secplannum = '"+planNum+"') "
					+"AND patnum != '"+patNum.ToString()+"'";
				FillTable();
				if(table.Rows.Count!=0){
					MessageBox.Show(Lan.g("ContrFamily","Patient has insurance that is in use by other family memebers.  Please see the manual for instructions."));
					return true;
				}
				 cmd.CommandText="SELECT patnum FROM claim "
					+"WHERE plannum = '"+planNum+"' "
					+"AND patnum != '"+patNum.ToString()+"'";
				FillTable();
				if(table.Rows.Count!=0){
					MessageBox.Show(Lan.g("ContrFamily","Patient has insurance that has existing claims for other family members. Please see the manual for instructions."));
					return true;
				}
			}
			return false;
		}*/

		///<summary>This is used in FormQuery.SubmitQuery to allow display of carrier names.</summary>
		public static void GetHListAll(){
			//need to review why this is used and to clear it when done to conserve memory
			cmd.CommandText="SELECT insplan.PlanNum,carrier.CarrierName "
				+"FROM insplan,carrier "
				+"WHERE insplan.CarrierNum=carrier.CarrierNum";
			FillTable();
			HListAll=new Hashtable(table.Rows.Count);
			int plannum;
			string carrierName;
			for(int i=0;i<table.Rows.Count;i++){
				plannum=PIn.PInt(table.Rows[i][0].ToString());
				carrierName=PIn.PString(table.Rows[i][1].ToString());
				HListAll.Add(plannum,carrierName);
			}
		}

		/*
		///<summary>Updates the change in link status immediately. Used from insplan edit window so that the change will show immediately in the template edit window in the link list.</summary>
		public static void UpdateTemplateNum(){
			cmd.CommandText="UPDATE insplan SET TemplateNum = '"+POut.PInt(Cur.TemplateNum)
				+"' WHERE PlanNum = '"+POut.PInt(Cur.PlanNum)+"'";
			NonQ();
		}*/

		///<summary>Returns a list of insplans that have identical info as this one. Used to display in the insplan window.</summary>
		public static string[] SamePlans(){
			cmd.CommandText="SELECT CONCAT(LName,', ',FName) FROM patient,insplan" 
				+" WHERE patient.PatNum=insplan.Subscriber "
				+"&& insplan.EmployerNum = '"  +POut.PInt   (Cur.EmployerNum)+"' "
				+"&& insplan.GroupName = '"    +POut.PString(Cur.GroupName)+"' "
				+"&& insplan.GroupNum = '"     +POut.PString(Cur.GroupNum)+"' "
				+"&& insplan.CarrierNum = '"   +POut.PInt   (Cur.CarrierNum)+"' "
				+"&& insplan.PlanType = '"     +POut.PString(Cur.PlanType)+"' "
				+"&& insplan.UseAltCode = '"   +POut.PBool  (Cur.UseAltCode)+"' "
				+"&& insplan.ClaimsUseUCR = '" +POut.PBool  (Cur.ClaimsUseUCR)+"' "
				+"&& insplan.FeeSched = '"     +POut.PInt   (Cur.FeeSched)+"' "
				+"&& insplan.CopayFeeSched = '"+POut.PInt   (Cur.CopayFeeSched)+"' "
				+"&& insplan.ClaimFormNum = '" +POut.PInt   (Cur.ClaimFormNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			string[] retStr=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				retStr[i]=PIn.PString(table.Rows[i][0].ToString());
			}
			return retStr;
		}

		///<summary>Used when closing the edit plan window to find all patients using this plan and to update all claimProcs for each patient.  This keeps estimates correct.</summary>
		public static void ComputeEstimatesForCur(){
			cmd.CommandText="SELECT PatNum,PriPlanNum,SecPlanNum FROM patient "
				+"WHERE PriPlanNum='"+Cur.PlanNum.ToString()+"' "
				+"OR SecPlanNum='"+Cur.PlanNum.ToString()+"'";
			FillTable();
			int patNum=0;
			for(int i=0;i<table.Rows.Count;i++){
				patNum=PIn.PInt(table.Rows[i][0].ToString());
				//Patients.GetFamily(patNum);
				ClaimProc[] claimProcs=ClaimProcs.Refresh(patNum);
				Procedure[] procs=Procedures.Refresh(patNum);
				Procedures.ComputeEstimatesForAll(patNum,
					PIn.PInt(table.Rows[i][1].ToString()),
					PIn.PInt(table.Rows[i][2].ToString()),claimProcs,procs);
			}

			
		}



	}

	

	

	


}













