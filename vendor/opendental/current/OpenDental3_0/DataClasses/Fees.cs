
using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the fee table in the database.</summary>
	public struct Fee{
		///<summary>Primary key.</summary>
		public int FeeNum;
		///<summary>The amount usually charged.</summary>
		public double Amount;
		///<summary>Foreign key to procedurelog.ADACode.</summary>
		public string ADACode;
		///<summary>Foreign key to definition.DefNum.</summary>
		public int FeeSched;
		///<summary>Not used.</summary>
		public bool UseDefaultFee;
		///<summary>Not used.</summary>
		public bool UseDefaultCov;
	}
	
	/*=========================================================================================
		=================================== class Fees ===========================================*/
	///<summary></summary>
	public class Fees:DataClass{
		///<summary></summary>
		public static Fee Cur;
		///<summary></summary>
		public static Hashtable[] HList;
		
		///<summary></summary>
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

		///<summary></summary>
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

		///<summary></summary>
		public static void UpdateCur(){
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

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO fee (amount,adacode,"
				+"feesched,usedefaultfee,usedefaultcov) VALUES("
				+"'"+POut.PDouble(Cur.Amount)+"', "
				+"'"+POut.PString(Cur.ADACode)+"', "
				+"'"+POut.PInt   (Cur.FeeSched)+"', "
				+"'"+POut.PBool  (Cur.UseDefaultFee)+"', "
				+"'"+POut.PBool  (Cur.UseDefaultCov)+"')";
			NonQ(false);
		}

		///<summary></summary>
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

		///<summary></summary>
		public static double GetAmount(string adacode, int feeSched){
			if(adacode==null)
				return 0;
			if(feeSched==0)
				return 0;
			int i=Defs.GetOrder(DefCat.FeeSchedNames,feeSched);
			if(i==-1){
				return 0;//you cannot obtain fees for hidden fee schedules
			}
			if(HList[i].Contains(adacode)){
				return ((Fee)HList[i][adacode]).Amount;
			}
			return 0;//code not found
		}


	}

	

}