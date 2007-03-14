using System;

namespace OpenDental.Reporting
{
	///<summary>Provides properties and methods for retrieving information and setting options for any named formula field in a report.  This is still in the testing stages and is not complete yet.</summary>
	public class FormulaFieldDefinition:FieldDefinition{
		///<summary>The text of the formula including all formatting characters like carriage returns.</summary>
		public string Text;

		///<summary></summary>
		public FormulaFieldDefinition(){
			
		}

	}

}
