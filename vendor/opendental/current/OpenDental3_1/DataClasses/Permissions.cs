using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the permission table in the database.</summary>
	public struct Permission{
		///<summary>Primary key.</summary>
		public int PermissionNum;
		///<summary>Description.  Not user editable.</summary>
		public string Name;
		///<summary>If true, will display user/password dialog for that feature.</summary>
		public bool RequiresPassword;
		///<summary>Only displays user/password dialog if item date is before this date.</summary>
		public DateTime BeforeDate;
		///<summary>Can be -1 for "do not calculate days". 0 days=always require password. Only displays user/password dialog if item is before given number of days</summary>
		public int BeforeDays;//
	}
	
	/*=========================================================================================
	=================================== class Permissions ==========================================*/
  
	///<summary></summary>
	public class Permissions:DataClass{
		///<summary></summary>
		public static Permission Cur;
		///<summary></summary>
		public static Permission[] List;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from permission ORDER BY Name";
			FillTable();
			List=new Permission[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i].PermissionNum  = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Name           = PIn.PString(table.Rows[i][1].ToString());
				List[i].RequiresPassword= PIn.PBool  (table.Rows[i][2].ToString());	
				List[i].BeforeDate     = PIn.PDate  (table.Rows[i][3].ToString());
				List[i].BeforeDays     = PIn.PInt   (table.Rows[i][4].ToString());
			}
		}

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
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE permission SET "
				+"name ='"              +POut.PString(Cur.Name)+"'"
				+",requirespassword ='" +POut.PBool  (Cur.RequiresPassword)+"'"
				+",beforedate='"        +POut.PDate  (Cur.BeforeDate)+"'"
				+",beforedays ='"       +POut.PInt   (Cur.BeforeDays)+"'"
				+" WHERE permissionnum = '"+POut.PInt(Cur.PermissionNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static bool GetCur(string permissionName){
			//used in Security log entry and UserPermissions.CheckUserPassword
			for(int i=0;i<List.Length;i++){
				if(List[i].Name==permissionName){
					Cur=List[i];
					return true;
				}
			}
			return false;
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

		///<summary></summary>
		public static void DisableSecurity(){
			cmd.CommandText="UPDATE permission SET RequiresPassword = '0' "
				+"WHERE Name = 'Security Administration'";
			NonQ(false);
			Refresh();
		}

		///<summary></summary>
		public static void SetAll(bool doRequirePass){
			if(doRequirePass){
				cmd.CommandText="UPDATE permission SET RequiresPassword = '1'";
			}
			else{
				cmd.CommandText="UPDATE permission SET RequiresPassword = '0'";
			}
			NonQ(false);
		}

	}

	

	


}










