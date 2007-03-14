using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.Reporting;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormEmailTemplateEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		/// <summary>Required designer variable.</summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textSubject;
		private OpenDental.ODtextBox textBodyText;
		///<summary></summary>
		public bool IsNew;
		public EmailTemplate ETcur;

		///<summary></summary>
		public FormEmailTemplateEdit()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormEmailTemplateEdit));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.textSubject = new System.Windows.Forms.TextBox();
			this.textBodyText = new OpenDental.ODtextBox();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(755, 505);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(755, 472);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(7, 12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 14);
			this.label2.TabIndex = 3;
			this.label2.Text = "Subject:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSubject
			// 
			this.textSubject.Location = new System.Drawing.Point(99, 7);
			this.textSubject.Name = "textSubject";
			this.textSubject.Size = new System.Drawing.Size(741, 20);
			this.textSubject.TabIndex = 0;
			this.textSubject.Text = "";
			// 
			// textBodyText
			// 
			this.textBodyText.AcceptsReturn = true;
			this.textBodyText.Location = new System.Drawing.Point(99, 32);
			this.textBodyText.Multiline = true;
			this.textBodyText.Name = "textBodyText";
			this.textBodyText.QuickPasteType = OpenDental.QuickPasteType.Email;
			this.textBodyText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBodyText.Size = new System.Drawing.Size(741, 426);
			this.textBodyText.TabIndex = 4;
			this.textBodyText.Text = "";
			// 
			// FormEmailTemplateEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(844, 544);
			this.Controls.Add(this.textBodyText);
			this.Controls.Add(this.textSubject);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEmailTemplateEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit E-mail Template";
			this.Load += new System.EventHandler(this.FormEmailTemplateEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormEmailTemplateEdit_Load(object sender, System.EventArgs e) {
			textSubject.Text=ETcur.Subject;
			textBodyText.Text=ETcur.BodyText;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textSubject.Text=="" && textBodyText.Text==""){
				MessageBox.Show(Lan.g(this,"Both can not be left blank."));
				return;
			}
			ETcur.Subject=textSubject.Text;
			ETcur.BodyText=textBodyText.Text;
			if(IsNew){
				ETcur.Insert();
			}
			else{
				ETcur.Update();
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		


	}
}





















