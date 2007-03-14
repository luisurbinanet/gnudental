/*   OBSOLETE
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the periosequence table in the database.</summary>
	public struct PerioSequence{
		///<summary>Primary key.</summary>
		public int PerioSequenceNum;
		///<summary>Foreign key to perioexam.PerioExamNum.</summary>
		public int PerioExamNum;
		///<summary>eg probing, mobility, recession, etc.</summary>
		public PerioSequenceType SequenceType;
	}

	=========================================================================================
	=================================== class PerioSequences ==========================================

	///<summary></summary>
	public class PerioSequences:DataClass{
		///<summary>List of all perio sequences for the current exam.</summary>
		public static PerioSequence[,] List;
		///<summary>The current perio sequence</summary>
		public static PerioSequence Cur;

		///<summary>Gets all periosequences for the current patient. Some sequences will have no measurements</summary>
		public static void RefreshForExam(){
			cmd.CommandText =
				"SELECT periosequence.* from periosequence,perioexam"
				+" WHERE perioexam.PerioExamNum = periosequence.PerioExamNum"
				+" && perioexam.PatNum = '"+Patients.Cur.PatNum.ToString()+"'"
				+" ORDER BY perioexam.ExamDate,periosequence.PerioSequenceNum";
			FillTable();
			List=new PerioSequence[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].PerioSequenceNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PerioExamNum    = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].SequenceType    = (PerioSequenceType)PIn.PInt   (table.Rows[i][2].ToString());
			}
		}

		not used
		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE periosequence SET "
				+ "PerioSequence = '"     +POut.PInt   (Cur.PatNum)+"'"
				+",PerioExamNum = '"   +POut.PDate  (Cur.ExamDate)+"'"
				+" WHERE PerioExamNum = '"+POut.PInt(Cur.PerioExamNum)+"'";
			NonQ();
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO periosequence (PerioExamNum,SequenceType"
				+") VALUES("
				+"'"+POut.PInt   (Cur.PerioExamNum)+"', "
				+"'"+POut.PInt   ((int)Cur.SequenceType)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.PerioSequenceNum=InsertID;
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from periosequence WHERE PerioSequenceNum = '"
				+Cur.PerioSequenceNum.ToString()+"'";
			NonQ();
		}

		///<summary>Used by PerioMeasures when refreshing to organize array.</summary>
		public static int GetSeqIndex(int perioSequenceNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].PerioSequenceNum==perioSequenceNum){
					return i;
				}
			}
			MessageBox.Show("Error. PerioSequenceNum not in list: "+perioSequenceNum.ToString());
			return 0;
		}
	
	}
	
	

}

*/













