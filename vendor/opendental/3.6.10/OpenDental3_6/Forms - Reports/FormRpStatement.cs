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
		private System.Drawing.Printing.PrintDocument pd2;
		private int totalPages;
		private Font bodyFont=new Font("Arial",9);
		private Font NameFont=new Font("Arial",10,FontStyle.Bold);
		private Font TotalFont=new Font("Arial",9,FontStyle.Bold);
		private Font GTotalFont=new Font("Arial",9,FontStyle.Bold);
		///<summary></summary>
		private int famsPrinted;
		///<summary>For one family</summary>
		private int linesPrinted;
		private bool isFirstLineOnPage;
		private bool notePrinted;
		///<summary>For one family</summary>
		private int pagesPrinted;
		private bool HidePayment;
		private string Note;
		///<summary>Simply holds the table to print.  This will be improved some day.  The first dimension is of the family.  The other two dimensions represent a table for that family.</summary>
		private string[][,] StatementA;
		//private Family FamCur;
		private bool SubtotalsOnly;
		///<summary>Rirst dim is for the family. Second dim is family members</summary>
		private int[][] PatNums;
		///<summary>The guarantor for the statement that is currently printing.</summary>
		private Patient PatGuar;

		///<summary></summary>
		public FormRpStatement(){
			InitializeComponent();
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
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.Image = ((System.Drawing.Image)(resources.GetObject("butPrint.Image")));
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(340, 5);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(85, 27);
			this.butPrint.TabIndex = 8;
			this.butPrint.Text = "          Print";
			this.butPrint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(434, 5);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 27);
			this.butClose.TabIndex = 7;
			this.butClose.Text = "Close";
			// 
			// panelZoom
			// 
			this.panelZoom.Controls.Add(this.butFwd);
			this.panelZoom.Controls.Add(this.butBack);
			this.panelZoom.Controls.Add(this.labelTotPages);
			this.panelZoom.Controls.Add(this.butZoomIn);
			this.panelZoom.Controls.Add(this.butFullPage);
			this.panelZoom.Controls.Add(this.butClose);
			this.panelZoom.Controls.Add(this.butPrint);
			this.panelZoom.Location = new System.Drawing.Point(35, 419);
			this.panelZoom.Name = "panelZoom";
			this.panelZoom.Size = new System.Drawing.Size(524, 37);
			this.panelZoom.TabIndex = 12;
			// 
			// butFwd
			// 
			this.butFwd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butFwd.Autosize = true;
			this.butFwd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFwd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFwd.Image = ((System.Drawing.Image)(resources.GetObject("butFwd.Image")));
			this.butFwd.Location = new System.Drawing.Point(195, 7);
			this.butFwd.Name = "butFwd";
			this.butFwd.Size = new System.Drawing.Size(18, 22);
			this.butFwd.TabIndex = 13;
			this.butFwd.Click += new System.EventHandler(this.butFwd_Click);
			// 
			// butBack
			// 
			this.butBack.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butBack.Autosize = true;
			this.butBack.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBack.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBack.Image = ((System.Drawing.Image)(resources.GetObject("butBack.Image")));
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
			this.butZoomIn.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butZoomIn.Autosize = true;
			this.butZoomIn.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butZoomIn.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("butZoomIn.Image")));
			this.butZoomIn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butZoomIn.Location = new System.Drawing.Point(9, 5);
			this.butZoomIn.Name = "butZoomIn";
			this.butZoomIn.Size = new System.Drawing.Size(94, 27);
			this.butZoomIn.TabIndex = 10;
			this.butZoomIn.Text = "       Zoom In";
			this.butZoomIn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butZoomIn.Click += new System.EventHandler(this.butZoomIn_Click);
			// 
			// butFullPage
			// 
			this.butFullPage.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butFullPage.Autosize = true;
			this.butFullPage.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFullPage.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
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
			this.Controls.Add(this.panelZoom);
			this.Controls.Add(this.printPreviewControl2);
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
			//this only happens during debugging
			labelTotPages.Text="/ "+totalPages.ToString();
			printPreviewControl2.Zoom=((double)printPreviewControl2.ClientSize.Height
				/(double)pd2.DefaultPageSettings.PaperSize.Height);
			labelTotPages.Text="/ "+totalPages.ToString();
		}

		///<summary>Used from FormBilling to print all statements for all the supplied patNums.</summary>
		public void LoadAndPrint(int[] guarNums){
			//this will be moved later. Right now, it's functional, but inefficient:
			int[][] patNums=new int[guarNums.Length][];
			Family famCur;
			for(int i=0;i<guarNums.Length;i++){//loop through each family
				famCur=Patients.GetFamily(guarNums[i]);
				patNums[i]=new int[famCur.List.Length];
				for(int j=0;j<famCur.List.Length;j++){
					patNums[i][j]=famCur.List[j].PatNum;
				}
			}
			PrintStatements(patNums,DateTime.Today.AddDays(-45),DateTime.Today,true,false,false,false,"");
		}

		///<summary>This is called from ContrAccount about 3 times and also from FormRpStatement as part of the billing process.  This is what you call to print statements, either one or many.  For the patNum parameter, the first dim is for the family. Second dim is family members.</summary>
		public void PrintStatements(int[][] patNums,DateTime fromDate,DateTime toDate,bool includeClaims, bool subtotalsOnly,bool hidePayment,bool nextAppt,string note){
			//these 4 variables are needed by the printing logic. The rest are not.
			PatNums=(int[][])patNums.Clone();
			Note=note;
			SubtotalsOnly=subtotalsOnly;
			HidePayment=hidePayment;
			PrintDocument pd=new PrintDocument();
			if(!Printers.SetPrinter(pd,PrintSituation.Statement)){
				return;
			}
			pd.PrintPage+=new PrintPageEventHandler(this.pd2_PrintPage);
			pd.DefaultPageSettings.Margins=new Margins(10,40,40,60);//?
			ContrAccount contrAccount=new ContrAccount();
			StatementA=new string[patNums.GetLength(0)][,];
			for(int i=0;i<patNums.GetLength(0);i++){//loop through each family
				StatementA[i]=AssembleStatement(contrAccount,patNums[i],fromDate,toDate,includeClaims,nextAppt);
				Commlogs.Cur=new Commlog();
				Commlogs.Cur.CommDateTime=DateTime.Now;
				Commlogs.Cur.CommType=CommItemType.StatementSent;
				Commlogs.Cur.PatNum=patNums[i][0];//uaually the guarantor
				//there is no dialog here because it is just a simple entry
				Commlogs.InsertCur();
			}
			famsPrinted=0;
			linesPrinted=0;
			isFirstLineOnPage=false;
			notePrinted=false;
			pagesPrinted=0;
			#if DEBUG
				printPreviewControl2.Document=pd;
			#else
				try{
					pd.Print();
				}
				catch{
					MessageBox.Show(Lan.g(this,"Printer not available"));
				}
			#endif
		}

		/// <summary>Gets one statement grid for a single family.</summary>
		private string[,] AssembleStatement(ContrAccount contrAccount,int[] famPatNums,DateTime fromDate,DateTime toDate,bool includeClaims,bool nextAppt){
			ArrayList StatementAL=new ArrayList();
			AcctLine tempLine;
			double subtotal;//used because .NET won't let me contrAccount.Subtotal.ToString().
			for(int i=0;i<famPatNums.Length;i++){
				contrAccount.RefreshModuleData(famPatNums[i]);
				contrAccount.FillAcctLineAL(fromDate,toDate,includeClaims,SubtotalsOnly);
				//FamTotDue+=PatCur.EstBalance;
				tempLine=new AcctLine();
				tempLine.Description=contrAccount.PatCur.GetNameLF();
				if(nextAppt){
					Appointment[] allAppts=Appointments.GetForPat(contrAccount.PatCur.PatNum);
					for(int a=0;a<allAppts.Length;a++){
						if(allAppts[a].AptDateTime.Date<=DateTime.Today){
							continue;//ignore old appts.
						}
						tempLine.Description+=":  "+Lan.g(this,"Your next appointment is on")+" "
							+allAppts[a].AptDateTime.ToString();
						break;//so that only one appointment will be displayed
					}
				}
				tempLine.StateType="PatName";
				StatementAL.Add(tempLine);//this adds a group heading for one patient.
				StatementAL.AddRange(contrAccount.AcctLineAL);//this adds the detail for one patient
				tempLine=new AcctLine();
				tempLine.Description="";
				tempLine.StateType="PatTotal";
				tempLine.Fee="";
				tempLine.InsEst="";
				tempLine.InsPay="";
				tempLine.Patient="";
				if(SubtotalsOnly){
					subtotal=contrAccount.SubTotal;
					tempLine.Balance=subtotal.ToString("n");
				}
				else{
					tempLine.Balance=contrAccount.PatCur.EstBalance.ToString("F");
				}
				//group footer for one patient: If subtotalsOnly, then this will actually be a subtotal
				StatementAL.Add(tempLine);
			}
			//GrandTotal is no longer used since it is available to FormRpStatement from FamCur.
			string[,] StatA=new string[12,StatementAL.Count];
			//now, move the collection of lines into a simple array to print.
			for(int i=0;i<StatA.GetLength(1);i++){
				try{//error catch bad dates
					StatA[0,i]=((AcctLine)StatementAL[i]).Date; 
				}
				catch{
					StatA[0,i]="";
				}
				//StatementA[1,i]=((AcctLine)StatementAL[i]).Provider;
				StatA[1,i]=((AcctLine)StatementAL[i]).Code;
				StatA[2,i]=((AcctLine)StatementAL[i]).Tooth;
				StatA[3,i]=((AcctLine)StatementAL[i]).Description;
				StatA[4,i]=((AcctLine)StatementAL[i]).Fee;
				StatA[5,i]=((AcctLine)StatementAL[i]).InsEst;
				StatA[6,i]=((AcctLine)StatementAL[i]).InsPay;
				if(StatA[6,i]=="In Queue")
					StatA[6,i]="Sent";//to keep it simple for the patient
				StatA[7,i]=((AcctLine)StatementAL[i]).Patient;
				StatA[8,i]=((AcctLine)StatementAL[i]).Adj;
				StatA[9,i]=((AcctLine)StatementAL[i]).Paid;
				StatA[10,i]=((AcctLine)StatementAL[i]).Balance;
				StatA[11,i]=((AcctLine)StatementAL[i]).StateType;
			}//end for
			return StatA;
		}

		private void GetPatGuar(int patNum){
			if(PatGuar!=null
				&& patNum==PatGuar.PatNum){//if PatGuar is already set
				return;
			}
			//but if the guarantor is not on the list of patients in the fam to print, it will also refresh
      Family FamCur=Patients.GetFamily(patNum);
			PatGuar=FamCur.List[0].Copy();
		}

		private void pd2_PrintPage(object sender, PrintPageEventArgs ev){//raised for each page to be printed
			Graphics g=ev.Graphics;
		  float yPos=30;
			float xPos=0;
			float width=0;
			float height=0;
			float rowHeight=0;
			Font font;
			Pen pen=new Pen(Color.Black);
			Brush brush=Brushes.Black;
			string text;
			GetPatGuar(PatNums[famsPrinted][0]);
			#region Page 1 header
			if(pagesPrinted==0){
				//HEADING------------------------------------------------------------------------------
 				text=Lan.g(this,"STATEMENT");
				font=new Font("Arial",14,FontStyle.Bold);
				g.DrawString(text,font,brush,425-g.MeasureString(text,font).Width/2,yPos);
 				text=DateTime.Today.ToShortDateString();
				font=new Font("Arial",10);
				yPos+=22;
				g.DrawString(text,font,brush,425-g.MeasureString(text,font).Width/2,yPos);
				yPos+=18;
				text=Lan.g(this,"Account Number")+" ";
				if(Prefs.GetBool("StatementAccountsUseChartNumber")){
					text+=PatGuar.ChartNumber;
				}
				else{
					text+=PatGuar.PatNum;
				}
				g.DrawString(text,font,brush,425-g.MeasureString(text,font).Width/2,yPos);
				//Practice Address----------------------------------------------------------------------
				font=new Font("Arial",10);
				yPos=50;
				xPos=30;
				g.DrawString(Prefs.GetString("PracticeTitle"),font,brush,xPos,yPos);
				yPos+=18;
				g.DrawString(Prefs.GetString("PracticeAddress"),font,brush,xPos,yPos);	    
				yPos+=18;
				if(Prefs.GetString("PracticeAddress2")!=""){
					g.DrawString(Prefs.GetString("PracticeAddress2"),font,brush,xPos,yPos);	    
					yPos+=18;
				}
				g.DrawString(Prefs.GetString("PracticeCity")+", "+Prefs.GetString("PracticeST")+" "
					+Prefs.GetString("PracticeZip"),font,brush,xPos,yPos);
				yPos+=18;
				text=Prefs.GetString("PracticePhone");
				if(text.Length==10)
					text="("+text.Substring(0,3)+")"+text.Substring(3,3)+"-"+text.Substring(6);
				g.DrawString(text,font,brush,xPos,yPos);
				yPos+=18;
				//AMOUNT ENCLOSED------------------------------------------------------------------------
				if(!HidePayment && !SubtotalsOnly){
					yPos=110;
					xPos=450;
					width=110;//width of an individual cell
					height=14;
					float hCell=20;
					brush=Brushes.LightGray;
					pen=new Pen(Color.Black);
					Pen peng=new Pen(Color.Gray);
					for(int i=0;i<3;i++){
						g.FillRectangle(brush,xPos+width*i,yPos,width,height);
						g.DrawRectangle(pen,xPos+width*i,yPos,width,height);
						g.DrawLine(peng,xPos+width*i,yPos+height+hCell,xPos+width*(i+1),yPos+height+hCell);//horiz
						g.DrawLine(peng,xPos+width*i,yPos+height,xPos+width*i,yPos+height+hCell);//vert
					}
					g.DrawLine(peng,xPos+width*3,yPos+height,xPos+width*3,yPos+height+hCell);//vert
					font=new Font("Arial",8,FontStyle.Bold);
					brush=Brushes.Black;
					text=Lan.g(this,"Amount Due");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					text=Lan.g(this,"Date Due");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					text=Lan.g(this,"Amount Enclosed");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos=450;
					yPos+=height+3;
					font=new Font("Arial",9);
					text=(PatGuar.BalTotal-PatGuar.InsEst).ToString("F");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					if(Prefs.GetInt("StatementsCalcDueDate")==-1){
						text=Lan.g(this,"Upon Receipt");
					}
					else{
						text=DateTime.Today.AddDays(Prefs.GetInt("StatementsCalcDueDate")).ToShortDateString();
					}
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
				}
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
				font=new Font("Arial",11);
				brush=Brushes.Black;
				yPos=225 + 1;//+10
				xPos=62.5F+12.5F;
				g.DrawString(PatGuar.GetNameFL(),font,brush,xPos,yPos);
				yPos+=19;
				g.DrawString(PatGuar.Address,font,brush,xPos,yPos);	    
				yPos+=19;
				if(PatGuar.Address2!=""){
					g.DrawString(PatGuar.Address2,font,brush,xPos,yPos);	    
					yPos+=19;  
				}
   			g.DrawString(PatGuar.City+", "+PatGuar.State+" "+PatGuar.Zip,font,brush,xPos,yPos);
				//Credit Card Info-----------------------------------------------------------------------
				if(!HidePayment){
					if(Prefs.GetBool("StatementShowCreditCard")){
						xPos=450;
						yPos=175;
						font=new Font("Arial",7,FontStyle.Bold);
						brush=Brushes.Black;
						pen=new Pen(Color.Black);
						text=Lan.g(this,"CREDIT CARD TYPE");
						rowHeight=30;
						g.DrawString(text,font,brush,xPos,yPos);
						g.DrawLine(pen,xPos+(g.MeasureString(text,font)).Width,
							yPos+font.GetHeight(g),776,yPos+font.GetHeight(g));
						yPos+=rowHeight;
						text=Lan.g(this,"#");
						g.DrawString(text,font,brush,xPos,yPos); 
						g.DrawLine(pen,xPos+(g.MeasureString(text,font)).Width,
							yPos+font.GetHeight(g),776,yPos+font.GetHeight(g)); 
						yPos+=rowHeight;
						text=Lan.g(this,"EXPIRES");
						g.DrawString(text,font,brush,xPos,yPos); 
						g.DrawLine(pen,xPos+(g.MeasureString(text,font)).Width,
							yPos+font.GetHeight(g),776,yPos+font.GetHeight(g)); 
						yPos+=rowHeight;
						text=Lan.g(this,"AMOUNT APPROVED");
						g.DrawString(text,font,brush,xPos,yPos); 
						g.DrawLine(pen,xPos+(g.MeasureString(text,font)).Width,
							yPos+font.GetHeight(g),776,yPos+font.GetHeight(g));
						yPos+=rowHeight;
						text=Lan.g(this,"NAME");
						g.DrawString(text,font,brush,xPos,yPos); 
						g.DrawLine(pen,xPos+(g.MeasureString(text,font)).Width,
							yPos+font.GetHeight(g),776,yPos+font.GetHeight(g));
						yPos+=rowHeight;
						text=Lan.g(this,"SIGNATURE");
						g.DrawString(text,font,brush,xPos,yPos); 
						g.DrawLine(pen,xPos+(g.MeasureString(text,font)).Width,
							yPos+font.GetHeight(g),776,yPos+font.GetHeight(g));
						yPos-=rowHeight;
						text=Lan.g(this,"(As it appears on card)");
						font=new Font("Arial",5);
						g.DrawString(text,font,brush,625-g.MeasureString(text,font).Width/2+5,yPos+13);
					}
				}
				//perforated line------------------------------------------------------------------
				if(!HidePayment){
					pen=new Pen(Brushes.Black);
					pen.Width=(float).125;
					pen.DashStyle=DashStyle.Dot;
					yPos=350;//3.62 inches from top, 1/3 page down
					g.DrawLine(pen,0,yPos,ev.PageBounds.Width,yPos);
 					text=Lan.g(this,"PLEASE DETACH AND RETURN THE UPPER PORTION WITH YOUR PAYMENT");
					yPos+=5;
					font=new Font("Arial",6,FontStyle.Bold);
					xPos=425-g.MeasureString(text,font).Width/2;
					brush=Brushes.Gray;
					g.DrawString(text,font,brush,xPos,yPos);
				}
				//Aging-----------------------------------------------------------------------------------
				if(!HidePayment && !SubtotalsOnly){
					yPos=350+25;
					xPos=160;
					width=70;//width of an individual cell
					height=14;
					float hCell=20;
					brush=Brushes.LightGray;
					pen=new Pen(Color.Black);
					Pen peng=new Pen(Color.Gray);
					for(int i=0;i<7;i++){
						g.FillRectangle(brush,xPos+width*i,yPos,width,height);
						g.DrawRectangle(pen,xPos+width*i,yPos,width,height);
						g.DrawLine(peng,xPos+width*i,yPos+height+hCell,xPos+width*(i+1),yPos+height+hCell);//horiz
						g.DrawLine(peng,xPos+width*i,yPos+height,xPos+width*i,yPos+height+hCell);//vert
					}
					g.DrawLine(peng,xPos+width*7,yPos+height,xPos+width*7,yPos+height+hCell);//vert
					font=new Font("Arial",8,FontStyle.Bold);
					brush=Brushes.Black;
					text=Lan.g(this,"0-30");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					text=Lan.g(this,"31-60");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					text=Lan.g(this,"61-90");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					text=Lan.g(this,"over 90");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					text=Lan.g(this,"Total");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					text=Lan.g(this,"- InsEst");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					text=Lan.g(this,"= Balance");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos=160;
					font=new Font("Arial",9);
					yPos+=height+3;
					text=PatGuar.Bal_0_30.ToString("F");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					text=PatGuar.Bal_31_60.ToString("F");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					text=PatGuar.Bal_61_90.ToString("F");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					text=PatGuar.BalOver90.ToString("F");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					text=PatGuar.BalTotal.ToString("F");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					text=PatGuar.InsEst.ToString("F");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
					xPos+=width;
					text=(PatGuar.BalTotal-PatGuar.InsEst).ToString("F");
					g.DrawString(text,font,brush,xPos+width/2-g.MeasureString(text,font).Width/2,yPos);
				}
				yPos=350+68;
				//yPos=770;//change this value to test multiple pages
			}//if(pagesPrinted==0){
			else {
				yPos=18;
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
			ColCaption[6]=Lan.g(this,"Ins Pd");
      ColCaption[7]=Lan.g(this,"Patient");
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
				&& linesPrinted<StatementA[famsPrinted].GetLength(1))
			{
				if(StatementA[famsPrinted][11,linesPrinted]=="PatName"){
					//Patient Name-------------------------------------------------------------------------
					//if(there is not room for at least a few rows){
						//break
					//}
					g.DrawString(StatementA[famsPrinted][3,linesPrinted],NameFont,Brushes.Black,colPos[0],yPos);
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
				else if(StatementA[famsPrinted][11,linesPrinted]=="PatTotal"){
					//Totals--------------------------------------------------------------------------------
					for(int iCol=3;iCol<11;iCol++){
						g.DrawString(Lan.g(this,StatementA[famsPrinted][iCol,linesPrinted])
							,TotalFont,Brushes.Black,new RectangleF(
							colPos[iCol+1]
							-g.MeasureString(StatementA[famsPrinted][iCol,linesPrinted],TotalFont).Width-1,yPos
							,colPos[iCol+1]-colPos[iCol]+8,TotalFont.GetHeight(g)));
					}
					yPos+=TotalFont.GetHeight(g);
				}
				else{
					//Body data--------------------------------------------------------------------------------
					if(isFirstLineOnPage){
						//g.DrawLine(new Pen(Color.Gray),colPos[0],yPos,colPos[11],yPos);
					}
					//description column determines height of row
					rowHeight=g.MeasureString(StatementA[famsPrinted][3,linesPrinted],bodyFont,colPos[3+1]-colPos[3]+6).Height;
						//bodyFont.GetHeight(g);
					for(int i=0;i<11;i++){
						//left line for this cell
						g.DrawLine(new Pen(Color.Gray),colPos[i],yPos,colPos[i],yPos+rowHeight);
						if(i==10){//if this is the right column, then also draw line for right side of cell
							g.DrawLine(new Pen(Color.Gray),colPos[i+1],yPos,colPos[i+1],yPos+rowHeight);
						}
						//bottom line for this cell
						g.DrawLine(new Pen(Color.LightGray),colPos[i],yPos+rowHeight,colPos[i+1],yPos+rowHeight);
						//if new date, then print dark line above
						if(linesPrinted>0 && StatementA[famsPrinted][0,linesPrinted] != StatementA[famsPrinted][0,linesPrinted-1]){
							g.DrawLine(new Pen(Color.Black,1.5f),colPos[i],yPos,colPos[i+1],yPos);
						}
						if(colAlign[i]==HorizontalAlignment.Right){
							g.DrawString(StatementA[famsPrinted][i,linesPrinted]
								,bodyFont,Brushes.Black,new RectangleF(
								colPos[i+1]-g.MeasureString(StatementA[famsPrinted][i,linesPrinted],bodyFont).Width+1,//x
								yPos,//y
								colPos[i+1]-colPos[i]+8,//w
								rowHeight));//h
						}
						else{
							g.DrawString(StatementA[famsPrinted][i,linesPrinted]
								,bodyFont,Brushes.Black,new RectangleF(
								colPos[i],yPos
								,colPos[i+1]-colPos[i]+6,rowHeight));
						}
						if(StatementA[famsPrinted][11,linesPrinted+1]=="PatTotal"){
							g.DrawLine(new Pen(Color.Gray),colPos[i],yPos+rowHeight,colPos[11],yPos+rowHeight);
						}
					}
					yPos+=rowHeight;
				}
				isFirstLineOnPage=false;
				linesPrinted++;
				//if(linesPrinted<StatementA[famsPrinted].GetLength(1)
				//	&& StatementA[famsPrinted][11,linesPrinted]=="GrandTotal")
				//{
 				//	linesPrinted++;
				//}
			}//end while lines
			#endregion
			#region Note
			//Note----------------------------------------------------------------------------------------
			if(!notePrinted && //if note has not printed
				linesPrinted==StatementA[famsPrinted].GetLength(1))//and all table data already printed
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
			if(linesPrinted<StatementA[famsPrinted].GetLength(1)){//if this family is not done printing
				ev.HasMorePages=true;
				pagesPrinted++;
				totalPages++;
			}
			else{//family is done printing
				pagesPrinted=0;
				linesPrinted=0;
				totalPages++;
				famsPrinted++;
				if(famsPrinted<StatementA.GetLength(0)){//if more families to print
					ev.HasMorePages=true;
				}
				else{//completely done
					ev.HasMorePages=false;
					labelTotPages.Text="1 / "+totalPages.ToString();	
					famsPrinted=0;
					pagesPrinted=0;
				}
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
			//just for debugging
			/*PrintReport(false);
			DialogResult=DialogResult.Cancel;*/			
		}

		private void butFullPage_Click(object sender, System.EventArgs e) {
			butFullPage.Visible=false;
			butZoomIn.Visible=true;
			printPreviewControl2.Zoom=((double)printPreviewControl2.ClientSize.Height
				/(double)pd2.DefaultPageSettings.PaperSize.Height);	
		}
	}
}
