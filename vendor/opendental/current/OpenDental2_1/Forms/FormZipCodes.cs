using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormZipCodes : System.Windows.Forms.Form{
		private OpenDental.TableZips tbZips;
		private System.Windows.Forms.Button butAdd;
		private System.Windows.Forms.Button butDelete;
		private System.Windows.Forms.Button butClose;
		private System.ComponentModel.Container components = null;

		public FormZipCodes(){
			InitializeComponent();
      tbZips.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbZips_CellDoubleClicked);
			Lan.C("All", new System.Windows.Forms.Control[] {
				butClose,
				butAdd,
				butDelete,
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
			this.tbZips = new OpenDental.TableZips();
			this.butAdd = new System.Windows.Forms.Button();
			this.butClose = new System.Windows.Forms.Button();
			this.butDelete = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// tbZips
			// 
			this.tbZips.BackColor = System.Drawing.SystemColors.Window;
			this.tbZips.Location = new System.Drawing.Point(19, 14);
			this.tbZips.Name = "tbZips";
			this.tbZips.SelectedIndices = new int[0];
			this.tbZips.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbZips.Size = new System.Drawing.Size(519, 531);
			this.tbZips.TabIndex = 25;
			// 
			// butAdd
			// 
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(615, 374);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(76, 26);
			this.butAdd.TabIndex = 28;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butClose
			// 
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Location = new System.Drawing.Point(615, 513);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(76, 26);
			this.butClose.TabIndex = 26;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butDelete
			// 
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.Location = new System.Drawing.Point(615, 410);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(76, 26);
			this.butDelete.TabIndex = 31;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormZipCodes
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(715, 563);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.tbZips);
			this.Name = "FormZipCodes";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Zip Codes";
			this.Load += new System.EventHandler(this.FormZipCodes_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormZipCodes_Load(object sender, System.EventArgs e) {
		  FillTable();
		}

		private void FillTable(){
			ZipCodes.Refresh();
  		tbZips.ResetRows(ZipCodes.List.Length);
			tbZips.SetGridColor(Color.Gray);
			tbZips.SetBackGColor(Color.White);      
			for(int i=0;i<ZipCodes.List.Length;i++){
				tbZips.Cell[0,i]=ZipCodes.List[i].ZipCodeDigits;
				tbZips.Cell[1,i]=ZipCodes.List[i].City;
				tbZips.Cell[2,i]=ZipCodes.List[i].State;
				if(ZipCodes.List[i].IsFrequent){
					tbZips.Cell[3,i]="X";
				}
			}
			tbZips.SelectedRow=-1;
			tbZips.LayoutTables(); 
		}

		private void tbZips_CellDoubleClicked(object sender, CellEventArgs e){
			if(tbZips.SelectedRow==-1){
				return;
			}
			ZipCodes.Cur=ZipCodes.List[tbZips.SelectedRow];
      FormZipCodeEdit FormZCE=new FormZipCodeEdit();
			FormZCE.ShowDialog();
			FillTable(); 
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(tbZips.SelectedRow==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}	
			ZipCodes.Cur=ZipCodes.List[tbZips.SelectedRow];		
			if (MessageBox.Show(Lan.g(this,"Delete Zipcode?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;   
			}
			ZipCodes.DeleteCur();
			FillTable();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			ZipCodes.Cur=new ZipCode();
			FormZipCodeEdit FormZCE=new FormZipCodeEdit();
			FormZCE.IsNew=true;
			FormZCE.ShowDialog();
			FillTable(); 				
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
			Close();
		}
	

	}
}
