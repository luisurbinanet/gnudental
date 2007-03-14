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
		public static void SendData(Patient pat){
			ProgramProperties.GetForProgram();
			ProgramProperties.GetCur("InfoFile path");
			string infoFile=ProgramProperties.Cur.PropertyValue;
			if(pat!=null){
				try{
					using(StreamWriter sw=new StreamWriter(infoFile,false)){
						sw.WriteLine(pat.LName+", "+pat.FName
							+"  "+pat.Birthdate.ToShortDateString()
							+"  ("+pat.PatNum.ToString()+")");
						//patientID can be any string format, max 8 char.
						//There is no validation to ensure that length is 8 char or less.
						ProgramProperties.GetCur("Enter 0 to use PatientNum, or 1 to use ChartNum");
						if(ProgramProperties.Cur.PropertyValue=="0"){
							sw.WriteLine("PN="+pat.PatNum.ToString());
						}
						else{
							sw.WriteLine("PN="+pat.ChartNumber);
						}
						sw.WriteLine("PN="+pat.PatNum.ToString());
						sw.WriteLine("LN="+pat.LName);
						sw.WriteLine("FN="+pat.FName);
						sw.WriteLine("BD="+pat.Birthdate.ToShortDateString());
						if(pat.Gender==PatientGender.Female)
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










