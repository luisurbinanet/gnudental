using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormAbout : System.Windows.Forms.Form{
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.Button butClose;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.ComponentModel.Container components = null;

		public FormAbout(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.labelVersion,
				//butReset,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butClose,
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
			this.labelVersion = new System.Windows.Forms.Label();
			this.butClose = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// labelVersion
			// 
			this.labelVersion.Location = new System.Drawing.Point(20, 25);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(188, 23);
			this.labelVersion.TabIndex = 1;
			this.labelVersion.Text = "Using Version ";
			// 
			// butClose
			// 
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.butClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClose.Location = new System.Drawing.Point(305, 383);
			this.butClose.Name = "butClose";
			this.butClose.TabIndex = 2;
			this.butClose.Text = "Close";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(19, 87);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(665, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "Open Dental (AKA Free Dental)  Copyright 2003, Jordan S. Sparks, D.M.D., www.open" +
				"-dent.com";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(19, 140);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(652, 20);
			this.label2.TabIndex = 4;
			this.label2.Text = "ByteFX, the data driver - Copyright 2003, www.bytefx.com";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(19, 113);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(652, 20);
			this.label3.TabIndex = 6;
			this.label3.Text = "MySQL - Copyright 1995-2003, www.mysql.com";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(20, 62);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(652, 20);
			this.label4.TabIndex = 7;
			this.label4.Text = "All parts of this program are licensed under the GPL, www.opensource.org/licenses" +
				"/gpl-license.php";
			// 
			// FormAbout
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(695, 415);
			this.ControlBox = false;
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.labelVersion);
			this.Name = "FormAbout";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "About Open Dental";
			this.Load += new System.EventHandler(this.FormAbout_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormAbout_Load(object sender, System.EventArgs e) {
			labelVersion.Text=Lan.g(this,"Using Version "+Application.ProductVersion);
		}


	}
}
