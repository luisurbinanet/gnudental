
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the languageforeign table in the database.</summary>
	///<remarks>Will usually only contain translations for a single foreign language, although more are allowed.  The primary key is a combination of the ClassType and the English phrase.</remarks>
	public class LanguageForeign{
		///<summary>A string representing the class where the translation is used.</summary>
		public string ClassType;
		///<summary>The English version of the phrase.  Case sensitive.</summary>
		public string English;
		///<summary>The specific culture name.  Almost always in 5 digit format like this: en-US.</summary>
		public string Culture;
		///<summary>The foreign translation.</summary>
		public string Translation;
		///<summary>Comments for other translators for the foreign language.</summary>
		public string Comments;

		///<summary></summary>
		public LanguageForeign Copy(){
			LanguageForeign l=new LanguageForeign();
			l.ClassType=ClassType;
			l.English=English;
			l.Culture=Culture;
			l.Translation=Translation;
			l.Comments=Comments;
			return l;
		}

		///<summary></summary>
		public void Insert(){
			string command= "INSERT INTO languageforeign(ClassType,English,Culture"
				+",Translation,Comments) "
				+"VALUES("
				+"'"+POut.PString(ClassType)+"', "
				+"'"+POut.PString(English)+"', "
				+"'"+POut.PString(Culture)+"', "
				+"'"+POut.PString(Translation)+"', "
				+"'"+POut.PString(Comments)+"')";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Update(){
			string command="UPDATE languageforeign SET " 
				+"Translation	= '"+POut.PString(Translation)+"'"
				+",Comments = '"  +POut.PString(Comments)+"'" 
				+" WHERE ClassType= BINARY '"+POut.PString(ClassType)+"'" 
				+" AND English= BINARY '"+POut.PString(English)+"'"
				+" AND Culture= '"+CultureInfo.CurrentCulture.Name+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Delete(){
			string command= "DELETE from languageforeign "
				+"WHERE ClassType=BINARY '"+POut.PString(ClassType)+"' "
				+"AND English=BINARY '"    +POut.PString(English)+"' "
				+"AND Culture='"+CultureInfo.CurrentCulture.Name+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

	}

	/*=========================================================================================
	=================================== class LanguageForeign ===========================================*/
	///<summary></summary>
	public class LanguageForeigns{
		///<summary>just translations for the culture currently being used.  If a translation is missing, it tries to use a translation from another culture with the same language. Key=ClassType+English. Value =LanguageForeign object.  When support for multiple simultaneous languages is added, there will still be a current culture, but then we will add a supplemental way to extract translations for alternate cultures.</summary>
		public static Hashtable HList;
		
		///<summary>Called once when the program first starts up.  Then only if user downloads new translations or adds their own.</summary>
		public static void Refresh(CultureInfo cultureInfo){
			HList=new Hashtable();
			if(CultureInfo.CurrentCulture.Name=="en-US"){
				return;
			}
			//load all translations for the current culture, using other culture of same language if no trans avail.
			string command=
				"SELECT * FROM languageforeign "
				+"WHERE Culture LIKE '"+cultureInfo.TwoLetterISOLanguageName+"%' "
				+"ORDER BY Culture";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			LanguageForeign lf;
			for(int i=0;i<table.Rows.Count;i++){
				lf=new LanguageForeign();
				lf.ClassType  = PIn.PString(table.Rows[i][0].ToString());
				lf.English    = PIn.PString(table.Rows[i][1].ToString());
				lf.Culture    = PIn.PString(table.Rows[i][2].ToString());
				lf.Translation= PIn.PString(table.Rows[i][3].ToString());
				lf.Comments   = PIn.PString(table.Rows[i][4].ToString());
				if(lf.Culture==cultureInfo.Name){//if exact culture match
					if(HList.ContainsKey(lf.ClassType+lf.English)){
						HList.Remove(lf.ClassType+lf.English);//remove any existing entry
					}
					HList.Add(lf.ClassType+lf.English,lf);
				}
				else{//or if any other culture of same language
					if(!HList.ContainsKey(lf.ClassType+lf.English)){
						//only add if not already in HList
						HList.Add(lf.ClassType+lf.English,lf);
					}
				}
			}
		}

		///<summary>Only used during export to get a list of all translations for specified culture only.</summary>
		public static LanguageForeign[] GetListForCulture(CultureInfo cultureInfo){
			string command=
				"SELECT * FROM languageforeign "
				+"WHERE Culture='"+CultureInfo.CurrentCulture.Name+"'";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			LanguageForeign[] List=new LanguageForeign[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new LanguageForeign();
				List[i].ClassType  = PIn.PString(table.Rows[i][0].ToString());
				List[i].English    = PIn.PString(table.Rows[i][1].ToString());
				List[i].Culture    = PIn.PString(table.Rows[i][2].ToString());
				List[i].Translation= PIn.PString(table.Rows[i][3].ToString());
				List[i].Comments   = PIn.PString(table.Rows[i][4].ToString());
			}
			return List;
		}

		///<summary>Used in FormTranslation to get all translations for all cultures for one classtype</summary>
		public static LanguageForeign[] GetListForType(string classType){
			string command=
				"SELECT * FROM languageforeign "
				+"WHERE ClassType='"+POut.PString(classType)+"'";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			LanguageForeign[] List=new LanguageForeign[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new LanguageForeign();
				List[i].ClassType  = PIn.PString(table.Rows[i][0].ToString());
				List[i].English    = PIn.PString(table.Rows[i][1].ToString());
				List[i].Culture    = PIn.PString(table.Rows[i][2].ToString());
				List[i].Translation= PIn.PString(table.Rows[i][3].ToString());
				List[i].Comments   = PIn.PString(table.Rows[i][4].ToString());
			}
			return List;
		}
		
		///<summary>Used in FormTranslation to get a single entry for the specified culture.  The culture match must be extact.  If no translation entries, then it returns null.</summary>
		public static LanguageForeign GetForCulture(LanguageForeign[] listForType,string english,string cultureName){
			for(int i=0;i<listForType.Length;i++){
				if(english!=listForType[i].English){
					continue;
				}
				if(cultureName!=listForType[i].Culture){
					continue;
				}
				return listForType[i];
			}
			return null;
		}

		///<summary>Used in FormTranslation to get a single entry with the same language as the specified culture, but only for a different culture.  For instance, if culture is es-PR (Spanish-PuertoRico), then it will return any spanish translation that is NOT from Puerto Rico.  If no other translation entries, then it returns null.</summary>
		public static LanguageForeign GetOther(LanguageForeign[] listForType,string english,string cultureName){
			for(int i=0;i<listForType.Length;i++){
				if(english!=listForType[i].English){
					continue;
				}
				if(cultureName==listForType[i].Culture){
					continue;
				}
				if(cultureName.Substring(0,2)!=listForType[i].Culture.Substring(0,2)){
					continue;
				}
				return listForType[i];
			}
			return null;
		}

		

	}

	

	

}













