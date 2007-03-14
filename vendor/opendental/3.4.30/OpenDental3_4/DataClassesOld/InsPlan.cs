
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the insplan table in the database.</summary>
	public class InsPlan{
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
		///<summary>Foreign key to Definition.DefNum. This fee schedule holds amounts allowed by carriers.</summary>
		public int AllowedFeeSched;
		///<summary>This is NOT a database column.  It is just used to display the number of plans with the same info.</summary>
		public int NumberPlans;

		///<summary>Returns a copy of this InsPlan.</summary>
		public InsPlan Copy(){
			InsPlan p=new InsPlan();
			p.PlanNum=PlanNum;
			p.Subscriber=Subscriber;
			p.DateEffective=DateEffective;
			p.DateTerm=DateTerm;
			p.GroupName=GroupName;
			p.GroupNum=GroupNum;
			p.AnnualMax=AnnualMax;
			p.RenewMonth=RenewMonth;
			p.Deductible=Deductible;
			p.DeductWaivPrev=DeductWaivPrev;
			p.OrthoMax=OrthoMax;
			p.FloToAge=FloToAge;
			p.PlanNote=PlanNote;
			p.MissToothExcl=MissToothExcl;
			p.MajorWait=MajorWait;
			p.FeeSched=FeeSched;
			p.ReleaseInfo=ReleaseInfo;
			p.AssignBen=AssignBen;
			p.PlanType=PlanType;
			p.ClaimFormNum=ClaimFormNum;
			p.UseAltCode=UseAltCode;
			p.ClaimsUseUCR=ClaimsUseUCR;
			p.CopayFeeSched=CopayFeeSched;
			p.SubscriberID=SubscriberID;
			p.EmployerNum=EmployerNum;
			p.CarrierNum=CarrierNum;
			p.AllowedFeeSched=AllowedFeeSched;
			return p;
		}

		///<summary>Also fills PlanNum from db.</summary>
		public void Insert(){
			string command= "INSERT INTO insplan (subscriber, carrier, "
				+"dateeffective,dateterm,phone,groupname,groupnum,address,address2,city,state,zip,"
				+"nosendelect,electid,employer,annualmax,renewmonth,deductible,"
				+"deductwaivprev,orthomax,"
				+"flotoage,plannote,misstoothexcl,majorwait,feesched,"
				+"releaseinfo,assignben,plantype,claimformnum,usealtcode,"
				+"claimsuseucr,iswrittenoff,copayfeesched,subscriberid,"
				+"EmployerNum,CarrierNum,AllowedFeeSched) VALUES("
				+"'"+POut.PInt   (Subscriber)+"', "
				+"'"+POut.PString(Carrier)+"', "
				+"'"+POut.PDate  (DateEffective)+"', "
				+"'"+POut.PDate  (DateTerm)+"', "
				+"'"+POut.PString(Phone)+"', "
				+"'"+POut.PString(GroupName)+"', "
				+"'"+POut.PString(GroupNum)+"', "
				+"'"+POut.PString(Address)+"', "
				+"'"+POut.PString(Address2)+"', "
				+"'"+POut.PString(City)+"', "
				+"'"+POut.PString(State)+"', "
				+"'"+POut.PString(Zip)+"', "
				+"'"+POut.PBool  (NoSendElect)+"', "
				+"'"+POut.PString(ElectID)+"', "
				+"'"+POut.PString(Employer)+"', "
				+"'"+POut.PInt   (AnnualMax)+"', "
				+"'"+POut.PInt   (RenewMonth)+"', "
				+"'"+POut.PInt   (Deductible)+"', "
				+"'"+POut.PInt   ((int)DeductWaivPrev)+"', "
				+"'"+POut.PInt   (OrthoMax)+"', "
				+"'"+POut.PInt   (FloToAge)+"', "
				+"'"+POut.PString(PlanNote)+"', "
				+"'"+POut.PInt   ((int)MissToothExcl)+"', "
				+"'"+POut.PInt   ((int)MajorWait)+"', "
				+"'"+POut.PInt   (FeeSched)+"', "
				+"'"+POut.PBool  (ReleaseInfo)+"', "
				+"'"+POut.PBool  (AssignBen)+"', "
				+"'"+POut.PString(PlanType)+"', "
				+"'"+POut.PInt   (ClaimFormNum)+"', "
				+"'"+POut.PBool  (UseAltCode)+"', "
				+"'"+POut.PBool  (ClaimsUseUCR)+"', "
				+"'"+POut.PBool  (IsWrittenOff)+"', "
				+"'"+POut.PInt   (CopayFeeSched)+"', "
				+"'"+POut.PString(SubscriberID)+"', "
				+"'"+POut.PInt   (EmployerNum)+"', "
				+"'"+POut.PInt   (CarrierNum)+"', "
				+"'"+POut.PInt   (AllowedFeeSched)+"')";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			PlanNum=dcon.InsertID;
		}

		///<summary></summary>
		public void Update(){
			string command= "UPDATE insplan SET " 
				+ "Subscriber = '"   +POut.PInt   (Subscriber)+"'"
				+ ",Carrier = '"      +POut.PString(Carrier)+"'"
				+ ",DateEffective = '"+POut.PDate  (DateEffective)+"'"
				+ ",DateTerm = '"     +POut.PDate  (DateTerm)+"'"
				+ ",Phone = '"        +POut.PString(Phone)+"'"
				+ ",GroupName = '"    +POut.PString(GroupName)+"'"
				+ ",GroupNum = '"     +POut.PString(GroupNum)+"'"
				+ ",Address = '"      +POut.PString(Address)+"'"
				+ ",Address2 = '"     +POut.PString(Address2)+"'"
				+ ",City = '"         +POut.PString(City)+"'"
				+ ",State = '"        +POut.PString(State)+"'"
				+ ",Zip = '"          +POut.PString(Zip)+"'"
				+ ",NoSendElect = '"  +POut.PBool  (NoSendElect)+"'"
				+ ",ElectID = '"      +POut.PString(ElectID)+"'"
				+ ",Employer = '"     +POut.PString(Employer)+"'"
				+ ",AnnualMax = '"    +POut.PInt   (AnnualMax)+"'"
				+ ",RenewMonth = '"   +POut.PInt   (RenewMonth)+"'"
				+ ",Deductible = '"   +POut.PInt   (Deductible)+"'"
				+ ",DeductWaivPrev= '"+POut.PInt   ((int)DeductWaivPrev)+"'"
				+ ",OrthoMax = '"     +POut.PInt   (OrthoMax)+"'"
				+ ",FloToAge = '"     +POut.PInt   (FloToAge)+"'"
				+ ",PlanNote = '"     +POut.PString(PlanNote)+"'"
				+ ",MissToothExcl = '"+POut.PInt   ((int)MissToothExcl)+"'"
				+ ",MajorWait = '"    +POut.PInt   ((int)MajorWait)+"'"
				+ ",feesched = '"     +POut.PInt   (FeeSched)+"'"
				+ ",releaseinfo = '"  +POut.PBool  (ReleaseInfo)+"'"
				+ ",assignben = '"    +POut.PBool  (AssignBen)+"'"
				+ ",plantype = '"     +POut.PString(PlanType)+"'"
				+ ",claimformnum = '" +POut.PInt   (ClaimFormNum)+"'"
				+ ",usealtcode = '"   +POut.PBool  (UseAltCode)+"'"
				+ ",claimsuseucr = '" +POut.PBool  (ClaimsUseUCR)+"'"
				+ ",iswrittenoff = '" +POut.PBool  (IsWrittenOff)+"'"
				+ ",copayfeesched = '"+POut.PInt   (CopayFeeSched)+"'"
				+ ",subscriberid = '" +POut.PString(SubscriberID)+"'"
				+ ",EmployerNum = '"  +POut.PInt   (EmployerNum)+"'"
				+ ",CarrierNum = '"   +POut.PInt   (CarrierNum)+"'"
				+ ",AllowedFeeSched='"+POut.PInt   (AllowedFeeSched)+"'"
				+" WHERE PlanNum = '" +POut.PInt(PlanNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary>Called from FormInsPlanEditAll. This updates the synchronized fields for all plans like the specified insPlan.  Cur must be set to the new values that we want.</summary>
		public void UpdateForLike(InsPlan like){
			string command= "UPDATE insplan SET "
				+"EmployerNum = '"     +POut.PInt   (EmployerNum)+"'"
				+",GroupName = '"      +POut.PString(GroupName)+"'"
				+",GroupNum = '"       +POut.PString(GroupNum)+"'"
				+",CarrierNum = '"     +POut.PInt   (CarrierNum)+"'"
				+",PlanType = '"       +POut.PString(PlanType)+"'"
				+",UseAltCode = '"     +POut.PBool  (UseAltCode)+"'"
				+",ClaimsUseUcr = '"   +POut.PBool  (ClaimsUseUCR)+"'"
				+",FeeSched = '"       +POut.PInt   (FeeSched)+"'"
				+",CopayFeeSched = '"  +POut.PInt   (CopayFeeSched)+"'"
				+",ClaimFormNum = '"   +POut.PInt   (ClaimFormNum)+"'"
				+",AllowedFeeSched= '" +POut.PInt   (AllowedFeeSched)+"'"
				+" WHERE "
				+"EmployerNum = '"        +POut.PInt   (like.EmployerNum)+"' "
				+"AND GroupName = '"      +POut.PString(like.GroupName)+"' "
				+"AND GroupNum = '"       +POut.PString(like.GroupNum)+"' "
				+"AND CarrierNum = '"     +POut.PInt   (like.CarrierNum)+"' "
				+"AND PlanType = '"       +POut.PString(like.PlanType)+"' "
				+"AND UseAltCode = '"     +POut.PBool  (like.UseAltCode)+"' "
				+"AND ClaimsUseUCR = '"   +POut.PBool  (like.ClaimsUseUCR)+"' "
				+"AND FeeSched = '"       +POut.PInt   (like.FeeSched)+"' "
				+"AND CopayFeeSched = '"  +POut.PInt   (like.CopayFeeSched)+"' "
				+"AND ClaimFormNum = '"   +POut.PInt   (like.ClaimFormNum)+"' "
				+"AND AllowedFeeSched = '"+POut.PInt   (like.AllowedFeeSched)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		/// <summary>Only used from FormInsPlan. Returns true if successful. This is quite complex, because it also must update all claimprocs for all patients affected by the deletion.</summary>
		public bool Delete(InsPlan[] PlanList){
			//first, check claims
			string command="SELECT patnum FROM claim "
				+"WHERE plannum = '"+PlanNum.ToString()+"' LIMIT 1";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count!=0){
				MessageBox.Show(Lan.g("FormInsPlan","Not allowed to delete a plan with existing claims."));
				return false;
			}
			//then, check claimprocs
			command="SELECT PatNum FROM claimproc "
				+"WHERE PlanNum = '"+PlanNum.ToString()+"' LIMIT 1";
			dcon=new DataConnection();
 			table=dcon.GetTable(command);
			if(table.Rows.Count!=0){
				MessageBox.Show(Lan.g("FormInsPlan","Not allowed to delete a plan attached to procedures."));
				return false;
			}
			//then, find any primary coverage for this ins.
			command="SELECT patnum,secplannum,secrelationship FROM patient "
				+"WHERE priplannum = '"+PlanNum.ToString()+"'";
			table=dcon.GetTable(command);
			//and move the existing secondary into primary. This also works if secondary is 0.
			int patNum=0;
			Patient pat;
			for(int i=0;i<table.Rows.Count;i++){
				patNum=PIn.PInt(table.Rows[i][0].ToString());
				//if both primary and secondary are set to this plan:
				if(PlanNum.ToString()==table.Rows[i][1].ToString()){
					command="UPDATE patient SET "
						+"priplannum = '0'"
						+",prirelationship = '0'"
						+",secplannum = '0'"
						+",secrelationship = '0' "
						+"WHERE patnum = '"+patNum.ToString()+"'";
				}
				else{//only the primary
					command="UPDATE patient SET "
						+"priplannum = '"+table.Rows[i][1].ToString()+"' "
						+",prirelationship = '"+table.Rows[i][2].ToString()+"' "
						+",secplannum = '0' "
						+",secrelationship = '0' "
						+"WHERE patnum = '"+patNum.ToString()+"'";
				}
				dcon.NonQ(command);
				//Patients.GetFamily(patNum);
				pat=Patients.GetPat(patNum);
				ClaimProc[] claimProcs=ClaimProcs.Refresh(patNum);
				Procedure[] procs=Procedures.Refresh(patNum);
				Procedures.ComputeEstimatesForAll(patNum,pat.PriPlanNum,
					pat.SecPlanNum,claimProcs,procs,pat,PlanList);
			}
			//then secondary only
			command="SELECT patnum FROM patient "
				+"WHERE secplannum = '"+PlanNum.ToString()+"'";
			table=dcon.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++){
				patNum=PIn.PInt(table.Rows[i][0].ToString());
				command="UPDATE patient SET "
					+"SecPlanNum = '0'"
					+",SecRelationship = '0' "
					+"WHERE PatNum = '"+patNum.ToString()+"'";
				dcon.NonQ(command);
				pat=Patients.GetPat(patNum);
				ClaimProc[] claimProcs=ClaimProcs.Refresh(pat.PatNum);
				Procedure[] procs=Procedures.Refresh(pat.PatNum);
				Procedures.ComputeEstimatesForAll(pat.PatNum,pat.PriPlanNum,
					pat.SecPlanNum,claimProcs,procs,pat,PlanList);
			}			
			command="DELETE FROM covpat WHERE plannum = '"+PlanNum.ToString()+"'";
			dcon.NonQ(command);
			command="DELETE FROM insplan "
				+"WHERE planNum = '"+PlanNum.ToString()+"'";
			dcon.NonQ(command);
			return true;
			//one unfinished detail is that if the secondary gets moved to primary,
			//it still does not move the percentages over.
		}

		///<summary>Returns a list of insplans that have identical info as this one. Used to display in the insplan window.</summary>
		public string[] SamePlans(){
			string command="SELECT CONCAT(LName,', ',FName) FROM patient,insplan" 
				+" WHERE patient.PatNum=insplan.Subscriber "
				+"AND insplan.EmployerNum = '"    +POut.PInt   (EmployerNum)+"' "
				+"AND insplan.GroupName = '"      +POut.PString(GroupName)+"' "
				+"AND insplan.GroupNum = '"       +POut.PString(GroupNum)+"' "
				+"AND insplan.CarrierNum = '"     +POut.PInt   (CarrierNum)+"' "
				+"AND insplan.PlanType = '"       +POut.PString(PlanType)+"' "
				+"AND insplan.UseAltCode = '"     +POut.PBool  (UseAltCode)+"' "
				+"AND insplan.ClaimsUseUCR = '"   +POut.PBool  (ClaimsUseUCR)+"' "
				+"AND insplan.FeeSched = '"       +POut.PInt   (FeeSched)+"' "
				+"AND insplan.CopayFeeSched = '"  +POut.PInt   (CopayFeeSched)+"' "
				+"AND insplan.ClaimFormNum = '"   +POut.PInt   (ClaimFormNum)+"' "
				+"AND insplan.AllowedFeeSched = '"+POut.PInt   (AllowedFeeSched)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			string[] retStr=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				retStr[i]=PIn.PString(table.Rows[i][0].ToString());
			}
			return retStr;
		}

		///<summary>Used when closing the edit plan window to find all patients using this plan and to update all claimProcs for each patient.  This keeps estimates correct.</summary>
		public void ComputeEstimatesForCur(Family FamCur){
			string command="SELECT PatNum,PriPlanNum,SecPlanNum FROM patient "
				+"WHERE PriPlanNum='"+PlanNum.ToString()+"' "
				+"OR SecPlanNum='"+PlanNum.ToString()+"'";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			int patNum=0;
			Patient pat;
			for(int i=0;i<table.Rows.Count;i++){
				patNum=PIn.PInt(table.Rows[i][0].ToString());
				//Patients.GetFamily(patNum);
				pat=Patients.GetPat(patNum);
				ClaimProc[] claimProcs=ClaimProcs.Refresh(patNum);
				Procedure[] procs=Procedures.Refresh(patNum);
				InsPlan[] plans=InsPlans.Refresh(FamCur);
				Procedures.ComputeEstimatesForAll(patNum,
					PIn.PInt(table.Rows[i][1].ToString()),
					PIn.PInt(table.Rows[i][2].ToString()),claimProcs,procs,pat,plans);
			}

			
		}



	}

	

	

	


}













