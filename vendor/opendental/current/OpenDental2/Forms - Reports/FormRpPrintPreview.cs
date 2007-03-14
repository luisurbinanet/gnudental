using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormRpPrintPreview : System.Windows.Forms.Form{
		public System.Windows.Forms.PrintPreviewControl printPreviewControl2;
		private System.Windows.Forms.Button button1;
		private System.ComponentModel.Container components = null;

		public FormRpPrintPreview(){
			InitializeComponent();
 			Lan.C(this, new System.Windows.Forms.Control[] {
				button1,
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
			this.printPreviewControl2 = new System.Windows.Forms.PrintPreviewControl();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// printPreviewControl2
			// 
			this.printPreviewControl2.AutoZoom = false;
			this.printPreviewControl2.Location = new System.Drawing.Point(0, 0);
			this.printPreviewControl2.Name = "printPreviewControl2";
			this.printPreviewControl2.Size = new System.Drawing.Size(842, 538);
			this.printPreviewControl2.TabIndex = 7;
			this.printPreviewControl2.Zoom = 1;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(323, 709);
			this.button1.Name = "button1";
			this.button1.TabIndex = 8;
			this.button1.Text = "next page";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// FormRpPrintPreview
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(842, 746);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.printPreviewControl2);
			this.Name = "FormRpPrintPreview";
			this.Text = "FormRxPrintPreview";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormRpPrintPreview_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e) {
			printPreviewControl2.StartPage++;
		}

		private void FormRpPrintPreview_Load(object sender, System.EventArgs e) {
			button1.Location=new Point(this.ClientRectangle.Width-100,this.ClientRectangle.Height-30);
			printPreviewControl2.Height=this.ClientRectangle.Height-40;
			printPreviewControl2.Width=this.ClientRectangle.Width;
			printPreviewControl2.Zoom=(double)printPreviewControl2.ClientSize.Height
				/1100;
		}


	}
}
