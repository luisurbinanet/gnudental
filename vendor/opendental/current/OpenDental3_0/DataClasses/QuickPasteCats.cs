using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the quickpastecat table in the database.</summary>
	public class QuickPasteCat{
		///<summary>Primary key.</summary>
		public int QuickPasteCatNum;
		///<summary>Foreign key. Keeps track of which category this note is in.</summary>
		public string Description;
		///<summary>The order of this note within it's category. 0-based.</summary>
		public int ItemOrder;
		///<summary>Each Category can be set to be the default category for multiple types of notes. Stored as integers separated by commas.</summary>
		public string DefaultForTypes;

		///<summary></summary>
		public void Insert(){
			string command="INSERT INTO quickpastecat (Description,ItemOrder,DefaultForTypes) "
				+"VALUES ("
				+"'"+POut.PString(Description)+"', "
				+"'"+POut.PInt   (ItemOrder)+"', "
				+"'"+POut.PString(DefaultForTypes)+"')";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			QuickPasteCatNum=dcon.InsertID;
		}

		///<summary></summary>
		public void Update(){
			string command="UPDATE quickpastecat SET "
				+"Description='"       +POut.PString(Description)+"'"
				+",ItemOrder = '"      +POut.PInt   (ItemOrder)+"'"
				+",DefaultForTypes = '"+POut.PString(DefaultForTypes)+"'"
				+" WHERE QuickPasteCatNum = '"+POut.PInt (QuickPasteCatNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Delete(){
			string command="DELETE from quickpastecat WHERE QuickPasteCatNum = '"
				+POut.PInt(QuickPasteCatNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}




	}	

	/*=========================================================================================
	=================================== class QuickPasteCats======================================*/

	///<summary></summary>
	public class QuickPasteCats{
		///<summary></summary>
		public static QuickPasteCat[] List;

		///<summary></summary>
		public static void Refresh(){
			string command=
				"SELECT * from quickpastecat "
				+"ORDER BY ItemOrder";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			List=new QuickPasteCat[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new QuickPasteCat();
				List[i].QuickPasteCatNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description     = PIn.PString(table.Rows[i][1].ToString());
				List[i].ItemOrder       = PIn.PInt   (table.Rows[i][2].ToString());	
				List[i].DefaultForTypes = PIn.PString(table.Rows[i][3].ToString());
			}
		}

		///<summary>Called from FormQuickPaste and from QuickPasteNotes.Substitute(). Returns the index of the default category for the specified type. If user has entered more than one, only one is returned.</summary>
		public static int GetDefaultType(QuickPasteType type){
			if(List.Length==0){
				return -1;
			}
			if(type==QuickPasteType.None){
				return 0;//default to first line
			}
			string[] types;
			for(int i=0;i<List.Length;i++){
				if(List[i].DefaultForTypes==""){
					types=new string[0];
				}
				else{
					types=List[i].DefaultForTypes.Split(',');
				}
				for(int j=0;j<types.Length;j++){
					if(((int)type).ToString()==types[j]){
						return i;
					}
				}
			}
			return 0;
		}

		

		


		


	}

	


}









