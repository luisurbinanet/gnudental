using System;
using System.Collections;
//using System.Drawing.Printing;
using System.Text.RegularExpressions; 
using System.Windows.Forms;
using OpenDental.Reporting;

namespace OpenDental{
	
	///<summary>Corresponds to the reportparameter table in the db.</summary>
	public struct ReportParameter{
		///<summary>Primary key.</summary>
		public int ReportParameterNum;
		///<summary>Foreign key to report.ReportNum</summary>
		public int ReportNum;
		///<summary>The name as it will show in the query, but without the preceding question mark.</summary>
		public string Name;
		///<summary>The type of value that the parameter can accept.</summary>
		public FieldValueType ValueType;
		///<summary>Comma delimited list of values that will show in the dialog prefilled when it asks the user for values.  If this string is empty, then the length of the ArrayList will be 0 to specify that the initial value is blank. For enum and defcat, these are numbers.</summary>
		public string DefaultValues;
		///<summary>The text that prompts the user what to enter for this parameter.</summary>
		public string PromptingText;
		///<summary>The snippet of SQL that will be repeated once for each value supplied by the user, connected with OR's, and surrounded by parentheses.</summary>
		public string Snippet;
		///<summary>If the ValueKind is EnumField, then this specifies which type of enum. It is the string name of the type.</summary>
		public EnumType EnumerationType;
		///<summary>If ValueKind is DefParameter, then this specifies which DefCat.</summary>
		public DefCat DefCategory;
	}

	/*=========================================================================================
		=================================== class ReportParameters =====================================*/

