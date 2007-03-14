using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the procedurecode table in the database.</summary>
	public class ProcedureCode{
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
		///<summary>No longer used. Extraction paint type is used instead to show missing teeth.</summary>
		public bool RemoveToothOld;
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
		///<summary>No longer used.  Foreign key to GraphicType</summary>
		public int GTypeNum;
		///<summary>For Medicaid.  There may be more later.</summary>
		public string AlternateCode1;
		///<summary>Foreign key to procedurecode.ADACode.  Anytime a procedure it added, this medical code will also be added to that procedure.  The user can change it in procedurelog.</summary>
		public string MedicalCode;
		///<summary>Not used yet. No user interface built yet.  SalesTaxPercentage has been added to the preference table to store the amount of sales tax to apply as an adjustment attached to a procedurelog entry.</summary>
		public bool IsTaxed;
		///<summary></summary>
		public ToothPaintingType PaintType;
		///<summary>If set to anything but 0, then this will override the graphic color for all procedures of this code, regardless of the status.</summary>
		public Color GraphicColor;
		///<summary>When creating treatment plans, this description will be used instead of the technical description.</summary>
		public string LaymanTerm;

		///<summary>Returns a copy of this Procedurecode.</summary>
		public ProcedureCode Copy(){
			ProcedureCode p=new ProcedureCode();
			p.ADACode=ADACode;
			p.Descript=Descript;
			p.AbbrDesc=AbbrDesc;
			p.ProcTime=ProcTime;
			p.ProcCat=ProcCat;
			p.TreatArea=TreatArea;
			//p.RemoveTooth=RemoveTooth;
			p.SetRecall=SetRecall;
			p.NoBillIns=NoBillIns;
			p.IsProsth=IsProsth;
			p.DefaultNote=DefaultNote;
			p.IsHygiene=IsHygiene;
			p.GTypeNum=GTypeNum;
			p.AlternateCode1=AlternateCode1;
			p.MedicalCode=MedicalCode;
			p.IsTaxed=IsTaxed;
			p.PaintType=PaintType;
			p.GraphicColor=GraphicColor;
			p.LaymanTerm=LaymanTerm;
			return p;
		}

		///<summary></summary>
		public void Insert(){
			//must have already checked ADACode for nonduplicate.
			string command="INSERT INTO procedurecode (adacode,descript,abbrdesc,"
				+"proctime,proccat,treatarea,setrecall,"
				+"nobillins,isprosth,defaultnote,ishygiene,gtypenum,alternatecode1,MedicalCode,IsTaxed,"
				+"PaintType,GraphicColor,LaymanTerm) VALUES("
				+"'"+POut.PString(ADACode)+"', "
				+"'"+POut.PString(Descript)+"', "
				+"'"+POut.PString(AbbrDesc)+"', "
				+"'"+POut.PString(ProcTime)+"', "
				+"'"+POut.PInt   (ProcCat)+"', "
				+"'"+POut.PInt   ((int)TreatArea)+"', "
				//+"'"+POut.PBool  (RemoveTooth)+"', "
				+"'"+POut.PBool  (SetRecall)+"', "
				+"'"+POut.PBool  (NoBillIns)+"', "
				+"'"+POut.PBool  (IsProsth)+"', "
				+"'"+POut.PString(DefaultNote)+"', "
				+"'"+POut.PBool  (IsHygiene)+"', "
				+"'"+POut.PInt   (GTypeNum)+"', "
				+"'"+POut.PString(AlternateCode1)+"', "
				+"'"+POut.PString(MedicalCode)+"', "
				+"'"+POut.PBool  (IsTaxed)+"', "
				+"'"+POut.PInt   ((int)PaintType)+"', "
				+"'"+POut.PInt   (GraphicColor.ToArgb())+"', "
				+"'"+POut.PString(LaymanTerm)+"')";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
			ProcedureCodes.Refresh();
			//Cur already set
			//MessageBox.Show(Cur.PayNum.ToString());
		}

		///<summary></summary>
		public void Update(){
			//MessageBox.Show("Updating");
			string command="UPDATE procedurecode SET " 
				+ "descript = '"       +POut.PString(Descript)+"'"
				+ ",abbrdesc = '"      +POut.PString(AbbrDesc)+"'"
				+ ",proctime = '"      +POut.PString(ProcTime)+"'"
				+ ",proccat = '"       +POut.PInt   (ProcCat)+"'"
				+ ",treatarea = '"     +POut.PInt   ((int)TreatArea)+"'"
				//+ ",removetooth = '"   +POut.PBool  (RemoveTooth)+"'"
				+ ",setrecall = '"     +POut.PBool  (SetRecall)+"'"
				+ ",nobillins = '"     +POut.PBool  (NoBillIns)+"'"
				+ ",isprosth = '"      +POut.PBool  (IsProsth)+"'"
				+ ",defaultnote = '"   +POut.PString(DefaultNote)+"'"
				+ ",ishygiene = '"     +POut.PBool  (IsHygiene)+"'"
				+ ",gtypenum = '"      +POut.PInt   (GTypeNum)+"'"
				+ ",alternatecode1 = '"+POut.PString(AlternateCode1)+"'"
				+ ",MedicalCode = '"   +POut.PString(MedicalCode)+"'"
				+ ",IsTaxed = '"       +POut.PBool  (IsTaxed)+"'"
				+ ",PaintType = '"     +POut.PInt   ((int)PaintType)+"'"
				+ ",GraphicColor = '"  +POut.PInt   (GraphicColor.ToArgb())+"'"
				+ ",LaymanTerm = '"    +POut.PString(LaymanTerm)+"'"
				+" WHERE adacode = '"+POut.PString(ADACode)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}



	}

	/*=========================================================================================
	=================================== class ProcedureCodes ===========================================*/

	///<summary></summary>
	public class ProcedureCodes{
		///<summary></summary>
		private static DataTable tableStat;
		///<summary></summary>
		public static ArrayList RecallAL;
		///<summary>key:AdaCode, value:ProcedureCode</summary>
		public static Hashtable HList;//
		///<summary></summary>
		public static ProcedureCode[] List;

		///<summary></summary>
		public static void Refresh(){
			HList=new Hashtable();
			ProcedureCode tempCode=new ProcedureCode();
			string command="SELECT * from procedurecode ORDER BY ProcCat,ADACode";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			tableStat=table.Copy();
			RecallAL=new ArrayList();
			List=new ProcedureCode[tableStat.Rows.Count];
			for(int i=0;i<tableStat.Rows.Count;i++){
				tempCode=new ProcedureCode();
				tempCode.ADACode       =PIn.PString(tableStat.Rows[i][0].ToString());
				tempCode.Descript      =PIn.PString(tableStat.Rows[i][1].ToString());
				tempCode.AbbrDesc      =PIn.PString(tableStat.Rows[i][2].ToString());
				tempCode.ProcTime      =PIn.PString(tableStat.Rows[i][3].ToString());
				tempCode.ProcCat       =PIn.PInt   (tableStat.Rows[i][4].ToString());
				tempCode.TreatArea     =(TreatmentArea)PIn.PInt(tableStat.Rows[i][5].ToString());
				//tempCode.RemoveTooth   =PIn.PBool  (tableStat.Rows[i][6].ToString());
				tempCode.SetRecall     =PIn.PBool  (tableStat.Rows[i][7].ToString());
				tempCode.NoBillIns     =PIn.PBool  (tableStat.Rows[i][8].ToString());
				tempCode.IsProsth      =PIn.PBool  (tableStat.Rows[i][9].ToString());
				tempCode.DefaultNote   =PIn.PString(tableStat.Rows[i][10].ToString());
				tempCode.IsHygiene     =PIn.PBool  (tableStat.Rows[i][11].ToString());
				tempCode.GTypeNum      =PIn.PInt   (tableStat.Rows[i][12].ToString());
				tempCode.AlternateCode1=PIn.PString(tableStat.Rows[i][13].ToString());
				tempCode.MedicalCode   =PIn.PString(tableStat.Rows[i][14].ToString());
				tempCode.IsTaxed       =PIn.PBool  (tableStat.Rows[i][15].ToString());
				tempCode.PaintType     =(ToothPaintingType)PIn.PInt(tableStat.Rows[i][16].ToString());
				tempCode.GraphicColor  =Color.FromArgb(PIn.PInt(tableStat.Rows[i][17].ToString()));
				tempCode.LaymanTerm    =PIn.PString(tableStat.Rows[i][18].ToString());
				HList.Add(tempCode.ADACode,tempCode.Copy());
				List[i]=tempCode.Copy();
				if(tempCode.SetRecall){
					RecallAL.Add(tempCode);
				}
			}
		}

		///<summary>Returns the ProcedureCode for the supplied adaCode.</summary>
		public static ProcedureCode GetProcCode(string myADA){
			if(myADA==null){
				MessageBox.Show(Lan.g("ProcCodes","Error. Invalid procedure code."));
				return new ProcedureCode();
			}
			if(HList.Contains(myADA)){
				return (ProcedureCode)HList[myADA];
			}
			else{
				return new ProcedureCode();
			}
		}

		///<summary></summary>
		public static bool IsValidCode(string adaCode){
			if(adaCode==null || adaCode=="") {
				return false;
			}
			if(HList.Contains(adaCode)) {
				return true;
			}
			else {
				return false;
			}
		}

		///<summary>Grouped by Category.  Used in Procedures window and in FormRpProcCodes.</summary>
		public static ProcedureCode[] GetProcList(){
			//ProcedureCode[] ProcList=new ProcedureCode[tableStat.Rows.Count];
			//int i=0;
			ProcedureCode procCode;
			ArrayList AL=new ArrayList();
			for(int j=0;j<Defs.Short[(int)DefCat.ProcCodeCats].Length;j++){
				for(int k=0;k<tableStat.Rows.Count;k++){
					if(Defs.Short[(int)DefCat.ProcCodeCats][j].DefNum==PIn.PInt(tableStat.Rows[k][4].ToString())){
						procCode=new ProcedureCode();
						procCode.ADACode = PIn.PString(tableStat.Rows[k][0].ToString());
						procCode.Descript= PIn.PString(tableStat.Rows[k][1].ToString());
						procCode.AbbrDesc= PIn.PString(tableStat.Rows[k][2].ToString());
						procCode.ProcCat = PIn.PInt   (tableStat.Rows[k][4].ToString());
						AL.Add(procCode);
						//i++;
					}
				}
			}
			/*for(int k=0;k<tableStat.Rows.Count;k++){
				if(PIn.PInt(tableStat.Rows[k][4].ToString())==255){
					ProcList[i]=new ProcedureCode();
					ProcList[i].ADACode = PIn.PString(tableStat.Rows[k][0].ToString());
					ProcList[i].Descript= PIn.PString(tableStat.Rows[k][1].ToString());
					ProcList[i].AbbrDesc= PIn.PString(tableStat.Rows[k][2].ToString());
					ProcList[i].ProcCat = 255;
					i++;
				}
			}*/
			ProcedureCode[] retVal=new ProcedureCode[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
			//return ProcList;
		}

		///<summary>Gets a list of procedure codes directly fromt the database.  If categories.length==0, then we will get for all categories.  Categories are defnums.  FeeScheds are, for now, defnums.</summary>
		public static DataTable GetProcTable(string abbr,string desc,string code,int[] categories,int feeSched){
			string whereCat;
			if(categories.Length==0){
				whereCat="1";
			}
			else{
				whereCat="(";
				for(int i=0;i<categories.Length;i++){
					if(i>0){
						whereCat+=" OR ";
					}
					whereCat+="ProcCat="+POut.PInt(categories[i]);
				}
				whereCat+=")";
			}
			string command="SELECT ProcCat,Descript,AbbrDesc,procedurecode.ADACode,"
				+"IFNULL(fee.Amount,'-1') "
				//+"IFNULL((SELECT fee.Amount FROM fee WHERE fee.ADACode=procedurecode.ADACode "
				//+"AND fee.FeeSched="+POut.PInt(feeSched)+"),'-1') "
				+"FROM procedurecode "
				+"LEFT JOIN fee ON fee.ADACode=procedurecode.ADACode "
				+"AND fee.FeeSched="+POut.PInt(feeSched)
				+" WHERE "+whereCat
				+" AND Descript LIKE '%"+POut.PString(desc)+"%' "
				+"AND AbbrDesc LIKE '%"+POut.PString(abbr)+"%' "
				+"AND procedurecode.ADACode LIKE '%"+POut.PString(code)+"%' "
				+"ORDER BY ProcCat,procedurecode.ADACode";
			//MsgBoxCopyPaste msg=new MsgBoxCopyPaste(command);
			//msg.ShowDialog();
			DataConnection dcon=new DataConnection();
			return dcon.GetTable(command);
		}

		///<summary>Returns the LaymanTerm for the supplied adaCode, or the description if none present.</summary>
		public static string GetLaymanTerm(string myADA) {
			if(myADA==null) {
				MessageBox.Show(Lan.g("ProcCodes","Error. Invalid procedure code."));
				return "";
			}
			if(HList.Contains(myADA)) {
				if(((ProcedureCode)HList[myADA]).LaymanTerm !=""){
					return ((ProcedureCode)HList[myADA]).LaymanTerm;
				}
				return ((ProcedureCode)HList[myADA]).Descript;
			}
			else {
				return "";
			}
		}

	}

	
	
	


}










