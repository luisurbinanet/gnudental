using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental {

	/// <summary>Corresponds to the RxAlert table in the database. Many-to-many relationship connecting Rx with DiseaseDef.</summary>
	public class RxAlert{
		///<summary>Primary key.</summary>
		public int RxAlertNum;
		///<summary>fk to rxdef.RxDefNum.</summary>
		public int RxDefNum;
		///<summary>fk to diseasedef.DiseaseDefNum</summary>
		public int DiseaseDefNum;

		///<summary></summary>
		public RxAlert Copy() {
			RxAlert r=new RxAlert();
			r.RxAlertNum=RxAlertNum;
			r.RxDefNum=RxDefNum;
			r.DiseaseDefNum=DiseaseDefNum;
			return r;
		}

		///<summary></summary>
		public void Update() {
			string command="UPDATE rxalert SET " 
				+"RxDefNum = '"      +POut.PInt   (RxDefNum)+"'"
				+",DiseaseDefNum = '"+POut.PInt   (DiseaseDefNum)+"'"
				+" WHERE RxAlertNum  ='"+POut.PInt   (RxAlertNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Insert() {
			string command="INSERT INTO rxalert (RxDefNum,DiseaseDefNum) VALUES("
				+"'"+POut.PInt   (RxDefNum)+"', "
				+"'"+POut.PInt   (DiseaseDefNum)+"')";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command,true);
			RxAlertNum=dcon.InsertID;
		}

		///<summary></summary>
		public void Delete() {
			string command="DELETE FROM rxalert WHERE RxAlertNum ="+POut.PInt(RxAlertNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

	}

	/*================================================================================================================
	==================================================== class RxAlerts =============================================*/

	///<summary></summary>
	public class RxAlerts {
		
		///<summary>Gets a list of all RxAlerts for one RxDef.</summary>
		public static RxAlert[] Refresh(int rxDefNum) {
			string command="SELECT * FROM rxalert WHERE RxDefNum="+POut.PInt(rxDefNum);
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			RxAlert[] List=new RxAlert[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new RxAlert();
				List[i].RxAlertNum   = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].RxDefNum     = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].DiseaseDefNum= PIn.PInt   (table.Rows[i][2].ToString());
			}
			return List;
		}

		
		
		
	}

		



		
	

	

	


}










