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

namespace fyiReporting.RDL
{
	///<summary>
	/// Definition of a field within a DataSet.   
	///</summary>
	[Serializable]
	internal class Field : ReportLink
	{
		Name _Name;			// Name to use for the field within the report
		// Note: Field names need only be unique
		//  within the containing Fields collection
		// Note: Either _DataField or _Value must be specified but not both
		string _DataField;	// Name of the field in the query
		// Note: DataField names do not need to be
		// unique. Multiple fields can refer to the same
		// data field.
		int _ColumnNumber;	// Column number
		TypeCode _Type;	// The data type of the field
		QueryColumn qc;		// Query column: resolved from the query SQL
		Expression _Value;	// (Variant) An expression that evaluates to the value of
		//  this field.
		// For example,
		// =Fields!Price.Value+Fields!Tax.Value
		// The expression cannot contain aggregates or
		// references to report items.	
		// runtime variables
		[NonSerialized] int _QueryColumnNumber;	// Determined at runtime query


		internal Field(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_Name=null;
			_DataField=null;
			_Value=null;
			_ColumnNumber = -1;
			_Type = TypeCode.String;
			// Run thru the attributes
			foreach(XmlAttribute xAttr in xNode.Attributes)
			{
				switch (xAttr.Name)
				{
					case "Name":
						_Name = new Name(xAttr.Value);
						break;
				}
			}
			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "DataField":
						_DataField = xNodeLoop.InnerText;
						break;
					case "TypeName":		// Extension !!!!!!!!!!!!!!!!!
					case "rd:TypeName":		// Microsoft Designer uses this extension
						_Type = DataType.GetStyle(xNodeLoop.InnerText, this.OwnerReport);
						break;
					case "Value":
						_Value = new Expression(r, this, xNodeLoop, ExpressionType.Variant);
						break;
					default:
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown Field element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
			if (_DataField != null && _Value != null)
				OwnerReport.rl.LogError(8, "Only DataField or Value may be specified in a Field element, not both.");
			else if (_DataField == null && _Value == null)
				OwnerReport.rl.LogError(8, "Either DataField or Value must be specified in a Field element.");
		}

		// Handle parsing of function in final pass
		override internal void FinalPass()
		{
			if (_Value != null)
				_Value.FinalPass();

			// Resolve the field if specified
			if (_DataField != null)
			{
				Fields f = (Fields) this.Parent;
				DataSet ds = (DataSet) f.Parent;
				Query q = ds.Query;
				if (q != null && q.Columns != null)
				{
					qc = (QueryColumn) q.Columns[_DataField];
					if (qc == null)
					{	// couldn't find the data field
						OwnerReport.rl.LogError(8, "DataField '" + _DataField + "' not part of query.");
					}
				}
			}

			return;
		}

		internal Name Name
		{
			get { return  _Name; }
			set {  _Name = value; }
		}

		internal string DataField
		{
			get { return  _DataField; }
			set {  _DataField = value; }
		}

		internal Expression Value
		{
			get { return  _Value; }
			set {  _Value = value; }
		}

		internal int ColumnNumber
		{
			get { return  _ColumnNumber; }
			set {  _ColumnNumber = value; }
		}

		internal int QueryColumnNumber
		{
			get { return  _QueryColumnNumber; }
			set {  _QueryColumnNumber = value; }
		}

		internal QueryColumn qColumn
		{
			get { return  qc; }
		}

		internal TypeCode Type
		{
			get { return  _Type; }
			set {  _Type = value; }
		}

		internal TypeCode RunType
		{
			get 
			{
				if (qc != null)
					return qc.colType;
				else 
					return _Type;
			}
		}
	}
}
