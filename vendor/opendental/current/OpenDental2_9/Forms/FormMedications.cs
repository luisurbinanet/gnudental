using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormMedications : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.ListBox listMeds;
		private System.Windows.Forms.Button butAdd;
		/// <summary>Required designer variable.</summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool SelectGenericMode;
		///<summary></summary>
		public bool SelectMode;
		private System.Windows.Forms.Label label1;
		///<summary>the number returned if using either of the select modes.</summary>
		public int MedicationNum;

		///<summary></summary>
		public FormMedications()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormMedications));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.listMeds = new System.Windows.Forms.ListBox();
			this.butAdd = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(858, 635);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(858, 594);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// listMeds
			// 
			this.listMeds.Location = new System.Drawing.Point(8, 25);
			this.listMeds.MultiColumn = true;
			this.listMeds.Name = "listMeds";
			this.listMeds.Size = new System.Drawing.Size(841, 628);
			this.listMeds.TabIndex = 2;
			this.listMeds.DoubleClick += new System.EventHandler(this.listMeds_DoubleClick);
			// 
			// butAdd
			// 
			this.butAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butAdd.Location = new System.Drawing.Point(858, 524);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75, 26);
			this.butAdd.TabIndex = 3;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(9, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(506, 17);
			this.label1.TabIndex = 4;
			this.label1.Text = "(medications marked with a * are missing notes)";
			// 
			// FormMedications
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(941, 671);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.listMeds);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMedications";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Medications";
			this.Load += new System.EventHandler(this.FormMedications_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormMedications_Load(object sender, System.EventArgs e) {
			//not refreshed in localdata
			FillMedList();
			if(SelectGenericMode){
				this.Text=Lan.g(this,"Select Generic Medication");
				//butAdd.Visible=false;//visible, but it ONLY lets you add a generic
			}
			if(SelectMode){
				this.Text=Lan.g(this,"Select Medication");
			}
		}

		private void FillMedList(){
			listMeds.Items.Clear();
			if(SelectGenericMode){
				Medications.RefreshGeneric();
			}
			else{//SelectMode and standard
				Medications.Refresh();
			}
			string s;
			for(int i=0;i<Medications.List.Length;i++){
				s=Medications.List[i].MedName;
				if(Medications.List[i].MedicationNum==Medications.List[i].GenericNum
					&& Medications.List[i].Notes=="")
				{
					s+=" *";
				}
				listMeds.Items.Add(s);
			}
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			Medications.Cur=new Medication();
			Medications.InsertCur();//so that we will have the primary key
			Medications.Cur.GenericNum=Medications.Cur.MedicationNum;//new meds start out as generics until changed
			FormMedicationEdit FormME=new FormMedicationEdit();
			FormME.IsNew=true;
			if(SelectGenericMode){
				FormME.GenericOnly=true;
			}
			FormME.ShowDialog();
			FillMedList();
		}

		private void listMeds_DoubleClick(object sender, System.EventArgs e) {
			if(SelectGenericMode){
				MedicationNum=Medications.List[listMeds.SelectedIndex].MedicationNum;
				DialogResult=DialogResult.OK;
			}
			else if(SelectMode){
				MedicationNum=Medications.List[listMeds.SelectedIndex].MedicationNum;
				DialogResult=DialogResult.OK;
			}
			else{//normal mode from main menu
				//edit
				Medications.Cur=Medications.List[listMeds.SelectedIndex];
				FormMedicationEdit FormME=new FormMedicationEdit();
				FormME.ShowDialog();
				FillMedList();
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(SelectGenericMode){
				if(listMeds.SelectedIndex==-1){
					MessageBox.Show(Lan.g(this,"Please select an item first."));
					return;
				}
				MedicationNum=Medications.List[listMeds.SelectedIndex].MedicationNum;
			}
			else if(SelectMode){
				if(listMeds.SelectedIndex==-1){
					MessageBox.Show(Lan.g(this,"Please select an item first."));
					return;
				}
				MedicationNum=Medications.List[listMeds.SelectedIndex].MedicationNum;
			}
			else{//normal mode from main menu
				//just close
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		


	}
}





















