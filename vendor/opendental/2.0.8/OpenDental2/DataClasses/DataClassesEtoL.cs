/*=============================================================================================================
FreeDental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
//using MySQLDriverCS;

using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace OpenDental{
	
	/*=========================================================================================
	=================================== class Employees ==========================================*/

	public class Employees:DataClass{
		public static Employee[] List;
		public static Employee Cur;

		public static void Refresh(){
			cmd.CommandText="SELECT * FROM employee";
			FillTable();
			List=new Employee[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].EmployeeNum=PIn.PInt   (table.Rows[i][0].ToString());
				List[i].LName =     PIn.PString(table.Rows[i][1].ToString());
				List[i].FName =     PIn.PString(table.Rows[i][2].ToString());
				List[i].MiddleI =   PIn.PString(table.Rows[i][3].ToString());
				List[i].Password =  PIn.PString(table.Rows[i][4].ToString());
				List[i].IsHidden =  PIn.PBool  (table.Rows[i][5].ToString());
				List[i].UserName =	PIn.PString(table.Rows[i][6].ToString());	
			}
		}

		public static void UpdateCur(){//updates Cur
			cmd.CommandText="UPDATE employee SET " 
				+ "lname = '"     +POut.PString(Cur.LName)+"' "
				+ ",fname = '"    +POut.PString(Cur.FName)+"' "
				+ ",middlei = '"  +POut.PString(Cur.MiddleI)+"' "
				+ ",password = '" +POut.PString(Cur.Password)+"' "
				+ ",ishidden = '" +POut.PBool  (Cur.IsHidden)+"' "
				+ ",username = '" +POut.PString(Cur.UserName)+"' "

				+" WHERE EmployeeNum = '"+POut.PInt(Cur.EmployeeNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		public static void InsertCur(){//saves Cur
			cmd.CommandText = "INSERT INTO employee (lname,fname,middlei,password,ishidden,username) "
				+"VALUES("
				+"'"+POut.PString(Cur.LName)+"', "
				+"'"+POut.PString(Cur.FName)+"', "
				+"'"+POut.PString(Cur.MiddleI)+"', "
				+"'"+POut.PString(Cur.Password)+"', "
				+"'"+POut.PBool  (Cur.IsHidden)+"', "
				+"'"+POut.PString(Cur.UserName)+"')";
			NonQ(true);
			Cur.EmployeeNum=InsertID;
		}

		public static void DeleteCur(){
			cmd.CommandText = "DELETE from employee WHERE EmployeeNum = '"+Cur.EmployeeNum.ToString()+"'";
			NonQ(false);
		}

		public static string GetName(Employee emp){
			return(emp.LName+", "+emp.FName+" "+emp.MiddleI);
		}

		public static string GetName(int employeeNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].EmployeeNum==employeeNum){
					return GetName(List[i]);
				}
			}
			return "";
		}

	}

	public struct Employee{
		public int EmployeeNum;//primary key
		public string LName;//employee's last name
		public string FName;//employee's first name
		public string MiddleI;//employee's middle initial or middle name 
		public string Password;//password to secure employee's times from other employees
		public bool IsHidden;//tells whether employee is active
		public string UserName;
		//public string Abbrev;//Not in use
		//public bool IsAdmin;//Not in use
		//public string TimePeriodType;//Not in use
	}
	
	/*=========================================================================================
		=================================== class Fees ===========================================*/

	public class Fees:DataClass{
		public static Fee Cur;
		public static Hashtable[] HList;
		
		public static void Refresh(){
			HList=new Hashtable[Defs.Short[(int)DefCat.FeeSchedNames].Length];
			for(int i=0;i<HList.Length;i++){
				HList[i]=new Hashtable(53);
			}
			Fee temp = new Fee();
			cmd.CommandText = 
				"SELECT * from fee";
			FillTable();
			for (int i=0;i<table.Rows.Count;i++){
				temp.FeeNum       =PIn.PInt   (table.Rows[i][0].ToString());
				temp.Amount       =PIn.PDouble(table.Rows[i][1].ToString());
				temp.ADACode      =PIn.PString(table.Rows[i][2].ToString());
				temp.FeeSched     =PIn.PInt   (table.Rows[i][3].ToString());
				temp.UseDefaultFee=PIn.PBool  (table.Rows[i][4].ToString());
				temp.UseDefaultCov=PIn.PBool  (table.Rows[i][5].ToString());
				if(Defs.GetOrder(DefCat.FeeSchedNames,temp.FeeSched)!=-1){
					if(HList[Defs.GetOrder(DefCat.FeeSchedNames,temp.FeeSched)].ContainsKey(temp.ADACode)){
						cmd.CommandText="DELETE FROM fee WHERE feenum = '"+temp.FeeNum+"'";
						NonQ(false);
					}
					else{
						HList[Defs.GetOrder(DefCat.FeeSchedNames,temp.FeeSched)].Add(temp.ADACode,temp);
					}
				}
			}
		}

		public static void SaveOrUpdate(){
			if(Cur.FeeNum==0){
				MessageBox.Show(Lan.g("Fees","inserting new record"));
				InsertCur();
				//no, total refresh instead
				//HList[Prefs.GetOrder(PrefCat.FeeSchedNames,Cur.FeeSched)].Add(Cur.ADACode,Cur);
			}
			else{
				MessageBox.Show(Lan.g("Fees","updating existing record"));
				UpdateCur();
				//
			}
		}

		public static void UpdateCur(){//updates Cur
			cmd.CommandText = "UPDATE fee SET " 
				+ "amount = '"        +POut.PDouble(Cur.Amount)+"'"
				+ ",adacode = '"      +POut.PString(Cur.ADACode)+"'"
				+ ",feesched = '"     +POut.PInt   (Cur.FeeSched)+"'"
				+ ",usedefaultfee = '"+POut.PBool  (Cur.UseDefaultFee)+"'"
				+ ",usedefaultcov = '"+POut.PBool  (Cur.UseDefaultCov)+"'"
				+" WHERE feenum = '"  +POut.PInt   (Cur.FeeNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		public static void InsertCur(){//saves Cur
			cmd.CommandText = "INSERT INTO fee (amount,adacode,"
				+"feesched,usedefaultfee,usedefaultcov) VALUES("
				+"'"+POut.PDouble(Cur.Amount)+"', "
				+"'"+POut.PString(Cur.ADACode)+"', "
				+"'"+POut.PInt   (Cur.FeeSched)+"', "
				+"'"+POut.PBool  (Cur.UseDefaultFee)+"', "
				+"'"+POut.PBool  (Cur.UseDefaultCov)+"')";
			NonQ(false);
		}

		public static Fee GetFeeByOrder(string adacode, int order){
			if(adacode==null)
				return new Fee();
			if(HList[order].Contains(adacode)){
				return (Fee)HList[order][adacode];
			}
			else{
				//MessageBox.Show("code not found: "+myADA);
				return new Fee();
			}
		}

		public static double GetAmount(string adacode, int feeSched){
			double retVal=0;
			if(adacode==null)
				return 0;
			int i=Defs.GetOrder(DefCat.FeeSchedNames,feeSched);
			if(i==-1){
				return 0;//you can not obtain fees for hidden fee schedules
			}
			if(HList[i].Contains(adacode)){
				return ((Fee)HList[i][adacode]).Amount;
			}
			return retVal;
		}


	}//end class Fees

	public struct Fee{
		public int FeeNum;//primary key
		public double Amount;//fee charged
		public string ADACode;//foreign key to ProcedureLog.ADACode
		public int FeeSched;//foreign key to Definition.DefNum
		public bool UseDefaultFee;//not used
		public bool UseDefaultCov;//not used
	}//end struct Fee

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
	public class GraphicAssemblies:DataClass{
		public static GraphicAssembly[] List;
		public static GraphicAssembly Cur;
		//public static GraphicAssembly[] Sublist;//obsolete

		public static string GetSelectText(){
			//cmd.CommandText =
			return "SELECT * from graphicassembly";
		}

		public static void Refresh(){
			table=ds.Tables["graphicassembly"];
			List=new GraphicAssembly[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].GAssemblyNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].GTypeNum    = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].GElementNum = PIn.PInt   (table.Rows[i][2].ToString());
			}
		}
		
		public static void UpdateCur(){
      cmd.CommandText = "UPDATE graphicassembly SET " 
				+ "gtypenum = '"     +POut.PInt   (Cur.GTypeNum)+"'"
				+ ",gelementnum = '" +POut.PInt   (Cur.GElementNum)+"'"
				+" WHERE GAssemblyNum = '" +POut.PInt(Cur.GAssemblyNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO graphicassembly (gtypenum,gelementnum"
				+") VALUES("
				+"'"+POut.PInt   (Cur.GTypeNum)+"', "
				+"'"+POut.PInt   (Cur.GElementNum)+"')";
			NonQ(false);
		}

		public static void DeleteCur(){
			cmd.CommandText="DELETE FROM graphicassembly "
				+"WHERE GAssemblyNum = '"+Cur.GAssemblyNum.ToString()+"'";
			NonQ(false);
		}

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

		public static void GetCur(int gTypeNum,int gElementNum){
			for(int i=0;i<List.Length;i++){
				if(gTypeNum==List[i].GTypeNum && gElementNum==List[i].GElementNum){
					Cur=List[i];
					return;
				}
			}
		}

	}

	public struct GraphicAssembly{//the only purpose of this table is to link types to elements
																//in a many to many relationship
		public int GAssemblyNum;//primary key
		public int GTypeNum;//foreign key to  graphictype.GTypeNum
		public int GElementNum;//foreign key to graphicelement.GElementNum
	}

	/*=========================================================================================
	=================================== class GraphicElements ==========================================*/
	public class GraphicElements:DataClass{
		public static GraphicElement[] List;
		public static GraphicElement Cur;
		public static GraphicElement[] Sublist;
		public static ArrayList ALNewElement;
		public static Hashtable HList;
		public static int[,][] MultElementNum;//[Type.ItemOrder,OrdinalTooth][ElementNum list]

		public static string GetSelectText(){
			return "SELECT * from graphicelement"
				+" ORDER BY zorder";
		}
		
		public static void Refresh(){//MUST come immediately after GType.Refresh and GAssemb.Refresh
			table=ds.Tables["graphicelement"];
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

		public static void AttachCur(){//attaches an existing element to an existing type
			GraphicAssemblies.Cur=new GraphicAssembly();
			GraphicAssemblies.Cur.GTypeNum=GraphicTypes.Cur.GTypeNum;
			GraphicAssemblies.Cur.GElementNum=GraphicElements.Cur.GElementNum;
			GraphicAssemblies.InsertCur();
		}

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

		public static void GetSublistForFilling(string toothNum,string surf){
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

	public struct GraphicElement{
		public int GElementNum;//primary key
		public string ToothNum;//2char, valid 1-8,25-32,A-E,P-T, or blank.  Left side is simple mirror
		public string Description;//eg. M Filling
		public string Surface;//1 char.
		public int ZOrder;//order of painting
		//public Color ColorForTesting;//may change
	}

	/*=========================================================================================
	=================================== class GraphicPoints ==========================================*/
	public class GraphicPoints:DataClass{
		public static GraphicPoint[] List;
		public static GraphicPoint Cur;
		public static GraphicPoint[] Sublist;
		public static Hashtable HList;
		public static Hashtable HbyShape;

		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from graphicpoint"
				+" ORDER BY itemorder";
			FillData();
		}

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

		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO graphicpoint (gshapenum,xpos,ypos,itemorder"
				+") VALUES("
				+"'"+POut.PInt   (Cur.GShapeNum)+"', "
				+"'"+POut.PInt   (Cur.Xpos)+"', "
				+"'"+POut.PInt   (Cur.Ypos)+"', "
				+"'"+POut.PInt   (Cur.ItemOrder)+"')";
			NonQ(false);
		}

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

		public static void GetSublist(int gShapeNum){
			Sublist=(GraphicPoint[])HbyShape[gShapeNum];
		}

	}

	public struct GraphicPoint{
		public int GPointNum;//primary key
		public int GShapeNum;//foreign key
		public int Xpos;
		public int Ypos;
		public int ItemOrder;
	}


	/*=========================================================================================
	=================================== class GraphicShapes ==========================================*/
	public class GraphicShapes:DataClass{
		public static GraphicShape[] List;
		public static GraphicShape Cur;
		public static GraphicShape[] Sublist;
		public static Hashtable HList;
		public static Hashtable HbyElement;

		public static string GetSelectText(){
			return "SELECT * from graphicshape"
				+" order by shapetype";
		}
		
		public static void Refresh(){
			table=ds.Tables["graphicshape"];
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
		
		public static void UpdateCur(){
      cmd.CommandText = "UPDATE graphicshape SET " 
				+ "gelementnum = '"  +POut.PInt   (Cur.GElementNum)+"'"
				+ ",shapetype = '"   +POut.PString(Cur.ShapeType)+"'"
				+ ",description = '" +POut.PString(Cur.Description)+"'"
				+" WHERE GShapeNum = '" +POut.PInt(Cur.GShapeNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO graphicshape (gelementnum,shapetype,description"
				+") VALUES("
				+"'"+POut.PInt   (Cur.GElementNum)+"', "
				+"'"+POut.PString(Cur.ShapeType)+"', "
				+"'"+POut.PString(Cur.Description)+"')";
			NonQ(true);
		}

		public static void DeleteCur(){
			cmd.CommandText="DELETE FROM graphicpoint "
				+"WHERE gshapenum = '"+Cur.GShapeNum.ToString()+"'";
			NonQ(false);
			cmd.CommandText="DELETE FROM graphicshape "
				+"WHERE gshapenum = '"+Cur.GShapeNum.ToString()+"'";
			NonQ(false);
		}

		public static void GetSublist(int gElementNum){
			Sublist=(GraphicShape[])HbyElement[gElementNum];
		}

	}

	public struct GraphicShape{
		public int GShapeNum;//primary key
		public int GElementNum;//foreign key
		public string ShapeType;//F=Fill Only,G=Polygon,L=Line (strategically in alphabetical order.)
		public string Description;//eg. MD groove
	}

	/*=========================================================================================
	=================================== class GraphicTypes ==========================================*/
	public class GraphicTypes:DataClass{
		public static GraphicType[] List;
		public static GraphicType Cur;

		public static string GetSelectText(){
			return "SELECT * from graphictype"
				+" ORDER BY itemorder";
		}
		
		public static void Refresh(){
			table=ds.Tables["graphictype"];
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

		public static void GetType(int gTypeNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].GTypeNum==gTypeNum){
					Cur=List[i];
				}
			}
		}

		public static int GetIndex(int gTypeNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].GTypeNum==gTypeNum){
					return i;
				}
			}
			return -1;//should never happen
		}

		public static string GetSpecialType(int gTypeNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].GTypeNum==gTypeNum){
					return List[i].SpecialType;
				}
			}
			return "";
		}

	}

	public struct GraphicType{
		public int GTypeNum;//primary key
		public string Description;
		public int ItemOrder;//order that items show in lists
		//public int ZOrder;//order that they are painted
		public string BrushType;//"outline","solid",or "hatch"
		public bool IsSameEachTooth;//if true, then not allowed to specify tooth number in GElement
		public bool IsHidden;//if hidden, it will not be used in Free Dental in any way
		public string SpecialType;//"","filling","crown","bridge", or "denture"
			//if filling: always refer to GTypeNum 2 for points
			//if crown:   always refer to GTypeNum 6
			//if bridge:									GTypeNum 12
			//if denture:                 GTypeNum 24
	}

	/*=========================================================================================
		=================================== Class InsPlans ===========================================*/
	public class InsPlans:DataClass{
		public static InsPlan[] List;
		public static Hashtable HList;
		public static Hashtable HListAll;
		public static int Selected;
		public static InsPlan Cur;

		public static void Refresh(){
			string s="subscriber='"+Patients.FamilyList[0].PatNum+"'";
			for(int i=1;i<Patients.FamilyList.Length;i++){
				s+=" || subscriber='"+Patients.FamilyList[i].PatNum+"'";
			}
			cmd.CommandText =
				"SELECT * from insplan "
				+"WHERE "+s
				+" ORDER BY dateeffective";
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			HList=new Hashtable();
			List = new InsPlan[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				List[i].PlanNum      = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Subscriber   = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].Carrier      = PIn.PString(table.Rows[i][2].ToString());
				List[i].DateEffective= PIn.PDate  (table.Rows[i][3].ToString());
				List[i].DateTerm     = PIn.PDate  (table.Rows[i][4].ToString());
				List[i].Phone        = PIn.PString(table.Rows[i][5].ToString());
				List[i].GroupName    = PIn.PString(table.Rows[i][6].ToString());
				List[i].GroupNum     = PIn.PString(table.Rows[i][7].ToString());
				List[i].Address      = PIn.PString(table.Rows[i][8].ToString());
				List[i].Address2     = PIn.PString(table.Rows[i][9].ToString());
				List[i].City         = PIn.PString(table.Rows[i][10].ToString());
				List[i].State        = PIn.PString(table.Rows[i][11].ToString());
				List[i].Zip          = PIn.PString(table.Rows[i][12].ToString());
				List[i].ClaimFormat  = PIn.PInt   (table.Rows[i][13].ToString());
				List[i].ElectID      = PIn.PString(table.Rows[i][14].ToString());
				List[i].Employer     = PIn.PString(table.Rows[i][15].ToString());
				List[i].AnnualMax    = PIn.PInt   (table.Rows[i][16].ToString());
				List[i].RenewMonth   = PIn.PInt   (table.Rows[i][17].ToString());
				List[i].Deductible   = PIn.PInt   (table.Rows[i][18].ToString());
				List[i].DeductWaivPrev=(YN)PIn.PInt(table.Rows[i][19].ToString());
				List[i].OrthoMax     = PIn.PInt    (table.Rows[i][20].ToString());
				List[i].FloToAge     = PIn.PInt    (table.Rows[i][21].ToString());
				List[i].PlanNote     = PIn.PString (table.Rows[i][22].ToString());
				List[i].MissToothExcl= (YN)PIn.PInt(table.Rows[i][23].ToString());
				List[i].MajorWait    = (YN)PIn.PInt(table.Rows[i][24].ToString());
				List[i].FeeSched     = PIn.PInt    (table.Rows[i][25].ToString());
				List[i].ReleaseInfo  = PIn.PBool   (table.Rows[i][26].ToString());
				List[i].AssignBen    = PIn.PBool   (table.Rows[i][27].ToString());
				HList.Add(List[i].PlanNum,List[i]);
			}//end for
		}//end Refresh

		//To decide whether to use SavePlan or UpdatePlan, you have to test first
		//for value of PlanNum
		public static void InsertCur(){//only for new plans
			cmd.CommandText = "INSERT INTO insplan (subscriber, carrier, "
				+"dateeffective,dateterm,phone,groupname,groupnum,address,address2,city,state,zip,"
				+"claimformat,electid,employer,annualmax,renewmonth,deductible,"
				+"deductwaivprev,orthomax,"
				+"flotoage,plannote,misstoothexcl,majorwait,feesched,"
				+"releaseinfo,assignben) VALUES("
				//+"'"+POut.PInt   (Cur.PlanOrder)+"', "
				+"'"+POut.PInt   (Cur.Subscriber)+"', "
				+"'"+POut.PString(Cur.Carrier)+"', "
				+"'"+POut.PDate  (Cur.DateEffective)+"', "
				+"'"+POut.PDate  (Cur.DateTerm)+"', "
				+"'"+POut.PString(Cur.Phone)+"', "
				+"'"+POut.PString(Cur.GroupName)+"', "
				+"'"+POut.PString(Cur.GroupNum)+"', "
				+"'"+POut.PString(Cur.Address)+"', "
				+"'"+POut.PString(Cur.Address2)+"', "
				+"'"+POut.PString(Cur.City)+"', "
				+"'"+POut.PString(Cur.State)+"', "
				+"'"+POut.PString(Cur.Zip)+"', "
				+"'"+POut.PInt   (Cur.ClaimFormat)+"', "
				+"'"+POut.PString(Cur.ElectID)+"', "
				+"'"+POut.PString(Cur.Employer)+"', "
				+"'"+POut.PInt   (Cur.AnnualMax)+"', "
				+"'"+POut.PInt   (Cur.RenewMonth)+"', "
				+"'"+POut.PInt   (Cur.Deductible)+"', "
				+"'"+POut.PInt   ((int)Cur.DeductWaivPrev)+"', "
				+"'"+POut.PInt   (Cur.OrthoMax)+"', "
				+"'"+POut.PInt   (Cur.FloToAge)+"', "
				+"'"+POut.PString(Cur.PlanNote)+"', "
				+"'"+POut.PInt   ((int)Cur.MissToothExcl)+"', "
				+"'"+POut.PInt   ((int)Cur.MajorWait)+"', "
				+"'"+POut.PInt   (Cur.FeeSched)+"', "
				+"'"+POut.PBool  (Cur.ReleaseInfo)+"', "
				+"'"+POut.PBool  (Cur.AssignBen)+"')";
			NonQ(true);
			Cur.PlanNum=InsertID;
		}

		public static void UpdateCur(){//updates Cur
			cmd.CommandText = "UPDATE insplan SET " 
				//+ "PlanOrder = '"     +POut.PInt   (Cur.PlanOrder)+"'"
				+ "Subscriber = '"   +POut.PInt   (Cur.Subscriber)+"'"
				+ ",Carrier = '"      +POut.PString(Cur.Carrier)+"'"
				+ ",DateEffective = '"+POut.PDate  (Cur.DateEffective)+"'"
				+ ",DateTerm = '"     +POut.PDate  (Cur.DateTerm)+"'"
				+ ",Phone = '"        +POut.PString(Cur.Phone)+"'"
				+ ",GroupName = '"    +POut.PString(Cur.GroupName)+"'"
				+ ",GroupNum = '"     +POut.PString(Cur.GroupNum)+"'"
				+ ",Address = '"      +POut.PString(Cur.Address)+"'"
				+ ",Address2 = '"     +POut.PString(Cur.Address2)+"'"
				+ ",City = '"         +POut.PString(Cur.City)+"'"
				+ ",State = '"        +POut.PString(Cur.State)+"'"
				+ ",Zip = '"          +POut.PString(Cur.Zip)+"'"
				+ ",ClaimFormat = '"  +POut.PInt   (Cur.ClaimFormat)+"'"
				+ ",ElectID = '"      +POut.PString(Cur.ElectID)+"'"
				+ ",Employer = '"     +POut.PString(Cur.Employer)+"'"
				+ ",AnnualMax = '"    +POut.PInt   (Cur.AnnualMax)+"'"
				+ ",RenewMonth = '"   +POut.PInt   (Cur.RenewMonth)+"'"
				+ ",Deductible = '"   +POut.PInt   (Cur.Deductible)+"'"
				+ ",DeductWaivPrev= '"+POut.PInt   ((int)Cur.DeductWaivPrev)+"'"
				+ ",OrthoMax = '"     +POut.PInt   (Cur.OrthoMax)+"'"
				+ ",FloToAge = '"     +POut.PInt   (Cur.FloToAge)+"'"
				+ ",PlanNote = '"     +POut.PString(Cur.PlanNote)+"'"
				+ ",MissToothExcl = '"+POut.PInt   ((int)Cur.MissToothExcl)+"'"
				+ ",MajorWait = '"    +POut.PInt   ((int)Cur.MajorWait)+"'"
				+ ",feesched = '"     +POut.PInt   (Cur.FeeSched)+"'"
				+ ",releaseinfo = '"  +POut.PBool  (Cur.ReleaseInfo)+"'"
				+ ",assignben = '"    +POut.PBool  (Cur.AssignBen)+"'"
				+" WHERE PlanNum = '" +POut.PInt(Cur.PlanNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		public static void DeleteSelected(){
			Cur=List[Selected]; 
			cmd.CommandText="SELECT patnum FROM patient "
				+"WHERE priplannum = '"+InsPlans.Cur.PlanNum.ToString()+"' "
				+"|| secplannum = '"+InsPlans.Cur.PlanNum.ToString()+"'";
			FillTable();
			if(table.Rows.Count!=0){
				MessageBox.Show(Lan.g("ContrFamily","Not allowed to delete a plan that is in use by patients."));
				return;
			}
			cmd.CommandText="SELECT patnum FROM claim "
				+"WHERE plannum = '"+InsPlans.Cur.PlanNum.ToString()+"'";
			FillTable();
			if(table.Rows.Count!=0){
				MessageBox.Show(Lan.g("ContrFamily","Not allowed to delete a plan with existing claims."));
				return;
			}
			cmd.CommandText = "DELETE FROM covpat WHERE plannum = '"+Cur.PlanNum.ToString()+"'";
			NonQ(false);
			cmd.CommandText = "DELETE FROM insplan "
				+"WHERE planNum = '"+Cur.PlanNum.ToString()+"'";
			NonQ(false);
		}

		public static void DeleteCur(){//used in new plan if cancel
			cmd.CommandText = "DELETE from insplan WHERE planNum = '"+Cur.PlanNum.ToString()+"'";
			NonQ(false);
		}

		public static string GetDescInFam(int planNum){//only works if plan list already loaded for family
			string retStr="";
			Patients Patients=new Patients();
			if(!HList.Contains(planNum))
				return "";
			try{
				for(int i=0;i<List.Length;i++){
					if(List[i].PlanNum==planNum){
						retStr=(i+1).ToString()+": "+List[i].Carrier
							+" ("+Patients.GetNameInFamFL(List[i].Subscriber)+")";
					}
				}
				//retStr+=
				//	((InsPlan)HList[planNum]).Carrier;
			}
			catch{ ; }
			
			return retStr;
		}

		public static string GetCarrierInFam(int planNum){//only works if plan list already loaded for family
			string retStr="";
			if(HList.Contains(planNum))
				retStr=((InsPlan)HList[planNum]).Carrier;
			return retStr;
		}

		public static void SetCurInFam(int planNum){
			Cur=new InsPlan();
			for(int i=0;i<List.Length;i++){
				if(List[i].PlanNum==planNum){
					Cur=List[i];
				}
			}
		}

		public static string GetDesc(int planNum){
			//string retStr="";
			if(planNum==0){
				return "";
			}
			cmd.CommandText = 
				"SELECT carrier from insplan "
				+"WHERE planNum = '"+planNum.ToString()+"'";
			FillTable();
			return PIn.PString(table.Rows[0][0].ToString());
		}

		public static double GetInsRem(DateTime date,int planNum,int excludeClaim){//Claims,InsPlans must be refreshed
			//must return actual remaining estimate, taking into account inspayed, and ins pending.
			if(((InsPlan)HList[planNum]).AnnualMax<=0){
				return 0;
			}
			double retVal=((InsPlan)HList[planNum]).AnnualMax;
			for(int i=0;i<Claims.List.Length;i++){
				if(Claims.List[i].ClaimType=="PreAuth")
					continue;//preauths do not affect any insurance benefits.
				if(Claims.List[i].PlanNum==planNum
					&& Claims.List[i].DateService.Year==date.Year
					&& Claims.List[i].ClaimNum != excludeClaim){
					if(Claims.List[i].ClaimStatus=="R" || Claims.List[i].ClaimStatus=="A"){
						retVal-=Claims.List[i].InsPayAmt;
					}
					else{//just estimate
						retVal-=Claims.List[i].InsPayEst;
					}
				}
			}
			if(retVal<0) return 0;
			return retVal;
		}

		public static double GetPending(DateTime date,int planNum){//Claims,InsPlans must be refreshed
			if(((InsPlan)HList[planNum]).AnnualMax<=0){
				return 0;
			}
			double retVal=0;
			for(int i=0;i<Claims.List.Length;i++){
				if(Claims.List[i].PlanNum==planNum
					&& Claims.List[i].DateService.Year==date.Year
					&& Claims.List[i].ClaimStatus!="R"
					&& Claims.List[i].ClaimType!="PreAuth"
					//ClaimStatus "A" has no insPayEst, so can ignore it here.
					){
					retVal+=Claims.List[i].InsPayEst;
				}
			}
			return retVal;
		}

		public static double GetDedRem(DateTime date,int planNum){//Claims,InsPlans must be refreshed
			if(((InsPlan)HList[planNum]).AnnualMax<=0){
				return 0;
			}
			double retVal=0;
			if(((InsPlan)HList[planNum]).Deductible!=-1){
				retVal=((InsPlan)HList[planNum]).Deductible;
			}
			for(int i=0;i<Claims.List.Length;i++){
				if(Claims.List[i].PlanNum==planNum
					&& Claims.List[i].DateService.Year==date.Year
					//ClaimStatus "A" does get included in this calculation
					&& Claims.List[i].ClaimType!="PreAuth"//preauths do not affect deductibles
					){
					retVal-=Claims.List[i].DedApplied;
				}
			}
			if(retVal<0) return 0;
			return retVal;
		}

		public static bool CheckDependencies(int patNum){
			//get insplans for this subscriber.
			cmd.CommandText="SELECT planNum FROM insplan WHERE "
				+"subscriber = '"+patNum.ToString()+"'";
			FillTable();
			if(table.Rows.Count==0){
				return false;
			}
			string planNum;
			for (int i=0;i<table.Rows.Count;i++){
				planNum = PIn.PString(table.Rows[i][0].ToString());
				cmd.CommandText="SELECT patnum FROM patient "
					+"WHERE (priplannum = '"+planNum+"' "
					+"|| secplannum = '"+planNum+"') "
					+"AND patnum != '"+patNum.ToString()+"'";
				FillTable();
				if(table.Rows.Count!=0){
					MessageBox.Show(Lan.g("ContrFamily","Patient has insurance that is in use by other family memebers.  Please see the manual for instructions."));
					return true;
				}
				cmd.CommandText="SELECT patnum FROM claim "
					+"WHERE plannum = '"+planNum+"' "
					+"AND patnum != '"+patNum.ToString()+"'";
				FillTable();
				if(table.Rows.Count!=0){
					MessageBox.Show(Lan.g("ContrFamily","Patient has insurance that has existing claims for other family members. Please see the manual for instructions."));
					return true;
				}
			}
			return false;
		}

		public static void GetHListAll(){
			cmd.CommandText="SELECT plannum,carrier "
				+"FROM insplan";
			FillTable();
			HListAll=new Hashtable(table.Rows.Count);
			int plannum;
			string carrier;
			for(int i=0;i<table.Rows.Count;i++){
				plannum=PIn.PInt(table.Rows[i][0].ToString());
				carrier=PIn.PString(table.Rows[i][1].ToString());
				HListAll.Add(plannum,carrier);
			}
		}

	}//end Class InsPlans

	public struct InsPlan{
		public int PlanNum;//primary key
		public int Subscriber;//foreign key to Patient.PatNum.
		public string Carrier;//Name of carrier
		public DateTime DateEffective;//Date plan became effective
		public DateTime DateTerm;//Date plan was terminated
		public string Phone;//includes any punctuation
		public string GroupName;//usually the employer
		public string GroupNum;
		public string Address;
		public string Address2;
		public string City;
		public string State;//2 char
		public string Zip;
		public int ClaimFormat;//foreign key to Definition.DefNum.  
		//For now, Definition.ItemValue will = "eclaim" or "ADA2002"
		public string ElectID;//5 char for eclaims
		public string Employer;
		public int AnnualMax;
		public int RenewMonth;
		public int Deductible;
		public YN DeductWaivPrev;//0=unknown,1=Yes-covered,2=No-not covered
		public int OrthoMax;
		public int FloToAge;
		public string PlanNote;
		public YN MissToothExcl;
		public YN MajorWait;
		public int FeeSched;//foreign key to Definition.DefNum.
		//Name of feeschedule is stored in Definition.ItemValue.
		public bool ReleaseInfo;
		public bool AssignBen;
	}

	/*=========================================================================================
		=================================== Class InsTemplates ===========================================*/
	public class InsTemplates:DataClass{
		public static InsTemplate[] List;
		public static int Selected;
		public static bool IsSelected;
		public static InsTemplate Cur;

		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from instemplate "
				+"ORDER BY Carrier ASC";
			FillTable();
			List = new InsTemplate[table.Rows.Count];
			for (int i=0;i<List.Length;i++){
				List[i].TemplateNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Carrier    = PIn.PString(table.Rows[i][1].ToString());
				List[i].Address    = PIn.PString(table.Rows[i][2].ToString());
				List[i].Address2   = PIn.PString(table.Rows[i][3].ToString());
				List[i].City       = PIn.PString(table.Rows[i][4].ToString());
				List[i].State      = PIn.PString(table.Rows[i][5].ToString());
				List[i].Zip        = PIn.PString(table.Rows[i][6].ToString());
				List[i].Phone      = PIn.PString(table.Rows[i][7].ToString());
				List[i].ClaimFormat= PIn.PInt   (table.Rows[i][8].ToString());
				List[i].ElectID    = PIn.PString(table.Rows[i][9].ToString());
				List[i].Note       = PIn.PString(table.Rows[i][10].ToString());
			}
		}

		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO instemplate (carrier,address,address2,city,state,zip,"
				+"phone,claimformat,electid,note) VALUES("
				+"'"+POut.PString(Cur.Carrier)+"', "
				+"'"+POut.PString(Cur.Address)+"', "
				+"'"+POut.PString(Cur.Address2)+"', "
				+"'"+POut.PString(Cur.City)+"', "
				+"'"+POut.PString(Cur.State)+"', "
				+"'"+POut.PString(Cur.Zip)+"', "
				+"'"+POut.PString(Cur.Phone)+"', "
				+"'"+POut.PInt   (Cur.ClaimFormat)+"', "
				+"'"+POut.PString(Cur.ElectID)+"', "
				+"'"+POut.PString(Cur.Note)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE InsTemplate SET " 
				+ "Carrier = '"   +POut.PString   (Cur.Carrier)+"'"
				+ ",Address = '"  +POut.PString(Cur.Address)+"'"
				+ ",Address2 = '" +POut.PString(Cur.Address2)+"'"
				+ ",City = '"     +POut.PString(Cur.City)+"'"
				+ ",State = '"    +POut.PString(Cur.State)+"'"
				+ ",Zip = '"      +POut.PString(Cur.Zip)+"'"
				+ ",Phone = '"    +POut.PString(Cur.Phone)+"'"
				+ ",ClaimFormat='"+POut.PInt   (Cur.ClaimFormat)+"'"
				+ ",ElectID = '"  +POut.PString(Cur.ElectID)+"'"
				+ ",Note = '"     +POut.PString(Cur.Note)+"'"
				+" WHERE TemplateNum = '" +POut.PInt(Cur.TemplateNum)+"'";
			NonQ(false);
		}

		public static void DeleteSelected(){
			Cur=List[Selected];
			cmd.CommandText = "DELETE from instemplate WHERE templatenum = '"+Cur.TemplateNum.ToString()+"'";
			NonQ(false);
		}
	}

	public struct InsTemplate{
		public int TemplateNum;
		public string Carrier;
		public string Address;
		public string Address2;
		public string City;
		public string State;
		public string Zip;
		public string Phone;
		public int ClaimFormat;
		public string ElectID;
		public string Note;
	}

	/*=========================================================================================
	=================================== class Lan ===========================================*/
	public class Lan:DataClass{
		public static Hashtable HList;//the purpose is to allow automatic adding of phrases to db
		public static Language Cur;
		public static string[] ListCat;//list of categories
		public static Language[] List;
		public static string CurCat;
		public static Language[] ListForCat;

		public static void Refresh(){
			//MessageBox.Show("refreshing");
			HList=new Hashtable();
			if(CultureInfo.CurrentCulture.TwoLetterISOLanguageName=="en"){
				return;
			}
			cmd.CommandText =
				"SELECT * from language";
			FillTable();
			List=new Language[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				List[i].EnglishComments= PIn.PString(table.Rows[i][0].ToString());
				List[i].ClassType      = PIn.PString(table.Rows[i][1].ToString());
				List[i].English        = PIn.PString(table.Rows[i][2].ToString());
				List[i].IsObsolete     = PIn.PBool  (table.Rows[i][3].ToString());
				HList.Add(List[i].ClassType+List[i].English,List[i]);
			}
		}

		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO language (classtype,english) "
				+"VALUES("
				+"'"+POut.PString(Cur.ClassType)+"', "
				+"'"+POut.PString(Cur.English)+"')";
			NonQ(false);
		}

		public static void UpdateCur(){
			//not used to update the english version of text.  Create new instead.
			cmd.CommandText="UPDATE language SET "
				+"englishcomments = '" +POut.PString(Cur.EnglishComments)+"'"
				+",isobsolete = '"     +POut.PBool  (Cur.IsObsolete)+"'"
				+" WHERE classtype = '"+POut.PString(Cur.ClassType)+"'"
				+" && english = '"     +POut.PString(Cur.English)+"'";
			NonQ(false);
			
		}

		public static void GetListCat(){
			cmd.CommandText =
				"SELECT Distinct ClassType FROM language ORDER BY ClassType ";
			FillTable();
			ListCat=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				ListCat[i]=PIn.PString(table.Rows[i][0].ToString());
			}
		}

		public static void SetObsolete(Language[] lanList,bool isObsolete){
			for(int i=0;i<lanList.Length;i++){
				Cur=lanList[i];
				Cur.IsObsolete=isObsolete;
				UpdateCur();
			}
		}

		public static void GetListForCat(string classType){
			//only used in translation tool
			if(CultureInfo.CurrentCulture.TwoLetterISOLanguageName=="en"){
				return;
			}
			cmd.CommandText =
				"SELECT * from language "
				+"WHERE classtype = '"+classType+"'";
			FillTable();
			ListForCat=new Language[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				ListForCat[i].EnglishComments= PIn.PString(table.Rows[i][0].ToString());
				ListForCat[i].ClassType      = PIn.PString(table.Rows[i][1].ToString());
				ListForCat[i].English        = PIn.PString(table.Rows[i][2].ToString());
				ListForCat[i].IsObsolete     = PIn.PBool  (table.Rows[i][3].ToString());
			}
		}

		public static string g(System.Windows.Forms.Control sender,string text){
			return g(sender.GetType().Name,text);
		}

		public static string g(string classType,string text){
			if(CultureInfo.CurrentCulture.TwoLetterISOLanguageName=="en"){
				return text;
			}
			if(text==""){
				return "";
			}
			if(HList==null) return text;
			//try{
			if(!HList.ContainsKey(classType+text)){
				Cur=new Language();
				Cur.ClassType=classType;
				Cur.English=text;
				//MessageBox.Show(Cur.ClassType+Cur.English);
				InsertCur();
				Refresh();
				return text;
			}
			//}
			//catch{
			//	MessageBox.Show(classType+text);
			//}
			if(LanguageForeigns.HList.Contains(classType+text)){
				return ((LanguageForeign)LanguageForeigns.HList[classType+text]).Translation;	
			}
			else{
				return text;
			}
		}

		public static void C(string classType, System.Windows.Forms.MenuItem[] item){
			for(int i=0;i<item.Length;i++){
				item[i].Text=g(classType,item[i].Text);
			}
		}

		public static void C(System.Windows.Forms.MenuItem sender, System.Windows.Forms.MenuItem[] item){
			for(int i=0;i<item.Length;i++){
				item[i].Text=g(sender.GetType().Name,item[i].Text);
			}
		}

		public static string g(System.Windows.Forms.MenuItem sender,string text){
			return g(sender.GetType().Name,text);
		}

		public static void C(string classType, System.Windows.Forms.Control[] contr){
			for(int i=0;i<contr.Length;i++){
				contr[i].Text=g(classType,contr[i].Text);
			}
		}

		public static void C(System.Windows.Forms.Control sender, System.Windows.Forms.Control[] contr){
			for(int i=0;i<contr.Length;i++){
				contr[i].Text=g(sender.GetType().Name,contr[i].Text);
			}
		}

	}//end class Lan

	public struct Language{
		public string EnglishComments;
		public string ClassType;
		public string English;
		public bool IsObsolete;
	}

	/*=========================================================================================
	=================================== class LanguageForeign ===========================================*/
	public class LanguageForeigns:DataClass{
		public static Hashtable HList;//just translations for one language
		public static LanguageForeign Cur;
		public static LanguageForeign[] List;

		public static void Refresh(){
			HList=new Hashtable();
			if(CultureInfo.CurrentCulture.TwoLetterISOLanguageName=="en"){
				return;
			}
			cmd.CommandText =
				"SELECT * from languageforeign "
				+"WHERE culture = '"+CultureInfo.CurrentCulture.TwoLetterISOLanguageName+"'";
			FillTable();
			List=new LanguageForeign[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				List[i].ClassType  = PIn.PString(table.Rows[i][0].ToString());
				List[i].English    = PIn.PString(table.Rows[i][1].ToString());
				List[i].Culture    = PIn.PString(table.Rows[i][2].ToString());
				List[i].Translation= PIn.PString(table.Rows[i][3].ToString());
				List[i].Comments   = PIn.PString(table.Rows[i][4].ToString());
				HList.Add(List[i].ClassType+List[i].English,List[i]);
			}
		}

		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO languageforeign(classtype,english,culture,translation,comments) "
				+"VALUES("
				+"'"+POut.PString(Cur.ClassType)+"', "
				+"'"+POut.PString(Cur.English)+"', "
		  	+"'"+POut.PString(Cur.Culture)+"', "
				+"'"+POut.PString(Cur.Translation)+"', "
			  +"'"+POut.PString(Cur.Comments)+"')";
			NonQ(false);
		}

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE languageforeign SET " 
				+ "translation	= '"+POut.PString(Cur.Translation)+"'"
				+ ",comments    = '"+POut.PString(Cur.Comments)+"'" 
				+" WHERE ClassType='"+Cur.ClassType+"' && "
				+"English='"+Cur.English+"' && Culture='"+CultureInfo.CurrentCulture.TwoLetterISOLanguageName+"'";
			NonQ(false);
		}

		public static void DeleteCur(){
			cmd.CommandText = "DELETE from languageforeign WHERE ClassType='"+Cur.ClassType+"' && "
				+"English = '"+Cur.English+"' && Culture = '"
				+CultureInfo.CurrentCulture.TwoLetterISOLanguageName+"'";
			NonQ(false);
		}

	}

	public struct LanguageForeign{
		public string ClassType;
		public string English;
		public string Culture;
		public string Translation;
		public string Comments;
	}

	/*=========================================================================================
	=================================== class Ledgers ===========================================*/
	//this used to be a temporary database table, but the table was deleted with version 2.0
	
	public class Ledgers:DataClass{
		public static double[] Bal;
		public static double InsEst;
		private static DateTime AsOfDate;
		public static int[] AllGuarantors;
		public struct DateValuePair{
			public DateTime Date;
			public double Value;
		}

		public static void GetAllGuarantors(){
			cmd.CommandText="SELECT DISTINCT guarantor FROM patient";
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			//MessageBox.Show(table.Rows.Count.ToString());
			AllGuarantors=new int[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				AllGuarantors[i]=PIn.PInt(table.Rows[i][0].ToString());
			}
		}

		public static DateTime GetClosestFirst(DateTime date){ 
			if(date.Day > 15){
				if(date.Month!=12){
					return new DateTime(date.Year,date.Month+1,1);		
				}
				else{ 
					return new DateTime(date.Year+1,1,1);	
				}
			}
			else{
				return new DateTime(date.Year,date.Month,1);
			}
		}

		public static void ComputeAging(int guarantor){
			DateTime asOfDate;
			if(DateTime.Today.Day > 15){
				if(DateTime.Today.Month==12){
					asOfDate=new DateTime(DateTime.Today.Year+1,1,1);
				}
				else{
					asOfDate=new DateTime(DateTime.Today.Year,DateTime.Today.Month+1,1);	
				}
			}
			else{
				asOfDate=new DateTime(DateTime.Today.Year,DateTime.Today.Month,1);
			}
			ComputeAging(guarantor,asOfDate);
			Patients.ResetAging(guarantor);
			Patients.UpdateAging(guarantor,Bal[0],Bal[1],Bal[2],Bal[3],InsEst);
		}

		public static void ComputeAging(int guarantor,DateTime asOfDate){
			AsOfDate=asOfDate;
			Bal=new double[4];
			Bal[0]=0;//0_30
			Bal[1]=0;//31_60
			Bal[2]=0;//61_90
			Bal[3]=0;//90plus
			InsEst=0;
			DateValuePair[] pairs;
			string wherePats="";
			cmd.CommandText="SELECT patnum FROM patient WHERE guarantor = '"+POut.PInt(guarantor)+"'";
			FillTable();
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0) wherePats+=" ||";
				wherePats+=" patnum = '"+table.Rows[i][0].ToString()+"'";
			}
			//PROCEDURES:
			cmd.CommandText="SELECT procdate,procfee FROM procedurelog"
				+" WHERE procstatus = '2'"
				+" && ("+wherePats+")";
			FillTable();
			pairs=new DateValuePair[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				pairs[i].Date=  PIn.PDate  (table.Rows[i][0].ToString());
				pairs[i].Value= PIn.PDouble(table.Rows[i][1].ToString());
			}
			for(int i=0;i<pairs.Length;i++){
				//MessageBox.Show(pairs[i].Date.ToShortDateString()+","+pairs[i].Value.ToString("F"));
				Bal[GetAgingType(pairs[i].Date)]+=pairs[i].Value;
			}
			//POSITIVE ADJUSTMENTS:
			cmd.CommandText="SELECT adjdate,adjamt FROM adjustment"
				+" WHERE adjamt > 0"
				+" && ("+wherePats+")";
			FillTable();
			pairs=new DateValuePair[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				pairs[i].Date=  PIn.PDate  (table.Rows[i][0].ToString());
				pairs[i].Value= PIn.PDouble(table.Rows[i][1].ToString());
			}
			for(int i=0;i<pairs.Length;i++){
				Bal[GetAgingType(pairs[i].Date)]+=pairs[i].Value;
			}
			//NEGATIVE ADJUSTMENTS:
			cmd.CommandText="SELECT adjdate,adjamt FROM adjustment"
				+" WHERE adjamt < 0"
				+" && ("+wherePats+")"
				+" ORDER BY adjdate";
			FillTable();
			pairs=new DateValuePair[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				pairs[i].Date=  PIn.PDate  (table.Rows[i][0].ToString());
				pairs[i].Value= -PIn.PDouble(table.Rows[i][1].ToString());
			}
			//MessageBox.Show(Bal[3].ToString()+","+pairs[0].Value.ToString());
			ComputePayments(pairs);
			//MessageBox.Show(Bal[3].ToString());
			//CLAIM PAYMENTS:
			//there are different ways to calculate the date of a claim payment
			//for now, we are trying to keep it consistent with the layout in the account module.
			//Using date of service.  Later will include alternative for date received.
			cmd.CommandText="SELECT dateservice,inspayamt FROM claim"
				+" WHERE ClaimStatus = 'R'"//this also effectively eliminates 'A' adjustments
				+" && ("+wherePats+")"
				+" ORDER BY dateservice";
			FillTable();
			pairs=new DateValuePair[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				pairs[i].Date=  PIn.PDate  (table.Rows[i][0].ToString());
				pairs[i].Value= PIn.PDouble(table.Rows[i][1].ToString());
			}
			ComputePayments(pairs);
			//PAYSPLITS:
			cmd.CommandText="SELECT procdate,splitamt FROM paysplit"
				+" WHERE"
				+wherePats
				+" ORDER BY procdate";
			FillTable();
			pairs=new DateValuePair[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				pairs[i].Date=  PIn.PDate  (table.Rows[i][0].ToString());
				pairs[i].Value= PIn.PDouble(table.Rows[i][1].ToString());
			}
			ComputePayments(pairs);
			//CLAIM ESTIMATES
			cmd.CommandText="SELECT inspayest FROM claim"
				+" WHERE claimstatus != 'R' && claimstatus != 'A'"
				+" && claimtype != 'PreAuth'"
				+" && ("+wherePats+")";
				//+" ORDER BY procdate";
			FillTable();
			//pairs=new DateValuePair[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				InsEst+=PIn.PDouble(table.Rows[i][0].ToString());
			}
			//must complete by updating patient table. Done from wherever this was called.
		}

		private static void ComputePayments(DateValuePair[] pairs){
			for(int i=0;i<pairs.Length;i++){
				switch(GetAgingType(pairs[i].Date)){
					case 3://over 90
						Bal[3]-=pairs[i].Value;//can go negative if patient balance was negative at some point
						break;
					case 2://60 90
						if(Bal[3]>0){//apply to older balance first
							if(Bal[3]>pairs[i].Value){
								Bal[3]-=pairs[i].Value;//apply all to over 90 bal
								pairs[i].Value=0;
							}
							else{
								pairs[i].Value-=Bal[3];//deduct amount applied
								Bal[3]=0;//apply only part to over 90
							}
						}
						Bal[2]-=pairs[i].Value;//apply whatever is left over to 60 90
						break;
					case 1://30 60
						if(Bal[3]>0){//apply to older balance first
							if(Bal[3]>pairs[i].Value){
								Bal[3]-=pairs[i].Value;
								pairs[i].Value=0;
							}
							else{
								pairs[i].Value-=Bal[3];
								Bal[3]=0;
							}
						}
						if(Bal[2]>0){
							if(Bal[2]>pairs[i].Value){
								Bal[2]-=pairs[i].Value;
								pairs[i].Value=0;
							}
							else{
								pairs[i].Value-=Bal[2];
								Bal[2]=0;
							}
						}
						Bal[1]-=pairs[i].Value;//apply whatever is left over to 30 60
						break;
					case 0://0 30
						if(Bal[3]>0){
							if(Bal[3]>pairs[i].Value){
								Bal[3]-=pairs[i].Value;
								pairs[i].Value=0;
							}
							else{
								pairs[i].Value-=Bal[3];
								Bal[3]=0;
							}
						}
						if(Bal[2]>0){
							if(Bal[2]>pairs[i].Value){
								Bal[2]-=pairs[i].Value;
								pairs[i].Value=0;
							}
							else{
								pairs[i].Value-=Bal[2];
								Bal[2]=0;
							}
						}
						if(Bal[1]>0){
							if(Bal[1]>pairs[i].Value){
								Bal[1]-=pairs[i].Value;
								pairs[i].Value=0;
							}
							else{
								pairs[i].Value-=Bal[1];
								Bal[1]=0;
							}
						}
						Bal[0]-=pairs[i].Value;//apply whatever is left over to 0 30
						break;
				}
				

			}
		}

		private static int GetAgingType(DateTime date){
			//MessageBox.Show(AsOfDate.ToShortDateString()+","+date.ToShortDateString());
			int retVal;
			if(date < AsOfDate.AddMonths(-3))
				retVal= 3;
			else if(date < AsOfDate.AddMonths(-2))
				retVal= 2;
			else if(date < AsOfDate.AddMonths(-1))
				retVal= 1;
			else 
				retVal= 0;
			//MessageBox.Show(retVal.ToString());
			return retVal;
		}

			//Patients.UpdateAging(PIn.PInt(row[0].ToString()),PIn.PDouble(row[4].ToString()),PIn.PDouble(row[3].ToString()),
			//	PIn.PDouble(row[2].ToString()),PIn.PDouble(row[1].ToString()),PIn.PDouble(row[5].ToString()));
			//Prefs.Cur=(Pref)Prefs.HList["DateLastAging"];
			//Prefs.Cur.ValueString=POut.PDate(DateTime.Today);
			//Prefs.UpdateCur();
	
	}


}//end namespace













