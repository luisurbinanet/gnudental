using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental.Reporting
{
	///<summary>There is one ReportObject for each element of an ODReport that gets printed on the page. This class is the parent for many different kinds of ReportObjects.</summary>
	public class ReportObject{
		///<summary>The index of the section to which this object is attached.  For lines and boxes that span multiple sections, this is the section in which the upper part of the object resides.  If sections are added or reordered, then all reportObjects need to be updated.</summary>
		public int SectionIndex;
		///<summary>Location within the section. Frequently, y=0</summary>
		public Point Location;
		///<summary></summary>
		public Size Size;
		///<summary>The unique name of the ReportObject.</summary>
		public string Name;
		///<summary>For instance, FieldObject, or TextObject.</summary>
		public ReportObjectKind Kind;
			

		//No constructor. Use the constructor of the inherited object instead.


		///<summary>Converts contentAlignment into a combination of StringAlignments.  More arguments will later be added for other formatting options.  This method is called by FormReport when drawing text for a reportObject.</summary>
		///<param name="contentAlignment"></param>
		///<returns></returns>
		public static StringFormat GetStringFormat(ContentAlignment contentAlignment){
			if(!Enum.IsDefined(typeof(ContentAlignment),(int)contentAlignment))
				throw new System.ComponentModel.InvalidEnumArgumentException(
					"contentAlignment",(int)contentAlignment,typeof(ContentAlignment));
			StringFormat stringFormat = new StringFormat();
			switch (contentAlignment){
				case ContentAlignment.MiddleCenter:
					stringFormat.LineAlignment = StringAlignment.Center;
					stringFormat.Alignment = StringAlignment.Center;
					break;
				case ContentAlignment.MiddleLeft:
					stringFormat.LineAlignment = StringAlignment.Center;
					stringFormat.Alignment = StringAlignment.Near;
					break;
				case ContentAlignment.MiddleRight:
					stringFormat.LineAlignment = StringAlignment.Center;
					stringFormat.Alignment = StringAlignment.Far;
					break;
				case ContentAlignment.TopCenter:
					stringFormat.LineAlignment = StringAlignment.Near;
					stringFormat.Alignment = StringAlignment.Center;
					break;
				case ContentAlignment.TopLeft:
					stringFormat.LineAlignment = StringAlignment.Near;
					stringFormat.Alignment = StringAlignment.Near;
					break;
				case ContentAlignment.TopRight:
					stringFormat.LineAlignment = StringAlignment.Near;
					stringFormat.Alignment = StringAlignment.Far;
					break;
				case ContentAlignment.BottomCenter:
					stringFormat.LineAlignment = StringAlignment.Far;
					stringFormat.Alignment = StringAlignment.Center;
					break;
				case ContentAlignment.BottomLeft:
					stringFormat.LineAlignment = StringAlignment.Far;
					stringFormat.Alignment = StringAlignment.Near;
					break;
				case ContentAlignment.BottomRight:
					stringFormat.LineAlignment = StringAlignment.Far;
					stringFormat.Alignment = StringAlignment.Far;
					break;
			}
			return stringFormat;
		}


	}

	///<summary>Used in the Kind field of each ReportObject to provide a quick way to tell what kind of reportObject.</summary>
	public enum ReportObjectKind{
		//BlobFieldObject Object is a blob field. 
		///<summary>Object is a box.</summary>
		BoxObject,
		//ChartObject Object is a chart. 
		//CrossTabObject Object is a cross tab. 
		///<summary>Object is a field object.</summary>
		FieldObject,
		///<summary>Object is a line. </summary>
		LineObject,
		//PictureObject Object is a picture. 
		//SubreportObject Object is a subreport.
		///<summary>Object is a text object. </summary>
		TextObject
	}

}
