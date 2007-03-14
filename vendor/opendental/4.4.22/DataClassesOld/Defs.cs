using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>The info in the definition table is used by other tables extensively.  Almost every table in the database links to definition.  Almost all links to this table will be to a DefNum.  Using the DefNum, you can find any of the other fields of interest, usually the ItemName.  Make sure to look at the Defs class to see how the definitions are used.  Loaded into memory ahead of time for speed.  Some types of info such as operatories started out life in this table, but then got moved to their own table when more complexity was needed.</summary>
	public struct Def{
		///<summary>Primary key.</summary>
		public int DefNum;
		///<summary>Enum:DefCat</summary>
		public DefCat Category;
		///<summary>Order that each item shows on various lists.</summary>
		public int ItemOrder;
		///<summary>Each category is a little different.  This field is usually the common name of the item.</summary>
		public string ItemName;
		///<summary>This field can be used to store extra info about the item.</summary>
		public string ItemValue;
		///<summary>Some categories include a color option.</summary>
		public Color ItemColor;
		///<summary>If hidden, the item will not show on any list, but can still be referenced.</summary>
		public bool IsHidden;
	}

	/*=========================================================================================
	=================================== class Defs ==========================================*/

	///<summary>Handles database commands related to the definition table in the db.</summary>\
	///<remarks>This class is referenced frequently from many different areas of the program.  It stores data from the definition table in a couple of two dimensional arrays for immediate retrieval.  </remarks>
	public class Defs:DataClass{
		///<summary>For only one category. Only used in FormDefinitions.</summary>
		public static Def[] List;
		///<summary>Current definition.</summary>
		public static Def Cur;
		///<summary>Only used in the defs window</summary>
		public static bool IsSelected;
		///<summary>Keeps track of the selected row in the defs window.</summary>
		public static int Selected;
		///<summary>Stores all defs in a 2D array except the hidden ones.</summary>
		///<remarks>The first dimension is the category, in int format.  The second dimension is the index of the definition in this category.  This is dependent on how it was refreshed, and not on what is in the database.  If you need to reference a specific def, then the DefNum is more effective.</remarks>
		public static Def[][] Short;
		///<summary>Stores all defs in a 2D array.</summary>
		public static Def[][] Long;

		///<summary></summary>
		public static void Refresh(){
			Long=new Def[Enum.GetValues(typeof(DefCat)).Length][];
			for(int j=0;j<Enum.GetValues(typeof(DefCat)).Length;j++){
				cmd.CommandText =
					"SELECT * from definition"
					+" WHERE category = '"+j+"'"
					+" ORDER BY ItemOrder";
				FillTable();
				Long[j]=new Def[table.Rows.Count];
				for(int i=0;i<table.Rows.Count;i++){
					Long[j][i].DefNum    = PIn.PInt   (table.Rows[i][0].ToString());
					Long[j][i].Category  = (DefCat)PIn.PInt   (table.Rows[i][1].ToString());
					Long[j][i].ItemOrder = PIn.PInt   (table.Rows[i][2].ToString());
					Long[j][i].ItemName  = PIn.PString(table.Rows[i][3].ToString());
					Long[j][i].ItemValue = PIn.PString(table.Rows[i][4].ToString());
					Long[j][i].ItemColor = Color.FromArgb(PIn.PInt(table.Rows[i][5].ToString()));
					Long[j][i].IsHidden  = PIn.PBool  (table.Rows[i][6].ToString());
				}
			}//end for j
			//MessageBox.Show(Long[(int)DefCat.ApptConfirmed].Length.ToString());
			Short=new Def[Enum.GetValues(typeof(DefCat)).Length][];
			for(int j=0;j<Enum.GetValues(typeof(DefCat)).Length;j++){
				cmd.CommandText =
					"SELECT * from definition"
					+" WHERE category = '"+j+"'"
					+" AND IsHidden = 0"
					+" ORDER BY ItemOrder";
				FillTable();
				Short[j]=new Def[table.Rows.Count];
				for(int i=0;i<table.Rows.Count;i++){
					Short[j][i].DefNum    = PIn.PInt   (table.Rows[i][0].ToString());
					Short[j][i].Category  = (DefCat)PIn.PInt   (table.Rows[i][1].ToString());
					Short[j][i].ItemOrder = PIn.PInt   (table.Rows[i][2].ToString());
					Short[j][i].ItemName  = PIn.PString(table.Rows[i][3].ToString());
					Short[j][i].ItemValue = PIn.PString(table.Rows[i][4].ToString());
					Short[j][i].ItemColor = Color.FromArgb(PIn.PInt(table.Rows[i][5].ToString()));
					Short[j][i].IsHidden  = PIn.PBool  (table.Rows[i][6].ToString());
				}
			}//end for j
			//MessageBox.Show(Short[(int)DefCat.ApptConfirmed].Length.ToString());
		}

		///<summary></summary>
		public static string GetName(DefCat myCat, int myDefNum){
			//return "x";
			//string retStr="";
			for(int i=0;i<Long[(int)myCat].GetLength(0);i++){
				if(Long[(int)myCat][i].DefNum==myDefNum){
					return Long[(int)myCat][i].ItemName;
				}
			}
			return "";
			//return retStr;
		}

		///<summary>Gets the order of the def within Short or -1 if not found.</summary>
		public static int GetOrder(DefCat myCat,int myDefNum){
			//gets the index in the list of unhidden (the Short list).
			for(int i=0;i<Short[(int)myCat].GetLength(0);i++){
				if(Short[(int)myCat][i].DefNum==myDefNum){
					return i;
				}
			}
			return -1;
		}

		///<summary></summary>
		public static string GetValue(DefCat myCat, int myDefNum){
			string retStr="";
			for(int i=0;i<Long[(int)myCat].GetLength(0);i++){
				if(Long[(int)myCat][i].DefNum==myDefNum){
					retStr=Long[(int)myCat][i].ItemValue;
				}
			}
			return retStr;
		}

		///<summary></summary>
		public static Color GetColor(DefCat myCat, int myDefNum){
			Color retCol=Color.White;
			for(int i=0;i<Long[(int)myCat].GetLength(0);i++){
				if(Long[(int)myCat][i].DefNum==myDefNum){
					retCol=Long[(int)myCat][i].ItemColor;
				}
			}
			return retCol;
		}

		///<summary></summary>
		public static bool GetHidden(DefCat myCat,int myDefNum) {
			for(int i=0;i<Long[(int)myCat].GetLength(0);i++) {
				if(Long[(int)myCat][i].DefNum==myDefNum) {
					return Long[(int)myCat][i].IsHidden;
				}
			}
			return false;
		}

		///<summary></summary>
		public static void GetCatList(int myCat){
			cmd.CommandText =
				"SELECT * from definition"
				+" WHERE category = '"+myCat+"'"
				+" ORDER BY ItemOrder";
			FillTable();
			List=new Def[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].DefNum    = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Category  = (DefCat)PIn.PInt   (table.Rows[i][1].ToString());
				List[i].ItemOrder = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].ItemName  = PIn.PString(table.Rows[i][3].ToString());
				List[i].ItemValue = PIn.PString(table.Rows[i][4].ToString());
				List[i].ItemColor = Color.FromArgb(PIn.PInt(table.Rows[i][5].ToString()));
				List[i].IsHidden  = PIn.PBool  (table.Rows[i][6].ToString());
			}//end for
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE definition SET "
				+ "category = '"  +POut.PInt   ((int)Cur.Category)+"'"
				+",itemorder = '" +POut.PInt   (Cur.ItemOrder)+"'"
				+",itemname = '"  +POut.PString(Cur.ItemName)+"'"
				+",itemvalue = '" +POut.PString(Cur.ItemValue)+"'"
				+",itemcolor = '" +POut.PInt   (Cur.ItemColor.ToArgb())+"'"
				+",ishidden = '"  +POut.PBool  (Cur.IsHidden)+"'"
				+" WHERE defnum = '"+POut.PInt(Cur.DefNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO definition (category,itemorder,"
				+"itemname,itemvalue,itemcolor,ishidden) VALUES("
				+"'"+POut.PInt   ((int)Cur.Category)+"', "
				+"'"+POut.PInt   (Cur.ItemOrder)+"', "
				+"'"+POut.PString(Cur.ItemName)+"', "
				+"'"+POut.PString(Cur.ItemValue)+"', "
				+"'"+POut.PInt   (Cur.ItemColor.ToArgb())+"', "
				+"'"+POut.PBool  (Cur.IsHidden)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Defs.Cur.DefNum=InsertID;//used in conversion
		}

		///<summary></summary>
		public static void HideDef(){//hides Selected
			Cur=List[Selected];
			Cur.IsHidden=true;
			UpdateCur();
		}

		///<summary></summary>
		public static void MoveUp(){
			if(IsSelected==false){
				MessageBox.Show(Lan.g("Defs","Please select an item first."));
				return;
			}
			if(Selected==0){
				return;
			}
			SetOrder(Selected-1,List[Selected].ItemOrder);
			SetOrder(Selected,List[Selected].ItemOrder-1);
			Selected-=1;
		}//end MoveUp

		///<summary></summary>
		public static void MoveDown(){
			if(IsSelected==false){
				MessageBox.Show(Lan.g("Defs","Please select an item first."));
				return;
			}
			if(Selected==List.Length-1){
				return;
			}
			SetOrder(Selected+1,List[Selected].ItemOrder);
			SetOrder(Selected,List[Selected].ItemOrder+1);
			Selected+=1;
		}

		///<summary></summary>
		public static void SetOrder(int mySelNum, int myItemOrder){
			//Preference temp=new Preference();
			//for(int i=0;i<List.Length;i++){
			//	if(List[i].PrefNum==myPrefNum)
			//		temp=List[i];
			//}
			Def temp=List[mySelNum];
			temp.ItemOrder=myItemOrder;
			Cur=temp;
			UpdateCur();
		}

		///<summary>Allowed types are blank, C, or A</summary>
		public static Def[] GetFeeSchedList(string type){
			ArrayList AL=new ArrayList();
			for(int i=0;i<Short[(int)DefCat.FeeSchedNames].Length;i++){
				if(Short[(int)DefCat.FeeSchedNames][i].ItemValue==type){
					AL.Add(Short[(int)DefCat.FeeSchedNames][i]);
				}
			}
			Def[] retVal=new Def[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

	}

	

	

}









