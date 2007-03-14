using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Keeps track of the computers in an office.  The list will eventually become cluttered with the names of old computers that are no longer in service.  The old rows can be safely deleted.  Although the primary key is used in at least one table, this will probably be changed, and the computername will become the primary key.</summary>
	public class Computer{//
		///<summary>Primary key.</summary>
		public int ComputerNum;
		///<summary>Name of the computer.</summary>
		public string CompName;
		///<summary>No longer used. Moved to printer table.  Used to hold default printer for each computer</summary>
		public string PrinterName;
		//<summary>Not a database table.  Filled from printer table when refreshing computer List.</summary>
		//public Printer[] PrinterList;

			///<summary>ONLY use this if compname is not already present</summary>
		public void Insert(){
			if(Prefs.RandomKeys){
				ComputerNum=MiscData.GetKey("computer","ComputerNum");
			}
			string command= "INSERT INTO computer (";
			if(Prefs.RandomKeys){
				command+="ComputerNum,";
			}
			command+="CompName"
				+") VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(ComputerNum)+"', ";
			}
			command+=
				"'"+POut.PString(CompName)+"')";
				//+"'"+POut.PString(PrinterName)+"')";
			DataConnection dcon=new DataConnection();
			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				ComputerNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void Update(){
			string command= "UPDATE computer SET "
				+"compname = '"    +POut.PString(CompName)+"' "
				//+"printername = '" +POut.PString(PrinterName)+"' "
				+"WHERE ComputerNum = '"+POut.PInt(ComputerNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Delete(){
			string command= "DELETE FROM computer WHERE computernum = '"+ComputerNum.ToString()+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		//public static string GetCurPrinter(
	}

	/*=========================================================================================
	=================================== class Computers ==========================================*/

	///<summary></summary>
	public class Computers{
		///<summary>A list of all computers that have logged into the database in the past.  Might be some extra computer names in the list unless user has cleaned it up.</summary>
		public static Computer[] List;

		///<summary></summary>
		public static void Refresh(){
			//first, make sure this computer is in the db:
			string command=
				"SELECT * from computer "
				+"WHERE compname = '"+SystemInformation.ComputerName+"'";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count==0){
				Computer Cur=new Computer();
				Cur.CompName=SystemInformation.ComputerName;
				Cur.Insert();
			}
			//then, refresh List:
			List=GetList();
		}

		///<summary></summary>
		public static Computer[] GetList(){
			string command="SELECT * FROM computer ORDER BY CompName";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			Computer[] list=new Computer[table.Rows.Count];
			//HList=new Hashtable();
			for(int i=0;i<list.Length;i++){
				list[i]=new Computer();
				list[i].ComputerNum = PIn.PInt   (table.Rows[i][0].ToString());
				list[i].CompName    = PIn.PString(table.Rows[i][1].ToString());
				//List[i].PrinterName = PIn.PString(table.Rows[i][2].ToString());
				//List[i].PrinterList=Printers.GetForComputer(List[i].ComputerNum);
				//if(SystemInformation.ComputerName==List[i].CompName){
				//	Cur=List[i];
				//}
				//HList.Add(List[i].ComputerNum,List[i]);
			}
			return list;
		}

		///<summary>Only called from Printers.GetForSit</summary>
		public static Computer GetCur(){
			for(int i=0;i<List.Length;i++){
				if(SystemInformation.ComputerName==List[i].CompName){
					return List[i];
				}
			}
			return null;//this will never happen
		}
   

	

	}

	

	



}









