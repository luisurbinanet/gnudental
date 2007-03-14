using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormBackup : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private OpenDental.UI.Button butBrowse;
		private OpenDental.UI.Button butSave;
		private System.Windows.Forms.TextBox textBackupFolder;
		private OpenDental.UI.Button butRestore;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkSkipBackup;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormBackup()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		///<summary></summary>
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormBackup));
			this.butCancel = new OpenDental.UI.Button();
			this.butSave = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBackupFolder = new System.Windows.Forms.TextBox();
			this.butBrowse = new OpenDental.UI.Button();
			this.butRestore = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkSkipBackup = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.Location = new System.Drawing.Point(519, 348);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(86, 26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butSave
			// 
			this.butSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butSave.Autosize = true;
			this.butSave.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSave.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSave.Location = new System.Drawing.Point(519, 176);
			this.butSave.Name = "butSave";
			this.butSave.Size = new System.Drawing.Size(86, 26);
			this.butSave.TabIndex = 1;
			this.butSave.Text = "Backup";
			this.butSave.Click += new System.EventHandler(this.butBackup_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(18, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(611, 60);
			this.label1.TabIndex = 2;
			this.label1.Text = @"This only backs up the current database and does not include images.  If you have images in your A to Z folder, be sure to back those up separately.  BACKUPS ARE USELESS UNLESS YOU REGULARLY VERIFY THEIR QUALITY BY TAKING A BACKUP HOME AND RESTORING IT TO YOUR HOME COMPUTER.  We suggest an inexpensive USB flash drive for this purpose.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 94);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(282, 23);
			this.label2.TabIndex = 3;
			this.label2.Text = "Default Backup Folder:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textBackupFolder
			// 
			this.textBackupFolder.Location = new System.Drawing.Point(18, 121);
			this.textBackupFolder.Name = "textBackupFolder";
			this.textBackupFolder.Size = new System.Drawing.Size(481, 20);
			this.textBackupFolder.TabIndex = 4;
			this.textBackupFolder.Text = "";
			// 
			// butBrowse
			// 
			this.butBrowse.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butBrowse.Autosize = true;
			this.butBrowse.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBrowse.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBrowse.Location = new System.Drawing.Point(519, 118);
			this.butBrowse.Name = "butBrowse";
			this.butBrowse.Size = new System.Drawing.Size(86, 26);
			this.butBrowse.TabIndex = 5;
			this.butBrowse.Text = "Browse";
			this.butBrowse.Click += new System.EventHandler(this.butBrowse_Click);
			// 
			// butRestore
			// 
			this.butRestore.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butRestore.Autosize = true;
			this.butRestore.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRestore.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRestore.Location = new System.Drawing.Point(493, 19);
			this.butRestore.Name = "butRestore";
			this.butRestore.Size = new System.Drawing.Size(86, 26);
			this.butRestore.TabIndex = 6;
			this.butRestore.Text = "Restore";
			this.butRestore.Click += new System.EventHandler(this.butRestore_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkSkipBackup);
			this.groupBox1.Controls.Add(this.butRestore);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(18, 214);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(590, 95);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Restore (takes a few minutes)";
			// 
			// checkSkipBackup
			// 
			this.checkSkipBackup.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkSkipBackup.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkSkipBackup.Location = new System.Drawing.Point(15, 31);
			this.checkSkipBackup.Name = "checkSkipBackup";
			this.checkSkipBackup.Size = new System.Drawing.Size(456, 56);
			this.checkSkipBackup.TabIndex = 7;
			this.checkSkipBackup.Text = "Faster - restore without backing up the current database first.  Do NOT use this " +
				"option on your main server, but only on a secondary computer.";
			this.checkSkipBackup.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// FormBackup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(636, 398);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butBrowse);
			this.Controls.Add(this.textBackupFolder);
			this.Controls.Add(this.butSave);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormBackup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Backup";
			this.Load += new System.EventHandler(this.FormBackup_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormBackup_Load(object sender, System.EventArgs e) {
			textBackupFolder.Text=Prefs.GetString("BackupPath");//already includes the \
			DataConnection dcon=new DataConnection();
		}

		private void butBrowse_Click(object sender, System.EventArgs e) {
			FolderBrowserDialog browserDlg=new FolderBrowserDialog();
			browserDlg.SelectedPath=textBackupFolder.Text;
			if(browserDlg.ShowDialog()==DialogResult.Cancel){
				return;
			}
			textBackupFolder.Text=browserDlg.SelectedPath;
			if(textBackupFolder.Text==Prefs.GetString("BackupPath")){
				return;//already default
			}
			if(!MsgBox.Show(this,true,"Set as default?")){
				return;
			}
			if(Prefs.UpdateString("BackupPath",textBackupFolder.Text)){
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}		
		}

		private void butBackup_Click(object sender, System.EventArgs e) {
			//only works with mysql 4.1
			SaveFileDialog saveDlg=new SaveFileDialog();
			saveDlg.OverwritePrompt=false;
			saveDlg.CheckPathExists=false;
			saveDlg.AddExtension=true;
			saveDlg.CreatePrompt=false;
			saveDlg.DefaultExt="txt";
			saveDlg.InitialDirectory=textBackupFolder.Text;
			saveDlg.FileName=FormChooseDatabase.Database+".txt";
			if(saveDlg.ShowDialog()==DialogResult.Cancel){
				return;
			}
			Cursor=Cursors.WaitCursor;
			//first, try to backup Setup.exe
			if(File.Exists(Prefs.GetString("DocPath")+"Setup.exe")){//docPath includes trailing \
				File.Copy(Prefs.GetString("DocPath")+"Setup.exe",Path.GetDirectoryName(saveDlg.FileName)+@"\DentalSetup.exe",true);
			}
			//then, save the database			
			using(StreamWriter sw=new StreamWriter(saveDlg.FileName,false)){
				sw.Write("v"+Application.ProductVersion+";");
				DataConnection dcon=new DataConnection();
				string command="FLUSH TABLES WITH READ LOCK";//locks all tables for all databases. read still allowed.
				dcon.NonQ(command);
				command="SHOW TABLES";
				DataTable table=dcon.GetTable(command);
				string[] tableName=new string[table.Rows.Count];
				for(int i=0;i<table.Rows.Count;i++){
					tableName[i]=table.Rows[i][0].ToString();
				}
				string tempFile=Prefs.GetString("DocPath")+"TempDumpFile.txt";//for one table at a time
				for(int i=0;i<tableName.Length;i++){
					//delete the tempfile in preparation for dump of the next table
					File.Delete(tempFile);//no exception if file does not exist
					//drop table not necessary, because this requires that we start with a fresh database
					//create table
					command="SHOW CREATE TABLE "+tableName[i];
					table=dcon.GetTable(command);
					sw.Write(table.Rows[0][1].ToString()+";");
					//data from table
					command="SELECT * FROM "+tableName[i]+" INTO OUTFILE '"+tempFile.Replace("\\","/")+"'";
					dcon.NonQ(command);
					using(StreamReader sr=new StreamReader(tempFile)){
						sw.Write(sr.ReadToEnd());
					}
				}
				File.Delete(tempFile);
				command="UNLOCK TABLES";
				dcon.NonQ(command);
			}
			Cursor=Cursors.Default;
			DialogResult=DialogResult.Cancel;
		}

		private void butRestore_Click(object sender, System.EventArgs e) {
			OpenFileDialog openDlg=new OpenFileDialog();
			openDlg.InitialDirectory=textBackupFolder.Text;
			if(openDlg.ShowDialog()==DialogResult.Cancel){
				return;
			}
			//make sure the version is exactly the same:
			FileStream fs=new FileStream(openDlg.FileName,FileMode.Open);
			using(BinaryReader br=new BinaryReader(fs)){
				char onechar;
				string strFileVersion="";
				while(true){
					onechar=br.ReadChar();//gets rid of "v3.7.4.0;" or similar
					if(onechar=='v'){
						continue;
					}
					if(onechar==';'){
						break;
					}
					strFileVersion+=onechar.ToString();
				}
				string strActualVersion=Application.ProductVersion;
				if(strActualVersion!=strFileVersion){
					if(File.Exists(Path.GetDirectoryName(openDlg.FileName)+@"\DentalSetup.exe")){
						if(!MsgBox.Show(this,true,"Program versions do not match.  DentalSetup.exe will now be launched from your backup media to update the program first."))
						{
							return;
						}
						Process.Start(Path.GetDirectoryName(openDlg.FileName)+@"\DentalSetup.exe");
						ExitApplicationNow.ExitNow();
						return;
					}
					else{//Setup file not found
						MessageBox.Show(Lan.g(this,"Program versions do not match.")+"\r\n"
							+Lan.g(this,"Backup is version ")+strFileVersion+"\r\n"
							+Lan.g(this,"This program is version ")+strActualVersion+"\r\n"
							+Lan.g(this,"You need to update this program to the same version as the backup.  Try getting a copy of Setup.exe from your A-Z folder on the computer where you made this backup.")
							);
						return;
					}
				}
			}
			Cursor=Cursors.WaitCursor;
			if(!checkSkipBackup.Checked){
				//create a backup of the current db before emptying it out
				ClassConvertDatabase convertdb=new ClassConvertDatabase();
				convertdb.MakeABackup();
				MsgBox.Show(this,"Current data backed up");
			}
			//we won't delete the current db, because it will be easier just to delete each table.
			DataConnection dcon=new DataConnection();
			string command="SHOW TABLES";
			DataTable table=dcon.GetTable(command);
			string[] tableNames=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				tableNames[i]=table.Rows[i][0].ToString();
			}
			for(int i=0;i<tableNames.Length;i++){
				command="DROP TABLE "+tableNames[i];
				dcon.NonQ(command);
			}
			command="ALTER DATABASE CHARACTER SET utf8";//because this is the only info that won't be part of the file.
			dcon.NonQ(command);
			fs=new FileStream(openDlg.FileName,FileMode.Open);
			using(BinaryReader br=new BinaryReader(fs)){
				char onechar;
				while(true){
					onechar=br.ReadChar();//gets rid of "v3.7.4.0;" or similar
					if(onechar==';'){
						break;
					}
				}
				string tempFile=Prefs.GetString("DocPath")+"TempDumpFile.txt";//for one table at a time
				string tableName="";
				StringBuilder createCmd=new StringBuilder();
				StringBuilder tableData=new StringBuilder();	
				while(true){
					try{
						onechar=br.ReadChar();
					}
					catch{
						FillTableWithData(tableData,tempFile,tableName);
						break;//this is the only possible place where we can break out of reading lines
					}
					if(onechar=='E' && tableData.Length>10 && tableData.ToString().Substring(tableData.Length-11,11)=="CREATE TABL"){
						tableData.Remove(tableData.Length-11,11);
						createCmd=new StringBuilder();
						createCmd.Append("CREATE TABLE");
						if(tableName!=""){//skip the first one, because no previous table to finish
							//we'll get right back to the create table command as soon as we fill the previous table
							FillTableWithData(tableData,tempFile,tableName);
							tableData=new StringBuilder();
						}
					}
					else if(createCmd.Length>0){//in the middle of reading the create command.
						createCmd.Append(onechar);
						if(onechar==';'){ //end of the create command
							tableName=createCmd.ToString().Split(new char[] {'`'},3)[1];
							//tableName will be used in FillTableWithData
							dcon.NonQ(createCmd.ToString());
							createCmd=new StringBuilder();
						}
					}
					else{
						tableData.Append(onechar);
					}
				}
				File.Delete(tempFile);
			}
			Cursor=Cursors.Default;
			DialogResult=DialogResult.OK;
		}

		private void FillTableWithData(StringBuilder tableData,string tempFile,string tableName){
			using(StreamWriter sw=new StreamWriter(tempFile,false)){//new file each time
				sw.Write(tableData.ToString());
			}
			string command="LOAD DATA INFILE '"+tempFile.Replace("\\","/")+"' INTO TABLE "+tableName;
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		/*This was a tool that I used once to compare output files for differences
		private void button1_Click(object sender, System.EventArgs e) {
			FileStream fs1=new FileStream(Prefs.GetString("DocPath")+"TempDumpFile.txt",FileMode.Open);
			//BinaryReader br1=new BinaryReader(fs1);
			FileStream fs2=new FileStream(@"C:\b\Testing\development35.txt",FileMode.Open);//1 byte larger
			//BinaryReader br2=new BinaryReader(fs2);
			MessageBox.Show("lengths: "+fs1.Length.ToString()+" "+fs2.Length.ToString());
			string leadup="";
			//try{
				int byte1;
				int byte2;
				while(true){
					byte1=fs1.ReadByte();
					byte2=fs2.ReadByte();
					//if(fs1.Position>106057 && fs1.Position<106088){
					//	leadup+=(char)byte1;
					//}
					if(byte1!=byte2){
						MessageBox.Show("mismatch at "+fs1.Position.ToString());
						//MessageBox.Show("leadup:"+leadup);
						MessageBox.Show(byte1.ToString()+" "+byte2.ToString());
						//break;
						byte1=fs1.ReadByte();
						byte2=fs2.ReadByte();
						MessageBox.Show("trailing: "+byte1.ToString()+" "+byte2.ToString());
					}
					//if(fs1.Position==106088){
						
					//}
				}
			//}
			//catch(Exception ex){
			//	MessageBox.Show(ex.Message);
				fs1.Close();
				fs2.Close();
			//}
		}*/

		

		

	}
}





















