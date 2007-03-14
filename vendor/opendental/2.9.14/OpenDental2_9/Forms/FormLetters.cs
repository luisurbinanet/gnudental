using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using OpenDental.Reporting;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormLetters : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private OpenDental.XPButton butAdd;
		private System.Windows.Forms.ListBox listLetters;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkIncludeRet;
		private OpenDental.XPButton butEdit;
		private OpenDental.XPButton butDelete;
		private System.Windows.Forms.Button butCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textBody;
		private bool localChanged;
		private System.Drawing.Printing.PrintDocument pd2;
		private System.Windows.Forms.PrintDialog printDialog2;
		private bool bodyChanged;
		private OpenDental.XPButton butPrint;
		private int pagesPrinted=0;

		///<summary></summary>
		public FormLetters()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.C("All", new System.Windows.Forms.Control[] {
				butCancel
			});
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormLetters));
			this.butCancel = new System.Windows.Forms.Button();
			this.listLetters = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butEdit = new OpenDental.XPButton();
			this.butAdd = new OpenDental.XPButton();
			this.label2 = new System.Windows.Forms.Label();
			this.textBody = new System.Windows.Forms.TextBox();
			this.checkIncludeRet = new System.Windows.Forms.CheckBox();
			this.butDelete = new OpenDental.XPButton();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.printDialog2 = new System.Windows.Forms.PrintDialog();
			this.butPrint = new OpenDental.XPButton();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(758, 633);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(79, 26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// listLetters
			// 
			this.listLetters.Location = new System.Drawing.Point(20, 133);
			this.listLetters.Name = "listLetters";
			this.listLetters.Size = new System.Drawing.Size(164, 277);
			this.listLetters.TabIndex = 2;
			this.listLetters.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listLetters_MouseDown);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(19, 114);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(124, 14);
			this.label1.TabIndex = 3;
			this.label1.Text = "Letters";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butEdit
			// 
			this.butEdit.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEdit.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butEdit.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butEdit.Image = ((System.Drawing.Image)(resources.GetObject("butEdit.Image")));
			this.butEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butEdit.Location = new System.Drawing.Point(106, 414);
			this.butEdit.Name = "butEdit";
			this.butEdit.Size = new System.Drawing.Size(79, 26);
			this.butEdit.TabIndex = 8;
			this.butEdit.Text = "&Edit";
			this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(19, 414);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(79, 26);
			this.butAdd.TabIndex = 7;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(22, 12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(167, 86);
			this.label2.TabIndex = 12;
			this.label2.Text = "This creates a letter for a single patient.  For complex letters to multiple pati" +
				"ents, export data from a report and merge it with a Word or OpenOffice template." +
				"";
			// 
			// textBody
			// 
			this.textBody.AcceptsReturn = true;
			this.textBody.Location = new System.Drawing.Point(206, 25);
			this.textBody.Multiline = true;
			this.textBody.Name = "textBody";
			this.textBody.Size = new System.Drawing.Size(630, 597);
			this.textBody.TabIndex = 13;
			this.textBody.Text = "";
			this.textBody.TextChanged += new System.EventHandler(this.textBody_TextChanged);
			// 
			// checkIncludeRet
			// 
			this.checkIncludeRet.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIncludeRet.Location = new System.Drawing.Point(206, 1);
			this.checkIncludeRet.Name = "checkIncludeRet";
			this.checkIncludeRet.Size = new System.Drawing.Size(193, 24);
			this.checkIncludeRet.TabIndex = 15;
			this.checkIncludeRet.Text = "Include Return Address";
			this.checkIncludeRet.Click += new System.EventHandler(this.checkIncludeRet_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(19, 448);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(79, 26);
			this.butDelete.TabIndex = 16;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPrint.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butPrint.Image = ((System.Drawing.Image)(resources.GetObject("butPrint.Image")));
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(654, 633);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(79, 26);
			this.butPrint.TabIndex = 17;
			this.butPrint.Text = "&Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// FormLetters
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(858, 674);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.checkIncludeRet);
			this.Controls.Add(this.textBody);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butEdit);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listLetters);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormLetters";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Letters";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormLetterSetup_Closing);
			this.Load += new System.EventHandler(this.FormLetterSetup_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormLetterSetup_Load(object sender, System.EventArgs e) {
			if(((Pref)Prefs.HList["LettersIncludeReturnAddress"]).ValueString=="1")
				checkIncludeRet.Checked=true;
			FillList();
		}

		private void FillList(){
			Letters.Refresh();
			listLetters.Items.Clear();
			for(int i=0;i<Letters.List.Length;i++){
				listLetters.Items.Add(Letters.List[i].Description);
			}
			//no items are initially selected
		}

		private void listLetters_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listLetters.SelectedIndex==-1){
				return;
			}
			if(!WarnOK())
				return;
			Letters.Cur=Letters.List[listLetters.SelectedIndex];
			StringBuilder str=new StringBuilder();
			//return address
			if(checkIncludeRet.Checked){
				str.Append(((Pref)Prefs.HList["PracticeTitle"]).ValueString+"\r\n");
				str.Append(((Pref)Prefs.HList["PracticeAddress"]).ValueString+"\r\n");
				if(((Pref)Prefs.HList["PracticeAddress2"]).ValueString!="")
					str.Append(((Pref)Prefs.HList["PracticeAddress2"]).ValueString+"\r\n");
				str.Append(((Pref)Prefs.HList["PracticeCity"]).ValueString+", ");
				str.Append(((Pref)Prefs.HList["PracticeST"]).ValueString+"  ");
				str.Append(((Pref)Prefs.HList["PracticeZip"]).ValueString+"\r\n");
			}
			else{
				str.Append("\r\n\r\n\r\n\r\n");
			}
			str.Append("\r\n\r\n");
			//address
			str.Append(Patients.Cur.FName+" "+Patients.Cur.MiddleI+" "+Patients.Cur.LName+"\r\n");
			str.Append(Patients.Cur.Address+"\r\n");
			if(Patients.Cur.Address2!="")
				str.Append(Patients.Cur.Address2+"\r\n");
			str.Append(Patients.Cur.City+", "+Patients.Cur.State+"  "+Patients.Cur.Zip);
			str.Append("\r\n\r\n\r\n\r\n");
			//date
			str.Append(DateTime.Today.ToShortDateString()+"\r\n\r\n");
			//greeting
			str.Append("Dear ");
			if(Patients.Cur.Salutation!="")
				str.Append(Patients.Cur.Salutation);
			else if(Patients.Cur.Preferred!="")
				str.Append(Patients.Cur.Preferred);
			else
				str.Append(Patients.Cur.FName);
			str.Append(":\r\n\r\n");
			//body text
			str.Append(Letters.Cur.BodyText);
			//closing
			str.Append("\r\n\r\nSincerely,\r\n\r\n\r\n\r\n");
			str.Append(((Pref)Prefs.HList["PracticeTitle"]).ValueString);
			textBody.Text=str.ToString();
			bodyChanged=false;
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			if(!WarnOK())
				return;
			Letters.Cur=new Letter();
			FormLetterEdit FormLE=new FormLetterEdit();
			FormLE.IsNew=true;
			FormLE.ShowDialog();
			FillList();
		}

		private void butEdit_Click(object sender, System.EventArgs e) {
			if(!WarnOK())
				return;
			if(listLetters.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			Letters.Cur=Letters.List[listLetters.SelectedIndex];//just in case
      FormLetterEdit FormLE=new FormLetterEdit();
			FormLE.ShowDialog();
			FillList();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(listLetters.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete letter permanently for all patients?"),"",MessageBoxButtons.OKCancel)
				!=DialogResult.OK){
				return;
			}
			Letters.Cur=Letters.List[listLetters.SelectedIndex];
			Letters.DeleteCur();
			FillList();
		}

		private void checkIncludeRet_Click(object sender, System.EventArgs e) {	
			Prefs.Cur=(Pref)Prefs.HList["LettersIncludeReturnAddress"];
			if(checkIncludeRet.Checked)
				Prefs.Cur.ValueString="1";
			else
				Prefs.Cur.ValueString="0";
			Prefs.UpdateCur();
			localChanged=true;
			Prefs.Refresh();
		}

		///<summary>If the user has selected a letter, and then edited it in the main textbox, this warns them before continuing.</summary>
		private bool WarnOK(){
			if(bodyChanged){
				if(MessageBox.Show(Lan.g(this
					,"Any changes you made to the letter you were working on will be lost.  "
					+"Do you wish to continue?"),"",MessageBoxButtons.OKCancel)
					!=DialogResult.OK)
				{
					return false;
				}
			}
			return true;
		}

		private void textBody_TextChanged(object sender, System.EventArgs e) {
			bodyChanged=true;
		}

		private void butPrint_Click(object sender, System.EventArgs e) {
			pd2=new PrintDocument();
			pd2.PrintPage+=new PrintPageEventHandler(this.pd2_PrintPage);
			pd2.OriginAtMargins=true;
			printDialog2=new PrintDialog();
			printDialog2.PrinterSettings=new PrinterSettings();
			printDialog2.PrinterSettings.PrinterName=Computers.Cur.PrinterName;
			if(printDialog2.ShowDialog()!=DialogResult.OK){
				return;
			}
			if(printDialog2.PrinterSettings.IsValid){
				//pd2.PrinterSettings.PrinterName=;
				pd2.PrinterSettings=printDialog2.PrinterSettings;
			}
			//uses default printer if selected printer not valid
			try{
				pd2.Print();
			}
			catch{
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
			Commlogs.Cur=new Commlog();
			Commlogs.Cur.CommDate=DateTime.Today;
			Commlogs.Cur.CommType=CommItemType.LetterSent;
			Commlogs.Cur.PatNum=Patients.Cur.PatNum;
			Commlogs.Cur.Note=Letters.Cur.Description+". ";
			FormCommItem FormCI=new FormCommItem();
			FormCI.IsNew=true;
			FormCI.ShowDialog();
			//this window now closes regardless of whether the user saved the comm item.
			DialogResult=DialogResult.OK;
		}

		private void pd2_PrintPage(object sender, PrintPageEventArgs ev){//raised for each page to be printed.
			Graphics grfx=ev.Graphics;
			ev.PageSettings.Margins=new Margins(100,100,80,80);
			//ev.PageSettings.PrinterSettings.
			//ev.
			//grfx.TextRenderingHint=TextRenderingHint.
			//StringFormat format=new StringFormat();
			//format..FormatFlags=StringFormatFlags.
			grfx.DrawString(textBody.Text,new Font(FontFamily.GenericSansSerif,11),Brushes.Black
				,new RectangleF(0,0,ev.MarginBounds.Width,ev.MarginBounds.Height));
			pagesPrinted++;
			ev.HasMorePages=false;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormLetterSetup_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(localChanged){
				DataValid.IType=InvalidType.LocalData;
				DataValid DataValid2=new DataValid();
				DataValid2.SetInvalid();
			}
		}


		

		

		

		

		

		

		

		


	}
}





















