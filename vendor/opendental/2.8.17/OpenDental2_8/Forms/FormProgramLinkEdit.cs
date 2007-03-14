using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormProgramLinkEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.CheckBox checkEnabled;
		private System.ComponentModel.Container components = null;
		private OpenDental.XPButton butDelete;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textProgName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textProgDesc;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ListBox listProperties;
		private System.Windows.Forms.TextBox textPath;
		private System.Windows.Forms.TextBox textCommandLine;
		private System.Windows.Forms.ListBox listToolBars;
		private System.Windows.Forms.TextBox textButtonText;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textNote;// Required designer variable.
		/// <summary>This Program link is new.</summary>
		public bool IsNew;

		///<summary></summary>
		public FormProgramLinkEdit(){
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormProgramLinkEdit));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.checkEnabled = new System.Windows.Forms.CheckBox();
			this.butDelete = new OpenDental.XPButton();
			this.label1 = new System.Windows.Forms.Label();
			this.textProgName = new System.Windows.Forms.TextBox();
			this.textProgDesc = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textPath = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textCommandLine = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.listToolBars = new System.Windows.Forms.ListBox();
			this.listProperties = new System.Windows.Forms.ListBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textButtonText = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(662, 410);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(662, 369);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// checkEnabled
			// 
			this.checkEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkEnabled.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkEnabled.Location = new System.Drawing.Point(161, 66);
			this.checkEnabled.Name = "checkEnabled";
			this.checkEnabled.Size = new System.Drawing.Size(98, 18);
			this.checkEnabled.TabIndex = 41;
			this.checkEnabled.Text = "Enabled";
			this.checkEnabled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(31, 410);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75, 26);
			this.butDelete.TabIndex = 43;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(58, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(187, 18);
			this.label1.TabIndex = 44;
			this.label1.Text = "Internal Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textProgName
			// 
			this.textProgName.Location = new System.Drawing.Point(246, 9);
			this.textProgName.Name = "textProgName";
			this.textProgName.ReadOnly = true;
			this.textProgName.Size = new System.Drawing.Size(275, 20);
			this.textProgName.TabIndex = 45;
			this.textProgName.Text = "";
			// 
			// textProgDesc
			// 
			this.textProgDesc.Location = new System.Drawing.Point(246, 37);
			this.textProgDesc.Name = "textProgDesc";
			this.textProgDesc.Size = new System.Drawing.Size(275, 20);
			this.textProgDesc.TabIndex = 47;
			this.textProgDesc.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(57, 38);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(187, 18);
			this.label2.TabIndex = 46;
			this.label2.Text = "Description";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPath
			// 
			this.textPath.Location = new System.Drawing.Point(246, 90);
			this.textPath.Name = "textPath";
			this.textPath.Size = new System.Drawing.Size(410, 20);
			this.textPath.TabIndex = 49;
			this.textPath.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(13, 92);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(231, 18);
			this.label3.TabIndex = 48;
			this.label3.Text = "Path of file to open";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCommandLine
			// 
			this.textCommandLine.Location = new System.Drawing.Point(246, 119);
			this.textCommandLine.Name = "textCommandLine";
			this.textCommandLine.Size = new System.Drawing.Size(410, 20);
			this.textCommandLine.TabIndex = 52;
			this.textCommandLine.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 120);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(232, 18);
			this.label4.TabIndex = 51;
			this.label4.Text = "Optional command line arguments";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// listToolBars
			// 
			this.listToolBars.Location = new System.Drawing.Point(34, 212);
			this.listToolBars.Name = "listToolBars";
			this.listToolBars.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listToolBars.Size = new System.Drawing.Size(147, 95);
			this.listToolBars.TabIndex = 53;
			// 
			// listProperties
			// 
			this.listProperties.Location = new System.Drawing.Point(246, 212);
			this.listProperties.Name = "listProperties";
			this.listProperties.Size = new System.Drawing.Size(323, 95);
			this.listProperties.TabIndex = 54;
			this.listProperties.DoubleClick += new System.EventHandler(this.listProperties_DoubleClick);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(246, 191);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(331, 17);
			this.label5.TabIndex = 55;
			this.label5.Text = "Additional Properties (you can edit, but not add)";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(33, 191);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(156, 17);
			this.label6.TabIndex = 56;
			this.label6.Text = "Add a button to these toolbars";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textButtonText
			// 
			this.textButtonText.Location = new System.Drawing.Point(246, 148);
			this.textButtonText.Name = "textButtonText";
			this.textButtonText.Size = new System.Drawing.Size(195, 20);
			this.textButtonText.TabIndex = 58;
			this.textButtonText.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(13, 149);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(232, 18);
			this.label7.TabIndex = 57;
			this.label7.Text = "Text on button";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(246, 353);
			this.textNote.MaxLength = 255;
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(323, 80);
			this.textNote.TabIndex = 59;
			this.textNote.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(246, 333);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(221, 17);
			this.label8.TabIndex = 60;
			this.label8.Text = "Notes";
			this.label8.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormProgramLinkEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(757, 456);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.textButtonText);
			this.Controls.Add(this.textCommandLine);
			this.Controls.Add(this.textPath);
			this.Controls.Add(this.textProgDesc);
			this.Controls.Add(this.textProgName);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.listProperties);
			this.Controls.Add(this.listToolBars);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.checkEnabled);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProgramLinkEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Program Link";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormProgramLinkEdit_Closing);
			this.Load += new System.EventHandler(this.FormProgramLinkEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormProgramLinkEdit_Load(object sender, System.EventArgs e) {
			if(Programs.Cur.ProgName!=""){
				//user not allowed to delete program links that we include, only their own.
				butDelete.Enabled=false;
			}
			FillForm();
		}

		private void FillForm(){
			//this is not refined enought to be called more than once on the form because it will not
			//remember the toolbars that were selected.
			ToolButItems.Refresh();
			ProgramProperties.Refresh();
			textProgName.Text=Programs.Cur.ProgName;
			textProgDesc.Text=Programs.Cur.ProgDesc;
			checkEnabled.Checked=Programs.Cur.Enabled;
			textPath.Text=Programs.Cur.Path;
			textCommandLine.Text=Programs.Cur.CommandLine;
			textNote.Text=Programs.Cur.Note;
			ToolButItems.GetForProgram();
			listToolBars.Items.Clear();
			for(int i=0;i<Enum.GetNames(typeof(ToolBarsAvail)).Length;i++){
				listToolBars.Items.Add(Enum.GetNames(typeof(ToolBarsAvail))[i]);
			}
			for(int i=0;i<ToolButItems.ForProgram.Count;i++){
				listToolBars.SetSelected((int)((ToolButItem)ToolButItems.ForProgram[i]).ToolBar,true);
			}
			if(ToolButItems.ForProgram.Count>0){//the text on all buttons will be the same for now
				textButtonText.Text=((ToolButItem)ToolButItems.ForProgram[0]).ButtonText;
			}
			ProgramProperties.GetForProgram();
			listProperties.Items.Clear();
			for(int i=0;i<ProgramProperties.ForProgram.Count;i++){
				listProperties.Items.Add(((ProgramProperty)ProgramProperties.ForProgram[i]).PropertyDesc
					+": "+((ProgramProperty)ProgramProperties.ForProgram[i]).PropertyValue);
			}
		}

		private void listProperties_DoubleClick(object sender, System.EventArgs e) {
			if(listProperties.SelectedIndex==-1)
				return;
			ProgramProperties.Cur=(ProgramProperty)ProgramProperties.ForProgram[listProperties.SelectedIndex];
			FormProgramProperty FormPP=new FormProgramProperty();
			FormPP.ShowDialog();
			if(FormPP.DialogResult!=DialogResult.OK)
				return;
			ProgramProperties.Refresh();
			ProgramProperties.GetForProgram();
			listProperties.Items.Clear();
			for(int i=0;i<ProgramProperties.ForProgram.Count;i++){
				listProperties.Items.Add(((ProgramProperty)ProgramProperties.ForProgram[i]).PropertyDesc
					+": "+((ProgramProperty)ProgramProperties.ForProgram[i]).PropertyValue);
			}
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"Delete this program link?"),"",MessageBoxButtons.OKCancel)
				!=DialogResult.OK){
				return;
			}
			if(!IsNew){
				Programs.DeleteCur();
			}
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			Programs.Cur.ProgName=textProgName.Text;
			Programs.Cur.ProgDesc=textProgDesc.Text;
			Programs.Cur.Enabled=checkEnabled.Checked;
			Programs.Cur.Path=textPath.Text;
			Programs.Cur.CommandLine=textCommandLine.Text;
			Programs.Cur.Note=textNote.Text;
			if(IsNew){
				Programs.InsertCur();
			}
			else{
				Programs.UpdateCur();
			}
			ToolButItems.DeleteAllForProgram();
			//then add one toolButItem for each highlighted row in listbox
			for(int i=0;i<listToolBars.SelectedIndices.Count;i++){
				ToolButItems.Cur=new ToolButItem();
				ToolButItems.Cur.ProgramNum=Programs.Cur.ProgramNum;
				ToolButItems.Cur.ButtonText=textButtonText.Text;
				ToolButItems.Cur.ToolBar=(ToolBarsAvail)listToolBars.SelectedIndices[i];
				ToolButItems.InsertCur();
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormProgramLinkEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNew){
				Programs.DeleteCur();
			}
		}

		

		

		
		


	}
}





















