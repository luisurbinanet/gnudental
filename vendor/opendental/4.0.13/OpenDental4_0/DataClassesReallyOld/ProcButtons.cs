using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the procbutton table in the database.</summary>
	public struct ProcButton{
		///<summary>Primary key</summary>
		public int ProcButtonNum;
		///<summary>The text to show on the button.</summary>
		public string Description;
		///<summary>Order that they will show in the Chart module.</summary>
		public int ItemOrder;
	}

	/*=========================================================================================
		=================================== class ProcButtons===========================================*/

	///<summary></summary>
	public class ProcButtons:DataClass{
		///<summary></summary>
		public static ProcButton Cur;
		///<summary></summary>
		public static Hashtable HList;
		///<summary></summary>
		public static ProcButton[] List;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText = 
				"SELECT * from procbutton "
				+"ORDER BY itemorder";
			FillTable();
			HList=new Hashtable();
			List=new ProcButton[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				List[i].ProcButtonNum=PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description  =PIn.PString(table.Rows[i][1].ToString());
				List[i].ItemOrder    =PIn.PInt   (table.Rows[i][2].ToString());
				//List[i].IsFourQuad   =PIn.PBool  (table.Rows[i][2].ToString());
				HList.Add(List[i].ProcButtonNum,List[i]);
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			//must have already checked ADACode for nonduplicate.
			cmd.CommandText = "INSERT INTO procbutton (description,itemorder) VALUES("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PInt   (Cur.ItemOrder)+"')";
				//+"'"+POut.PBool  (Cur.IsFourQuad)+"')";
			NonQ(true);
			Cur.ProcButtonNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE procbutton SET " 
				+ "description = '" +POut.PString(Cur.Description)+"'"
				+ ",itemorder = '"  +POut.PInt   (Cur.ItemOrder)+"'"  
				//+ ",isfourquad = '" +POut.PBool  (Cur.IsFourQuad)+"'"     
				+" WHERE procbuttonnum = '"+POut.PInt(Cur.ProcButtonNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from procbuttonitem WHERE procbuttonnum = '"
				+POut.PInt(Cur.ProcButtonNum)+"'";
			NonQ(false);
			cmd.CommandText = "DELETE from procbutton WHERE procbuttonnum = '"
				+POut.PInt(Cur.ProcButtonNum)+"'";
			NonQ(false);
		}

	}

	

	


}










