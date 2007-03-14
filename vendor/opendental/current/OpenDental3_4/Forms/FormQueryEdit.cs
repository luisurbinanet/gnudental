using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	///<summary></summary>
	public class FormQueryEdit : System.Windows.Forms.Form{
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textTitle;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textFileName;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private OpenDental.ODtextBox textQuery;// Required designer variable.
		///<summary></summary>
		public bool IsNew;

		///<summary></summary>
		public FormQueryEdit(){
			InitializeComponent();// Required for Windows Form Designer support
			Lan.F(this);
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
			this.label1 = new System.Windows.Forms.Label();
			this.textTitle = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textFileName = new System.Windows.Forms.TextBox();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textQuery = new OpenDental.ODtextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(5, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(115, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Title";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textTitle
			// 
			this.textTitle.Location = new System.Drawing.Point(122, 10);
			this.textTitle.Name = "textTitle";
			this.textTitle.Size = new System.Drawing.Size(328, 20);
			this.textTitle.TabIndex = 0;
			this.textTitle.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(3, 42);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(117, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Query Text";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(19, 636);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(145, 16);
			this.label3.TabIndex = 4;
			this.label3.Text = "Export File Name";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textFileName
			// 
			this.textFileName.Location = new System.Drawing.Point(168, 634);
			this.textFileName.Name = "textFileName";
			this.textFileName.Size = new System.Drawing.Size(326, 20);
			this.textFileName.TabIndex = 2;
			this.textFileName.Text = "";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(770, 586);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(770, 624);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textQuery
			// 
			this.textQuery.AcceptsReturn = true;
			this.textQuery.Location = new System.Drawing.Point(121, 35);
			this.textQuery.Multiline = true;
			this.textQuery.Name = "textQuery";
			this.textQuery.QuickPasteType = OpenDental.QuickPasteType.Query;
			this.textQuery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textQuery.Size = new System.Drawing.Size(624, 588);
			this.textQuery.TabIndex = 5;
			this.textQuery.Text = "";
			// 
			// FormQueryEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(862, 670);
			this.Controls.Add(this.textQuery);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.textFileName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textTitle);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormQueryEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Favorite";
			this.Load += new System.EventHandler(this.FormQueryEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormQueryEdit_Load(object sender, System.EventArgs e) {
			textTitle.Text=UserQueries.Cur.Description;
			textQuery.Text=UserQueries.Cur.QueryText;
			textFileName.Text=UserQueries.Cur.FileName;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textTitle.Text==""){
				MessageBox.Show(Lan.g(this,"Please enter a title first."));
				return;
			}
			UserQueries.Cur.Description=textTitle.Text;
			UserQueries.Cur.QueryText=textQuery.Text;
			UserQueries.Cur.FileName=textFileName.Text;
			if(IsNew){
				UserQueries.InsertCur();
			}
			else{
				UserQueries.UpdateCur();
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
		
		}




	}
}
