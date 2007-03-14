using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the apptview table in the database. Enables viewing a variety of operatories or providers.</summary>
	public struct ApptView{
		///<summary>Primary key.</summary>
		public int ApptViewNum;
		///<summary>Description of this view.  Gets displayed in Appt module.</summary>
		public string Description;
		///<summary>Order to display in lists. Every view must have a unique itemorder, but it is acceptable to have some missing itemorders in the sequence.</summary>
		public int ItemOrder;
		///<summary>Number of rows per time increment.  Value updated to ContrApptSheet.RowsPerIncr to track current state.</summary>
		public int RowsPerIncr;
	}
	
	
	/*=========================================================================================
	=================================== class ApptViews ===========================================*/
	///<summary>Handles database commands related to the apptview table in the database.</summary>
	public class ApptViews:DataClass{
		///<summary>Current.  A single row of data.</summary>
		public static ApptView Cur;
		///<summary>A list of all apptviews, in order.</summary>
		public static ApptView[] List;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from apptview ORDER BY itemorder";
			FillTable();
			List=new ApptView[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i].ApptViewNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description = PIn.PString(table.Rows[i][1].ToString());
				List[i].ItemOrder   = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].RowsPerIncr = PIn.PInt   (table.Rows[i][3].ToString());	
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO apptview (Description,ItemOrder,RowsPerIncr) "
				+"VALUES ("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PInt   (Cur.ItemOrder)+"', "
				+"'"+POut.PInt   (Cur.RowsPerIncr)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.ApptViewNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE apptview SET "
				+"Description='"   +POut.PString(Cur.Description)+"'"
				+",ItemOrder = '"  +POut.PInt   (Cur.ItemOrder)+"'"
				+",RowsPerIncr = '"+POut.PInt   (Cur.RowsPerIncr)+"'"
				+" WHERE ApptViewNum = '"+POut.PInt(Cur.ApptViewNum)+"'";
			NonQ();
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText="DELETE from apptview WHERE ApptViewNum = '"
				+POut.PInt(Cur.ApptViewNum)+"'";
			NonQ();
		}

		/// <summary>Used in appt module.  Can be -1 if no category selected </summary>
		public static void SetCur(int index){
			if(index==-1){
				Cur=new ApptView();
			}
			else{
				Cur=List[index];
			}
		}


	}

	


}









