using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary></summary>
	public class Recalls{

		///<summary>Gets all recalls for the supplied patients, usually a family or single pat.  Result might have a length of zero.</summary>
		public static Recall[] GetList(int[] patNums){
			string wherePats="";
			for(int i=0;i<patNums.Length;i++){
				if(i!=0){
					wherePats+=" OR ";
				}
				wherePats+="PatNum="+patNums[i].ToString();
			}
			string command=
				"SELECT * from recall "
				+"WHERE "+wherePats;
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			Recall[] List=new Recall[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new Recall();
				List[i].RecallNum      = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum         = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].DateDueCalc    = PIn.PDate  (table.Rows[i][2].ToString());
				List[i].DateDue        = PIn.PDate  (table.Rows[i][3].ToString());
				List[i].DatePrevious   = PIn.PDate  (table.Rows[i][4].ToString());
				List[i].RecallInterval = new Interval(PIn.PInt(table.Rows[i][5].ToString()));
				List[i].RecallStatus   = PIn.PInt   (table.Rows[i][6].ToString());
				List[i].Note           = PIn.PString(table.Rows[i][7].ToString());
				List[i].IsDisabled     = PIn.PBool  (table.Rows[i][8].ToString());
			}
			return List;
		}

		/// <summary></summary>
		public static Recall[] GetList(Patient[] patients){
			int[] patNums=new int[patients.Length];
			for(int i=0;i<patients.Length;i++){
				patNums[i]=patients[i].PatNum;
			}
			return GetList(patNums);
		}

		///<summary>Only used in FormRecallList to get a list of patients with recall.  Supply a date range, using min(-1 day) and max values if user left blank.</summary>
		public static RecallItem[] GetRecallList(DateTime fromDate,DateTime toDate){
			string command=
				"SELECT recall.*,"
				+"CONCAT(patient.LName,', ',patient.FName,' ',patient.Preferred,' ',"
				+"patient.MiddleI) AS 'Patient Name', "
				+"patient.Birthdate "
				+"FROM recall,patient "
				+"WHERE recall.PatNum=patient.PatNum "
				+"AND recall.DateDue >= '"+POut.PDate(fromDate)+"' "
				+"AND recall.DateDue <= '"+POut.PDate(toDate)+"' "
				+"AND patient.patstatus=0 ";
				//ordering will be done down below
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			RecallItem[] RecallList = new RecallItem[table.Rows.Count];
			DateTime[] orderDate=new DateTime[table.Rows.Count];
			DateTime dateDue;
			DateTime dateDueOverride;
			for(int i=0;i<table.Rows.Count;i++){
				dateDue        =PIn.PDate  (table.Rows[i][2].ToString());
				dateDueOverride=PIn.PDate  (table.Rows[i][3].ToString());
				if(dateDueOverride.Year>1880){
					RecallList[i].DueDate=dateDueOverride;
				}
				else{
					RecallList[i].DueDate=dateDue;
				}
				RecallList[i].PatientName   = PIn.PString(table.Rows[i][9].ToString());
				RecallList[i].BirthDate     = PIn.PDate  (table.Rows[i][10].ToString());
				RecallList[i].RecallInterval= new Interval(PIn.PInt(table.Rows[i][5].ToString()));
				RecallList[i].RecallStatus  = PIn.PInt   (table.Rows[i][6].ToString());
				RecallList[i].PatNum        = PIn.PInt   (table.Rows[i][1].ToString());
				RecallList[i].Age           = Shared.DateToAge(RecallList[i].BirthDate);
				RecallList[i].Note          = PIn.PString(table.Rows[i][7].ToString());
				RecallList[i].RecallNum     = PIn.PInt   (table.Rows[i][0].ToString());
				orderDate[i]=RecallList[i].DueDate;
			}
			Array.Sort(orderDate,RecallList);
			return RecallList;

			 
			/*	"SELECT MAX(procedurelog.procdate) AS 'LastDate', "
				//+ INTERVAL patient.recallinterval MONTH AS 'DueDate', "
				+"CONCAT(patient.lname,', ',patient.fname,' ',patient.preferred,' ',"
				+"patient.middlei) AS 'Patient Name', "
				+"patient.birthdate,patient.recallinterval,patient.recallstatus,patient.patnum "
				//+"patient.nextaptnum,appointment.aptdatetime "
				+"FROM patient,procedurelog,procedurecode "
				//+"LEFT JOIN appointment ON appointment.nextaptnum = patient.nextaptnum "
				+"WHERE patient.patnum = procedurelog.patnum "
				+"AND procedurecode.adacode = procedurelog.adacode "
				+"AND procedurecode.setrecall = 1 "
				+"AND (procedurelog.procstatus = 2 "
				+"OR procedurelog.procstatus = 3 "
				+"OR procedurelog.procstatus = 4) "
				+"AND patient.patstatus = 0 "
				+"GROUP BY patient.patnum "
				+"ORDER BY LastDate";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			RecallItem[] RecallList = new RecallItem[table.Rows.Count];
			DateTime[] orderDate=new DateTime[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				RecallList[i].DueDate = (PIn.PDate  (table.Rows[i][0].ToString()))//last date
					.AddMonths(PIn.PInt(table.Rows[i][3].ToString()));//plus number of months
				RecallList[i].PatientName   = PIn.PString(table.Rows[i][1].ToString());
				RecallList[i].BirthDate     = PIn.PDate  (table.Rows[i][2].ToString());
				RecallList[i].RecallInterval= PIn.PInt   (table.Rows[i][3].ToString());
				RecallList[i].RecallStatus  = PIn.PInt   (table.Rows[i][4].ToString());
				RecallList[i].PatNum        = PIn.PInt   (table.Rows[i][5].ToString());
				RecallList[i].Age=Shared.DateToAge(RecallList[i].BirthDate);
				orderDate[i]=RecallList[i].DueDate;
			}
			Array.Sort(orderDate,RecallList);
			return RecallList;*/
		}

		///<summary>Synchronizes all recall for one patient. If datePrevious has changed, then it completely deletes the old recall information and sets a new dateDueCalc and DatePrevious.  Also updates dateDue to match dateDueCalc if not disabled.  The supplied recall can be null if patient has no existing recall. Deletes or creates any recalls as necessary.</summary>
		public static void Synch(int patNum,Recall recall){
			DateTime previousDate=GetPreviousDate(patNum);
			if(recall!=null 
				&& !recall.IsDisabled
				&& previousDate.Year>1880//this protects recalls that were manually added as part of a conversion
				&& previousDate != recall.DatePrevious) {//if datePrevious has changed, reset
				recall.RecallStatus=0;
				recall.Note="";
				recall.DateDue=recall.DateDueCalc;//now it is allowed to be changed in the steps below
			}
			if(previousDate.Year<1880){//if no previous date
				if(recall==null){//no recall present
					//do nothing.
				}
				else{
					recall.DatePrevious=DateTime.MinValue;
					if(recall.DateDue==recall.DateDueCalc){//user did not enter a DateDue
						recall.DateDue=DateTime.MinValue;
					}
					recall.DateDueCalc=DateTime.MinValue;
					recall.Update();
					if(recall.IsAllDefault()){//no useful info
						recall.Delete();
						recall=null;
					}
				}
			}
			else{//if previous date is a valid date
				if(recall==null){//no recall present
					recall=new Recall();
					recall.PatNum=patNum;
					recall.DatePrevious=previousDate;
					recall.RecallInterval=new Interval(0,0,6,0);
					recall.DateDueCalc=previousDate+recall.RecallInterval;
					recall.DateDue=recall.DateDueCalc;
					recall.Insert();
					return;
				}
				else{
					recall.DatePrevious=previousDate;
					if(recall.IsDisabled){//if the existing recall is disabled 
						recall.DateDue=DateTime.MinValue;//DateDue is always blank
					}
					else{//but if not disabled
						if(recall.DateDue==recall.DateDueCalc//if user did not enter a DateDue
							|| recall.DateDue.Year<1880) {//or DateDue was blank
							recall.DateDue=recall.DatePrevious+recall.RecallInterval;//set same as DateDueCalc
						}
					}
					recall.DateDueCalc=recall.DatePrevious+recall.RecallInterval;
					recall.Update();
				}
			}
		}

		///<summary>Synchronizes all recall for one patient. Sets dateDueCalc and DatePrevious.  Also updates dateDue to match dateDueCalc if not disabled.  The supplied recall can be null if patient has no existing recall. Deletes or creates any recalls as necessary.</summary>
		public static void Synch(int patNum){
			Recall[] recalls=GetList(new int[] {patNum});
			Recall recall=null;
			if(recalls.Length>0){
				recall=recalls[0];
			}
			Synch(patNum,recall);
		}

		private static DateTime GetPreviousDate(int patNum){
			string command= 
				"SELECT MAX(procedurelog.procdate) "
				+"FROM procedurelog,procedurecode "
				+"WHERE procedurelog.PatNum="+patNum.ToString()
				+" AND procedurecode.ADACode = procedurelog.ADACode "
				+"AND procedurecode.SetRecall = 1 "
				+"AND (procedurelog.ProcStatus = 2 "
				+"OR procedurelog.ProcStatus = 3 "
				+"OR procedurelog.ProcStatus = 4) "
				+"GROUP BY procedurelog.PatNum";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count==0){
				return DateTime.MinValue;
			}
			return PIn.PDate(table.Rows[0][0].ToString());
		}

		///<summary>Only called when editing certain procedurecodes, but only very rarely as needed. For power users, this is a good little trick to use to synch recall for all patients.</summary>
		public static void SynchAllPatients(){
			//get all active patients
			string command="SELECT PatNum "
				+"FROM patient "
				+"WHERE PatStatus=0";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++){
				Synch(PIn.PInt(table.Rows[i][0].ToString()));
			}
		}

		/// <summary></summary>
		public static DataTable GetAddrTable(int[] patNums){
			string command="SELECT patient.LName,patient.FName,patient.MiddleI,patient.Preferred,"
				+"patient.Address,patient.Address2,patient.City,patient.State,patient.Zip,recall.DateDue "
				+"FROM patient,recall "
				+"WHERE patient.PatNum=recall.PatNum "
				+"AND (";
      for(int i=0;i<patNums.Length;i++){
        if(i>0){
					command+=" OR ";
				}
        command+="patient.PatNum="+patNums[i].ToString();
      }
			command+=") ORDER BY patient.LName,patient.FName";
			DataConnection dcon=new DataConnection();
			return dcon.GetTable(command);
		}

		/// <summary></summary>
		public static void UpdateStatus(int recallNum,int newStatus){
			string command="UPDATE recall SET RecallStatus="+newStatus.ToString()
				+" WHERE RecallNum="+recallNum.ToString();
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}


	}

	


}









