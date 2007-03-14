using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>An autocode automates entering procedures.  The user only has to pick composite, for instance, and the autocode figures out the code based on the number of surfaces, and posterior vs. anterior.  Autocodes also enforce and suggest changes to a procedure code if the number of surfaces or other properties change.</summary>
	public struct AutoCode{
		///<summary>Primary key.</summary>
		public int AutoCodeNum;
		///<summary>Displays meaningful decription, like "Amalgam".</summary>
		public string Description;
		///<summary>User can hide autocodes</summary>
		public bool IsHidden;
		///<summary>This will be true if user no longer wants to see this autocode message when closing a procedure. This makes it less intrusive, but it can still be used in procedure buttons.</summary>
		public bool LessIntrusive;
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
		///<summary>key=AutoCodeNum, value=AutoCode</summary>
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
				List[i].AutoCodeNum  = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description  = PIn.PString(table.Rows[i][1].ToString());
				List[i].IsHidden     = PIn.PBool  (table.Rows[i][2].ToString());	
				List[i].LessIntrusive= PIn.PBool  (table.Rows[i][3].ToString());	
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
			cmd.CommandText = "INSERT INTO autocode (Description,IsHidden,LessIntrusive) "
				+"VALUES ("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PBool  (Cur.IsHidden)+"', "
				+"'"+POut.PBool  (Cur.LessIntrusive)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.AutoCodeNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE autocode SET "
				+"Description='"      +POut.PString(Cur.Description)+"'"
				+",IsHidden = '"      +POut.PBool  (Cur.IsHidden)+"'"
				+",LessIntrusive = '" +POut.PBool  (Cur.LessIntrusive)+"'"
				+" WHERE autocodenum = '"+POut.PInt (Cur.AutoCodeNum)+"'";
			NonQ(false);
		}

		///<summary>This could be improved since it does not delete any autocode items.</summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from autocode WHERE autocodenum = '"+POut.PInt(Cur.AutoCodeNum)+"'";
			NonQ(false);
		}

		


	}

	


}









