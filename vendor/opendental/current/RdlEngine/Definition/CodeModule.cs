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
	/// CodeModule definition and processing.
	///</summary>
	[Serializable]
	internal class CodeModule : ReportLink
	{
		string _CodeModule;	// Name of the code module to load
		[NonSerialized] Assembly _LoadedAssembly=null;	// 
		[NonSerialized] bool bLoadFailed=false;
	
		internal CodeModule(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_CodeModule=xNode.InnerText;
		}

		internal Assembly LoadedAssembly
		{
			get 
			{
				if (_LoadedAssembly == null)
					LoadModule();
				return _LoadedAssembly;
			}
		}

		override internal void FinalPass()
		{
			return;
		}

		internal void LoadModule()
		{
			if (bLoadFailed)		// We only try to load once.
				return;

			if (_LoadedAssembly == null)
			{
				try
				{
					_LoadedAssembly = Assembly.LoadFrom(_CodeModule);
				}
				catch (Exception e)
				{
					OwnerReport.rl.LogError(4, String.Format("CodeModule {0} failed to load.  {1}",
														_CodeModule, e.Message));
					_LoadedAssembly = null;
					bLoadFailed = true;
				}
			}
		}

		internal string CdModule
		{
			get { return  _CodeModule; }
			set {  _CodeModule = value; }
		}
	}
}
