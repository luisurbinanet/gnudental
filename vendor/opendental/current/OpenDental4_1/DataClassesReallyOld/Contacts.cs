using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the contact table in the database.</summary>
	public struct Contact{
		///<summary>Primary key.</summary>
		public int ContactNum;
		///<summary>Last name or, frequently, the entire name.</summary>
		public string LName;
		///<summary>First name is optional.</summary>
		public string FName;
		///<summary>Work phone.</summary>
		public string WkPhone;
		///<summary>Fax number.</summary>
		public string Fax;
		///<summary>Foreign key to definition.DefNum</summary>
		public int Category;
		///<summary>Note for this contact.</summary>
		public string Notes;
	}

	/*=========================================================================================
		=================================== class Contacts ==========================================*/

	///<summary></summary>
	public class Contacts:DataClass{
		///<summary></summary>
		public static Contact Cur;
		///<summary></summary>
		public static Contact[] List;//for one category only. Not refreshed with local data
		//public static Contact[] ListForCat;

		///<summary></summary>
		public static void Refresh(int category){
			cmd.CommandText =
				"SELECT * from contact WHERE category = '"+category+"'"
				+" ORDER BY LName";
			FillTable();
			List = new Contact[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i].ContactNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].LName      = PIn.PString(table.Rows[i][1].ToString());
				List[i].FName      = PIn.PString(table.Rows[i][2].ToString());
				List[i].WkPhone    = PIn.PString(table.Rows[i][3].ToString());
				List[i].Fax        = PIn.PString(table.Rows[i][4].ToString());
				List[i].Category   = PIn.PInt   (table.Rows[i][5].ToString());
				List[i].Notes      = PIn.PString(table.Rows[i][6].ToString());
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			if(Prefs.RandomKeys){
				Cur.ContactNum=MiscData.GetKey("contact","ContactNum");
			}
			cmd.CommandText="INSERT INTO contact (";
			if(Prefs.RandomKeys){
				cmd.CommandText+="ContactNum,";
			}
			cmd.CommandText+="LName,FName,WkPhone,Fax,Category,"
				+"Notes) VALUES(";
			if(Prefs.RandomKeys){
				cmd.CommandText+="'"+POut.PInt(Cur.ContactNum)+"', ";
			}
			cmd.CommandText+=
				 "'"+POut.PString(Cur.LName)+"', "
				+"'"+POut.PString(Cur.FName)+"', "
				+"'"+POut.PString(Cur.WkPhone)+"', "
				+"'"+POut.PString(Cur.Fax)+"', "
				+"'"+POut.PInt   (Cur.Category)+"', "
				+"'"+POut.PString(Cur.Notes)+"')";
			if(Prefs.RandomKeys){
				NonQ();
			}
			else{
 				NonQ(true);
				Cur.ContactNum=InsertID;
			}
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE contact SET "
				+"lname = '"    +POut.PString(Cur.LName)+"' "
				+",fname = '"   +POut.PString(Cur.FName)+"' "
				+",wkphone = '" +POut.PString(Cur.WkPhone)+"' "
				+",fax = '"     +POut.PString(Cur.Fax)+"' "
				+",category = '"+POut.PInt   (Cur.Category)+"' "
				+",notes = '"   +POut.PString(Cur.Notes)+"' "
				+"WHERE contactnum = '"+POut.PInt  (Cur.ContactNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM contact WHERE contactnum = '"+Cur.ContactNum.ToString()+"'";
			NonQ(false);
		}

	}

	
}