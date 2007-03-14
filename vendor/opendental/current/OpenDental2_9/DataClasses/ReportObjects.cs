using System;
using System.Collections;
using System.Drawing;
//using System.Drawing.Printing;
using System.Text.RegularExpressions; 
using System.Windows.Forms;
using OpenDental.Reporting;

namespace OpenDental{
	
	///<summary>Corresponds to the reportobject.  Many of the fields are explained in more detail in the actual ReportObject Class.</summary>
	public struct ReportObjectStruct{
		///<summary>Primary key.</summary>
		public int ReportObjectNum;
		///<summary>Foreign key to report.ReportNum</summary>
		public int ReportNum;
		///<summary>Name of the section where this reportObject will show.</summary>
		public string SectionName;
		///<summary>X component of Location.</summary>
		public int LocationX;
		///<summary>Y component of Location</summary>
		public int LocationY;
		///<summary>Width component of Size</summary>
		public int SizeWidth;
		///<summary>Height component of Size</summary>
		public int SizeHeight;
		///<summary>Unique name of object. Called Name instead of ObjectName in ReportObject.</summary>
		public string ObjectName;
		///<summary>See the ReportObjectKind enumeration.</summary>
		public ReportObjectKind ObjectKind;
		///<summary>eg 8</summary>
		public float FontSize;
		///<summary>eg Arial</summary>
		public string FontName;
		///<summary>Flags for fontstyle, like Bold or Itallic</summary>
		public FontStyle FontStyle;
		///<summary>See ContentAlignment enumeration.</summary>
		public ContentAlignment TextAlign;
		///<summary>The text color or line color.</summary>
		public Color ForeColor;
		///<summary>For ObjectKind.TextObject, this is the text that fills the object.  It can be very complex.</summary>
		public string StaticText;
		///<summary>Used to format dates, numbers, and currency.</summary>
		public string FormatString;
		///<summary>If the previous row was the same, then this row will not show.</summary>
		public bool SuppressIfDuplicate;
		///<summary>For drawing object, the lower section where the drawing ends.</summary>
		public string EndSectionName;
		///<summary>X component of LocationLowerRight</summary>
		public int LocationLowerRightX;
		///<summary>Y component of LocationLowerRight</summary>
		public int LocationLowerRightY;
		///<summary>For a drawing object, the thickness of the</summary>
		public float LineThickness;
		///<summary>See FieldDefKind enumeration.</summary>
		public FieldDefKind FieldKind;
		///<summary>See FieldValueType enumeration.</summary>
		public FieldValueType ValueType;
		///<summary>Only for SpecialFields.  See SpecialFieldType enumeration.</summary>
		public SpecialFieldType SpecialType;
		///<summary>Only for SummaryFields. See SummaryOperation enumeration.</summary>
		public SummaryOperation Operation;
		///<summary>Only for SummaryFields.  The name of the DataField that is summarized.</summary>
		public string SummarizedField;
		///<summary>The name of the field that </summary>
		public string DataField;
	}

	/*=========================================================================================
		=================================== class ReportObjects =====================================*/

