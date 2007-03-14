using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental {

	///<summary>Insurance coverage categories.  They need to look like in the manual for the American calculations to work properly.</summary>
	public class CovCat {
		///<summary>Primary key.</summary>
		public int CovCatNum;
		///<summary>Description of this category.</summary>
		public string Description;
		///<summary>Default percent for this category. -1 to skip this category and not apply a percentage.</summary>
		public int DefaultPercent;
		///<summary>The order in which the categories are displayed.  Includes hidden categories. 0-based.</summary>
		public int CovOrder;
		///<summary>If true, this category will be hidden.</summary>
		public bool IsHidden;
		///<summary>Enum:EbenefitCategory  The X12 benefit categories.  Each CovCat can link to one X12 category.  Default is 0 (unlinked).</summary>
		public EbenefitCategory EbenefitCat;

		///<summary></summary>
		public CovCat Copy() {
			CovCat c=new CovCat();
			c.CovCatNum=CovCatNum;
			c.Description=Description;
			c.DefaultPercent=DefaultPercent;
			c.CovOrder=CovOrder;
			c.IsHidden=IsHidden;
			c.EbenefitCat=EbenefitCat;
			return c;
		}

		///<summary></summary>
		public void Update() {
			string command= "UPDATE covcat SET "
				+ "Description = '"    +POut.PString(Description)+"'"
				+",DefaultPercent = '" +POut.PInt   (DefaultPercent)+"'"
				+",CovOrder = '"       +POut.PInt   (CovOrder)+"'"
				+",IsHidden = '"       +POut.PBool  (IsHidden)+"'"
				+",EbenefitCat = '"    +POut.PInt((int)EbenefitCat)+"'"
				+" WHERE covcatnum = '"+POut.PInt(CovCatNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Insert() {
			string command="INSERT INTO covcat (Description,DefaultPercent,"
				+"CovOrder,IsHidden,EbenefitCat) VALUES("
				+"'"+POut.PString(Description)+"', "
				+"'"+POut.PInt(DefaultPercent)+"', "
				+"'"+POut.PInt(CovOrder)+"', "
				+"'"+POut.PBool(IsHidden)+"', "
				+"'"+POut.PInt((int)EbenefitCat)+"')";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void MoveUp() {
			CovCats.Refresh();
			int oldOrder=CovCats.GetOrderLong(CovCatNum);
			if(oldOrder==0) {
				return;
			}
			CovCats.List[oldOrder].SetOrder(oldOrder-1);
			CovCats.List[oldOrder-1].SetOrder(oldOrder);
		}

		///<summary></summary>
		public void MoveDown() {
			CovCats.Refresh();
			int oldOrder=CovCats.GetOrderLong(CovCatNum);
			if(oldOrder==CovCats.List.Length-1) {
				return;
			}
			CovCats.List[oldOrder].SetOrder(oldOrder+1);
			CovCats.List[oldOrder+1].SetOrder(oldOrder);
		}

		///<summary></summary>
		private void SetOrder(int newOrder) {
			CovOrder=newOrder;
			Update();
		}

	}

	/*=========================================================================================
	=================================== class CovCats ==========================================*/

	///<summary></summary>
	public class CovCats {
		///<summary>All CovCats</summary>
		public static CovCat[] List;
		///<summary>Only CovCats that are not hidden.</summary>
		public static CovCat[] ListShort;

		///<summary></summary>
		public static void Refresh() {
			string command=
				"SELECT * from covcat"
				+" ORDER BY covorder";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			List=new CovCat[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new CovCat();
				List[i].CovCatNum     =PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description   =PIn.PString(table.Rows[i][1].ToString());
				List[i].DefaultPercent=PIn.PInt   (table.Rows[i][2].ToString());
				List[i].CovOrder      =PIn.PInt   (table.Rows[i][3].ToString());
				List[i].IsHidden      =PIn.PBool  (table.Rows[i][4].ToString());
				List[i].EbenefitCat   =(EbenefitCategory)PIn.PInt(table.Rows[i][5].ToString());
			}
			command=
				"SELECT * FROM covcat"
				+" WHERE IsHidden = 0"
				+" ORDER BY CovOrder";
			table=dcon.GetTable(command);
			ListShort=new CovCat[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				ListShort[i]=new CovCat();
				ListShort[i].CovCatNum     =PIn.PInt   (table.Rows[i][0].ToString());
				ListShort[i].Description   =PIn.PString(table.Rows[i][1].ToString());
				ListShort[i].DefaultPercent=PIn.PInt   (table.Rows[i][2].ToString());
				ListShort[i].CovOrder      =PIn.PInt   (table.Rows[i][3].ToString());
				ListShort[i].IsHidden      =PIn.PBool  (table.Rows[i][4].ToString());
				ListShort[i].EbenefitCat   =(EbenefitCategory)PIn.PInt(table.Rows[i][5].ToString());
			}
		}

		///<summary></summary>
		public static int GetCatNum(string myADACode){
			return 0;
		}

		///<summary></summary>
		public static CovCat GetCovCat(int covCatNum){
			for(int i=0;i<List.Length;i++) {
				if(covCatNum==List[i].CovCatNum) {
					return List[i].Copy();
				}
			}
			return null;//won't happen	
		}
		
		///<summary></summary>
		public static double GetDefaultPercent(int myCovCatNum){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(myCovCatNum==List[i].CovCatNum){
					retVal=(double)List[i].DefaultPercent;
				}
			}
			return retVal;	
		}

		///<summary></summary>
		public static string GetDesc(int covCatNum){
			string retStr="";
			for(int i=0;i<List.Length;i++){
				if(covCatNum==List[i].CovCatNum){
					retStr=List[i].Description;
				}
			}
			return retStr;	
		}

		///<summary></summary>
		public static int GetCovCatNum(int orderShort){
			//need to check this again:
			int retVal=0;
			for(int i=0;i<ListShort.Length;i++){
				if(orderShort==ListShort[i].CovOrder){
					retVal=ListShort[i].CovCatNum;
				}
			}
			return retVal;	
		}

		///<summary></summary>
		public static int GetOrderShort(int CovCatNum){
			int retVal=-1;
			for(int i=0;i<ListShort.Length;i++){
				if(CovCatNum==ListShort[i].CovCatNum){
					retVal=i;
				}
			}
			return retVal;	
		}

		///<summary></summary>
		public static int GetOrderLong(int covCatNum) {
			for(int i=0;i<List.Length;i++) {
				if(covCatNum==List[i].CovCatNum) {
					return i;
				}
			}
			return -1;
		}	

		///<summary>Gets a matching benefit category from the short list.  Returns null if not found, which should be tested for.</summary>
		public static CovCat GetForEbenCat(EbenefitCategory eben){
			for(int i=0;i<ListShort.Length;i++) {
				if(eben==ListShort[i].EbenefitCat) {
					return ListShort[i];
				}
			}
			return null;
		}

		

	}

	



}









