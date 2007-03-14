using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the docattach table in the database.</summary>
	///<remarks>Links documents (images) to patients.  This will allow one document to be shared between multiple patients in a future version.</remarks>
	public struct DocAttach{
		///<summary>Primary key.</summary>
		public int DocAttachNum;
		///<summary>Foreign key to patient.PatNum.</summary>
		public int PatNum;
		///<summary>Foreign key document.DocNum.</summary>
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

		///<summary>This should be followed by Documents.Refresh</summary>
		public static void Refresh(){
			cmd.CommandText="SELECT * from docattach WHERE PatNum = '"
				+Patients.Cur.PatNum+"'";			//	MessageBox.Show(cmd.CommandText);			FillTable();//find all attachments for that patient
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
			cmd.CommandText = "INSERT INTO docattach (PatNum, DocNum) VALUES ("
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PInt   (Cur.DocNum)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ();
			//Cur.DocAttachNum=InsertID;
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









