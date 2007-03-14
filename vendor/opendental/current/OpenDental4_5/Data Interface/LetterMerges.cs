using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Describes the templates for letter merges to Word.</summary>
	public class LetterMerge{
		///<summary>Primary key.</summary>
		public int LetterMergeNum;
		///<summary>Description of this letter.</summary>
		public string Description;
		///<summary>The filename of the Word template. eg MyTemplate.doc.</summary>
		public string TemplateName;
		///<summary>The name of the data file. eg MyTemplate.txt.</summary>
		public string DataFileName;
		///<summary>FK to definition.DefNum.</summary>
		public int Category;
		///<summary>Not a database column.  Filled using fk from the lettermergefields table.  The arrayList is a collection of strings representing field names.</summary>
		public ArrayList Fields;

		/*//<summary>Returns a copy of the clearinghouse.</summary>
    public ClaimForm Copy(){
			ClaimForm cf=new ClaimForm();
			cf.ClaimFormNum=ClaimFormNum;
			cf.Description=Description;
			return cf;
		}*/

		///<summary>Inserts this lettermerge into database.</summary>
		public void Insert(){
			if(Prefs.RandomKeys){
				LetterMergeNum=MiscData.GetKey("lettermerge","LetterMergeNum");
			}
			string command= "INSERT INTO lettermerge (";
			if(Prefs.RandomKeys){
				command+="LetterMergeNum,";
			}
			command+="Description,TemplateName,DataFileName,"
				+"Category) VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(LetterMergeNum)+"', ";
			}
			command+=
				 "'"+POut.PString(Description)+"', "
				+"'"+POut.PString(TemplateName)+"', "
				+"'"+POut.PString(DataFileName)+"', "
				+"'"+POut.PInt   (Category)+"')";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				LetterMergeNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void Update(){
			string command="UPDATE lettermerge SET "
				+"Description = '"   +POut.PString(Description)+"' "
				+",TemplateName = '" +POut.PString(TemplateName)+"' "
				+",DataFileName = '" +POut.PString(DataFileName)+"' "
				+",Category = '"     +POut.PInt   (Category)+"' "
				+"WHERE LetterMergeNum = '"+POut.PInt(LetterMergeNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Delete(){
			string command="DELETE FROM lettermerge "
				+"WHERE LetterMergeNum = "+POut.PInt(LetterMergeNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}
		
	}

/*=========================================================================================
		=================================== class LetterMerges=======================================*/

	///<summary></summary>
	public class LetterMerges{
		///<summary>List of all lettermerges.</summary>
		public static LetterMerge[] List;
		private static Word.Application wordApp;

		///<summary>This is a static reference to a word application.  That way, we can reuse it instead of having to reopen Word each time.</summary>
		public static Word.Application WordApp{
			get{
				if(wordApp==null){
					wordApp=new Word.Application();
					wordApp.Visible=true;
				}
				try{
					wordApp.Activate();
				}
				catch{
					wordApp=new Word.Application();
					wordApp.Visible=true;
					wordApp.Activate();
				}
				return wordApp;
			}
		}
		
		///<summary>Must have run LetterMergeFields first.</summary>
		public static void Refresh(){
			string command=
				"SELECT * FROM lettermerge ORDER BY Description";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			List=new LetterMerge[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new LetterMerge();
				List[i].LetterMergeNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description   = PIn.PString(table.Rows[i][1].ToString());
				List[i].TemplateName  = PIn.PString(table.Rows[i][2].ToString());
				List[i].DataFileName  = PIn.PString(table.Rows[i][3].ToString());
				List[i].Category      = PIn.PInt   (table.Rows[i][4].ToString());
				List[i].Fields=LetterMergeFields.GetForLetter(List[i].LetterMergeNum);
			}
		}

		///<summary>Supply the index of the cat within Defs.Short.</summary>
		public static LetterMerge[] GetListForCat(int catIndex){
			ArrayList AL=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].Category==Defs.Short[(int)DefCat.LetterMergeCats][catIndex].DefNum){
					AL.Add(List[i]);
				}
			}
			LetterMerge[] retVal=new LetterMerge[AL.Count];
			for(int i=0;i<AL.Count;i++){
				retVal[i]=(LetterMerge)AL[i];
			}
			return retVal;
		}

	
		


	}

	



}









