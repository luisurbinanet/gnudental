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

namespace fyiReporting.RDL
{
	///<summary>
	/// The definition of a Subreport (report name, parameters, ...).
	///</summary>
	[Serializable]
	internal class Subreport : ReportItem, ICacheData
	{
		string _ReportName;		// The full path (e.g. “/salesreports/orderdetails”) or
								// relative path (e.g. “orderdetails”) to a subreport.
								// Relative paths start in the same folder as the current
								// Report output formats unable to support FitProportional or Clip should output as Fit instead.
								// (parent) report.
								// Cannot be an empty string (ignoring whitespace)
		SubReportParameters _Parameters;	//Parameters to the Subreport
								// If the subreport is executed without parameters
								// (and contains no Toggle elements), it will only be
								// executed once (even if it appears inside of a list,
								// table or matrix)
		Expression _NoRows;		// (string)	Message to display in the subreport (instead of the
								// region layout) when no rows of data are available
								// in any data set in the subreport
								// Note: Style information on the subreport applies to
								// this text.
		bool _MergeTransactions;	// Indicates that transactions in the subreport should
								//be merged with transactions in the parent report
								//(into a single transaction for the entire report) if the
								//data sources use the same connection.		
		// Runtime variable
		[NonSerialized] Report _Report;			// loaded report
		[NonSerialized] Page _FirstPage;		// first page for the loaded report
		[NonSerialized] IList _ReportErrors;	// reported errors
		[NonSerialized] bool _LoadFailed;		// set to true if load of report has failed
		[NonSerialized] bool _BeenRun;			// when true we don't need to rerun report; just render it

		internal Subreport(Report r, ReportLink p, XmlNode xNode) :base(r, p, xNode)
		{
			_ReportName=null;
			_Parameters=null;
			_NoRows=null;
			_MergeTransactions=true;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "ReportName":
						_ReportName = xNodeLoop.InnerText;
						break;
					case "Parameters":
						_Parameters = new SubReportParameters(r, this, xNodeLoop);
						break;
					case "NoRows":
						_NoRows = new Expression(r, this, xNodeLoop, ExpressionType.String);
						break;
					case "MergeTransactions":
						_MergeTransactions = XmlUtil.Boolean(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					default:	
						if (ReportItemElement(xNodeLoop))	// try at ReportItem level
							break;
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown Image element " + xNodeLoop.Name + " ignored.");
						break;
				}
			}
		
			if (_ReportName == null)
				OwnerReport.rl.LogError(8, "Subreport requires the ReportName element.");
		}

		// Handle parsing of function in final pass
		override internal void FinalPass()
		{
			base.FinalPass();

			// Subreports aren't allowed in PageHeader or PageFooter; 
			if (this.InPageHeaderOrFooter())
				OwnerReport.rl.LogError(8, String.Format("The Subreport '{0}' is not allowed in a PageHeader or PageFooter", this.Name == null? "unknown": Name.Nm) );

			if (_Parameters != null)
				_Parameters.FinalPass();
			if (_NoRows != null)
				_NoRows.FinalPass();

			OwnerReport.DataCache.Add(this);
			return;
		}

		override internal void Run(IPresent ip, Row row)
		{
			base.Run(ip, row);

			if (_LoadFailed)
				return;

			if (_Report == null)
			{
				_Report = GetReport();
				if (_Report == null)		// todo handle the error messages
				{
					_LoadFailed = true;		// mark it so we don't keep trying and trying
					if (_ReportErrors != null)
					{
						ip.Subreport(this, row);	// let the renderer report the error
						_ReportErrors = null;
					}
					return;
				}
				_Report.Subreport = this;
			}

			if (_Parameters != null)
				SetSubreportParameters(row);

			if (!_BeenRun)
			{
				_Report.RunGetData(null);	// we've already set the parameters
				if (_Parameters == null)	// if no parameters 
					_BeenRun = true;		//    we don't need to obtain data ever again
			}

			_ReportErrors = null;			// we don't have any serious errors to report via render
			ip.Subreport(this, row);
			if (_Report.ErrorItems != null)
			{
				OwnerReport.rl.LogError(4, _Report.ErrorItems);
				_Report.ErrorReset();
			}
		}

