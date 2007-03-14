using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace OpenDental.Bridges{
	/// <summary></summary>
	public class Sirona{
		private static string iniFile;

		/// <summary></summary>
		public Sirona(){
			
		}

		[DllImport("kernel32")]//this is the windows function for writing to ini files.
    private static extern long WritePrivateProfileString(string section,string key,string val
			,string filePath);

		[DllImport("kernel32")]//this is the windows function for reading from ini files.
    private static extern int GetPrivateProfileString(string section,string key,string def
			,StringBuilder retVal,int size,string filePath);

		private static string ReadValue(string section,string key){
			StringBuilder strBuild=new StringBuilder(255);
			int i=GetPrivateProfileString(section,key,"",strBuild,255,iniFile);
				return strBuild.ToString();
			}

		///<summary>Sends data for Patient to a mailbox file and launches the program.</summary>
		public static void SendData(Patient pat){
			ProgramProperties.GetForProgram();
			if(pat!=null){
				//read file C:\sidexis\sifiledb.ini
				iniFile=Path.GetDirectoryName(Programs.Cur.Path)+"\\sifiledb.ini";
				if(!File.Exists(iniFile)){
					MessageBox.Show(iniFile+" could not be found. Is Sidexis installed properly?");
					return;
				}
				//read FromStation0 | File to determine location of comm file (sendBox) (siomin.sdx)
				//example:
				//[FromStation0]
				//File=F:\PDATA\siomin.sdx  //only one sendBox on entire network.
				string sendBox=ReadValue("FromStation0","File");
				//read Multistations | GetRequest (=1) to determine if station can take xrays.
				//but we don't care at this point, so ignore
				//set OfficeManagement | OffManConnected = 1 to make sidexis ready to accept a message.
				WritePrivateProfileString("OfficeManagement","OffManConnected","1",iniFile);
				//line formats: first two bytes are the length of line including first two bytes and \r\n
				//each field is terminated by null (byte 0).
				//Append U token to siomin.sdx file
				StringBuilder line=new StringBuilder();
				char nTerm=Convert.ToChar(0);
				line.Append("U");//U signifies Update patient in sidexis. Gets ignored if new patient.
				line.Append(nTerm);
				line.Append(pat.LName);
				line.Append(nTerm);
				line.Append(pat.FName);
				line.Append(nTerm);
				line.Append(pat.Birthdate.ToString("dd.MM.yyyy"));
				line.Append(nTerm);
				//leave initial patient id blank. This updates sidexis to patNums used in Open Dental
				line.Append(nTerm);
				line.Append(pat.LName);
				line.Append(nTerm);
				line.Append(pat.FName);
				line.Append(nTerm);
				line.Append(pat.Birthdate.ToString("dd.MM.yyyy"));
				line.Append(nTerm);
				//Patient id:
				ProgramProperties.GetCur("Enter 0 to use PatientNum, or 1 to use ChartNum");
				if(ProgramProperties.Cur.PropertyValue=="0"){
					line.Append(pat.PatNum.ToString());
				}
				else{
					line.Append(pat.ChartNumber);
				}
				line.Append(nTerm);
				if(pat.Gender==PatientGender.Female)
					line.Append("F");
				else
					line.Append("M");
				line.Append(nTerm);
				line.Append(Providers.GetAbbr(pat.GetProvNum()));
				line.Append(nTerm);
				line.Append("OpenDental");
				line.Append(nTerm);
				line.Append("Sidexis");
				line.Append(nTerm);
				line.Append("\r\n");
				line.Insert(0,IntToCharArray(line.Length+2));//the 2 accounts for these two chars.
				using(StreamWriter sw=new StreamWriter(sendBox,true)){//true to append
					sw.Write(line.ToString());
				}
				//Append N token to siomin.sdx file
				//N signifies create New patient in sidexis. If patient already exists,
				//then it simply updates any old data.
				line=new StringBuilder();
				line.Append("N");
				line.Append(nTerm);
				line.Append(pat.LName);
				line.Append(nTerm);
				line.Append(pat.FName);
				line.Append(nTerm);
				line.Append(pat.Birthdate.ToString("dd.MM.yyyy"));
				line.Append(nTerm);
				//Patient id:
				if(ProgramProperties.Cur.PropertyValue=="0"){
					line.Append(pat.PatNum.ToString());
				}
				else{
					line.Append(pat.ChartNumber);
				}
				line.Append(nTerm);
				if(pat.Gender==PatientGender.Female)
					line.Append("F");
				else
					line.Append("M");
				line.Append(nTerm);
				line.Append(Providers.GetAbbr(pat.GetProvNum()));
				line.Append(nTerm);
				line.Append("OpenDental");
				line.Append(nTerm);
				line.Append("Sidexis");
				line.Append(nTerm);
				line.Append("\r\n");
				line.Insert(0,IntToCharArray(line.Length+2));
				using(StreamWriter sw=new StreamWriter(sendBox,true)){
					sw.Write(line.ToString());
				}
				//Append A token to siomin.sdx file
				//A signifies Autoselect patient. 
				line=new StringBuilder();
				line.Append("A");
				line.Append(nTerm);
				line.Append(pat.LName);
				line.Append(nTerm);
				line.Append(pat.FName);
				line.Append(nTerm);
				line.Append(pat.Birthdate.ToString("dd.MM.yyyy"));
				line.Append(nTerm);
				if(ProgramProperties.Cur.PropertyValue=="0"){
					line.Append(pat.PatNum.ToString());
				}
				else{
					line.Append(pat.ChartNumber);
				}
				line.Append(nTerm);
				line.Append(SystemInformation.ComputerName);
				line.Append(nTerm);
				line.Append(DateTime.Now.ToString("dd.MM.yyyy"));
				line.Append(nTerm);
				line.Append(DateTime.Now.ToString("HH.mm.ss"));
				line.Append(nTerm);
				line.Append("OpenDental");
				line.Append(nTerm);
				line.Append("Sidexis");
				line.Append(nTerm);
				line.Append("0");//0=no image selection
				line.Append(nTerm);
				line.Append("\r\n");
				line.Insert(0,IntToCharArray(line.Length+2));
				using(StreamWriter sw=new StreamWriter(sendBox,true)){
					sw.Write(line.ToString());
				}
			}//if patient is loaded
			//Start Sidexis.exe whether patient loaded or not.
			try{
				Process.Start(Programs.Cur.Path);
			}
			catch{
				MessageBox.Show(Programs.Cur.Path+" is not available.");
			}
		}

		///<summary>this is my way of converting to hex. I don't like their file format at all. Always returns an array of 2 chars.  Used when measuring length of line.</summary>
		private static char[] IntToCharArray(int toConvert){
			char[] retVal=new char[2];
			retVal[0]=Convert.ToChar((byte)Math.IEEERemainder(toConvert,256));
			retVal[1]=Convert.ToChar((byte)(toConvert/256));//rounds down automatically
			return retVal;
		}

	}
}










