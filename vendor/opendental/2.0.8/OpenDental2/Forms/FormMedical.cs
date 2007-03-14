using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormMedical : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListBox listService;
		private System.Windows.Forms.ListBox listMedical;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textService;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textMedUrgNote;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textMedical;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ListBox listMedicalComp;
		private System.Windows.Forms.TextBox textMedicalComp;
		private System.ComponentModel.Container components = null;// Required designer variable.

		public FormMedical(){
			InitializeComponent();// Required for Windows Form Designer support
			Lan.C(this, new System.Windows.Forms.Control[] {
				label1,
				label5,
				label2,
				label4,
				label3,
				label6,
				label7,
				groupBox1,
				this
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
			});
		}

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
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.listService = new System.Windows.Forms.ListBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textService = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textMedUrgNote = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textMedical = new System.Windows.Forms.TextBox();
			this.listMedical = new System.Windows.Forms.ListBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textMedicalComp = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.listMedicalComp = new System.Windows.Forms.ListBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(750, 641);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 3;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.Location = new System.Drawing.Point(843, 641);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(212, 188);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 18);
			this.label1.TabIndex = 2;
			this.label1.Text = "Quick paste Service";
			// 
			// listService
			// 
			this.listService.Location = new System.Drawing.Point(213, 205);
			this.listService.Name = "listService";
			this.listService.Size = new System.Drawing.Size(120, 420);
			this.listService.TabIndex = 1;
			this.listService.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listService_MouseDown);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textService);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.textMedUrgNote);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.textMedical);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.listMedical);
			this.groupBox1.Controls.Add(this.listService);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(7, 24);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(420, 640);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "View in Chart";
			// 
			// textService
			// 
			this.textService.Location = new System.Drawing.Point(212, 85);
			this.textService.Multiline = true;
			this.textService.Name = "textService";
			this.textService.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textService.Size = new System.Drawing.Size(202, 85);
			this.textService.TabIndex = 2;
			this.textService.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(209, 67);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 16);
			this.label3.TabIndex = 50;
			this.label3.Text = "Service Notes";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(9, 25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(116, 14);
			this.label2.TabIndex = 49;
			this.label2.Text = "Urgent Medical Notes";
			// 
			// textMedUrgNote
			// 
			this.textMedUrgNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textMedUrgNote.ForeColor = System.Drawing.Color.Red;
			this.textMedUrgNote.Location = new System.Drawing.Point(9, 43);
			this.textMedUrgNote.Multiline = true;
			this.textMedUrgNote.Name = "textMedUrgNote";
			this.textMedUrgNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textMedUrgNote.Size = new System.Drawing.Size(202, 20);
			this.textMedUrgNote.TabIndex = 0;
			this.textMedUrgNote.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(9, 69);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(182, 12);
			this.label4.TabIndex = 47;
			this.label4.Text = "Medical Summary";
			// 
			// textMedical
			// 
			this.textMedical.Location = new System.Drawing.Point(9, 85);
			this.textMedical.Multiline = true;
			this.textMedical.Name = "textMedical";
			this.textMedical.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textMedical.Size = new System.Drawing.Size(202, 86);
			this.textMedical.TabIndex = 1;
			this.textMedical.Text = "";
			// 
			// listMedical
			// 
			this.listMedical.Location = new System.Drawing.Point(9, 205);
			this.listMedical.Name = "listMedical";
			this.listMedical.Size = new System.Drawing.Size(120, 420);
			this.listMedical.TabIndex = 0;
			this.listMedical.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listMedical_MouseDown);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 188);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(112, 18);
			this.label5.TabIndex = 48;
			this.label5.Text = "Quick paste Medical";
			// 
			// textMedicalComp
			// 
			this.textMedicalComp.Location = new System.Drawing.Point(562, 28);
			this.textMedicalComp.Multiline = true;
			this.textMedicalComp.Name = "textMedicalComp";
			this.textMedicalComp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textMedicalComp.Size = new System.Drawing.Size(356, 589);
			this.textMedicalComp.TabIndex = 5;
			this.textMedicalComp.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(561, 11);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(261, 18);
			this.label6.TabIndex = 6;
			this.label6.Text = "Medical History";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(437, 11);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(112, 18);
			this.label7.TabIndex = 50;
			this.label7.Text = "Quick paste Medical";
			// 
			// listMedicalComp
			// 
			this.listMedicalComp.Location = new System.Drawing.Point(438, 28);
			this.listMedicalComp.Name = "listMedicalComp";
			this.listMedicalComp.Size = new System.Drawing.Size(120, 537);
			this.listMedicalComp.TabIndex = 49;
			this.listMedicalComp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listMedicalComp_MouseDown);
			// 
			// FormMedical
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(934, 683);
			this.Controls.Add(this.listMedicalComp);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textMedicalComp);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Name = "FormMedical";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Medical";
			this.Load += new System.EventHandler(this.FormMedical_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormMedical_Load(object sender, System.EventArgs e) {
			for(int i=0;i<Defs.Short[(int)DefCat.MedicalNotes].Length;i++){
				listMedical.Items.Add(Defs.Short[(int)DefCat.MedicalNotes][i].ItemName);
				listMedicalComp.Items.Add(Defs.Short[(int)DefCat.MedicalNotes][i].ItemName);
			}
			for(int i=0;i<Defs.Short[(int)DefCat.ServiceNotes].Length;i++){
				listService.Items.Add(Defs.Short[(int)DefCat.ServiceNotes][i].ItemName);
			}
			textMedUrgNote.Text=Patients.Cur.MedUrgNote;
			textMedical.Text=PatientNotes.Cur.Medical;
			textMedicalComp.Text=PatientNotes.Cur.MedicalComp;
			textService.Text=PatientNotes.Cur.Service;
		}

		private void listService_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listService.IndexFromPoint(e.X,e.Y)==-1){
				return;
			}
			int caret=textService.SelectionStart;
			string strPaste;
			strPaste=Defs.Short[(int)DefCat.ServiceNotes][listService.IndexFromPoint(e.X,e.Y)].ItemName;
			textService.Text=textService.Text.Insert(caret,strPaste);
			textService.Select();
			textService.SelectionStart=caret+strPaste.Length;
			textService.SelectionLength=0;
			listService.SelectedIndex=-1;
		}

		private void listMedical_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listMedical.IndexFromPoint(e.X,e.Y)==-1){
				return;
			}
			int caret=textMedical.SelectionStart;
			string strPaste;
			strPaste=Defs.Short[(int)DefCat.MedicalNotes][listMedical.IndexFromPoint(e.X,e.Y)].ItemName;
			textMedical.Text=textMedical.Text.Insert(caret,strPaste);
			textMedical.Select();
			textMedical.SelectionStart=caret+strPaste.Length;
			textMedical.SelectionLength=0;
			listMedical.SelectedIndex=-1;
		}

		private void listMedicalComp_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listMedicalComp.IndexFromPoint(e.X,e.Y)==-1){
				return;
			}
			int caret=textMedicalComp.SelectionStart;
			string strPaste;
			strPaste=Defs.Short[(int)DefCat.MedicalNotes][listMedicalComp.IndexFromPoint(e.X,e.Y)].ItemName;
			textMedicalComp.Text=textMedicalComp.Text.Insert(caret,strPaste);
			textMedicalComp.Select();
			textMedicalComp.SelectionStart=caret+strPaste.Length;
			textMedicalComp.SelectionLength=0;
			listMedicalComp.SelectedIndex=-1;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			Patients.Cur.MedUrgNote=textMedUrgNote.Text;
			Patients.UpdateCur();
			PatientNotes.Cur.Medical=textMedical.Text;
			PatientNotes.Cur.Service=textService.Text;
			PatientNotes.Cur.MedicalComp=textMedicalComp.Text;
			PatientNotes.UpdateCur();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}
