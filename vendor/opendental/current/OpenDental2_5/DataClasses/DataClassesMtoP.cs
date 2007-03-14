/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
//using MySQLDriverCS;

using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OpenDental{
	
	/*=========================================================================================
		=================================== class Medications ==========================================*/

	public class Medications:DataClass{
		//not refreshed with local data.  Only refreshed as needed.
		public static Medication Cur;
		public static Medication[] List;
		public static Hashtable HList;

		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from medication ORDER BY medname";
			FillList();
		}

		public static void RefreshGeneric(){
			cmd.CommandText =
				"SELECT * from medication WHERE medicationnum = genericnum ORDER BY medname";
			FillList();
		}

		private static void FillList(){
			FillTable();
			HList=new Hashtable();
			List=new Medication[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].MedicationNum=PIn.PInt   (table.Rows[i][0].ToString());
				List[i].MedName      =PIn.PString(table.Rows[i][1].ToString());
				List[i].GenericNum   =PIn.PInt   (table.Rows[i][2].ToString());
				List[i].Notes        =PIn.PString(table.Rows[i][3].ToString());
				HList.Add(List[i].MedicationNum,List[i]);
			}
		}

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE medication SET " 
				+ "medname = '"      +POut.PString(Cur.MedName)+"'"
				+ ",genericnum = '"  +POut.PInt   (Cur.GenericNum)+"'"
				+ ",notes = '"       +POut.PString(Cur.Notes)+"'"
				+" WHERE medicationnum = '" +POut.PInt   (Cur.MedicationNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO medication (medname,genericnum,notes"
				+") VALUES("
				+"'"+POut.PString(Cur.MedName)+"', "
				+"'"+POut.PInt   (Cur.GenericNum)+"', "
				+"'"+POut.PString(Cur.Notes)+"')";
			NonQ(true);
			Cur.MedicationNum=InsertID;
			//MessageBox.Show(Cur.PayNum.ToString());
		}

		public static void DeleteCur(){
			cmd.CommandText = "DELETE from medication WHERE medicationNum = '"+Cur.MedicationNum.ToString()+"'";
			NonQ(false);
		}
		
	}

	public struct Medication{
		public int MedicationNum;//primary key
		public string MedName;
		public int GenericNum;//(optional)foreign key to Medication.MedicationNum
		public string Notes;
	}

