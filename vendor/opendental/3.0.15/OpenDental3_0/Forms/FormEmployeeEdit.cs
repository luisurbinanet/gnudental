using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace OpenDental{
	///<summary></summary>
	public class FormEmployeeEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.TextBox textLName;
		private System.Windows.Forms.TextBox textFName;
		private System.Windows.Forms.TextBox textMI;
		private System.Windows.Forms.TextBox textPassword;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.ComponentModel.Container components = null;
		private OpenDental.TableUserPermissions tbUserPerm;
		private System.Windows.Forms.Button butNone;
		private System.Windows.Forms.Button butAll;
		private System.Windows.Forms.TextBox textUserName;
		private System.Windows.Forms.CheckBox checkIsHidden;
		private System.Windows.Forms.GroupBox groupSecurity;
		///<summary></summary>
		public bool IsNew;

		///<summary></summary>
		public FormEmployeeEdit(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label1,
				this.label10,
				this.label7,
				this.label8,
				this.label9,
				this.groupSecurity,
				this.checkIsHidden,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
				butAll,
				butNone
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
			this.textLName = new System.Windows.Forms.TextBox();
			this.textFName = new System.Windows.Forms.TextBox();
			this.textMI = new System.Windows.Forms.TextBox();
			this.textPassword = new System.Windows.Forms.TextBox();
			this.textUserName = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.checkIsHidden = new System.Windows.Forms.CheckBox();
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.tbUserPerm = new OpenDental.TableUserPermissions();
			this.butNone = new System.Windows.Forms.Button();
			this.butAll = new System.Windows.Forms.Button();
			this.groupSecurity = new System.Windows.Forms.GroupBox();
			this.groupSecurity.SuspendLayout();
			this.SuspendLayout();
			// 
			// textLName
			// 
			this.textLName.Location = new System.Drawing.Point(76, 58);
			this.textLName.MaxLength = 100;
			this.textLName.Name = "textLName";
			this.textLName.Size = new System.Drawing.Size(174, 20);
			this.textLName.TabIndex = 23;
			this.textLName.Text = "";
			// 
			// textFName
			// 
			this.textFName.Location = new System.Drawing.Point(76, 88);
			this.textFName.MaxLength = 100;
			this.textFName.Name = "textFName";
			this.textFName.Size = new System.Drawing.Size(174, 20);
			this.textFName.TabIndex = 24;
			this.textFName.Text = "";
			// 
			// textMI
			// 
			this.textMI.Location = new System.Drawing.Point(76, 120);
			this.textMI.MaxLength = 100;
			this.textMI.Name = "textMI";
			this.textMI.Size = new System.Drawing.Size(88, 20);
			this.textMI.TabIndex = 25;
			this.textMI.Text = "";
			// 
			// textPassword
			// 
			this.textPassword.Location = new System.Drawing.Point(64, 40);
			this.textPassword.MaxLength = 100;
			this.textPassword.Name = "textPassword";
			this.textPassword.PasswordChar = '*';
			this.textPassword.Size = new System.Drawing.Size(310, 20);
			this.textPassword.TabIndex = 26;
			this.textPassword.Text = "";
			// 
			// textUserName
			// 
			this.textUserName.Location = new System.Drawing.Point(64, 16);
			this.textUserName.MaxLength = 100;
			this.textUserName.Name = "textUserName";
			this.textUserName.Size = new System.Drawing.Size(310, 20);
			this.textUserName.TabIndex = 22;
			this.textUserName.Text = "";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(2, 62);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(70, 14);
			this.label10.TabIndex = 31;
			this.label10.Text = "Last Name";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(4, 42);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(60, 14);
			this.label9.TabIndex = 30;
			this.label9.Text = "Password";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(2, 92);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(70, 14);
			this.label8.TabIndex = 29;
			this.label8.Text = "First Name";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(-2, 124);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(70, 14);
			this.label7.TabIndex = 28;
			this.label7.Text = "MI";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(4, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 14);
			this.label1.TabIndex = 27;
			this.label1.Text = "UserName";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkIsHidden
			// 
			this.checkIsHidden.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsHidden.Location = new System.Drawing.Point(76, 32);
			this.checkIsHidden.Name = "checkIsHidden";
			this.checkIsHidden.Size = new System.Drawing.Size(70, 18);
			this.checkIsHidden.TabIndex = 32;
			this.checkIsHidden.Text = "Hidden";
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(720, 550);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 35;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(720, 522);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 34;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// tbUserPerm
			// 
			this.tbUserPerm.BackColor = System.Drawing.SystemColors.Window;
			this.tbUserPerm.Location = new System.Drawing.Point(8, 78);
			this.tbUserPerm.Name = "tbUserPerm";
			this.tbUserPerm.ScrollValue = 1;
			this.tbUserPerm.SelectedIndices = new int[0];
			this.tbUserPerm.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbUserPerm.Size = new System.Drawing.Size(369, 356);
			this.tbUserPerm.TabIndex = 36;
			this.tbUserPerm.CellClicked += new OpenDental.ContrTable.CellEventHandler(this.tbUserPerm_CellClicked);
			// 
			// butNone
			// 
			this.butNone.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butNone.Location = new System.Drawing.Point(102, 440);
			this.butNone.Name = "butNone";
			this.butNone.TabIndex = 38;
			this.butNone.Text = "&None";
			this.butNone.Click += new System.EventHandler(this.butNone_Click);
			// 
			// butAll
			// 
			this.butAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butAll.Location = new System.Drawing.Point(14, 440);
			this.butAll.Name = "butAll";
			this.butAll.TabIndex = 37;
			this.butAll.Text = "&All";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// groupSecurity
			// 
			this.groupSecurity.Controls.Add(this.tbUserPerm);
			this.groupSecurity.Controls.Add(this.textUserName);
			this.groupSecurity.Controls.Add(this.label1);
			this.groupSecurity.Controls.Add(this.textPassword);
			this.groupSecurity.Controls.Add(this.label9);
			this.groupSecurity.Controls.Add(this.butAll);
			this.groupSecurity.Controls.Add(this.butNone);
			this.groupSecurity.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupSecurity.Location = new System.Drawing.Point(406, 32);
			this.groupSecurity.Name = "groupSecurity";
			this.groupSecurity.Size = new System.Drawing.Size(390, 470);
			this.groupSecurity.TabIndex = 39;
			this.groupSecurity.TabStop = false;
			this.groupSecurity.Text = "Security";
			// 
			// FormEmployeeEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(814, 592);
			this.Controls.Add(this.groupSecurity);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.checkIsHidden);
			this.Controls.Add(this.textLName);
			this.Controls.Add(this.textFName);
			this.Controls.Add(this.textMI);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEmployeeEdit";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Employee Edit";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormEmployeeEdit_Closing);
			this.Load += new System.EventHandler(this.FormEmployeeEdit_Load);
			this.groupSecurity.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormEmployeeEdit_Load(object sender, System.EventArgs e) {
			groupSecurity.Visible=false;
			Permissions.GetCur("Security Administration");
			if(!Permissions.Cur.RequiresPassword){
				groupSecurity.Visible=true;
			}
			if(Users.Cur.EmployeeNum > 0){
				if(UserPermissions.CheckHasPermission("Security Administration",Users.Cur.EmployeeNum,true))
					groupSecurity.Visible=true;
			}
			else{
				if(UserPermissions.CheckHasPermission("Security Administration",Users.Cur.ProvNum,false))
					groupSecurity.Visible=true;
			}
			if(IsNew){
				Employees.Cur=new Employee();
				Employees.Cur.IsHidden=false;
				Employees.InsertCur();
			}
			textUserName.Text=Employees.Cur.UserName;
			if(Employees.Cur.UserName!=""){
				textPassword.Text="*****";
			}
			textLName.Text=Employees.Cur.LName;
			textFName.Text=Employees.Cur.FName;
			textMI.Text=Employees.Cur.MiddleI;
			checkIsHidden.Checked=Employees.Cur.IsHidden;
			FillSecurity();
		}

		private void FillSecurity(){
			UserPermissions.Refresh();
			tbUserPerm.ResetRows(Permissions.List.Length);
			tbUserPerm.SetGridColor(Color.Gray);
			UserPermissions.GetListForEmp(Employees.Cur.EmployeeNum);
			for(int i=0;i<Permissions.List.Length;i++){
				tbUserPerm.Cell[0,i]=Permissions.List[i].Name;
				for(int j=0;j<UserPermissions.ListForUser.Length;j++){
					if(UserPermissions.ListForUser[j].PermissionNum==Permissions.List[i].PermissionNum){
						tbUserPerm.Cell[1,i]="X";
						if(UserPermissions.ListForUser[j].IsLogged){
							tbUserPerm.Cell[2,i]="X";
						}
					}
				}//j
			}//i
			tbUserPerm.LayoutTables();
		}

		private void butAll_Click(object sender, System.EventArgs e) {
			for(int i=0;i<Permissions.List.Length;i++){
				if(Permissions.List[i].Name!="Security Administration" &&
					!UserPermissions.CheckHasPermission(Permissions.List[i].Name,Employees.Cur.EmployeeNum,true)){
					UserPermissions.Cur=new UserPermission();
					UserPermissions.Cur.EmployeeNum=Employees.Cur.EmployeeNum;
					UserPermissions.Cur.PermissionNum=Permissions.List[i].PermissionNum;
					UserPermissions.Cur.IsLogged=true;
					UserPermissions.InsertCur();
				}
			}
			FillSecurity();
		}

		private void butNone_Click(object sender, System.EventArgs e) {
			UserPermissions.DeleteAllForEmp(Employees.Cur.EmployeeNum);
			FillSecurity();
		}

		private void tbUserPerm_CellClicked(object sender, OpenDental.CellEventArgs e) {
			if(e.Col==1){//permission
				if(tbUserPerm.Cell[1,e.Row]!="X"){//add X
					if(Permissions.List[e.Row].Name=="Security Administration"){
						MessageBox.Show(Lan.g(this,"Employees cannot have Security Administration."));
						return;
					}
					UserPermissions.Cur=new UserPermission();
					UserPermissions.Cur.EmployeeNum=Employees.Cur.EmployeeNum;
					UserPermissions.Cur.PermissionNum=Permissions.List[e.Row].PermissionNum;
					UserPermissions.InsertCur();
				}
				else{//remove X
					for(int i=0;i<UserPermissions.ListForUser.Length;i++){
						if(UserPermissions.ListForUser[i].PermissionNum==Permissions.List[e.Row].PermissionNum){
							UserPermissions.Cur=UserPermissions.ListForUser[i];
							UserPermissions.DeleteCur();
						}	
					}//i
				}
				FillSecurity();
				return;
			}//e.Col==1
			else if(e.Col==2){//logging
				if(tbUserPerm.Cell[2,e.Row]!="X" && tbUserPerm.Cell[1,e.Row]!="X"){//add X
					UserPermissions.Cur.EmployeeNum=Employees.Cur.EmployeeNum;
					UserPermissions.Cur.PermissionNum=Permissions.List[e.Row].PermissionNum;
					UserPermissions.Cur.IsLogged=true;
					UserPermissions.InsertCur();
				}
				else if(tbUserPerm.Cell[2,e.Row]!="X" && tbUserPerm.Cell[1,e.Row]=="X"){
					for(int i=0;i<UserPermissions.ListForUser.Length;i++){
						if(UserPermissions.ListForUser[i].PermissionNum==Permissions.List[e.Row].PermissionNum){
							UserPermissions.Cur=UserPermissions.ListForUser[i];
							UserPermissions.Cur.IsLogged=true;
							UserPermissions.UpdateCur();
						}	
					}//end for
				}
				else{//remove X
					for(int i=0;i<UserPermissions.ListForUser.Length;i++){
						if(UserPermissions.ListForUser[i].PermissionNum==Permissions.List[e.Row].PermissionNum){
							UserPermissions.Cur=UserPermissions.ListForUser[i];
							UserPermissions.Cur.IsLogged=false;
							UserPermissions.UpdateCur();
						}	
					}//end for
				}
				FillSecurity();
				return;
			}//e.Col==2
			else{
				return;//if e.Col isn't 1 or 2 nothing needs to be done
			}
		}


		private void butOK_Click(object sender, System.EventArgs e) {
			Employees.Cur.LName=textLName.Text;
			Employees.Cur.FName=textFName.Text;
			Employees.Cur.MiddleI=textMI.Text;
			Employees.Cur.IsHidden=checkIsHidden.Checked;
			if(textUserName.Text==""){
				if(UserPermissions.ListForUser.Length>0){
					if(MessageBox.Show(Lan.g(this,
						"UserName is blank.  Are you sure you want to delete this user's permissions?"),"",
						MessageBoxButtons.OKCancel)!=DialogResult.OK){
						return;
					}
					UserPermissions.DeleteAllForEmp(Employees.Cur.EmployeeNum);
				}
			}
			Employees.Cur.UserName=textUserName.Text;//if this is blank, then no 'user' exists
			if(textPassword.Text!="*****"){
				Employees.Cur.Password=Passwords.EncryptPassword(textPassword.Text);		
			}
			Employees.UpdateCur();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormEmployeeEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNew){
				UserPermissions.DeleteAllForEmp(Employees.Cur.EmployeeNum);
				Employees.DeleteCur();
			}
		}

		
		
	}

	
	
}

























