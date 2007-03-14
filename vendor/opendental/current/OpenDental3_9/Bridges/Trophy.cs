using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
//using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OpenDental.Bridges{
	/// <summary></summary>
	public class Trophy{

		/// <summary></summary>
		public Trophy(){
			
		}

		///<summary>Launches the program using a combination of command line characters and the patient.Cur data.</summary>
		public static void SendData(Patient pat){
			ProgramProperties.GetForProgram();
			if(pat!=null){
				ProgramProperties.GetCur("Storage Path");
				string comline="-P"+ProgramProperties.Cur.PropertyValue+@"\";
				//Patient id can be any string format
				ProgramProperties.GetCur("Enter 0 to use PatientNum, or 1 to use ChartNum");
				if(ProgramProperties.Cur.PropertyValue=="0"){
					comline+=pat.PatNum.ToString();
				}
				else{
					comline+=pat.ChartNumber;
				}
				comline+=" -N"+pat.LName+", "+pat.FName;
				comline=comline.Replace("\"","");//gets rid of any quotes
				comline=comline.Replace("'","");//gets rid of any single quotes
				try{
					Process.Start(Programs.Cur.Path,comline);
				}
				catch{
					MessageBox.Show(Programs.Cur.Path+" is not available.");
				}
			}//if patient is loaded
			else{
				try{
					Process.Start(Programs.Cur.Path);//should start Trophy without bringing up a pt.
				}
				catch{
					MessageBox.Show(Programs.Cur.Path+" is not available.");
				}
			}
		}

	}
}










