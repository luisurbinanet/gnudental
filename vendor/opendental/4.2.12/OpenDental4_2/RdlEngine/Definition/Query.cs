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
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Globalization;
using System.Reflection;

namespace fyiReporting.RDL
{
	///<summary>
	/// Query representation against a data source.  Holds the data at runtime.
	///</summary>
	[Serializable]
	internal class Query : ReportLink
	{
		string _DataSourceName;		// Name of the data source to execute the query against
		DataSource _DataSource;		//  the data source object the DataSourceName references.
		QueryCommandTypeEnum _QueryCommandType;	// Indicates what type of query is contained in the CommandText
		Expression _CommandText;	//	(string) The query to execute to obtain the data for the report
		QueryParameters _QueryParameters;	// A list of parameters that are passed to the data
											// source as part of the query.		
		int _Timeout;				// Number of seconds to allow the query to run before
									// timing out.   Must be >= 0; If omitted or zero; no timeout
		int _RowLimit;				// Number of rows to retrieve before stopping retrieval; 0 means no limit

		IDictionary _Columns;		// QueryColumn (when SQL)
		[NonSerialized] Rows _Data;					// Runtime data
		[NonSerialized] Rows _UserData;				// Runtime data (set by user)

		internal Query(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_DataSourceName=null;
			_QueryCommandType=QueryCommandTypeEnum.Text;
			_CommandText=null;
			_QueryParameters=null;
			_Timeout=0;
			_RowLimit=0;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "DataSourceName":
						_DataSourceName = xNodeLoop.InnerText;
						break;
					case "QueryCommandType":
						_QueryCommandType = fyiReporting.RDL.QueryCommandType.GetStyle(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					case "CommandText":
						_CommandText = new Expression(r, this, xNodeLoop, ExpressionType.String);
						break;
					case "QueryParameters":
						_QueryParameters = new QueryParameters(r, this, xNodeLoop);
						break;
					case "Timeout":
						_Timeout = XmlUtil.Integer(xNodeLoop.InnerText);
						break;
					case "RowLimit":				// Extension of RDL specification
						_RowLimit = XmlUtil.Integer(xNodeLoop.InnerText);
						break;
					default:	
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown Query element '" + xNodeLoop.Name + "' ignored.");
						break;
				}	// end of switch
			}	// end of foreach
			
			// Resolve the data source name to the object
			if (_DataSourceName == null)
			{
				r.rl.LogError(8, "DataSourceName element not specified for Query.");
				return;
			}
		}

		// Handle parsing of function in final pass
		override internal void FinalPass()
		{
			if (_CommandText != null)
				_CommandText.FinalPass();
			if (_QueryParameters != null)
				_QueryParameters.FinalPass();

			// verify the data source
			DataSource ds=null;
			if (OwnerReport.DataSources != null &&
				OwnerReport.DataSources.Items != null)
			{
				ds = OwnerReport.DataSources[_DataSourceName];
			}
			if (ds == null)
			{
				OwnerReport.rl.LogError(8, "Query references unknown data source '" + _DataSourceName + "'");
				return;
			}
			_DataSource = ds;

			if (ds.SqlConnect == null || _CommandText == null)
				return;

			// Treat this as a SQL statement
			String sql = _CommandText.EvaluateString(null);
			IDbCommand cmSQL=null;
			IDataReader dr=null;
			try
			{
				cmSQL = ds.SqlConnect.CreateCommand();		
				cmSQL.CommandText = AddParametersAsLiterals(sql, false);
				AddParameters(cmSQL, false);
				dr = cmSQL.ExecuteReader(CommandBehavior.SchemaOnly);
				if (dr.FieldCount < 10)
					_Columns = new ListDictionary();	// Hashtable is overkill for small lists
				else
					_Columns = new Hashtable();

				for (int i=0; i < dr.FieldCount; i++)
				{
					QueryColumn qc = new QueryColumn(i, dr.GetName(i), Type.GetTypeCode(dr.GetFieldType(i)) );

					try { _Columns.Add(qc.colName, qc); }
					catch	// name has already been added to list: 
					{	// According to the RDL spec SQL names are matched by Name not by relative
						//   position: this seems wrong to me and causes this problem; but 
						//   user can fix by using "as" keyword to name columns in Select 
						//    e.g.  Select col as "col1", col as "col2" from tableA
						OwnerReport.rl.LogError(8, String.Format("Column '{0}' is not uniquely defined within the SQL Select columns.", qc.colName));
					}
				}
			}
			catch (Exception e)
			{
				OwnerReport.rl.LogError(8, "SQL Exception: " + e.Message + "\r\nSQL: " + sql);
			}
			finally
			{
				if (cmSQL != null)
				{
					cmSQL.Dispose();
					if (dr != null)
						dr.Close();
				}
			}

			return;
		}

