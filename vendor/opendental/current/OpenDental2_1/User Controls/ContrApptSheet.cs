/*=============================================================================================================
FreeDental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
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
using System.Windows.Forms;

namespace OpenDental{

	public class ContrApptSheet : System.Windows.Forms.UserControl{
		private System.ComponentModel.Container components = null;// Required designer variable.
		public static int ColWidth;
		public static int TimeWidth;
		public static int ProvWidth;
		public static int Lh;
		public static int ColCount;
		public static int ProvCount;
		public Bitmap Shadow;
		//public Bitmap HundShadow;//basis for HundredShadows
		//public Bitmap[,] HundredShadows;
		public bool IsScrolling=false;

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
			TimeWidth=30;
			ProvWidth=8;
			Lh=12;
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

		public int DoubleClickToOp(int newX){
			int retVal=(int)Math.Floor((double)(newX-TimeWidth-ProvWidth*ProvCount)/ColWidth);
			if(retVal>ColCount-1) retVal=ColCount-1;
			if(retVal<0) retVal=0;
			return retVal;
		}

		public int DoubleClickToHour (int newY){
			//retVal=
			return System.Convert.ToInt32((newY)/6/Lh);
		}

		public int DoubleClickToMin (int newY){
			int tempReturn = System.Convert.ToInt32(Decimal.Remainder(newY-Lh/2,6*Lh)/Lh)*10;
			if (tempReturn == 60)return 0;
			else return tempReturn;
		}

		public int ConvertToOp (int newX){
			int retVal=0;
			retVal=(int)Math.Round((double)(newX-TimeWidth-ProvWidth*ProvCount)/ColWidth);
			if(retVal > Defs.Short[(int)DefCat.Operatories].Length-1)
				retVal=Defs.Short[(int)DefCat.Operatories].Length-1;
			if(retVal<0) retVal=0;
			return retVal;
		}

		public int ConvertToHour (int newY){
			//if (newY<13*6*Lh-Lh/2){//Before 12:55
			return System.Convert.ToInt32((newY+Lh/2)/6/Lh);
			//}
			//else{//after 12:55
			//	return System.Convert.ToInt32(((newY+Lh/2)/6/Lh)-12);
			//}
		}

		public int ConvertToMin (int newY){
			int tempReturn = System.Convert.ToInt32(Decimal.Remainder(newY,6*Lh)/Lh)*10;
			if (tempReturn == 60)return 0;
			else return tempReturn;
		}

		protected override void OnPaint(PaintEventArgs pea){
			DrawShadow();
			//revisit this later, using a separate thread:
			/*
			if(HundShadow==null)return;
			if(IsScrolling){
				pea.Graphics.DrawImage(Shadow,pea.ClipRectangle.X,pea.ClipRectangle.Y,pea.ClipRectangle,GraphicsUnit.Pixel);
				return;
			}
			base.OnPaint(pea);
			int top;//row
			int bottom;
			int left;
			int right;
			top=(int)(10*(pea.ClipRectangle.Location.Y-(-Location.Y))/(double)HundShadow.Height);
			bottom=(int)(10*(pea.ClipRectangle.Location.Y+pea.ClipRectangle.Height-(-Location.Y))/(double)HundShadow.Height);
			left=(int)(10*pea.ClipRectangle.Left/(double)HundShadow.Width);
			right=(int)(10*pea.ClipRectangle.Right/(double)HundShadow.Width);
			if(top>9) top=9;
			if(bottom>9) bottom=9;
			if(left>9) left=9;
			if(right>9) right=9;
			for(int x=left;x <= right;x++){
				for(int y=top;y <= bottom;y++){
					try{
						pea.Graphics.DrawImage(HundredShadows[x,y],x*HundShadow.Width/10,-Location.Y+y*HundShadow.Height/10);
					}
					catch{}//to prevent crash when minimizing application
				}
			}*/
		}

		public void CreateShadow(){
			if(Shadow!=null){
				Shadow=null;
			}
			Shadow=new Bitmap(Width,Height);       
			//MessageBox.Show(StartTime.ToString());
			Graphics grfx=Graphics.FromImage(Shadow);
			int totalHeight=(int)(24*Lh*6);
			//Graphics grfx = pea.Graphics;
			//background
			grfx.FillRectangle(new SolidBrush(Color.LightGray),0,0,TimeWidth,totalHeight);//L time bar
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
			grfx.FillRectangle(closedBrush,TimeWidth,0,ColWidth*ColCount+ProvWidth*ProvCount,totalHeight);//main background
      if(Schedules.DayList!=null && Schedules.DayList.Length > 0){
				for(int i=0;i<Schedules.DayList.Length;i++){	
					if(Schedules.DayList[i].Status==SchedStatus.Holiday){
 						grfx.FillRectangle(holidayBrush,TimeWidth,0,ColWidth*ColCount+ProvWidth*ProvCount,totalHeight);
					} 
          else{        
 						grfx.FillRectangle(openBrush
							,TimeWidth
							,Schedules.DayList[i].StartTime.Hour*Lh*6
							+(int)Schedules.DayList[i].StartTime.Minute*Lh/10
							,ColWidth*ColCount+ProvWidth*ProvCount
							,(Schedules.DayList[i].StopTime-Schedules.DayList[i].StartTime).Hours*Lh*6
							+(Schedules.DayList[i].StopTime-Schedules.DayList[i].StartTime).Minutes*Lh/10);
          }
				}         
      }
      else{//default sched
			  for(int i=0;i<SchedDefaults.List.Length;i++){
					if(SchedDefaults.List[i].DayOfWeek==(int)Appointments.DateSelected.DayOfWeek){
						grfx.FillRectangle(openBrush
							,TimeWidth
							,SchedDefaults.List[i].StartTime.Hour*Lh*6
							+SchedDefaults.List[i].StartTime.Minute*Lh/10
							,ColWidth*ColCount+ProvWidth*ProvCount
							,(SchedDefaults.List[i].StopTime-SchedDefaults.List[i].StartTime).Hours*Lh*6
							+(SchedDefaults.List[i].StopTime-SchedDefaults.List[i].StartTime).Minutes*Lh/10);
					}
				}
      }
			grfx.FillRectangle(new SolidBrush(Color.LightGray),TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,0,TimeWidth,totalHeight);//R time bar
			//vert
			grfx.DrawLine(new Pen(Color.DarkGray), 0,0,0,totalHeight);
			grfx.DrawLine(new Pen(Color.White), TimeWidth-2,0,TimeWidth-2,totalHeight);
			grfx.DrawLine(new Pen(Color.DarkGray), TimeWidth-1,0,TimeWidth-1,totalHeight);
			for(int i=0;i<ProvCount;i++){
				grfx.DrawLine(new Pen(Color.DarkGray),TimeWidth+ProvWidth*i,0,TimeWidth+ProvWidth*i,totalHeight);
			}
			for(int i=0;i<ColCount;i++){
				grfx.DrawLine(new Pen(Color.DarkGray),TimeWidth+ProvWidth*ProvCount+ColWidth*i,0
					,TimeWidth+ProvWidth*ProvCount+ColWidth*i,totalHeight);
			}
			grfx.DrawLine(new Pen(Color.DarkGray), TimeWidth+ProvWidth*ProvCount+ColWidth*ColCount,0
				,TimeWidth+ProvWidth*ProvCount+ColWidth*ColCount,totalHeight);
			grfx.DrawLine(new Pen(Color.DarkGray),TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount,0
				,TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount,totalHeight);
			//horiz gray
			for(int i=0;i<(totalHeight);i+=Lh){
				grfx.DrawLine(new Pen(Color.LightGray),TimeWidth,i
					,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i);
			}
			//horiz Hour lines
			for(int i=0;i<totalHeight;i+=Lh*6){
				grfx.DrawLine(new Pen(Color.White),0,i-1
					,TimeWidth*2+ColWidth*ColCount+ProvWidth*ProvCount,i-1);
				grfx.DrawLine(new Pen(Color.DarkSlateGray),0,i,TimeWidth,i);
				grfx.DrawLine(new Pen(Color.Black),TimeWidth,i
					,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i);
				grfx.DrawLine(new Pen(Color.DarkSlateGray),TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i
					,TimeWidth*2+ColWidth*ColCount+ProvWidth*ProvCount,i);
			}
			//time bar
			for(int j=0;j<ContrApptSingle.ProvBar.Length;j++){
				for (int i=0;i<144;i++){//ContrApptSingle.TimeBar.Length;i++){
					switch(ContrApptSingle.ProvBar[j][i]){
						case 0:
							break;
						case 1:
							try{
								grfx.FillRectangle(new SolidBrush(Providers.List[j].ProvColor)
									,TimeWidth+ProvWidth*j+1,(i*Lh)+1,ProvWidth-1,Lh-1);
							}
							catch{//design-time
								grfx.FillRectangle(new SolidBrush(Color.White)
									,TimeWidth+ProvWidth*j+1,(i*Lh)+1,ProvWidth-1,Lh-1);
							}
							break;
						case 2:
							grfx.FillRectangle(new HatchBrush(HatchStyle.DarkUpwardDiagonal
								,Color.Black,Providers.List[j].ProvColor)
								,TimeWidth+ProvWidth*j+1,(i*Lh)+1,ProvWidth-1,Lh-1);
							break;
						default://more than 2
							grfx.FillRectangle(new SolidBrush(Color.Black)
								,TimeWidth+ProvWidth*j+1,(i*Lh)+1,ProvWidth-1,Lh-1);
							break;
					}
				}
			}//end for ProvBar
			//time indicator
			int curTimeY=(int)(DateTime.Now.Hour*Lh*6+DateTime.Now.Minute/60f*(float)Lh*6);
			grfx.DrawLine(new Pen(Color.Red),0,curTimeY
				,TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount,curTimeY);
			grfx.DrawLine(new Pen(Color.Red),0,curTimeY+1
				,TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount,curTimeY+1);
			//minutes
			Font font = new Font("Microsoft Sans Serif", 8);
			Font bfont = new Font("Arial", 8,FontStyle.Bold);
			grfx.TextRenderingHint=TextRenderingHint.SingleBitPerPixelGridFit;//to make printing clearer
			for (int i=0; i<24; i+=1){
				string sTime;
				if(i==0){
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
				}
				SizeF sizef = grfx.MeasureString(sTime,bfont);
				grfx.DrawString(sTime,bfont,new SolidBrush(Color.Black),TimeWidth-sizef.Width-1,i*Lh*6-1);
				grfx.DrawString(":10",font,new SolidBrush(Color.Black),TimeWidth-19,i*Lh*6+Lh-1);
				grfx.DrawString(":20",font,new SolidBrush(Color.Black),TimeWidth-19,i*Lh*6+Lh*2-1);
				grfx.DrawString(":30",font,new SolidBrush(Color.Black),TimeWidth-19,i*Lh*6+Lh*3-1);
				grfx.DrawString(":40",font,new SolidBrush(Color.Black),TimeWidth-19,i*Lh*6+Lh*4-1);
				grfx.DrawString(":50",font,new SolidBrush(Color.Black),TimeWidth-19,i*Lh*6+Lh*5-1);

				grfx.DrawString(sTime,bfont,new SolidBrush(Color.Black)
					,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*6-1);
				grfx.DrawString(":10",font,new SolidBrush(Color.Black)
					,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*6+Lh-1);
				grfx.DrawString(":20",font,new SolidBrush(Color.Black)
					,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*6+Lh*2-1);
				grfx.DrawString(":30",font,new SolidBrush(Color.Black)
					,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*6+Lh*3-1);
				grfx.DrawString(":40",font,new SolidBrush(Color.Black)
					,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*6+Lh*4-1);
				grfx.DrawString(":50",font,new SolidBrush(Color.Black)
					,TimeWidth+ColWidth*ColCount+ProvWidth*ProvCount,i*Lh*6+Lh*5-1);
			}//end for minutes
			grfx.Dispose();
		}

		public void DrawShadow(){
			if(Shadow!=null){
				Graphics grfx2=this.CreateGraphics();
				grfx2.DrawImage(Shadow,0,0);
				grfx2.Dispose();
			}
		}

		public void ComputeColWidth(int totalWidth){
			try{
				ColCount=Defs.Short[(int)DefCat.Operatories].Length;
				ProvCount=Providers.List.Length;
				ColWidth=Convert.ToInt32((totalWidth-TimeWidth*2-ProvWidth*ProvCount)/ColCount);
				Height=Lh*24*6;
				Width=TimeWidth*2+ProvWidth*ProvCount+ColWidth*ColCount;
				//MessageBox.Show("width computed");
			}
			catch{
				//MessageBox.Show("error computing width");
			}
		}
		



	}
}
