using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>A provider is usually a dentist or a hygienist.  But a provider might also be a denturist, a dental student, or a dental hygiene student.  A provider might also be a 'dummy', used only for billing purposes or for notes in the Appointments module.  There is no limit to the number of providers that can be added.</summary>
	public class Provider{
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
		///<summary>FK to Definition.DefNum.</summary>
		public int FeeSched;
		///<summary>Enum:DentalSpecialty</summary>
		public DentalSpecialty Specialty;
		///<summary>or TIN.  No punctuation</summary>
		public string SSN;
		///<summary>can include punctuation</summary>
		public string StateLicense;
		///<summary>.</summary>
		public string DEANum;
		///<summary>True if hygienist.</summary>
		public bool IsSecondary;//
		///<summary>Color that shows in appointments</summary>
		public Color ProvColor;
		///<summary>If true, provider will not show on any lists</summary>
		public bool IsHidden;
		///<summary>True if the SSN field is actually a Tax ID Num</summary>
		public bool UsingTIN;
		///<summary>No longer used since each state assigns a different ID.  Use the providerident instead which allows you to assign a different BCBS ID for each Payor ID.</summary>
		public string BlueCrossIDOld;
		///<summary>Signature on file.</summary>
		public bool SigOnFile;
		///<summary>.</summary>
		public string MedicaidID;
		///<summary>Color that shows in appointments as outline when highlighted.</summary>
		public Color OutlineColor;
		///<summary>FK to schoolclass.SchoolClassNum Used in dental schools.  Each student is a provider.  This keeps track of which class they are in.</summary>
		public int SchoolClassNum;
		///<summary>Used for Canadian claims right now, and will be required in US within a year or two.  Goes out on e-claims if avaialable.</summary>
		public string NationalProvID;

		///<summary>Returns a copy of this Provider.</summary>
		public Provider Copy(){
			Provider p=new Provider();
			p.ProvNum=ProvNum;
			p.Abbr=Abbr;
			p.ItemOrder=ItemOrder;
			p.LName=LName;
			p.FName=FName;
			p.MI=MI;
			p.Suffix=Suffix;
			p.FeeSched=FeeSched;
			p.Specialty=Specialty;
			p.SSN=SSN;
			p.StateLicense=StateLicense;
			p.DEANum=DEANum;
			p.IsSecondary=IsSecondary;
			p.ProvColor=ProvColor;
			p.IsHidden=IsHidden;
			p.UsingTIN=UsingTIN;
			//bluecross
			p.SigOnFile=SigOnFile;
			p.MedicaidID=MedicaidID;
			p.OutlineColor=OutlineColor;
			p.SchoolClassNum=SchoolClassNum;
			p.NationalProvID=NationalProvID;
			return p;
		}

		///<summary></summary>
		private void Update(){
			string command="UPDATE provider SET "
				+ "Abbr = '"          +POut.PString(Abbr)+"'"
				+",ItemOrder = '"     +POut.PInt   (ItemOrder)+"'"
				+",LName = '"         +POut.PString(LName)+"'"
				+",FName = '"         +POut.PString(FName)+"'"
				+",MI = '"            +POut.PString(MI)+"'"
				+",Suffix = '"        +POut.PString(Suffix)+"'"
				+",FeeSched = '"      +POut.PInt   (FeeSched)+"'"
				+",Specialty = '"     +POut.PInt   ((int)Specialty)+"'"
				+",SSN = '"           +POut.PString(SSN)+"'"
				+",StateLicense = '"  +POut.PString(StateLicense)+"'"
				+",DEANum = '"        +POut.PString(DEANum)+"'"
				+",IsSecondary = '"   +POut.PBool  (IsSecondary)+"'"
				+",ProvColor = '"     +POut.PInt   (ProvColor.ToArgb())+"'"
				+",IsHidden = '"      +POut.PBool  (IsHidden)+"'"
				+",UsingTIN = '"      +POut.PBool  (UsingTIN)+"'"
				//+",bluecrossid = '" +POut.PString(BlueCrossID)+"'"
				+",SigOnFile = '"     +POut.PBool  (SigOnFile)+"'"
				+",MedicaidID = '"    +POut.PString(MedicaidID)+"'"
				+",OutlineColor = '"  +POut.PInt   (OutlineColor.ToArgb())+"'"
				+",SchoolClassNum = '"+POut.PInt   (SchoolClassNum)+"'"
				+",NationalProvID = '"+POut.PString(NationalProvID)+"'"
				+" WHERE provnum = '" +POut.PInt(ProvNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		private void Insert(){
			string command= "INSERT INTO provider (Abbr,ItemOrder,LName,FName,MI,Suffix,"
				+"FeeSched,Specialty,SSN,StateLicense,DEANum,IsSecondary,"
				+"ProvColor,IsHidden,UsingTIN,SigOnFile"
				+",MedicaidID,OutlineColor,SchoolClassNum,NationalProvID) VALUES("
				+"'"+POut.PString(Abbr)+"', "
				+"'"+POut.PInt   (ItemOrder)+"', "
				+"'"+POut.PString(LName)+"', "
				+"'"+POut.PString(FName)+"', "
				+"'"+POut.PString(MI)+"', "
				+"'"+POut.PString(Suffix)+"', "
				+"'"+POut.PInt   (FeeSched)+"', "
				+"'"+POut.PInt   ((int)Specialty)+"', "
				+"'"+POut.PString(SSN)+"', "
				+"'"+POut.PString(StateLicense)+"', "
				+"'"+POut.PString(DEANum)+"', "
				+"'"+POut.PBool  (IsSecondary)+"', "
				+"'"+POut.PInt   (ProvColor.ToArgb())+"', "
				+"'"+POut.PBool  (IsHidden)+"', "
				+"'"+POut.PBool  (UsingTIN)+"', "
				//+"'"+POut.PString(BlueCrossID)+"', "
				+"'"+POut.PBool  (SigOnFile)+"', "
				+"'"+POut.PString(MedicaidID)+"', "
				+"'"+POut.PInt   (OutlineColor.ToArgb())+"', "
				+"'"+POut.PInt   (SchoolClassNum)+"', "
				+"'"+POut.PString(NationalProvID)+"')";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			ProvNum=dcon.InsertID;
		}

		///<summary></summary>
		public void InsertOrUpdate(bool IsNew){
			//if(){
				//throw new ApplicationException(Lan.g(this,""));
			//}
			if(IsNew){
				Insert();
			}
			else{
				Update();
			}
		}

		///<summary>Only used from FormProvEdit if user clicks cancel before finishing entering a new provider.</summary>
		public void Delete(){
			string command="DELETE from provider WHERE provnum = '"+ProvNum.ToString()+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

	}

	/*=========================================================================================
	=================================== class Providers ==========================================*/

	///<summary></summary>
	public class Providers{
		///<summary>Rarely used. Includes all providers, even if hidden.</summary>
		public static Provider[] ListLong;
		///<summary>This is the list used most often. It does not include hidden providers.</summary>
		public static Provider[] List;
		//<summary></summary>
		//public static Provider Cur;
		///<summary>This should be eliminated when time.  It's just used in FormProviderSelect to keep track of which provider is highlighted.</summary>
		public static int Selected;

		///<summary>Refreshes List with all providers.</summary>
		public static void Refresh(){
			ArrayList AL=new ArrayList();
			string command="SELECT * from provider ORDER BY itemorder";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			ListLong=new Provider[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				ListLong[i]=new Provider();
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
				ListLong[i].MedicaidID    = PIn.PString(table.Rows[i][18].ToString());
				ListLong[i].OutlineColor  = Color.FromArgb(PIn.PInt(table.Rows[i][19].ToString()));
				ListLong[i].SchoolClassNum= PIn.PInt   (table.Rows[i][20].ToString());
				ListLong[i].NationalProvID= PIn.PString(table.Rows[i][21].ToString());
				if(!ListLong[i].IsHidden) AL.Add(ListLong[i]);	
			}
			List=new Provider[AL.Count];
			AL.CopyTo(List);
		}

		///<summary></summary>
		public static string GetAbbr(int provNum){
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ProvNum==provNum){
					return ListLong[i].Abbr;
				}
			}
			return "";
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
		public static Provider GetProv(int provNum) {
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].ProvNum==provNum) {
					return ListLong[i].Copy();
				}
			}
			return null;
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

		///<summary>Used by MoveUp and MoveDown.</summary>
		private static void SetOrder(int mySelNum, int myItemOrder){
			Provider temp=ListLong[mySelNum];
			temp.ItemOrder=myItemOrder;
			temp.InsertOrUpdate(false);
		}
	}
	
	

}










