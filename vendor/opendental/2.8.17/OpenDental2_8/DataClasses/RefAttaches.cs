using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the refattach table in the database.  Attaches a reference to a patient.</summary>
	public struct RefAttach{  
		///<summary>Primary key.</summary>
		public int RefAttachNum;
		///<summary>Foreign key to referral.ReferralNum.</summary>
		public int ReferralNum;
		///<summary>Foreign key to patient.PatNum.</summary>
		public int PatNum;
		///<summary>Order to display in patient info. Will be automated more in future.</summary>
		public int ItemOrder;
		///<summary>Date of referral.</summary>
		public DateTime RefDate;//
		///<summary>true=from, false=to</summary>
		public bool IsFrom;
	}

	/*================================================================================================
		=================================== class RefAttaches ==========================================*/
///<summary></summary>
	public class RefAttaches:DataClass{
		///<summary></summary>
		public static RefAttach[] List;//for this patient only
		///<summary></summary>
		public static RefAttach Cur;
		///<summary></summary>
		public static Hashtable HList;//key:refAttachNum, value:RefAttach

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * FROM refattach"
				+" WHERE patnum = '"+Patients.Cur.PatNum+"'"
				+" ORDER BY itemorder";
			FillTable();
			List=new RefAttach[table.Rows.Count];
			HList=new Hashtable();
			for(int i=0;i<table.Rows.Count;i++){
				List[i].RefAttachNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ReferralNum = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].PatNum      = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].ItemOrder   = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].RefDate     = PIn.PDate  (table.Rows[i][4].ToString());
				List[i].IsFrom      = PIn.PBool  (table.Rows[i][5].ToString());       
				HList.Add(List[i].RefAttachNum,List[i]);
			}
		}
	
		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE refattach SET " 
				+ "referralnum = '" +POut.PInt   (Cur.ReferralNum)+"'"
				+ ",patnum = '"     +POut.PInt   (Cur.PatNum)+"'"
				+ ",itemorder = '"  +POut.PInt   (Cur.ItemOrder)+"'"
				+ ",refdate = '"    +POut.PDate  (Cur.RefDate)+"'"
				+ ",isfrom = '"     +POut.PBool  (Cur.IsFrom)+"'"
				+" WHERE RefAttachNum = '" +POut.PInt(Cur.RefAttachNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO refattach (referralnum,patnum,"
				+"itemorder,refdate,IsFrom) VALUES("
				+"'"+POut.PInt   (Cur.ReferralNum)+"', "
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PInt   (Cur.ItemOrder)+"', "
				+"'"+POut.PDate  (Cur.RefDate)+"', "
				+"'"+POut.PBool  (Cur.IsFrom)+"')";
			NonQ(true);
			Cur.RefAttachNum=InsertID;
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM refattach "
				+"WHERE refattachnum = '"+Cur.RefAttachNum+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static bool IsReferralAttached(int referralNum){
			cmd.CommandText =
				"SELECT * FROM refattach"
				+" WHERE referralnum = '"+referralNum+"'";
			FillTable();
			if(table.Rows.Count > 0){
				return true;
			}
			else{
				return false;
			}
		}

	}

	

	

}













