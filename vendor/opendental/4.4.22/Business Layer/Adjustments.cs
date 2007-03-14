using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>An adjustment in the patient account.  Usually, adjustments are very simple, just being assigned to one patient and provider.  But they can also be attached to a procedure to represent a discount on that procedure.  Attaching adjustments to procedures is not automated, so it is not very common.</summary>
	public class Adjustment{
		///<summary>Primary key.</summary>
		public int AdjNum;
		///<summary>The date that the adjustment shows in the patient account.</summary>
		public DateTime AdjDate;
		///<summary>Amount of adjustment.  Can be pos or neg.</summary>
		public double AdjAmt;
		///<summary>FK to patient.PatNum.</summary>
		public int PatNum;
		///<summary>FK to definition.DefNum.</summary>
		public int AdjType;
		///<summary>FK to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>Note for this adjustment.</summary>
		public string AdjNote;
		///<summary>Procedure date.  Not when the adjustment was entered.  This is what the aging will be based on in a future version.</summary>
		public DateTime ProcDate;
		///<summary>FK to procedurelog.ProcNum.  Only used if attached to a procedure.  Otherwise, 0.</summary>
		public int ProcNum;
		///<summary>Timestamp automatically generated and user not allowed to change.  The actual date of entry.</summary>
		public DateTime DateEntry;

		/*///<summary>Returns a copy of this Adjustment.</summary>
		public Adjustment Copy(){
			Adjustment a=new Adjustment();
			a.AdjNum=AdjNum;
			//etc
			return a;
		}*/

		///<summary></summary>
		private void Update(){
			string command="UPDATE adjustment SET " 
				+ "adjdate = '"      +POut.PDate  (AdjDate)+"'"
				+ ",adjamt = '"      +POut.PDouble(AdjAmt)+"'"
				+ ",patnum = '"      +POut.PInt   (PatNum)+"'"
				+ ",adjtype = '"     +POut.PInt   (AdjType)+"'"
				+ ",provnum = '"     +POut.PInt   (ProvNum)+"'"
				+ ",adjnote = '"     +POut.PString(AdjNote)+"'"
				+ ",ProcDate = '"    +POut.PDate  (ProcDate)+"'"
				+ ",ProcNum = '"     +POut.PInt   (ProcNum)+"'"
				//DateEntry not allowed to change
				+" WHERE adjNum = '" +POut.PInt   (AdjNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		private void Insert(){
			if(Prefs.RandomKeys){
				AdjNum=MiscData.GetKey("adjustment","AdjNum");
			}
			string command= "INSERT INTO adjustment (";
			if(Prefs.RandomKeys){
				command+="AdjNum,";
			}
			command+="AdjDate,AdjAmt,PatNum, "
				+"AdjType,ProvNum,AdjNote,ProcDate,ProcNum,DateEntry) VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(AdjNum)+"', ";
			}
			command+=
				 "'"+POut.PDate  (AdjDate)+"', "
				+"'"+POut.PDouble(AdjAmt)+"', "
				+"'"+POut.PInt   (PatNum)+"', "
				+"'"+POut.PInt   (AdjType)+"', "
				+"'"+POut.PInt   (ProvNum)+"', "
				+"'"+POut.PString(AdjNote)+"', "
				+"'"+POut.PDate  (ProcDate)+"', "
				+"'"+POut.PInt   (ProcNum)+"', "
				+"NOW())";//DateEntry set to server date
			DataConnection dcon=new DataConnection();
			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				AdjNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void InsertOrUpdate(bool IsNew){
			//if(){
				//throw new Exception(Lan.g(this,""));
			//}
			if(IsNew){
				Insert();
			}
			else{
				Update();
			}
		}

		///<summary>This will soon be eliminated or changed to only allow deleting on same day as EntryDate.</summary>
		public void Delete(){
			string command="DELETE FROM adjustment "
				+"WHERE AdjNum = '"+AdjNum.ToString()+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

	}

	/*=========================================================================================
	=================================== class Adjustments ==========================================*/

	///<summary>Handles database commands related to the adjustment table in the db.</summary>
	public class Adjustments{

		///<summary>Gets all adjustments for a single patient.</summary>
		public static Adjustment[] Refresh(int patNum){
			string command=
				"SELECT * FROM adjustment"
				+" WHERE PatNum = '"+patNum.ToString()+"' ORDER BY AdjDate";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			Adjustment[] List=new Adjustment[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Adjustment();
				List[i].AdjNum   = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].AdjDate  = PIn.PDate  (table.Rows[i][1].ToString());
				List[i].AdjAmt   = PIn.PDouble(table.Rows[i][2].ToString());
				List[i].PatNum   = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].AdjType  = PIn.PInt   (table.Rows[i][4].ToString());
				List[i].ProvNum  = PIn.PInt   (table.Rows[i][5].ToString());
				List[i].AdjNote  = PIn.PString(table.Rows[i][6].ToString());
				List[i].ProcDate = PIn.PDate  (table.Rows[i][7].ToString());
				List[i].ProcNum  = PIn.PInt   (table.Rows[i][8].ToString());
				List[i].DateEntry= PIn.PDate  (table.Rows[i][9].ToString());
			}
			return List;
		}

		///<summary>Loops through the supplied list of adjustments and returns an ArrayList of adjustments for the given proc.</summary>
		public static ArrayList GetForProc(int procNum,Adjustment[] List){
			ArrayList retVal=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum){
					retVal.Add(List[i]);
				}
			}
			return retVal;
		}

		///<summary>Used from ContrAccount and ProcEdit to display and calculate adjustments attached to procs.</summary>
		public static double GetTotForProc(int procNum,Adjustment[] List){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum){
					retVal+=List[i].AdjAmt;
				}
			}
			return retVal;
		}

		///<summary>Must make sure Refresh is done first.  Returns the sum of all adjustments for this patient.  Amount might be pos or neg.</summary>
		public static double ComputeBal(Adjustment[] List){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				retVal+=List[i].AdjAmt;
			}
			return retVal;
		}
	}

	


	


}









