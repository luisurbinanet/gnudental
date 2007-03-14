using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormPermissionsManage : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butClose;
		private OpenDental.TablePermissions tbPerm;
		private System.Windows.Forms.Button butAll;
		private System.Windows.Forms.Button butNone;
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormPermissionsManage(){
			InitializeComponent();
			tbPerm.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbPerm_CellDoubleClicked);
			tbPerm.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbPerm_CellClicked);
			Lan.C("All", new System.Windows.Forms.Control[] {
				butClose,
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
			this.tbPerm = new OpenDental.TablePermissions();
			this.butClose = new System.Windows.Forms.Button();
			this.butAll = new System.Windows.Forms.Button();
			this.butNone = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// tbPerm
			// 
			this.tbPerm.BackColor = System.Drawing.SystemColors.Window;
			this.tbPerm.Location = new System.Drawing.Point(22, 26);
			this.tbPerm.Name = "tbPerm";
			this.tbPerm.ScrollValue = 1;
			this.tbPerm.SelectedIndices = new int[0];
			this.tbPerm.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbPerm.Size = new System.Drawing.Size(529, 356);
			this.tbPerm.TabIndex = 0;
			// 
			// butClose
			// 
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClose.Location = new System.Drawing.Point(570, 416);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 26);
			this.butClose.TabIndex = 4;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butAll
			// 
			this.butAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butAll.Location = new System.Drawing.Point(22, 416);
			this.butAll.Name = "butAll";
			this.butAll.Size = new System.Drawing.Size(75, 26);
			this.butAll.TabIndex = 5;
			this.butAll.Text = "&All";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// butNone
			// 
			this.butNone.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butNone.Location = new System.Drawing.Point(114, 416);
			this.butNone.Name = "butNone";
			this.butNone.Size = new System.Drawing.Size(75, 26);
			this.butNone.TabIndex = 6;
			this.butNone.Text = "&None";
			this.butNone.Click += new System.EventHandler(this.butNone_Click);
			// 
			// FormPermissionsManage
			// 
			this.AcceptButton = this.butClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(662, 462);
			this.Controls.Add(this.butNone);
			this.Controls.Add(this.butAll);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.tbPerm);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPermissionsManage";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Manage Permissions";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormPermissionsManage_Closing);
			this.Load += new System.EventHandler(this.FormManagePermissions_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormManagePermissions_Load(object sender, System.EventArgs e) {
			if(!UserPermissions.CheckUserPassword("Security Administration")){
				MessageBox.Show(Lan.g(this,"You do not have permission for this feature."));
				DialogResult=DialogResult.Cancel;
				return;
			}
		  FillTable();
		}

		private void FillTable(){
			Permissions.Refresh();
			tbPerm.ResetRows(Permissions.List.Length);
			tbPerm.SetGridColor(Color.Gray);
			for(int i=0;i<Permissions.List.Length;i++){
				tbPerm.Cell[0,i]=Permissions.List[i].Name;
				if(Permissions.List[i].RequiresPassword)
					tbPerm.Cell[1,i]="X";
				switch(Permissions.List[i].Name){
					default:
						break;
					case "Procedure Completed Edit":
					case "Prescription Edit":
					case "Claims Sent Edit":
					case "Adjustment Edit":
					case "Payment Edit":
						if(Permissions.List[i].BeforeDate.Date.Year < 1890){
							tbPerm.Cell[2,i]="---";
						}
						else{
							tbPerm.Cell[2,i]=Permissions.List[i].BeforeDate.ToShortDateString();
						}
						tbPerm.Cell[3,i]=Permissions.List[i].BeforeDays.ToString();
						break;
				}
			}
			tbPerm.LayoutTables();
		}

		private void tbPerm_CellClicked(object sender, CellEventArgs e){
			Permissions.Cur=Permissions.List[e.Row];
			if(e.Col==1){
				if(Permissions.Cur.Name=="Security Administration"
					&& !Permissions.Cur.RequiresPassword
					&& UserPermissions.AdministratorCount()==0){
					MessageBox.Show(Lan.g(this,"You cannot enable Security Administration until you have set up at least one provider with Security Administration permission."));
					return;
				}
				if(Permissions.Cur.RequiresPassword){
					Permissions.Cur.RequiresPassword=false;
					Permissions.UpdateCur();
				}
				else{
					Permissions.Cur.RequiresPassword=true;
					Permissions.UpdateCur();
				}
				FillTable();
				return;
			}
			else if(e.Col==2 || e.Col==3){
				switch(Permissions.Cur.Name){
					default:
						break;
					case "Procedure Completed Edit":
					case "Prescription Edit":
					case "Claims Sent Edit":
					case "Adjustment Edit":
					case "Payment Edit":
						FormPermissionEdit FormPE=new FormPermissionEdit();
						FormPE.ShowDialog();
						if(FormPE.DialogResult==DialogResult.OK){
							FillTable();
						}
						break;
				}
			}
			FillTable();
		}

		private void tbPerm_CellDoubleClicked(object sender, CellEventArgs e){
			/*Permissions.Cur=Permissions.List[e.Row]; 
			FormPermissionEdit FormPE=new FormPermissionEdit();
			FormPE.ShowDialog();
			if(FormPE.DialogResult==DialogResult.OK){
				FillTable();
			}*/
		}

		private void butAll_Click(object sender, System.EventArgs e) {
			Permissions.SetAll(true);
			FillTable();
		}

		private void butNone_Click(object sender, System.EventArgs e) {
			Permissions.SetAll(false);
			FillTable();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormPermissionsManage_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			SecurityLogs.MakeLogEntry("Security Administration","Altered Permissions");
		}

		

	}
}


















