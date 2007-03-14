using System;
using System.Collections;
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
		///<summary>"P"=primary, "S"=secondary, "PreAuth"=preauth, "Other"=other, "Cap"=capitation</summary>
		///<remarks>ClaimType is the new way of determining claimtype. Not allowed to be blank. The update for version 2.1 added "P" or "S" to all existing claims.</remarks>
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
		///<summary></summary>
		public static QueueItem[] ListQueue;
		///<summary></summary>
		public static QueueItem CurQueue;

		///<summary></summary>
		public static void RefreshByCheck(int claimPaymentNum, bool showUnattached){
			cmd.CommandText =
				"SELECT claim.DateService,claim.ProvTreat,CONCAT(patient.LName,', ',patient.FName)"
				+",carrier.CarrierName,SUM(claimproc.FeeBilled),SUM(claimproc.InsPayAmt),claim.ClaimNum"
				+",claimproc.ClaimPaymentNum"
				+" FROM claim,patient,insplan,carrier,claimproc" // added carrier, SPK 8/04
				+" WHERE claimproc.ClaimNum = claim.ClaimNum"
				+" && patient.PatNum = claim.PatNum"
				+" && insplan.PlanNum = claim.PlanNum"
				+" && insplan.CarrierNum = carrier.CarrierNum"	// added SPK
				+" && (claimproc.Status = '1' || claimproc.Status = '4')"//received or supplemental
 				+" && (claimproc.ClaimPaymentNum = '"+claimPaymentNum+"'";
			if(showUnattached){
				cmd.CommandText+=" || (claimproc.InsPayAmt > 0 && claimproc.ClaimPaymentNum = '0'))"
					+" GROUP BY claimproc.ClaimNum";
			}
			else{//shows only items attached to this payment
				cmd.CommandText+=")"
					+" GROUP BY claimproc.ClaimNum";
			}
			//MessageBox.Show(
			FillTable();
			ListQueue=new QueueItem[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				ListQueue[i].DateClaim      =PIn.PDate  (table.Rows[i][0].ToString());
				ListQueue[i].ProvAbbr       =Providers.GetAbbr(PIn.PInt(table.Rows[i][1].ToString()));
				ListQueue[i].PatName        =PIn.PString(table.Rows[i][2].ToString());
				ListQueue[i].Carrier        =PIn.PString(table.Rows[i][3].ToString());
				ListQueue[i].FeeBilled      =PIn.PDouble(table.Rows[i][4].ToString());
				ListQueue[i].InsPayAmt      =PIn.PDouble(table.Rows[i][5].ToString());
				ListQueue[i].ClaimNum       =PIn.PInt   (table.Rows[i][6].ToString());
				ListQueue[i].ClaimPaymentNum=PIn.PInt   (table.Rows[i][7].ToString());
			}
		}

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * FROM claim"
				+" WHERE patnum = '"+Patients.Cur.PatNum+"'"
				+" ORDER BY dateservice";
			FillTable();
			List=new Claim[table.Rows.Count];
			HList=new Hashtable();
			for(int i=0;i<table.Rows.Count;i++){
				List[i].ClaimNum     =		PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum       =		PIn.PInt   (table.Rows[i][1].ToString());
				List[i].DateService  =		PIn.PDate  (table.Rows[i][2].ToString());
				List[i].DateSent     =		PIn.PDate  (table.Rows[i][3].ToString());
				List[i].ClaimStatus  =		PIn.PString(table.Rows[i][4].ToString());
				List[i].DateReceived =		PIn.PDate  (table.Rows[i][5].ToString());
				List[i].PlanNum      =		PIn.PInt   (table.Rows[i][6].ToString());
				List[i].ProvTreat    =		PIn.PInt   (table.Rows[i][7].ToString());
				List[i].ClaimFee     =		PIn.PDouble(table.Rows[i][8].ToString());
				List[i].InsPayEst    =		PIn.PDouble(table.Rows[i][9].ToString());
				List[i].InsPayAmt    =		PIn.PDouble(table.Rows[i][10].ToString());
				List[i].DedApplied   =		PIn.PDouble(table.Rows[i][11].ToString());
				List[i].PreAuthString=		PIn.PString(table.Rows[i][12].ToString());
				List[i].IsProsthesis =		PIn.PString(table.Rows[i][13].ToString());
				List[i].PriorDate    =		PIn.PDate  (table.Rows[i][14].ToString());
				List[i].ReasonUnderPaid=	PIn.PString(table.Rows[i][15].ToString());
				List[i].ClaimNote    =		PIn.PString(table.Rows[i][16].ToString());
				List[i].ClaimType    =    PIn.PString(table.Rows[i][17].ToString());
				List[i].ProvBill     =		PIn.PInt   (table.Rows[i][18].ToString());
				List[i].ReferringProv=		PIn.PInt   (table.Rows[i][19].ToString());
				List[i].RefNumString =		PIn.PString(table.Rows[i][20].ToString());
				List[i].PlaceService = (PlaceOfService)PIn.PInt(table.Rows[i][21].ToString());
				List[i].AccidentRelated=	PIn.PString(table.Rows[i][22].ToString());
				List[i].AccidentDate  =		PIn.PDate  (table.Rows[i][23].ToString());
				List[i].AccidentST    =		PIn.PString(table.Rows[i][24].ToString());
				List[i].EmployRelated=(YN)PIn.PInt   (table.Rows[i][25].ToString());
				List[i].IsOrtho       =		PIn.PBool  (table.Rows[i][26].ToString());
				List[i].OrthoRemainM  =		PIn.PInt   (table.Rows[i][27].ToString());
				List[i].OrthoDate     =		PIn.PDate  (table.Rows[i][28].ToString());
				List[i].PatRelat      =(Relat)PIn.PInt(table.Rows[i][29].ToString());
				List[i].PlanNum2      =   PIn.PInt   (table.Rows[i][30].ToString());
				List[i].PatRelat2     =(Relat)PIn.PInt(table.Rows[i][31].ToString());
				List[i].WriteOff      =   PIn.PDouble(table.Rows[i][32].ToString());
				List[i].Radiographs   =   PIn.PInt   (table.Rows[i][33].ToString());
				HList.Add(List[i].ClaimNum,List[i]);
			}//end for
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO claim (patnum,dateservice,datesent,claimstatus,datereceived"
				+",plannum,provtreat,claimfee,inspayest,inspayamt,dedapplied"
				+",preauthstring,isprosthesis,priordate,reasonunderpaid,claimnote"
				+",claimtype,provbill,referringprov"
				+",refnumstring,placeservice,accidentrelated,accidentdate,accidentst"
				+",employrelated,isortho,orthoremainm,orthodate,patrelat,plannum2"
				+",patrelat2,writeoff,Radiographs) VALUES("
				+"'"+POut.PInt   (Cur.PatNum)+"', "
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
				+"'"+POut.PInt   (Cur.Radiographs)+"')";
			NonQ(true);
			Cur.ClaimNum=InsertID;
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
		public static void GetQueueList(){
			cmd.CommandText =
				"SELECT claim.ClaimNum,carrier.NoSendElect"
				+",concat(patient.LName,', ',patient.FName,' ',patient.MiddleI)"
				+",claim.ClaimStatus,carrier.CarrierName,patient.PatNum "
				+"FROM claim "
				+"Left join insplan on claim.PlanNum = insplan.PlanNum "
				+"Left join carrier on insplan.CarrierNum = carrier.CarrierNum "
				+"Left join patient on patient.PatNum = claim.PatNum "
				+"WHERE claim.ClaimStatus = 'W' || claim.ClaimStatus = 'P'";
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			ListQueue=new QueueItem[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				ListQueue[i].ClaimNum   = PIn.PInt   (table.Rows[i][0].ToString());
				ListQueue[i].NoSendElect= PIn.PBool  (table.Rows[i][1].ToString());
				ListQueue[i].PatName    = PIn.PString(table.Rows[i][2].ToString());
				ListQueue[i].ClaimStatus= PIn.PString(table.Rows[i][3].ToString());
				ListQueue[i].Carrier    = PIn.PString(table.Rows[i][4].ToString());
				ListQueue[i].PatNum     = PIn.PInt   (table.Rows[i][5].ToString());
			}
		}

		///<summary></summary>
		public static void UpdateStatus(int claimNum,string newStatus){
			cmd.CommandText = "UPDATE claim SET "
				+"claimstatus = '"+newStatus+"' "
				+"WHERE claimnum = '"+claimNum+"'";
			NonQ(false);
		}

	

	}//end class Claims



	///<summary>Used to hold a list of claims to show in the claims 'queue' waiting to be sent, and in the Claim Check Edit window.</summary>
	public struct QueueItem{
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









