using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the zipcode table in the database.</summary>
	public struct ZipCode{
		///<summary>Primary key.</summary>
		public int ZipCodeNum;
		///<summary>The actual zipcode.</summary>
		public string ZipCodeDigits;
		///<summary></summary>
		public string City;
		///<summary></summary>
		public string State;
		///<summary>If true, then it will show in the dropdown list in the patient edit window.</summary>
		public bool IsFrequent;
	}

	/*=========================================================================================
		=================================== class ZipCodes ===========================================*/
  ///<summary></summary>
	public class ZipCodes:DataClass{
		///<summary></summary>
		public static ZipCode Cur;
		///<summary></summary>
		public static ZipCode[] List;
		///<summary></summary>
		public static ArrayList ALFrequent;
		///<summary></summary>
		public static ArrayList ALMatches;
		//public static Hashtable HList; 

		///<summary>Refresh done on startup and then whenever a change is made.</summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from zipcode ORDER BY zipcodedigits";
			FillTable();
			//HList=new Hashtable();
			ALFrequent=new ArrayList();
			List=new ZipCode[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i].ZipCodeNum   =PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ZipCodeDigits=PIn.PString(table.Rows[i][1].ToString());
				List[i].City         =PIn.PString(table.Rows[i][2].ToString());	
				List[i].State        =PIn.PString(table.Rows[i][3].ToString());	
				List[i].IsFrequent   =PIn.PBool  (table.Rows[i][4].ToString());
				if(List[i].IsFrequent){
					ALFrequent.Add(List[i]);
				}
				//HList.Add(List[i].ZipCodeNum,List[i]);
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO zipcode (zipcodedigits,city,state,isfrequent) "
				+"VALUES ("
				+"'"+POut.PString(Cur.ZipCodeDigits)+"', "
				+"'"+POut.PString(Cur.City)+"', "
				+"'"+POut.PString(Cur.State)+"', "
				+"'"+POut.PBool  (Cur.IsFrequent)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
			//Cur.ZipCodeNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE zipcode SET "
				+"zipcodedigits ='"+POut.PString(Cur.ZipCodeDigits)+"'"
				+",city ='"        +POut.PString(Cur.City)+"'"
				+",state ='"       +POut.PString(Cur.State)+"'"
				+",isfrequent ='"  +POut.PBool  (Cur.IsFrequent)+"'"
				+" WHERE zipcodenum = '"+POut.PInt(Cur.ZipCodeNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from zipcode WHERE zipcodenum = '"+POut.PInt(Cur.ZipCodeNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void GetALMatches(string zipCodeDigits){
			ALMatches=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ZipCodeDigits==zipCodeDigits){
					ALMatches.Add(List[i]);
				}
			}

		}

	}

	

}













