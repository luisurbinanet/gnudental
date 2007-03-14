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
		///<summary>A string representing the class where the translation is used.</summary>
		public string ClassType;
		///<summary>The English version of the phrase.</summary>
		public string English;
		///<summary>As this gets more complex, we will use this field to mark some phrases obsolete instead of just deleting them outright.  That way, translators will still have access to them.</summary>
		public bool IsObsolete;
	}

	/*=========================================================================================
	=================================== class Lan ===========================================*/
	///<summary>Handles database commands for the language table in the database.</summary>
	public class Lan:DataClass{
		///<summary></summary>
		public static Hashtable HList;//the purpose is to allow automatic adding of phrases to db
		///<summary></summary>
		public static Language Cur;
		///<summary></summary>
		public static string[] ListCat;//list of categories
		///<summary></summary>
		public static Language[] List;
		///<summary></summary>
		public static string CurCat;
		///<summary></summary>
		public static Language[] ListForCat;

		///<summary></summary>
		public static void Refresh(){
			//Refreshed automatically to always be kept current with all phrases, regardless of whether
			//there are any entries in LanguageForeign table.
			HList=new Hashtable();
			if(CultureInfo.CurrentCulture.TwoLetterISOLanguageName=="en"){
				return;
			}
			cmd.CommandText =
				"SELECT * from language";
			FillTable();
			List=new Language[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				List[i].EnglishComments= PIn.PString(table.Rows[i][0].ToString());
				List[i].ClassType      = PIn.PString(table.Rows[i][1].ToString());
				List[i].English        = PIn.PString(table.Rows[i][2].ToString());
				List[i].IsObsolete     = PIn.PBool  (table.Rows[i][3].ToString());
				HList.Add(List[i].ClassType+List[i].English,List[i]);
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO language (classtype,english) "
				+"VALUES("
				+"'"+POut.PString(Cur.ClassType)+"', "
				+"'"+POut.PString(Cur.English)+"')";
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

		///<summary>Converts a string to the current language.</summary>
		public static string g(System.Windows.Forms.Control sender,string text){
			return g(sender.GetType().Name,text);
		}

		///<summary>Converts a string to the current language.</summary>
		public static string g(string classType,string text){
			if(CultureInfo.CurrentCulture.TwoLetterISOLanguageName=="en"){
				return text;
			}
			if(text==""){
				return "";
			}
			if(HList==null) return text;
			//try{
			if(!HList.ContainsKey(classType+text)){
				Cur=new Language();
				Cur.ClassType=classType;
				Cur.English=text;
				//MessageBox.Show(Cur.ClassType+Cur.English);
				InsertCur();
				Refresh();
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

		///<summary></summary>
		public static void C(string classType, System.Windows.Forms.MenuItem[] item){
			for(int i=0;i<item.Length;i++){
				item[i].Text=g(classType,item[i].Text);
			}
		}

		///<summary></summary>
		public static void C(System.Windows.Forms.MenuItem sender, System.Windows.Forms.MenuItem[] item){
			for(int i=0;i<item.Length;i++){
				item[i].Text=g(sender.GetType().Name,item[i].Text);
			}
		}

		///<summary>Converts a string to the current language.</summary>
		public static string g(System.Windows.Forms.MenuItem sender,string text){
			return g(sender.GetType().Name,text);
		}

		///<summary></summary>
		public static void C(string classType, System.Windows.Forms.Control[] contr){
			for(int i=0;i<contr.Length;i++){
				contr[i].Text=g(classType,contr[i].Text);
			}
		}

		///<summary></summary>
		public static void C(System.Windows.Forms.Control sender, System.Windows.Forms.Control[] contr){
			for(int i=0;i<contr.Length;i++){
				contr[i].Text=g(sender.GetType().Name,contr[i].Text);
			}
		}

	}

	

	

}













