using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary>Handles documents and images for the Images module</summary>
	public class Documents {
		///<summary>This gets a list of all documents referred to in the docAttachList.  Basically, all docs for a patient.  It is done this way because patients will later be sharing documents.</summary>
		public static Document[] Refresh(List<DocAttach> docAttachList) {
			if(docAttachList.Count==0) {
				return new Document[0];
			}
			string command="SELECT * FROM document WHERE DocNum = '"+docAttachList[0].DocNum.ToString()+"'";
			for(int i=1;i<docAttachList.Count;i++) {
				command+=" OR DocNum = '"+POut.PInt(docAttachList[i].DocNum)+"'";
			}
			command+=" ORDER BY DateCreated";
			return RefreshAndFill(command);
		}

		/*
		///<summary></summary>
		public static Document[] GetAllWithPat(int patNum) {
			string command="SELECT * FROM document WHERE WithPat="+POut.PInt(patNum);
			return RefreshAndFill(command);
		}*/

		private static Document[] RefreshAndFill(string command) {
			DataSet ds=null;
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					ds=GeneralB.GetTable(command);
				}
				else {
					DtoGeneralGetTable dto=new DtoGeneralGetTable();
					dto.Command=command;
					ds=RemotingClient.ProcessQuery(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
			DataTable table=ds.Tables[0];
			Document[] List=new Document[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new Document();
				List[i].DocNum        =PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description   =PIn.PString(table.Rows[i][1].ToString());
				List[i].DateCreated   =PIn.PDate  (table.Rows[i][2].ToString());
				List[i].DocCategory   =PIn.PInt   (table.Rows[i][3].ToString());
				List[i].WithPat       =PIn.PInt   (table.Rows[i][4].ToString());
				List[i].FileName      =PIn.PString(table.Rows[i][5].ToString());
				List[i].ImgType       =(ImageType)PIn.PInt(table.Rows[i][6].ToString());
				List[i].IsFlipped     =PIn.PBool  (table.Rows[i][7].ToString());
				List[i].DegreesRotated=PIn.PInt   (table.Rows[i][8].ToString());
				List[i].ToothNumbers  =PIn.PString(table.Rows[i][9].ToString());
				List[i].Note          =PIn.PString(table.Rows[i][10].ToString());
				List[i].SigIsTopaz    =PIn.PBool  (table.Rows[i][11].ToString());
				List[i].Signature     =PIn.PString(table.Rows[i][12].ToString());
			}
			return List;
		}

		///<summary>Inserts a new document into db, creates a filename based on Cur.DocNum, and then updates the db with this filename.  Also attaches the document to the current patient.</summary>
		public static void Insert(Document doc,Patient pat){
			int insertID=0;
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					insertID=DocumentB.Insert(doc,pat.LName+pat.FName,pat.PatNum);
				}
				else {
					DtoDocumentInsert dto=new DtoDocumentInsert();
					dto.Doc=doc;
					dto.PatLF=pat.LName+pat.FName;
					dto.PatNum=pat.PatNum;
					insertID=RemotingClient.ProcessCommand(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
			doc.DocNum=insertID;
		}

		///<summary></summary>
		public static void Update(Document doc){
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					DocumentB.Update(doc);
				}
				else {
					DtoDocumentUpdate dto=new DtoDocumentUpdate();
					dto.Doc=doc;
					RemotingClient.ProcessCommand(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}		
		}

		///<summary></summary>
		public static void Delete(Document doc){
			string command= "DELETE from document WHERE DocNum = '"+doc.DocNum.ToString()+"'";
			General.NonQ(command);	
			command= "DELETE from docattach WHERE DocNum = '"+doc.DocNum.ToString()+"'";
			General.NonQ(command);	
		}

		///<summary>Loops through List to find Cur based on docNum.</summary>
		public static Document GetDocument(string docNum,Document[] docList){
			for(int i=0;i<docList.Length;i++){
				if(docList[i].DocNum.ToString()==docNum){
					return docList[i];
				}
			}
			return null;//should never happen
		}

		///<summary>Used when dragging to a new category. Loops through List to find the defNum of the category based on docNum.</summary>
		public static int GetCategory(string docNum,Document[] docList){
			for(int i=0;i<docList.Length;i++){
				if(docList[i].DocNum.ToString()==docNum){
					return docList[i].DocCategory;
				}
			}
			return -1;
		}

		///<summary>This is used by FormImageViewer to get a list of paths based on supplied list of DocNums. The reason is that later we will allow sharing of documents, so the paths may not be in the current patient folder.</summary>
		public static ArrayList GetPaths(ArrayList docNums){
			if(docNums.Count==0){
				return new ArrayList();
			}
			string command="SELECT document.DocNum,document.FileName,patient.ImageFolder "
				+"FROM document "
				+"LEFT JOIN patient ON patient.PatNum=document.WithPat "
				+"WHERE document.DocNum = '"+docNums[0].ToString()+"'";
			for(int i=1;i<docNums.Count;i++){
				command+=" OR document.DocNum = '"+docNums[i].ToString()+"'";
			}
			//remember, they will not be in the correct order.
			DataTable table=General.GetTable(command);
			Hashtable hList=new Hashtable();//key=docNum, value=path
			//one row for each document, but in the wrong order
			for(int i=0;i<table.Rows.Count;i++){
				hList.Add(PIn.PInt(table.Rows[i][0].ToString()),
					((Pref)PrefB.HList["DocPath"]).ValueString
					+PIn.PString(table.Rows[i][2].ToString()).Substring(0,1)
					+@"\"
					+PIn.PString(table.Rows[i][2].ToString())
					+@"\"
					+PIn.PString(table.Rows[i][1].ToString()));
			}
			ArrayList retVal=new ArrayList();
			for(int i=0;i<docNums.Count;i++){
				retVal.Add((string)hList[(int)docNums[i]]);
			}
			return retVal;
		}

		/// <summary>Makes one call to the database to retrieve the filename of the patient pict for the given patNum.  Assumes WithPat will always be same as patnum.</summary>
		public static string GetPatPict(int patNum){
			//first establish which category pat pics are in
			int defNumPicts=0;
			for(int i=0;i<DefB.Short[(int)DefCat.ImageCats].Length;i++){
				if(DefB.Short[(int)DefCat.ImageCats][i].ItemValue=="P"){
					defNumPicts=DefB.Short[(int)DefCat.ImageCats][i].DefNum;
					break;
				}
			}
			if(defNumPicts==0){//no category set for picts
				return "";
			}
			//then find 
			string command="SELECT FileName FROM document,docattach "
				+"WHERE document.DocNum=docattach.DocNum "
				+"AND docattach.PatNum="+POut.PInt(patNum)
				+" AND document.DocCategory="+POut.PInt(defNumPicts)
				+" ORDER BY DateCreated DESC ";
			//gets the most recent
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle){
				command="SELECT * FROM ("+command+") WHERE ROWNUM<=1";
			}else{//Assume MySQL
				command+="LIMIT 1";
			}
			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0){//no pictures
				return "";
			}
			return PIn.PString(table.Rows[0][0].ToString());
		}

	}

	
  
	/*public struct DocBackup{
		public string FileName;
		public DateTime LastAltered;
		public bool IsDeleted;
		public string PatFolder;
	}*/


}









