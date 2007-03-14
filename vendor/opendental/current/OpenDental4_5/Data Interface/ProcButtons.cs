using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>The 'buttons' to show in the Chart module.  They must have items attached in order to do anything.</summary>
	public class ProcButton{
		///<summary>Primary key</summary>
		public int ProcButtonNum;
		///<summary>The text to show on the button.</summary>
		public string Description;
		///<summary>Order that they will show in the Chart module.</summary>
		public int ItemOrder;
		///<summary>FK to definition.DefNum.</summary>
		public int Category;
		///<summary>If no image, then the blob will be an empty string.  In this case, the bitmap will be null when loaded from the database.</summary>
		public Bitmap ButtonImage;

		///<summary></summary>
		public ProcButton Copy() {
			ProcButton p=new ProcButton();
			p.ProcButtonNum=ProcButtonNum;
			p.Description=Description;
			p.ItemOrder=ItemOrder;
			p.Category=Category;
			p.ButtonImage=ButtonImage;
			return p;
		}

		///<summary>must have already checked ADACode for nonduplicate.</summary>
		public void Insert() {
			string command= "INSERT INTO procbutton (Description,ItemOrder,Category,ButtonImage) VALUES("
				+"'"+POut.PString(Description)+"', "
				+"'"+POut.PInt   (ItemOrder)+"', "
				+"'"+POut.PInt   (Category)+"', "
				+"'"+POut.PBitmap(ButtonImage)+"')";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command,true);
			ProcButtonNum=dcon.InsertID;
		}

		///<summary></summary>
		public void Update() {
			//string test=POut.PBitmap(ButtonImage);
			string command="UPDATE procbutton SET " 
				+ "Description = '" +POut.PString(Description)+"'"
				+ ",ItemOrder = '"  +POut.PInt   (ItemOrder)+"'"
				+ ",Category = '"   +POut.PInt   (Category)+"'"
				+ ",ButtonImage = '"+POut.PBitmap(ButtonImage)+"'"
				+" WHERE ProcButtonNum = '"+POut.PInt(ProcButtonNum)+"'";
			//MessageBox.Show(command);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Delete() {
			string command="DELETE FROM procbuttonitem WHERE ProcButtonNum = '"
				+POut.PInt(ProcButtonNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
			command="DELETE FROM procbutton WHERE ProcButtonNum = '"
				+POut.PInt(ProcButtonNum)+"'";
			dcon.NonQ(command);
		}


	}

	/*=========================================================================================
		=================================== class ProcButtons===========================================*/

	///<summary></summary>
	public class ProcButtons{
		///<summary></summary>
		public static ProcButton[] List;

		///<summary></summary>
		public static void Refresh(){
			string command="SELECT * FROM procbutton ORDER BY ItemOrder";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			List=new ProcButton[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				List[i]=new ProcButton();
				List[i].ProcButtonNum=PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description  =PIn.PString(table.Rows[i][1].ToString());
				List[i].ItemOrder    =PIn.PInt   (table.Rows[i][2].ToString());
				List[i].Category     =PIn.PInt   (table.Rows[i][3].ToString());
				List[i].ButtonImage  =PIn.PBitmap(table.Rows[i][4].ToString());
			}
		}

		///<summary></summary>
		public static ProcButton[] GetForCat(int selectedCat){
			ArrayList AL=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].Category==selectedCat){
					AL.Add(List[i]);
				}
			}
			ProcButton[] retVal=new ProcButton[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		/*//<summary>Used when a button is moved out of a category.  This leaves a 'hole' in the order, so we need to clean up the orders.  Remember to run Refresh before this.</summary>
		public static void ResetOrder(int cat){

		}*/

		
	}

	

	


}










