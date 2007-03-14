using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormZipSelect : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butAdd;
		private System.Windows.Forms.ListBox listMatches;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button butEdit;
		private System.Windows.Forms.Button butDelete;
		private System.ComponentModel.Container components=null;

		public FormZipSelect(){
			InitializeComponent();

			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
				butAdd,
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormZipSelect));
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.butAdd = new System.Windows.Forms.Button();
			this.listMatches = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butEdit = new System.Windows.Forms.Button();
			this.butDelete = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(336, 153);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 0;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(336, 186);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 1;
			this.butCancel.Text = "Cancel";
			// 
			// butAdd
			// 
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(26, 182);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(76, 27);
			this.butAdd.TabIndex = 2;
			this.butAdd.Text = "Add";
			this.butAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// listMatches
			// 
			this.listMatches.Location = new System.Drawing.Point(24, 45);
			this.listMatches.Name = "listMatches";
			this.listMatches.Size = new System.Drawing.Size(197, 95);
			this.listMatches.TabIndex = 3;
			this.listMatches.DoubleClick += new System.EventHandler(this.listMatches_DoubleClick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(25, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(377, 26);
			this.label1.TabIndex = 4;
			this.label1.Text = "Cities attached to this zipcode:";
			// 
			// butEdit
			// 
			this.butEdit.Image = ((System.Drawing.Image)(resources.GetObject("butEdit.Image")));
			this.butEdit.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.butEdit.Location = new System.Drawing.Point(106, 182);
			this.butEdit.Name = "butEdit";
			this.butEdit.Size = new System.Drawing.Size(82, 27);
			this.butEdit.TabIndex = 5;
			this.butEdit.Text = "Edit";
			this.butEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
			// 
			// butDelete
			// 
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(192, 182);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(82, 27);
			this.butDelete.TabIndex = 6;
			this.butDelete.Text = "Delete";
			this.butDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormZipSelect
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(430, 235);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butEdit);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listMatches);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormZipSelect";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Zipcode";
			this.Load += new System.EventHandler(this.FormZipSelect_Load);
			this.ResumeLayout(false);

		}
		#endregion

		//This form is only accessed directly from the patient edit window, either by pushing the
		//button, or when user enters a zipcode that has more than one city available.
		
		private void FormZipSelect_Load(object sender, System.EventArgs e) {
		  FillList();
		}
		
		private void FillList(){ 
			listMatches.Items.Clear();
			string itemText="";
			for(int i=0;i<ZipCodes.ALMatches.Count;i++){ 
				itemText=((ZipCode)ZipCodes.ALMatches[i]).City+" "
					+((ZipCode)ZipCodes.ALMatches[i]).State;
				if(((ZipCode)ZipCodes.ALMatches[i]).IsFrequent){
					itemText+=Lan.g(this," (freq)");
				}
				listMatches.Items.Add(itemText);
			}
			listMatches.SelectedIndex=-1;
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormZipCodeEdit FormZCE=new FormZipCodeEdit();
			ZipCodes.Cur=new ZipCode();
			ZipCodes.Cur.ZipCodeDigits=((ZipCode)ZipCodes.ALMatches[0]).ZipCodeDigits;
			FormZCE.IsNew=true;
			FormZCE.ShowDialog();
			if(FormZCE.DialogResult!=DialogResult.OK){
				return;
			}
			ZipCodes.Refresh();
			ZipCodes.GetALMatches(ZipCodes.Cur.ZipCodeDigits);
			FillList();
		}

		private void butEdit_Click(object sender, System.EventArgs e) {
			if(listMatches.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			ZipCodes.Cur=(ZipCode)ZipCodes.ALMatches[listMatches.SelectedIndex];
			FormZipCodeEdit FormZCE=new FormZipCodeEdit();
			FormZCE.ShowDialog();
			if(FormZCE.DialogResult!=DialogResult.OK){
				return;
			}
			ZipCodes.Refresh();
			ZipCodes.GetALMatches(ZipCodes.Cur.ZipCodeDigits);
			FillList();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(listMatches.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			ZipCodes.Cur=(ZipCode)ZipCodes.ALMatches[listMatches.SelectedIndex];
			ZipCodes.DeleteCur();
			ZipCodes.Refresh();
			ZipCodes.GetALMatches(ZipCodes.Cur.ZipCodeDigits);
			FillList();
		}

		private void listMatches_DoubleClick(object sender, System.EventArgs e) {
			if(listMatches.SelectedIndex==-1){
				return;
			}
			ZipCodes.Cur=(ZipCode)ZipCodes.ALMatches[listMatches.SelectedIndex];
			DialogResult=DialogResult.OK;		
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(listMatches.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			ZipCodes.Cur=(ZipCode)ZipCodes.ALMatches[listMatches.SelectedIndex];
			DialogResult=DialogResult.OK;
		}

		


	}
}





