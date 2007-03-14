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
using System.Drawing.Imaging;
using System.Text;
using System.Xml;
using System.Globalization;
using System.Drawing;

namespace fyiReporting.RDL
{
	
	///<summary>
	/// Renders a report to HTML.   This handles some page formating but does not do true page formatting.
	///</summary>
	internal class RenderHtml: IPresent
	{
		Report r;					// report
		StringWriter tw;			// temporary location where the output is going
		IStreamGen _sg;				// stream generater
		Hashtable _styles;			// hash table of styles we've generated
		int cssId=1;				// ID for css when names are usable or available
		bool bScriptToggle=false;	// need to generate toggle javascript in header
		bool bScriptTableSort=false; // need to generate table sort javascript in header
		Bitmap _bm=null;			// bm and
		Graphics _g=null;			//		  g are needed when calculating string heights

		public RenderHtml(Report rep, IStreamGen sg)
		{
			r = rep;
			_sg = sg;					// We need this in future

			tw = new StringWriter();	// will hold the bulk of the HTML until we generate
			// final file
			_styles = new Hashtable();
		}
		~RenderHtml()
		{
			// These should already be cleaned up; but in case of an unexpected error 
			//   these still need to be disposed of
			if (_bm != null)
				_bm.Dispose();
			if (_g != null)
				_g.Dispose();
		}

		public bool IsPagingNeeded()
		{
			return false;
		}

		public void Start()		
		{
			return;
		}

