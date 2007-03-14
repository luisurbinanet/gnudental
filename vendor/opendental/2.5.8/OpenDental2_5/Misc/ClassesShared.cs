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
using System.Windows.Forms;

namespace OpenDental{

	///<summary></summary>
	public class Shared{

		///<summary></summary>
		public Shared(){
			
		}//end constructor
/*
		public bool SelectPatient(){
			FormSelectPatient formSelectPatient2 = new FormSelectPatient();
			formSelectPatient2.ShowDialog();
			if (formSelectPatient2.DialogResult == DialogResult.OK){
				InsPlans InsPlans=new InsPlans();
				InsPlans.Refresh();
				CovPats CovPats=new CovPats();
				CovPats.Refresh();
				return true;
			}
			else return false;
		}*/

		/*public bool ChangePatient(int patNum){
			Patients Patients=new Patients();
			Patients.GetFamily(patNum);
			InsPlans InsPlans=new InsPlans();
			InsPlans.Refresh();
			CovPats CovPats=new CovPats();
			CovPats.Refresh();
			return true;
		}*/

		///<summary></summary>
		public static string DateToAge(DateTime aDate){
			if(aDate.Year<1890) return "";
			if(aDate.DayOfYear < DateTime.Now.DayOfYear)
				return (DateTime.Now.Year-aDate.Year).ToString();
			return (DateTime.Now.Year-aDate.Year-1).ToString();
		}

		///<summary></summary>
		public static void ComputeBalances(){//operates on Patients.Cur
			//must have refreshed all 5 first
			double curBal=Patients.Cur.EstBalance;
			Patients.Cur.EstBalance=Procedures.ComputeBal()+ClaimProcs.ComputeBal()
				+Adjustments.ComputeBal()-PaySplits.ComputeBal()+PayPlans.ComputeBal();
			if(curBal!=Patients.Cur.EstBalance){
				Patients.UpdateCur();
				Patients.GetFamily(Patients.Cur.PatNum);
			}
		}

	}//end class shared

	/*=================================Class=========================================
===========================================================================================*/

	/*public class ChartNumDent{//dentrix
		private static string chartNum;

		public ChartNumDent(){//constructor

		}

		public delegate bool MyDelegate(int first, int second);

		[ DllImport("user32.dll") ] 
		static extern int GetWindowText(int hWnd, StringBuilder text, int count); 
		[ DllImport("user32.dll") ] 
		static extern int EnumWindows(MyDelegate x, int lParam);
		
		public static bool EnumWindowsProc (int hwnd, int lParam){
			const int nChars = 256;
			StringBuilder buffer = new StringBuilder(nChars);
			if (GetWindowText(hwnd, buffer, nChars)>0){  // get title bar text
				if (string.Compare(buffer.ToString(),0,"Dentrix Ledger",0,14)==0){
					chartNum=buffer.ToString().Substring(buffer.Length-8,6);
					return false;
				}//end if
				else return true;
			}//end if
			else return true;
			//return true; //continue enumeration
		}//end EnumWindowsProc

		public static string GetChartNum(){
			MyDelegate UseEnum = new MyDelegate(EnumWindowsProc);
			EnumWindows(UseEnum, 0);
			return chartNum;
		}


	}//end class ChartNumDent*/

	///<summary></summary>
	public class Messages{
		///<summary></summary>
		public static MessageInvalid RecdMessage;
		///<summary></summary>
		public static MessageButtons RecdMsgBut;
		///<summary></summary>
		public static MessageInvalid MessageToSend;
		///<summary></summary>
		public static MessageButtons ButtonsToSend;
		private static Thread Thread2;
		
		///<summary></summary>
		public static void SendMessage(){
			//fix.  check to see if thread is done yet.
			//if(Thread2==null)//for the first time
			//	Thread2=new Thread(new ThreadStart(SendMessageThread));
			Thread2 = new Thread(new ThreadStart(SendMessageThread));
			Thread2.Start();
		}

