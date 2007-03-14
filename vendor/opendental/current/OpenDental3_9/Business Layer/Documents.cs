using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the document table in the database. It should be called image, but the name is for historical reasons.</summary>
	public class Document{
		///<summary>Primary key.</summary>
		public int DocNum;
		///<summary>Description of the document.</summary>
		public string Description;
		///<summary>Date created.</summary>
		public DateTime DateCreated;
		///<summary>Foreign key to definition.DefNum. Cateories for documents.</summary>
		public int DocCategory;
		///<summary>Foreign key to patient.PatNum.  Patient folder that document is in.(for sharing situations later)</summary>
		public int WithPat;
		///<summary>The name of the file. Does not include any directory info.</summary>
		public string FileName;
		///<summary>eg. document, radiograph, photo, file</summary>
		public ImageType ImgType;
		///<summary>True if flipped horizontally. A vertical flip would be stored as a horizontal flip plus a 180 rotation.</summary>
		public bool IsFlipped;
		///<summary>Only allowed 0,90,180, and 270.</summary>
		public int DegreesRotated;
		//public DateTime LastAltered;
		//public bool IsDeleted;
		///<summary>Incomplete.  An optional list of tooth numbers separated by commas.  The tooth numbers will be in American format and must be processed for display.  When displayed, dashes will be used for sequences of 3 or more tooth numbers.</summary>
		public string ToothNumbers;

		///<summary>Returns a copy of this Clinic.</summary>
		public Document Copy(){
			Document d=new Document();
			d.DocNum=DocNum;
			d.Description=Description;
			d.DateCreated=DateCreated;
			d.DocCategory=DocCategory;
			d.WithPat=WithPat;
			d.FileName=FileName;
			d.ImgType=ImgType;
			d.IsFlipped=IsFlipped;
			d.DegreesRotated=DegreesRotated;
			d.ToothNumbers=ToothNumbers;
			return d;
		}

		///<summary>Inserts a new document into db, creates a filename based on Cur.DocNum, and then updates the db with this filename.  Also attaches the document to the current patient.</summary>
		public void Insert(Patient pat){
			if(Prefs.RandomKeys){
				DocNum=MiscData.GetKey("document","DocNum");
			}
			string command="INSERT INTO document (";
			if(Prefs.RandomKeys){
				command+="DocNum,";
			}
			command+="Description,DateCreated,DocCategory,"
				+"WithPat,FileName,ImgType,IsFlipped,DegreesRotated,ToothNumbers) VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(DocNum)+"', ";
			}
			command+=
				"'"+POut.PString(Description)+"', "
				+"'"+POut.PDate  (DateCreated)+"', "
				+"'"+POut.PInt   (DocCategory)+"', "
				+"'"+POut.PInt   (WithPat)+"', "
				+"'"+POut.PString(FileName)+"', "//this may simply be the extension at this point, or it may be the full filename.
				+"'"+POut.PInt   ((int)ImgType)+"', "
				+"'"+POut.PBool  (IsFlipped)+"', "
				+"'"+POut.PInt   (DegreesRotated)+"', "
				+"'"+POut.PString(ToothNumbers)+"')";
			/*+"'"+POut.PDate  (LastAltered)+"', "//will later be used in backups
					+"'"+POut.PBool  (IsDeleted)+"')";//ditto*/
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
				dcon.NonQ(command,true);
				DocNum=dcon.InsertID;
			}
			//If the current filename is just an extension, then assign it a unique name.
			if(FileName==Path.GetExtension(FileName)){
				string extension=FileName;
				FileName="";
				string s=pat.LName+pat.FName;
				for(int i=0;i<s.Length;i++){
					if(Char.IsLetter(s,i)){
						FileName+=s.Substring(i,1);
					}
				}
				FileName+=DocNum.ToString()+extension;//ensures unique name
				//there is still a slight chance that someone manually added a file with this name, so quick fix:
				Document[] docList=Documents.GetAllWithPat(WithPat);
				while(Documents.IsFileNameInList(FileName,docList)){
					FileName="x"+FileName;
				}
				Update();
			}
			DocAttaches.Cur=new DocAttach();
			DocAttaches.Cur.DocNum=DocNum;
			DocAttaches.Cur.PatNum=pat.PatNum;
			DocAttaches.InsertCur();
		}

		///<summary></summary>
		public void Update(){
			string command="UPDATE document SET " 
				+ "Description = '"      +POut.PString(Description)+"'"
				+ ",DateCreated = '"     +POut.PDate  (DateCreated)+"'"
				+ ",DocCategory = '"     +POut.PInt   (DocCategory)+"'"
				+ ",WithPat = '"         +POut.PInt   (WithPat)+"'"
				+ ",FileName    = '"     +POut.PString(FileName)+"'"
				+ ",ImgType    = '"      +POut.PInt   ((int)ImgType)+"'"
				+ ",IsFlipped   = '"     +POut.PBool  (IsFlipped)+"'"
				+ ",DegreesRotated   = '"+POut.PInt   (DegreesRotated)+"'"
				+ ",ToothNumbers   = '"  +POut.PString(ToothNumbers)+"'"
				//+ ",LastAltered= '"     +POut.PDate  (LastAltered)+"'"
				//+ ",IsDeleted = '"      +POut.PBool  (IsDeleted)+"'"
				+" WHERE DocNum = '"    +POut.PInt(DocNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);		
		}

		///<summary></summary>
		public void Delete(){
			string command= "DELETE from document WHERE DocNum = '"+DocNum.ToString()+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);	
			command= "DELETE from docattach WHERE DocNum = '"+DocNum.ToString()+"'";
			dcon.NonQ(command);	
		}

	}

	/*=========================================================================================
	=================================== class Documents ==========================================*/

	///<summary>Handles images for the Images module</summary>
	public class Documents{
		//<summary></summary>
		//public static Document[] List;
		//public static DocBackup[] ListBackup;
		//<summary></summary>
		//public static Document Cur;	

		///<summary>This gets a list of all documents referred to in the docAttachList.  Basically, all docs for a patient.  It is done this way because patients will later be sharing documents.</summary>
		public static Document[] Refresh(DocAttach[] docAttachList){
			if(docAttachList.Length==0){
				return new Document[0];
			}
			string command="SELECT * FROM document WHERE DocNum = '"+docAttachList[0].DocNum.ToString()+"'";
			for(int i=1;i<docAttachList.Length;i++){
				command+=" OR DocNum = '"+POut.PInt(docAttachList[i].DocNum)+"'";
			}
			command+=" ORDER BY DateCreated";
			return RefreshAndFill(command);
		}

		public static Document[] GetAllWithPat(int patNum){
			string command="SELECT * FROM document WHERE WithPat="+POut.PInt(patNum);
			return RefreshAndFill(command);
		}

		private static Document[] RefreshAndFill(string command){
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			Document[] List=new Document[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
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
				//List[i].LastAltered=PIn.PDate  (table.Rows[i][].ToString());
				//List[i].IsDeleted  =PIn.PBool  (table.Rows[i][].ToString());
			}
			return List;
		}

		/*public static void GetBackupList(){
			cmd.CommandText="SELECT filename,lastaltered,isdeleted,imagefolder FROM document,patient " 
				+"WHERE document.withpat=patient.patnum && lastaltered > '"+POut.PDateT(BackupJobs.Cur.LastRun)+"'"; 
			FillTable();
			ListBackup=new DocBackup[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				ListBackup[i].FileName   =PIn.PString(table.Rows[i][0].ToString());
				ListBackup[i].LastAltered=PIn.PDate  (table.Rows[i][1].ToString());
				ListBackup[i].IsDeleted  =PIn.PBool  (table.Rows[i][2].ToString());
				ListBackup[i].PatFolder  =PIn.PString(table.Rows[i][3].ToString());
			}
		}*/

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

		///<summary>When first opening the image module, this tests to see whether a given filename is in the database. Also used when naming a new document to ensure unique name.</summary>
		public static bool IsFileNameInList(string fileName,Document[] docList){
			for(int i=0;i<docList.Length;i++){
				if(docList[i].FileName==fileName)
					return true;
			}
			return false;
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
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			Hashtable hList=new Hashtable();//key=docNum, value=path
			//one row for each document, but in the wrong order
			for(int i=0;i<table.Rows.Count;i++){
				hList.Add(PIn.PInt(table.Rows[i][0].ToString()),
					((Pref)Prefs.HList["DocPath"]).ValueString
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
			for(int i=0;i<Defs.Short[(int)DefCat.ImageCats].Length;i++){
				if(Defs.Short[(int)DefCat.ImageCats][i].ItemValue=="P"){
					defNumPicts=Defs.Short[(int)DefCat.ImageCats][i].DefNum;
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
				+" ORDER BY DateCreated DESC LIMIT 1";//gets the most recent
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
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









