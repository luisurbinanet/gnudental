using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the refattach table in the database.  Attaches a reference to a patient.</summary>
	public class RefAttach{  
		///<summary>Primary key.</summary>
		public int RefAttachNum;
		///<summary>Foreign key to referral.ReferralNum.</summary>
		public int ReferralNum;
		///<summary>Foreign key to patient.PatNum.</summary>
		public int PatNum;
		///<summary>Order to display in patient info. Will be automated more in future.</summary>
		public int ItemOrder;
		///<summary>Date of referral.</summary>
		public DateTime RefDate;//
		///<summary>true=from, false=to</summary>
		public bool IsFrom;

		///<summary>Returns a copy of this RefAttach.</summary>
		public RefAttach Copy(){
			RefAttach r=new RefAttach();
			r.RefAttachNum=RefAttachNum;
			r.ReferralNum=ReferralNum;
			r.PatNum=PatNum;
			r.ItemOrder=ItemOrder;
			r.RefDate=RefDate;
			r.IsFrom=IsFrom;
			return r;
		}

		///<summary></summary>
		public void Update(){
			string command= "UPDATE refattach SET " 
				+ "referralnum = '" +POut.PInt   (ReferralNum)+"'"
				+ ",patnum = '"     +POut.PInt   (PatNum)+"'"
				+ ",itemorder = '"  +POut.PInt   (ItemOrder)+"'"
				+ ",refdate = '"    +POut.PDate  (RefDate)+"'"
				+ ",isfrom = '"     +POut.PBool  (IsFrom)+"'"
				+" WHERE RefAttachNum = '" +POut.PInt(RefAttachNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Insert(){
			string command= "INSERT INTO refattach (referralnum,patnum,"
				+"itemorder,refdate,IsFrom) VALUES("
				+"'"+POut.PInt   (ReferralNum)+"', "
				+"'"+POut.PInt   (PatNum)+"', "
				+"'"+POut.PInt   (ItemOrder)+"', "
				+"'"+POut.PDate  (RefDate)+"', "
				+"'"+POut.PBool  (IsFrom)+"')";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			RefAttachNum=dcon.InsertID;
		}

		///<summary></summary>
		public void Delete(){
			string command= "DELETE FROM refattach "
				+"WHERE refattachnum = '"+RefAttachNum+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		


	}

	/*================================================================================================
		=================================== class RefAttaches ==========================================*/
///<summary></summary>
	public class RefAttaches:DataClass{
		//<summary>for this patient only</summary>
		//public static RefAttach[] List;
		//<summary></summary>
		//public static RefAttach Cur;
		//<summary></summary>
		//public static Hashtable HList;//key:refAttachNum, value:RefAttach

		///<summary>For one patient</summary>
		public static RefAttach[] Refresh(int patNum){
			string command=
				"SELECT * FROM refattach"
				+" WHERE patnum = "+patNum.ToString()
				+" ORDER BY itemorder";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			RefAttach[] List=new RefAttach[table.Rows.Count];
			//HList=new Hashtable();
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new RefAttach();
				List[i].RefAttachNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ReferralNum = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].PatNum      = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].ItemOrder   = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].RefDate     = PIn.PDate  (table.Rows[i][4].ToString());
				List[i].IsFrom      = PIn.PBool  (table.Rows[i][5].ToString());       
				//HList.Add(List[i].RefAttachNum,List[i]);
			}
			return List;
		}

		///<summary></summary>
		public static bool IsReferralAttached(int referralNum){
			string command =
				"SELECT * FROM refattach"
				+" WHERE referralnum = '"+referralNum+"'";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count > 0){
				return true;
			}
			else{
				return false;
			}
		}

		///<summary>Returns a list of patient names that are attached to this referral. Used to display in the referral edit window.</summary>
		public static string[] GetPats(int refNum,bool IsFrom){
			string command="SELECT CONCAT(patient.LName,', ',patient.FName) "
				+"FROM patient,refattach,referral " 
				+"WHERE patient.PatNum=refattach.PatNum "
				+"AND refattach.ReferralNum=referral.ReferralNum "
				+"AND refattach.IsFrom="+POut.PBool(IsFrom)
				+" AND referral.ReferralNum="+refNum.ToString();
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			string[] retStr=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				retStr[i]=PIn.PString(table.Rows[i][0].ToString());
			}
			return retStr;
		}
	
		

	}

	

	

}












