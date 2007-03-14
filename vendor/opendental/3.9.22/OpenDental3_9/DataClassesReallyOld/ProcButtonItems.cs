using System;
using System.Collections;

namespace OpenDental{
	
	///<summary>Corresponds to the procbuttonitem table in the database.</summary>
	public struct ProcButtonItem{
		///<summary>Primary key.</summary>
		public int ProcButtonItemNum;
		///<summary>Foreign key to procbutton.ProcButtonNum.</summary>
		public int ProcButtonNum;
		///<summary>Foreign key to procedurecode.ADACode.</summary>
		public string ADACode;
		///<summary>Foreign key to autocode.AutoCodeNum.</summary>
		public int AutoCodeNum;
	}

	/*=========================================================================================
		=================================== class ProcButtonItems===========================================*/

	///<summary></summary>
	public class ProcButtonItems:DataClass{
		///<summary></summary>
		public static ProcButtonItem Cur;
		///<summary></summary>
		public static Hashtable HList;
		///<summary></summary>
		public static ProcButtonItem[] List;
		//public static ProcButtonItem[] ListForButton;
		//these two used when editing buttons:
		///<summary></summary>
		public static ArrayList ALadaCodes;//string, because some buttonItems are adacodes
		///<summary></summary>
		public static ArrayList ALautoCodes;//int, and some are autocodes
		//these two used when clicking buttons:
		///<summary></summary>
		public static string[] adaCodeList;
		///<summary></summary>
		public static int[] autoCodeList;
		//private static ArrayList ALlist;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText = 
				"SELECT * FROM procbuttonitem";
			FillTable();
			HList=new Hashtable();
			List=new ProcButtonItem[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				List[i].ProcButtonItemNum=PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ProcButtonNum    =PIn.PInt   (table.Rows[i][1].ToString());
				List[i].ADACode          =PIn.PString(table.Rows[i][2].ToString());
				List[i].AutoCodeNum      =PIn.PInt   (table.Rows[i][3].ToString());
				HList.Add(List[i].ProcButtonItemNum,List[i]);
			}
		}

		///<summary></summary>
		public static void GetListsForButton(int procButtonNum){
			//ArrayList ALtemp=new ArrayList();
			ALadaCodes=new ArrayList();
			ALautoCodes=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcButtonNum==procButtonNum){
					//ALtemp.Add(List[i]);
					if(List[i].AutoCodeNum > 0){
						ALautoCodes.Add(List[i].AutoCodeNum);
					}
					else{
						ALadaCodes.Add(List[i].ADACode);
					}
				} 
			}
			/*
			ListForButton=new ProcButtonItem[ALtemp.Count];
			if(ALtemp.Count > 0){
				ALtemp.CopyTo(ListForButton);
			}*/
			autoCodeList=new int[ALautoCodes.Count];
			if(ALautoCodes.Count > 0){
				ALautoCodes.CopyTo(autoCodeList);
			}
			adaCodeList=new string[ALadaCodes.Count];
			if(ALadaCodes.Count > 0){
				ALadaCodes.CopyTo(adaCodeList);
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			//must have already checked ADACode for nonduplicate.
			cmd.CommandText = "INSERT INTO procbuttonitem (procbuttonnum,adacode,autocodenum) VALUES("
				+"'"+POut.PInt   (Cur.ProcButtonNum)+"', "
				+"'"+POut.PString(Cur.ADACode)+"', "
				+"'"+POut.PInt   (Cur.AutoCodeNum)+"')";  

			NonQ(false);
		}

		///<summary></summary>
		public static void UpdateCur(){
			//MessageBox.Show("Updating");
			cmd.CommandText = "UPDATE procbuttonitem SET " 
				+ "procbuttonnum='"+POut.PInt   (Cur.ProcButtonNum)+"'"
				+ ",adacode='"     +POut.PString(Cur.ADACode)+"'"
				+ ",autocodenum='" +POut.PInt   (Cur.AutoCodeNum)+"'"
				+" WHERE procbuttonitemnum = '"+POut.PInt(Cur.ProcButtonItemNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from procbuttonitem WHERE procbuttonitemnum = '"+POut.PInt(Cur.ProcButtonItemNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteAllForCur(){
			cmd.CommandText = "DELETE from procbuttonitem WHERE procbuttonnum = '"+POut.PInt(ProcButtons.Cur.ProcButtonNum)+"'";
			NonQ(false);
		}

	}

	




}










