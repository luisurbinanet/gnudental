using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the PayPeriod table in the database.  Used to view employee timecards.</summary>
	public class PayPeriod{
		///<summary>Primary key.</summary>
		public int PayPeriodNum;
		///<summary>The first day of the payperiod</summary>
		public DateTime DateStart;
		///<summary>The last day of the payperiod.</summary>
		public DateTime DateStop;
		///<summary>The date that paychecks will be dated.  A few days after the dateStop.  Optional.</summary>
		public DateTime DatePaycheck;

		///<summary></summary>
		public PayPeriod Copy() {
			PayPeriod p=new PayPeriod();
			p.PayPeriodNum=PayPeriodNum;
			p.DateStart=DateStart;
			p.DateStop=DateStop;
			p.DatePaycheck=DatePaycheck;
			return p;
		}

		///<summary></summary>
		public void Insert() {
			if(Prefs.RandomKeys) {
				PayPeriodNum=MiscData.GetKey("payperiod","PayPeriodNum");
			}
			string command="INSERT INTO payperiod (";
			if(Prefs.RandomKeys) {
				command+="PayPeriodNum,";
			}
			command+="DateStart,DateStop,DatePaycheck) VALUES(";
			if(Prefs.RandomKeys) {
				command+="'"+POut.PInt(PayPeriodNum)+"', ";
			}
			command+=
				 "'"+POut.PDate  (DateStart)+"', "
				+"'"+POut.PDate  (DateStop)+"', "
				+"'"+POut.PDate  (DatePaycheck)+"')";
			DataConnection dcon=new DataConnection();
			if(Prefs.RandomKeys) {
				dcon.NonQ(command);
			}
			else {
				dcon.NonQ(command,true);
				PayPeriodNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void Update() {
			string command= "UPDATE payperiod SET "
				+"DateStart = '"    +POut.PDate  (DateStart)+"' "
				+",DateStop = '"    +POut.PDate  (DateStop)+"' "
				+",DatePaycheck = '"+POut.PDate  (DatePaycheck)+"' "
				+"WHERE PayPeriodNum = '"+POut.PInt(PayPeriodNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Delete() {
			string command= "DELETE FROM payperiod WHERE PayPeriodNum = "+POut.PInt(PayPeriodNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

	}

	/*=========================================================================================
		=================================== class PayPeriods ==========================================*/

	///<summary></summary>
	public class PayPeriods{
		///<summary>A list of all payperiods.</summary>
		public static PayPeriod[] List;

		///<summary>Fills List with all payperiods, ordered by startdate.</summary>
		public static void Refresh(){
			string command=
				"SELECT * from payperiod ORDER BY DateStart";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			List=new PayPeriod[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new PayPeriod();
				List[i].PayPeriodNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].DateStart    = PIn.PDate  (table.Rows[i][1].ToString());
				List[i].DateStop     = PIn.PDate  (table.Rows[i][2].ToString());
				List[i].DatePaycheck = PIn.PDate  (table.Rows[i][3].ToString());
			}
		}

		///<summary></summary>
		public static int GetForDate(DateTime date){
			for(int i=0;i<List.Length;i++){
				if(date.Date >= List[i].DateStart.Date && date.Date <= List[i].DateStop.Date){
					return i;
				}
			}
			return List.Length-1;//if we can't find a match, just return the last index
		}
		




	}

	
}




