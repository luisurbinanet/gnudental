using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace OpenDental{

	///<summary></summary>
	public class Procedures{
		
		///<summary>Gets all procedures for a single patient.</summary>
		public static Procedure[] Refresh(int patNum){
			string command=
				"SELECT * from procedurelog "
				+"WHERE PatNum = '"+patNum.ToString()+"' "
				+"ORDER BY ProcDate,ADACode";
			return RefreshAndFill(command);
		}

		///<summary>Gets one procedure directly from the db.</summary>
		public static Procedure GetOneProc(int procNum){
			string command=
				"SELECT * from procedurelog "
				+"WHERE ProcNum="+procNum.ToString();
			Procedure[] List=RefreshAndFill(command);
			if(List.Length==0){
				MessageBox.Show(Lan.g("Procedures","Error. Procedure not found")+": "+procNum.ToString());
				return new Procedure();
			}
			return List[0];
		}

		private static Procedure[] RefreshAndFill(string command){
 			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			Procedure[] List=new Procedure[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
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
				List[i].Prosthesis		  = PIn.PString(table.Rows[i][22].ToString());
				List[i].DateOriginalProsth= PIn.PDate(table.Rows[i][23].ToString());
				List[i].ClaimNote		    = PIn.PString(table.Rows[i][24].ToString());
				List[i].DateLocked	    = PIn.PDate  (table.Rows[i][25].ToString());
				List[i].DateEntryC      = PIn.PDate  (table.Rows[i][26].ToString());
				List[i].ClinicNum       = PIn.PInt   (table.Rows[i][27].ToString());
				List[i].MedicalCode     = PIn.PString(table.Rows[i][28].ToString());
				List[i].DiagnosticCode  = PIn.PString(table.Rows[i][29].ToString());
				List[i].IsPrincDiag     = PIn.PBool  (table.Rows[i][30].ToString());
				List[i].LabFee          = PIn.PDouble(table.Rows[i][31].ToString());
			}
			return List;
		}

		///<summary>Returns a ProcDesc(AptNum,ProcLines,Production) struct for a single appointment directly from the database</summary>
		public static Procedure[] GetProcsForSingle(int aptNum, bool isNext){
			string command;
			if(isNext){
				command = "SELECT * from procedurelog WHERE NextAptNum = '"+POut.PInt(aptNum)+"'";
			}
			else{
				command = "SELECT * from procedurelog WHERE AptNum = '"+POut.PInt(aptNum)+"'";
			}
			return RefreshAndFill(command);
		}

		/// <summary>Used by GetProcsForSingle and GetProcsMultApts to generate a short string description of a procedure.</summary>
		public static string ConvertProcToString(string aDACode,string surf,string toothNum){
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

		///<summary>Gets a list (procsMultApts is a struct of type ProcDesc(aptNum, string[], and production) of all the procedures attached to the specified appointments.  Then, use GetProcsOneApt to pull procedures for one appointment from this list.  This process requires only one call to the database. "myAptNums" is the list of appointments to get procedures for.</summary>
		public static Procedure[] GetProcsMultApts(int[] myAptNums){
			return GetProcsMultApts(myAptNums,false);
		}

		///<summary>Gets a list (procsMultApts is a struct of type ProcDesc(aptNum, string[], and production) of all the procedures attached to the specified appointments.  Then, use GetProcsOneApt to pull procedures for one appointment from this list or GetProductionOneApt.  This process requires only one call to the database.  "myAptNums" is the list of appointments to get procedures for.  isForNext gets procedures for a list of next appointments rather than regular appointments.</summary>
		public static Procedure[] GetProcsMultApts(int[] myAptNums,bool isForNext){
			if(myAptNums.Length==0){
				return new Procedure[0];
			}
			string strAptNums="";
			for(int i=0;i<myAptNums.Length;i++){
				if(i>0){
					strAptNums+=" OR";
				}
				if(isForNext){
					strAptNums+=" NextAptNum='"+POut.PInt(myAptNums[i])+"'";
				}
				else{
					strAptNums+=" AptNum='"+POut.PInt(myAptNums[i])+"'";
				}
			}
			string command = "SELECT * FROM procedurelog WHERE"+strAptNums;
			return RefreshAndFill(command);
		}

		///<summary>Used do display procedure descriptions on appointments. The returned string also includes surf and toothNum.</summary>
		public static string GetDescription(Procedure proc){
			return ConvertProcToString(proc.ADACode,proc.Surf,proc.ToothNum);
		}

		///<summary>Gets procedures for one appointment by looping through the procsMultApts which was filled previously from GetProcsMultApts.</summary>
		public static Procedure[] GetProcsOneApt(int myAptNum,Procedure[] procsMultApts){
			ArrayList AL=new ArrayList();
			for(int i=0;i<procsMultApts.Length;i++){
				if(procsMultApts[i].AptNum==myAptNum){
					AL.Add(procsMultApts[i].Copy());
				}
			}
			Procedure[] retVal=new Procedure[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		///<summary>Gets the production for one appointment by looping through the procsMultApts which was filled previously from GetProcsMultApts.</summary>
		public static double GetProductionOneApt(int myAptNum,Procedure[] procsMultApts){
			double retVal=0;
			for(int i=0;i<procsMultApts.Length;i++){
				if(procsMultApts[i].AptNum==myAptNum){
					retVal+=procsMultApts[i].ProcFee;
				}
			}
			return retVal;
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
		public static void UnattachProcsInPlannedAppt(int myAptNum){
			string command="UPDATE procedurelog SET "
				+"NextAptNum = '0' "
				+"WHERE NextAptNum = '"+myAptNum+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary>Loops through each proc. Does not add notes to a procedure that already has notes. Used twice, security checked in both places before calling this.</summary>
		public static void SetCompleteInAppt(Appointment apt,InsPlan[] PlanList,PatPlan[] patPlans){
			Procedure[] ProcList=Procedures.Refresh(apt.PatNum);
			ClaimProc[] ClaimProcList=ClaimProcs.Refresh(apt.PatNum);
			Benefit[] benefitList=Benefits.Refresh(patPlans);
			//CovPats.Refresh(PlanList,patPlans);
			//bool doResetRecallStatus=false;
			ProcedureCode procCode;
			Procedure oldProc;
			for(int i=0;i<ProcList.Length;i++){
				if(ProcList[i].AptNum!=apt.AptNum){
					continue;
				}
				oldProc=ProcList[i].Copy();
				procCode=ProcedureCodes.GetProcCode(ProcList[i].ADACode);
				if(procCode.PaintType==ToothPaintingType.Extraction){//if an extraction, then mark previous procs hidden
					ProcList[i].SetHideGraphical();//might not matter anymore
					ToothInitials.SetValue(apt.PatNum,ProcList[i].ToothNum,ToothInitialType.Missing);
				}
				ProcList[i].ProcStatus=ProcStat.C;
				ProcList[i].ProcDate=apt.AptDateTime.Date;
				if(oldProc.ProcStatus!=ProcStat.C){
					ProcList[i].DateEntryC=DateTime.Now;//this triggers it to set to server time NOW().
				}
				ProcList[i].PlaceService=(PlaceOfService)Prefs.GetInt("DefaultProcedurePlaceService");
				//if procedure was already complete, then don't add more notes.
				if(oldProc.ProcStatus!=ProcStat.C){
					ProcList[i].ProcNote+=procCode.DefaultNote;
				}
				ProcList[i].ClinicNum=apt.ClinicNum;
				ProcList[i].PlaceService=Clinics.GetPlaceService(apt.ClinicNum);
				if(apt.ProvHyg!=0){//if the appointment has a hygiene provider
					if(procCode.IsHygiene){//hyg proc
						ProcList[i].ProvNum=apt.ProvHyg;
					}
					else{//regular proc
						ProcList[i].ProvNum=apt.ProvNum;
					}
				}
				else{//same provider for every procedure
					ProcList[i].ProvNum=apt.ProvNum;
				}
				ProcList[i].Update(oldProc);
				ProcList[i].ComputeEstimates(apt.PatNum,ClaimProcList,false,PlanList,patPlans,benefitList);
			}
			//if(doResetRecallStatus){
			//	Recalls.Reset(apt.PatNum);//this also synchs recall
			//}
			Recalls.Synch(apt.PatNum);
		}

		///<summary>Does not make any calls to db.</summary>
		public static double ComputeBal(Procedure[] List){
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
		public static void SetDateFirstVisit(DateTime visitDate, int situation,Patient pat){
			if(situation==1){
				if(pat.DateFirstVisit.Year>1880){
					return;//a date has already been set.
				}
			}	
			string command="SELECT Count(*) from procedurelog WHERE "
				+"PatNum = '"+POut.PInt(pat.PatNum)+"' "
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
				command="UPDATE patient SET DateFirstVisit ='0001-01-01'"
					+" WHERE PatNum ='"
					+POut.PInt(pat.PatNum)+"'";
			}
			else{
				command="UPDATE patient SET DateFirstVisit ='"
					+POut.PDate(visitDate)+"' WHERE PatNum ='"
					+POut.PInt(pat.PatNum)+"'";
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

		///<summary>Used in FormClaimProc to get the fee for a procedure directly from the db.  Do not use this if accessing FormClaimProc from the ProcEdit window, because proc might not be updated to db yet.</summary>
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
		public static void ComputeEstimatesForAll(int patNum,ClaimProc[] claimProcs,Procedure[] procs,InsPlan[] PlanList,PatPlan[] patPlans,Benefit[] benefitList)
		{
			for(int i=0;i<procs.Length;i++){
				procs[i].ComputeEstimates(patNum,claimProcs,false,PlanList,patPlans,benefitList);
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
			//first, check for missing teeth
			string command="SELECT COUNT(*) FROM toothinitial "
				+"WHERE ToothNum='"+toothNum+"' "
				+"AND PatNum="+POut.PInt(patNum)
				+" AND InitialType=0";//missing
			DataConnection dcon=new DataConnection();
			if(dcon.GetCount(command)!="0"){
				return true;
			}
			//then, check for a planned extraction
			command="SELECT COUNT(*) FROM procedurelog,procedurecode "
				+"WHERE procedurelog.ADACode=procedurecode.ADACode "
				+"AND procedurelog.ToothNum='"+toothNum+"' "
				+"AND procedurelog.PatNum="+patNum.ToString()
				+" AND procedurecode.PaintType=1";//extraction
			if(dcon.GetCount(command)!="0") {
				return true;
			}
			return false;
		}

		///<summary>Used from TP to get a list of all TP procs, ordered by priority, toothnum.</summary>
		public static Procedure[] GetListTP(Procedure[] procList){
			ArrayList AL=new ArrayList();
			int iPriority;
			int oPriority;
			int iToothInt;
			int oToothInt;
			for(int i=0;i<procList.Length;i++){
				if(procList[i].ProcStatus!=ProcStat.TP){
					continue;
				}
				if(AL.Count==0)//first procedure simple add
					AL.Add(procList[i]);
				else{//after that, figure out where to place the procedure to order things properly
					iPriority=Defs.GetOrder(DefCat.TxPriorities,procList[i].Priority);
					if(Tooth.IsValidDB(procList[i].ToothNum))
						iToothInt=Tooth.ToInt(procList[i].ToothNum);
					else
						iToothInt=0;
					for(int o=0;o<AL.Count;o++){
						oPriority=Defs.GetOrder(DefCat.TxPriorities,((Procedure)AL[o]).Priority);
						if(Tooth.IsValidDB(((Procedure)AL[o]).ToothNum))
							oToothInt=Tooth.ToInt(((Procedure)AL[o]).ToothNum);
						else
							oToothInt=0;
						if(iPriority==oPriority){
							if(iToothInt < oToothInt){
								AL.Insert(o,procList[i]);
								break;
							}
						}
						if(iPriority < oPriority){
							AL.Insert(o,procList[i]);
							break;
						}
						if(o==AL.Count-1){
							AL.Add(procList[i]);
							break;
						}
					}//for o
				}//else
			}//for i
			Procedure[] retVal=new Procedure[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		


	}

	

	


}