		// puts the JavaScript into the header
		private void ScriptGenerate(TextWriter ftw)
		{
			if (bScriptToggle || bScriptTableSort)
			{
				ftw.WriteLine("<script language=\"javascript\">");
			}

			if (bScriptToggle)
			{
				ftw.WriteLine("var dname='';");
				ftw.WriteLine(@"function hideShow(node, hideCount, showID) {
if (navigator.appName.toLowerCase().indexOf('netscape') > -1) 
	dname = 'table-row';
else
	dname = 'block';

 var tNode;
 for (var ci=0;ci<node.childNodes.length;ci++) {
     if (node.childNodes[ci].tagName && node.childNodes[ci].tagName.toLowerCase() == 'img') tNode = node.childNodes[ci];
 }
	var rows = findObject(showID);
	if (rows[0].style.display == dname) {hideRows(rows, hideCount); tNode.src='plus.gif';}
	else {
	   tNode.src='minus.gif';
	   for (var i = 0; i < rows.length; i++) {
		  rows[i].style.display = dname;
	   }
	}
}
function hideRows(rows, count) {
    var row;
	if (navigator.appName.toLowerCase().indexOf('netscape') > -1) 
	{
		for (var r=0; r < rows.length; r++) {
			row = rows[r];
			row.style.display = 'none';
			var imgs = row.getElementsByTagName('img');
			for (var ci=0;ci<imgs.length;ci++) {
				if (imgs[ci].className == 'toggle') {
					imgs[ci].src='plus.gif';
				}
			}
		}
	return;
	}
 if (rows.tagName == 'TR')
    row = rows;
 else
    row = rows[0];   	
 while (count > 0) {
   row.style.display = 'none';
   var imgs = row.getElementsByTagName('img');
   for (var ci=0;ci<imgs.length;ci++) {
      if (imgs[ci].className == 'toggle') {
		     imgs[ci].src='plus.gif';
      }
   }
   row = row.nextSibling;
   count--;
 }
}
function findObject(id) {
	if (navigator.appName.toLowerCase().indexOf('netscape') > -1) 
	{
	   var a = new Array();
	   var count=0;
	   for (var i=0; i < document.all.length; i++)
	   {
	      if (document.all[i].id == id)
			a[count++] = document.all[i];
	   }
		return a;
	} 
	else 
	{
	    var o = document.all[id];
		if (o.tagName == 'TR')
		{
		   var a = new Array();
		   a[0] = o;
		   return a;
		}
		return o;
	} 
}
");
 
			}

			if (bScriptTableSort)
			{
				ftw.WriteLine("var SORT_INDEX;");
				ftw.WriteLine("var SORT_DIR;");

				ftw.WriteLine("function sort_getInnerText(element) {");
				ftw.WriteLine("	if (typeof element == 'string') return element;");
				ftw.WriteLine("	if (typeof element == 'undefined') return element;");
				ftw.WriteLine("	if (element.innerText) return element.innerText;");
				ftw.WriteLine("	var s = '';");
	
				ftw.WriteLine("	var cn = element.childNodes;");
				ftw.WriteLine("	for (var i = 0; i < cn.length; i++) {");
				ftw.WriteLine("		switch (cn[i].nodeType) {");
				ftw.WriteLine("			case 1:");		// element node
				ftw.WriteLine("				s += sort_getInnerText(cn[i]);");
				ftw.WriteLine("				break;");
				ftw.WriteLine("			case 3:");		// text node
				ftw.WriteLine("				s += cn[i].nodeValue;");
				ftw.WriteLine("				break;");
				ftw.WriteLine("		}");
				ftw.WriteLine("	}");
				ftw.WriteLine("	return s;");
				ftw.WriteLine("}");

				ftw.WriteLine("function sort_table(node, sortfn, header_rows, footer_rows) {");
				ftw.WriteLine("    var arrowNode;");	// arrow node
				ftw.WriteLine("    for (var ci=0;ci<node.childNodes.length;ci++) {");
				ftw.WriteLine("        if (node.childNodes[ci].tagName && node.childNodes[ci].tagName.toLowerCase() == 'span') arrowNode = node.childNodes[ci];");
				ftw.WriteLine("    }");

				ftw.WriteLine("    var td = node.parentNode;");
				ftw.WriteLine("    SORT_INDEX = td.cellIndex;");	// need to remember SORT_INDEX in compare function
				ftw.WriteLine("    var table = sort_getTable(td);");

				ftw.WriteLine("    var sortnext;");
				ftw.WriteLine("    if (arrowNode.getAttribute('sortdir') == 'down') {");
				ftw.WriteLine("        arrow = '&nbsp;&nbsp;&uarr;';");
				ftw.WriteLine("        SORT_DIR = -1;");			// descending SORT_DIR in compare function
				ftw.WriteLine("        sortnext = 'up';");
				ftw.WriteLine("    } else {");
				ftw.WriteLine("        arrow = '&nbsp;&nbsp;&darr;';");
				ftw.WriteLine("        SORT_DIR = 1;");				// ascending SORT_DIR in compare function
				ftw.WriteLine("        sortnext = 'down';");
				ftw.WriteLine("    }");
    
				ftw.WriteLine("    var newRows = new Array();");
				ftw.WriteLine("    for (j=header_rows;j<table.rows.length-footer_rows;j++) { newRows[j-header_rows] = table.rows[j]; }");

				ftw.WriteLine("    newRows.sort(sortfn);");

				// We appendChild rows that already exist to the tbody, so it moves them rather than creating new ones
				ftw.WriteLine("    for (i=0;i<newRows.length;i++) {table.tBodies[0].appendChild(newRows[i]);}");
    
				// Reset all arrows and directions for next time
				ftw.WriteLine("    var spans = document.getElementsByTagName('span');");
				ftw.WriteLine("    for (var ci=0;ci<spans.length;ci++) {");
				ftw.WriteLine("        if (spans[ci].className == 'sortarrow') {");
				// in the same table as us?
				ftw.WriteLine("            if (sort_getTable(spans[ci]) == sort_getTable(node)) {");
				ftw.WriteLine("                spans[ci].innerHTML = '&nbsp;&nbsp;&nbsp;';");
				ftw.WriteLine("                spans[ci].setAttribute('sortdir','up');");
				ftw.WriteLine("            }");
				ftw.WriteLine("        }");
				ftw.WriteLine("    }");
        
				ftw.WriteLine("    arrowNode.innerHTML = arrow;");
				ftw.WriteLine("    arrowNode.setAttribute('sortdir',sortnext);");
				ftw.WriteLine("}");

				ftw.WriteLine("function sort_getTable(el) {");
				ftw.WriteLine("	if (el == null) return null;");
				ftw.WriteLine("	else if (el.nodeType == 1 && el.tagName.toLowerCase() == 'table')");
				ftw.WriteLine("		return el;");
				ftw.WriteLine("	else");
				ftw.WriteLine("		return sort_getTable(el.parentNode);");
				ftw.WriteLine("}");

				ftw.WriteLine("function sort_cmp_date(c1,c2) {");
				ftw.WriteLine("    t1 = sort_getInnerText(c1.cells[SORT_INDEX]);");
				ftw.WriteLine("    t2 = sort_getInnerText(c2.cells[SORT_INDEX]);");
				ftw.WriteLine("    dt1 = new Date(t1);");
				ftw.WriteLine("    dt2 = new Date(t2);");
				ftw.WriteLine("    if (dt1==dt2) return 0;");
				ftw.WriteLine("    if (dt1<dt2) return -SORT_DIR;");
				ftw.WriteLine("    return SORT_DIR;");
				ftw.WriteLine("}");

				// numeric - removes any extraneous formating characters before parsing
				ftw.WriteLine("function sort_cmp_number(c1,c2) {"); 
				ftw.WriteLine("    t1 = sort_getInnerText(c1.cells[SORT_INDEX]).replace(/[^0-9.]/g,'');");
				ftw.WriteLine("    t2 = sort_getInnerText(c2.cells[SORT_INDEX]).replace(/[^0-9.]/g,'');");
				ftw.WriteLine("    n1 = parseFloat(t1);");
				ftw.WriteLine("    n2 = parseFloat(t2);");
				ftw.WriteLine("    if (isNaN(n1)) n1 = Number.MAX_VALUE");
				ftw.WriteLine("    if (isNaN(n2)) n2 = Number.MAX_VALUE");
				ftw.WriteLine("    return (n1 - n2)*SORT_DIR;");
				ftw.WriteLine("}");

				// For string we first do a case insensitive comparison;
				//   when equal we then do a case sensitive comparison
				ftw.WriteLine("function sort_cmp_string(c1,c2) {");
				ftw.WriteLine("    t1 = sort_getInnerText(c1.cells[SORT_INDEX]).toLowerCase();");
				ftw.WriteLine("    t2 = sort_getInnerText(c2.cells[SORT_INDEX]).toLowerCase();");
				ftw.WriteLine("    if (t1==t2) return sort_cmp_casesensitive(c1,c2);");
				ftw.WriteLine("    if (t1<t2) return -SORT_DIR;");
				ftw.WriteLine("    return SORT_DIR;");
				ftw.WriteLine("}");

				ftw.WriteLine("function sort_cmp_casesensitive(c1,c2) {");
				ftw.WriteLine("    t1 = sort_getInnerText(c1.cells[SORT_INDEX]);");
				ftw.WriteLine("    t2 = sort_getInnerText(c2.cells[SORT_INDEX]);");
				ftw.WriteLine("    if (t1==t2) return 0;");
				ftw.WriteLine("    if (t2<t2) return -SORT_DIR;");
				ftw.WriteLine("    return SORT_DIR;");
				ftw.WriteLine("}");
			}

			if (bScriptToggle || bScriptTableSort)
			{
				ftw.WriteLine("</script>");
			}

			return;
		}

