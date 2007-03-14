using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace OpenDental{
	
	///<summary>Corresponds to the claimform table in the database.</summary>
	///<remarks>Stores the information for printing different types of claim forms.</remarks>
	public class ClaimForm{
		///<summary>Primary key.</summary>
		[XmlIgnore]
		public int ClaimFormNum;
		///<summary>eg. ADA2002 or CA Medicaid</summary>
		public string Description;
		///<summary>If true, then it will not be displayed in various claim form lists as a choice.</summary>
		[XmlIgnore]
		public bool IsHidden;
		///<summary>Valid font name for all text on the form.</summary>
		public string FontName="";
		///<summary>Font size for all text on the form.</summary>
		public float FontSize;
		///<summary>For instance OD12 or JoeDeveloper9.  If you are a developer releasing claimforms, then this should be your name or company followed by a unique number.  This will later make it easier for you to maintain your claimforms for your customers.  All claimforms that we release will be of the form OD##.  Reports that the user creates will have this field blank, protecting them from being changed by us.</summary>
		///<remarks>ADA2002=OD1,Denti-Cal=OD2,ADA2000=OD3,HCFA1500=OD4,HCFA1500preprinted=OD5,Canadian=OD6,Belgian=OD7</remarks>
		public string UniqueID="";
		///<summary>Set to false to not print images.  This removes the background for printing on premade forms.</summary>
		public bool PrintImages;
		///<summary>Shifts all items by x/100th's of an inch to compensate for printer, typically less than 1/4 inch.</summary>
		[XmlIgnore]
		public int OffsetX;
		///<summary>Shifts all items by y/100th's of an inch to compensate for printer, typically less than 1/4 inch.</summary>
		[XmlIgnore]
		public int OffsetY;
		///<summary>This is not a database column.  It is an array of all claimformItems that are attached to this ClaimForm.</summary>
		public ClaimFormItem[] Items;

		///<summary>Returns a copy of the claimform including the Items.  Only used in FormClaimForms.butCopy_Click.</summary>
    public ClaimForm Copy(){
			ClaimForm cf=new ClaimForm();
			cf.ClaimFormNum=ClaimFormNum;
			cf.Description=Description;
			cf.IsHidden=IsHidden;
			cf.FontName=FontName;
			cf.FontSize=FontSize;
			cf.UniqueID=UniqueID;
			cf.PrintImages=PrintImages;
			cf.OffsetX=OffsetX;
			cf.OffsetY=OffsetY;
			cf.Items=(ClaimFormItem[])Items.Clone();
			//Items.CopyTo(cf.Items,0);
			return cf;
		}

		///<summary>Inserts this claimform into database and retrieves the new primary key.</summary>
		public void Insert(){
			string command="INSERT INTO claimform (Description,IsHidden,FontName,FontSize"
				+",UniqueId,PrintImages,OffsetX,OffsetY) VALUES("
				+"'"+POut.PString(Description)+"', "
				+"'"+POut.PBool  (IsHidden)+"', "
				+"'"+POut.PString(FontName)+"', "
				+"'"+POut.PFloat (FontSize)+"', "
				+"'"+POut.PString(UniqueID)+"', "
				+"'"+POut.PBool  (PrintImages)+"', "
				+"'"+POut.PInt   (OffsetX)+"', "
				+"'"+POut.PInt   (OffsetY)+"')";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			ClaimFormNum=dcon.InsertID;
		}

		///<summary></summary>
		public void Update(){
			string command="UPDATE claimform SET "
				+"Description = '" +POut.PString(Description)+"' "
				+",IsHidden = '"    +POut.PBool  (IsHidden)+"' "
				+",FontName = '"    +POut.PString(FontName)+"' "
				+",FontSize = '"    +POut.PFloat (FontSize)+"' "
				+",UniqueID = '"    +POut.PString(UniqueID)+"' "
				+",PrintImages = '" +POut.PBool  (PrintImages)+"' "
				+",OffsetX = '"     +POut.PInt   (OffsetX)+"' "
				+",OffsetX = '"     +POut.PInt   (OffsetY)+"' "
				+"WHERE ClaimFormNum = '"+POut.PInt   (ClaimFormNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary> Called when cancelling out of creating a new claimform, and from the claimform window when clicking delete. Returns true if successful or false if dependencies found.</summary>
		public bool Delete(){
			//first, do dependency testing
			string command="SELECT * FROM insplan WHERE claimformnum = '"
				+ClaimFormNum.ToString()+"' LIMIT 1";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			if(table.Rows.Count==1){
				return false;
			}
			//Then, delete the claimform
			command="DELETE FROM claimform "
				+"WHERE ClaimFormNum = '"+POut.PInt(ClaimFormNum)+"'";
			dcon.NonQ(command);
			command="DELETE FROM claimformitem "
				+"WHERE ClaimFormNum = '"+POut.PInt(ClaimFormNum)+"'";
			dcon.NonQ(command);
			return true;
		}

		///<summary>Updates all claimforms with this unique id including all attached items.</summary>
		public void UpdateByUniqueID(){
			//first get a list of the ClaimFormNums with this UniqueId
			string command=
				"SELECT ClaimFormNum FROM claimform WHERE UniqueID ='"+UniqueID.ToString()+"'";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			int[] claimFormNums=new int[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				claimFormNums[i]=PIn.PInt   (table.Rows[i][0].ToString());
			}
			//loop through each matching claimform
			for(int i=0;i<claimFormNums.Length;i++){
				ClaimFormNum=claimFormNums[i];
				Update();
				command="DELETE FROM claimformitem "
					+"WHERE ClaimFormNum = '"+POut.PInt(claimFormNums[i])+"'";
				dcon.NonQ(command);
				for(int j=0;j<Items.Length;j++){
					Items[j].ClaimFormNum=claimFormNums[i];
					Items[j].Insert();
				}
			}
		}




	}

/*=========================================================================================
		=================================== class ClaimForms ==========================================*/

	///<summary></summary>
	public class ClaimForms{
		///<summary>List of all claim forms.</summary>
		public static ClaimForm[] ListLong;
		///<summary>List of all claim forms except those marked as hidden.</summary>
		public static ClaimForm[] ListShort;
		//<summary></summary>
		//public static ClaimForm Cur;

		///<summary>Fills ListShort and ListLong with all claimforms from the db. Also attaches corresponding claimformitems to each.</summary>
		public static void Refresh(){
			string command=
				"SELECT * FROM claimform";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			ListLong=new ClaimForm[table.Rows.Count];
			ArrayList tempAL=new ArrayList();
			for(int i=0;i<table.Rows.Count;i++){
				ListLong[i]=new ClaimForm();
				ListLong[i].ClaimFormNum= PIn.PInt   (table.Rows[i][0].ToString());
				ListLong[i].Description = PIn.PString(table.Rows[i][1].ToString());
				ListLong[i].IsHidden    = PIn.PBool  (table.Rows[i][2].ToString());
				ListLong[i].FontName    = PIn.PString(table.Rows[i][3].ToString());
				ListLong[i].FontSize    = PIn.PFloat (table.Rows[i][4].ToString());
				ListLong[i].UniqueID    = PIn.PString(table.Rows[i][5].ToString());
				ListLong[i].PrintImages = PIn.PBool  (table.Rows[i][6].ToString());
				ListLong[i].OffsetX     = PIn.PInt   (table.Rows[i][7].ToString());
				ListLong[i].OffsetY     = PIn.PInt   (table.Rows[i][8].ToString());
				ListLong[i].Items=ClaimFormItems.GetListForForm(ListLong[i].ClaimFormNum);
				if(!ListLong[i].IsHidden)
					tempAL.Add(ListLong[i]);
			}
			ListShort=new ClaimForm[tempAL.Count];
			for(int i=0;i<ListShort.Length;i++){
				ListShort[i]=(ClaimForm)tempAL[i];
			}
		}

		///<summary>Returns the claim form specified by the given claimFormNum</summary>
		public static ClaimForm GetClaimForm(int claimFormNum){
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ClaimFormNum==claimFormNum){
					return ListLong[i];
				}
			}
			MessageBox.Show("Error. Could not locate Claim Form.");
			return null;
		}

		


	}

	



}









