using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental {

	/// <summary>Corresponds to the patfield table in the database.  These are custom fields added and managed by the user.</summary>
	public class PatField{
		///<summary>Primary key.</summary>
		public int PatFieldNum;
		///<summary>fk to patient.PatNum</summary>
		public int PatNum;
		///<summary>fk to patfielddef.FieldName.  The full name is shown here for ease of use when running queries.  But the user is only allowed to change fieldNames in the patFieldDef setup window.</summary>
		public string FieldName;
		///<summary>Any text that the user types in.</summary>
		public string FieldValue;

		///<summary></summary>
		public PatField Copy() {
			PatField p=new PatField();
			p.PatFieldNum=PatFieldNum;
			p.PatNum=PatNum;
			p.FieldName=FieldName;
			p.FieldValue=FieldValue;
			return p;
		}

		///<summary></summary>
		public void Update() {
			string command="UPDATE patfield SET " 
				+"PatNum = '"            +POut.PInt   (PatNum)+"'"
				+",FieldName = '"        +POut.PString(FieldName)+"'"
				+",FieldValue = '"       +POut.PString(FieldValue)+"'"
				+" WHERE PatFieldNum  ='"+POut.PInt   (PatFieldNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Insert() {
			if(Prefs.RandomKeys) {
				PatFieldNum=MiscData.GetKey("patfield","PatFieldNum");
			}
			string command="INSERT INTO patfield (";
			if(Prefs.RandomKeys) {
				command+="PatFieldNum,";
			}
			command+="PatNum,FieldName,FieldValue) VALUES(";
			if(Prefs.RandomKeys) {
				command+="'"+POut.PInt(PatFieldNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (PatNum)+"', "
				+"'"+POut.PString(FieldName)+"', "
				+"'"+POut.PString(FieldValue)+"')";
			DataConnection dcon=new DataConnection();
			if(Prefs.RandomKeys) {
				dcon.NonQ(command);
			}
			else {
				dcon.NonQ(command,true);
				PatFieldNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void Delete() {
			string command="DELETE FROM patfield WHERE PatFieldNum ="+POut.PInt(PatFieldNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

	}

	/*================================================================================================================
	==================================================== class PatFields =============================================*/

	///<summary></summary>
	public class PatFields {
		///<summary>Gets a list of all PatFields for a given patient.</summary>
		public static PatField[] Refresh(int patNum) {
			string command="SELECT * FROM patfield WHERE PatNum="+POut.PInt(patNum);
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			PatField[] List=new PatField[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new PatField();
				List[i].PatFieldNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum     = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].FieldName  = PIn.PString(table.Rows[i][2].ToString());
				List[i].FieldValue = PIn.PString(table.Rows[i][3].ToString());
			}
			return List;
		}

		///<summary>Frequently returns null.</summary>
		public static PatField GetByName(string name,PatField[] fieldList){
			for(int i=0;i<fieldList.Length;i++){
				if(fieldList[i].FieldName==name){
					return fieldList[i];
				}
			}
			return null;
		}
		
		
	}

		



		
	

	

	


}










