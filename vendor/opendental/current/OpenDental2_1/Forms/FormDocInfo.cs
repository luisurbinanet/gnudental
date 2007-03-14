/*=============================================================================================================
FreeDental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text; 

namespace OpenDental{

	public class FormDocInfo : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.ListBox listCategory;
		private System.Windows.Forms.TextBox textDescript;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private OpenDental.ValidDate textDate;
		private System.ComponentModel.Container components = null;//required by designer
		private System.Windows.Forms.TextBox textFileName;
		public bool IsNew;
		
		public FormDocInfo(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label1,
				this.label2,
				this.label3,
				this.label4,
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.listCategory = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textDescript = new System.Windows.Forms.TextBox();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textFileName = new System.Windows.Forms.TextBox();
			this.textDate = new OpenDental.ValidDate();
			this.SuspendLayout();
			// 
			// listCategory
			// 
			this.listCategory.Location = new System.Drawing.Point(12, 36);
			this.listCategory.Name = "listCategory";
			this.listCategory.Size = new System.Drawing.Size(104, 225);
			this.listCategory.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Select Category:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(138, 162);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(124, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Optional Description:";
			// 
			// textDescript
			// 
			this.textDescript.Location = new System.Drawing.Point(138, 178);
			this.textDescript.MaxLength = 255;
			this.textDescript.Multiline = true;
			this.textDescript.Name = "textDescript";
			this.textDescript.Size = new System.Drawing.Size(364, 111);
			this.textDescript.TabIndex = 2;
			this.textDescript.Text = "";
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(331, 314);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 3;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.Location = new System.Drawing.Point(429, 314);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(141, 90);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(68, 12);
			this.label3.TabIndex = 6;
			this.label3.Text = "Date:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(141, 33);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 16);
			this.label4.TabIndex = 8;
			this.label4.Text = "File Name:";
			// 
			// textFileName
			// 
			this.textFileName.Location = new System.Drawing.Point(141, 49);
			this.textFileName.Name = "textFileName";
			this.textFileName.ReadOnly = true;
			this.textFileName.Size = new System.Drawing.Size(362, 20);
			this.textFileName.TabIndex = 9;
			this.textFileName.Text = "";
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(141, 106);
			this.textDate.Name = "textDate";
			this.textDate.TabIndex = 1;
			this.textDate.Text = "";
			// 
			// FormDocInfo
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(533, 356);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.textDescript);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listCategory);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textFileName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormDocInfo";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = Lan.g(this,"Document Info");
			this.Load += new System.EventHandler(this.FormDocInfo_Load);
			this.ResumeLayout(false);

		}
		#endregion

		public void FormDocInfo_Load(object sender, System.EventArgs e){
			//if (Docs.Cur.FileName.Equals(null))
			if(IsNew){
				Documents.Cur.DateCreated=DateTime.Today;
				Documents.Cur.WithPat=Patients.Cur.PatNum;
			}
			listCategory.Items.Clear();
			for(int i=0;i<Defs.Short[(int)DefCat.DocumentCats].Length;i++){
				listCategory.Items.Add(Defs.Short[(int)DefCat.DocumentCats][i].ItemName);
				if(Defs.Short[(int)DefCat.DocumentCats][i].DefNum==Documents.Cur.DocCategory)
					listCategory.SelectedIndex=i;
			}
			textDate.Text=Documents.Cur.DateCreated.ToString("d");
			textDescript.Text=Documents.Cur.Description;
		  textFileName.Text=Documents.Cur.FileName;
			if (IsNew)  
        listCategory.SelectedIndex = 0;
		}

		private void butOK_Click(object sender, System.EventArgs e){
			Documents.Cur.DocCategory=Defs.Short[(int)DefCat.DocumentCats][listCategory.SelectedIndex].DefNum;
			Documents.Cur.Description=textDescript.Text;			Documents.Cur.DateCreated=DateTime.Parse(textDate.Text);
      //Docs.Cur.LastAltered=DateTime.Today;
			if(IsNew){
				Documents.InsertCur();
			}
			else{
				Documents.UpdateCur();
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
		
	}
}