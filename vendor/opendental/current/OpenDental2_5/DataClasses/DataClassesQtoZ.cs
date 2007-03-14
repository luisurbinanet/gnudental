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
using System.Windows.Forms;

namespace OpenDental{
	
	


	/*================================================================================================
		=================================== class RefAttaches ==========================================*/
///<summary></summary>
	public class RefAttaches:DataClass{
		///<summary></summary>
		public static RefAttach[] List;//for this patient only
		///<summary></summary>
		public static RefAttach Cur;
		///<summary></summary>
		public static Hashtable HList;//key:refAttachNum, value:RefAttach

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * FROM refattach"
				+" WHERE patnum = '"+Patients.Cur.PatNum+"'"
				+" ORDER BY itemorder";
			FillTable();
			List=new RefAttach[table.Rows.Count];
			HList=new Hashtable();
			for(int i=0;i<table.Rows.Count;i++){
				List[i].RefAttachNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ReferralNum = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].PatNum      = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].ItemOrder   = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].RefDate     = PIn.PDate  (table.Rows[i][4].ToString());
				List[i].IsFrom      = PIn.PBool  (table.Rows[i][5].ToString());       
				HList.Add(List[i].RefAttachNum,List[i]);
			}
		}
	
		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE refattach SET " 
				+ "referralnum = '" +POut.PInt   (Cur.ReferralNum)+"'"
				+ ",patnum = '"     +POut.PInt   (Cur.PatNum)+"'"
				+ ",itemorder = '"  +POut.PInt   (Cur.ItemOrder)+"'"
				+ ",refdate = '"    +POut.PDate  (Cur.RefDate)+"'"
				+ ",isfrom = '"     +POut.PBool  (Cur.IsFrom)+"'"
				+" WHERE RefAttachNum = '" +POut.PInt(Cur.RefAttachNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO refattach (referralnum,patnum,"
				+"itemorder,refdate,IsFrom) VALUES("
				+"'"+POut.PInt   (Cur.ReferralNum)+"', "
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PInt   (Cur.ItemOrder)+"', "
				+"'"+POut.PDate  (Cur.RefDate)+"', "
				+"'"+POut.PBool  (Cur.IsFrom)+"')";
			NonQ(true);
			Cur.RefAttachNum=InsertID;
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM refattach "
				+"WHERE refattachnum = '"+Cur.RefAttachNum+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static bool IsReferralAttached(int referralNum){
			cmd.CommandText =
				"SELECT * FROM refattach"
				+" WHERE referralnum = '"+referralNum+"'";
			FillTable();
			if(table.Rows.Count > 0){
				return true;
			}
			else{
				return false;
			}
		}

	}

	///<summary>Corresponds to the refattach table in the database.  Attaches a reference to a patient.</summary>
	public struct RefAttach{  
		///<summary>Primary key.</summary>
		public int RefAttachNum;
		///<summary>Foreign key to referral.ReferralNum.</summary>
		public int ReferralNum;
		///<summary>Foreign key to patient.PatNum.</summary>
		public int PatNum;
		///<summary>Order to display in patient info. Will be automated more in future.</summary>
		public int ItemOrder;
		///<summary>Date of referral.</summary>
		public DateTime RefDate;//
		///<summary>true=from, false=to</summary>
		public bool IsFrom;
	}

	/*==============================================================================================
		=================================== class Referrals ==========================================*/
///<summary></summary>
	public class Referrals:DataClass{
		///<summary></summary>
		public static Referral[] List;//all referrals for all patients
		//should later add a list for single patient, along with a faster refresh sequence.
		///<summary></summary>
		public static Referral Cur;
		private static Hashtable HList;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from referral "
				+"ORDER BY lname";
			FillTable();
			List=new Referral[table.Rows.Count];
			HList=new Hashtable();
			for(int i=0;i<table.Rows.Count;i++){
				List[i].ReferralNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].LName      = PIn.PString(table.Rows[i][1].ToString());
				List[i].FName      = PIn.PString(table.Rows[i][2].ToString());
				List[i].MName      = PIn.PString(table.Rows[i][3].ToString());
				List[i].SSN        = PIn.PString(table.Rows[i][4].ToString());
				List[i].UsingTIN   = PIn.PBool  (table.Rows[i][5].ToString());
				List[i].Specialty  = (DentalSpecialty)PIn.PInt(table.Rows[i][6].ToString());
				List[i].ST         = PIn.PString(table.Rows[i][7].ToString());
				List[i].Telephone  = PIn.PString(table.Rows[i][8].ToString());
				List[i].Address    = PIn.PString(table.Rows[i][9].ToString());
				List[i].Address2   = PIn.PString(table.Rows[i][10].ToString());
				List[i].City       = PIn.PString(table.Rows[i][11].ToString());
				List[i].Zip        = PIn.PString(table.Rows[i][12].ToString());
				List[i].Note       = PIn.PString(table.Rows[i][13].ToString());
				List[i].Phone2     = PIn.PString(table.Rows[i][14].ToString());
				List[i].IsHidden   = PIn.PBool  (table.Rows[i][15].ToString());
				List[i].NotPerson  = PIn.PBool  (table.Rows[i][16].ToString());
				List[i].Title      = PIn.PString(table.Rows[i][17].ToString());
				List[i].EMail      = PIn.PString(table.Rows[i][18].ToString());
				List[i].PatNum     = PIn.PInt   (table.Rows[i][19].ToString());
				HList.Add(List[i].ReferralNum,List[i]);
			}
		}
	
		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE referral SET " 
				+ "lname = '"      +POut.PString(Cur.LName)+"'"
				+ ",fname = '"     +POut.PString(Cur.FName)+"'"
				+ ",mname = '"     +POut.PString(Cur.MName)+"'"
				+ ",ssn = '"       +POut.PString(Cur.SSN)+"'"
				+ ",usingtin = '"  +POut.PBool  (Cur.UsingTIN)+"'"
				+ ",specialty = '" +POut.PInt   ((int)Cur.Specialty)+"'"
				+ ",st = '"        +POut.PString(Cur.ST)+"'"
				+ ",telephone = '" +POut.PString(Cur.Telephone)+"'"
				+ ",address = '"   +POut.PString(Cur.Address)+"'"
				+ ",address2 = '"  +POut.PString(Cur.Address2)+"'"
				+ ",city = '"      +POut.PString(Cur.City)+"'"
				+ ",zip = '"       +POut.PString(Cur.Zip)+"'"
				+ ",note = '"      +POut.PString(Cur.Note)+"'"
				+ ",phone2 = '"    +POut.PString(Cur.Phone2)+"'"
				+ ",ishidden = '"  +POut.PBool  (Cur.IsHidden)+"'"
				+ ",notperson = '" +POut.PBool  (Cur.NotPerson)+"'"
				+ ",title = '"     +POut.PString(Cur.Title)+"'"
				+ ",email = '"     +POut.PString(Cur.EMail)+"'"
				+ ",patnum = '"     +POut.PInt  (Cur.PatNum)+"'"     

				+" WHERE ReferralNum = '" +POut.PInt (Cur.ReferralNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO referral (lname,fname,mname,ssn,usingtin,specialty,st,"
				+"telephone,address,address2,city,zip,note,phone2,ishidden,notperson,title,email,patnum) VALUES("
				+"'"+POut.PString(Cur.LName)+"', "
				+"'"+POut.PString(Cur.FName)+"', "
				+"'"+POut.PString(Cur.MName)+"', "
				+"'"+POut.PString(Cur.SSN)+"', "
				+"'"+POut.PBool  (Cur.UsingTIN)+"', "
				+"'"+POut.PInt   ((int)Cur.Specialty)+"', "
				+"'"+POut.PString(Cur.ST)+"', "
				+"'"+POut.PString(Cur.Telephone)+"', "    
				+"'"+POut.PString(Cur.Address)+"', "
				+"'"+POut.PString(Cur.Address2)+"', "
				+"'"+POut.PString(Cur.City)+"', "
				+"'"+POut.PString(Cur.Zip)+"', "
				+"'"+POut.PString(Cur.Note)+"', "
				+"'"+POut.PString(Cur.Phone2)+"', "
				+"'"+POut.PBool  (Cur.IsHidden)+"', "
				+"'"+POut.PBool  (Cur.NotPerson)+"', "
				+"'"+POut.PString(Cur.Title)+"', "
				+"'"+POut.PString(Cur.EMail)+"', "
				+"'"+POut.PInt   (Cur.PatNum)+"')";
			NonQ(true);
			Cur.ReferralNum=InsertID;
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM referral "
				+"WHERE referralnum = '"+Cur.ReferralNum+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void GetCur(int referralNum){
			if(Referrals.HList.ContainsKey(referralNum)){
				Referrals.Cur=(Referral)Referrals.HList[referralNum];
				return;
			}
			Refresh();//must be outdated
			if(!Referrals.HList.ContainsKey(referralNum)){
				MessageBox.Show("Error.  Referral not found: "+referralNum.ToString());
			}
			Referrals.Cur=(Referral)Referrals.HList[referralNum];
		}

	}

	///<summary>Corresponds to the referral table in the database.</summary>
	public struct Referral{
		///<summary>Primary key.</summary>
		public int ReferralNum;
		///<summary>Last name.</summary>
		public string LName;
		///<summary>First name.</summary>
		public string FName;
		///<summary>Middle name or initial.</summary>
		public string MName;
		///<summary>SSN or TIN, no punctuation.</summary>
		public string SSN;
		///<summary>Specificies if SSN is real SSN.</summary>
		public bool UsingTIN;
		///<summary>See the DentalSpecialty enumeration.</summary>
		public DentalSpecialty Specialty;
		///<summary>State</summary>
		public string ST;
		///<summary>Primary phone, restrictive, must only be 10 digits and only numbers.</summary>
		public string Telephone;
		///<summary></summary>
		public string Address;
		///<summary></summary>
		public string Address2;
		///<summary></summary>
		public string City;
		///<summary></summary>
		public string Zip;
		///<summary>Holds important info about the referral.</summary>
		public string Note;//
		///<summary>Additional phone no restrictions</summary>
		public string Phone2;
		///<summary>Can't delete a referral, but can hide if not needed any more.</summary>
		public bool IsHidden;//
		///<summary>Set to true for referralls such as Yellow Pages.</summary>
		public bool NotPerson;
		///<summary>i.e. DMD or DDS</summary>
		public string Title;
		///<summary></summary>
		public string EMail;
		///<summary>Foreign key to patient.PatNum for referrals that are patients.</summary>
		public int PatNum;
	}

	/*=========================================================================================
		=================================== class RxDefs ==========================================*/
///<summary></summary>
	public class RxDefs:DataClass{
		///<summary></summary>
		public static RxDef[] List;
		///<summary></summary>
		public static RxDef Cur;
	
		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from rxdef"
				+" ORDER BY drug";
			FillTable();
			List=new RxDef[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].RxDefNum   = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Drug       = PIn.PString(table.Rows[i][1].ToString());
				List[i].Sig        = PIn.PString(table.Rows[i][2].ToString());
				List[i].Disp       = PIn.PString(table.Rows[i][3].ToString());
				List[i].Refills    = PIn.PString(table.Rows[i][4].ToString());
				List[i].Notes      = PIn.PString(table.Rows[i][5].ToString());
			}
		}
	
		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE rxdef SET " 
				+ "drug = '"       +POut.PString(Cur.Drug)+"'"
				+ ",sig = '"        +POut.PString(Cur.Sig)+"'"
				+ ",disp = '"       +POut.PString(Cur.Disp)+"'"
				+ ",refills = '"    +POut.PString(Cur.Refills)+"'"
				+ ",notes = '"      +POut.PString(Cur.Notes)+"'"
				+" WHERE RxDefNum = '" +POut.PInt (Cur.RxDefNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO rxdef (drug,sig,"
				+"disp,refills,notes) VALUES("
				//+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PString(Cur.Drug)+"', "
				+"'"+POut.PString(Cur.Sig)+"', "
				+"'"+POut.PString(Cur.Disp)+"', "
				+"'"+POut.PString(Cur.Refills)+"', "
				+"'"+POut.PString(Cur.Notes)+"')";
			NonQ(true);
			Cur.RxDefNum=InsertID;
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM rxdef "
				+"WHERE rxdefnum = '"+Cur.RxDefNum+"'";
			NonQ(false);
		}
	}

	///<summary>Corresponds to the rxdef table in the database.</summary>
	public struct RxDef{
		///<summary>Primary key.</summary>
		public int RxDefNum;
		///<summary>The name of the drug.</summary>
		public string Drug;
		///<summary>Directions.</summary>
		public string Sig;
		///<summary>Amount to dispense.</summary>
		public string Disp;
		///<summary>Number of refills.</summary>
		public string Refills;
		///<summary>Notes about this drug. Will not be copied to the rxpat.</summary>
		public string Notes;
	}

	/*=========================================================================================
	=================================== class RxPats ==========================================*/
