using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormApptViews : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butClose;
		private System.Windows.Forms.Label label1;
		private OpenDental.XPButton butDown;
		private OpenDental.XPButton butUp;
		private OpenDental.XPButton butAdd;
		private System.Windows.Forms.ListBox listViews;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormApptViews()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.C("All", new System.Windows.Forms.Control[] {
				butClose
			});
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormApptViews));
			this.butClose = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.listViews = new System.Windows.Forms.ListBox();
			this.butDown = new OpenDental.XPButton();
			this.butUp = new OpenDental.XPButton();
			this.butAdd = new OpenDental.XPButton();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClose.Location = new System.Drawing.Point(334, 441);
			this.butClose.Name = "butClose";
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(57, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(158, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Views";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listViews
			// 
			this.listViews.Location = new System.Drawing.Point(56, 60);
			this.listViews.Name = "listViews";
			this.listViews.Size = new System.Drawing.Size(179, 329);
			this.listViews.TabIndex = 2;
			this.listViews.DoubleClick += new System.EventHandler(this.listViews_DoubleClick);
			// 
			// butDown
			// 
			this.butDown.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDown.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDown.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDown.Image = ((System.Drawing.Image)(resources.GetObject("butDown.Image")));
			this.butDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDown.Location = new System.Drawing.Point(154, 437);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(82, 26);
			this.butDown.TabIndex = 38;
			this.butDown.Text = "Down";
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butUp.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butUp.Image = ((System.Drawing.Image)(resources.GetObject("butUp.Image")));
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(154, 399);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(82, 26);
			this.butUp.TabIndex = 39;
			this.butUp.Text = "Up";
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(55, 399);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(82, 26);
			this.butAdd.TabIndex = 36;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// FormApptViews
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(435, 491);
			this.Controls.Add(this.butDown);
			this.Controls.Add(this.butUp);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.listViews);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormApptViews";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Appointment Views";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormApptViews_Closing);
			this.Load += new System.EventHandler(this.FormApptViews_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormApptViews_Load(object sender, System.EventArgs e) {
			FillViewList();
		}

		private void FillViewList(){
			ApptViews.Refresh();
			ApptViewItems.Refresh();
			listViews.Items.Clear();
			string F;
			for(int i=0;i<ApptViews.List.Length;i++){
				if(i<12)
					F="F"+(i+1).ToString()+"-";
				else
					F="";
				listViews.Items.Add(F+ApptViews.List[i].Description);
			}
		}
		
		private void butAdd_Click(object sender, System.EventArgs e) {
			ApptViews.Cur=new ApptView();
			ApptViews.Cur.ItemOrder=ApptViews.List.Length;
			ApptViews.InsertCur();//this also gets the primary key
			FormApptViewEdit FormAVE=new FormApptViewEdit();
			FormAVE.IsNew=true;
			FormAVE.ShowDialog();
			FillViewList();
			listViews.SelectedIndex=listViews.Items.Count-1;//this works even if no items
		}

		private void listViews_DoubleClick(object sender, System.EventArgs e) {
			int selected=listViews.SelectedIndex;
			ApptViews.Cur=ApptViews.List[listViews.SelectedIndex];
			FormApptViewEdit FormAVE=new FormApptViewEdit();
			FormAVE.ShowDialog();
			FillViewList();
			if(selected<listViews.Items.Count){
				listViews.SelectedIndex=selected;
			}
		}

		private void butUp_Click(object sender, System.EventArgs e) {
			if(listViews.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select a category first."));
				return;
			}
			if(listViews.SelectedIndex==0){
				return;//can't go up any more
			}
			int selected=listViews.SelectedIndex;
			//it will flip flop with the one above it
			ApptViews.Cur=ApptViews.List[listViews.SelectedIndex];
			ApptViews.Cur.ItemOrder=ApptViews.Cur.ItemOrder-1;
			ApptViews.UpdateCur();
			//now the other
			ApptViews.Cur=ApptViews.List[listViews.SelectedIndex-1];
			ApptViews.Cur.ItemOrder=ApptViews.Cur.ItemOrder+1;
			ApptViews.UpdateCur();
			FillViewList();
			listViews.SelectedIndex=selected-1;
		}

		private void butDown_Click(object sender, System.EventArgs e) {
			if(listViews.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select a category first."));
				return;
			}
			if(listViews.SelectedIndex==ApptViews.List.Length-1){
				return;//can't go down any more
			}
			int selected=listViews.SelectedIndex;
			//it will flip flop with the one below it
			ApptViews.Cur=ApptViews.List[listViews.SelectedIndex];
			ApptViews.Cur.ItemOrder=ApptViews.Cur.ItemOrder+1;
			ApptViews.UpdateCur();
			//now the other
			ApptViews.Cur=ApptViews.List[listViews.SelectedIndex+1];
			ApptViews.Cur.ItemOrder=ApptViews.Cur.ItemOrder-1;
			ApptViews.UpdateCur();
			FillViewList();
			listViews.SelectedIndex=selected+1;
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormApptViews_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
		}

	

		


	}
}





















