using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>Lets the user choose which payment plan to attach a payment to if there are more than one available.</summary>
	public class FormPayPlanSelect : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		/// <summary>A list of plans passed to this form which are to be displayed.</summary>
		public PayPlan[] ValidPlans;
		private System.Windows.Forms.ListBox listPayPlans;
		/// <summary>The index of the plan selected.</summary>
		public int IndexSelected;

		///<summary></summary>
		public FormPayPlanSelect()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormPayPlanSelect));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.listPayPlans = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(195, 175);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(99, 175);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// listPayPlans
			// 
			this.listPayPlans.Location = new System.Drawing.Point(32, 51);
			this.listPayPlans.Name = "listPayPlans";
			this.listPayPlans.Size = new System.Drawing.Size(238, 95);
			this.listPayPlans.TabIndex = 2;
			this.listPayPlans.DoubleClick += new System.EventHandler(this.listPayPlans_DoubleClick);
			// 
			// FormPayPlanSelect
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(295, 222);
			this.Controls.Add(this.listPayPlans);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPayPlanSelect";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Payment Plan";
			this.Load += new System.EventHandler(this.FormPayPlanSelect_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormPayPlanSelect_Load(object sender, System.EventArgs e) {
			for(int i=0;i<ValidPlans.Length;i++){
				listPayPlans.Items.Add(ValidPlans[i].PayPlanDate.ToShortDateString()
					+"  "+ValidPlans[i].TotalAmount.ToString("F"));
			}
		}

		private void listPayPlans_DoubleClick(object sender, System.EventArgs e) {
			if(listPayPlans.SelectedIndex==-1){
				return;
			}
			IndexSelected=listPayPlans.SelectedIndex;
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(listPayPlans.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select a payment plan first."));
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

	


	}
}





















