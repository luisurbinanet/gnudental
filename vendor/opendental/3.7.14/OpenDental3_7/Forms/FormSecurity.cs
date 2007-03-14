using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormSecurity : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TreeView treeUsers;
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butAddGroup;
		private OpenDental.UI.Button butAddUser;
		private System.Windows.Forms.TreeView treePermissions;
		private System.Windows.Forms.ImageList imageListPerm;
		private System.Windows.Forms.Label labelPerm;
		private System.Windows.Forms.Label labelMultiuser;
		private System.ComponentModel.IContainer components;
		private int SelectedGroupNum;
		private TreeNode clickedPermNode;
		private OpenDental.UI.Button butAudit;
		private System.Windows.Forms.CheckBox checkTimecardSecurityEnabled;
		private bool changed;

		///<summary></summary>
		public FormSecurity()
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormSecurity));
			this.butClose = new OpenDental.UI.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.treeUsers = new System.Windows.Forms.TreeView();
			this.butAddGroup = new OpenDental.UI.Button();
			this.butAddUser = new OpenDental.UI.Button();
			this.treePermissions = new System.Windows.Forms.TreeView();
			this.imageListPerm = new System.Windows.Forms.ImageList(this.components);
			this.labelPerm = new System.Windows.Forms.Label();
			this.labelMultiuser = new System.Windows.Forms.Label();
			this.butAudit = new OpenDental.UI.Button();
			this.checkTimecardSecurityEnabled = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.Location = new System.Drawing.Point(793, 579);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 14);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(141, 19);
			this.label2.TabIndex = 3;
			this.label2.Text = "Groups and Users";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// treeUsers
			// 
			this.treeUsers.HideSelection = false;
			this.treeUsers.ImageIndex = -1;
			this.treeUsers.Location = new System.Drawing.Point(8, 37);
			this.treeUsers.Name = "treeUsers";
			this.treeUsers.SelectedImageIndex = -1;
			this.treeUsers.ShowRootLines = false;
			this.treeUsers.Size = new System.Drawing.Size(184, 529);
			this.treeUsers.TabIndex = 4;
			this.treeUsers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeUsers_MouseDown);
			this.treeUsers.DoubleClick += new System.EventHandler(this.treeUsers_DoubleClick);
			// 
			// butAddGroup
			// 
			this.butAddGroup.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddGroup.Autosize = true;
			this.butAddGroup.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddGroup.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddGroup.Location = new System.Drawing.Point(8, 579);
			this.butAddGroup.Name = "butAddGroup";
			this.butAddGroup.Size = new System.Drawing.Size(75, 25);
			this.butAddGroup.TabIndex = 1;
			this.butAddGroup.Text = "Add Group";
			this.butAddGroup.Click += new System.EventHandler(this.butAddGroup_Click);
			// 
			// butAddUser
			// 
			this.butAddUser.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddUser.Autosize = true;
			this.butAddUser.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddUser.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddUser.Location = new System.Drawing.Point(118, 579);
			this.butAddUser.Name = "butAddUser";
			this.butAddUser.Size = new System.Drawing.Size(75, 25);
			this.butAddUser.TabIndex = 0;
			this.butAddUser.Text = "Add User";
			this.butAddUser.Click += new System.EventHandler(this.butAddUser_Click);
			// 
			// treePermissions
			// 
			this.treePermissions.HideSelection = false;
			this.treePermissions.ImageList = this.imageListPerm;
			this.treePermissions.Location = new System.Drawing.Point(388, 37);
			this.treePermissions.Name = "treePermissions";
			this.treePermissions.ShowPlusMinus = false;
			this.treePermissions.ShowRootLines = false;
			this.treePermissions.Size = new System.Drawing.Size(479, 529);
			this.treePermissions.TabIndex = 6;
			this.treePermissions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treePermissions_MouseDown);
			this.treePermissions.DoubleClick += new System.EventHandler(this.treePermissions_DoubleClick);
			this.treePermissions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treePermissions_AfterSelect);
			// 
			// imageListPerm
			// 
			this.imageListPerm.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageListPerm.ImageSize = new System.Drawing.Size(16, 16);
			this.imageListPerm.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPerm.ImageStream")));
			this.imageListPerm.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// labelPerm
			// 
			this.labelPerm.Location = new System.Drawing.Point(386, 14);
			this.labelPerm.Name = "labelPerm";
			this.labelPerm.Size = new System.Drawing.Size(425, 19);
			this.labelPerm.TabIndex = 5;
			this.labelPerm.Text = "Permissions for group:";
			this.labelPerm.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// labelMultiuser
			// 
			this.labelMultiuser.Location = new System.Drawing.Point(199, 36);
			this.labelMultiuser.Name = "labelMultiuser";
			this.labelMultiuser.Size = new System.Drawing.Size(182, 188);
			this.labelMultiuser.TabIndex = 8;
			this.labelMultiuser.Text = "When in multiuser mode,  anyone will have permission for the items you select.  I" +
				"n this case, the actions will not be logged in an audit trail.  Any unchecked pe" +
				"rmission will require user to enter a password each time and will be logged.";
			// 
			// butAudit
			// 
			this.butAudit.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAudit.Autosize = true;
			this.butAudit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAudit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAudit.Location = new System.Drawing.Point(388, 579);
			this.butAudit.Name = "butAudit";
			this.butAudit.Size = new System.Drawing.Size(107, 25);
			this.butAudit.TabIndex = 9;
			this.butAudit.Text = "View Audit Trail";
			this.butAudit.Click += new System.EventHandler(this.butAudit_Click);
			// 
			// checkTimecardSecurityEnabled
			// 
			this.checkTimecardSecurityEnabled.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkTimecardSecurityEnabled.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTimecardSecurityEnabled.Location = new System.Drawing.Point(198, 552);
			this.checkTimecardSecurityEnabled.Name = "checkTimecardSecurityEnabled";
			this.checkTimecardSecurityEnabled.Size = new System.Drawing.Size(182, 19);
			this.checkTimecardSecurityEnabled.TabIndex = 57;
			this.checkTimecardSecurityEnabled.Text = "TimecardSecurityEnabled";
			this.checkTimecardSecurityEnabled.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// FormSecurity
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(884, 619);
			this.Controls.Add(this.checkTimecardSecurityEnabled);
			this.Controls.Add(this.butAudit);
			this.Controls.Add(this.butAddUser);
			this.Controls.Add(this.butAddGroup);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.labelMultiuser);
			this.Controls.Add(this.treePermissions);
			this.Controls.Add(this.labelPerm);
			this.Controls.Add(this.treeUsers);
			this.Controls.Add(this.label2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSecurity";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Security";
			this.Load += new System.EventHandler(this.FormSecurity_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormSecurity_Load(object sender, System.EventArgs e) {
			FillTreePermissionsInitial();
			FillTreeUsers();
			FillTreePerm();
			checkTimecardSecurityEnabled.Checked=Prefs.GetBool("TimecardSecurityEnabled");
		}

		private void FillTreePermissionsInitial(){
			TreeNode node;
			TreeNode node2;//second level
			TreeNode node3;
			node=SetNode("Main Menu");
				node2=SetNode(Permissions.Setup);
					node.Nodes.Add(node2);
				node2=SetNode(Permissions.ChooseDatabase);
					node.Nodes.Add(node2);
				node2=SetNode(Permissions.SecurityAdmin);
					node.Nodes.Add(node2);
				node2=SetNode(Permissions.Schedules);
					node.Nodes.Add(node2);
				node2=SetNode(Permissions.Blockouts);
					node.Nodes.Add(node2);
				node2=SetNode(Permissions.UserQuery);
					node.Nodes.Add(node2);
				node2=SetNode(Permissions.Reports);
					node.Nodes.Add(node2);
				treePermissions.Nodes.Add(node);
			node=SetNode(Permissions.AppointmentsModule);
				node2=SetNode(Permissions.AppointmentCreate);
					node.Nodes.Add(node2);
				node2=SetNode(Permissions.AppointmentMove);
					node.Nodes.Add(node2);
				node2=SetNode(Permissions.AppointmentEdit);
					node.Nodes.Add(node2);
				treePermissions.Nodes.Add(node);
			node=SetNode(Permissions.FamilyModule);
				treePermissions.Nodes.Add(node);
			node=SetNode(Permissions.AccountModule);
				node2=SetNode(Permissions.ClaimsSentEdit);
					node.Nodes.Add(node2);
				node2=SetNode("Payment");
					node3=SetNode(Permissions.PaymentCreate);
						node2.Nodes.Add(node3);
					node3=SetNode(Permissions.PaymentEdit);
						node2.Nodes.Add(node3);
					node.Nodes.Add(node2);
				node2=SetNode("Adjustment");
					node3=SetNode(Permissions.AdjustmentCreate);
						node2.Nodes.Add(node3);
					node3=SetNode(Permissions.AdjustmentEdit);
						node2.Nodes.Add(node3);
					node.Nodes.Add(node2);
				treePermissions.Nodes.Add(node);
			node=SetNode(Permissions.TPModule);
				treePermissions.Nodes.Add(node);
			node=SetNode(Permissions.ChartModule);
				node2=SetNode("Procedure");
					node3=SetNode(Permissions.ProcComplCreate);
						node2.Nodes.Add(node3);
					node3=SetNode(Permissions.ProcComplEdit);
						node2.Nodes.Add(node3);
					node.Nodes.Add(node2);
				node2=SetNode(Permissions.RxCreate);
					node.Nodes.Add(node2);
				treePermissions.Nodes.Add(node);
			node=SetNode(Permissions.ImagesModule);
				treePermissions.Nodes.Add(node);
			node=SetNode(Permissions.ManageModule);
				node2=SetNode(Permissions.Backup);
					node.Nodes.Add(node2);
				node2=SetNode(Permissions.TimecardsEditAll);
					node.Nodes.Add(node2);
				treePermissions.Nodes.Add(node);
			treePermissions.ExpandAll();
		}

		///<summary>This just keeps FillTreePermissionsInitial looking cleaner.</summary>
		private TreeNode SetNode(Permissions perm){
			TreeNode retVal=new TreeNode();
			retVal.Text=GroupPermissions.GetDesc(perm);
			retVal.Tag=perm;
			retVal.ImageIndex=1;
			retVal.SelectedImageIndex=1;
			return retVal;
		}

		private TreeNode SetNode(string text){
			TreeNode retVal=new TreeNode();
			retVal.Text=text;
			retVal.Tag=Permissions.None;
			retVal.ImageIndex=0;
			retVal.SelectedImageIndex=0;
			return retVal;
		}

		private void FillTreeUsers(){
			UserGroups.Refresh();
			Users.Refresh();
			treeUsers.Nodes.Clear();
			User[] usersForGroup;
			TreeNode groupNode;
			TreeNode userNode;
			for(int i=0;i<UserGroups.List.Length;i++){
				groupNode=new TreeNode(UserGroups.List[i].Description);
				groupNode.Tag=UserGroups.List[i].UserGroupNum;
				usersForGroup=Users.GetForGroup(UserGroups.List[i].UserGroupNum);
				for(int j=0;j<usersForGroup.Length;j++){
					userNode=new TreeNode(usersForGroup[j].UserName);
					userNode.Tag=usersForGroup[j].UserNum;
					groupNode.Nodes.Add(userNode);
				}
				treeUsers.Nodes.Add(groupNode);
			}
			treeUsers.ExpandAll();
			treeUsers.SelectedNode=treeUsers.Nodes[0];
			SelectedGroupNum=UserGroups.List[0].UserGroupNum;
		}

		private void butAddGroup_Click(object sender, System.EventArgs e) {
			UserGroup group=new UserGroup();
			FormUserGroupEdit FormU=new FormUserGroupEdit(group);
			FormU.IsNew=true;
			FormU.ShowDialog();
			if(FormU.DialogResult==DialogResult.Cancel){
				return;
			}
			FillTreeUsers();
			FillTreePerm();
			changed=true;
		}

		private void butAddUser_Click(object sender, System.EventArgs e) {
			User user=new User();
			user.UserGroupNum=SelectedGroupNum;
			FormUserEdit FormU=new FormUserEdit(user);
			FormU.IsNew=true;
			FormU.ShowDialog();
			if(FormU.DialogResult==DialogResult.Cancel){
				return;
			}
			FillTreeUsers();
			FillTreePerm();
			changed=true;
		}

		private void treeUsers_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			TreeNode clickedNode=treeUsers.GetNodeAt(e.X,e.Y);
			if(clickedNode==null){
				return;
			}
			SelectedGroupNum=0;
			if(clickedNode.Parent==null){//group
				SelectedGroupNum=(int)clickedNode.Tag;
			}
			else{//user
				SelectedGroupNum=Users.GetUser((int)clickedNode.Tag).UserGroupNum;
			}
			FillTreePerm();
		}

		private void treeUsers_DoubleClick(object sender, System.EventArgs e) {
			if(treeUsers.SelectedNode==null){
				return;
			}
			treeUsers.ExpandAll();
			if(treeUsers.SelectedNode.Parent==null){//group
				UserGroup group=UserGroups.List[treeUsers.SelectedNode.Index];
				FormUserGroupEdit FormU=new FormUserGroupEdit(group);
				FormU.ShowDialog();
				if(FormU.DialogResult==DialogResult.Cancel){
					return;
				}
				FillTreeUsers();
				//reselect group
				for(int i=0;i<treeUsers.Nodes.Count;i++){
					if((int)treeUsers.Nodes[i].Tag==group.UserGroupNum){
						treeUsers.SelectedNode=treeUsers.Nodes[i];
						SelectedGroupNum=group.UserGroupNum;
					}
				}
			}
			else{//user
				User user=Users.GetUser((int)treeUsers.SelectedNode.Tag);
				FormUserEdit FormU=new FormUserEdit(user);
				FormU.ShowDialog();
				if(FormU.DialogResult==DialogResult.Cancel){
					return;
				}
				FillTreeUsers();
				for(int i=0;i<treeUsers.Nodes.Count;i++){
					for(int j=0;j<treeUsers.Nodes[i].Nodes.Count;j++){
						if((int)treeUsers.Nodes[i].Nodes[j].Tag==FormU.UserCur.UserNum){
							treeUsers.SelectedNode=treeUsers.Nodes[i].Nodes[j];
							SelectedGroupNum=FormU.UserCur.UserGroupNum;
						}
					}
				}
			}
			FillTreePerm();
			changed=true;
		}

		private void FillTreePerm(){
			GroupPermissions.Refresh();
			labelPerm.Text=Lan.g(this,"Permissions for group:")+"  "+UserGroups.GetGroup(SelectedGroupNum).Description;
			labelMultiuser.Visible=false;
			for(int i=0;i<treePermissions.Nodes.Count;i++){
				FillNodes(treePermissions.Nodes[i],SelectedGroupNum);
			}
		}

		///<summary>A recursive function that sets the checkbox for a node.  Also sets the text for the node.</summary>
		private void FillNodes(TreeNode node,int userGroupNum){
			//first, any child nodes
			for(int i=0;i<node.Nodes.Count;i++){
				FillNodes(node.Nodes[i],userGroupNum);
			}
			//then this node
			if(node.ImageIndex==0){
				return;
			}
			node.ImageIndex=1;
			node.Text=GroupPermissions.GetDesc((Permissions)node.Tag);
			for(int i=0;i<GroupPermissions.List.Length;i++){
				if(GroupPermissions.List[i].UserGroupNum==userGroupNum
					&& GroupPermissions.List[i].PermType==(Permissions)node.Tag)
				{
					node.ImageIndex=2;
					if(GroupPermissions.List[i].NewerDate.Year>1880){
						node.Text+=" ("+Lan.g(this,"if date newer than")+" "+GroupPermissions.List[i].NewerDate.ToShortDateString()+")";
					}
					else if(GroupPermissions.List[i].NewerDays>0){
						node.Text+=" ("+Lan.g(this,"if days newer than")+" "+GroupPermissions.List[i].NewerDays.ToString()+")";
					}
				}
			}
		}

		private void treePermissions_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			clickedPermNode=treePermissions.GetNodeAt(e.X,e.Y);
			if(clickedPermNode==null){
				return;
			}
			treePermissions.BeginUpdate();
			if(clickedPermNode.Parent==null){//level 1
				if(e.X<5 || e.X>17){
					return;
				}
			}
			else if(clickedPermNode.Parent.Parent==null){//level 2
				if(e.X<24 || e.X>36){
					return;
				}
			}
			else if(clickedPermNode.Parent.Parent.Parent==null){//level 3
				if(e.X<43 || e.X>55){
					return;
				}
			}
			if(clickedPermNode.ImageIndex==1){//unchecked, so need to add a permission
				GroupPermission perm=new GroupPermission();
				perm.PermType=(Permissions)clickedPermNode.Tag;
				perm.UserGroupNum=SelectedGroupNum;
				if(GroupPermissions.PermTakesDates(perm.PermType)){
					FormGroupPermEdit FormG=new FormGroupPermEdit(perm);
					FormG.IsNew=true;
					FormG.ShowDialog();
					if(FormG.DialogResult==DialogResult.Cancel){
						treePermissions.EndUpdate();
						return;
					}
				}
				else{
					try{
						perm.InsertOrUpdate(true);
					}
					catch(Exception ex){
						MessageBox.Show(ex.Message);
						return;
					}
				}
			}
			else if(clickedPermNode.ImageIndex==2){//checked, so need to delete the perm
				try{
					GroupPermissions.RemovePermission(SelectedGroupNum,(Permissions)clickedPermNode.Tag);
				}
				catch(Exception ex){
					MessageBox.Show(ex.Message);
					return;
				}
			}
			FillTreePerm();
			changed=true;		
		}

		private void treePermissions_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			treePermissions.SelectedNode=null;
			treePermissions.EndUpdate();
		}

		private void treePermissions_DoubleClick(object sender, System.EventArgs e) {
			if(clickedPermNode==null){
				return;
			}
			Permissions permType=(Permissions)clickedPermNode.Tag;
			if(!GroupPermissions.PermTakesDates(permType)){
				return;
			}
			GroupPermission perm=GroupPermissions.GetPerm(SelectedGroupNum,(Permissions)clickedPermNode.Tag);
			if(perm==null){
				return;
			}
			FormGroupPermEdit FormG=new FormGroupPermEdit(perm);
			FormG.ShowDialog();
			if(FormG.DialogResult==DialogResult.Cancel){
				return;
			}
			FillTreePerm();
			changed=true;
		}

		private void butAudit_Click(object sender, System.EventArgs e) {
			FormAudit FormA=new FormAudit();
			FormA.ShowDialog();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			if(changed){
				DataValid.SetInvalid(InvalidTypes.Security);
			}
			if(Prefs.UpdateBool("TimecardSecurityEnabled",checkTimecardSecurityEnabled.Checked)){
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
			Close();
		}

		

		

		

		

		

		

		

		

		

		

		

		
		


	}
}





















