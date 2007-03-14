using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>This table stores references to the sequence of sounds and lights that should get sent out with a button push.</summary>
	public class SigButDefElement:IComparable{
		///<summary>Primary key.</summary>
		public int ElementNum;
		///<summary>FK to sigbutdef.SigButDefNum  A few elements are usually attached to a single button.</summary>
		public int SigButDefNum;
		///<summary>FK to sigelementdef.SigElementDefNum, which contains the actual sound or light.</summary>
		public int SigElementDefNum;

		///<summary>IComparable.CompareTo implementation.  This is used to order SigButDefElements by type so that the sounds are fired in the correct sequence.</summary>
		public int CompareTo(object obj) {
			if(!(obj is SigButDefElement)) {
				throw new ArgumentException("object is not a SigButDefElement");
			}
			SigButDefElement element=(SigButDefElement)obj;
			int type1=(int)(SigElementDefs.GetElement(SigElementDefNum).SigElementType);//0,1,or 2
			int type2=(int)(SigElementDefs.GetElement(element.SigElementDefNum).SigElementType);
			return type1.CompareTo(type2);
		}
		
		///<summary></summary>
		public SigButDefElement Copy(){
			SigButDefElement s=new SigButDefElement();
			s.ElementNum=ElementNum;
			s.SigButDefNum=SigButDefNum;
			s.SigElementDefNum=SigElementDefNum;
			return s;
		}

		/*
		///<summary>This will never happen</summary>
		public void Update(){
			string command= "UPDATE SigButDefElement SET " 
				+"FromUser = '"    +POut.PString(FromUser)+"'"
				+",ITypes = '"     +POut.PInt   ((int)ITypes)+"'"
				+",DateViewing = '"+POut.PDate  (DateViewing)+"'"
				+",SigType = '"    +POut.PInt   ((int)SigType)+"'"
				+" WHERE SigButDefElementNum = '"+POut.PInt(SigButDefElementNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}*/

		///<summary></summary>
		public void Insert(){
			string command= "INSERT INTO sigbutdefelement (";
			command+="SigButDefNum,SigElementDefNum"
				+") VALUES(";
			command+=
				 "'"+POut.PInt   (SigButDefNum)+"', "
				+"'"+POut.PInt   (SigElementDefNum)+"')";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			ElementNum=dcon.InsertID;
		}

		///<summary></summary>
		public void Delete(){
			string command= "DELETE from sigbutdefelement WHERE ElementNum = '"+POut.PInt(ElementNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}


	}

	/*=========================================================================================
		=================================== class SigButDefElements ==========================================*/

	///<summary></summary>
	public class SigButDefElements{
		///<summary>A list of all elements for all buttons</summary>
		public static SigButDefElement[] List;
		
		///<summary>Gets all SigButDefElements for all buttons, ordered by type: user,extras, message.</summary>
		public static void Refresh(){
			string command="SELECT * FROM sigbutdefelement";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			List=new SigButDefElement[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new SigButDefElement();
				List[i].ElementNum      = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].SigButDefNum    = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].SigElementDefNum= PIn.PInt   (table.Rows[i][2].ToString());
			}
			Array.Sort(List);
		}

		///<summary>Loops through the SigButDefElement list and pulls out all elements for one specific button.</summary>
		public static SigButDefElement[] GetForButton(int sigButDefNum){
			ArrayList AL=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].SigButDefNum==sigButDefNum){
					AL.Add(List[i].Copy());
				}
			}
			SigButDefElement[] retVal=new SigButDefElement[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
			//already ordered because List is ordered.
		}

		

	
	}

	

	


}




















