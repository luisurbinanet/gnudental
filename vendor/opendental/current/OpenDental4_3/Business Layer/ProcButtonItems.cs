using System;
using System.Collections;
using System.Data;

namespace OpenDental{
	
	///<summary>Corresponds to the procbuttonitem table in the database.</summary>
	public class ProcButtonItem{
		///<summary>Primary key.</summary>
		public int ProcButtonItemNum;
		///<summary>Foreign key to procbutton.ProcButtonNum.</summary>
		public int ProcButtonNum;
		///<summary>Foreign key to procedurecode.ADACode.</summary>
		public string ADACode;
		///<summary>Foreign key to autocode.AutoCodeNum.</summary>
		public int AutoCodeNum;

		///<summary></summary>
		public ProcButtonItem Copy() {
			ProcButtonItem p=new ProcButtonItem();
			p.ProcButtonItemNum=ProcButtonItemNum;
			p.ProcButtonNum=ProcButtonNum;
			p.ADACode=ADACode;
			p.AutoCodeNum=AutoCodeNum;
			return p;
		}

		///<summary>Must have already checked ADACode for nonduplicate.</summary>
		public void Insert() {
			string command="INSERT INTO procbuttonitem (procbuttonnum,adacode,autocodenum) VALUES("
				+"'"+POut.PInt   (ProcButtonNum)+"', "
				+"'"+POut.PString(ADACode)+"', "
				+"'"+POut.PInt   (AutoCodeNum)+"')";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Update() {
			string command="UPDATE procbuttonitem SET " 
				+ "procbuttonnum='"+POut.PInt   (ProcButtonNum)+"'"
				+ ",adacode='"     +POut.PString(ADACode)+"'"
				+ ",autocodenum='" +POut.PInt   (AutoCodeNum)+"'"
				+" WHERE procbuttonitemnum = '"+POut.PInt(ProcButtonItemNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Delete() {
			string command="DELETE from procbuttonitem WHERE procbuttonitemnum = '"+POut.PInt(ProcButtonItemNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}


	}

	/*=========================================================================================
		=================================== class ProcButtonItems===========================================*/

	///<summary></summary>
	public class ProcButtonItems{
		///<summary></summary>
		public static ProcButtonItem[] List;

		///<summary>Fills List in preparation for later usage.</summary>
		public static void Refresh(){
			string command="SELECT * FROM procbuttonitem";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			List=new ProcButtonItem[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new ProcButtonItem();
				List[i].ProcButtonItemNum=PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ProcButtonNum    =PIn.PInt   (table.Rows[i][1].ToString());
				List[i].ADACode          =PIn.PString(table.Rows[i][2].ToString());
				List[i].AutoCodeNum      =PIn.PInt   (table.Rows[i][3].ToString());
			}
		}

		///<summary></summary>
		public static string[] GetADAListForButton(int procButtonNum){
			ArrayList ALadaCodes=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcButtonNum==procButtonNum && List[i].AutoCodeNum==0){
					ALadaCodes.Add(List[i].ADACode);
				} 
			}
			string[] adaCodeList=new string[ALadaCodes.Count];
			if(ALadaCodes.Count > 0){
				ALadaCodes.CopyTo(adaCodeList);
			}
			return adaCodeList;
		}

		///<summary></summary>
		public static int[] GetAutoListForButton(int procButtonNum) {
			ArrayList ALautoCodes=new ArrayList();
			for(int i=0;i<List.Length;i++) {
				if(List[i].ProcButtonNum==procButtonNum && List[i].AutoCodeNum > 0){
					ALautoCodes.Add(List[i].AutoCodeNum);
				}
			}
			int[] autoCodeList=new int[ALautoCodes.Count];
			if(ALautoCodes.Count > 0) {
				ALautoCodes.CopyTo(autoCodeList);
			}
			return autoCodeList;
		}

		///<summary></summary>
		public static void DeleteAllForButton(int procButtonNum){
			string command= "DELETE from procbuttonitem WHERE procbuttonnum = '"+POut.PInt(procButtonNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

	}

	




}