	///<summary>Handles database commands related to the reportobject table in the db.</summary>
	public class ReportObjects:DataClass{
		///<summary>An array of reportObjects for one report.</summary>
		public static ReportObjectStruct[] List;
		///<summary></summary>
		public static ReportObjectStruct Cur;
		
		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * FROM reportobject "
				+"WHERE ReportNum ='"+Reports.CurStruct.ReportNum.ToString()+"'";
			FillTable();
			List=new ReportObjectStruct[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].ReportObjectNum    = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ReportNum          = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].SectionName        = PIn.PString(table.Rows[i][2].ToString());
				List[i].LocationX          = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].LocationY          = PIn.PInt   (table.Rows[i][4].ToString());
				List[i].SizeWidth          = PIn.PInt   (table.Rows[i][5].ToString());
				List[i].SizeHeight         = PIn.PInt   (table.Rows[i][6].ToString());
				List[i].ObjectName         = PIn.PString(table.Rows[i][7].ToString());
				List[i].ObjectKind         = (ReportObjectKind)PIn.PInt(table.Rows[i][8].ToString());
				List[i].FontSize           = PIn.PFloat (table.Rows[i][9].ToString());
				List[i].FontName           = PIn.PString(table.Rows[i][10].ToString());
				List[i].FontStyle          = (FontStyle)PIn.PInt   (table.Rows[i][11].ToString());
				List[i].TextAlign          = (ContentAlignment)PIn.PInt(table.Rows[i][12].ToString());
				List[i].ForeColor          = Color.FromArgb(PIn.PInt(table.Rows[i][13].ToString()));
				List[i].StaticText         = PIn.PString(table.Rows[i][14].ToString());
				List[i].FormatString       = PIn.PString(table.Rows[i][15].ToString());
				List[i].SuppressIfDuplicate= PIn.PBool  (table.Rows[i][16].ToString());
				List[i].EndSectionName     = PIn.PString(table.Rows[i][17].ToString());
				List[i].LocationLowerRightX= PIn.PInt   (table.Rows[i][18].ToString());
				List[i].LocationLowerRightY= PIn.PInt   (table.Rows[i][19].ToString());
				List[i].LineThickness      = PIn.PFloat (table.Rows[i][20].ToString());
				List[i].FieldKind          = (FieldDefKind)PIn.PInt(table.Rows[i][21].ToString());
				List[i].ValueType          = (FieldValueType)PIn.PInt(table.Rows[i][22].ToString());
				List[i].SpecialType        = (SpecialFieldType)PIn.PInt(table.Rows[i][23].ToString());
				List[i].Operation          = (SummaryOperation)PIn.PInt(table.Rows[i][24].ToString());
				List[i].SummarizedField    = PIn.PString(table.Rows[i][25].ToString());
				List[i].DataField          = PIn.PString(table.Rows[i][26].ToString());
			}
		}

		///<summary>Inserts the specified reportobject into the database giving it the ReportNum of the Cur ReportStruct.</summary>
		public static void InsertReportObject(ReportObject reportObject){
			Cur=new ReportObjectStruct();
			Cur.ReportNum=Reports.CurStruct.ReportNum;
			Cur.SectionName=reportObject.SectionName;
			Cur.LocationX=reportObject.Location.X;
			Cur.LocationY=reportObject.Location.Y;
			Cur.SizeWidth=reportObject.Size.Width;
			Cur.SizeHeight=reportObject.Size.Height;
			Cur.ObjectName=reportObject.Name;
			Cur.ObjectKind=reportObject.ObjectKind;
			Cur.FontSize=reportObject.Font.Size;
			Cur.FontName=reportObject.Font.Name;
			Cur.FontStyle=reportObject.Font.Style;
			Cur.TextAlign=reportObject.TextAlign;
			Cur.ForeColor=reportObject.ForeColor;
			Cur.StaticText=reportObject.StaticText;
			Cur.FormatString=reportObject.FormatString;
			Cur.SuppressIfDuplicate=reportObject.SuppressIfDuplicate;
			Cur.EndSectionName=reportObject.EndSectionName;
			Cur.LocationLowerRightX=reportObject.LocationLowerRight.X;
			Cur.LocationLowerRightY=reportObject.LocationLowerRight.Y;
			Cur.LineThickness=reportObject.LineThickness;
			Cur.FieldKind=reportObject.FieldKind;
			Cur.ValueType=reportObject.ValueType;
			Cur.SpecialType=reportObject.SpecialType;
			Cur.Operation=reportObject.Operation;
			Cur.SummarizedField=reportObject.SummarizedField;
			Cur.DataField=reportObject.DataField;
			InsertCur();
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO reportobject (ReportNum,SectionName,LocationX,LocationY"
				+",SizeWidth,SizeHeight,ObjectName,ObjectKind,FontSize,FontName,FontStyle,TextAlign"
				+",ForeColor,StaticText,FormatString,SuppressIfDuplicate,EndSectionName"
				+",LocationLowerRightX,LocationLowerRightY,LineThickness,FieldKind,ValueType,SpecialType"
				+",Operation,SummarizedField,DataField) VALUES("
				+"'"+POut.PInt   (Cur.ReportNum)+"', "
				+"'"+POut.PString(Cur.SectionName)+"', "
				+"'"+POut.PInt   (Cur.LocationX)+"', "
				+"'"+POut.PInt   (Cur.LocationY)+"', "
				+"'"+POut.PInt   (Cur.SizeWidth)+"', "
				+"'"+POut.PInt   (Cur.SizeHeight)+"', "
				+"'"+POut.PString(Cur.ObjectName)+"', "
				+"'"+POut.PInt   ((int)Cur.ObjectKind)+"', "
				+"'"+POut.PFloat (Cur.FontSize)+"', "
				+"'"+POut.PString(Cur.FontName)+"', "
				+"'"+POut.PInt   ((int)Cur.FontStyle)+"', "
				+"'"+POut.PInt   ((int)Cur.TextAlign)+"', "
				+"'"+POut.PInt   (Cur.ForeColor.ToArgb())+"', "
				+"'"+POut.PString(Cur.StaticText)+"', "
				+"'"+POut.PString(Cur.FormatString)+"', "
				+"'"+POut.PBool  (Cur.SuppressIfDuplicate)+"', "
				+"'"+POut.PString(Cur.EndSectionName)+"', "
				+"'"+POut.PInt   (Cur.LocationLowerRightX)+"', "
				+"'"+POut.PInt   (Cur.LocationLowerRightY)+"', "
				+"'"+POut.PFloat (Cur.LineThickness)+"', "
				+"'"+POut.PInt   ((int)Cur.FieldKind)+"', "
				+"'"+POut.PInt   ((int)Cur.ValueType)+"', "
				+"'"+POut.PInt   ((int)Cur.SpecialType)+"', "
				+"'"+POut.PInt   ((int)Cur.Operation)+"', "
				+"'"+POut.PString(Cur.SummarizedField)+"', "
				+"'"+POut.PString(Cur.DataField)+"')";
			NonQ();
		}

		///<summary>Returns the reportobjects from the database for the Cur ReportStruct.</summary>
		public static ReportObjectCollection GetForReport(){
			Refresh();
			ReportObjectCollection retVal=new ReportObjectCollection();
			ReportObject ro;
			for(int i=0;i<List.Length;i++){
				ro=new ReportObject();
				ro.SectionName=List[i].SectionName;
				ro.Location=new Point(List[i].LocationX,List[i].LocationY);
				ro.Size=new Size(List[i].SizeWidth,List[i].SizeHeight);
				ro.Name=List[i].ObjectName;
				ro.ObjectKind=List[i].ObjectKind;
				ro.Font=new Font(List[i].FontName,List[i].FontSize,List[i].FontStyle);
				ro.TextAlign=List[i].TextAlign;
				ro.ForeColor=List[i].ForeColor;
				ro.StaticText=List[i].StaticText;
				ro.FormatString=List[i].FormatString;
				ro.SuppressIfDuplicate=List[i].SuppressIfDuplicate;
				ro.EndSectionName=List[i].EndSectionName;
				ro.LocationLowerRight=new Point(List[i].LocationLowerRightX,List[i].LocationLowerRightY);
				ro.LineThickness=List[i].LineThickness;
				ro.FieldKind=List[i].FieldKind;
				ro.ValueType=List[i].ValueType;
				ro.SpecialType=List[i].SpecialType;
				ro.Operation=List[i].Operation;
				retVal.Add(ro);
			}
			return retVal;
		}

		
		///<summary></summary>
		public static void DeleteForReport(){
			cmd.CommandText="DELETE FROM reportobject "
				+"WHERE ReportNum = '"+Reports.CurStruct.ReportNum.ToString()+"'";
			NonQ();
		}


		
		
		
		
	}

}









