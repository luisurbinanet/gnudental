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
	public class FormLetterEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.TextBox textBody;
		/// <summary>Required designer variable.</summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textDescription;
		private System.Windows.Forms.Label label7;
		///<summary></summary>
		public bool IsNew;

		///<summary></summary>
		public FormLetterEdit()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormLetterEdit));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.textBody = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(737, 496);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(737, 455);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textBody
			// 
			this.textBody.AcceptsReturn = true;
			this.textBody.Location = new System.Drawing.Point(132, 55);
			this.textBody.Multiline = true;
			this.textBody.Name = "textBody";
			this.textBody.Size = new System.Drawing.Size(677, 345);
			this.textBody.TabIndex = 1;
			this.textBody.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(29, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 14);
			this.label2.TabIndex = 3;
			this.label2.Text = "Description";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(132, 30);
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(346, 20);
			this.textDescription.TabIndex = 0;
			this.textDescription.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(43, 56);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(87, 79);
			this.label7.TabIndex = 7;
			this.label7.Text = "Body of Letter (do not include the address, greeting, or closing)";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormLetterEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(844, 544);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.textBody);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormLetterEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Letter";
			this.Load += new System.EventHandler(this.FormLetterEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormLetterEdit_Load(object sender, System.EventArgs e) {
			textDescription.Text=Letters.Cur.Description;
			textBody.Text=Letters.Cur.BodyText;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			Letters.Cur.Description=textDescription.Text;
			Letters.Cur.BodyText=textBody.Text;
			if(IsNew){
				Letters.InsertCur();
			}
			else{
				Letters.UpdateCur();
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		


	}
}





















