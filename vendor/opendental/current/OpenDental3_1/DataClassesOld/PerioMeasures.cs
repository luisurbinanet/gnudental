using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the periomeasure table in the database.</summary>
	public struct PerioMeasure{
		///<summary>Primary key.</summary>
		public int PerioMeasureNum;
		///<summary>Foreign key to perioexam.PerioExamNum.</summary>
		public int PerioExamNum;
		///<summary>eg probing, mobility, recession, etc.</summary>
		public PerioSequenceType SequenceType;
		///<summary>Valid values are 1-32. Every measurement must be associated with a tooth.</summary>
		public int IntTooth;
		///<summary>This is used when the measurement does not apply to a surface(mobility and skiptooth).  Valid values for all surfaces are 0 through 19, or -1 to represent no measurement taken.</summary>
		public int ToothValue;
		///<summary></summary>
		public int MBvalue;
		///<summary></summary>
		public int Bvalue;
		///<summary></summary>
		public int DBvalue;
		///<summary></summary>
		public int MLvalue;
		///<summary></summary>
		public int Lvalue;
		///<summary></summary>
		public int DLvalue;
	}

	/*=========================================================================================
	=================================== class PerioMeasures ==========================================*/

	///<summary></summary>
	public class PerioMeasures:DataClass{
		///<summary>List of all perio measures for the current patient. Dim 1 is exams. Dim 2 is Sequences. Dim 3 is Measurements, always 33 per sequence(0 is not used).</summary>
		public static PerioMeasure[,,] List;
		///<summary>The current perio measurement</summary>
		public static PerioMeasure Cur;

		///<summary>Gets all measurements for the current patient, then organizes them by exam and sequence.</summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT periomeasure.*,perioexam.ExamDate"
				+" FROM periomeasure,perioexam"
				+" WHERE periomeasure.PerioExamNum = perioexam.PerioExamNum"
				+" && perioexam.PatNum = '"+Patients.Cur.PatNum.ToString()+"'"
				+" ORDER BY perioexam.ExamDate";
			FillTable();
			List=new PerioMeasure[PerioExams.List.Length,Enum.GetNames(typeof(PerioSequenceType)).Length,33];
			int curExamI=0;
			for(int i=0;i<table.Rows.Count;i++){
				Cur.PerioMeasureNum =PIn.PInt   (table.Rows[i][0].ToString());
				Cur.PerioExamNum    =PIn.PInt   (table.Rows[i][1].ToString());
				Cur.SequenceType    =(PerioSequenceType)PIn.PInt   (table.Rows[i][2].ToString());
				Cur.IntTooth        =PIn.PInt   (table.Rows[i][3].ToString());
				Cur.ToothValue      =PIn.PInt   (table.Rows[i][4].ToString());
				Cur.MBvalue         =PIn.PInt   (table.Rows[i][5].ToString());
				Cur.Bvalue          =PIn.PInt   (table.Rows[i][6].ToString());
				Cur.DBvalue         =PIn.PInt   (table.Rows[i][7].ToString());
				Cur.MLvalue         =PIn.PInt   (table.Rows[i][8].ToString());
				Cur.Lvalue          =PIn.PInt   (table.Rows[i][9].ToString());
				Cur.DLvalue         =PIn.PInt   (table.Rows[i][10].ToString());
				//perioexam.ExamDate                           11
				//the next statement can also handle exams with no measurements:
				if(i==0//if this is the first row
					|| table.Rows[i][1].ToString() != table.Rows[i-1][1].ToString())//or examNum has changed
				{
					curExamI=PerioExams.GetExamIndex(PIn.PInt(table.Rows[i][1].ToString()));
				}
				List[curExamI,(int)Cur.SequenceType,Cur.IntTooth]=Cur;
			}
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE periomeasure SET "
				+ "PerioExamNum = '"+POut.PInt   (Cur.PerioExamNum)+"'"
				+",SequenceType = '"+POut.PInt   ((int)Cur.SequenceType)+"'"
				+",IntTooth = '"    +POut.PInt   (Cur.IntTooth)+"'"
				+",ToothValue = '"  +POut.PInt   (Cur.ToothValue)+"'"
				+",MBvalue = '"     +POut.PInt   (Cur.MBvalue)+"'"
				+",Bvalue = '"      +POut.PInt   (Cur.Bvalue)+"'"
				+",DBvalue = '"     +POut.PInt   (Cur.DBvalue)+"'"
				+",MLvalue = '"     +POut.PInt   (Cur.MLvalue)+"'"
				+",Lvalue = '"      +POut.PInt   (Cur.Lvalue)+"'"
				+",DLvalue = '"     +POut.PInt   (Cur.DLvalue)+"'"
				+" WHERE PerioMeasureNum = '"+POut.PInt(Cur.PerioMeasureNum)+"'";
			NonQ();
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO periomeasure (PerioExamNum,SequenceType,IntTooth,ToothValue,"
				+"MBvalue,Bvalue,DBvalue,MLvalue,Lvalue,DLvalue"
				+") VALUES("
				+"'"+POut.PInt   (Cur.PerioExamNum)+"', "
				+"'"+POut.PInt   ((int)Cur.SequenceType)+"', "
				+"'"+POut.PInt   (Cur.IntTooth)+"', "
				+"'"+POut.PInt   (Cur.ToothValue)+"', "
				+"'"+POut.PInt   (Cur.MBvalue)+"', "
				+"'"+POut.PInt   (Cur.Bvalue)+"', "
				+"'"+POut.PInt   (Cur.DBvalue)+"', "
				+"'"+POut.PInt   (Cur.MLvalue)+"', "
				+"'"+POut.PInt   (Cur.Lvalue)+"', "
				+"'"+POut.PInt   (Cur.DLvalue)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.PerioMeasureNum=InsertID;
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from periomeasure WHERE PerioMeasureNum = '"
				+Cur.PerioMeasureNum.ToString()+"'";
			NonQ();
		}

		///<summary>For the current exam, clears existing skipped teeth and resets them to the specified skipped teeth. The ArrayList valid values are 1-32 int.</summary>
		public static void SetSkipped(ArrayList skippedTeeth){
			//for(int i=0;i<skippedTeeth.Count;i++){
			//MessageBox.Show(skippedTeeth[i].ToString());
			//}
			//first, delete all skipped teeth for this exam
			cmd.CommandText = "DELETE from periomeasure WHERE "
				+"PerioExamNum = '"+PerioExams.Cur.PerioExamNum.ToString()+"' "
				+"&& SequenceType = '"+POut.PInt((int)PerioSequenceType.SkipTooth)+"'";
			NonQ();
			//then add the new ones in one at a time.
			for(int i=0;i<skippedTeeth.Count;i++){
				Cur=new PerioMeasure();
				Cur.PerioExamNum=PerioExams.Cur.PerioExamNum;
				Cur.SequenceType=PerioSequenceType.SkipTooth;
				Cur.IntTooth=(int)skippedTeeth[i];
				Cur.ToothValue=1;
				Cur.MBvalue=-1;
				Cur.Bvalue=-1;
				Cur.DBvalue=-1;
				Cur.MLvalue=-1;
				Cur.Lvalue=-1;
				Cur.DLvalue=-1;
				InsertCur();
			}
		}

		///<summary>Used in FormPerio.Add_Click. For the specified exam, gets a list of all skipped teeth. The ArrayList valid values are 1-32 int.</summary>
		public static ArrayList GetSkipped(int perioExamNum){
			cmd.CommandText = "SELECT IntTooth FROM periomeasure WHERE "
				+"SequenceType = '"+POut.PInt((int)PerioSequenceType.SkipTooth)+"' "
				+"&& PerioExamNum = '"+perioExamNum.ToString()+"' "
				+"&& ToothValue = '1'";
			FillTable();
			ArrayList retList=new ArrayList();
			for(int i=0;i<table.Rows.Count;i++){
				retList.Add(PIn.PInt(table.Rows[i][0].ToString()));
			}
			return retList;
		}

	
	}
	
	

}















