/* ====================================================================
    Copyright (C) 2004-2005  fyiReporting Software, LLC

    This file is part of the fyiReporting RDL project.
	
    The RDL project is free software; you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation; either version 2 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

    For additional information, email info@fyireporting.com or visit
    the website www.fyiReporting.com.
*/
using System;
using System.Xml;
using System.IO;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;


namespace fyiReporting.RdlDesign
{
	/// <summary>
	/// Static utility classes used in the Rdl Designer
	/// </summary>
	internal class DesignerUtility
	{
		static internal Color ColorFromHtml(string sc, Color dc)
		{
			Color c = dc;
			try 
			{
				c = ColorTranslator.FromHtml(sc);
			}
			catch 
			{	// Probably should report this error
			}
			return c;
		}

		static internal string FormatXml(string sDoc)
		{
			XmlDocument xDoc = new XmlDocument();
			xDoc.PreserveWhitespace = false;
			xDoc.LoadXml(sDoc);						// this will throw an exception if invalid XML
			StringWriter sw = new StringWriter();
			XmlTextWriter xtw = new XmlTextWriter(sw);
			xtw.IndentChar = ' ';
			xtw.Indentation = 2;
			xtw.Formatting = Formatting.Indented;
			
			xDoc.WriteContentTo(xtw);
			xtw.Close();
			sw.Close();
			return sw.ToString();
		}

		static internal void GetSqlData(string dataProvider, string connection, string sql, IList parameters, DataTable dt)
		{
			IDbConnection cnSQL=null;
			IDbCommand cmSQL=null;
			IDataReader dr=null;	   
			Cursor saveCursor=Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				/*switch (dataProvider)
				{
					case "SQL":
						cnSQL = new SqlConnection(connection);
						cnSQL.Open();
						cmSQL = new SqlCommand(sql, (SqlConnection) cnSQL);
						break;
					case "ODBC":
						cnSQL = new OdbcConnection(connection);
						cnSQL.Open();
						cmSQL = new OdbcCommand(sql, (OdbcConnection)cnSQL);
						break;
					case "OLEDB":
						cnSQL = new OleDbConnection(connection);
						cnSQL.Open();
						cmSQL = new OleDbCommand(sql, (OleDbConnection)cnSQL);
						break;
					default:
						throw new Exception(string.Format("Unknown data provider '{0}'.", dataProvider ));
				}*/
				cnSQL = new MySqlConnection(GetOpenDentalConnStr());
				cnSQL.Open();
				cmSQL = new MySqlCommand(sql, (MySqlConnection) cnSQL);
				AddParameters(cmSQL, parameters);
				dr = cmSQL.ExecuteReader(CommandBehavior.SingleResult);

				string[] rowValues = new string[dt.Columns.Count];

				while (dr.Read())
				{
					int ci=0;
					foreach (DataColumn dc in dt.Columns)
					{
						rowValues[ci++] = dr[dc.ColumnName].ToString();
					}
					dt.Rows.Add(rowValues);
				}
			}
			finally
			{
				if (cnSQL != null)
				{
					cnSQL.Close();
					cnSQL.Dispose();
					if (cmSQL != null)
					{
						cmSQL.Dispose();
						if (dr != null)
							dr.Close();
					}
				}
				Cursor.Current=saveCursor;
			}
			return;
		}

		static internal ArrayList GetSqlColumns(DesignXmlDraw d, string ds, string sql)
		{
			XmlNode dsNode = d.DataSourceName(ds);
			if (dsNode == null)
				return null;
			string dataProvider;
			string connection;
			XmlNode dp = DesignXmlDraw.FindNextInHierarchy(dsNode, "ConnectionProperties", "DataProvider");
			if (dp == null)
				return null;
			dataProvider = dp.InnerText;
			dp = DesignXmlDraw.FindNextInHierarchy(dsNode, "ConnectionProperties", "ConnectString");
			if (dp == null)
				return null;
			connection = dp.InnerText;
			IList parameters=null;

			return GetSqlColumns(dataProvider, connection, sql, parameters);
		}

