using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormMedPat : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textMedName;
		private System.Windows.Forms.TextBox textGenericName;
		private System.Windows.Forms.TextBox textMedNote;
		private System.Windows.Forms.TextBox textPatNote;
		private System.Windows.Forms.Button butRemove;
		private System.Windows.Forms.Button butEdit;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew;

		///<summary></summary>
		public FormMedPat()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormMedPat));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textMedName = new System.Windows.Forms.TextBox();
			this.textGenericName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textMedNote = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textPatNote = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.butEdit = new System.Windows.Forms.Button();
			this.butRemove = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(563, 474);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(461, 474);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(38, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(145, 17);
			this.label1.TabIndex = 2;
			this.label1.Text = "Drug Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textMedName
			// 
			this.textMedName.Location = new System.Drawing.Point(183, 22);
			this.textMedName.Name = "textMedName";
			this.textMedName.ReadOnly = true;
			this.textMedName.Size = new System.Drawing.Size(348, 20);
			this.textMedName.TabIndex = 3;
			this.textMedName.Text = "";
			// 
			// textGenericName
			// 
			this.textGenericName.Location = new System.Drawing.Point(183, 57);
			this.textGenericName.Name = "textGenericName";
			this.textGenericName.ReadOnly = true;
			this.textGenericName.Size = new System.Drawing.Size(348, 20);
			this.textGenericName.TabIndex = 5;
			this.textGenericName.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(19, 60);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(163, 17);
			this.label2.TabIndex = 4;
			this.label2.Text = "Generic Name";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textMedNote
			// 
			this.textMedNote.Location = new System.Drawing.Point(183, 92);
			this.textMedNote.Multiline = true;
			this.textMedNote.Name = "textMedNote";
			this.textMedNote.ReadOnly = true;
			this.textMedNote.Size = new System.Drawing.Size(348, 105);
			this.textMedNote.TabIndex = 7;
			this.textMedNote.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(9, 96);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(174, 17);
			this.label3.TabIndex = 6;
			this.label3.Text = "Medication Notes";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPatNote
			// 
			this.textPatNote.AcceptsReturn = true;
			this.textPatNote.Location = new System.Drawing.Point(252, 319);
			this.textPatNote.Multiline = true;
			this.textPatNote.Name = "textPatNote";
			this.textPatNote.Size = new System.Drawing.Size(348, 105);
			this.textPatNote.TabIndex = 9;
			this.textPatNote.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(66, 320);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(182, 17);
			this.label4.TabIndex = 8;
			this.label4.Text = "Notes for this Patient";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.butEdit);
			this.groupBox1.Controls.Add(this.textMedNote);
			this.groupBox1.Controls.Add(this.textMedName);
			this.groupBox1.Controls.Add(this.textGenericName);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(71, 34);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(565, 264);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Medication";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(266, 218);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(128, 28);
			this.label6.TabIndex = 11;
			this.label6.Text = "(edit this medication for all patients)";
			// 
			// butEdit
			// 
			this.butEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butEdit.Location = new System.Drawing.Point(183, 218);
			this.butEdit.Name = "butEdit";
			this.butEdit.Size = new System.Drawing.Size(75, 26);
			this.butEdit.TabIndex = 9;
			this.butEdit.Text = "&Edit";
			this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
			// 
			// butRemove
			// 
			this.butRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butRemove.Location = new System.Drawing.Point(49, 474);
			this.butRemove.Name = "butRemove";
			this.butRemove.Size = new System.Drawing.Size(75, 26);
			this.butRemove.TabIndex = 8;
			this.butRemove.Text = "&Remove";
			this.butRemove.Click += new System.EventHandler(this.butRemove_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(20, 502);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(127, 43);
			this.label5.TabIndex = 10;
			this.label5.Text = "(remove this medication from this patient)";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// FormMedPat
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(685, 540);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textPatNote);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butRemove);
			this.Controls.Add(this.label5);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMedPat";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Medication for Patient";
			this.Load += new System.EventHandler(this.FormMedPat_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormMedPat_Load(object sender, System.EventArgs e) {
			FillForm();
		}

		private void FillForm(){
			textMedName.Text=((Medication)Medications.HList[MedicationPats.Cur.MedicationNum]).MedName;
			textGenericName.Text=((Medication)Medications.HList[
					((Medication)Medications.HList[MedicationPats.Cur.MedicationNum]).GenericNum]).MedName;
			textMedNote.Text=((Medication)Medications.HList[
					((Medication)Medications.HList[MedicationPats.Cur.MedicationNum]).GenericNum]).Notes;
			textPatNote.Text=MedicationPats.Cur.PatNote;
		}

		private void butEdit_Click(object sender, System.EventArgs e) {
			Medications.Cur=(Medication)Medications.HList[MedicationPats.Cur.MedicationNum];
			FormMedicationEdit FormME=new FormMedicationEdit();
			FormME.ShowDialog();
			if(FormME.DialogResult!=DialogResult.OK){
				return;
			}
			FillForm();
		}

		private void butRemove_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g
				(this,"Remove this medication from this patient?  Patient notes will be lost.")
				,"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
			{
				return;
			}
			MedicationPats.DeleteCur();
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			MedicationPats.Cur.PatNote=textPatNote.Text;
			if(IsNew){
				MedicationPats.InsertCur();
			}
			else{
				MedicationPats.UpdateCur();
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		


	}
}





















