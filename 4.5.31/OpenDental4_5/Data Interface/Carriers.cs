using System;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class Carriers{
		///<summary></summary>
		public static Carrier[] List;
		///<summary>A hashtable of all carriers.</summary>
		public static Hashtable HList;

		///<summary>Carriers are not refreshed as local data, but are refreshed as needed. A full refresh is frequently triggered if a carrierNum cannot be found in the HList.  Important retrieval is done directly from the db.</summary>
		public static void Refresh(){
			HList=new Hashtable();
			string command="SELECT * from carrier ORDER BY CarrierName";
			DataTable table=General.GetTable(command);
			List=new Carrier[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Carrier();
				List[i].CarrierNum  =PIn.PInt   (table.Rows[i][0].ToString());
				List[i].CarrierName =PIn.PString(table.Rows[i][1].ToString());
				List[i].Address     =PIn.PString(table.Rows[i][2].ToString());
				List[i].Address2    =PIn.PString(table.Rows[i][3].ToString());
				List[i].City        =PIn.PString(table.Rows[i][4].ToString());
				List[i].State       =PIn.PString(table.Rows[i][5].ToString());
				List[i].Zip         =PIn.PString(table.Rows[i][6].ToString());
				List[i].Phone       =PIn.PString(table.Rows[i][7].ToString());
				List[i].ElectID     =PIn.PString(table.Rows[i][8].ToString());
				List[i].NoSendElect =PIn.PBool  (table.Rows[i][9].ToString());
				HList.Add(List[i].CarrierNum,List[i]);
			}
		}

		///<summary></summary>
		public static void Update(Carrier Cur){
			string command="UPDATE carrier SET "
				+ "CarrierName= '" +POut.PString(Cur.CarrierName)+"' "
				+ ",Address= '"    +POut.PString(Cur.Address)+"' "
				+ ",Address2= '"   +POut.PString(Cur.Address2)+"' "
				+ ",City= '"       +POut.PString(Cur.City)+"' "
				+ ",State= '"      +POut.PString(Cur.State)+"' "
				+ ",Zip= '"        +POut.PString(Cur.Zip)+"' "
				+ ",Phone= '"      +POut.PString(Cur.Phone)+"' "
				+ ",ElectID= '"    +POut.PString(Cur.ElectID)+"' "
				+ ",NoSendElect= '"+POut.PBool  (Cur.NoSendElect)+"' "
				+"WHERE CarrierNum = '"+POut.PInt(Cur.CarrierNum)+"'";
			//MessageBox.Show(string command);
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(Carrier Cur){
			if(PrefB.RandomKeys){
				Cur.CarrierNum=MiscData.GetKey("carrier","CarrierNum");
			}
			string command="INSERT INTO carrier (";
			if(PrefB.RandomKeys){
				command+="CarrierNum,";
			}
			command+="CarrierName,Address,Address2,City,State,Zip,Phone"
				+",ElectID,NoSendElect) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(Cur.CarrierNum)+"', ";
			}
			command+=
				 "'"+POut.PString(Cur.CarrierName)+"', "
				+"'"+POut.PString(Cur.Address)+"', "
				+"'"+POut.PString(Cur.Address2)+"', "
				+"'"+POut.PString(Cur.City)+"', "
				+"'"+POut.PString(Cur.State)+"', "
				+"'"+POut.PString(Cur.Zip)+"', "
				+"'"+POut.PString(Cur.Phone)+"', "
				+"'"+POut.PString(Cur.ElectID)+"', "
				+"'"+POut.PBool  (Cur.NoSendElect)+"')";
			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				Cur.CarrierNum=General.NonQ(command,true);
			}
			//id used in the conversion process for 2.8
		}

		///<summary>There MUST not be any dependencies before calling this or there will be invalid foreign keys.  This is only called from FormCarrierEdit after proper validation.</summary>
		public static void Delete(Carrier Cur){
			string command="DELETE from carrier WHERE CarrierNum = '"+Cur.CarrierNum.ToString()+"'";
			General.NonQ(command);
		}

		///<summary>Returns a list of insplans that are dependent on the Cur carrier. Used to display in carrier edit and before deleting a carrier to make sure carrier is not in use.</summary>
		public static string[] DependentPlans(Carrier Cur){
			string command="SELECT CONCAT(LName,', ',FName) FROM patient,insplan" 
				+" WHERE patient.PatNum=insplan.Subscriber"
				+" && insplan.CarrierNum = '"+POut.PInt(Cur.CarrierNum)+"'"
				+" ORDER BY LName,FName";
			DataTable table=General.GetTable(command);
			string[] retStr=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				retStr[i]=PIn.PString(table.Rows[i][0].ToString());
			}
			return retStr;
		}

		///<summary>Gets the name of a carrier based on the carrierNum.  This also refreshes the list if necessary, so it will work even if the list has not been refreshed recently.</summary>
		public static string GetName(int carrierNum){
			if(HList.ContainsKey(carrierNum)){
				return ((Carrier)HList[carrierNum]).CarrierName;
			}
			//if the carrierNum could not be found:
			Refresh();
			if(HList.ContainsKey(carrierNum)){
				return ((Carrier)HList[carrierNum]).CarrierName;
			}
			//this could only happen if corrupted:
			return "";
		}

		/*
		///<summary>Getting phased out.  Sets Cur to the carrier based on the carrierNum.  This also refreshes the list if necessary, so it will work even if the list has not been refreshed recently..</summary>
		public static void GetCur(int carrierNum){
			Cur=GetCarrier(carrierNum);
		}*/

		///<summary>Replacing GetCur. Gets the specified carrier. This also refreshes the list if necessary, so it will work even if the list has not been refreshed recently.</summary>
		public static Carrier GetCarrier(int carrierNum){
			if(carrierNum==0){
				Carrier retVal=new Carrier();
				retVal.CarrierName="";//instead of null. Helps prevent crash.
				return retVal;
			}
			if(HList.ContainsKey(carrierNum)){
				return (Carrier)HList[carrierNum];
			}
			//if the carrierNum could not be found:
			Refresh();
			if(HList.ContainsKey(carrierNum)){
				return (Carrier)HList[carrierNum];
			}
			//this could only happen if corrupted:
			Carrier retVall=new Carrier();
			retVall.CarrierName="";//instead of null. Helps prevent crash.
			return retVall;
		}

		///<summary>Gets a carrierNum from the database based on the other data in Cur.  Sets Cur.CarrierNum accordingly. If there is no matching carrier, then a new carrier is created.  The end result is that Cur will now always have a valid carrierNum to use.</summary>
		public static void GetCurSame(Carrier Cur){
			if(Cur.CarrierName==""){
				Cur=new Carrier();
				return;
			}
			string command="SELECT CarrierNum FROM carrier WHERE " 
				+"CarrierName = '"   +POut.PString(Cur.CarrierName)+"' "
				+"&& Address = '"    +POut.PString(Cur.Address)+"' "
				+"&& Address2 = '"   +POut.PString(Cur.Address2)+"' "
				+"&& City = '"       +POut.PString(Cur.City)+"' "
				+"&& State = '"      +POut.PString(Cur.State)+"' "
				+"&& Zip = '"        +POut.PString(Cur.Zip)+"' "
				+"&& Phone = '"      +POut.PString(Cur.Phone)+"' "
				+"&& ElectID = '"    +POut.PString(Cur.ElectID)+"' "
				+"&& NoSendElect = '"+POut.PBool  (Cur.NoSendElect)+"'";
			DataTable table=General.GetTable(command);
			if(table.Rows.Count>0){
				Cur.CarrierNum=PIn.PInt(table.Rows[0][0].ToString());
				return;
			}
			Insert(Cur);
			//MessageBox.Show(Cur.EmployerNum.ToString());
			return;
		}

		///<summary>Returns an arraylist of Carriers with names similar to the supplied string.  Used in dropdown list from carrier field for faster entry.  There is a small chance that the list will not be completely refreshed when this is run, but it won't really matter if one carrier doesn't show in dropdown.</summary>
		public static ArrayList GetSimilarNames(string carrierName){
			ArrayList retVal=new ArrayList();
			for(int i=0;i<List.Length;i++){
				//if(i>0 && List[i].CarrierName==List[i-1].CarrierName){
				//	continue;//ignore all duplicate names
				//}
				//if(Regex.IsMatch(List[i].CarrierName,"^"+carrierName,RegexOptions.IgnoreCase))
				if(List[i].CarrierName.ToUpper().IndexOf(carrierName.ToUpper())==0)
					retVal.Add(List[i]);
			}
			return retVal;
		}

		///<summary>Combines all the given carriers into one. The carrier that will be used as the basis of the combination is specified in the usingIndex parameter. Updates insplan, then deletes all the other carriers.</summary>
		public static void Combine(int[] carrierNums,int usingIndex){
			string newNum=carrierNums[usingIndex].ToString();
			for(int i=0;i<carrierNums.Length;i++){
				if(i==usingIndex)
					continue;
				string command="UPDATE insplan SET CarrierNum = '"+newNum
					+"' WHERE CarrierNum = '"+carrierNums[i].ToString()+"'";
				//MessageBox.Show(string command);
				General.NonQ(command);
				command="DELETE FROM carrier"
					+" WHERE CarrierNum = '"+carrierNums[i].ToString()+"'";
				General.NonQ(command);
			}
		}

	}

	
	

}













