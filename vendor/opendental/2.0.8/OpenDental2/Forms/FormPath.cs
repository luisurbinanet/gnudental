using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace OpenDental{

	public class FormPath : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textExportPath;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.TextBox textDocPath;
		private System.Windows.Forms.Button butBrowseExport;
		private System.Windows.Forms.Button buBrowseDoc;
		private System.Windows.Forms.FolderBrowserDialog fbExportPath;
		private System.Windows.Forms.FolderBrowserDialog fbDocPath;
    private bool IsBackup=false;

		public FormPath(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				butBrowseExport,
				buBrowseDoc,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
			});
		}

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
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.textDocPath = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textExportPath = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.butBrowseExport = new System.Windows.Forms.Button();
			this.buBrowseDoc = new System.Windows.Forms.Button();
			this.fbExportPath = new System.Windows.Forms.FolderBrowserDialog();
			this.fbDocPath = new System.Windows.Forms.FolderBrowserDialog();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(556, 331);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 2;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.Location = new System.Drawing.Point(556, 365);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "Cancel";
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
			this.textBox1.Location = new System.Drawing.Point(13, 152);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(510, 72);
			this.textBox1.TabIndex = 5;
			this.textBox1.Text = @"Export Path: for exporting tables.  If you use a network path (like \\server\FreeDentalExport\ ), the data will be exported to one central computer.   But it is usually easier to use a local path (like C:\FreeDentalExport\ ), and the data will be stored on the local hard drive of the computer you export on.  The folder will be created later if it does not exist.";
			// 
			// textExportPath
			// 
			this.textExportPath.Location = new System.Drawing.Point(11, 224);
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
			this.textBox3.Text = @"Document Path: for storing images of documents.  This path is the same for every computer, and there can be only be one folder.  If you have only one computer, then the folder can be local (like C:\FreeDentalData\ ), otherwise it should be a folder shared on the network (like \\server\FreeDentalData\ ).  It must contain the A - Z folders.  ";
			// 
			// butBrowseExport
			// 
			this.butBrowseExport.Location = new System.Drawing.Point(531, 224);
			this.butBrowseExport.Name = "butBrowseExport";
			this.butBrowseExport.Size = new System.Drawing.Size(100, 23);
			this.butBrowseExport.TabIndex = 91;
			this.butBrowseExport.Text = "Browse";
			this.butBrowseExport.Click += new System.EventHandler(this.butBrowseExport_Click);
			// 
			// buBrowseDoc
			// 
			this.buBrowseDoc.Location = new System.Drawing.Point(531, 84);
			this.buBrowseDoc.Name = "buBrowseDoc";
			this.buBrowseDoc.Size = new System.Drawing.Size(100, 23);
			this.buBrowseDoc.TabIndex = 90;
			this.buBrowseDoc.Text = "Browse";
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
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(652, 413);
			this.ControlBox = false;
			this.Controls.Add(this.butBrowseExport);
			this.Controls.Add(this.buBrowseDoc);
			this.Controls.Add(this.textDocPath);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.textExportPath);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Name = "FormPath";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Paths";
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
			if(!textDocPath.Text.EndsWith(@"\")){
				textDocPath.Text+=@"\";
			}
		}

		private void textExportPath_Leave(object sender, System.EventArgs e) {
			if(!textExportPath.Text.EndsWith(@"\")){
				textExportPath.Text+=@"\";
			}
		}

		private void butOK_Click(object sender, System.EventArgs e){
			//if(
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

		private void butCancel_Click(object sender, System.EventArgs e){
			if(!Directory.Exists(((Pref)Prefs.HList["DocPath"]).ValueString) || !Directory.Exists(((Pref)Prefs.HList["DocPath"]).ValueString+"A\\")){
				MessageBox.Show(Lan.g(this,"Invalid Document path.  Closing Free Dental."));
				Application.Exit();
			}
			DialogResult=DialogResult.Cancel;
		}

		private void buBrowseDoc_Click(object sender, System.EventArgs e){
		  fbDocPath.ShowDialog();
      textDocPath.Text=fbDocPath.SelectedPath+@"\";
		}

		private void butBrowseExport_Click(object sender, System.EventArgs e){
		  fbExportPath.ShowDialog();
      textDocPath.Text=fbExportPath.SelectedPath+@"\";		
		}

	}
}
