using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the provider table in the database.</summary>
	public struct Provider{
		///<summary>Primary key.</summary>
		public int ProvNum;
		///<summary>Abbreviation.</summary>
		public string Abbr;
		///<summary>Order that provider will show in lists.</summary>
		public int ItemOrder;
		///<summary>Last name.</summary>
		public string LName;
		///<summary>First name.</summary>
		public string FName;
		///<summary>Middle inital or name.</summary>
		public string MI;
		///<summary>eg. DMD or DDS. Was 'title' in previous versions.</summary>
		public string Suffix;
		///<summary>Foreign key to Definition.DefNum.</summary>
		public int FeeSched;
		///<summary>See the DentalSpecialty enumeration.</summary>
		public DentalSpecialty Specialty;
		///<summary>or TIN.  No punctuation</summary>
		public string SSN;
		///<summary>can include punctuation</summary>
		public string StateLicense;
		///<summary></summary>
		public string DEANum;
		///<summary>True if hygeinist.</summary>
		public bool IsSecondary;//
		///<summary>Color that shows in appointments</summary>
		public Color ProvColor;
		///<summary>If true, provider will not show on any lists</summary>
		public bool IsHidden;
		///<summary>True if the SSN field is actually a Tax ID Num</summary>
		public bool UsingTIN;
		///<summary>No longer used since each state assigns a different ID.  Use the providerident instead which allows you to assign a different BCBS ID for each Payor ID.</summary>
		public string BlueCrossIDOld;
		///<summary>Signature on file</summary>
		public bool SigOnFile;//
		///<summary></summary>
		public string Password;
		///<summary></summary>
		public string UserName;
		///<summary></summary>
		public string MedicaidID;
		///<summary>Color that shows in appointments as outline when highlighted.</summary>
		public Color OutlineColor;
		///<summary>Used in dental schools.  Foreign key to schoolclass.SchoolClassNum.  Each student is a provider.  This keeps track of which class they are in.</summary>
		public int SchoolClassNum;
	}

	/*=========================================================================================
	=================================== class Providers ==========================================*/

	///<summary></summary>
	public class Providers:DataClass{
		///<summary>Rarely used. Includes all providers, even if hidden.</summary>
		public static Provider[] ListLong;
		///<summary>This is the list used most often. It does not include hidden providers.</summary>
		public static Provider[] List;
		///<summary></summary>
		public static Provider Cur;
		///<summary></summary>
		public static int Selected;

		///<summary></summary>
		public static void Refresh(){
			ArrayList AL=new ArrayList();
			cmd.CommandText =
				"SELECT * from provider"
				//+" WHERE category = '"+j+"'"
				+" ORDER BY itemorder";
			FillTable();
			ListLong=new Provider[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				ListLong[i].ProvNum       = PIn.PInt   (table.Rows[i][0].ToString());
				ListLong[i].Abbr          = PIn.PString(table.Rows[i][1].ToString());
				ListLong[i].ItemOrder     = PIn.PInt   (table.Rows[i][2].ToString());
				ListLong[i].LName         = PIn.PString(table.Rows[i][3].ToString());
				ListLong[i].FName         = PIn.PString(table.Rows[i][4].ToString());
				ListLong[i].MI            = PIn.PString(table.Rows[i][5].ToString());
				ListLong[i].Suffix        = PIn.PString(table.Rows[i][6].ToString());
				ListLong[i].FeeSched      = PIn.PInt   (table.Rows[i][7].ToString());
				ListLong[i].Specialty     =(DentalSpecialty)PIn.PInt (table.Rows[i][8].ToString());
				ListLong[i].SSN           = PIn.PString(table.Rows[i][9].ToString());
				ListLong[i].StateLicense  = PIn.PString(table.Rows[i][10].ToString());
				ListLong[i].DEANum        = PIn.PString(table.Rows[i][11].ToString());
				ListLong[i].IsSecondary   = PIn.PBool  (table.Rows[i][12].ToString());
				ListLong[i].ProvColor     = Color.FromArgb(PIn.PInt(table.Rows[i][13].ToString()));
				ListLong[i].IsHidden      = PIn.PBool  (table.Rows[i][14].ToString());
				ListLong[i].UsingTIN      = PIn.PBool  (table.Rows[i][15].ToString());
				//ListLong[i].BlueCrossID = PIn.PString(table.Rows[i][16].ToString());
				ListLong[i].SigOnFile     = PIn.PBool  (table.Rows[i][17].ToString());
				ListLong[i].Password      = PIn.PString(table.Rows[i][18].ToString());
				ListLong[i].UserName      = PIn.PString(table.Rows[i][19].ToString());
				ListLong[i].MedicaidID    = PIn.PString(table.Rows[i][20].ToString());
				ListLong[i].OutlineColor  = Color.FromArgb(PIn.PInt(table.Rows[i][21].ToString()));
				ListLong[i].SchoolClassNum= PIn.PInt   (table.Rows[i][22].ToString());
				if(!ListLong[i].IsHidden) AL.Add(ListLong[i]);	
			}
			List=new Provider[AL.Count];
			AL.CopyTo(List);
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE provider SET "
				+ "abbr = '"          +POut.PString(Cur.Abbr)+"'"
				+",itemorder = '"     +POut.PInt   (Cur.ItemOrder)+"'"
				+",lname = '"         +POut.PString(Cur.LName)+"'"
				+",fname = '"         +POut.PString(Cur.FName)+"'"
				+",mi = '"            +POut.PString(Cur.MI)+"'"
				+",suffix = '"        +POut.PString(Cur.Suffix)+"'"
				+",feesched = '"      +POut.PInt   (Cur.FeeSched)+"'"
				+",specialty = '"     +POut.PInt   ((int)Cur.Specialty)+"'"
				+",ssn = '"           +POut.PString(Cur.SSN)+"'"
				+",statelicense = '"  +POut.PString(Cur.StateLicense)+"'"
				+",deanum = '"        +POut.PString(Cur.DEANum)+"'"
				+",issecondary = '"   +POut.PBool  (Cur.IsSecondary)+"'"
				+",provcolor = '"     +POut.PInt   (Cur.ProvColor.ToArgb())+"'"
				+",ishidden = '"      +POut.PBool  (Cur.IsHidden)+"'"
				+",usingtin = '"      +POut.PBool  (Cur.UsingTIN)+"'"
				//+",bluecrossid = '" +POut.PString(Cur.BlueCrossID)+"'"
				+",sigonfile = '"     +POut.PBool  (Cur.SigOnFile)+"'"
				+",password = '"      +POut.PString(Cur.Password)+"'"
				+",username = '"      +POut.PString(Cur.UserName)+"'"
				+",medicaidid = '"    +POut.PString(Cur.MedicaidID)+"'"
				+",OutlineColor = '"  +POut.PInt   (Cur.OutlineColor.ToArgb())+"'"
				+",SchoolClassNum = '"+POut.PInt   (Cur.SchoolClassNum)+"'"
				+" WHERE provnum = '" +POut.PInt(Cur.ProvNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO provider (abbr,itemorder,lname,fname,mi,suffix,"
				+"feesched,specialty,ssn,statelicense,deanum,issecondary,"
				+"provcolor,ishidden,usingtin,sigonfile,password,username"
				+",medicaidid,OutlineColor,SchoolClassNum) VALUES("
				+"'"+POut.PString(Cur.Abbr)+"', "
				+"'"+POut.PInt   (Cur.ItemOrder)+"', "
				+"'"+POut.PString(Cur.LName)+"', "
				+"'"+POut.PString(Cur.FName)+"', "
				+"'"+POut.PString(Cur.MI)+"', "
				+"'"+POut.PString(Cur.Suffix)+"', "
				+"'"+POut.PInt   (Cur.FeeSched)+"', "
				+"'"+POut.PInt   ((int)Cur.Specialty)+"', "
				+"'"+POut.PString(Cur.SSN)+"', "
				+"'"+POut.PString(Cur.StateLicense)+"', "
				+"'"+POut.PString(Cur.DEANum)+"', "
				+"'"+POut.PBool  (Cur.IsSecondary)+"', "
				+"'"+POut.PInt   (Cur.ProvColor.ToArgb())+"', "
				+"'"+POut.PBool  (Cur.IsHidden)+"', "
				+"'"+POut.PBool  (Cur.UsingTIN)+"', "
				//+"'"+POut.PString(Cur.BlueCrossID)+"', "
				+"'"+POut.PBool  (Cur.SigOnFile)+"', "
				+"'"+POut.PString(Cur.Password)+"', "			  
				+"'"+POut.PString(Cur.UserName)+"', "
				+"'"+POut.PString(Cur.MedicaidID)+"', "
				+"'"+POut.PInt   (Cur.OutlineColor.ToArgb())+"', "
				+"'"+POut.PInt   (Cur.SchoolClassNum)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.ProvNum=InsertID;
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from provider WHERE provnum = '"+Cur.ProvNum.ToString()+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static string GetAbbr(int provNum){
			string retStr="";
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ProvNum==provNum){
					retStr=ListLong[i].Abbr;
				}
			}
			return retStr;
		}

		///<summary>Used in the HouseCalls bridge</summary>
		public static string GetLName(int provNum){
			string retStr="";
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ProvNum==provNum){
					retStr=ListLong[i].LName;
				}
			}
			return retStr;
		}

		///<summary></summary>
		public static Color GetColor(int provNum){
			Color retCol=Color.White;
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ProvNum==provNum){
					retCol=ListLong[i].ProvColor;
				}
			}
			return retCol;
		}

		///<summary></summary>
		public static Color GetOutlineColor(int provNum){
			Color retCol=Color.Black;
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ProvNum==provNum){
					retCol=ListLong[i].OutlineColor;
				}
			}
			return retCol;
		}

		///<summary></summary>
		public static bool GetIsSec(int provNum){
			bool retVal=false;
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ProvNum==provNum){
					retVal=ListLong[i].IsSecondary;
				}
			}
			return retVal;
		}

		///<summary></summary>
		public static int GetIndexLong(int provNum){
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ProvNum==provNum){
					return i;
				}
			}
			return -1;//should NEVER return a -1
		}

		///<summary></summary>
		public static int GetIndex(int provNum){
			//Gets the index of the provider in short list (visible providers)
			for(int i=0;i<List.Length;i++){
				if(List[i].ProvNum==provNum){
					return i;
				}
			}
			return -1;
		}

		///<summary></summary>
		public static void MoveUp(){
			if(Selected<0){
				MessageBox.Show(Lan.g("Providers","Please select a provider first."));
				return;
			}
			if(Selected==0){
				return;
			}
			SetOrder(Selected-1,ListLong[Selected].ItemOrder);
			SetOrder(Selected,ListLong[Selected].ItemOrder-1);
			Selected-=1;
		}

		///<summary></summary>
		public static void MoveDown(){
			if(Selected<0){
				MessageBox.Show(Lan.g("Providers","Please select a provider first."));
				return;
			}
			if(Selected==ListLong.Length-1){
				return;
			}
			SetOrder(Selected+1,ListLong[Selected].ItemOrder);
			SetOrder(Selected,ListLong[Selected].ItemOrder+1);
			Selected+=1;
		}

		///<summary></summary>
		public static void SetOrder(int mySelNum, int myItemOrder){
			Provider temp=ListLong[mySelNum];
			temp.ItemOrder=myItemOrder;
			Cur=temp;
			UpdateCur();
		}
	}
	
	

}










