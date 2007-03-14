using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormClockEventEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.TextBox textTimeEntered;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton radioClockIn;
		private System.Windows.Forms.RadioButton radioClockOut;
		private System.Windows.Forms.ListBox listStatus;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.TextBox textTimeDisplayed;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormClockEventEdit()
		{
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormClockEventEdit));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.textTimeEntered = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textTimeDisplayed = new System.Windows.Forms.TextBox();
			this.radioClockIn = new System.Windows.Forms.RadioButton();
			this.radioClockOut = new System.Windows.Forms.RadioButton();
			this.listStatus = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(533, 265);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(533, 224);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textTimeEntered
			// 
			this.textTimeEntered.Location = new System.Drawing.Point(179, 21);
			this.textTimeEntered.Name = "textTimeEntered";
			this.textTimeEntered.ReadOnly = true;
			this.textTimeEntered.Size = new System.Drawing.Size(156, 20);
			this.textTimeEntered.TabIndex = 2;
			this.textTimeEntered.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(154, 16);
			this.label1.TabIndex = 3;
			this.label1.Text = "Date and Time Entered";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(14, 53);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(163, 16);
			this.label2.TabIndex = 5;
			this.label2.Text = "Date and Time Displayed";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textTimeDisplayed
			// 
			this.textTimeDisplayed.Location = new System.Drawing.Point(179, 51);
			this.textTimeDisplayed.Name = "textTimeDisplayed";
			this.textTimeDisplayed.Size = new System.Drawing.Size(155, 20);
			this.textTimeDisplayed.TabIndex = 4;
			this.textTimeDisplayed.Text = "";
			this.textTimeDisplayed.Validating += new System.ComponentModel.CancelEventHandler(this.textTimeDisplayed_Validating);
			// 
			// radioClockIn
			// 
			this.radioClockIn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioClockIn.Location = new System.Drawing.Point(178, 85);
			this.radioClockIn.Name = "radioClockIn";
			this.radioClockIn.Size = new System.Drawing.Size(104, 16);
			this.radioClockIn.TabIndex = 6;
			this.radioClockIn.Text = "Clock In";
			// 
			// radioClockOut
			// 
			this.radioClockOut.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioClockOut.Location = new System.Drawing.Point(178, 105);
			this.radioClockOut.Name = "radioClockOut";
			this.radioClockOut.Size = new System.Drawing.Size(104, 16);
			this.radioClockOut.TabIndex = 7;
			this.radioClockOut.Text = "Clock Out";
			// 
			// listStatus
			// 
			this.listStatus.Location = new System.Drawing.Point(179, 131);
			this.listStatus.Name = "listStatus";
			this.listStatus.Size = new System.Drawing.Size(120, 43);
			this.listStatus.TabIndex = 8;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(72, 131);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(105, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Status";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(72, 182);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(105, 16);
			this.label4.TabIndex = 10;
			this.label4.Text = "Note";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(179, 181);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(317, 110);
			this.textNote.TabIndex = 11;
			this.textNote.Text = "";
			// 
			// FormClockEventEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(647, 316);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.textTimeDisplayed);
			this.Controls.Add(this.textTimeEntered);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.listStatus);
			this.Controls.Add(this.radioClockOut);
			this.Controls.Add(this.radioClockIn);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClockEventEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Clock Event";
			this.Load += new System.EventHandler(this.FormClockEventEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormClockEventEdit_Load(object sender, System.EventArgs e) {
			textTimeEntered.Text=ClockEvents.Cur.TimeEntered.ToString();
			textTimeDisplayed.Text=ClockEvents.Cur.TimeDisplayed.ToString();
			if(ClockEvents.Cur.ClockIn){
				radioClockIn.Checked=true;
			}
			else{
				radioClockOut.Checked=true;
			}
			listStatus.Items.Clear();
			for(int i=0;i<Enum.GetNames(typeof(TimeClockStatus)).Length;i++){
				listStatus.Items.Add(Enum.GetNames(typeof(TimeClockStatus))[i]);
			}
			listStatus.SelectedIndex=(int)ClockEvents.Cur.ClockStatus;//all clockevents have a status
			textNote.Text=ClockEvents.Cur.Note;
		}

		private void textTimeDisplayed_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			try{
				ClockEvents.Cur.TimeDisplayed=DateTime.Parse(textTimeDisplayed.Text);
			}
			catch{
				MessageBox.Show(Lan.g(this,"Please enter a valid date and time."));
				e.Cancel=true;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//TimeDisplayed already handled
			ClockEvents.Cur.ClockIn=radioClockIn.Checked;
			ClockEvents.Cur.ClockStatus=(TimeClockStatus)listStatus.SelectedIndex;
			ClockEvents.Cur.Note=textNote.Text;
			ClockEvents.UpdateCur();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

	


	}
}






































