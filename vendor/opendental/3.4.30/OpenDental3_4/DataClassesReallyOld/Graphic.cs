
using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	

	/*=========================================================================================
		=================================== Class Generic ===========================================*/
	/*
	//Obsolete class
	public class Generic:SDataClass{
		public Gen Cur;

		public static void Refresh(string myEmp){//this method is not used anywhere yet
			cmd.CommandText =
				"SELECT * from generic";
				//+" WHERE Employee = '"+myEmp+"'";
			FillTable();
			Cur = new Gen();
			Cur.IDNum = PIn.PInt   (table.Rows[0][0].ToString());
			Cur.CatNum= PIn.PInt   (table.Rows[0][1].ToString());
			Cur.Item  = PIn.PString(table.Rows[0][2].ToString());
			Cur.Value = PIn.PString(table.Rows[0][3].ToString());
		}//end method refresh 

		public static void AddItem(){
			//string pIDNum = Cur.IDNum.ToString();
			string pCatNum= POut.PInt   (Cur.CatNum);
			string pItem  = POut.PString(Cur.Item);
			string pValue = POut.PString(Cur.Value);
			cmd.CommandText = "INSERT INTO generic "
				+"(catnum, item, value) VALUES ("
				+"'"+pCatNum+"', "				+"'"+pItem+"', "				+"'"+pValue+"')";			//MessageBox.Show(cmd.CommandText);			NonQ(false);
		}

		public static void Update(){//this method is not in use yet
			string pIDNum = POut.PInt   (Cur.IDNum);
			string pCatNum= POut.PInt   (Cur.CatNum);
			string pItem  = POut.PString(Cur.Item);
			string pValue = POut.PString(Cur.Value);
			
			//cmd.CommandText = "UPDATE Breaks SET " 
			//	+"MornRem = '"+pMornRem+"', "
			//	+"MornStart = '"+pMornStart+"', "
			//	+"EntryDate = '"+pEntryDate+"' "
			//	+"WHERE Employee = '"+pEmployee+"'";
			//MessageBox.Show(cmd.CommandText);
			
			NonQ(false);
		}

	}//end class Generic

	public struct Gen{
		public int IDNum;
		public int CatNum;
		public string Item;
		public string Value;
	}//end struct Gen*/
	
	/*=========================================================================================
	=================================== class GraphicAssemblies ========================================*/
	///<summary></summary>
	public class GraphicAssemblies:DataClass{
		///<summary></summary>
		public static GraphicAssembly[] List;
		///<summary></summary>
		public static GraphicAssembly Cur;
		//public static GraphicAssembly[] Sublist;//obsolete

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText="SELECT * from graphicassembly";
			FillTable();
			List=new GraphicAssembly[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].GAssemblyNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].GTypeNum    = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].GElementNum = PIn.PInt   (table.Rows[i][2].ToString());
			}
		}
		
		///<summary></summary>
		public static void UpdateCur(){
      cmd.CommandText = "UPDATE graphicassembly SET " 
				+ "gtypenum = '"     +POut.PInt   (Cur.GTypeNum)+"'"
				+ ",gelementnum = '" +POut.PInt   (Cur.GElementNum)+"'"
				+" WHERE GAssemblyNum = '" +POut.PInt(Cur.GAssemblyNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO graphicassembly (gtypenum,gelementnum"
				+") VALUES("
				+"'"+POut.PInt   (Cur.GTypeNum)+"', "
				+"'"+POut.PInt   (Cur.GElementNum)+"')";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText="DELETE FROM graphicassembly "
				+"WHERE GAssemblyNum = '"+Cur.GAssemblyNum.ToString()+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static bool HasMultiple(int gElementNum){
			int count=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].GElementNum==gElementNum){
					count++;
				}
			}
			if(count==1) return false;
			return true;
		}

		///<summary></summary>
		public static void GetCur(int gTypeNum,int gElementNum){
			for(int i=0;i<List.Length;i++){
				if(gTypeNum==List[i].GTypeNum && gElementNum==List[i].GElementNum){
					Cur=List[i];
					return;
				}
			}
		}

	}

	///<summary>Corresponds to the graphicassembly table in the database.</summary>
	///<remarks>The only purpose of this table is to link types to elements in a many to many relationship</remarks>
	public struct GraphicAssembly{
		///<summary>Primary key.</summary>
		public int GAssemblyNum;
		///<summary>Foreign key to graphictype.GTypeNum.</summary>
		public int GTypeNum;
		///<summary>Foreign key to graphicelement.GElementNum.</summary>
		public int GElementNum;
	}

	/*=========================================================================================
	=================================== class GraphicElements ==========================================*/
	///<summary></summary>
	public class GraphicElements:DataClass{
		///<summary></summary>
		public static GraphicElement[] List;
		///<summary></summary>
		public static GraphicElement Cur;
		///<summary></summary>
		public static GraphicElement[] Sublist;
		///<summary></summary>
		public static ArrayList ALNewElement;
		///<summary></summary>
		public static Hashtable HList;
		///<summary></summary>
		public static int[,][] MultElementNum;//[Type.ItemOrder,OrdinalTooth][ElementNum list]
		
		///<summary></summary>
		public static void Refresh(){//MUST come immediately after GType.Refresh and GAssemb.Refresh
			cmd.CommandText="SELECT * from graphicelement"
				+" ORDER BY zorder";
			FillTable();
			List=new GraphicElement[table.Rows.Count];
			HList=new Hashtable();
			for(int i=0;i<table.Rows.Count;i++){
				List[i].GElementNum  = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ToothNum     = PIn.PString(table.Rows[i][1].ToString());
				List[i].Description  = PIn.PString(table.Rows[i][2].ToString());
				List[i].Surface      = PIn.PString(table.Rows[i][3].ToString());
				List[i].ZOrder       = PIn.PInt   (table.Rows[i][4].ToString());
				//List[i].ColorForTesting= PIn.PString(table.Rows[i][4].ToString());
				HList.Add(List[i].GElementNum,List[i]);
			}
			ArrayList[,] tempAList=new ArrayList[GraphicTypes.List.Length,53];
			for(int i=0;i<GraphicTypes.List.Length;i++){
				for(int j=0;j<=52;j++){
					tempAList[i,j]=new ArrayList();
				}
			}
			int zIndex=0;
			int ordinalTooth;
			for(int i=0;i<GraphicTypes.List.Length;i++){
				for(int j=0;j<GraphicAssemblies.List.Length;j++){
					if(GraphicAssemblies.List[j].GTypeNum==GraphicTypes.List[i].GTypeNum){
						ordinalTooth=Tooth.ToOrdinal(
							((GraphicElement)HList[GraphicAssemblies.List[j].GElementNum]).ToothNum);
						//find index to insert at based on element zorder.
						zIndex=0;
						//if(intTooth==31){
							//MessageBox.Show(
							//((GraphicElement)HList[GraphicAssemblies.List[j].GElementNum]).ZOrder.ToString());
						//}
						for(int k=0;k<tempAList[i,ordinalTooth].Count;k++){
							//zIndex=k;
							//if(intTooth==31){
								//MessageBox.Show(
								//	((GraphicElement)HList[(int)tempAList[i,intTooth][k]]).ZOrder.ToString()+","+
								//	((GraphicElement)HList[GraphicAssemblies.List[j].GElementNum]).ZOrder.ToString());
							//}
							if( ((GraphicElement)HList[(int)tempAList[i,ordinalTooth][k]]).ZOrder 
								>=((GraphicElement)HList[GraphicAssemblies.List[j].GElementNum]).ZOrder ){
								break;
							}
							zIndex++;
						}
						//if(intTooth==31){
							//MessageBox.Show(zIndex.ToString());
						//}
						tempAList[i,ordinalTooth].Insert(zIndex,GraphicAssemblies.List[j].GElementNum);
					}
				}
			}
			MultElementNum=new int[GraphicTypes.List.Length,53][];
			for(int i=0;i<MultElementNum.GetLength(0);i++){
				for(int j=0;j<MultElementNum.GetLength(1);j++){
					MultElementNum[i,j]=new int[tempAList[i,j].Count];
					for(int k=0;k<tempAList[i,j].Count;k++){
						MultElementNum[i,j][k]=(int)tempAList[i,j][k];
					}
				}
			}
			//string toShow="";
			//for(int i=0;i<MultElementNum[0,31].Length;i++){
			//	toShow+=MultElementNum[0,31][i].ToString()+",";
			//}
			//MessageBox.Show(toShow);
		}
		
		///<summary></summary>
		public static void UpdateCur(){
      cmd.CommandText = "UPDATE graphicelement SET " 
				+ "toothnum = '"     +POut.PString(Cur.ToothNum)+"'"
				+ ",description = '" +POut.PString(Cur.Description)+"'"
				+ ",surface = '"     +POut.PString(Cur.Surface)+"'"
				+ ",zorder = '"      +POut.PInt   (Cur.ZOrder)+"'"
				//+ ",colorfortesting = '"+POut.PString(Cur.Brush)+"'"
				+" WHERE GElementNum = '" +POut.PInt(Cur.GElementNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO graphicelement (toothnum,description,surface"
				+",zorder) VALUES("
				+"'"+POut.PString(Cur.ToothNum)+"', "
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PString(Cur.Surface)+"', "
				+"'"+POut.PInt   (Cur.ZOrder)+"')";
				//+"'"+POut.PInt   (Cur.ColorForTesting)+"')";
			NonQ(true);
			GraphicAssemblies.Cur=new GraphicAssembly();
			GraphicAssemblies.Cur.GTypeNum=GraphicTypes.Cur.GTypeNum;
			GraphicAssemblies.Cur.GElementNum=InsertID;
			GraphicAssemblies.InsertCur();
		}

		///<summary></summary>
		public static void AttachCur(){//attaches an existing element to an existing type
			GraphicAssemblies.Cur=new GraphicAssembly();
			GraphicAssemblies.Cur.GTypeNum=GraphicTypes.Cur.GTypeNum;
			GraphicAssemblies.Cur.GElementNum=GraphicElements.Cur.GElementNum;
			GraphicAssemblies.InsertCur();
		}

		///<summary></summary>
		public static void DeleteCur(){
			GraphicShapes.GetSublist(Cur.GElementNum);
			for(int i=0;i<GraphicShapes.Sublist.Length;i++){
				GraphicShapes.Cur=GraphicShapes.Sublist[i];
				GraphicShapes.DeleteCur();//deletes shape and all points.
			}
			cmd.CommandText="DELETE FROM graphicelement "
				+"WHERE gElementNum = '"+Cur.GElementNum.ToString()+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void GetSublist(int typeIndex,string toothNum){
			if(typeIndex==-1){
				Sublist=new GraphicElement[0];
				return;
			}
			int ordinalTooth=Tooth.ToOrdinal(toothNum);
			Sublist=new GraphicElement[MultElementNum[typeIndex,ordinalTooth].Length];
			for(int i=0;i<Sublist.Length;i++){
				Sublist[i]=(GraphicElement)HList[MultElementNum[typeIndex,ordinalTooth][i]];
			}
		}

		///<summary></summary>
		public static void GetSublistForFilling(string toothNum,string surf){
			if(toothNum==""){
				Sublist=new GraphicElement[0];
				return;
			}
			int typeIndex=GraphicTypes.GetIndex(2);
			int ordTooth;
			ordTooth=Tooth.ToOrdinal(toothNum);
			ArrayList ALsurf=new ArrayList();
			ArrayList ALsubListIndex=new ArrayList();
			for(int i=0;i<surf.Length;i++){
				ALsurf.Add(surf.Substring(i,1));
			}
			string thisSurf;
			for(int i=0;i<MultElementNum[typeIndex,ordTooth].Length;i++){
				thisSurf=((GraphicElement)HList[MultElementNum[typeIndex,ordTooth][i]]).Surface;
				if(thisSurf.Length==1 && ALsurf.Contains(thisSurf)){
					ALsubListIndex.Add(i);
				}
			}
			Sublist=new GraphicElement[ALsubListIndex.Count];
      for(int i=0;i<Sublist.Length;i++){
				Sublist[i]=(GraphicElement)HList[MultElementNum[typeIndex,ordTooth][(int)ALsubListIndex[i]]];
			}
		}

		///<summary></summary>
		public static void GetALNewElement(string toothNum){
			int intTooth=Tooth.ToInt(toothNum);
			ALNewElement=new ArrayList();
			for(int i=0;i<GraphicTypes.List.Length;i++){
				GetSublist(i,toothNum);
				for(int j=0;j<Sublist.Length;j++){
					ALNewElement.Add(Sublist[j]);
				}
			}
		}

	}

	///<summary>Corresponds to the graphicelement table in the database.</summary>
	public struct GraphicElement{
		///<summary>Primary key.</summary>
		public int GElementNum;
		///<summary>2char, valid 1-8,25-32,A-E,P-T, or blank.  Left side is simple mirror</summary>
		public string ToothNum;
		///<summary>eg. M Filling</summary>
		public string Description;
		///<summary>1 char.</summary>
		public string Surface;
		///<summary>Order of painting.</summary>
		public int ZOrder;
		//public Color ColorForTesting;//may change
	}

	/*=========================================================================================
	=================================== class GraphicPoints ==========================================*/
	///<summary></summary>
	public class GraphicPoints:DataClass{
		///<summary></summary>
		public static GraphicPoint[] List;
		///<summary></summary>
		public static GraphicPoint Cur;
		///<summary></summary>
		public static GraphicPoint[] Sublist;
		///<summary></summary>
		public static Hashtable HList;
		///<summary></summary>
		public static Hashtable HbyShape;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from graphicpoint"
				+" ORDER BY itemorder";
			FillData();
		}

		///<summary></summary>
		public static void RefreshShape(int GShapeNum){
			cmd.CommandText =
				"SELECT * from graphicpoint"
				//+" WHERE 
				+" ORDER BY itemorder";
			FillData();
		}

		private static void FillData(){
			FillTable();
			List=new GraphicPoint[table.Rows.Count];
			HList=new Hashtable();
			for(int i=0;i<table.Rows.Count;i++){
				List[i].GPointNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].GShapeNum = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].Xpos      = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].Ypos      = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].ItemOrder = PIn.PInt   (table.Rows[i][4].ToString());
				HList.Add(List[i].GPointNum,List[i]);
			}
			HbyShape=new Hashtable();
			ArrayList tempAList;
			GraphicPoint[] tempPoints;
			for(int i=0;i<GraphicShapes.List.Length;i++){
				tempAList=new ArrayList();
				for(int j=0;j<List.Length;j++){
					if(List[j].GShapeNum==GraphicShapes.List[i].GShapeNum){
						tempAList.Add(List[j]);
					}
				}
				tempPoints=new GraphicPoint[tempAList.Count];
				for(int j=0;j<tempAList.Count;j++){
					tempPoints[j]=(GraphicPoint)tempAList[j];
				}
				HbyShape.Add(GraphicShapes.List[i].GShapeNum,tempPoints);
			}
		}
		
		///<summary></summary>
		public static void UpdateCur(){
      cmd.CommandText = "UPDATE graphicpoint SET " 
				+ "gshapenum = '"  +POut.PInt   (Cur.GShapeNum)+"'"
				+ ",xpos = '"      +POut.PInt   (Cur.Xpos)+"'"
				+ ",ypos = '"      +POut.PInt   (Cur.Ypos)+"'"
				+ ",itemorder = '" +POut.PInt   (Cur.ItemOrder)+"'"
				+" WHERE GPointNum = '" +POut.PInt(Cur.GPointNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO graphicpoint (gshapenum,xpos,ypos,itemorder"
				+") VALUES("
				+"'"+POut.PInt   (Cur.GShapeNum)+"', "
				+"'"+POut.PInt   (Cur.Xpos)+"', "
				+"'"+POut.PInt   (Cur.Ypos)+"', "
				+"'"+POut.PInt   (Cur.ItemOrder)+"')";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			GraphicPoint tempPoint=Cur;
			for(int i=Cur.ItemOrder+1;i<Sublist.Length;i++){
				Cur=Sublist[i];
				Cur.ItemOrder-=1;
				UpdateCur();
			}
			Cur=tempPoint;
			cmd.CommandText="DELETE FROM graphicpoint "
				+"WHERE gpointnum = '"+Cur.GPointNum.ToString()+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void GetSublist(int gShapeNum){
			Sublist=(GraphicPoint[])HbyShape[gShapeNum];
		}

	}

	///<summary>Corresponds to the graphicpoint table in the database.</summary>
	public struct GraphicPoint{
		///<summary>Primary key.</summary>
		public int GPointNum;//primary key
		///<summary>Foreign key to graphicshape.GShapeNum.</summary>
		public int GShapeNum;
		///<summary></summary>
		public int Xpos;
		///<summary></summary>
		public int Ypos;
		///<summary></summary>
		public int ItemOrder;
	}


	/*=========================================================================================
	=================================== class GraphicShapes ==========================================*/
	///<summary></summary>
	public class GraphicShapes:DataClass{
		///<summary></summary>
		public static GraphicShape[] List;
		///<summary></summary>
		public static GraphicShape Cur;
		///<summary></summary>
		public static GraphicShape[] Sublist;
		///<summary></summary>
		public static Hashtable HList;
		///<summary></summary>
		public static Hashtable HbyElement;
		
		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText="SELECT * from graphicshape"
				+" order by shapetype";
			FillTable();
			List=new GraphicShape[table.Rows.Count];
			HList=new Hashtable();
			for(int i=0;i<table.Rows.Count;i++){
				List[i].GShapeNum   = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].GElementNum = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].ShapeType   = PIn.PString(table.Rows[i][2].ToString());
				List[i].Description = PIn.PString(table.Rows[i][3].ToString());
				HList.Add(List[i].GShapeNum,List[i]);
			}
			HbyElement=new Hashtable();
			ArrayList tempAList;
			GraphicShape[] tempShape;
			for(int i=0;i<GraphicElements.List.Length;i++){
				tempAList=new ArrayList();
				for(int j=0;j<List.Length;j++){
					if(List[j].GElementNum==GraphicElements.List[i].GElementNum){
						tempAList.Add(List[j]);
					}
				}
				tempShape=new GraphicShape[tempAList.Count];
				for(int j=0;j<tempAList.Count;j++){
					tempShape[j]=(GraphicShape)tempAList[j];
				}
				HbyElement.Add(GraphicElements.List[i].GElementNum,tempShape);
			}
		}
		
		///<summary></summary>
		public static void UpdateCur(){
      cmd.CommandText = "UPDATE graphicshape SET " 
				+ "gelementnum = '"  +POut.PInt   (Cur.GElementNum)+"'"
				+ ",shapetype = '"   +POut.PString(Cur.ShapeType)+"'"
				+ ",description = '" +POut.PString(Cur.Description)+"'"
				+" WHERE GShapeNum = '" +POut.PInt(Cur.GShapeNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO graphicshape (gelementnum,shapetype,description"
				+") VALUES("
				+"'"+POut.PInt   (Cur.GElementNum)+"', "
				+"'"+POut.PString(Cur.ShapeType)+"', "
				+"'"+POut.PString(Cur.Description)+"')";
			NonQ(true);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText="DELETE FROM graphicpoint "
				+"WHERE gshapenum = '"+Cur.GShapeNum.ToString()+"'";
			NonQ(false);
			cmd.CommandText="DELETE FROM graphicshape "
				+"WHERE gshapenum = '"+Cur.GShapeNum.ToString()+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void GetSublist(int gElementNum){
			Sublist=(GraphicShape[])HbyElement[gElementNum];
		}

	}

	///<summary>Corresponds to the graphicshape table in the database.</summary>
	public struct GraphicShape{
		///<summary>Primary key.</summary>
		public int GShapeNum;
		///<summary>Foreign key to graphicelement.GElementNum.</summary>
		public int GElementNum;
		///<summary>F=Fill Only,G=Polygon,L=Line (strategically in alphabetical order.)</summary>
		public string ShapeType;
		///<summary>eg. MD groove</summary>
		public string Description;
	}

	/*=========================================================================================
	=================================== class GraphicTypes ==========================================*/
	///<summary></summary>
	public class GraphicTypes:DataClass{
		///<summary></summary>
		public static GraphicType[] List;
		///<summary></summary>
		public static GraphicType Cur;

		
		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText="SELECT * from graphictype"
				+" ORDER BY itemorder";
			FillTable();
			List=new GraphicType[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].GTypeNum     = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description  = PIn.PString(table.Rows[i][1].ToString());
				List[i].ItemOrder    = PIn.PInt   (table.Rows[i][2].ToString());
				//List[i].ZOrder       = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].BrushType    = PIn.PString(table.Rows[i][3].ToString());
				List[i].IsSameEachTooth= PIn.PBool(table.Rows[i][4].ToString());
				List[i].IsHidden     = PIn.PBool  (table.Rows[i][5].ToString());
				List[i].SpecialType  = PIn.PString(table.Rows[i][6].ToString());
			}
		}
		
		///<summary></summary>
		public static void UpdateCur(){
      cmd.CommandText = "UPDATE graphictype SET " 
				+ "description = '" +POut.PString(Cur.Description)+"'"
				+ ",itemorder = '"  +POut.PInt   (Cur.ItemOrder)+"'"
				//+ ",zorder = '"     +POut.PInt   (Cur.ZOrder)+"'"
				+ ",brushtype = '"  +POut.PString(Cur.BrushType)+"'"
				+ ",issameeachtooth = '"+POut.PBool(Cur.IsSameEachTooth)+"'"
				+ ",ishidden = '"   +POut.PBool  (Cur.IsHidden)+"'"
				+ ",specialtype = '"+POut.PString(Cur.SpecialType)+"'"
				+" WHERE GTypeNum = '" +POut.PInt   (Cur.GTypeNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO graphictype (description,itemorder"
				+",brushtype,issameeachtooth,ishidden,specialtype) VALUES("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PInt   (Cur.ItemOrder)+"', "
				//+"'"+POut.PInt   (Cur.ZOrder)+"', "
				+"'"+POut.PString(Cur.BrushType)+"', "
				+"'"+POut.PBool  (Cur.IsSameEachTooth)+"', "
				+"'"+POut.PBool  (Cur.IsHidden)+"', "
				+"'"+POut.PString(Cur.SpecialType)+"')";
			NonQ(false);
		}

		//public static void DeleteCur(){
		//	cmd.CommandText="DELETE FROM graphictype "
		//		+"WHERE patnum = '"+Cur.PatNum.ToString()+"'";
		//	NonQ(false);
		//}

		///<summary></summary>
		public static void GetType(int gTypeNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].GTypeNum==gTypeNum){
					Cur=List[i];
				}
			}
		}

		///<summary></summary>
		public static int GetIndex(int gTypeNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].GTypeNum==gTypeNum){
					return i;
				}
			}
			return -1;//should never happen
		}

		///<summary></summary>
		public static string GetSpecialType(int gTypeNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].GTypeNum==gTypeNum){
					return List[i].SpecialType;
				}
			}
			return "";
		}

	}

	///<summary>Corresponds to the graphictype table in the database.</summary>
	public struct GraphicType{
		///<summary>Primary key.</summary>
		public int GTypeNum;
		///<summary>Description. eg Crown Hatch.</summary>
		public string Description;
		///<summary>order that items show in lists</summary>
		public int ItemOrder;//
		///<summary>"outline","solid",or "hatch"</summary>
		public string BrushType;
		///<summary>If true, then not allowed to specify tooth number in GElement.</summary>
		public bool IsSameEachTooth;
		///<summary>If hidden, it will not be used in Open Dental in any way.</summary>
		public bool IsHidden;
		///<summary>"","filling","crown","bridge", or "denture"</summary>
		///<remarks>If filling: always refer to GTypeNum 2 for points.
		///If crown:   always refer to GTypeNum 6.
		///If bridge:									GTypeNum 12
		///If denture:                 GTypeNum 24</remarks>
		public string SpecialType;
	}

}













