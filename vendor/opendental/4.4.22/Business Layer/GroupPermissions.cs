using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Every user group has certain permissions.  This defines a permission for a group.  The absense of permission would cause that row to be deleted from this table.</summary>
	public class GroupPermission{
		///<summary>Primary key.</summary>
		public int GroupPermNum;
		///<summary>Only granted permission if newer than this date.  Can be Minimum (01-01-0001) to always grant permission.</summary>
		public DateTime NewerDate;
		///<summary>Can be 0 to always grant permission.  Otherwise, only granted permission if item is newer than the given number of days.  1 would mean only if entered today.</summary>
		public int NewerDays;
		///<summary>FK to usergroup.UserGroupNum.  The user group for which this permission is granted.  If not authorized, then this groupPermission will have been deleted.</summary>
		public int UserGroupNum;
		///<summary>Enum:Permissions</summary>
		public Permissions PermType;

		///<summary></summary>
		public GroupPermission Copy(){
			GroupPermission g=new GroupPermission();
			g.GroupPermNum=GroupPermNum;
			g.NewerDate=NewerDate;
			g.NewerDays=NewerDays;
			g.UserGroupNum=UserGroupNum;
			g.PermType=PermType;
			return g;
		}

		///<summary></summary>
		private void Update(){
			string command= "UPDATE grouppermission SET " 
				+"NewerDate = '"   +POut.PDate  (NewerDate)+"'"
				+",NewerDays = '"   +POut.PInt   (NewerDays)+"'"
				+",UserGroupNum = '"+POut.PInt   (UserGroupNum)+"'"
				+",PermType = '"    +POut.PInt   ((int)PermType)+"'"
				+" WHERE GroupPermNum = '"+POut.PInt(GroupPermNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		private void Insert(){
			string command= "INSERT INTO grouppermission (NewerDate,NewerDays,UserGroupNum,PermType) "
				+"VALUES("
				+"'"+POut.PDate  (NewerDate)+"', "
				+"'"+POut.PInt   (NewerDays)+"', "
				+"'"+POut.PInt   (UserGroupNum)+"', "
				+"'"+POut.PInt   ((int)PermType)+"')";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public void InsertOrUpdate(bool isNew){
			if(NewerDate.Year>1880 && NewerDays>0){
				throw new Exception(Lan.g(this,"Date or days can be set, but not both."));
			}
			if(!GroupPermissions.PermTakesDates(PermType)){
				if(NewerDate.Year>1880 || NewerDays>0){
					throw new Exception(Lan.g(this,"This type of permission may not have a date or days set."));
				}
			}
			if(isNew){
				Insert();
			}
			else{
				Update();
			}
		}

		/*
		///<summary>Checks for dependencies first</summary>
		public void Delete(){
			//string command="SELECT COUNT(*) FROM user WHERE GroupPermissionNum='"
			//	+POut.PInt(GroupPermissionNum)+"'";
			DataConnection dcon=new DataConnection();
			//DataTable table=dcon.GetTable(command);
			//if(PIn.PInt(table.Rows[0][0].ToString())>0){
			//	throw new Exception(Lan.g(this,"Must move users to another group first."));
			//}
			string command= "DELETE from grouppermission WHERE GroupPermNum='"
				+POut.PInt(GroupPermNum)+"'";
 			dcon.NonQ(command);
		}*/

	}

	/*=========================================================================================
	=================================== class GroupPermissions==========================================*/
	///<summary></summary>
	public class GroupPermissions{
		///<summary>A list of all GroupPermissions for all groups.</summary>
		public static GroupPermission[] List;   

		///<summary></summary>
		public static void Refresh(){
			string command="SELECT * from grouppermission";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			List=new GroupPermission[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new GroupPermission();
				List[i].GroupPermNum  = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].NewerDate     = PIn.PDate  (table.Rows[i][1].ToString());
				List[i].NewerDays     = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].UserGroupNum  = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].PermType      =(Permissions) PIn.PInt   (table.Rows[i][4].ToString());
			}
		}

		/*
		///<summary></summary>
		public static void SetPermission(int groupNum,Permissions permType){
			string command= "INSERT INTO grouppermission (NewerDate,NewerDays,UserGroupNum,PermType) "
				+"VALUES("
				+"'"+POut.PDate  (NewerDate)+"', "
				+"'"+POut.PInt   (NewerDays)+"', "
				+"'"+POut.PInt   (UserGroupNum)+"', "
				+"'"+POut.PInt   ((int)permType)+"')";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			GroupPermNum=dcon.InsertID;
			
		}*/

		///<summary></summary>
		public static void RemovePermission(int groupNum,Permissions permType){
			DataConnection dcon=new DataConnection();
			string command;
			if(permType==Permissions.SecurityAdmin){
				//need to make sure that at least one other user has this permission
				command="SELECT COUNT(*) FROM grouppermission WHERE PermType='"+POut.PInt((int)permType)+"'";
				DataTable table=dcon.GetTable(command);
				if(table.Rows[0][0].ToString()=="1"){//only one, so this would delete the last one.
					throw new Exception(Lan.g("FormSecurity","At least one group must have Security Admin permission."));
				}
			}
			command="DELETE from grouppermission WHERE UserGroupNum='"+POut.PInt(groupNum)+"' "
				+"AND PermType='"+POut.PInt((int)permType)+"'";
 			dcon.NonQ(command);
		}

		///<summary>Gets a GroupPermission based on the supplied userGroupNum and permType.  If not found, then it returns null.  Used in FormSecurity when double clicking on a dated permission or when clicking the all button.</summary>
		public static GroupPermission GetPerm(int userGroupNum,Permissions permType){
			for(int i=0;i<List.Length;i++){
				if(List[i].UserGroupNum==userGroupNum && List[i].PermType==permType){
					return List[i].Copy();
				}
			}
			return null;
		}

		///<summary>Used in Security.IsAuthorized</summary>
		public static bool HasPermission(int userGroupNum,Permissions permType){
			for(int i=0;i<List.Length;i++){
				if(List[i].UserGroupNum!=userGroupNum || List[i].PermType!=permType){
					continue;
				}
				return true;
			}
			return false;
		}

		/*
		///<summary>Must supply a user that has permission for this permission type, so only run AFTER Security.GetAuthorizedUser returns a valid user.  Used for types of permissions that use date limits.  Gets run from the form where the permission is needed.  Date comparisons are based on the server date.</summary>
		public static DateTime GetDateLimit(Permissions permType,int userGroupNum){
			DateTime nowDate=MiscData.GetNowDate();
			DateTime retVal=DateTime.MinValue;
			for(int i=0;i<List.Length;i++){
				if(List[i].UserGroupNum!=userGroupNum || List[i].PermType!=permType){
					continue;
				}
				//this should only happen once.  One match.
				if(List[i].NewerDate.Year>1880){
					retVal=List[i].NewerDate;
				}
				if(List[i].NewerDays==0){//do not restrict by days
					//do not change retVal
				}
				else if(nowDate.AddDays(-List[i].NewerDays)>retVal){
					retVal=nowDate.AddDays(-List[i].NewerDays);
				}
			}
			return retVal;
		}*/

		/*public static GroupPermission[] GetForGroup(int UserGroupNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].GroupPermissionNum==GroupPermissionNum){
					return List[i].Copy();
				}
			}
			return null;
		}*/

		///<summary>Gets the description for the specified permisssion.  Already translated.</summary>
		public static string GetDesc(Permissions perm){
			switch(perm){
				case Permissions.AccountingCreate:
					return Lan.g("enumPermissions","Accounting Create Entry");
				case Permissions.AccountingEdit:
					return Lan.g("enumPermissions","Accounting Edit Entry");
				case Permissions.AccountModule:
					return Lan.g("enumPermissions","Account Module");
				case Permissions.AdjustmentCreate:
					return Lan.g("enumPermissions","Adjustment Create");
				case Permissions.AdjustmentEdit:
					return Lan.g("enumPermissions","Adjustment Edit");
				case Permissions.AppointmentCreate:
					return Lan.g("enumPermissions","Appointment Create");
				case Permissions.AppointmentEdit:
					return Lan.g("enumPermissions","Appointment Edit");
				case Permissions.AppointmentMove:
					return Lan.g("enumPermissions","Appointment Move");
				case Permissions.AppointmentsModule:
					return Lan.g("enumPermissions","Appointments Module");
				case Permissions.Backup:
					return Lan.g("enumPermissions","Backup");
				case Permissions.Blockouts:
					return Lan.g("enumPermissions","Blockouts");
				case Permissions.ChartModule:
					return Lan.g("enumPermissions","Chart Module");
				case Permissions.ChooseDatabase:
					return Lan.g("enumPermissions","Choose Database");
				case Permissions.ClaimsSentEdit:
					return Lan.g("enumPermissions","Claims Sent Edit");
				case Permissions.DepositSlips:
					return Lan.g("enumPermissions","Deposit Slips");
				case Permissions.FamilyModule:
					return Lan.g("enumPermissions","Family Module");
				case Permissions.ImagesModule:
					return Lan.g("enumPermissions","Images Module");
				case Permissions.ManageModule:
					return Lan.g("enumPermissions","Manage Module");
				case Permissions.None:
					return "";
				case Permissions.PaymentCreate:
					return Lan.g("enumPermissions","Payment Create");
				case Permissions.PaymentEdit:
					return Lan.g("enumPermissions","Payment Edit");
				case Permissions.ProcComplCreate:
					return Lan.g("enumPermissions","Create Completed Procedure (or set complete)");
				case Permissions.ProcComplEdit:
					return Lan.g("enumPermissions","Edit Completed Procedure");
				case Permissions.Reports:
					return Lan.g("enumPermissions","Reports");
				case Permissions.RxCreate:
					return Lan.g("enumPermissions","Rx Create");
				case Permissions.Schedules:
					return Lan.g("enumPermissions","Schedules - Practice and Provider");
				case Permissions.SecurityAdmin:
					return Lan.g("enumPermissions","Security Admin");
				case Permissions.Setup:
					return Lan.g("enumPermissions","Setup - Covers a wide variety of setup functions");
				case Permissions.TimecardsEditAll:
					return Lan.g("enumPermissions","Edit All Timecards");
				case Permissions.TPModule:
					return Lan.g("enumPermissions","TreatmentPlan Module");
				case Permissions.UserQuery:
					return Lan.g("enumPermissions","User Query");
			}
			return "";//should never happen
		}

		///<summary></summary>
		public static bool PermTakesDates(Permissions permType){
			if(permType==Permissions.AdjustmentEdit
				|| permType==Permissions.PaymentEdit
				|| permType==Permissions.ProcComplEdit
				|| permType==Permissions.AccountingEdit
				|| permType==Permissions.AccountingCreate//prevents backdating
				|| permType==Permissions.DepositSlips//prevents backdating
				)
			{
				return true;
			}
			return false;
		}
		

	}
 
	

	
}