		override internal void RunPage(Pages pgs, Row row)
		{
			if (IsHidden(row))
				return;

			base.RunPage(pgs, row);

			if (_LoadFailed)				// if we fail hard (e.g. can't load report) don't keep trying and failing
				return;

			if (_Report == null)			// First time we won't have loaded the report yet
			{
				_Report = GetReport();
				if (_Report == null)		// todo handle the error messages
				{
					_LoadFailed = true;		// mark it so we don't keep trying and trying
					if (_ReportErrors != null)
					{
						RunPageError(pgs, row);	// output the errors
						_ReportErrors = null;
					}
					return;
				}
				_Report.Subreport = this;	// we got the report; mark it as being a subreport
				SetPageLeft();				// Set the Left attribute since this will be the margin for this report
			}

			// Apply the parameters
			if (_Parameters != null)
				SetSubreportParameters(row);

			// Obtain the data for the subreport;  don't need to repeat if no parameters on subreport
			if (!_BeenRun)
			{
				_Report.RunGetData(null);	// we've already set the parameters
				if (_Parameters == null)	// if no parameters 
					_BeenRun = true;		//    we don't need to obtain data ever again
			}

			_ReportErrors = null;			// we don't have any serious errors to report via render
			
			SetPagePositionBegin(pgs);
			_FirstPage = pgs.CurrentPage;

			//
			// Run the subreport -- this is the major effort in creating the display objects in the page
			//
			_Report.Body.RunPage(pgs);		// create a the subreport items

			SetPagePositionEnd(pgs, pgs.CurrentPage.YOffset);

			// Save any runtime errors that might have occured in the subreport
			if (_Report.ErrorItems != null)
			{
				OwnerReport.rl.LogError(4, _Report.ErrorItems);
				_Report.ErrorReset();
			}
		}

		private void RunPageError(Pages pgs, Row row)
		{
			if (_ReportErrors == null)
				return;
			StringBuilder sb = new StringBuilder();
			foreach (string msg in _ReportErrors)
			{
				sb.Append(msg);
				sb.Append("\r\n");
			}
			PageText pt = new PageText(sb.ToString());
			SetPagePositionAndStyle(pt, row);

			Page p = pgs.CurrentPage;
			p.AddObject(pt);
		}

		private Report GetReport()
		{
			string prog;
			string name;

			if (_ReportName[0] == Path.DirectorySeparatorChar ||
				_ReportName[0] == Path.AltDirectorySeparatorChar)
				name = _ReportName;
			else 
				name = OwnerReport.Folder + Path.DirectorySeparatorChar + _ReportName;

			name = name + ".rdl";			// TODO: shouldn't necessarily require this extension

			// Load and Compile the report
			RDLParser rdlp;
			Report r;
			try
			{
				prog = GetRdlSource(name);
				rdlp =  new RDLParser(prog);
				r = rdlp.Parse();
				if (r.ErrorMaxSeverity > 0) 
				{
					int severity = r.ErrorMaxSeverity;
					r.ErrorReset();
					if (severity > 4)
					{
						this._ReportErrors = r.ErrorItems;
						r = null;			// don't return when severe errors
					}
				}
				// If we've loaded the report; we should tell it where it got loaded from
				if (r != null)
				{	// Don't care much if this fails; and don't want to null out report if it does
					try {r.Folder = Path.GetDirectoryName(name);}
					catch {}
				}
			}
			catch (Exception ex)
			{
				if (_ReportErrors == null)
					_ReportErrors = new ArrayList();

				_ReportErrors.Add(string.Format("Subreport {0} failed with exception. {1}", this._ReportName, ex.Message));
				r = null;
			}
			return r;
		}

		private string GetRdlSource(string name)
		{
			// TODO: at some point might want to provide interface so that read can be controlled
			//         by server:  would allow for caching etc.
			StreamReader fs=null;
			string prog=null;
			try
			{
				fs = new StreamReader(name);		
				prog = fs.ReadToEnd();
			}
			finally
			{
				if (fs != null)
					fs.Close();
			}

			return prog;
		}

		private void SetSubreportParameters(Row row)
		{
			UserReportParameter userp;
			foreach (SubreportParameter srp in _Parameters.Items)
			{
				userp=null;						
				foreach (UserReportParameter urp in _Report.UserReportParameters)
				{
					if (urp.Name == srp.Name.Nm)
					{
						userp = urp;
						break;
					}
				}
				if (userp == null)
				{	// parameter name not found
					throw new Exception(
						string.Format("Subreport {0} doesn't define parameter {1}.", _ReportName, srp.Name.Nm));
				}
				object v = srp.Value.Evaluate(row);
				userp.Value = v;
			}
		}

		internal Page FirstPage
		{
			get { return  _FirstPage; }
		}

		internal string ReportName
		{
			get { return  _ReportName; }
			set {  _ReportName = value; }
		}

		internal Report Report
		{
			get { return _Report; }
		}

		internal IList ReportErrors
		{
			get { return _ReportErrors; }
		}

		internal SubReportParameters Parameters
		{
			get { return  _Parameters; }
			set {  _Parameters = value; }
		}

		internal Expression NoRows
		{
			get { return  _NoRows; }
			set {  _NoRows = value; }
		}

		internal bool MergeTransactions
		{
			get { return  _MergeTransactions; }
			set {  _MergeTransactions = value; }
		}
		#region ICacheData Members

		public void ClearCache()
		{
			_BeenRun = false;
		}

		#endregion
	}
}
