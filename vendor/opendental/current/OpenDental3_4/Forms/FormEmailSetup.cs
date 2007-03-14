using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormEmailSetup : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.TextBox textSMTPserver;
		private System.Windows.Forms.TextBox textSender;
		private System.Windows.Forms.TextBox textBox1;
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormEmailSetup(){
			InitializeComponent();
			Lan.F(this);
			Lan.C(this, new System.Windows.Forms.Control[]
			{
				textBox6,
				textBox1
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
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textSMTPserver = new System.Windows.Forms.TextBox();
			this.textSender = new System.Windows.Forms.TextBox();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(477, 251);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(477, 209);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(70, 61);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(140, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "SMTP Server";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(18, 166);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(190, 20);
			this.label2.TabIndex = 3;
			this.label2.Text = "E-mail address of sender";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textSMTPserver
			// 
			this.textSMTPserver.Location = new System.Drawing.Point(210, 58);
			this.textSMTPserver.Name = "textSMTPserver";
			this.textSMTPserver.Size = new System.Drawing.Size(340, 20);
			this.textSMTPserver.TabIndex = 0;
			this.textSMTPserver.Text = "";
			// 
			// textSender
			// 
			this.textSender.Location = new System.Drawing.Point(210, 163);
			this.textSender.Name = "textSender";
			this.textSender.Size = new System.Drawing.Size(340, 20);
			this.textSender.TabIndex = 1;
			this.textSender.Text = "";
			// 
			// textBox6
			// 
			this.textBox6.BackColor = System.Drawing.SystemColors.Control;
			this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox6.Location = new System.Drawing.Point(39, 18);
			this.textBox6.Multiline = true;
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(589, 30);
			this.textBox6.TabIndex = 15;
			this.textBox6.Text = "There is no way to receive e-mail from within Open Dental yet.  These settings ar" +
				"e for outgoing e-mail.";
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(212, 81);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(198, 59);
			this.textBox1.TabIndex = 16;
			this.textBox1.Text = "smtp.comcast.net\r\nmailhost.mycompany.com \r\nmail.mycompany.com\r\nor similar...";
			// 
			// FormEmailSetup
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(579, 299);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.textBox6);
			this.Controls.Add(this.textSender);
			this.Controls.Add(this.textSMTPserver);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEmailSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Setup E-mail";
			this.Load += new System.EventHandler(this.FormEmailSetup_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormEmailSetup_Load(object sender, System.EventArgs e) {
			textSMTPserver.Text=((Pref)Prefs.HList["EmailSMTPserver"]).ValueString;
			textSender.Text=((Pref)Prefs.HList["EmailSenderAddress"]).ValueString;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			Prefs.Cur.PrefName="EmailSMTPserver";
			Prefs.Cur.ValueString=textSMTPserver.Text;
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="EmailSenderAddress";
			Prefs.Cur.ValueString=textSender.Text;
			Prefs.UpdateCur();

			DataValid.SetInvalid(InvalidTypes.Prefs);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}