		static internal ArrayList GetSqlColumns(string dataProvider, string connection, string sql, IList parameters)
		{
			ArrayList cols = new ArrayList();
			IDbConnection cnSQL=null;
			IDbCommand cmSQL=null;
			IDataReader dr=null;	   
			Cursor saveCursor=Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				/*switch (dataProvider)
				{
					case "SQL":
						cnSQL = new SqlConnection(connection);
						cnSQL.Open();
						cmSQL = new SqlCommand(sql, (SqlConnection) cnSQL);
						break;
					case "ODBC":
						cnSQL = new OdbcConnection(connection);
						cnSQL.Open();
						cmSQL = new OdbcCommand(sql, (OdbcConnection)cnSQL);
						break;
					case "OLEDB":
						cnSQL = new OleDbConnection(connection);
						cnSQL.Open();
						cmSQL = new OleDbCommand(sql, (OleDbConnection)cnSQL);
						break;
					default:
						throw new Exception(string.Format("Unknown data provider '{0}'.", dataProvider ));
				}*/
				cnSQL = new MySqlConnection(GetOpenDentalConnStr());
				cnSQL.Open();
				cmSQL = new MySqlCommand(sql, (MySqlConnection) cnSQL);
				AddParameters(cmSQL, parameters);
				dr = cmSQL.ExecuteReader(CommandBehavior.SchemaOnly);
				for (int i=0; i < dr.FieldCount; i++)
				{
					SqlColumn sc = new SqlColumn();
					sc.Name = dr.GetName(i);
					sc.DataType = dr.GetFieldType(i);
					cols.Add(sc);
				}
			}
			catch (SqlException sqle)
			{
				MessageBox.Show(sqle.Message, "SQL Error");
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Error");
			}
			finally
			{
				if (cnSQL != null)
				{
					cnSQL.Close();
					cnSQL.Dispose();
					if (cmSQL != null)
					{
						cmSQL.Dispose();
						if (dr != null)
							dr.Close();
					}
				}
				Cursor.Current=saveCursor;
			}
			return cols;
		}

		static internal ArrayList GetSchemaInfo(DesignXmlDraw d, string ds)
		{
			XmlNode dsNode = d.DataSourceName(ds);
			if (dsNode == null)
				return null;

			string dataProvider;
			string connection;
			XmlNode dp = DesignXmlDraw.FindNextInHierarchy(dsNode, "ConnectionProperties", "DataProvider");
			if (dp == null)
				return null;
			dataProvider = dp.InnerText;
			dp = DesignXmlDraw.FindNextInHierarchy(dsNode, "ConnectionProperties", "ConnectString");
			if (dp == null)
				return null;
			connection = dp.InnerText;
			return GetSchemaInfo(dataProvider, connection);
		}

		static internal ArrayList GetSchemaInfo(string dataProvider, string connection)
		{
			ArrayList schemaList = new ArrayList();
			IDbConnection cnSQL = null;
			IDbCommand cmSQL = null;
			IDataReader dr = null;
			Cursor saveCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;

			// Get the schema information
			try
			{
				int ID_TABLE = 0;
				int ID_TYPE = 1;
				string sql = "SHOW TABLES";
					//"SELECT TABLE_NAME, TABLE_TYPE FROM INFORMATION_SCHEMA.TABLES ORDER BY 2, 1";
				/*switch (dataProvider)
				{
					case "SQL":
						cnSQL = new SqlConnection(connection);
						cnSQL.Open();
						cmSQL = new SqlCommand(sql, (SqlConnection) cnSQL);
						break;
					case "ODBC":
						ID_TYPE = -1;
						cnSQL = new OdbcConnection(connection);
						cnSQL.Open();
						OdbcConnection oc = cnSQL as OdbcConnection;
						if (oc.Driver.ToLower().IndexOf("my") >= 0)	// not a good way but ...
							sql = "show tables";					// mysql syntax is non-standard
						
						cmSQL = new OdbcCommand(sql, (OdbcConnection)cnSQL);
						break;
					case "OLEDB":
						cnSQL = new OleDbConnection(connection);
						cnSQL.Open();
						cmSQL = new OleDbCommand(sql, (OleDbConnection)cnSQL);
						break;
				}*/
				ID_TYPE = -1;
				cnSQL = new MySqlConnection(GetOpenDentalConnStr());
				cnSQL.Open();
				cmSQL = new MySqlCommand(sql, (MySqlConnection) cnSQL);
				dr = cmSQL.ExecuteReader();
				string type = "TABLE";
				while (dr.Read())
				{
					SqlSchemaInfo ssi = new SqlSchemaInfo();

					if (ID_TYPE >= 0 && (string) dr[ID_TYPE] == "VIEW")
						type = "VIEW";

					ssi.Type = type;
					ssi.Name = (string) dr[ID_TABLE];
					schemaList.Add(ssi);
				}
			}
			catch (SqlException sqle)
			{
				MessageBox.Show(sqle.Message, "SQL Error");
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Error");
			}
			finally
			{
				if (cnSQL != null)
				{
					cnSQL.Close();
					if (cmSQL != null)
					{
						cmSQL.Dispose();
					}
					if (dr != null)
						dr.Close();
				}
				Cursor.Current=saveCursor;
			}
			return schemaList;
		}

		///<summary></summary>
		private static string GetOpenDentalConnStr(){
			XmlDocument document=new XmlDocument();
			if(!File.Exists("FreeDentalConfig.xml")){
				return "";
			}
			string computerName="";
			string database="";
			string user="";
			string password="";
			try{
				document.Load("FreeDentalConfig.xml");
				XmlNodeReader reader=new XmlNodeReader(document);
				string currentElement="";
				while(reader.Read()){
					if(reader.NodeType==XmlNodeType.Element){
						currentElement=reader.Name;
					}
					else if(reader.NodeType==XmlNodeType.Text){
						switch(currentElement){
							case "ComputerName":
								computerName=reader.Value;
								break;
							case "Database":
								database=reader.Value;
								break;
							case "User":
								user=reader.Value;
								break;
							case "Password":
								password=reader.Value;
								break;
						}
					}
				}
				reader.Close();
			}
			catch{
				return "";
			}
			return "Server="+computerName
				+";Database="+database
				+";User ID="+user
				+";Password="+password
				+";CharSet=utf8";
		}

		static internal bool TestConnection(string dataProvider, string connection)
		{
			IDbConnection cnSQL=null;
			bool bResult = false;
			try
			{
				switch (dataProvider)
				{
					case "SQL":
						cnSQL = new SqlConnection(connection);
						cnSQL.Open();
						break;
					case "ODBC":
						cnSQL = new OdbcConnection(connection);
						cnSQL.Open();
						break;
					case "OLEDB":
						cnSQL = new OleDbConnection(connection);
						cnSQL.Open();
						break;
				}
				bResult = true;			// we opened the connection
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Unable to open connection");
			}
			finally
			{
				if (cnSQL != null)
				{
					cnSQL.Close();
					cnSQL.Dispose();
				}
			}
			return bResult;
		}

		static internal bool IsNumeric(Type t)
		{
			string st = t.ToString();	
			switch (st)
			{
				case "System.Int16":
				case "System.Int32":
				case "System.Single":
				case "System.Double":
				case "System.Decimal":
					return true;
				default:
					return false;
			}
		}

		/// <summary>
		/// Validates a size parameter
		/// </summary>
		/// <param name="t"></param>
		/// <param name="bZero">true if 0 is valid size</param>
		/// <param name="bMinus">true if minus is allowed</param>
		/// <returns>Throws exception with the invalid message</returns>
		static internal void ValidateSize(string t, bool bZero, bool bMinus)
		{
			t = t.Trim();
			if (t.Length == 0)		// not specified is ok?
				return;

			// Ensure we have valid units
			if (t.IndexOf("in") < 0 &&
				t.IndexOf("cm") < 0 &&
				t.IndexOf("mm") < 0 &&
				t.IndexOf("pt") < 0 &&
				t.IndexOf("pc") < 0)
			{
				throw new Exception("Size unit is not valid.  Must be in, cm, mm, pt, or pc.");
			}

			int space = t.LastIndexOf(' '); 
			
			string n="";					// number string
			string u;						// unit string
			try		// Convert.ToDecimal can be very picky
			{
				if (space != -1)	// any spaces
				{
					n = t.Substring(0,space).Trim();	// number string
					u = t.Substring(space).Trim();	// unit string
				}
				else if (t.Length >= 3)
				{
					n = t.Substring(0, t.Length-2).Trim();
					u = t.Substring(t.Length-2).Trim();
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

			if (n.Length == 0 || !Regex.IsMatch(n, @"\A[ ]*[-]?[0-9]*[.]?[0-9]*[ ]*\Z"))
			{
				throw new Exception("Number format is invalid.  ###.## is the proper form.");
			}

			float v = DesignXmlDraw.GetSize(t);
			if (!bZero)
			{
				if (v < .1)
					throw new Exception("Size can't be zero.");
			}
			else if (v < 0 && !bMinus)
				throw new Exception("Size can't be less than zero.");

			return;
		}

		static internal string MakeValidSize(string t, bool bZero)
		{
			return MakeValidSize(t, bZero, false);
		}

		/// <summary>
		/// Ensures that a user provided string results in a valid size
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		static internal string MakeValidSize(string t, bool bZero, bool bNegative)
		{
			// Ensure we have valid units
			if (t.IndexOf("in") < 0 &&
				t.IndexOf("cm") < 0 &&
				t.IndexOf("mm") < 0 &&
				t.IndexOf("pt") < 0 &&
				t.IndexOf("pc") < 0)
			{
				t += "in";
			}

			float v = DesignXmlDraw.GetSize(t);
			if (!bZero)
			{
				if (v < .1)
					t = ".1pt";
			}
			if (!bNegative)
			{
				if (v < 0)
					t = "0in";
			}

			return t;
		}

		static private void AddParameters(IDbCommand cmSQL, IList parameters)
		{
			if (parameters == null || parameters.Count <= 0)
				return;

			foreach(ReportParm rp in parameters)
			{
				string paramName;

				// force the name to start with @
				if (rp.Name[0] == '@')
					paramName = rp.Name;
				else
					paramName = "@" + rp.Name;

				IDbDataParameter dp = cmSQL.CreateParameter();
				dp.ParameterName = paramName;
				if (rp.DefaultValue == null || rp.DefaultValue.Count == 0)
				{
					object pvalue=null;
					// put some dummy values in it;  some drivers (e.g. mysql odbc) don't like null values
					switch (rp.DataType.ToLower())
					{
						case "datetime":
							pvalue = new DateTime(0);
							break;
						case "double":
							pvalue = new double();
							break;
						case "boolean":
							pvalue = new Boolean();
							break;
						case "string":
						default:
							pvalue = (object) "";
							break;
					}
					dp.Value = pvalue;
				}
				else
				{
					string val = (string) rp.DefaultValue[0];
					dp.Value = val;
				}

				cmSQL.Parameters.Add(dp);
			}
		}
	}

	internal class SqlColumn
	{
		string _Name;
		Type _DataType;

		override public string ToString()
		{
			return _Name;
		}

		internal string Name
		{
			get {return _Name;}
			set {_Name = value;}
		}

		internal Type DataType
		{
			get {return _DataType;}
			set {_DataType = value;}
		}
	}

	internal class SqlSchemaInfo
	{
		string _Name;
		string _Type;

		internal string Name
		{
			get {return _Name;}
			set {_Name = value;}
		}

		internal string Type
		{
			get {return _Type;}
			set {_Type = value;}
		}
	}

	internal class ReportParm
	{
		string _Name;
		string _Prompt;
		string _DataType;
		
		bool   _bDefault=true;				// use default value if true otherwise DataSetName
		ArrayList _DefaultValue;			// list of strings
		string _DefaultDSRDataSetName;		// DefaultValues DataSetReference DataSetName
		string _DefaultDSRValueField;		// DefaultValues DataSetReference ValueField

		bool   _bValid=true;				// use valid value if true otherwise DataSetName
		ArrayList _ValidValues;				// list of ParameterValueItem
		string _ValidValuesDSRDataSetName;		// ValidValues DataSetReference DataSetName
		string _ValidValuesDSRValueField;		// ValidValues DataSetReference ValueField
		string _ValidValuesDSRLabelField;		// ValidValues DataSetReference LabelField
		bool _AllowNull;
		bool _AllowBlank;

		internal ReportParm(string name)
		{
			_Name = name;
			_DataType = "String";
		}

		internal string Name
		{
			get {return _Name;}
			set {_Name = value;}
		}

		internal string Prompt
		{
			get {return _Prompt;}
			set {_Prompt = value;}
		}

		internal string DataType
		{
			get {return _DataType;}
			set {_DataType = value;}
		}

		internal bool Valid
		{
			get {return _bValid;}
			set {_bValid = value;}
		}

		internal ArrayList ValidValues
		{
			get {return _ValidValues;}
			set {_ValidValues = value;}
		}

		internal string ValidValuesDisplay
		{
			get 
			{
				if (_ValidValues == null || _ValidValues.Count == 0)
					return "";
				StringBuilder sb = new StringBuilder();
				bool bFirst = true;
				foreach (ParameterValueItem pvi in _ValidValues)
				{
					if (bFirst)
						bFirst = false;
					else
						sb.Append(", ");
					if (pvi.Label != null)
						sb.AppendFormat("{0}={1}", pvi.Value, pvi.Label);
					else
						sb.Append(pvi.Value);
				}
				return sb.ToString();
			}
		}

		internal bool Default
		{
			get {return _bDefault;}
			set {_bDefault = value;}
		}

		internal ArrayList DefaultValue
		{
			get {return _DefaultValue;}
			set {_DefaultValue = value;}
		}

		internal string DefaultValueDisplay
		{
			get 
			{
				if (_DefaultValue == null || _DefaultValue.Count == 0)
					return "";
				StringBuilder sb = new StringBuilder();
				bool bFirst = true;
				foreach (string dv in _DefaultValue)
				{
					if (bFirst)
						bFirst = false;
					else
						sb.Append(", ");
					sb.Append(dv);
				}
				return sb.ToString();
			}
		}
		internal bool AllowNull
		{
			get {return _AllowNull;}
			set {_AllowNull = value;}
		}

		internal bool AllowBlank
		{
			get {return _AllowBlank;}
			set {_AllowBlank = value;}
		}
		internal string DefaultDSRDataSetName
		{
			get {return _DefaultDSRDataSetName;}
			set {_DefaultDSRDataSetName=value;}
		}
		internal string DefaultDSRValueField
		{
			get {return _DefaultDSRValueField;}
			set {_DefaultDSRValueField=value;}
		}

		internal string ValidValuesDSRDataSetName
		{
			get {return _ValidValuesDSRDataSetName;}
			set {_ValidValuesDSRDataSetName=value;}
		}
		internal string  ValidValuesDSRValueField
		{
			get {return _ValidValuesDSRValueField;}
			set {_ValidValuesDSRValueField=value;}
		}
		internal string ValidValuesDSRLabelField
		{
			get {return _ValidValuesDSRLabelField;}
			set {_ValidValuesDSRLabelField=value;}
		}

		override public string ToString()
		{
			return _Name;
		}
	}

	internal class ParameterValueItem
	{
		internal string Value;
		internal string Label;
	}

	internal class StaticLists
	{
		/// <summary>
		/// Names of colors to put into lists
		/// </summary>
		static public readonly string[] ColorList = new string[] {
										"Aliceblue",
										"Antiquewhite",
										"Aqua",
										"Aquamarine",
										"Azure",
										"Beige",
										"Bisque",
										"Black",
										"Blanchedalmond",
										"Blue",
										"Blueviolet",
										"Brown",
										"Burlywood",
										"Cadetblue",
										"Chartreuse",
										"Chocolate",
										"Coral",
										"Cornflowerblue",
										"Cornsilk",
										"Crimson",
										"Cyan",
										"Darkblue",
										"Darkcyan",
										"Darkgoldenrod",
										"Darkgray",
										"Darkgreen",
										"Darkkhaki",
										"Darkmagenta",
										"Darkolivegreen",
										"Darkorange",
										"Darkorchid",
										"Darkred",
										"Darksalmon",
										"Darkseagreen",
										"Darkslateblue",
										"Darkslategray",
										"Darkturquoise",
										"Darkviolet",
										"Deeppink",
										"Deepskyblue",
										"Dimgray",
										"Dodgerblue",
										"Firebrick",
										"Floralwhite",
										"Forestgreen",
										"Fuchsia",
										"Gainsboro",
										"Ghostwhite",
										"Gold",
										"Goldenrod",
										"Gray",
										"Green",
										"Greenyellow",
										"Honeydew",
										"Hotpink",
										"Indianred Indigo",
										"Ivory",
										"Khaki",
										"Lavender",
										"Lavenderblush",
										"Lawngreen",
										"Lemonchiffon",
										"Lightblue",
										"Lightcoral",
										"Lightcyan",
										"Lightgoldenrodyellow",
										"Lightgreen",
										"Lightgrey",
										"Lightpink",
										"Lightsalmon",
										"Lightseagreen",
										"Lightskyblue",
										"Lightslategrey",
										"Lightsteelblue",
										"Lightyellow",
										"Lime",
										"Limegreen",
										"Linen",
										"Magenta",
										"Maroon",
										"Mediumaquamarine",
										"Mediumblue",
										"Mediumorchid",
										"Mediumpurple",
										"Mediumseagreen",
										"Mediumslateblue",
										"Mediumspringgreen",
										"Mediumturquoise",
										"Mediumvioletred",
										"Midnightblue",
										"Mintcream",
										"Mistyrose",
										"Moccasin",
										"Navajowhite",
										"Navy",
										"Oldlace",
										"Olive",
										"Olivedrab",
										"Orange",
										"Orangered",
										"Orchid",
										"Palegoldenrod",
										"Palegreen",
										"Paleturquoise",
										"Palevioletred",
										"Papayawhip",
										"Peachpuff",
										"Peru",
										"Pink",
										"Plum",
										"Powderblue",
										"Purple",
										"Red",
										"Rosybrown",
										"Royalblue",
										"Saddlebrown",
										"Salmon",
										"Sandybrown",
										"Seagreen",
										"Seashell",
										"Sienna",
										"Silver",
										"Skyblue",
										"Slateblue",
										"Slategray",
										"Snow",
										"Springgreen",
										"Steelblue",
										"Tan",
										"Teal",
										"Thistle",
										"Tomato",
										"Turquoise",
										"Violet",
										"Wheat",
										"White",
										"Whitesmoke",
										"Yellow",
										"Yellowgreen"};
		/// <summary>
		/// Names of globals to put into expressions
		/// </summary>
		static public readonly string[] GlobalList = new string[] {
																	  "=Globals!PageNumber.Value",
																	  "=Globals!TotalPages.Value",
																	  "=Globals!ExecutionTime.Value",
																	  "=Globals!ReportFolder.Value",
																	  "=Globals!ReportName.Value"};
		/// <summary>
		/// Zoom values
		/// </summary>
		static public readonly string[] ZoomList = new string[] {
									"Actual Size",
									"Fit Page",
									"Fit Width",
									"800%",
									"400%",
									"200%",
									"150%",
									"125%",
									"100%",
									"75%",
									"50%",
									"25%"};
	}
}