		// handle the Action tag
		private string Action(Action a, Row r, string t, string tooltip)
		{
			if (a == null)
				return t;
			
			string result = t;
			if (a.Hyperlink != null)
			{	// Handle a hyperlink
				string url = a.HyperLinkValue(r);
				if (tooltip == null)
					result = String.Format("<a target=\"_top\" href=\"{0}\">{1}</a>", url, t);
				else
					result = String.Format("<a target=\"_top\" href=\"{0}\" title=\"{1}\">{2}</a>", url, tooltip, t);
			}
			else if (a.Drill != null)
			{	// Handle a drill through
				StringBuilder args= new StringBuilder("<a target=\"_top\" href=\"");
				args.Append(a.Drill.ReportName);
				args.Append(".rdl");
				if (a.Drill.DrillthroughParameters != null)
				{
					bool bFirst = true;
					foreach (DrillthroughParameter dtp in a.Drill.DrillthroughParameters.Items)
					{
						if (!dtp.OmitValue(r))
						{
							if (bFirst)
							{	// First parameter - prefixed by '?'
								args.Append('?');
								bFirst = false;
							}
							else
							{	// Subsequant parameters - prefixed by '&'
								args.Append('&');
							}
							args.Append(dtp.Name.Nm);
							args.Append('=');
							args.Append(dtp.ValueValue(r));
						}
					}
				}
				args.Append('"');
				if (tooltip != null)
					args.Append(String.Format(" title=\"{0}\"", tooltip));
				args.Append(">");
				args.Append(t);
				args.Append("</a>");
				result = args.ToString();
			}
			else if (a.BookmarkLink != null)
			{	// Handle a bookmark
				string bm = a.BookmarkLinkValue(r);
				if (tooltip == null)
					result = String.Format("<a href=\"#{0}\">{1}</a>", bm, t);
				else
					result = String.Format("<a href=\"#{0}\" title=\"{1}\">{2}</a>", bm, tooltip, t);
			}

			return result;
		}

		private string Bookmark(string bm, string t)
		{
			if (bm == null)
				return t;

			return String.Format("<div id=\"{0}\">{1}</div>", bm, t);
		}

		// Generate the CSS styles and put them in the header
		private void CssGenerate(TextWriter ftw)
		{
			if (_styles.Count <= 0)
				return;

			ftw.WriteLine("<style type='text/css'>");

			foreach (CssCacheEntry cce in _styles.Values)
			{
				int i = cce.Css.IndexOf('{');
				if (cce.Name.IndexOf('#') >= 0)
					ftw.WriteLine("{0} {1}", cce.Name, cce.Css.Substring(i));
				else
					ftw.WriteLine(".{0} {1}", cce.Name, cce.Css.Substring(i));
			}

			ftw.WriteLine("</style>");
		}

		private string CssAdd(Style s, ReportLink rl, Row row)
		{
			return CssAdd(s, rl, row, false);
		}

		private string CssAdd(Style s, ReportLink rl, Row row, bool bForceRelative)
		{
			string css;
			string prefix = CssPrefix(s, rl);

			if (s != null)
				css = prefix + "{" + CssPosition(rl, row, bForceRelative) + s.GetCSS(row, true) + "}";
			else if (rl is Table || rl is Matrix)
				css = prefix + "{" + CssPosition(rl, row, bForceRelative) + "border-collapse:collapse;)";
			else
				css = prefix + "{" + CssPosition(rl, row, bForceRelative) + "}";

			CssCacheEntry cce = (CssCacheEntry) _styles[css];
			if (cce == null)
			{
				string name = prefix + "css" + cssId++.ToString();
				cce = new CssCacheEntry(css, name);
				_styles.Add(cce.Css, cce);
			}

			int i = cce.Name.IndexOf('#');
			if (i > 0)
				return cce.Name.Substring(i+1);
			else
				return cce.Name;
		}

