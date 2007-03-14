//This class is incomplete and is mostly in preparation for version 2.0

using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary></summary>
	public class Tooth{
		///<summary></summary>
		public Tooth(){
			
		}

		///<summary></summary>
		public static bool IsAnterior(string toothNum){
			if(!IsValidDB(toothNum))
				return false;
			int intTooth=ToInt(toothNum);
			if(intTooth>=6 && intTooth<=11)
				return true;
			if(intTooth>=22 && intTooth<=27)
				return true;
			return false;
		}

		///<summary></summary>
		public static bool IsAnterior(int intTooth){
			string toothNum=FromInt(intTooth);
			return IsAnterior(toothNum);
		}

		///<summary></summary>
		public static bool IsPosterior(string toothNum){
			if(!IsValidDB(toothNum))
				return false;
			int intTooth=ToInt(toothNum);
			if(intTooth>=1 && intTooth<=5)
				return true;
			if(intTooth>=12 && intTooth<=21)
				return true;
			if(intTooth>=28 && intTooth<=32)
				return true;
			return false;
		}

		///<summary></summary>
		public static bool IsPosterior(int intTooth){
			string toothNum=FromInt(intTooth);
			return IsPosterior(toothNum);
		}

		///<summary></summary>
		public static bool IsMolar(string toothNum){
			if(!IsValidDB(toothNum))
				return false;
			int intTooth=ToInt(toothNum);
			if(intTooth>=1 && intTooth<=3)
				return true;
			if(intTooth>=14 && intTooth<=19)
				return true;
			if(intTooth>=30 && intTooth<=32)
				return true;
			return false;
		}

		///<summary></summary>
		public static bool IsMolar(int intTooth){
			string toothNum=FromInt(intTooth);
			return IsMolar(toothNum);
		}

		///<summary></summary>
		public static bool IsPreMolar(string toothNum){
			if(!IsValidDB(toothNum))
				return false;
			int intTooth=ToInt(toothNum);
			if(intTooth==4 
				|| intTooth==5
				|| intTooth==12
				|| intTooth==13
				|| intTooth==20
				|| intTooth==21
				|| intTooth==28
				|| intTooth==29)
				return true;
			return false;
		}

		///<summary></summary>
		public static bool IsPreMolar(int intTooth){
			string toothNum=FromInt(intTooth);
			return IsPreMolar(toothNum);
		}

		///<summary></summary>
		public static bool IsValidEntry(string toothNum){
			//used every time user enters toothNum in procedure box.
			//this is the *ONLY* method that is designed to accept user input.
			//will be expanded later to handle international toothnum
			//Regex regex;
			//switch(culture){
				//case default:
				return IsValidDB(toothNum);//default to use US toothnums for now.
			//break;
			//}
		}

		///<summary></summary>
		public static bool IsValidDB(string toothNum){
			//intended to validate toothNum coming in from database.
			//will not handle any international tooth nums since all database teeth are in US format.
			if(toothNum==null || toothNum=="")
				return false;
			Regex regex=new Regex("^[A-Z]$");
			if(regex.IsMatch(toothNum))
				return true;
			regex=new Regex(@"^[1-9]\d?$");//matches 1 or 2 digits, leading 0 not allowed
			if(!regex.IsMatch(toothNum)){
				return false;
			}
			int intTooth=Convert.ToInt32(toothNum);
			if(intTooth>32)
				return false;
			return true;
		}

		///<summary></summary>
		public static int ToInt(string toothNum){
			//the toothNum must be validated before coming here.
			//Primary or perm are ok.  Empty and null are also ok.
			if(toothNum==null || toothNum=="")
				return -1;
			if(IsPrimary(toothNum)){
				return Convert.ToInt32(PriToPerm(toothNum));
			}
			else{
				//try{
				return Convert.ToInt32(toothNum);
				//}
				//catch{
				//	MessageBox.Show(toothNum);
				//	return -1;
				//}
			}
		}

		///<summary></summary>
		public static string FromInt(int intTooth){
			//don't need much error checking.
			string retStr="";
			retStr=intTooth.ToString();
			return retStr;
		}

		///<summary></summary>
		public static bool IsPrimary(string toothNum){
			Regex regex=new Regex("^[A-Z]$");
			return regex.IsMatch(toothNum);
		}

		///<summary></summary>
		public static string PermToPri(string toothNum){
			switch(toothNum){
				default: return "";
				case "4": return "A";
				case "5": return "B";
				case "6": return "C";
				case "7": return "D";
				case "8": return "E";
				case "9": return "F";
				case "10": return "G";
				case "11": return "H";
				case "12": return "I";
				case "13": return "J";
				case "20": return "K";
				case "21": return "L";
				case "22": return "M";
				case "23": return "N";
				case "24": return "O";
				case "25": return "P";
				case "26": return "Q";
				case "27": return "R";
				case "28": return "S";
				case "29": return "T";
			}
		}

		///<summary></summary>
		public static string PermToPri(int intTooth){
			string toothNum=FromInt(intTooth);
			return PermToPri(toothNum);
		}

		///<summary></summary>
		public static string PriToPerm(string toothNum){
			switch(toothNum){
				default: return "";
				case "A": return "4";
				case "B": return "5";
				case "C": return "6";
				case "D": return "7";
				case "E": return "8";
				case "F": return "9";
				case "G": return "10";
				case "H": return "11";
				case "I": return "12";
				case "J": return "13";
				case "K": return "20";
				case "L": return "21";
				case "M": return "22";
				case "N": return "23";
				case "O": return "24";
				case "P": return "25";
				case "Q": return "26";
				case "R": return "27";
				case "S": return "28";
				case "T": return "29";
			}
		}
		
		///<summary></summary>
		public static int ToOrdinal(string toothNum){
			//used to put perm and pri into a single array.  1-32 is perm.  33-52 is pri.
			if(IsPrimary(toothNum)){
				switch(toothNum){
					default: return -1;
					case "A": return 33;
					case "B": return 34;
					case "C": return 35;
					case "D": return 36;
					case "E": return 37;
					case "F": return 38;
					case "G": return 39;
					case "H": return 40;
					case "I": return 41;
					case "J": return 42;
					case "K": return 43;
					case "L": return 44;
					case "M": return 45;
					case "N": return 46;
					case "O": return 47;
					case "P": return 48;
					case "Q": return 49;
					case "R": return 50;
					case "S": return 51;
					case "T": return 52;
				}
			}
			else{//perm
				return ToInt(toothNum);
			}
		}
			
		///<summary></summary>
		public static bool IsMaxillary(int intTooth){
			string toothNum=FromInt(intTooth);
			return IsMaxillary(toothNum);
		}

		///<summary></summary>
		public static bool IsMaxillary(string toothNum){
			if(!IsValidDB(toothNum))
				return false;
			int intTooth=ToInt(toothNum);
			if(intTooth>=1 && intTooth<=16)
				return true;
			return false;
		}

		///<summary></summary>
		public static string SurfTidy(string surf,string toothNum){
			//yes... this would be a little more elegant with a regex
      string surfTidy="";
			bool isPosterior=false;
			if(toothNum=="" || !IsAnterior(toothNum))
				isPosterior=true;
			bool isAnterior=false;
			if(toothNum=="" || !IsPosterior(toothNum))
				isAnterior=true;
      ArrayList al=new ArrayList();
      for(int i=0;i<surf.Length;i++){
        al.Add(surf.Substring(i,1).ToUpper());
      }
      if(al.Contains((string)"M")){
        surfTidy+="M";
      } 
			if(isPosterior
				&& al.Contains((string)"O")){
        surfTidy+="O";
      } 
      if(isAnterior
				&& al.Contains((string)"I")){
        surfTidy+="I";
      } 
      if(al.Contains((string)"D")){
        surfTidy+="D";
      } 
      if(isPosterior
				&& al.Contains((string)"B")){
        surfTidy+="B";
      } 
      if(isAnterior
				&& al.Contains((string)"F")){
        surfTidy+="F";
      } 
      if(al.Contains((string)"L")){
        surfTidy+="L";
      }
      return surfTidy;      
    }


	}
}
