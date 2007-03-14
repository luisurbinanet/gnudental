/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using ByteFX.Data.MySqlClient;

using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace OpenDental{
	
	/*=========================================================================================
	=================================== class DataClass ==========================================*/
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
			//catch(MySqlException e){
			//	MessageBox.Show(Lan.g("DataClass","Error: ")+e.Message);
			//}
			catch{
				MessageBox.Show("Error: "+cmd.CommandText);
			}
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

	/*=========================================================================================
	=================================== class Batch ========================================*/
	///<summary>Used to send batch SQL Select statements.</summary>
	///<remarks>this is a first attempt at batch commands and it needs some refining.  The huge advantage is that it only involves ONE round trip to the database.</remarks>
	public class Batch:DataClass{
		/// <summary>Retrieves a set of tables from the database.</summary>
		/// <param name="tableList">A simple string with the names of each table separated by commas.</param>
		/// <remarks>Would like to eliminate the switch statement from this class and replace it with a more flexible strategy possibly using reflection.</remarks>
		public static void Select(string tableList){
			AssembleCommand(tableList);
			FillDataSet();
			AssignTableNames(tableList);
			RefreshClasses(tableList);
		}

		private static void AssembleCommand(string tableList){
			string[] tableArray=tableList.Split(',');
			cmd.CommandText="";
			for(int i=0;i<tableArray.Length;i++){
				switch(tableArray[i]){
					case "graphicassembly":
						cmd.CommandText+=GraphicAssemblies.GetSelectText();
						break;
					case "graphicelement":
						cmd.CommandText+=GraphicElements.GetSelectText();
						break;
					case "graphicshape":
						cmd.CommandText+=GraphicShapes.GetSelectText();
						break;
					case "graphictype":
						cmd.CommandText+=GraphicTypes.GetSelectText();
						break;
				}
			}
		}

		private static void AssignTableNames(string tableList){
			string[] tableArray=tableList.Split(',');
			for(int i=0;i<tableArray.Length;i++){
				switch(tableArray[i]){
					default:
						ds.Tables[i].TableName=tableArray[i];
						break;
					//only reason to not use default is if you use parameters(no examples of that yet)
				}
			}
		}

		private static void RefreshClasses(string tableList){
			string[] tableArray=tableList.Split(',');
			for(int i=0;i<tableArray.Length;i++){
				switch(tableArray[i]){
					case "graphicassembly":
						GraphicAssemblies.Refresh();
						break;
					case "graphicelement":
						GraphicElements.Refresh();
						break;
					case "graphicshape":
						GraphicShapes.Refresh();
						break;
					case "graphictype":
						GraphicTypes.Refresh();
						break;
				}
			}//for
		}



	}

	/*=========================================================================================
	=================================== class Adjustments ==========================================*/
	///<summary>Handles database commands related to the adjustment table in the db.</summary>
	public class Adjustments:DataClass{
		///<summary>A list of adjustments for a single patient.</summary>
		public static Adjustment[] List;
		///<summary>Current. A single row of data.</summary>
		public static Adjustment Cur;
		//<summary></summary>
		//public static Adjustment[] PaymentList;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT adjnum,adjdate,adjamt,patnum, "
				+"adjtype,provnum,adjnote"
				+" from adjustment"
				+" WHERE patnum = '"+Patients.Cur.PatNum+"' ORDER BY adjdate";
			FillTable();
			List=new Adjustment[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].AdjNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].AdjDate= PIn.PDate  (table.Rows[i][1].ToString());
				List[i].AdjAmt = PIn.PDouble(table.Rows[i][2].ToString());
				List[i].PatNum = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].AdjType= PIn.PInt   (table.Rows[i][4].ToString());
				List[i].ProvNum= PIn.PInt   (table.Rows[i][5].ToString());
				List[i].AdjNote= PIn.PString(table.Rows[i][6].ToString());
			}//end for
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE adjustment SET " 
				+ "adjdate = '"      +POut.PDate  (Cur.AdjDate)+"'"
				+ ",adjamt = '"      +POut.PDouble(Cur.AdjAmt)+"'"
				+ ",patnum = '"      +POut.PInt   (Cur.PatNum)+"'"
				+ ",adjtype = '"     +POut.PInt   (Cur.AdjType)+"'"
				+ ",provnum = '"     +POut.PInt   (Cur.ProvNum)+"'"
				+ ",adjnote = '"     +POut.PString(Cur.AdjNote)+"'"
				+" WHERE adjNum = '" +POut.PInt   (Cur.AdjNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO adjustment (adjdate,adjamt,patnum, "
				+"adjtype,provnum,adjnote) VALUES("
				+"'"+POut.PDate  (Cur.AdjDate)+"', "
				+"'"+POut.PDouble(Cur.AdjAmt)+"', "
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PInt   (Cur.AdjType)+"', "
				+"'"+POut.PInt   (Cur.ProvNum)+"', "
				+"'"+POut.PString(Cur.AdjNote)+"')";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText="DELETE FROM adjustment "
				+"WHERE adjnum = '"+Cur.AdjNum.ToString()+"'";
			NonQ(false);
		}

		///<summary>Must make sure Refresh is done first.  Returns the sum of all adjustments for this patient.  Amount might be pos or neg.</summary>
		public static double ComputeBal(){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				retVal+=List[i].AdjAmt;
			}
			return retVal;
		}
	}

	///<summary>Corresponds to the adjustment table in the database.</summary>
	public struct Adjustment{
		///<summary>Primary key.</summary>
		public int AdjNum;
		///<summary>Date of adjustment.</summary>
		public DateTime AdjDate;
		///<summary>Amount of adjustment.</summary>
		public double AdjAmt;
		///<summary>Foreign key to <see cref="Patient.PatNum">patient.PatNum</see>.  Can be pos or neg.</summary>
		public int PatNum;
		///<summary>Foreign key to <see cref="Def.DefNum">definition.DefNum</see>.</summary>
		public int AdjType;
		///<summary>Foreign key to <see cref="Provider.ProvNum">provider.ProvNum</see>.</summary>
		public int ProvNum;
		///<summary>Note for this adjustment.</summary>
		public string AdjNote;
	}


	/*=========================================================================================
	=================================== class Appointments ==========================================*/
	///<summary>Handles database commands related to the appointment table in the db.</summary>
	public class Appointments:DataClass{
		///<summary>This is a temporary private list of appointments which then gets copied to one of the three public lists.</summary>
		private static Appointment[] List;
		///<summary>A list of appointments for one day in the schedule, whether hidden or not.</summary>
		public static Appointment[] ListDay;
		///<summary>A list of appointments for use on the Unscheduled list or the Next appointment tracker.</summary>
		public static Appointment[] ListUn;
		///<summary>A list of appointments for use on the Other appointments list for a single patient.</summary>
		public static Appointment[] ListOth;
		///<summary>Current.  A single row of data.</summary>
		public static Appointment Cur;
		///<summary>The appointment on the pinboard.</summary>
		public static Appointment PinBoard;
		///<summary>The date currently selected in the appointment module.</summary>
		public static DateTime DateSelected;

		///<summary>Gets the ListDay for a given date.</summary>
		public static void Refresh(DateTime thisDay){
			DateSelected = thisDay;
			cmd.CommandText =
				"SELECT * from appointment "
				+"WHERE AptDateTime LIKE '"+POut.PDate(thisDay)+"%' "
				+"&& aptstatus != '"+(int)ApptStatus.UnschedList+"'";
			FillList();
			ListDay=List;
			List=null;
		}

		///<summary>Gets ListUn for both the unscheduled list and for next appt tracker.
		///This is in transition, since the unscheduled list will probably eventually be phased out.</summary>
		///<param name="doGetNext">True if getting Next appointments, false if getting Unscheduled appointments.</param>
		public static void RefreshUnsched(bool doGetNext){
			if(doGetNext){
				cmd.CommandText="SELECT Tnext.*,Tregular.aptnum FROM appointment AS Tnext "
					+"LEFT JOIN appointment AS Tregular ON Tnext.aptnum = Tregular.nextaptnum "
					+"WHERE Tnext.aptstatus = '"+(int)ApptStatus.Next+"' "
					+"AND Tregular.aptnum IS NULL "
					+"ORDER BY Tnext.UnschedStatus,Tnext.AptDateTime";
			}
			else{//unsched
				cmd.CommandText="SELECT * FROM appointment "
					+"WHERE aptstatus = '"+(int)ApptStatus.UnschedList+"' "
					+"ORDER BY AptDateTime";
			}
			FillList();
			ListUn=List;
			List=null;
		}

		///<summary>Gets the ListOth for the current patient.</summary>
		public static void RefreshOther(){
			cmd.CommandText =
				"SELECT * FROM appointment "
				+"WHERE patnum = '"+Patients.Cur.PatNum.ToString()+"' "
				+"ORDER BY AptDateTime";
			FillList();
			ListOth=List;
			List=null;
		}

		private static void FillList(){
			FillTable();
			List = new Appointment[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].AptNum      =PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum      =PIn.PInt   (table.Rows[i][1].ToString());
				List[i].AptStatus   =(ApptStatus)PIn.PInt(table.Rows[i][2].ToString());
				List[i].Pattern     =PIn.PString(table.Rows[i][3].ToString());
				List[i].Confirmed   =PIn.PInt   (table.Rows[i][4].ToString());
				List[i].AddTime     =PIn.PInt   (table.Rows[i][5].ToString());
				List[i].Op          =PIn.PInt   (table.Rows[i][6].ToString());
				List[i].Note        =PIn.PString(table.Rows[i][7].ToString());
				List[i].ProvNum     =PIn.PInt   (table.Rows[i][8].ToString());
				List[i].ProvHyg     =PIn.PInt   (table.Rows[i][9].ToString());
				List[i].AptDateTime =PIn.PDateT (table.Rows[i][10].ToString());
				List[i].NextAptNum  =PIn.PInt   (table.Rows[i][11].ToString());
				List[i].UnschedStatus=PIn.PInt  (table.Rows[i][12].ToString());
				List[i].Lab         =(LabCase)PIn.PInt   (table.Rows[i][13].ToString());
				List[i].IsNewPatient=PIn.PBool  (table.Rows[i][14].ToString());
				List[i].ProcDescript=PIn.PString(table.Rows[i][15].ToString());
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO appointment (patnum,aptstatus, "
				+"pattern,confirmed,addtime,op,note,provnum,"
				+"provhyg,aptdatetime,nextaptnum,unschedstatus,lab,isnewpatient,procdescript) VALUES("
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PInt   ((int)Cur.AptStatus)+"', "
				+"'"+POut.PString(Cur.Pattern)+"', "
				+"'"+POut.PInt   (Cur.Confirmed)+"', "
				+"'"+POut.PInt   (Cur.AddTime)+"', "
				+"'"+POut.PInt   (Cur.Op)+"', "
				+"'"+POut.PString(Cur.Note)+"', "
				+"'"+POut.PInt   (Cur.ProvNum)+"', "
				+"'"+POut.PInt   (Cur.ProvHyg)+"', "
				+"'"+POut.PDateT (Cur.AptDateTime)+"', "
				+"'"+POut.PInt   (Cur.NextAptNum)+"', "
				+"'"+POut.PInt   (Cur.UnschedStatus)+"', "
				+"'"+POut.PInt   ((int)Cur.Lab)+"', "
				+"'"+POut.PBool  (Cur.IsNewPatient)+"', "
				+"'"+POut.PString(Cur.ProcDescript)+"')";
			NonQ(true);
			Cur.AptNum=InsertID;
			//MessageBox.Show(Cur.AptNum.ToString());
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE appointment SET "
				+"PatNum = '"      +POut.PInt   (Cur.PatNum)+"', "
				+"AptStatus = '"   +POut.PInt   ((int)Cur.AptStatus)+"', "
				+"Pattern = '"     +POut.PString(Cur.Pattern)+"', "
				+"Confirmed = '"   +POut.PInt   (Cur.Confirmed)+"', "
				+"AddTime = '"     +POut.PInt   (Cur.AddTime)+"', "
				+"Op = '"          +POut.PInt   (Cur.Op)+"', "
				+"Note = '"        +POut.PString(Cur.Note)+"', "
				+"provnum = '"     +POut.PInt   (Cur.ProvNum)+"', "
				+"provhyg = '"     +POut.PInt   (Cur.ProvHyg)+"', "
				+"aptdatetime = '" +POut.PDateT (Cur.AptDateTime)+"', "
				+"nextaptnum = '"  +POut.PInt   (Cur.NextAptNum)+"', "
				+"unschedstatus = '" +POut.PInt(Cur.UnschedStatus)+"', "
				+"lab = '"         +POut.PInt   ((int)Cur.Lab)+"', "
				+"isnewpatient = '"+POut.PBool  (Cur.IsNewPatient)+"', "
				+"procdescript = '"+POut.PString(Cur.ProcDescript)+"' "
				+"WHERE AptNum = '"+POut.PInt  (Cur.AptNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary>Gets one appointment and stores the info in Cur.</summary>
		public static void RefreshCur(int aptNum){
			cmd.CommandText =
				"SELECT * "
				+"FROM appointment "
				+"WHERE aptnum = '"+POut.PInt(aptNum)+"'";
			FillTable();
			Cur=new Appointment();
			if(table.Rows.Count==0){
				return;
			}
			Cur.AptNum      =PIn.PInt   (table.Rows[0][0].ToString());
			Cur.PatNum      =PIn.PInt   (table.Rows[0][1].ToString());
			Cur.AptStatus   =(ApptStatus)PIn.PInt(table.Rows[0][2].ToString());
			Cur.Pattern     =PIn.PString(table.Rows[0][3].ToString());
			Cur.Confirmed   =PIn.PInt   (table.Rows[0][4].ToString());
			Cur.AddTime     =PIn.PInt   (table.Rows[0][5].ToString());
			Cur.Op          =PIn.PInt   (table.Rows[0][6].ToString());
			Cur.Note        =PIn.PString(table.Rows[0][7].ToString());
			Cur.ProvNum     =PIn.PInt   (table.Rows[0][8].ToString());
			Cur.ProvHyg     =PIn.PInt   (table.Rows[0][9].ToString());
			Cur.AptDateTime =PIn.PDateT (table.Rows[0][10].ToString());
			Cur.NextAptNum  =PIn.PInt   (table.Rows[0][11].ToString());
			Cur.UnschedStatus =PIn.PInt (table.Rows[0][12].ToString());
			Cur.Lab         =(LabCase)PIn.PInt(table.Rows[0][13].ToString());
			Cur.IsNewPatient=PIn.PBool  (table.Rows[0][14].ToString());
			Cur.ProcDescript=PIn.PString(table.Rows[0][15].ToString());
		}
	
		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText="DELETE from appointment WHERE "
				+"aptnum = '"+POut.PInt(Cur.AptNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary>Used when generating the recall list to test whether a patient already has a future appointment scheduled.</summary>
		///<param name="patNum"></param>
		///<returns></returns>
		///<remarks>It was not possible to incorporate this into the main query because it would have been too complex.  A single query might be a good idea at some point.</remarks>
		public static bool PatientHasFutureRecall(int patNum){
			cmd.CommandText="SELECT appointment.patNum FROM appointment,procedurelog,procedurecode "
				+"WHERE procedurelog.patnum = '"+patNum.ToString()+"' "
				+"AND appointment.patnum = '"+patNum.ToString()+"' "
				+"AND procedurelog.ADACode = procedurecode.ADACode "
				+"AND procedurelog.aptnum = appointment.aptnum "
				+"AND appointment.AptDateTime >= '"+DateTime.Today.ToString("yyyy-MM-dd")+"' "
				+"AND procedurecode.SetRecall = '1'";
			FillTable();
			if(table.Rows.Count==0){
				return false;
			}
			else{
				return true;
			}
		}
	}//end class Appointments
	
	///<summary>Corresponds to the appointment table in the database.</summary>
	public struct Appointment{
		///<summary>Primary key.</summary>
		public int AptNum;
		///<summary>Foreign key to <see cref="Patient.PatNum">patient.PatNum</see>.</summary>
		public int PatNum;
		///<summary>See the <see cref="ApptStatus"/> enumeration.</summary>
		public ApptStatus AptStatus;
		///<summary>Time pattern, X for Dr time, / for assist time.</summary>
		public string Pattern;
		///<summary>Foreign key to <see cref="Def.DefNum">definition.DefNum</see>.</summary>
		///<remarks>The <see cref="Def.Category">definition.Category</see> in the definition table is <see cref="DefCat.ApptConfirmed">DefCat.ApptConfirmed</see>.</remarks>
		public int Confirmed;
		///<summary>Amount of time to add to appointment.  Example: 2 would represent add 20 minutes.</summary>
		public int AddTime;
		///<summary>Operatory.  Foreign key to <see cref="Def.DefNum">definition.DefNum</see>.</summary>
		///<remarks>The <see cref="Def.Category">definition.Category</see> in the definition table is <see cref="DefCat.Operatories">DefCat.Operatories</see>.</remarks>
		public int Op;
		///<summary>Note.</summary>
		public string Note;
		///<summary>Foreign key to <see cref="Provider.ProvNum">provider.ProvNum</see>.</summary>
		public int ProvNum;
		///<summary>Hygiene provider.  Foreign key to <see cref="Provider.ProvNum">provider.ProvNum</see>.</summary>
		public int ProvHyg;
		///<summary>Appointment Date and time.</summary>
		public DateTime AptDateTime;
		///<summary>Only used to show that this apt is derived from specified next apt. Otherwise, 0. Foreign key to appointment.AptNum.</summary>
		public int NextAptNum;
		///<summary>Foreign key to <see cref="Def.DefNum">definition.DefNum</see>.</summary>
		///<remarks>The <see cref="Def.Category">definition.Category</see> in the definition table is <see cref="DefCat.RecallUnschedStatus">DefCat.RecallUnschedStatus</see>.  Only used if this is an Unsched or Next appt.</remarks>
		public int UnschedStatus;
		///<summary>A lab case is expected for this appointment.</summary>
		public LabCase Lab;
		///<summary>This is the first appoinment this patient has had at this office.</summary>
		public bool IsNewPatient;
		///<summary>A one line summary of all procedures.  Can be used in various reports, Unscheduled list, and Next appointment tracker.  Not user editable right now.</summary>
		public string ProcDescript;
	}

	/*=========================================================================================
	=================================== class ApptViews ===========================================*/
	///<summary>Handles database commands related to the apptview table in the database.</summary>
	public class ApptViews:DataClass{
		///<summary>Current.  A single row of data.</summary>
		public static ApptView Cur;
		///<summary>A list of all apptviews, in order.</summary>
		public static ApptView[] List;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from apptview ORDER BY itemorder";
			FillTable();
			List=new ApptView[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i].ApptViewNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description = PIn.PString(table.Rows[i][1].ToString());
				List[i].ItemOrder   = PIn.PInt   (table.Rows[i][2].ToString());	
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO apptview (description,itemorder) "
				+"VALUES ("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PInt   (Cur.ItemOrder)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.ApptViewNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE apptview SET "
				+"description='" +POut.PString(Cur.Description)+"'"
				+",itemorder = '"+POut.PInt   (Cur.ItemOrder)+"'"
				+" WHERE apptviewnum = '"+POut.PInt(Cur.ApptViewNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText="DELETE from apptview WHERE apptviewnum = '"
				+POut.PInt(Cur.ApptViewNum)+"'";
			NonQ(false);
		}

		/// <summary>Used in appt module.  Can be -1 if no category selected </summary>
		public static void SetCur(int index){
			if(index==-1){
				Cur=new ApptView();
			}
			else{
				Cur=List[index];
			}
		}


	}

	///<summary>Corresponds to the apptview table in the database. Enables viewing a variety of operatories or providers.</summary>
	public struct ApptView{
		///<summary>Primary key.</summary>
		public int ApptViewNum;
		///<summary>Description of this view.  Gets displayed in Appt module.</summary>
		public string Description;
		///<summary>Order to display in lists. Every view must have a unique itemorder, but it is acceptable to have some missing itemorders in the sequence.</summary>
		public int ItemOrder;
	}

	/*=========================================================================================
	=================================== class ApptViewItems ===========================================*/
	///<summary>Handles database commands related to the apptviewitem table in the database.</summary>
	public class ApptViewItems:DataClass{
		///<summary>Current.  A single row of data.</summary>
		public static ApptViewItem Cur;
		///<summary>A list of all ApptViewItems.</summary>
		public static ApptViewItem[] List;
		///<summary>A list of the ApptViewItems for the current view.</summary>
		public static ApptViewItem[] ForCurView;
		//these two are subsets of provs and ops. You can't include hidden prov or op in this list.
		///<summary>Visible providers in appt module.  List of indices to providers.List(short).</summary>
		///<remarks>Also see VisOps.  This is a subset of the available provs.  You can't include a hidden prov in this list.</remarks>
		public static int[] VisProvs;
		///<summary>Visible ops in appt module.  List of indices to Defs.Short[ops].</summary>
		///<remarks>Also see VisProvs.  This is a subset of the available ops.  You can't include a hidden op in this list.</remarks>
		public static int[] VisOps;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from apptviewitem";
			FillTable();
			List=new ApptViewItem[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i].ApptViewItemNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ApptViewNum     = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].OpNum           = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].ProvNum         = PIn.PInt   (table.Rows[i][3].ToString());
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO apptviewitem (apptviewnum,opnum,provnum) "
				+"VALUES ("
				+"'"+POut.PInt   (Cur.ApptViewNum)+"', "
				+"'"+POut.PInt   (Cur.OpNum)+"', "
				+"'"+POut.PInt   (Cur.ProvNum)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
			//Cur.ApptViewNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE apptviewitem SET "
				+"apptviewnum='" +POut.PInt   (Cur.ApptViewNum)+"'"
				+",opnum = '"    +POut.PInt   (Cur.OpNum)+"'"
				+",provnum = '"  +POut.PInt   (Cur.ProvNum)+"'"
				+" WHERE apptviewitemnum = '"+POut.PInt(Cur.ApptViewItemNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText="DELETE from apptviewitem WHERE apptviewitemnum = '"
				+POut.PInt(Cur.ApptViewItemNum)+"'";
			NonQ(false);
		}

		///<summary>Deletes all apptviewitems for the current apptView.</summary>
		public static void DeleteAllForView(){
			cmd.CommandText="DELETE from apptviewitem WHERE apptviewnum = '"
				+POut.PInt(ApptViews.Cur.ApptViewNum)+"'";
			NonQ(false);
		}

		/// <summary>Gets (list)ForCurView, VisOps, and VisProvs.  Works even if no apptview is selected.
		/// </summary>
		public static void GetForCurView(){
			ArrayList tempAL=new ArrayList();
			ArrayList ALprov=new ArrayList();
			ArrayList ALops=new ArrayList();
			if(ApptViews.Cur.ApptViewNum==0){
				//MessageBox.Show("apptcategorynum:"+ApptCategories.Cur.ApptCategoryNum.ToString());
				//make visible ops exactly the same as the short def list (all except hidden)
				for(int i=0;i<Defs.Short[(int)DefCat.Operatories].Length;i++){
					ALops.Add(i);
				}
				//make visible provs exactly the same as the prov list (all except hidden)
				for(int i=0;i<Providers.List.Length;i++){
					ALprov.Add(i);
				}

			}
			else{
				int index;
				for(int i=0;i<List.Length;i++){
					if(List[i].ApptViewNum==ApptViews.Cur.ApptViewNum){
						tempAL.Add(List[i]);
						if(List[i].OpNum>0){//op
							index=Defs.GetOrder(DefCat.Operatories,List[i].OpNum);
							if(index!=-1){
								ALops.Add(index);
							}
						}
						else{//prov
							index=Providers.GetIndex(List[i].ProvNum);
							if(index!=-1){
								ALprov.Add(index);
							}
						}
					}
				}
			}
			ForCurView=new ApptViewItem[tempAL.Count];
			for(int i=0;i<tempAL.Count;i++){
				ForCurView[i]=(ApptViewItem)tempAL[i];
			}
			VisOps=new int[ALops.Count];
			for(int i=0;i<ALops.Count;i++){
				VisOps[i]=(int)ALops[i];
			}
			Array.Sort(VisOps);
			VisProvs=new int[ALprov.Count];
			for(int i=0;i<ALprov.Count;i++){
				VisProvs[i]=(int)ALprov[i];
			}
			Array.Sort(VisProvs);
		}

		///<summary>Returns the index of the provNum within VisProvs.</summary>
		public static int GetIndexProv(int provNum){
			for(int i=0;i<VisProvs.Length;i++){
				if(Providers.List[VisProvs[i]].ProvNum==provNum)
					return i;
			}		
			return -1;
		}

		///<summary>Returns the index of the opNum(defNum) within VisOps.</summary>
		public static int GetIndexOp(int opNum){
			for(int i=0;i<VisOps.Length;i++){
				if(Defs.Short[(int)DefCat.Operatories][VisOps[i]].DefNum==opNum)
					return i;
			}		
			return -1;
		}

		///<summary>Only used in ApptViewItem setup. Must have run GetForCurView first.</summary>
		public static bool OpIsInView(int opNum){
			for(int i=0;i<ForCurView.Length;i++){
				if(ForCurView[i].OpNum==opNum)
					return true;
			}
			return false;
		}

		///<summary>Only used in ApptViewItem setup. Must have run GetForCurView first.</summary>
		public static bool ProvIsInView(int provNum){
			for(int i=0;i<ForCurView.Length;i++){
				if(ForCurView[i].ProvNum==provNum)
					return true;
			}
			return false;
		}


	}

	///<summary>Corresponds to the apptviewitem table in the database.</summary>
	public struct ApptViewItem{
		///<summary>Primary key.</summary>
		public int ApptViewItemNum;//
		///<summary>Foreign key to apptview.</summary>
		public int ApptViewNum;
		///<summary>Foreign key to definition.DefNum.</summary>
		public int OpNum;
		///<summary>Foreign key to provider.ProvNum.</summary>
		public int ProvNum;
	}


	/*=========================================================================================
	=================================== class AutoCodes ===========================================*/

	///<summary></summary>
	public class AutoCodes:DataClass{
		///<summary></summary>
		public static AutoCode Cur;
		///<summary></summary>
		public static AutoCode[] List;
		///<summary></summary>
		public static AutoCode[] ListShort;
		///<summary></summary>
		public static Hashtable HList; 

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from autocode";
			FillTable();
			HList=new Hashtable();
			List=new AutoCode[table.Rows.Count];
			ArrayList ALshort=new ArrayList();//int of indexes of short list
			for(int i = 0;i<List.Length;i++){
				List[i].AutoCodeNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description= PIn.PString(table.Rows[i][1].ToString());
				List[i].IsHidden   = PIn.PBool  (table.Rows[i][2].ToString());	
				HList.Add(List[i].AutoCodeNum,List[i]);
				if(!List[i].IsHidden){
					ALshort.Add(i);
				}
			}
			ListShort=new AutoCode[ALshort.Count];
			for(int i=0;i<ALshort.Count;i++){
				ListShort[i]=List[(int)ALshort[i]];
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO autocode (description,ishidden) "
				+"VALUES ("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PBool  (Cur.IsHidden)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.AutoCodeNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE autocode SET "
				+"description='"+POut.PString(Cur.Description)+"'"
				+",ishidden = '" +POut.PBool  (Cur.IsHidden)+"'"
				+" WHERE autocodenum = '"+POut.PInt (Cur.AutoCodeNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from autocode WHERE autocodenum = '"+POut.PInt(Cur.AutoCodeNum)+"'";
			NonQ(false);
		}

	}

	///<summary>Corresponds to the autocode table in the database.</summary>
	public struct AutoCode{
		///<summary>Primary key.</summary>
		public int AutoCodeNum;
		///<summary>Displays meaningful decription, like "Amalgam".</summary>
		public string Description;
		///<summary>User can hide autocodes</summary>
		public bool IsHidden;
	}

	/*=========================================================================================
	=================================== class AutoCodeConds ===========================================*/
  ///<summary></summary>
	public class AutoCodeConds:DataClass{
		///<summary></summary>
		public static AutoCodeCond Cur;
		///<summary></summary>
		public static AutoCodeCond[] List;
		///<summary></summary>
		public static AutoCodeCond[] ListForItem;
		private static ArrayList ALlist;
		//public static Hashtable HList; 

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from autocodecond ORDER BY condition";
			FillTable();
			//HList=new Hashtable();
			List=new AutoCodeCond[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i].AutoCodeCondNum= PIn.PInt        (table.Rows[i][0].ToString());
				List[i].AutoCodeItemNum= PIn.PInt        (table.Rows[i][1].ToString());
				List[i].Condition=(AutoCondition)PIn.PInt(table.Rows[i][2].ToString());	
				//HList.Add(List[i].AutoCodeItemNum,List[i]);
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO autocodecond (autocodeitemnum,condition) "
				+"VALUES ("
				+"'"+POut.PInt(Cur.AutoCodeItemNum)+"', "
				+"'"+POut.PInt((int)Cur.Condition)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.AutoCodeCondNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE autocodecond SET "
				+"autocodeitemnum='"+POut.PInt(Cur.AutoCodeItemNum)+"'"
				+",condition ='"     +POut.PInt((int)Cur.Condition)+"'"
				+" WHERE autocodecondnum = '"+POut.PInt(Cur.AutoCodeCondNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from autocodecond WHERE autocodecondnum = '"
				+POut.PInt(Cur.AutoCodeCondNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteForItemNum(int itemNum){
			cmd.CommandText = "DELETE from autocodecond WHERE autocodeitemnum = '"
				+POut.PInt(itemNum)+"'";//AutoCodeItems.Cur.AutoCodeItemNum)
			NonQ(false); 
		}

		///<summary></summary>
		public static void GetListForItem(int autoCodeItemNum){
			ALlist=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].AutoCodeItemNum==autoCodeItemNum){
					ALlist.Add(List[i]);
				} 
			}
			ListForItem=new AutoCodeCond[ALlist.Count];
			if(ALlist.Count > 0){			
				ALlist.CopyTo(ListForItem);
			}     
		}

		///<summary></summary>
		public static bool IsSurf(AutoCondition myAutoCondition){
			switch(myAutoCondition){
				case AutoCondition.One_Surf:
				case AutoCondition.Two_Surf:
				case AutoCondition.Three_Surf:
				case AutoCondition.Four_Surf:
				case AutoCondition.Five_Surf:
					return true;
				default:
					return false;
			}
		}

		///<summary></summary>
		public static bool ConditionIsMet(AutoCondition myAutoCondition, string toothNum,string surf,bool isAdditional){//MissingTeeth is already available for given patient
			switch(myAutoCondition){
				case AutoCondition.Anterior:
					return Tooth.IsAnterior(toothNum);
				case AutoCondition.Posterior:
					return Tooth.IsPosterior(toothNum);
				case AutoCondition.Premolar:
					return Tooth.IsPreMolar(toothNum);
				case AutoCondition.Molar:
					return Tooth.IsMolar(toothNum);
				case AutoCondition.One_Surf:
					return surf.Length==1;
				case AutoCondition.Two_Surf:
					return surf.Length==2;
				case AutoCondition.Three_Surf:
					return surf.Length==3;
				case AutoCondition.Four_Surf:
					return surf.Length==4;
				case AutoCondition.Five_Surf:
					return surf.Length==5;
				case AutoCondition.First:
					return !isAdditional;
				case AutoCondition.EachAdditional:
					return isAdditional;
				case AutoCondition.Maxillary:
					return Tooth.IsMaxillary(toothNum);
				case AutoCondition.Mandibular:
					return !Tooth.IsMaxillary(toothNum);
				case AutoCondition.Primary:
					return Tooth.IsPrimary(toothNum);
				case AutoCondition.Permanent:
					return !Tooth.IsPrimary(toothNum);
				case AutoCondition.Pontic:
					return Procedures.MissingTeeth.Contains(toothNum);
				case AutoCondition.Retainer:
					return !Procedures.MissingTeeth.Contains(toothNum);
				default:
					return false;
			}//switch
		}//Condition is met

	}

	///<summary>Corresponds to the autocodecond table in the database.</summary>
	///<remarks>There is usually only one or two conditions for a given AutoCodeItem.</remarks>
	public struct AutoCodeCond{//
		///<summary>Primary key.</summary>
		public int AutoCodeCondNum;
		///<summary>Foreign key to AutoCodeItem.AutoCodeItemNum.</summary>
		public int AutoCodeItemNum;
		///<summary>See the AutoCondition enumeration.</summary>
		public AutoCondition Condition;
	}

	/*=========================================================================================
	=================================== class AutoCodeItems ===========================================*/

	///<summary></summary>
	public class AutoCodeItems:DataClass{
		///<summary></summary>
		public static AutoCodeItem Cur;
		///<summary></summary>
		public static AutoCodeItem[] List;//all
		///<summary></summary>
		public static AutoCodeItem[] ListForCode;//all items for a specific AutoCode
		///<summary></summary>
		public static Hashtable HList;//key=ADACode,value=AutoCodeNum

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from autocodeitem";
			FillTable();
			HList=new Hashtable();
			List=new AutoCodeItem[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i].AutoCodeItemNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].AutoCodeNum    = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].ADACode        = PIn.PString(table.Rows[i][2].ToString());
				if(!HList.ContainsKey(List[i].ADACode)){
					HList.Add(List[i].ADACode,List[i].AutoCodeNum);
				}
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO autocodeitem (autocodenum,adacode) "
				+"VALUES ("
				+"'"+POut.PInt   (Cur.AutoCodeNum)+"', "
				+"'"+POut.PString(Cur.ADACode)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.AutoCodeItemNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE autocodeitem SET "
				+"autocodenum='"+POut.PInt   (Cur.AutoCodeNum)+"'"
				+",adacode ='"  +POut.PString(Cur.ADACode)+"'"
				+" WHERE autocodeitemnum = '"+POut.PInt(Cur.AutoCodeItemNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from autocodeitem WHERE autocodeitemnum = '"
				+POut.PInt(Cur.AutoCodeItemNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void Delete(int autoCodeNum){
			cmd.CommandText = "DELETE from autocodeitem WHERE AutoCodeNum = '"
				+POut.PInt(autoCodeNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void GetListForCode(int autoCodeNum){
			//loop through AutoCodeItems.List to fill ListForCode
			ArrayList ALtemp=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].AutoCodeNum==autoCodeNum){
					ALtemp.Add(List[i]);
				} 
			}
			ListForCode=new AutoCodeItem[ALtemp.Count];
			if(ALtemp.Count>0){
				ALtemp.CopyTo(ListForCode);
			}     
		}

		///<summary></summary>
		public static string GetADA(int autoCodeNum,string toothNum,string surf,bool isAdditional){
			bool allCondsMet;
			GetListForCode(autoCodeNum);
			if(ListForCode.Length==0){
				return "";
			}
			for(int i=0;i<ListForCode.Length;i++){
				AutoCodeConds.GetListForItem(ListForCode[i].AutoCodeItemNum);
				allCondsMet=true;
				for(int j=0;j<AutoCodeConds.ListForItem.Length;j++){
					if(!AutoCodeConds.ConditionIsMet
						(AutoCodeConds.ListForItem[j].Condition,toothNum,surf,isAdditional)){
						allCondsMet=false;
					}
				}
				if(allCondsMet){
					return ListForCode[i].ADACode;
				}
			}
			return ListForCode[0].ADACode;//if couldn't find a better match
		}

		///<summary></summary>
		public static string VerifyCode(string ADACode,string toothNum,string surf,bool isAdditional){
			bool allCondsMet;
			if(!AutoCodeItems.HList.ContainsKey(ADACode)){
				return ADACode;
			}
			//AutoCode verAutoCode=(AutoCode)HList[ADACode];
			GetListForCode((int)HList[ADACode]);
			for(int i=0;i<ListForCode.Length;i++){
				AutoCodeConds.GetListForItem(ListForCode[i].AutoCodeItemNum);
				allCondsMet=true;
				for(int j=0;j<AutoCodeConds.ListForItem.Length;j++){
					if(!AutoCodeConds.ConditionIsMet
						(AutoCodeConds.ListForItem[j].Condition,toothNum,surf,isAdditional)){
						allCondsMet=false;
					}
				}
				if(allCondsMet){
					return ListForCode[i].ADACode;
				}
			}
			return ADACode;//if couldn't find a better match
		}

	}

	///<summary>Corresponds to the autocodeitem table in the database.</summary>
	///<remarks>There are multiple AutoCodeItems for a given AutoCode.  Each Item has one ADA code.</remarks>
	public struct AutoCodeItem{
		///<summary>Primary key.</summary>
		public int AutoCodeItemNum;
		///<summary>Foreign key to AutoCode.AutoCodeNum</summary>
		public int AutoCodeNum;
		///<summary>Foreign key to ProcedureCode.ADACode</summary>
		public string ADACode;
	}
	
	/*=========================================================================================
	=================================== class Claims ==========================================*/
	///<summary></summary>
	public class Claims:DataClass{
		///<summary></summary>
		public static Claim[] List;
		///<summary></summary>
		public static Hashtable HList;
		///<summary></summary>
		public static Claim Cur;
		///<summary></summary>
		public static QueueItem[] ListQueue;
		///<summary></summary>
		public static QueueItem CurQueue;

		///<summary></summary>
		public static void RefreshByCheck(int claimPaymentNum, bool showUnattached){
			cmd.CommandText =
				"SELECT claim.dateservice,claim.provtreat,CONCAT(patient.lname,', ',patient.fname)"
				+",insplan.carrier,SUM(claimproc.feebilled),SUM(claimproc.inspayamt),claim.claimnum"
				+",claimproc.claimpaymentnum"
				+" FROM claim,patient,insplan,claimproc"
				+" WHERE claimproc.claimnum = claim.claimnum"
				+" && patient.patnum = claim.patnum"
				+" && insplan.plannum = claim.plannum"
				+" && (claimproc.status = '1' || claimproc.status = '4')"//received or supplemental
 				+" && (claimproc.claimpaymentnum = '"+claimPaymentNum+"'";
			if(showUnattached){
				cmd.CommandText+=" || (claimproc.inspayamt > 0 && claimproc.claimpaymentnum = '0'))"
					+" GROUP BY claimproc.claimnum";
			}
			else{//shows only items attached to this payment
				cmd.CommandText+=")"
					+" GROUP BY claimproc.claimnum";
			}
			//MessageBox.Show(
			FillTable();
			ListQueue=new QueueItem[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				ListQueue[i].DateClaim      =PIn.PDate  (table.Rows[i][0].ToString());
				ListQueue[i].ProvAbbr       =Providers.GetAbbr(PIn.PInt(table.Rows[i][1].ToString()));
				ListQueue[i].PatName        =PIn.PString(table.Rows[i][2].ToString());
				ListQueue[i].Carrier        =PIn.PString(table.Rows[i][3].ToString());
				ListQueue[i].FeeBilled      =PIn.PDouble(table.Rows[i][4].ToString());
				ListQueue[i].InsPayAmt      =PIn.PDouble(table.Rows[i][5].ToString());
				ListQueue[i].ClaimNum       =PIn.PInt   (table.Rows[i][6].ToString());
				ListQueue[i].ClaimPaymentNum=PIn.PInt   (table.Rows[i][7].ToString());
			}
		}

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * FROM claim"
				+" WHERE patnum = '"+Patients.Cur.PatNum+"'"
				+" ORDER BY dateservice";
			FillTable();
			List=new Claim[table.Rows.Count];
			HList=new Hashtable();
			for(int i=0;i<table.Rows.Count;i++){
				List[i].ClaimNum     =		PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum       =		PIn.PInt   (table.Rows[i][1].ToString());
				List[i].DateService  =		PIn.PDate  (table.Rows[i][2].ToString());
				List[i].DateSent     =		PIn.PDate  (table.Rows[i][3].ToString());
				List[i].ClaimStatus  =		PIn.PString(table.Rows[i][4].ToString());
				List[i].DateReceived =		PIn.PDate  (table.Rows[i][5].ToString());
				List[i].PlanNum      =		PIn.PInt   (table.Rows[i][6].ToString());
				List[i].ProvTreat    =		PIn.PInt   (table.Rows[i][7].ToString());
				List[i].ClaimFee     =		PIn.PDouble(table.Rows[i][8].ToString());
				List[i].InsPayEst    =		PIn.PDouble(table.Rows[i][9].ToString());
				List[i].InsPayAmt    =		PIn.PDouble(table.Rows[i][10].ToString());
				List[i].DedApplied   =		PIn.PDouble(table.Rows[i][11].ToString());
				List[i].PreAuthString=		PIn.PString(table.Rows[i][12].ToString());
				List[i].IsProsthesis =		PIn.PString(table.Rows[i][13].ToString());
				List[i].PriorDate    =		PIn.PDate  (table.Rows[i][14].ToString());
				List[i].ReasonUnderPaid=	PIn.PString(table.Rows[i][15].ToString());
				List[i].ClaimNote    =		PIn.PString(table.Rows[i][16].ToString());
				List[i].ClaimType    =    PIn.PString(table.Rows[i][17].ToString());
				List[i].ProvBill     =		PIn.PInt   (table.Rows[i][18].ToString());
				List[i].ReferringProv=		PIn.PInt   (table.Rows[i][19].ToString());
				List[i].RefNumString =		PIn.PString(table.Rows[i][20].ToString());
				List[i].PlaceService = (PlaceOfService)PIn.PInt(table.Rows[i][21].ToString());
				List[i].AccidentRelated=	PIn.PString(table.Rows[i][22].ToString());
				List[i].AccidentDate  =		PIn.PDate  (table.Rows[i][23].ToString());
				List[i].AccidentST    =		PIn.PString(table.Rows[i][24].ToString());
				List[i].EmployRelated=(YN)PIn.PInt   (table.Rows[i][25].ToString());
				List[i].IsOrtho       =		PIn.PBool  (table.Rows[i][26].ToString());
				List[i].OrthoRemainM  =		PIn.PInt   (table.Rows[i][27].ToString());
				List[i].OrthoDate     =		PIn.PDate  (table.Rows[i][28].ToString());
				List[i].PatRelat      =(Relat)PIn.PInt(table.Rows[i][29].ToString());
				List[i].PlanNum2      =   PIn.PInt   (table.Rows[i][30].ToString());
				List[i].PatRelat2     =(Relat)PIn.PInt(table.Rows[i][31].ToString());
				List[i].WriteOff      =   PIn.PDouble(table.Rows[i][32].ToString());
				HList.Add(List[i].ClaimNum,List[i]);
			}//end for
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO claim (patnum,dateservice,datesent,claimstatus,datereceived"
				+",plannum,provtreat,claimfee,inspayest,inspayamt,dedapplied"
				+",preauthstring,isprosthesis,priordate,reasonunderpaid,claimnote"
				+",claimtype,provbill,referringprov"
				+",refnumstring,placeservice,accidentrelated,accidentdate,accidentst"
				+",employrelated,isortho,orthoremainm,orthodate,patrelat,plannum2"
				+",patrelat2,writeoff) VALUES("
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PDate  (Cur.DateService)+"', "
				+"'"+POut.PDate  (Cur.DateSent)+"', "
				+"'"+POut.PString(Cur.ClaimStatus)+"', "
				+"'"+POut.PDate  (Cur.DateReceived)+"', "
				+"'"+POut.PInt   (Cur.PlanNum)+"', "
				+"'"+POut.PInt   (Cur.ProvTreat)+"', "
				+"'"+POut.PDouble(Cur.ClaimFee)+"', "
				+"'"+POut.PDouble(Cur.InsPayEst)+"', "
				+"'"+POut.PDouble(Cur.InsPayAmt)+"', "
				+"'"+POut.PDouble(Cur.DedApplied)+"', "
				+"'"+POut.PString(Cur.PreAuthString)+"', "
				+"'"+POut.PString(Cur.IsProsthesis)+"', "
				+"'"+POut.PDate  (Cur.PriorDate)+"', "
				+"'"+POut.PString(Cur.ReasonUnderPaid)+"', "
				+"'"+POut.PString(Cur.ClaimNote)+"', "
				+"'"+POut.PString(Cur.ClaimType)+"', "
				+"'"+POut.PInt   (Cur.ProvBill)+"', "
				+"'"+POut.PInt   (Cur.ReferringProv)+"', "
				+"'"+POut.PString(Cur.RefNumString)+"', "
				+"'"+POut.PInt   ((int)Cur.PlaceService)+"', "
				+"'"+POut.PString(Cur.AccidentRelated)+"', "
				+"'"+POut.PDate  (Cur.AccidentDate)+"', "
				+"'"+POut.PString(Cur.AccidentST)+"', "
				+"'"+POut.PInt   ((int)Cur.EmployRelated)+"', "
				+"'"+POut.PBool  (Cur.IsOrtho)+"', "
				+"'"+POut.PInt   (Cur.OrthoRemainM)+"', "
				+"'"+POut.PDate  (Cur.OrthoDate)+"', "
				+"'"+POut.PInt   ((int)Cur.PatRelat)+"', "
				+"'"+POut.PInt   (Cur.PlanNum2)+"', "
				+"'"+POut.PInt   ((int)Cur.PatRelat2)+"', "
				+"'"+POut.PDouble(Cur.WriteOff)+"')";
			NonQ(true);
			Cur.ClaimNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE claim SET "
				+"patnum = '"          +POut.PInt   (Cur.PatNum)+"' "
				+",dateservice = '"    +POut.PDate  (Cur.DateService)+"' "
				+",datesent = '"       +POut.PDate  (Cur.DateSent)+"' "
				+",claimstatus = '"    +POut.PString(Cur.ClaimStatus)+"' "
				+",datereceived = '"   +POut.PDate  (Cur.DateReceived)+"' "
				+",plannum = '"        +POut.PInt   (Cur.PlanNum)+"' "
				+",provtreat = '"      +POut.PInt   (Cur.ProvTreat)+"' "
				+",claimfee = '"       +POut.PDouble(Cur.ClaimFee)+"' "
				+",inspayest = '"      +POut.PDouble(Cur.InsPayEst)+"' "
				+",inspayamt = '"      +POut.PDouble(Cur.InsPayAmt)+"' "
				+",dedapplied = '"   +  POut.PDouble(Cur.DedApplied)+"' "
				+",preauthstring = '"+	POut.PString(Cur.PreAuthString)+"' "
				+",isprosthesis = '" +	POut.PString(Cur.IsProsthesis)+"' "
				+",priordate = '"    +	POut.PDate  (Cur.PriorDate)+"' "
				+",reasonunderpaid = '"+POut.PString(Cur.ReasonUnderPaid)+"' "
				+",claimnote = '"    +	POut.PString(Cur.ClaimNote)+"' "
				+",claimtype='"      +	POut.PString(Cur.ClaimType)+"' "
				+",provbill = '"     +	POut.PInt   (Cur.ProvBill)+"' "
				+",referringprov = '"+	POut.PInt   (Cur.ReferringProv)+"' "
				+",refnumstring = '" +	POut.PString(Cur.RefNumString)+"' "
				+",placeservice = '" +	POut.PInt   ((int)Cur.PlaceService)+"' "
				+",accidentrelated = '"+POut.PString(Cur.AccidentRelated)+"' "
				+",accidentdate = '" +	POut.PDate  (Cur.AccidentDate)+"' "
				+",accidentst = '"   +	POut.PString(Cur.AccidentST)+"' "
				+",employrelated = '"+	POut.PInt   ((int)Cur.EmployRelated)+"' "
				+",isortho = '"      +	POut.PBool  (Cur.IsOrtho)+"' "
				+",orthoremainm = '" +	POut.PInt   (Cur.OrthoRemainM)+"' "
				+",orthodate = '"    +	POut.PDate  (Cur.OrthoDate)+"' "
				+",patrelat = '"     +	POut.PInt   ((int)Cur.PatRelat)+"' "
				+",plannum2 = '"     +	POut.PInt   (Cur.PlanNum2)+"' "
				+",patrelat2 = '"    +	POut.PInt   ((int)Cur.PatRelat2)+"' "
				+",writeoff = '"     +	POut.PDouble(Cur.WriteOff)+"' "
				+"WHERE claimnum = '"+	POut.PInt   (Cur.ClaimNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM claim "
				+"WHERE claimnum = '"+POut.PInt(Cur.ClaimNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		//public static void GetProcsInClaim(int myClaimNum){
			//moved to claimprocs

		///<summary></summary>
		public static void DetachProcsFromClaim(){
			cmd.CommandText = "UPDATE procedurelog SET "
				+"claimnum = '0' "
				+"WHERE claimnum = '"+POut.PInt(Cur.ClaimNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		/*public static void DetachAllFromCheck(int myClaimPaymentNum){
			cmd.CommandText = "UPDATE claim SET "
				+"ClaimPaymentNum = '0' "
				+"WHERE claimpaymentNum = '"+myClaimPaymentNum+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}*/

		/*public double GetFee(int myClaimNum){
			//not in use anymore.  Only here for example for later
			double retVal=0;
			for (int i = 0; i<Procedures.List.Length; i++){
				if(Procedures.List[i].ClaimNum==myClaimNum){
					retVal+=Procedures.List[i].ProcFee;
				}//end if
			}//end for i
			return 0;
		}*/

		/*public double GetEst(int myClaimNum){
			//not in use anymore.  See Procedures.GetEstForCur
			CovPats CovPats=new CovPats();
			double retVal=0;
			for (int i = 0; i<Procedures.List.Length; i++){
			//fix: percentage off
				if(Procedures.List[i].ClaimNum==myClaimNum){
					retVal+=CovPats.GetPercent(Procedures.List[i].ADACode,PercentType.Pri)*Procedures.List[i].ProcFee;
				}//end if
			}//end for i
			return 0;
		}*/

		/*public static double ComputeBal(){//must make sure Refresh is done first
			this has all been moved to claimprocs
			double retVal=0;
			double pat;
			//for(int i=0;i<Ledgers.List.Length;i++){
			//	retVal+=Ledgers.List[i].ClaimPays;
			//	retVal+=Ledgers.List[i].InsEst;
			//}
			for(int i=0;i<List.Length;i++){
				if(List[i].ClaimStatus=="A"){
					continue;//claim adjustments do not affect patient balance
				}
				if(List[i].ClaimType=="PreAuth"){
					continue;//preauthorizations do not affect patient balance
				}
				//if(List[i].ClaimStatus=="R"){
				//	retVal+=List[i].InsPayAmt;
				//}
				//else{
				//	retVal+=List[i].InsPayEst;
				//}
				pat=0;
				if(List[i].ClaimStatus=="R"){
					pat=List[i].ClaimFee-List[i].InsPayAmt;
				}
				else{
					pat=List[i].ClaimFee-List[i].InsPayEst;
				}
				//if(List[i].ClaimNum==List[i].PriClaimNum){//is pri claim
				//	if(List[i].SecClaimNum==0){//no sec claim exists
				//		retVal+=pat;
				//	}
				//}
				//else{//sec claim
				//	if(((Claim)HList[List[i].PriClaimNum]).ClaimStatus=="R"){
				//		pat-=((Claim)HList[List[i].PriClaimNum]).InsPayAmt;
				//	}
				//	else{
				//		pat-=((Claim)HList[List[i].PriClaimNum]).InsPayEst;
				//	}
				//	retVal+=pat;
				//}
			}
			return retVal;
		}*/

		///<summary></summary>
		public static void GetQueueList(){
			cmd.CommandText =
				"SELECT T1.claimnum,T2.nosendelect,concat(T3.lname,', ',T3.fname,' ',T3.middlei)"
				+",T1.claimstatus,T2.Carrier,T3.patnum "
				+"FROM claim as T1 "
				+"Left join insplan as T2 on T1.plannum = T2.plannum "
				+"left join patient as T3 on T1.patnum = T3.patnum "
				+"WHERE T1.claimstatus = 'W' || T1.claimstatus = 'P'";
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			ListQueue=new QueueItem[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				ListQueue[i].ClaimNum   = PIn.PInt   (table.Rows[i][0].ToString());
				ListQueue[i].NoSendElect= PIn.PBool  (table.Rows[i][1].ToString());
				ListQueue[i].PatName    = PIn.PString(table.Rows[i][2].ToString());
				ListQueue[i].ClaimStatus= PIn.PString(table.Rows[i][3].ToString());
				ListQueue[i].Carrier    = PIn.PString(table.Rows[i][4].ToString());
				ListQueue[i].PatNum     = PIn.PInt   (table.Rows[i][5].ToString());
			}
		}

		///<summary></summary>
		public static void UpdateStatus(int claimNum,string newStatus){
			cmd.CommandText = "UPDATE claim SET "
				+"claimstatus = '"+newStatus+"' "
				+"WHERE claimnum = '"+claimNum+"'";
			NonQ(false);
		}

	}//end class Claims

	///<summary>Corresponds to the claim table in the database.</summary>
	///<remarks>The claim table holds information about individual claims.  Each row represents one claim.</remarks>
	public struct Claim{
		///<summary>Primary key</summary>
		public int ClaimNum;
		///<summary>Foreign key to <see cref="Patient.PatNum">patient.PatNum</see></summary>
		public int PatNum;//
		///<summary>Usually the same date as the procedures, but it can be changed if you wish.</summary>
		public DateTime DateService;//
		///<summary>Usually the date it was created.  It might be sent a few days later if you don't send your e-claims every day.</summary>
		public DateTime DateSent;
		///<summary>Single char: U,H,W,P,S,or R.</summary>
		///<remarks>U=Unsent, H=Hold until pri received, W=Waiting in queue, P=Probably sent, S=Sent, R=Received.  A(adj) is no longer used.</remarks>
		public string ClaimStatus;//
		///<summary>Date the claim was received.</summary>
		public DateTime DateReceived;
		///<summary>Foreign key to InsPlan.PlanNum</summary>
		public int PlanNum;
		///<summary>Treating provider. Foreign key to Provider.ProvNum.</summary>
		public int ProvTreat;//
		///<summary>Total fee of claim.</summary>
		public double ClaimFee;
		///<summary>Amount insurance is estimated to pay on this claim.</summary>
		public double InsPayEst;
		///<summary>Amount insurance actually paid.</summary>
		public double InsPayAmt;
		///<summary>Deductible applied to this claim.</summary>
		public double DedApplied;
		///<summary>The preauth number received from ins.</summary>
		public string PreAuthString;
		///<summary>single char for No, Initial, or Replacement.</summary>
		public string IsProsthesis;
		///<summary>Date prior prosthesis was placed.</summary>
		public DateTime PriorDate;
		///<summary>Note for patient for why insurance didn't pay as expected.</summary>
		public string ReasonUnderPaid;//
		///<summary>Note to be sent to insurance.</summary>
		public string ClaimNote;//
		///<summary>"P"=primary, "S"=secondary, "PreAuth"=preauth, "Other"=other, "Cap"=capitation</summary>
		///<remarks>ClaimType is the new way of determining claimtype. Not allowed to be blank. The update for version 2.1 added "P" or "S" to all existing claims.</remarks>
		public string ClaimType;
		///<summary>Billing provider.  Foreign key to Provider.ProvNum.</summary>
		public int ProvBill;
		///<summary>Foreign key to Referral.ReferralNum.</summary>
		public int ReferringProv;
		///<summary>Referral number for this claim.</summary>
		public string RefNumString;
		///<summary>See the PlaceOfService enum.</summary>
		public PlaceOfService PlaceService;
		///<summary>blank or A=Auto, E=Employment, O=Other.</summary>
		public string AccidentRelated;
		///<summary>Date of accident, if applicable.</summary>
		public DateTime AccidentDate;
		///<summary>Accident state.</summary>
		public string AccidentST;
		///<summary>See the YN enum.</summary>
		public YN EmployRelated;
		///<summary>True if is ortho.</summary>
		public bool IsOrtho;
		///<summary>Remaining months of ortho. Valid values are 1-36.</summary>
		public int OrthoRemainM;
		///<summary>Date ortho appliance placed.</summary>
		public DateTime OrthoDate;
		///<summary>Relationship to subscriber.  See the Relat enumeration.</summary>
		///<remarks>You no longer have to look in patient to find the relationship, since it is copied over when the claim is created.</remarks>
		public Relat PatRelat;
		///<summary>Other coverage plan number.  Foreign key to InsPlan.PlanNum for other coverage.  0 if none.</summary>
		///<remarks>This provides the user with total control over what other coverage shows. This obviously limits the coverage on a single claim to two insurance companies.</remarks>
		public int PlanNum2;
		///<summary>The relationsip to the subscriber for other coverage on this claim.</summary>
		public Relat PatRelat2;
		///<summary>Sum of ClaimProc.Writeoff for this claim.</summary>
		public double WriteOff;
	}

	///<summary>Used to hold a list of claims to show in the claims 'queue' waiting to be sent, and in the Claim Check Edit window.</summary>
	public struct QueueItem{
		///<summary></summary>
		public int ClaimNum;
		///<summary></summary>
		public bool NoSendElect;
		///<summary></summary>
		public string PatName;
		///<summary>Single char: U,H,W,P,S,or R.</summary>
		///<remarks>U=Unsent, H=Hold until pri received, W=Waiting in queue, P=Probably sent, S=Sent, R=Received.  A(adj) is no longer used.</remarks>
		public string ClaimStatus;
		///<summary></summary>
		public string Carrier;
		///<summary></summary>
		public int PatNum;
		///<summary></summary>
		public DateTime DateClaim;
		///<summary></summary>
		public string ProvAbbr;
		///<summary></summary>
		public double FeeBilled;
		///<summary></summary>
		public double InsPayAmt;
		///<summary></summary>
		public int ClaimPaymentNum;
	}

/*=========================================================================================
		=================================== class ClaimForms ==========================================*/

	///<summary></summary>
	public class ClaimForms:DataClass{
		///<summary>List of all claim forms.</summary>
		public static ClaimForm[] ListLong;
		///<summary>List of all claim forms except those marked as hidden.</summary>
		public static ClaimForm[] ListShort;
		///<summary></summary>
		public static ClaimForm Cur;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * FROM claimform";
			FillTable();
			ListLong=new ClaimForm[table.Rows.Count];
			ArrayList tempAL=new ArrayList();
			for(int i=0;i<table.Rows.Count;i++){
				ListLong[i].ClaimFormNum= PIn.PInt   (table.Rows[i][0].ToString());
				ListLong[i].Description = PIn.PString(table.Rows[i][1].ToString());
				ListLong[i].IsHidden    = PIn.PBool  (table.Rows[i][2].ToString());
				ListLong[i].FontName    = PIn.PString(table.Rows[i][3].ToString());
				ListLong[i].FontSize    = PIn.PFloat (table.Rows[i][4].ToString());
				ListLong[i].UniqueID    = PIn.PInt   (table.Rows[i][5].ToString());
				ListLong[i].PrintImages = PIn.PBool  (table.Rows[i][6].ToString());
				ListLong[i].OffsetX     = PIn.PInt   (table.Rows[i][7].ToString());
				ListLong[i].OffsetY     = PIn.PInt   (table.Rows[i][8].ToString());
				if(!ListLong[i].IsHidden)
					tempAL.Add(ListLong[i]);
			}
			ListShort=new ClaimForm[tempAL.Count];
			for(int i=0;i<ListShort.Length;i++){
				ListShort[i]=(ClaimForm)tempAL[i];
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO claimform (description,ishidden,fontname,fontsize"
				+",uniqueid,printimages,offsetx,offsety) VALUES("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PBool  (Cur.IsHidden)+"', "
				+"'"+POut.PString(Cur.FontName)+"', "
				+"'"+POut.PFloat (Cur.FontSize)+"', "
				+"'"+POut.PInt   (Cur.UniqueID)+"', "
				+"'"+POut.PBool  (Cur.PrintImages)+"', "
				+"'"+POut.PInt   (Cur.OffsetX)+"', "
				+"'"+POut.PInt   (Cur.OffsetY)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.ClaimFormNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE claimform SET "
				+"description = '" +POut.PString(Cur.Description)+"' "
				+",ishidden = '"    +POut.PBool  (Cur.IsHidden)+"' "
				+",fontname = '"    +POut.PString(Cur.FontName)+"' "
				+",fontsize = '"    +POut.PFloat (Cur.FontSize)+"' "
				+",uniqueid = '"    +POut.PInt   (Cur.UniqueID)+"' "
				+",printimages = '" +POut.PBool  (Cur.PrintImages)+"' "
				+",offsetx = '"     +POut.PInt   (Cur.OffsetX)+"' "
				+",offsety = '"     +POut.PInt   (Cur.OffsetY)+"' "
				+"WHERE ClaimFormNum = '"+POut.PInt   (Cur.ClaimFormNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void SetCur(int claimFormNum){
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ClaimFormNum==claimFormNum){
					Cur=ListLong[i];
					return;
				}
			}
			MessageBox.Show("Error. Could not locate Claim Form.");
		}

		///<summary> Called when cancelling out of creating a new claimform, and from the claimform window when clicking delete. Returns true if successful or false if dependencies found.</summary>
		public static bool DeleteCur(){
			//first, do dependency testing
			cmd.CommandText="SELECT * FROM insplan WHERE claimformnum = '"
				+Cur.ClaimFormNum.ToString()+"' LIMIT 1";
			FillTable();
			if(table.Rows.Count==1){
				return false;
			}
			cmd.CommandText="SELECT * FROM instemplate WHERE claimformnum = '"
				+Cur.ClaimFormNum.ToString()+"' LIMIT 1";
			FillTable();
			if(table.Rows.Count==1){
				return false;
			}
			//Then, delete the claimform
			cmd.CommandText = "DELETE FROM claimform "
				+"WHERE ClaimFormNum = '"+POut.PInt(Cur.ClaimFormNum)+"'";
			NonQ(false);
			cmd.CommandText = "DELETE FROM claimformitem "
				+"WHERE ClaimFormNum = '"+POut.PInt(Cur.ClaimFormNum)+"'";
			NonQ(false);
			return true;
		}


	}

	///<summary>Corresponds to the claimform table in the database.</summary>
	///<remarks>Stores the information for printing different types of claim forms.</remarks>
	public struct ClaimForm{
		///<summary>Primary key.</summary>
		public int ClaimFormNum;
		///<summary>eg. ADA2002 or CA Medicaid</summary>
		public string Description;
		///<summary>If true, then it will not be displayed in various claim form lists as a choice.</summary>
		public bool IsHidden;
		///<summary>Valid font name for all text on the form.</summary>
		public string FontName;
		///<summary>Font size for all text on the form.</summary>
		public float FontSize;
		///<summary>Assigned by us for maintenance purposes. Do not change.
		///Will be 0 for claim forms added by user, protecting them from being changed by us.</summary>
		public int UniqueID;
		///<summary>Set to false to not print images.  This removes the background for printing on premade forms.</summary>
		public bool PrintImages;
		///<summary>Shifts all items by x/100th's of an inch to compensate for printer, typically less than 1/4 inch.</summary>
		public int OffsetX;
		///<summary>Shifts all items by y/100th's of an inch to compensate for printer, typically less than 1/4 inch.</summary>
		public int OffsetY;
	}

	/*=========================================================================================
		=================================== class ClaimFormItems ==========================================*/

	///<summary></summary>
	public class ClaimFormItems:DataClass{
		///<summary></summary>
		public static ClaimFormItem[] List;
		///<summary></summary>
		public static ClaimFormItem Cur;
		///<summary></summary>
		public static ClaimFormItem[] ListForForm;

		///<summary>Gets all claimformitems for all claimforms.  Items for individual claimforms can later be extracted as needed.</summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * FROM claimformitem ORDER BY imagefilename desc";
			FillTable();
			List=new ClaimFormItem[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].ClaimFormItemNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ClaimFormNum    = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].ImageFileName   = PIn.PString(table.Rows[i][2].ToString());
				List[i].FieldName       = PIn.PString(table.Rows[i][3].ToString());
				List[i].FormatString    = PIn.PString(table.Rows[i][4].ToString());
				List[i].XPos            = PIn.PFloat (table.Rows[i][5].ToString());
				List[i].YPos            = PIn.PFloat (table.Rows[i][6].ToString());
				List[i].Width           = PIn.PFloat (table.Rows[i][7].ToString());
				List[i].Height          = PIn.PFloat (table.Rows[i][8].ToString());
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO claimformitem (claimformnum,imagefilename,fieldname,formatstring"
				+",xpos,ypos,width,height) VALUES("
				+"'"+POut.PInt   (Cur.ClaimFormNum)+"', "
				+"'"+POut.PString(Cur.ImageFileName)+"', "
				+"'"+POut.PString(Cur.FieldName)+"', "
				+"'"+POut.PString(Cur.FormatString)+"', "
				+"'"+POut.PFloat (Cur.XPos)+"', "
				+"'"+POut.PFloat (Cur.YPos)+"', "
				+"'"+POut.PFloat (Cur.Width)+"', "
				+"'"+POut.PFloat (Cur.Height)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.ClaimFormItemNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE claimformitem SET "
				+"claimformnum = '" +POut.PInt   (Cur.ClaimFormNum)+"' "
				+",imagefilename = '"+POut.PString(Cur.ImageFileName)+"' "
				+",fieldname = '"    +POut.PString(Cur.FieldName)+"' "
				+",formatstring = '" +POut.PString(Cur.FormatString)+"' "
				+",xpos = '"         +POut.PFloat (Cur.XPos)+"' "
				+",ypos = '"         +POut.PFloat (Cur.YPos)+"' "
				+",width = '"        +POut.PFloat (Cur.Width)+"' "
				+",height = '"       +POut.PFloat (Cur.Height)+"' "
				+"WHERE ClaimFormItemNum = '"+POut.PInt   (Cur.ClaimFormItemNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM claimformitem "
				+"WHERE ClaimFormItemNum = '"+POut.PInt(Cur.ClaimFormItemNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary>Gets all claimformitems for the current claimform from the preloaded List.</summary>
		public static void GetListForForm(){
			ArrayList tempAL=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ClaimFormNum==ClaimForms.Cur.ClaimFormNum){
					tempAL.Add(List[i]);
				}
			}
			ListForForm=new ClaimFormItem[tempAL.Count];
			tempAL.CopyTo(ListForForm);
		}


	}

	///<summary>Corresponds to the claimformitem table in the database.</summary>
	///<remarks>One item is needed for each field on a claimform.</remarks>
	public struct ClaimFormItem{
		///<summary>Primary key.</summary>
		public int ClaimFormItemNum;
		///<summary>Foreign key to ClaimForm.</summary>
		public int ClaimFormNum;
		///<summary>If this item is an image.  Usually only one per claim.  eg ADA2002.emf</summary>
		public string ImageFileName;
		///<summary>Must be one of the hardcoded available fieldnames for claims.</summary>
		public string FieldName;//
		///<summary>For dates, the format string. ie MM/dd/yyyy or M d y among many other possibilities.</summary>
		public string FormatString;
		///<summary>The x position of the item on the claim form</summary>
		///<remarks>In pixels. 100 pixels per inch.</remarks>
		public float XPos;
		///<summary>The y position.</summary>
		public float YPos;
		///<summary>Limits the printable area of the item. Set to zero to not limit.</summary>
		public float Width;
		///<summary>Limits the printable area of the item. Set to zero to not limit.</summary>
		public float Height;
	}

	/*=========================================================================================
		=================================== class ClaimPayments ==========================================*/
	///<summary></summary>
	public class ClaimPayments:DataClass{
		///<summary></summary>
		public static ClaimPayment[] List;
		///<summary></summary>
		public static ClaimPayment Cur;

		///<summary></summary>
		public static void GetForClaim(){	
		//public static void GetCheck(int claimPaymentNum){
			cmd.CommandText =
				"SELECT claimpayment.claimpaymentnum,claimpayment.checkdate,SUM(claimproc.inspayamt)"
				//claimpayment.checkamt"
				+",claimpayment.checknum,claimpayment.bankbranch,claimpayment.note"
				+" FROM claimpayment,claimproc"
				+" WHERE claimpayment.claimpaymentnum = claimproc.claimpaymentnum"
				+" && claimproc.claimnum = '"+Claims.Cur.ClaimNum.ToString()+"'"
				+" GROUP BY claimpayment.claimpaymentnum,claimproc.claimnum";
				//+" WHERE ClaimPaymentnum = '"+claimPaymentNum+"'";
			FillTable();
			List=new ClaimPayment[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].ClaimPaymentNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].CheckDate    = PIn.PDate  (table.Rows[i][1].ToString());
				//warning.  This is not the actual amount of the check, but only of a portion of it
				List[i].CheckAmt     = PIn.PDouble(table.Rows[i][2].ToString());
				List[i].CheckNum     = PIn.PString(table.Rows[i][3].ToString());
				List[i].BankBranch   = PIn.PString(table.Rows[i][4].ToString());
				List[i].Note         = PIn.PString(table.Rows[i][5].ToString());
			}
			//MessageBox.Show(List.Length.ToString()+cmd.CommandText);
			//if(List.Length==1)
			//	Cur=List[0];
			//else
			//	Cur=new ClaimPayment();
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO claimpayment (checkdate,checkamt,checknum,"
				+"bankbranch,note) VALUES("
				+"'"+POut.PDate  (Cur.CheckDate)+"', "
				+"'"+POut.PDouble(Cur.CheckAmt)+"', "
				+"'"+POut.PString(Cur.CheckNum)+"', "
				+"'"+POut.PString(Cur.BankBranch)+"', "
				+"'"+POut.PString(Cur.Note)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.ClaimPaymentNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE claimpayment SET "
				+"checkdate = '"   +POut.PDate  (Cur.CheckDate)+"' "
				+",checkamt = '"   +POut.PDouble(Cur.CheckAmt)+"' "
				+",checknum = '"   +POut.PString(Cur.CheckNum)+"' "
				+",bankbranch = '" +POut.PString(Cur.BankBranch)+"' "
				+",note = '"       +POut.PString(Cur.Note)+"' "
				+"WHERE ClaimPaymentnum = '"+POut.PInt   (Cur.ClaimPaymentNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM claimpayment "
				+"WHERE ClaimPaymentnum = '"+POut.PInt(Cur.ClaimPaymentNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}
	}//end class ClaimPayments

	///<summary>Corresponds to the claimpayment table in the database.</summary>
	///<remarks>Each row represents a single check from the insurance company.  The amount may be split between patients using claimprocs.  The amount of the check must always exactly equal the sum of all the claimprocs attached to it.  There may be only one claimproc.</remarks>
	public struct ClaimPayment{
		///<summary>Primary key.</summary>
		public int ClaimPaymentNum;
		///<summary>Date the check was entered into this system, not the date on the check.</summary>
		public DateTime CheckDate;
		///<summary>The amount of the check.</summary>
		public Double CheckAmt;
		///<summary>The check number.</summary>
		public string CheckNum;
		///<summary>Bank and branch.</summary>
		public string BankBranch;
		///<summary>Note for this check if needed.</summary>
		public string Note;
	}

	/*=========================================================================================
	=================================== class ClaimProcs ===========================================*/

	///<summary></summary>
	public class ClaimProcs:DataClass{
		///<summary></summary>
		public static ClaimProc Cur;
		///<summary></summary>
		public static ClaimProc[] List;//all for Patients.Cur
		///<summary></summary>
		public static ClaimProc[] ForClaim;//ClaimProcs for Claims.Cur.ClaimNum
		//public static ArrayList ProcsInClaim;//AL of Procedures

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from claimproc "
				+"WHERE PatNum = '"+POut.PInt(Patients.Cur.PatNum)+"' ";
			FillTable();
			List=new ClaimProc[table.Rows.Count];
			for(int i = 0;i<List.Length;i++){
				List[i].ClaimProcNum   = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ProcNum        = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].ClaimNum       = PIn.PInt   (table.Rows[i][2].ToString());	
				List[i].PatNum         = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].ProvNum        = PIn.PInt   (table.Rows[i][4].ToString());
				List[i].FeeBilled      = PIn.PDouble(table.Rows[i][5].ToString());
				List[i].InsPayEst      = PIn.PDouble(table.Rows[i][6].ToString());
				List[i].DedApplied     = PIn.PDouble(table.Rows[i][7].ToString());
				List[i].Status         = (ClaimProcStatus)PIn.PInt(table.Rows[i][8].ToString());
				List[i].InsPayAmt      = PIn.PDouble(table.Rows[i][9].ToString());
				List[i].Remarks        = PIn.PString(table.Rows[i][10].ToString());
				List[i].ClaimPaymentNum= PIn.PInt   (table.Rows[i][11].ToString());
				List[i].PlanNum        = PIn.PInt   (table.Rows[i][12].ToString());
				List[i].DateCP         = PIn.PDate  (table.Rows[i][13].ToString());
				List[i].WriteOff       = PIn.PDouble(table.Rows[i][14].ToString());
				List[i].CodeSent       = PIn.PString(table.Rows[i][15].ToString());
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO claimproc (procnum,claimnum,patnum,provnum"
				+",feebilled,inspayest,dedapplied,status,inspayamt,remarks,claimpaymentnum"
				+",plannum,datecp,writeoff,codesent) VALUES ("
				+"'"+POut.PInt   (Cur.ProcNum)+"', "
				+"'"+POut.PInt   (Cur.ClaimNum)+"', "
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PInt   (Cur.ProvNum)+"', "
				+"'"+POut.PDouble(Cur.FeeBilled)+"', "
				+"'"+POut.PDouble(Cur.InsPayEst)+"', "
				+"'"+POut.PDouble(Cur.DedApplied)+"', "
				+"'"+POut.PInt   ((int)Cur.Status)+"', "
				+"'"+POut.PDouble(Cur.InsPayAmt)+"', "
				+"'"+POut.PString(Cur.Remarks)+"', "
				+"'"+POut.PInt   (Cur.ClaimPaymentNum)+"', "
				+"'"+POut.PInt   (Cur.PlanNum)+"', "
				+"'"+POut.PDate  (Cur.DateCP)+"', "
				+"'"+POut.PDouble(Cur.WriteOff)+"', "
				+"'"+POut.PString(Cur.CodeSent)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.ClaimProcNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE claimproc SET "
				+"procnum = '"        +POut.PInt   (Cur.ProcNum)+"'"
				+",claimnum = '"      +POut.PInt   (Cur.ClaimNum)+"' "
				+",patnum = '"        +POut.PInt   (Cur.PatNum)+"'"
				+",provnum = '"       +POut.PInt   (Cur.ProvNum)+"'"
				+",feebilled = '"     +POut.PDouble(Cur.FeeBilled)+"'"
				+",inspayest = '"     +POut.PDouble(Cur.InsPayEst)+"'"
				+",dedapplied = '"    +POut.PDouble(Cur.DedApplied)+"'"
				+",status = '"        +POut.PInt   ((int)Cur.Status)+"'"
				+",inspayamt = '"     +POut.PDouble(Cur.InsPayAmt)+"'"
				+",remarks = '"       +POut.PString(Cur.Remarks)+"'"
				+",claimpaymentnum= '"+POut.PInt   (Cur.ClaimPaymentNum)+"'"
				+",plannum= '"        +POut.PInt   (Cur.PlanNum)+"'"
				+",datecp= '"         +POut.PDate  (Cur.DateCP)+"'"
				+",writeoff= '"       +POut.PDouble(Cur.WriteOff)+"'"
				+",codesent= '"       +POut.PString(Cur.CodeSent)+"'"
				+" WHERE claimprocnum = '"+POut.PInt (Cur.ClaimProcNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from claimproc WHERE claimprocNum = '"+POut.PInt(Cur.ClaimProcNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void GetForClaim(){
			//MessageBox.Show(List.Length.ToString());
			ArrayList ALForClaim=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ClaimNum==Claims.Cur.ClaimNum){
					ALForClaim.Add(List[i]);  
				}
			}
			ForClaim=new ClaimProc[ALForClaim.Count];
			for(int i=0;i<ALForClaim.Count;i++){
				ForClaim[i]=(ClaimProc)ALForClaim[i];
			}
		}

		///<summary></summary>
		public static bool ProcIsAttached(int procNum){
			//used in ProcEdit, and ContrAcct
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum && List[i].Status!=ClaimProcStatus.Preauth){
					return true;
				}
			}
			return false;
		}

		///<summary></summary>
		public static bool ProcIsSent(int procNum){
			//Warning: In the future, the claim.hlist might not be already loaded and available.
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum){
					if(((Claim)Claims.HList[List[i].ClaimNum]).ClaimStatus != "U"//not unsent
						&& ((Claim)Claims.HList[List[i].ClaimNum]).ClaimStatus != "H"){//not hold
						//must be sent
						return true;
					}
				}
			}
			return false;
		}

		///<summary></summary>
		public static bool ProcIsPaid(int procNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum
					&& List[i].InsPayAmt > 0//ins paid
					&& List[i].Status!=ClaimProcStatus.Preauth){
					return true;
				}
			}
			return false;
		}

		///<summary></summary>
		public static void DetachAllFromCheck(int claimPaymentNum){
			cmd.CommandText = "UPDATE claimproc SET "
				+"ClaimPaymentNum = '0' "
				+"WHERE claimpaymentNum = '"+claimPaymentNum+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void SetForClaim(int claimNum,int claimPaymentNum,bool setAttached){
			cmd.CommandText = "UPDATE claimproc SET ClaimPaymentNum = ";
			if(setAttached){
				cmd.CommandText+="'"+claimPaymentNum+"' ";
			}
			else{
				cmd.CommandText+="'0' ";
			}
			cmd.CommandText+="WHERE claimnum = '"+claimNum+"' && "
				+"inspayamt > 0 && ("
				+"claimpaymentNum = '"+claimPaymentNum+"' || claimpaymentNum = '0')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static double ComputeBal(){//must make sure Refresh is done first
			double retVal=0;
			//double pat;
			for(int i=0;i<List.Length;i++){
				if(List[i].Status==ClaimProcStatus.Adjustment//ins adjustments do not affect patient balance
					|| List[i].Status==ClaimProcStatus.Preauth//preauthorizations do not affect patient balance
					|| List[i].Status==ClaimProcStatus.Capitation//cap procs do not affect patient balance
					){
					continue;
				}
				if(List[i].Status==ClaimProcStatus.Received
					|| List[i].Status==ClaimProcStatus.Supplemental){//because supplemental are always received
					retVal-=List[i].InsPayAmt;
				}
				else{//not recieved
					retVal-=List[i].InsPayEst;
				}
				retVal-=List[i].WriteOff;
			}
			return retVal;
		}


	}

	///<summary>Corresponds to the claimproc table in the database.</summary>
	///<remarks>Links procedures to claims.  Also links ins payments to procedures or claims.  Warning: One proc might be linked twice to a given claim if insurance made two payments.  Many of the important fields are actually optional.  For instance, ProcNum is only required if itemizing ins payment, and ClaimNum is blank if Status=A for adjustment.</remarks>
	public struct ClaimProc{
		///<summary>Primary key.</summary>
		public int ClaimProcNum;
		///<summary>Foreign key to procedurelog.ProcNum.</summary>
		public int ProcNum;
		///<summary>Foreign key to claim.ClaimNum.</summary>
		public int ClaimNum;
		///<summary>Foreign key to patient.PatNum.</summary>
		public int PatNum;
		///<summary>Foreign key to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>Fee billed. Might not be the same as the actual fee.</summary>
		///<remarks>The fee billed can be different than the actual procedure.  For instance, if you have set the insurance plan to bill insurance using UCR fees, then this field will contain the UCR fee instead of the fee that the patient was charged.</remarks>
		public double FeeBilled;
		///<summary>Amount this carrier is expected to pay.</summary>
		public double InsPayEst;
		///<summary>Deductible applied to this procedure only.</summary>
		public double DedApplied;
		///<summary>See the ClaimProcStatus enumeration.</summary>
		public ClaimProcStatus Status;
		///<summary>Amount insurance actually paid.</summary>
		public double InsPayAmt;
		///<summary>The remarks that insurance sends in the EOB about procedures.</summary>
		public string Remarks;
		///<summary>Foreign key to ClaimPayment.ClaimPaymentNum(the insurance check).</summary>
		public int ClaimPaymentNum;
		///<summary>Foreign key to insplan.PlanNum</summary>
		public int PlanNum;
		///<summary>Date of this ClaimProc.  Especially useful for adjustments and balance.</summary>
		///<remarks>For payments, MUST be date of treatment, not payment, to properly track annual benefits.</remarks>
		public DateTime DateCP;
		///<summary>Amount not covered by ins which is written off</summary>
		public Double WriteOff;
		///<summary>The procedure code that was sent to insurance.</summary>
		///<remarks>This is not necessarily the usual procedure code.  It will already have been trimmed to 5 char if it started with "D", or it could be the alternate code.  Not allowed to be blank if it is procedure.</remarks>
		public string CodeSent;
	}

	/*=========================================================================================
	=================================== class Commlogs ==========================================*/

	///<summary></summary>
	public class Commlogs:DataClass{
		///<summary></summary>
		public static Commlog[] List;//for one patient
		///<summary></summary>
		public static Commlog Cur;
		//public static Hashtable HList;

		///<summary>Gets all items for the current patient ordered by date.</summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * FROM commlog"
				+" WHERE patnum = '"+Patients.Cur.PatNum+"'"
				+" ORDER BY commdate";
			FillTable();
			List=new Commlog[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i].CommlogNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum    = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].CommDate  = PIn.PDate  (table.Rows[i][2].ToString());
				List[i].CommType  = (CommItemType)PIn.PInt(table.Rows[i][3].ToString());
				List[i].Note      = PIn.PString(table.Rows[i][4].ToString());
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO commlog (patnum"
				+",commdate,commtype,note) VALUES("
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PDate  (Cur.CommDate)+"', "
				+"'"+POut.PInt   ((int)Cur.CommType)+"', "
				+"'"+POut.PString(Cur.Note)+"')";
			NonQ(false);
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE commlog SET "
				+"patnum = '"   +POut.PInt   (Cur.PatNum)+"', "
				+"commdate= '"  +POut.PDate  (Cur.CommDate)+"', "
				+"commtype = '" +POut.PInt   ((int)Cur.CommType)+"', "
				+"note = '"     +POut.PString(Cur.Note)+"' "
				+"WHERE commlognum = '"+POut.PInt(Cur.CommlogNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM commlog WHERE commlognum = '"+Cur.CommlogNum.ToString()+"'";
			NonQ(false);
		}

	}

	/// <summary>Corresponds to the commlog table in the database.</summary>
	/// <remarks>Will eventually track all communications including emails, phonecalls, letters, etc.
	/// There is no user field yet to track who made the entry because we need to add a user table first to get a unique id.</remarks>
	public struct Commlog{
		///<summary>Primary key.</summary>
		public int CommlogNum;
		///<summary>Foreign key to patient.PatNum</summary>
		public int PatNum;
		///<summary>Date of entry</summary>
		public DateTime CommDate;
		///<summary>See the CommItemType enumeration.</summary>
		public CommItemType CommType;
		///<summary>Note for this commlog entry.</summary>
		public string Note;
	}


	/*=========================================================================================
	=================================== class Computers ==========================================*/

	///<summary></summary>
	public class Computers:DataClass{
		///<summary></summary>
		public static Computer[] List;
		///<summary></summary>
		public static Computer Cur;
		///<summary></summary>
		public static Hashtable HList;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from computer "
				+"WHERE compname = '"+SystemInformation.ComputerName+"'";
			FillTable();
			if(table.Rows.Count==0){
				Cur=new Computer();
				Cur.CompName=SystemInformation.ComputerName;
				InsertCur();
			}
			cmd.CommandText =
				"SELECT * from computer";
			FillTable();
			List=new Computer[table.Rows.Count];
			HList=new Hashtable();
			for(int i=0;i<List.Length;i++){
				List[i].ComputerNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].CompName    = PIn.PString(table.Rows[i][1].ToString());
				List[i].PrinterName = PIn.PString(table.Rows[i][2].ToString());
				if(SystemInformation.ComputerName==List[i].CompName){
					Cur=List[i];
				}		
				HList.Add(List[i].ComputerNum,List[i]);
			}
			/*if(!Directory.Exists(Cur.PriDocPath)){
				//MessageBox("Need to determine proper path");

				FormInputBox IBox2=new FormInputBox();
				IBox2.label1.Text="Please enter primary path for Documents";
				IBox2.textBox1.Text=Cur.PriDocPath;
				while(!Directory.Exists(IBox2.textBox1.Text)){
					IBox2.ShowDialog();
					if(IBox2.DialogResult==DialogResult.OK){
						if(!Directory.Exists(IBox2.textBox1.Text)){
							if(MessageBox.Show("Invalid path.  Quit application?","Quit?"
								,MessageBoxButtons.YesNo)==DialogResult.Yes){
								Application.Exit();
								return;
							}
						}
					}
					else{//dialogresult!=OK
						MessageBox.Show("Closing Application");
						Application.Exit();
						return;
					}
				}
				Cur.PriDocPath=IBox2.textBox1.Text;
				UpdateCur();
			}*/
		}
   

		///<summary></summary>
		public static void InsertCur(){//ONLY use this if compname is not already present
			cmd.CommandText = "INSERT INTO computer (compname,"
				+"printername) VALUES("
				+"'"+POut.PString(Cur.CompName)+"', "
				+"'"+POut.PString(Cur.PrinterName)+"')";
			NonQ(false);
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE computer SET "
				+"compname = '"    +POut.PString(Cur.CompName)+"', "
				+"printername = '" +POut.PString(Cur.PrinterName)+"' "
				+"WHERE computernum = '"+POut.PInt(Cur.ComputerNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM computer WHERE computernum = '"+Cur.ComputerNum.ToString()+"'";
			NonQ(false);
		}

	}

	

	///<summary>Corresponds to the computer table in the database.</summary>
	///<remarks>Keeps track of the computers in an office.  There is no interface to maintain this list yet.  The list will eventually become cluttered with the names of old computers that are no longer in service.  The old rows can be safely deleted; that will actually speed up the messaging system.</remarks>
	public struct Computer{//
		///<summary>Primary key.</summary>
		public int ComputerNum;
		///<summary>Name of the computer.</summary>
		public string CompName;
		///<summary>Default printer for each computer</summary>
		public string PrinterName;
	}

	/*=========================================================================================
		=================================== class Contacts ==========================================*/

	///<summary></summary>
	public class Contacts:DataClass{
		///<summary></summary>
		public static Contact Cur;
		///<summary></summary>
		public static Contact[] List;//for one category only. Not refreshed with local data
		//public static Contact[] ListForCat;

		///<summary></summary>
		public static void Refresh(int category){
			cmd.CommandText =
				"SELECT * from contact WHERE category = '"+category+"'"
				+" ORDER BY LName";
			FillTable();
			List = new Contact[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i].ContactNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].LName      = PIn.PString(table.Rows[i][1].ToString());
				List[i].FName      = PIn.PString(table.Rows[i][2].ToString());
				List[i].WkPhone    = PIn.PString(table.Rows[i][3].ToString());
				List[i].Fax        = PIn.PString(table.Rows[i][4].ToString());
				List[i].Category   = PIn.PInt   (table.Rows[i][5].ToString());
				List[i].Notes      = PIn.PString(table.Rows[i][6].ToString());
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO contact (lname,fname,wkphone,fax,category,"
				+"notes) VALUES("
				+"'"+POut.PString(Cur.LName)+"', "
				+"'"+POut.PString(Cur.FName)+"', "
				+"'"+POut.PString(Cur.WkPhone)+"', "
				+"'"+POut.PString(Cur.Fax)+"', "
				+"'"+POut.PInt   (Cur.Category)+"', "
				+"'"+POut.PString(Cur.Notes)+"')";
			NonQ(false);
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE contact SET "
				+"lname = '"    +POut.PString(Cur.LName)+"' "
				+",fname = '"   +POut.PString(Cur.FName)+"' "
				+",wkphone = '" +POut.PString(Cur.WkPhone)+"' "
				+",fax = '"     +POut.PString(Cur.Fax)+"' "
				+",category = '"+POut.PInt   (Cur.Category)+"' "
				+",notes = '"   +POut.PString(Cur.Notes)+"' "
				+"WHERE contactnum = '"+POut.PInt  (Cur.ContactNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM contact WHERE contactnum = '"+Cur.ContactNum.ToString()+"'";
			NonQ(false);
		}

	}

	///<summary>Corresponds to the contact table in the database.</summary>
	public struct Contact{
		///<summary>Primary key.</summary>
		public int ContactNum;
		///<summary>Last name or, frequently, the entire name.</summary>
		public string LName;
		///<summary>First name is optional.</summary>
		public string FName;
		///<summary>Work phone.</summary>
		public string WkPhone;
		///<summary>Fax number.</summary>
		public string Fax;
		///<summary>Foreign key to definition.DefNum</summary>
		public int Category;
		///<summary>Note for this contact.</summary>
		public string Notes;
	}

	/*=========================================================================================
		=================================== class CovPats ==========================================*/

	///<summary></summary>
	public class CovPats:DataClass{
		//the first two are the usual lists of interest
		///<summary></summary>
		public static int[] PriList;//filled during refresh
		///<summary></summary>
		public static int[] SecList;//filled during refresh
		///<summary></summary>
		public static CovPat[] List;
		///<summary></summary>
		public static CovPat Cur;
		///<summary></summary>
		public static CovPat[] ListForPlan;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from covpat"
				+" WHERE PlanNum = '"+Patients.Cur.PriPlanNum+"'"
				+" || PlanNum = '"+Patients.Cur.SecPlanNum+"'"
				+" || PriPatNum = '"+Patients.Cur.PatNum+"'"
				+" || SecPatNum = '"+Patients.Cur.PatNum+"'";
			FillTable();
			List = new CovPat[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i].CovPatNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].CovCatNum = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].PlanNum   = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].PriPatNum = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].SecPatNum = PIn.PInt   (table.Rows[i][4].ToString());
				List[i].Percent   = PIn.PInt   (table.Rows[i][5].ToString());
			}
			PriList=new int[CovCats.ListShort.Length];
			SecList=new int[CovCats.ListShort.Length];
			for(int i=0;i<CovCats.ListShort.Length;i++){
				PriList[i]=-1;//sets each item to -1(unknown) unless a covpat match is made
				SecList[i]=-1;
			}
			for(int i=0;i<List.Length;i++){//plans first
				if(CovCats.GetOrderShort(List[i].CovCatNum)!=-1
					&& List[i].PlanNum != 0){
					if(List[i].PlanNum==Patients.Cur.PriPlanNum){
						PriList[CovCats.GetOrderShort(List[i].CovCatNum)]=List[i].Percent;
					}
					if(List[i].PlanNum==Patients.Cur.SecPlanNum){
						SecList[CovCats.GetOrderShort(List[i].CovCatNum)]=List[i].Percent;
					}
				}
			}
			for(int i=0;i<List.Length;i++){//then Pri & Sec (ok to overwrite plans)
				if(CovCats.GetOrderShort(List[i].CovCatNum)!=-1){
					if(List[i].PriPatNum==Patients.Cur.PatNum){
						PriList[CovCats.GetOrderShort(List[i].CovCatNum)]=List[i].Percent;
					}
					if(List[i].SecPatNum==Patients.Cur.PatNum){
						SecList[CovCats.GetOrderShort(List[i].CovCatNum)]=List[i].Percent;
					}
				}
			}
			//flat copay ins plans are always 100% coverage regardless of any percentages present.
			InsPlans.GetCur(Patients.Cur.PriPlanNum);
			if(InsPlans.Cur.PlanType=="f"){//flat copay
				for(int i=0;i<PriList.Length;i++){
					PriList[i]=100;//sets all 6 or so categories for this patient to 100%
				}
			}
			InsPlans.GetCur(Patients.Cur.SecPlanNum);
			if(InsPlans.Cur.PlanType=="f"){//flat copay
				for(int i=0;i<SecList.Length;i++){
					SecList[i]=100;//sets all 6 or so categories for this patient to 100%
				}
			}
		}//end method refresh 
		
		///<summary></summary>
		public static void RefreshForPlan(){
			cmd.CommandText =
				"SELECT * from covpat"
				+" WHERE PlanNum = '"+InsPlans.Cur.PlanNum+"'";
			FillTable();
			ListForPlan = new CovPat[table.Rows.Count];
			for(int i=0;i<ListForPlan.Length;i++){
				ListForPlan[i].CovPatNum = PIn.PInt   (table.Rows[i][0].ToString());
				ListForPlan[i].CovCatNum = PIn.PInt   (table.Rows[i][1].ToString());
				ListForPlan[i].PlanNum   = PIn.PInt   (table.Rows[i][2].ToString());
				ListForPlan[i].PriPatNum = PIn.PInt   (table.Rows[i][3].ToString());
				ListForPlan[i].SecPatNum = PIn.PInt   (table.Rows[i][4].ToString());
				ListForPlan[i].Percent   = PIn.PInt   (table.Rows[i][5].ToString());
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO covpat (covcatnum,plannum,pripatnum,"
				+"secpatnum,percent) VALUES("
				+"'"+POut.PInt   (Cur.CovCatNum)+"', "
				+"'"+POut.PInt   (Cur.PlanNum)+"', "
				+"'"+POut.PInt   (Cur.PriPatNum)+"', "
				+"'"+POut.PInt   (Cur.SecPatNum)+"', "
				+"'"+POut.PInt   (Cur.Percent)+"')";
			NonQ(false);
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE covpat SET "
				+"covcatnum = '" +POut.PInt   (Cur.CovCatNum)+"' "
				+",plannum = '"   +POut.PInt   (Cur.PlanNum)+"' "
				+",pripatnum = '" +POut.PInt   (Cur.PriPatNum)+"' "
				+",secpatnum = '" +POut.PInt   (Cur.SecPatNum)+"' "
				+",percent = '"   +POut.PInt   (Cur.Percent)+"' "
				+"WHERE covpatnum = '"+POut.PInt  (Cur.CovPatNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM covpat WHERE covpatnum = '"+Cur.CovPatNum.ToString()+"'";
			NonQ(false);
		}

		//public static void DeleteAllInCurPlan(){//obsolete
		//	cmd.CommandText = "DELETE FROM covpat WHERE plannum = '"+InsPlans.Cur.PlanNum.ToString()+"'";
		//	NonQ(false);
		//}

		///<summary></summary>
		public static double GetPercent(string myADACode, PriSecTot pst){//does not return a tot?
			double retVal=0;
			int covCatNum=0;
			for(int i=0;i<CovSpans.List.Length;i++){
				if(String.Compare(myADACode,CovSpans.List[i].FromCode)>=0
					&& String.Compare(myADACode,CovSpans.List[i].ToCode)<=0){
					covCatNum=CovSpans.List[i].CovCatNum;
				}
			}
			int priPercent=0;
			int secPercent=0;
			for(int i=0;i<CovCats.ListShort.Length;i++){
				if(covCatNum==CovCats.ListShort[i].CovCatNum){
					if(PriList[i]==-1)
						priPercent=0;
					else
						priPercent=PriList[i];
					if(SecList[i]==-1)
						secPercent=0;
					else
						secPercent=SecList[i];
				}
			}
			if(pst==PriSecTot.Pri){
				retVal=priPercent;
			}
			else{
				retVal=secPercent;
			}
			return retVal/100;	
		}
		
		/*public double GetCatPercent(int myCovCatNum){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(myCovCatNum==List[i].CovCatNum){
					if(List[i].Percent!=0) retVal=(double)List[i].Percent/100;
				}
			}
			return retVal;	
		}*/

	}

	///<summary>Corresponds to the covpat table in the database.</summary>
	///<remarks>Coverage percentage for a patient.  Each entry in this table is a single percentage value.  A covpat can have a value in ONLY ONE of these three fields: PlanNum, PriPatNum, or SecPatNum.  If it is for a PlanNum, then the percentage is attached to an insurance plan.  If it is one of the others, then it is attached to the coverage for a patient, either primary or secondary, and overrides the plan percentage.</remarks>
	public struct CovPat{  
		///<summary>Primary key.</summary>
		public int CovPatNum;
		///<summary>Foreign key to covcat.CovCatNum.</summary>
		public int CovCatNum;
		///<summary>OPT 1: Foreign key to insplan.PlanNum.</summary>
		public int PlanNum;
		///<summary>OPT 2: Foreign key to patient.PatNum for primary coverage.</summary>
		public int PriPatNum;
		///<summary>OPT 3: Foreign key to patient.PatNum for secondary coverage.</summary>
		public int SecPatNum;
		///<summary>Valid values are 0 to 100. If unknown, the covpat is simply deleted.</summary>
		public int Percent;
	}

	/*=========================================================================================
	=================================== class CovCats ==========================================*/

	///<summary></summary>
	public class CovCats:DataClass{
		///<summary></summary>
		public static CovCat[] List;
		///<summary></summary>
		public static CovCat[] ListShort;
		///<summary></summary>
		public static CovCat Cur;
		///<summary></summary>
		public static int Selected;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from covcat"
				//+" WHERE "+s
				+" ORDER BY covorder";
			FillTable();
			//MessageBox.Show(cmd.CommandText);
			List=new CovCat[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].CovCatNum     = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description   = PIn.PString(table.Rows[i][1].ToString());
				List[i].DefaultPercent= PIn.PInt   (table.Rows[i][2].ToString());
				List[i].IsPreventive  = PIn.PBool  (table.Rows[i][3].ToString());
				List[i].CovOrder      = PIn.PInt   (table.Rows[i][4].ToString());
				List[i].IsHidden      = PIn.PBool  (table.Rows[i][5].ToString());
			}//end for
			cmd.CommandText =
				"SELECT * from covcat"
				+" WHERE ishidden = 0"
				+" ORDER BY covorder";
			FillTable();
			//MessageBox.Show(cmd.CommandText);
			ListShort=new CovCat[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				ListShort[i].CovCatNum     = PIn.PInt   (table.Rows[i][0].ToString());
				ListShort[i].Description   = PIn.PString(table.Rows[i][1].ToString());
				ListShort[i].DefaultPercent= PIn.PInt   (table.Rows[i][2].ToString());
				ListShort[i].IsPreventive  = PIn.PBool  (table.Rows[i][3].ToString());
				ListShort[i].CovOrder      = PIn.PInt   (table.Rows[i][4].ToString());
				//ishidden
			}//end for
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE covcat SET "
				+ "description = '"    +POut.PString(Cur.Description)+"'"
				+",defaultpercent = '" +POut.PInt   (Cur.DefaultPercent)+"'"
				+",ispreventive = '"   +POut.PBool  (Cur.IsPreventive)+"'"
				+",covorder = '"       +POut.PInt   (Cur.CovOrder)+"'"
				+",ishidden = '"       +POut.PBool  (Cur.IsHidden)+"'"
				+" WHERE covcatnum = '"+POut.PInt(Cur.CovCatNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO covcat (description,defaultpercent,ispreventive,"
				+"covorder,ishidden) VALUES("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PInt   (Cur.DefaultPercent)+"', "
				+"'"+POut.PBool  (Cur.IsPreventive)+"', "
				+"'"+POut.PInt   (Cur.CovOrder)+"', "
				+"'"+POut.PBool  (Cur.IsHidden)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static int GetCatNum(string myADACode){
			return 0;
		}
		
		///<summary></summary>
		public static double GetDefaultPercent(int myCovCatNum){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(myCovCatNum==List[i].CovCatNum){
					retVal=(double)List[i].DefaultPercent;
				}
			}
			return retVal;	
		}

		///<summary></summary>
		public static string GetDesc(int covCatNum){
			string retStr="";
			for(int i=0;i<List.Length;i++){
				if(covCatNum==List[i].CovCatNum){
					retStr=List[i].Description;
				}
			}
			return retStr;	
		}

		///<summary></summary>
		public static int GetCovCatNum(int orderShort){
			//need to check this again:
			int retVal=0;
			for(int i=0;i<ListShort.Length;i++){
				if(orderShort==ListShort[i].CovOrder){
					retVal=ListShort[i].CovCatNum;
				}
			}
			return retVal;	
		}

		///<summary></summary>
		public static int GetOrderShort(int CovCatNum){
			int retVal=-1;
			for(int i=0;i<ListShort.Length;i++){
				if(CovCatNum==ListShort[i].CovCatNum){
					retVal=i;
				}
			}
			return retVal;	
		}	

		///<summary></summary>
		public static void MoveUp(){
			if(Selected==-1){
				MessageBox.Show(Lan.g("CovCat","Please select an item first."));
				return;
			}
			if(Selected==0){
				return;
			}
			SetOrder(Selected-1,List[Selected].CovOrder);
			SetOrder(Selected,List[Selected].CovOrder-1);
			Selected-=1;
		}//end MoveUp

		///<summary></summary>
		public static void MoveDown(){
			if(Selected==-1){
				MessageBox.Show(Lan.g("CovCat","Please select an item first."));
				return;
			}
			if(Selected==List.Length-1){
				return;
			}
			SetOrder(Selected+1,List[Selected].CovOrder);
			SetOrder(Selected,List[Selected].CovOrder+1);
			Selected+=1;
		}

		///<summary></summary>
		public static void SetOrder(int mySelNum, int myItemOrder){
			CovCat temp=List[mySelNum];
			temp.CovOrder=myItemOrder;
			Cur=temp;
			UpdateCur();
		}

		///<summary></summary>
		public static bool GetIsPrev(string myADACode){
			int covCatNum=0;
			for(int i=0;i<CovSpans.List.Length;i++){
				if(String.Compare(myADACode,CovSpans.List[i].FromCode)>=0
					&& String.Compare(myADACode,CovSpans.List[i].ToCode)<=0){
					covCatNum=CovSpans.List[i].CovCatNum;
				}
			}
			for(int i=0;i<ListShort.Length;i++){
				if(covCatNum==ListShort[i].CovCatNum){
					return ListShort[i].IsPreventive;
				}
			}
			return false;//should never happen	
		}

	}

	///<summary>Corresponds to the covcat table in the database.</summary>
	public struct CovCat{
		///<summary>Primary key.</summary>
		public int CovCatNum;
		///<summary>Description of this category.</summary>
		public string Description;
		///<summary>Default percent for this category.</summary>
		public int DefaultPercent;
		///<summary>True if this is a preventive category.</summary>
		public bool IsPreventive;
		///<summary>The order in which the categories are displayed.</summary>
		public int CovOrder;
		///<summary>If true, this category will be hidden.</summary>
		public bool IsHidden;
	}


	/*=========================================================================================
		=================================== class Conversions ==========================================*/

	///<summary></summary>
	public class Conversions:DataClass{
		///<summary></summary>
		public static string[] NonQArray;
		///<summary></summary>
		public static string NonQString;
		///<summary></summary>
		public static string SelectText;
		///<summary></summary>
		public static DataTable TableQ;

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

		///<summary></summary>
		public static bool SubmitSelect(){//return true if successful
			try{
				cmd.CommandText=SelectText;
				FillTable();
				TableQ=table.Copy();
			}
			catch(MySqlException e){
				MessageBox.Show("e:"+e.Message);
				return false;
			}
			catch{
				MessageBox.Show("Command:"+cmd.CommandText);
				return false;
			}
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

	/*=========================================================================================
		=================================== class CovSpans ==========================================*/

	///<summary></summary>
	public class CovSpans:DataClass{
		///<summary></summary>
		public static CovSpan[] List;
		///<summary></summary>
		public static CovSpan Cur;
		
		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from covspan"
				+" ORDER BY FromCode";
			FillTable();
			List=new CovSpan[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].CovSpanNum  = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].CovCatNum   = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].FromCode    = PIn.PString(table.Rows[i][2].ToString());
				List[i].ToCode      = PIn.PString(table.Rows[i][3].ToString());
			}//end for
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE covspan SET "
				+ "covcatnum = '" +POut.PInt   (Cur.CovCatNum)+"'"
				+",fromcode = '"  +POut.PString(Cur.FromCode)+"'"
				+",tocode = '"    +POut.PString(Cur.ToCode)+"'"
				+" WHERE covspannum = '"+POut.PInt(Cur.CovSpanNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO covspan (covcatnum,"
				+"fromcode,tocode) VALUES("
				+"'"+POut.PInt   (Cur.CovCatNum)+"', "
				+"'"+POut.PString(Cur.FromCode)+"', "
				+"'"+POut.PString(Cur.ToCode)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText="DELETE FROM covspan"
				+" WHERE covspannum = '"+POut.PInt(Cur.CovSpanNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static int GetCat(string myADACode){
			int retVal=0;
			for(int i=0;i<List.Length;i++){
				if(String.Compare(myADACode,List[i].FromCode)>=0
					&& String.Compare(myADACode,List[i].ToCode)<=0){
					retVal=List[i].CovCatNum;
				}
			}
			return retVal;
		}

	}

	///<summary>Corresponds to the covspan table in the database.</summary>
	public struct CovSpan{
		///<summary>Primary key.</summary>
		public int CovSpanNum;
		///<summary>Foreign key</summary>
		public int CovCatNum;
		///<summary>Foreign key to procedurecode.ADACode.</summary>
		public string FromCode;
		///<summary>Foreign key to procedurecode.ADACode.</summary>
		public string ToCode;
	}

	/*=========================================================================================
	=================================== class Defs ==========================================*/

	///<summary>Handles database commands related to the definition table in the db.</summary>\
	///<remarks>This class is referenced frequently from many different areas of the program.  It stores data from the definition table in a couple of two dimensional arrays for immediate retrieval.  </remarks>
	public class Defs:DataClass{
		///<summary>For only one category. Only used in FormDefinitions.</summary>
		public static Def[] List;
		///<summary>Current definition.</summary>
		public static Def Cur;
		///<summary>Will probably be phased out.</summary>
		public static bool IsSelected;
		///<summary>Will probably be phased out.</summary>
		public static int Selected;
		///<summary>Stores all defs in a 2D array except the hidden ones.</summary>
		///<remarks>The first dimension is the category, in int format.  The second dimension is the index of the definition in this category.  This is dependent on how it was refreshed, and not on what is in the database.  If you need to reference a specific def, then the DefNum is more effective.</remarks>
		public static Def[][] Short;
		///<summary>Stores all defs in a 2D array.</summary>
		public static Def[][] Long;

		///<summary></summary>
		public static void Refresh(){
			Long=new Def[Enum.GetValues(typeof(DefCat)).Length][];
			for(int j=0;j<Enum.GetValues(typeof(DefCat)).Length;j++){
				cmd.CommandText =
					"SELECT * from definition"
					+" WHERE category = '"+j+"'"
					+" ORDER BY ItemOrder";
				FillTable();
				Long[j]=new Def[table.Rows.Count];
				for(int i=0;i<table.Rows.Count;i++){
					Long[j][i].DefNum    = PIn.PInt   (table.Rows[i][0].ToString());
					Long[j][i].Category  = (DefCat)PIn.PInt   (table.Rows[i][1].ToString());
					Long[j][i].ItemOrder = PIn.PInt   (table.Rows[i][2].ToString());
					Long[j][i].ItemName  = PIn.PString(table.Rows[i][3].ToString());
					Long[j][i].ItemValue = PIn.PString(table.Rows[i][4].ToString());
					Long[j][i].ItemColor = Color.FromArgb(PIn.PInt(table.Rows[i][5].ToString()));
					Long[j][i].IsHidden  = PIn.PBool  (table.Rows[i][6].ToString());
				}
			}//end for j
			//MessageBox.Show(Long[(int)DefCat.ApptConfirmed].Length.ToString());
			Short=new Def[Enum.GetValues(typeof(DefCat)).Length][];
			for(int j=0;j<Enum.GetValues(typeof(DefCat)).Length;j++){
				cmd.CommandText =
					"SELECT * from definition"
					+" WHERE category = '"+j+"'"
					+" AND IsHidden = 0"
					+" ORDER BY ItemOrder";
				FillTable();
				Short[j]=new Def[table.Rows.Count];
				for(int i=0;i<table.Rows.Count;i++){
					Short[j][i].DefNum    = PIn.PInt   (table.Rows[i][0].ToString());
					Short[j][i].Category  = (DefCat)PIn.PInt   (table.Rows[i][1].ToString());
					Short[j][i].ItemOrder = PIn.PInt   (table.Rows[i][2].ToString());
					Short[j][i].ItemName  = PIn.PString(table.Rows[i][3].ToString());
					Short[j][i].ItemValue = PIn.PString(table.Rows[i][4].ToString());
					Short[j][i].ItemColor = Color.FromArgb(PIn.PInt(table.Rows[i][5].ToString()));
					Short[j][i].IsHidden  = PIn.PBool  (table.Rows[i][6].ToString());
				}
			}//end for j
			//MessageBox.Show(Short[(int)DefCat.ApptConfirmed].Length.ToString());
		}

		///<summary></summary>
		public static string GetName(DefCat myCat, int myDefNum){
			string retStr="";
			for(int i=0;i<Long[(int)myCat].GetLength(0);i++){
				if(Long[(int)myCat][i].DefNum==myDefNum){
					retStr=Long[(int)myCat][i].ItemName;
				}
			}
			return retStr;
		}

		///<summary></summary>
		public static int GetOrder(DefCat myCat, int myDefNum){
			//gets the index in the list of unhidden (the Short list).
			for(int i=0;i<Short[(int)myCat].GetLength(0);i++){
				if(Short[(int)myCat][i].DefNum==myDefNum){
					return i;
				}
			}
			return -1;
		}

		///<summary></summary>
		public static string GetValue(DefCat myCat, int myDefNum){
			string retStr="";
			for(int i=0;i<Long[(int)myCat].GetLength(0);i++){
				if(Long[(int)myCat][i].DefNum==myDefNum){
					retStr=Long[(int)myCat][i].ItemValue;
				}
			}
			return retStr;
		}

		///<summary></summary>
		public static Color GetColor(DefCat myCat, int myDefNum){
			Color retCol=Color.White;
			for(int i=0;i<Long[(int)myCat].GetLength(0);i++){
				if(Long[(int)myCat][i].DefNum==myDefNum){
					retCol=Long[(int)myCat][i].ItemColor;
				}
			}
			return retCol;
		}

		///<summary></summary>
		public static void GetCatList(int myCat){
			cmd.CommandText =
				"SELECT * from definition"
				+" WHERE category = '"+myCat+"'"
				+" ORDER BY ItemOrder";
			FillTable();
			List=new Def[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].DefNum    = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Category  = (DefCat)PIn.PInt   (table.Rows[i][1].ToString());
				List[i].ItemOrder = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].ItemName  = PIn.PString(table.Rows[i][3].ToString());
				List[i].ItemValue = PIn.PString(table.Rows[i][4].ToString());
				List[i].ItemColor = Color.FromArgb(PIn.PInt(table.Rows[i][5].ToString()));
				List[i].IsHidden  = PIn.PBool  (table.Rows[i][6].ToString());
			}//end for
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE definition SET "
				+ "category = '"  +POut.PInt   ((int)Cur.Category)+"'"
				+",itemorder = '" +POut.PInt   (Cur.ItemOrder)+"'"
				+",itemname = '"  +POut.PString(Cur.ItemName)+"'"
				+",itemvalue = '" +POut.PString(Cur.ItemValue)+"'"
				+",itemcolor = '" +POut.PInt   (Cur.ItemColor.ToArgb())+"'"
				+",ishidden = '"  +POut.PBool  (Cur.IsHidden)+"'"
				+" WHERE defnum = '"+POut.PInt(Cur.DefNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO definition (category,itemorder,"
				+"itemname,itemvalue,itemcolor,ishidden) VALUES("
				+"'"+POut.PInt   ((int)Cur.Category)+"', "
				+"'"+POut.PInt   (Cur.ItemOrder)+"', "
				+"'"+POut.PString(Cur.ItemName)+"', "
				+"'"+POut.PString(Cur.ItemValue)+"', "
				+"'"+POut.PInt   (Cur.ItemColor.ToArgb())+"', "
				+"'"+POut.PBool  (Cur.IsHidden)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Defs.Cur.DefNum=InsertID;//used in conversion
		}

		///<summary></summary>
		public static void HideDef(){//hides Selected
			Cur=List[Selected];
			Cur.IsHidden=true;
			UpdateCur();
		}

		///<summary></summary>
		public static void MoveUp(){
			if(IsSelected==false){
				MessageBox.Show(Lan.g("Defs","Please select an item first."));
				return;
			}
			if(Selected==0){
				return;
			}
			SetOrder(Selected-1,List[Selected].ItemOrder);
			SetOrder(Selected,List[Selected].ItemOrder-1);
			Selected-=1;
		}//end MoveUp

		///<summary></summary>
		public static void MoveDown(){
			if(IsSelected==false){
				MessageBox.Show(Lan.g("Defs","Please select an item first."));
				return;
			}
			if(Selected==List.Length-1){
				return;
			}
			SetOrder(Selected+1,List[Selected].ItemOrder);
			SetOrder(Selected,List[Selected].ItemOrder+1);
			Selected+=1;
		}

		///<summary></summary>
		public static void SetOrder(int mySelNum, int myItemOrder){
			//Preference temp=new Preference();
			//for(int i=0;i<List.Length;i++){
			//	if(List[i].PrefNum==myPrefNum)
			//		temp=List[i];
			//}
			Def temp=List[mySelNum];
			temp.ItemOrder=myItemOrder;
			Cur=temp;
			UpdateCur();
		}

	}

	///<summary>Corresponds to the definition table in the database.</summary>
	///<remarks>Almost every table in the database links to definition.  Almost all links to this table will be to a DefNum.  Using the DefNum, you can find any of the other fields of interest, usually the ItemName.  Make sure to look at the Defs class to see how the definitions are loaded into memory ahead of time.</remarks>
	public struct Def{
		///<summary>Primary key.</summary>
		public int DefNum;
		///<summary>See the DefCat enumeration.</summary>
		public DefCat Category;
		///<summary>Order that each item shows on various lists.</summary>
		public int ItemOrder;
		///<summary>Each category is a little different.  This field is usually the common name of the item.</summary>
		public string ItemName;
		///<summary>This field can be used to store extra info about the item.</summary>
		public string ItemValue;
		///<summary>Some categories include a color option.</summary>
		public Color ItemColor;//
		///<summary>If hidden, the item will not show on any list, but can still be referenced.</summary>
		public bool IsHidden;
	}

	/*==================================================================================================
	 =================================== Class DocAttaches ===========================================*/
	///<summary></summary>
	public class DocAttaches:DataClass{
		//public Break Cur;
		///<summary></summary>
		public static DocAttach Cur;
		///<summary></summary>
		public static DocAttach[] List;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText="SELECT * from docattach WHERE patnum = '"
				+Patients.Cur.PatNum+"'";			//	MessageBox.Show(cmd.CommandText);			FillTable();//find all attachments for that patient
			List=new DocAttach[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				List[i].DocAttachNum =PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum       =PIn.PInt   (table.Rows[i][1].ToString());
				List[i].DocNum       =PIn.PInt   (table.Rows[i][2].ToString());
			}
			//	MessageBox.Show(List.Length.ToString());
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO docattach (PatNum, DocNum) VALUES ("
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PInt   (Cur.DocNum)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
			//Cur.DocAttachNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE docattach SET " 
				+ ",PatNum = '"  +POut.PInt(Cur.PatNum)+"'"
				+ ",DocNum = '"  +POut.PInt(Cur.DocNum)+"'"
				+" WHERE DocAttachNum = '" +POut.PInt(Cur.DocAttachNum)+"'";
			NonQ(false);
		}
	
		
				
		/*public static void DeleteDocNum(string myDocNum){
			cmd.CommandText = "DELETE from docattach WHERE docnum = '"+myDocNum+"'";
			NonQ(false);
		}
*/
	}

	///<summary>Corresponds to the docattach table in the database.</summary>
	///<remarks>Links documents (images) to patients.  This will allow one document to be shared between multiple patients in a future version.</remarks>
	public struct DocAttach{
		///<summary>Primary key.</summary>
		public int DocAttachNum;
		///<summary>Foreign key to patient.PatNum.</summary>
		public int PatNum;
		///<summary>Foreign key document.DocNum.</summary>
		public int DocNum;
		
	}//end struct DocAttach

	/*=========================================================================================
	=================================== class Documents ==========================================*/

	///<summary></summary>
	public class Documents:DataClass{
		///<summary></summary>
		public static Document[] List;
		//public static DocBackup[] ListBackup;
		///<summary></summary>
		public static Document Cur;	

		///<summary></summary>
		public static void Refresh(){
			if(DocAttaches.List.Length==0){
				List=new Document[0];
				return;
			}
			cmd.CommandText="SELECT * FROM document WHERE docnum = '"+DocAttaches.List[0].DocNum.ToString()+"'";
			for(int i=1;i<DocAttaches.List.Length;i++){
				cmd.CommandText+=" || docnum = '"+DocAttaches.List[i].DocNum.ToString()+"'";
			}
			cmd.CommandText+=" order by datecreated";
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			List=new Document[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				List[i].DocNum     =PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description=PIn.PString(table.Rows[i][1].ToString());
				List[i].DateCreated=PIn.PDate  (table.Rows[i][2].ToString());
				List[i].DocCategory=PIn.PInt   (table.Rows[i][3].ToString());
				List[i].WithPat    =PIn.PInt   (table.Rows[i][4].ToString());
				List[i].FileName   =PIn.PString(table.Rows[i][5].ToString());
				//List[i].LastAltered=PIn.PDate  (table.Rows[i][6].ToString());
				//List[i].IsDeleted  =PIn.PBool  (table.Rows[i][7].ToString());
			}
		}

		/*public static void GetBackupList(){
			cmd.CommandText="SELECT filename,lastaltered,isdeleted,imagefolder FROM document,patient " 
				+"WHERE document.withpat=patient.patnum && lastaltered > '"+POut.PDateT(BackupJobs.Cur.LastRun)+"'"; 
			FillTable();
			ListBackup=new DocBackup[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				ListBackup[i].FileName   =PIn.PString(table.Rows[i][0].ToString());
				ListBackup[i].LastAltered=PIn.PDate  (table.Rows[i][1].ToString());
				ListBackup[i].IsDeleted  =PIn.PBool  (table.Rows[i][2].ToString());
				ListBackup[i].PatFolder  =PIn.PString(table.Rows[i][3].ToString());
			}
		}*/

		///<summary></summary>
		public static void GetCurrent(string docNum){
			for (int i = 0; i<List.Length; i+=1){
				if (List[i].DocNum.ToString()==docNum){
					Cur = List[i];
				}
			}
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from document WHERE docnum = '"+Cur.DocNum.ToString()+"'";
			NonQ(false);
			cmd.CommandText = "DELETE from docattach WHERE docnum = '"+Cur.DocNum.ToString()+"'";
			NonQ(false);
			//Cur.IsDeleted=true;
			//UpdateCur();
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = 
				"INSERT INTO document (Description,DateCreated,DocCategory,"
				+"WithPat,Filename) VALUES ("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PDate  (Cur.DateCreated)+"', "
				+"'"+POut.PInt   (Cur.DocCategory)+"', "
				+"'"+POut.PInt   (Cur.WithPat)+"', "
				+"'"+POut.PString(Cur.FileName)+"')";
				//+"'"+POut.PDate  (Cur.LastAltered)+"', "
				//+"'"+POut.PBool  (Cur.IsDeleted)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.DocNum=InsertID;
			Cur.FileName="";
			string s=Patients.Cur.LName+Patients.Cur.FName;
			for(int i=0;i<s.Length;i++){
				if(Char.IsLetter(s,i)){
					Cur.FileName+=s.Substring(i,1);
				}
			}
			Cur.FileName+=Cur.DocNum.ToString()+".jpg";//ensures unique name
			UpdateCur();
			DocAttaches.Cur=new DocAttach();
			DocAttaches.Cur.DocNum=Cur.DocNum;
			DocAttaches.Cur.PatNum=Patients.Cur.PatNum;
			DocAttaches.InsertCur();
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE document SET " 
				+ "Description = '"     +POut.PString(Cur.Description)+"'"
				+ ",DateCreated = '"    +POut.PDate  (Cur.DateCreated)+"'"
				+ ",DocCategory = '"    +POut.PInt   (Cur.DocCategory)+"'"
				+ ",WithPat = '"        +POut.PInt   (Cur.WithPat)+"'"
				+ ",FileName    = '"    +POut.PString(Cur.FileName)+"'"
				//+ ",LastAltered= '"     +POut.PDate  (Cur.LastAltered)+"'"
				//+ ",IsDeleted = '"      +POut.PBool  (Cur.IsDeleted)+"'"
				+" WHERE DocNum = '"    +POut.PInt(Cur.DocNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);			
		}

	}//end class Docs

	///<summary>Corresponds to the document table in the database.</summary>
	public struct Document{
		///<summary>Primary key.</summary>
		public int DocNum;
		///<summary>Description of the document.</summary>
		public string Description;
		///<summary>Date created.</summary>
		public DateTime DateCreated;
		///<summary>Foreign key to definition.DefNum. Cateories for documents.</summary>
		public int DocCategory;
		///<summary>Foreign key to patient.PatNum.  Patient folder that document is in.(for sharing situations later)</summary>
		public int WithPat;
		///<summary>The name of the file.</summary>
		public string FileName;
		//public DateTime LastAltered;
		//public bool IsDeleted;
	}
  
	/*public struct DocBackup{
		public string FileName;
		public DateTime LastAltered;
		public bool IsDeleted;
		public string PatFolder;
	}*/


}









