using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormRxSetup : System.Windows.Forms.Form{
		private System.ComponentModel.Container components = null;
		private OpenDental.TableRxSetup tbMain;
		private System.Windows.Forms.Button butAdd;
		private System.Windows.Forms.Button butDelete;
		private System.Windows.Forms.Button butAdd2;
		private System.Windows.Forms.Button butClose;// Required designer variable.

		public FormRxSetup(){
			InitializeComponent();// Required for Windows Form Designer support
			tbMain.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbMain_CellClicked);
			tbMain.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbMain_CellDoubleClicked);
			Lan.C(this, new System.Windows.Forms.Control[] {
				butAdd2
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butDelete,
				butAdd,
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormRxSetup));
			this.tbMain = new OpenDental.TableRxSetup();
			this.butClose = new System.Windows.Forms.Button();
			this.butAdd = new System.Windows.Forms.Button();
			this.butDelete = new System.Windows.Forms.Button();
			this.butAdd2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// tbMain
			// 
			this.tbMain.BackColor = System.Drawing.SystemColors.Window;
			this.tbMain.Location = new System.Drawing.Point(8, 12);
			this.tbMain.Name = "tbMain";
			this.tbMain.SelectionMode = SelectionMode.None;//OpenDental.SelectRowsMode.None;
			this.tbMain.Size = new System.Drawing.Size(919, 582);
			this.tbMain.TabIndex = 0;
			// 
			// butClose
			// 
			this.butClose.Location = new System.Drawing.Point(850, 636);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 26);
			this.butClose.TabIndex = 4;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butAdd
			// 
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(594, 636);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(100, 26);
			this.butAdd.TabIndex = 2;
			this.butAdd.Text = "          Add New";
			this.butAdd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butDelete
			// 
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(704, 636);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(85, 26);
			this.butDelete.TabIndex = 3;
			this.butDelete.Text = "          Delete";
			this.butDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butAdd2
			// 
			this.butAdd2.Image = ((System.Drawing.Image)(resources.GetObject("butAdd2.Image")));
			this.butAdd2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd2.Location = new System.Drawing.Point(356, 636);
			this.butAdd2.Name = "butAdd2";
			this.butAdd2.Size = new System.Drawing.Size(227, 27);
			this.butAdd2.TabIndex = 1;
			this.butAdd2.Text = "          Add Using Selected as Starting Pt.";
			this.butAdd2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd2.Click += new System.EventHandler(this.butAdd2_Click);
			// 
			// FormRxSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(942, 674);
			this.ControlBox = false;
			this.Controls.Add(this.butAdd2);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.tbMain);
			this.Name = "FormRxSetup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Rx Setup";
			this.Load += new System.EventHandler(this.FormRxSetup_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRxSetup_Load(object sender, System.EventArgs e) {
			if(!UserPermissions.CheckUserPassword("Prescription Setup")){
				MessageBox.Show(Lan.g(this,"You do not have permission for this feature"));
				DialogResult=DialogResult.Cancel;
				return;
			}
			FillMain();
		}

		private void FillMain(){
			RxDefs.Refresh();
			tbMain.SelectedRow=-1;
			tbMain.ResetRows(RxDefs.List.Length);
			for(int i=0;i<RxDefs.List.Length;i++){
				tbMain.Cell[0,i]=RxDefs.List[i].Drug;
				tbMain.Cell[1,i]=RxDefs.List[i].Sig;
				tbMain.Cell[2,i]=RxDefs.List[i].Disp;
				tbMain.Cell[3,i]=RxDefs.List[i].Refills;
				tbMain.Cell[4,i]=RxDefs.List[i].Notes;
			}
			tbMain.SetGridColor(Color.LightGray);
			tbMain.LayoutTables();
		}

		private void tbMain_CellClicked(object sender, CellEventArgs e){
			if(tbMain.SelectedRow!=-1)
				tbMain.ColorRow(tbMain.SelectedRow,Color.White);
			tbMain.SelectedRow=e.Row;
			tbMain.ColorRow(e.Row,Color.LightGray);
			RxDefs.Cur=RxDefs.List[tbMain.SelectedRow];
		}

		private void tbMain_CellDoubleClicked(object sender, CellEventArgs e){
			FormRxDefEdit FormE=new FormRxDefEdit();
			FormE.IsNew=false;
			FormE.ShowDialog();
			FillMain();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			RxDefs.Cur=new RxDef();
			FormRxDefEdit FormE=new FormRxDefEdit();
			FormE.IsNew=true;
			FormE.ShowDialog();
			FillMain();
		}

		private void butAdd2_Click(object sender, System.EventArgs e) {
			if(tbMain.SelectedRow==-1){
				MessageBox.Show(Lan.g(this,"Please select item first"));
				return;
			}
			FormRxDefEdit FormE=new FormRxDefEdit();
			FormE.IsNew=true;
			FormE.ShowDialog();
			FillMain();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(tbMain.SelectedRow==-1){
				MessageBox.Show(Lan.g(this,"Please select item first"));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete item?  This can be done safely without altering any patient data.")
				,"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				FillMain();
				return;
			}
			RxDefs.DeleteCur();
			FillMain();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			SecurityLogs.MakeLogEntry("Prescription Setup","Altered Prescription Setup");
			DialogResult=DialogResult.Cancel;
		}

	}
}
