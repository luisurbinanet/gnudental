using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;
using OpenDental.UI;

namespace OpenDental{
	///<summary>Eventually, the user will be able to edit some image display settings and do a Documents.UpdateCur, but they can't actually make changes to the image.</summary>
	public class FormImageViewer : System.Windows.Forms.Form{
		private OpenDental.UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.PictureBox PictureBox1;
		private System.ComponentModel.IContainer components;
		private Document displayedDoc;
		private Bitmap ImageCurrent;
		private System.Windows.Forms.ImageList imageListTools;
		private Rectangle RecZoom;
		private Point MouseDownOrigin;
		private bool MouseIsDown;
		private Rectangle RecTempZoom;

		///<summary></summary>
		public FormImageViewer()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//Lan.C("All", new System.Windows.Forms.Control[] {
			//});
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormImageViewer));
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.PictureBox1 = new System.Windows.Forms.PictureBox();
			this.imageListTools = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListTools;
			this.ToolBarMain.Location = new System.Drawing.Point(0, 0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(903, 29);
			this.ToolBarMain.TabIndex = 11;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// PictureBox1
			// 
			this.PictureBox1.BackColor = System.Drawing.SystemColors.Window;
			this.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PictureBox1.Location = new System.Drawing.Point(0, 29);
			this.PictureBox1.Name = "PictureBox1";
			this.PictureBox1.Size = new System.Drawing.Size(903, 669);
			this.PictureBox1.TabIndex = 12;
			this.PictureBox1.TabStop = false;
			this.PictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox1_Paint);
			this.PictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseUp);
			this.PictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseMove);
			this.PictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseDown);
			// 
			// imageListTools
			// 
			this.imageListTools.ImageSize = new System.Drawing.Size(22, 22);
			this.imageListTools.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTools.ImageStream")));
			this.imageListTools.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// FormImageViewer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(903, 698);
			this.Controls.Add(this.PictureBox1);
			this.Controls.Add(this.ToolBarMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormImageViewer";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Image Viewer";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Resize += new System.EventHandler(this.FormImageViewer_Resize);
			this.Load += new System.EventHandler(this.FormImageViewer_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormImageViewer_Load(object sender, System.EventArgs e) {
			//RecZoom.Width=0;
			//DisplayImage(true);
			LayoutToolBar();
		}

		/// <summary>This form will get the necessary images off disk so that it can control layout.</summary>
		public void SetImage(Document thisDocument,string displayTitle){
			//for now, the document is single. Later, it will get groups for composite images/mounts.
			Text=displayTitle;
			displayedDoc=thisDocument;
			ArrayList docNums=new ArrayList();
			docNums.Add(thisDocument.DocNum);
			string fileName=(string)Documents.GetPaths(docNums)[0];
			if(!File.Exists(fileName)){
				MessageBox.Show(fileName+" could not be found.");
				return;
			}
			try{				ImageCurrent=(Bitmap)Bitmap.FromFile(fileName);//only jpg and gif allowed to run this method anyway.	    }		  catch(System.Exception exception){		    MessageBox.Show(Lan.g(this,exception.Message)); 				ImageCurrent=null;	    }
			RecZoom.Width=0;
			DisplayImage(true);
		}

		private void FormImageViewer_Resize(object sender, System.EventArgs e) {
			if(this.WindowState==FormWindowState.Minimized)
				return;
			RecZoom.Width=0;
			DisplayImage(true);
		}

		///<summary>Causes the toolbar to be laid out again.</summary>
		public void LayoutToolBar(){
			//ODToolBarButton button;
			ToolBarMain.Buttons.Clear();
			ToolBarMain.Buttons.Add(new ODToolBarButton("",0,Lan.g(this,"Zoom In"),"ZoomIn"));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",1,Lan.g(this,"Zoom Out"),"ZoomOut"));
			ToolBarMain.Invalidate();
		}

		private void ToolBarMain_ButtonClick(object sender, OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			switch(e.Button.Tag.ToString()){
				case "ZoomIn":
					OnZoomIn_Click();
					break;
				case "ZoomOut":
					OnZoomOut_Click();
					break;
			}
		}

		private void OnZoomIn_Click() {
			if(ImageCurrent==null)
				return;
			Rectangle sourceRect=GetSourceRect();
			RecZoom.Height=RecZoom.Height/2;
			RecZoom.Width=RecZoom.Width/2;
			if(displayedDoc.DegreesRotated==90 || displayedDoc.DegreesRotated==270){
				//sideways
				//maintain original x
				if(displayedDoc.DegreesRotated==270){
					//maintain the lower edge instead.
					if(displayedDoc.IsFlipped){
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
				if(displayedDoc.DegreesRotated==0){
					//do nothing. Y is already correct.
				}
				else{//180
					//maintain the lower edge instead.
					RecZoom.Y=RecZoom.Y+RecZoom.Height;
				}
				//maintain original x center.(RecZoom.x+width)
				RecZoom.X=RecZoom.X+(RecZoom.Width/2);//works if flipped
				if(displayedDoc.IsFlipped){
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
			DisplayImage(false);
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
				DisplayImage(true);
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
					if(displayedDoc.IsFlipped){
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
					if(displayedDoc.IsFlipped){
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
			DisplayImage(true);
		}

		

		private void DisplayImage(bool clearFirst){
			if(this.WindowState==FormWindowState.Minimized)
				return;
			//Enhanced=enhanced;
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
			//RecCrop=new Rectangle();//so it won't show anymore
			//if(enhanced){
			switch(displayedDoc.DegreesRotated){
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
			//}
			if(displayedDoc.DegreesRotated==0 || displayedDoc.DegreesRotated==180){
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

		///<summary>This is almost a duplicate of ContrDocs.GetSourceRect, except it does not use Documents.Cur because it has its own encapsulated document object.</summary>
		private Rectangle GetSourceRect(){
			string whiteSide=GetWhiteSide();//L,R,T,or B
			Rectangle retRect=new Rectangle(0,0,ImageCurrent.Width,ImageCurrent.Height);
			float imageRatio=(float)ImageCurrent.Width/(float)ImageCurrent.Height;
			float screenRatio
				=(float)PictureBox1.ClientRectangle.Width/(float)PictureBox1.ClientRectangle.Height;
			//if(!Enhanced){
			//	if(whiteSide=="L" || whiteSide=="R"){
			//		retRect.Width=(int)(retRect.Height*screenRatio);
			//	}
			//	else{
			//		retRect.Height=(int)(retRect.Width/screenRatio);
			//	}
			//}
			if(displayedDoc.DegreesRotated==0 || displayedDoc.DegreesRotated==180){
				if(whiteSide=="L" || whiteSide=="R"){
					retRect.Width=(int)(retRect.Height*screenRatio);
				}
				else{
					retRect.Height=(int)(retRect.Width/screenRatio);
				}
				if(displayedDoc.DegreesRotated==0){
					if(whiteSide=="L"){
						retRect.X=ImageCurrent.Width-retRect.Width;//a negative value
					}
				}
				if(displayedDoc.DegreesRotated==180){
					if(whiteSide=="T"){
						retRect.Y=ImageCurrent.Height-retRect.Height;//a negative value
					}
					if(whiteSide=="L"){
						retRect.X=ImageCurrent.Width-retRect.Width;//a negative value??
					}
				}
			}
			else if(displayedDoc.DegreesRotated==90 || displayedDoc.DegreesRotated==270){
				if(whiteSide=="L" || whiteSide=="R"){//image shorter than screen width
					retRect.Height=(int)(retRect.Width*screenRatio);//make it taller
				}
				else{
					retRect.Width=(int)(retRect.Height/screenRatio);
				}
				if(displayedDoc.DegreesRotated==90){
					if(whiteSide=="T"){
						retRect.X=ImageCurrent.Width-retRect.Width;//a negative value
					}
					if(whiteSide=="L"){
						retRect.Y=ImageCurrent.Height-retRect.Height;//a negative value
					}
				}
				if(displayedDoc.DegreesRotated==270){
					if(whiteSide=="T"){
						retRect.X=ImageCurrent.Width-retRect.Width;//a negative value
					}
				}
			}
			if(displayedDoc.IsFlipped){
				retRect.X=retRect.Right;
				retRect.Width=-retRect.Width;
			}
			return retRect;
		}//GetSourceRect

		///<summary>Just like ContrDocs.GetWhiteSide, except, does not use Documents.Cur. </summary>
		private string GetWhiteSide(){
			float imageRatio=(float)ImageCurrent.Width/(float)ImageCurrent.Height;
			float screenRatio
				=(float)PictureBox1.ClientRectangle.Width/(float)PictureBox1.ClientRectangle.Height;
			//if(!Enhanced){
			//	if(imageRatio<screenRatio){//if image narrower than picturebox
			//		return "R";
			//	}
			//	else{//if image shorter than picturebox
			//		return "B";
			//	}
			//}
			if(displayedDoc.DegreesRotated==0){
				if(imageRatio<screenRatio){//if image narrower
					if(displayedDoc.IsFlipped)
						return "L";
					else
						return "R";
				}
				else{//if image shorter
					return "B";
				}
			}
			if(displayedDoc.DegreesRotated==90){//imagine image on side when doing calculations
				//if the image(laid sideways) is proprortionally narrower than the picturebox
  			if((1/imageRatio)<screenRatio){
					return "L";
				}
				else{//if image is shorter
					if(displayedDoc.IsFlipped)
						return "T";
					else
						return "B";
				}
			}
			else if(displayedDoc.DegreesRotated==180){
				if(imageRatio<screenRatio){//if image narrower
					if(displayedDoc.IsFlipped)
						return "R";
					else
						return "L";
				}
				else{//if image shorter
					return "T";
				}
			}
			else{ //if(displayedDoc.DegreesRotated==270){
  			if((1/imageRatio)<screenRatio){//if image(laid sideways) is narrower
					//adjust retRect to be wider so it actually extends beyond image boundaries
					return "R";
				}
				else{//if image is shorter
					if(displayedDoc.IsFlipped)
						return "B";
					else
						return "T";
				}
			}
		}//GetWhiteSide()

		private void PictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e) {
			if(ImageCurrent==null){
				return;
			}
			Graphics g=e.Graphics;
			//if(Enhanced){
			switch(displayedDoc.DegreesRotated){
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
			if(displayedDoc.DegreesRotated==0 || displayedDoc.DegreesRotated==180){
				g.DrawImage(ImageCurrent
					,new Rectangle(0,0,PictureBox1.ClientRectangle.Width,PictureBox1.ClientRectangle.Height)
					,RecZoom,GraphicsUnit.Pixel);
			}
			else{//90 or 270
				g.DrawImage(ImageCurrent
					,new Rectangle(0,0,PictureBox1.ClientRectangle.Height,PictureBox1.ClientRectangle.Width)
					,RecZoom,GraphicsUnit.Pixel);
			}
			//}
			//else{
			//	g.DrawImage(ImageCurrent
			//		,new Rectangle(0,0,PictureBox1.ClientRectangle.Width,PictureBox1.ClientRectangle.Height)
			//		,RecZoom,GraphicsUnit.Pixel);
			//}
		}

		private void PictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e){
			//if(ToolBarMain.Buttons["Hand"].Pushed){//hand mode
				MouseDownOrigin=new Point(e.X,e.Y);
				MouseIsDown=true;
			/*}
			else{//Crop Mode
				MouseDownOrigin=new Point(e.X,e.Y);
				MouseIsDown=true;
			}*/
		}		

		private void PictureBox1_MouseMove(object sender,System.Windows.Forms.MouseEventArgs e){
			if(ImageCurrent==null)
				return;
			Graphics g=PictureBox1.CreateGraphics();
			switch(displayedDoc.DegreesRotated){
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
			//if(ToolBarMain.Buttons["Hand"].Pushed){//hand mode
			if(!MouseIsDown)
				return;
			RecTempZoom=RecZoom;
			//these are the amounts the rectangle moves across the image. They are negative, because
			//as the mouse drags down, the rectangle must move up to expose the upper part of the image.
			int moveX=(int)(-(e.X-MouseDownOrigin.X)*2*
				Math.Abs((decimal)RecZoom.Width)/(decimal)PictureBox1.ClientRectangle.Width);
			//Debug.WriteLine(moveX);
			int moveY=-(e.Y-MouseDownOrigin.Y)*2* RecZoom.Height/PictureBox1.ClientRectangle.Height;
			if(displayedDoc.DegreesRotated==0){
				RecTempZoom.Y=RecZoom.Y+moveY;
				if(displayedDoc.IsFlipped){
					RecTempZoom.X=RecZoom.X-moveX;
				}
				else{//normal
					RecTempZoom.X=RecZoom.X+moveX;
				}
			}
			else if(displayedDoc.DegreesRotated==90){
				RecTempZoom.Y=RecZoom.Y-moveX;
				if(displayedDoc.IsFlipped){
					RecTempZoom.X=RecZoom.X-moveY;
				}
				else{
					RecTempZoom.X=RecZoom.X+moveY;
				}
			}
			else if(displayedDoc.DegreesRotated==180){
				RecTempZoom.Y=RecZoom.Y-moveY;
				if(displayedDoc.IsFlipped){
					RecTempZoom.X=RecZoom.X+moveX;
				}
				else{
					RecTempZoom.X=RecZoom.X-moveX;
				}
			}
			else if(displayedDoc.DegreesRotated==270){
				RecTempZoom.Y=RecZoom.Y+moveX;
				if(displayedDoc.IsFlipped){
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
				if(displayedDoc.DegreesRotated==0 && whiteSide=="B")
					startedWhiteB=true;
				else if(displayedDoc.DegreesRotated==90 && whiteSide=="L")
					startedWhiteB=true;
				else if(displayedDoc.DegreesRotated==180 && whiteSide=="T")
					startedWhiteB=true;
				else if(displayedDoc.DegreesRotated==270 && whiteSide=="R")
					startedWhiteB=true;
			}
			if(RecZoom.X+RecZoom.Width>ImageCurrent.Width){
				if(displayedDoc.DegreesRotated==0 && whiteSide=="R")
					startedWhiteR=true;
				else if(displayedDoc.DegreesRotated==90 && whiteSide=="B")
					startedWhiteR=true;
				else if(displayedDoc.DegreesRotated==180 && whiteSide=="L")
					startedWhiteR=true;
				else if(displayedDoc.DegreesRotated==270 && whiteSide=="T")
					startedWhiteR=true;
			}
			if(RecZoom.X+RecZoom.Width<0){//width is neg because only for flipped
				if(displayedDoc.DegreesRotated==0 && whiteSide=="L")
					startedWhiteL=true;
				else if(displayedDoc.DegreesRotated==90 && whiteSide=="T")
					startedWhiteL=true;
				else if(displayedDoc.DegreesRotated==180 && whiteSide=="R")
					startedWhiteL=true;
				else if(displayedDoc.DegreesRotated==270 && whiteSide=="B")
					startedWhiteL=true;
			}
			//limit movement to right
			if(startedWhiteR){
				//same for flipped and regular
				if(RecTempZoom.X>RecZoom.X)
					RecTempZoom.X=RecZoom.X;
			}
			else{
				if(displayedDoc.IsFlipped){
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
				if(displayedDoc.IsFlipped){
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
			if(displayedDoc.DegreesRotated==0 || displayedDoc.DegreesRotated==180){
				g.DrawImage(ImageCurrent
					,new Rectangle(0,0,PictureBox1.ClientRectangle.Width,PictureBox1.ClientRectangle.Height)
					,RecTempZoom,GraphicsUnit.Pixel);
			}
			else{//90 or 270
				g.DrawImage(ImageCurrent
					,new Rectangle(0,0,PictureBox1.ClientRectangle.Height,PictureBox1.ClientRectangle.Width)
					,RecTempZoom,GraphicsUnit.Pixel);
			}
			g.Dispose();
		}//end mousemove

		private void PictureBox1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e){
			MouseIsDown=false;
			RecZoom=RecTempZoom;
		}


		


		

	}
}





















