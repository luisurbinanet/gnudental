using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormBlockEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox listDay;
		private System.Windows.Forms.TextBox textStart;
		private System.Windows.Forms.TextBox textStop;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew;
		private OpenDental.UI.Button butDelete;
		private SchedDefaults SchedDefaults=new SchedDefaults();

		///<summary></summary>
		public FormBlockEdit(){
			InitializeComponent();
			Lan.F(this);
			listDay.Items.Clear();
			listDay.Items.AddRange(CultureInfo.CurrentCulture.DateTimeFormat.DayNames);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormBlockEdit));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.listDay = new System.Windows.Forms.ListBox();
			this.textStart = new System.Windows.Forms.TextBox();
			this.textStop = new System.Windows.Forms.TextBox();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(98, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Start Time";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "Stop Time";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(22, 68);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(84, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "Day";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listDay
			// 
			this.listDay.Location = new System.Drawing.Point(108, 68);
			this.listDay.Name = "listDay";
			this.listDay.Size = new System.Drawing.Size(102, 95);
			this.listDay.TabIndex = 2;
			// 
			// textStart
			// 
			this.textStart.Location = new System.Drawing.Point(108, 8);
			this.textStart.Name = "textStart";
			this.textStart.TabIndex = 0;
			this.textStart.Text = "";
			// 
			// textStop
			// 
			this.textStop.Location = new System.Drawing.Point(108, 34);
			this.textStop.Name = "textStop";
			this.textStop.TabIndex = 1;
			this.textStop.Text = "";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(136, 178);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(136, 212);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 5;
			this.butCancel.Text = "&Cancel";
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(10, 212);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(86, 26);
			this.butDelete.TabIndex = 7;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormBlockEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(224, 244);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.textStop);
			this.Controls.Add(this.textStart);
			this.Controls.Add(this.listDay);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormBlockEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Block";
			this.Load += new System.EventHandler(this.FormBlockEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormBlockEdit_Load(object sender, System.EventArgs e) {
			if(IsNew){
				SchedDefaults.Cur=new SchedDefault();
			}
			textStart.Text=SchedDefaults.Cur.StartTime.ToShortTimeString();
			textStop.Text=SchedDefaults.Cur.StopTime.ToShortTimeString();
			listDay.SelectedIndex=SchedDefaults.Cur.DayOfWeek;
		}
		
		private void butDelete_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"Delete?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			if(IsNew){
				DialogResult=DialogResult.Cancel;
			}
			else{
				SchedDefaults.DeleteCur();
				DialogResult=DialogResult.OK;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//MessageBox.Show
			try{
				SchedDefaults.Cur.StartTime=DateTime.Parse(textStart.Text);
				SchedDefaults.Cur.StopTime=DateTime.Parse(textStop.Text);
				SchedDefaults.Cur.DayOfWeek=listDay.SelectedIndex;
			}
			catch{
				MessageBox.Show(Lan.g(this,"Incorrect time format"));
				return;
			}
			if(SchedDefaults.Cur.StartTime.TimeOfDay.CompareTo(SchedDefaults.Cur.StopTime.TimeOfDay)>0){
				MessageBox.Show(Lan.g(this,"Stop time must be later than start time."));
				return;
			}
			for(int i=0;i<SchedDefaults.List.Length;i++){
				if(SchedDefaults.Cur.SchedDefaultNum!=SchedDefaults.List[i].SchedDefaultNum
					&& SchedDefaults.Cur.DayOfWeek==SchedDefaults.List[i].DayOfWeek
					&& SchedDefaults.Cur.StartTime.TimeOfDay.CompareTo(SchedDefaults.List[i].StartTime.TimeOfDay)>=0
					&& SchedDefaults.Cur.StartTime.TimeOfDay.CompareTo(SchedDefaults.List[i].StopTime.TimeOfDay)<0
					){
					MessageBox.Show(Lan.g(this,"Cannot overlap another time block."));
					return;
				}
				if(SchedDefaults.Cur.SchedDefaultNum!=SchedDefaults.List[i].SchedDefaultNum
					&& SchedDefaults.Cur.DayOfWeek==SchedDefaults.List[i].DayOfWeek
					&& SchedDefaults.Cur.StopTime.TimeOfDay.CompareTo(SchedDefaults.List[i].StartTime.TimeOfDay)>0
					&& SchedDefaults.Cur.StopTime.TimeOfDay.CompareTo(SchedDefaults.List[i].StopTime.TimeOfDay)<=0
					){
					MessageBox.Show(Lan.g(this,"Cannot overlap another time block."));
					return;
				}
				if(SchedDefaults.Cur.SchedDefaultNum!=SchedDefaults.List[i].SchedDefaultNum
					&& SchedDefaults.Cur.DayOfWeek==SchedDefaults.List[i].DayOfWeek
					&& SchedDefaults.Cur.StartTime.TimeOfDay.CompareTo(SchedDefaults.List[i].StartTime.TimeOfDay)<=0
					&& SchedDefaults.Cur.StopTime.TimeOfDay.CompareTo(SchedDefaults.List[i].StopTime.TimeOfDay)>=0
					){
					MessageBox.Show(Lan.g(this,"Cannot overlap another time block."));
					return;
				}
			}
			if(IsNew){
				SchedDefaults.InsertCur();
			}
			else{
				SchedDefaults.UpdateCur();
			}
			DialogResult=DialogResult.OK;
		}

	}
}