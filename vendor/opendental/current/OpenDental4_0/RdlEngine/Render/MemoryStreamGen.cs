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
using fyiReporting.RDL;
using System.IO;
using System.Collections;
using System.Text;

namespace fyiReporting.RDL
{
	
	///<summary>
	/// An implementation of IStreamGen.  Used for single file with memory stream.
	/// XML and PDF are the only types that will work with this implementation.
	///</summary>

	public class MemoryStreamGen : IStreamGen, IDisposable
	{
		MemoryStream _io;
		StreamWriter _sw=null;
		public MemoryStreamGen()
		{
			_io = new MemoryStream();
		}

		public string GetText()
		{
			_sw.Flush();
			StreamReader sr = null; 
			string t=null;
			try
			{
				_io.Position = 0;
				sr = new StreamReader(_io);
				t = sr.ReadToEnd();
			}
			finally
			{
				sr.Close();
			}
			return t;
		}

		#region IStreamGen Members
		public void CloseMainStream()
		{
			_io.Close();
			return;
		}

		public Stream GetStream()
		{
			return this._io;
		}

		public TextWriter GetTextWriter()
		{
			if (_sw == null)
				_sw = new StreamWriter(_io);
			return _sw;
		}

		// create a new file in the directory specified and return
		//   a Stream caller can then write to.   relativeName is filled in with
		//   name we generate (sans the directory).
		public Stream GetIOStream(out string relativeName, string extension)
		{
			throw new ArgumentException("GetIOStream not supported");
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			if (_sw != null)
			{
				_sw.Close();
				_sw = null;
			}
			if (_io != null)
			{
				_io.Close();
				_io = null;
			}
		}

		#endregion
	}
}
