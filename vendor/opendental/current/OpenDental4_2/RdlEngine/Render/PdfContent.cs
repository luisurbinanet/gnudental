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
	/// <summary>
	///Represents the general content stream in a Pdf Page. 
	///This is used only by the PageObjec 
	/// </summary>
	internal class PdfContent:PdfBase
	{
		private string content;
		private string contentStream;
		internal PdfContent(PdfAnchor pa):base(pa)
		{
			content=null;
			contentStream="%stream\r";
		}
		/// <summary>
		/// Set the Stream of this Content Dict.
		/// Stream is taken from PdfElements Objects
		/// </summary>
		/// <param name="stream"></param>
		internal void SetStream(string stream)
		{
			if (stream == null)
				return;
			contentStream+=stream;
		}
		/// <summary>
		/// Enter the text inside the table just created.
		/// </summary>
		/// <summary>
		/// Get the Content Dictionary
		/// </summary>
		internal byte[] GetContentDict(long filePos,out int size)
		{
			content=string.Format("\r\n{0} 0 obj<</Length {1}>>stream\r{2}\rendstream\rendobj\r",
				this.objectNum,contentStream.Length,contentStream);

			return GetUTF8Bytes(content,filePos,out size);
		}
	}

}
