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
		private System.Windows.Forms.Label label1;
		private OpenDental.XPButton butDown;
		private OpenDental.XPButton butUp;
		private OpenDental.XPButton butAdd;
		private System.Windows.Forms.ListBox listViews;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioTen;
		private System.Windows.Forms.RadioButton radioFifteen;
		private System.Windows.Forms.CheckBox checkTwoRows;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Label label2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormApptViews()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label1, //*Ann
				this.groupBox1, //*Ann
				this.radioTen, //*Ann
				this.radioFifteen, //*Ann
				this.label2, //*Ann
			});
			Lan.C("All", new System.Windows.Forms.Control[]
			{
				this.butAdd,
				this.butDown,
				this.butUp,
				butOK,
				butCancel
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
			this.butCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.listViews = new System.Windows.Forms.ListBox();
			this.butDown = new OpenDental.XPButton();
			this.butUp = new OpenDental.XPButton();
			this.butAdd = new OpenDental.XPButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioFifteen = new System.Windows.Forms.RadioButton();
			this.radioTen = new System.Windows.Forms.RadioButton();
			this.checkTwoRows = new System.Windows.Forms.CheckBox();
			this.butOK = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(435, 433);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
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
			this.listViews.Size = new System.Drawing.Size(183, 329);
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
			this.butDown.Location = new System.Drawing.Point(151, 437);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(89, 26);
			this.butDown.TabIndex = 38;
			this.butDown.Text = "&Down";
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butUp.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butUp.Image = ((System.Drawing.Image)(resources.GetObject("butUp.Image")));
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(151, 399);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(89, 26);
			this.butUp.TabIndex = 39;
			this.butUp.Text = "&Up";
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
			this.butAdd.Size = new System.Drawing.Size(89, 26);
			this.butAdd.TabIndex = 36;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioFifteen);
			this.groupBox1.Controls.Add(this.radioTen);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(279, 54);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(169, 70);
			this.groupBox1.TabIndex = 40;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Time Increments";
			// 
			// radioFifteen
			// 
			this.radioFifteen.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioFifteen.Location = new System.Drawing.Point(23, 42);
			this.radioFifteen.Name = "radioFifteen";
			this.radioFifteen.Size = new System.Drawing.Size(100, 18);
			this.radioFifteen.TabIndex = 1;
			this.radioFifteen.Text = "15 Min";
			// 
			// radioTen
			// 
			this.radioTen.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioTen.Location = new System.Drawing.Point(23, 21);
			this.radioTen.Name = "radioTen";
			this.radioTen.Size = new System.Drawing.Size(100, 18);
			this.radioTen.TabIndex = 0;
			this.radioTen.Text = "10 Min";
			// 
			// checkTwoRows
			// 
			this.checkTwoRows.Location = new System.Drawing.Point(0, 0);
			this.checkTwoRows.Name = "checkTwoRows";
			this.checkTwoRows.TabIndex = 0;
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(435, 394);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 41;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(298, 426);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(127, 31);
			this.label2.TabIndex = 42;
			this.label2.Text = "Changes to the Views will always be saved";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// FormApptViews
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(546, 485);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butDown);
			this.Controls.Add(this.butUp);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.listViews);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormApptViews";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Appointment Views";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormApptViews_Closing);
			this.Load += new System.EventHandler(this.FormApptViews_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormApptViews_Load(object sender, System.EventArgs e) {
			FillViewList();
			if(Prefs.GetInt("AppointmentTimeIncrement")==10){
				radioTen.Checked=true;
			}
			else{
				radioFifteen.Checked=true;
			}
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

		private void butOK_Click(object sender, System.EventArgs e) {
			if(radioTen.Checked){
				Prefs.UpdateInt("AppointmentTimeIncrement",10);
			}
			else{
				Prefs.UpdateInt("AppointmentTimeIncrement",15);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			//all this cancels is the 10 vs 15 selection
			DialogResult=DialogResult.Cancel;
		}

		private void FormApptViews_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			//always update
			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
		}


	

		


	}
}





