		// Handle parsing of function in final pass
		internal void GetData(Fields flds, Filters f)
		{
			if (_UserData != null) 
			{
				_Data = _UserData;
				return;
			}
			
			DataSource ds = _DataSource;

			// Treat this as a SQL statement
			if (ds == null || ds.SqlConnect == null || _CommandText == null) 
			{
				_Data = null;
				return;
			}

			_Data = new Rows(null,null,null);		// no sorting and grouping at base data
			String sql = _CommandText.EvaluateString(null);
			IDbCommand cmSQL=null;
			IDataReader dr=null;
			try
			{
				cmSQL = ds.SqlConnect.CreateCommand();		
				cmSQL.CommandText = AddParametersAsLiterals(sql, true);
				//if (this._Timeout > 0)
				//	cmSQL.CommandTimeout = this._Timeout;
				
				AddParameters(cmSQL, true);
				dr = cmSQL.ExecuteReader(CommandBehavior.SingleResult);

				ArrayList ar = new ArrayList();
				_Data.Data = ar;
				int rowCount=0;
				int maxRows = _RowLimit > 0? _RowLimit: int.MaxValue;
				int fieldCount = flds.Items.Count;

				// Determine the query column number for each field
				foreach (Field fld in flds)
				{
					fld.QueryColumnNumber = -1;
					if (fld.Value != null)
						continue;
					try
					{
						fld.QueryColumnNumber = dr.GetOrdinal(fld.DataField);
					}
					catch
					{
						fld.QueryColumnNumber = -1;
					}
				}

				while (dr.Read())
				{
					Row or = new Row(_Data, fieldCount);

					foreach (Field fld in flds)
					{
						if (fld.QueryColumnNumber != -1)
						{
							or.Data[fld.ColumnNumber] = dr.GetValue(fld.QueryColumnNumber);
						}
					}

					// Apply the filters
					if (f == null || f.Apply(or))
					{
						or.RowNumber = rowCount;	// 
						rowCount++;
						ar.Add(or);
					}
					if (--maxRows <= 0)				// don't retrieve more than max
						break;
				}
				ar.TrimToSize();		// free up any extraneous space; can be sizeable for large # rows
				if (f != null)
					f.ApplyFinalFilters(_Data, false);
//#if DEBUG
//				OwnerReport.rl.LogError(4, "Rows Read:" + ar.Count.ToString() + " SQL:" + sql );
//#endif
			}
			catch (Exception e)
			{
				OwnerReport.rl.LogError(8, "SQL Exception" + e.Message + "\r\n" + e.StackTrace);
			}
			finally
			{
				if (cmSQL != null)
				{
					cmSQL.Dispose();
					if (dr != null)
						dr.Close();
				}
			}
		}

