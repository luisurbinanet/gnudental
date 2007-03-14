using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the claimformitem table in the database.</summary>
	///<remarks>One item is needed for each field on a claimform.</remarks>
	public struct ClaimFormItem{
		///<summary>Primary key.</summary>
		public int ClaimFormItemNum;
		///<summary>Foreign key to ClaimForm.</summary>
		public int ClaimFormNum;
		///<summary>If this item is an image.  Usually only one per claim.  eg ADA2002.emf</summary>
		public string ImageFileName;
		///<summary>Must be one of the hardcoded available fieldnames for claims.</summary>
		public string FieldName;//
		///<summary>For dates, the format string. ie MM/dd/yyyy or M d y among many other possibilities.</summary>
		public string FormatString;
		///<summary>The x position of the item on the claim form</summary>
		///<remarks>In pixels. 100 pixels per inch.</remarks>
		public float XPos;
		///<summary>The y position.</summary>
		public float YPos;
		///<summary>Limits the printable area of the item. Set to zero to not limit.</summary>
		public float Width;
		///<summary>Limits the printable area of the item. Set to zero to not limit.</summary>
		public float Height;
	}

	/*=========================================================================================
		=================================== class ClaimFormItems ==========================================*/

	///<summary></summary>
	public class ClaimFormItems:DataClass{
		///<summary></summary>
		public static ClaimFormItem[] List;
		///<summary></summary>
		public static ClaimFormItem Cur;
		///<summary></summary>
		public static ClaimFormItem[] ListForForm;

		///<summary>Gets all claimformitems for all claimforms.  Items for individual claimforms can later be extracted as needed.</summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * FROM claimformitem ORDER BY imagefilename desc";
			FillTable();
			List=new ClaimFormItem[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].ClaimFormItemNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ClaimFormNum    = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].ImageFileName   = PIn.PString(table.Rows[i][2].ToString());
				List[i].FieldName       = PIn.PString(table.Rows[i][3].ToString());
				List[i].FormatString    = PIn.PString(table.Rows[i][4].ToString());
				List[i].XPos            = PIn.PFloat (table.Rows[i][5].ToString());
				List[i].YPos            = PIn.PFloat (table.Rows[i][6].ToString());
				List[i].Width           = PIn.PFloat (table.Rows[i][7].ToString());
				List[i].Height          = PIn.PFloat (table.Rows[i][8].ToString());
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO claimformitem (claimformnum,imagefilename,fieldname,formatstring"
				+",xpos,ypos,width,height) VALUES("
				+"'"+POut.PInt   (Cur.ClaimFormNum)+"', "
				+"'"+POut.PString(Cur.ImageFileName)+"', "
				+"'"+POut.PString(Cur.FieldName)+"', "
				+"'"+POut.PString(Cur.FormatString)+"', "
				+"'"+POut.PFloat (Cur.XPos)+"', "
				+"'"+POut.PFloat (Cur.YPos)+"', "
				+"'"+POut.PFloat (Cur.Width)+"', "
				+"'"+POut.PFloat (Cur.Height)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.ClaimFormItemNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE claimformitem SET "
				+"claimformnum = '" +POut.PInt   (Cur.ClaimFormNum)+"' "
				+",imagefilename = '"+POut.PString(Cur.ImageFileName)+"' "
				+",fieldname = '"    +POut.PString(Cur.FieldName)+"' "
				+",formatstring = '" +POut.PString(Cur.FormatString)+"' "
				+",xpos = '"         +POut.PFloat (Cur.XPos)+"' "
				+",ypos = '"         +POut.PFloat (Cur.YPos)+"' "
				+",width = '"        +POut.PFloat (Cur.Width)+"' "
				+",height = '"       +POut.PFloat (Cur.Height)+"' "
				+"WHERE ClaimFormItemNum = '"+POut.PInt   (Cur.ClaimFormItemNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM claimformitem "
				+"WHERE ClaimFormItemNum = '"+POut.PInt(Cur.ClaimFormItemNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary>Gets all claimformitems for the current claimform from the preloaded List.</summary>
		public static void GetListForForm(){
			ArrayList tempAL=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ClaimFormNum==ClaimForms.Cur.ClaimFormNum){
					tempAL.Add(List[i]);
				}
			}
			ListForForm=new ClaimFormItem[tempAL.Count];
			tempAL.CopyTo(ListForForm);
		}


	}

	

	

}