	///<summary>Handles database commands related to the reportparameter table in the db.</summary>
	public class ReportParameters:DataClass{
		///<summary>An array of all ReportParameters for one report.</summary>
		public static ReportParameter[] List;
		///<summary></summary>
		public static ReportParameter Cur;
		
		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * FROM reportparameter "
				+"WHERE ReportNum ='"+Reports.CurStruct.ReportNum.ToString()+"'";
			FillTable();
			List=new ReportParameter[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i].ReportParameterNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ReportNum         = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].Name              = PIn.PString(table.Rows[i][2].ToString());
				List[i].ValueType         = (FieldValueType)PIn.PInt(table.Rows[i][3].ToString());
				List[i].DefaultValues     = PIn.PString(table.Rows[i][4].ToString());
				List[i].PromptingText     = PIn.PString(table.Rows[i][5].ToString());
				List[i].Snippet           = PIn.PString(table.Rows[i][6].ToString());
				List[i].EnumerationType   = (EnumType)PIn.PInt(table.Rows[i][7].ToString());
				List[i].DefCategory       = (DefCat)PIn.PInt(table.Rows[i][8].ToString());
			}
		}

		/*
		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE reportparameter SET " 
				+ "ReportNum = '"       +POut.PInt   (Cur.ReportNum)+"'"
				+ ",Name = '"           +POut.PString(Cur.Name)+"'"
				+ ",ValueType = '"      +POut.PInt   ((int)Cur.ValueType)+"'"
				+ ",DefaultValues = '"  +POut.PString(Cur.DefaultValues)+"'"
				+ ",PromptingText = '"  +POut.PString(Cur.PromptingText)+"'"
				+ ",Snippet = '"        +POut.PString(Cur.Snippet)+"'"
				+ ",EnumerationType = '"+POut.PInt   ((int)Cur.EnumerationType)+"'"
				+ ",DefCategory = '"    +POut.PInt   ((int)Cur.DefCategory)+"'"				
				+" WHERE ReportNum = '" +POut.PInt(Cur.ReportNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ();
		}*/

		///<summary>Inserts the specified parameter into the database giving it the ReportNum of the Cur ReportStruct.</summary>
		public static void InsertParameter(ParameterField parameterField){
			Cur=new ReportParameter();
			Cur.ReportNum=Reports.CurStruct.ReportNum;
			Cur.Name=parameterField.Name;
			Cur.ValueType=parameterField.ValueType;
			Cur.DefaultValues="";
			for(int i=0;i<parameterField.DefaultValues.Count;i++){
				if(i>0) Cur.DefaultValues+=",";
				switch(Cur.ValueType){
					case FieldValueType.Boolean:
						Cur.DefaultValues+=POut.PBool((bool)parameterField.DefaultValues[i]);
						break;
					case FieldValueType.Date:
						Cur.DefaultValues+=POut.PDate((DateTime)parameterField.DefaultValues[i]);
						break;
					case FieldValueType.Def:
					case FieldValueType.Enum:
					case FieldValueType.Integer:
						Cur.DefaultValues+=POut.PInt((int)parameterField.DefaultValues[i]);
						break;
					case FieldValueType.Number:
						Cur.DefaultValues+=POut.PDouble((double)parameterField.DefaultValues[i]);
						break;
					case FieldValueType.String:
						//don't run the string through the POut.PString, because that will be done later.
						//It is not currently possible to create a stringparameter with multiple values.
						Cur.DefaultValues+=Regex.Replace(parameterField.DefaultValues[i].ToString(),
							",", "");//replace all commas with empty strings
						break;
				}
			}
			Cur.PromptingText=parameterField.PromptingText;
			Cur.Snippet=parameterField.Snippet;
			Cur.EnumerationType=parameterField.EnumerationType;
			Cur.DefCategory=parameterField.DefCategory;
			InsertCur();
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO report (ReportNum,Name,ValueType,DefaultValues"
				+",PromptingText,Snippet,EnumerationType,DefCategory) VALUES("
				+"'"+POut.PInt   (Cur.ReportNum)+"', "
				+"'"+POut.PString(Cur.Name)+"', "
				+"'"+POut.PInt   ((int)Cur.ValueType)+"', "
				+"'"+POut.PString(Cur.DefaultValues)+"', "
				+"'"+POut.PString(Cur.PromptingText)+"', "
				+"'"+POut.PString(Cur.Snippet)+"', "
				+"'"+POut.PInt   ((int)Cur.EnumerationType)+"', "
				+"'"+POut.PInt   ((int)Cur.DefCategory)+"')";
			NonQ();
		}

		///<summary></summary>
		public static void DeleteForReport(){
			cmd.CommandText="DELETE FROM reportparameter "
				+"WHERE ReportNum = '"+Reports.CurStruct.ReportNum.ToString()+"'";
			NonQ();
		}



		///<summary>Returns the parameters from the database for the Cur ReportStruct.</summary>
		public static ParameterFieldCollection GetForReport(){
			Refresh();
			ParameterFieldCollection retVal=new ParameterFieldCollection();
			ParameterField param;
			ArrayList defaultValues;
			string strVal;
			for(int i=0;i<List.Length;i++){
				param=new ParameterField();
				param.Name=List[i].Name;
				param.ValueType=List[i].ValueType;
				defaultValues=new ArrayList();
				for(int j=0;j<List[i].DefaultValues.Split(',').Length;j++){
					strVal=List[i].DefaultValues.Split(',')[j];
					switch(param.ValueType){
						case FieldValueType.Boolean:
							defaultValues.Add(PIn.PBool(strVal));
							break;
						case FieldValueType.Date:
							defaultValues.Add(PIn.PDate(strVal));
							break;
						case FieldValueType.Def:
							defaultValues.Add(PIn.PInt(strVal));
							break;
						case FieldValueType.Enum:
							defaultValues.Add(PIn.PInt(strVal));
							break;
						case FieldValueType.Integer:
							defaultValues.Add(PIn.PInt(strVal));
							break;
						case FieldValueType.Number:
							defaultValues.Add(PIn.PDouble(strVal));
							break;
						case FieldValueType.String:
							defaultValues.Add(PIn.PString(strVal));
							break;
					}
				}
				param.CurrentValues=defaultValues;
				param.DefaultValues=defaultValues;
				param.PromptingText=List[i].PromptingText;
				param.Snippet=List[i].Snippet;
				param.EnumerationType=List[i].EnumerationType;
				param.DefCategory=List[i].DefCategory;
				retVal.Add(param);
			}
			return retVal;
		}



		
		
		
		
	}

}









