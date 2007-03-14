/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
//#define ISXP
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text; 
using System.Windows.Forms;
using WIALib;
using OpenDental.UI;

namespace OpenDental{

	///<summary></summary>
	public class ContrDocs : System.Windows.Forms.UserControl	{
		private System.Windows.Forms.ImageList imageListTree;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ImageList imageListTools2;
		private Rectangle RecCrop;
		private Rectangle RecZoom;
		private System.Windows.Forms.PrintDialog PrintDialog1;
		private System.Drawing.Printing.PrintDocument PrintDocument2;
		private System.Windows.Forms.TreeView TreeDocuments;
		private Point PtOrigin;
		private bool MouseIsDown;
		private System.Windows.Forms.PictureBox PictureBox1;
		private System.Drawing.Bitmap ImageCurrent;
		private Rectangle RecTemp;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuExit;
		private System.Windows.Forms.MenuItem menuPrefs;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.OpenFileDialog openFileDialog2;
    private Stream myStream;
    private FormDocInfo formDocInfo2;
		private string patFolder;
		private OpenDental.UI.ODToolBar ToolBarMain;
		private string imageFileName;
		///<summary>Starts out as false. It's only used when repainting the toolbar, not to test mode.</summary>
		private bool IsCropMode;//

		///<summary></summary>
		public ContrDocs(){
			InitializeComponent();
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ContrDocs));
			this.TreeDocuments = new System.Windows.Forms.TreeView();
			this.imageListTree = new System.Windows.Forms.ImageList(this.components);
			this.PrintDialog1 = new System.Windows.Forms.PrintDialog();
			this.PrintDocument2 = new System.Drawing.Printing.PrintDocument();
			this.imageListTools2 = new System.Windows.Forms.ImageList(this.components);
			this.PictureBox1 = new System.Windows.Forms.PictureBox();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuExit = new System.Windows.Forms.MenuItem();
			this.menuPrefs = new System.Windows.Forms.MenuItem();
			this.button1 = new System.Windows.Forms.Button();
			this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.SuspendLayout();
			// 
			// TreeDocuments
			// 
			this.TreeDocuments.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.TreeDocuments.FullRowSelect = true;
			this.TreeDocuments.HideSelection = false;
			this.TreeDocuments.ImageIndex = 2;
			this.TreeDocuments.ImageList = this.imageListTree;
			this.TreeDocuments.Indent = 20;
			this.TreeDocuments.Location = new System.Drawing.Point(0, 33);
			this.TreeDocuments.Name = "TreeDocuments";
			this.TreeDocuments.SelectedImageIndex = 2;
			this.TreeDocuments.Size = new System.Drawing.Size(228, 519);
			this.TreeDocuments.TabIndex = 0;
			this.TreeDocuments.DoubleClick += new System.EventHandler(this.TreeDocuments_DoubleClick);
			this.TreeDocuments.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeDocuments_AfterSelect);
			// 
			// imageListTree
			// 
			this.imageListTree.ImageSize = new System.Drawing.Size(17, 17);
			this.imageListTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTree.ImageStream")));
			this.imageListTree.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// PrintDialog1
			// 
			this.PrintDialog1.AllowPrintToFile = false;
			this.PrintDialog1.Document = this.PrintDocument2;
			// 
			// PrintDocument2
			// 
			this.PrintDocument2.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument2_PrintPage);
			// 
			// imageListTools2
			// 
			this.imageListTools2.ImageSize = new System.Drawing.Size(22, 22);
			this.imageListTools2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTools2.ImageStream")));
			this.imageListTools2.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// PictureBox1
			// 
			this.PictureBox1.BackColor = System.Drawing.SystemColors.Window;
			this.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PictureBox1.Location = new System.Drawing.Point(232, 33);
			this.PictureBox1.Name = "PictureBox1";
			this.PictureBox1.Size = new System.Drawing.Size(568, 432);
			this.PictureBox1.TabIndex = 6;
			this.PictureBox1.TabStop = false;
			this.PictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox1_Paint);
			this.PictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseUp);
			this.PictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseMove);
			this.PictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseDown);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																							this.menuItem1,
																																							this.menuPrefs});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																							this.menuExit});
			this.menuItem1.Text = "File";
			// 
			// menuExit
			// 
			this.menuExit.Index = 0;
			this.menuExit.Text = "Exit";
			// 
			// menuPrefs
			// 
			this.menuPrefs.Index = 1;
			this.menuPrefs.Text = "Preferences";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(68, 380);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(96, 23);
			this.button1.TabIndex = 7;
			this.button1.Text = "adjust contrast";
			this.button1.Visible = false;
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListTools2;
			this.ToolBarMain.Location = new System.Drawing.Point(0, 0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(939, 29);
			this.ToolBarMain.TabIndex = 10;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// ContrDocs
			// 
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.PictureBox1);
			this.Controls.Add(this.TreeDocuments);
			this.Name = "ContrDocs";
			this.Size = new System.Drawing.Size(939, 606);
			this.Load += new System.EventHandler(this.ContrDocs_Load);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ContrDocs_Layout);
			this.ResumeLayout(false);

		}
		#endregion

		private void ContrDocs_Layout(object sender, System.Windows.Forms.LayoutEventArgs e){
      TreeDocuments.Height= Height-TreeDocuments.Location.Y-2;
      PictureBox1.Height=Height-PictureBox1.Location.Y-2;
		  PictureBox1.Width=Width-PictureBox1.Location.X-2;
		}

		///<summary></summary>
		public void InstantClasses(){
			PtOrigin=new Point();
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.button1,
			});
			LayoutToolBar();
		}

		///<summary>Causes the toolbar to be laid out again.</summary>
		public void LayoutToolBar(){
			ToolBarMain.Buttons.Clear();
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Select Patient"),0,"","Patient"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",1,Lan.g(this,"Print"),"Print"));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",2,Lan.g(this,"Delete"),"Delete"));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",3,Lan.g(this,"Item Info"),"Info"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Scan"),4,"","Scan"));
			ToolBarMain.Buttons.Add(new ODToolBarButton
				(Lan.g(this,"Import"),5,Lan.g(this,"Import From File"),"Import"));
			ToolBarMain.Buttons.Add(new ODToolBarButton
				(Lan.g(this,"Paste"),6,Lan.g(this,"Paste From Clipboard"),"Paste"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ToolBarButtonStyle.Separator));
			ODToolBarButton button=new ODToolBarButton("",7,Lan.g(this,"Crop Tool"),"Crop");
			button.Style=ToolBarButtonStyle.ToggleButton;
			if(IsCropMode)
				button.Pushed=true;
			ToolBarMain.Buttons.Add(button);
			button=new ODToolBarButton("",10,Lan.g(this,"Hand Tool"),"Hand");
			button.Style=ToolBarButtonStyle.ToggleButton;
			if(!IsCropMode)
				button.Pushed=true;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton(ToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Zoom In"),8,"","ZoomIn"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Zoom Out"),9,"","ZoomOut"));
			ArrayList toolButItems=ToolButItems.GetForToolBar(ToolBarsAvail.ImagesModule);
			for(int i=0;i<toolButItems.Count;i++){
				ToolBarMain.Buttons.Add(new ODToolBarButton(ToolBarButtonStyle.Separator));
				ToolBarMain.Buttons.Add(new ODToolBarButton(((ToolButItem)toolButItems[i]).ButtonText
					,-1,"",((ToolButItem)toolButItems[i]).ProgramNum));
			}
			ToolBarMain.Invalidate();
		}

		///<summary></summary>
		public void ModuleSelected(){
			RefreshModuleData();
			RefreshModuleScreen();
		}

		///<summary></summary>
		public void ModuleUnselected(){
			Patients.FamilyList=null;
			//from FillDocList:
			DocAttaches.List=null;
			Documents.List=null;
		}

  	private void RefreshModuleData(){
			if(!Patients.PatIsLoaded)
				return;
			Patients.GetFamily(Patients.Cur.PatNum);
			ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString+" - "+Patients.GetCurNameLF();
			if(Patients.Cur.ImageFolder==""){//creates new folder for patient if none present
				string name=Patients.Cur.LName+Patients.Cur.FName;
				string folder="";
				for(int i=0;i<name.Length;i++){
					if(Char.IsLetter(name,i)){
						folder+=name.Substring(i,1);
					}
				}
				folder+=Patients.Cur.PatNum.ToString();//ensures unique name
				try{
					Patient PatCur=Patients.Cur;
					PatCur.ImageFolder=folder;
					Patients.Cur=PatCur;
					patFolder=((Pref)Prefs.HList["DocPath"]).ValueString
						+Patients.Cur.ImageFolder.Substring(0,1)+@"\"
						+Patients.Cur.ImageFolder+@"\";
					Directory.CreateDirectory(patFolder);
					Patients.UpdateCur();
				}
				catch{
					MessageBox.Show(Lan.g(this,"Error.  Could not create folder for patient. "));
					return;
				}
			}
			else{//patient folder already created once
				patFolder=((Pref)Prefs.HList["DocPath"]).ValueString
					+Patients.Cur.ImageFolder.Substring(0,1)+@"\"
					+Patients.Cur.ImageFolder+@"\";
			}
			if(!Directory.Exists(patFolder)){//this makes it more resiliant and allows copies
					//of the opendentaldata folder to be used in read-only situations.
				try{
					Directory.CreateDirectory(patFolder);
				}
				catch{
					MessageBox.Show(Lan.g(this,"Error.  Could not create folder for patient. "));
					return;
				}
			}
			//now find all files in the patient folder that are not in the db and add them
			DocAttaches.Refresh();
			Documents.Refresh();
			DirectoryInfo di=new DirectoryInfo(patFolder);
			FileInfo[] fiList=di.GetFiles();
			int countAdded=0;
			for(int i=0;i<fiList.Length;i++){
				if(!Documents.IsFileNameInList(fiList[i].Name)){
					//MessageBox.Show(fiList[i].Name);
					Documents.Cur=new Document();
					Documents.Cur.DateCreated=DateTime.Today;
					Documents.Cur.Description=fiList[i].Name;
					Documents.Cur.DocCategory=Defs.Short[(int)DefCat.DocumentCats][0].DefNum;
					Documents.Cur.FileName=fiList[i].Name;
					Documents.Cur.WithPat=Patients.Cur.PatNum;
					Documents.InsertCur();
					countAdded++;
				}
			}
			if(countAdded>0){
				MessageBox.Show(countAdded.ToString()+" documents found and added to the first category.");
			}
			//it will refresh in FillDocList																					 
		}

		private void RefreshModuleScreen(){
			if (Patients.PatIsLoaded){
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString+" - "+Patients.GetCurNameLF();
				ToolBarMain.Buttons["Print"].Enabled=true;
				ToolBarMain.Buttons["Delete"].Enabled=true;
				ToolBarMain.Buttons["Info"].Enabled=true;
				ToolBarMain.Buttons["Import"].Enabled=true;
				ToolBarMain.Buttons["Scan"].Enabled=true;
				ToolBarMain.Buttons["Paste"].Enabled=true;
				ToolBarMain.Buttons["Crop"].Enabled=true;
				ToolBarMain.Buttons["Hand"].Enabled=true;
				ToolBarMain.Buttons["ZoomIn"].Enabled=true;
				ToolBarMain.Buttons["ZoomOut"].Enabled=true;
			}
			else{
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString;
				Patients.Cur=new Patient();
				ToolBarMain.Buttons["Print"].Enabled=false;
				ToolBarMain.Buttons["Delete"].Enabled=false;
				ToolBarMain.Buttons["Info"].Enabled=false;
				ToolBarMain.Buttons["Import"].Enabled=false;
				ToolBarMain.Buttons["Scan"].Enabled=false;
				ToolBarMain.Buttons["Paste"].Enabled=false;
				ToolBarMain.Buttons["Crop"].Enabled=false;
				ToolBarMain.Buttons["Hand"].Enabled=false;
				ToolBarMain.Buttons["ZoomIn"].Enabled=false;
				ToolBarMain.Buttons["ZoomOut"].Enabled=false;
			}
			ToolBarMain.Invalidate();
			FillDocList(false);
		}

		private void ContrDocs_Load(object sender, System.EventArgs e){
			//if (SystemInformation.PrimaryMonitorSize.Height<=768){
		}

		/// <summary>Fills the treeview.</summary>
		/// <param name="keepDoc">Set to true to keep the current doc displayed.</param>
		private void FillDocList(bool keepDoc){
			if(!Patients.PatIsLoaded){
				TreeDocuments.Nodes.Clear();
			  ImageCurrent=null;
				DisplayImage(true);
				return;
			}
  	  string sNewNode;
  	  Graphics mygraphics = PictureBox1.CreateGraphics();
			if(!keepDoc){
				ImageCurrent=null;
				mygraphics.FillRectangle(Brushes.White,0,0,PictureBox1.ClientRectangle.Width,PictureBox1.ClientRectangle.Height);
			}
			DocAttaches.Refresh();
			Documents.Refresh();
			for(int i=0;i<TreeDocuments.Nodes.Count;i++) 
				TreeDocuments.Nodes[i].Nodes.Clear();
			TreeDocuments.Nodes.Clear();
			for(int i=0;i<Defs.Short[(int)DefCat.DocumentCats].Length;i++){
				sNewNode=Defs.Short[(int)DefCat.DocumentCats][i].ItemName;
				TreeDocuments.Nodes.Add(new TreeNode(sNewNode));
				TreeDocuments.Nodes[i].SelectedImageIndex=1;  
				TreeDocuments.Nodes[i].ImageIndex=1;          
			}
			for (int i=0;i<Documents.List.Length;i++){
	  		sNewNode=Documents.List[i].DateCreated.ToString("d")+": "+Documents.List[i].Description;
				if(Defs.GetOrder(DefCat.DocumentCats,Documents.List[i].DocCategory)==-1){//if cat hidden
					MessageBox.Show(Lan.g(this,"There is a document in a hidden category: "
						+Defs.GetName(DefCat.DocumentCats,Documents.List[i].DocCategory)
						+". You can unhide this category in Definitions section."));
				}
				else{
					TreeDocuments.Nodes[Defs.GetOrder(DefCat.DocumentCats,Documents.List[i].DocCategory)]
						.Nodes.Add(new TreeNode(sNewNode));
					//store docnum in tag of node:
					TreeDocuments.Nodes[Defs.GetOrder(DefCat.DocumentCats,Documents.List[i].DocCategory)]
						.LastNode.Tag = Documents.List[i].DocNum;
				}
			}
			TreeDocuments.ExpandAll();
			string SrcFileName="";
			if(keepDoc){
				Documents.GetCurrent(Documents.Cur.DocNum.ToString());
				//MessageBox.Show(Docs.Cur.DocNum.ToString());
				if(Defs.GetOrder(DefCat.DocumentCats,Documents.Cur.DocCategory)!=-1){
					foreach(TreeNode n in TreeDocuments.Nodes
						[Defs.GetOrder(DefCat.DocumentCats,Documents.Cur.DocCategory)].Nodes){	
						if(n.Tag.ToString()==Documents.Cur.DocNum.ToString())
							TreeDocuments.SelectedNode=n;
					}				}				SrcFileName=patFolder+Documents.Cur.FileName;
				try{					WebRequest request=WebRequest.Create(SrcFileName); 					WebResponse response=request.GetResponse();					if(Path.GetExtension(Documents.Cur.FileName)==".jpg"){//can only display jpg for now						ImageCurrent=(Bitmap)System.Drawing.Bitmap.FromStream (response.GetResponseStream());					}					else{						ImageCurrent=null;//this may be unnecessary					}					response.Close();			  }			  catch(System.Exception exception){					MessageBox.Show(Lan.g(this,exception.Message)); 			  }
				RecZoom.Width=0;
			}
			else TreeDocuments.SelectedNode=TreeDocuments.Nodes[0];
			mygraphics.Dispose();
		}//end RefreshDocList

		private void ToolBarMain_ButtonClick(object sender, OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			if(e.Button.Tag.GetType()==typeof(string)){
				//standard predefined button
				switch(e.Button.Tag.ToString()){
					case "Patient":
						OnPat_Click();
						break;
					case "Print":
						OnPrint_Click();
						break;
					case "Delete":
						OnDelete_Click();
						break;
					case "Info":
						OnInfo_Click();
						break;
					case "Scan":
						OnScan_Click();
						break;
					case "Import":
						OnImport_Click();
						break;
					case "Paste":
						OnPaste_Click();
						break;
					case "Crop":
						OnCrop_Click();
						break;
					case "Hand":
						OnHand_Click();
						break;
					case "ZoomIn":
						OnZoomIn_Click();
						break;
					case "ZoomOut":
						OnZoomOut_Click();
						break;
				}
			}
			else if(e.Button.Tag.GetType()==typeof(int)){
				Programs.Execute((int)e.Button.Tag);
			}
		}

		private void OnPat_Click() {
			FormPatientSelect formSelectPatient2=new FormPatientSelect();
			formSelectPatient2.ShowDialog();
			if(formSelectPatient2.DialogResult!=DialogResult.OK){
				return;
			}
			ModuleSelected();
			FillDocList(false);
		}

		private void OnPrint_Click() {
			for(int i=0;i<TreeDocuments.Nodes.Count;i++){//does not print a main node
				if(TreeDocuments.SelectedNode.Equals(TreeDocuments.Nodes[i]))//check to see if this is correct
					return;
      }			
			try{
				if(PrintDialog1.ShowDialog()==DialogResult.OK){;
					PrintDocument2.Print();
				}
			}
			catch(System.Exception ex){
				MessageBox.Show(Lan.g(this,"An error occurred while printing"), ex.ToString());
			}
		}

		private void OnDelete_Click() {
			for(int i=0;i<TreeDocuments.Nodes.Count;i++){//can't delete a main node
				if(TreeDocuments.SelectedNode.Equals(TreeDocuments.Nodes[i]))
					return;
      }
			if(MessageBox.Show(Lan.g(this,"Delete Document?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
				return;
			try{
				File.Delete(patFolder+Documents.Cur.FileName);
			}
			catch{
				MessageBox.Show(Lan.g(this,"Could not delete file.  It may be in use elsewhere."));
				return;
			}
			Documents.DeleteCur();
			FillDocList(false);
		}

		private void OnInfo_Click() {
			for(int i=0;i<TreeDocuments.Nodes.Count;i++){//can't get info on a main node
				if(TreeDocuments.SelectedNode.Equals(TreeDocuments.Nodes[i]))
					return;
      }
			FormDocInfo formDocInfo2=new FormDocInfo();
 			formDocInfo2.IsNew=false;
			formDocInfo2.ShowDialog();
			if(formDocInfo2.DialogResult!=DialogResult.OK){
				return;
			}
			FillDocList(true);
			DisplayImage(false);//because the category may have changed
		}

		private void OnScan_Click() {
			#if(ISXP)
				ScanImage();
			#else
				MessageBox.Show(Lan.g(this,"Scanning only works on Windows XP."));
			#endif
		}

		private void OnImport_Click() {
			openFileDialog2=new OpenFileDialog();
  		//openFileDialog2.InitialDirectory=
      //openFileDialog2.Filter="jpg files(*.jpg)|*.jpg|gif files(*.gif)|*.gif|All files(*.*)|*.*";
      //openFileDialog2.FilterIndex=1;
			if(openFileDialog2.ShowDialog()!=DialogResult.OK){
				return;
			}
			if((myStream=openFileDialog2.OpenFile())==null){
				return;
			}
			//Documents.Cur.Description=Path.GetFileName(openFileDialog2.FileName);
			try{				WebRequest request = WebRequest.Create(openFileDialog2.FileName); 				WebResponse response = request.GetResponse();				if(Path.GetExtension(openFileDialog2.FileName)==".jpg"					|| Path.GetExtension(openFileDialog2.FileName)==".gif"){					ImageCurrent = (Bitmap)System.Drawing.Bitmap.FromStream(response.GetResponseStream());				}				else{					ImageCurrent=null;//may not be necessary				}				response.Close();			}			catch(System.Exception exception){				MessageBox.Show(exception.Message);// + " Selected File Not Image."));				myStream.Close();				return;			}
			RecZoom.Width=0;
			DisplayImage(true);
			Documents.Cur=new Document();
			//Documents.InsertCur will use this extension when naming:
			Documents.Cur.FileName=Path.GetExtension(openFileDialog2.FileName);
			formDocInfo2=new FormDocInfo();
			formDocInfo2.IsNew=true;
			//formDocInfo2.Extension=;
			formDocInfo2.ShowDialog();//this saves data to db
		  if(formDocInfo2.DialogResult==DialogResult.OK){
				try{
					//MessageBox.Show(Path.GetDirectoryName(openFileDialog2.FileName)+"\\"+","+patFolder);
					//if(Path.GetDirectoryName(openFileDialog2.FileName)==patFolder
					File.Copy(openFileDialog2.FileName,patFolder+Documents.Cur.FileName);
				}
				catch{
					MessageBox.Show(Lan.g(this,"Unable to copy file.  May be in use."));
					Documents.DeleteCur();
					ImageCurrent=null;
				}
			}
			else{
				ImageCurrent=null;
			}
			myStream.Close();
			//MessageBox.Show("check 2");
			if(ImageCurrent==null){
				FillDocList(false);
			}
			else{
				FillDocList(true);	
			}	
			DisplayImage(true);
		}

		private void OnPaste_Click() {
			IDataObject clipboard=Clipboard.GetDataObject();
			if(!clipboard.GetDataPresent(DataFormats.Bitmap)){
				MessageBox.Show(Lan.g(this,"No bitmap present on clipboard"));	
				return;
			}
			ImageCurrent=(Bitmap)clipboard.GetData(DataFormats.Bitmap);
			RecZoom.Width=0;
			DisplayImage(true);
			Documents.Cur=new Document();
			Documents.Cur.FileName=".jpg";
			formDocInfo2=new FormDocInfo();
			formDocInfo2.IsNew=true;
			formDocInfo2.ShowDialog();
			if(formDocInfo2.DialogResult!=DialogResult.OK){
				return;
			}
			try{
				ImageCurrent.Save(patFolder+Documents.Cur.FileName);
        //Documents.Cur.LastAltered=DateTime.Today; 
        //Documents.UpdateCur();
			}
			catch{
				MessageBox.Show(Lan.g(this,"Error saving document."));
				Documents.DeleteCur();
			}
			FillDocList(true);
			DisplayImage(false);
		}

		private void OnCrop_Click() {
			/*
			if(butCrop.FlatStyle==FlatStyle.Standard){//crop mode
				//do nothing
			}
			else{
				//switch to crop mode
				butCrop.FlatStyle=FlatStyle.Standard;
				butHand.FlatStyle=FlatStyle.Popup;
				PictureBox1.Cursor = Cursors.Default;
			}*/
			//remember it's testing after the push has been completed
			if(ToolBarMain.Buttons["Crop"].Pushed){ //Crop Mode
				ToolBarMain.Buttons["Hand"].Pushed=false;
				PictureBox1.Cursor = Cursors.Default;
			}		
			else{
				ToolBarMain.Buttons["Crop"].Pushed=true;
			}
			IsCropMode=true;
			ToolBarMain.Invalidate();
		}

		private void OnHand_Click() {
			/*
			if(butHand.FlatStyle==FlatStyle.Standard){//if hand mode
				//do nothing
			}
			else{
				//switch to hand mode
				butHand.FlatStyle=FlatStyle.Standard;
				butCrop.FlatStyle=FlatStyle.Popup;
				PictureBox1.Cursor = Cursors.Hand;
				RecCrop=new Rectangle();
				DisplayImage(false);
			}*/
			if(ToolBarMain.Buttons["Hand"].Pushed){//Hand Mode
				ToolBarMain.Buttons["Crop"].Pushed=false;
				PictureBox1.Cursor=Cursors.Hand;
				RecCrop=new Rectangle();
				DisplayImage(false);
			}
			else{
				ToolBarMain.Buttons["Hand"].Pushed=true;
			}
			IsCropMode=false;
			ToolBarMain.Invalidate();
		}

		private void OnZoomIn_Click() {
			if(ImageCurrent==null) return;
			RecZoom.Height=RecZoom.Height/2;
			RecZoom.Width=RecZoom.Width/2;
			//maintain original x center.(RecZoom.x+width)
			RecZoom.X=RecZoom.X+(RecZoom.Width/2);
			//if white space on right, and x>0, center document
			if((RecZoom.X+RecZoom.Width>ImageCurrent.Width)&&(RecZoom.X>0)){
				RecZoom.X=(ImageCurrent.Width/2)-(RecZoom.Width/2);
				if (RecZoom.X<0) RecZoom.X=0;
			}
			Graphics dc=PictureBox1.CreateGraphics();
			dc.DrawImage(ImageCurrent
				,new Rectangle(0,0,PictureBox1.ClientRectangle.Width,PictureBox1.ClientRectangle.Height)
				,RecZoom, GraphicsUnit.Pixel);
			dc.Dispose();
		}

		private void OnZoomOut_Click() {
			if(ImageCurrent==null) return;
			RecZoom.Height=RecZoom.Height*2;
			RecZoom.Width=RecZoom.Width*2;
			RecZoom.X=RecZoom.X-(RecZoom.Width/4);
			//if RecZoom is larger in both dimensions:
			if((RecZoom.Height > ImageCurrent.Height)&&(RecZoom.Width > ImageCurrent.Width)){
				RecZoom.Height=ImageCurrent.Height;
				RecZoom.Width=ImageCurrent.Width;
				RecZoom.X=0;
				RecZoom.Y=0;
				if(((double)ImageCurrent.Width/(double)ImageCurrent.Height)
					< ((double)PictureBox1.ClientRectangle.Width/(double)PictureBox1.ClientRectangle.Height)){
					RecZoom.Width=(int)(RecZoom.Height*(double)PictureBox1.ClientRectangle.Width
						/(double)PictureBox1.ClientRectangle.Height);
				}
				else{
					RecZoom.Height=(int)(RecZoom.Width*(double)PictureBox1.ClientRectangle.Height
						/(double)PictureBox1.ClientRectangle.Width);
				}
			}
			else if(RecZoom.X+RecZoom.Width > ImageCurrent.Width){
				RecZoom.X=ImageCurrent.Width-RecZoom.Width;
			}
			else if(RecZoom.Y+RecZoom.Height > ImageCurrent.Height)
				RecZoom.Y=ImageCurrent.Height-RecZoom.Height;
			if(RecZoom.X<0)
				RecZoom.X=0;
			if(RecZoom.Y<0)
				RecZoom.Y=0;
			Graphics dc2=PictureBox1.CreateGraphics();
			dc2.FillRectangle(Brushes.White,0,0
				,PictureBox1.ClientRectangle.Width,PictureBox1.ClientRectangle.Height);
			dc2.DrawImage(ImageCurrent
				,new Rectangle(0,0,PictureBox1.ClientRectangle.Width,PictureBox1.ClientRectangle.Height)
				,RecZoom,GraphicsUnit.Pixel);
			dc2.Dispose();
		}

		private void AutoCrop(){
			float edgeDarkness=.94f;  //threshold for brightness. 1 is white.
			float deltaStart;
			try{
				deltaStart=Convert.ToSingle(((Pref)Prefs.HList["CropDelta"]).ValueString);
			}
			catch{
				deltaStart=.035f;//.035f is good.  change in brightness
				//larger number is less sensitive
			}
			float delta;
  		Bitmap imageTemp=new Bitmap(ImageCurrent);
			int width=0;      //used to find averages to figure out dimensions
			int height=0;     //used to find averages to figure out dimensions
			float bright=0;     //used to store average brightness to figure out where to crop image
			float brightLast=0;//used to compare this row with previous row brightness.
			int cropTop=0;    //stores Top Dimension
			int cropBottom=imageTemp.Height; //stores Bottom Dimension
			int cropLeft=0;   //stores Left Dimension
			int cropRight=imageTemp.Width;  //stores Right Dimension
			Color colorPix;  //stores pixel color at certain coordinates	    
			delta=deltaStart;
			FindEdges://beginning of big loop
			if(cropTop==0){//starts at top and works down 'til change in avg brightness indicates start of important data
				brightLast=0;
				for(height=0;height<cropBottom-100;height+=5){//minimum crop rect is 100x100
					bright=0;
					for(width=cropLeft;width<cropRight;width+=5){
						colorPix=imageTemp.GetPixel(width,height);
						bright+=colorPix.GetBrightness();
					}
					bright=bright/(cropRight-cropLeft)*5;
					if(brightLast-bright > delta && brightLast > edgeDarkness){
						cropTop=height;
						break;
					}
					brightLast=bright;
				}
			}
			if(cropLeft==0){//starts at left and works right
				brightLast=0;
				for(width=0;width<cropRight-100;width+=5){
					bright=0;
					for(height=cropTop;height<cropBottom;height+=5){
						colorPix=imageTemp.GetPixel(width,height);
						bright+=colorPix.GetBrightness();
					}		
					bright=bright/(cropBottom-cropTop)*5;
					if(brightLast-bright > delta && brightLast > edgeDarkness){
						cropLeft=width;
						break;
					}
					brightLast=bright;
				}
			}
			if(cropRight==imageTemp.Width){//starts at right and works left
				brightLast=0;
				for(width=imageTemp.Width-1;width>cropLeft+100;width-=5){
					bright=0;
					for(height=cropTop;height<cropBottom;height+=5){
						colorPix=imageTemp.GetPixel(width,height);
						bright+=colorPix.GetBrightness();
					}																								
					bright=bright/(cropBottom-cropTop)*5;
					if(brightLast-bright > .003 && brightLast > edgeDarkness){//right side set ultra sensitive with only one pass
						cropRight=width;
						break;
					}
					brightLast=bright;
				}
			}
			if(cropBottom==imageTemp.Height){//starts at bottom and works up
				brightLast=0;
				for(height=imageTemp.Height-1;height>cropTop+100;height-=5){
					bright=0;
					for(width=cropLeft;width<cropRight;width+=5){
						colorPix=imageTemp.GetPixel(width,height);
						bright+=colorPix.GetBrightness();
					}	
					bright=bright/(cropRight-cropLeft)*5;
					if(brightLast-bright > delta && brightLast > edgeDarkness){
						cropBottom=height;
						break;
					}
					brightLast=bright;
				}
			}
			if(delta>0){
				if(cropTop==0 || cropLeft==0 || cropBottom==imageTemp.Height){//right side skipped on purpose
					delta-=.005f;
					goto FindEdges;
				}
			}
			cropLeft-=7;
			cropTop-=7;
			cropRight+=17;
			cropBottom+=10;
			if(cropLeft<0) cropLeft=0;
			if(cropTop<0) cropTop=0;
			if(cropRight>imageTemp.Width-1) cropRight=imageTemp.Width-1;
			if(cropBottom>imageTemp.Height-1) cropBottom=imageTemp.Height-1;
			RecCrop=new Rectangle();			RecCrop.X=cropLeft;			RecCrop.Y=cropTop;			RecCrop.Width=cropRight-cropLeft;			RecCrop.Height=cropBottom-cropTop;			Graphics grPictBox = PictureBox1.CreateGraphics();
			//ImageCurrent will get the blue rectangle drawn in it, but then it will be replaced with a 
			//clean image before crop and save.
			Graphics grImg=Graphics.FromImage(ImageCurrent);			grImg.DrawRectangle(new Pen(Color.Blue),RecCrop);			grPictBox.DrawImage(ImageCurrent
				,new Rectangle(0,0,PictureBox1.ClientRectangle.Width,PictureBox1.ClientRectangle.Height)
				,RecZoom,GraphicsUnit.Pixel);
      if (MessageBox.Show(Lan.g(this,"Crop to Rectangle?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				ImageCurrent=new Bitmap(imageTemp);
   			return;
			}
			ImageCurrent=new Bitmap(imageTemp);
			Bitmap bitmapTemp=new Bitmap(1,1);//just to get the horizontal res
			float ratio=bitmapTemp.HorizontalResolution/ImageCurrent.HorizontalResolution;// 96/150
			bitmapTemp=new Bitmap((int)(RecCrop.Width*ratio),(int)(RecCrop.Height*ratio));
			Graphics grTemp2=Graphics.FromImage(bitmapTemp);//we're going to draw on bitmapTemp
			grTemp2.DrawImage(ImageCurrent,0,0,RecCrop,GraphicsUnit.Pixel);
			grImg.Dispose();
			grPictBox.Dispose();
			grTemp2.Dispose();
 			ImageCurrent=new Bitmap(bitmapTemp);
			RecZoom.Width=0;//DisplayImage will then recreate RecZoom
			DisplayImage(true);
			ImageCurrent.Save(patFolder+Documents.Cur.FileName,ImageFormat.Jpeg);
      //Documents.Cur.LastAltered=DateTime.Today;
      //Documents.UpdateCur();
			FillDocList(true);
			DisplayImage(true);
		}

		[Conditional("ISXP")]
		private void ScanImage(){
			//MessageBox.Show(Environment.OSVersion.Version.ToString());
			//return;
			ShowBlank();                   //sets screen to blank while waiting for image to scan
			ImageCurrent=null;             // current image is null till receives value from scan 
			WiaClass wiaManager=null;		   // WIA manager COM object
			CollectionClass	wiaDevs=null;	 // WIA devices collection COM object
			ItemClass	wiaRoot=null;		     // WIA root device COM object
			CollectionClass	wiaPics=null;	 // WIA collection COM object
			ItemClass	wiaItem=null;		     // WIA image COM object
			ImageCodecInfo myImageCodecInfo;
			ImageCodecInfo[] encoders;
			encoders = ImageCodecInfo.GetImageEncoders();
			myImageCodecInfo=null;
			for(int j=0;j<encoders.Length;j++){
				if(encoders[j].MimeType=="image/jpeg")
					myImageCodecInfo=encoders[j];
			}
			System.Drawing.Imaging.Encoder myEncoder= System.Drawing.Imaging.Encoder.Quality;
			EncoderParameters myEncoderParameters= new EncoderParameters(1);
			long qualityL=(long)Convert.ToInt32(((Pref)Prefs.HList["ScannerCompression"]).ValueString);//Possible values 0-100?
			EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder,qualityL);
			myEncoderParameters.Param[0] = myEncoderParameter;
			//ImageCurrent.Save("ShapesLZW.tif", myImageCodecInfo, myEncoderParameters);
			try {
				wiaManager=new WiaClass();		// create COM instance of WIA manager
				wiaDevs=wiaManager.Devices as CollectionClass;			// call Wia.Devices to get all devices
				if((wiaDevs==null) || (wiaDevs.Count==0))  {
					MessageBox.Show(Lan.g(this,"No scanners or cameras found!"));
					return;
				}
				object selectUsingUI=System.Reflection.Missing.Value;			// = Nothing / Default
				wiaRoot=(ItemClass)wiaManager.Create(ref selectUsingUI);	// let user select device
				if(wiaRoot==null)											// nothing to do
					return;
				// this call shows the common WIA dialog to let the user select a picture:
				wiaPics=wiaRoot.GetItemsFromUI(WiaFlag.SingleImage,WiaIntent.ImageTypeGrayscale) as CollectionClass;
				if(wiaPics==null)
					return;
				bool takeFirst=true;						        // this sample uses only one single picture
				foreach(object wiaObj in wiaPics){		// enumerate all the pictures the user selected
					if(takeFirst){
						wiaItem=(ItemClass)Marshal.CreateWrapperOfType(wiaObj,typeof(ItemClass));
            imageFileName=Path.GetTempFileName();				    // create temporary file for image
						Cursor.Current=Cursors.WaitCursor;				      // could take some time
						this.Refresh();
						wiaItem.Transfer(imageFileName,false);			    // transfer picture to our temporary file
						ImageCurrent=(Bitmap)Bitmap.FromFile(imageFileName);	    // sets current image to temp file
						RecZoom.Width=0;
						DisplayImage(true);                             //shows image
						Documents.Cur=new Document();
						Documents.Cur.FileName=".jpg";
						formDocInfo2=new FormDocInfo();
						formDocInfo2.IsNew=true;     
						formDocInfo2.ShowDialog();    //opens dialog to set info and precursor to save
						if (formDocInfo2.DialogResult != DialogResult.OK){   //return if Cancel is pushed
              ShowBlank();   //sets screen to blank and loses image from scanner
							ImageCurrent=null;
							return;
            }
						ImageCurrent.Save(patFolder+Documents.Cur.FileName,myImageCodecInfo,myEncoderParameters);
            //Documents.Cur.LastAltered=DateTime.Today;
						//Documents.UpdateCur();
						FillDocList(true);       //adds new doc to DocList and TreeDocument and saves path to image
						DisplayImage(true);     //sets current image of Docs.Cur image path and displays that image
						AutoCrop();
  				}//end of if(takeFirst)
					Marshal.ReleaseComObject(wiaObj);// release enumerated COM object
				}//end of for each
			}//end of try
			catch(Exception ee)  {
				MessageBox.Show(Lan.g(this,"Acquire from WIA Imaging failed\r\n" + ee.Message));
				ShowBlank();
				ImageCurrent=null;
				return;
			} // end of catch
			finally {    
				if(wiaItem!=null )
					Marshal.ReleaseComObject(wiaItem);		// release WIA image COM object
				if(wiaPics!=null)
					Marshal.ReleaseComObject(wiaPics);		// release WIA collection COM object
				if(wiaRoot!=null)
					Marshal.ReleaseComObject(wiaRoot);		// release WIA root device COM object
				if(wiaDevs!=null)
					Marshal.ReleaseComObject(wiaDevs);		// release WIA devices collection COM object
				if(wiaManager!=null)
					Marshal.ReleaseComObject(wiaManager);		// release WIA manager COM object
				Cursor.Current=Cursors.Default;				// restore cursor
			}
		}//end ScanImage

		private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e){
			RectangleF rectf=e.Graphics.VisibleClipBounds;
			//MessageBox.Show("Top:"+((int)rectf.Top).ToString()
			//	+"Left:"+((int)rectf.Left).ToString()
			//	+"Right:"+((int)rectf.Right).ToString()
			//	+"Bottom:"+((int)rectf.Bottom).ToString()
			//	);
			//e.Cancel=true;
			double xratio=ImageCurrent.Width/rectf.Width;
			double yratio=ImageCurrent.Height/rectf.Height;
			double ratio;
			if(xratio > yratio) ratio=xratio;
			else ratio=yratio;
//later allow option for unscaled, probably make it default for smaller images.
			e.Graphics.DrawImage(ImageCurrent,0,0,(float)(ImageCurrent.Width/ratio),(float)(ImageCurrent.Height/ratio));
			e.HasMorePages = false;
		}

		private void treeDocuments_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e){
			//later change the event to click instead of AfterSelect for more intuitive response.
			string SrcFileName="";
			for(int i=0;i<TreeDocuments.Nodes.Count;i++){
			  if (TreeDocuments.SelectedNode.Equals(TreeDocuments.Nodes[i])){
				  ShowBlank();
				  return;
        }
	    }
			//tag holds the document number of the node
		  Documents.GetCurrent(TreeDocuments.SelectedNode.Tag.ToString());			SrcFileName = patFolder+Documents.Cur.FileName;			try{		    WebRequest request=WebRequest.Create(SrcFileName); 			  WebResponse response=request.GetResponse();				//MessageBox.Show(Path.GetExtension(SrcFileName));				if(Path.GetExtension(SrcFileName)==".jpg"					|| Path.GetExtension(SrcFileName)==".JPG"					|| Path.GetExtension(SrcFileName)==".gif"					|| Path.GetExtension(SrcFileName)==".GIF"){					ImageCurrent = (Bitmap)System.Drawing.Bitmap.FromStream(response.GetResponseStream());				}				else{					ImageCurrent=null;//may not be necessary				}			  response.Close();	    }		  catch(System.Exception exception){		    MessageBox.Show(Lan.g(this,exception.Message)); 				ImageCurrent=null;	    }
		  RecZoom.Width=0;
		  DisplayImage(true);
		}

		private void TreeDocuments_DoubleClick(object sender, System.EventArgs e) {
			//AfterSelect will have just run, so the cur document will have been refreshed.
			string SrcFileName="";
			for(int i=0;i<TreeDocuments.Nodes.Count;i++){
			  if (TreeDocuments.SelectedNode.Equals(TreeDocuments.Nodes[i])){
				  ShowBlank();
				  return;
        }
	    }
			//tag holds the document number of the node
		  //Documents.GetCurrent(TreeDocuments.SelectedNode.Tag.ToString());			SrcFileName = patFolder+Documents.Cur.FileName;
			if(Path.GetExtension(SrcFileName)!=".jpg"
				&& Path.GetExtension(SrcFileName)!=".JPG"){
				try{
					Process.Start(SrcFileName);
				}
				catch(Exception ex){
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void ShowBlank(){
			Graphics mygraphics = PictureBox1.CreateGraphics();
			mygraphics.FillRectangle(Brushes.White,0,0,PictureBox1.ClientRectangle.Width
				,PictureBox1.ClientRectangle.Height);
			mygraphics.Dispose();
		}

		private void PictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e){
			Graphics dc=e.Graphics;
			if (ImageCurrent != null)
				dc.DrawImage(ImageCurrent
					,new Rectangle(0,0,PictureBox1.ClientRectangle.Width,PictureBox1.ClientRectangle.Height)
					,RecZoom,GraphicsUnit.Pixel);
		}

		private void menuExit_Click(object sender, System.EventArgs e){
			//this.Close();
		}

		private void DisplayImage(bool clearFirst){
			Graphics dc = PictureBox1.CreateGraphics();
			if(clearFirst)
				dc.FillRectangle(Brushes.White,0,0,PictureBox1.ClientRectangle.Width
					,PictureBox1.ClientRectangle.Height);
			if(ImageCurrent==null)
				return;
			if(RecZoom.Width==0){
  			RecZoom=new Rectangle(0,0,ImageCurrent.Width,ImageCurrent.Height);
  			if(((double)ImageCurrent.Width/(double)ImageCurrent.Height)
					<((double)PictureBox1.ClientRectangle.Width/(double)PictureBox1.ClientRectangle.Height)){
					RecZoom.Width=(int)(RecZoom.Height*(double)PictureBox1.ClientRectangle.Width
						/(double)PictureBox1.ClientRectangle.Height);
				}
				else{
					RecZoom.Height=(int)(RecZoom.Width*(double)PictureBox1.ClientRectangle.Height
						/(double)PictureBox1.ClientRectangle.Width);
				}
			}
			RecCrop=new Rectangle();//so it won't show anymore
			dc.DrawImage(ImageCurrent
				,new Rectangle(0,0,PictureBox1.ClientRectangle.Width,PictureBox1.ClientRectangle.Height)
				,RecZoom,GraphicsUnit.Pixel);
			dc.Dispose();
		}

		private void PictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e){
			//if(toolBarButHand.Pushed){//Hand Mode
			//if(butHand.FlatStyle==FlatStyle.Standard){//hand mode
			if(ToolBarMain.Buttons["Hand"].Pushed){//hand mode
				PtOrigin=new Point(e.X,e.Y);
				MouseIsDown=true;
				//RecZoom is already established and will not change until after MouseUp
			}
			else{//Crop Mode
				PtOrigin=new Point(e.X,e.Y);
				MouseIsDown=true;
			}
		}		

		private void PictureBox1_MouseMove(object sender,System.Windows.Forms.MouseEventArgs e){
			if(ImageCurrent==null) return;
			//if(toolBarButHand.Pushed){//Hand Mode
			//if(butHand.FlatStyle==FlatStyle.Standard){//hand mode
			if(ToolBarMain.Buttons["Hand"].Pushed){//hand mode
				if(!MouseIsDown) return;
				RecTemp=new Rectangle();
				RecTemp.X=RecZoom.X-((e.X-PtOrigin.X)*2* RecZoom.Width/PictureBox1.ClientRectangle.Width);
				RecTemp.Y=RecZoom.Y-((e.Y-PtOrigin.Y)*2* RecZoom.Height/PictureBox1.ClientRectangle.Height);
				RecTemp.Width=RecZoom.Width;
				RecTemp.Height=RecZoom.Height;
				if(RecTemp.X<0) RecTemp.X=0;
				if(RecTemp.Y<0) RecTemp.Y=0;
				if(RecZoom.X+RecZoom.Width>ImageCurrent.Width){ //if There was white space on side when you started
					if(RecTemp.X > RecZoom.X) RecTemp.X=RecZoom.X;
				}
				else{//no white space on side when started
					if (RecTemp.X+RecTemp.Width > ImageCurrent.Width)
						RecTemp.X=ImageCurrent.Width-RecTemp.Width;
				}
				if(RecZoom.Y+RecZoom.Height>ImageCurrent.Height){//if There was white space on bottom when you started
					if (RecTemp.Y>RecZoom.Y)
						RecTemp.Y=RecZoom.Y;
				}
				else{//no white space on bottom when started
					if (RecTemp.Y+RecTemp.Height > ImageCurrent.Height)
						RecTemp.Y=ImageCurrent.Height-RecTemp.Height;
				}
				Graphics dc=PictureBox1.CreateGraphics();
				dc.DrawImage(ImageCurrent
					,new Rectangle(0,0,PictureBox1.ClientRectangle.Width,PictureBox1.ClientRectangle.Height)
					,RecTemp,GraphicsUnit.Pixel);
				dc.Dispose();
			}
			else{//Crop Mode				Graphics mygraphics=PictureBox1.CreateGraphics();
				if(MouseIsDown){					mygraphics.DrawImage(ImageCurrent						,new Rectangle(0,0,PictureBox1.ClientRectangle.Width,PictureBox1.ClientRectangle.Height)						,RecZoom,GraphicsUnit.Pixel);					RecCrop=new Rectangle();					RecCrop.X=PtOrigin.X;					RecCrop.Y=PtOrigin.Y;					RecCrop.Width=e.X-PtOrigin.X;					RecCrop.Height=e.Y-PtOrigin.Y;					if(RecZoom.X+(e.X*RecZoom.Width/PictureBox1.ClientRectangle.Width) > ImageCurrent.Width)						RecCrop.Width=((ImageCurrent.Width-RecZoom.X)*PictureBox1.ClientRectangle.Width/RecZoom.Width)							-RecCrop.X;					if(RecZoom.Y+(e.Y*RecZoom.Height/PictureBox1.ClientRectangle.Height)>ImageCurrent.Height)						RecCrop.Height=((ImageCurrent.Height-RecZoom.Y)*PictureBox1.ClientRectangle.Height/RecZoom.Height)							-RecCrop.Y;					//need to unmangle rectangle?? not too important...					mygraphics.DrawRectangle(new Pen(Color.Blue),RecCrop);				}				else{//mouse is up 					mygraphics.DrawImage(ImageCurrent						,new Rectangle(0,0,PictureBox1.ClientRectangle.Width,PictureBox1.ClientRectangle.Height)						,RecZoom,GraphicsUnit.Pixel);					if(e.X<ImageCurrent.Width*PictureBox1.ClientRectangle.Width/RecZoom.Width)						mygraphics.DrawLine(new Pen(Color.Blue),new Point(e.X,0),new Point(e.X,ImageCurrent.Height*PictureBox1.ClientRectangle.Height/RecZoom.Height));					if(e.Y<ImageCurrent.Height*PictureBox1.ClientRectangle.Height/RecZoom.Height)						mygraphics.DrawLine(new Pen(Color.Blue),new Point(0,e.Y),new Point(ImageCurrent.Width*PictureBox1.ClientRectangle.Width/RecZoom.Width,e.Y));					mygraphics.DrawRectangle(new Pen(Color.Blue),RecCrop);				}				mygraphics.Dispose();			}
		}//end mousemove

		private void PictureBox1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e){
			MouseIsDown=false;
			//if(toolBarButHand.Pushed){//Hand Mode
			//if(butHand.FlatStyle==FlatStyle.Standard){
			if(ToolBarMain.Buttons["Hand"].Pushed){//hand mode
				RecZoom=RecTemp;
			}
			else{//Crop Mode
				//Graphics grfx=PictureBox1.CreateGraphics();
				if(RecCrop.Width==0 || RecCrop.Height==0){
					return;//the size of a rectangle must be larger than 0
				}
				if(MessageBox.Show(Lan.g(this,"Crop to Rectangle?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
					return;
				//math to convert RecCrop from client coord to image coords:
				//use RecZoom as temp rectangle in Image coordinates
				RecZoom.X=RecZoom.X+(RecCrop.X*RecZoom.Width/PictureBox1.ClientRectangle.Width);
				RecZoom.Y=RecZoom.Y+(RecCrop.Y*RecZoom.Height/PictureBox1.ClientRectangle.Height);
				RecZoom.Width=RecCrop.Width*RecZoom.Width/PictureBox1.ClientRectangle.Width;
				RecZoom.Height=RecCrop.Height*RecZoom.Height/PictureBox1.ClientRectangle.Height;
				Bitmap bitmapTemp=new Bitmap(1,1);//just to get the horizontal res
				float ratio=bitmapTemp.HorizontalResolution/ImageCurrent.HorizontalResolution;// 96/150
				bitmapTemp=new Bitmap((int)(RecZoom.Width*ratio),(int)(RecZoom.Height*ratio));
				Graphics grTemp=Graphics.FromImage(bitmapTemp);//we're going to draw on bitmapTemp
				grTemp.DrawImage(ImageCurrent,0,0,RecZoom,GraphicsUnit.Pixel);
				grTemp.Dispose();
 				ImageCurrent=(Bitmap)bitmapTemp.Clone();
				RecZoom.Width=0;//DisplayImage will then recreate RecZoom
				DisplayImage(true);
				ImageCurrent.Save(patFolder+Documents.Cur.FileName,ImageFormat.Jpeg);
        //Documents.Cur.LastAltered=DateTime.Today;
        //Documents.UpdateCur();
				FillDocList(true);
				DisplayImage(true);
			}
		}

		

		

		
	}
}