using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormPerioEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private OpenDental.ValidDate textDate;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox listProv;
		private System.Windows.Forms.Label label2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormPerioEdit()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormPerioEdit));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.textDate = new OpenDental.ValidDate();
			this.label1 = new System.Windows.Forms.Label();
			this.listProv = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(347, 164);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(347, 123);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(151, 18);
			this.textDate.Name = "textDate";
			this.textDate.TabIndex = 2;
			this.textDate.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(14, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(136, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "Exam Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(151, 55);
			this.listProv.Name = "listProv";
			this.listProv.Size = new System.Drawing.Size(120, 134);
			this.listProv.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(13, 51);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(136, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "Provider";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormPerioEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(455, 215);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPerioEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Perio Date";
			this.Load += new System.EventHandler(this.FormPerioEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormPerioEdit_Load(object sender, System.EventArgs e) {
			textDate.Text=PerioExams.Cur.ExamDate.ToShortDateString();
			listProv.Items.Clear();
			for(int i=0;i<Providers.List.Length;i++){
				listProv.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==PerioExams.Cur.ProvNum){
					listProv.SelectedIndex=i;
				}
			}
			if(listProv.SelectedIndex==-1)
				listProv.SelectedIndex=0;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(  textDate.errorProvider1.GetError(textDate)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			PerioExams.Cur.ExamDate=PIn.PDate(textDate.Text);
			PerioExams.Cur.ProvNum=Providers.List[listProv.SelectedIndex].ProvNum;
			PerioExams.UpdateCur();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	


	}
}





