		// Obtain the data from the XML
		internal void GetData(string xmlData, Fields flds, Filters f)
		{
			if (_UserData != null)
			{
				_Data = _UserData;
				return;
			}

			int fieldCount = flds.Items.Count;

			XmlDocument doc = new XmlDocument();
			doc.PreserveWhitespace = false;
			doc.LoadXml(xmlData);

			XmlNode xNode;
			xNode = doc.LastChild;
			if (xNode == null || xNode.Name != "Rows")
			{
				throw new Exception("Error: XML Data must contain top level rows.");
			}

			_Data = new Rows(null,null,null);		
			ArrayList ar = new ArrayList();
			_Data.Data = ar;

			int rowCount=0;
			foreach(XmlNode xNodeRow in xNode.ChildNodes)
			{
				if (xNodeRow.NodeType != XmlNodeType.Element)
					continue;
				if (xNodeRow.Name != "Row")
					continue;
				Row or = new Row(_Data, fieldCount);
				foreach (XmlNode xNodeColumn in xNodeRow.ChildNodes)
				{	
					Field fld = (Field) (flds.Items[xNodeColumn.Name]);	// Find the column
					if (fld == null)
						continue;			// Extraneous data is ignored
					TypeCode tc = fld.qColumn != null? fld.qColumn.colType: fld.Type;

					if (xNodeColumn.InnerText == null || xNodeColumn.InnerText.Length == 0)
						or.Data[fld.ColumnNumber] = null;
					else if (tc == TypeCode.String)
						or.Data[fld.ColumnNumber] = xNodeColumn.InnerText;
					else
					{
						try
						{
							or.Data[fld.ColumnNumber] = 
								Convert.ChangeType(xNodeColumn.InnerText, tc, NumberFormatInfo.InvariantInfo);
						}
						catch	// all conversion errors result in a null value
						{
							or.Data[fld.ColumnNumber] = null;
						}
					}
				}
				// Apply the filters 
				if (f == null || f.Apply(or))
				{
					or.RowNumber = rowCount;	// 
					rowCount++;
					ar.Add(or);
				}
			}
					
			ar.TrimToSize();		// free up any extraneous space; can be sizeable for large # rows
			if (f != null)
				f.ApplyFinalFilters(_Data, false);
		}
		internal void SetData(IEnumerable ie, Fields flds, Filters f)
		{
			if (ie == null)			// Does user want to remove user data?
			{	
				_UserData = null;
				return;
			}

			Rows rows = new Rows(null,null,null);		// no sorting and grouping at base data

			ArrayList ar = new ArrayList();
			rows.Data = ar;
			int rowCount=0;
			int maxRows = _RowLimit > 0? _RowLimit: int.MaxValue;
			int fieldCount = flds.Items.Count;
			foreach (object dt in ie)
			{
				// Get the type.
				Type myType = dt.GetType();

				// Build the row
				Row or = new Row(rows, fieldCount);

				// Go thru each field and try to obtain a value
				foreach (Field fld in flds)
				{
					// Get the type and fields of FieldInfoClass.
					FieldInfo fi = myType.GetField(fld.Name.Nm, BindingFlags.Instance | BindingFlags.Public);
					if (fi != null)
					{
						or.Data[fld.ColumnNumber] = fi.GetValue(dt);
					}
				}

				// Apply the filters 
				if (f == null || f.Apply(or))
				{
					or.RowNumber = rowCount;	// 
					rowCount++;
					ar.Add(or);
				}
				if (--maxRows <= 0)				// don't retrieve more than max
					break;
			}
			ar.TrimToSize();		// free up any extraneous space; can be sizeable for large # rows
			if (f != null)
				f.ApplyFinalFilters(_Data, false);

			_UserData = rows;
		}

		internal void SetData(IDataReader dr, Fields flds, Filters f)
		{
			if (dr == null)			// Does user want to remove user data?
			{	
				_UserData = null;
				return;
			}

			Rows rows = new Rows(null,null,null);		// no sorting and grouping at base data

			ArrayList ar = new ArrayList();
			rows.Data = ar;
			int rowCount=0;
			int maxRows = _RowLimit > 0? _RowLimit: int.MaxValue;
			while (dr.Read())
			{
				Row or = new Row(rows, dr.FieldCount);
				dr.GetValues(or.Data);
				// Apply the filters 
				if (f == null || f.Apply(or))
				{
					or.RowNumber = rowCount;	// 
					rowCount++;
					ar.Add(or);
				}
				if (--maxRows <= 0)				// don't retrieve more than max
					break;
			}
			ar.TrimToSize();		// free up any extraneous space; can be sizeable for large # rows
			if (f != null)
				f.ApplyFinalFilters(_Data, false);

			_UserData = rows;
		}

