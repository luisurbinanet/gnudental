using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.Reporting;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormLetterEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TextBox textBody;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.GroupBox groupBox2;
		/// <summary>Required designer variable.</summary>
		private System.ComponentModel.Container components = null;
		private OpenDental.ValidDate validDate1;
		private OpenDental.ValidDate validDate2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label5;
		private OpenDental.ValidDate validDate3;
		private System.Windows.Forms.Label label6;
		private OpenDental.ValidDate validDate4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textDescription;
		private System.Windows.Forms.TextBox textReportName;
		private System.Windows.Forms.Label label7;
		private OpenDental.UI.ContrMultInput multInput;
		private System.Windows.Forms.Label label8;
		///<summary></summary>
		public bool IsNew;

		///<summary></summary>
		public FormLetterEdit()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormLetterEdit));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.validDate3 = new OpenDental.ValidDate();
			this.label6 = new System.Windows.Forms.Label();
			this.validDate4 = new OpenDental.ValidDate();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.validDate1 = new OpenDental.ValidDate();
			this.label1 = new System.Windows.Forms.Label();
			this.validDate2 = new OpenDental.ValidDate();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.textBody = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.textReportName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.multInput = new OpenDental.UI.ContrMultInput();
			this.label8 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(834, 625);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(834, 584);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Location = new System.Drawing.Point(32, 443);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(782, 201);
			this.tabControl1.TabIndex = 2;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Controls.Add(this.groupBox2);
			this.tabPage1.Controls.Add(this.checkBox2);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(774, 175);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Parameters";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.validDate3);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.validDate4);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(279, 72);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(218, 87);
			this.groupBox1.TabIndex = 15;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "New Pt Appointment Date Range";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(30, 56);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(82, 17);
			this.label5.TabIndex = 16;
			this.label5.Text = "Ending";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// validDate3
			// 
			this.validDate3.Location = new System.Drawing.Point(113, 28);
			this.validDate3.Name = "validDate3";
			this.validDate3.Size = new System.Drawing.Size(80, 20);
			this.validDate3.TabIndex = 15;
			this.validDate3.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(34, 29);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(78, 17);
			this.label6.TabIndex = 7;
			this.label6.Text = "Beginning";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// validDate4
			// 
			this.validDate4.Location = new System.Drawing.Point(113, 56);
			this.validDate4.Name = "validDate4";
			this.validDate4.Size = new System.Drawing.Size(80, 20);
			this.validDate4.TabIndex = 17;
			this.validDate4.Text = "";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.validDate1);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.validDate2);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(30, 69);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(218, 87);
			this.groupBox2.TabIndex = 14;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Any Appointment Date Range";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(30, 56);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(82, 17);
			this.label4.TabIndex = 16;
			this.label4.Text = "Ending";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// validDate1
			// 
			this.validDate1.Location = new System.Drawing.Point(113, 28);
			this.validDate1.Name = "validDate1";
			this.validDate1.Size = new System.Drawing.Size(80, 20);
			this.validDate1.TabIndex = 15;
			this.validDate1.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(34, 29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(78, 17);
			this.label1.TabIndex = 7;
			this.label1.Text = "Beginning";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// validDate2
			// 
			this.validDate2.Location = new System.Drawing.Point(113, 56);
			this.validDate2.Name = "validDate2";
			this.validDate2.Size = new System.Drawing.Size(80, 20);
			this.validDate2.TabIndex = 17;
			this.validDate2.Text = "";
			// 
			// checkBox2
			// 
			this.checkBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBox2.Location = new System.Drawing.Point(36, 35);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(222, 16);
			this.checkBox2.TabIndex = 11;
			this.checkBox2.Text = "Guarantors Only";
			// 
			// textBody
			// 
			this.textBody.Location = new System.Drawing.Point(132, 87);
			this.textBody.Multiline = true;
			this.textBody.Name = "textBody";
			this.textBody.Size = new System.Drawing.Size(677, 206);
			this.textBody.TabIndex = 0;
			this.textBody.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(29, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 14);
			this.label2.TabIndex = 3;
			this.label2.Text = "Description";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(132, 30);
			this.textDescription.Multiline = true;
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(677, 52);
			this.textDescription.TabIndex = 4;
			this.textDescription.Text = "";
			// 
			// textReportName
			// 
			this.textReportName.Location = new System.Drawing.Point(132, 6);
			this.textReportName.Name = "textReportName";
			this.textReportName.Size = new System.Drawing.Size(509, 20);
			this.textReportName.TabIndex = 6;
			this.textReportName.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(30, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 14);
			this.label3.TabIndex = 5;
			this.label3.Text = "Letter Name";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(29, 91);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 14);
			this.label7.TabIndex = 7;
			this.label7.Text = "Text of Letter";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// multInput
			// 
			this.multInput.Location = new System.Drawing.Point(132, 302);
			this.multInput.Name = "multInput";
			this.multInput.Size = new System.Drawing.Size(677, 173);
			this.multInput.TabIndex = 8;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(18, 304);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(111, 38);
			this.label8.TabIndex = 9;
			this.label8.Text = "Default Values";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormLetterEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(921, 664);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.multInput);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textReportName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.textBody);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormLetterEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Letter";
			this.Load += new System.EventHandler(this.FormLetterEdit_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormLetterEdit_Load(object sender, System.EventArgs e) {
			if(IsNew){
				Reports.CurStruct=new ReportStruct();
				
			}
			Reports.AssembleReport();
			textReportName.Text=Reports.Cur.ReportName;
			textDescription.Text=Reports.Cur.Description;
			textBody.Text=Reports.Cur.ReportObjects["LetterBody"].StaticText;
			for(int i=0;i<Reports.Cur.ParameterFields.Count;i++){
				multInput.AddInputItem(Reports.Cur.ParameterFields[i].PromptingText
					,Reports.Cur.ParameterFields[i].ValueType
					,Reports.Cur.ParameterFields[i].DefaultValues
					,Reports.Cur.ParameterFields[i].EnumerationType
					,Reports.Cur.ParameterFields[i].DefCategory);
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		


	}
}





