		private string CssPosition(ReportLink rl,Row row, bool bForceRelative)
		{
			if (!(rl is ReportItem))		// if not a report item then no position
				return "";

			// no positioning within a table
			for (ReportLink p=rl.Parent; p != null; p=p.Parent)
			{
				if (p is TableCell ||			
					p is RowGrouping ||
					p is MatrixCell ||
					p is ColumnGrouping ||
					p is Corner)
					return "";
			}

			// TODO: optimize by putting this into ReportItem and caching result???
			ReportItem ri = (ReportItem) rl;

			StringBuilder sb = new StringBuilder();

			if (ri.Left != null)
			{
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "left: {0}; ", ri.Left.Original);
			}
			if (ri.Width != null)
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "width: {0}; ", ri.Width.Original);
			if (ri.Top != null)
			{
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "top: {0}pt; ", ri.Gap);
			}
			if (ri is List)
			{
				List l = ri as List;
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "height: {0}pt; ", l.HeightOfList(GetGraphics,row));
			}
			else if (ri.Height != null)
				sb.AppendFormat(NumberFormatInfo.InvariantInfo, "height: {0}; ", ri.Height.Original);

			if (sb.Length > 0)
			{
				if (bForceRelative || ri.YParents != null)
					//				if (bForceRelative)
					sb.Insert(0, "position: relative; ");
				else
					sb.Insert(0, "position: absolute; ");
			}

			return sb.ToString();
		}

		private Graphics GetGraphics
		{
			get 
			{
				if (_g == null)
				{
					_bm = new Bitmap(10, 10);
					_g = Graphics.FromImage(_bm);
				}
				return _g;
			}
		}

		private string CssPrefix(Style s, ReportLink rl)
		{
			string cssPrefix=null;
			ReportLink p;

			if (rl is Table || rl is Matrix || rl is Rectangle)
			{
				cssPrefix = "table#";
			}
			else if (rl is Body)
			{
				cssPrefix = "body#";
			}
			else if (rl is Line)
			{
				cssPrefix = "table#";
			}
			else if (rl is List)
			{
				cssPrefix = "";
			}
			else if (rl is Subreport)
			{
				cssPrefix = "";
			}
			else if (rl is Chart)
			{	
				cssPrefix = "";
			}
			if (cssPrefix != null)
				return cssPrefix;

			// now find what the style applies to
			for (p=rl.Parent; p != null; p=p.Parent)
			{
				if (p is TableCell)
				{
					bool bHead = false;
					ReportLink p2;
					for (p2=p.Parent; p2 != null; p2=p2.Parent)
					{
						Type t2 = p2.GetType();
						if (t2 == typeof(Header))
						{
							if (p2.Parent is Table)
								bHead=true;
							break;
						}
					}
					if (bHead)
						cssPrefix = "th#";
					else
						cssPrefix = "td#";
					break;
				}
				else if (p is RowGrouping ||
					p is MatrixCell ||
					p is ColumnGrouping ||
					p is Corner)
				{
					cssPrefix = "td#";
					break;
				}
			}

			return cssPrefix == null? "": cssPrefix;
		}

		public void End()
		{
			string bodyCssId;
			if (r.Body != null)
				bodyCssId = CssAdd(r.Body.Style, r.Body, null);		// add the style for the body
			else
				bodyCssId = null;

			TextWriter ftw = _sg.GetTextWriter();	// the final text writer location

			ftw.WriteLine(@"<html>");

			// handle the <head>: description, javascript and CSS goes here
			ftw.WriteLine("<head>");

			ScriptGenerate(ftw);
			CssGenerate(ftw);

			if (r.Description != null)	// Use description as title if provided
				ftw.WriteLine(string.Format(@"<title>{0}</title>", XmlUtil.XmlAnsi(r.Description)));

			ftw.WriteLine(@"</head>");

			// Always want an HTML body - even if report doesn't have a body stmt
			if (bodyCssId != null)
				ftw.WriteLine(@"<body id='{0}'><table>", bodyCssId);
			else
				ftw.WriteLine("<body><table>");

			ftw.Write(tw.ToString());

			ftw.WriteLine(@"</table></body></html>");

			if (_g != null)
			{
				_g.Dispose();
				_g = null;
			}
			if (_bm != null)
			{
				_bm.Dispose();
				_bm = null;
			}
			return;
		}

		// Body: main container for the report
		public void BodyStart(Body b)
		{
			if (b.ReportItems != null && b.ReportItems.Items.Count > 0)
				tw.WriteLine("<tr><td><div style=\"POSITION: relative; \">");
		}

		public void BodyEnd(Body b)
		{
			if (b.ReportItems != null && b.ReportItems.Items.Count > 0)
				tw.WriteLine("</div></td></tr>");
		}
		
		public void PageHeaderStart(PageHeader ph)
		{
			if (ph.ReportItems != null && ph.ReportItems.Items.Count > 0)
				tw.WriteLine("<tr><td><div style=\"overflow: clip; POSITION: relative; HEIGHT: {0};\">", ph.Height.Original);
		}

		public void PageHeaderEnd(PageHeader ph)
		{
			if (ph.ReportItems != null && ph.ReportItems.Items.Count > 0)
				tw.WriteLine("</div></td></tr>");
		}
		
		public void PageFooterStart(PageFooter pf)
		{
			if (pf.ReportItems != null && pf.ReportItems.Items.Count > 0)
				tw.WriteLine("<tr><td><div style=\"overflow: clip; POSITION: static; HEIGHT: {0};\">", pf.Height.Original);
		}

		public void PageFooterEnd(PageFooter pf)
		{
			if (pf.ReportItems != null && pf.ReportItems.Items.Count > 0)
				tw.WriteLine("</div></td></tr>");
		}

		public void Textbox(Textbox tb, string t, Row row)
		{
			// make all the characters browser readable
			t = XmlUtil.XmlAnsi(t);

			// handle any specified bookmark
			t = Bookmark(tb.BookmarkValue(row), t);

			// handle any specified actions
			t = Action(tb.Action, row, t, tb.ToolTipValue(row));
			
			// determine if we're in a tablecell
			Type tp = tb.Parent.Parent.GetType();
			bool bCell;
			if (tp == typeof(TableCell) ||
				tp == typeof(Corner) ||
				tp == typeof(DynamicColumns) ||
				tp == typeof(DynamicRows) ||
				tp == typeof(StaticRow) ||
				tp == typeof(Subtotal) ||
				tp == typeof(MatrixCell))
				bCell = true;
			else
				bCell = false;

			if (tp == typeof(Rectangle))
				tw.Write("<td>");

			if (bCell)
			{	// The cell has the formatting for this text
				if (t == "")
					tw.Write("<br />");		// must have something in cell for formating
				else
					tw.Write(t);
			}
			else
			{	// Formatting must be specified
				string cssName = CssAdd(tb.Style, tb, row);	// get the style name for this item

				tw.Write("<div class='{0}'>{1}</div>", cssName, t);
			}

			if (tp == typeof(Rectangle))
				tw.Write("</td>");
		}

		public void DataRegionNoRows(DataRegion d, string noRowsMsg)			// no rows in table
		{
			if (noRowsMsg == null)
				noRowsMsg = "";

			bool bTableCell = d.Parent.Parent.GetType() == typeof(TableCell);

			if (bTableCell)
			{
				if (noRowsMsg == "")
					tw.Write("<br />");
				else
					tw.Write(noRowsMsg);
			}
			else
			{
				string cssName = CssAdd(d.Style, d, null);	// get the style name for this item
				tw.Write("<div class='{0}'>{1}</div>", cssName, noRowsMsg);
			}
		}

		// Lists
		public bool ListStart(List l, Row r)
		{
			// identifiy reportitem it if necessary
			string bookmark = l.BookmarkValue(r);
			if (bookmark != null)	// 
				tw.WriteLine("<div id=\"{0}\">", bookmark);		// can't use the table id since we're using for css style
			return true;
		}

		public void ListEnd(List l, Row r)
		{
			string bookmark = l.BookmarkValue(r);
			if (bookmark != null)
				tw.WriteLine("</div>"); 
		}

		public void ListEntryBegin(List l, Row r)
		{
			string cssName = CssAdd(l.Style, l, r, true);	// get the style name for this item; force to be relative
			tw.WriteLine();
			tw.WriteLine("<div class={0}>", cssName);
		}

		public void ListEntryEnd(List l, Row r)
		{
			tw.WriteLine();
			tw.WriteLine("</div>");
		}

		// Tables					// Report item table
		public bool TableStart(Table t, Row row)
		{
			string cssName = CssAdd(t.Style, t, row);	// get the style name for this item

			// Determine if report custom defn want this table to be sortable
			if (IsTableSortable(t))
			{
				this.bScriptTableSort = true;
			}

			string bookmark = t.BookmarkValue(row);
			if (bookmark != null)
				tw.WriteLine("<div id=\"{0}\">", bookmark);		// can't use the table id since we're using for css style

			// Calculate the width of all the columns
			int width = t.WidthInPixels;
			if (width < 0)
				tw.WriteLine("<table id='{0}'>", cssName);
			else
				tw.WriteLine("<table id='{0}' width={1}>", cssName, width);

			return true;
		}

		public bool IsTableSortable(Table t)
		{
			if (t.TableGroups != null || t.Details == null || 
				t.Details.TableRows == null || t.Details.TableRows.Items.Count != 1)		
				return false;	// can't have tableGroups; must have 1 detail row

			// Determine if report custom defn want this table to be sortable
			bool bReturn = false;
			if (t.Custom != null)
			{
				// Loop thru all the child nodes
				foreach(XmlNode xNodeLoop in t.Custom.CustomXmlNode.ChildNodes)
				{
					if (xNodeLoop.Name == "HTML")
					{
						if (xNodeLoop.LastChild.InnerText.ToLower() == "true")
						{
							bReturn = true;
						}
						break;
					}
				}
			}
			return bReturn;
		}

		public void TableEnd(Table t, Row row)
		{
			string bookmark = t.BookmarkValue(row);
			if (bookmark != null)
				tw.WriteLine("</div>"); 
			tw.WriteLine("</table>");
			return;
		}
 
		public void TableBodyStart(Table t, Row row)
		{
			tw.WriteLine("<tbody>");
		}

		public void TableBodyEnd(Table t, Row row)
		{
			tw.WriteLine("</tbody>");
		}

		public void TableFooterStart(Footer f, Row row)
		{
			tw.WriteLine("<tfoot>");
		}

		public void TableFooterEnd(Footer f, Row row)
		{
			tw.WriteLine("</tfoot>");
		}

		public void TableHeaderStart(Header h, Row row)
		{
			tw.WriteLine("<thead>");
		}

		public void TableHeaderEnd(Header h, Row row)
		{
			tw.WriteLine("</thead>");
		}

		public void TableRowStart(TableRow tr, Row row)
		{
			tw.Write("\t<tr");
			ReportLink rl = tr.Parent.Parent;
			Visibility v=null;
			Textbox togText=null;		// holds the toggle text box if any
			if (rl is Details)
			{
				Details d = (Details) rl;
				v = d.Visibility;
				togText = d.ToggleTextbox;
			}
			else if (rl.Parent is TableGroup)
			{
				TableGroup tg = (TableGroup) rl.Parent;
				v = tg.Visibility;
				togText = tg.ToggleTextbox;
			}

			if (v != null &&
				v.Hidden != null)
			{
				bool bHide = v.Hidden.EvaluateBoolean(row);
				if (bHide)
					tw.Write(" style=\"display:none;\"");
			}

			if (togText != null && togText.Name != null)
			{
				string name = togText.Name.Nm + "_" + togText.RunCount.ToString();
				tw.Write(" id='{0}'", name);
			}

			tw.Write(">");
		}

		public void TableRowEnd(TableRow tr, Row row)
		{
			tw.WriteLine("</tr>");
		}

		public void TableCellStart(TableCell t, Row row)
		{
			string cellType = t.InTableHeader? "th": "td";

			ReportItem r = (ReportItem) t.ReportItems.Items[0];

			string cssName = CssAdd(r.Style, r, row);	// get the style name for this item

			tw.Write("<{0} id='{1}'", cellType, cssName);

			// calculate width of column
			if (t.InTableHeader && t.OwnerTable.TableColumns != null)
			{
				TableColumn tc = t.OwnerTable.TableColumns.Items[t.ColIndex] as TableColumn;
				if (tc != null && tc.Width != null)
					tw.Write(" width={0}", tc.Width.PixelsX);
			}

			if (t.ColSpan > 1)
				tw.Write(" colspan={0}", t.ColSpan);

			Textbox tb = r as Textbox;
			if (tb != null &&				// have textbox
				tb.IsToggle &&				//   and its a toggle
				tb.Name != null)			//   and need name as well
			{
				if (t.OwnerTable.GroupNestCount > 0) // anything to toggle?
				{
					string name = tb.Name.Nm + "_" + (tb.RunCount+1).ToString();
					bScriptToggle = true;

					// need both hand and pointer because IE and Firefox use different names
					tw.Write(" onClick=\"hideShow(this, {0}, '{1}')\" onMouseOver=\"style.cursor ='hand';style.cursor ='pointer'\">", t.OwnerTable.GroupNestCount, name);
					tw.Write("<img class='toggle' src=\"plus.gif\" align=top/>");
				}
				else
					tw.Write("<img src=\"empty.gif\" align=top/>");
			}
			else
				tw.Write(">");

			if (t.InTableHeader)
			{	
				// put the second half of the sort tags for the column; if needed
				// first half ---- <a href="#" onclick="sort_table(this,sort_cmp_string,1,0);return false;">
				// next half follows text  ---- <span class="sortarrow">&nbsp;&nbsp;&nbsp;</span></a></th>

				string sortcmp = SortType(t, tb);	// obtain the sort type
				if (sortcmp != null)				// null if sort not needed
				{
					int headerRows, footerRows;
					headerRows = t.OwnerTable.Header.TableRows.Items.Count;	// since we're in header we know we have some rows
					if (t.OwnerTable.Footer != null &&
						t.OwnerTable.Footer.TableRows != null)
						footerRows = t.OwnerTable.Footer.TableRows.Items.Count;
					else
						footerRows = 0;
					tw.Write("<a href=\"#\" title='Sort' onclick=\"sort_table(this,{0},{1},{2});return false;\">",sortcmp, headerRows, footerRows);
				}
			}

			return;
		}

		private string SortType(TableCell tc, Textbox tb)
		{
			// return of null means don't sort
			if (tb == null || !IsTableSortable(tc.OwnerTable))
				return null;

			// default is true if table is sortable;
			//   but user may place override on Textbox custom tag
			if (tb.Custom != null)
			{
				// Loop thru all the child nodes
				foreach(XmlNode xNodeLoop in tb.Custom.CustomXmlNode.ChildNodes)
				{
					if (xNodeLoop.Name == "HTML")
					{
						if (xNodeLoop.LastChild.InnerText.ToLower() == "false")
						{
							return null;
						}
						break;
					}
				}
			}

			// Must find out the type of the detail column
			Details d = tc.OwnerTable.Details;
			if (d == null)
				return null;
			TableRow tr = d.TableRows.Items[0] as TableRow;
			if (tr == null)
				return null;
			TableCell dtc = tr.TableCells.Items[tc.ColIndex] as TableCell;
			if (dtc == null)
				return null;
			Textbox dtb = dtc.ReportItems.Items[0] as Textbox;
			if (dtb == null)
				return null;

			string sortcmp;
			switch (dtb.Value.Type)
			{
				case TypeCode.DateTime:
					sortcmp = "sort_cmp_date";
					break;
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Decimal:
				case TypeCode.Single:
				case TypeCode.Double:
					sortcmp = "sort_cmp_number";
					break;
				case TypeCode.String:
					sortcmp = "sort_cmp_string";
					break;
				case TypeCode.Empty:	// Not a type we know how to sort
				default:		
					sortcmp = null;
					break;
			}

			return sortcmp;
		}

		public void TableCellEnd(TableCell t, Row row)
		{
			string cellType = t.InTableHeader? "th": "td";
			Textbox tb = t.ReportItems.Items[0] as Textbox;
			if (cellType == "th" && SortType(t, tb) != null)
			{	// put the second half of the sort tags for the column
				// first half ---- <a href="#" onclick="sort_table(this,sort_cmp_string,1,0);return false;">
				// next half follows text  ---- <span class="sortarrow">&nbsp;&nbsp;&nbsp;</span></a></th>
				tw.Write("<span class=\"sortarrow\">&nbsp;&nbsp;&nbsp;</span></a>");
			}

			tw.Write("</{0}>", cellType);
			return;
		}

		public bool MatrixStart(Matrix m, Row r)				// called first
		{
			string bookmark = m.BookmarkValue(r);
			if (bookmark != null)
				tw.WriteLine("<div id=\"{0}\">", bookmark);		// can't use the table id since we're using for css style

			// output some of the table styles
			string cssName = CssAdd(m.Style, m, r);	// get the style name for this item

			tw.WriteLine("<table id='{0}'>", cssName);
			return true;
		}

		public void MatrixColumns(Matrix m, MatrixColumns mc)	// called just after MatrixStart
		{
		}

		public void MatrixCellStart(Matrix m, ReportItem ri, int row, int column, Row r)
		{
			if (ri == null)			// Empty cell?
			{
				tw.Write("<td>");
				return;
			}

			string cssName = CssAdd(ri.Style, ri, r);	// get the style name for this item

			tw.Write("<td id='{0}'", cssName);

			if (ri is Textbox)
			{
				Textbox tb = (Textbox) ri;
				if (tb.IsToggle && tb.Name != null)		// name is required for this
				{
					string name = tb.Name.Nm + "_" + (tb.RunCount+1).ToString();

					bScriptToggle = true;	// we need to generate JavaScript in header
					// TODO -- need to calculate the hide count correctly
					tw.Write(" onClick=\"hideShow(this, {0}, '{1}')\" onMouseOver=\"style.cursor ='hand'\"", 0, name);
				}
			}

			tw.Write(">");
		}

		public void MatrixCellEnd(Matrix m, ReportItem ri, int row, int column, Row r)
		{
			tw.Write("</td>");
			return;
		}

		public void MatrixRowStart(Matrix m, int row, Row r)
		{
			tw.Write("\t<tr");

			tw.Write(">");
		}

		public void MatrixRowEnd(Matrix m, int row, Row r)
		{
			tw.WriteLine("</tr>");
		}

		public void MatrixEnd(Matrix m, Row r)				// called last
		{
			tw.Write("</table>");

			string bookmark = m.BookmarkValue(r);
			if (bookmark != null)
				tw.WriteLine("</div>");		
			return;
		}

		public void Chart(Chart c, Row r, ChartBase cb)
		{
			string relativeName;

			Stream io = _sg.GetIOStream(out relativeName, "png");
			try
			{
				cb.Save(io, ImageFormat.Png);
			}
			finally
			{
				io.Flush();
				io.Close();
			}
		
			if (relativeName[0] != Path.DirectorySeparatorChar)
				relativeName = Path.DirectorySeparatorChar + relativeName;

			// Create syntax in a string buffer
			StringWriter sw = new StringWriter();

			string bookmark = c.BookmarkValue(r);
			if (bookmark != null)
				sw.WriteLine("<div id=\"{0}\">", bookmark);		// can't use the table id since we're using for css style

			string cssName = CssAdd(c.Style, c, null);	// get the style name for this item

			sw.Write("<img src=\"{0}\" class='{1}'", relativeName, cssName);
			string tooltip = c.ToolTipValue(r);
			if (tooltip != null)
				sw.Write(" alt=\"{0}\"", tooltip);
			if (c.Height != null)
				sw.Write(" height=\"{0}\"", c.Height.PixelsY.ToString());
			if (c.Width != null)
				sw.Write(" width=\"{0}\"", c.Width.PixelsX.ToString());
			sw.Write(">");
			if (bookmark != null)
				sw.Write("</div>");

			tw.Write(Action(c.Action, r, sw.ToString(), tooltip));
			
			return;
		}

		public void Image(Image i, Row r, string mimeType, Stream ioin)
		{
			string relativeName;
			string suffix;

			switch (mimeType)
			{
				case "image/bmp":
					suffix = "bmp";
					break;
				case "image/jpeg":
					suffix = "jpeg";
					break;
				case "image/gif":
					suffix = "gif";
					break;
				case "image/png":
				case "image/x-png":
					suffix = "png";
					break;
				default:
					suffix = "unk";
					break;
			}
			Stream io = _sg.GetIOStream(out relativeName, suffix);
			try
			{
				byte[] ba = new byte[ioin.Length];
				ioin.Read(ba, 0, ba.Length);
				io.Write(ba, 0, ba.Length);
			}
			finally
			{
				io.Flush();
				io.Close();
			}
			if (relativeName[0] != Path.DirectorySeparatorChar)
				relativeName = Path.DirectorySeparatorChar + relativeName;
			
			// Create syntax in a string buffer
			StringWriter sw = new StringWriter();

			string bookmark = i.BookmarkValue(r);
			if (bookmark != null)
				sw.WriteLine("<div id=\"{0}\">", bookmark);		// we're using for css style

			string cssName = CssAdd(i.Style, i, null);	// get the style name for this item

			sw.Write("<img src=\"{0}\" class='{1}'", relativeName, cssName);

			string tooltip = i.ToolTipValue(r);
			if (tooltip != null)
				sw.Write(" alt=\"{0}\"", tooltip);
			if (i.Height != null)
				sw.Write(" height=\"{0}\"", i.Height.PixelsY.ToString());
			if (i.Width != null)
				sw.Write(" width=\"{0}\"", i.Width.PixelsX.ToString());
			sw.Write("/>");

			if (bookmark != null)
				sw.Write("</div>");

			tw.Write(Action(i.Action, r, sw.ToString(), tooltip));
			return;
		}

		public void Line(Line l, Row r)
		{
			bool bVertical;
			string t;
			if (l.Height == null || l.Height.PixelsY > 0)	// only handle horizontal rule
			{
				if (l.Width == null || l.Width.PixelsX > 0)	//    and vertical rules
					return;
				bVertical = true;
				t = "<TABLE style=\"border-collapse:collapse;BORDER-STYLE: none;WIDTH: {0}; POSITION: absolute; LEFT: {1}; TOP: {2}; HEIGHT: {3}; BACKGROUND-COLOR:{4};\"><TBODY><TR style=\"WIDTH:{0}\"><TD style=\"WIDTH:{0}\"></TD></TR></TBODY></TABLE>";
			}
			else
			{
				bVertical = false;			
				t = "<TABLE style=\"border-collapse:collapse;BORDER-STYLE: none;WIDTH: {0}; POSITION: absolute; LEFT: {1}; TOP: {2}; HEIGHT: {3}; BACKGROUND-COLOR:{4};\"><TBODY><TR style=\"HEIGHT:{3}\"><TD style=\"HEIGHT:{3}\"></TD></TR></TBODY></TABLE>";
			}

			string width, left, top, height, color;
			Style s = l.Style;

			left = l.Left == null? "0px": l.Left.Original;
			top = l.Top == null? "0px": l.Top.Original;

			if (bVertical)
			{
				height = l.Height == null? "0px": l.Height.Original;
				// width comes from the BorderWidth
				if (s != null && s.BorderWidth != null && s.BorderWidth.Default != null)
					width = s.BorderWidth.Default.EvaluateString(r);
				else
					width = "1px";
			}
			else
			{
				width = l.Width == null? "0px": l.Width.Original;
				// height comes from the BorderWidth
				if (s != null && s.BorderWidth != null && s.BorderWidth.Default != null)
					height = s.BorderWidth.Default.EvaluateString(r);
				else
					height = "1px";
			}

			if (s != null && s.BorderColor != null && s.BorderColor.Default != null)
				color = s.BorderColor.Default.EvaluateString(r);
			else
				color = "black";
			
			tw.WriteLine(t, width, left, top, height, color);
			return;
		}

		public bool RectangleStart(RDL.Rectangle rect, Row r)
		{
			string cssName = CssAdd(rect.Style, rect, r);	// get the style name for this item

			string bookmark = rect.BookmarkValue(r);
			if (bookmark != null)
				tw.WriteLine("<div id=\"{0}\">", bookmark);		// can't use the table id since we're using for css style

			// Calculate the width of all the columns
			int width = rect.Width.PixelsX;
			if (width < 0)
				tw.WriteLine("<table id='{0}'><tr>", cssName);
			else
				tw.WriteLine("<table id='{0}' width={1}><tr>", cssName, width);

			return true;
		}

		public void RectangleEnd(RDL.Rectangle rect, Row r)
		{
			tw.WriteLine("</tr></table>");
			string bookmark = rect.BookmarkValue(r);
			if (bookmark != null)
				tw.WriteLine("</div>"); 
			return;
		}

		// Subreport:  
		public void Subreport(Subreport s, Row r)
		{
			string cssName = CssAdd(s.Style, s, r);	// get the style name for this item

			tw.WriteLine("<div class='{0}'>", cssName);

			if (s.ReportErrors == null)
				s.Report.Run(this);
			else
			{
				foreach (string err in s.ReportErrors)
				{	// Just put the errors out into the string
					tw.Write("<p>"+ err);
				}
			}
			
			tw.WriteLine("</div>");
		}
		public void GroupingStart(Grouping g)			// called at start of grouping
		{
		}
		public void GroupingInstanceStart(Grouping g)	// called at start for each grouping instance
		{
		}
		public void GroupingInstanceEnd(Grouping g)	// called at start for each grouping instance
		{
		}
		public void GroupingEnd(Grouping g)			// called at end of grouping
		{
		}
		public void RunPages(Pages pgs)	// we don't have paging turned on for html
		{
		}
	}
	

	class CssCacheEntry
	{
		string _Css;					// css 
		string _Name;					// name of entry

		public CssCacheEntry(string css, string name)
		{
			_Css = css;
			_Name = name;				
		}

		public string Css
		{
			get { return  _Css; }
			set { _Css = value; }
		}

		public string Name
		{
			get { return  _Name; }
			set { _Name = value; }
		}
	}
}