/*=========================================================================================
		=================================== class MedicationPats ==========================================*/

	public class MedicationPats:DataClass{
		public static MedicationPat Cur;
		public static MedicationPat[] List;//for current pat

		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from medicationpat WHERE patnum = '"+Patients.Cur.PatNum+"'";
			FillTable();
			List=new MedicationPat[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].MedicationPatNum=PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum          =PIn.PInt   (table.Rows[i][1].ToString());
				List[i].MedicationNum   =PIn.PInt   (table.Rows[i][2].ToString());
				List[i].PatNote         =PIn.PString(table.Rows[i][3].ToString());
				//HList.Add(List[i].MedicationNum,List[i]);
			}
		}

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE medicationpat SET " 
				+ "patnum = '"        +POut.PInt   (Cur.PatNum)+"'"
				+ ",medicationnum = '"+POut.PInt   (Cur.MedicationNum)+"'"
				+ ",patnote = '"      +POut.PString(Cur.PatNote)+"'"
				+" WHERE medicationpatnum = '" +POut.PInt   (Cur.MedicationPatNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO medicationpat (patnum,medicationnum,patnote"
				+") VALUES("
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PInt   (Cur.MedicationNum)+"', "
				+"'"+POut.PString(Cur.PatNote)+"')";
			NonQ(true);
			Cur.MedicationPatNum=InsertID;
			//MessageBox.Show(Cur.PayNum.ToString());
		}

		public static void DeleteCur(){
			cmd.CommandText = "DELETE from medicationpat WHERE medicationpatNum = '"
				+Cur.MedicationPatNum.ToString()+"'";
			NonQ(false);
		}
		
	}

	public struct MedicationPat{//table medicationpat.  links meds to pats.
		public int MedicationPatNum;//primary key
		public int PatNum;//foreign key to patient.PatNum
		public int MedicationNum;//foreign key to medication.MedicationNum
		public string PatNote;//medication notes specific to this patient
	}


	/*=========================================================================================
	=================================== class PIn ===========================================*/
	public class PIn{
		public static bool PBool (string myString){
			return myString=="1";
		}

		public static byte PByte (string myString){
			if(myString==""){
				return 0;
			}
			else{
				return System.Convert.ToByte(myString);
			}
		}

		public static DateTime PDate(string myString){
			if(myString=="")
				return DateTime.MinValue;
			try{
				return (DateTime.Parse(myString));
			}
			catch{
				return DateTime.MinValue;
			}
		}

		public static DateTime PDateT(string myString){
			if(myString=="")
				return DateTime.MinValue;
			try{
				return (DateTime.Parse(myString));
			}
			catch{
				return DateTime.MinValue;
			}
		}

		public static double PDouble (string myString){
			if (myString==""){
				return 0;
			}
			else{
				return System.Convert.ToDouble(myString);
			}

		}

		public static int PInt (string myString){
			if (myString==""){
				return 0;
			}
			else{
				//try{
				return System.Convert.ToInt32(myString);
				//}
				//catch{
				//	MessageBox.Show(myString);
				//	return 0;
				//}
			}
		}

		public static float PFloat(string myString){
			if(myString==""){
				return 0;
			}
			//try{
				return System.Convert.ToSingle(myString);
			//}
			//catch{
			//	return 0;
			//}
		}

		public static string PString (string myString){
			return myString;
		}
		
		public static string PTime (string myTime){
			return DateTime.Parse(myTime).ToString("HH:mm:ss");
		}

	}//end class PIn

	/*=========================================================================================
	=================================== class POut ===========================================*/

	public class POut{
		public static string PBool (bool myBool){
			if (myBool==true){
				return "1";
			}
			else{
				return "0";
			}
		}

		public static string PByte (byte myByte){
			return myByte.ToString();
		}

		public static string PDateT(DateTime myDateT){
			try{
				return myDateT.ToString("yyyy-MM-dd HH:mm:ss");
			}
			catch{
				return "";//this actually saves zero's to the database
			}
		}

		public static string PDate(DateTime myDate){
			try{
				return myDate.ToString("yyyy-MM-dd");
			}
			catch{
				//return "0000-00-00";
				return "";//this saves zeros to the database
			}
		}

		public static string PDouble (double myDouble){
			return myDouble.ToString();
		}

		public static string PInt (int myInt){
			return myInt.ToString();
		}

		public static string PFloat(float myFloat){
			return myFloat.ToString();
		}

		public static string PString (string myString){
			string newString="";
			if(myString==null){
				myString="";
			}
			for (int i=0; i<myString.Length; i++){
				switch (myString.Substring(i,1)){
					case "'": newString+=@"\'"; break;
					case @"\": newString+=@"\\"; break;//single \ replaced by \\
					case "\r": newString+=@"\r"; break;//carriage return(usually followed by new line)
					case "\n": newString+=@"\n"; break;//new line
					case "\t": newString+=@"\t"; break;//tab
						//case "%": newString+="\\%"; break;//causes errors because only ambiguous in LIKE clause
						//case "_": newString+="\\_"; break;//see above
					default : newString+=myString.Substring(i,1); break;
				}//end switch
			}//end for
			return newString;
		}

		public static string PTime (string myTime){
			return DateTime.Parse(myTime).ToString("HH:mm:ss");
		}

	}//end class POut

	/*=========================================================================================
	=================================== class Patients ===========================================*/

	public class Patients:DataClass{
		public static bool PatIsLoaded=false;
		public static Patient Cur;
		public static Patient Lim;
		public static string LimName;
		public static Patient[] FamilyList;
		public static Patient[] PtList;
		public static PatAging[] AgingList;
		public static int GuarIndex;
		public static RecallItem[] RecallList;
		public static Hashtable HList;

		public static void GetFamily(int patNum){
			cmd.CommandText= 
				"SELECT guarantor FROM patient "
				+"WHERE patnum = '"+POut.PInt(patNum)+"'";
			FillTable();
			if(table.Rows.Count==0){
				PatIsLoaded=false;
				Cur=new Patient();
				FamilyList=new Patient[1];
				FamilyList[0]=Cur;
				GuarIndex=0;
				return;
			}
			cmd.CommandText= 
				"SELECT * "
				+"FROM patient "
				+"WHERE guarantor = '"+table.Rows[0][0].ToString()+"'"
				+" ORDER BY (patnum!=guarantor), birthdate";
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			FamilyList = new Patient[table.Rows.Count];
			//MessageBox.Show(table.Rows.Count.ToString());
			for (int i=0;i<table.Rows.Count;i++){
				FamilyList[i].PatNum       = PIn.PInt   (table.Rows[i][0].ToString());
				FamilyList[i].LName        = PIn.PString(table.Rows[i][1].ToString());
				FamilyList[i].FName        = PIn.PString(table.Rows[i][2].ToString());
				FamilyList[i].MiddleI      = PIn.PString(table.Rows[i][3].ToString());
				FamilyList[i].Preferred    = PIn.PString(table.Rows[i][4].ToString());
				FamilyList[i].PatStatus    = (PatientStatus)PIn.PInt   (table.Rows[i][5].ToString());
				FamilyList[i].Gender       = (PatientGender)PIn.PInt   (table.Rows[i][6].ToString());
				FamilyList[i].Position     = (PatientPosition)PIn.PInt   (table.Rows[i][7].ToString());
				FamilyList[i].Birthdate    = PIn.PDate  (table.Rows[i][8].ToString());
				FamilyList[i].Age=Shared.DateToAge(FamilyList[i].Birthdate);
				FamilyList[i].SSN          = PIn.PString(table.Rows[i][9].ToString());
				FamilyList[i].Address      = PIn.PString(table.Rows[i][10].ToString());
				FamilyList[i].Address2     = PIn.PString(table.Rows[i][11].ToString());
				FamilyList[i].City         = PIn.PString(table.Rows[i][12].ToString());
				FamilyList[i].State        = PIn.PString(table.Rows[i][13].ToString());
				FamilyList[i].Zip          = PIn.PString(table.Rows[i][14].ToString());
				FamilyList[i].HmPhone      = PIn.PString(table.Rows[i][15].ToString());
				FamilyList[i].WkPhone      = PIn.PString(table.Rows[i][16].ToString());
				FamilyList[i].WirelessPhone= PIn.PString(table.Rows[i][17].ToString());
				FamilyList[i].Guarantor    = PIn.PInt   (table.Rows[i][18].ToString());
				FamilyList[i].CreditType   = PIn.PString(table.Rows[i][19].ToString());
				FamilyList[i].Email        = PIn.PString(table.Rows[i][20].ToString());
				FamilyList[i].Salutation   = PIn.PString(table.Rows[i][21].ToString());
				FamilyList[i].PriPlanNum   = PIn.PInt   (table.Rows[i][22].ToString());
				FamilyList[i].PriRelationship=(Relat)PIn.PInt(table.Rows[i][23].ToString());
				FamilyList[i].SecPlanNum   = PIn.PInt   (table.Rows[i][24].ToString());
				FamilyList[i].SecRelationship=(Relat)PIn.PInt(table.Rows[i][25].ToString());
				FamilyList[i].EstBalance   = PIn.PDouble(table.Rows[i][26].ToString());
				FamilyList[i].NextAptNum   = PIn.PInt   (table.Rows[i][27].ToString());
				FamilyList[i].PriProv      = PIn.PInt   (table.Rows[i][28].ToString());
				FamilyList[i].SecProv      = PIn.PInt   (table.Rows[i][29].ToString());
				FamilyList[i].FeeSched     = PIn.PInt   (table.Rows[i][30].ToString());
				FamilyList[i].BillingType  = PIn.PInt   (table.Rows[i][31].ToString());
				FamilyList[i].RecallInterval= PIn.PInt   (table.Rows[i][32].ToString());
				FamilyList[i].RecallStatus = PIn.PInt   (table.Rows[i][33].ToString());
				FamilyList[i].ImageFolder  = PIn.PString(table.Rows[i][34].ToString());
				FamilyList[i].AddrNote     = PIn.PString(table.Rows[i][35].ToString());
				FamilyList[i].FamFinUrgNote= PIn.PString(table.Rows[i][36].ToString());
				FamilyList[i].MedUrgNote   = PIn.PString(table.Rows[i][37].ToString());
				FamilyList[i].ApptModNote  = PIn.PString(table.Rows[i][38].ToString());
				FamilyList[i].StudentStatus= PIn.PString(table.Rows[i][39].ToString());
				FamilyList[i].SchoolName   = PIn.PString(table.Rows[i][40].ToString());
				FamilyList[i].ChartNumber  = PIn.PString(table.Rows[i][41].ToString());
				FamilyList[i].MedicaidID   = PIn.PString(table.Rows[i][42].ToString());
				FamilyList[i].Bal_0_30     = PIn.PDouble(table.Rows[i][43].ToString());
				FamilyList[i].Bal_31_60    = PIn.PDouble(table.Rows[i][44].ToString());
				FamilyList[i].Bal_61_90    = PIn.PDouble(table.Rows[i][45].ToString());
				FamilyList[i].BalOver90    = PIn.PDouble(table.Rows[i][46].ToString());
				FamilyList[i].InsEst       = PIn.PDouble(table.Rows[i][47].ToString());
				FamilyList[i].PrimaryTeeth = PIn.PString(table.Rows[i][48].ToString());
				FamilyList[i].BalTotal     = PIn.PDouble(table.Rows[i][49].ToString());
				if(FamilyList[i].PatNum==patNum)
					Cur=FamilyList[i];
				if (FamilyList[i].Guarantor==FamilyList[i].PatNum)
					GuarIndex=i;
			}
			PatIsLoaded=true;
			//InfoChanged=false;//unused?
		}

		public static void InsertCur(){//ONLY for new patients
			cmd.CommandText = "INSERT INTO patient (lname,fname,middlei,preferred,patstatus,gender,"
				+"position,birthdate,ssn,address,address2,city,state,zip,hmphone,wkphone,wirelessphone,"
				+"guarantor,credittype,email,salutation,priplannum,prirelationship,secplannum,"
				+"secrelationship,estbalance,nextaptnum,priprov,secprov,feesched,billingtype,recallinterval,"
				+"recallstatus,imagefolder,addrnote,famfinurgnote,medurgnote,apptmodnote,"
				+"studentstatus,schoolname,chartnumber,medicaidid"
				+",Bal_0_30,Bal_31_60,Bal_61_90,BalOver90,insest,primaryteeth,BalTotal) VALUES("
				+"'"+POut.PString(Cur.LName)+"', "
				+"'"+POut.PString(Cur.FName)+"', "
				+"'"+POut.PString(Cur.MiddleI)+"', "
				+"'"+POut.PString(Cur.Preferred)+"', "
				+"'"+POut.PInt   ((int)Cur.PatStatus)+"', "
				+"'"+POut.PInt   ((int)Cur.Gender)+"', "
				+"'"+POut.PInt   ((int)Cur.Position)+"', "
				+"'"+POut.PDate  (Cur.Birthdate)+"', "
				+"'"+POut.PString(Cur.SSN)+"', "
				+"'"+POut.PString(Cur.Address)+"', "
				+"'"+POut.PString(Cur.Address2)+"', "
				+"'"+POut.PString(Cur.City)+"', "
				+"'"+POut.PString(Cur.State)+"', "
				+"'"+POut.PString(Cur.Zip)+"', "
				+"'"+POut.PString(Cur.HmPhone)+"', "
				+"'"+POut.PString(Cur.WkPhone)+"', "
				+"'"+POut.PString(Cur.WirelessPhone)+"', "
				+"'"+POut.PInt   (Cur.Guarantor)+"', "
				+"'"+POut.PString(Cur.CreditType)+"', "
				+"'"+POut.PString(Cur.Email)+"', "
				+"'"+POut.PString(Cur.Salutation)+"', "
				+"'"+POut.PInt   (Cur.PriPlanNum)+"', "
				+"'"+POut.PInt   ((int)Cur.PriRelationship)+"', "
				+"'"+POut.PInt   (Cur.SecPlanNum)+"', "
				+"'"+POut.PInt   ((int)Cur.SecRelationship)+"', "
				+"'"+POut.PDouble(Cur.EstBalance)+"', "
				+"'"+POut.PInt   (Cur.NextAptNum)+"', "
				+"'"+POut.PInt   (Cur.PriProv)+"', "
				+"'"+POut.PInt   (Cur.SecProv)+"', "
				+"'"+POut.PInt   (Cur.FeeSched)+"', "
				+"'"+POut.PInt   (Cur.BillingType)+"', "
				+"'"+POut.PInt   (Cur.RecallInterval)+"', "
				+"'"+POut.PInt   (Cur.RecallStatus)+"', "
				+"'"+POut.PString(Cur.ImageFolder)+"', "
				+"'"+POut.PString(Cur.AddrNote)+"', "
				+"'"+POut.PString(Cur.FamFinUrgNote)+"', "
				+"'"+POut.PString(Cur.MedUrgNote)+"', "
				+"'"+POut.PString(Cur.ApptModNote)+"', "
				+"'"+POut.PString(Cur.StudentStatus)+"', "
				+"'"+POut.PString(Cur.SchoolName)+"', "
				+"'"+POut.PString(Cur.ChartNumber)+"', "
				+"'"+POut.PString(Cur.MedicaidID)+"', "
				+"'"+POut.PDouble(Cur.Bal_0_30)+"', "
				+"'"+POut.PDouble(Cur.Bal_31_60)+"', "
				+"'"+POut.PDouble(Cur.Bal_61_90)+"', "
				+"'"+POut.PDouble(Cur.BalOver90)+"', "
				+"'"+POut.PDouble(Cur.InsEst)+"', "
				+"'"+POut.PString(Cur.PrimaryTeeth)+"', "
				+"'"+POut.PDouble(Cur.BalTotal)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.PatNum=InsertID;
			//PatientNotes PatientNotes=new PatientNotes();
			//PatientNotes.SaveCur();
		}

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE patient SET " 
				+ "LName = '"     +POut.PString(Cur.LName)+"'"
				+",FName = '"     +POut.PString(Cur.FName)+"'"
				+",MiddleI = '"   +POut.PString(Cur.MiddleI)+"'"
				+",Preferred = '" +POut.PString(Cur.Preferred)+"'"
				+",patStatus = '" +POut.PInt   ((int)Cur.PatStatus)+"'"
				+",Gender = '"    +POut.PInt   ((int)Cur.Gender)+"'"
				+",Position = '"  +POut.PInt   ((int)Cur.Position)+"'"
				+",Birthdate = '" +POut.PDate  (Cur.Birthdate)+"'" 
				+",SSN = '"       +POut.PString(Cur.SSN)+"'"
				+",Address = '"   +POut.PString(Cur.Address)+"'"
				+",Address2 = '"  +POut.PString(Cur.Address2)+"'"
				+",City = '"      +POut.PString(Cur.City)+"'"
				+",State = '"     +POut.PString(Cur.State)+"'"
				+",Zip = '"       +POut.PString(Cur.Zip)+"'"
				+",HmPhone = '"   +POut.PString(Cur.HmPhone)+"'"
				+",WkPhone = '"   +POut.PString(Cur.WkPhone)+"'"
				+",WirelessPhone='"    +POut.PString(Cur.WirelessPhone)+"'"
				+",guarantor = '"      +POut.PInt   (Cur.Guarantor)+"'"
				+",credittype = '"     +POut.PString(Cur.CreditType)+"'"
				+",Email = '"          +POut.PString(Cur.Email)+"'"
				+",Salutation = '"     +POut.PString(Cur.Salutation)+"'"
				+",priplannum = '"     +POut.PInt   (Cur.PriPlanNum)+"'"
				+",prirelationship = '"+POut.PInt((int)Cur.PriRelationship)+"'"
				+",secplannum = '"     +POut.PInt   (Cur.SecPlanNum)+"'"
				+",secrelationship = '"+POut.PInt((int)Cur.SecRelationship)+"'"
				+",estbalance = '"     +POut.PDouble(Cur.EstBalance)+"'"
				+",nextaptnum = '"     +POut.PInt   (Cur.NextAptNum)+"'"
				+",priprov = '"        +POut.PInt   (Cur.PriProv)+"'"
				+",secprov = '"        +POut.PInt   (Cur.SecProv)+"'"
				+",feesched = '"       +POut.PInt   (Cur.FeeSched)+"'"
				+",billingtype = '"    +POut.PInt   (Cur.BillingType)+"'"
				+",recallinterval = '" +POut.PInt   (Cur.RecallInterval)+"'"
				+",recallstatus = '"   +POut.PInt   (Cur.RecallStatus)+"'"
				+",imagefolder = '"    +POut.PString(Cur.ImageFolder)+"'"
				+",addrnote = '"       +POut.PString(Cur.AddrNote)+"'"
				+",famfinurgnote = '"  +POut.PString(Cur.FamFinUrgNote)+"'"
				+",medurgnote = '"     +POut.PString(Cur.MedUrgNote)+"'"
				+",apptmodnote = '"    +POut.PString(Cur.ApptModNote)+"'"
				+",studentstatus = '"  +POut.PString(Cur.StudentStatus)+"'"
				+",schoolname = '"     +POut.PString(Cur.SchoolName)+"'"
				+",chartnumber = '"    +POut.PString(Cur.ChartNumber)+"'"
				+",medicaidid = '"     +POut.PString(Cur.MedicaidID)+"'"
				+",Bal_0_30 = '"       +POut.PDouble(Cur.Bal_0_30)+"'"
				+",Bal_31_60 = '"      +POut.PDouble(Cur.Bal_31_60)+"'"
				+",Bal_61_90 = '"      +POut.PDouble(Cur.Bal_61_90)+"'"
				+",BalOver90 = '"      +POut.PDouble(Cur.BalOver90)+"'"
				+",insest    = '"      +POut.PDouble(Cur.InsEst)+"'"
				+",primaryteeth = '"   +POut.PString(Cur.PrimaryTeeth)+"'"
				+",BalTotal = '"       +POut.PDouble(Cur.BalTotal)+"'"
				+" WHERE PatNum = '"   +POut.PInt   (Cur.PatNum)+"'";
			NonQ(false);
			//MessageBox.Show(cmd.CommandText);
		}//end UpdatePatient

		public static bool GetPtList(string lname,string fname,string hmphone,string address,bool hideInactive){
			//string psearchStr = POut.PString(searchStr)+"%";
			//Only used for the Select Patient dialog
			bool retval=false;
			cmd.CommandText = 
				"SELECT patnum,lname,fname,middlei,preferred,birthdate,ssn,hmphone,address,patstatus,billingtype "
				+"FROM patient "
				+"WHERE patstatus != '4' "//not status 'deleted'
				+"AND lname LIKE '"+POut.PString(lname)+"%' "
				+"AND fname LIKE '"+POut.PString(fname)+"%' "
				+"AND hmphone LIKE '"+POut.PString(hmphone)+"%' "
				+"AND address LIKE '"+POut.PString(address)+"%' ";
			if(hideInactive){
				cmd.CommandText+="AND patstatus = '0' ";
			}
				cmd.CommandText+="ORDER BY lname,fname LIMIT 43";//only need 42, but the extra will help indicate more rows
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			if(table.Rows.Count==43){
				retval=true;
				PtList=new Patient[42];
			}
			else
				PtList=new Patient[table.Rows.Count];
			for(int i=0;i<table.Rows.Count && i<42;i++){
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
			}
			return retval;//if true, there are more rows.
		}

		public static void GetRecallList(){
			cmd.CommandText = 
				"SELECT MAX(procedurelog.procdate) AS 'LastDate', "
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
				/* for future debugging:
SELECT MAX(procedurelog.procdate) + INTERVAL patient.recallinterval MONTH AS 'DueDate',
CONCAT(patient.lname,', ',patient.fname,' ',patient.preferred,' ',
patient.middlei) AS 'Patient Name',
patient.birthdate,patient.recallinterval,patient.recallstatus,patient.patnum,
patient.nextaptnum,appointment.aptdatetime
FROM patient,procedurelog,procedurecode
LEFT JOIN appointment ON appointment.nextaptnum = patient.nextaptnum
WHERE patient.patnum = procedurelog.patnum
AND procedurecode.adacode = procedurelog.adacode 
AND procedurecode.setrecall = 1
AND (procedurelog.procstatus = 2
OR procedurelog.procstatus = 3
OR procedurelog.procstatus = 4)
AND patient.patstatus = 0
GROUP BY patient.patnum
ORDER BY DueDate
			*/
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			RecallList = new RecallItem[table.Rows.Count];
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
		}

		public static string GetNameInFamLF(int myPatNum){
			string retStr="";
			for(int i=0;i<FamilyList.Length;i++){
				if(FamilyList[i].PatNum==myPatNum){
					if(FamilyList[i].Preferred==""){
						retStr=FamilyList[i].LName+", "+FamilyList[i].FName+" "+FamilyList[i].MiddleI; 
					}
					else{
						retStr=FamilyList[i].LName+", '"+FamilyList[i].Preferred+"' "+FamilyList[i].FName+" "+FamilyList[i].MiddleI;
					}
				}
			}
			return retStr;
		}

		public static string GetNameInFamLFI(int myi){
			string retStr="";
			if(FamilyList[myi].Preferred==""){
				retStr=FamilyList[myi].LName+", "+FamilyList[myi].FName+" "+FamilyList[myi].MiddleI; 
			}
			else{
				retStr=FamilyList[myi].LName+", '"+FamilyList[myi].Preferred+"' "+FamilyList[myi].FName+" "+FamilyList[myi].MiddleI;
			}
			return retStr;
		}

		public static string GetNameInFamFL(int myPatNum){
			string retStr="";
			for(int i=0;i<FamilyList.Length;i++){
				if(FamilyList[i].PatNum==myPatNum){
					if(FamilyList[i].Preferred==""){
						retStr=FamilyList[i].FName+" "+FamilyList[i].MiddleI+" "+FamilyList[i].LName; 
					}
					else{
						retStr="'"+FamilyList[i].Preferred+"' "+FamilyList[i].FName+" "+FamilyList[i].MiddleI+" "+FamilyList[i].LName;
					}
				}
			}
			return retStr;
		}

		public static string GetNameInFamFLI(int myi){
			string retStr="";
			if(FamilyList[myi].Preferred==""){
				retStr=FamilyList[myi].FName+" "+FamilyList[myi].MiddleI+" "+FamilyList[myi].LName; 
			}
			else{
				retStr="'"+FamilyList[myi].Preferred+"' "+FamilyList[myi].FName+" "+FamilyList[myi].MiddleI+" "+FamilyList[myi].LName;
			}
			return retStr;
		}

		public static void GetLim(int patNum){
			if(patNum==0){
				Lim=new Patient();
				LimName="";
				return;
			}
			cmd.CommandText = 
				"SELECT PatNum,lname,fname,middlei,preferred,credittype,guarantor,priplannum,SSN " 
				+"FROM patient "
				+"WHERE PatNum = '"+patNum.ToString()+"'";
			FillTable();
			if(table.Rows.Count==0){
				Lim=new Patient();
				LimName="";
				return;
			}
			Lim.PatNum     = PIn.PInt   (table.Rows[0][0].ToString());
			Lim.LName      = PIn.PString(table.Rows[0][1].ToString());
			Lim.FName      = PIn.PString(table.Rows[0][2].ToString());
			Lim.MiddleI    = PIn.PString(table.Rows[0][3].ToString());
			Lim.Preferred  = PIn.PString(table.Rows[0][4].ToString());
			Lim.CreditType = PIn.PString(table.Rows[0][5].ToString());
			Lim.Guarantor  = PIn.PInt   (table.Rows[0][6].ToString());
			Lim.PriPlanNum = PIn.PInt   (table.Rows[0][7].ToString());
			Lim.SSN        = PIn.PString(table.Rows[0][8].ToString());
			if (Lim.Preferred=="")
				LimName=Lim.LName+", "+Lim.FName+" "+Lim.MiddleI;
			else LimName=Lim.LName+", '"+Lim.Preferred+"' "+Lim.FName+" "+Lim.MiddleI;
		}

		public static string GetCurNameLF(){
			if(Cur.Preferred=="")
				return Cur.LName+", "+Cur.FName+" "+Cur.MiddleI;
			else
				return Cur.LName+", '"+Cur.Preferred+"' "+Cur.FName+" "+Cur.MiddleI;
		}

		public static string GetLimCreditIns(){
			string retStr="";
			if(Lim.CreditType=="")
				retStr+=" ";
			else retStr+=Lim.CreditType;
			if(Lim.PriPlanNum==0)
				retStr+=" ";
			else retStr+="I";
			return retStr;
		}

		public static string GetCreditIns(){
			string retStr="";
			if(Cur.CreditType=="")
				retStr+=" ";
			else retStr+=Cur.CreditType;
			if(Cur.PriPlanNum==0)
				retStr+=" ";
			else retStr+="I";	
			return retStr;
		}

		public static int GetIndex(int patNum){
			int retVal=-1;//will return -1 if not found
			for(int i=0;i<FamilyList.Length;i++){
				if(FamilyList[i].PatNum==patNum){
					retVal=i;
				}
			}
			return retVal;
		}

		//public static void DeleteCur(){
		//this does not actually delete the patient, just changes status
		//and they will never show up on the selection list again
			
		//NonQ(false);
		//MessageBox.Show("Patient Deleted");
		//}

		public static void ChangeGuarantorToCur(){
			//Move famfinurgnote to current patient:
			cmd.CommandText = 
				"UPDATE patient SET "
				//+"famaddrnote = '"  +FamilyList[GuarIndex].FamAddrNote+"', "
				+"famfinurgnote = '"+FamilyList[GuarIndex].FamFinUrgNote+"' "
				+"WHERE patnum = '"+Cur.PatNum.ToString()+"'";
			NonQ(false);
			cmd.CommandText = 
				"UPDATE patient SET "
				//+"famaddrnote = '', "
				+"famfinurgnote = '' "
				+"WHERE patnum = '"+Cur.Guarantor.ToString()+"'";
			NonQ(false);
			//Move family financial note to current patient:
			cmd.CommandText="SELECT FamFinancial FROM patientnote "
				+"WHERE patnum = '"+Cur.Guarantor.ToString()+"'";
			FillTable();
			if(table.Rows.Count==1){
				cmd.CommandText = 
					"UPDATE patientnote SET "
					+"famfinancial = '"+table.Rows[0][0].ToString()+"' "
					+"WHERE patnum = '"+Cur.PatNum.ToString()+"'";
				NonQ(false);
			}
			cmd.CommandText = 
				"UPDATE patientnote SET "
				+"famfinancial = ''"
				+"WHERE patnum = '"+Cur.Guarantor.ToString()+"'";
			NonQ(false);
			//change guarantor of all family members:
			cmd.CommandText = 
				"UPDATE patient SET "
				+"guarantor = '"+Cur.PatNum.ToString()+"' "
				+"WHERE guarantor = '"+Cur.Guarantor.ToString()+"'";
			NonQ(false);
		}
		
		public static void CombineGuarantors(){
			//concat cur notes with guarantor notes
			cmd.CommandText = 
				"UPDATE patient SET "
				//+"addrnote = '"+POut.PString(FamilyList[GuarIndex].FamAddrNote)
				//									+POut.PString(Cur.FamAddrNote)+"', "
				+"famfinurgnote = '"+POut.PString(FamilyList[GuarIndex].FamFinUrgNote)
				+POut.PString(Cur.FamFinUrgNote)+"' "
				+"WHERE patnum = '"+Cur.Guarantor.ToString()+"'";
			NonQ(false);
			//delete cur notes
			cmd.CommandText = 
				"UPDATE patient SET "
				//+"famaddrnote = '', "
				+"famfinurgnote = '' "
				+"WHERE patnum = '"+Cur.PatNum+"'";
			NonQ(false);
			//concat family financial notes
			PatientNotes PatientNotes=new PatientNotes();
			PatientNotes.Refresh();
			//patientnote table must have been refreshed for this to work.
			//Makes sure there are entries for patient and for guarantor.
			//Also, PatientNotes.Cur.FamFinancial will now have the guar info in it.
			string strGuar=PatientNotes.Cur.FamFinancial;
			cmd.CommandText = 
				"SELECT famfinancial "
				+"FROM patientnote WHERE patnum ='"+POut.PInt(Patients.Cur.PatNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			string strCur=PIn.PString(table.Rows[0][0].ToString());
			cmd.CommandText = 
				"UPDATE patientnote SET "
				+"famfinancial = '"+strGuar+strCur+"' "
				+"WHERE patnum = '"+Cur.Guarantor.ToString()+"'";
			NonQ(false);
			//delete cur financial notes
			cmd.CommandText = 
				"UPDATE patientnote SET "
				+"famfinancial = ''"
				+"WHERE patnum = '"+Cur.PatNum.ToString()+"'";
			NonQ(false);
		}

		public static void GetHList(){
			cmd.CommandText="SELECT patnum,lname,fname,middlei,preferred "
				+"FROM patient";
			FillTable();
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

		public static void UpdateAddressForFam(){
			cmd.CommandText = "UPDATE patient SET " 
				+"Address = '"    +POut.PString(Cur.Address)+"'"
				+",Address2 = '"   +POut.PString(Cur.Address2)+"'"
				+",City = '"       +POut.PString(Cur.City)+"'"
				+",State = '"      +POut.PString(Cur.State)+"'"
				+",Zip = '"        +POut.PString(Cur.Zip)+"'"
				+",HmPhone = '"    +POut.PString(Cur.HmPhone)+"'"
				+",credittype = '" +POut.PString(Cur.CreditType)+"'"
				+",priprov = '"    +POut.PInt   (Cur.PriProv)+"'"
				+",secprov = '"    +POut.PInt   (Cur.SecProv)+"'"
				+",feesched = '"   +POut.PInt   (Cur.FeeSched)+"'"
				+",billingtype = '"+POut.PInt   (Cur.BillingType)+"'"
				+" WHERE guarantor = '"+POut.PDouble(Cur.Guarantor)+"'";
			NonQ(false);
			//MessageBox.Show(cmd.CommandText);
		}

		public static void UpdateNotesForFam(){
			cmd.CommandText = "UPDATE patient SET " 
				+"addrnote = '"   +POut.PString(Cur.AddrNote)+"'"
				+" WHERE guarantor = '"+POut.PDouble(Cur.Guarantor)+"'";
			NonQ(false);
			//MessageBox.Show(cmd.CommandText);
		}

		public static void GetAgingList(string age,int[] billingIndices,bool excludeAddr
			,bool excludeNeg,double excludeLessThan){
			//This is only used in the Billing dialog
			cmd.CommandText =
				"SELECT patnum,Bal_0_30,Bal_31_60,Bal_61_90,BalOver90,BalTotal,InsEst,LName,FName,MiddleI "
				+"FROM patient "//actually only gets guarantors since others are 0.
				+" WHERE (BalTotal - InsEst > '"
				+excludeLessThan.ToString()+"'";
			if(!excludeNeg){
				cmd.CommandText+=" || BalTotal - InsEst < '0')";
			}
			else{
				cmd.CommandText+=")";
			}
			switch(age){
				//where is age 0. Is it missing because no restriction
				case "30":
					cmd.CommandText+=" && (Bal_31_60 > '0' || Bal_61_90 > '0' || BalOver90 > '0')";
					break;
				case "60":
					cmd.CommandText+=" && (Bal_61_90 > '0' || BalOver90 > '0')";
					break;
				case "90":
					cmd.CommandText+=" && (BalOver90 > '0')";
					break;
			}
			for(int i=0;i<billingIndices.Length;i++){
				if(i==0){
					cmd.CommandText+=" && (billingtype = '";
				}
				else{
					cmd.CommandText+=" || billingtype = '";
				}
				cmd.CommandText+=
					Defs.Short[(int)DefCat.BillingTypes][billingIndices[i]].DefNum.ToString()+"'";
			}
			cmd.CommandText+=")";
			if(excludeAddr){
				cmd.CommandText+=" && (zip !='')";
			}	
			cmd.CommandText+=" ORDER BY LName,FName";
			FillTable();
			AgingList=new PatAging[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
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
		}

		public static void GetAgingList(){
			//used only to run finance charges, so it ignores negative balances
			cmd.CommandText =
				"SELECT patnum,Bal_0_30,Bal_31_60,Bal_61_90,BalOver90,BalTotal,InsEst,LName,FName,MiddleI,priprov "
				+"FROM patient "//actually only gets guarantors since others are 0.
				+" WHERE Bal_0_30 + Bal_31_60 + Bal_61_90 + BalOver90 - InsEst > '0.005'"//more that 1/2 cent
				+" ORDER BY LName,FName";
			FillTable();
			AgingList=new PatAging[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
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
		}

		public static void ResetAging(){//for entire database
			//need to zero everything out first because the update aging only inserts non-zero values
			cmd.CommandText="Update patient SET "
				+"Bal_0_30   = '0'"
				+",Bal_31_60 = '0'"
				+",Bal_61_90 = '0'"
				+",BalOver90 = '0'"
				+",InsEst    = '0'"
				+",BalTotal  = '0'";
			NonQ(false);		
		}

		public static void ResetAging(int guarantor){
			cmd.CommandText="Update patient SET "
				+"Bal_0_30   = '0'"
				+",Bal_31_60 = '0'"
				+",Bal_61_90 = '0'"
				+",BalOver90 = '0'"
				+",InsEst    = '0'"
				+",BalTotal  = '0'"
			  +" WHERE guarantor = '"+POut.PInt(guarantor)+"'";
			NonQ(false);
		}

		public static void UpdateAging(int patnum,double Bal0,double Bal31
			,double Bal61,double Bal91,double InsEst,double BalTotal){
			cmd.CommandText="Update patient SET "
				+"Bal_0_30        = '" +POut.PDouble(Bal0)+"'"
				+",Bal_31_60      = '" +POut.PDouble(Bal31)+"'"
				+",Bal_61_90      = '" +POut.PDouble(Bal61)+"'"
				+",BalOver90      = '" +POut.PDouble(Bal91)+"'"
				+",InsEst         = '" +POut.PDouble(InsEst)+"'"
				+",BalTotal       = '" +POut.PDouble(BalTotal)+"'"
				+" WHERE patnum   = '" +POut.PInt   (patnum)+"'";
			NonQ(false);
		}

		public static int GetProvForCur(){
			if(Cur.PriProv!=0)
				return Cur.PriProv;
			if(PIn.PInt(((Pref)Prefs.HList["PracticeDefaultProv"]).ValueString)==0){
				MessageBox.Show(Lan.g("Patients","Please set a default provider in the practice setup window."));
				return Providers.List[0].ProvNum;
			}
			return PIn.PInt(((Pref)Prefs.HList["PracticeDefaultProv"]).ValueString);
		}

		public static string GetNextChartNum(){
			//could later add a where clause based on preferred format
			cmd.CommandText="SELECT chartnumber from patient WHERE chartnumber != ''"
				+" ORDER BY chartnumber DESC LIMIT 1";
			FillTable();
			if(table.Rows.Count==0){//no existing chart numbers
				return "1";
			}
			string lastChartNum=PIn.PString(table.Rows[0][0].ToString());
			//or could add more match conditions
			if(Regex.IsMatch(lastChartNum,@"^\d+$")){//if is an integer
				return(PIn.PInt(lastChartNum)+1).ToString();
			}
			return "1";//if there are no integer chartnumbers yet
		}

	}

	public struct Patient{
		public int    PatNum;//primary key
		public string LName;//last name
		public string FName;//first name
		public string MiddleI;//middle initial
		public string Preferred;//preferred name
		public PatientStatus PatStatus;//enum PatientStatus{Patient=0,NonPatient=1,Inactive=2,Archived=3,Deleted=4}
		public PatientGender Gender;//enum PatientGender{Male=0,Female=1,Unknown=2}
		public PatientPosition Position;//enum PatientPosition{Single=0,Married=1,Child=2}
		public DateTime Birthdate;
		public string SSN;//9 digits, no dashes
		public string Address;
		public string Address2;
		public string City;
		public string State;//2 Char
		public string Zip;
		public string HmPhone;//includes any punctuation
		public string WkPhone;
		public string WirelessPhone;
		public int    Guarantor;//foreign key to Patient.PatNum.  Head of household.
		public string Age;//derived from Birthdate.  Not in database table
		public string CreditType;//single char. Shows in appointment book.
		public string Email;
		public string Salutation;
		public int PriPlanNum;//foreign key to InsPlan.PlanNum.  Primary insurance.
		public Relat PriRelationship;//Relationship to subscriber for primary insurance.
		//enum Relat{Self=0,Spouse=1,Child=2,Employee=3,HandicapDep=4,SignifOther=5,InjuredPlaintiff=6,
		//LifePartner=7,Dependent=8}
		public int SecPlanNum;//foreign key to InsPlan.PlanNum.  Secondary insurance.
		public Relat SecRelationship;//Relationship to subscriber for secondary insurance.
		public double EstBalance;//Current patient balance.(not family)
		public int NextAptNum;//may be 0(none) or -1(done), otherwise it is the foreign key
		//to Appointment.AptNum.  This is the appointment that will show in the Chart module.
		//It will never show in the Appointments module.
		public int PriProv;//foreign key to Provider.ProvNum.  The patient's primary provider.
		public int SecProv;//foreign key to Provider.ProvNum.  Secondary provider.
		public int FeeSched;//foreign key to Definition.DefNum.  Default fee schedule.
		public int BillingType;//foreign key to Definition.DefNum.
		public int RecallInterval;//months between recalls.
		public int RecallStatus;//foreign key to Definition.DefNum, or 0 for none.
		public string ImageFolder;//name of folder where images will be stored. Not editable for now.
		public string AddrNote;
		public string FamFinUrgNote;//Family financial urgent note.  Only stored with guarantor, and shared for family.
		public string MedUrgNote;//Individual patient note for Urgent medical.
		public string ApptModNote;//Individual patient note for Appointment module note.
		public string StudentStatus;//single char for Nonstudent, Parttime, or Fulltime.  Blank=Nonstudent
		public string SchoolName;
		public string ChartNumber;//max 15 char.  Used for reference to previous programs.
		public string MedicaidID;
		public double Bal_0_30;//aging numbers are for entire family.  Only stored with guarantor.
		public double Bal_31_60;
		public double Bal_61_90;
		public double BalOver90;
		public double InsEst;//Insurance Estimate for entire family.
		public string PrimaryTeeth;//Teeth to display in chart as primary. eg: "1,2,3,4,5,12,13"
		public double BalTotal;//for entire family. Stored with guarantor
		
	}//end struct Patient

	public struct PatAging{//not a database table.  Just used for running reports.
		public int PatNum;
		public double Bal_0_30;
		public double Bal_31_60;
		public double Bal_61_90;
		public double BalOver90;
		public double InsEst;
		public string PatName;
		public double BalTotal;
		public double AmountDue;
		public int PriProv;//the patient priprov to assign the finance charge to.
	}

	/*=========================================================================================
		=================================== class PatientNotes ===========================================*/
	public class PatientNotes:DataClass{
		public static PatientNote Cur;
		
		public static void Refresh(){
			cmd.CommandText = 
				"SELECT * FROM patientnote WHERE patnum = '"+POut.PInt(Patients.Cur.PatNum)+"'";
			FillTable();
			if(table.Rows.Count==0){
				InsertRow(Patients.Cur.PatNum);
			}
			cmd.CommandText = 
				"SELECT patnum,apptphone,medical,service,medicalcomp "
				+"FROM patientnote WHERE patnum ='"+POut.PInt(Patients.Cur.PatNum)+"'";
			FillTable();
			Cur.PatNum      = PIn.PInt   (table.Rows[0][0].ToString());
			Cur.ApptPhone   = PIn.PString(table.Rows[0][1].ToString());
			Cur.Medical     = PIn.PString(table.Rows[0][2].ToString());
			Cur.Service     = PIn.PString(table.Rows[0][3].ToString());
			Cur.MedicalComp = PIn.PString(table.Rows[0][4].ToString());
			//fam note:
			cmd.CommandText = 
				"SELECT * FROM patientnote WHERE patnum ='"+POut.PInt(Patients.Cur.Guarantor)+"'";
			FillTable();
			if(table.Rows.Count==0){
				InsertRow(Patients.Cur.Guarantor);
			}
			cmd.CommandText = 
				"SELECT famfinancial "
				+"FROM patientnote WHERE patnum ='"+POut.PInt(Patients.Cur.Guarantor)+"'";
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			Cur.FamFinancial= PIn.PString(table.Rows[0][0].ToString());
		}

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE patientnote SET "
				+ "apptphone = '"   +POut.PString(Cur.ApptPhone)+"'"
				+ ",medical = '"    +POut.PString(Cur.Medical)+"'"
				+ ",service = '"    +POut.PString(Cur.Service)+"'"
				+ ",medicalcomp = '"+POut.PString(Cur.MedicalComp)+"'"
				+" WHERE patnum = '"+POut.PInt   (Cur.PatNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
			cmd.CommandText = "UPDATE patientnote SET "
				+ "famfinancial = '"+POut.PString(Cur.FamFinancial)+"'"
				+" WHERE patnum = '"+POut.PInt   (Patients.Cur.Guarantor)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		public static void InsertRow(int patNum){
			cmd.CommandText = "INSERT INTO patientnote (patnum"
				+") VALUES('"+patNum+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		//public static void ChangeGuarantor(int newGuarantor){
		//	 cmd.CommandText = "UPDATE familynote SET "
		//		+ "guarantor = '"+POut.PInt(newGuarantor)+"'"
		//		+" WHERE guarantor = '" +POut.PInt(Cur.Guarantor)+"'";
		//	//MessageBox.Show(cmd.CommandText);
		//	NonQ(false);
		//}

	}//end class FamilyNote

	public struct PatientNote{
		public int PatNum;
		public string FamFinancial;//only one note per family stored with guarantor.
		public string ApptPhone;//Notes for phone calls for Recall and Unscheduled list
		public string Medical;//Medical Summary
		public string Service;//Service notes
		public string MedicalComp;//Complete Medical History
	}

	/*=========================================================================================
		=================================== class PaymentPlans ==========================================*/
/*not used yet
	public class PaymentPlans:DataClass{
		public static PaymentPlan Cur;
		public static PaymentPlan[] List;

		public static void Refresh(){
			cmd.CommandText = "SELECT * FROM paymentplan "
				+"WHERE patnum = '"+POut.PInt(Patients.Cur.PatNum)+"'";
			FillTable();
			List=new PaymentPlan[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].PaymentPlanNum	 = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum					 = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].PaymentPlanDate	 = PIn.PDate  (table.Rows[i][2].ToString());
				List[i].Description			 = PIn.PString(table.Rows[i][3].ToString());
				List[i].PaymentPlanAmount= PIn.PDouble(table.Rows[i][4].ToString());
				List[i].APR							 = PIn.PDouble(table.Rows[i][5].ToString());
				List[i].NumberOfPayments = PIn.PInt   (table.Rows[i][6].ToString());
				List[i].MonthlyAmount		 = PIn.PDouble(table.Rows[i][7].ToString());
				List[i].Note						 = PIn.PString(table.Rows[i][8].ToString());
			}
		}

		public static void UpdateCur(){//updates Cur
			cmd.CommandText = "UPDATE paymentplan SET " 
				+ "PatNum							= '" +POut.PInt   (Cur.PatNum)+"'"
				+ ",PaymentPlanDate		= '" +POut.PDate  (Cur.PaymentPlanDate)+"'"
				+ ",Description       = '" +POut.PString(Cur.Description)+"'"
				+ ",PaymentPlanAmount = '" +POut.PDouble(Cur.PaymentPlanAmount)+"'"
				+ ",APR							  = '" +POut.PDouble(Cur.APR)+"'"
				+ ",NumberOfPayments  = '" +POut.PInt   (Cur.NumberOfPayments)+"'"
				+ ",MonthlyAmount		  = '" +POut.PDouble(Cur.MonthlyAmount)+"'"
				+ ",Note						  = '" +POut.PString(Cur.Note)+"'"
				+" WHERE PaymentPlanNum = '" +POut.PInt(Cur.PaymentPlanNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		public static void InsertCur(){//saves Cur
			cmd.CommandText = "INSERT INTO paymentplan (PatNum,PaymentPlanDate,Description,"
				+"PaymentPlanAmount,APR,NumberOfPayments,MonthlyAmount,Note) VALUES("
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PDate  (Cur.PaymentPlanDate)+"', "
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PDouble(Cur.PaymentPlanAmount)+"', "
				+"'"+POut.PDouble(Cur.APR)+"', "
				+"'"+POut.PInt   (Cur.NumberOfPayments)+"', "
				+"'"+POut.PDouble(Cur.MonthlyAmount)+"', "
				+"'"+POut.PString(Cur.Note)+"')";
			NonQ(true);
			Cur.PaymentPlanNum=InsertID;
			//MessageBox.Show(Cur.PayNum.ToString());
		}

		public static void DeleteCur(){//deletes Cur
			cmd.CommandText = "DELETE from paymentplan WHERE PaymentPlanNum = '"+Cur.PaymentPlanNum.ToString()+"'";
			NonQ(false);
		}
	}

	public struct PaymentPlan{
		public int PaymentPlanNum;
		public int PatNum;//Selected automatically from the splits.  Just to make reports easier
		public DateTime PaymentPlanDate;
		public string Description;
		public double PaymentPlanAmount;
		public double APR;
		public int NumberOfPayments;
		public double MonthlyAmount;
		public string Note;
	}*/


	/*=========================================================================================
		=================================== class Payments ==========================================*/

	public class Payments:DataClass{
		public static Payment Cur;

		public static void SetCur(int myPayNum){
			cmd.CommandText =
				"SELECT * from payment"
				+" WHERE PayNum = '"+myPayNum+"'";
			FillTable();
			Cur.PayNum    =PIn.PInt   (table.Rows[0][0].ToString());
			Cur.PayType   =PIn.PInt   (table.Rows[0][1].ToString());
			Cur.PayDate   =PIn.PDate  (table.Rows[0][2].ToString());
			Cur.PayAmt    =PIn.PDouble(table.Rows[0][3].ToString());
			Cur.CheckNum  =PIn.PString(table.Rows[0][4].ToString());
			Cur.BankBranch=PIn.PString(table.Rows[0][5].ToString());
			Cur.PayNote   =PIn.PString(table.Rows[0][6].ToString());
			Cur.IsSplit   =PIn.PBool  (table.Rows[0][7].ToString());
			Cur.PatNum    =PIn.PInt   (table.Rows[0][8].ToString());
		}

		public static void UpdateCur(){//updates Cur
			cmd.CommandText = "UPDATE payment SET " 
				+ "paytype = '"      +POut.PInt   (Cur.PayType)+"'"
				+ ",paydate = '"     +POut.PDate  (Cur.PayDate)+"'"
				+ ",payamt = '"      +POut.PDouble(Cur.PayAmt)+"'"
				+ ",checknum = '"    +POut.PString(Cur.CheckNum)+"'"
				+ ",bankbranch = '"  +POut.PString(Cur.BankBranch)+"'"
				+ ",paynote = '"     +POut.PString(Cur.PayNote)+"'"
				+ ",issplit = '"     +POut.PBool  (Cur.IsSplit)+"'"
				+ ",patnum = '"      +POut.PInt   (Cur.PatNum)+"'"
				+" WHERE payNum = '" +POut.PInt   (Cur.PayNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		public static void InsertCur(){//saves Cur
			cmd.CommandText = "INSERT INTO payment (paytype,paydate,payamt, "
				+"checknum,bankbranch,paynote,issplit,patnum) VALUES("
				+"'"+POut.PInt   (Cur.PayType)+"', "
				+"'"+POut.PDate  (Cur.PayDate)+"', "
				+"'"+POut.PDouble(Cur.PayAmt)+"', "
				+"'"+POut.PString(Cur.CheckNum)+"', "
				+"'"+POut.PString(Cur.BankBranch)+"', "
				+"'"+POut.PString(Cur.PayNote)+"', "
				+"'"+POut.PBool  (Cur.IsSplit)+"', "
				+"'"+POut.PInt   (Cur.PatNum)+"')";
			NonQ(true);
			Cur.PayNum=InsertID;
			//MessageBox.Show(Cur.PayNum.ToString());
		}

		public static void DeleteCur(){//deletes Cur
			cmd.CommandText = "DELETE from payment WHERE payNum = '"+Cur.PayNum.ToString()+"'";
			NonQ(false);
		}

		public static string GetInfo(int payNum){
			string retStr;
			SetCur(payNum);
			retStr= Defs.GetName(DefCat.PaymentTypes,Cur.PayType);
			if(Cur.IsSplit) retStr=retStr
												+"  $"+Cur.PayAmt.ToString("F")
												+"  "+Cur.PayDate.ToString("d")
												+" split between patients";
			return retStr;
		}
	}

	public struct Payment{
		public int PayNum;
		public int PayType;
		public DateTime PayDate;
		public double PayAmt;
		public string CheckNum;
		public string BankBranch;
		public string PayNote;
		public bool IsSplit;//(between patients.  Not including discounts)
		public int PatNum;//Selected automatically from the splits.  Just to make reports easier
	}

	/*=========================================================================================
	=================================== class PaySplits ==========================================*/

	public class PaySplits:DataClass{
		public static PaySplit[] List;
		public static PaySplit[] PaymentList;
		public static PaySplit Cur;

		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from paysplit"
				+" WHERE patnum = '"+Patients.Cur.PatNum+"' ORDER BY procdate";
			FillTable();
			List=new PaySplit[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].SplitNum    = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].SplitAmt    = PIn.PDouble(table.Rows[i][1].ToString());
				List[i].PatNum      = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].ProcDate    = PIn.PDate  (table.Rows[i][3].ToString());
				List[i].PayNum      = PIn.PInt   (table.Rows[i][4].ToString());
				List[i].IsDiscount  = PIn.PBool  (table.Rows[i][5].ToString());
				List[i].DiscountType= PIn.PInt   (table.Rows[i][6].ToString());
				List[i].ProvNum     = PIn.PInt   (table.Rows[i][7].ToString());
			}//end for
		}

		public static void UpdateCur(){//updates Cur
			cmd.CommandText = "UPDATE paysplit SET " 
				+ "splitamt = '"     +POut.PDouble(Cur.SplitAmt)+"'"
				+ ",patnum = '"      +POut.PInt   (Cur.PatNum)+"'"
				+ ",procdate = '"    +POut.PDate  (Cur.ProcDate)+"'"
				+ ",paynum = '"      +POut.PInt   (Cur.PayNum)+"'"
				+ ",isdiscount = '"  +POut.PBool  (Cur.IsDiscount)+"'"
				+ ",discounttype = '"+POut.PInt   (Cur.DiscountType)+"'"
				+ ",provnum = '"     +POut.PInt   (Cur.ProvNum)+"'"
				+" WHERE splitNum = '" +POut.PInt (Cur.SplitNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		public static void InsertCur(){//saves Cur
			cmd.CommandText = "INSERT INTO paysplit (splitamt,patnum,procdate, "
				+"paynum,isdiscount,discounttype,provnum) VALUES("
				+"'"+POut.PDouble(Cur.SplitAmt)+"', "
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PDate  (Cur.ProcDate)+"', "
				+"'"+POut.PInt   (Cur.PayNum)+"', "
				+"'"+POut.PBool  (Cur.IsDiscount)+"', "
				+"'"+POut.PInt   (Cur.DiscountType)+"', "
				+"'"+POut.PInt   (Cur.ProvNum)+"')";
			NonQ(false);
		}

		public static void RefreshPaymentList(int payNum){
			cmd.CommandText =
				"SELECT * from paysplit"
				+" WHERE paynum = '"+payNum+"'";
			FillTable();
			PaymentList=new PaySplit[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				PaymentList[i].SplitNum    = PIn.PInt   (table.Rows[i][0].ToString());
				PaymentList[i].SplitAmt    = PIn.PDouble(table.Rows[i][1].ToString());
				PaymentList[i].PatNum      = PIn.PInt   (table.Rows[i][2].ToString());
				PaymentList[i].ProcDate    = PIn.PDate  (table.Rows[i][3].ToString());
				PaymentList[i].PayNum      = PIn.PInt   (table.Rows[i][4].ToString());
				PaymentList[i].IsDiscount  = PIn.PBool  (table.Rows[i][5].ToString());
				PaymentList[i].DiscountType= PIn.PInt   (table.Rows[i][6].ToString());
				PaymentList[i].ProvNum     = PIn.PInt   (table.Rows[i][7].ToString());
			}//end for
		}

		public static void DeleteCur(){//deletes Cur
			//Cur=List[Selected];
			//PutBal(Cur.PatNum,Cur.ProcDate,-Cur.SplitAmt);
			cmd.CommandText = "DELETE from paysplit WHERE splitNum = '"+Cur.SplitNum.ToString()+"'";
			NonQ(false);
		}

		public static double ComputeBal(){//must make sure Refresh is done first
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				retVal+=List[i].SplitAmt;
			}
			return retVal;
		}

	}

	public struct PaySplit{
		public int SplitNum;//primary key
		public double SplitAmt;//amount
		public int PatNum;//foreign key to Patient.PatNum
		public DateTime ProcDate;//procedure date.  Not necessarily when the payment was made.
		public int PayNum;//foreign key to Payment.PayNum
		public bool IsDiscount;//can be discount or payment
		public int DiscountType;//foreign key to Definition.DefNum
		public int ProvNum;//foreign key to Provider.ProvNum
	}

	/*=========================================================================================
	=================================== class Permissions ==========================================*/
  
	public class Permissions:DataClass{
		public static Permission Cur;
		public static Permission[] List;

		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from permission ORDER BY Name";
			FillTable();
			List=new Permission[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i].PermissionNum  = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Name           = PIn.PString(table.Rows[i][1].ToString());
				List[i].RequiresPassword= PIn.PBool  (table.Rows[i][2].ToString());	
				List[i].BeforeDate     = PIn.PDate  (table.Rows[i][3].ToString());
				List[i].BeforeDays     = PIn.PInt   (table.Rows[i][4].ToString());
			}
		}

		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO permission (Name,RequiresPassword,BeforeDate,BeforeDays) "
				+"VALUES ("
				+"'"+POut.PString(Cur.Name)+"', "
				+"'"+POut.PBool  (Cur.RequiresPassword)+"', "
				+"'"+POut.PDate  (Cur.BeforeDate)+"', "
				+"'"+POut.PInt   (Cur.BeforeDays)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.PermissionNum=InsertID;
		}

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE permission SET "
				+"name ='"              +POut.PString(Cur.Name)+"'"
				+",requirespassword ='" +POut.PBool  (Cur.RequiresPassword)+"'"
				+",beforedate='"        +POut.PDate  (Cur.BeforeDate)+"'"
				+",beforedays ='"       +POut.PInt   (Cur.BeforeDays)+"'"
				+" WHERE permissionnum = '"+POut.PInt(Cur.PermissionNum)+"'";
			NonQ(false);
		}

		public static bool GetCur(string permissionName){
			//used in Security log entry and UserPermissions.CheckUserPassword
			for(int i=0;i<List.Length;i++){
				if(List[i].Name==permissionName){
					Cur=List[i];
					return true;
				}
			}
			return false;
		}

		/*public static bool GetCur(int permissionNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].PermissionNum==permissionNum){
					Cur=List[i];
					return true;
				}
				//if(i==List.Length-1)
				//	MessageBox.Show("error. unexpected permissionnum: "+permissionNum.ToString());
			}
			return false;
		}*/

		public static void DisableSecurity(){
			cmd.CommandText="UPDATE permission SET RequiresPassword = '0' "
				+"WHERE Name = 'Security Administration'";
			NonQ(false);
			Refresh();
		}

		public static void SetAll(bool doRequirePass){
			if(doRequirePass){
				cmd.CommandText="UPDATE permission SET RequiresPassword = '1'";
			}
			else{
				cmd.CommandText="UPDATE permission SET RequiresPassword = '0'";
			}
			NonQ(false);
		}

	}

	public struct Permission{//table permission
		public int PermissionNum;//Primary Key
		public string Name;//Description.  Not user editable.
		public bool RequiresPassword;//if true, will display user/password dialog for that feature
		public DateTime BeforeDate;//only displays user/password dialog if item date is before this date
		public int BeforeDays;//can be -1 for "do not calculate days". 0 days=always require password.
			//Only displays user/password dialog if item is before given number of days
	}

	/*=========================================================================================
	=================================== class Prefs ==========================================*/

	public class Prefs:DataClass{
		public static Hashtable HList;
		public static Pref Cur;
		//private string DataBaseVersion=Application.ProductVersion;//was "1.0.0";

		public static bool ConvertDB(){
			ExitApplicationNow ExitApplicationNow2=new ExitApplicationNow();
			ClassConvertDatabase ClassConvertDatabase2=new ClassConvertDatabase();
			if(ClassConvertDatabase2.Convert(((Pref)HList["DataBaseVersion"]).ValueString)){
				return true;
			}
			else{
				MessageBox.Show(Lan.g("Pref","Conversion unsuccessful"));
				ExitApplicationNow2.ExitNow();
				return false;
			}
		}

		public static void Refresh(){
			HList=new Hashtable();
			Pref tempPref = new Pref();
			cmd.CommandText = 
				"SELECT * from preference";
			FillTable();
			for(int i=0;i<table.Rows.Count;i++){
				tempPref.PrefName=PIn.PString(table.Rows[i][0].ToString());
				tempPref.ValueString=PIn.PString(table.Rows[i][1].ToString());
				HList.Add(tempPref.PrefName,tempPref);
			}
		}

		public static bool TryToConnect(){
			try{
				con.Open();
				cmd.CommandText="update preference set valuestring = '0' where valuestring = '0'";
				int rowsUpdated = cmd.ExecuteNonQuery();
				con.Close();

			}
			catch{//(MySQLDriverCS.MySQLException ex){
				return false; 
			}
			return true;
		}

		public static bool DBExists(){
			try{
				con.Open();
				//cmd.CommandText="update preference set valuestring = '0' where valuestring = '0'";
				//int rowsUpdated = cmd.ExecuteNonQuery();
				con.Close();

			}
			catch{//(MySQLDriverCS.MySQLException ex){
				return false; 
			}
			return true;
		}

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE preference SET "
				+"valuestring = '"  +POut.PString(Cur.ValueString)+"'"
				+" WHERE prefname = '"+POut.PString(Cur.PrefName)+"'";
			NonQ(false);
		}

		public static void FlushAndLock(){
			try{
				con.Open();
				cmd.CommandText="FLUSH TABLES WITH READ LOCK";
				int rowsUpdated = cmd.ExecuteNonQuery();
			}
			catch{
				//MessageBox.Show(con.ConnectionString);
				MessageBox.Show(Lan.g("Pref","Error in FlushAndLock"));
			}
		}

		public static void Unlock(){
			con.Close();
		}		
	}

	public struct Pref{//stores preferences and practice info
		public string PrefName;//the 'key'
		public string ValueString;
	}


	/*=========================================================================================
		=================================== class ProcButtons===========================================*/

	public class ProcButtons:DataClass{
		public static ProcButton Cur;
		public static Hashtable HList;
		public static ProcButton[] List;

		public static void Refresh(){
			cmd.CommandText = 
				"SELECT * from procbutton "
				+"ORDER BY itemorder";
			FillTable();
			HList=new Hashtable();
			List=new ProcButton[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				List[i].ProcButtonNum=PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description  =PIn.PString(table.Rows[i][1].ToString());
				List[i].ItemOrder    =PIn.PInt   (table.Rows[i][2].ToString());
				//List[i].IsFourQuad   =PIn.PBool  (table.Rows[i][2].ToString());
				HList.Add(List[i].ProcButtonNum,List[i]);
			}
		}

		public static void InsertCur(){
			//must have already checked ADACode for nonduplicate.
			cmd.CommandText = "INSERT INTO procbutton (description,itemorder) VALUES("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PInt   (Cur.ItemOrder)+"')";
				//+"'"+POut.PBool  (Cur.IsFourQuad)+"')";
			NonQ(true);
			Cur.ProcButtonNum=InsertID;
		}

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE procbutton SET " 
				+ "description = '" +POut.PString(Cur.Description)+"'"
				+ ",itemorder = '"  +POut.PInt   (Cur.ItemOrder)+"'"  
				//+ ",isfourquad = '" +POut.PBool  (Cur.IsFourQuad)+"'"     
				+" WHERE procbuttonnum = '"+POut.PInt(Cur.ProcButtonNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		public static void DeleteCur(){
			cmd.CommandText = "DELETE from procbuttonitem WHERE procbuttonnum = '"
				+POut.PInt(Cur.ProcButtonNum)+"'";
			NonQ(false);
			cmd.CommandText = "DELETE from procbutton WHERE procbuttonnum = '"
				+POut.PInt(Cur.ProcButtonNum)+"'";
			NonQ(false);
		}

	}

	public struct ProcButton{
		public int ProcButtonNum;//primary key
		public string Description;
		public int ItemOrder;//order that they will show
		//public bool IsFourQuad;
	}

	/*=========================================================================================
		=================================== class ProcButtonItems===========================================*/

	public class ProcButtonItems:DataClass{
		public static ProcButtonItem Cur;
		public static Hashtable HList;
		public static ProcButtonItem[] List;
		//public static ProcButtonItem[] ListForButton;
		//these two used when editing buttons:
		public static ArrayList ALadaCodes;//string, because some buttonItems are adacodes
		public static ArrayList ALautoCodes;//int, and some are autocodes
		//these two used when clicking buttons:
		public static string[] adaCodeList;
		public static int[] autoCodeList;
		//private static ArrayList ALlist;

		public static void Refresh(){
			cmd.CommandText = 
				"SELECT * FROM procbuttonitem";
			FillTable();
			HList=new Hashtable();
			List=new ProcButtonItem[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				List[i].ProcButtonItemNum=PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ProcButtonNum    =PIn.PInt   (table.Rows[i][1].ToString());
				List[i].ADACode          =PIn.PString(table.Rows[i][2].ToString());
				List[i].AutoCodeNum      =PIn.PInt   (table.Rows[i][3].ToString());
				HList.Add(List[i].ProcButtonItemNum,List[i]);
			}
		}

		public static void GetListsForButton(int procButtonNum){
			//ArrayList ALtemp=new ArrayList();
			ALadaCodes=new ArrayList();
			ALautoCodes=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcButtonNum==procButtonNum){
					//ALtemp.Add(List[i]);
					if(List[i].AutoCodeNum > 0){
						ALautoCodes.Add(List[i].AutoCodeNum);
					}
					else{
						ALadaCodes.Add(List[i].ADACode);
					}
				} 
			}
			/*
			ListForButton=new ProcButtonItem[ALtemp.Count];
			if(ALtemp.Count > 0){
				ALtemp.CopyTo(ListForButton);
			}*/
			autoCodeList=new int[ALautoCodes.Count];
			if(ALautoCodes.Count > 0){
				ALautoCodes.CopyTo(autoCodeList);
			}
			adaCodeList=new string[ALadaCodes.Count];
			if(ALadaCodes.Count > 0){
				ALadaCodes.CopyTo(adaCodeList);
			}
		}

		public static void InsertCur(){
			//must have already checked ADACode for nonduplicate.
			cmd.CommandText = "INSERT INTO procbuttonitem (procbuttonnum,adacode,autocodenum) VALUES("
				+"'"+POut.PInt   (Cur.ProcButtonNum)+"', "
				+"'"+POut.PString(Cur.ADACode)+"', "
				+"'"+POut.PInt   (Cur.AutoCodeNum)+"')";  

			NonQ(false);
		}

		public static void UpdateCur(){
			//MessageBox.Show("Updating");
			cmd.CommandText = "UPDATE procbuttonitem SET " 
				+ "procbuttonnum='"+POut.PInt   (Cur.ProcButtonNum)+"'"
				+ ",adacode='"     +POut.PString(Cur.ADACode)+"'"
				+ ",autocodenum='" +POut.PInt   (Cur.AutoCodeNum)+"'"
				+" WHERE procbuttonitemnum = '"+POut.PInt(Cur.ProcButtonItemNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		public static void DeleteCur(){
			cmd.CommandText = "DELETE from procbuttonitem WHERE procbuttonitemnum = '"+POut.PInt(Cur.ProcButtonItemNum)+"'";
			NonQ(false);
		}

		public static void DeleteAllForCur(){
			cmd.CommandText = "DELETE from procbuttonitem WHERE procbuttonnum = '"+POut.PInt(ProcButtons.Cur.ProcButtonNum)+"'";
			NonQ(false);
		}

	}//end class ProcButtonItems

	public struct ProcButtonItem{
		public int ProcButtonItemNum;//pk
		public int ProcButtonNum;//fk to procbutton table
		public string ADACode;//fk to procedurecode table
		public int AutoCodeNum;//fk to autocode table
	}//end struct ProcButtonItem


	/*=========================================================================================
	=================================== class ProcCodes ===========================================*/

	public class ProcCodes:DataClass{
		public static DataTable tableStat;
		public static ProcedureCode[] ProcList;//grouped by category
		public static ArrayList RecallAL;
		public static ProcedureCode Cur;
		public static Hashtable HList;//key:AdaCode, value:ProcedureCode 
		public static ProcedureCode[] List;

		public static void Refresh(){
			HList=new Hashtable();
			ProcedureCode tempCode = new ProcedureCode();
			cmd.CommandText = 
				"SELECT * from procedurecode "
				+"ORDER BY ProcCat, ADACode";
			FillTable();
			tableStat=table.Copy();
			RecallAL=new ArrayList();
			List=new ProcedureCode[tableStat.Rows.Count];
			for (int i=0;i<tableStat.Rows.Count;i++){
				tempCode.ADACode       =PIn.PString(tableStat.Rows[i][0].ToString());
				tempCode.Descript      =PIn.PString(tableStat.Rows[i][1].ToString());
				tempCode.AbbrDesc      =PIn.PString(tableStat.Rows[i][2].ToString());
				tempCode.ProcTime      =PIn.PString(tableStat.Rows[i][3].ToString());
				tempCode.ProcCat       =PIn.PInt   (tableStat.Rows[i][4].ToString());
				tempCode.TreatArea     =(TreatmentArea)PIn.PInt   (tableStat.Rows[i][5].ToString());
				tempCode.RemoveTooth   =PIn.PBool  (tableStat.Rows[i][6].ToString());
				tempCode.SetRecall     =PIn.PBool  (tableStat.Rows[i][7].ToString());
				tempCode.NoBillIns     =PIn.PBool  (tableStat.Rows[i][8].ToString());
				tempCode.IsProsth      =PIn.PBool  (tableStat.Rows[i][9].ToString());
				tempCode.DefaultNote   =PIn.PString(tableStat.Rows[i][10].ToString());
				tempCode.IsHygiene     =PIn.PBool  (tableStat.Rows[i][11].ToString());
				tempCode.GTypeNum      =PIn.PInt   (tableStat.Rows[i][12].ToString());
				tempCode.AlternateCode1=PIn.PString(tableStat.Rows[i][13].ToString());
				HList.Add(tempCode.ADACode,tempCode);
				List[i]=tempCode;
				if(tempCode.SetRecall){
					RecallAL.Add(tempCode);
				}
			}
		}

		public static void InsertCur(){
			//must have already checked ADACode for nonduplicate.
			cmd.CommandText = "INSERT INTO procedurecode (adacode,descript,abbrdesc,"
				+"proctime,proccat,treatarea,removetooth,setrecall,"
				+"nobillins,isprosth,defaultnote,ishygiene,gtypenum,alternatecode1) VALUES("
				+"'"+POut.PString(Cur.ADACode)+"', "
				+"'"+POut.PString(Cur.Descript)+"', "
				+"'"+POut.PString(Cur.AbbrDesc)+"', "
				+"'"+POut.PString(Cur.ProcTime)+"', "
				+"'"+POut.PInt   (Cur.ProcCat)+"', "
				+"'"+POut.PInt   ((int)Cur.TreatArea)+"', "
				+"'"+POut.PBool  (Cur.RemoveTooth)+"', "
				+"'"+POut.PBool  (Cur.SetRecall)+"', "
				+"'"+POut.PBool  (Cur.NoBillIns)+"', "
				+"'"+POut.PBool  (Cur.IsProsth)+"', "
				+"'"+POut.PString(Cur.DefaultNote)+"', "
				+"'"+POut.PBool  (Cur.IsHygiene)+"', "
				+"'"+POut.PInt   (Cur.GTypeNum)+"', "
				+"'"+POut.PString(Cur.AlternateCode1)+"')";
			NonQ(false);
			Refresh();
			//Cur already set
			//MessageBox.Show(Cur.PayNum.ToString());
		}

		public static void UpdateCur(){
			//MessageBox.Show("Updating");
			cmd.CommandText = "UPDATE procedurecode SET " 
				+ "descript = '"       +POut.PString(Cur.Descript)+"'"
				+ ",abbrdesc = '"      +POut.PString(Cur.AbbrDesc)+"'"
				+ ",proctime = '"      +POut.PString(Cur.ProcTime)+"'"
				+ ",proccat = '"       +POut.PInt   (Cur.ProcCat)+"'"
				+ ",treatarea = '"     +POut.PInt   ((int)Cur.TreatArea)+"'"
				+ ",removetooth = '"   +POut.PBool  (Cur.RemoveTooth)+"'"
				+ ",setrecall = '"     +POut.PBool  (Cur.SetRecall)+"'"
				+ ",nobillins = '"     +POut.PBool  (Cur.NoBillIns)+"'"
				+ ",isprosth = '"      +POut.PBool  (Cur.IsProsth)+"'"
				+ ",defaultnote = '"   +POut.PString(Cur.DefaultNote)+"'"
				+ ",ishygiene = '"     +POut.PBool  (Cur.IsHygiene)+"'"
				+ ",gtypenum = '"      +POut.PInt   (Cur.GTypeNum)+"'"
				+ ",alternatecode1 = '"+POut.PString(Cur.AlternateCode1)+"'"
				+" WHERE adacode = '"+POut.PString(Cur.ADACode)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		public static ProcedureCode GetProcCode(string myADA){
			if(myADA==null){
				MessageBox.Show(Lan.g("ProcCodes","Error. Invalid procedure code."));
				return new ProcedureCode();
			}
			if(HList.Contains(myADA)){
				return (ProcedureCode)HList[myADA];
			}
			else{
				MessageBox.Show(Lan.g("ProcCodes","code not found: ")+myADA);
				return new ProcedureCode();
			}
		}

		public static void GetProcList(){
			ProcList = new ProcedureCode[tableStat.Rows.Count];
			int i=0;
			for(int j=0;j<Defs.Short[(int)DefCat.ProcCodeCats].Length;j++){
				for(int k=0;k<tableStat.Rows.Count;k++){
					if(Defs.Short[(int)DefCat.ProcCodeCats][j].DefNum==PIn.PInt(tableStat.Rows[k][4].ToString())){
						ProcList[i].ADACode = PIn.PString(tableStat.Rows[k][0].ToString());
						ProcList[i].Descript= PIn.PString(tableStat.Rows[k][1].ToString());
						ProcList[i].AbbrDesc= PIn.PString(tableStat.Rows[k][2].ToString());
						ProcList[i].ProcCat = PIn.PInt   (tableStat.Rows[k][4].ToString());
						i++;
					}
				}
			}
			for(int k=0;k<tableStat.Rows.Count;k++){
				if(PIn.PInt(tableStat.Rows[k][4].ToString())==255){
					ProcList[i].ADACode = PIn.PString(tableStat.Rows[k][0].ToString());
					ProcList[i].AbbrDesc= PIn.PString(tableStat.Rows[k][2].ToString());
					ProcList[i].ProcCat = 255;
					i++;
				}
			}
		}

	}//end class ProcCodes

	public struct ProcedureCode{
		public string ADACode;//primary key, 6 Char, last char is for user's use, and must be dropped when sending claim
		public string Descript;
		public string AbbrDesc;
		public string ProcTime;//X's and /'s describe Dr's time and assistant's time
		public int ProcCat;//foreign key to Definition.DefNum
		public TreatmentArea TreatArea;//enum TreatmentArea{Surf=1,Tooth=2,Mouth=3 or 0,Quad=4,Sextant=5,Arch=6,ToothRange=7}
		public bool RemoveTooth;//true for extractions so teeth will show as missing
		public bool SetRecall;//triggers recall in 6 months.
		public bool NoBillIns;//if true, do not bill this procedure to insurance
		public bool IsProsth;//is a bridge, crown, or denture. Not currently in use anywhere.
		public string DefaultNote;
		public bool IsHygiene;//Identifies hygiene procedures so that the correct provider can be selected.
		public int GTypeNum; //foreign key to GraphicType
		public string AlternateCode1;//For Medicaid.  There may be more later.
	}//end struct ProcedureCode
	
	/*=========================================================================================
	=================================== class Procedures ===========================================*/

	public class Procedures:DataClass{
		public static Procedure Cur;
		public static Procedure[] List;//all procedures for current patient
		public static Hashtable HList;//Hashtable of all procedures for current patient
		public static ArrayList MissingTeeth;//valid "1"-"32", and "A"-"Z"
		private static ProcDesc[] procsMultApts;
		public static string[] ProcsOneApt;
		public static string[] ProcsForSingle;//for one appointment or one next appointment
		//private static ProcCodes ProcCodes;

		public Procedures(){
			//ProcCodes=new ProcCodes();
		}

		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from procedurelog "
				+"WHERE PatNum = '"+POut.PInt(Patients.Cur.PatNum)+"' "
				+"ORDER BY ProcDate";
			FillTable();
			MissingTeeth=new ArrayList();
			HList=new Hashtable();
			List = new Procedure[table.Rows.Count];
			for (int i = 0; i < List.Length; i += 1){
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
				//PriEstim [15]
				//SecEstim [16]
				//List[i].ClaimNum				= PIn.PInt   (table.Rows[i][17].ToString());
				List[i].ProvNum					= PIn.PInt   (table.Rows[i][15].ToString());
				List[i].Dx							= PIn.PInt   (table.Rows[i][16].ToString());
				List[i].NextAptNum			= PIn.PInt   (table.Rows[i][17].ToString());
				List[i].IsCovIns				= PIn.PBool  (table.Rows[i][18].ToString());
				HList.Add(List[i].ProcNum,List[i]);    
				if(ProcCodes.GetProcCode(List[i].ADACode).RemoveTooth && (
					List[i].ProcStatus==ProcStat.C
					|| List[i].ProcStatus==ProcStat.EC
					|| List[i].ProcStatus==ProcStat.EO))
				{
					MissingTeeth.Add(Procedures.List[i].ToothNum);
				}  
			}
		}

		/*public static int ConvertToInt(string toothNum){
			int retVal=-1;//0?
			int intPri;
			try{
				retVal=System.Convert.ToInt32(toothNum);
			}
			catch{
				//if (Procedures.List[i].ToothNum.CompareTo("A")>=0 & Procedures.List[i].ToothNum.CompareTo("J")<=0) toothIsAnt = true;
				//else if (Procedures.List[i].ToothNum.CompareTo("K")>=0 & Procedures.List[i].ToothNum.CompareTo("T")<=0) toothIsAnt = true;
				if (toothNum!=null & toothNum.Length>0){
					intPri=Convert.ToByte(Convert.ToChar(toothNum));//F=70
					if(intPri>=65 & intPri<=74){
						retVal=intPri-61;
					}
					else if(intPri>=75 & intPri<=84){
						retVal=intPri-55;
					}
				}//end if
				else retVal=0;
			}
			return retVal;
		}*/

		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO procedurelog " 
				+"(PatNum, AptNum, ADACode, ProcDate, "
				+"ProcFee, "
				+"OverridePri, OverrideSec, Surf, "
				+"ToothNum, ToothRange, NoBillIns, Priority, "
				+"ProcStatus, ProcNote, ProvNum, "
				+"dx,nextaptnum,iscovins) "
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
				//+"'"+POut.PDouble(Cur.PriEstim)+"', "
				//+"'"+POut.PDouble(Cur.SecEstim)+"', "
				//+"'"+POut.PInt   (Cur.ClaimNum)+"', "
				+"'"+POut.PInt   (Cur.ProvNum)+"', "
				+"'"+POut.PInt   (Cur.Dx)+"', "
				+"'"+POut.PInt   (Cur.NextAptNum)+"', "
				+"'"+POut.PBool  (Cur.IsCovIns)+"')";
				//+"'"+POut.PBool  (Cur.NoShowGraphical)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.ProcNum=InsertID;
		}

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
				//+"ClaimNum = '"				 +POut.PInt   (Cur.ClaimNum)+"', "
				+"ProvNum = '"				 +POut.PInt   (Cur.ProvNum)+"', "
				+"Dx = '"							 +POut.PInt   (Cur.Dx)+"', "
				+"nextaptnum = '"			 +POut.PInt   (Cur.NextAptNum)+"', "
				+"iscovins = '"				 +POut.PBool  (Cur.IsCovIns)+"' "
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

		

		public static void DeleteCur(){
			cmd.CommandText = "DELETE from procedurelog WHERE procNum = '"+POut.PInt(Cur.ProcNum)+"'";
			NonQ(false);
		}

		public static void GetProcsForSingle(int aptNum, bool isNext){//gets string[]
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

		private static string ConvertProcToString(string aDACode,string surf, string toothNum){//used by GetProcsForSingle
			//and     GetProcsMultApts
			string strLine="";
			switch (ProcCodes.GetProcCode(aDACode).TreatArea){
				case TreatmentArea.Surf :
					strLine+="#"+toothNum+"-"+surf+"-";//""#12-MOD-"
					break;
				case TreatmentArea.Tooth :
					strLine+="#"+toothNum+"-";//"#12-"
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
			strLine+=ProcCodes.GetProcCode(aDACode).AbbrDesc;
			return strLine;
		}

		public static void GetProcsMultApts(int[] myAptNums){//fills procsMultApts (ProcDesc (aptNum and string[]))
			//if (myAptNums.Length==0)
			Procedure tempProcedure = new Procedure();
			string strAptNums="";
			if (myAptNums.Length>0){
				strAptNums="AptNum='"+myAptNums[0].ToString()+"'";
				for (int i=1;i<myAptNums.Length;i++){
					strAptNums+=" || AptNum='"+myAptNums[i].ToString()+"'";
				}//end for
				//MessageBox.Show(strAptNums);
				cmd.CommandText = "SELECT * from procedurelog WHERE "+strAptNums;
				FillTable();
			}//end if >0
			int count3 = table.Rows.Count;
			//already defined: ProcDesc[] procsEntireDay
			//MessageBox.Show(count3.ToString());
			procsMultApts=new ProcDesc[myAptNums.Length];
			for (int i = 0; i < myAptNums.Length; i += 1){
				procsMultApts[i].AptNum=myAptNums[i];
				int internalCount=0;
				for (int j=0; j<count3; j+=1){
					if (PIn.PInt(table.Rows[j][2].ToString())==myAptNums[i]){
						internalCount+=1;
					}
				}
				procsMultApts[i].ProcLines=new string[internalCount];
				internalCount=0;
				string pADACode="";
				string pSurf="";
				string pToothNum="";
				for (int j=0; j<count3; j+=1){
					pADACode = PIn.PString(table.Rows[j][3].ToString());
					pSurf    = PIn.PString(table.Rows[j][8].ToString());
					pToothNum= PIn.PString(table.Rows[j][9].ToString());
					if (PIn.PInt(table.Rows[j][2].ToString())==myAptNums[i]){
						procsMultApts[i].ProcLines[internalCount]=ConvertProcToString(pADACode,pSurf,pToothNum);
						internalCount+=1;
					}
				}
			}//end for myAptNums
			//MessageBox.Show(procsEntireDay[0].AptNum.ToString()+procsEntireDay[1].AptNum.ToString());
		}

		public static void GetProcsOneApt(int myAptNum){//gets one from procsMultApts
			for (int i = 0; i < procsMultApts.Length; i += 1){
				if (procsMultApts[i].AptNum==myAptNum){
					//MessageBox.Show(myAptNum.ToString());
					ProcsOneApt=procsMultApts[i].ProcLines;
				}
			}
		}

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
					;//? 
				}
				else if(priCopay>0){
					priEst=Cur.ProcFee-InsPlans.GetCopay(Cur.ADACode,Patients.Cur.PriPlanNum);
				}
			}
			double secCopay=InsPlans.GetCopay(Cur.ADACode,Patients.Cur.SecPlanNum);//also gets InsPlan
			if(secCopay!=-1){//if a secondary copay fee schedule exists.
				if(InsPlans.Cur.PlanType=="c"){//capitation
					;//? 
				}
				else if(secCopay>0){
					secEst=Cur.ProcFee-InsPlans.GetCopay(Cur.ADACode,Patients.Cur.SecPlanNum);
				}
			}
			//if(InsPlans.GetCopay(Cur.ADACode,Patients.Cur.PriPlanNum)>0)
			//	priEst=InsPlans.GetCopay(Cur.ADACode,Patients.Cur.PriPlanNum);
			//if(InsPlans.GetCopay(Cur.ADACode,Patients.Cur.SecPlanNum)>0)
			//	secEst=InsPlans.GetCopay(Cur.ADACode,Patients.Cur.SecPlanNum);
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

		public static void UnattachProcsInAppt(int myAptNum){
			cmd.CommandText = "UPDATE procedurelog SET "
				+"AptNum = '0' "
				+"WHERE AptNum = '"+myAptNum+"'";
			NonQ(false);
		}

		public static void UnattachProcsInNextAppt(int myAptNum){
			cmd.CommandText = "UPDATE procedurelog SET "
				+"NextAptNum = '0' "
				+"WHERE NextAptNum = '"+myAptNum+"'";
			NonQ(false);
		}

		public static void SetCompleteInAppt(){//assumes you have set Appointments.Cur
			cmd.CommandText = "SELECT procnum,adacode,procnote FROM procedurelog "
				+"WHERE AptNum = '"+POut.PInt(Appointments.Cur.AptNum)+"'";
			FillTable();
			//int tempProcNum;
			bool doResetRecallStatus=false;
			for (int i=0;i<table.Rows.Count;i++){
				if(((ProcedureCode)ProcCodes.HList[table.Rows[i][1].ToString()]).SetRecall){//is a recall proc
					doResetRecallStatus=true;
				}
				cmd.CommandText = "UPDATE procedurelog SET "
					+"procstatus = '" +POut.PInt   ((int)ProcStat.C)+"', "
					+"Procdate = '"   +POut.PDate  (Appointments.Cur.AptDateTime.Date)+"', "
					+"procnote = '"		+POut.PString(table.Rows[i][2].ToString())//does not delete the existing note
					+POut.PString(((ProcedureCode)ProcCodes.HList[table.Rows[i][1].ToString()]).DefaultNote)+"'";
				if(Appointments.Cur.ProvHyg!=0){//if the appointment has a hygiene provider
					if(((ProcedureCode)ProcCodes.HList[table.Rows[i][1].ToString()]).IsHygiene){//hyg proc
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

		public static double ComputeBal(){//must make sure Refresh is done first
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcStatus==ProcStat.C){//complete
					//&& !ClaimProcs.ProcIsAttached(List[i].ProcNum)){// and not attached to a claim
					Cur=List[i];
					retVal+=Cur.ProcFee;//-GetEstForCur(PriSecTot.Tot);
				}
			}
			return retVal;
		}

		//public static void

	}//end class Procedures

	public struct Procedure{//table ProcedureLog
		public int ProcNum;//primary key
		public int PatNum;//foreign key to Patient.PatNum
		public int AptNum;//foreign key to Appointment.AptNum.  Only allowed to attach proc to one appt(not counting next apt)
		public string ADACode;//foreign key to ProcedureCode.ADACode
		public DateTime ProcDate;//procedure date
		public double ProcFee;//procedure fee
		public double OverridePri;//override primary insurance estimate.  -1 for false.
		public double OverrideSec;//override secondary insurance estimate. -1 for false.
		public string Surf;//use "UL" etc for quadrant, "2" etc for sextant, "U","L" for arches,
		public string ToothNum;//May be blank, otherwise 1-32 or A-T, 1 or 2 char.
		public string ToothRange;//May be blank, otherwise can include commas and dashes.
		public bool NoBillIns;//set true to indicate "do not bill procedure to insurance".
		public int Priority;//foreign key to Definition.DefNum, which contains the text of the priority
		public ProcStat ProcStatus;//enum ProcStat{TP=1,C=2,EC=3,EO=4,R=5} 
		//(TreatmentPlanned,Complete,ExistingCurrentProv,ExistingOtherProv,ReferredOut)
		public string ProcNote;//Procedure Note
		//public double PriEstim;//dropped
		//public double SecEstim;//dropped
		//public int ClaimNum;//dropped
		public int ProvNum;//foreign key to Provider.ProvNum.
		public int Dx;//foreign key to Definition.DefNum, which contains text of the Diagnosis.
		public int NextAptNum;//foreign key to Appointment.AptNum.  
		//Allows this procedure to be attached to a next appointment as well as a standard appointment
		public bool IsCovIns;//Is Covered by Insurance.  Set to false if patient does not have ins coverage.
    //public bool NoShowGraphical;//Graphical Tooth Chart addition in case tooth had drawings on it and then was extracted
	}//end struct procedure
	
	public struct ProcDesc{
		public int AptNum;
		public string[] ProcLines;
	}

	/*=========================================================================================
	=================================== class Programs ==========================================*/

	public class Programs:DataClass{
		public static Hashtable HList;
		public static Program Cur;
		public static Program[] List;

		public static void Refresh(){
			//MessageBox.Show("refreshing");
			HList=new Hashtable();
			Program tempProgram = new Program();
			cmd.CommandText = 
				"SELECT * from program";
			FillTable();
			List=new Program[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				tempProgram.ProgramNum=PIn.PInt   (table.Rows[i][0].ToString());
				tempProgram.ProgName  =PIn.PString(table.Rows[i][1].ToString());
				tempProgram.ProgDesc  =PIn.PString(table.Rows[i][2].ToString());
				tempProgram.Enabled   =PIn.PBool  (table.Rows[i][3].ToString());
				List[i]=tempProgram;
				HList.Add(tempProgram.ProgName,tempProgram);
			}
			//MessageBox.Show(HList.Count.ToString());
		}

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE program SET "
				+"progname = '"   +POut.PString(Cur.ProgName)+"'"
				+",progdesc  = '" +POut.PString(Cur.ProgDesc)+"'"
				+",enabled  = '"  +POut.PBool  (Cur.Enabled)+"'"
				+" WHERE programnum = '"+POut.PInt(Cur.ProgramNum)+"'";
			NonQ(false);
		}

		public static void InsertCur(){
			//not used yet
		}

		public static bool IsEnabled(string progName){
			if(HList.ContainsKey("VisiQuick") && ((Program)HList["VisiQuick"]).Enabled){
				return true;
			}
			return false;
		}


	}

	public struct Program{
		public int ProgramNum;//primary key
		public string ProgName;//unique name can not change
		public string ProgDesc;//description that shows
		public bool Enabled;
	}

	/*=========================================================================================
	=================================== class Providers ==========================================*/

	public class Providers:DataClass{
		public static Provider[] ListLong;
		public static Provider[] List;
		public static Provider Cur;
		public static int Selected;

		public static void Refresh(){
			ArrayList AL=new ArrayList();
			cmd.CommandText =
				"SELECT * from provider"
				//+" WHERE category = '"+j+"'"
				+" ORDER BY itemorder";
			FillTable();
			ListLong=new Provider[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				ListLong[i].ProvNum     = PIn.PInt   (table.Rows[i][0].ToString());
				ListLong[i].Abbr        = PIn.PString(table.Rows[i][1].ToString());
				ListLong[i].ItemOrder   = PIn.PInt   (table.Rows[i][2].ToString());
				ListLong[i].LName       = PIn.PString(table.Rows[i][3].ToString());
				ListLong[i].FName       = PIn.PString(table.Rows[i][4].ToString());
				ListLong[i].MI          = PIn.PString(table.Rows[i][5].ToString());
				ListLong[i].Title       = PIn.PString(table.Rows[i][6].ToString());
				ListLong[i].FeeSched    = PIn.PInt   (table.Rows[i][7].ToString());
				ListLong[i].Specialty   =(DentalSpecialty)PIn.PInt (table.Rows[i][8].ToString());
				ListLong[i].SSN         = PIn.PString(table.Rows[i][9].ToString());
				ListLong[i].StateLicense= PIn.PString(table.Rows[i][10].ToString());
				ListLong[i].DEANum      = PIn.PString(table.Rows[i][11].ToString());
				ListLong[i].IsSecondary = PIn.PBool  (table.Rows[i][12].ToString());
				ListLong[i].ProvColor   = Color.FromArgb(PIn.PInt(table.Rows[i][13].ToString()));
				ListLong[i].IsHidden    = PIn.PBool  (table.Rows[i][14].ToString());
				ListLong[i].UsingTIN    = PIn.PBool  (table.Rows[i][15].ToString());
				ListLong[i].BlueCrossID = PIn.PString(table.Rows[i][16].ToString());
				ListLong[i].SigOnFile   = PIn.PBool  (table.Rows[i][17].ToString());
				ListLong[i].Password    = PIn.PString(table.Rows[i][18].ToString());
				ListLong[i].UserName    = PIn.PString(table.Rows[i][19].ToString());
				ListLong[i].MedicaidID  = PIn.PString(table.Rows[i][20].ToString());
				if(!ListLong[i].IsHidden) AL.Add(ListLong[i]);	
			}
			List=new Provider[AL.Count];
			AL.CopyTo(List);
		}

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE provider SET "
				+ "abbr = '"        +POut.PString(Cur.Abbr)+"'"
				+",itemorder = '"   +POut.PInt   (Cur.ItemOrder)+"'"
				+",lname = '"       +POut.PString(Cur.LName)+"'"
				+",fname = '"       +POut.PString(Cur.FName)+"'"
				+",mi = '"          +POut.PString(Cur.MI)+"'"
				+",title = '"       +POut.PString(Cur.Title)+"'"
				+",feesched = '"    +POut.PInt   (Cur.FeeSched)+"'"
				+",specialty = '"   +POut.PInt   ((int)Cur.Specialty)+"'"
				+",ssn = '"         +POut.PString(Cur.SSN)+"'"
				+",statelicense = '"+POut.PString(Cur.StateLicense)+"'"
				+",deanum = '"      +POut.PString(Cur.DEANum)+"'"
				+",issecondary = '" +POut.PBool  (Cur.IsSecondary)+"'"
				+",provcolor = '"   +POut.PInt   (Cur.ProvColor.ToArgb())+"'"
				+",ishidden = '"    +POut.PBool  (Cur.IsHidden)+"'"
				+",usingtin = '"    +POut.PBool  (Cur.UsingTIN)+"'"
				+",bluecrossid = '" +POut.PString(Cur.BlueCrossID)+"'"
				+",sigonfile = '"   +POut.PBool  (Cur.SigOnFile)+"'"
				+",password = '"    +POut.PString(Cur.Password)+"'"
				+",username = '"    +POut.PString(Cur.UserName)+"'"
				+",medicaidid = '"  +POut.PString(Cur.MedicaidID)+"'"
				+" WHERE provnum = '"+POut.PInt(Cur.ProvNum)+"'";
			NonQ(false);
		}

		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO provider (abbr,itemorder,lname,fname,mi,title,"
				+"feesched,specialty,ssn,statelicense,deanum,issecondary,"
				+"provcolor,ishidden,usingtin,bluecrossid,sigonfile,password,username"
				+",medicaidid) VALUES("
				+"'"+POut.PString(Cur.Abbr)+"', "
				+"'"+POut.PInt   (Cur.ItemOrder)+"', "
				+"'"+POut.PString(Cur.LName)+"', "
				+"'"+POut.PString(Cur.FName)+"', "
				+"'"+POut.PString(Cur.MI)+"', "
				+"'"+POut.PString(Cur.Title)+"', "
				+"'"+POut.PInt   (Cur.FeeSched)+"', "
				+"'"+POut.PInt   ((int)Cur.Specialty)+"', "
				+"'"+POut.PString(Cur.SSN)+"', "
				+"'"+POut.PString(Cur.StateLicense)+"', "
				+"'"+POut.PString(Cur.DEANum)+"', "
				+"'"+POut.PBool  (Cur.IsSecondary)+"', "
				+"'"+POut.PInt   (Cur.ProvColor.ToArgb())+"', "
				+"'"+POut.PBool  (Cur.IsHidden)+"', "
				+"'"+POut.PBool  (Cur.UsingTIN)+"', "
				+"'"+POut.PString(Cur.BlueCrossID)+"', "
				+"'"+POut.PBool  (Cur.SigOnFile)+"', "
				+"'"+POut.PString(Cur.Password)+"', "			  
				+"'"+POut.PString(Cur.UserName)+"', "
				+"'"+POut.PString(Cur.MedicaidID)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.ProvNum=InsertID;
		}

		public static void DeleteCur(){
			cmd.CommandText = "DELETE from provider WHERE provnum = '"+Cur.ProvNum.ToString()+"'";
			NonQ(false);
		}

		public static string GetAbbr(int provNum){
			string retStr="";
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ProvNum==provNum){
					retStr=ListLong[i].Abbr;
				}
			}
			return retStr;
		}

		public static Color GetColor(int provNum){
			Color retCol=Color.White;
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ProvNum==provNum){
					retCol=ListLong[i].ProvColor;
				}
			}
			return retCol;
		}

		public static bool GetIsSec(int provNum){
			bool retVal=false;
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ProvNum==provNum){
					retVal=ListLong[i].IsSecondary;
				}
			}
			return retVal;
		}

		public static int GetIndexLong(int provNum){
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ProvNum==provNum){
					return i;
				}
			}
			return -1;//should NEVER return a -1
		}

		public static int GetIndex(int provNum){
			//Gets the index of the provider in short list (visible providers)
			for(int i=0;i<List.Length;i++){
				if(List[i].ProvNum==provNum){
					return i;
				}
			}
			return -1;
		}

		public static void MoveUp(){
			if(Selected<0){
				MessageBox.Show(Lan.g("Providers","Please select a provider first."));
				return;
			}
			if(Selected==0){
				return;
			}
			SetOrder(Selected-1,ListLong[Selected].ItemOrder);
			SetOrder(Selected,ListLong[Selected].ItemOrder-1);
			Selected-=1;
		}

		public static void MoveDown(){
			if(Selected<0){
				MessageBox.Show(Lan.g("Providers","Please select a provider first."));
				return;
			}
			if(Selected==ListLong.Length-1){
				return;
			}
			SetOrder(Selected+1,ListLong[Selected].ItemOrder);
			SetOrder(Selected,ListLong[Selected].ItemOrder+1);
			Selected+=1;
		}

		public static void SetOrder(int mySelNum, int myItemOrder){
			Provider temp=ListLong[mySelNum];
			temp.ItemOrder=myItemOrder;
			Cur=temp;
			UpdateCur();
		}
	}
	
	public struct Provider{
		public int ProvNum;//primary key
		public string Abbr;//abbreviation
		public int ItemOrder;//order that provider will show in lists
		public string LName;
		public string FName;
		public string MI;
		public string Title;//eg. DMD
		public int FeeSched;//foreign key to Definition.DefNum
		public DentalSpecialty Specialty;//enum DentalSpecialty{General=0,Hygienist,Endodontics,Pediatric,
		//Perio,Prosth,Ortho,Denturist,Surgery,Assistant,
		//LabTech,Pathology,PublicHealth,Radiology}
		public string SSN;//or TIN.  No punctuation
		public string StateLicense;//can include punctuation
		public string DEANum;
		public bool IsSecondary;//true if hygeinist
		public Color ProvColor;//color that shows in appointments
		public bool IsHidden;//if true, provider will not show on any lists
		public bool UsingTIN;//true if the SSN field is actually a Tax ID Num
		public string BlueCrossID;
		public bool SigOnFile;//signature on file
		public string Password;
		public string UserName;
		public string MedicaidID;
	}


}










