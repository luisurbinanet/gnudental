using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental {

	/// <summary>This defines the items that will be available for clicking when composing a manual message.  Also, these are referred to in the button definitions as a sequence of elements.</summary>
	public class SigElementDef{
		///<summary>Primary key.</summary>
		public int SigElementDefNum;
		///<summary>If this element should cause a button to light up, this would be the row.  0 means none.</summary>
		public int LightRow;
		///<summary>If a light row is set, this is the color it will turn when triggered.  Ack sets it back to white.  Note that color and row can be in two separate elements of the same signal.</summary>
		public Color LightColor;
		///<summary>Enum:SignalElementType  0=User,1=Extra,2=Message.</summary>
		public SignalElementType SigElementType;
		///<summary>The text that shows for the element, like the user name or the two word message.  No long text is stored here.</summary>
		public string SigText;
		///<summary>The sound to play for this element.  Wav file stored in the database in string format until "played".  If empty string, then no sound.</summary>
		public string Sound;
		///<summary>The order of this element within the list of the same type.</summary>
		public int ItemOrder;

		///<summary></summary>
		public SigElementDef Copy() {
			SigElementDef s=new SigElementDef();
			s.SigElementDefNum=SigElementDefNum;
			s.LightRow=LightRow;
			s.LightColor=LightColor;
			s.SigElementType=SigElementType;
			s.SigText=SigText;
			s.Sound=Sound;
			s.ItemOrder=ItemOrder;
			return s;
		}

		///<summary></summary>
		public void Update() {
			string command="UPDATE sigelementdef SET " 
				+"LightRow = '"       +POut.PInt   (LightRow)+"'"
				+",LightColor = '"    +POut.PInt   (LightColor.ToArgb())+"'"
				+",SigElementType = '"+POut.PInt   ((int)SigElementType)+"'"
				+",SigText = '"       +POut.PString(SigText)+"'"
				+",Sound = '"         +POut.PString(Sound)+"'"
				+",ItemOrder = '"     +POut.PInt   (ItemOrder)+"'"
				+" WHERE SigElementDefNum  ='"+POut.PInt   (SigElementDefNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Insert() {
			string command="INSERT INTO sigelementdef (LightRow,LightColor,SigElementType,SigText,Sound,"
				+"ItemOrder) VALUES("
				+"'"+POut.PInt   (LightRow)+"', "
				+"'"+POut.PInt   (LightColor.ToArgb())+"', "
				+"'"+POut.PInt   ((int)SigElementType)+"', "
				+"'"+POut.PString(SigText)+"', "
				+"'"+POut.PString(Sound)+"', "
				+"'"+POut.PInt   (ItemOrder)+"')";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command,true);
			SigElementDefNum=dcon.InsertID;
		}

		///<summary>No need to surround with try/catch, because all deletions are allowed.  This routine, deletes references in the SigButDefElement table.  References in the SigElement table are left hanging.  The user interface needs to be able to handle missing elementdefs.</summary>
		public void Delete() {
			string command="DELETE FROM sigbutdefelement WHERE SigElementDefNum="+POut.PInt(SigElementDefNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
			command="DELETE FROM sigelementdef WHERE SigElementDefNum ="+POut.PInt(SigElementDefNum);
			dcon.NonQ(command);
		}

	}

	/*================================================================================================================
	==================================================== class SigElementDefs =============================================*/

	///<summary></summary>
	public class SigElementDefs {
		///<summary>A list of all SigElementDefs.</summary>
		public static SigElementDef[] List;

		///<summary>Gets a list of all SigElementDefs when program first opens.</summary>
		public static void Refresh() {
			string command="SELECT * FROM sigelementdef ORDER BY ItemOrder";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			List=new SigElementDef[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new SigElementDef();
				List[i].SigElementDefNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].LightRow        = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].LightColor      = Color.FromArgb(PIn.PInt(table.Rows[i][2].ToString()));
				List[i].SigElementType  = (SignalElementType)PIn.PInt(table.Rows[i][3].ToString());
				List[i].SigText         = PIn.PString(table.Rows[i][4].ToString());
				List[i].Sound           = PIn.PString(table.Rows[i][5].ToString());
				List[i].ItemOrder       = PIn.PInt   (table.Rows[i][6].ToString());
			}
		}

		///<summary></summary>
		public static SigElementDef[] GetSubList(SignalElementType sigElementType){
			ArrayList AL=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(sigElementType==List[i].SigElementType){
					AL.Add(List[i]);
				}
			}
			SigElementDef[] retVal=new SigElementDef[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		///<summary>Moves the selected item up in the supplied sub list.</summary>
		public static void MoveUp(int selected,SigElementDef[] subList){
			if(selected<0) {
				throw new ApplicationException(Lan.g("SigElementDefs","Please select an item first."));
			}
			if(selected==0) {//already at top
				return;
			}
			if(selected>subList.Length-1){
				throw new ApplicationException(Lan.g("SigElementDefs","Invalid selection."));
			}
			SetOrder(selected-1,subList[selected].ItemOrder,subList);
			SetOrder(selected,subList[selected].ItemOrder-1,subList);
			//Selected-=1;
		}

		///<summary></summary>
		public static void MoveDown(int selected,SigElementDef[] subList) {
			if(selected<0) {
				throw new ApplicationException(Lan.g("SigElementDefs","Please select an item first."));
			}
			if(selected==subList.Length-1){//already at bottom
				return;
			}
			if(selected>subList.Length-1) {
				throw new ApplicationException(Lan.g("SigElementDefs","Invalid selection."));
			}
			SetOrder(selected+1,subList[selected].ItemOrder,subList);
			SetOrder(selected,subList[selected].ItemOrder+1,subList);
			//selected+=1;
		}

		///<summary>Used by MoveUp and MoveDown.</summary>
		private static void SetOrder(int mySelNum,int myItemOrder,SigElementDef[] subList) {
			SigElementDef temp=subList[mySelNum];
			temp.ItemOrder=myItemOrder;
			temp.Update();
		}

		///<summary>Returns the SigElementDef with the specified num.</summary>
		public static SigElementDef GetElement(int SigElementDefNum) {
			for(int i=0;i<List.Length;i++) {
				if(List[i].SigElementDefNum==SigElementDefNum) {
					return List[i].Copy();
				}
			}
			return null;
		}
		
		
	}

		



		
	

	

	


}










