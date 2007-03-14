using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental {

	/// <summary>Corresponds to the benefit table in the database which replaces the old covpat table.  A benefit is usually a percentage, deductible, limitation, max, or similar. Each row represents a single benefit.  A benefit can have a value in EITHER PlanNum OR PatPlanNum.  If it is for a PlanNum, the most common, then the benefit is attached to an insurance plan.  If it is for a PatPlanNum, then it overrides the plan benefit, usually a percentage, for a single patient.  Benefits we can't handle yet include posterior composites, COB duplication, amounts used, in/out of plan network, authorization required, missing tooth exclusion, and any date related limitations like waiting periods.  We also cannot yet handle family level benefits.  All benefits are at the individual patient level.</summary>
	/// <remarks>Here are examples of typical usage which parallel X12 usage.
	/// Example fields shown in this order:
	/// CovCat,ADACode(- indicates blank),BenefitType,Percent,MonetaryAmt,TimePeriod,QuantityQualifier,Quantity,
	/// Annual Max $1000: General,-,Limitations,0,1000,CalendarYear,None,0
	/// Restorative 80%: Restorative,-,Percentage,80,0,CalendarYear,None,0
	/// $50 deductible: General,-,Deductible,0,50,CalendarYear,None,0
	/// Deductible waived on preventive: Preventive,-,Deductible,0,0,CalendarYear,None,0
	/// 1 pano every 5 years: General(ignored),D0330,Limitations,0,0,Years,Years,5
	/// 2 exams per year: Preventive(or Diagnostic),-,Limitations,0,0,BenefitYear,NumberOfServices,2
	/// Fluoride limit 18yo: General(ignored),D1204,Limitations,0,0,CalendarYear(or None),AgeLimit,18 (might require a second identical entry for D1205)
	/// 4BW every 6 months: General(ignored),D0274,Limitations,0,0,None,Months,6.
	///</remarks>
	public class Benefit:IComparable {
		///<summary>Primary key.</summary>
		public int BenefitNum;
		///<summary>Foreign key to insplan.PlanNum.  Most benefits should be attached using PlanNum.  The exception would be if each patient has a different percentage.  If this is used, then PatPlanNum should be 0.</summary>
		public int PlanNum;
		///<summary>Foreign key to patplan.PatPlanNum.  It is rare to attach benefits this way.  Usually only used to override percentages for patients.   In this case, PlanNum should be 0.</summary>
		public int PatPlanNum;
		///<summary>Foreign key to covcat.CovCatNum.  Corresponds to X12 EB03- Service Type code.  Can never be blank.  There will be very specific categories covered by X12. Users should set their InsCovCats to the defaults we will provide.</summary>
		public int CovCatNum;
		///<summary>Foreign key to procedurecode.ADACode.  Typical uses include fluoride, sealants, etc.  If a specific code is used here, then the CovCat is completely ignored.</summary>
		public string ADACode;
		///<summary>Corresponds to X12 EB01. Examples: 1=Percentage,2=Deductible,3=CoPayment,4=Exclusions,5=Limitations. There's not really any difference between limitations and exclusions as far as the logic is concerned.</summary>
		public InsBenefitType BenefitType;
		///<summary>Only used if BenefitType=Percentage.  Valid values are 0 to 100.</summary>
		public int Percent;
		///<summary>Used CoPayment, Limitations, and as deductible in PercentDeduct.</summary>
		public double MonetaryAmt;
		///<summary>Corresponds to X12 EB06, Time Period Qualifier.  Examples: 0=None,1=ServiceYear,2=CalendarYear,3=Lifetime,4=Years. Might add Visit and Remaining.</summary>
		public BenefitTimePeriod TimePeriod;
		///<summary>Corresponds to X12 EB09. Not used very much. Examples: 0=None,1=NumberOfServices,2=AgeLimit,3=Visits,4=Years,5=Months</summary>
		public BenefitQuantity QuantityQualifier;
		///<summary>Corresponds to X12 EB10. Qualify the quantity</summary>
		public int Quantity;

		///<summary>IComparable.CompareTo implementation.  This is used to order benefit lists as well as to group benefits if the type is essentially equal.  It doesn't compare values such as percentages or amounts.  It only compares types.</summary>
		public int CompareTo(object obj) {
			if(!(obj is Benefit)) {
				throw new ArgumentException("object is not a Benefit");
			}
			Benefit ben=(Benefit)obj;
			//first by type
			if(BenefitType!=ben.BenefitType) {//if types are different
				return BenefitType.CompareTo(ben.BenefitType);
			}
			//types are the same, so check covCat. This is a loose comparison, ignored if either is 0.
			if(CovCatNum!=0 && ben.CovCatNum!=0//if both covcats have values
				&& CovCatNum!=ben.CovCatNum) {
				return CovCats.GetOrderShort(CovCatNum).CompareTo(CovCats.GetOrderShort(ben.CovCatNum));
			}
			//ADACode
			if(ADACode!=ben.ADACode) {
				return ADACode.CompareTo(ben.ADACode);
			}
			//TimePeriod-ServiceYear and CalendarYear are treated as the same.
			//if either are not serviceYear or CalendarYear
			if((TimePeriod!=BenefitTimePeriod.CalendarYear && TimePeriod!=BenefitTimePeriod.ServiceYear)
				|| (ben.TimePeriod!=BenefitTimePeriod.CalendarYear && ben.TimePeriod!=BenefitTimePeriod.ServiceYear)) {
				return TimePeriod.CompareTo(ben.TimePeriod);
			}
			//QuantityQualifier
			if(QuantityQualifier!=ben.QuantityQualifier) {//if different
				return QuantityQualifier.CompareTo(ben.QuantityQualifier);
			}
			//always different if plan vs. pat override
			if(PatPlanNum==0 && ben.PatPlanNum!=0) {
				return -1;
			}
			if(PlanNum==0 && ben.PlanNum!=0) {
				return 1;
			}
			//Last resort.  Can't find any significant differencesin the type, so:
			return 0;//then values are the same.
		}

		///<summary></summary>
		public Benefit Copy() {
			Benefit b=new Benefit();
			b.BenefitNum=BenefitNum;
			b.PlanNum=PlanNum;
			b.PatPlanNum=PatPlanNum;
			b.CovCatNum=CovCatNum;
			b.ADACode=ADACode;
			b.BenefitType=BenefitType;
			b.Percent=Percent;
			b.MonetaryAmt=MonetaryAmt;
			b.TimePeriod=TimePeriod;
			b.QuantityQualifier=QuantityQualifier;
			b.Quantity=Quantity;
			return b;
		}

		///<summary></summary>
		public void Update() {
			string command="UPDATE benefit SET " 
				+"PlanNum = '"          +POut.PInt   (PlanNum)+"'"
				+",PatPlanNum = '"      +POut.PInt   (PatPlanNum)+"'"
				+",CovCatNum = '"       +POut.PInt   (CovCatNum)+"'"
				+",ADACode = '"         +POut.PString(ADACode)+"'"
				+",BenefitType = '"     +POut.PInt   ((int)BenefitType)+"'"
				+",Percent = '"         +POut.PInt   (Percent)+"'"
				+",MonetaryAmt = '"     +POut.PDouble(MonetaryAmt)+"'"
				+",TimePeriod = '"      +POut.PInt   ((int)TimePeriod)+"'"
				+",QuantityQualifier ='"+POut.PInt   ((int)QuantityQualifier)+"'"
				+",Quantity = '"        +POut.PInt   (Quantity)+"'"
				+" WHERE BenefitNum  ='"+POut.PInt   (BenefitNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Insert() {
			if(Prefs.RandomKeys) {
				BenefitNum=MiscData.GetKey("benefit","BenefitNum");
			}
			string command="INSERT INTO benefit (";
			if(Prefs.RandomKeys) {
				command+="BenefitNum,";
			}
			command+="PlanNum,PatPlanNum,CovCatNum,ADACode,BenefitType,Percent,MonetaryAmt,TimePeriod,"
				+"QuantityQualifier,Quantity) VALUES(";
			if(Prefs.RandomKeys) {
				command+="'"+POut.PInt(BenefitNum)+"', ";
			}
			command+=
				 "'"+POut.PInt(PlanNum)+"', "
				+"'"+POut.PInt(PatPlanNum)+"', "
				+"'"+POut.PInt(CovCatNum)+"', "
				+"'"+POut.PString(ADACode)+"', "
				+"'"+POut.PInt((int)BenefitType)+"', "
				+"'"+POut.PInt(Percent)+"', "
				+"'"+POut.PDouble(MonetaryAmt)+"', "
				+"'"+POut.PInt((int)TimePeriod)+"', "
				+"'"+POut.PInt((int)QuantityQualifier)+"', "
				+"'"+POut.PInt(Quantity)+"')";
			DataConnection dcon=new DataConnection();
			if(Prefs.RandomKeys) {
				dcon.NonQ(command);
			}
			else {
				dcon.NonQ(command,true);
				BenefitNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void Delete() {
			string command="DELETE FROM benefit WHERE BenefitNum ="+POut.PInt(BenefitNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

	}

	/*================================================================================================================
	==================================================== class Benefits =============================================*/

	///<summary></summary>
	public class Benefits {
		///<summary>Gets a list of all benefits for a given list of patplans for one patient.</summary>
		public static Benefit[] Refresh(PatPlan[] listForPat) {
			if(listForPat.Length==0) {
				return new Benefit[0];
			}
			string s="";
			for(int i=0;i<listForPat.Length;i++) {
				if(i>0) {
					s+=" OR";
				}
				s+=" PlanNum="+POut.PInt(listForPat[i].PlanNum);
				s+=" OR";
				s+=" PatPlanNum="+POut.PInt(listForPat[i].PatPlanNum);
			}
			string command="SELECT * FROM benefit"
				+" WHERE"+s;
			//Debug.WriteLine(command);
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			Benefit[] List=new Benefit[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new Benefit();
				List[i].BenefitNum       = PIn.PInt(table.Rows[i][0].ToString());
				List[i].PlanNum          = PIn.PInt(table.Rows[i][1].ToString());
				List[i].PatPlanNum       = PIn.PInt(table.Rows[i][2].ToString());
				List[i].CovCatNum        = PIn.PInt(table.Rows[i][3].ToString());
				List[i].ADACode          = PIn.PString(table.Rows[i][4].ToString());
				List[i].BenefitType      = (InsBenefitType)PIn.PInt(table.Rows[i][5].ToString());
				List[i].Percent          = PIn.PInt(table.Rows[i][6].ToString());
				List[i].MonetaryAmt      = PIn.PDouble(table.Rows[i][7].ToString());
				List[i].TimePeriod       = (BenefitTimePeriod)PIn.PInt(table.Rows[i][8].ToString());
				List[i].QuantityQualifier= (BenefitQuantity)PIn.PInt(table.Rows[i][9].ToString());
				List[i].Quantity         = PIn.PInt(table.Rows[i][10].ToString());
			}
			Array.Sort(List);
			return List;
		}
		
		///<summary>Used in the Plan edit window to get a list of benefits for specified plan and patPlan.  patPlanNum can be 0.</summary>
		public static ArrayList RefreshForPlan(int planNum,int patPlanNum) {
			string command="SELECT *"//,IFNULL(covcat.CovCatNum,0) AS covorder "
				+" FROM benefit"
				//+" LEFT JOIN covcat ON covcat.CovCatNum=benefit.CovCatNum"
				+" WHERE PlanNum = "+POut.PInt(planNum);
			if(patPlanNum!=0){
				command+=" OR PatPlanNum = "+POut.PInt(patPlanNum);
			}
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			ArrayList retVal=new ArrayList();
			Benefit ben;
			for(int i=0;i<table.Rows.Count;i++) {
				ben=new Benefit();
				ben.BenefitNum       = PIn.PInt   (table.Rows[i][0].ToString());
				ben.PlanNum          = PIn.PInt   (table.Rows[i][1].ToString());
				ben.PatPlanNum       = PIn.PInt   (table.Rows[i][2].ToString());
				ben.CovCatNum        = PIn.PInt   (table.Rows[i][3].ToString());
				ben.ADACode          = PIn.PString(table.Rows[i][4].ToString());
				ben.BenefitType      = (InsBenefitType)PIn.PInt(table.Rows[i][5].ToString());
				ben.Percent          = PIn.PInt   (table.Rows[i][6].ToString());
				ben.MonetaryAmt      = PIn.PDouble(table.Rows[i][7].ToString());
				ben.TimePeriod       = (BenefitTimePeriod)PIn.PInt(table.Rows[i][8].ToString());
				ben.QuantityQualifier= (BenefitQuantity)PIn.PInt(table.Rows[i][9].ToString());
				ben.Quantity         = PIn.PInt   (table.Rows[i][10].ToString());
				retVal.Add(ben);
			}
			return retVal;
		}

		
		///<summary>Used in the Plan edit window to get a typical list of benefits for all identical plans.  The suppllied plan will have no planNum.  patPlanNum can be 0.</summary>
		public static ArrayList RefreshForAll(InsPlan like){
			//Get planNums for all identical plans
			string command="SELECT PlanNum FROM insplan "
				+"WHERE EmployerNum = '"  +POut.PInt   (like.EmployerNum)+"' "
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
			DataTable table=dcon.GetTable(command);
			string planNums="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					planNums+=" OR";
				}
				planNums+=" PlanNum="+table.Rows[i][0].ToString();
			}
			//Get all benefits for all those plans
			command="SELECT * FROM benefit WHERE"+planNums;
			table=dcon.GetTable(command);
			Benefit[] List=new Benefit[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Benefit();
				List[i].BenefitNum       = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PlanNum          = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].PatPlanNum       = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].CovCatNum        = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].ADACode          = PIn.PString(table.Rows[i][4].ToString());
				List[i].BenefitType      = (InsBenefitType)PIn.PInt(table.Rows[i][5].ToString());
				List[i].Percent          = PIn.PInt   (table.Rows[i][6].ToString());
				List[i].MonetaryAmt      = PIn.PDouble(table.Rows[i][7].ToString());
				List[i].TimePeriod       = (BenefitTimePeriod)PIn.PInt(table.Rows[i][8].ToString());
				List[i].QuantityQualifier= (BenefitQuantity)PIn.PInt(table.Rows[i][9].ToString());
				List[i].Quantity         = PIn.PInt   (table.Rows[i][10].ToString());
			}
			ArrayList retVal=new ArrayList();
			//Loop through all benefits
			bool matchFound;
			for(int i=0;i<List.Length;i++){
				//For each benefit, loop through retVal and compare.
				matchFound=false;
				for(int j=0;j<retVal.Count;j++){
					if(List[i].CompareTo(retVal[j])==0){//if the type is equal
						matchFound=true;
						break;
					}
				}
				if(matchFound){
					continue;
				}
				//If no match found, then add it to the return list
				retVal.Add(List[i].Copy());
			}
			for(int i=0;i<retVal.Count;i++) {
				((Benefit)retVal[i]).PlanNum=like.PlanNum;//change all the planNums to match the current plan
				((Benefit)retVal[i]).BenefitNum=0;//and change all benefitNums to 0 to trigger having them saved as new.
			}
			return retVal;
		}
		
		///<summary>Gets an annual max from the supplied list of benefits.  Ignores benefits that do not match either the planNum or the patPlanNum.  Because it starts at the top of the benefit list, it will get the most general limitation first.  Returns -1 if none found.</summary>
		public static double GetAnnualMax(Benefit[] list,int planNum,int patPlanNum) {
			for(int i=0;i<list.Length;i++) {
				if(list[i].PlanNum==0 && list[i].PatPlanNum!=patPlanNum) {
					continue;
				}
				if(list[i].PatPlanNum==0 && list[i].PlanNum!=planNum) {
					continue;
				}
				if(list[i].BenefitType!=InsBenefitType.Limitations) {
					continue;
				}
				if(list[i].QuantityQualifier!=BenefitQuantity.None) {
					continue;
				}
				if(list[i].TimePeriod!=BenefitTimePeriod.CalendarYear && list[i].TimePeriod!=BenefitTimePeriod.ServiceYear) {
					continue;
				}
				return list[i].MonetaryAmt;
			}
			return -1;
		}

		///<summary>Gets a deductible from the supplied list of benefits.  Ignores benefits that do not match either the planNum or the patPlanNum.  Because it starts at the top of the benefit list, it will get the most general deductible first.</summary>
		public static double GetDeductible(Benefit[] list,int planNum,int patPlanNum) {
			for(int i=0;i<list.Length;i++) {
				if(list[i].PlanNum==0 && list[i].PatPlanNum!=patPlanNum) {
					continue;
				}
				if(list[i].PatPlanNum==0 && list[i].PlanNum!=planNum) {
					continue;
				}
				if(list[i].BenefitType!=InsBenefitType.Deductible) {
					continue;
				}
				if(list[i].QuantityQualifier!=BenefitQuantity.None) {
					continue;
				}
				if(list[i].TimePeriod!=BenefitTimePeriod.CalendarYear && list[i].TimePeriod!=BenefitTimePeriod.ServiceYear) {
					continue;
				}
				return list[i].MonetaryAmt;
			}
			return -1;
		}

		///<summary>Gets a deductible from the supplied list of benefits.  Ignores benefits that do not match either the planNum or the patPlanNum.  Because it starts at the bottom of the benefit list, it will get the most specific matching deductible first.</summary>
		public static double GetDeductibleByCode(Benefit[] benList,int planNum,int patPlanNum,string adaCode) {
			CovSpan[] spansForCat;
			for(int i=benList.Length-1;i>=0;i--) {
				if(benList[i].PlanNum==0 && benList[i].PatPlanNum!=patPlanNum) {
					continue;
				}
				if(benList[i].PatPlanNum==0 && benList[i].PlanNum!=planNum) {
					continue;
				}
				if(benList[i].BenefitType!=InsBenefitType.Deductible) {
					continue;
				}
				if(benList[i].QuantityQualifier!=BenefitQuantity.None) {
					continue;
				}
				if(benList[i].TimePeriod!=BenefitTimePeriod.CalendarYear && benList[i].TimePeriod!=BenefitTimePeriod.ServiceYear) {
					continue;
				}
				spansForCat=CovSpans.GetForCat(benList[i].CovCatNum);
				for(int j=0;j<spansForCat.Length;j++){
					if(String.Compare(adaCode,spansForCat[j].FromCode)>=0
						&& String.Compare(adaCode,spansForCat[j].ToCode)<=0)
					{
						return benList[i].MonetaryAmt;
					}
				}
			}
			return 0;
		}

		///<summary>Gets the renewal date for annual benefits from the supplied list of benefits.  Looks for a general limitation dollar amount.  Ignores benefits that do not match either the planNum or the patPlanNum.  Because it starts at the top of the benefit list, it will get the most general limitation first.  Because there is one renew date each year, the date returned will be today's date or earlier; the most recent renewal date.</summary>
		public static DateTime GetRenewDate(Benefit[] list,int planNum,int patPlanNum,DateTime insStartDate) {
			for(int i=0;i<list.Length;i++) {
				if(list[i].PlanNum==0 && list[i].PatPlanNum!=patPlanNum) {
					continue;
				}
				if(list[i].PatPlanNum==0 && list[i].PlanNum!=planNum) {
					continue;
				}
				if(list[i].BenefitType!=InsBenefitType.Limitations) {
					continue;
				}
				if(list[i].QuantityQualifier!=BenefitQuantity.None) {
					continue;
				}
				if(list[i].TimePeriod!=BenefitTimePeriod.CalendarYear && list[i].TimePeriod!=BenefitTimePeriod.ServiceYear) {
					continue;
				}
				if(list[i].TimePeriod==BenefitTimePeriod.CalendarYear){
					return new DateTime(DateTime.Now.Year,1,1);
				}
				//now, for benefit year not beginning on Jan 1.
				if(insStartDate.Year<1880){//if no start date was entered.
					return new DateTime(DateTime.Today.Year,1,1);
				}
				if(insStartDate.Month<=DateTime.Today.Month && insStartDate.Day<=DateTime.Today.Day){//earlier this year, or today
					return new DateTime(DateTime.Today.Year,insStartDate.Month,insStartDate.Day);
				}
				//late last year
				return new DateTime(DateTime.Today.Year-1,insStartDate.Month,insStartDate.Day);
			}
			return new DateTime(DateTime.Now.Year,1,1);
		}

		///<summary>Only use pri or sec, not tot.  Used from ClaimProc.ComputeBaseEst. This is a low level function to get the percent to store in a claimproc.  It does not consider any percentOverride.  Always returns a number between 0 and 100.  The supplied benefit list should be sorted frirst.</summary>
		public static int GetPercent(string myADACode,InsPlan insPlan,PatPlan patPlan,Benefit[] benList){
			if(insPlan.PlanType=="f" || insPlan.PlanType=="c"){
				return 100;//flat and cap are always covered 100%
			}
			CovSpan[] spansForCat;
			//loop through benefits starting at bottom (most specific)
			for(int i=benList.Length-1;i>=0;i--){
				//if plan benefit, but no match
				if(benList[i].PlanNum!=0 && insPlan.PlanNum!=benList[i].PlanNum){
					continue;
				}
				//if patplan benefit, but no match
				if(benList[i].PatPlanNum!=0 && patPlan.PatPlanNum!=benList[i].PatPlanNum){
					continue;
				}
				if(benList[i].BenefitType!=InsBenefitType.Percentage){
					continue;
				}
				spansForCat=CovSpans.GetForCat(benList[i].CovCatNum);
				for(int j=0;j<spansForCat.Length;j++){
					if(String.Compare(myADACode,spansForCat[j].FromCode)>=0 && String.Compare(myADACode,spansForCat[j].ToCode)<=0){
						return benList[i].Percent;
					}
				}
			}
			return 0;
		}

		///<summary>Used in FormInsPlan to sych database with changes user made to the benefit list for a plan.  Must supply an old list for comparison.  Only the differences are saved.</summary>
		public static void UpdateList(ArrayList oldBenefitList,ArrayList newBenefitList){
			Benefit newBenefit;
			for(int i=0;i<oldBenefitList.Count;i++){//loop through the old list
				newBenefit=null;
				for(int j=0;j<newBenefitList.Count;j++){
					if(newBenefitList[j]==null || ((Benefit)newBenefitList[j]).BenefitNum==0){
						continue;
					}
					if(((Benefit)oldBenefitList[i]).BenefitNum==((Benefit)newBenefitList[j]).BenefitNum){
						newBenefit=(Benefit)newBenefitList[j];
						break;
					}
				}
				if(newBenefit==null){
					//benefit with matching benefitNum was not found, so it must have been deleted
					((Benefit)oldBenefitList[i]).Delete();
					continue;
				}
				//benefit was found with matching benefitNum, so check for changes
				if(  newBenefit.PlanNum != ((Benefit)oldBenefitList[i]).PlanNum
					|| newBenefit.PatPlanNum != ((Benefit)oldBenefitList[i]).PatPlanNum
					|| newBenefit.CovCatNum != ((Benefit)oldBenefitList[i]).CovCatNum
					|| newBenefit.ADACode != ((Benefit)oldBenefitList[i]).ADACode
					|| newBenefit.BenefitType != ((Benefit)oldBenefitList[i]).BenefitType
					|| newBenefit.Percent != ((Benefit)oldBenefitList[i]).Percent
					|| newBenefit.MonetaryAmt != ((Benefit)oldBenefitList[i]).MonetaryAmt
					|| newBenefit.TimePeriod != ((Benefit)oldBenefitList[i]).TimePeriod
					|| newBenefit.QuantityQualifier != ((Benefit)oldBenefitList[i]).QuantityQualifier
					|| newBenefit.Quantity != ((Benefit)oldBenefitList[i]).Quantity)
				{
					newBenefit.Update();
				}
			}
			for(int i=0;i<newBenefitList.Count;i++){//loop through the new list
				if(newBenefitList[i]==null){
					continue;	
				}
				if(((Benefit)newBenefitList[i]).BenefitNum!=0){
					continue;
				}
				//benefit with benefitNum=0, so it's new
				((Benefit)newBenefitList[i]).Insert();
			}
		}

		///<summary>Used in FormInsPlan when applying changes to all identical plans.  It first compares the old benefit list with the new one.  If there are no changes, it does nothing.  But if there are any changes, then we no longer care what the old benefit list was.  We will just delete it for all similar plans and recreate it.</summary>
		public static void UpdateListForIdentical(ArrayList oldBenefitList,ArrayList newBenefitList,InsPlan plan) {
			Benefit newBenefit;
			bool changed=false;
			for(int i=0;i<newBenefitList.Count;i++) {//loop through the new list
				//look for new benefits
				if(((Benefit)newBenefitList[i]).BenefitNum==0) {
					changed=true;
					break;
				}
			}
			if(!changed){
				for(int i=0;i<oldBenefitList.Count;i++) {//loop through the old list
					newBenefit=null;
					for(int j=0;j<newBenefitList.Count;j++) {
						if(newBenefitList[j]==null || ((Benefit)newBenefitList[j]).BenefitNum==0) {
							continue;
						}
						if(((Benefit)oldBenefitList[i]).BenefitNum==((Benefit)newBenefitList[j]).BenefitNum) {
							newBenefit=(Benefit)newBenefitList[j];
							break;
						}
					}
					if(newBenefit==null) {
						//benefit with matching benefitNum was not found, so it must have been deleted
						changed=true;
						break;
					}
					//benefit was found with matching benefitNum, so check for changes
					if(newBenefit.PlanNum             != ((Benefit)oldBenefitList[i]).PlanNum
						|| newBenefit.PatPlanNum        != ((Benefit)oldBenefitList[i]).PatPlanNum
						|| newBenefit.CovCatNum         != ((Benefit)oldBenefitList[i]).CovCatNum
						|| newBenefit.ADACode           != ((Benefit)oldBenefitList[i]).ADACode
						|| newBenefit.BenefitType       != ((Benefit)oldBenefitList[i]).BenefitType
						|| newBenefit.Percent           != ((Benefit)oldBenefitList[i]).Percent
						|| newBenefit.MonetaryAmt       != ((Benefit)oldBenefitList[i]).MonetaryAmt
						|| newBenefit.TimePeriod        != ((Benefit)oldBenefitList[i]).TimePeriod
						|| newBenefit.QuantityQualifier != ((Benefit)oldBenefitList[i]).QuantityQualifier
						|| newBenefit.Quantity          != ((Benefit)oldBenefitList[i]).Quantity) 
					{
						changed=true;
						break;
					}
				}
			}
			if(!changed){
				return;
			}
			int[] planNums=plan.GetPlanNumsOfSamePlans();
			string command="";
			DataConnection dcon=new DataConnection();
			for(int i=0;i<planNums.Length;i++){//loop through each plan
				//delete all benefits for all identical plans
				command="DELETE FROM benefit WHERE PlanNum="+POut.PInt(planNums[i]);
				dcon.NonQ(command);
				for(int j=0;j<newBenefitList.Count;j++){//loop through the new list
					if(newBenefitList[j]==null) {
						continue;
					}
					if(((Benefit)newBenefitList[j]).PatPlanNum!=0) {
						continue;//skip benefits attached to patients.  We are only concerned with ones attached to plans.
					}
					newBenefit=((Benefit)newBenefitList[j]).Copy();
					newBenefit.PlanNum=planNums[i];
					newBenefit.Insert();
				}
			}
			//don't forget to compute estimates for each plan now.
		}

		///<summary>Used in family module display to get a list of benefits.  The main purpose of this function is to group similar benefits for each plan on the same row, making it easier to display in a simple grid.  Supply a list of all benefits for the patient, and the patPlans for the patient.</summary>
		public static Benefit[,] GetDisplayMatrix(Benefit[] bensForPat,PatPlan[] patPlanList){
			ArrayList AL=new ArrayList();//each object is a Benefit[]
			Benefit[] row;
			ArrayList refAL=new ArrayList();//each object is a Benefit from any random column. Used when searching for a type.
			int col;
			for(int i=0;i<bensForPat.Length;i++){
				//determine the column
				col=-1;
				for(int j=0;j<patPlanList.Length;j++){
					if(patPlanList[j].PatPlanNum==bensForPat[i].PatPlanNum
						|| patPlanList[j].PlanNum==bensForPat[i].PlanNum)
					{
						col=j;
						break;
					}
				}
				if(col==-1){
					throw new Exception("col not found");//should never happen
				}
				//search refAL for a matching type that already exists
				row=null;
				for(int j=0;j<refAL.Count;j++){
					if(((Benefit)refAL[j]).CompareTo(bensForPat[i])==0){//if the type is equivalent
						row=(Benefit[])AL[j];
						break;
					}
				}
				//if no matching type found, add a row, and use that row
				if(row==null){
					refAL.Add(bensForPat[i].Copy());
					row=new Benefit[patPlanList.Length];
					row[col]=bensForPat[i].Copy();
					AL.Add(row);
					continue;
				}
				//if the column for the matching row is null, then use that row
				if(row[col]==null){
					row[col]=bensForPat[i].Copy();
					continue;
				}
				//if not null, then add another row.
				refAL.Add(bensForPat[i].Copy());
				row=new Benefit[patPlanList.Length];
				row[col]=bensForPat[i].Copy();
				AL.Add(row);
			}
			IComparer myComparer = new BenefitArraySorter();
			AL.Sort(myComparer);
			Benefit[,] retVal=new Benefit[patPlanList.Length,AL.Count];
			for(int y=0;y<AL.Count;y++){
				for(int x=0;x<patPlanList.Length;x++){
					if(((Benefit[])AL[y])[x]!=null){
						retVal[x,y]=((Benefit[])AL[y])[x].Copy();
					}
				}
			}
			return retVal;
		}

		///<summary>Deletes all benefits for a plan from the database.  Only used in FormInsPlan when picking a plan from the list.  Need to clear out benefits so that they won't be picked up when choosing benefits for all.</summary>
		public static void DeleteForPlan(int planNum){
			string command="DELETE FROM benefit WHERE PlanNum="+POut.PInt(planNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

	}

	/*================================================================================================================
	=========================================== class BenefitArraySorter =============================================*/
	///<summary></summary>
	public class BenefitArraySorter:IComparer {
		///<summary></summary>
		int IComparer.Compare(Object x,Object y) {
			Benefit[] array1=(Benefit[])x;
			Benefit ben1=null;
			for(int i=0;i<array1.Length;i++){
				if(array1[i]==null){
					continue;
				}
				ben1=array1[i].Copy();
				break;
			}
			Benefit[] array2=(Benefit[])y;
			Benefit ben2=null;
			for(int i=0;i<array2.Length;i++) {
				if(array2[i]==null) {
					continue;
				}
				ben2=array2[i].Copy();
				break;
			}
			return(ben1.CompareTo(ben2));
		}

	}

		



		
	

	

	


}










