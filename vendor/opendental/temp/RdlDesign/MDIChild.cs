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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Xml;
using fyiReporting.RDL;
using fyiReporting.RdlViewer;

namespace fyiReporting.RdlDesign
{
	/// <summary>
	/// RdlReader is a application for displaying reports based on RDL.
	/// </summary>
	internal class MDIChild : Form
	{
		public delegate void RdlChangeHandler(object sender, EventArgs e);
		public event RdlChangeHandler OnSelectionChanged;
		public event RdlChangeHandler OnSelectionMoved;
		public event RdlChangeHandler OnReportItemInserted;
		public event RdlChangeHandler OnDesignTabChanged;
		private fyiReporting.RdlDesign.RdlEditPreview rdlDesigner;
		string _SourceFile;

		public MDIChild(int width, int height)
		{
			this.rdlDesigner = new fyiReporting.RdlDesign.RdlEditPreview();
			this.SuspendLayout();
			// 
			// rdlDesigner
			// 
			this.rdlDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rdlDesigner.Location = new System.Drawing.Point(0, 0);
			this.rdlDesigner.Name = "rdlDesigner";
			this.rdlDesigner.Size = new System.Drawing.Size(width, height);
			this.rdlDesigner.TabIndex = 0;
			// register event for RDL changed.
			rdlDesigner.OnRdlChanged += new RdlEditPreview.RdlChangeHandler(rdlDesigner_RdlChanged);
			rdlDesigner.OnSelectionChanged += new RdlEditPreview.RdlChangeHandler(rdlDesigner_SelectionChanged);
			rdlDesigner.OnSelectionMoved += new RdlEditPreview.RdlChangeHandler(rdlDesigner_SelectionMoved);
			rdlDesigner.OnReportItemInserted += new RdlEditPreview.RdlChangeHandler(rdlDesigner_ReportItemInserted);
			rdlDesigner.OnDesignTabChanged += new RdlEditPreview.RdlChangeHandler(rdlDesigner_DesignTabChanged);
			// 
			// MDIChild
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(width, height);
			this.Controls.Add(this.rdlDesigner);
			this.Name = "";
			this.Text = "";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MDIChild_Closing);

			this.ResumeLayout(false);
		}

		public RdlEditPreview Editor
		{
			get
			{
				return rdlDesigner.CanEdit? rdlDesigner: null;	// only return when it can edit
			}
		}

		public string CurrentInsert
		{
			get {return rdlDesigner.CurrentInsert; }
			set 
			{
				rdlDesigner.CurrentInsert = value;
			}
		}

		internal string DesignTab
		{
			get {return rdlDesigner.DesignTab;}
		}

		internal DesignXmlDraw DrawCtl
		{
			get {return rdlDesigner.DrawCtl;}
		}

		public XmlDocument ReportDocument
		{
			get {return rdlDesigner.ReportDocument;}
		}
		
		internal void SetFocus()
		{
			rdlDesigner.SetFocus();
		}

		public StyleInfo SelectedStyle
		{
			get {return rdlDesigner.SelectedStyle;}
		}
		
		public string SelectionName
		{
			get {return rdlDesigner.SelectionName;}
		}
		
		public PointF SelectionPosition
		{
			get {return rdlDesigner.SelectionPosition;}
		}
		
		public void ApplyStyleToSelected(string name, string v)
		{
			rdlDesigner.ApplyStyleToSelected(name, v);
		}

		public bool FileSave()
		{
			string file = SourceFile;
			if (file == "" || file == null)			// if no file name then do SaveAs
			{
				return FileSaveAs();
			}
			string rdl = GetRdlText();

			return FileSave(file, rdl);
		}

		private bool FileSave(string file, string rdl)
		{
			StreamWriter writer=null;
			bool bOK=true;
			try
			{
				writer = new StreamWriter(file);
				writer.Write(rdl);
				//				editRDL.ClearUndo();
				//				editRDL.Modified = false;
				//				SetTitle();
				//				statusBar.Text = "Saved " + curFileName;
			}
			catch (Exception ae)
			{
				bOK=false;
				MessageBox.Show(ae.Message + "\r\n" +  ae.StackTrace);
				//				statusBar.Text = "Save of file '" + curFileName + "' failed";
			}
			finally
			{
				writer.Close();
			}
			if (bOK)
				this.Modified=false;
			return bOK;
		}
		
		public bool FileSaveAs()
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = 
				"RDL files (*.rdl)|*.rdl|" +
				"PDF files (*.pdf)|*.pdf|" +
				"XML files (*.xml)|*.xml|" +
				"HTML files (*.html)|*.html";
			sfd.FilterIndex = 1;

			string file = SourceFile;

			sfd.FileName = file == null? "*.rdl": file;

			if (sfd.ShowDialog(this) != DialogResult.OK)
				return false;

			// save the report in a rendered format 
			string ext=null;
			int i = sfd.FileName.LastIndexOf('.');
			if (i < 1)
				ext = "";
			else
				ext = sfd.FileName.Substring(i+1).ToLower();

