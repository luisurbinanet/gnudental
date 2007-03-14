using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace OpenDental{
	///<summary></summary>
	public class FormChooseDatabase : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textPassword;
		private System.Windows.Forms.TextBox textUser;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		///<summary></summary>
    private static string ComputerName;
		///<summary>The name of the database currently is use.</summary>
    public static string Database;
		///<summary></summary>
    private static string DbUser;
		///<summary></summary>
    private static string Password;
		//public static int Port;
		private string originalComputerName;
    private string originalDatabase;
    private string originalUser;
    private string originalPassword;
		//<summary></summary>
		//public bool IsInStartup;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label7;
		private OpenDental.UI.Button butStart;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label5;
		private System.ComponentModel.Container components = null;
		private string mysqlInstalled;
		private System.Windows.Forms.Label labelStatus;
		private System.Windows.Forms.ComboBox comboComputerName;
		private System.Windows.Forms.ComboBox comboDatabase;
		private bool mysqlIsStarted;
		private CheckBox checkNoShow;
		//<summary>user lowercase is for internal OD security.  this.DbUser capital is for the MySQL user.  Don't get them confused.</summary>
		//private User user;
		///<summary></summary>
		public bool NoShow;

		///<summary></summary>
		public FormChooseDatabase(){
			InitializeComponent();
			//textPort.MaxVal=System.Int32.MaxValue;
			Lan.F(this);
			//Lan.C(this, new System.Windows.Forms.Control[] {
			//	textNotInstalled
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChooseDatabase));
			this.label1 = new System.Windows.Forms.Label();
			this.textPassword = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textUser = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.comboDatabase = new System.Windows.Forms.ComboBox();
			this.comboComputerName = new System.Windows.Forms.ComboBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.butStart = new OpenDental.UI.Button();
			this.labelStatus = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.checkNoShow = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(40,20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(582,46);
			this.label1.TabIndex = 0;
			this.label1.Text = "Computer Name: The name of the computer where the MySQL server and database are l" +
    "ocated.  If you are running Open Dental on a single computer only, then the comp" +
    "uter name may be localhost.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textPassword
			// 
			this.textPassword.Location = new System.Drawing.Point(42,260);
			this.textPassword.Name = "textPassword";
			this.textPassword.Size = new System.Drawing.Size(280,20);
			this.textPassword.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(40,222);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(588,34);
			this.label2.TabIndex = 2;
			this.label2.Text = "Password: For new installations, the password will be blank.  You probably don\'t " +
    "need to change this.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textUser
			// 
			this.textUser.Location = new System.Drawing.Point(42,192);
			this.textUser.Name = "textUser";
			this.textUser.Size = new System.Drawing.Size(280,20);
			this.textUser.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(40,156);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(592,32);
			this.label3.TabIndex = 4;
			this.label3.Text = "User: When MySQL is first installed, the user is root.  You probably don\'t need t" +
    "o change this.";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(40,100);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(582,26);
			this.label4.TabIndex = 6;
			this.label4.Text = "DataBase: usually opendental unless you changed the name.";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(602,506);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,25);
			this.butOK.TabIndex = 5;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(602,542);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,25);
			this.butCancel.TabIndex = 6;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.comboDatabase);
			this.groupBox1.Controls.Add(this.comboComputerName);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.textPassword);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.textUser);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(16,16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(660,298);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Connection Settings - These values will only be used on this computer.  They have" +
    " to be set on each computer";
			// 
			// comboDatabase
			// 
			this.comboDatabase.Location = new System.Drawing.Point(42,130);
			this.comboDatabase.MaxDropDownItems = 100;
			this.comboDatabase.Name = "comboDatabase";
			this.comboDatabase.Size = new System.Drawing.Size(280,21);
			this.comboDatabase.TabIndex = 9;
			// 
			// comboComputerName
			// 
			this.comboComputerName.Location = new System.Drawing.Point(42,72);
			this.comboComputerName.MaxDropDownItems = 100;
			this.comboComputerName.Name = "comboComputerName";
			this.comboComputerName.Size = new System.Drawing.Size(280,21);
			this.comboComputerName.TabIndex = 8;
			this.comboComputerName.Leave += new System.EventHandler(this.comboComputerName_Leave);
			this.comboComputerName.SelectionChangeCommitted += new System.EventHandler(this.comboComputerName_SelectionChangeCommitted);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.butStart);
			this.groupBox2.Controls.Add(this.labelStatus);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(16,326);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(660,160);
			this.groupBox2.TabIndex = 16;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "MySQL service";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(260,98);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(386,54);
			this.label5.TabIndex = 21;
			this.label5.Text = resources.GetString("label5.Text");
			// 
			// butStart
			// 
			this.butStart.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butStart.Autosize = true;
			this.butStart.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butStart.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butStart.Location = new System.Drawing.Point(172,108);
			this.butStart.Name = "butStart";
			this.butStart.Size = new System.Drawing.Size(75,25);
			this.butStart.TabIndex = 20;
			this.butStart.Text = "Stop";
			this.butStart.Click += new System.EventHandler(this.butStart_Click);
			// 
			// labelStatus
			// 
			this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelStatus.Location = new System.Drawing.Point(100,112);
			this.labelStatus.Name = "labelStatus";
			this.labelStatus.Size = new System.Drawing.Size(72,18);
			this.labelStatus.TabIndex = 19;
			this.labelStatus.Text = "Started";
			this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(14,112);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(88,18);
			this.label8.TabIndex = 18;
			this.label8.Text = "Status:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8,24);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(628,20);
			this.label7.TabIndex = 0;
			this.label7.Text = "(A service is just a kind of program that runs in the background without any wind" +
    "ow)";
			// 
			// checkNoShow
			// 
			this.checkNoShow.AutoSize = true;
			this.checkNoShow.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNoShow.Location = new System.Drawing.Point(16,542);
			this.checkNoShow.Name = "checkNoShow";
			this.checkNoShow.Size = new System.Drawing.Size(294,18);
			this.checkNoShow.TabIndex = 17;
			this.checkNoShow.Text = "Do not show this window on startup (this computer only)";
			this.checkNoShow.UseVisualStyleBackColor = true;
			// 
			// FormChooseDatabase
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(708,582);
			this.Controls.Add(this.checkNoShow);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormChooseDatabase";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Choose Database";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormConfig_Closing);
			this.Load += new System.EventHandler(this.FormConfig_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormConfig_Load(object sender, System.EventArgs e) {
			originalComputerName=ComputerName;
			originalDatabase=Database;
			originalUser=DbUser;
			originalPassword=Password;
			GetConfig();
			FillComboComputerNames();
			FillComboDatabases();
			FillService();
		}

		///<summary></summary>
		public static string GetConnectionString(){
			return "Server="+ComputerName
				+";Database="+Database
				+";User ID="+DbUser
				+";Password="+Password
				+";CharSet=utf8";
		}

		///<summary>Sets the connection to an alternate database for backup purposes.  Currently only used during conversions to do a quick backup first, and in FormChooseDatabase to get db names.</summary>
		public static string GetAlternateConnStr(string db){
			return "Server="+ComputerName
				+";Database="+db
				+";User ID="+DbUser
				+";Password="+Password
				+";CharSet=utf8";
		}

		///<summary>Gets a list of all computer names on the network (this is not easy)</summary>
		private string[] GetComputerNames(){
			try{
				ArrayList retList=new ArrayList();
				//string myAdd=Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();//obsolete
				string myAdd=Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
				ProcessStartInfo psi=new ProcessStartInfo();
				psi.FileName=@"C:\WINDOWS\system32\cmd.exe";//Path for the cmd prompt
				psi.Arguments="/c net view > tempCompNames.txt";//Arguments for the command prompt
				//"/c" tells it to run the following command which is "net view > tempCompNames.txt"
				//"net view" lists all the computers on the network
				//" > tempCompNames.txt" tells dos to put the results in a file called tempCompNames.txt
				psi.WindowStyle=ProcessWindowStyle.Hidden;//Hide the window
				Process.Start(psi);
				StreamReader sr;
				string filename=Application.StartupPath+"\\tempCompNames.txt";
				while(true){
					Thread.Sleep(200);//sleep for 1/5 second
					if(File.Exists(filename)){
						try{
							sr=new StreamReader(filename);
							break;
						}
						catch{
						}
					}
				}
				while(!sr.ReadLine().StartsWith("--")){
					//The line just before the data looks like: --------------------------
				}
				string line="";
				retList.Add("localhost");
				while(true){
					line=sr.ReadLine();
					if(line.StartsWith("The"))//cycle until we reach,"The command completed successfully."
						break;
					line=line.Split(char.Parse(" "))[0];// Split the line after the first space
					// Normally, in the file it lists it like this
					// \\MyComputer                 My Computer's Description
					// Take off the slashes, "\\MyComputer" to "MyComputer"
					retList.Add(line.Substring(2,line.Length-2));
				}
				sr.Close();
				File.Delete(Application.StartupPath+"\\tempCompNames.txt");
				string[] retArray=new string[retList.Count];
				retList.CopyTo(retArray);
				return retArray;
			}
			catch{//it will always fail if not WinXP
				return new string[0];
			}
		}

		///<summary>Gets a list of all computer names on the network (this is not easy)</summary>
		private string[] GetDatabases(){
			ComputerName=comboComputerName.Text;
			DbUser=textUser.Text;
			Password=textPassword.Text;
			DataConnection dcon=new DataConnection("mysql");//use the one table that we know exists
			if(!dcon.IsValid()){
				return new string[0];
			}
			string command="SHOW DATABASES";
			//if this next step fails, table will simply have 0 rows
			DataTable table=dcon.GetTable(command);
			string[] dbNames=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				dbNames[i]=table.Rows[i][0].ToString();
			}
			return dbNames;
		}

		private void FillComboComputerNames(){
			comboComputerName.Items.Clear();
			comboComputerName.Items.AddRange(GetComputerNames());
		}

		private void FillComboDatabases(){
			comboDatabase.Items.Clear();
			comboDatabase.Items.AddRange(GetDatabases());
		}

		private void comboComputerName_SelectionChangeCommitted(object sender, System.EventArgs e) {
			FillComboDatabases();
		}

		private void comboComputerName_Leave(object sender, System.EventArgs e) {
			FillComboDatabases();
		}

		private void FillService(){
			ServiceController[] services=ServiceController.GetServices();
			mysqlInstalled="";
			for(int i=0;i<services.Length;i++){
				if(services[i].ServiceName=="MySql"
					|| services[i].ServiceName=="MySQL"){
					mysqlInstalled=services[i].ServiceName;
					break;
				}
			}
			if(mysqlInstalled==""){
				labelStatus.Text="";
				butStart.Text="Start";
				butStart.Enabled=false;
				return;
			}
			butStart.Enabled=true;
			ServiceController sc=new ServiceController(mysqlInstalled);
			if(sc.Status.Equals(ServiceControllerStatus.Running)){
				mysqlIsStarted=true;
				labelStatus.Text="Started";
				butStart.Text="Stop";
			}
			else{
				mysqlIsStarted=false;
				labelStatus.Text="Stopped";
				butStart.Text="Start";
			}
		}

		private void butStart_Click(object sender, System.EventArgs e) {
			//this button won't even be available unless the service is installed
			//but the service might be present and unable to start, so must use try.
			Cursor=Cursors.WaitCursor;
			ServiceController sc=new ServiceController(mysqlInstalled);
			if(mysqlIsStarted){
				sc.Stop();
				sc.WaitForStatus(ServiceControllerStatus.Stopped,new TimeSpan(0,0,10));
				Cursor=Cursors.Default;
				if(sc.Status!=ServiceControllerStatus.Stopped){
					MessageBox.Show("Unable to stop the MySQL service.");
				}
			}
			else{
				try{
					sc.Start();
					sc.WaitForStatus(ServiceControllerStatus.Running,new TimeSpan(0,0,10));
				}
				catch{
					//
				}
				Cursor=Cursors.Default;
				if(sc.Status!=ServiceControllerStatus.Running){
					MessageBox.Show("Unable to start the MySQL service.");
				}
			}
			FillService();
		}

		///<summary></summary>
		public void GetConfig(){
			XmlDocument document=new XmlDocument();
			if(!File.Exists("FreeDentalConfig.xml")){
				File.Create("FreeDentalConfig.xml");
				comboComputerName.Text="localhost";
				#if(TRIALONLY)
					comboDatabase.Text="demo";
				#else
					comboDatabase.Text="opendental";
				#endif
				textUser.Text="root";
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
								comboComputerName.Text=ComputerName;
								break;
							case "Database":
								Database=reader.Value;
								comboDatabase.Text=Database;
								break;
							case "User":
								DbUser=reader.Value;
								textUser.Text=DbUser;
								break;
							case "Password":
								Password=reader.Value;
								textPassword.Text=Password;
								break;
							case "NoShowOnStartup":
								if(reader.Value=="True") {
									NoShow=true;
									checkNoShow.Checked=true;
								}
								else {
									NoShow=false;
									checkNoShow.Checked=false;
								}
								break;
						}
					}
				}
				reader.Close();
			}
			catch{//Exception e) {
				//MessageBox.Show(e.Message);
				comboComputerName.Text="localhost";
				comboDatabase.Text="opendental";
				textUser.Text="root";
			}
		}

		private void ResetToOriginal(){
			ComputerName=originalComputerName;
			Database=originalDatabase;
			DbUser=originalUser;
			Password=originalPassword;
			DataClass.SetConnection();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			ComputerName=comboComputerName.Text;
			Database=comboDatabase.Text;
			DbUser=textUser.Text;
			Password=textPassword.Text;
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
			xmlwriter.WriteRaw("<?xml version=\"1.0\"?>");
			xmlwriter.WriteWhitespace("\r\n");
			xmlwriter.WriteStartElement("DatabaseConnection");
			xmlwriter.WriteWhitespace("\r\n\t");
			xmlwriter.WriteElementString("ComputerName",comboComputerName.Text);
			xmlwriter.WriteWhitespace("\r\n\t");
			xmlwriter.WriteElementString("Database",comboDatabase.Text);
			xmlwriter.WriteWhitespace("\r\n\t");
			xmlwriter.WriteElementString("User",textUser.Text);
			xmlwriter.WriteWhitespace("\r\n\t");
			xmlwriter.WriteElementString("Password",textPassword.Text);
			xmlwriter.WriteWhitespace("\r\n\t");
			xmlwriter.WriteElementString("NoShowOnStartup",checkNoShow.Checked.ToString());
			xmlwriter.WriteWhitespace("\r\n");
			xmlwriter.WriteEndElement();
			xmlwriter.Close();
			//fyiReporting.RDL.DataSource.SetOpenDentalConnectionString(
			//	"Server="+ComputerName+";Database="+Database+";User ID="+DbUser+";Password="+Password+";CharSet=utf8");
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