using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the electid table in the database. Never editable by users.  We keep this table updated with new numbers as part of upgrades, so NEVER edit this table as it will mess up our primary keys.  Helps with entering elecronic/payor id's as well as keeping track of the specific carrier requirements. Only used by the X12 format.</summary>
	public class ElectID{
		///<summary>Primary key.</summary>
		public int ElectIDNum;
		///<summary>aka Electronic ID.  A simple string.</summary>
		public string PayorID;
		///<summary>As published. Not editable. Used when doing a search.</summary>
		public string CarrierName;
		///<summary>True if medicaid. Then, the billing and treating providers will have their Medicaid ID's attached.</summary>
		public bool IsMedicaid;
		///<summary>Integers separated by commas. Each int represents a ProviderSupplementalID type that is required by this insurance. Usually only used for BCBS or other carriers that require supplemental provider id's.  Even if we don't put the supplemental types in here, the user can still add them.  This just helps by doing an additional check for known required types.</summary>
		public string ProviderTypes;
		///<summary>Any comments. Usually includes enrollment requirements and descriptions of how to use the provider id's supplied by the carrier because they might call them by different names.</summary>
		public string Comments;


	}

	/*=========================================================================================
	=================================== class ElectIDs ======================================*/

	///<summary>Refreshed with local data.</summary>
	public class ElectIDs{
		///<summary>This is the list of all electronic IDs.</summary>
		public static ElectID[] List;

		///<summary></summary>
		public static void Refresh(){
			string command="SELECT * from electid "
				+"ORDER BY CarrierName";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			List=new ElectID[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new ElectID();
				List[i].ElectIDNum   = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PayorID      = PIn.PString(table.Rows[i][1].ToString());
				List[i].CarrierName  = PIn.PString(table.Rows[i][2].ToString());
				List[i].IsMedicaid   = PIn.PBool  (table.Rows[i][3].ToString());
				List[i].ProviderTypes= PIn.PString(table.Rows[i][4].ToString());
				List[i].Comments     = PIn.PString(table.Rows[i][5].ToString());
			}
		}

		///<summary></summary>
		public static ProviderSupplementalID[] GetRequiredIdents(string payorID){
			ElectID electID=GetID(payorID);
			if(electID==null){
				return new ProviderSupplementalID[0];
			}
			if(electID.ProviderTypes==""){
				return new ProviderSupplementalID[0];
			}
			string[] provTypes=electID.ProviderTypes.Split(',');
			if(provTypes.Length==0){
				return new ProviderSupplementalID[0];
			}
			ProviderSupplementalID[] retVal=new ProviderSupplementalID[provTypes.Length];
			for(int i=0;i<provTypes.Length;i++){
				retVal[i]=(ProviderSupplementalID)(Convert.ToInt32(provTypes[i]));
			}
			/*
			if(electID=="SB601"){//BCBS of GA
				retVal=new ProviderSupplementalID[2];
				retVal[0]=ProviderSupplementalID.BlueShield;
				retVal[1]=ProviderSupplementalID.SiteNumber;
			}*/
			return retVal;
		}

		///<summary>Gets ONE ElectID that uses the supplied payorID. Even if there are multiple payors using that ID.  So use this carefully.</summary>
		public static ElectID GetID(string payorID){
			ArrayList electIDs=GetIDs(payorID);
			if(electIDs.Count==0){
				return null;
			}
			return (ElectID)electIDs[0];//simply return the first one we encounter
		}

		///<summary>Gets an arrayList of ElectID objects based on a supplied payorID. If no matches found, then returns array of 0 length. Used to display payors in FormInsPlan and also to get required idents.  This means that all payors with the same ID should have the same required idents and notes.</summary>
		public static ArrayList GetIDs(string payorID){
			ArrayList retVal=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].PayorID==payorID){
					retVal.Add(List[i]);
				}
			}
			return retVal;
		}

		///<summary>Gets the names of the payors to display based on the payorID.  Since carriers sometimes share payorIDs, there will often be multiple payor names returned.</summary>
		public static string[] GetDescripts(string payorID){
			if(payorID==""){
				return new string[]{};
			}
			ArrayList electIDs=GetIDs(payorID);
			string[] retVal=new string[electIDs.Count];
			for(int i=0;i<retVal.Length;i++){
				retVal[i]=((ElectID)electIDs[i]).CarrierName;
			}
			return retVal;
		}

		/*
		///<summary></summary>
		public static ProviderIdent[] GetForProv(int provNum){
			ArrayList arrayL=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ProvNum==provNum){
					arrayL.Add(List[i]);
				}
			}
			ProviderIdent[] ForProv=new ProviderIdent[arrayL.Count];
			for(int i=0;i<arrayL.Count;i++){
				ForProv[i]=(ProviderIdent)arrayL[i];
			}
			return ForProv;
		}*/



	
	}
	
	

}










