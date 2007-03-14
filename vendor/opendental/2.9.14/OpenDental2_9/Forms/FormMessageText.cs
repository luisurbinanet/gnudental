using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormMessageText : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Timer timer2;
		///<summary></summary>
		public System.Windows.Forms.TextBox Text2;
		private System.Windows.Forms.Button butCancel;
		private System.ComponentModel.IContainer components;

		///<summary></summary>
		public FormMessageText(){
			InitializeComponent();
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
			});
		}

		///<summary></summary>
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
			this.butCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Text2
			// 
			this.Text2.AcceptsReturn = true;
			this.Text2.Location = new System.Drawing.Point(42, 65);
			this.Text2.Multiline = true;
			this.Text2.Name = "Text2";
			this.Text2.Size = new System.Drawing.Size(351, 84);
			this.Text2.TabIndex = 0;
			this.Text2.Text = "";
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(319, 197);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// timer2
			// 
			this.timer2.Enabled = true;
			this.timer2.Interval = 6000;
			this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(319, 234);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			// 
			// FormMessageText
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(438, 276);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.Text2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMessageText";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Message";
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
