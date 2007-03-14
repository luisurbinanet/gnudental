using System;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using OpenDental.Bridges;

namespace OpenDental{
	
	///<summary>Each row is a bridge to an outside program, frequently an imaging program.  Most of the bridges are hard coded, and simply need to be enabled.  But user can also add their own custom bridge.</summary>
	public struct Program{
		///<summary>Primary key.</summary>
		public int ProgramNum;
		///<summary>Unique name for built-in program bridges. Not user-editable.</summary>
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

		///<summary>Supply a valid program Name, and this will set Cur to be the corresponding Program object.</summary>
		public static void GetCur(string progName){
			for(int i=0;i<List.Length;i++){
				if(List[i].ProgName==progName){
					Cur=List[i];
					return;
				}
			}
			Cur.ProgramNum=0;//to signify that the program could not be located. (user deleted it in an older version)
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
			else if(Cur.ProgName=="VixWinOld") {
				VixWinOld.SendData(pat);
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
			else if(Cur.ProgName=="TrophyEnhanced") {
				TrophyEnhanced.SendData(pat);
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
			else if(Cur.ProgName=="ImageFX"){
				ImageFX.SendData(pat);
				return;
			}
			else if(Cur.ProgName=="DentForms"){
				DentForms.SendData(pat);
				return;
			}
			else if(Cur.ProgName=="DBSWin"){
				DBSWin.SendData(pat);
				return;
			}
			else if(Cur.ProgName=="DentX"){
				DentX.SendData(pat);
				return;
			}
			else if(Cur.ProgName=="Lightyear"){
				Lightyear.SendData(pat);
				return;
			}
			else if(Cur.ProgName=="DentalEye") {
				DentalEye.SendData(pat);
				return;
			}
			else if(Cur.ProgName=="FloridaProbe") {
				FloridaProbe.SendData(pat);
				return;
			}
			else if(Cur.ProgName=="DrCeph") {
				DrCeph.SendData(pat);
				return;
			}
			else if(Cur.ProgName=="MediaDent") {
				MediaDent.SendData(pat);
				return;
			}
			else if(Cur.ProgName=="PerioPal") {
				PerioPal.SendData(pat);
				return;
			}
			else if(Cur.ProgName=="NewPatientForm.com") {
				NewPatientForm npf=new NewPatientForm();
				npf.ShowDownload(Cur.Path);//NewPatientForm.com
				return;
			}
			//all remaining programs:
			try{
				string cmdline=Cur.CommandLine;
				cmdline=cmdline.Replace("[PatNum]",pat.PatNum.ToString());
				cmdline=cmdline.Replace("[ChartNumber]",pat.ChartNumber);
				string path=Cur.Path;
				path=path.Replace("[PatNum]",pat.PatNum.ToString());
				path=path.Replace("[ChartNumber]",pat.ChartNumber);
				Process.Start(path,cmdline);
			}
			catch{
				MessageBox.Show(Cur.ProgDesc+" is not available.");
				return;
			}
		}




	}

	

	


}










