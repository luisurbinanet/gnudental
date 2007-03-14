using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Used in the Accounting section.  Each row represents one reconcile.  Transactions will be attached to it.</summary>
	public class Reconcile{
		///<summary>Primary key.</summary>
		public int ReconcileNum;
		///<summary>FK to account.AccountNum</summary>
		public int AccountNum;
		///<summary>User enters starting balance here.</summary>
		public double StartingBal;
		///<summary>User enters ending balance here.</summary>
		public double EndingBal;
		///<summary>The date that the reconcile was performed.</summary>
		public DateTime DateReconcile;
		///<summary>If StartingBal + sum of entries selected = EndingBal, then user can lock.  Unlock requires special permission, which nobody will have by default.</summary>
		public bool IsLocked;

		///<summary></summary>
		public Reconcile Copy() {
			Reconcile r=new Reconcile();
			r.ReconcileNum=ReconcileNum;
			r.AccountNum=AccountNum;
			r.StartingBal=StartingBal;
			r.EndingBal=EndingBal;
			r.DateReconcile=DateReconcile;
			r.IsLocked=IsLocked;
			return r;
		}

		///<summary></summary>
		public void Insert() {
			if(Prefs.RandomKeys) {
				ReconcileNum=MiscData.GetKey("reconcile","ReconcileNum");
			}
			string command="INSERT INTO reconcile (";
			if(Prefs.RandomKeys) {
				command+="ReconcileNum,";
			}
			command+="AccountNum,StartingBal,EndingBal,DateReconcile,IsLocked) VALUES(";
			if(Prefs.RandomKeys) {
				command+="'"+POut.PInt(ReconcileNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (AccountNum)+"', "
				+"'"+POut.PDouble(StartingBal)+"', "
				+"'"+POut.PDouble(EndingBal)+"', "
				+"'"+POut.PDate  (DateReconcile)+"', "
				+"'"+POut.PBool  (IsLocked)+"')";
			DataConnection dcon=new DataConnection();
			if(Prefs.RandomKeys) {
				dcon.NonQ(command);
			}
			else {
				dcon.NonQ(command,true);
				ReconcileNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void Update() {
			string command= "UPDATE reconcile SET "
				+"AccountNum = '"    +POut.PInt   (AccountNum)+"' "
				+",StartingBal= '"   +POut.PDouble(StartingBal)+"' "
				+",EndingBal = '"    +POut.PDouble(EndingBal)+"' "
				+",DateReconcile = '"+POut.PDate  (DateReconcile)+"' "
				+",IsLocked = '"     +POut.PBool  (IsLocked)+"' "
				+"WHERE ReconcileNum = '"+POut.PInt(ReconcileNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary>Throws exception if Reconcile is in use.</summary>
		public void Delete() {
			//check to see if any journal entries are attached to this Reconcile
			string command="SELECT COUNT(*) FROM journalentry WHERE ReconcileNum="+POut.PInt(ReconcileNum);
			DataConnection dcon=new DataConnection();
			if(dcon.GetCount(command)!="0"){
				throw new ApplicationException(Lan.g("FormReconcileEdit",
					"Not allowed to delete a Reconcile with existing journal entries."));
			}
			command="DELETE FROM reconcile WHERE ReconcileNum = "+POut.PInt(ReconcileNum);
			dcon.NonQ(command);
		}

	}

	/*=========================================================================================
		=================================== class Reconciles ==========================================*/

	///<summary>The two lists get refreshed the first time they are needed rather than at startup.</summary>
	public class Reconciles{

		///<summary></summary>
		public static Reconcile[] GetList(int accountNum){
			string command="SELECT * FROM reconcile WHERE AccountNum="+POut.PInt(accountNum)
				+" ORDER BY DateReconcile";
			return RefreshAndFill(command);
		}

		///<summary>Gets one reconcile directly from the database.  Program will crash if reconcile not found.</summary>
		public static Reconcile GetOne(int reconcileNum){
			string command="SELECT * FROM reconcile WHERE ReconcileNum="+POut.PInt(reconcileNum);
			return RefreshAndFill(command)[0];
		}

		private static Reconcile[] RefreshAndFill(string command){
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			Reconcile[] List=new Reconcile[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new Reconcile();
				List[i].ReconcileNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].AccountNum   = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].StartingBal  = PIn.PDouble(table.Rows[i][2].ToString());
				List[i].EndingBal    = PIn.PDouble(table.Rows[i][3].ToString());
				List[i].DateReconcile= PIn.PDate  (table.Rows[i][4].ToString());
				List[i].IsLocked     = PIn.PBool  (table.Rows[i][5].ToString());
			}
			return List;
		}

		

	}

	
}




