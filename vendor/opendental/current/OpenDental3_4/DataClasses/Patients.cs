using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary></summary>
	public class Patients{
		//<summary>Current patient.</summary>
		//private static Patient curr;
		//<summary>Used during patient.update.  Stores the original data before any changes were made.</summary>
		//public static Patient CurOld;
		///<summary>A list of all patient names. Key=patNum, value=formatted name.  Fill with GetHList.</summary>
		public static Hashtable HList;
		///<summary>Collection of Patients. The last five patients. Gets displayed on dropdown button.</summary>
		private static ArrayList buttonLastFive;

		//<summary>Current Patient. This will be phased out eventually along with all other global variables.</summary>
		/*public static Patient Curr{
			get{
				return curr;
			}
			set{
				curr=value;
				//curOld=value;
			}
		}*/

		///<summary>Returns a Family object for the supplied patNum.  Use Family.GetPatient to extract the desired patient from the family.</summary>
		public static Family GetFamily(int patNum){
			string command= 
				"SELECT guarantor FROM patient "
				+"WHERE patnum = '"+POut.PInt(patNum)+"'";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count==0){
				//PatIsLoaded=false;
				//cur=null;
				//Family.List=null;//new Patient[1];
				//FamilyList[0]=cur;
				//GuarIndex=0;
				return null;
			}
			command= 
				"SELECT * "
				+"FROM patient "
				+"WHERE Guarantor = '"+table.Rows[0][0].ToString()+"'"
				+" ORDER BY (PatNum!=Guarantor), Birthdate";
			Family fam=new Family();
			fam.List=SubmitAndFill(command);
			return fam;
		}

		///<summary>This is a way to get a single patient from the database if you don't already have a family object to use.</summary>
		public static Patient GetPat(int patNum){
			string command="SELECT * FROM patient WHERE PatNum="+POut.PInt(patNum);
			return SubmitAndFill(command)[0];
		}

		private static Patient[] SubmitAndFill(string command){//,int patNum){//,bool isSingle){
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			Patient[] retVal=new Patient[table.Rows.Count];
			//if(isSingle){
				//
			//}
			//else{
			//	Family.List=new Patient[table.Rows.Count];
			//}
			//MessageBox.Show(table.Rows.Count.ToString());
			//Patient tempPat;
			for(int i=0;i<table.Rows.Count;i++){
				retVal[i]=new Patient();
				retVal[i].PatNum       = PIn.PInt   (table.Rows[i][0].ToString());
				retVal[i].LName        = PIn.PString(table.Rows[i][1].ToString());
				retVal[i].FName        = PIn.PString(table.Rows[i][2].ToString());
				retVal[i].MiddleI      = PIn.PString(table.Rows[i][3].ToString());
				retVal[i].Preferred    = PIn.PString(table.Rows[i][4].ToString());
				retVal[i].PatStatus    = (PatientStatus)PIn.PInt   (table.Rows[i][5].ToString());
				retVal[i].Gender       = (PatientGender)PIn.PInt   (table.Rows[i][6].ToString());
				retVal[i].Position     = (PatientPosition)PIn.PInt   (table.Rows[i][7].ToString());
				retVal[i].Birthdate    = PIn.PDate  (table.Rows[i][8].ToString());
				retVal[i].Age=Shared.DateToAge(retVal[i].Birthdate);
				//Debug.WriteLine("*"+retVal[i].Age+"*");
				retVal[i].SSN          = PIn.PString(table.Rows[i][9].ToString());
				retVal[i].Address      = PIn.PString(table.Rows[i][10].ToString());
				retVal[i].Address2     = PIn.PString(table.Rows[i][11].ToString());
				retVal[i].City         = PIn.PString(table.Rows[i][12].ToString());
				retVal[i].State        = PIn.PString(table.Rows[i][13].ToString());
				retVal[i].Zip          = PIn.PString(table.Rows[i][14].ToString());
				retVal[i].HmPhone      = PIn.PString(table.Rows[i][15].ToString());
				retVal[i].WkPhone      = PIn.PString(table.Rows[i][16].ToString());
				retVal[i].WirelessPhone= PIn.PString(table.Rows[i][17].ToString());
				retVal[i].Guarantor    = PIn.PInt   (table.Rows[i][18].ToString());
				retVal[i].CreditType   = PIn.PString(table.Rows[i][19].ToString());
				retVal[i].Email        = PIn.PString(table.Rows[i][20].ToString());
				retVal[i].Salutation   = PIn.PString(table.Rows[i][21].ToString());
				retVal[i].PriPlanNum   = PIn.PInt   (table.Rows[i][22].ToString());
				retVal[i].PriRelationship=(Relat)PIn.PInt(table.Rows[i][23].ToString());
				retVal[i].SecPlanNum   = PIn.PInt   (table.Rows[i][24].ToString());
				retVal[i].SecRelationship=(Relat)PIn.PInt(table.Rows[i][25].ToString());
				retVal[i].EstBalance   = PIn.PDouble(table.Rows[i][26].ToString());
				retVal[i].NextAptNum   = PIn.PInt   (table.Rows[i][27].ToString());
				retVal[i].PriProv      = PIn.PInt   (table.Rows[i][28].ToString());
				retVal[i].SecProv      = PIn.PInt   (table.Rows[i][29].ToString());
				retVal[i].FeeSched     = PIn.PInt   (table.Rows[i][30].ToString());
				retVal[i].BillingType  = PIn.PInt   (table.Rows[i][31].ToString());
				//retVal[i].RecallInterval=PIn.PInt   (table.Rows[i][32].ToString());
				//retVal[i].RecallStatus = PIn.PInt   (table.Rows[i][33].ToString());
				retVal[i].ImageFolder  = PIn.PString(table.Rows[i][34].ToString());
				retVal[i].AddrNote     = PIn.PString(table.Rows[i][35].ToString());
				retVal[i].FamFinUrgNote= PIn.PString(table.Rows[i][36].ToString());
				retVal[i].MedUrgNote   = PIn.PString(table.Rows[i][37].ToString());
				retVal[i].ApptModNote  = PIn.PString(table.Rows[i][38].ToString());
				retVal[i].StudentStatus= PIn.PString(table.Rows[i][39].ToString());
				retVal[i].SchoolName   = PIn.PString(table.Rows[i][40].ToString());
				retVal[i].ChartNumber  = PIn.PString(table.Rows[i][41].ToString());
				retVal[i].MedicaidID   = PIn.PString(table.Rows[i][42].ToString());
				retVal[i].Bal_0_30     = PIn.PDouble(table.Rows[i][43].ToString());
				retVal[i].Bal_31_60    = PIn.PDouble(table.Rows[i][44].ToString());
				retVal[i].Bal_61_90    = PIn.PDouble(table.Rows[i][45].ToString());
				retVal[i].BalOver90    = PIn.PDouble(table.Rows[i][46].ToString());
				retVal[i].InsEst       = PIn.PDouble(table.Rows[i][47].ToString());
				retVal[i].PrimaryTeeth = PIn.PString(table.Rows[i][48].ToString());
				retVal[i].BalTotal     = PIn.PDouble(table.Rows[i][49].ToString());
				retVal[i].EmployerNum  = PIn.PInt   (table.Rows[i][50].ToString());
				retVal[i].EmploymentNote=PIn.PString(table.Rows[i][51].ToString());
				retVal[i].Race         = (PatientRace)PIn.PInt(table.Rows[i][52].ToString());
				retVal[i].County       = PIn.PString(table.Rows[i][53].ToString());
				retVal[i].GradeSchool  = PIn.PString(table.Rows[i][54].ToString());
				retVal[i].GradeLevel   = (PatientGrade)PIn.PInt(table.Rows[i][55].ToString());
				retVal[i].Urgency      = (TreatmentUrgency)PIn.PInt(table.Rows[i][56].ToString());
				retVal[i].DateFirstVisit=PIn.PDate  (table.Rows[i][57].ToString());
				retVal[i].PriPending   = PIn.PBool  (table.Rows[i][58].ToString());
				retVal[i].SecPending   = PIn.PBool  (table.Rows[i][59].ToString());
				//if(isSingle){
				//	retVal=tempPat.Copy();
				//}
				//else{
				//	Family.List[i]=tempPat.Copy();
				//	if(Family.List[i].PatNum==patNum){
				//		cur=Family.List[i].Copy();
				//		CurOld=Family.List[i].Copy();
				//	}
					//if(FamilyList[i].Guarantor==FamilyList[i].PatNum)
					//	GuarIndex=i;
				//}
			}
			//if(!isSingle){
				//PatIsLoaded=true;
			//}
			return retVal;//really only used when isSingle
		}

 		///<summary>Only used for the Select Patient dialog</summary>
		public static DataTable GetPtDataTable(bool limit,string lname,string fname,string hmphone,string address,bool hideInactive,string city,string state,string ssn,string patnum,string chartnumber,int[] billingtypes,bool guarOnly){
			//bool retval=false;
			string billingsnippet="";
			for(int i=0;i<billingtypes.Length;i++){
				if(i==0){
					billingsnippet+="AND (";
				}
				else{
					billingsnippet+="|| ";
				}
				billingsnippet+="BillingType ='"+billingtypes[i].ToString()+"' ";
				if(i==billingtypes.Length-1){//if there is only one row, this will also be triggered.
					billingsnippet+=") ";
				}
			}
			string command= 
				"SELECT PatNum,LName,FName,MiddleI,Preferred,Birthdate,SSN,HmPhone,Address,PatStatus"
				+",BillingType,ChartNumber,City,State "
				+"FROM patient "
				+"WHERE PatStatus != '4' "//not status 'deleted'
				+"AND LName LIKE '"      +POut.PString(lname)+"%' "
				+"AND FName LIKE '"      +POut.PString(fname)+"%' "
				+"AND HmPhone LIKE '"    +POut.PString(hmphone)+"%' "
				+"AND Address LIKE '"    +POut.PString(address)+"%' "
				+"AND City LIKE '"       +POut.PString(city)+"%' "
				+"AND State LIKE '"      +POut.PString(state)+"%' "
				+"AND SSN LIKE '"        +POut.PString(ssn)+"%' "
				+"AND PatNum LIKE '"     +POut.PString(patnum)+"%' "
				+"AND ChartNumber LIKE '"+POut.PString(chartnumber)+"%' "
				+billingsnippet;
			if(hideInactive){
				command+="AND PatStatus = '0' ";
			}
			if(guarOnly){
				command+="AND PatNum = Guarantor ";
			}
			command+="ORDER BY LName,FName";
			if(limit)
				command+=" LIMIT 36";//only need 35, but the extra will help indicate more rows
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			DataTable PtDataTable=table.Clone();//does not copy any data
			for(int i=0;i<PtDataTable.Columns.Count;i++){
				PtDataTable.Columns[i].DataType=typeof(string);
			}
			//if(limit && table.Rows.Count==36){
			//	retval=true;
			//}
			DataRow r;
			for(int i=0;i<table.Rows.Count;i++){//table.Rows.Count && i<44;i++){
				r=PtDataTable.NewRow();
				//PatNum,LName,FName,MiddleI,Preferred,Birthdate,SSN,HmPhone,Address,PatStatus"
				//+",BillingType,ChartNumber,City,State
				r[0]=table.Rows[i][0].ToString();
				r[1]=table.Rows[i][1].ToString();
				r[2]=table.Rows[i][2].ToString();
				r[3]=table.Rows[i][3].ToString();
				r[4]=table.Rows[i][4].ToString();
				r[5]=Shared.DateToAge(PIn.PDate(table.Rows[i][5].ToString()));
				r[6]=table.Rows[i][6].ToString();
				r[7]=table.Rows[i][7].ToString();
				r[8]=table.Rows[i][8].ToString();
				r[9]=((PatientStatus)PIn.PInt(table.Rows[i][9].ToString())).ToString();
				r[10]=Defs.GetName(DefCat.BillingTypes,PIn.PInt(table.Rows[i][10].ToString()));
				r[11]=table.Rows[i][11].ToString();
				r[12]=table.Rows[i][12].ToString();
				r[13]=table.Rows[i][13].ToString();
				PtDataTable.Rows.Add(r);

				/*
				PtList[i].PatNum     = PIn.PInt   (table.Rows[i][0].ToString());
				PtList[i].LName      = PIn.PString(table.Rows[i][1].ToString());
				PtList[i].FName      = PIn.PString(table.Rows[i][2].ToString());
				PtList[i].MiddleI    = PIn.PString(table.Rows[i][3].ToString());
				PtList[i].Preferred  = PIn.PString(table.Rows[i][4].ToString());
				PtList[i].Age        = Shared.DateToAge(PIn.PDate  (table.Rows[i][5].ToString()));
				PtList[i].SSN        = PIn.PString(table.Rows[i][6].ToString());
				PtList[i].HmPhone    = PIn.PString(table.Rows[i][7].ToString());
				PtList[i].Address    = PIn.PString(table.Rows[i][8].ToString());
				PtList[i].PatStatus  = (PatientStatus)PIn.PInt(table.Rows[i][9].ToString());
				PtList[i].BillingType= PIn.PInt(table.Rows[i][10].ToString());
				//chartnumber
				//city
				//state*/
			}
			return PtDataTable;//retval;//if true, there are more rows.
		}

		///<summary>Used when filling appointments for an entire day. Gets a list of Pats, multPats, of all the specified patients.  Then, use GetOnePat to pull one patient from this list.  This process requires only one call to the database.</summary>
		public static Patient[] GetMultPats(int[] patNums){
			//MessageBox.Show(patNums.Length.ToString());
			string strPatNums="";
			DataTable table;
			if(patNums.Length>0){
				for(int i=0;i<patNums.Length;i++){
					if(i>0){
						strPatNums+="|| ";
					}
					strPatNums+="PatNum='"+patNums[i].ToString()+"' ";
				}
				string command="SELECT * from patient WHERE "+strPatNums;
				//MessageBox.Show(cmd.CommandText);
				DataConnection dcon=new DataConnection();
 				table=dcon.GetTable(command);
			}
			else{
				table=new DataTable();
			}
			Patient[] multPats=new Patient[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				multPats[i]=new Patient();
				multPats[i].PatNum       = PIn.PInt   (table.Rows[i][0].ToString());
				multPats[i].LName        = PIn.PString(table.Rows[i][1].ToString());
				multPats[i].FName        = PIn.PString(table.Rows[i][2].ToString());
				multPats[i].MiddleI      = PIn.PString(table.Rows[i][3].ToString());
				multPats[i].Preferred    = PIn.PString(table.Rows[i][4].ToString());
				multPats[i].PatStatus    = (PatientStatus)PIn.PInt   (table.Rows[i][5].ToString());
				multPats[i].Gender       = (PatientGender)PIn.PInt   (table.Rows[i][6].ToString());
				multPats[i].Position     = (PatientPosition)PIn.PInt   (table.Rows[i][7].ToString());
				multPats[i].Birthdate    = PIn.PDate  (table.Rows[i][8].ToString());
				multPats[i].Age=Shared.DateToAge(multPats[i].Birthdate);
				multPats[i].SSN          = PIn.PString(table.Rows[i][9].ToString());
				multPats[i].Address      = PIn.PString(table.Rows[i][10].ToString());
				multPats[i].Address2     = PIn.PString(table.Rows[i][11].ToString());
				multPats[i].City         = PIn.PString(table.Rows[i][12].ToString());
				multPats[i].State        = PIn.PString(table.Rows[i][13].ToString());
				multPats[i].Zip          = PIn.PString(table.Rows[i][14].ToString());
				multPats[i].HmPhone      = PIn.PString(table.Rows[i][15].ToString());
				multPats[i].WkPhone      = PIn.PString(table.Rows[i][16].ToString());
				multPats[i].WirelessPhone= PIn.PString(table.Rows[i][17].ToString());
				multPats[i].Guarantor    = PIn.PInt   (table.Rows[i][18].ToString());
				multPats[i].CreditType   = PIn.PString(table.Rows[i][19].ToString());
				multPats[i].Email        = PIn.PString(table.Rows[i][20].ToString());
				multPats[i].Salutation   = PIn.PString(table.Rows[i][21].ToString());
				multPats[i].PriPlanNum   = PIn.PInt   (table.Rows[i][22].ToString());
				multPats[i].PriRelationship=(Relat)PIn.PInt(table.Rows[i][23].ToString());
				multPats[i].SecPlanNum   = PIn.PInt   (table.Rows[i][24].ToString());
				multPats[i].SecRelationship=(Relat)PIn.PInt(table.Rows[i][25].ToString());
				multPats[i].EstBalance   = PIn.PDouble(table.Rows[i][26].ToString());
				multPats[i].NextAptNum   = PIn.PInt   (table.Rows[i][27].ToString());
				multPats[i].PriProv      = PIn.PInt   (table.Rows[i][28].ToString());
				multPats[i].SecProv      = PIn.PInt   (table.Rows[i][29].ToString());
				multPats[i].FeeSched     = PIn.PInt   (table.Rows[i][30].ToString());
				multPats[i].BillingType  = PIn.PInt   (table.Rows[i][31].ToString());
				//multPats[i].RecallInterval=PIn.PInt   (table.Rows[i][32].ToString());
				//multPats[i].RecallStatus = PIn.PInt   (table.Rows[i][33].ToString());
				multPats[i].ImageFolder  = PIn.PString(table.Rows[i][34].ToString());
				multPats[i].AddrNote     = PIn.PString(table.Rows[i][35].ToString());
				multPats[i].FamFinUrgNote= PIn.PString(table.Rows[i][36].ToString());
				multPats[i].MedUrgNote   = PIn.PString(table.Rows[i][37].ToString());
				multPats[i].ApptModNote  = PIn.PString(table.Rows[i][38].ToString());
				multPats[i].StudentStatus= PIn.PString(table.Rows[i][39].ToString());
				multPats[i].SchoolName   = PIn.PString(table.Rows[i][40].ToString());
				multPats[i].ChartNumber  = PIn.PString(table.Rows[i][41].ToString());
				multPats[i].MedicaidID   = PIn.PString(table.Rows[i][42].ToString());
				multPats[i].Bal_0_30     = PIn.PDouble(table.Rows[i][43].ToString());
				multPats[i].Bal_31_60    = PIn.PDouble(table.Rows[i][44].ToString());
				multPats[i].Bal_61_90    = PIn.PDouble(table.Rows[i][45].ToString());
				multPats[i].BalOver90    = PIn.PDouble(table.Rows[i][46].ToString());
				multPats[i].InsEst       = PIn.PDouble(table.Rows[i][47].ToString());
				multPats[i].PrimaryTeeth = PIn.PString(table.Rows[i][48].ToString());
				multPats[i].BalTotal     = PIn.PDouble(table.Rows[i][49].ToString());
				multPats[i].EmployerNum  = PIn.PInt   (table.Rows[i][50].ToString());
				multPats[i].EmploymentNote=PIn.PString(table.Rows[i][51].ToString());
				multPats[i].Race         = (PatientRace)PIn.PInt(table.Rows[i][52].ToString());
				multPats[i].County       = PIn.PString(table.Rows[i][53].ToString());
				multPats[i].GradeSchool  = PIn.PString(table.Rows[i][54].ToString());
				multPats[i].GradeLevel   = (PatientGrade)PIn.PInt(table.Rows[i][55].ToString());
				multPats[i].Urgency      = (TreatmentUrgency)PIn.PInt(table.Rows[i][56].ToString());
				multPats[i].DateFirstVisit=PIn.PDate  (table.Rows[i][57].ToString());
				multPats[i].PriPending   = PIn.PBool  (table.Rows[i][58].ToString());
				multPats[i].SecPending   = PIn.PBool  (table.Rows[i][59].ToString());
			}
			return multPats;
		}

		///<summary>First call GetMultPats to fill the list of multPats. Then, use this to return one patient from that list.</summary>
		public static Patient GetOnePat(Patient[] multPats, int patNum){
			for(int i=0;i<multPats.Length;i++){
				if(multPats[i].PatNum==patNum){
					return multPats[i];
				}
			}
			return new Patient();
		}

		/// <summary>Gets nine of the most useful fields from the db for the given patnum.</summary>
		public static Patient GetLim(int patNum){
			if(patNum==0){
				return new Patient();
			}
			string command= 
				"SELECT PatNum,LName,FName,MiddleI,Preferred,CreditType,Guarantor,PriPlanNum,SSN " 
				+"FROM patient "
				+"WHERE PatNum = '"+patNum.ToString()+"'";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count==0){
				return new Patient();
			}
			Patient Lim=new Patient();
			Lim.PatNum     = PIn.PInt   (table.Rows[0][0].ToString());
			Lim.LName      = PIn.PString(table.Rows[0][1].ToString());
			Lim.FName      = PIn.PString(table.Rows[0][2].ToString());
			Lim.MiddleI    = PIn.PString(table.Rows[0][3].ToString());
			Lim.Preferred  = PIn.PString(table.Rows[0][4].ToString());
			Lim.CreditType = PIn.PString(table.Rows[0][5].ToString());
			Lim.Guarantor  = PIn.PInt   (table.Rows[0][6].ToString());
			Lim.PriPlanNum = PIn.PInt   (table.Rows[0][7].ToString());
			Lim.SSN        = PIn.PString(table.Rows[0][8].ToString());
			return Lim;
			//if(Lim.Preferred=="")
			//	LimName=Lim.LName+", "+Lim.FName+" "+Lim.MiddleI;
			//else LimName=Lim.LName+", '"+Lim.Preferred+"' "+Lim.FName+" "+Lim.MiddleI;
		}

		///<summary></summary>
		public static void ChangeGuarantorToCur(Family Fam,Patient Pat){
			//Move famfinurgnote to current patient:
			string command= 
				"UPDATE patient SET "
				//+"famaddrnote = '"  +FamilyList[GuarIndex].FamAddrNote+"', "
				+"famfinurgnote = '"+Fam.List[0].FamFinUrgNote+"' "
				+"WHERE patnum = '"+Pat.PatNum.ToString()+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
			command= 
				"UPDATE patient SET "
				//+"famaddrnote = '', "
				+"famfinurgnote = '' "
				+"WHERE patnum = '"+Pat.Guarantor.ToString()+"'";
			dcon.NonQ(command);
			//Move family financial note to current patient:
			command="SELECT FamFinancial FROM patientnote "
				+"WHERE patnum = '"+Pat.Guarantor.ToString()+"'";
			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count==1){
				command= 
					"UPDATE patientnote SET "
					+"famfinancial = '"+table.Rows[0][0].ToString()+"' "
					+"WHERE patnum = '"+Pat.PatNum.ToString()+"'";
				dcon.NonQ(command);
			}
			command= 
				"UPDATE patientnote SET "
				+"famfinancial = ''"
				+"WHERE patnum = '"+Pat.Guarantor.ToString()+"'";
			dcon.NonQ(command);
			//change guarantor of all family members:
			command= 
				"UPDATE patient SET "
				+"guarantor = '"+Pat.PatNum.ToString()+"' "
				+"WHERE guarantor = '"+Pat.Guarantor.ToString()+"'";
			dcon.NonQ(command);
		}
		
		///<summary></summary>
		public static void CombineGuarantors(Family Fam,Patient Pat){
			//concat cur notes with guarantor notes
			string command= 
				"UPDATE patient SET "
				//+"addrnote = '"+POut.PString(FamilyList[GuarIndex].FamAddrNote)
				//									+POut.PString(cur.FamAddrNote)+"', "
				+"famfinurgnote = '"+POut.PString(Fam.List[0].FamFinUrgNote)
				+POut.PString(Pat.FamFinUrgNote)+"' "
				+"WHERE patnum = '"+Pat.Guarantor.ToString()+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
			//delete cur notes
			command= 
				"UPDATE patient SET "
				//+"famaddrnote = '', "
				+"famfinurgnote = '' "
				+"WHERE patnum = '"+Pat.PatNum+"'";
			dcon.NonQ(command);
			//concat family financial notes
			PatientNotes.Refresh(Pat.PatNum,Pat.Guarantor);
			//patientnote table must have been refreshed for this to work.
			//Makes sure there are entries for patient and for guarantor.
			//Also, PatientNotes.cur.FamFinancial will now have the guar info in it.
			string strGuar=PatientNotes.Cur.FamFinancial;
			command= 
				"SELECT famfinancial "
				+"FROM patientnote WHERE patnum ='"+POut.PInt(Pat.PatNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataTable table=dcon.GetTable(command);
			string strCur=PIn.PString(table.Rows[0][0].ToString());
			command= 
				"UPDATE patientnote SET "
				+"famfinancial = '"+strGuar+strCur+"' "
				+"WHERE patnum = '"+Pat.Guarantor.ToString()+"'";
			dcon.NonQ(command);
			//delete cur financial notes
			command= 
				"UPDATE patientnote SET "
				+"famfinancial = ''"
				+"WHERE patnum = '"+Pat.PatNum.ToString()+"'";
			dcon.NonQ(command);
		}

		///<summary>Gets names for all patients.</summary>
		public static void GetHList(){
			string command="SELECT patnum,lname,fname,middlei,preferred "
				+"FROM patient";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			HList=new Hashtable(table.Rows.Count);
			int patnum;
			string lname,fname,middlei,preferred;
			for(int i=0;i<table.Rows.Count;i++){
				patnum=PIn.PInt(table.Rows[i][0].ToString());
				lname=PIn.PString(table.Rows[i][1].ToString());
				fname=PIn.PString(table.Rows[i][2].ToString());
				middlei=PIn.PString(table.Rows[i][3].ToString());
				preferred=PIn.PString(table.Rows[i][4].ToString());
				if(preferred==""){
					HList.Add(patnum,lname+", "+fname+" "+middlei);
				}
				else{
					HList.Add(patnum,lname+", '"+preferred+"' "+fname+" "+middlei);
				}
			}
		}

		///<summary></summary>
		public static void UpdateAddressForFam(Patient pat){
			string command= "UPDATE patient SET " 
				+"Address = '"    +POut.PString(pat.Address)+"'"
				+",Address2 = '"   +POut.PString(pat.Address2)+"'"
				+",City = '"       +POut.PString(pat.City)+"'"
				+",State = '"      +POut.PString(pat.State)+"'"
				+",Zip = '"        +POut.PString(pat.Zip)+"'"
				+",HmPhone = '"    +POut.PString(pat.HmPhone)+"'"
				+",credittype = '" +POut.PString(pat.CreditType)+"'"
				+",priprov = '"    +POut.PInt   (pat.PriProv)+"'"
				+",secprov = '"    +POut.PInt   (pat.SecProv)+"'"
				+",feesched = '"   +POut.PInt   (pat.FeeSched)+"'"
				+",billingtype = '"+POut.PInt   (pat.BillingType)+"'"
				+" WHERE guarantor = '"+POut.PDouble(pat.Guarantor)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public static void UpdateNotesForFam(Patient pat){
			string command= "UPDATE patient SET " 
				+"addrnote = '"   +POut.PString(pat.AddrNote)+"'"
				+" WHERE guarantor = '"+POut.PDouble(pat.Guarantor)+"'";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			//MessageBox.Show(cmd.CommandText);
		}		

		///<summary>This is only used in the Billing dialog</summary>
		public static PatAging[] GetAgingList(string age,int[] billingIndices,bool excludeAddr
			,bool excludeNeg,double excludeLessThan,bool excludeInactive){
			string command=
				"SELECT patnum,Bal_0_30,Bal_31_60,Bal_61_90,BalOver90,BalTotal,InsEst,LName,FName,MiddleI "
				+"FROM patient WHERE ";//actually only gets guarantors since others are 0.
			if(excludeInactive){
				command+="(patstatus != '2') && ";
			}
			command+="(BalTotal - InsEst > '"+excludeLessThan.ToString()+"'";
			if(!excludeNeg){
				command+=" || BalTotal - InsEst < '0')";
			}
			else{
				command+=")";
			}
			switch(age){
				//where is age 0. Is it missing because no restriction
				case "30":
					command+=" && (Bal_31_60 > '0' || Bal_61_90 > '0' || BalOver90 > '0')";
					break;
				case "60":
					command+=" && (Bal_61_90 > '0' || BalOver90 > '0')";
					break;
				case "90":
					command+=" && (BalOver90 > '0')";
					break;
			}
			for(int i=0;i<billingIndices.Length;i++){
				if(i==0){
					command+=" && (billingtype = '";
				}
				else{
					command+=" || billingtype = '";
				}
				command+=
					Defs.Short[(int)DefCat.BillingTypes][billingIndices[i]].DefNum.ToString()+"'";
			}
			command+=")";
			if(excludeAddr){
				command+=" && (zip !='')";
			}	
			command+=" ORDER BY LName,FName";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			PatAging[] AgingList=new PatAging[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				AgingList[i]=new PatAging();
				AgingList[i].PatNum   = PIn.PInt   (table.Rows[i][0].ToString());
				AgingList[i].Bal_0_30 = PIn.PDouble(table.Rows[i][1].ToString());
				AgingList[i].Bal_31_60= PIn.PDouble(table.Rows[i][2].ToString());
				AgingList[i].Bal_61_90= PIn.PDouble(table.Rows[i][3].ToString());
				AgingList[i].BalOver90= PIn.PDouble(table.Rows[i][4].ToString());
				AgingList[i].BalTotal = PIn.PDouble(table.Rows[i][5].ToString());
				AgingList[i].InsEst   = PIn.PDouble(table.Rows[i][6].ToString());
				AgingList[i].PatName=PIn.PString(table.Rows[i][7].ToString())
					+", "+PIn.PString(table.Rows[i][8].ToString())
					+" "+PIn.PString(table.Rows[i][9].ToString());;
				//AgingList[i].Balance=AgingList[i].Bal_0_30+AgingList[i].Bal_31_60
				//	+AgingList[i].Bal_61_90+AgingList[i].BalOver90;
				AgingList[i].AmountDue=AgingList[i].BalTotal-AgingList[i].InsEst;
			}
			return AgingList;
		}

		///<summary>Used only to run finance charges, so it ignores negative balances.</summary>
		public static PatAging[] GetAgingList(){
			string command =
				"SELECT patnum,Bal_0_30,Bal_31_60,Bal_61_90,BalOver90,BalTotal,InsEst,LName,FName,MiddleI,priprov "
				+"FROM patient "//actually only gets guarantors since others are 0.
				+" WHERE Bal_0_30 + Bal_31_60 + Bal_61_90 + BalOver90 - InsEst > '0.005'"//more that 1/2 cent
				+" ORDER BY LName,FName";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			PatAging[] AgingList=new PatAging[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				AgingList[i]=new PatAging();
				AgingList[i].PatNum   = PIn.PInt   (table.Rows[i][0].ToString());
				AgingList[i].Bal_0_30 = PIn.PDouble(table.Rows[i][1].ToString());
				AgingList[i].Bal_31_60= PIn.PDouble(table.Rows[i][2].ToString());
				AgingList[i].Bal_61_90= PIn.PDouble(table.Rows[i][3].ToString());
				AgingList[i].BalOver90= PIn.PDouble(table.Rows[i][4].ToString());
				AgingList[i].BalTotal = PIn.PDouble(table.Rows[i][5].ToString());
				AgingList[i].InsEst   = PIn.PDouble(table.Rows[i][6].ToString());
				AgingList[i].PatName=PIn.PString(table.Rows[i][7].ToString())
					+", "+PIn.PString(table.Rows[i][8].ToString())
					+" "+PIn.PString(table.Rows[i][9].ToString());;
				//AgingList[i].Balance=AgingList[i].Bal_0_30+AgingList[i].Bal_31_60
				//	+AgingList[i].Bal_61_90+AgingList[i].BalOver90;
				AgingList[i].AmountDue=AgingList[i].BalTotal-AgingList[i].InsEst;
				AgingList[i].PriProv=PIn.PInt(table.Rows[i][10].ToString());
			}
			return AgingList;
		}

		///<summary></summary>
		public static void ResetAging(){//for entire database
			//need to zero everything out first because the update aging only inserts non-zero values
			string command="Update patient SET "
				+"Bal_0_30   = '0'"
				+",Bal_31_60 = '0'"
				+",Bal_61_90 = '0'"
				+",BalOver90 = '0'"
				+",InsEst    = '0'"
				+",BalTotal  = '0'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public static void ResetAging(int guarantor){
			string command="Update patient SET "
				+"Bal_0_30   = '0'"
				+",Bal_31_60 = '0'"
				+",Bal_61_90 = '0'"
				+",BalOver90 = '0'"
				+",InsEst    = '0'"
				+",BalTotal  = '0'"
			  +" WHERE guarantor = '"+POut.PInt(guarantor)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public static void UpdateAging(int patnum,double Bal0,double Bal31
			,double Bal61,double Bal91,double InsEst,double BalTotal){
			string command="Update patient SET "
				+"Bal_0_30        = '" +POut.PDouble(Bal0)+"'"
				+",Bal_31_60      = '" +POut.PDouble(Bal31)+"'"
				+",Bal_61_90      = '" +POut.PDouble(Bal61)+"'"
				+",BalOver90      = '" +POut.PDouble(Bal91)+"'"
				+",InsEst         = '" +POut.PDouble(InsEst)+"'"
				+",BalTotal       = '" +POut.PDouble(BalTotal)+"'"
				+" WHERE patnum   = '" +POut.PInt   (patnum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary>Gets the next available integer chart number.  Will later add a where clause based on preferred format.</summary>
		public static string GetNextChartNum(){
			string command="SELECT ChartNumber from patient WHERE"
				+" ChartNumber REGEXP '^[0-9]+$'"//matches any number of digits
				+" ORDER BY chartnumber DESC LIMIT 1";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count==0){//no existing chart numbers
				return "1";
			}
			string lastChartNum=PIn.PString(table.Rows[0][0].ToString());
			//or could add more match conditions
			//if(Regex.IsMatch(lastChartNum,@"^\d+$")){//if is an integer
			return(PIn.PInt(lastChartNum)+1).ToString();
			//}
			//return "1";//if there are no integer chartnumbers yet
		}

		///<summary>Returns the name(only one) of the patient using this chartnumber.</summary>
		public static string ChartNumUsedBy(string chartNum,int excludePatNum){
			string command="SELECT LName,FName from patient WHERE "
				+"ChartNumber = '"+chartNum
				+"' && PatNum != '"+excludePatNum.ToString()+"'";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			string retVal="";
			if(table.Rows.Count!=0){//found duplicate chart number
				retVal=PIn.PString(table.Rows[0][1].ToString())+" "+PIn.PString(table.Rows[0][0].ToString());
			}
			return retVal;
		}

		///<summary>Used in the patient select window to determine if a trial version user is over their limit.</summary>
		public static int GetNumberPatients(){
			string command="SELECT Count(*) FROM patient";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			return PIn.PInt(table.Rows[0][0].ToString());
		}

		///<summary>Adds the current patient to the button. Can handle null values for pat and fam. Also resets the family list on the button appropriately. Need to supply the menu to fill as well as the EventHandler to set for each item (all the same).</summary>
		public static void AddPatsToMenu(ContextMenu menu,EventHandler onClick,Patient pat,Family fam){
			//add current patient
			if(buttonLastFive==null){
				buttonLastFive=new ArrayList();
			}
			if(pat!=null){
				if(buttonLastFive.Count==0
					|| pat.PatNum!=((Patient)buttonLastFive[0]).PatNum){//different patient selected
					buttonLastFive.Insert(0,pat);
					if(buttonLastFive.Count>5){
						buttonLastFive.RemoveAt(5);
					}
				}
			}
			//fill menu
			menu.MenuItems.Clear();
			for(int i=0;i<buttonLastFive.Count;i++){
				menu.MenuItems.Add(((Patient)buttonLastFive[i]).GetNameLF(),onClick);
			}
			menu.MenuItems.Add("-");
			menu.MenuItems.Add("FAMILY");
			if(pat!=null){
				for(int i=0;i<fam.List.Length;i++){
					menu.MenuItems.Add(fam.List[i].GetNameLF(),onClick);
				}
			}
		}

		///<summary>Determines which menu Item was selected from the Patient dropdown list and returns the patNum for that patient. This will not be activated when click on 'FAMILY' or on separator, because they do not have events attached.  Calling class then does a ModuleSelected.</summary>
		public static int ButtonSelect(ContextMenu menu,object sender,Family fam){
			int index=menu.MenuItems.IndexOf((MenuItem)sender);
			//Patients.PatIsLoaded=true;
			if(index<buttonLastFive.Count){
				return ((Patient)buttonLastFive[index]).PatNum;
			}
			return fam.List[index-buttonLastFive.Count-2].PatNum;
		}

		


	}

	

	///<summary>Not a database table.  Just used for running reports.</summary>
	public class PatAging{
		///<summary></summary>
		public int PatNum;
		///<summary></summary>
		public double Bal_0_30;
		///<summary></summary>
		public double Bal_31_60;
		///<summary></summary>
		public double Bal_61_90;
		///<summary></summary>
		public double BalOver90;
		///<summary></summary>
		public double InsEst;
		///<summary></summary>
		public string PatName;
		///<summary></summary>
		public double BalTotal;
		///<summary></summary>
		public double AmountDue;
		///<summary>The patient priprov to assign the finance charge to.</summary>
		public int PriProv;
	}

	///<summary></summary>
	public class PatientSelectedEventArgs{
		private int myPatNum;

		///<summary></summary>
		public PatientSelectedEventArgs(int patNum){
			myPatNum=patNum;
		}

		///<summary></summary>
		public int PatNum{
			get{ 
				return myPatNum;
			}
		}

	}

	///<summary></summary>
	public delegate void PatientSelectedEventHandler(object sender,PatientSelectedEventArgs e);

	


}









