using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormUpdate : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Button butReset;
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.GroupBox groupAvailable;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label10;
		private OpenDental.UI.Button butDownload;
		private OpenDental.UI.Button butCheck;
		private OpenDental.UI.Button butLicense;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textRegistrationNumber;
		private System.Windows.Forms.TextBox textWebsitePath;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox checkInvalid;
		private System.Windows.Forms.CheckBox checkFailed;
		private System.Windows.Forms.CheckBox checkCurrent;
		private System.Windows.Forms.CheckBox checkNewBuild;
		private System.Windows.Forms.CheckBox checkNewVersion;
		private System.Windows.Forms.CheckBox checkBeta;
		private System.Windows.Forms.TextBox textNewVersion;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private WebClient myWebClient;
		private string myStringWebResource;
		private FormProgress FormP;

		///<summary></summary>
		public FormUpdate()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormUpdate));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butReset = new System.Windows.Forms.Button();
			this.labelVersion = new System.Windows.Forms.Label();
			this.groupAvailable = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textNewVersion = new System.Windows.Forms.TextBox();
			this.checkBeta = new System.Windows.Forms.CheckBox();
			this.butDownload = new OpenDental.UI.Button();
			this.butCheck = new OpenDental.UI.Button();
			this.checkInvalid = new System.Windows.Forms.CheckBox();
			this.checkCurrent = new System.Windows.Forms.CheckBox();
			this.checkFailed = new System.Windows.Forms.CheckBox();
			this.checkNewBuild = new System.Windows.Forms.CheckBox();
			this.checkNewVersion = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textRegistrationNumber = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.butLicense = new OpenDental.UI.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textWebsitePath = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupAvailable.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.Location = new System.Drawing.Point(554, 239);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 25);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(554, 198);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 25);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butReset
			// 
			this.butReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.butReset.ForeColor = System.Drawing.SystemColors.Control;
			this.butReset.Location = new System.Drawing.Point(0, 0);
			this.butReset.Name = "butReset";
			this.butReset.Size = new System.Drawing.Size(61, 49);
			this.butReset.TabIndex = 6;
			this.butReset.Click += new System.EventHandler(this.butReset_Click);
			// 
			// labelVersion
			// 
			this.labelVersion.Location = new System.Drawing.Point(9, 9);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(188, 23);
			this.labelVersion.TabIndex = 10;
			this.labelVersion.Text = "Using Version ";
			// 
			// groupAvailable
			// 
			this.groupAvailable.Controls.Add(this.textNewVersion);
			this.groupAvailable.Controls.Add(this.checkBeta);
			this.groupAvailable.Controls.Add(this.butDownload);
			this.groupAvailable.Controls.Add(this.butCheck);
			this.groupAvailable.Controls.Add(this.checkInvalid);
			this.groupAvailable.Controls.Add(this.checkCurrent);
			this.groupAvailable.Controls.Add(this.checkFailed);
			this.groupAvailable.Controls.Add(this.checkNewBuild);
			this.groupAvailable.Controls.Add(this.checkNewVersion);
			this.groupAvailable.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupAvailable.Location = new System.Drawing.Point(9, 74);
			this.groupAvailable.Name = "groupAvailable";
			this.groupAvailable.Size = new System.Drawing.Size(493, 205);
			this.groupAvailable.TabIndex = 17;
			this.groupAvailable.TabStop = false;
			this.groupAvailable.Text = "Update Availability";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			// 
			// textNewVersion
			// 
			this.textNewVersion.Location = new System.Drawing.Point(172, 178);
			this.textNewVersion.Name = "textNewVersion";
			this.textNewVersion.ReadOnly = true;
			this.textNewVersion.Size = new System.Drawing.Size(89, 20);
			this.textNewVersion.TabIndex = 33;
			this.textNewVersion.Text = "";
			// 
			// checkBeta
			// 
			this.checkBeta.AutoCheck = false;
			this.checkBeta.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkBeta.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBeta.Location = new System.Drawing.Point(13, 139);
			this.checkBeta.Name = "checkBeta";
			this.checkBeta.Size = new System.Drawing.Size(339, 28);
			this.checkBeta.TabIndex = 32;
			this.checkBeta.Text = "The available version is beta (test), so it has some bugs and you will need to up" +
				"date it frequently.";
			this.checkBeta.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// butDownload
			// 
			this.butDownload.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDownload.Autosize = true;
			this.butDownload.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDownload.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDownload.Location = new System.Drawing.Point(11, 175);
			this.butDownload.Name = "butDownload";
			this.butDownload.Size = new System.Drawing.Size(83, 25);
			this.butDownload.TabIndex = 20;
			this.butDownload.Text = "Download";
			this.butDownload.Click += new System.EventHandler(this.butDownload_Click);
			// 
			// butCheck
			// 
			this.butCheck.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCheck.Autosize = true;
			this.butCheck.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCheck.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCheck.Location = new System.Drawing.Point(13, 18);
			this.butCheck.Name = "butCheck";
			this.butCheck.Size = new System.Drawing.Size(117, 25);
			this.butCheck.TabIndex = 21;
			this.butCheck.Text = "Check for Updates";
			this.butCheck.Click += new System.EventHandler(this.butCheck_Click);
			// 
			// checkInvalid
			// 
			this.checkInvalid.AutoCheck = false;
			this.checkInvalid.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkInvalid.Location = new System.Drawing.Point(13, 53);
			this.checkInvalid.Name = "checkInvalid";
			this.checkInvalid.Size = new System.Drawing.Size(463, 17);
			this.checkInvalid.TabIndex = 27;
			this.checkInvalid.Text = "Registration number not valid";
			// 
			// checkCurrent
			// 
			this.checkCurrent.AutoCheck = false;
			this.checkCurrent.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkCurrent.Location = new System.Drawing.Point(13, 87);
			this.checkCurrent.Name = "checkCurrent";
			this.checkCurrent.Size = new System.Drawing.Size(463, 17);
			this.checkCurrent.TabIndex = 29;
			this.checkCurrent.Text = "You are using the most current build of this version";
			// 
			// checkFailed
			// 
			this.checkFailed.AutoCheck = false;
			this.checkFailed.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkFailed.Location = new System.Drawing.Point(13, 70);
			this.checkFailed.Name = "checkFailed";
			this.checkFailed.Size = new System.Drawing.Size(463, 17);
			this.checkFailed.TabIndex = 28;
			this.checkFailed.Text = "Registration number not valid, or internet connection failed";
			// 
			// checkNewBuild
			// 
			this.checkNewBuild.AutoCheck = false;
			this.checkNewBuild.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNewBuild.Location = new System.Drawing.Point(13, 104);
			this.checkNewBuild.Name = "checkNewBuild";
			this.checkNewBuild.Size = new System.Drawing.Size(463, 17);
			this.checkNewBuild.TabIndex = 30;
			this.checkNewBuild.Text = "A new build of this version is available for download";
			// 
			// checkNewVersion
			// 
			this.checkNewVersion.AutoCheck = false;
			this.checkNewVersion.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNewVersion.Location = new System.Drawing.Point(13, 121);
			this.checkNewVersion.Name = "checkNewVersion";
			this.checkNewVersion.Size = new System.Drawing.Size(463, 17);
			this.checkNewVersion.TabIndex = 31;
			this.checkNewVersion.Text = "A newer version is also available.   Contact us for a new Registration number";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(9, 31);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(134, 19);
			this.label2.TabIndex = 18;
			this.label2.Text = "Registration number";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textRegistrationNumber
			// 
			this.textRegistrationNumber.Location = new System.Drawing.Point(149, 31);
			this.textRegistrationNumber.Name = "textRegistrationNumber";
			this.textRegistrationNumber.Size = new System.Drawing.Size(113, 20);
			this.textRegistrationNumber.TabIndex = 19;
			this.textRegistrationNumber.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(13, 409);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(248, 20);
			this.label6.TabIndex = 15;
			this.label6.Text = "We also wish to thank:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(13, 328);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(584, 20);
			this.label7.TabIndex = 13;
			this.label7.Text = "All parts of this program are licensed under the GPL, www.opensource.org/licenses" +
				"/gpl-license.php";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(12, 377);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(584, 20);
			this.label8.TabIndex = 12;
			this.label8.Text = "MySQL - Copyright 1995-2005, www.mysql.com";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(12, 352);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(585, 23);
			this.label10.TabIndex = 10;
			this.label10.Text = "This program Copyright 2003-2005, Jordan S. Sparks, D.M.D.";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butLicense
			// 
			this.butLicense.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butLicense.Autosize = true;
			this.butLicense.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLicense.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLicense.Image = ((System.Drawing.Image)(resources.GetObject("butLicense.Image")));
			this.butLicense.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butLicense.Location = new System.Drawing.Point(15, 286);
			this.butLicense.Name = "butLicense";
			this.butLicense.Size = new System.Drawing.Size(102, 25);
			this.butLicense.TabIndex = 21;
			this.butLicense.Text = "License Info";
			this.butLicense.Click += new System.EventHandler(this.butLicense_Click);
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(16, 436);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(558, 130);
			this.textBox1.TabIndex = 22;
			this.textBox1.Text = "David Adams\r\nDan Crawford\r\nLarry Dagley\r\nAnn Hellemans-De Hondt\r\nSamir Kothari\r\nJ" +
				"eff Smerdon";
			// 
			// textWebsitePath
			// 
			this.textWebsitePath.Location = new System.Drawing.Point(149, 52);
			this.textWebsitePath.Name = "textWebsitePath";
			this.textWebsitePath.Size = new System.Drawing.Size(337, 20);
			this.textWebsitePath.TabIndex = 24;
			this.textWebsitePath.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 52);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(134, 19);
			this.label3.TabIndex = 26;
			this.label3.Text = "Website Path";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormUpdate
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(649, 573);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textWebsitePath);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.butLicense);
			this.Controls.Add(this.textRegistrationNumber);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.groupAvailable);
			this.Controls.Add(this.labelVersion);
			this.Controls.Add(this.butReset);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label8);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormUpdate";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Update";
			this.Load += new System.EventHandler(this.FormUpdate_Load);
			this.groupAvailable.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormUpdate_Load(object sender, System.EventArgs e) {
			Height=butLicense.Bottom+50;
			labelVersion.Text=Lan.g(this,"Using Version:")+" "+Application.ProductVersion;
			textRegistrationNumber.Text=Prefs.GetString("RegistrationNumber");
			textWebsitePath.Text=Prefs.GetString("UpdateWebsitePath");//should include trailing /
			butDownload.Enabled=false;
		}

		private void butReset_Click(object sender, System.EventArgs e) {
			FormPasswordReset FormPR=new FormPasswordReset();
			FormPR.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		private void butLicense_Click(object sender, System.EventArgs e) {
			Height=textBox1.Bottom+50;
		}

		private void butCheck_Click(object sender, System.EventArgs e) {
			Cursor=Cursors.WaitCursor;
			SavePrefs();
//butDownload.Enabled=false;
			checkInvalid.Checked=false;
			checkFailed.Checked=false;
			checkCurrent.Checked=false;
			checkNewBuild.Checked=false;
			checkNewVersion.Checked=false;
			checkBeta.Checked=false;
			textNewVersion.Text="";
			if(textRegistrationNumber.Text.Length==0){
				checkInvalid.Checked=true;
				Cursor=Cursors.Default;
				return;
			}
			string remoteUri=textWebsitePath.Text+textRegistrationNumber.Text+"/";
			string fileName="Manifest.txt";
			WebClient myWebClient=new WebClient();
			string myStringWebResource=remoteUri+fileName;
			Version versionNewBuild=null;
			string strNewVersion;
			bool isBeta=false;
			try{
				using(StreamReader sr=new StreamReader(myWebClient.OpenRead(myStringWebResource))){
					string newBuild=sr.ReadLine();//must be be 3 or 4 components (revision is optional)
					if(newBuild.EndsWith("b")){
						isBeta=true;
						newBuild=newBuild.Replace("b","");
					}
					versionNewBuild=new Version(newBuild);
					if(versionNewBuild.Revision==-1){
						versionNewBuild=new Version(
							versionNewBuild.Major,versionNewBuild.Minor,versionNewBuild.Build,0);
					}
					strNewVersion=sr.ReadLine();//returns null if no second line
					if(strNewVersion!=null && strNewVersion.EndsWith("b")){
						isBeta=true;
						strNewVersion=strNewVersion.Replace("b","");
					}
				}
			}
			catch{
				checkFailed.Checked=true;
				Cursor=Cursors.Default;
				return;
			}
			Debug.WriteLine(versionNewBuild);//3.4.2
			Debug.WriteLine(new Version(Application.ProductVersion));//3.4.2.0
			if(versionNewBuild == new Version(Application.ProductVersion)){
				checkCurrent.Checked=true;
			}
			if(versionNewBuild != new Version(Application.ProductVersion)){
				//this also allows users to install previous versions.
				checkNewBuild.Checked=true;
				textNewVersion.Text=versionNewBuild.ToString();
				butDownload.Enabled=true;
			}
			//Whether or not build is current, we want to inform user about the next minor version
			if(strNewVersion!=null){//we don't really care what it is.
				checkNewVersion.Checked=true;
			}
			if((checkNewBuild.Checked || checkNewVersion.Checked) && isBeta){
				checkBeta.Checked=true;
			}
			Cursor=Cursors.Default;
		}

		private void butDownload_Click(object sender, System.EventArgs e) {
			string remoteUri=textWebsitePath.Text+textRegistrationNumber.Text+"/";
			myWebClient=new WebClient();
			myStringWebResource=remoteUri+"Setup.exe";
			WebRequest wr= WebRequest.Create(myStringWebResource);
			WebResponse webResp= wr.GetResponse();
			int fileSize=(int)webResp.ContentLength/1024;
			//start the thread that will perform the download
			Thread workerThread=new Thread(new ThreadStart(InstanceMethod));
      workerThread.Start();
			//display the progress dialog to the user:
			FormP=new FormProgress();
			FormP.MaxVal=fileSize;
			FormP.FileName=Prefs.GetString("DocPath")+"Setup.exe";
			FormP.ShowDialog();
			if(FormP.DialogResult==DialogResult.Cancel){
				workerThread.Abort();
				return;
			}
			MsgBox.Show(this,"Download succeeded.  Setup program will now begin.  When done, restart the program on this computer, then on the other computers.");
			try{
				Process.Start(Prefs.GetString("DocPath")+"Setup.exe");
				ExitApplicationNow.ExitNow();
			}
			catch{
				MsgBox.Show(this,"Could not launch setup");
			}
		}

		///<summary>This is the function that the worker thread uses to actually perform the download.</summary>
		private void InstanceMethod(){
			File.Delete(Prefs.GetString("DocPath")+"Setup.exe");//fixes a minor bug
			int chunk=10;//KB
			byte[] buffer;
			int i=0;
			Stream readStream=myWebClient.OpenRead(myStringWebResource);
			BinaryReader br=new BinaryReader(readStream);
			FileStream writeStream=new FileStream(Prefs.GetString("DocPath")+"Setup.exe",FileMode.Create);
			BinaryWriter bw=new BinaryWriter(writeStream);
			try{
				while(true){
					buffer=br.ReadBytes(chunk*1024);
					if(buffer.Length==0){
						break;
					}
					Invoke(new PassProgressDelegate(PassProgressToDialog),
						new object [] { (chunk*i)+(int)((double)buffer.Length/1024) });
					bw.Write(buffer);
					i++;
				}
			}
			catch{//for instance, if abort.
				br.Close();
				bw.Close();
				File.Delete(Prefs.GetString("DocPath")+"Setup.exe");
			}
			finally{
				br.Close();
				bw.Close();
			}
			//myWebClient.DownloadFile(myStringWebResource,Prefs.GetString("DocPath")+"Setup.exe");
		}

		///<summary>This function gets invoked from the worker thread.</summary>
		private void PassProgressToDialog(int msg){
			FormP.CurrentVal=msg;
		}

		private void SavePrefs(){
			bool changed=false;
			if(Prefs.UpdateString("RegistrationNumber",textRegistrationNumber.Text)){
				changed=true;
			}
			if(Prefs.UpdateString("UpdateWebsitePath",textWebsitePath.Text)){
				changed=true;
			}
			if(changed){
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			SavePrefs();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	

	

		

	

	}

	///<summary></summary>
	public delegate void PassProgressDelegate(int msg);
}





















