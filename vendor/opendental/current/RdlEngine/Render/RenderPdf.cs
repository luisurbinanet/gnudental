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
using System.Drawing;


namespace fyiReporting.RDL
{
	
	///<summary>
	/// Renders a report to PDF.   This is a page oriented formatting renderer.
	///</summary>
	internal class RenderPdf: IPresent
	{
		Report r;					// report
		Stream tw;					// where the output is going
		PdfAnchor anchor;			// anchor tieing together all pdf objects
		PdfCatalog catalog;
		PdfPageTree pageTree;
		PdfInfo info;
		PdfFonts fonts;
		PdfImages images;
		PdfUtility pdfUtility;
		int filesize;
		PdfPage page;
		PdfContent content;
		PdfElements elements;
		readonly char[] lineBreak = new char[] {' ', '\r', '\n'};

		public RenderPdf(Report rep, IStreamGen sg)
		{
			r = rep;
			tw = sg.GetStream();
		}

		public bool IsPagingNeeded()
		{
			return true;
		}

		public void Start()		
		{
			// Create the anchor for all pdf objects
			anchor = new PdfAnchor();

			//Create a PdfCatalog
			string lang;
			if (r.Language != null)
				lang = r.Language.EvaluateString(null);
			else
				lang = null;
			catalog= new PdfCatalog(anchor, lang);

			//Create a Page Tree Dictionary
			pageTree= new PdfPageTree(anchor);

			//Create a Font Dictionary
			fonts = new PdfFonts(anchor);

			//Create an Image Dictionary
			images = new PdfImages(anchor);

			//Create the info Dictionary
			info=new PdfInfo(anchor);

			//Set the info Dictionary. 
			info.SetInfo(r.Name,r.Author,r.Description,"");	// title, author, subject, company

			//Create a utility object
			pdfUtility=new PdfUtility(anchor);

			//write out the header
			int size=0;
			tw.Write(pdfUtility.GetHeader("1.5",out size),0,size);
			filesize = size;
		}

		public void End()
		{
			//Write everything 
			int size=0;
			tw.Write(catalog.GetCatalogDict(pageTree.objectNum, 
				filesize,out size),0,size);
			filesize += size;
			tw.Write(pageTree.GetPageTree(filesize,out size),0,size);
			filesize += size;
			tw.Write(fonts.GetFontDict(filesize,out size),0,size);
			filesize += size;
			if (images.Images.Count > 0)
			{
				tw.Write(images.GetImageDict(filesize,out size),0,size);
				filesize += size;
			}
			tw.Write(info.GetInfoDict(filesize,out size),0,size);
			filesize += size;
			tw.Write(pdfUtility.CreateXrefTable(filesize,out size),0,size);
			filesize += size;
			tw.Write(pdfUtility.GetTrailer(catalog.objectNum,
				info.objectNum,out size),0,size);
			filesize += size;
			return;
		}

		public void RunPages(Pages pgs)	// this does all the work
		{	  
			foreach (Page p in pgs)
			{
				//Create a Page Dictionary representing a visible page
				page=new PdfPage(anchor);
				content=new PdfContent(anchor);

				PdfPageSize pSize=new PdfPageSize((int) r.PageWidth.Points, (int) r.PageHeight.Points);
				page.CreatePage(pageTree.objectNum,pSize);
				pageTree.AddPage(page.objectNum);

				//Create object that presents the elements in the page
				elements=new PdfElements(page, pSize);

				ProcessPage(pgs, p);

				// after a page
				content.SetStream(elements.EndElements());

				page.AddResource(fonts,content.objectNum);

				int size=0;
				tw.Write(page.GetPageDict(filesize,out size),0,size);
				filesize += size;

				tw.Write(content.GetContentDict(filesize,out size),0,size);
				filesize += size;
			}
			return;
		}
		// render all the objects in a page in PDF
		private void ProcessPage(Pages pgs, Page p)
		{
			foreach (PageItem pi in p)
			{
				if (pi.SI.BackgroundImage != null)
				{	// put out any background image
					PageImage i = pi.SI.BackgroundImage;
					elements.AddImage(images, i.Name, content.objectNum, i.SI, i.ImgFormat, 
						pi.X, pi.Y, pi.W, pi.H, i.ImageData,i.SamplesW, i.SamplesH);
				}

				if (pi is PageText)
				{
					PageText pt = pi as PageText;
					float[] textwidth;
					string[] sa = MeasureString(pt, pgs.G, out textwidth);
					elements.AddText(pt.X, pt.Y, pt.H, pt.W, sa, pt.SI, fonts, textwidth, pt.CanGrow);
					continue;
				}

				if (pi is PageLine)
				{
					PageLine pl = pi as PageLine;
					elements.AddLine(pl.X, pl.Y, pl.X2, pl.Y2, pl.SI);
					continue;
				}

				if (pi is PageImage)
				{
					PageImage i = pi as PageImage;
					elements.AddImage(images, i.Name, content.objectNum, i.SI, i.ImgFormat, 
						i.X, i.Y, i.W, i.H, i.ImageData,i.SamplesW, i.SamplesH);
					continue;
				}

				if (pi is PageRectangle)
				{
					PageRectangle pr = pi as PageRectangle;
					elements.AddRectangle(pr.X, pr.Y, pr.H, pr.W, pi.SI);
					continue;
				}
			}
		}

