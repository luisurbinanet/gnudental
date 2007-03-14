using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormCounties : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private OpenDental.XPButton butDelete;
		private OpenDental.XPButton butAdd;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox listCounties;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormCounties()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
			});
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormCounties));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.listCounties = new System.Windows.Forms.ListBox();
			this.butDelete = new OpenDental.XPButton();
			this.butAdd = new OpenDental.XPButton();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(338, 605);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(86, 26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(338, 564);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(86, 26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// listCounties
			// 
			this.listCounties.Location = new System.Drawing.Point(12, 30);
			this.listCounties.Name = "listCounties";
			this.listCounties.Size = new System.Drawing.Size(298, 602);
			this.listCounties.TabIndex = 2;
			this.listCounties.DoubleClick += new System.EventHandler(this.listCounties_DoubleClick);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(338, 461);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(86, 26);
			this.butDelete.TabIndex = 72;
			this.butDelete.Text = "&Remove";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(338, 423);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(86, 26);
			this.butAdd.TabIndex = 71;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(11, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(276, 19);
			this.label1.TabIndex = 73;
			this.label1.Text = "This is a list of Counties  for Public Health";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FormCounties
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(464, 646);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.listCounties);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormCounties";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Counties";
			this.Load += new System.EventHandler(this.FormCounties_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormCounties_Load(object sender, System.EventArgs e) {
			FillList();
		}

		private void FillList(){
			Counties.Refresh();
			listCounties.Items.Clear();
			string s="";
			for(int i=0;i<Counties.List.Length;i++){
				s=Counties.List[i].CountyName;
				if(Counties.List[i].CountyCode != ""){
					s+=", "+Counties.List[i].CountyCode;
				}
				listCounties.Items.Add(s);
			}
		}

		private void listCounties_DoubleClick(object sender, System.EventArgs e) {
			if(listCounties.SelectedIndex==-1){
				return;
			}
			Counties.Cur=Counties.List[listCounties.SelectedIndex];
			FormCountyEdit FormSE=new FormCountyEdit();
			FormSE.ShowDialog();
			if(FormSE.DialogResult!=DialogResult.OK){
				return;
			}
			FillList();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormCountyEdit FormSE=new FormCountyEdit();
			FormSE.IsNew=true;
			Counties.Cur=new County();
			FormSE.ShowDialog();
			if(FormSE.DialogResult!=DialogResult.OK){
				return;
			}
			FillList();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(listCounties.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			Counties.Cur=Counties.List[listCounties.SelectedIndex];
			string usedBy=Counties.UsedBy(Counties.Cur.CountyName);
			if(usedBy != ""){
				MessageBox.Show(Lan.g(this,"Cannot delete County because it is already in use by the following patients: \r")+usedBy);
				return;
			}
			Counties.DeleteCur();
			FillList();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		


	}
}




















