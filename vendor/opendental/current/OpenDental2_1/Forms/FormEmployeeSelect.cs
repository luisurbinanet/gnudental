using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormEmployee : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.CheckBox checkHidden;
		private System.Windows.Forms.ListBox listEmployees;
		private System.Windows.Forms.Button butAdd;
		private System.ComponentModel.Container components = null;
		private ArrayList ALemployees;

		public FormEmployee(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.checkHidden,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butAdd,
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormEmployee));
			this.butOK = new System.Windows.Forms.Button();
			this.checkHidden = new System.Windows.Forms.CheckBox();
			this.listEmployees = new System.Windows.Forms.ListBox();
			this.butAdd = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(278, 400);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 16;
			this.butOK.Text = "Close";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// checkHidden
			// 
			this.checkHidden.Location = new System.Drawing.Point(250, 28);
			this.checkHidden.Name = "checkHidden";
			this.checkHidden.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkHidden.TabIndex = 17;
			this.checkHidden.Text = "Show Hidden";
			this.checkHidden.Click += new System.EventHandler(this.checkHidden_Click);
			// 
			// listEmployees
			// 
			this.listEmployees.Location = new System.Drawing.Point(16, 28);
			this.listEmployees.Name = "listEmployees";
			this.listEmployees.Size = new System.Drawing.Size(212, 316);
			this.listEmployees.TabIndex = 20;
			this.listEmployees.DoubleClick += new System.EventHandler(this.listEmployees_DoubleClick);
			// 
			// butAdd
			// 
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(16, 358);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(74, 26);
			this.butAdd.TabIndex = 19;
			this.butAdd.Text = "           Add";
			this.butAdd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// FormEmployee
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(376, 440);
			this.ControlBox = false;
			this.Controls.Add(this.listEmployees);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.checkHidden);
			this.Controls.Add(this.butOK);
			this.Name = "FormEmployee";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Employees";
			this.Load += new System.EventHandler(this.FormEmployee_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormEmployee_Load(object sender, System.EventArgs e) {
			bool requirePass=false;
			Permissions.GetCur("Security Administration");
			if(Permissions.Cur.RequiresPassword){
				requirePass=true;
			}
			Permissions.GetCur("Employees");
			if(Permissions.Cur.RequiresPassword){
				requirePass=true;
			}
			if(!requirePass){//allow access if no password required
				FillList();
				return;
			}
			//check password if either permission requires a password.
			FormPassword FormP=new FormPassword();
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK){
				DialogResult=DialogResult.Cancel;
				return;
			}
			//allow access if permission for Security Admin (remember, employees not allowed Security Admin)
			if(Users.Cur.ProvNum > 0){
				UserPermissions.GetListForProv(Users.Cur.ProvNum);
				if(UserPermissions.CheckHasPermission("Security Administration",Users.Cur.ProvNum,false)) {
					FillList();
					return;
				}
			}
			//allow access if permission for Employees
			if(Users.Cur.EmployeeNum > 0){
				UserPermissions.GetListForEmp(Users.Cur.EmployeeNum);
				if(UserPermissions.CheckHasPermission("Employees",Users.Cur.EmployeeNum,true)) {
					FillList();
					return;
				}
			}
			else{//prov
				UserPermissions.GetListForProv(Users.Cur.ProvNum);
				if(UserPermissions.CheckHasPermission("Employees",Users.Cur.ProvNum,false)) {
					FillList();
					return;
				}
			}
			MessageBox.Show(Lan.g(this,"You do not have permission for this feature"));
			DialogResult=DialogResult.Cancel;
		}

		private void FillList(){
			Employees.Refresh();
			listEmployees.Items.Clear();
			ALemployees=new ArrayList();
			for(int i=0;i<Employees.List.Length;i++){
				if(Employees.List[i].IsHidden){
					if(checkHidden.Checked){
						ALemployees.Add(Employees.List[i]);
						listEmployees.Items.Add(Employees.GetName(Employees.List[i])+"(hidden)");
					}
				}
				else{
					ALemployees.Add(Employees.List[i]);
					listEmployees.Items.Add(Employees.GetName(Employees.List[i]));
				}
			}
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormEmployeeEdit FormEE=new FormEmployeeEdit();
			FormEE.IsNew=true;
			FormEE.ShowDialog();
			FillList();
		}

		private void listEmployees_DoubleClick(object sender, System.EventArgs e) {
			Employees.Cur=(Employee)ALemployees[listEmployees.SelectedIndex];
			FormEmployeeEdit FormEE=new FormEmployeeEdit();
			FormEE.ShowDialog();
			FillList();
		}

		private void checkHidden_Click(object sender, System.EventArgs e) {
			FillList();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			SecurityLogs.MakeLogEntry("Employees","Altered Employees");
			DialogResult=DialogResult.OK;
		}

	}
}
