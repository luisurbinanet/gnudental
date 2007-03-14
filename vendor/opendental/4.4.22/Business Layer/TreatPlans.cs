using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>A treatment plan saved by a user.  Does not include the default tp, which is just a list of procedurelog entries with a status of tp.  A treatplan has many proctp's attached to it.</summary>
	public class TreatPlan{
		///<summary>Primary key.</summary>
		public int TreatPlanNum;
		///<summary>FK to patient.PatNum.</summary>
		public int PatNum;
		///<summary>The date of the treatment plan</summary>
		public DateTime DateTP;
		///<summary>The heading that shows at the top of the treatment plan.  Usually 'Proposed Treatment Plan'</summary>
		public string Heading;
		///<summary>A note specific to this treatment plan that shows at the bottom.</summary>
		public string Note;
		
		///<summary></summary>
		public TreatPlan Copy(){
			TreatPlan t=new TreatPlan();
			t.TreatPlanNum=TreatPlanNum;
			t.PatNum=PatNum;
			t.DateTP=DateTP;
			t.Heading=Heading;
			t.Note=Note;
			return t;
		}

		///<summary></summary>
		private void Update(){
			string command= "UPDATE treatplan SET "
				+"PatNum = '"   +POut.PInt   (PatNum)+"'"
				+",DateTP = '"  +POut.PDate  (DateTP)+"'"
				+",Heading = '" +POut.PString(Heading)+"'"
				+",Note = '"    +POut.PString(Note)+"'"
				+" WHERE TreatPlanNum = '"+POut.PInt(TreatPlanNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		private void Insert(){
			if(Prefs.RandomKeys){
				TreatPlanNum=MiscData.GetKey("treatplan","TreatPlanNum");
			}
			string command= "INSERT INTO treatplan (";
			if(Prefs.RandomKeys){
				command+="TreatPlanNum,";
			}
			command+="PatNum,DateTP,Heading,Note) VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(TreatPlanNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (PatNum)+"', "
				+"'"+POut.PDate  (DateTP)+"', "
				+"'"+POut.PString(Heading)+"', "
				+"'"+POut.PString(Note)+"')";
			DataConnection dcon=new DataConnection();
 			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				TreatPlanNum=dcon.InsertID;
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

		///<summary>Dependencies checked first and throws an exception if any found. So surround by try catch</summary>
		public void Delete(){
			//check proctp for dependencies
			string command="SELECT * FROM proctp WHERE TreatPlanNum ="+POut.PInt(TreatPlanNum);
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count>0){
				//this should never happen
				throw new InvalidProgramException(Lan.g("TreatPlans","Cannot delete treatment plan because it has ProcTP's attached"));
			}
			command= "DELETE from treatplan WHERE TreatPlanNum = '"+POut.PInt(TreatPlanNum)+"'";
 			dcon.NonQ(command);
		}


	}

	/*=========================================================================================
		=================================== class TreatPlans ==========================================*/

	///<summary></summary>
	public class TreatPlans{
			
		///<summary>Gets all TreatPlans for a given Patient, ordered by date.</summary>
		public static TreatPlan[] Refresh(int patNum){
			string command="SELECT * FROM treatplan "
				+"WHERE PatNum="+POut.PInt(patNum)
				+" ORDER BY DateTP";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			TreatPlan[] List=new TreatPlan[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new TreatPlan();
				List[i].TreatPlanNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum      = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].DateTP      = PIn.PDate  (table.Rows[i][2].ToString());
				List[i].Heading     = PIn.PString(table.Rows[i][3].ToString());
				List[i].Note        = PIn.PString(table.Rows[i][4].ToString());
			}
			return List;
		}

	
	}

	

	


}




















