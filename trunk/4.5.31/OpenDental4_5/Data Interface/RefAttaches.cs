using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class RefAttaches{

		///<summary>For one patient</summary>
		public static RefAttach[] Refresh(int patNum) {
			string command=
				"SELECT * FROM refattach"
				+" WHERE patnum = "+patNum.ToString()
				+" ORDER BY itemorder";
			DataTable table=General.GetTable(command);
			RefAttach[] List=new RefAttach[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new RefAttach();
				List[i].RefAttachNum= PIn.PInt(table.Rows[i][0].ToString());
				List[i].ReferralNum = PIn.PInt(table.Rows[i][1].ToString());
				List[i].PatNum      = PIn.PInt(table.Rows[i][2].ToString());
				List[i].ItemOrder   = PIn.PInt(table.Rows[i][3].ToString());
				List[i].RefDate     = PIn.PDate(table.Rows[i][4].ToString());
				List[i].IsFrom      = PIn.PBool(table.Rows[i][5].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static void Update(RefAttach attach){
			string command= "UPDATE refattach SET " 
				+ "referralnum = '" +POut.PInt   (attach.ReferralNum)+"'"
				+ ",patnum = '"     +POut.PInt   (attach.PatNum)+"'"
				+ ",itemorder = '"  +POut.PInt   (attach.ItemOrder)+"'"
				+ ",refdate = '"    +POut.PDate  (attach.RefDate)+"'"
				+ ",isfrom = '"     +POut.PBool  (attach.IsFrom)+"'"
				+" WHERE RefAttachNum = '" +POut.PInt(attach.RefAttachNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(RefAttach attach){
			string command= "INSERT INTO refattach (referralnum,patnum,"
				+"itemorder,refdate,IsFrom) VALUES("
				+"'"+POut.PInt   (attach.ReferralNum)+"', "
				+"'"+POut.PInt   (attach.PatNum)+"', "
				+"'"+POut.PInt   (attach.ItemOrder)+"', "
				+"'"+POut.PDate  (attach.RefDate)+"', "
				+"'"+POut.PBool  (attach.IsFrom)+"')";
 			attach.RefAttachNum=General.NonQ(command,true);
		}

		///<summary></summary>
		public static void Delete(RefAttach attach){
			string command= "DELETE FROM refattach "
				+"WHERE refattachnum = '"+attach.RefAttachNum+"'";
 			General.NonQ(command);
		}

		

		///<summary></summary>
		public static bool IsReferralAttached(int referralNum){
			string command =
				"SELECT * FROM refattach"
				+" WHERE referralnum = '"+referralNum+"'";
 			DataTable table=General.GetTable(command);
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
			DataTable table=General.GetTable(command);
			string[] retStr=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				retStr[i]=PIn.PString(table.Rows[i][0].ToString());
			}
			return retStr;
		}

		///<summary>Pass in all the refattaches for the patient.  This funtion finds the first referral from a Dr and returns that Dr's name.  Used in specialty practices.  Function is only used right now in the Dr. Ceph bridge.</summary>
		public static string GetReferringDr(RefAttach[] attachList){
			if(attachList.Length==0){
				return "";
			}
			if(!attachList[0].IsFrom){
				return "";
			}
			Referral referral=Referrals.GetReferral(attachList[0].ReferralNum);
			if(referral.PatNum!=0){
				return "";
			}
			string retVal=referral.FName+" "+referral.MName+" "+referral.LName;
			if(referral.Title!=""){
				retVal+=", "+referral.Title;
			}
			return retVal;
		}
	
		

	}

	

	

}