		///<summary></summary>
		public static void SendMessageThread(){
			string msgTo;
			for(int i=0;i<Computers.List.Length;i++){
				msgTo = Computers.List[i].CompName;		
				if(msgTo==SystemInformation.ComputerName){
					continue;
				}
				//if(
				MessageToSend.From = SystemInformation.ComputerName;
				try{
					TcpClient TcpClientSend = new TcpClient(msgTo, 2123);//was 2112
					NetworkStream ns = TcpClientSend.GetStream();
					StreamWriter writer = new StreamWriter(ns);
					XmlWriter xmlwriter=new XmlTextWriter(writer);
					xmlwriter.WriteStartElement("MessageInvalid");
					xmlwriter.WriteElementString("From", MessageToSend.From);
					xmlwriter.WriteElementString("Type", MessageToSend.Type);
					xmlwriter.WriteElementString("DateViewing", POut.PDate(MessageToSend.DateViewing));
					xmlwriter.WriteEndElement();
					writer.Close();
					xmlwriter.Close();
					//byte[] sendBytes = Encoding.ASCII.GetBytes(strSend);
					//ns.Write(sendBytes,0,sendBytes.Length);
					ns.Close();
					TcpClientSend.Close();
				}
				catch{
					//MessageBox.Show(msgTo+" not found.");
				}
			}
		}//end SendMessages

		///<summary></summary>
		public static void SendButtons(){
			Thread2 = new Thread(new ThreadStart(SendButtonsThread));
			Thread2.Start();
		}

		///<summary></summary>
		public static void SendButtonsThread(){
			string msgTo;
			for(int i=0;i<Computers.List.Length;i++){
				msgTo = Computers.List[i].CompName;		
				if(msgTo==SystemInformation.ComputerName//don't send to self
					&& ButtonsToSend.Type!="Text"){//unless it's text
					continue;
				}
				ButtonsToSend.From = SystemInformation.ComputerName;
				try{
					TcpClient TcpClientSend = new TcpClient(msgTo, 2123);
					NetworkStream ns = TcpClientSend.GetStream();
					//TcpClientSend.
					StreamWriter writer = new StreamWriter(ns);
					XmlWriter xmlwriter=new XmlTextWriter(writer);
					xmlwriter.WriteStartElement("MessageButtons");
					xmlwriter.WriteElementString("From", ButtonsToSend.From);
					xmlwriter.WriteElementString("Type", ButtonsToSend.Type);
					xmlwriter.WriteElementString("Text", ButtonsToSend.Text);
					xmlwriter.WriteElementString("Row", POut.PInt(ButtonsToSend.Row));
					xmlwriter.WriteElementString("Col", POut.PInt(ButtonsToSend.Col));	
					xmlwriter.WriteElementString("Pushed", POut.PBool(ButtonsToSend.Pushed));
					xmlwriter.WriteEndElement();
					writer.Close();
					xmlwriter.Close();
					//byte[] sendBytes = Encoding.ASCII.GetBytes(strSend);
					//ns.Write(sendBytes,0,sendBytes.Length);
					ns.Close();
					TcpClientSend.Close();
				}
				catch{
					//MessageBox.Show(msgTo+" not found.");
				}
			}
		}//end SendMessages
				
