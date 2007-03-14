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
			if(date.DayOfYear < DateTime.Now.DayOfYear)
				return DateTime.Now.Year-date.Year;
			return DateTime.Now.Year-date.Year-1;
		}

		///<summary></summary>
		public static string AgeToString(int age){
			if(age==0)
				return "";
			else
				return age.ToString();
		}

		///<summary>Computes balance for a single patient. Returns true if a refresh is needed.</summary>
		public static bool ComputeBalances(Procedure[] procList,ClaimProc[] claimProcList,Patient PatCur,PaySplit[] paySplitList,Adjustment[] AdjustmentList){
			//must have refreshed all 5 first
			double calcBal
				=Procedures.ComputeBal(procList)
				+ClaimProcs.ComputeBal(claimProcList)
				+Adjustments.ComputeBal(AdjustmentList)
				-PaySplits.ComputeBal(paySplitList)
				+PayPlans.ComputeBal(PatCur.PatNum);
			if(calcBal!=PatCur.EstBalance){
				Patient PatOld=PatCur.Copy();
				PatCur.EstBalance=calcBal;
				PatCur.Update(PatOld);
				return true;
			}
			return false;
		}

	}//end class shared

	/*=================================Class Messages=========================================
===========================================================================================*/

	///<summary>Handles sending messages between computers for buttons and for invalidating appointment screen and locally stored data. David Adams from Adivad Technologies was very helpful in providing the threading logic.</summary>
	public class Messages{

		///<summary></summary>
		public static void SendMessage(ODMessage msg){
			ThreadPool.QueueUserWorkItem(new WaitCallback(SendOneMessage),msg);
		}

		///<summary></summary>
		private static void SendOneMessage(Object myObject){
			ODMessage msg=(ODMessage)myObject;
			string msgTo;
			for(int i=0;i<Computers.List.Length;i++){
				msgTo=Computers.List[i].CompName;		
				if(msgTo==SystemInformation.ComputerName//don't send to self
					&& msg.MessageType!="Text"){//unless it's text
					continue;
				}
				msg.From=SystemInformation.ComputerName;
				try{
					TcpClient TcpClientSend=new TcpClient(msgTo, 2123);
					NetworkStream ns=TcpClientSend.GetStream();
					XmlSerializer serializer=new XmlSerializer(typeof(ODMessage));
					TextWriter writer=new StreamWriter(ns);
					serializer.Serialize(writer,msg);
					writer.Close();
				}
				catch{//(System.Exception e) {
					//MessageBox.Show(e.Message);
				}
			}
		}
				
		///<summary></summary>
		public static ODMessage RecMessage(string strMessage){
			StringReader sr=new StringReader(strMessage);
			XmlSerializer serializer=new XmlSerializer(typeof(ODMessage));
			ClaimForm tempClaimForm=new ClaimForm();
			try{
				return (ODMessage)serializer.Deserialize(sr);
			}
			catch(System.Exception e) {
				MessageBox.Show(e.Message);
				return null;
			}
		}

	}

	///<summary>From gets filled in during SendMessage. No need to fill it in ahead of time.</summary>
	public class ODMessage{
		///<summary></summary>
		public string From;
		///<summary>InvalidTypes.None, InvalidTypes.Date, or combined flags for any LocalData</summary>
		public InvalidTypes ITypes;
		///<summary></summary>
		public DateTime DateViewing;
		///<summary>"Button", "Text", or "Invalid"</summary>
		public string MessageType;
		///<summary></summary>
		public string Text;
		///<summary></summary>
		public int Row;
		///<summary></summary>
		public int Col;
		///<summary></summary>
		public bool Pushed;

		///<summary></summary>
		public ODMessage(InvalidTypes iTypes,DateTime dateViewing,string messageType,string text,
			int row, int col, bool pushed)
		{
			ITypes=iTypes;
			DateViewing=dateViewing;
			MessageType=messageType;
			Text=text;
			Row=row;
			Col=col;
			Pushed=pushed;
		}

		///<summary>This constructor is just here so the serializer will work.</summary>
		public ODMessage(){

		}
	}


/*=================================Class DataValid=========================================
===========================================================================================*/

	///<summary></summary>
	public class DataValid{

		///<summary></summary>
		public static event OpenDental.ValidEventHandler BecameInvalid;	

		///<summary>Triggers an event that causes a signal to be sent to all other computers telling them what kind of locally stored data needs to be updated.  Either supply a set of flags for the types, or supply a date if the appointment screen needs to be refreshed.</summary>
		public static void SetInvalid(InvalidTypes itypes){
			OnBecameInvalid(new OpenDental.ValidEventArgs(DateTime.MinValue,itypes));
		}

		///<summary>Triggers an event that causes a signal to be sent to all other computers telling them what kind of locally stored data needs to be updated.  Either supply a set of flags for the types, or supply a date if the appointment screen needs to be refreshed.</summary>
		public static void SetInvalid(DateTime date){
			OnBecameInvalid(new OpenDental.ValidEventArgs(date,InvalidTypes.Date));
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
		
		///<summary></summary>
		public ValidEventArgs(DateTime dateViewing, InvalidTypes itypes) : base(){
			this.dateViewing=dateViewing;
			this.itypes=itypes;
		}

		///<summary></summary>
		public DateTime DateViewing{get{return dateViewing;}}
		///<summary></summary>
		public InvalidTypes ITypes{get{return itypes;}}
	}

	/*=================================Class ExitApplicationNow=========================================
===========================================================================================*/

	///<summary>This really needs to be rewritten.  I mean it works, but it's clumsy.</summary>
	public class ExitApplicationNow{
		///<summary></summary>
		public static event System.EventHandler WantsToExit;

		///<summary></summary>
		public void ExitNow(){
			OnWantsToExit(new System.EventArgs());
		}

		///<summary></summary>
		protected virtual void OnWantsToExit(System.EventArgs e){
			if(WantsToExit !=null){
				WantsToExit(this,e);
			}
		}

	}

	/*=================================Class TelephoneNumbers============================================*/

	///<summary></summary>
	public class TelephoneNumbers{

		///<summary></summary>
		public static string ReFormat(string phoneNum){
			//only used in the tool that loops through the database fixing telephone numbers.
			Regex regex;
			regex=new Regex(@"^\d{10}");//eg. 5033635432
			if(regex.IsMatch(phoneNum)){
				return("("+phoneNum.Substring(0,3)+")"+phoneNum.Substring(3,3)+"-"+phoneNum.Substring(6));
			}
			regex=new Regex(@"^\d{3}-\d{3}-\d{4}");//eg. 503-363-5432
			if(regex.IsMatch(phoneNum)){
				return("("+phoneNum.Substring(0,3)+")"+phoneNum.Substring(4));
			}
			regex=new Regex(@"^\d-\d{3}-\d{3}-\d{4}");//eg. 1-503-363-5432 to 1(503)363-5432
			if(regex.IsMatch(phoneNum)){
				return(phoneNum.Substring(0,1)+"("+phoneNum.Substring(2,3)+")"+phoneNum.Substring(6));
			}
			regex=new Regex(@"^\d{3} \d{3}-\d{4}");//eg 503 363-5432
			if(regex.IsMatch(phoneNum)){
				return("("+phoneNum.Substring(0,3)+")"+phoneNum.Substring(4));
			}
			return phoneNum;     
		}

		///<summary></summary>
		public static string AutoFormat(string phoneNum){
			//reformats initial entry with each keystroke
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