		internal void SetData(DataTable dt, Fields flds, Filters f)
		{
			if (dt == null)			// Does user want to remove user data?
			{	
				_UserData = null;
				return;
			}

			Rows rows = new Rows(null,null,null);		// no sorting and grouping at base data

			ArrayList ar = new ArrayList();
			rows.Data = ar;
			int rowCount=0;
			int maxRows = _RowLimit > 0? _RowLimit: int.MaxValue;

			int fieldCount = flds.Items.Count;
			foreach (DataRow dr in dt.Rows)
				{
				Row or = new Row(rows, fieldCount);
				// Loop thru the columns obtaining the data values by name
				foreach (Field fld in flds.Items.Values)
				{
					or.Data[fld.ColumnNumber] = dr[fld.DataField];
				}
				// Apply the filters 
				if (f == null || f.Apply(or))
				{
					or.RowNumber = rowCount;	// 
					rowCount++;
					ar.Add(or);
				}
				if (--maxRows <= 0)				// don't retrieve more than max
					break;
			}
			ar.TrimToSize();		// free up any extraneous space; can be sizeable for large # rows
			if (f != null)
				f.ApplyFinalFilters(_Data, false);

			_UserData = rows;
		}

		internal void SetData(XmlDocument xmlDoc, Fields flds, Filters f)
		{
			if (xmlDoc == null)			// Does user want to remove user data?
			{	
				_UserData = null;
				return;
			}

			Rows rows = new Rows(null,null,null);		// no sorting and grouping at base data
			
			XmlNode xNode;
			xNode = xmlDoc.LastChild;
			if (xNode == null || xNode.Name != "Rows")
			{
				throw new Exception("XML Data must contain top level element Rows.");
			}

			ArrayList ar = new ArrayList();
			rows.Data = ar;

			int rowCount=0;
			int fieldCount = flds.Items.Count;
			foreach(XmlNode xNodeRow in xNode.ChildNodes)
			{
				if (xNodeRow.NodeType != XmlNodeType.Element)
					continue;
				if (xNodeRow.Name != "Row")
					continue;
				Row or = new Row(rows, fieldCount);
				foreach (XmlNode xNodeColumn in xNodeRow.ChildNodes)
				{	
					Field fld = (Field) (flds.Items[xNodeColumn.Name]);	// Find the column
					if (fld == null)
						continue;			// Extraneous data is ignored
					if (xNodeColumn.InnerText == null || xNodeColumn.InnerText.Length == 0)
						or.Data[fld.ColumnNumber] = null;
					else if (fld.Type == TypeCode.String)
						or.Data[fld.ColumnNumber] = xNodeColumn.InnerText;
					else
					{
						try
						{
							or.Data[fld.ColumnNumber] = 
								Convert.ChangeType(xNodeColumn.InnerText, fld.Type, NumberFormatInfo.InvariantInfo);
						}
						catch	// all conversion errors result in a null value
						{
							or.Data[fld.ColumnNumber] = null;
						}
					}
				}
				// Apply the filters 
				if (f == null || f.Apply(or))
				{
					or.RowNumber = rowCount;	// 
					rowCount++;
					ar.Add(or);
				}
			}
					
			ar.TrimToSize();		// free up any extraneous space; can be sizeable for large # rows
			if (f != null)
				f.ApplyFinalFilters(rows, false);

			_UserData = rows;
		}

