using System;
using System.Collections;
using System.Data;
//using System.Drawing;
//using System.IO;
using System.Windows.Forms;
using ByteFX.Data.MySqlClient;

namespace OpenDental{
	


	/*=========================================================================================
		=================================== class Conversions ==========================================*/

	///<summary>Handles all database interactions for the ClassConvertDatabase.</summary>
	public class Conversions:DataClass{
		///<summary>Set up an array of strings here, and then execute them all by calling SubmitNonQArray().</summary>
		public static string[] NonQArray;
		///<summary>Set up one string here, and then call SubmitNonQString().</summary>
		public static string NonQString;
		///<summary>To select a table, enter the query here. Then call SubmitSelect() and the result will be in TableQ.</summary>
		public static string SelectText;
		///<summary>The result table after calling SubmitSelect().</summary>
		public static DataTable TableQ;
		///<summary>Temporary only. Used for backing up.</summary>
		public static string ReaderText;

		///<summary></summary>
		public static bool SubmitNonQArray(){//return true if successful
			try{
				//int rowsUpdated;
				con.Open();
				for(int i=0;i<NonQArray.Length;i++){
					cmd.CommandText=NonQArray[i];
					cmd.ExecuteNonQuery();
				}
			}
			catch(MySqlException e){
				MessageBox.Show("e:"+e.Message);
				return false;
			}
			catch{
				MessageBox.Show("Command:"+cmd.CommandText);
				return false;
			}
			finally{
				con.Close();
			}
			return true;
		}

		///<summary></summary>
		public static bool SubmitNonQString(){//return true if successful
			try{
				//int rowsUpdated;
				con.Open();
				cmd.CommandText=NonQString;
				cmd.ExecuteNonQuery();
			}
			catch(MySqlException e){
				MessageBox.Show("e:"+e.Message);
				return false;
			}
			catch{
				MessageBox.Show("Command:"+cmd.CommandText);
				return false;
			}
			finally{
				con.Close();
			}
			return true;
		}

		///<summary>Only used in making backups. always returns the second column of row 1. This is used because of a bug in the data adaptor.</summary>
		public static string SubmitReader(){
			//try{
			cmd.CommandText=ReaderText;
			con.Open();
			MySqlDataReader reader=cmd.ExecuteReader();
			//try{
			MessageBox.Show(reader.HasRows.ToString()+","+reader.FieldCount.ToString());
			reader.Read();
			//}
			//catch{
			//	MessageBox.Show(cmd.CommandText);
			//}
			con.Close();
			return reader.GetString(0);
			
			//FillTable();
			//	TableQ=table.Copy();
			//}
			//catch(MySqlException e){
			//	MessageBox.Show("e:"+e.Message);
			//	return false;
			//}
			//catch{
			//	MessageBox.Show("Command:"+cmd.CommandText);
			//	return false;
			//}
			//return "";//true;
		}	

		///<summary>Returns true if successful.</summary>
		public static bool SubmitSelect(){
			//try{
				cmd.CommandText=SelectText;
				FillTable();
				TableQ=table.Copy();
			//}
			//catch(MySqlException e){
			//	MessageBox.Show("e:"+e.Message);
			//	return false;
			//}
			//catch{
			//	MessageBox.Show("Command:"+cmd.CommandText);
			//	return false;
			//}
			return true;
		}	

		///<summary></summary>
		public static bool AddressNotesVers2_0(){
			try{
				cmd.CommandText="SELECT patnum,addrnote FROM patient WHERE patnum = guarantor";
				FillTable();
				for(int i=0;i<table.Rows.Count;i++){
					cmd.CommandText="UPDATE patient SET "
						+"addrnote = '"+POut.PString(table.Rows[i][1].ToString())+"' "
						+"WHERE guarantor = '"+table.Rows[i][0].ToString()+"'";
					NonQ(false);
				}
				return true;
			}
			catch{
				MessageBox.Show(cmd.CommandText);
				return false;
			}
			finally{
				con.Close();
			}
		}

	}		



}









