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
using System.Collections;

namespace fyiReporting.RDL
{
	///<summary>
	/// Parsing name lookup.  Fields, parameters, report items, globals, user, aggregate scopes, grouping,...
	///</summary>
	[Serializable]
	internal class NameLookup
	{
		IDictionary fields;
		IDictionary parameters;
		IDictionary reportitems;
		IDictionary globals;
		IDictionary user;
		IDictionary aggrScope;
		Grouping g;					// if expression in a table group or detail group
									//   used to default aggregates to the right group
		Matrix m;					// if expression used in a Matrix
									//   used to default aggregate to the right matrix
		Classes instances;
		CodeModules cms;
		DataSets dsets;

		internal NameLookup(IDictionary f, IDictionary p, IDictionary r, 
			IDictionary gbl, IDictionary u, IDictionary ascope, 
			Grouping ag, Matrix mt, CodeModules cm, Classes i, DataSets ds)
		{
			fields=f;
			parameters=p;
			reportitems=r;
			globals=gbl;
			user=u;
			aggrScope = ascope;
			g=ag;
			m = mt;
			cms = cm;
			instances = i;
			dsets = ds;
		}

		internal ReportClass LookupInstance(string name)
		{
			if (instances == null)
				return null;
			return instances[name];
		}

		internal Field LookupField(string name)
		{	
			if (fields == null)
				return null;

			return (Field) fields[name];
		}

		internal ReportParameter LookupParameter(string name)
		{	
			if (parameters == null)
				return null;
			return (ReportParameter) parameters[name];
		}

		internal Textbox LookupReportItem(string name)
		{	
			if (reportitems == null)
				return null;
			return (Textbox) reportitems[name];
		}

		internal IExpr LookupGlobal(string name)
		{	
			if (globals == null)
				return null;
			return (IExpr) globals[name];
		}

		internal Type LookupType(string clsname)
		{
			if (cms == null)
				return null;
			return cms[clsname];
		}

		internal CodeModules CMS
		{
			get{return cms;}
		}

		internal IExpr LookupUser(string name)
		{	
			if (user == null)
				return null;
			return (IExpr) user[name];
		}

		internal Grouping LookupGrouping()
		{
			return g;
		}

		internal Matrix LookupMatrix()
		{
			return m;
		}

		internal object LookupScope(string name)
		{
			if (aggrScope == null)
				return null;
			return aggrScope[name];
		}

		internal DataSet ScopeDataSet(string name)
		{
			if (name == null)
			{	// Only allowed when there is only one dataset
				if (dsets.Items.Count != 1)
					return null;
				foreach (DataSet ds in dsets.Items.Values)	// No easy way to get the item by index
					return ds;
				return null;
			}
			return dsets[name];
		}

	}
}
