using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormScannerSetup : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TrackBar trackQ;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textBox1;
		private OpenDental.ValidDouble textCropDelta;

		///<summary></summary>
		public FormScannerSetup(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				label1,
				label2,
				label3,
				label4,
				label5,
				label6,
				label7,
				label8,
				label9,
				label10,
				label11,
				label12,
				label13,
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
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.trackQ = new System.Windows.Forms.TrackBar();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textCropDelta = new OpenDental.ValidDouble();
			((System.ComponentModel.ISupportInitialize)(this.trackQ)).BeginInit();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(498, 345);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(498, 383);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// trackQ
			// 
			this.trackQ.LargeChange = 10;
			this.trackQ.Location = new System.Drawing.Point(46, 60);
			this.trackQ.Maximum = 100;
			this.trackQ.Name = "trackQ";
			this.trackQ.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackQ.Size = new System.Drawing.Size(45, 218);
			this.trackQ.SmallChange = 10;
			this.trackQ.TabIndex = 0;
			this.trackQ.TickFrequency = 10;
			this.trackQ.Value = 40;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(78, 238);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 12);
			this.label1.TabIndex = 3;
			this.label1.Text = "10";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(78, 86);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 12);
			this.label2.TabIndex = 4;
			this.label2.Text = "90";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(78, 106);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 12);
			this.label3.TabIndex = 5;
			this.label3.Text = "80";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(78, 124);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(44, 12);
			this.label4.TabIndex = 6;
			this.label4.Text = "70";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(78, 144);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(44, 12);
			this.label5.TabIndex = 7;
			this.label5.Text = "60";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(78, 162);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(44, 12);
			this.label6.TabIndex = 8;
			this.label6.Text = "50";
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.Location = new System.Drawing.Point(78, 182);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(44, 12);
			this.label7.TabIndex = 9;
			this.label7.Text = "40";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(78, 200);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(44, 12);
			this.label8.TabIndex = 10;
			this.label8.Text = "30";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(78, 220);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(44, 12);
			this.label9.TabIndex = 11;
			this.label9.Text = "20";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(78, 68);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(120, 12);
			this.label10.TabIndex = 12;
			this.label10.Text = "100 - No compression";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(78, 258);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(44, 12);
			this.label11.TabIndex = 13;
			this.label11.Text = "0";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(40, 10);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(80, 52);
			this.label12.TabIndex = 14;
			this.label12.Text = "JPEG Compression - Quality After Scanning";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(268, 367);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(176, 34);
			this.label13.TabIndex = 15;
			this.label13.Text = "Suggested setting for scanning documents is Greyscale, 150 dpi.";
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(306, 15);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(251, 76);
			this.textBox1.TabIndex = 16;
			this.textBox1.Text = "Delta threshhold for automatic crop.  Recommend 0.035.  Smaller setting will incr" +
				"ease sensitivity and tend to crop documents to larger size.  Useful range is abo" +
				"ut 0.01 to 0.05.";
			// 
			// textCropDelta
			// 
			this.textCropDelta.Location = new System.Drawing.Point(307, 100);
			this.textCropDelta.Name = "textCropDelta";
			this.textCropDelta.TabIndex = 1;
			this.textCropDelta.Text = "";
			// 
			// FormScannerSetup
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(605, 436);
			this.Controls.Add(this.textCropDelta);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.trackQ);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormScannerSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Scanner Setup";
			this.Load += new System.EventHandler(this.FormScannerSetup_Load);
			((System.ComponentModel.ISupportInitialize)(this.trackQ)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void FormScannerSetup_Load(object sender, System.EventArgs e) {
			Prefs.Cur=(Pref)Prefs.HList["ScannerCompression"];
			try{
				trackQ.Value=Convert.ToInt32(Prefs.Cur.ValueString);
			}
			catch{}
			textCropDelta.Text=((Pref)Prefs.HList["CropDelta"]).ValueString;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(  textCropDelta.errorProvider1.GetError(textCropDelta)!=""
				//|| textAmount.errorProvider1.GetError(textAmount)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			Prefs.Cur=(Pref)Prefs.HList["ScannerCompression"];
			Prefs.Cur.ValueString=trackQ.Value.ToString();
			Prefs.UpdateCur();
			Prefs.Cur=(Pref)Prefs.HList["CropDelta"];
			Prefs.Cur.ValueString=textCropDelta.Text;
			Prefs.UpdateCur();
			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
			Defs.IsSelected=false;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void label5_Click(object sender, System.EventArgs e) {
		
		}
		
	}
}
