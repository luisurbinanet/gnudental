using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormInsTemplateCombine : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary>After this window closes, this will be the index within the main List of the selected carrier.</summary>
		public int UsingIndex;
		private OpenDental.TableTemplates tbTemplates;
		///<summary>Before opening this Form, set the indices of the main List to show.</summary>
		public int[] ShowingIndices;

		///<summary></summary>
		public FormInsTemplateCombine()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
			});
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormInsTemplateCombine));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.tbTemplates = new OpenDental.TableTemplates();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(854, 459);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(747, 459);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(13, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(476, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "Please select the template to keep when combining";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// tbTemplates
			// 
			this.tbTemplates.BackColor = System.Drawing.SystemColors.Window;
			this.tbTemplates.Location = new System.Drawing.Point(3, 45);
			this.tbTemplates.Name = "tbTemplates";
			this.tbTemplates.ScrollValue = 1;
			this.tbTemplates.SelectedIndices = new int[0];
			this.tbTemplates.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbTemplates.Size = new System.Drawing.Size(936, 355);
			this.tbTemplates.TabIndex = 15;
			this.tbTemplates.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(this.Table2_CellDoubleClicked);
			// 
			// FormInsTemplateCombine
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(945, 505);
			this.Controls.Add(this.tbTemplates);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormInsTemplateCombine";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Combine Insurance Templates";
			this.Load += new System.EventHandler(this.FormInsTemplateCombine_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormInsTemplateCombine_Load(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			/*
			tbCarriers.ResetRows(ShowingIndices.Length);
			tbCarriers.SetGridColor(Color.Gray);
			tbCarriers.SetBackGColor(Color.White);
			for(int i=0;i<ShowingIndices.Length;i++){
				tbCarriers.Cell[0,i]=Carriers.List[ShowingIndices[i]].CarrierName;
				tbCarriers.Cell[1,i]=Carriers.List[ShowingIndices[i]].Phone;
				tbCarriers.Cell[2,i]=Carriers.List[ShowingIndices[i]].Address;
				tbCarriers.Cell[3,i]=Carriers.List[ShowingIndices[i]].Address2;
				tbCarriers.Cell[4,i]=Carriers.List[ShowingIndices[i]].City;
				tbCarriers.Cell[5,i]=Carriers.List[ShowingIndices[i]].State;
				tbCarriers.Cell[6,i]=Carriers.List[ShowingIndices[i]].Zip;
				tbCarriers.Cell[7,i]=Carriers.List[ShowingIndices[i]].ElectID;
			}
			tbCarriers.LayoutTables();*/
		}

		private void Table2_CellDoubleClicked(object sender, OpenDental.CellEventArgs e) {
		//	UsingIndex=e.Row;
		//	DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//if(tbCarriers.SelectedRow==-1){
			//	MessageBox.Show(Lan.g(this,"Please select an item first."));
			//	return;
			//}
			//UsingIndex=tbCarriers.SelectedRow;
			//DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			//DialogResult=DialogResult.Cancel;
		}

	
		

		

		


	}
}





















