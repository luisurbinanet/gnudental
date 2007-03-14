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

		///<summary></summary>
		public bool Convert(string fromVersion){//return false to indicate exit app.
			//Conversions=new Conversions();
			FromVersion=new Version(fromVersion);
			ToVersion=new Version(Application.ProductVersion);
			if(FromVersion.CompareTo(ToVersion)>0){
				MessageBox.Show("Can not convert database to an older version.");
				return false;
			}
			if(FromVersion < new Version("1.0.0")
				|| FromVersion.ToString()=="2.0.1.0"
				|| FromVersion.ToString()=="2.1.0.0"
				|| FromVersion.ToString()=="2.1.1.0"
				|| FromVersion.ToString()=="2.5.0.0"){
				MessageBox.Show("Can not convert database version "+FromVersion.ToString()
					+" which was only for development purposes.");
				return false;
			}
			if(FromVersion < new Version("2.8.14.0")){
				if(MessageBox.Show("Your database will now be converted from version "
					+FromVersion.ToString()+" to version "+ToVersion.ToString()
					+". Please be certain you have a current backup.  "
					+"The conversion works best if you are on the server.  "
					+"Depending on the speed of your computer, it can be as fast as a few seconds, "
					+"or it can take as long as 10 minutes. ","",MessageBoxButtons.OKCancel)
					!=DialogResult.OK){
					return false;
				}
			}
			else{
				return true;//no conversion necessary
			}
			MakeABackup();
			if(To1_0_1()){//begins going through the chain of conversion steps, each returning true
				MessageBox.Show("Conversion successful");
				Prefs.Refresh();
				return true;
			}
			else{
				return false;
			}
		}

		///<summary>Backs up the database to the same directory as the original just in case the user did not have sense enough to do a backup first.</summary>
		private void MakeABackup(){
			try{
				string newDb="opendentalbackup"+DateTime.Today.ToString("MMddyyyy");
				string oldDb=FormConfig.Database;
				Conversions.NonQString="CREATE DATABASE "+newDb;
				if(!Conversions.SubmitNonQString()) throw new Exception("Could not create database "+newDb);
				Conversions.SelectText="SHOW TABLES";
				if(!Conversions.SubmitSelect()) throw new Exception("Could not show tables");
				string[] tableName=new string[Conversions.TableQ.Rows.Count];
				for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
					tableName[i]=Conversions.TableQ.Rows[i][0].ToString();
				}
				//switch to using the new database
				DataClass.SetConnection(newDb);
				for(int i=0;i<tableName.Length;i++){
					Conversions.SelectText="SHOW CREATE TABLE "+oldDb+"."+tableName[i];
					if(!Conversions.SubmitSelect()) throw new Exception("Could not show create table.");
					Conversions.NonQString=Conversions.TableQ.Rows[0][1].ToString();
					if(!Conversions.SubmitNonQString()) throw new Exception(Conversions.NonQString);
					Conversions.NonQString="INSERT INTO "+newDb+"."+tableName[i]
						+" SELECT * FROM "+oldDb+"."+tableName[i];
					if(!Conversions.SubmitNonQString()) throw new Exception(Conversions.NonQString);
				}
			}
			catch(Exception e){
				if(e.Message!=""){
					//MessageBox.Show(e.Message);
				}
				MessageBox.Show("You can disregard the previous error message.  It came up simply because the automated backup had already been run today.  The conversion will now begin.");
			}
			finally{
				//MessageBox.Show("Backup done");
				//go back to the current db
				DataClass.SetConnection();
			}
		}

		private bool To1_0_1(){//returns true if successful
			if(FromVersion < new Version("1.0.1")){
				try{
					Conversions.NonQArray=new string[] 
						{"INSERT INTO preference (PrefName,ValueString) "
						+"VALUES('TreatmentPlanNote','If you have dental insurance, please be aware that THIS IS AN ESTIMATE ONLY.  Coverage may be different if your deductible has not been met, annual maximum has been met, or if your coverage table is lower than average.')"
						,"ALTER TABLE patient CHANGE Balance EstBalance DOUBLE DEFAULT '0' NOT NULL" 
						,"UPDATE preference SET ValueString = '1.0.1.0' WHERE PrefName = 'DataBaseVersion'"}; 
					if(!Conversions.SubmitNonQArray()) return false;
				}
				catch{
					return false;
				}
			}
			return To1_0_11();
		}

		private bool To1_0_11(){
			if(FromVersion < new Version("1.0.11")){
				try{
					Conversions.NonQArray=new string[] 
						{"ALTER TABLE patient ADD MedicaidID VARCHAR(20) NOT NULL"
						,"UPDATE preference SET ValueString = '1.0.11' WHERE PrefName = 'DataBaseVersion'"}; 
					if(!Conversions.SubmitNonQArray()) return false;
				}
				catch{
					return false;
				}
			}
			return To2_0_2();
		}

		private bool To2_0_2(){
			if(FromVersion < new Version("2.0.2")){
				if(!File.Exists(@"ConversionFiles\convert_2_0_2.txt")){
					MessageBox.Show("convert_2_0_2.txt could not be found.");
					return false;
				}
				StreamReader sr;
				string line="";
				string cmd="";
				ArrayList AL=new ArrayList();
				try{
					sr=new StreamReader(@"ConversionFiles\convert_2_0_2.txt");
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
					AL.Add("UPDATE preference SET ValueString = '2.0.2' WHERE PrefName = 'DataBaseVersion';"); 
					Conversions.NonQArray=new string[AL.Count];
					AL.CopyTo(Conversions.NonQArray);
					//MessageBox.Show(AL.Count.ToString());
					//for(int i=30;i<Conversions.ArrayQueryText.Length;i++){
					//	MessageBox.Show(Conversions.ArrayQueryText[i]);
					//}
					if(!Conversions.SubmitNonQArray()){
						sr.Close();
						return false;
					}
					sr.Close();
					//Add a finance charge type to adjustments:
					Defs.Refresh();
					Defs.Cur=new Def();
					Defs.Cur.Category=DefCat.AdjTypes;
					Defs.Cur.ItemName="Finance Charge";
					Defs.Cur.ItemOrder=Defs.Long[(int)DefCat.AdjTypes].Length;
					Defs.Cur.ItemValue="+";
					Defs.InsertCur();
					Prefs.Cur=new Pref();
					Prefs.Cur.PrefName="FinanceChargeAdjustmentType";
					Prefs.Cur.ValueString=Defs.Cur.DefNum.ToString();
					Prefs.UpdateCur();
					//Copy address notes to each family member
					Conversions.AddressNotesVers2_0();
				}//try
				catch{
					return false;
				}
			}//if
			return To2_1_2();
		}

		/// <summary>Takes a text file with a series of SQL commands, and sends them as queries to the database.
		/// Used in version upgrades and in the function that downloads and installs the latest translations.
		/// </summary>
		/// <param name="fileName">The filename is always relative to the application directory.</param>
		/// <returns>True if executed successfully.  False if an error.</returns>
		public bool ExecuteFile(string fileName){
			//also used in the function that downloads and installs the latests translations.
			if(!File.Exists(fileName)){
				MessageBox.Show(fileName+" could not be found.");
				return false;
			}
			StreamReader sr;
			string line="";
			string cmd="";
			ArrayList AL=new ArrayList();
			try{
				sr=new StreamReader(fileName);
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
				if(!Conversions.SubmitNonQArray()){
					sr.Close();
					return false;
				}
				sr.Close();
			}
			catch{
				return false;
			}
			return true;
		}

		private bool To2_1_2(){
			if(FromVersion < new Version("2.1.2")){
				try{
					Conversions.NonQArray=new string[]{
						"UPDATE insplan SET claimformat = '1' WHERE claimformat = '43'"
						,"UPDATE insplan SET claimformat = '0' WHERE claimformat = '44'"
						,"ALTER TABLE insplan CHANGE claimformat NoSendElect TINYINT(1) unsigned NOT NULL default '0'"
						,"UPDATE instemplate SET claimformat = '1' WHERE claimformat = '43'"
						,"UPDATE instemplate SET claimformat = '0' WHERE claimformat = '44'"
						,"ALTER TABLE instemplate CHANGE claimformat NoSendElect TINYINT(1) unsigned NOT NULL default '0'"
						,"ALTER TABLE insplan ADD PlanType char(1) NOT NULL"
						,"UPDATE insplan SET plantype = 'I'"//start out all as Insurance
						,"ALTER TABLE instemplate ADD PlanType char(1) NOT NULL"
						,"UPDATE instemplate SET plantype = 'I'"//start out all as Insurance
						,"ALTER TABLE insplan ADD ClaimFormNum smallint(5) unsigned NOT NULL default '0'"
						,"UPDATE insplan SET ClaimFormNum = '1'"//set all to ADAform
						,"ALTER TABLE instemplate ADD ClaimFormNum smallint(5) unsigned NOT NULL default '0'"
						,"UPDATE instemplate SET ClaimFormNum = '1'"//set all to ADAform
						,"ALTER TABLE insplan ADD UseAltCode tinyint(1) unsigned NOT NULL default '0'"
						,"ALTER TABLE instemplate ADD UseAltCode tinyint(1) unsigned NOT NULL default '0'"
						,"ALTER TABLE insplan ADD ClaimsUseUCR tinyint(1) unsigned NOT NULL default '0'"
						,"ALTER TABLE instemplate ADD ClaimsUseUCR tinyint(1) unsigned NOT NULL default '0'"
						,"ALTER TABLE instemplate ADD FeeSched smallint(5) unsigned NOT NULL default '0'"
						,"ALTER TABLE instemplate ADD IsWrittenOff tinyint(1) unsigned NOT NULL default '0'"
						,"ALTER TABLE instemplate ADD CopayFeeSched smallint(5) unsigned NOT NULL default '0'"
						,"ALTER TABLE insplan ADD IsWrittenOff tinyint(1) unsigned NOT NULL default '0'"
						,"ALTER TABLE insplan ADD CopayFeeSched smallint(5) unsigned NOT NULL default '0'"
						,"ALTER TABLE insplan ADD SubscriberID varchar(40) NOT NULL"
						,"ALTER TABLE procedurecode ADD AlternateCode1 varchar(15) NOT NULL"
						,"ALTER TABLE claim ADD PlanNum2 mediumint(8) unsigned NOT NULL default '0'"
						,"ALTER TABLE claim ADD PatRelat2 tinyint(3) unsigned NOT NULL default '0'"
						,"ALTER TABLE claim ADD WriteOff double NOT NULL default '0'"
						,"ALTER TABLE claimproc CHANGE ClaimProcNum ClaimProcNum MEDIUMINT(8) unsigned NOT NULL AUTO_INCREMENT"
						,"ALTER TABLE claimproc CHANGE ProcNum ProcNum MEDIUMINT(8) unsigned NOT NULL"
						,"ALTER TABLE claimproc CHANGE ClaimNum ClaimNum MEDIUMINT(8) unsigned NOT NULL"
						,"ALTER TABLE claimproc CHANGE PatNum PatNum MEDIUMINT(8) unsigned NOT NULL"
						,"ALTER TABLE claimproc ADD ProvNum smallint(5) unsigned NOT NULL default '0'"
						,"ALTER TABLE claimproc ADD FeeBilled double NOT NULL default '0'"
						,"ALTER TABLE claimproc ADD InsPayEst double NOT NULL default '0'"
						,"ALTER TABLE claimproc ADD DedApplied double NOT NULL default '0'"
						,"ALTER TABLE claimproc ADD Status tinyint(3) unsigned NOT NULL default '0'"
						,"ALTER TABLE claimproc ADD InsPayAmt double NOT NULL default '0'"
						,"ALTER TABLE claimproc ADD Remarks varchar(255) NOT NULL"
						,"ALTER TABLE claimproc ADD ClaimPaymentNum mediumint(8) unsigned NOT NULL default '0'"
						,"ALTER TABLE claimproc ADD PlanNum mediumint(8) unsigned NOT NULL default '0'"
						,"ALTER TABLE claimproc ADD DateCP date NOT NULL default '0000-00-00'"
						,"ALTER TABLE claimproc ADD WriteOff double NOT NULL default '0'"
						,"ALTER TABLE claimproc ADD CodeSent varchar(15) NOT NULL"
						,"ALTER TABLE claimproc ADD INDEX indexPatNum (PatNum)"
						,"ALTER TABLE claimproc ADD INDEX indexPlanNum (PlanNum)"
						,"ALTER TABLE claimproc ADD INDEX indexClaimNum (ClaimNum)"
						,"ALTER TABLE claimproc ADD INDEX indexProvNum (ProvNum)"
						,"ALTER TABLE patient ADD BalTotal double NOT NULL default '0'"
						,"UPDATE claimproc SET Status = 'P'"//set all existing to preauth
						,"UPDATE claim SET ClaimType = 'P' WHERE ClaimType = '' && PriClaimNum = ClaimNum"
						,"UPDATE claim SET ClaimType = 'S' WHERE ClaimType = '' && SecClaimNum = ClaimNum"
						//the next line is a safety net in case of slight corruption. Can NEVER have empty claimtype.
						,"UPDATE claim SET ClaimType = 'S' WHERE ClaimType = ''"
						//make sure all patients have a primary provider
						,"UPDATE patient SET PriProv='"+((Pref)Prefs.HList["PracticeDefaultProv"]).ValueString
							+"' WHERE PriProv='0'"
					}; 
					if(!Conversions.SubmitNonQArray()) return false;
					//copy SSN into each insplan.subscriberID.
					Conversions.SelectText="SELECT patient.ssn,insplan.plannum "
						+"FROM patient,insplan WHERE patient.patnum = insplan.subscriber";
					if(!Conversions.SubmitSelect()) return false;
					for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
						Conversions.NonQArray=new string[]{
							"UPDATE insplan SET subscriberid = '"
							+POut.PString(Conversions.TableQ.Rows[i][0].ToString())+"'"
							+" WHERE plannum = '"+Conversions.TableQ.Rows[i][1].ToString()+"'"
						};
						if(!Conversions.SubmitNonQArray()) return false;
					}
					//fix any adjustments without provnum
					Conversions.SelectText="SELECT patient.priprov,adjustment.adjnum "
						+"FROM patient,adjustment WHERE patient.patnum = adjustment.patnum "
						+"&& adjustment.provnum = '0'";
					if(!Conversions.SubmitSelect()) return false;
					for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
						Conversions.NonQArray=new string[]{
							"UPDATE adjustment SET provnum = '"
							+Conversions.TableQ.Rows[i][0].ToString()+"'"
							+" WHERE adjnum = '"+Conversions.TableQ.Rows[i][1].ToString()+"'"
						};
						if(!Conversions.SubmitNonQArray()) return false;
					}
					//fix any procedures without provnum
					Conversions.SelectText="SELECT patient.priprov,procedurelog.procnum "
						+"FROM patient,procedurelog WHERE patient.patnum = procedurelog.patnum "
						+"&& procedurelog.provnum = '0'";
					if(!Conversions.SubmitSelect()) return false;
					for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
						Conversions.NonQArray=new string[]{
							"UPDATE procedurelog SET provnum = '"
							+Conversions.TableQ.Rows[i][0].ToString()+"'"
							+" WHERE procnum = '"+Conversions.TableQ.Rows[i][1].ToString()+"'"
						};
						if(!Conversions.SubmitNonQArray()) return false;
					}
					//fix all appointments with status of 0. Move them to unsched list.
					Conversions.SelectText="SELECT appointment.aptnum from patient,appointment "
						+"WHERE patient.patnum = appointment.patnum "
						+"&& patient.nextaptnum != appointment.aptnum "
						+"&& appointment.aptstatus = '0'";
					if(!Conversions.SubmitSelect()) return false;
					for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
						Conversions.NonQArray=new string[]{
							"UPDATE appointment SET aptstatus = '3'"
							+" WHERE aptnum = '"+Conversions.TableQ.Rows[i][0].ToString()+"'"
						};
						if(!Conversions.SubmitNonQArray()) return false;
					}
					//now, get all claims
					Conversions.SelectText="SELECT claim.claimnum,claim.patnum,claim.provtreat,claim.claimfee"//3
						+",claim.inspayest,claim.inspayamt,claim.claimpaymentnum,claim.dedapplied"//7
						+",claim.claimtype,claim.claimstatus,claim.plannum,claim.secclaimnum,claim.dateservice"//12
						+",claim.priclaimnum,patient.prirelationship,patient.secrelationship"//15
						+" FROM claim,patient"// WHERE ClaimType != 'PreAuth'";
						+" WHERE claim.patnum = patient.patnum";
					if(!Conversions.SubmitSelect()) return false;
					DataTable claimTable=Conversions.TableQ.Copy();
					//DataTable procTable;
					for(int i=0;i<claimTable.Rows.Count;i++){//loop through all claims
						//if adjustment:
						if(PIn.PString(claimTable.Rows[i][9].ToString())=="A"){//if status=A
							ClaimProcs.Cur=new ClaimProc();
							ClaimProcs.Cur.PatNum    =PIn.PInt   (claimTable.Rows[i][1].ToString());
							ClaimProcs.Cur.DedApplied=PIn.PDouble(claimTable.Rows[i][7].ToString());
							ClaimProcs.Cur.Status    =ClaimProcStatus.Adjustment;
							ClaimProcs.Cur.InsPayAmt =PIn.PDouble(claimTable.Rows[i][5].ToString());
							ClaimProcs.Cur.PlanNum   =PIn.PInt   (claimTable.Rows[i][10].ToString());
							ClaimProcs.Cur.DateCP    =PIn.PDate  (claimTable.Rows[i][12].ToString());
							ClaimProcs.InsertCur();
							//claim will be deleted later in batch command.
							continue;
						}
						//if not adjustment, then select all procs for this claim and create a claimproc for each
						if(PIn.PString(claimTable.Rows[i][8].ToString())=="PreAuth"){//if type=PreAuth
							//get all claim procs and fill in the missing fields
							Conversions.SelectText="SELECT claimproc.claimprocnum,claimproc.procnum"
								+",claimproc.claimnum"
								+",claimproc.patnum,procedurelog.procfee,procedurelog.adacode"
								+" FROM claimproc,procedurelog"
								+" WHERE claimproc.claimnum = '"+claimTable.Rows[i][0].ToString()+"'"
								+" && claimproc.procnum = procedurelog.procnum";
							if(!Conversions.SubmitSelect()) return false;
							for(int j=0;j<Conversions.TableQ.Rows.Count;j++){//loop through claimprocs
								ClaimProcs.Cur=new ClaimProc();
								ClaimProcs.Cur.ClaimProcNum =PIn.PInt   (Conversions.TableQ.Rows[j][0].ToString());
								ClaimProcs.Cur.ProcNum      =PIn.PInt   (Conversions.TableQ.Rows[j][1].ToString());
								ClaimProcs.Cur.ClaimNum     =PIn.PInt   (Conversions.TableQ.Rows[j][2].ToString());
								ClaimProcs.Cur.PatNum       =PIn.PInt   (Conversions.TableQ.Rows[j][3].ToString());
								//new fields:
								ClaimProcs.Cur.ProvNum      =PIn.PInt   (claimTable.Rows[i][2].ToString());
								ClaimProcs.Cur.FeeBilled    =PIn.PDouble(Conversions.TableQ.Rows[j][4].ToString());
								//inspayest
								//dedapplied
								ClaimProcs.Cur.Status       =ClaimProcStatus.Preauth;
													//check the claimstatus to determine if rec'd
								ClaimProcs.Cur.InsPayAmt    =PIn.PDouble(claimTable.Rows[i][5].ToString());
								//remarks
								//claimpaymentnum
								ClaimProcs.Cur.PlanNum      =PIn.PInt   (claimTable.Rows[i][10].ToString());
								ClaimProcs.Cur.DateCP       =PIn.PDate  (claimTable.Rows[i][12].ToString());
								//writeoff
								ClaimProcs.Cur.CodeSent     =PIn.PString(Conversions.TableQ.Rows[j][5].ToString());
								ClaimProcs.UpdateCur();
							}//loop procs
							continue;//don't need financial claimproc, so continue to next claim
						}
						//fill in relat for this claim
						Relat thisRelat;
						if(PIn.PString(claimTable.Rows[i][8].ToString())=="P"){
							thisRelat=(Relat)PIn.PInt(claimTable.Rows[i][14].ToString());
						}
						else{//secondary
							thisRelat=(Relat)PIn.PInt(claimTable.Rows[i][15].ToString());
						}
						Conversions.NonQArray=new string[]{
							"UPDATE claim SET patrelat = '"+POut.PInt((int)thisRelat)+"'"
							+" WHERE claimnum = '"+claimTable.Rows[i][0].ToString()+"'"
						};
						if(!Conversions.SubmitNonQArray()) return false;
						//get procedures for claim
						Conversions.SelectText="SELECT procnum,provnum,procfee,adacode FROM procedurelog ";
						if(PIn.PString(claimTable.Rows[i][8].ToString())=="P"){//if type=Pri
							Conversions.SelectText+="WHERE claimnum = '"
								+PIn.PInt(claimTable.Rows[i][0].ToString())+"'";
						}
						else if(PIn.PString(claimTable.Rows[i][8].ToString())=="S"){//if type=Sec
							Conversions.SelectText+="WHERE claimnum = '"
								+PIn.PInt(claimTable.Rows[i][13].ToString())+"'";
						}
						else{
							MessageBox.Show("Unknown claim type: "+PIn.PString(claimTable.Rows[i][8].ToString()));
							return false;
						}
						if(!Conversions.SubmitSelect()) return false;
						for(int j=0;j<Conversions.TableQ.Rows.Count;j++){//loop through procs
							ClaimProcs.Cur=new ClaimProc();
							ClaimProcs.Cur.ProcNum     =PIn.PInt   (Conversions.TableQ.Rows[j][0].ToString());
							ClaimProcs.Cur.ClaimNum    =PIn.PInt   (claimTable.Rows[i][0].ToString());
							ClaimProcs.Cur.PatNum      =PIn.PInt   (claimTable.Rows[i][1].ToString());
							ClaimProcs.Cur.ProvNum     =PIn.PInt   (Conversions.TableQ.Rows[j][1].ToString());
							ClaimProcs.Cur.FeeBilled   =PIn.PDouble(Conversions.TableQ.Rows[j][2].ToString());
							//inspayest: too hard to calculate, so simply add the total to the financial claimproc
							//   It will get deleted when a recalc is performed.
							//dedapplied
							switch(PIn.PString(claimTable.Rows[i][9].ToString())){
								case "U":
								case "H":
								case "W":
								case "P":
								case "S":
									ClaimProcs.Cur.Status=ClaimProcStatus.NotReceived;
									break;
								case "R":
									ClaimProcs.Cur.Status=ClaimProcStatus.Received;
									break;
								//adjustments already handled separately
							}
							//inspayamt
							//remarks
							//claimpaymentnum
							ClaimProcs.Cur.PlanNum =PIn.PInt(claimTable.Rows[i][10].ToString());
							ClaimProcs.Cur.DateCP  =PIn.PDate(claimTable.Rows[i][12].ToString());
							//writeoff
							ClaimProcs.Cur.CodeSent=PIn.PString(Conversions.TableQ.Rows[j][3].ToString());
							ClaimProcs.InsertCur();
						}
						//if(claimTable.Rows[i][9].ToString()!="R"){
						//	continue;//no
						//}
						//Add a claimproc regardless of status.
						//This allows a mechanism to show inspayest even if not received.
						ClaimProcs.Cur=new ClaimProc();
						//ClaimProcs.Cur.ProcNum 
						ClaimProcs.Cur.ClaimNum    =PIn.PInt(claimTable.Rows[i][0].ToString());
						ClaimProcs.Cur.PatNum      =PIn.PInt(claimTable.Rows[i][1].ToString());
						ClaimProcs.Cur.ProvNum     =PIn.PInt(claimTable.Rows[i][2].ToString());
						//ClaimProcs.Cur.FeeBilled   =PIn.PDouble(Conversions.TableQ.Rows[i][2].ToString());
						//see note above regarding usage of inspayest here
						ClaimProcs.Cur.InsPayEst   =PIn.PDouble(claimTable.Rows[i][4].ToString());
						ClaimProcs.Cur.DedApplied  =PIn.PDouble(claimTable.Rows[i][7].ToString());
						switch(PIn.PString(claimTable.Rows[i][9].ToString())){
							case "U":
							case "H":
							case "W":
							case "P":
							case "S":
								ClaimProcs.Cur.Status=ClaimProcStatus.NotReceived;
								break;
							case "R":
								ClaimProcs.Cur.Status=ClaimProcStatus.Received;
								break;
							//adjustments already handled
						}
						//preauths not involved
						ClaimProcs.Cur.InsPayAmt=PIn.PDouble(claimTable.Rows[i][5].ToString());
						//remarks
						ClaimProcs.Cur.ClaimPaymentNum=PIn.PInt(claimTable.Rows[i][6].ToString());
						ClaimProcs.Cur.PlanNum=PIn.PInt(claimTable.Rows[i][10].ToString());
						ClaimProcs.Cur.DateCP=PIn.PDate(claimTable.Rows[i][12].ToString());
						//writeoff
						//codesent
						ClaimProcs.InsertCur();
					}//loop i claimns
					Defs.Cur=new Def();
					Defs.Cur.Category=DefCat.ContactCategories;
					Defs.Cur.ItemName="Pharmacies";
					Defs.Cur.ItemOrder=0;
					Defs.InsertCur();
					Defs.Cur=new Def();
					Defs.Cur.Category=DefCat.ContactCategories;
					Defs.Cur.ItemName="Local Dentists";
					Defs.Cur.ItemOrder=1;
					Defs.InsertCur();
					if(!ExecuteFile(@"ConversionFiles\convert_2_1_2.txt")) return false;
					File.Copy(@"ConversionFiles\DentiCal.jpg"
						,((Pref)Prefs.HList["DocPath"]).ValueString+@"\DentiCal.jpg",true);
					Conversions.NonQArray=new string[]{
						"UPDATE preference SET ValueString = '2.1.2' WHERE PrefName = 'DataBaseVersion'"
					};
					if(!Conversions.SubmitNonQArray()) return false;
				}
				catch{
					return false;
				}
			}//if
			return To2_1_5();
		}

		private bool To2_1_5(){
			if(FromVersion < new Version("2.1.5")){
				try{
					Conversions.NonQArray=new string[]{
						"UPDATE claimformitem SET formatstring = 'MM/dd/yy' WHERE "
							+"claimformitemnum = '280' || claimformitemnum = '286' || claimformitemnum = '292' "
							+"|| claimformitemnum = '298' || claimformitemnum = '304' || claimformitemnum = '310' "
							+"|| claimformitemnum = '316' || claimformitemnum = '322' || claimformitemnum = '328' "
							+"|| claimformitemnum = '334'"
						,"ALTER TABLE procedurecode CHANGE ADACode ADACode VARCHAR(15) BINARY NOT NULL default ''"
						,"UPDATE preference SET ValueString = '2.1.5' WHERE PrefName = 'DataBaseVersion'"
					};
					if(!Conversions.SubmitNonQArray()) return false;
				}
				catch{
					return false;
				}
			}//if
			return To2_5_1();
		}

		private bool To2_5_1(){
			if(FromVersion < new Version("2.5.1")){
				try{
					//first check to see if the conversion file is available
					if(!File.Exists(@"ConversionFiles\convert_2_5_1.txt")){
						MessageBox.Show(@"ConversionFiles\convert_2_5_1.txt"+" could not be found.");
						return false;
					}
					//then, do all file transfers.
					try{
						File.Copy(@"ConversionFiles\DentiCal.jpg"
							,((Pref)Prefs.HList["DocPath"]).ValueString+@"\DentiCal.jpg",true);
						File.Copy(@"ConversionFiles\ADA2000.jpg"
							,((Pref)Prefs.HList["DocPath"]).ValueString+@"\ADA2000.jpg",true);
						File.Copy(@"ConversionFiles\HCFA1500.gif"
							,((Pref)Prefs.HList["DocPath"]).ValueString+@"\HCFA1500.gif",true);
					}
					catch{
						MessageBox.Show("Files could not be copied correctly.");
						return false;
					}
					//set the aptstatus of all the next appointments to Next.
					//and update the dates on all the next appointments
					Conversions.SelectText="SELECT patient.nextaptnum,procedurelog.procdate "
						+"FROM patient "
						+"LEFT JOIN procedurelog ON patient.nextaptnum = procedurelog.nextaptnum "
						+"WHERE patient.nextaptnum > '0' ";
					if(!Conversions.SubmitSelect()) return false;
					for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
						Conversions.NonQString=
							"UPDATE appointment SET aptstatus = '6',"
							+"aptdatetime = '"+POut.PDateT(PIn.PDate(Conversions.TableQ.Rows[i][1].ToString()))+"'"
							+" WHERE aptnum = '"+Conversions.TableQ.Rows[i][0].ToString()+"'";
						//MessageBox.Show(Conversions.ArrayQueryText[0]);
						if(!Conversions.SubmitNonQString()) return false;
					}
					//add various columns, rows, and tables in a script
					//this also adds all new claimform items with dummy foreign keys which will get replaced.
					if(!ExecuteFile(@"ConversionFiles\convert_2_5_1.txt")) return false;
					//get the primary keys for the 3(4) claimforms
					Conversions.SelectText="SELECT ClaimFormNum,UniqueID FROM claimform "
						+"WHERE UniqueID > '0'";
					if(!Conversions.SubmitSelect()) return false;
					//use those primary keys to delete appropriate claimformitems and change foreign keys
					for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
						Conversions.NonQString="DELETE FROM claimformitem"
							+" WHERE ClaimFormNum = '"+Conversions.TableQ.Rows[i][0].ToString()+"'";
						if(!Conversions.SubmitNonQString()) return false;
						Conversions.NonQString="UPDATE claimformitem"
							+" SET ClaimFormNum = '"+Conversions.TableQ.Rows[i][0].ToString()
							+"' WHERE ClaimFormNum = '1000"+Conversions.TableQ.Rows[i][1].ToString()+"'";
						if(!Conversions.SubmitNonQString()) return false;
					}
					//Move all patientnote.ApptPhone entries into CommLog
					Conversions.SelectText="SELECT patnum,apptphone "
						+"FROM patientnote "
						+"WHERE apptphone != ''";
					if(!Conversions.SubmitSelect()) return false;
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
						if(!Conversions.SubmitNonQArray()) return false;
					}
					//Add a procDescript to each appointment
					Conversions.SelectText="SELECT procedurelog.aptnum,procedurecode.abbrdesc "
						+"FROM procedurelog,procedurecode "
						+"WHERE procedurelog.AptNum > '0' AND procedurelog.adacode = procedurecode.adacode "
						+"ORDER BY aptnum";
					if(!Conversions.SubmitSelect()) return false;
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
							"UPDATE appointment SET procdescript = '"+procs+"' "
							+" WHERE aptnum = '"+curAptNum.ToString()+"'"
						};
						//MessageBox.Show(Conversions.ArrayQueryText[0]);
						if(!Conversions.SubmitNonQArray()) return false;
					}
					//Add a procDescript to each next appointment
					Conversions.SelectText="SELECT procedurelog.nextaptnum,procedurecode.abbrdesc "
						+"FROM procedurelog,procedurecode "
						+"WHERE procedurelog.NextAptNum > '0' AND procedurelog.adacode = procedurecode.adacode "
						+"ORDER BY nextaptnum";
					if(!Conversions.SubmitSelect()) return false;
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
						if(!Conversions.SubmitNonQArray()) return false;
					}

					//final changes
					Conversions.NonQArray=new string[]{
						"UPDATE patientnote SET ApptPhone = ''"
						,"UPDATE preference SET ValueString = '2.5.1' WHERE PrefName = 'DataBaseVersion'"
					};
					if(!Conversions.SubmitNonQArray()) return false;
				}
				catch{
					return false;
				}
			}
			return To2_5_2();
		}

		private bool To2_5_2(){
			if(FromVersion < new Version("2.5.2.0")){
				try{
					Conversions.NonQArray=new string[]{
						"ALTER TABLE claimform CHANGE OffsetX OffsetX SMALLINT(5) NOT NULL default '0'"
						,"ALTER TABLE claimform CHANGE OffsetY OffsetY SMALLINT(5) NOT NULL default '0'"
						,"INSERT INTO preference (PrefName,ValueString) VALUES ('BillingExcludeInactive','0')"
						,"UPDATE preference SET ValueString = '2.5.2.0' WHERE PrefName = 'DataBaseVersion'"
					};
					if(!Conversions.SubmitNonQArray()) return false;
				}
				catch{
					return false;
				}
			}
			return To2_5_6();
		}

		private bool To2_5_6(){
			if(FromVersion < new Version("2.5.6.0")){
				try{
					Conversions.NonQArray=new string[]{
						"INSERT INTO preference (PrefName,ValueString) VALUES ('GenericEClaimsForm','')"
						,"UPDATE preference SET ValueString = '2.5.6.0' WHERE PrefName = 'DataBaseVersion'"
					};
					if(!Conversions.SubmitNonQArray()) return false;
				}
				catch{
					return false;
				}
			}
			return To2_5_7();
		}

		private bool To2_5_7(){
			if(FromVersion < new Version("2.5.7.0")){
				try{
					//copy the new ADA2002.gif
					try{
						File.Copy(@"ConversionFiles\ADA2002.gif"
							,((Pref)Prefs.HList["DocPath"]).ValueString+@"\ADA2002.gif",true);
					}
					catch{
						MessageBox.Show("ADA2002.gif could not be copied correctly.");
						return false;
					}
					//delete the old ADA2002.emf, and the old ADA2002.jpg if there is one
					if(File.Exists(((Pref)Prefs.HList["DocPath"]).ValueString+@"\ADA2002.emf")){
						File.Delete(((Pref)Prefs.HList["DocPath"]).ValueString+@"\ADA2002.emf");
					}
					if(File.Exists(((Pref)Prefs.HList["DocPath"]).ValueString+@"\ADA2002.jpg")){
						File.Delete(((Pref)Prefs.HList["DocPath"]).ValueString+@"\ADA2002.jpg");
					}
					//Get the claimformNum of the ADA2002
					ClaimForms.Refresh();
					for(int i=0;i<ClaimForms.ListLong.Length;i++){
						if(ClaimForms.ListLong[i].UniqueID==1){
							ClaimForms.Cur=ClaimForms.ListLong[i];
							break;
						}
					}
					//get the claimformitem for the background image
					ClaimFormItems.Refresh();
					ClaimFormItems.GetListForForm();
					ClaimFormItems.Cur=new ClaimFormItem();
					for(int i=0;i<ClaimFormItems.ListForForm.Length;i++){
						if(ClaimFormItems.ListForForm[i].ImageFileName=="ADA2002.emf"
							|| ClaimFormItems.ListForForm[i].ImageFileName=="ADA2002.gif"
							|| ClaimFormItems.ListForForm[i].ImageFileName=="ADA2002.jpg"){
							ClaimFormItems.Cur=ClaimFormItems.ListForForm[i];
						}
					}
					//if a match was not found, then it will start from scratch
					//Change the background to the new gif image
					ClaimFormItems.Cur.ClaimFormNum=ClaimForms.Cur.ClaimFormNum;
					ClaimFormItems.Cur.ImageFileName="ADA2002.gif";
					ClaimFormItems.Cur.XPos=9;
					ClaimFormItems.Cur.YPos=0;
					ClaimFormItems.Cur.Width=834;
					ClaimFormItems.Cur.Height=1058;
					//save the changes
					if(ClaimFormItems.Cur.ClaimFormItemNum==0){
						ClaimFormItems.InsertCur();
					}
					else{
						ClaimFormItems.UpdateCur();
					}
					//final:
					Conversions.NonQArray=new string[]{
						"UPDATE preference SET ValueString = '2.5.7.0' WHERE PrefName = 'DataBaseVersion'"
					};
					if(!Conversions.SubmitNonQArray()) return false;
				}
				catch{
					return false;
				}
			}
			return To2_8_0();
		}

		private bool To2_8_0(){
			if(FromVersion < new Version("2.8.0.0")){
				//try{
					//warn user about deleting templates
					if(MessageBox.Show(@"In version 2.8, the concept of insurance templates is being phased out.  As part of the conversion process, your existing insurance template list will be replaced with an insurance plan list and a carrier list.  If you have any notes in your insurance templates, they will be deleted.  If you have important notes in any of your insurance templates, or if you have important templates that you don't want to lose, then you should use the print screen tool to print out the important information before proceeding.  Do you wish to proceed?","",MessageBoxButtons.OKCancel)!=DialogResult.OK){
						return false;
					}
					//check to see if the conversion file is available
					if(!File.Exists(@"ConversionFiles\convert_2_8_0.txt")){
						MessageBox.Show(@"ConversionFiles\convert_2_8_0.txt"+" could not be found.");
						return false;
					}
					if(!ExecuteFile(@"ConversionFiles\convert_2_8_0.txt")) return false;
					//load all existing employer names into the new Employer table:
					Conversions.SelectText="SELECT DISTINCT Employer FROM insplan WHERE Employer !=''";
					Conversions.SubmitSelect();
					for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
						Conversions.NonQString="INSERT INTO employer(EmpName) VALUES('"
							+POut.PString(PIn.PString(Conversions.TableQ.Rows[i][0].ToString()))+"')";
						Conversions.SubmitNonQString();
						//these next 3 lines were causing a bug in converting:
						//Employers.Cur=new Employer();
						//Employers.Cur.EmpName=PIn.PString(Conversions.TableQ.Rows[i][0].ToString());
						//Employers.InsertCur();
						
					}
					//now, get the employers into HEmpNames so we can retrieve the empnum from the name
					Hashtable HEmpNames=new Hashtable();
					Conversions.SelectText="SELECT EmpName,EmployerNum FROM employer";
					Conversions.SubmitSelect();
					for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
						HEmpNames.Add(PIn.PString(Conversions.TableQ.Rows[i][0].ToString()),//name
							PIn.PInt(Conversions.TableQ.Rows[i][1].ToString()));//num
							//Employers.Cur.EmpName,Employers.Cur.EmployerNum);
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
					//Create all new ins templates based on and linked to identical insplans
					/*
					Conversions.SelectText="SELECT DISTINCT PlanType,ClaimFormNum,UseAltCode,ClaimsUseUCR"
						+",FeeSched,CopayFeeSched,EmployerNum,GroupName,GroupNum,CarrierNum"
						+" FROM insplan WHERE CarrierNum !='0'";//this will skip insplans with blank carrier(?if any)
					Conversions.SubmitSelect();
					for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
						InsTemplates.Cur=new InsTemplate();
						InsTemplates.Cur.PlanType     =PIn.PString(Conversions.TableQ.Rows[i][0].ToString());
						InsTemplates.Cur.ClaimFormNum =PIn.PInt   (Conversions.TableQ.Rows[i][1].ToString());
						InsTemplates.Cur.UseAltCode   =PIn.PBool  (Conversions.TableQ.Rows[i][2].ToString());
						InsTemplates.Cur.ClaimsUseUCR =PIn.PBool  (Conversions.TableQ.Rows[i][3].ToString());
						InsTemplates.Cur.FeeSched     =PIn.PInt   (Conversions.TableQ.Rows[i][4].ToString());
						InsTemplates.Cur.CopayFeeSched=PIn.PInt   (Conversions.TableQ.Rows[i][5].ToString());
						InsTemplates.Cur.EmployerNum  =PIn.PInt   (Conversions.TableQ.Rows[i][6].ToString());
						InsTemplates.Cur.GroupName    =PIn.PString(Conversions.TableQ.Rows[i][7].ToString());
						InsTemplates.Cur.GroupNum     =PIn.PString(Conversions.TableQ.Rows[i][8].ToString());
						InsTemplates.Cur.CarrierNum   =PIn.PInt   (Conversions.TableQ.Rows[i][9].ToString());
						InsTemplates.InsertCur();
						Conversions.NonQString="UPDATE insplan SET TemplateNum = '"
							+POut.PInt(InsTemplates.Cur.TemplateNum)+"' WHERE "
							+"PlanType = '"        +POut.PString(InsTemplates.Cur.PlanType)+"' "
							+"&& ClaimFormNum = '" +POut.PInt   (InsTemplates.Cur.ClaimFormNum)+"' "
							+"&& UseAltCode = '"   +POut.PBool  (InsTemplates.Cur.UseAltCode)+"' "
							+"&& ClaimsUseUCR = '" +POut.PBool  (InsTemplates.Cur.ClaimsUseUCR)+"' "
							+"&& FeeSched = '"     +POut.PInt   (InsTemplates.Cur.FeeSched)+"' "
							+"&& CopayFeeSched = '"+POut.PInt   (InsTemplates.Cur.CopayFeeSched)+"' "
							+"&& EmployerNum = '"  +POut.PInt   (InsTemplates.Cur.EmployerNum)+"' "
							+"&& GroupName = '"    +POut.PString(InsTemplates.Cur.GroupName)+"' "
							+"&& GroupNum = '"     +POut.PString(InsTemplates.Cur.GroupNum)+"' "
							+"&& CarrierNum = '"   +POut.PInt   (InsTemplates.Cur.CarrierNum)+"'";
						Conversions.SubmitNonQString();
					}*/
					//Add PracticeWeb Reporting program link
					//UPDATE program SET Path = 'PWReports.exe' WHERE ProgName = 'PracticeWebReports';
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
					Programs.Cur.Note=@"This link uses the VixWin QuikLink program to listen for new files in the quiklink directory. No other file path or command line arguments are needed.  The QuikLink directory would typically be C:\vx_qlink\ .  If you use ChartNum for link, it can not be more than 6 characters.";
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
					ClaimFormItems.Cur=new ClaimFormItem();
					ClaimFormItems.Cur.ClaimFormNum=PIn.PInt(Conversions.TableQ.Rows[0][0].ToString());
					ClaimFormItems.Cur.FieldName="RadiographsNumAttached";
					ClaimFormItems.Cur.XPos=684;
					ClaimFormItems.Cur.YPos=738;
					ClaimFormItems.Cur.Width=27;
					ClaimFormItems.Cur.Height=14;
					ClaimFormItems.InsertCur();
					//Denti-Cal
					Conversions.SelectText="SELECT ClaimFormNum FROM claimform WHERE UniqueID = '2'";
					Conversions.SubmitSelect();
					ClaimFormItems.Cur=new ClaimFormItem();
					ClaimFormItems.Cur.ClaimFormNum=PIn.PInt(Conversions.TableQ.Rows[0][0].ToString());
					ClaimFormItems.Cur.FieldName="RadiographsNumAttached";
					ClaimFormItems.Cur.XPos=111;
					ClaimFormItems.Cur.YPos=217;
					ClaimFormItems.Cur.Width=30;
					ClaimFormItems.Cur.Height=14;
					ClaimFormItems.InsertCur();
					ClaimFormItems.Cur=new ClaimFormItem();
					ClaimFormItems.Cur.ClaimFormNum=PIn.PInt(Conversions.TableQ.Rows[0][0].ToString());
					ClaimFormItems.Cur.FieldName="IsRadiographsAttached";
					ClaimFormItems.Cur.XPos=186;
					ClaimFormItems.Cur.YPos=187;
					ClaimFormItems.InsertCur();
					//final:
					Conversions.NonQArray=new string[]{
						"UPDATE preference SET ValueString = '2.8.0.0' WHERE PrefName = 'DataBaseVersion'"
					};
					if(!Conversions.SubmitNonQArray()) return false;
				//}
				//catch{
				//	return false;
				//}
			}
			return To2_8_2();
		}

		private bool To2_8_2(){
			if(FromVersion < new Version("2.8.2.0")){
				try{
					Conversions.NonQArray=new string[]
					{
						"ALTER TABLE insplan DROP TemplateNum"
						,"DROP TABLE instemplate"
						,"UPDATE preference SET ValueString = '2.8.2.0' WHERE PrefName = 'DataBaseVersion'"
					};
					if(!Conversions.SubmitNonQArray()) return false;
				}
				catch{
					return false;
				}
			}
			return To2_8_3();
		}

		private bool To2_8_3(){
			if(FromVersion < new Version("2.8.3.0")){
				try{
					Conversions.NonQArray=new string[]
					{
						"INSERT INTO preference VALUES ('RenaissanceLastBatchNumber','0')"
						,"INSERT INTO preference VALUES ('PatientSelectUsesSearchButton','0')"
						,"UPDATE preference SET ValueString = '2.8.3.0' WHERE PrefName = 'DataBaseVersion'"
					};
					if(!Conversions.SubmitNonQArray()) return false;
				}
				catch{
					return false;
				}
			}
			return To2_8_6();
		}

		private bool To2_8_6(){
			if(FromVersion < new Version("2.8.6.0")){
				try{
					Conversions.NonQArray=new string[]
					{
						"ALTER TABLE patient CHANGE City City VARCHAR(100) NOT NULL"
						,"ALTER TABLE patient CHANGE State State VARCHAR(100) NOT NULL"
						,"ALTER TABLE patient CHANGE Zip Zip VARCHAR(100) NOT NULL"
						,"ALTER TABLE patient CHANGE SSN SSN VARCHAR(100) NOT NULL"
						,"UPDATE preference SET ValueString = '2.8.6.0' WHERE PrefName = 'DataBaseVersion'"
					};
					if(!Conversions.SubmitNonQArray()) return false;
				}
				catch{
					return false;
				}
			}
			return To2_8_10();
		}

		private bool To2_8_10(){
			if(FromVersion < new Version("2.8.10.0")){
				try{
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
					if(!Conversions.SubmitNonQArray()) return false;
				}
				catch{
					return false;
				}
			}
			return To2_8_14();
		}

		private bool To2_8_14(){
			if(FromVersion < new Version("2.8.14.0")){
				try{
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
					if(!Conversions.SubmitNonQArray()) return false;
				}
				catch{
					return false;
				}
			}
			return true;
		}



	}
}







