using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OpenDental.Reporting
{


	/// <summary>This is the new report class which will be replacing the old Report struct.  This class is loosely modeled after CrystalReports.ReportDocument, but much leaner.</summary>
	public class ODReport{
		private ArrayList dataFields;
		///<summary>Contains all the information relating to data manipulation based on the data source in a report.  It is usually better, however, to access this information directly from the FieldObject that displays the data.</summary>
		public DataDefinition DataDefinition;
		private SectionCollection sections;
		private ReportObjectCollection reportObjects;
		//<summary>Collection of ParameterFields that are available for the query.</summary>
		//public ParameterFieldDefinitions ParameterFields;
		//<summary></summary>
		//public PageSettingOptions PageSettings;
		// <summary></summary>
		//public struct PageSettingOptions{
		///<summary></summary>
		public Margins ReportMargins=null;
		///<summary></summary>
		public bool IsLandscape;
		//}
		///<summary>The actual query that will be sent to the database.</summary>
		public string Query;
		///<summary>The datatable that is returned from the database.</summary>
		public DataTable ReportTable;
		

		///<summary>Collection of strings representing available datatable field names.</summary>
		public ArrayList DataFields{
			get{
				return dataFields;
			}
			set{
				dataFields=value;
			}
		}

		///<summary>Collection of Sections.</summary>
		public SectionCollection Sections{
			get{
				return sections;
			}
			set{
				sections=value;
			}
		}
///<summary>A collection of ReportObjects</summary>
		public ReportObjectCollection ReportObjects{
			get{
				return reportObjects;
			}
			set{
				reportObjects=value;
			}
		}

		//public Section Section[string name]
		

		///<summary>When a new ODReport is created, the default sections are added automatically.</summary>
		public ODReport(){
			//ReportMargins=new Margins(50,50,30,30);//this should work for almost all printers.
			//margins will be null unless set by user.
			//when printing, if margins are null, the defaults will depend on the page orientation.
			sections=new SectionCollection();
			sections.Add(new Section(AreaSectionKind.ReportHeader,"Report Header",0));
			sections.Add(new Section(AreaSectionKind.PageHeader,"Page Header",0));
			//sections.Add("Group Header");
			sections.Add(new Section(AreaSectionKind.Detail,"Detail",0));
			//sections.Add("Group Footer");
			sections.Add(new Section(AreaSectionKind.PageFooter,"Page Footer",0));
			sections.Add(new Section(AreaSectionKind.ReportFooter,"Report Footer",0));
			reportObjects=new ReportObjectCollection();
			dataFields=new ArrayList();
			DataDefinition=new DataDefinition();
		}

		/// <summary>Adds a ReportObject large, centered, and bold, to the top of the Report Header Section.  Should only be done once, and done before any subTitles.</summary>
		/// <param name="title">The text of the title.</param>
		public void AddTitle(string title){
			FormReport FormR=new FormReport();
			//this is just to get a graphics object. There must be a better way.
			Graphics grfx=FormR.CreateGraphics();
			Font font=new Font("Tahoma",17,FontStyle.Bold);
			Size size=new Size((int)(grfx.MeasureString(title,font).Width/grfx.DpiX*100+2)
				,(int)(grfx.MeasureString(title,font).Height/grfx.DpiY*100+2));
			int xPos;
			if(IsLandscape)
				xPos=1100/2;
			else
				xPos=850/2;
			if(ReportMargins==null){
				if(IsLandscape)
          xPos-=50;
				else
					xPos-=30;
			}
			else{
				xPos-=ReportMargins.Left;//to make it look centered
			}
			xPos-=(int)(size.Width/2);
			ReportObjects.Add(
				new TextObject(0,new Point(xPos,0),size,title,font,ContentAlignment.MiddleCenter));
			((Section)sections[0]).Height=(int)size.Height+20;//this it the only place a white buffer is added to a header.
			grfx.Dispose();
			FormR.Dispose();
		}

		/// <summary>Adds a ReportObject, centered and bold, at the bottom of the Report Header Section.  Should only be done after AddTitle.  You can add as many subtitles as you want.</summary>
		/// <param name="subTitle">The text of the subtitle.</param>
		public void AddSubTitle(string subTitle){
			FormReport FormR=new FormReport();
			Graphics grfx=FormR.CreateGraphics();
			Font font=new Font("Tahoma",10,FontStyle.Bold);
			Size size=new Size((int)(grfx.MeasureString(subTitle,font).Width/grfx.DpiX*100+2)
				,(int)(grfx.MeasureString(subTitle,font).Height/grfx.DpiY*100+2));
			int xPos;
			if(IsLandscape)
				xPos=1100/2;
			else
				xPos=850/2;
			if(ReportMargins==null){
				if(IsLandscape)
          xPos-=50;
				else
					xPos-=30;
			}
			else{
				xPos-=ReportMargins.Left;//to make it look centered
			}
			xPos-=(int)(size.Width/2);
			//find the yPos+Height of the last reportObject in the Report Header section
			int yPos=0;
			foreach(ReportObject reportObject in reportObjects){
				if(reportObject.SectionIndex!=0) continue;
				if(reportObject.Location.Y+reportObject.Size.Height > yPos){
					yPos=reportObject.Location.Y+reportObject.Size.Height;
				}
			}
			ReportObjects.Add(
				new TextObject(0,new Point(xPos,yPos+5),size,subTitle,font,ContentAlignment.MiddleCenter));
			((Section)sections[0]).Height+=(int)size.Height+5;
			grfx.Dispose();
			FormR.Dispose();
		}

		/// <summary>Adds all the objects necessary for a typical column, including the textObject for column header and the fieldObject for the data.  Does not add lines or shading. If the column is type Double, then the alignment is set right and a total field is added. Also, default formatstrings are set for dates and doubles.</summary>
		/// <param name="dataField">The name of the column title as well as the dataField to add.</param>
		/// <param name="width"></param>
		/// <param name="valueType"></param>
		public void AddColumn(string dataField,int width,FieldValueType valueType){
			dataFields.Add(dataField);
			FormReport FormR=new FormReport();
			Graphics grfx=FormR.CreateGraphics();
			Font font;
			Size size;
			ContentAlignment textAlign;
			if(valueType==FieldValueType.DoubleField){
				textAlign=ContentAlignment.MiddleRight;
			}
			else{
				textAlign=ContentAlignment.MiddleLeft;
			}
			string formatString="";
			if(valueType==FieldValueType.DoubleField){
				formatString="n";
			}
			if(valueType==FieldValueType.DateTimeField){
				formatString="d";
			}
			//add textobject for column header
			font=new Font("Tahoma",8,FontStyle.Bold);
			size=new Size((int)(grfx.MeasureString(dataField,font).Width/grfx.DpiX*100+2)
				,(int)(grfx.MeasureString(dataField,font).Height/grfx.DpiY*100+2));
			if(((Section)sections[1]).Height==0){
				((Section)sections[1]).Height=size.Height;
			}
			int xPos=0;
			//find next available xPos
			foreach(ReportObject reportObject in reportObjects){
				if(reportObject.SectionIndex!=1) continue;
				if(reportObject.Location.X+reportObject.Size.Width > xPos){
					xPos=reportObject.Location.X+reportObject.Size.Width;
				}
			}
			ReportObjects.Add(new TextObject(sections.GetIndexOfKind(AreaSectionKind.PageHeader)
				,new Point(xPos,0),new Size(width,size.Height),dataField,font,textAlign));
			//add fieldObject for rows in details section
			font=new Font("Tahoma",9);
			size=new Size((int)(grfx.MeasureString(dataField,font).Width/grfx.DpiX*100+2)
				,(int)(grfx.MeasureString(dataField,font).Height/grfx.DpiY*100+2));
			if(((Section)sections[sections.GetIndexOfKind(AreaSectionKind.Detail)]).Height==0){
				((Section)sections[sections.GetIndexOfKind(AreaSectionKind.Detail)]).Height=size.Height;
			}
			ReportObjects.Add(new FieldObject(sections.GetIndexOfKind(AreaSectionKind.Detail)
				,new Point(xPos,0),new Size(width,size.Height)
				,new DataTableFieldDefinition(dataField,valueType),font,textAlign,formatString));
			//add fieldObject for total in ReportFooter
			if(valueType==FieldValueType.DoubleField){
				font=new Font("Tahoma",9,FontStyle.Bold);
				//use same size as already set for otherFieldObjects above
				if(((Section)sections[sections.GetIndexOfKind(AreaSectionKind.ReportFooter)]).Height==0){
					((Section)sections[sections.GetIndexOfKind(AreaSectionKind.ReportFooter)]).Height=size.Height;
				}
				ReportObjects.Add(new FieldObject(sections.GetIndexOfKind(AreaSectionKind.ReportFooter)
					,new Point(xPos,0),new Size(width,size.Height)
					,new SummaryFieldDefinition("Sum"+dataField,SummaryOperation.Sum
					,((FieldObject)GetLastRO(ReportObjectKind.FieldObject)).DataSource)
					,font,textAlign,formatString));
			}
			//tidy up
			grfx.Dispose();
			FormR.Dispose();
			return;
		}

		/// <summary>Gets the last reportObect of a particular kind. Used immediately after entering an Object to alter its properties.</summary>
		/// <param name="kind"></param>
		/// <returns></returns>
		public ReportObject GetLastRO(ReportObjectKind kind){
			//ReportObject ro=null;
			for(int i=ReportObjects.Count-1;i>=0;i--){//search from the end backwards
				if(ReportObjects[i].Kind==kind){
					return ReportObjects[i];
				}
			}
			MessageBox.Show("end of loop");
			return null;
		}

		/// <summary>Put a pagenumber object on lower left of page footer section.</summary>
		public void AddPageNum(){
			FormReport FormR=new FormReport();
			Graphics grfx=FormR.CreateGraphics();
			//add page number
			Font font=new Font("Tahoma",9);
			Size size=new Size(150,(int)(grfx.MeasureString("anytext",font).Height/grfx.DpiY*100+2));
			Section section=Sections.GetOfKind(AreaSectionKind.PageFooter);
			if(section.Height==0){
				section.Height=size.Height;
			}
			ReportObjects.Add(new FieldObject(sections.GetIndexOfKind(AreaSectionKind.PageFooter)
				,new Point(0,0),size,new SpecialVarFieldDefinition("PageNum",FieldValueType.StringField
				,SpecialVarType.PageNumber),font,ContentAlignment.MiddleLeft,""));
			grfx.Dispose();
			FormR.Dispose();
		}
		
		///<summary>Adds a parameterField which will be used in the query to represent user-entered data. One overload lets you enter the value as well.</summary>
		///<param name="name">The unique formula name of the parameter.</param>
		///<param name="valueKind">The data type that this parameter stores.</param>
		public void AddParameter(string name,ParameterValueKind valueKind){
			DataDefinition.ParameterFields.Add(new ParameterFieldDefinition(name,valueKind));
		}

		/// <summary></summary>
		public void AddParameter(string name,ParameterValueKind valueKind,object myValue){
			DataDefinition.ParameterFields.Add(new ParameterFieldDefinition(name,valueKind,myValue));
		}

		///<summary>Submits the Query to the database and fills ReportTable with the results.</summary>
		public void SubmitQuery(){
			//the outputQuery will get altered without affecting the original Query.
			string outputQuery=Query;
			string replacement="";//the replacement value to put into the outputQuery for each match
			//first replace all parameters with values:
			MatchCollection mc;
			Regex regex=new Regex(@"\?\w+");//? followed by one or more text characters
			mc=regex.Matches(outputQuery);
			//loop through each occurance of "?etc"
			for(int i=0;i<mc.Count;i++){
				replacement=DataDefinition.ParameterFields[mc[i].Value.Substring(1)].OutputValue;
				regex=new Regex(@"\"+mc[i].Value);
				outputQuery=regex.Replace(outputQuery,replacement);
			}
			//then, submit the query
			ReportTable=ODReportData.SubmitQuery(outputQuery);
		}

		/*
		/// <summary>Add Simple. This is used when there is only a single page in the report and all elements are added to the report header.  Height is set to one row, and all width is set to full page width of 850. There are no other sections to the report.</summary>
		public void AddSimp(string text,int xPos,int yPos,Font font){
			FormReport FormR=new FormReport();
			Graphics grfx=FormR.CreateGraphics();
			Size size=grfx.MeasureString(text,font);
			Section section=Sections.GetOfKind(AreaSectionKind.ReportHeader);
			if(section.Height<1100)
				section.Height=1100;
			ReportObjects.Add(new TextObject(Sections.IndexOf(section),new Point(xPos,yPos)
				,new Size(850,size.Height+2),text,font,ContentAlignment.MiddleLeft));
			grfx.Dispose();
			FormR.Dispose();
		}

		public void AddSimp(string text,int xPos,int yPos){
			AddSimp(text,xPos,yPos,new Font("Tahoma",9));
		}*/

		

	}
}











