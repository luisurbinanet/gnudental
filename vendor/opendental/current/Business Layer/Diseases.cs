using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental {

	/// <summary>Corresponds to the Disease table in the database.  Each row is one disease that one patient has.  A disease is a medical condition or allergy.</summary>
	public class Disease:IComparable{
		///<summary>Primary key.</summary>
		public int DiseaseNum;
		///<summary>fk to patient.PatNum</summary>
		public int PatNum;
		///<summary>fk to DiseaseDef.DiseaseDefNum.  The disease description is in that table.</summary>
		public int DiseaseDefNum;
		///<summary>Any note about this disease that is specific to this patient.</summary>
		public string PatNote;

		///<summary>IComparable.CompareTo implementation.  This is used to order disease lists.</summary>
		public int CompareTo(object obj) {
			if(!(obj is Disease)) {
				throw new ArgumentException("object is not a Disease");
			}
			Disease disease=(Disease)obj;
			return DiseaseDefs.GetOrder(DiseaseDefNum).CompareTo(DiseaseDefs.GetOrder(disease.DiseaseDefNum));
		}

		///<summary></summary>
		public Disease Copy() {
			Disease d=new Disease();
			d.DiseaseNum=DiseaseNum;
			d.PatNum=PatNum;
			d.DiseaseDefNum=DiseaseDefNum;
			d.PatNote=PatNote;
			return d;
		}

		///<summary></summary>
		public void Update() {
			string command="UPDATE disease SET " 
				+"PatNum = '"        +POut.PInt   (PatNum)+"'"
				+",DiseaseDefNum = '"+POut.PInt   (DiseaseDefNum)+"'"
				+",PatNote = '"      +POut.PString(PatNote)+"'"
				+" WHERE DiseaseNum  ='"+POut.PInt   (DiseaseNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Insert() {
			if(Prefs.RandomKeys) {
				DiseaseNum=MiscData.GetKey("disease","DiseaseNum");
			}
			string command="INSERT INTO disease (";
			if(Prefs.RandomKeys) {
				command+="DiseaseNum,";
			}
			command+="PatNum,DiseaseDefNum,PatNote) VALUES(";
			if(Prefs.RandomKeys) {
				command+="'"+POut.PInt(DiseaseNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (PatNum)+"', "
				+"'"+POut.PInt   (DiseaseDefNum)+"', "
				+"'"+POut.PString(PatNote)+"')";
			DataConnection dcon=new DataConnection();
			if(Prefs.RandomKeys) {
				dcon.NonQ(command);
			}
			else {
				dcon.NonQ(command,true);
				DiseaseNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void Delete() {
			string command="DELETE FROM disease WHERE DiseaseNum ="+POut.PInt(DiseaseNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

	}

	/*================================================================================================================
	==================================================== class Diseases =============================================*/

	///<summary></summary>
	public class Diseases {
		///<summary>Gets a list of all Diseases for a given patient.  Includes hidden. Sorted by diseasedef.ItemOrder.</summary>
		public static Disease[] Refresh(int patNum) {
			string command="SELECT * FROM disease WHERE PatNum="+POut.PInt(patNum);
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			Disease[] List=new Disease[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new Disease();
				List[i].DiseaseNum   = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum       = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].DiseaseDefNum= PIn.PInt   (table.Rows[i][2].ToString());
				List[i].PatNote      = PIn.PString(table.Rows[i][3].ToString());
			}
			Array.Sort(List);
			return List;
		}

		///<summary>Deletes all diseases for one patient.</summary>
		public static void DeleteAllForPt(int patNum){
			string command="DELETE FROM disease WHERE PatNum ="+POut.PInt(patNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}
		
		
		
	}

		



		
	

	

	


}










