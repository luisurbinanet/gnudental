using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace OpenDental.Bridges{
	/// <summary></summary>
	public class TigerView{
		private static string iniFile;

		/// <summary></summary>
		public TigerView(){
			
		}

		[DllImport("kernel32")]//this is the windows function for working with ini files.
    private static extern long WritePrivateProfileString(string section,string key,string val,string filePath);

		private static void WriteValue(string key,string val){
			WritePrivateProfileString("Slave",key,val,iniFile);
		}


		///<summary>Sends data for Patient.Cur to the Tiger1.ini file and launches the program.</summary>
		public static void SendData(){
			ProgramProperties.GetForProgram();
			ProgramProperties.GetCur("Tiger1.ini path");
			iniFile=ProgramProperties.Cur.PropertyValue;
			if(Patients.PatIsLoaded){
				if(!File.Exists(iniFile)){
					MessageBox.Show("Could not find "+iniFile);
					return;
				}
				WriteValue("LastName",Patients.Cur.LName);
				WriteValue("FirstName",Patients.Cur.FName);
				//Patient Id can be any string format.
				ProgramProperties.GetCur("Enter 0 to use PatientNum, or 1 to use ChartNum");
				if(ProgramProperties.Cur.PropertyValue=="0"){
					WriteValue("PatientID",Patients.Cur.PatNum.ToString());
				}
				else{
					WriteValue("PatientID",Patients.Cur.ChartNumber);
				}
				WriteValue("PatientSSN",Patients.Cur.SSN);
				//WriteValue("SubscriberSSN",Patients.Cur);
				if(Patients.Cur.Gender==PatientGender.Female)
					WriteValue("Gender","Female");
				else
					WriteValue("Gender","Male");
				WriteValue("DOB",Patients.Cur.Birthdate.ToString("MM/dd/yy"));
				//WriteValue("ClaimID",Patients.Cur);??huh
				WriteValue("AddrStreetNo",Patients.Cur.Address);
				//WriteValue("AddrStreetName",Patients.Cur);??
				//WriteValue("AddrSuiteNo",Patients.Cur);??
				WriteValue("AddrCity",Patients.Cur.City);
				WriteValue("AddrState",Patients.Cur.State);
				WriteValue("AddrZip",Patients.Cur.Zip);
				WriteValue("PhHome",Patients.Cur.HmPhone);
				WriteValue("PhWork",Patients.Cur.WkPhone);
				try{
					Process.Start(Programs.Cur.Path,Programs.Cur.CommandLine);
				}
				catch{
					MessageBox.Show(Programs.Cur.Path+" is not available.");
				}
			}//if patient is loaded
			else{
				try{
					Process.Start(Programs.Cur.Path);//should start TigerView without bringing up a pt.
				}
				catch{
					MessageBox.Show(Programs.Cur.Path+" is not available.");
				}
			}
		}

	}
}










