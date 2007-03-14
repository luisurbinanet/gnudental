/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
/* 
 * Modified by Frederik Carlier: Patch to run on Linux.
 * Patch Copyright (c) 2007 Frederik Carlier
 */
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
//using System.Windows.Forms;

namespace OpenDentBusiness{

	///<summary></summary>
	public class DataConnection{//
		///<summary>This data adapter is used for all queries to the database.</summary>
		private MySqlDataAdapter da;
		///<summary>This is the connection that is used by the data adapter for all queries.</summary>
		private MySqlConnection con;
		///<summary>Used to get very small bits of data from the db when the data adapter would be overkill.  For instance retrieving the response after a command is sent.</summary>
		private MySqlDataReader dr;
		///<summary>Stores the string of the command that will be sent to the database.</summary>
		public MySqlCommand cmd;
		///<summary>After inserting a row, this variable will contain the primary key for the newly inserted row.  This can frequently save an additional query to the database.</summary>
		public int InsertID;
		private static string Database;
		private static string ServerName;
		private static string MysqlUser;
		private static string MysqlPass;
		//User with lower privileges:
		private static string MysqlUserLow;
		private static string MysqlPassLow;

		///<summary>This needs to be run every time we switch databases, especially on startup.  Will throw an exception if fails.  Calling class should catch exception.</summary>
		public void SetDb(string server, string database,string user, string password, string userLow, string passLow){
			string DataConnectionString=
				"Server="+server
				+";Database="+database
				+";User ID="+user
				+";Password="+password
                // Required to work on Mono. See http://blogs.ittoolbox.com/windows/alex/archives/getting-mono-and-mysql-to-play-nice-14516
                +";Pooling=false;Connection Timeout=10;Protocol=socket;Port=3306;CharSet=utf8";
			con=new MySqlConnection(DataConnectionString);
			cmd = new MySqlCommand();
			cmd.Connection=con;
			con.Open();
			cmd.CommandText="UPDATE preference SET ValueString = '0' WHERE ValueString = '0'";
			cmd.ExecuteNonQuery();
			con.Close();
			if(userLow!=""){
				DataConnectionString=
					"Server="+server
					+";Database="+database
					+";User ID="+userLow
					+";Password="+passLow
					+";Pooling=false;Connection Timeout=10;Protocol=socket;Port=3306;CharSet=utf8";
				con=new MySqlConnection(DataConnectionString);
				cmd = new MySqlCommand();
				cmd.Connection=con;
				con.Open();
				cmd.CommandText="select * FROM preference WHERE ValueString = 'DataBaseVersion'";
				DataTable table=new DataTable();
				da=new MySqlDataAdapter(cmd);
				da.Fill(table);
				con.Close();
			}
			//connection strings must be valid, so OK to set permanently
			Database=database;
			ServerName=server;
			MysqlUser=user;
			MysqlPass=password;
			MysqlUserLow=userLow;
			MysqlPassLow=passLow;
		}

		//<summary>Constructor sets the connection values.</summary>
		//private static string 

		public DataConnection(bool isLow){
			string DataConnectionString=
				"Server="+ServerName
				+";Database="+Database
				+";User ID="+MysqlUserLow
				+";Password="+MysqlPassLow
			    +";Pooling=false;Connection Timeout=10;Protocol=socket;Port=3306;CharSet=utf8";
			con=new MySqlConnection(DataConnectionString);
			//dr = null;
			cmd = new MySqlCommand();
			cmd.Connection=con;
		}

		///<summary></summary>
		public DataConnection(){
			string DataConnectionString=
				"Server="+ServerName
				+";Database="+Database
				+";User ID="+MysqlUser
				+";Password="+MysqlPass
				+";Pooling=false;Connection Timeout=10;Protocol=socket;Port=3306;CharSet=utf8";
		  con=new MySqlConnection(DataConnectionString);
			//dr = null;
			cmd = new MySqlCommand();
			cmd.Connection=con;
			//table=new DataTable();
		}

