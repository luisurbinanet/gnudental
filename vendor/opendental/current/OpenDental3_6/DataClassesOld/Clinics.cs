
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the clinic table in the database.</summary>
	public class Clinic{
		///<summary>Primary key.</summary>
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
		///<summary>Includes any punctuation.</summary>
		public string Phone;

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
			return c;
		}

		///<summary></summary>
		public void Insert(){
			string command= "INSERT INTO clinic (Description,Address,Address2,City,State,Zip,Phone"
				+") VALUES("
				+"'"+POut.PString(Description)+"', "
				+"'"+POut.PString(Address)+"', "
				+"'"+POut.PString(Address2)+"', "
				+"'"+POut.PString(City)+"', "
				+"'"+POut.PString(State)+"', "
				+"'"+POut.PString(Zip)+"', "
				+"'"+POut.PString(Phone)+"')";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			ClinicNum=dcon.InsertID;
		}

		///<summary></summary>
		public void Update(){
			string command= "UPDATE clinic SET " 
				+ "Description = '"+POut.PString(Description)+"'"
				+ ",Address = '"    +POut.PString(Address)+"'"
				+ ",Address2 = '"   +POut.PString(Address2)+"'"
				+ ",City = '"       +POut.PString(City)+"'"
				+ ",State = '"      +POut.PString(State)+"'"
				+ ",Zip = '"        +POut.PString(Zip)+"'"
				+ ",Phone = '"      +POut.PString(Phone)+"'"
				+" WHERE ClinicNum = '" +POut.PInt(ClinicNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary>Checks dependencies first.  Returns true if deleted, or false if not.</summary>
		public bool Delete(){
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
				MessageBox.Show(
					Lan.g("Clinics","Cannot delete clinic because it is in use by the following patients:")
					+pats);
				return false;
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
				MessageBox.Show(
					Lan.g("Clinics","Cannot delete clinic because the following patients have payments using it:")
					+pats);
				return false;
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
				MessageBox.Show(
					Lan.g("Clinics","Cannot delete clinic because the following patients have claim payments using it:")
					+pats);
				return false;
			}
			//delete
			command= "DELETE FROM clinic" 
				+" WHERE ClinicNum = "+POut.PInt(ClinicNum);
			//MessageBox.Show(cmd.CommandText);
 			dcon.NonQ(command);
			return true;
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
				List[i].ClinicNum   = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description = PIn.PString(table.Rows[i][1].ToString());
			}
		}
	
	}
	


}