///<summary></summary>
	public class RxPats:DataClass{
		///<summary></summary>
		public static RxPat[] List;
		///<summary></summary>
		public static RxPat Cur;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from rxpat"
				+" WHERE patnum = '"+POut.PInt(Patients.Cur.PatNum)+"'"
				+" ORDER BY rxdate";
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			List=new RxPat[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].RxNum      = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum     = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].RxDate     = PIn.PDate  (table.Rows[i][2].ToString());
				List[i].Drug       = PIn.PString(table.Rows[i][3].ToString());
				List[i].Sig        = PIn.PString(table.Rows[i][4].ToString());
				List[i].Disp       = PIn.PString(table.Rows[i][5].ToString());
				List[i].Refills    = PIn.PString(table.Rows[i][6].ToString());
				List[i].ProvNum    = PIn.PInt   (table.Rows[i][7].ToString());
				List[i].Notes      = PIn.PString(table.Rows[i][8].ToString());
			}
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE rxpat SET " 
				+ "patnum = '"      +POut.PInt   (Cur.PatNum)+"'"
				+ ",rxdate = '"     +POut.PDate  (Cur.RxDate)+"'"
				+ ",drug = '"       +POut.PString(Cur.Drug)+"'"
				+ ",sig = '"        +POut.PString(Cur.Sig)+"'"
				+ ",disp = '"       +POut.PString(Cur.Disp)+"'"
				+ ",refills = '"    +POut.PString(Cur.Refills)+"'"
				+ ",provnum = '"    +POut.PInt   (Cur.ProvNum)+"'"
				+ ",notes = '"      +POut.PString(Cur.Notes)+"'"
				+" WHERE RxNum = '" +POut.PInt (Cur.RxNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO rxpat (patnum,rxdate,drug,sig,"
				+"disp,refills,provnum,notes) VALUES("
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PDate  (Cur.RxDate)+"', "
				+"'"+POut.PString(Cur.Drug)+"', "
				+"'"+POut.PString(Cur.Sig)+"', "
				+"'"+POut.PString(Cur.Disp)+"', "
				+"'"+POut.PString(Cur.Refills)+"', "
				+"'"+POut.PInt   (Cur.ProvNum)+"', "
				+"'"+POut.PString(Cur.Notes)+"')";
			NonQ(true);
			Cur.RxNum=InsertID;
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from rxpat WHERE rxnum = '"+POut.PInt(Cur.RxNum)+"'";
			NonQ(false);
		}
	}

	///<summary>Corresponds to the rxpat table in the database.  One Rx for one patient. Copied from rxdef rather than linked to it.</summary>
	public struct RxPat{
		///<summary>Primary key.</summary>
		public int RxNum;
		///<summary>Foreign key to patient.PatNum.</summary>
		public int PatNum;
		///<summary>Date of Rx.</summary>
		public DateTime RxDate;
		///<summary>Drug name.</summary>
		public string Drug;
		///<summary>Directions.</summary>
		public string Sig;
		///<summary>Amount to dispense.</summary>
		public string Disp;
		///<summary>Number of refills.</summary>
		public string Refills;
		///<summary>Foreign key to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>Notes specific to this Rx.</summary>
		public string Notes;
	}

	/*=========================================================================================
		=================================== class SchedDefaults ==========================================*/

	///<summary></summary>
	public class SchedDefaults:DataClass{
		///<summary></summary>
		public static SchedDefault[] List;
		///<summary></summary>
		public static SchedDefault Cur;
	
		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from scheddefault";
			FillTable();
			List=new SchedDefault[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].SchedDefaultNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].DayOfWeek      = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].StartTime      = PIn.PDateT (table.Rows[i][2].ToString());
				List[i].StopTime       = PIn.PDateT (table.Rows[i][3].ToString());
			}
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE scheddefault SET " 
				+ "dayofweek = '"  +POut.PInt   (Cur.DayOfWeek)+"'"
				+ ",starttime = '" +POut.PDateT (Cur.StartTime)+"'"
				+ ",stoptime = '"  +POut.PDateT (Cur.StopTime)+"'"
				+" WHERE SchedDefaultNum = '" +POut.PInt (Cur.SchedDefaultNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO scheddefault (dayofweek,starttime,stoptime"
				+") VALUES("
				+"'"+POut.PInt   (Cur.DayOfWeek)+"', "
				+"'"+POut.PDateT (Cur.StartTime)+"', "
				+"'"+POut.PDateT (Cur.StopTime)+"')";
			NonQ(true);
			Cur.SchedDefaultNum=InsertID;
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from scheddefault WHERE scheddefaultnum = '"+POut.PInt(Cur.SchedDefaultNum)+"'";
			NonQ(false);
		}
	}

	///<summary>Corresponds to the scheddefault table in the database.</summary>
	public struct SchedDefault{
		///<summary>Primary key.</summary>
		public int SchedDefaultNum;
		///<summary>Sun=0, Mon=1, etc.</summary>
		public int DayOfWeek;
		///<summary>Start time for this timeblock.</summary>
		public DateTime StartTime;
		///<summary>Stop time for this timeblock.</summary>
		public DateTime StopTime;
		///<summary>See the ScheduleType enumeration.</summary>
		public ScheduleType SchedType;
		///<summary>Foreign key to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>Not in use yet. Will be Foreign key to definition.DefNum.</summary>
		public int BlockoutType;
	}

	/*=========================================================================================
		=================================== class Schedule ==========================================*/
