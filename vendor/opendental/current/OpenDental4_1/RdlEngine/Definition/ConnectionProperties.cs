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
	/// Information about how to connect to the data source.
	///</summary>
	[Serializable]
	internal class ConnectionProperties : ReportLink
	{
		string _DataProvider;	// The type of the data source. This will determine
								// the syntax of the Connectstring and
								// CommandText. Supported types are SQL, OLEDB, ODBC, Oracle
		string _ConnectString;	// The connection string for the data source
		bool _IntegratedSecurity;	// Indicates that this data source should connected
									// to using integrated security
		string _Prompt;			// The prompt displayed to the user when
								// prompting for database credentials for this data source.
	
		internal ConnectionProperties(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_DataProvider=null;
			_ConnectString=null;
			_IntegratedSecurity=false;
			_Prompt=null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "DataProvider":
						_DataProvider = xNodeLoop.InnerText;
						break;
					case "ConnectString":
						_ConnectString = xNodeLoop.InnerText;
						break;
					case "IntegratedSecurity":
						_IntegratedSecurity = XmlUtil.Boolean(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					case "Prompt":
						_Prompt = xNode.InnerText;
						break;
					default:
						break;
				}
			}
			if (_DataProvider == null)
				OwnerReport.rl.LogError(8, "ConnectionProperties DataProvider is required.");
			if (_ConnectString == null)
				OwnerReport.rl.LogError(8, "ConnectionProperties ConnectString is required.");
		}
		
		override internal void FinalPass()
		{
			return;
		}

		internal string DataProvider
		{
			get { return  _DataProvider; }
			set {  _DataProvider = value; }
		}

		internal string Connectstring
		{
			get { return  _ConnectString; }
			set {  _ConnectString = value; }
		}

		internal bool IntegratedSecurity
		{
			get { return  _IntegratedSecurity; }
			set {  _IntegratedSecurity = value; }
		}

		internal string Prompt
		{
			get { return  _Prompt; }
			set {  _Prompt = value; }
		}
	}
}
