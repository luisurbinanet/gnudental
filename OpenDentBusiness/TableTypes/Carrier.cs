using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{

	///<summary>Every InsPlan has a Carrier.  The carrier stores the name and address.</summary>
	public class Carrier{
		///<summary>Primary key.</summary>
		public int CarrierNum;
		///<summary>Name of the carrier.</summary>
		public string CarrierName;
		///<summary>.</summary>
		public string Address;
		///<summary>Second line of address.</summary>
		public string Address2;
		///<summary>.</summary>
		public string City;
		///<summary>2 char in the US.</summary>
		public string State;
		///<summary>Postal code.</summary>
		public string Zip;
		///<summary>Includes any punctuation.</summary>
		public string Phone;
		///<summary>E-claims electronic payer id.  5 char.</summary>
		public string ElectID;
		///<summary>Do not send electronically.  It's just a default; you can still send electronically.</summary>
		public bool NoSendElect;
	}
	

	
	

}













