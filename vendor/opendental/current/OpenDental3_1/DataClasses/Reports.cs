using System;
using System.Collections;
using System.Drawing.Printing;
using System.Windows.Forms;
using OpenDental.Reporting;

namespace OpenDental{
	
	///<summary>Not in use yet. Corresponds to the Report table in the db. This ReportStruct is used because the actual Report class is too complex to be represented effectively in the database.  This is the intermediary.</summary>
	public struct ReportStruct{
		///<summary>Primary key.</summary>
		public int ReportNum;
		///<summary>Comma delimited list of the field names to be used in this report. Later, multiple tables will be allowed.  Then, the format would be tablename.FieldName</summary>
		public string DataFields;
		///<summary>The query will get altered before it is actually used to retrieve data. Any parameters will be replaced with user entered data without saving those changes.</summary>
		public string Query;
		///<summary></summary>
		public int MarginL;
		///<summary></summary>
		public int MarginR;
		///<summary></summary>
		public int MarginT;
		///<summary></summary>
		public int MarginB;
		///<summary></summary>
		public bool IsLandscape;
		///<summary>The name of the report to show in the menu.</summary>
		public string ReportName;
		///<summary>The 1-based order to show in the Letter menu, or 0 to not show in that menu.</summary>
		public int LetterOrder;
		///<summary>For instance OD12 or JoeDeveloper9.  If you are a developer releasing reports, then this should be your name or company followed by a unique number.  This will later make it easier to maintain your reports for your customers.  All reports that we release will be of the form OD##.  Reports that the user creates will have this field blank.</summary>
		public string AuthorID;
		///<summary>Gives the user a description and some guidelines about what they can expect from this report.</summary>
		public string Description;
	}

	/*=========================================================================================
		=================================== class Reports ==========================================*/

	///<summary>Handles database commands related to the report table in the db.</summary>
	public class Reports:DataClass{
		///<summary>An array of all ReportStructs. Need info from 3 other tables to construct an actual report. For now, all items will be letters, since other reports are not yet stored here.</summary>
		public static ReportStruct[] List;
		///<summary>Only contains the data from the Report table in the db. Need info from 3 other tables to construct a report.</summary>
		public static ReportStruct CurStruct;
		///<summary>Once a report has been constructed from multiple db tables, it can be stored here.</summary>
		public static Report Cur;
		
