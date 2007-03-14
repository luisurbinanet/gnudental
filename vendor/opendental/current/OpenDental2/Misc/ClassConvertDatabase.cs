using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Resources;
using System.Text; 
using System.Windows.Forms;

namespace OpenDental{

	public class ClassConvertDatabase{
		private System.Version FromVersion;
		private System.Version ToVersion;

		public bool Convert(string fromVersion){//return false to indicate exit app.
			//Conversions=new Conversions();
			FromVersion=new Version(fromVersion);
			ToVersion=new Version(Application.ProductVersion);
			if(FromVersion.CompareTo(ToVersion)>0){
				MessageBox.Show("Can not convert database to an older version.");
				return false;
			}
			if(FromVersion < new Version("1.0.0")){
				MessageBox.Show("Can not convert databases before version 1.0.0");
				return false;
			}
			if(FromVersion.CompareTo(new Version("2.0.1"))==0){
				MessageBox.Show("Can not convert database version 2.0.1 which was only for development purposes.");
				return false;
			}
			if(FromVersion < new Version("2.0.2")){
				if(MessageBox.Show("Your database will now be converted from version "
					+FromVersion.ToString()+" to version "+ToVersion.ToString()
					+". Please be certain you have a current backup.  "
					+"The conversion works best if you are on the server.  "
					+"It can take up to 10 minutes. ","",MessageBoxButtons.OKCancel)
					!=DialogResult.OK){
					return false;
				}
			}
			else{
				return true;//no conversion necessary
			}
			if(To1_0_1()){//begins going through the chain of conversion steps, each returning true
				MessageBox.Show("Conversion successful");
				Prefs.Refresh();
				return true;
			}
			else{
				return false;
			}
		}

		private bool To1_0_1(){//returns true if successful
			if(FromVersion < new Version("1.0.1")){
				try{
					Conversions.ArrayQueryText=new string[] 
						{"INSERT INTO preference (PrefName,ValueString) "
						+"VALUES('TreatmentPlanNote','If you have dental insurance, please be aware that THIS IS AN ESTIMATE ONLY.  Coverage may be different if your deductible has not been met, annual maximum has been met, or if your coverage table is lower than average.')"
						,"ALTER TABLE patient CHANGE Balance EstBalance DOUBLE DEFAULT '0' NOT NULL" 
						,"UPDATE preference SET ValueString = '1.0.1.0' WHERE PrefName = 'DataBaseVersion'"}; 
					if(!Conversions.SubmitQuery()) return false;
				}
				catch{
					return false;
				}
			}
			return To1_0_11();
		}

		private bool To1_0_11(){
			if(FromVersion < new Version("1.0.11")){
				try{
					Conversions.ArrayQueryText=new string[] 
						{"ALTER TABLE patient ADD MedicaidID VARCHAR(20) NOT NULL"
						,"UPDATE preference SET ValueString = '1.0.11' WHERE PrefName = 'DataBaseVersion'"}; 
					if(!Conversions.SubmitQuery()) return false;
				}
				catch{
					return false;
				}
			}
			return To2_0_2();
		}

		private bool To2_0_2(){
			if(FromVersion < new Version("2.0.2")){
				if(!File.Exists(@"convert_2_0_2.txt")){
					MessageBox.Show("convert_2_0_2.txt could not be found.");
					return false;
				}
				StreamReader sr;
				string line="";
				string cmd="";
				ArrayList AL=new ArrayList();
				try{
					sr=new StreamReader("convert_2_0_2.txt");
					while(true){
						line=sr.ReadLine();
						if(line==null){
							break;
						}
						if(line.Length==0 || line.Substring(0,1)=="#"){//could improve white space handling
							//continue;
						}
						else{
							cmd+=" "+line;
							if(cmd.Substring(cmd.Length-1)==";"){//again, need to handle white space better
								AL.Add(cmd);
								cmd="";
							}
						}
					}
					AL.Add("UPDATE preference SET ValueString = '2.0.2' WHERE PrefName = 'DataBaseVersion';"); 
					Conversions.ArrayQueryText=new string[AL.Count];
					AL.CopyTo(Conversions.ArrayQueryText);
					//MessageBox.Show(AL.Count.ToString());
					//for(int i=30;i<Conversions.ArrayQueryText.Length;i++){
					//	MessageBox.Show(Conversions.ArrayQueryText[i]);
					//}
					if(!Conversions.SubmitQuery()){
						sr.Close();
						return false;
					}
					sr.Close();
					//Add a finance charge type to adjustments:
					Defs.Refresh();
					Defs.Cur=new Def();
					Defs.Cur.Category=(int)DefCat.AdjTypes;
					Defs.Cur.ItemName="Finance Charge";
					Defs.Cur.ItemOrder=Defs.Long[(int)DefCat.AdjTypes].Length;
					Defs.Cur.ItemValue="+";
					Defs.InsertCur();
					Prefs.Cur=new Pref();
					Prefs.Cur.PrefName="FinanceChargeAdjustmentType";
					Prefs.Cur.ValueString=Defs.Cur.DefNum.ToString();
					Prefs.UpdateCur();
					//Copy address notes to each family member
					Conversions.AddressNotesVers2_0();
				}//try
				catch{
					return false;
				}
			}//if
			return To2_1_0();
		}

		private bool To2_1_0(){
			//not actually used yet
			return true;
		}

	}
}







