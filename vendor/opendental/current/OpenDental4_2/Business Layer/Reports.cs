/*using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the report table in the database.</summary>
	public class Report{
		///<summary>Primary key</summary>
		public int ReportNum;
		///<summary>The name of the report as displayed in the menu.</summary>
		public string Description;
		///<summary>Foreign key to definition.DefNum.  It will be grouped in the menu under this category.</summary>
		public int Category;
		///<summary>The order that this report shows within its category.</summary>
		public int ItemOrder;
		///<summary>The name of the .rdl file where the report definition is stored.  Does not include any path info.  e.g. MyReport.rdl.</summary>
		public string FileName;
		///<summary>This field contains a copy of the report for repair purposes, just in case the user loses the text file or alters it accidentally.  This field is filled when the report is first attached to the database, and is never altered after that.  The only way to change it would be to delete the report from the database, and then reattach to an rdl file.  If the user deletes both the database entry and the file, they can redownload from our website.</summary>
		public string RepairCopy;
		

		///<summary>Returns a copy of this report.</summary>
		public Report Copy(){
			Report r=new Report();
			r.ReportNum=ReportNum;
			r.Description=Description;
			r.Category=Category;
			r.ItemOrder=ItemOrder;
			r.FileName=FileName;
			r.RepairCopy=RepairCopy;
			return r;
		}

		///<summary></summary>
		public void Insert(){
			string command="INSERT INTO report (Description,Category,ItemOrder,FileName,RepairCopy"
				+") VALUES("
				+"'"+POut.PString(Description)+"', "
				+"'"+POut.PInt   (Category)+"', "
				+"'"+POut.PInt   (ItemOrder)+"', "
				+"'"+POut.PString(FileName)+"', "
				+"'"+POut.PString(RepairCopy)+"')";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			ReportNum=dcon.InsertID;
		}

		///<summary></summary>
		public void Update(){
			string command="UPDATE report SET " 
				+ "Description = '"     +POut.PString(Description)+"'"
				+ ",Category = '"       +POut.PInt   (Category)+"'"
				+ ",ItemOrder = '"      +POut.PInt   (ItemOrder)+"'"
				+ ",FileName = '"				+POut.PString(FileName)+"'"
				+ ",RepairCopy = '"     +POut.PString(RepairCopy)+"'"		
				+" WHERE ReportNum = '" +POut.PInt   (reportNum)+"'";
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

    
	}*/

	/*========================================================================================================================
	=================================== class Reports ===================================================================*/

	/*
	///<summary></summary>
	public class Reports{
		///<summary>A list of all reports in the database.</summary>
		public static Report[] List;

		///<summary>Refresh all Reports</summary>
		public static void Refresh(){
			string command="SELECT * FROM report "
				+"ORDER BY ItemOrder";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			List=new Report[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new report();
				List[i].reportNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].OpName       = PIn.PString(table.Rows[i][1].ToString());
				List[i].Abbrev       = PIn.PString(table.Rows[i][2].ToString());
				List[i].ItemOrder    = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].IsHidden     = PIn.PBool  (table.Rows[i][4].ToString());
				List[i].ProvDentist  = PIn.PInt   (table.Rows[i][5].ToString());
				List[i].ProvHygienist= PIn.PInt   (table.Rows[i][6].ToString());
				List[i].IsHygiene    = PIn.PBool  (table.Rows[i][7].ToString());
				List[i].ClinicNum    = PIn.PInt   (table.Rows[i][8].ToString());
			}
			return List;
		}

		///<summary>Gets the order of the op within ListShort or -1 if not found.</summary>
		public static int GetOrder(int opNum){
			for(int i=0;i<ListShort.Length;i++){
				if(ListShort[i].reportNum==opNum){
					return i;
				}
			}
			return -1;
		}

		///<summary>Gets the abbreviation of an op.</summary>
		public static string GetAbbrev(int opNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].reportNum==opNum){
					return List[i].Abbrev;
				}
			}
			return "";
		}

		///<summary></summary>
		public static report Getreport(int reportNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].reportNum==reportNum){
					return List[i].Copy();
				}
			}
			return null;
		}
	
	}
	


}
*/












