using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormProgramLinks : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ListBox listProgram;
		private System.Windows.Forms.CheckBox checkEnabled;
		private System.ComponentModel.Container components = null;// Required designer variable.
		private Programs Programs=new Programs();

		public FormProgramLinks(){
			InitializeComponent();// Required for Windows Form Designer support
			Lan.C(this, new System.Windows.Forms.Control[] {
				label10,
				checkEnabled,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
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
			this.listProgram = new System.Windows.Forms.ListBox();
			this.butOK = new System.Windows.Forms.Button();
			this.label10 = new System.Windows.Forms.Label();
			this.checkEnabled = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// listProgram
			// 
			this.listProgram.Items.AddRange(new object[] {
																										 ""});
			this.listProgram.Location = new System.Drawing.Point(18, 32);
			this.listProgram.Name = "listProgram";
			this.listProgram.Size = new System.Drawing.Size(282, 446);
			this.listProgram.TabIndex = 34;
			this.listProgram.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listProgram_MouseDown);
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(346, 460);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 38;
			this.butOK.Text = "Close";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(18, 14);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(100, 16);
			this.label10.TabIndex = 35;
			this.label10.Text = "Programs";
			// 
			// checkEnabled
			// 
			this.checkEnabled.Location = new System.Drawing.Point(342, 32);
			this.checkEnabled.Name = "checkEnabled";
			this.checkEnabled.Size = new System.Drawing.Size(66, 18);
			this.checkEnabled.TabIndex = 40;
			this.checkEnabled.Text = "Enabled";
			this.checkEnabled.Click += new System.EventHandler(this.checkEnabled_Click);
			// 
			// FormProgramLinks
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(438, 498);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																																	this.checkEnabled,
																																	this.listProgram,
																																	this.butOK,
																																	this.label10});
			this.Name = "FormProgramLinks";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = Lan.g(this,"Program Links");
			this.Load += new System.EventHandler(this.FormProgramLinks_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormProgramLinks_Load(object sender, System.EventArgs e) {
			listProgram.Items.Clear();
			for(int i=0;i<Programs.List.Length;i++){
				listProgram.Items.Add(Programs.List[i].ProgDesc);
			}
		}

		private void listProgram_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listProgram.SelectedIndex==-1){
				return;
			}
			Programs.Cur=Programs.List[listProgram.SelectedIndex];
			checkEnabled.Checked=Programs.Cur.Enabled;
		}

		private void checkEnabled_Click(object sender, System.EventArgs e) {
			if(listProgram.SelectedIndex==-1) return;
			Programs.Cur.Enabled=checkEnabled.Checked;
			Programs.UpdateCur();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
			Close();
		}

		//private void butCancel_Click(object sender, System.EventArgs e) {
		//	DialogResult=DialogResult.Cancel;
		//}

		

		



		
	}
}
