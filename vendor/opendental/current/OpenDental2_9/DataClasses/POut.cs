using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Converts various datatypes into strings formatted correctly for MySQL.</summary>
	public class POut{
		///<summary></summary>
		public static string PBool (bool myBool){
			if (myBool==true){
				return "1";
			}
			else{
				return "0";
			}
		}

		///<summary></summary>
		public static string PByte (byte myByte){
			return myByte.ToString();
		}

		///<summary></summary>
		public static string PDateT(DateTime myDateT){
			try{
				return myDateT.ToString("yyyy-MM-dd HH:mm:ss",new DateTimeFormatInfo());
			}
			catch{
				return "";//this actually saves zero's to the database
			}
		}

		///<summary></summary>
		public static string PDate(DateTime myDate){
			try{
				return myDate.ToString("yyyy-MM-dd",new DateTimeFormatInfo());
			}
			catch{
				//return "0000-00-00";
				return "";//this saves zeros to the database
			}
		}

		///<summary></summary>
		public static string PDouble (double myDouble){
			return myDouble.ToString();
		}

		///<summary></summary>
		public static string PInt (int myInt){
			return myInt.ToString();
		}

		///<summary></summary>
		public static string PFloat(float myFloat){
			return myFloat.ToString();
		}

		///<summary></summary>
		public static string PString(string myString){
			if(myString==null){
				return "";
			}
			StringBuilder strBuild=new StringBuilder();
			for(int i=0;i<myString.Length;i++){
				switch(myString.Substring(i,1)){
					case "'": strBuild.Append(@"\'");	break;// ' replaced by \'
					case @"\": strBuild.Append(@"\\"); break;//single \ replaced by \\
					case "\r": strBuild.Append(@"\r"); break;//carriage return(usually followed by new line)
					case "\n": strBuild.Append(@"\n"); break;//new line
					case "\t": strBuild.Append(@"\t"); break;//tab
					default: strBuild.Append(myString.Substring(i,1)); break;
				}
			}
			//The old slow way of doing it:
			/*string newString="";
			for(int i=0;i<myString.Length;i++){
				switch (myString.Substring(i,1)){
					case "'": newString+=@"\'"; break;
					case @"\": newString+=@"\\"; break;//single \ replaced by \\
					case "\r": newString+=@"\r"; break;//carriage return(usually followed by new line)
					case "\n": newString+=@"\n"; break;//new line
					case "\t": newString+=@"\t"; break;//tab
						//case "%": newString+="\\%"; break;//causes errors because only ambiguous in LIKE clause
						//case "_": newString+="\\_"; break;//see above
					default : newString+=myString.Substring(i,1); break;
				}//end switch
			}//end for*/
			//MessageBox.Show(strBuild.ToString());
			return strBuild.ToString();
		}

		///<summary></summary>
		public static string PTime (string myTime){
			return DateTime.Parse(myTime).ToString("HH:mm:ss");
		}

	}

	


}










