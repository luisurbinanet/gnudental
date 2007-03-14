using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormPercentEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.TextBox text2;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label1;
		///<summary></summary>
		public int RetVal;

		///<summary></summary>
		public FormPercentEdit(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				label1,
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

		private void InitializeComponent(){
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.text2 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(194, 58);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 0;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(194, 94);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 1;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// text2
			// 
			this.text2.Location = new System.Drawing.Point(14, 68);
			this.text2.Name = "text2";
			this.text2.TabIndex = 2;
			this.text2.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(230, 32);
			this.label1.TabIndex = 3;
			this.label1.Text = "Please enter a number from 0 to 100, or leave blank.  No letters or symbols allow" +
				"ed";
			// 
			// FormPercentEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(292, 140);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.text2);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPercentEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Percent";
			this.Load += new System.EventHandler(this.FormPercentEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormPercentEdit_Load(object sender, System.EventArgs e) {
			if(RetVal==-1){
				text2.Text="";
			}
			else{
				text2.Text=RetVal.ToString();
			}
			text2.Select();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(text2.Text==""){
				RetVal=-1;
				DialogResult=DialogResult.OK;
				return;
			}
			try{
				RetVal=System.Convert.ToInt32(text2.Text);
			}
			catch{
				MessageBox.Show(Lan.g(this,"Invalid number format."));
				return;
			}
			if(RetVal>100){
				MessageBox.Show(Lan.g(this,"Value must not be more than 100"));
				return;
			}
			else if(RetVal<0){
				MessageBox.Show(Lan.g(this,"Value must not be less than 0"));
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
		
		}

	}
}
