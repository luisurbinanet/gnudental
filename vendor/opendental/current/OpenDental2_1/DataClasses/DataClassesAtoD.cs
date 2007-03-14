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
	public class DataClass{//this is the parent of all data classes
		protected static MySqlDataAdapter da;
		protected static MySqlConnection con;
		protected static DataSet ds;
		protected static MySqlDataReader dr;
		public static MySqlCommand cmd;
		protected static DataTable table;
		protected static int InsertID;

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

		protected static void FillDataSet(){//the driver is finally good enough to try this
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
	public class Batch:DataClass{//used to send batch SQL Select statements
		//this is a first attempt at batch commands.
		//The huge advantage is that it only involves ONE round trip.

		public static void Select(string tableList){
			AssembleCommand(tableList);
			FillDataSet();
			AssignTableNames(tableList);
			RefreshClasses(tableList);
		}

		private static void AssembleCommand(string tableList){
			//I could avoid a switch statement if I knew how to use reflection to pass table names
			//instead of using strings.
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
	public class Adjustments:DataClass{
		public static Adjustment[] List;
		public static Adjustment Cur;
		public static Adjustment[] PaymentList;

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

		public static void UpdateCur(){//updates Cur
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

		public static void DeleteCur(){
			cmd.CommandText="DELETE FROM adjustment "
				+"WHERE adjnum = '"+Cur.AdjNum.ToString()+"'";
			NonQ(false);
		}

		public static double ComputeBal(){//must make sure Refresh is done first
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				retVal+=List[i].AdjAmt;//amount might be pos or neg
			}
			return retVal;
		}
	}

	public struct Adjustment{
		public int AdjNum;//primary key
		public DateTime AdjDate;
		public double AdjAmt;
		public int PatNum;//primary key to Patient.PatNum
		public int AdjType;//foreign key to Definition.DefNum
		public int ProvNum;//foreign key to Provider.ProvNum
		public string AdjNote;
	}


	/*=========================================================================================
	=================================== class Appointments ==========================================*/
	public class Appointments:DataClass{
		private static Appointment[] List;
		public static Appointment[] ListDay;
		public static Appointment[] ListUn;
		public static Appointment[] ListOth;
		public static Appointment Cur;
		public static Appointment PinBoard;
		public static DateTime DateSelected;

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

		public static void RefreshUnsched(){
			cmd.CommandText =
				"SELECT * FROM appointment "
				+"WHERE aptstatus = '"+(int)ApptStatus.UnschedList+"' "
				+"ORDER BY AptDateTime";
			FillList();
			ListUn=List;
			List=null;
		}

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
			for (int i = 0; i < table.Rows.Count; i += 1){
				List[i].AptNum     =PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum     =PIn.PInt   (table.Rows[i][1].ToString());
				List[i].AptStatus  =(ApptStatus)PIn.PInt(table.Rows[i][2].ToString());
				List[i].Pattern    =PIn.PString(table.Rows[i][3].ToString());
				List[i].Confirmed  =PIn.PInt   (table.Rows[i][4].ToString());
				List[i].AddTime    =PIn.PInt   (table.Rows[i][5].ToString());
				List[i].Op         =PIn.PInt   (table.Rows[i][6].ToString());
				List[i].Note       =PIn.PString(table.Rows[i][7].ToString());
				List[i].ProvNum    =PIn.PInt   (table.Rows[i][8].ToString());
				List[i].ProvHyg    =PIn.PInt   (table.Rows[i][9].ToString());
				List[i].AptDateTime=PIn.PDateT (table.Rows[i][10].ToString());
				List[i].NextAptNum =PIn.PInt   (table.Rows[i][11].ToString());
				List[i].UnschedStatus=PIn.PInt (table.Rows[i][12].ToString());
				List[i].Lab        =(LabCase)PIn.PInt   (table.Rows[i][13].ToString());
			}
		}

		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO appointment (patnum,aptstatus, "
				+"pattern,confirmed,addtime,op,note,provnum,"
				+"provhyg,aptdatetime,nextaptnum,unschedstatus,lab) VALUES("
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
				+"'"+POut.PInt   ((int)Cur.Lab)+"')";
			NonQ(true);
			Cur.AptNum=InsertID;
			//MessageBox.Show(Cur.AptNum.ToString());
		}

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE appointment SET "
				+"PatNum = '"     +POut.PInt   (Cur.PatNum)+"', "
				+"AptStatus = '"     +POut.PInt   ((int)Cur.AptStatus)+"', "
				+"Pattern = '"    +POut.PString(Cur.Pattern)+"', "
				+"Confirmed = '"  +POut.PInt   (Cur.Confirmed)+"', "
				+"AddTime = '"    +POut.PInt   (Cur.AddTime)+"', "
				+"Op = '"         +POut.PInt   (Cur.Op)+"', "
				+"Note = '"       +POut.PString(Cur.Note)+"', "
				+"provnum = '"    +POut.PInt   (Cur.ProvNum)+"', "
				+"provhyg = '"    +POut.PInt   (Cur.ProvHyg)+"', "
				+"aptdatetime = '"+POut.PDateT (Cur.AptDateTime)+"', "
				+"nextaptnum = '" +POut.PInt   (Cur.NextAptNum)+"', "
				+"unschedstatus = '" +POut.PInt(Cur.UnschedStatus)+"', "
				+"lab = '"        +POut.PInt   ((int)Cur.Lab)+"' "
				+"WHERE AptNum = '"+POut.PInt  (Cur.AptNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		public static void RefreshCur(int aptNum){
			cmd.CommandText =
				"SELECT aptnum,patnum,aptstatus,"
				+"pattern,confirmed,addtime,op,note,provnum,"
				+"provhyg,aptdatetime,nextaptnum,unschedstatus,lab "
				+"FROM appointment "
				+"WHERE aptnum = '"+POut.PInt(aptNum)+"'";
			FillTable();
			Cur=new Appointment();
			if(table.Rows.Count==0){
				return;
			}
			Cur.AptNum     =PIn.PInt   (table.Rows[0][0].ToString());
			Cur.PatNum     =PIn.PInt   (table.Rows[0][1].ToString());
			Cur.AptStatus  =(ApptStatus)PIn.PInt(table.Rows[0][2].ToString());
			Cur.Pattern    =PIn.PString(table.Rows[0][3].ToString());
			Cur.Confirmed  =PIn.PInt   (table.Rows[0][4].ToString());
			Cur.AddTime    =PIn.PInt   (table.Rows[0][5].ToString());
			Cur.Op         =PIn.PInt   (table.Rows[0][6].ToString());
			Cur.Note       =PIn.PString(table.Rows[0][7].ToString());
			Cur.ProvNum    =PIn.PInt   (table.Rows[0][8].ToString());
			Cur.ProvHyg    =PIn.PInt   (table.Rows[0][9].ToString());
			Cur.AptDateTime=PIn.PDateT (table.Rows[0][10].ToString());
			Cur.NextAptNum =PIn.PInt   (table.Rows[0][11].ToString());
			Cur.UnschedStatus =PIn.PInt(table.Rows[0][12].ToString());
			Cur.Lab        =(LabCase)PIn.PInt(table.Rows[0][13].ToString());
		}
	
		public static void DeleteCur(){
			cmd.CommandText="DELETE from appointment WHERE "
				+"aptnum = '"+POut.PInt(Cur.AptNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

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
	
	public struct Appointment{
		public int AptNum;//primary key
		public int PatNum;//foreign key to Patient.PatNum
		public ApptStatus AptStatus;//enum ApptStatus{None=0,Scheduled=1,Complete=2,UnschedList=3,ASAP=4,Broken=5}
		public string Pattern;//Time pattern, X for Dr time, / for assist time
		public int Confirmed;//foreign key to Definition.DefNum
		public int AddTime;//example: 2 would represent add 20 minutes Dr time.
		public int Op;//foreign key to Definition.DefNum
		public string Note;
		public int ProvNum;//foreign key to Provider.ProvNum
		public int ProvHyg;//foreign key to Provider.ProvNum for Hygeine provider
		public DateTime AptDateTime;
		public int NextAptNum;//Only used to show that this apt is derived from specified next apt. otherwise, 0
		public int UnschedStatus;//foreign key to Definition.DefNum. Only used if this is an Unsched appt.
		public LabCase Lab;//enum LabCase{None=0,Sent,Received};
	}//end struct Appointment

	/*=========================================================================================
	=================================== class ApptViews ===========================================*/

	public class ApptViews:DataClass{
		public static ApptView Cur;
		public static ApptView[] List;

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

		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO apptview (description,itemorder) "
				+"VALUES ("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PInt   (Cur.ItemOrder)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.ApptViewNum=InsertID;
		}

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE apptview SET "
				+"description='" +POut.PString(Cur.Description)+"'"
				+",itemorder = '"+POut.PInt   (Cur.ItemOrder)+"'"
				+" WHERE apptviewnum = '"+POut.PInt(Cur.ApptViewNum)+"'";
			NonQ(false);
		}

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

	///<summary>table apptcategory. Enables viewing a variety of operatories.</summary>
	public struct ApptView{
		///<summary>primary key</summary>
		public int ApptViewNum;
		///<summary>Description of this view.  Gets displayed in Appt module.</summary>
		public string Description;
		///<summary>Order to display in lists. Every view must have a unique itemorder, but it is acceptable to have some missing itemorders in the sequence.</summary>
		public int ItemOrder;
	}

	/*=========================================================================================
	=================================== class ApptViewItems ===========================================*/

	public class ApptViewItems:DataClass{
		public static ApptViewItem Cur;
		public static ApptViewItem[] List;
		public static ApptViewItem[] ForCurView;
		//these two are subsets of provs and ops. You can't include hidden prov or op in this list.
		///<summary>Visible providers in appt module.  List of indices to providers.List(short).</summary>
		public static int[] VisProvs;
		///<summary>Visible ops in appt module.  List of indices to Defs.Short[ops].</summary>
		public static int[] VisOps;

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

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE apptviewitem SET "
				+"apptviewnum='" +POut.PInt   (Cur.ApptViewNum)+"'"
				+",opnum = '"    +POut.PInt   (Cur.OpNum)+"'"
				+",provnum = '"  +POut.PInt   (Cur.ProvNum)+"'"
				+" WHERE apptviewitemnum = '"+POut.PInt(Cur.ApptViewItemNum)+"'";
			NonQ(false);
		}

		public static void DeleteCur(){
			cmd.CommandText="DELETE from apptviewitem WHERE apptviewitemnum = '"
				+POut.PInt(Cur.ApptViewItemNum)+"'";
			NonQ(false);
		}

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

	public struct ApptViewItem{//table apptviewitem
		public int ApptViewItemNum;//primary key
		public int ApptViewNum;//foreign key to apptview
		public int OpNum;
		public int ProvNum;
	}


	/*=========================================================================================
	=================================== class AutoCodes ===========================================*/

	public class AutoCodes:DataClass{
		public static AutoCode Cur;
		public static AutoCode[] List;
		public static AutoCode[] ListShort;
		public static Hashtable HList; 

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

		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO autocode (description,ishidden) "
				+"VALUES ("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PBool  (Cur.IsHidden)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.AutoCodeNum=InsertID;
		}

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE autocode SET "
				+"description='"+POut.PString(Cur.Description)+"'"
				+",ishidden = '" +POut.PBool  (Cur.IsHidden)+"'"
				+" WHERE autocodenum = '"+POut.PInt (Cur.AutoCodeNum)+"'";
			NonQ(false);
		}

		public static void DeleteCur(){
			cmd.CommandText = "DELETE from autocode WHERE autocodenum = '"+POut.PInt(Cur.AutoCodeNum)+"'";
			NonQ(false);
		}

	}

	public struct AutoCode{//table AutoCode
		public int AutoCodeNum;//primary key
		public string Description;//Displays meaningful decription, like "amalgam"
		public bool IsHidden;//User can hide autocodes
	}

	/*=========================================================================================
	=================================== class AutoCodeConds ===========================================*/
  
	public class AutoCodeConds:DataClass{
		public static AutoCodeCond Cur;
		public static AutoCodeCond[] List;
		public static AutoCodeCond[] ListForItem;
		private static ArrayList ALlist;
		//public static Hashtable HList; 

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

		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO autocodecond (autocodeitemnum,condition) "
				+"VALUES ("
				+"'"+POut.PInt(Cur.AutoCodeItemNum)+"', "
				+"'"+POut.PInt((int)Cur.Condition)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.AutoCodeCondNum=InsertID;
		}

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE autocodecond SET "
				+"autocodeitemnum='"+POut.PInt(Cur.AutoCodeItemNum)+"'"
				+",condition ='"     +POut.PInt((int)Cur.Condition)+"'"
				+" WHERE autocodecondnum = '"+POut.PInt(Cur.AutoCodeCondNum)+"'";
			NonQ(false);
		}

		public static void DeleteCur(){
			cmd.CommandText = "DELETE from autocodecond WHERE autocodecondnum = '"
				+POut.PInt(Cur.AutoCodeCondNum)+"'";
			NonQ(false);
		}

		public static void DeleteForItemNum(int itemNum){
			cmd.CommandText = "DELETE from autocodecond WHERE autocodeitemnum = '"
				+POut.PInt(itemNum)+"'";//AutoCodeItems.Cur.AutoCodeItemNum)
			NonQ(false); 
		}

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

	public struct AutoCodeCond{//table AutoCodeCond
		//There is usually only one or two conditions for a given item.
		public int AutoCodeCondNum;//primary key
		public int AutoCodeItemNum;//foreign key to AutoCodeItem.AutoCodeItemNum
		public AutoCondition Condition;//enum {Anterior,Posterior,Premolar,Molar,One_Surf,Two_Surf,Three_Surf,Four_Surf,Five_Surf,First,EachAdditional,Maxillary,Mandibular,Primary,Permanent,Pontic,Retainer}
	}

	/*=========================================================================================
	=================================== class AutoCodeItems ===========================================*/

	public class AutoCodeItems:DataClass{
		public static AutoCodeItem Cur;
		public static AutoCodeItem[] List;//all
		public static AutoCodeItem[] ListForCode;//all items for a specific AutoCode
		public static Hashtable HList;//key=ADACode,value=AutoCodeNum

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

		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO autocodeitem (autocodenum,adacode) "
				+"VALUES ("
				+"'"+POut.PInt   (Cur.AutoCodeNum)+"', "
				+"'"+POut.PString(Cur.ADACode)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.AutoCodeItemNum=InsertID;
		}

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE autocodeitem SET "
				+"autocodenum='"+POut.PInt   (Cur.AutoCodeNum)+"'"
				+",adacode ='"  +POut.PString(Cur.ADACode)+"'"
				+" WHERE autocodeitemnum = '"+POut.PInt(Cur.AutoCodeItemNum)+"'";
			NonQ(false);
		}

		public static void DeleteCur(){
			cmd.CommandText = "DELETE from autocodeitem WHERE autocodeitemnum = '"
				+POut.PInt(Cur.AutoCodeItemNum)+"'";
			NonQ(false);
		}

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

	public struct AutoCodeItem{//table AutoCodeItem
		//There are multiple AutoCodeItems for a given AutoCode.  Each Item has one ADA code.
		public int AutoCodeItemNum;//primary key
		public int AutoCodeNum;//foreign key to AutoCode.AutoCodeNum
		public string ADACode;//foreign key to ProcedureCode.ADACode
	}
	
	/*=========================================================================================
	=================================== class Claims ==========================================*/
	public class Claims:DataClass{
		public static Claim[] List;
		public static Hashtable HList;
		public static Claim Cur;
		public static QueueItem[] ListQueue;
		public static QueueItem CurQueue;

		public static void RefreshByCheck(int claimPaymentNum, bool showUnattached){
			cmd.CommandText =
				"SELECT claim.dateservice,claim.provtreat,CONCAT(patient.lname,', ',patient.fname)"
				+",insplan.carrier,SUM(claimproc.feebilled),SUM(claimproc.inspayamt),claim.claimnum"
				+",claimproc.claimpaymentnum"
				+" FROM claim,patient,insplan,claimproc"
				+" WHERE claimproc.claimnum = claim.claimnum"
				+" && patient.patnum = claim.patnum"
				+" && insplan.plannum = claim.plannum"
				+" && claimproc.status = '1'"//received
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

		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM claim "
				+"WHERE claimnum = '"+POut.PInt(Cur.ClaimNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		//public static void GetProcsInClaim(int myClaimNum){
			//moved to claimprocs

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

		public static void UpdateStatus(int claimNum,string newStatus){
			cmd.CommandText = "UPDATE claim SET "
				+"claimstatus = '"+newStatus+"' "
				+"WHERE claimnum = '"+claimNum+"'";
			NonQ(false);
		}

	}//end class Claims

	public struct Claim{
		public int ClaimNum;//primary key
		public int PatNum;//foreign key to Patient.PatNum
		public DateTime DateService;//procedures might have different dates
		public DateTime DateSent;
		public string ClaimStatus;//single char: U,H,W,P,S,or R. A(adj) is no longer used
		//Unsent,Hold until pri received,Waiting in queue,Probably sent,Sent,Received 
		public DateTime DateReceived;
		public int PlanNum;//foreign key to InsPlan.PlanNum
		public int ProvTreat;//foreign key to Provider.ProvNum.  Treating provider.
		public double ClaimFee;//total fee of claim
		public double InsPayEst;//amount insurance is estimated to pay on this claim
		public double InsPayAmt;//amount insurance actually paid.
		//public int ClaimPaymentNum;//Dropped in version 2.1
		public double DedApplied;//deductible applied to this claim
		//public double OverMax;//Dropped in version 2.1
		public string PreAuthString;//The preauth number received from ins.
		public string IsProsthesis;//single char for No, Initial, or Replacement
		public DateTime PriorDate;//date prior prosthesis was placed
		public string ReasonUnderPaid;//note for patient for why insurance didn't pay as expected.
		public string ClaimNote;//note to be sent to insurance
		//public int PriClaimNum;//Dropped in version 2.1.
		//public int SecClaimNum;//Dropped in version 2.1.
		public string ClaimType;//"PreAuth"=preauth, "P"=primary, "S"=secondary, or "Other"=other
			//ClaimType is the new way of determining claimtype. Not allowed to be blank. The update 
			//for version 2.1 added P or S to all existing claims.
		public int ProvBill;//foreign key to Provider.ProvNum .  Billing provider
		public int ReferringProv;//foreign key to Referral.ReferralNum;
		public string RefNumString;//referral number
		public PlaceOfService PlaceService;//enum PlaceOfService{Office=0,PatientsHome,InpatHospital
			//,OutpatHospital,SkilledNursFac,AdultLivCareFac,OtherLocation=6}
		public string AccidentRelated;//blank or A=Auto, E=Employment, O=Other
		public DateTime AccidentDate;
		public string AccidentST;//State.
		public YN EmployRelated;//enum YN{Unknown=0, Yes=1, No=2}
		public bool IsOrtho;
		public int OrthoRemainM;//Remaining months 1-36.
		public DateTime OrthoDate;//Date ortho appliance placed
		public Relat PatRelat;//Relationship to subscriber. You no longer have to look in patient.
			//enum Relat{Self=0,Spouse=1,Child=2,Employee=3,HandicapDep=4,SignifOther=5
			//,InjuredPlaintiff=6,LifePartner=7,Dependent=8}
		//the next 2 fields now provide the user with total control over what other cov shows:
		//This obviously limits the coverage to two insurance companies
		public int PlanNum2;//foreign key to InsPlan.PlanNum for other coverage.  0 if none.
		public Relat PatRelat2;//enum Relat.  for other coverage
		public double WriteOff;//Sum of ClaimProc.Writeoff for this claim
	}//end struct Claim

	public struct QueueItem{//not a database table
		//used in claims waiting tool, and in the Claim Check Edit window.
		public int ClaimNum;
		public bool NoSendElect;
		public string PatName;
		public string ClaimStatus;
		public string Carrier;
		public int PatNum;
		public DateTime DateClaim;
		public string ProvAbbr;
		public double FeeBilled;
		public double InsPayAmt;
		public int ClaimPaymentNum;
	}//end struct QueueItem

/*=========================================================================================
		=================================== class ClaimForms ==========================================*/

	public class ClaimForms:DataClass{
		///<summary>List of all claim forms.</summary>
		public static ClaimForm[] ListLong;
		///<summary>List of all claim forms except those marked as hidden.</summary>
		public static ClaimForm[] ListShort;
		public static ClaimForm Cur;

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
				if(!ListLong[i].IsHidden)
					tempAL.Add(ListLong[i]);
			}
			ListShort=new ClaimForm[tempAL.Count];
			for(int i=0;i<ListShort.Length;i++){
				ListShort[i]=(ClaimForm)tempAL[i];
			}
		}

		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO claimform (description,ishidden,fontname,fontsize"
				+",uniqueid) VALUES("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PBool  (Cur.IsHidden)+"', "
				+"'"+POut.PString(Cur.FontName)+"', "
				+"'"+POut.PFloat (Cur.FontSize)+"', "
				+"'"+POut.PInt   (Cur.UniqueID)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.ClaimFormNum=InsertID;
		}

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE claimform SET "
				+"description = '" +POut.PString(Cur.Description)+"' "
				+",ishidden = '"    +POut.PBool  (Cur.IsHidden)+"' "
				+",fontname = '"    +POut.PString(Cur.FontName)+"' "
				+",fontsize = '"    +POut.PFloat (Cur.FontSize)+"' "
				+",uniqueid = '"    +POut.PInt   (Cur.UniqueID)+"' "
				+"WHERE ClaimFormNum = '"+POut.PInt   (Cur.ClaimFormNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		public static void SetCur(int claimFormNum){
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ClaimFormNum==claimFormNum){
					Cur=ListLong[i];
					return;
				}
			}
			MessageBox.Show("Error. Could not locate Claim Form.");
		}


	}

	///<summary>Table claimform. Stores the information for printing a claim form.</summary>
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
	}

	/*=========================================================================================
		=================================== class ClaimFormItems ==========================================*/

	public class ClaimFormItems:DataClass{
		public static ClaimFormItem[] List;
		public static ClaimFormItem Cur;
		public static ClaimFormItem[] ListForForm;

		public static void Refresh(){
			cmd.CommandText =
				"SELECT * FROM claimformitem";
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

		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM claimformitem "
				+"WHERE ClaimFormItemNum = '"+POut.PInt(Cur.ClaimFormItemNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

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

	public struct ClaimFormItem{//table claimformitem.
		public int ClaimFormItemNum;
		public int ClaimFormNum;//foreign key to ClaimForm.
		public string ImageFileName;//eg ADA2002.emf
		public string FieldName;//one of the available fieldnames for claims
		public string FormatString;//usage not finalized yet
		public float XPos;//the x position of the item on the claim form
		public float YPos;//the y position
		public float Width;//limits the printable area of the item
		public float Height;
	}

	/*=========================================================================================
		=================================== class ClaimPayments ==========================================*/
	public class ClaimPayments:DataClass{
		public static ClaimPayment[] List;
		public static ClaimPayment Cur;

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

		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM claimpayment "
				+"WHERE ClaimPaymentnum = '"+POut.PInt(Cur.ClaimPaymentNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}
	}//end class ClaimPayments

	public struct ClaimPayment{
		public int ClaimPaymentNum;
		public DateTime CheckDate;
		public Double CheckAmt;
		public string CheckNum;
		public string BankBranch;
		public string Note;
	}//end struct ClaimPayment

	/*=========================================================================================
	=================================== class ClaimProcs ===========================================*/

	public class ClaimProcs:DataClass{
		public static ClaimProc Cur;
		public static ClaimProc[] List;//all for Patients.Cur
		public static ClaimProc[] ForClaim;//ClaimProcs for Claims.Cur.ClaimNum
		//public static ArrayList ProcsInClaim;//AL of Procedures

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

		public static void DeleteCur(){
			cmd.CommandText = "DELETE from claimproc WHERE claimprocNum = '"+POut.PInt(Cur.ClaimProcNum)+"'";
			NonQ(false);
		}

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

		public static bool ProcIsAttached(int procNum){
			//used in ProcEdit, and ContrAcct
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum && List[i].Status!=ClaimProcStatus.Preauth){
					return true;
				}
			}
			return false;
		}

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

		public static void DetachAllFromCheck(int claimPaymentNum){
			cmd.CommandText = "UPDATE claimproc SET "
				+"ClaimPaymentNum = '0' "
				+"WHERE claimpaymentNum = '"+claimPaymentNum+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

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

		public static double ComputeBal(){//must make sure Refresh is done first
			double retVal=0;
			//double pat;
			for(int i=0;i<List.Length;i++){
				if(List[i].Status==ClaimProcStatus.Adjustment){
					continue;//claim adjustments do not affect patient balance
				}
				if(List[i].Status==ClaimProcStatus.Preauth){
					continue;//preauthorizations do not affect patient balance
				}
				if(List[i].Status==ClaimProcStatus.Received
					|| List[i].Status==ClaimProcStatus.Supplemental){//because supplemental are always received
					retVal-=List[i].InsPayAmt;
				}
				else{
					retVal-=List[i].InsPayEst;
				}
				retVal-=List[i].WriteOff;
			}
			return retVal;
		}


	}

	public struct ClaimProc{//table claimproc.  
		//Links procedures to claims.
		//Also links ins payments to procedures or claims
		//Warning-One proc might be linked twice to a given claim if insurance made two payments.
		//Many of the important fields are actually optional.  For instance, ProcNum is only 
		//required if itemizing ins payment, and ClaimNum is blank if Status=A.
		public int ClaimProcNum;//primary key
		public int ProcNum;//foreign key to procedurelog.ProcNum
		public int ClaimNum;//foreign key to Claim.ClaimNum.  
		public int PatNum;//foreign key to Patient.PatNum
		//new fields:
		public int ProvNum;//foreign key to Provider.ProvNum
		public double FeeBilled;//might not be the same as the actual fee
		public double InsPayEst;//amount this carrier is expected to pay.
		public double DedApplied;//deductible applied to this procedure only.
		public ClaimProcStatus Status;//enum ClaimProcStatus
					//{NotReceived=0,Received,Preauth,Adjustment,Supplemental=4}
		public double InsPayAmt;//amount insurance actually paid
		public string Remarks;//The remarks that insurance sends about procedures.
		public int ClaimPaymentNum;//foreign key to ClaimPayment.ClaimPaymentNum(the insurance check)
		public int PlanNum;//foreign key to insplan.PlanNum
		public DateTime DateCP;//especially useful for adjustments and balance.
			//For payments, MUST be date of treatment, not payment, to properly track annual benefits.
		public Double WriteOff;//amount not covered by ins which is written off
		public string CodeSent;//valid ADA code already trimmed to 5 char or alternate code.
			//Blank not allowed if is procedure.
	}

	/*=========================================================================================
	=================================== class Commlogs ==========================================*/

	public class Commlogs:DataClass{
		public static Commlog[] List;//for one patient
		public static Commlog Cur;
		//public static Hashtable HList;

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
				List[i].CommType  = PIn.PInt   (table.Rows[i][3].ToString());
			}
		}

		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO commlog (patnum"
				+",commdate,commtype) VALUES("
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PDate  (Cur.CommDate)+"', "
				+"'"+POut.PInt   (Cur.CommType)+"')";
			NonQ(false);
		}

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE commlog SET "
				+"patnum = '"   +POut.PInt   (Cur.PatNum)+"', "
				+"commdate= '"  +POut.PDate  (Cur.CommDate)+"', "
				+"commtype = '" +POut.PInt   (Cur.CommType)+"' "
				+"WHERE commlognum = '"+POut.PInt(Cur.CommlogNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM commlog WHERE commlognum = '"+Cur.CommlogNum.ToString()+"'";
			NonQ(false);
		}

	}

	public struct Commlog{//table commlog
		//Will eventually track all communications including emails, phonecalls, letters, etc.
		public int CommlogNum;//primary key
		public int PatNum;//foreign key to patient.PatNum
		public DateTime CommDate;
		public int CommType;//always '1' for 'statement sent' in version 2.  Will later be an enum.
	}


	/*=========================================================================================
	=================================== class Computers ==========================================*/

	public class Computers:DataClass{
		public static Computer[] List;
		public static Computer Cur;
		public static Hashtable HList;

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
   

		public static void InsertCur(){//ONLY use this if compname is not already present
			cmd.CommandText = "INSERT INTO computer (compname,"
				+"printername) VALUES("
				+"'"+POut.PString(Cur.CompName)+"', "
				+"'"+POut.PString(Cur.PrinterName)+"')";
			NonQ(false);
		}

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE computer SET "
				+"compname = '"    +POut.PString(Cur.CompName)+"', "
				+"printername = '" +POut.PString(Cur.PrinterName)+"' "
				+"WHERE computernum = '"+POut.PInt(Cur.ComputerNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}
	}

	public struct Computer{//keeps track of the computers in an office
		public int ComputerNum;//primary key
		public string CompName;//Name of computer
		public string PrinterName;//Default printer for each computer
	}

	/*=========================================================================================
		=================================== class Contacts ==========================================*/

	public class Contacts:DataClass{
		public static Contact Cur;
		public static Contact[] List;//for one category only. Not refreshed with local data
		//public static Contact[] ListForCat;

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

		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM contact WHERE contactnum = '"+Cur.ContactNum.ToString()+"'";
			NonQ(false);
		}

	}

	public struct Contact{//table contact
		public int ContactNum;//primary key
		public string LName;
		public string FName;//optional
		public string WkPhone;
		public string Fax;
		public int Category;//foreign key to Definitions.DefNum
		public string Notes;
	}

	/*=========================================================================================
		=================================== class CovPats ==========================================*/

	public class CovPats:DataClass{
		//the first two are the usual lists of interest
		public static int[] PriList;//filled during refresh
		public static int[] SecList;//filled during refresh
		public static CovPat[] List;
		public static CovPat Cur;
		public static CovPat[] ListForPlan;

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

		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM covpat WHERE covpatnum = '"+Cur.CovPatNum.ToString()+"'";
			NonQ(false);
		}

		//public static void DeleteAllInCurPlan(){//obsolete
		//	cmd.CommandText = "DELETE FROM covpat WHERE plannum = '"+InsPlans.Cur.PlanNum.ToString()+"'";
		//	NonQ(false);
		//}

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

	public struct CovPat{//table covpat.  
		//as shown below, a covpat can only be ONE of the three options below
		public int CovPatNum;//primary key
		public int CovCatNum;//foreign key to covcat.covcatnum
		public int PlanNum;//OPT 1: foreign key to insplan.plannum
		public int PriPatNum;//OPT 2: fk to patient.patnum for primary coverage
		public int SecPatNum;//OPT 2: fk to patient.patnum for primary coverage
		public int Percent;//valid -1 to 100???
	}

	/*=========================================================================================
	=================================== class CovCats ==========================================*/

	public class CovCats:DataClass{
		public static CovCat[] List;
		public static CovCat[] ListShort;
		public static CovCat Cur;
		public static int Selected;

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

		public static int GetCatNum(string myADACode){
			return 0;
		}
		
		public static double GetDefaultPercent(int myCovCatNum){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(myCovCatNum==List[i].CovCatNum){
					retVal=(double)List[i].DefaultPercent;
				}
			}
			return retVal;	
		}

		public static string GetDesc(int covCatNum){
			string retStr="";
			for(int i=0;i<List.Length;i++){
				if(covCatNum==List[i].CovCatNum){
					retStr=List[i].Description;
				}
			}
			return retStr;	
		}

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

		public static int GetOrderShort(int CovCatNum){
			int retVal=-1;
			for(int i=0;i<ListShort.Length;i++){
				if(CovCatNum==ListShort[i].CovCatNum){
					retVal=i;
				}
			}
			return retVal;	
		}	

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

		public static void SetOrder(int mySelNum, int myItemOrder){
			CovCat temp=List[mySelNum];
			temp.CovOrder=myItemOrder;
			Cur=temp;
			UpdateCur();
		}

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

	public struct CovCat{
		public int CovCatNum;
		public string Description;
		public int DefaultPercent;
		public bool IsPreventive;
		public int CovOrder;
		public bool IsHidden;
	}


	/*=========================================================================================
		=================================== class Conversions ==========================================*/

	public class Conversions:DataClass{
		public static string[] ArrayQueryText;
		public static string SelectText;
		public static DataTable TableQ;

		public static bool SubmitQuery(){//return true if successful
			try{
				//int rowsUpdated;
				con.Open();
				for(int i=0;i<ArrayQueryText.Length;i++){
					cmd.CommandText=ArrayQueryText[i];
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

	public class CovSpans:DataClass{
		public static CovSpan[] List;
		public static CovSpan Cur;
		
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

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE covspan SET "
				+ "covcatnum = '" +POut.PInt   (Cur.CovCatNum)+"'"
				+",fromcode = '"  +POut.PString(Cur.FromCode)+"'"
				+",tocode = '"    +POut.PString(Cur.ToCode)+"'"
				+" WHERE covspannum = '"+POut.PInt(Cur.CovSpanNum)+"'";
			NonQ(false);
		}

		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO covspan (covcatnum,"
				+"fromcode,tocode) VALUES("
				+"'"+POut.PInt   (Cur.CovCatNum)+"', "
				+"'"+POut.PString(Cur.FromCode)+"', "
				+"'"+POut.PString(Cur.ToCode)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		public static void DeleteCur(){
			cmd.CommandText="DELETE FROM covspan"
				+" WHERE covspannum = '"+POut.PInt(Cur.CovSpanNum)+"'";
			NonQ(false);
		}

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

	public struct CovSpan{
		public int CovSpanNum;
		public int CovCatNum;
		public string FromCode;
		public string ToCode;
	}

	/*=========================================================================================
	=================================== class Defs ==========================================*/

	public class Defs:DataClass{
		public static Def[] List;//for only one category. used in Defs dialog only.
		public static Def Cur;
		public static bool IsSelected;
		public static int Selected;
		public static Def[][] Short;//not showing the hidden rows
		public static Def[][] Long;//same list, but showing the hidden rows

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
					Long[j][i].Category  = PIn.PInt   (table.Rows[i][1].ToString());
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
					Short[j][i].Category  = PIn.PInt   (table.Rows[i][1].ToString());
					Short[j][i].ItemOrder = PIn.PInt   (table.Rows[i][2].ToString());
					Short[j][i].ItemName  = PIn.PString(table.Rows[i][3].ToString());
					Short[j][i].ItemValue = PIn.PString(table.Rows[i][4].ToString());
					Short[j][i].ItemColor = Color.FromArgb(PIn.PInt(table.Rows[i][5].ToString()));
					Short[j][i].IsHidden  = PIn.PBool  (table.Rows[i][6].ToString());
				}
			}//end for j
			//MessageBox.Show(Short[(int)DefCat.ApptConfirmed].Length.ToString());
		}

		public static string GetName(DefCat myCat, int myDefNum){
			string retStr="";
			for(int i=0;i<Long[(int)myCat].GetLength(0);i++){
				if(Long[(int)myCat][i].DefNum==myDefNum){
					retStr=Long[(int)myCat][i].ItemName;
				}
			}
			return retStr;
		}

		public static int GetOrder(DefCat myCat, int myDefNum){
			//gets the index in the list of unhidden (the Short list).
			for(int i=0;i<Short[(int)myCat].GetLength(0);i++){
				if(Short[(int)myCat][i].DefNum==myDefNum){
					return i;
				}
			}
			return -1;
		}

		public static string GetValue(DefCat myCat, int myDefNum){
			string retStr="";
			for(int i=0;i<Long[(int)myCat].GetLength(0);i++){
				if(Long[(int)myCat][i].DefNum==myDefNum){
					retStr=Long[(int)myCat][i].ItemValue;
				}
			}
			return retStr;
		}

		public static Color GetColor(DefCat myCat, int myDefNum){
			Color retCol=Color.White;
			for(int i=0;i<Long[(int)myCat].GetLength(0);i++){
				if(Long[(int)myCat][i].DefNum==myDefNum){
					retCol=Long[(int)myCat][i].ItemColor;
				}
			}
			return retCol;
		}

		public static void GetCatList(int myCat){
			cmd.CommandText =
				"SELECT * from definition"
				+" WHERE category = '"+myCat+"'"
				+" ORDER BY ItemOrder";
			FillTable();
			List=new Def[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].DefNum    = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Category  = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].ItemOrder = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].ItemName  = PIn.PString(table.Rows[i][3].ToString());
				List[i].ItemValue = PIn.PString(table.Rows[i][4].ToString());
				List[i].ItemColor = Color.FromArgb(PIn.PInt(table.Rows[i][5].ToString()));
				List[i].IsHidden  = PIn.PBool  (table.Rows[i][6].ToString());
			}//end for
		}

		public static void UpdateCur(){
			cmd.CommandText = "UPDATE definition SET "
				+ "category = '"  +POut.PInt   (Cur.Category)+"'"
				+",itemorder = '" +POut.PInt   (Cur.ItemOrder)+"'"
				+",itemname = '"  +POut.PString(Cur.ItemName)+"'"
				+",itemvalue = '" +POut.PString(Cur.ItemValue)+"'"
				+",itemcolor = '" +POut.PInt   (Cur.ItemColor.ToArgb())+"'"
				+",ishidden = '"  +POut.PBool  (Cur.IsHidden)+"'"
				+" WHERE defnum = '"+POut.PInt(Cur.DefNum)+"'";
			NonQ(false);
		}

		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO definition (category,itemorder,"
				+"itemname,itemvalue,itemcolor,ishidden) VALUES("
				+"'"+POut.PInt   (Cur.Category)+"', "
				+"'"+POut.PInt   (Cur.ItemOrder)+"', "
				+"'"+POut.PString(Cur.ItemName)+"', "
				+"'"+POut.PString(Cur.ItemValue)+"', "
				+"'"+POut.PInt   (Cur.ItemColor.ToArgb())+"', "
				+"'"+POut.PBool  (Cur.IsHidden)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Defs.Cur.DefNum=InsertID;//used in conversion
		}

		public static void HideDef(){//hides Selected
			Cur=List[Selected];
			Cur.IsHidden=true;
			UpdateCur();
		}

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

	public struct Def{//table Definition (almost every table in the database links to Definition
		public int DefNum;//primary key
		public int Category;//DefCat is a very long enumeration(22) of the categories
		public int ItemOrder;//Order that each item shows on various lists
		public string ItemName;//Each category is a little different.  This field is usually the common name of the item.
		public string ItemValue;//This field can be used to store extra info about the item.
		public Color ItemColor;//some categories include color options.
		public bool IsHidden;//if hidden, the item will not show on any list, but can still be referenced.
	}

	/*==================================================================================================
	 =================================== Class DocAttaches ===========================================*/
	public class DocAttaches:DataClass{
		//public Break Cur;
		public static DocAttach Cur;
		public static DocAttach[] List;

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

		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO docattach (PatNum, DocNum) VALUES ("
				+"'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PInt   (Cur.DocNum)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
			//Cur.DocAttachNum=InsertID;
		}

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
	public struct DocAttach{
		public int DocAttachNum;
		public int PatNum;
		public int DocNum;
		
	}//end struct DocAttach

	/*=========================================================================================
	=================================== class Documentss ==========================================*/

	public class Documents:DataClass{
		public static Document[] List;
		//public static DocBackup[] ListBackup;
		public static Document Cur;	

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

		public static void GetCurrent(string docNum){
			for (int i = 0; i<List.Length; i+=1){
				if (List[i].DocNum.ToString()==docNum){
					Cur = List[i];
				}
			}
		}

		public static void DeleteCur(){
			cmd.CommandText = "DELETE from document WHERE docnum = '"+Cur.DocNum.ToString()+"'";
			NonQ(false);
			cmd.CommandText = "DELETE from docattach WHERE docnum = '"+Cur.DocNum.ToString()+"'";
			NonQ(false);
			//Cur.IsDeleted=true;
			//UpdateCur();
		}

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

	public struct Document{//table Document
		public int DocNum;//primary key
		public string Description;//description of document
		public DateTime DateCreated;
		public int DocCategory;//foreign key to Definition.DefNum.  Categories for documents
		public int WithPat;//foreign key to Patient.PatNum.  Patient folder that document is in.(for sharing situations later)    
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









