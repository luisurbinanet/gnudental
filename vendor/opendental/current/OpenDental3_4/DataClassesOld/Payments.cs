using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary></summary>
	public class Payment{
		///<summary></summary>
		public int PayNum;
		///<summary>Foreign key to definition.DefNum.</summary>
		public int PayType;
		///<summary></summary>
		public DateTime PayDate;
		///<summary></summary>
		public double PayAmt;
		///<summary></summary>
		public string CheckNum;
		///<summary></summary>
		public string BankBranch;
		///<summary></summary>
		public string PayNote;
		///<summary>Set to true to indicate that a payment is split between patients.  Just makes a few functions easier.  Might be eliminated.</summary>
		public bool IsSplit;
		///<summary>Foreign Key to patient.PatNum.  The patient where the payment entry will show.  But only the splits affect accounts.</summary>
		public int PatNum;
		///<summary>Foreign Key to clinic.ClinicNum.  Can be 0. Copied from patient.ClinicNum when creating payment, but user can override.</summary>
		public int ClinicNum;


		///<summary></summary>
		public void Update(){
			string command="UPDATE payment SET " 
				+ "paytype = '"      +POut.PInt   (PayType)+"'"
				+ ",paydate = '"     +POut.PDate  (PayDate)+"'"
				+ ",payamt = '"      +POut.PDouble(PayAmt)+"'"
				+ ",checknum = '"    +POut.PString(CheckNum)+"'"
				+ ",bankbranch = '"  +POut.PString(BankBranch)+"'"
				+ ",paynote = '"     +POut.PString(PayNote)+"'"
				+ ",issplit = '"     +POut.PBool  (IsSplit)+"'"
				+ ",patnum = '"      +POut.PInt   (PatNum)+"'"
				+ ",ClinicNum = '"   +POut.PInt   (ClinicNum)+"'"
				+" WHERE payNum = '" +POut.PInt   (PayNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Insert(){
			string command= "INSERT INTO payment (paytype,paydate,payamt, "
				+"checknum,bankbranch,paynote,issplit,patnum,ClinicNum) VALUES("
				+"'"+POut.PInt   (PayType)+"', "
				+"'"+POut.PDate  (PayDate)+"', "
				+"'"+POut.PDouble(PayAmt)+"', "
				+"'"+POut.PString(CheckNum)+"', "
				+"'"+POut.PString(BankBranch)+"', "
				+"'"+POut.PString(PayNote)+"', "
				+"'"+POut.PBool  (IsSplit)+"', "
				+"'"+POut.PInt   (PatNum)+"', "
				+"'"+POut.PInt   (ClinicNum)+"')";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			PayNum=dcon.InsertID;
			//MessageBox.Show(PayNum.ToString());
		}

		///<summary></summary>
		public void Delete(){
			string command= "DELETE from payment WHERE payNum = '"+PayNum.ToString()+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

	}

	/*=========================================================================================
		=================================== class Payments ==========================================*/

	///<summary></summary>
	public class Payments{

		///<summary>Gets all payments for the specified patient. This has NOTHING to do with pay splits.  Must use pay splits for accounting.  This is only for display in Account module.</summary>
		public static Payment[] Refresh(int patNum){
			string command=
				"SELECT * from payment"
				+" WHERE PatNum="+patNum.ToString();
			return RefreshAndFill(command);
		}

		///<summary>Get one specific payment from db.</summary>
		public static Payment GetPayment(int payNum){
			string command=
				"SELECT * from payment"
				+" WHERE PayNum = '"+payNum+"'";
			return RefreshAndFill(command)[0];
		}

		///<summary>Get all specified payments.</summary>
		public static Payment[] GetPayments(int[] payNums){
			if(payNums.Length==0){
				return new Payment[0];
			}
			string command=
				"SELECT * from payment"
				+" WHERE";
			for(int i=0;i<payNums.Length;i++){
				if(i>0){
					command+=" OR";
				}
				command+=" PayNum="+payNums[i].ToString();
			}
			return RefreshAndFill(command);
		}

		private static Payment[] RefreshAndFill(string command){
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			Payment[] List=new Payment[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new Payment();
				List[i].PayNum    =PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PayType   =PIn.PInt   (table.Rows[i][1].ToString());
				List[i].PayDate   =PIn.PDate  (table.Rows[i][2].ToString());
				List[i].PayAmt    =PIn.PDouble(table.Rows[i][3].ToString());
				List[i].CheckNum  =PIn.PString(table.Rows[i][4].ToString());
				List[i].BankBranch=PIn.PString(table.Rows[i][5].ToString());
				List[i].PayNote   =PIn.PString(table.Rows[i][6].ToString());
				List[i].IsSplit   =PIn.PBool  (table.Rows[i][7].ToString());
				List[i].PatNum    =PIn.PInt   (table.Rows[i][8].ToString());
				List[i].ClinicNum =PIn.PInt   (table.Rows[i][9].ToString());
			}
			return List;
		}

		///<summary>Used for display in ProcEdit. List MUST include the requested payment. Use GetPayments to get the list.</summary>
		public static Payment GetFromList(int payNum,Payment[] List){
			for(int i=0;i<List.Length;i++){
				if(List[i].PayNum==payNum){
					return List[i];
				}
			}
			return null;//should never happen
		}

		/*
		///<summary></summary>
		public static string GetInfo(int payNum){
			string retStr;
			Payment Cur=GetPayment(payNum);
			retStr=Defs.GetName(DefCat.PaymentTypes,Cur.PayType);
			if(Cur.IsSplit) retStr=retStr
				+"  "+Cur.PayAmt.ToString("c")
				+"  "+Cur.PayDate.ToString("d")
				+" "+Lan.g("Payments","split between patients");
			return retStr;
		}*/
	}

	

	

}










