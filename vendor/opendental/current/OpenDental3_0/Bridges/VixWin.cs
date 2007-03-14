using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace OpenDental.Bridges{
	/// <summary></summary>
	public class VixWin{

		/// <summary></summary>
		public VixWin(){
			
		}

		


		///<summary>Sends data for Patient.Cur to the QuikLink directory. No further action is required.</summary>
		public static void SendData(){
			ProgramProperties.GetForProgram();
			ProgramProperties.GetCur("QuikLink directory.");
			string quikLinkDir=ProgramProperties.Cur.PropertyValue;
			if(!Patients.PatIsLoaded){
				return;
			}
			if(!Directory.Exists(quikLinkDir)){
				MessageBox.Show(quikLinkDir+" is not a valid folder.");
				return;
			}
			try{
				string patID;
				ProgramProperties.GetCur("Enter 0 to use PatientNum, or 1 to use ChartNum");
				if(ProgramProperties.Cur.PropertyValue=="0"){
					patID=Patients.Cur.PatNum.ToString().PadLeft(6,'0');
				}
				else{
					patID=Patients.Cur.ChartNumber.PadLeft(6,'0');
				}
				if(patID.Length>6){
					MessageBox.Show("Patient ID is longer than six digits, so link failed.");
					return;
				}
				string fileName=quikLinkDir+patID+".DDE";
				//MessageBox.Show(fileName);
				using(StreamWriter sw=new StreamWriter(fileName,false)){
					sw.WriteLine("\""+Patients.Cur.FName+"\","
						+"\""+Patients.Cur.LName+"\","
						+"\""+patID+"\"");
				}
			}
			catch{
				MessageBox.Show("Error creating file.");
			}
		}
			

	}
}










