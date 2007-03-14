using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormInsCatsSetup : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox listCovCats;
		private System.Windows.Forms.Button butUp;
		private System.Windows.Forms.Button butDown;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button butAddCat;
		private System.Windows.Forms.Button butOK;
		private OpenDental.TableCovSpans tbMain;
		private System.Windows.Forms.Button butAddSpan;
		private System.Windows.Forms.Button butDelete;

		public FormInsCatsSetup(){
			InitializeComponent();
			tbMain.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbMain_CellClicked);
			tbMain.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbMain_CellDoubleClicked);
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label1,
				this.butAddCat,
				this.butAddSpan,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butUp,
				butDown,
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormInsCatsSetup));
			this.label1 = new System.Windows.Forms.Label();
			this.listCovCats = new System.Windows.Forms.ListBox();
			this.butUp = new System.Windows.Forms.Button();
			this.butDown = new System.Windows.Forms.Button();
			this.butAddCat = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.tbMain = new OpenDental.TableCovSpans();
			this.butAddSpan = new System.Windows.Forms.Button();
			this.butDelete = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(72, 96);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(134, 16);
			this.label1.TabIndex = 3;
			this.label1.Text = "Coverage Categories";
			// 
			// listCovCats
			// 
			this.listCovCats.Location = new System.Drawing.Point(72, 116);
			this.listCovCats.Name = "listCovCats";
			this.listCovCats.Size = new System.Drawing.Size(120, 199);
			this.listCovCats.TabIndex = 0;
			this.listCovCats.DoubleClick += new System.EventHandler(this.listCovCats_DoubleClick);
			// 
			// butUp
			// 
			this.butUp.Image = ((System.Drawing.Image)(resources.GetObject("butUp.Image")));
			this.butUp.Location = new System.Drawing.Point(72, 330);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(54, 24);
			this.butUp.TabIndex = 1;
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// butDown
			// 
			this.butDown.Image = ((System.Drawing.Image)(resources.GetObject("butDown.Image")));
			this.butDown.Location = new System.Drawing.Point(140, 330);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(52, 24);
			this.butDown.TabIndex = 2;
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// butAddCat
			// 
			this.butAddCat.Image = ((System.Drawing.Image)(resources.GetObject("butAddCat.Image")));
			this.butAddCat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddCat.Location = new System.Drawing.Point(72, 364);
			this.butAddCat.Name = "butAddCat";
			this.butAddCat.Size = new System.Drawing.Size(74, 24);
			this.butAddCat.TabIndex = 3;
			this.butAddCat.Text = "          Add";
			this.butAddCat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddCat.Click += new System.EventHandler(this.butAddCat_Click);
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(682, 578);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 6;
			this.butOK.Text = "Close";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// tbMain
			// 
			this.tbMain.BackColor = System.Drawing.SystemColors.Window;
			this.tbMain.Location = new System.Drawing.Point(306, 14);
			this.tbMain.Name = "tbMain";
			this.tbMain.SelectionMode = SelectionMode.None;//OpenDental.SelectRowsMode.None;
			this.tbMain.Size = new System.Drawing.Size(329, 586);
			this.tbMain.TabIndex = 8;
			// 
			// butAddSpan
			// 
			this.butAddSpan.Image = ((System.Drawing.Image)(resources.GetObject("butAddSpan.Image")));
			this.butAddSpan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddSpan.Location = new System.Drawing.Point(680, 444);
			this.butAddSpan.Name = "butAddSpan";
			this.butAddSpan.Size = new System.Drawing.Size(81, 26);
			this.butAddSpan.TabIndex = 4;
			this.butAddSpan.Text = "          Add";
			this.butAddSpan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddSpan.Click += new System.EventHandler(this.butAddSpan_Click);
			// 
			// butDelete
			// 
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(680, 486);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(81, 26);
			this.butDelete.TabIndex = 5;
			this.butDelete.Text = "         Delete";
			this.butDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormInsCatsSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(774, 620);
			this.ControlBox = false;
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butAddSpan);
			this.Controls.Add(this.tbMain);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butAddCat);
			this.Controls.Add(this.butDown);
			this.Controls.Add(this.butUp);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listCovCats);
			this.Name = "FormInsCatsSetup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = Lan.g(this,"Setup Insurance Categories");
			this.Load += new System.EventHandler(this.FormInsCatsSetup_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormInsCatsSetup_Load(object sender, System.EventArgs e) {
			CovCats.Selected=-1;
			FillCats();
			FillSpans();
		}

		private void FillCats(){
			listCovCats.Items.Clear();
			for(int i=0;i<CovCats.List.Length;i++){
				if(CovCats.List[i].IsHidden){
					listCovCats.Items.Add(CovCats.List[i].Description+"(hidden)");
				}
				else{
					listCovCats.Items.Add(CovCats.List[i].Description);
				}
			}
			listCovCats.SelectedIndex=CovCats.Selected;
		}

		private void FillSpans(){
			tbMain.SelectedRow=-1;
			tbMain.ResetRows(CovSpans.List.Length);
			for(int i=0;i<CovSpans.List.Length;i++){
				tbMain.Cell[0,i]=CovSpans.List[i].FromCode;
				tbMain.Cell[1,i]=CovSpans.List[i].ToCode;
				tbMain.Cell[2,i]=CovCats.GetDesc(CovSpans.List[i].CovCatNum);
			}
			tbMain.SetGridColor(Color.LightGray);
			tbMain.LayoutTables();
		}

		private void tbMain_CellClicked(object sender, CellEventArgs e){
			if(tbMain.SelectedRow!=-1)
				tbMain.ColorRow(tbMain.SelectedRow,Color.White);
			tbMain.SelectedRow=e.Row;
			tbMain.ColorRow(e.Row,Color.LightGray);
			CovSpans.Cur=CovSpans.List[tbMain.SelectedRow];
		}

		private void tbMain_CellDoubleClicked(object sender, CellEventArgs e){
			FormInsSpanEdit FormE=new FormInsSpanEdit();
			FormE.IsNew=false;
			FormE.ShowDialog();
			if(FormE.DialogResult!=DialogResult.OK){
				return;
			}
			CovSpans.Refresh();
			FillSpans();
		}

		private void butAddSpan_Click(object sender, System.EventArgs e) {
			FormInsSpanEdit FormE=new FormInsSpanEdit();
			FormE.IsNew=true;
			CovSpans.Cur=new CovSpan();
			FormE.ShowDialog();
			if(FormE.DialogResult!=DialogResult.OK){
				return;
			}
			CovSpans.Refresh();
			FillSpans();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(tbMain.SelectedRow==-1){
				MessageBox.Show(Lan.g(this,"Please select a span first"));
				return;																										 
			}
			if(MessageBox.Show(Lan.g(this,"Delete Span?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			CovSpans.DeleteCur();
			CovSpans.Refresh();
			FillSpans();
		}

		private void butUp_Click(object sender, System.EventArgs e) {
			CovCats.Selected=listCovCats.SelectedIndex;
			CovCats.MoveUp();
			CovCats.Refresh();
			FillCats();
		}

		private void butDown_Click(object sender, System.EventArgs e) {
			CovCats.Selected=listCovCats.SelectedIndex;
			CovCats.MoveDown();
			CovCats.Refresh();
			FillCats();
		}

		private void butAddCat_Click(object sender, System.EventArgs e) {
			FormInsCatEdit FormE=new FormInsCatEdit();
			FormE.IsNew=true;
			CovCats.Cur=new CovCat();
			FormE.ShowDialog();
			if(FormE.DialogResult==DialogResult.OK){
				CovCats.Refresh();
				FillCats();
			}			
		}

		private void listCovCats_DoubleClick(object sender, System.EventArgs e) {
			if(listCovCats.SelectedIndex==-1){
				return;
			}
			FormInsCatEdit FormE=new FormInsCatEdit();
			FormE.IsNew=false;
			CovCats.Cur=CovCats.List[listCovCats.SelectedIndex];
			FormE.ShowDialog();
			if(FormE.DialogResult==DialogResult.OK){
				CovCats.Refresh();
				FillCats();
				FillSpans();
			}			
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
			Close();
			//DialogResult=DialogResult.OK;
		}		

	}
}
