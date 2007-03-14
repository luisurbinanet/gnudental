using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>An actual signal that gets sent out as part of the messaging functionality.</summary>
	public class Signal:IComparable{
		///<summary>Primary key.</summary>
		public int SignalNum;
		///<summary>Text version of 'user' this message was sent from, which can actually be any description of a group or individual.</summary>
		public string FromUser;
		///<summary>Enum:InvalidTypes  None, Date, or combined flags for any LocalData</summary>
		public InvalidTypes ITypes;
		///<summary>If IType=Date, then this is the affected date in the Appointments module.</summary>
		public DateTime DateViewing;
		///<summary>Enum:SignalType  Button, or Invalid.</summary>
		public SignalType SigType;
		///<summary>This is only used if the type is button, and the user types in some text.  This is the typed portion and does not include any of the text that was on the buttons.  These types of signals are displayed in their own separate list in addition to any light and sound that they may cause.</summary>
		public string SigText;
		///<summary>The exact server time when this signal was entered into db.  This does not need to be set by sender since it's handled automatically.</summary>
		public DateTime SigDateTime;
		///<summary>Text version of 'user' this message was sent to, which can actually be any description of a group or individual.</summary>
		public string ToUser;
		///<summary>If this signal has been acknowledged, then this will contain the date and time.  This is how lights get turned off also.</summary>
		public DateTime AckTime;
		///<summary>Not a database field.  The sounds and lights attached to the signal.</summary>
		public SigElement[] ElementList;

		///<summary>IComparable.CompareTo implementation.  This is used to order signals.  This is needed because ordering signals is too complex to do with a query.</summary>
		public int CompareTo(object obj) {
			if(!(obj is Signal)) {
				throw new ArgumentException("object is not a Signal");
			}
			Signal sig=(Signal)obj;
			DateTime date1;
			DateTime date2;
			if(AckTime.Year<1880){//if not acknowledged
				date1=SigDateTime;
			}
			else{
				date1=AckTime;
			}
			if(sig.AckTime.Year<1880) {//if not acknowledged
				date2=sig.SigDateTime;
			}
			else {
				date2=sig.AckTime;
			}
			return date1.CompareTo(date2);
		}
		
		///<summary></summary>
		public Signal Copy(){
			Signal s=new Signal();
			s.SignalNum=SignalNum;
			s.FromUser=FromUser;
			s.ITypes=ITypes;
			s.DateViewing=DateViewing;
			s.SigType=SigType;
			s.SigText=SigText;
			s.SigDateTime=SigDateTime;
			s.ToUser=ToUser;
			s.AckTime=AckTime;
			s.ElementList=new SigElement[ElementList.Length];
			ElementList.CopyTo(s.ElementList,0);
			return s;
		}

		///<summary></summary>
		public void Update(){
			string command= "UPDATE signal SET " 
				+"FromUser = '"    +POut.PString(FromUser)+"'"
				+",ITypes = '"     +POut.PInt   ((int)ITypes)+"'"
				+",DateViewing = '"+POut.PDate  (DateViewing)+"'"
				+",SigType = '"    +POut.PInt   ((int)SigType)+"'"
				+",SigText = '"    +POut.PString(SigText)+"'"
				//+",SigDateTime = '"+POut.PDateT (SigDateTime)+"'"//we don't want to ever update this
				+",ToUser = '"     +POut.PString(ToUser)+"'"
				+",AckTime = '"    +POut.PDateT (AckTime)+"'"
				+" WHERE SignalNum = '"+POut.PInt(SignalNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Insert(){
			//we need to explicitly get the server time in advance rather than using NOW(),
			//because we need to update the signal object soon after creation.
			//DateTime now=ClockEvents.GetServerTime();
			if(Prefs.RandomKeys){
				SignalNum=MiscData.GetKey("signal","SignalNum");
			}
			string command= "INSERT INTO signal (";
			if(Prefs.RandomKeys){
				command+="SignalNum,";
			}
			command+="FromUser,ITypes,DateViewing,SigType,SigText,SigDateTime,ToUser,AckTime"
				+") VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(SignalNum)+"', ";
			}
			command+=
				 "'"+POut.PString(FromUser)+"', "
				+"'"+POut.PInt   ((int)ITypes)+"', "
				+"'"+POut.PDate  (DateViewing)+"', "
				+"'"+POut.PInt   ((int)SigType)+"', "
				+"'"+POut.PString(SigText)+"', "
				//+"'"+POut.PDateT (now)+"', "
				+"NOW(), "
				+"'"+POut.PString(ToUser)+"', "
				+"'"+POut.PDateT (AckTime)+"')";
			DataConnection dcon=new DataConnection();
 			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				SignalNum=dcon.InsertID;
			}
		}

		//<summary>There's no such thing as deleting a signal</summary>
		/*public void Delete(){
			string command= "DELETE from Signal WHERE SignalNum = '"
				+POut.PInt(SignalNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}*/


	}

	/*=========================================================================================
		=================================== class Signals ==========================================*/

	///<summary></summary>
	public class Signals{
		
		///<summary>Gets all Signals and Acks Since a given DateTime.  If it can't connect to the database, then it no longer throws an error, but instead returns a list of length 0.  Remeber that the supplied dateTime is server time.  This has to be accounted for.</summary>
		public static Signal[] RefreshTimed(DateTime sinceDateT){
			string command="SELECT * FROM signal "
				+"WHERE SigDateTime>'"+POut.PDateT(sinceDateT)+"' "
				+"OR AckTime>'"+POut.PDateT(sinceDateT)+"' "
				+"ORDER BY SigDateTime";
			//note: this might return an occasional row that has both times newer.
			Signal[] sigList=RefreshAndFill(command);
			SigElement[] sigElementsAll=SigElements.GetElements(sigList);
			for(int i=0;i<sigList.Length;i++){
				sigList[i].ElementList=SigElements.GetForSig(sigElementsAll,sigList[i].SignalNum);
			}
			return sigList;
		}

		///<summary>This excludes all Invalids.  It is only concerned with text and button messages.  It includes all messages, whether acked or not.  It's up to the UI to filter out acked if necessary.  Also includes all unacked messages regardless of date.</summary>
		public static ArrayList RefreshFullText(DateTime sinceDateT){
			string command="SELECT * FROM signal "
				+"WHERE (SigDateTime>'"+POut.PDateT(sinceDateT)+"' "
				+"OR AckTime>'"+POut.PDateT(sinceDateT)+"' "
				+"OR AckTime<'1880-01-01') "//always include all unacked.
				+"AND SigType="+POut.PInt((int)SignalType.Button)
				+" ORDER BY SigDateTime";
			//note: this might return an occasional row that has both times newer.
			Signal[] sigList=RefreshAndFill(command);
			SigElement[] sigElementsAll=SigElements.GetElements(sigList);
			for(int i=0;i<sigList.Length;i++) {
				sigList[i].ElementList=SigElements.GetForSig(sigElementsAll,sigList[i].SignalNum);
			}
			ArrayList retVal=new ArrayList(sigList);
			return retVal;
		}

		///<summary>Only used when starting up to get the current button state.  Only gets unacked messages.  There may well be extra and useless messages included.  But only the lights will be used anyway, so it doesn't matter.</summary>
		public static Signal[] RefreshCurrentButState(){
			string command="SELECT * FROM signal "
				+"WHERE SigType=0 "//buttons only
				+"AND AckTime<'1880-01-01' "
				+"ORDER BY SigDateTime";
			Signal[] sigList=RefreshAndFill(command);
			SigElement[] sigElementsAll=SigElements.GetElements(sigList);
			for(int i=0;i<sigList.Length;i++) {
				sigList[i].ElementList=SigElements.GetForSig(sigElementsAll,sigList[i].SignalNum);
			}
			return sigList;
		}

		private static Signal[] RefreshAndFill(string command){
			MySqlConnection con=new MySqlConnection(FormChooseDatabase.GetConnectionString());
			MySqlCommand cmd=new MySqlCommand();
			MySqlDataAdapter da;
			Signal[] List;
			cmd.Connection=con;
			cmd.CommandText=command;
			DataTable table=new DataTable();
			try{
				da=new MySqlDataAdapter(cmd);
				da.Fill(table);
				con.Close();
			}
			catch{
				con.Close();
				List=new Signal[0];
				return List;
			}
			List=new Signal[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Signal();
				List[i].SignalNum  = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].FromUser   = PIn.PString(table.Rows[i][1].ToString());
				List[i].ITypes     = (InvalidTypes) PIn.PInt(table.Rows[i][2].ToString());
				List[i].DateViewing= PIn.PDate  (table.Rows[i][3].ToString());
				List[i].SigType    = (SignalType)   PIn.PInt(table.Rows[i][4].ToString());
				List[i].SigText    = PIn.PString(table.Rows[i][5].ToString());
				List[i].SigDateTime= PIn.PDateT (table.Rows[i][6].ToString());
				List[i].ToUser     = PIn.PString(table.Rows[i][7].ToString());
				List[i].AckTime    = PIn.PDateT (table.Rows[i][8].ToString());
			}
			Array.Sort(List);
			return List;
		}

		///<summary>After a refresh, this is used to determine whether the Appt Module needs to be refreshed.  Must supply the current date showing as well as the recently retrieved signal list.</summary>
		public static bool ApptNeedsRefresh(Signal[] signalList,DateTime dateTimeShowing){
			for(int i=0;i<signalList.Length;i++){
				if(signalList[i].ITypes==InvalidTypes.Date && signalList[i].DateViewing.Date==dateTimeShowing){
					return true;
				}
			}
			return false;
		}

		///<summary>After a refresh, this is used to get a single value representing all flags of types that need to be refreshed.   Types of Date are not included.</summary>
		public static InvalidTypes GetInvalidTypes(Signal[] signalList){
			InvalidTypes retVal=0;
			for(int i=0;i<signalList.Length;i++){
				if(signalList[i].SigType!=SignalType.Invalid){
					continue;
				}
				if(signalList[i].ITypes==InvalidTypes.Date){
					continue;
				}
				retVal=retVal | signalList[i].ITypes;
			}
			return retVal;
		}

		///<summary>After a refresh, this gets a list of only the button signals.</summary>
		public static Signal[] GetButtonSigs(Signal[] signalList){
			ArrayList AL=new ArrayList();
			for(int i=0;i<signalList.Length;i++){
				if(signalList[i].SigType!=SignalType.Button){
					continue;
				}
				AL.Add(signalList[i]);
			}
			Signal[] retVal=new Signal[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		///<summary>When user clicks on a colored light, they intend to ack it to turn it off.  This acks all signals with the specified index.  This is in case multiple signals have been created from different workstations.  This acks them all in one shot.  Must specify a time because you only want to ack signals earlier than the last time this workstation was refreshed.  A newer signal would not get acked.
		///If this seems slow, then I will need to check to make sure all these tables are properly indexed.</summary>
		public static void AckButton(int buttonIndex,DateTime time){
			string command= "UPDATE signal,sigelement,sigelementdef "
				+"SET signal.AckTime = NOW() "
				+"WHERE signal.AckTime < '1880-01-01' "
				+"AND SigDateTime <= '"+POut.PDateT(time)+"' "
				+"AND signal.SignalNum=sigelement.SignalNum "
				+"AND sigelement.SigElementDefNum=sigelementdef.SigElementDefNum "
				+"AND sigelementdef.LightRow="+POut.PInt(buttonIndex);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

	
	}

	

	


}




















