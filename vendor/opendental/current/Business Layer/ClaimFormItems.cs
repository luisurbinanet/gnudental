using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace OpenDental{
	
	///<summary>One item is needed for each field on a claimform.</summary>
	public class ClaimFormItem{
		///<summary>Primary key.</summary>
		[XmlIgnore]
		public int ClaimFormItemNum;
		///<summary>FK to claimform.ClaimFormNum</summary>
		[XmlIgnore]
		public int ClaimFormNum;
		///<summary>If this item is an image.  Usually only one per claimform.  eg ADA2002.emf.  Otherwise it MUST be left blank, or it will trigger an error that the image cannot be found.</summary>
		public string ImageFileName;
		///<summary>Must be one of the hardcoded available fieldnames for claims.</summary>
		public string FieldName;
		///<summary>For dates, the format string. ie MM/dd/yyyy or M d y among many other possibilities.</summary>
		public string FormatString;
		///<summary>The x position of the item on the claim form.  In pixels. 100 pixels per inch.</summary>
		public float XPos;
		///<summary>The y position of the item.</summary>
		public float YPos;
		///<summary>Limits the printable area of the item. Set to zero to not limit.</summary>
		public float Width;
		///<summary>Limits the printable area of the item. Set to zero to not limit.</summary>
		public float Height;

		///<summary>Returns a copy of the claimformitem.</summary>
    public ClaimFormItem Copy(){
			ClaimFormItem cfi=new ClaimFormItem();
			cfi.ClaimFormItemNum=ClaimFormItemNum;
			cfi.ClaimFormNum=ClaimFormNum;
			cfi.ImageFileName=ImageFileName;
			cfi.FieldName=FieldName;
			cfi.FormatString=FormatString;
			cfi.XPos=XPos;
			cfi.YPos=YPos;
			cfi.Width=Width;
			cfi.Height=Height;
			return cfi;
		}

		///<summary></summary>
		public void Insert(){
			string command="INSERT INTO claimformitem (claimformnum,imagefilename,fieldname,formatstring"
				+",xpos,ypos,width,height) VALUES("
				+"'"+POut.PInt   (ClaimFormNum)+"', "
				+"'"+POut.PString(ImageFileName)+"', "
				+"'"+POut.PString(FieldName)+"', "
				+"'"+POut.PString(FormatString)+"', "
				+"'"+POut.PFloat (XPos)+"', "
				+"'"+POut.PFloat (YPos)+"', "
				+"'"+POut.PFloat (Width)+"', "
				+"'"+POut.PFloat (Height)+"')";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			ClaimFormItemNum=dcon.InsertID;
		}

		///<summary></summary>
		public void Update(){
			string command= "UPDATE claimformitem SET "
				+"claimformnum = '" +POut.PInt   (ClaimFormNum)+"' "
				+",imagefilename = '"+POut.PString(ImageFileName)+"' "
				+",fieldname = '"    +POut.PString(FieldName)+"' "
				+",formatstring = '" +POut.PString(FormatString)+"' "
				+",xpos = '"         +POut.PFloat (XPos)+"' "
				+",ypos = '"         +POut.PFloat (YPos)+"' "
				+",width = '"        +POut.PFloat (Width)+"' "
				+",height = '"       +POut.PFloat (Height)+"' "
				+"WHERE ClaimFormItemNum = '"+POut.PInt   (ClaimFormItemNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Delete(){
			string command = "DELETE FROM claimformitem "
				+"WHERE ClaimFormItemNum = '"+POut.PInt(ClaimFormItemNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}



	}

	/*=========================================================================================
		=================================== class ClaimFormItems ==========================================*/

	///<summary></summary>
	public class ClaimFormItems{
		///<summary></summary>
		private static ClaimFormItem[] List;
		//<summary></summary>
		//public static ClaimFormItem Cur;
		//<summary></summary>
		//public static ClaimFormItem[] ListForForm;

		///<summary>Gets all claimformitems for all claimforms.  Items for individual claimforms can later be extracted as needed.</summary>
		public static void Refresh(){
			string command=
				"SELECT * FROM claimformitem ORDER BY imagefilename desc";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			List=new ClaimFormItem[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new ClaimFormItem();
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

		///<summary>Gets all claimformitems for the specified claimform from the preloaded List.</summary>
		public static ClaimFormItem[] GetListForForm(int claimFormNum){
			ArrayList tempAL=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ClaimFormNum==claimFormNum){
					tempAL.Add(List[i]);
				}
			}
			ClaimFormItem[] ListForForm=new ClaimFormItem[tempAL.Count];
			tempAL.CopyTo(ListForForm);
			return ListForForm;
		}


	}

	

	

}









