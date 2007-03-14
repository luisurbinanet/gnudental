using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	public class FormQueryEdit : System.Windows.Forms.Form{
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textTitle;
		private System.Windows.Forms.TextBox textQuery;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textFileName;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;// Required designer variable.
		public bool IsNew;

		public FormQueryEdit(){
			InitializeComponent();// Required for Windows Form Designer support
			Lan.C(this, new System.Windows.Forms.Control[] {
				label1,
				label2,
				label3,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
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
			this.label1 = new System.Windows.Forms.Label();
			this.textTitle = new System.Windows.Forms.TextBox();
			this.textQuery = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textFileName = new System.Windows.Forms.TextBox();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(38, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Title";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textTitle
			// 
			this.textTitle.Location = new System.Drawing.Point(76, 12);
			this.textTitle.Name = "textTitle";
			this.textTitle.Size = new System.Drawing.Size(328, 20);
			this.textTitle.TabIndex = 0;
			this.textTitle.Text = "";
			// 
			// textQuery
			// 
			this.textQuery.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textQuery.Location = new System.Drawing.Point(76, 36);
			this.textQuery.Multiline = true;
			this.textQuery.Name = "textQuery";
			this.textQuery.Size = new System.Drawing.Size(624, 582);
			this.textQuery.TabIndex = 1;
			this.textQuery.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(68, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Query Text";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(18, 638);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 16);
			this.label3.TabIndex = 4;
			this.label3.Text = "Export File Name";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textFileName
			// 
			this.textFileName.Location = new System.Drawing.Point(122, 636);
			this.textFileName.Name = "textFileName";
			this.textFileName.Size = new System.Drawing.Size(326, 20);
			this.textFileName.TabIndex = 2;
			this.textFileName.Text = "";
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(724, 588);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 3;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(724, 626);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "Cancel";
			// 
			// FormQueryEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(820, 670);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.textFileName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textQuery);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textTitle);
			this.Controls.Add(this.label1);
			this.Name = "FormQueryEdit";
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




	}
}
