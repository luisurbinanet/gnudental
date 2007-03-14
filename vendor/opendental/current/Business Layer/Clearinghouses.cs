using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Since we can send e-claims to multiple clearinghouses, this table keeps track of each clearinghouse.  Will eventually be used for individual carriers as well if they accept </summary>
	public class Clearinghouse{
		///<summary>Primary key.</summary>
		public int ClearinghouseNum;
		///<summary>Description of this clearinghouse</summary>
		public string Description;
		///<summary>The path to export the X12 file to. Includes \.</summary>
		public string ExportPath;
		///<summary>Set to true if this is the default clearinghouse to which you want most of your e-claims sent.</summary>
		public bool IsDefault;
		///<summary>A list of all payors which should have claims sent to this clearinghouse. Comma delimited with no spaces.  Not necessary if IsDefault.</summary>
		public string Payors;
		///<summary>Enum:ElectronicClaimFormat The format of the file that gets sent electronically.</summary>
		public ElectronicClaimFormat Eformat;
		///<summary>The ID of the clearinghouse. Provided by them. Usually goes on ISA08. Examples: BCBSGA or 0135WCH00(webMD)</summary>
		public string ReceiverID;
		///<summary>Not used anymore since Open Dental is always the sender.  The dental office will be identified by provider rather than by sender.</summary>
		public string SenderIDold;
		///<summary>Password is usually combined with the login ID for user validation.</summary>
		public string Password;
		///<summary>The path that all incoming response files will be saved to. Includes \.</summary>
		public string ResponsePath;
		///<summary>Enum:EclaimsCommBridge  One of the included hard-coded communications briges.  Or none to just create the claim files without uploading.</summary>
		public EclaimsCommBridge CommBridge;
		///<summary>If applicable, this is the name of the client program to launch.  It is even used by the hard-coded comm bridges, because the user may have changed the installation directory or exe name.</summary>
		public string ClientProgram;
		///<summary>Each clearinghouse increments their batch numbers by one each time a claim file is sent.  User never sees this number.  Maxes out at 999, then loops back to 1.  This field is skipped during all update and retreival except if specifically looking for this one field.  Defaults to 0 for brand new clearinghouses, which causes the first batch to go out as #1.</summary>
		public int LastBatchNumber;
		///<summary>1,2,3,or 4. The port that the modem is connected to if applicable. Always uses 9600 baud and standard settings. Will crash if port or modem not valid.</summary>
		public int ModemPort;
		///<summary>A clearinghouse usually has a login ID that is used with the password in order to access the remote server.  This value is not usualy used within the actual claim.</summary>
		public string LoginID;

		/*//<summary>Returns a copy of the clearinghouse.</summary>
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
			return cf;
		}*/

		///<summary>Inserts this clearinghouse into database.</summary>
		public void Insert(){
			string command="INSERT INTO clearinghouse (Description,ExportPath,IsDefault,Payors"
				+",Eformat,ReceiverID,Password,ResponsePath,CommBridge,ClientProgram,"
				+"ModemPort,LoginID) VALUES("
				+"'"+POut.PString(Description)+"', "
				+"'"+POut.PString(ExportPath)+"', "
				+"'"+POut.PBool  (IsDefault)+"', "
				+"'"+POut.PString(Payors)+"', "
				+"'"+POut.PInt   ((int)Eformat)+"', "
				+"'"+POut.PString(ReceiverID)+"', "
				//+"'"+POut.PString(SenderID)+"', "
				+"'"+POut.PString(Password)+"', "
				+"'"+POut.PString(ResponsePath)+"', "
				+"'"+POut.PInt   ((int)CommBridge)+"', "
				+"'"+POut.PString(ClientProgram)+"', "
				//LastBatchNumber
				+"'"+POut.PInt   (ModemPort)+"', "
				+"'"+POut.PString(LoginID)+"')";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
			//ClaimFormNum=dcon.InsertID;
		}

		///<summary></summary>
		public void Update(){
			string command="UPDATE clearinghouse SET "
				+"Description = '"  +POut.PString(Description)+"' "
				+",ExportPath = '"  +POut.PString(ExportPath)+"' "
				+",IsDefault = '"   +POut.PBool  (IsDefault)+"' "
				+",Payors = '"      +POut.PString(Payors)+"' "
				+",Eformat = '"     +POut.PInt   ((int)Eformat)+"' "
				+",ReceiverID = '"  +POut.PString(ReceiverID)+"' "
				//+",SenderID = '"    +POut.PString(SenderID)+"' "
				+",Password = '"    +POut.PString(Password)+"' "
				+",ResponsePath = '"+POut.PString(ResponsePath)+"' "
				+",CommBridge = '"  +POut.PInt   ((int)CommBridge)+"' "
				+",ClientProgram ='"+POut.PString(ClientProgram)+"' "
				//LastBatchNumber
				+",ModemPort ='"    +POut.PInt   (ModemPort)+"' "
				+",LoginID ='"      +POut.PString(LoginID)+"' "
				+"WHERE ClearinghouseNum = '"+POut.PInt   (ClearinghouseNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Delete(){
			string command="DELETE FROM clearinghouse "
				+"WHERE ClearinghouseNum = '"+POut.PInt(ClearinghouseNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary>Gets the last batch number for this clearinghouse and increments it by one.  Saves the new value, then returns it.  So even if the new value is not used for some reason, it will have already been incremented. Remember that LastBatchNumber is never accurate with local data in memory.</summary>
		public int GetNextBatchNumber(){
			//get last batch number
			string command="SELECT LastBatchNumber FROM clearinghouse "
				+"WHERE ClearinghouseNum = "+POut.PInt(ClearinghouseNum);
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			int batchNum=PIn.PInt(table.Rows[0][0].ToString());
			//and increment it by one
			if(batchNum==999)
				batchNum=1;
			else
				batchNum++;
			//save the new batch number. Even if user cancels, it will have incremented.
			command="UPDATE clearinghouse SET LastBatchNumber="+batchNum.ToString()
				+" WHERE ClearinghouseNum = "+POut.PInt(ClearinghouseNum);
			dcon.NonQ(command);
			return batchNum;
		}

		
	}

/*=========================================================================================
		=================================== class Clearinghouses=======================================*/

	///<summary></summary>
	public class Clearinghouses{
		///<summary>List of all clearinghouses.</summary>
		public static Clearinghouse[] List;
		///<summary>Key=PayorID. Value=ClearinghouseNum.</summary>
		private static Hashtable HList;

		///<summary></summary>
		public static void Refresh(){
			string command=
				"SELECT * FROM clearinghouse";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			List=new Clearinghouse[table.Rows.Count];
			HList=new Hashtable();
			string[] payors;
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Clearinghouse();
				List[i].ClearinghouseNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description     = PIn.PString(table.Rows[i][1].ToString());
				List[i].ExportPath      = PIn.PString(table.Rows[i][2].ToString());
				List[i].IsDefault       = PIn.PBool  (table.Rows[i][3].ToString());
				List[i].Payors          = PIn.PString(table.Rows[i][4].ToString());
				List[i].Eformat         = (ElectronicClaimFormat)PIn.PInt(table.Rows[i][5].ToString());
				List[i].ReceiverID      = PIn.PString(table.Rows[i][6].ToString());
				//List[i].SenderID        = PIn.PString(table.Rows[i][7].ToString());
				List[i].Password        = PIn.PString(table.Rows[i][8].ToString());
				List[i].ResponsePath    = PIn.PString(table.Rows[i][9].ToString());
				List[i].CommBridge      = (EclaimsCommBridge)PIn.PInt(table.Rows[i][10].ToString());
				List[i].ClientProgram   = PIn.PString(table.Rows[i][11].ToString());
				//12: LastBatchNumber
				List[i].ModemPort       = PIn.PInt   (table.Rows[i][13].ToString());
				List[i].LoginID         = PIn.PString(table.Rows[i][14].ToString());
				payors=List[i].Payors.Split(',');
				for(int j=0;j<payors.Length;j++){
					if(!HList.ContainsKey(payors[j])){
						HList.Add(payors[j],List[i].ClearinghouseNum);
					}
				}
			}
		}

		///<summary>Returns the clearinghouseNum for claims for the supplied payorID.  If the payorID was not entered or if no default was set, then 0 is returned.</summary>
		public static int GetNumForPayor(string payorID){
			//this is not done because Renaissance does not require payorID
			//if(payorID==""){
			//	return ElectronicClaimFormat.None;
			//}
			if(payorID!="" && HList.ContainsKey(payorID)){
				return (int)HList[payorID];
			}
			//payorID not found
			Clearinghouse defaultCH=GetDefault();
			if(defaultCH==null){
				return 0;//ElectronicClaimFormat.None;
			}
			return defaultCH.ClearinghouseNum;
		}

		///<summary>Returns the clearinghouse specified by the given num.</summary>
		public static Clearinghouse GetClearinghouse(int clearinghouseNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].ClearinghouseNum==clearinghouseNum){
					return List[i];
				}
			}
			MessageBox.Show("Error. Could not locate Clearinghouse.");
			return null;
		}

		///<summary>Returns the default clearinghouse. If no default present, returns null.</summary>
		public static Clearinghouse GetDefault(){
			for(int i=0;i<List.Length;i++){
				if(List[i].IsDefault){
					return List[i];
				}
			}
			return null;
		}
		
		///<summary></summary>
		public static string GetDescript(int clearinghouseNum){
			if(clearinghouseNum==0){
				return "";
			}
			return GetClearinghouse(clearinghouseNum).Description;
		}

		///<summary>Gets the index of this clearinghouse within List</summary>
		public static int GetIndex(int clearinghouseNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].ClearinghouseNum==clearinghouseNum){
					return i;
				}
			}
			MessageBox.Show("Clearinghouses.GetIndex failed.");
			return -1;
		}

		


	}

	



}









