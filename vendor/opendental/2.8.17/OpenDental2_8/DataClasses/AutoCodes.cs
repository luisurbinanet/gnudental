using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the autocode table in the database.</summary>
	public struct AutoCode{
		///<summary>Primary key.</summary>
		public int AutoCodeNum;
		///<summary>Displays meaningful decription, like "Amalgam".</summary>
		public string Description;
		///<summary>User can hide autocodes</summary>
		public bool IsHidden;
	}	

	/*=========================================================================================
	=================================== class AutoCodes ===========================================*/

	///<summary></summary>
	public class AutoCodes:DataClass{
		///<summary></summary>
		public static AutoCode Cur;
		///<summary></summary>
		public static AutoCode[] List;
		///<summary></summary>
		public static AutoCode[] ListShort;
		///<summary></summary>
		public static Hashtable HList; 

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from autocode";
			FillTable();
			HList=new Hashtable();
			List=new AutoCode[table.Rows.Count];
			ArrayList ALshort=new ArrayList();//int of indexes of short list
			for(int i = 0;i<List.Length;i++){
				List[i].AutoCodeNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description= PIn.PString(table.Rows[i][1].ToString());
				List[i].IsHidden   = PIn.PBool  (table.Rows[i][2].ToString());	
				HList.Add(List[i].AutoCodeNum,List[i]);
				if(!List[i].IsHidden){
					ALshort.Add(i);
				}
			}
			ListShort=new AutoCode[ALshort.Count];
			for(int i=0;i<ALshort.Count;i++){
				ListShort[i]=List[(int)ALshort[i]];
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO autocode (description,ishidden) "
				+"VALUES ("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PBool  (Cur.IsHidden)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.AutoCodeNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE autocode SET "
				+"description='"+POut.PString(Cur.Description)+"'"
				+",ishidden = '" +POut.PBool  (Cur.IsHidden)+"'"
				+" WHERE autocodenum = '"+POut.PInt (Cur.AutoCodeNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from autocode WHERE autocodenum = '"+POut.PInt(Cur.AutoCodeNum)+"'";
			NonQ(false);
		}

	}

	


}









