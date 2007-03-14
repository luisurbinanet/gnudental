using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace OpenDental.Bridges{
	/// <summary></summary>
	public class Planmeca{

		/// <summary></summary>
		public Planmeca(){
			
		}

		///<summary>Launches the program using the patient.Cur data.</summary>
		public static void SendData(Patient pat){
			//DxStart.exe ”PatientID” ”FamilyName” ”FirstName” ”BirthDate”
			//this should strip " from fields, but I didn't bother
			ProgramProperties.GetForProgram();
			if(pat==null){
				MessageBox.Show("Please select a patient first");
				return;
			}
			string info="";
			//Patient id can be any string format
			ProgramProperties.GetCur("Enter 0 to use PatientNum, or 1 to use ChartNum");
			if(ProgramProperties.Cur.PropertyValue=="0"){
				info+="\""+pat.PatNum.ToString()+"\" ";
			}
			else{
				info+="\""+pat.ChartNumber+"\" ";
			}
			info+="\""+pat.LName+"\" "
				+"\""+pat.FName+"\" "
				+"\""+pat.Birthdate.ToShortDateString()+"\"";
			try{
				Process.Start(Programs.Cur.Path,info);
			}
			catch{
				MessageBox.Show(Programs.Cur.Path+" is not available.");
			}
			
		}

	}
}










