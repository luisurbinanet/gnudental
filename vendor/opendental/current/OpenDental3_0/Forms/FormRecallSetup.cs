using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormRecallSetup : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textPattern;
		private System.Windows.Forms.TextBox textProcs;
		private System.Windows.Forms.TextBox textBW;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox6;
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormRecallSetup(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				label1,
				label2,
				label3,
				label4,
				label5,
				label6,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
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
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textPattern = new System.Windows.Forms.TextBox();
			this.textProcs = new System.Windows.Forms.TextBox();
			this.textBW = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(594, 301);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(594, 259);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(56, 97);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Time Pattern";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(54, 131);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Procedures";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(-2, 187);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(156, 16);
			this.label3.TabIndex = 4;
			this.label3.Text = "BiteWings code every year";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(20, 20);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(474, 30);
			this.textBox1.TabIndex = 5;
			this.textBox1.Text = "The following information is used to automate the process of creating recall appo" +
				"intments from the recall list.  You can make changes to the appointment after it" +
				" has been created.";
			// 
			// textPattern
			// 
			this.textPattern.Location = new System.Drawing.Point(156, 94);
			this.textPattern.Name = "textPattern";
			this.textPattern.Size = new System.Drawing.Size(170, 20);
			this.textPattern.TabIndex = 0;
			this.textPattern.Text = "";
			// 
			// textProcs
			// 
			this.textProcs.Location = new System.Drawing.Point(156, 128);
			this.textProcs.Multiline = true;
			this.textProcs.Name = "textProcs";
			this.textProcs.Size = new System.Drawing.Size(336, 42);
			this.textProcs.TabIndex = 1;
			this.textProcs.Text = "";
			// 
			// textBW
			// 
			this.textBW.Location = new System.Drawing.Point(156, 184);
			this.textBW.Name = "textBW";
			this.textBW.TabIndex = 2;
			this.textBW.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(337, 96);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(255, 19);
			this.label4.TabIndex = 9;
			this.label4.Text = "(must contain only /\'s and X\'s)";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(502, 135);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(183, 27);
			this.label5.TabIndex = 10;
			this.label5.Text = "(valid codes separated by commas)";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(266, 188);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(228, 23);
			this.label6.TabIndex = 11;
			this.label6.Text = "(leave blank to disable automated BW\'s)";
			// 
			// textBox6
			// 
			this.textBox6.BackColor = System.Drawing.SystemColors.Control;
			this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox6.Location = new System.Drawing.Point(41, 289);
			this.textBox6.Multiline = true;
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(361, 30);
			this.textBox6.TabIndex = 15;
			this.textBox6.Text = "For now, children under 12 do not have their procedures automatically attached.  " +
				"Their appointments are created blank.";
			// 
			// FormRecallSetup
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(697, 359);
			this.Controls.Add(this.textBox6);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBW);
			this.Controls.Add(this.textProcs);
			this.Controls.Add(this.textPattern);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRecallSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Setup Recall";
			this.Load += new System.EventHandler(this.FormRecallSetup_Load);
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

			Prefs.Cur.PrefName="RecallPattern";
			Prefs.Cur.ValueString=textPattern.Text;//savePattern.ToString();
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="RecallProcedures";
			Prefs.Cur.ValueString=textProcs.Text;
			Prefs.UpdateCur();

			Prefs.Cur.PrefName="RecallBW";
			Prefs.Cur.ValueString=textBW.Text;
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
