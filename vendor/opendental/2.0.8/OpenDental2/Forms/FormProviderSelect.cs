using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormProviderSelect : System.Windows.Forms.Form{
		private System.Windows.Forms.ListBox listProviders;
		private System.Windows.Forms.Button butClose;
		private System.Windows.Forms.Button butDown;
		private System.Windows.Forms.Button butAdd;
		private System.Windows.Forms.Button butUp;
		private System.ComponentModel.Container components = null;
		
		public FormProviderSelect(){
			InitializeComponent();
			Providers.Selected=-1;
			Lan.C("All", new System.Windows.Forms.Control[] {
				butClose,
				butAdd,
				butDown,
				butUp
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormProviderSelect));
			this.listProviders = new System.Windows.Forms.ListBox();
			this.butClose = new System.Windows.Forms.Button();
			this.butDown = new System.Windows.Forms.Button();
			this.butAdd = new System.Windows.Forms.Button();
			this.butUp = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listProviders
			// 
			this.listProviders.Location = new System.Drawing.Point(16, 22);
			this.listProviders.Name = "listProviders";
			this.listProviders.Size = new System.Drawing.Size(120, 316);
			this.listProviders.TabIndex = 4;
			this.listProviders.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listProviders_MouseDown);
			this.listProviders.DoubleClick += new System.EventHandler(this.listProviders_DoubleClick);
			// 
			// butClose
			// 
			this.butClose.Location = new System.Drawing.Point(274, 413);
			this.butClose.Name = "butClose";
			this.butClose.TabIndex = 3;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butDown
			// 
			this.butDown.Image = ((System.Drawing.Image)(resources.GetObject("butDown.Image")));
			this.butDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDown.Location = new System.Drawing.Point(106, 412);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(73, 26);
			this.butDown.TabIndex = 2;
			this.butDown.Text = "        Down";
			this.butDown.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// butAdd
			// 
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(18, 368);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(74, 26);
			this.butAdd.TabIndex = 0;
			this.butAdd.Text = "           Add";
			this.butAdd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butUp
			// 
			this.butUp.Image = ((System.Drawing.Image)(resources.GetObject("butUp.Image")));
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(18, 412);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(74, 26);
			this.butUp.TabIndex = 1;
			this.butUp.Text = "         Up";
			this.butUp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// FormProviderSelect
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(372, 472);
			this.ControlBox = false;
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.listProviders);
			this.Controls.Add(this.butDown);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butUp);
			this.Name = "FormProviderSelect";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Providers";
			this.Load += new System.EventHandler(this.FormProviderSelect_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormProviderSelect_Load(object sender, System.EventArgs e) {
			//if Security Administration permission has not been enabled, then allow access
			Permissions.GetCur("Security Administration");
			if(!Permissions.Cur.RequiresPassword){
				FillList();
				return;
			}
			//whether or not Providers requires a password, since Security Admin has been enabled,
			//verify password so that the security box can be hidden if no permission for that.
			FormPassword FormP=new FormPassword();
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK){
				DialogResult=DialogResult.Cancel;
				return;//bad password
			}
			//allow access if permission for Security Admin (remember, employees not allowed Security Admin)
			if(Users.Cur.ProvNum > 0){
				UserPermissions.GetListForProv(Users.Cur.ProvNum);
				if(UserPermissions.CheckHasPermission("Security Administration",Users.Cur.ProvNum,false)) {
					FillList();
					return;
				}
			}
			//allow access if permission for Providers
			if(Users.Cur.EmployeeNum > 0){
				UserPermissions.GetListForEmp(Users.Cur.EmployeeNum);
				if(UserPermissions.CheckHasPermission("Providers",Users.Cur.EmployeeNum,true)) {
					FillList();
					return;
				}
			}
			else{//prov
				UserPermissions.GetListForProv(Users.Cur.ProvNum);
				if(UserPermissions.CheckHasPermission("Providers",Users.Cur.ProvNum,false)) {
					FillList();
					return;
				}
			}
			MessageBox.Show(Lan.g(this,"You do not have permission for this feature"));
			DialogResult=DialogResult.Cancel;
		}

		private void FillList(){
			Providers.Refresh();
			listProviders.Items.Clear();
			for(int i=0;i<Providers.ListLong.Length;i++){
				if(Providers.ListLong[i].IsHidden)
					listProviders.Items.Add(Providers.ListLong[i].Abbr+"(hidden)");
				else
					listProviders.Items.Add(Providers.ListLong[i].Abbr);
			}
			listProviders.SelectedIndex=Providers.Selected;
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormProvEdit FormProvEdit2 = new FormProvEdit();
			Providers.Cur = new Provider();
			Providers.Cur.ItemOrder=Providers.ListLong.Length;
			FormProvEdit2.IsNew=true;
			FormProvEdit2.ShowDialog();
			if(FormProvEdit2.DialogResult!=DialogResult.OK){
				return;
			}
			Providers.Selected=Providers.ListLong.Length;//this is one more than allowed, but it's ok;
			FillList();
		}

		//private void butHide_Click(object sender, System.EventArgs e) {
			//if(listProviders.SelectedIndex<0){
			//	MessageBox.Show("Please select provider first,");
			//	return;
			//}
			//Providers.HideProv();
			//FillList();
		//}

		private void butUp_Click(object sender, System.EventArgs e) {
			Providers.MoveUp();
			FillList();
		}

		private void butDown_Click(object sender, System.EventArgs e) {
			Providers.MoveDown();
			FillList();
		}

		private void listProviders_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listProviders.IndexFromPoint(e.X,e.Y)<0) return;
			Providers.Selected=listProviders.IndexFromPoint(e.X,e.Y);
			listProviders.SelectedIndex=listProviders.IndexFromPoint(e.X,e.Y);

		}

		private void listProviders_DoubleClick(object sender, System.EventArgs e) {
			if(listProviders.SelectedIndex<0) return;
			FormProvEdit FormProvEdit2=new FormProvEdit();
			FormProvEdit2.IsNew=false;
			Providers.Cur=Providers.ListLong[Providers.Selected];
			FormProvEdit2.ShowDialog();
			FillList();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			SecurityLogs.MakeLogEntry("Providers","Altered Providers");
			DialogResult=DialogResult.OK;
		}

	}
}
