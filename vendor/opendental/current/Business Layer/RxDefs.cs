using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Rx definitions.  Can safely delete or alter, because they get copied to the rxPat table, not referenced.</summary>
	public class RxDef{
		///<summary>Primary key.</summary>
		public int RxDefNum;
		///<summary>The name of the drug.</summary>
		public string Drug;
		///<summary>Directions.</summary>
		public string Sig;
		///<summary>Amount to dispense.</summary>
		public string Disp;
		///<summary>Number of refills.</summary>
		public string Refills;
		///<summary>Notes about this drug. Will not be copied to the rxpat.</summary>
		public string Notes;

		///<summary></summary>
		public RxDef Copy() {
			RxDef r=new RxDef();
			r.RxDefNum=RxDefNum;
			r.Drug=Drug;
			r.Sig=Sig;
			r.Disp=Disp;
			r.Refills=Refills;
			r.Notes=Notes;
			return r;
		}

		///<summary></summary>
		public void Update() {
			string command= "UPDATE rxdef SET " 
				+"Drug = '"    +POut.PString(Drug)+"'"
				+",Sig = '"    +POut.PString(Sig)+"'"
				+",Disp = '"   +POut.PString(Disp)+"'"
				+",Refills = '"+POut.PString(Refills)+"'"
				+",Notes = '"  +POut.PString(Notes)+"'"
				+" WHERE RxDefNum = '" +POut.PInt(RxDefNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Insert() {
			string command= "INSERT INTO rxdef (Drug,Sig,Disp,Refills,Notes) VALUES("
				+"'"+POut.PString(Drug)+"', "
				+"'"+POut.PString(Sig)+"', "
				+"'"+POut.PString(Disp)+"', "
				+"'"+POut.PString(Refills)+"', "
				+"'"+POut.PString(Notes)+"')";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command,true);
			RxDefNum=dcon.InsertID;
		}

		///<summary>Also deletes all RxAlerts that were attached.</summary>
		public void Delete() {
			string command="DELETE FROM rxalert WHERE RxDefNum="+POut.PInt(RxDefNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
			command= "DELETE FROM rxdef "
				+"WHERE rxdefnum = "+POut.PInt(RxDefNum);
			dcon.NonQ(command);
		}



	}

	/*=========================================================================================
		=================================== class RxDefs ==========================================*/
///<summary></summary>
	public class RxDefs{
		//<summary></summary>
		//public static RxDef[] List;
	
		///<summary></summary>
		public static RxDef[] Refresh(){
			string command="SELECT * FROM rxdef ORDER BY Drug";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			RxDef[] List=new RxDef[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new RxDef();
				List[i].RxDefNum   = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Drug       = PIn.PString(table.Rows[i][1].ToString());
				List[i].Sig        = PIn.PString(table.Rows[i][2].ToString());
				List[i].Disp       = PIn.PString(table.Rows[i][3].ToString());
				List[i].Refills    = PIn.PString(table.Rows[i][4].ToString());
				List[i].Notes      = PIn.PString(table.Rows[i][5].ToString());
			}
			return List;
		}
	
	
	}

	

	


}













