using System;
using System.Collections;
//using System.Drawing.Printing;
using System.Text.RegularExpressions; 
using System.Windows.Forms;
using OpenDental.ReportingOld2;

namespace OpenDental{
	
	///<summary>Corresponds to the reportparameter table in the db.</summary>
	public struct ReportParameter{
		///<summary>Primary key.</summary>
		public int ReportParameterNum;
		///<summary>Foreign key to report.ReportNum</summary>
		public int ReportNum;
		///<summary>The name as it will show in the query, but without the preceding question mark.</summary>
		public string Name;
		///<summary>The type of value that the parameter can accept.</summary>
		public FieldValueType ValueType;
		///<summary>Comma delimited list of values that will show in the dialog prefilled when it asks the user for values.  If this string is empty, then the length of the ArrayList will be 0 to specify that the initial value is blank. For enum and defcat, these are numbers.</summary>
		public string DefaultValues;
		///<summary>The text that prompts the user what to enter for this parameter.</summary>
		public string PromptingText;
		///<summary>The snippet of SQL that will be repeated once for each value supplied by the user, connected with OR's, and surrounded by parentheses.</summary>
		public string Snippet;
		///<summary>If the ValueKind is EnumField, then this specifies which type of enum. It is the string name of the type.</summary>
		public EnumType EnumerationType;
		///<summary>If ValueKind is DefParameter, then this specifies which DefCat.</summary>
		public DefCat DefCategory;
	}

	

}









