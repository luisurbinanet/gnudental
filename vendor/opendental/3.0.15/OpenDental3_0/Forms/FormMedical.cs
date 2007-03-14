using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormMedical : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button butAdd;
		private System.Windows.Forms.Label label8;
		private OpenDental.TableMedications tbMeds;
		private OpenDental.ODtextBox textMedical;
		private OpenDental.ODtextBox textService;
		private OpenDental.ODtextBox textMedicalComp;
		private OpenDental.ODtextBox textMedUrgNote;
		private System.ComponentModel.Container components = null;// Required designer variable.

		///<summary></summary>
		public FormMedical(){
			InitializeComponent();// Required for Windows Form Designer support
			tbMeds.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbMeds_CellDoubleClicked);
			Lan.C(this, new System.Windows.Forms.Control[] {
				//label1,
				//label5,
				label2,
				label4,
				label3,
				label6,
				//label7,
				label8,
				groupBox1,
				this
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
			});
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
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textMedUrgNote = new OpenDental.ODtextBox();
			this.textService = new OpenDental.ODtextBox();
			this.textMedical = new OpenDental.ODtextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.butAdd = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.tbMeds = new OpenDental.TableMedications();
			this.textMedicalComp = new OpenDental.ODtextBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(786, 650);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(879, 650);
			this.butCancel.Name = "butCancel";
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
			this.label3.Size = new System.Drawing.Size(100, 16);
			this.label3.TabIndex = 50;
			this.label3.Text = "Service Notes";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(9, 21);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(116, 14);
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
			this.butAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
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

		private void FormMedical_Load(object sender, System.EventArgs e) {
			//for(int i=0;i<Defs.Short[(int)DefCat.MedicalNotes].Length;i++){
			//	listMedical.Items.Add(Defs.Short[(int)DefCat.MedicalNotes][i].ItemName);
			//	listMedicalComp.Items.Add(Defs.Short[(int)DefCat.MedicalNotes][i].ItemName);
			//}
			//for(int i=0;i<Defs.Short[(int)DefCat.ServiceNotes].Length;i++){
			//	listService.Items.Add(Defs.Short[(int)DefCat.ServiceNotes][i].ItemName);
			//}
			textMedUrgNote.Text=Patients.Cur.MedUrgNote;
			textMedical.Text=PatientNotes.Cur.Medical;
			textMedicalComp.Text=PatientNotes.Cur.MedicalComp;
			textService.Text=PatientNotes.Cur.Service;
			FillMeds();
		}

		private void FillMeds(){
			Medications.Refresh();
			MedicationPats.Refresh();
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
			MedicationPats.Cur.PatNum=Patients.Cur.PatNum;
			MedicationPats.Cur.MedicationNum=FormM.MedicationNum;
			FormMedPat FormMP=new FormMedPat();
			FormMP.IsNew=true;
			FormMP.ShowDialog();
			if(FormMP.DialogResult!=DialogResult.OK){
				return;
			}
			FillMeds();
		}

		/*private void listService_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
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
		}*/

		private void butOK_Click(object sender, System.EventArgs e) {
			Patient PatCur=Patients.Cur;
			PatCur.MedUrgNote=textMedUrgNote.Text;
			Patients.Cur=PatCur;
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
