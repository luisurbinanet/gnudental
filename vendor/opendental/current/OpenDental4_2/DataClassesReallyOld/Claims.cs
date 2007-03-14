using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace OpenDental{

		///<summary>Corresponds to the claim table in the database.</summary>
	///<remarks>The claim table holds information about individual claims.  Each row represents one claim.</remarks>
	public struct Claim{
		///<summary>Primary key</summary>
		public int ClaimNum;
		///<summary>Foreign key to <see cref="Patient.PatNum">patient.PatNum</see></summary>
		public int PatNum;//
		///<summary>Usually the same date as the procedures, but it can be changed if you wish.</summary>
		public DateTime DateService;//
		///<summary>Usually the date it was created.  It might be sent a few days later if you don't send your e-claims every day.</summary>
		public DateTime DateSent;
		///<summary>Single char: U,H,W,P,S,or R.</summary>
		///<remarks>U=Unsent, H=Hold until pri received, W=Waiting in queue, P=Probably sent, S=Sent, R=Received.  A(adj) is no longer used.</remarks>
		public string ClaimStatus;//
		///<summary>Date the claim was received.</summary>
		public DateTime DateReceived;
		///<summary>Foreign key to InsPlan.PlanNum</summary>
		public int PlanNum;
		///<summary>Treating provider. Foreign key to Provider.ProvNum.</summary>
		public int ProvTreat;//
		///<summary>Total fee of claim.</summary>
		public double ClaimFee;
		///<summary>Amount insurance is estimated to pay on this claim.</summary>
		public double InsPayEst;
		///<summary>Amount insurance actually paid.</summary>
		public double InsPayAmt;
		///<summary>Deductible applied to this claim.</summary>
		public double DedApplied;
		///<summary>The preauth number received from ins.</summary>
		public string PreAuthString;
		///<summary>single char for No, Initial, or Replacement.</summary>
		public string IsProsthesis;
		///<summary>Date prior prosthesis was placed.</summary>
		public DateTime PriorDate;
		///<summary>Note for patient for why insurance didn't pay as expected.</summary>
		public string ReasonUnderPaid;//
		///<summary>Note to be sent to insurance. Max 255 char.</summary>
		public string ClaimNote;//
		///<summary>"P"=primary, "S"=secondary, "PreAuth"=preauth, "Other"=other, "Cap"=capitation.  Not allowed to be blank. Might need to add "Med"=medical claim.</summary>
		public string ClaimType;
		///<summary>Billing provider.  Foreign key to Provider.ProvNum.</summary>
		public int ProvBill;
		///<summary>Foreign key to Referral.ReferralNum.</summary>
		public int ReferringProv;
		///<summary>Referral number for this claim.</summary>
		public string RefNumString;
		///<summary>See the PlaceOfService enum.</summary>
		public PlaceOfService PlaceService;
		///<summary>blank or A=Auto, E=Employment, O=Other.</summary>
		public string AccidentRelated;
		///<summary>Date of accident, if applicable.</summary>
		public DateTime AccidentDate;
		///<summary>Accident state.</summary>
		public string AccidentST;
		///<summary>See the YN enum.</summary>
		public YN EmployRelated;
		///<summary>True if is ortho.</summary>
		public bool IsOrtho;
		///<summary>Remaining months of ortho. Valid values are 1-36.</summary>
		public int OrthoRemainM;
		///<summary>Date ortho appliance placed.</summary>
		public DateTime OrthoDate;
		///<summary>Relationship to subscriber.  See the Relat enumeration.</summary>
		///<remarks>You no longer have to look in patient to find the relationship, since it is copied over when the claim is created.</remarks>
		public Relat PatRelat;
		///<summary>Other coverage plan number.  Foreign key to InsPlan.PlanNum for other coverage.  0 if none.</summary>
		///<remarks>This provides the user with total control over what other coverage shows. This obviously limits the coverage on a single claim to two insurance companies.</remarks>
		public int PlanNum2;
		///<summary>The relationsip to the subscriber for other coverage on this claim.</summary>
		public Relat PatRelat2;
		///<summary>Sum of ClaimProc.Writeoff for this claim.</summary>
		public double WriteOff;
		///<summary>The number of x-rays enclosed.</summary>
		public int Radiographs;
		///<summary>Foreign key to clinic.ClinicNum.  0 if no clinic.</summary>
		public int ClinicNum;
	}
	
	/*=========================================================================================
	=================================== class Claims ==========================================*/
	///<summary></summary>
	public class Claims:DataClass{
		///<summary></summary>
		public static Claim[] List;
		///<summary></summary>
		public static Hashtable HList;
		///<summary></summary>
		public static Claim Cur;
		//<summary></summary>
		//public static QueueItem[] ListQueue;
		//<summary></summary>
		//public static QueueItem CurQueue;

		///<summary></summary>
		public static ClaimPaySplit[] RefreshByCheck(int claimPaymentNum, bool showUnattached){
			cmd.CommandText =
				"SELECT claim.DateService,claim.ProvTreat,CONCAT(patient.LName,', ',patient.FName) AS PatName"
				+",carrier.CarrierName,SUM(claimproc.FeeBilled),SUM(claimproc.InsPayAmt),claim.ClaimNum"
				+",claimproc.ClaimPaymentNum"
				+" FROM claim,patient,insplan,carrier,claimproc" // added carrier, SPK 8/04
				+" WHERE claimproc.ClaimNum = claim.ClaimNum"
				+" AND patient.PatNum = claim.PatNum"
				+" AND insplan.PlanNum = claim.PlanNum"
				+" AND insplan.CarrierNum = carrier.CarrierNum"	// added SPK
				+" AND (claimproc.Status = '1' OR claimproc.Status = '4')"//received or supplemental
 				+" AND (claimproc.ClaimPaymentNum = '"+claimPaymentNum+"'";
			if(showUnattached){
				cmd.CommandText+=" OR (claimproc.InsPayAmt != 0 && claimproc.ClaimPaymentNum = '0'))"
					+" GROUP BY claimproc.ClaimNum";
			}
			else{//shows only items attached to this payment
				cmd.CommandText+=")"
					+" GROUP BY claimproc.ClaimNum";
			}
			cmd.CommandText+=" ORDER BY PatName";
			//MessageBox.Show(
			FillTable();
			ClaimPaySplit[] splits=new ClaimPaySplit[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				splits[i]=new ClaimPaySplit();
				splits[i].DateClaim      =PIn.PDate  (table.Rows[i][0].ToString());
				splits[i].ProvAbbr       =Providers.GetAbbr(PIn.PInt(table.Rows[i][1].ToString()));
				splits[i].PatName        =PIn.PString(table.Rows[i][2].ToString());
				splits[i].Carrier        =PIn.PString(table.Rows[i][3].ToString());
				splits[i].FeeBilled      =PIn.PDouble(table.Rows[i][4].ToString());
				splits[i].InsPayAmt      =PIn.PDouble(table.Rows[i][5].ToString());
				splits[i].ClaimNum       =PIn.PInt   (table.Rows[i][6].ToString());
				splits[i].ClaimPaymentNum=PIn.PInt   (table.Rows[i][7].ToString());
			}
			return splits;
		}

		///<summary>Gets the specified claim from the database.</summary>
		public static Claim GetClaim(int claimNum){
			string command="SELECT * FROM claim"
				+" WHERE ClaimNum = "+claimNum.ToString();
			Claim retClaim=SubmitAndFill(command,true);
			return retClaim;
		}

		///<summary>Gets all claims for the specified patient.</summary>
		public static void Refresh(int patNum){
			string command=
				"SELECT * FROM claim"
				+" WHERE PatNum = "+patNum.ToString()
				+" ORDER BY dateservice";
			SubmitAndFill(command,false);
		}

		private static Claim SubmitAndFill(string command,bool single){
			cmd.CommandText=command;
			FillTable();
			Claim tempClaim;
			if(!single){
				List=new Claim[table.Rows.Count];
				HList=new Hashtable();
			}
			Claim retVal=new Claim();
			for(int i=0;i<table.Rows.Count;i++){
				tempClaim=new Claim();
				tempClaim.ClaimNum     =		PIn.PInt   (table.Rows[i][0].ToString());
				tempClaim.PatNum       =		PIn.PInt   (table.Rows[i][1].ToString());
				tempClaim.DateService  =		PIn.PDate  (table.Rows[i][2].ToString());
				tempClaim.DateSent     =		PIn.PDate  (table.Rows[i][3].ToString());
				tempClaim.ClaimStatus  =		PIn.PString(table.Rows[i][4].ToString());
				tempClaim.DateReceived =		PIn.PDate  (table.Rows[i][5].ToString());
				tempClaim.PlanNum      =		PIn.PInt   (table.Rows[i][6].ToString());
				tempClaim.ProvTreat    =		PIn.PInt   (table.Rows[i][7].ToString());
				tempClaim.ClaimFee     =		PIn.PDouble(table.Rows[i][8].ToString());
				tempClaim.InsPayEst    =		PIn.PDouble(table.Rows[i][9].ToString());
				tempClaim.InsPayAmt    =		PIn.PDouble(table.Rows[i][10].ToString());
				tempClaim.DedApplied   =		PIn.PDouble(table.Rows[i][11].ToString());
				tempClaim.PreAuthString=		PIn.PString(table.Rows[i][12].ToString());
				tempClaim.IsProsthesis =		PIn.PString(table.Rows[i][13].ToString());
				tempClaim.PriorDate    =		PIn.PDate  (table.Rows[i][14].ToString());
				tempClaim.ReasonUnderPaid=	PIn.PString(table.Rows[i][15].ToString());
				tempClaim.ClaimNote    =		PIn.PString(table.Rows[i][16].ToString());
				tempClaim.ClaimType    =    PIn.PString(table.Rows[i][17].ToString());
				tempClaim.ProvBill     =		PIn.PInt   (table.Rows[i][18].ToString());
				tempClaim.ReferringProv=		PIn.PInt   (table.Rows[i][19].ToString());
				tempClaim.RefNumString =		PIn.PString(table.Rows[i][20].ToString());
				tempClaim.PlaceService = (PlaceOfService)PIn.PInt(table.Rows[i][21].ToString());
				tempClaim.AccidentRelated=	PIn.PString(table.Rows[i][22].ToString());
				tempClaim.AccidentDate  =		PIn.PDate  (table.Rows[i][23].ToString());
				tempClaim.AccidentST    =		PIn.PString(table.Rows[i][24].ToString());
				tempClaim.EmployRelated=(YN)PIn.PInt   (table.Rows[i][25].ToString());
				tempClaim.IsOrtho       =		PIn.PBool  (table.Rows[i][26].ToString());
				tempClaim.OrthoRemainM  =		PIn.PInt   (table.Rows[i][27].ToString());
				tempClaim.OrthoDate     =		PIn.PDate  (table.Rows[i][28].ToString());
				tempClaim.PatRelat      =(Relat)PIn.PInt(table.Rows[i][29].ToString());
				tempClaim.PlanNum2      =   PIn.PInt   (table.Rows[i][30].ToString());
				tempClaim.PatRelat2     =(Relat)PIn.PInt(table.Rows[i][31].ToString());
				tempClaim.WriteOff      =   PIn.PDouble(table.Rows[i][32].ToString());
				tempClaim.Radiographs   =   PIn.PInt   (table.Rows[i][33].ToString());
				tempClaim.ClinicNum     =   PIn.PInt   (table.Rows[i][34].ToString());
				if(single){
					retVal=tempClaim;
				}
				else{
					List[i]=tempClaim;
					HList.Add(tempClaim.ClaimNum,tempClaim);
				}
			}//end for
			return retVal;//only really used if single
		}

		///<summary></summary>
		public static void InsertCur(){
			if(Prefs.RandomKeys){
				Cur.ClaimNum=MiscData.GetKey("claim","ClaimNum");
			}
			cmd.CommandText="INSERT INTO claim (";
			if(Prefs.RandomKeys){
				cmd.CommandText+="ClaimNum,";
			}
			cmd.CommandText+="patnum,dateservice,datesent,claimstatus,datereceived"
				+",plannum,provtreat,claimfee,inspayest,inspayamt,dedapplied"
				+",preauthstring,isprosthesis,priordate,reasonunderpaid,claimnote"
				+",claimtype,provbill,referringprov"
				+",refnumstring,placeservice,accidentrelated,accidentdate,accidentst"
				+",employrelated,isortho,orthoremainm,orthodate,patrelat,plannum2"
				+",patrelat2,writeoff,Radiographs,ClinicNum) VALUES(";
			if(Prefs.RandomKeys){
				cmd.CommandText+="'"+POut.PInt(Cur.ClaimNum)+"', ";
			}
			cmd.CommandText+=
				 "'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PDate  (Cur.DateService)+"', "
				+"'"+POut.PDate  (Cur.DateSent)+"', "
				+"'"+POut.PString(Cur.ClaimStatus)+"', "
				+"'"+POut.PDate  (Cur.DateReceived)+"', "
				+"'"+POut.PInt   (Cur.PlanNum)+"', "
				+"'"+POut.PInt   (Cur.ProvTreat)+"', "
				+"'"+POut.PDouble(Cur.ClaimFee)+"', "
				+"'"+POut.PDouble(Cur.InsPayEst)+"', "
				+"'"+POut.PDouble(Cur.InsPayAmt)+"', "
				+"'"+POut.PDouble(Cur.DedApplied)+"', "
				+"'"+POut.PString(Cur.PreAuthString)+"', "
				+"'"+POut.PString(Cur.IsProsthesis)+"', "
				+"'"+POut.PDate  (Cur.PriorDate)+"', "
				+"'"+POut.PString(Cur.ReasonUnderPaid)+"', "
				+"'"+POut.PString(Cur.ClaimNote)+"', "
				+"'"+POut.PString(Cur.ClaimType)+"', "
				+"'"+POut.PInt   (Cur.ProvBill)+"', "
				+"'"+POut.PInt   (Cur.ReferringProv)+"', "
				+"'"+POut.PString(Cur.RefNumString)+"', "
				+"'"+POut.PInt   ((int)Cur.PlaceService)+"', "
				+"'"+POut.PString(Cur.AccidentRelated)+"', "
				+"'"+POut.PDate  (Cur.AccidentDate)+"', "
				+"'"+POut.PString(Cur.AccidentST)+"', "
				+"'"+POut.PInt   ((int)Cur.EmployRelated)+"', "
				+"'"+POut.PBool  (Cur.IsOrtho)+"', "
				+"'"+POut.PInt   (Cur.OrthoRemainM)+"', "
				+"'"+POut.PDate  (Cur.OrthoDate)+"', "
				+"'"+POut.PInt   ((int)Cur.PatRelat)+"', "
				+"'"+POut.PInt   (Cur.PlanNum2)+"', "
				+"'"+POut.PInt   ((int)Cur.PatRelat2)+"', "
				+"'"+POut.PDouble(Cur.WriteOff)+"', "
				+"'"+POut.PInt   (Cur.Radiographs)+"', "
				+"'"+POut.PInt   (Cur.ClinicNum)+"')";
			if(Prefs.RandomKeys){
				NonQ();
			}
			else{
 				NonQ(true);
				Cur.ClaimNum=InsertID;
			}
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE claim SET "
				+"patnum = '"          +POut.PInt   (Cur.PatNum)+"' "
				+",dateservice = '"    +POut.PDate  (Cur.DateService)+"' "
				+",datesent = '"       +POut.PDate  (Cur.DateSent)+"' "
				+",claimstatus = '"    +POut.PString(Cur.ClaimStatus)+"' "
				+",datereceived = '"   +POut.PDate  (Cur.DateReceived)+"' "
				+",plannum = '"        +POut.PInt   (Cur.PlanNum)+"' "
				+",provtreat = '"      +POut.PInt   (Cur.ProvTreat)+"' "
				+",claimfee = '"       +POut.PDouble(Cur.ClaimFee)+"' "
				+",inspayest = '"      +POut.PDouble(Cur.InsPayEst)+"' "
				+",inspayamt = '"      +POut.PDouble(Cur.InsPayAmt)+"' "
				+",dedapplied = '"   +  POut.PDouble(Cur.DedApplied)+"' "
				+",preauthstring = '"+	POut.PString(Cur.PreAuthString)+"' "
				+",isprosthesis = '" +	POut.PString(Cur.IsProsthesis)+"' "
				+",priordate = '"    +	POut.PDate  (Cur.PriorDate)+"' "
				+",reasonunderpaid = '"+POut.PString(Cur.ReasonUnderPaid)+"' "
				+",claimnote = '"    +	POut.PString(Cur.ClaimNote)+"' "
				+",claimtype='"      +	POut.PString(Cur.ClaimType)+"' "
				+",provbill = '"     +	POut.PInt   (Cur.ProvBill)+"' "
				+",referringprov = '"+	POut.PInt   (Cur.ReferringProv)+"' "
				+",refnumstring = '" +	POut.PString(Cur.RefNumString)+"' "
				+",placeservice = '" +	POut.PInt   ((int)Cur.PlaceService)+"' "
				+",accidentrelated = '"+POut.PString(Cur.AccidentRelated)+"' "
				+",accidentdate = '" +	POut.PDate  (Cur.AccidentDate)+"' "
				+",accidentst = '"   +	POut.PString(Cur.AccidentST)+"' "
				+",employrelated = '"+	POut.PInt   ((int)Cur.EmployRelated)+"' "
				+",isortho = '"      +	POut.PBool  (Cur.IsOrtho)+"' "
				+",orthoremainm = '" +	POut.PInt   (Cur.OrthoRemainM)+"' "
				+",orthodate = '"    +	POut.PDate  (Cur.OrthoDate)+"' "
				+",patrelat = '"     +	POut.PInt   ((int)Cur.PatRelat)+"' "
				+",plannum2 = '"     +	POut.PInt   (Cur.PlanNum2)+"' "
				+",patrelat2 = '"    +	POut.PInt   ((int)Cur.PatRelat2)+"' "
				+",writeoff = '"     +	POut.PDouble(Cur.WriteOff)+"' "
				+",Radiographs = '"  +  POut.PInt   (Cur.Radiographs)+"' "
				+",ClinicNum = '"    +  POut.PInt   (Cur.ClinicNum)+"' "
				+"WHERE claimnum = '"+	POut.PInt   (Cur.ClaimNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM claim "
				+"WHERE claimnum = '"+POut.PInt(Cur.ClaimNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		//public static void GetProcsInClaim(int myClaimNum){
			//moved to claimprocs

		///<summary></summary>
		public static void DetachProcsFromClaim(){
			cmd.CommandText = "UPDATE procedurelog SET "
				+"claimnum = '0' "
				+"WHERE claimnum = '"+POut.PInt(Cur.ClaimNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary>Called from claimsend window.</summary>
		public static ClaimSendQueueItem[] GetQueueList(){
			cmd.CommandText =
				"SELECT claim.ClaimNum,carrier.NoSendElect"
				+",concat(patient.LName,', ',patient.FName,' ',patient.MiddleI)"
				+",claim.ClaimStatus,carrier.CarrierName,patient.PatNum,carrier.ElectID,insplan.IsMedical "
				+"FROM claim "
				+"Left join insplan on claim.PlanNum = insplan.PlanNum "
				+"Left join carrier on insplan.CarrierNum = carrier.CarrierNum "
				+"Left join patient on patient.PatNum = claim.PatNum "
				+"WHERE claim.ClaimStatus = 'W' || claim.ClaimStatus = 'P' "
				+"ORDER BY insplan.IsMedical";//this puts the medical claims at the end, helping with the looping in X12.
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			ClaimSendQueueItem[] listQueue=new ClaimSendQueueItem[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				listQueue[i]=new ClaimSendQueueItem();
				listQueue[i].ClaimNum        = PIn.PInt   (table.Rows[i][0].ToString());
				listQueue[i].NoSendElect     = PIn.PBool  (table.Rows[i][1].ToString());
				listQueue[i].PatName         = PIn.PString(table.Rows[i][2].ToString());
				listQueue[i].ClaimStatus     = PIn.PString(table.Rows[i][3].ToString());
				listQueue[i].Carrier         = PIn.PString(table.Rows[i][4].ToString());
				listQueue[i].PatNum          = PIn.PInt   (table.Rows[i][5].ToString());
				listQueue[i].ClearinghouseNum=Clearinghouses.GetNumForPayor(PIn.PString(table.Rows[i][6].ToString()));
				listQueue[i].IsMedical       = PIn.PBool  (table.Rows[i][7].ToString());
			}
			return listQueue;
		}

		///<summary></summary>
		public static void UpdateStatus(int claimNum,string newStatus){
			cmd.CommandText = "UPDATE claim SET "
				+"claimstatus = '"+newStatus+"' "
				+"WHERE claimnum = '"+claimNum+"'";
			NonQ(false);
		}

		///<summary>Supply an arrayList of type ClaimSendQueueItem. Called from X12 to begin the sorting process on claims going to one clearinghouse. Returns an array with Carrier,ProvBill,Subscriber,PatNum,ClaimNum, all in the correct order. Carrier is a string, the rest are int.</summary>
		public static object[,] GetX12TransactionInfo(ArrayList queueItems){
			StringBuilder str=new StringBuilder();
			for(int i=0;i<queueItems.Count;i++){
				if(i>0){
					str.Append(" OR");
				}
				str.Append(" claim.ClaimNum="+((ClaimSendQueueItem)queueItems[i]).ClaimNum.ToString());
			}
			cmd.CommandText="SELECT carrier.ElectID,claim.ProvBill,insplan.Subscriber,"
				+"claim.PatNum,claim.ClaimNum "
				+"FROM claim,insplan,carrier "
				+"WHERE claim.PlanNum=insplan.PlanNum "
				+"AND carrier.CarrierNum=insplan.CarrierNum "
				+"AND ("+str.ToString()+") "
				+"ORDER BY carrier.ElectID,claim.ProvBill,insplan.Subscriber,insplan.Subscriber!=claim.PatNum,claim.PatNum";
			FillTable();
			object[,] myA=new object[5,table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				myA[0,i]=PIn.PString(table.Rows[i][0].ToString());
				myA[1,i]=PIn.PInt   (table.Rows[i][1].ToString());
				myA[2,i]=PIn.PInt   (table.Rows[i][2].ToString());
				myA[3,i]=PIn.PInt   (table.Rows[i][3].ToString());
				myA[4,i]=PIn.PInt   (table.Rows[i][4].ToString());
			}
			return myA;
		}

		///<summary>Updates all claimproc estimates and also updates claim totals to db. Must supply all claimprocs for this patient.  Must supply procList which includes all procedures that this claim is linked to.  Will also need to refresh afterwards to see the results</summary>
		public static void CalculateAndUpdate(ClaimProc[] ClaimProcList,Procedure[] procList,InsPlan[] PlanList,Claim ClaimCur,PatPlan[] patPlans,Benefit[] benefitList){
			//Remember that this can be called externally also
			//ClaimProcList=claimProcList;
			ClaimProc[] ClaimProcsForClaim=ClaimProcs.GetForClaim(ClaimProcList,ClaimCur.ClaimNum);
			double claimFee=0;
			double dedApplied=0;
			double insPayEst=0;
			double insPayAmt=0;
			InsPlan PlanCur=InsPlans.GetPlan(ClaimCur.PlanNum,PlanList);
			if(PlanCur==null){
				return;
			}
			//InsPlans.Cur=(InsPlan)InsPlans.HList[ClaimCur.PlanNum];
			int provNum;
			double dedRem;
			double insRem;//takes annual max into consideration
			//first loop handles totals for received items.
			for(int i=0;i<ClaimProcsForClaim.Length;i++){
				if(ClaimProcsForClaim[i].Status!=ClaimProcStatus.Received){
					continue;//disregard any status except Receieved.
				}
				claimFee+=ClaimProcsForClaim[i].FeeBilled;
				dedApplied+=ClaimProcsForClaim[i].DedApplied;
				insPayEst+=ClaimProcsForClaim[i].InsPayEst;
				insPayAmt+=ClaimProcsForClaim[i].InsPayAmt;
			}
			//loop again only for procs not received.
			//And for preauth.
			Procedure ProcCur;
			int patPlanNum;
			for(int i=0;i<ClaimProcsForClaim.Length;i++){
				if(ClaimProcsForClaim[i].Status!=ClaimProcStatus.NotReceived
					&& ClaimProcsForClaim[i].Status!=ClaimProcStatus.Preauth){
					continue;
				}
				ProcCur=Procedures.GetProc(procList,ClaimProcsForClaim[i].ProcNum);
				if(ProcCur.ProcNum==0){
					continue;//ignores payments, etc
				}
				//fee:
				if(PlanCur.ClaimsUseUCR){//use UCR for the provider of the procedure
					provNum=ProcCur.ProvNum;
					if(provNum==0){//if no prov set, then use practice default.
						provNum=Convert.ToInt32(((Pref)Prefs.HList["PracticeDefaultProv"]).ValueString);
					}
					ClaimProcsForClaim[i].FeeBilled=Fees.GetAmount0(//get the fee based on ada and prov fee sched
						ProcCur.ADACode
						,Providers.ListLong[Providers.GetIndexLong(provNum)].FeeSched);
				}
				else{//don't use ucr.  Use the procedure fee instead.
					ClaimProcsForClaim[i].FeeBilled=ProcCur.ProcFee;
				}
				claimFee+=ClaimProcsForClaim[i].FeeBilled;
				if(ClaimCur.ClaimType=="PreAuth" || ClaimCur.ClaimType=="Other"){
					//only the fee gets calculated, the rest does not
					ClaimProcsForClaim[i].Update();
					continue;
				}
				//deduct:
				patPlanNum=PatPlans.GetPatPlanNum(patPlans,ClaimCur.PlanNum);
				if(patPlanNum==0){//patient does not have current coverage
					dedRem=0;
				}
				else{
					dedRem=InsPlans.GetDedRem(ClaimProcList,ClaimProcsForClaim[i].ProcDate,ClaimCur.PlanNum,patPlanNum,
						ClaimCur.ClaimNum,PlanList,benefitList,ProcCur.ADACode)
						-dedApplied;//subtracts deductible amounts already applied on this claim
					if(dedRem<0) {
						dedRem=0;
					}

				}
				if(dedRem > ClaimProcsForClaim[i].FeeBilled){//if deductible is more than cost of procedure
					ClaimProcsForClaim[i].DedApplied=ClaimProcsForClaim[i].FeeBilled;
				}
				else{
					ClaimProcsForClaim[i].DedApplied=dedRem;
				}
				//??obsolete: if dedApplied is too big, it might be adjusted in the next few lines.??
				//insest:
				//Unlike deductible, we do not need to take into account any of the received claimprocs when calculating insest.  So insRem takes care of annual max rather than received+est.
				if(patPlanNum==0){//patient does not have current coverage
					insRem=0;
				}
				else{
					insRem=InsPlans.GetInsRem(ClaimProcList,ClaimProcsForClaim[i].ProcDate,ClaimCur.PlanNum,
						patPlanNum,ClaimCur.ClaimNum,PlanList,benefitList)
						-insPayEst;//subtracts insest amounts already applied on this claim
					if(insRem<0) {
						insRem=0;
					}
				}
				if(ClaimCur.ClaimType=="P"){//primary
					ClaimProcsForClaim[i].ComputeBaseEst(ProcCur,PriSecTot.Pri,PlanList,patPlans,benefitList);//handles dedBeforePerc
					ClaimProcsForClaim[i].InsPayEst=ProcCur.GetEst(ClaimProcList,PriSecTot.Pri,patPlans);
						//ClaimProcsForClaim[i].BaseEst;
					if(!ClaimProcsForClaim[i].DedBeforePerc){
						ClaimProcsForClaim[i].InsPayEst-=ClaimProcsForClaim[i].DedApplied;
					}
				}
				else if(ClaimCur.ClaimType=="S"){//secondary
					ClaimProcsForClaim[i].ComputeBaseEst(ProcCur,PriSecTot.Sec,PlanList,patPlans,benefitList);
					ClaimProcsForClaim[i].InsPayEst=ProcCur.GetEst(ClaimProcList,PriSecTot.Sec,patPlans);
						//ClaimProcsForClaim[i].BaseEst;
					if(!ClaimProcsForClaim[i].DedBeforePerc){
						ClaimProcsForClaim[i].InsPayEst-=ClaimProcsForClaim[i].DedApplied;
					}
				}
				//other claimtypes only changed manually
				if(ClaimProcsForClaim[i].InsPayEst < 0){
					//example: if inspayest = 19 - 50(ded) for total of -31.
					ClaimProcsForClaim[i].DedApplied+=ClaimProcsForClaim[i].InsPayEst;//eg. 50+(-31)=19
					ClaimProcsForClaim[i].InsPayEst=0;
					//so only 19 of deductible gets applied, and inspayest is 0
				}
				if(ClaimProcsForClaim[i].InsPayEst>insRem){
					ClaimProcsForClaim[i].InsPayEst=insRem;
				}
				dedApplied+=ClaimProcsForClaim[i].DedApplied;
				insPayEst+=ClaimProcsForClaim[i].InsPayEst;
				ClaimProcsForClaim[i].Update();
				//but notice that the ClaimProcs lists are not refreshed until the loop is finished.
			}//for claimprocs.forclaim
			ClaimCur.ClaimFee=claimFee;
			ClaimCur.DedApplied=dedApplied;
			ClaimCur.InsPayEst=insPayEst;
			ClaimCur.InsPayAmt=insPayAmt;
			Cur=ClaimCur;
			UpdateCur();
		}
	}//end class Claims



	///<summary>Used to hold a list of claims to show in the claims 'queue' waiting to be sent.</summary>
	public class ClaimSendQueueItem{
		///<summary></summary>
		public int ClaimNum;
		///<summary></summary>
		public bool NoSendElect;
		///<summary></summary>
		public string PatName;
		///<summary>Single char: U,H,W,P,S,or R.</summary>
		///<remarks>U=Unsent, H=Hold until pri received, W=Waiting in queue, P=Probably sent, S=Sent, R=Received.  A(adj) is no longer used.</remarks>
		public string ClaimStatus;
		///<summary></summary>
		public string Carrier;
		///<summary></summary>
		public int PatNum;
		///<summary></summary>
		public int ClearinghouseNum;
		///<summary>True if the plan is a medical plan.</summary>
		public bool IsMedical;
	}

	///<summary>Used to hold a list of claims to show in the Claim Check Edit window.</summary>
	public class ClaimPaySplit{
		///<summary></summary>
		public int ClaimNum;
		///<summary></summary>
		public string PatName;
		///<summary></summary>
		public string Carrier;
		///<summary></summary>
		public DateTime DateClaim;
		///<summary></summary>
		public string ProvAbbr;
		///<summary></summary>
		public double FeeBilled;
		///<summary></summary>
		public double InsPayAmt;
		///<summary></summary>
		public int ClaimPaymentNum;
	}



}









