using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Resources;
using System.Text; 
using System.Windows.Forms;

namespace OpenDental{

	///<summary></summary>
	public class ClassConvertDatabase{
		private System.Version FromVersion;
		private System.Version ToVersion;

		///<summary>Return false to indicate exit app.  Only called when program first starts up at the beginning of FormOpenDental.RefreshLocalData.</summary>
		public bool Convert(string fromVersion){
			FromVersion=new Version(fromVersion);
			ToVersion=new Version(Application.ProductVersion);
			if(FromVersion>=new Version("3.4.0") && Prefs.GetBool("CorruptedDatabase")){
				MsgBox.Show(this,"Your database is corrupted because a conversion failed.  Please contact us.  This database is unusable and you will need to restore from a backup.");
				return false;//shuts program down.
			}
			if(FromVersion.CompareTo(ToVersion)>0){//"Cannot convert database to an older version."
				//no longer necessary to catch it here.  It will be handled soon enough in CheckProgramVersion
				return true;
			}
			if(FromVersion < new Version("2.1.2")){
				MsgBox.Show(this,"This database is too old to easily convert in one step. Please uninstall this version and install version 2.1 first. Run the program, and after converting to version 2.1, then install this version. We apologize for the inconvenience.");
				return false;
			}
			if(//FromVersion < new Version("1.0.0")
				//|| FromVersion.ToString()=="2.0.1.0"
				//|| FromVersion.ToString()=="2.1.0.0"
				//|| FromVersion.ToString()=="2.1.1.0"
				FromVersion.ToString()=="2.5.0.0"
				|| FromVersion.ToString()=="2.9.0.0"
				|| FromVersion.ToString()=="3.0.0.0")
			{
				MsgBox.Show(this,"Cannot convert this database version which was only for development purposes.");
				return false;
			}
			if(FromVersion < new Version("3.4.30.0")){
				if(MessageBox.Show(Lan.g(this,"Your database will now be converted")+"\r"
					+Lan.g(this,"from version")+" "+FromVersion.ToString()+"\r"
					+Lan.g(this,"to version")+" "+ToVersion.ToString()+"\r"
					+Lan.g(this,"The conversion works best if you are on the server.  Depending on the speed of your computer, it can be as fast as a few seconds, or it can take as long as 10 minutes.")
					,"",MessageBoxButtons.OKCancel)
					!=DialogResult.OK)
				{
					return false;//close the program
				}
			}
			else{
				return true;//no conversion necessary
			}
			#if !DEBUG
				try{
					MakeABackup();
				}
				catch(Exception e){
					if(e.Message!=""){
						MessageBox.Show(e.Message);
					}
					MsgBox.Show(this,"Backup failed. Your database has not been altered.");
					return false;//but this should never happen
				}
			#endif
			try{
				if(FromVersion>=new Version("3.4.0")){
					Prefs.UpdateBool("CorruptedDatabase",true);
				}
				To2_1_5();//begins going through the chain of conversion steps
				MsgBox.Show(this,"Conversion successful");
				if(FromVersion>=new Version("3.4.0")){
					Prefs.Refresh();//or it won't know it has to update in the next line.
					Prefs.UpdateBool("CorruptedDatabase",false);
				}
				Prefs.Refresh();
				return true;
			}
			catch(System.IO.FileNotFoundException e){
				MessageBox.Show(e.FileName+" "+Lan.g(this,"could not be found. Your database has not been altered and is still usable if you uninstall this version, then reinstall the previous version."));
				if(FromVersion>=new Version("3.4.0")){
					Prefs.UpdateBool("CorruptedDatabase",false);
				}
				//Prefs.Refresh();
				return false;
			}
			catch(System.IO.DirectoryNotFoundException){
				MessageBox.Show(Lan.g(this,"ConversionFiles folder could not be found. Your database has not been altered and is still usable if you uninstall this version, then reinstall the previous version."));
				if(FromVersion>=new Version("3.4.0")){
					Prefs.UpdateBool("CorruptedDatabase",false);
				}
				//Prefs.Refresh();
				return false;
			}
			catch{
			//	MessageBox.Show(e.Message);
				MsgBox.Show(this,"Conversion unsuccessful. Your database is now corrupted and you cannot use it.  Please contact us.");
				//Then, application will exit, and database will remain tagged as corrupted.
				return false;
			}
		}

		///<summary>Used in MakeABackup to ensure a unique backup database name.</summary>
		private bool Contains(string[] arrayToSearch,string valueToTest){
			string compare;
			for(int i=0;i<arrayToSearch.Length;i++){
				compare=arrayToSearch[i];
				if(arrayToSearch[i]==valueToTest){
					return true;
				}
			}
			return false;
		}

