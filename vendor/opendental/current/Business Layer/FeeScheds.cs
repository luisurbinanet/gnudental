//this is not used.  See the definition table instead.
/*using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the feesched table in the database.</summary>
	public class FeeSched{
		///<summary>Primary key.  This used to be kept in the Definition table.  When converted in 4.1, the DefNums were retained for simplicity.</summary>
		public int FeeSchedNum;
		///<summary>Description of the fee schedule as it will display.</summary>
		public string Descript;
		///<summary></summary>
		public FeeSchedType SchedType;
		///<summary></summary>
		public int ItemOrder;
		///<summary></summary>
		public bool IsHidden;
		
		///<summary></summary>
		public FeeSched Copy(){
			FeeSched f=new FeeSched();
			f.FeeSchedNum=FeeSchedNum;
			f.Descript=Descript;
			f.SchedType=SchedType;
			f.ItemOrder=ItemOrder;
			f.IsHidden=IsHidden;
			return f;
		}

		///<summary></summary>
		public void Update(){
			string command= "UPDATE feesched SET "
				+"Descript = '"   +POut.PString(Descript)+"'"
				+",SchedType = '" +POut.PInt   ((int)SchedType)+"'"
				+",ItemOrder = '" +POut.PInt   (ItemOrder)+"'"
				+",IsHidden = '"  +POut.PBool  (IsHidden)+"'"
				+" WHERE FeeSchedNum ='"+POut.PInt(FeeSchedNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Insert(){
			if(Prefs.RandomKeys){
				FeeSchedNum=MiscData.GetKey("feesched","FeeSchedNum");
			}
			string command= "INSERT INTO feesched (";
			if(Prefs.RandomKeys){
				command+="FeeSchedNum,";
			}
			command+="Descript,SchedType,ItemOrder,IsHidden) VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(FeeSchedNum)+"', ";
			}
			command+=
				 "'"+POut.PString(Descript)+"', "
				+"'"+POut.PInt   ((int)SchedType)+"', "
				+"'"+POut.PInt   (ItemOrder)+"', "
				+"'"+POut.PBool  (IsHidden)+"')";
			DataConnection dcon=new DataConnection();
 			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				FeeSchedNum=dcon.InsertID;
			}
		}

		//not allowed to delete a feeschedule


	}

	=========================================================================================
		=================================== class FeeScheds ==========================================

	///<summary></summary>
	public class FeeScheds{
			
		///<summary>Gets all FeeScheds, ordered by date.  </summary>
		public static FeeSched[] Refresh(){
			string command="SELECT * FROM FeeSched "
				+"ORDER BY DateFeeSched";
			return RefreshAndFill(command);
		}

		///<summary>Gets only FeeScheds which are not attached to transactions.</summary>
		public static FeeSched[] GetUnattached() {
			string command="SELECT * FROM FeeSched "
				+"WHERE NOT EXISTS(SELECT * FROM transaction WHERE FeeSched.FeeSchedNum=transaction.FeeSchedNum) "
				+"ORDER BY DateFeeSched";
			return RefreshAndFill(command);
		}

		///<summary>Gets a single FeeSched directly from the database.</summary>
		public static FeeSched GetOne(int FeeSchedNum) {
			string command="SELECT * FROM FeeSched "
				+"WHERE FeeSchedNum="+POut.PInt(FeeSchedNum);
			return RefreshAndFill(command)[0];
		}

		private static FeeSched[] RefreshAndFill(string command){
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			FeeSched[] List=new FeeSched[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new FeeSched();
				List[i].FeeSchedNum     = PIn.PInt(table.Rows[i][0].ToString());
				List[i].DateFeeSched    = PIn.PDate(table.Rows[i][1].ToString());
				List[i].BankAccountInfo= PIn.PString(table.Rows[i][2].ToString());
				List[i].Amount         = PIn.PDouble(table.Rows[i][3].ToString());
			}
			return List;
		}



	
	}

	

	


}


*/

















