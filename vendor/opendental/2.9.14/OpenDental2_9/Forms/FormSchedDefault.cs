using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormSchedDefault : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butClose;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private OpenDental.ContrSchedGrid contrGrid;
		private System.ComponentModel.Container components = null;
		private OpenDental.XPButton butAdd;
		private Point mousePos;

		///<summary></summary>
		public FormSchedDefault(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				label1,
				label2,
				label3,
				label4,
			  label5,
				label6,
				label7,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
       butAdd,
			 butClose,
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormSchedDefault));
			this.butClose = new System.Windows.Forms.Button();
			this.contrGrid = new OpenDental.ContrSchedGrid();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.butAdd = new OpenDental.XPButton();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClose.Location = new System.Drawing.Point(752, 653);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 26);
			this.butClose.TabIndex = 1;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// contrGrid
			// 
			this.contrGrid.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.contrGrid.Location = new System.Drawing.Point(24, 38);
			this.contrGrid.Name = "contrGrid";
			this.contrGrid.Size = new System.Drawing.Size(752, 577);
			this.contrGrid.TabIndex = 3;
			this.contrGrid.Click += new System.EventHandler(this.contrGrid_Click);
			this.contrGrid.DoubleClick += new System.EventHandler(this.contrGrid_DoubleClick);
			this.contrGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.contrGrid_MouseDown);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(159, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 14);
			this.label1.TabIndex = 4;
			this.label1.Text = "Monday";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(664, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 14);
			this.label2.TabIndex = 5;
			this.label2.Text = "Saturday";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(563, 22);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 14);
			this.label3.TabIndex = 6;
			this.label3.Text = "Friday";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(462, 22);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 14);
			this.label4.TabIndex = 7;
			this.label4.Text = "Thursday";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(361, 22);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 14);
			this.label5.TabIndex = 8;
			this.label5.Text = "Wednesday";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(260, 22);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(80, 14);
			this.label6.TabIndex = 9;
			this.label6.Text = "Tuesday";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(58, 22);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(80, 14);
			this.label7.TabIndex = 10;
			this.label7.Text = "Sunday";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(485, 653);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(106, 26);
			this.butAdd.TabIndex = 15;
			this.butAdd.Text = "&Add Block";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// FormSchedDefault
			// 
			this.AcceptButton = this.butClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(846, 690);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.contrGrid);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butClose);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSchedDefault";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Default Schedule";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormSchedDefault_Closing);
			this.Load += new System.EventHandler(this.FormSchedDefault_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormSchedDefault_Load(object sender, System.EventArgs e) {
			if(!UserPermissions.CheckUserPassword("Practice Default Schedule")){
				MessageBox.Show(Lan.g(this,"You do not have permission for this feature"));
				DialogResult=DialogResult.Cancel;
				return;
			}
			FillGrid();
		}

		private void FillGrid(){
			SchedDefaults.Refresh();
			contrGrid.ArrayBlocks=SchedDefaults.List;
			contrGrid.BackColor=Defs.Long[(int)DefCat.AppointmentColors][1].ItemColor;
			contrGrid.Refresh();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormBlockEdit FormBE=new FormBlockEdit();
			FormBE.IsNew=true;
			FormBE.ShowDialog();
			FillGrid();
		}

		private void contrGrid_Click(object sender, System.EventArgs e) {
			
		}

		private void contrGrid_DoubleClick(object sender, System.EventArgs e) {
			int tempDay=(int)Math.Floor((mousePos.X-ContrSchedGrid.NumW)/ContrSchedGrid.ColW);
			if(tempDay==7) tempDay=6;
			int tempMin=(int)((mousePos.Y-Math.Floor(mousePos.Y/ContrSchedGrid.RowH/6)*ContrSchedGrid.RowH*6)/ContrSchedGrid.RowH)*10;
			int tempHr=(int)Math.Floor(mousePos.Y/ContrSchedGrid.RowH/6);
			TimeSpan tempSpan=new TimeSpan(tempHr,tempMin,0);
			//MessageBox.Show(tempDay.ToString()+","+tempHr.ToString()+":"+tempMin.ToString());
			for(int i=0;i<SchedDefaults.List.Length;i++){
				if(tempDay==SchedDefaults.List[i].DayOfWeek
					&& tempSpan.CompareTo(SchedDefaults.List[i].StartTime.TimeOfDay) > 0 
					&& tempSpan.CompareTo(SchedDefaults.List[i].StopTime.TimeOfDay) < 0 
					){
					FormBlockEdit FormBE=new FormBlockEdit();
					FormBE.IsNew=false;
					SchedDefaults.Cur=SchedDefaults.List[i];
					FormBE.ShowDialog();
					FillGrid();
					return;
				}
			}
		}

		private void contrGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			mousePos=new Point(e.X,e.Y);
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormSchedDefault_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
			SecurityLogs.MakeLogEntry("Practice Default Schedule","Altered Schedule Defaults");	
		}

	}
}
