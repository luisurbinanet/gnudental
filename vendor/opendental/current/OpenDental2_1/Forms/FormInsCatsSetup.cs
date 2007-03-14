using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormInsCatsSetup : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox listCovCats;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button butOK;
		private OpenDental.XPButton butDelete;
		private OpenDental.XPButton butAddSpan;
		private OpenDental.XPButton butUp;
		private OpenDental.XPButton butAddCat;
		private OpenDental.XPButton butDown;
		private OpenDental.TableCovSpans tbMain;

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
			this.butOK = new System.Windows.Forms.Button();
			this.tbMain = new OpenDental.TableCovSpans();
			this.butDelete = new OpenDental.XPButton();
			this.butAddSpan = new OpenDental.XPButton();
			this.butUp = new OpenDental.XPButton();
			this.butAddCat = new OpenDental.XPButton();
			this.butDown = new OpenDental.XPButton();
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
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(681, 578);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 6;
			this.butOK.Text = "Close";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// tbMain
			// 
			this.tbMain.BackColor = System.Drawing.SystemColors.Window;
			this.tbMain.Location = new System.Drawing.Point(306, 14);
			this.tbMain.Name = "tbMain";
			this.tbMain.SelectedIndices = new int[0];
			this.tbMain.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbMain.Size = new System.Drawing.Size(329, 586);
			this.tbMain.TabIndex = 8;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(681, 443);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75, 26);
			this.butDelete.TabIndex = 10;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butAddSpan
			// 
			this.butAddSpan.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddSpan.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butAddSpan.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butAddSpan.Image = ((System.Drawing.Image)(resources.GetObject("butAddSpan.Image")));
			this.butAddSpan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddSpan.Location = new System.Drawing.Point(681, 404);
			this.butAddSpan.Name = "butAddSpan";
			this.butAddSpan.Size = new System.Drawing.Size(75, 26);
			this.butAddSpan.TabIndex = 9;
			this.butAddSpan.Text = "Add";
			this.butAddSpan.Click += new System.EventHandler(this.butAddSpan_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butUp.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butUp.Image = ((System.Drawing.Image)(resources.GetObject("butUp.Image")));
			this.butUp.Location = new System.Drawing.Point(71, 321);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(54, 26);
			this.butUp.TabIndex = 12;
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// butAddCat
			// 
			this.butAddCat.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddCat.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butAddCat.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butAddCat.Image = ((System.Drawing.Image)(resources.GetObject("butAddCat.Image")));
			this.butAddCat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddCat.Location = new System.Drawing.Point(71, 356);
			this.butAddCat.Name = "butAddCat";
			this.butAddCat.Size = new System.Drawing.Size(75, 26);
			this.butAddCat.TabIndex = 11;
			this.butAddCat.Text = "Add";
			this.butAddCat.Click += new System.EventHandler(this.butAddCat_Click);
			// 
			// butDown
			// 
			this.butDown.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDown.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDown.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDown.Image = ((System.Drawing.Image)(resources.GetObject("butDown.Image")));
			this.butDown.Location = new System.Drawing.Point(139, 321);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(54, 26);
			this.butDown.TabIndex = 13;
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// FormInsCatsSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(774, 620);
			this.Controls.Add(this.butDown);
			this.Controls.Add(this.butUp);
			this.Controls.Add(this.butAddCat);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butAddSpan);
			this.Controls.Add(this.tbMain);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listCovCats);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormInsCatsSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Setup Insurance Categories";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormInsCatsSetup_Closing);
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
			DialogResult=DialogResult.OK;
		}

		private void FormInsCatsSetup_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
		}		

	}
}
