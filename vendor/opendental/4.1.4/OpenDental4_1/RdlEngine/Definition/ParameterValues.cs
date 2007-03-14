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
using System.Xml;

namespace fyiReporting.RDL
{
	///<summary>
	/// Collection of parameter values.
	///</summary>
	[Serializable]
	internal class ParameterValues : ReportLink
	{
		ArrayList _Items;			// list of report items

		internal ParameterValues(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			ParameterValue pv;
			_Items = new ArrayList();
			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "ParameterValue":
						pv = new ParameterValue(r, this, xNodeLoop);
						break;
					default:
						pv=null;		// don't know what this is
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown ParameterValues element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
				if (pv != null)
					_Items.Add(pv);
			}

			if (_Items.Count == 0)
				OwnerReport.rl.LogError(8, "For ParameterValues at least one ParameterValue is required.");
		}
		
		override internal void FinalPass()
		{
			foreach (ParameterValue pv in _Items)
			{
				pv.FinalPass();
			}
			return;
		}

		internal ArrayList Items
		{
			get { return  _Items; }
		}

		internal void SupplyValues(out string[] displayValues, out object[] dataValues)
		{
			displayValues = new string[_Items.Count];
			dataValues = new object[_Items.Count];
			int index=0;
			// go thru the parameters extracting the data values
			foreach (ParameterValue pv in _Items)
			{
				if (pv.Value == null)
					dataValues[index] = null;
				else
					dataValues[index] = pv.Value.Evaluate(null);
				if (pv.Label == null)
				{	// if label is null use the data value; if not provided use ""
					if (dataValues[index] == null)
						displayValues[index] = "";
					else
						displayValues[index] = dataValues[index].ToString();
				}
				else
				{
					displayValues[index] = pv.Label.EvaluateString(null);
					if (displayValues[index] == null)
						displayValues[index] = "";
				}
				index++;
			}
			return;
		}
	}
}
