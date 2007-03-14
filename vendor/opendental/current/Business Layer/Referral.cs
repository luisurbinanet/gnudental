using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>All info about a referral is stored with that referral even if a patient.  That way, it's available for easy queries.</summary>
	public class Referral{
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
		///<summary>Enum:DentalSpecialty</summary>
		public DentalSpecialty Specialty;
		///<summary>State</summary>
		public string ST;
		///<summary>Primary phone, restrictive, must only be 10 digits and only numbers.</summary>
		public string Telephone;
		///<summary>.</summary>
		public string Address;
		///<summary>.</summary>
		public string Address2;
		///<summary>.</summary>
		public string City;
		///<summary>.</summary>
		public string Zip;
		///<summary>Holds important info about the referral.</summary>
		public string Note;
		///<summary>Additional phone no restrictions</summary>
		public string Phone2;
		///<summary>Can't delete a referral, but can hide if not needed any more.</summary>
		public bool IsHidden;
		///<summary>Set to true for referralls such as Yellow Pages.</summary>
		public bool NotPerson;
		///<summary>i.e. DMD or DDS</summary>
		public string Title;
		///<summary>.</summary>
		public string EMail;
		///<summary>FK to patient.PatNum for referrals that are patients.</summary>
		public int PatNum;

		///<summary>Returns a copy of this Referral.</summary>
		public Referral Copy(){
			Referral r=new Referral();
			r.ReferralNum=ReferralNum;
			r.LName=LName;
			r.FName=FName;
			r.MName=MName;
			r.SSN=SSN;
			r.UsingTIN=UsingTIN;
			r.Specialty=Specialty;
			r.ST=ST;
			r.Telephone=Telephone;
			r.Address=Address;
			r.Address2=Address2;
			r.City=City;
			r.Zip=Zip;
			r.Note=Note;
			r.Phone2=Phone2;
			r.IsHidden=IsHidden;
			r.NotPerson=NotPerson;
			r.Title=Title;
			r.EMail=EMail;
			r.PatNum=PatNum;
			return r;
		}
		
		///<summary></summary>
		public void Update(){
			string command = "UPDATE referral SET " 
				+ "LName = '"      +POut.PString(LName)+"'"
				+ ",FName = '"     +POut.PString(FName)+"'"
				+ ",MName = '"     +POut.PString(MName)+"'"
				+ ",SSN = '"       +POut.PString(SSN)+"'"
				+ ",UsingTIN = '"  +POut.PBool  (UsingTIN)+"'"
				+ ",Specialty = '" +POut.PInt   ((int)Specialty)+"'"
				+ ",ST = '"        +POut.PString(ST)+"'"
				+ ",Telephone = '" +POut.PString(Telephone)+"'"
				+ ",Address = '"   +POut.PString(Address)+"'"
				+ ",Address2 = '"  +POut.PString(Address2)+"'"
				+ ",City = '"      +POut.PString(City)+"'"
				+ ",Zip = '"       +POut.PString(Zip)+"'"
				+ ",Note = '"      +POut.PString(Note)+"'"
				+ ",Phone2 = '"    +POut.PString(Phone2)+"'"
				+ ",IsHidden = '"  +POut.PBool  (IsHidden)+"'"
				+ ",NotPerson = '" +POut.PBool  (NotPerson)+"'"
				+ ",Title = '"     +POut.PString(Title)+"'"
				+ ",EMail = '"     +POut.PString(EMail)+"'"
				+ ",PatNum = '"    +POut.PInt  (PatNum)+"'"     
				+" WHERE ReferralNum = '" +POut.PInt (ReferralNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Insert(){
			if(Prefs.RandomKeys){
				ReferralNum=MiscData.GetKey("referral","ReferralNum");
			}
			string command= "INSERT INTO referral (";
			if(Prefs.RandomKeys){
				command+="ReferralNum,";
			}
			command+="LName,FName,MName,SSN,UsingTIN,Specialty,ST,"
				+"Telephone,Address,Address2,City,Zip,Note,Phone2,IsHidden,NotPerson,Title,Email,PatNum) VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(ReferralNum)+"', ";
			}
			command+=
				 "'"+POut.PString(LName)+"', "
				+"'"+POut.PString(FName)+"', "
				+"'"+POut.PString(MName)+"', "
				+"'"+POut.PString(SSN)+"', "
				+"'"+POut.PBool  (UsingTIN)+"', "
				+"'"+POut.PInt   ((int)Specialty)+"', "
				+"'"+POut.PString(ST)+"', "
				+"'"+POut.PString(Telephone)+"', "    
				+"'"+POut.PString(Address)+"', "
				+"'"+POut.PString(Address2)+"', "
				+"'"+POut.PString(City)+"', "
				+"'"+POut.PString(Zip)+"', "
				+"'"+POut.PString(Note)+"', "
				+"'"+POut.PString(Phone2)+"', "
				+"'"+POut.PBool  (IsHidden)+"', "
				+"'"+POut.PBool  (NotPerson)+"', "
				+"'"+POut.PString(Title)+"', "
				+"'"+POut.PString(EMail)+"', "
				+"'"+POut.PInt   (PatNum)+"')";
			DataConnection dcon=new DataConnection();
 			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				ReferralNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void Delete(){
			string command= "DELETE FROM referral "
				+"WHERE referralnum = '"+ReferralNum+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public string GetName(){
			if(ReferralNum==0)
				return "";
			string retVal=LName;
			if(FName!=""){
				retVal+=", "+FName+" "+MName;
			}
			return retVal;
		}

		




	}

	

	


}













