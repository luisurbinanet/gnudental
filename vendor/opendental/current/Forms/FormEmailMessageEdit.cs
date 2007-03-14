using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
//using System.Web.Mail;
using System.Windows.Forms;
//using OpenDental.Reporting;
using Indy.Sockets.IndySMTP;
using Indy.Sockets.IndyMessage;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormEmailMessageEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		/// <summary>Required designer variable.</summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textSubject;
		private System.Windows.Forms.TextBox textToAddress;
		private System.Windows.Forms.TextBox textFromAddress;
		private OpenDental.UI.Button butSend;
		private System.Windows.Forms.TextBox textMsgDateTime;
		private OpenDental.UI.Button butDelete;
		private OpenDental.UI.Button butAdd;
		private System.Windows.Forms.Label label4;
		private OpenDental.ODtextBox textBodyText;
		private System.Windows.Forms.ListBox listTemplates;
		private OpenDental.UI.Button butInsert;
		private System.Windows.Forms.Label labelSent;
		//<summary></summary>
		//public bool IsNew;
		private bool templatesChanged;
		private System.Windows.Forms.Panel panelTemplates;
		private bool messageChanged;
		private int PatNum;

		///<summary></summary>
		public FormEmailMessageEdit(int patNum){
			InitializeComponent();// Required for Windows Form Designer support
			PatNum=patNum;
			Lan.F(this);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormEmailMessageEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butSend = new OpenDental.UI.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.textSubject = new System.Windows.Forms.TextBox();
			this.textToAddress = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textFromAddress = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textMsgDateTime = new System.Windows.Forms.TextBox();
			this.labelSent = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.listTemplates = new System.Windows.Forms.ListBox();
			this.textBodyText = new OpenDental.ODtextBox();
			this.butInsert = new OpenDental.UI.Button();
			this.panelTemplates = new System.Windows.Forms.Panel();
			this.panelTemplates.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(832, 629);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 25);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butSend
			// 
			this.butSend.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSend.Autosize = true;
			this.butSend.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSend.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSend.Location = new System.Drawing.Point(744, 629);
			this.butSend.Name = "butSend";
			this.butSend.Size = new System.Drawing.Size(75, 25);
			this.butSend.TabIndex = 2;
			this.butSend.Text = "&Send";
			this.butSend.Click += new System.EventHandler(this.butSend_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(196, 65);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(69, 14);
			this.label2.TabIndex = 3;
			this.label2.Text = "Subject:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSubject
			// 
			this.textSubject.Location = new System.Drawing.Point(265, 60);
			this.textSubject.Name = "textSubject";
			this.textSubject.Size = new System.Drawing.Size(641, 20);
			this.textSubject.TabIndex = 0;
			this.textSubject.Text = "";
			// 
			// textToAddress
			// 
			this.textToAddress.Location = new System.Drawing.Point(265, 40);
			this.textToAddress.Name = "textToAddress";
			this.textToAddress.Size = new System.Drawing.Size(641, 20);
			this.textToAddress.TabIndex = 8;
			this.textToAddress.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(192, 45);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 14);
			this.label1.TabIndex = 9;
			this.label1.Text = "To:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textFromAddress
			// 
			this.textFromAddress.Location = new System.Drawing.Point(265, 20);
			this.textFromAddress.Name = "textFromAddress";
			this.textFromAddress.Size = new System.Drawing.Size(641, 20);
			this.textFromAddress.TabIndex = 10;
			this.textFromAddress.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(192, 25);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(71, 14);
			this.label3.TabIndex = 11;
			this.label3.Text = "From:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textMsgDateTime
			// 
			this.textMsgDateTime.BackColor = System.Drawing.SystemColors.Control;
			this.textMsgDateTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textMsgDateTime.Location = new System.Drawing.Point(265, 3);
			this.textMsgDateTime.Name = "textMsgDateTime";
			this.textMsgDateTime.Size = new System.Drawing.Size(286, 13);
			this.textMsgDateTime.TabIndex = 12;
			this.textMsgDateTime.Text = "";
			// 
			// labelSent
			// 
			this.labelSent.Location = new System.Drawing.Point(193, 4);
			this.labelSent.Name = "labelSent";
			this.labelSent.Size = new System.Drawing.Size(71, 14);
			this.labelSent.TabIndex = 13;
			this.labelSent.Text = "Date / Time:";
			this.labelSent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(7, 339);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75, 26);
			this.butDelete.TabIndex = 21;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(7, 305);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75, 26);
			this.butAdd.TabIndex = 19;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(7, 5);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(124, 14);
			this.label4.TabIndex = 18;
			this.label4.Text = "E-mail Template";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listTemplates
			// 
			this.listTemplates.Location = new System.Drawing.Point(10, 26);
			this.listTemplates.Name = "listTemplates";
			this.listTemplates.Size = new System.Drawing.Size(164, 277);
			this.listTemplates.TabIndex = 17;
			this.listTemplates.DoubleClick += new System.EventHandler(this.listTemplates_DoubleClick);
			// 
			// textBodyText
			// 
			this.textBodyText.AcceptsReturn = true;
			this.textBodyText.Location = new System.Drawing.Point(265, 81);
			this.textBodyText.Multiline = true;
			this.textBodyText.Name = "textBodyText";
			this.textBodyText.QuickPasteType = OpenDental.QuickPasteType.Email;
			this.textBodyText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBodyText.Size = new System.Drawing.Size(641, 532);
			this.textBodyText.TabIndex = 22;
			this.textBodyText.Text = "";
			this.textBodyText.TextChanged += new System.EventHandler(this.textBodyText_TextChanged);
			// 
			// butInsert
			// 
			this.butInsert.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butInsert.Autosize = true;
			this.butInsert.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butInsert.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butInsert.Image = ((System.Drawing.Image)(resources.GetObject("butInsert.Image")));
			this.butInsert.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butInsert.Location = new System.Drawing.Point(102, 305);
			this.butInsert.Name = "butInsert";
			this.butInsert.Size = new System.Drawing.Size(74, 26);
			this.butInsert.TabIndex = 23;
			this.butInsert.Text = "Insert";
			this.butInsert.Click += new System.EventHandler(this.butInsert_Click);
			// 
			// panelTemplates
			// 
			this.panelTemplates.Controls.Add(this.butInsert);
			this.panelTemplates.Controls.Add(this.butDelete);
			this.panelTemplates.Controls.Add(this.butAdd);
			this.panelTemplates.Controls.Add(this.label4);
			this.panelTemplates.Controls.Add(this.listTemplates);
			this.panelTemplates.Location = new System.Drawing.Point(8, 9);
			this.panelTemplates.Name = "panelTemplates";
			this.panelTemplates.Size = new System.Drawing.Size(180, 370);
			this.panelTemplates.TabIndex = 24;
			// 
			// FormEmailMessageEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(921, 670);
			this.Controls.Add(this.panelTemplates);
			this.Controls.Add(this.textBodyText);
			this.Controls.Add(this.textMsgDateTime);
			this.Controls.Add(this.textFromAddress);
			this.Controls.Add(this.textToAddress);
			this.Controls.Add(this.textSubject);
			this.Controls.Add(this.labelSent);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butSend);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEmailMessageEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit E-mail Message";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormEmailMessageEdit_Closing);
			this.Load += new System.EventHandler(this.FormEmailMessageEdit_Load);
			this.panelTemplates.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormEmailMessageEdit_Load(object sender, System.EventArgs e) {
			if(EmailMessages.Cur.MsgDateTime.Year<1880){//not sent
				labelSent.Visible=false;
				textMsgDateTime.Text="";
			}
			else{
				//already sent
				panelTemplates.Visible=false;
				textMsgDateTime.Text=EmailMessages.Cur.MsgDateTime.ToString();
				butSend.Enabled=false;//not allowed to send again.
			}
			textFromAddress.Text=EmailMessages.Cur.FromAddress;
			textToAddress.Text=EmailMessages.Cur.ToAddress;
			textSubject.Text=EmailMessages.Cur.Subject;
			textBodyText.Text=EmailMessages.Cur.BodyText;
			FillList();
		}

		private void FillList(){
			listTemplates.Items.Clear();
			for(int i=0;i<EmailTemplates.List.Length;i++){
				listTemplates.Items.Add(EmailTemplates.List[i].Subject);
			}
		}

		private void listTemplates_DoubleClick(object sender, System.EventArgs e) {
			if(listTemplates.SelectedIndex==-1){
				return;
			}
			FormEmailTemplateEdit FormE=new FormEmailTemplateEdit();
			FormE.ETcur=EmailTemplates.List[listTemplates.SelectedIndex];
			FormE.ShowDialog();
			if(FormE.DialogResult!=DialogResult.OK){
				return;
			}
			EmailTemplates.Refresh();
			templatesChanged=true;
			FillList();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormEmailTemplateEdit FormE=new FormEmailTemplateEdit();
			FormE.IsNew=true;
			FormE.ETcur=new EmailTemplate();
			FormE.ShowDialog();
			if(FormE.DialogResult!=DialogResult.OK){
				return;
			}
			EmailTemplates.Refresh();
			templatesChanged=true;
			FillList();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(listTemplates.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete e-mail template?"),"",MessageBoxButtons.OKCancel)
				!=DialogResult.OK){
				return;
			}
			EmailTemplates.List[listTemplates.SelectedIndex].Delete();
			EmailTemplates.Refresh();
			templatesChanged=true;
			FillList();
		}

		private void butInsert_Click(object sender, System.EventArgs e) {
			if(listTemplates.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			if(messageChanged){
				if(MessageBox.Show(Lan.g(this,"Replace exising e-mail text with text from the template?"),"",MessageBoxButtons.OKCancel)
					!=DialogResult.OK){
					return;
				}
			}
			textSubject.Text=EmailTemplates.List[listTemplates.SelectedIndex].Subject;
			textBodyText.Text=EmailTemplates.List[listTemplates.SelectedIndex].BodyText;
			messageChanged=false;
		}

		private void textBodyText_TextChanged(object sender, System.EventArgs e) {
			messageChanged=true;
		}

		private void butSend_Click(object sender, System.EventArgs e) {
			if(textFromAddress.Text==""
				|| textToAddress.Text=="")
			{
				MessageBox.Show("Addresses not allowed to be blank.");
				return;
			}
			if(((Pref)Prefs.HList["EmailSMTPserver"]).ValueString==""){
				MessageBox.Show("You need to enter an SMTP server name in e-mail setup before you can send e-mail.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			Indy.Sockets.IndyMessage.Message msg=new Indy.Sockets.IndyMessage.Message();
			Indy.Sockets.IndySMTP.SMTP smtp=new Indy.Sockets.IndySMTP.SMTP();
			smtp.Host=((Pref)Prefs.HList["EmailSMTPserver"]).ValueString;
			smtp.UseEhlo=false;
			msg.From.Text=textFromAddress.Text;
			msg.Recipients.EMailAddresses=textToAddress.Text;
			msg.Subject=textSubject.Text;
			msg.Body.Add(textBodyText.Text);
			try{
				smtp.Connect();
				smtp.Send(msg);
			}
			catch(Exception ex){
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
				return;
			}
			finally {
				smtp.Disconnect();
			}
			/*try{
        Smtp smtp=new Smtp();
        smtp.server=((Pref)Prefs.HList["EmailSMTPserver"]).ValueString;
        smtp.from=textFromAddress.Text;
				smtp.to.Add(textToAddress.Text);
        smtp.subject=textSubject.Text;
        //smtp.bodyHtml="<HTML><BODY>Hello World</BODY></HTML>";
				smtp.bodyText=textBodyText.Text;
        smtp.Send();
			}
			catch(SmtpException ex){
				Cursor=Cursors.Default;
				MessageBox.Show(ex.What());
				return;
			}*/
			Cursor=Cursors.Default;
			EmailMessages.Cur.MsgDateTime=DateTime.Now;
			EmailMessages.Cur.FromAddress=textFromAddress.Text;
			EmailMessages.Cur.ToAddress=textToAddress.Text;
			EmailMessages.Cur.Subject=textSubject.Text;
			EmailMessages.Cur.BodyText=textBodyText.Text;
			EmailMessages.Cur.PatNum=PatNum;
			EmailMessages.InsertCur();
			Commlog CommlogCur=new Commlog();
			CommlogCur.PatNum=PatNum;
			CommlogCur.CommDateTime=DateTime.Now;
			CommlogCur.CommType=CommItemType.Misc;
			CommlogCur.EmailMessageNum=EmailMessages.Cur.EmailMessageNum;
			CommlogCur.Mode=CommItemMode.Email;
			CommlogCur.SentOrReceived=CommSentOrReceived.Sent;
			CommlogCur.Note=EmailMessages.Cur.Subject;
			CommlogCur.Insert();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormEmailMessageEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(templatesChanged){
				DataValid.SetInvalid(InvalidTypes.Email);
			}
		}

		

		

		


	}
}





















