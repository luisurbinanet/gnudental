using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormNoteFinancial : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.TextBox textFinNotes;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button butDate;

		public FormNoteFinancial(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				butDate,
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
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.textFinNotes = new System.Windows.Forms.TextBox();
			this.butDate = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(446, 652);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 41;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(446, 614);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 40;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textFinNotes
			// 
			this.textFinNotes.BackColor = System.Drawing.Color.White;
			this.textFinNotes.Location = new System.Drawing.Point(50, 22);
			this.textFinNotes.Multiline = true;
			this.textFinNotes.Name = "textFinNotes";
			this.textFinNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textFinNotes.Size = new System.Drawing.Size(328, 650);
			this.textFinNotes.TabIndex = 42;
			this.textFinNotes.Text = "";
			// 
			// butDate
			// 
			this.butDate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butDate.Location = new System.Drawing.Point(416, 56);
			this.butDate.Name = "butDate";
			this.butDate.Size = new System.Drawing.Size(75, 26);
			this.butDate.TabIndex = 43;
			this.butDate.Text = "Insert Date";
			this.butDate.Click += new System.EventHandler(this.butDate_Click);
			// 
			// FormNoteFinancial
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(556, 696);
			this.Controls.Add(this.butDate);
			this.Controls.Add(this.textFinNotes);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormNoteFinancial";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Family Financial Note";
			this.Load += new System.EventHandler(this.FormNoteFinancial_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormNoteFinancial_Load(object sender, System.EventArgs e) {
			textFinNotes.Text=PatientNotes.Cur.FamFinancial;
			textFinNotes.Select();
			textFinNotes.SelectionStart=textFinNotes.Text.Length;
			textFinNotes.ScrollToCaret();
		}

		private void butDate_Click(object sender, System.EventArgs e) {
			int caret=textFinNotes.SelectionStart;
			string strPaste;
			strPaste="**"+DateTime.Today.ToString("d")+"**";
			textFinNotes.Text=textFinNotes.Text.Insert(caret,strPaste);
			textFinNotes.Select();
			textFinNotes.SelectionStart=caret+strPaste.Length;
			textFinNotes.SelectionLength=0;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			PatientNotes.Cur.FamFinancial=textFinNotes.Text;
			PatientNotes.UpdateCur();
			//Patients.GetFamily(tempPat.PatNum);//probably ok without refresh here
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}
