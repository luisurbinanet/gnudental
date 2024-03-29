using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using OpenDentBusiness;

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
		private OpenDental.UI.Button butExport;
		private OpenDental.UI.Button butImport;
		private System.Windows.Forms.GroupBox groupBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butDefault;
		private bool changed;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClaimForms));
			this.butClose = new OpenDental.UI.Button();
			this.listClaimForms = new System.Windows.Forms.ListBox();
			this.butAdd = new OpenDental.UI.Button();
			this.butCopy = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.butExport = new OpenDental.UI.Button();
			this.butImport = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butDefault = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(320,395);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,23);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// listClaimForms
			// 
			this.listClaimForms.Location = new System.Drawing.Point(45,33);
			this.listClaimForms.Name = "listClaimForms";
			this.listClaimForms.Size = new System.Drawing.Size(203,251);
			this.listClaimForms.TabIndex = 2;
			this.listClaimForms.DoubleClick += new System.EventHandler(this.listClaimForms_DoubleClick);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.Location = new System.Drawing.Point(15,25);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75,23);
			this.butAdd.TabIndex = 3;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butCopy
			// 
			this.butCopy.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCopy.Autosize = true;
			this.butCopy.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCopy.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCopy.Location = new System.Drawing.Point(15,87);
			this.butCopy.Name = "butCopy";
			this.butCopy.Size = new System.Drawing.Size(115,23);
			this.butCopy.TabIndex = 4;
			this.butCopy.Text = "Make a Copy";
			this.butCopy.Click += new System.EventHandler(this.butCopy_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.Location = new System.Drawing.Point(15,56);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,23);
			this.butDelete.TabIndex = 5;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(44,14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(161,16);
			this.label1.TabIndex = 6;
			this.label1.Text = "Edit Claim Form";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butExport
			// 
			this.butExport.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butExport.Autosize = true;
			this.butExport.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butExport.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butExport.Location = new System.Drawing.Point(142,25);
			this.butExport.Name = "butExport";
			this.butExport.Size = new System.Drawing.Size(75,23);
			this.butExport.TabIndex = 9;
			this.butExport.Text = "Export";
			this.butExport.Click += new System.EventHandler(this.butExport_Click);
			// 
			// butImport
			// 
			this.butImport.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butImport.Autosize = true;
			this.butImport.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butImport.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butImport.Location = new System.Drawing.Point(141,56);
			this.butImport.Name = "butImport";
			this.butImport.Size = new System.Drawing.Size(75,23);
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
			this.groupBox1.Location = new System.Drawing.Point(29,298);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(232,122);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Advanced Users Only";
			// 
			// butDefault
			// 
			this.butDefault.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDefault.Autosize = true;
			this.butDefault.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDefault.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDefault.Location = new System.Drawing.Point(266,33);
			this.butDefault.Name = "butDefault";
			this.butDefault.Size = new System.Drawing.Size(87,23);
			this.butDefault.TabIndex = 12;
			this.butDefault.Text = "Set Default";
			this.butDefault.Click += new System.EventHandler(this.butDefault_Click);
			// 
			// FormClaimForms
			// 
			this.AcceptButton = this.butClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(425,451);
			this.Controls.Add(this.butDefault);
			this.Controls.Add(this.groupBox1);
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
			ClaimFormItems.Refresh();
			FillList();
		}

		///<summary></summary>
		private void FillList(){
			ClaimForms.Refresh();
			listClaimForms.Items.Clear();
			string description;
			for(int i=0;i<ClaimForms.ListLong.Length;i++){
				description=ClaimForms.ListLong[i].Description;
				if(ClaimForms.ListLong[i].IsHidden){
					description+=" (hidden)";
				}
				if(ClaimForms.ListLong[i].ClaimFormNum==PrefB.GetInt("DefaultClaimForm")) {
					description+=" (default)";
				}
				listClaimForms.Items.Add(description);
			}
		}

		private void listClaimForms_DoubleClick(object sender, System.EventArgs e) {
			if(listClaimForms.SelectedIndex==-1)
				return;
			FormClaimFormEdit FormCFE=new FormClaimFormEdit();
			FormCFE.ClaimFormCur=ClaimForms.ListLong[listClaimForms.SelectedIndex];
			FormCFE.ShowDialog();
			changed=true;//we don't really know if they changed it, but always refresh
			//ClaimFormItems refreshed within FormCFE
			FillList();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			ClaimForm ClaimFormCur=new ClaimForm();
			ClaimForms.Insert(ClaimFormCur);
			FormClaimFormEdit FormCFE=new FormClaimFormEdit();
			FormCFE.ClaimFormCur=ClaimFormCur;
			FormCFE.IsNew=true;
			FormCFE.ShowDialog();
			if(FormCFE.DialogResult!=DialogResult.OK){
				return;
			}
			changed=true;
			//ClaimFormItems refreshed within FormCFE
			FillList();
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
			if(!ClaimForms.Delete(ClaimForms.ListLong[listClaimForms.SelectedIndex])){
				MessageBox.Show(Lan.g(this,"Claim form is already in use."));
				return;
			}
			changed=true;
			ClaimFormItems.Refresh();
			FillList();
		}

		private void butCopy_Click(object sender, System.EventArgs e) {
			if(listClaimForms.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			ClaimForm ClaimFormCur=ClaimForms.ListLong[listClaimForms.SelectedIndex].Copy();
			int oldClaimFormNum=ClaimFormCur.ClaimFormNum;
			ClaimFormCur.UniqueID="";//designates it as a user added claimform
			ClaimForms.Insert(ClaimFormCur);//this duplicates the original claimform, but no items.
			int newClaimFormNum=ClaimFormCur.ClaimFormNum;
			//ClaimFormItems.GetListForForm(ClaimForms.ListLong[listClaimForms.SelectedIndex].ClaimFormNum);
			for(int i=0;i<ClaimFormCur.Items.Length;i++){
				//ClaimFormItems.Cur=ClaimFormItems.ListForForm[i];
				ClaimFormCur.Items[i].ClaimFormNum=newClaimFormNum;
				ClaimFormItems.Insert(ClaimFormCur.Items[i]);
			}
			ClaimFormItems.Refresh();
			changed=true;
			FillList();
		}

		private void butExport_Click(object sender, System.EventArgs e) {
			if(listClaimForms.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			ClaimForm ClaimFormCur=ClaimForms.ListLong[listClaimForms.SelectedIndex];
			SaveFileDialog saveDlg=new SaveFileDialog();
			string filename="ClaimForm"+ClaimFormCur.Description+".xml";
			saveDlg.InitialDirectory=PrefB.GetString("ExportPath");
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
			OpenFileDialog openDlg=new OpenFileDialog();
			openDlg.InitialDirectory=PrefB.GetString("ExportPath");
			if(openDlg.ShowDialog()!=DialogResult.OK){
				return;
			}
			try{
				ImportForm(openDlg.FileName,false);
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
				return;
			}
			MessageBox.Show("Imported");
			ClaimFormItems.Refresh();
			changed=true;
			FillList();
		}

		///<Summary>Can be called externally as part of the update sequence.  Surround with try catch.  Returns the ClaimFormNum of the new ClaimForm if it inserted a new claimform.</Summary>
		public static int ImportForm(string path, bool isUpdateSequence){
			if(!File.Exists(path)) {
				throw new ApplicationException(Lan.g("FormClaimForm","File does not exist."));
			}
			ClaimForm tempClaimForm=new ClaimForm();
			XmlSerializer serializer=new XmlSerializer(typeof(ClaimForm));
			try{
				using(TextReader reader=new StreamReader(path)){
					tempClaimForm=(ClaimForm)serializer.Deserialize(reader);
				}
			}
			catch{
				throw new ApplicationException(Lan.g("FormClaimForm","Invalid file format"));
			}
			int retVal=0;
			bool isNew=true;
			if(tempClaimForm.UniqueID!=""){//if it's blank, it's always inserted.
				for(int i=0;i<ClaimForms.ListLong.Length;i++){
					if(ClaimForms.ListLong[i].UniqueID==tempClaimForm.UniqueID){
						isNew=false;
					}
				}
			}
			if(isNew){
				ClaimForms.Insert(tempClaimForm);//now we have a primary key.
				retVal=tempClaimForm.ClaimFormNum;
				for(int j=0;j<tempClaimForm.Items.Length;j++){
					tempClaimForm.Items[j].ClaimFormNum=tempClaimForm.ClaimFormNum;
					ClaimFormItems.Insert(tempClaimForm.Items[j]);
				}
			}
			else{
				if(!isUpdateSequence){
					if(MessageBox.Show(tempClaimForm.Description+" already exists.  Replace?","",
						MessageBoxButtons.OKCancel)!=DialogResult.OK) {
						return 0;
					}
				}
				ClaimForms.UpdateByUniqueID(tempClaimForm);
			}
			return retVal;//only if uniqueID
		}

		private void butDefault_Click(object sender,EventArgs e) {
			if(listClaimForms.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a claimform from the list first.");
				return;
			}
			if(Prefs.UpdateInt("DefaultClaimForm",ClaimForms.ListLong[listClaimForms.SelectedIndex].ClaimFormNum)){
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
			FillList();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			//Prefs.Cur.PrefName="GenericEClaimsForm";
			//if(listEClaim.SelectedIndex==-1){
			//	Prefs.Cur.ValueString="";
			//}
			//else{
			//	Prefs.Cur.ValueString=POut.PInt(ClaimForms.ListShort[listEClaim.SelectedIndex].ClaimFormNum);
			//}
			//Prefs.UpdateCur();
			Close();
		}

		private void FormClaimForms_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(changed){
				DataValid.SetInvalid(InvalidTypes.ClaimForms);
			}
		}

		

		

		

		

		

		


	}
}





















