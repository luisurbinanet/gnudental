using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormClaimForms : System.Windows.Forms.Form{
		private System.Windows.Forms.ListBox listClaimForms;
		private System.Windows.Forms.Button butAdd;
		private System.Windows.Forms.Button butClose;
		private System.Windows.Forms.Button butCopy;
		private System.Windows.Forms.Button butDelete;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox listEClaim;
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
			Lan.C(this, new System.Windows.Forms.Control[] {
				this,
				label1,
				label2
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				//butOK,
				//butCancel,
				butAdd
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormClaimForms));
			this.butClose = new System.Windows.Forms.Button();
			this.listClaimForms = new System.Windows.Forms.ListBox();
			this.butAdd = new System.Windows.Forms.Button();
			this.butCopy = new System.Windows.Forms.Button();
			this.butDelete = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.listEClaim = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClose.Location = new System.Drawing.Point(460, 362);
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
			this.butAdd.Location = new System.Drawing.Point(46, 298);
			this.butAdd.Name = "butAdd";
			this.butAdd.TabIndex = 3;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butCopy
			// 
			this.butCopy.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCopy.Location = new System.Drawing.Point(46, 360);
			this.butCopy.Name = "butCopy";
			this.butCopy.Size = new System.Drawing.Size(115, 23);
			this.butCopy.TabIndex = 4;
			this.butCopy.Text = "Make a Copy";
			this.butCopy.Click += new System.EventHandler(this.butCopy_Click);
			// 
			// butDelete
			// 
			this.butDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butDelete.Location = new System.Drawing.Point(46, 329);
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
			this.label2.Location = new System.Drawing.Point(330, 14);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(254, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "Select Claim Form for Generic E-Claims";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listEClaim
			// 
			this.listEClaim.Location = new System.Drawing.Point(331, 33);
			this.listEClaim.Name = "listEClaim";
			this.listEClaim.Size = new System.Drawing.Size(203, 251);
			this.listEClaim.TabIndex = 7;
			// 
			// FormClaimForms
			// 
			this.AcceptButton = this.butClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(566, 411);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listEClaim);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCopy);
			this.Controls.Add(this.butAdd);
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
			this.ResumeLayout(false);

		}
		#endregion

		private void FormClaimForms_Load(object sender, System.EventArgs e) {
			FillLists();
		}

		private void FillLists(){
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
			ClaimForms.Cur=ClaimForms.ListLong[listClaimForms.SelectedIndex];
			FormClaimFormEdit FormCFE=new FormClaimFormEdit();
			FormCFE.ShowDialog();
			FillLists();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			ClaimForms.Cur=new ClaimForm();
			ClaimForms.InsertCur();
			FormClaimFormEdit FormCFE=new FormClaimFormEdit();
			FormCFE.IsNew=true;
			FormCFE.ShowDialog();
			FillLists();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(listClaimForms.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			ClaimForms.Cur=ClaimForms.ListLong[listClaimForms.SelectedIndex];
			if(ClaimForms.Cur.UniqueID!=0){
				MessageBox.Show(Lan.g(this,"Not allowed to delete a premade claimform, but you can hide it instead."));
				return;
			}
			if(!ClaimForms.DeleteCur()){
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
			ClaimForms.Cur=ClaimForms.ListLong[listClaimForms.SelectedIndex];
			int oldClaimFormNum=ClaimForms.Cur.ClaimFormNum;
			ClaimForms.Cur.UniqueID=0;//designates it as a user added claimform
			ClaimForms.InsertCur();//this duplicates the original claimform, but no items.
			int newClaimFormNum=ClaimForms.Cur.ClaimFormNum;
			ClaimForms.Cur=ClaimForms.ListLong[listClaimForms.SelectedIndex];//switch back to old claimform
			ClaimFormItems.GetListForForm();
			for(int i=0;i<ClaimFormItems.ListForForm.Length;i++){
				ClaimFormItems.Cur=ClaimFormItems.ListForForm[i];
				ClaimFormItems.Cur.ClaimFormNum=newClaimFormNum;
				ClaimFormItems.InsertCur();
			}
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





















