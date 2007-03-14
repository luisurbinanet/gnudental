using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the referral table in the database.</summary>
	public struct Referral{
		///<summary>Primary key.</summary>
		public int ReferralNum;
		///<summary>Last name.</summary>
		public string LName;
		///<summary>First name.</summary>
		public string FName;
		///<summary>Middle name or initial.</summary>
		public string MName;
		///<summary>SSN or TIN, no punctuation.</summary>
		public string SSN;
		///<summary>Specificies if SSN is real SSN.</summary>
		public bool UsingTIN;
		///<summary>See the DentalSpecialty enumeration.</summary>
		public DentalSpecialty Specialty;
		///<summary>State</summary>
		public string ST;
		///<summary>Primary phone, restrictive, must only be 10 digits and only numbers.</summary>
		public string Telephone;
		///<summary></summary>
		public string Address;
		///<summary></summary>
		public string Address2;
		///<summary></summary>
		public string City;
		///<summary></summary>
		public string Zip;
		///<summary>Holds important info about the referral.</summary>
		public string Note;//
		///<summary>Additional phone no restrictions</summary>
		public string Phone2;
		///<summary>Can't delete a referral, but can hide if not needed any more.</summary>
		public bool IsHidden;//
		///<summary>Set to true for referralls such as Yellow Pages.</summary>
		public bool NotPerson;
		///<summary>i.e. DMD or DDS</summary>
		public string Title;
		///<summary></summary>
		public string EMail;
		///<summary>Foreign key to patient.PatNum for referrals that are patients.</summary>
		public int PatNum;
	}
	
	/*==============================================================================================
		=================================== class Referrals ==========================================*/
///<summary></summary>
	public class Referrals:DataClass{
		///<summary>All referrals for all patients</summary>
		public static Referral[] List;
		//should later add a list for single patient, along with a faster refresh sequence.
		///<summary></summary>
		public static Referral Cur;
		private static Hashtable HList;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from referral "
				+"ORDER BY lname";
			FillTable();
			List=new Referral[table.Rows.Count];
			HList=new Hashtable();
			for(int i=0;i<table.Rows.Count;i++){
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
	
		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE referral SET " 
				+ "lname = '"      +POut.PString(Cur.LName)+"'"
				+ ",fname = '"     +POut.PString(Cur.FName)+"'"
				+ ",mname = '"     +POut.PString(Cur.MName)+"'"
				+ ",ssn = '"       +POut.PString(Cur.SSN)+"'"
				+ ",usingtin = '"  +POut.PBool  (Cur.UsingTIN)+"'"
				+ ",specialty = '" +POut.PInt   ((int)Cur.Specialty)+"'"
				+ ",st = '"        +POut.PString(Cur.ST)+"'"
				+ ",telephone = '" +POut.PString(Cur.Telephone)+"'"
				+ ",address = '"   +POut.PString(Cur.Address)+"'"
				+ ",address2 = '"  +POut.PString(Cur.Address2)+"'"
				+ ",city = '"      +POut.PString(Cur.City)+"'"
				+ ",zip = '"       +POut.PString(Cur.Zip)+"'"
				+ ",note = '"      +POut.PString(Cur.Note)+"'"
				+ ",phone2 = '"    +POut.PString(Cur.Phone2)+"'"
				+ ",ishidden = '"  +POut.PBool  (Cur.IsHidden)+"'"
				+ ",notperson = '" +POut.PBool  (Cur.NotPerson)+"'"
				+ ",title = '"     +POut.PString(Cur.Title)+"'"
				+ ",email = '"     +POut.PString(Cur.EMail)+"'"
				+ ",patnum = '"     +POut.PInt  (Cur.PatNum)+"'"     

				+" WHERE ReferralNum = '" +POut.PInt (Cur.ReferralNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO referral (lname,fname,mname,ssn,usingtin,specialty,st,"
				+"telephone,address,address2,city,zip,note,phone2,ishidden,notperson,title,email,patnum) VALUES("
				+"'"+POut.PString(Cur.LName)+"', "
				+"'"+POut.PString(Cur.FName)+"', "
				+"'"+POut.PString(Cur.MName)+"', "
				+"'"+POut.PString(Cur.SSN)+"', "
				+"'"+POut.PBool  (Cur.UsingTIN)+"', "
				+"'"+POut.PInt   ((int)Cur.Specialty)+"', "
				+"'"+POut.PString(Cur.ST)+"', "
				+"'"+POut.PString(Cur.Telephone)+"', "    
				+"'"+POut.PString(Cur.Address)+"', "
				+"'"+POut.PString(Cur.Address2)+"', "
				+"'"+POut.PString(Cur.City)+"', "
				+"'"+POut.PString(Cur.Zip)+"', "
				+"'"+POut.PString(Cur.Note)+"', "
				+"'"+POut.PString(Cur.Phone2)+"', "
				+"'"+POut.PBool  (Cur.IsHidden)+"', "
				+"'"+POut.PBool  (Cur.NotPerson)+"', "
				+"'"+POut.PString(Cur.Title)+"', "
				+"'"+POut.PString(Cur.EMail)+"', "
				+"'"+POut.PInt   (Cur.PatNum)+"')";
			NonQ(true);
			Cur.ReferralNum=InsertID;
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM referral "
				+"WHERE referralnum = '"+Cur.ReferralNum+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void GetCur(int referralNum){
			if(referralNum==0){
				Cur=new Referral();
				return;
			}
			if(HList.ContainsKey(referralNum)){
				Cur=(Referral)HList[referralNum];
				return;
			}
			Refresh();//must be outdated
			if(!HList.ContainsKey(referralNum)){
				MessageBox.Show("Error.  Referral not found: "+referralNum.ToString());
				Cur=new Referral();
				return;
			}
			Cur=(Referral)HList[referralNum];
		}

		///<summary></summary>
		public static string GetCurName(){
			if(Cur.ReferralNum==0)
				return "";
			string retVal=Referrals.Cur.LName;
			if(Referrals.Cur.FName!=""){
				retVal+=", "+Referrals.Cur.FName+" "+Referrals.Cur.MName;
			}
			return retVal;
		}





	}

	

	


}













