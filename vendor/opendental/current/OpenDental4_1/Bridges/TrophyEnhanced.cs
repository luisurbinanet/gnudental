using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
//using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OpenDental.Bridges{
	/// <summary></summary>
	public class TrophyEnhanced{

		/// <summary></summary>
		public TrophyEnhanced(){
			
		}

		///<summary>Launches the program using command line.  It is confirmed that there is no space after the -P or -N</summary>
		public static void SendData(Patient pat){
			ProgramProperties.GetForProgram();
			if(pat!=null){
				if(pat.TrophyFolder==""){
					MessageBox.Show("You must first enter a value for the Trophy Folder in the Patient Edit Window.");
					return;
				}
				ProgramProperties.GetCur("Storage Path");
				string comline="-P"+ProgramProperties.Cur.PropertyValue+@"\";
				comline+=pat.TrophyFolder;
				comline+=" -N"+pat.LName+","+pat.FName;
				comline=comline.Replace("\"","");//gets rid of any quotes
				comline=comline.Replace("'","");//gets rid of any single quotes
				//MessageBox.Show(comline);
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










