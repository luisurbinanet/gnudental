using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace OpenDental{
	///<summary></summary>
	public class FormConfig : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textComputerName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textPassword;
		private System.Windows.Forms.TextBox textUser;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		///<summary></summary>
    public static string ComputerName;
		///<summary></summary>
    public static string Database;
		///<summary></summary>
    public static string User;
		///<summary></summary>
    public static string Password;
		//public static int Port;
		private string originalComputerName;
    private string originalDatabase;
    private string originalUser;
    private string originalPassword;
		//private int originalPort;
    //private FormPreferences FormPreferences2 = new FormPreferences();
		private System.Windows.Forms.TextBox textDatabase;
		private System.Windows.Forms.Label label6;
		///<summary></summary>
		public bool IsInStartup;
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormConfig(){
			InitializeComponent();
			//textPort.MaxVal=System.Int32.MaxValue;
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label1,
				this.label2,
				this.label3,
				this.label4,
				this.label6,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.textComputerName = new System.Windows.Forms.TextBox();
			this.textPassword = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textUser = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textDatabase = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 70);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(582, 46);
			this.label1.TabIndex = 0;
			this.label1.Text = "Computer Name: The name of the computer where the MySQL server and database are l" +
				"ocated.  If you are running Open Dental on a single computer only, then the comp" +
				"uter name may be localhost.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textComputerName
			// 
			this.textComputerName.Location = new System.Drawing.Point(12, 120);
			this.textComputerName.Name = "textComputerName";
			this.textComputerName.Size = new System.Drawing.Size(280, 20);
			this.textComputerName.TabIndex = 0;
			this.textComputerName.Text = "";
			// 
			// textPassword
			// 
			this.textPassword.Location = new System.Drawing.Point(12, 314);
			this.textPassword.Name = "textPassword";
			this.textPassword.Size = new System.Drawing.Size(280, 20);
			this.textPassword.TabIndex = 3;
			this.textPassword.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(10, 276);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(588, 34);
			this.label2.TabIndex = 2;
			this.label2.Text = "Password: For new installations, the password will be blank.  Only change it here" +
				" if you have already changed it in MySQL first.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textUser
			// 
			this.textUser.Location = new System.Drawing.Point(12, 246);
			this.textUser.Name = "textUser";
			this.textUser.Size = new System.Drawing.Size(280, 20);
			this.textUser.TabIndex = 2;
			this.textUser.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(10, 210);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(592, 32);
			this.label3.TabIndex = 4;
			this.label3.Text = "User: When MySQL is first installed, the user is root.  If your network is expose" +
				"d, you may need to change this, but you need to change it in MySQL first and the" +
				"n here.  See the manual for more details.";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textDatabase
			// 
			this.textDatabase.Location = new System.Drawing.Point(12, 180);
			this.textDatabase.Name = "textDatabase";
			this.textDatabase.Size = new System.Drawing.Size(280, 20);
			this.textDatabase.TabIndex = 1;
			this.textDatabase.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(10, 156);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(582, 18);
			this.label4.TabIndex = 6;
			this.label4.Text = "DataBase: usually opendental unless you changed the name.";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(524, 392);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 5;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(524, 428);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 6;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(14, 10);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(562, 44);
			this.label6.TabIndex = 12;
			this.label6.Text = "The values below will only be used on this computer.  They have to be set on each" +
				" computer.";
			// 
			// FormConfig
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(616, 472);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textDatabase);
			this.Controls.Add(this.textUser);
			this.Controls.Add(this.textPassword);
			this.Controls.Add(this.textComputerName);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormConfig";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MySQL Client Configuration";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormConfig_Closing);
			this.Load += new System.EventHandler(this.FormConfig_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormConfig_Load(object sender, System.EventArgs e) {
			if(!IsInStartup && !UserPermissions.CheckUserPassword("MySQL Config")){
				MessageBox.Show(Lan.g(this,"You do not have permission for this feature."));
				DialogResult=DialogResult.Cancel;
				return;
			}
			originalComputerName=ComputerName;
			originalDatabase=Database;
			originalUser=User;
			originalPassword=Password;
			//originalPort=Port;
			GetConfig();
		}

		///<summary></summary>
		public void GetConfig(){
			XmlDocument document=new XmlDocument();
			if(!File.Exists("FreeDentalConfig.xml")){
				File.Create("FreeDentalConfig.xml");
				textComputerName.Text="localhost";
				textDatabase.Text="opendental";
				textUser.Text="root";
				//textPort.Text="3306";
				return;
			}
			try{
				document.Load("FreeDentalConfig.xml");
				XmlNodeReader reader=new XmlNodeReader(document);
				string currentElement="";
				while(reader.Read()){
					if(reader.NodeType==XmlNodeType.Element){
						currentElement=reader.Name;
					}
					else if(reader.NodeType==XmlNodeType.Text){
						switch(currentElement){
							case "ComputerName":
								ComputerName=reader.Value;
								textComputerName.Text=ComputerName;
								break;
							case "Database":
								Database=reader.Value;
								textDatabase.Text=Database;
								break;
							case "User":
								User=reader.Value;
								textUser.Text=User;
								break;
							case "Password":
								Password=reader.Value;
								textPassword.Text=Password;
								break;
							//case "Port":
								//MessageBox.Show(reader.Value);
							//	try{
							//		Port=Convert.ToInt32(reader.Value);
							//	}
							//	catch{
							//		Port=3306;
							//	}
								//MessageBox.Show(Port.ToString());
							//	textPort.Text=Port.ToString();
							//	break;
						}
					}
					
				}

				reader.Close();
			}
			catch{
				textComputerName.Text="localhost";
				textDatabase.Text="opendental";
				textUser.Text="root";
				//textPort.Text="3306";
			}
		}

		private void ResetToOriginal(){
			ComputerName=originalComputerName;
			Database=originalDatabase;
			User=originalUser;
			Password=originalPassword;
			//Port=originalPort;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//if(textPort.errorProvider1.GetError(textPort)!=""){
			//	MessageBox.Show(Lan.g(this,"Please fix data entry errors first"));
			//	return;
			//}
			if(!IsInStartup && MessageBox.Show
				(Lan.g(this,"Are you sure? You will have to close the program and then reopen it after changes are saved.")
				,"",MessageBoxButtons.OKCancel)
				!=DialogResult.OK){
				return;
			}
			ComputerName=textComputerName.Text;
			Database=textDatabase.Text;
			User=textUser.Text;
			Password=textPassword.Text;
			//Port=PIn.PInt(textPort.Text);
      DataClass.SetConnection();
			if(!Prefs.DBExists()){
        MessageBox.Show(Lan.g(this,"Database not present.  Could not establish connection."));
        return;
      }
      if(!Prefs.TryToConnect()){
        MessageBox.Show(Lan.g(this,"Database is present, but is not accepting commands."));
        return;
      }
			XmlWriter xmlwriter=new XmlTextWriter("FreeDentalConfig.xml",System.Text.Encoding.Default);
			xmlwriter.WriteStartElement("DatabaseConnection");
			xmlwriter.WriteWhitespace("\r\n\t");
			xmlwriter.WriteElementString("ComputerName", textComputerName.Text);
			xmlwriter.WriteWhitespace("\r\n\t");
			xmlwriter.WriteElementString("Database",textDatabase.Text);
			xmlwriter.WriteWhitespace("\r\n\t");
			xmlwriter.WriteElementString("User",textUser.Text);
			xmlwriter.WriteWhitespace("\r\n\t");
			xmlwriter.WriteElementString("Password", textPassword.Text);
			xmlwriter.WriteWhitespace("\r\n");
			//xmlwriter.WriteElementString("Port", textPort.Text);
			xmlwriter.WriteEndElement();
			xmlwriter.Close();
			if(!IsInStartup){
				MessageBox.Show(Lan.g(this,"You must close Open Dental now, and reopen it."));
				SecurityLogs.MakeLogEntry("MySQL Config","FreeDentalConfig.xml has been changed");
			}
			DialogResult=DialogResult.OK;
    }

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormConfig_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.Cancel){
				ResetToOriginal();
			}
		}
		
  }
}
