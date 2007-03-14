using System;
using System.Collections;
using System.Data;
//using System.Drawing;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the procedurecode table in the database.</summary>
	public struct ProcedureCode{
		///<summary>Primary key.  Does not have to be a valid ADACode. Can also include ADACodes with suffixes which get automatically trimmed when sending claims.</summary>
		public string ADACode;
		///<summary>The main description.</summary>
		public string Descript;
		///<summary>Abbreviated description.</summary>
		public string AbbrDesc;
		///<summary>X's and /'s describe Dr's time and assistant's time in 10 minute increments.</summary>
		public string ProcTime;
		///<summary>Foreign key to definition.DefNum.</summary>
		public int ProcCat;
		///<summary>See the TreatmentArea enumeration.</summary>
		public TreatmentArea TreatArea;
		///<summary>True for extractions so teeth will show as missing if complete or existing.</summary>
		public bool RemoveTooth;
		///<summary>Triggers recall in 6 months.</summary>
		public bool SetRecall;
		///<summary>If true, do not usually bill this procedure to insurance.</summary>
		public bool NoBillIns;
		///<summary>True if Crown,Bridge,Denture, or RPD. Forces user to enter Initial or Replacement and Date.</summary>
		public bool IsProsth;
		///<summary>The default procedure note to copy when marking complete.</summary>
		public string DefaultNote;
		///<summary>Identifies hygiene procedures so that the correct provider can be selected.</summary>
		public bool IsHygiene;
		///<summary>Foreign key to GraphicType</summary>
		public int GTypeNum;
		///<summary>For Medicaid.  There may be more later.</summary>
		public string AlternateCode1;
	}

	/*=========================================================================================
	=================================== class ProcedureCodes ===========================================*/

	///<summary></summary>
	public class ProcedureCodes:DataClass{
		///<summary></summary>
		public static DataTable tableStat;
		///<summary></summary>
		public static ProcedureCode[] ProcList;//grouped by category
		///<summary></summary>
		public static ArrayList RecallAL;
		///<summary></summary>
		public static ProcedureCode Cur;
		///<summary></summary>
		public static Hashtable HList;//key:AdaCode, value:ProcedureCode 
		///<summary></summary>
		public static ProcedureCode[] List;

		///<summary></summary>
		public static void Refresh(){
			HList=new Hashtable();
			ProcedureCode tempCode = new ProcedureCode();
			cmd.CommandText = 
				"SELECT * from procedurecode "
				+"ORDER BY ProcCat, ADACode";
			FillTable();
			tableStat=table.Copy();
			RecallAL=new ArrayList();
			List=new ProcedureCode[tableStat.Rows.Count];
			for (int i=0;i<tableStat.Rows.Count;i++){
				tempCode.ADACode       =PIn.PString(tableStat.Rows[i][0].ToString());
				tempCode.Descript      =PIn.PString(tableStat.Rows[i][1].ToString());
				tempCode.AbbrDesc      =PIn.PString(tableStat.Rows[i][2].ToString());
				tempCode.ProcTime      =PIn.PString(tableStat.Rows[i][3].ToString());
				tempCode.ProcCat       =PIn.PInt   (tableStat.Rows[i][4].ToString());
				tempCode.TreatArea     =(TreatmentArea)PIn.PInt   (tableStat.Rows[i][5].ToString());
				tempCode.RemoveTooth   =PIn.PBool  (tableStat.Rows[i][6].ToString());
				tempCode.SetRecall     =PIn.PBool  (tableStat.Rows[i][7].ToString());
				tempCode.NoBillIns     =PIn.PBool  (tableStat.Rows[i][8].ToString());
				tempCode.IsProsth      =PIn.PBool  (tableStat.Rows[i][9].ToString());
				tempCode.DefaultNote   =PIn.PString(tableStat.Rows[i][10].ToString());
				tempCode.IsHygiene     =PIn.PBool  (tableStat.Rows[i][11].ToString());
				tempCode.GTypeNum      =PIn.PInt   (tableStat.Rows[i][12].ToString());
				tempCode.AlternateCode1=PIn.PString(tableStat.Rows[i][13].ToString());
				HList.Add(tempCode.ADACode,tempCode);
				List[i]=tempCode;
				if(tempCode.SetRecall){
					RecallAL.Add(tempCode);
				}
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			//must have already checked ADACode for nonduplicate.
			cmd.CommandText = "INSERT INTO procedurecode (adacode,descript,abbrdesc,"
				+"proctime,proccat,treatarea,removetooth,setrecall,"
				+"nobillins,isprosth,defaultnote,ishygiene,gtypenum,alternatecode1) VALUES("
				+"'"+POut.PString(Cur.ADACode)+"', "
				+"'"+POut.PString(Cur.Descript)+"', "
				+"'"+POut.PString(Cur.AbbrDesc)+"', "
				+"'"+POut.PString(Cur.ProcTime)+"', "
				+"'"+POut.PInt   (Cur.ProcCat)+"', "
				+"'"+POut.PInt   ((int)Cur.TreatArea)+"', "
				+"'"+POut.PBool  (Cur.RemoveTooth)+"', "
				+"'"+POut.PBool  (Cur.SetRecall)+"', "
				+"'"+POut.PBool  (Cur.NoBillIns)+"', "
				+"'"+POut.PBool  (Cur.IsProsth)+"', "
				+"'"+POut.PString(Cur.DefaultNote)+"', "
				+"'"+POut.PBool  (Cur.IsHygiene)+"', "
				+"'"+POut.PInt   (Cur.GTypeNum)+"', "
				+"'"+POut.PString(Cur.AlternateCode1)+"')";
			NonQ(false);
			Refresh();
			//Cur already set
			//MessageBox.Show(Cur.PayNum.ToString());
		}

		///<summary></summary>
		public static void UpdateCur(){
			//MessageBox.Show("Updating");
			cmd.CommandText = "UPDATE procedurecode SET " 
				+ "descript = '"       +POut.PString(Cur.Descript)+"'"
				+ ",abbrdesc = '"      +POut.PString(Cur.AbbrDesc)+"'"
				+ ",proctime = '"      +POut.PString(Cur.ProcTime)+"'"
				+ ",proccat = '"       +POut.PInt   (Cur.ProcCat)+"'"
				+ ",treatarea = '"     +POut.PInt   ((int)Cur.TreatArea)+"'"
				+ ",removetooth = '"   +POut.PBool  (Cur.RemoveTooth)+"'"
				+ ",setrecall = '"     +POut.PBool  (Cur.SetRecall)+"'"
				+ ",nobillins = '"     +POut.PBool  (Cur.NoBillIns)+"'"
				+ ",isprosth = '"      +POut.PBool  (Cur.IsProsth)+"'"
				+ ",defaultnote = '"   +POut.PString(Cur.DefaultNote)+"'"
				+ ",ishygiene = '"     +POut.PBool  (Cur.IsHygiene)+"'"
				+ ",gtypenum = '"      +POut.PInt   (Cur.GTypeNum)+"'"
				+ ",alternatecode1 = '"+POut.PString(Cur.AlternateCode1)+"'"
				+" WHERE adacode = '"+POut.PString(Cur.ADACode)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary>Returns the ProcedureCode for the supplied adaCode. If adaCode not present, then it adds it to the db.  Be very careful not to use this in the middle of a loop over a shared table, because it jumps out of the loop to refresh local data.</summary>
		public static ProcedureCode GetProcCode(string myADA){
			if(myADA==null){
				MessageBox.Show(Lan.g("ProcCodes","Error. Invalid procedure code."));
				return new ProcedureCode();
			}
			if(HList.Contains(myADA)){
				return (ProcedureCode)HList[myADA];
			}
			else{
				//MessageBox.Show(Lan.g("ProcCodes","code not found: ")+myADA);
				//return new ProcedureCode();
				/*Cur=new ProcedureCode();
				Cur.ADACode=myADA;
				Cur.Descript=myADA;
				Cur.AbbrDesc=myADA;
				Cur.ProcCat=Defs.Short[(int)DefCat.ProcCodeCats][0].DefNum;
				Cur.TreatArea=TreatmentArea.Mouth;
				InsertCur();
				DataValid.SetInvalid(InvalidTypes.ProcCodes);
				return Cur;*/
				return new ProcedureCode();
			}
		}

		///<summary></summary>
		public static void GetProcList(){
			ProcList = new ProcedureCode[tableStat.Rows.Count];
			int i=0;
			for(int j=0;j<Defs.Short[(int)DefCat.ProcCodeCats].Length;j++){
				for(int k=0;k<tableStat.Rows.Count;k++){
					if(Defs.Short[(int)DefCat.ProcCodeCats][j].DefNum==PIn.PInt(tableStat.Rows[k][4].ToString())){
						ProcList[i].ADACode = PIn.PString(tableStat.Rows[k][0].ToString());
						ProcList[i].Descript= PIn.PString(tableStat.Rows[k][1].ToString());
						ProcList[i].AbbrDesc= PIn.PString(tableStat.Rows[k][2].ToString());
						ProcList[i].ProcCat = PIn.PInt   (tableStat.Rows[k][4].ToString());
						i++;
					}
				}
			}
			for(int k=0;k<tableStat.Rows.Count;k++){
				if(PIn.PInt(tableStat.Rows[k][4].ToString())==255){
					ProcList[i].ADACode = PIn.PString(tableStat.Rows[k][0].ToString());
					ProcList[i].AbbrDesc= PIn.PString(tableStat.Rows[k][2].ToString());
					ProcList[i].ProcCat = 255;
					i++;
				}
			}
		}

	}

	
	
	


}










