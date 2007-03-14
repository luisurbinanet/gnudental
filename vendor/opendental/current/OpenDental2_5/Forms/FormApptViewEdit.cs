using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormApptViewEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private OpenDental.XPButton butDelete;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox listOps;
		private System.Windows.Forms.ListBox listProv;
		private System.Windows.Forms.Label label2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textDescription;
		public bool IsNew;

		public FormApptViewEdit()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormApptViewEdit));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.butDelete = new OpenDental.XPButton();
			this.label1 = new System.Windows.Forms.Label();
			this.listOps = new System.Windows.Forms.ListBox();
			this.listProv = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(485, 410);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(485, 369);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(31, 405);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(82, 26);
			this.butDelete.TabIndex = 38;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(37, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 23);
			this.label1.TabIndex = 39;
			this.label1.Text = "View Operatories";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listOps
			// 
			this.listOps.Location = new System.Drawing.Point(38, 84);
			this.listOps.Name = "listOps";
			this.listOps.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listOps.Size = new System.Drawing.Size(120, 186);
			this.listOps.TabIndex = 40;
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(204, 84);
			this.listProv.Name = "listProv";
			this.listProv.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listProv.Size = new System.Drawing.Size(120, 186);
			this.listProv.TabIndex = 42;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(203, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(128, 23);
			this.label2.TabIndex = 41;
			this.label2.Text = "View Providers";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(35, 18);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(146, 23);
			this.label3.TabIndex = 43;
			this.label3.Text = "Description";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(184, 20);
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(250, 20);
			this.textDescription.TabIndex = 44;
			this.textDescription.Text = "";
			// 
			// FormApptViewEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(594, 464);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listOps);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormApptViewEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Appointment View Edit";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormApptViewEdit_Closing);
			this.Load += new System.EventHandler(this.FormApptViewEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormApptViewEdit_Load(object sender, System.EventArgs e) {
			textDescription.Text=ApptViews.Cur.Description;
			ApptViewItems.GetForCurView();
			for(int i=0;i<Defs.Short[(int)DefCat.Operatories].Length;i++){
				listOps.Items.Add(Defs.Short[(int)DefCat.Operatories][i].ItemName);
				if(ApptViewItems.OpIsInView(Defs.Short[(int)DefCat.Operatories][i].DefNum)){
					listOps.SetSelected(i,true);
				}
			}
			for(int i=0;i<Providers.List.Length;i++){
				listProv.Items.Add
					(Providers.List[i].Abbr+" - "+Providers.List[i].LName+", "+Providers.List[i].FName);
				if(ApptViewItems.ProvIsInView(Providers.List[i].ProvNum)){
					listProv.SetSelected(i,true);
				}
			}
			
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			//this does mess up the item orders a little, but missing numbers don't actually hurt anything.
			if(MessageBox.Show(Lan.g(this,"Delete this category?"),"",MessageBoxButtons.OKCancel)
				!=DialogResult.OK){
				return;
			}
			ApptViewItems.DeleteAllForView();
			ApptViews.DeleteCur();
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(listOps.SelectedIndices.Count==0 || listProv.SelectedIndices.Count==0){
				MessageBox.Show(Lan.g(this,"At least one operatory and one provider must be selected."));
				return;
			}
			if(textDescription.Text==""){
				MessageBox.Show(Lan.g(this,"A description must be entered."));
				return;
			}
			ApptViewItems.DeleteAllForView();//start with a clean slate
			for(int i=0;i<Defs.Short[(int)DefCat.Operatories].Length;i++){
				if(listOps.SelectedIndices.Contains(i)){
					ApptViewItems.Cur=new ApptViewItem();
					ApptViewItems.Cur.ApptViewNum=ApptViews.Cur.ApptViewNum;
					ApptViewItems.Cur.OpNum=Defs.Short[(int)DefCat.Operatories][i].DefNum;
					ApptViewItems.InsertCur();
				}
			}
			for(int i=0;i<Providers.List.Length;i++){
				if(listProv.SelectedIndices.Contains(i)){
					ApptViewItems.Cur=new ApptViewItem();
					ApptViewItems.Cur.ApptViewNum=ApptViews.Cur.ApptViewNum;
					ApptViewItems.Cur.ProvNum=Providers.List[i].ProvNum;
					ApptViewItems.InsertCur();
				}
			}
			ApptViews.Cur.Description=textDescription.Text;
			ApptViews.UpdateCur();//same whether isnew or not
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;;
		}

		private void FormApptViewEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNew){
				ApptViewItems.DeleteAllForView();
				ApptViews.DeleteCur();
			}
		}


	}
}





















