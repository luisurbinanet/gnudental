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
using System.IO;
using System.Collections;
using System.Text;
using System.Drawing;


namespace fyiReporting.RdlDesign
{
	/// <summary>
	/// MatrixView:  builds a simplfied representation of the matrix so that it
	///  can be drawn or hit test in a simplified fashion.
	/// </summary>
	internal class MatrixView
	{
		DesignXmlDraw _Draw;
		XmlNode _MatrixNode;
		int _Rows;
		int _HeaderRows;
		int _Columns;
		int _HeaderColumns;
		float _Height;
		float _Width;
		MatrixItem[,] _MatrixView;

		internal MatrixView(DesignXmlDraw dxDraw, XmlNode matrix)
		{
			_Draw = dxDraw;
			_MatrixNode = matrix;
			BuildView();
		}

		internal MatrixItem this[int row, int column] 
		{
			get {return _MatrixView[row, column]; }
		}

		internal int Columns
		{
			get {return _Columns;}
		}

		internal int Rows
		{
			get {return _Rows;}
		}

		internal int HeaderColumns
		{
			get {return _HeaderColumns;}
		}

		internal int HeaderRows
		{
			get {return _HeaderRows;}
		}

		internal float Height
		{
			get {return _Height;}
		}

		internal float Width
		{
			get {return _Width;}
		}

		void BuildView()
		{
			CountRowColumns();		// get the total count of rows and columns
			_MatrixView = new MatrixItem[_Rows, _Columns];	// allocate the 2-dimensional array
			FillMatrix();
		}

		void CountRowColumns()
		{
			int cols=0;
			int rows=0;
			XmlNode cGroupings = _Draw.GetNamedChildNode(_MatrixNode, "ColumnGroupings");
			if (cGroupings != null)
			{
				cols++;			// add a column for the ColumnGroupings
				foreach (XmlNode c in cGroupings.ChildNodes)
				{
					if (c.Name != "ColumnGrouping")
						continue;
					XmlNode subtotal = DesignXmlDraw.FindNextInHierarchy(c, "DynamicColumns", "Subtotal");
					if (subtotal != null)
						cols++;
					rows++;		// add a row per ColumnGrouping
				}
			}
			_HeaderRows = rows;
			_HeaderColumns = 0;
			XmlNode rGroupings = _Draw.GetNamedChildNode(_MatrixNode, "RowGroupings");
			if (rGroupings != null)
			{
				rows++;			// add a row for the rowgroupings
				foreach (XmlNode c in rGroupings.ChildNodes)
				{
					if (c.Name != "RowGrouping")
						continue;
					XmlNode subtotal = DesignXmlDraw.FindNextInHierarchy(c, "DynamicRows", "Subtotal");
					if (subtotal != null)
						rows++;	// add a row for the subtotal
					cols++;		// add a column per RowGrouping
					_HeaderColumns++;
				}
			}
			_Rows = rows;
			_Columns = cols;
		}

