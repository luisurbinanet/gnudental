using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Used by ODReport to interface with database.</summary>
	public class ODReportData:DataClass{

		///<summary>Submits the Query to the database and returns the DataTable as a result.</summary>
		///<param name="query"></param>
		public static DataTable SubmitQuery(string query){
			//MessageBox.Show(query);
			cmd.CommandText=query;
			FillTable();
			//MessageBox.Show(table.Rows.Count.ToString());
			return table;
		}
	}



}










