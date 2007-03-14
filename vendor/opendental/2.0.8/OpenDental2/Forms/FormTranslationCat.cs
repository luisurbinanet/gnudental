using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace OpenDental{

	public class FormTranslationCat : System.Windows.Forms.Form{
		private System.Windows.Forms.ListBox listCats;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button butClose;
		private System.ComponentModel.Container components = null;

		public FormTranslationCat(){
			InitializeComponent();
			Lan.C("All", new System.Windows.Forms.Control[] {
			  butClose,																											
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
			this.listCats = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listCats
			// 
			this.listCats.Location = new System.Drawing.Point(28, 34);
			this.listCats.Name = "listCats";
			this.listCats.Size = new System.Drawing.Size(262, 589);
			this.listCats.TabIndex = 0;
			this.listCats.DoubleClick += new System.EventHandler(this.listCats_DoubleClick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(28, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(214, 18);
			this.label1.TabIndex = 1;
			this.label1.Text = "Select a category";
			// 
			// butClose
			// 
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(320, 592);
			this.butClose.Name = "butClose";
			this.butClose.TabIndex = 2;
			this.butClose.Text = "Close";
			// 
			// FormTranslationCat
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(422, 648);
			this.ControlBox = false;
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listCats);
			this.Name = "FormTranslationCat";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Category";
			this.Load += new System.EventHandler(this.FormTranslation_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormTranslation_Load(object sender, System.EventArgs e) {
			if(CultureInfo.CurrentCulture.TwoLetterISOLanguageName=="en"){
				MessageBox.Show
					("You must change your language in Windows first to something other than English.");
				DialogResult=DialogResult.Cancel;
				return;
			}
			Lan.GetListCat();
			FillList();
		}

		private void FillList(){
			listCats.Items.Clear();
			for(int i=0;i<Lan.ListCat.Length;i++){
				listCats.Items.Add(Lan.ListCat[i]);
			}

		}

		private void listCats_DoubleClick(object sender, System.EventArgs e) {
			Lan.CurCat=Lan.ListCat[listCats.SelectedIndex];
			FormTranslation FormT=new FormTranslation(); 
			FormT.ShowDialog();
		}


	}
}
