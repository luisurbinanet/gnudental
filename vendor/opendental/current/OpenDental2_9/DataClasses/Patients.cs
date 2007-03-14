using System;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the patient table in the database.</summary>
	public struct Patient{
		///<summary>Primary key.</summary>
		public int    PatNum;
		///<summary>Last name.</summary>
		public string LName;
		///<summary>First name.</summary>
		public string FName;
		///<summary>Middle initial or name.</summary>
		public string MiddleI;
		///<summary>Preferred name.</summary>
		public string Preferred;
		///<summary>See the PatientStatus enumeration.</summary>
		public PatientStatus PatStatus;
		///<summary>See the PatientGender enumeration.</summary>
		public PatientGender Gender;
		///<summary>See the PatientPosition enumeration.</summary>
		public PatientPosition Position;
		///<summary></summary>
		public DateTime Birthdate;
		///<summary>In the US, this is 9 digits, no dashes. For all other countries, any punctuation or format is allowed.</summary>
		public string SSN;
		///<summary></summary>
		public string Address;
		///<summary></summary>
		public string Address2;
		///<summary></summary>
		public string City;
		///<summary>2 Char in USA</summary>
		public string State;
		///<summary></summary>
		public string Zip;
		///<summary>Includes any punctuation</summary>
		public string HmPhone;
		///<summary></summary>
		public string WkPhone;
		///<summary></summary>
		public string WirelessPhone;
		///<summary>Foreign key to patient.PatNum.  Head of household.</summary>
		public int    Guarantor;
		///<summary>Derived from Birthdate.  Not in the database table.</summary>
		public string Age;
		///<summary>Single char. Shows at upper left corner of appointments.  Suggested use is A,B,or C to designate creditworthiness, but it can actually be used for any purpose.</summary>
		public string CreditType;
		///<summary></summary>
		public string Email;
		///<summary></summary>
		public string Salutation;
		///<summary>Foreign key to insplan.PlanNum.  Primary insurance.</summary>
		public int PriPlanNum;//
		///<summary>Relationship to subscriber for primary insurance.  See the Relat enumeration.</summary>
		public Relat PriRelationship;
		///<summary>Foreign key to insplan.PlanNum.  Secondary insurance.</summary>
		public int SecPlanNum;//
		///<summary>Relationship to subscriber for secondary insurance.</summary>
		public Relat SecRelationship;
		///<summary>Current patient balance.(not family)</summary>
		public double EstBalance;
		///<summary>May be 0(none) or -1(done), otherwise it is the foreign key to appointment.AptNum.  This is the appointment that will show in the Chart module and in the Next appointment tracker.  It will never show in the Appointments module.</summary>
		public int NextAptNum;//
		///<summary>Foreign key to provider.ProvNum.  The patient's primary provider.</summary>
		public int PriProv;
		///<summary>Foreign key to provider.ProvNum.  Secondary provider (hygienist)</summary>
		public int SecProv;//
		///<summary>Foreign key to definition.DefNum.  Fee schedule for this patient.</summary>
		public int FeeSched;
		///<summary>Foreign key to definition.DefNum.  Must have a value, or the patient will not show on some reports.</summary>
		public int BillingType;
		///<summary>Months between recalls.</summary>
		public int RecallInterval;
		///<summary>Foreign key to Definition.DefNum, or 0 for none.</summary>
		public int RecallStatus;
		///<summary>Name of folder where images will be stored. Not editable for now.</summary>
		public string ImageFolder;
		///<summary>Address or phone note.</summary>
		public string AddrNote;
		///<summary>Family financial urgent note.  Only stored with guarantor, and shared for family.</summary>
		public string FamFinUrgNote;
		///<summary>Individual patient note for Urgent medical.</summary>
		public string MedUrgNote;
		///<summary>Individual patient note for Appointment module note.</summary>
		public string ApptModNote;
		///<summary>Single char for Nonstudent, Parttime, or Fulltime.  Blank=Nonstudent</summary>
		public string StudentStatus;
		///<summary></summary>
		public string SchoolName;
		///<summary>Max 15 char.  Used for reference to previous programs.</summary>
		public string ChartNumber;
		///<summary>Optional. The Medicaid ID for this patient.</summary>
		public string MedicaidID;
		///<summary>Aged balance from 0 to 30 days old. Aging numbers are for entire family.  Only stored with guarantor.</summary>
		public double Bal_0_30;
		///<summary>Aged balance from 31 to 60 days old. Aging numbers are for entire family.  Only stored with guarantor.</summary>
		public double Bal_31_60;
		///<summary>Aged balance from 61 to 90 days old. Aging numbers are for entire family.  Only stored with guarantor.</summary>
		public double Bal_61_90;
		///<summary>Aged balance over 90 days old. Aging numbers are for entire family.  Only stored with guarantor.</summary>
		public double BalOver90;
		///<summary>Insurance Estimate for entire family.</summary>
		public double InsEst;
		///<summary>Teeth to display in chart as primary. eg: "1,2,3,4,5,12,13"</summary>
		public string PrimaryTeeth;
		///<summary>Total balance for entire family before insurance estimate.  Not the same as the sum of the 4 aging balances because this can be negative.  Only stored with guarantor.</summary>
		public double BalTotal;
		///<summary>Foreign key to employer.EmployerNum.</summary>
		public int EmployerNum;
		///<summary>A freeform not section for details about this patient's employment. It will later be viewable in the Chart module next to the service notes.</summary>
		public string EmploymentNote;
	}

	/*=========================================================================================
	=================================== class Patients ===========================================*/
	///<summary></summary>
	public class Patients:DataClass{
		///<summary>Always test this to see if a patient is loaded, not patient.Cur.PatNum.</summary>
		public static bool PatIsLoaded=false;
		///<summary>Current patient.</summary>
		public static Patient Cur;
		///<summary>Stores limited information about a patient. Filled first via GetLim.</summary>
		public static Patient Lim;
		///<summary>Stores formatted LName, FName, MI for the Lim patient.</summary>
		public static string LimName;
		///<summary>A list of patients in one family (sharing a GuarantorNum)</summary>
		public static Patient[] FamilyList;
		///<summary>Used in the Aging window.</summary>
		public static PatAging[] AgingList;
		///<summary>The index within the FamilyList of the Guarantor. Might not be necessary anymore since the guarantor should always be index 0.</summary>
		public static int GuarIndex;
		///<summary></summary>
		public static RecallItem[] RecallList;
		///<summary>A list of all patient names. Key=patNum, value=formatted name.  Fill with GetHList.</summary>
		public static Hashtable HList;
		//<summary>Used in the Select Patient window.</summary>
		//public static Patient[] PtList;
		///<summary>This replaces the PtList for use in the Select Patient window.</summary>
		public static DataTable PtDataTable;

		///<summary></summary>
		public static void GetFamily(int patNum){
			cmd.CommandText= 
				"SELECT guarantor FROM patient "
				+"WHERE patnum = '"+POut.PInt(patNum)+"'";
			FillTable();
			if(table.Rows.Count==0){
				PatIsLoaded=false;
				Cur=new Patient();
				FamilyList=new Patient[1];
				FamilyList[0]=Cur;
				GuarIndex=0;
				return;
			}
			cmd.CommandText= 
				"SELECT * "
				+"FROM patient "
				+"WHERE guarantor = '"+table.Rows[0][0].ToString()+"'"
				+" ORDER BY (patnum!=guarantor), birthdate";
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			FamilyList = new Patient[table.Rows.Count];
			//MessageBox.Show(table.Rows.Count.ToString());
			for (int i=0;i<table.Rows.Count;i++){
				FamilyList[i].PatNum       = PIn.PInt   (table.Rows[i][0].ToString());
				FamilyList[i].LName        = PIn.PString(table.Rows[i][1].ToString());
				FamilyList[i].FName        = PIn.PString(table.Rows[i][2].ToString());
				FamilyList[i].MiddleI      = PIn.PString(table.Rows[i][3].ToString());
				FamilyList[i].Preferred    = PIn.PString(table.Rows[i][4].ToString());
				FamilyList[i].PatStatus    = (PatientStatus)PIn.PInt   (table.Rows[i][5].ToString());
				FamilyList[i].Gender       = (PatientGender)PIn.PInt   (table.Rows[i][6].ToString());
				FamilyList[i].Position     = (PatientPosition)PIn.PInt   (table.Rows[i][7].ToString());
				FamilyList[i].Birthdate    = PIn.PDate  (table.Rows[i][8].ToString());
				FamilyList[i].Age=Shared.DateToAge(FamilyList[i].Birthdate);
				FamilyList[i].SSN          = PIn.PString(table.Rows[i][9].ToString());
				FamilyList[i].Address      = PIn.PString(table.Rows[i][10].ToString());
				FamilyList[i].Address2     = PIn.PString(table.Rows[i][11].ToString());
				FamilyList[i].City         = PIn.PString(table.Rows[i][12].ToString());
				FamilyList[i].State        = PIn.PString(table.Rows[i][13].ToString());
				FamilyList[i].Zip          = PIn.PString(table.Rows[i][14].ToString());
				FamilyList[i].HmPhone      = PIn.PString(table.Rows[i][15].ToString());
				FamilyList[i].WkPhone      = PIn.PString(table.Rows[i][16].ToString());
				FamilyList[i].WirelessPhone= PIn.PString(table.Rows[i][17].ToString());
				FamilyList[i].Guarantor    = PIn.PInt   (table.Rows[i][18].ToString());
				FamilyList[i].CreditType   = PIn.PString(table.Rows[i][19].ToString());
				FamilyList[i].Email        = PIn.PString(table.Rows[i][20].ToString());
				FamilyList[i].Salutation   = PIn.PString(table.Rows[i][21].ToString());
				FamilyList[i].PriPlanNum   = PIn.PInt   (table.Rows[i][22].ToString());
				FamilyList[i].PriRelationship=(Relat)PIn.PInt(table.Rows[i][23].ToString());
				FamilyList[i].SecPlanNum   = PIn.PInt   (table.Rows[i][24].ToString());
				FamilyList[i].SecRelationship=(Relat)PIn.PInt(table.Rows[i][25].ToString());
				FamilyList[i].EstBalance   = PIn.PDouble(table.Rows[i][26].ToString());
				FamilyList[i].NextAptNum   = PIn.PInt   (table.Rows[i][27].ToString());
				FamilyList[i].PriProv      = PIn.PInt   (table.Rows[i][28].ToString());
				FamilyList[i].SecProv      = PIn.PInt   (table.Rows[i][29].ToString());
				FamilyList[i].FeeSched     = PIn.PInt   (table.Rows[i][30].ToString());
				FamilyList[i].BillingType  = PIn.PInt   (table.Rows[i][31].ToString());
				FamilyList[i].RecallInterval=PIn.PInt   (table.Rows[i][32].ToString());
				FamilyList[i].RecallStatus = PIn.PInt   (table.Rows[i][33].ToString());
				FamilyList[i].ImageFolder  = PIn.PString(table.Rows[i][34].ToString());
				FamilyList[i].AddrNote     = PIn.PString(table.Rows[i][35].ToString());
				FamilyList[i].FamFinUrgNote= PIn.PString(table.Rows[i][36].ToString());
				FamilyList[i].MedUrgNote   = PIn.PString(table.Rows[i][37].ToString());
				FamilyList[i].ApptModNote  = PIn.PString(table.Rows[i][38].ToString());
				FamilyList[i].StudentStatus= PIn.PString(table.Rows[i][39].ToString());
				FamilyList[i].SchoolName   = PIn.PString(table.Rows[i][40].ToString());
				FamilyList[i].ChartNumber  = PIn.PString(table.Rows[i][41].ToString());
				FamilyList[i].MedicaidID   = PIn.PString(table.Rows[i][42].ToString());
				FamilyList[i].Bal_0_30     = PIn.PDouble(table.Rows[i][43].ToString());
				FamilyList[i].Bal_31_60    = PIn.PDouble(table.Rows[i][44].ToString());
				FamilyList[i].Bal_61_90    = PIn.PDouble(table.Rows[i][45].ToString());
				FamilyList[i].BalOver90    = PIn.PDouble(table.Rows[i][46].ToString());
				FamilyList[i].InsEst       = PIn.PDouble(table.Rows[i][47].ToString());
				FamilyList[i].PrimaryTeeth = PIn.PString(table.Rows[i][48].ToString());
				FamilyList[i].BalTotal     = PIn.PDouble(table.Rows[i][49].ToString());
				FamilyList[i].EmployerNum  = PIn.PInt   (table.Rows[i][50].ToString());
				FamilyList[i].EmploymentNote=PIn.PString(table.Rows[i][51].ToString());
				if(FamilyList[i].PatNum==patNum)
					Cur=FamilyList[i];
				if (FamilyList[i].Guarantor==FamilyList[i].PatNum)
					GuarIndex=i;
			}
			PatIsLoaded=true;
			//InfoChanged=false;//unused?
		}

		///<summary></summary>
		public static void InsertCur(){//ONLY for new patients
			cmd.CommandText = "INSERT INTO patient (lname,fname,middlei,preferred,patstatus,gender,"
				+"position,birthdate,ssn,address,address2,city,state,zip,hmphone,wkphone,wirelessphone,"
				+"guarantor,credittype,email,salutation,priplannum,prirelationship,secplannum,"
				+"secrelationship,estbalance,nextaptnum,priprov,secprov,feesched,billingtype,recallinterval,"
				+"recallstatus,imagefolder,addrnote,famfinurgnote,medurgnote,apptmodnote,"
				+"studentstatus,schoolname,chartnumber,medicaidid"
				+",Bal_0_30,Bal_31_60,Bal_61_90,BalOver90,insest,primaryteeth,BalTotal"
				+",EmployerNum,EmploymentNote) VALUES("
				+"'"+POut.PString(Cur.LName)+"', "
				+"'"+POut.PString(Cur.FName)+"', "
				+"'"+POut.PString(Cur.MiddleI)+"', "
				+"'"+POut.PString(Cur.Preferred)+"', "
				+"'"+POut.PInt   ((int)Cur.PatStatus)+"', "
				+"'"+POut.PInt   ((int)Cur.Gender)+"', "
				+"'"+POut.PInt   ((int)Cur.Position)+"', "
				+"'"+POut.PDate  (Cur.Birthdate)+"', "
				+"'"+POut.PString(Cur.SSN)+"', "
				+"'"+POut.PString(Cur.Address)+"', "
				+"'"+POut.PString(Cur.Address2)+"', "
				+"'"+POut.PString(Cur.City)+"', "
				+"'"+POut.PString(Cur.State)+"', "
				+"'"+POut.PString(Cur.Zip)+"', "
				+"'"+POut.PString(Cur.HmPhone)+"', "
				+"'"+POut.PString(Cur.WkPhone)+"', "
				+"'"+POut.PString(Cur.WirelessPhone)+"', "
				+"'"+POut.PInt   (Cur.Guarantor)+"', "
				+"'"+POut.PString(Cur.CreditType)+"', "
				+"'"+POut.PString(Cur.Email)+"', "
				+"'"+POut.PString(Cur.Salutation)+"', "
				+"'"+POut.PInt   (Cur.PriPlanNum)+"', "
				+"'"+POut.PInt   ((int)Cur.PriRelationship)+"', "
				+"'"+POut.PInt   (Cur.SecPlanNum)+"', "
				+"'"+POut.PInt   ((int)Cur.SecRelationship)+"', "
				+"'"+POut.PDouble(Cur.EstBalance)+"', "
				+"'"+POut.PInt   (Cur.NextAptNum)+"', "
				+"'"+POut.PInt   (Cur.PriProv)+"', "
				+"'"+POut.PInt   (Cur.SecProv)+"', "
				+"'"+POut.PInt   (Cur.FeeSched)+"', "
				+"'"+POut.PInt   (Cur.BillingType)+"', "
				+"'"+POut.PInt   (Cur.RecallInterval)+"', "
				+"'"+POut.PInt   (Cur.RecallStatus)+"', "
				+"'"+POut.PString(Cur.ImageFolder)+"', "
				+"'"+POut.PString(Cur.AddrNote)+"', "
				+"'"+POut.PString(Cur.FamFinUrgNote)+"', "
				+"'"+POut.PString(Cur.MedUrgNote)+"', "
				+"'"+POut.PString(Cur.ApptModNote)+"', "
				+"'"+POut.PString(Cur.StudentStatus)+"', "
				+"'"+POut.PString(Cur.SchoolName)+"', "
				+"'"+POut.PString(Cur.ChartNumber)+"', "
				+"'"+POut.PString(Cur.MedicaidID)+"', "
				+"'"+POut.PDouble(Cur.Bal_0_30)+"', "
				+"'"+POut.PDouble(Cur.Bal_31_60)+"', "
				+"'"+POut.PDouble(Cur.Bal_61_90)+"', "
				+"'"+POut.PDouble(Cur.BalOver90)+"', "
				+"'"+POut.PDouble(Cur.InsEst)+"', "
				+"'"+POut.PString(Cur.PrimaryTeeth)+"', "
				+"'"+POut.PDouble(Cur.BalTotal)+"', "
				+"'"+POut.PInt   (Cur.EmployerNum)+"', "
				+"'"+POut.PString(Cur.EmploymentNote)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.PatNum=InsertID;
			//PatientNotes PatientNotes=new PatientNotes();
			//PatientNotes.SaveCur();
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE patient SET " 
				+ "LName = '"     +POut.PString(Cur.LName)+"'"
				+",FName = '"     +POut.PString(Cur.FName)+"'"
				+",MiddleI = '"   +POut.PString(Cur.MiddleI)+"'"
				+",Preferred = '" +POut.PString(Cur.Preferred)+"'"
				+",patStatus = '" +POut.PInt   ((int)Cur.PatStatus)+"'"
				+",Gender = '"    +POut.PInt   ((int)Cur.Gender)+"'"
				+",Position = '"  +POut.PInt   ((int)Cur.Position)+"'"
				+",Birthdate = '" +POut.PDate  (Cur.Birthdate)+"'" 
				+",SSN = '"       +POut.PString(Cur.SSN)+"'"
				+",Address = '"   +POut.PString(Cur.Address)+"'"
				+",Address2 = '"  +POut.PString(Cur.Address2)+"'"
				+",City = '"      +POut.PString(Cur.City)+"'"
				+",State = '"     +POut.PString(Cur.State)+"'"
				+",Zip = '"       +POut.PString(Cur.Zip)+"'"
				+",HmPhone = '"   +POut.PString(Cur.HmPhone)+"'"
				+",WkPhone = '"   +POut.PString(Cur.WkPhone)+"'"
				+",WirelessPhone='"    +POut.PString(Cur.WirelessPhone)+"'"
				+",guarantor = '"      +POut.PInt   (Cur.Guarantor)+"'"
				+",credittype = '"     +POut.PString(Cur.CreditType)+"'"
				+",Email = '"          +POut.PString(Cur.Email)+"'"
				+",Salutation = '"     +POut.PString(Cur.Salutation)+"'"
				+",priplannum = '"     +POut.PInt   (Cur.PriPlanNum)+"'"
				+",prirelationship = '"+POut.PInt((int)Cur.PriRelationship)+"'"
				+",secplannum = '"     +POut.PInt   (Cur.SecPlanNum)+"'"
				+",secrelationship = '"+POut.PInt((int)Cur.SecRelationship)+"'"
				+",estbalance = '"     +POut.PDouble(Cur.EstBalance)+"'"
				+",nextaptnum = '"     +POut.PInt   (Cur.NextAptNum)+"'"
				+",priprov = '"        +POut.PInt   (Cur.PriProv)+"'"
				+",secprov = '"        +POut.PInt   (Cur.SecProv)+"'"
				+",feesched = '"       +POut.PInt   (Cur.FeeSched)+"'"
				+",billingtype = '"    +POut.PInt   (Cur.BillingType)+"'"
				+",recallinterval = '" +POut.PInt   (Cur.RecallInterval)+"'"
				+",recallstatus = '"   +POut.PInt   (Cur.RecallStatus)+"'"
				+",imagefolder = '"    +POut.PString(Cur.ImageFolder)+"'"
				+",addrnote = '"       +POut.PString(Cur.AddrNote)+"'"
				+",famfinurgnote = '"  +POut.PString(Cur.FamFinUrgNote)+"'"
				+",medurgnote = '"     +POut.PString(Cur.MedUrgNote)+"'"
				+",apptmodnote = '"    +POut.PString(Cur.ApptModNote)+"'"
				+",studentstatus = '"  +POut.PString(Cur.StudentStatus)+"'"
				+",schoolname = '"     +POut.PString(Cur.SchoolName)+"'"
				+",chartnumber = '"    +POut.PString(Cur.ChartNumber)+"'"
				+",medicaidid = '"     +POut.PString(Cur.MedicaidID)+"'"
				+",Bal_0_30 = '"       +POut.PDouble(Cur.Bal_0_30)+"'"
				+",Bal_31_60 = '"      +POut.PDouble(Cur.Bal_31_60)+"'"
				+",Bal_61_90 = '"      +POut.PDouble(Cur.Bal_61_90)+"'"
				+",BalOver90 = '"      +POut.PDouble(Cur.BalOver90)+"'"
				+",insest    = '"      +POut.PDouble(Cur.InsEst)+"'"
				+",primaryteeth = '"   +POut.PString(Cur.PrimaryTeeth)+"'"
				+",BalTotal = '"       +POut.PDouble(Cur.BalTotal)+"'"
				+",EmployerNum = '"    +POut.PInt   (Cur.EmployerNum)+"'"
				+",EmploymentNote = '" +POut.PString(Cur.EmploymentNote)+"'"
				+" WHERE PatNum = '"   +POut.PInt   (Cur.PatNum)+"'";
			NonQ(false);
			//MessageBox.Show(cmd.CommandText);
		}//end UpdatePatient

		///<summary></summary>
		public static bool GetPtDataTable(bool limit,string lname,string fname,string hmphone,string address,bool hideInactive,string city,string state,string ssn,string patnum,string chartnumber,int[] billingtypes,bool guarOnly){
			//string psearchStr = POut.PString(searchStr)+"%";
			//Only used for the Select Patient dialog
			bool retval=false;
			string billingsnippet="";
			for(int i=0;i<billingtypes.Length;i++){
				if(i==0){
					billingsnippet+="AND (";
				}
				else{
					billingsnippet+="|| ";
				}
				billingsnippet+="BillingType ='"+billingtypes[i].ToString()+"' ";
				if(i==billingtypes.Length-1){//if there is only one row, this will also be triggered.
					billingsnippet+=") ";
				}
			}
			cmd.CommandText = 
				"SELECT PatNum,LName,FName,MiddleI,Preferred,Birthdate,SSN,HmPhone,Address,PatStatus,BillingType"
				+",ChartNumber,City,State "
				+"FROM patient "
				+"WHERE PatStatus != '4' "//not status 'deleted'
				+"AND LName LIKE '"      +POut.PString(lname)+"%' "
				+"AND FName LIKE '"      +POut.PString(fname)+"%' "
				+"AND HmPhone LIKE '"    +POut.PString(hmphone)+"%' "
				+"AND Address LIKE '"    +POut.PString(address)+"%' "
				+"AND City LIKE '"       +POut.PString(city)+"%' "
				+"AND State LIKE '"      +POut.PString(state)+"%' "
				+"AND PatNum LIKE '"     +POut.PString(patnum)+"%' "
				+"AND ChartNumber LIKE '"+POut.PString(chartnumber)+"%' "
				+billingsnippet;
			if(hideInactive){
				cmd.CommandText+="AND PatStatus = '0' ";
			}
			if(guarOnly){
				cmd.CommandText+="AND PatNum = Guarantor ";
			}
			cmd.CommandText+="ORDER BY LName,FName";
			if(limit)
				cmd.CommandText+=" LIMIT 36";//only need 35, but the extra will help indicate more rows
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			PtDataTable=table.Clone();
			//PtDataTable.Columns[0].DataType=typeof(int);//patnum
			for(int i=0;i<PtDataTable.Columns.Count;i++){
				PtDataTable.Columns[i].DataType=typeof(string);
			}
			if(limit && table.Rows.Count==36){
				retval=true;
			//	PtList=new Patient[44];
			}
			//else
				//PtList=new Patient[table.Rows.Count];
			DataRow r;
			for(int i=0;i<table.Rows.Count;i++){//table.Rows.Count && i<44;i++){
				r=PtDataTable.NewRow();
				r[0]=table.Rows[i][0].ToString();
				r[1]=table.Rows[i][1].ToString();
				r[2]=table.Rows[i][2].ToString();
				r[3]=table.Rows[i][3].ToString();
				r[4]=table.Rows[i][4].ToString();
				r[5]=Shared.DateToAge(PIn.PDate(table.Rows[i][5].ToString()));
				r[6]=table.Rows[i][6].ToString();
				r[7]=table.Rows[i][7].ToString();
				r[8]=table.Rows[i][8].ToString();
				r[9]=Lan.g("enumPatientStatus",
					((PatientStatus)PIn.PInt(table.Rows[i][9].ToString())).ToString());
				r[10]=Defs.GetName(DefCat.BillingTypes,PIn.PInt(table.Rows[i][10].ToString()));
				r[11]=table.Rows[i][11].ToString();
				r[12]=table.Rows[i][12].ToString();
				r[13]=table.Rows[i][13].ToString();
				PtDataTable.Rows.Add(r);

				/*
				PtList[i].PatNum     = PIn.PInt   (table.Rows[i][0].ToString());
				PtList[i].LName      = PIn.PString(table.Rows[i][1].ToString());
				PtList[i].FName      = PIn.PString(table.Rows[i][2].ToString());
				PtList[i].MiddleI    = PIn.PString(table.Rows[i][3].ToString());
				PtList[i].Preferred  = PIn.PString(table.Rows[i][4].ToString());
				PtList[i].Age        = Shared.DateToAge(PIn.PDate  (table.Rows[i][5].ToString()));
				PtList[i].SSN        = PIn.PString(table.Rows[i][6].ToString());
				PtList[i].HmPhone    = PIn.PString(table.Rows[i][7].ToString());
				PtList[i].Address    = PIn.PString(table.Rows[i][8].ToString());
				PtList[i].PatStatus  = (PatientStatus)PIn.PInt(table.Rows[i][9].ToString());
				PtList[i].BillingType= PIn.PInt(table.Rows[i][10].ToString());
				//chartnumber
				//city
				//state*/
			}
			return retval;//if true, there are more rows.
		}

		///<summary></summary>
		public static void GetRecallList(){
			cmd.CommandText = 
				"SELECT MAX(procedurelog.procdate) AS 'LastDate', "
				//+ INTERVAL patient.recallinterval MONTH AS 'DueDate', "
				+"CONCAT(patient.lname,', ',patient.fname,' ',patient.preferred,' ',"
				+"patient.middlei) AS 'Patient Name', "
				+"patient.birthdate,patient.recallinterval,patient.recallstatus,patient.patnum "
				//+"patient.nextaptnum,appointment.aptdatetime "
				+"FROM patient,procedurelog,procedurecode "
				//+"LEFT JOIN appointment ON appointment.nextaptnum = patient.nextaptnum "
				+"WHERE patient.patnum = procedurelog.patnum "
				+"AND procedurecode.adacode = procedurelog.adacode "
				+"AND procedurecode.setrecall = 1 "
				+"AND (procedurelog.procstatus = 2 "
				+"OR procedurelog.procstatus = 3 "
				+"OR procedurelog.procstatus = 4) "
				+"AND patient.patstatus = 0 "
				+"GROUP BY patient.patnum "
				+"ORDER BY LastDate";
				/* for future debugging:
SELECT MAX(procedurelog.procdate) + INTERVAL patient.recallinterval MONTH AS 'DueDate',
CONCAT(patient.lname,', ',patient.fname,' ',patient.preferred,' ',
patient.middlei) AS 'Patient Name',
patient.birthdate,patient.recallinterval,patient.recallstatus,patient.patnum,
patient.nextaptnum,appointment.aptdatetime
FROM patient,procedurelog,procedurecode
LEFT JOIN appointment ON appointment.nextaptnum = patient.nextaptnum
WHERE patient.patnum = procedurelog.patnum
AND procedurecode.adacode = procedurelog.adacode 
AND procedurecode.setrecall = 1
AND (procedurelog.procstatus = 2
OR procedurelog.procstatus = 3
OR procedurelog.procstatus = 4)
AND patient.patstatus = 0
GROUP BY patient.patnum
ORDER BY DueDate
			*/
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			RecallList = new RecallItem[table.Rows.Count];
			DateTime[] orderDate=new DateTime[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				RecallList[i].DueDate = (PIn.PDate  (table.Rows[i][0].ToString()))//last date
					.AddMonths(PIn.PInt(table.Rows[i][3].ToString()));//plus number of months
				RecallList[i].PatientName   = PIn.PString(table.Rows[i][1].ToString());
				RecallList[i].BirthDate     = PIn.PDate  (table.Rows[i][2].ToString());
				RecallList[i].RecallInterval= PIn.PInt   (table.Rows[i][3].ToString());
				RecallList[i].RecallStatus  = PIn.PInt   (table.Rows[i][4].ToString());
				RecallList[i].PatNum        = PIn.PInt   (table.Rows[i][5].ToString());
				RecallList[i].Age=Shared.DateToAge(RecallList[i].BirthDate);
				orderDate[i]=RecallList[i].DueDate;
			}
			Array.Sort(orderDate,RecallList);
		}

		///<summary></summary>
		public static string GetNameInFamLF(int myPatNum){
			string retStr="";
			for(int i=0;i<FamilyList.Length;i++){
				if(FamilyList[i].PatNum==myPatNum){
					if(FamilyList[i].Preferred==""){
						retStr=FamilyList[i].LName+", "+FamilyList[i].FName+" "+FamilyList[i].MiddleI; 
					}
					else{
						retStr=FamilyList[i].LName+", '"+FamilyList[i].Preferred+"' "+FamilyList[i].FName+" "+FamilyList[i].MiddleI;
					}
				}
			}
			return retStr;
		}

		///<summary></summary>
		public static string GetNameInFamLFI(int myi){
			string retStr="";
			if(FamilyList[myi].Preferred==""){
				retStr=FamilyList[myi].LName+", "+FamilyList[myi].FName+" "+FamilyList[myi].MiddleI; 
			}
			else{
				retStr=FamilyList[myi].LName+", '"+FamilyList[myi].Preferred+"' "+FamilyList[myi].FName+" "+FamilyList[myi].MiddleI;
			}
			return retStr;
		}

		///<summary></summary>
		public static string GetNameInFamFL(int myPatNum){
			string retStr="";
			for(int i=0;i<FamilyList.Length;i++){
				if(FamilyList[i].PatNum==myPatNum){
					if(FamilyList[i].Preferred==""){
						retStr=FamilyList[i].FName+" "+FamilyList[i].MiddleI+" "+FamilyList[i].LName; 
					}
					else{
						retStr="'"+FamilyList[i].Preferred+"' "+FamilyList[i].FName+" "+FamilyList[i].MiddleI+" "+FamilyList[i].LName;
					}
				}
			}
			return retStr;
		}

		///<summary></summary>
		public static string GetNameInFamFLI(int myi){
			string retStr="";
			if(FamilyList[myi].Preferred==""){
				retStr=FamilyList[myi].FName+" "+FamilyList[myi].MiddleI+" "+FamilyList[myi].LName; 
			}
			else{
				retStr="'"+FamilyList[myi].Preferred+"' "+FamilyList[myi].FName+" "+FamilyList[myi].MiddleI+" "+FamilyList[myi].LName;
			}
			return retStr;
		}

		/// <summary>Gets nine of the most useful fields from the db for the given patnums, saving the data
		/// in Lim and LimName.</summary>
		/// <param name="patNum">The PatNum to use in retrieving the data.</param>
		public static void GetLim(int patNum){
			if(patNum==0){
				Lim=new Patient();
				LimName="";
				return;
			}
			cmd.CommandText = 
				"SELECT PatNum,lname,fname,middlei,preferred,credittype,guarantor,priplannum,SSN " 
				+"FROM patient "
				+"WHERE PatNum = '"+patNum.ToString()+"'";
			FillTable();
			if(table.Rows.Count==0){
				Lim=new Patient();
				LimName="";
				return;
			}
			Lim.PatNum     = PIn.PInt   (table.Rows[0][0].ToString());
			Lim.LName      = PIn.PString(table.Rows[0][1].ToString());
			Lim.FName      = PIn.PString(table.Rows[0][2].ToString());
			Lim.MiddleI    = PIn.PString(table.Rows[0][3].ToString());
			Lim.Preferred  = PIn.PString(table.Rows[0][4].ToString());
			Lim.CreditType = PIn.PString(table.Rows[0][5].ToString());
			Lim.Guarantor  = PIn.PInt   (table.Rows[0][6].ToString());
			Lim.PriPlanNum = PIn.PInt   (table.Rows[0][7].ToString());
			Lim.SSN        = PIn.PString(table.Rows[0][8].ToString());
			if (Lim.Preferred=="")
				LimName=Lim.LName+", "+Lim.FName+" "+Lim.MiddleI;
			else LimName=Lim.LName+", '"+Lim.Preferred+"' "+Lim.FName+" "+Lim.MiddleI;
		}

		///<summary></summary>
		public static string GetCurNameLF(){
			if(Cur.Preferred=="")
				return Cur.LName+", "+Cur.FName+" "+Cur.MiddleI;
			else
				return Cur.LName+", '"+Cur.Preferred+"' "+Cur.FName+" "+Cur.MiddleI;
		}

		///<summary></summary>
		public static string GetLimCreditIns(){
			string retStr="";
			if(Lim.CreditType=="")
				retStr+=" ";
			else retStr+=Lim.CreditType;
			if(Lim.PriPlanNum==0)
				retStr+=" ";
			else retStr+="I";
			return retStr;
		}

		///<summary></summary>
		public static string GetCreditIns(){
			string retStr="";
			if(Cur.CreditType=="")
				retStr+=" ";
			else retStr+=Cur.CreditType;
			if(Cur.PriPlanNum==0)
				retStr+=" ";
			else retStr+="I";	
			return retStr;
		}

		///<summary></summary>
		public static int GetIndex(int patNum){
			int retVal=-1;//will return -1 if not found
			for(int i=0;i<FamilyList.Length;i++){
				if(FamilyList[i].PatNum==patNum){
					retVal=i;
				}
			}
			return retVal;
		}

		//public static void DeleteCur(){
		//this does not actually delete the patient, just changes status
		//and they will never show up on the selection list again
			
		//NonQ(false);
		//MessageBox.Show("Patient Deleted");
		//}

		///<summary></summary>
		public static void ChangeGuarantorToCur(){
			//Move famfinurgnote to current patient:
			cmd.CommandText = 
				"UPDATE patient SET "
				//+"famaddrnote = '"  +FamilyList[GuarIndex].FamAddrNote+"', "
				+"famfinurgnote = '"+FamilyList[GuarIndex].FamFinUrgNote+"' "
				+"WHERE patnum = '"+Cur.PatNum.ToString()+"'";
			NonQ(false);
			cmd.CommandText = 
				"UPDATE patient SET "
				//+"famaddrnote = '', "
				+"famfinurgnote = '' "
				+"WHERE patnum = '"+Cur.Guarantor.ToString()+"'";
			NonQ(false);
			//Move family financial note to current patient:
			cmd.CommandText="SELECT FamFinancial FROM patientnote "
				+"WHERE patnum = '"+Cur.Guarantor.ToString()+"'";
			FillTable();
			if(table.Rows.Count==1){
				cmd.CommandText = 
					"UPDATE patientnote SET "
					+"famfinancial = '"+table.Rows[0][0].ToString()+"' "
					+"WHERE patnum = '"+Cur.PatNum.ToString()+"'";
				NonQ(false);
			}
			cmd.CommandText = 
				"UPDATE patientnote SET "
				+"famfinancial = ''"
				+"WHERE patnum = '"+Cur.Guarantor.ToString()+"'";
			NonQ(false);
			//change guarantor of all family members:
			cmd.CommandText = 
				"UPDATE patient SET "
				+"guarantor = '"+Cur.PatNum.ToString()+"' "
				+"WHERE guarantor = '"+Cur.Guarantor.ToString()+"'";
			NonQ(false);
		}
		
		///<summary></summary>
		public static void CombineGuarantors(){
			//concat cur notes with guarantor notes
			cmd.CommandText = 
				"UPDATE patient SET "
				//+"addrnote = '"+POut.PString(FamilyList[GuarIndex].FamAddrNote)
				//									+POut.PString(Cur.FamAddrNote)+"', "
				+"famfinurgnote = '"+POut.PString(FamilyList[GuarIndex].FamFinUrgNote)
				+POut.PString(Cur.FamFinUrgNote)+"' "
				+"WHERE patnum = '"+Cur.Guarantor.ToString()+"'";
			NonQ(false);
			//delete cur notes
			cmd.CommandText = 
				"UPDATE patient SET "
				//+"famaddrnote = '', "
				+"famfinurgnote = '' "
				+"WHERE patnum = '"+Cur.PatNum+"'";
			NonQ(false);
			//concat family financial notes
			PatientNotes PatientNotes=new PatientNotes();
			PatientNotes.Refresh();
			//patientnote table must have been refreshed for this to work.
			//Makes sure there are entries for patient and for guarantor.
			//Also, PatientNotes.Cur.FamFinancial will now have the guar info in it.
			string strGuar=PatientNotes.Cur.FamFinancial;
			cmd.CommandText = 
				"SELECT famfinancial "
				+"FROM patientnote WHERE patnum ='"+POut.PInt(Patients.Cur.PatNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			string strCur=PIn.PString(table.Rows[0][0].ToString());
			cmd.CommandText = 
				"UPDATE patientnote SET "
				+"famfinancial = '"+strGuar+strCur+"' "
				+"WHERE patnum = '"+Cur.Guarantor.ToString()+"'";
			NonQ(false);
			//delete cur financial notes
			cmd.CommandText = 
				"UPDATE patientnote SET "
				+"famfinancial = ''"
				+"WHERE patnum = '"+Cur.PatNum.ToString()+"'";
			NonQ(false);
		}

		///<summary>Gets names for all patients.</summary>
		public static void GetHList(){
			cmd.CommandText="SELECT patnum,lname,fname,middlei,preferred "
				+"FROM patient";
			FillTable();
			HList=new Hashtable(table.Rows.Count);
			int patnum;
			string lname,fname,middlei,preferred;
			for(int i=0;i<table.Rows.Count;i++){
				patnum=PIn.PInt(table.Rows[i][0].ToString());
				lname=PIn.PString(table.Rows[i][1].ToString());
				fname=PIn.PString(table.Rows[i][2].ToString());
				middlei=PIn.PString(table.Rows[i][3].ToString());
				preferred=PIn.PString(table.Rows[i][4].ToString());
				if(preferred==""){
					HList.Add(patnum,lname+", "+fname+" "+middlei);
				}
				else{
					HList.Add(patnum,lname+", '"+preferred+"' "+fname+" "+middlei);
				}
			}
		}

		///<summary></summary>
		public static void UpdateAddressForFam(){
			cmd.CommandText = "UPDATE patient SET " 
				+"Address = '"    +POut.PString(Cur.Address)+"'"
				+",Address2 = '"   +POut.PString(Cur.Address2)+"'"
				+",City = '"       +POut.PString(Cur.City)+"'"
				+",State = '"      +POut.PString(Cur.State)+"'"
				+",Zip = '"        +POut.PString(Cur.Zip)+"'"
				+",HmPhone = '"    +POut.PString(Cur.HmPhone)+"'"
				+",credittype = '" +POut.PString(Cur.CreditType)+"'"
				+",priprov = '"    +POut.PInt   (Cur.PriProv)+"'"
				+",secprov = '"    +POut.PInt   (Cur.SecProv)+"'"
				+",feesched = '"   +POut.PInt   (Cur.FeeSched)+"'"
				+",billingtype = '"+POut.PInt   (Cur.BillingType)+"'"
				+" WHERE guarantor = '"+POut.PDouble(Cur.Guarantor)+"'";
			NonQ(false);
			//MessageBox.Show(cmd.CommandText);
		}

		///<summary></summary>
		public static void UpdateNotesForFam(){
			cmd.CommandText = "UPDATE patient SET " 
				+"addrnote = '"   +POut.PString(Cur.AddrNote)+"'"
				+" WHERE guarantor = '"+POut.PDouble(Cur.Guarantor)+"'";
			NonQ(false);
			//MessageBox.Show(cmd.CommandText);
		}

		///<summary>This is only used in the Billing dialog</summary>
		public static void GetAgingList(string age,int[] billingIndices,bool excludeAddr
			,bool excludeNeg,double excludeLessThan,bool excludeInactive){
			cmd.CommandText=
				"SELECT patnum,Bal_0_30,Bal_31_60,Bal_61_90,BalOver90,BalTotal,InsEst,LName,FName,MiddleI "
				+"FROM patient WHERE ";//actually only gets guarantors since others are 0.
			if(excludeInactive){
				cmd.CommandText+="(patstatus != '2') && ";
			}
			cmd.CommandText+="(BalTotal - InsEst > '"+excludeLessThan.ToString()+"'";
			if(!excludeNeg){
				cmd.CommandText+=" || BalTotal - InsEst < '0')";
			}
			else{
				cmd.CommandText+=")";
			}
			switch(age){
				//where is age 0. Is it missing because no restriction
				case "30":
					cmd.CommandText+=" && (Bal_31_60 > '0' || Bal_61_90 > '0' || BalOver90 > '0')";
					break;
				case "60":
					cmd.CommandText+=" && (Bal_61_90 > '0' || BalOver90 > '0')";
					break;
				case "90":
					cmd.CommandText+=" && (BalOver90 > '0')";
					break;
			}
			for(int i=0;i<billingIndices.Length;i++){
				if(i==0){
					cmd.CommandText+=" && (billingtype = '";
				}
				else{
					cmd.CommandText+=" || billingtype = '";
				}
				cmd.CommandText+=
					Defs.Short[(int)DefCat.BillingTypes][billingIndices[i]].DefNum.ToString()+"'";
			}
			cmd.CommandText+=")";
			if(excludeAddr){
				cmd.CommandText+=" && (zip !='')";
			}	
			cmd.CommandText+=" ORDER BY LName,FName";
			FillTable();
			AgingList=new PatAging[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				AgingList[i].PatNum   = PIn.PInt   (table.Rows[i][0].ToString());
				AgingList[i].Bal_0_30 = PIn.PDouble(table.Rows[i][1].ToString());
				AgingList[i].Bal_31_60= PIn.PDouble(table.Rows[i][2].ToString());
				AgingList[i].Bal_61_90= PIn.PDouble(table.Rows[i][3].ToString());
				AgingList[i].BalOver90= PIn.PDouble(table.Rows[i][4].ToString());
				AgingList[i].BalTotal = PIn.PDouble(table.Rows[i][5].ToString());
				AgingList[i].InsEst   = PIn.PDouble(table.Rows[i][6].ToString());
				AgingList[i].PatName=PIn.PString(table.Rows[i][7].ToString())
					+", "+PIn.PString(table.Rows[i][8].ToString())
					+" "+PIn.PString(table.Rows[i][9].ToString());;
				//AgingList[i].Balance=AgingList[i].Bal_0_30+AgingList[i].Bal_31_60
				//	+AgingList[i].Bal_61_90+AgingList[i].BalOver90;
				AgingList[i].AmountDue=AgingList[i].BalTotal-AgingList[i].InsEst;
			}
		}

		///<summary></summary>
		public static void GetAgingList(){
			//used only to run finance charges, so it ignores negative balances
			cmd.CommandText =
				"SELECT patnum,Bal_0_30,Bal_31_60,Bal_61_90,BalOver90,BalTotal,InsEst,LName,FName,MiddleI,priprov "
				+"FROM patient "//actually only gets guarantors since others are 0.
				+" WHERE Bal_0_30 + Bal_31_60 + Bal_61_90 + BalOver90 - InsEst > '0.005'"//more that 1/2 cent
				+" ORDER BY LName,FName";
			FillTable();
			AgingList=new PatAging[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				AgingList[i].PatNum   = PIn.PInt   (table.Rows[i][0].ToString());
				AgingList[i].Bal_0_30 = PIn.PDouble(table.Rows[i][1].ToString());
				AgingList[i].Bal_31_60= PIn.PDouble(table.Rows[i][2].ToString());
				AgingList[i].Bal_61_90= PIn.PDouble(table.Rows[i][3].ToString());
				AgingList[i].BalOver90= PIn.PDouble(table.Rows[i][4].ToString());
				AgingList[i].BalTotal = PIn.PDouble(table.Rows[i][5].ToString());
				AgingList[i].InsEst   = PIn.PDouble(table.Rows[i][6].ToString());
				AgingList[i].PatName=PIn.PString(table.Rows[i][7].ToString())
					+", "+PIn.PString(table.Rows[i][8].ToString())
					+" "+PIn.PString(table.Rows[i][9].ToString());;
				//AgingList[i].Balance=AgingList[i].Bal_0_30+AgingList[i].Bal_31_60
				//	+AgingList[i].Bal_61_90+AgingList[i].BalOver90;
				AgingList[i].AmountDue=AgingList[i].BalTotal-AgingList[i].InsEst;
				AgingList[i].PriProv=PIn.PInt(table.Rows[i][10].ToString());
			}
		}

		///<summary></summary>
		public static void ResetAging(){//for entire database
			//need to zero everything out first because the update aging only inserts non-zero values
			cmd.CommandText="Update patient SET "
				+"Bal_0_30   = '0'"
				+",Bal_31_60 = '0'"
				+",Bal_61_90 = '0'"
				+",BalOver90 = '0'"
				+",InsEst    = '0'"
				+",BalTotal  = '0'";
			NonQ(false);		
		}

		///<summary></summary>
		public static void ResetAging(int guarantor){
			cmd.CommandText="Update patient SET "
				+"Bal_0_30   = '0'"
				+",Bal_31_60 = '0'"
				+",Bal_61_90 = '0'"
				+",BalOver90 = '0'"
				+",InsEst    = '0'"
				+",BalTotal  = '0'"
			  +" WHERE guarantor = '"+POut.PInt(guarantor)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void UpdateAging(int patnum,double Bal0,double Bal31
			,double Bal61,double Bal91,double InsEst,double BalTotal){
			cmd.CommandText="Update patient SET "
				+"Bal_0_30        = '" +POut.PDouble(Bal0)+"'"
				+",Bal_31_60      = '" +POut.PDouble(Bal31)+"'"
				+",Bal_61_90      = '" +POut.PDouble(Bal61)+"'"
				+",BalOver90      = '" +POut.PDouble(Bal91)+"'"
				+",InsEst         = '" +POut.PDouble(InsEst)+"'"
				+",BalTotal       = '" +POut.PDouble(BalTotal)+"'"
				+" WHERE patnum   = '" +POut.PInt   (patnum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static int GetProvForCur(){
			if(Cur.PriProv!=0)
				return Cur.PriProv;
			if(PIn.PInt(((Pref)Prefs.HList["PracticeDefaultProv"]).ValueString)==0){
				MessageBox.Show(Lan.g("Patients","Please set a default provider in the practice setup window."));
				return Providers.List[0].ProvNum;
			}
			return PIn.PInt(((Pref)Prefs.HList["PracticeDefaultProv"]).ValueString);
		}

		///<summary>Gets the next available integer chart number.  Willl later add a where clause based on preferred format.</summary>
		public static string GetNextChartNum(){
			cmd.CommandText="SELECT ChartNumber from patient WHERE"
				+" ChartNumber REGEXP '^[0-9]+$'"//matches any number of digits
				+" ORDER BY chartnumber DESC LIMIT 1";
			FillTable();
			if(table.Rows.Count==0){//no existing chart numbers
				return "1";
			}
			string lastChartNum=PIn.PString(table.Rows[0][0].ToString());
			//or could add more match conditions
			//if(Regex.IsMatch(lastChartNum,@"^\d+$")){//if is an integer
			return(PIn.PInt(lastChartNum)+1).ToString();
			//}
			//return "1";//if there are no integer chartnumbers yet
		}

		///<summary></summary>
		public static bool ChartNumIsUnique(string chartNum,int excludePatNum){
			cmd.CommandText="SELECT ChartNumber from patient WHERE "
				+"ChartNumber = '"+chartNum
				+"' && PatNum != '"+excludePatNum.ToString()+"'";
			FillTable();
			if(table.Rows.Count==0){//no duplicate chart numbers
				return true;
			}
			else return false;
		}

		///<summary>Used in the patient select window to determine if a trial version user is over their limit.</summary>
		public static int GetNumberPatients(){
			cmd.CommandText="SELECT Count(*) FROM patient";
			FillTable();
			return PIn.PInt(table.Rows[0][0].ToString());
		}

	}

	

	///<summary>Not a database table.  Just used for running reports.</summary>
	public struct PatAging{
		///<summary></summary>
		public int PatNum;
		///<summary></summary>
		public double Bal_0_30;
		///<summary></summary>
		public double Bal_31_60;
		///<summary></summary>
		public double Bal_61_90;
		///<summary></summary>
		public double BalOver90;
		///<summary></summary>
		public double InsEst;
		///<summary></summary>
		public string PatName;
		///<summary></summary>
		public double BalTotal;
		///<summary></summary>
		public double AmountDue;
		///<summary>The patient priprov to assign the finance charge to.</summary>
		public int PriProv;
	}

	


}










