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
using System.Globalization;

namespace fyiReporting.RDL
{
	///<summary>
	/// Table definition and processing.  Inherits from DataRegion which inherits from ReportItem.
	///</summary>
	[Serializable]
	internal class Table : DataRegion
	{
		TableColumns _TableColumns;	// The columns in the table
		Header _Header;			// The table header rows
		TableGroups _TableGroups;	// The groups (group expressions/headers/footers) for the table
		Details _Details;		// The details rows for the table
								//	The table must have at least one of:
								//	Details, Header or Footer
		Footer _Footer;			// The table footer rows
		bool _FillPage;			// Indicates the table should expand to
								//	fill the page, pushing items below it
								//	to the bottom of the page.
		string _DetailDataElementName;	//The name to use for the data element
									// for instances of this group. Ignored if
									// there is a grouping defined for the details.
									// Default: “Details”
		string _DetailDataCollectionName;	// The name to use for the data element
											// for the collection of all instances of this group.
											// Default: “Details_Collection”
		DataElementOutputEnum _DetailDataElementOutput;	// Indicates whether the details should appear in a data rendering.
	
		// Runtime variables
		[NonSerialized] Rows _Data;	// Runtime data; either original query if no groups
									// or sorting or a copied version that is grouped/sorted
		[NonSerialized] ArrayList _Groups;		// Runtime groups; array of GroupEntry
		[NonSerialized] int _GroupNestCount;	// Runtime: calculated on fly for # of table rows that are part of a group
												//    used to handle toggling of a group 
		[NonSerialized] Grouping _RecursiveGroup;	// Runtime: set with a recursive; currently on support a single recursive group
		
