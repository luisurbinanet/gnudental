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


namespace fyiReporting.RDL
{
	///<summary>
	/// Handle the image size enumeration.  AutoSize, Fit, FitProportional, Clip
	///</summary>
	public enum ImageSizingEnum
	{
		AutoSize,	// The borders should grow/shrink
					// to accommodate the image (Default).
		Fit,		// The image is resized to exactly match
					// the height and width of the image element.
		FitProportional,	//The image should be
					// resized to fit, preserving aspect ratio.1
		Clip		// The image should be clipped to fit.1		
	}

	public class ImageSizing
	{
		static public ImageSizingEnum GetStyle(string s)
		{
			return GetStyle(s, null);
		}

		static internal ImageSizingEnum GetStyle(string s, ReportLog rl)
			{
			ImageSizingEnum rs;

			switch (s)
			{		
				case "AutoSize":
					rs = ImageSizingEnum.AutoSize;
					break;
				case "Fit":
					rs = ImageSizingEnum.Fit;
					break;
				case "FitProportional":
					rs = ImageSizingEnum.FitProportional;
					break;
				case "Clip":
					rs = ImageSizingEnum.Clip;
					break;
				default:		
					if (rl != null)
						rl.LogError(4, "Unknown ImageSizing '" + s + "'.  AutoSize assumed.");

					rs = ImageSizingEnum.AutoSize;
					break;
			}
			return rs;
		}
	}

}
