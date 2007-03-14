using System;

namespace OpenDental.Reporting
{
	///<summary>Every ReportObject in a ODReport must be attached to a Section.</summary>
	public class Section{
		///<summary></summary>
		public string Name;
		///<summary></summary>
		public int Height;
		///<summary>Specifies which kind, like ReportHeader, or GroupFooter.</summary>
		public AreaSectionKind Kind;

		///<summary></summary>
		public Section(AreaSectionKind kind,string name,int height){
			Kind=kind;
			Name=name;
			Height=height;
		}


	}

	///<summary>The type of section is used in the Section class.  Only ONE of each type is allowed except for the GroupHeader and GroupFooter which are optional and can have one pair for each group.  The order of the sections is locked and user can not change.</summary>
	public enum AreaSectionKind{
		///<summary>This is the data of the report and represents one row of data.  This section gets printed once for each record in the datatable.</summary>
		Detail,
		///<summary>Not implemented yet.</summary>
		GroupFooter,
		///<summary>Not implemented yet. Will print at the top of a specific group.</summary>
		GroupHeader,
		///<summary>Prints at the bottom of each page, including after the reportFooter</summary>
		PageFooter,
		///<summary>Printed at the top of each page.</summary>
		PageHeader,
		///<summary>Prints at the bottom of the report, but before the page footer for the last page.</summary>
		ReportFooter,
		///<summary>Printed at the top of the report.</summary>
		ReportHeader
	}

}












