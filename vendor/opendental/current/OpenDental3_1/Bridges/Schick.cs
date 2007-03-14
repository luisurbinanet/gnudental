using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows.Forms;

namespace OpenDental.Bridges{
	///<summary>Provides bridging functionality to Schick CDR.</summary>
	public class Schick{
		private static object cdrApp;
		private static object exam;

		///<summary>Default constructor</summary>
		public Schick(){

		}

		///<summary>Declare managed prototype for unmanaged function.</summary>
		[System.Runtime.InteropServices.DllImport("User32.dll")]
		public static extern bool SetForegroundWindow(int hndRef);
		
		///<summary>Launches the main Patient Document window of Schick.</summary>
		public static void SendData(){
			if(!Patients.PatIsLoaded){
				return;
			}
			try{
				//Late bound COM object so that we don't have to add a reference
				//first define an Application type
				Type ApplicationType=Type.GetTypeFromProgID("CDR.Application");
				//test to see if user has ever opened an exam window open
				if(cdrApp==null || exam==null){//if not, then create one
					//create an instance application type
					cdrApp=Activator.CreateInstance(ApplicationType);
					//create an ExamDocument from the Application
					exam=ApplicationType.InvokeMember("CreateExamDocument"
						,BindingFlags.InvokeMethod,null,cdrApp,null);
				}
				//Gets the ExamDocument type 
        Type ExamDocumentType=Type.GetTypeFromHandle(Type.GetTypeHandle(exam));
				//Get the handle for the exam window
				object examhWnd=ExamDocumentType.InvokeMember("hWnd"
					,BindingFlags.GetProperty,null,exam,null);
				//test to see if an exam window currently exists
				if(examhWnd==null){//if not, then create one
					exam=ApplicationType.InvokeMember("CreateExamDocument"
						,BindingFlags.InvokeMethod,null,cdrApp,null);
				}
				//Force exam window to the foreground
				SetForegroundWindow((int)examhWnd);
				//set exam.Visible = true
				ExamDocumentType.InvokeMember("Visible"
					,BindingFlags.SetProperty,null,exam,new Object[] {true});
				//Patient ID can be any string format.
				//Determine patientID
				string patientID="";
				ProgramProperties.GetForProgram();
				ProgramProperties.GetCur("Enter 0 to use PatientNum, or 1 to use ChartNum");
				if(ProgramProperties.Cur.PropertyValue=="0"){
					patientID=Patients.Cur.PatNum.ToString();
				}
				else{
					patientID=Patients.Cur.ChartNumber;
				}
				//Assemble the parameters
				object[] patInfo=new object[]
				{
					Patients.Cur.LName,
					Patients.Cur.FName,
					patientID
				};
				// Try to load this patient
				object result=ExamDocumentType.InvokeMember("LoadPatient"
					,BindingFlags.InvokeMethod,null,exam,patInfo);
				//if successfully loaded, then return
				if((bool)result){
					return;
				}
				//Otherwise, create a new patient in CDR
				//get the new patient object from the ExamDocument
				object patient=ExamDocumentType.InvokeMember("Patient" 
					,BindingFlags.GetProperty,null,exam,null); 
				//Get the type of PatientType
        Type PatientType=Type.GetTypeFromHandle(Type.GetTypeHandle(patient)); 
				//set the first name property of patient
				PatientType.InvokeMember("FirstName"
					,BindingFlags.SetProperty,null,patient,new object[]{Patients.Cur.FName});
				//set the last name property of patient
				PatientType.InvokeMember("LastName"
					,BindingFlags.SetProperty,null,patient,new object[]{Patients.Cur.LName});
				//set the id property of patient
				PatientType.InvokeMember("IDNumber"
					,BindingFlags.SetProperty,null,patient,new object[]{patientID});
				//Now call new exam dialog
				ExamDocumentType.InvokeMember("NewExam"
					,BindingFlags.InvokeMethod,null,exam,new object[] {false});
			}
			catch{
				MessageBox.Show("Error calling Schick CDR Dicom.");
			}

			/*
		///<summary>Launches the main Patient Document window of Schick using CDRStart.ext program.</summary>
		public static void SendData(){
			if(!Patients.PatIsLoaded){
				return;
			}
			string arguments=
				"/LastName "+Patients.Cur.LName
				+" /FirstName "+Patients.Cur.FName
				+" /IDNumber ";
			ProgramProperties.GetForProgram();
			ProgramProperties.GetCur("Enter 0 to use PatientNum, or 1 to use ChartNum");
			if(ProgramProperties.Cur.PropertyValue=="0"){
				arguments+=Patients.Cur.PatNum.ToString();
			}
			else{
				arguments+=Patients.Cur.ChartNumber;
			}
			Process.Start(
				@"C:\Documents and Settings\jordan\My Documents\FreeDental Extras\Bridge Info\Schick\CDRStart\CDRStart\Release\CDRStart.exe",arguments);				
				//Application.StartupPath+"\\CDRStart.exe",arguments);
		}*/


		}

		





	}
}
