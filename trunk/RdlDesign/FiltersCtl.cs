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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using System.Text;

namespace fyiReporting.RdlDesign
{
	/// <summary>
	/// Filters specification: used for DataRegions (List, Chart, Table, Matrix), DataSets, group instances
	/// </summary>
	internal class FiltersCtl : System.Windows.Forms.UserControl, IProperty
	{
		private DesignXmlDraw _Draw;
		private XmlNode _FilterParent;
		private DataTable _DataTable;
		private DataGridTextBoxColumn dgtbFE;
		private DataGridTextBoxColumn dgtbOP;
		private DataGridTextBoxColumn dgtbFV;

		private System.Windows.Forms.Button bDelete;
		private System.Windows.Forms.DataGrid dgFilters;
		private System.Windows.Forms.DataGridTableStyle dgTableStyle;
		private System.Windows.Forms.Button bUp;
		private System.Windows.Forms.Button bDown;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		internal FiltersCtl(DesignXmlDraw dxDraw, XmlNode filterParent)
		{
			_Draw = dxDraw;
			_FilterParent = filterParent;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Initialize form using the style node values
			InitValues();			
		}

		private void InitValues()
		{
			// Initialize the DataGrid columns
			dgtbFE = new DataGridTextBoxColumn();
			dgtbOP = new DataGridTextBoxColumn();
			dgtbFV = new DataGridTextBoxColumn();

			this.dgTableStyle.GridColumnStyles.AddRange(new DataGridColumnStyle[] {
															this.dgtbFE,
															this.dgtbOP,
															this.dgtbFV});
			// 
			// dgtbFE
			// 
			dgtbFE.HeaderText = "Filter Expression";
			dgtbFE.MappingName = "FilterExpression";
			dgtbFE.Width = 75;
			// Get the parent's dataset name
//			string dataSetName = _Draw.GetDataSetNameValue(_FilterParent);
//
//			string[] fields = _Draw.GetFields(dataSetName, true);
//			if (fields != null)
//				dgtbFE.CB.Items.AddRange(fields);
			// 
			// dgtbOP
			// 
			//dgtbOP.Format = "";
			//dgtbOP.FormatInfo = null;
			dgtbOP.HeaderText = "Operator";
			dgtbOP.MappingName = "Operator";
			dgtbOP.Width = 70;
//			dgtbOP.CB.Items.AddRange(new string[] 
//				{"=", "Like", "NotEqual", ">",
//				 ">=", "<", "<=",
//				 "TopN", "BottomN", "TopPercent", "BottomPercent",
//				 "In", "Between" } );
			// 
			// dgtbFV
			// 
			this.dgtbFV.HeaderText = "Value(s)";
			this.dgtbFV.MappingName = "Value";
			this.dgtbFV.Width = 75;
//			string[] parms = _Draw.GetReportParameters(true);
//			if (parms != null)
//				dgtbFV.CB.Items.AddRange(parms);

			// Initialize the DataTable
			_DataTable = new DataTable();
			_DataTable.Columns.Add(new DataColumn("FilterExpression", typeof(string)));
			_DataTable.Columns.Add(new DataColumn("Operator", typeof(string)));
			_DataTable.Columns.Add(new DataColumn("Value", typeof(string)));

			string[] rowValues = new string[3];
			XmlNode filters = _Draw.GetNamedChildNode(_FilterParent, "Filters");

			if (filters != null)
			foreach (XmlNode fNode in filters.ChildNodes)
			{
				if (fNode.NodeType != XmlNodeType.Element || 
						fNode.Name != "Filter")
					continue;
				rowValues[0] = _Draw.GetElementValue(fNode, "FilterExpression", "");
				rowValues[1] = _Draw.GetElementValue(fNode, "Operator", "");
				// Get the values
				XmlNode vNodes = _Draw.GetNamedChildNode(fNode, "FilterValues");
				if (vNodes != null)
				{
					StringBuilder sb = new StringBuilder();
					foreach (XmlNode v in vNodes.ChildNodes)
					{
						if (v.InnerText.Length <= 0)
							continue;
						if (sb.Length != 0)
							sb.Append(", ");
						sb.Append(v.InnerText);
					}
					rowValues[2] = sb.ToString();
				}
				else 
					rowValues[2] = "";

				_DataTable.Rows.Add(rowValues);
			}
			this.dgFilters.DataSource = _DataTable;
			DataGridTableStyle ts = dgFilters.TableStyles[0];
		//	ts.PreferredRowHeight = dgtbOP.CB.Height;
			ts.GridColumnStyles[0].Width = 140;
			ts.GridColumnStyles[1].Width = 55;
			ts.GridColumnStyles[2].Width = 140;
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dgFilters = new System.Windows.Forms.DataGrid();
			this.dgTableStyle = new System.Windows.Forms.DataGridTableStyle();
			this.bDelete = new System.Windows.Forms.Button();
			this.bUp = new System.Windows.Forms.Button();
			this.bDown = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgFilters)).BeginInit();
			this.SuspendLayout();
			// 
			// dgFilters
			// 
			this.dgFilters.CaptionVisible = false;
			this.dgFilters.DataMember = "";
			this.dgFilters.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgFilters.Location = new System.Drawing.Point(8, 8);
			this.dgFilters.Name = "dgFilters";
			this.dgFilters.Size = new System.Drawing.Size(376, 264);
			this.dgFilters.TabIndex = 2;
			this.dgFilters.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																								  this.dgTableStyle});
			// 
			// dgTableStyle
			// 
			this.dgTableStyle.AllowSorting = false;
			this.dgTableStyle.DataGrid = this.dgFilters;
			this.dgTableStyle.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgTableStyle.MappingName = "";
			// 
			// bDelete
			// 
			this.bDelete.Location = new System.Drawing.Point(392, 16);
			this.bDelete.Name = "bDelete";
			this.bDelete.Size = new System.Drawing.Size(48, 23);
			this.bDelete.TabIndex = 1;
			this.bDelete.Text = "Delete";
			this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
			// 
			// bUp
			// 
			this.bUp.Location = new System.Drawing.Point(392, 48);
			this.bUp.Name = "bUp";
			this.bUp.Size = new System.Drawing.Size(48, 23);
			this.bUp.TabIndex = 3;
			this.bUp.Text = "Up";
			this.bUp.Click += new System.EventHandler(this.bUp_Click);
			// 
			// bDown
			// 
			this.bDown.Location = new System.Drawing.Point(392, 80);
			this.bDown.Name = "bDown";
			this.bDown.Size = new System.Drawing.Size(48, 23);
			this.bDown.TabIndex = 4;
			this.bDown.Text = "Down";
			this.bDown.Click += new System.EventHandler(this.bDown_Click);
			// 
			// FiltersCtl
			// 
			this.Controls.Add(this.bDown);
			this.Controls.Add(this.bUp);
			this.Controls.Add(this.bDelete);
			this.Controls.Add(this.dgFilters);
			this.Name = "FiltersCtl";
			this.Size = new System.Drawing.Size(488, 304);
			((System.ComponentModel.ISupportInitialize)(this.dgFilters)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		public bool IsValid()
		{
			return true;
		}

		public void Apply()
		{
			// Remove the old filters
			XmlNode filters = _Draw.GetCreateNamedChildNode(_FilterParent, "Filters");
			while (filters.FirstChild != null)
			{
				filters.RemoveChild(filters.FirstChild);
			}
			// Loop thru and add all the filters
			foreach (DataRow dr in _DataTable.Rows)
			{
				if (dr[0] == DBNull.Value || dr[1] == DBNull.Value || dr[2] == DBNull.Value)
					continue;
				string fe = (string) dr[0];
				string op = (string) dr[1];
				string fv = (string) dr[2];
				if (fe.Length <= 0 || op.Length <= 0 || fv.Length <= 0)
					continue;
				XmlNode fNode = _Draw.CreateElement(filters, "Filter", null);
				_Draw.CreateElement(fNode, "FilterExpression", fe);
				_Draw.CreateElement(fNode, "Operator", op);
				XmlNode fvNode = _Draw.CreateElement(fNode, "FilterValues", null);
				if (op == "In")
				{
					string[] vs = fv.Split(',');
					foreach (string v in vs)
						_Draw.CreateElement(fvNode, "FilterValue", v.Trim());
				}
				else if (op == "Between")
				{
					string[] vs = fv.Split(new char[] {','}, 2);
					foreach (string v in vs)
						_Draw.CreateElement(fvNode, "FilterValue", v.Trim());
				}
				else
				{
					_Draw.CreateElement(fvNode, "FilterValue", fv);
				}
			}
			if (!filters.HasChildNodes)
				_FilterParent.RemoveChild(filters);
		}

		private void bDelete_Click(object sender, System.EventArgs e)
		{
			this._DataTable.Rows.RemoveAt(this.dgFilters.CurrentRowIndex);
		}

		private void bUp_Click(object sender, System.EventArgs e)
		{
			int cr = dgFilters.CurrentRowIndex;
			if (cr <= 0)		// already at the top
				return;
			
			SwapRow(_DataTable.Rows[cr-1], _DataTable.Rows[cr]);
			dgFilters.CurrentRowIndex = cr-1;
		}

		private void bDown_Click(object sender, System.EventArgs e)
		{
			int cr = dgFilters.CurrentRowIndex;
			if (cr < 0)			// invalid index
				return;
			if (cr + 1 >= _DataTable.Rows.Count)
				return;			// already at end
			
			SwapRow(_DataTable.Rows[cr+1], _DataTable.Rows[cr]);
			dgFilters.CurrentRowIndex = cr+1;
		}

		private void SwapRow(DataRow tdr, DataRow fdr)
		{
			// column 1
			object save = tdr[0];
			tdr[0] = fdr[0];
			fdr[0] = save;
			// column 2
			save = tdr[1];
			tdr[1] = fdr[1];
			fdr[1] = save;
			// column 3
			save = tdr[2];
			tdr[2] = fdr[2];
			fdr[2] = save;
			return;
		}
	}
}
