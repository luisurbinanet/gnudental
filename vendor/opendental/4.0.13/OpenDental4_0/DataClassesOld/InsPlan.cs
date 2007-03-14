
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
		///<summary>Date plan became effective</summary>
		public DateTime DateEffective;
		///<summary>Date plan was terminated</summary>
		public DateTime DateTerm;
		///<summary>Optional</summary>
		public string GroupName;
		///<summary></summary>
		public string GroupNum;
		///<summary>Note for all plans identical to this one.  Always stays in synch with other identical plans regardless of user actions.  If they change it on one, it gets changed on all.</summary>
		public string PlanNote;
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
		///<summary></summary>
		public string TrojanID;
		///<summary>Only used in Canada. It's a suffix to the group number.</summary>
		public string DivisionNo;
		///<summary>User doesn't usually put these in.  Only used when automatically requesting benefits, such as with Trojan.  All the benefits get stored here in text form for later reference.  Specific to one plan and not synchronized because might be specific to subscriber.  If blank, we might add a function to try to find any benefitNote for a similar plan.</summary>
		public string BenefitNotes;
		///<summary>True if this is medical insurance rather than dental insurance.</summary>
		public bool IsMedical;
		///<summary>Specific to an individual plan and not synchronized in any way.  Use to store any other info that affects coverage.</summary>
		public string SubscNote;
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
			p.PlanNote=PlanNote;
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
			p.TrojanID=TrojanID;
			p.DivisionNo=DivisionNo;
			p.BenefitNotes=BenefitNotes;
			p.IsMedical=IsMedical;
			p.SubscNote=SubscNote;
			return p;
		}

		///<summary>Also fills PlanNum from db.</summary>
		public void Insert(){
			if(Prefs.RandomKeys){
				PlanNum=MiscData.GetKey("insplan","PlanNum");
			}
			string command= "INSERT INTO insplan (";
			if(Prefs.RandomKeys){
				command+="PlanNum,";
			}
			command+="Subscriber,"
				+"DateEffective,DateTerm,GroupName,GroupNum,PlanNote,"
				+"FeeSched,ReleaseInfo,AssignBen,PlanType,ClaimFormNum,UseAltCode,"
				+"ClaimsUseUCR,CopayFeeSched,SubscriberID,"
				+"EmployerNum,CarrierNum,AllowedFeeSched,TrojanID,DivisionNo,BenefitNotes,IsMedical,SubscNote) VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(PlanNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (Subscriber)+"', "
				+"'"+POut.PDate  (DateEffective)+"', "
				+"'"+POut.PDate  (DateTerm)+"', "
				+"'"+POut.PString(GroupName)+"', "
				+"'"+POut.PString(GroupNum)+"', "
				+"'"+POut.PString(PlanNote)+"', "
				+"'"+POut.PInt   (FeeSched)+"', "
				+"'"+POut.PBool  (ReleaseInfo)+"', "
				+"'"+POut.PBool  (AssignBen)+"', "
				+"'"+POut.PString(PlanType)+"', "
				+"'"+POut.PInt   (ClaimFormNum)+"', "
				+"'"+POut.PBool  (UseAltCode)+"', "
				+"'"+POut.PBool  (ClaimsUseUCR)+"', "
				+"'"+POut.PInt   (CopayFeeSched)+"', "
				+"'"+POut.PString(SubscriberID)+"', "
				+"'"+POut.PInt   (EmployerNum)+"', "
				+"'"+POut.PInt   (CarrierNum)+"', "
				+"'"+POut.PInt   (AllowedFeeSched)+"', "
				+"'"+POut.PString(TrojanID)+"', "
				+"'"+POut.PString(DivisionNo)+"', "
				+"'"+POut.PString(BenefitNotes)+"', "
				+"'"+POut.PBool  (IsMedical)+"', "
				+"'"+POut.PString(SubscNote)+"')";
			DataConnection dcon=new DataConnection();
			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				PlanNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void Update(){
			string command= "UPDATE insplan SET " 
				+ "Subscriber = '"   +POut.PInt   (Subscriber)+"'"
				+ ",DateEffective = '"+POut.PDate  (DateEffective)+"'"
				+ ",DateTerm = '"     +POut.PDate  (DateTerm)+"'"
				+ ",GroupName = '"    +POut.PString(GroupName)+"'"
				+ ",GroupNum = '"     +POut.PString(GroupNum)+"'"
				+ ",PlanNote = '"     +POut.PString(PlanNote)+"'"
				+ ",FeeSched = '"     +POut.PInt   (FeeSched)+"'"
				+ ",ReleaseInfo = '"  +POut.PBool  (ReleaseInfo)+"'"
				+ ",AssignBen = '"    +POut.PBool  (AssignBen)+"'"
				+ ",PlanType = '"     +POut.PString(PlanType)+"'"
				+ ",ClaimFormNum = '" +POut.PInt   (ClaimFormNum)+"'"
				+ ",UseAltcode = '"   +POut.PBool  (UseAltCode)+"'"
				+ ",ClaimsUseUCR = '" +POut.PBool  (ClaimsUseUCR)+"'"
				+ ",CopayFeeSched = '"+POut.PInt   (CopayFeeSched)+"'"
				+ ",SubscriberID = '" +POut.PString(SubscriberID)+"'"
				+ ",EmployerNum = '"  +POut.PInt   (EmployerNum)+"'"
				+ ",CarrierNum = '"   +POut.PInt   (CarrierNum)+"'"
				+ ",AllowedFeeSched='"+POut.PInt   (AllowedFeeSched)+"'"
				+ ",TrojanID='"       +POut.PString(TrojanID)+"'"
				+ ",DivisionNo='"     +POut.PString(DivisionNo)+"'"
				+ ",BenefitNotes='"   +POut.PString(BenefitNotes)+"'"
				+ ",IsMedical='"      +POut.PBool  (IsMedical)+"'"
				+ ",SubscNote='"      +POut.PString(SubscNote)+"'"
				+" WHERE PlanNum = '" +POut.PInt(PlanNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary>Called from FormInsPlan when applying changes to all identical insurance plans. This updates the synchronized fields for all plans like the specified insPlan.  Cur must be set to the new values that we want.  BenefitNotes and SubscNote are specific to subscriber and are not changed.  PlanNotes are handled separately in a different function after this one is complete.</summary>
		public void UpdateForLike(InsPlan like){
			string command= "UPDATE insplan SET "
				+"EmployerNum = '"     +POut.PInt   (EmployerNum)+"'"
				+",GroupName = '"      +POut.PString(GroupName)+"'"
				+",GroupNum = '"       +POut.PString(GroupNum)+"'"
				+",DivisionNo = '"     +POut.PString(DivisionNo)+"'"
				+",CarrierNum = '"     +POut.PInt   (CarrierNum)+"'"
				+",PlanType = '"       +POut.PString(PlanType)+"'"
				+",UseAltCode = '"     +POut.PBool  (UseAltCode)+"'"
				+",IsMedical = '"      +POut.PBool  (IsMedical)+"'"
				+",ClaimsUseUCR = '"   +POut.PBool  (ClaimsUseUCR)+"'"
				+",FeeSched = '"       +POut.PInt   (FeeSched)+"'"
				+",CopayFeeSched = '"  +POut.PInt   (CopayFeeSched)+"'"
				+",ClaimFormNum = '"   +POut.PInt   (ClaimFormNum)+"'"
				+",AllowedFeeSched= '" +POut.PInt   (AllowedFeeSched)+"'"
				+",TrojanID = '"       +POut.PString(TrojanID)+"'"
				+" WHERE "
				+"EmployerNum = '"        +POut.PInt   (like.EmployerNum)+"' "
				+"AND GroupName = '"      +POut.PString(like.GroupName)+"' "
				+"AND GroupNum = '"       +POut.PString(like.GroupNum)+"' "
				+"AND DivisionNo = '"     +POut.PString(like.DivisionNo)+"'"
				+"AND CarrierNum = '"     +POut.PInt   (like.CarrierNum)+"' "
				+"AND PlanType = '"       +POut.PString(like.PlanType)+"' "
				+"AND UseAltCode = '"     +POut.PBool  (like.UseAltCode)+"' "
				+"AND IsMedical = '"      +POut.PBool  (like.IsMedical)+"' "
				+"AND ClaimsUseUCR = '"   +POut.PBool  (like.ClaimsUseUCR)+"' "
				+"AND FeeSched = '"       +POut.PInt   (like.FeeSched)+"' "
				+"AND CopayFeeSched = '"  +POut.PInt   (like.CopayFeeSched)+"' "
				+"AND ClaimFormNum = '"   +POut.PInt   (like.ClaimFormNum)+"' "
				+"AND AllowedFeeSched = '"+POut.PInt   (like.AllowedFeeSched)+"' "
				+"AND TrojanID = '"       +POut.PString(like.TrojanID)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		/// <summary>Only used from FormInsPlan. Throws ApplicationException if any dependencies. This is quite complex, because it also must update all claimprocs for all patients affected by the deletion.  Also deletes patplans, benefits, and claimprocs.</summary>
		public void Delete(){
			//first, check claims
			string command="SELECT PatNum FROM claim "
				+"WHERE plannum = '"+PlanNum.ToString()+"' LIMIT 1";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count!=0){
				throw new ApplicationException(Lan.g("FormInsPlan","Not allowed to delete a plan with existing claims."));
			}
			//then, check claimprocs
			command="SELECT PatNum FROM claimproc "
				+"WHERE PlanNum = "+POut.PInt(PlanNum)
				+" AND Status != 6 "//ignore estimates
				+"LIMIT 1";
			dcon=new DataConnection();
 			table=dcon.GetTable(command);
			if(table.Rows.Count!=0){
				throw new ApplicationException(Lan.g("FormInsPlan","Not allowed to delete a plan attached to procedures."));
			}
			//get a list of all patplans with this planNum
			command="SELECT PatPlanNum FROM patplan "
				+"WHERE PlanNum = "+PlanNum.ToString();
			table=dcon.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++){
				//benefits with this PatPlanNum are also deleted here
				PatPlans.Delete(PIn.PInt(table.Rows[i][0].ToString()));
			}
			command="DELETE FROM benefit WHERE PlanNum="+POut.PInt(PlanNum);
			dcon.NonQ(command);
			command="DELETE FROM claimproc WHERE PlanNum="+POut.PInt(PlanNum);//just estimates
			dcon.NonQ(command);
			command="DELETE FROM insplan "
				+"WHERE PlanNum = '"+PlanNum.ToString()+"'";
			dcon.NonQ(command);
		}

		///<summary>Gets a list of subscriber names from the database that have identical plan info as this one. Used to display in the insplan window.  Note that the current plan should have been saved to the database in order for it to show in the list.</summary>
		public string[] GetSubscribersForSamePlans(){
			string command="SELECT CONCAT(LName,', ',FName) FROM patient,insplan" 
				+" WHERE patient.PatNum=insplan.Subscriber "
				+"AND insplan.EmployerNum = '"    +POut.PInt   (EmployerNum)+"' "
				+"AND insplan.GroupName = '"      +POut.PString(GroupName)+"' "
				+"AND insplan.GroupNum = '"       +POut.PString(GroupNum)+"' "
				+"AND insplan.DivisionNo = '"     +POut.PString(DivisionNo)+"' "
				+"AND insplan.CarrierNum = '"     +POut.PInt   (CarrierNum)+"' "
				+"AND insplan.PlanType = '"       +POut.PString(PlanType)+"' "
				+"AND insplan.UseAltCode = '"     +POut.PBool  (UseAltCode)+"' "
				+"AND insplan.IsMedical = '"      +POut.PBool  (IsMedical)+"' "
				+"AND insplan.ClaimsUseUCR = '"   +POut.PBool  (ClaimsUseUCR)+"' "
				+"AND insplan.FeeSched = '"       +POut.PInt   (FeeSched)+"' "
				+"AND insplan.CopayFeeSched = '"  +POut.PInt   (CopayFeeSched)+"' "
				+"AND insplan.ClaimFormNum = '"   +POut.PInt   (ClaimFormNum)+"' "
				+"AND insplan.AllowedFeeSched = '"+POut.PInt   (AllowedFeeSched)+"' "
				+"AND insplan.TrojanID = '"       +POut.PString(TrojanID)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			string[] retStr=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				retStr[i]=PIn.PString(table.Rows[i][0].ToString());
			}
			return retStr;
		}

		///<summary>Gets a list of PlanNums from the database of plans that have identical info as this one. Used to perform updates to benefits, etc.  Note that the current plan should have been saved to the database in order for it to show in the list.</summary>
		public int[] GetPlanNumsOfSamePlans() {
			string command="SELECT PlanNum FROM insplan WHERE" 
				+" insplan.EmployerNum = '"       +POut.PInt   (EmployerNum)+"' "
				+"AND insplan.GroupName = '"      +POut.PString(GroupName)+"' "
				+"AND insplan.GroupNum = '"       +POut.PString(GroupNum)+"' "
				+"AND insplan.DivisionNo = '"     +POut.PString(DivisionNo)+"' "
				+"AND insplan.CarrierNum = '"     +POut.PInt   (CarrierNum)+"' "
				+"AND insplan.PlanType = '"       +POut.PString(PlanType)+"' "
				+"AND insplan.UseAltCode = '"     +POut.PBool  (UseAltCode)+"' "
				+"AND insplan.IsMedical = '"      +POut.PBool  (IsMedical)+"' "
				+"AND insplan.ClaimsUseUCR = '"   +POut.PBool  (ClaimsUseUCR)+"' "
				+"AND insplan.FeeSched = '"       +POut.PInt   (FeeSched)+"' "
				+"AND insplan.CopayFeeSched = '"  +POut.PInt   (CopayFeeSched)+"' "
				+"AND insplan.ClaimFormNum = '"   +POut.PInt   (ClaimFormNum)+"' "
				+"AND insplan.AllowedFeeSched = '"+POut.PInt   (AllowedFeeSched)+"' "
				+"AND insplan.TrojanID = '"       +POut.PString(TrojanID)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			int[] retVal=new int[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				retVal[i]=PIn.PInt(table.Rows[i][0].ToString());
			}
			return retVal;
		}

		
		



	}

	

	

	


}













