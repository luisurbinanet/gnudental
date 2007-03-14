using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the preference table in the database.  Stores small bits of data for a wide variety of purposes.</summary>
	public struct Pref{
		///<summary>Primary key.</summary>
		public string PrefName;//
		///<summary>The stored value.</summary>
		public string ValueString;
	}

	/*=========================================================================================
	=================================== class Prefs ==========================================*/

	///<summary></summary>
	public class Prefs:DataClass{
		///<summary></summary>
		public static Hashtable HList;
		///<summary></summary>
		public static Pref Cur;
		//private string DataBaseVersion=Application.ProductVersion;//was "1.0.0";

		///<summary>This ONLY runs when first opening the program.  Gets run early in the sequence. Returns false if the program should exit.</summary>
		public static bool CheckMySqlVersion(){
			string command="SELECT @@version";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			string thisVersion=PIn.PString(table.Rows[0][0].ToString());
			ExitApplicationNow ExitApplicationNow2=new ExitApplicationNow();
			//if(thisVersion.Substring(0,3)=="4.0"){
				//do nothing
			//}
			if(thisVersion.Substring(0,3)=="4.1"
				|| thisVersion.Substring(0,3)=="5.0")
			{
				if(HList.ContainsKey("DatabaseConvertedForMySql41"))
					//&& GetBool("DatabaseConvertedForMySql41"))
				{
					return true;//already converted
				}
				if(!MsgBox.Show("Prefs",true,"Your database will now be converted for use with MySQL 4.1.")){
					ExitApplicationNow2.ExitNow();
					return false;
				}
				ClassConvertDatabase CCD=new ClassConvertDatabase();
				#if !DEBUG
					try{
						CCD.MakeABackup();
					}
					catch(Exception e){
						if(e.Message!=""){
							MessageBox.Show(e.Message);
						}
						MsgBox.Show("Prefs","Backup failed. Your database has not been altered.");
						ExitApplicationNow2.ExitNow();
						return false;//but this should never happen
					}
				#endif
				MessageBox.Show("Backup performed");
				command="SHOW TABLES";
				table=dcon.GetTable(command);
				string[] tableNames=new string[table.Rows.Count];
				for(int i=0;i<table.Rows.Count;i++){
					tableNames[i]=table.Rows[i][0].ToString();
				}
				for(int i=0;i<tableNames.Length;i++){
					if(tableNames[i]!="procedurecode"){
						command="ALTER TABLE "+tableNames[i]+" CONVERT TO CHARACTER SET utf8";
						dcon.NonQ(command);
					}
				}
				string[] commands=new string[]
				{
					"ALTER TABLE procedurecode CHANGE ADACode ADACode varchar(15) character set utf8 collate utf8_bin NOT NULL"
					,"ALTER TABLE procedurecode DEFAULT character set utf8"
					,"ALTER TABLE procedurecode MODIFY Descript varchar(255) character set utf8 NOT NULL"
					,"ALTER TABLE procedurecode MODIFY AbbrDesc varchar(50) character set utf8 NOT NULL"
					,"ALTER TABLE procedurecode MODIFY ProcTime varchar(24) character set utf8 NOT NULL"
					,"ALTER TABLE procedurecode MODIFY DefaultNote text character set utf8 NOT NULL"
					,"ALTER TABLE procedurecode MODIFY AlternateCode1 varchar(15) character set utf8 NOT NULL"
					,"ALTER TABLE procedurelog MODIFY ADACode varchar(15) character set utf8 collate utf8_bin NOT NULL"
					,"ALTER TABLE autocodeitem MODIFY ADACode varchar(15) character set utf8 collate utf8_bin NOT NULL"
					,"ALTER TABLE procbuttonitem MODIFY ADACode varchar(15) character set utf8 collate utf8_bin NOT NULL"
					,"ALTER TABLE covspan MODIFY FromCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
					,"ALTER TABLE covspan MODIFY ToCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
				};
				dcon.NonQ(commands);
				//and set the default too
				command="ALTER DATABASE CHARACTER SET utf8";
				dcon.NonQ(command);
				command="INSERT INTO preference VALUES('DatabaseConvertedForMySql41','1')";
				dcon.NonQ(command);
				MessageBox.Show("converted");
				//Refresh();
			}
			else{
				MessageBox.Show(Lan.g("Prefs","Your version of MySQL won't work with this program")+": "+thisVersion
					+".  "+Lan.g("Prefs","You should upgrade to MySQL 4.1"));
				//but let them through anyway
					//,"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
				//{
					//ExitApplicationNow2.ExitNow();
					//return false;
				//}
			}
			return true;
		}

		///<summary>This ONLY runs when first opening the program</summary>
		public static bool ConvertDB(){
			ExitApplicationNow ExitApplicationNow2=new ExitApplicationNow();
			ClassConvertDatabase ClassConvertDatabase2=new ClassConvertDatabase();
			if(ClassConvertDatabase2.Convert(((Pref)HList["DataBaseVersion"]).ValueString)){
				return true;
			}
			else{
				//MessageBox.Show("Conversion unsuccessful");
				ExitApplicationNow2.ExitNow();
				return false;
			}
		}

		///<summary>Only called once from RefreshLocalData.</summary>
		public static bool CheckProgramVersion(){
			ExitApplicationNow ExitApplicationNow2=new ExitApplicationNow();
			Version storedVersion=new Version(GetString("ProgramVersion"));
			Version currentVersion=new Version(Application.ProductVersion);
			if(storedVersion<currentVersion){
				UpdateString("ProgramVersion",currentVersion.ToString());
				Prefs.Refresh();
			}
			if(storedVersion>currentVersion){
				if(File.Exists(GetString("DocPath")+"Setup.exe")){
					if(MessageBox.Show("A newer version has been installed on at least one computer.  The setup program will now be launched.","",MessageBoxButtons.OKCancel)
						==DialogResult.Cancel)
					{
						if(MessageBox.Show("Download again?","",MessageBoxButtons.OKCancel)
							==DialogResult.OK)
						{
							FormUpdate FormU=new FormUpdate();
							FormU.ShowDialog();
						}
						ExitApplicationNow2.ExitNow();
						return false;
					}
					try{
						Process.Start(GetString("DocPath")+"Setup.exe");
					}
					catch{
						MessageBox.Show("Could not launch Setup.exe");
					}
				}
				else if(MessageBox.Show("A newer version has been installed on at least one computer, but Setup.exe could not be found in "+GetString("DocPath")+".  Download again?","",MessageBoxButtons.OKCancel)==DialogResult.OK)
				{
					FormUpdate FormU=new FormUpdate();
					FormU.ShowDialog();
				}
				ExitApplicationNow2.ExitNow();//always exits, whether launch of setup worked or no
				return false;
			}
			return true;
		}

		///<summary></summary>
		public static void Refresh(){
			HList=new Hashtable();
			Pref tempPref = new Pref();
			cmd.CommandText = 
				"SELECT * from preference";
			FillTable();
			for(int i=0;i<table.Rows.Count;i++){
				tempPref.PrefName=PIn.PString(table.Rows[i][0].ToString());
				tempPref.ValueString=PIn.PString(table.Rows[i][1].ToString());
				HList.Add(tempPref.PrefName,tempPref);
			}
		}

		///<summary>Gets a pref of type int.</summary>
		public static int GetInt(string prefName){
			if(!HList.ContainsKey(prefName)){
				MessageBox.Show(prefName+" is an invalid pref name.");
				return 0;
			}
			return PIn.PInt(((Pref)HList[prefName]).ValueString);
		}

		///<summary>Gets a pref of type bool.</summary>
		public static bool GetBool(string prefName){
			if(!HList.ContainsKey(prefName)){
				MessageBox.Show(prefName+" is an invalid pref name.");
				return false;
			}
			return PIn.PBool(((Pref)HList[prefName]).ValueString);
		}

		///<summary>Gets a pref of type string.</summary>
		public static string GetString(string prefName){
			if(!HList.ContainsKey(prefName)){
				MessageBox.Show(prefName+" is an invalid pref name.");
				return "";
			}
			return ((Pref)HList[prefName]).ValueString;
		}

		///<summary></summary>
		public static bool TryToConnect(){
			try{
				con.Open();
				cmd.CommandText="update preference set valuestring = '0' where valuestring = '0'";
				int rowsUpdated = cmd.ExecuteNonQuery();
				con.Close();

			}
			catch{//(MySQLDriverCS.MySQLException ex){
				return false; 
			}
			return true;
		}

		///<summary></summary>
		public static bool DBExists(){
			try{
				con.Open();
				//cmd.CommandText="update preference set valuestring = '0' where valuestring = '0'";
				//int rowsUpdated = cmd.ExecuteNonQuery();
				con.Close();

			}
			catch{//(MySql.Data.MySqlClient.MySqlException ex){
				return false; 
			}
			return true;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE preference SET "
				+"valuestring = '"  +POut.PString(Cur.ValueString)+"'"
				+" WHERE prefname = '"+POut.PString(Cur.PrefName)+"'";
			NonQ();
		}

		///<summary>Updates a pref of type int.  Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateInt(string prefName,int newValue){
			if(!HList.ContainsKey(prefName)){
				MessageBox.Show(prefName+" is an invalid pref name.");
				return false;
			}
			if(GetInt(prefName)==newValue){
				return false;//no change needed
			}
			cmd.CommandText = "UPDATE preference SET "
				+"ValueString = '"+POut.PInt(newValue)+"' "
				+"WHERE PrefName = '"+POut.PString(prefName)+"'";
			NonQ();
			return true;
		}

		///<summary>Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateBool(string prefName,bool newValue){
			if(!HList.ContainsKey(prefName)){
				MessageBox.Show(prefName+" is an invalid pref name.");
				return false;
			}
			if(GetBool(prefName)==newValue){
				return false;//no change needed
			}
			cmd.CommandText = "UPDATE preference SET "
				+"ValueString = '"+POut.PBool(newValue)+"' "
				+"WHERE PrefName = '"+POut.PString(prefName)+"'";
			NonQ();
			return true;
		}

		///<summary>Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateString(string prefName,string newValue){
			if(!HList.ContainsKey(prefName)){
				MessageBox.Show(prefName+" is an invalid pref name.");
				return false;
			}
			if(GetString(prefName)==newValue){
				return false;//no change needed
			}
			cmd.CommandText = "UPDATE preference SET "
				+"ValueString = '"+POut.PString(newValue)+"' "
				+"WHERE PrefName = '"+POut.PString(prefName)+"'";
			NonQ();
			return true;
		}

		///<summary></summary>
		public static void FlushAndLock(){
			try{
				con.Open();
				cmd.CommandText="FLUSH TABLES WITH READ LOCK";
				int rowsUpdated = cmd.ExecuteNonQuery();
			}
			catch{
				//MessageBox.Show(con.ConnectionString);
				MessageBox.Show(Lan.g("Pref","Error in FlushAndLock"));
			}
		}

		///<summary></summary>
		public static void Unlock(){
			con.Close();
		}	


	}

	


	


}










