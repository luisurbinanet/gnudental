using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace OpenDental.Bridges{
	/// <summary></summary>
	public class Dexis{

		/// <summary></summary>
		public Dexis(){
			
		}

		///<summary>Sends data for Patient.Cur to the InfoFile and launches the program.</summary>
		public static void SendData(){
			ProgramProperties.GetForProgram();
			ProgramProperties.GetCur("InfoFile path");
			string infoFile=ProgramProperties.Cur.PropertyValue;
			if(Patients.PatIsLoaded){
				try{
					using(StreamWriter sw=new StreamWriter(infoFile,false)){
						sw.WriteLine(Patients.Cur.LName+", "+Patients.Cur.FName
							+"  "+Patients.Cur.Birthdate.ToShortDateString()
							+"  ("+Patients.Cur.PatNum.ToString()+")");
						//patientID can be any string format, max 8 char.
						//There is no validation to ensure that length is 8 char or less.
						ProgramProperties.GetCur("Enter 0 to use PatientNum, or 1 to use ChartNum");
						if(ProgramProperties.Cur.PropertyValue=="0"){
							sw.WriteLine("PN="+Patients.Cur.PatNum.ToString());
						}
						else{
							sw.WriteLine("PN="+Patients.Cur.ChartNumber);
						}
						sw.WriteLine("PN="+Patients.Cur.PatNum.ToString());
						sw.WriteLine("LN="+Patients.Cur.LName);
						sw.WriteLine("FN="+Patients.Cur.FName);
						sw.WriteLine("BD="+Patients.Cur.Birthdate.ToShortDateString());
						if(Patients.Cur.Gender==PatientGender.Female)
							sw.WriteLine("SX=F");
						else
							sw.WriteLine("SX=M");
					}
					Process.Start(Programs.Cur.Path,"@"+infoFile);
				}
				catch{
					MessageBox.Show(Programs.Cur.Path+" is not available.");
				}
			}//if patient is loaded
			else{
				try{
					Process.Start(Programs.Cur.Path);//should start Dexis without bringing up a pt.
				}
				catch{
					MessageBox.Show(Programs.Cur.Path+" is not available.");
				}
			}
		}

	}
}