		///<summary></summary>
		public DataConnection(string database) {
			string DataConnectionString=
				"Server="+ServerName
				+";Database="+database
				+";User ID="+MysqlUser
				+";Password="+MysqlPass
				+";Pooling=false;Connection Timeout=10;Protocol=socket;Port=3306;CharSet=utf8";
			con=new MySqlConnection(DataConnectionString);
			//dr = null;
			cmd = new MySqlCommand();
			cmd.Connection=con;
			//table=new DataTable();
		}

		public DataConnection(string serverName, string database, string mysqlUser, string mysqlPass){
			string DataConnectionString=
				"Server="+serverName
				+";Database="+database
				+";User ID="+mysqlUser
				+";Password="+mysqlPass
				+";Pooling=false;Connection Timeout=10;Protocol=socket;Port=3306;CharSet=utf8";
			con=new MySqlConnection(DataConnectionString);
			//dr = null;
			cmd = new MySqlCommand();
			cmd.Connection=con;
		}

		///<summary>Fills table with data from the database.</summary>
		public DataTable GetTable(string command){
			cmd.CommandText=command;
			DataTable table=new DataTable();
 			da=new MySqlDataAdapter(cmd);
 			da.Fill(table);
			con.Close();
 			return table;
		}

		///<summary>Fills dataset with data from the database.</summary>
		public DataSet GetDs(string commands) {
			cmd.CommandText=commands;
			DataSet ds=new DataSet();
			da=new MySqlDataAdapter(cmd);
			da.Fill(ds);
			con.Close();
			return ds;
		}

		///<summary>Sends a non query command to the database and returns the number of rows affected. If true, then InsertID will be set to the value of the primary key of the newly inserted row.</summary>
		public int NonQ(string command,bool getInsertID) {
			cmd.CommandText=command;
			int rowsChanged=0;
			con.Open();
			rowsChanged=cmd.ExecuteNonQuery();
			if(getInsertID) {
				cmd.CommandText="SELECT LAST_INSERT_ID()";
				dr=(MySqlDataReader)cmd.ExecuteReader();
				if(dr.Read())
					InsertID=Convert.ToInt32(dr[0].ToString());
			}
			con.Close();
			return rowsChanged;
		}
		
		///<summary>Sends a non query command to the database and returns the number of rows affected. If true, then InsertID will be set to the value of the primary key of the newly inserted row.</summary>
		public int NonQ(string command){
			return NonQ(command,false);
		}

		/*
		///<summary>Executes a stored procedure and bubbles any resulting exception.</summary>
		public void ExecuteSP(){
			cmd.CommandType=CommandType.StoredProcedure;
 			//try{
				con.Open();
				cmd.ExecuteNonQuery();
			//}
			//catch(Exception ex){
			//	throw ex;
			//}
			//finally{
				con.Close();
			//}
 		}*/
		/*
		///<summary>Submits an array of commands in sequence. Used in conversions. Throws an exception if unsuccessful.  Returns the number of rows affected</summary>
		public int NonQ(string[] commands){
			int rowsChanged=0;
			con.Open();
			for(int i=0;i<commands.Length;i++){
				cmd.CommandText=commands[i];
				//Debug.WriteLine(cmd.CommandText);
				rowsChanged+=cmd.ExecuteNonQuery();
			}
			con.Close();
			return rowsChanged;
		}*/

		///<summary>Use this for count(*) queries.  They are always guaranteed to return one and only one value.  Uses datareader instead of datatable, so faster.  Can also be used when retrieving prefs manually, since they will also return exactly one value</summary>
		public string GetCount(string command){
			cmd.CommandText=command;
			con.Open();
			dr=(MySqlDataReader)cmd.ExecuteReader();
			dr.Read();
			string retVal=dr[0].ToString();
			con.Close();
			return retVal;
		}



	}



}









