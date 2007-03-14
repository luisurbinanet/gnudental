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
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.Label label37;
		private System.Windows.Forms.TrackBar trackRadiographs;
		private System.Windows.Forms.TrackBar trackPhotos;
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.trackRadiographs = new System.Windows.Forms.TrackBar();
			this.label25 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label26 = new System.Windows.Forms.Label();
			this.label27 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.label30 = new System.Windows.Forms.Label();
			this.label31 = new System.Windows.Forms.Label();
			this.label32 = new System.Windows.Forms.Label();
			this.label33 = new System.Windows.Forms.Label();
			this.label34 = new System.Windows.Forms.Label();
			this.label35 = new System.Windows.Forms.Label();
			this.label36 = new System.Windows.Forms.Label();
			this.trackPhotos = new System.Windows.Forms.TrackBar();
			this.label37 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.trackQ)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackRadiographs)).BeginInit();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackPhotos)).BeginInit();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(718, 430);
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
			this.butCancel.Location = new System.Drawing.Point(718, 468);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// trackQ
			// 
			this.trackQ.LargeChange = 10;
			this.trackQ.Location = new System.Drawing.Point(131, 65);
			this.trackQ.Maximum = 100;
			this.trackQ.Name = "trackQ";
			this.trackQ.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackQ.Size = new System.Drawing.Size(45, 218);
			this.trackQ.SmallChange = 10;
			this.trackQ.TabIndex = 0;
			this.trackQ.TickFrequency = 10;
			this.trackQ.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.trackQ.Value = 40;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(89, 244);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 12);
			this.label1.TabIndex = 3;
			this.label1.Text = "10";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(89, 92);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 12);
			this.label2.TabIndex = 4;
			this.label2.Text = "90";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(89, 112);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 12);
			this.label3.TabIndex = 5;
			this.label3.Text = "80";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(89, 130);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(44, 12);
			this.label4.TabIndex = 6;
			this.label4.Text = "70";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(89, 150);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(44, 12);
			this.label5.TabIndex = 7;
			this.label5.Text = "60";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(89, 168);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(44, 12);
			this.label6.TabIndex = 8;
			this.label6.Text = "50";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.Location = new System.Drawing.Point(89, 188);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(44, 12);
			this.label7.TabIndex = 9;
			this.label7.Text = "40";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(89, 206);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(44, 12);
			this.label8.TabIndex = 10;
			this.label8.Text = "30";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(89, 226);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(44, 12);
			this.label9.TabIndex = 11;
			this.label9.Text = "20";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(13, 74);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(120, 12);
			this.label10.TabIndex = 12;
			this.label10.Text = "No compression - 100";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(89, 264);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(44, 12);
			this.label11.TabIndex = 13;
			this.label11.Text = "0";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(17, 28);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(139, 35);
			this.label12.TabIndex = 14;
			this.label12.Text = "JPEG Compression - Quality After Scanning";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(13, 293);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(176, 34);
			this.label13.TabIndex = 15;
			this.label13.Text = "Suggested setting for scanning documents is Greyscale, 150 dpi.";
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(9, 374);
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
			this.textCropDelta.Location = new System.Drawing.Point(8, 442);
			this.textCropDelta.Name = "textCropDelta";
			this.textCropDelta.TabIndex = 1;
			this.textCropDelta.Text = "";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.trackQ);
			this.groupBox1.Controls.Add(this.label13);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.textCropDelta);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(20, 19);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(269, 478);
			this.groupBox1.TabIndex = 17;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Documents";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Controls.Add(this.label15);
			this.groupBox2.Controls.Add(this.label16);
			this.groupBox2.Controls.Add(this.label17);
			this.groupBox2.Controls.Add(this.label18);
			this.groupBox2.Controls.Add(this.label19);
			this.groupBox2.Controls.Add(this.label20);
			this.groupBox2.Controls.Add(this.label21);
			this.groupBox2.Controls.Add(this.label22);
			this.groupBox2.Controls.Add(this.label23);
			this.groupBox2.Controls.Add(this.label24);
			this.groupBox2.Controls.Add(this.trackRadiographs);
			this.groupBox2.Controls.Add(this.label25);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(301, 19);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(237, 341);
			this.groupBox2.TabIndex = 18;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Radiographs";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(89, 168);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(44, 12);
			this.label14.TabIndex = 8;
			this.label14.Text = "50";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label15
			// 
			this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label15.Location = new System.Drawing.Point(89, 188);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(44, 12);
			this.label15.TabIndex = 9;
			this.label15.Text = "40";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(89, 206);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(44, 12);
			this.label16.TabIndex = 10;
			this.label16.Text = "30";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(13, 74);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(120, 12);
			this.label17.TabIndex = 12;
			this.label17.Text = "No compression - 100";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(89, 264);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(44, 12);
			this.label18.TabIndex = 13;
			this.label18.Text = "0";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label19
			// 
			this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label19.Location = new System.Drawing.Point(89, 92);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(44, 12);
			this.label19.TabIndex = 4;
			this.label19.Text = "90";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(89, 112);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(44, 12);
			this.label20.TabIndex = 5;
			this.label20.Text = "80";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(89, 244);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(44, 12);
			this.label21.TabIndex = 3;
			this.label21.Text = "10";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(89, 226);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(44, 12);
			this.label22.TabIndex = 11;
			this.label22.Text = "20";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(89, 130);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(44, 12);
			this.label23.TabIndex = 6;
			this.label23.Text = "70";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(89, 150);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(44, 12);
			this.label24.TabIndex = 7;
			this.label24.Text = "60";
			this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackRadiographs
			// 
			this.trackRadiographs.LargeChange = 10;
			this.trackRadiographs.Location = new System.Drawing.Point(131, 65);
			this.trackRadiographs.Maximum = 100;
			this.trackRadiographs.Name = "trackRadiographs";
			this.trackRadiographs.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackRadiographs.Size = new System.Drawing.Size(45, 218);
			this.trackRadiographs.SmallChange = 10;
			this.trackRadiographs.TabIndex = 0;
			this.trackRadiographs.TickFrequency = 10;
			this.trackRadiographs.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.trackRadiographs.Value = 90;
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(13, 292);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(176, 34);
			this.label25.TabIndex = 15;
			this.label25.Text = "Suggested setting for scanning radiographs is Greyscale, 600 dpi.";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label26);
			this.groupBox3.Controls.Add(this.label27);
			this.groupBox3.Controls.Add(this.label28);
			this.groupBox3.Controls.Add(this.label29);
			this.groupBox3.Controls.Add(this.label30);
			this.groupBox3.Controls.Add(this.label31);
			this.groupBox3.Controls.Add(this.label32);
			this.groupBox3.Controls.Add(this.label33);
			this.groupBox3.Controls.Add(this.label34);
			this.groupBox3.Controls.Add(this.label35);
			this.groupBox3.Controls.Add(this.label36);
			this.groupBox3.Controls.Add(this.trackPhotos);
			this.groupBox3.Controls.Add(this.label37);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(557, 19);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(235, 341);
			this.groupBox3.TabIndex = 19;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Photos";
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(89, 168);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(44, 12);
			this.label26.TabIndex = 8;
			this.label26.Text = "50";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label27
			// 
			this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label27.Location = new System.Drawing.Point(89, 188);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(44, 12);
			this.label27.TabIndex = 9;
			this.label27.Text = "40";
			this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label28
			// 
			this.label28.Location = new System.Drawing.Point(89, 206);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(44, 12);
			this.label28.TabIndex = 10;
			this.label28.Text = "30";
			this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label29
			// 
			this.label29.Location = new System.Drawing.Point(13, 74);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(120, 12);
			this.label29.TabIndex = 12;
			this.label29.Text = "No compression - 100";
			this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label30
			// 
			this.label30.Location = new System.Drawing.Point(89, 264);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(44, 12);
			this.label30.TabIndex = 13;
			this.label30.Text = "0";
			this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label31
			// 
			this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label31.Location = new System.Drawing.Point(89, 92);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(44, 12);
			this.label31.TabIndex = 4;
			this.label31.Text = "90";
			this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label32
			// 
			this.label32.Location = new System.Drawing.Point(89, 112);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(44, 12);
			this.label32.TabIndex = 5;
			this.label32.Text = "80";
			this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label33
			// 
			this.label33.Location = new System.Drawing.Point(89, 244);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(44, 12);
			this.label33.TabIndex = 3;
			this.label33.Text = "10";
			this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label34
			// 
			this.label34.Location = new System.Drawing.Point(89, 226);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(44, 12);
			this.label34.TabIndex = 11;
			this.label34.Text = "20";
			this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label35
			// 
			this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label35.Location = new System.Drawing.Point(89, 130);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(44, 12);
			this.label35.TabIndex = 6;
			this.label35.Text = "70";
			this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label36
			// 
			this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label36.Location = new System.Drawing.Point(89, 150);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(44, 12);
			this.label36.TabIndex = 7;
			this.label36.Text = "60";
			this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackPhotos
			// 
			this.trackPhotos.LargeChange = 10;
			this.trackPhotos.Location = new System.Drawing.Point(131, 65);
			this.trackPhotos.Maximum = 100;
			this.trackPhotos.Name = "trackPhotos";
			this.trackPhotos.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackPhotos.Size = new System.Drawing.Size(45, 218);
			this.trackPhotos.SmallChange = 10;
			this.trackPhotos.TabIndex = 0;
			this.trackPhotos.TickFrequency = 10;
			this.trackPhotos.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.trackPhotos.Value = 70;
			// 
			// label37
			// 
			this.label37.Location = new System.Drawing.Point(13, 292);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(176, 34);
			this.label37.TabIndex = 15;
			this.label37.Text = "Suggested setting for scanning photos is Color, 300 dpi.";
			// 
			// FormScannerSetup
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(820, 515);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
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
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.trackRadiographs)).EndInit();
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.trackPhotos)).EndInit();
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
			Prefs.Cur=(Pref)Prefs.HList["ScannerCompressionRadiographs"];
			Prefs.Cur.ValueString=trackRadiographs.Value.ToString();
			Prefs.UpdateCur();
			Prefs.Cur=(Pref)Prefs.HList["ScannerCompressionPhotos"];
			Prefs.Cur.ValueString=trackPhotos.Value.ToString();
			Prefs.UpdateCur();
			Prefs.Cur=(Pref)Prefs.HList["CropDelta"];
			Prefs.Cur.ValueString=textCropDelta.Text;
			Prefs.UpdateCur();
			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
			//Defs.IsSelected=false;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	}
}
