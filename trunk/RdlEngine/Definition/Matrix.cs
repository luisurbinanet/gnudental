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
using System.Collections;
using System.Globalization;

namespace fyiReporting.RDL
{
	///<summary>
	///  Represents the report item (and Data region) for a matrix (cross-tabulation)
	///</summary>
	[Serializable]
	internal class Matrix : DataRegion
	{
		Corner _Corner;		// The region that contains the elements of
							// the upper left corner area of the matrix.
							// If omitted, no report items are output in
							// the corner.
		ColumnGroupings _ColumnGroupings;	// The set of column groupings for the matrix
		RowGroupings _RowGroupings;	// The set of row groupings for the matrix
		MatrixRows _MatrixRows;		// The rows contained in each detail cell
									// of the matrix layout
		MatrixColumns _MatrixColumns;	// The columns contained in each detail
									// cell of the matrix layout
		MatrixLayoutDirectionEnum _LayoutDirection;	// Indicates whether the matrix columns
									// grow left-to-right (with headers on the
									// left) or right-to-left (with headers on the
									// right).
		int _GroupsBeforeRowHeaders;	// The number of instances of the
									// outermost column group that should
									// appear to the left of the row headers
									// (right of the row headers for RTL
									// matrixes). Default is 0.
		string _CellDataElementName;	// The name to use for the cell element. Default: “Cell”
		MatrixCellDataElementOutputEnum _CellDataElementOutput; // Indicates whether the cell contents
									//should appear in a data rendering.  Default is Output.

		// Runtime
		[NonSerialized] Rows _Data;					// data for a cell in an array
		[NonSerialized] ColumnGrouping _lastCg;		// last column group
		[NonSerialized] RowGrouping _lastRg;		// last row group

