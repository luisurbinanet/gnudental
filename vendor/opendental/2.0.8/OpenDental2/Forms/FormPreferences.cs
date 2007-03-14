/*=============================================================================================================
FreeDental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Data;
using System.Text; 
using System.Runtime.InteropServices;


namespace OpenDental{

	public class FormPreferences : System.Windows.Forms.Form{
		private System.ComponentModel.Container components = null;
		public static string DataPath;
		public static string DataBase;
		public static string User;
		public static string Password;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textExtra1;
		private System.Windows.Forms.TextBox textExtra2;
		private System.Windows.Forms.Label label4;
		public int SelectedTab;

		public FormPreferences(){
			InitializeComponent();//required for Form Designer support
			Lan.C(this, new System.Windows.Forms.Control[] {
				label1,
				label2,
				label3,
				label4,
				groupBox1,
				buttonClose,
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonClose = new System.Windows.Forms.Button();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.label3 = new System.Windows.Forms.Label();
			this.textExtra1 = new System.Windows.Forms.TextBox();
			this.textExtra2 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(672, 580);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(76, 24);
			this.buttonClose.TabIndex = 1;
			this.buttonClose.Text = "Close";
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// trackBar1
			// 
			this.trackBar1.Location = new System.Drawing.Point(44, 24);
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(104, 45);
			this.trackBar1.TabIndex = 2;
			this.trackBar1.Value = 5;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																																						this.label1,
																																						this.trackBar1,
																																						this.label2});
			this.groupBox1.Location = new System.Drawing.Point(10, 598);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(192, 76);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Adjust contrast after each scan:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(152, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(16, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "Hi";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(20, 28);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(20, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "Lo";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(28, 92);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(162, 18);
			this.label3.TabIndex = 5;
			this.label3.Text = "Family module Extra1 caption";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textExtra1
			// 
			this.textExtra1.Location = new System.Drawing.Point(196, 88);
			this.textExtra1.Name = "textExtra1";
			this.textExtra1.Size = new System.Drawing.Size(164, 20);
			this.textExtra1.TabIndex = 6;
			this.textExtra1.Text = "";
			this.textExtra1.TextChanged += new System.EventHandler(this.textExtra1_TextChanged);
			// 
			// textExtra2
			// 
			this.textExtra2.Location = new System.Drawing.Point(196, 112);
			this.textExtra2.Name = "textExtra2";
			this.textExtra2.Size = new System.Drawing.Size(164, 20);
			this.textExtra2.TabIndex = 8;
			this.textExtra2.Text = "";
			this.textExtra2.TextChanged += new System.EventHandler(this.textExtra2_TextChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(28, 116);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(162, 18);
			this.label4.TabIndex = 7;
			this.label4.Text = "Family module Extra2 caption";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormPreferences
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(930, 704);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																																	this.textExtra2,
																																	this.label4,
																																	this.textExtra1,
																																	this.label3,
																																	this.buttonClose,
																																	this.groupBox1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPreferences";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = Lan.g(this,"Misc Preferences");
			this.Load += new System.EventHandler(this.FormPreferences_Load);
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

//*******************************************This Form is obsolete and is being replaced.************

		//public void GetPaths(){//getpaths runs on startup of FormOpenDental
			//ini file should read as follows:
			/*
			PrimaryDatabasePath=localhost
			DataBase=freedental
			user=root
			password=
			*/
			/*
      XmlDocument document = new XmlDocument();
      document.Load("FreeDentalConfig.xml");
      XmlNodeReader reader = new XmlNodeReader(document);
      while(reader.Read()){
        if(reader.NodeType==XmlNodeType.Element){
          if(reader.Name == "ComputerName"){
            reader.Read();
            DataPath=reader.Value;
          }
          else if(reader.Name == "Database"){
            reader.Read();
            DataBase=reader.Value;
          }
          else if(reader.Name == "User"){
            reader.Read();
            User=reader.Value;
          }
          else if(reader.Name == "Password"){
            reader.Read();
            Password=reader.Value;
          }
					else if(reader.Name == "Port"){
            reader.Read();
            Port=reader.Value;
          }
        }
      }
      reader.Close(); */

			/*
			StreamReader sr=new StreamReader(@"freedental.ini");
			priDataPath=sr.ReadLine().Substring(20);
			DataBase   =sr.ReadLine().Substring(9);
			User       =sr.ReadLine().Substring(5);
			Password   =sr.ReadLine().Substring(9);
			sr.Close();
*/
			//usingSecondary=false;
//			DataPath=priDataPath;
			//MessageBox.Show(DataBase);
			//imagePath="";
			//MessageBox.Show(FormPreferences.User+"-"+FormPreferences.Password+"--g-");
		//}

		private void FormPreferences_Load(object sender, System.EventArgs e) {
			//this.textExtra1.Text=((Pref)Prefs.HList["FamilyExtra1"]).ValueString;
			//this.textExtra2.Text=((Pref)Prefs.HList["FamilyExtra2"]).ValueString;
		}
	
		private void buttonClose_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void textExtra1_TextChanged(object sender, System.EventArgs e) {
			//Prefs.Cur=(Pref)Prefs.HList["FamilyExtra1"];
			//Prefs.UpdateCur();
		}

		private void textExtra2_TextChanged(object sender, System.EventArgs e) {
			//Prefs.Cur=(Pref)Prefs.HList["FamilyExtra1"];
			//Prefs.UpdateCur();
		}


	}
}
