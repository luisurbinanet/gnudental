using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormPermissionEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.TextBox textName;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkRequire;
		private OpenDental.ValidDate textDate;
		private OpenDental.ValidNum textDays;
		private System.Windows.Forms.Label label3;

		private System.ComponentModel.Container components = null;

		public FormPermissionEdit(){
			InitializeComponent();
			Lan.C(this, new Control[] {
				label1,
				label3,
				label2,
				checkRequire,
				label10
			});
			Lan.C("All", new Control[] {
				butOK,
				butCancel,
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
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.textName = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.checkRequire = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textDate = new OpenDental.ValidDate();
			this.textDays = new OpenDental.ValidNum();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(464, 218);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 37;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(464, 190);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 36;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textName
			// 
			this.textName.Location = new System.Drawing.Point(102, 12);
			this.textName.MaxLength = 100;
			this.textName.Name = "textName";
			this.textName.ReadOnly = true;
			this.textName.Size = new System.Drawing.Size(260, 20);
			this.textName.TabIndex = 38;
			this.textName.Text = "";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(62, 14);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(38, 14);
			this.label10.TabIndex = 43;
			this.label10.Text = "Name";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkRequire
			// 
			this.checkRequire.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkRequire.Location = new System.Drawing.Point(0, 46);
			this.checkRequire.Name = "checkRequire";
			this.checkRequire.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkRequire.Size = new System.Drawing.Size(116, 18);
			this.checkRequire.TabIndex = 44;
			this.checkRequire.Text = "Require Password";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(38, 80);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 23);
			this.label1.TabIndex = 46;
			this.label1.Text = "Before Date";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(36, 112);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(66, 16);
			this.label2.TabIndex = 47;
			this.label2.Text = "Before Days";
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(102, 78);
			this.textDate.Name = "textDate";
			this.textDate.TabIndex = 50;
			this.textDate.Text = "";
			// 
			// textDays
			// 
			this.textDays.Location = new System.Drawing.Point(102, 110);
			this.textDays.Name = "textDays";
			this.textDays.Size = new System.Drawing.Size(76, 20);
			this.textDays.TabIndex = 51;
			this.textDays.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(104, 138);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(320, 76);
			this.label3.TabIndex = 52;
			this.label3.Text = "(Set to 0 to always check password.  If you only want to take date into considera" +
				"tion, then Days should be very large.  Perhaps 1000)";
			// 
			// FormPermissionEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(554, 254);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textDays);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.textName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkRequire);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPermissionEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Permission";
			this.Load += new System.EventHandler(this.FormPermissionEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormPermissionEdit_Load(object sender, System.EventArgs e) {
			switch(Permissions.Cur.Name){
				default:
					textDate.Enabled=false;
					textDays.Enabled=false;
					break;
				case "Procedure Completed Edit":
				case "Prescription Edit":
				case "Claims Sent Edit":
				case "Adjustment Edit":
				case "Payment Edit":
					textDate.Enabled=true;
					textDays.Enabled=true;
					break;
			}
			textName.Text=Permissions.Cur.Name;
			if(Permissions.Cur.BeforeDate.Date.Year < 1890){
				textDate.Text="";
			}
			else{
				textDate.Text=Permissions.Cur.BeforeDate.ToShortDateString();
			}
			textDays.Text=Permissions.Cur.BeforeDays.ToString();//defaults to zero: always check.
			//If you only want to take date into consideration, days should be very large. This is consistent.
			checkRequire.Checked=Permissions.Cur.RequiresPassword;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDays.errorProvider1.GetError(textDays)!=""
				|| textDate.errorProvider1.GetError(textDate)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			Permissions.Cur.BeforeDays=PIn.PInt(textDays.Text);
			Permissions.Cur.RequiresPassword=checkRequire.Checked;
			Permissions.Cur.BeforeDate=PIn.PDate(textDate.Text);
			Permissions.UpdateCur();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}



	}
}