///<summary></summary>
	public class Schedules:DataClass{
		///<summary></summary>
		public static Schedule[] ListMonth;
		///<summary></summary>
		public static Schedule[] ListDay;
		///<summary></summary>
		public static Schedule Cur;
		///<summary></summary>
		public static DateTime CurDate;
		//private static ArrayList AL;
	
		///<summary></summary>
		public static void RefreshMonth(){
			//used in the schedule setup window
			cmd.CommandText =
				"SELECT * FROM schedule WHERE MONTH(SchedDate)='"+CurDate.Month.ToString()
				+"' && YEAR(SchedDate)='"+CurDate.Year.ToString()+"' "
				+"ORDER BY starttime";
			FillTable();
			ListMonth=new Schedule[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				ListMonth[i].ScheduleNum    = PIn.PInt   (table.Rows[i][0].ToString());
				ListMonth[i].SchedDate      = PIn.PDate  (table.Rows[i][1].ToString());
				ListMonth[i].StartTime      = PIn.PDateT (table.Rows[i][2].ToString());
				ListMonth[i].StopTime       = PIn.PDateT (table.Rows[i][3].ToString());
				//schedtype
				//provnum
				//blockouttype
				ListMonth[i].Note           = PIn.PString(table.Rows[i][7].ToString());
				ListMonth[i].Status         = (SchedStatus)PIn.PInt(table.Rows[i][8].ToString());        
			}
		}

		///<summary></summary>
		public static void RefreshDay(DateTime thisDay){
			//Called every time the day is refreshed or changed in Appointments module
			CurDate=thisDay;//may revise later
			cmd.CommandText =
				"SELECT * FROM schedule WHERE SchedDate='"+POut.PDate(CurDate)+"'"
				+" ORDER BY starttime";
			FillTable();
			ListDay=new Schedule[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				ListDay[i].ScheduleNum    = PIn.PInt   (table.Rows[i][0].ToString());
				ListDay[i].SchedDate      = PIn.PDate  (table.Rows[i][1].ToString());
				ListDay[i].StartTime      = PIn.PDateT (table.Rows[i][2].ToString());
				ListDay[i].StopTime       = PIn.PDateT (table.Rows[i][3].ToString());
				//schedtype
				//provnum
				//blockouttype
				ListDay[i].Note           = PIn.PString(table.Rows[i][7].ToString());
				ListDay[i].Status         = (SchedStatus)PIn.PInt(table.Rows[i][8].ToString());        
			}
		}

		/*public static void GetDayList(){
			AL=new ArrayList();
			for(int i=0;i<table.Rows.Count;i++){
				if(CurDate.Day==List[i].Date.Day){
					AL.Add(List[i]);
				}
			}
			DayList=new Schedule[AL.Count];
			AL.CopyTo(DayList);
		}   */ 

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE schedule SET " 
				+ "scheddate = '"  +POut.PDate  (Cur.SchedDate)+"'"
				+ ",starttime = '" +POut.PDateT (Cur.StartTime)+"'"
				+ ",stoptime = '"  +POut.PDateT (Cur.StopTime)+"'"
				//schedtype
				//provnum
				//blockouttype
				+ ",note = '"      +POut.PString(Cur.Note)+"'"
				+ ",status = '"    +POut.PInt   ((int)Cur.Status)+"'"
				+" WHERE ScheduleNum = '" +POut.PInt (Cur.ScheduleNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO schedule (scheddate,starttime,stoptime,note,status"
				+") VALUES("
				+"'"+POut.PDate  (Cur.SchedDate)     +"', "
				+"'"+POut.PDateT (Cur.StartTime)+"', "
				+"'"+POut.PDateT (Cur.StopTime) +"', "
				//schedtype
				//provnum
				//blockouttype
				+"'"+POut.PString(Cur.Note)     +"', "
				+"'"+POut.PInt   ((int)Cur.Status)   +"')";
			NonQ(true);
			Cur.ScheduleNum=InsertID;
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from schedule WHERE schedulenum = '"+POut.PInt(Cur.ScheduleNum)+"'";
			NonQ(false);
		}
	}

	///<summary>Corresponds to the schedule table in the database.</summary>
	public struct Schedule{
		///<summary>Primary key.</summary>
		public int ScheduleNum;
		///<summary>Date for this timeblock.</summary>
		public DateTime SchedDate;
		///<summary>Start time for this timeblock.</summary>
		public DateTime StartTime;
		///<summary>Stop time for this timeblock.</summary>
		public DateTime StopTime;
		///<summary>See the ScheduleType enumeration.</summary>
		public ScheduleType SchedType;
		///<summary>Not in use yet. Will be used for provider schedules.</summary>
		public int ProvNum;
		///<summary>(not in use yet)foreign key to definition.DefNum.  eg. HighProduction, RCT Only, Emerg.</summary>
		public int BlockoutType;
		///<summary>This contains various types of text entered by the user.</summary>
		public string Note;
		///<summary>See the SchedStatus enumeration.</summary>
		public SchedStatus Status;
	}

	/*=========================================================================================
		=================================== class Queries ==========================================*/
///<summary></summary>
	public class Queries:DataClass{
		//public static string queryString;
		///<summary></summary>
		public static DataTable TableQ;
		///<summary></summary>
		public static DataTable TableTemp;
		///<summary></summary>
		public static Report CurReport;

		///<summary></summary>
		public static void SubmitCur(){
			cmd.CommandText = CurReport.Query;
			FillTable();
			TableQ=table.Copy();
		}

		///<summary></summary>
		public static void SubmitTemp(){
			cmd.CommandText = CurReport.Query;
			FillTable();
			TableTemp=table.Copy();
		}

		///<summary></summary>
		public static void SubmitNonQ(){
			cmd.CommandText = CurReport.Query;
			NonQ(false);
		}
	}

	///<summary>Not a database table.</summary>
	public struct Report{
		///<summary></summary>
		public string Query;
		///<summary></summary>
		public string Title;
		///<summary></summary>
		public string[] SubTitle;
		///<summary>Always 1 extra for right boundary of right col</summary>
		public int[] ColPos;
		///<summary></summary>
		public string[] ColCaption;
		///<summary></summary>
		public HorizontalAlignment[] ColAlign;
		///<summary></summary>
		public double[] ColTotal;
		///<summary></summary>
		public int[] ColWidth;
		///<summary></summary>
		public string[] Summary;
	}

	/*=========================================================================================
	=================================== class SecurityLogs==========================================*/
  ///<summary></summary>
	public class SecurityLogs:DataClass{
		///<summary></summary>
		public static SecurityLog Cur;
		///<summary></summary>
		public static SecurityLog[] List;

		/*public static void Refresh(){//this may be used later for reporting
			cmd.CommandText =
				"SELECT * from userlog";
			FillTable();
			List=new UserLog[table.Rows.Count];
			for(int i = 0;i<List.Length;i++){
				List[i].UserLogNum   = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PermissionNum= PIn.PInt   (table.Rows[i][1].ToString());
				List[i].EmployeeNum  = PIn.PInt   (table.Rows[i][2].ToString());	
				List[i].ProviderNum  = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].LogDate      = PIn.PDateT (table.Rows[i][4].ToString());
				List[i].Note         = PIn.PString(table.Rows[i][5].ToString());
			}
		}*/

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO securitylog (permission,username,logdatetime,logtext) "
				+"VALUES ("
				+"'"+POut.PString(Cur.Permission)+"', "
				+"'"+POut.PString(Cur.UserName)+"', "
				+"'"+POut.PDateT (Cur.LogDateTime)+"', "
				+"'"+POut.PString(Cur.LogText)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.SecurityLogNum=InsertID;
		}

		//there are no methods for deleting or changing log entries because that will never be allowed.

		///<summary></summary>
		public static void MakeLogEntry(string permissionName,string logText){
			bool IsLogged=false;
			if(Permissions.GetCur(permissionName)){//if permissionName is a recognized permission
				if(!Permissions.Cur.RequiresPassword){
					return;//no password required, so no logging either
				}
				if(Users.Cur.EmployeeNum > 0){
					UserPermissions.GetListForEmp(Users.Cur.EmployeeNum);
				}
				else{
					UserPermissions.GetListForProv(Users.Cur.ProvNum);
				}
				for(int i=0;i<UserPermissions.ListForUser.Length;i++){
					if(UserPermissions.ListForUser[i].PermissionNum==Permissions.Cur.PermissionNum 
						&& UserPermissions.ListForUser[i].IsLogged){
						UserPermissions.Cur=UserPermissions.ListForUser[i];
						IsLogged=true;
					}
				}
				if(!IsLogged){
					return;
				}
			}
			Cur=new SecurityLog();
			if(Users.Cur.EmployeeNum > 0){
				Cur.UserName=Employees.GetName(UserPermissions.Cur.EmployeeNum);
			}
			else{
				Cur.UserName=Providers.GetAbbr(UserPermissions.Cur.ProvNum);
			}
			Cur.Permission=permissionName;
			Cur.LogDateTime=DateTime.Now;
			Cur.LogText=logText;
			InsertCur();
		}

	}

	///<summary>Corresponds to the securitylog table in the database.</summary>
	public struct SecurityLog{
		///<summary>Primary key.</summary>
		public int SecurityLogNum;
		///<summary>Permission name in plain text.</summary>
		public string Permission;
		///<summary>User name in plain text.</summary>
		public string UserName;
		///<summary>The date and time of the entry.</summary>
		public DateTime LogDateTime;
		///<summary>The description of exactly what was done. Varies by permission type.</summary>
		public string LogText;
	}

	/*=========================================================================================
	=================================== class UserPermissions ==========================================*/
  ///<summary></summary>
	public class UserPermissions:DataClass{
		///<summary></summary>
		public static UserPermission Cur;
		///<summary></summary>
		public static UserPermission[] List;//all user permissions for all users.
		///<summary></summary>
		public static UserPermission[] ListForUser;//user permissions for a single user

		///<summary></summary>
		public static void Refresh(){
			//gets all userpermissions for all users
			cmd.CommandText =
				"SELECT * from userpermission";
			FillTable();
			List=new UserPermission[table.Rows.Count];
			for(int i = 0;i<List.Length;i++){
				List[i].UserPermissionNum= PIn.PInt (table.Rows[i][0].ToString());
				List[i].PermissionNum    = PIn.PInt (table.Rows[i][1].ToString());
				List[i].EmployeeNum      = PIn.PInt (table.Rows[i][2].ToString());	
				List[i].ProvNum      = PIn.PInt (table.Rows[i][3].ToString());
				List[i].IsLogged         = PIn.PBool(table.Rows[i][4].ToString());
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO userpermission (permissionnum,employeenum,provnum,islogged) "
				+"VALUES ("
				+"'"+POut.PInt (Cur.PermissionNum)+"', "
				+"'"+POut.PInt (Cur.EmployeeNum)+"', "
				+"'"+POut.PInt (Cur.ProvNum)+"', "
				+"'"+POut.PBool(Cur.IsLogged)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.UserPermissionNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE userpermission SET "
				+"permissionnum='" +POut.PInt (Cur.PermissionNum)+"'"
				+",employeenum ='" +POut.PInt (Cur.EmployeeNum)+"'"
				+",provnum ='"     +POut.PInt (Cur.ProvNum)+"'"
				+",islogged    ='" +POut.PBool(Cur.IsLogged)+"'"
				+" WHERE userpermissionnum = '"+POut.PInt(Cur.UserPermissionNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from userpermission WHERE userpermissionnum = '"
				+POut.PInt(Cur.UserPermissionNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteAllForEmp(int employeeNum){
			cmd.CommandText = "DELETE from userpermission WHERE employeenum = '"+POut.PInt(employeeNum)+"'";
			NonQ(false);
		}
		
		///<summary></summary>
		public static void DeleteAllForProv(int provNum){
			cmd.CommandText = "DELETE from userpermission WHERE provnum = '"+POut.PInt(provNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void GetListForEmp(int employeeNum){
			//subset of List including only for one user
			ArrayList ALtemp=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].EmployeeNum==employeeNum){
					ALtemp.Add(List[i]);
				} 
			}
			if(ALtemp.Count==0){
				ListForUser=new UserPermission[0];  
			}
			else{
				ListForUser=new UserPermission[ALtemp.Count];
				ALtemp.CopyTo(ListForUser);
			}    
		}

		///<summary></summary>
		public static void GetListForProv(int provNum){
			//subset of List including only for one user
			ArrayList ALtemp=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ProvNum==provNum){
					ALtemp.Add(List[i]);
				} 
			}
			if(ALtemp.Count==0){
				ListForUser=new UserPermission[0];  
			}
			else{
				ListForUser=new UserPermission[ALtemp.Count];
				ALtemp.CopyTo(ListForUser);
			}    
		}

		///<summary></summary>
		public static int AdministratorCount(){//TestForAdminCount(){
			//returns number of provs with security administration permission
			cmd.CommandText=
				"SELECT userpermission.userpermissionnum FROM userpermission,permission "
				+"WHERE userpermission.permissionnum=permission.permissionnum && "
				+"permission.name='Security Administration'";
			FillTable();
			return table.Rows.Count;
		}

		/*public static void ResetPassword(){
			Permissions.GetCur("Security Administration");
			cmd.CommandText="DELETE from userpermission where permissionnum = '"
				+Permissions.Cur.PermissionNum+"'";
			NonQ(false);
		}*/

		///<summary></summary>
		public static bool CheckUserPassword(string permissionName){
			//used for most security checks in program.
			//displays user/password dialog only if necessary.
			Permissions.GetCur(permissionName);
			if(!Permissions.Cur.RequiresPassword){
				return true;//password not required.
			}
			FormPassword FormP=new FormPassword();
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK){
				return false;//bad password
			}
			if(Users.Cur.EmployeeNum > 0){
				GetListForEmp(Users.Cur.EmployeeNum);
			}
			else{
				GetListForProv(Users.Cur.ProvNum);
			}
			for(int i=0;i<ListForUser.Length;i++){
				if(ListForUser[i].PermissionNum==Permissions.Cur.PermissionNum){
					Cur=ListForUser[i];//not sure why this is necessary.  might be in use.
					return true;//allow access
				}
			}		
			return false;
		}

		///<summary></summary>
		public static bool CheckUserPassword(string permissionName,DateTime myDate){
			//only checks password if before a certain date or number of days
			Permissions.GetCur(permissionName);
			bool doCheck=false;
			if(myDate <= Permissions.Cur.BeforeDate){//if date is before specified date
				doCheck=true;
			}
			if(myDate <= DateTime.Today.AddDays(-Permissions.Cur.BeforeDays)){//if date is older than # of days
				doCheck=true;
			}
			if(doCheck){
				return CheckUserPassword(permissionName);
			}
			else return true;//allow access if newer
		}

		///<summary></summary>
		public static bool CheckHasPermission(string permissionName,int num,bool IsEmployee){
			//used when selecting all permissions in form prov or emp.
			//also used whenever we have already displayed a dialog and just want to check permission for a user
			Permissions.GetCur(permissionName);
			if(IsEmployee)
				GetListForEmp(num);
			else
				GetListForProv(num);
			for(int i=0;i<ListForUser.Length;i++){
				if(ListForUser[i].PermissionNum==Permissions.Cur.PermissionNum){
					Cur=ListForUser[i];
					return true;
				}
			}
			return false;
		}

		//public static void GetAdminProvider(){
			/*Providers.Cur=new Provider();
			for(int i=0;i<Providers.List.Length;i++){
				if(CheckHasPermission("Administer Passwords",Providers.List[i].ProvNum,false)){
					Providers.Cur=Providers.List[i];
					MessageBox.Show(Lan.g("Permissions","Found"));
				}
			}*/
		//}

	}//end class UserPermissions

	///<summary>Corresponds to the userpermission table in the database.</summary>
	public struct UserPermission{
		///<summary>Primary key.</summary>
		public int UserPermissionNum;
		///<summary>Foreign key to permission.PermissionNum.</summary>
		public int PermissionNum;
		///<summary>Foreign key to employee.EmployeeNum.</summary>
		public int EmployeeNum;
		///<summary>Foreign key to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>If true, then activities of this permission type will be logged for this user.</summary>
		public bool IsLogged;
	}

	/*=========================================================================================
		=================================== class UserQueries ==========================================*/
