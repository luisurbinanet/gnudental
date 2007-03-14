using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	public class ContrSchedGrid : System.Windows.Forms.UserControl{
		private System.ComponentModel.Container components = null;// Required designer variable.
		public static int RowH;
		public static int ColW;
		public static int NumW;
		public SchedDefault[] ArrayBlocks;

		public ContrSchedGrid(){
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
			RowH=4;
			ColW=100;
			NumW=26;
			ArrayBlocks=new SchedDefault[0];
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
			// ContrSchedGrid
			// 
			this.BackColor = System.Drawing.Color.Silver;
			this.Name = "ContrSchedGrid";
			this.Size = new System.Drawing.Size(472, 342);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ContrSchedGrid_Paint);

		}
		#endregion

		private void ContrSchedGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e) {
				SolidBrush blockBrush;
			try{
				blockBrush=new SolidBrush(Defs.Long[(int)DefCat.AppointmentColors][0].ItemColor);
			}
			catch{
				blockBrush=new SolidBrush(Color.White);
			}
			for(int i=0;i<ArrayBlocks.Length;i++){
				e.Graphics.FillRectangle(blockBrush
					,NumW+ArrayBlocks[i].DayOfWeek*ColW
					,ArrayBlocks[i].StartTime.Hour*6*RowH + (int)ArrayBlocks[i].StartTime.Minute/10*RowH
					,ColW
					,((ArrayBlocks[i].StopTime-ArrayBlocks[i].StartTime).Hours*6+(ArrayBlocks[i].StopTime-ArrayBlocks[i].StartTime).Minutes/10)*RowH);
			}
			
			Pen bPen=new Pen(Color.Black);
			Pen gPen=new Pen(Color.LightGray);
			for(int y=0;y<24*6;y++){
				e.Graphics.DrawLine(gPen,NumW,y*RowH,NumW+ColW*7,y*RowH);
			}
			for(int y=0;y<25;y++){
				e.Graphics.DrawLine(bPen,NumW,y*RowH*6,NumW+ColW*7,y*RowH*6);
			}
			for(int x=0;x<8;x++){
				e.Graphics.DrawLine(bPen,NumW+x*ColW,0,NumW+x*ColW,RowH*6*24);
			}
			e.Graphics.DrawString(Lan.g(this,"12am"),new Font("Small Font",7f),new SolidBrush(Color.Black),0,-2);
			e.Graphics.DrawString(Lan.g(this,"12am"),new Font("Small Font",7f),new SolidBrush(Color.Black),NumW+ColW*7,-2);
			for(int y=1;y<12;y++){
				e.Graphics.DrawString(Lan.g(this,y.ToString()+"am"),new Font("Small Font",7f),new SolidBrush(Color.Black),0,y*RowH*6-3);
				e.Graphics.DrawString(Lan.g(this,y.ToString()+"am"),new Font("Small Font",7f),new SolidBrush(Color.Black),NumW+ColW*7,y*RowH*6-3);
			}
			e.Graphics.DrawString(Lan.g(this,"12pm"),new Font("Small Font",7f),new SolidBrush(Color.Black),0,12*RowH*6-3);
			e.Graphics.DrawString(Lan.g(this,"12pm"),new Font("Small Font",7f),new SolidBrush(Color.Black),NumW+ColW*7,12*RowH*6-3);
			for(int y=1;y<12;y++){
				e.Graphics.DrawString(Lan.g(this,y.ToString()+"pm"),new Font("Small Font",7f),new SolidBrush(Color.Black),0,(12+y)*RowH*6-3);
				e.Graphics.DrawString(Lan.g(this,y.ToString()+"pm"),new Font("Small Font",7f),new SolidBrush(Color.Black),NumW+ColW*7,(12+y)*RowH*6-3);
			}
			Width=NumW*2+ColW*7;
			Height=RowH*24*6+1;
		}
	}

	public struct TimeBlock{
		public int Col;
		public DateTime TStart;
		public DateTime TStop;
	}
}
