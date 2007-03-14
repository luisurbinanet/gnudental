using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace OpenDental.Bridges{
	/// <summary></summary>
	public class VixWin{

		/// <summary></summary>
		public VixWin(){
			
		}

		///<summary>Sends data for Patient.Cur by command line interface.</summary>
		public static void SendData(Patient pat){
			ProgramProperties.GetForProgram();
			if(pat==null){
				return;
			}
			//Example: c:\vixwin\vixwin -I 123ABC -N Bill^Smith
			string info="-I ";
			ProgramProperties.GetCur("Enter 0 to use PatientNum, or 1 to use ChartNum");
			if(ProgramProperties.Cur.PropertyValue=="0"){
				info+=pat.PatNum.ToString();
			}
			else{
				info+=pat.ChartNumber;//max 64 char
			}
			info+=" -N "+pat.FName.Replace(" ","")+"^"+pat.LName.Replace(" ","");//no spaces allowed
			//MessageBox.Show(info);
			try{
				Process.Start(Programs.Cur.Path,info);
			}
			catch{
				MessageBox.Show(Programs.Cur.Path+" is not available.");
			}
		}
			

	}
}










