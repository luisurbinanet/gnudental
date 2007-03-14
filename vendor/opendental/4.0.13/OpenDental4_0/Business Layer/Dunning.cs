using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the dunning table in the database.</summary>
	public class Dunning{
		///<summary>Primary key.</summary>
		public int DunningNum;
		///<summary>The actual dunning message that will go on the patient bill.</summary>
		public string DunMessage;
		///<summary>Foreign key to definition.DefNum.</summary>
		public int BillingType;
		///<summary>This is an int field, but program forces only 0,30,60,or 90.</summary>
		public int AgeAccount;
		///<summary>Set Y to only show if insurance is pending.</summary>
		public YN InsIsPending;

		///<summary>Returns a copy of this Dunning.</summary>
		public Dunning Copy(){
			Dunning d=new Dunning();
			d.DunningNum=DunningNum;
			d.DunMessage=DunMessage;
			d.BillingType=BillingType;
			d.AgeAccount=AgeAccount;
			d.InsIsPending=InsIsPending;
			return d;
		}

		///<summary></summary>
		private void Insert(){
			string command= "INSERT INTO dunning (DunMessage,BillingType,AgeAccount,InsIsPending) VALUES("
				+"'"+POut.PString(DunMessage)+"', "
				+"'"+POut.PInt   (BillingType)+"', "
				+"'"+POut.PInt   (AgeAccount)+"', "
				+"'"+POut.PInt   ((int)InsIsPending)+"')";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			DunningNum=dcon.InsertID;
		}

		///<summary></summary>
		private void Update(){
			string command= "UPDATE dunning SET " 
				+ "DunMessage = '"       +POut.PString(DunMessage)+"'"
				+ ",BillingType = '"     +POut.PInt   (BillingType)+"'"
				+ ",AgeAccount = '"      +POut.PInt   (AgeAccount)+"'"
				+ ",InsIsPending = '"    +POut.PInt   ((int)InsIsPending)+"'"
				+" WHERE DunningNum = '" +POut.PInt   (DunningNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public void InsertOrUpdate(bool IsNew){
			//if(){
				//throw new Exception(Lan.g(this,""));
			//}
			if(IsNew){
				Insert();
			}
			else{
				Update();
			}
		}

		///<summary></summary>
		public void Delete(){
			string command="DELETE FROM dunning" 
				+" WHERE DunningNum = "+POut.PInt(DunningNum);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}


    
	}

	/*=========================================================================================
	=================================== class Dunnings ==========================================*/
	///<summary></summary>
	public class Dunnings{

		///<summary>Gets a list of all dunnings.</summary>
		public static Dunning[] Refresh(){
			string command="SELECT * FROM dunning "
				+"ORDER BY BillingType,AgeAccount,InsIsPending";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			Dunning[] List=new Dunning[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Dunning();
				List[i].DunningNum     = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].DunMessage     = PIn.PString(table.Rows[i][1].ToString());
				List[i].BillingType    = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].AgeAccount     = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].InsIsPending   = (YN)PIn.PInt   (table.Rows[i][4].ToString());
			}
			return List;
		}

		public static string GetMessage(Dunning[] dunList, int billingType,int ageAccount,YN insIsPending){
			//loop backwards through Dunning list and find the first dunning that matches criteria.
			for(int i=dunList.Length-1;i>=0;i--){
				if(dunList[i].BillingType!=0//0 in the list matches all
					&& dunList[i].BillingType!=billingType){
					continue;
				}
				if(ageAccount < dunList[i].AgeAccount){//match if age is >= item in list
					continue;
				}
				if(dunList[i].InsIsPending!=YN.Unknown//unknown in the list matches all
					&& dunList[i].InsIsPending!=insIsPending){
					continue;
				}
				return dunList[i].DunMessage;
			}
			return "";
		}

		

	}
	


}













