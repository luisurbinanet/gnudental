using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormInputBox : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		public System.Windows.Forms.TextBox textBox1;
		public System.Windows.Forms.Label label1;
		private System.ComponentModel.Container components = null;

		public FormInputBox(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label1,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
			});
		}

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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.butOK.Location = new System.Drawing.Point(328, 192);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 0;
			this.butOK.Text = "OK";
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(328, 226);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 1;
			this.butCancel.Text = "Cancel";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(86, 128);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(250, 20);
			this.textBox1.TabIndex = 2;
			this.textBox1.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(88, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(248, 76);
			this.label1.TabIndex = 3;
			this.label1.Text = "label1";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormInputBox
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(428, 268);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																																	this.label1,
																																	this.textBox1,
																																	this.butCancel,
																																	this.butOK});
			this.Name = "FormInputBox";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = Lan.g(this,"Input Box");
			this.ResumeLayout(false);

		}
		#endregion
	}
}
