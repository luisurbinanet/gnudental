using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Media;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormSigButDefEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private Label label2;
		private TextBox textButtonText;
		private Label label3;
		private Label label5;
		private ValidNum textSynchIcon;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butDelete;
		private OpenFileDialog openFileDialog1;
		private SaveFileDialog saveFileDialog1;
		private RadioButton radioAll;
		private RadioButton radioOne;
		private TextBox textComputerName;
		private ComboBox comboTo;
		private Label label4;
		private Label label10;
		private ComboBox comboExtras;
		private Label label11;
		private ComboBox comboMessage;
		///<summary></summary>
		public bool IsNew;
		private SigElementDef[] sigElementDefUser;
		private SigElementDef[] sigElementDefExtras;
		private SigElementDef[] sigElementDefMessages;
		//<summary>This needs to be set before calling this form.  Blank means applies to all.</summary>
		//public string ComputerName;
		///<summary>Required to be set before opening this form.</summary>
		public SigButDef ButtonCur;

		///<summary></summary>
		public FormSigButDefEdit()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
			this.label2 = new System.Windows.Forms.Label();
			this.textButtonText = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.butDelete = new OpenDental.UI.Button();
			this.textSynchIcon = new OpenDental.ValidNum();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.radioAll = new System.Windows.Forms.RadioButton();
			this.radioOne = new System.Windows.Forms.RadioButton();
			this.textComputerName = new System.Windows.Forms.TextBox();
			this.comboTo = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.comboExtras = new System.Windows.Forms.ComboBox();
			this.label11 = new System.Windows.Forms.Label();
			this.comboMessage = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			this.label2.Location = new System.Drawing.Point(102,84);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,20);
			this.label2.TabIndex = 4;
			this.label2.Text = "Text";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textButtonText
			// 
			this.textButtonText.Location = new System.Drawing.Point(204,81);
			this.textButtonText.Name = "textButtonText";
			this.textButtonText.Size = new System.Drawing.Size(105,20);
			this.textButtonText.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			this.label3.Location = new System.Drawing.Point(261,104);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(304,40);
			this.label3.TabIndex = 6;
			this.label3.Text = "The cell number (1-9) of the main program icon that should light up whenever this" +
    " button lights up.  0 for none.";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			this.label5.Location = new System.Drawing.Point(102,114);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100,20);
			this.label5.TabIndex = 8;
			this.label5.Text = "Synch Icon";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(45,292);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(82,26);
			this.butDelete.TabIndex = 14;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// textSynchIcon
			// 
			this.textSynchIcon.Location = new System.Drawing.Point(204,114);
			this.textSynchIcon.MaxVal = 9;
			this.textSynchIcon.MinVal = 0;
			this.textSynchIcon.Name = "textSynchIcon";
			this.textSynchIcon.Size = new System.Drawing.Size(51,20);
			this.textSynchIcon.TabIndex = 1;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(442,292);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.Location = new System.Drawing.Point(544,292);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// radioAll
			// 
			this.radioAll.AutoCheck = false;
			this.radioAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioAll.Location = new System.Drawing.Point(2,10);
			this.radioAll.Name = "radioAll";
			this.radioAll.Size = new System.Drawing.Size(214,20);
			this.radioAll.TabIndex = 15;
			this.radioAll.TabStop = true;
			this.radioAll.Text = "Applies to all computers";
			this.radioAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioAll.UseVisualStyleBackColor = true;
			// 
			// radioOne
			// 
			this.radioOne.AutoCheck = false;
			this.radioOne.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioOne.Location = new System.Drawing.Point(2,33);
			this.radioOne.Name = "radioOne";
			this.radioOne.Size = new System.Drawing.Size(214,20);
			this.radioOne.TabIndex = 16;
			this.radioOne.TabStop = true;
			this.radioOne.Text = "Only applies to one computer:";
			this.radioOne.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioOne.UseVisualStyleBackColor = true;
			// 
			// textComputerName
			// 
			this.textComputerName.Location = new System.Drawing.Point(225,33);
			this.textComputerName.Name = "textComputerName";
			this.textComputerName.ReadOnly = true;
			this.textComputerName.Size = new System.Drawing.Size(154,20);
			this.textComputerName.TabIndex = 17;
			// 
			// comboTo
			// 
			this.comboTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboTo.FormattingEnabled = true;
			this.comboTo.Location = new System.Drawing.Point(204,147);
			this.comboTo.MaxDropDownItems = 30;
			this.comboTo.Name = "comboTo";
			this.comboTo.Size = new System.Drawing.Size(121,21);
			this.comboTo.TabIndex = 18;
			// 
			// label4
			// 
			this.label4.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			this.label4.Location = new System.Drawing.Point(102,148);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100,20);
			this.label4.TabIndex = 19;
			this.label4.Text = "To User";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			this.label10.Location = new System.Drawing.Point(102,183);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(100,20);
			this.label10.TabIndex = 24;
			this.label10.Text = "Extras";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboExtras
			// 
			this.comboExtras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboExtras.FormattingEnabled = true;
			this.comboExtras.Location = new System.Drawing.Point(204,182);
			this.comboExtras.MaxDropDownItems = 30;
			this.comboExtras.Name = "comboExtras";
			this.comboExtras.Size = new System.Drawing.Size(121,21);
			this.comboExtras.TabIndex = 23;
			// 
			// label11
			// 
			this.label11.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			this.label11.Location = new System.Drawing.Point(102,217);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(100,20);
			this.label11.TabIndex = 26;
			this.label11.Text = "Message";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboMessage
			// 
			this.comboMessage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboMessage.FormattingEnabled = true;
			this.comboMessage.Location = new System.Drawing.Point(204,216);
			this.comboMessage.MaxDropDownItems = 30;
			this.comboMessage.Name = "comboMessage";
			this.comboMessage.Size = new System.Drawing.Size(121,21);
			this.comboMessage.TabIndex = 25;
			// 
			// FormSigButDefEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(671,343);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.comboMessage);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.comboExtras);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.comboTo);
			this.Controls.Add(this.textComputerName);
			this.Controls.Add(this.radioOne);
			this.Controls.Add(this.radioAll);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textSynchIcon);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textButtonText);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSigButDefEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Message Button";
			this.Load += new System.EventHandler(this.FormSigButDefEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormSigButDefEdit_Load(object sender,EventArgs e) {
			if(ButtonCur.ComputerName==""){
				radioAll.Checked=true;
			}
			else{
				radioOne.Checked=true;
				textComputerName.Text=ButtonCur.ComputerName;
			}
			textButtonText.Text=ButtonCur.ButtonText;
			textSynchIcon.Text=ButtonCur.SynchIcon.ToString();
			sigElementDefUser=SigElementDefs.GetSubList(SignalElementType.User);
			sigElementDefExtras=SigElementDefs.GetSubList(SignalElementType.Extra);
			sigElementDefMessages=SigElementDefs.GetSubList(SignalElementType.Message);
			SigButDefElement elementUser=SigButDefs.GetElement(ButtonCur,SignalElementType.User);
			SigButDefElement elementExtra=SigButDefs.GetElement(ButtonCur,SignalElementType.Extra);
			SigButDefElement elementMessage=SigButDefs.GetElement(ButtonCur,SignalElementType.Message);
			comboTo.Items.Clear();
			comboTo.Items.Add(Lan.g(this,"none"));
			comboTo.SelectedIndex=0;
			for(int i=0;i<sigElementDefUser.Length;i++) {
				comboTo.Items.Add(sigElementDefUser[i].SigText);
				if(elementUser!=null && elementUser.SigElementDefNum==sigElementDefUser[i].SigElementDefNum){
					comboTo.SelectedIndex=i+1;
				}
			}
			comboExtras.Items.Clear();
			comboExtras.Items.Add(Lan.g(this,"none"));
			comboExtras.SelectedIndex=0;
			for(int i=0;i<sigElementDefExtras.Length;i++) {
				comboExtras.Items.Add(sigElementDefExtras[i].SigText);
				if(elementExtra!=null && elementExtra.SigElementDefNum==sigElementDefExtras[i].SigElementDefNum) {
					comboExtras.SelectedIndex=i+1;
				}
			}
			comboMessage.Items.Clear();
			comboMessage.Items.Add(Lan.g(this,"none"));
			comboMessage.SelectedIndex=0;
			for(int i=0;i<sigElementDefMessages.Length;i++) {
				comboMessage.Items.Add(sigElementDefMessages[i].SigText);
				if(elementMessage!=null && elementMessage.SigElementDefNum==sigElementDefMessages[i].SigElementDefNum) {
					comboMessage.SelectedIndex=i+1;
				}
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew) {
				DialogResult=DialogResult.Cancel;
			}
			else {
				if(!MsgBox.Show(this,true,"Delete?")) {
					return;
				}
				SigButDefs.Delete(ButtonCur);//also deletes elements
				DialogResult=DialogResult.OK;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textSynchIcon.errorProvider1.GetError(textSynchIcon)!=""
				) {
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(textButtonText.Text==""){
				MsgBox.Show(this,"Please enter a text description first.");
				return;
			}
			if(textSynchIcon.Text==""){
				textSynchIcon.Text="0";
			}
			ButtonCur.ButtonText=textButtonText.Text;
			ButtonCur.SynchIcon=PIn.PInt(textSynchIcon.Text);
			if(IsNew){
				SigButDefs.Insert(ButtonCur);
			}
			else{
				SigButDefs.Update(ButtonCur);
			}
			//delete all the existing elements
			SigButDefs.DeleteElements(ButtonCur);
			SigButDefElement element;
			if(comboTo.SelectedIndex!=0){
				element=new SigButDefElement();
				element.SigButDefNum=ButtonCur.SigButDefNum;
				element.SigElementDefNum=sigElementDefUser[comboTo.SelectedIndex-1].SigElementDefNum;
				SigButDefElements.Insert(element);
			}
			if(comboExtras.SelectedIndex!=0) {
				element=new SigButDefElement();
				element.SigButDefNum=ButtonCur.SigButDefNum;
				element.SigElementDefNum=sigElementDefExtras[comboExtras.SelectedIndex-1].SigElementDefNum;
				SigButDefElements.Insert(element);
			}
			if(comboMessage.SelectedIndex!=0) {
				element=new SigButDefElement();
				element.SigButDefNum=ButtonCur.SigButDefNum;
				element.SigElementDefNum=sigElementDefMessages[comboMessage.SelectedIndex-1].SigElementDefNum;
				SigButDefElements.Insert(element);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		


	}
}





















