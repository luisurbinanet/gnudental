using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.ServiceProcess;
using System.Windows.Forms;

/*  OBSOLETE

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormConfigStatus : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.ProgressBar progBar;
		public System.Windows.Forms.Label labelMain;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Timer timer1;
		///<summary>Set true to start.  Set false to stop.</summary>
		public bool StartService;
		private ServiceController sc;

		///<summary></summary>
		public FormConfigStatus()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.C("All", new System.Windows.Forms.Control[] {
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormConfigStatus));
			this.butCancel = new System.Windows.Forms.Button();
			this.progBar = new System.Windows.Forms.ProgressBar();
			this.labelMain = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(175, 103);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(88, 26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// progBar
			// 
			this.progBar.Location = new System.Drawing.Point(20, 61);
			this.progBar.Name = "progBar";
			this.progBar.Size = new System.Drawing.Size(399, 23);
			this.progBar.TabIndex = 1;
			this.progBar.Value = 10;
			// 
			// labelMain
			// 
			this.labelMain.Location = new System.Drawing.Point(20, 9);
			this.labelMain.Name = "labelMain";
			this.labelMain.Size = new System.Drawing.Size(398, 40);
			this.labelMain.TabIndex = 2;
			this.labelMain.Text = "Attempting to start the service";
			this.labelMain.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// FormConfigStatus
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(442, 157);
			this.Controls.Add(this.labelMain);
			this.Controls.Add(this.progBar);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormConfigStatus";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Status";
			this.Load += new System.EventHandler(this.FormConfigStatus_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormConfigStatus_Load(object sender, System.EventArgs e) {
			if(StartService){
				labelMain.Text="Attempting to start the service";
			}
			else{
				labelMain.Text="Attempting to stop the service";
			}
			sc=new ServiceController("MySql");
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void timer1_Tick(object sender, System.EventArgs e) {
			if(StartService && sc.Status.Equals(ServiceControllerStatus.Running)){
				DialogResult=DialogResult.OK;
			}
			else if(!StartService && sc.Status.Equals(ServiceControllerStatus.Stopped)){
				DialogResult=DialogResult.OK;
			}
			if(progBar.Value<96)
				progBar.Value+=5;
		}

		


	}
}


*/


















