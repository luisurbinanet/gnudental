/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	public class ContrApptSingle : System.Windows.Forms.UserControl{
		private System.ComponentModel.Container components = null;// Required designer variable.
		public static int ClickedAptNum;//Set on mouse down or from Appt module
		public static int SelectedAptNum;//set manually
		public bool ThisIsPinBoard;
		public static bool PinBoardIsSelected;
		public static int[][] ProvBar;
		public InfoApt Info;
		public Bitmap Shadow;

		public ContrApptSingle(){
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
		}

		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code

		private void InitializeComponent(){
			// 
			// ContrApptSingle
			// 
			this.Name = "ContrApptSingle";
			this.Size = new System.Drawing.Size(85, 72);
			this.Load += new System.EventHandler(this.ContrApptSingle_Load);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ContrApptSingle_MouseDown);

		}
		#endregion
		
		protected override void OnPaint(PaintEventArgs pea){
			//Graphics grfx=pea.Graphics;
			//grfx.DrawImage(shadow,0,0);
		}

		
		public void SetLocation(){
			if(Info.IsNext){
				Width=110;
				//don't change location
			}
			else{
				Location=new Point(ConvertToX(),ConvertToY());
				Width=ContrApptSheet.ColWidth-1;
				Height=Info.MyApt.Pattern.Length*ContrApptSheet.Lh;
			}
		}

		public void CreateShadow(){
			if(Shadow!=null){
				Shadow=null;
			}
			//MessageBox.Show(Width.ToString());
			Shadow=new Bitmap(Width,Height);
			Graphics grfx = Graphics.FromImage(Shadow);//pea.Graphics;
			Pen penB = new Pen(Color.Black);
			Pen penW = new Pen(Color.White);
			Pen penGr = new Pen(Color.SlateGray);
			Pen penDG = new Pen(Color.DarkSlateGray);
			Color timeColor;
			if(Info.MyApt.AptStatus==ApptStatus.Complete){
				grfx.FillRectangle(new SolidBrush(Defs.GetColor(DefCat.AppointmentColors,141)),7,0,Width-7,Height);
				timeColor=Defs.GetColor(DefCat.AppointmentColors,140);
			}
			else{
				grfx.FillRectangle(new SolidBrush(Providers.GetColor(Info.MyApt.ProvNum)),7,0,Width-7,Height);
				timeColor=Defs.GetColor(DefCat.ApptConfirmed,Info.MyApt.Confirmed);
			}
			grfx.FillRectangle(new SolidBrush(timeColor),0,0,7,Height);
			grfx.DrawLine(penB,7,0,7,Height);
			Font fontSF = new Font("Arial",8);//Small Font", 7);
			grfx.TextRenderingHint=TextRenderingHint.SingleBitPerPixelGridFit;//to make printing clearer
			for (int i=0; i<Info.MyApt.Pattern.Length; i++){
				grfx.DrawLine(penB,6,i*ContrApptSheet.Lh+1,1,(i+1)*ContrApptSheet.Lh-2);
				//grfx.DrawString("/",fontCourier,new SolidBrush(Color.Black),-1,i*Lh);
				if (Info.MyApt.Pattern.Substring(i,1)=="X"){
					grfx.DrawLine(penB,1,i*ContrApptSheet.Lh+1,6,(i+1)*ContrApptSheet.Lh-2);
					//grfx.FillRectangle(new SolidBrush(Providers.GetColor(MyApt.ProvNum))
					//	,9,i*ContrApptSheet.Lh,Width-10,(i+1)*ContrApptSheet.Lh);
					
					//int timeBarInc = ConvertToY()/Lh+i;
					//TimeBar[timeBarInc]+=1;
					//if (timeBar[timeBarInc]==0)
				}
				else{
					//grfx.FillRectangle(new SolidBrush(Color.LightBlue),9,i*ContrApptSheet.Lh,Width-10,(i+1)*ContrApptSheet.Lh);
				}
				if (i==0)
					grfx.DrawString(Info.Lines[i],fontSF,new SolidBrush(Color.Black),13,i*ContrApptSheet.Lh);
				else
					grfx.DrawString(Info.Lines[i],fontSF,new SolidBrush(Color.Black),9,i*ContrApptSheet.Lh);

			}
			grfx.DrawRectangle(new Pen(Color.Black),0,0,Width-1,Height-1);
			grfx.FillRectangle(new SolidBrush(Color.White),1,1,12,ContrApptSheet.Lh-2);
			grfx.DrawRectangle(new Pen(Color.Black),0,0,13,ContrApptSheet.Lh-1);//started out as 11
			grfx.DrawString(Info.CreditAndIns,fontSF,new SolidBrush(Color.Black),0,-1);
			//if (apptIsSelected)
				//if (selectedIndex==myIndex){
			if(PinBoardIsSelected & ThisIsPinBoard
				|| (Info.MyApt.AptNum==SelectedAptNum && !ThisIsPinBoard)){
					//Left
					grfx.DrawLine(penW,8,ContrApptSheet.Lh,8,Height-2);
					grfx.DrawLine(penW,9,ContrApptSheet.Lh,9,Height-3);
					grfx.DrawLine(penW,10,ContrApptSheet.Lh,10,Height-3);
					grfx.DrawLine(penW,14,1,14,ContrApptSheet.Lh);
					//Right
					grfx.DrawLine(penDG,Width-2,1,Width-2,Height-2);
					grfx.DrawLine(penDG,Width-3,2,Width-3,Height-3);
					//grfx.DrawLine(penGr,Width-4,2,Width-4,Height-2);
					//Top
					grfx.DrawLine(penW,8,ContrApptSheet.Lh,14,ContrApptSheet.Lh);
					grfx.DrawLine(penW,14,1,Width-2,1);
					grfx.DrawLine(penW,14,2,Width-3,2);
					//bottom
					grfx.DrawLine(penDG,9,Height-2,Width-2,Height-2);
					grfx.DrawLine(penDG,10,Height-3,Width-3,Height-3);
			}
			//grfx.DrawString(":10",font,new SolidBrush(Color.Black),timeWidth-19,i*Lh*6+Lh-1);
			if(Info.MyApt.AptStatus==ApptStatus.Broken){
				grfx.DrawLine(new Pen(Color.Black),8,1,Width-1,Height-1);
				grfx.DrawLine(new Pen(Color.Black),8,Height-1,Width-1,1);
			}
			this.BackgroundImage=Shadow;
			//Shadow=null;
			grfx.Dispose();
		}

		private int ConvertToX (){
			return ContrApptSheet.TimeWidth+ContrApptSheet.ProvWidth*ContrApptSheet.ProvCount
				+ContrApptSheet.ColWidth*(ApptViewItems.GetIndexOp(Info.MyApt.Op))+1;
		}

		public int ConvertToY (){
			return (Convert.ToInt32(Info.MyApt.AptDateTime.Hour)*6+Convert.ToInt32(Info.MyApt.AptDateTime.Minute/10))*ContrApptSheet.Lh;
		}

		private void ContrApptSingle_Load(object sender, System.EventArgs e){
			/*
			if(Info.IsNext){
				Width=110;
				//don't change location
			}
			else{
				Location=new Point(ConvertToX(),ConvertToY());
				Width=ContrApptSheet.ColWidth-1;
				Height=Info.MyApt.Pattern.Length*ContrApptSheet.Lh;
			}
			*/
		}

		public void SetSize(){
			Height=Info.MyApt.Pattern.Length*ContrApptSheet.Lh;
			if(ThisIsPinBoard){
				if(Height > ContrAppt.PinboardSize.Height-4)
					Height=ContrAppt.PinboardSize.Height-4;
				if(Width > ContrAppt.PinboardSize.Width-4)
					Width=ContrAppt.PinboardSize.Width-4;
			}
		}

		private void ContrApptSingle_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			ClickedAptNum=Info.MyApt.AptNum;
		}




	}//end class
}//end namespace
