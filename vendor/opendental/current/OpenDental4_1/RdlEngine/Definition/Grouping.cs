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
using System.Collections;

namespace fyiReporting.RDL
{
	///<summary>
	/// Grouping definition: expressions forming group, paging forced when group changes, ...
	///</summary>
	[Serializable]
	internal class Grouping : ReportLink
	{
		Name _Name;		// Name of the Grouping (for use in
						// RunningValue and RowNumber)
						// No two grouping elements may have the
						// same name. No grouping element may
						// have the same name as a data set or a data
						// region
		Expression _Label;	// (string) A label to identify an instance of the group
						//within the client UI (to provide a userfriendly
						// label for searching). See ReportItem.Label
		GroupExpressions _GroupExpressions;	//The expressions to group the data by
		bool _PageBreakAtStart;	// Indicates the report should page break at
						// the start of the group.
						// Not valid for column groupings in Matrix regions.
		bool _PageBreakAtEnd;	// Indicates the report should page break at
						// the end of the group.
						// Not valid for column groupings in Matrix regions.
		Custom _Custom; // Custom information to be passed to the
						// report output component.
		Filters _Filters;	// Filters to apply to each instance of the group.
		Expression _ParentGroup; //(Variant)
						//An expression that identifies the parent
						//group in a recursive hierarchy. Only
						//allowed if the group has exactly one group
						//expression.
						//Indicates the following:
						//1. Groups should be sorted according
						//to the recursive hierarchy (Sort is
						//still used to sort peer groups).
						//2. Labels (in the document map)
						//should be placed/indented
						//according to the recursive
						//hierarchy.
						//3. Intra-group show/hide should
						//toggle items according to the
						//recursive hierarchy (see
						//ToggleItem)
						//If filters on the group eliminate a group
						// instance’s parent, it is instead treated as a
						// child of the parent’s parent.
		string _DataElementName;	// The name to use for the data element for
									// instances of this group.
									// Default: Name of the group
		string _DataCollectionName;	// The name to use for the data element for
						// the collection of all instances of this group.
						// Default: “DataElementName_Collection”
		DataElementOutputEnum _DataElementOutput;	// Indicates whether the group should appear
						// in a data rendering.  Default: Output
		ArrayList _HideDuplicates;	// holds any textboxes that use this as a hideduplicate scope

		// runtime working value
		int _Index=-1;	// index for runtime grouping
		internal Grouping(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_Name=null;
			_Label=null;
			_GroupExpressions=null;
			_PageBreakAtStart=false;
			_PageBreakAtEnd=false;
			_Custom=null;
			_Filters=null;
			_ParentGroup=null;
			_DataElementName=null;
			_DataCollectionName=null;
			_DataElementOutput=DataElementOutputEnum.Output;
			_HideDuplicates=null;
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
					case "Label":
						_Label = new Expression(r, this, xNodeLoop, ExpressionType.String);
						break;
					case "GroupExpressions":
						_GroupExpressions = new GroupExpressions(r, this, xNodeLoop);
						break;
					case "PageBreakAtStart":
						_PageBreakAtStart = XmlUtil.Boolean(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					case "PageBreakAtEnd":
						_PageBreakAtEnd = XmlUtil.Boolean(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					case "Custom":
						_Custom = new Custom(r, this, xNodeLoop);
						break;
					case "Filters":
						_Filters = new Filters(r, this, xNodeLoop);
						break;
					case "Parent":
						_ParentGroup = new Expression(r, this, xNodeLoop, ExpressionType.Variant);
						break;
					case "DataElementName":
						_DataElementName = xNodeLoop.InnerText;
						break;
					case "DataCollectionName":
						_DataCollectionName = xNodeLoop.InnerText;
						break;
					case "DataElementOutput":
						_DataElementOutput = fyiReporting.RDL.DataElementOutput.GetStyle(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					default:	
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown Grouping element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
			if (this.Name != null)
			{
				try
				{
					OwnerReport.LUAggrScope.Add(this.Name.Nm, this);		// add to referenceable Grouping's
				}
				catch	// wish duplicate had its own exception
				{
					OwnerReport.rl.LogError(8, "Duplicate Grouping name '" + this.Name.Nm + "'.");
				}
			}
			if (_GroupExpressions == null)
				OwnerReport.rl.LogError(8, "Group Expressions are required within group '" + (this.Name==null? "unnamed": this.Name.Nm) + "'.");
		}

		// Handle parsing of function in final pass
		override internal void FinalPass()
		{
			if (_Label != null)
				_Label.FinalPass();
			if (_GroupExpressions != null)
				_GroupExpressions.FinalPass();
			if (_Custom != null)
				_Custom.FinalPass();
			if (_Filters != null)
				_Filters.FinalPass();
			if (_ParentGroup != null)
				_ParentGroup.FinalPass();
			return;
		}

		internal void AddHideDuplicates(Textbox tb)
		{
			if (_HideDuplicates == null)
				_HideDuplicates = new ArrayList();
			_HideDuplicates.Add(tb);
		}

		internal void ResetHideDuplicates()
		{
			if (_HideDuplicates == null)
				return;

			foreach (Textbox tb in _HideDuplicates)
				tb.ResetPrevious();
		}

		internal Name Name
		{
			get { return  _Name; }
			set {  _Name = value; }
		}

		internal Expression Label
		{
			get { return  _Label; }
			set {  _Label = value; }
		}

		internal GroupExpressions GroupExpressions
		{
			get { return  _GroupExpressions; }
			set {  _GroupExpressions = value; }
		}

		internal bool PageBreakAtStart
		{
			get { return  _PageBreakAtStart; }
			set {  _PageBreakAtStart = value; }
		}

		internal bool PageBreakAtEnd
		{
			get { return  _PageBreakAtEnd; }
			set {  _PageBreakAtEnd = value; }
		}

		internal Custom Custom
		{
			get { return  _Custom; }
			set {  _Custom = value; }
		}

		internal Filters Filters
		{
			get { return  _Filters; }
			set {  _Filters = value; }
		}

		internal Expression ParentGroup
		{
			get { return  _ParentGroup; }
			set {  _ParentGroup = value; }
		}

		internal string DataElementName
		{
			get 
			{ 
				if (_DataElementName == null)
				{
					if (this.Name != null)
						return this.Name.Nm;
				}
				return  _DataElementName; 
			}
			set {  _DataElementName = value; }
		}

		internal string DataCollectionName
		{
			get 
			{
				if (_DataCollectionName == null)
					return DataElementName + "_Collection";
				return  _DataCollectionName; 
			}
			set {  _DataCollectionName = value; }
		}

		internal DataElementOutputEnum DataElementOutput
		{
			get { return  _DataElementOutput; }
			set {  _DataElementOutput = value; }
		}

		internal int Index
		{
			get { return  _Index; }
			set {  _Index = value; }
		}
	}
}
