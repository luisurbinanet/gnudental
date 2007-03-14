using System;
using System.Data;

namespace OpenDental.Reporting
{
	///<summary>Holds properties for a summary field used in the report. The ValueType must be double and the SummarizedField.Kind must be DataTableField, or nothing will be printed.  Also, .</summary>
	public class SummaryFieldDefinition:FieldDefinition{
		///<summary>The summary operation type.</summary>
		public SummaryOperation Operation;
		///<summary>The summarized field's FieldDefinition object.</summary>
		public FieldDefinition SummarizedField;
		//<summary>This is used durring the printing process when doing calculations.  Once all fields have been looped through, this field should hold the value that needs to be displayed.</summary>
		//public double CurrentValue;

		///<summary></summary>
		public SummaryFieldDefinition(string name,SummaryOperation operation,FieldDefinition summarizedField){
			Kind=FieldKind.SummaryField;
			Name=name;
			FormulaName="{@"+name+"}";
			ValueType=FieldValueType.DoubleField;
			Operation=operation;
			SummarizedField=summarizedField;
		}

		///<summary>Once a dataTable has been set, this method can be run to get the value of this field.  It will still need to be formatted. It loops through all records to get this value.</summary>
		public double GetValue(DataTable dataTable,int col){
			if(SummarizedField.Kind!=FieldKind.DataTableField){
				return 0;
			}
			double retVal=0;
			for(int i=0;i<dataTable.Rows.Count;i++){
				if(Operation==SummaryOperation.Sum){
					retVal+=PIn.PDouble(dataTable.Rows[i][col].ToString());
						//PIn.PDouble(Report.ReportTable.Rows[i][Report.DataFields.IndexOf(fieldObject.DataSource.Name)].ToString())
				}
				else if(Operation==SummaryOperation.Count){
					retVal++;
				}
			}
			return retVal;
		}

	}

	///<summary></summary>
	public enum SummaryOperation{
		//Average Summary returns the average of a field.
		///<summary>Summary counts the number of values, from the field.</summary>
		Count,
		//DistinctCount Summary returns the number of none repeating values, from the field. 
		//Maximum Summary returns the largest value from the field. 
		//Median Summary returns the middle value in a sequence of numeric values. 
		//Minimum Summary returns the smallest value from the field. 
		//Percentage Summary returns as a percentage of the grand total summary. 
		///<summary>Summary returns the total of all the values for the field.</summary>
		Sum
	}
	


}
