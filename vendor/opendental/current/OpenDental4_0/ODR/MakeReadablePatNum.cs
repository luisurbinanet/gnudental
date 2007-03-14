using System;

namespace ODR{
	///<summary></summary>
	public class MakeReadablePatNum{
		///<summary>Constructor</summary>
		public MakeReadablePatNum(){
			//todo: fill hashtable with names
		}

		public string Get(string patNum){
			if(patNum=="1"){
				return "one1";
			}
			else if(patNum=="2"){
				return "two2";
			}
			return "other3";
		}
		
	}

}
