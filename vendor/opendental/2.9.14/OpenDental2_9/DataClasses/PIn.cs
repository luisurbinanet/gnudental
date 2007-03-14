using System;
using System.Collections;

namespace OpenDental{
	
	/*=========================================================================================
	=================================== class PIn ===========================================*/
	///<summary>Converts strings coming in from the database into the appropriate type.</summary>
	///<remarks>P was originally short for Parameter because it was replacing the data adapter parameters.  Using strings instead of parameters is much easier to debug.  This class will be replaced with an IConvertible interface with better naming as soon as we have time, but it is still very functional.</remarks>
	public class PIn{
		///<summary></summary>
		public static bool PBool (string myString){
			return myString=="1";
		}

		///<summary></summary>
		public static byte PByte (string myString){
			if(myString==""){
				return 0;
			}
			else{
				return System.Convert.ToByte(myString);
			}
		}

		///<summary></summary>
		public static DateTime PDate(string myString){
			if(myString=="")
				return DateTime.MinValue;
			try{
				return (DateTime.Parse(myString));
			}
			catch{
				return DateTime.MinValue;
			}
		}

		///<summary></summary>
		public static DateTime PDateT(string myString){
			if(myString=="")
				return DateTime.MinValue;
			try{
				return (DateTime.Parse(myString));
			}
			catch{
				return DateTime.MinValue;
			}
		}

		///<summary></summary>
		public static double PDouble (string myString){
			if (myString==""){
				return 0;
			}
			else{
				try{
					return System.Convert.ToDouble(myString);
				}
				catch{
					//MessageBox.Show(myString);
					return 0;
				}
			}

		}

		///<summary></summary>
		public static int PInt (string myString){
			if(myString==""){
				return 0;
			}
			else{
				return System.Convert.ToInt32(myString);
			}
		}

		///<summary></summary>
		public static float PFloat(string myString){
			if(myString==""){
				return 0;
			}
			//try{
				return System.Convert.ToSingle(myString);
			//}
			//catch{
			//	return 0;
			//}
		}

		///<summary></summary>
		public static string PString (string myString){
			return myString;
		}
		
		///<summary></summary>
		public static string PTime (string myTime){
			return DateTime.Parse(myTime).ToString("HH:mm:ss");
		}

	}

	


}










