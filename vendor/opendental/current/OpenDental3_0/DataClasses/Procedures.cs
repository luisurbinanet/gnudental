using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary></summary>
	public class Procedures{
		//<summary>The current procedure. Always taken from the List.</summary>
		//private static Procedure cur;
		//<summary>When doing update, this Patient is the original before any changes were made. This allows only the changed fields to be updated, minimizing concurrency issues.</summary>
		//public static Procedure CurOld;
		//<summary>all procedures for current patient</summary>
		//public static Procedure[] List;
		//<summary>Hashtable of all procedures for current patient. Key=ProcNum.</summary>
		//public static Hashtable HList;
		///<summary>Strings. Valid "1"-"32", and "A"-"Z". Moved to a function instead.</summary>
		public static ArrayList MissingTeeth;
		private static ProcDesc[] procsMultApts;
		///<summary>Descriptions of procedures for one appointment or one next appointment. Fill by using GetProcsMultApts, then GetProcsOneApt to pull data from that list.</summary>
		public static string[] ProcsOneApt;
		//<summary>Descriptions of procedures for one appointment or one next appointment. Fill using GetProcsForSingle to get data directly from the database.</summary>
		//public static ProcDesc ProcsForSingle;//string[] ProcsForSingle;//
		//private static ProcCodes ProcCodes;

		//<summary>Current procedure</summary>
		/*public static Procedure Cur{
			get{
				return cur;
			}
			set{
				cur=value;
				//curOld=value;
			}
		}*/

		///<summary></summary>
		public static Procedure[] Refresh(int patNum){
			string command=
				"SELECT * from procedurelog "
				+"WHERE PatNum = '"+patNum.ToString()+"' "
				+"ORDER BY ProcDate";
 			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			MissingTeeth=new ArrayList();
			//HList=new Hashtable();
			Procedure[] List=new Procedure[table.Rows.Count];
			for (int i=0;i<List.Length;i++){
				List[i]=new Procedure();
				List[i].ProcNum					= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum					= PIn.PInt   (table.Rows[i][1].ToString());
				List[i].AptNum					= PIn.PInt   (table.Rows[i][2].ToString());
				List[i].ADACode					= PIn.PString(table.Rows[i][3].ToString());
				List[i].ProcDate				= PIn.PDate  (table.Rows[i][4].ToString());
				List[i].ProcFee					= PIn.PDouble(table.Rows[i][5].ToString());
				//List[i].OverridePri			= PIn.PDouble(table.Rows[i][6].ToString());
				//List[i].OverrideSec			= PIn.PDouble(table.Rows[i][7].ToString());
				List[i].Surf						= PIn.PString(table.Rows[i][8].ToString());
				List[i].ToothNum				= PIn.PString(table.Rows[i][9].ToString());
				List[i].ToothRange			= PIn.PString(table.Rows[i][10].ToString());
				//List[i].NoBillIns				= PIn.PBool  (table.Rows[i][11].ToString());
				List[i].Priority				= PIn.PInt   (table.Rows[i][12].ToString());
				List[i].ProcStatus			= (ProcStat)PIn.PInt   (table.Rows[i][13].ToString());
				List[i].ProcNote				= PIn.PString(table.Rows[i][14].ToString());
				List[i].ProvNum					= PIn.PInt   (table.Rows[i][15].ToString());
				List[i].Dx							= PIn.PInt   (table.Rows[i][16].ToString());
				List[i].NextAptNum			= PIn.PInt   (table.Rows[i][17].ToString());
				//List[i].IsCovIns				= PIn.PBool  (table.Rows[i][18].ToString());
				//List[i].CapCoPay  			= PIn.PDouble(table.Rows[i][19].ToString());
				List[i].PlaceService		= (PlaceOfService)PIn.PInt(table.Rows[i][20].ToString());
				List[i].HideGraphical		= PIn.PBool  (table.Rows[i][21].ToString());
				//HList.Add(List[i].ProcNum,List[i]);    
				if(ProcedureCodes.GetProcCode(List[i].ADACode).RemoveTooth && (
					List[i].ProcStatus==ProcStat.C
					|| List[i].ProcStatus==ProcStat.EC
					|| List[i].ProcStatus==ProcStat.EO))
				{
					MissingTeeth.Add(List[i].ToothNum);
				}  
			}
			return List;
		}

		/*This will be used later to get rid of the global variable for missing teeth
		///<summary>Gets a list of missing teeth as strings. Valid "1"-"32", and "A"-"Z".</summary>
		public static ArrayList GetMissingTeeth(Procedure[] procs){
			ArrayList missing=new ArrayList();
			for(int i=0;i<procs.Length;i++){
				if(ProcedureCodes.GetProcCode(procs[i].ADACode).RemoveTooth && (
					procs[i].ProcStatus==ProcStat.C
					|| procs[i].ProcStatus==ProcStat.EC
					|| procs[i].ProcStatus==ProcStat.EO))
				{
					missing.Add(procs[i].ToothNum);
				}  
			}
			return missing;
		}*/

		//public static void RefreshByDate(){
		//	RefreshAndFill();
		//}

		//public static void RefreshByPriority(){
		//	cmd.CommandText =
		//		"SELECT * from procedurelog "
		//		+"WHERE PatNum = '"+POut.PInt(Patients.cur.PatNum)+"' "
		//		+"ORDER BY Priority";
		//	RefreshAndFill();
		//}

		///<summary>Returns a ProcDesc(AptNum,ProcLines,Production) struct for a single appointment.</summary>
		public static ProcDesc GetProcsForSingle(int aptNum, bool isNext){
			string command;
			if(isNext){
				command = "SELECT * from procedurelog WHERE nextaptnum = '"+POut.PInt(aptNum)+"'";
			}
			else{
				command = "SELECT * from procedurelog WHERE aptnum = '"+POut.PInt(aptNum)+"'";
			}
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			ProcDesc procsForSingle=new ProcDesc();
			procsForSingle.AptNum=aptNum;
			procsForSingle.ProcLines=new string[table.Rows.Count];
			string pADACode;
			string pSurf;
			string pToothNum;
			for(int j=0;j<table.Rows.Count;j++){
				pADACode = PIn.PString(table.Rows[j][3].ToString());
				pSurf    = PIn.PString(table.Rows[j][8].ToString());
				pToothNum= PIn.PString(table.Rows[j][9].ToString());
				procsForSingle.ProcLines[j]=ConvertProcToString(pADACode,pSurf,pToothNum);
				procsForSingle.Production+=PIn.PDouble(table.Rows[j][5].ToString());
			}
			return procsForSingle;
		}

		/// <summary>Used by GetProcsForSingle and GetProcsMultApts to generate a short string description of a procedure.</summary>
		private static string ConvertProcToString(string aDACode,string surf,string toothNum){
			string strLine="";
			switch (ProcedureCodes.GetProcCode(aDACode).TreatArea){
				case TreatmentArea.Surf :
					strLine+="#"+Tooth.ToInternat(toothNum)+"-"+surf+"-";//""#12-MOD-"
					break;
				case TreatmentArea.Tooth :
					strLine+="#"+Tooth.ToInternat(toothNum)+"-";//"#12-"
					break;
				default ://area 3 or 0 (mouth)
					break;
				case TreatmentArea.Quad :
					strLine+=surf+"-";//"UL-"
					break;
				case TreatmentArea.Sextant :
					strLine+="S"+surf+"-";//"S2-"
					break;
				case TreatmentArea.Arch :
					strLine+=surf+"-";//"U-"
					break;
				case TreatmentArea.ToothRange :
					//strLine+=table.Rows[j][13].ToString()+" ";//don't show range
					break;
			}//end switch
			strLine+=ProcedureCodes.GetProcCode(aDACode).AbbrDesc;
			return strLine;
		}

		///<summary>Gets a list (procsMultApts is a struct of type ProcDesc(aptNum, string[], and production) of all the procedures attached to the specified appointments.  Then, use GetProcsOneApt to pull procedures for one appointment from this list.  This process requires only one call to the database.</summary>
		/// <param name="myAptNums">The list of appointments to get procedures for.</param>
		public static void GetProcsMultApts(int[] myAptNums){
			GetProcsMultApts(myAptNums,false);
		}

		///<summary>Gets a list (procsMultApts is a struct of type ProcDesc(aptNum, string[], and production) of all the procedures attached to the specified appointments.  Then, use GetProcsOneApt to pull procedures for one appointment from this list or GetProductionOneApt.  This process requires only one call to the database.</summary>
		/// <param name="myAptNums">The list of appointments to get procedures for.</param>
		/// <param name="isForNext">Gets procedures for a list of next appointments rather than regular appointments.</param>
		public static void GetProcsMultApts(int[] myAptNums,bool isForNext){
			DataTable table=new DataTable();
			//if (myAptNums.Length==0)
			Procedure tempProcedure = new Procedure();
			string strAptNums="";
			if(myAptNums.Length>0){
				if(isForNext){
					strAptNums="NextAptNum='"+myAptNums[0].ToString()+"'";
					for (int i=1;i<myAptNums.Length;i++){
						strAptNums+=" || NextAptNum='"+myAptNums[i].ToString()+"'";
					}
				}
				else{
					strAptNums="AptNum='"+myAptNums[0].ToString()+"'";
					for (int i=1;i<myAptNums.Length;i++){
						strAptNums+=" || AptNum='"+myAptNums[i].ToString()+"'";
					}
				}
				//MessageBox.Show(strAptNums);
				string command = "SELECT * from procedurelog WHERE "+strAptNums;
				DataConnection dcon=new DataConnection();
 				table=dcon.GetTable(command);
			}//end if >0
			//else
			//	table=new DataTable();
			//int count3 = table.Rows.Count;
			//already defined: ProcDesc[] procsEntireDay
			//MessageBox.Show(count3.ToString());
			procsMultApts=new ProcDesc[myAptNums.Length];
			int procCount;
			for(int i=0;i<myAptNums.Length;i++){
				procsMultApts[i].AptNum=myAptNums[i];
				procsMultApts[i].Production=0;
				procCount=0;
				for(int j=0;j<table.Rows.Count;j++){
					if(isForNext){
						if(PIn.PInt(table.Rows[j][17].ToString())==myAptNums[i]){
							procCount++;
						}
					}
					else{//regular appt
						if(PIn.PInt(table.Rows[j][2].ToString())==myAptNums[i]){
							procCount++;
						}
					}
				}
				procsMultApts[i].ProcLines=new string[procCount];
				procCount=0;
				string pADACode="";
				string pSurf="";
				string pToothNum="";
				for(int j=0;j<table.Rows.Count;j++){
					pADACode = PIn.PString(table.Rows[j][3].ToString());
					pSurf    = PIn.PString(table.Rows[j][8].ToString());
					pToothNum= PIn.PString(table.Rows[j][9].ToString());
					if(isForNext){
						if(PIn.PInt(table.Rows[j][17].ToString())==myAptNums[i]){
							procsMultApts[i].Production+=PIn.PDouble(table.Rows[j][5].ToString());
							procsMultApts[i].ProcLines[procCount]=ConvertProcToString(pADACode,pSurf,pToothNum);
							procCount++;
						}
					}
					else{//regular appt
						if(PIn.PInt(table.Rows[j][2].ToString())==myAptNums[i]){
							procsMultApts[i].Production+=PIn.PDouble(table.Rows[j][5].ToString());
							procsMultApts[i].ProcLines[procCount]=ConvertProcToString(pADACode,pSurf,pToothNum);
							procCount++;
						}
					}
				}
			}//end for myAptNums
			//MessageBox.Show(procsEntireDay[0].AptNum.ToString()+procsEntireDay[1].AptNum.ToString());
		}

		///<summary>Gets procedures for one appointment by looping through the procsMultApts which was filled previously from GetProcsMultApts.</summary>
		public static void GetProcsOneApt(int myAptNum){
			for(int i=0;i<procsMultApts.Length;i++){
				if (procsMultApts[i].AptNum==myAptNum){
					//MessageBox.Show(myAptNum.ToString());
					ProcsOneApt=procsMultApts[i].ProcLines;
				}
			}
		}

		///<summary>Gets the production for one appointment by looping through the procsMultApts which was filled previously from GetProcsMultApts.</summary>
		public static double GetProductionOneApt(int myAptNum){
			for(int i=0;i<procsMultApts.Length;i++){
				if(procsMultApts[i].AptNum==myAptNum){
					//MessageBox.Show(myAptNum.ToString());
					return procsMultApts[i].Production;
				}
			}
			return 0;
		}

		///<summary>Used in FormClaimEdit,FormClaimPrint,FormClaimPayTotal, etc to get description of procedure. Procedure list needs to include the procedure we are looking for.</summary>
		public static Procedure GetProc(Procedure[] list, int procNum){
			for(int i=0;i<list.Length;i++){
				if(procNum==list[i].ProcNum){
					return list[i];
				}
			}
			MessageBox.Show("Error. Procedure not found");
			return new Procedure();
		}

		///<summary></summary>
		public static void UnattachProcsInAppt(int myAptNum){
			string command="UPDATE procedurelog SET "
				+"AptNum = '0' "
				+"WHERE AptNum = '"+myAptNum+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public static void UnattachProcsInNextAppt(int myAptNum){
			string command="UPDATE procedurelog SET "
				+"NextAptNum = '0' "
				+"WHERE NextAptNum = '"+myAptNum+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary>Loops through each proc.  Assumes you have set Appointments.Cur</summary>
		public static void SetCompleteInAppt(){
			Procedure[] ProcList=Procedures.Refresh(Patients.Cur.PatNum);
			ClaimProc[] ClaimProcList=ClaimProcs.Refresh(Patients.Cur.PatNum);
			CovPats.Refresh();
			bool doResetRecallStatus=false;
			ProcedureCode procCode;
			Procedure oldProc;
			for(int i=0;i<ProcList.Length;i++){
				if(ProcList[i].AptNum!=Appointments.Cur.AptNum){
					continue;
				}
				oldProc=ProcList[i].Copy();
				procCode=ProcedureCodes.GetProcCode(ProcList[i].ADACode);
				if(procCode.RemoveTooth){//if an extraction, then mark previous procs hidden
					ProcList[i].SetHideGraphical();
				}
				//if is a recall proc
				if(procCode.SetRecall){
					doResetRecallStatus=true;
				}
				ProcList[i].ProcStatus=ProcStat.C;
				ProcList[i].ProcDate=Appointments.Cur.AptDateTime.Date;
				ProcList[i].PlaceService=(PlaceOfService)Prefs.GetInt("DefaultProcedurePlaceService");
				ProcList[i].ProcNote+=procCode.DefaultNote;
				if(Appointments.Cur.ProvHyg!=0){//if the appointment has a hygiene provider
					if(procCode.IsHygiene){//hyg proc
						ProcList[i].ProvNum=Appointments.Cur.ProvHyg;
					}
					else{//regular proc
						ProcList[i].ProvNum=Appointments.Cur.ProvNum;
					}
				}
				else{//same provider for every procedure
					ProcList[i].ProvNum=Appointments.Cur.ProvNum;
				}
				ProcList[i].Update(oldProc);
				ProcList[i].ComputeEstimates(Patients.Cur.PatNum,Patients.Cur.PriPlanNum
					,Patients.Cur.SecPlanNum,ClaimProcList,false);
			}
			if(doResetRecallStatus){
				Patient PatCur=Patients.Cur;
				PatCur.RecallStatus=0;
				Patients.Cur=PatCur;
				Patients.UpdateCur();
			}
		}

		///<summary></summary>
		public static double ComputeBal(Procedure[] List){//must make sure Refresh is done first
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcStatus==ProcStat.C){//complete
					//cur=List[i];
					//note: capitation estimates are now part of claimproc.WriteOff
					//if(cur.CapCoPay==-1)//not capitation
					retVal+=List[i].ProcFee;
					//else//capitation
					//	retVal+=cur.CapCoPay;
				}
			}
			return retVal;
		}

		///<summary>Sets the patient.DateFirstVisit if necessary. A visitDate is required to be passed in because it may not be today's date. This is triggered by:
		///1. When any procedure is inserted regardless of status. From Chart or appointment. If no C procs and date blank, changes date.
		///2. When updating a procedure to status C. If no C procs, update visit date. Ask user first?
		///3. When an appointment is deleted. If no C procs, clear visit date.
		///4. Changing an appt date of type IsNewPatient. If no C procs, change visit date.
		///Old: when setting a procedure complete in the Chart module or the ProcEdit window.  Also when saving an appointment that is marked IsNewPat.</summary>
		public static void SetDateFirstVisit(DateTime visitDate, int situation){
			if(situation==1){
				if(Patients.Cur.DateFirstVisit.Year>1880){
					return;//a date has already been set.
				}
			}	
			string command="SELECT Count(*) from procedurelog WHERE "
				+"PatNum = '"+POut.PInt(Patients.Cur.PatNum)+"' "
				+"&& ProcStatus = '2'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			if(PIn.PInt(table.Rows[0][0].ToString())>0){
				return;//there are already completed procs (for all situations)
			}
			if(situation==2){
				//ask user first?
			}
			if(situation==3){
				command="UPDATE patient SET DateFirstVisit =''"
					+" WHERE PatNum ='"
					+POut.PInt(Patients.Cur.PatNum)+"'";
			}
			else{
				command="UPDATE patient SET DateFirstVisit ='"
					+POut.PDate(visitDate)+"' WHERE PatNum ='"
					+POut.PInt(Patients.Cur.PatNum)+"'";
			}
			//MessageBox.Show(cmd.CommandText);
			dcon.NonQ(command);
			
		}

		///<summary>Used in FormClaimProc to get the ADAcode for a procedure. Do not use this if accessing FormClaimProc from the ProcEdit window, because proc might not be updated to db yet.</summary>
		public static string GetADA(int procNum){
			string command="SELECT ADACode FROM procedurelog WHERE ProcNum='"+procNum.ToString()+"'";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count==0){
				return "";
			}
			return PIn.PString(table.Rows[0][0].ToString());
		}

		///<summary>Used in FormClaimProc to get the fee for a procedure.  Do not use this if accessing FormClaimProc from the ProcEdit window, because proc might not be updated to db yet.</summary>
		public static double GetProcFee(int procNum){
			string command="SELECT ProcFee FROM procedurelog WHERE ProcNum='"+procNum.ToString()+"'";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count==0){
				return 0;
			}
			return PIn.PDouble(table.Rows[0][0].ToString());
		}

		///<summary>After changing important coverage plan info, this is called to recompute estimates for all procedures for this patient.</summary>
		public static void ComputeEstimatesForAll(int patNum,int priPlanNum,int secPlanNum
			,ClaimProc[] claimProcs,Procedure[] procs)
		{
			for(int i=0;i<procs.Length;i++){
				procs[i].ComputeEstimates(patNum,priPlanNum
					,secPlanNum,claimProcs,false);
			}
		}

		///<summary>Called from FormApptsOther when creating a new appointment.  Returns true if there are any procedures marked complete for this patient.  The result is that the NewPt box on the appointment won't be checked.</summary>
		public static bool AreAnyComplete(int patNum){
			string command="SELECT COUNT(*) FROM procedurelog "
				+"WHERE PatNum="+patNum.ToString()
				+" AND ProcStatus=2";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(PIn.PInt(table.Rows[0][0].ToString())==0){
				return false;
			}
			else return true;
		}

		///<summary>Called from AutoCodeItems.  Makes a call to the database to determine whether the specified tooth has been extracted or will be extracted. This could then trigger a pontic code.</summary>
		public static bool WillBeMissing(string toothNum,int patNum){
			string command="SELECT COUNT(*) FROM procedurelog,procedurecode "
				+"WHERE procedurelog.ADACode=procedurecode.ADACode "
				+"AND procedurelog.ToothNum='"+toothNum+"' "
				+"AND procedurelog.PatNum="+patNum.ToString()
				+" AND procedurecode.RemoveTooth=1";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(PIn.PInt(table.Rows[0][0].ToString())==0){
				return false;
			}
			else return true;
			/*for(int i=0;i<procList.Length;i++){
				if(procList[i].ToothNum==toothNum
					&& ProcedureCodes.GetProcCode(procList[i].ADACode).RemoveTooth)
				{
					return true;
				}
			}
			return false;*/
		}



	}

	
	
	///<summary>Not a database table.</summary>
	public struct ProcDesc{
		///<summary></summary>
		public int AptNum;
		///<summary></summary>
		public string[] ProcLines;
		///<summary></summary>
		public double Production;
	}

	


}










