using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental {

	/// <summary>This defines the light buttons on the left of the main screen.</summary>
	public class SigButDef{
		///<summary>Primary key.</summary>
		public int SigButDefNum;
		///<summary>The text on the button</summary>
		public string ButtonText;
		///<summary>0-based index defines the order of the buttons.</summary>
		public int ButtonIndex;
		///<summary>0=none, or 1-9. The cell in the 3x3 tic-tac-toe main program icon that is to be synched with this button.  It will light up or clear whenever this button lights or clears.</summary>
		public int SynchIcon;
		///<summary>Blank for the default buttons.  Or contains the computer name for the buttons that override the defaults.</summary>
		public string ComputerName;
		///<summary>Not a database field.  The sounds and lights attached to the button.</summary>
		public SigButDefElement[] ElementList;

		///<summary></summary>
		public SigButDef Copy() {
			SigButDef s=new SigButDef();
			s.SigButDefNum=SigButDefNum;
			s.ButtonText=ButtonText;
			s.ButtonIndex=ButtonIndex;
			s.SynchIcon=SynchIcon;
			s.ComputerName=ComputerName;
			s.ElementList=new SigButDefElement[ElementList.Length];
			ElementList.CopyTo(s.ElementList,0);
			return s;
		}

		///<summary></summary>
		public void Update() {
			string command="UPDATE sigbutdef SET " 
				+"ButtonText = '"   +POut.PString(ButtonText)+"'"
				+",ButtonIndex = '" +POut.PInt   (ButtonIndex)+"'"
				+",SynchIcon = '"   +POut.PInt   (SynchIcon)+"'"
				+",ComputerName = '"+POut.PString(ComputerName)+"'"
				+" WHERE SigButDefNum  ='"+POut.PInt   (SigButDefNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Insert() {
			string command="INSERT INTO sigbutdef (ButtonText,ButtonIndex,SynchIcon,ComputerName"
				+") VALUES("
				+"'"+POut.PString(ButtonText)+"', "
				+"'"+POut.PInt   (ButtonIndex)+"', "
				+"'"+POut.PInt   (SynchIcon)+"', "
				+"'"+POut.PString(ComputerName)+"')";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command,true);
			SigButDefNum=dcon.InsertID;
		}

		///<summary>No need to surround with try/catch, because all deletions are allowed.</summary>
		public void Delete() {
			string command="DELETE FROM sigbutdefelement WHERE SigButDefNum="+POut.PInt(SigButDefNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
			command="DELETE FROM sigbutdef WHERE SigButDefNum ="+POut.PInt(SigButDefNum);
			dcon.NonQ(command);
		}

		///<summary>Used in the Button edit dialog.</summary>
		public void DeleteElements() {
			string command="DELETE FROM sigbutdefelement WHERE SigButDefNum="+POut.PInt(SigButDefNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary>Loops through the element list and pulls out one element of a specific type. Used in the button edit window.</summary>
		public SigButDefElement GetElement(SignalElementType elementType) {
			for(int i=0;i<ElementList.Length;i++) {
				if(SigElementDefs.GetElement(ElementList[i].SigElementDefNum).SigElementType==elementType){
					return ElementList[i].Copy();
				}
			}
			return null;
		}

	}

	/*================================================================================================================
	==================================================== class SigButDefs =============================================*/

	///<summary></summary>
	public class SigButDefs {
		///<summary>A list of all SigButDefs.</summary>
		public static SigButDef[] List;

		///<summary>Gets a list of all SigButDefs when program first opens.  Also refreshes SigButDefElements and attaches all elements to the appropriate buttons.</summary>
		public static void Refresh() {
			SigButDefElements.Refresh();
			string command="SELECT * FROM sigbutdef ORDER BY ButtonIndex";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			List=new SigButDef[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new SigButDef();
				List[i].SigButDefNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ButtonText  = PIn.PString(table.Rows[i][1].ToString());
				List[i].ButtonIndex = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].SynchIcon   = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].ComputerName= PIn.PString(table.Rows[i][4].ToString());
				List[i].ElementList=SigButDefElements.GetForButton(List[i].SigButDefNum);
			}
		}

		///<summary>Used in Setup.  The returned list also includes defaults if not overridden by one with a computername.  The supplied computer name can be blank for the default setup.</summary>
		public static SigButDef[] GetByComputer(string computerName){
			//first, get a default list, because we will always need that
			ArrayList AL=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ComputerName==""){
					AL.Add(List[i]);
				}
			}
			SigButDef[] defaultList=new SigButDef[AL.Count];
			AL.CopyTo(defaultList);
			if(computerName==""){//if all we are interested in is the default list, then done.
				return defaultList;
			}
			//for any other computer:
			AL=new ArrayList();
			//SigButDef defaultBut;
			for(int i=0;i<List.Length;i++){
				if(computerName==List[i].ComputerName){
					//defaultBut=GetByIndex(List[i].ButtonIndex,defaultList);
					//if(defaultBut==null){//no default
						AL.Add(List[i]);
					//}
					//else{
					//	AL.Add(defaultBut);
					//}
				}
			}
			SigButDef[] retVal=new SigButDef[AL.Count];
			AL.CopyTo(retVal);
			//but we are still missing some defaults
			SigButDef matchingBut;
			for(int i=0;i<defaultList.Length;i++){
				matchingBut=GetByIndex(defaultList[i].ButtonIndex,retVal);
				if(matchingBut==null){
					AL.Add(defaultList[i]);
				}
			}
			retVal=new SigButDef[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		///<summary>Moves the selected item up in the supplied sub list.</summary>
		public static void MoveUp(SigButDef selected,SigButDef[] subList){
			if(selected.ButtonIndex==0) {//already at top
				return;
			}
			SigButDef occupied=null;
			for(int i=0;i<subList.Length;i++){
				if(subList[i].SigButDefNum!=selected.SigButDefNum//if not the selected object
					&& subList[i].ButtonIndex==selected.ButtonIndex-1)//and position occupied
				{
					occupied=subList[i].Copy();
				}
			}
			if(occupied!=null){
				occupied.ButtonIndex++;
				occupied.Update();
			}
			selected.ButtonIndex--;
			selected.Update();
		}

		///<summary></summary>
		public static void MoveDown(SigButDef selected,SigButDef[] subList) {
			if(selected.ButtonIndex==20) {
				throw new ApplicationException(Lan.g("SigButDefs","Max 20 buttons."));
			}
			SigButDef occupied=null;
			for(int i=0;i<subList.Length;i++) {
				if(subList[i].SigButDefNum!=selected.SigButDefNum//if not the selected object
					&& subList[i].ButtonIndex==selected.ButtonIndex+1)//and position occupied
				{
					occupied=subList[i].Copy();
				}
			}
			if(occupied!=null) {
				occupied.ButtonIndex--;
				occupied.Update();
			}
			selected.ButtonIndex++;
			selected.Update();
		}

		///<summary>Returns the SigButDef with the specified buttonIndex.  Used from the setup page.  The supplied list will already have been filtered by computername.  Supply buttonIndex in 0-based format.</summary>
		public static SigButDef GetByIndex(int buttonIndex,SigButDef[] subList) {
			for(int i=0;i<subList.Length;i++) {
				if(subList[i].ButtonIndex==buttonIndex) {
					return subList[i].Copy();
				}
			}
			return null;
		}
		
		
	}

		



		
	

	

	


}










