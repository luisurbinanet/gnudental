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
		private System.Windows.Forms.TextBox textExportPath;
		private System.Windows.Forms.TextBox textDocPath;
		private OpenDental.UI.Button butBrowseExport;
		private OpenDental.UI.Button buBrowseDoc;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private OpenDental.UI.Button butBrowseLetter;
		private System.Windows.Forms.TextBox textLetterMergePath;
		private System.Windows.Forms.FolderBrowserDialog fb;
    //private bool IsBackup=false;

		///<summary></summary>
		public FormPath(){
			InitializeComponent();
			Lan.F(this);
			//Lan.C(this, new System.Windows.Forms.Control[] {
			//	this.textBox1,
			//	this.textBox3
			//});
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
			this.textExportPath = new System.Windows.Forms.TextBox();
			this.butBrowseExport = new OpenDental.UI.Button();
			this.buBrowseDoc = new OpenDental.UI.Button();
			this.fb = new System.Windows.Forms.FolderBrowserDialog();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.butBrowseLetter = new OpenDental.UI.Button();
			this.textLetterMergePath = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(620, 362);
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
			this.butCancel.Location = new System.Drawing.Point(620, 396);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textDocPath
			// 
			this.textDocPath.Location = new System.Drawing.Point(10, 104);
			this.textDocPath.Name = "textDocPath";
			this.textDocPath.Size = new System.Drawing.Size(518, 20);
			this.textDocPath.TabIndex = 0;
			this.textDocPath.Text = "";
			this.textDocPath.Leave += new System.EventHandler(this.textDocPath_Leave);
			// 
			// textExportPath
			// 
			this.textExportPath.Location = new System.Drawing.Point(10, 210);
			this.textExportPath.Name = "textExportPath";
			this.textExportPath.Size = new System.Drawing.Size(515, 20);
			this.textExportPath.TabIndex = 1;
			this.textExportPath.Text = "";
			this.textExportPath.Leave += new System.EventHandler(this.textExportPath_Leave);
			// 
			// butBrowseExport
			// 
			this.butBrowseExport.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butBrowseExport.Autosize = true;
			this.butBrowseExport.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBrowseExport.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBrowseExport.Location = new System.Drawing.Point(530, 208);
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
			this.buBrowseDoc.Location = new System.Drawing.Point(531, 102);
			this.buBrowseDoc.Name = "buBrowseDoc";
			this.buBrowseDoc.Size = new System.Drawing.Size(76, 25);
			this.buBrowseDoc.TabIndex = 90;
			this.buBrowseDoc.Text = "&Browse";
			this.buBrowseDoc.Click += new System.EventHandler(this.buBrowseDoc_Click);
			// 
			// fb
			// 
			this.fb.SelectedPath = "C:\\";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(11, 144);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(543, 65);
			this.label1.TabIndex = 92;
			this.label1.Text = @"Export Path: for exporting tables.  If you use a network path (like \\server\OpenDentalExport\ ), the data will be exported to one central computer.   But it is usually easier to use a local path (like C:\OpenDentalExport\ ), and the data will be stored on the local hard drive of the computer you export on.  The folder will be created later if it does not exist.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(11, 34);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(545, 63);
			this.label2.TabIndex = 93;
			this.label2.Text = @"Document Path: for storing images of documents.  This path is the same for every computer, and there can be only be one folder.  If you have only one computer, then the folder can be local (like C:\OpenDentalData\ ), otherwise it should be a folder shared on the network (like \\server\OpenDentalData\ ).  It must contain the A - Z folders.  ";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(11, 239);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(543, 65);
			this.label3.TabIndex = 96;
			this.label3.Text = @"Letter Merge Path: The location where your letter templates are stored.   Letters are usually stored in one central location, so use a network path (like \\server\OpenDentalLetters\ ).   Don't forget to share the folder so all the computers can access it.  However, if you only have one computer with no network, then use a local path (like C:\OpenDentalLetters\ ). ";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butBrowseLetter
			// 
			this.butBrowseLetter.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butBrowseLetter.Autosize = true;
			this.butBrowseLetter.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBrowseLetter.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBrowseLetter.Location = new System.Drawing.Point(530, 303);
			this.butBrowseLetter.Name = "butBrowseLetter";
			this.butBrowseLetter.Size = new System.Drawing.Size(76, 25);
			this.butBrowseLetter.TabIndex = 95;
			this.butBrowseLetter.Text = "Browse";
			this.butBrowseLetter.Click += new System.EventHandler(this.butBrowseLetter_Click);
			// 
			// textLetterMergePath
			// 
			this.textLetterMergePath.Location = new System.Drawing.Point(10, 305);
			this.textLetterMergePath.Name = "textLetterMergePath";
			this.textLetterMergePath.Size = new System.Drawing.Size(515, 20);
			this.textLetterMergePath.TabIndex = 94;
			this.textLetterMergePath.Text = "";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(12, 9);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(483, 23);
			this.label4.TabIndex = 97;
			this.label4.Text = "The first box is mandatory.  The others are optional.";
			// 
			// FormPath
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(718, 437);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.butBrowseLetter);
			this.Controls.Add(this.textLetterMergePath);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butBrowseExport);
			this.Controls.Add(this.buBrowseDoc);
			this.Controls.Add(this.textDocPath);
			this.Controls.Add(this.textExportPath);
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
			/*Lan.C(this, new System.Windows.Forms.Control[] {
				this.textBox1,
				this.textBox3
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				this.butCancel,
				this.butOK
			});*/
			textDocPath.Text=((Pref)Prefs.HList["DocPath"]).ValueString;
			textExportPath.Text=((Pref)Prefs.HList["ExportPath"]).ValueString;
			textLetterMergePath.Text=((Pref)Prefs.HList["LetterMergePath"]).ValueString;
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
      //CheckIfDocBackup();//checks if new folder is pointing at a backup
      /*if(IsBackup){
				if(MessageBox.Show(Lan.g(this,"You are setting you Image Folder to a Backup Folder.  Do you wish to continue?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;   
				} 
      }*/
			Prefs.Cur=(Pref)Prefs.HList["DocPath"];
			Prefs.Cur.ValueString=textDocPath.Text;
			Prefs.UpdateCur();      
			Prefs.Cur=(Pref)Prefs.HList["ExportPath"];
			Prefs.Cur.ValueString=textExportPath.Text;
			Prefs.UpdateCur();
			Prefs.Cur=(Pref)Prefs.HList["LetterMergePath"];
			Prefs.Cur.ValueString=textLetterMergePath.Text;
			Prefs.UpdateCur();
			DataValid.SetInvalid(InvalidTypes.Prefs);
			SecurityLogs.MakeLogEntry("Form Path","Altered Path");
			DialogResult=DialogResult.OK;
		}

    /*private void CheckIfDocBackup(){
      IsBackup=false;
 			DirectoryInfo dirInfo=new DirectoryInfo(textDocPath.Text);
			FileInfo[] fi=dirInfo.GetFiles();
			for(int i=0;i<fi.Length;i++){
				if(fi[i].Name=="Backup"){
          IsBackup=true;   
        }
			}	       
    }*/

		private void buBrowseDoc_Click(object sender, System.EventArgs e){
		  fb.ShowDialog();
      textDocPath.Text=fb.SelectedPath+@"\";
		}

		private void butBrowseExport_Click(object sender, System.EventArgs e){
		  fb.ShowDialog();
      textExportPath.Text=fb.SelectedPath+@"\";		
		}

		private void butBrowseLetter_Click(object sender, System.EventArgs e) {
			fb.ShowDialog();
      textLetterMergePath.Text=fb.SelectedPath+@"\";		
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
