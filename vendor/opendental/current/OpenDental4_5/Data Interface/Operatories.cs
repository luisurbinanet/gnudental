using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Each row is a single operatory or column in the appts module.</summary>
	public class Operatory{
		///<summary>Primary key</summary>
		public int OperatoryNum;
		///<summary>The full name to show in the column.</summary>
		public string OpName;
		///<summary>5 char or less. Not used much.</summary>
		public string Abbrev;
		///<summary>The order that this op column will show.  Changing views only hides some ops; it does not change their order.</summary>
		public int ItemOrder;
		///<summary>Used instead of deleting to hide an op that is no longer used.</summary>
		public bool IsHidden;
		///<summary>FK to provider.ProvNum.  The dentist assigned to this op.  If more than one dentist might be assigned to an op, then create a second op and use one for each dentist. If 0, then no dentist is assigned.</summary>
		public int ProvDentist;
		///<summary>FK to provider.ProvNum.  The hygienist assigned to this op.  If 0, then no hygienist is assigned.</summary>
		public int ProvHygienist;
		///<summary>Set true if this is a hygiene operatory.  The hygienist will then be considered the main provider for this op.</summary>
		public bool IsHygiene;
		///<summary>FK to clinic.ClinicNum.  0 if no clinic.</summary>
		public int ClinicNum;

		///<summary>Returns a copy of this Operatory.</summary>
		public Operatory Copy(){
			Operatory o=new Operatory();
			o.OperatoryNum=OperatoryNum;
			o.OpName=OpName;
			o.Abbrev=Abbrev;
			o.ItemOrder=ItemOrder;
			o.IsHidden=IsHidden;
			o.ProvDentist=ProvDentist;
			o.ProvHygienist=ProvHygienist;
			o.IsHygiene=IsHygiene;
			o.ClinicNum=ClinicNum;
			return o;
		}

		///<summary></summary>
		private void Insert(){
			string command= "INSERT INTO operatory (OpName,Abbrev,ItemOrder,IsHidden,ProvDentist,ProvHygienist,"
				+"IsHygiene,ClinicNum) VALUES("
				+"'"+POut.PString(OpName)+"', "
				+"'"+POut.PString(Abbrev)+"', "
				+"'"+POut.PInt   (ItemOrder)+"', "
				+"'"+POut.PBool  (IsHidden)+"', "
				+"'"+POut.PInt   (ProvDentist)+"', "
				+"'"+POut.PInt   (ProvHygienist)+"', "
				+"'"+POut.PBool  (IsHygiene)+"', "
				+"'"+POut.PInt   (ClinicNum)+"')";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			OperatoryNum=dcon.InsertID;
		}

		///<summary></summary>
		private void Update(){
			string command= "UPDATE operatory SET " 
				+ "OpName = '"        +POut.PString(OpName)+"'"
				+ ",Abbrev = '"       +POut.PString(Abbrev)+"'"
				+ ",ItemOrder = '"    +POut.PInt   (ItemOrder)+"'"
				+ ",IsHidden = '"     +POut.PBool  (IsHidden)+"'"
				+ ",ProvDentist = '"  +POut.PInt   (ProvDentist)+"'"
				+ ",ProvHygienist = '"+POut.PInt   (ProvHygienist)+"'"
				+ ",IsHygiene = '"    +POut.PBool  (IsHygiene)+"'"
				+ ",ClinicNum = '"    +POut.PInt   (ClinicNum)+"'"				
				+" WHERE OperatoryNum = '" +POut.PInt(OperatoryNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public void InsertOrUpdate(bool IsNew){
			//if(){
				//throw new ApplicationException(Lan.g(this,""));
			//}
			if(IsNew){
				Insert();
			}
			else{
				Update();
			}
		}

		//<summary>Checks dependencies first.  Throws exception if can't delete.</summary>
		//public void Delete(){//no such thing as delete.  Hide instead
		//}

    
	}

	/*========================================================================================================================
	=================================== class Operatories ===================================================================*/

	///<summary></summary>
	public class Operatories{
		///<summary></summary>
		public static Operatory[] List;
		///<summary>A list of only those operatories that are visible.</summary>
		public static Operatory[] ListShort;

		///<summary>Refresh all operatories</summary>
		public static void Refresh(){
			string command="SELECT * FROM operatory "
				+"ORDER BY ItemOrder";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			List=new Operatory[table.Rows.Count];
			ArrayList AL=new ArrayList();
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Operatory();
				List[i].OperatoryNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].OpName       = PIn.PString(table.Rows[i][1].ToString());
				List[i].Abbrev       = PIn.PString(table.Rows[i][2].ToString());
				List[i].ItemOrder    = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].IsHidden     = PIn.PBool  (table.Rows[i][4].ToString());
				List[i].ProvDentist  = PIn.PInt   (table.Rows[i][5].ToString());
				List[i].ProvHygienist= PIn.PInt   (table.Rows[i][6].ToString());
				List[i].IsHygiene    = PIn.PBool  (table.Rows[i][7].ToString());
				List[i].ClinicNum    = PIn.PInt   (table.Rows[i][8].ToString());
				if(!List[i].IsHidden){
					AL.Add(List[i]);
				}
			}
			ListShort=new Operatory[AL.Count];
			AL.CopyTo(ListShort);
		}

		///<summary>Gets the order of the op within ListShort or -1 if not found.</summary>
		public static int GetOrder(int opNum){
			for(int i=0;i<ListShort.Length;i++){
				if(ListShort[i].OperatoryNum==opNum){
					return i;
				}
			}
			return -1;
		}

		///<summary>Gets the abbreviation of an op.</summary>
		public static string GetAbbrev(int opNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].OperatoryNum==opNum){
					return List[i].Abbrev;
				}
			}
			return "";
		}

		///<summary></summary>
		public static Operatory GetOperatory(int operatoryNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].OperatoryNum==operatoryNum){
					return List[i].Copy();
				}
			}
			return null;
		}
	
	}
	


}













