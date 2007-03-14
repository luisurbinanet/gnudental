using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental {

	/// <summary>Corresponds to the TerminalActive table in the database.  Each row is one computer that currently acting as a terminal for new patient info input.</summary>
	public class TerminalActive{
		///<summary>Primary key.</summary>
		public int TerminalActiveNum;
		///<summary>The name of the computer where the terminal is active.</summary>
		public string ComputerName;
		///<summary>Indicates at what point the patient is in the sequence. 0=standby, 1=PatientInfo, 2=Medical, 4=UpdateOnly.  If status is 1, then nobody else on the network can open the patient edit window for that patient.</summary>
		public TerminalStatusEnum TerminalStatus;
		///<summary>fk to patient.PatNum.  The patient currently showing in the terminal.  0 if terminal is in standby mode.</summary>
		public int PatNum;

		///<summary></summary>
		public TerminalActive Copy() {
			TerminalActive t=new TerminalActive();
			t.TerminalActiveNum=TerminalActiveNum;
			t.ComputerName=ComputerName;
			t.TerminalStatus=TerminalStatus;
			t.PatNum=PatNum;
			return t;
		}

		///<summary></summary>
		public void Update() {
			string command="UPDATE terminalactive SET " 
				+"ComputerName = '"   +POut.PString(ComputerName)+"'"
				+",TerminalStatus = '"+POut.PInt   ((int)TerminalStatus)+"'"
				+",PatNum = '"        +POut.PInt   (PatNum)+"'"
				+" WHERE TerminalActiveNum  ='"+POut.PInt   (TerminalActiveNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Insert() {
			if(Prefs.RandomKeys) {
				TerminalActiveNum=MiscData.GetKey("terminalactive","TerminalActiveNum");
			}
			string command="INSERT INTO terminalactive (";
			if(Prefs.RandomKeys) {
				command+="TerminalActiveNum,";
			}
			command+="ComputerName,TerminalStatus,PatNum) VALUES(";
			if(Prefs.RandomKeys) {
				command+="'"+POut.PInt(TerminalActiveNum)+"', ";
			}
			command+=
				 "'"+POut.PString(ComputerName)+"', "
				+"'"+POut.PInt   ((int)TerminalStatus)+"', "
				+"'"+POut.PInt   (PatNum)+"')";
			DataConnection dcon=new DataConnection();
			if(Prefs.RandomKeys) {
				dcon.NonQ(command);
			}
			else {
				dcon.NonQ(command,true);
				TerminalActiveNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void Delete() {
			string command="DELETE FROM terminalactive WHERE TerminalActiveNum ="+POut.PInt(TerminalActiveNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

	}

	/*================================================================================================================
	==================================================== class TerminalActives =============================================*/

	///<summary></summary>
	public class TerminalActives {

		///<summary>Gets a list of all TerminalActives.  Used by the terminal monitor window and by the terminal window itself.  Data is retrieved at regular short intervals, so functions as a messaging system.</summary>
		public static TerminalActive[] Refresh() {
			string command="SELECT * FROM terminalactive ORDER BY ComputerName";
			return RefreshAndFill(command);
		}

		///<summary></summary>
		public static TerminalActive GetTerminal(string computerName){
			string command="SELECT * FROM terminalactive WHERE ComputerName ='"+POut.PString(computerName)+"'";
			TerminalActive[] List=RefreshAndFill(command);
			if(List.Length>0){
				return List[0];
			}
			return null;
		}

		///<summary>Gets a list of all TerminalActives.  Used by the terminal monitor window and by the terminal window itself.  Data is retrieved at regular short intervals, so functions as a messaging system.</summary>
		private static TerminalActive[] RefreshAndFill(string command) {
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			TerminalActive[] List=new TerminalActive[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new TerminalActive();
				List[i].TerminalActiveNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ComputerName     = PIn.PString(table.Rows[i][1].ToString());
				List[i].TerminalStatus   = (TerminalStatusEnum)PIn.PInt   (table.Rows[i][2].ToString());
				List[i].PatNum           = PIn.PInt   (table.Rows[i][3].ToString());
			}
			return List;
		}

		///<summary>Run when starting up a terminal window to delete any previous entries for this computer that didn't get deleted properly.</summary>
		public static void DeleteAllForComputer(string computerName){
			string command="DELETE FROM terminalactive WHERE ComputerName ='"+POut.PString(computerName)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary>Called whenever user wants to edit patient info.  Not allowed to if patient edit window is open at a terminal.  Once patient is done at terminal, then staff allowed back into patient edit window.</summary>
		public static bool PatIsInUse(int patNum){
			string command="SELECT COUNT(*) FROM terminalactive WHERE PatNum="+POut.PInt(patNum)
				+" AND (TerminalStatus="+POut.PInt((int)TerminalStatusEnum.PatientInfo)
				+" OR TerminalStatus="+POut.PInt((int)TerminalStatusEnum.UpdateOnly)+")";
			DataConnection dcon=new DataConnection();
			if(dcon.GetCount(command)=="0"){
				return false;
			}
			return true;
		}
	
		
		
	}

		



		
	

	

	


}










