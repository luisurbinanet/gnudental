using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	/// <summary>Each row represents one charge that will be added monthly.</summary>
	public class RepeatCharge{
		/// <summary>Primary key</summary>
		public int RepeatChargeNum;
		/// <summary>FK to patient.PatNum.</summary>
		public int PatNum;
		///<summary>FK to procedurecode.ADACode.  The code that will be added to the account as a completed procedure.</summary>
		public string ADACode;
		///<summary>The amount that will be charged.  The amount from the procedurecode will not be used.  This way, a repeating charge cannot be accidentally altered.</summary>
		public double ChargeAmt;
		///<summary>The date of the first charge.  Charges will always be added on the same day of the month as the start date.  If more than one month goes by, then multiple charges will be added.</summary>
		public DateTime DateStart;
		///<summary>The last date on which a charge is allowed.  So if you want 12 charges, and the start date is 8/1/05, then the stop date should be 7/1/05, not 8/1/05.  Can be blank (0001-01-01) to represent a perpetual repeating charge.</summary>
		public DateTime DateStop;
		///<summary>Any note for internal use.</summary>
		public string Note;	

		///<summary></summary>
		public RepeatCharge Copy(){
			RepeatCharge r=new RepeatCharge();
			r.RepeatChargeNum=RepeatChargeNum;
			r.PatNum=PatNum;
			r.ADACode=ADACode;
			r.ChargeAmt=ChargeAmt;
			r.DateStart=DateStart;
			r.DateStop=DateStop;
			r.Note=Note;
			return r;
		}

		///<summary></summary>
		public void Update(){
			string command="UPDATE repeatcharge SET " 
				+"PatNum = '"    +POut.PInt   (PatNum)+"'"
				+",ADACode = '"  +POut.PString(ADACode)+"'"
				+",ChargeAmt = '"+POut.PDouble(ChargeAmt)+"'"
				+",DateStart = '"+POut.PDate  (DateStart)+"'"
				+",DateStop = '" +POut.PDate  (DateStop)+"'"
				+",Note = '"     +POut.PString(Note)+"'"
				+" WHERE RepeatChargeNum = '" +POut.PInt(RepeatChargeNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Insert(){
			if(Prefs.RandomKeys){
				RepeatChargeNum=MiscData.GetKey("repeatcharge","RepeatChargeNum");
			}
			string command="INSERT INTO repeatcharge (";
			if(Prefs.RandomKeys){
				command+="RepeatChargeNum,";
			}
			command+="PatNum,ADACode,ChargeAmt,DateStart,DateStop,Note) VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(RepeatChargeNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (PatNum)+"', "
				+"'"+POut.PString(ADACode)+"', "
				+"'"+POut.PDouble(ChargeAmt)+"', "
				+"'"+POut.PDate  (DateStart)+"', "
				+"'"+POut.PDate  (DateStop)+"', "
				+"'"+POut.PString(Note)+"')";
			DataConnection dcon=new DataConnection();
			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				RepeatChargeNum=dcon.InsertID;
			}
		}

		///<summary>Called from FormRepeatCharge.</summary>
		public void Delete(){
			string command="DELETE FROM repeatcharge WHERE RepeatChargeNum ="+POut.PInt(RepeatChargeNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

	}

	/*=========================================================================================
	=================================== class RepeatCharges ==========================================*/

	///<summary></summary>
	public class RepeatCharges{
		///<summary>Gets a list of all RepeatCharges for a given patient.  Supply 0 to get a list for all patients.</summary>
		public static RepeatCharge[] Refresh(int patNum){
			string command="SELECT * from repeatcharge";
			if(patNum!=0){
				command+=" WHERE PatNum = "+POut.PInt(patNum);
			}
			command+=" ORDER BY DateStart";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			RepeatCharge[] List=new RepeatCharge[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new RepeatCharge();
				List[i].RepeatChargeNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum         = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].ADACode        = PIn.PString(table.Rows[i][2].ToString());
				List[i].ChargeAmt      = PIn.PDouble(table.Rows[i][3].ToString());
				List[i].DateStart      = PIn.PDate  (table.Rows[i][4].ToString());
				List[i].DateStop       = PIn.PDate  (table.Rows[i][5].ToString());
				List[i].Note           = PIn.PString(table.Rows[i][6].ToString());
			}
			return List;
		}

		///<summary>Used in FormRepeatChargesUpdate to get a list of the dates of procedures that have the adacode and patnum specified.</summary>
		public static ArrayList GetDates(string aDACode,int patNum){
			ArrayList retVal=new ArrayList();
			string command="SELECT ProcDate FROM procedurelog "
				+"WHERE PatNum="+POut.PInt(patNum)
				+" AND ADACode='"+POut.PString(aDACode)
				+"' AND ProcStatus=2";//complete
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++){
				retVal.Add(PIn.PDate(table.Rows[i][0].ToString()));
			}
			return retVal;
		}
		

		

		


	}

	

	


}










