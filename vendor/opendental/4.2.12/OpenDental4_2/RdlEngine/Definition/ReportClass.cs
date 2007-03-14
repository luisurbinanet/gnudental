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
using System.Reflection;

namespace fyiReporting.RDL
{
	///<summary>
	/// ReportClass represents the Class report element. 
	///</summary>
	[Serializable]
	internal class ReportClass : ReportLink
	{
		string _ClassName;		// The name of the class
		Name _InstanceName;		// The name of the variable to assign the class to.
								// This variable can be used in expressions
								// throughout the report.
		[NonSerialized] object _Instance=null;	// 
		[NonSerialized] bool bCreateFailed=false;
	
		internal ReportClass(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_ClassName=null;
			_InstanceName = null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "ClassName":
						_ClassName = xNodeLoop.InnerText;
						break;
					case "InstanceName":
						_InstanceName = new Name(xNodeLoop.InnerText);
						break;
					default:
						break;
				}
			}
			if (_ClassName == null)
				OwnerReport.rl.LogError(8, "Class ClassName is required but not specified.");

			if (_InstanceName == null)
				OwnerReport.rl.LogError(8, "Class InstanceName is required but not specified or invalid for " + _ClassName==null? "<unknown name>": _ClassName);
		}
		
		override internal void FinalPass()
		{
			return;
		}

		internal void Load()
		{
			if (bCreateFailed)		// We only try to create once.
				return;

			if (_Instance != null)	// Already loaded
				return;

			if (OwnerReport.CodeModules == null)	// nothing to load against
				return;

			// Load an instance of the object
			string err="";
			try
			{
				Type tp = OwnerReport.CodeModules[_ClassName];
				if (tp != null)
				{
					Assembly asm = tp.Assembly;
					_Instance = asm.CreateInstance(_ClassName, false);
				}
				else
					err = "Class not found.";
			}
			catch (Exception e)
			{
				_Instance = null;
				err = e.Message;
			}

			if (_Instance == null)
			{
				OwnerReport.rl.LogError(4, String.Format("Unable to create instance of class {0}.  {1}",
					_ClassName, err));
				bCreateFailed = true;
			}
			return;			
		}

		internal string ClassName
		{
			get { return  _ClassName; }
		}

		internal Name InstanceName
		{
			get { return  _InstanceName; }
		}

		internal object Instance
		{
			get 
			{
				if (_Instance == null)			// try to load
					Load();
				return  _Instance; 
			}
		}
	}
}
