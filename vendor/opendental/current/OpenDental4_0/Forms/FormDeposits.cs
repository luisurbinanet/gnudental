using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormDeposits : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butAdd;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.ODGrid grid;
		Deposit[] DList;

		///<summary></summary>
		public FormDeposits()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormDeposits));
			this.butClose = new OpenDental.UI.Button();
			this.grid = new OpenDental.UI.ODGrid();
			this.butAdd = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.Location = new System.Drawing.Point(382, 576);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// grid
			// 
			this.grid.Columns.Add(new OpenDental.UI.ODGridColumn("Date", 80, System.Windows.Forms.HorizontalAlignment.Left));
			this.grid.Columns.Add(new OpenDental.UI.ODGridColumn("Amount", 90, System.Windows.Forms.HorizontalAlignment.Right));
			this.grid.HScrollVisible = false;
			this.grid.Location = new System.Drawing.Point(18, 13);
			this.grid.Name = "grid";
			this.grid.ScrollValue = 0;
			this.grid.Size = new System.Drawing.Size(189, 588);
			this.grid.TabIndex = 1;
			this.grid.Title = "Deposit Slips";
			this.grid.TranslationName = "TableDepositSlips";
			this.grid.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.grid_CellDoubleClick);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(223, 576);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(77, 26);
			this.butAdd.TabIndex = 98;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// FormDeposits
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(486, 613);
			this.Controls.Add(this.grid);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.butAdd);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDeposits";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Deposit Slips";
			this.Load += new System.EventHandler(this.FormDeposits_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormDeposits_Load(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			DList=Deposits.Refresh();
			grid.BeginUpdate();
			grid.Rows.Clear();
			OpenDental.UI.ODGridRow row;
			for(int i=0;i<DList.Length;i++){
				row=new OpenDental.UI.ODGridRow();
				row.Cells.Add(DList[i].DateDeposit.ToShortDateString());
				row.Cells.Add(DList[i].Amount.ToString("F"));
				grid.Rows.Add(row);
			}
			grid.EndUpdate();
		}

		private void grid_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			FormDepositEdit FormD=new FormDepositEdit(DList[e.Row]);
			FormD.ShowDialog();
			if(FormD.DialogResult==DialogResult.Cancel){
				return;
			}
			FillGrid();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			Deposit deposit=new Deposit();
			deposit.DateDeposit=DateTime.Today;
			deposit.BankAccountInfo=Prefs.GetString("PracticeBankNumber");
			FormDepositEdit FormD=new FormDepositEdit(deposit);
			FormD.IsNew=true;
			FormD.ShowDialog();
			if(FormD.DialogResult==DialogResult.Cancel){
				return;
			}
			FillGrid();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		

		

		


	}
}





















