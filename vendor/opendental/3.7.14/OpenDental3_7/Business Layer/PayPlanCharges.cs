using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the payplancharge table in the database.</summary>
	public class PayPlanCharge{
		///<summary>Primary key.</summary>
		public int PayPlanChargeNum;
		///<summary>Foreign key to payplan.PayPlanNum.</summary>
		public int PayPlanNum;
		///<summary>Foreign key to patient.PatNum.  The guarantor account that each charge will affect.</summary>
		public int Guarantor;
		///<summary>Foreign key to patient.PatNum.  The patient account that the principal gets removed from.</summary>
		public int PatNum;
		///<summary>The date that the charge will show on the patient account.  Any charge with a future date will not show on the account and will not affect the balance.</summary>
		public DateTime ChargeDate;
		///<summary>The principal portion of this payment.</summary>
		public double Principal;
		///<summary>The interest portion of this payment.</summary>
		public double Interest;
		///<summary>Any note about this particular payment plan charge</summary>
		public string Note;
		
		///<summary></summary>
		public PayPlanCharge Copy(){
			PayPlanCharge p=new PayPlanCharge();
			p.PayPlanChargeNum=PayPlanChargeNum;
			p.PayPlanNum=PayPlanNum;
			p.Guarantor=Guarantor;
			p.PatNum=PatNum;
			p.ChargeDate=ChargeDate;
			p.Principal=Principal;
			p.Interest=Interest;
			p.Note=Note;
			return p;
		}

		///<summary></summary>
		private void Update(){
			string command= "UPDATE payplancharge SET " 
				+"PayPlanChargeNum = '"+POut.PInt   (PayPlanChargeNum)+"'"
				+",PayPlanNum = '"     +POut.PInt   (PayPlanNum)+"'"
				+",Guarantor = '"      +POut.PInt   (Guarantor)+"'"
				+",PatNum = '"         +POut.PInt   (PatNum)+"'"
				+",ChargeDate = '"     +POut.PDate  (ChargeDate)+"'"
				+",Principal = '"      +POut.PDouble(Principal)+"'"
				+",Interest = '"       +POut.PDouble(Interest)+"'"
				+",Note = '"           +POut.PString(Note)+"'"
				+" WHERE PayPlanChargeNum = '"+POut.PInt(PayPlanChargeNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		private void Insert(){
			if(Prefs.RandomKeys){
				PayPlanChargeNum=MiscData.GetKey("payplancharge","PayPlanChargeNum");
			}
			string command= "INSERT INTO payplancharge (";
			if(Prefs.RandomKeys){
				command+="PayPlanChargeNum,";
			}
			command+="PayPlanNum,Guarantor,PatNum,ChargeDate,Principal,Interest,Note) VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(PayPlanChargeNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (PayPlanNum)+"', "
				+"'"+POut.PInt   (Guarantor)+"', "
				+"'"+POut.PInt   (PatNum)+"', "
				+"'"+POut.PDate  (ChargeDate)+"', "
				+"'"+POut.PDouble(Principal)+"', "
				+"'"+POut.PDouble(Interest)+"', "
				+"'"+POut.PString(Note)+"')";
			DataConnection dcon=new DataConnection();
 			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				PayPlanChargeNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void InsertOrUpdate(bool isNew){
			//if(){
			//	throw new ApplicationException(Lan.g(this,""));
			//}
			if(isNew){
				Insert();
			}
			else{
				Update();
			}
		}

		///<summary></summary>
		public void Delete(){
			string command= "DELETE from payplancharge WHERE PayPlanChargeNum = '"
				+POut.PInt(PayPlanChargeNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}


	}

	/*=========================================================================================
		=================================== class PayPlanCharges ==========================================*/

	///<summary></summary>
	public class PayPlanCharges{
			
		///<summary>Gets all PayPlanCharges for a guarantor or patient, ordered by date.</summary>
		public static PayPlanCharge[] Refresh(int patNum){
			string command=
				"SELECT * FROM payplancharge "
				+"WHERE Guarantor='"+POut.PInt(patNum)+"' "
				+"OR PatNum='"+POut.PInt(patNum)+"' "
				+"ORDER BY ChargeDate";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			PayPlanCharge[] List=new PayPlanCharge[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new PayPlanCharge();
				List[i].PayPlanChargeNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PayPlanNum      = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].Guarantor       = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].PatNum          = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].ChargeDate      = PIn.PDate  (table.Rows[i][4].ToString());
				List[i].Principal       = PIn.PDouble(table.Rows[i][5].ToString());
				List[i].Interest        = PIn.PDouble(table.Rows[i][6].ToString());
				List[i].Note            = PIn.PString(table.Rows[i][7].ToString());
			}
			return List;
		}

		///<summary>Must pass in a list of charges for the guarantor.  The ones for this particular payplan will be returned.</summary>
		public static PayPlanCharge[] GetForPayPlan(int payPlanNum,PayPlanCharge[] ChargeList){
			ArrayList AL=new ArrayList();
			for(int i=0;i<ChargeList.Length;i++){
				if(ChargeList[i].PayPlanNum==payPlanNum){
					AL.Add(ChargeList[i]);
				}
			}
			PayPlanCharge[] retVal=new PayPlanCharge[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		public static void DeleteAllInPlan(int payPlanNum){
			string command="DELETE FROM payplancharge WHERE PayPlanNum="+payPlanNum.ToString();
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

	
	}

	

	


}




















