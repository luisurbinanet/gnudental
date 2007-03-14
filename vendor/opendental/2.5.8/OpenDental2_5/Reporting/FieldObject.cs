using System;
using System.Drawing;
using System.Windows.Forms;


namespace OpenDental.Reporting{

	///<summary>Represents field data for one record in the details section of the report. Gets drawn multiple times.</summary>
	public class FieldObject:ReportObject{
		///<summary>The data can come from a wide variety of sources. Each different kind of FieldDefinition will have different methods and properties for accessing the data. Need to test DataSource.Kind to determine for example whether it is a DataTableField or a FormulaField.</summary>
		public FieldDefinition DataSource;
		//<summary>Temporarily used to store the name of the datafield.  Will be replaced by DataSource.Name if the DataSource.Kind==</summary>
		//public string DataField;
		///<summary></summary>
		public Font Font;
		///<summary></summary>
		public ContentAlignment TextAlign;
		///<summary>A C# format string that specifies how to print dates, times, numbers, and currency based on the country or on a custom format.</summary>
		///<remarks>There are a LOT of options for this string.  Look in C# help under Standard Numeric Format Strings, Custom Numeric Format Strings, Standard DateTime Format Strings, Custom DateTime Format Strings, and Enumeration Format Strings.  Once users are allowed to edit reports, we will assemble a help page with all of the common options. The best options are "n" for number, and "d" for date.</remarks>
		public string FormatString;
		//<summary>The value type of data that this field displays, such as string or date.</summary>
		//public FieldValueType ValueType; This is in the DataSource.ValueType
		///<summary>If the ValueType is EnumField, then this specifies which type of enum.</summary>
		public EnumerationType EnumType;
		///<summary></summary>
		public Color TextColor;
		///<summary>Suppresses this field if the field for the previous record was the same.</summary>
		public bool SuppressIfDuplicate;

		//public bool

		///<summary></summary>
		public FieldObject(int thisSectionIndex,Point thisLocation,Size thisSize,FieldDefinition thisDataSource,Font thisFont,ContentAlignment thisTextAlign,string thisFormatString){
			SectionIndex=thisSectionIndex;
			Location=thisLocation;
			Size=thisSize;
			DataSource=thisDataSource;
			Font=thisFont;
			TextAlign=thisTextAlign;
			FormatString=thisFormatString;
			//defaults:
			TextColor=Color.Black;
			Kind=ReportObjectKind.FieldObject;
		}

	}

	

	///<summary>This will eventually hold all of the enumeration types that are used in the database.  The report can then use this to display human-readable data.</summary>
	public enum EnumerationType{
		
	}

}
