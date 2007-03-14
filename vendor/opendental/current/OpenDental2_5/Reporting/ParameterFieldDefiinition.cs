using System;
using System.Windows.Forms;

namespace OpenDental.Reporting
{
	///<summary>Holds information about a parameter used in the report.  A parameter is a string that can be used in a query that will be replaced by user-provided data before the query is sent.  For instance, ?date might be replaced with 2004-17-02.</summary>
	public class ParameterFieldDefinition:FieldDefinition{
		///<summary>The type of value that the parameter can accept.</summary>
		private ParameterValueKind ValueKind;
		///<summary>The value of the parameter. This can be a string or date (or later a number, currency, Boolean, etc). It is</summary>
		private object CurrentValue;
		///<summary>The value in text form as it will be sent to the database.</summary>
		public string OutputValue;
		
		///<summary></summary>
		///<param name="name">The unique formula name to assign to this parameter.</param>
		/// <param name="valueKind">The type of value that this parameter can accept.</param>
		public ParameterFieldDefinition(string name, ParameterValueKind valueKind){
//fix: need to ensure uniqueness of name.
			Kind=FieldKind.ParameterField;
			Name=name;
			ValueKind=valueKind;
			ValueType=FieldValueType.StringField;
		}

		///<summary></summary>
		public ParameterFieldDefinition(string name, ParameterValueKind valueKind, object myValue){
			Kind=FieldKind.ParameterField;
			Name=name;
			ValueKind=valueKind;
			ValueType=FieldValueType.StringField;
			if(!ApplyValue(myValue))
				MessageBox.Show("Invalid value.");
		}

		///<summary>Applies a value to the specified parameter field of a report.  Returns false if unsuccessful.</summary>
		///<param name="myCurrentValue">The value to apply. Can be date or string (or later a number, currency, or boolean).</param>
		public bool ApplyValue(object myCurrentValue){
			if(ValueKind==ParameterValueKind.StringParameter){
				try{
					OutputValue=POut.PString((string)myCurrentValue);
					CurrentValue=myCurrentValue;
				}
				catch{
					return false;
				}
				return true;
			}
			if(ValueKind==ParameterValueKind.DateParameter){
				try{
					OutputValue=POut.PDate((DateTime)myCurrentValue);
					CurrentValue=myCurrentValue;
				}
				catch{
					return false;
				}
				return true;
			}
			return false;
		}

	}

	///<summary>Specifies the parameter value kind in the ParameterValueKind property of the ParameterField class.  Number, currency, and boolean will be added later.</summary>
	public enum ParameterValueKind{
		///<summary>Parameter takes a date value.</summary>
		DateParameter,
		///<summary>Parameter takes a string value.</summary>
		StringParameter
	}

}
