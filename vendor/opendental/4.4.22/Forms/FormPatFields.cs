using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;

namespace OpenDental{
	/// <summary>Not currently used.</summary>
	public class FormPatFields:System.Windows.Forms.Form {
		private OpenDental.UI.Button butClose;
		private System.ComponentModel.IContainer components;
		private OpenDental.UI.ODGrid gridMain;
		private System.Windows.Forms.ToolTip toolTip1;
		private int PatNum;
		private PatField[] PatFieldList;

		///<summary></summary>
		public FormPatFields(int patNum)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			PatNum=patNum;
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPatFields));
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butClose = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(18,12);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(622,231);
			this.gridMain.TabIndex = 8;
			this.gridMain.Title = "Patient Fields";
			this.gridMain.TranslationName = "FormPatFields";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.Location = new System.Drawing.Point(562,264);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(79,26);
			this.butClose.TabIndex = 1;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormPatFields
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(660,302);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPatFields";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Patient Fields";
			this.Load += new System.EventHandler(this.FormPatFields_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormPatFields_Load(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			PatFieldList=PatFields.Refresh(PatNum);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("FormPatFields","Field Name"),150);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("FormPatFields","Field Value"),450);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<PatFieldList.Length;i++){
				row=new ODGridRow();
				row.Cells.Add(PatFieldList[i].FieldName);
				row.Cells.Add(PatFieldList[i].FieldValue);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormPatFieldEdit FormP=new FormPatFieldEdit(PatFieldList[e.Row]);
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK)
				return;
			FillGrid();
		}

		/*private void butAdd_Click(object sender, System.EventArgs e) {
			//Employers.Cur=new Employer();
			PatFieldDef def=new PatFieldDef();
			FormPatFieldDefEdit FormP=new FormPatFieldDefEdit(def);
			FormP.IsNew=true;
			FormP.ShowDialog();
			FillGrid();
		}*/

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		

		
		
		

		

		

		

		

		


	}
}



























