using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary>Handles database commands related to the definition table in the db.  This class is referenced frequently from many different areas of the program.  It stores data from the definition table in a couple of two dimensional arrays for immediate retrieval.  </summary>\
	public class Defs{
		///<summary>Stores all defs in a 2D array except the hidden ones.  The first dimension is the category, in int format.  The second dimension is the index of the definition in this category.  This is dependent on how it was refreshed, and not on what is in the database.  If you need to reference a specific def, then the DefNum is more effective.</summary>
		public static Def[][] Short;
		///<summary>Stores all defs in a 2D array.</summary>
		public static Def[][] Long;

		///<summary></summary>
		public static void Refresh(){
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					DefB.Refresh();
				}
				else {
					DtoDefRefresh dto=new DtoDefRefresh();
					DataSet ds=RemotingClient.ProcessQuery(dto);
					DefB.RawData=ds.Tables[0];
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
				return;
			}
			Long=new Def[Enum.GetValues(typeof(DefCat)).Length][];
			for(int j=0;j<Enum.GetValues(typeof(DefCat)).Length;j++){
				Long[j]=GetForCategory(j,true,DefB.RawData);
			}
			Short=new Def[Enum.GetValues(typeof(DefCat)).Length][];
			for(int j=0;j<Enum.GetValues(typeof(DefCat)).Length;j++){
				Short[j]=GetForCategory(j,false,DefB.RawData);
			}
		}

		///<summary>Used by the refresh method above.</summary>
		private static Def[] GetForCategory(int catIndex, bool includeHidden, DataTable table){
			List<Def> list=new List<Def>();
			Def def;
			for(int i=0;i<table.Rows.Count;i++) {
				if(PIn.PInt(table.Rows[i][1].ToString())!=catIndex){
					continue;
				}
				if(PIn.PBool(table.Rows[i][6].ToString())//if is hidden
					&& !includeHidden)//and we don't want to include hidden
				{
					continue;
				}
				def=new Def();
				def.DefNum    = PIn.PInt   (table.Rows[i][0].ToString());
				def.Category  = (DefCat)PIn.PInt(table.Rows[i][1].ToString());
				def.ItemOrder = PIn.PInt   (table.Rows[i][2].ToString());
				def.ItemName  = PIn.PString(table.Rows[i][3].ToString());
				def.ItemValue = PIn.PString(table.Rows[i][4].ToString());
				def.ItemColor = Color.FromArgb(PIn.PInt(table.Rows[i][5].ToString()));
				def.IsHidden  = PIn.PBool  (table.Rows[i][6].ToString());
				list.Add(def);
			}
			return list.ToArray();
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
		public static Def[] GetCatList(int myCat){
			string command=
				"SELECT * from definition"
				+" WHERE category = '"+myCat+"'"
				+" ORDER BY ItemOrder";
			DataSet ds=null;
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					ds=GeneralB.GetTable(command);
				}
				else {
					DtoGeneralGetTable dto=new DtoGeneralGetTable();
					dto.Command=command;
					ds=RemotingClient.ProcessQuery(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
			DataTable table=ds.Tables[0];
			Def[] List=new Def[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Def();
				List[i].DefNum    = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Category  = (DefCat)PIn.PInt   (table.Rows[i][1].ToString());
				List[i].ItemOrder = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].ItemName  = PIn.PString(table.Rows[i][3].ToString());
				List[i].ItemValue = PIn.PString(table.Rows[i][4].ToString());
				List[i].ItemColor = Color.FromArgb(PIn.PInt(table.Rows[i][5].ToString()));
				List[i].IsHidden  = PIn.PBool  (table.Rows[i][6].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static void Update(Def def) {
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					DefB.Update(def);
				}
				else {
					DtoDefUpdate dto=new DtoDefUpdate();
					dto.DefCur=def;
					RemotingClient.ProcessCommand(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
				return;
			}
		}

		///<summary></summary>
		public static void Insert(Def def) {
			int defNum;
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					defNum=DefB.Insert(def);
				}
				else {
					DtoDefInsert dto=new DtoDefInsert();
					dto.DefCur=def;
					defNum=RemotingClient.ProcessCommand(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
				return;
			}
			def.DefNum=defNum;			
		}

		///<summary></summary>
		public static void HideDef(Def def){
			def.IsHidden=true;
			Update(def);
		}

		///<summary>Returns the new selected.</summary>
		public static int MoveUp(bool isSelected,int selected,Def[] list){
			if(isSelected==false){
				MessageBox.Show(Lan.g("Defs","Please select an item first."));
				return selected;
			}
			if(selected==0){
				return selected;
			}
			SetOrder(selected-1,list[selected].ItemOrder,list);
			SetOrder(selected,list[selected].ItemOrder-1,list);
			selected-=1;
			return selected;
		}

		///<summary></summary>
		public static int MoveDown(bool isSelected,int selected,Def[] list){
			if(isSelected==false){
				MessageBox.Show(Lan.g("Defs","Please select an item first."));
				return selected;
			}
			if(selected==list.Length-1){
				return selected;
			}
			SetOrder(selected+1,list[selected].ItemOrder,list);
			SetOrder(selected,list[selected].ItemOrder+1,list);
			selected+=1;
			return selected;
		}

		///<summary></summary>
		private static void SetOrder(int mySelNum, int myItemOrder,Def[] list){
			Def def=list[mySelNum];
			def.ItemOrder=myItemOrder;
			//Cur=temp;
			Update(def);
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









