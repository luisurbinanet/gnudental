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
	///<summary></summary>
	public class Medications:DataClass{
		//not refreshed with local data.  Only refreshed as needed.
		///<summary></summary>
		public static Medication Cur;
		///<summary></summary>
		public static Medication[] List;
		///<summary></summary>
		public static Hashtable HList;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from medication ORDER BY MedName";
			FillList();
		}

		///<summary></summary>
		public static void RefreshGeneric(){
			cmd.CommandText =
				"SELECT * from medication WHERE medicationnum = genericnum ORDER BY MedName";
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

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE medication SET " 
				+ "medname = '"      +POut.PString(Cur.MedName)+"'"
				+ ",genericnum = '"  +POut.PInt   (Cur.GenericNum)+"'"
				+ ",notes = '"       +POut.PString(Cur.Notes)+"'"
				+" WHERE medicationnum = '" +POut.PInt   (Cur.MedicationNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
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

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from medication WHERE medicationNum = '"+Cur.MedicationNum.ToString()+"'";
			NonQ(false);
		}
		
	}

	///<summary>Corresponds to the medication table in the database.</summary>
	public struct Medication{
		///<summary>Primary key.</summary>
		public int MedicationNum;
		///<summary>Name of the medication.</summary>
		public string MedName;
		///<summary>Foreign key to medication.MedicationNum.  If this is a generic drug, then the GenericNum will be the same as the MedicationNum.</summary>
		public int GenericNum;
		///<summary>Notes.</summary>
		public string Notes;
	}

/*=========================================================================================
		=================================== class MedicationPats ==========================================*/

	///<summary></summary>
	public class MedicationPats:DataClass{
		///<summary></summary>
		public static MedicationPat Cur;
		///<summary></summary>
		public static MedicationPat[] List;//for current pat

		///<summary></summary>
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

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE medicationpat SET " 
				+ "patnum = '"        +POut.PInt   (Cur.PatNum)+"'"
				+ ",medicationnum = '"+POut.PInt   (Cur.MedicationNum)+"'"
				+ ",patnote = '"      +POut.PString(Cur.PatNote)+"'"
				+" WHERE medicationpatnum = '" +POut.PInt   (Cur.MedicationPatNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
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

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from medicationpat WHERE medicationpatNum = '"
				+Cur.MedicationPatNum.ToString()+"'";
			NonQ(false);
		}
		
	}

	///<summary>Corresponds to the medicationpat table in the database. It links medications to patients.</summary>
	public struct MedicationPat{
		///<summary>Primary key.</summary>
		public int MedicationPatNum;
		///<summary>Foreign key to patient.PatNum.</summary>
		public int PatNum;
		///<summary>Foreign key to medication.MedicationNum.</summary>
		public int MedicationNum;
		///<summary>Medication notes specific to this patient.</summary>
		public string PatNote;
	}

