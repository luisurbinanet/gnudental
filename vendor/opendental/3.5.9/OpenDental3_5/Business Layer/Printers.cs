using System;
using System.Collections;
using System.Data;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the printer table in the database.</summary>
	public class Printer{
		///<summary>Primary key.</summary>
		public int PrinterNum;
		///<summary>Foreign key to computer.ComputerNum.</summary>
		public int ComputerNum;
		///<summary>One of about 10 different situations where printing takes place.  If no printer object exists for a situation, then a default is used and a prompt is displayed.</summary>
		public PrintSituation PrintSit;
		///<summary>The name of the printer as set from the specified computer.</summary>
		public string PrinterName;
		///<summary>If true, then user will be prompted for printer.  Otherwise, print directly with little user interaction.</summary>
		public bool DisplayPrompt;

		/*//<summary>Returns a copy of the clearinghouse.</summary>
    public ClaimForm Copy(){
			ClaimForm cf=new ClaimForm();
			cf.ClaimFormNum=ClaimFormNum;
			cf.Description=Description;
			return cf;
		}*/

		
	}

/*=========================================================================================
		=================================== class Printers=======================================*/

	///<summary>Handles all the business logic for printers.  Used heavily by the UI.  Every single function that makes changes to the database must be completely autonomous and do ALL validation itself.</summary>
	public class Printers{
		///<summary>List of all printers.</summary>
		private static Printer[] List;
		
		///<summary></summary>
		public static void Refresh(){
			string command="SELECT * FROM printer";
			List=RefreshAndFill(command);
		}

		///<summary></summary>
		public static Printer GetOnePrinter(PrintSituation sit,int compNum){
			string command="SELECT * FROM printer WHERE "
				+"PrintSit = '"      +POut.PInt((int)sit)+"' "
				+"AND ComputerNum ='"+POut.PInt(compNum)+"'";
			Printer[] printerList=RefreshAndFill(command);
			if(printerList.Length==0){
				return null;
			}
			return printerList[0];
		}

		private static Printer[] RefreshAndFill(string command){
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			Printer[] pList=new Printer[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				pList[i]=new Printer();
				pList[i].PrinterNum    = PIn.PInt   (table.Rows[i][0].ToString());
				pList[i].ComputerNum   = PIn.PInt   (table.Rows[i][1].ToString());
				pList[i].PrintSit      =(PrintSituation)PIn.PInt(table.Rows[i][2].ToString());
				pList[i].PrinterName   = PIn.PString(table.Rows[i][3].ToString());
				pList[i].DisplayPrompt = PIn.PBool  (table.Rows[i][4].ToString());
			}
			return pList;
		}

		///<summary></summary>
		private static void Insert(Printer cur){
			string command="INSERT INTO printer (ComputerNum,PrintSit,PrinterName,"
				+"DisplayPrompt) VALUES("
				+"'"+POut.PInt   (cur.ComputerNum)+"', "
				+"'"+POut.PInt   ((int)cur.PrintSit)+"', "
				+"'"+POut.PString(cur.PrinterName)+"', "
				+"'"+POut.PBool  (cur.DisplayPrompt)+"')";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			cur.PrinterNum=dcon.InsertID;
		}

		///<summary></summary>
		private static void Update(Printer cur){
			string command="UPDATE printer SET "
				+"ComputerNum = '"   +POut.PInt   (cur.ComputerNum)+"' "
				+",PrintSit = '"     +POut.PInt   ((int)cur.PrintSit)+"' "
				+",PrinterName = '"  +POut.PString(cur.PrinterName)+"' "
				+",DisplayPrompt = '"+POut.PBool  (cur.DisplayPrompt)+"' "
				+"WHERE PrinterNum = '"+POut.PInt(cur.PrinterNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		private static void Delete(Printer cur){
			string command="DELETE FROM printer "
				+"WHERE PrinterNum = "+POut.PInt(cur.PrinterNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary>Called from many places in the program.  Every single time we print, this function is used to figure out which printer to use.  It also handles displaying the dialog if necessary.  Tests to see if the selected printer is valid, and if not, then it gives user the option to print to an available printer.</summary>
		public static bool SetPrinter(PrintDocument pd,PrintSituation sit){
			PrinterSettings pSet=pd.PrinterSettings;
			//pSet will always be new when this function is called
			//0.5. Get the name of the Windows default printer.
			//This method only works when the pSet is still new.
			//string winDefault=pSet.PrinterName;
			//1. If a default printer is set in OD,
			//and it is in the list of installed printers, use it.
			bool prompt=true;
			Printer printerForSit=GetForSit(PrintSituation.Default);//warning: this changes
			string printerName="";
			if(printerForSit!=null){
				printerName=printerForSit.PrinterName;
				prompt=printerForSit.DisplayPrompt;
				if(PrinterIsInstalled(printerName)){
					pSet.PrinterName=printerName;
				}
			}
			//2. If a printer is set for this situation,
			//and it is in the list of installed printers, use it.
			if(sit!=PrintSituation.Default){
				printerForSit=GetForSit(sit);
				printerName="";
				if(printerForSit!=null){
					printerName=printerForSit.PrinterName;
					prompt=printerForSit.DisplayPrompt;
					if(PrinterIsInstalled(printerName)){
						pSet.PrinterName=printerName;
					}
				}
			}
			//4. Present the dialog
			if(!prompt){
				return true;
			}
			PrintDialog dialog=new PrintDialog();
			dialog.PrinterSettings=pSet;
			if(dialog.ShowDialog()!=DialogResult.OK){
				return false;
			}
			//if(!dialog.PrinterSettings.IsValid){//not needed since we have already checked each name.
				//pd2.PrinterSettings=printDialog2.PrinterSettings;
			//}
			return true;
		}

		private static bool PrinterIsInstalled(string name){
			for(int i=0;i<PrinterSettings.InstalledPrinters.Count;i++){
				if(PrinterSettings.InstalledPrinters[i]==name){
					return true;
				}
			}
			return false;
		}

		///<summary>Gets the set printer whether or not it is valid.</summary>
		public static Printer GetForSit(PrintSituation sit){
			Computer compCur=Computers.GetCur();
			for(int i=0;i<List.Length;i++){
				if(List[i].ComputerNum==compCur.ComputerNum
					&& List[i].PrintSit==sit)
				{
					return List[i];
				}
			}
			return null;
		}

		///<summary>Either does an insert or an update to the database if need to create a Printer object.  Or it also deletes a printer object if needed.</summary>
		public static void PutForSit(PrintSituation sit,string computerName, string printerName
			,bool displayPrompt)
		{
			//Computer[] compList=Computers.Refresh();
			//Computer compCur=Computers.GetCur();
			string command="SELECT ComputerNum FROM computer "
				+"WHERE CompName = '"+POut.PString(computerName)+"'";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count==0){
				return;//computer not yet entered in db.
			}
			int compNum=PIn.PInt(table.Rows[0][0].ToString());
			Printer existing=GetOnePrinter(sit,compNum);   //GetForSit(sit);
			if(printerName=="" && !displayPrompt){//then should not be an entry in db
				if(existing!=null){//need to delete Printer
					Delete(existing);
				}
			}
			else if(existing==null){
				Printer cur=new Printer();
				cur.ComputerNum=compNum;
				cur.PrintSit=sit;
				cur.PrinterName=printerName;
				cur.DisplayPrompt=displayPrompt;
				Insert(cur);
			}
			else{
				existing.PrinterName=printerName;
				existing.DisplayPrompt=displayPrompt;
				Update(existing);
			}
		}

		///<summary>Called from FormPrinterSetup if user selects the easy option.  Since the other options will be hidden, we have to clear them.  User should be sternly warned before this happens.</summary>
		public static void ClearAll(){
			//first, delete all entries
			string command="DELETE FROM printer";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
			//then, add one printer for each computer. Default and show prompt
			Computers.Refresh();
			Printer cur;
			for(int i=0;i<Computers.List.Length;i++){
				cur=new Printer();
				cur.ComputerNum=Computers.List[i].ComputerNum;
				cur.PrintSit=PrintSituation.Default;
				cur.PrinterName="";
				cur.DisplayPrompt=true;
				Insert(cur);
			}
		}
		


	}

	



}









