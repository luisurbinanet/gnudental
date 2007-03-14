using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the autocodecond table in the database.</summary>
	///<remarks>There is usually only one or two conditions for a given AutoCodeItem.</remarks>
	public struct AutoCodeCond{//
		///<summary>Primary key.</summary>
		public int AutoCodeCondNum;
		///<summary>Foreign key to AutoCodeItem.AutoCodeItemNum.</summary>
		public int AutoCodeItemNum;
		///<summary>See the AutoCondition enumeration.</summary>
		public AutoCondition Condition;
	}

	/*=========================================================================================
	=================================== class AutoCodeConds ===========================================*/
  ///<summary></summary>
	public class AutoCodeConds:DataClass{
		///<summary></summary>
		public static AutoCodeCond Cur;
		///<summary></summary>
		public static AutoCodeCond[] List;
		///<summary></summary>
		public static AutoCodeCond[] ListForItem;
		private static ArrayList ALlist;
		//public static Hashtable HList; 

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from autocodecond ORDER BY condition";
			FillTable();
			//HList=new Hashtable();
			List=new AutoCodeCond[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i].AutoCodeCondNum= PIn.PInt        (table.Rows[i][0].ToString());
				List[i].AutoCodeItemNum= PIn.PInt        (table.Rows[i][1].ToString());
				List[i].Condition=(AutoCondition)PIn.PInt(table.Rows[i][2].ToString());	
				//HList.Add(List[i].AutoCodeItemNum,List[i]);
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO autocodecond (autocodeitemnum,condition) "
				+"VALUES ("
				+"'"+POut.PInt(Cur.AutoCodeItemNum)+"', "
				+"'"+POut.PInt((int)Cur.Condition)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.AutoCodeCondNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE autocodecond SET "
				+"autocodeitemnum='"+POut.PInt(Cur.AutoCodeItemNum)+"'"
				+",condition ='"     +POut.PInt((int)Cur.Condition)+"'"
				+" WHERE autocodecondnum = '"+POut.PInt(Cur.AutoCodeCondNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from autocodecond WHERE autocodecondnum = '"
				+POut.PInt(Cur.AutoCodeCondNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteForItemNum(int itemNum){
			cmd.CommandText = "DELETE from autocodecond WHERE autocodeitemnum = '"
				+POut.PInt(itemNum)+"'";//AutoCodeItems.Cur.AutoCodeItemNum)
			NonQ(false); 
		}

		///<summary></summary>
		public static void GetListForItem(int autoCodeItemNum){
			ALlist=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].AutoCodeItemNum==autoCodeItemNum){
					ALlist.Add(List[i]);
				} 
			}
			ListForItem=new AutoCodeCond[ALlist.Count];
			if(ALlist.Count > 0){			
				ALlist.CopyTo(ListForItem);
			}     
		}

		///<summary></summary>
		public static bool IsSurf(AutoCondition myAutoCondition){
			switch(myAutoCondition){
				case AutoCondition.One_Surf:
				case AutoCondition.Two_Surf:
				case AutoCondition.Three_Surf:
				case AutoCondition.Four_Surf:
				case AutoCondition.Five_Surf:
					return true;
				default:
					return false;
			}
		}

		///<summary></summary>
		public static bool ConditionIsMet(AutoCondition myAutoCondition, string toothNum,string surf,bool isAdditional){//MissingTeeth is already available for given patient
			switch(myAutoCondition){
				case AutoCondition.Anterior:
					return Tooth.IsAnterior(toothNum);
				case AutoCondition.Posterior:
					return Tooth.IsPosterior(toothNum);
				case AutoCondition.Premolar:
					return Tooth.IsPreMolar(toothNum);
				case AutoCondition.Molar:
					return Tooth.IsMolar(toothNum);
				case AutoCondition.One_Surf:
					return surf.Length==1;
				case AutoCondition.Two_Surf:
					return surf.Length==2;
				case AutoCondition.Three_Surf:
					return surf.Length==3;
				case AutoCondition.Four_Surf:
					return surf.Length==4;
				case AutoCondition.Five_Surf:
					return surf.Length==5;
				case AutoCondition.First:
					return !isAdditional;
				case AutoCondition.EachAdditional:
					return isAdditional;
				case AutoCondition.Maxillary:
					return Tooth.IsMaxillary(toothNum);
				case AutoCondition.Mandibular:
					return !Tooth.IsMaxillary(toothNum);
				case AutoCondition.Primary:
					return Tooth.IsPrimary(toothNum);
				case AutoCondition.Permanent:
					return !Tooth.IsPrimary(toothNum);
				case AutoCondition.Pontic:
					return Procedures.MissingTeeth.Contains(toothNum);
				case AutoCondition.Retainer:
					return !Procedures.MissingTeeth.Contains(toothNum);
				default:
					return false;
			}//switch
		}//Condition is met

	}

	

	


}









