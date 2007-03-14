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
using System.Reflection;


namespace fyiReporting.RDL
{
	///<summary>
	/// CodeModules definition and processing.
	///</summary>
	[Serializable]
	internal class CodeModules : ReportLink, IEnumerable
	{
		ArrayList _Items;			// list of report items

		internal CodeModules(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_Items = new ArrayList();
			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				if (xNodeLoop.Name == "CodeModule")
				{
					CodeModule cm = new CodeModule(r, this, xNodeLoop);
					_Items.Add(cm);
				}
				else
				{
					// don't know this element - log it
					OwnerReport.rl.LogError(4, "Unknown CodeModules element '" + xNodeLoop.Name + "' ignored.");
				}
			}
			if (_Items.Count == 0)
				OwnerReport.rl.LogError(8, "For CodeModules at least one CodeModule is required.");
		}
		/// <summary>
		/// Return the Type given a class name.  Searches the CodeModules that are specified
		/// in the report.
		/// </summary>
		internal Type this[string s]
		{
			get 
			{
				Type tp=null;	  
				try
				{
					// loop thru all the codemodules looking for the assembly
					//  that contains this type
					foreach (CodeModule cm in _Items)
					{
						if (cm.LoadedAssembly != null)
						{
							tp = cm.LoadedAssembly.GetType(s);
							if (tp != null)
								break;
						}
					}
				}
				catch {}
				return tp;
			}
		}
		
		override internal void FinalPass()
		{
			foreach (CodeModule cm in _Items)
			{
				cm.FinalPass();
			}
			return;
		}

		internal void LoadModules()
		{
			foreach (CodeModule cm in _Items)
			{
				cm.LoadModule();
			}
		}

		internal ArrayList Items
		{
			get { return  _Items; }
		}
		
		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return _Items.GetEnumerator();
		}

		#endregion
	}
}