///<summary></summary>
	public class UserQueries:DataClass{
		///<summary></summary>
		public static UserQuery[] List;
		///<summary></summary>
		public static UserQuery Cur;
		///<summary></summary>
		public static bool IsSelected;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT querynum,description,filename,querytext"
				+" FROM userquery"
				//+" WHERE hidden != '1'";
				+" ORDER BY description";
			FillTable();
			List=new UserQuery[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].QueryNum    = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description = PIn.PString(table.Rows[i][1].ToString());
				List[i].FileName    = PIn.PString(table.Rows[i][2].ToString());
				List[i].QueryText   = PIn.PString(table.Rows[i][3].ToString());
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText="INSERT INTO userquery (description,filename,querytext) VALUES("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PString(Cur.FileName)+"', "
				+"'"+POut.PString(Cur.QueryText)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}
		
		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from userquery WHERE querynum = '"+POut.PInt(Cur.QueryNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE userquery SET "
				+ "description = '" +POut.PString(Cur.Description)+"'"
				+ ",filename = '"    +POut.PString(Cur.FileName)+"'"
				+",querytext = '"   +POut.PString(Cur.QueryText)+"'"
				+" WHERE querynum = '"+POut.PInt(Cur.QueryNum)+"'";
			NonQ(false);
		}
	}

	///<summary>Corresponds to the userquery table in the database.</summary>
	public struct UserQuery{
		///<summary>Primary key.</summary>
		public int QueryNum;
		///<summary>Description.</summary>
		public string Description;
		///<summary>The name of the file to export to.</summary>
		public string FileName;
		///<summary>The text of the query.</summary>
		public string QueryText;
	}

	/*=========================================================================================
	=================================== class Users==========================================*/
	///<summary></summary>
	public class Users{
		///<summary></summary>
		public static User Cur;
		///<summary></summary>
		public static User[] List;   

		///<summary></summary>
		public static void Refresh(){
			ArrayList AL=new ArrayList();
			for(int i=0;i<Providers.List.Length;i++){
				User user=new User();
				user.UserName=Providers.List[i].UserName;
				user.Password=Providers.List[i].Password;
				user.ProvNum=Providers.List[i].ProvNum;
				if(user.UserName!="")
					AL.Add(user);
			}
			for(int i=0;i<Employees.List.Length;i++){
				User user=new User();
				user.UserName=Employees.List[i].UserName;
				user.Password=Employees.List[i].Password;
				user.EmployeeNum=Employees.List[i].EmployeeNum;
				if(user.UserName!="")
					AL.Add(user);
			}
			if(AL.Count==0){
				List=new User[0];  
			}
			else{
				List=new User[AL.Count];
				AL.CopyTo(List);
			}  
		}

		///<summary></summary>
		public static bool UserNameTaken(string userName){
			for(int i=0;i<List.Length;i++){
				if(List[i].UserName==userName)
					return true;
			}
			return false;
		}

	}
 
	//<summary>Table user.  Every provider and employee is assigned a unique user number,
	//whether or not they have been assigned a username and password. A usernumber can never
	//be changed, ensuring a permanent way to record transactions.</summary>
	/// <summary>Not a database table yet.</summary>
	public struct User{
		//<summary>Primary key.</summary>
		//public int UserNum;
		///<summary></summary>
		public string UserName;
		///<summary></summary>
		public string Password;
		///<summary></summary>
		public int EmployeeNum;//Foreign key to employee.employeeNum
		///<summary></summary>
		public int ProvNum;//Foreign key to provider.ProvNum. Either Emp or Prov, not both.
	}

	/*=========================================================================================
		=================================== class ZipCodes ===========================================*/
  ///<summary></summary>
	public class ZipCodes:DataClass{
		///<summary></summary>
		public static ZipCode Cur;
		///<summary></summary>
		public static ZipCode[] List;
		///<summary></summary>
		public static ArrayList ALFrequent;
		///<summary></summary>
		public static ArrayList ALMatches;
		//public static Hashtable HList; 

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from zipcode ORDER BY zipcodedigits";
			FillTable();
			//HList=new Hashtable();
			ALFrequent=new ArrayList();
			List=new ZipCode[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i].ZipCodeNum   =PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ZipCodeDigits=PIn.PString(table.Rows[i][1].ToString());
				List[i].City         =PIn.PString(table.Rows[i][2].ToString());	
				List[i].State        =PIn.PString(table.Rows[i][3].ToString());	
				List[i].IsFrequent   =PIn.PBool  (table.Rows[i][4].ToString());
				if(List[i].IsFrequent){
					ALFrequent.Add(List[i]);
				}
				//HList.Add(List[i].ZipCodeNum,List[i]);
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO zipcode (zipcodedigits,city,state,isfrequent) "
				+"VALUES ("
				+"'"+POut.PString(Cur.ZipCodeDigits)+"', "
				+"'"+POut.PString(Cur.City)+"', "
				+"'"+POut.PString(Cur.State)+"', "
				+"'"+POut.PBool  (Cur.IsFrequent)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
			//Cur.ZipCodeNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE zipcode SET "
				+"zipcodedigits ='"+POut.PString(Cur.ZipCodeDigits)+"'"
				+",city ='"        +POut.PString(Cur.City)+"'"
				+",state ='"       +POut.PString(Cur.State)+"'"
				+",isfrequent ='"  +POut.PBool  (Cur.IsFrequent)+"'"
				+" WHERE zipcodenum = '"+POut.PInt(Cur.ZipCodeNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from zipcode WHERE zipcodenum = '"+POut.PInt(Cur.ZipCodeNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void GetALMatches(string zipCodeDigits){
			ALMatches=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ZipCodeDigits==zipCodeDigits){
					ALMatches.Add(List[i]);
				}
			}

		}

	}

	///<summary>Corresponds to the zipcode table in the database.</summary>
	public struct ZipCode{
		///<summary>Primary key.</summary>
		public int ZipCodeNum;
		///<summary>The actual zipcode.</summary>
		public string ZipCodeDigits;
		///<summary></summary>
		public string City;
		///<summary></summary>
		public string State;
		///<summary>If true, then it will show in the dropdown list in the patient edit window.</summary>
		public bool IsFrequent;
	}

}













