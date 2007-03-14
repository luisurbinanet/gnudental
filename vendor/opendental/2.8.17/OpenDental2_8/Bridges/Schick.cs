using System;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows.Forms;

namespace OpenDental.Bridges{
	///<summary>Provides bridging functionality to Schick CDR.</summary>
	public class Schick{

		///<summary>Default constructor</summary>
		public Schick(){

		}

		///<summary>Declare managed prototype for unmanaged function.</summary>
		[DllImport("User32.dll")]
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
				//then create an instance of that type
				object cdrApp=Activator.CreateInstance(ApplicationType);
				//create an ExamDocument from the Application
				object exam=ApplicationType.InvokeMember("CreateExamDocument"
					,BindingFlags.InvokeMethod,null,cdrApp,null);
				//Gets the return type for Application.CreateExamDocument which is ExamDocument type
				Type ExamDocumentType=Type.GetTypeFromProgID("CDR.ExamDocument");
				//set the ExamDocument.Visible to true
				ExamDocumentType.InvokeMember("Visible"
					,BindingFlags.SetProperty,null,exam,new Object[] {true});
				//Get the handle for the exam window
				object examhWnd=ExamDocumentType.InvokeMember("hWnd"
					,BindingFlags.GetProperty,null,exam,null);
				//Force the window to the foreground
				SetForegroundWindow((int)examhWnd);
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
				//Get the type of PatientType
				Type PatientType=Type.GetTypeFromProgID("CDRData.CDRPatient");
				//get the new patient object from the ExamDocument
				object patient=ExamDocumentType.InvokeMember("Patient"
					,BindingFlags.GetProperty,null,exam,null);
				//set the first name property of patient
				PatientType.InvokeMember("FirstName"
					,BindingFlags.SetProperty,null,patient,new object[]{Patients.Cur.FName.ToString()});
				//set the last name property of patient
				PatientType.InvokeMember("LastName"
					,BindingFlags.SetProperty,null,patient,new object[]{Patients.Cur.LName.ToString()});
				//set the first name property of patient
				PatientType.InvokeMember("IDNumber"
					,BindingFlags.SetProperty,null,patient,new object[]{Patients.Cur.PatNum.ToString()});
				//Now call new exam dialog
				ExamDocumentType.InvokeMember("NewExam"
					,BindingFlags.InvokeMethod,null,exam,new object[] {false});
			}
			catch{
				MessageBox.Show("Error calling Schick CDR Dicom.");
			}


		}

		





	}
}
