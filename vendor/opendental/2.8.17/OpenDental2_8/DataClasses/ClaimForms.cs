using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the claimform table in the database.</summary>
	///<remarks>Stores the information for printing different types of claim forms.</remarks>
	public struct ClaimForm{
		///<summary>Primary key.</summary>
		public int ClaimFormNum;
		///<summary>eg. ADA2002 or CA Medicaid</summary>
		public string Description;
		///<summary>If true, then it will not be displayed in various claim form lists as a choice.</summary>
		public bool IsHidden;
		///<summary>Valid font name for all text on the form.</summary>
		public string FontName;
		///<summary>Font size for all text on the form.</summary>
		public float FontSize;
		///<summary>Assigned by us for maintenance purposes. Do not change.
		///Will be 0 for claim forms added by user, protecting them from being changed by us.</summary>
		public int UniqueID;
		///<summary>Set to false to not print images.  This removes the background for printing on premade forms.</summary>
		public bool PrintImages;
		///<summary>Shifts all items by x/100th's of an inch to compensate for printer, typically less than 1/4 inch.</summary>
		public int OffsetX;
		///<summary>Shifts all items by y/100th's of an inch to compensate for printer, typically less than 1/4 inch.</summary>
		public int OffsetY;
	}

/*=========================================================================================
		=================================== class ClaimForms ==========================================*/

	///<summary></summary>
	public class ClaimForms:DataClass{
		///<summary>List of all claim forms.</summary>
		public static ClaimForm[] ListLong;
		///<summary>List of all claim forms except those marked as hidden.</summary>
		public static ClaimForm[] ListShort;
		///<summary></summary>
		public static ClaimForm Cur;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * FROM claimform";
			FillTable();
			ListLong=new ClaimForm[table.Rows.Count];
			ArrayList tempAL=new ArrayList();
			for(int i=0;i<table.Rows.Count;i++){
				ListLong[i].ClaimFormNum= PIn.PInt   (table.Rows[i][0].ToString());
				ListLong[i].Description = PIn.PString(table.Rows[i][1].ToString());
				ListLong[i].IsHidden    = PIn.PBool  (table.Rows[i][2].ToString());
				ListLong[i].FontName    = PIn.PString(table.Rows[i][3].ToString());
				ListLong[i].FontSize    = PIn.PFloat (table.Rows[i][4].ToString());
				ListLong[i].UniqueID    = PIn.PInt   (table.Rows[i][5].ToString());
				ListLong[i].PrintImages = PIn.PBool  (table.Rows[i][6].ToString());
				ListLong[i].OffsetX     = PIn.PInt   (table.Rows[i][7].ToString());
				ListLong[i].OffsetY     = PIn.PInt   (table.Rows[i][8].ToString());
				if(!ListLong[i].IsHidden)
					tempAL.Add(ListLong[i]);
			}
			ListShort=new ClaimForm[tempAL.Count];
			for(int i=0;i<ListShort.Length;i++){
				ListShort[i]=(ClaimForm)tempAL[i];
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO claimform (description,ishidden,fontname,fontsize"
				+",uniqueid,printimages,offsetx,offsety) VALUES("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PBool  (Cur.IsHidden)+"', "
				+"'"+POut.PString(Cur.FontName)+"', "
				+"'"+POut.PFloat (Cur.FontSize)+"', "
				+"'"+POut.PInt   (Cur.UniqueID)+"', "
				+"'"+POut.PBool  (Cur.PrintImages)+"', "
				+"'"+POut.PInt   (Cur.OffsetX)+"', "
				+"'"+POut.PInt   (Cur.OffsetY)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.ClaimFormNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE claimform SET "
				+"description = '" +POut.PString(Cur.Description)+"' "
				+",ishidden = '"    +POut.PBool  (Cur.IsHidden)+"' "
				+",fontname = '"    +POut.PString(Cur.FontName)+"' "
				+",fontsize = '"    +POut.PFloat (Cur.FontSize)+"' "
				+",uniqueid = '"    +POut.PInt   (Cur.UniqueID)+"' "
				+",printimages = '" +POut.PBool  (Cur.PrintImages)+"' "
				+",offsetx = '"     +POut.PInt   (Cur.OffsetX)+"' "
				+",offsety = '"     +POut.PInt   (Cur.OffsetY)+"' "
				+"WHERE ClaimFormNum = '"+POut.PInt   (Cur.ClaimFormNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void SetCur(int claimFormNum){
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ClaimFormNum==claimFormNum){
					Cur=ListLong[i];
					return;
				}
			}
			MessageBox.Show("Error. Could not locate Claim Form.");
		}

		///<summary> Called when cancelling out of creating a new claimform, and from the claimform window when clicking delete. Returns true if successful or false if dependencies found.</summary>
		public static bool DeleteCur(){
			//first, do dependency testing
			cmd.CommandText="SELECT * FROM insplan WHERE claimformnum = '"
				+Cur.ClaimFormNum.ToString()+"' LIMIT 1";
			FillTable();
			if(table.Rows.Count==1){
				return false;
			}
			cmd.CommandText="SELECT * FROM instemplate WHERE claimformnum = '"
				+Cur.ClaimFormNum.ToString()+"' LIMIT 1";
			FillTable();
			if(table.Rows.Count==1){
				return false;
			}
			//Then, delete the claimform
			cmd.CommandText = "DELETE FROM claimform "
				+"WHERE ClaimFormNum = '"+POut.PInt(Cur.ClaimFormNum)+"'";
			NonQ(false);
			cmd.CommandText = "DELETE FROM claimformitem "
				+"WHERE ClaimFormNum = '"+POut.PInt(Cur.ClaimFormNum)+"'";
			NonQ(false);
			return true;
		}


	}

	



}









