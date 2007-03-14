using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.Reporting;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormLetterSetup : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private OpenDental.XPButton butDelete;
		private OpenDental.XPButton butAdd;
		private System.Windows.Forms.ListBox listLetters;
		private OpenDental.XPButton butDown;
		private OpenDental.XPButton butUp;
		private System.Windows.Forms.Button butClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormLetterSetup()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.C("All", new System.Windows.Forms.Control[] {
				butClose,
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormLetterSetup));
			this.butClose = new System.Windows.Forms.Button();
			this.listLetters = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.XPButton();
			this.butAdd = new OpenDental.XPButton();
			this.butDown = new OpenDental.XPButton();
			this.butUp = new OpenDental.XPButton();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClose.Location = new System.Drawing.Point(287, 346);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(79, 26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// listLetters
			// 
			this.listLetters.Location = new System.Drawing.Point(46, 53);
			this.listLetters.Name = "listLetters";
			this.listLetters.Size = new System.Drawing.Size(164, 277);
			this.listLetters.TabIndex = 2;
			this.listLetters.DoubleClick += new System.EventHandler(this.listLetters_DoubleClick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(45, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(124, 14);
			this.label1.TabIndex = 3;
			this.label1.Text = "Letters";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(287, 251);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(79, 26);
			this.butDelete.TabIndex = 8;
			this.butDelete.Text = "&Delete";
			this.butDelete.Visible = false;
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(287, 211);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(79, 26);
			this.butAdd.TabIndex = 7;
			this.butAdd.Text = "&Add";
			this.butAdd.Visible = false;
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butDown
			// 
			this.butDown.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDown.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDown.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDown.Image = ((System.Drawing.Image)(resources.GetObject("butDown.Image")));
			this.butDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDown.Location = new System.Drawing.Point(133, 346);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(79, 26);
			this.butDown.TabIndex = 11;
			this.butDown.Text = "D&own";
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0, 1);
			this.butUp.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butUp.Image = ((System.Drawing.Image)(resources.GetObject("butUp.Image")));
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(47, 346);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(79, 26);
			this.butUp.TabIndex = 10;
			this.butUp.Text = "&Up";
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// FormLetterSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(394, 397);
			this.Controls.Add(this.butDown);
			this.Controls.Add(this.butUp);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listLetters);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormLetterSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Letter Setup";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormLetterSetup_Closing);
			this.Load += new System.EventHandler(this.FormLetterSetup_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormLetterSetup_Load(object sender, System.EventArgs e) {
			FillList();
		}

		private void FillList(){
			Reports.Refresh();
			listLetters.Items.Clear();
			for(int i=0;i<Reports.List.Length;i++){
				listLetters.Items.Add(Reports.List[i].ReportName);
			}
		}

		private void listLetters_DoubleClick(object sender, System.EventArgs e) {
			if(listLetters.SelectedIndex==-1){
				return;
			}
			Reports.CurStruct=Reports.List[listLetters.SelectedIndex];
			FormLetterEdit FormLE=new FormLetterEdit();
			FormLE.ShowDialog();
			FillList();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			Reports.Cur=new Report();
			Reports.Cur.LetterOrder=Reports.List.Length+1;
			Reports.Cur.DataFields.Add("LName");
			Reports.Cur.DataFields.Add("FName");
			Reports.Cur.DataFields.Add("MiddleI");
			Reports.Cur.DataFields.Add("Address");
			Reports.Cur.DataFields.Add("Address2");
			Reports.Cur.DataFields.Add("City");
			Reports.Cur.DataFields.Add("State");
			Reports.Cur.DataFields.Add("Zip");
			


			FormLetterEdit FormLE=new FormLetterEdit();
			FormLE.ShowDialog();
			FillList();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			/*
			if(listLetters.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete letter?"),"",MessageBoxButtons.OKCancel)
				!=DialogResult.OK){
				return;
			}
			Reports.CurStruct=Reports.List[listLetters.SelectedIndex];
			Reports.DeleteCur();*/
		}

		private void butUp_Click(object sender, System.EventArgs e) {
			if(listLetters.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			if(listLetters.SelectedIndex==0){//can't go up anymore
				return;
			}
			//change the order number of the selected row
			Reports.CurStruct=Reports.List[listLetters.SelectedIndex];
			Reports.CurStruct.LetterOrder=Reports.CurStruct.LetterOrder-1;
			Reports.UpdateCurStruct();
			//then change the order number of the row it is trading places with.
			Reports.CurStruct=Reports.List[listLetters.SelectedIndex-1];
			Reports.CurStruct.LetterOrder=Reports.CurStruct.LetterOrder+1;
			Reports.UpdateCurStruct();
			FillList();
		}

		private void butDown_Click(object sender, System.EventArgs e) {
			if(listLetters.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			if(listLetters.SelectedIndex==listLetters.Items.Count-1){//can't go down anymore
				return;
			}
			//change the order number of the selected row
			Reports.CurStruct=Reports.List[listLetters.SelectedIndex];
			Reports.CurStruct.LetterOrder=Reports.CurStruct.LetterOrder+1;
			Reports.UpdateCurStruct();
			//then change the order number of the row it is trading places with.
			Reports.CurStruct=Reports.List[listLetters.SelectedIndex+1];
			Reports.CurStruct.LetterOrder=Reports.CurStruct.LetterOrder-1;
			Reports.UpdateCurStruct();
			FillList();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormLetterSetup_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
		}

		

		

		


	}
}





















