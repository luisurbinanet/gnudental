using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace OpenDental.Bridges{
	/// <summary></summary>
	public class Apteryx{

		/// <summary></summary>
		public Apteryx(){
			
		}

		///<summary>Launches the program using a combination of command line characters and the patient.Cur data.</summary>
		public static void SendData(){
			ProgramProperties.GetForProgram();
			if(Patients.PatIsLoaded){
				string info="\""+Patients.Cur.LName+", "+Patients.Cur.FName+"::";
				if(Patients.Cur.SSN.Length==9){
					info+=Patients.Cur.SSN.Substring(0,3)+"-"
						+Patients.Cur.SSN.Substring(3,2)+"-"
						+Patients.Cur.SSN.Substring(5,4);
				}
				//Patient id can be any string format
				ProgramProperties.GetCur("Enter 0 to use PatientNum, or 1 to use ChartNum");
				if(ProgramProperties.Cur.PropertyValue=="0"){
					info+="::"+Patients.Cur.PatNum.ToString();
				}
				else{
					info+="::"+Patients.Cur.ChartNumber;
				}
				info+="::"+Patients.Cur.Birthdate.ToShortDateString()+"::";
				if(Patients.Cur.Gender==PatientGender.Female)
					info+="F";
				else
					info+="M";
				info+="\"";
				try{
					Process.Start(Programs.Cur.Path,Programs.Cur.CommandLine+info);
				}
				catch{
					MessageBox.Show(Programs.Cur.Path+" is not available, or there is an error in the command line options.");
				}
			}//if patient is loaded
			else{
				try{
					Process.Start(Programs.Cur.Path);//should start Apteryx without bringing up a pt.
				}
				catch{
					MessageBox.Show(Programs.Cur.Path+" is not available.");
				}
			}
		}

	}
}










