using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormClaimForms : System.Windows.Forms.Form{
		private System.Windows.Forms.ListBox listClaimForms;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butCopy;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox listEClaim;
		private OpenDental.UI.Button butExport;
		private OpenDental.UI.Button butImport;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.OpenFileDialog openDlg;
		private System.Windows.Forms.SaveFileDialog saveDlg;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormClaimForms()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormClaimForms));
			this.butClose = new OpenDental.UI.Button();
			this.listClaimForms = new System.Windows.Forms.ListBox();
			this.butAdd = new OpenDental.UI.Button();
			this.butCopy = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.listEClaim = new System.Windows.Forms.ListBox();
			this.butExport = new OpenDental.UI.Button();
			this.butImport = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.openDlg = new System.Windows.Forms.OpenFileDialog();
			this.saveDlg = new System.Windows.Forms.SaveFileDialog();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClose.Location = new System.Drawing.Point(320, 395);
			this.butClose.Name = "butClose";
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// listClaimForms
			// 
			this.listClaimForms.Location = new System.Drawing.Point(45, 33);
			this.listClaimForms.Name = "listClaimForms";
			this.listClaimForms.Size = new System.Drawing.Size(203, 251);
			this.listClaimForms.TabIndex = 2;
			this.listClaimForms.DoubleClick += new System.EventHandler(this.listClaimForms_DoubleClick);
			// 
			// butAdd
			// 
			this.butAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butAdd.Location = new System.Drawing.Point(15, 25);
			this.butAdd.Name = "butAdd";
			this.butAdd.TabIndex = 3;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butCopy
			// 
			this.butCopy.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCopy.Location = new System.Drawing.Point(15, 87);
			this.butCopy.Name = "butCopy";
			this.butCopy.Size = new System.Drawing.Size(115, 23);
			this.butCopy.TabIndex = 4;
			this.butCopy.Text = "Make a Copy";
			this.butCopy.Click += new System.EventHandler(this.butCopy_Click);
			// 
			// butDelete
			// 
			this.butDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butDelete.Location = new System.Drawing.Point(15, 56);
			this.butDelete.Name = "butDelete";
			this.butDelete.TabIndex = 5;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(44, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(161, 16);
			this.label1.TabIndex = 6;
			this.label1.Text = "Edit Claim Form";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(353, 25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(254, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "Select Claim Form for Generic E-Claims";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.label2.Visible = false;
			// 
			// listEClaim
			// 
			this.listEClaim.Location = new System.Drawing.Point(354, 44);
			this.listEClaim.Name = "listEClaim";
			this.listEClaim.Size = new System.Drawing.Size(203, 251);
			this.listEClaim.TabIndex = 7;
			this.listEClaim.Visible = false;
			// 
			// butExport
			// 
			this.butExport.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butExport.Location = new System.Drawing.Point(142, 25);
			this.butExport.Name = "butExport";
			this.butExport.TabIndex = 9;
			this.butExport.Text = "Export";
			this.butExport.Click += new System.EventHandler(this.butExport_Click);
			// 
			// butImport
			// 
			this.butImport.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butImport.Location = new System.Drawing.Point(141, 56);
			this.butImport.Name = "butImport";
			this.butImport.TabIndex = 10;
			this.butImport.Text = "Import";
			this.butImport.Click += new System.EventHandler(this.butImport_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butCopy);
			this.groupBox1.Controls.Add(this.butExport);
			this.groupBox1.Controls.Add(this.butAdd);
			this.groupBox1.Controls.Add(this.butImport);
			this.groupBox1.Controls.Add(this.butDelete);
			this.groupBox1.Location = new System.Drawing.Point(29, 298);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(232, 122);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Advanced Users Only";
			// 
			// FormClaimForms
			// 
			this.AcceptButton = this.butClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(425, 451);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listEClaim);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listClaimForms);
			this.Controls.Add(this.butClose);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClaimForms";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Claim Forms";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormClaimForms_Closing);
			this.Load += new System.EventHandler(this.FormClaimForms_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormClaimForms_Load(object sender, System.EventArgs e) {
			FillLists();
		}

		///<summary>Refreshes ClaimForms, then fills both lists.</summary>
		private void FillLists(){
			//ClaimFormItems.Refresh();
			ClaimForms.Refresh();
			listClaimForms.Items.Clear();
			string description;
			for(int i=0;i<ClaimForms.ListLong.Length;i++){
				description=ClaimForms.ListLong[i].Description;
				if(ClaimForms.ListLong[i].IsHidden)
					description+="(hidden)";
				listClaimForms.Items.Add(description);
			}
			listEClaim.Items.Clear();
			for(int i=0;i<ClaimForms.ListShort.Length;i++){
				listEClaim.Items.Add(ClaimForms.ListShort[i].Description);
				if(PIn.PInt(((Pref)Prefs.HList["GenericEClaimsForm"]).ValueString)
					==ClaimForms.ListShort[i].ClaimFormNum){
					listEClaim.SelectedIndex=i;
				}
			}
		}

		private void listClaimForms_DoubleClick(object sender, System.EventArgs e) {
			if(listClaimForms.SelectedIndex==-1)
				return;
			FormClaimFormEdit FormCFE=new FormClaimFormEdit();
			FormCFE.ClaimFormCur=ClaimForms.ListLong[listClaimForms.SelectedIndex];
			FormCFE.ShowDialog();
			FillLists();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			ClaimForm ClaimFormCur=new ClaimForm();
			ClaimFormCur.Insert();
			FormClaimFormEdit FormCFE=new FormClaimFormEdit();
			FormCFE.ClaimFormCur=ClaimFormCur;
			FormCFE.IsNew=true;
			FormCFE.ShowDialog();
			FillLists();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(listClaimForms.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			//ClaimForms.Cur=ClaimForms.ListLong[listClaimForms.SelectedIndex];
			if(ClaimForms.ListLong[listClaimForms.SelectedIndex].UniqueID!=""){
				MessageBox.Show(Lan.g(this,"Not allowed to delete a premade claimform, but you can hide it instead."));
				return;
			}
			if(!ClaimForms.ListLong[listClaimForms.SelectedIndex].Delete()){
				MessageBox.Show(Lan.g(this,"Claim form is already in use."));
			}
			ClaimFormItems.Refresh();
			FillLists();
		}

		private void butCopy_Click(object sender, System.EventArgs e) {
			if(listClaimForms.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			ClaimForm ClaimFormCur=ClaimForms.ListLong[listClaimForms.SelectedIndex].Copy();
			int oldClaimFormNum=ClaimFormCur.ClaimFormNum;
			ClaimFormCur.UniqueID="";//designates it as a user added claimform
			ClaimFormCur.Insert();//this duplicates the original claimform, but no items.
			int newClaimFormNum=ClaimFormCur.ClaimFormNum;
			//ClaimFormItems.GetListForForm(ClaimForms.ListLong[listClaimForms.SelectedIndex].ClaimFormNum);
			for(int i=0;i<ClaimFormCur.Items.Length;i++){
				//ClaimFormItems.Cur=ClaimFormItems.ListForForm[i];
				ClaimFormCur.Items[i].ClaimFormNum=newClaimFormNum;
				ClaimFormCur.Items[i].Insert();
			}
			ClaimFormItems.Refresh();
			FillLists();
		}

		private void butExport_Click(object sender, System.EventArgs e) {
			if(listClaimForms.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			ClaimForm ClaimFormCur=ClaimForms.ListLong[listClaimForms.SelectedIndex];
			saveDlg=new SaveFileDialog();
			string filename="ClaimForm"+ClaimFormCur.Description+".xml";
			saveDlg.InitialDirectory=Prefs.GetString("ExportPath");
			saveDlg.FileName=filename;
			if(saveDlg.ShowDialog()!=DialogResult.OK){
				return;
			}
			//MessageBox.Show(saveDlg.FileName);
			XmlSerializer serializer=new XmlSerializer(typeof(ClaimForm));
			TextWriter writer=new StreamWriter(saveDlg.FileName);
			serializer.Serialize(writer,ClaimFormCur);
      writer.Close();
			MessageBox.Show("Exported");
		}

		private void butImport_Click(object sender, System.EventArgs e) {
			openDlg=new OpenFileDialog();
			openDlg.InitialDirectory=Prefs.GetString("ExportPath");
			if(openDlg.ShowDialog()!=DialogResult.OK){
				return;
			}
			if(!File.Exists(openDlg.FileName)){
				MessageBox.Show("File does not exist.");
				return;
			}
			XmlSerializer serializer=new XmlSerializer(typeof(ClaimForm));
			TextReader reader=new StreamReader(openDlg.FileName);
			ClaimForm tempClaimForm=new ClaimForm();
			try{
				tempClaimForm=(ClaimForm)serializer.Deserialize(reader);
			}
			catch{
				MessageBox.Show("Invalid file format");
				return;
			}
			bool isNew=true;
			if(tempClaimForm.UniqueID!=""){//if it's blank, it's always inserted.
				for(int i=0;i<ClaimForms.ListLong.Length;i++){
					if(ClaimForms.ListLong[i].UniqueID==tempClaimForm.UniqueID){
						isNew=false;
					}
				}
			}
			if(isNew){
				tempClaimForm.Insert();//now we have a primary key.
				for(int j=0;j<tempClaimForm.Items.Length;j++){
					tempClaimForm.Items[j].ClaimFormNum=tempClaimForm.ClaimFormNum;
					tempClaimForm.Items[j].Insert();
				}
			}
			else{
				if(MessageBox.Show(tempClaimForm.Description+" already exists.  Replace?","",
					MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;
				}
				tempClaimForm.UpdateByUniqueID();
			}
			MessageBox.Show("Imported");
			ClaimFormItems.Refresh();
			FillLists();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Prefs.Cur.PrefName="GenericEClaimsForm";
			if(listEClaim.SelectedIndex==-1){
				Prefs.Cur.ValueString="";
			}
			else{
				Prefs.Cur.ValueString=POut.PInt(ClaimForms.ListShort[listEClaim.SelectedIndex].ClaimFormNum);
			}
			Prefs.UpdateCur();
			Close();
		}

		private void FormClaimForms_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
		}

		

		

		

		

		


	}
}





















