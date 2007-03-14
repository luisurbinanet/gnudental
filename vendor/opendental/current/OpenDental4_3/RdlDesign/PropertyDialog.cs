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
using System.Xml;

namespace fyiReporting.RdlDesign
{
	internal enum PropertyTypeEnum
	{
		Report,
		DataSets,
		ReportItems,
		Grouping,
		ChartLegend,
		CategoryAxis,
		ValueAxis,
		ChartTitle,
		CategoryAxisTitle,
		ValueAxisTitle,
		TableGroup
	}

	/// <summary>
	/// Summary description for PropertyDialog.
	/// </summary>
	internal class PropertyDialog : System.Windows.Forms.Form
	{
		private DesignXmlDraw _Draw;		// design draw 
		private ArrayList _Nodes;			// selected nodes
		private PropertyTypeEnum _Type;	
		private bool _Changed=false;
		private bool _Delete=false;
		private ArrayList _TabPanels=new ArrayList();		// list of IProperty controls
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button bCancel;
		private System.Windows.Forms.Button bOK;
		private System.Windows.Forms.Button bApply;
		private System.Windows.Forms.TabControl tcProps;
		private System.Windows.Forms.Button bDelete;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		internal PropertyDialog(DesignXmlDraw dxDraw, ArrayList sNodes, PropertyTypeEnum type)
		{
			this._Draw = dxDraw;
			this._Nodes = sNodes;
			this._Type = type;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//   Add the controls for the selected ReportItems
			switch (_Type)
			{
				case PropertyTypeEnum.Report:
					BuildReportTabs();
					break;
				case PropertyTypeEnum.DataSets:
					BuildDataSetsTabs();
					break;
				case PropertyTypeEnum.Grouping:
					BuildGroupingTabs();
					break;
				case PropertyTypeEnum.ChartLegend:
					BuildChartLegendTabs();
					break;
				case PropertyTypeEnum.CategoryAxis:
				case PropertyTypeEnum.ValueAxis:
					BuildChartAxisTabs(type);
					break;
				case PropertyTypeEnum.ChartTitle:
				case PropertyTypeEnum.CategoryAxisTitle:
				case PropertyTypeEnum.ValueAxisTitle:
					BuildTitle(type);
					break;
				case PropertyTypeEnum.ReportItems:
				default:
					BuildReportItemTabs();
					break;
			}
		}

		internal bool Changed
		{
			get {return _Changed; }
		}

		internal bool Delete
		{
			get {return _Delete; }
		}

		private void BuildReportTabs()
		{
			this.Text = "Report Properties";

			ReportCtl rc = new ReportCtl(_Draw);
			AddTab("Report", rc);

			ReportParameterCtl pc = new ReportParameterCtl(_Draw);
			AddTab("Parameters", pc);

			ReportXmlCtl xc = new ReportXmlCtl(_Draw);
			AddTab("XML Rendering", xc);

			BodyCtl bc = new BodyCtl(_Draw);
			AddTab("Body", bc);
			return;
		}

		private void BuildDataSetsTabs()
		{
			bDelete.Visible = true;

			this.Text = "DataSet";

			XmlNode aNode;
			if (_Nodes != null && _Nodes.Count > 0)
				aNode = (XmlNode) _Nodes[0];
			else
				aNode = null;

			DataSetsCtl dsc = new DataSetsCtl(_Draw, aNode);
			AddTab("DataSet", dsc);

			QueryParametersCtl qp = new QueryParametersCtl(_Draw, dsc.DSV);
			AddTab("Query Parameters", qp);

			FiltersCtl fc = new FiltersCtl(_Draw, aNode);
			AddTab("Filters", fc);

			DataSetRowsCtl dsrc = new DataSetRowsCtl(_Draw, aNode, dsc.DSV);
			AddTab("Data", dsrc);
			return;
		}

		private void BuildGroupingTabs()
		{
			XmlNode aNode = (XmlNode) _Nodes[0];
			if (aNode.Name == "DynamicSeries")
			{
				this.Text = "Series Grouping";
			}
			else if (aNode.Name == "DynamicCategories")
			{
				this.Text = "Category Grouping";
			}
			else
			{
				this.Text = "Grouping and Sorting";
			}

			GroupingCtl gc = new GroupingCtl(_Draw, aNode);
			AddTab("Grouping", gc);

			SortingCtl sc = new SortingCtl(_Draw, aNode);
			AddTab("Sorting", sc);

			// We have to create a grouping here but will need to kill it if no definition follows it
			XmlNode gNode = _Draw.GetCreateNamedChildNode(aNode, "Grouping");   

			FiltersCtl fc = new FiltersCtl(_Draw, gNode);
			AddTab("Filters", fc);
			return;
		}

