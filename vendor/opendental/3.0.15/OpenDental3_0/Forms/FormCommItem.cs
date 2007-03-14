/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	///<summary></summary>
	public class FormCommItem : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label6;
		private System.ComponentModel.Container components = null;// Required designer variable.
		///<summary></summary>
		public bool IsNew;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butDelete;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textDateTime;
		private System.Windows.Forms.ListBox listMode;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox listSentOrReceived;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button butEmail;
		private OpenDental.ODtextBox textNote;
		private System.Windows.Forms.ListBox listType;
		//private ArrayList NegIndex=new ArrayList();
		//private DateTime OriginalDate;
		//private double OriginalAmt;

		///<summary></summary>
		public FormCommItem(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label1,
				this.label2, //*Ann
				this.label3, //*Ann
				this.label4, //*Ann
				this.label6,
				this.butEmail, //*Ann
				this //*Ann
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
				butDelete
			}); 
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

		private void InitializeComponent(){
			this.label1 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.butDelete = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.listType = new System.Windows.Forms.ListBox();
			this.textDateTime = new System.Windows.Forms.TextBox();
			this.listMode = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.listSentOrReceived = new System.Windows.Forms.ListBox();
			this.label4 = new System.Windows.Forms.Label();
			this.butEmail = new System.Windows.Forms.Button();
			this.textNote = new OpenDental.ODtextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(11, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Date / Time";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(105, 47);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(82, 16);
			this.label6.TabIndex = 5;
			this.label6.Text = "Type";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(541, 356);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 6;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(541, 389);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 7;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butDelete
			// 
			this.butDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butDelete.Location = new System.Drawing.Point(32, 388);
			this.butDelete.Name = "butDelete";
			this.butDelete.TabIndex = 17;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(106, 166);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(82, 16);
			this.label2.TabIndex = 18;
			this.label2.Text = "Note";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listType
			// 
			this.listType.Location = new System.Drawing.Point(107, 65);
			this.listType.Name = "listType";
			this.listType.Size = new System.Drawing.Size(120, 95);
			this.listType.TabIndex = 20;
			// 
			// textDateTime
			// 
			this.textDateTime.Location = new System.Drawing.Point(107, 14);
			this.textDateTime.Name = "textDateTime";
			this.textDateTime.Size = new System.Drawing.Size(205, 20);
			this.textDateTime.TabIndex = 21;
			this.textDateTime.Text = "";
			// 
			// listMode
			// 
			this.listMode.Location = new System.Drawing.Point(241, 65);
			this.listMode.Name = "listMode";
			this.listMode.Size = new System.Drawing.Size(73, 95);
			this.listMode.TabIndex = 23;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(240, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(82, 16);
			this.label3.TabIndex = 22;
			this.label3.Text = "Mode";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listSentOrReceived
			// 
			this.listSentOrReceived.Location = new System.Drawing.Point(329, 65);
			this.listSentOrReceived.Name = "listSentOrReceived";
			this.listSentOrReceived.Size = new System.Drawing.Size(87, 43);
			this.listSentOrReceived.TabIndex = 25;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(328, 47);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(142, 16);
			this.label4.TabIndex = 24;
			this.label4.Text = "Sent or Received";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butEmail
			// 
			this.butEmail.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butEmail.Location = new System.Drawing.Point(108, 296);
			this.butEmail.Name = "butEmail";
			this.butEmail.Size = new System.Drawing.Size(88, 23);
			this.butEmail.TabIndex = 26;
			this.butEmail.Text = "View E-mail";
			this.butEmail.Click += new System.EventHandler(this.butEmail_Click);
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(107, 186);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDental.QuickPasteType.CommLog;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(512, 103);
			this.textNote.TabIndex = 27;
			this.textNote.Text = "";
			// 
			// FormCommItem
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(652, 434);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.butEmail);
			this.Controls.Add(this.listSentOrReceived);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.listMode);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textDateTime);
			this.Controls.Add(this.listType);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormCommItem";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Communication Item";
			this.Load += new System.EventHandler(this.FormCommItem_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormCommItem_Load(object sender, System.EventArgs e) {
			/*{				
				if(!UserPermissions.CheckUserPassword("Adjustment Edit",Adjustments.Cur.AdjDate)){
					//MessageBox.Show(Lan.g(this,"You only have permission to view the Adjustment. No changes will be saved."));
					butOK.Enabled=false;
					butDelete.Enabled=false;
				}					
			}*/
			textDateTime.Text=Commlogs.Cur.CommDateTime.ToString();
			//remember, this list is 1-based
			//there will ALWAYS be a commtype set before this dialog is opened
			for(int i=0;i<Enum.GetNames(typeof(CommItemType)).Length;i++){
				listType.Items.Add(Enum.GetNames(typeof(CommItemType))[i]);
			}
			listType.SelectedIndex=(int)Commlogs.Cur.CommType-1;
			for(int i=0;i<Enum.GetNames(typeof(CommItemMode)).Length;i++){
				listMode.Items.Add(Enum.GetNames(typeof(CommItemMode))[i]);
			}
			listMode.SelectedIndex=(int)Commlogs.Cur.Mode;
			for(int i=0;i<Enum.GetNames(typeof(CommSentOrReceived)).Length;i++){
				listSentOrReceived.Items.Add(Enum.GetNames(typeof(CommSentOrReceived))[i]);
			}
			try{
			listSentOrReceived.SelectedIndex=(int)Commlogs.Cur.SentOrReceived;
			}
			catch{
				MessageBox.Show(((int)Commlogs.Cur.SentOrReceived).ToString());
			}
			if(Commlogs.Cur.EmailMessageNum==0)
				butEmail.Visible=false;
			textNote.Text=Commlogs.Cur.Note;
			textNote.SelectionStart=textNote.Text.Length;
			textNote.Select();
		}

		///<summary>This button won't even be visible unless there is an email to view.</summary>
		private void butEmail_Click(object sender, System.EventArgs e) {
			EmailMessages.Refresh(Commlogs.Cur.EmailMessageNum);
			//a date is entered, so they will not be able to click Send to save any changes
			FormEmailMessageEdit FormE=new FormEmailMessageEdit();
			FormE.ShowDialog();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDateTime.Text==""
				//|| textAmount.errorProvider1.GetError(textAmount)!=""
				){
				MessageBox.Show(Lan.g(this,"Please enter a date first."));
				return;
			}
			try{
				DateTime.Parse(textDateTime.Text);
			}
			catch{
				MessageBox.Show(Lan.g(this,"Date and time invalid."));
				return;
			}
			Commlogs.Cur.CommDateTime=PIn.PDateT(textDateTime.Text);
			//there will always be a commtype selected.
			Commlogs.Cur.CommType=(CommItemType)(listType.SelectedIndex+1);
			Commlogs.Cur.Mode=(CommItemMode)listMode.SelectedIndex;
			Commlogs.Cur.SentOrReceived=(CommSentOrReceived)listSentOrReceived.SelectedIndex;
			Commlogs.Cur.Note=textNote.Text;
			if(IsNew){
				Commlogs.InsertCur();
			}
			else{
				Commlogs.UpdateCur();
		  	//SecurityLogs.MakeLogEntry("Adjustment Edit",Adjustments.cmd.CommandText);
			}
			DialogResult=DialogResult.OK;
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
			}
			else{
				//SecurityLogs.MakeLogEntry("Adjustment Edit","Delete. patNum: "+Adjustments.Cur.PatNum.ToString());
				Commlogs.DeleteCur();
				DialogResult=DialogResult.OK;
			}
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		//private void button1_Click(object sender, System.EventArgs e) {
		//	textNote.Select(textNote.Text.Length,1);
		//}

	}

}
