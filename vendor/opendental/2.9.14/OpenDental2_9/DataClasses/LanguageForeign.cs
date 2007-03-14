
using System;
using System.Collections;
using System.Globalization;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the languageforeign table in the database.</summary>
	///<remarks>Will usually only contain translations for a single foreign language, although more are allowed.  The primary key is a combination of the ClassType and the English phrase.</remarks>
	public struct LanguageForeign{
		///<summary>A string representing the class where the translation is used.</summary>
		public string ClassType;
		///<summary>The English version of the phrase.</summary>
		public string English;
		///<summary>The culture. Currently simply the 2-letter language identifier, but will be extended to include multiple cultures using a single language.</summary>
		public string Culture;
		///<summary>The foreign translation.</summary>
		public string Translation;
		///<summary>Comments for other translators for the foreign language.</summary>
		public string Comments;
	}

	/*=========================================================================================
	=================================== class LanguageForeign ===========================================*/
	///<summary></summary>
	public class LanguageForeigns:DataClass{
		///<summary></summary>
		public static Hashtable HList;//just translations for one language
		///<summary></summary>
		public static LanguageForeign Cur;
		///<summary></summary>
		public static LanguageForeign[] List;

		///<summary></summary>
		public static void Refresh(){
			//called once when the program first starts up.  Then only if user downloads new translations
			//or adds their own.
			HList=new Hashtable();
			if(CultureInfo.CurrentCulture.TwoLetterISOLanguageName=="en"){
				return;
			}
			cmd.CommandText =
				"SELECT * from languageforeign "
				+"WHERE culture = '"+CultureInfo.CurrentCulture.TwoLetterISOLanguageName+"'";
			FillTable();
			List=new LanguageForeign[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				List[i].ClassType  = PIn.PString(table.Rows[i][0].ToString());
				List[i].English    = PIn.PString(table.Rows[i][1].ToString());
				List[i].Culture    = PIn.PString(table.Rows[i][2].ToString());
				List[i].Translation= PIn.PString(table.Rows[i][3].ToString());
				List[i].Comments   = PIn.PString(table.Rows[i][4].ToString());
				HList.Add(List[i].ClassType+List[i].English,List[i]);
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO languageforeign(classtype,english,culture,translation,comments) "
				+"VALUES("
				+"'"+POut.PString(Cur.ClassType)+"', "
				+"'"+POut.PString(Cur.English)+"', "
		  	+"'"+POut.PString(Cur.Culture)+"', "
				+"'"+POut.PString(Cur.Translation)+"', "
			  +"'"+POut.PString(Cur.Comments)+"')";
			NonQ(false);
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE languageforeign SET " 
				+ "translation	= '"+POut.PString(Cur.Translation)+"'"
				+ ",comments    = '"+POut.PString(Cur.Comments)+"'" 
				+" WHERE ClassType='"+Cur.ClassType+"' && "
				+"English='"+Cur.English+"' && Culture='"+CultureInfo.CurrentCulture.TwoLetterISOLanguageName+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from languageforeign WHERE ClassType='"+Cur.ClassType+"' && "
				+"English='"+Cur.English+"' && Culture='"+CultureInfo.CurrentCulture.TwoLetterISOLanguageName+"'";
			NonQ(false);
		}

	}

	

	

}













