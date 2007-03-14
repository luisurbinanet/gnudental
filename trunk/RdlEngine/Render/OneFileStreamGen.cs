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

namespace fyiReporting.RDL
{
	
	///<summary>
	/// An implementation of IStreamGen.  Constructor is passed the name of a
	/// file.  The first file uses that name.  If subsequant files are needed 
	/// then a number suffix is generated sequentially.	 e.g. afile.html, afile2.gif,
	/// afile3.jpeg, ...
	///</summary>

	public class OneFileStreamGen : IStreamGen, IDisposable
	{
		string _Directory;
		StreamWriter _SW;
		Stream _io;
		string _FileName;
		ArrayList _FileList;
		int _nextFileNumber=1;
		bool _Overwrite;

		public OneFileStreamGen(string filename, bool bOverwrite)
		{
			_Overwrite = bOverwrite;
			string ext = Path.GetExtension(filename).Substring(1);	// extension (without the '.')
			_Directory = Path.GetDirectoryName(filename);
			_FileName = Path.GetFileNameWithoutExtension(filename);

			_FileList = new ArrayList();

			string relativeName;
			_io = GetIOStream(out relativeName, ext);
		}

		internal ArrayList FileList
		{
			get { return _FileList; }
		}
		
		internal string FileName
		{
			get { return _FileName; }
		}

		#region IStreamGen Members
		public void CloseMainStream()
		{
			if (_SW != null)
			{
				_SW.Close();
				_SW = null;
			}
			if (_io != null)
			{
				_io.Close();
				_io = null;
			}
			return;
		}

		public Stream GetStream()
		{
			return this._io;
		}

		public TextWriter GetTextWriter()
		{
			if (_SW == null)
				_SW = new StreamWriter(_io);
			return _SW;
		}

		// create a new file in the directory specified and return
		//   a Stream caller can then write to.   relativeName is filled in with
		//   name we generate (sans the directory).
		public Stream GetIOStream(out string relativeName, string extension)
		{
			Stream io=null;

			// Obtain a new file name
			string filename = string.Format("{0}{1}{2}{3}.{4}",
				_Directory,						// directory
				Path.DirectorySeparatorChar,	// "\"
				_FileName,						// filename
				(this._nextFileNumber > 1? _nextFileNumber.ToString(): ""),		// suffix: first file doesn't need number suffix
				extension);						// extension
			_nextFileNumber++;			// increment to next file

			FileInfo fi = new FileInfo(filename);
			if (fi.Exists)
			{
				if (_Overwrite)
					fi.Delete();
				else
					throw new Exception(string.Format("File {0} already exists.", filename));
			}

			relativeName = Path.GetFileName(filename);
			io = fi.Create();
			_FileList.Add(filename);
			return io; 
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			if (_SW != null)
			{
				_SW.Flush();
				_SW.Close();
				_SW = null;
			}
			if (_io != null)
			{
				_io.Flush();
				_io.Close();
				_io = null;
			}
		}

		#endregion
	}
}
