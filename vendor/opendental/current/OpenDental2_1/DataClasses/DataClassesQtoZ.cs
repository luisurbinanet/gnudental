/*=============================================================================================================
FreeDental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
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

	public class RefAttaches:DataClass{
		public static RefAttach[] List;//for this patient only
		public static RefAttach Cur;
		public static Hashtable HList;//key:refAttachNum, value:RefAttach

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

		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM refattach "
				+"WHERE refattachnum = '"+Cur.RefAttachNum+"'";
			NonQ(false);
		}

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

	public struct RefAttach{  
		public int RefAttachNum;//Primary Key		
		public int ReferralNum;//Foreign Key to referral.ReferralNum
		public int PatNum;//Foreign Key to Patient.PatNum
		public int ItemOrder;//Order to display in patient info. Will be automated more in future.
		public DateTime RefDate;//date of referral
		public bool IsFrom;//true=from, false=to
	}

	/*==============================================================================================
		=================================== class Referrals ==========================================*/

	public class Referrals:DataClass{
		public static Referral[] List;//all referrals for all patients
		//should later add a list for single patient, along with a faster refresh sequence.
		public static Referral Cur;
		private static Hashtable HList;//to control access and refresh

		public static void Refresh(){
			//may change how this is done so that referrals don't take up so much memory
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

		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM referral "
				+"WHERE referralnum = '"+Cur.ReferralNum+"'";
			NonQ(false);
		}

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

	public struct Referral{//table referral
		public int ReferralNum;//Primary Key
		public string LName;
		public string FName;
		public string MName;
		public string SSN;
		public bool UsingTIN;//specificies if SSN is real SSN
		public DentalSpecialty Specialty;//enumeration found in ClassEnumerations.cs
		public string ST;//State
		public string Telephone;//primary phone, restrictive, must only be 10 digits and only numbers
		public string Address;
		public string Address2;
		public string City;
		public string Zip;
		public string Note;//holds important info
		public string Phone2;//additional phone no restrictions
		public bool IsHidden;//Can't delete, but can hide if not needed
		public bool NotPerson;//Set to true for referralls such as Yellow Pages.
		public string Title;//i.e. DMD
		public string EMail;
		public int PatNum;//foreign key to Patient.PatNum for referrals that are patients
	}

	/*=========================================================================================
		=================================== class RxDefs ==========================================*/

	public class RxDefs:DataClass{
		public static RxDef[] List;
		public static RxDef Cur;
	
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

		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM rxdef "
				+"WHERE rxdefnum = '"+Cur.RxDefNum+"'";
			NonQ(false);
		}
	}

	public struct RxDef{
		public int RxDefNum;
		public string Drug;
		public string Sig;
		public string Disp;
		public string Refills;
		public string Notes;
	}

	/*=========================================================================================
	=================================== class RxPats ==========================================*/

	public class RxPats:DataClass{
		public static RxPat[] List;
		public static RxPat Cur;

	
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

		public static void DeleteCur(){
			cmd.CommandText = "DELETE from rxpat WHERE rxnum = '"+POut.PInt(Cur.RxNum)+"'";
			NonQ(false);
		}
	}

	public struct RxPat{
		public int RxNum;
		public int PatNum;
		public DateTime RxDate;
		public string Drug;
		public string Sig;
		public string Disp;
		public string Refills;
		public int ProvNum;
		public string Notes;
	}

	/*=========================================================================================
		=================================== class SchedDefaults ==========================================*/

	public class SchedDefaults:DataClass{
		public static SchedDefault[] List;
		public static SchedDefault Cur;
	
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

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE scheddefault SET " 
				+ "dayofweek = '"  +POut.PInt   (Cur.DayOfWeek)+"'"
				+ ",starttime = '" +POut.PDateT (Cur.StartTime)+"'"
				+ ",stoptime = '"  +POut.PDateT (Cur.StopTime)+"'"
				+" WHERE SchedDefaultNum = '" +POut.PInt (Cur.SchedDefaultNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO scheddefault (dayofweek,starttime,stoptime"
				+") VALUES("
				+"'"+POut.PInt   (Cur.DayOfWeek)+"', "
				+"'"+POut.PDateT (Cur.StartTime)+"', "
				+"'"+POut.PDateT (Cur.StopTime)+"')";
			NonQ(true);
			Cur.SchedDefaultNum=InsertID;
		}

		public static void DeleteCur(){
			cmd.CommandText = "DELETE from scheddefault WHERE scheddefaultnum = '"+POut.PInt(Cur.SchedDefaultNum)+"'";
			NonQ(false);
		}
	}

	public struct SchedDefault{//table scheddefault
		public int SchedDefaultNum;//primary key
		public int DayOfWeek;//Sun=0, etc.
		public DateTime StartTime;
		public DateTime StopTime;
		public ScheduleType SchedType;//enum ScheduleType{Practice=0,Provider,Blockout} (Provider and Blockout not in use yet)
		public int ProvNum;//not used yet.
		public int BlockoutType;//future def
	}

	/*=========================================================================================
		=================================== class Schedule ==========================================*/

	public class Schedules:DataClass{
		public static Schedule[] List;//entire month
		public static Schedule[] DayList;//one day
		public static Schedule Cur;
		public static DateTime CurDate;
		//private static ArrayList AL;
	
		public static void RefreshMonth(){
			cmd.CommandText =
				"SELECT * FROM schedule WHERE MONTH(SchedDate)='"+CurDate.Month.ToString()
				+"' && YEAR(SchedDate)='"+CurDate.Year.ToString()+"' "
				+"ORDER BY starttime";
			FillTable();
			List=new Schedule[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].ScheduleNum    = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].SchedDate      = PIn.PDate  (table.Rows[i][1].ToString());
				List[i].StartTime      = PIn.PDateT (table.Rows[i][2].ToString());
				List[i].StopTime       = PIn.PDateT (table.Rows[i][3].ToString());
				//schedtype
				//provnum
				//blockouttype
				List[i].Note           = PIn.PString(table.Rows[i][7].ToString());
				List[i].Status         = (SchedStatus)PIn.PInt(table.Rows[i][8].ToString());        
			}
		}

		public static void RefreshDay(DateTime thisDay){
			CurDate=thisDay;//may revise later
			cmd.CommandText =
				"SELECT * FROM schedule WHERE SchedDate='"+POut.PDate(CurDate)+"'"
				+" ORDER BY starttime";
			FillTable();
			DayList=new Schedule[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				DayList[i].ScheduleNum    = PIn.PInt   (table.Rows[i][0].ToString());
				DayList[i].SchedDate      = PIn.PDate  (table.Rows[i][1].ToString());
				DayList[i].StartTime      = PIn.PDateT (table.Rows[i][2].ToString());
				DayList[i].StopTime       = PIn.PDateT (table.Rows[i][3].ToString());
				//schedtype
				//provnum
				//blockouttype
				DayList[i].Note           = PIn.PString(table.Rows[i][7].ToString());
				DayList[i].Status         = (SchedStatus)PIn.PInt(table.Rows[i][8].ToString());        
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

		public static void DeleteCur(){
			cmd.CommandText = "DELETE from schedule WHERE schedulenum = '"+POut.PInt(Cur.ScheduleNum)+"'";
			NonQ(false);
		}
	}

	public struct Schedule{
		public int ScheduleNum;
		public DateTime SchedDate;
		public DateTime StartTime;
		public DateTime StopTime;
		public ScheduleType SchedType;//enum ScheduleType{Practice=0,Provider,Blockout} (Provider and Blockout not in use yet)
		public int ProvNum;//not in use yet. Used for provider schedules
		public int BlockoutType;//(not in use yet)foreign key to Defs.  eg. HighProduction, RCT Only, Emerg.
		public string Note;
		public SchedStatus Status;//enum SchedStatus{Open=0,Closed,Holiday}
	}

	/*=========================================================================================
		=================================== class Queries ==========================================*/

	public class Queries:DataClass{
		//public static string queryString;
		public static DataTable TableQ;
		public static DataTable TableTemp;
		public static Report CurReport;

		public static void SubmitCur(){
			cmd.CommandText = CurReport.Query;
			FillTable();
			TableQ=table.Copy();
		}

		public static void SubmitTemp(){
			cmd.CommandText = CurReport.Query;
			FillTable();
			TableTemp=table.Copy();
		}

		public static void SubmitNonQ(){
			cmd.CommandText = CurReport.Query;
			NonQ(false);
		}
	}

	public struct Report{
		public string Query;
		public string Title;
		public string[] SubTitle;
		public int[] ColPos;//always 1 extra for right boundary of right col
		public string[] ColCaption;
		public HorizontalAlignment[] ColAlign;
		public double[] ColTotal;
		public int[] ColWidth;
		public string[] Summary;
	}

	/*=========================================================================================
	=================================== class SecurityLogs==========================================*/
  
	public class SecurityLogs:DataClass{
		public static SecurityLog Cur;
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

	public struct SecurityLog{
		public int SecurityLogNum;//Primary Key
		public string Permission;//permission name in plain text
		public string UserName;//user name in plain text
		public DateTime LogDateTime;
		public string LogText;//The description of exactly what was done. Varies by permission type
	}

	/*=========================================================================================
	=================================== class UserPermissions ==========================================*/
  
	public class UserPermissions:DataClass{
		public static UserPermission Cur;
		public static UserPermission[] List;//all user permissions for all users.
		public static UserPermission[] ListForUser;//user permissions for a single user

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

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE userpermission SET "
				+"permissionnum='" +POut.PInt (Cur.PermissionNum)+"'"
				+",employeenum ='" +POut.PInt (Cur.EmployeeNum)+"'"
				+",provnum ='"     +POut.PInt (Cur.ProvNum)+"'"
				+",islogged    ='" +POut.PBool(Cur.IsLogged)+"'"
				+" WHERE userpermissionnum = '"+POut.PInt(Cur.UserPermissionNum)+"'";
			NonQ(false);
		}

		public static void DeleteCur(){
			cmd.CommandText = "DELETE from userpermission WHERE userpermissionnum = '"
				+POut.PInt(Cur.UserPermissionNum)+"'";
			NonQ(false);
		}

		public static void DeleteAllForEmp(int employeeNum){
			cmd.CommandText = "DELETE from userpermission WHERE employeenum = '"+POut.PInt(employeeNum)+"'";
			NonQ(false);
		}
		
		public static void DeleteAllForProv(int provNum){
			cmd.CommandText = "DELETE from userpermission WHERE provnum = '"+POut.PInt(provNum)+"'";
			NonQ(false);
		}

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

	public struct UserPermission{
		public int UserPermissionNum;//Primary Key
		public int PermissionNum;//FK
		public int EmployeeNum;//FK
		public int ProvNum;//FK
		public bool IsLogged;

	}//end struct UserPermission

	/*=========================================================================================
		=================================== class UserQueries ==========================================*/

	public class UserQueries:DataClass{
		public static UserQuery[] List;
		public static UserQuery Cur;
		public static bool IsSelected;

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

		public static void InsertCur(){
			cmd.CommandText="INSERT INTO userquery (description,filename,querytext) VALUES("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PString(Cur.FileName)+"', "
				+"'"+POut.PString(Cur.QueryText)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}
		

		public static void DeleteCur(){
			cmd.CommandText = "DELETE from userquery WHERE querynum = '"+POut.PInt(Cur.QueryNum)+"'";
			NonQ(false);
		}

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE userquery SET "
				+ "description = '" +POut.PString(Cur.Description)+"'"
				+ ",filename = '"    +POut.PString(Cur.FileName)+"'"
				+",querytext = '"   +POut.PString(Cur.QueryText)+"'"
				+" WHERE querynum = '"+POut.PInt(Cur.QueryNum)+"'";
			NonQ(false);
		}
	}

	public struct UserQuery{
		public int QueryNum;
		public string Description;
		public string FileName;
		public string QueryText;
	}

	/*=========================================================================================
	=================================== class Users==========================================*/
	public class Users{
		public static User Cur;
		public static User[] List;   

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

		public static bool UserNameTaken(string userName){
			for(int i=0;i<List.Length;i++){
				if(List[i].UserName==userName)
					return true;
			}
			return false;
		}

	}
 
	public struct User{//not a database table.  This is a combination of providers and employees
		public string UserName;
		public string Password;
		public int EmployeeNum;//Foreign key to employee.employeeNum
		public int ProvNum;//Foreign key to provider.ProvNum. Either Emp or Prov, not both.
	}

	/*=========================================================================================
		=================================== class ZipCodes ===========================================*/
  
	public class ZipCodes:DataClass{
		public static ZipCode Cur;
		public static ZipCode[] List;
		public static ArrayList ALFrequent;
		public static ArrayList ALMatches;
		//public static Hashtable HList; 

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

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE zipcode SET "
				+"zipcodedigits ='"+POut.PString(Cur.ZipCodeDigits)+"'"
				+",city ='"        +POut.PString(Cur.City)+"'"
				+",state ='"       +POut.PString(Cur.State)+"'"
				+",isfrequent ='"  +POut.PBool  (Cur.IsFrequent)+"'"
				+" WHERE zipcodenum = '"+POut.PInt(Cur.ZipCodeNum)+"'";
			NonQ(false);
		}

		public static void DeleteCur(){
			cmd.CommandText = "DELETE from zipcode WHERE zipcodenum = '"+POut.PInt(Cur.ZipCodeNum)+"'";
			NonQ(false);
		}

		public static void GetALMatches(string zipCodeDigits){
			ALMatches=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ZipCodeDigits==zipCodeDigits){
					ALMatches.Add(List[i]);
				}
			}

		}

	}

	public struct ZipCode{//table zipcode
		public int ZipCodeNum;//primary key
		public string ZipCodeDigits;//the actual zipcode
		public string City;
		public string State;
		public bool IsFrequent;
	}

}//end namespace













