using System;
using System.Collections;
using System.Globalization;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the language table in the database.</summary>
	///<remarks>The primary key is a combination of the ClassType and the English phrase.  This table is currently filled dynmically at run time, but the plan is to fill it using a tool that parses the code.</remarks>
	public struct Language{
		///<summary>Comments by us regarding usage.</summary>
		public string EnglishComments;
		///<summary>A string representing the class where the translation is used. Maximum length is 25 characters.</summary>
		public string ClassType;
		///<summary>The English version of the phrase, case sensitive.</summary>
		public string English;
		///<summary>As this gets more sophisticated, we will use this field to mark some phrases obsolete instead of just deleting them outright.  That way, translators will still have access to them.</summary>
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

		///<summary>not used to update the english version of text.  Create new instead.</summary>
		public static void UpdateCur(){
			cmd.CommandText="UPDATE language SET "
				+"EnglishComments = '" +POut.PString(Cur.EnglishComments)+"'"
				+",IsObsolete = '"     +POut.PBool  (Cur.IsObsolete)+"'"
				+" WHERE ClassType = BINARY '"+POut.PString(Cur.ClassType)+"'"
				+" AND English = BINARY '"     +POut.PString(Cur.English)+"'";
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
				+"WHERE ClassType = BINARY '"+classType+"'";
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

		//forms----------------------------------------------------------------------------------------
		///<summary>F is for Form. Translates the following controls on the entire form: title Text, labels, buttons, groupboxes, checkboxes, radiobuttons.  Can include a list of controls to exclude. Also puts all the correct controls into the All category (OK,Cancel,Close,Delete,etc).</summary>
		public static void F(System.Windows.Forms.Form sender){
			F(sender,new System.Windows.Forms.Control[] {});
		}

		///<summary>F is for Form. Translates the following controls on the entire form: title Text, labels, buttons, groupboxes, checkboxes, radiobuttons.  Can include a list of controls to exclude. Also puts all the correct controls into the All category (OK,Cancel,Close,Delete,etc).</summary>
		public static void F(Form sender,Control[] exclusions){
			if(CultureInfo.CurrentCulture.TwoLetterISOLanguageName=="en"){
				return;
			}
			//first translate the main title Text on the form:
			if(!Contains(exclusions,sender)){
				sender.Text=ConvertString(sender.GetType().Name,sender.Text);
			}
			//then launch the recursive function for all child controls
			Fchildren(sender,sender,exclusions);
			if(itemInserted)
				Refresh();
		}

		///<summary>Called from F and also recursively. Translates all children of the given control except those in the exclusions list.</summary>
		private static void Fchildren(Form sender,Control parent,Control[] exclusions){
			foreach(Control contr in parent.Controls){
				//first any controls with children of their own.
				if(contr.HasChildren){
					Fchildren(sender,contr,exclusions);
				}
				//ignore any controls except the types we are interested in
				if(contr.GetType()!=typeof(TextBox)
					&& contr.GetType()!=typeof(Button)
					&& contr.GetType()!=typeof(OpenDental.UI.Button)
					&& contr.GetType()!=typeof(Label)
					&& contr.GetType()!=typeof(GroupBox)
					&& contr.GetType()!=typeof(CheckBox)
					&& contr.GetType()!=typeof(RadioButton))
				{
					continue;
				}
				if(contr.Text==""){
					continue;
				}
				if(!Contains(exclusions,contr)){
					if(contr.Text=="OK"
						|| contr.Text=="&OK"
						|| contr.Text=="Cancel"
						|| contr.Text=="&Cancel"
						|| contr.Text=="Close"
						|| contr.Text=="&Close"
						|| contr.Text=="Add"
						|| contr.Text=="&Add"
						|| contr.Text=="Delete"
						|| contr.Text=="&Delete"
						|| contr.Text=="Up"
						|| contr.Text=="&Up"
						|| contr.Text=="Down"
						|| contr.Text=="&Down"
						|| contr.Text=="Print"
						|| contr.Text=="&Print"
						//|| contr.Text==""
						){
						contr.Text=ConvertString("All",contr.Text);
					}
					else{
						contr.Text=ConvertString(sender.GetType().Name,contr.Text);
					}
				}
			}
		}

		///<summary>Returns true if the contrToFind exists in the supplied contrArray. Used to check the exclusion list of F.</summary>
		private static bool Contains(Control[] contrArray,Control contrToFind){
			for(int i=0;i<contrArray.Length;i++){
				if(contrArray[i]==contrToFind){
					return true;
				}
			}
			return false;
		}

		///<summary>Gets a short time format for displaying in appt and schedule along the sides. Create a clone of the current culture and pass it in. It will get altered. Returns a string format.</summary>
		public static string GetShortTimeFormat(CultureInfo ci){
			string hFormat="";
			ci.DateTimeFormat.AMDesignator=ci.DateTimeFormat.AMDesignator.ToLower();
			ci.DateTimeFormat.PMDesignator=ci.DateTimeFormat.PMDesignator.ToLower();
			string shortPattern=ci.DateTimeFormat.ShortTimePattern;
			if(shortPattern.IndexOf("hh")!=-1){//if hour is 01-12
				hFormat+="hh";
			}
			else if(shortPattern.IndexOf("h")!=-1){//or if hour is 1-12
				hFormat+="h";
			}
			else if(shortPattern.IndexOf("HH")!=-1){//or if hour is 00-23
				hFormat+="HH";
			}
			else{//hour is 0-23
				hFormat+="H";
			}
			if(shortPattern.IndexOf("t")!=-1){//if there is an am/pm designator
				hFormat+="tt";
			}
			else{//if no am/pm designator, then use :00
				hFormat+=":00";//time separator will actually change according to region
			}
			return hFormat;
		}

	}

	

	

}













