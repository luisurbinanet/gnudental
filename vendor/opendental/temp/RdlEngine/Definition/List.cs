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
using System.Drawing;

namespace fyiReporting.RDL
{
	///<summary>
	/// Represents the report item for a List (i.e. absolute positioning)
	///</summary>
	[Serializable]
	internal class List : DataRegion
	{
		Grouping _Grouping;		//The expressions to group the data by
								// Required if there are any DataRegions
								// contained within this List
		Sorting _Sorting;		// The expressions to sort the repeated list regions by
		ReportItems _ReportItems;	// The elements of the list layout
		string _DataInstanceName;	// The name to use for the data element for the
								// each instance of this list. Ignored if there is a
								// grouping for the list.
								// Default: "Item"
		DataInstanceElementOutputEnum _DataInstanceElementOutput;
							// Indicates whether the list instances should
							// appear in a data rendering. Ignored if there is
							// a grouping for the list.  Default: output
		bool _CanGrow;			// indicates that row height can increase in size
		ArrayList _GrowList;	// list of TextBox's that need to be checked for growth

		// Runtime variables
		[NonSerialized] float _CalcHeight;		// dynamic when CanGrow true
		[NonSerialized] Rows _Data;	// Runtime data; either original query if no groups
									// or sorting or a copied version that is grouped/sorted
		[NonSerialized] ArrayList _Groups;			// Runtime groups; array of GroupEntry
	
