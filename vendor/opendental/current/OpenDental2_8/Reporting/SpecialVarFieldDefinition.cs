using System;

namespace OpenDental.Reporting
{
	///<summary>For retrieving special fields like page numbers.</summary>
	public class SpecialVarFieldDefinition:FieldDefinition{
		///<summary></summary>
		public SpecialVarType SpecialVarType;

		///<summary></summary>
		public SpecialVarFieldDefinition(string name,FieldValueType valueType,SpecialVarType specialVarType){
			Kind=FieldKind.SpecialVarField;
			Name=name;
			FormulaName="{@"+name+"}";
			ValueType=valueType;
			SpecialVarType=specialVarType;
		}

		/*///<summary>Gets the formatted value of this field.</summary>
		public string GetValue(DataTable dataTable,int col,string formatString){
			if(SpecialVarType==SpecialVarType.PageNumber){
				return 
			}
			else if(SpecialVarType==SpecialVarType.PageNofM){

			}
			else if(SpecialVarType==SpecialVarType.PrintDate){

			}
			return "";
		}*/


	}

	///<summary>Specifies the special field type in the SpecialVarType property of the SpecialVarFieldDefinition class.</summary>
	public enum SpecialVarType{
		///<summary>Field returns "Page [current page number] of [total page count]" formula. Not functional yet.</summary>
		PageNofM,
		///<summary>Field returns the current page number.</summary>
		PageNumber,
		///<summary>Field returns the current date.</summary>
		PrintDate
	}


}