		private void BuildReportItemTabs()
		{
			XmlNode aNode = (XmlNode) _Nodes[0];

			// Determine if all nodes are the same type
			string type = aNode.Name;
			foreach (XmlNode pNode in this._Nodes)
			{
				if (pNode.Name != type)
					type = "";			// Not all nodes have the same type
			}

			EnsureStyle();	// Make sure we have Style nodes for all the report items

			if (_Nodes.Count > 1)
				this.Text = "Group Selection Properties";
			else
			{
				string name = _Draw.GetElementAttribute(aNode, "Name", "");
				this.Text = string.Format("{0} {1} Properties", type, name);
			}

			// Create all the tabs
			if (type == "Textbox")
			{
				StyleTextCtl stc = new StyleTextCtl(_Draw, this._Nodes);
				AddTab("Text", stc);
			}
			else if (type == "List")
			{
				ListCtl lc = new ListCtl(_Draw, this._Nodes);
				AddTab("List", lc);
				if (_Nodes.Count == 1)
				{
					FiltersCtl fc = new FiltersCtl(_Draw, (XmlNode) _Nodes[0]);
					AddTab("Filters", fc);
				}
			}
			else if (type == "Chart")
			{
				ChartCtl cc = new ChartCtl(_Draw, this._Nodes);
				AddTab("Chart", cc);
				if (_Nodes.Count == 1)
				{
					FiltersCtl fc = new FiltersCtl(_Draw, (XmlNode) _Nodes[0]);
					AddTab("Filters", fc);
				}
			}
			else if (type == "Image")
			{
				ImageCtl imgc = new ImageCtl(_Draw, this._Nodes);
				AddTab("Image", imgc);
			}
			else if (type == "Table")
			{
				XmlNode table = (XmlNode) _Nodes[0];
				TableCtl tc = new TableCtl(_Draw, this._Nodes);
				AddTab("Table", tc);
				FiltersCtl fc = new FiltersCtl(_Draw, table);
				AddTab("Filters", fc);
				XmlNode details = _Draw.GetNamedChildNode(table, "Details");
				if (details != null)
				{	// if no details then we don't need details sorting
					GroupingCtl grpc = new GroupingCtl(_Draw, details);
					AddTab("Grouping", grpc);
					SortingCtl srtc = new SortingCtl(_Draw, details);
					AddTab("Sorting", srtc);
				}
			}
			else if (type == "Matrix")
			{
				XmlNode matrix = (XmlNode) _Nodes[0];
				MatrixCtl mc = new MatrixCtl(_Draw, this._Nodes);
				AddTab("Matrix", mc);
				FiltersCtl fc = new FiltersCtl(_Draw, matrix);
				AddTab("Filters", fc);
			}
			else if (type == "Subreport" && _Nodes.Count == 1)
			{
				XmlNode subreport = (XmlNode) _Nodes[0];
				SubreportCtl src = new SubreportCtl(_Draw, subreport);
				AddTab("Subreport", src);
			}

			// Position tab
			PositionCtl pc = new PositionCtl(_Draw, this._Nodes);
			AddTab("Name/Position", pc);

			// Border tab
			StyleBorderCtl bc = new StyleBorderCtl(_Draw, this._Nodes);
			AddTab("Border", bc);

			if (! (type == "Line" || type == "Subreport"))
			{
				// Style tab
				StyleCtl sc = new StyleCtl(_Draw, this._Nodes);
				AddTab("Style", sc);

				// Interactivity tab
				InteractivityCtl ic = new InteractivityCtl(_Draw, this._Nodes);
				AddTab("Interactivity", ic);
			}
		}

