using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormRpStatement : System.Windows.Forms.Form{
		private System.Windows.Forms.PrintPreviewControl printPreviewControl2;
		private System.Windows.Forms.Button butPrint;
		private System.Windows.Forms.Button butClose;
		private System.Windows.Forms.Panel panelZoom;
		private System.Windows.Forms.Button butFwd;
		private System.Windows.Forms.Button butBack;
		private System.Windows.Forms.Label labelTotPages;
		private System.Windows.Forms.Button butZoomIn;
		private System.Windows.Forms.Button butFullPage;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.PrintDialog printDialog2;
		private System.Drawing.Printing.PrintDocument pd2;
		private int totalPages=0;
		private Font bodyFont=new Font("Arial",9);
		private Font NameFont=new Font("Arial",10,FontStyle.Bold);
		private Font TotalFont=new Font("Arial",9,FontStyle.Bold);
		private Font GTotalFont=new Font("Arial",9,FontStyle.Bold);
		private int linesPrinted;
		private int pagesPrinted;
    private string curPatName="";
		private int linesCanPrint=1;
		bool middleOfPatient=false;
		bool middleOfPatientNewPage=false;
		private int tempCount=0;
		float colHeight=0;
		private int patLines=0;

		///<summary></summary>
		public FormRpStatement(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				labelTotPages,
				butZoomIn,
				butFullPage,
				butFwd,
				butBack,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butClose,
				butPrint,
			});
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing )	{
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormRpStatement));
			this.printPreviewControl2 = new System.Windows.Forms.PrintPreviewControl();
			this.butPrint = new System.Windows.Forms.Button();
			this.butClose = new System.Windows.Forms.Button();
			this.panelZoom = new System.Windows.Forms.Panel();
			this.butFwd = new System.Windows.Forms.Button();
			this.butBack = new System.Windows.Forms.Button();
			this.labelTotPages = new System.Windows.Forms.Label();
			this.butZoomIn = new System.Windows.Forms.Button();
			this.butFullPage = new System.Windows.Forms.Button();
			this.printDialog2 = new System.Windows.Forms.PrintDialog();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.panelZoom.SuspendLayout();
			this.SuspendLayout();
			// 
			// printPreviewControl2
			// 
			this.printPreviewControl2.AutoZoom = false;
			this.printPreviewControl2.Location = new System.Drawing.Point(-116, -7);
			this.printPreviewControl2.Name = "printPreviewControl2";
			this.printPreviewControl2.Size = new System.Drawing.Size(842, 538);
			this.printPreviewControl2.TabIndex = 6;
			this.printPreviewControl2.Zoom = 1;
			// 
			// butPrint
			// 
			this.butPrint.Image = ((System.Drawing.Bitmap)(resources.GetObject("butPrint.Image")));
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(340, 5);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(75, 27);
			this.butPrint.TabIndex = 8;
			this.butPrint.Text = "          Print";
			this.butPrint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// butClose
			// 
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(434, 5);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 27);
			this.butClose.TabIndex = 7;
			this.butClose.Text = "Close";
			// 
			// panelZoom
			// 
			this.panelZoom.Controls.AddRange(new System.Windows.Forms.Control[] {
																																						this.butFwd,
																																						this.butBack,
																																						this.labelTotPages,
																																						this.butZoomIn,
																																						this.butFullPage,
																																						this.butClose,
																																						this.butPrint});
			this.panelZoom.Location = new System.Drawing.Point(35, 419);
			this.panelZoom.Name = "panelZoom";
			this.panelZoom.Size = new System.Drawing.Size(524, 37);
			this.panelZoom.TabIndex = 12;
			// 
			// butFwd
			// 
			this.butFwd.Image = ((System.Drawing.Bitmap)(resources.GetObject("butFwd.Image")));
			this.butFwd.Location = new System.Drawing.Point(195, 7);
			this.butFwd.Name = "butFwd";
			this.butFwd.Size = new System.Drawing.Size(18, 22);
			this.butFwd.TabIndex = 13;
			this.butFwd.Click += new System.EventHandler(this.butFwd_Click);
			// 
			// butBack
			// 
			this.butBack.Image = ((System.Drawing.Bitmap)(resources.GetObject("butBack.Image")));
			this.butBack.Location = new System.Drawing.Point(123, 7);
			this.butBack.Name = "butBack";
			this.butBack.Size = new System.Drawing.Size(18, 22);
			this.butBack.TabIndex = 12;
			this.butBack.Click += new System.EventHandler(this.butBack_Click);
			// 
			// labelTotPages
			// 
			this.labelTotPages.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelTotPages.Location = new System.Drawing.Point(143, 9);
			this.labelTotPages.Name = "labelTotPages";
			this.labelTotPages.Size = new System.Drawing.Size(47, 18);
			this.labelTotPages.TabIndex = 11;
			this.labelTotPages.Text = "1 / 2";
			this.labelTotPages.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// butZoomIn
			// 
			this.butZoomIn.Image = ((System.Drawing.Bitmap)(resources.GetObject("butZoomIn.Image")));
			this.butZoomIn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butZoomIn.Location = new System.Drawing.Point(9, 5);
			this.butZoomIn.Name = "butZoomIn";
			this.butZoomIn.Size = new System.Drawing.Size(75, 27);
			this.butZoomIn.TabIndex = 10;
			this.butZoomIn.Text = "       Zoom In";
			this.butZoomIn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butZoomIn.Click += new System.EventHandler(this.butZoomIn_Click);
			// 
			// butFullPage
			// 
			this.butFullPage.Location = new System.Drawing.Point(9, 5);
			this.butFullPage.Name = "butFullPage";
			this.butFullPage.Size = new System.Drawing.Size(75, 27);
			this.butFullPage.TabIndex = 9;
			this.butFullPage.Text = "Full Page";
			this.butFullPage.Visible = false;
			this.butFullPage.Click += new System.EventHandler(this.butFullPage_Click);
			// 
			// FormRpStatement
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(787, 610);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																																	this.panelZoom,
																																	this.printPreviewControl2});
			this.Name = "FormRpStatement";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Statement";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormRpStatement_Load);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.FormRpStatement_Layout);
			this.panelZoom.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		
		private void FormRpStatement_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
			printPreviewControl2.Location=new Point(0,0);
			printPreviewControl2.Height=ClientSize.Height-39;
			printPreviewControl2.Width=ClientSize.Width;	
			panelZoom.Location=new Point(ClientSize.Width-620,ClientSize.Height-38);
		}

		private void FormRpStatement_Load(object sender, System.EventArgs e) {
			if(PrinterSettings.InstalledPrinters.Count==0){
				MessageBox.Show(Lan.g(this,"No printer installed"));
				DialogResult=DialogResult.OK;
			}
			else{
				PrintReport(false);
				labelTotPages.Text=Lan.g(this,"/ "+totalPages.ToString());
				printPreviewControl2.Zoom=((double)printPreviewControl2.ClientSize.Height
					/(double)pd2.DefaultPageSettings.PaperSize.Height);
				labelTotPages.Text=Lan.g(this,"/ "+totalPages.ToString());
			}
		}

		///<summary></summary>
		public void PrintReport(bool justPreview){
			pd2=new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			pd2.DefaultPageSettings.Margins=new Margins(10,40,40,60);
			//pagesPrinted=0;
			//linesPrinted=0;
			PrintDocument tempPD = new PrintDocument();
			tempPD.PrinterSettings.PrinterName=Computers.Cur.PrinterName;
			if(tempPD.PrinterSettings.IsValid){
				pd2.PrinterSettings.PrinterName=Computers.Cur.PrinterName;
			}
			//uses default printer if selected printer not valid
			tempPD.Dispose();
			//try{
				if(justPreview){
					printPreviewControl2.Document=pd2;
				}
				else{
					//printDialog2=new PrintDialog();
					//printDialog2.Document=pd2;
					//if(printDialog2.ShowDialog()==DialogResult.OK){
						linesPrinted=0;
						pagesPrinted=0;
						pd2.Print();
					DialogResult=DialogResult.OK;
					//}
				}
			//}
			//catch{
			//	MessageBox.Show(Lan.g(this,"Printer not available"));
			//}
		}
		private void pd2_PrintPage(object sender, PrintPageEventArgs ev){//raised for each page to be printed
			//This printout logic is just a mess and needs to be entirely rewritten properly some day.
			//should look more like the treatment plan.
		  float yPos = 30;
			float labelX = 30;
			float yTbPos=0;
			float xTbPos=0;
			float wTbPos=0;
			float hTbPos=0;
      tempCount=0;//counts rows printed on this page
			if(pagesPrinted==0){
#region Page 1 header
				//Print HEADING
 				string heading = "STATEMENT";
				Font headingFont=new Font("Arial",14,FontStyle.Bold);
				float xHeading = (float)(425-(ev.Graphics.MeasureString(heading,headingFont).Width/2));
				ev.Graphics.DrawString(heading,headingFont,Brushes.Black,xHeading,yPos);
				//Print Date
 				string date = DateTime.Today.ToShortDateString();
				Font dateFont=new Font("Arial",9,FontStyle.Regular);
				float xDate = (float)(425-(ev.Graphics.MeasureString(date,dateFont).Width/2));
				ev.Graphics.DrawString(date,dateFont,Brushes.Black,xDate,yPos+20);
				//if first page, print Practice Address
				Font pFont=new Font("Arial",10);
				yPos=50;
				string pTitle=((Pref)Prefs.HList[Lan.g("Pref","PracticeTitle")]).ValueString;
				string pAddress=((Pref)Prefs.HList[Lan.g("Pref","PracticeAddress")]).ValueString;
				string pAddress2=((Pref)Prefs.HList[Lan.g("Pref","PracticeAddress2")]).ValueString;
				string pCity=((Pref)Prefs.HList[Lan.g("Pref","PracticeCity")]).ValueString;
				string pState=((Pref)Prefs.HList[Lan.g("Pref","PracticeST")]).ValueString;
				string pZip=((Pref)Prefs.HList[Lan.g("Pref","PracticeZip")]).ValueString;
				string CityStateZip=pCity+", "+pState+" "+pZip;
				string pPhone=((Pref)Prefs.HList[Lan.g("Pref","PracticePhone")]).ValueString;
				if(pPhone.Length==10)
					pPhone= "(" + pPhone.Substring(0,3)+")"+pPhone.Substring(3,3)+"-"+pPhone.Substring(6);
				ev.Graphics.DrawString(pTitle,pFont,Brushes.Black,labelX,yPos);
				yPos+=18;
				ev.Graphics.DrawString(pAddress,pFont,Brushes.Black,labelX,yPos);	    
				yPos+=18;
				if(pAddress2!=""){
					ev.Graphics.DrawString(pAddress2,pFont,Brushes.Black,labelX,yPos);	    
					yPos+=18;
				}
				ev.Graphics.DrawString(CityStateZip,pFont,Brushes.Black,labelX,yPos);
				yPos+=18;
				ev.Graphics.DrawString(pPhone,pFont,Brushes.Black,labelX,yPos);
				yPos+=18;
				// Print Grand Total Headings and Lines 
				float yTotal=(float)100;
				float xTotal=(float)275;
				float wTotal=300;
				float hTotal=(float)12.5;
				ev.Graphics.FillRectangle(Brushes.LightGray,xTotal,yTotal,wTotal,hTotal);
				ev.Graphics.DrawRectangle(new Pen(Color.Black),xTotal,yTotal,wTotal,hTotal);  
					for(int i=1;i<4;i++) 
							ev.Graphics.DrawLine(new Pen(Color.Black),xTotal+(i*75),yTotal,xTotal+(i*75),yTotal+hTotal); 
				string head1="";
				string head2="Total";
				string head3="- Ins Est";
				string head4="= Amt Due";
				Font tHeadFont=new Font("Arial",8,FontStyle.Bold);
				float xHead1 = (float)(xTotal+75/2-(ev.Graphics.MeasureString(head1,tHeadFont).Width/2));
				float xHead2 = (float)(xTotal+3*75/2-(ev.Graphics.MeasureString(head2,tHeadFont).Width/2));
				float xHead3 = (float)(xTotal+5*75/2-(ev.Graphics.MeasureString(head3,tHeadFont).Width/2));
				float xHead4 = (float)(xTotal+7*75/2-(ev.Graphics.MeasureString(head4,tHeadFont).Width/2));
				ev.Graphics.DrawString(head1,tHeadFont,Brushes.Black,xHead1,yTotal); 
				ev.Graphics.DrawString(head2,tHeadFont,Brushes.Black,xHead2,yTotal); 			
				ev.Graphics.DrawString(head3,tHeadFont,Brushes.Black,xHead3,yTotal); 
				ev.Graphics.DrawString(head4,tHeadFont,Brushes.Black,xHead4,yTotal); 
				//Print Grand Total Data and Lines
				for(int i=0;i<5;i++) {
	  			ev.Graphics.DrawLine(new Pen(Color.Gray),xTotal+(i*75),(float)(yTotal+hTotal+.5),
						xTotal+(i*75),(float)(yTotal+hTotal+.5+25));
				} 
				ev.Graphics.DrawLine(new Pen(Color.Gray),xTotal,yTotal+hTotal+25,xTotal+wTotal,
					yTotal+hTotal+25);
				//only prints totals if a GrandTotal line is present at the end of the array
				if(ContrAccount.StatementA[11,ContrAccount.StatementA.GetLength(1)-1]=="GrandTotal"){
					string tot1=ContrAccount.StatementA[4,ContrAccount.StatementA.GetLength(1)-1];
					string tot2=ContrAccount.StatementA[5,ContrAccount.StatementA.GetLength(1)-1];
					string tot3=ContrAccount.StatementA[6,ContrAccount.StatementA.GetLength(1)-1];
					string tot4=ContrAccount.StatementA[7,ContrAccount.StatementA.GetLength(1)-1];
					Font tTotalFont=new Font("Arial",8,FontStyle.Regular);
					float xTotal1=(float)(xTotal+75/2-(ev.Graphics.MeasureString(tot1,bodyFont).Width/2));
					float xTotal2=(float)(xTotal+3*75/2-(ev.Graphics.MeasureString(tot2,bodyFont).Width/2));
					float xTotal3=(float)(xTotal+5*75/2-(ev.Graphics.MeasureString(tot3,bodyFont).Width/2));
					float xTotal4=(float)(xTotal+7*75/2-(ev.Graphics.MeasureString(tot4,bodyFont).Width/2));
					ev.Graphics.DrawString(tot1,bodyFont,Brushes.Black,xTotal1,yTotal+hTotal+5); 
					ev.Graphics.DrawString(tot2,bodyFont,Brushes.Black,xTotal2,yTotal+hTotal+5); 			
					ev.Graphics.DrawString(tot3,bodyFont,Brushes.Black,xTotal3,yTotal+hTotal+5); 
					ev.Graphics.DrawString(tot4,bodyFont,Brushes.Black,xTotal4,yTotal+hTotal+5); 
				}				
				//Prints Credit Card Info
				if(((Pref)Prefs.HList[Lan.g("Pref","StatementShowCreditCard")]).ValueString=="1"){
					float xCredit=(float)450;
					float yCredit=(float)175;
					float space=30;
					string type= "CREDIT CARD TYPE";
					string amt=  "#";
					string exp= "EXPIRES";
					string approv="AMOUNT APPROVED";
					string name=  "NAME";
					string dis="(As it appears on card)";          
					string sig="SIGNATURE";
					Font creditFont=new Font("Arial",7,FontStyle.Bold);
					Font disFont=new Font("Arial",5,FontStyle.Regular);
					float xDis=(float)(625.5-(ev.Graphics.MeasureString(dis,disFont).Width/2)+5);
					ev.Graphics.DrawString(type,creditFont,Brushes.Black,xCredit,yCredit);
					ev.Graphics.DrawString(amt,creditFont,Brushes.Black,xCredit,yCredit+space); 
					ev.Graphics.DrawString(exp,creditFont,Brushes.Black,xCredit,yCredit+space*2);	
 					ev.Graphics.DrawString(approv,creditFont,Brushes.Black,xCredit,yCredit+space*3);	     
					ev.Graphics.DrawString(name,creditFont,Brushes.Black,xCredit,yCredit+(space*4));
					ev.Graphics.DrawString(dis,disFont,Brushes.Black,xDis,yCredit+space*4+13);
					ev.Graphics.DrawString(sig,creditFont,Brushes.Black,xCredit,yCredit+(space*5)); 
  				ev.Graphics.DrawLine(new Pen(Color.Black),xCredit+(ev.Graphics.MeasureString(type,creditFont)).Width,
						yCredit+creditFont.GetHeight(ev.Graphics),776,yCredit+creditFont.GetHeight(ev.Graphics)); 
  				ev.Graphics.DrawLine(new Pen(Color.Black),xCredit+(ev.Graphics.MeasureString(amt,creditFont)).Width,
						yCredit+creditFont.GetHeight(ev.Graphics)+space,776,yCredit+creditFont.GetHeight(ev.Graphics)
						+space); 
  				ev.Graphics.DrawLine(new Pen(Color.Black),xCredit+(ev.Graphics.MeasureString(exp,creditFont)).Width,
						yCredit+creditFont.GetHeight(ev.Graphics)+(space*2),776,yCredit+creditFont.GetHeight(ev.Graphics)
						+(space*2)); 
  				ev.Graphics.DrawLine(new Pen(Color.Black),xCredit+(ev.Graphics.MeasureString(approv,creditFont)).Width,
						yCredit+creditFont.GetHeight(ev.Graphics)+(space*3),776,yCredit+creditFont.GetHeight(ev.Graphics)
						+(space*3)); 
  				ev.Graphics.DrawLine(new Pen(Color.Black),xCredit+(ev.Graphics.MeasureString(name,creditFont)).Width,
						yCredit+creditFont.GetHeight(ev.Graphics)+(space*4),776,yCredit+creditFont.GetHeight(ev.Graphics)
						+(space*4)); 
					ev.Graphics.DrawLine(new Pen(Color.Black),xCredit+(ev.Graphics.MeasureString(sig,creditFont)).Width,
						yCredit+creditFont.GetHeight(ev.Graphics)+(space*5),776,yCredit+creditFont.GetHeight(ev.Graphics)
						+(space*5)); 
				}
	//Prints AMOUNT ENCLOSED
				float yEncl=(float)100;
				float xEncl=(float)651;
				float wEncl=125;
				float hEncl=(float)12.5;
				ev.Graphics.FillRectangle(Brushes.LightGray,xEncl,yEncl,wEncl,hEncl);
				ev.Graphics.DrawRectangle(new Pen(Color.Black),xEncl,yEncl,wEncl,hEncl);  
	      
				string amtEncl="Amount Enclosed";
				Font enclFont=new Font("Arial",8,FontStyle.Bold);
				float xamtEncl = (float)(713.5-(ev.Graphics.MeasureString(amtEncl,enclFont).Width/2));
				ev.Graphics.DrawString(amtEncl,enclFont,Brushes.Black,xamtEncl,yEncl); 

	  		ev.Graphics.DrawLine(new Pen(Color.Gray),xEncl,(float)(yEncl+hEncl+.5),
					xEncl,(float)(yEncl+hEncl+.5+25));
 	  		ev.Graphics.DrawLine(new Pen(Color.Gray),xEncl+125,(float)(yEncl+hEncl+.5),
					xEncl+125,(float)(yEncl+hEncl+.5+25));      
				ev.Graphics.DrawLine(new Pen(Color.Gray),xEncl,yEncl+hEncl+25,xEncl+wEncl,
					yEncl+hEncl+25);

				string payNow="";//"(Payment Due upon Receipt)";
				Font payNowFont=new Font("Arial",5,FontStyle.Regular);
				float xPayNow=(float)(713.5-(ev.Graphics.MeasureString(payNow,payNowFont).Width/2)+5);
				ev.Graphics.DrawString(payNow,payNowFont,Brushes.Black,xPayNow,yEncl+hEncl+30); 
	// Rectangle size of window to put in address
				//float x2=(float)(62.5);
				//float y2=(float)((225)); //+10
				//float w2=(float)(350);
				//float h2=(float)((75));
				//ev.Graphics.DrawRectangle(new Pen(Color.Black),x2,y2,w2,h2);
	//Prints Grayish Rectangle to block off 
				//float x=25;//(float)(-12.5);  // 0 = 1/4 inch
				//float	y=(float)((187.5));//+10
				//float w=(float)(437.5);
				//float h=(float)((150));
				//ev.Graphics.DrawRectangle(new Pen(Color.Gray),x,y,w,h);
	//Prints Patient's Billing Address	
				Font addrFont=new Font("Arial",11,FontStyle.Regular);
				yPos=225 + 1;//+10
				labelX=(float)(62.5+12.5);
				Patients.Cur=Patients.FamilyList[Patients.GuarIndex];
				string pName=Patients.Cur.FName +" "+Patients.Cur.MiddleI+" "+Patients.Cur.LName;
				pAddress=Patients.Cur.Address;
				pCity=Patients.Cur.City;
				pState=Patients.Cur.State;
				pZip=Patients.Cur.Zip;
				CityStateZip=pCity+", "+pState+" "+pZip;
				ev.Graphics.DrawString(pName,addrFont,Brushes.Black,labelX,yPos);
				yPos+=19;
				ev.Graphics.DrawString(pAddress,addrFont,Brushes.Black,labelX,yPos);	    
				yPos+=19;
				if (Patients.Cur.Address2!=""){
					ev.Graphics.DrawString(Patients.Cur.Address2,addrFont,Brushes.Black,labelX,yPos);	    
					yPos+=19;  
				}
   			ev.Graphics.DrawString(CityStateZip,addrFont,Brushes.Black,labelX,yPos);				
	//Draw perforated line
				Pen pen2 = new Pen(Brushes.Black);
				pen2.Width=(float).125;
				pen2.DashStyle=DashStyle.Dot;
				yPos=350;//3.62 inches from top
				ev.Graphics.DrawLine(pen2,0,yPos,ev.PageBounds.Width,yPos);
	//Prints Please Detach Statement
 				yPos=350;  //  1/3 page down plus one line
 				string detach = "PLEASE DETACH AND RETURN THE UPPER PORTION WITH YOUR PAYMENT";
				Font DetachFont=new Font("Arial",6,FontStyle.Bold);
				float xDetach = (float)(425-(ev.Graphics.MeasureString(detach,DetachFont).Width/2));
				ev.Graphics.DrawString(detach,DetachFont,Brushes.Gray,xDetach,yPos+5);
				yPos+=15;
#endregion			
				//yPos=770;  change this value to test multiple pages
			} 
			else {
				yPos=(float)18.75;
			}
//  Start of Patient Procedures etc Table
			int[] colPos=new int[12];
			HorizontalAlignment[] colAlign=new HorizontalAlignment[11];
			string[] ColCaption=new string[11];
			ColCaption[0]=Lan.g(this,"Date");
      ColCaption[1]=Lan.g(this,"Code");
      ColCaption[2]=Lan.g(this,"Tooth");
      ColCaption[3]=Lan.g(this,"Description");
			ColCaption[4]=Lan.g(this,"Fee");
      ColCaption[5]=Lan.g(this,"Ins Est");
			ColCaption[6]=Lan.g(this,"Ins Pay");
      ColCaption[7]=Lan.g(this,"Pat Pay");
			ColCaption[8]=Lan.g(this,"Adj");
			ColCaption[9]=Lan.g(this,"Paid");
			ColCaption[10]=Lan.g(this,"Balance");
			colPos[0]=30;   colAlign[0]=HorizontalAlignment.Left;//date
			colPos[1]=103;  colAlign[1]=HorizontalAlignment.Left;//code
			colPos[2]=148;  colAlign[2]=HorizontalAlignment.Left;//tooth
			colPos[3]=190;  colAlign[3]=HorizontalAlignment.Left;//description
			colPos[4]=425;  colAlign[4]=HorizontalAlignment.Right;//fee
			colPos[5]=475;  colAlign[5]=HorizontalAlignment.Right;//insest
			colPos[6]=525;  colAlign[6]=HorizontalAlignment.Right;//inspay
			colPos[7]=575;  colAlign[7]=HorizontalAlignment.Right;//patient
			colPos[8]=625;  colAlign[8]=HorizontalAlignment.Right;//adj
			colPos[9]=675;  colAlign[9]=HorizontalAlignment.Right;//paid
			colPos[10]=725;  colAlign[10]=HorizontalAlignment.Right;//balance
			colPos[11]=780;//+1  //col 11 is for formatting codes
			while(yPos<ev.MarginBounds.Top+ev.MarginBounds.Height
				&& linesPrinted<ContrAccount.StatementA.GetLength(1)
				&& linesPrinted<300){//failsafe until I can rewrite this entire form
        patLines=0;//resets for new page
				//Figures out if patient info can fit on one page or must go on to next				
				if(tempCount==linesCanPrint && middleOfPatient)  {
				  colHeight=(float)1037.5;
  				ev.Graphics.DrawLine(new Pen(Color.Gray),xTbPos,(float)(yTbPos+hTbPos+.5),xTbPos,colHeight);
					for(int i=1;i<9;i++) 
	  			  ev.Graphics.DrawLine(new Pen(Color.Gray),colPos[i]
							,(float)(yTbPos+hTbPos+.5),colPos[i],colHeight);
					ev.Graphics.DrawLine(new Pen(Color.Gray),xTbPos+wTbPos,(float)(yTbPos+hTbPos+.5)
						,xTbPos+wTbPos,colHeight);
				  middleOfPatientNewPage=true; 
					break;
        }
				//Prints the Name of patient and column headers
				if(ContrAccount.StatementA[11,linesPrinted]=="PatName"
					|| middleOfPatientNewPage==true && tempCount==0){
					if(!middleOfPatientNewPage)
					  curPatName=ContrAccount.StatementA[3,linesPrinted];
          for(int i=linesPrinted;i<ContrAccount.StatementA.GetLength(1);i++){
						patLines++;
					  if(ContrAccount.StatementA[11,i]=="PatTotal"){
							break;
						}
          }
					float currentY=(float)(yPos+20+NameFont.GetHeight(ev.Graphics)
						+bodyFont.GetHeight(ev.Graphics));
					if((currentY+(patLines*(bodyFont.GetHeight(ev.Graphics)*2)))>1025){
						if(currentY<1025){
              linesCanPrint=(int)((1025-currentY)/(bodyFont.GetHeight(ev.Graphics)));
						} 
            else{
						  break;
						}
            if(linesCanPrint > 5)
							middleOfPatient=true;
						else
							break;
			    }
			    else{
					  middleOfPatient=false;
						yPos+=20;
			    }
					if(!middleOfPatientNewPage){
					  ev.Graphics.DrawString(Lan.g(this,ContrAccount.StatementA[3,linesPrinted])//print name
						  ,NameFont,Brushes.Black,colPos[0],yPos);
					  yPos+=NameFont.GetHeight(ev.Graphics);
            linesPrinted++;
          }
					else  {
						yPos+=20;
						ev.Graphics.DrawString(Lan.g(this,curPatName),NameFont,Brushes.Black,colPos[0],yPos);//print name
						//yPos+=20;
					} 
					//Prints Heading Box and Lines          
					yPos+=10;
          yTbPos=(float)(yPos-2.5);
					xTbPos=colPos[0]-5;
					wTbPos=colPos[11]-colPos[0]+5;
					hTbPos=(float)(TotalFont.GetHeight(ev.Graphics)+5);
					ev.Graphics.FillRectangle(Brushes.LightGray,xTbPos,yTbPos,wTbPos,hTbPos);
					ev.Graphics.DrawRectangle(new Pen(Color.Black),xTbPos,yTbPos,wTbPos,hTbPos);  
          for(int i=1;i<11;i++) 
					  ev.Graphics.DrawLine(new Pen(Color.Black),colPos[i],yTbPos,colPos[i],yTbPos+hTbPos);
					//Prints the Column Titles
					for(int i=0;i<ColCaption.Length;i++)  { 
					  if(colAlign[i]==HorizontalAlignment.Right){
						  ev.Graphics.DrawString(Lan.g(this,ColCaption[i]),TotalFont,Brushes.Black,new RectangleF(
							  colPos[i+1]-ev.Graphics.MeasureString(ColCaption[i],TotalFont).Width-1,yPos
							  ,colPos[i+1]-colPos[i]+8,TotalFont.GetHeight(ev.Graphics)));
					  }
            else 
						  ev.Graphics.DrawString(Lan.g(this,ColCaption[i]),TotalFont,Brushes.Black,colPos[i],yPos);
			    }
          yPos+=20;
					middleOfPatientNewPage=false;
				}//end Print name
				//Prints out the patient info				
				for(int iCol=0;iCol<11;iCol++){
					if(colAlign[iCol]==HorizontalAlignment.Right){
						ev.Graphics.DrawString(Lan.g(this,ContrAccount.StatementA[iCol,linesPrinted])
							,bodyFont,Brushes.Black,new RectangleF(
							colPos[iCol+1]
							-ev.Graphics.MeasureString(ContrAccount.StatementA[iCol,linesPrinted],bodyFont).Width-1,yPos
							,colPos[iCol+1]-colPos[iCol]+8,bodyFont.GetHeight(ev.Graphics)));
					}
					else{
						ev.Graphics.DrawString(Lan.g(this,ContrAccount.StatementA[iCol,linesPrinted])
							,bodyFont,Brushes.Black,new RectangleF(
							colPos[iCol],yPos
							,colPos[iCol+1]-colPos[iCol]+6,bodyFont.GetHeight(ev.Graphics)));
					}
				}
				yPos+=bodyFont.GetHeight(ev.Graphics);
				linesPrinted++;
				//Prints out totals
				if(ContrAccount.StatementA[11,linesPrinted]=="PatTotal"){
					//  Prints Column lines					
  				ev.Graphics.DrawLine(new Pen(Color.Gray),xTbPos,(float)(yTbPos+hTbPos+.5),xTbPos,yPos);
					for(int i=1;i<11;i++) 
	  			  ev.Graphics.DrawLine(new Pen(Color.Gray),colPos[i],(float)(yTbPos+hTbPos+.5),colPos[i],yPos);
					ev.Graphics.DrawLine(new Pen(Color.Gray),xTbPos+wTbPos
						,(float)(yTbPos+hTbPos+.5),xTbPos+wTbPos,yPos);
				  ev.Graphics.DrawLine(new Pen(Color.Gray),xTbPos,yPos,xTbPos+wTbPos,yPos);
					//Prints Patient Totals
					for(int iCol=3;iCol<11;iCol++){
						ev.Graphics.DrawString(Lan.g(this,ContrAccount.StatementA[iCol,linesPrinted])
							,TotalFont,Brushes.Black,new RectangleF(
							colPos[iCol+1]
							-ev.Graphics.MeasureString(ContrAccount.StatementA[iCol,linesPrinted],TotalFont).Width-1,yPos
							,colPos[iCol+1]-colPos[iCol]+8,TotalFont.GetHeight(ev.Graphics)));
					}
					yPos+=TotalFont.GetHeight(ev.Graphics);
					linesPrinted++;
				}
				if(linesPrinted==ContrAccount.StatementA.GetLength(1))//if we are at the end of the array
					break;
				if(ContrAccount.StatementA[11,linesPrinted]=="GrandTotal"){
 					linesPrinted++;
				}			
        if(middleOfPatient)
				  tempCount++;  
			}//end while
			#region Commented Out 
			/*
			if(!headerPrinted){
				ev.Graphics.DrawString(Queries.CurReport.Title
					,titleFont,Brushes.Black
					,ev.MarginBounds.Width/2-ev.Graphics.MeasureString(Queries.CurReport.Title,titleFont).Width/2,yPos);
				yPos+=titleFont.GetHeight(ev.Graphics);
				for(int i=0;i<Queries.CurReport.SubTitle.Length;i++){
					ev.Graphics.DrawString(Queries.CurReport.SubTitle[i]
						,subtitleFont,Brushes.Black
						,ev.MarginBounds.Width/2-ev.Graphics.MeasureString(Queries.CurReport.SubTitle[i],subtitleFont).Width/2,yPos);
					yPos+=subtitleFont.GetHeight(ev.Graphics)+2;
				}
			}
			yPos+=10;
			ev.Graphics.DrawString("Date: "+DateTime.Today.ToString("d")
				,bodyFont,Brushes.Black,ev.MarginBounds.Left,yPos);
			//if(totalPages==0){
				ev.Graphics.DrawString("Page: "+(pagesPrinted+1).ToString()
					,bodyFont,Brushes.Black
					,ev.MarginBounds.Right-ev.Graphics.MeasureString("Page: "+(pagesPrinted+1).ToString(),bodyFont).Width,yPos);
			yPos+=bodyFont.GetHeight(ev.Graphics)+10;
			ev.Graphics.DrawLine(new Pen(Color.Black),ev.MarginBounds.Left,yPos-5,ev.MarginBounds.Right,yPos-5);
			//column captions:
			for(int i=0;i<Queries.CurReport.ColCaption.Length;i++){
				if(Queries.CurReport.ColAlign[i]==HorizontalAlignment.Right){
					ev.Graphics.DrawString(Queries.CurReport.ColCaption[i]
						,colCaptFont,Brushes.Black,new RectangleF(
						ev.MarginBounds.Left+Queries.CurReport.ColPos[i+1]
						-ev.Graphics.MeasureString(Queries.CurReport.ColCaption[i],colCaptFont).Width,yPos
						,Queries.CurReport.ColWidth[i],colCaptFont.GetHeight(ev.Graphics)));
				}
				else{
					ev.Graphics.DrawString(Queries.CurReport.ColCaption[i]
						,colCaptFont,Brushes.Black,ev.MarginBounds.Left+Queries.CurReport.ColPos[i],yPos);
				}
			}
			yPos+=bodyFont.GetHeight(ev.Graphics)+5;
			//table:
			while(yPos<ev.MarginBounds.Top+ev.MarginBounds.Height && linesPrinted < Queries.TableQ.Rows.Count){
				for(int iCol=0;iCol<Queries.TableQ.Columns.Count;iCol++){
					if(Queries.CurReport.ColAlign[iCol]==HorizontalAlignment.Right){
						ev.Graphics.DrawString(grid2[linesPrinted,iCol].ToString()
							,bodyFont,Brushes.Black,new RectangleF(
							ev.MarginBounds.Left+Queries.CurReport.ColPos[iCol+1]
							-ev.Graphics.MeasureString(grid2[linesPrinted,iCol].ToString(),bodyFont).Width-1,yPos
							,Queries.CurReport.ColWidth[iCol],bodyFont.GetHeight(ev.Graphics)));
					}
					else{
						ev.Graphics.DrawString(grid2[linesPrinted,iCol].ToString()
							,bodyFont,Brushes.Black,new RectangleF(
							ev.MarginBounds.Left+Queries.CurReport.ColPos[iCol],yPos
							,Queries.CurReport.ColPos[iCol+1]-Queries.CurReport.ColPos[iCol]+6,bodyFont.GetHeight(ev.Graphics)));
					}
				}
				yPos+=bodyFont.GetHeight(ev.Graphics);
				linesPrinted++;
				if(linesPrinted==Queries.TableQ.Rows.Count){
					tablePrinted=true;

				}
			}
			//totals:
			if(tablePrinted){
				if(yPos<ev.MarginBounds.Top+ev.MarginBounds.Height){
					ev.Graphics.DrawLine(new Pen(Color.Black),ev.MarginBounds.Left,yPos+3,ev.MarginBounds.Right,yPos+3);
					yPos+=4;
					for(int iCol=0;iCol<Queries.TableQ.Columns.Count;iCol++){
						if(Queries.CurReport.ColAlign[iCol]==HorizontalAlignment.Right){
							float textWidth=(float)(ev.Graphics.MeasureString(Queries.CurReport.ColTotal[iCol].ToString("F"),subtitleFont).Width);
							ev.Graphics.DrawString(Queries.CurReport.ColTotal[iCol].ToString("F")
								,subtitleFont,Brushes.Black,new RectangleF(
								ev.MarginBounds.Left+Queries.CurReport.ColPos[iCol+1]-textWidth+3,yPos//the 3 is arbitrary
								,textWidth,subtitleFont.GetHeight(ev.Graphics)));
						}
						//else{
						//	ev.Graphics.DrawString(grid2[linesPrinted,iCol].ToString()
						//		,bodyFont,Brushes.Black,new RectangleF(
						//		ev.MarginBounds.Left+Queries.CurReport.ColPos[iCol],yPos
						//		,Queries.CurReport.ColPos[iCol+1]-Queries.CurReport.ColPos[iCol],bodyFont.GetHeight(ev.Graphics)));
						//}
					}
					totalsPrinted=true;
					yPos+=subtitleFont.GetHeight(ev.Graphics);
				}
			}
			//Summary
			if(totalsPrinted){
				if(yPos+Queries.CurReport.Summary.Length*subtitleFont.GetHeight(ev.Graphics)
					< ev.MarginBounds.Top+ev.MarginBounds.Height) {
					ev.Graphics.DrawLine(new Pen(Color.Black),ev.MarginBounds.Left,yPos+2,ev.MarginBounds.Right,yPos+2);
					yPos+=bodyFont.GetHeight(ev.Graphics);
					for(int i=0;i<Queries.CurReport.Summary.Length;i++){
					//while(yPos<ev.MarginBounds.Top+ev.MarginBounds.Height && linesPrinted < Queries.TableQ.Rows.Count){
						//if(yPos>=ev.MarginBounds.Top+ev.MarginBounds.Height) break;
						ev.Graphics.DrawString(Queries.CurReport.Summary[i]
							,subtitleFont,Brushes.Black,ev.MarginBounds.Left,yPos);
						yPos+=subtitleFont.GetHeight(ev.Graphics);
					}
				}
			}
			if(linesPrinted < Queries.TableQ.Rows.Count){
				ev.HasMorePages = true;
				pagesPrinted++;
			}
			else{
				ev.HasMorePages = false;
				//UpDownPage.Maximum=pagesPrinted+1;
				totalPages=pagesPrinted+1;
				labelTotPages.Text="1 / "+totalPages.ToString();
				pagesPrinted=0;
				linesPrinted=0;
				headerPrinted=false;
				tablePrinted=false;
				totalsPrinted=false;
			}
	*/		
#endregion
 
			if(linesPrinted<ContrAccount.StatementA.GetLength(1)){
				ev.HasMorePages = true;
				pagesPrinted++;
			}
			else{
				ev.HasMorePages = false;
				totalPages=pagesPrinted+1;
				labelTotPages.Text="1 / "+totalPages.ToString();
				pagesPrinted=0;
				linesPrinted=0;
			}
		}
		private void butZoomIn_Click(object sender, System.EventArgs e) {
			butFullPage.Visible=true;
			butZoomIn.Visible=false;
			printPreviewControl2.Zoom=1;		
		}
		private void butBack_Click(object sender, System.EventArgs e) {
			if(printPreviewControl2.StartPage==0) 
				return;
			printPreviewControl2.StartPage--;
			labelTotPages.Text=(printPreviewControl2.StartPage+1).ToString()
				+" / "+totalPages.ToString();	
		}
		private void butFwd_Click(object sender, System.EventArgs e) {
			if(printPreviewControl2.StartPage==totalPages-1) return;
			printPreviewControl2.StartPage++;
			labelTotPages.Text=(printPreviewControl2.StartPage+1).ToString()
				+" / "+totalPages.ToString();		
		}
		private void butPrint_Click(object sender, System.EventArgs e) {
			PrintReport(false);
			DialogResult=DialogResult.Cancel;			
		}
		private void butFullPage_Click(object sender, System.EventArgs e) {
			butFullPage.Visible=false;
			butZoomIn.Visible=true;
			printPreviewControl2.Zoom=((double)printPreviewControl2.ClientSize.Height
				/(double)pd2.DefaultPageSettings.PaperSize.Height);	
		}
	}
}
