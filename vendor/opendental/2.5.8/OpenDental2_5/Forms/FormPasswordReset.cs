using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormPasswordReset : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textMasterPass;
		private System.Windows.Forms.Button butCancel;
		private string masterHash;

		///<summary></summary>
		public FormPasswordReset(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				label1
				//labelPassword,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
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
			this.textMasterPass = new System.Windows.Forms.TextBox();
			this.butOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.butCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textMasterPass
			// 
			this.textMasterPass.Location = new System.Drawing.Point(120, 24);
			this.textMasterPass.MaxLength = 100;
			this.textMasterPass.Name = "textMasterPass";
			this.textMasterPass.Size = new System.Drawing.Size(212, 20);
			this.textMasterPass.TabIndex = 35;
			this.textMasterPass.Text = "";
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(384, 96);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(76, 26);
			this.butOK.TabIndex = 37;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(18, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 50);
			this.label1.TabIndex = 38;
			this.label1.Text = "Master Password (you must call us)";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(384, 132);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(76, 26);
			this.butCancel.TabIndex = 39;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormPasswordReset
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(478, 176);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.textMasterPass);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPasswordReset";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Reset Password";
			this.Load += new System.EventHandler(this.FormRP_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRP_Load(object sender, System.EventArgs e) {
			//it does not compromise security to include the hash to the master password in the code
			//because the user must still enter the password, not the hash.
			masterHash="921967431631579194136163208691539149188";
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//MessageBox.Show(Passwords.EncryptPassword(textMasterPass.Text));
			if(!Passwords.CheckPassword(textMasterPass.Text,masterHash)){
				MessageBox.Show(Lan.g(this,"Master password incorrect."));
				return;
			}
			Permissions.DisableSecurity();
			Permissions.Refresh();
			MessageBox.Show(Lan.g(this,"Security Administration permission has been reset."));
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}




	}
}
