using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormMedical : System.Windows.Forms.Form{
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private OpenDental.UI.Button butAdd;
		private System.Windows.Forms.Label label8;
		private OpenDental.TableMedications tbMeds;
		private OpenDental.ODtextBox textMedical;
		private OpenDental.ODtextBox textService;
		private OpenDental.ODtextBox textMedicalComp;
		private OpenDental.ODtextBox textMedUrgNote;
		private System.ComponentModel.Container components = null;// Required designer variable.
		private Patient PatCur;

		///<summary></summary>
		public FormMedical(Patient patCur){
			InitializeComponent();// Required for Windows Form Designer support
			PatCur=patCur;
			tbMeds.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbMeds_CellDoubleClicked);
			Lan.F(this);
		}

		///<summary></summary>
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
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textMedUrgNote = new OpenDental.ODtextBox();
			this.textService = new OpenDental.ODtextBox();
			this.textMedical = new OpenDental.ODtextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.butAdd = new OpenDental.UI.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.tbMeds = new OpenDental.TableMedications();
			this.textMedicalComp = new OpenDental.ODtextBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(786, 650);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 25);
			this.butOK.TabIndex = 0;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(879, 650);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 25);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textMedUrgNote);
			this.groupBox1.Controls.Add(this.textService);
			this.groupBox1.Controls.Add(this.textMedical);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(7, 226);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(420, 142);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "View in Chart";
			// 
			// textMedUrgNote
			// 
			this.textMedUrgNote.AcceptsReturn = true;
			this.textMedUrgNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textMedUrgNote.ForeColor = System.Drawing.Color.Red;
			this.textMedUrgNote.Location = new System.Drawing.Point(10, 37);
			this.textMedUrgNote.Multiline = true;
			this.textMedUrgNote.Name = "textMedUrgNote";
			this.textMedUrgNote.QuickPasteType = OpenDental.QuickPasteType.MedicalUrgent;
			this.textMedUrgNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textMedUrgNote.Size = new System.Drawing.Size(202, 21);
			this.textMedUrgNote.TabIndex = 53;
			this.textMedUrgNote.Text = "";
			// 
			// textService
			// 
			this.textService.AcceptsReturn = true;
			this.textService.Location = new System.Drawing.Point(213, 78);
			this.textService.Multiline = true;
			this.textService.Name = "textService";
			this.textService.QuickPasteType = OpenDental.QuickPasteType.ServiceNotes;
			this.textService.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textService.Size = new System.Drawing.Size(202, 57);
			this.textService.TabIndex = 52;
			this.textService.Text = "";
			// 
			// textMedical
			// 
			this.textMedical.AcceptsReturn = true;
			this.textMedical.Location = new System.Drawing.Point(9, 78);
			this.textMedical.Multiline = true;
			this.textMedical.Name = "textMedical";
			this.textMedical.QuickPasteType = OpenDental.QuickPasteType.MedicalSummary;
			this.textMedical.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textMedical.Size = new System.Drawing.Size(202, 57);
			this.textMedical.TabIndex = 51;
			this.textMedical.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(211, 63);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(174, 16);
			this.label3.TabIndex = 50;
			this.label3.Text = "Service Notes";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(9, 21);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(190, 14);
			this.label2.TabIndex = 49;
			this.label2.Text = "Urgent Medical Notes";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(9, 63);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(182, 12);
			this.label4.TabIndex = 47;
			this.label4.Text = "Medical Summary";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(439, 212);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(261, 18);
			this.label6.TabIndex = 6;
			this.label6.Text = "Medical History";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.Location = new System.Drawing.Point(142, 0);
			this.butAdd.Name = "butAdd";
			this.butAdd.TabIndex = 51;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(2, 6);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(128, 16);
			this.label8.TabIndex = 52;
			this.label8.Text = "Medications";
			// 
			// tbMeds
			// 
			this.tbMeds.BackColor = System.Drawing.SystemColors.Window;
			this.tbMeds.Location = new System.Drawing.Point(3, 24);
			this.tbMeds.Name = "tbMeds";
			this.tbMeds.ScrollValue = 544;
			this.tbMeds.SelectedIndices = new int[0];
			this.tbMeds.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbMeds.Size = new System.Drawing.Size(959, 175);
			this.tbMeds.TabIndex = 53;
			// 
			// textMedicalComp
			// 
			this.textMedicalComp.AcceptsReturn = true;
			this.textMedicalComp.Location = new System.Drawing.Point(440, 232);
			this.textMedicalComp.Multiline = true;
			this.textMedicalComp.Name = "textMedicalComp";
			this.textMedicalComp.QuickPasteType = OpenDental.QuickPasteType.MedicalHistory;
			this.textMedicalComp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textMedicalComp.Size = new System.Drawing.Size(512, 409);
			this.textMedicalComp.TabIndex = 54;
			this.textMedicalComp.Text = "";
			// 
			// FormMedical
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(964, 683);
			this.Controls.Add(this.textMedicalComp);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.tbMeds);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMedical";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Medical";
			this.Load += new System.EventHandler(this.FormMedical_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormMedical_Load(object sender, System.EventArgs e){
			textMedUrgNote.Text=PatCur.MedUrgNote;
			textMedical.Text=PatientNotes.Cur.Medical;
			textMedicalComp.Text=PatientNotes.Cur.MedicalComp;
			textService.Text=PatientNotes.Cur.Service;
			FillMeds();
		}

		private void FillMeds(){
			Medications.Refresh();
			MedicationPats.Refresh(PatCur.PatNum);
			tbMeds.ResetRows(MedicationPats.List.Length);
			tbMeds.SetGridColor(Color.Gray);
			tbMeds.SetBackGColor(Color.White);  
			for(int i=0;i<MedicationPats.List.Length;i++){
				tbMeds.Cell[0,i]=((Medication)Medications.HList[MedicationPats.List[i].MedicationNum]).MedName;
				tbMeds.Cell[1,i]=((Medication)Medications.HList[
					((Medication)Medications.HList[MedicationPats.List[i].MedicationNum]).GenericNum]).MedName;
				tbMeds.Cell[2,i]=((Medication)Medications.HList[
					((Medication)Medications.HList[MedicationPats.List[i].MedicationNum]).GenericNum]).Notes;
				tbMeds.Cell[3,i]=MedicationPats.List[i].PatNote;
			}
			tbMeds.LayoutTables(); 
		}

		private void tbMeds_CellDoubleClicked(object sender, CellEventArgs e){
			MedicationPats.Cur=MedicationPats.List[e.Row];
			FormMedPat FormMP=new FormMedPat();
			FormMP.ShowDialog();
			FillMeds();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			//select medication from list.  Additional meds can be added to the list from within that dlg
			FormMedications FormM=new FormMedications();
			FormM.SelectMode=true;
			FormM.ShowDialog();
			if(FormM.DialogResult!=DialogResult.OK){
				return;
			}
			MedicationPats.Cur=new MedicationPat();
			MedicationPats.Cur.PatNum=PatCur.PatNum;
			MedicationPats.Cur.MedicationNum=FormM.MedicationNum;
			FormMedPat FormMP=new FormMedPat();
			FormMP.IsNew=true;
			FormMP.ShowDialog();
			if(FormMP.DialogResult!=DialogResult.OK){
				return;
			}
			FillMeds();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			Patient PatOld=PatCur.Copy();
			PatCur.MedUrgNote=textMedUrgNote.Text;
			PatCur.Update(PatOld);
			PatientNotes.Cur.Medical=textMedical.Text;
			PatientNotes.Cur.Service=textService.Text;
			PatientNotes.Cur.MedicalComp=textMedicalComp.Text;
			PatientNotes.UpdateCur(PatCur.Guarantor);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

	}
}