		internal Matrix(Report r, ReportLink p, XmlNode xNode):base(r,p,xNode)
		{
			_Corner=null;
			_ColumnGroupings=null;
			_RowGroupings=null;
			_MatrixRows=null;
			_MatrixColumns=null;
			_LayoutDirection=MatrixLayoutDirectionEnum.LTR;
			_GroupsBeforeRowHeaders=0;
			_CellDataElementName=null;
			_CellDataElementOutput=MatrixCellDataElementOutputEnum.Output;
			_Data =null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "Corner":
						_Corner = new Corner(r, this, xNodeLoop);
						break;
					case "ColumnGroupings":
						_ColumnGroupings = new ColumnGroupings(r, this, xNodeLoop);
						break;
					case "RowGroupings":
						_RowGroupings = new RowGroupings(r, this, xNodeLoop);
						break;
					case "MatrixRows":
						_MatrixRows = new MatrixRows(r, this, xNodeLoop);
						break;
					case "MatrixColumns":
						_MatrixColumns = new MatrixColumns(r, this, xNodeLoop);
						break;
					case "LayoutDirection":
						_LayoutDirection = MatrixLayoutDirection.GetStyle(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					case "GroupsBeforeRowHeaders":
						_GroupsBeforeRowHeaders = XmlUtil.Integer(xNodeLoop.InnerText);
						break;
					case "CellDataElementName":
						_CellDataElementName = xNodeLoop.InnerText;
						break;
					case "CellDataElementOutput":
						_CellDataElementOutput = MatrixCellDataElementOutput.GetStyle(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					default:
						if (DataRegionElement(xNodeLoop))	// try at DataRegion level
							break;
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown Matrix element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
			DataRegionFinish();			// Tidy up the DataRegion

			if (_ColumnGroupings == null)
				OwnerReport.rl.LogError(8, "Matrix element ColumnGroupings not specified for " + (this.Name == null? "'name not specified'": this.Name.Nm));
			if (_RowGroupings == null)
				OwnerReport.rl.LogError(8, "Matrix element RowGroupings not specified for " + (this.Name == null? "'name not specified'": this.Name.Nm));
			if (_MatrixRows == null)
				OwnerReport.rl.LogError(8, "Matrix element MatrixRows not specified for " + (this.Name == null? "'name not specified'": this.Name.Nm));
			if (_MatrixColumns == null)
 				OwnerReport.rl.LogError(8, "Matrix element MatrixColumns not specified for " + (this.Name == null? "'name not specified'": this.Name.Nm));

		}

		override internal void FinalPass()
		{
			base.FinalPass();

			float totalHeight=0;
			if (_Corner != null)
				_Corner.FinalPass();
			if (_ColumnGroupings != null)
			{
				_ColumnGroupings.FinalPass();
				totalHeight += _ColumnGroupings.DefnHeight();
			}
			if (_RowGroupings != null)
				_RowGroupings.FinalPass();
			if (_MatrixRows != null)
			{
				_MatrixRows.FinalPass();
				totalHeight += _MatrixRows.DefnHeight();
			}
			if (_MatrixColumns != null)
				_MatrixColumns.FinalPass();

			if (this.Height == null)
			{	// Calculate a height based on the sum of the TableRows
				this.Height = new RSize(this.OwnerReport, string.Format(NumberFormatInfo.InvariantInfo, "{0:0.00}pt", totalHeight));
			}

			return;
		}

		override internal void Run(IPresent ip, Row row)
		{
			RunReset();
			_Data = GetFilteredData(row);

			if (!AnyRows(ip, _Data))		// if no rows return
				return;					//   nothing left to do

			int maxColumns;
			int maxRows;
			MatrixCellEntry[,] matrix = RunBuild(out maxRows, out maxColumns);

			// Now run thru the rows and columns of the matrix passing the information
			//   on to the rendering engine
			if (!ip.MatrixStart(this, row))
				return;
			for (int iRow = 0; iRow < maxRows; iRow++)
			{
				ip.MatrixRowStart(this, iRow, row);
				for (int iColumn = 0; iColumn < maxColumns; iColumn++)
				{
					MatrixCellEntry mce = matrix[iRow, iColumn];
					if (mce == null)
					{
						ip.MatrixCellStart(this, null, iRow, iColumn, row);
						ip.MatrixCellEnd(this, null, iRow, iColumn, row);
					}
					else
					{
						this._Data = mce.Data;		// Must set this for evaluation
						Row lrow = this._Data.Data.Count > 0? (Row) (this._Data.Data[0]):null;
						mce.DisplayItem.MC = mce;	// set for use by the display item

						ip.MatrixCellStart(this, mce.DisplayItem, iRow, iColumn, lrow);

						mce.DisplayItem.Run(ip, lrow);
						ip.MatrixCellEnd(this, mce.DisplayItem, iRow, iColumn, lrow);
					}
				}
				ip.MatrixRowEnd(this, iRow, row);
			}
			ip.MatrixEnd(this, row);
		}

		override internal void RunPage(Pages pgs, Row row)
		{
			if (IsHidden(row))
				return;

			RunReset();

			_Data = GetFilteredData(row);

			SetPagePositionBegin(pgs);

			if (!AnyRowsPage(pgs, _Data))		// if no rows return
				return;						//   nothing left to do

			int maxColumns;
			int maxRows;
			int headerRows = _ColumnGroupings.Items.Count;	// number of column headers we have
			MatrixCellEntry[,] matrix = RunBuild(out maxRows, out maxColumns);

			// Now run thru the rows and columns of the matrix creating the pages
			RunPageRegionBegin(pgs);
			Page p = pgs.CurrentPage;
			p.YOffset += this.RelativeY;

			for (int iRow = 0; iRow < maxRows; iRow++)
			{
				float h = HeightOfRow(pgs, matrix, iRow);
				if (h <= 0)		// there were no cells in row
					continue;	//     skip the row

				if (p.YOffset + h > pgs.BottomOfPage)
				{
					p = RunPageNew(pgs, p);
					// run thru the headers again
					for (int aRow = 0; aRow < headerRows; aRow++)
					{	
						RunPageColumns(pgs, matrix, aRow, maxColumns);
						p.YOffset += HeightOfRow(pgs, matrix, aRow);
					}
				}
				RunPageColumns(pgs, matrix, iRow, maxColumns);
				p.YOffset += h;
			}

			RunPageRegionEnd(pgs);
			SetPagePositionEnd(pgs, pgs.CurrentPage.YOffset);
		}

		internal void RunReset()
		{
			_Data=null;
			_lastCg=null;
			_lastRg=null;
		}

		float HeightOfRow(Pages pgs, MatrixCellEntry[,] matrix, int iRow)
		{
			int maxColumns = matrix.GetLength(1);
			float height=0;
			bool bResetAllHeights=false;
			for (int iCol=0; iCol < maxColumns; iCol++)
			{
				MatrixCellEntry mce = matrix[iRow, iCol];
				if (mce == null)
					continue;
				if (mce.DisplayItem is Textbox)
				{
					Textbox tb = mce.DisplayItem as Textbox;
					if (tb.CanGrow)
					{
						this._Data = mce.Data;		// Must set this for evaluation
						Row lrow = this._Data.Data.Count > 0? (Row) (this._Data.Data[0]):null;
						mce.DisplayItem.MC = mce;	// set for use by the display item
						float tbh = tb.RunTextCalcHeight(pgs.G, lrow);
						if (height < tbh)
						{
							bResetAllHeights = true;
							height = tbh;
						}
					}
				}

				if (height < mce.Height)
					height = mce.Height;
			}

			if (bResetAllHeights)	// If any text forces the row to grow; all heights must be fixed
			{
				for (int iCol=0; iCol < maxColumns; iCol++)
				{
					MatrixCellEntry mce = matrix[iRow, iCol];
					if (mce != null)
						mce.Height = height;
				}
			}
			return height;
		}

		float WidthOfColumn(MatrixCellEntry[,] matrix, int iCol)
		{
			int maxRows = matrix.GetLength(0);
			for (int iRow=0; iRow < maxRows; iRow++)
			{
				if (matrix[iRow, iCol] != null)
					return matrix[iRow, iCol].Width;
			}
			return 0;
		}

		void RunPageColumns(Pages pgs, MatrixCellEntry[,] matrix, int iRow, int maxColumns)
		{
			float xpos = OffsetCalc + LeftCalc;
			for (int iColumn = 0; iColumn < maxColumns; iColumn++)
			{
				MatrixCellEntry mce = matrix[iRow, iColumn];
					
				if (mce == null)
				{	// have a null column but we need to fill column space
					xpos += WidthOfColumn(matrix, iColumn);
					continue;
				}
				this._Data = mce.Data;		// Must set this for evaluation
				Row lrow = this._Data.Data.Count > 0? (Row) (this._Data.Data[0]):null;
				mce.DisplayItem.MC = mce;	// set for use by the display item
				mce.XPosition = xpos;
				mce.DisplayItem.RunPage(pgs, lrow);
				xpos += mce.Width;
			}
		}

		// RunBuild is used by both Matrix.Run and Chart.Run to obtain the necessary data
		//   used by their respective rendering interfaces
		internal MatrixCellEntry[,] RunBuild(out int numRows, out int numCols)
		{
			// loop thru all the data;
			//    form bitmap arrays for each unique data value of each grouping (row and column) value
			int maxColumns = _RowGroupings.Items.Count;	// maximum # of columns in matrix
									// at top we need a row per column grouping
			_lastCg = (ColumnGrouping) (_ColumnGroupings.Items[_ColumnGroupings.Items.Count-1]);
			int maxRows = _ColumnGroupings.Items.Count;	// maximum # of rows in matrix
									// at left we need a column per row grouping
			_lastRg = (RowGrouping) (_RowGroupings.Items[_RowGroupings.Items.Count-1]);

			string nullterminal = '\ufffe'.ToString();
			string terminal = '\uffff'.ToString();

			_ColumnGroupings.ME = new MatrixEntry(null, _Data.Data.Count);
			_ColumnGroupings.ME.FirstRow=0;
			_ColumnGroupings.ME.LastRow=_Data.Data.Count-1;
			_ColumnGroupings.ME.Rows = new BitArray(_Data.Data.Count, true);	// all data

			_RowGroupings.ME = new MatrixEntry(null, _Data.Data.Count);
			_RowGroupings.ME.FirstRow=0;
			_RowGroupings.ME.LastRow=_Data.Data.Count-1;
			_RowGroupings.ME.Rows = new BitArray(_Data.Data.Count, true);		// all data

			MatrixEntry m;
			int iRow=0;				// row counter
			foreach (Row r in _Data.Data)
			{
				// Handle the column values
				m = _ColumnGroupings.ME;
				foreach (ColumnGrouping cg in _ColumnGroupings.Items)
				{
					Grouping grp=null;
					if (cg.DynamicColumns != null)
						grp = cg.DynamicColumns.Grouping;
					else
						throw new Exception("Only Dynamic columns currently supported");

					string result="";
					foreach (GroupExpression ge in grp.GroupExpressions.Items)
					{
						string temp = ge.Expression.EvaluateString(r);
						if (temp == null || temp == "")
							result += nullterminal;
						else
							result += temp;
						result += terminal;		// mark end of group 
					}

					MatrixEntry ame = (MatrixEntry) (m.HashData[result]);
					if (ame == null)
					{
						ame = new MatrixEntry(m, _Data.Data.Count);
						ame.ColumnGroup = cg;
						m.HashData.Add(result, ame);
						if (cg == _lastCg)		// Add a column when we add data at lowest level
							maxColumns++;
					}
					ame.Rows.Set(iRow, true);
					// Logic in FirstRow and Last row determine whether value gets set
					ame.FirstRow = iRow;
					ame.LastRow = iRow;
					m = ame;			// now go down a level
				}

				// Handle the row values
				m = _RowGroupings.ME;
				foreach (RowGrouping rg in _RowGroupings.Items)
				{
					Grouping grp=null;
					if (rg.DynamicRows != null)
						grp = rg.DynamicRows.Grouping;
					else
						throw new Exception("only Dynamic rows currently supported");  //TODO

					string result="";
					foreach (GroupExpression ge in grp.GroupExpressions.Items)
					{
						string temp = ge.Expression.EvaluateString(r);
						if (temp == null || temp == "")
							result += terminal;
						else
							result += temp;
						result += terminal;		// mark end of group 
					}
					MatrixEntry ame = (MatrixEntry) (m.HashData[result]);
					if (ame == null)
					{
						ame = new MatrixEntry(m, _Data.Data.Count);
						ame.RowGroup = rg;
						m.HashData.Add(result, ame);
						if (rg == _lastRg)
							maxRows++;		// Add a row when we add data at lowest level
					}
					ame.Rows.Set(iRow, true);
					// Logic in FirstRow and Last row determine whether value gets set
					ame.FirstRow = iRow;
					ame.LastRow = iRow;
					m = ame;			// now go down a level
				}

				iRow++;
			}

			// Determine how many subtotal columns are needed
			maxColumns += RunCountSubtotalColumns(_ColumnGroupings.ME);

			// Determine how many subtotal rows are needed
			maxRows += RunCountSubtotalRows(_RowGroupings.ME);

			/////
			// Build and populate the 2 dimensional table of MatrixCellEntry
			//    that constitute the matrix
			/////
			MatrixCellEntry[,] matrix = new MatrixCellEntry[maxRows, maxColumns];
	
			// Do the corner
			matrix[0, 0] = RunCorner(_Data);

			// Do the column headings
			int iColumn = _RowGroupings.Items.Count;
			RunColumnHeaders(_ColumnGroupings.ME, matrix, _Data, 0, ref iColumn);

			// Do the row headings
			iRow = _ColumnGroupings.Items.Count;
			RunRowHeaders(_RowGroupings.ME, matrix, _Data, ref iRow, 0);

			// Do the row/column data
			iRow = _ColumnGroupings.Items.Count;		
			RunDataRow(_RowGroupings.ME, _ColumnGroupings.ME, matrix, _Data, ref iRow, _RowGroupings.Items.Count);

			// now return the matrix data
			numRows = maxRows;
			numCols = maxColumns;
			return matrix;
		}
		
		int RunCountSubtotalColumns(MatrixEntry m)
		{
			int count = 0;
			// When we're on the first level (m.ColumnGroup == null)
			//   then we'll increase the column count by 1 to account for the lowest level
			if (m.ColumnGroup == null && _lastCg.DynamicColumns.Subtotal != null)
				count = 1;

			if (m.SortedData == null)
				return count;

			MatrixEntry firstChild=null;
			// We need to look at the first child to see the real type
			foreach (MatrixEntry ame in m.SortedData.Values)
			{
				firstChild = ame;
				break;
			}
			if (firstChild == null || firstChild.ColumnGroup == _lastCg)
				return count;

			bool bSubtotal = firstChild.ColumnGroup.DynamicColumns.Subtotal == null? false: true; 

			count += m.SortedData.Count;		// we'll need a subtotal for each of these groups

			// Now dive into the data      Note - if this is the second to last group we really don't
			//                                       don't need to dive in since the last groups returns 0
			foreach (MatrixEntry ame in m.SortedData.Values)
			{
				count += RunCountSubtotalColumns(ame);
			}

			return count;
		}
		
		int RunCountSubtotalRows(MatrixEntry m)
		{
			int count = 0;
			// When we're on the first level (m.RowGroup == null)
			//   then we'll increase the row count by 1 to account for the lowest level
			if (m.RowGroup == null && _lastRg.DynamicRows.Subtotal != null)
				count = 1;

			if (m.SortedData == null)
				return count;

			MatrixEntry firstChild=null;
			// We need to look at the first child to see the real type
			foreach (MatrixEntry ame in m.SortedData.Values)
			{
				firstChild = ame;
				break;
			}
			if (firstChild == null || firstChild.RowGroup == _lastRg)
				return count;

			bool bSubtotal = firstChild.RowGroup.DynamicRows.Subtotal == null? false: true; 

			count += m.SortedData.Count;		// we'll need a subtotal for each of these groups

			// Now dive into the data      Note - if this is the second to last group we really
			//                                       don't need to dive in since the last groups returns 0
			foreach (MatrixEntry ame in m.SortedData.Values)
			{
				count += RunCountSubtotalRows(ame);
			}

			return count;
		}
		
		void RunColumnHeaders(MatrixEntry m, MatrixCellEntry[,] matrix, Rows _Data, int iRow, ref int iColumn)
		{
			float width;
			if (this.MatrixColumns != null)
			{
				MatrixColumn mc = (MatrixColumn) (this.MatrixColumns.Items[0]);	// get width from the matrix column
				width = mc.Width.Points;
			}
			else
				width = 0;		// We use this routine for chart(s) and they don't build the matrix columns

			foreach (MatrixEntry ame in m.SortedData.Values)
			{
				matrix[iRow, iColumn] = RunGetColumnHeader(ame, _Data);
				matrix[iRow, iColumn].Width = width;
				matrix[iRow, iColumn].Height = ame.ColumnGroup.Height == null? 0: ame.ColumnGroup.Height.Points;
				if (ame.SortedData != null)
				{
					RunColumnHeaders(ame, matrix, _Data, iRow+1, ref iColumn);
					if (ame.ColumnGroup.DynamicColumns.Subtotal != null)
					{
						ReportItem ri = (ReportItem) (ame.ColumnGroup.DynamicColumns.Subtotal.ReportItems.Items[0]);	
						Rows subData = new Rows(_Data, ame.FirstRow, ame.LastRow, ame.Rows);
						matrix[iRow, iColumn] = new MatrixCellEntry(subData, ri); 
						matrix[iRow, iColumn].Height = ame.ColumnGroup.Height.Points;
						matrix[iRow, iColumn].Width = width;
						iColumn++;
					}
				}
				else
					iColumn++;
			}
			// if top level and we need subtotal on the whole group
			if (m.ColumnGroup == null && _lastCg.DynamicColumns.Subtotal != null)
			{
				ReportItem ri = (ReportItem) (_lastCg.DynamicColumns.Subtotal.ReportItems.Items[0]);	
				matrix[iRow, iColumn] = new MatrixCellEntry(_Data, ri);
				matrix[iRow, iColumn].Height = _lastCg.Height.Points;
				matrix[iRow, iColumn].Width = width;
				iColumn++;
			}
		}

		MatrixCellEntry RunGetColumnHeader(MatrixEntry me, Rows _Data)
		{
			ReportItem ri = (ReportItem) (me.ColumnGroup.DynamicColumns.ReportItems.Items[0]);	
			Rows subData = new Rows(_Data, me.FirstRow, me.LastRow, me.Rows);
			MatrixCellEntry mce = new MatrixCellEntry(subData, ri);

			return mce;
		}

		MatrixCellEntry RunCorner(Rows d)
		{
			if (_Corner == null)
				return null;

			ReportItem ri = (ReportItem) (_Corner.ReportItems.Items[0]);	
			MatrixCellEntry mce = new MatrixCellEntry(d, ri);

			ColumnGrouping cg = this.ColumnGroupings.Items[0] as ColumnGrouping;
			RowGrouping rg = this.RowGroupings.Items[0] as RowGrouping;
			mce.Height = cg.Height.Points;
			mce.Width = rg.Width.Points;

			return mce;
		}

		void RunDataColumn(MatrixEntry rm, MatrixEntry cm, MatrixCellEntry[,] matrix, Rows _Data, int iRow, ref int iColumn)
		{
			BitArray andData;
			float width;
			if (this.MatrixColumns != null)
			{
				MatrixColumn mc = this.MatrixColumns.Items[0] as MatrixColumn;	// assumes dynamic! TODO
				width = mc.Width.Points;
			}
			else
				width = 0;
			MatrixRow mr = this.MatrixRows.Items[0] as MatrixRow;			// assumes dyanmic  TODO
			float height = mr.Height == null? 0: mr.Height.Points;

			foreach (MatrixEntry ame in cm.SortedData.Values)
			{
				if (ame.ColumnGroup != _lastCg)
				{
					RunDataColumn(rm, ame, matrix, _Data, iRow, ref iColumn);
					if (ame.ColumnGroup.DynamicColumns.Subtotal != null)
					{
						andData = new BitArray(ame.Rows);	// copy the data
						andData.And(rm.Rows);				//  because And is destructive
						matrix[iRow, iColumn] = RunGetMatrixCell(_Data, andData, 
							Math.Max(rm.FirstRow, ame.FirstRow),
							Math.Min(rm.LastRow, ame.LastRow));
						matrix[iRow, iColumn].Height = height;
						matrix[iRow, iColumn].Width = width;
						iColumn++;
					}
					continue;
				}
				andData = new BitArray(ame.Rows);	// copy the data
				andData.And(rm.Rows);				//  because And is destructive
				matrix[iRow, iColumn] = RunGetMatrixCell(_Data, andData, 
						Math.Max(rm.FirstRow, ame.FirstRow),
						Math.Min(rm.LastRow, ame.LastRow));
				matrix[iRow, iColumn].Height = height;
				matrix[iRow, iColumn].Width = width;
				
				iColumn++;
			}
			// if top level and we need subtotal on the whole group
			if (cm.ColumnGroup == null && _lastCg.DynamicColumns.Subtotal != null)
			{
				andData = new BitArray(cm.Rows);	// copy the data
				andData.And(rm.Rows);				//  because And is destructive
				matrix[iRow, iColumn] = RunGetMatrixCell(_Data, andData, 
					Math.Max(rm.FirstRow, cm.FirstRow),
					Math.Min(rm.LastRow, cm.LastRow));
				matrix[iRow, iColumn].Height = height;
				matrix[iRow, iColumn].Width = width;
				iColumn++;
			}
		}

		void RunDataRow(MatrixEntry rm, MatrixEntry cm, MatrixCellEntry[,] matrix, Rows _Data, ref int iRow, int iColumn)
		{
			int saveColumn;
			foreach (MatrixEntry ame in rm.SortedData.Values)
			{
				if (ame.RowGroup != _lastRg)
				{
					RunDataRow(ame, cm, matrix, _Data, ref iRow, iColumn);
					if (ame.RowGroup.DynamicRows.Subtotal != null)
					{
						saveColumn = iColumn;
						RunDataColumn(ame, cm, matrix, _Data, iRow, ref saveColumn);
						iRow++;
					}
					continue;
				}
				saveColumn = iColumn;
				RunDataColumn(ame, cm, matrix, _Data, iRow, ref saveColumn);
				iRow++;
			}
			// if top level and we need subtotal on the whole group
			if (rm.RowGroup == null && _lastRg.DynamicRows.Subtotal != null)
			{
				saveColumn = iColumn;
				RunDataColumn(rm, cm, matrix, _Data, iRow, ref saveColumn);
				iRow++;
			}
		}

		void RunRowHeaders(MatrixEntry m, MatrixCellEntry[,] matrix, Rows _Data, ref int iRow, int iColumn)
		{
//			ColumnGrouping cg = this.ColumnGroupings.Items[iColumn] as ColumnGrouping;
//			float height = cg.Height == null? 0: cg.Height.Points;
			MatrixRow mr = this.MatrixRows.Items[0] as MatrixRow;	// this will change when support "static"
			float height = mr.Height == null? 0: mr.Height.Points;

			foreach (MatrixEntry ame in m.SortedData.Values)
			{
				matrix[iRow, iColumn] = RunGetRowHeader(ame, _Data);
				matrix[iRow, iColumn].Width = ame.RowGroup.Width == null? 0: ame.RowGroup.Width.Points;
				matrix[iRow, iColumn].Height = height;
				if (ame.SortedData != null)
				{
					RunRowHeaders(ame, matrix, _Data, ref iRow, iColumn+1);
					if (ame.RowGroup.DynamicRows.Subtotal != null)
					{
						ReportItem ri = (ReportItem) (ame.RowGroup.DynamicRows.Subtotal.ReportItems.Items[0]);	
						Rows subData = new Rows(_Data, ame.FirstRow, ame.LastRow, ame.Rows);
						matrix[iRow, iColumn] =  new MatrixCellEntry(subData, ri);
						matrix[iRow, iColumn].Width = ame.RowGroup.Width == null? 0: ame.RowGroup.Width.Points;
						matrix[iRow, iColumn].Height = height;
						iRow++;
					}
				}
				else
					iRow++;
			}
			// if top level and we need subtotal on the whole group
			if (m.RowGroup == null && _lastRg.DynamicRows.Subtotal != null)
			{
				ReportItem ri = (ReportItem) (_lastRg.DynamicRows.Subtotal.ReportItems.Items[0]);	
				matrix[iRow, iColumn] = new MatrixCellEntry(_Data, ri);
				matrix[iRow, iColumn].Width = _lastRg.Width == null? 0: _lastRg.Width.Points;
				matrix[iRow, iColumn].Height = height;
				iRow++;
			}
		}

		MatrixCellEntry RunGetRowHeader(MatrixEntry me, Rows _Data)
		{
			ReportItem ri = (ReportItem) (me.RowGroup.DynamicRows.ReportItems.Items[0]);	
			Rows subData = new Rows(_Data, me.FirstRow, me.LastRow, me.Rows);
			MatrixCellEntry mce = new MatrixCellEntry(subData, ri);

			return mce;
		}

		MatrixCellEntry RunGetMatrixCell(Rows _Data, BitArray rows, int firstRow, int lastRow)
		{
			MatrixRow mr = (MatrixRow) (this._MatrixRows.Items[0]);	
			MatrixCell mc = (MatrixCell) (mr.MatrixCells.Items[0]);
			ReportItem ri = (ReportItem) (mc.ReportItems.Items[0]);
			Rows subData = new Rows(_Data, firstRow, lastRow, rows);

			MatrixCellEntry mce = new MatrixCellEntry(subData, ri);
			
			return mce;
		}

		internal Corner Corner 
		{
			get { return  _Corner; }
			set {  _Corner = value; }
		}

		internal ColumnGroupings ColumnGroupings
		{
			get { return  _ColumnGroupings; }
			set {  _ColumnGroupings = value; }
		}

		internal Rows Data
		{
			get { return  _Data; }
			set {  _Data = value; }
		}

		internal RowGroupings RowGroupings
		{
			get { return  _RowGroupings; }
			set {  _RowGroupings = value; }
		}

		internal MatrixRows MatrixRows
		{
			get { return  _MatrixRows; }
			set {  _MatrixRows = value; }
		}

		internal MatrixColumns MatrixColumns
		{
			get { return  _MatrixColumns; }
			set {  _MatrixColumns = value; }
		}

		internal MatrixLayoutDirectionEnum LayoutDirection
		{
			get { return  _LayoutDirection; }
			set {  _LayoutDirection = value; }
		}

		internal int GroupsBeforeRowHeaders
		{
			get { return  _GroupsBeforeRowHeaders; }
			set {  _GroupsBeforeRowHeaders = value; }
		}

		internal string CellDataElementName
		{
			get { return  _CellDataElementName; }
			set {  _CellDataElementName = value; }
		}

		internal MatrixCellDataElementOutputEnum CellDataElementOutput
		{
			get { return  _CellDataElementOutput; }
			set {  _CellDataElementOutput = value; }
		}
	}
}
