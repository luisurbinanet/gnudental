
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the clinic table in the database.</summary>
	public class Clinic{
		///<summary>Primary key.  Used in patient,payment,claimpayment,appointment,procedurelog</summary>
		public int ClinicNum;
		///<summary></summary>
		public string Description;
		///<summary></summary>
		public string Address;
		///<summary>Second line of address.</summary>
		public string Address2;
		///<summary></summary>
		public string City;
		///<summary>2 char in the US.</summary>
		public string State;
		///<summary></summary>
		public string Zip;
		///<summary>Does not include any punctuation.  Should be exactly 10 digits</summary>
		public string Phone;
		///<summary>The account number for deposits.</summary>
		public string BankNumber;
		///<summary>Usually 0 unless a mobile clinic for instance.</summary>
		public PlaceOfService DefaultPlaceService;

		///<summary>Returns a copy of this Clinic.</summary>
		public Clinic Copy(){
			Clinic c=new Clinic();
			c.ClinicNum=ClinicNum;
			c.Description=Description;
			c.Address=Address;
			c.Address2=Address2;
			c.City=City;
			c.State=State;
			c.Zip=Zip;
			c.Phone=Phone;
			c.BankNumber=BankNumber;
			c.DefaultPlaceService=DefaultPlaceService;
			return c;
		}

		///<summary></summary>
		private void Insert(){
			string command= "INSERT INTO clinic (Description,Address,Address2,City,State,Zip,Phone,"
				+"BankNumber,DefaultPlaceService) VALUES("
				+"'"+POut.PString(Description)+"', "
				+"'"+POut.PString(Address)+"', "
				+"'"+POut.PString(Address2)+"', "
				+"'"+POut.PString(City)+"', "
				+"'"+POut.PString(State)+"', "
				+"'"+POut.PString(Zip)+"', "
				+"'"+POut.PString(Phone)+"', "
				+"'"+POut.PString(BankNumber)+"', "
				+"'"+POut.PInt   ((int)DefaultPlaceService)+"')";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			ClinicNum=dcon.InsertID;
		}

		///<summary></summary>
		private void Update(){
			string command= "UPDATE clinic SET " 
				+ "Description = '"       +POut.PString(Description)+"'"
				+ ",Address = '"          +POut.PString(Address)+"'"
				+ ",Address2 = '"         +POut.PString(Address2)+"'"
				+ ",City = '"             +POut.PString(City)+"'"
				+ ",State = '"            +POut.PString(State)+"'"
				+ ",Zip = '"              +POut.PString(Zip)+"'"
				+ ",Phone = '"            +POut.PString(Phone)+"'"
				+ ",BankNumber = '"       +POut.PString(BankNumber)+"'"
				+ ",DefaultPlaceService='"+POut.PInt   ((int)DefaultPlaceService)+"'"
				+" WHERE ClinicNum = '" +POut.PInt(ClinicNum)+"'";
			//MessageBox.Show(cmd.CommandText);
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

		///<summary>Checks dependencies first.  Throws exception if can't delete.</summary>
		public void Delete(){
			//check patients for dependencies
			string command="SELECT LName,FName FROM patient WHERE ClinicNum ="
				+POut.PInt(ClinicNum);
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count>0){
				string pats="";
				for(int i=0;i<table.Rows.Count;i++){
					pats+="\r";
					pats+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString();
				}
				throw new Exception(Lan.g("Clinics","Cannot delete clinic because it is in use by the following patients:")+pats);
			}
			//check payments for dependencies
			command="SELECT patient.LName,patient.FName FROM patient,payment "
				+"WHERE payment.ClinicNum ="+POut.PInt(ClinicNum)
				+" AND patient.PatNum=payment.PatNum";
			table=dcon.GetTable(command);
			if(table.Rows.Count>0){
				string pats="";
				for(int i=0;i<table.Rows.Count;i++){
					pats+="\r";
					pats+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString();
				}
				throw new Exception(Lan.g("Clinics","Cannot delete clinic because the following patients have payments using it:")+pats);
			}
			//check claimpayments for dependencies
			command="SELECT patient.LName,patient.FName FROM patient,claimproc,claimpayment "
				+"WHERE claimpayment.ClinicNum ="+POut.PInt(ClinicNum)
				+" AND patient.PatNum=claimproc.PatNum"
				+" AND claimproc.ClaimPaymentNum=claimpayment.ClaimPaymentNum "
				+"GROUP BY claimpayment.ClaimPaymentNum";
			table=dcon.GetTable(command);
			if(table.Rows.Count>0){
				string pats="";
				for(int i=0;i<table.Rows.Count;i++){
					pats+="\r";
					pats+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString();
				}
				throw new Exception(Lan.g("Clinics","Cannot delete clinic because the following patients have claim payments using it:")+pats);
			}
			//check appointments for dependencies
			command="SELECT patient.LName,patient.FName FROM patient,appointment "
				+"WHERE appointment.ClinicNum ="+POut.PInt(ClinicNum)
				+" AND patient.PatNum=appointment.PatNum";
			table=dcon.GetTable(command);
			if(table.Rows.Count>0){
				string pats="";
				for(int i=0;i<table.Rows.Count;i++){
					pats+="\r";
					pats+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString();
				}
				throw new Exception(Lan.g("Clinics","Cannot delete clinic because the following patients have appointments using it:")+pats);
			}
			//check procedures for dependencies
			command="SELECT patient.LName,patient.FName FROM patient,procedurelog "
				+"WHERE procedurelog.ClinicNum ="+POut.PInt(ClinicNum)
				+" AND patient.PatNum=procedurelog.PatNum";
			table=dcon.GetTable(command);
			if(table.Rows.Count>0){
				string pats="";
				for(int i=0;i<table.Rows.Count;i++){
					pats+="\r";
					pats+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString();
				}
				throw new Exception(Lan.g("Clinics","Cannot delete clinic because the following patients have procedures using it:")+pats);
			}
			//check operatories for dependencies
			command="SELECT OpName FROM operatory "
				+"WHERE ClinicNum ="+POut.PInt(ClinicNum);
			table=dcon.GetTable(command);
			if(table.Rows.Count>0){
				string ops="";
				for(int i=0;i<table.Rows.Count;i++){
					ops+="\r";
					ops+=table.Rows[i][0].ToString();
				}
				throw new Exception(Lan.g("Clinics","Cannot delete clinic because the following operatories are using it:")+ops);
			}
			//delete
			command= "DELETE FROM clinic" 
				+" WHERE ClinicNum = "+POut.PInt(ClinicNum);
			//MessageBox.Show(cmd.CommandText);
 			dcon.NonQ(command);
		}


    
	}


	///<summary></summary>
	public class Clinics{
		///<summary></summary>
		public static Clinic[] List;

		///<summary>Refresh all clinics</summary>
		public static void Refresh(){
			string command="SELECT * FROM clinic";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			List=new Clinic[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Clinic();
				List[i].ClinicNum       = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description     = PIn.PString(table.Rows[i][1].ToString());
				List[i].Address         = PIn.PString(table.Rows[i][2].ToString());
				List[i].Address2        = PIn.PString(table.Rows[i][3].ToString());
				List[i].City            = PIn.PString(table.Rows[i][4].ToString());
				List[i].State           = PIn.PString(table.Rows[i][5].ToString());
				List[i].Zip             = PIn.PString(table.Rows[i][6].ToString());
				List[i].Phone           = PIn.PString(table.Rows[i][7].ToString());
				List[i].BankNumber      = PIn.PString(table.Rows[i][8].ToString());
				List[i].DefaultPlaceService=(PlaceOfService)PIn.PInt(table.Rows[i][9].ToString());
			}
		}

		///<summary></summary>
		public static Clinic GetClinic(int clinicNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].ClinicNum==clinicNum){
					return List[i].Copy();
				}
			}
			return null;
		}

		///<summary>Returns an empty string for invalid clinicNums.</summary>
		public static string GetDesc(int clinicNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].ClinicNum==clinicNum){
					return List[i].Description;
				}
			}
			return "";
		}
	
		///<summary>Returns office for invalid clinicNums.</summary>
		public static PlaceOfService GetPlaceService(int clinicNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].ClinicNum==clinicNum){
					return List[i].DefaultPlaceService;
				}
			}
			return PlaceOfService.Office;
		}

	}
	


}













