using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormCountyEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textCountyName;
		private System.Windows.Forms.TextBox textCountyCode;
		///<summary></summary>
		public bool IsNew;

		///<summary></summary>
		public FormCountyEdit()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormCountyEdit));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textCountyName = new System.Windows.Forms.TextBox();
			this.textCountyCode = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(355, 189);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(86, 26);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(355, 148);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(86, 26);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(13, 45);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(130, 20);
			this.label1.TabIndex = 2;
			this.label1.Text = "County Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCountyName
			// 
			this.textCountyName.Location = new System.Drawing.Point(144, 47);
			this.textCountyName.Name = "textCountyName";
			this.textCountyName.Size = new System.Drawing.Size(297, 20);
			this.textCountyName.TabIndex = 0;
			this.textCountyName.Text = "";
			this.textCountyName.TextChanged += new System.EventHandler(this.textCountyName_TextChanged);
			// 
			// textCountyCode
			// 
			this.textCountyCode.Location = new System.Drawing.Point(144, 83);
			this.textCountyCode.Name = "textCountyCode";
			this.textCountyCode.Size = new System.Drawing.Size(297, 20);
			this.textCountyCode.TabIndex = 1;
			this.textCountyCode.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(64, 86);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(78, 52);
			this.label2.TabIndex = 4;
			this.label2.Text = "County Code (use varies)";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormCountyEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(486, 264);
			this.Controls.Add(this.textCountyCode);
			this.Controls.Add(this.textCountyName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormCountyEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "County Edit";
			this.Load += new System.EventHandler(this.FormCountyEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormCountyEdit_Load(object sender, System.EventArgs e) {
			textCountyName.Text=Counties.Cur.CountyName;
			textCountyCode.Text=Counties.Cur.CountyCode;
		}

		private void textCountyName_TextChanged(object sender, System.EventArgs e) {
			if(textCountyName.Text.Length==1){
				textCountyName.Text=textCountyName.Text.ToUpper();
				textCountyName.SelectionStart=1;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			Counties.Cur.CountyName=textCountyName.Text;
			Counties.Cur.CountyCode=textCountyCode.Text;
			if(IsNew){
				if(Counties.DoesExist(Counties.Cur.CountyName)){
					MessageBox.Show(Lan.g(this,"County name already exists. Duplicate not allowed."));
					return;
				}
				Counties.InsertCur();
			}
			else{//existing County
				if(Counties.Cur.CountyName!=Counties.Cur.OldCountyName){//County name was changed
					if(Counties.DoesExist(Counties.Cur.CountyName)){//changed to a name that already exists.
						MessageBox.Show(Lan.g(this,"County name already exists. Duplicate not allowed."));
						return;
					}
				}
				Counties.UpdateCur();
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		


	}
}




















