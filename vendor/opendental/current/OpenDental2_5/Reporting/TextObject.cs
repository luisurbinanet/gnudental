using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental.Reporting
{
	///<summary>Used for any fixed text such as column headings and titles.</summary>
	public class TextObject:ReportObject{
		///<summary>The text to display.</summary>
		public string Text;
		///<summary></summary>
		public Font Font;
		///<summary></summary>
		public ContentAlignment TextAlign;
		///<summary></summary>
		public Color TextColor;

		///<summary></summary>
		public TextObject(int thisSectionIndex,Point thisLocation,Size thisSize,string thisText,Font thisFont,ContentAlignment thisTextAlign){
			SectionIndex=thisSectionIndex;
			Location=thisLocation;
			Size=thisSize;
			Text=thisText;
			Font=thisFont;
			TextAlign=thisTextAlign;
			TextColor=Color.Black;
			Kind=ReportObjectKind.TextObject;
		}

		



	}
}
