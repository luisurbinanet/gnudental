using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormProcButtons : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butClose;
		private System.Windows.Forms.Button butDown;
		private System.Windows.Forms.Button butAdd;
		private System.Windows.Forms.Button butUp;
		private System.Windows.Forms.ListBox listButtons;
		private System.Windows.Forms.Button butDelete;
		private System.ComponentModel.Container components = null;

		public FormProcButtons(){
			InitializeComponent();
			Lan.C("All", new System.Windows.Forms.Control[] {
				butDown,
				butUp,
				butClose,
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormProcButtons));
			this.butClose = new System.Windows.Forms.Button();
			this.listButtons = new System.Windows.Forms.ListBox();
			this.butDown = new System.Windows.Forms.Button();
			this.butAdd = new System.Windows.Forms.Button();
			this.butUp = new System.Windows.Forms.Button();
			this.butDelete = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.Location = new System.Drawing.Point(332, 396);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 26);
			this.butClose.TabIndex = 8;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// listButtons
			// 
			this.listButtons.Location = new System.Drawing.Point(40, 22);
			this.listButtons.Name = "listButtons";
			this.listButtons.Size = new System.Drawing.Size(234, 316);
			this.listButtons.TabIndex = 9;
			this.listButtons.DoubleClick += new System.EventHandler(this.listButtons_DoubleClick);
			// 
			// butDown
			// 
			this.butDown.Image = ((System.Drawing.Image)(resources.GetObject("butDown.Image")));
			this.butDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDown.Location = new System.Drawing.Point(190, 396);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(82, 26);
			this.butDown.TabIndex = 7;
			this.butDown.Text = "        Down";
			this.butDown.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// butAdd
			// 
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(42, 358);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(86, 26);
			this.butAdd.TabIndex = 5;
			this.butAdd.Text = "           Add";
			this.butAdd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butUp
			// 
			this.butUp.Image = ((System.Drawing.Image)(resources.GetObject("butUp.Image")));
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(190, 358);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(82, 26);
			this.butUp.TabIndex = 6;
			this.butUp.Text = "         Up";
			this.butUp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// butDelete
			// 
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(42, 394);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(86, 26);
			this.butDelete.TabIndex = 10;
			this.butDelete.Text = "          Delete";
			this.butDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormProcButtons
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(426, 446);
			this.ControlBox = false;
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.listButtons);
			this.Controls.Add(this.butDown);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butUp);
			this.Name = "FormProcButtons";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Setup Procedure Buttons";
			this.Load += new System.EventHandler(this.FormChartProcedureEntry_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormChartProcedureEntry_Load(object sender, System.EventArgs e) {     
		  FillList();
		}

    private void FillList(){
      ProcButtons.Refresh();
      listButtons.Items.Clear();
      for(int i=0;i<ProcButtons.List.Length;i++){
        listButtons.Items.Add(ProcButtons.List[i].Description);
      } 
    }

		private void butDown_Click(object sender, System.EventArgs e) {
      int selected=0;
		  if(listButtons.SelectedIndex < 0){
        return;
      }
      else if(listButtons.SelectedIndex==listButtons.Items.Count-1){
        return; 
      }
      else{
        ProcButtons.Cur=ProcButtons.List[listButtons.SelectedIndex];
        ProcButtons.Cur.ItemOrder++;
        ProcButtons.UpdateCur();
        selected=ProcButtons.Cur.ItemOrder;
        ProcButtons.Cur=ProcButtons.List[listButtons.SelectedIndex+1];
        ProcButtons.Cur.ItemOrder--;
        ProcButtons.UpdateCur();
      }		
      FillList();
      listButtons.SelectedIndex=selected;	 
		}

		private void butUp_Click(object sender, System.EventArgs e) {
      int selected=0;
		  if(listButtons.SelectedIndex < 0){
        return;
      }
      else if(listButtons.SelectedIndex==0){
        return; 
      }
      else{
        ProcButtons.Cur=ProcButtons.List[listButtons.SelectedIndex];
        ProcButtons.Cur.ItemOrder--;
        ProcButtons.UpdateCur();
        selected=ProcButtons.Cur.ItemOrder;
        ProcButtons.Cur=ProcButtons.List[listButtons.SelectedIndex-1];
        ProcButtons.Cur.ItemOrder++;
        ProcButtons.UpdateCur();
      }	
      FillList();	
      listButtons.SelectedIndex=selected;
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
      FormProcButtonEdit FormPBE=new FormProcButtonEdit();
      FormPBE.IsNew=true;
      FormPBE.ShowDialog();
      FillList();	
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(listButtons.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			ProcButtons.Cur=ProcButtons.List[listButtons.SelectedIndex];
			ProcButtons.DeleteCur();
		}

		private void listButtons_DoubleClick(object sender, System.EventArgs e) {
			ProcButtons.Cur=ProcButtons.List[listButtons.SelectedIndex];
      FormProcButtonEdit FormPBE=new FormProcButtonEdit();
      FormPBE.ShowDialog();
      FillList();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
      DialogResult=DialogResult.OK;
		}

		

	}
}
