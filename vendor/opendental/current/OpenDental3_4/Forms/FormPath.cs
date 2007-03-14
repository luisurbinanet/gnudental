using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormPath : System.Windows.Forms.Form{
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textExportPath;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.TextBox textDocPath;
		private OpenDental.UI.Button butBrowseExport;
		private OpenDental.UI.Button buBrowseDoc;
		private System.Windows.Forms.FolderBrowserDialog fbExportPath;
		private System.Windows.Forms.FolderBrowserDialog fbDocPath;
    private bool IsBackup=false;

		///<summary></summary>
		public FormPath(){
			InitializeComponent();
			Lan.F(this);
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.textBox1,
				this.textBox3
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
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textDocPath = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textExportPath = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.butBrowseExport = new OpenDental.UI.Button();
			this.buBrowseDoc = new OpenDental.UI.Button();
			this.fbExportPath = new System.Windows.Forms.FolderBrowserDialog();
			this.fbDocPath = new System.Windows.Forms.FolderBrowserDialog();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(629, 267);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 2;
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
			this.butCancel.Location = new System.Drawing.Point(629, 301);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textDocPath
			// 
			this.textDocPath.Location = new System.Drawing.Point(11, 84);
			this.textDocPath.Name = "textDocPath";
			this.textDocPath.Size = new System.Drawing.Size(518, 20);
			this.textDocPath.TabIndex = 0;
			this.textDocPath.Text = "";
			this.textDocPath.Leave += new System.EventHandler(this.textDocPath_Leave);
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(12, 136);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(510, 72);
			this.textBox1.TabIndex = 5;
			this.textBox1.Text = @"Export Path: for exporting tables.  If you use a network path (like \\server\OpenDentalExport\ ), the data will be exported to one central computer.   But it is usually easier to use a local path (like C:\OpenDentalExport\ ), and the data will be stored on the local hard drive of the computer you export on.  The folder will be created later if it does not exist.";
			// 
			// textExportPath
			// 
			this.textExportPath.Location = new System.Drawing.Point(10, 208);
			this.textExportPath.Name = "textExportPath";
			this.textExportPath.Size = new System.Drawing.Size(515, 20);
			this.textExportPath.TabIndex = 1;
			this.textExportPath.Text = "";
			this.textExportPath.Leave += new System.EventHandler(this.textExportPath_Leave);
			// 
			// textBox3
			// 
			this.textBox3.BackColor = System.Drawing.SystemColors.Control;
			this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox3.Location = new System.Drawing.Point(12, 14);
			this.textBox3.Multiline = true;
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(516, 70);
			this.textBox3.TabIndex = 7;
			this.textBox3.Text = @"Document Path: for storing images of documents.  This path is the same for every computer, and there can be only be one folder.  If you have only one computer, then the folder can be local (like C:\OpenDentalData\ ), otherwise it should be a folder shared on the network (like \\server\OpenDentalData\ ).  It must contain the A - Z folders.  ";
			// 
			// butBrowseExport
			// 
			this.butBrowseExport.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butBrowseExport.Autosize = true;
			this.butBrowseExport.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBrowseExport.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBrowseExport.Location = new System.Drawing.Point(530, 206);
			this.butBrowseExport.Name = "butBrowseExport";
			this.butBrowseExport.Size = new System.Drawing.Size(76, 25);
			this.butBrowseExport.TabIndex = 91;
			this.butBrowseExport.Text = "Browse";
			this.butBrowseExport.Click += new System.EventHandler(this.butBrowseExport_Click);
			// 
			// buBrowseDoc
			// 
			this.buBrowseDoc.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.buBrowseDoc.Autosize = true;
			this.buBrowseDoc.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buBrowseDoc.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buBrowseDoc.Location = new System.Drawing.Point(531, 82);
			this.buBrowseDoc.Name = "buBrowseDoc";
			this.buBrowseDoc.Size = new System.Drawing.Size(76, 25);
			this.buBrowseDoc.TabIndex = 90;
			this.buBrowseDoc.Text = "&Browse";
			this.buBrowseDoc.Click += new System.EventHandler(this.buBrowseDoc_Click);
			// 
			// fbExportPath
			// 
			this.fbExportPath.SelectedPath = "C:\\";
			// 
			// fbDocPath
			// 
			this.fbDocPath.SelectedPath = "C:\\";
			// 
			// FormPath
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(727, 342);
			this.Controls.Add(this.butBrowseExport);
			this.Controls.Add(this.buBrowseDoc);
			this.Controls.Add(this.textDocPath);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.textExportPath);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPath";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Paths";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormPath_Closing);
			this.Load += new System.EventHandler(this.FormPath_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormPath_Load(object sender, System.EventArgs e){
			if(!UserPermissions.CheckUserPassword("Data Paths")){
				MessageBox.Show(Lan.g(this,"You do not have permission for this feature."));
				DialogResult=DialogResult.Cancel;
				return;
			}
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.textBox1,
				this.textBox3
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				this.butCancel,
				this.butOK
			});
			textDocPath.Text=((Pref)Prefs.HList["DocPath"]).ValueString;
			textExportPath.Text=((Pref)Prefs.HList["ExportPath"]).ValueString;
		}

		private void textDocPath_Leave(object sender, System.EventArgs e) {
			//if(!textDocPath.Text.EndsWith(@"\")){
			//	textDocPath.Text+=@"\";
			//}
		}

		private void textExportPath_Leave(object sender, System.EventArgs e) {
			//if(!textExportPath.Text.EndsWith(@"\")){
			//	textExportPath.Text+=@"\";
			//}
		}

		private void butOK_Click(object sender, System.EventArgs e){
			/*string remoteUri = "http://www.open-dent.com/languages/";
			string fileName = CultureInfo.CurrentCulture.TwoLetterISOLanguageName+".sql";//eg. es.sql for spanish
			//string fileName="bogus.sql";
			string myStringWebResource = null;
			WebClient myWebClient = new WebClient();
			myStringWebResource = remoteUri + fileName;
			try{
				//myWebClient.Credentials=new NetworkCredential("username","password","www.open-dent.com");
				myWebClient.DownloadFile(myStringWebResource,fileName);
			}
			catch{
				MessageBox.Show("Either you do not have internet access, or no translations are available for "+CultureInfo.CurrentCulture.Parent.DisplayName);
				return;
			}
			ClassConvertDatabase ConvertDB=new ClassConvertDatabase();
			if(!ConvertDB.ExecuteFile(fileName)){
				MessageBox.Show("Translations not installed properly.");
				return;
			}
			LanguageForeigns.Refresh();
			MessageBox.Show("Done");*/





			if(!textDocPath.Text.EndsWith(@"\")
				&& !textDocPath.Text.EndsWith(@"/"))
			{
				MessageBox.Show(Lan.g(this,"Document Path is not valid."));
				return;
			}
			if(!Directory.Exists(textDocPath.Text)){
				MessageBox.Show(Lan.g(this,"Document Path is not valid."));
				return;
    	}
			if(!Directory.Exists(textDocPath.Text+"A\\")){
				MessageBox.Show(Lan.g(this,"Document Path is not correct.  Must contain folders A-Z"));
				return;
			}
      CheckIfDocBackup();//checks if new folder is pointing at a backup
      if(IsBackup){
				if(MessageBox.Show(Lan.g(this,"You are setting you Image Folder to a Backup Folder.  Do you wish to continue?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;   
				} 
      }
			Prefs.Cur=(Pref)Prefs.HList["DocPath"];
			Prefs.Cur.ValueString=textDocPath.Text;
			Prefs.UpdateCur();      
			Prefs.Cur=(Pref)Prefs.HList["ExportPath"];
			Prefs.Cur.ValueString=textExportPath.Text;
			Prefs.UpdateCur();
			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
			SecurityLogs.MakeLogEntry("Form Path","Altered Path");
			DialogResult=DialogResult.OK;
		}

    private void CheckIfDocBackup(){
      IsBackup=false;
 			DirectoryInfo dirInfo=new DirectoryInfo(textDocPath.Text);
			FileInfo[] fi=dirInfo.GetFiles();
			for(int i=0;i<fi.Length;i++){
				if(fi[i].Name=="Backup"){
          IsBackup=true;   
        }
			}	       
    }

		

		private void buBrowseDoc_Click(object sender, System.EventArgs e){
		  fbDocPath.ShowDialog();
      textDocPath.Text=fbDocPath.SelectedPath+@"\";
		}

		private void butBrowseExport_Click(object sender, System.EventArgs e){
		  fbExportPath.ShowDialog();
      textDocPath.Text=fbExportPath.SelectedPath+@"\";		
		}

		private void butCancel_Click(object sender, System.EventArgs e){
			DialogResult=DialogResult.Cancel;
		}

		private void FormPath_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(!Directory.Exists(((Pref)Prefs.HList["DocPath"]).ValueString) || !Directory.Exists(((Pref)Prefs.HList["DocPath"]).ValueString+"A\\")){
				MessageBox.Show(Lan.g(this,"Invalid Document path.  Closing Free Dental."));
				Application.Exit();
			}
		}



	}
}
