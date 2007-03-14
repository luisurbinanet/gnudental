using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the signal table in the database.</summary>
	public class Signal{
		///<summary>Primary key.</summary>
		public int SignalNum;
		///<summary>Foreign key to user.UserNum.  Not really used yet.</summary>
		public int FromUser;
		///<summary>InvalidTypes.None, InvalidTypes.Date, or combined flags for any LocalData</summary>
		public InvalidTypes ITypes;
		///<summary>If IType=Date, then this is the affected date in the Appointments module.</summary>
		public DateTime DateViewing;
		///<summary>Button, Text, or Invalid.</summary>
		public SignalType SigType;
		///<summary>If Text, then this is the message.</summary>
		public string SigText;
		///<summary></summary>
		public int GridCol;
		///<summary></summary>
		public int GridRow;
		///<summary>0=not pushed, 1=pushed.  Can be expanded later.</summary>
		public bool PushedState;
		///<summary>The exact server time when this signal was received.  This does not need to be set by sender since it's handled automatically.</summary>
		public DateTime SigDateTime;
		
		///<summary></summary>
		public Signal Copy(){
			Signal s=new Signal();
			s.SignalNum=SignalNum;
			s.FromUser=FromUser;
			s.ITypes=ITypes;
			s.DateViewing=DateViewing;
			s.SigType=SigType;
			s.SigText=SigText;
			s.GridCol=GridCol;
			s.GridRow=GridRow;
			s.PushedState=PushedState;
			s.SigDateTime=SigDateTime;
			return s;
		}

		//<summary></summary>
		/*private void Update(){
			string command= "UPDATE signal SET " 
				+"FromUser = '"    +POut.PInt   (FromUser)+"'"
				+",ITypes = '"     +POut.PInt   ((int)ITypes)+"'"
				+",DateViewing = '"+POut.PDate  (DateViewing)+"'"
				+",SigType = '"    +POut.PInt   ((int)SigType)+"'"
				+",SigText = '"    +POut.PString(SigText)+"'"
				+",GridCol = '"    +POut.PInt   (GridCol)+"'"
				+",GridRow = '"    +POut.PInt   (GridRow)+"'"
				+",PushedState = '"+POut.PBool  (PushedState)+"'"
				+",SigDateTime = '"+POut.PDateT (PushedState)+"'"
				+" WHERE SignalNum = '"+POut.PInt(SignalNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}*/

		///<summary></summary>
		public void Insert(){
			if(Prefs.RandomKeys){
				SignalNum=MiscData.GetKey("signal","SignalNum");
			}
			string command= "INSERT INTO signal (";
			if(Prefs.RandomKeys){
				command+="SignalNum,";
			}
			command+="FromUser,ITypes,DateViewing,SigType,SigText,GridCol,GridRow,PushedState,SigDateTime"
				+") VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(SignalNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (FromUser)+"', "
				+"'"+POut.PInt   ((int)ITypes)+"', "
				+"'"+POut.PDate  (DateViewing)+"', "
				+"'"+POut.PInt   ((int)SigType)+"', "
				+"'"+POut.PString(SigText)+"', "
				+"'"+POut.PInt  (GridCol)+"', "
				+"'"+POut.PInt   (GridRow)+"', "
				+"'"+POut.PBool  (PushedState)+"', "
				+"NOW())";//SigDateTime set to current server time
			DataConnection dcon=new DataConnection();
 			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				SignalNum=dcon.InsertID;
			}
		}

		//<summary></summary>
		/*public void InsertOrUpdate(bool isNew){
			if(isNew){
				Insert();
			}
			else{
				Update();
			}
		}*/

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
		
		///<summary>Gets all Signals Since a given DateTime.</summary>
		public static Signal[] Refresh(DateTime sinceDateT){
			string command=
				"SELECT * FROM signal "
				+"WHERE SigDateTime>'"+POut.PDateT(sinceDateT)+"' "
				+"ORDER BY SigDateTime";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			Signal[] List=new Signal[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Signal();
				List[i].SignalNum  = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].FromUser   = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].ITypes     = (InvalidTypes) PIn.PInt(table.Rows[i][2].ToString());
				List[i].DateViewing= PIn.PDate  (table.Rows[i][3].ToString());
				List[i].SigType    = (SignalType)   PIn.PInt(table.Rows[i][4].ToString());
				List[i].SigText    = PIn.PString(table.Rows[i][5].ToString());
				List[i].GridCol    = PIn.PInt   (table.Rows[i][6].ToString());
				List[i].GridRow    = PIn.PInt   (table.Rows[i][7].ToString());
				List[i].PushedState= PIn.PBool  (table.Rows[i][8].ToString());
				List[i].SigDateTime= PIn.PDateT (table.Rows[i][9].ToString());
			}
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

		///<summary>After a refresh, this gets a single text signal.  This will be significantly enhanced later.</summary>
		public static Signal GetTextSig(Signal[] signalList){
			for(int i=0;i<signalList.Length;i++){
				if(signalList[i].SigType!=SignalType.Text){
					continue;
				}
				return signalList[i];
			}
			return null;
		}

	
	}

	

	


}




