		private void AddParameters(IDbCommand cmSQL, bool bValue)
		{
			// any parameters to substitute
			if (this._QueryParameters == null ||
				this._QueryParameters.Items == null ||
				this._QueryParameters.Items.Count == 0)
				return;

			// Don't want to do this for ODBC datasources - AddParametersAsLiterals handles it
			/*if (this._DataSource != null &&
				this._DataSource.ConnectionProperties != null &&
				this._DataSource.ConnectionProperties.DataProvider.ToLower() == "odbc")
				return;*/

			foreach(QueryParameter qp in this._QueryParameters.Items)
			{
				string paramName;

				// force the name to start with @
				if (qp.Name.Nm[0] == '@')
					paramName = qp.Name.Nm;
				else
					paramName = "@" + qp.Name.Nm;
				object pvalue= bValue? qp.Value.Evaluate(null): null;

				IDbDataParameter dp = cmSQL.CreateParameter();
				dp.ParameterName = paramName;
				dp.Value = pvalue;
				cmSQL.Parameters.Add(dp);
			}
		}

		private string AddParametersAsLiterals(string sql, bool bValue)
		{
			// No parameters means nothing to do
			if (this._QueryParameters == null ||
				this._QueryParameters.Items == null ||
				this._QueryParameters.Items.Count == 0)
				return sql;

			// Only do this for ODBC datasources - AddParameters handles it in other cases
			/*if (this._DataSource == null ||
				this._DataSource.ConnectionProperties == null ||
				this._DataSource.ConnectionProperties.DataProvider.ToLower() != "odbc")
				return sql;*/

			StringBuilder sb = new StringBuilder(sql);
			ArrayList qlist;
			if (_QueryParameters.Items.Count <= 1)
				qlist = _QueryParameters.Items;
			else
			{	// need to sort the list so that longer items are first in the list
				// otherwise substitution could be don't incorrectly
				qlist = new ArrayList(_QueryParameters.Items);
				qlist.Sort();
			}

			foreach(QueryParameter qp in qlist)
			{
				string paramName;

				// force the name to start with @
				if (qp.Name.Nm[0] == '@')
					paramName = qp.Name.Nm;
				else
					paramName = "@" + qp.Name.Nm;

				// build the replacement value
				string svalue;
				if (bValue)
				{	// use the value provided
					svalue = qp.Value.EvaluateString(null);
					if (svalue == null)
						svalue = "null";
					else switch (qp.Value.Expr.GetTypeCode())
					{
						case TypeCode.Char:
						case TypeCode.DateTime:
						case TypeCode.String:
							// need to double up on "'" and then surround by '
							svalue = svalue.Replace("'", "''");
							svalue = "'" + svalue + "'";
							break;
					}
				}
				else
				{	// just need a place holder value that will pass parsing
					switch (qp.Value.Expr.GetTypeCode())
					{
						case TypeCode.Char:
							svalue = "' '";
							break;
						case TypeCode.DateTime:
							svalue = "'1900-01-01 00:00:00'";
							break;
						case TypeCode.Decimal:
						case TypeCode.Double:
						case TypeCode.Int32:
							svalue = "0";
							break;
						case TypeCode.Boolean:
							svalue = "'false'";
							break;
						case TypeCode.String:
						default:
							svalue = "' '";
							break;
					}
				}
				sb.Replace(paramName, svalue);
			}
			return sb.ToString();
		}

		internal string DataSourceName
		{
			get { return  _DataSourceName; }
		}

		internal DataSource DataSource
		{
			get { return  _DataSource; }
		}

		internal QueryCommandTypeEnum QueryCommandType
		{
			get { return  _QueryCommandType; }
			set {  _QueryCommandType = value; }
		}

		internal Expression CommandText
		{
			get { return  _CommandText; }
			set {  _CommandText = value; }
		}

		internal QueryParameters QueryParameters
		{
			get { return  _QueryParameters; }
			set {  _QueryParameters = value; }
		}

		internal int Timeout
		{
			get { return  _Timeout; }
			set {  _Timeout = value; }
		}

		internal IDictionary Columns
		{
			get { return  _Columns; }
		}

		internal Rows Data
		{
			get { return  _Data; }
			set {  _Data = value; }
		}
	}
}
