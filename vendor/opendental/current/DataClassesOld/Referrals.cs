using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	
///<summary></summary>
	public class Referrals{
		///<summary>All referrals for all patients</summary>
		public static Referral[] List;
		//should later add a faster refresh sequence.
		private static Hashtable HList;

		///<summary>Refreshes all referrals for all patients.  Need to rework at some point so less memory is consumed.  Also refreshes dynamically, so no need to invalidate local data.</summary>
		public static void Refresh(){
			string command=
				"SELECT * from referral "
				+"ORDER BY lname";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			List=new Referral[table.Rows.Count];
			HList=new Hashtable();
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Referral();
				List[i].ReferralNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].LName      = PIn.PString(table.Rows[i][1].ToString());
				List[i].FName      = PIn.PString(table.Rows[i][2].ToString());
				List[i].MName      = PIn.PString(table.Rows[i][3].ToString());
				List[i].SSN        = PIn.PString(table.Rows[i][4].ToString());
				List[i].UsingTIN   = PIn.PBool  (table.Rows[i][5].ToString());
				List[i].Specialty  = (DentalSpecialty)PIn.PInt(table.Rows[i][6].ToString());
				List[i].ST         = PIn.PString(table.Rows[i][7].ToString());
				List[i].Telephone  = PIn.PString(table.Rows[i][8].ToString());
				List[i].Address    = PIn.PString(table.Rows[i][9].ToString());
				List[i].Address2   = PIn.PString(table.Rows[i][10].ToString());
				List[i].City       = PIn.PString(table.Rows[i][11].ToString());
				List[i].Zip        = PIn.PString(table.Rows[i][12].ToString());
				List[i].Note       = PIn.PString(table.Rows[i][13].ToString());
				List[i].Phone2     = PIn.PString(table.Rows[i][14].ToString());
				List[i].IsHidden   = PIn.PBool  (table.Rows[i][15].ToString());
				List[i].NotPerson  = PIn.PBool  (table.Rows[i][16].ToString());
				List[i].Title      = PIn.PString(table.Rows[i][17].ToString());
				List[i].EMail      = PIn.PString(table.Rows[i][18].ToString());
				List[i].PatNum     = PIn.PInt   (table.Rows[i][19].ToString());
				HList.Add(List[i].ReferralNum,List[i]);
			}
		}
	
		///<summary>Gets Referral info from memory (HList). Does not make a call to the database.</summary>
		public static Referral GetReferral(int referralNum){
			if(referralNum==0){
				return null;
			}
			if(HList.ContainsKey(referralNum)){
				return (Referral)HList[referralNum];
			}
			Refresh();//must be outdated
			if(!HList.ContainsKey(referralNum)){
				MessageBox.Show("Error.  Referral not found: "+referralNum.ToString());
				return null;
			}
			return (Referral)HList[referralNum];
		}


	}

	

	


}













