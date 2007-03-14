/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class ContrApptSheet : System.Windows.Forms.UserControl{
		private System.ComponentModel.Container components = null;// Required designer variable.
		///<summary>The width of each operatory.</summary>
		public static int ColWidth;
		///<summary></summary>
		public static int TimeWidth;
		///<summary></summary>
		public static int ProvWidth;
		///<summary></summary>
		public static int Lh;
		///<summary></summary>
		public static int ColCount;
		///<summary></summary>
		public static int ProvCount;
		///<summary>Based on the view.  If no view, then it is set to 1. Different computers can be showing different views.</summary>
		public static int RowsPerIncr;
		///<summary>Pulled from Prefs AppointmentTimeIncrement.  For now, either 10 or 15. An increment can be one or more rows.</summary>
		public static int MinPerIncr;
		///<summary>Typical values would be 10,15,5,or 7.5.</summary>
		public static float MinPerRow;
		///<summary>Rows per hour, based on RowsPerIncr and MinPerIncr</summary>
		public static int RowsPerHr;
		///<summary></summary>
		public Bitmap Shadow;
		///<summary></summary>
		public bool IsScrolling=false;
		//public int selectedCat;//selected ApptCategory.

		///<summary></summary>
		public ContrApptSheet(){
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
			//fix: need to add following for design-time support??(would currently cause bugs):
				//ColWidth=100;
				//ColCount=4;
				//ProvCount=3;
				//ContrApptSingle.ProvBar = new int[ProvCount][];//design-time support
				//for(int i=0;i<ProvCount;i++){
				//	ContrApptSingle.ProvBar[i] = new int[144];
				//}
			TimeWidth=37;
			ProvWidth=8;
			Lh=13;
			//selectedCat=-1;
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

		#region Component Designer generated code

		private void InitializeComponent(){
			// 
			// ContrApptSheet
			// 
			this.Name = "ContrApptSheet";
			this.Size = new System.Drawing.Size(482, 540);
			this.Load += new System.EventHandler(this.ContrApptSheet_Load);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ContrApptSheet_Layout);

		}
		#endregion

		private void ContrApptSheet_Load(object sender, System.EventArgs e) {
			
		}

		private void ContrApptSheet_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
			//Height=Lh*24*6;
			//Width=TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount;
		}

		/*public int ConvertToX (int op){
				return timeWidth+ProvWidth*ProvCount+ColWidth*(op-1);
		}

		public int ConvertToY (double timeApt){//decimal port. of timeApt used as base 6, not 10
			int myHours=0;
			int myMins=0;
			if (timeApt > 7.5){ //after 7:50 is am appt
				//myHours=(int)Decimal.Truncate(timeApt);
				myMins=(int)((timeApt-myHours)*10);
				return ((myHours-startTime)*6+myMins)*Lh;
			}
			else{//pm appt
				return 100;
			}
		}*/

		///<summary></summary>
		public int DoubleClickToOp(int newX){
			int retVal=(int)Math.Floor((double)(newX-TimeWidth-ProvWidth*ProvCount)/ColWidth);
			if(retVal>ColCount-1)
				retVal=ColCount-1;
			if(retVal<0)
				retVal=0;
			return retVal;
		}

		///<summary>Called when mouse down anywhere on apptSheet. Automatically rounds down.</summary>
		public int DoubleClickToHour(int newY){
			int retVal=newY/Lh/RowsPerHr;//newY/Lh/6;
			return retVal;
		}

		///<summary>Called when mouse down anywhere on apptSheet. This will give very precise minutes. It is not rounded for accuracy.</summary>
		public int DoubleClickToMin(int newY){
			int hourPortion=DoubleClickToHour(newY)*Lh*RowsPerHr;
			float MinPerPixel=60/(float)Lh/(float)RowsPerHr;
			int minutes=(int)((newY-hourPortion)*MinPerPixel);
			return minutes;
		}

		///<summary>converts x-coordinate to operatory index of ApptCatItems.VisOps</summary>
		public int ConvertToOp(int newX){
			int retVal=0;
			retVal=(int)Math.Round((double)(newX-TimeWidth-ProvWidth*ProvCount)/ColWidth);
			//make sure it's not outside bounds of array:
			if(retVal > ApptViewItems.VisOps.Length-1)
				//Defs.Short[(int)DefCat.Operatories].Length-1)
				retVal=ApptViewItems.VisOps.Length-1;
					//Defs.Short[(int)DefCat.Operatories].Length-1;
			if(retVal<0)
				retVal=0;
			return retVal;
		}

		///<summary>Used when dropping an appointment to a new location. Rounds to the nearest increment.</summary>
		public int ConvertToHour(int newY){
			//return (int)((newY+Lh/2)/6/Lh);
			return (int)(((double)newY+(double)Lh*(double)RowsPerIncr/2)/(double)RowsPerHr/(double)Lh);
		}

		///<summary>Used when dropping an appointment to a new location. Rounds to the nearest increment.</summary>
		public int ConvertToMin(int newY){
			//int retVal=(int)(Decimal.Remainder(newY,6*Lh)/Lh)*10;
			//first, add pixels equivalent to 1/2 increment: newY+Lh*RowsPerIncr/2
			//Yloc     Height     Rows      1
			//---- + ( ------ x --------- x - )
			//  1       Row     Increment   2
			//then divide by height per hour: RowsPerHr*Lh
			//Rows   Height
			//---- * ------
			//Hour    Row
			int pixels=(int)Decimal.Remainder(
				(decimal)newY+(decimal)Lh*(decimal)RowsPerIncr/2
				,(decimal)RowsPerHr*(decimal)Lh);
			//We are only interested in the remainder, and this is called pixels.
			//Convert pixels to increments. Round down to nearest increment when converting to int.
			//pixels/Lh/RowsPerIncr:
			//pixels    Row     Increment
			//------ x ------ x ---------
			//  1      pixels     Rows
			int increments=(int)((double)pixels/(double)Lh/(double)RowsPerIncr);
			//Convert increments to minutes: increments*MinPerIncr
      int retVal=increments*MinPerIncr;
			if(retVal==60)
				return 0;
			return retVal;
		}

		///<summary></summary>
		protected override void OnPaint(PaintEventArgs pea){
			DrawShadow();
		}

		///<summary></summary>
		public void CreateShadow(){
			if(Shadow!=null){
				Shadow=null;
			}
			if(Width<2)
				return;
			Shadow=new Bitmap(Width,Height);
			if(RowsPerIncr==0)
				RowsPerIncr=1;
			//MessageBox.Show(StartTime.ToString());
			Graphics g=Graphics.FromImage(Shadow);
			//int totalHeightt=(int)(24*Lh*60/Prefs.GetInt("AppointmentTimeIncrement"));
			//if(TwoRowsPerIncrement){
			//	totalHeight=totalHeight*2;
			//}
			//Graphics grfx = pea.Graphics;
			//background
			g.FillRectangle(new SolidBrush(Color.LightGray),0,0,TimeWidth,Height);//L time bar
			SolidBrush openBrush;
			SolidBrush closedBrush;
      SolidBrush holidayBrush;
			try{
				openBrush=new SolidBrush(Defs.Long[(int)DefCat.AppointmentColors][0].ItemColor);
				closedBrush=new SolidBrush(Defs.Long[(int)DefCat.AppointmentColors][1].ItemColor);
        holidayBrush=new SolidBrush(Defs.Long[(int)DefCat.AppointmentColors][4].ItemColor);  
			}
			catch{//this is just for design-time
				openBrush=new SolidBrush(Color.White);
				closedBrush=new SolidBrush(Color.LightGray);
        holidayBrush=new SolidBrush(Color.FromArgb(255,128,128));
			}
			g.FillRectangle(closedBrush,TimeWidth,0,ColWidth*ColCount+ProvWidth*ProvCount,Height);//main background
      if(Schedules.ListDay!=null && Schedules.ListDay.Length > 0){
				for(int i=0;i<Schedules.ListDay.Length;i++){	
					if(Schedules.ListDay[i].Status==SchedStatus.Holiday){
 						g.FillRectangle(holidayBrush,TimeWidth,0,ColWidth*ColCount+ProvWidth*ProvCount,Height);
					} 
          else{
 						g.FillRectangle(openBrush
							,TimeWidth
							,Schedules.ListDay[i].StartTime.Hour*Lh*RowsPerHr//6
							+(int)Schedules.ListDay[i].StartTime.Minute*Lh/MinPerRow//10
							,ColWidth*ColCount+ProvWidth*ProvCount
							,(Schedules.ListDay[i].StopTime-Schedules.ListDay[i].StartTime).Hours*Lh*RowsPerHr//6
							+(Schedules.ListDay[i].StopTime-Schedules.ListDay[i].StartTime).Minutes*Lh/MinPerRow);//10
          }
				}         
      }
      else{//default sched
			  for(int i=0;i<SchedDefaults.List.Length;i++){
					if(SchedDefaults.List[i].DayOfWeek==(int)Appointments.DateSelected.DayOfWeek){
						g.FillRectangle(openBrush
							,TimeWidth
							,SchedDefaults.List[i].StartTime.Hour*Lh*RowsPerHr
							+SchedDefaults.List[i].StartTime.Minute*Lh/MinPerRow
							,ColWidth*ColCount+ProvWidth*ProvCount
							,(SchedDefaults.List[i].StopTime-SchedDefaults.List[i].StartTime).Hours*Lh*RowsPerHr
							+(SchedDefaults.List[i].StopTime-SchedDefaults.List[i].StartTime).Minutes*Lh/MinPerRow);
							//*((float)Lh/(float)MinPerRow));
					}
				}
      }
			g.FillRectangle(new SolidBrush(Color.LightGray),TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,0,TimeWidth,Height);//R time bar
			//vert
			g.DrawLine(new Pen(Color.DarkGray),0,0,0,Height);
			g.DrawLine(new Pen(Color.White),TimeWidth-2,0,TimeWidth-2,Height);
			g.DrawLine(new Pen(Color.DarkGray),TimeWidth-1,0,TimeWidth-1,Height);
			for(int i=0;i<ProvCount;i++){
				g.DrawLine(new Pen(Color.DarkGray),TimeWidth+ProvWidth*i,0,TimeWidth+ProvWidth*i,Height);
			}
			for(int i=0;i<ColCount;i++){
				g.DrawLine(new Pen(Color.DarkGray),TimeWidth+ProvWidth*ProvCount+ColWidth*i,0
					,TimeWidth+ProvWidth*ProvCount+ColWidth*i,Height);
			}
			g.DrawLine(new Pen(Color.DarkGray), TimeWidth+ProvWidth*ProvCount+ColWidth*ColCount,0
				,TimeWidth+ProvWidth*ProvCount+ColWidth*ColCount,Height);
			g.DrawLine(new Pen(Color.DarkGray),TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount,0
				,TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount,Height);
			//horiz gray
			for(int i=0;i<(Height);i+=Lh*RowsPerIncr){
				g.DrawLine(new Pen(Color.LightGray),TimeWidth,i
					,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i);
			}
			//horiz Hour lines
			for(int i=0;i<Height;i+=Lh*RowsPerHr){
				g.DrawLine(new Pen(Color.LightGray),0,i-1//was white
					,TimeWidth*2+ColWidth*ColCount+ProvWidth*ProvCount,i-1);
				g.DrawLine(new Pen(Color.DarkSlateGray),0,i,TimeWidth,i);
				g.DrawLine(new Pen(Color.Black),TimeWidth,i
					,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i);
				g.DrawLine(new Pen(Color.DarkSlateGray),TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i
					,TimeWidth*2+ColWidth*ColCount+ProvWidth*ProvCount,i);
			}
			//horiz halfHour lines
			/*for(int i=0;i<Height;i+=Lh*RowsPerHr){
				g.DrawLine(new Pen(Color.White),0,i-1
					,TimeWidth*2+ColWidth*ColCount+ProvWidth*ProvCount,i-1);
				g.DrawLine(new Pen(Color.DarkSlateGray),0,i,TimeWidth,i);
				g.DrawLine(new Pen(Color.Black),TimeWidth,i
					,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i);
				g.DrawLine(new Pen(Color.DarkSlateGray),TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i
					,TimeWidth*2+ColWidth*ColCount+ProvWidth*ProvCount,i);
			}*/
			//time bar
			for(int j=0;j<ContrApptSingle.ProvBar.Length;j++){
				for(int i=0;i<24*RowsPerHr;i++){
					//144;i++){//ContrApptSingle.TimeBar.Length;i++){
					switch(ContrApptSingle.ProvBar[j][i]){
						case 0:
							break;
						case 1:
							try{
								g.FillRectangle(new SolidBrush(Providers.List[ApptViewItems.VisProvs[j]].ProvColor)
									,TimeWidth+ProvWidth*j+1,(i*Lh)+1,ProvWidth-1,Lh-1);
							}
							catch{//design-time
								g.FillRectangle(new SolidBrush(Color.White)
									,TimeWidth+ProvWidth*j+1,(i*Lh)+1,ProvWidth-1,Lh-1);
							}
							break;
						case 2:
							g.FillRectangle(new HatchBrush(HatchStyle.DarkUpwardDiagonal
								,Color.Black,Providers.List[ApptViewItems.VisProvs[j]].ProvColor)
								,TimeWidth+ProvWidth*j+1,(i*Lh)+1,ProvWidth-1,Lh-1);
							break;
						default://more than 2
							g.FillRectangle(new SolidBrush(Color.Black)
								,TimeWidth+ProvWidth*j+1,(i*Lh)+1,ProvWidth-1,Lh-1);
							break;
					}
				}
			}//end for ProvBar
			//time indicator
			int curTimeY=(int)(DateTime.Now.Hour*Lh*RowsPerHr+DateTime.Now.Minute/60f*(float)Lh*RowsPerHr);
			g.DrawLine(new Pen(Color.Red),0,curTimeY
				,TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount,curTimeY);
			g.DrawLine(new Pen(Color.Red),0,curTimeY+1
				,TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount,curTimeY+1);
			//minutes
			Font font=new Font(FontFamily.GenericSansSerif,8);//was msSans
			Font bfont=new Font(FontFamily.GenericSansSerif,8,FontStyle.Bold);//was Arial
			g.TextRenderingHint=TextRenderingHint.SingleBitPerPixelGridFit;//to make printing clearer
			DateTime hour;
			string hFormat="";
			CultureInfo ci=(CultureInfo)CultureInfo.CurrentCulture.Clone();
			ci.DateTimeFormat.AMDesignator=ci.DateTimeFormat.AMDesignator.ToLower();
			ci.DateTimeFormat.PMDesignator=ci.DateTimeFormat.PMDesignator.ToLower();
			string shortPattern=ci.DateTimeFormat.ShortTimePattern;
			if(shortPattern.IndexOf("hh")!=-1){//if hour is 01-12
				hFormat+="hh";
			}
			else if(shortPattern.IndexOf("h")!=-1){//or if hour is 1-12
				hFormat+="h";
			}
			else if(shortPattern.IndexOf("HH")!=-1){//or if hour is 00-23
				hFormat+="HH";
			}
			else{//hour is 0-23
				hFormat+="H";
			}
			//hFormat+=
			if(shortPattern.IndexOf("t")!=-1){//if there is an am/pm designator
				hFormat+="tt";
			}
			else{//if no am/pm designator, then use :00
				hFormat+=":00";//time separator will actually change according to region
			}
			string sTime;
			for(int i=0;i<24;i++){
				hour=new DateTime(2000,1,1,i,0,0);//hour is the only important part of this time.
				sTime=hour.ToString(hFormat,ci);
				/*if(i==0){
					sTime="12am";
				}
				else if(i<12){
					sTime=i.ToString()+"am";
				}
				else if(i==12){
					sTime="12pm";
				}
				else{
					sTime=(i-12).ToString()+"pm";
				}*/
				SizeF sizef=g.MeasureString(sTime,bfont);
				g.DrawString(sTime,bfont,new SolidBrush(Color.Black),TimeWidth-sizef.Width-2,i*Lh*RowsPerHr+1);
				g.DrawString(sTime,bfont,new SolidBrush(Color.Black)
					,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*RowsPerHr+1);
				if(MinPerIncr==10){
					g.DrawString(":10",font,new SolidBrush(Color.Black)
						,TimeWidth-19,i*Lh*RowsPerHr+Lh*RowsPerIncr);
					g.DrawString(":20",font,new SolidBrush(Color.Black)
						,TimeWidth-19,i*Lh*RowsPerHr+Lh*RowsPerIncr*2);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,TimeWidth-19,i*Lh*RowsPerHr+Lh*RowsPerIncr*3);
					g.DrawString(":40",font,new SolidBrush(Color.Black)
						,TimeWidth-19,i*Lh*RowsPerHr+Lh*RowsPerIncr*4);
					g.DrawString(":50",font,new SolidBrush(Color.Black)
						,TimeWidth-19,i*Lh*RowsPerHr+Lh*RowsPerIncr*5);
					g.DrawString(":10",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*RowsPerHr+Lh*RowsPerIncr);
					g.DrawString(":20",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*RowsPerHr+Lh*RowsPerIncr*2);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*RowsPerHr+Lh*RowsPerIncr*3);
					g.DrawString(":40",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*RowsPerHr+Lh*RowsPerIncr*4);
					g.DrawString(":50",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*RowsPerHr+Lh*RowsPerIncr*5);
				}
				else{//15
					g.DrawString(":15",font,new SolidBrush(Color.Black)
						,TimeWidth-19,i*Lh*RowsPerHr+Lh*RowsPerIncr);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,TimeWidth-19,i*Lh*RowsPerHr+Lh*RowsPerIncr*2);
					g.DrawString(":45",font,new SolidBrush(Color.Black)
						,TimeWidth-19,i*Lh*RowsPerHr+Lh*RowsPerIncr*3);
					g.DrawString(":15",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*RowsPerHr+Lh*RowsPerIncr);
					g.DrawString(":30",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*RowsPerHr+Lh*RowsPerIncr*2);
					g.DrawString(":45",font,new SolidBrush(Color.Black)
						,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*RowsPerHr+Lh*RowsPerIncr*3);
				}
			}//end for minutes
			g.Dispose();
		}

		///<summary></summary>
		public void DrawShadow(){
			if(Shadow!=null){
				Graphics grfx2=this.CreateGraphics();
				grfx2.DrawImage(Shadow,0,0);
				grfx2.Dispose();
			}
		}

		///<summary>Called from ContrAppt.comboView_SelectedIndexChanged and ContrAppt.RefreshVisops. So, whenever appt Module layout and when comboView is changed.</summary>
		public void ComputeColWidth(int totalWidth){
			if(ApptViewItems.VisOps==null || ApptViewItems.VisProvs==null){
				return;
			}
			try{
				if(RowsPerIncr==0)
					RowsPerIncr=1;
				ColCount=ApptViewItems.VisOps.Length;
					//Defs.Short[(int)DefCat.Operatories].Length;
				ProvCount=ApptViewItems.VisProvs.Length;
					//Providers.List.Length;
				//ColWidth=Convert.ToInt32((totalWidth-TimeWidth*2-ProvWidth*ProvCount)/ColCount);
				ColWidth=(totalWidth-TimeWidth*2-ProvWidth*ProvCount)/ColCount;
				MinPerIncr=Prefs.GetInt("AppointmentTimeIncrement");
				MinPerRow=(float)MinPerIncr/(float)RowsPerIncr;
				RowsPerHr=60/MinPerIncr*RowsPerIncr;
				//if(TwoRowsPerIncrement){
					//MinPerRow=MinPerRow/2;
					//RowsPerHr=RowsPerHr*2;
				//}
				Height=Lh*24*RowsPerHr;
				//if(TwoRowsPerIncrement){
				//	Height=Height*2;
				//}
				Width=TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount;
			}
			catch{
				MessageBox.Show("error computing width");
			}
		}
		


	}
}
