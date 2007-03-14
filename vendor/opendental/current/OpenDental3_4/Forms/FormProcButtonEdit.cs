using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormProcButtonEdit : System.Windows.Forms.Form{
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ListBox listAutoCodes;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textDescript;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label2;
		///<summary></summary>
    public bool IsNew;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox listADA;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.Label label4;

		///<summary></summary>
		public FormProcButtonEdit(){
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormProcButtonEdit));
			this.listAutoCodes = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textDescript = new System.Windows.Forms.TextBox();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.listADA = new System.Windows.Forms.ListBox();
			this.label4 = new System.Windows.Forms.Label();
			this.butAdd = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// listAutoCodes
			// 
			this.listAutoCodes.Location = new System.Drawing.Point(258, 128);
			this.listAutoCodes.Name = "listAutoCodes";
			this.listAutoCodes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listAutoCodes.Size = new System.Drawing.Size(158, 381);
			this.listAutoCodes.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(2, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(115, 12);
			this.label1.TabIndex = 25;
			this.label1.Text = "Description";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDescript
			// 
			this.textDescript.Location = new System.Drawing.Point(119, 30);
			this.textDescript.Name = "textDescript";
			this.textDescript.Size = new System.Drawing.Size(316, 20);
			this.textDescript.TabIndex = 24;
			this.textDescript.Text = "";
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(538, 536);
			this.butCancel.Name = "butCancel";
			this.butCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 27;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(538, 498);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 26;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 110);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(203, 12);
			this.label2.TabIndex = 29;
			this.label2.Text = "Add ADA Codes";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(238, 108);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(200, 14);
			this.label3.TabIndex = 31;
			this.label3.Text = "Highlight Auto Codes";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// listADA
			// 
			this.listADA.Location = new System.Drawing.Point(36, 128);
			this.listADA.Name = "listADA";
			this.listADA.Size = new System.Drawing.Size(160, 381);
			this.listADA.TabIndex = 32;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(36, 74);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(462, 23);
			this.label4.TabIndex = 35;
			this.label4.Text = "Add any number of ADA codes and Auto Codes";
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
			this.butAdd.Location = new System.Drawing.Point(35, 518);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75, 26);
			this.butAdd.TabIndex = 36;
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
			this.butDelete.Location = new System.Drawing.Point(122, 518);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75, 26);
			this.butDelete.TabIndex = 37;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormProcButtonEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(638, 590);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.listADA);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textDescript);
			this.Controls.Add(this.listAutoCodes);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProcButtonEdit";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Procedure Button";
			this.Load += new System.EventHandler(this.FormChartProcedureEntryEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormChartProcedureEntryEdit_Load(object sender, System.EventArgs e) {
      AutoCodes.Refresh(); 
      ProcButtonItems.Refresh();     
      if(IsNew){
        this.Text=Lan.g(this,"Add Procedure Button");
        ProcButtons.Cur=new ProcButton();
      }
      else{
        this.Text=Lan.g(this,"Edit Procedure Button");
        textDescript.Text=ProcButtons.Cur.Description;
        //checkQuad.Checked=ProcButtons.Cur.IsFourQuad;
      }     
		  FillLists();
		}

		private void FillLists(){
			ProcButtonItems.GetListsForButton(ProcButtons.Cur.ProcButtonNum); 
			FillADACodeList();
			FillAutoCodeList();
		}

    private void FillADACodeList(){      
      listADA.Items.Clear();
      for(int i=0;i<ProcButtonItems.ALadaCodes.Count;i++){
        listADA.Items.Add(ProcButtonItems.ALadaCodes[i]);
      }
    }

    private void FillAutoCodeList(){      
      listAutoCodes.Items.Clear();
      for(int i=0;i<AutoCodes.ListShort.Length;i++){
        listAutoCodes.Items.Add(AutoCodes.ListShort[i].Description);
				if(ProcButtonItems.ALautoCodes.Contains(AutoCodes.ListShort[i].AutoCodeNum)){
				//for(int j=0;j<ProcButtonItems.autoCodeList.Length;j++){
					//if(AutoCodes.List[i].AutoCodeNum==(int)ProcButtonItems.autoCodeList[j]){
					listAutoCodes.SetSelected(i,true);        
					//}         
        }
      }   
    }

		private void butAdd_Click(object sender, System.EventArgs e) {
		  FormProcedures FormP=new FormProcedures();
      FormP.Mode=FormProcMode.Select;
      FormP.ShowDialog();
      if(FormP.DialogResult!=DialogResult.Cancel){
        listADA.Items.Add(FormP.SelectedADA);  
      } 
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
      if(listADA.SelectedIndex < 0){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
      }
      listADA.Items.RemoveAt(listADA.SelectedIndex);        
		}

	 	private void butOK_Click(object sender, System.EventArgs e) {
      if(textDescript.Text==""){
				MessageBox.Show(Lan.g(this,"You must type in a description."));
				return; 
      }
			if(listADA.Items.Count==0  && listAutoCodes.SelectedIndices.Count==0){
        MessageBox.Show(Lan.g(this,"You must pick at least one Auto Code or ADA Code."));
        return;
      }
      ProcButtons.Cur.Description=textDescript.Text;
      //ProcButtons.Cur.IsFourQuad=checkQuad.Checked;    
      if(IsNew){
        ProcButtons.Cur.ItemOrder=ProcButtons.List.Length;        
        ProcButtons.InsertCur();
      }
      else{
        ProcButtons.UpdateCur();
      }
      ProcButtonItems.DeleteAllForCur();
      for(int i=0;i<listADA.Items.Count;i++){
        ProcButtonItems.Cur=new ProcButtonItem();
        ProcButtonItems.Cur.ProcButtonNum=ProcButtons.Cur.ProcButtonNum;
        ProcButtonItems.Cur.ADACode=listADA.Items[i].ToString();    
        ProcButtonItems.InsertCur();
      }
      for(int i=0;i<listAutoCodes.SelectedIndices.Count;i++){
        ProcButtonItems.Cur=new ProcButtonItem();
        ProcButtonItems.Cur.ProcButtonNum=ProcButtons.Cur.ProcButtonNum;
        ProcButtonItems.Cur.AutoCodeNum=AutoCodes.ListShort[listAutoCodes.SelectedIndices[i]].AutoCodeNum;
        ProcButtonItems.InsertCur();
      }
      DialogResult=DialogResult.OK;    
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
		
		} 

		
   
	}
}
