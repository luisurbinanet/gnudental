using System;
using System.Collections;

namespace OpenDental.Reporting
{
	///<summary>Contains collections of all the FieldDefinition objects in an ODReport.  These are generally read-only.  The best place to access them is directly from the FieldObject which has a property called DataSource.  DataSource can be an instance of any of the classes derived from FieldDefinition.</summary>
	public class DataDefinition{
		//FormulaFieldDefinitions FormulaFields ;
		///<summary></summary>
		public ParameterFieldDefinitions ParameterFields;
		//SummaryFields

		///<summary></summary>
		public DataDefinition(){
			//FormulaFields=new FormulaFieldDefinitions();
			ParameterFields=new ParameterFieldDefinitions();
		}



	}

}