/*=========================================================================================
		=================================== class ODReportData ==========================================*/

	///<summary>Used by ODReport to interface with database.</summary>
	public class ODReportData:DataClass{

		///<summary>Submits the Query to the database and returns the DataTable as a result.</summary>
		///<param name="query"></param>
		public static DataTable SubmitQuery(string query){
			//MessageBox.Show(query);
			cmd.CommandText=query;
			FillTable();
			//MessageBox.Show(table.Rows.Count.ToString());
			return table;
		}
	}

	/*=========================================================================================
	=================================== class PIn ===========================================*/
	///<summary>Converts strings coming in from the database into the appropriate type.</summary>
	///<remarks>P was originally short for Parameter because it was replacing the data adapter parameters.  Using strings instead of parameters is much easier to debug.  This class will be replaced with an IConvertible interface as soon as we have time.</remarks>
	public class PIn{
		///<summary></summary>
		public static bool PBool (string myString){
			return myString=="1";
		}

		///<summary></summary>
		public static byte PByte (string myString){
			if(myString==""){
				return 0;
			}
			else{
				return System.Convert.ToByte(myString);
			}
		}

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
		public static double PDouble (string myString){
			if (myString==""){
				return 0;
			}
			else{
				try{
					return System.Convert.ToDouble(myString);
				}
				catch{
					//MessageBox.Show(myString);
					return 0;
				}
			}

		}

		///<summary></summary>
		public static int PInt (string myString){
			if (myString==""){
				return 0;
			}
			else{
				return System.Convert.ToInt32(myString);
			}
		}

		///<summary></summary>
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

		///<summary></summary>
		public static string PString (string myString){
			return myString;
		}
		
		///<summary></summary>
		public static string PTime (string myTime){
			return DateTime.Parse(myTime).ToString("HH:mm:ss");
		}

	}//end class PIn

	/*=========================================================================================
	=================================== class POut ===========================================*/

	///<summary>Converts various datatypes into strings formatted correctly for MySQL.</summary>
	public class POut{
		///<summary></summary>
		public static string PBool (bool myBool){
			if (myBool==true){
				return "1";
			}
			else{
				return "0";
			}
		}

		///<summary></summary>
		public static string PByte (byte myByte){
			return myByte.ToString();
		}

		///<summary></summary>
		public static string PDateT(DateTime myDateT){
			try{
				return myDateT.ToString("yyyy-MM-dd HH:mm:ss",new DateTimeFormatInfo());
			}
			catch{
				return "";//this actually saves zero's to the database
			}
		}

		///<summary></summary>
		public static string PDate(DateTime myDate){
			try{
				return myDate.ToString("yyyy-MM-dd",new DateTimeFormatInfo());
			}
			catch{
				//return "0000-00-00";
				return "";//this saves zeros to the database
			}
		}

		///<summary></summary>
		public static string PDouble (double myDouble){
			return myDouble.ToString();
		}

		///<summary></summary>
		public static string PInt (int myInt){
			return myInt.ToString();
		}

		///<summary></summary>
		public static string PFloat(float myFloat){
			return myFloat.ToString();
		}

		///<summary></summary>
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

		///<summary></summary>
		public static string PTime (string myTime){
			return DateTime.Parse(myTime).ToString("HH:mm:ss");
		}

	}//end class POut

	/*=========================================================================================
	=================================== class Patients ===========================================*/
	///<summary></summary>
	public class Patients:DataClass{
		///<summary>Always test this to see if a patient is loaded, not patient.Cur.PatNum.</summary>
		public static bool PatIsLoaded=false;
		///<summary>Current patient.</summary>
		public static Patient Cur;
		///<summary>Stores limited information about a patient. Filled first via GetLim.</summary>
		public static Patient Lim;
		///<summary>Stores formatted LName, FName, MI for the Lim patient.</summary>
		public static string LimName;
		///<summary>A list of patients in one family (sharing a GuarantorNum)</summary>
		public static Patient[] FamilyList;
		///<summary>Used in the Select Patient window.</summary>
		public static Patient[] PtList;
		///<summary>Used in the Aging window.</summary>
		public static PatAging[] AgingList;
		///<summary>The index within the FamilyList of the Guarantor. Might not be necessary anymore since the guarantor should always be index 0.</summary>
		public static int GuarIndex;
		///<summary></summary>
		public static RecallItem[] RecallList;
		///<summary>A list of all patient names. Key=patNum, value=formatted name.  Fill with GetHList.</summary>
		public static Hashtable HList;

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
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

		/// <summary>Gets nine of the most useful fields from the db for the given patnums, saving the data
		/// in Lim and LimName.</summary>
		/// <param name="patNum">The PatNum to use in retrieving the data.</param>
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

		///<summary></summary>
		public static string GetCurNameLF(){
			if(Cur.Preferred=="")
				return Cur.LName+", "+Cur.FName+" "+Cur.MiddleI;
			else
				return Cur.LName+", '"+Cur.Preferred+"' "+Cur.FName+" "+Cur.MiddleI;
		}

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
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
		
		///<summary></summary>
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

		///<summary>Gets names for all patients.</summary>
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

		///<summary></summary>
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

		///<summary></summary>
		public static void UpdateNotesForFam(){
			cmd.CommandText = "UPDATE patient SET " 
				+"addrnote = '"   +POut.PString(Cur.AddrNote)+"'"
				+" WHERE guarantor = '"+POut.PDouble(Cur.Guarantor)+"'";
			NonQ(false);
			//MessageBox.Show(cmd.CommandText);
		}

		///<summary>This is only used in the Billing dialog</summary>
		public static void GetAgingList(string age,int[] billingIndices,bool excludeAddr
			,bool excludeNeg,double excludeLessThan,bool excludeInactive){
			cmd.CommandText=
				"SELECT patnum,Bal_0_30,Bal_31_60,Bal_61_90,BalOver90,BalTotal,InsEst,LName,FName,MiddleI "
				+"FROM patient WHERE ";//actually only gets guarantors since others are 0.
			if(excludeInactive){
				cmd.CommandText+="(patstatus != '2') && ";
			}
			cmd.CommandText+="(BalTotal - InsEst > '"+excludeLessThan.ToString()+"'";
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

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
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
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static int GetProvForCur(){
			if(Cur.PriProv!=0)
				return Cur.PriProv;
			if(PIn.PInt(((Pref)Prefs.HList["PracticeDefaultProv"]).ValueString)==0){
				MessageBox.Show(Lan.g("Patients","Please set a default provider in the practice setup window."));
				return Providers.List[0].ProvNum;
			}
			return PIn.PInt(((Pref)Prefs.HList["PracticeDefaultProv"]).ValueString);
		}

		///<summary></summary>
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

	///<summary>Corresponds to the patient table in the database.</summary>
	public struct Patient{
		///<summary>Primary key.</summary>
		public int    PatNum;
		///<summary>Last name.</summary>
		public string LName;
		///<summary>First name.</summary>
		public string FName;
		///<summary>Middle initial or name.</summary>
		public string MiddleI;
		///<summary>Preferred name.</summary>
		public string Preferred;
		///<summary>See the PatientStatus enumeration.</summary>
		public PatientStatus PatStatus;
		///<summary>See the PatientGender enumeration.</summary>
		public PatientGender Gender;
		///<summary>See the PatientPosition enumeration.</summary>
		public PatientPosition Position;
		///<summary></summary>
		public DateTime Birthdate;
		///<summary>9 digits, no dashes.</summary>
		public string SSN;
		///<summary></summary>
		public string Address;
		///<summary></summary>
		public string Address2;
		///<summary></summary>
		public string City;
		///<summary>2 Char in USA</summary>
		public string State;
		///<summary></summary>
		public string Zip;
		///<summary>Includes any punctuation</summary>
		public string HmPhone;
		///<summary></summary>
		public string WkPhone;
		///<summary></summary>
		public string WirelessPhone;
		///<summary>Foreign key to patient.PatNum.  Head of household.</summary>
		public int    Guarantor;
		///<summary>Derived from Birthdate.  Not in the database table.</summary>
		public string Age;
		///<summary>Single char. Shows in appointment book.</summary>
		public string CreditType;
		///<summary></summary>
		public string Email;
		///<summary></summary>
		public string Salutation;
		///<summary>Foreign key to insplan.PlanNum.  Primary insurance.</summary>
		public int PriPlanNum;//
		///<summary>Relationship to subscriber for primary insurance.  See the Relat enumeration.</summary>
		public Relat PriRelationship;
		///<summary>Foreign key to insplan.PlanNum.  Secondary insurance.</summary>
		public int SecPlanNum;//
		///<summary>Relationship to subscriber for secondary insurance.</summary>
		public Relat SecRelationship;
		///<summary>Current patient balance.(not family)</summary>
		public double EstBalance;
		///<summary>May be 0(none) or -1(done), otherwise it is the foreign key to appointment.AptNum.  This is the appointment that will show in the Chart module and in the Next appointment tracker.  It will never show in the Appointments module.</summary>
		public int NextAptNum;//
		///<summary>Foreign key to provider.ProvNum.  The patient's primary provider.</summary>
		public int PriProv;
		///<summary>Foreign key to provider.ProvNum.  Secondary provider (hygienist)</summary>
		public int SecProv;//
		///<summary>Foreign key to definition.DefNum.  Fee schedule for this patient.</summary>
		public int FeeSched;
		///<summary>Foreign key to definition.DefNum.  Must have a value, or the patient will not show on some reports.</summary>
		public int BillingType;
		///<summary>Months between recalls.</summary>
		public int RecallInterval;
		///<summary>Foreign key to Definition.DefNum, or 0 for none.</summary>
		public int RecallStatus;
		///<summary>Name of folder where images will be stored. Not editable for now.</summary>
		public string ImageFolder;
		///<summary>Address or phone note.</summary>
		public string AddrNote;
		///<summary>Family financial urgent note.  Only stored with guarantor, and shared for family.</summary>
		public string FamFinUrgNote;
		///<summary>Individual patient note for Urgent medical.</summary>
		public string MedUrgNote;
		///<summary>Individual patient note for Appointment module note.</summary>
		public string ApptModNote;
		///<summary>Single char for Nonstudent, Parttime, or Fulltime.  Blank=Nonstudent</summary>
		public string StudentStatus;
		///<summary></summary>
		public string SchoolName;
		///<summary>Max 15 char.  Used for reference to previous programs.</summary>
		public string ChartNumber;
		///<summary></summary>
		public string MedicaidID;
		///<summary>Aging numbers are for entire family.  Only stored with guarantor.</summary>
		public double Bal_0_30;
		///<summary></summary>
		public double Bal_31_60;
		///<summary></summary>
		public double Bal_61_90;
		///<summary></summary>
		public double BalOver90;
		///<summary>Insurance Estimate for entire family.</summary>
		public double InsEst;
		///<summary>Teeth to display in chart as primary. eg: "1,2,3,4,5,12,13"</summary>
		public string PrimaryTeeth;
		///<summary>For entire family. Stored with guarantor.</summary>
		public double BalTotal;
	}//end struct Patient

	///<summary>Not a database table.  Just used for running reports.</summary>
	public struct PatAging{
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

	/*=========================================================================================
		=================================== class PatientNotes ===========================================*/
	///<summary></summary>
	public class PatientNotes:DataClass{
		///<summary></summary>
		public static PatientNote Cur;
		
		///<summary></summary>
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

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE patientnote SET "
				//+ "apptphone = '"   +POut.PString(Cur.ApptPhone)+"'"
				+ "medical = '"    +POut.PString(Cur.Medical)+"'"
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

		///<summary></summary>
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

	///<summary>Corresponds to the patientnote table in the database.</summary>
	public struct PatientNote{
		///<summary></summary>
		public int PatNum;
		///<summary>Only one note per family stored with guarantor.</summary>
		public string FamFinancial;
		///<summary>No longer used.</summary>
		public string ApptPhone;
		///<summary>Medical Summary</summary>
		public string Medical;
		///<summary>Service notes</summary>
		public string Service;
		///<summary>Complete current Medical History</summary>
		public string MedicalComp;
	}

	/*=========================================================================================
		=================================== class Payments ==========================================*/

	///<summary></summary>
	public class Payments:DataClass{
		///<summary></summary>
		public static Payment Cur;

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
		public static void DeleteCur(){//deletes Cur
			cmd.CommandText = "DELETE from payment WHERE payNum = '"+Cur.PayNum.ToString()+"'";
			NonQ(false);
		}

		///<summary></summary>
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

	///<summary></summary>
	public struct Payment{
		///<summary></summary>
		public int PayNum;
		///<summary></summary>
		public int PayType;
		///<summary></summary>
		public DateTime PayDate;
		///<summary></summary>
		public double PayAmt;
		///<summary></summary>
		public string CheckNum;
		///<summary></summary>
		public string BankBranch;
		///<summary></summary>
		public string PayNote;
		///<summary></summary>
		public bool IsSplit;//(between patients.  Not including discounts)
		///<summary></summary>
		public int PatNum;//Selected automatically from the splits.  Just to make reports easier
	}

	/*=========================================================================================
	=================================== class PayPlans ==========================================*/

	///<summary></summary>
	public class PayPlans:DataClass{
		///<summary>List of all payplans for a given patient, whether they are the guarantor or the patient.  This is also used in UpdateAll to store all payment plans in entire database.</summary>
		public static PayPlan[] List;
		///<summary></summary>
		public static PayPlan Cur;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from payplan"
				+" WHERE patnum = '"+Patients.Cur.PatNum+"'"
				+" || guarantor = '"+Patients.Cur.PatNum+"' ORDER BY payplandate";
			FillTable();
			List=new PayPlan[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].PayPlanNum    = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum        = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].Guarantor     = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].PayPlanDate   = PIn.PDate  (table.Rows[i][3].ToString());
				List[i].TotalAmount   = PIn.PDouble(table.Rows[i][4].ToString());
				List[i].APR           = PIn.PDouble(table.Rows[i][5].ToString());
				List[i].MonthlyPayment= PIn.PDouble(table.Rows[i][6].ToString());
				List[i].Term          = PIn.PInt   (table.Rows[i][7].ToString());
				List[i].CurrentDue    = PIn.PDouble(table.Rows[i][8].ToString());
				List[i].DateFirstPay  = PIn.PDate  (table.Rows[i][9].ToString());
				List[i].DownPayment   = PIn.PDouble(table.Rows[i][10].ToString());
				List[i].Note          = PIn.PString(table.Rows[i][11].ToString());
			}//end for
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE payplan SET " 
				+ "patnum = '"         +POut.PInt   (Cur.PatNum)+"'"
				+ ",guarantor = '"     +POut.PInt   (Cur.Guarantor)+"'"
				+ ",payplandate = '"   +POut.PDate  (Cur.PayPlanDate)+"'"
				+ ",totalamount = '"   +POut.PDouble(Cur.TotalAmount)+"'"
				+ ",apr = '"           +POut.PDouble(Cur.APR)+"'"
				+ ",monthlypayment = '"+POut.PDouble(Cur.MonthlyPayment)+"'"
				+ ",term = '"          +POut.PInt   (Cur.Term)+"'"
				+ ",currentdue = '"    +POut.PDouble(Cur.CurrentDue)+"'"
				+ ",datefirstpay = '"  +POut.PDate  (Cur.DateFirstPay)+"'"
				+ ",downpayment = '"   +POut.PDouble(Cur.DownPayment)+"'"
				+ ",note = '"          +POut.PString(Cur.Note)+"'"
				+" WHERE payplanNum = '" +POut.PInt   (Cur.PayPlanNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO payplan (patnum,guarantor,payplandate,totalamount,"
				+"apr,monthlypayment,term,currentdue,datefirstpay,downpayment,note) VALUES("
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PInt   (Cur.Guarantor)+"', "
				+"'"+POut.PDate  (Cur.PayPlanDate)+"', "
				+"'"+POut.PDouble(Cur.TotalAmount)+"', "
				+"'"+POut.PDouble(Cur.APR)+"', "
				+"'"+POut.PDouble(Cur.MonthlyPayment)+"', "
				+"'"+POut.PInt   (Cur.Term)+"', "
				+"'"+POut.PDouble(Cur.CurrentDue)+"', "
				+"'"+POut.PDate  (Cur.DateFirstPay)+"', "
				+"'"+POut.PDouble(Cur.DownPayment)+"', "
				+"'"+POut.PString(Cur.Note)+"')";
			NonQ(false);
		}

		///<summary>Must have already verified that there are no paysplits attached.  Called from FormPayPlan.</summary>
		public static void DeleteCur(){
			cmd.CommandText="DELETE FROM payplan WHERE payplannum = '"
				+Cur.PayPlanNum.ToString()+"'";
			NonQ(false);
		}

		/// <summary>Recalculates the CurrentDue for all payment plans based on the specified date.  Does not take into account any payments made.</summary>
		public static void UpdateAll(DateTime date){
			cmd.CommandText =
				"SELECT * FROM payplan";
			FillTable();
			List=new PayPlan[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].PayPlanNum    = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum        = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].Guarantor     = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].PayPlanDate   = PIn.PDate  (table.Rows[i][3].ToString());
				List[i].TotalAmount   = PIn.PDouble(table.Rows[i][4].ToString());
				List[i].APR           = PIn.PDouble(table.Rows[i][5].ToString());
				List[i].MonthlyPayment= PIn.PDouble(table.Rows[i][6].ToString());
				List[i].Term          = PIn.PInt   (table.Rows[i][7].ToString());
				List[i].CurrentDue    = PIn.PDouble(table.Rows[i][8].ToString());
				List[i].DateFirstPay  = PIn.PDate  (table.Rows[i][9].ToString());
				List[i].DownPayment   = PIn.PDouble(table.Rows[i][10].ToString());
				//List[i].Note          = PIn.PString(table.Rows[i][11].ToString());
			}//end for
			for(int i=0;i<List.Length;i++){
				Cur=List[i];
				cmd.CommandText="UPDATE payplan SET CurrentDue = '"
					+GetAmtDue().ToString()+"' WHERE PayPlanNum = '"+POut.PInt(Cur.PayPlanNum)+"'";
				NonQ(false);
			}
		}

		///<summary>Gets the amount due for the current payment plan based on today's date.  It is simply the number of months x monthly payment.  Includes interest, but does not include payments made so far.</summary>
		public static double GetAmtDue(){
			return Cur.DownPayment+Cur.MonthlyPayment*GetMonthsDue();
			//return retVal;
		}

		/// <summary>For the Cur payment plan, gets the number of months, rounded up, between the first payment date and today's date.  This is the number of payments that are due.  Used from GetAmtDue() and from FormPayPlan.</summary>
		public static int GetMonthsDue(){
			for(int i=0;i<100;i++){
				//MessageBox.Show(Cur.DateFirstPay.AddMonths(i).ToString()+","+DateTime.Today.ToString());
				if(Cur.DateFirstPay.AddMonths(i)>DateTime.Today){
					return i;
				}
			}
			return 0;
		}

		/// <summary>Used from PayPlan window to get the amount paid so far on one payment plan.</summary>
		/// <param name="payPlanNum"></param>
		public static double GetAmtPaid(int payPlanNum){
			if(payPlanNum==0){//for a new paymentPlan
				return 0;
			}
			cmd.CommandText="SELECT SUM(paysplit.splitamt) FROM paysplit "
				+"WHERE paysplit.payplannum = '"+payPlanNum.ToString()+"' "
				+"GROUP BY paysplit.payplannum";
			FillTable();
			if(table.Rows.Count==0){
				return 0;
			}
			return PIn.PDouble(table.Rows[0][0].ToString());
		}

		///<summary>Must make sure Refresh is done first.  Returns the sum of all payment plan entries for guarantor and/or patient.</summary>
		public static double ComputeBal(){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				//one or both of these conditions may be met:
				if(List[i].Guarantor==Patients.Cur.PatNum){
					retVal+=List[i].CurrentDue;
				}
				if(List[i].PatNum==Patients.Cur.PatNum){
					retVal-=List[i].TotalAmount;
				}
			}
			return retVal;
		}


	}

	/// <summary>Corresponds to the payplan table in the database.  Each row represents one signed agreement to make payments.</summary>
	public struct PayPlan{
		/// <summary>Primary key</summary>
		public int PayPlanNum;
		/// <summary>Foreign key to  patient.PatNum.  The patient who had the treatment done.</summary>
		public int PatNum;
		/// <summary>Foreign key to  patient.PatNum.  The person responsible for the payments.  
		/// Does not need to be in the same family as the patient.</summary>
		public int Guarantor;
		/// <summary>Date that the payment plan was started.</summary>
		public DateTime PayPlanDate;
		/// <summary>Total amount financed.</summary>
		public double TotalAmount;
		/// <summary>Annual percentage rate.  eg 18.  This does not take into consideration any late payments, but only the percentage used to determine the current amount due.</summary>
		public double APR;
		/// <summary>Amount of payment agreed to for each month</summary>
		public double MonthlyPayment;
		/// <summary>Number of months agreed for payment.</summary>
		public int Term;
		/// <summary>The current amount due not taking into account payments made.  Updated every time it's loaded.  Also updated using the update tool.</summary>
		public double CurrentDue;
		/// <summary>Date first payment is due.</summary>
		public DateTime DateFirstPay;
		/// <summary>The amount of downpayment not counting the first payment.</summary>
		public double DownPayment;
		///<summary>Generally used to archive the terms so they don't accidently get changed.</summary>
		public string Note;
	}

	/*=========================================================================================
	=================================== class PaySplits ==========================================*/

	///<summary></summary>
	public class PaySplits:DataClass{
		///<summary></summary>
		public static PaySplit[] List;
		///<summary></summary>
		public static PaySplit[] PaymentList;
		///<summary></summary>
		public static PaySplit Cur;

		///<summary></summary>
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
				List[i].PayPlanNum  = PIn.PInt   (table.Rows[i][8].ToString());
			}//end for
		}

		///<summary></summary>
		public static void UpdateCur(){//updates Cur
			cmd.CommandText = "UPDATE paysplit SET " 
				+ "splitamt = '"     +POut.PDouble(Cur.SplitAmt)+"'"
				+ ",patnum = '"      +POut.PInt   (Cur.PatNum)+"'"
				+ ",procdate = '"    +POut.PDate  (Cur.ProcDate)+"'"
				+ ",paynum = '"      +POut.PInt   (Cur.PayNum)+"'"
				+ ",isdiscount = '"  +POut.PBool  (Cur.IsDiscount)+"'"
				+ ",discounttype = '"+POut.PInt   (Cur.DiscountType)+"'"
				+ ",provnum = '"     +POut.PInt   (Cur.ProvNum)+"'"
				+ ",payplannum = '"  +POut.PInt   (Cur.PayPlanNum)+"'"
				+" WHERE splitNum = '" +POut.PInt (Cur.SplitNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){//saves Cur
			cmd.CommandText = "INSERT INTO paysplit (splitamt,patnum,procdate, "
				+"paynum,isdiscount,discounttype,provnum,payplannum) VALUES("
				+"'"+POut.PDouble(Cur.SplitAmt)+"', "
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PDate  (Cur.ProcDate)+"', "
				+"'"+POut.PInt   (Cur.PayNum)+"', "
				+"'"+POut.PBool  (Cur.IsDiscount)+"', "
				+"'"+POut.PInt   (Cur.DiscountType)+"', "
				+"'"+POut.PInt   (Cur.ProvNum)+"', "
				+"'"+POut.PInt   (Cur.PayPlanNum)+"')";
			NonQ(false);
		}

		///<summary></summary>
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
				PaymentList[i].PayPlanNum  = PIn.PInt   (table.Rows[i][8].ToString());
			}//end for
		}

		///<summary></summary>
		public static void DeleteCur(){//deletes Cur
			//Cur=List[Selected];
			//PutBal(Cur.PatNum,Cur.ProcDate,-Cur.SplitAmt);
			cmd.CommandText = "DELETE from paysplit WHERE splitNum = '"+Cur.SplitNum.ToString()+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static double ComputeBal(){//must make sure Refresh is done first
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				retVal+=List[i].SplitAmt;
			}
			return retVal;
		}

	}

	///<summary>Corresponds to the paysplit table in the database.</summary>
	public struct PaySplit{
		///<summary>Primary key.</summary>
		public int SplitNum;
		///<summary>Amount of split.</summary>
		public double SplitAmt;
		///<summary>Foreign key to patient.PatNum.</summary>
		public int PatNum;
		///<summary>Procedure date.  Not necessarily when the payment was made.  This field will be eliminated when we add a ProcNum field because allowing splits to have different dates makes accurate aging almost impossible.</summary>
		public DateTime ProcDate;//
		///<summary>Foreign key to payment.PayNum.  Every paysplit must be linked to a payment.</summary>
		public int PayNum;
		///<summary>True if discount rather than a payment.</summary>
		public bool IsDiscount;
		///<summary>Foreign key to definition.DefNum if a discount.</summary>
		public int DiscountType;
		///<summary>Foreign key to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>Foreign key to payplan.PayPlanNum.  0 if not attached to a payplan.</summary>
		public int PayPlanNum;
	}

	/*=========================================================================================
	=================================== class Permissions ==========================================*/
  
	///<summary></summary>
	public class Permissions:DataClass{
		///<summary></summary>
		public static Permission Cur;
		///<summary></summary>
		public static Permission[] List;

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE permission SET "
				+"name ='"              +POut.PString(Cur.Name)+"'"
				+",requirespassword ='" +POut.PBool  (Cur.RequiresPassword)+"'"
				+",beforedate='"        +POut.PDate  (Cur.BeforeDate)+"'"
				+",beforedays ='"       +POut.PInt   (Cur.BeforeDays)+"'"
				+" WHERE permissionnum = '"+POut.PInt(Cur.PermissionNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
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

		///<summary></summary>
		public static void DisableSecurity(){
			cmd.CommandText="UPDATE permission SET RequiresPassword = '0' "
				+"WHERE Name = 'Security Administration'";
			NonQ(false);
			Refresh();
		}

		///<summary></summary>
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

	///<summary>Corresponds to the permission table in the database.</summary>
	public struct Permission{
		///<summary>Primary key.</summary>
		public int PermissionNum;
		///<summary>Description.  Not user editable.</summary>
		public string Name;
		///<summary>If true, will display user/password dialog for that feature.</summary>
		public bool RequiresPassword;
		///<summary>Only displays user/password dialog if item date is before this date.</summary>
		public DateTime BeforeDate;
		///<summary>Can be -1 for "do not calculate days". 0 days=always require password. Only displays user/password dialog if item is before given number of days</summary>
		public int BeforeDays;//
	}

	/*=========================================================================================
	=================================== class Prefs ==========================================*/

	///<summary></summary>
	public class Prefs:DataClass{
		///<summary></summary>
		public static Hashtable HList;
		///<summary></summary>
		public static Pref Cur;
		//private string DataBaseVersion=Application.ProductVersion;//was "1.0.0";

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE preference SET "
				+"valuestring = '"  +POut.PString(Cur.ValueString)+"'"
				+" WHERE prefname = '"+POut.PString(Cur.PrefName)+"'";
			NonQ(false);
		}

		///<summary></summary>
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

		///<summary></summary>
		public static void Unlock(){
			con.Close();
		}		
	}

	///<summary>Corresponds to the preference table in the database.  Stores small bits of data for a wide variety of purposes.</summary>
	public struct Pref{
		///<summary>Primary key.</summary>
		public string PrefName;//
		///<summary>The stored value.</summary>
		public string ValueString;
	}


	/*=========================================================================================
		=================================== class ProcButtons===========================================*/

	///<summary></summary>
	public class ProcButtons:DataClass{
		///<summary></summary>
		public static ProcButton Cur;
		///<summary></summary>
		public static Hashtable HList;
		///<summary></summary>
		public static ProcButton[] List;

		///<summary></summary>
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

		///<summary></summary>
		public static void InsertCur(){
			//must have already checked ADACode for nonduplicate.
			cmd.CommandText = "INSERT INTO procbutton (description,itemorder) VALUES("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PInt   (Cur.ItemOrder)+"')";
				//+"'"+POut.PBool  (Cur.IsFourQuad)+"')";
			NonQ(true);
			Cur.ProcButtonNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE procbutton SET " 
				+ "description = '" +POut.PString(Cur.Description)+"'"
				+ ",itemorder = '"  +POut.PInt   (Cur.ItemOrder)+"'"  
				//+ ",isfourquad = '" +POut.PBool  (Cur.IsFourQuad)+"'"     
				+" WHERE procbuttonnum = '"+POut.PInt(Cur.ProcButtonNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from procbuttonitem WHERE procbuttonnum = '"
				+POut.PInt(Cur.ProcButtonNum)+"'";
			NonQ(false);
			cmd.CommandText = "DELETE from procbutton WHERE procbuttonnum = '"
				+POut.PInt(Cur.ProcButtonNum)+"'";
			NonQ(false);
		}

	}

	///<summary>Corresponds to the procbutton table in the database.</summary>
	public struct ProcButton{
		///<summary>Primary key</summary>
		public int ProcButtonNum;
		///<summary>The text to show on the button.</summary>
		public string Description;
		///<summary>Order that they will show in the Chart module.</summary>
		public int ItemOrder;
	}

	/*=========================================================================================
		=================================== class ProcButtonItems===========================================*/

	///<summary></summary>
	public class ProcButtonItems:DataClass{
		///<summary></summary>
		public static ProcButtonItem Cur;
		///<summary></summary>
		public static Hashtable HList;
		///<summary></summary>
		public static ProcButtonItem[] List;
		//public static ProcButtonItem[] ListForButton;
		//these two used when editing buttons:
		///<summary></summary>
		public static ArrayList ALadaCodes;//string, because some buttonItems are adacodes
		///<summary></summary>
		public static ArrayList ALautoCodes;//int, and some are autocodes
		//these two used when clicking buttons:
		///<summary></summary>
		public static string[] adaCodeList;
		///<summary></summary>
		public static int[] autoCodeList;
		//private static ArrayList ALlist;

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
		public static void InsertCur(){
			//must have already checked ADACode for nonduplicate.
			cmd.CommandText = "INSERT INTO procbuttonitem (procbuttonnum,adacode,autocodenum) VALUES("
				+"'"+POut.PInt   (Cur.ProcButtonNum)+"', "
				+"'"+POut.PString(Cur.ADACode)+"', "
				+"'"+POut.PInt   (Cur.AutoCodeNum)+"')";  

			NonQ(false);
		}

		///<summary></summary>
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

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from procbuttonitem WHERE procbuttonitemnum = '"+POut.PInt(Cur.ProcButtonItemNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteAllForCur(){
			cmd.CommandText = "DELETE from procbuttonitem WHERE procbuttonnum = '"+POut.PInt(ProcButtons.Cur.ProcButtonNum)+"'";
			NonQ(false);
		}

	}//end class ProcButtonItems

	///<summary>Corresponds to the procbuttonitem table in the database.</summary>
	public struct ProcButtonItem{
		///<summary>Primary key.</summary>
		public int ProcButtonItemNum;
		///<summary>Foreign key to procbutton.ProcButtonNum.</summary>
		public int ProcButtonNum;
		///<summary>Foreign key to procedurecode.ADACode.</summary>
		public string ADACode;
		///<summary>Foreign key to autocode.AutoCodeNum.</summary>
		public int AutoCodeNum;
	}


	/*=========================================================================================
	=================================== class ProcedureCodes ===========================================*/

	///<summary></summary>
	public class ProcedureCodes:DataClass{
		///<summary></summary>
		public static DataTable tableStat;
		///<summary></summary>
		public static ProcedureCode[] ProcList;//grouped by category
		///<summary></summary>
		public static ArrayList RecallAL;
		///<summary></summary>
		public static ProcedureCode Cur;
		///<summary></summary>
		public static Hashtable HList;//key:AdaCode, value:ProcedureCode 
		///<summary></summary>
		public static ProcedureCode[] List;

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
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

	///<summary>Corresponds to the procedurecode table in the database.</summary>
	public struct ProcedureCode{
		///<summary>Primary key.  Does not have to be a valid ADACode. Can also include ADACodes with suffixes which get automatically trimmed when sending claims.</summary>
		public string ADACode;
		///<summary>The main description.</summary>
		public string Descript;
		///<summary>Abbreviated description.</summary>
		public string AbbrDesc;
		///<summary>X's and /'s describe Dr's time and assistant's time in 10 minute increments.</summary>
		public string ProcTime;
		///<summary>Foreign key to definition.DefNum.</summary>
		public int ProcCat;
		///<summary>See the TreatmentArea enumeration.</summary>
		public TreatmentArea TreatArea;
		///<summary>True for extractions so teeth will show as missing if complete or existing.</summary>
		public bool RemoveTooth;
		///<summary>Triggers recall in 6 months.</summary>
		public bool SetRecall;
		///<summary>If true, do not usually bill this procedure to insurance.</summary>
		public bool NoBillIns;
		///<summary>Not currently in use anywhere.  Might be used to designate a bridge, crown, or denture.</summary>
		public bool IsProsth;
		///<summary>The default procedure note to copy when marking complete.</summary>
		public string DefaultNote;
		///<summary>Identifies hygiene procedures so that the correct provider can be selected.</summary>
		public bool IsHygiene;
		///<summary>Foreign key to GraphicType</summary>
		public int GTypeNum;
		///<summary>For Medicaid.  There may be more later.</summary>
		public string AlternateCode1;
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
		private static string ConvertProcToString(string aDACode,string surf, string toothNum){
			string strLine="";
			switch (ProcedureCodes.GetProcCode(aDACode).TreatArea){
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

	}//end class Procedures

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
	
	///<summary>Not a database table.</summary>
	public struct ProcDesc{
		///<summary></summary>
		public int AptNum;
		///<summary></summary>
		public string[] ProcLines;
	}

	/*=========================================================================================
	=================================== class Programs ==========================================*/

	///<summary></summary>
	public class Programs:DataClass{
		///<summary></summary>
		public static Hashtable HList;
		///<summary></summary>
		public static Program Cur;
		///<summary></summary>
		public static Program[] List;

		///<summary></summary>
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

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE program SET "
				+"progname = '"   +POut.PString(Cur.ProgName)+"'"
				+",progdesc  = '" +POut.PString(Cur.ProgDesc)+"'"
				+",enabled  = '"  +POut.PBool  (Cur.Enabled)+"'"
				+" WHERE programnum = '"+POut.PInt(Cur.ProgramNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO program (progname,progdesc,enabled"
				+") VALUES("
				+"'"+POut.PString(Cur.ProgName)+"', "
				+"'"+POut.PString(Cur.ProgDesc)+"', "
				+"'"+POut.PBool  (Cur.Enabled)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
			//Cur.ProgramNum=InsertID;
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from program WHERE programnum = '"+Cur.ProgramNum.ToString()+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static bool IsEnabled(string progName){
			if(HList.ContainsKey(progName) && ((Program)HList[progName]).Enabled){
				return true;
			}
			return false;
		}

	}

	///<summary>Corresponds to the program table in the database.</summary>
	public struct Program{
		///<summary>Primary key.</summary>
		public int ProgramNum;
		///<summary>Unique name can not change.</summary>
		public string ProgName;
		///<summary>Description that shows.</summary>
		public string ProgDesc;
		///<summary>True if enabled.</summary>
		public bool Enabled;
		//IsSystem is still in the database, but no longer used.
	}

	/*=========================================================================================
	=================================== class ProgramProperties ==========================================*/

	///<summary>to be completed in the next version.</summary>
	public class ProgramProperties:DataClass{
		
	}

	///<summary>Corresponds to the programproperty table in the database(does not exist yet).  Stores settings for linking to other programs.</summary>
	public struct ProgramProperty{
		///<summary>Primary key.</summary>
		public int ProgramPropertyNum;
		///<summary></summary>
		public int ProgramNum;
		///<summary></summary>
		public string PropertyDesc;
		///<summary></summary>
		public string PropertyValue;
	}

	/*=========================================================================================
	=================================== class Providers ==========================================*/

	///<summary></summary>
	public class Providers:DataClass{
		///<summary></summary>
		public static Provider[] ListLong;
		///<summary></summary>
		public static Provider[] List;
		///<summary></summary>
		public static Provider Cur;
		///<summary></summary>
		public static int Selected;

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from provider WHERE provnum = '"+Cur.ProvNum.ToString()+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static string GetAbbr(int provNum){
			string retStr="";
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ProvNum==provNum){
					retStr=ListLong[i].Abbr;
				}
			}
			return retStr;
		}

		///<summary></summary>
		public static Color GetColor(int provNum){
			Color retCol=Color.White;
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ProvNum==provNum){
					retCol=ListLong[i].ProvColor;
				}
			}
			return retCol;
		}

		///<summary></summary>
		public static bool GetIsSec(int provNum){
			bool retVal=false;
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ProvNum==provNum){
					retVal=ListLong[i].IsSecondary;
				}
			}
			return retVal;
		}

		///<summary></summary>
		public static int GetIndexLong(int provNum){
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ProvNum==provNum){
					return i;
				}
			}
			return -1;//should NEVER return a -1
		}

		///<summary></summary>
		public static int GetIndex(int provNum){
			//Gets the index of the provider in short list (visible providers)
			for(int i=0;i<List.Length;i++){
				if(List[i].ProvNum==provNum){
					return i;
				}
			}
			return -1;
		}

		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
		public static void SetOrder(int mySelNum, int myItemOrder){
			Provider temp=ListLong[mySelNum];
			temp.ItemOrder=myItemOrder;
			Cur=temp;
			UpdateCur();
		}
	}
	
	///<summary>Corresponds to the provider table in the database.</summary>
	public struct Provider{
		///<summary>Primary key.</summary>
		public int ProvNum;
		///<summary>Abbreviation.</summary>
		public string Abbr;
		///<summary>Order that provider will show in lists.</summary>
		public int ItemOrder;
		///<summary>Last name.</summary>
		public string LName;
		///<summary>First name.</summary>
		public string FName;
		///<summary>Middle inital or name.</summary>
		public string MI;
		///<summary>eg. DMD or DDS</summary>
		public string Title;
		///<summary>Foreign key to Definition.DefNum.</summary>
		public int FeeSched;
		///<summary>See the DentalSpecialty enumeration.</summary>
		public DentalSpecialty Specialty;
		///<summary>or TIN.  No punctuation</summary>
		public string SSN;
		///<summary>can include punctuation</summary>
		public string StateLicense;
		///<summary></summary>
		public string DEANum;
		///<summary>True if hygeinist.</summary>
		public bool IsSecondary;//
		///<summary>Color that shows in appointments</summary>
		public Color ProvColor;
		///<summary>If true, provider will not show on any lists</summary>
		public bool IsHidden;
		///<summary>True if the SSN field is actually a Tax ID Num</summary>
		public bool UsingTIN;
		///<summary></summary>
		public string BlueCrossID;
		///<summary>Signature on file</summary>
		public bool SigOnFile;//
		///<summary></summary>
		public string Password;
		///<summary></summary>
		public string UserName;
		///<summary></summary>
		public string MedicaidID;
	}


}










