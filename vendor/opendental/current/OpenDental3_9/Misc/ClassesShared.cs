using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text; 
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace OpenDental{

	///<summary></summary>
	public class Shared{

		///<summary></summary>
		public Shared(){
			
		}

		///<summary>Converts a date to an age. If age is over 115, then returns 0.</summary>
		public static int DateToAge(DateTime date){
			if(date.Year<1890)
				return 0;
			if(date.Month < DateTime.Now.Month){//birthday in previous month
				return DateTime.Now.Year-date.Year;
			}
			if(date.Month == DateTime.Now.Month && date.Day <= DateTime.Now.Day){//birthday in this month
				return DateTime.Now.Year-date.Year;
			}
			return DateTime.Now.Year-date.Year-1;
		}

		///<summary></summary>
		public static string AgeToString(int age){
			if(age==0)
				return "";
			else
				return age.ToString();
		}

		///<summary>Converts a date to an age.  Blank if over 115.</summary>
		public static string DateToAgeString(DateTime date){
			return AgeToString(DateToAge(date));
		}

		///<summary>Computes balance for a single patient. Returns true if a refresh is needed.</summary>
		public static bool ComputeBalances(Procedure[] procList,ClaimProc[] claimProcList,Patient PatCur,PaySplit[] paySplitList,Adjustment[] AdjustmentList,PayPlan[] payPlanList,PayPlanCharge[] payPlanChargeList){
			//must have refreshed all 5 first
			double calcBal
				=Procedures.ComputeBal(procList)
				+ClaimProcs.ComputeBal(claimProcList)
				+Adjustments.ComputeBal(AdjustmentList)
				-PaySplits.ComputeBal(paySplitList)
				+PayPlans.ComputeBal(PatCur.PatNum,payPlanList,payPlanChargeList);
			if(calcBal!=PatCur.EstBalance){
				Patient PatOld=PatCur.Copy();
				PatCur.EstBalance=calcBal;
				PatCur.Update(PatOld);
				return true;
			}
			return false;
		}

	}

/*=================================Class DataValid=========================================
===========================================================================================*/

	///<summary>Handles a global event to keep local data synchronized.</summary>
	public class DataValid{

		///<summary></summary>
		public static event OpenDental.ValidEventHandler BecameInvalid;	

		///<summary>Triggers an event that causes a signal to be sent to all other computers telling them what kind of locally stored data needs to be updated.  Either supply a set of flags for the types, or supply a date if the appointment screen needs to be refreshed.</summary>
		public static void SetInvalid(InvalidTypes itypes){
			OnBecameInvalid(new OpenDental.ValidEventArgs(DateTime.MinValue,itypes,false));
		}

		///<summary>Triggers an event that causes a signal to be sent to all other computers telling them what kind of locally stored data needs to be updated.  Either supply a set of flags for the types, or supply a date if the appointment screen needs to be refreshed.</summary>
		public static void SetInvalid(DateTime date){
			OnBecameInvalid(new OpenDental.ValidEventArgs(date,InvalidTypes.Date,false));
		}

		///<summary>Triggers an event that only causes this computer to refresh itself as if starting up.  Does not send out signal to other computers.  Used when restoring database from a backup.</summary>
		public static void SetInvalid(bool onlyLocal){
			OnBecameInvalid(new OpenDental.ValidEventArgs(DateTime.MinValue,InvalidTypes.AllLocal,true));
		}

		///<summary></summary>
		protected static void OnBecameInvalid(OpenDental.ValidEventArgs e){
			if(BecameInvalid !=null){
				BecameInvalid(e);
			}
		}

	}

	///<summary></summary>
	public delegate void ValidEventHandler(ValidEventArgs e);

	///<summary></summary>
	public class ValidEventArgs : System.EventArgs{
		private DateTime dateViewing;
		private InvalidTypes itypes;
		private bool onlyLocal;
		
		///<summary></summary>
		public ValidEventArgs(DateTime dateViewing, InvalidTypes itypes,bool onlyLocal) : base(){
			this.dateViewing=dateViewing;
			this.itypes=itypes;
			this.onlyLocal=onlyLocal;
		}

		///<summary></summary>
		public DateTime DateViewing{
			get{return dateViewing;}
		}

		///<summary></summary>
		public InvalidTypes ITypes{
			get{return itypes;}
		}

		///<summary></summary>
		public bool OnlyLocal{
			get{return onlyLocal;}
		}

	}

	/*=================================Class GotoModule==================================================
	===========================================================================================*/

	///<summary>Used to trigger a global event to jump between modules and perform actions in other modules.</summary>
	public class GotoModule{
		///<summary></summary>
		public static event ModuleEventHandler ModuleSelected;

		///<summary>This triggers a global event which the main form responds to by going directly to a module.</summary>
		public static void GoNow(DateTime dateSelected, Appointment pinAppt,int selectedAptNum,int iModule){
			OnModuleSelected(new ModuleEventArgs(dateSelected,pinAppt,selectedAptNum,iModule));
		}

		///<summary></summary>
		protected static void OnModuleSelected(ModuleEventArgs e){
			if(ModuleSelected !=null){
				ModuleSelected(e);
			}
		}
	}

	///<summary>This is used for our global module events.</summary>
	public delegate void ModuleEventHandler(ModuleEventArgs e);

	///<summary></summary>
	public class ModuleEventArgs : System.EventArgs{
		private DateTime dateSelected;
		private Appointment pinAppt;
		private int selectedAptNum;
		private int iModule;
		
		///<summary></summary>
		public ModuleEventArgs(DateTime dateSelected,Appointment pinAppt,int selectedAptNum,int iModule) : base(){
			this.dateSelected=dateSelected;
			this.pinAppt=pinAppt;
			this.selectedAptNum=selectedAptNum;
			this.iModule=iModule;
		}

		///<summary>If going to the ApptModule, this lets you pick a date.</summary>
		public DateTime DateSelected{
			get{return dateSelected;}
		}

		///<summary>Don't remember why I called this one pin.  I don't think it has anything to do with pinboard.</summary>
		public Appointment PinAppt{
			get{return pinAppt;}
		}

		///<summary></summary>
		public int SelectedAptNum{
			get{return selectedAptNum;}
		}

		///<summary></summary>
		public int IModule{
			get{return iModule;}
		}
	}

	/*=================================Class TelephoneNumbers============================================*/

	///<summary></summary>
	public class TelephoneNumbers{

		///<summary>Used in the tool that loops through the database fixing telephone numbers.  Also used in the patient import from XML tool.</summary>
		public static string ReFormat(string phoneNum){
			if(CultureInfo.CurrentCulture.Name!="en-US" && CultureInfo.CurrentCulture.Name!="en-CA"){
				return phoneNum;
			}
			Regex regex;
			regex=new Regex(@"^\d{10}");//eg. 5033635432
			if(regex.IsMatch(phoneNum)){
				return "("+phoneNum.Substring(0,3)+")"+phoneNum.Substring(3,3)+"-"+phoneNum.Substring(6);
			}
			regex=new Regex(@"^\d{3}-\d{3}-\d{4}");//eg. 503-363-5432
			if(regex.IsMatch(phoneNum)){
				return "("+phoneNum.Substring(0,3)+")"+phoneNum.Substring(4);
			}
			regex=new Regex(@"^\d-\d{3}-\d{3}-\d{4}");//eg. 1-503-363-5432 to 1(503)363-5432
			if(regex.IsMatch(phoneNum)){
				return phoneNum.Substring(0,1)+"("+phoneNum.Substring(2,3)+")"+phoneNum.Substring(6);
			}
			regex=new Regex(@"^\d{3} \d{3}-\d{4}");//eg 503 363-5432
			if(regex.IsMatch(phoneNum)){
				return "("+phoneNum.Substring(0,3)+")"+phoneNum.Substring(4);
			}
			//Keyush Shah 04/21/05 Added more formats:
			regex=new Regex(@"^\d{3} \d{3} \d{4}");//eg 916 363 5432
			if(regex.IsMatch(phoneNum)){
				return "("+phoneNum.Substring(0,3)+")"+phoneNum.Substring(4,3)+"-"+phoneNum.Substring(8,4);
			}
      regex=new Regex(@"^\(\d{3}\) \d{3} \d{4}");//eg (916) 363 5432
			if(regex.IsMatch(phoneNum)){
				return "("+phoneNum.Substring(1,3)+")"+phoneNum.Substring(6,3)+"-"+phoneNum.Substring(10,4);
			}
			regex=new Regex(@"^\(\d{3}\) \d{3}-\d{4}");//eg (916) 363-5432
			if(regex.IsMatch(phoneNum)){
				return "("+phoneNum.Substring(1,3)+")"+phoneNum.Substring(6,3)+"-"+phoneNum.Substring(10,4);
			}
			regex=new Regex(@"^\d{7}$");//eg 3635432
			if(regex.IsMatch(phoneNum)){
				return(phoneNum.Substring(0,3)+"-"+phoneNum.Substring(3));
			}
			return phoneNum;   
		}

		///<summary>reformats initial entry with each keystroke</summary>
		public static string AutoFormat(string phoneNum){
			if(CultureInfo.CurrentCulture.Name!="en-US"){
				return phoneNum;
			}
			if(Regex.IsMatch(phoneNum,@"^[2-9]$")){
				return "("+phoneNum;
			}
			if(Regex.IsMatch(phoneNum,@"^1\d$")){
				return "1("+phoneNum.Substring(1);
			}
			if(Regex.IsMatch(phoneNum,@"^\(\d\d\d\d$")){
				return( phoneNum.Substring(0,4)+")"+phoneNum.Substring(4));
			}
			if(Regex.IsMatch(phoneNum,@"^1\(\d\d\d\d$")){
				return( phoneNum.Substring(0,5)+")"+phoneNum.Substring(5));
			}
			if(Regex.IsMatch(phoneNum,@"^\(\d\d\d\)\d\d\d\d$")){
				return( phoneNum.Substring(0,8)+"-"+phoneNum.Substring(8));
			}
			if(Regex.IsMatch(phoneNum,@"^1\(\d\d\d\)\d\d\d\d$")){
				return( phoneNum.Substring(0,9)+"-"+phoneNum.Substring(9));
			}
			return phoneNum;
		}

		///<summary></summary>
		public static string FormatNumbersOnly(string phoneNum){
			string newPhoneNum="";
			for(int i=0;i<phoneNum.Length;i++){
				if(Char.IsNumber(phoneNum,i)){
					newPhoneNum+=phoneNum.Substring(i,1);
				}
			}
			if(newPhoneNum.Length==10){
				return newPhoneNum;
			}
			else{
				return "";
			}
		}

	}

	

}
