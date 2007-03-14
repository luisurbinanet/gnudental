using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormNoteApptMod : System.Windows.Forms.Form{
		private System.Windows.Forms.TextBox textApptModNote;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.ComponentModel.Container components = null;
		private Patients Patients=new Patients();

		///<summary></summary>
		public FormNoteApptMod(){
			InitializeComponent();

			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
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
			this.textApptModNote = new System.Windows.Forms.TextBox();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textApptModNote
			// 
			this.textApptModNote.AcceptsReturn = true;
			this.textApptModNote.Location = new System.Drawing.Point(50, 32);
			this.textApptModNote.Multiline = true;
			this.textApptModNote.Name = "textApptModNote";
			this.textApptModNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textApptModNote.Size = new System.Drawing.Size(202, 38);
			this.textApptModNote.TabIndex = 0;
			this.textApptModNote.Text = "";
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(236, 92);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(236, 127);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormNoteApptMod
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(324, 160);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.textApptModNote);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormNoteApptMod";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Appointment Module Note";
			this.Load += new System.EventHandler(this.FormNoteApptMod_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormNoteApptMod_Load(object sender, System.EventArgs e) {
			textApptModNote.Text=Patients.Cur.ApptModNote;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			Patients.Cur.ApptModNote=textApptModNote.Text;
			Patients.UpdateCur();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
		
	}
}
