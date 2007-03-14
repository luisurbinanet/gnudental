using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormPassword : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label8;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.TextBox textUser;
		private System.Windows.Forms.TextBox textPassword;
		private System.ComponentModel.Container components = null;
		///<summary>If this window closes with OK, then this contains the authenticated user.</summary>
		public User AuthenticatedUser;
		///<summary></summary>
		private string Display;

		///<summary>Must supply display text.</summary>
		public FormPassword(string display){
			InitializeComponent();
			Lan.F(this);
			Display=display;
		}

		///<summary></summary>
		protected override void Dispose(bool disposing){
			if(disposing){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			this.textUser = new System.Windows.Forms.TextBox();
			this.textPassword = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// textUser
			// 
			this.textUser.Location = new System.Drawing.Point(84, 10);
			this.textUser.MaxLength = 100;
			this.textUser.Name = "textUser";
			this.textUser.Size = new System.Drawing.Size(212, 20);
			this.textUser.TabIndex = 32;
			this.textUser.Text = "";
			// 
			// textPassword
			// 
			this.textPassword.Location = new System.Drawing.Point(84, 40);
			this.textPassword.MaxLength = 100;
			this.textPassword.Name = "textPassword";
			this.textPassword.PasswordChar = '*';
			this.textPassword.Size = new System.Drawing.Size(212, 20);
			this.textPassword.TabIndex = 33;
			this.textPassword.Text = "";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(0, 14);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(80, 14);
			this.label10.TabIndex = 37;
			this.label10.Text = "User Name";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(0, 44);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(82, 14);
			this.label8.TabIndex = 36;
			this.label8.Text = "Password";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.Location = new System.Drawing.Point(296, 106);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 39;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(296, 74);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 38;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// FormPassword
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(386, 142);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.textUser);
			this.Controls.Add(this.textPassword);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label8);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPassword";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Verify Password";
			this.Load += new System.EventHandler(this.FormPassword_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormPassword_Load(object sender, System.EventArgs e) {
			Text=Text+" - "+Lan.g("permissionNames",Display);
			Users.Refresh();
			if(Users.List.Length==0){
				MsgBox.Show(this,"You do not have any usernames or passwords set up yet.");
				DialogResult=DialogResult.OK;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			User user=null;
			for(int i=0;i<Users.List.Length;i++){
				if(textUser.Text==Users.List[i].UserName){
					user=Users.List[i];
					break;
				}
			}
			if(user==null){
				MsgBox.Show(this,"UserName or Password invalid.");
				return;
			}
			if(!Passwords.CheckPassword(textPassword.Text,user.Password)){
				MsgBox.Show(this,"UserName or Password invalid.");
				return;
			}
			AuthenticatedUser=user;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}



		

	}

	///<summary></summary>
	public class Passwords{

		///<summary></summary>
		public static string EncryptPassword(string inputPass){
			HashAlgorithm hash=HashAlgorithm.Create("MD5");
			Encoding unicode = Encoding.Unicode;
      byte[] unicodeBytes = unicode.GetBytes(inputPass);
			byte[] hashbytes=hash.ComputeHash(unicodeBytes);
			StringBuilder strB=new StringBuilder();
			for(int i=0;i<hashbytes.Length;i++){
				strB.Append(hashbytes[i].ToString());
			}
			return strB.ToString();
		}
		
		///<summary></summary>
		public static bool CheckPassword(string inputPass,string hashedPass){
			string hashedInput=EncryptPassword(inputPass);
			//MessageBox.Show(
			//Debug.WriteLine(hashedInput+","+hashedPass);
			return hashedInput==hashedPass;
		}
	}


}








