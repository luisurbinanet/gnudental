using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental {

	/// <summary>These are the definitions for the custom patient fields added and managed by the user.</summary>
	public class PatFieldDef{
		///<summary>Primary key.</summary>
		public int PatFieldDefNum;
		///<summary>The name of the field that the user will be allowed to fill in the patient info window.</summary>
		public string FieldName;

		///<summary></summary>
		public PatFieldDef Copy() {
			PatFieldDef p=new PatFieldDef();
			p.PatFieldDefNum=PatFieldDefNum;
			p.FieldName=FieldName;
			return p;
		}

		///<summary>Must supply the old field name so that the patient lists can be updated.</summary>
		public void Update(string oldFieldName) {
			string command="UPDATE patfielddef SET " 
				+"FieldName = '"        +POut.PString(FieldName)+"'"
				+" WHERE PatFieldDefNum  ='"+POut.PInt   (PatFieldDefNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
			command="UPDATE patfield SET FieldName='"+POut.PString(FieldName)+"'"
				+" WHERE FieldName='"+POut.PString(oldFieldName)+"'";
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Insert() {
			string command="INSERT INTO patfielddef (FieldName) VALUES("
				+"'"+POut.PString(FieldName)+"')";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command,true);
			PatFieldDefNum=dcon.InsertID;
		}

		///<summary>Surround with try/catch, because it will throw an exception if any patient is using this def.</summary>
		public void Delete() {
			string command="SELECT LName,FName FROM patient,patfield WHERE "
				+"patient.PatNum=patfield.PatNum "
				+"AND FieldName='"+POut.PString(FieldName)+"'";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count>0){
				string s=Lan.g("PatFieldDef","Not allowed to delete. Already in use by ")+table.Rows.Count.ToString()
					+" "+Lan.g("PatFieldDef","patients, including")+" \r\n";
				for(int i=0;i<table.Rows.Count;i++){
					if(i>5){
						break;
					}
					s+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString()+"\r\n";
				}
				throw new ApplicationException(s);
			}
			command="DELETE FROM patfielddef WHERE PatFieldDefNum ="+POut.PInt(PatFieldDefNum);
			dcon.NonQ(command);
		}

	}

	/*================================================================================================================
	==================================================== class PatFieldDefs =============================================*/

	///<summary></summary>
	public class PatFieldDefs {
		///<summary>A list of all allowable patFields.</summary>
		public static PatFieldDef[] List;

		///<summary>Gets a list of all PatFieldDefs when program first opens.</summary>
		public static void Refresh() {
			string command="SELECT * FROM patfielddef";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			List=new PatFieldDef[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new PatFieldDef();
				List[i].PatFieldDefNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].FieldName     = PIn.PString(table.Rows[i][1].ToString());
			}
		}
		
		
	}

		



		
	

	

	


}










