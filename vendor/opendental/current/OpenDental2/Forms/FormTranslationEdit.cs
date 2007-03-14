using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Globalization;
using System.IO;

namespace OpenDental{

	public class FormTranslationEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textEnglish;
		private System.Windows.Forms.Label label4;
		private bool IsNew;
		//private bool IsNewCulTran;
		private System.Windows.Forms.TextBox textComments;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textEnglishComments;
		private System.Windows.Forms.TextBox textTranslation;
		private System.ComponentModel.Container components = null;

		public FormTranslationEdit(){
			InitializeComponent();
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
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textEnglish = new System.Windows.Forms.TextBox();
			this.textTranslation = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textComments = new System.Windows.Forms.TextBox();
			this.textEnglishComments = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(786, 594);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 0;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(786, 628);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 1;
			this.butCancel.Text = "Cancel";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "English";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 308);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 16);
			this.label2.TabIndex = 5;
			this.label2.Text = "Translation";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textEnglish
			// 
			this.textEnglish.AcceptsReturn = true;
			this.textEnglish.Location = new System.Drawing.Point(100, 34);
			this.textEnglish.Multiline = true;
			this.textEnglish.Name = "textEnglish";
			this.textEnglish.ReadOnly = true;
			this.textEnglish.Size = new System.Drawing.Size(672, 130);
			this.textEnglish.TabIndex = 6;
			this.textEnglish.Text = "";
			// 
			// textTranslation
			// 
			this.textTranslation.AcceptsReturn = true;
			this.textTranslation.Location = new System.Drawing.Point(100, 306);
			this.textTranslation.Multiline = true;
			this.textTranslation.Name = "textTranslation";
			this.textTranslation.Size = new System.Drawing.Size(672, 130);
			this.textTranslation.TabIndex = 7;
			this.textTranslation.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(18, 444);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(82, 14);
			this.label4.TabIndex = 10;
			this.label4.Text = "Comments";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textComments
			// 
			this.textComments.AcceptsReturn = true;
			this.textComments.Location = new System.Drawing.Point(100, 442);
			this.textComments.Multiline = true;
			this.textComments.Name = "textComments";
			this.textComments.Size = new System.Drawing.Size(672, 130);
			this.textComments.TabIndex = 11;
			this.textComments.Text = "";
			// 
			// textEnglishComments
			// 
			this.textEnglishComments.AcceptsReturn = true;
			this.textEnglishComments.Location = new System.Drawing.Point(100, 170);
			this.textEnglishComments.Multiline = true;
			this.textEnglishComments.Name = "textEnglishComments";
			this.textEnglishComments.ReadOnly = true;
			this.textEnglishComments.Size = new System.Drawing.Size(672, 130);
			this.textEnglishComments.TabIndex = 13;
			this.textEnglishComments.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 170);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 40);
			this.label3.TabIndex = 12;
			this.label3.Text = "English Comments";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormTranslationEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(880, 668);
			this.ControlBox = false;
			this.Controls.Add(this.textEnglishComments);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textComments);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textTranslation);
			this.Controls.Add(this.textEnglish);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Name = "FormTranslationEdit";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Translation Edit";
			this.Load += new System.EventHandler(this.FormTranslationEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormTranslationEdit_Load(object sender, System.EventArgs e){
			#if(DEBUG)
				textEnglishComments.ReadOnly=false;
			#endif
			textEnglish.Text=Lan.Cur.English;
			textEnglishComments.Text=Lan.Cur.EnglishComments;
			if(LanguageForeigns.HList.ContainsKey(Lan.Cur.ClassType+Lan.Cur.English)){
				LanguageForeigns.Cur=((LanguageForeign)LanguageForeigns.HList[Lan.Cur.ClassType+Lan.Cur.English]);
				textTranslation.Text=LanguageForeigns.Cur.Translation;
				textComments.Text=LanguageForeigns.Cur.Comments;
				Text="Edit Translation";
				IsNew=false;
			}
			else{
				LanguageForeigns.Cur=new LanguageForeign();
				LanguageForeigns.Cur.ClassType=Lan.Cur.ClassType;
				LanguageForeigns.Cur.English=Lan.Cur.English;
				LanguageForeigns.Cur.Culture=CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
				Text="Add Translation";
				IsNew=true;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			LanguageForeigns.Cur.Translation=textTranslation.Text;
			LanguageForeigns.Cur.Comments=textComments.Text;
			#if(DEBUG)
				if(Lan.Cur.EnglishComments!=textEnglishComments.Text){
					Lan.Cur.EnglishComments=textEnglishComments.Text;
					Lan.UpdateCur();
					Lan.Refresh();
				}
			#endif
			if(textTranslation.Text==""){
				if(IsNew){
					
				}
				else{
					LanguageForeigns.DeleteCur();
				}
				DialogResult=DialogResult.OK;
				return;
			}
			if(IsNew){
				LanguageForeigns.InsertCur();
			}
			else{
				LanguageForeigns.UpdateCur();
			}
			DialogResult=DialogResult.OK;
		}

	}
}
