using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the procedurelog table in the database.</summary>
	public struct Procedure{
		///<summary>Primary key.</summary>
		public int ProcNum;//
		///<summary>Foreign key to patient.PatNum</summary>
		public int PatNum;
		///<summary>Foreign key to appointment.AptNum.  Only allowed to attach proc to one appt(not counting next apt)</summary>
		public int AptNum;
		///<summary>Foreign key to procedureCode.ADACode</summary>
		public string ADACode;
		///<summary>Procedure date.</summary>
		public DateTime ProcDate;
		///<summary>Procedure fee.</summary>
		public double ProcFee;
		///<summary>Override primary insurance estimate.  -1 for false.</summary>
		public double OverridePri;
		///<summary>Override secondary insurance estimate. -1 for false.</summary>
		public double OverrideSec;
		///<summary>Surfaces, or use "UL" etc for quadrant, "2" etc for sextant, "U","L" for arches.</summary>
		public string Surf;
		///<summary>May be blank, otherwise 1-32 or A-T, 1 or 2 char.</summary>
		public string ToothNum;
		///<summary>May be blank, otherwise is series of toothnumbers separated by commas.</summary>
		public string ToothRange;
		///<summary>Set true to indicate "do not bill procedure to insurance".</summary>
		public bool NoBillIns;
		///<summary>Foreign key to definition.DefNum, which contains the text of the priority.</summary>
		public int Priority;
		///<summary>See the ProcStat enumeration.</summary>
		public ProcStat ProcStatus;
		///<summary>Procedure note.</summary>
		public string ProcNote;
		///<summary>Foreign key to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>Foreign key to definition.DefNum, which contains text of the Diagnosis.</summary>
		public int Dx;
		///<summary>Foreign key to appointment.AptNum.  Allows this procedure to be attached to a Next appointment as well as a standard appointment.</summary>
		public int NextAptNum;
		///<summary>Is covered by insurance.  Set to false if patient does not have ins coverage for this procedure.</summary>
		public bool IsCovIns;
		///<summary>Capitation Co-pay amount.  Will always be -1 if patient does not have capitation coverage for this procedure.</summary>
		public double CapCoPay;
		//public bool NoShowGraphical;//Graphical Tooth Chart addition in case tooth had drawings on it and then was extracted
	}

	/*=========================================================================================
	=================================== class Procedures ===========================================*/

	///<summary></summary>
	public class Procedures:DataClass{
		///<summary></summary>
		public static Procedure Cur;
		///<summary></summary>
		public static Procedure[] List;//all procedures for current patient
		///<summary></summary>
		public static Hashtable HList;//Hashtable of all procedures for current patient
		///<summary></summary>
		public static ArrayList MissingTeeth;//valid "1"-"32", and "A"-"Z"
		private static ProcDesc[] procsMultApts;
		///<summary>Descriptions of procedures for one appointment or one next appointment. Fill by using GetProcsMultApts, then GetProcsOneApt to pull data from that list.</summary>
		public static string[] ProcsOneApt;
		///<summary>Descriptions of procedures for one appointment or one next appointment. Fill using GetProcsForSingle to get data directly from the database.</summary>
		public static string[] ProcsForSingle;//
		//private static ProcCodes ProcCodes;

		///<summary></summary>
		public Procedures(){
			//ProcCodes=new ProcCodes();
		}

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from procedurelog "
				+"WHERE PatNum = '"+POut.PInt(Patients.Cur.PatNum)+"' "
				+"ORDER BY ProcDate";
			FillTable();
			MissingTeeth=new ArrayList();
			HList=new Hashtable();
			List=new Procedure[table.Rows.Count];
			for (int i=0;i<List.Length;i++){
				List[i].ProcNum					= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum					= PIn.PInt   (table.Rows[i][1].ToString());
				List[i].AptNum					= PIn.PInt   (table.Rows[i][2].ToString());
				List[i].ADACode					= PIn.PString(table.Rows[i][3].ToString());
				List[i].ProcDate				= PIn.PDate  (table.Rows[i][4].ToString());
				List[i].ProcFee					= PIn.PDouble(table.Rows[i][5].ToString());
				List[i].OverridePri			= PIn.PDouble(table.Rows[i][6].ToString());
				List[i].OverrideSec			= PIn.PDouble(table.Rows[i][7].ToString());
				List[i].Surf						= PIn.PString(table.Rows[i][8].ToString());
				List[i].ToothNum				= PIn.PString(table.Rows[i][9].ToString());
				List[i].ToothRange			= PIn.PString(table.Rows[i][10].ToString());
				List[i].NoBillIns				= PIn.PBool  (table.Rows[i][11].ToString());
				List[i].Priority				= PIn.PInt   (table.Rows[i][12].ToString());
				List[i].ProcStatus			= (ProcStat)PIn.PInt   (table.Rows[i][13].ToString());
				List[i].ProcNote				= PIn.PString(table.Rows[i][14].ToString());
				List[i].ProvNum					= PIn.PInt   (table.Rows[i][15].ToString());
				List[i].Dx							= PIn.PInt   (table.Rows[i][16].ToString());
				List[i].NextAptNum			= PIn.PInt   (table.Rows[i][17].ToString());
				List[i].IsCovIns				= PIn.PBool  (table.Rows[i][18].ToString());
				List[i].CapCoPay  			= PIn.PDouble(table.Rows[i][19].ToString());
				HList.Add(List[i].ProcNum,List[i]);    
				if(ProcedureCodes.GetProcCode(List[i].ADACode).RemoveTooth && (
					List[i].ProcStatus==ProcStat.C
					|| List[i].ProcStatus==ProcStat.EC
					|| List[i].ProcStatus==ProcStat.EO))
				{
					MissingTeeth.Add(Procedures.List[i].ToothNum);
				}  
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO procedurelog " 
				+"(PatNum, AptNum, ADACode, ProcDate, "
				+"ProcFee, "
				+"OverridePri, OverrideSec, Surf, "
				+"ToothNum, ToothRange, NoBillIns, Priority, "
				+"ProcStatus, ProcNote, ProvNum, "
				+"dx,nextaptnum,iscovins,capcopay) "
				+"VALUES ("
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PInt   (Cur.AptNum)+"', "
				+"'"+POut.PString(Cur.ADACode)+"', "
				+"'"+POut.PDate  (Cur.ProcDate)+"', "
				+"'"+POut.PDouble(Cur.ProcFee)+"', "
				+"'"+POut.PDouble(Cur.OverridePri)+"', "
				+"'"+POut.PDouble(Cur.OverrideSec)+"', "
				+"'"+POut.PString(Cur.Surf)+"', "
				+"'"+POut.PString(Cur.ToothNum)+"', "
				+"'"+POut.PString(Cur.ToothRange)+"', "
				+"'"+POut.PBool  (Cur.NoBillIns)+"', "
				+"'"+POut.PInt   (Cur.Priority)+"', "
				+"'"+POut.PInt   ((int)Cur.ProcStatus)+"', "
				+"'"+POut.PString(Cur.ProcNote)+"', "
				+"'"+POut.PInt   (Cur.ProvNum)+"', "
				+"'"+POut.PInt   (Cur.Dx)+"', "
				+"'"+POut.PInt   (Cur.NextAptNum)+"', "
				+"'"+POut.PBool  (Cur.IsCovIns)+"', "
				+"'"+POut.PDouble(Cur.CapCoPay)+"')";
				//+"'"+POut.PBool  (Cur.NoShowGraphical)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.ProcNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE procedurelog SET "
				+"PatNum = '"					 +POut.PInt   (Cur.PatNum)+"', "
				+"AptNum = '"					 +POut.PInt   (Cur.AptNum)+"', "
				+"ADACode = '"				 +POut.PString(Cur.ADACode)+"', "
				+"ProcDate = '"				 +POut.PDate  (Cur.ProcDate)+"', "
				+"ProcFee = '"				 +POut.PDouble(Cur.ProcFee)+"', "
				+"OverridePri = '"		 +POut.PDouble(Cur.OverridePri)+"', "
				+"OverrideSec = '"		 +POut.PDouble(Cur.OverrideSec)+"', "
				+"Surf = '"						 +POut.PString(Cur.Surf)+"', "
				+"ToothNum = '"				 +POut.PString(Cur.ToothNum)+"', "
				+"ToothRange = '"			 +POut.PString(Cur.ToothRange)+"', "
				+"NoBillIns = '"			 +POut.PBool  (Cur.NoBillIns)+"', "
				+"Priority = '"				 +POut.PInt   (Cur.Priority)+"', "
				+"ProcStatus = '"			 +POut.PInt   ((int)Cur.ProcStatus)+"', "
				+"ProcNote = '"				 +POut.PString(Cur.ProcNote)+"', "
				+"ProvNum = '"				 +POut.PInt   (Cur.ProvNum)+"', "
				+"Dx = '"							 +POut.PInt   (Cur.Dx)+"', "
				+"nextaptnum = '"			 +POut.PInt   (Cur.NextAptNum)+"', "
				+"iscovins = '"				 +POut.PBool  (Cur.IsCovIns)+"', "
				+"capcopay = '"				 +POut.PDouble(Cur.CapCoPay)+"' "
				//+"noshowgraphical = '" +POut.PBool  (Cur.NoShowGraphical)+"' "
				+"WHERE ProcNum = '"+POut.PInt (Cur.ProcNum)+"'";
			NonQ(false);
		}

		//public static void RefreshByDate(){
		//	RefreshAndFill();
		//}

		//public static void RefreshByPriority(){
		//	cmd.CommandText =
		//		"SELECT * from procedurelog "
		//		+"WHERE PatNum = '"+POut.PInt(Patients.Cur.PatNum)+"' "
		//		+"ORDER BY Priority";
		//	RefreshAndFill();
		//}

		

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from procedurelog WHERE procNum = '"+POut.PInt(Cur.ProcNum)+"'";
			NonQ(false);
		}

		///<summary>Gets a string[] (ProcsForSingle) of the procedures for a single appointment or Next appointment from the database.</summary>
		public static void GetProcsForSingle(int aptNum, bool isNext){
			if(isNext){
				cmd.CommandText = "SELECT * from procedurelog WHERE nextaptnum = '"+POut.PInt(aptNum)+"'";
			}
			else{
				cmd.CommandText = "SELECT * from procedurelog WHERE aptnum = '"+POut.PInt(aptNum)+"'";
			}
			FillTable();
			ProcsForSingle=new string[table.Rows.Count];	
			string pADACode;
			string pSurf;
			string pToothNum;
			for(int j=0;j<table.Rows.Count;j++){
				pADACode = PIn.PString(table.Rows[j][3].ToString());
				pSurf    = PIn.PString(table.Rows[j][8].ToString());
				pToothNum= PIn.PString(table.Rows[j][9].ToString());
				ProcsForSingle[j]=ConvertProcToString(pADACode,pSurf,pToothNum);
			}
		}

		/// <summary>Used by GetProcsForSingle and GetProcsMultApts to generate a short string description of a procedure.</summary>
		/// <param name="aDACode"></param>
		/// <param name="surf"></param>
		/// <param name="toothNum"></param>
		/// <returns></returns>
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

		///<summary>Gets a list (procsMultApts is a struct of type ProcDesc(aptNum and string[]) of all the procedures attached to the specified appointments.  Then, use GetProcsOneApt to pull procedures for one appointment from this list.  This process requires only one call to the database.</summary>
		/// <param name="myAptNums">The list of appointments to get procedures for.</param>
		public static void GetProcsMultApts(int[] myAptNums){
			GetProcsMultApts(myAptNums,false);
		}

		///<summary>Gets a list (procsMultApts is a struct of type ProcDesc(aptNum and string[]) of all the procedures attached to the specified appointments.  Then, use GetProcsOneApt to pull procedures for one appointment from this list.  This process requires only one call to the database.</summary>
		/// <param name="myAptNums">The list of appointments to get procedures for.</param>
		/// <param name="isForNext">Gets procedures for a list of next appointments rather than regular appointments.</param>
		public static void GetProcsMultApts(int[] myAptNums,bool isForNext){
			//if (myAptNums.Length==0)
			Procedure tempProcedure = new Procedure();
			string strAptNums="";
			if (myAptNums.Length>0){
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
				cmd.CommandText = "SELECT * from procedurelog WHERE "+strAptNums;
				FillTable();
			}//end if >0
			else
				table=new DataTable();
			//int count3 = table.Rows.Count;
			//already defined: ProcDesc[] procsEntireDay
			//MessageBox.Show(count3.ToString());
			procsMultApts=new ProcDesc[myAptNums.Length];
			int internalCount;
			for(int i=0;i<myAptNums.Length;i++){
				procsMultApts[i].AptNum=myAptNums[i];
				internalCount=0;
				for(int j=0;j<table.Rows.Count;j++){
					if(isForNext){
						if(PIn.PInt(table.Rows[j][17].ToString())==myAptNums[i]){
							internalCount+=1;
						}
					}
					else{//regular appt
						if(PIn.PInt(table.Rows[j][2].ToString())==myAptNums[i]){
							internalCount+=1;
						}
					}
				}
				procsMultApts[i].ProcLines=new string[internalCount];
				internalCount=0;
				string pADACode="";
				string pSurf="";
				string pToothNum="";
				for(int j=0;j<table.Rows.Count;j++){
					pADACode = PIn.PString(table.Rows[j][3].ToString());
					pSurf    = PIn.PString(table.Rows[j][8].ToString());
					pToothNum= PIn.PString(table.Rows[j][9].ToString());
					if(isForNext){
						if (PIn.PInt(table.Rows[j][17].ToString())==myAptNums[i]){
							procsMultApts[i].ProcLines[internalCount]=ConvertProcToString(pADACode,pSurf,pToothNum);
							internalCount+=1;
						}
					}
					else{//regular appt
						if (PIn.PInt(table.Rows[j][2].ToString())==myAptNums[i]){
							procsMultApts[i].ProcLines[internalCount]=ConvertProcToString(pADACode,pSurf,pToothNum);
							internalCount+=1;
						}
					}
				}
			}//end for myAptNums
			//MessageBox.Show(procsEntireDay[0].AptNum.ToString()+procsEntireDay[1].AptNum.ToString());
		}

		///<summary>Gets procedures for one appointment by looping through the procsMultApts which was filled previously from GetProcsMultApts.</summary>
		public static void GetProcsOneApt(int myAptNum){
			for (int i = 0; i < procsMultApts.Length; i += 1){
				if (procsMultApts[i].AptNum==myAptNum){
					//MessageBox.Show(myAptNum.ToString());
					ProcsOneApt=procsMultApts[i].ProcLines;
				}
			}
		}

		///<summary></summary>
		public static double GetEstForCur(PriSecTot pst){
			//does not take into consideration:
			//annual max or deductible
			if(Cur.NoBillIns){
				return 0;
			}
			if(!Cur.IsCovIns){
				return 0;
			}
			double priPercent=CovPats.GetPercent(Cur.ADACode,PriSecTot.Pri);
			double secPercent=CovPats.GetPercent(Cur.ADACode,PriSecTot.Sec);
			double priEst=Cur.ProcFee*priPercent;
			double secEst=Cur.ProcFee*secPercent;
			double priCopay=InsPlans.GetCopay(Cur.ADACode,Patients.Cur.PriPlanNum);//also gets InsPlan
			if(priCopay!=-1){//if a primary copay fee schedule exsists
				if(InsPlans.Cur.PlanType=="c"){//capitation
					;//no need to handle here.  It's a field in the procedure. 
				}
				else if(priCopay>0){//only use if not 0
					priEst=Cur.ProcFee-InsPlans.GetCopay(Cur.ADACode,Patients.Cur.PriPlanNum);
				}
			}
			double secCopay=InsPlans.GetCopay(Cur.ADACode,Patients.Cur.SecPlanNum);//also gets InsPlan
			if(secCopay!=-1){//if a secondary copay fee schedule exists.
				if(InsPlans.Cur.PlanType=="c"){//capitation
					;//no need to handle here.  It's a field in the procedure. 
				}
				else if(secCopay>0){//only use if not 0
					secEst=Cur.ProcFee-InsPlans.GetCopay(Cur.ADACode,Patients.Cur.SecPlanNum);
				}
			}
			if(Cur.OverridePri!=-1)
				priEst=Cur.OverridePri;
			if(Cur.OverrideSec!=-1)
				secEst=Cur.OverrideSec;
			if(Procedures.Cur.ProcFee-priEst < secEst)
				secEst=Procedures.Cur.ProcFee-priEst;
			switch(pst){
				case PriSecTot.Pri:
					return priEst;
				case PriSecTot.Sec:
					return secEst;
				case PriSecTot.Tot:
					return priEst+secEst;
			}
			return 0;
		}

		///<summary></summary>
		public static void UnattachProcsInAppt(int myAptNum){
			cmd.CommandText = "UPDATE procedurelog SET "
				+"AptNum = '0' "
				+"WHERE AptNum = '"+myAptNum+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void UnattachProcsInNextAppt(int myAptNum){
			cmd.CommandText = "UPDATE procedurelog SET "
				+"NextAptNum = '0' "
				+"WHERE NextAptNum = '"+myAptNum+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void SetCompleteInAppt(){//assumes you have set Appointments.Cur
			cmd.CommandText = "SELECT procnum,adacode,procnote FROM procedurelog "
				+"WHERE AptNum = '"+POut.PInt(Appointments.Cur.AptNum)+"'";
			FillTable();
			//int tempProcNum;
			bool doResetRecallStatus=false;
			for (int i=0;i<table.Rows.Count;i++){
				if(((ProcedureCode)ProcedureCodes.HList[table.Rows[i][1].ToString()]).SetRecall){//is a recall proc
					doResetRecallStatus=true;
				}
				cmd.CommandText = "UPDATE procedurelog SET "
					+"procstatus = '" +POut.PInt   ((int)ProcStat.C)+"', "
					+"Procdate = '"   +POut.PDate  (Appointments.Cur.AptDateTime.Date)+"', "
					+"procnote = '"		+POut.PString(table.Rows[i][2].ToString())//does not delete the existing note
					+POut.PString(((ProcedureCode)ProcedureCodes.HList[table.Rows[i][1].ToString()]).DefaultNote)+"'";
				if(Appointments.Cur.ProvHyg!=0){//if the appointment has a hygiene provider
					if(((ProcedureCode)ProcedureCodes.HList[table.Rows[i][1].ToString()]).IsHygiene){//hyg proc
						cmd.CommandText+=", provnum = '"+POut.PInt   (Appointments.Cur.ProvHyg)+"'";
					}
					else{//regular proc
						cmd.CommandText+=", provnum = '"+POut.PInt   (Appointments.Cur.ProvNum)+"'";
					}
				}
				else{//same provider for every procedure
					cmd.CommandText+=", provnum = '"+POut.PInt   (Appointments.Cur.ProvNum)+"'";
				}
				cmd.CommandText+=" WHERE ProcNum = '"+POut.PInt(PIn.PInt(table.Rows[i][0].ToString()))+"'";
				NonQ(false);
			}
			if(doResetRecallStatus){
				Patients.Cur.RecallStatus=0;
				Patients.UpdateCur();
			}
		}

		///<summary></summary>
		public static void PutBal(DateTime date, double amt){//not using anymore
			/*
			amt=(double)Math.Round(amt,2);
			Ledgers Ledgers2=new Ledgers();
			Ledgers2.Refresh(Patients.Cur.PatNum);
			DateTime monthYear;
			monthYear=new DateTime(date.Year,date.Month,1);//eg 3/1/03
			if(Ledgers.HList.ContainsKey(monthYear.Date)){
				Ledgers.Cur=(Ledger)Ledgers.HList[monthYear.Date];
				Ledgers.Cur.ProcFees+=amt;
				Ledgers2.UpdateCur();
			}
			else{
				Ledgers.Cur=new Ledger();
				Ledgers.Cur.PatNum=Patients.Cur.PatNum;
				Ledgers.Cur.MonthYear=monthYear;
				Ledgers.Cur.ProcFees=amt;
				Ledgers2.SaveCur();
			}*/
		}

		///<summary></summary>
		public static double ComputeBal(){//must make sure Refresh is done first
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcStatus==ProcStat.C//complete
					){
					Cur=List[i];
					if(Cur.CapCoPay==-1)//not capitation
						retVal+=Cur.ProcFee;
					else//capitation
						retVal+=Cur.CapCoPay;
				}
			}
			return retVal;
		}

		//public static void

	}

	
	
	///<summary>Not a database table.</summary>
	public struct ProcDesc{
		///<summary></summary>
		public int AptNum;
		///<summary></summary>
		public string[] ProcLines;
	}

	


}










