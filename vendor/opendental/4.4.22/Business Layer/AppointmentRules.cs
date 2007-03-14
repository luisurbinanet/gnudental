using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>For now, the rule is simple. It simply blocks all double booking of the specified code range.  The double booking would have to be for the same provider.  This can later be extended to provide more complex rules, such as partial double booking, time limitations, etc.</summary>
	public class AppointmentRule{
		///<summary>Primary key.</summary>
		public int AppointmentRuleNum;
		///<summary>The description of the rule which will be displayed to the user.</summary>
		public string RuleDesc;
		///<summary>The procedure code of the start of the range.</summary>
		public string ADACodeStart;
		///<summary>The procedure code of the end of the range.</summary>
		public string ADACodeEnd;
		///<summary>Usually true.  But this does allow you to turn off a rule temporarily without losing the settings.</summary>
		public bool IsEnabled;

		///<summary>Returns a copy of this AppointmentRule.</summary>
		public AppointmentRule Copy(){
			AppointmentRule a=new AppointmentRule();
			a.AppointmentRuleNum=AppointmentRuleNum;
			a.RuleDesc=RuleDesc;
			a.ADACodeStart=ADACodeStart;
			a.ADACodeEnd=ADACodeEnd;
			a.IsEnabled=IsEnabled;
			return a;
		}

		///<summary></summary>
		public void Insert(){
			string command= "INSERT INTO appointmentrule (RuleDesc,ADACodeStart,ADACodeEnd,IsEnabled) VALUES("
				+"'"+POut.PString(RuleDesc)+"', "
				+"'"+POut.PString(ADACodeStart)+"', "
				+"'"+POut.PString(ADACodeEnd)+"', "
				+"'"+POut.PBool  (IsEnabled)+"')";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			AppointmentRuleNum=dcon.InsertID;
		}

		///<summary></summary>
		public void Update(){
			string command= "UPDATE appointmentrule SET " 
				+ "RuleDesc = '"      +POut.PString(RuleDesc)+"'"
				+ ",ADACodeStart = '" +POut.PString(ADACodeStart)+"'"
				+ ",ADACodeEnd = '"   +POut.PString(ADACodeEnd)+"'"
				+ ",IsEnabled = '"    +POut.PBool  (IsEnabled)+"'"
				+" WHERE AppointmentRuleNum = '" +POut.PInt   (AppointmentRuleNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Delete(){
			string command="DELETE FROM appointmentrule" 
				+" WHERE AppointmentRuleNum = "+POut.PInt(AppointmentRuleNum);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}


    
	}

	/*================================================================================================================
	======================================= class AppointmentRules =================================================*/
	///<summary></summary>
	public class AppointmentRules{
		///<summary></summary>
		public static AppointmentRule[] List;

		///<summary>Fills List with all AppointmentRules.</summary>
		public static void Refresh(){
			string command="SELECT * FROM appointmentrule";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			List=new AppointmentRule[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new AppointmentRule();
				List[i].AppointmentRuleNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].RuleDesc           = PIn.PString(table.Rows[i][1].ToString());
				List[i].ADACodeStart       = PIn.PString(table.Rows[i][2].ToString());
				List[i].ADACodeEnd         = PIn.PString(table.Rows[i][3].ToString());
				List[i].IsEnabled          = PIn.PBool  (table.Rows[i][4].ToString());
			}
		}

		///<summary>Whenever an appointment is scheduled, the procedures which would be double booked are calculated.  In this method, those procedures are checked to see if the double booking should be blocked.  If double booking is indeed blocked, then a separate function will tell the user which category.</summary>
		public static bool IsBlocked(ArrayList adaCodes){
			for(int j=0;j<adaCodes.Count;j++){
				for(int i=0;i<List.Length;i++){
					if(!List[i].IsEnabled){
						continue;
					}
					if(String.Compare((string)adaCodes[j],List[i].ADACodeStart) < 0){
						continue;
					}
					if(String.Compare((string)adaCodes[j],List[i].ADACodeEnd) > 0) {
						continue;
					}
					return true;
				}
			}
			return false;
		}

		///<summary>Whenever an appointment is blocked from being double booked, this method will tell the user which category.</summary>
		public static string GetBlockedDescription(ArrayList adaCodes){
			for(int j=0;j<adaCodes.Count;j++) {
				for(int i=0;i<List.Length;i++) {
					if(!List[i].IsEnabled) {
						continue;
					}
					if(String.Compare((string)adaCodes[j],List[i].ADACodeStart) < 0) {
						continue;
					}
					if(String.Compare((string)adaCodes[j],List[i].ADACodeEnd) > 0) {
						continue;
					}
					return List[i].RuleDesc;
				}
			}
			return "";
		}

		
		

	}
	


}













