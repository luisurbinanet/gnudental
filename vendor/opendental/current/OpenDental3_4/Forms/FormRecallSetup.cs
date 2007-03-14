using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormRecallSetup : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.TextBox textPostcardMessage;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textPostcardsPerSheet;
		private System.Windows.Forms.ListBox listProcs;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBW;
		private System.Windows.Forms.TextBox textProcs;
		private System.Windows.Forms.TextBox textPattern;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label9;
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormRecallSetup(){
			InitializeComponent();
			Lan.F(this);
			Lan.C(this, new System.Windows.Forms.Control[] {
				textBox1,
				textBox6
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.textPostcardMessage = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textPostcardsPerSheet = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.listProcs = new System.Windows.Forms.ListBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBW = new System.Windows.Forms.TextBox();
			this.textProcs = new System.Windows.Forms.TextBox();
			this.textPattern = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(684, 509);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 4;
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
			this.butOK.Location = new System.Drawing.Point(684, 467);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(47, 21);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(500, 30);
			this.textBox1.TabIndex = 5;
			this.textBox1.Text = "The following information is used to automate the process of creating recall appo" +
				"intments from the recall list.  You can make changes to the appointment after it" +
				" has been created.";
			// 
			// textBox6
			// 
			this.textBox6.BackColor = System.Drawing.SystemColors.Control;
			this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox6.Location = new System.Drawing.Point(23, 178);
			this.textBox6.Multiline = true;
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(672, 20);
			this.textBox6.TabIndex = 15;
			this.textBox6.Text = "For now, children under 12 do not have their procedures automatically attached.  " +
				"Their appointments are created blank.";
			// 
			// textPostcardMessage
			// 
			this.textPostcardMessage.AcceptsReturn = true;
			this.textPostcardMessage.Location = new System.Drawing.Point(176, 232);
			this.textPostcardMessage.MaxLength = 255;
			this.textPostcardMessage.Multiline = true;
			this.textPostcardMessage.Name = "textPostcardMessage";
			this.textPostcardMessage.Size = new System.Drawing.Size(466, 118);
			this.textPostcardMessage.TabIndex = 16;
			this.textPostcardMessage.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(44, 235);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(132, 90);
			this.label7.TabIndex = 17;
			this.label7.Text = "Postcard message.  Use ?DateDue wherever you want the date due to be inserted.";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPostcardsPerSheet
			// 
			this.textPostcardsPerSheet.Location = new System.Drawing.Point(176, 359);
			this.textPostcardsPerSheet.Name = "textPostcardsPerSheet";
			this.textPostcardsPerSheet.Size = new System.Drawing.Size(34, 20);
			this.textPostcardsPerSheet.TabIndex = 18;
			this.textPostcardsPerSheet.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(49, 362);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(127, 35);
			this.label8.TabIndex = 19;
			this.label8.Text = "Postcards per sheet (1,3,or 4)";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listProcs
			// 
			this.listProcs.BackColor = System.Drawing.SystemColors.Control;
			this.listProcs.Location = new System.Drawing.Point(176, 410);
			this.listProcs.Name = "listProcs";
			this.listProcs.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.listProcs.Size = new System.Drawing.Size(130, 121);
			this.listProcs.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBW);
			this.groupBox1.Controls.Add(this.textProcs);
			this.groupBox1.Controls.Add(this.textPattern);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.textBox6);
			this.groupBox1.Location = new System.Drawing.Point(16, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(737, 205);
			this.groupBox1.TabIndex = 21;
			this.groupBox1.TabStop = false;
			// 
			// textBW
			// 
			this.textBW.Location = new System.Drawing.Point(159, 139);
			this.textBW.Name = "textBW";
			this.textBW.TabIndex = 15;
			this.textBW.Text = "";
			// 
			// textProcs
			// 
			this.textProcs.Location = new System.Drawing.Point(159, 89);
			this.textProcs.Multiline = true;
			this.textProcs.Name = "textProcs";
			this.textProcs.Size = new System.Drawing.Size(336, 42);
			this.textProcs.TabIndex = 13;
			this.textProcs.Text = "";
			// 
			// textPattern
			// 
			this.textPattern.Location = new System.Drawing.Point(159, 62);
			this.textPattern.Name = "textPattern";
			this.textPattern.Size = new System.Drawing.Size(170, 20);
			this.textPattern.TabIndex = 12;
			this.textPattern.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(266, 140);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(445, 15);
			this.label6.TabIndex = 20;
			this.label6.Text = "(leave blank to disable automated BW\'s)";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(499, 90);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(216, 34);
			this.label5.TabIndex = 19;
			this.label5.Text = "(valid codes separated by commas)";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(340, 64);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(255, 19);
			this.label4.TabIndex = 18;
			this.label4.Text = "(must contain only /\'s and X\'s)";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(1, 142);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(156, 16);
			this.label3.TabIndex = 17;
			this.label3.Text = "BiteWings code every year";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(14, 92);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(143, 16);
			this.label2.TabIndex = 16;
			this.label2.Text = "Procedures";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(19, 65);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(140, 16);
			this.label1.TabIndex = 14;
			this.label1.Text = "Time Pattern";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(20, 409);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(151, 88);
			this.label9.TabIndex = 22;
			this.label9.Text = "Procedures that Trigger Recall - You can change these in procedure code setup";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormRecallSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(783, 550);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textPostcardsPerSheet);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.textPostcardMessage);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.listProcs);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRecallSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Setup Recall";
			this.Load += new System.EventHandler(this.FormRecallSetup_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRecallSetup_Load(object sender, System.EventArgs e) {
			//convert time pattern from 5 to current increment.
			/*StringBuilder strBTime=new StringBuilder();
			for(int i=0;i<Prefs.GetString("RecallPattern").Length;i++){
				strBTime.Append(Prefs.GetString("RecallPattern").Substring(i,1));
				i++;
				if(Prefs.GetInt("AppointmentTimeIncrement")==15){
					i++;
				}
			}*/
			//textPattern.Text=strBTime.ToString();
			textPattern.Text=Prefs.GetString("RecallPattern");
			textProcs.Text=((Pref)Prefs.HList["RecallProcedures"]).ValueString;
			textBW.Text=((Pref)Prefs.HList["RecallBW"]).ValueString;
			textPostcardMessage.Text=((Pref)Prefs.HList["RecallPostcardMessage"]).ValueString;
			textPostcardsPerSheet.Text=Prefs.GetInt("RecallPostcardsPerSheet").ToString();
			listProcs.Items.Clear();
			for(int i=0;i<ProcedureCodes.RecallAL.Count;i++){
				listProcs.Items.Add(((ProcedureCode)ProcedureCodes.RecallAL[i]).Descript);
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//convert time pattern to 5 minute increment
			/*StringBuilder savePattern=new StringBuilder();
			for(int i=0;i<textPattern.Text.Length;i++){
				savePattern.Append(textPattern.Text.Substring(i,1));
				savePattern.Append(textPattern.Text.Substring(i,1));
				if(Prefs.GetInt("AppointmentTimeIncrement")==15){
					savePattern.Append(textPattern.Text.Substring(i,1));
				}
			}*/
			if(textPostcardsPerSheet.Text!="1"
				&& textPostcardsPerSheet.Text!="3"
				&& textPostcardsPerSheet.Text!="4")
			{
				MsgBox.Show(this,"The value in postcards per sheet must be 1, 3, or 4");
				return;
			}

			Prefs.Cur.PrefName="RecallPattern";
			Prefs.Cur.ValueString=textPattern.Text;//savePattern.ToString();
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="RecallProcedures";
			Prefs.Cur.ValueString=textProcs.Text;
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="RecallBW";
			Prefs.Cur.ValueString=textBW.Text;
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="RecallPostcardMessage";
			Prefs.Cur.ValueString=textPostcardMessage.Text;
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="RecallPostcardsPerSheet";
			Prefs.Cur.ValueString=textPostcardsPerSheet.Text;
			Prefs.UpdateCur();

			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}
