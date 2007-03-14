using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the usergroup table in database.</summary>
	public class UserGroup{
		///<summary>Primary key.</summary>
		public int UserGroupNum;
		///<summary></summary>
		public string Description;

		///<summary></summary>
		public UserGroup Copy(){
			UserGroup u=new UserGroup();
			u.UserGroupNum=UserGroupNum;
			u.Description=Description;
			return u;
		}

		///<summary></summary>
		private void Update(){
			string command= "UPDATE usergroup SET " 
				+"Description = '"  +POut.PString(Description)+"'"
				+" WHERE UserGroupNum = '"+POut.PInt(UserGroupNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		private void Insert(){
			string command= "INSERT INTO usergroup (Description) VALUES("
				+"'"+POut.PString(Description)+"')";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			UserGroupNum=dcon.InsertID;
		}

		///<summary></summary>
		public void InsertOrUpdate(bool isNew){
			//if(){
			//	throw new Exception(Lan.g(this,""));
			//}
			if(isNew){
				Insert();
			}
			else{
				Update();
			}
		}

		///<summary>Checks for dependencies first</summary>
		public void Delete(){
			string command="SELECT COUNT(*) FROM user WHERE UserGroupNum='"
				+POut.PInt(UserGroupNum)+"'";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			if(table.Rows[0][0].ToString()!="0"){
				throw new Exception(Lan.g(this,"Must move users to another group first."));
			}
			command= "DELETE from usergroup WHERE UserGroupNum='"
				+POut.PInt(UserGroupNum)+"'";
 			dcon.NonQ(command);
		}

	}

	/*=========================================================================================
	=================================== class UserGroups==========================================*/
	///<summary></summary>
	public class UserGroups{
		///<summary>A list of all users.</summary>
		public static UserGroup[] List;   

		///<summary></summary>
		public static void Refresh(){
			string command="SELECT * from usergroup ORDER BY Description";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			List=new UserGroup[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new UserGroup();
				List[i].UserGroupNum  = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description   = PIn.PString(table.Rows[i][1].ToString());
			}
		}

		///<summary></summary>
		public static UserGroup GetGroup(int userGroupNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].UserGroupNum==userGroupNum){
					return List[i].Copy();
				}
			}
			return null;
		}

		

	}
 
	

	
}