		internal List(Report r, ReportLink p, XmlNode xNode):base(r,p,xNode)
		{
			_Grouping=null;
			_Sorting=null;
			_ReportItems=null;
			_DataInstanceName="Item";
			_DataInstanceElementOutput=DataInstanceElementOutputEnum.Output;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "Grouping":
						_Grouping = new Grouping(r, this, xNodeLoop);
						break;
					case "Sorting":
						_Sorting = new Sorting(r, this, xNodeLoop);
						break;
					case "ReportItems":
						_ReportItems = new ReportItems(r, this, xNodeLoop);
						break;
					case "DataInstanceName":
						_DataInstanceName = xNodeLoop.InnerText;
						break;
					case "DataInstanceElementOutput":
						_DataInstanceElementOutput = fyiReporting.RDL.DataInstanceElementOutput.GetStyle(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					default:	
						if (DataRegionElement(xNodeLoop))	// try at DataRegion level
							break;
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown List element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
			DataRegionFinish();			// Tidy up the DataRegion
		}

		override internal void FinalPass()
		{
			base.FinalPass();

			if (_Grouping != null)
				_Grouping.FinalPass();
			if (_Sorting != null)
				_Sorting.FinalPass();
			if (_ReportItems != null)
				_ReportItems.FinalPass();

			// determine if the size is dynamic depending on any of its
			//   contained textbox have cangrow true
			if (this.Height != null)
				_CalcHeight = this.Height.Points;

			if (ReportItems == null)	// no report items in List region
				return;

			foreach (ReportItem ri in this.ReportItems.Items)
			{
				if (!(ri is Textbox))
					continue;
				Textbox tb = ri as Textbox;
				if (tb.CanGrow)
				{
					if (this._GrowList == null)
						_GrowList = new ArrayList();
					_GrowList.Add(tb);
					_CanGrow = true;
				}
			}

			if (_CanGrow)				// shrink down the resulting list
				_GrowList.TrimToSize();

			return;
		}

		override internal void Run(IPresent ip, Row row)
		{
			RunReset();
			_Data = GetFilteredData(row);

			if (!AnyRows(ip, _Data))		// if no rows return
				return;					//   nothing left to do

			RunSetGrouping();

			base.Run(ip, row);

			if (!ip.ListStart(this, row))	
				return;							// renderer doesn't want to continue
						   
			RunGroups(ip, _Groups);

			ip.ListEnd(this, row);
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

			RunPageRegionBegin(pgs);

			RunSetGrouping();

			RunPageGroups(pgs, _Groups);

			RunPageRegionEnd(pgs);
			SetPagePositionEnd(pgs, pgs.CurrentPage.YOffset);

		}

		private void RunReset()
		{
			_Data = null;
			_Groups = null;
		}

		private void RunSetGrouping()
		{
			GroupEntry[] currentGroups; 

			// We have some data
			if (_Grouping != null || 
				_Sorting != null)		// fix up the data
			{
				Rows saveData = _Data;
				_Data = new Rows(null, _Grouping, _Sorting);
				_Data.Data = saveData.Data;
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
			else
				currentGroups = new GroupEntry[1];

			_Data.CurrentGroups = currentGroups;

			return;
		}

		private void PrepGroups()
		{
			if (_Grouping == null)
				return;

			int i=0;
			// 1) Build array of all GroupExpression objects
			ArrayList gea = _Grouping.GroupExpressions.Items;
			GroupEntry[] currentGroups = new GroupEntry[1];
			_Grouping.Index = 0;	// set the index of this group (so we can find the GroupEntry)
			currentGroups[0] = new GroupEntry(_Grouping, 0);

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
				foreach (GroupExpression ge in gea)  // Could optimize to only calculate as needed in comparison loop below??
				{
					grpValues[i++] = ge.Expression.Evaluate(row);
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

		private void RunGroups(IPresent ip, ArrayList groupEntries)
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

				if (ge.Group == null)
				{	// need to run all the rows since no group defined
					for (int r=ge.StartRow; r <= ge.EndRow; r++)
					{
						ip.ListEntryBegin(this, (Row) ( _Data.Data[r]));
						_ReportItems.Run(ip, (Row) (_Data.Data[r]));
						ip.ListEntryEnd(this,(Row) ( _Data.Data[r]));
					}
				}
				else
				{	// need to process just whole group as a List entry
					ip.ListEntryBegin(this, (Row) ( _Data.Data[ge.StartRow]));

					// pass the first row of the group
					_ReportItems.Run(ip, (Row) (_Data.Data[ge.StartRow]));

					ip.ListEntryEnd(this,(Row) ( _Data.Data[ge.StartRow]));
				}
			}
		}

		private void RunPageGroups(Pages pgs, ArrayList groupEntries)
		{
			Page p = pgs.CurrentPage;
			float pagebottom = OwnerReport.BottomOfPage;
			p.YOffset += (Top == null? 0: this.Top.Points); 
			float listoffset = OffsetCalc+LeftCalc;

			float height;	
			Row row;

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

				if (ge.Group == null)
				{	// need to run all the rows since no group defined
					for (int r=ge.StartRow; r <= ge.EndRow; r++)
					{
						row = (Row) (_Data.Data[r]);
						height = HeightOfList(pgs.G, row);
						_ReportItems.RunPage(pgs, row, listoffset);
						p.YOffset += height;
						if (p.YOffset + height > pagebottom)		// need another page for next row?
						{
							p = RunPageNew(pgs, p);					// yes; if at end this page is empty
						}											// and will be cleaned up later				
					}
				}
				else
				{	// need to process just whole group as a List entry
					if (ge.Group.PageBreakAtStart && !p.IsEmpty())
						p = RunPageNew(pgs, p);

					// pass the first row of the group
					row = (Row) (_Data.Data[ge.StartRow]);
					height = HeightOfList(pgs.G, row);
					_ReportItems.RunPage(pgs, row, listoffset);
					p.YOffset += height;
					if (ge.Group.PageBreakAtEnd ||					// need another page for next group?
						p.YOffset + height > pagebottom)
					{
						p = RunPageNew(pgs, p);						// yes; if at end empty page will be cleaned up later
					}
				}
			}
		}

		private void RunGroupsSetGroups(ArrayList groupEntries)
		{
			// set the group entry value
			GroupEntry ge = (GroupEntry) (groupEntries[0]);
			_Data.CurrentGroups[ge.Group.Index] = ge;

			if (ge.NestedGroup.Count > 0)
				RunGroupsSetGroups(ge.NestedGroup);
		}

		internal Grouping Grouping
		{
			get { return  _Grouping; }
			set {  _Grouping = value; }
		}

		internal float HeightOfList(Graphics g, Row r)
		{		   
			float defnHeight = this.HeightOrOwnerHeight;
			if (!_CanGrow)
				return defnHeight;

			float height;
			foreach (Textbox tb in this._GrowList)
			{
				float top = (float) (tb.Top == null? 0.0 : tb.Top.Points);
				height = top + tb.RunTextCalcHeight(g, r);
				if (tb.Style != null)
					height += (tb.Style.EvalPaddingBottom(r) + tb.Style.EvalPaddingTop(r));
				defnHeight = Math.Max(height, defnHeight);
			}
			_CalcHeight = defnHeight;
			return _CalcHeight;
		}

		internal Sorting Sorting
		{
			get { return  _Sorting; }
			set {  _Sorting = value; }
		}

		internal ReportItems ReportItems
		{
			get { return  _ReportItems; }
			set {  _ReportItems = value; }
		}

		internal string DataInstanceName
		{
			get { return  _DataInstanceName == null? "Item": _DataInstanceName; }
			set {  _DataInstanceName = value; }
		}

		internal DataInstanceElementOutputEnum DataInstanceElementOutput
		{
			get { return  _DataInstanceElementOutput; }
			set {  _DataInstanceElementOutput = value; }
		}
	}
}