		private void BuildChartAxisTabs(PropertyTypeEnum type)
		{
			string propName;
			if (type == PropertyTypeEnum.CategoryAxis)
			{
				this.Text = "Chart Category (X) Axis";
				propName = "CategoryAxis";
			}
			else
			{
				this.Text = "Chart Value (Y) Axis";
				propName = "ValueAxis";
			}

			XmlNode cNode = (XmlNode) _Nodes[0];
			XmlNode aNode = _Draw.GetCreateNamedChildNode(cNode, propName);
			XmlNode axNode = _Draw.GetCreateNamedChildNode(aNode, "Axis");

			// Now we replace the node array with a new one containing only the legend
			_Nodes = new ArrayList();
			_Nodes.Add(axNode);

			EnsureStyle();	// Make sure we have Style nodes

			// Chart Axis
			ChartAxisCtl cac = new ChartAxisCtl(_Draw, this._Nodes);
			AddTab("Axis", cac);

			// Style Text
			StyleTextCtl stc = new StyleTextCtl(_Draw, this._Nodes);
			AddTab("Text", stc);

			// Border tab
			StyleBorderCtl bc = new StyleBorderCtl(_Draw, this._Nodes);
			AddTab("Border", bc);

			// Style tab
			StyleCtl sc = new StyleCtl(_Draw, this._Nodes);
			AddTab("Style", sc);
		}

		private void BuildChartLegendTabs()
		{
			this.Text = "Chart Legend Properties";

			XmlNode cNode = (XmlNode) _Nodes[0];
			XmlNode lNode = _Draw.GetCreateNamedChildNode(cNode, "Legend");

			// Now we replace the node array with a new one containing only the legend
			_Nodes = new ArrayList();
			_Nodes.Add(lNode);

			EnsureStyle();	// Make sure we have Style nodes

			// Chart Legend
			ChartLegendCtl clc = new ChartLegendCtl(_Draw, this._Nodes);
			AddTab("Legend", clc);

			// Style Text
			StyleTextCtl stc = new StyleTextCtl(_Draw, this._Nodes);
			AddTab("Text", stc);

			// Border tab
			StyleBorderCtl bc = new StyleBorderCtl(_Draw, this._Nodes);
			AddTab("Border", bc);

			// Style tab
			StyleCtl sc = new StyleCtl(_Draw, this._Nodes);
			AddTab("Style", sc);
		}

		private void BuildTitle(PropertyTypeEnum type)
		{
			XmlNode cNode = (XmlNode) _Nodes[0];
			_Nodes = new ArrayList();		// replace with a new one
			if (type == PropertyTypeEnum.ChartTitle)
			{
				this.Text = "Chart Title";

				XmlNode lNode = _Draw.GetCreateNamedChildNode(cNode, "Title");
				_Nodes.Add(lNode);		// Working on the title		
			}
			else if (type == PropertyTypeEnum.CategoryAxisTitle)
			{
				this.Text = "Category (X) Axis Title";
				XmlNode caNode = _Draw.GetCreateNamedChildNode(cNode, "CategoryAxis");
				XmlNode aNode = _Draw.GetCreateNamedChildNode(caNode, "Axis");
				XmlNode tNode = _Draw.GetCreateNamedChildNode(aNode, "Title");
				_Nodes.Add(tNode);		// Working on the title		
			}
			else
			{
				this.Text = "Value (Y) Axis Title";
				XmlNode caNode = _Draw.GetCreateNamedChildNode(cNode, "ValueAxis");
				XmlNode aNode = _Draw.GetCreateNamedChildNode(caNode, "Axis");
				XmlNode tNode = _Draw.GetCreateNamedChildNode(aNode, "Title");
				_Nodes.Add(tNode);		// Working on the title		
			}

			EnsureStyle();	// Make sure we have Style nodes

			// Style Text
			StyleTextCtl stc = new StyleTextCtl(_Draw, this._Nodes);
			AddTab("Text", stc);

			// Border tab
			StyleBorderCtl bc = new StyleBorderCtl(_Draw, this._Nodes);
			AddTab("Border", bc);

			// Style tab
			StyleCtl sc = new StyleCtl(_Draw, this._Nodes);
			AddTab("Style", sc);
		}
		
		private void EnsureStyle()
		{
			// Make sure we have Style nodes for all the nodes
			foreach (XmlNode pNode in this._Nodes)
			{
				XmlNode stNode = _Draw.GetCreateNamedChildNode(pNode, "Style");
			}
			return;
		}

