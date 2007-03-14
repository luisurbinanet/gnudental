using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Miscellaneous database functions.</summary>
	public class MiscData{
		
		///<summary>Gets the current date direcly from the server.  Mostly used to prevent uesr from altering the workstation date to bypass security.</summary>
		public static DateTime GetNowDate(){
			string command="SELECT NOW()";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			return PIn.PDate(table.Rows[0][0].ToString()).Date;
		}

		///<summary>Generates a random primary key.  Tests to see if that key already exists before returning it for use.  Currently, the range of returned values is greater than 0, and less than or equal to 16777215, the limit for mysql medium int.  This will eventually change to a max of 18446744073709551615.  Then, the return value would have to be a ulong and the mysql type would have to be bigint.</summary>
		public static int GetKey(string tablename,string field){
			Random random=new Random();
			double rnd=random.NextDouble();
			while(rnd==0 || InUse(tablename,field,(int)(rnd*16777215))){
				rnd=random.NextDouble();
			}
			return (int)(rnd*16777215);
		}

		private static bool InUse(string tablename,string field,int keynum){
			string command="SELECT COUNT(*) FROM "+tablename+" WHERE "+field+"="+keynum.ToString();
			DataConnection dcon=new DataConnection();
			if(dcon.GetOneValue(command)=="0"){
				return false;
			}
			return true;//already in use
		}

	}
 
	

	
}































