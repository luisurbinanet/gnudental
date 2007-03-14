using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the claimproc table in the database.</summary>
	///<remarks>Links procedures to claims.  Also links ins payments to procedures or claims.  Warning: One proc might be linked twice to a given claim if insurance made two payments.  Many of the important fields are actually optional.  For instance, ProcNum is only required if itemizing ins payment, and ClaimNum is blank if Status=A for adjustment.</remarks>
	public struct ClaimProc{
		///<summary>Primary key.</summary>
		public int ClaimProcNum;
		///<summary>Foreign key to procedurelog.ProcNum.</summary>
		public int ProcNum;
		///<summary>Foreign key to claim.ClaimNum.</summary>
		public int ClaimNum;
		///<summary>Foreign key to patient.PatNum.</summary>
		public int PatNum;
		///<summary>Foreign key to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>Fee billed. Might not be the same as the actual fee.</summary>
		///<remarks>The fee billed can be different than the actual procedure.  For instance, if you have set the insurance plan to bill insurance using UCR fees, then this field will contain the UCR fee instead of the fee that the patient was charged.</remarks>
		public double FeeBilled;
		///<summary>Amount this carrier is expected to pay.</summary>
		public double InsPayEst;
		///<summary>Deductible applied to this procedure only.</summary>
		public double DedApplied;
		///<summary>See the ClaimProcStatus enumeration.</summary>
		public ClaimProcStatus Status;
		///<summary>Amount insurance actually paid.</summary>
		public double InsPayAmt;
		///<summary>The remarks that insurance sends in the EOB about procedures.</summary>
		public string Remarks;
		///<summary>Foreign key to ClaimPayment.ClaimPaymentNum(the insurance check).</summary>
		public int ClaimPaymentNum;
		///<summary>Foreign key to insplan.PlanNum</summary>
		public int PlanNum;
		///<summary>Date of this ClaimProc.  Especially useful for adjustments and balance.</summary>
		///<remarks>For payments, MUST be date of treatment, not payment, to properly track annual benefits.</remarks>
		public DateTime DateCP;
		///<summary>Amount not covered by ins which is written off</summary>
		public Double WriteOff;
		///<summary>The procedure code that was sent to insurance.</summary>
		///<remarks>This is not necessarily the usual procedure code.  It will already have been trimmed to 5 char if it started with "D", or it could be the alternate code.  Not allowed to be blank if it is procedure.</remarks>
		public string CodeSent;
	}

	/*=========================================================================================
	=================================== class ClaimProcs ===========================================*/

	///<summary></summary>
	public class ClaimProcs:DataClass{
		///<summary></summary>
		public static ClaimProc Cur;
		///<summary></summary>
		public static ClaimProc[] List;//all for Patients.Cur
		///<summary></summary>
		public static ClaimProc[] ForClaim;//ClaimProcs for Claims.Cur.ClaimNum
		//public static ArrayList ProcsInClaim;//AL of Procedures

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from claimproc "
				+"WHERE PatNum = '"+POut.PInt(Patients.Cur.PatNum)+"' ";
			FillTable();
			List=new ClaimProc[table.Rows.Count];
			for(int i = 0;i<List.Length;i++){
				List[i].ClaimProcNum   = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ProcNum        = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].ClaimNum       = PIn.PInt   (table.Rows[i][2].ToString());	
				List[i].PatNum         = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].ProvNum        = PIn.PInt   (table.Rows[i][4].ToString());
				List[i].FeeBilled      = PIn.PDouble(table.Rows[i][5].ToString());
				List[i].InsPayEst      = PIn.PDouble(table.Rows[i][6].ToString());
				List[i].DedApplied     = PIn.PDouble(table.Rows[i][7].ToString());
				List[i].Status         = (ClaimProcStatus)PIn.PInt(table.Rows[i][8].ToString());
				List[i].InsPayAmt      = PIn.PDouble(table.Rows[i][9].ToString());
				List[i].Remarks        = PIn.PString(table.Rows[i][10].ToString());
				List[i].ClaimPaymentNum= PIn.PInt   (table.Rows[i][11].ToString());
				List[i].PlanNum        = PIn.PInt   (table.Rows[i][12].ToString());
				List[i].DateCP         = PIn.PDate  (table.Rows[i][13].ToString());
				List[i].WriteOff       = PIn.PDouble(table.Rows[i][14].ToString());
				List[i].CodeSent       = PIn.PString(table.Rows[i][15].ToString());
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO claimproc (procnum,claimnum,patnum,provnum"
				+",feebilled,inspayest,dedapplied,status,inspayamt,remarks,claimpaymentnum"
				+",plannum,datecp,writeoff,codesent) VALUES ("
				+"'"+POut.PInt   (Cur.ProcNum)+"', "
				+"'"+POut.PInt   (Cur.ClaimNum)+"', "
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PInt   (Cur.ProvNum)+"', "
				+"'"+POut.PDouble(Cur.FeeBilled)+"', "
				+"'"+POut.PDouble(Cur.InsPayEst)+"', "
				+"'"+POut.PDouble(Cur.DedApplied)+"', "
				+"'"+POut.PInt   ((int)Cur.Status)+"', "
				+"'"+POut.PDouble(Cur.InsPayAmt)+"', "
				+"'"+POut.PString(Cur.Remarks)+"', "
				+"'"+POut.PInt   (Cur.ClaimPaymentNum)+"', "
				+"'"+POut.PInt   (Cur.PlanNum)+"', "
				+"'"+POut.PDate  (Cur.DateCP)+"', "
				+"'"+POut.PDouble(Cur.WriteOff)+"', "
				+"'"+POut.PString(Cur.CodeSent)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.ClaimProcNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE claimproc SET "
				+"procnum = '"        +POut.PInt   (Cur.ProcNum)+"'"
				+",claimnum = '"      +POut.PInt   (Cur.ClaimNum)+"' "
				+",patnum = '"        +POut.PInt   (Cur.PatNum)+"'"
				+",provnum = '"       +POut.PInt   (Cur.ProvNum)+"'"
				+",feebilled = '"     +POut.PDouble(Cur.FeeBilled)+"'"
				+",inspayest = '"     +POut.PDouble(Cur.InsPayEst)+"'"
				+",dedapplied = '"    +POut.PDouble(Cur.DedApplied)+"'"
				+",status = '"        +POut.PInt   ((int)Cur.Status)+"'"
				+",inspayamt = '"     +POut.PDouble(Cur.InsPayAmt)+"'"
				+",remarks = '"       +POut.PString(Cur.Remarks)+"'"
				+",claimpaymentnum= '"+POut.PInt   (Cur.ClaimPaymentNum)+"'"
				+",plannum= '"        +POut.PInt   (Cur.PlanNum)+"'"
				+",datecp= '"         +POut.PDate  (Cur.DateCP)+"'"
				+",writeoff= '"       +POut.PDouble(Cur.WriteOff)+"'"
				+",codesent= '"       +POut.PString(Cur.CodeSent)+"'"
				+" WHERE claimprocnum = '"+POut.PInt (Cur.ClaimProcNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from claimproc WHERE claimprocNum = '"+POut.PInt(Cur.ClaimProcNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void GetForClaim(){
			//MessageBox.Show(List.Length.ToString());
			ArrayList ALForClaim=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ClaimNum==Claims.Cur.ClaimNum){
					ALForClaim.Add(List[i]);  
				}
			}
			ForClaim=new ClaimProc[ALForClaim.Count];
			for(int i=0;i<ALForClaim.Count;i++){
				ForClaim[i]=(ClaimProc)ALForClaim[i];
			}
		}

		///<summary>Used in ProcEdit, and ContrAcct</summary>
		public static bool ProcIsAttached(int procNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum && List[i].Status!=ClaimProcStatus.Preauth){
					return true;
				}
			}
			return false;
		}

		///<summary>All claim procs for this procedure are received, not just some of them.  Assumes you have already tested ProcIsAttached==true.</summary>
		public static bool ProcIsReceived(int procNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum
					&& List[i].Status!=ClaimProcStatus.Received
					&& List[i].Status!=ClaimProcStatus.Supplemental){
					return false;
				}
			}
			return true;
		}

		///<summary></summary>
		public static bool ProcIsSent(int procNum){
			//Warning: In the future, the claim.hlist might not be already loaded and available.
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum){
					if(((Claim)Claims.HList[List[i].ClaimNum]).ClaimStatus != "U"//not unsent
						&& ((Claim)Claims.HList[List[i].ClaimNum]).ClaimStatus != "H"){//not hold
						//must be sent
						return true;
					}
				}
			}
			return false;
		}

		///<summary></summary>
		public static bool ProcIsPaid(int procNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum
					&& List[i].InsPayAmt > 0//ins paid
					&& List[i].Status!=ClaimProcStatus.Preauth){
					return true;
				}
			}
			return false;
		}

		///<summary>The insurance estimate based on all claimprocs with this procNum.</summary>
		public static double ProcInsEst(int procNum){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum
					//&& List[i].InsPayAmt > 0//ins paid
					&& List[i].Status!=ClaimProcStatus.Preauth){
					retVal+=List[i].InsPayEst;
				}
			}
			return retVal;
		}

		///<summary>The insurance estimate based on all claimprocs with this procNum, but only for those claimprocs that are not received yet.</summary>
		public static double ProcEstNotReceived(int procNum){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum
					&& List[i].Status!=ClaimProcStatus.Preauth
					&& List[i].Status!=ClaimProcStatus.Received
					&& List[i].Status!=ClaimProcStatus.Supplemental){
					retVal+=List[i].InsPayEst;
				}
			}
			return retVal;
		}
		

		///<summary>The insurance amount paid based on all claimprocs with this procNum.</summary>
		public static double ProcInsPay(int procNum){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum
					//&& List[i].InsPayAmt > 0//ins paid
					&& List[i].Status!=ClaimProcStatus.Preauth){
					retVal+=List[i].InsPayAmt;
				}
			}
			return retVal;
		}

		///<summary>The amount paid on a claim only by total, not including by procedure.</summary>
		public static double ClaimByTotalOnly(int claimNum){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].ClaimNum==claimNum
					&& List[i].ProcNum==0
					&& List[i].Status!=ClaimProcStatus.Preauth){
					retVal+=List[i].InsPayAmt;
				}
			}
			return retVal;
		}

		///<summary></summary>
		public static void DetachAllFromCheck(int claimPaymentNum){
			cmd.CommandText = "UPDATE claimproc SET "
				+"ClaimPaymentNum = '0' "
				+"WHERE claimpaymentNum = '"+claimPaymentNum+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void SetForClaim(int claimNum,int claimPaymentNum,bool setAttached){
			cmd.CommandText = "UPDATE claimproc SET ClaimPaymentNum = ";
			if(setAttached){
				cmd.CommandText+="'"+claimPaymentNum+"' ";
			}
			else{
				cmd.CommandText+="'0' ";
			}
			cmd.CommandText+="WHERE claimnum = '"+claimNum+"' && "
				+"inspayamt > 0 && ("
				+"claimpaymentNum = '"+claimPaymentNum+"' || claimpaymentNum = '0')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static double ComputeBal(){//must make sure Refresh is done first
			double retVal=0;
			//double pat;
			for(int i=0;i<List.Length;i++){
				if(List[i].Status==ClaimProcStatus.Adjustment//ins adjustments do not affect patient balance
					|| List[i].Status==ClaimProcStatus.Preauth//preauthorizations do not affect patient balance
					|| List[i].Status==ClaimProcStatus.Capitation//cap procs do not affect patient balance
					){
					continue;
				}
				if(List[i].Status==ClaimProcStatus.Received
					|| List[i].Status==ClaimProcStatus.Supplemental){//because supplemental are always received
					retVal-=List[i].InsPayAmt;
				}
				else{//not recieved
					retVal-=List[i].InsPayEst;
				}
				retVal-=List[i].WriteOff;
			}
			return retVal;
		}


	}

	


}