		///<summary></summary>
		public static void RecMessage(string strMessage){
			//MessageBox.Show(strMessage);
			StringReader stringReader2 = new StringReader(strMessage);
			RecdMessage = new MessageInvalid();
			RecdMsgBut = new MessageButtons();
			XmlTextReader reader = new XmlTextReader(stringReader2);
			reader.Read();
			switch (reader.Name){
				case "MessageInvalid" :
					reader.Read();
					reader.Read();
					RecdMessage.From = reader.Value;
					reader.Read();
					reader.Read();
					reader.Read();
					RecdMessage.Type = reader.Value;
					reader.Read();
					reader.Read();
					reader.Read();
					RecdMessage.DateViewing = PIn.PDate(reader.Value);
					/*reader.Read();
					reader.Read();
					reader.Read();
					recdMessage.PatNum = reader.Value;
					reader.Read();
					reader.Read();
					reader.Read();
					recdMessage.TableChanged = reader.Value;*/
					break;
				case "MessageButtons" :
					reader.Read();
					reader.Read();
					RecdMsgBut.From = reader.Value;
					reader.Read();
					reader.Read();
					reader.Read();
					RecdMsgBut.Type = reader.Value;
					reader.Read();
					reader.Read();
					reader.Read();
					RecdMsgBut.Text = reader.Value;
					reader.Read();
					reader.Read();
					reader.Read();
					RecdMsgBut.Row = PIn.PInt(reader.Value);
					reader.Read();
					reader.Read();
					reader.Read();
					RecdMsgBut.Col = PIn.PInt(reader.Value);
					reader.Read();
					reader.Read();
					reader.Read();
					RecdMsgBut.Pushed = PIn.PBool(reader.Value);
					//MessageBox.Show(RecdMsgBut.Type);
					break;
			}//end switch 
			//MessageBox.Show(RecdMessage.From+","+RecdMessage.Type+","+RecdMessage.DateViewing);
			reader.Close();
			stringReader2.Close();
		}//end RecMessages


	}//end class Messages

	///<summary></summary>
	public struct MessageInvalid{
		///<summary></summary>
		public string From;
		///<summary></summary>
		public string Type;//"LocalData" or "Date"
		///<summary></summary>
		public DateTime DateViewing;
		//public string PatNum;
		//public string TableChanged;
		//might include type of TableChange: insert, delete, update, etc. 
	}

	///<summary></summary>
	public struct MessageButtons{
		///<summary></summary>
		public string From;
		///<summary></summary>
		public string Type;//"Button" or "Text"
		///<summary></summary>
		public string Text;
		///<summary></summary>
		public int Row;
		///<summary></summary>
		public int Col;
		///<summary></summary>
		public bool Pushed;
	}

/*=================================Class DataValid=========================================
===========================================================================================*/

	///<summary></summary>
	public class DataValid{
		/*
		public struct Module{
			public static bool ApptDate;
			public static bool ApptPat;
			public static bool Acct;
			public static bool Chart;
			public static bool Docs;
			public static bool Fam;
			public static bool Tools;
			public static bool Treat;
		}
		*/
		///<summary></summary>
		public static DateTime DateViewing;
		//public static int PatNum;
		//public static string Table;
		///<summary></summary>
		public static InvalidType IType;

		/*public void InvalidPat(){
			Module.ApptPat=false;
			Module.Acct=false;
			Module.Chart=false;
			Module.Docs=false;
			Module.Fam=false;
			Module.Tools=false;
			Module.Treat=false;
		}

		public void InvalidDate(){
			Module.ApptDate=false;
		}*/
		///<summary></summary>
		public static event System.EventHandler BecameInvalid;

		///<summary></summary>
		public void SetInvalid(){
			//this is the main method,but you first have to set:
			//IType and possibly DateViewing
			OnBecameInvalid(new System.EventArgs());
		}

		///<summary></summary>
		protected virtual void OnBecameInvalid(System.EventArgs e){
			if(BecameInvalid !=null){
				BecameInvalid(this,e);
			}
		}



	}

	/*public class ValidEventArgs : EventArgs{
		private DateTime dateViewing;
		private int patNum;
		private string tableChanged;
		
		public ValidEventArgs(DateTime dateViewing, int patNum, string tableChanged) : base(){
			this.dateViewing=dateViewing;
			this.patNum=patNum;
			this.tableChanged=tableChanged;
		}

		public DateTime DateViewing{get{return dateViewing;}}
		public int PatNum{get{return patNum;}}
		public string TableChanged{get{return tableChanged;}}
	}*/

	/*=================================Class ExitApplicationNow=========================================
===========================================================================================*/

	///<summary></summary>
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

	

}//end namespace
