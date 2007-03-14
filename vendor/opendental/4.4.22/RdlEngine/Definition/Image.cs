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
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.Collections.Specialized;


namespace fyiReporting.RDL
{
	///<summary>
	/// Represents an image.  Source of image can from database, external or embedded. 
	///</summary>
	[Serializable]
	internal class Image : ReportItem
	{
		ImageSourceEnum _ImageSource;	// Identifies the source of the image:
		Expression _Value;		// See Source. Expected datatype is string or
								// binary, depending on Source. If the Value is
								// null, no image is displayed.
		Expression _MIMEType;	// (string) An expression, the value of which is the
								//	MIMEType for the image.
								//	Valid values are: image/bmp, image/jpeg,
								//	image/gif, image/png, image/x-png
								// Required if Source = Database. Ignored otherwise.
		ImageSizingEnum _Sizing;	// Defines the behavior if the image does not fit within the specified size.
	
		bool _ConstantImage;	// true if Image is a constant at runtime
		[NonSerialized] PageImage _pgImage;	// When ConstantImage is true this will save the PageImage for reuse
		[NonSerialized] ListDictionary _mimes;

		internal Image(Report r, ReportLink p, XmlNode xNode):base(r,p,xNode)
		{
			_ImageSource=ImageSourceEnum.Unknown;
			_Value=null;
			_MIMEType=null;
			_Sizing=ImageSizingEnum.AutoSize;
			_ConstantImage = false;
			_pgImage = null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "Source":
						_ImageSource = fyiReporting.RDL.ImageSource.GetStyle(xNodeLoop.InnerText);
						break;
					case "Value":
						_Value = new Expression(r, this, xNodeLoop, ExpressionType.Variant);
						break;
					case "MIMEType":
						_MIMEType = new Expression(r, this, xNodeLoop, ExpressionType.String);
						break;
					case "Sizing":
						_Sizing = ImageSizing.GetStyle(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					default:
						if (ReportItemElement(xNodeLoop))	// try at ReportItem level
							break;
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown Image element " + xNodeLoop.Name + " ignored.");
						break;
				}
			}
			if (_ImageSource==ImageSourceEnum.Unknown)
				OwnerReport.rl.LogError(8, "Image requires a Source element.");
			if (_Value == null)
				OwnerReport.rl.LogError(8, "Image requires the Value element.");
		}

		// Handle parsing of function in final pass
		override internal void FinalPass()
		{
			base.FinalPass();

			_Value.FinalPass();
			if (_MIMEType != null)
				_MIMEType.FinalPass();

			_ConstantImage = this.IsConstant();
			
			return;
		}

		// Returns true if the image and style remain constant at runtime
		bool IsConstant()
		{
			
			if (_Value.IsConstant())
			{
				if (_MIMEType == null || _MIMEType.IsConstant())
				{
					if (this.Style == null || this.Style.ConstantStyle)
						return true;
				}
			}
			return false;
		}

		override internal void Run(IPresent ip, Row row)
		{
			base.Run(ip, row);

			string mtype=null; 
			Stream strm=null;
			try 
			{
				strm = GetImageStream(row, out mtype);

				ip.Image(this, row, mtype, strm);
			}
			catch
			{
				// image failed to load;  continue processing
			}
			finally
			{
				if (strm != null)
					strm.Close();
			}
			return;
		}

