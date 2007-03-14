using System;

namespace OpenDental.Reporting
{
	///<summary></summary>
	public class DataTableFieldDefinition:FieldDefinition{

		///<summary></summary>
		public DataTableFieldDefinition(string name,FieldValueType valueType){
			Kind=FieldKind.DataTableField;
			Name=name;
			ValueType=valueType;
		}

	}
}
