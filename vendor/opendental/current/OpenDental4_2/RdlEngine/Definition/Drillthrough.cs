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
	/// Defines information needed for creating links to URLs in a report.  Primarily HTML.
	///</summary>
	[Serializable]
	internal class Drillthrough : ReportLink
	{
		string _ReportName;	// URL The path of the drillthrough report. Paths may be
							// absolute or relative.
		DrillthroughParameters _DrillthroughParameters;	// Parameters to the drillthrough report		
	
		internal Drillthrough(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_ReportName=null;
			_DrillthroughParameters=null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "ReportName":
						_ReportName = xNodeLoop.InnerText;
						break;
					case "Parameters":
						_DrillthroughParameters = new DrillthroughParameters(r, this, xNodeLoop);
						break;
					default:
						break;
				}
			}
			if (_ReportName == null)
				OwnerReport.rl.LogError(8, "Drillthrough requires the ReportName element.");
		}
		
		override internal void FinalPass()
		{
			if (_DrillthroughParameters != null)
				_DrillthroughParameters.FinalPass();
			return;
		}

		internal string ReportName
		{
			get { return  _ReportName; }
			set {  _ReportName = value; }
		}

		internal DrillthroughParameters DrillthroughParameters
		{
			get { return  _DrillthroughParameters; }
			set {  _DrillthroughParameters = value; }
		}
	}
}
