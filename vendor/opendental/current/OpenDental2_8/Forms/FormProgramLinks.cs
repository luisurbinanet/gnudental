using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormProgramLinks : System.Windows.Forms.Form{
		private System.Windows.Forms.ListBox listProgram;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button butClose;
		private OpenDental.XPButton butAdd;// Required designer variable.
		private Programs Programs=new Programs();

		///<summary></summary>
		public FormProgramLinks(){
			InitializeComponent();// Required for Windows Form Designer support
			Lan.C(this, new System.Windows.Forms.Control[] {
				this,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butClose,
				butAdd
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormProgramLinks));
			this.listProgram = new System.Windows.Forms.ListBox();
			this.butClose = new System.Windows.Forms.Button();
			this.butAdd = new OpenDental.XPButton();
			this.SuspendLayout();
			// 
			// listProgram
			// 
			this.listProgram.Items.AddRange(new object[] {
																										 ""});
			this.listProgram.Location = new System.Drawing.Point(17, 63);
			this.listProgram.Name = "listProgram";
			this.listProgram.Size = new System.Drawing.Size(282, 277);
			this.listProgram.TabIndex = 34;
			this.listProgram.DoubleClick += new System.EventHandler(this.listProgram_DoubleClick);
			// 
			// butClose
			// 
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClose.Location = new System.Drawing.Point(345, 359);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 26);
			this.butClose.TabIndex = 38;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(18, 357);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75, 26);
			this.butAdd.TabIndex = 41;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// FormProgramLinks
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(447, 412);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.listProgram);
			this.Controls.Add(this.butClose);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProgramLinks";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Program Links";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormProgramLinks_Closing);
			this.Load += new System.EventHandler(this.FormProgramLinks_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormProgramLinks_Load(object sender, System.EventArgs e) {
			FillList();
		}

		private void FillList(){
			Programs.Refresh();
			listProgram.Items.Clear();
			string itemName="";
			for(int i=0;i<Programs.List.Length;i++){
				itemName=Programs.List[i].ProgDesc;
				if(Programs.List[i].Enabled)
					itemName+="(enabled)";
				listProgram.Items.Add(itemName);
			}
		}

		/*private void listProgram_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listProgram.SelectedIndex==-1){
				return;
			}
			Programs.Cur=Programs.List[listProgram.SelectedIndex];
			checkEnabled.Checked=Programs.Cur.Enabled;
		}*/

		/*private void checkEnabled_Click(object sender, System.EventArgs e) {
			if(listProgram.SelectedIndex==-1) return;
			Programs.Cur.Enabled=checkEnabled.Checked;
			Programs.UpdateCur();
		}*/

		private void butAdd_Click(object sender, System.EventArgs e) {
			Programs.Cur=new Program();
			FormProgramLinkEdit FormPE=new FormProgramLinkEdit();
			FormPE.IsNew=true;
			FormPE.ShowDialog();
			FillList();
		}

		private void listProgram_DoubleClick(object sender, System.EventArgs e) {
			if(listProgram.SelectedIndex==-1)
				return;
			Programs.Cur=Programs.List[listProgram.SelectedIndex];
			FormProgramLinkEdit FormPE=new FormProgramLinkEdit();
			FormPE.ShowDialog();
			FillList();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormProgramLinks_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
		}

	

		

		

		



		
	}
}
