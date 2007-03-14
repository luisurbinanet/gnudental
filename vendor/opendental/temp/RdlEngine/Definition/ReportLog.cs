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
	/// Error logging (parse and runtime) within report.
	///</summary>
	[Serializable]
	internal class ReportLog
	{
		ArrayList _ErrorItems;			// list of report items
		int _MaxSeverity=0;				// maximum severity encountered				

		internal ReportLog()
		{
			_ErrorItems = null;
		}

		internal void LogError(int severity, string item)
		{
			if (_ErrorItems == null)			// create log if first time
				_ErrorItems = new ArrayList();

			if (severity > _MaxSeverity)
				_MaxSeverity = severity;

			string msg = "Severity: " + Convert.ToString(severity) + " - " + item;

			_ErrorItems.Add(msg);

			if (severity >= 12)		
				throw new Exception(msg);		// terminate the processing

			return;
		}

		internal void LogError(int severity, IList list)
		{
			if (_ErrorItems == null)			// create log if first time
				_ErrorItems = new ArrayList();

			if (severity > _MaxSeverity)
				_MaxSeverity = severity;

			_ErrorItems.AddRange(list);

			return;
		}

		internal void Reset()
		{
			_ErrorItems=null;
			if (_MaxSeverity < 8)				// we keep the severity to indicate we can't run report
				_MaxSeverity=0;
		}

		internal ArrayList ErrorItems
		{
			get { return  _ErrorItems; }
		}


		internal int MaxSeverity
		{
			get { return  _MaxSeverity; }
			set {  _MaxSeverity = value; }
		}
	}
}
