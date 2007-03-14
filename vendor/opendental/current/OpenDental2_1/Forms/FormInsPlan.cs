/*=============================================================================================================
FreeDental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormInsPlan : System.Windows.Forms.Form{
		private System.ComponentModel.Container components = null;// Required designer variable.
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.TextBox textPhone;
		private System.Windows.Forms.TextBox textGroupName;
		private System.Windows.Forms.TextBox textGroupNum;
		private System.Windows.Forms.TextBox textAddress;
		private System.Windows.Forms.TextBox textCity;
		private System.Windows.Forms.TextBox textState;
		private System.Windows.Forms.TextBox textZip;
		private System.Windows.Forms.TextBox textElectID;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.RadioButton radioDedUnkn;
		private System.Windows.Forms.RadioButton radioDedNo;
		private System.Windows.Forms.RadioButton radioDedYes;
		private System.Windows.Forms.TextBox textCarrier;
		private OpenDental.ValidDate textDateEffect;
		private OpenDental.ValidDate textDateTerm;
		private System.Windows.Forms.ListBox listSubscriber;
		private System.Windows.Forms.ListBox listClaimFormat;
		public bool IsNew;
		private System.Windows.Forms.TextBox textPlanNote;
		private System.Windows.Forms.TextBox textEmployer;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.RadioButton radioMissUnkn;
		private System.Windows.Forms.RadioButton radioMissNo;
		private System.Windows.Forms.RadioButton radioMissYes;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.RadioButton radioWaitUnkn;
		private System.Windows.Forms.RadioButton radioWaitNo;
		private System.Windows.Forms.RadioButton radioWaitYes;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.TextBox textAddress2;
		private System.Windows.Forms.Label label21;
		private OpenDental.TablePercent tbPercent1;
		private OpenDental.ValidNumber textAnnualMax;
		private OpenDental.ValidNumber textOrthoMax;
		private OpenDental.ValidNumber textRenewMonth;
		private OpenDental.ValidNumber textDeductible;
		private OpenDental.ValidNumber textFloToAge;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.ListBox listFeeSched;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkRelease;
		private System.Windows.Forms.CheckBox checkAssign;

		public FormInsPlan(){
			InitializeComponent();
			tbPercent1.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbPercent1_CellClicked);
			Lan.C(this, new System.Windows.Forms.Control[] {
				label2,
				label4,
				label3,
				label5,
				label6,
				label7,
				label8,
				label9,
				label10,
				label11,
				label12,
				label13,
				label14,
				label15,
				label17,
				label18,
				label19,
				label20,
				label24,
				label27,
				label28,
				panel3,
				radioDedUnkn,
				radioDedNo,
				radioDedYes,
				label16,
				panel2,
				radioMissUnkn,
				radioMissNo,
				radioMissYes,
				label29,
				panel4,
				radioWaitUnkn,
				radioWaitNo,
				radioWaitYes,
				label30,
				label21,
				label22,
				label1,
				groupBox1,
				checkRelease,
				checkAssign,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel
			});

		}

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
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.label27 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.textCarrier = new System.Windows.Forms.TextBox();
			this.textPhone = new System.Windows.Forms.TextBox();
			this.textGroupName = new System.Windows.Forms.TextBox();
			this.textGroupNum = new System.Windows.Forms.TextBox();
			this.textAddress = new System.Windows.Forms.TextBox();
			this.textCity = new System.Windows.Forms.TextBox();
			this.textState = new System.Windows.Forms.TextBox();
			this.textZip = new System.Windows.Forms.TextBox();
			this.textElectID = new System.Windows.Forms.TextBox();
			this.textPlanNote = new System.Windows.Forms.TextBox();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.panel4 = new System.Windows.Forms.Panel();
			this.radioWaitUnkn = new System.Windows.Forms.RadioButton();
			this.radioWaitNo = new System.Windows.Forms.RadioButton();
			this.radioWaitYes = new System.Windows.Forms.RadioButton();
			this.label30 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.radioMissUnkn = new System.Windows.Forms.RadioButton();
			this.radioMissNo = new System.Windows.Forms.RadioButton();
			this.radioMissYes = new System.Windows.Forms.RadioButton();
			this.label29 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.radioDedUnkn = new System.Windows.Forms.RadioButton();
			this.radioDedNo = new System.Windows.Forms.RadioButton();
			this.radioDedYes = new System.Windows.Forms.RadioButton();
			this.textDateEffect = new OpenDental.ValidDate();
			this.textDateTerm = new OpenDental.ValidDate();
			this.listSubscriber = new System.Windows.Forms.ListBox();
			this.listClaimFormat = new System.Windows.Forms.ListBox();
			this.textEmployer = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.textAddress2 = new System.Windows.Forms.TextBox();
			this.label21 = new System.Windows.Forms.Label();
			this.tbPercent1 = new OpenDental.TablePercent();
			this.textAnnualMax = new OpenDental.ValidNumber();
			this.textOrthoMax = new OpenDental.ValidNumber();
			this.textRenewMonth = new OpenDental.ValidNumber();
			this.textDeductible = new OpenDental.ValidNumber();
			this.textFloToAge = new OpenDental.ValidNumber();
			this.label22 = new System.Windows.Forms.Label();
			this.listFeeSched = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkAssign = new System.Windows.Forms.CheckBox();
			this.checkRelease = new System.Windows.Forms.CheckBox();
			this.panel4.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(38, 46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(62, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Subscriber";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(26, 29);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 12);
			this.label4.TabIndex = 3;
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(29, 28);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 12);
			this.label3.TabIndex = 4;
			this.label3.Text = "Carrier";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6, 184);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(92, 12);
			this.label5.TabIndex = 5;
			this.label5.Text = "Date Effective";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(30, 204);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(72, 12);
			this.label6.TabIndex = 6;
			this.label6.Text = "Date Term.";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(26, 224);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(72, 12);
			this.label7.TabIndex = 7;
			this.label7.Text = "Phone";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(28, 244);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(72, 16);
			this.label8.TabIndex = 8;
			this.label8.Text = "Group Name";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(26, 264);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(72, 12);
			this.label9.TabIndex = 9;
			this.label9.Text = "Group Num";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(26, 304);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(72, 12);
			this.label10.TabIndex = 10;
			this.label10.Text = "Address";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(26, 348);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(72, 16);
			this.label11.TabIndex = 11;
			this.label11.Text = "City";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(24, 368);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(72, 12);
			this.label12.TabIndex = 12;
			this.label12.Text = "State";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(24, 388);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(72, 16);
			this.label13.TabIndex = 13;
			this.label13.Text = "Zip";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(17, 408);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(84, 16);
			this.label14.TabIndex = 14;
			this.label14.Text = "Claim Format";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(27, 478);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(72, 12);
			this.label15.TabIndex = 15;
			this.label15.Text = "Electronic ID";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(406, 20);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(86, 12);
			this.label17.TabIndex = 17;
			this.label17.Text = "Annual Max $";
			this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(412, 64);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(80, 12);
			this.label18.TabIndex = 18;
			this.label18.Text = "Renew Month";
			this.label18.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(418, 84);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(72, 12);
			this.label19.TabIndex = 19;
			this.label19.Text = "Deductible $";
			this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(408, 106);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(80, 26);
			this.label20.TabIndex = 20;
			this.label20.Text = "Deduct waived on Prevent?";
			this.label20.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(418, 42);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(72, 12);
			this.label24.TabIndex = 24;
			this.label24.Text = "Ortho Max $";
			this.label24.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label27
			// 
			this.label27.Location = new System.Drawing.Point(417, 272);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(72, 16);
			this.label27.TabIndex = 27;
			this.label27.Text = "Flo to Age";
			this.label27.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label28
			// 
			this.label28.Location = new System.Drawing.Point(426, 450);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(88, 16);
			this.label28.TabIndex = 28;
			this.label28.Text = "Ins Plan Note";
			// 
			// textCarrier
			// 
			this.textCarrier.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textCarrier.Location = new System.Drawing.Point(102, 23);
			this.textCarrier.MaxLength = 50;
			this.textCarrier.Name = "textCarrier";
			this.textCarrier.Size = new System.Drawing.Size(176, 21);
			this.textCarrier.TabIndex = 0;
			this.textCarrier.Text = "";
			// 
			// textPhone
			// 
			this.textPhone.Location = new System.Drawing.Point(102, 220);
			this.textPhone.MaxLength = 30;
			this.textPhone.Name = "textPhone";
			this.textPhone.Size = new System.Drawing.Size(157, 20);
			this.textPhone.TabIndex = 4;
			this.textPhone.Text = "";
			this.textPhone.TextChanged += new System.EventHandler(this.textPhone_TextChanged);
			// 
			// textGroupName
			// 
			this.textGroupName.Location = new System.Drawing.Point(102, 240);
			this.textGroupName.MaxLength = 50;
			this.textGroupName.Name = "textGroupName";
			this.textGroupName.Size = new System.Drawing.Size(193, 20);
			this.textGroupName.TabIndex = 5;
			this.textGroupName.Text = "";
			// 
			// textGroupNum
			// 
			this.textGroupNum.Location = new System.Drawing.Point(102, 260);
			this.textGroupNum.MaxLength = 20;
			this.textGroupNum.Name = "textGroupNum";
			this.textGroupNum.Size = new System.Drawing.Size(129, 20);
			this.textGroupNum.TabIndex = 6;
			this.textGroupNum.Text = "";
			// 
			// textAddress
			// 
			this.textAddress.Location = new System.Drawing.Point(102, 300);
			this.textAddress.MaxLength = 60;
			this.textAddress.Name = "textAddress";
			this.textAddress.Size = new System.Drawing.Size(192, 20);
			this.textAddress.TabIndex = 8;
			this.textAddress.Text = "";
			// 
			// textCity
			// 
			this.textCity.Location = new System.Drawing.Point(102, 344);
			this.textCity.MaxLength = 40;
			this.textCity.Name = "textCity";
			this.textCity.Size = new System.Drawing.Size(155, 20);
			this.textCity.TabIndex = 10;
			this.textCity.Text = "";
			// 
			// textState
			// 
			this.textState.Location = new System.Drawing.Point(102, 364);
			this.textState.MaxLength = 2;
			this.textState.Name = "textState";
			this.textState.Size = new System.Drawing.Size(34, 20);
			this.textState.TabIndex = 11;
			this.textState.Text = "";
			// 
			// textZip
			// 
			this.textZip.Location = new System.Drawing.Point(102, 384);
			this.textZip.MaxLength = 10;
			this.textZip.Name = "textZip";
			this.textZip.Size = new System.Drawing.Size(72, 20);
			this.textZip.TabIndex = 12;
			this.textZip.Text = "";
			// 
			// textElectID
			// 
			this.textElectID.Location = new System.Drawing.Point(102, 474);
			this.textElectID.MaxLength = 5;
			this.textElectID.Name = "textElectID";
			this.textElectID.Size = new System.Drawing.Size(54, 20);
			this.textElectID.TabIndex = 14;
			this.textElectID.Text = "";
			// 
			// textPlanNote
			// 
			this.textPlanNote.Location = new System.Drawing.Point(427, 475);
			this.textPlanNote.Multiline = true;
			this.textPlanNote.Name = "textPlanNote";
			this.textPlanNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textPlanNote.Size = new System.Drawing.Size(350, 150);
			this.textPlanNote.TabIndex = 27;
			this.textPlanNote.Text = "";
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(676, 658);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 59;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(766, 658);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 60;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.radioWaitUnkn);
			this.panel4.Controls.Add(this.radioWaitNo);
			this.panel4.Controls.Add(this.radioWaitYes);
			this.panel4.Location = new System.Drawing.Point(493, 338);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(168, 12);
			this.panel4.TabIndex = 25;
			// 
			// radioWaitUnkn
			// 
			this.radioWaitUnkn.Location = new System.Drawing.Point(104, -1);
			this.radioWaitUnkn.Name = "radioWaitUnkn";
			this.radioWaitUnkn.Size = new System.Drawing.Size(80, 15);
			this.radioWaitUnkn.TabIndex = 2;
			this.radioWaitUnkn.Text = "Unknown";
			this.radioWaitUnkn.Click += new System.EventHandler(this.radioWaitUnkn_Click);
			// 
			// radioWaitNo
			// 
			this.radioWaitNo.Location = new System.Drawing.Point(52, -1);
			this.radioWaitNo.Name = "radioWaitNo";
			this.radioWaitNo.Size = new System.Drawing.Size(48, 15);
			this.radioWaitNo.TabIndex = 1;
			this.radioWaitNo.Text = "No";
			this.radioWaitNo.Click += new System.EventHandler(this.radioWaitNo_Click);
			// 
			// radioWaitYes
			// 
			this.radioWaitYes.Location = new System.Drawing.Point(0, -1);
			this.radioWaitYes.Name = "radioWaitYes";
			this.radioWaitYes.Size = new System.Drawing.Size(46, 15);
			this.radioWaitYes.TabIndex = 0;
			this.radioWaitYes.Text = "Yes";
			this.radioWaitYes.Click += new System.EventHandler(this.radioWaitYes_Click);
			// 
			// label30
			// 
			this.label30.Location = new System.Drawing.Point(415, 330);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(72, 26);
			this.label30.TabIndex = 77;
			this.label30.Text = "Wait on Major Treat?";
			this.label30.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.radioMissUnkn);
			this.panel2.Controls.Add(this.radioMissNo);
			this.panel2.Controls.Add(this.radioMissYes);
			this.panel2.Location = new System.Drawing.Point(493, 302);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(168, 12);
			this.panel2.TabIndex = 24;
			// 
			// radioMissUnkn
			// 
			this.radioMissUnkn.Location = new System.Drawing.Point(104, -1);
			this.radioMissUnkn.Name = "radioMissUnkn";
			this.radioMissUnkn.Size = new System.Drawing.Size(80, 15);
			this.radioMissUnkn.TabIndex = 2;
			this.radioMissUnkn.Text = "Unknown";
			this.radioMissUnkn.Click += new System.EventHandler(this.radioMissUnkn_Click);
			// 
			// radioMissNo
			// 
			this.radioMissNo.Location = new System.Drawing.Point(52, -1);
			this.radioMissNo.Name = "radioMissNo";
			this.radioMissNo.Size = new System.Drawing.Size(48, 15);
			this.radioMissNo.TabIndex = 1;
			this.radioMissNo.Text = "No";
			this.radioMissNo.Click += new System.EventHandler(this.radioMissNo_Click);
			// 
			// radioMissYes
			// 
			this.radioMissYes.Location = new System.Drawing.Point(0, -1);
			this.radioMissYes.Name = "radioMissYes";
			this.radioMissYes.Size = new System.Drawing.Size(46, 15);
			this.radioMissYes.TabIndex = 0;
			this.radioMissYes.Text = "Yes";
			this.radioMissYes.Click += new System.EventHandler(this.radioMissYes_Click);
			// 
			// label29
			// 
			this.label29.Location = new System.Drawing.Point(405, 296);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(84, 26);
			this.label29.TabIndex = 75;
			this.label29.Text = "Missing Tooth Exclusion?";
			this.label29.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.radioDedUnkn);
			this.panel3.Controls.Add(this.radioDedNo);
			this.panel3.Controls.Add(this.radioDedYes);
			this.panel3.Location = new System.Drawing.Point(492, 116);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(168, 12);
			this.panel3.TabIndex = 20;
			// 
			// radioDedUnkn
			// 
			this.radioDedUnkn.Location = new System.Drawing.Point(104, -1);
			this.radioDedUnkn.Name = "radioDedUnkn";
			this.radioDedUnkn.Size = new System.Drawing.Size(80, 15);
			this.radioDedUnkn.TabIndex = 2;
			this.radioDedUnkn.Text = "Unknown";
			this.radioDedUnkn.Click += new System.EventHandler(this.radioDedUnkn_Click);
			// 
			// radioDedNo
			// 
			this.radioDedNo.Location = new System.Drawing.Point(52, -1);
			this.radioDedNo.Name = "radioDedNo";
			this.radioDedNo.Size = new System.Drawing.Size(48, 15);
			this.radioDedNo.TabIndex = 1;
			this.radioDedNo.Text = "No";
			this.radioDedNo.Click += new System.EventHandler(this.radioDedNo_Click);
			// 
			// radioDedYes
			// 
			this.radioDedYes.Location = new System.Drawing.Point(0, -1);
			this.radioDedYes.Name = "radioDedYes";
			this.radioDedYes.Size = new System.Drawing.Size(46, 15);
			this.radioDedYes.TabIndex = 0;
			this.radioDedYes.Text = "Yes";
			this.radioDedYes.Click += new System.EventHandler(this.radioDedYes_Click);
			// 
			// textDateEffect
			// 
			this.textDateEffect.Location = new System.Drawing.Point(102, 180);
			this.textDateEffect.Name = "textDateEffect";
			this.textDateEffect.Size = new System.Drawing.Size(72, 20);
			this.textDateEffect.TabIndex = 2;
			this.textDateEffect.Text = "";
			// 
			// textDateTerm
			// 
			this.textDateTerm.Location = new System.Drawing.Point(102, 200);
			this.textDateTerm.Name = "textDateTerm";
			this.textDateTerm.Size = new System.Drawing.Size(72, 20);
			this.textDateTerm.TabIndex = 3;
			this.textDateTerm.Text = "";
			// 
			// listSubscriber
			// 
			this.listSubscriber.Items.AddRange(new object[] {
																												""});
			this.listSubscriber.Location = new System.Drawing.Point(102, 45);
			this.listSubscriber.Name = "listSubscriber";
			this.listSubscriber.ScrollAlwaysVisible = true;
			this.listSubscriber.Size = new System.Drawing.Size(190, 134);
			this.listSubscriber.TabIndex = 1;
			// 
			// listClaimFormat
			// 
			this.listClaimFormat.Items.AddRange(new object[] {
																												 "Papr",
																												 "e-C"});
			this.listClaimFormat.Location = new System.Drawing.Point(102, 404);
			this.listClaimFormat.Name = "listClaimFormat";
			this.listClaimFormat.Size = new System.Drawing.Size(158, 69);
			this.listClaimFormat.TabIndex = 13;
			// 
			// textEmployer
			// 
			this.textEmployer.Location = new System.Drawing.Point(102, 280);
			this.textEmployer.MaxLength = 40;
			this.textEmployer.Name = "textEmployer";
			this.textEmployer.Size = new System.Drawing.Size(192, 20);
			this.textEmployer.TabIndex = 7;
			this.textEmployer.Text = "";
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(28, 284);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(72, 14);
			this.label16.TabIndex = 73;
			this.label16.Text = "Employer";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textAddress2
			// 
			this.textAddress2.Location = new System.Drawing.Point(102, 322);
			this.textAddress2.MaxLength = 60;
			this.textAddress2.Name = "textAddress2";
			this.textAddress2.Size = new System.Drawing.Size(192, 20);
			this.textAddress2.TabIndex = 9;
			this.textAddress2.Text = "";
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(26, 326);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(72, 12);
			this.label21.TabIndex = 79;
			this.label21.Text = "Address 2";
			this.label21.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// tbPercent1
			// 
			this.tbPercent1.BackColor = System.Drawing.SystemColors.Window;
			this.tbPercent1.Location = new System.Drawing.Point(493, 162);
			this.tbPercent1.Name = "tbPercent1";
			this.tbPercent1.SelectedIndices = new int[0];
			this.tbPercent1.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbPercent1.Size = new System.Drawing.Size(242, 86);
			this.tbPercent1.TabIndex = 22;
			// 
			// textAnnualMax
			// 
			this.textAnnualMax.Location = new System.Drawing.Point(494, 19);
			this.textAnnualMax.Name = "textAnnualMax";
			this.textAnnualMax.Size = new System.Drawing.Size(60, 20);
			this.textAnnualMax.TabIndex = 16;
			this.textAnnualMax.Text = "";
			// 
			// textOrthoMax
			// 
			this.textOrthoMax.Location = new System.Drawing.Point(494, 40);
			this.textOrthoMax.Name = "textOrthoMax";
			this.textOrthoMax.Size = new System.Drawing.Size(60, 20);
			this.textOrthoMax.TabIndex = 17;
			this.textOrthoMax.Text = "";
			// 
			// textRenewMonth
			// 
			this.textRenewMonth.Location = new System.Drawing.Point(494, 61);
			this.textRenewMonth.Name = "textRenewMonth";
			this.textRenewMonth.Size = new System.Drawing.Size(31, 20);
			this.textRenewMonth.TabIndex = 18;
			this.textRenewMonth.Text = "";
			// 
			// textDeductible
			// 
			this.textDeductible.Location = new System.Drawing.Point(494, 82);
			this.textDeductible.Name = "textDeductible";
			this.textDeductible.Size = new System.Drawing.Size(45, 20);
			this.textDeductible.TabIndex = 19;
			this.textDeductible.Text = "";
			// 
			// textFloToAge
			// 
			this.textFloToAge.Location = new System.Drawing.Point(493, 269);
			this.textFloToAge.Name = "textFloToAge";
			this.textFloToAge.Size = new System.Drawing.Size(36, 20);
			this.textFloToAge.TabIndex = 23;
			this.textFloToAge.Text = "";
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(492, 143);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(206, 17);
			this.label22.TabIndex = 21;
			this.label22.Text = "Plan Percentages (single click to edit)";
			// 
			// listFeeSched
			// 
			this.listFeeSched.Location = new System.Drawing.Point(102, 496);
			this.listFeeSched.Name = "listFeeSched";
			this.listFeeSched.Size = new System.Drawing.Size(108, 121);
			this.listFeeSched.TabIndex = 15;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 500);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(83, 12);
			this.label1.TabIndex = 91;
			this.label1.Text = "Fee Schedule";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkAssign);
			this.groupBox1.Controls.Add(this.checkRelease);
			this.groupBox1.Location = new System.Drawing.Point(481, 370);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 65);
			this.groupBox1.TabIndex = 26;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Signatures on File";
			// 
			// checkAssign
			// 
			this.checkAssign.Location = new System.Drawing.Point(12, 41);
			this.checkAssign.Name = "checkAssign";
			this.checkAssign.Size = new System.Drawing.Size(150, 16);
			this.checkAssign.TabIndex = 1;
			this.checkAssign.Text = "Assignment of Benefits";
			// 
			// checkRelease
			// 
			this.checkRelease.Location = new System.Drawing.Point(12, 19);
			this.checkRelease.Name = "checkRelease";
			this.checkRelease.Size = new System.Drawing.Size(140, 17);
			this.checkRelease.TabIndex = 0;
			this.checkRelease.Text = "Release of Information";
			// 
			// FormInsPlan
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(866, 696);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listFeeSched);
			this.Controls.Add(this.label22);
			this.Controls.Add(this.textFloToAge);
			this.Controls.Add(this.textDeductible);
			this.Controls.Add(this.textRenewMonth);
			this.Controls.Add(this.textOrthoMax);
			this.Controls.Add(this.textAnnualMax);
			this.Controls.Add(this.tbPercent1);
			this.Controls.Add(this.textAddress2);
			this.Controls.Add(this.label21);
			this.Controls.Add(this.textEmployer);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.listClaimFormat);
			this.Controls.Add(this.listSubscriber);
			this.Controls.Add(this.textDateTerm);
			this.Controls.Add(this.textDateEffect);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.textElectID);
			this.Controls.Add(this.textZip);
			this.Controls.Add(this.textState);
			this.Controls.Add(this.textCity);
			this.Controls.Add(this.textAddress);
			this.Controls.Add(this.textGroupNum);
			this.Controls.Add(this.textGroupName);
			this.Controls.Add(this.textPhone);
			this.Controls.Add(this.textCarrier);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textPlanNote);
			this.Controls.Add(this.label28);
			this.Controls.Add(this.label29);
			this.Controls.Add(this.label24);
			this.Controls.Add(this.label27);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.label30);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.panel4);
			this.Controls.Add(this.label20);
			this.Name = "FormInsPlan";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = Lan.g(this,"Edit Insurance Plan");
			this.Load += new System.EventHandler(this.FormInsPlan_Load);
			this.panel4.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormInsPlan_Load(object sender, System.EventArgs e) {
			if(IsNew){
				for(int i=0;i<CovCats.ListShort.Length;i++){
					if(CovCats.ListShort[i].DefaultPercent==-1){
						continue;
					}
					CovPats.Cur=new CovPat();
					CovPats.Cur.CovCatNum=CovCats.ListShort[i].CovCatNum;
					CovPats.Cur.PlanNum=InsPlans.Cur.PlanNum;
					CovPats.Cur.Percent=CovCats.ListShort[i].DefaultPercent;
					CovPats.InsertCur();
				}
				InsPlans.Cur.ReleaseInfo=true;
				InsPlans.Cur.AssignBen=true;
      }
			listSubscriber.Items.Clear();
			for(int i=0;i<Patients.FamilyList.Length;i++){
				listSubscriber.Items.Add(Patients.GetNameInFamLFI(i));
				if(InsPlans.Cur.Subscriber==Patients.FamilyList[i].PatNum){
					listSubscriber.SelectedIndex=i;
				}
			}
			textCarrier.Text=InsPlans.Cur.Carrier;
			if(DateTime.Compare(InsPlans.Cur.DateEffective,DateTime.Parse("1/1/1880")) < 0)
				textDateEffect.Text="";
			else
				textDateEffect.Text=InsPlans.Cur.DateEffective.ToString("d");
			if(DateTime.Compare(InsPlans.Cur.DateTerm,DateTime.Parse("1/1/1880")) < 0)
				textDateTerm.Text="";
			else
				textDateTerm.Text=InsPlans.Cur.DateTerm.ToString("d");
			textPhone.Text=InsPlans.Cur.Phone;
			textGroupName.Text=InsPlans.Cur.GroupName;
			textGroupNum.Text=InsPlans.Cur.GroupNum;
			textAddress.Text=InsPlans.Cur.Address;
			textAddress2.Text=InsPlans.Cur.Address2;
			textCity.Text=InsPlans.Cur.City;
			textState.Text=InsPlans.Cur.State;
			textZip.Text=InsPlans.Cur.Zip;
			listClaimFormat.Items.Clear();
			for(int i=0;i<Defs.Short[(int)DefCat.ClaimFormats].Length;i++){
				listClaimFormat.Items.Add(Defs.Short[(int)DefCat.ClaimFormats][i].ItemName);
				if(InsPlans.Cur.ClaimFormat==Defs.Short[(int)DefCat.ClaimFormats][i].DefNum)
					listClaimFormat.SelectedIndex=i;
			}
			textElectID.Text=InsPlans.Cur.ElectID;
			textEmployer.Text=InsPlans.Cur.Employer;
			for(int i=0;i<Defs.Short[(int)DefCat.FeeSchedNames].Length;i++){
				listFeeSched.Items.Add(Defs.Short[(int)DefCat.FeeSchedNames][i].ItemName);
				if(Defs.Short[(int)DefCat.FeeSchedNames][i].DefNum==InsPlans.Cur.FeeSched)
					listFeeSched.SelectedIndex=i;
			}
			textAnnualMax.MaxVal=50000;
			if(InsPlans.Cur.AnnualMax==-1)
				textAnnualMax.Text="";
			else
				textAnnualMax.Text=InsPlans.Cur.AnnualMax.ToString();
			textOrthoMax.MaxVal=50000;
			if(InsPlans.Cur.OrthoMax==-1)
				textOrthoMax.Text="";
			else
				textOrthoMax.Text=InsPlans.Cur.OrthoMax.ToString();
			textRenewMonth.MaxVal=12;
			if(InsPlans.Cur.RenewMonth==-1)
				textRenewMonth.Text="";
			else
				textRenewMonth.Text=InsPlans.Cur.RenewMonth.ToString();
			textDeductible.MaxVal=10000;
			if(InsPlans.Cur.Deductible==-1)
				textDeductible.Text="";
			else
				textDeductible.Text=InsPlans.Cur.Deductible.ToString();
			switch (InsPlans.Cur.DeductWaivPrev){
				case YN.Unknown:radioDedUnkn.Checked=true;break;
				case YN.Yes:radioDedYes.Checked=true;break;
				case YN.No:radioDedNo.Checked=true;break;
			}
			textFloToAge.MaxVal=100;
			if(InsPlans.Cur.FloToAge==-1)
				textFloToAge.Text="";
			else
				textFloToAge.Text=InsPlans.Cur.FloToAge.ToString();
			textPlanNote.Text=InsPlans.Cur.PlanNote;
			switch (InsPlans.Cur.MissToothExcl){
				case YN.Unknown:radioMissUnkn.Checked=true;break;
				case YN.Yes:radioMissYes.Checked=true;break;
				case YN.No:radioMissNo.Checked=true;break;
			}
			switch (InsPlans.Cur.MajorWait){
				case YN.Unknown:radioWaitUnkn.Checked=true;break;
				case YN.Yes:radioWaitYes.Checked=true;break;
				case YN.No:radioWaitNo.Checked=true;break;
			}
			checkRelease.Checked=InsPlans.Cur.ReleaseInfo;
			checkAssign.Checked=InsPlans.Cur.AssignBen;
			FillTable();
		}

		private void FillTable(){
			CovPats.RefreshForPlan();
			tbPercent1.ResetRows(CovCats.ListShort.Length);
			tbPercent1.SetGridColor(Color.LightGray);
			for(int i=0;i<CovCats.ListShort.Length;i++){
				tbPercent1.Cell[0,i]=CovCats.ListShort[i].Description;
				tbPercent1.Cell[1,i]="";
			}
			for(int i=0;i<CovPats.ListForPlan.Length;i++){

				tbPercent1.Cell[1,CovCats.GetOrderShort(CovPats.ListForPlan[i].CovCatNum)]=CovPats.ListForPlan[i].Percent.ToString();
				
			}
			tbPercent1.LayoutTables();
		}

		private void tbPercent1_CellClicked(object sender, CellEventArgs e){
			bool isNew=true;
			for(int i=0;i<CovPats.ListForPlan.Length;i++){
				if(CovCats.GetOrderShort(CovPats.ListForPlan[i].CovCatNum)==e.Row){
					isNew=false;
					CovPats.Cur=CovPats.ListForPlan[i];
				}
			}
			FormPercentEdit FormPE=new FormPercentEdit();
			if(isNew){
				FormPE.RetVal=-1;
			}
			else{
				FormPE.RetVal=CovPats.Cur.Percent;
			}			
			FormPE.ShowDialog();
			if(FormPE.DialogResult!=DialogResult.OK){
				return;
			}
			if(isNew){
				if(FormPE.RetVal==-1){
					return;
				}
				CovPats.Cur=new CovPat();
				CovPats.Cur.CovCatNum=CovCats.ListShort[e.Row].CovCatNum;
				CovPats.Cur.PlanNum=InsPlans.Cur.PlanNum;
				CovPats.Cur.Percent=FormPE.RetVal;
				CovPats.InsertCur();
			}
			else{
				if(FormPE.RetVal==-1){
					CovPats.DeleteCur();
				}
				else{
					CovPats.Cur.Percent=FormPE.RetVal;
					CovPats.UpdateCur();
				}
			}
			//CovPats.Refresh();
			FillTable();
		}

		private void radioDedYes_Click(object sender, System.EventArgs e) {
			InsPlans.Cur.DeductWaivPrev=YN.Yes;
		}

		private void radioDedNo_Click(object sender, System.EventArgs e) {
			InsPlans.Cur.DeductWaivPrev=YN.No;
		}

		private void radioDedUnkn_Click(object sender, System.EventArgs e) {
			InsPlans.Cur.DeductWaivPrev=YN.Unknown;
		}

		private void radioMissYes_Click(object sender, System.EventArgs e) {
			InsPlans.Cur.MissToothExcl=YN.Yes;
		}

		private void radioMissNo_Click(object sender, System.EventArgs e) {
			InsPlans.Cur.MissToothExcl=YN.No;
		}

		private void radioMissUnkn_Click(object sender, System.EventArgs e) {
			InsPlans.Cur.MissToothExcl=YN.Unknown;
		}

		private void radioWaitYes_Click(object sender, System.EventArgs e) {
			InsPlans.Cur.MajorWait=YN.Yes;
		}

		private void radioWaitNo_Click(object sender, System.EventArgs e) {
			InsPlans.Cur.MajorWait=YN.No;
		}

		private void radioWaitUnkn_Click(object sender, System.EventArgs e) {
			InsPlans.Cur.MajorWait=YN.Unknown;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDateEffect.errorProvider1.GetError(textDateEffect)!=""
				|| textDateTerm.errorProvider1.GetError(textDateTerm)!=""
				|| textAnnualMax.errorProvider1.GetError(textAnnualMax)!=""
				|| textRenewMonth.errorProvider1.GetError(textRenewMonth)!=""
				|| textDeductible.errorProvider1.GetError(textDeductible)!=""
				|| textOrthoMax.errorProvider1.GetError(textOrthoMax)!=""
				|| textFloToAge.errorProvider1.GetError(textFloToAge)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			InsPlans.Cur.Subscriber=Patients.FamilyList[listSubscriber.SelectedIndex].PatNum;
			InsPlans.Cur.Carrier      =textCarrier.Text;
			InsPlans.Cur.DateEffective=PIn.PDate(textDateEffect.Text);
			InsPlans.Cur.DateTerm     =PIn.PDate(textDateTerm.Text);
			InsPlans.Cur.Phone        =textPhone.Text;
			InsPlans.Cur.GroupName=textGroupName.Text;
			InsPlans.Cur.GroupNum=textGroupNum.Text;
			InsPlans.Cur.Address=textAddress.Text;
			InsPlans.Cur.Address2=textAddress2.Text;
			InsPlans.Cur.City=textCity.Text;
			InsPlans.Cur.State=textState.Text;
			InsPlans.Cur.Zip=textZip.Text;
			if(listClaimFormat.SelectedIndex!=-1)
				InsPlans.Cur.ClaimFormat=Defs.Short[(int)DefCat.ClaimFormats][listClaimFormat.SelectedIndex].DefNum;
			InsPlans.Cur.ElectID=textElectID.Text;
			InsPlans.Cur.Employer=textEmployer.Text;
			if(listFeeSched.SelectedIndex==-1)
				InsPlans.Cur.FeeSched=0;
			else
				InsPlans.Cur.FeeSched=Defs.Short[(int)DefCat.FeeSchedNames][listFeeSched.SelectedIndex].DefNum;
			if(textAnnualMax.Text=="")
				InsPlans.Cur.AnnualMax=-1;
			else
				InsPlans.Cur.AnnualMax=PIn.PInt(textAnnualMax.Text);
			if(textRenewMonth.Text=="")
				InsPlans.Cur.RenewMonth=-1;
			else
				InsPlans.Cur.RenewMonth=PIn.PInt(textRenewMonth.Text);
			if(textDeductible.Text=="")
				InsPlans.Cur.Deductible=-1;
			else
				InsPlans.Cur.Deductible=PIn.PInt(textDeductible.Text);
			//skip DedWaivPrev
			if(textOrthoMax.Text=="")
				InsPlans.Cur.OrthoMax=-1;
			else
				InsPlans.Cur.OrthoMax=PIn.PInt(textOrthoMax.Text);
			if(textFloToAge.Text=="")
				InsPlans.Cur.FloToAge=-1;
			else
				InsPlans.Cur.FloToAge=PIn.PInt(textFloToAge.Text);
			InsPlans.Cur.PlanNote=textPlanNote.Text;
			//missToothExcl
			//majorwait
			InsPlans.Cur.ReleaseInfo=checkRelease.Checked;
			InsPlans.Cur.AssignBen=checkAssign.Checked;
			if(IsNew){
				InsPlans.UpdateCur();
			}
			else{
				InsPlans.UpdateCur();
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			if(IsNew){
				CovPats.DeleteAllInCurPlan();
				InsPlans.DeleteCur();
			}
			else{
				;
			}
			DialogResult=DialogResult.Cancel;
		}

		private void textPhone_TextChanged(object sender, System.EventArgs e) {
			textPhone.Text=TelephoneNumbers.AutoFormat(textPhone.Text);
			textPhone.SelectionStart=textPhone.Text.Length;		
		}

	}
}
