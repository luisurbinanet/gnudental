using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the document table in the database. It should be called image, but the name is for historical reasons.</summary>
	public struct Document{
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
	}

	/*=========================================================================================
	=================================== class Documents ==========================================*/

	///<summary>Handles images for the Images module</summary>
	public class Documents:DataClass{
		///<summary></summary>
		public static Document[] List;
		//public static DocBackup[] ListBackup;
		///<summary></summary>
		public static Document Cur;	

		///<summary>This is refreshed after DocAttaches.</summary>
		public static void Refresh(){
			if(DocAttaches.List.Length==0){
				List=new Document[0];
				return;
			}
			cmd.CommandText="SELECT * FROM document WHERE DocNum = '"+DocAttaches.List[0].DocNum.ToString()+"'";
			for(int i=1;i<DocAttaches.List.Length;i++){
				cmd.CommandText+=" || DocNum = '"+DocAttaches.List[i].DocNum.ToString()+"'";
			}
			cmd.CommandText+=" order by DateCreated";
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			List=new Document[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				List[i].DocNum        =PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description   =PIn.PString(table.Rows[i][1].ToString());
				List[i].DateCreated   =PIn.PDate  (table.Rows[i][2].ToString());
				List[i].DocCategory   =PIn.PInt   (table.Rows[i][3].ToString());
				List[i].WithPat       =PIn.PInt   (table.Rows[i][4].ToString());
				List[i].FileName      =PIn.PString(table.Rows[i][5].ToString());
				List[i].ImgType       =(ImageType)PIn.PInt(table.Rows[i][6].ToString());
				List[i].IsFlipped     =PIn.PBool  (table.Rows[i][7].ToString());
				List[i].DegreesRotated=PIn.PInt   (table.Rows[i][8].ToString());
				//List[i].LastAltered=PIn.PDate  (table.Rows[i][].ToString());
				//List[i].IsDeleted  =PIn.PBool  (table.Rows[i][].ToString());
			}
		}

		///<summary>Inserts a new document into db, creates a filename based on Cur.DocNum, and then updates the db with this filename.  Also attaches the document to the current patient.</summary>
		public static void InsertCur(Patient pat){
			cmd.CommandText = 
				"INSERT INTO document (Description,DateCreated,DocCategory,"
				+"WithPat,FileName,ImgType,IsFlipped,DegreesRotated) VALUES ("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PDate  (Cur.DateCreated)+"', "
				+"'"+POut.PInt   (Cur.DocCategory)+"', "
				+"'"+POut.PInt   (Cur.WithPat)+"', "
				+"'"+POut.PString(Cur.FileName)+"', "//this may simply be the extension at this point, or it may be the full filename.
				+"'"+POut.PInt   ((int)Cur.ImgType)+"', "
				+"'"+POut.PBool  (Cur.IsFlipped)+"', "
				+"'"+POut.PInt   (Cur.DegreesRotated)+"')";
				/*+"'"+POut.PDate  (Cur.LastAltered)+"', "//will later be used in backups
				  +"'"+POut.PBool  (Cur.IsDeleted)+"')";//ditto*/
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.DocNum=InsertID;
			//If the current filename is just an extension, then assign it a unique name.
			if(Cur.FileName==Path.GetExtension(Cur.FileName)){
				string extension=Cur.FileName;
				Cur.FileName="";
				string s=pat.LName+pat.FName;
				for(int i=0;i<s.Length;i++){
					if(Char.IsLetter(s,i)){
						Cur.FileName+=s.Substring(i,1);
					}
				}
				Cur.FileName+=Cur.DocNum.ToString()+extension;//ensures unique name
				//there is still a slight chance that someone manually added a file with this name, so quick fix:
				while(IsFileNameInList(Cur.FileName)){
					Cur.FileName="x"+Cur.FileName;
				}
				UpdateCur();
			}
			DocAttaches.Cur=new DocAttach();
			DocAttaches.Cur.DocNum=Cur.DocNum;
			DocAttaches.Cur.PatNum=pat.PatNum;
			DocAttaches.InsertCur();
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE document SET " 
				+ "Description = '"      +POut.PString(Cur.Description)+"'"
				+ ",DateCreated = '"     +POut.PDate  (Cur.DateCreated)+"'"
				+ ",DocCategory = '"     +POut.PInt   (Cur.DocCategory)+"'"
				+ ",WithPat = '"         +POut.PInt   (Cur.WithPat)+"'"
				+ ",FileName    = '"     +POut.PString(Cur.FileName)+"'"
				+ ",ImgType    = '"      +POut.PInt   ((int)Cur.ImgType)+"'"
				+ ",IsFlipped   = '"     +POut.PBool  (Cur.IsFlipped)+"'"
				+ ",DegreesRotated   = '"+POut.PInt   (Cur.DegreesRotated)+"'"
				//+ ",LastAltered= '"     +POut.PDate  (Cur.LastAltered)+"'"
				//+ ",IsDeleted = '"      +POut.PBool  (Cur.IsDeleted)+"'"
				+" WHERE DocNum = '"    +POut.PInt(Cur.DocNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ();			
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
		public static void GetCurrent(string docNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].DocNum.ToString()==docNum){
					Cur=List[i];
				}
			}
		}

		///<summary>Used when dragging to a new category. Loops through List to find the defNum of the category based on docNum.</summary>
		public static int GetCategory(string docNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].DocNum.ToString()==docNum){
					return List[i].DocCategory;
				}
			}
			return -1;
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from document WHERE DocNum = '"+Cur.DocNum.ToString()+"'";
			NonQ();
			cmd.CommandText = "DELETE from docattach WHERE DocNum = '"+Cur.DocNum.ToString()+"'";
			NonQ();
		}

		///<summary>When first openning the image module, this tests to see whether a given filename is in the database. Also used when naming a new document to ensure unique name.</summary>
		public static bool IsFileNameInList(string fileName){
			for(int i=0;i<List.Length;i++){
				if(List[i].FileName==fileName)
					return true;
			}
			return false;
		}

		///<summary>This is used by FormImageViewer to get a list of paths based on supplied list of DocNums. The reason is that later we will allow sharing of documents, so the paths may not be in the current patient folder.</summary>
		public static ArrayList GetPaths(ArrayList docNums){
			if(docNums.Count==0){
				return new ArrayList();
			}
			cmd.CommandText="SELECT document.DocNum,document.FileName,patient.ImageFolder "
				+"FROM document "
				+"LEFT JOIN patient ON patient.PatNum=document.WithPat "
				+"WHERE document.DocNum = '"+docNums[0].ToString()+"'";
			for(int i=1;i<docNums.Count;i++){
				cmd.CommandText+=" || document.DocNum = '"+docNums[i].ToString()+"'";
			}
			//remember, they will not be in the correct order.
			//MessageBox.Show(cmd.CommandText);
			FillTable();
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


	}

	
  
	/*public struct DocBackup{
		public string FileName;
		public DateTime LastAltered;
		public bool IsDeleted;
		public string PatFolder;
	}*/


}









