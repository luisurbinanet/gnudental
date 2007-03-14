using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the document table in the database.</summary>
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
		///<summary>The name of the file.</summary>
		public string FileName;
		//public DateTime LastAltered;
		//public bool IsDeleted;
	}

	/*=========================================================================================
	=================================== class Documents ==========================================*/

	///<summary></summary>
	public class Documents:DataClass{
		///<summary></summary>
		public static Document[] List;
		//public static DocBackup[] ListBackup;
		///<summary></summary>
		public static Document Cur;	

		///<summary></summary>
		public static void Refresh(){
			if(DocAttaches.List.Length==0){
				List=new Document[0];
				return;
			}
			cmd.CommandText="SELECT * FROM document WHERE docnum = '"+DocAttaches.List[0].DocNum.ToString()+"'";
			for(int i=1;i<DocAttaches.List.Length;i++){
				cmd.CommandText+=" || docnum = '"+DocAttaches.List[i].DocNum.ToString()+"'";
			}
			cmd.CommandText+=" order by datecreated";
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			List=new Document[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				List[i].DocNum     =PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description=PIn.PString(table.Rows[i][1].ToString());
				List[i].DateCreated=PIn.PDate  (table.Rows[i][2].ToString());
				List[i].DocCategory=PIn.PInt   (table.Rows[i][3].ToString());
				List[i].WithPat    =PIn.PInt   (table.Rows[i][4].ToString());
				List[i].FileName   =PIn.PString(table.Rows[i][5].ToString());
				//List[i].LastAltered=PIn.PDate  (table.Rows[i][6].ToString());
				//List[i].IsDeleted  =PIn.PBool  (table.Rows[i][7].ToString());
			}
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

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from document WHERE docnum = '"+Cur.DocNum.ToString()+"'";
			NonQ(false);
			cmd.CommandText = "DELETE from docattach WHERE docnum = '"+Cur.DocNum.ToString()+"'";
			NonQ(false);
		}

		///<summary>Inserts a new document into db, creates a filename based on Cur.DocNum, and then updates the db with this filename.  Also attaches the document to the current patient.</summary>
		public static void InsertCur(){
			cmd.CommandText = 
				"INSERT INTO document (Description,DateCreated,DocCategory,"
				+"WithPat,FileName) VALUES ("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PDate  (Cur.DateCreated)+"', "
				+"'"+POut.PInt   (Cur.DocCategory)+"', "
				+"'"+POut.PInt   (Cur.WithPat)+"', "
				+"'"+POut.PString(Cur.FileName)+"')";//this may simply be the extension at this point, or it may be the full filename.				
				/*+"'"+POut.PDate  (Cur.LastAltered)+"', "//will later be used in backups
				  +"'"+POut.PBool  (Cur.IsDeleted)+"')";//ditto*/
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.DocNum=InsertID;
			//If the current filename is just an extension, then assign it a unique name.
			if(Cur.FileName==Path.GetExtension(Cur.FileName)){
				string extension=Cur.FileName;
				Cur.FileName="";
				string s=Patients.Cur.LName+Patients.Cur.FName;
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
			DocAttaches.Cur.PatNum=Patients.Cur.PatNum;
			DocAttaches.InsertCur();
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE document SET " 
				+ "Description = '"     +POut.PString(Cur.Description)+"'"
				+ ",DateCreated = '"    +POut.PDate  (Cur.DateCreated)+"'"
				+ ",DocCategory = '"    +POut.PInt   (Cur.DocCategory)+"'"
				+ ",WithPat = '"        +POut.PInt   (Cur.WithPat)+"'"
				+ ",FileName    = '"    +POut.PString(Cur.FileName)+"'"
				//+ ",LastAltered= '"     +POut.PDate  (Cur.LastAltered)+"'"
				//+ ",IsDeleted = '"      +POut.PBool  (Cur.IsDeleted)+"'"
				+" WHERE DocNum = '"    +POut.PInt(Cur.DocNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);			
		}

		///<summary>When first openning the image module, this tests to see whether a given filename is in the database. Also used when naming a new document to ensure unique name.</summary>
		public static bool IsFileNameInList(string fileName){
			for(int i=0;i<List.Length;i++){
				if(List[i].FileName==fileName)
					return true;
			}
			return false;
		}

	}

	
  
	/*public struct DocBackup{
		public string FileName;
		public DateTime LastAltered;
		public bool IsDeleted;
		public string PatFolder;
	}*/


}









