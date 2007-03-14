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
		///<summary>This is the blue crop rectangle.  It is in PictureBox coordinates. The math to convert to image coordinates is handled in the mouseup event.</summary>
		private Rectangle RecCrop;
		///<summary>Think of this as a rectangle laid on the ImageCurrent which represents the portion of the image to be displayed depending on the zoom level.  It always starts out bigger than the actual image, but can be shrunk by zooming in.  It always has exactly the same proportions as the PictureBox.</summary>
		private Rectangle RecZoom;
		private System.Windows.Forms.PrintDialog PrintDialog1;
		private System.Drawing.Printing.PrintDocument PrintDocument2;
		private System.Windows.Forms.TreeView TreeDocuments;
		///<summary>When dragging on Picturebox, this is the starting point it PictureBox coordinates.</summary>
		private Point MouseDownOrigin;
		private bool MouseIsDown;
		private System.Windows.Forms.PictureBox PictureBox1;
		private System.Drawing.Bitmap ImageCurrent;
		///<summary>During dragging, this rectangle is used just like RecZoom, but is only temporary.</summary>
		private Rectangle RecTempZoom;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuExit;
		private System.Windows.Forms.MenuItem menuPrefs;
		private System.Windows.Forms.OpenFileDialog openFileDialog2;
    private Stream myStream;
    private FormDocInfo formDocInfo2;
		///<summary>The path to the patient folder, including the letter folder, and ending with \</summary>
		private string patFolder;
		private OpenDental.UI.ODToolBar ToolBarMain;
		private string imageFileName;
		///<summary>Starts out as false. It's only used when repainting the toolbar, not to test mode.</summary>
		private bool IsCropMode;//
		private bool MouseIsDownOnTree;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.ContextMenu contextTree;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private Point TreeOriginalMouse;
		private System.Windows.Forms.ContextMenu menuPatient;
		///<summary>Set when DisplayImage is run. It is false if no Documents.Cur.</summary>
		private bool Enhanced;
		// declarations, spk 10/05/04
		[System.Runtime.InteropServices.DllImport("EZTW32.DLL")] 
		public static extern int TWAIN_AcquireToFilename(IntPtr hwndApp, string bmpFileName); 
		[System.Runtime.InteropServices.DllImport("EZTW32.DLL")] 
		public static extern int TWAIN_SelectImageSource(IntPtr hwndApp); 
		[System.Runtime.InteropServices.DllImport("EZTW32.DLL")] 
		public static extern int TWAIN_AcquireToClipboard(IntPtr hwndApp, long wPixTypes); 
		[System.Runtime.InteropServices.DllImport("EZTW32.DLL")] 
		public static extern int TWAIN_IsAvailable(); 
		[System.Runtime.InteropServices.DllImport("EZTW32.DLL")] 
		public static extern int TWAIN_EasyVersion();
		// spk 10/05/04

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
			this.contextTree = new System.Windows.Forms.ContextMenu();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.imageListTree = new System.Windows.Forms.ImageList(this.components);
			this.PrintDialog1 = new System.Windows.Forms.PrintDialog();
			this.PrintDocument2 = new System.Drawing.Printing.PrintDocument();
			this.imageListTools2 = new System.Windows.Forms.ImageList(this.components);
			this.PictureBox1 = new System.Windows.Forms.PictureBox();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuExit = new System.Windows.Forms.MenuItem();
			this.menuPrefs = new System.Windows.Forms.MenuItem();
			this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.menuPatient = new System.Windows.Forms.ContextMenu();
			this.SuspendLayout();
			// 
			// TreeDocuments
			// 
			this.TreeDocuments.ContextMenu = this.contextTree;
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
			this.TreeDocuments.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeDocuments_MouseDown);
			this.TreeDocuments.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TreeDocuments_MouseUp);
			this.TreeDocuments.DoubleClick += new System.EventHandler(this.TreeDocuments_DoubleClick);
			this.TreeDocuments.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TreeDocuments_MouseMove);
			// 
			// contextTree
			// 
			this.contextTree.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																								this.menuItem2,
																																								this.menuItem3,
																																								this.menuItem4});
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "Print";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "Delete";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.Text = "Info";
			// 
			// imageListTree
			// 
			this.imageListTree.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imageListTree.ImageSize = new System.Drawing.Size(16, 16);
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
			this.imageListTools2.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
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
			this.PictureBox1.Size = new System.Drawing.Size(600, 400);
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
			//Debug.WriteLine(PictureBox1.ClientSize);
			RecZoom.Width=0;
			DisplayImage(true,Enhanced);
		}

		///<summary></summary>
		public void InstantClasses(){
			MouseDownOrigin=new Point();
			Lan.C(this, new System.Windows.Forms.Control[] {
				//this.button1,
			});
			LayoutToolBar();
			contextTree.MenuItems.Clear();
			contextTree.MenuItems.Add("Print",new System.EventHandler(menuTree_Click));
			contextTree.MenuItems.Add("Delete",new System.EventHandler(menuTree_Click));
			contextTree.MenuItems.Add("Info",new System.EventHandler(menuTree_Click));
		}

		///<summary>Causes the toolbar to be laid out again.</summary>
		public void LayoutToolBar(){
			ToolBarMain.Buttons.Clear();
			ODToolBarButton button;
			button=new ODToolBarButton(Lan.g(this,"Select Patient"),0,"","Patient");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=menuPatient;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",1,Lan.g(this,"Print"),"Print"));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",2,Lan.g(this,"Delete"),"Delete"));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",3,Lan.g(this,"Item Info"),"Info"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			button=new ODToolBarButton("Scan:",-1,"","");
			button.Style=ODToolBarButtonStyle.Label;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton("",14,Lan.g(this,"Scan Document"),"ScanDoc"));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",16,Lan.g(this,"Scan Radiograph"),"ScanXRay"));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",15,Lan.g(this,"Scan Photo"),"ScanPhoto"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton
				(Lan.g(this,"Import"),5,Lan.g(this,"Import From File"),"Import"));
			ToolBarMain.Buttons.Add(new ODToolBarButton
				(Lan.g(this,"Paste"),6,Lan.g(this,"Paste From Clipboard"),"Paste"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			button=new ODToolBarButton("",7,Lan.g(this,"Crop Tool"),"Crop");
			button.Style=ODToolBarButtonStyle.ToggleButton;
			if(IsCropMode)
				button.Pushed=true;
			ToolBarMain.Buttons.Add(button);
			button=new ODToolBarButton("",10,Lan.g(this,"Hand Tool"),"Hand");
			button.Style=ODToolBarButtonStyle.ToggleButton;
			if(!IsCropMode)
				button.Pushed=true;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",8,Lan.g(this,"Zoom In"),"ZoomIn"));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",9,Lan.g(this,"Zoom Out"),"ZoomOut"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			button=new ODToolBarButton("Rotate:",-1,"","");
			button.Style=ODToolBarButtonStyle.Label;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton("",11,Lan.g(this,"Flip"),"Flip"));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",12,Lan.g(this,"Rotate Left"),"RotateL"));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",13,Lan.g(this,"Rotate Right"),"RotateR"));
			ArrayList toolButItems=ToolButItems.GetForToolBar(ToolBarsAvail.ImagesModule);
			for(int i=0;i<toolButItems.Count;i++){
				ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
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
				if(!Documents.IsFileNameInList(fiList[i].Name)
					&& fiList[i].Name!="Thumbs.db")//Thumbs.db is a hidden Windows file unrelated to OD.
				{
					//MessageBox.Show(fiList[i].Name);
					Documents.Cur=new Document();
					Documents.Cur.DateCreated=DateTime.Today;
					Documents.Cur.Description=fiList[i].Name;
					Documents.Cur.DocCategory=Defs.Short[(int)DefCat.ImageCats][0].DefNum;
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
				ToolBarMain.Buttons["ScanDoc"].Enabled=true;
				ToolBarMain.Buttons["ScanXRay"].Enabled=true;
				ToolBarMain.Buttons["ScanPhoto"].Enabled=true;
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
				ToolBarMain.Buttons["ScanDoc"].Enabled=false;
				ToolBarMain.Buttons["ScanXRay"].Enabled=false;
				ToolBarMain.Buttons["ScanPhoto"].Enabled=false;
				ToolBarMain.Buttons["Paste"].Enabled=false;
				ToolBarMain.Buttons["Crop"].Enabled=false;
				ToolBarMain.Buttons["Hand"].Enabled=false;
				ToolBarMain.Buttons["ZoomIn"].Enabled=false;
				ToolBarMain.Buttons["ZoomOut"].Enabled=false;
			}
			FillPatientButton();
			ToolBarMain.Invalidate();
			FillDocList(false);
		}

		private void FillPatientButton(){
			Patients.AddPatsToMenu(menuPatient,new EventHandler(menuPatient_Click));
		}

		private void menuPatient_Click(object sender,System.EventArgs e) {
			Patients.ButtonSelect(menuPatient,sender);
			ModuleSelected();
		}


		private void ContrDocs_Load(object sender, System.EventArgs e){
			//if (SystemInformation.PrimaryMonitorSize.Height<=768){
		}

		/// <summary>Refreshes list from db, then fills the treeview.  Set to true to keep the current doc displayed.</summary>
		private void FillDocList(bool keepDoc){
			if(!Patients.PatIsLoaded){
				TreeDocuments.Nodes.Clear();
			  ImageCurrent=null;
				DisplayImage(true,false);
				return;
			}
  	  string sNewNode;
			int indexImageList;
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
			for(int i=0;i<Defs.Short[(int)DefCat.ImageCats].Length;i++){
				sNewNode=Defs.Short[(int)DefCat.ImageCats][i].ItemName;
				TreeDocuments.Nodes.Add(new TreeNode(sNewNode));
				TreeDocuments.Nodes[i].SelectedImageIndex=1;  
				TreeDocuments.Nodes[i].ImageIndex=1;          
			}
			for (int i=0;i<Documents.List.Length;i++){
	  		sNewNode=Documents.List[i].DateCreated.ToString("d")+": "+Documents.List[i].Description;
				if(Documents.List[i].ImgType==ImageType.File)
					indexImageList=5;
				else if(Documents.List[i].ImgType==ImageType.Radiograph)
					indexImageList=3;
				else if(Documents.List[i].ImgType==ImageType.Photo)
					indexImageList=4;
				else//document
					indexImageList=2;
				if(Defs.GetOrder(DefCat.ImageCats,Documents.List[i].DocCategory)==-1){//if cat hidden
					MessageBox.Show(Lan.g(this,"There is a document in a hidden category: "
						+Defs.GetName(DefCat.ImageCats,Documents.List[i].DocCategory)
						+". You can unhide this category in Definitions section."));
				}
				else{
					TreeDocuments.Nodes[Defs.GetOrder(DefCat.ImageCats,Documents.List[i].DocCategory)]
						.Nodes.Add(new TreeNode(sNewNode));
					//store docnum in tag of node:
					TreeDocuments.Nodes[Defs.GetOrder(DefCat.ImageCats,Documents.List[i].DocCategory)]
						.LastNode.Tag=Documents.List[i].DocNum;
					TreeDocuments.Nodes[Defs.GetOrder(DefCat.ImageCats,Documents.List[i].DocCategory)]
						.LastNode.ImageIndex=indexImageList;
					TreeDocuments.Nodes[Defs.GetOrder(DefCat.ImageCats,Documents.List[i].DocCategory)]
						.LastNode.SelectedImageIndex=indexImageList;
				}
			}
			TreeDocuments.ExpandAll();
			string SrcFileName="";
			if(keepDoc){
				Documents.GetCurrent(Documents.Cur.DocNum.ToString());
				//MessageBox.Show(Docs.Cur.DocNum.ToString());
				if(Defs.GetOrder(DefCat.ImageCats,Documents.Cur.DocCategory)!=-1){
					foreach(TreeNode n in TreeDocuments.Nodes
						[Defs.GetOrder(DefCat.ImageCats,Documents.Cur.DocCategory)].Nodes){	
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
					case "ScanDoc":
						OnScan_Click("doc");
						break;
					case "ScanXRay":
						OnScan_Click("xray");
						break;
					case "ScanPhoto":
						OnScan_Click("photo");
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
					case "Flip":
						OnFlip_Click();
						break;
					case "RotateL":
						OnRotateL_Click();
						break;
					case "RotateR":
						OnRotateR_Click();
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
			DeleteThumbnail();
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
			DisplayImage(false,true);//because the category may have changed
		}

		private void OnScan_Click(string scanType) {
			//#if(ISXP)
				ScanImage(scanType);
			//#else
			//	MessageBox.Show(Lan.g(this,"Scanning only works on Windows XP."));
			//#endif
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
			DisplayImage(true,false);
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
			DisplayImage(true,true);
		}

		private void OnPaste_Click() {
			IDataObject clipboard=Clipboard.GetDataObject();
			if(!clipboard.GetDataPresent(DataFormats.Bitmap)){
				MessageBox.Show(Lan.g(this,"No bitmap present on clipboard"));	
				return;
			}
			ImageCurrent=(Bitmap)clipboard.GetData(DataFormats.Bitmap);
			RecZoom.Width=0;
			DisplayImage(true,false);
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
			DisplayImage(false,true);
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
				DisplayImage(false,true);
			}
			else{
				ToolBarMain.Buttons["Hand"].Pushed=true;
			}
			IsCropMode=false;
			ToolBarMain.Invalidate();
		}

		private void OnZoomIn_Click() {
			if(ImageCurrent==null)
				return;
			Rectangle sourceRect=GetSourceRect();
			RecZoom.Height=RecZoom.Height/2;
			RecZoom.Width=RecZoom.Width/2;
			if(Documents.Cur.DegreesRotated==90 || Documents.Cur.DegreesRotated==270){
				//sideways
				//maintain original x
				if(Documents.Cur.DegreesRotated==270){
					//maintain the lower edge instead.
					if(Documents.Cur.IsFlipped){
						RecZoom.X=RecZoom.X+RecZoom.Width;
					}
					else{
						RecZoom.X=RecZoom.X+RecZoom.Width;//width is negative
					}
				}
				//maintain original y center
				RecZoom.Y=RecZoom.Y+(RecZoom.Height/2);
				//if white space on bottom
				if((RecZoom.Y+RecZoom.Height>ImageCurrent.Height)&&(RecZoom.Y>0)){
					RecZoom.Y=(ImageCurrent.Height/2)-(RecZoom.Height/2);
					if(RecZoom.Y<0)
						RecZoom.Y=0;
				}
				//if white space on top
				if(RecZoom.Y<0){
					RecZoom.Y=(ImageCurrent.Height/2)-(RecZoom.Height/2);
					if(RecZoom.Y+RecZoom.Height>ImageCurrent.Height)
						RecZoom.Y=ImageCurrent.Height-RecZoom.Height;
				}
			}
			else{//normal
				//maintain original y
				if(Documents.Cur.DegreesRotated==0){
					//do nothing. Y is already correct.
				}
				else{//180
					//maintain the lower edge instead.
					RecZoom.Y=RecZoom.Y+RecZoom.Height;
				}
				//maintain original x center.(RecZoom.x+width)
				RecZoom.X=RecZoom.X+(RecZoom.Width/2);//works if flipped
				if(Documents.Cur.IsFlipped){
					//if white space on right
					if((RecZoom.X>ImageCurrent.Width)&&(RecZoom.X+RecZoom.Width>0)){
						RecZoom.X=(ImageCurrent.Width/2)-(RecZoom.Width/2);
						if(RecZoom.X+RecZoom.Width<0)
							RecZoom.X=-RecZoom.Width;
					}
					//if white space on left
					if(RecZoom.X+RecZoom.Width<0){
						RecZoom.X=sourceRect.X-(ImageCurrent.Width/2)-(RecZoom.Width/2);
						if(RecZoom.X>ImageCurrent.Width)
							RecZoom.X=ImageCurrent.Width;
					}
				}
				else{//not flipped
					//if white space on right
					if((RecZoom.X+RecZoom.Width>ImageCurrent.Width)&&(RecZoom.X>0)){
						RecZoom.X=(ImageCurrent.Width/2)-(RecZoom.Width/2);
						if(RecZoom.X<0)
							RecZoom.X=0;
					}
					//if white space on left
					if(RecZoom.X<0){
						RecZoom.X=(ImageCurrent.Width/2)-(RecZoom.Width/2);
						if(RecZoom.X+RecZoom.Width>ImageCurrent.Width)
							RecZoom.X=ImageCurrent.Width-RecZoom.Width;
					}
				}
			}			
			DisplayImage(false,true);
			//Graphics g=PictureBox1.CreateGraphics();
			//g.DrawImage(ImageCurrent
			//	,new Rectangle(0,0,PictureBox1.ClientRectangle.Width,PictureBox1.ClientRectangle.Height)
			//	,RecZoom, GraphicsUnit.Pixel);
			//g.Dispose();
		}

		private void OnZoomOut_Click() {
			if(ImageCurrent==null)
				return;
			RecZoom.Height=RecZoom.Height*2;
			RecZoom.Width=RecZoom.Width*2;
			RecZoom.X=RecZoom.X-(RecZoom.Width/4);//works for flipped also
			RecZoom.Y=RecZoom.Y-(RecZoom.Height/4);
			//if RecZoom is larger in both dimensions:
			if((RecZoom.Height > ImageCurrent.Height)&&(Math.Abs(RecZoom.Width) > ImageCurrent.Width)){
				RecZoom=GetSourceRect();
				DisplayImage(true,true);
				return;
			}
			string whiteSide=GetWhiteSide();
			//eliminate overhangs on rotating basis ending with opposite from whiteside
			//Debug.WriteLine(whiteSide);
			for(int i=0;i<5;i++){
				//one of the first 4 will be skipped and done on 4 instead.
				//R
				if((i==4 && whiteSide=="L") || (i==0 && whiteSide!="L")){
					//Debug.WriteLine("R");
					if(Documents.Cur.IsFlipped){
						if(RecZoom.X>ImageCurrent.Width){
							RecZoom.X=ImageCurrent.Width;
						}
					}
					else{
						if(RecZoom.X+RecZoom.Width>ImageCurrent.Width){
							RecZoom.X=ImageCurrent.Width-RecZoom.Width;
						}
					}
				}
				//L
				if((i==4 && whiteSide=="R") || (i==1 && whiteSide!="R")){
					//Debug.WriteLine("L");
					if(Documents.Cur.IsFlipped){
						if(RecZoom.X+RecZoom.Width<0){
							RecZoom.X=-RecZoom.Width;
						}
					}
					else{
						if(RecZoom.X<0){
							RecZoom.X=0;
						}
					}
				}
				//T
				if((i==4 && whiteSide=="B") || (i==2 && whiteSide!="B")){
					//Debug.WriteLine("T");
					if(RecZoom.Y<0){
						RecZoom.Y=0;
					}
				}
				//B
				if((i==4 && whiteSide=="T") || (i==3 && whiteSide!="T")){
					//Debug.WriteLine("B");
					if(RecZoom.Y+RecZoom.Height>ImageCurrent.Height){
						RecZoom.Y=ImageCurrent.Height-RecZoom.Height;
					}
				}
			}//for
			DisplayImage(true,true);
			/*Graphics g=PictureBox1.CreateGraphics();
			g.FillRectangle(Brushes.White,0,0
				,PictureBox1.ClientRectangle.Width,PictureBox1.ClientRectangle.Height);
			g.DrawImage(ImageCurrent
				,new Rectangle(0,0,PictureBox1.ClientRectangle.Width,PictureBox1.ClientRectangle.Height)
				,RecZoom,GraphicsUnit.Pixel);
			g.Dispose();*/
		}

		private void OnFlip_Click(){
			if(TreeDocuments.SelectedNode==null || TreeDocuments.SelectedNode.Parent==null){
				return;
			}
			//MessageBox.Show("do flip action");
			Documents.Cur.IsFlipped=!Documents.Cur.IsFlipped;
			if(Documents.Cur.DegreesRotated==90 || Documents.Cur.DegreesRotated==270){
				//then also rotate 180 to maintain illusion of horizontal flip.
				Documents.Cur.DegreesRotated+=180;
				if(Documents.Cur.DegreesRotated>=360){
					Documents.Cur.DegreesRotated-=360;
				}
			}		
			Documents.UpdateCur();
			FillDocList(true);
			DisplayImage(true,true);
			DeleteThumbnail();
		}

		private void OnRotateL_Click(){
			if(TreeDocuments.SelectedNode==null || TreeDocuments.SelectedNode.Parent==null){
				return;
			}
			Documents.Cur.DegreesRotated-=90;
			if(Documents.Cur.DegreesRotated<0){
				Documents.Cur.DegreesRotated+=360;
			}
			Documents.UpdateCur();
			FillDocList(true);
			DisplayImage(true,true);
			DeleteThumbnail();
		}

		private void OnRotateR_Click(){
			if(TreeDocuments.SelectedNode==null || TreeDocuments.SelectedNode.Parent==null){
				return;
			}
			Documents.Cur.DegreesRotated+=90;
			if(Documents.Cur.DegreesRotated>=360){
				Documents.Cur.DegreesRotated-=360;
			}
			Documents.UpdateCur();
			FillDocList(true);
			DisplayImage(true,true);
			DeleteThumbnail();
		}

		///<summary>This is done when deleting and after flipping or rotating.  The next time a thumbnail is needed, it will be created with the proper orientation.</summary>
		private void DeleteThumbnail(){
			string thumbFileName=patFolder+@"Thumbnails\"+Documents.Cur.FileName;
			if(File.Exists(thumbFileName)){
				try{
					File.Delete(thumbFileName);
				}
				catch{}
			}
		}

		///<summary>I am very unhappy with the accuracy of this auto crop tool.  I think it's because GetPixel is so incredibly slow that I had to skip some pixels.  This will be revisited later.</summary>
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
			DisplayImage(true,true);//any flip and rotate will stay in place.
			//New saved image will be in whatever orientation the original was in, but just smaller.
			ImageCurrent.Save(patFolder+Documents.Cur.FileName,ImageFormat.Jpeg);
      //Documents.Cur.LastAltered=DateTime.Today;
      //Documents.UpdateCur();
			FillDocList(true);
			DisplayImage(true,true);
		}

		///<summary>Valid values for scanType are "doc","xray",and "photo"</summary>
		private void ScanImage(string scanType){
			//A user may have more than one scanning device. 
			//The code below will allow the user to select one.
			long wPIXTypes; 
			ShowBlank();                   //sets screen to blank while waiting for image to scan
			ImageCurrent=null;             // current image is null 'til receives value from scan 
	 		wPIXTypes=TWAIN_SelectImageSource(this.Handle);
			if(wPIXTypes==0){//user clicked Cancel
				return;
			}
			//Clipboard.SetDataObject("");
			//Acquire the image to the clipboard
			TWAIN_AcquireToClipboard(this.Handle, wPIXTypes);
			//Transfer the image to the application
			IDataObject oDataObject=Clipboard.GetDataObject(); 
			if(oDataObject.GetDataPresent(DataFormats.Bitmap,true)){ 
				ImageCurrent=(Bitmap)oDataObject.GetData(DataFormats.Bitmap);
			} 
			else if(oDataObject.GetDataPresent(DataFormats.Dib,true)){ 
				ImageCurrent=(Bitmap)oDataObject.GetData(DataFormats.Dib);
			} 
			else{ 
				MessageBox.Show ("The image could not be acquired from the device. Please check to see that the device is properly connected to the computer.");
				return;
			}
			RecZoom.Width=0;
			DisplayImage(true,false);
			Documents.Cur=new Document();
			if(scanType=="doc")
					Documents.Cur.ImgType=ImageType.Document;
				else if(scanType=="xray")
					Documents.Cur.ImgType=ImageType.Radiograph;
				else if(scanType=="photo")
					Documents.Cur.ImgType=ImageType.Photo;
			Documents.Cur.FileName=".jpg";
			formDocInfo2=new FormDocInfo();
			formDocInfo2.IsNew=true;
			formDocInfo2.ShowDialog();
			if(formDocInfo2.DialogResult!=DialogResult.OK){
				return;
			}
			//try{
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
				long qualityL=0;
				if(scanType=="doc"){
					//Possible values 0-100?
					qualityL=(long)Convert.ToInt32(((Pref)Prefs.HList["ScannerCompression"]).ValueString);
				}
				else if(scanType=="xray"){
					qualityL=Convert.ToInt64(((Pref)Prefs.HList["ScannerCompressionRadiographs"]).ValueString);
				}
				else if(scanType=="photo"){
					qualityL=Convert.ToInt64(((Pref)Prefs.HList["ScannerCompressionPhotos"]).ValueString);
				}
				EncoderParameter myEncoderParameter=new EncoderParameter(myEncoder,qualityL);
				myEncoderParameters.Param[0]=myEncoderParameter;
				ImageCurrent.Save(patFolder+Documents.Cur.FileName,myImageCodecInfo,myEncoderParameters);
				FillDocList(true);//adds new doc to DocList and TreeDocument and saves path to image
				DisplayImage(true,true);//sets current image of Docs.Cur image path and displays that image
			//}
			//catch{
			//	MessageBox.Show(Lan.g(this,"Unable to save document."));
			//	Documents.DeleteCur();
			//}
			//}
			//catch(Exception ex){
			//	MessageBox.Show ("ERROR: " + ex.Message);
			//}
			//AutoCrop();??
		}

		/*[Conditional("ISXP")]
		private void OldScanImage(string scanType){
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
			long qualityL=0;
			if(scanType=="doc"){
				//Possible values 0-100?
				qualityL=(long)Convert.ToInt32(((Pref)Prefs.HList["ScannerCompression"]).ValueString);
			}
			else if(scanType=="xray"){
				qualityL=Convert.ToInt64(((Pref)Prefs.HList["ScannerCompressionRadiographs"]).ValueString);
			}
			else if(scanType=="photo"){
				qualityL=Convert.ToInt64(((Pref)Prefs.HList["ScannerCompressionPhotos"]).ValueString);
			}
			EncoderParameter myEncoderParameter=new EncoderParameter(myEncoder,qualityL);
			myEncoderParameters.Param[0]=myEncoderParameter;
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
						DisplayImage(true,false);                             //shows image
						Documents.Cur=new Document();
						if(scanType=="doc")
							Documents.Cur.ImgType=ImageType.Document;
						else if(scanType=="xray")
							Documents.Cur.ImgType=ImageType.Radiograph;
						else if(scanType=="photo")
							Documents.Cur.ImgType=ImageType.Photo;
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
						DisplayImage(true,true);     //sets current image of Docs.Cur image path and displays that image
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
		}//end ScanImage*/

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

		private void TreeDocuments_DoubleClick(object sender, System.EventArgs e) {
			//MouseUp will have just run, so the cur document will have been refreshed.
			string SrcFileName="";
			for(int i=0;i<TreeDocuments.Nodes.Count;i++){
			  if (TreeDocuments.SelectedNode.Equals(TreeDocuments.Nodes[i])){
				  ShowBlank();
				  return;
        }
	    }
			//tag holds the document number of the node
		  //Documents.GetCurrent(TreeDocuments.SelectedNode.Tag.ToString());			SrcFileName = patFolder+Documents.Cur.FileName;
			if(Path.GetExtension(SrcFileName).ToLower()!=".jpg"
				&& Path.GetExtension(SrcFileName).ToLower()!=".gif"
				&& Path.GetExtension(SrcFileName).ToLower()!=".jpeg"){
				try{
					Process.Start(SrcFileName);
				}
				catch(Exception ex){
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void ShowBlank(){
			Graphics g=PictureBox1.CreateGraphics();
			g.FillRectangle(Brushes.White,0,0,PictureBox1.ClientRectangle.Width
				,PictureBox1.ClientRectangle.Height);
			g.Dispose();
		}

		private void PictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e){
			if(ImageCurrent==null){
				return;
			}
			Graphics g=e.Graphics;
			if(Enhanced){
				switch(Documents.Cur.DegreesRotated){
					case 0:
						//
						break;
					case 90:
						g.TranslateTransform(PictureBox1.ClientRectangle.Width,0);
						g.RotateTransform(90);//prepended to matrix transform
						break;
					case 180:
						g.TranslateTransform((float)PictureBox1.ClientRectangle.Width
							,(float)PictureBox1.ClientRectangle.Height);
						g.RotateTransform(180);
						break;
					case 270:
						g.TranslateTransform(0,PictureBox1.ClientRectangle.Height);
						g.RotateTransform(270);
						break;
				}			
				if(Documents.Cur.DegreesRotated==0 || Documents.Cur.DegreesRotated==180){
					g.DrawImage(ImageCurrent
						,new Rectangle(0,0,PictureBox1.ClientRectangle.Width,PictureBox1.ClientRectangle.Height)
						,RecZoom,GraphicsUnit.Pixel);
				}
				else{//90 or 270
					g.DrawImage(ImageCurrent
						,new Rectangle(0,0,PictureBox1.ClientRectangle.Height,PictureBox1.ClientRectangle.Width)
						,RecZoom,GraphicsUnit.Pixel);
				}
			}
			else{
				g.DrawImage(ImageCurrent
					,new Rectangle(0,0,PictureBox1.ClientRectangle.Width,PictureBox1.ClientRectangle.Height)
					,RecZoom,GraphicsUnit.Pixel);
			}
		}

		private void menuExit_Click(object sender, System.EventArgs e){
			//this.Close();
		}

		///<summary>Make sure to set enhanced to false if no Documents.Cur</summary>
		private void DisplayImage(bool clearFirst,bool enhanced){
			Enhanced=enhanced;
			Graphics g=PictureBox1.CreateGraphics();
			if(clearFirst)
				g.FillRectangle(Brushes.White,0,0,PictureBox1.ClientRectangle.Width
					,PictureBox1.ClientRectangle.Height);
			if(ImageCurrent==null){
				g.Dispose();
				return;
			}
			if(RecZoom.Width==0){//need to create new RecZoom
				RecZoom=GetSourceRect();
			}
			RecCrop=new Rectangle();//so it won't show anymore
			if(enhanced){
				switch(Documents.Cur.DegreesRotated){
					case 0:
						//
						break;
					case 90:
						g.TranslateTransform(PictureBox1.ClientRectangle.Width,0);
						g.RotateTransform(90);//prepended to matrix transform
						break;
					case 180:
						g.TranslateTransform((float)PictureBox1.ClientRectangle.Width
							,(float)PictureBox1.ClientRectangle.Height);
						g.RotateTransform(180);
						break;
					case 270:
						g.TranslateTransform(0,PictureBox1.ClientRectangle.Height);
						g.RotateTransform(270);
						break;
				}
			}
			if(Documents.Cur.DegreesRotated==0 || Documents.Cur.DegreesRotated==180){
				g.DrawImage(ImageCurrent
					,new Rectangle(0,0,PictureBox1.ClientRectangle.Width,PictureBox1.ClientRectangle.Height)
					,RecZoom,GraphicsUnit.Pixel);
			}
			else{//90 or 270
				g.DrawImage(ImageCurrent
					,new Rectangle(0,0,PictureBox1.ClientRectangle.Height,PictureBox1.ClientRectangle.Width)
					,RecZoom,GraphicsUnit.Pixel);
			}
			g.Dispose();
		}

		///<summary>Gets the original source rectangle representing the portion of ImageCurrent to display.  Always bigger than ImageCurrent and in proportions of PictureBox client area.  Always white space on one side which depends on the rotation, flip, and proportions.  Used to set initial RecZoom as well as to test when zooming out and dragging to make sure it stays within allowed bounds.</summary>
		private Rectangle GetSourceRect(){
			string whiteSide=GetWhiteSide();//L,R,T,or B
			Rectangle retRect=new Rectangle(0,0,ImageCurrent.Width,ImageCurrent.Height);
			float imageRatio=(float)ImageCurrent.Width/(float)ImageCurrent.Height;
			float screenRatio
				=(float)PictureBox1.ClientRectangle.Width/(float)PictureBox1.ClientRectangle.Height;
			if(!Enhanced){
				if(whiteSide=="L" || whiteSide=="R"){
					retRect.Width=(int)(retRect.Height*screenRatio);
				}
				else{
					retRect.Height=(int)(retRect.Width/screenRatio);
				}
			}
			else if(Documents.Cur.DegreesRotated==0 || Documents.Cur.DegreesRotated==180){
				if(whiteSide=="L" || whiteSide=="R"){
					retRect.Width=(int)(retRect.Height*screenRatio);
				}
				else{
					retRect.Height=(int)(retRect.Width/screenRatio);
				}
				if(Documents.Cur.DegreesRotated==0){
					if(whiteSide=="L"){
						retRect.X=ImageCurrent.Width-retRect.Width;//a negative value
					}
				}
				if(Documents.Cur.DegreesRotated==180){
					if(whiteSide=="T"){
						retRect.Y=ImageCurrent.Height-retRect.Height;//a negative value
					}
					if(whiteSide=="L"){
						retRect.X=ImageCurrent.Width-retRect.Width;//a negative value??
					}
				}
			}
			else if(Documents.Cur.DegreesRotated==90 || Documents.Cur.DegreesRotated==270){
				if(whiteSide=="L" || whiteSide=="R"){//image shorter than screen width
					retRect.Height=(int)(retRect.Width*screenRatio);//make it taller
				}
				else{
					retRect.Width=(int)(retRect.Height/screenRatio);
				}
				if(Documents.Cur.DegreesRotated==90){
					if(whiteSide=="T"){
						retRect.X=ImageCurrent.Width-retRect.Width;//a negative value
					}
					if(whiteSide=="L"){
						retRect.Y=ImageCurrent.Height-retRect.Height;//a negative value
					}
				}
				if(Documents.Cur.DegreesRotated==270){
					if(whiteSide=="T"){
						retRect.X=ImageCurrent.Width-retRect.Width;//a negative value
					}
				}
			}
			if(Documents.Cur.IsFlipped){
				retRect.X=retRect.Right;
				retRect.Width=-retRect.Width;
			}
			return retRect;
		}//GetSourceRect

		///<summary>Returns either L,R,T,or B to indicate which side of the original source rectangle the white space is on.  But the source rectangle will already be rotated and flipped into the proper orientation before considering which side.  So the side is not in image coordinates.  This is used in GetSourceRect. Also by comparing source rect with ImageCurrent, a simple subtraction will tell the amount of white space.  This is used when zooming and dragging to keep the image from going outside the bounds of pict box.</summary>
		private string GetWhiteSide(){
			//Rectangle retRect=new Rectangle(0,0,ImageCurrent.Width,ImageCurrent.Height);
			float imageRatio=(float)ImageCurrent.Width/(float)ImageCurrent.Height;
			float screenRatio
				=(float)PictureBox1.ClientRectangle.Width/(float)PictureBox1.ClientRectangle.Height;
			if(!Enhanced){
				if(imageRatio<screenRatio){//if image narrower than picturebox
					return "R";
				}
				else{//if image shorter than picturebox
					return "B";
				}
			}
			if(Documents.Cur.DegreesRotated==0){
				if(imageRatio<screenRatio){//if image narrower
					if(Documents.Cur.IsFlipped)
						return "L";
					else
						return "R";
				}
				else{//if image shorter
					return "B";
				}
			}
			if(Documents.Cur.DegreesRotated==90){//imagine image on side when doing calculations
				//if the image(laid sideways) is proprortionally narrower than the picturebox
  			if((1/imageRatio)<screenRatio){
					return "L";
				}
				else{//if image is shorter
					if(Documents.Cur.IsFlipped)
						return "T";
					else
						return "B";
				}
			}
			else if(Documents.Cur.DegreesRotated==180){
				if(imageRatio<screenRatio){//if image narrower
					if(Documents.Cur.IsFlipped)
						return "R";
					else
						return "L";
				}
				else{//if image shorter
					return "T";
				}
			}
			else{ //if(Documents.Cur.DegreesRotated==270){
  			if((1/imageRatio)<screenRatio){//if image(laid sideways) is narrower
					//adjust retRect to be wider so it actually extends beyond image boundaries
					return "R";
				}
				else{//if image is shorter
					if(Documents.Cur.IsFlipped)
						return "B";
					else
						return "T";
				}
			}
		}//GetWhiteSide()

			/*old code
				if(((double)ImageCurrent.Height/(double)ImageCurrent.Width)
					<((double)PictureBox1.ClientRectangle.Width/(double)PictureBox1.ClientRectangle.Height))
				{
					//adjust narrower
					drawW=(int)(drawH*(double)ImageCurrent.Height/(double)ImageCurrent.Width);
				}
				else{//if image is proportionally shorter than picturebox
					//adjust height instead.
					drawH=(int)(drawW*(double)ImageCurrent.Width/(double)ImageCurrent.Height);
				}
			if(((double)ImageCurrent.Width/(double)ImageCurrent.Height)
					<((double)PictureBox1.ClientRectangle.Width/(double)PictureBox1.ClientRectangle.Height))
				{
					//adjust narrower
					drawW=(int)(drawH*(double)ImageCurrent.Width/(double)ImageCurrent.Height);
				}
				else{
					//adjust height instead.
					drawH=(int)(drawW*(double)ImageCurrent.Height/(double)ImageCurrent.Width);
				}
			//starting h&w.  Always drawn at 0,0
			//int drawW=PictureBox1.ClientRectangle.Width;
			//int drawH=PictureBox1.ClientRectangle.Height;
			//Debug.WriteLine(drawW.ToString()+","+drawH.ToString());*/
		

		private void PictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e){
			//if(toolBarButHand.Pushed){//Hand Mode
			//if(butHand.FlatStyle==FlatStyle.Standard){//hand mode
			if(ToolBarMain.Buttons["Hand"].Pushed){//hand mode
				MouseDownOrigin=new Point(e.X,e.Y);
				MouseIsDown=true;
				//RecZoom is already established and will not change until after MouseUp
			}
			else{//Crop Mode
				MouseDownOrigin=new Point(e.X,e.Y);
				MouseIsDown=true;
			}
		}		

		private void PictureBox1_MouseMove(object sender,System.Windows.Forms.MouseEventArgs e){
			if(ImageCurrent==null)
				return;
			Graphics g=PictureBox1.CreateGraphics();
			switch(Documents.Cur.DegreesRotated){
				case 0:
					//
					break;
				case 90:
					g.TranslateTransform(PictureBox1.ClientRectangle.Width,0);
					g.RotateTransform(90);//prepended to matrix transform
					break;
				case 180:
					g.TranslateTransform((float)PictureBox1.ClientRectangle.Width
						,(float)PictureBox1.ClientRectangle.Height);
					g.RotateTransform(180);
					break;
				case 270:
					g.TranslateTransform(0,PictureBox1.ClientRectangle.Height);
					g.RotateTransform(270);
					break;
			}
			string whiteSide=GetWhiteSide();//L,R,T,or B
			if(ToolBarMain.Buttons["Hand"].Pushed){//hand mode
				if(!MouseIsDown)
					return;
				RecTempZoom=RecZoom;
				//these are the amounts the rectangle moves across the image. They are negative, because
				//as the mouse drags down, the rectangle must move up to expose the upper part of the image.
				int moveX=(int)(-(e.X-MouseDownOrigin.X)*2*
					Math.Abs((decimal)RecZoom.Width)/(decimal)PictureBox1.ClientRectangle.Width);
				//Debug.WriteLine(moveX);
				int moveY=-(e.Y-MouseDownOrigin.Y)*2* RecZoom.Height/PictureBox1.ClientRectangle.Height;
				if(Documents.Cur.DegreesRotated==0){
					RecTempZoom.Y=RecZoom.Y+moveY;
					if(Documents.Cur.IsFlipped){
						RecTempZoom.X=RecZoom.X-moveX;
					}
					else{//normal
						RecTempZoom.X=RecZoom.X+moveX;
					}
				}
				else if(Documents.Cur.DegreesRotated==90){
					RecTempZoom.Y=RecZoom.Y-moveX;
					if(Documents.Cur.IsFlipped){
						RecTempZoom.X=RecZoom.X-moveY;
					}
					else{
						RecTempZoom.X=RecZoom.X+moveY;
					}
				}
				else if(Documents.Cur.DegreesRotated==180){
					RecTempZoom.Y=RecZoom.Y-moveY;
					if(Documents.Cur.IsFlipped){
						RecTempZoom.X=RecZoom.X+moveX;
					}
					else{
						RecTempZoom.X=RecZoom.X-moveX;
					}
				}
				else if(Documents.Cur.DegreesRotated==270){
					RecTempZoom.Y=RecZoom.Y+moveX;
					if(Documents.Cur.IsFlipped){
						RecTempZoom.X=RecZoom.X+moveY;
					}
					else{
						RecTempZoom.X=RecZoom.X-moveY;
					}
				}
				Rectangle sourceRect=GetSourceRect();
				//remember that white side is considered after the image is already rotated and flipped.
				//BUT: from here on down, white side will be relative to RecZoom BEFORE rotation and flip.
				//Yes, this is confusing, but I'd like to see anyone come up with something better.
				//these track whether there was whitespace when the drag was started.
				bool startedWhiteR=false;
				bool startedWhiteL=false;
				bool startedWhiteB=false;
				if(RecZoom.Y+RecZoom.Height>ImageCurrent.Height){
					if(Documents.Cur.DegreesRotated==0 && whiteSide=="B")
						startedWhiteB=true;
					else if(Documents.Cur.DegreesRotated==90 && whiteSide=="L")
						startedWhiteB=true;
					else if(Documents.Cur.DegreesRotated==180 && whiteSide=="T")
						startedWhiteB=true;
					else if(Documents.Cur.DegreesRotated==270 && whiteSide=="R")
						startedWhiteB=true;
				}
				if(RecZoom.X+RecZoom.Width>ImageCurrent.Width){
					if(Documents.Cur.DegreesRotated==0 && whiteSide=="R")
						startedWhiteR=true;
					else if(Documents.Cur.DegreesRotated==90 && whiteSide=="B")
						startedWhiteR=true;
					else if(Documents.Cur.DegreesRotated==180 && whiteSide=="L")
						startedWhiteR=true;
					else if(Documents.Cur.DegreesRotated==270 && whiteSide=="T")
						startedWhiteR=true;
				}
				if(RecZoom.X+RecZoom.Width<0){//width is neg because only for flipped
					if(Documents.Cur.DegreesRotated==0 && whiteSide=="L")
						startedWhiteL=true;
					else if(Documents.Cur.DegreesRotated==90 && whiteSide=="T")
						startedWhiteL=true;
					else if(Documents.Cur.DegreesRotated==180 && whiteSide=="R")
						startedWhiteL=true;
					else if(Documents.Cur.DegreesRotated==270 && whiteSide=="B")
						startedWhiteL=true;
				}
				//limit movement to right
				if(startedWhiteR){
					//same for flipped and regular
					if(RecTempZoom.X>RecZoom.X)
						RecTempZoom.X=RecZoom.X;
				}
				else{
					if(Documents.Cur.IsFlipped){
						if(RecTempZoom.X>ImageCurrent.Width)
							RecTempZoom.X=ImageCurrent.Width;
					}
					else{
						if(RecTempZoom.X+RecTempZoom.Width>ImageCurrent.Width)
							RecTempZoom.X=ImageCurrent.Width-RecTempZoom.Width;
					}
				}
				//limit movement to left
				if(startedWhiteL){
					//same for flipped and regular
					if(RecTempZoom.X<RecZoom.X)
						RecTempZoom.X=RecZoom.X;
				}
				else{
					if(Documents.Cur.IsFlipped){
						if(RecTempZoom.X+RecTempZoom.Width<0)
							RecTempZoom.X=-RecTempZoom.Width;
					}
					else{
						if(RecTempZoom.X<0)
							RecTempZoom.X=0;
					}
				}
				//limit movement to bottom
				if(startedWhiteB){
					if(RecTempZoom.Y>RecZoom.Y)
						RecTempZoom.Y=RecZoom.Y;
				}
				else{
					if(RecTempZoom.Y+RecTempZoom.Height > ImageCurrent.Height)
						RecTempZoom.Y=ImageCurrent.Height-RecTempZoom.Height;
				}
				//limit movement to top.
				if(RecTempZoom.Y<0)
					RecTempZoom.Y=0;
				//draw
				if(Documents.Cur.DegreesRotated==0 || Documents.Cur.DegreesRotated==180){
					g.DrawImage(ImageCurrent
						,new Rectangle(0,0,PictureBox1.ClientRectangle.Width,PictureBox1.ClientRectangle.Height)
						,RecTempZoom,GraphicsUnit.Pixel);
				}
				else{//90 or 270
					g.DrawImage(ImageCurrent
						,new Rectangle(0,0,PictureBox1.ClientRectangle.Height,PictureBox1.ClientRectangle.Width)
						,RecTempZoom,GraphicsUnit.Pixel);
				}
			}
			else{//Crop Mode				float ratio=1;				if(Documents.Cur.DegreesRotated==0 || Documents.Cur.DegreesRotated==180){					ratio=(float)PictureBox1.ClientRectangle.Width/Math.Abs(RecZoom.Width);				}				else{					ratio=(float)PictureBox1.ClientRectangle.Width/Math.Abs(RecZoom.Height);				}				int rBound=0;//in picturebox coordinates				int bBound=0;				if(Documents.Cur.DegreesRotated==0 || Documents.Cur.DegreesRotated==180){					rBound=(int)(ImageCurrent.Width*ratio);					bBound=(int)(ImageCurrent.Height*ratio);				}				else{					rBound=(int)(ImageCurrent.Height*ratio);					bBound=(int)(ImageCurrent.Width*ratio);				}				if(Documents.Cur.DegreesRotated==0 || Documents.Cur.DegreesRotated==180){
					g.DrawImage(ImageCurrent
						,new Rectangle(0,0,PictureBox1.ClientRectangle.Width,PictureBox1.ClientRectangle.Height)
						,RecZoom,GraphicsUnit.Pixel);
				}
				else{//90 or 270
					g.DrawImage(ImageCurrent
						,new Rectangle(0,0,PictureBox1.ClientRectangle.Height,PictureBox1.ClientRectangle.Width)
						,RecZoom,GraphicsUnit.Pixel);
				}				if(MouseIsDown){					RecCrop=new Rectangle();					RecCrop.X=MouseDownOrigin.X;					RecCrop.Y=MouseDownOrigin.Y;					RecCrop.Width=e.X-MouseDownOrigin.X;					RecCrop.Height=e.Y-MouseDownOrigin.Y;					if(e.X > rBound){						RecCrop.Width=(int)rBound-RecCrop.X;					}					if(e.Y > bBound){						RecCrop.Height=(int)bBound-RecCrop.Y;					}					/*if(RecZoom.X*ratio+e.X > rBound){						RecCrop.Width=(int)(rBound-RecZoom.X*ratio)-RecCrop.X;					}					if(RecZoom.Y*ratio+e.Y > bBound){						RecCrop.Height=(int)(bBound-RecZoom.Y*ratio)-RecCrop.Y;					}*/					//need to unmangle rectangle?? not too important...					g.ResetTransform();					g.DrawRectangle(new Pen(Color.Blue),RecCrop);				}				else{//mouse is up 					g.ResetTransform();					if(e.X<rBound){						g.DrawLine(new Pen(Color.Blue),new Point(e.X,0),new Point(e.X,bBound));					}					if(e.Y<bBound){						g.DrawLine(new Pen(Color.Blue),new Point(0,e.Y),new Point(rBound,e.Y));					}					g.DrawRectangle(new Pen(Color.Blue),RecCrop);				}			}
			g.Dispose();
		}//end mousemove

		private void PictureBox1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e){
			MouseIsDown=false;
			if(ToolBarMain.Buttons["Hand"].Pushed){//hand mode
				RecZoom=RecTempZoom;
			}
			else{//Crop Mode
				//Graphics grfx=PictureBox1.CreateGraphics();
				if(RecCrop.Width==0 || RecCrop.Height==0){
					return;//the size of a rectangle must be larger than 0
				}
				if(MessageBox.Show(Lan.g(this,"Crop to Rectangle?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
					return;
				//math to convert RecCrop from client coord to image coords:
				RectangleF sourceRect=new RectangleF();//in image coordinates.
				//sourceRect has positive width
				float ratio=1;
				if(Documents.Cur.DegreesRotated==0 || Documents.Cur.DegreesRotated==180){					ratio=Math.Abs(RecZoom.Width)/(float)PictureBox1.ClientRectangle.Width;				}				else{					ratio=RecZoom.Height/(float)PictureBox1.ClientRectangle.Width;				}
				
				if(Documents.Cur.DegreesRotated==0){
					if(Documents.Cur.IsFlipped){
						sourceRect.X     = RecZoom.Left  -(ratio*RecCrop.Right);
						sourceRect.Y     = RecZoom.Top   +(ratio*RecCrop.Top);
						sourceRect.Width = RecCrop.Width *ratio;
						sourceRect.Height= RecCrop.Height*ratio;
					}
					else{
						sourceRect.X     = RecZoom.Left  +(ratio*RecCrop.Left);
						sourceRect.Y     = RecZoom.Top   +(ratio*RecCrop.Top);
						sourceRect.Width = RecCrop.Width *ratio;
						sourceRect.Height= RecCrop.Height*ratio;
					}
				}
				else if(Documents.Cur.DegreesRotated==90){
					if(Documents.Cur.IsFlipped){
						sourceRect.X     = RecZoom.Left -(ratio*RecCrop.Bottom);
						sourceRect.Y     = RecZoom.Bottom-(ratio*RecCrop.Right);
						sourceRect.Width = RecCrop.Height*ratio;
						sourceRect.Height= RecCrop.Width*ratio;
					}
					else{
						sourceRect.X     = RecZoom.Left  +(ratio*RecCrop.Top);
						sourceRect.Y     = RecZoom.Bottom-(ratio*RecCrop.Right);
						sourceRect.Width = RecCrop.Height*ratio;
						sourceRect.Height= RecCrop.Width*ratio;
					}
				}
				else if(Documents.Cur.DegreesRotated==180){
					if(Documents.Cur.IsFlipped){
						sourceRect.X     = RecZoom.Right  +(ratio*RecCrop.Left);
						sourceRect.Y     = RecZoom.Bottom-(ratio*RecCrop.Bottom);
						sourceRect.Width = RecCrop.Width*ratio;
						sourceRect.Height= RecCrop.Height*ratio;
					}
					else{
						sourceRect.X     = RecZoom.Right -(ratio*RecCrop.Right);
						sourceRect.Y     = RecZoom.Bottom-(ratio*RecCrop.Bottom);
						sourceRect.Width = RecCrop.Width*ratio;
						sourceRect.Height= RecCrop.Height*ratio;
					}
				}
				else if(Documents.Cur.DegreesRotated==270){
					if(Documents.Cur.IsFlipped){
						sourceRect.X     = RecZoom.Right  +(ratio*RecCrop.Top);
						sourceRect.Y     = RecZoom.Top   +(ratio*RecCrop.Left);
						sourceRect.Width = RecCrop.Height*ratio;
						sourceRect.Height= RecCrop.Width*ratio;
					}
					else{
						sourceRect.X     = RecZoom.Right -(ratio*RecCrop.Bottom);
						sourceRect.Y     = RecZoom.Top   +(ratio*RecCrop.Left);
						sourceRect.Width = RecCrop.Height*ratio;
						sourceRect.Height= RecCrop.Width*ratio;
					}
				}
				Bitmap bitmapTemp=new Bitmap(1,1);//just to get the horizontal res
				float ratio2=(float)bitmapTemp.HorizontalResolution
					/(float)ImageCurrent.HorizontalResolution;// 96/150
				bitmapTemp=new Bitmap((int)(sourceRect.Width*ratio2),(int)(sourceRect.Height*ratio2));
				Graphics gTemp=Graphics.FromImage(bitmapTemp);//we're going to draw on bitmapTemp
				gTemp.DrawImage(ImageCurrent,0,0,sourceRect,GraphicsUnit.Pixel);
				gTemp.Dispose();
 				ImageCurrent=(Bitmap)bitmapTemp.Clone();
				RecZoom.Width=0;//DisplayImage will then recreate RecZoom
				DisplayImage(true,true);
				//the cropped image will stay in the same orientation as the original
				ImageCurrent.Save(patFolder+Documents.Cur.FileName,ImageFormat.Jpeg);
        //Documents.Cur.LastAltered=DateTime.Today;
        //Documents.UpdateCur();
				FillDocList(true);
				DisplayImage(true,true);
			}
		}

		/*private void treeDocuments_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e){
			//later change the event to mouse up instead of AfterSelect for more intuitive response.
			
		}*/

		private void TreeDocuments_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			TreeNode selectedNode=TreeDocuments.GetNodeAt(e.X,e.Y);
			if(selectedNode==null){
				//nothing happens if user does not click on a node
				return;
			}
			if(selectedNode.Parent==null){//category node
				TreeDocuments.SelectedNode=selectedNode;
				TreeDocuments.ContextMenu=null;
				ShowBlank();
				return;//no dragging
	    }
			if(e.Button==MouseButtons.Right && TreeDocuments.SelectedNode==selectedNode){
				TreeDocuments.ContextMenu=contextTree;
				return;
				//but if rightclicked on a different node, then show the image before showing context menu
			}
			TreeDocuments.SelectedNode=selectedNode;
			MouseIsDownOnTree=true;
			TreeOriginalMouse=new Point(e.X,e.Y);
			TreeDocuments.HotTracking=true;
			//then, display image:
			string SrcFileName="";
			//tag holds the document number of the node
		  Documents.GetCurrent(TreeDocuments.SelectedNode.Tag.ToString());			SrcFileName=patFolder+Documents.Cur.FileName;			try{				//I just used webrequest for kicks. It is faster to use Image.FromFile().		    WebRequest request=WebRequest.Create(SrcFileName); 			  WebResponse response=request.GetResponse();				//MessageBox.Show(Path.GetExtension(SrcFileName));				if(Path.GetExtension(SrcFileName).ToLower()==".jpg"					|| Path.GetExtension(SrcFileName).ToLower()==".gif"					|| Path.GetExtension(SrcFileName).ToLower()==".jpeg")				{					ImageCurrent=(Bitmap)System.Drawing.Bitmap.FromStream(response.GetResponseStream());				}				else{					ImageCurrent=null;//may not be necessary				}			  response.Close();	    }		  catch(System.Exception exception){		    MessageBox.Show(Lan.g(this,exception.Message)); 				ImageCurrent=null;	    }
		  RecZoom.Width=0;
		  DisplayImage(true,true);//clears image, then displays current
		}

		private void TreeDocuments_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(!MouseIsDownOnTree){
				return;
			}
			//otherwise, we are dragging
			if(TreeOriginalMouse.Y-e.Y>-4 && TreeOriginalMouse.Y-e.Y<4){//mouse not moved very much
				Cursor=Cursors.Default;
			}
			else{//if mouse has moved a lot
				Cursor=Cursors.Hand;
			}
		}

		private void TreeDocuments_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Click event still not registered, but we manually set the selectedNode
			if(TreeDocuments.SelectedNode==null){
				return;
			}
			if(TreeDocuments.SelectedNode.Parent==null){//category node
				return;
			}
			MouseIsDownOnTree=false;
			Cursor=Cursors.Default;
			TreeDocuments.HotTracking=false;
			TreeNode upNode=TreeDocuments.GetNodeAt(e.X,e.Y);
			if(upNode==null){//mouse not up on a tree node
				//do nothing
			}
			else if(TreeOriginalMouse.Y-e.Y>-4 && TreeOriginalMouse.Y-e.Y<4){//mouse not moved very much
				//do nothing
			}
			else{
				//move to new category
				if(upNode.Parent==null){//category node
					Documents.Cur.DocCategory=Defs.Short[(int)DefCat.ImageCats][upNode.Index].DefNum;
				}
				else{
					Documents.Cur.DocCategory=Documents.GetCategory(upNode.Tag.ToString());
				}
				Documents.UpdateCur();
				FillDocList(true);
			}
		}

		private void menuTree_Click(object sender, System.EventArgs e) {
			switch(((MenuItem)sender).Index){
				case 0://print
					OnPrint_Click();
					break;
				case 1://delete
					OnDelete_Click();
					break;
				case 2://info
					OnInfo_Click();
					break;
			}
		}

		

		

		
	}
}









