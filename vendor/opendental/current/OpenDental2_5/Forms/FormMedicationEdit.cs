using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormMedicationEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textGenericName;
		private System.Windows.Forms.CheckBox checkIsGeneric;
		private System.Windows.Forms.Button butChange;
		private System.Windows.Forms.TextBox textMedName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textNotes;
		private System.Windows.Forms.Label label3;
		///<summary>Required designer variable.</summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew;
		///<summary></summary>
		public bool GenericOnly;//does not allow changing to non generic.

		///<summary></summary>
		public FormMedicationEdit()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormMedicationEdit));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textGenericName = new System.Windows.Forms.TextBox();
			this.checkIsGeneric = new System.Windows.Forms.CheckBox();
			this.butChange = new System.Windows.Forms.Button();
			this.textMedName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textNotes = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(558, 411);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(558, 373);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(94, 78);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(127, 17);
			this.label1.TabIndex = 2;
			this.label1.Text = "Generic name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textGenericName
			// 
			this.textGenericName.Location = new System.Drawing.Point(222, 75);
			this.textGenericName.Name = "textGenericName";
			this.textGenericName.ReadOnly = true;
			this.textGenericName.Size = new System.Drawing.Size(248, 20);
			this.textGenericName.TabIndex = 3;
			this.textGenericName.Text = "";
			// 
			// checkIsGeneric
			// 
			this.checkIsGeneric.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsGeneric.Checked = true;
			this.checkIsGeneric.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkIsGeneric.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsGeneric.Location = new System.Drawing.Point(20, 102);
			this.checkIsGeneric.Name = "checkIsGeneric";
			this.checkIsGeneric.Size = new System.Drawing.Size(216, 20);
			this.checkIsGeneric.TabIndex = 4;
			this.checkIsGeneric.Text = "This is the generic name";
			this.checkIsGeneric.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsGeneric.Click += new System.EventHandler(this.checkIsGeneric_Click);
			// 
			// butChange
			// 
			this.butChange.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butChange.Location = new System.Drawing.Point(480, 74);
			this.butChange.Name = "butChange";
			this.butChange.Size = new System.Drawing.Size(80, 23);
			this.butChange.TabIndex = 5;
			this.butChange.Text = "C&hange";
			this.butChange.Click += new System.EventHandler(this.butChange_Click);
			// 
			// textMedName
			// 
			this.textMedName.Location = new System.Drawing.Point(222, 23);
			this.textMedName.Name = "textMedName";
			this.textMedName.Size = new System.Drawing.Size(248, 20);
			this.textMedName.TabIndex = 0;
			this.textMedName.Text = "";
			this.textMedName.TextChanged += new System.EventHandler(this.textMedName_TextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(94, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(127, 17);
			this.label2.TabIndex = 6;
			this.label2.Text = "Drug name";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textNotes
			// 
			this.textNotes.AcceptsReturn = true;
			this.textNotes.Location = new System.Drawing.Point(222, 162);
			this.textNotes.Multiline = true;
			this.textNotes.Name = "textNotes";
			this.textNotes.Size = new System.Drawing.Size(409, 179);
			this.textNotes.TabIndex = 1;
			this.textNotes.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(93, 165);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(127, 17);
			this.label3.TabIndex = 8;
			this.label3.Text = "Notes";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormMedicationEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(664, 455);
			this.Controls.Add(this.textNotes);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textMedName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butChange);
			this.Controls.Add(this.checkIsGeneric);
			this.Controls.Add(this.textGenericName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMedicationEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Medication";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormMedicationEdit_Closing);
			this.Load += new System.EventHandler(this.MedicationEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void MedicationEdit_Load(object sender, System.EventArgs e) {
			if(GenericOnly){
				this.Text=Lan.g(this,"Edit Generic Medication");
				checkIsGeneric.Visible=false;
				butChange.Visible=false;
			}
			//Medications.Refresh() should have already been run.
			if(Medications.Cur.MedicationNum==Medications.Cur.GenericNum){
				checkIsGeneric.Checked=true;
			}
			else
				checkIsGeneric.Checked=false;
			FillForm();
		}

		private void FillForm(){
			textMedName.Text=Medications.Cur.MedName;
			if(checkIsGeneric.Checked){
				textGenericName.Text=Medications.Cur.MedName;
				textNotes.Text=Medications.Cur.Notes;
				butChange.Enabled=false;
				textNotes.ReadOnly=false;
			}
			else{
				textGenericName.Text=((Medication)Medications.HList[Medications.Cur.GenericNum]).MedName;
				textNotes.Text=((Medication)Medications.HList[Medications.Cur.GenericNum]).Notes;
				butChange.Enabled=true;
				textNotes.ReadOnly=true;
			}
		}

		private void textMedName_TextChanged(object sender, System.EventArgs e) {
			//this causes immediate display update with each keypress
			Medications.Cur.MedName=textMedName.Text;
			if(checkIsGeneric.Checked){
				textGenericName.Text=Medications.Cur.MedName;
			}
		}

		private void checkIsGeneric_Click(object sender, System.EventArgs e) {
			//the state will have already changed at this point.
			if(checkIsGeneric.Checked){
				Medications.Cur.GenericNum=Medications.Cur.MedicationNum;

			}
			else{//switching to not generic
				if(MessageBox.Show(Lan.g(this,"Any notes will be lost.  Please select the generic drug from the list that will come up next")
					,"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
					checkIsGeneric.Checked=true;
					return;
				}
				int curMedNum=Medications.Cur.MedicationNum;
				FormMedications FormM=new FormMedications();
				FormM.SelectGenericMode=true;
				FormM.ShowDialog();
				if(FormM.DialogResult==DialogResult.OK){
					Medications.Refresh();
					Medications.Cur=(Medication)Medications.HList[curMedNum];
					Medications.Cur.GenericNum=FormM.MedicationNum;
				}
				else{
					Medications.Refresh();
					Medications.Cur=(Medication)Medications.HList[curMedNum];
					Medications.Cur.GenericNum=Medications.Cur.MedicationNum;
				}
			}
			FillForm();
		}

		private void butChange_Click(object sender, System.EventArgs e) {
			int curMed=Medications.Cur.MedicationNum;
			FormMedications FormM=new FormMedications();
			FormM.SelectGenericMode=true;
			FormM.ShowDialog();
			Medications.Refresh();
			Medications.Cur=(Medication)Medications.HList[curMed];
			if(FormM.DialogResult==DialogResult.OK){
				Medications.Cur.GenericNum=FormM.MedicationNum;
			}
			FillForm();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//MedName already handled
			//generic num already handled
			if(checkIsGeneric.Checked){
				Medications.Cur.Notes=textNotes.Text;
			}
			else{
				Medications.Cur.Notes="";
			}
			Medications.UpdateCur();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormMedicationEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNew){
				Medications.DeleteCur();
			}
		}

		

		

		

		


	}
}





















