using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormNoteFinUrg : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.TextBox textUrgFinNote;
		private System.ComponentModel.Container components = null;

		public FormNoteFinUrg(){
			InitializeComponent();
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
			this.textUrgFinNote = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(268, 170);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 20);
			this.butCancel.TabIndex = 39;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(268, 132);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 38;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textUrgFinNote
			// 
			this.textUrgFinNote.BackColor = System.Drawing.Color.White;
			this.textUrgFinNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textUrgFinNote.ForeColor = System.Drawing.Color.Red;
			this.textUrgFinNote.Location = new System.Drawing.Point(66, 44);
			this.textUrgFinNote.MaxLength = 255;
			this.textUrgFinNote.Multiline = true;
			this.textUrgFinNote.Name = "textUrgFinNote";
			this.textUrgFinNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textUrgFinNote.Size = new System.Drawing.Size(198, 36);
			this.textUrgFinNote.TabIndex = 40;
			this.textUrgFinNote.Text = "";
			// 
			// FormNoteFinUrg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(368, 210);
			this.Controls.Add(this.textUrgFinNote);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Name = "FormNoteFinUrg";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = Lan.g(this,"Edit Urgent Family Financial Note");
			this.Load += new System.EventHandler(this.FormNoteFinUrg_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormNoteFinUrg_Load(object sender, System.EventArgs e) {
			textUrgFinNote.Text=Patients.FamilyList[Patients.GuarIndex].FamFinUrgNote;
			textUrgFinNote.Select();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			Patient tempPat=Patients.Cur;
			Patients.Cur=Patients.FamilyList[Patients.GuarIndex];
			Patients.Cur.FamFinUrgNote=textUrgFinNote.Text;
			Patients.UpdateCur();
			Patients.GetFamily(tempPat.PatNum);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


	}
}
