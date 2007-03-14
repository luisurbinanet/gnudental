using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormInsCatEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textDescription;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.CheckBox checkPreventive;
		private System.Windows.Forms.CheckBox checkHidden;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew;
		private OpenDental.ValidNumber textPercent;

		///<summary></summary>
		public FormInsCatEdit(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label1,
				label2,
				label3,
				label6,
				this.checkHidden,
				this.checkPreventive,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
			});
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
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
			this.label1 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.checkPreventive = new System.Windows.Forms.CheckBox();
			this.checkHidden = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textPercent = new OpenDental.ValidNumber();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(36, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(70, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Description";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 48);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 18);
			this.label6.TabIndex = 5;
			this.label6.Text = "Default Percent";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(114, 22);
			this.textDescription.MaxLength = 50;
			this.textDescription.Name = "textDescription";
			this.textDescription.TabIndex = 0;
			this.textDescription.Text = "";
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(274, 146);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 4;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(274, 184);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 5;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// checkPreventive
			// 
			this.checkPreventive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkPreventive.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkPreventive.Location = new System.Drawing.Point(36, 70);
			this.checkPreventive.Name = "checkPreventive";
			this.checkPreventive.Size = new System.Drawing.Size(92, 20);
			this.checkPreventive.TabIndex = 2;
			this.checkPreventive.Text = "Is Preventive";
			// 
			// checkHidden
			// 
			this.checkHidden.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkHidden.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkHidden.Location = new System.Drawing.Point(54, 94);
			this.checkHidden.Name = "checkHidden";
			this.checkHidden.Size = new System.Drawing.Size(74, 16);
			this.checkHidden.TabIndex = 3;
			this.checkHidden.Text = "Is Hidden";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 178);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(194, 30);
			this.label2.TabIndex = 12;
			this.label2.Text = "You can not delete a category, but you can hide it.";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(10, 136);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(176, 23);
			this.label3.TabIndex = 13;
			this.label3.Text = "Changes affect all patients";
			// 
			// textPercent
			// 
			this.textPercent.Location = new System.Drawing.Point(114, 46);
			this.textPercent.Name = "textPercent";
			this.textPercent.Size = new System.Drawing.Size(40, 20);
			this.textPercent.TabIndex = 1;
			this.textPercent.Text = "";
			// 
			// FormInsCatEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(358, 216);
			this.Controls.Add(this.textPercent);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.checkHidden);
			this.Controls.Add(this.checkPreventive);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormInsCatEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Insurance Categories";
			this.Load += new System.EventHandler(this.FormInsCatEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormInsCatEdit_Load(object sender, System.EventArgs e) {
			textDescription.Text=CovCats.Cur.Description;
			if(CovCats.Cur.DefaultPercent==-1)
				textPercent.Text="";
			else
				textPercent.Text=CovCats.Cur.DefaultPercent.ToString();
			textPercent.MaxVal=100;
			checkPreventive.Checked=CovCats.Cur.IsPreventive;
			checkHidden.Checked=CovCats.Cur.IsHidden;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(  textPercent.errorProvider1.GetError(textPercent)!=""
				//|| textPriBasicPercent.errorProvider1.GetError(textPriBasicPercent)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			CovCats.Cur.Description=textDescription.Text;
			if(textPercent.Text=="")
				CovCats.Cur.DefaultPercent=-1;
			else
				CovCats.Cur.DefaultPercent=PIn.PInt(textPercent.Text);
			CovCats.Cur.IsPreventive=checkPreventive.Checked;
			CovCats.Cur.IsHidden=checkHidden.Checked;
			if(IsNew){
				CovCats.Cur.CovOrder=CovCats.List.Length;
				CovCats.InsertCur();
			}
			else{
				CovCats.UpdateCur();
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


	}
}
