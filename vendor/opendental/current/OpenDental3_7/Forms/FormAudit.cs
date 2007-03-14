using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormAudit : System.Windows.Forms.Form{
		private OpenDental.UI.ODGrid grid;
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.Label label2;
		private OpenDental.ValidDate textDateFrom;
		private OpenDental.ValidDate textDateTo;
		private System.Windows.Forms.Label label3;
		private OpenDental.UI.Button butRefresh;
		private System.ComponentModel.IContainer components;

		///<summary></summary>
		public FormAudit()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormAudit));
			this.butClose = new OpenDental.UI.Button();
			this.grid = new OpenDental.UI.ODGrid();
			this.label2 = new System.Windows.Forms.Label();
			this.textDateFrom = new OpenDental.ValidDate();
			this.textDateTo = new OpenDental.ValidDate();
			this.label3 = new System.Windows.Forms.Label();
			this.butRefresh = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.Location = new System.Drawing.Point(793, 591);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// grid
			// 
			this.grid.Columns.Add(new OpenDental.UI.ODGridColumn("Date", 80, System.Windows.Forms.HorizontalAlignment.Left));
			this.grid.Columns.Add(new OpenDental.UI.ODGridColumn("User", 80, System.Windows.Forms.HorizontalAlignment.Left));
			this.grid.Columns.Add(new OpenDental.UI.ODGridColumn("Permission", 120, System.Windows.Forms.HorizontalAlignment.Left));
			this.grid.Columns.Add(new OpenDental.UI.ODGridColumn("Log Text", 451, System.Windows.Forms.HorizontalAlignment.Left));
			this.grid.HScrollVisible = false;
			this.grid.Location = new System.Drawing.Point(8, 31);
			this.grid.Name = "grid";
			this.grid.ScrollValue = 0;
			this.grid.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.grid.Size = new System.Drawing.Size(750, 585);
			this.grid.TabIndex = 2;
			this.grid.Title = "Audit Trail";
			this.grid.TranslationName = "TableAudit";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(5, 5);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(82, 14);
			this.label2.TabIndex = 45;
			this.label2.Text = "From Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateFrom
			// 
			this.textDateFrom.Location = new System.Drawing.Point(91, 2);
			this.textDateFrom.Name = "textDateFrom";
			this.textDateFrom.TabIndex = 47;
			this.textDateFrom.Text = "";
			// 
			// textDateTo
			// 
			this.textDateTo.Location = new System.Drawing.Point(276, 2);
			this.textDateTo.Name = "textDateTo";
			this.textDateTo.TabIndex = 48;
			this.textDateTo.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(188, 6);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(82, 13);
			this.label3.TabIndex = 46;
			this.label3.Text = "To Date";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.Location = new System.Drawing.Point(439, 1);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(82, 26);
			this.butRefresh.TabIndex = 49;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// FormAudit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(905, 634);
			this.Controls.Add(this.butRefresh);
			this.Controls.Add(this.textDateFrom);
			this.Controls.Add(this.textDateTo);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.grid);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAudit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Audit Trail";
			this.Load += new System.EventHandler(this.FormAudit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormAudit_Load(object sender, System.EventArgs e) {
			textDateFrom.Text=DateTime.Today.AddDays(-10).ToShortDateString();
			textDateTo.Text=DateTime.Today.ToShortDateString();
			FillGrid();
		}

		private void FillGrid(){
			SecurityLog[] logList=SecurityLogs.Refresh(PIn.PDate(textDateFrom.Text),PIn.PDate(textDateTo.Text));
			grid.BeginUpdate();
			grid.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<logList.Length;i++){
				row=new ODGridRow();
				row.Cells.Add(logList[i].LogDateTime.ToShortDateString());
				row.Cells.Add(Users.GetUser(logList[i].UserNum).UserName);
				row.Cells.Add(logList[i].PermType.ToString());
				row.Cells.Add(logList[i].LogText);
				grid.Rows.Add(row);
			}
			grid.EndUpdate();
			grid.ScrollToEnd();
		}

		private void butRefresh_Click(object sender, System.EventArgs e) {
			if( textDateFrom.Text=="" 
				|| textDateTo.Text==""
				|| textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateTo.errorProvider1.GetError(textDateTo)!=""
				){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			FillGrid();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			this.Close();
		}

	


		


	}
}





















