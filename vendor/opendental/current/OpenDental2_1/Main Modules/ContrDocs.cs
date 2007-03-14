/*=============================================================================================================
FreeDental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
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

namespace OpenDental{

	public class ContrDocs : System.Windows.Forms.UserControl	{
		private System.Windows.Forms.ImageList imageListTree;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ImageList imageListTools2;
		private System.Windows.Forms.ToolBarButton toolBarButton9;
		private System.Windows.Forms.ToolBarButton toolBarButton10;
		private System.Windows.Forms.ToolBarButton toolBarSep4;
		private System.Windows.Forms.ToolBarButton toolBarButCrop;
		private System.Windows.Forms.ToolBarButton toolBarButHand;
		private Rectangle RecCrop;
		private Rectangle RecZoom;
		private System.Windows.Forms.PrintDialog PrintDialog1;
		private System.Drawing.Printing.PrintDocument PrintDocument2;
		private System.Windows.Forms.ToolBar ToolBar2;
		private System.Windows.Forms.TreeView TreeDocuments;
		private Point PtOrigin;
		private bool MouseIsDown;
		private System.Windows.Forms.PictureBox PictureBox1;
		private System.Drawing.Bitmap ImageCurrent;
		private System.Windows.Forms.ToolBarButton toolBarSep3;
		private Rectangle RecTemp;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuExit;
		private System.Windows.Forms.MenuItem menuPrefs;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ToolBarButton toolBarButZoomIn;
		private System.Windows.Forms.ToolBarButton toolBarButZoomOut;
		private System.Windows.Forms.ToolBarButton toolBarButPat;
		private System.Windows.Forms.ToolBarButton toolBarButPrint;
		private System.Windows.Forms.ToolBarButton toolBarButDel;
		private System.Windows.Forms.ToolBarButton toolBarButInfo;
		private System.Windows.Forms.ToolBarButton toolBarButScan;
		private System.Windows.Forms.ToolBarButton toolBarButImp;
		private System.Windows.Forms.ToolBarButton toolBarButPaste;
		private System.Windows.Forms.OpenFileDialog openFileDialog2;
    private Stream myStream;
    private FormDocInfo formDocInfo2;
		private string patFolder;
		private string imageFileName;

		public ContrDocs(){
			InitializeComponent();
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ContrDocs));
			this.TreeDocuments = new System.Windows.Forms.TreeView();
			this.imageListTree = new System.Windows.Forms.ImageList(this.components);
			this.PrintDialog1 = new System.Windows.Forms.PrintDialog();
			this.PrintDocument2 = new System.Drawing.Printing.PrintDocument();
			this.ToolBar2 = new System.Windows.Forms.ToolBar();
			this.toolBarButPat = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton9 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButPrint = new System.Windows.Forms.ToolBarButton();
			this.toolBarButDel = new System.Windows.Forms.ToolBarButton();
			this.toolBarButInfo = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton10 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButScan = new System.Windows.Forms.ToolBarButton();
			this.toolBarButImp = new System.Windows.Forms.ToolBarButton();
			this.toolBarButPaste = new System.Windows.Forms.ToolBarButton();
			this.toolBarSep3 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButCrop = new System.Windows.Forms.ToolBarButton();
			this.toolBarButHand = new System.Windows.Forms.ToolBarButton();
			this.toolBarSep4 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButZoomIn = new System.Windows.Forms.ToolBarButton();
			this.toolBarButZoomOut = new System.Windows.Forms.ToolBarButton();
			this.imageListTools2 = new System.Windows.Forms.ImageList(this.components);
			this.PictureBox1 = new System.Windows.Forms.PictureBox();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuExit = new System.Windows.Forms.MenuItem();
			this.menuPrefs = new System.Windows.Forms.MenuItem();
			this.button1 = new System.Windows.Forms.Button();
			this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
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
			// ToolBar2
			// 
			this.ToolBar2.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																																								this.toolBarButPat,
																																								this.toolBarButton9,
																																								this.toolBarButPrint,
																																								this.toolBarButDel,
																																								this.toolBarButInfo,
																																								this.toolBarButton10,
																																								this.toolBarButScan,
																																								this.toolBarButImp,
																																								this.toolBarButPaste,
																																								this.toolBarSep3,
																																								this.toolBarButCrop,
																																								this.toolBarButHand,
																																								this.toolBarSep4,
																																								this.toolBarButZoomIn,
																																								this.toolBarButZoomOut});
			this.ToolBar2.DropDownArrows = true;
			this.ToolBar2.ImageList = this.imageListTools2;
			this.ToolBar2.Location = new System.Drawing.Point(0, 0);
			this.ToolBar2.Name = "ToolBar2";
			this.ToolBar2.ShowToolTips = true;
			this.ToolBar2.Size = new System.Drawing.Size(838, 34);
			this.ToolBar2.TabIndex = 5;
			this.ToolBar2.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar2_ButtonClick);
			// 
			// toolBarButPat
			// 
			this.toolBarButPat.ImageIndex = 0;
			this.toolBarButPat.Tag = "Pat";
			this.toolBarButPat.ToolTipText = "Select Patient";
			// 
			// toolBarButton9
			// 
			this.toolBarButton9.Enabled = false;
			this.toolBarButton9.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// toolBarButPrint
			// 
			this.toolBarButPrint.Enabled = false;
			this.toolBarButPrint.ImageIndex = 1;
			this.toolBarButPrint.Tag = "Print";
			this.toolBarButPrint.ToolTipText = "Print Document";
			// 
			// toolBarButDel
			// 
			this.toolBarButDel.Enabled = false;
			this.toolBarButDel.ImageIndex = 2;
			this.toolBarButDel.Tag = "Del";
			this.toolBarButDel.ToolTipText = "Delete Document";
			// 
			// toolBarButInfo
			// 
			this.toolBarButInfo.Enabled = false;
			this.toolBarButInfo.ImageIndex = 3;
			this.toolBarButInfo.Tag = "Info";
			this.toolBarButInfo.ToolTipText = "Document Info";
			// 
			// toolBarButton10
			// 
			this.toolBarButton10.Enabled = false;
			this.toolBarButton10.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// toolBarButScan
			// 
			this.toolBarButScan.Enabled = false;
			this.toolBarButScan.ImageIndex = 4;
			this.toolBarButScan.Tag = "Scan";
			this.toolBarButScan.ToolTipText = "Scan";
			// 
			// toolBarButImp
			// 
			this.toolBarButImp.Enabled = false;
			this.toolBarButImp.ImageIndex = 5;
			this.toolBarButImp.Tag = "Imp";
			this.toolBarButImp.ToolTipText = "Import From File";
			// 
			// toolBarButPaste
			// 
			this.toolBarButPaste.Enabled = false;
			this.toolBarButPaste.ImageIndex = 6;
			this.toolBarButPaste.Tag = "Paste";
			this.toolBarButPaste.ToolTipText = "Paste From Clipboard";
			// 
			// toolBarSep3
			// 
			this.toolBarSep3.Enabled = false;
			this.toolBarSep3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// toolBarButCrop
			// 
			this.toolBarButCrop.Enabled = false;
			this.toolBarButCrop.ImageIndex = 7;
			this.toolBarButCrop.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.toolBarButCrop.Tag = "Crop";
			this.toolBarButCrop.ToolTipText = "Crop Tool";
			// 
			// toolBarButHand
			// 
			this.toolBarButHand.Enabled = false;
			this.toolBarButHand.ImageIndex = 10;
			this.toolBarButHand.Pushed = true;
			this.toolBarButHand.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.toolBarButHand.Tag = "Hand";
			this.toolBarButHand.ToolTipText = "Toggle Hand Tool";
			// 
			// toolBarSep4
			// 
			this.toolBarSep4.Enabled = false;
			this.toolBarSep4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// toolBarButZoomIn
			// 
			this.toolBarButZoomIn.Enabled = false;
			this.toolBarButZoomIn.ImageIndex = 8;
			this.toolBarButZoomIn.Tag = "ZoomIn";
			this.toolBarButZoomIn.ToolTipText = "Zoom In";
			// 
			// toolBarButZoomOut
			// 
			this.toolBarButZoomOut.Enabled = false;
			this.toolBarButZoomOut.ImageIndex = 9;
			this.toolBarButZoomOut.Tag = "ZoomOut";
			this.toolBarButZoomOut.ToolTipText = "Zoom Out";
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
			this.menuItem1.Text = Lan.g(this,"File");
			// 
			// menuExit
			// 
			this.menuExit.Index = 0;
			this.menuExit.Text = Lan.g(this,"Exit");
			// 
			// menuPrefs
			// 
			this.menuPrefs.Index = 1;
			this.menuPrefs.Text = Lan.g(this,"Preferences");
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
			// ContrDocs
			// 
			this.Controls.Add(this.button1);
			this.Controls.Add(this.PictureBox1);
			this.Controls.Add(this.ToolBar2);
			this.Controls.Add(this.TreeDocuments);
			this.Name = "ContrDocs";
			this.Size = new System.Drawing.Size(838, 606);
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

		public void InstantClasses(){
			PtOrigin=new Point();
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.button1,
			});
		}

		public void ModuleSelected(){
			RefreshModuleData();
			RefreshModuleScreen();
		}

  	private void RefreshModuleData(){
			if (!Patients.PatIsLoaded)
				return;
			Patients.GetFamily(Patients.Cur.PatNum);
			ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString+" - "+Patients.GetCurNameLF();
			if(Patients.Cur.ImageFolder==""){//creates new folder for patient if none present
				string s=Patients.Cur.LName+Patients.Cur.FName;
				for(int i=0;i<s.Length;i++){
					if(Char.IsLetter(s,i)){
						Patients.Cur.ImageFolder+=s.Substring(i,1);
					}
				}
				Patients.Cur.ImageFolder+=Patients.Cur.PatNum.ToString();//ensures unique name
				try{
					patFolder=((Pref)Prefs.HList["DocPath"]).ValueString
						+Patients.Cur.ImageFolder.Substring(0,1)+@"\"
						+Patients.Cur.ImageFolder+@"\";
					Directory.CreateDirectory(patFolder);
					Patients.UpdateCur();
				}
				catch{
					MessageBox.Show(Lan.g(this,"Error.  Could not create folder for patient. "));
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
				}
			}
		}

		private void RefreshModuleScreen(){
			if (Patients.PatIsLoaded){
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString+" - "+Patients.GetCurNameLF();
				toolBarButPrint.Enabled=true;
	      toolBarButDel.Enabled=true;
				toolBarButInfo.Enabled=true;
				toolBarButImp.Enabled=true;
				toolBarButScan.Enabled=true;
				toolBarButCrop.Enabled=true;
				toolBarButPaste.Enabled=true;
				toolBarButHand.Enabled=true;
				toolBarButZoomIn.Enabled=true;
				toolBarButZoomOut.Enabled=true;
			}
			else{
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString;
				Patients.Cur=new Patient();
				toolBarButPrint.Enabled=false;
				toolBarButDel.Enabled=false;
				toolBarButPrint.Enabled=false;
	      toolBarButDel.Enabled=false;
				toolBarButInfo.Enabled=false;
				toolBarButImp.Enabled=false;
				toolBarButScan.Enabled=false;
				toolBarButCrop.Enabled=false;
				toolBarButPaste.Enabled=false;
				toolBarButHand.Enabled=false;
				toolBarButZoomIn.Enabled=false;
				toolBarButZoomOut.Enabled=false;
			}
			FillDocList(false);
		}

		private void ContrDocs_Load(object sender, System.EventArgs e){
			//if (SystemInformation.PrimaryMonitorSize.Height<=768){
		}

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
				try{					WebRequest request=WebRequest.Create(SrcFileName); 					WebResponse response=request.GetResponse();					ImageCurrent=(Bitmap)System.Drawing.Bitmap.FromStream (response.GetResponseStream());					response.Close();			  }			  catch(System.Exception exception){					MessageBox.Show(Lan.g(this,exception.Message)); 			  }
				RecZoom.Width=0;
			}
			else TreeDocuments.SelectedNode=TreeDocuments.Nodes[0];
		}//end RefreshDocList

		public void toolBar2_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e){
			switch (ToolBar2.Buttons[ToolBar2.Buttons.IndexOf(e.Button)].Tag.ToString()){
				case "Pat":
					FormPatientSelect formSelectPatient2=new FormPatientSelect();
					formSelectPatient2.ShowDialog();
					if(formSelectPatient2.DialogResult==DialogResult.OK){
						ModuleSelected();
						FillDocList(false);
					}
					break;
				case "Print":
				  for(int i=0;i<TreeDocuments.Nodes.Count;i++){
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
					break;
				case "Del":
					for(int i=0;i<TreeDocuments.Nodes.Count;i++){
					  if(TreeDocuments.SelectedNode.Equals(TreeDocuments.Nodes[i]))
					    return;
          }
					if(MessageBox.Show(Lan.g(this,"Delete Document?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
						break;
					try{
						File.Delete(patFolder+Documents.Cur.FileName);
					}
					catch{
						MessageBox.Show(Lan.g(this,"Could not delete file.  It may be in use elsewhere."));
						return;
					}
					Documents.DeleteCur();
					FillDocList(false);
					break;
				case "Info":
					for(int i=0;i<TreeDocuments.Nodes.Count;i++){
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
					DisplayImage(false);
					break;
				case "Scan":
					#if(ISXP)
						ScanImage();
					#else
						MessageBox.Show(Lan.g(this,"Scanning only works on Windows XP."));
					#endif
  				break;
				case "Imp": 
					openFileDialog2=new OpenFileDialog();
  				//openFileDialog2.InitialDirectory=
          openFileDialog2.Filter="jpg files(*.jpg)|*.jpg|gif files(*.gif)|*.gif|All files(*.*)|*.*";
          openFileDialog2.FilterIndex=1;
					if(openFileDialog2.ShowDialog()!=DialogResult.OK){
						return;
					}
					if((myStream=openFileDialog2.OpenFile())==null){
						return;
					}
					try{						WebRequest request = WebRequest.Create(openFileDialog2.FileName); 						WebResponse response = request.GetResponse();						ImageCurrent = (Bitmap)System.Drawing.Bitmap.FromStream(response.GetResponseStream());						response.Close();					}					catch(System.Exception exception){						MessageBox.Show(Lan.g(this,exception.Message + " Selected File Not Image."));						myStream.Close();						return;					}
					RecZoom.Width=0;
					DisplayImage(true);
					Documents.Cur=new Document();
					formDocInfo2=new FormDocInfo();
			    formDocInfo2.IsNew=true;
			    formDocInfo2.ShowDialog();
		      if(formDocInfo2.DialogResult==DialogResult.OK){
						try{
							File.Copy(openFileDialog2.FileName,patFolder+Documents.Cur.FileName);
							//
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
					break;
				case "Paste":
					IDataObject clipboard=Clipboard.GetDataObject();
					if(!clipboard.GetDataPresent(DataFormats.Bitmap)){
						MessageBox.Show(Lan.g(this,"No bitmap present on clipboard"));	
						return;
					}
					ImageCurrent=(Bitmap)clipboard.GetData(DataFormats.Bitmap);
					RecZoom.Width=0;
					DisplayImage(true);
					Documents.Cur=new Document();
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
					break;
				case "Crop":
					if(toolBarButCrop.Pushed){ //Crop Mode
						toolBarButHand.Pushed=false;
				  	PictureBox1.Cursor = Cursors.Default;
					}		
					else{
						toolBarButCrop.Pushed=true;
					}
					break;
				case "Hand":
					if(toolBarButHand.Pushed){//Hand Mode
						toolBarButCrop.Pushed=false;
						PictureBox1.Cursor=Cursors.Hand;
						RecCrop=new Rectangle();
						DisplayImage(false);
					}
					else{
						toolBarButHand.Pushed=true;
					}				
					break;
				case "ZoomIn":
					if(ImageCurrent==null) break;
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
					break;
				case "ZoomOut":
					if(ImageCurrent==null) break;
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
					break;
        case "ScanTemp":   
					AutoCrop();
					break;
			} // end of switch
		}//end toolBar2.button_click

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
			for(int i=0;i<TreeDocuments.Nodes.Count;i++){//new jws checks if node is in first level and leaves blank
			  if (TreeDocuments.SelectedNode.Equals(TreeDocuments.Nodes[i])){
				  ShowBlank();
				  return;
        }
	    }
		  Documents.GetCurrent(TreeDocuments.SelectedNode.Tag.ToString());//tag holds the document number of the node:			SrcFileName = patFolder+Documents.Cur.FileName;			try  {		    WebRequest request=WebRequest.Create(SrcFileName); 			  WebResponse response=request.GetResponse();			  ImageCurrent=(Bitmap)System.Drawing.Bitmap.FromStream(response.GetResponseStream());			  response.Close();	    }		  catch(System.Exception exception)  {		    MessageBox.Show(Lan.g(this,exception.Message)); 				ImageCurrent=null;	    }
		  RecZoom.Width=0;
		  DisplayImage(true);
		}

		private void ShowBlank(){
			Graphics mygraphics = PictureBox1.CreateGraphics();
			mygraphics.FillRectangle(Brushes.White,0,0,PictureBox1.ClientRectangle.Width
				,PictureBox1.ClientRectangle.Height);
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
		}

		private void PictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e){
			if(toolBarButHand.Pushed){//Hand Mode
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
			if(toolBarButHand.Pushed){//Hand Mode
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
			}
			else{//Crop Mode				Graphics mygraphics=PictureBox1.CreateGraphics();
				if(MouseIsDown){					mygraphics.DrawImage(ImageCurrent						,new Rectangle(0,0,PictureBox1.ClientRectangle.Width,PictureBox1.ClientRectangle.Height)						,RecZoom,GraphicsUnit.Pixel);					RecCrop=new Rectangle();					RecCrop.X=PtOrigin.X;					RecCrop.Y=PtOrigin.Y;					RecCrop.Width=e.X-PtOrigin.X;					RecCrop.Height=e.Y-PtOrigin.Y;					if(RecZoom.X+(e.X*RecZoom.Width/PictureBox1.ClientRectangle.Width) > ImageCurrent.Width)						RecCrop.Width=((ImageCurrent.Width-RecZoom.X)*PictureBox1.ClientRectangle.Width/RecZoom.Width)							-RecCrop.X;					if(RecZoom.Y+(e.Y*RecZoom.Height/PictureBox1.ClientRectangle.Height)>ImageCurrent.Height)						RecCrop.Height=((ImageCurrent.Height-RecZoom.Y)*PictureBox1.ClientRectangle.Height/RecZoom.Height)							-RecCrop.Y;					//need to unmangle rectangle?? not too important...					mygraphics.DrawRectangle(new Pen(Color.Blue),RecCrop);									}				else{//mouse is up 					mygraphics.DrawImage(ImageCurrent						,new Rectangle(0,0,PictureBox1.ClientRectangle.Width,PictureBox1.ClientRectangle.Height)						,RecZoom,GraphicsUnit.Pixel);					if(e.X<ImageCurrent.Width*PictureBox1.ClientRectangle.Width/RecZoom.Width)						mygraphics.DrawLine(new Pen(Color.Blue),new Point(e.X,0),new Point(e.X,ImageCurrent.Height*PictureBox1.ClientRectangle.Height/RecZoom.Height));					if(e.Y<ImageCurrent.Height*PictureBox1.ClientRectangle.Height/RecZoom.Height)						mygraphics.DrawLine(new Pen(Color.Blue),new Point(0,e.Y),new Point(ImageCurrent.Width*PictureBox1.ClientRectangle.Width/RecZoom.Width,e.Y));					mygraphics.DrawRectangle(new Pen(Color.Blue),RecCrop);				}			}
		}//end mousemove

		private void PictureBox1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e){
			MouseIsDown=false;
			if(toolBarButHand.Pushed){//Hand Mode
				RecZoom=RecTemp;
			}
			else{//Crop Mode
				//Graphics grfx=PictureBox1.CreateGraphics();
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