		void FillMatrix()
		{
			int rows=0;
			int cols=0;
			MatrixItem mi;
			XmlNode cGroupings = _Draw.GetNamedChildNode(_MatrixNode, "ColumnGroupings");
			if (cGroupings != null)
			{
				float width = _Draw.GetSize(
					DesignXmlDraw.FindNextInHierarchy(_MatrixNode, "MatrixColumns", "MatrixColumn"),
					"Width");
				int subtotalCols=0;
				foreach (XmlNode c in cGroupings.ChildNodes)
				{
					if (c.Name != "ColumnGrouping")
						continue;
					XmlNode ris = DesignXmlDraw.FindNextInHierarchy(c, "DynamicColumns", "ReportItems");
					mi = new MatrixItem(ris);
					mi.Height = _Draw.GetSize(c, "Height");
					mi.Width = width;
					_MatrixView[rows, _HeaderColumns] = mi;

					XmlNode subtotal = DesignXmlDraw.FindNextInHierarchy(c, "DynamicColumns", "Subtotal");
					if (subtotal != null)
					{
						ris = DesignXmlDraw.FindNextInHierarchy(subtotal, "ReportItems");
						mi = new MatrixItem(ris);
						mi.Height = _Draw.GetSize(c, "Height");
						mi.Width = width;
						subtotalCols++;
						_MatrixView[rows, _HeaderColumns+subtotalCols] = mi;
					}
					rows++;		// add a row per ColumnGrouping
				}
			}
			XmlNode rGroupings = _Draw.GetNamedChildNode(_MatrixNode, "RowGroupings");
			if (rGroupings != null)
			{
				float height = _Draw.GetSize(
					DesignXmlDraw.FindNextInHierarchy(_MatrixNode, "MatrixRows", "MatrixRow"),
					"Height");
				cols = 0;
				int subtotalrows=0;
				foreach (XmlNode c in rGroupings.ChildNodes)
				{
					if (c.Name != "RowGrouping")
						continue;
					XmlNode ris = DesignXmlDraw.FindNextInHierarchy(c, "DynamicRows", "ReportItems");
					mi = new MatrixItem(ris);
					mi.Width = _Draw.GetSize(c, "Width");
					mi.Height = height;
					_MatrixView[_HeaderRows, cols] = mi;

					XmlNode subtotal = DesignXmlDraw.FindNextInHierarchy(c, "DynamicRows", "Subtotal");
					if (subtotal != null)
					{
						subtotalrows++;	// add a row for the subtotal
						ris = DesignXmlDraw.FindNextInHierarchy(subtotal, "ReportItems");
						mi = new MatrixItem(ris);
						mi.Width = _Draw.GetSize(c, "Width");
						mi.Height = height;
						_MatrixView[_HeaderRows+subtotalrows, cols] = mi;
					}
					cols++;		// add a column per RowGrouping
				}
			}
			XmlNode corner = _Draw.GetNamedChildNode(_MatrixNode, "Corner");
			if (corner != null)
			{
				XmlNode ris = DesignXmlDraw.FindNextInHierarchy(corner, "ReportItems");
				mi = new MatrixItem(ris);
				_MatrixView[0,0] = mi;
			}
			// obtain the matrix cell
			XmlNode mcell = DesignXmlDraw.FindNextInHierarchy(_MatrixNode, 
										"MatrixRows", "MatrixRow", "MatrixCells", "MatrixCell", "ReportItems");
			
			// now fill out the rest of the matrix with empty entries
			for (int row=0; row < this.Rows; row++)
			{
				for (int col= 0; col < this.Columns; col++)
				{
					if (_MatrixView[row, col] == null)
					{
						XmlNode n=null;
						if (row >= _HeaderRows && col >= _HeaderColumns)
							n = mcell;		// this is filled by the matrixcell item

						mi = new MatrixItem(n);
						_MatrixView[row, col] = mi;
					}
				}
			}
			// fill out the heights for each row
			this._Height = 0;
			for (int row=0; row < this.Rows; row++)
			{
				float height=0;
				for (int col= 0; col < this.Columns; col++)
				{
					mi = _MatrixView[row,col];
					height = Math.Max(height, mi.Height);
				}
				for (int col= 0; col < this.Columns; col++)
					_MatrixView[row,col].Height = height;

				this._Height += height;
			}

			// fill out the widths for each column
			this._Width = 0;
			for (int col=0; col < this.Columns; col++)
			{
				float width=0;
				for (int row= 0; row < this.Rows; row++)
				{
					mi = _MatrixView[row,col];
					width = Math.Max(width, mi.Width);
				}
				for (int row= 0; row < this.Rows; row++)
					_MatrixView[row,col].Width = width;

				this._Width += width;
			}

			if (this.Columns == 0 || this.Rows == 0)
				return;

			// set the height and width for the corner
			mi = _MatrixView[0,0];
			mi.Height = 0;
			for (int row=0; row < this._HeaderRows; row++)
				mi.Height += _MatrixView[row, 1].Height;
			mi.Width = 0;
			for (int col=0; col < this._HeaderColumns; col++)
				mi.Width += _MatrixView[1, col].Width;


		}
	}

	class MatrixItem
	{
		XmlNode _ReportItem;
		float _Width;
		float _Height;

		internal MatrixItem(XmlNode ri)
		{
			_ReportItem = ri;
		}

		internal XmlNode ReportItem
		{
			get {return _ReportItem;}
			set {_ReportItem = value;}
		}

		internal float Width
		{
			get {return _Width;}
			set {_Width = value;}
		}

		internal float Height
		{
			get {return _Height;}
			set {_Height = value;}
		}
	}
}
