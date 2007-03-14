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
	public class FormRxEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.TextBox textSig;
		private System.Windows.Forms.TextBox textDisp;
		private System.Windows.Forms.TextBox textRefills;
		private System.Windows.Forms.TextBox textDrug;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private OpenDental.ValidDate textDate;
		private System.ComponentModel.Container components = null;// Required designer variable.
		private System.Windows.Forms.ListBox listProv;
		private System.Windows.Forms.Label label7;
		private System.Drawing.Printing.PrintDocument pd2;
		///<summary></summary>
		public bool IsNew;
		private OpenDental.UI.Button butPrint;
		private OpenDental.UI.Button butDelete;
		private OpenDental.ODtextBox textNotes;
		///<summary></summary>
    public FormRpPrintPreview pView = new FormRpPrintPreview();
		private Patient PatCur;
		private User user;

		///<summary></summary>
		public FormRxEdit(Patient patCur){//RxPat rxPatCur){
			InitializeComponent();
			//RxPatCur=rxPatCur;
			PatCur=patCur;
			Lan.F(this);
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormRxEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.textSig = new System.Windows.Forms.TextBox();
			this.textDisp = new System.Windows.Forms.TextBox();
			this.textRefills = new System.Windows.Forms.TextBox();
			this.textDrug = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textDate = new OpenDental.ValidDate();
			this.listProv = new System.Windows.Forms.ListBox();
			this.label7 = new System.Windows.Forms.Label();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.butPrint = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.textNotes = new OpenDental.ODtextBox();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(618, 424);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 9;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(618, 384);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 8;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textSig
			// 
			this.textSig.AcceptsReturn = true;
			this.textSig.Location = new System.Drawing.Point(110, 104);
			this.textSig.Multiline = true;
			this.textSig.Name = "textSig";
			this.textSig.Size = new System.Drawing.Size(254, 44);
			this.textSig.TabIndex = 2;
			this.textSig.Text = "";
			// 
			// textDisp
			// 
			this.textDisp.Location = new System.Drawing.Point(110, 160);
			this.textDisp.Name = "textDisp";
			this.textDisp.Size = new System.Drawing.Size(112, 20);
			this.textDisp.TabIndex = 3;
			this.textDisp.Text = "";
			// 
			// textRefills
			// 
			this.textRefills.Location = new System.Drawing.Point(110, 194);
			this.textRefills.Name = "textRefills";
			this.textRefills.Size = new System.Drawing.Size(114, 20);
			this.textRefills.TabIndex = 4;
			this.textRefills.Text = "";
			// 
			// textDrug
			// 
			this.textDrug.Location = new System.Drawing.Point(110, 72);
			this.textDrug.Name = "textDrug";
			this.textDrug.Size = new System.Drawing.Size(254, 20);
			this.textDrug.TabIndex = 1;
			this.textDrug.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(17, 108);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(89, 14);
			this.label6.TabIndex = 17;
			this.label6.Text = "Sig";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(7, 164);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(99, 14);
			this.label5.TabIndex = 16;
			this.label5.Text = "Disp";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(7, 198);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(99, 14);
			this.label4.TabIndex = 15;
			this.label4.Text = "Refills";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(11, 232);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(95, 14);
			this.label3.TabIndex = 14;
			this.label3.Text = "Notes";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(13, 74);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(93, 14);
			this.label1.TabIndex = 13;
			this.label1.Text = "Drug";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(3, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(105, 14);
			this.label2.TabIndex = 25;
			this.label2.Text = "Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(110, 40);
			this.textDate.Name = "textDate";
			this.textDate.TabIndex = 0;
			this.textDate.Text = "";
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(524, 28);
			this.listProv.Name = "listProv";
			this.listProv.Size = new System.Drawing.Size(120, 212);
			this.listProv.TabIndex = 6;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(522, 10);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(103, 14);
			this.label7.TabIndex = 28;
			this.label7.Text = "Provider";
			// 
			// pd2
			// 
			this.pd2.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pd2_PrintPage);
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.Image = ((System.Drawing.Image)(resources.GetObject("butPrint.Image")));
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(482, 424);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(88, 26);
			this.butPrint.TabIndex = 29;
			this.butPrint.Text = "&Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(38, 424);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(88, 26);
			this.butDelete.TabIndex = 30;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// textNotes
			// 
			this.textNotes.AcceptsReturn = true;
			this.textNotes.Location = new System.Drawing.Point(110, 231);
			this.textNotes.Multiline = true;
			this.textNotes.Name = "textNotes";
			this.textNotes.QuickPasteType = OpenDental.QuickPasteType.Rx;
			this.textNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNotes.Size = new System.Drawing.Size(373, 111);
			this.textNotes.TabIndex = 31;
			this.textNotes.Text = "";
			// 
			// FormRxEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(710, 472);
			this.Controls.Add(this.textNotes);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.textSig);
			this.Controls.Add(this.textDisp);
			this.Controls.Add(this.textRefills);
			this.Controls.Add(this.textDrug);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRxEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Rx";
			this.Load += new System.EventHandler(this.FormRxEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRxEdit_Load(object sender, System.EventArgs e) {
			//Rx create is handled at the button click rather than here
			if(!IsNew){
				if(Permissions.AuthorizationRequired("Prescription Edit",RxPats.Cur.RxDate)){
					user=Users.Authenticate("Prescription Edit");
					if(user==null){
						DialogResult=DialogResult.Cancel;
						return;
					}
					if(!UserPermissions.IsAuthorized("Prescription Edit",user)){
						butOK.Enabled=false;
						butPrint.Enabled=false;
					}
				}
			}
			for(int i=0;i<Providers.List.Length;i++){
				this.listProv.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==RxPats.Cur.ProvNum)
					listProv.SelectedIndex=i;
			}
			if(listProv.SelectedIndex==-1){
				listProv.SelectedIndex=0;
			}
			textDate.Text=RxPats.Cur.RxDate.ToString("d");
			textDrug.Text=RxPats.Cur.Drug;
			textSig.Text=RxPats.Cur.Sig;
			textDisp.Text=RxPats.Cur.Disp;
			textRefills.Text=RxPats.Cur.Refills;
			textNotes.Text=RxPats.Cur.Notes;
		}

		private bool SaveRx(){
			if(  textDate.errorProvider1.GetError(textDate)!=""
				//|| textAmount.errorProvider1.GetError(textAmount)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return false;
			}
			if(listProv.SelectedIndex!=-1)
				RxPats.Cur.ProvNum=Providers.List[listProv.SelectedIndex].ProvNum;
			RxPats.Cur.RxDate=PIn.PDate(textDate.Text);
			RxPats.Cur.Drug=textDrug.Text;
			RxPats.Cur.Sig=textSig.Text;
			RxPats.Cur.Disp=textDisp.Text;
			RxPats.Cur.Refills=textRefills.Text;
			RxPats.Cur.Notes=textNotes.Text;
			if(IsNew){
				RxPats.InsertCur();
				SecurityLogs.MakeLogEntry("Prescription Create",RxPats.cmd.CommandText,user);
			}
			else{
				RxPats.UpdateCur();
				SecurityLogs.MakeLogEntry("Prescription Edit",RxPats.cmd.CommandText,user);
			}
			return true;
		}

		///<summary>justPreview only used in debugging</summary>
		public void PrintReport(bool justPreview){
			pd2=new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			pd2.DefaultPageSettings.Margins=new Margins(10,40,40,60);
			if(justPreview){
				pView.printPreviewControl2.Document=pd2;
				pView.ShowDialog();
			}
			else{
				if(!Printers.SetPrinter(pd2,PrintSituation.Rx)){
					return;
				}
				try{
					pd2.Print();
				}
				catch{
					MessageBox.Show(Lan.g(this,"Printer not available"));
				}
			}			
		}

		private void pd2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
			//Draw borders to cut			
      Pen penBorder=new Pen(Color.Black,(float)(.125));
      penBorder.DashStyle=DashStyle.Dot;
      e.Graphics.DrawLine(penBorder,512,0,512,400); 
      e.Graphics.DrawLine(penBorder,0,400,512,400); 
			//Print String "Please Cut Here"
			Font cutFont=new Font("Arial",6,FontStyle.Bold);
      string cut=Lan.g(this,"PLEASE CUT HERE");
      StringFormat cutFormat=new StringFormat(StringFormatFlags.DirectionVertical);  
			e.Graphics.DrawString(cut,cutFont,Brushes.Gray
				,512,(400/2)-(e.Graphics.MeasureString(cut,cutFont).Width/2),cutFormat);
  		e.Graphics.DrawString(cut,cutFont,Brushes.Gray
				,(512/2)-(e.Graphics.MeasureString(cut,cutFont).Width/2),400);
			//Heading Info  Margins are x=25,y=37.5 (note to self system adds 25 to x and 12.5 to y
			//Left Side
			Font headingFont=new Font("Arial",8,FontStyle.Bold);
			int xPos=25;
			int yPos=37;
			int fontH=(int)headingFont.GetHeight(e.Graphics);
	    string pracTitle=((Pref)Prefs.HList["PracticeTitle"]).ValueString;
			string pracAddress=((Pref)Prefs.HList["PracticeAddress"]).ValueString;
      string pracAddress2=((Pref)Prefs.HList["PracticeAddress2"]).ValueString;
			string pracCSZ=((Pref)Prefs.HList["PracticeCity"]).ValueString+", "
				+((Pref)Prefs.HList["PracticeST"]).ValueString+" "
				+((Pref)Prefs.HList["PracticeZip"]).ValueString;
			e.Graphics.DrawString(pracTitle,headingFont,Brushes.Black,xPos,yPos);
			yPos+=fontH;
			e.Graphics.DrawString(pracAddress,headingFont,Brushes.Black,xPos,yPos);
      yPos+=fontH;
			if(pracAddress2==""){
        e.Graphics.DrawString(pracCSZ,headingFont,Brushes.Black,xPos,yPos);
  		}
			else{
 			  e.Graphics.DrawString(pracAddress2,headingFont,Brushes.Black,xPos,yPos);
			  yPos+=fontH;
			  e.Graphics.DrawString(pracCSZ,headingFont,Brushes.Black,xPos,yPos);     
  		}
      yPos=100;
      e.Graphics.DrawLine(new Pen(Color.Black),xPos,yPos,512-25,yPos); 
			//Right Side
      Provider curProv=new Provider();
			for(int i=0;i<Providers.ListLong.Length;i++){
        if(RxPats.Cur.ProvNum==Providers.ListLong[i].ProvNum)
					curProv=Providers.ListLong[i];
		  }
			string presName="";
			if(curProv.MI!="")
  			presName=curProv.FName+" "+curProv.MI+ " "+curProv.LName;
      else
		    presName=curProv.FName+" "+curProv.LName;
			string pracPhone=((Pref)Prefs.HList["PracticePhone"]).ValueString;
			string presPhone="";
			if(pracPhone.Length==10)
				presPhone="("+pracPhone.Substring(0,3)+")"+pracPhone.Substring(3,3)+"-"
					+pracPhone.Substring(6);
      string presDEA=curProv.DEANum;
			string nameTitle=Lan.g(this,"PRESCRIBER:")+"  "+presName;
      string phoneTitle=Lan.g(this,"TELEPHONE:")+"  "+presPhone;
      string deaTitle=Lan.g(this,"DEA NO:")+"  "+presDEA;
			xPos=270;
			yPos=38;
		  e.Graphics.DrawString(nameTitle,headingFont,Brushes.Black,xPos,yPos);
			yPos+=fontH;
		  e.Graphics.DrawString(phoneTitle,headingFont,Brushes.Black,xPos,yPos);
			yPos+=fontH;
		  e.Graphics.DrawString(deaTitle,headingFont,Brushes.Black,xPos,yPos);
			//Print Body Section
			//Upper Left
			Font bodyFont=new Font("Arial",9,FontStyle.Regular);			
  		string patName;
			if(PatCur.MiddleI!="")
        patName=PatCur.FName+" "+PatCur.MiddleI+" "+PatCur.LName;
      else  
        patName=PatCur.FName+" "+PatCur.LName;
			string patAddress=PatCur.Address;
			string patAddress2=PatCur.Address2;
	    string patCSZ=PatCur.City+", "+PatCur.State+" "+PatCur.Zip;
			fontH=(int)bodyFont.GetHeight(e.Graphics);
			xPos=25;
			yPos=120;
			e.Graphics.DrawString(Lan.g(this,"PATIENT:"),bodyFont,Brushes.Black,xPos,yPos);
			yPos+=fontH;
  		e.Graphics.DrawString(Lan.g(this,"ADDRESS:"),bodyFont,Brushes.Black,xPos,yPos);
      yPos=120;
			xPos=100;
      e.Graphics.DrawString(patName,bodyFont,Brushes.Black,xPos,yPos);
			yPos+=fontH;
			e.Graphics.DrawString(patAddress,bodyFont,Brushes.Black,xPos,yPos);
			yPos+=fontH;
			if(patAddress2==""){
        e.Graphics.DrawString(patCSZ,bodyFont,Brushes.Black,xPos,yPos);
  		}
			else{
 			  e.Graphics.DrawString(patAddress2,bodyFont,Brushes.Black,xPos,yPos);
			  yPos+=fontH;
			  e.Graphics.DrawString(patCSZ,bodyFont,Brushes.Black,xPos,yPos);     
  		}
			//Phone and date
			string patPhone=PatCur.HmPhone;
			string patDOB=PatCur.Birthdate.ToShortDateString();
			string rxDate=RxPats.Cur.RxDate.ToShortDateString();
			xPos=280;
			yPos=120;
      e.Graphics.DrawString(Lan.g(this,"TELEPHONE:"),bodyFont,Brushes.Black,xPos,yPos);
      yPos+=fontH;
      e.Graphics.DrawString(Lan.g(this,"DOB:"),bodyFont,Brushes.Black,xPos,yPos);
      yPos+=fontH;
			e.Graphics.DrawString(Lan.g(this,"DATE:"),bodyFont,Brushes.Black,xPos,yPos);
      yPos-=fontH*2;
			xPos=370;
			yPos=120;
      e.Graphics.DrawString(patPhone,bodyFont,Brushes.Black,xPos,yPos);
      yPos+=fontH;
      e.Graphics.DrawString(patDOB,bodyFont,Brushes.Black,xPos,yPos);
      yPos+=fontH;
			e.Graphics.DrawString(rxDate,bodyFont,Brushes.Black,xPos,yPos);
			//Print string Rx
      yPos=190;
			xPos=25;
			Font RxFont = new Font("Times New Roman",24,FontStyle.Regular);
			e.Graphics.DrawString(Lan.g(this,"Rx"),RxFont,Brushes.Black,xPos,yPos);
			yPos=205;
			xPos=100;
	    e.Graphics.DrawString(RxPats.Cur.Drug,bodyFont,Brushes.Black,xPos,yPos);
      yPos+=(int)(fontH*1.5);
	    e.Graphics.DrawString(Lan.g(this,"Disp:")+"  "+RxPats.Cur.Disp,bodyFont,Brushes.Black,xPos,yPos);
      yPos+=(int)(fontH*1.5);
		  e.Graphics.DrawString(Lan.g(this,"Sig:")+"  "+RxPats.Cur.Sig,bodyFont,Brushes.Black
				,new RectangleF(xPos,yPos,512-xPos-5,fontH*2));
      yPos+=(int)(fontH*2.5);
	    e.Graphics.DrawString(Lan.g(this,"Refills:")+"  "+RxPats.Cur.Refills,bodyFont,Brushes.Black,xPos,yPos);
			//Print Check Boxes
      xPos=25;
			yPos=400-62;
			e.Graphics.DrawRectangle(new Pen(Color.Black),xPos,yPos,12,12);
      yPos+=25;
			e.Graphics.DrawRectangle(new Pen(Color.Black),xPos,yPos,12,12);		
			//Print X in Generic Substitution Permitted
			e.Graphics.DrawLine(new Pen(Color.Black),xPos,yPos,xPos+12,yPos+12);
			e.Graphics.DrawLine(new Pen(Color.Black),xPos+12,yPos,xPos,yPos+12);	
			//Print Strings for checkboxes
			string check1="DISPENSE AS WRITTEN";
			string check2="GENERIC SUBSTITUTION PERMITTED";
			Font checkFont= new Font("Arial",6,FontStyle.Regular);
			yPos-=24;
			xPos=xPos+17;
	    e.Graphics.DrawString(Lan.g(this,check1),checkFont,Brushes.Black,xPos,yPos);
			yPos+=25;
	    e.Graphics.DrawString(Lan.g(this,check2),checkFont,Brushes.Black,xPos,yPos);		
			//Print Signature Line and Signature Text
			yPos=360;
			xPos=225;
			e.Graphics.DrawLine(new Pen(Color.Black),xPos,yPos,512-25,yPos);
			string sigLine=Lan.g(this,"SIGNATURE OF PRESCRIBER");
			xPos=344-(int)(e.Graphics.MeasureString(sigLine,checkFont).Width/2);
		  e.Graphics.DrawString(sigLine,checkFont,Brushes.Black,xPos,yPos+4);
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete Prescription?"),"",MessageBoxButtons.OKCancel)
				!=DialogResult.OK){
				return;
			}
			RxPats.DeleteCur();
			DialogResult=DialogResult.OK;	
		}

		private void butPrint_Click(object sender, System.EventArgs e) {
			if(SaveRx()){
				PrintReport(false);
				DialogResult=DialogResult.OK;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(SaveRx())
				DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

	}
}