		///<summary>Gets all reports</summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * FROM report ORDER BY LetterOrder";
			FillTable();
			List=new ReportStruct[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].ReportNum   = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].DataFields  = PIn.PString(table.Rows[i][1].ToString());
				List[i].Query       = PIn.PString(table.Rows[i][2].ToString());
				List[i].MarginL     = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].MarginR     = PIn.PInt   (table.Rows[i][4].ToString());
				List[i].MarginT     = PIn.PInt   (table.Rows[i][5].ToString());
				List[i].MarginB     = PIn.PInt   (table.Rows[i][6].ToString());
				List[i].IsLandscape = PIn.PBool  (table.Rows[i][7].ToString());
				List[i].ReportName  = PIn.PString(table.Rows[i][8].ToString());
				List[i].LetterOrder = PIn.PInt   (table.Rows[i][9].ToString());
				List[i].AuthorID    = PIn.PString(table.Rows[i][10].ToString());
				List[i].Description = PIn.PString(table.Rows[i][11].ToString());
			}
		}

		///<summary></summary>
		public static void UpdateCurStruct(){
			cmd.CommandText = "UPDATE report SET " 
				+ "DataFields = '"  +POut.PString(CurStruct.DataFields)+"'"
				+ ",Query = '"      +POut.PString(CurStruct.Query)+"'"
				+ ",MarginL = '"    +POut.PInt   (CurStruct.MarginL)+"'"
				+ ",MarginR = '"    +POut.PInt   (CurStruct.MarginR)+"'"
				+ ",MarginT = '"    +POut.PInt   (CurStruct.MarginT)+"'"
				+ ",MarginB = '"    +POut.PInt   (CurStruct.MarginB)+"'"
				+ ",IsLandscape = '"+POut.PBool  (CurStruct.IsLandscape)+"'"
				+ ",ReportName = '" +POut.PString(CurStruct.ReportName)+"'"
				+ ",LetterOrder = '"+POut.PInt   (CurStruct.LetterOrder)+"'"
				+ ",AuthorID = '"   +POut.PString(CurStruct.AuthorID)+"'"
				+ ",Description = '"+POut.PString(CurStruct.Description)+"'"
				+" WHERE ReportNum = '" +POut.PInt(CurStruct.ReportNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ();
		}

		///<summary></summary>
		public static void InsertCurStruct(){
			cmd.CommandText = "INSERT INTO report (DataFields,Query,MarginL,MarginR,MarginT,MarginB"
				+",IsLandscape,ReportName,LetterOrder,AuthorID,Description) VALUES("
				+"'"+POut.PString(CurStruct.DataFields)+"', "
				+"'"+POut.PString(CurStruct.Query)+"', "
				+"'"+POut.PInt   (CurStruct.MarginL)+"', "
				+"'"+POut.PInt   (CurStruct.MarginR)+"', "
				+"'"+POut.PInt   (CurStruct.MarginT)+"', "
				+"'"+POut.PInt   (CurStruct.MarginB)+"', "
				+"'"+POut.PBool  (CurStruct.IsLandscape)+"', "
				+"'"+POut.PString(CurStruct.ReportName)+"', "
				+"'"+POut.PInt   (CurStruct.LetterOrder)+"', "
				+"'"+POut.PString(CurStruct.AuthorID)+"', "
				+"'"+POut.PString(CurStruct.Description)+"')";
			NonQ();
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText="DELETE FROM report "
				+"WHERE ReportNum = '"+CurStruct.ReportNum.ToString()+"'";
			NonQ();
		}

		///<summary>Assembles a report based on the CurStruct.  Gets data from the other tables in the database to do so.</summary>
		public static void AssembleReport(){
			Cur=new Report();
			string[] dataFields=CurStruct.DataFields.Split(',');
			for(int i=0;i<dataFields.Length;i++){
				Cur.DataFields.Add(dataFields[i]);
			}
	//todo: sections
			Cur.ReportObjects=ReportObjects.GetForReport();
			Cur.ParameterFields=ReportParameters.GetForReport();
			Cur.ReportMargins=new Margins(CurStruct.MarginL,CurStruct.MarginR
				,CurStruct.MarginT,CurStruct.MarginB);
			Cur.IsLandscape=CurStruct.IsLandscape;
			Cur.Query=CurStruct.Query;
			Cur.ReportName=CurStruct.ReportName;
			Cur.Description=CurStruct.Description;
			Cur.AuthorID=CurStruct.AuthorID;	
			Cur.LetterOrder=CurStruct.LetterOrder;
		}

		///<summary>Transforms the Cur Report into the 4 database types and updates each.</summary>
		public static void UpdateReport(){
			//The Cur Struct will already correspond to the Cur Report,
			//since the report was created from the struct. (I think)
			CurStruct.DataFields="";
//todo: validate user input and strip commas from datafields
			for(int i=0;i<Cur.DataFields.Count;i++){
				if(i>0) CurStruct.DataFields+=",";
				CurStruct.DataFields+=Cur.DataFields[i].ToString();
			}
			CurStruct.Query=Cur.Query;
			CurStruct.MarginL=Cur.ReportMargins.Left;
			CurStruct.MarginR=Cur.ReportMargins.Right;
			CurStruct.MarginT=Cur.ReportMargins.Top;
			CurStruct.MarginB=Cur.ReportMargins.Bottom;
			CurStruct.IsLandscape=Cur.IsLandscape;
			CurStruct.ReportName=Cur.ReportName;
			CurStruct.LetterOrder=Cur.LetterOrder;
			CurStruct.AuthorID=Cur.AuthorID;
			CurStruct.Description=Cur.Description;
			UpdateCurStruct();
			ReportParameters.DeleteForReport();
			for(int i=0;i<Cur.ParameterFields.Count;i++){
				ReportParameters.InsertParameter(Cur.ParameterFields[i]);
			}
		}



		
		
		
		
	}

}