			bool rc=true;
			switch(ext)
			{
				case "pdf":	case "xml": case "html": case "htm":
					try {SaveAs(sfd.FileName, ext);}
					catch (Exception ex)
					{
						MessageBox.Show(this, 
							ex.Message, "Save As Error", 
							MessageBoxButtons.OK, MessageBoxIcon.Error);
						rc = false;
					}
					break;
				case "rdl":
					string rdl = GetRdlText();
					if (FileSave(sfd.FileName, rdl))
					{	// Save was successful
						Text = sfd.FileName;
						_SourceFile = sfd.FileName;	
					}
					else
						rc = false;
					break;
				default:
					MessageBox.Show(this, 
						String.Format("{0} is not a valid file type.  File extension must be RDL, PDF, XML, or HTML.", sfd.FileName), "Save As Error", 
						MessageBoxButtons.OK, MessageBoxIcon.Error);
					rc = false;
					break;
			}
			return rc;
		}
 
		public string GetRdlText()
		{
			return this.rdlDesigner.GetRdlText();
		}

		public bool Modified
		{
			get {return rdlDesigner.Modified;}
			set 
			{
				rdlDesigner.Modified = value;
				SetTitle();
			}
		}

		/// <summary>
		/// The RDL file that should be displayed.
		/// </summary>
		public string SourceFile
		{
			get {return _SourceFile;}
			set 
			{
				_SourceFile = value;
				string rdl = GetRdlSource();
				this.rdlDesigner.SetRdlText(rdl == null? "": rdl);
			}
		}

		public string SourceRdl
		{
			get {return this.rdlDesigner.GetRdlText();}
			set	{this.rdlDesigner.SetRdlText(value);}
		}

		private string GetRdlSource()
		{
			StreamReader fs=null;
			string prog=null;
			try
			{
				fs = new StreamReader(_SourceFile);
				prog = fs.ReadToEnd();
			}
			finally
			{
				if (fs != null)
					fs.Close();
			}

			return prog;
		}

		/// <summary>
		/// Number of pages in the report.
		/// </summary>
		public int PageCount
		{
			get {return this.rdlDesigner.PageCount;}
		}

		/// <summary>
		/// Current page in view on report
		/// </summary>
		public int PageCurrent
		{
			get {return this.rdlDesigner.PageCurrent;}
		}

		/// <summary>
		/// Page height of the report.
		/// </summary>
		public float PageHeight
		{
			get {return this.rdlDesigner.PageHeight;}  
		}

		/// <summary>
		/// Page width of the report.
		/// </summary>
		public float PageWidth
		{
			get {return this.rdlDesigner.PageWidth;}
		}

		/// <summary>
		/// Zoom 
		/// </summary>
		public float Zoom
		{
			get {return this.rdlDesigner.Zoom;}
			set {this.rdlDesigner.Zoom = value;}
		}

		/// <summary>
		/// ZoomMode 
		/// </summary>
		public ZoomEnum ZoomMode
		{
			get {return this.rdlDesigner.ZoomMode;}
			set {this.rdlDesigner.ZoomMode = value;}
		}
 
		/// <summary>
		/// Print the report.  
		/// </summary>
		public void Print(PrintDocument pd)
		{
			this.rdlDesigner.Print(pd);
		}

		public void SaveAs(string filename, string ext)
		{
			rdlDesigner.SaveAs(filename, ext);
		}

		private void MDIChild_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (!OkToClose())
				e.Cancel = true;
		}

		public bool OkToClose()
		{
			if (!this.Modified)
				return true;

			DialogResult r = 
					MessageBox.Show(this, String.Format("Do you want to save changes you made to '{0}'?",
					_SourceFile==null?"Untitled":Path.GetFileName(_SourceFile)), 
					"fyiReporting Designer",
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button3);

			bool bOK = true;
			if (r == DialogResult.Cancel)
				bOK = false;
			else if (r == DialogResult.Yes)
			{
				if (!FileSave())
					bOK = false;
			}
			return bOK;
		}

		private void rdlDesigner_RdlChanged(object sender, System.EventArgs e)
		{
			SetTitle();
		}

		private void rdlDesigner_SelectionChanged(object sender, System.EventArgs e)
		{
			if (OnSelectionChanged != null)
				OnSelectionChanged(this, e);
		}

		private void rdlDesigner_DesignTabChanged(object sender, System.EventArgs e)
		{
			if (OnDesignTabChanged != null)
				OnDesignTabChanged(this, e);
		}

		private void rdlDesigner_ReportItemInserted(object sender, System.EventArgs e)
		{
			if (OnReportItemInserted != null)
				OnReportItemInserted(this, e);
		}

		private void rdlDesigner_SelectionMoved(object sender, System.EventArgs e)
		{
			if (OnSelectionMoved != null)
				OnSelectionMoved(this, e);
		}

		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MDIChild));
			// 
			// MDIChild
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MDIChild";

		}

		private void SetTitle()
		{
			string title = this.Text;
			if (title.Length < 1)
				return;
			char m = title[title.Length-1];
			if (this.Modified)
			{
				if (m != '*')
					title = title + "*";
			}
			else if (m == '*')
				title = title.Substring(0, title.Length-1);

			if (title != this.Text)
				this.Text = title;
			return;
		}

		public fyiReporting.RdlViewer.RdlViewer Viewer
		{
			get {return rdlDesigner.Viewer;}
		}

	}
}
