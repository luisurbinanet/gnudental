using System;
using System.Data;
using System.Windows.Forms;

namespace OpenDental.Reporting
{
	/*
	///<summary>Describes many different types of data sources.  Describes the data for a FieldObject. Used for parameters in queries, etc.
	///</summary>
	public class FieldDefOld{
		///<summary>The kind of field, like FormulaField, SummaryField, or  ParameterField.</summary>
		public FieldDefKind FieldKind;
		///<summary>The field definition unique formula name as used in other formulas.  It includes the curly brackets and the preceding ? or @. For instance: {?SomeParameter} or {@SomeFormula} or {SomeDataField}.  It does not have to match the ReportObject Name, but it does have to be unique.</summary>
		public string FormulaName;
		///<summary>This is not the same as the ReportObject Name.  This is the Formula name without any of the punctuation. For instance, if the FormulaName is {?SomeParameter}, than the Name of the FieldDefinition would be SomeParameter. For a dataTableField, this would be the column name.</summary>
		public string Name;
		///<summary>The value type of field, like string or datetime.</summary>
		public FieldValueType ValueType;
		//<summary>If the ValueType is EnumField, then this specifies which type of enum.</summary>
		//public EnumerationType EnumType;


		//These are only for specific kinds:
		///<summary>eg. pagenumber</summary>
		public SpecialVarType SpecialVarType;
		///<summary>The summary operation type.</summary>
		public SummaryOperation Operation;
		///<summary>The summarized field's FieldDefinition object.</summary>
		public FieldDef SummarizedField;
		//<summary>The text of the formula including all formatting characters like carriage returns.</summary>
		//public string FormulaText;

		///<summary></summary>
		public FieldDef(FieldDefKind kind){
			FieldKind=kind;
		}

		/// <summary>Overload for DataTableFields.</summary>
		public FieldDef(string name,FieldValueType valueType){
			FieldKind=FieldDefKind.DataTableField;
			Name=name;
			ValueType=valueType;
		}

		///<summary>Overload for SummaryFields.</summary>
		public FieldDef(string name,SummaryOperation operation,FieldDef summarizedField){
			FieldKind=FieldDefKind.SummaryField;
			Name=name;
			FormulaName="{@"+name+"}";
			ValueType=FieldValueType.Number;
			Operation=operation;
			SummarizedField=summarizedField;
		}

		///<summary>Overload for SpecialVarFields.</summary>
		public FieldDef(string name,FieldValueType valueType,SpecialVarType specialVarType){
			FieldKind=FieldDefKind.SpecialVarField;
			Name=name;
			FormulaName="{@"+name+"}";
			ValueType=valueType;
			SpecialVarType=specialVarType;
		}

		

		
		

		
		
	}*/

	

	/*
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
	}*/

	

}
