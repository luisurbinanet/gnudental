using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the carrier table in the database.</summary>
	public struct Carrier{
		///<summary>Primary key.</summary>
		public int CarrierNum;
		///<summary>Name of the carrier.</summary>
		public string CarrierName;
		///<summary></summary>
		public string Address;
		///<summary>Second line of address.</summary>
		public string Address2;
		///<summary></summary>
		public string City;
		///<summary>2 char in the US.</summary>
		public string State;
		///<summary></summary>
		public string Zip;
		///<summary>Includes any punctuation.</summary>
		public string Phone;
		///<summary>E-claims electronic payer id.  5 char.</summary>
		public string ElectID;
		///<summary>Do not send electronically.  It's just a default; you can still send electronically.</summary>
		public bool NoSendElect;
	}
	
	/*=========================================================================================
		=================================== class Carriers ===========================================*/
	///<summary>Carriers are not refreshed as local data, but are refreshed as needed. A full refresh is frequently triggered if an carrierNum cannot be found in the HList.  Important retrieval is done directly from the db.</summary>
	public class Carriers:DataClass{
		///<summary></summary>
		public static Carrier[] List;
		///<summary>A hashtable of all carriers.</summary>
		public static Hashtable HList;
		///<summary></summary>
		public static Carrier Cur;

		///<summary></summary>
		public static void Refresh(){
			HList=new Hashtable();
			cmd.CommandText = 
				"SELECT * from carrier ORDER BY CarrierName";
			FillTable();
			//Employer temp=new Employer();
			List=new Carrier[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
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
		public static void UpdateCur(){
			cmd.CommandText="UPDATE carrier SET "
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
			//MessageBox.Show(cmd.CommandText);
			NonQ();
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO carrier(CarrierName,Address,Address2,City,State,Zip,Phone"
				+",ElectID,NoSendElect) VALUES("
				+"'"+POut.PString(Cur.CarrierName)+"', "
				+"'"+POut.PString(Cur.Address)+"', "
				+"'"+POut.PString(Cur.Address2)+"', "
				+"'"+POut.PString(Cur.City)+"', "
				+"'"+POut.PString(Cur.State)+"', "
				+"'"+POut.PString(Cur.Zip)+"', "
				+"'"+POut.PString(Cur.Phone)+"', "
				+"'"+POut.PString(Cur.ElectID)+"', "
				+"'"+POut.PBool  (Cur.NoSendElect)+"')";
			NonQ(true);//id used in the conversion process for 2.8
			Cur.CarrierNum=InsertID;
		}

		///<summary>There MUST not be any dependencies before calling this or there will be invalid foreign keys.  This is only called from FormCarrierEdit after proper validation.</summary>
		public static void DeleteCur(){
			cmd.CommandText="DELETE from carrier WHERE CarrierNum = '"+Cur.CarrierNum.ToString()+"'";
			NonQ();
		}

		///<summary>Returns a list of insplans that are dependent on the Cur carrier. Used to display in carrier edit and before deleting a carrier to make sure carrier is not in use.</summary>
		public static string[] DependentPlans(){
			cmd.CommandText="SELECT CONCAT(LName,', ',FName) FROM patient,insplan" 
				+" WHERE patient.PatNum=insplan.Subscriber"
				+" && insplan.CarrierNum = '"+POut.PInt(Cur.CarrierNum)+"'"
				+" ORDER BY LName,FName";
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			string[] retStr=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				retStr[i]=PIn.PString(table.Rows[i][0].ToString());
			}
			return retStr;
		}

		///<summary>Gets the name of a carrier based on the employerNum.  This also refreshes the list if necessary, so it will work even if the list has not been refreshed recently.</summary>
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

		///<summary>Sets Cur to the carrier based on the carrierNum.  This also refreshes the list if necessary, so it will work even if the list has not been refreshed recently..</summary>
		public static void GetCur(int carrierNum){
			if(HList.ContainsKey(carrierNum)){
				Cur=(Carrier)HList[carrierNum];
				return;
			}
			//if the carrierNum could not be found:
			Refresh();
			if(HList.ContainsKey(carrierNum)){
				Cur=(Carrier)HList[carrierNum];
				return;
			}
			//this could only happen if corrupted:
			Cur=new Carrier();
		}

		///<summary>Gets a carrierNum based on the other data in Cur.  If there is no matching carrier, then a new carrier is created.  The end result is that a valid Cur will always be set.</summary>
		public static void GetCurSame(){
			if(Cur.CarrierName==""){
				Cur=new Carrier();
				return;
			}
			cmd.CommandText="SELECT CarrierNum FROM carrier WHERE " 
				+"CarrierName = '"   +POut.PString(Cur.CarrierName)+"' "
				+"&& Address = '"    +POut.PString(Cur.Address)+"' "
				+"&& Address2 = '"   +POut.PString(Cur.Address2)+"' "
				+"&& City = '"       +POut.PString(Cur.City)+"' "
				+"&& State = '"      +POut.PString(Cur.State)+"' "
				+"&& Zip = '"        +POut.PString(Cur.Zip)+"' "
				+"&& Phone = '"      +POut.PString(Cur.Phone)+"' "
				+"&& ElectID = '"    +POut.PString(Cur.ElectID)+"' "
				+"&& NoSendElect = '"+POut.PBool  (Cur.NoSendElect)+"'";
			FillTable();
			if(table.Rows.Count>0){
				Cur.CarrierNum=PIn.PInt(table.Rows[0][0].ToString());
				return;
			}
			InsertCur();
			//MessageBox.Show(Cur.EmployerNum.ToString());
			return;
		}

		///<summary>Returns an arraylist of Carriers with names similar to the supplied string.  Used in dropdown list from carrier field for faster entry.  There is a small chance that the list will not be completely refreshed when this is run, but it won't really matter if one carrier doesn't show in dropdown.</summary>
		public static ArrayList GetSimilarNames(string carrierName){
			ArrayList retVal=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(i>0 && List[i].CarrierName==List[i-1].CarrierName){
					continue;//ignore all duplicate names
				}
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
				cmd.CommandText="UPDATE insplan SET CarrierNum = '"+newNum
					+"' WHERE CarrierNum = '"+carrierNums[i].ToString()+"'";
				//MessageBox.Show(cmd.CommandText);
				NonQ();
				cmd.CommandText="DELETE FROM carrier"
					+" WHERE CarrierNum = '"+carrierNums[i].ToString()+"'";
				NonQ();
			}
		}

	}

	
	

}