		private void AddTab(string name, UserControl uc)
		{
			// Style tab
			TabPage tp = new TabPage();
			tp.Location = new System.Drawing.Point(4, 22);
			tp.Name = name + "1";
			tp.Size = new System.Drawing.Size(552, 284);
			tp.TabIndex = 1;
			tp.Text = name;

			_TabPanels.Add(uc);
			tp.Controls.Add(uc);

			uc.Dock = System.Windows.Forms.DockStyle.Fill;
			uc.Location = new System.Drawing.Point(0, 0);
			uc.Name = name + "1";
			uc.Size = new System.Drawing.Size(552, 284);
			uc.TabIndex = 0;

			tcProps.Controls.Add(tp);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.bApply = new System.Windows.Forms.Button();
			this.bOK = new System.Windows.Forms.Button();
			this.bCancel = new System.Windows.Forms.Button();
			this.tcProps = new System.Windows.Forms.TabControl();
			this.bDelete = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.CausesValidation = false;
			this.panel1.Controls.Add(this.bDelete);
			this.panel1.Controls.Add(this.bApply);
			this.panel1.Controls.Add(this.bOK);
			this.panel1.Controls.Add(this.bCancel);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 310);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(458, 56);
			this.panel1.TabIndex = 1;
			// 
			// bApply
			// 
			this.bApply.Location = new System.Drawing.Point(376, 16);
			this.bApply.Name = "bApply";
			this.bApply.TabIndex = 2;
			this.bApply.Text = "Apply";
			this.bApply.Click += new System.EventHandler(this.bApply_Click);
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(200, 17);
			this.bOK.Name = "bOK";
			this.bOK.TabIndex = 0;
			this.bOK.Text = "OK";
			this.bOK.Click += new System.EventHandler(this.bOK_Click);
			// 
			// bCancel
			// 
			this.bCancel.CausesValidation = false;
			this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bCancel.Location = new System.Drawing.Point(288, 16);
			this.bCancel.Name = "bCancel";
			this.bCancel.TabIndex = 1;
			this.bCancel.Text = "Cancel";
			// 
			// tcProps
			// 
			this.tcProps.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcProps.Location = new System.Drawing.Point(0, 0);
			this.tcProps.Name = "tcProps";
			this.tcProps.SelectedIndex = 0;
			this.tcProps.Size = new System.Drawing.Size(458, 310);
			this.tcProps.TabIndex = 0;
			// 
			// bDelete
			// 
			this.bDelete.Location = new System.Drawing.Point(8, 16);
			this.bDelete.Name = "bDelete";
			this.bDelete.TabIndex = 3;
			this.bDelete.Text = "Delete";
			this.bDelete.Visible = false;
			this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
			// 
			// PropertyDialog
			// 
			this.AcceptButton = this.bOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.bCancel;
			this.ClientSize = new System.Drawing.Size(458, 366);
			this.Controls.Add(this.tcProps);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PropertyDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Properties";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.PropertyDialog_Closing);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void bApply_Click(object sender, System.EventArgs e)
		{
			if (!IsValid())
				return;

			this._Changed = true;
			foreach (IProperty ip in _TabPanels)
			{
				ip.Apply();
			}
			this._Draw.Invalidate();		// Force screen to redraw
		}

		private void bOK_Click(object sender, System.EventArgs e)
		{
			if (!IsValid())
				return;

			bApply_Click(sender, e);	// Apply does all the work
			this.DialogResult = DialogResult.OK;
		}

		private bool IsValid()
		{
			int index=0;
			foreach (IProperty ip in _TabPanels)
			{
				if (!ip.IsValid())			
				{
					tcProps.SelectedIndex = index;
					return false;
				}
				index++;
			}
			return true;
		}

		private void PropertyDialog_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (_Type == PropertyTypeEnum.Grouping)
			{	// Need to check if grouping value is still required
				XmlNode aNode = (XmlNode) _Nodes[0];

				// We have to create a grouping here but will need to kill it if no definition follows it
				XmlNode gNode = _Draw.GetNamedChildNode(aNode, "Grouping");   
				if (gNode != null && 
					_Draw.GetNamedChildNode(gNode, "GroupExpressions") == null)
				{	// Not a valid group if no GroupExpressions
					aNode.RemoveChild(gNode);
				}
			}
		
		}

		private void bDelete_Click(object sender, System.EventArgs e)
		{
			if (MessageBox.Show(this, 
					"Are you sure you want to delete this dataset?", 
					"DataSet",
					MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				_Delete = true;
				this.DialogResult = DialogResult.OK;
			}
		}
	}

	internal interface IProperty
	{
		void Apply();
		bool IsValid();
	}

}
