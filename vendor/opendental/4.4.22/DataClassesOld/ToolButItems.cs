using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Each row represents one toolbar button to be placed on a toolbar and linked to a program.</summary>
	public struct ToolButItem{
		///<summary>Primary key.</summary>
		public int ToolButItemNum;
		///<summary>FK to program.ProgramNum.</summary>
		public int ProgramNum;
		///<summary>Enum:ToolBarsAvail The toolbar to show the button on.</summary>
		public ToolBarsAvail ToolBar;
		///<summary>The text to show on the toolbar button.</summary>
		public string ButtonText;
		//later include ComputerName.  If blank, then show on all computers.
		//also later, include an image.
	}

	/*=========================================================================================
		=================================== class ToolButItems ===========================================*/
  ///<summary></summary>
	public class ToolButItems:DataClass{
		///<summary></summary>
		public static ToolButItem Cur;
		///<summary></summary>
		public static ToolButItem[] List;
		///<summary></summary>
		public static ArrayList ForProgram;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from toolbutitem";
			FillTable();
			List=new ToolButItem[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i].ToolButItemNum  =PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ProgramNum      =PIn.PInt   (table.Rows[i][1].ToString());
				List[i].ToolBar         =(ToolBarsAvail)PIn.PInt(table.Rows[i][2].ToString());
				List[i].ButtonText      =PIn.PString(table.Rows[i][3].ToString());
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO toolbutitem (ProgramNum,ToolBar,ButtonText) "
				+"VALUES ("
				+"'"+POut.PInt   (Cur.ProgramNum)+"', "
				+"'"+POut.PInt   ((int)Cur.ToolBar)+"', "
				+"'"+POut.PString(Cur.ButtonText)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ();
			//Cur.=InsertID;
		}

		///<summary>This in not currently being used.</summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE toolbutitem SET "
				+"ProgramNum ='" +POut.PInt   (Cur.ProgramNum)+"'"
				+",ToolBar ='"   +POut.PInt   ((int)Cur.ToolBar)+"'"
				+",ButtonText ='"+POut.PString(Cur.ButtonText)+"'"
				+" WHERE ToolButItemNum = '"+POut.PInt(Cur.ToolButItemNum)+"'";
			NonQ();
		}

		///<summary>This is not currently being used.</summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from toolbutitem WHERE ToolButItemNum = '"
				+POut.PInt(Cur.ToolButItemNum)+"'";
			NonQ();
		}

		///<summary>Deletes all ToolButItems for the Programs.Cur.  This is used regularly when saving a Program link because of the way the user interface works.</summary>
		public static void DeleteAllForProgram(){
			cmd.CommandText = "DELETE from toolbutitem WHERE ProgramNum = '"
				+POut.PInt(Programs.Cur.ProgramNum)+"'";
			NonQ();
		}

		///<summary>Fills ForProgram with toolbutitems attached to the Programs.Cur</summary>
		public static void GetForProgram(){
			ForProgram=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ProgramNum==Programs.Cur.ProgramNum){
					ForProgram.Add(List[i]);
				}
			}
		}

		///<summary>Returns a list of toolbutitems for the specified toolbar. Used when laying out toolbars.</summary>
		public static ArrayList GetForToolBar(ToolBarsAvail toolbar){
			ArrayList retVal=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ToolBar==toolbar && Programs.IsEnabled(List[i].ProgramNum)){
					retVal.Add(List[i]);
				}
			}
			return retVal;
		}


	}

	

}













