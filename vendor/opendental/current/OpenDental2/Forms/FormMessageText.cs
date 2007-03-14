using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormMessageText : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Timer timer2;
		public System.Windows.Forms.TextBox Text2;
		private System.ComponentModel.IContainer components;

		public FormMessageText(){
			InitializeComponent();
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
			});
		}


		protected override void Dispose( bool disposing ){
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
			this.Text2 = new System.Windows.Forms.TextBox();
			this.butOK = new System.Windows.Forms.Button();
			this.timer2 = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// Text2
			// 
			this.Text2.Location = new System.Drawing.Point(42, 65);
			this.Text2.Multiline = true;
			this.Text2.Name = "Text2";
			this.Text2.Size = new System.Drawing.Size(351, 84);
			this.Text2.TabIndex = 0;
			this.Text2.Text = "";
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(320, 210);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 1;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// timer2
			// 
			this.timer2.Enabled = true;
			this.timer2.Interval = 6000;
			this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
			// 
			// FormMessageText
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(438, 276);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.Text2);
			this.Name = "FormMessageText";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = Lan.g(this,"Message");
			this.Load += new System.EventHandler(this.FormMessageText_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormMessageText_Load(object sender, System.EventArgs e) {
			timer2.Start();
			Text2.SelectionLength=0;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void timer2_Tick(object sender, System.EventArgs e) {
			Close();
		}


	}
}
