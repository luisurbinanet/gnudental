using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the lettermergefield table in the database.</summary>
	public class LetterMergeField{
		///<summary>Primary key.</summary>
		public int FieldNum;
		///<summary>Foreign key to lettermerge.LetterMergeNum.</summary>
		public int LetterMergeNum;
		///<summary>One of the preset available field names.</summary>
		public string FieldName;

		/*//<summary>Returns a copy of the clearinghouse.</summary>
    public ClaimForm Copy(){
			ClaimForm cf=new ClaimForm();
			cf.ClaimFormNum=ClaimFormNum;
			cf.Description=Description;
			return cf;
		}*/

		///<summary>Inserts this lettermergefield into database.</summary>
		public void Insert(){
			string command="INSERT INTO lettermergefield (LetterMergeNum,FieldName"
				+") VALUES("
				+"'"+POut.PInt   (LetterMergeNum)+"', "
				+"'"+POut.PString(FieldName)+"')";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
			//LetterMergeNum=dcon.InsertID;
		}

		/*
		///<summary></summary>
		public void Update(){
			string command="UPDATE lettermergefield SET "
				+"LetterMergeNum = '"+POut.PInt   (LetterMergeNum)+"' "
				+",FieldName = '"    +POut.PString(FieldName)+"' "
				+"WHERE FieldNum = '"+POut.PInt(FieldNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}*/

		/*
		///<summary></summary>
		public void Delete(){
			string command="DELETE FROM lettermergefield "
				+"WHERE FieldNum = "+POut.PInt(FieldNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}*/
		
	}

/*=========================================================================================
		=================================== class LetterMergeFields=======================================*/

	///<summary></summary>
	public class LetterMergeFields{
		///<summary>List of all lettermergeFields.</summary>
		private static LetterMergeField[] List;
		
		///<summary></summary>
		public static void Refresh(){
			string command=
				"SELECT * FROM lettermergefield "
				+"ORDER BY FieldName";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			List=new LetterMergeField[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new LetterMergeField();
				List[i].FieldNum      = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].LetterMergeNum= PIn.PInt   (table.Rows[i][1].ToString());
				List[i].FieldName     = PIn.PString(table.Rows[i][2].ToString());
			}
		}

		///<summary>Called from LetterMerge.Refresh() to get all field names for a given letter.  The arrayList is a collection of strings representing field names.</summary>
		public static ArrayList GetForLetter(int letterMergeNum){
			ArrayList retVal=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].LetterMergeNum==letterMergeNum){
					retVal.Add(List[i].FieldName);
				}
			}
			return retVal;
		}

		///<summary>Deletes all lettermergefields for the given letter.  This is then followed by adding them all back, which is simpler than just updating.</summary>
		public static void DeleteForLetter(int letterMergeNum){
			string command="DELETE FROM lettermergefield "
				+"WHERE LetterMergeNum = "+POut.PInt(letterMergeNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		
		


	}

	



}









