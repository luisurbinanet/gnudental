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
using System.IO;
using System.Net;
using System.Resources;
using System.Text; 
using System.Windows.Forms;

namespace OpenDental{

	public class ClassConvertDatabase{
		private System.Version FromVersion;
		private System.Version ToVersion;

		public bool Convert(string fromVersion){//return false to indicate exit app.
			//Conversions=new Conversions();
			FromVersion=new Version(fromVersion);
			ToVersion=new Version(Application.ProductVersion);
			if(FromVersion.CompareTo(ToVersion)>0){
				MessageBox.Show("Can not convert database to an older version.");
				return false;
			}
			if(FromVersion < new Version("1.0.0")){
				MessageBox.Show("Can not convert databases before version 1.0.0");
				return false;
			}
			if(FromVersion.CompareTo(new Version("2.0.1"))==0){
				MessageBox.Show("Can not convert database version 2.0.1 which was only for development purposes.");
				return false;
			}
			if(FromVersion.CompareTo(new Version("2.1.0"))==0){
				MessageBox.Show("Can not convert database version 2.1.0 which was only for development purposes.");
				return false;
			}
			if(FromVersion.CompareTo(new Version("2.1.1"))==0){
				MessageBox.Show("Can not convert database version 2.1.1 which was only for development purposes.");
				return false;
			}
			if(FromVersion < new Version("2.1.5")){
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
			if(To1_0_1()){//begins going through the chain of conversion steps, each returning true
				MessageBox.Show("Conversion successful");
				Prefs.Refresh();
				return true;
			}
			else{
				return false;
			}
		}

		private bool To1_0_1(){//returns true if successful
			if(FromVersion < new Version("1.0.1")){
				try{
					Conversions.ArrayQueryText=new string[] 
						{"INSERT INTO preference (PrefName,ValueString) "
						+"VALUES('TreatmentPlanNote','If you have dental insurance, please be aware that THIS IS AN ESTIMATE ONLY.  Coverage may be different if your deductible has not been met, annual maximum has been met, or if your coverage table is lower than average.')"
						,"ALTER TABLE patient CHANGE Balance EstBalance DOUBLE DEFAULT '0' NOT NULL" 
						,"UPDATE preference SET ValueString = '1.0.1.0' WHERE PrefName = 'DataBaseVersion'"}; 
					if(!Conversions.SubmitQuery()) return false;
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
					Conversions.ArrayQueryText=new string[] 
						{"ALTER TABLE patient ADD MedicaidID VARCHAR(20) NOT NULL"
						,"UPDATE preference SET ValueString = '1.0.11' WHERE PrefName = 'DataBaseVersion'"}; 
					if(!Conversions.SubmitQuery()) return false;
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
					Conversions.ArrayQueryText=new string[AL.Count];
					AL.CopyTo(Conversions.ArrayQueryText);
					//MessageBox.Show(AL.Count.ToString());
					//for(int i=30;i<Conversions.ArrayQueryText.Length;i++){
					//	MessageBox.Show(Conversions.ArrayQueryText[i]);
					//}
					if(!Conversions.SubmitQuery()){
						sr.Close();
						return false;
					}
					sr.Close();
					//Add a finance charge type to adjustments:
					Defs.Refresh();
					Defs.Cur=new Def();
					Defs.Cur.Category=(int)DefCat.AdjTypes;
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
				Conversions.ArrayQueryText=new string[AL.Count];
				AL.CopyTo(Conversions.ArrayQueryText);
				if(!Conversions.SubmitQuery()){
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
					Conversions.ArrayQueryText=new string[]{
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
					if(!Conversions.SubmitQuery()) return false;
					//copy SSN into each insplan.subscriberID.
					Conversions.SelectText="SELECT patient.ssn,insplan.plannum "
						+"FROM patient,insplan WHERE patient.patnum = insplan.subscriber";
					if(!Conversions.SubmitSelect()) return false;
					for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
						Conversions.ArrayQueryText=new string[]{
							"UPDATE insplan SET subscriberid = '"
							+POut.PString(Conversions.TableQ.Rows[i][0].ToString())+"'"
							+" WHERE plannum = '"+Conversions.TableQ.Rows[i][1].ToString()+"'"
						};
						if(!Conversions.SubmitQuery()) return false;
					}
					//fix any adjustments without provnum
					Conversions.SelectText="SELECT patient.priprov,adjustment.adjnum "
						+"FROM patient,adjustment WHERE patient.patnum = adjustment.patnum "
						+"&& adjustment.provnum = '0'";
					if(!Conversions.SubmitSelect()) return false;
					for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
						Conversions.ArrayQueryText=new string[]{
							"UPDATE adjustment SET provnum = '"
							+Conversions.TableQ.Rows[i][0].ToString()+"'"
							+" WHERE adjnum = '"+Conversions.TableQ.Rows[i][1].ToString()+"'"
						};
						if(!Conversions.SubmitQuery()) return false;
					}
					//fix any procedures without provnum
					Conversions.SelectText="SELECT patient.priprov,procedurelog.procnum "
						+"FROM patient,procedurelog WHERE patient.patnum = procedurelog.patnum "
						+"&& procedurelog.provnum = '0'";
					if(!Conversions.SubmitSelect()) return false;
					for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
						Conversions.ArrayQueryText=new string[]{
							"UPDATE procedurelog SET provnum = '"
							+Conversions.TableQ.Rows[i][0].ToString()+"'"
							+" WHERE procnum = '"+Conversions.TableQ.Rows[i][1].ToString()+"'"
						};
						if(!Conversions.SubmitQuery()) return false;
					}
					//fix all appointments with status of 0. Move them to unsched list.
					Conversions.SelectText="SELECT appointment.aptnum from patient,appointment "
						+"WHERE patient.patnum = appointment.patnum "
						+"&& patient.nextaptnum != appointment.aptnum "
						+"&& appointment.aptstatus = '0'";
					if(!Conversions.SubmitSelect()) return false;
					for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
						Conversions.ArrayQueryText=new string[]{
							"UPDATE appointment SET aptstatus = '3'"
							+" WHERE aptnum = '"+Conversions.TableQ.Rows[i][0].ToString()+"'"
						};
						if(!Conversions.SubmitQuery()) return false;
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
						Conversions.ArrayQueryText=new string[]{
							"UPDATE claim SET patrelat = '"+POut.PInt((int)thisRelat)+"'"
							+" WHERE claimnum = '"+claimTable.Rows[i][0].ToString()+"'"
						};
						if(!Conversions.SubmitQuery()) return false;
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
					Defs.Cur.Category=(int)DefCat.ContactCategories;
					Defs.Cur.ItemName="Pharmacies";
					Defs.Cur.ItemOrder=0;
					Defs.InsertCur();
					Defs.Cur=new Def();
					Defs.Cur.Category=(int)DefCat.ContactCategories;
					Defs.Cur.ItemName="Local Dentists";
					Defs.Cur.ItemOrder=1;
					Defs.InsertCur();
					if(!ExecuteFile(@"ConversionFiles\convert_2_1_2.txt")) return false;
					File.Copy(@"ConversionFiles\DentiCal.jpg"
						,((Pref)Prefs.HList["DocPath"]).ValueString+@"\DentiCal.jpg",true);
					Conversions.ArrayQueryText=new string[]{
						"UPDATE preference SET ValueString = '2.1.2' WHERE PrefName = 'DataBaseVersion'"
					};
					if(!Conversions.SubmitQuery()) return false;
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
					Conversions.ArrayQueryText=new string[]{
						"UPDATE claimformitem SET formatstring = 'MM/dd/yy' WHERE "
							+"claimformitemnum = '280' || claimformitemnum = '286' || claimformitemnum = '292' "
							+"|| claimformitemnum = '298' || claimformitemnum = '304' || claimformitemnum = '310' "
							+"|| claimformitemnum = '316' || claimformitemnum = '322' || claimformitemnum = '328' "
							+"|| claimformitemnum = '334'"
						,"ALTER TABLE procedurecode CHANGE ADACode ADACode VARCHAR(15) BINARY NOT NULL default ''"
						,"UPDATE preference SET ValueString = '2.1.5' WHERE PrefName = 'DataBaseVersion'"
					};
					if(!Conversions.SubmitQuery()) return false;
				}
				catch{
					return false;
				}
			}//if
			return To2_5_0();
		}

		private bool To2_5_0(){
			return true;
		}

	}
}