		///<summary>Backs up the database to the same directory as the original just in case the user did not have sense enough to do a backup first.</summary>
		public void MakeABackup(){
			string oldDb=FormConfig.Database;
			string newDb=oldDb+"backup_"+DateTime.Today.ToString("MM_dd_yyyy");
			DataConnection dcon=new DataConnection();
			string command="SHOW DATABASES";
			DataTable table=dcon.GetTable(command);
			string[] databases=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				databases[i]=table.Rows[i][0].ToString();
			}
			if(Contains(databases,newDb)){//if the new database name already exists
				//find a unique one
				int uniqueID=1;
				string originalNewDb=newDb;
				do{
					newDb=originalNewDb+"_"+uniqueID.ToString();
					uniqueID++;
				}
				while(Contains(databases,newDb));
			}
			command="CREATE DATABASE "+newDb;
			dcon.NonQ(command);
			command="SHOW TABLES";
			table=dcon.GetTable(command);
			string[] tableName=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				tableName[i]=table.Rows[i][0].ToString();
			}
			//switch to using the new database
			dcon=new DataConnection(newDb);
			for(int i=0;i<tableName.Length;i++){
				command="SHOW CREATE TABLE "+oldDb+"."+tableName[i];
				table=dcon.GetTable(command);
				command=table.Rows[0][1].ToString();
				dcon.NonQ(command);
				command="INSERT INTO "+newDb+"."+tableName[i]
					+" SELECT * FROM "+oldDb+"."+tableName[i];
				dcon.NonQ(command);
			}
		}

		/// <summary>Takes a text file with a series of SQL commands, and sends them as queries to the database.  Used in version upgrades and in the function that downloads and installs the latest translations.  The filename is always relative to the application directory.  Throws an exception if it fails.</summary>
		public void ExecuteFile(string fileName){
			//also used in the function that downloads and installs the latests translations.
			//if(!File.Exists(fileName)){
			//	throw new Exception(fileName+" could not be found.");
			//}
			StreamReader sr;
			string line="";
			string cmd="";
			ArrayList AL=new ArrayList();
			sr=new StreamReader(fileName);//throws a FileNotFoundException if not found. We handle that.
			while(true){
				line=sr.ReadLine();
				if(line==null){
					break;
				}
				if(line.Length==0 || line.Substring(0,1)=="#"){//could improve white space handling
					//continue;
				}
				else{
					cmd+=" "+line;
					if(cmd.Substring(cmd.Length-1)==";"){//again, need to handle white space better
						AL.Add(cmd);
						cmd="";
					}
				}
			}
			Conversions.NonQArray=new string[AL.Count];
			AL.CopyTo(Conversions.NonQArray);
				Conversions.SubmitNonQArray();
			sr.Close();
		}

		private void To2_1_5(){
			if(FromVersion < new Version("2.1.5")){
				Conversions.NonQArray=new string[]{
					"UPDATE claimformitem SET formatstring = 'MM/dd/yy' WHERE "
						+"claimformitemnum = '280' || claimformitemnum = '286' || claimformitemnum = '292' "
						+"|| claimformitemnum = '298' || claimformitemnum = '304' || claimformitemnum = '310' "
						+"|| claimformitemnum = '316' || claimformitemnum = '322' || claimformitemnum = '328' "
						+"|| claimformitemnum = '334'"
					,"ALTER TABLE procedurecode CHANGE ADACode ADACode VARCHAR(15) BINARY NOT NULL default ''"
					,"UPDATE preference SET ValueString = '2.1.5' WHERE PrefName = 'DataBaseVersion'"
				};
				Conversions.SubmitNonQArray();
			}
			To2_5_1();
		}

		private void To2_5_1(){
			if(FromVersion < new Version("2.5.1")){
				//these might generate a FileNotFoundException which we handle.
				File.Copy(@"ConversionFiles\DentiCal.jpg"
					,((Pref)Prefs.HList["DocPath"]).ValueString+@"\DentiCal.jpg",true);
				File.Copy(@"ConversionFiles\ADA2000.jpg"
					,((Pref)Prefs.HList["DocPath"]).ValueString+@"\ADA2000.jpg",true);
				File.Copy(@"ConversionFiles\HCFA1500.gif"
					,((Pref)Prefs.HList["DocPath"]).ValueString+@"\HCFA1500.gif",true);
				//set the aptstatus of all the next appointments to Next.
				//and update the dates on all the next appointments
				Conversions.SelectText="SELECT patient.nextaptnum,procedurelog.procdate "
					+"FROM patient "
					+"LEFT JOIN procedurelog ON patient.nextaptnum = procedurelog.nextaptnum "
					+"WHERE patient.nextaptnum > '0' ";
				Conversions.SubmitSelect();
				for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
					Conversions.NonQString=
						"UPDATE appointment SET aptstatus = '6',"
						+"aptdatetime = '"+POut.PDateT(PIn.PDate(Conversions.TableQ.Rows[i][1].ToString()))+"'"
						+" WHERE aptnum = '"+Conversions.TableQ.Rows[i][0].ToString()+"'";
					//MessageBox.Show(Conversions.ArrayQueryText[0]);
					Conversions.SubmitNonQString();
				}
				//add various columns, rows, and tables in a script
				//this also adds all new claimform items with dummy foreign keys which will get replaced.
				ExecuteFile(@"ConversionFiles\convert_2_5_1.txt");
				//get the primary keys for the 3(4) claimforms
				Conversions.SelectText="SELECT ClaimFormNum,UniqueID FROM claimform "
					+"WHERE UniqueID > '0'";
				Conversions.SubmitSelect();
				//use those primary keys to delete appropriate claimformitems and change foreign keys
				for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
					Conversions.NonQString="DELETE FROM claimformitem"
						+" WHERE ClaimFormNum = '"+Conversions.TableQ.Rows[i][0].ToString()+"'";
					Conversions.SubmitNonQString();
					Conversions.NonQString="UPDATE claimformitem"
						+" SET ClaimFormNum = '"+Conversions.TableQ.Rows[i][0].ToString()
						+"' WHERE ClaimFormNum = '1000"+Conversions.TableQ.Rows[i][1].ToString()+"'";
					Conversions.SubmitNonQString();
				}
				//Move all patientnote.ApptPhone entries into CommLog
				Conversions.SelectText="SELECT patnum,apptphone "
					+"FROM patientnote "
					+"WHERE apptphone != ''";
				Conversions.SubmitSelect();
				for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
					Conversions.NonQArray=new string[]{
						"INSERT INTO commlog (patnum"
						+",commdate,commtype,note) VALUES("
						+"'"+POut.PInt   (PIn.PInt(Conversions.TableQ.Rows[i][0].ToString()))+"', "
						+"'"+POut.PDate  (DateTime.Today)+"', "
						+"'"+POut.PInt   (2)+"', "//2=appointment scheduling
						+"'"+POut.PString(PIn.PString(Conversions.TableQ.Rows[i][1].ToString()))+"')"
					};
					//MessageBox.Show(Conversions.ArrayQueryText[0]);
					Conversions.SubmitNonQArray();
				}
				//Add a procDescript to each appointment
				Conversions.SelectText="SELECT procedurelog.aptnum,procedurecode.abbrdesc "
					+"FROM procedurelog,procedurecode "
					+"WHERE procedurelog.AptNum > '0' AND procedurelog.adacode = procedurecode.adacode "
					+"ORDER BY aptnum";
				Conversions.SubmitSelect();
				string procs="";
				int curAptNum=0;
				int j=0;
				while(j<Conversions.TableQ.Rows.Count){
					curAptNum=PIn.PInt(Conversions.TableQ.Rows[j][0].ToString());
					procs="";
					while(j<Conversions.TableQ.Rows.Count &&
						curAptNum==PIn.PInt(Conversions.TableQ.Rows[j][0].ToString()))
					{
						procs+=PIn.PString(Conversions.TableQ.Rows[j][1].ToString())+", ";
						j++;
					}
					procs=procs.Substring(0,procs.Length-2);//trim the last comma and space
					Conversions.NonQArray=new string[]{
						"UPDATE appointment SET procdescript = '"+POut.PString(procs)+"' "
						+" WHERE aptnum = '"+curAptNum.ToString()+"'"
					};
					//MessageBox.Show(Conversions.ArrayQueryText[0]);
					Conversions.SubmitNonQArray();
				}
				//Add a procDescript to each next appointment
				Conversions.SelectText="SELECT procedurelog.nextaptnum,procedurecode.abbrdesc "
					+"FROM procedurelog,procedurecode "
					+"WHERE procedurelog.NextAptNum > '0' AND procedurelog.adacode = procedurecode.adacode "
					+"ORDER BY nextaptnum";
				Conversions.SubmitSelect();
				j=0;
				while(j<Conversions.TableQ.Rows.Count){
					curAptNum=PIn.PInt(Conversions.TableQ.Rows[j][0].ToString());
					procs="";
					while(j<Conversions.TableQ.Rows.Count &&
						curAptNum==PIn.PInt(Conversions.TableQ.Rows[j][0].ToString()))
					{
						procs+=PIn.PString(Conversions.TableQ.Rows[j][1].ToString())+", ";
						j++;
					}
					procs=procs.Substring(0,procs.Length-2);//trim the last comma and space
					Conversions.NonQArray=new string[]{
						"UPDATE appointment SET procdescript = '"+procs+"' "
						+" WHERE aptnum = '"+curAptNum.ToString()+"'"
					};
					//MessageBox.Show(Conversions.ArrayQueryText[0]);
					Conversions.SubmitNonQArray();
				}
				//final changes
				Conversions.NonQArray=new string[]{
					"UPDATE patientnote SET ApptPhone = ''"
					,"UPDATE preference SET ValueString = '2.5.1' WHERE PrefName = 'DataBaseVersion'"
				};
				Conversions.SubmitNonQArray();
			}
			To2_5_2();
		}

		private void To2_5_2(){
			if(FromVersion < new Version("2.5.2.0")){
				Conversions.NonQArray=new string[]{
					"ALTER TABLE claimform CHANGE OffsetX OffsetX SMALLINT(5) NOT NULL default '0'"
					,"ALTER TABLE claimform CHANGE OffsetY OffsetY SMALLINT(5) NOT NULL default '0'"
					,"INSERT INTO preference (PrefName,ValueString) VALUES ('BillingExcludeInactive','0')"
					,"UPDATE preference SET ValueString = '2.5.2.0' WHERE PrefName = 'DataBaseVersion'"
				};
				Conversions.SubmitNonQArray();
			}
			To2_5_6();
		}

		private void To2_5_6(){
			if(FromVersion < new Version("2.5.6.0")){
				Conversions.NonQArray=new string[]{
					"INSERT INTO preference (PrefName,ValueString) VALUES ('GenericEClaimsForm','')"
					,"UPDATE preference SET ValueString = '2.5.6.0' WHERE PrefName = 'DataBaseVersion'"
				};
				Conversions.SubmitNonQArray();
			}
			To2_5_7();
		}

		private void To2_5_7(){
			if(FromVersion < new Version("2.5.7.0")){
				//copy the new ADA2002.gif
				//try{
					File.Copy(@"ConversionFiles\ADA2002.gif"
						,((Pref)Prefs.HList["DocPath"]).ValueString+@"\ADA2002.gif",true);
				//}
				//catch{
				//	throw new Exception("ADA2002.gif could not be copied correctly.");
				//}
				//delete the old ADA2002.emf, and the old ADA2002.jpg if there is one
				if(File.Exists(((Pref)Prefs.HList["DocPath"]).ValueString+@"\ADA2002.emf")){
					File.Delete(((Pref)Prefs.HList["DocPath"]).ValueString+@"\ADA2002.emf");
				}
				if(File.Exists(((Pref)Prefs.HList["DocPath"]).ValueString+@"\ADA2002.jpg")){
					File.Delete(((Pref)Prefs.HList["DocPath"]).ValueString+@"\ADA2002.jpg");
				}
				//Get the claimformNum of the ADA2002
				ClaimFormItems.Refresh();//I added this line much later to prevent a crash in the next line.
				ClaimForms.Refresh();
				ClaimForm ClaimFormCur=new ClaimForm();
				for(int i=0;i<ClaimForms.ListLong.Length;i++){
					if(ClaimForms.ListLong[i].UniqueID=="1"){
						ClaimFormCur=ClaimForms.ListLong[i];
						break;
					}
				}
				//get the claimformitem for the background image
				ClaimFormItems.Refresh();
				//ClaimFormItems.GetListForForm(ClaimFormCur.ClaimFormNum);
				ClaimFormItem CFIcur=new ClaimFormItem();
				for(int i=0;i<ClaimFormCur.Items.Length;i++){
					if(ClaimFormCur.Items[i].ImageFileName=="ADA2002.emf"
						|| ClaimFormCur.Items[i].ImageFileName=="ADA2002.gif"
						|| ClaimFormCur.Items[i].ImageFileName=="ADA2002.jpg"){
						CFIcur=ClaimFormCur.Items[i];
					}
				}
				//if a match was not found, then it will start from scratch
				//Change the background to the new gif image
				CFIcur.ClaimFormNum=ClaimFormCur.ClaimFormNum;
				CFIcur.ImageFileName="ADA2002.gif";
				CFIcur.XPos=9;
				CFIcur.YPos=0;
				CFIcur.Width=834;
				CFIcur.Height=1058;
				//save the changes
				if(CFIcur.ClaimFormItemNum==0){
					CFIcur.Insert();
				}
				else{
					CFIcur.Update();
				}
				//final:
				Conversions.NonQArray=new string[]{
					"UPDATE preference SET ValueString = '2.5.7.0' WHERE PrefName = 'DataBaseVersion'"
				};
				Conversions.SubmitNonQArray();
			}
			To2_8_0();
		}

		private void To2_8_0(){
			if(FromVersion < new Version("2.8.0.0")){
				//warn user about deleting templates
				if(MessageBox.Show(@"In version 2.8, the concept of insurance templates is being phased out.  As part of the conversion process, your existing insurance template list will be replaced with an insurance plan list and a carrier list.  If you have any notes in your insurance templates, they will be deleted.  If you have important notes in any of your insurance templates, or if you have important templates that you don't want to lose, then you should use the print screen tool to print out the important information before proceeding.  Do you wish to proceed?","",MessageBoxButtons.OKCancel)!=DialogResult.OK)
				{
					throw new Exception();
				}
				//check to see if the conversion file is available
				//if(!File.Exists(@"ConversionFiles\convert_2_8_0.txt")){
				//	throw new Exception(@"ConversionFiles\convert_2_8_0.txt"+" could not be found.");
				//}
				ExecuteFile(@"ConversionFiles\convert_2_8_0.txt");//might throw an exception
				//load all existing employer names into the new Employer table:
				Conversions.SelectText="SELECT DISTINCT Employer FROM insplan WHERE Employer !=''";
				Conversions.SubmitSelect();
				for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
					Conversions.NonQString="INSERT INTO employer(EmpName) VALUES('"
						+POut.PString(PIn.PString(Conversions.TableQ.Rows[i][0].ToString()))+"')";
					Conversions.SubmitNonQString();
				}
				//now, get the employers into HEmpNames so we can retrieve the empnum from the name
				Hashtable HEmpNames=new Hashtable();
				Conversions.SelectText="SELECT EmpName,EmployerNum FROM employer";
				Conversions.SubmitSelect();
				for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
					HEmpNames.Add(PIn.PString(Conversions.TableQ.Rows[i][0].ToString()),//name
						PIn.PInt(Conversions.TableQ.Rows[i][1].ToString()));//num
				}
				//replace Employer with EmployerNum in insplan and add to patient
				Conversions.SelectText="SELECT PlanNum,Subscriber,Employer FROM insplan WHERE Employer !=''";
				Conversions.SubmitSelect();
				for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
					try{
						Conversions.NonQString="UPDATE insplan SET EmployerNum = '"
							+((int)HEmpNames[PIn.PString(Conversions.TableQ.Rows[i][2].ToString())]).ToString()
							+"' WHERE PlanNum = '"+Conversions.TableQ.Rows[i][0].ToString()+"'";
						Conversions.SubmitNonQString();
						Conversions.NonQString="UPDATE patient SET EmployerNum = '"
							+((int)HEmpNames[PIn.PString(Conversions.TableQ.Rows[i][2].ToString())]).ToString()
							+"' WHERE PatNum = '"+Conversions.TableQ.Rows[i][1].ToString()+"'";
						Conversions.SubmitNonQString();
					}
					catch{
						//will sometimes fail due to capitalization, but it's not that important
					}
				}
				//delete all existing insplan.Employer
				Conversions.NonQString="UPDATE insplan SET Employer = ''";
				Conversions.SubmitNonQString();
				//reformat phone numbers in preparation for extracting carrier info
				if(CultureInfo.CurrentCulture.Name=="en-US"){
					FormTelephone.Reformat();
					Carriers.Refresh();
				}
				//load all carrier info from insplans:
				Conversions.SelectText="SELECT DISTINCT Carrier,Phone,Address,Address2,City,State,Zip"
					+",NoSendElect,ElectID"
					+" FROM insplan WHERE Carrier !=''";
				Conversions.SubmitSelect();
				for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
					Carriers.Cur=new Carrier();
					Carriers.Cur.CarrierName=PIn.PString(Conversions.TableQ.Rows[i][0].ToString());
					Carriers.Cur.Phone      =PIn.PString(Conversions.TableQ.Rows[i][1].ToString());
					Carriers.Cur.Address    =PIn.PString(Conversions.TableQ.Rows[i][2].ToString());
					Carriers.Cur.Address2   =PIn.PString(Conversions.TableQ.Rows[i][3].ToString());
					Carriers.Cur.City       =PIn.PString(Conversions.TableQ.Rows[i][4].ToString());
					Carriers.Cur.State      =PIn.PString(Conversions.TableQ.Rows[i][5].ToString());
					Carriers.Cur.Zip        =PIn.PString(Conversions.TableQ.Rows[i][6].ToString());
					Carriers.Cur.NoSendElect=PIn.PBool  (Conversions.TableQ.Rows[i][7].ToString());
					Carriers.Cur.ElectID    =PIn.PString(Conversions.TableQ.Rows[i][8].ToString());
					Carriers.InsertCur();
				}
				Carriers.Refresh();
				//loop through all Carriers and update CarrierNum in insplan
				for(int i=0;i<Carriers.List.Length;i++){
					Conversions.NonQString="UPDATE insplan SET "
						+"CarrierNum = '"+POut.PInt(Carriers.List[i].CarrierNum)+"' "
						+"WHERE "
						+"Carrier = '"   +POut.PString(Carriers.List[i].CarrierName)+"' "
						+"&& Phone = '"      +POut.PString(Carriers.List[i].Phone)+"' "
						+"&& Address = '"    +POut.PString(Carriers.List[i].Address)+"' "
						+"&& Address2 = '"   +POut.PString(Carriers.List[i].Address2)+"' "
						+"&& City = '"       +POut.PString(Carriers.List[i].City)+"' "
						+"&& State = '"      +POut.PString(Carriers.List[i].State)+"' "
						+"&& Zip = '"        +POut.PString(Carriers.List[i].Zip)+"' "
						+"&& NoSendElect = '"+POut.PBool  (Carriers.List[i].NoSendElect)+"' "
						+"&& ElectID = '"    +POut.PString(Carriers.List[i].ElectID)+"'";
					Conversions.SubmitNonQString();
				}
				//Clear out all carrier info except CarrierNum from insplan
				Conversions.NonQString=
					"UPDATE insplan SET "
					+"Carrier='',Phone='',Address='',Address2='',City=''"
					+",State='',Zip='',NoSendElect='',ElectID=''";
				Conversions.SubmitNonQString();
				//Delete all ins templates
				Conversions.NonQString="DELETE FROM instemplate";
				Conversions.SubmitNonQString();
				//Add PracticeWeb Reporting program link
				Programs.Refresh();
				if(Programs.HList.ContainsKey("PracticeWebReports")){
					Programs.Cur=(Program)Programs.HList["PracticeWebReports"];
					Programs.Cur.Path="PWReports.exe";
					Programs.UpdateCur();
				}
				else{
					Programs.Cur=new Program();
					Programs.Cur.ProgName="PracticeWebReports";
					Programs.Cur.ProgDesc="PracticeWeb Reports from practice-web.com";
					Programs.Cur.Path="PWReports.exe";
					Programs.InsertCur();
				}
				//Add WebClaims program link
				Programs.Cur=new Program();
				Programs.Cur.ProgName="WebClaim";
				Programs.Cur.ProgDesc="WebClaim from webclaim.net";
				Programs.Cur.Path="WebClaim.exe";
				Programs.Cur.Note=@"This link will only work from the Send Claims toolbar.";
				Programs.Cur.Enabled=true;
				Programs.InsertCur();//we now have a ProgramNum to work with
				ToolButItems.Cur=new ToolButItem();
				ToolButItems.Cur.ProgramNum=Programs.Cur.ProgramNum;
				ToolButItems.Cur.ButtonText="WebClaim";
				ToolButItems.Cur.ToolBar=ToolBarsAvail.ClaimsSend;
				ToolButItems.InsertCur();
				//Add Renaissance program link
				Programs.Cur=new Program();
				Programs.Cur.ProgName="Renaissance";
				Programs.Cur.ProgDesc="Renaissance Claims from www.rss-llc.com";
				Programs.Cur.Path="";
				Programs.Cur.Note="This link will only work from the Send Claims toolbar.  No path or command line arguments are needed.";
				Programs.Cur.Enabled=true;
				Programs.InsertCur();//we now have a ProgramNum to work with
				ToolButItems.Cur=new ToolButItem();
				ToolButItems.Cur.ProgramNum=Programs.Cur.ProgramNum;
				ToolButItems.Cur.ButtonText="Renaissance";
				ToolButItems.Cur.ToolBar=ToolBarsAvail.ClaimsSend;
				ToolButItems.InsertCur();
				//Add Tigerview program link
				//id is any string format
				Programs.Cur=new Program();
				Programs.Cur.ProgName="TigerView";
				Programs.Cur.ProgDesc="TigerView from www.televere.com";
				Programs.Cur.Path=@"C:\Program Files\TigerView\tiger1.exe";
				Programs.Cur.CommandLine="SLAVE";
				Programs.Cur.Note="Command line should be SLAVE.  This will cause TigerView to look in the file specified in the Tiger1.ini path for information about the patient to open.";
				Programs.InsertCur();//we now have a ProgramNum to work with
				ProgramProperties.Cur=new ProgramProperty();
				ProgramProperties.Cur.ProgramNum=Programs.Cur.ProgramNum;
				ProgramProperties.Cur.PropertyDesc="Tiger1.ini path";
				ProgramProperties.Cur.PropertyValue=@"C:\Windows\Tiger1.ini";
				ProgramProperties.InsertCur();
				ProgramProperties.Cur=new ProgramProperty();
				ProgramProperties.Cur.ProgramNum=Programs.Cur.ProgramNum;
				ProgramProperties.Cur.PropertyDesc="Enter 0 to use PatientNum, or 1 to use ChartNum";
				ProgramProperties.Cur.PropertyValue="0";
				ProgramProperties.InsertCur();
				ToolButItems.Cur=new ToolButItem();
				ToolButItems.Cur.ProgramNum=Programs.Cur.ProgramNum;
				ToolButItems.Cur.ButtonText="TigerView";
				ToolButItems.Cur.ToolBar=ToolBarsAvail.ChartModule;
				ToolButItems.InsertCur();
				//Add Apteryx program link
				//id is any string format
				Programs.Cur=new Program();
				Programs.Cur.ProgName="Apteryx";
				Programs.Cur.ProgDesc="Apteryx from www.apteryxware.com";
				Programs.Cur.Path=@"C:\Program Files\Apteryx\XrayVision.exe";
				Programs.Cur.CommandLine="/p";
				Programs.Cur.Note="Command line option is typically /p for 'patient'. But you also have some other options before the /p, including /b for 'bar' which just brings up the patient's image bar, or /h to hide the splash screen. You can combine options. For example /h /p is valid.";
				Programs.InsertCur();//we now have a ProgramNum to work with
				ProgramProperties.Cur=new ProgramProperty();
				ProgramProperties.Cur.ProgramNum=Programs.Cur.ProgramNum;
				ProgramProperties.Cur.PropertyDesc="Enter 0 to use PatientNum, or 1 to use ChartNum";
				ProgramProperties.Cur.PropertyValue="0";
				ProgramProperties.InsertCur();
				ToolButItems.Cur=new ToolButItem();
				ToolButItems.Cur.ProgramNum=Programs.Cur.ProgramNum;
				ToolButItems.Cur.ButtonText="Apteryx";
				ToolButItems.Cur.ToolBar=ToolBarsAvail.ChartModule;
				ToolButItems.InsertCur();
				//Add Schick program link
				//id is any string format
				Programs.Cur=new Program();
				Programs.Cur.ProgName="Schick";
				Programs.Cur.ProgDesc="Schick from www.schicktech.com";
				Programs.Cur.Path="";
				Programs.Cur.Note="There is no path or command line for this link.  It will simply recognize the Schick CDR DICOM program if installed.";
				Programs.InsertCur();//we now have a ProgramNum to work with
				ProgramProperties.Cur=new ProgramProperty();
				ProgramProperties.Cur.ProgramNum=Programs.Cur.ProgramNum;
				ProgramProperties.Cur.PropertyDesc="Enter 0 to use PatientNum, or 1 to use ChartNum";
				ProgramProperties.Cur.PropertyValue="0";
				ProgramProperties.InsertCur();
				ToolButItems.Cur=new ToolButItem();
				ToolButItems.Cur.ProgramNum=Programs.Cur.ProgramNum;
				ToolButItems.Cur.ButtonText="Schick";
				ToolButItems.Cur.ToolBar=ToolBarsAvail.ChartModule;
				ToolButItems.InsertCur();
				//Add Dexis program link
				//id is any string format max 8 char.
				Programs.Cur=new Program();
				Programs.Cur.ProgName="Dexis";
				Programs.Cur.ProgDesc="Dexis from www.dexray.com";
				Programs.Cur.Path=@"C:\DEXIS\DEXIS.EXE";
				Programs.Cur.Note="There is no command line needed. The InfoFile path would usually be 'InfoFile.txt' which will be created the first time the link is used.";
				Programs.InsertCur();//we now have a ProgramNum to work with
				ProgramProperties.Cur=new ProgramProperty();
				ProgramProperties.Cur.ProgramNum=Programs.Cur.ProgramNum;
				ProgramProperties.Cur.PropertyDesc="Enter 0 to use PatientNum, or 1 to use ChartNum";
				ProgramProperties.Cur.PropertyValue="0";
				ProgramProperties.InsertCur();
				ProgramProperties.Cur=new ProgramProperty();
				ProgramProperties.Cur.ProgramNum=Programs.Cur.ProgramNum;
				ProgramProperties.Cur.PropertyDesc="InfoFile path";
				ProgramProperties.Cur.PropertyValue="InfoFile.txt";
				ProgramProperties.InsertCur();
				ToolButItems.Cur=new ToolButItem();
				ToolButItems.Cur.ProgramNum=Programs.Cur.ProgramNum;
				ToolButItems.Cur.ButtonText="Dexis";
				ToolButItems.Cur.ToolBar=ToolBarsAvail.ChartModule;
				ToolButItems.InsertCur();
				//Add VixWin program link:
				//id must be exactly 6 characters
				Programs.Cur=new Program();
				Programs.Cur.ProgName="VixWin";
				Programs.Cur.ProgDesc="VixWin from www.gendexxray.com";
				Programs.Cur.Path="";
				Programs.Cur.Note=@"This link uses the VixWin QuikLink program to listen for new files in the quiklink directory. No other file path or command line arguments are needed.  The QuikLink directory would typically be C:\vx_qlink\ .  If you use ChartNum for link, it cannot be more than 6 characters.";
				Programs.InsertCur();//we now have a ProgramNum to work with
				ProgramProperties.Cur=new ProgramProperty();
				ProgramProperties.Cur.ProgramNum=Programs.Cur.ProgramNum;
				ProgramProperties.Cur.PropertyDesc="Enter 0 to use PatientNum, or 1 to use ChartNum";
				ProgramProperties.Cur.PropertyValue="0";
				ProgramProperties.InsertCur();
				ProgramProperties.Cur=new ProgramProperty();
				ProgramProperties.Cur.ProgramNum=Programs.Cur.ProgramNum;
				ProgramProperties.Cur.PropertyDesc="QuikLink directory.";
				ProgramProperties.Cur.PropertyValue=@"C:\vx_qlink\";
				ProgramProperties.InsertCur();
				ToolButItems.Cur=new ToolButItem();
				ToolButItems.Cur.ProgramNum=Programs.Cur.ProgramNum;
				ToolButItems.Cur.ButtonText="VixWin";
				ToolButItems.Cur.ToolBar=ToolBarsAvail.ChartModule;
				ToolButItems.InsertCur();
				//Add Trophy program link
				//id is any string format
				Programs.Cur=new Program();
				Programs.Cur.ProgName="Trophy";
				Programs.Cur.ProgDesc="Trophy from www.trophy-imaging.com";
				Programs.Cur.Path="TW.exe";
				Programs.Cur.Note=@"Applies to Trophy versions 4.2 and 5.0.  No command line arguments are needed. The storage path is where all images are stored.  For instance \\SERVER\TrophyImages.  The images for each patient will be stored in a folder named according to the patient id.  For instance, \\SERVER\TrophyImages\AB1234\.";
				Programs.InsertCur();//we now have a ProgramNum to work with
				ProgramProperties.Cur=new ProgramProperty();
				ProgramProperties.Cur.ProgramNum=Programs.Cur.ProgramNum;
				ProgramProperties.Cur.PropertyDesc="Enter 0 to use PatientNum, or 1 to use ChartNum";
				ProgramProperties.Cur.PropertyValue="0";
				ProgramProperties.InsertCur();
				ProgramProperties.Cur=new ProgramProperty();
				ProgramProperties.Cur.ProgramNum=Programs.Cur.ProgramNum;
				ProgramProperties.Cur.PropertyDesc="Storage Path";
				ProgramProperties.Cur.PropertyValue="";
				ProgramProperties.InsertCur();
				ToolButItems.Cur=new ToolButItem();
				ToolButItems.Cur.ProgramNum=Programs.Cur.ProgramNum;
				ToolButItems.Cur.ButtonText="Trophy";
				ToolButItems.Cur.ToolBar=ToolBarsAvail.ChartModule;
				ToolButItems.InsertCur();
				//Add the attachments fields to the two claimforms that require them
				//ADA2002
				Conversions.SelectText="SELECT ClaimFormNum FROM claimform WHERE UniqueID = '1'";
				Conversions.SubmitSelect();
				ClaimFormItem ClaimFormItemCur=new ClaimFormItem();
				ClaimFormItemCur.ClaimFormNum=PIn.PInt(Conversions.TableQ.Rows[0][0].ToString());
				ClaimFormItemCur.FieldName="RadiographsNumAttached";
				ClaimFormItemCur.XPos=684;
				ClaimFormItemCur.YPos=738;
				ClaimFormItemCur.Width=27;
				ClaimFormItemCur.Height=14;
				ClaimFormItemCur.Insert();
				//Denti-Cal
				Conversions.SelectText="SELECT ClaimFormNum FROM claimform WHERE UniqueID = '2'";
				Conversions.SubmitSelect();
				ClaimFormItemCur=new ClaimFormItem();
				ClaimFormItemCur.ClaimFormNum=PIn.PInt(Conversions.TableQ.Rows[0][0].ToString());
				ClaimFormItemCur.FieldName="RadiographsNumAttached";
				ClaimFormItemCur.XPos=111;
				ClaimFormItemCur.YPos=217;
				ClaimFormItemCur.Width=30;
				ClaimFormItemCur.Height=14;
				ClaimFormItemCur.Insert();
				ClaimFormItemCur=new ClaimFormItem();
				ClaimFormItemCur.ClaimFormNum=PIn.PInt(Conversions.TableQ.Rows[0][0].ToString());
				ClaimFormItemCur.FieldName="IsRadiographsAttached";
				ClaimFormItemCur.XPos=186;
				ClaimFormItemCur.YPos=187;
				ClaimFormItemCur.Insert();
				//final:
				Conversions.NonQArray=new string[]{
					"UPDATE preference SET ValueString = '2.8.0.0' WHERE PrefName = 'DataBaseVersion'"
				};
				Conversions.SubmitNonQArray();
			}
			To2_8_2();
		}

		private void To2_8_2(){
			if(FromVersion < new Version("2.8.2.0")){
				Conversions.NonQArray=new string[]
				{
					"ALTER TABLE insplan DROP TemplateNum"
					,"DROP TABLE instemplate"
					,"UPDATE preference SET ValueString = '2.8.2.0' WHERE PrefName = 'DataBaseVersion'"
				};
				Conversions.SubmitNonQArray();
			}
			To2_8_3();
		}

		private void To2_8_3(){
			if(FromVersion < new Version("2.8.3.0")){
				Conversions.NonQArray=new string[]
				{
					"INSERT INTO preference VALUES ('RenaissanceLastBatchNumber','0')"
					,"INSERT INTO preference VALUES ('PatientSelectUsesSearchButton','0')"
					,"UPDATE preference SET ValueString = '2.8.3.0' WHERE PrefName = 'DataBaseVersion'"
				};
				Conversions.SubmitNonQArray();
			}
			To2_8_6();
		}

		private void To2_8_6(){
			if(FromVersion < new Version("2.8.6.0")){
				Conversions.NonQArray=new string[]
				{
					"ALTER TABLE patient CHANGE City City VARCHAR(100) NOT NULL"
					,"ALTER TABLE patient CHANGE State State VARCHAR(100) NOT NULL"
					,"ALTER TABLE patient CHANGE Zip Zip VARCHAR(100) NOT NULL"
					,"ALTER TABLE patient CHANGE SSN SSN VARCHAR(100) NOT NULL"
					,"UPDATE preference SET ValueString = '2.8.6.0' WHERE PrefName = 'DataBaseVersion'"
				};
				Conversions.SubmitNonQArray();
			}
			To2_8_10();
		}

		private void To2_8_10(){
			if(FromVersion < new Version("2.8.10.0")){
				Conversions.NonQArray=new string[]
				{
					"ALTER TABLE employer ADD Address varchar(255) NOT NULL"
					,"ALTER TABLE employer ADD Address2 varchar(255) NOT NULL"
					,"ALTER TABLE employer ADD City varchar(255) NOT NULL"
					,"ALTER TABLE employer ADD State varchar(255) NOT NULL"
					,"ALTER TABLE employer ADD Zip varchar(255) NOT NULL"
					,"ALTER TABLE employer ADD Phone varchar(255) NOT NULL"
					,"INSERT INTO preference VALUES ('CustomizedForPracticeWeb','0')"
					,"UPDATE preference SET ValueString = '2.8.10.0' WHERE PrefName = 'DataBaseVersion'"
				};
				Conversions.SubmitNonQArray();
			}
			To2_8_14();
		}

		private void To2_8_14(){
			if(FromVersion < new Version("2.8.14.0")){
				Conversions.NonQArray=new string[]
				{
					"ALTER TABLE adjustment CHANGE AdjType AdjType smallint unsigned NOT NULL"
					,"ALTER TABLE appointment CHANGE Confirmed Confirmed smallint unsigned NOT NULL"
					,"ALTER TABLE payment CHANGE PayType PayType smallint unsigned NOT NULL"
					,"ALTER TABLE procedurecode CHANGE ProcCat ProcCat smallint unsigned NOT NULL"
					,"ALTER TABLE procedurelog CHANGE Priority Priority smallint unsigned NOT NULL"
					,"ALTER TABLE procedurelog CHANGE Dx Dx smallint unsigned NOT NULL"
					,"UPDATE preference SET ValueString = '2.8.14.0' WHERE PrefName = 'DataBaseVersion'"
				};
				Conversions.SubmitNonQArray();
			}
			To2_9_1();
		}

		private void To2_9_1(){
			if(FromVersion < new Version("2.9.1.0")){
				//check to see if the conversion file is available
				//if(!File.Exists(@"ConversionFiles\convert_2_9_1.txt")){
				//	throw new Exception(@"ConversionFiles\convert_2_9_1.txt"+" could not be found.");
				//}
				ExecuteFile(@"ConversionFiles\convert_2_9_1.txt");//might throw an exception which we handle.
				Conversions.NonQArray=new string[]
				{
					"UPDATE preference SET ValueString = '2.9.1.0' WHERE PrefName = 'DataBaseVersion'"
				};
				Conversions.SubmitNonQArray();
			}
			To2_9_2();
		}

		private void To2_9_2(){
			if(FromVersion < new Version("2.9.2.0")){
				Conversions.NonQArray=new string[]
				{
					"ALTER TABLE patient ADD PriPending tinyint(1) unsigned NOT NULL"
					,"ALTER TABLE patient ADD SecPending tinyint(1) unsigned NOT NULL"
					,"ALTER TABLE appointment ADD Assistant smallint unsigned NOT NULL"
					,"UPDATE preference SET ValueString = '2.9.2.0' WHERE PrefName = 'DataBaseVersion'"
				};
				Conversions.SubmitNonQArray();
			}
			To2_9_5();
		}

		private void To2_9_5(){
			if(FromVersion < new Version("2.9.5.0")){
				Conversions.NonQArray=new string[]
				{
					"ALTER TABLE autocode ADD LessIntrusive tinyint(1) unsigned NOT NULL"
					,"UPDATE preference SET ValueString = '2.9.5.0' WHERE PrefName = 'DataBaseVersion'"
				};
				Conversions.SubmitNonQArray();
			}
			To2_9_8();
		}

		private void To2_9_8(){
			if(FromVersion < new Version("2.9.8.0")){
				string claimFormNum;
				//Change the PlaceNumericCode field for both HCFA forms
				Conversions.SelectText="SELECT ClaimFormNum FROM claimform WHERE UniqueID = '4'";
				Conversions.SubmitSelect();
				if(Conversions.TableQ.Rows.Count>0){
					claimFormNum=Conversions.TableQ.Rows[0][0].ToString();
					Conversions.NonQArray=new string[]
					{
						"UPDATE claimformitem SET FieldName='P1PlaceNumericCode' "
						+"WHERE FieldName='PlaceNumericCode' && ClaimFormNum='"+claimFormNum+"' "
						+"&& YPos='751'"
						,"UPDATE claimformitem SET FieldName='P2PlaceNumericCode' "
						+"WHERE FieldName='PlaceNumericCode' && ClaimFormNum='"+claimFormNum+"' "
						+"&& YPos='784'"
						,"UPDATE claimformitem SET FieldName='P3PlaceNumericCode' "
						+"WHERE FieldName='PlaceNumericCode' && ClaimFormNum='"+claimFormNum+"' "
						+"&& YPos='817'"
						,"UPDATE claimformitem SET FieldName='P4PlaceNumericCode' "
						+"WHERE FieldName='PlaceNumericCode' && ClaimFormNum='"+claimFormNum+"' "
						+"&& YPos='850'"
						,"UPDATE claimformitem SET FieldName='P5PlaceNumericCode' "
						+"WHERE FieldName='PlaceNumericCode' && ClaimFormNum='"+claimFormNum+"' "
						+"&& YPos='884'"
						,"UPDATE claimformitem SET FieldName='P6PlaceNumericCode' "
						+"WHERE FieldName='PlaceNumericCode' && ClaimFormNum='"+claimFormNum+"' "
						+"&& YPos='917'"
					};
					Conversions.SubmitNonQArray();
				}
				Conversions.SelectText="SELECT ClaimFormNum FROM claimform WHERE UniqueID = '5'";
				Conversions.SubmitSelect();
				if(Conversions.TableQ.Rows.Count>0){
					claimFormNum=Conversions.TableQ.Rows[0][0].ToString();
					Conversions.NonQArray=new string[]
					{
						"UPDATE claimformitem SET FieldName='P1PlaceNumericCode' "
						+"WHERE FieldName='PlaceNumericCode' && ClaimFormNum='"+claimFormNum+"' "
						+"&& YPos='751'"
						,"UPDATE claimformitem SET FieldName='P2PlaceNumericCode' "
						+"WHERE FieldName='PlaceNumericCode' && ClaimFormNum='"+claimFormNum+"' "
						+"&& YPos='784'"
						,"UPDATE claimformitem SET FieldName='P3PlaceNumericCode' "
						+"WHERE FieldName='PlaceNumericCode' && ClaimFormNum='"+claimFormNum+"' "
						+"&& YPos='817'"
						,"UPDATE claimformitem SET FieldName='P4PlaceNumericCode' "
						+"WHERE FieldName='PlaceNumericCode' && ClaimFormNum='"+claimFormNum+"' "
						+"&& YPos='850'"
						,"UPDATE claimformitem SET FieldName='P5PlaceNumericCode' "
						+"WHERE FieldName='PlaceNumericCode' && ClaimFormNum='"+claimFormNum+"' "
						+"&& YPos='884'"
						,"UPDATE claimformitem SET FieldName='P6PlaceNumericCode' "
						+"WHERE FieldName='PlaceNumericCode' && ClaimFormNum='"+claimFormNum+"' "
						+"&& YPos='917'"
					};
					Conversions.SubmitNonQArray();
				}
				//ADA2002 medicaid id's
				Conversions.SelectText="SELECT ClaimFormNum FROM claimform WHERE UniqueID = '1'";
				Conversions.SubmitSelect();
				if(Conversions.TableQ.Rows.Count>0){
					claimFormNum=Conversions.TableQ.Rows[0][0].ToString();
					Conversions.NonQArray=new string[]
					{
						"INSERT INTO claimformitem (ClaimFormNum,FieldName,XPos,YPos,Width,Height) VALUES("
						+"'"+claimFormNum+"','TreatingDentistMedicaidID','492','946','117','14')"
						,"INSERT INTO claimformitem (ClaimFormNum,FieldName,XPos,YPos,Width,Height) VALUES("
						+"'"+claimFormNum+"','BillingDentistMedicaidID','39','990','120','14')"
					};
					Conversions.SubmitNonQArray();
				}
				//ADA2000 employer and 3 radiograph fields.
				Conversions.SelectText="SELECT ClaimFormNum FROM claimform WHERE UniqueID = '3'";
				Conversions.SubmitSelect();
				if(Conversions.TableQ.Rows.Count>0){
					claimFormNum=Conversions.TableQ.Rows[0][0].ToString();
					Conversions.NonQArray=new string[]
					{
						"INSERT INTO claimformitem (ClaimFormNum,FieldName,XPos,YPos,Width,Height) VALUES("
						+"'"+claimFormNum+"','EmployerName','482','391','140','14')"
						,"INSERT INTO claimformitem (ClaimFormNum,FieldName,XPos,YPos,Width,Height) VALUES("
						+"'"+claimFormNum+"','IsRadiographsAttached','388','548','0','0')"
						,"INSERT INTO claimformitem (ClaimFormNum,FieldName,XPos,YPos,Width,Height) VALUES("
						+"'"+claimFormNum+"','RadiographsNotAttached','495','547','0','0')"
						,"INSERT INTO claimformitem (ClaimFormNum,FieldName,XPos,YPos,Width,Height) VALUES("
						+"'"+claimFormNum+"','RadiographsNumAttached','460','545','35','14')"
					};
					Conversions.SubmitNonQArray();
				}
				Conversions.NonQArray=new string[]
				{
					"UPDATE preference SET ValueString = '2.9.8.0' WHERE PrefName = 'DataBaseVersion'"
				};
				Conversions.SubmitNonQArray();
			}
			To3_0_1();
		}

		///<summary>Used by To3_0_1. IMPORTANT: remember that this method alters TableQ.</summary>
		private int GetPercent(int patNum,int priPlanNum,int secPlanNum,string adaCode,string priORsec){
			//Conversions.SelectText="SELECT 
			//get the covCatNum for this adaCode
			Conversions.SelectText="SELECT CovCatNum FROM covspan "
				+"WHERE '"+POut.PString(adaCode)+"' > FromCode "
				+"AND '"+POut.PString(adaCode)+"' < ToCode";
			Conversions.SubmitSelect();
			if(Conversions.TableQ.Rows.Count==0){
				return 0;//this code is not in any category, so coverage=0
			}
			int covCatNum=PIn.PInt(Conversions.TableQ.Rows[0][0].ToString());
			Conversions.SelectText="SELECT PlanNum,PriPatNum,SecPatNum,Percent FROM covpat WHERE "
				+"CovCatNum = '"+covCatNum.ToString()+"' "
				+"AND (PlanNum = '"+priPlanNum.ToString()+"' "
				+"OR PlanNum = '"+secPlanNum.ToString()+"' "
				+"OR PriPatNum = '"+patNum.ToString()+"' "
				+"OR SecPatNum = '"+patNum.ToString()+"')";
			Conversions.SubmitSelect();
			if(Conversions.TableQ.Rows.Count==0){
				return 0;//no percentages have been entered for this patient or plan
			}
			for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
				//first handle the patient overrides
				if(priORsec=="pri" && PIn.PInt(Conversions.TableQ.Rows[i][1].ToString())==patNum){
					return PIn.PInt(Conversions.TableQ.Rows[i][3].ToString());
				}
				if(priORsec=="sec" && PIn.PInt(Conversions.TableQ.Rows[i][2].ToString())==patNum){
					return PIn.PInt(Conversions.TableQ.Rows[i][3].ToString());
				}
				//then handle the percentages attached to plans(much more common)
				if(priORsec=="pri" && PIn.PInt(Conversions.TableQ.Rows[i][0].ToString())==priPlanNum){
					return PIn.PInt(Conversions.TableQ.Rows[i][3].ToString());
				}
				if(priORsec=="sec" && PIn.PInt(Conversions.TableQ.Rows[i][0].ToString())==secPlanNum){
					return PIn.PInt(Conversions.TableQ.Rows[i][3].ToString());
				}
			}
			return 0;
		}

		private void To3_0_1(){
			if(FromVersion < new Version("3.0.1.0")){
				//if(!File.Exists(@"ConversionFiles\convert_3_0_1.txt")){
				//	throw new Exception(@"ConversionFiles\convert_3_0_1.txt"+" could not be found.");
				//}
				ExecuteFile(@"ConversionFiles\convert_3_0_1.txt");//might throw an exception which we handle.
				//convert appointment patterns from ten minute to five minute intervals---------------------
				Conversions.SelectText="SELECT AptNum,Pattern FROM appointment";
				Conversions.SubmitSelect();
				StringBuilder sb;
				string pattern;
				for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
					pattern=PIn.PString(Conversions.TableQ.Rows[i][1].ToString());
					sb=new StringBuilder();
					for(int j=0;j<pattern.Length;j++){
						sb.Append(pattern.Substring(j,1));
						sb.Append(pattern.Substring(j,1));
					}
					Conversions.NonQString="UPDATE appointment SET "
						+"Pattern='"+POut.PString(sb.ToString())+"' "
						+"WHERE AptNum='"+Conversions.TableQ.Rows[i][0].ToString()+"'";
					Conversions.SubmitNonQString();
				}
				//add the default 5 Elements to each ApptView-----------------------------------------------
				Conversions.SelectText="SELECT ApptViewNum FROM apptview";
				Conversions.SubmitSelect();
				for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
					Conversions.NonQArray=new string[]
					{
						"INSERT INTO apptviewitem(ApptViewNum,ElementDesc,ElementOrder,ElementColor) "
							+"VALUES('"+Conversions.TableQ.Rows[i][0].ToString()+"','PatientName','0','-16777216')"
						,"INSERT INTO apptviewitem(ApptViewNum,ElementDesc,ElementOrder,ElementColor) "
							+"VALUES('"+Conversions.TableQ.Rows[i][0].ToString()+"','Lab','1','-65536')"
						,"INSERT INTO apptviewitem(ApptViewNum,ElementDesc,ElementOrder,ElementColor) "
							+"VALUES('"+Conversions.TableQ.Rows[i][0].ToString()+"','Procs','2','-16777216')"
						,"INSERT INTO apptviewitem(ApptViewNum,ElementDesc,ElementOrder,ElementColor) "
							+"VALUES('"+Conversions.TableQ.Rows[i][0].ToString()+"','Note','3','-16777216')"
						,"INSERT INTO apptviewitem(ApptViewNum,ElementDesc,ElementOrder,ElementColor) "
							+"VALUES('"+Conversions.TableQ.Rows[i][0].ToString()+"','Production','4','-16777216')"
					};
					Conversions.SubmitNonQArray();
				}				
				//MessageBox.Show("Appointments converted.");
				//Any claimprocs attached to claims with ins being Cap, should be CapClaim, even if paid
				Conversions.SelectText="SELECT claimproc.ClaimProcNum FROM claimproc,insplan "
					+"WHERE claimproc.PlanNum=insplan.PlanNum "
					+"AND claimproc.ClaimNum != '0' "
					+"AND insplan.PlanType='c'";
				Conversions.SubmitSelect();
				for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
					Conversions.NonQString="UPDATE claimproc SET Status='5' "//CapClaim
						+"WHERE ClaimProcNum='"+Conversions.TableQ.Rows[i][0].ToString()+"'";
					Conversions.SubmitNonQString();
				}
				//edit any existing claimprocs-------------------------------------------------------------
				//These are all associated with claims, but we are not changing the claim values,
				//just some of the estimates.  None of these changes affect any claim or balance.
				//Ignore any status=CapClaim since these are all duplicates just for sending the claim
				//Add percentages etc from procedure
				int percentage=0;
				int planNum=0;
				double baseEst=0;
				double overrideAmt=0;
				DataTable procTable;
				Conversions.SelectText="SELECT claimproc.ClaimProcNum,patient.PriPlanNum,"//0,1
					+"patient.SecPlanNum,patient.PatNum,claimproc.PlanNum,procedurelog.ADACode,"//2,3,4,5
					+"procedurelog.OverridePri,procedurelog.OverrideSec,procedurelog.ProcFee "//6,7,8
					+"FROM claimproc,procedurelog,patient "
					//this next line ignores any claimprocs not attached to a proc. so skips adjustments.
					+"WHERE claimproc.ProcNum=procedurelog.ProcNum "
					+"AND patient.PatNum=procedurelog.PatNum "
					+"AND claimproc.Status != 2 "//skips preauths
					+"AND claimproc.Status != 4 "//skips supplemental
					+"AND claimproc.Status != 5 ";//skips capClaim
				Conversions.SubmitSelect();
				procTable=Conversions.TableQ.Copy();//so that we can perform other queries
				for(int i=0;i<procTable.Rows.Count;i++){
					planNum=PIn.PInt(procTable.Rows[i][4].ToString());//claimproc.PlanNum
					//if primary
					if(planNum==PIn.PInt(procTable.Rows[i][1].ToString())){//priPlanNum
						percentage=GetPercent(PIn.PInt(procTable.Rows[i][3].ToString()),//patNum
							PIn.PInt(procTable.Rows[i][1].ToString()),//priPlanNum
							PIn.PInt(procTable.Rows[i][2].ToString()),//secPlanNum
							PIn.PString(procTable.Rows[i][5].ToString()),//ADACode
							"pri");
						overrideAmt=PIn.PDouble(procTable.Rows[i][6].ToString());
					}
					//else if secondary
					else if(planNum==PIn.PInt(procTable.Rows[i][2].ToString())){//priPlanNum
						percentage=GetPercent(PIn.PInt(procTable.Rows[i][3].ToString()),//patNum
							PIn.PInt(procTable.Rows[i][1].ToString()),//priPlanNum
							PIn.PInt(procTable.Rows[i][2].ToString()),//secPlanNum
							PIn.PString(procTable.Rows[i][5].ToString()),//ADACode
							"sec");
						overrideAmt=PIn.PDouble(procTable.Rows[i][7].ToString());
					}
					else{
						//plan is neither pri or sec, so disregard
						continue;
					}
					//fee x percentage:
					baseEst=PIn.PDouble(procTable.Rows[i][8].ToString())*(double)percentage/100;
					Conversions.NonQString="UPDATE claimproc SET "
						//+"AllowedAmt='-1',"
						+"Percentage='"+percentage.ToString()+"',"
						//+"PercentOverride='-1',"
						//+"CopayAmt='-1',"
						+"OverrideInsEst='"+overrideAmt.ToString()+"',"
						//+"OverAnnualMax='-1',"
						//+"PaidOtherIns='-1',"
						+"BaseEst='"+baseEst.ToString()+"'"
						//+"CopayOverride='-1'"
						+" WHERE ClaimProcNum='"+procTable.Rows[i][0].ToString()+"'";
					//MessageBox.Show(Conversions.NonQString);
					Conversions.SubmitNonQString();
				}
				//convert all estimates into claimprocs-------------------------------------------------
				Conversions.SelectText="SELECT procedurelog.ProcNum,procedurelog.PatNum,"//0,1
					+"procedurelog.ProvNum,patient.PriPlanNum,patient.SecPlanNum,"//2,3,4
					+"claimproc.ClaimProcNum,procedurelog.ADACode,procedurelog.ProcDate,"//5,6,7
					+"procedurelog.OverridePri,procedurelog.OverrideSec,procedurelog.NoBillIns,"//8,9,10
					+"procedurelog.CapCoPay,procedurelog.ProcStatus,procedurelog.ProcFee,"//11,12,13
					+"insplan.PlanType "//14
					+"FROM procedurelog,patient,insplan "
					+"LEFT JOIN claimproc ON claimproc.ProcNum=procedurelog.ProcNum "//only interested in NULL
					+"WHERE procedurelog.PatNum=patient.PatNum "
					//this is to test for capitation. It also limits results to patients with insurance.
					+"AND patient.PriPlanNum=insplan.PlanNum "
					//+"AND patient.PriPlanNum > 0 "//only patients with insurance
					+"AND (procedurelog.ProcStatus=1 "//status TP
					+"OR procedurelog.ProcStatus=2) "//status C
					+"AND (claimproc.ClaimProcNum IS NULL "//only if not already attached to a claim
					+"OR claimproc.Status='5')";//or CapClaim
				Conversions.SubmitSelect();
				procTable=Conversions.TableQ.Copy();//so that we can perform other queries
				int status=0;
				double copay=0;
				double writeoff=0;
				for(int i=0;i<procTable.Rows.Count;i++){//loop procedures
					//1. noBillIns
					if(PIn.PBool(procTable.Rows[i][10].ToString())//if noBillIns
						&& PIn.PDouble(procTable.Rows[i][11].ToString()) ==-1){//and not a cap procedure
						//primary
						if(PIn.PInt(procTable.Rows[i][3].ToString())!=0){//if has pri ins
							Conversions.NonQString="INSERT INTO claimproc(ProcNum,PatNum,ProvNum,Status,PlanNum,"
								+"DateCP,AllowedAmt,Percentage,PercentOverride,CopayAmt,OverrideInsEst,"
								+"NoBillIns,OverAnnualMax,PaidOtherIns) "
								+"VALUES ("
								+"'"+procTable.Rows[i][0].ToString()+"',"//procnum
								+"'"+procTable.Rows[i][1].ToString()+"',"//patnum
								+"'"+procTable.Rows[i][2].ToString()+"',"//provnum
								+"'6',"//status:Estimate
								+"'"+procTable.Rows[i][3].ToString()+"',"//priPlanNum
								+"'"+POut.PDate(PIn.PDate(procTable.Rows[i][7].ToString()))+"',"//dateCP
								//these -1's are unnecessary, but I already added them, so they are here.
								+"'-1',"//allowedamt
								+"'-1',"//percentage
								+"'-1',"//percentoverride
								+"'-1',"//copayamt
								+"'-1',"//overrideInsEst
								+"'1',"//NoBillIns,
								+"'-1',"//OverAnnualMax
								+"'-1'"//PaidOtherIns
								+")";
							Conversions.SubmitNonQString();
						}
						//secondary
						if(PIn.PInt(procTable.Rows[i][4].ToString())!=0){//if has sec ins
							Conversions.NonQString="INSERT INTO claimproc(ProcNum,PatNum,ProvNum,Status,PlanNum,"
								+"DateCP,AllowedAmt,Percentage,PercentOverride,CopayAmt,OverrideInsEst,"
								+"NoBillIns,OverAnnualMax,PaidOtherIns) "
								+"VALUES ("
								+"'"+procTable.Rows[i][0].ToString()+"',"//procnum
								+"'"+procTable.Rows[i][1].ToString()+"',"//patnum
								+"'"+procTable.Rows[i][2].ToString()+"',"//provnum
								+"'6',"//status:Estimate
								+"'"+procTable.Rows[i][4].ToString()+"',"//secPlanNum
								+"'"+POut.PDate(PIn.PDate(procTable.Rows[i][7].ToString()))+"',"//dateCP
								+"'-1',"//allowedamt
								+"'-1',"//percentage
								+"'-1',"//percentoverride
								+"'-1',"//copayamt
								+"'-1',"//overrideInsEst
								+"'1',"//NoBillIns,
								+"'-1',"//OverAnnualMax
								+"'-1'"//PaidOtherIns
								+")";
							Conversions.SubmitNonQString();
						}
						continue;
					}//1. noBillIns
					//2. capitation. Always primary. If C, then affects aging via CapComplete.
							//Never attached to claim.
					copay=PIn.PDouble(procTable.Rows[i][11].ToString());
					//if CapCoPay not -1, and priIns is cap, then this is a cap proc
					if(copay!=-1 && PIn.PString(procTable.Rows[i][14].ToString())=="c"){
						if(PIn.PInt(procTable.Rows[i][12].ToString())==1){//proc status =tp
							status=8;//claimProc status=CapEstimate
						}
						if(PIn.PInt(procTable.Rows[i][12].ToString())==2){//proc status =c
							status=7;//claimProc status=CapComplete
						}
						writeoff=PIn.PDouble(procTable.Rows[i][13].ToString())//procFee
							-copay;
						Conversions.NonQString="INSERT INTO claimproc(ProcNum,PatNum,ProvNum,"
							+"Status,PlanNum,DateCP,WriteOff,AllowedAmt,Percentage,PercentOverride,"
							+"CopayAmt,OverrideInsEst,OverAnnualMax,PaidOtherIns,NoBillIns) "
							+"VALUES ("
							+"'"+procTable.Rows[i][0].ToString()+"',"//procnum
							+"'"+procTable.Rows[i][1].ToString()+"',"//patnum
							+"'"+procTable.Rows[i][2].ToString()+"',"//provnum
							+"'"+status.ToString()+"',"//status
							+"'"+procTable.Rows[i][3].ToString()+"',"//priPlanNum
							+"'"+POut.PDate(PIn.PDate(procTable.Rows[i][7].ToString()))+"',"//dateCP
							+"'"+writeoff.ToString()+"',"//writeoff
							+"'-1',"//allowedamt
							+"'-1',"//percentage
							+"'-1',"//percentoverride
							+"'"+copay.ToString()+"',"//copayamt
							+"'-1',"//overrideInsEst
							+"'-1',"//OverAnnualMax
							+"'-1',"//PaidOtherIns
							+"'"+procTable.Rows[i][10].ToString()+"'"//noBillIns is allowed for cap
							+")";
						Conversions.SubmitNonQString();
						continue;
					}
					//3. standard primary estimate:
					//always a primary estimate because original query excluded patients with no ins.
					planNum=PIn.PInt(procTable.Rows[i][3].ToString());//priPlanNum
					percentage=GetPercent(PIn.PInt(procTable.Rows[i][1].ToString()),//patNum
						PIn.PInt(procTable.Rows[i][3].ToString()),//priPlanNum
						PIn.PInt(procTable.Rows[i][4].ToString()),//secPlanNum
						PIn.PString(procTable.Rows[i][6].ToString()),//ADACode
						"pri");
					baseEst=PIn.PDouble(procTable.Rows[i][13].ToString())*(double)percentage/100;
					Conversions.NonQString="INSERT INTO claimproc(ProcNum,PatNum,ProvNum,"
						+"Status,PlanNum,DateCP,WriteOff,AllowedAmt,Percentage,PercentOverride,"
						+"CopayAmt,OverrideInsEst,NoBillIns,OverAnnualMax,PaidOtherIns,BaseEst) "
						+"VALUES ("
						+"'"+procTable.Rows[i][0].ToString()+"',"//procnum
						+"'"+procTable.Rows[i][1].ToString()+"',"//patnum
						+"'"+procTable.Rows[i][2].ToString()+"',"//provnum
						+"'6',"//status:Estimate
						+"'"+planNum.ToString()+"',"//plannum
						+"'"+POut.PDate(PIn.PDate(procTable.Rows[i][7].ToString()))+"',"//dateCP
						+"'0',"//writeoff
						+"'-1',"//allowedamt
						+"'"+percentage.ToString()+"',"//percentage
						+"'-1',"//percentoverride
						+"'-1',"//copayamt
						+"'"+procTable.Rows[i][8].ToString()+"',"//overrideInsEst-pri
						+"'0',"//NoBillIns,
						+"'-1',"//OverAnnualMax
						+"'-1',"//PaidOtherIns
						+"'"+baseEst.ToString()+"'"//BaseEst
						+")";
					Conversions.SubmitNonQString();
					//4. standard secondary estimate
					//secondary can be in addition to primary, or not at all
					planNum=PIn.PInt(procTable.Rows[i][4].ToString());//secPlanNum
					if(planNum==0){
						continue;
					}
					percentage=GetPercent(PIn.PInt(procTable.Rows[i][1].ToString()),//patNum
						PIn.PInt(procTable.Rows[i][3].ToString()),//priPlanNum
						PIn.PInt(procTable.Rows[i][4].ToString()),//secPlanNum
						PIn.PString(procTable.Rows[i][6].ToString()),//ADACode
						"sec");
					baseEst=PIn.PDouble(procTable.Rows[i][13].ToString())*(double)percentage/100;
					Conversions.NonQString="INSERT INTO claimproc(ProcNum,PatNum,ProvNum,"
						+"Status,PlanNum,DateCP,WriteOff,AllowedAmt,Percentage,PercentOverride,"
						+"CopayAmt,OverrideInsEst,NoBillIns,OverAnnualMax,PaidOtherIns,BaseEst) "
						+"VALUES ("
						+"'"+procTable.Rows[i][0].ToString()+"',"//procnum
						+"'"+procTable.Rows[i][1].ToString()+"',"//patnum
						+"'"+procTable.Rows[i][2].ToString()+"',"//provnum
						+"'6',"//status:Estimate
						+"'"+planNum.ToString()+"',"//plannum
						+"'"+POut.PDate(PIn.PDate(procTable.Rows[i][7].ToString()))+"',"//dateCP
						+"'0',"//writeoff
						+"'-1',"//allowedamt
						+"'"+percentage.ToString()+"',"//percentage
						+"'-1',"//percentoverride
						+"'-1',"//copayamt
						+"'"+procTable.Rows[i][9].ToString()+"',"//overrideInsEst-pri
						+"'0',"//NoBillIns,
						+"'-1',"//OverAnnualMax
						+"'-1',"//PaidOtherIns
						+"'"+baseEst.ToString()+"'"//BaseEst
						+")";
					Conversions.SubmitNonQString();
				}//loop procedures
				Conversions.NonQString="UPDATE claimproc SET ProcDate=DateCP";//affects ALL patients
				Conversions.SubmitNonQString();
				//MessageBox.Show("Procedure percentages converted to claimprocs.");
				Conversions.NonQArray=new string[]
				{
					"UPDATE procedurelog SET OverridePri='0',OverrideSec='0',NoBillIns='0',"
						+"IsCovIns='0',CapCoPay='0'"
				};
				Conversions.SubmitNonQArray();
				//convert medical/service notes from defs table to quickpaste notes----------------------
				Conversions.NonQArray=new string[]
				{
					"INSERT INTO quickpastecat "
						+"VALUES ('1','Medical Urgent','0','22')"
					,"INSERT INTO quickpastecat "
						+"VALUES ('2','Medical Summary','1','9')"
					,"INSERT INTO quickpastecat "
						+"VALUES ('3','Service Notes','2','10')"
					,"INSERT INTO quickpastecat "
						+"VALUES ('4','Medical History','3','11')"
				};
				Conversions.SubmitNonQArray();
				Conversions.SelectText="SELECT * FROM definition WHERE Category='8'";//Medical Notes
				Conversions.SubmitSelect();
				for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
					Conversions.NonQString="INSERT INTO quickpastenote (QuickPasteCatNum,ItemOrder,Note) "
						+"VALUES ('1','"+i.ToString()+"','"
						+POut.PString(Conversions.TableQ.Rows[i][3].ToString())+"')";
					Conversions.SubmitNonQString();
					Conversions.NonQString="INSERT INTO quickpastenote (QuickPasteCatNum,ItemOrder,Note) "
						+"VALUES ('2','"+i.ToString()+"','"
						+POut.PString(Conversions.TableQ.Rows[i][3].ToString())+"')";
					Conversions.SubmitNonQString();
					Conversions.NonQString="INSERT INTO quickpastenote (QuickPasteCatNum,ItemOrder,Note) "
						+"VALUES ('4','"+i.ToString()+"','"
						+POut.PString(Conversions.TableQ.Rows[i][3].ToString())+"')";
					Conversions.SubmitNonQString();
				}
				Conversions.SelectText="SELECT * FROM definition WHERE Category='14'";//Service Notes
				Conversions.SubmitSelect();
				for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
					Conversions.NonQString="INSERT INTO quickpastenote (QuickPasteCatNum,ItemOrder,Note) "
						+"VALUES ('3','"+i.ToString()+"','"
						+POut.PString(Conversions.TableQ.Rows[i][3].ToString())+"')";
					Conversions.SubmitNonQString();
				}
				//add image categories to the chart module-----------------------------------------------
				Conversions.SelectText="SELECT MAX(ItemOrder) FROM definition WHERE Category=18";
				Conversions.SubmitSelect();
				int lastI=PIn.PInt(Conversions.TableQ.Rows[0][0].ToString());
				Conversions.NonQArray=new string[]
				{
					"INSERT INTO definition(Category,ItemOrder,ItemName,ItemValue) "
						+"VALUES(18,"+POut.PInt(lastI+1)+",'BWs','X')"
					,"INSERT INTO definition(Category,ItemOrder,ItemName,ItemValue) "
						+"VALUES(18,"+POut.PInt(lastI+2)+",'FMXs','X')"
					,"INSERT INTO definition(Category,ItemOrder,ItemName,ItemValue) "
						+"VALUES(18,"+POut.PInt(lastI+3)+",'Panos','X')"
					,"INSERT INTO definition(Category,ItemOrder,ItemName,ItemValue) "
						+"VALUES(18,"+POut.PInt(lastI+4)+",'Photos','X')"
					,"UPDATE preference SET ValueString = '3.0.1.0' WHERE PrefName = 'DataBaseVersion'"
				};
				Conversions.SubmitNonQArray();
			}
			To3_0_2();
		}

		private void To3_0_2(){
			if(FromVersion < new Version("3.0.2.0")){
				Conversions.NonQArray=new string[]
				{
					"INSERT INTO preference VALUES('TreatPlanShowGraphics','1')"
					,"INSERT INTO preference VALUES('TreatPlanShowCompleted','1')"
					,"INSERT INTO preference VALUES('TreatPlanShowIns','1')"
					,"UPDATE preference SET ValueString = '3.0.2.0' WHERE PrefName = 'DataBaseVersion'"
				};
				Conversions.SubmitNonQArray();
			}
			To3_0_3();
		}

		private void To3_0_3(){
			if(FromVersion < new Version("3.0.3.0")){
				Conversions.SelectText="SELECT CONCAT(LName,', ',FName) FROM payplan,patient "
					+"WHERE patient.PatNum=payplan.PatNum";
				Conversions.SubmitSelect();
				if(Conversions.TableQ.Rows.Count>0){
					string planPats="";
					for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
						if(i>0){
							planPats+=",";
						}
						planPats+=PIn.PString(Conversions.TableQ.Rows[i][0].ToString());
					}				
					MessageBox.Show("You have payment plans for the following patients: "
						+planPats+".  "
						+"There was a bug in the way the amount due was being calculated, so you will "
						+"want to follow these steps to correct the amounts due.  For each payment plan, "
						+"simply open the plan from the patient account and then click OK.  This will "
						+"reset the amount due.");
				}
				Conversions.NonQArray=new string[]
				{
					"ALTER TABLE payplan ADD TotalCost double NOT NULL"
					,"UPDATE payplan SET TotalCost = TotalAmount"
					,"UPDATE preference SET ValueString = '3.0.3.0' WHERE PrefName = 'DataBaseVersion'"
				};
				Conversions.SubmitNonQArray();
			}
			To3_0_4();
		}

		private void To3_0_4(){
			if(FromVersion < new Version("3.0.4.0")){
				Conversions.NonQArray=new string[]
				{
					"ALTER TABLE procedurelog ADD HideGraphical tinyint unsigned NOT NULL"
					,"ALTER TABLE adjustment CHANGE AdjNote AdjNote text NOT NULL"
					,"UPDATE preference SET ValueString = '3.0.4.0' WHERE PrefName = 'DataBaseVersion'"
				};
				Conversions.SubmitNonQArray();
			}
			To3_0_5();
		}

		private void To3_0_5(){
			if(FromVersion < new Version("3.0.5.0")){
				//Delete procedures for patients that have been deleted:
				Conversions.SelectText="SELECT patient.PatNum FROM patient,procedurelog "
					+"WHERE patient.PatNum=procedurelog.PatNum "
					+"AND patient.PatStatus=4";
				Conversions.SubmitSelect();
				for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
					Conversions.NonQString="DELETE FROM procedurelog "
						+"WHERE PatNum="+Conversions.TableQ.Rows[i][0].ToString();
					Conversions.SubmitNonQString();
				}
				//Delete extra est entries caused when patient switched plans before conversion:
				Conversions.SelectText=@"SELECT 
					cp1.ClaimProcNum,patient.PatNum,patient.LName,patient.FName
					FROM claimproc cp1,claimproc cp2,patient
					WHERE patient.PatNum=cp1.PatNum
					AND patient.PatNum=cp2.PatNum
					AND patient.PriPlanNum=cp1.PlanNum
					AND patient.SecPlanNum=0
					AND cp1.ProcNum=cp2.ProcNum
					AND cp1.ClaimProcNum!=cp2.ClaimProcNum
					AND cp1.Status=6";//estimate
				Conversions.SubmitSelect();
				for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
					Conversions.NonQString="DELETE FROM claimproc "
						+"WHERE ClaimProcNum="+Conversions.TableQ.Rows[i][0].ToString();
					Conversions.SubmitNonQString();
				}
				Conversions.NonQArray=new string[]
				{
					"ALTER TABLE claimform CHANGE UniqueID UniqueID varchar(255) NOT NULL"
					,"UPDATE claimform SET UniqueID=concat('OD',UniqueID)"
					,"UPDATE claimform SET UniqueID='' WHERE UniqueID='OD0'"
					,"UPDATE preference SET ValueString = '3.0.5.0' WHERE PrefName = 'DataBaseVersion'"
				};
				Conversions.SubmitNonQArray();
			}
			To3_1_0();
		}

		private void To3_1_0(){
			if(FromVersion < new Version("3.1.0.0")){
				//if(!File.Exists(@"ConversionFiles\convert_3_1_0.txt")){
				//	throw new Exception(@"ConversionFiles\convert_3_1_0.txt"+" could not be found.");
				//}
				ExecuteFile(@"ConversionFiles\convert_3_1_0.txt");//Might throw an exception which we handle
				//add Sirona Sidexis:
				string command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
					+") VALUES("
					+"'Sirona', "
					+"'Sirona Sidexis from www.sirona.com', "
					+"'0', "
					+"'"+POut.PString(@"C:\sidexis\sidexis.exe")+"', "
					+"'', "
					+"'')";
				DataConnection dcon=new DataConnection();
				dcon.NonQ(command,true);
				int programNum=dcon.InsertID;//we now have a ProgramNum to work with
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
					+") VALUES("
					+"'"+programNum.ToString()+"', "
					+"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
					+"'0')";
				dcon.NonQ(command);
				command="INSERT INTO toolbutitem (ProgramNum,ToolBar,ButtonText) "
					+"VALUES ("
					+"'"+programNum.ToString()+"', "
					+"'"+((int)ToolBarsAvail.ChartModule).ToString()+"', "
					+"'Sirona')";
				dcon.NonQ(command);
				//convert recall
				//For inactive patients, assume no meaningful info if patients inactive,
				//so no need to create a recall. Only convert active patients.
				command="SELECT PatNum,RecallStatus,RecallInterval "
					+"FROM patient WHERE PatStatus=0";
				DataTable patTable=dcon.GetTable(command);
				DataTable table;
				DateTime previousDate;
				DateTime dueDate;
				int patNum;
				int status;
				int interval;
				Interval newInterval;
				for(int i=0;i<patTable.Rows.Count;i++){
					patNum=PIn.PInt(patTable.Rows[i][0].ToString());
					status=PIn.PInt(patTable.Rows[i][1].ToString());
					interval=PIn.PInt(patTable.Rows[i][2].ToString());
					//get previous date
					command="SELECT MAX(procedurelog.procdate) "
						+"FROM procedurelog,procedurecode "
						+"WHERE procedurelog.PatNum="+patNum.ToString()
						+" AND procedurecode.ADACode = procedurelog.ADACode "
						+"AND procedurecode.SetRecall = 1 "
						+"AND (procedurelog.ProcStatus = 2 "
						+"OR procedurelog.ProcStatus = 3 "
						+"OR procedurelog.ProcStatus = 4) "
						+"GROUP BY procedurelog.PatNum";
					table=dcon.GetTable(command);
					if(table.Rows.Count==0){
						previousDate=DateTime.MinValue;
					}
					else{
						previousDate=PIn.PDate(table.Rows[0][0].ToString());
					}
					//If no useful info and no trigger. No recall created
					if(status==0 && (interval==0 || interval==6)
						&& previousDate==DateTime.MinValue)//and no trigger
					{
						continue;
					}
					if(interval==0){
						newInterval=new Interval(0,0,6,0);
					}
					else{
						newInterval=new Interval(0,0,interval,0);
					}
					if(previousDate==DateTime.MinValue){
						dueDate=DateTime.MinValue;
					}
					else{
						dueDate=previousDate+newInterval;
					}
					command="INSERT INTO recall (PatNum,DateDueCalc,DateDue,DatePrevious,"
						+"RecallInterval,RecallStatus"
						+") VALUES ("
						+"'"+POut.PInt   (patNum)+"', "
						+"'"+POut.PDate  (dueDate)+"', "
						+"'"+POut.PDate  (dueDate)+"', "
						+"'"+POut.PDate  (previousDate)+"', "
						+"'"+POut.PInt   (newInterval.ToInt())+"', "
						+"'"+POut.PInt   (status)+"')";
					dcon.NonQ(command);
				}//for int i<patTable
				command="UPDATE preference SET ValueString = '3.1.0.0' WHERE PrefName = 'DataBaseVersion'";
				dcon.NonQ(command);
			}
			To3_1_3();
		}

		private void To3_1_3(){
			if(FromVersion < new Version("3.1.3.0")){
				//0 values in date fields are causing a lot of program slowdown
				string[] commands=new string[]
				{
					"UPDATE adjustment SET AdjDate='0001-01-01' WHERE AdjDate='0000-00-00'"
					,"UPDATE appointment SET AptDateTime='0001-01-01 00:00:00' "
						+"WHERE AptDateTime LIKE '0000-00-00%'"
					,"UPDATE claim SET DateService='0001-01-01' WHERE DateService='0000-00-00'"
					,"UPDATE claim SET DateSent='0001-01-01' WHERE DateSent='0000-00-00'"
					,"UPDATE claim SET DateReceived='0001-01-01' WHERE DateReceived='0000-00-00'"
					,"UPDATE claim SET PriorDate='0001-01-01' WHERE PriorDate='0000-00-00'"
					,"UPDATE claim SET AccidentDate='0001-01-01' WHERE AccidentDate='0000-00-00'"
					,"UPDATE claim SET OrthoDate='0001-01-01' WHERE OrthoDate='0000-00-00'"
					,"UPDATE claimpayment SET CheckDate='0001-01-01' WHERE CheckDate='0000-00-00'"
					,"UPDATE claimproc SET DateCP='0001-01-01' WHERE DateCP='0000-00-00'"
					,"UPDATE claimproc SET ProcDate='0001-01-01' WHERE ProcDate='0000-00-00'"
					,"UPDATE insplan SET DateEffective='0001-01-01' WHERE DateEffective='0000-00-00'"
					,"UPDATE insplan SET DateTerm='0001-01-01' WHERE DateTerm='0000-00-00'"
					,"UPDATE insplan SET RenewMonth='1' WHERE RenewMonth='0'"
					,"UPDATE patient SET Birthdate='0001-01-01' WHERE Birthdate='0000-00-00'"
					,"UPDATE patient SET DateFirstVisit='0001-01-01' WHERE DateFirstVisit='0000-00-00'"
					,"UPDATE procedurelog SET ProcDate='0001-01-01' WHERE ProcDate='0000-00-00'"
					,"UPDATE procedurelog SET DateOriginalProsth='0001-01-01' "
						+"WHERE DateOriginalProsth='0000-00-00'"
					,"UPDATE procedurelog SET DateLocked='0001-01-01' WHERE DateLocked='0000-00-00'"
					,"UPDATE recall SET DateDueCalc='0001-01-01' WHERE DateDueCalc='0000-00-00'"
					,"UPDATE recall SET DateDue='0001-01-01' WHERE DateDue='0000-00-00'"
					,"UPDATE recall SET DatePrevious='0001-01-01' WHERE DatePrevious='0000-00-00'"
					,"ALTER table adjustment CHANGE AdjDate AdjDate date NOT NULL default '0001-01-01'"
					,"ALTER table appointment CHANGE AptDateTime AptDateTime datetime NOT NULL "
						+"default '0001-01-01 00:00:00'"
					,"ALTER table claim CHANGE DateService DateService date NOT NULL default '0001-01-01'"
					,"ALTER table claim CHANGE DateSent DateSent date NOT NULL default '0001-01-01'"
					,"ALTER table claim CHANGE DateReceived DateReceived date NOT NULL default '0001-01-01'"
					,"ALTER table claim CHANGE PriorDate PriorDate date NOT NULL default '0001-01-01'"
					,"ALTER table claim CHANGE AccidentDate AccidentDate date NOT NULL default '0001-01-01'"
					,"ALTER table claim CHANGE OrthoDate OrthoDate date NOT NULL default '0001-01-01'"
					,"ALTER table claimpayment CHANGE CheckDate CheckDate date NOT NULL default '0001-01-01'"
					,"ALTER table claimproc CHANGE DateCP DateCP date NOT NULL default '0001-01-01'"
					,"ALTER table claimproc CHANGE ProcDate ProcDate date NOT NULL default '0001-01-01'"
					,"ALTER table insplan CHANGE DateEffective DateEffective date NOT NULL default '0001-01-01'"
					,"ALTER table insplan CHANGE DateTerm DateTerm date NOT NULL default '0001-01-01'"
					,"ALTER table insplan CHANGE RenewMonth RenewMonth tinyint unsigned NOT NULL default '1'"
					,"ALTER table patient CHANGE Birthdate Birthdate date NOT NULL default '0001-01-01'"
					,"ALTER table patient CHANGE DateFirstVisit DateFirstVisit date NOT NULL default '0001-01-01'"
					,"ALTER table procedurelog CHANGE ProcDate ProcDate date NOT NULL default '0001-01-01'"
					,"ALTER table procedurelog CHANGE DateOriginalProsth DateOriginalProsth "
						+"date NOT NULL default '0001-01-01'"
					,"ALTER table procedurelog CHANGE DateLocked DateLocked date NOT NULL default '0001-01-01'"
					,"ALTER table recall CHANGE DateDueCalc DateDueCalc date NOT NULL default '0001-01-01'"
					,"ALTER table recall CHANGE DateDue DateDue date NOT NULL default '0001-01-01'"
					,"ALTER table recall CHANGE DatePrevious DatePrevious date NOT NULL default '0001-01-01'"
					//Set prosth codes
					,"UPDATE procedurecode SET IsProsth=1 WHERE ADACode='D2740' || ADACode='D2750' "
						+"|| ADACode='D2751' || ADACode='D2752' || ADACode='D2790' || ADACode='D2791' "
						+"|| ADACode='D2792' || ADACode='D5110' || ADACode='D5120' || ADACode='D5130' "
						+"|| ADACode='D5140' || ADACode='D5211' || ADACode='D5212' || ADACode='D5213' "
						+"|| ADACode='D5214' || ADACode='D5225' || ADACode='D5226' || ADACode='D5281'"
						+"|| ADACode='D5810' || ADACode='D5811' || ADACode='D5820' || ADACode='D5821'"
						+"|| ADACode LIKE 'D62%' || ADACode LIKE 'D65%' || ADACode LIKE 'D66%' "
						+"|| ADACode LIKE 'D67%'"
					//add new ada codes
					//,"INSERT INTO procedurecode "
					,"INSERT INTO definition(Category,ItemOrder,ItemName,ItemValue,ItemColor,IsHidden) "
						+"VALUES ('21','7','Commlog Appt Related','','-886','0')"
					,"UPDATE preference SET ValueString = '3.1.3.0' WHERE PrefName = 'DataBaseVersion'"
				};
				DataConnection dcon=new DataConnection();
				dcon.NonQ(commands);
			}
			To3_1_4();
		}

		private void To3_1_4(){
			if(FromVersion < new Version("3.1.4.0")){
				string[] commands=new string[]
				{
					"ALTER table clearinghouse ADD LoginID varchar(255) NOT NULL"
					,"UPDATE clearinghouse SET ReceiverID='0135WCH00' WHERE ReceiverID='WebMD'"
					,"ALTER table provider ADD OutlineColor int NOT NULL"
					,"UPDATE provider SET OutlineColor ='-11711155'"
					,"UPDATE preference SET ValueString = '3.1.4.0' WHERE PrefName = 'DataBaseVersion'"
				};
				DataConnection dcon=new DataConnection();
				dcon.NonQ(commands);
			}
			To3_1_13();
		}

		private void To3_1_13(){
			if(FromVersion < new Version("3.1.13.0")){
				//get rid of any medication pats where medication no longer exists.
				string command="SELECT medicationpat.MedicationPatNum FROM medicationpat "
					+"LEFT JOIN medication ON medicationpat.MedicationNum=medication.MedicationNum "
					+"WHERE medication.MedicationNum IS NULL";
				DataConnection dcon=new DataConnection();
				DataTable table=dcon.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++){
					command="DELETE FROM medicationpat WHERE MedicationPatNum="
						+table.Rows[i][0].ToString();
					dcon.NonQ(command);
				}
				string[] commands=new string[]
				{
					"INSERT INTO clearinghouse(Description,ExportPath,IsDefault,Payors,Eformat,ReceiverID,"
						+"SenderID,Password,ResponsePath,CommBridge,ClientProgram) "
						+@"VALUES('RECS','C:\\Recscom\\','0','','1','RECS','','',"
						+@"'','5','C:\\Recscom\\Recscom.exe')"
					,"UPDATE preference SET ValueString = '3.1.13.0' WHERE PrefName = 'DataBaseVersion'"
				};
				
				dcon.NonQ(commands);
			}
			To3_1_16();
		}

		private void To3_1_16(){
			if(FromVersion < new Version("3.1.16.0")){
				//this functionality is all copied directly from the Check Database tool.
				string command=@"SELECT PatNum FROM patient
					LEFT JOIN insplan on patient.PriPlanNum=insplan.PlanNum
					WHERE patient.PriPlanNum != 0
					AND insplan.PlanNum IS NULL";
				DataConnection dcon=new DataConnection();
				DataTable table=dcon.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++){
					command="UPDATE patient set PriPlanNum=0 "
						+"WHERE PatNum="+table.Rows[i][0].ToString();
					dcon.NonQ(command);
				}
				command=@"SELECT ClaimProcNum FROM claimproc
					LEFT JOIN insplan ON claimproc.PlanNum=insplan.PlanNum
					WHERE insplan.PlanNum IS NULL";
				table=dcon.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++){
					command="DELETE FROM claimproc "
						+"WHERE ClaimProcNum="+table.Rows[i][0].ToString();
					dcon.NonQ(command);
				}
				command=@"SELECT ClaimNum FROM claim
					LEFT JOIN insplan ON claim.PlanNum=insplan.PlanNum
					WHERE insplan.PlanNum IS NULL";
				table=dcon.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++){
					command="DELETE FROM claim "
						+"WHERE ClaimNum="+table.Rows[i][0].ToString();
					dcon.NonQ(command);
				}
				command="UPDATE preference SET ValueString = '3.1.16.0' WHERE PrefName = 'DataBaseVersion'";
				dcon.NonQ(command);
			}
			To3_4_0();
		}

		private void To3_4_0(){
			if(FromVersion < new Version("3.4.0.0")){
				ExecuteFile(@"ConversionFiles\convert_3_4_0.txt");//Might throw an exception which we handle.
				//----------------Copy payment dates into paysplits--------------------------------------
				string command="SELECT paysplit.SplitNum,payment.PayDate FROM payment,paysplit "
					+"WHERE payment.PayNum=paysplit.PayNum";
				DataConnection dcon=new DataConnection();
				DataTable table=dcon.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++){
					command="UPDATE paysplit SET "
						+"DatePay='"+POut.PDate (PIn.PDate (table.Rows[i][1].ToString()))+"' "
						+"WHERE SplitNum="+table.Rows[i][0].ToString();
					dcon.NonQ(command);
				}
				//----------------Convert all discounts to adjustments-----------------------------------
				//add adjustment categories.
				command="SELECT Max(ItemOrder) FROM definition WHERE Category=1";
				table=dcon.GetTable(command);
				int firstItemOrder=PIn.PInt(table.Rows[0][0].ToString())+1;
				command="SELECT * FROM definition WHERE Category=15 ORDER BY ItemOrder";//cat=DiscountTypes
				table=dcon.GetTable(command);
				Hashtable HDiscToAdj=new Hashtable();//key=original defNum(discountType. value=new defNum(AdjType)
				for(int i=0;i<table.Rows.Count;i++){
					command="INSERT INTO definition (category,itemorder,itemname,itemvalue,ishidden) VALUES("
					+"1, "//category=AdjTypes
					+"'"+POut.PInt   (firstItemOrder+i)+"', "//itemOrder
					+"'"+POut.PString(PIn.PString(table.Rows[i][3].ToString()))+"', "//item name
					+"'-', "//itemValue. All discounts are negative
					+"'"+table.Rows[i][6].ToString()+"')";//is hidden
					dcon.NonQ(command,true);
					HDiscToAdj.Add(PIn.PInt(table.Rows[i][0].ToString()),//defNum of disc
						dcon.InsertID);//defNum of adj
				}
				//handle 0:
				HDiscToAdj.Add(0,dcon.InsertID);
				//create new adjustments from existing discounts
				command="SELECT * FROM paysplit WHERE IsDiscount=1";//0=SplitNum,1=SplitAmt,2=PatNum,3=ProcDate,
					//4=PayNum,5=IsDiscount,6=DiscountType,7=ProvNum,8=PayPlanNum,9=DatePay
				table=dcon.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++){
					command="INSERT INTO adjustment (AdjDate,AdjAmt,PatNum, "
					+"AdjType,ProvNum,ProcDate) "//AdjNote
					+"VALUES("
					+"'"+POut.PDate  (PIn.PDate  (table.Rows[i][9].ToString()))+"', "//entryDate
					+"'"+POut.PDouble(-PIn.PDouble(table.Rows[i][1].ToString()))+"', "//amt
					+"'"+POut.PInt   (PIn.PInt   (table.Rows[i][2].ToString()))+"', "//patNum
					+"'"+POut.PInt   ((int)HDiscToAdj[PIn.PInt(table.Rows[i][6].ToString())])+"', "//type
					+"'"+POut.PInt   (PIn.PInt   (table.Rows[i][7].ToString()))+"', "//provNum
					+"'"+POut.PDate  (PIn.PDate  (table.Rows[i][3].ToString()))+"')";//procDate
					//note
					dcon.NonQ(command);
				}
				command="DELETE FROM paysplit WHERE IsDiscount=1";
				dcon.NonQ(command);
				//--------------------Printers----------------------------------------------------------
				command="SELECT * FROM computer WHERE PrinterName != ''";
				table=dcon.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++){
					command="INSERT INTO printer (ComputerNum,PrintSit,PrinterName,"
					+"DisplayPrompt) "
					+"VALUES("
					+"'"+POut.PInt   (PIn.PInt   (table.Rows[i][0].ToString()))+"', "
					+"'"+POut.PInt   ((int)PrintSituation.Default)+"', "
					+"'"+POut.PString(PIn.PString(table.Rows[i][2].ToString()))+"', "
					+"'1')";
					dcon.NonQ(command);
				}
				command="UPDATE computer SET PrinterName = ''";
				dcon.NonQ(command);
				//HouseCalls link-----------------------------------------------------------------------
				command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
					+") VALUES("
					+"'HouseCalls', "
					+"'HouseCalls from www.housecallsweb.com', "
					+"'0', "
					+"'', "
					+"'', "
					+"'"+POut.PString(@"Typical Export Path is C:\HouseCalls\")+"')";
				dcon=new DataConnection();
				dcon.NonQ(command,true);
				int programNum=dcon.InsertID;//we now have a ProgramNum to work with
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
					+") VALUES("
					+"'"+programNum.ToString()+"', "
					+"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
					+"'0')";
				dcon.NonQ(command);
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
					+") VALUES("
					+"'"+programNum.ToString()+"', "
					+"'Export Path', "
					+"'"+POut.PString(@"C:\HouseCalls\")+"')";
				dcon.NonQ(command);
				command="INSERT INTO toolbutitem (ProgramNum,ToolBar,ButtonText) "
					+"VALUES ("
					+"'"+programNum.ToString()+"', "
					+"'"+((int)ToolBarsAvail.ChartModule).ToString()+"', "
					+"'HouseCalls')";
				dcon.NonQ(command);
				//Final cleanup-------------------------------------------------------------------------
				command="UPDATE preference SET ValueString = '3.4.0.0' WHERE PrefName = 'DataBaseVersion'";
				dcon.NonQ(command);
			}
			To3_4_7();
		}
		
		private void To3_4_7(){
			if(FromVersion < new Version("3.4.7.0")){
				string[] commands=new string[]
				{
					"INSERT INTO clearinghouse(Description,ExportPath,IsDefault,Payors,Eformat,ReceiverID,"
						+"SenderID,Password,ResponsePath,CommBridge,ClientProgram) "
						+@"VALUES('WebClaim','C:\\WebClaim\\Upload\\','0','','1','330989922','','',"
						+@"'','4','')"
					,"UPDATE preference SET ValueString = '3.4.7.0' WHERE PrefName = 'DataBaseVersion'"
				};
				DataConnection dcon=new DataConnection();
				dcon.NonQ(commands);
			}
			To3_4_10();
		}

		private void To3_4_10(){
			//the only purpose of this is to check the bug fix in conversions
			if(FromVersion < new Version("3.4.10.0")){
				string[] commands=new string[]
				{
					"UPDATE preference SET ValueString = '3.4.10.0' WHERE PrefName = 'DataBaseVersion'"
				};
				DataConnection dcon=new DataConnection();
				dcon.NonQ(commands);
			}
			To3_4_11();
		}

		private void To3_4_11(){
			if(FromVersion < new Version("3.4.11.0")){
				//Planmeca link-----------------------------------------------------------------------
				string command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
					+") VALUES("
					+"'Planmeca', "
					+"'Dimaxis from Planmeca', "
					+"'0', "
					+"'DxStart.exe', "
					+"'', "
					+"'"+POut.PString(@"Typical file path is DxStart.exe which is available from Planmeca and should be placed in the same folder as this program.")+"')";
				DataConnection dcon=new DataConnection();
				dcon.NonQ(command,true);
				int programNum=dcon.InsertID;//we now have a ProgramNum to work with
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
					+") VALUES("
					+"'"+programNum.ToString()+"', "
					+"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
					+"'0')";
				dcon.NonQ(command);
				command="INSERT INTO toolbutitem (ProgramNum,ToolBar,ButtonText) "
					+"VALUES ("
					+"'"+programNum.ToString()+"', "
					+"'"+((int)ToolBarsAvail.ChartModule).ToString()+"', "
					+"'Planmeca')";
				dcon.NonQ(command);
				command=
					"UPDATE preference SET ValueString = '3.4.11.0' WHERE PrefName = 'DataBaseVersion'";
				dcon.NonQ(command);
			}
			To3_4_16();
		}

		private void To3_4_16(){
			if(FromVersion < new Version("3.4.16.0")){
				DataConnection dcon=new DataConnection();
				string[] commands=new string[]
				{
					@"UPDATE clearinghouse SET Description='ClaimConnect',ExportPath='C:\\ClaimConnect\\Upload\\' WHERE Description='WebClaim'"
					,"UPDATE preference SET ValueString = '3.4.16.0' WHERE PrefName = 'DataBaseVersion'"
				};
				dcon.NonQ(commands);
			}
			To3_4_17();
		}

		private void To3_4_17(){
			if(FromVersion < new Version("3.4.17.0")){
				DataConnection dcon=new DataConnection();
				string[] commands=new string[]
				{
					"UPDATE patient SET DateFirstVisit='0001-01-01' WHERE DateFirstVisit='0000-00-00'"
					,"UPDATE preference SET ValueString = '3.4.17.0' WHERE PrefName = 'DataBaseVersion'"
				};
				dcon.NonQ(commands);
			}
			To3_4_24();
		}

		private void To3_4_24(){
			if(FromVersion < new Version("3.4.24.0")){
				DataConnection dcon=new DataConnection();
				//Delete program links for WebClaim and Renaissance--------------------------------------
				string command="SELECT ProgramNum FROM program WHERE ProgName='WebClaim' OR ProgName='Renaissance'";
				DataTable table=dcon.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++){
					command="DELETE FROM program WHERE ProgramNum="+table.Rows[i][0].ToString();
					dcon.NonQ(command);
					command="DELETE FROM toolbutitem WHERE ProgramNum="+table.Rows[i][0].ToString();
					dcon.NonQ(command);
				}
				//Fix utf8 binary collations for ADACode columns-------------------------------------------
				command="SELECT @@version";
 				table=dcon.GetTable(command);
				string thisVersion=PIn.PString(table.Rows[0][0].ToString());
				string[] commands;
				if(thisVersion.Substring(0,3)=="4.1" || thisVersion.Substring(0,3)=="5.0"){
					commands=new string[]
					{
						"ALTER TABLE procedurecode CHANGE ADACode ADACode varchar(15) character set utf8 collate utf8_bin NOT NULL"
						,"ALTER TABLE procedurecode DEFAULT character set utf8"
						,"ALTER TABLE procedurecode MODIFY Descript varchar(255) character set utf8 NOT NULL"
						,"ALTER TABLE procedurecode MODIFY AbbrDesc varchar(50) character set utf8 NOT NULL"
						,"ALTER TABLE procedurecode MODIFY ProcTime varchar(24) character set utf8 NOT NULL"
						,"ALTER TABLE procedurecode MODIFY DefaultNote text character set utf8 NOT NULL"
						,"ALTER TABLE procedurecode MODIFY AlternateCode1 varchar(15) character set utf8 NOT NULL"
						,"ALTER TABLE procedurelog MODIFY ADACode varchar(15) character set utf8 collate utf8_bin NOT NULL"
						,"ALTER TABLE autocodeitem MODIFY ADACode varchar(15) character set utf8 collate utf8_bin NOT NULL"
						,"ALTER TABLE procbuttonitem MODIFY ADACode varchar(15) character set utf8 collate utf8_bin NOT NULL"
						,"ALTER TABLE covspan MODIFY FromCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
						,"ALTER TABLE covspan MODIFY ToCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
					};
					dcon.NonQ(commands);
				}
				commands=new string[]
				{
				//Inmediata clearinghouse--------------------------------------------------------------
					"INSERT INTO clearinghouse(Description,ExportPath,IsDefault,Payors,Eformat,ReceiverID,"
						+"SenderID,Password,ResponsePath,CommBridge,ClientProgram) "
						+@"VALUES('Inmediata Health Group Corp','C:\\Inmediata\\Claims\\','0','','1','660610220','','',"
						+@"'C:\\Inmediata\\Reports\\','6','C:\\Program Files\\Inmediata\\IMPlug.exe')"
					,"UPDATE preference SET ValueString = '3.4.24.0' WHERE PrefName = 'DataBaseVersion'"
				};
				dcon.NonQ(commands);
			}
			To3_4_30();
		}

		private void To3_4_30(){
			if(FromVersion < new Version("3.4.30.0")){
				string[] commands=new string[]
				{
					"DELETE FROM schedule WHERE Status=0 AND StartTime=StopTime"
					,"UPDATE preference SET ValueString = '3.4.30.0' WHERE PrefName = 'DataBaseVersion'"
				};
				DataConnection dcon=new DataConnection();
				dcon.NonQ(commands);
			}
			//To3_4_30();
		}




	}
}







