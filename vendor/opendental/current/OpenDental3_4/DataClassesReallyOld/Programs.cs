using System;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using OpenDental.Bridges;

namespace OpenDental{
	
	///<summary>Corresponds to the program table in the database.</summary>
	public struct Program{
		///<summary>Primary key.</summary>
		public int ProgramNum;
		///<summary>Unique name for built-in programs. Not user-editable.</summary>
		public string ProgName;
		///<summary>Description that shows.</summary>
		public string ProgDesc;
		///<summary>True if enabled.</summary>
		public bool Enabled;
		///<summary>The path of the executable to run or file to open.</summary>
		public string Path;
		///<summary>Some programs will accept command line arguments.</summary>
		public string CommandLine;
		///<summary>Notes about this program link. Peculiarities, etc.</summary>
		public string Note;
	}

	/*=========================================================================================
	=================================== class Programs ==========================================*/

	///<summary></summary>
	public class Programs:DataClass{
		///<summary></summary>
		public static Hashtable HList;
		///<summary></summary>
		public static Program Cur;
		///<summary></summary>
		public static Program[] List;

		///<summary></summary>
		public static void Refresh(){
			//MessageBox.Show("refreshing");
			HList=new Hashtable();
			Program tempProgram = new Program();
			cmd.CommandText = 
				"SELECT * from program ORDER BY ProgDesc";
			FillTable();
			List=new Program[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				tempProgram.ProgramNum =PIn.PInt   (table.Rows[i][0].ToString());
				tempProgram.ProgName   =PIn.PString(table.Rows[i][1].ToString());
				tempProgram.ProgDesc   =PIn.PString(table.Rows[i][2].ToString());
				tempProgram.Enabled    =PIn.PBool  (table.Rows[i][3].ToString());
				tempProgram.Path       =PIn.PString(table.Rows[i][4].ToString());
				tempProgram.CommandLine=PIn.PString(table.Rows[i][5].ToString());
				tempProgram.Note       =PIn.PString(table.Rows[i][6].ToString());
				List[i]=tempProgram;
				if(!HList.ContainsKey(tempProgram.ProgName)){
					HList.Add(tempProgram.ProgName,tempProgram);
				}
			}
			//MessageBox.Show(HList.Count.ToString());
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE program SET "
				+"ProgName = '"     +POut.PString(Cur.ProgName)+"'"
				+",ProgDesc  = '"   +POut.PString(Cur.ProgDesc)+"'"
				+",Enabled  = '"    +POut.PBool  (Cur.Enabled)+"'"
				+",Path = '"        +POut.PString(Cur.Path)+"'"
				+",CommandLine  = '"+POut.PString(Cur.CommandLine)+"'"
				+",Note  = '"       +POut.PString(Cur.Note)+"'"
				+" WHERE programnum = '"+POut.PInt(Cur.ProgramNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
				+") VALUES("
				+"'"+POut.PString(Cur.ProgName)+"', "
				+"'"+POut.PString(Cur.ProgDesc)+"', "
				+"'"+POut.PBool  (Cur.Enabled)+"', "
				+"'"+POut.PString(Cur.Path)+"', "
				+"'"+POut.PString(Cur.CommandLine)+"', "
				+"'"+POut.PString(Cur.Note)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.ProgramNum=InsertID;
		}

		///<summary>This can only be called by the user if it is a program link that they created. Included program links cannot be deleted.  If calling this from ClassConversion, must delete any dependent ProgramProperties first.  It will delete ToolButItems for you.</summary>
		public static void DeleteCur(){
			ToolButItems.DeleteAllForProgram();
			cmd.CommandText = "DELETE from program WHERE programnum = '"+Cur.ProgramNum.ToString()+"'";
			NonQ();
			
		}

		///<summary>Returns true if a Program link with the given name or number exists and is enabled.</summary>
		public static bool IsEnabled(string progName){
			if(HList.ContainsKey(progName) && ((Program)HList[progName]).Enabled){
				return true;
			}
			return false;
		}

		///<summary></summary>
		public static bool IsEnabled(int programNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].ProgramNum==programNum && List[i].Enabled){
					return true;
				}
			}
			return false;
		}

		///<summary>Typically used when user clicks a button to a Program link.  This method attempts to identify and execute the program based on the given programNum.</summary>
		public static void Execute(int programNum,Patient pat){
			Cur.ProgramNum=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].ProgramNum==programNum){
					Cur=List[i];
				}
			}
			if(Cur.ProgramNum==0){//no match was found
				MessageBox.Show("Error, program entry not found in database.");
				return;
			}
			if(Cur.ProgName=="TigerView"){
				TigerView.SendData(pat);
				return;
			}
			else if(Cur.ProgName=="Apteryx"){
				Apteryx.SendData(pat);
				return;
			}
			else if(Cur.ProgName=="Schick"){
				Schick.SendData(pat);
				return;
			}
			else if(Cur.ProgName=="Dexis"){
				Dexis.SendData(pat);
				return;
			}
			else if(Cur.ProgName=="VixWin"){
				VixWin.SendData(pat);
				return;
			}
			else if(Cur.ProgName=="Trophy"){
				Trophy.SendData(pat);
				return;
			}
			else if(Cur.ProgName=="Sirona"){
				Sirona.SendData(pat);
				return;
			}
			else if(Cur.ProgName=="HouseCalls"){
				FormHouseCalls FormHC=new FormHouseCalls();
				FormHC.ShowDialog();
				return;
			}
			else if(Cur.ProgName=="Planmeca"){
				Planmeca.SendData(pat);
				return;
			}
			//all remaining programs:
			try{
				Process.Start(Cur.Path,Cur.CommandLine);
			}
			catch{
				MessageBox.Show(Cur.ProgDesc+" is not available.");
				return;
			}
		}




	}

	

	


}










