using System;
using System.Drawing;

namespace OpenDental.Reporting
{
	///<summary>Parent class for the BoxObject and the LineObject.  They differ from text objects in that they can span multiple sections.  The size value is ignored since that is always dependent on the size of each section. </summary>
	public class DrawingObject:ReportObject{
		///<summary>The index of the Section to which the lower part of the object extends.  This will normally be the same as the sectionIndex unless the object spans multiple sections.  The object will be drawn across all sections inbetween.</summary>
		public int endSectionIndex;
		///<summary>The position of the lower right corner of the box or line in the coordinates of the endSection.</summary>
		public Point locationLowerRight;
		//public LineStyle LineStyle;//maybe later
		///<summary></summary>
		public float LineThickness;
		///<summary>Color of the line or outline.</summary>
		public Color LineColor;

		//no Constructor.  Use the constructor of the inherited object instead.

	}

}
