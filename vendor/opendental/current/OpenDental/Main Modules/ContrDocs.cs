/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
//#define ISXP
using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Security.Cryptography;
using System.Text; 
using System.Windows.Forms;
//using WIALib;
using OpenDental.UI;
using OpenDentBusiness;
using Tao.OpenGl;
using CodeBase;

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
		///<summary>When dragging on Picturebox, this is the starting point in PictureBox coordinates.</summary>
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
    private Stream myStream;
    //private FormDocInfo formDocInfo2;
		///<summary>The path to the patient folder, including the letter folder, and ending with \.  It's public for NewPatientForm.com functionality.</summary>
		public string patFolder;
		private OpenDental.UI.ODToolBar ToolBarMain;
		//private string imageFileName;
		///<summary>Starts out as false. It's only used when repainting the toolbar, not to test mode.</summary>
		private bool IsCropMode;//
		private bool MouseIsDownOnTree;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.ContextMenu contextTree;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private Point TreeOriginalMouse;
		private System.Windows.Forms.ContextMenu menuPatient;
		private Panel panelNote;
		private Label label1;
		private TextBox textNote;
		private SignatureBox sigBox;
		private Label label15;
		private Topaz.SigPlusNET sigBoxTopaz;
		private Label labelInvalidSig;
		///<summary>Set when DisplayImage is run. It is false if no Documents.Cur.</summary>
		private bool Enhanced;
		///<summary></summary>
		[Category("Data"),Description("Occurs when user changes current patient, usually by clicking on the Select Patient button.")]
		public event PatientSelectedEventHandler PatientSelected=null;
		// declarations, spk 10/05/04
		///<summary></summary>
		[System.Runtime.InteropServices.DllImport("EZTW32.DLL")]
		public static extern int TWAIN_AcquireToFilename(IntPtr hwndApp, string bmpFileName); 
		///<summary></summary>
		[System.Runtime.InteropServices.DllImport("EZTW32.DLL")] 
		public static extern int TWAIN_SelectImageSource(IntPtr hwndApp); 
		///<summary></summary>
		[System.Runtime.InteropServices.DllImport("EZTW32.DLL")] 
		public static extern int TWAIN_AcquireToClipboard(IntPtr hwndApp, long wPixTypes); 
		///<summary></summary>
		[System.Runtime.InteropServices.DllImport("EZTW32.DLL")] 
		public static extern int TWAIN_IsAvailable(); 
		///<summary></summary>
		[System.Runtime.InteropServices.DllImport("EZTW32.DLL")] 
		public static extern int TWAIN_EasyVersion();// spk 10/05/04
		///<summary>The only reason this is public is for NewPatientForm.com functionality.</summary>
		public Patient PatCur;
		private Family FamCur;
		private Document[] DocumentList;
		private Document DocCur;
		private ContextMenu menuForms;
		private List<DocAttach> DocAttachList;
		private bool ShowNote;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContrDocs));
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
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuExit = new System.Windows.Forms.MenuItem();
			this.menuPrefs = new System.Windows.Forms.MenuItem();
			this.menuPatient = new System.Windows.Forms.ContextMenu();
			this.panelNote = new System.Windows.Forms.Panel();
			this.labelInvalidSig = new System.Windows.Forms.Label();
			this.sigBoxTopaz = new Topaz.SigPlusNET();
			this.sigBox = new OpenDental.UI.SignatureBox();
			this.label15 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
			this.panelNote.SuspendLayout();
			this.SuspendLayout();
			// 
			// TreeDocuments
			// 
			this.TreeDocuments.ContextMenu = this.contextTree;
			this.TreeDocuments.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.TreeDocuments.FullRowSelect = true;
			this.TreeDocuments.HideSelection = false;
			this.TreeDocuments.ImageIndex = 2;
			this.TreeDocuments.ImageList = this.imageListTree;
			this.TreeDocuments.Indent = 20;
			this.TreeDocuments.Location = new System.Drawing.Point(0,33);
			this.TreeDocuments.Name = "TreeDocuments";
			this.TreeDocuments.SelectedImageIndex = 2;
			this.TreeDocuments.Size = new System.Drawing.Size(228,519);
			this.TreeDocuments.TabIndex = 0;
			this.TreeDocuments.DoubleClick += new System.EventHandler(this.TreeDocuments_DoubleClick);
			this.TreeDocuments.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TreeDocuments_MouseUp);
			this.TreeDocuments.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TreeDocuments_MouseMove);
			this.TreeDocuments.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeDocuments_MouseDown);
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
			this.imageListTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTree.ImageStream")));
			this.imageListTree.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListTree.Images.SetKeyName(0,"");
			this.imageListTree.Images.SetKeyName(1,"");
			this.imageListTree.Images.SetKeyName(2,"");
			this.imageListTree.Images.SetKeyName(3,"");
			this.imageListTree.Images.SetKeyName(4,"");
			this.imageListTree.Images.SetKeyName(5,"");
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
			this.imageListTools2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTools2.ImageStream")));
			this.imageListTools2.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListTools2.Images.SetKeyName(0,"");
			this.imageListTools2.Images.SetKeyName(1,"");
			this.imageListTools2.Images.SetKeyName(2,"");
			this.imageListTools2.Images.SetKeyName(3,"");
			this.imageListTools2.Images.SetKeyName(4,"");
			this.imageListTools2.Images.SetKeyName(5,"");
			this.imageListTools2.Images.SetKeyName(6,"");
			this.imageListTools2.Images.SetKeyName(7,"");
			this.imageListTools2.Images.SetKeyName(8,"");
			this.imageListTools2.Images.SetKeyName(9,"");
			this.imageListTools2.Images.SetKeyName(10,"");
			this.imageListTools2.Images.SetKeyName(11,"");
			this.imageListTools2.Images.SetKeyName(12,"");
			this.imageListTools2.Images.SetKeyName(13,"");
			this.imageListTools2.Images.SetKeyName(14,"");
			this.imageListTools2.Images.SetKeyName(15,"");
			this.imageListTools2.Images.SetKeyName(16,"");
			this.imageListTools2.Images.SetKeyName(17,"copy.gif");
			// 
			// PictureBox1
			// 
			this.PictureBox1.BackColor = System.Drawing.SystemColors.Window;
			this.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PictureBox1.Location = new System.Drawing.Point(232,33);
			this.PictureBox1.Name = "PictureBox1";
			this.PictureBox1.Size = new System.Drawing.Size(600,400);
			this.PictureBox1.TabIndex = 6;
			this.PictureBox1.TabStop = false;
			this.PictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseDown);
			this.PictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseMove);
			this.PictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox1_Paint);
			this.PictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseUp);
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
			// panelNote
			// 
			this.panelNote.Controls.Add(this.labelInvalidSig);
			this.panelNote.Controls.Add(this.sigBoxTopaz);
			this.panelNote.Controls.Add(this.sigBox);
			this.panelNote.Controls.Add(this.label15);
			this.panelNote.Controls.Add(this.label1);
			this.panelNote.Controls.Add(this.textNote);
			this.panelNote.Location = new System.Drawing.Point(234,439);
			this.panelNote.Name = "panelNote";
			this.panelNote.Size = new System.Drawing.Size(705,114);
			this.panelNote.TabIndex = 11;
			this.panelNote.DoubleClick += new System.EventHandler(this.panelNote_DoubleClick);
			// 
			// labelInvalidSig
			// 
			this.labelInvalidSig.BackColor = System.Drawing.SystemColors.Window;
			this.labelInvalidSig.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelInvalidSig.Location = new System.Drawing.Point(414,35);
			this.labelInvalidSig.Name = "labelInvalidSig";
			this.labelInvalidSig.Size = new System.Drawing.Size(196,59);
			this.labelInvalidSig.TabIndex = 94;
			this.labelInvalidSig.Text = "Invalid Signature -  Document or note has changed since it was signed.";
			this.labelInvalidSig.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labelInvalidSig.DoubleClick += new System.EventHandler(this.labelInvalidSig_DoubleClick);
			// 
			// sigBoxTopaz
			// 
			this.sigBoxTopaz.Location = new System.Drawing.Point(437,15);
			this.sigBoxTopaz.Name = "sigBoxTopaz";
			this.sigBoxTopaz.Size = new System.Drawing.Size(394,91);
			this.sigBoxTopaz.TabIndex = 93;
			this.sigBoxTopaz.Text = "sigPlusNET1";
			this.sigBoxTopaz.DoubleClick += new System.EventHandler(this.sigBoxTopaz_DoubleClick);
			// 
			// sigBox
			// 
			this.sigBox.Location = new System.Drawing.Point(308,20);
			this.sigBox.Name = "sigBox";
			this.sigBox.Size = new System.Drawing.Size(394,91);
			this.sigBox.TabIndex = 90;
			this.sigBox.DoubleClick += new System.EventHandler(this.sigBox_DoubleClick);
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(305,0);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(126,18);
			this.label15.TabIndex = 87;
			this.label15.Text = "Signature";
			this.label15.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.label15.DoubleClick += new System.EventHandler(this.label15_DoubleClick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0,0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,18);
			this.label1.TabIndex = 1;
			this.label1.Text = "Note";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.label1.DoubleClick += new System.EventHandler(this.label1_DoubleClick);
			// 
			// textNote
			// 
			this.textNote.BackColor = System.Drawing.SystemColors.Window;
			this.textNote.Location = new System.Drawing.Point(0,20);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.ReadOnly = true;
			this.textNote.Size = new System.Drawing.Size(302,91);
			this.textNote.TabIndex = 0;
			this.textNote.DoubleClick += new System.EventHandler(this.textNote_DoubleClick);
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListTools2;
			this.ToolBarMain.Location = new System.Drawing.Point(0,0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(939,29);
			this.ToolBarMain.TabIndex = 10;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// ContrDocs
			// 
			this.Controls.Add(this.panelNote);
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.PictureBox1);
			this.Controls.Add(this.TreeDocuments);
			this.Name = "ContrDocs";
			this.Size = new System.Drawing.Size(939,606);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ContrDocs_Layout);
			this.Load += new System.EventHandler(this.ContrDocs_Load);
			this.Resize += new System.EventHandler(this.ContrDocs_Resize);
			((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
			this.panelNote.ResumeLayout(false);
			this.panelNote.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private void ContrDocs_Layout(object sender, System.Windows.Forms.LayoutEventArgs e){
			//the problem is that this event fires way too often to be useful.
      //so everything has been moved into Resize
		}

		private void ContrDocs_Resize(object sender,EventArgs e) {
			TreeDocuments.Height= Height-TreeDocuments.Location.Y-2;
		  PictureBox1.Width=Width-PictureBox1.Location.X-2;
			PictureBox1.Height=Height-PictureBox1.Location.Y-2;
			panelNote.Location=new Point(PictureBox1.Left,PictureBox1.Bottom-panelNote.Height); 
			panelNote.Width=PictureBox1.Width;
			//now, the panelNote is in just the right position.  It just has to be made visible, and the picturebox shortened.
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
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Sign"),-1,Lan.g(this,"Sign this document"),"Sign"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			button=new ODToolBarButton(Lan.g(this,"Scan:"),-1,"","");
			button.Style=ODToolBarButtonStyle.Label;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton("",14,Lan.g(this,"Scan Document"),"ScanDoc"));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",16,Lan.g(this,"Scan Radiograph"),"ScanXRay"));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",15,Lan.g(this,"Scan Photo"),"ScanPhoto"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Import"),5,Lan.g(this,"Import From File"),"Import"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Copy"),17,Lan.g(this,"Copy displayed image to clipboard"),"Copy"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Paste"),6,Lan.g(this,"Paste From Clipboard"),"Paste"));
			button=new ODToolBarButton(Lan.g(this,"Forms"),-1,"","Forms");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			menuForms=new ContextMenu();
			if(Directory.Exists(PrefB.GetString("DocPath")+"Forms")){
				DirectoryInfo dirInfo=new DirectoryInfo(PrefB.GetString("DocPath")+"Forms");
				FileInfo[] fileInfos=dirInfo.GetFiles();
				for(int i=0;i<fileInfos.Length;i++){
					menuForms.MenuItems.Add(fileInfos[i].Name,new System.EventHandler(menuForms_Click));
				}
			}
			button.DropDownMenu=menuForms;
			ToolBarMain.Buttons.Add(button);
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
			button=new ODToolBarButton(Lan.g(this,"Rotate:"),-1,"","");
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
		public void ModuleSelected(int patNum){
			RefreshModuleData(patNum);
			RefreshModuleScreen();
		}

		///<summary></summary>
		public void ModuleUnselected(){
			FamCur=null;
			PatCur=null;
			//from FillDocList:
			DocAttachList=null;
			DocumentList=null;
		}

		///<summary>This is public for NewPatientForm functionality.</summary>
  	public void RefreshModuleData(int patNum){
			if(patNum==0){
				PatCur=null;
				FamCur=null;
				return;
			}
			FamCur=Patients.GetFamily(patNum);
			PatCur=FamCur.GetPatient(patNum);
			if(ParentForm != null){ //Added so NewPatientform can have access without showing
				ParentForm.Text=Patients.GetMainTitle(PatCur);
			}
			if(PatCur.ImageFolder==""){//creates new folder for patient if none present
				string name=PatCur.LName+PatCur.FName;
				string folder="";
				for(int i=0;i<name.Length;i++){
					if(Char.IsLetter(name,i)){
						folder+=name.Substring(i,1);
					}
				}
				folder+=PatCur.PatNum.ToString();//ensures unique name
				try{
					Patient PatOld=PatCur.Copy();
					PatCur.ImageFolder=folder;
					patFolder=((Pref)PrefB.HList["DocPath"]).ValueString
						+PatCur.ImageFolder.Substring(0,1)+@"\"
						+PatCur.ImageFolder+@"\";
					Directory.CreateDirectory(patFolder);
					Patients.Update(PatCur,PatOld);
				}
				catch{
					MessageBox.Show(Lan.g(this,"Error.  Could not create folder for patient. "));
					return;
				}
			}
			else{//patient folder already created once
				patFolder=PrefB.GetString("DocPath")
					+PatCur.ImageFolder.Substring(0,1)+@"\"
					+PatCur.ImageFolder+@"\";
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
			DocAttachList=DocAttaches.Refresh(PatCur.PatNum);
			DocumentList=Documents.Refresh(DocAttachList);
			DirectoryInfo di=new DirectoryInfo(patFolder);
			FileInfo[] fiList=di.GetFiles();
			int countAdded=0;
			string[] usedNames=new string[DocumentList.Length];
			for(int i=0;i<DocumentList.Length;i++) {
				usedNames[i]=DocumentList[i].FileName;
			}
			for(int i=0;i<fiList.Length;i++){
				if(!DocumentB.IsFileNameInList(fiList[i].Name,usedNames)
					&& fiList[i].Name!="Thumbs.db")//Thumbs.db is a hidden Windows file unrelated to OD.
				{
					//MessageBox.Show(fiList[i].Name);
					Document doc=new Document();
					doc.DateCreated=DateTime.Today;
					doc.Description=fiList[i].Name;
					doc.DocCategory=DefB.Short[(int)DefCat.ImageCats][0].DefNum;
					doc.FileName=fiList[i].Name;
					doc.WithPat=PatCur.PatNum;
					Documents.Insert(doc,PatCur);
					//doc.Insert(PatCur);
					countAdded++;
				}
			}
			if(countAdded>0){
				MessageBox.Show(countAdded.ToString()+" documents found and added to the first category.");
			}
			//it will refresh in FillDocList																					 
		}

		private void RefreshModuleScreen(){
			if(PatCur!=null){
				ParentForm.Text=((Pref)PrefB.HList["MainWindowTitle"]).ValueString+" - "
					+PatCur.GetNameLF();
				ToolBarMain.Buttons["Print"].Enabled=true;
				ToolBarMain.Buttons["Delete"].Enabled=true;
				ToolBarMain.Buttons["Info"].Enabled=true;
				ToolBarMain.Buttons["Import"].Enabled=true;
				ToolBarMain.Buttons["ScanDoc"].Enabled=true;
				ToolBarMain.Buttons["ScanXRay"].Enabled=true;
				ToolBarMain.Buttons["ScanPhoto"].Enabled=true;
				ToolBarMain.Buttons["Copy"].Enabled=true;
				ToolBarMain.Buttons["Paste"].Enabled=true;
				ToolBarMain.Buttons["Forms"].Enabled=true;
				ToolBarMain.Buttons["Crop"].Enabled=true;
				ToolBarMain.Buttons["Hand"].Enabled=true;
				ToolBarMain.Buttons["ZoomIn"].Enabled=true;
				ToolBarMain.Buttons["ZoomOut"].Enabled=true;
				ToolBarMain.Buttons["Flip"].Enabled=true;
				ToolBarMain.Buttons["RotateR"].Enabled=true;
				ToolBarMain.Buttons["RotateL"].Enabled=true;
			}
			else{
				ParentForm.Text=((Pref)PrefB.HList["MainWindowTitle"]).ValueString;
				//PatCur=new Patient();
				ToolBarMain.Buttons["Print"].Enabled=false;
				ToolBarMain.Buttons["Delete"].Enabled=false;
				ToolBarMain.Buttons["Info"].Enabled=false;
				ToolBarMain.Buttons["Import"].Enabled=false;
				ToolBarMain.Buttons["ScanDoc"].Enabled=false;
				ToolBarMain.Buttons["ScanXRay"].Enabled=false;
				ToolBarMain.Buttons["ScanPhoto"].Enabled=false;
				ToolBarMain.Buttons["Copy"].Enabled=false;
				ToolBarMain.Buttons["Paste"].Enabled=false;
				ToolBarMain.Buttons["Forms"].Enabled=false;
				ToolBarMain.Buttons["Crop"].Enabled=false;
				ToolBarMain.Buttons["Hand"].Enabled=false;
				ToolBarMain.Buttons["ZoomIn"].Enabled=false;
				ToolBarMain.Buttons["ZoomOut"].Enabled=false;
				ToolBarMain.Buttons["Flip"].Enabled=false;
				ToolBarMain.Buttons["RotateR"].Enabled=false;
				ToolBarMain.Buttons["RotateL"].Enabled=false;
			}
			FillPatientButton();
			ToolBarMain.Invalidate();
			FillDocList(false);
		}

		private void FillPatientButton(){
			Patients.AddPatsToMenu(menuPatient,new EventHandler(menuPatient_Click),PatCur,FamCur);
		}

		private void menuPatient_Click(object sender,System.EventArgs e) {
			int newPatNum=Patients.ButtonSelect(menuPatient,sender,FamCur);
			OnPatientSelected(newPatNum);
			ModuleSelected(newPatNum);
		}

		///<summary></summary>
		private void OnPatientSelected(int patNum){
			PatientSelectedEventArgs eArgs=new OpenDental.PatientSelectedEventArgs(patNum);
			if(PatientSelected!=null)
				PatientSelected(this,eArgs);
		}

		private void ContrDocs_Load(object sender, System.EventArgs e){
			//if (SystemInformation.PrimaryMonitorSize.Height<=768){
		}

		/// <summary>Refreshes list from db, then fills the treeview.  Set to true to keep the current doc displayed.</summary>
		private void FillDocList(bool keepDoc){
			if(PatCur==null){
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
			DocAttachList=DocAttaches.Refresh(PatCur.PatNum);
			DocumentList=Documents.Refresh(DocAttachList);
			for(int i=0;i<TreeDocuments.Nodes.Count;i++) 
				TreeDocuments.Nodes[i].Nodes.Clear();
			TreeDocuments.Nodes.Clear();
			for(int i=0;i<DefB.Short[(int)DefCat.ImageCats].Length;i++){
				sNewNode=DefB.Short[(int)DefCat.ImageCats][i].ItemName;
				TreeDocuments.Nodes.Add(new TreeNode(sNewNode));
				TreeDocuments.Nodes[i].SelectedImageIndex=1;  
				TreeDocuments.Nodes[i].ImageIndex=1;          
			}
			for(int i=0;i<DocumentList.Length;i++){
	  		sNewNode=DocumentList[i].DateCreated.ToString("d")+": "+DocumentList[i].Description;
				if(DocumentList[i].ImgType==ImageType.File)
					indexImageList=5;
				else if(DocumentList[i].ImgType==ImageType.Radiograph)
					indexImageList=3;
				else if(DocumentList[i].ImgType==ImageType.Photo)
					indexImageList=4;
				else//document
					indexImageList=2;
				if(DefB.GetOrder(DefCat.ImageCats,DocumentList[i].DocCategory)==-1){//if cat hidden
					MessageBox.Show(Lan.g(this,"There is a document in a hidden category: "
						+DefB.GetName(DefCat.ImageCats,DocumentList[i].DocCategory)
						+". You can unhide this category in Definitions section."));
				}
				else{
					TreeDocuments.Nodes[DefB.GetOrder(DefCat.ImageCats,DocumentList[i].DocCategory)]
						.Nodes.Add(new TreeNode(sNewNode));
					//store docnum in tag of node:
					TreeDocuments.Nodes[DefB.GetOrder(DefCat.ImageCats,DocumentList[i].DocCategory)]
						.LastNode.Tag=DocumentList[i].DocNum;
					TreeDocuments.Nodes[DefB.GetOrder(DefCat.ImageCats,DocumentList[i].DocCategory)]
						.LastNode.ImageIndex=indexImageList;
					TreeDocuments.Nodes[DefB.GetOrder(DefCat.ImageCats,DocumentList[i].DocCategory)]
						.LastNode.SelectedImageIndex=indexImageList;
				}
			}
			TreeDocuments.ExpandAll();
			string SrcFileName="";
			if(keepDoc){
				DocCur=Documents.GetDocument(DocCur.DocNum.ToString(),DocumentList);
				//MessageBox.Show(Docs.Cur.DocNum.ToString());
				if(DefB.GetOrder(DefCat.ImageCats,DocCur.DocCategory)!=-1){
					foreach(TreeNode n in TreeDocuments.Nodes[DefB.GetOrder(DefCat.ImageCats,DocCur.DocCategory)].Nodes){	
						if(n.Tag.ToString()==DocCur.DocNum.ToString()){
							TreeDocuments.SelectedNode=n;
						}
					}
				}
				SrcFileName=patFolder+DocCur.FileName;
				try{
					WebRequest request=WebRequest.Create(SrcFileName); 
					WebResponse response=request.GetResponse();
					if(Path.GetExtension(DocCur.FileName)==".jpg"){//can only display jpg for now
						ImageCurrent=(Bitmap)System.Drawing.Bitmap.FromStream (response.GetResponseStream());
					}
					else{
						ImageCurrent=null;//this may be unnecessary
					}
					response.Close();
			  }
			  catch(System.Exception exception){
					MessageBox.Show(Lan.g(this,exception.Message)); 
			  }
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
					case "Sign":
						OnSign_Click();
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
					case "Copy":
						OnCopy_Click();
						break;
					case "Paste":
						OnPaste_Click();
						break;
					case "Forms":
						MsgBox.Show(this,"Use the dropdown list.  Add forms to the list by copying image files into your A-Z folder, Forms.  Restart the program to see newly added forms.");
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
				Programs.Execute((int)e.Button.Tag,PatCur);
			}
		}

		private void OnPat_Click() {
			FormPatientSelect formPS=new FormPatientSelect();
			formPS.ShowDialog();
			if(formPS.DialogResult==DialogResult.OK){
				OnPatientSelected(formPS.SelectedPatNum);
				ModuleSelected(formPS.SelectedPatNum);
			}
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
				File.Delete(patFolder+DocCur.FileName);
			}
			catch{
				MessageBox.Show(Lan.g(this,"Could not delete file.  It may be in use elsewhere."));
				return;
			}
			DeleteThumbnail();
			Documents.Delete(DocCur);
			//DocCur.Delete();
			FillDocList(false);
		}

		private void OnInfo_Click() {
			for(int i=0;i<TreeDocuments.Nodes.Count;i++){//can't get info on a main node
				if(TreeDocuments.SelectedNode.Equals(TreeDocuments.Nodes[i]))
					return;
      }
			FormDocInfo formDocInfo2=new FormDocInfo(PatCur,DocCur);
			formDocInfo2.ShowDialog();
			if(formDocInfo2.DialogResult!=DialogResult.OK){
				return;
			}
			FillDocList(true);
			DisplayImage(false,true);//because the category may have changed
		}

		private void OnSign_Click(){
			for(int i=0;i<TreeDocuments.Nodes.Count;i++) {
				if(TreeDocuments.SelectedNode.Equals(TreeDocuments.Nodes[i]))
					return;//a document is not selected.
			}
			if(DocCur.IsFlipped || DocCur.DegreesRotated!=0){
				MsgBox.Show(this,"Not allowed to sign an image that has been flipped or rotated.  This restriction will be lifted in a later version.");
				return;
			}
			ShowNote=true;
			DisplayImage(true,true);
			FormDocSign FormD=new FormDocSign(DocCur,patFolder);
			FormD.Location=PointToScreen(new Point(20,panelNote.Top));
			FormD.ShowDialog();
			ShowNote=false;
			DisplayImage(true,true);
		}

		private void OnScan_Click(string scanType) {
			ScanImage(scanType);
		}

		private void OnImport_Click() {
			OpenFileDialog openFileDialog=new OpenFileDialog();
			openFileDialog.Multiselect = true;
			if(openFileDialog.ShowDialog()!=DialogResult.OK) {
				return;
			}
			string[] fileNames = openFileDialog.FileNames;
			for(int i=0;i<fileNames.Length;i++) {
				openFileDialog.FileName=fileNames[i];
				if((myStream=openFileDialog.OpenFile())!=null){//Says I'm using the file don't touch it to other programs
					//Documents.Cur.Description=Path.GetFileName(openFileDialog2.FileName);
					try {
						WebRequest request=WebRequest.Create(openFileDialog.FileName);
						WebResponse response=request.GetResponse();
						if(Path.GetExtension(openFileDialog.FileName).ToUpper()==".JPG"
							|| Path.GetExtension(openFileDialog.FileName).ToUpper()==".GIF")
						{
							ImageCurrent=(Bitmap)System.Drawing.Bitmap.FromStream(response.GetResponseStream());
						}
						else {
							ImageCurrent=null;//may not be necessary
						}
						response.Close();
					}
					catch(System.Exception exception) {
						MessageBox.Show(exception.Message);// + " Selected File Not Image."));
						myStream.Close();
						return;
					}
					RecZoom.Width = 0;
					DisplayImage(true,false);
					DocCur=new Document();
					//Document.Insert will use this extension when naming:
					DocCur.FileName=Path.GetExtension(openFileDialog.FileName);
					DocCur.DateCreated=DateTime.Today;
					DocCur.WithPat=PatCur.PatNum;
					Documents.Insert(DocCur,PatCur);//this assigns a filename and saves to db
					//DocCur.Insert(PatCur);
					FormDocInfo FormD=new FormDocInfo(PatCur,DocCur);
					FormD.ShowDialog();//some of the fields might get changed, but not the filename
					if(FormD.DialogResult==DialogResult.OK) {
						try {
							//MessageBox.Show(Path.GetDirectoryName(openFileDialog2.FileName)+"\\"+","+patFolder);
							//if(Path.GetDirectoryName(openFileDialog2.FileName)==patFolder
							File.Copy(openFileDialog.FileName,patFolder + DocCur.FileName);
						}
						catch {
							MessageBox.Show(Lan.g(this,"Unable to copy file.  May be in use."));
							Documents.Delete(DocCur);
							ImageCurrent = null;
						}
					}
					else {
						ImageCurrent = null;
						Documents.Delete(DocCur);
					}
					myStream.Close();
				} // end if ((myStream = openFileDialog.OpenFile()) != null)
			}//end for i
			if(ImageCurrent==null) {
				FillDocList(false);
			}
			else {
				FillDocList(true);
			}
			DisplayImage(true,true);
		}

		private void OnCopy_Click() {
			Clipboard.SetDataObject(ImageCurrent);
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
			DocCur=new Document();
			DocCur.FileName=".jpg";
			DocCur.DateCreated=DateTime.Today;
			DocCur.WithPat=PatCur.PatNum;
			Documents.Insert(DocCur,PatCur);//this assigns a filename and saves to db
			FormDocInfo formD=new FormDocInfo(PatCur,DocCur);
			formD.ShowDialog();
			if(formD.DialogResult!=DialogResult.OK){
				Documents.Delete(DocCur);
				return;
			}
			try{
				ImageCurrent.Save(patFolder+DocCur.FileName);
        //Documents.Cur.LastAltered=DateTime.Today; 
        //Documents.UpdateCur();
			}
			catch{
				MessageBox.Show(Lan.g(this,"Error saving document."));
				Documents.Delete(DocCur);
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
			if(DocCur.DegreesRotated==90 || DocCur.DegreesRotated==270){
				//sideways
				//maintain original x
				if(DocCur.DegreesRotated==270){
					//maintain the lower edge instead.
					if(DocCur.IsFlipped){
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
				if(DocCur.DegreesRotated==0){
					//do nothing. Y is already correct.
				}
				else{//180
					//maintain the lower edge instead.
					RecZoom.Y=RecZoom.Y+RecZoom.Height;
				}
				//maintain original x center.(RecZoom.x+width)
				RecZoom.X=RecZoom.X+(RecZoom.Width/2);//works if flipped
				if(DocCur.IsFlipped){
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
					if(DocCur.IsFlipped){
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
					if(DocCur.IsFlipped){
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
			DocCur.IsFlipped=!DocCur.IsFlipped;
			if(DocCur.DegreesRotated==90 || DocCur.DegreesRotated==270){
				//then also rotate 180 to maintain illusion of horizontal flip.
				DocCur.DegreesRotated+=180;
				if(DocCur.DegreesRotated>=360){
					DocCur.DegreesRotated-=360;
				}
			}		
			Documents.Update(DocCur);
			FillDocList(true);
			DisplayImage(true,true);
			DeleteThumbnail();
		}

		private void OnRotateL_Click(){
			if(TreeDocuments.SelectedNode==null || TreeDocuments.SelectedNode.Parent==null){
				return;
			}
			DocCur.DegreesRotated-=90;
			if(DocCur.DegreesRotated<0){
				DocCur.DegreesRotated+=360;
			}
			Documents.Update(DocCur);
			FillDocList(true);
			DisplayImage(true,true);
			DeleteThumbnail();
		}

		private void OnRotateR_Click(){
			if(TreeDocuments.SelectedNode==null || TreeDocuments.SelectedNode.Parent==null){
				return;
			}
			DocCur.DegreesRotated+=90;
			if(DocCur.DegreesRotated>=360){
				DocCur.DegreesRotated-=360;
			}
			Documents.Update(DocCur);
			FillDocList(true);
			DisplayImage(true,true);
			DeleteThumbnail();
		}

		///<summary>This is done when deleting and after flipping or rotating.  The next time a thumbnail is needed, it will be created with the proper orientation.</summary>
		private void DeleteThumbnail(){
			string thumbFileName=patFolder+@"Thumbnails\"+DocCur.FileName;
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
				deltaStart=Convert.ToSingle(((Pref)PrefB.HList["CropDelta"]).ValueString);
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
			RecCrop=new Rectangle();
			RecCrop.X=cropLeft;
			RecCrop.Y=cropTop;
			RecCrop.Width=cropRight-cropLeft;
			RecCrop.Height=cropBottom-cropTop;
			Graphics grPictBox = PictureBox1.CreateGraphics();
			//ImageCurrent will get the blue rectangle drawn in it, but then it will be replaced with a 
			//clean image before crop and save.
			Graphics grImg=Graphics.FromImage(ImageCurrent);
			grImg.DrawRectangle(new Pen(Color.Blue),RecCrop);
			grPictBox.DrawImage(ImageCurrent
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
			ImageCurrent.Save(patFolder+DocCur.FileName,ImageFormat.Jpeg);
      //Documents.Cur.LastAltered=DateTime.Today;
      //Documents.UpdateCur();
			FillDocList(true);
			DisplayImage(true,true);
		}

		///<summary>Valid values for scanType are "doc","xray",and "photo"</summary>
		private void ScanImage(string scanType){
			//EZTwain.SelectImageSource(IntPtr.Zero);
			//return;


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
			DocCur=new Document();
			if(scanType=="doc")
					DocCur.ImgType=ImageType.Document;
				else if(scanType=="xray")
					DocCur.ImgType=ImageType.Radiograph;
				else if(scanType=="photo")
					DocCur.ImgType=ImageType.Photo;
			DocCur.FileName=".jpg";
			DocCur.DateCreated=DateTime.Today;
			DocCur.WithPat=PatCur.PatNum;
			Documents.Insert(DocCur,PatCur);//creates filename and saves to db
			FormDocInfo formDocInfo=new FormDocInfo(PatCur,DocCur);
			formDocInfo.ShowDialog();
			if(formDocInfo.DialogResult!=DialogResult.OK){
				Documents.Delete(DocCur);
				return;
			}
			try{
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
					qualityL=(long)Convert.ToInt32(((Pref)PrefB.HList["ScannerCompression"]).ValueString);
				}
				else if(scanType=="xray"){
					qualityL=Convert.ToInt64(((Pref)PrefB.HList["ScannerCompressionRadiographs"]).ValueString);
				}
				else if(scanType=="photo"){
					qualityL=Convert.ToInt64(((Pref)PrefB.HList["ScannerCompressionPhotos"]).ValueString);
				}
				EncoderParameter myEncoderParameter=new EncoderParameter(myEncoder,qualityL);
				myEncoderParameters.Param[0]=myEncoderParameter;
				ImageCurrent.Save(patFolder+DocCur.FileName,myImageCodecInfo,myEncoderParameters);
				FillDocList(true);//adds new doc to DocList and TreeDocument and saves path to image
				DisplayImage(true,true);//sets current image of Docs.Cur image path and displays that image
			}
			catch{
				MessageBox.Show(Lan.g(this,"Unable to save document."));
				Documents.Delete(DocCur);
			}
			//}
			//catch(Exception ex){
			//	MessageBox.Show ("ERROR: " + ex.Message);
			//}
			//AutoCrop();??
		}

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
		  //Documents.GetCurrent(TreeDocuments.SelectedNode.Tag.ToString());
			SrcFileName = patFolder+DocCur.FileName;
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
			ShowNote=false;
			panelNote.Visible=false;
			PictureBox1.Height=panelNote.Bottom-PictureBox1.Top;
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
				switch(DocCur.DegreesRotated){
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
				if(DocCur.DegreesRotated==0 || DocCur.DegreesRotated==180){
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

		///<summary>Make sure to set enhanced to false if no DocCur</summary>
		private void DisplayImage(bool clearFirst,bool enhanced){
			Enhanced=enhanced;
			//make room for the note and signature at the bottom
			if(DocCur!=null && (ShowNote
				|| (DocCur.Note!=null && DocCur.Note!="")
				|| (DocCur.Signature!=null && DocCur.Signature!="")))
			{
				panelNote.Visible=true;
				PictureBox1.Height=panelNote.Top-PictureBox1.Top-2;
				FillSignature();
			}
			else{//no reason to show note
				panelNote.Visible=false;
				PictureBox1.Height=panelNote.Bottom-PictureBox1.Top;
			}
			if(DocCur!=null && DocCur.Signature!=""){//if signed
				ToolBarMain.Buttons["Crop"].Enabled=false;
				ToolBarMain.Buttons["Flip"].Enabled=false;
				ToolBarMain.Buttons["RotateR"].Enabled=false;
				ToolBarMain.Buttons["RotateL"].Enabled=false;
				ToolBarMain.Invalidate();
			}
			if(DocCur!=null && DocCur.Signature==""){//if not signed
				ToolBarMain.Buttons["Crop"].Enabled=true;
				ToolBarMain.Buttons["Flip"].Enabled=true;
				ToolBarMain.Buttons["RotateR"].Enabled=true;
				ToolBarMain.Buttons["RotateL"].Enabled=true;
				ToolBarMain.Invalidate();
			}
			//now, draw the image
			Graphics g=PictureBox1.CreateGraphics();
			if(clearFirst)
				g.FillRectangle(Brushes.White,0,0,PictureBox1.ClientRectangle.Width
					,PictureBox1.ClientRectangle.Height);
			if(ImageCurrent==null){
				g.Dispose();
				return;
			}
			if(DocCur==null){
				DocCur=new Document();
			}
			if(RecZoom.Width==0){//need to create new RecZoom
				RecZoom=GetSourceRect();
			}
			RecCrop=new Rectangle();//so it won't show anymore
			if(enhanced){
				switch(DocCur.DegreesRotated){
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
			if(DocCur.DegreesRotated==0 || DocCur.DegreesRotated==180){
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

		private void FillSignature(){
			textNote.Text=DocCur.Note;
			sigBoxTopaz.Location=sigBox.Location;//this puts both boxes in the same spot.
			sigBoxTopaz.Visible=false;
			sigBox.SetTabletState(0);//never accepts input here
			sigBoxTopaz.SetTabletState(0);
			labelInvalidSig.Visible=false;
			if(DocCur.SigIsTopaz) {
				if(DocCur.Signature!=null && DocCur.Signature!="") {
					sigBoxTopaz.Visible=true;
					sigBoxTopaz.ClearTablet();
					sigBoxTopaz.SetSigCompressionMode(0);
					sigBoxTopaz.SetEncryptionMode(0);
					sigBoxTopaz.SetKeyString(GetHashString());//"0000000000000000");
					//sigBoxTopaz.SetAutoKeyData(ProcCur.Note+ProcCur.UserNum.ToString());
					sigBoxTopaz.SetEncryptionMode(2);//high encryption
					sigBoxTopaz.SetSigCompressionMode(2);//high compression
					sigBoxTopaz.SetSigString(DocCur.Signature);
					if(sigBoxTopaz.NumberOfTabletPoints()==0){
						labelInvalidSig.Visible=true;
					}
				}
			}
			else {
				sigBox.ClearTablet();
				if(DocCur.Signature!=null && DocCur.Signature!="") {
					//sigBox.SetSigCompressionMode(0);
					//sigBox.SetEncryptionMode(0);
					sigBox.SetKeyString(GetHashString());//"0000000000000000");
					//sigBox.SetAutoKeyData(ProcCur.Note+ProcCur.UserNum.ToString());
					//sigBox.SetEncryptionMode(2);//high encryption
					//sigBox.SetSigCompressionMode(2);//high compression
					sigBox.SetSigString(DocCur.Signature);
					if(sigBox.NumberOfTabletPoints()==0){
						labelInvalidSig.Visible=true;
					}
					sigBox.SetTabletState(0);//not accepting input.
				}
			}
		}

		private string GetHashString() {
			//the key data is the bytes of the file, concatenated with the bytes of the note.
			byte[] textbytes;
			if(DocCur.Note==null){
				textbytes=Encoding.UTF8.GetBytes("");
			}
			else{
				textbytes=Encoding.UTF8.GetBytes(DocCur.Note);
			}
			string path=patFolder+DocCur.FileName;
			if(!File.Exists(path)) {
				return "";
			}
			FileStream fs=new FileStream(path,FileMode.Open,FileAccess.Read,FileShare.Read);
			int fileLength=(int)fs.Length;
			byte[] buffer=new byte[fileLength+textbytes.Length];
			fs.Read(buffer,0,fileLength);
			fs.Close();
			Array.Copy(textbytes,0,buffer,fileLength,textbytes.Length);
			HashAlgorithm algorithm=MD5.Create();
			byte[] hash=algorithm.ComputeHash(buffer);//always results in length of 16.
			return Encoding.ASCII.GetString(hash);
		}

		///<summary>Gets the original source rectangle representing the portion of ImageCurrent to display.  Always bigger than ImageCurrent and in proportions of PictureBox client area.  Always white space on one side which depends on the rotation, flip, and proportions.  Used to set initial RecZoom as well as to test when zooming out and dragging to make sure it stays within allowed bounds.</summary>
		private Rectangle GetSourceRect(){
			string whiteSide=GetWhiteSide();//L,R,T,or B
			Rectangle retRect=new Rectangle(0,0,ImageCurrent.Width,ImageCurrent.Height);
			//if(DocCur==null){
			//	return retRect;
			//}
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
			else if(DocCur.DegreesRotated==0 || DocCur.DegreesRotated==180){
				if(whiteSide=="L" || whiteSide=="R"){
					retRect.Width=(int)(retRect.Height*screenRatio);
				}
				else{
					retRect.Height=(int)(retRect.Width/screenRatio);
				}
				if(DocCur.DegreesRotated==0){
					if(whiteSide=="L"){
						retRect.X=ImageCurrent.Width-retRect.Width;//a negative value
					}
				}
				if(DocCur.DegreesRotated==180){
					if(whiteSide=="T"){
						retRect.Y=ImageCurrent.Height-retRect.Height;//a negative value
					}
					if(whiteSide=="L"){
						retRect.X=ImageCurrent.Width-retRect.Width;//a negative value??
					}
				}
			}
			else if(DocCur.DegreesRotated==90 || DocCur.DegreesRotated==270){
				if(whiteSide=="L" || whiteSide=="R"){//image shorter than screen width
					retRect.Height=(int)(retRect.Width*screenRatio);//make it taller
				}
				else{
					retRect.Width=(int)(retRect.Height/screenRatio);
				}
				if(DocCur.DegreesRotated==90){
					if(whiteSide=="T"){
						retRect.X=ImageCurrent.Width-retRect.Width;//a negative value
					}
					if(whiteSide=="L"){
						retRect.Y=ImageCurrent.Height-retRect.Height;//a negative value
					}
				}
				if(DocCur.DegreesRotated==270){
					if(whiteSide=="T"){
						retRect.X=ImageCurrent.Width-retRect.Width;//a negative value
					}
				}
			}
			if(DocCur.IsFlipped){
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
			if(DocCur.DegreesRotated==0){
				if(imageRatio<screenRatio){//if image narrower
					if(DocCur.IsFlipped)
						return "L";
					else
						return "R";
				}
				else{//if image shorter
					return "B";
				}
			}
			if(DocCur.DegreesRotated==90){//imagine image on side when doing calculations
				//if the image(laid sideways) is proprortionally narrower than the picturebox
  			if((1/imageRatio)<screenRatio){
					return "L";
				}
				else{//if image is shorter
					if(DocCur.IsFlipped)
						return "T";
					else
						return "B";
				}
			}
			else if(DocCur.DegreesRotated==180){
				if(imageRatio<screenRatio){//if image narrower
					if(DocCur.IsFlipped)
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
					if(DocCur.IsFlipped)
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
			switch(DocCur.DegreesRotated){
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
				if(DocCur.DegreesRotated==0){
					RecTempZoom.Y=RecZoom.Y+moveY;
					if(DocCur.IsFlipped){
						RecTempZoom.X=RecZoom.X-moveX;
					}
					else{//normal
						RecTempZoom.X=RecZoom.X+moveX;
					}
				}
				else if(DocCur.DegreesRotated==90){
					RecTempZoom.Y=RecZoom.Y-moveX;
					if(DocCur.IsFlipped){
						RecTempZoom.X=RecZoom.X-moveY;
					}
					else{
						RecTempZoom.X=RecZoom.X+moveY;
					}
				}
				else if(DocCur.DegreesRotated==180){
					RecTempZoom.Y=RecZoom.Y-moveY;
					if(DocCur.IsFlipped){
						RecTempZoom.X=RecZoom.X+moveX;
					}
					else{
						RecTempZoom.X=RecZoom.X-moveX;
					}
				}
				else if(DocCur.DegreesRotated==270){
					RecTempZoom.Y=RecZoom.Y+moveX;
					if(DocCur.IsFlipped){
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
					if(DocCur.DegreesRotated==0 && whiteSide=="B")
						startedWhiteB=true;
					else if(DocCur.DegreesRotated==90 && whiteSide=="L")
						startedWhiteB=true;
					else if(DocCur.DegreesRotated==180 && whiteSide=="T")
						startedWhiteB=true;
					else if(DocCur.DegreesRotated==270 && whiteSide=="R")
						startedWhiteB=true;
				}
				if(RecZoom.X+RecZoom.Width>ImageCurrent.Width){
					if(DocCur.DegreesRotated==0 && whiteSide=="R")
						startedWhiteR=true;
					else if(DocCur.DegreesRotated==90 && whiteSide=="B")
						startedWhiteR=true;
					else if(DocCur.DegreesRotated==180 && whiteSide=="L")
						startedWhiteR=true;
					else if(DocCur.DegreesRotated==270 && whiteSide=="T")
						startedWhiteR=true;
				}
				if(RecZoom.X+RecZoom.Width<0){//width is neg because only for flipped
					if(DocCur.DegreesRotated==0 && whiteSide=="L")
						startedWhiteL=true;
					else if(DocCur.DegreesRotated==90 && whiteSide=="T")
						startedWhiteL=true;
					else if(DocCur.DegreesRotated==180 && whiteSide=="R")
						startedWhiteL=true;
					else if(DocCur.DegreesRotated==270 && whiteSide=="B")
						startedWhiteL=true;
				}
				//limit movement to right
				if(startedWhiteR){
					//same for flipped and regular
					if(RecTempZoom.X>RecZoom.X)
						RecTempZoom.X=RecZoom.X;
				}
				else{
					if(DocCur.IsFlipped){
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
					if(DocCur.IsFlipped){
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
				if(DocCur.DegreesRotated==0 || DocCur.DegreesRotated==180){
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
			else{//Crop Mode
				float ratio=1;
				if(DocCur.DegreesRotated==0 || DocCur.DegreesRotated==180){
					ratio=(float)PictureBox1.ClientRectangle.Width/Math.Abs(RecZoom.Width);
				}
				else{
					ratio=(float)PictureBox1.ClientRectangle.Width/Math.Abs(RecZoom.Height);
				}
				int rBound=0;//in picturebox coordinates
				int bBound=0;
				if(DocCur.DegreesRotated==0 || DocCur.DegreesRotated==180){
					rBound=(int)(ImageCurrent.Width*ratio);
					bBound=(int)(ImageCurrent.Height*ratio);
				}
				else{
					rBound=(int)(ImageCurrent.Height*ratio);
					bBound=(int)(ImageCurrent.Width*ratio);
				}
				if(DocCur.DegreesRotated==0 || DocCur.DegreesRotated==180){
					g.DrawImage(ImageCurrent
						,new Rectangle(0,0,PictureBox1.ClientRectangle.Width,PictureBox1.ClientRectangle.Height)
						,RecZoom,GraphicsUnit.Pixel);
				}
				else{//90 or 270
					g.DrawImage(ImageCurrent
						,new Rectangle(0,0,PictureBox1.ClientRectangle.Height,PictureBox1.ClientRectangle.Width)
						,RecZoom,GraphicsUnit.Pixel);
				}
				if(MouseIsDown){
					RecCrop=new Rectangle();
					RecCrop.X=MouseDownOrigin.X;
					RecCrop.Y=MouseDownOrigin.Y;
					RecCrop.Width=e.X-MouseDownOrigin.X;
					RecCrop.Height=e.Y-MouseDownOrigin.Y;
					if(e.X > rBound){
						RecCrop.Width=(int)rBound-RecCrop.X;
					}
					if(e.Y > bBound){
						RecCrop.Height=(int)bBound-RecCrop.Y;
					}
					/*if(RecZoom.X*ratio+e.X > rBound){
						RecCrop.Width=(int)(rBound-RecZoom.X*ratio)-RecCrop.X;
					}
					if(RecZoom.Y*ratio+e.Y > bBound){
						RecCrop.Height=(int)(bBound-RecZoom.Y*ratio)-RecCrop.Y;
					}*/
					//need to unmangle rectangle?? not too important...
					g.ResetTransform();
					g.DrawRectangle(new Pen(Color.Blue),RecCrop);
				}
				else{//mouse is up 
					g.ResetTransform();
					if(e.X<rBound){
						g.DrawLine(new Pen(Color.Blue),new Point(e.X,0),new Point(e.X,bBound));
					}
					if(e.Y<bBound){
						g.DrawLine(new Pen(Color.Blue),new Point(0,e.Y),new Point(rBound,e.Y));
					}
					g.DrawRectangle(new Pen(Color.Blue),RecCrop);
				}
			}
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
				Rectangle sourceRect=new Rectangle();//in image coordinates.
				//sourceRect has positive width
				float ratio=1;
				if(DocCur.DegreesRotated==0 || DocCur.DegreesRotated==180){
					ratio=(float)Math.Abs(RecZoom.Width)/(float)PictureBox1.ClientRectangle.Width;
				}
				else{
					ratio=(float)RecZoom.Height/(float)PictureBox1.ClientRectangle.Width;
				}
				if(DocCur.DegreesRotated==0){
					if(DocCur.IsFlipped){
						sourceRect.X     = RecZoom.Left  -(int)(ratio*(float)RecCrop.Right);
						sourceRect.Y     = RecZoom.Top   +(int)(ratio*(float)RecCrop.Top);
						sourceRect.Width = (int)((float)RecCrop.Width *ratio);
						sourceRect.Height= (int)((float)RecCrop.Height*ratio);
					}
					else{
						sourceRect.X     = RecZoom.Left  +(int)(ratio*(float)RecCrop.Left);
						sourceRect.Y     = RecZoom.Top   +(int)(ratio*(float)RecCrop.Top);
						sourceRect.Width = (int)((float)RecCrop.Width *ratio);
						sourceRect.Height= (int)((float)RecCrop.Height*ratio);
					}
				}
				else if(DocCur.DegreesRotated==90){
					if(DocCur.IsFlipped){
						sourceRect.X     = RecZoom.Left -(int)(ratio*(float)RecCrop.Bottom);
						sourceRect.Y     = RecZoom.Bottom-(int)(ratio*(float)RecCrop.Right);
						sourceRect.Width = (int)((float)RecCrop.Height*ratio);
						sourceRect.Height= (int)((float)RecCrop.Width*ratio);
					}
					else{
						sourceRect.X     = RecZoom.Left  +(int)(ratio*(float)RecCrop.Top);
						sourceRect.Y     = RecZoom.Bottom-(int)(ratio*(float)RecCrop.Right);
						sourceRect.Width = (int)((float)RecCrop.Height*ratio);
						sourceRect.Height= (int)((float)RecCrop.Width*ratio);
					}
				}
				else if(DocCur.DegreesRotated==180){
					if(DocCur.IsFlipped){
						sourceRect.X     = RecZoom.Right  +(int)(ratio*(float)RecCrop.Left);
						sourceRect.Y     = RecZoom.Bottom-(int)(ratio*(float)RecCrop.Bottom);
						sourceRect.Width = (int)((float)RecCrop.Width*ratio);
						sourceRect.Height= (int)((float)RecCrop.Height*ratio);
					}
					else{
						sourceRect.X     = RecZoom.Right -(int)(ratio*(float)RecCrop.Right);
						sourceRect.Y     = RecZoom.Bottom-(int)(ratio*(float)RecCrop.Bottom);
						sourceRect.Width = (int)((float)RecCrop.Width*ratio);
						sourceRect.Height= (int)((float)RecCrop.Height*ratio);
					}
				}
				else if(DocCur.DegreesRotated==270){
					if(DocCur.IsFlipped){
						sourceRect.X     = RecZoom.Right  +(int)(ratio*(float)RecCrop.Top);
						sourceRect.Y     = RecZoom.Top   +(int)(ratio*(float)RecCrop.Left);
						sourceRect.Width = (int)((float)RecCrop.Height*ratio);
						sourceRect.Height= (int)((float)RecCrop.Width*ratio);
					}
					else{
						sourceRect.X     = RecZoom.Right -(int)(ratio*(float)RecCrop.Bottom);
						sourceRect.Y     = RecZoom.Top   +(int)(ratio*(float)RecCrop.Left);
						sourceRect.Width = (int)((float)RecCrop.Height*ratio);
						sourceRect.Height= (int)((float)RecCrop.Width*ratio);
					}
				}
				Bitmap bitmapTemp=new Bitmap(1,1);//just to get the horizontal res
				float ratio2=(float)bitmapTemp.HorizontalResolution
					/(float)ImageCurrent.HorizontalResolution;// 96/150 or 96/96
				bitmapTemp=new Bitmap((int)((float)sourceRect.Width*ratio2),(int)((float)sourceRect.Height*ratio2));
				Graphics gTemp=Graphics.FromImage(bitmapTemp);//we're going to draw on bitmapTemp
				gTemp.DrawImage(ImageCurrent,0,0,sourceRect,GraphicsUnit.Pixel);
				gTemp.Dispose();
 				ImageCurrent=(Bitmap)bitmapTemp.Clone();
				RecZoom.Width=0;//DisplayImage will then recreate RecZoom
				DisplayImage(true,true);
				//the cropped image will stay in the same orientation as the original
				ImageCurrent.Save(patFolder+DocCur.FileName,ImageFormat.Jpeg);
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
				ImageCurrent=null;
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
		  DocCur=Documents.GetDocument(TreeDocuments.SelectedNode.Tag.ToString(),DocumentList);
			SrcFileName=patFolder+DocCur.FileName;
			try{
				//I just used webrequest for kicks. It is faster to use Image.FromFile().
		    WebRequest request=WebRequest.Create(SrcFileName); 
			  WebResponse response=request.GetResponse();
				//MessageBox.Show(Path.GetExtension(SrcFileName));
				if(Path.GetExtension(SrcFileName).ToLower()==".jpg"
					|| Path.GetExtension(SrcFileName).ToLower()==".gif"
					|| Path.GetExtension(SrcFileName).ToLower()==".jpeg")
				{
					ImageCurrent=(Bitmap)System.Drawing.Bitmap.FromStream(response.GetResponseStream());
				}
				else{
					ImageCurrent=null;//may not be necessary
				}
			  response.Close();
	    }
		  catch(System.Exception exception){
		    MessageBox.Show(Lan.g(this,exception.Message)); 
				ImageCurrent=null;
	    }
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
				//might have moved to new category, so test for that
				int oldCategory=DocCur.DocCategory;
				int newCategory=0;
				if(upNode.Parent==null){//category node
					newCategory=DefB.Short[(int)DefCat.ImageCats][upNode.Index].DefNum;
				}
				else{
					newCategory=Documents.GetCategory(upNode.Tag.ToString(),DocumentList);
				}
				if(oldCategory!=newCategory){
					if(upNode.Parent==null){//category node
						DocCur.DocCategory=DefB.Short[(int)DefCat.ImageCats][upNode.Index].DefNum;
					}
					else{
						DocCur.DocCategory=Documents.GetCategory(upNode.Tag.ToString(),DocumentList);
					}
					Documents.Update(DocCur);
					FillDocList(true);
				}
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

		private void menuForms_Click(object sender, System.EventArgs e) {
			string fileName=PrefB.GetString("DocPath")+@"Forms\"+((MenuItem)sender).Text;
			if(!File.Exists(fileName)){
				MessageBox.Show(Lan.g(this,"Could not find file: ")+fileName);
				return;
			}

			try {
				WebRequest request=WebRequest.Create(fileName);
				WebResponse response=request.GetResponse();
				if(Path.GetExtension(fileName).ToUpper()==".JPG"
							|| Path.GetExtension(fileName).ToUpper()==".GIF") {
					ImageCurrent=(Bitmap)System.Drawing.Bitmap.FromStream(response.GetResponseStream());
				}
				else {
					ImageCurrent=null;//may not be necessary
				}
				response.Close();
			}
			catch(System.Exception exception) {
				MessageBox.Show(exception.Message);// + " Selected File Not Image."));
				return;
			}
			RecZoom.Width = 0;
			DisplayImage(true,false);
			DocCur=new Document();
			//Document.Insert will use this extension when naming:
			DocCur.FileName=Path.GetExtension(fileName);
			DocCur.DateCreated=DateTime.Today;
			DocCur.WithPat=PatCur.PatNum;
			Documents.Insert(DocCur,PatCur);//this assigns a filename and saves to db
			FormDocInfo FormD=new FormDocInfo(PatCur,DocCur);
			FormD.ShowDialog();//some of the fields might get changed, but not the filename
			if(FormD.DialogResult==DialogResult.OK) {
				try {
					//MessageBox.Show(Path.GetDirectoryName(openFileDialog2.FileName)+"\\"+","+patFolder);
					//if(Path.GetDirectoryName(openFileDialog2.FileName)==patFolder
					File.Copy(fileName,patFolder + DocCur.FileName);
				}
				catch {
					MessageBox.Show(Lan.g(this,"Unable to copy file.  May be in use."));
					Documents.Delete(DocCur);
					ImageCurrent = null;
				}
			}
			else {
				ImageCurrent = null;
				Documents.Delete(DocCur);
			}
			if(ImageCurrent==null) {
				FillDocList(false);
			}
			else {
				FillDocList(true);
			}
			DisplayImage(true,true);
		}

		private void textNote_DoubleClick(object sender,EventArgs e) {
			OnSign_Click();
		}

		private void label1_DoubleClick(object sender,EventArgs e) {
			OnSign_Click();
		}

		private void label15_DoubleClick(object sender,EventArgs e) {
			OnSign_Click();
		}

		private void sigBox_DoubleClick(object sender,EventArgs e) {
			OnSign_Click();
		}

		private void sigBoxTopaz_DoubleClick(object sender,EventArgs e) {
			OnSign_Click();
		}

		private void labelInvalidSig_DoubleClick(object sender,EventArgs e) {
			OnSign_Click();
		}

		private void panelNote_DoubleClick(object sender,EventArgs e) {
			OnSign_Click();
		}




		//NEW CODE THAT IS NOT YET BEING USED.

		uint curImage=0;

		private void SetPrimaryImage(Document doc){
			if(curImage!=0){//This image will be loaded to replace an existing image on-screen.
				ContextGl.glDeleteTextures(1,ref curImage);//Remove the old image and free its reference number.
				curImage=0;//Just in case glGenTextures fails.
				ContextGl.glGenTextures(1,out curImage);//Get a new image reference number for the new image.
				ContextGl.glBindTexture(Gl.GL_TEXTURE_2D,curImage);//Set the loaded image as the current working texture.
				//Texture filter modes for zoom-in/zoom-out. 
				ContextGl.glTexParameteri(Gl.GL_TEXTURE_2D,Gl.GL_TEXTURE_MIN_FILTER,Gl.GL_LINEAR_MIPMAP_LINEAR);//TODO:Overkill?
				ContextGl.glTexParameteri(Gl.GL_TEXTURE_2D,Gl.GL_TEXTURE_MAG_FILTER,Gl.GL_LINEAR_MIPMAP_LINEAR);//TODO:Overkill?


				//Image image=Image.FromFile();
				//image.
				//ImageFormat format=image.RawFormat();
				//ContextGl.glTexImage2D();

			}
		}

		private void Render(Document doc){
			if(doc.ImgType==ImageType.File){
				throw new Exception("Attempt to render an unsupported document type.");
			}
			Logger.openlog.curSev=Logger.Severity.ERROR;
			try{			
				ContextGl.glBindTexture(Gl.GL_TEXTURE_2D,curImage);//Set the loaded image as the current working texture.
				
			}catch(Exception e){
				Logger.openlog.LogMB("Failed to load image from file: "+doc.FileName);
				Logger.openlog.Log("Additional info of image load error: "+e.ToString());
				return;
			}
			try{
				//Render the image.
				ContextGl.glMatrixMode(Gl.GL_MODELVIEW);
				ContextGl.glLoadIdentity();
				ContextGl.glRotatef(doc.DegreesRotated,0,1,0);

			}catch(Exception e){
				Logger.openlog.Log(e.ToString());
			}		
		}
		
	}
}









