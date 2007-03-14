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

namespace fyiReporting.RDL
{
	///<summary>
	/// Handle sort direction enumeration: ascending, descending.
	///</summary>
	internal enum SortDirectionEnum
	{
		Ascending,
		Descending
	}
	internal class SortDirection
	{
		static internal SortDirectionEnum GetStyle(string s, ReportLog rl)
		{
			SortDirectionEnum rs;

			switch (s)
			{		
				case "Ascending":
					rs = SortDirectionEnum.Ascending;
					break;
				case "Descending":
					rs = SortDirectionEnum.Descending;
					break;
				default:		
					rl.LogError(4, "Unknown SortDirection '" + s + "'.  Ascending assumed.");
					rs = SortDirectionEnum.Ascending;
					break;
			}
			return rs;
		}
	}

}
