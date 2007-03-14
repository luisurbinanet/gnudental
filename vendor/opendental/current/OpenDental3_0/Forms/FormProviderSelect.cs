using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormProviderSelect : System.Windows.Forms.Form{
		private System.Windows.Forms.ListBox listProviders;
		private System.Windows.Forms.Button butClose;
		private OpenDental.XPButton butDown;
		private OpenDental.XPButton butUp;
		private OpenDental.XPButton butAdd;
		private System.ComponentModel.Container components = null;
		
		///<summary></summary>
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

		///<summary></summary>
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
			this.butDown = new OpenDental.XPButton();
			this.butUp = new OpenDental.XPButton();
			this.butAdd = new OpenDental.XPButton();
			this.SuspendLayout();
			// 
			// listProviders
			// 
			this.listProviders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.listProviders.Location = new System.Drawing.Point(16, 12);
			this.listProviders.Name = "listProviders";
			this.listProviders.Size = new System.Drawing.Size(192, 316);
			this.listProviders.TabIndex = 4;
			this.listProviders.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listProviders_MouseDown);
			this.listProviders.DoubleClick += new System.EventHandler(this.listProviders_DoubleClick);
			// 
			// butClose
			// 
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClose.Location = new System.Drawing.Point(208, 376);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 26);
			this.butClose.TabIndex = 3;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butDown
			// 
			this.butDown.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDown.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDown.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDown.Image = ((System.Drawing.Image)(resources.GetObject("butDown.Image")));
			this.butDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDown.Location = new System.Drawing.Point(105, 375);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(79, 26);
			this.butDown.TabIndex = 12;
			this.butDown.Text = "&Down";
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0, 1);
			this.butUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butUp.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butUp.Image = ((System.Drawing.Image)(resources.GetObject("butUp.Image")));
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(16, 375);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(79, 26);
			this.butUp.TabIndex = 11;
			this.butUp.Text = "&Up";
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAdd.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(16, 340);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(79, 26);
			this.butAdd.TabIndex = 10;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// FormProviderSelect
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(311, 414);
			this.Controls.Add(this.butDown);
			this.Controls.Add(this.butUp);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.listProviders);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProviderSelect";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Providers";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormProviderSelect_Closing);
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
			if(listProviders.Items.Count>20){//for large institutions, like dental schools
				this.Size=new Size(900,700);
				this.Location=new Point((SystemInformation.WorkingArea.Width-Width)/2,(SystemInformation.WorkingArea.Height-Height)/2);
			}
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
			Close();
		}

		private void FormProviderSelect_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
			SecurityLogs.MakeLogEntry("Providers","Altered Providers");
		}

	}
}
