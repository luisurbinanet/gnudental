using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the covcat table in the database.</summary>
	public struct CovCat{
		///<summary>Primary key.</summary>
		public int CovCatNum;
		///<summary>Description of this category.</summary>
		public string Description;
		///<summary>Default percent for this category.</summary>
		public int DefaultPercent;
		///<summary>True if this is a preventive category.</summary>
		public bool IsPreventive;
		///<summary>The order in which the categories are displayed.</summary>
		public int CovOrder;
		///<summary>If true, this category will be hidden.</summary>
		public bool IsHidden;
	}

	/*=========================================================================================
	=================================== class CovCats ==========================================*/

	///<summary></summary>
	public class CovCats:DataClass{
		///<summary></summary>
		public static CovCat[] List;
		///<summary></summary>
		public static CovCat[] ListShort;
		///<summary></summary>
		public static CovCat Cur;
		///<summary></summary>
		public static int Selected;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from covcat"
				//+" WHERE "+s
				+" ORDER BY covorder";
			FillTable();
			//MessageBox.Show(cmd.CommandText);
			List=new CovCat[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].CovCatNum     = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description   = PIn.PString(table.Rows[i][1].ToString());
				List[i].DefaultPercent= PIn.PInt   (table.Rows[i][2].ToString());
				List[i].IsPreventive  = PIn.PBool  (table.Rows[i][3].ToString());
				List[i].CovOrder      = PIn.PInt   (table.Rows[i][4].ToString());
				List[i].IsHidden      = PIn.PBool  (table.Rows[i][5].ToString());
			}//end for
			cmd.CommandText =
				"SELECT * from covcat"
				+" WHERE ishidden = 0"
				+" ORDER BY covorder";
			FillTable();
			//MessageBox.Show(cmd.CommandText);
			ListShort=new CovCat[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				ListShort[i].CovCatNum     = PIn.PInt   (table.Rows[i][0].ToString());
				ListShort[i].Description   = PIn.PString(table.Rows[i][1].ToString());
				ListShort[i].DefaultPercent= PIn.PInt   (table.Rows[i][2].ToString());
				ListShort[i].IsPreventive  = PIn.PBool  (table.Rows[i][3].ToString());
				ListShort[i].CovOrder      = PIn.PInt   (table.Rows[i][4].ToString());
				//ishidden
			}//end for
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE covcat SET "
				+ "description = '"    +POut.PString(Cur.Description)+"'"
				+",defaultpercent = '" +POut.PInt   (Cur.DefaultPercent)+"'"
				+",ispreventive = '"   +POut.PBool  (Cur.IsPreventive)+"'"
				+",covorder = '"       +POut.PInt   (Cur.CovOrder)+"'"
				+",ishidden = '"       +POut.PBool  (Cur.IsHidden)+"'"
				+" WHERE covcatnum = '"+POut.PInt(Cur.CovCatNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO covcat (description,defaultpercent,ispreventive,"
				+"covorder,ishidden) VALUES("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PInt   (Cur.DefaultPercent)+"', "
				+"'"+POut.PBool  (Cur.IsPreventive)+"', "
				+"'"+POut.PInt   (Cur.CovOrder)+"', "
				+"'"+POut.PBool  (Cur.IsHidden)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static int GetCatNum(string myADACode){
			return 0;
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
		public static void MoveUp(){
			if(Selected==-1){
				MessageBox.Show(Lan.g("CovCat","Please select an item first."));
				return;
			}
			if(Selected==0){
				return;
			}
			SetOrder(Selected-1,List[Selected].CovOrder);
			SetOrder(Selected,List[Selected].CovOrder-1);
			Selected-=1;
		}//end MoveUp

		///<summary></summary>
		public static void MoveDown(){
			if(Selected==-1){
				MessageBox.Show(Lan.g("CovCat","Please select an item first."));
				return;
			}
			if(Selected==List.Length-1){
				return;
			}
			SetOrder(Selected+1,List[Selected].CovOrder);
			SetOrder(Selected,List[Selected].CovOrder+1);
			Selected+=1;
		}

		///<summary></summary>
		public static void SetOrder(int mySelNum, int myItemOrder){
			CovCat temp=List[mySelNum];
			temp.CovOrder=myItemOrder;
			Cur=temp;
			UpdateCur();
		}

		///<summary></summary>
		public static bool GetIsPrev(string myADACode){
			int covCatNum=0;
			for(int i=0;i<CovSpans.List.Length;i++){
				if(String.Compare(myADACode,CovSpans.List[i].FromCode)>=0
					&& String.Compare(myADACode,CovSpans.List[i].ToCode)<=0){
					covCatNum=CovSpans.List[i].CovCatNum;
				}
			}
			for(int i=0;i<ListShort.Length;i++){
				if(covCatNum==ListShort[i].CovCatNum){
					return ListShort[i].IsPreventive;
				}
			}
			return false;//should never happen	
		}

	}

	



}









