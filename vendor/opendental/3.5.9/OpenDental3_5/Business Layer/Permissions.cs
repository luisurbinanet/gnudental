using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the permission table in the database.</summary>
	public class Permission{
		///<summary>Primary key.</summary>
		public int PermissionNum;
		///<summary>Description.  Not user editable.</summary>
		public string Name;
		///<summary>If true, will display user/password dialog for that feature.</summary>
		public bool RequiresPassword;
		///<summary>Only displays user/password dialog if item date is before this date.</summary>
		public DateTime BeforeDate;
		///<summary>Can be -1 for "do not calculate days". 0 days=always require password. Only displays user/password dialog if item is before given number of days</summary>
		public int BeforeDays;

		///<summary></summary>
		public Permission Copy(){
			Permission p=new Permission();
			p.PermissionNum=PermissionNum;
			p.Name=Name;
			p.RequiresPassword=RequiresPassword;
			p.BeforeDate=BeforeDate;
			p.BeforeDays=BeforeDays;
			return p;
		}

		/*
		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO permission (Name,RequiresPassword,BeforeDate,BeforeDays) "
				+"VALUES ("
				+"'"+POut.PString(Cur.Name)+"', "
				+"'"+POut.PBool  (Cur.RequiresPassword)+"', "
				+"'"+POut.PDate  (Cur.BeforeDate)+"', "
				+"'"+POut.PInt   (Cur.BeforeDays)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.PermissionNum=InsertID;
		}*/

		///<summary></summary>
		public void Update(){
			string command="UPDATE permission SET "
				//+"name ='"              +POut.PString(Name)+"'"//name not allowed to change
				+"RequiresPassword ='" +POut.PBool  (RequiresPassword)+"'"
				+",BeforeDate='"        +POut.PDate  (BeforeDate)+"'"
				+",BeforeDays ='"       +POut.PInt   (BeforeDays)+"'"
				+" WHERE PermissionNum = '"+POut.PInt(PermissionNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}


	}
	
	/*=========================================================================================
	=================================== class Permissions ==========================================*/
  
	///<summary></summary>
	public class Permissions{
		//<summary></summary>
		//public static Permission Cur;
		///<summary>A list of all permissions.  These have nothing to do with individual users.  They are all hardcoded.</summary>
		public static Permission[] List;

		///<summary></summary>
		public static void Refresh(){
			string command="SELECT * from permission ORDER BY Name";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			List=new Permission[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new Permission();
				List[i].PermissionNum  = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Name           = PIn.PString(table.Rows[i][1].ToString());
				List[i].RequiresPassword= PIn.PBool  (table.Rows[i][2].ToString());	
				List[i].BeforeDate     = PIn.PDate  (table.Rows[i][3].ToString());
				List[i].BeforeDays     = PIn.PInt   (table.Rows[i][4].ToString());
			}
		}

		///<summary></summary>
		public static Permission GetPermission(string permissionName){
			//used in Security log entry and UserPermissions.CheckUserPassword
			for(int i=0;i<List.Length;i++){
				if(List[i].Name==permissionName){
					return List[i].Copy();
				}
			}
			return null;//will never happen
		}

		/*public static bool GetCur(int permissionNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].PermissionNum==permissionNum){
					Cur=List[i];
					return true;
				}
				//if(i==List.Length-1)
				//	MessageBox.Show("error. unexpected permissionnum: "+permissionNum.ToString());
			}
			return false;
		}*/

		///<summary>Need to perform refresh after this.</summary>
		public static void DisableSecurity(){
			string command="UPDATE permission SET RequiresPassword = '0' "
				+"WHERE Name = 'Security Administration'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
			//Refresh();
		}

		///<summary></summary>
		public static void SetAll(bool doRequirePass){
			string command="";
			if(doRequirePass){
				command="UPDATE permission SET RequiresPassword = '1'";
			}
			else{
				command="UPDATE permission SET RequiresPassword = '0'";
			}
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary>Tests to see if this permission requires a password.  If so, the calling class would usually display a user/password dialog.</summary>
		public static bool AuthorizationRequired(string permissionName){
			Permission permission=GetPermission(permissionName);
			if(permission.RequiresPassword){
				return true;
			}
			return false;
		}

		///<summary>Tests to see if this permission requires authorization.  If so, it usually displays a user/password dialog.</summary>
		public static bool AuthorizationRequired(string permissionName,DateTime myDate){
			Permission permission=GetPermission(permissionName);
			bool doCheck=false;
			if(myDate <= permission.BeforeDate){//if date is before specified date
				doCheck=true;
			}
			if(myDate <= DateTime.Today.AddDays(-permission.BeforeDays)){//if date is older than # of days
				doCheck=true;
			}
			if(doCheck){
				return AuthorizationRequired(permissionName);
			}
			return false;//allow access if newer
		}

	}

	

	


}










