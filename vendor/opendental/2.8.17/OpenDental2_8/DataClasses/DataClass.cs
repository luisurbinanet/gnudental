/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using ByteFX.Data.MySqlClient;
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>This is the parent of all data classes.</summary>
	///<remarks>Every database table has a corresponding struct of the same or similar name.  For instance, the claim table has the <see cref="Claim">Claim</see> struct which can store each of the individual values for a single claim row.  Each table also has a class with the same or similar name with an 's' on the end, for instance, the <see cref="Claims">Claims</see> class.  These classes all inherit from this DataClass, which means they all have access to the members of this class.  So there are roughly 60 of theses classes, one for every table plus a few extra.  Every query to the database goes through the corresponding class using the data connection members of this parent class.  Usually, <see cref="DataClass.FillTable">FillTable</see> will fill <see cref="DataClass.table">table</see> with data from the database.  Then a class like claims will copy the data into an array of type claim.  The name of the array is usually List for each of the different classes, for instance <see cref="Claims.List">Claims.List</see>.
	///</remarks>
	public class DataClass{//
		///<summary>This data adapter is used for all queries to the database.</summary>
		protected static MySqlDataAdapter da;
		///<summary>This is the connection that is used by the data adapter for all queries.</summary>
		protected static MySqlConnection con;
		///<summary>A dataset is a set of tables stored locally in memory.</summary>
		protected static DataSet ds;
		///<summary>Used to get very small bits of data from the db when the data adapter would be overkill.  For instance retrieving the response after a command is sent.</summary>
		protected static MySqlDataReader dr;
		///<summary>Stores the string of the command that will be sent to the database.</summary>
		public static MySqlCommand cmd;
		///<summary>After using the FillTable command, this table will have the table that was retrieved from the database.</summary>
		protected static DataTable table;
		///<summary>After inserting a row, this variable will contain the primary key for the newly inserted row.  This can frequently save an additional query to the database.</summary>
		protected static int InsertID;

		///<summary>Sets the connection values.</summary>
		///<remarks>This is run whenever the connection values have changed by the user and a new connection needs to be established.  Usually only when starting the program.</remarks>
		public static void SetConnection(){
		  con= new MySqlConnection(
				"Server="+FormConfig.ComputerName
				+";Database="+FormConfig.Database
				+";User ID="+FormConfig.User
				+";Password="+FormConfig.Password);
			dr = null;
			cmd = new MySqlCommand();
			cmd.Connection = con;
			table=new DataTable(null);
		}

		///<summary>Sets the connection to an alternate database for backup purposes.  Currently only used during conversions to do a quick backup first.</summary>
		public static void SetConnection(string db){
		  con= new MySqlConnection(
				"Server="+FormConfig.ComputerName
				+";Database="+db
				+";User ID="+FormConfig.User
				+";Password="+FormConfig.Password);
			dr = null;
			cmd = new MySqlCommand();
			cmd.Connection = con;
			table=new DataTable(null);
		}

		///<summary>Fills table with data from the database.</summary>
		protected static void FillTable(){
			try{
				da=new MySqlDataAdapter(cmd);
				da.Fill(table=new DataTable(null));
			}
			catch(MySqlException e){
				MessageBox.Show("Error: "+e.Message);
			}
			//catch{
			//	MessageBox.Show("Error: "+cmd.CommandText);
			//}
			finally{
				con.Close();
			}
		}

		///<summary>Used to retrieve multiple tables from the database.</summary>
		///<remarks>The driver did not used to be good enough to retreive datasets, but now that it is, we are trying to slowly transition to using this method to reduce the number of queries that have to be sent.</remarks>
		protected static void FillDataSet(){//
			try{
				da=new MySqlDataAdapter(cmd);
				ds=new DataSet();
				da.Fill(ds);
			}
			catch(MySqlException e){
				MessageBox.Show("Error: "+e.Message);
			}
			catch{
				MessageBox.Show("Error: "+cmd.CommandText);
			}
			finally{
				con.Close();
			}
		}

		///<summary></summary>
		protected static void NonQ(){
			NonQ(false);
		}

		/// <summary>Sends a non query command to the database.</summary>
		/// <param name="getInsertID">If true, then InsertID will be set to the value of the primary key of the newly inserted row.</param>
		protected static void NonQ(bool getInsertID){
			try{
				con.Open();
				cmd.ExecuteNonQuery();
				if(getInsertID){
					cmd.CommandText = "SELECT LAST_INSERT_ID()";
					dr = (MySqlDataReader)cmd.ExecuteReader();
					if(dr.Read())
						InsertID=PIn.PInt(dr[0].ToString());
				}
			}
			catch(MySqlException e){
				MessageBox.Show("Error: "+e.Message+","+cmd.CommandText);
			}
			//catch{
			//	MessageBox.Show("Error: "+);
			//}
			finally{
				con.Close();
				dr=null;
			}
		}

	}//end DataClass



}









