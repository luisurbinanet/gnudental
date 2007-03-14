using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the rxpat table in the database.  One Rx for one patient. Copied from rxdef rather than linked to it.</summary>
	public class RxPat{
		///<summary>Primary key.</summary>
		public int RxNum;
		///<summary>Foreign key to patient.PatNum.</summary>
		public int PatNum;
		///<summary>Date of Rx.</summary>
		public DateTime RxDate;
		///<summary>Drug name.</summary>
		public string Drug;
		///<summary>Directions.</summary>
		public string Sig;
		///<summary>Amount to dispense.</summary>
		public string Disp;
		///<summary>Number of refills.</summary>
		public string Refills;
		///<summary>Foreign key to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>Notes specific to this Rx.</summary>
		public string Notes;

		///<summary></summary>
		public RxPat Copy() {
			RxPat r=new RxPat();
			r.RxNum=RxNum;
			r.PatNum=PatNum;
			r.RxDate=RxDate;
			r.Drug=Drug;
			r.Sig=Sig;
			r.Disp=Disp;
			r.Refills=Refills;
			r.ProvNum=ProvNum;
			r.Notes=Notes;
			return r;
		}

		///<summary></summary>
		public void Update() {
			string command= "UPDATE rxpat SET " 
				+ "PatNum = '"      +POut.PInt   (PatNum)+"'"
				+ ",RxDate = '"     +POut.PDate  (RxDate)+"'"
				+ ",Drug = '"       +POut.PString(Drug)+"'"
				+ ",Sig = '"        +POut.PString(Sig)+"'"
				+ ",Disp = '"       +POut.PString(Disp)+"'"
				+ ",Refills = '"    +POut.PString(Refills)+"'"
				+ ",ProvNum = '"    +POut.PInt   (ProvNum)+"'"
				+ ",Notes = '"      +POut.PString(Notes)+"'"
				+" WHERE RxNum = '" +POut.PInt   (RxNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Insert() {
			if(Prefs.RandomKeys) {
				RxNum=MiscData.GetKey("rxpat","RxNum");
			}
			string command="INSERT INTO rxpat (";
			if(Prefs.RandomKeys) {
				command+="RxNum,";
			}
			command+="PatNum,RxDate,Drug,Sig,Disp,Refills,ProvNum,Notes) VALUES(";
			if(Prefs.RandomKeys) {
				command+="'"+POut.PInt(RxNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (PatNum)+"', "
				+"'"+POut.PDate  (RxDate)+"', "
				+"'"+POut.PString(Drug)+"', "
				+"'"+POut.PString(Sig)+"', "
				+"'"+POut.PString(Disp)+"', "
				+"'"+POut.PString(Refills)+"', "
				+"'"+POut.PInt   (ProvNum)+"', "
				+"'"+POut.PString(Notes)+"')";
			DataConnection dcon=new DataConnection();
			if(Prefs.RandomKeys) {
				dcon.NonQ(command);
			}
			else{
				dcon.NonQ(command,true);
				RxNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void Delete() {
			string command= "DELETE from rxpat WHERE RxNum = '"+POut.PInt(RxNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}



	}

	/*=========================================================================================
	=================================== class RxPats ==========================================*/
///<summary></summary>
	public class RxPats{
		//<summary></summary>
		//public static RxPat[] List;
		//<summary></summary>
		//public static RxPat Cur;

		///<summary></summary>
		public static RxPat[] Refresh(int patNum){
			string command="SELECT * FROM rxpat"
				+" WHERE PatNum = '"+POut.PInt(patNum)+"'"
				+" ORDER BY RxDate";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			RxPat[] List=new RxPat[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new RxPat();
				List[i].RxNum      = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum     = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].RxDate     = PIn.PDate  (table.Rows[i][2].ToString());
				List[i].Drug       = PIn.PString(table.Rows[i][3].ToString());
				List[i].Sig        = PIn.PString(table.Rows[i][4].ToString());
				List[i].Disp       = PIn.PString(table.Rows[i][5].ToString());
				List[i].Refills    = PIn.PString(table.Rows[i][6].ToString());
				List[i].ProvNum    = PIn.PInt   (table.Rows[i][7].ToString());
				List[i].Notes      = PIn.PString(table.Rows[i][8].ToString());
			}
			return List;
		}

		
	}

	


}













