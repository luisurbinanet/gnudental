using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormQuickPasteNoteEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textNote;
		private OpenDental.XPButton butDelete;
		private QuickPasteNote QuickNote;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textAbbreviation;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		public bool IsNew;

		///<summary></summary>
		public FormQuickPasteNoteEdit(QuickPasteNote quickNote){
			//
			// Required for Windows Form Designer support
			//
			QuickNote=quickNote;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormQuickPasteNoteEdit));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.textNote = new System.Windows.Forms.TextBox();
			this.butDelete = new OpenDental.XPButton();
			this.label1 = new System.Windows.Forms.Label();
			this.textAbbreviation = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(592, 514);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(497, 514);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(38, 62);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(628, 431);
			this.textNote.TabIndex = 1;
			this.textNote.Text = "";
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(40, 514);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(79, 26);
			this.butDelete.TabIndex = 4;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(37, 44);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 16);
			this.label1.TabIndex = 15;
			this.label1.Text = "Note";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textAbbreviation
			// 
			this.textAbbreviation.Location = new System.Drawing.Point(38, 22);
			this.textAbbreviation.Name = "textAbbreviation";
			this.textAbbreviation.Size = new System.Drawing.Size(252, 20);
			this.textAbbreviation.TabIndex = 0;
			this.textAbbreviation.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(37, 4);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(157, 16);
			this.label2.TabIndex = 17;
			this.label2.Text = "Abbreviation";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(293, 13);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(365, 33);
			this.label3.TabIndex = 18;
			this.label3.Text = "If you type a ? immediately followed by the abbreviation, your note will be inser" +
				"ted";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormQuickPasteNoteEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(719, 564);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textAbbreviation);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormQuickPasteNoteEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Quick Paste Note";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormQuickPasteNoteEdit_Closing);
			this.Load += new System.EventHandler(this.FormQuickPasteNoteEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormQuickPasteNoteEdit_Load(object sender, System.EventArgs e) {
			textAbbreviation.Text=QuickNote.Abbreviation;
			textNote.Text=QuickNote.Note;
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"Delete note?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			QuickNote.Delete();
			QuickNote.QuickPasteNoteNum=0;//triggers an action in the calling form
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			QuickNote.Abbreviation=textAbbreviation.Text;
			if(QuickNote.Abbreviation!=""){
				if(QuickNote.AbbrAlreadyInUse()){
					MessageBox.Show(Lan.g(this,"Abbreviation is already in use."));
					return;
				}
			}
			QuickNote.Note=textNote.Text;
			if(IsNew){
				QuickNote.Insert();
			}
			else{
				QuickNote.Update();
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormQuickPasteNoteEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK){
				return;
			}
			if(IsNew){
				QuickNote.Delete();
				DialogResult=DialogResult.Cancel;
			}
		}

		

		


	}
}





















