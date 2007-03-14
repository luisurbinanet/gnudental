using System;

namespace OpenDental
{
	///<summary></summary>
	public class Family{
		///<summary></summary>
		public Family(){
			
		}

		///<summary>List of patients in the family.</summary>
		public Patient[] List;

		///<summary></summary>
		public string GetNameInFamLF(int myPatNum){
			string retStr="";
			for(int i=0;i<List.Length;i++){
				if(List[i].PatNum==myPatNum){
					if(List[i].Preferred==""){
						retStr=List[i].LName+", "+List[i].FName+" "+List[i].MiddleI; 
					}
					else{
						retStr=List[i].LName+", '"+List[i].Preferred+"' "+List[i].FName+" "+List[i].MiddleI;
					}
				}
			}
			return retStr;
		}

		///<summary>Gets last, (preferred) first middle</summary>
		public string GetNameInFamLFI(int myi){
			string retStr="";
			if(List[myi].Preferred==""){
				retStr=List[myi].LName+", "+List[myi].FName+" "+List[myi].MiddleI; 
			}
			else{
				retStr=List[myi].LName+", '"+List[myi].Preferred+"' "+List[myi].FName+" "+List[myi].MiddleI;
			}
			return retStr;
		}

		///<summary></summary>
		public string GetNameInFamFL(int myPatNum){
			string retStr="";
			for(int i=0;i<List.Length;i++){
				if(List[i].PatNum==myPatNum){
					if(List[i].Preferred==""){
						retStr=List[i].FName+" "+List[i].MiddleI+" "+List[i].LName; 
					}
					else{
						retStr="'"+List[i].Preferred+"' "+List[i].FName+" "+List[i].MiddleI+" "+List[i].LName;
					}
				}
			}
			return retStr;
		}

		///<summary>Gets (preferred)first middle last</summary>
		public string GetNameInFamFLI(int myi){
			string retStr="";
			if(List[myi].Preferred==""){
				retStr=List[myi].FName+" "+List[myi].MiddleI+" "+List[myi].LName; 
			}
			else{
				retStr="'"+List[myi].Preferred+"' "+List[myi].FName+" "+List[myi].MiddleI+" "+List[myi].LName;
			}
			return retStr;
		}

		///<summary></summary>
		public int GetIndex(int patNum){
			int retVal=-1;//will return -1 if not found
			for(int i=0;i<List.Length;i++){
				if(List[i].PatNum==patNum){
					retVal=i;
				}
			}
			return retVal;
		}

		///<summary>Gets a copy of a specific patient from within the family. Does not make a call to the database.</summary>
		public Patient GetPatient(int patNum){
			Patient retVal=null;
			for(int i=0;i<List.Length;i++){
				if(List[i].PatNum==patNum){
					retVal=List[i].Copy();
				}
			}
			return retVal;
		}

	}
}
