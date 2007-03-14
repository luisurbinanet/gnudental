using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormProcButtons : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.ListBox listButtons;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butDelete;
		private OpenDental.UI.Button butDown;
		private OpenDental.UI.Button butUp;
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormProcButtons(){
			InitializeComponent();
			Lan.F(this);
		}

		///<summary></summary>
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
			this.butClose = new OpenDental.UI.Button();
			this.listButtons = new System.Windows.Forms.ListBox();
			this.butAdd = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.butDown = new OpenDental.UI.Button();
			this.butUp = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(332, 396);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 26);
			this.butClose.TabIndex = 8;
			this.butClose.Text = "&Close";
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
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(38, 358);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(82, 26);
			this.butAdd.TabIndex = 32;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(38, 396);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(82, 26);
			this.butDelete.TabIndex = 33;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butDown
			// 
			this.butDown.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDown.Autosize = true;
			this.butDown.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDown.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDown.Image = ((System.Drawing.Image)(resources.GetObject("butDown.Image")));
			this.butDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDown.Location = new System.Drawing.Point(190, 396);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(82, 26);
			this.butDown.TabIndex = 34;
			this.butDown.Text = "&Down";
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butUp.Autosize = true;
			this.butUp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUp.Image = ((System.Drawing.Image)(resources.GetObject("butUp.Image")));
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(190, 358);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(82, 26);
			this.butUp.TabIndex = 35;
			this.butUp.Text = "&Up";
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// FormProcButtons
			// 
			this.AcceptButton = this.butClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(426, 446);
			this.Controls.Add(this.butDown);
			this.Controls.Add(this.butUp);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.listButtons);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProcButtons";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Setup Procedure Buttons";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormProcButtons_Closing);
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
			FillList();
		}

		private void listButtons_DoubleClick(object sender, System.EventArgs e) {
			ProcButtons.Cur=ProcButtons.List[listButtons.SelectedIndex];
      FormProcButtonEdit FormPBE=new FormProcButtonEdit();
      FormPBE.ShowDialog();
      FillList();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormProcButtons_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
		}

		

	}
}
