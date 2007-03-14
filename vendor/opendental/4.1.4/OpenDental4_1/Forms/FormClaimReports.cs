using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.Eclaims;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormClaimReports : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboClearhouse;
		private OpenDental.UI.Button butArchive;
		private OpenDental.UI.Button butRetrieve;
		private System.Windows.Forms.ListBox listMain;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormClaimReports()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormClaimReports));
			this.butClose = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.comboClearhouse = new System.Windows.Forms.ComboBox();
			this.listMain = new System.Windows.Forms.ListBox();
			this.butArchive = new OpenDental.UI.Button();
			this.butRetrieve = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.Location = new System.Drawing.Point(393, 515);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(15, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(405, 20);
			this.label1.TabIndex = 1;
			this.label1.Text = "These reports are generated by the clearinghouses. ";
			// 
			// comboClearhouse
			// 
			this.comboClearhouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClearhouse.Location = new System.Drawing.Point(18, 32);
			this.comboClearhouse.Name = "comboClearhouse";
			this.comboClearhouse.Size = new System.Drawing.Size(187, 21);
			this.comboClearhouse.TabIndex = 2;
			this.comboClearhouse.SelectedIndexChanged += new System.EventHandler(this.comboClearhouse_SelectedIndexChanged);
			// 
			// listMain
			// 
			this.listMain.Location = new System.Drawing.Point(18, 72);
			this.listMain.Name = "listMain";
			this.listMain.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listMain.Size = new System.Drawing.Size(450, 420);
			this.listMain.TabIndex = 3;
			this.listMain.DoubleClick += new System.EventHandler(this.listMain_DoubleClick);
			// 
			// butArchive
			// 
			this.butArchive.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butArchive.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butArchive.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butArchive.Location = new System.Drawing.Point(17, 514);
			this.butArchive.Name = "butArchive";
			this.butArchive.Size = new System.Drawing.Size(87, 26);
			this.butArchive.TabIndex = 4;
			this.butArchive.Text = "Archive";
			this.butArchive.Click += new System.EventHandler(this.butArchive_Click);
			// 
			// butRetrieve
			// 
			this.butRetrieve.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRetrieve.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRetrieve.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRetrieve.Location = new System.Drawing.Point(222, 29);
			this.butRetrieve.Name = "butRetrieve";
			this.butRetrieve.Size = new System.Drawing.Size(90, 26);
			this.butRetrieve.TabIndex = 5;
			this.butRetrieve.Text = "Retrieve";
			this.butRetrieve.Click += new System.EventHandler(this.butRetrieve_Click);
			// 
			// FormClaimReports
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(505, 564);
			this.Controls.Add(this.butRetrieve);
			this.Controls.Add(this.butArchive);
			this.Controls.Add(this.listMain);
			this.Controls.Add(this.comboClearhouse);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClaimReports";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "E-claim Reports";
			this.Load += new System.EventHandler(this.FormClaimReports_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormClaimReports_Load(object sender, System.EventArgs e) {
			for(int i=0;i<Clearinghouses.List.Length;i++){
				comboClearhouse.Items.Add(Clearinghouses.List[i].Description);
				if(Clearinghouses.List[i].IsDefault){
					comboClearhouse.SelectedIndex=i;
				}
			}
			if(comboClearhouse.Items.Count>0
				&& comboClearhouse.SelectedIndex==-1)
			{
				comboClearhouse.SelectedIndex=0;
			}
			FillGrid();
		}

		private void comboClearhouse_SelectedIndexChanged(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			listMain.Items.Clear();
			if(comboClearhouse.Items.Count==0){
				return;
			}
			if(!Directory.Exists(Clearinghouses.List[comboClearhouse.SelectedIndex].ResponsePath)){
				//MsgBox.Show(this,"Clearinghouse does not have a valid Report Path set.");
				return;
			}
			listMain.Items.AddRange(
				Directory.GetFiles(Clearinghouses.List[comboClearhouse.SelectedIndex].ResponsePath));
		}

		private void butRetrieve_Click(object sender, System.EventArgs e) {
			if(comboClearhouse.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a clearinghouse first.");
				return;
			}
			if(Clearinghouses.List[comboClearhouse.SelectedIndex].CommBridge
				==EclaimsCommBridge.None
				|| Clearinghouses.List[comboClearhouse.SelectedIndex].CommBridge
				==EclaimsCommBridge.Renaissance
				|| Clearinghouses.List[comboClearhouse.SelectedIndex].CommBridge
				==EclaimsCommBridge.RECS)
			{
				MsgBox.Show
					(this,"No built-in functionality for retrieving reports from this clearinghouse.");
				return;
			}
			if(!MsgBox.Show(this,true,"Connect to clearinghouse to retrieve reports?")){
				return;
			}
			else if(Clearinghouses.List[comboClearhouse.SelectedIndex].CommBridge
				==EclaimsCommBridge.WebMD)
			{
				if(!WebMD.Launch(Clearinghouses.List[comboClearhouse.SelectedIndex],0)){
					MessageBox.Show(Lan.g(this,"Error retrieving."));
					return;
				}
				//MsgBox.Show
				//	(this,"Reports from WebMD are retrieved every time you send the next batch of claims.");
				//return;
			}
			else if(Clearinghouses.List[comboClearhouse.SelectedIndex].CommBridge
				==EclaimsCommBridge.BCBSGA)
			{
				if(!BCBSGA.Retrieve(Clearinghouses.List[comboClearhouse.SelectedIndex])){
					MessageBox.Show(Lan.g(this,"Error retrieving."));
					return;
				}
			}
			else if(Clearinghouses.List[comboClearhouse.SelectedIndex].CommBridge
				==EclaimsCommBridge.ClaimConnect)
			{ //Added SPK 5/05
				try
				{
					Process.Start(@"http://www.dentalxchange.com/newdxc");
				}
				catch
				{
					MessageBox.Show("Could not locate the site.");
				}
				return;
			}
			else if(Clearinghouses.List[comboClearhouse.SelectedIndex].CommBridge
				==EclaimsCommBridge.AOS)
			{ //Added SPK 7/05
				try
				{
					Process.Start(@"C:\Program files\AOS\AOSCommunicator\AOSCommunicator.exe");
				}
				catch
				{
					MessageBox.Show("Could not locate the file.");
				}
				return;
			}
			MsgBox.Show(this,"Retrieval successful");
			FillGrid();
		}

		private void butArchive_Click(object sender, System.EventArgs e) {
			if(listMain.Items.Count==0){
				MsgBox.Show(this,"There are no reports to archive.");
				return;
			}
			if(listMain.SelectedIndices.Count==0){
				for(int i=0;i<listMain.Items.Count;i++){
					listMain.SetSelected(i,true);
				}
			}
			if(!MsgBox.Show(this,true,"Move selected reports to the archive folder?")){
				return;
			}
			string respPath=Clearinghouses.List[comboClearhouse.SelectedIndex].ResponsePath;
			for(int i=0;i<listMain.SelectedIndices.Count;i++){
				ArchiveFile((string)listMain.Items[listMain.SelectedIndices[i]]);
			}
			FillGrid();
		}

		///<summary>Supply the fileName including the full path. Moves it to an Archive folder within the original folder, renaming as necessary to avoid duplicate.</summary>
		private void ArchiveFile(string fileName){
			//original fileName actually includes full path
			string respPath=Path.GetDirectoryName(fileName)+"\\";
			if(!Directory.Exists(respPath+"Archive")){
				Directory.CreateDirectory(respPath+"Archive");
			}
			//find a unique filename
			string oldFileName=Path.GetFileNameWithoutExtension(fileName);//no extens
			string extens=Path.GetExtension(fileName);
			string newFileName=oldFileName+DateTime.Today.ToString("yyyy_MM_dd");//no extens
			string uniqueString="";
			int i=0;
			while(File.Exists(respPath+"Archive\\"+newFileName+uniqueString+extens)){
				i++;
				uniqueString="("+i.ToString()+")";
			}
			//string oldpath=fileName;
			//string newpath=respPath+"Archive\\"+newFileName+uniqueString+extens;
			File.Move(fileName,respPath+"Archive\\"+newFileName+uniqueString+extens);
		}

		private void listMain_DoubleClick(object sender, System.EventArgs e) {
			if(listMain.SelectedIndices.Count==0){
				return;
			}
			//if the file is an X12 file (277 for now), then display it differently
			if(Path.GetExtension((string)listMain.SelectedItem)==".txt"){
				string firstLine="";
				using(StreamReader sr=new StreamReader((string)listMain.SelectedItem)){
					firstLine=sr.ReadLine();
				}
				if(firstLine!=null && firstLine.Length==106 && firstLine.Substring(0,3)=="ISA"){
					//try{
						string humanText=X277U.MakeHumanReadable((string)listMain.SelectedItem);
						ArchiveFile((string)listMain.SelectedItem);
						//now the file will be gone
						//create a new file from humanText with same name as original file
						StreamWriter sw=File.CreateText((string)listMain.SelectedItem);
						sw.Write(humanText);
						sw.Close();
						//now, it will try to launch the new text file
					//}
					//catch(Exception ex){
					//	MessageBox.Show(ex.Message);
					//	return;
					//}
				}
			}
			try{
				Process.Start((string)listMain.SelectedItem);
			}
			catch{
				MsgBox.Show(this,"Could not open the item. You could try open it directly from the folder where it is located.");
			}
			//FillGrid();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		

		

		


	}
}




















