using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>These are copies of procedures that are attached to treatment plans.</summary>
	public class ProcTP{
		///<summary>Primary key.</summary>
		public int ProcTPNum;
		///<summary>FK to treatplan.TreatPlanNum.  The treatment plan to which this proc is attached.</summary>
		public int TreatPlanNum;
		///<summary>FK to patient.PatNum.</summary>
		public int PatNum;
		///<summary>FK to procedurelog.ProcNum.  It is very common for the referenced procedure to be missing.  This procNum is only here to compare and test the existence of the referenced procedure.  If present, it will check to see whether the procedure is still status TP.</summary>
		public int ProcNumOrig;
		///<summary>The order of this proc within its tp.  This is set when the tp is first created and can't be changed.  Drastically simplifies loading the tp.</summary>
		public int ItemOrder;
		///<summary>FK to definition.DefNum which contains the text of the priority.</summary>
		public int Priority;
		///<summary>A simple string displaying the tooth number.  If international tooth numbers are used, then this will be in international format already.</summary>
		public string ToothNumTP;
		///<summary>Tooth surfaces or area.</summary>
		public string Surf;
		///<summary>Not a foreign key.  Simply display text.  Can be changed by user at any time.</summary>
		public string ADACode;
		///<summary>Description is originally copied from procedurecode.Descript, but user can change it.</summary>
		public string Descript;
		///<summary>The fee charged to the patient. Never gets automatically updated.</summary>
		public double FeeAmt;
		///<summary>The amount primary insurance is expected to pay. Never gets automatically updated.</summary>
		public double PriInsAmt;
		///<summary>The amount secondary insurance is expected to pay. Never gets automatically updated.</summary>
		public double SecInsAmt;
		///<summary>The amount the patient is expected to pay. Never gets automatically updated.</summary>
		public double PatAmt;
		
		///<summary></summary>
		public ProcTP Copy(){
			ProcTP t=new ProcTP();
			t.ProcTPNum=ProcTPNum;
			t.TreatPlanNum=TreatPlanNum;
			t.PatNum=PatNum;
			t.ProcNumOrig=ProcNumOrig;
			t.ItemOrder=ItemOrder;
			t.Priority=Priority;
			t.ToothNumTP=ToothNumTP;
			t.Surf=Surf;
			t.ADACode=ADACode;
			t.Descript=Descript;
			t.FeeAmt=FeeAmt;
			t.PriInsAmt=PriInsAmt;
			t.SecInsAmt=SecInsAmt;
			t.PatAmt=PatAmt;
			return t;
		}

		///<summary></summary>
		private void Update(){
			string command= "UPDATE ProcTP SET "
				+"TreatPlanNum = '"+POut.PInt   (TreatPlanNum)+"'"
				+",PatNum = '"     +POut.PInt   (PatNum)+"'"
				+",ProcNumOrig = '"+POut.PInt   (ProcNumOrig)+"'"
				+",ItemOrder = '"  +POut.PInt   (ItemOrder)+"'"
				+",Priority = '"   +POut.PInt   (Priority)+"'"
				+",ToothNumTP = '" +POut.PString(ToothNumTP)+"'"
				+",Surf = '"       +POut.PString(Surf)+"'"
				+",ADACode = '"    +POut.PString(ADACode)+"'"
				+",Descript = '"   +POut.PString(Descript)+"'"
				+",FeeAmt = '"     +POut.PDouble(FeeAmt)+"'"
				+",PriInsAmt = '"  +POut.PDouble(PriInsAmt)+"'"
				+",SecInsAmt = '"  +POut.PDouble(SecInsAmt)+"'"
				+",PatAmt = '"     +POut.PDouble(PatAmt)+"'"
				+" WHERE ProcTPNum = '"+POut.PInt(ProcTPNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		private void Insert(){
			if(Prefs.RandomKeys){
				ProcTPNum=MiscData.GetKey("proctp","ProcTPNum");
			}
			string command= "INSERT INTO proctp (";
			if(Prefs.RandomKeys){
				command+="ProcTPNum,";
			}
			command+="TreatPlanNum,PatNum,ProcNumOrig,ItemOrder,Priority,ToothNumTP,Surf,ADACode,Descript,FeeAmt,"
				+"PriInsAmt,SecInsAmt,PatAmt) VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(ProcTPNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (TreatPlanNum)+"', "
				+"'"+POut.PInt   (PatNum)+"', "
				+"'"+POut.PInt   (ProcNumOrig)+"', "
				+"'"+POut.PInt   (ItemOrder)+"', "
				+"'"+POut.PInt   (Priority)+"', "
				+"'"+POut.PString(ToothNumTP)+"', "
				+"'"+POut.PString(Surf)+"', "
				+"'"+POut.PString(ADACode)+"', "
				+"'"+POut.PString(Descript)+"', "
				+"'"+POut.PDouble(FeeAmt)+"', "
				+"'"+POut.PDouble(PriInsAmt)+"', "
				+"'"+POut.PDouble(SecInsAmt)+"', "
				+"'"+POut.PDouble(PatAmt)+"')";
			DataConnection dcon=new DataConnection();
 			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				ProcTPNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void InsertOrUpdate(bool isNew){
			if(isNew){
				Insert();
			}
			else{
				Update();
			}
		}

		///<summary>There are no dependencies.</summary>
		public void Delete(){
			string command= "DELETE from proctp WHERE ProcTPNum = '"+POut.PInt(ProcTPNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}


	}

	/*=========================================================================================
		=================================== class ProcTPs ==========================================*/

	///<summary></summary>
	public class ProcTPs{
			
		///<summary>Gets all ProcTPs for a given Patient ordered by ItemOrder.</summary>
		public static ProcTP[] Refresh(int patNum){
			string command="SELECT * FROM proctp "
				+"WHERE PatNum="+POut.PInt(patNum)
				+" ORDER BY ItemOrder";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			ProcTP[] List=new ProcTP[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new ProcTP();
				List[i].ProcTPNum   = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].TreatPlanNum= PIn.PInt   (table.Rows[i][1].ToString());
				List[i].PatNum      = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].ProcNumOrig = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].ItemOrder   = PIn.PInt   (table.Rows[i][4].ToString());
				List[i].Priority    = PIn.PInt   (table.Rows[i][5].ToString());
				List[i].ToothNumTP  = PIn.PString(table.Rows[i][6].ToString());
				List[i].Surf        = PIn.PString(table.Rows[i][7].ToString());
				List[i].ADACode     = PIn.PString(table.Rows[i][8].ToString());
				List[i].Descript    = PIn.PString(table.Rows[i][9].ToString());
				List[i].FeeAmt      = PIn.PDouble(table.Rows[i][10].ToString());
				List[i].PriInsAmt   = PIn.PDouble(table.Rows[i][11].ToString());
				List[i].SecInsAmt   = PIn.PDouble(table.Rows[i][12].ToString());
				List[i].PatAmt      = PIn.PDouble(table.Rows[i][13].ToString());
			}
			return List;
		}
	
		///<summary>Gets a list for just one tp.  Used in TP module.  Supply a list of all ProcTPs for pt.</summary>
		public static ProcTP[] GetListForTP(int treatPlanNum, ProcTP[] listAll){
			ArrayList AL=new ArrayList();
			for(int i=0;i<listAll.Length;i++){
				if(listAll[i].TreatPlanNum!=treatPlanNum){
					continue;
				}
				AL.Add(listAll[i]);
			}
			ProcTP[] retVal=new ProcTP[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		///<summary>No dependencies to worry about.</summary>
		public static void DeleteForTP(int treatPlanNum){
			string command="DELETE FROM proctp "
				+"WHERE TreatPlanNum="+POut.PInt(treatPlanNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

	
	}

	

	


}




















