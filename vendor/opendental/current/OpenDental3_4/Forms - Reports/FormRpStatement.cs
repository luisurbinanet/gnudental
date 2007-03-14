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
		private OpenDental.UI.Button butPrint;
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.Panel panelZoom;
		private OpenDental.UI.Button butFwd;
		private OpenDental.UI.Button butBack;
		private System.Windows.Forms.Label labelTotPages;
		private OpenDental.UI.Button butZoomIn;
		private OpenDental.UI.Button butFullPage;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.PrintDialog printDialog2;
		private System.Drawing.Printing.PrintDocument pd2;
		private int totalPages;
		private Font bodyFont=new Font("Arial",9);
		private Font NameFont=new Font("Arial",10,FontStyle.Bold);
		private Font TotalFont=new Font("Arial",9,FontStyle.Bold);
		private Font GTotalFont=new Font("Arial",9,FontStyle.Bold);
		///<summary></summary>
		private int linesPrinted;
		private bool isFirstLineOnPage;
		private bool notePrinted;
		private int pagesPrinted;
		///<summary>Gets set externally before printing.  It has to be public for debugging</summary>
		public bool HidePayment;
		///<summary>Gets set externally before printing.  It has to be public for debugging</summary>
		public string Note;
		///<summary>2D array simply holds the table to print.  This will be improved some day.  Gets set externally before printing.  It has to be public for debugging.</summary>
		public string[,] StatementA;
		private Family FamCur;

		///<summary></summary>
		public FormRpStatement(Family famCur){
			InitializeComponent();
			FamCur=famCur;
			Lan.F(this, new Control[] 
				{//exclude:
					labelTotPages
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
			this.butPrint = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.panelZoom = new System.Windows.Forms.Panel();
			this.butFwd = new OpenDental.UI.Button();
			this.butBack = new OpenDental.UI.Button();
			this.labelTotPages = new System.Windows.Forms.Label();
			this.butZoomIn = new OpenDental.UI.Button();
			this.butFullPage = new OpenDental.UI.Button();
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
				PrintReport(true);//this only happens during debugging
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
			try{
				if(justPreview){
					printPreviewControl2.Document=pd2;
				}
				else{
					//printDialog2=new PrintDialog();
					//printDialog2.Document=pd2;
					//if(printDialog2.ShowDialog()==DialogResult.OK){
						linesPrinted=0;
						isFirstLineOnPage=false;
						notePrinted=false;
						pagesPrinted=0;
						pd2.Print();
					DialogResult=DialogResult.OK;
					//}
				}
			}
			catch{
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
		}
		private void pd2_PrintPage(object sender, PrintPageEventArgs ev){//raised for each page to be printed
			Graphics g=ev.Graphics;
		  float yPos = 30;
			float rowHeight=0;
			#region Page 1 header
			if(pagesPrinted==0){
				//HEADING------------------------------------------------------------------------------
 				string heading = "STATEMENT";
				Font headingFont=new Font("Arial",14,FontStyle.Bold);
				float xHeading = (float)(425-(g.MeasureString(heading,headingFont).Width/2));
				g.DrawString(heading,headingFont,Brushes.Black,xHeading,yPos);
				//Date
 				string date = DateTime.Today.ToShortDateString();
				Font dateFont=new Font("Arial",9,FontStyle.Regular);
				float xDate = (float)(425-(g.MeasureString(date,dateFont).Width/2));
				g.DrawString(date,dateFont,Brushes.Black,xDate,yPos+20);
				//Practice Address----------------------------------------------------------------------
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
					pPhone="("+pPhone.Substring(0,3)+")"+pPhone.Substring(3,3)+"-"+pPhone.Substring(6);
				float labelX=30;
				g.DrawString(pTitle,pFont,Brushes.Black,labelX,yPos);
				yPos+=18;
				g.DrawString(pAddress,pFont,Brushes.Black,labelX,yPos);	    
				yPos+=18;
				if(pAddress2!=""){
					g.DrawString(pAddress2,pFont,Brushes.Black,labelX,yPos);	    
					yPos+=18;
				}
				g.DrawString(CityStateZip,pFont,Brushes.Black,labelX,yPos);
				yPos+=18;
				g.DrawString(pPhone,pFont,Brushes.Black,labelX,yPos);
				yPos+=18;
				//Grand Total Headings and Lines ---------------------------------------------------------
				if(!HidePayment){
					float yTotal=(float)100;
					float xTotal=(float)275;
					float wTotal=300;
					float hTotal=(float)12.5;
					g.FillRectangle(Brushes.LightGray,xTotal,yTotal,wTotal,hTotal);
					g.DrawRectangle(new Pen(Color.Black),xTotal,yTotal,wTotal,hTotal);  
						for(int i=1;i<4;i++) 
								g.DrawLine(new Pen(Color.Black),xTotal+(i*75),yTotal,xTotal+(i*75),yTotal+hTotal); 
					string head1="";
					string head2="Total";
					string head3="- Ins Est";
					string head4="= Amt Due";
					Font tHeadFont=new Font("Arial",8,FontStyle.Bold);
					float xHead1 = (float)(xTotal+75/2-(g.MeasureString(head1,tHeadFont).Width/2));
					float xHead2 = (float)(xTotal+3*75/2-(g.MeasureString(head2,tHeadFont).Width/2));
					float xHead3 = (float)(xTotal+5*75/2-(g.MeasureString(head3,tHeadFont).Width/2));
					float xHead4 = (float)(xTotal+7*75/2-(g.MeasureString(head4,tHeadFont).Width/2));
					g.DrawString(head1,tHeadFont,Brushes.Black,xHead1,yTotal); 
					g.DrawString(head2,tHeadFont,Brushes.Black,xHead2,yTotal); 			
					g.DrawString(head3,tHeadFont,Brushes.Black,xHead3,yTotal); 
					g.DrawString(head4,tHeadFont,Brushes.Black,xHead4,yTotal); 
					//Grand Total Data and Lines-------------------------------------------------------------
					for(int i=0;i<5;i++) {
	  				g.DrawLine(new Pen(Color.Gray),xTotal+(i*75),(float)(yTotal+hTotal+.5),
							xTotal+(i*75),(float)(yTotal+hTotal+.5+25));
					} 
					g.DrawLine(new Pen(Color.Gray),xTotal,yTotal+hTotal+25,xTotal+wTotal,
						yTotal+hTotal+25);
					//only prints totals if a GrandTotal line is present at the end of the array
					if(StatementA[11,StatementA.GetLength(1)-1]=="GrandTotal"){
						string tot1=StatementA[4,StatementA.GetLength(1)-1];
						string tot2=StatementA[5,StatementA.GetLength(1)-1];
						string tot3=StatementA[6,StatementA.GetLength(1)-1];
						string tot4=StatementA[7,StatementA.GetLength(1)-1];
						Font tTotalFont=new Font("Arial",8,FontStyle.Regular);
						float xTotal1=(float)(xTotal+75/2-(g.MeasureString(tot1,bodyFont).Width/2));
						float xTotal2=(float)(xTotal+3*75/2-(g.MeasureString(tot2,bodyFont).Width/2));
						float xTotal3=(float)(xTotal+5*75/2-(g.MeasureString(tot3,bodyFont).Width/2));
						float xTotal4=(float)(xTotal+7*75/2-(g.MeasureString(tot4,bodyFont).Width/2));
						g.DrawString(tot1,bodyFont,Brushes.Black,xTotal1,yTotal+hTotal+5); 
						g.DrawString(tot2,bodyFont,Brushes.Black,xTotal2,yTotal+hTotal+5); 			
						g.DrawString(tot3,bodyFont,Brushes.Black,xTotal3,yTotal+hTotal+5); 
						g.DrawString(tot4,bodyFont,Brushes.Black,xTotal4,yTotal+hTotal+5); 
					}				
					//Credit Card Info-----------------------------------------------------------------------
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
						float xDis=(float)(625.5-(g.MeasureString(dis,disFont).Width/2)+5);
						g.DrawString(type,creditFont,Brushes.Black,xCredit,yCredit);
						g.DrawString(amt,creditFont,Brushes.Black,xCredit,yCredit+space); 
						g.DrawString(exp,creditFont,Brushes.Black,xCredit,yCredit+space*2);	
 						g.DrawString(approv,creditFont,Brushes.Black,xCredit,yCredit+space*3);	     
						g.DrawString(name,creditFont,Brushes.Black,xCredit,yCredit+(space*4));
						g.DrawString(dis,disFont,Brushes.Black,xDis,yCredit+space*4+13);
						g.DrawString(sig,creditFont,Brushes.Black,xCredit,yCredit+(space*5)); 
  					g.DrawLine(new Pen(Color.Black),xCredit+(g.MeasureString(type,creditFont)).Width,
							yCredit+creditFont.GetHeight(g),776,yCredit+creditFont.GetHeight(g)); 
  					g.DrawLine(new Pen(Color.Black),xCredit+(g.MeasureString(amt,creditFont)).Width,
							yCredit+creditFont.GetHeight(g)+space,776,yCredit+creditFont.GetHeight(g)
							+space); 
  					g.DrawLine(new Pen(Color.Black),xCredit+(g.MeasureString(exp,creditFont)).Width,
							yCredit+creditFont.GetHeight(g)+(space*2),776,yCredit+creditFont.GetHeight(g)
							+(space*2)); 
  					g.DrawLine(new Pen(Color.Black),xCredit+(g.MeasureString(approv,creditFont)).Width,
							yCredit+creditFont.GetHeight(g)+(space*3),776,yCredit+creditFont.GetHeight(g)
							+(space*3)); 
  					g.DrawLine(new Pen(Color.Black),xCredit+(g.MeasureString(name,creditFont)).Width,
							yCredit+creditFont.GetHeight(g)+(space*4),776,yCredit+creditFont.GetHeight(g)
							+(space*4)); 
						g.DrawLine(new Pen(Color.Black),xCredit+(g.MeasureString(sig,creditFont)).Width,
							yCredit+creditFont.GetHeight(g)+(space*5),776,yCredit+creditFont.GetHeight(g)
							+(space*5)); 
					}
					//AMOUNT ENCLOSED------------------------------------------------------------------------
					float yEncl=(float)100;
					float xEncl=(float)651;
					float wEncl=125;
					float hEncl=(float)12.5;
					g.FillRectangle(Brushes.LightGray,xEncl,yEncl,wEncl,hEncl);
					g.DrawRectangle(new Pen(Color.Black),xEncl,yEncl,wEncl,hEncl);
					string amtEncl="Amount Enclosed";
					Font enclFont=new Font("Arial",8,FontStyle.Bold);
					float xamtEncl = (float)(713.5-(g.MeasureString(amtEncl,enclFont).Width/2));
					g.DrawString(amtEncl,enclFont,Brushes.Black,xamtEncl,yEncl); 
	  			g.DrawLine(new Pen(Color.Gray),xEncl,(float)(yEncl+hEncl+.5),
						xEncl,(float)(yEncl+hEncl+.5+25));
 	  			g.DrawLine(new Pen(Color.Gray),xEncl+125,(float)(yEncl+hEncl+.5),
						xEncl+125,(float)(yEncl+hEncl+.5+25));      
					g.DrawLine(new Pen(Color.Gray),xEncl,yEncl+hEncl+25,xEncl+wEncl,
						yEncl+hEncl+25);
					string payNow="";//"(Payment Due upon Receipt)";
					Font payNowFont=new Font("Arial",5,FontStyle.Regular);
					float xPayNow=(float)(713.5-(g.MeasureString(payNow,payNowFont).Width/2)+5);
					g.DrawString(payNow,payNowFont,Brushes.Black,xPayNow,yEncl+hEncl+30);
				}//if(!HidePayment){
				// Rectangle size of window to put in address. Here for debugging
				//float x2=(float)(62.5);
				//float y2=(float)((225)); //+10
				//float w2=(float)(350);
				//float h2=(float)((75));
				//g.DrawRectangle(new Pen(Color.Black),x2,y2,w2,h2);
				//Prints Grayish Rectangle to block off 
				//float x=25;//(float)(-12.5);  // 0 = 1/4 inch
				//float	y=(float)((187.5));//+10
				//float w=(float)(437.5);
				//float h=(float)((150));
				//g.DrawRectangle(new Pen(Color.Gray),x,y,w,h);
				//Patient's Billing Address--------------------------------------------------------	
				Font addrFont=new Font("Arial",11,FontStyle.Regular);
				yPos=225 + 1;//+10
				labelX=(float)(62.5+12.5);
				Patient PatCur=FamCur.List[0].Copy();
				string pName=PatCur.FName +" "+PatCur.MiddleI+" "+PatCur.LName;
				pAddress=PatCur.Address;
				pCity=PatCur.City;
				pState=PatCur.State;
				pZip=PatCur.Zip;
				CityStateZip=pCity+", "+pState+" "+pZip;
				g.DrawString(pName,addrFont,Brushes.Black,labelX,yPos);
				yPos+=19;
				g.DrawString(pAddress,addrFont,Brushes.Black,labelX,yPos);	    
				yPos+=19;
				if(PatCur.Address2!=""){
					g.DrawString(PatCur.Address2,addrFont,Brushes.Black,labelX,yPos);	    
					yPos+=19;  
				}
   			g.DrawString(CityStateZip,addrFont,Brushes.Black,labelX,yPos);
				if(!HidePayment){
					//perforated line------------------------------------------------------------------
					Pen pen2 = new Pen(Brushes.Black);
					pen2.Width=(float).125;
					pen2.DashStyle=DashStyle.Dot;
					yPos=350;//3.62 inches from top
					g.DrawLine(pen2,0,yPos,ev.PageBounds.Width,yPos);
					//Please Detach Statement----------------------------------------------------
 					yPos=350;  //  1/3 page down plus one line
 					string detach = "PLEASE DETACH AND RETURN THE UPPER PORTION WITH YOUR PAYMENT";
					Font DetachFont=new Font("Arial",6,FontStyle.Bold);
					float xDetach = (float)(425-(g.MeasureString(detach,DetachFont).Width/2));
					g.DrawString(detach,DetachFont,Brushes.Gray,xDetach,yPos+5);
					yPos+=15;
				}//if(!HidePayment){
				else{
					yPos=350+15;
				}
				//yPos=770;//change this value to test multiple pages
			}//if(pagesPrinted==0){
			else {
				yPos=(float)18.75;
			}
			#endregion
			#region Body Tables
			//an if is not needed here because the while loop will handle it
			//Body Tables----------------------------------------------------------------------------
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
			isFirstLineOnPage=true;
			while(yPos<ev.MarginBounds.Top+ev.MarginBounds.Height
				&& linesPrinted<StatementA.GetLength(1))
			{
				if(StatementA[11,linesPrinted]=="PatName"){
					//Patient Name-------------------------------------------------------------------------
					//if(there is not room for at least a few rows){
						//break
					//}
					g.DrawString(StatementA[3,linesPrinted],NameFont,Brushes.Black,colPos[0],yPos);
					yPos+=NameFont.GetHeight(g)+7;
					//Heading Box and Lines----------------------------------------------------------------       
					rowHeight=TotalFont.GetHeight(g)+3;
					g.FillRectangle(Brushes.LightGray,colPos[0],yPos,colPos[11]-colPos[0],rowHeight);
					g.DrawRectangle(new Pen(Color.Black),colPos[0],yPos,colPos[11]-colPos[0],rowHeight);  
          for(int i=1;i<11;i++) 
					  g.DrawLine(new Pen(Color.Black),colPos[i],yPos,colPos[i],yPos+rowHeight);
					//Column Titles
					for(int i=0;i<ColCaption.Length;i++)  { 
					  if(colAlign[i]==HorizontalAlignment.Right){
						  g.DrawString(Lan.g(this,ColCaption[i]),TotalFont,Brushes.Black,new RectangleF(
							  colPos[i+1]-g.MeasureString(ColCaption[i],TotalFont).Width-1,yPos+1
							  ,colPos[i+1]-colPos[i]+8,TotalFont.GetHeight(g)));
					  }
            else 
						  g.DrawString(Lan.g(this,ColCaption[i]),TotalFont,Brushes.Black,colPos[i],yPos+1);
			    }
          yPos+=rowHeight;
				}
				else if(StatementA[11,linesPrinted]=="PatTotal"){
					//Totals--------------------------------------------------------------------------------
					for(int iCol=3;iCol<11;iCol++){
						g.DrawString(Lan.g(this,StatementA[iCol,linesPrinted])
							,TotalFont,Brushes.Black,new RectangleF(
							colPos[iCol+1]
							-g.MeasureString(StatementA[iCol,linesPrinted],TotalFont).Width-1,yPos
							,colPos[iCol+1]-colPos[iCol]+8,TotalFont.GetHeight(g)));
					}
					yPos+=TotalFont.GetHeight(g);
				}
				else{
					//Body data--------------------------------------------------------------------------------
					if(isFirstLineOnPage){
						//g.DrawLine(new Pen(Color.Gray),colPos[0],yPos,colPos[11],yPos);
					}
					rowHeight=bodyFont.GetHeight(g);
					for(int i=0;i<11;i++){
						//left line for this cell
						g.DrawLine(new Pen(Color.Gray),colPos[i],yPos,colPos[i],yPos+rowHeight);
						if(i==10){//if this is the right column, then also draw line for right side of cell
							g.DrawLine(new Pen(Color.Gray),colPos[i+1],yPos,colPos[i+1],yPos+rowHeight);
						}
						if(colAlign[i]==HorizontalAlignment.Right){
							g.DrawString(Lan.g(this,StatementA[i,linesPrinted])
								,bodyFont,Brushes.Black,new RectangleF(
								colPos[i+1]
								-g.MeasureString(StatementA[i,linesPrinted],bodyFont).Width-1,yPos
								,colPos[i+1]-colPos[i]+8,bodyFont.GetHeight(g)));
						}
						else{
							g.DrawString(Lan.g(this,StatementA[i,linesPrinted])
								,bodyFont,Brushes.Black,new RectangleF(
								colPos[i],yPos
								,colPos[i+1]-colPos[i]+6,bodyFont.GetHeight(g)));
						}
						if(StatementA[11,linesPrinted+1]=="PatTotal"){
							g.DrawLine(new Pen(Color.Gray),colPos[i],yPos+rowHeight,colPos[11],yPos+rowHeight);
						}
					}
					yPos+=rowHeight;
				}
				isFirstLineOnPage=false;
				linesPrinted++;
				if(linesPrinted<StatementA.GetLength(1)
					&& StatementA[11,linesPrinted]=="GrandTotal")
				{
 					linesPrinted++;
				}
			}//end while lines
			#endregion
			#region Note
			//Note------------------------------------------------------------------------------------------
			if(!notePrinted && //if note has not printed
				linesPrinted==StatementA.GetLength(1))//and all table data already printed
			{
				if(Note==""){
					notePrinted=true;
				}
				else{
					float noteHeight=g.MeasureString(Note,bodyFont,colPos[11]-colPos[0]).Height;
					if(noteHeight<ev.MarginBounds.Bottom-yPos){//if there is room
						g.DrawString(Note,bodyFont,Brushes.Black,new RectangleF(colPos[0],yPos
							,colPos[11]-colPos[0],noteHeight));
						notePrinted=true;
					}
					//otherwise, pagesPrinted will increment and 
				}
			}
			#endregion
			if(linesPrinted<StatementA.GetLength(1)){
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
