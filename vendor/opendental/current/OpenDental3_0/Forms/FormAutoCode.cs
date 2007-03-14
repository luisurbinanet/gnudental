using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	///<summary></summary>
	public class FormAutoCode : System.Windows.Forms.Form{
		private System.Windows.Forms.ListBox listAutoCodes;
		private System.Windows.Forms.Button butClose;
		private OpenDental.XPButton butAdd;
		private OpenDental.XPButton butDelete;
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormAutoCode(){
			InitializeComponent();
			Lan.C("All", new System.Windows.Forms.Control[] {
				butAdd,
				butClose,
				butDelete,
			}); 
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormAutoCode));
			this.listAutoCodes = new System.Windows.Forms.ListBox();
			this.butClose = new System.Windows.Forms.Button();
			this.butAdd = new OpenDental.XPButton();
			this.butDelete = new OpenDental.XPButton();
			this.SuspendLayout();
			// 
			// listAutoCodes
			// 
			this.listAutoCodes.Location = new System.Drawing.Point(28, 26);
			this.listAutoCodes.Name = "listAutoCodes";
			this.listAutoCodes.Size = new System.Drawing.Size(178, 316);
			this.listAutoCodes.TabIndex = 0;
			this.listAutoCodes.DoubleClick += new System.EventHandler(this.listAutoCodes_DoubleClick);
			// 
			// butClose
			// 
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClose.Location = new System.Drawing.Point(230, 390);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(80, 26);
			this.butClose.TabIndex = 3;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(26, 354);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(90, 26);
			this.butAdd.TabIndex = 5;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(118, 354);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(90, 26);
			this.butDelete.TabIndex = 6;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormAutoCode
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(338, 430);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.listAutoCodes);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAutoCode";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Auto Codes";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormAutoCode_Closing);
			this.Load += new System.EventHandler(this.FormAutoCode_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormAutoCode_Load(object sender, System.EventArgs e) {
      FillList();		
		}

    private void FillList(){
      AutoCodes.Refresh();
      listAutoCodes.Items.Clear();
      for(int i=0;i<AutoCodes.List.Length;i++){
        if(AutoCodes.List[i].IsHidden){
          listAutoCodes.Items.Add(AutoCodes.List[i].Description+"(hidden)");  
        }
        else{
          listAutoCodes.Items.Add(AutoCodes.List[i].Description); 
        } 
      }  
    }

		private void butAdd_Click(object sender, System.EventArgs e) {
		  FormAutoCodeEdit FormACE=new FormAutoCodeEdit();
      FormACE.IsNew=true;
      FormACE.ShowDialog();
      FillList();
		}

		private void listAutoCodes_DoubleClick(object sender, System.EventArgs e) {
		  AutoCodes.Cur=AutoCodes.List[listAutoCodes.SelectedIndex];
		  FormAutoCodeEdit FormACE=new FormAutoCodeEdit();
      FormACE.ShowDialog();
      FillList();      
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
		  if(listAutoCodes.SelectedIndex < 0){
        MessageBox.Show(Lan.g(this,"You must first select a row"));
				return;
      }
      AutoCodes.Cur=AutoCodes.List[listAutoCodes.SelectedIndex];
      AutoCodes.DeleteCur();
      FillList(); 		
		}

		private void FormAutoCode_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
      DialogResult=DialogResult.OK;
		}
	}
}








