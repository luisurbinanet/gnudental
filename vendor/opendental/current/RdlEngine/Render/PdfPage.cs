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
using System.Diagnostics;

namespace fyiReporting.RDL
{
	/// <summary>
	/// The PageTree object contains references to all the pages used within the Pdf.
	/// All individual pages are referenced through the Kids arraylist
	/// </summary>
	internal class PdfPageTree:PdfBase
	{
		private string pageTree;
		private string kids;
		private static int MaxPages;
		
		internal PdfPageTree(PdfAnchor pa):base(pa)
		{
			kids="[ "; 
			MaxPages=0;
		}
		/// <summary>
		/// Add a page to the Page Tree. ObjNum is the object number of the page to be added.
		/// pageNum is the page number of the page.
		/// </summary>
		/// <param name="objNum"></param>
		/// <param name="pageNum"></param>
		internal void AddPage(int objNum)
		{
			Debug.Assert(objNum >= 0 && objNum <= this.Current);
			MaxPages++;
			string refPage=objNum+" 0 R ";
			kids=kids+refPage;
		}
		/// <summary>
		/// returns the Page Tree Dictionary
		/// </summary>
		/// <returns></returns>
		internal byte[] GetPageTree(long filePos,out int size)
		{
			pageTree=string.Format("\r\n{0} 0 obj<</Count {1}/Kids {2}]>> endobj\t",
				this.objectNum,MaxPages,kids);
			return this.GetUTF8Bytes(pageTree,filePos,out size);
		}
	}
	/// <summary>
	/// This class represents individual pages within the pdf. 
	/// The contents of the page belong to this class
	/// </summary>
	internal class PdfPage:PdfBase
	{
		private string page;
		private string pageSize;
		private string fontRef;
		private string imageRef;
		private string resourceDict,contents;
		internal PdfPage(PdfAnchor pa):base(pa)
		{
			resourceDict=null;
			contents=null;
			pageSize=null;
			fontRef=null;
			imageRef=null;
		}
		/// <summary>
		/// Create The Pdf page
		/// </summary>
		/// <param name="refParent"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		internal void CreatePage(int refParent,PdfPageSize pSize)
		{
			pageSize=string.Format("[0 0 {0} {1}]",pSize.xWidth,pSize.yHeight);
			page=string.Format("\r\n{0} 0 obj<</Type /Page/Parent {1} 0 R/Rotate 0/MediaBox {2}/CropBox {2}",
				this.objectNum,refParent,pageSize);
		}
		/// <summary>
		/// Add Font Resources to the pdf page
		/// </summary>
		/// <param name="font"></param>
		internal void AddResource(PdfFonts fonts,int contentRef)
		{
			foreach (PdfFontEntry font in fonts.Fonts.Values)
			{
				fontRef+=string.Format("/{0} {1} 0 R",font.font,font.objectNum);
			}
			if(contentRef>0)
			{
				contents=string.Format("/Contents {0} 0 R",contentRef);
			}
		}
		/// <summary>
		/// Add Image Resources to the pdf page
		/// </summary>
		internal void AddResource(PdfImageEntry ie,int contentRef)
		{
			if (imageRef == null || imageRef.IndexOf("/"+ie.name) < 0)	// only need it once per page
//				imageRef+=string.Format("/XObject << /{0} {1} 0 R >>",ie.name,ie.objectNum);
				imageRef+=string.Format("/{0} {1} 0 R ",ie.name,ie.objectNum);
			if(contentRef>0)
			{
				contents=string.Format("/Contents {0} 0 R",contentRef);
			}
		}
		/// <summary>
		/// Get the Page Dictionary to be written to the file
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		internal byte[] GetPageDict(long filePos,out int size)
		{
			if (imageRef == null)
				resourceDict=string.Format("/Resources<</Font<<{0}>>/ProcSet[/PDF/Text]>>",fontRef);
			else
				resourceDict=string.Format("/Resources<</Font<<{0}>>/ProcSet[/PDF/Text/ImageB]/XObject <<{1}>>>>",fontRef, imageRef);

			page+=resourceDict+contents+">>\r\nendobj\r\n";
			return this.GetUTF8Bytes(page,filePos,out size);
		}
	}
	/// <summary>
	/// Specify the page size in 1/72 inches units.
	/// </summary>
	internal struct PdfPageSize
	{
		internal int xWidth;
		internal int yHeight;
		internal int leftMargin;
		internal int rightMargin;
		internal int topMargin;
		internal int bottomMargin;

		internal PdfPageSize(int width,int height)
		{
			xWidth=width;
			yHeight=height;
			leftMargin=0;
			rightMargin=0;
			topMargin=0;
			bottomMargin=0;
		}
		internal void SetMargins(int L,int T,int R,int B)
		{
			leftMargin=L;
			rightMargin=R;
			topMargin=T;
			bottomMargin=B;
		}
	}

}
