using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;

namespace OpenDentBusiness {
	///<summary></summary>
	public class DefB {
		///<summary>This gets filled automatically when Refresh is called.  If using remoting, then the calling program is responsible for filling this RawData on the client since the automated part only happens on the server.  So there are TWO RawDatas in a server situation, but only one in a small office that connects directly to the database.</summary>
		public static DataTable RawData;

		///<summary></summary>
		public static DataSet Refresh(){
			string command="SELECT * FROM definition ORDER BY Category,ItemOrder";
			DataConnection dcon=new DataConnection();
			RawData=dcon.GetTable(command);
			DataSet retVal=new DataSet();
			retVal.Tables.Add(RawData);
			return retVal;
		}

		///<summary>Returns the new DefNum</summary>
		public static int Insert(Def def){
			string command= "INSERT INTO definition (category,itemorder,"
				+"itemname,itemvalue,itemcolor,ishidden) VALUES("
				+"'"+POut.PInt((int)def.Category)+"', "
				+"'"+POut.PInt(def.ItemOrder)+"', "
				+"'"+POut.PString(def.ItemName)+"', "
				+"'"+POut.PString(def.ItemValue)+"', "
				+"'"+POut.PInt(def.ItemColor.ToArgb())+"', "
				+"'"+POut.PBool(def.IsHidden)+"')";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command,true);
			int defNum=dcon.InsertID;//used in conversion
			return defNum;
		}

		///<summary></summary>
		public static int Update(Def def) {
			string command = "UPDATE definition SET "
				+ "category = '"  +POut.PInt((int)def.Category)+"'"
				+",itemorder = '" +POut.PInt(def.ItemOrder)+"'"
				+",itemname = '"  +POut.PString(def.ItemName)+"'"
				+",itemvalue = '" +POut.PString(def.ItemValue)+"'"
				+",itemcolor = '" +POut.PInt(def.ItemColor.ToArgb())+"'"
				+",ishidden = '"  +POut.PBool(def.IsHidden)+"'"
				+"WHERE defnum = '"+POut.PInt(def.DefNum)+"'";
			DataConnection dcon=new DataConnection();
			int rowsChanged=dcon.NonQ(command);
			return rowsChanged;
		}

	}
}