		internal Table(Report r, ReportLink p, XmlNode xNode):base(r,p,xNode)
		{
			_TableColumns=null;
			_Header=null;
			_TableGroups=null;
			_Details=null;
			_Footer=null;
			_FillPage=true;
			_DetailDataElementName="Details";
			_DetailDataCollectionName="Details_Collection";
			_DetailDataElementOutput=DataElementOutputEnum.Output;
			_Data = null;
			_Groups = null;
			_RecursiveGroup = null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "TableColumns":
						_TableColumns = new TableColumns(r, this, xNodeLoop);
						break;
					case "Header":
						_Header = new Header(r, this, xNodeLoop);
						break;
					case "TableGroups":
						_TableGroups = new TableGroups(r, this, xNodeLoop);
						break;
					case "Details":
						_Details = new Details(r, this, xNodeLoop);
						break;
					case "Footer":
						_Footer = new Footer(r, this, xNodeLoop);
						break;
					case "FillPage":
						_FillPage = XmlUtil.Boolean(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					case "DetailDataElementName":
						_DetailDataElementName = xNodeLoop.InnerText;
						break;
					case "DetailDataCollectionName":
						_DetailDataCollectionName = xNodeLoop.InnerText;
						break;
					case "DetailDataElementOutput":
						_DetailDataElementOutput = fyiReporting.RDL.DataElementOutput.GetStyle(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					default:	
						if (DataRegionElement(xNodeLoop))	// try at DataRegion level
							break;
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown Table element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
			DataRegionFinish();			// Tidy up the DataRegion
			if (_TableColumns == null)
			{
				OwnerReport.rl.LogError(8, "TableColumns element must be specified for a Table.");
				return;
			}

			if (OwnerReport.rl.MaxSeverity < 8)
				VerifyCC();			// Verify column count
		}

		private void VerifyCC()
		{
			int colCount = this._TableColumns.Items.Count;
			if (this._Header != null)
				VerifyCCTableRows("Table Header", _Header.TableRows, colCount);

			if (this._Footer != null)
				VerifyCCTableRows("Table Footer", _Footer.TableRows, colCount);

			if (this._Details != null)
				VerifyCCTableRows("Table Details", _Details.TableRows, colCount);

			if (this._TableGroups != null)
			{
				foreach (TableGroup tg in _TableGroups.Items)
				{
					if (tg.Header != null)
						VerifyCCTableRows("TableGroup Header", tg.Header.TableRows, colCount);

					if (tg.Footer != null)
						VerifyCCTableRows("TableGroup Footer", tg.Footer.TableRows, colCount);
				}
			}
		}

		private void VerifyCCTableRows(string label, TableRows trs, int colCount)
		{
			foreach (TableRow tr in trs.Items)
			{
				int cols=0;
				foreach (TableCell tc in tr.TableCells.Items)
				{
					cols += tc.ColSpan;
				}
				if (cols != colCount)
					OwnerReport.rl.LogError(8, String.Format("{0} must have the same number of columns as TableColumns.", label));
			}
			return;
		}

		override internal void FinalPass()
		{
			base.FinalPass();
			float totalHeight=0;

			if (_TableColumns != null)
				_TableColumns.FinalPass();
			if (_Header != null)
			{
				_Header.FinalPass();
				totalHeight += _Header.TableRows.DefnHeight();
			}
			if (_TableGroups != null)
			{
				_TableGroups.FinalPass();
				totalHeight += _TableGroups.DefnHeight();
			}
			if (_Details != null)
			{
				_Details.FinalPass();
				totalHeight += _Details.TableRows.DefnHeight();
			}
			if (_Footer != null)
			{
				_Footer.FinalPass();
				totalHeight += _Footer.TableRows.DefnHeight();
			}

			if (this.Height == null)
			{	// Calculate a height based on the sum of the TableRows
				this.Height = new RSize(this.OwnerReport, 
					string.Format(NumberFormatInfo.InvariantInfo,"{0:0.00}pt", totalHeight));
			}
			return;
		}

		override internal void Run(IPresent ip, Row row)
		{
			RunReset();

			_Data = GetFilteredData(row);

			if (!AnyRows(ip, _Data))		// if no rows return
				return;					//   nothing left to do

			RunPrep(row);

			if (!ip.TableStart(this, row))
				return;						// render doesn't want to continue

			if (_TableColumns != null)
				_TableColumns.Run(ip, row);

			// Header
			if (_Header != null)
			{
				ip.TableHeaderStart(_Header, row);
				Row frow = _Data.Data.Count > 0?  (Row) (_Data.Data[0]): null;
				_Header.Run(ip, frow);
				ip.TableHeaderEnd(_Header, row);
			}
						   
			// Body
			ip.TableBodyStart(this, row);
			if (this._RecursiveGroup != null)
				RunRecursiveGroups(ip);
			else
				RunGroups(ip, _Groups);
			ip.TableBodyEnd(this, row);

			// Footer
			if (_Footer != null)
			{
				ip.TableFooterStart(_Footer, row);
				Row lrow = _Data.Data.Count > 0?  (Row) (_Data.Data[_Data.Data.Count-1]): null;
				_Footer.Run(ip, (Row) lrow);
				ip.TableFooterEnd(_Footer, row);
			}

			ip.TableEnd(this, row);
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

			RunPrep(row);

			RunPageRegionBegin(pgs);

			Page p = pgs.CurrentPage;
			p.YOffset += this.RelativeY;

			// Calculate the xpositions of the columns
			TableColumns.CalculateXPositions(OffsetCalc + LeftCalc, row);

			RunPageHeader(pgs, (Row) (_Data.Data[0]), true, null);

			if (this._RecursiveGroup != null)
				RunRecursiveGroupsPage(pgs);
			else
				RunGroupsPage(pgs, _Groups, _Data.Data.Count-1, 0);

			// Footer
			if (_Footer != null)
			{
				Row lrow = _Data.Data.Count > 0?  (Row) (_Data.Data[_Data.Data.Count-1]): null;
				p = pgs.CurrentPage;
				// make sure the footer fits on the page
				if (p.YOffset + _Footer.HeightOfRows(pgs, lrow) > pgs.BottomOfPage)
				{
					p = RunPageNew(pgs, p);
					RunPageHeader(pgs, row, false, null);
				}
				_Footer.RunPage(pgs, (Row) lrow);
			}

			RunPageRegionEnd(pgs);

			SetPagePositionEnd(pgs, pgs.CurrentPage.YOffset);
		}

		internal void RunPageHeader(Pages pgs, Row frow, bool bFirst, TableGroup stoptg)
		{
			// Do the table headers
			bool isEmpty = pgs.CurrentPage.IsEmpty();

			if (_Header != null && _Header.RepeatOnNewPage)
			{
				_Header.RunPage(pgs, frow);
				if (isEmpty)
					pgs.CurrentPage.SetEmpty();		// Consider this empty of data
			}
			
			if (bFirst)							// the very first time we'll output the header (and not consider page empty)
				return;

			if (_TableGroups == null)
			{
				pgs.CurrentPage.SetEmpty();		// Consider this empty of data
				return;
			}

			// Do the group headers
			foreach (TableGroup tg in _TableGroups.Items)
			{
				if (stoptg != null && tg == stoptg)
					break;
				if (tg.Header != null)
				{
					if (tg.Header.RepeatOnNewPage)
					{
						tg.Header.RunPage(pgs, frow);
					}
				}
			}

			pgs.CurrentPage.SetEmpty();		// Consider this empty of data

			return;
		}

		void RunPrep(Row row)
		{
			GroupEntry[] currentGroups; 

			// We have some data
			if (_TableGroups != null || 
				(_Details != null &&
				 (_Details.Sorting != null ||
				 _Details.Grouping != null)))		// fix up the data
			{
				ArrayList saveData = _Data.Data;
				Grouping gr;
				Sorting srt; 
				if (_Details == null)
				{
					gr = null;  srt = null;
				}
				else
				{
					gr = _Details.Grouping;
					srt = _Details.Sorting;
				}

				_Data = new Rows(_TableGroups, gr, srt);
				_Data.Data = saveData;
				_Data.Sort();
				PrepGroups();
			}

			// If we haven't formed any groups then form one with all rows
			if (_Groups == null)
			{
				_Groups = new ArrayList();
				GroupEntry ge = new GroupEntry(null, 0);
				if (_Data.Data != null)		// Do we have any data?
					ge.EndRow = _Data.Data.Count-1;	// yes
				else
					ge.EndRow = -1;					// no
				_Groups.Add(ge);			// top group
				currentGroups = new GroupEntry[1];
			}
			else if (_TableGroups != null)
			{
				int count = _TableGroups.Items.Count;
				if (_Details != null && _Details.Grouping != null)
					count++;

				currentGroups = new GroupEntry[count];
			}
			else
				currentGroups = new GroupEntry[1];

			_Data.CurrentGroups = currentGroups;
		}

		private void PrepGroups()
		{
			_RecursiveGroup = null;		
			if (_TableGroups == null)
			{	// no tablegroups; check to ensure details is grouped
				if (_Details == null || _Details.Grouping == null)
					return;			// no groups to prepare
			}

			int i=0;
			// 1) Build array of all GroupExpression objects
			ArrayList gea = new ArrayList();
			//    count the number of groups
			int countG=0;
			if (_TableGroups != null)
				countG = _TableGroups.Items.Count;
			
			Grouping dg=null;
			if (_Details != null && _Details.Grouping != null)
			{
				dg = _Details.Grouping;
				countG++;
			}
			GroupEntry[] currentGroups = new GroupEntry[countG++];
			if (_TableGroups != null)
			{	// add in the groups for the tablegroup
				foreach (TableGroup tg in _TableGroups.Items)
				{
					if (tg.Grouping.ParentGroup != null)
						_RecursiveGroup = tg.Grouping;
					tg.Grouping.Index = i;		// set the index of this group (so we can find the GroupEntry)
					currentGroups[i++] = new GroupEntry(tg.Grouping, 0);
					foreach (GroupExpression ge in tg.Grouping.GroupExpressions.Items)
					{
						gea.Add(ge);
					}
				}
			}
			if (dg != null)
			{	// add in the groups for the details grouping
				if (dg.ParentGroup != null)
					_RecursiveGroup = dg;
				dg.Index = i;		// set the index of this group (so we can find the GroupEntry)
				currentGroups[i++] = new GroupEntry(dg, 0);
				foreach (GroupExpression ge in dg.GroupExpressions.Items)
				{
					gea.Add(ge);
				}
			}

			if (_RecursiveGroup != null)
			{
				if (gea.Count != 1)		// Limitiation of implementation
					throw new Exception("Error: Recursive groups must be the only group definition.");

				PrepRecursiveGroup();	// only one group and it's recursive: optimization 
				return;
			}

			// Save the typecodes, and grouping by groupexpression; for later use
			TypeCode[] tcs = new TypeCode[gea.Count];
			Grouping[] grp = new Grouping[gea.Count];
			i=0;
			foreach (GroupExpression ge in gea)
			{
				grp[i] = (Grouping) (ge.Parent.Parent);			// remember the group
				tcs[i++] = ge.Expression.GetTypeCode();	// remember type of expression
			}

			// 2) Loop thru the data, then loop thru the GroupExpression list
			_Groups = new ArrayList();
			object[] savValues=null;
			object[] grpValues=null;
			int rowCurrent = 0;

			foreach (Row row in _Data.Data)
			{
				// Get the values for all the group expressions
				if (grpValues == null)
					grpValues = new object[gea.Count];

				i=0;
				foreach (GroupExpression ge in gea)
				{
					if (((Grouping)(ge.Parent.Parent)).ParentGroup == null)	
						grpValues[i++] = ge.Expression.Evaluate(row);
					else
						grpValues[i++] = null;	// Want all the parentGroup to evaluate equal
				}

				// For first row we just primed the pump; action starts on next row
				if (rowCurrent == 0)			// always start new group on first row
				{
					rowCurrent++;
					savValues = grpValues;
					grpValues = null;
					continue;
				}

				// compare the values; if change then we have a group break
				for (i = 0; i < savValues.Length; i++)
				{
					if (Filter.ApplyCompare(tcs[i], savValues[i], grpValues[i]) != 0)
					{
						// start a new group; and force a break on every subgroup
						GroupEntry saveGe=null;	
						for (int j = grp[i].Index; j < currentGroups.Length; j++)
						{
							currentGroups[j].EndRow = rowCurrent-1;
							if (j == 0)
								_Groups.Add(currentGroups[j]);		// top group
							else if (saveGe == null)
								currentGroups[j-1].NestedGroup.Add(currentGroups[j]);
							else 
								saveGe.NestedGroup.Add(currentGroups[j]);

							saveGe = currentGroups[j];	// retain this GroupEntry
							currentGroups[j] = new GroupEntry(currentGroups[j].Group, rowCurrent);
						}
						savValues = grpValues;
						grpValues = null;
						break;		// break out of the value comparison loop
					}
				}
				rowCurrent++;
			}

			// End of all rows force break on end of rows
			for (i = 0; i < currentGroups.Length; i++)
			{
				currentGroups[i].EndRow = rowCurrent-1;
				if (i == 0)
					_Groups.Add(currentGroups[i]);		// top group
				else
					currentGroups[i-1].NestedGroup.Add(currentGroups[i]);
			}

			return;
		}

		private void RunReset()
		{
			_Data = null;
			_Groups = null;
			_GroupNestCount=0;
			_RecursiveGroup=null;
			return;
		}

		private void PrepRecursiveGroup()
		{
			// Prepare for processing recursive group
			Grouping g = this._RecursiveGroup;
			IExpr parentExpr = g.ParentGroup;
			GroupExpression gexpr = g.GroupExpressions.Items[0] as GroupExpression;
			IExpr groupExpr = gexpr.Expression;
			TypeCode tc = groupExpr.GetTypeCode();
			ArrayList odata = new ArrayList(_Data.Data);			// this is the old data that we'll recreate using the recursive hierarchy
			ArrayList newrows = new ArrayList(odata.Count);

			// For now we assume on a single top of tree (and it must sort first as null)
			//   spec is incomplete: should have ability to specify starting value of tree
			// TODO: pull all of the rows that start with null
			newrows.Add(odata[0]);					// add the starting row
			odata.RemoveAt(0);						//   remove olddata

			// we need to build the group entry stack
			// Build the initial one
			_Groups = new ArrayList();
			GroupEntry ge = new GroupEntry(null, 0);
			ge.EndRow = odata.Count-1;
			_Groups.Add(ge);				// top group

			ArrayList ges = new ArrayList();
			ges.Add(ge);

			// loop thru the rows and find their children
			//   we place the children right after the parent
			//   this reorders the rows in the form of the hierarchy
			Row r;
			RecursiveCompare rc = new RecursiveCompare(parentExpr, tc);
			for (int iRow=0; iRow < newrows.Count; iRow++)	// go thru the temp rows
			{
				r = (Row) newrows[iRow];
				
				r.GroupEntry = ge = new GroupEntry(g, iRow);
				r.GroupEntry.EndRow = iRow;

				object cmpvalue = groupExpr.Evaluate(r);
				// pull out all the rows that match this value
				int iMainRow=odata.BinarySearch(cmpvalue, rc);
				if (iMainRow < 0)
				{
					for (int i=0; i <= r.Level+1 && i < ges.Count; i++)
					{
						ge = ges[i] as GroupEntry;
						Row rr = (Row) newrows[ge.StartRow];	// start row has the base level of group	
						if (rr.Level < r.Level)					
							ge.EndRow = iRow;
					}
					continue;
				}

				// look backward for starting row; 
				//   in case of duplicates, BinarySearch can land on any of the rows
				int sRow = iMainRow-1;
				while (sRow >= 0)
				{
					object v = parentExpr.Evaluate((Row) odata[sRow]);
					if (Filter.ApplyCompare(tc, cmpvalue, v) != 0)
						break;
					sRow--;
				}
				sRow++;		// adjust; since we went just prior it
				// look forward for ending row
				int eRow = iMainRow+1;
				while (eRow < odata.Count)
				{
					object v = parentExpr.Evaluate((Row) odata[eRow]);
					if (Filter.ApplyCompare(tc, cmpvalue, v) != 0)
						break;
					eRow++;
				}
				// Build a group entry for this
				GroupEntry ge2 = ges[r.Level] as GroupEntry;
				ge2.NestedGroup.Add(ge);
				if (r.Level+1 >= ges.Count)	// ensure we have room in the array (based on level)
					ges.Add(ge);						// add to the array
				else
				{
					ges[r.Level+1] = ge;				// put this in the array
				}

				// add all of them in; want the same order for these rows.
				int offset=1;
				for (int tRow=sRow ; tRow < eRow; tRow++)
				{
					Row tr = (Row) odata[tRow];
					tr.Level = r.Level+1;
					newrows.Insert(iRow+offset, tr);
					offset++;	
				}
				// remove from old data
				odata.RemoveRange(sRow, eRow-sRow);
			}

			// update the groupentries for the very last row
			int lastrow = newrows.Count-1;
			r = (Row) newrows[lastrow];
			for (int i=0; i < r.Level+1 && i < ges.Count; i++)
			{
				ge = ges[i] as GroupEntry;
				ge.EndRow = lastrow;
			}

			_Data.Data = newrows;		// we've completely replaced the rows
			return;
		}

		private void RunGroups(IPresent ip, ArrayList groupEntries)
		{
			GroupEntry fge = (GroupEntry) (groupEntries[0]);
			if (fge.Group != null)
				ip.GroupingStart(fge.Group);

			foreach (GroupEntry ge in groupEntries)
			{
				// set the group entry value
				int index;
				if (ge.Group != null)	// groups?
				{
					ip.GroupingInstanceStart(ge.Group);
					ge.Group.ResetHideDuplicates();	// reset duplicate checking
					index = ge.Group.Index;	// yes
				}
				else					// no; must be main dataset
					index = 0;
				_Data.CurrentGroups[index] = ge;

				if (ge.NestedGroup.Count > 0)
					RunGroupsSetGroups(ge.NestedGroup);

				// Handle the group header
				if (ge.Group != null && ge.Group.Parent != null)
				{
					TableGroup tg = ge.Group.Parent as TableGroup;
					if (tg != null && tg.Header != null)
					{
						// Calculate the number of table rows below this group; header, footer, details count
						if (ge.NestedGroup.Count > 0)
							_GroupNestCount = RunGroupsCount(ge.NestedGroup, 0);
						else
							_GroupNestCount = (ge.EndRow - ge.StartRow + 1) * DetailsCount;
						tg.Header.Run(ip, (Row) (_Data.Data[ge.StartRow]));
						_GroupNestCount = 0;
					}
				}
				// Handle the nested groups if any
				if (ge.NestedGroup.Count > 0)
					RunGroups(ip, ge.NestedGroup);
				// If no nested groups then handle the detail rows for the group
				else if (_Details != null)
				{
					if (ge.Group != null &&
						ge.Group.Parent as TableGroup == null)
					{	// Group defined on table; means that Detail rows only put out once per group
						_Details.Run(ip, _Data, ge.StartRow, ge.StartRow);
					}
					else
						_Details.Run(ip, _Data, ge.StartRow, ge.EndRow);
				}

				// Do the group footer
				if (ge.Group != null)
				{
					if (ge.Group.Parent != null)
					{
						TableGroup tg = ge.Group.Parent as TableGroup;	// detail groups will result in null
						if (tg != null && tg.Footer != null)
							tg.Footer.Run(ip, (Row) (_Data.Data[ge.EndRow]));
					}
					ip.GroupingInstanceEnd(ge.Group);
				}
			}
			if (fge.Group != null)
				ip.GroupingEnd(fge.Group);
		}

		private void RunGroupsPage(Pages pgs, ArrayList groupEntries, int endRow, float groupHeight)
		{
			foreach (GroupEntry ge in groupEntries)
			{
				// set the group entry value
				int index;
				if (ge.Group != null)	// groups?
				{
					ge.Group.ResetHideDuplicates();	// reset duplicate checking
					index = ge.Group.Index;	// yes
				}
				else					// no; must be main dataset
					index = 0;
				_Data.CurrentGroups[index] = ge;

				if (ge.NestedGroup.Count > 0)
					RunGroupsSetGroups(ge.NestedGroup);

				// Handle the group header
				if (ge.Group != null)
				{
					TableGroup tg = ge.Group.Parent as TableGroup;
					if (ge.Group.PageBreakAtStart && !pgs.CurrentPage.IsEmpty())
					{
						RunPageNew(pgs, pgs.CurrentPage);
						RunPageHeader(pgs, (Row) (_Data.Data[ge.StartRow]), false, tg);
					}

					if (tg != null && tg.Header != null)
					{
						// Calculate the number of table rows below this group; header, footer, details count
						if (ge.NestedGroup.Count > 0)
							_GroupNestCount = RunGroupsCount(ge.NestedGroup, 0);
						else
							_GroupNestCount = (ge.EndRow - ge.StartRow + 1) * DetailsCount;

						tg.Header.RunPage(pgs, (Row) (_Data.Data[ge.StartRow]));
						_GroupNestCount = 0;
					}
				}
				float footerHeight = RunGroupsFooterHeight(pgs, ge);
				if (ge.EndRow == endRow)
					footerHeight += groupHeight;
				// Handle the nested groups if any
				if (ge.NestedGroup.Count > 0)
					RunGroupsPage(pgs, ge.NestedGroup, ge.EndRow, footerHeight);
				// If no nested groups then handle the detail rows for the group
				else if (_Details != null)
				{
					if (ge.Group != null &&
						ge.Group.Parent as TableGroup == null)
					{	// Group defined on table; means that Detail rows only put out once per group
						_Details.RunPage(pgs, _Data, ge.StartRow, ge.StartRow, footerHeight);
					}
					else
					{
						_Details.RunPage(pgs, _Data, ge.StartRow, ge.EndRow, footerHeight);
					}
				}
				else	// When no details we need to figure out whether any more fits on a page
				{
					Page p = pgs.CurrentPage;
					if (p.YOffset + footerHeight > pgs.BottomOfPage) //	Do we need new page to fit footer?
					{
						p = RunPageNew(pgs, p);
						RunPageHeader(pgs, (Row) (_Data.Data[ge.EndRow]), false, null);
					}
				}

				// Do the group footer
				if (ge.Group != null && ge.Group.Parent != null)
				{
					TableGroup tg = ge.Group.Parent as TableGroup;	// detail groups will result in null
					if (tg != null && tg.Footer != null)
						tg.Footer.RunPage(pgs, (Row) (_Data.Data[ge.EndRow]));

					if (ge.Group.PageBreakAtEnd && !pgs.CurrentPage.IsEmpty())
					{
						RunPageNew(pgs, pgs.CurrentPage);
						RunPageHeader(pgs, (Row) (_Data.Data[ge.StartRow]), false, tg);
					}
				}

			}
		}

		private float RunGroupsFooterHeight(Pages pgs, GroupEntry ge)
		{
			Grouping g = ge.Group;
			if (g == null)
				return 0;

			TableGroup tg = g.Parent as TableGroup;		// detail groups will result in null
			if (tg == null || tg.Footer == null)
				return 0;

			return tg.Footer.HeightOfRows(pgs, (Row) (_Data.Data[ge.EndRow]));
		}

		private int RunGroupsCount(ArrayList groupEntries, int count)
		{
			foreach (GroupEntry ge in groupEntries)
			{
				Grouping g = ge.Group;
				if (g != null)
				{
					TableGroup tg = g.Parent as TableGroup;
					if (tg != null)
						count += (tg.HeaderCount + tg.FooterCount);
				}
				if (ge.NestedGroup.Count > 0)
					count = RunGroupsCount(ge.NestedGroup, count);
				else
					count += (ge.EndRow - ge.StartRow + 1) * DetailsCount;
			}
			return count;
		}

		private void RunGroupsSetGroups(ArrayList groupEntries)
		{
			// set the group entry value
			GroupEntry ge = (GroupEntry) (groupEntries[0]);
			_Data.CurrentGroups[ge.Group.Index] = ge;

			if (ge.NestedGroup.Count > 0)
				RunGroupsSetGroups(ge.NestedGroup);
		}

		private void RunRecursiveGroups(IPresent ip)
		{
			ArrayList rows=_Data.Data;
			Row r;

			// Get any header/footer information for use in loop
			Header header=null;
			Footer footer=null;
			TableGroup tg = _RecursiveGroup.Parent as TableGroup;
			if (tg != null)
			{
				header = tg.Header;
				footer = tg.Footer;
			}

			bool bHeader = true;
			for (int iRow=0; iRow < rows.Count; iRow++)
			{
				r = (Row) rows[iRow];
				_Data.CurrentGroups[0] = r.GroupEntry;
				_GroupNestCount = r.GroupEntry.EndRow - r.GroupEntry.StartRow;
				if (bHeader)
				{
					bHeader = false;
					if (header != null)
						header.Run(ip, r);
				}

				if (_Details != null)
				{
					_Details.Run(ip, _Data, iRow, iRow);
				}

				// determine need for group headers and/or footers
				if (iRow < rows.Count - 1)
				{
					Row r2 = (Row) rows[iRow+1];
					if (r.Level > r2.Level)
					{
						if (footer != null)
							footer.Run(ip, r);
					}
					else if (r.Level < r2.Level)
						bHeader = true;
				}
			}
			if (footer != null)
				footer.Run(ip, rows[rows.Count-1] as Row);
		}

		private void RunRecursiveGroupsPage(Pages pgs)
		{
			ArrayList rows=_Data.Data;
			Row r;

			// Get any header/footer information for use in loop
			Header header=null;
			Footer footer=null;
			TableGroup tg = _RecursiveGroup.Parent as TableGroup;
			if (tg != null)
			{
				header = tg.Header;
				footer = tg.Footer;
			}

			bool bHeader = true;
			for (int iRow=0; iRow < rows.Count; iRow++)
			{
				r = (Row) rows[iRow];
				_Data.CurrentGroups[0] = r.GroupEntry;
				_GroupNestCount = r.GroupEntry.EndRow - r.GroupEntry.StartRow;
				if (bHeader)
				{
					bHeader = false;
					if (header != null)
					{
						Page p = pgs.CurrentPage;			// this can change after running a row
						float height = p.YOffset + header.HeightOfRows(pgs, r);
						if (height > pgs.BottomOfPage)
						{
							p = RunPageNew(pgs, p);
							RunPageHeader(pgs, r, false, null);
							if (!header.RepeatOnNewPage)
								header.RunPage(pgs, r);
						}
						else
							header.RunPage(pgs, r);
					}
				}

				// determine need for group headers and/or footers
				bool bFooter = false;
				float footerHeight=0;
 
				if (iRow < rows.Count - 1)
				{
					Row r2 = (Row) rows[iRow+1];
					if (r.Level > r2.Level)
					{
						if (footer != null)
						{
							bFooter = true;
							footerHeight = footer.HeightOfRows(pgs, r);
						}
					}
					else if (r.Level < r2.Level)
						bHeader = true;
				}

				if (_Details != null)
				{
					_Details.RunPage(pgs, _Data, iRow, iRow, footerHeight);
				}

				// and output the footer if needed
				if (bFooter)
					footer.RunPage(pgs, r);
			}
			if (footer != null)
				footer.RunPage(pgs, rows[rows.Count-1] as Row);
		}

		internal TableColumns TableColumns
		{
			get { return  _TableColumns; }
			set {  _TableColumns = value; }
		}

		internal int ColumnCount
		{
			get { return _TableColumns.Items.Count; }
		}

		internal Header Header
		{
			get { return  _Header; }
			set {  _Header = value; }
		}

		internal TableGroups TableGroups
		{
			get { return  _TableGroups; }
			set {  _TableGroups = value; }
		}

		internal Details Details
		{
			get { return  _Details; }
			set {  _Details = value; }
		}

		internal int DetailsCount
		{
			get 
			{
				if (_Details == null)
					return 0;
				return  
					_Details.TableRows.Items.Count; 
			}
		}

		internal Footer Footer
		{
			get { return  _Footer; }
			set {  _Footer = value; }
		}

		internal bool FillPage
		{
			get { return  _FillPage; }
			set {  _FillPage = value; }
		}

		internal string DetailDataElementName
		{
			get { return  _DetailDataElementName; }
			set {  _DetailDataElementName = value; }
		}

		internal string DetailDataCollectionName
		{
			get { return  _DetailDataCollectionName; }
			set {  _DetailDataCollectionName = value; }
		}

		internal DataElementOutputEnum DetailDataElementOutput
		{
			get { return  _DetailDataElementOutput; }
			set {  _DetailDataElementOutput = value; }
		}

		internal int GroupNestCount
		{
			get { return _GroupNestCount; }
		}

		internal int WidthInPixels
		{
			get 
			{	// -1 indicates no width is available	
//				if (this.Width != null)		// Use width if specified
//					return this.Width.PixelsX;

				// Otherwise, calculate this based on the sum of TableColumns
				int width=0;
				foreach (TableColumn tc in this.TableColumns.Items)
				{
					width += tc.Width.PixelsX;
				}
				return width;
			}
		}

		internal int WidthInUnits
		{
			get 
			{

				// Calculate this based on the sum of TableColumns
				int width=0;
				foreach (TableColumn tc in this.TableColumns.Items)
				{
					width += tc.Width.Size;
				}
				return width;
			}
		}
	}

	class RecursiveCompare: IComparer
	{
		TypeCode _Type;
		IExpr parentExpr;

		internal RecursiveCompare(IExpr pExpr, TypeCode tc)
		{
			_Type = tc;
			parentExpr = pExpr;
		}

		#region IComparer Members

		public int Compare(object x, object y)
		{
			// .Net is consistent where x is always the Row but Mono is not
			if (x is Row)
			{
				object v = parentExpr.Evaluate((Row) x);
			
				return -Filter.ApplyCompare(_Type, y, v);
			}
			else
			{
				object v = parentExpr.Evaluate((Row) y);
			
				return -Filter.ApplyCompare(_Type, x, v);
			}
		}

		#endregion
	}

}
