using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Links documents (images) to patients.  This will allow one document to be shared between multiple patients in a future version.</summary>
	public struct DocAttach{
		///<summary>Primary key.</summary>
		public int DocAttachNum;
		///<summary>FK to patient.PatNum.</summary>
		public int PatNum;
		///<summary>FK to document.DocNum.</summary>
		public int DocNum;
		
	}

	/*==================================================================================================
	 =================================== Class DocAttaches ===========================================*/
	///<summary></summary>
	public class DocAttaches:DataClass{
		//public Break Cur;
		///<summary></summary>
		public static DocAttach Cur;
		///<summary></summary>
		public static DocAttach[] List;

		///<summary>For one patient. This should be followed by Documents.Refresh</summary>
		public static void Refresh(int patNum){
			cmd.CommandText="SELECT * from docattach WHERE PatNum = '"
				+patNum+"'";			//	MessageBox.Show(cmd.CommandText);			FillTable();//find all attachments for that patient
			List=new DocAttach[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				List[i].DocAttachNum =PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum       =PIn.PInt   (table.Rows[i][1].ToString());
				List[i].DocNum       =PIn.PInt   (table.Rows[i][2].ToString());
			}
			//	MessageBox.Show(List.Length.ToString());
		}

		///<summary></summary>
		public static void InsertCur(){
			if(Prefs.RandomKeys){
				Cur.DocAttachNum=MiscData.GetKey("docattach","DocAttachNum");
			}
			cmd.CommandText="INSERT INTO docattach (";
			if(Prefs.RandomKeys){
				cmd.CommandText+="DocAttachNum,";
			}
			cmd.CommandText+="PatNum, DocNum) VALUES(";
			if(Prefs.RandomKeys){
				cmd.CommandText+="'"+POut.PInt(Cur.DocAttachNum)+"', ";
			}
			cmd.CommandText+=
				 "'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PInt   (Cur.DocNum)+"')";
			if(Prefs.RandomKeys){
				NonQ();
			}
			else{
 				NonQ(true);
				Cur.DocAttachNum=InsertID;
			}
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE docattach SET " 
				+ ",PatNum = '"  +POut.PInt(Cur.PatNum)+"'"
				+ ",DocNum = '"  +POut.PInt(Cur.DocNum)+"'"
				+" WHERE DocAttachNum = '" +POut.PInt(Cur.DocAttachNum)+"'";
			NonQ(false);
		}
	
		
				
		/*public static void DeleteDocNum(string myDocNum){
			cmd.CommandText = "DELETE from docattach WHERE docnum = '"+myDocNum+"'";
			NonQ(false);
		}
*/
	}

	

	

}









