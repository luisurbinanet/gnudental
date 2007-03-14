using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>These are the actual elements attached to each signal that is sent.  They contain references to the sounds and lights that should result.</summary>
	public class SigElement:IComparable{
		///<summary>Primary key.</summary>
		public int SigElementNum;
		///<summary>FK to sigelementdef.SigElementDefNum</summary>
		public int SigElementDefNum;
		///<summary>FK to signal.SignalNum.</summary>
		public int SignalNum;

		///<summary>IComparable.CompareTo implementation.  This is used to order sigelements by type so that the sounds are fired in the correct sequence.</summary>
		public int CompareTo(object obj) {
			if(!(obj is SigElement)) {
				throw new ArgumentException("object is not a SigElement");
			}
			SigElement element=(SigElement)obj;
			int type1=(int)(SigElementDefs.GetElement(SigElementDefNum).SigElementType);//0,1,or 2
			int type2=(int)(SigElementDefs.GetElement(element.SigElementDefNum).SigElementType);
			return type1.CompareTo(type2);
		}
		
		///<summary></summary>
		public SigElement Copy(){
			SigElement s=new SigElement();
			s.SigElementNum=SigElementNum;
			s.SigElementDefNum=SigElementDefNum;
			s.SignalNum=SignalNum;
			return s;
		}

		/*
		///<summary>This will never happen</summary>
		public void Update(){
			string command= "UPDATE sigelement SET " 
				+"FromUser = '"    +POut.PString(FromUser)+"'"
				+",ITypes = '"     +POut.PInt   ((int)ITypes)+"'"
				+",DateViewing = '"+POut.PDate  (DateViewing)+"'"
				+",SigType = '"    +POut.PInt   ((int)SigType)+"'"
				+" WHERE SigElementNum = '"+POut.PInt(SigElementNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}*/

		///<summary></summary>
		public void Insert(){
			if(Prefs.RandomKeys){
				SigElementNum=MiscData.GetKey("sigelement","SigElementNum");
			}
			string command= "INSERT INTO sigelement (";
			if(Prefs.RandomKeys){
				command+="SigElementNum,";
			}
			command+="SigElementDefNum,SignalNum"
				+") VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(SigElementNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (SigElementDefNum)+"', "
				+"'"+POut.PInt   (SignalNum)+"')";
			DataConnection dcon=new DataConnection();
 			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				SigElementNum=dcon.InsertID;
			}
		}

		//<summary>There's no such thing as deleting a SigElement</summary>
		/*public void Delete(){
			string command= "DELETE from SigElement WHERE SigElementNum = '"
				+POut.PInt(SigElementNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}*/


	}

	/*=========================================================================================
		=================================== class SigElements ==========================================*/

	///<summary></summary>
	public class SigElements{
		
		///<summary>Gets all SigElements for a set of Signals, ordered by type: user,extras, message.</summary>
		public static SigElement[] GetElements(Signal[] signalList){
			if(signalList.Length==0){
				return new SigElement[0];
			}
			string command="SELECT * FROM sigelement WHERE (";
			for(int i=0;i<signalList.Length;i++){
				if(i>0){
					command+=" OR ";
				}
				command+="SignalNum="+POut.PInt(signalList[i].SignalNum);
			}
			command+=")";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			SigElement[] List=new SigElement[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new SigElement();
				List[i].SigElementNum   = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].SigElementDefNum= PIn.PInt   (table.Rows[i][1].ToString());
				List[i].SignalNum       = PIn.PInt   (table.Rows[i][2].ToString());
			}
			Array.Sort(List);
			return List;
		}

		///<summary>Loops through the supplied sigElement list and pulls out all elements for one specific signal.</summary>
		public static SigElement[] GetForSig(SigElement[] elementList,int signalNum){
			ArrayList AL=new ArrayList();
			for(int i=0;i<elementList.Length;i++){
				if(elementList[i].SignalNum==signalNum){
					AL.Add(elementList[i].Copy());
				}
			}
			SigElement[] retVal=new SigElement[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		

	
	}

	

	


}




