		private string[] MeasureString(PageText pt, Graphics g, out float[] width)
		{
			StyleInfo si = pt.SI;
			string s = pt.Text;

			Font drawFont=null;
			StringFormat drawFormat=null;
			SizeF ms;
			string[] sa=null;
			width=null;
			try
			{
				// STYLE
				System.Drawing.FontStyle fs = 0;
				if (si.FontStyle == FontStyleEnum.Italic)
					fs |= System.Drawing.FontStyle.Italic;

				// WEIGHT
				switch (si.FontWeight)
				{
					case FontWeightEnum.Bold:
					case FontWeightEnum.Bolder:
					case FontWeightEnum.W500:
					case FontWeightEnum.W600:
					case FontWeightEnum.W700:
					case FontWeightEnum.W800:
					case FontWeightEnum.W900:
						fs |= System.Drawing.FontStyle.Bold;
						break;
					default:
						break;
				}
				drawFont = new Font(si.FontFamily, si.FontSize, fs);
				drawFormat = new StringFormat();
				drawFormat.Alignment = StringAlignment.Near;

				// Measure string
				if (!pt.CanGrow || pt.SI.WritingMode == WritingModeEnum.tb_rl)	// a single line;; TODO: support multiple lines for vertical text
				{
					ms = g.MeasureString(s, drawFont, int.MaxValue, drawFormat);
					width = new float[1];
					width[0] = ms.Width / g.DpiX * 72.0f;	// convert to points from pixels
					sa = new string[1];
					sa[0] = s;
					return sa;
				}

				// handle multiple lines
				string[] parts = s.Split(lineBreak);	// this is the maximum split of lines; TODO: more sophisticated line breaks 
				float[] partWidths = new float[parts.Length];
				for (int i=0; i < parts.Length; i++)
				{
					ms = g.MeasureString(parts[i], drawFont, int.MaxValue, drawFormat);
					partWidths[i] = ms.Width / g.DpiX * 72.0f;	// convert to points from pixels
				}

				// now combine the lines
				ArrayList lines = new ArrayList();
				ArrayList lineWidths = new ArrayList();
				float cwidth=partWidths[0];
				string cstring=parts[0];
				for (int i=1; i < parts.Length; i++)
				{
					if (cwidth + partWidths[i] > pt.W)
					{	// time for a new line
						lines.Add(cstring);
						lineWidths.Add(cwidth);
						cstring=null;
						cwidth=0;
					}
					if (cstring == null)
						cstring = parts[i];
					else
						cstring += (" " + parts[i]);	// TODO: really need to account for width of blank
					cwidth += partWidths[i];
				}
				lines.Add(cstring);
				lineWidths.Add(cwidth);
				string[] la = new string[lines.Count];
				width = new float[lineWidths.Count];
				for (int i=0; i < lineWidths.Count; i++)
				{
					la[i] = (string) lines[i];
					width[i] = (float) lineWidths[i];
				}
				return la;
			}
			finally
			{
				if (drawFont != null)
					drawFont.Dispose();
				if (drawFormat != null)
					drawFont.Dispose();
			}
		}

		// Body: main container for the report
		public void BodyStart(Body b)
		{
		}

		public void BodyEnd(Body b)
		{
		}
 		
		public void PageHeaderStart(PageHeader ph)
		{
		}

		public void PageHeaderEnd(PageHeader ph)
		{
		}
 		
		public void PageFooterStart(PageFooter pf)
		{
		}

		public void PageFooterEnd(PageFooter pf)
		{
		}

		public void Textbox(Textbox tb, string t, Row row)
		{
		}

		public void DataRegionNoRows(DataRegion d, string noRowsMsg)
		{
		}

		// Lists
		public bool ListStart(List l, Row r)
		{
			return true;
		}

		public void ListEnd(List l, Row r)
		{
		}

		public void ListEntryBegin(List l, Row r)
		{
		}

		public void ListEntryEnd(List l, Row r)
		{
		}

		// Tables					// Report item table
		public bool TableStart(Table t, Row row)
		{
			return true;
		}

		public void TableEnd(Table t, Row row)
		{
		}

		public void TableBodyStart(Table t, Row row)
		{
		}

		public void TableBodyEnd(Table t, Row row)
		{
		}

		public void TableFooterStart(Footer f, Row row)
		{
		}

		public void TableFooterEnd(Footer f, Row row)
		{
		}

		public void TableHeaderStart(Header h, Row row)
		{
		}

		public void TableHeaderEnd(Header h, Row row)
		{
		}

		public void TableRowStart(TableRow tr, Row row)
		{
		}

		public void TableRowEnd(TableRow tr, Row row)
		{
		}

		public void TableCellStart(TableCell t, Row row)
		{
			return;
		}

		public void TableCellEnd(TableCell t, Row row)
		{
			return;
		}

		public bool MatrixStart(Matrix m, Row r)				// called first
		{
			return true;
		}

		public void MatrixColumns(Matrix m, MatrixColumns mc)	// called just after MatrixStart
		{
		}

		public void MatrixCellStart(Matrix m, ReportItem ri, int row, int column, Row r)
		{
		}

		public void MatrixCellEnd(Matrix m, ReportItem ri, int row, int column, Row r)
		{
		}

		public void MatrixRowStart(Matrix m, int row, Row r)
		{
		}

		public void MatrixRowEnd(Matrix m, int row, Row r)
		{
		}

		public void MatrixEnd(Matrix m, Row r)				// called last
		{
		}

		public void Chart(Chart c, Row r, ChartBase cb)
		{
		}

		public void Image(fyiReporting.RDL.Image i, Row r, string mimeType, Stream ior)
		{
		}
 
		public void Line(Line l, Row r)
		{
			return;
		}

		public bool RectangleStart(RDL.Rectangle rect, Row r)
		{
			return true;
		}

		public void RectangleEnd(RDL.Rectangle rect, Row r)
		{
		}

		public void Subreport(Subreport s, Row r)
		{
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
	}
}
