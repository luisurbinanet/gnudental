using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormProgress : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;
		///<summary></summary>
		public string FileName;
		///<summary>The total size of the file in KB to download.</summary>
		public int MaxVal;
		private System.Windows.Forms.Label labelProgress;
		///<summary>KB downloaded so far.</summary>
		public int CurrentVal;

		///<summary>Supply the fileSize here so that the progress bar will display properly. Also supply the name of the file that is getting downloaded.  The progress bar will use the current size of the file.</summary>
		public FormProgress(){
			//
			// Required for Windows Form Designer support
			//
			//FileName=fileName;
			InitializeComponent();
			//progressBar1.Maximum=maxVal;
			Lan.F(this);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormProgress));
			this.butCancel = new OpenDental.UI.Button();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.label1 = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.labelProgress = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.Location = new System.Drawing.Point(376, 182);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(73, 79);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(377, 23);
			this.progressBar1.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(70, 49);
			this.label1.Name = "label1";
			this.label1.TabIndex = 3;
			this.label1.Text = "Progress";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 200;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// labelProgress
			// 
			this.labelProgress.Location = new System.Drawing.Point(72, 119);
			this.labelProgress.Name = "labelProgress";
			this.labelProgress.Size = new System.Drawing.Size(213, 23);
			this.labelProgress.TabIndex = 4;
			this.labelProgress.Text = "labelProgress";
			// 
			// FormProgress
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(500, 261);
			this.Controls.Add(this.labelProgress);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProgress";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.FormProgress_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormProgress_Load(object sender, System.EventArgs e) {
			progressBar1.Maximum=MaxVal;
		}
		
		///<summary>Happens every 200 ms</summary>
		private void timer1_Tick(object sender, System.EventArgs e) {
			//progress bar shows 0 through end file size
			labelProgress.Text
				=((double)CurrentVal/1024).ToString("F")+" MB of "
				+((double)MaxVal/1024).ToString("F")+" MB copied"; 
			if(CurrentVal<progressBar1.Maximum){
				progressBar1.Value=CurrentVal;
			}
			else{
				//must be done.
				//progressBar1.Value=progressBar1.Maximum;
				DialogResult=DialogResult.OK;
			}
		}

		private void butCancel_Click(object sender, System.EventArgs e) {

			DialogResult=DialogResult.Cancel;
		}

		

		


	}
}





















