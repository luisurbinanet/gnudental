using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental {

	/// <summary>Corresponds to the DiseaseDef table in the database.</summary>
	public class DiseaseDef:IComparable{
		///<summary>Primary key.</summary>
		public int DiseaseDefNum;
		///<summary></summary>
		public string DiseaseName;
		///<summary>The order that the diseases will show in various lists.</summary>
		public int ItemOrder;
		///<summary></summary>
		public bool IsHidden;

		///<summary>IComparable.CompareTo implementation.  This is used to order disease lists.</summary>
		public int CompareTo(object obj) {
			if(!(obj is DiseaseDef)) {
				throw new ArgumentException("object is not a DiseaseDef");
			}
			DiseaseDef diseaseDef=(DiseaseDef)obj;
			return DiseaseDefs.GetOrder(DiseaseDefNum).CompareTo(DiseaseDefs.GetOrder(diseaseDef.DiseaseDefNum));
		}

		///<summary></summary>
		public DiseaseDef Copy() {
			DiseaseDef d=new DiseaseDef();
			d.DiseaseDefNum=DiseaseDefNum;
			d.DiseaseName=DiseaseName;
			d.ItemOrder=ItemOrder;
			d.IsHidden=IsHidden;
			return d;
		}

		///<summary></summary>
		public void Update() {
			string command="UPDATE diseasedef SET " 
				+"DiseaseName = '" +POut.PString(DiseaseName)+"'"
				+",ItemOrder = '"   +POut.PInt   (ItemOrder)+"'"
				+",IsHidden = '"    +POut.PBool  (IsHidden)+"'"
				+" WHERE DiseaseDefNum  ='"+POut.PInt   (DiseaseDefNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Insert() {
			string command="INSERT INTO diseasedef (DiseaseName,ItemOrder,IsHidden) VALUES("
				+"'"+POut.PString(DiseaseName)+"', "
				+"'"+POut.PInt   (ItemOrder)+"', "
				+"'"+POut.PBool  (IsHidden)+"')";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command,true);
			DiseaseDefNum=dcon.InsertID;
		}

		///<summary>Surround with try/catch, because it will throw an exception if any patient is using this def.</summary>
		public void Delete() {
			string command="SELECT LName,FName FROM patient,disease WHERE "
				+"patient.PatNum=disease.PatNum "
				+"AND disease.DiseaseDefNum='"+POut.PInt(DiseaseDefNum)+"'";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count>0){
				string s=Lan.g("DiseaseDef","Not allowed to delete. Already in use by ")+table.Rows.Count.ToString()
					+" "+Lan.g("DiseaseDef","patients, including")+" \r\n";
				for(int i=0;i<table.Rows.Count;i++){
					if(i>5){
						break;
					}
					s+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString()+"\r\n";
				}
				throw new ApplicationException(s);
			}
			command="DELETE FROM diseasedef WHERE DiseaseDefNum ="+POut.PInt(DiseaseDefNum);
			dcon.NonQ(command);
		}

	}

	/*================================================================================================================
	==================================================== class DiseaseDefs =============================================*/

	///<summary></summary>
	public class DiseaseDefs {
		///<summary>A list of all Diseases.</summary>
		public static DiseaseDef[] ListLong;
		///<summary>The list that is typically used. Does not include hidden diseases.</summary>
		public static DiseaseDef[] List;

		///<summary>Gets a list of all DiseaseDefs when program first opens.</summary>
		public static void Refresh() {
			string command="SELECT * FROM diseasedef ORDER BY ItemOrder";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			ListLong=new DiseaseDef[table.Rows.Count];
			ArrayList AL=new ArrayList();
			for(int i=0;i<table.Rows.Count;i++) {
				ListLong[i]=new DiseaseDef();
				ListLong[i].DiseaseDefNum= PIn.PInt   (table.Rows[i][0].ToString());
				ListLong[i].DiseaseName  = PIn.PString(table.Rows[i][1].ToString());
				ListLong[i].ItemOrder    = PIn.PInt   (table.Rows[i][2].ToString());
				ListLong[i].IsHidden     = PIn.PBool  (table.Rows[i][3].ToString());
				if(!ListLong[i].IsHidden){
					AL.Add(ListLong[i]);
				}
			}
			List=new DiseaseDef[AL.Count];
			AL.CopyTo(List);
		}

		///<summary>Moves the selected item up in the listLong.</summary>
		public static void MoveUp(int selected){
			if(selected<0) {
				throw new ApplicationException(Lan.g("DiseaseDefs","Please select an item first."));
			}
			if(selected==0) {//already at top
				return;
			}
			if(selected>ListLong.Length-1){
				throw new ApplicationException(Lan.g("DiseaseDefs","Invalid selection."));
			}
			SetOrder(selected-1,ListLong[selected].ItemOrder);
			SetOrder(selected,ListLong[selected].ItemOrder-1);
			//Selected-=1;
		}

		///<summary></summary>
		public static void MoveDown(int selected) {
			if(selected<0) {
				throw new ApplicationException(Lan.g("DiseaseDefs","Please select an item first."));
			}
			if(selected==ListLong.Length-1){//already at bottom
				return;
			}
			if(selected>ListLong.Length-1) {
				throw new ApplicationException(Lan.g("DiseaseDefs","Invalid selection."));
			}
			SetOrder(selected+1,ListLong[selected].ItemOrder);
			SetOrder(selected,ListLong[selected].ItemOrder+1);
			//selected+=1;
		}

		///<summary>Used by MoveUp and MoveDown.</summary>
		private static void SetOrder(int mySelNum,int myItemOrder) {
			DiseaseDef temp=ListLong[mySelNum];
			temp.ItemOrder=myItemOrder;
			temp.Update();
		}

		///<summary>Returns the order in ListLong, whether hidden or not.</summary>
		public static int GetOrder(int diseaseDefNum){
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].DiseaseDefNum==diseaseDefNum){
					return ListLong[i].ItemOrder;
				}
			}
			return 0;
		}

		///<summary>Returns the name of the disease, whether hidden or not.</summary>
		public static string GetName(int diseaseDefNum) {
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].DiseaseDefNum==diseaseDefNum) {
					return ListLong[i].DiseaseName;
				}
			}
			return "";
		}

		///<summary>Returns the diseaseDef with the specified num.</summary>
		public static DiseaseDef GetItem(int diseaseDefNum) {
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].DiseaseDefNum==diseaseDefNum) {
					return ListLong[i].Copy();
				}
			}
			return null;
		}
		
		
	}

		



		
	

	

	


}










