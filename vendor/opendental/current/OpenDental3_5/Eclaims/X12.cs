using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OpenDental.Eclaims
{
	/// <summary>
	/// Summary description for X12.
	/// </summary>
	public class X12{
		///<summary></summary>
		public X12()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		///<summary>Gets the filename for this batch. Used when saving or when rolling back.</summary>
		public static string GetFileName(Clearinghouse clearhouse,int interchangeNum){
			string saveFolder=clearhouse.ExportPath;
			if(!Directory.Exists(saveFolder)){
				MessageBox.Show(saveFolder+" not found.");
				return "";
			}
			if(clearhouse.CommBridge==EclaimsCommBridge.RECS){
				if(File.Exists(saveFolder+"ecs.txt")){
					MessageBox.Show("You must send your existing claims from the RECS program before you can create another batch.");
					return "";//prevents overwriting an existing ecs.txt.
				}
				return saveFolder+"ecs.txt";
			}
			else{
				return saveFolder+"claims"+interchangeNum.ToString()+".txt";
			}
		}

		///<summary>If file creation was successful but communications failed, then this deletes the X12 file.</summary>
		public static void Rollback(Clearinghouse clearhouse,int interchangeNum){
			if(clearhouse.CommBridge==EclaimsCommBridge.RECS){
				//A RECS rollback never deletes the file, because there is only one
			}
			else{
				File.Delete(clearhouse.ExportPath+"claims"+interchangeNum.ToString()+".txt");
			}
		}

		///<summary>Supply an arrayList of type ClaimSendQueueItem. Called from Eclaims and includes multiple claims.</summary>
		public static bool SendBatch(ArrayList queueItems,int interchangeNum){
			Clearinghouse clearhouse=Clearinghouses.GetClearinghouse(
				((ClaimSendQueueItem)queueItems[0]).ClearinghouseNum);
			string saveFile=GetFileName(clearhouse,interchangeNum);
			if(saveFile==""){
				return false;
			}
			//one for each carrier. Can be reused in other interchanges, so not persisted:
			int transactionNum=1;
			bool isTesting=false;
			#if DEBUG
				isTesting=true;
			#endif
			using(StreamWriter sw=new StreamWriter(saveFile,false,Encoding.ASCII))
			{
				//Interchange Control Header (Interchange number tracked separately from transactionNum)
				sw.Write("ISA*00*          *"//ISA01,ISA02: 00 + 10 spaces
					+"00*          *"//ISA03,ISA04: 00 + 10 spaces
					+GetISA05(clearhouse)+"*"//ISA05: Sender ID type: ZZ=mutually defined. 30=TIN
					+GetISA06(clearhouse)+"*"//ISA06: Sender ID(TIN). Sometimes Jordan Sparks.  Sometimes the sending clinic.
					+GetISA07(clearhouse)+"*"//ISA07: Receiver ID type: ZZ=mutually defined. 30=TIN
					+clearhouse.ReceiverID.PadRight(15,' ')+"*"//ISA08: Receiver ID
					+DateTime.Today.ToString("yyMMdd")+"*"//ISA09: today's date
					+DateTime.Now.ToString("HHmm")+"*"//ISA10: current time
					+"U*00401*"//ISA11 and ISA12. 
					//ISA13: interchange control number, right aligned:
					+interchangeNum.ToString().PadLeft(9,'0')+"*"
					+"0*");//ISA14: no acknowledgment requested
					if(isTesting){
						sw.Write("T*");//ISA15: T=Test data.  P=Production data.
					}
					else{
						sw.Write("P*");
					}
					sw.WriteLine(":~");//ISA16: use ':'
				//Functional Group Header (only one)
				sw.WriteLine("GS*HC*"//GS01: Health Care Claim
					+GetGS02(clearhouse)+"*"//GS02: Application Senders Code. Sometimes Jordan Sparks.  Sometimes the sending clinic.
					+GetGS03(clearhouse)+"*"//GS03: Application Receiver's Code
					+DateTime.Today.ToString("yyyyMMdd")+"*"//GS04: today's date
					+DateTime.Now.ToString("HHmm")+"*"//GS05: current time
					//GS06: Group control number. Max length 9. No padding necessary. Using interchangeNum
					+interchangeNum.ToString()+"*"
					+"X*"//GS07: X
					+"004010X097A1~");//GS08: Version
				//Gets an array with PayorID,ProvBill,Subscriber,PatNum,ClaimNum all in the correct order
				object[,] claimAr=Claims.GetX12TransactionInfo(queueItems);
				bool newTrans;//true if this loop has transaction header
				bool hasFooter;//true if this loop has transaction footer(if the Next loop is a newTrans)
				int HLcount=1;
				int parentProv=0;//the HL sequence # of the current provider.
				int parentSubsc=0;//the HL sequence # of the current subscriber.
				string hasSubord="";//0 if no subordinate, 1 if at least one subordinate
				Claim claim;
				InsPlan insPlan;
				InsPlan otherPlan=new InsPlan();
				Patient patient;
				Patient subscriber;
				Patient otherSubsc=new Patient();
				Carrier carrier;
				Carrier otherCarrier=new Carrier();
				ClaimProc[] claimProcList;//all claimProcs for a patient. Only used briefly.
				ClaimProc[] claimProcs;
				Procedure[] procList;
				Procedure proc;
				ProcedureCode procCode;
				Provider provTreat;//might be different for each proc
				int seg=0;//segments for a particular ST-SE transaction
				for(int i=0;i<claimAr.GetLength(1);i++){
					#region Transaction Set Header
					if(i==0//if this is the first claim
						|| claimAr[0,i].ToString() != claimAr[0,i-1].ToString())//or the payorID has changed
					{
						newTrans=true;
						seg=0;
					}
					else newTrans=false;
					if(newTrans){
						//Transaction Set Header (one for each carrier)
						seg++;
						sw.WriteLine("ST*837*"
							+transactionNum.ToString().PadLeft(4,'0')+"~");//ST02 Transact. control #.
						//transactionNum gets incremented in SE section
						seg++;
						sw.WriteLine("BHT*0019*00*"
							+transactionNum.ToString().PadLeft(4,'0')+"*"//BHT03. Can be same as ST02
							+DateTime.Now.ToString("yyyyMMdd")+"*"
							+DateTime.Now.ToString("HHmmss")+"*CH~");
						seg++;
						if(isTesting){
							sw.WriteLine("REF*87*004010X097DA1~");
						}
						else{
							sw.WriteLine("REF*87*004010X097A1~");
						}
						//1000A Submitter is usually OPEN DENTAL, but for inmediata it's the practice
							//(depends on clearinghouse and Partnership agreements)
						//1000A NM1: required
						seg++;
						Write1000A_NM1(sw,clearhouse);
						//1000A PER: required. Contact number.
						seg++;//always one seg
						Write1000A_PER(sw,clearhouse);
						//1000B Receiver is always the Clearinghouse
						//1000B NM1: required
						seg++;
						sw.WriteLine("NM1*40*"//NM101: 40=receiver
							+"2*"//NM102: 2=nonperson
							+Sout(clearhouse.Description,35,1)+"*"//NM103:Receiver Name
							+"****"//NM104-NM107 not used since not a person
							+"46*"//NM108: 46 indicates ETIN
							+clearhouse.ReceiverID.ToUpper()+"~");//NM109: Receiver ID Code. aka ETIN#.
						HLcount=1;
						parentProv=0;//the HL sequence # of the current provider.
						parentSubsc=0;//the HL sequence # of the current subscriber.
						hasSubord="";//0 if no subordinate, 1 if at least one subordinate
					}
					#endregion
					//HL Loops:
					#region Billing Provider
					if(i==0//if first claim
						|| newTrans //or new Transaction set
						|| claimAr[1,i].ToString() != claimAr[1,i-1].ToString())//or prov has changed
					{
						//2000A HL: Billing/Pay-to provider HL loop
						seg++;
						sw.Write("HL*"+HLcount.ToString()+"*");//HL01: Heirarchical ID
						if(i==0)
							sw.Write("*");//HL02: No parent
						else
							sw.Write("0*");//HL02: No parent
						sw.WriteLine("20*"//HL03: Heirarchical level code. 20=Information source
							+"1~");//HL04: Heirarchical child code. 1=child HL present
						Provider billProv=Providers.ListLong[Providers.GetIndexLong((int)claimAr[1,i])];
						//2000A PRV: Provider Information (Optional Rendering prov for all claims in this HL)
						//used instead of 2310B.
						seg++;
						sw.WriteLine("PRV*PT*"//PRV01: Provider Code. BI=Billing, PT=Pay-To
							+"ZZ*"//PRV02: mutually defined taxonomy codes
							+GetTaxonomy(billProv.Specialty)+"~");//PRV03: Provider taxonomy code
						//2010AA NM1: Billing provider
						seg++;
						sw.Write("NM1*85*"//NM101: 85=Billing provider
							+"1*"//NM102: 1=person,2=non-person
							+Sout(billProv.LName,35)+"*"//NM103: Last name
							+Sout(billProv.FName,25)+"*"//NM104: First name
							+Sout(billProv.MI,25,1)+"*"//NM105: Middle name
							+"*"//NM106: not used
							+"*");//NM107: Name suffix. not used
						if(billProv.UsingTIN)
							sw.Write("24*");//NM108: ID code qualifier. 24=EIN. 34=SSN.
						else
							sw.Write("34*");
						sw.WriteLine(Sout(billProv.SSN,80)+"~");//NM109: ID code (EIN or SSN)
						//2010AA N3: Billing provider address
						seg++;
						sw.Write("N3*"+Sout(Prefs.GetString("PracticeAddress"),55));//N301: Address
						if(Prefs.GetString("PracticeAddress2")==""){
							sw.WriteLine("~");
						}
						else{
							//N302: Address2. Optional.
							sw.WriteLine("*"+Sout(Prefs.GetString("PracticeAddress2"),55)+"~");
						}
						//2010AA N4: Billing prov City,State,Zip
						seg++;
						sw.WriteLine("N4*"+Sout(Prefs.GetString("PracticeCity"),30)+"*"//N401: City
							+Sout(Prefs.GetString("PracticeST"),2)+"*"//N402: State
							+Sout(Prefs.GetString("PracticeZip"),15)+"~");//N403: Zip
						//2010AA REF: Office phone number. Required by WebMD.
						if(clearhouse.ReceiverID=="0135WCH00"){//if WebMD
							seg++;
							sw.WriteLine("REF*LU*"
								+Prefs.GetString("PracticePhone")+"~");
						}
						//2010AA REF: License #. Required by RECS clearinghouse,
						//but everyone else should find it useful too.
						seg++;
						sw.WriteLine("REF*1E*"//REF01: 1E=license #
							+Sout(billProv.StateLicense,30)+"~");
						//2010AA REF: Secondary ID number(s). Only required by some carriers.
						seg+=WriteProv_REF(sw,billProv,(string)claimAr[0,i]);
						parentProv=HLcount;
						HLcount++;
					}
					#endregion
					claim=Claims.GetClaim((int)claimAr[4,i]);
					insPlan=InsPlans.GetPlan(claim.PlanNum,new InsPlan[] {});
					//insPlan could be null if db corruption. No error checking for that
					if(claim.PlanNum2>0){
						otherPlan=InsPlans.GetPlan(claim.PlanNum2,new InsPlan[] {});
						otherSubsc=Patients.GetPat(otherPlan.Subscriber);
						otherCarrier=Carriers.GetCarrier(otherPlan.CarrierNum);
					}
					patient=Patients.GetPat(claim.PatNum);
					subscriber=Patients.GetPat(insPlan.Subscriber);
					carrier=Carriers.GetCarrier(insPlan.CarrierNum);
					claimProcList=ClaimProcs.Refresh(patient.PatNum);
					claimProcs=ClaimProcs.GetForSendClaim(claimProcList,claim.ClaimNum);
					procList=Procedures.Refresh(claim.PatNum);
					#region Subscriber
					if(i==0 || claimAr[2,i].ToString() != claimAr[2,i-1].ToString()){//if subscriber changed
						//situation 1:
						if(claimAr[3,i].ToString()==claimAr[2,i].ToString()){//if patient is the subscriber
							//-claim level will follow
							//-then, possibly more patients ONLY IF they have the same subscriber
							hasSubord="0";
							//loop through ALL the claims again and check for matching subscribers, diff pats
							for(int j=0;j<claimAr.GetLength(1);j++){
								if(j!=i//we are only trying to match different lines
									//other claims with same subscriber
									&& claimAr[2,j].ToString()==claimAr[2,i].ToString()
									&& claimAr[3,j].ToString()!=claimAr[3,i].ToString())//and patient is different
								{
									hasSubord="1";//so there will be a subord HL after the claim info
								}
							}
						}
						//situation 2:
						else{//patient is not the subscriber
							//-patient will always follow
							hasSubord="1";
						}
						//2000B HL: Subscriber HL loop
						seg++;
						sw.WriteLine("HL*"+HLcount.ToString()+"*"//HL01: Heirarchical ID
							+parentProv.ToString()+"*"//HL02: parent is always the provider HL
							+"22*"//HL03: 22=Subscriber
							+hasSubord+"~");//HL04: 1=additional subordinate HL segments. 0=no subord

						//2000B SBR:
						seg++;
						sw.Write("SBR*");
						if(claim.ClaimType=="P"){
							sw.Write("P*");//SBR01: Payer responsibility code
						}
						else if(claim.ClaimType=="S"){
							sw.Write("S*");
						}
						else{
							sw.Write("T*");//T=Tertiary
						}
//fix: what about Preauth and Cap?
						if(claimAr[3,i].ToString()==claimAr[2,i].ToString()){//if patient is the subscriber
							sw.Write("18*");//SBR02: Relationship. 18=self
						}
						else{
							sw.Write("*");//empty if patient is not subscriber.
						}
						sw.Write(Sout(insPlan.GroupNum,30)+"*"//SBR03: Group Number
							+Sout(insPlan.GroupName,60)+"*"//SBR04: Group Name
							+"*");//SBR05: Not used
						if(claim.PlanNum2>0){
							sw.Write("1*");//SBR06: 1=Coordination of benefits. 6=No coordination
						}
						else{
							sw.Write("6*");
						}
						sw.Write("**");//SBR07 & 08 not used.
						sw.WriteLine("CI~");//SBR09: 12=PPO,17=DMO,BL=BCBS,CI=CommercialIns,FI=FEP,HM=HMO
								//,MC=Medicaid,SA=self-administered, etc. There are too many. I'm just goint 
								//to use CI for everyone. I don't think anyone will care.
						//2010BA NM1: Subscriber Name
						seg++;
						sw.WriteLine("NM1*IL*"//NM101: IL=Insured or Subscriber
							+"1*"//NM102: 1=Person
							+Sout(subscriber.LName,35)+"*"//NM103: LName
							+Sout(subscriber.FName,25)+"*"//NM104: FName
							+Sout(subscriber.MiddleI,25)+"*"//NM105: MiddleName
							+"*"//NM106: not used
							+"*"//NM107: suffix. Not present in Open Dental yet.
							+"MI*"//NM108: MI=MemberID
							+Sout(insPlan.SubscriberID,80)+"~");//NM109: Subscriber ID
						//At the request of WebMD, we are including N3,N4,and DMG even if patient is not subscriber.
						//This does not make the transaction non-compliant, and they find it useful.
						//if(claimAr[3,i].ToString()==claimAr[2,i].ToString()){//if patient is the subscriber
						//2010BA N3: Subscriber Address. Only if patient is the subscriber
							seg++;
							sw.Write("N3*"+Sout(subscriber.Address,55));//N301: address
								if(subscriber.Address2==""){
									sw.WriteLine("~");
								}
								else{
									//N302: Address2. Optional.
									sw.WriteLine("*"+Sout(subscriber.Address2,55)+"~");
								}
						//2010BA N4: CityStZip. Only if patient is the subscriber
							seg++;
							sw.WriteLine("N4*"
								+Sout(subscriber.City,30,2)+"*"//N401: City
								+Sout(subscriber.State,2,2)+"*"//N402: State
								+Sout(subscriber.Zip,15,3)+"~");//N403: Zip
						//2010BA DMG: Subscr. Demographics. Only if patient is the subscriber
							seg++;
							sw.WriteLine("DMG*D8*"//DMG01: use D8
								+subscriber.Birthdate.ToString("yyyyMMdd")+"*"//DMG02: birthdate
								+GetGender(subscriber.Gender)+"~");//DMG03: gender. F,M,or U
						//}//if provider is the subscriber
						//2010BA REF: Secondary ID. Situational. Not used.
						//2010BA REF: Casualty Claim number. Not used.
						//2010BB: PayerName
						//2010BB NM1: Name
						seg++;
						sw.WriteLine("NM1*PR*"//NM101: PR=Payer
							+"2*"//NM102: 2=Non person
							+Sout(carrier.CarrierName,35)+"*"//NM103: Name
							+"****"//NM104-07 not used
							+"PI*"//NM108: PI=PayorID
							+Sout(carrier.ElectID)+"~");//NM109: PayorID
						//2010BB N3: Carrier Address
						seg++;
						sw.Write("N3*"+Sout(carrier.Address,55));//N301: address
							if(carrier.Address2==""){
								sw.WriteLine("~");
							}
							else{
								//N302: Address2. Optional.
								sw.WriteLine("*"+Sout(carrier.Address2,55)+"~");
							}
						//2010BB N4: Carrier City,St,Zip
						seg++;
						sw.WriteLine("N4*"
							+Sout(carrier.City,30,2)+"*"//N401: City
							+Sout(carrier.State,2,2)+"*"//N402: State
							+Sout(carrier.Zip,15,3)+"~");//N403: Zip
						parentSubsc=HLcount;
						HLcount++;
					}
					#endregion
					#region Patient
					if((i==0 || claimAr[3,i].ToString() != claimAr[3,i-1].ToString())//if patient changed
						&& claimAr[3,i].ToString() != claimAr[2,i].ToString())//AND patient is not subscriber
					{
						//2000C Patient HL loop
						seg++;
						sw.WriteLine("HL*"+HLcount.ToString()+"*"//HL01:Heirarchical ID
							+parentSubsc.ToString()+"*"//HL02: parent is always the subscriber HL
							+"23*"//HL03: 23=Dependent
							+"0~");//HL04: never a subordinate
						//2000C PAT
						seg++;
						sw.WriteLine("PAT*"
							+GetRelat(claim.PatRelat)+"*"//PAT01: Relat
							+"**"//PAT02 & 03 not used
							+GetStudent(patient.StudentStatus)+"~");//PAT04: Student status code: N,P,or F
						//2010CA NM1: Patient Name
						seg++;
						sw.Write("NM1*QC*"//NM101: QC=Patient
							+"1*"//NM102: 1=Person
							+Sout(patient.LName,35)+"*"//NM103: Lname
							+Sout(patient.FName,25)+"*");//NM104: Fname
						if(patient.SSN==""){
							if(patient.MiddleI==""){
								sw.WriteLine("~");
							}
							else{
								sw.WriteLine(Sout(patient.MiddleI,25)+"~");//NM105: Mid name
							}
						}
						else{//ssn not blank
							sw.WriteLine(Sout(patient.MiddleI,25)+"*"//NM105: Mid name (whether or not empty)
							+"**"//NM106: prefix not used. NM107: No suffix field in Open Dental
							+"MI*"//NM108: MI=Member ID
							+Sout(patient.SSN,80)+"~");//NM109: Patient ID
						}
						//2010CA N3: Patient address
						seg++;
						sw.Write("N3*"
							+Sout(patient.Address,55));//N301: address
							if(patient.Address2==""){
									sw.WriteLine("~");
								}
								else{
									//N302: Address2. Optional.
									sw.WriteLine("*"+Sout(patient.Address2,55)+"~");
								}
						//2010CA N4: City State Zip
						seg++;
						sw.WriteLine("N4*"
							+Sout(patient.City,30)+"*"//N401: City
							+Sout(patient.State,2)+"*"//N402: State
							+Sout(patient.Zip,15)+"~");//N403: Zip
						//2010CA DMG: Patient demographic
						seg++;
						sw.WriteLine("DMG*D8*"//DMG01: use D8
							+patient.Birthdate.ToString("yyyyMMdd")+"*"//DMG02: Birthdate
							+GetGender(patient.Gender)+"~");//DMG03: gender
						HLcount++;
					}
					#endregion
					#region Claim
					//2300 CLM: Claim
					seg++;
					sw.Write("CLM*"
						+claim.ClaimNum.ToString()+"*"//CLM01: ClaimNum, a unique id.
						+claim.ClaimFee.ToString()+"*"//CLM02: Claim Fee
						+"**"//CLM03 & 04 not used
						+GetPlaceService(claim.PlaceService)+"::1*"//CLM05: place+1. 1=Original claim
						+"Y*"//CLM06: prov sig on file (always yes)
						+"A*");//CLM07: prov accepts medicaid assignment. OD has no field for this, so no choice
					if(insPlan.AssignBen){
						sw.Write("Y*");//CLM08: assign ben. Y or N
					}
					else{
						sw.Write("N*");
					}
					if(insPlan.ReleaseInfo){
						sw.Write("Y");//CLM09: release info. Y or N
					}
					else{
						sw.Write("N");
					}
					if(GetRelatedCauses(claim)==""){
						sw.WriteLine("~");
					}
					else{
						sw.WriteLine("**"//* for CLM09. CLM10 not used
							+GetRelatedCauses(claim)+"~");//CLM11: Accident related, including state
					}
							//+"*"//CLM12: special programs like EPSTD
							//+"******"//CLM13-18 not used
							//+"PB*"//CLM19 PB=Predetermination of Benefits
							//CLM20: delay reason code
					//2300 DTP: Date referral
					//2300 DTP: Date accident
					if(claim.AccidentDate.Year>1880){
						seg++;
						sw.WriteLine("DTP*439*"//DTP01: 439=accident
							+"D8*"//DTP02: use D8
							+claim.AccidentDate.ToString("yyyyMMdd")+"~");
					}
					//2300 DTP: Date ortho appliance placed
					if(claim.OrthoDate.Year>1880){
						seg++;
						sw.WriteLine("DTP*452*"//DTP01: 452=Appliance placement
							+"D8*"//DTP02: use D8
							+claim.OrthoDate.ToString("yyyyMMdd")+"~");
					}
					//2300 DTP: Date of service for claim. Not used if predeterm
					if(claim.ClaimType!="PreAuth"){
						if(claim.DateService.Year>1880){
							seg++;
							sw.WriteLine("DTP*472*"//DTP01: 472=Service
								+"D8*"//DTP02: use D8
								+claim.DateService.ToString("yyyyMMdd")+"~");
						}
					}
					//2300 DN1: Months of ortho service
					if(claim.IsOrtho){
						seg++;
						sw.WriteLine("DN1*"
							+"*"//DN101 not used because no field yet in OD.
							+claim.OrthoRemainM.ToString()+"~");
					}
					//2300 DN2
					ArrayList missingTeeth=Procedures.GetMissingTeeth(procList);
					for(int j=0;j<missingTeeth.Count;j++){
						seg++;
						sw.WriteLine("DN2*"
							+missingTeeth[j]+"*"//DN201: tooth number
							+"M~");//DN202: M=Missing, I=Impacted, E=To be extracted
					}
					//2300 PWK: Paperwork. Used to identify attachments.
					//2300 AMT: Patient amount paid
					//2300 REF: Predetermination ID
					if(claim.PreAuthString!=""){
						seg++;
						sw.WriteLine("REF*G3*"//REF01: G3=predeterm
							+Sout(claim.PreAuthString,30)+"~");//REF02: Predeterm Identifier
					}
					//2300 REF: Referral number
					if(claim.RefNumString!=""){
						seg++;
						sw.WriteLine("REF*9F*"//REF01: 9F=Referral number
							+Sout(claim.RefNumString,30)+"~");
					}
					//2300 NTE: Note
					if(claim.ClaimNote!=""){
						seg++;
						sw.WriteLine("NTE*ADD*"//NTE01: ADD=Additional infor
							+Sout(claim.ClaimNote,80)+"~");
					}
					//2310B Rendering provider. Only required if different from the billing provider
					//But required by WebClaim, so we will always include it
					provTreat=Providers.ListLong[Providers.GetIndexLong(claim.ProvTreat)];
					//if(claim.ProvTreat!=claim.ProvBill){
					//2310B NM1: name
						seg++;
						sw.Write("NM1*82*"//82=rendering prov
							+"1*"//NM102: 1=person
							+Sout(provTreat.LName,35)+"*"//NM103: LName
							+Sout(provTreat.FName,25)+"*"//NM104: FName
							+Sout(provTreat.MI,25)+"*"//NM105: MiddleName
							+"*"//NM106: not used
							+"*");//NM107: suffix. not used
						if(provTreat.UsingTIN){
							sw.Write("24*");//NM108: 24=EIN, 34=SSN
						}
						else{
							sw.Write("34*");
						}
						sw.WriteLine(Sout(provTreat.SSN,80)+"~");//NM109: ID
					//2310B PRV: Rendering provider information
						seg++;
						sw.WriteLine("PRV*"
							+"PE*"//PRV01: PE=Performing
							+"ZZ*"//PRV02: ZZ=mutually defined taxonomy code
							+GetTaxonomy(provTreat.Specialty)+"~");//PRV03: Taxonomy code
					//2310B REF: Rendering provider secondary ID.
						seg++;
						sw.WriteLine("REF*1E*"//REF01: 1E=license #
							+Sout(provTreat.StateLicense,30)+"~");
					//2310B REF: Rendering provider secondary ID
						seg+=WriteProv_REF(sw,provTreat,(string)claimAr[0,i]);
					//}//if(claim.ProvTreat!=claim.ProvBill){
					//2310C NM1: Service facility location if not office
					if(claim.PlaceService!=PlaceOfService.Office){
						Provider provFac
							=Providers.List[Providers.GetIndex(Prefs.GetInt("PracticeDefaultProv"))];
						seg++;
						sw.Write("NM1*FA*"//FA=Facility
							+"2*"//NM102: 2=non-person
							+Sout(Prefs.GetString("PracticeTitle"),35)+"*"//NM103:Submitter Name
							+"*"//NM104: not used
							+"*"//NM105: not used
							+"*"//NM106: not used
							+"*");//NM107: not used
						if(provFac.UsingTIN){
							sw.Write("24*");//NM108: 24=EIN, 34=SSN
						}
						else{
							sw.Write("34*");
						}
						sw.WriteLine(Sout(provFac.SSN,80)+"~");//NM109: ID
					}
					//2320 Other subscriber
					if(claim.PlanNum2>0){
						seg++;
						sw.Write("SBR*");
						if(claim.ClaimType=="S"){
							sw.Write("P*");//SBR01: Payer responsibility code
						}
						else if(claim.ClaimType=="P"){
							sw.Write("S*");
						}
						else{
							sw.Write("T*");//T=Tertiary
						}
						sw.Write(GetRelat(claim.PatRelat2)+"*");
						sw.Write(Sout(otherPlan.GroupNum,30)+"*"//SBR03: Group Number
							+Sout(otherPlan.GroupName,60)+"*"//SBR04: Group Name
							+"*"//SBR05: Not used
							+"*");//SBR06: Not used
						sw.Write("**");//SBR07 & 08 not used.
						sw.WriteLine("CI~");//SBR09: 12=PPO,17=DMO,BL=BCBS,CI=CommercialIns,FI=FEP,HM=HMO
								//,MC=Medicaid,SA=self-administered, etc. There are too many. I'm just going 
								//to use CI for everyone. I don't think anyone will care.
					//2320 AMT: COB paid amount
					//2320 AMT: COB patient responsibility
					//2320 AMT: COB discount amount
					//2320 AMT: COB paid to patient
					//2320 DMG: Other subscriber demographics
						seg++;
						sw.WriteLine("DMG*D8*"//DMG01: use D8
							+otherSubsc.Birthdate.ToString("yyyyMMdd")+"*"//DMG02: Birthdate
							+GetGender(otherSubsc.Gender)+"~");//DMG03: gender
					//2320 OI: Other ins info
						seg++;
						sw.Write("OI***");//OI01 & 02 not used
						if(otherPlan.AssignBen){
							sw.Write("Y***");//OI03: assign ben. Y or N
						}
						else{
							sw.Write("N***");
						}
						if(otherPlan.ReleaseInfo){
							sw.WriteLine("Y~");//OI06: release info. Y or N
						}
						else{
							sw.WriteLine("N~");
						}
					//2330A NM1: Other subsc name
						seg++;
						sw.WriteLine("NM1*IL*"//NM010: IL=insured
							+"1*"//NM102: 1=person
							+Sout(otherSubsc.LName,35)+"*"//NM103: LName
							+Sout(otherSubsc.FName,25)+"*"//NM104: FName
							+Sout(otherSubsc.MiddleI,25)+"*"//NM105: MiddleName
							+"*"//NM106: not used
							+"*"//NM107: suffix. No corresponding field in OD
							+"MI*"//NM108: MI=Member ID
							+Sout(otherPlan.SubscriberID,80)+"~");//NM109: ID
					//2330A N3: Address
						seg++;
						sw.Write("N3*"
							+Sout(otherSubsc.Address,55));//N301: address
							if(otherSubsc.Address2==""){
								sw.WriteLine("~");
							}
							else{
								sw.WriteLine("*"+Sout(otherSubsc.Address2,55)+"~");//N302: address2
							}
					//2330A N4: City State Zip
						seg++;
						sw.WriteLine("N4*"
							+Sout(otherSubsc.City,30)+"*"//N401: City
							+Sout(otherSubsc.State,2)+"*"//N402: State
							+Sout(otherSubsc.Zip,15)+"~");//N403: Zip
					//2330B NM1: Other payer name
						seg++;
						sw.WriteLine("NM1*PR*"//NM010: PR=Payer
							+"2*"//NM102: 2=non-person
							+Sout(otherCarrier.CarrierName,35)+"*"//NM103: Name
							+"*"//NM104:
							+"*"//NM105:
							+"*"//NM106: not used
							+"*"//NM107: not used
							+"PI*"//NM108: PI=Payor ID
							+Sout(otherCarrier.ElectID,80,2)+"~");//NM109: ID
					//2330 DTP Claim Paid date
					}//if(claim.PlanNum2>0){
					#endregion
					#region Line Items
					//2400 Service Lines
					for(int j=0;j<claimProcs.Length;j++){
						proc=Procedures.GetProc(procList,claimProcs[j].ProcNum);
						procCode=ProcedureCodes.GetProcCode(proc.ADACode);
						//2400 LX: Line Counter
						seg++;
						sw.WriteLine("LX*"+(j+1).ToString()+"~");
						//2400 SV3: Dental Service
						seg++;
						sw.Write("SV3*"
							+"AD:"+Sout(claimProcs[j].CodeSent)+"*"//SV301-1: AD=ADA CDT, SV301-2:Procedure code
							+claimProcs[j].FeeBilled.ToString()+"*");//SV302: Charge Amount
						if(proc.PlaceService==claim.PlaceService){
							sw.Write("*");//SV303: Location Code if different from claim
						}
						else{
							sw.Write(GetPlaceService(proc.PlaceService)+"*");
						}
						sw.WriteLine(GetArea(proc,procCode)+"*"//SV304: Area
							+proc.Prosthesis+"*"//SV305: Initial or Replacement
							+"1~");//SV306: Procedure count
						//2400 TOO: Tooth number/surface
						if(procCode.TreatArea==TreatmentArea.Tooth){
							seg++;
							sw.WriteLine("TOO*JP*"//TOO01: JP=National tooth numbering
								+proc.ToothNum+"~");//TOO02: Tooth number
						}
						else if(procCode.TreatArea==TreatmentArea.Surf){
							seg++;
							sw.Write("TOO*JP*"//TOO01: JP=National tooth numbering
								+proc.ToothNum+"*");//TOO02: Tooth number
							for(int k=0;k<proc.Surf.Length;k++){
								if(k>0){
									sw.Write(":");
								}
								sw.Write(proc.Surf.Substring(k,1));//TOO03: Surfaces
							}
							sw.WriteLine("~");
						}
						//2400 DTP: Date of service if different from claim
						if(claim.DateService!=proc.ProcDate){
							seg++;
							sw.WriteLine("DTP*472*"//DTP01: 472=Service
								+"D8*"//DTP02: use D8
								+proc.ProcDate.ToString("yyyyMMdd")+"~");
						}
						//2400 DTP: Date prior placement
						if(proc.Prosthesis=="R"){//already validated date
							seg++;
							sw.WriteLine("DTP*441*"//DTP01: 441=Prior placement
								+"D8*"//DTP02: use D8
								+proc.DateOriginalProsth.ToString("yyyyMMdd")+"~");
						}
						//2400 DTP: Date ortho appliance placed. Not used.
						//2400 DTP: Date ortho appliance replaced.  Not used.
						//2400 QTY: Anesthesia quantity. Not used.
						//2400 REF: Pretermination ID. Not used.
						//2400 REF: Referral #. Not used.
						//2400 REF: Line item control number(Proc Num)
						seg++;
						sw.WriteLine("REF*6R*"//REF01: 6R=Procedure control number
							+proc.ProcNum.ToString()+"~");
						//2400 NTE: Line note
						if(proc.ClaimNote!=""){
							seg++;
							sw.WriteLine("NTE*ADD*"//NTE01: ADD=Additional info
								+Sout(proc.ClaimNote,80)+"~");
						}
						//2420A NM1: Rendering provider name. Only if different from the claim.
						if(claim.ProvTreat!=proc.ProvNum){
							provTreat=Providers.ListLong[Providers.GetIndexLong(proc.ProvNum)];
							seg++;
							sw.Write("NM1*82*"//82=rendering prov
								+"1*"//NM102: 1=person
								+Sout(provTreat.LName,35)+"*"//NM103: LName
								+Sout(provTreat.FName,25)+"*"//NM104: FName
								+Sout(provTreat.MI,25)+"*"//NM105: MiddleName
								+"*"//NM106: not used
								+"*");//NM107: suffix. not used
							if(provTreat.UsingTIN){
								sw.Write("24*");//NM108: 24=EIN, 34=SSN
							}
							else{
								sw.Write("34*");
							}
							sw.WriteLine(Sout(provTreat.SSN,80)+"~");//NM109: ID
						//2420A PRV: Rendering provider information
							seg++;
							sw.WriteLine("PRV*"
								+"PE*"//PRV01: PE=Performing
								+"ZZ*"//PRV02: ZZ=mutually defined taxonomy code
								+GetTaxonomy(provTreat.Specialty)+"~");//PRV03: Taxonomy code
							seg++;
						//2420A REF: Rendering provider secondary ID
							sw.WriteLine("REF*1E*"//REF01: 1E=license #
								+Sout(provTreat.StateLicense,30)+"~");
						//2420A REF: Rendering provider secondary ID
							seg+=WriteProv_REF(sw,provTreat,(string)claimAr[0,i]);
						}
					}//for int i claimProcs
					#endregion
					if(i==claimAr.GetLength(1)-1//if this is the last loop
						|| claimAr[0,i].ToString() != claimAr[0,i+1].ToString())//or the payorID will change
					{
						hasFooter=true;
					}
					else{
						hasFooter=false;
					}
					if(hasFooter){
						//Transaction Trailer
						seg++;
						sw.WriteLine("SE*"
							+seg.ToString()+"*"//SE01: Total segments, including ST & SE
							+transactionNum.ToString().PadLeft(4,'0')+"~");
						if(i<claimAr.GetLength(1)-1){//if this is not the last loop
							transactionNum++;
						}
						//sw.WriteLine();
					}
				}//for claimAr i
				//Functional Group Trailer
				sw.WriteLine("GE*"+transactionNum.ToString()+"*"//GE01: Number of transaction sets included
					+interchangeNum.ToString()+"~");//GE02: Group Control number. Must be identical to GS06
				//Interchange Control Trailer
				sw.WriteLine("IEA*1*"//IEA01: number of functional groups
					+interchangeNum.ToString().PadLeft(9,'0')+"~");//IEA02: Interchange control number
			}//using sw
			CopyToArchive(saveFile);
			return true;
		}

		///<summary>Usually ZZ(mutually defined), but sometimes 30(TIN)</summary>
		private static string GetISA05(Clearinghouse clearhouse){
			if(clearhouse.ReceiverID=="660610220")//Inmediata
			{
				return "30";//TIN
			}
			else{
				return "ZZ";//mutually defined
			}
		}
		
		///<summary>Usually Jordan Sparks's TIN, but sometimes the clinic's TIN.</summary>
		private static string GetISA06(Clearinghouse clearhouse){
			if(clearhouse.ReceiverID=="660610220"){//Inmediata
				Provider defProv=Providers.ListLong[Providers.GetIndexLong(Prefs.GetInt("PracticeDefaultProv"))];
				return Sout(defProv.SSN,15,15);//TIN or SSN of default provider.  This field should be later added to clearhouse
			}
			else{
				return "810624427".PadRight(15,' ');//TIN of Jordan Sparks.
			}
		}

		///<summary>Usually ZZ(mutually defined), but sometimes 30(TIN)</summary>
		private static string GetISA07(Clearinghouse clearhouse){
			if(clearhouse.ReceiverID=="330989922"//WebClaim
				|| clearhouse.ReceiverID=="660610220")//Inmediata
			{
				return "30";//TIN
			}
			else{
				return "ZZ";//mutually defined
			}
		}

		/// <summary>Usually Jordan Sparks's TIN, but sometimes the clinic's TIN.</summary>
		private static string GetGS02(Clearinghouse clearhouse){
			if(clearhouse.ReceiverID=="660610220"){//Inmediata
				Provider defProv=Providers.ListLong[Providers.GetIndexLong(Prefs.GetInt("PracticeDefaultProv"))];
				return Sout(defProv.SSN,15,2);//TIN or SSN of default provider.  This field should be later added to clearhouse
			}
			else{
				return "810624427";//TIN of Jordan Sparks.
			}
		}

		/// <summary>Usually just returns the clearhouse.ReceiverID, except Georgia Medicaid returns payorID.</summary>
		private static string GetGS03(Clearinghouse clearhouse){
			switch(clearhouse.ReceiverID){
				case "100000"://Georgia Medicaid
					return "77034";
			}
			return clearhouse.ReceiverID;
		}

		///<summary>Usually writes the name information for Open Dental. But for inmediata clearinghouse, writes practice info.</summary>
		private static void Write1000A_NM1(StreamWriter sw,Clearinghouse clearhouse){
			if(clearhouse.ReceiverID=="660610220"){//Inmediata
				Provider defProv=Providers.ListLong[Providers.GetIndexLong(Prefs.GetInt("PracticeDefaultProv"))];
				//TIN or SSN of default provider.  This field should be later added to clearhouse
				sw.WriteLine("NM1*41*"//NM101: 41=submitter
					+"2*"//NM102: 2=nonperson??
					+Sout(Prefs.GetString("PracticeTitle"),35,1)+"*"//NM103:Submitter Name
					+"****46*"//NM108: 46 indicates ETIN
					+Sout(defProv.SSN,80,2)+"~");//NM109: ID Code. aka ETIN#.
			}
			else{
				sw.WriteLine("NM1*41*"//NM101: 41=submitter
					+"2*"//NM102: 2=nonperson
					+"OPEN DENTAL SOFTWARE*"//NM103:Submitter Name
					+"****46*"//NM108: 46 indicates ETIN
					+"810624427~");//aka ETIN#.
			}
		}

		///<summary>Usually writes the contact information for Open Dental. But for inmediata clearinghouse, writes practice contact info.</summary>
		private static void Write1000A_PER(StreamWriter sw,Clearinghouse clearhouse){
			//if this is used, MUST ++ seg:
			/*if(clearhouse.ReceiverID=="BCBSGA"){
				sw.WriteLine("PER*IC*"//PER01:Function code: IC=Information Contact
					+"JORDAN SPARKS*"//PER02:Name
					+"ED*"//PER03:Comm Number Qualifier: ED=Electronic Data
					//this is probably wrong:
					+"810624427~");//PER04:Comm Number
			}*/
			if(clearhouse.ReceiverID=="660610220"){//Inmediata
				sw.WriteLine("PER*IC*"//PER01:Function code: IC=Information Contact
					+Sout(Prefs.GetString("PracticeTitle"),60,1)+"*"//PER02:Name. Practice title
					+"TE*"//PER03:Comm Number Qualifier: TE=Telephone
					+Sout(Prefs.GetString("PracticePhone"),80,1)+"~");//PER04:Comm Number. aka telephone number
			}
			else{
				sw.WriteLine("PER*IC*"//PER01:Function code: IC=Information Contact
					+"JORDAN SPARKS*"//PER02:Name
					+"TE*"//PER03:Comm Number Qualifier: TE=Telephone
					+"8776861248~");//PER04:Comm Number. aka telephone number
			}
		}

		//I believe this will always be the payorID, so no special function required
		/*private static string Get1000B_NM109(Clearinghouse clearhouse,object payorID){
			switch(clearhouse.ReceiverID){
				case "BCBSGA":
					return "SB601";//this is also their Electronic PayorID
				case "100000"://Georgia Medicaid
					return "77034";
			}
			return "";
		}*/

		///<summary>Returns the Provider Taxonomy code for the given specialty.</summary>
		public static string GetTaxonomy(DentalSpecialty specialty){
			//must return a string with length of at least one char.
			string spec=" ";
			switch(specialty){
				case DentalSpecialty.General:       spec="1223G0001X";	break;
				case DentalSpecialty.Hygienist:			spec="124Q00000X";	break;//?
				case DentalSpecialty.PublicHealth:  spec="1223D0001X";	break;
				case DentalSpecialty.Endodontics:   spec="1223E0200X";	break;
				case DentalSpecialty.Pathology:     spec="1223P0106X";	break;
				case DentalSpecialty.Radiology:     spec="1223D0008X";	break;
				case DentalSpecialty.Surgery:       spec="1223S0112X";	break;
				case DentalSpecialty.Ortho:         spec="1223X0400X";	break;
				case DentalSpecialty.Pediatric:     spec="1223P0221X";	break;
				case DentalSpecialty.Perio:         spec="1223P0300X";	break;
				case DentalSpecialty.Prosth:        spec="1223P0700X";	break;
				case DentalSpecialty.Denturist:			spec=" ";						break;
				case DentalSpecialty.Assistant:			spec=" ";						break;
				case DentalSpecialty.LabTech:				spec=" ";						break;
			}
			return spec;
		}

		///<summary>This is depedent only on the electronic payor id # rather than the clearinghouse.  Used for billing prov and also for treating prov. Returns the number of segments written</summary>
		private static int WriteProv_REF(StreamWriter sw,Provider prov,string payorID){
			int retVal=0;
			ElectID electID=ElectIDs.GetID(payorID);
			//if(electID==null){
			//	return;
			//}
			if(electID!=null && electID.IsMedicaid){
				retVal++;
				sw.WriteLine("REF*"
					+"1D*"//REF01: ID qualifier. 1D=Medicaid
					+Sout(prov.MedicaidID,30,1)+"~");//REF02. ID number
			}
			//I don't think there would be additional id's if Medicaid, but just in case, no return.
			ProviderIdent[] provIdents=ProviderIdents.GetForPayor(prov.ProvNum,payorID);
			for(int i=0;i<provIdents.Length;i++){
				retVal++;
				sw.WriteLine("REF*"
					//REF01: ID qualifier, a 2 letter code representing type
					+GetProvTypeQualifier(provIdents[i].SuppIDType)+"*"
					+provIdents[i].IDNumber+"~");//REF02. ID number
			}
			return retVal;
		}

		private static string GetProvTypeQualifier(ProviderSupplementalID provType){
			switch(provType){
				case ProviderSupplementalID.BlueCross:
					return "1A";
				case ProviderSupplementalID.BlueShield:
					return "1B";
				case ProviderSupplementalID.SiteNumber:
					return "G5";
				case ProviderSupplementalID.CommercialNumber:
					return "G2";
			}
			return "";
		}

		private static string GetGender(PatientGender patGender){
			switch(patGender){
				case PatientGender.Male:
					return "M";
				case PatientGender.Female:
					return "F";
				case PatientGender.Unknown:
					return "U";
			}
			return "";
		}

		private static string GetRelat(Relat relat){
			switch(relat){
				case(Relat.Self):
					return "18";
				case(Relat.Child):
					return "19";
				case(Relat.Dependent):
					return "76";
				case(Relat.Employee):
					return "20";
				case(Relat.HandicapDep):
					return "22";
				case(Relat.InjuredPlaintiff):
					return "41";
				case(Relat.LifePartner):
					return "53";
				case(Relat.SignifOther):
					return "29";
				case(Relat.Spouse):
					return "01";
			}
			return "";
		}

		private static string GetStudent(string studentStatus){
			if(studentStatus=="P"){
				return "P";
			}
			if(studentStatus=="F"){
				return "F";
			}
			return "N";//either N or blank
		}

		private static string GetPlaceService(PlaceOfService place){
			switch(place){
				case PlaceOfService.AdultLivCareFac:
					return "33";
				case PlaceOfService.FederalHealthCenter:
					return "50";
				case PlaceOfService.InpatHospital:
					return "21";
				case PlaceOfService.MilitaryTreatFac:
					return "26";
				case PlaceOfService.MobileUnit:
					return "15";
				case PlaceOfService.Office:
				case PlaceOfService.OtherLocation:
					return "11";
				case PlaceOfService.OutpatHospital:
					return "22";
				case PlaceOfService.PatientsHome:
					return "12";
				case PlaceOfService.PublicHealthClinic:
					return "71";
				case PlaceOfService.RuralHealthClinic:
					return "72";
				case PlaceOfService.School:
					return "03";
				case PlaceOfService.SkilledNursFac:
					return "31";
			}
			return "11";
		}

		private static string GetRelatedCauses(Claim claim){
			if(claim.AccidentRelated==""){
				return "";
			}
			//even though the specs let you submit all three types at once, we only allow one of the three
			if(claim.AccidentRelated=="A"){
				return "AA:::"+Sout(claim.AccidentST,2,2);
			}
			else if(claim.AccidentRelated=="E"){
				return "EM";
			}
			else{// if(claim.AccidentRelated=="O"){
				return "OA";
			}
		}

		private static string GetArea(Procedure proc,ProcedureCode procCode){
			if(procCode.TreatArea==TreatmentArea.Arch){
				if(proc.Surf=="U"){
					return "01";
				}
				if(proc.Surf=="L"){
					return "02";
				}
			}
			if(procCode.TreatArea==TreatmentArea.Mouth){
				return "";
			}
			if(procCode.TreatArea==TreatmentArea.Quad){
				if(proc.Surf=="UR"){
					return "10";
				}
				if(proc.Surf=="UL"){
					return "20";
				}
				if(proc.Surf=="LR"){
					return "40";
				}
				if(proc.Surf=="LL"){
					return "30";
				}
			}
			if(procCode.TreatArea==TreatmentArea.Sextant){
				//we will assume that these are very rarely billed to ins
				return "";
			}
			if(procCode.TreatArea==TreatmentArea.Surf){
				return "";//might need to enhance this
			}
			if(procCode.TreatArea==TreatmentArea.Tooth){
				return "";//might need to enhance this
			}
			if(procCode.TreatArea==TreatmentArea.ToothRange){
				//already checked for blank tooth range
				if(Tooth.IsMaxillary(proc.ToothRange.Split(',')[0])){
					return "01";
				}
				else{
					return "02";
				}
			}
			return "";
		}
		
		///<summary>Converts any string to an acceptable format for X12. Converts to all caps and strips off all invalid characters. Optionally shortens the string to the specified length and/or makes sure the string is long enough by padding with spaces.</summary>
		private static string Sout(string intputStr,int maxL,int minL){
			string retStr=intputStr.ToUpper();
			//Debug.Write(retStr+",");
			retStr=Regex.Replace(retStr,//replaces characters in this input string
				//Allowed: !"&'()+,-./;?=(space)#   # is actually part of extended character set
				"[^\\w!\"&'\\(\\)\\+,-\\./;\\?= #]",//[](any single char)^(that is not)\w(A-Z or 0-9) or one of the above chars.
				"");
			retStr=Regex.Replace(retStr,"[_]","");//replaces _
			if(maxL!=-1){
				if(retStr.Length>maxL){
					retStr=retStr.Substring(0,maxL);
				}
			}
			if(minL!=-1){
				if(retStr.Length<minL){
					retStr=retStr.PadRight(minL,' ');
				}
			}
			//Debug.WriteLine(retStr);
			return retStr;
		}

		///<summary>Converts any string to an acceptable format for X12. Converts to all caps and strips off all invalid characters. Optionally shortens the string to the specified length and/or makes sure the string is long enough by padding with spaces.</summary>
		private static string Sout(string str,int maxL){
			return Sout(str,maxL,-1);
		}

		///<summary>Converts any string to an acceptable format for X12. Converts to all caps and strips off all invalid characters. Optionally shortens the string to the specified length and/or makes sure the string is long enough by padding with spaces.</summary>
		private static string Sout(string str){
			return Sout(str,-1,-1);
		}

		///<summary>Returns a string describing all missing data on this claim.  Claim will not be allowed to be sent electronically unless this string comes back empty.</summary>
		public static string GetMissingData(ClaimSendQueueItem queueItem){
			string retVal="";
			Clearinghouse clearhouse=Clearinghouses.GetClearinghouse(queueItem.ClearinghouseNum);
			//if(clearhouse.SenderID==""){
			//	retVal+="Clearinghouse Office ID";
			//}
			if(clearhouse.ReceiverID==""){
				if(retVal!="")
					retVal+=",";
				retVal+="Clearinghouse ID";
			}
			if(Prefs.GetString("PracticeTitle")==""){//1000A NM103 min length=1
				if(retVal!="")
					retVal+=",";
				retVal+="Practice Title";
			}
			//1000A NM109 min length=2
			if(Providers.ListLong
				[Providers.GetIndexLong(Prefs.GetInt("PracticeDefaultProv"))].SSN.Length<2)
			{
				if(retVal!="")
					retVal+=",";
				retVal+="Default Prov SSN/TIN";
			}
			if(Prefs.GetString("PracticePhone").Length!=10){//1000A PER04 min length=1.
				//But 10 digit phone is required by WebMD and is universally assumed 
				if(retVal!="")
					retVal+=",";
				retVal+="Practice Phone";
			}
			//1000B NM109: Receiver ID Code. aka ETIN#. Min length=2
			/*if(clearhouse.IDCode.Length<2){
				if(retVal!="")
					retVal+=",";
				retVal+="Clearinghouse IDCode";
			}*/
			ArrayList queueItems=new ArrayList();
			queueItems.Add(queueItem);
			object[,] claimAr=Claims.GetX12TransactionInfo(queueItems);//just to get prov. Needs work.
			Provider billProv=Providers.ListLong[Providers.GetIndexLong((int)claimAr[1,0])];
			if(billProv.LName==""){
				if(retVal!="")
					retVal+=",";
				retVal+="Billing Prov LName";
			}
			if(billProv.FName==""){
				if(retVal!="")
					retVal+=",";
				retVal+="Billing Prov FName";
			}
			if(billProv.SSN.Length<2){
				if(retVal!="")
					retVal+=",";
				retVal+="Billing Prov SSN";
			}
			if(billProv.StateLicense==""){
				if(retVal!="")
					retVal+=",";
				retVal+="Billing Prov Lic #";
			}
			Claim claim=Claims.GetClaim(queueItem.ClaimNum);
			Provider treatProv=Providers.ListLong[Providers.GetIndexLong(claim.ProvTreat)];
			if(treatProv.LName==""){
				if(retVal!="")
					retVal+=",";
				retVal+="Treating Prov LName";
			}
			if(treatProv.FName==""){
				if(retVal!="")
					retVal+=",";
				retVal+="Treating Prov FName";
			}
			if(treatProv.SSN.Length<2){
				if(retVal!="")
					retVal+=",";
				retVal+="Treating Prov SSN";
			}
			if(treatProv.StateLicense==""){
				if(retVal!="")
					retVal+=",";
				retVal+="Treating Prov Lic #";
			}
			if(Prefs.GetString("PracticeAddress")==""){
				if(retVal!="")
					retVal+=",";
				retVal+="Practice Address";
			}
			if(Prefs.GetString("PracticeCity").Length<2){
				if(retVal!="")
					retVal+=",";
				retVal+="Practice City";
			}
			if(Prefs.GetString("PracticeST").Length!=2){
				if(retVal!="")
					retVal+=",";
				retVal+="Practice State(2 char)";
			}
			if(Prefs.GetString("PracticeZip").Length<3){
				if(retVal!="")
					retVal+=",";
				retVal+="Practice Zip";
			}
			InsPlan insPlan=InsPlans.GetPlan(claim.PlanNum,new InsPlan[] {});
			Carrier carrier=Carriers.GetCarrier(insPlan.CarrierNum);
			if(carrier.ElectID==""){
				if(retVal!="")
					retVal+=",";
				retVal+="Carrier ElectronicID";
			}
			if(carrier.Address==""){
				if(retVal!="")
					retVal+=",";
				retVal+="Carrier Address";
			}
			if(carrier.City.Length<2){
				if(retVal!="")
					retVal+=",";
				retVal+="Carrier City";
			}
			if(carrier.State.Length!=2){
				if(retVal!="")
					retVal+=",";
				retVal+="Carrier State(2 char)";
			}
			if(carrier.Zip.Length<3){
				if(retVal!="")
					retVal+=",";
				retVal+="Carrier Zip";
			}
			//Provider Idents:
			ProviderSupplementalID[] providerIdents=ElectIDs.GetRequiredIdents(carrier.ElectID);
			for(int i=0;i<providerIdents.Length;i++){
				if(!ProviderIdents.IdentExists(providerIdents[i],billProv.ProvNum,carrier.ElectID)){
					if(retVal!="")
						retVal+=",";
					retVal+="Billing Prov Supplemental ID:"+providerIdents[i].ToString();
				}
			}
			if(insPlan.SubscriberID.Length<2){
				if(retVal!="")
					retVal+=",";
				retVal+="SubscriberID";
			}
			Patient patient=Patients.GetPat(claim.PatNum);
			Patient subscriber=Patients.GetPat(insPlan.Subscriber);
			if(claim.PatNum != insPlan.Subscriber//if patient is not subscriber
				&& claim.PatRelat==Relat.Self){//and relat is self
				if(retVal!="")
					retVal+=",";
				retVal+="Relationship";
			}
			//if(patient.SSN.Length<2){//required when patient is not subscriber
			//	if(retVal!="")
			//		retVal+=",";
			//	retVal+="Patient SSN";
			//}
			/*Turns out we don't really need this info for subscriber, but only for patient
			if(subscriber.Address==""){
				if(retVal!="")
					retVal+=",";
				retVal+="Subscriber Address";
			}
			if(subscriber.City.Length<2){
				if(retVal!="")
					retVal+=",";
				retVal+="Subscriber City";
			}
			if(subscriber.State.Length!=2){
				if(retVal!="")
					retVal+=",";
				retVal+="Subscriber State";
			}
			if(subscriber.Zip.Length<3){
				if(retVal!="")
					retVal+=",";
				retVal+="Subscriber Zip";
			}
			if(subscriber.Birthdate.Year<1880){
				if(retVal!="")
					retVal+=",";
				retVal+="Subscriber Birthdate";
			}
			*/
			if(patient.Address==""){
				if(retVal!="")
					retVal+=",";
				retVal+="Patient Address";
			}
			if(patient.City.Length<2){
				if(retVal!="")
					retVal+=",";
				retVal+="Patient City";
			}
			if(patient.State.Length!=2){
				if(retVal!="")
					retVal+=",";
				retVal+="Patient State";
			}
			if(patient.Zip.Length<3){
				if(retVal!="")
					retVal+=",";
				retVal+="Patient Zip";
			}
			if(patient.Birthdate.Year<1880){
				if(retVal!="")
					retVal+=",";
				retVal+="Patient Birthdate";
			}
			if(claim.AccidentRelated=="A" && claim.AccidentST.Length!=2){//auto accident with no state
				if(retVal!="")
					retVal+=",";
				retVal+="Auto accident State";
			}
			if(claim.ClaimType=="PreAuth"){
				if(retVal!="")
					retVal+=",";
				retVal+="PreAuth not supported";
			}
			ClaimProc[] claimProcList=ClaimProcs.Refresh(patient.PatNum);
			ClaimProc[] claimProcs=ClaimProcs.GetForSendClaim(claimProcList,claim.ClaimNum);
			Procedure[] procList=Procedures.Refresh(claim.PatNum);
			Procedure proc;
			ProcedureCode procCode;
			for(int i=0;i<claimProcs.Length;i++){
				proc=Procedures.GetProc(procList,claimProcs[i].ProcNum);
				procCode=ProcedureCodes.GetProcCode(proc.ADACode);		
				if(procCode.TreatArea==TreatmentArea.Arch && proc.Surf==""){
					if(retVal!="")
						retVal+=",";
					retVal+=procCode.AbbrDesc+" missing arch";
				}
				if(procCode.TreatArea==TreatmentArea.ToothRange && proc.ToothRange==""){
					if(retVal!="")
						retVal+=",";
					retVal+=procCode.AbbrDesc+" tooth range";
				}
				if(procCode.IsProsth){
					if(proc.Prosthesis==""){//they didn't enter whether Initial or Replacement
						if(retVal!="")
							retVal+=",";
						retVal+=procCode.AbbrDesc+" Prosthesis";
					}
					if(proc.Prosthesis=="R"
						&& proc.DateOriginalProsth.Year<1880)
					{//if a replacement, they didn't enter a date
						if(retVal!="")
							retVal+=",";
						retVal+=procCode.AbbrDesc+" Prosth Date";
					}
				}
				treatProv=Providers.ListLong[Providers.GetIndexLong(proc.ProvNum)];
				if(treatProv.LName==""){
					if(retVal!="")
						retVal+=",";
					retVal+="Treating Prov LName";
				}
				if(treatProv.FName==""){
					if(retVal!="")
						retVal+=",";
					retVal+="Treating Prov FName";
				}
				if(treatProv.SSN.Length<2){
					if(retVal!="")
						retVal+=",";
					retVal+="Treating Prov SSN";
				}
				//will add any other checks as needed. Can't think of any others at the moment.
			}//for int i claimProcs

			
/*
			if(==""){
				if(retVal!="")
					retVal+=",";
				retVal+="";
			}*/

			return retVal;
		}

	///<summary>Copies the given file to an archive directory within the same directory as the file.</summary>
		private static void CopyToArchive(string fileName){
			string direct=Path.GetDirectoryName(fileName);
			string fileOnly=Path.GetFileName(fileName);
			if(!Directory.Exists(direct+"\\archive")){
				Directory.CreateDirectory(direct+"\\archive");
			}
			File.Copy(fileName,direct+"\\archive\\"+fileOnly,true);
		}

		

		
	



		

	}
}
