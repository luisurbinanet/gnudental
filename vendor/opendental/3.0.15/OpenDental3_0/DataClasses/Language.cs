using System;
using System.Collections;
using System.Globalization;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the language table in the database.</summary>
	///<remarks>The primary key is a combination of the ClassType and the English phrase.  We still haven't quite perfected this for situations where the English phrase is very long.  This table is currently filled dynmically at run time, but the plan is to fill it using a tool that parses the code.</remarks>
	public struct Language{
		///<summary>Comments by us regarding usage.</summary>
		public string EnglishComments;
		///<summary>A string representing the class where the translation is used. Maximum length is 25 characters.</summary>
		public string ClassType;
		///<summary>The English version of the phrase. Maximum length is 225 characters and is case sensitive.  If the phrase is longer than 225, it is currently ignored.</summary>
		public string English;
		///<summary>As this gets more complex, we will use this field to mark some phrases obsolete instead of just deleting them outright.  That way, translators will still have access to them.</summary>
		public bool IsObsolete;
	}

	/*=========================================================================================
	=================================== class Lan ===========================================*/
	///<summary>Handles database commands for the language table in the database.</summary>
	public class Lan:DataClass{
		///<summary>key=ClassType+English.  Value =Language object.</summary>
		public static Hashtable HList;
		///<summary></summary>
		public static Language Cur;
		///<summary></summary>
		public static string[] ListCat;//list of categories
		///<summary></summary>
		private static Language[] List;
		///<summary></summary>
		public static string CurCat;
		///<summary></summary>
		public static Language[] ListForCat;
		///<summary>Used by g to keep track of whether any language items were inserted into db. If so a refresh gets done.</summary>
		private static bool itemInserted;

		///<summary>Refreshed automatically to always be kept current with all phrases, regardless of whether there are any entries in LanguageForeign table.</summary>
		public static void Refresh(){
			HList=new Hashtable();
			if(CultureInfo.CurrentCulture.TwoLetterISOLanguageName=="en"){
				return;
			}
			cmd.CommandText =
				"SELECT * from language";
			FillTable();
			List=new Language[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].EnglishComments= PIn.PString(table.Rows[i][0].ToString());
				List[i].ClassType      = PIn.PString(table.Rows[i][1].ToString());
				List[i].English        = PIn.PString(table.Rows[i][2].ToString());
				List[i].IsObsolete     = PIn.PBool  (table.Rows[i][3].ToString());
				if(!HList.ContainsKey(List[i].ClassType+List[i].English)){
					HList.Add(List[i].ClassType+List[i].English,List[i]);
				}
			}
			//MessageBox.Show(List.Length.ToString());
		}

		///<summary>Tries to insert, but ignores the insert if this row already exists. This prevents the previous frequent crashes.</summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT IGNORE INTO language (ClassType,English) "
				+"VALUES("
				+"'"+POut.PString(Cur.ClassType)+"', "
				+"'"+POut.PString(Cur.English)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void UpdateCur(){
			//not used to update the english version of text.  Create new instead.
			cmd.CommandText="UPDATE language SET "
				+"englishcomments = '" +POut.PString(Cur.EnglishComments)+"'"
				+",isobsolete = '"     +POut.PBool  (Cur.IsObsolete)+"'"
				+" WHERE classtype = '"+POut.PString(Cur.ClassType)+"'"
				+" && english = '"     +POut.PString(Cur.English)+"'";
			NonQ(false);
			
		}

		///<summary></summary>
		public static void GetListCat(){
			cmd.CommandText =
				"SELECT Distinct ClassType FROM language ORDER BY ClassType ";
			FillTable();
			ListCat=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				ListCat[i]=PIn.PString(table.Rows[i][0].ToString());
			}
		}

		///<summary></summary>
		public static void SetObsolete(Language[] lanList,bool isObsolete){
			for(int i=0;i<lanList.Length;i++){
				Cur=lanList[i];
				Cur.IsObsolete=isObsolete;
				UpdateCur();
			}
		}

		///<summary></summary>
		public static void GetListForCat(string classType){
			//only used in translation tool
			if(CultureInfo.CurrentCulture.TwoLetterISOLanguageName=="en"){
				return;
			}
			cmd.CommandText =
				"SELECT * from language "
				+"WHERE classtype = '"+classType+"'";
			FillTable();
			ListForCat=new Language[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				ListForCat[i].EnglishComments= PIn.PString(table.Rows[i][0].ToString());
				ListForCat[i].ClassType      = PIn.PString(table.Rows[i][1].ToString());
				ListForCat[i].English        = PIn.PString(table.Rows[i][2].ToString());
				ListForCat[i].IsObsolete     = PIn.PBool  (table.Rows[i][3].ToString());
			}
		}

		private static string ConvertString(string classType,string text){
			if(CultureInfo.CurrentCulture.TwoLetterISOLanguageName=="en"){
				return text;
			}
			if(text==""){
				return "";
			}
			if(HList==null) return text;
			if(classType.Length>25 || text.Length>225){
				return text;
			}
			itemInserted=false;
			//try{
			if(!HList.ContainsKey(classType+text)){
				Cur=new Language();
				Cur.ClassType=classType;
				Cur.English=text;
				//MessageBox.Show(Cur.ClassType+Cur.English);
				InsertCur();
				itemInserted=true;
				//Refresh();
				return text;
			}
			//}
			//catch{
			//	MessageBox.Show(classType+text);
			//}
			if(LanguageForeigns.HList.Contains(classType+text)){
				if(((LanguageForeign)LanguageForeigns.HList[classType+text]).Translation==""){
					//if translation is empty
					return text;//return the English version
				}
				return ((LanguageForeign)LanguageForeigns.HList[classType+text]).Translation;	
			}
			else{
				return text;
			}
		}

		//strings-----------------------------------------------
		///<summary>Converts a string to the current language.</summary>
		public static string g(string classType,string text){
			string retVal=ConvertString(classType,text);
			if(itemInserted)
				Refresh();
			return retVal;
		}

		///<summary>Converts a string to the current language.</summary>
		public static string g(System.Windows.Forms.Control sender,string text){
			string retVal=ConvertString(sender.GetType().Name,text);
			if(itemInserted)
				Refresh();
			return retVal;
		}

		//menuItems---------------------------------------------
		///<summary>C is for control. Translates the text of this control to another language.</summary>
		public static void C(string classType, System.Windows.Forms.MenuItem mi){
			mi.Text=ConvertString(classType,mi.Text);
			if(itemInserted)
				Refresh();
		}

		///<summary></summary>
		public static void C(System.Windows.Forms.Control sender, System.Windows.Forms.MenuItem mi){
			mi.Text=ConvertString(sender.GetType().Name,mi.Text);
			if(itemInserted)
				Refresh();
		}		

		//controls-----------------------------------------------
		///<summary></summary>
		public static void C(string classType, System.Windows.Forms.Control[] contr){
			for(int i=0;i<contr.Length;i++){
				contr[i].Text=ConvertString(classType,contr[i].Text);
			}
			if(itemInserted)
				Refresh();
		}

		///<summary></summary>
		public static void C(System.Windows.Forms.Control sender, System.Windows.Forms.Control[] contr){
			for(int i=0;i<contr.Length;i++){
				contr[i].Text=ConvertString(sender.GetType().Name,contr[i].Text);
			}
			if(itemInserted)
				Refresh();
		}

	}

	

	

}













