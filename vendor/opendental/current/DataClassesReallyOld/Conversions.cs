using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
//using ByteFX.Data.MySqlClient;
using MySql.Data.MySqlClient;

namespace OpenDental{
	


	/*=========================================================================================
		=================================== class Conversions ==========================================*/

	///<summary>Previously handled all database interactions for the ClassConvertDatabase.  But getting phased out in favor of the new DataConnection class.</summary>
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

		///<summary>throws an exception if fails.</summary>
		public static void SubmitNonQArray(){
			con.Open();
			for(int i=0;i<NonQArray.Length;i++){
				cmd.CommandText=NonQArray[i];
				cmd.ExecuteNonQuery();
			}
			con.Close();
		}

		///<summary>throws an exception if fails.</summary>
		public static void SubmitNonQString(){
			con.Open();
			cmd.CommandText=NonQString;
			cmd.ExecuteNonQuery();
			con.Close();
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

		///<summary>throws an exception if fails.</summary>
		public static void SubmitSelect(){
			cmd.CommandText=SelectText;
			FillTable();
			TableQ=table.Copy();
		}	

		

	}		



}









