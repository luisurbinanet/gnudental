using System;
using System.Collections;
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

		///<summary></summary>
		public static bool ConvertDB(){
			ExitApplicationNow ExitApplicationNow2=new ExitApplicationNow();
			ClassConvertDatabase ClassConvertDatabase2=new ClassConvertDatabase();
			if(ClassConvertDatabase2.Convert(((Pref)HList["DataBaseVersion"]).ValueString)){
				return true;
			}
			else{
				MessageBox.Show(Lan.g("Pref","Conversion unsuccessful"));
				ExitApplicationNow2.ExitNow();
				return false;
			}
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
			catch{//(MySQLDriverCS.MySQLException ex){
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

		///<summary>Updates a pref of type int.</summary>
		public static void UpdateInt(string prefName,int newValue){
			if(!HList.ContainsKey(prefName)){
				MessageBox.Show(prefName+" is an invalid pref name.");
				return;
			}
			cmd.CommandText = "UPDATE preference SET "
				+"ValueString = '"+POut.PInt(newValue)+"' "
				+"WHERE PrefName = '"+POut.PString(prefName)+"'";
			NonQ();
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










