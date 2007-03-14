using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the perioexam table in the database.</summary>
	public struct PerioExam{
		///<summary>Primary key.</summary>
		public int PerioExamNum;
		///<summary>Foreign key to patient.PatNum.</summary>
		public int PatNum;
		///<summary></summary>
		public DateTime ExamDate;
		///<summary>Foreign key to provider.ProvNum.</summary>
		public int ProvNum;
	}

	/*=========================================================================================
	=================================== class PerioExams ==========================================*/

	///<summary></summary>
	public class PerioExams:DataClass{
		///<summary>List of all perio exams for the current patient.</summary>
		public static PerioExam[] List;
		///<summary>The current exam</summary>
		public static PerioExam Cur;

		///<summary>Most recent date last.  All exams loaded, even if not displayed. Also refreshes all related Measurement data.</summary>
		public static void Refresh(int patNum){
			cmd.CommandText =
				"SELECT * from perioexam"
				+" WHERE PatNum = '"+patNum.ToString()+"'"
				+" ORDER BY perioexam.ExamDate";
			FillTable();
			List=new PerioExam[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].PerioExamNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum      = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].ExamDate    = PIn.PDate  (table.Rows[i][2].ToString());
				List[i].ProvNum     = PIn.PInt   (table.Rows[i][3].ToString());
			}
			PerioMeasures.Refresh(patNum);
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE perioexam SET "
				+ "PatNum = '"     +POut.PInt   (Cur.PatNum)+"'"
				+",ExamDate = '"   +POut.PDate  (Cur.ExamDate)+"'"
				+",ProvNum = '"    +POut.PInt   (Cur.ProvNum)+"'"
				+" WHERE PerioExamNum = '"+POut.PInt(Cur.PerioExamNum)+"'";
			NonQ();
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO perioexam (PatNum,ExamDate,ProvNum"
				+") VALUES("
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PDate  (Cur.ExamDate)+"', "
				+"'"+POut.PInt   (Cur.ProvNum)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.PerioExamNum=InsertID;
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from perioexam WHERE PerioExamNum = '"+Cur.PerioExamNum.ToString()+"'";
			NonQ();
			cmd.CommandText = "DELETE from periomeasure WHERE PerioExamNum = '"+Cur.PerioExamNum.ToString()+"'";
			NonQ();
		}

		///<summary>Used by PerioMeasures when refreshing to organize array.</summary>
		public static int GetExamIndex(int perioExamNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].PerioExamNum==perioExamNum){
					return i;
				}
			}
			MessageBox.Show("Error. PerioExamNum not in list: "+perioExamNum.ToString());
			return 0;
		}

	
	}
	
	

}















