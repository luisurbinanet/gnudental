using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using VBbridges;

namespace OpenDental.Bridges{
	/// <summary></summary>
	public class DrCeph{
		
		/// <summary></summary>
		public DrCeph(){
			
		}

		///<summary>Uses a VB dll to launch.</summary>
		public static void SendData(Patient pat){
			if(pat==null){
				MessageBox.Show("Please select a patient first.");
				return;
			}
			ProgramProperties.GetForProgram();
			ProgramProperties.GetCur("Enter 0 to use PatientNum, or 1 to use ChartNum");
			string patID="";
			if(ProgramProperties.Cur.PropertyValue=="0"){
				patID=pat.PatNum.ToString();
			}
			else{
				patID=pat.ChartNumber;
			}
			try{
				VBbridges.DrCeph.Launch(patID);
			}
			catch{
				MessageBox.Show("DrCeph not responding.  It might not be installed properly.");
			}
		}

		

	}
}