		override internal void RunPage(Pages pgs, Row row)
		{
			if (IsHidden(row))
				return;

			string mtype=null; 
			Stream strm=null;
			System.Drawing.Image im=null;

			SetPagePositionBegin(pgs);
			if (this._pgImage != null)
			{	// have we already generated this one
				// reuse most of the work; only position will likely change
				PageImage pi = new PageImage(_pgImage.ImgFormat, _pgImage.ImageData, _pgImage.SamplesW, _pgImage.SamplesH);
				pi.Name = _pgImage.Name;				// this is name it will be shared under
				pi.Sizing = this._Sizing;
				this.SetPagePositionAndStyle(pi, row);
				pgs.CurrentPage.AddObject(pi);
				SetPagePositionEnd(pgs, pgs.CurrentPage.YOffset);
				return;
			}

			try 
			{
				strm = GetImageStream(row, out mtype); 
				im = System.Drawing.Image.FromStream(strm);
				int height = im.Height;
				int width = im.Width;
				MemoryStream ostrm = new MemoryStream();
				ImageFormat imf;
				//				if (mtype.ToLower() == "image/jpeg")    //TODO: how do we get png to work
				//					imf = ImageFormat.Jpeg;
				//				else
				imf = ImageFormat.Jpeg;
				im.Save(ostrm, imf);
				byte[] ba = ostrm.ToArray();
				ostrm.Close();
				PageImage pi = new PageImage(imf, ba, width, height);
				pi.Sizing = this._Sizing;
				this.SetPagePositionAndStyle(pi, row);

				pgs.CurrentPage.AddObject(pi);
				if (_ConstantImage)
				{
					this._pgImage = pi;
					pi.Name = OwnerReport.CreateRuntimeName(this);
				}
			}
			catch (Exception e)
			{	
				// image failed to load, continue processing
				this.OwnerReport.rl.LogError(4, "Image load failed.  " + e.Message);
			}
			finally
			{
				if (strm != null)
					strm.Close();
				if (im != null)
					im.Dispose();
			}
			SetPagePositionEnd(pgs, pgs.CurrentPage.YOffset);
			return;
		}

		Stream GetImageStream(Row row, out string mtype)
		{
			mtype=null; 
			Stream strm=null;
			try 
			{
				switch (this.ImageSource)
				{
					case ImageSourceEnum.Database:
						if (_MIMEType == null)
							return null;
						mtype = _MIMEType.EvaluateString(row);
						object o = _Value.Evaluate(row);
						strm = new MemoryStream((byte[]) o);
						break;
					case ImageSourceEnum.Embedded:
						string name = _Value.EvaluateString(row);
						EmbeddedImage ei = (EmbeddedImage) OwnerReport.LUEmbeddedImages[name];
						mtype = ei.MIMEType;
						byte[] ba = Convert.FromBase64String(ei.ImageData);
						strm = new MemoryStream(ba);
						break;
					case ImageSourceEnum.External:
						string fname = _Value.EvaluateString(row);
						mtype = GetMimeType(fname);
						strm = new FileStream(fname, System.IO.FileMode.Open, FileAccess.Read);		
						break;
					default:
						return null;
				}
			}
			catch
			{
				if (strm != null)
				{
					strm.Close();
					strm = null;
				}
			}

			return strm;
		}

		internal ImageSourceEnum ImageSource
		{
			get { return  _ImageSource; }
			set {  _ImageSource = value; }
		}

		internal Expression Value
		{
			get { return  _Value; }
			set {  _Value = value; }
		}

		internal Expression MIMEType
		{
			get { return  _MIMEType; }
			set {  _MIMEType = value; }
		}

		internal ImageSizingEnum Sizing
		{
			get { return  _Sizing; }
			set {  _Sizing = value; }
		}

		internal bool ConstantImage
		{
			get { return _ConstantImage; }
		}

		private string GetMimeType(string file)
		{
			String mimeType;
			String fileExt;
			
			int startPos = file.LastIndexOf(".") + 1;

			fileExt = file.Substring(startPos).ToLower();

			if (_mimes == null)
			{
				_mimes = new ListDictionary();
				_mimes.Add("bmp", "image/bmp");
				_mimes.Add("jpeg", "image/jpeg");
				_mimes.Add("gif", "image/gif");
				_mimes.Add("png", "image/png");
			}

			mimeType = (string) (_mimes[fileExt]);

			return mimeType; 
		}

	}
}
