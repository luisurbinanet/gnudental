using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the letter table in the database.</summary>
	public struct Letter{
		///<summary>Primary key.</summary>
		public int LetterNum;
		///<summary>Description of the Letter.</summary>
		public string Description;
		///<summary>Text of the letter</summary>
		public string BodyText;
	}
	
	/*=========================================================================================
		=================================== class Letters ===========================================*/
	///<summary>Letters are refreshed as local data.</summary>
	public class Letters:DataClass{
		///<summary>List of</summary>
		public static Letter[] List;
		///<summary></summary>
		public static Letter Cur;

		///<summary></summary>
		public static void Refresh(){
			//HList=new Hashtable();
			cmd.CommandText = 
				"SELECT * from letter ORDER BY Description";
			FillTable();
			//Employer temp=new Employer();
			List=new Letter[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].LetterNum  =PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description=PIn.PString(table.Rows[i][1].ToString());
				List[i].BodyText   =PIn.PString(table.Rows[i][2].ToString());
				//HList.Add(List[i].LetterNum,List[i]);
			}
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText="UPDATE letter SET "
				+ "Description= '" +POut.PString(Cur.Description)+"' "
				+ ",BodyText= '"   +POut.PString(Cur.BodyText)+"' "
				+"WHERE LetterNum = '"+POut.PInt(Cur.LetterNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ();
		}

		///<summary></summary>
		public static void InsertCur(){
			if(Prefs.RandomKeys){
				Cur.LetterNum=MiscData.GetKey("letter","LetterNum");
			}
			cmd.CommandText="INSERT INTO letter (";
			if(Prefs.RandomKeys){
				cmd.CommandText+="LetterNum,";
			}
			cmd.CommandText+="Description,BodyText) VALUES(";
			if(Prefs.RandomKeys){
				cmd.CommandText+="'"+POut.PInt(Cur.LetterNum)+"', ";
			}
			cmd.CommandText+=
				 "'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PString(Cur.BodyText)+"')";
			if(Prefs.RandomKeys){
				NonQ();
			}
			else{
 				NonQ(true);
				Cur.LetterNum=InsertID;
			}
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText="DELETE from letter WHERE LetterNum = '"+Cur.LetterNum.ToString()+"'";
			NonQ();
		}

		
	}

	
	

}













