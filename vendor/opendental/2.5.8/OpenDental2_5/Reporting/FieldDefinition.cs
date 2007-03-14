using System;

namespace OpenDental.Reporting
{
	///<summary>Base class providing generic properties for various kinds of field definition objects. Used to describe the data for a fieldObject. Use the constructor for each inherited class instead of creating an instance of this class.</summary>
	public class FieldDefinition{
		///<summary>The kind of field. The inherited type of FieldDefinition, like FormulaField or DataTableField.</summary>
		public FieldKind Kind;
		///<summary>The field definition unique formula name as used in other formulas.  It includes the curly brackets and the preceding ? or @. For instance: {?SomeParameter} or {@SomeFormula} or {SomeDataField}.  It does not have to match the ReportObject Name, but it does have to be unique.</summary>
		public string FormulaName;
		///<summary>This is not the same as the ReportObject Name.  This is the Formula name without any of the punctuation. For instance, if the FormulaName is {?SomeParameter}, than the Name of the FieldDefinition would be SomeParameter. For a dataTableField, this would be the column name.</summary>
		public string Name;
		///<summary>The value type of field, like string or datetime.</summary>
		public FieldValueType ValueType;
		
		
	}

	///<summary>Specifies the field kind in the Kind property of the FieldDefinition class and all classes derived from FieldDefinition.</summary>
	public enum FieldKind{
		///<summary></summary>
		DataTableField,
		///<summary></summary>
		FormulaField,
		///<summary></summary>
		ParameterField,
		///<summary></summary>
		SpecialVarField,
		///<summary></summary>
		SummaryField
		//RunningTotalField
		//GroupNameField
	}

	///<summary>Specifies the field value type in the ValueType property of the FieldDefinition class.</summary>
	public enum FieldValueType{
		///<summary>Field contains either a true or false value, or a 1 or 0.</summary>
		BooleanField, 
		///<summary>Field contains a double value, typically used for currency. Can be shown with or without decimals or currency symbol.</summary>
		DoubleField,
		///<summary>Field contains a DateTime value. If only the date or time are needed, then the other part of the value is simply ignored.</summary>
		DateTimeField,
		///<summary>Field contains an enumeration (values represented by numbers in the database) The string representation of the value will be shown in this field.  Specify the type of enumeration in the EnumType field of the FieldObject.</summary>
		EnumField,
		///<summary>Field contains an integer with a value between -2147483648 and 2147483647.</summary>
		IntField,
		///<summary>Field contains a PatNum which will be displayed as LName, FName. This reduces the number of joins required in an SQL statement.</summary>
		PatNumField,
		///<summary>Field contains string data.</summary>
		StringField		
	}

}
