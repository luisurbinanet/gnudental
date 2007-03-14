using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormClaimForms : System.Windows.Forms.Form{
		private System.Windows.Forms.ListBox listClaimForms;
		private System.Windows.Forms.Button butAdd;
		private System.Windows.Forms.Button butClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormClaimForms()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				this,
				
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				//butOK,
				//butCancel,
				butAdd
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormClaimForms));
			this.butClose = new System.Windows.Forms.Button();
			this.listClaimForms = new System.Windows.Forms.ListBox();
			this.butAdd = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClose.Location = new System.Drawing.Point(286, 339);
			this.butClose.Name = "butClose";
			this.butClose.TabIndex = 0;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// listClaimForms
			// 
			this.listClaimForms.Location = new System.Drawing.Point(45, 24);
			this.listClaimForms.Name = "listClaimForms";
			this.listClaimForms.Size = new System.Drawing.Size(203, 251);
			this.listClaimForms.TabIndex = 2;
			this.listClaimForms.DoubleClick += new System.EventHandler(this.listClaimForms_DoubleClick);
			// 
			// butAdd
			// 
			this.butAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butAdd.Location = new System.Drawing.Point(46, 298);
			this.butAdd.Name = "butAdd";
			this.butAdd.TabIndex = 3;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// FormClaimForms
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(400, 415);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.listClaimForms);
			this.Controls.Add(this.butClose);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClaimForms";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Claim Forms";
			this.Load += new System.EventHandler(this.FormClaimForms_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormClaimForms_Load(object sender, System.EventArgs e) {
			FillList();
		}

		private void FillList(){
			ClaimForms.Refresh();
			listClaimForms.Items.Clear();
			string description;
			for(int i=0;i<ClaimForms.ListLong.Length;i++){
				description=ClaimForms.ListLong[i].Description;
				if(ClaimForms.ListLong[i].IsHidden)
					description+="(hidden)";
				//if(ClaimForms.ListLong[i].UniqueID)
				//	description+="(locked)";
				listClaimForms.Items.Add(description);
			}
		}

		private void listClaimForms_DoubleClick(object sender, System.EventArgs e) {
			if(listClaimForms.SelectedIndex==-1)
				return;
			ClaimForms.Cur=ClaimForms.ListLong[listClaimForms.SelectedIndex];
			FormClaimFormEdit FormCFE=new FormClaimFormEdit();
			FormCFE.ShowDialog();
			FillList();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			ClaimForms.Cur=new ClaimForm();
			ClaimForms.InsertCur();
			FormClaimFormEdit FormCFE=new FormClaimFormEdit();
			FormCFE.IsNew=true;
			FormCFE.ShowDialog();
			FillList();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		


	}
}





















