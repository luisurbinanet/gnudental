/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormPatientEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.ComponentModel.Container components = null;// Required designer variable.
		private System.Windows.Forms.TextBox textLName;
		private System.Windows.Forms.TextBox textFName;
		private System.Windows.Forms.TextBox textMiddleI;
		private System.Windows.Forms.TextBox textPreferred;
		private System.Windows.Forms.TextBox textSSN;
		private System.Windows.Forms.TextBox textAddress;
		private System.Windows.Forms.TextBox textAddress2;
		private System.Windows.Forms.TextBox textCity;
		private System.Windows.Forms.TextBox textState;
		private System.Windows.Forms.TextBox textHmPhone;
		private System.Windows.Forms.TextBox textWkPhone;
		private System.Windows.Forms.TextBox textWirelessPhone;
		private System.Windows.Forms.TextBox textAddrNotes;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.TextBox textAge;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.TextBox textSalutation;
		private System.Windows.Forms.TextBox textEmail;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Button butSecClear;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.ListBox listSecProv;
		private System.Windows.Forms.ListBox listPriProv;
		private System.Windows.Forms.ListBox listFeeSched;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Button butClearFee;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.TextBox textCreditType;
		private System.Windows.Forms.ListBox listBillType;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label28;
		private OpenDental.ValidNum textRecall;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox textSchool;
		private System.Windows.Forms.RadioButton radioStudentN;
		private System.Windows.Forms.RadioButton radioStudentP;
		private System.Windows.Forms.RadioButton radioStudentF;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.TextBox textChartNumber;
		//private OpenDental.ValidDate textBirthdate2;
		private OpenDental.ValidDate textBirthdate;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkSame;
		private System.Windows.Forms.TextBox textBox1;
		private OpenDental.TableRefList tbRefList;
		private System.Windows.Forms.ComboBox comboZip;
		private System.Windows.Forms.TextBox textZip;
		private System.Windows.Forms.GroupBox groupNotes;
		private System.Windows.Forms.CheckBox checkNotesSame;
		private OpenDental.XPButton butAdd;
		private OpenDental.XPButton butDelete;
		private System.Windows.Forms.TextBox textPatNum;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.Button butAuto;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.TextBox textMedicaidID;
		private System.Windows.Forms.ListBox listStatus;
		private System.Windows.Forms.ListBox listGender;
		private System.Windows.Forms.ListBox listPosition;
		private System.Windows.Forms.TextBox textEmployer;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.Button butChangeEmp;
		private System.Windows.Forms.TextBox textEmploymentNote;
		private System.Windows.Forms.Label labelSSN;
		private System.Windows.Forms.Label labelZip;
		private System.Windows.Forms.Label labelST;
		private System.Windows.Forms.Button butEditZip;
		private System.Windows.Forms.Label labelCity;
		///<summary></summary>
		public bool IsNew;

		///<summary></summary>
		public FormPatientEdit(){
			InitializeComponent();// Required for Windows Form Designer support
			tbRefList.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbRefList_CellDoubleClicked);
			Lan.C(this, new System.Windows.Forms.Control[] 
			{
				label2,
				label3,
				label4,
				label5,
				label6,
				label7,
				label8,
				label9,
				labelSSN,
				label11,
				label12,
				labelST,
				labelCity,
				labelZip,
				label16,
				label17,
				label18,
				label20,
				label21,
				label22,
				label23,
				butSecClear,
				label24,
				label25,
				butClearFee,
				label26,
				label27,
				label1,
				label28,
				groupBox2,
				radioStudentN,
				radioStudentP,
				radioStudentF,
				label30,
				label29,
				groupBox1,
				checkSame,
				label31,
				label33,
				label34,
				labelST
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
				butDelete,
			});
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="CA"){//en-CA or fr-CA
				labelSSN.Text="SIN";
				labelZip.Text="Postal Code";
				labelST.Text="Province";
				butEditZip.Text="Edit Postal Code";
			}
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="GB"){//en-GB
				//labelSSN.Text="?";
				labelZip.Text="Postcode";
				labelST.Text="";//no such thing as state in GB
				butEditZip.Text="Edit Postcode";
			}
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

		private void InitializeComponent(){
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormPatientEdit));
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.labelSSN = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.labelCity = new System.Windows.Forms.Label();
			this.labelST = new System.Windows.Forms.Label();
			this.labelZip = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.textLName = new System.Windows.Forms.TextBox();
			this.textFName = new System.Windows.Forms.TextBox();
			this.textMiddleI = new System.Windows.Forms.TextBox();
			this.textPreferred = new System.Windows.Forms.TextBox();
			this.textSSN = new System.Windows.Forms.TextBox();
			this.textAddress = new System.Windows.Forms.TextBox();
			this.textAddress2 = new System.Windows.Forms.TextBox();
			this.textCity = new System.Windows.Forms.TextBox();
			this.textState = new System.Windows.Forms.TextBox();
			this.textHmPhone = new System.Windows.Forms.TextBox();
			this.textWkPhone = new System.Windows.Forms.TextBox();
			this.textWirelessPhone = new System.Windows.Forms.TextBox();
			this.textAddrNotes = new System.Windows.Forms.TextBox();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.label20 = new System.Windows.Forms.Label();
			this.textAge = new System.Windows.Forms.TextBox();
			this.textSalutation = new System.Windows.Forms.TextBox();
			this.label21 = new System.Windows.Forms.Label();
			this.textEmail = new System.Windows.Forms.TextBox();
			this.label22 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.butSecClear = new System.Windows.Forms.Button();
			this.label24 = new System.Windows.Forms.Label();
			this.listSecProv = new System.Windows.Forms.ListBox();
			this.listPriProv = new System.Windows.Forms.ListBox();
			this.listFeeSched = new System.Windows.Forms.ListBox();
			this.label25 = new System.Windows.Forms.Label();
			this.butClearFee = new System.Windows.Forms.Button();
			this.label26 = new System.Windows.Forms.Label();
			this.label27 = new System.Windows.Forms.Label();
			this.textCreditType = new System.Windows.Forms.TextBox();
			this.listBillType = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.textRecall = new OpenDental.ValidNum();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textSchool = new System.Windows.Forms.TextBox();
			this.radioStudentN = new System.Windows.Forms.RadioButton();
			this.radioStudentP = new System.Windows.Forms.RadioButton();
			this.radioStudentF = new System.Windows.Forms.RadioButton();
			this.label30 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.textChartNumber = new System.Windows.Forms.TextBox();
			this.textBirthdate = new OpenDental.ValidDate();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butEditZip = new System.Windows.Forms.Button();
			this.textZip = new System.Windows.Forms.TextBox();
			this.comboZip = new System.Windows.Forms.ComboBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.checkSame = new System.Windows.Forms.CheckBox();
			this.tbRefList = new OpenDental.TableRefList();
			this.groupNotes = new System.Windows.Forms.GroupBox();
			this.checkNotesSame = new System.Windows.Forms.CheckBox();
			this.butAdd = new OpenDental.XPButton();
			this.butDelete = new OpenDental.XPButton();
			this.textPatNum = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.label32 = new System.Windows.Forms.Label();
			this.butAuto = new System.Windows.Forms.Button();
			this.textMedicaidID = new System.Windows.Forms.TextBox();
			this.label31 = new System.Windows.Forms.Label();
			this.listStatus = new System.Windows.Forms.ListBox();
			this.listGender = new System.Windows.Forms.ListBox();
			this.listPosition = new System.Windows.Forms.ListBox();
			this.textEmployer = new System.Windows.Forms.TextBox();
			this.textEmploymentNote = new System.Windows.Forms.TextBox();
			this.label33 = new System.Windows.Forms.Label();
			this.label34 = new System.Windows.Forms.Label();
			this.butChangeEmp = new System.Windows.Forms.Button();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupNotes.SuspendLayout();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(10, 31);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(125, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "Last Name";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(6, 53);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(127, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "First Name";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(9, 74);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(124, 16);
			this.label4.TabIndex = 3;
			this.label4.Text = "Middle Initial";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(5, 95);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(129, 16);
			this.label5.TabIndex = 4;
			this.label5.Text = "Preferred Name";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(62, 139);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(114, 17);
			this.label6.TabIndex = 5;
			this.label6.Text = "Status";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(180, 140);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(106, 16);
			this.label7.TabIndex = 6;
			this.label7.Text = "Gender";
			this.label7.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(298, 141);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100, 16);
			this.label8.TabIndex = 7;
			this.label8.Text = "Position";
			this.label8.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(30, 236);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(105, 14);
			this.label9.TabIndex = 8;
			this.label9.Text = "Birthdate";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelSSN
			// 
			this.labelSSN.Location = new System.Drawing.Point(42, 258);
			this.labelSSN.Name = "labelSSN";
			this.labelSSN.Size = new System.Drawing.Size(92, 14);
			this.labelSSN.TabIndex = 9;
			this.labelSSN.Text = "SS#";
			this.labelSSN.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(5, 74);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(98, 14);
			this.label11.TabIndex = 10;
			this.label11.Text = "Address";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(5, 95);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(99, 14);
			this.label12.TabIndex = 11;
			this.label12.Text = "Address2";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelCity
			// 
			this.labelCity.Location = new System.Drawing.Point(4, 115);
			this.labelCity.Name = "labelCity";
			this.labelCity.Size = new System.Drawing.Size(98, 14);
			this.labelCity.TabIndex = 12;
			this.labelCity.Text = "City";
			this.labelCity.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelST
			// 
			this.labelST.Location = new System.Drawing.Point(7, 135);
			this.labelST.Name = "labelST";
			this.labelST.Size = new System.Drawing.Size(96, 14);
			this.labelST.TabIndex = 13;
			this.labelST.Text = "ST";
			this.labelST.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelZip
			// 
			this.labelZip.Location = new System.Drawing.Point(7, 156);
			this.labelZip.Name = "labelZip";
			this.labelZip.Size = new System.Drawing.Size(96, 14);
			this.labelZip.TabIndex = 14;
			this.labelZip.Text = "Zip";
			this.labelZip.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(7, 53);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(98, 14);
			this.label16.TabIndex = 15;
			this.label16.Text = "Home Phone";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(18, 383);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(118, 14);
			this.label17.TabIndex = 16;
			this.label17.Text = "Work Phone";
			this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(12, 362);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(124, 14);
			this.label18.TabIndex = 17;
			this.label18.Text = "Wireless Phone";
			this.label18.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textLName
			// 
			this.textLName.Location = new System.Drawing.Point(133, 29);
			this.textLName.MaxLength = 100;
			this.textLName.Name = "textLName";
			this.textLName.Size = new System.Drawing.Size(228, 20);
			this.textLName.TabIndex = 0;
			this.textLName.Text = "";
			this.textLName.TextChanged += new System.EventHandler(this.textLName_TextChanged);
			// 
			// textFName
			// 
			this.textFName.Location = new System.Drawing.Point(133, 50);
			this.textFName.MaxLength = 100;
			this.textFName.Name = "textFName";
			this.textFName.Size = new System.Drawing.Size(228, 20);
			this.textFName.TabIndex = 1;
			this.textFName.Text = "";
			this.textFName.TextChanged += new System.EventHandler(this.textFName_TextChanged);
			// 
			// textMiddleI
			// 
			this.textMiddleI.Location = new System.Drawing.Point(133, 71);
			this.textMiddleI.MaxLength = 100;
			this.textMiddleI.Name = "textMiddleI";
			this.textMiddleI.Size = new System.Drawing.Size(75, 20);
			this.textMiddleI.TabIndex = 2;
			this.textMiddleI.Text = "";
			this.textMiddleI.TextChanged += new System.EventHandler(this.textMiddleI_TextChanged);
			// 
			// textPreferred
			// 
			this.textPreferred.Location = new System.Drawing.Point(133, 92);
			this.textPreferred.MaxLength = 100;
			this.textPreferred.Name = "textPreferred";
			this.textPreferred.Size = new System.Drawing.Size(228, 20);
			this.textPreferred.TabIndex = 3;
			this.textPreferred.Text = "";
			this.textPreferred.TextChanged += new System.EventHandler(this.textPreferred_TextChanged);
			// 
			// textSSN
			// 
			this.textSSN.Location = new System.Drawing.Point(135, 253);
			this.textSSN.MaxLength = 11;
			this.textSSN.Name = "textSSN";
			this.textSSN.Size = new System.Drawing.Size(82, 20);
			this.textSSN.TabIndex = 9;
			this.textSSN.Text = "";
			this.textSSN.Validating += new System.ComponentModel.CancelEventHandler(this.textSSN_Validating);
			// 
			// textAddress
			// 
			this.textAddress.Location = new System.Drawing.Point(105, 71);
			this.textAddress.MaxLength = 100;
			this.textAddress.Name = "textAddress";
			this.textAddress.Size = new System.Drawing.Size(254, 20);
			this.textAddress.TabIndex = 1;
			this.textAddress.Text = "";
			this.textAddress.TextChanged += new System.EventHandler(this.textAddress_TextChanged);
			// 
			// textAddress2
			// 
			this.textAddress2.Location = new System.Drawing.Point(105, 91);
			this.textAddress2.MaxLength = 100;
			this.textAddress2.Name = "textAddress2";
			this.textAddress2.Size = new System.Drawing.Size(253, 20);
			this.textAddress2.TabIndex = 2;
			this.textAddress2.Text = "";
			this.textAddress2.TextChanged += new System.EventHandler(this.textAddress2_TextChanged);
			// 
			// textCity
			// 
			this.textCity.Location = new System.Drawing.Point(105, 111);
			this.textCity.MaxLength = 100;
			this.textCity.Name = "textCity";
			this.textCity.Size = new System.Drawing.Size(191, 20);
			this.textCity.TabIndex = 0;
			this.textCity.TabStop = false;
			this.textCity.Text = "";
			this.textCity.TextChanged += new System.EventHandler(this.textCity_TextChanged);
			// 
			// textState
			// 
			this.textState.Location = new System.Drawing.Point(105, 131);
			this.textState.MaxLength = 100;
			this.textState.Name = "textState";
			this.textState.Size = new System.Drawing.Size(61, 20);
			this.textState.TabIndex = 13;
			this.textState.TabStop = false;
			this.textState.Text = "";
			this.textState.TextChanged += new System.EventHandler(this.textState_TextChanged);
			// 
			// textHmPhone
			// 
			this.textHmPhone.Location = new System.Drawing.Point(105, 51);
			this.textHmPhone.MaxLength = 30;
			this.textHmPhone.Name = "textHmPhone";
			this.textHmPhone.Size = new System.Drawing.Size(174, 20);
			this.textHmPhone.TabIndex = 0;
			this.textHmPhone.Text = "";
			this.textHmPhone.TextChanged += new System.EventHandler(this.textHmPhone_TextChanged);
			// 
			// textWkPhone
			// 
			this.textWkPhone.Location = new System.Drawing.Point(135, 379);
			this.textWkPhone.MaxLength = 30;
			this.textWkPhone.Name = "textWkPhone";
			this.textWkPhone.Size = new System.Drawing.Size(174, 20);
			this.textWkPhone.TabIndex = 15;
			this.textWkPhone.Text = "";
			this.textWkPhone.TextChanged += new System.EventHandler(this.textWkPhone_TextChanged);
			// 
			// textWirelessPhone
			// 
			this.textWirelessPhone.Location = new System.Drawing.Point(135, 358);
			this.textWirelessPhone.MaxLength = 30;
			this.textWirelessPhone.Name = "textWirelessPhone";
			this.textWirelessPhone.Size = new System.Drawing.Size(174, 20);
			this.textWirelessPhone.TabIndex = 14;
			this.textWirelessPhone.Text = "";
			this.textWirelessPhone.TextChanged += new System.EventHandler(this.textWirelessPhone_TextChanged);
			// 
			// textAddrNotes
			// 
			this.textAddrNotes.AcceptsReturn = true;
			this.textAddrNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textAddrNotes.ForeColor = System.Drawing.Color.Red;
			this.textAddrNotes.Location = new System.Drawing.Point(15, 42);
			this.textAddrNotes.Multiline = true;
			this.textAddrNotes.Name = "textAddrNotes";
			this.textAddrNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textAddrNotes.Size = new System.Drawing.Size(238, 58);
			this.textAddrNotes.TabIndex = 0;
			this.textAddrNotes.Text = "";
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(874, 597);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 24;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(874, 637);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 25;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(215, 236);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(38, 16);
			this.label20.TabIndex = 40;
			this.label20.Text = "Age";
			this.label20.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textAge
			// 
			this.textAge.Location = new System.Drawing.Point(255, 232);
			this.textAge.Name = "textAge";
			this.textAge.ReadOnly = true;
			this.textAge.Size = new System.Drawing.Size(40, 20);
			this.textAge.TabIndex = 0;
			this.textAge.Text = "";
			// 
			// textSalutation
			// 
			this.textSalutation.Location = new System.Drawing.Point(133, 113);
			this.textSalutation.MaxLength = 100;
			this.textSalutation.Name = "textSalutation";
			this.textSalutation.Size = new System.Drawing.Size(228, 20);
			this.textSalutation.TabIndex = 4;
			this.textSalutation.Text = "";
			this.textSalutation.TextChanged += new System.EventHandler(this.textSalutation_TextChanged);
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(9, 117);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(127, 16);
			this.label21.TabIndex = 42;
			this.label21.Text = "Salutation (Dear __)";
			this.label21.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textEmail
			// 
			this.textEmail.Location = new System.Drawing.Point(135, 295);
			this.textEmail.MaxLength = 100;
			this.textEmail.Name = "textEmail";
			this.textEmail.Size = new System.Drawing.Size(226, 20);
			this.textEmail.TabIndex = 11;
			this.textEmail.Text = "";
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(31, 300);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(104, 14);
			this.label22.TabIndex = 44;
			this.label22.Text = "E-mail";
			this.label22.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(732, 6);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(110, 28);
			this.label23.TabIndex = 50;
			this.label23.Text = "Secondary Provider";
			this.label23.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butSecClear
			// 
			this.butSecClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butSecClear.Location = new System.Drawing.Point(734, 225);
			this.butSecClear.Name = "butSecClear";
			this.butSecClear.TabIndex = 50;
			this.butSecClear.Text = "Clear";
			this.butSecClear.Click += new System.EventHandler(this.butSecClear_Click);
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(624, 3);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(98, 31);
			this.label24.TabIndex = 48;
			this.label24.Text = "Primary Provider";
			this.label24.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listSecProv
			// 
			this.listSecProv.Location = new System.Drawing.Point(734, 35);
			this.listSecProv.Name = "listSecProv";
			this.listSecProv.Size = new System.Drawing.Size(82, 186);
			this.listSecProv.TabIndex = 21;
			// 
			// listPriProv
			// 
			this.listPriProv.Location = new System.Drawing.Point(624, 35);
			this.listPriProv.Name = "listPriProv";
			this.listPriProv.Size = new System.Drawing.Size(82, 186);
			this.listPriProv.TabIndex = 20;
			// 
			// listFeeSched
			// 
			this.listFeeSched.Location = new System.Drawing.Point(844, 36);
			this.listFeeSched.Name = "listFeeSched";
			this.listFeeSched.Size = new System.Drawing.Size(108, 186);
			this.listFeeSched.TabIndex = 22;
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(842, 4);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(121, 30);
			this.label25.TabIndex = 51;
			this.label25.Text = "Fee Schedule(optional)";
			this.label25.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butClearFee
			// 
			this.butClearFee.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClearFee.Location = new System.Drawing.Point(844, 226);
			this.butClearFee.Name = "butClearFee";
			this.butClearFee.TabIndex = 53;
			this.butClearFee.Text = "Clear";
			this.butClearFee.Click += new System.EventHandler(this.butClearFee_Click);
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(843, 257);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(123, 113);
			this.label26.TabIndex = 54;
			this.label26.Text = "If no fee schedule is selected, the Primary Provider\'s fee schedule will be used." +
				"  If the patient has insurance, that fee schedule will be used instead of either" +
				" of these.";
			// 
			// label27
			// 
			this.label27.Location = new System.Drawing.Point(6, 175);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(97, 16);
			this.label27.TabIndex = 55;
			this.label27.Text = "Credit Type";
			this.label27.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCreditType
			// 
			this.textCreditType.Location = new System.Drawing.Point(105, 172);
			this.textCreditType.MaxLength = 1;
			this.textCreditType.Name = "textCreditType";
			this.textCreditType.Size = new System.Drawing.Size(18, 20);
			this.textCreditType.TabIndex = 4;
			this.textCreditType.Text = "";
			this.textCreditType.TextChanged += new System.EventHandler(this.textCreditType_TextChanged);
			// 
			// listBillType
			// 
			this.listBillType.Location = new System.Drawing.Point(440, 35);
			this.listBillType.Name = "listBillType";
			this.listBillType.Size = new System.Drawing.Size(158, 186);
			this.listBillType.TabIndex = 19;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(439, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(158, 26);
			this.label1.TabIndex = 57;
			this.label1.Text = "Billing Type:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label28
			// 
			this.label28.Location = new System.Drawing.Point(17, 341);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(119, 16);
			this.label28.TabIndex = 58;
			this.label28.Text = "Recall Months";
			this.label28.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textRecall
			// 
			this.textRecall.Location = new System.Drawing.Point(135, 337);
			this.textRecall.MaxLength = 2;
			this.textRecall.MinVal = 0;
			this.textRecall.Name = "textRecall";
			this.textRecall.Size = new System.Drawing.Size(30, 20);
			this.textRecall.TabIndex = 13;
			this.textRecall.Text = "";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textSchool);
			this.groupBox2.Controls.Add(this.radioStudentN);
			this.groupBox2.Controls.Add(this.radioStudentP);
			this.groupBox2.Controls.Add(this.radioStudentF);
			this.groupBox2.Controls.Add(this.label30);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(440, 259);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(340, 65);
			this.groupBox2.TabIndex = 23;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Student Status if Dependent Over 19 (for Ins)";
			// 
			// textSchool
			// 
			this.textSchool.Location = new System.Drawing.Point(80, 36);
			this.textSchool.MaxLength = 30;
			this.textSchool.Name = "textSchool";
			this.textSchool.Size = new System.Drawing.Size(250, 20);
			this.textSchool.TabIndex = 3;
			this.textSchool.Text = "";
			// 
			// radioStudentN
			// 
			this.radioStudentN.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioStudentN.Location = new System.Drawing.Point(8, 18);
			this.radioStudentN.Name = "radioStudentN";
			this.radioStudentN.Size = new System.Drawing.Size(108, 16);
			this.radioStudentN.TabIndex = 0;
			this.radioStudentN.Text = "Nonstudent";
			this.radioStudentN.Click += new System.EventHandler(this.radioStudentN_Click);
			// 
			// radioStudentP
			// 
			this.radioStudentP.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioStudentP.Location = new System.Drawing.Point(218, 18);
			this.radioStudentP.Name = "radioStudentP";
			this.radioStudentP.Size = new System.Drawing.Size(104, 16);
			this.radioStudentP.TabIndex = 2;
			this.radioStudentP.Text = "Parttime";
			this.radioStudentP.Click += new System.EventHandler(this.radioStudentP_Click);
			// 
			// radioStudentF
			// 
			this.radioStudentF.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioStudentF.Location = new System.Drawing.Point(120, 18);
			this.radioStudentF.Name = "radioStudentF";
			this.radioStudentF.Size = new System.Drawing.Size(98, 16);
			this.radioStudentF.TabIndex = 1;
			this.radioStudentF.Text = "Fulltime";
			this.radioStudentF.Click += new System.EventHandler(this.radioStudentF_Click);
			// 
			// label30
			// 
			this.label30.Location = new System.Drawing.Point(4, 40);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(76, 16);
			this.label30.TabIndex = 9;
			this.label30.Text = "School Name";
			this.label30.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label29
			// 
			this.label29.Location = new System.Drawing.Point(20, 320);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(116, 16);
			this.label29.TabIndex = 62;
			this.label29.Text = "ChartNumber";
			this.label29.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textChartNumber
			// 
			this.textChartNumber.Location = new System.Drawing.Point(135, 316);
			this.textChartNumber.MaxLength = 20;
			this.textChartNumber.Name = "textChartNumber";
			this.textChartNumber.Size = new System.Drawing.Size(54, 20);
			this.textChartNumber.TabIndex = 12;
			this.textChartNumber.Text = "";
			// 
			// textBirthdate
			// 
			this.textBirthdate.Location = new System.Drawing.Point(135, 232);
			this.textBirthdate.Name = "textBirthdate";
			this.textBirthdate.Size = new System.Drawing.Size(82, 20);
			this.textBirthdate.TabIndex = 8;
			this.textBirthdate.Text = "";
			this.textBirthdate.Validated += new System.EventHandler(this.textBirthdate_Validated);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textHmPhone);
			this.groupBox1.Controls.Add(this.butEditZip);
			this.groupBox1.Controls.Add(this.textZip);
			this.groupBox1.Controls.Add(this.comboZip);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.checkSame);
			this.groupBox1.Controls.Add(this.textState);
			this.groupBox1.Controls.Add(this.labelST);
			this.groupBox1.Controls.Add(this.textAddress);
			this.groupBox1.Controls.Add(this.label27);
			this.groupBox1.Controls.Add(this.textCreditType);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.labelCity);
			this.groupBox1.Controls.Add(this.textAddress2);
			this.groupBox1.Controls.Add(this.labelZip);
			this.groupBox1.Controls.Add(this.label16);
			this.groupBox1.Controls.Add(this.textCity);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(30, 449);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(368, 197);
			this.groupBox1.TabIndex = 18;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Address and Phone";
			// 
			// butEditZip
			// 
			this.butEditZip.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butEditZip.Location = new System.Drawing.Point(189, 172);
			this.butEditZip.Name = "butEditZip";
			this.butEditZip.Size = new System.Drawing.Size(113, 23);
			this.butEditZip.TabIndex = 62;
			this.butEditZip.TabStop = false;
			this.butEditZip.Text = "&Edit Zip";
			this.butEditZip.Click += new System.EventHandler(this.butEditZip_Click);
			// 
			// textZip
			// 
			this.textZip.Location = new System.Drawing.Point(105, 151);
			this.textZip.MaxLength = 100;
			this.textZip.Name = "textZip";
			this.textZip.Size = new System.Drawing.Size(179, 20);
			this.textZip.TabIndex = 3;
			this.textZip.Text = "";
			this.textZip.Validating += new System.ComponentModel.CancelEventHandler(this.textZip_Validating);
			this.textZip.TextChanged += new System.EventHandler(this.textZip_TextChanged);
			// 
			// comboZip
			// 
			this.comboZip.DropDownWidth = 198;
			this.comboZip.Location = new System.Drawing.Point(105, 151);
			this.comboZip.Name = "comboZip";
			this.comboZip.Size = new System.Drawing.Size(198, 21);
			this.comboZip.TabIndex = 60;
			this.comboZip.TabStop = false;
			this.comboZip.SelectionChangeCommitted += new System.EventHandler(this.comboZip_SelectionChangeCommitted);
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(123, 19);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(230, 33);
			this.textBox1.TabIndex = 57;
			this.textBox1.TabStop = false;
			this.textBox1.Text = "Same for entire family.  (Including Billing Type, Providers, and Fee Schedule)";
			// 
			// checkSame
			// 
			this.checkSame.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkSame.Location = new System.Drawing.Point(105, 16);
			this.checkSame.Name = "checkSame";
			this.checkSame.Size = new System.Drawing.Size(18, 21);
			this.checkSame.TabIndex = 56;
			this.checkSame.TabStop = false;
			// 
			// tbRefList
			// 
			this.tbRefList.BackColor = System.Drawing.SystemColors.Window;
			this.tbRefList.Location = new System.Drawing.Point(440, 388);
			this.tbRefList.Name = "tbRefList";
			this.tbRefList.ScrollValue = 1;
			this.tbRefList.SelectedIndices = new int[0];
			this.tbRefList.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbRefList.Size = new System.Drawing.Size(459, 96);
			this.tbRefList.TabIndex = 64;
			// 
			// groupNotes
			// 
			this.groupNotes.Controls.Add(this.checkNotesSame);
			this.groupNotes.Controls.Add(this.textAddrNotes);
			this.groupNotes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupNotes.Location = new System.Drawing.Point(443, 538);
			this.groupNotes.Name = "groupNotes";
			this.groupNotes.Size = new System.Drawing.Size(265, 107);
			this.groupNotes.TabIndex = 17;
			this.groupNotes.TabStop = false;
			this.groupNotes.Text = "Address and Phone Notes";
			// 
			// checkNotesSame
			// 
			this.checkNotesSame.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNotesSame.Location = new System.Drawing.Point(15, 20);
			this.checkNotesSame.Name = "checkNotesSame";
			this.checkNotesSame.Size = new System.Drawing.Size(247, 18);
			this.checkNotesSame.TabIndex = 19;
			this.checkNotesSame.Text = "Same for entire family";
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(441, 360);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75, 26);
			this.butAdd.TabIndex = 69;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(523, 360);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(83, 26);
			this.butDelete.TabIndex = 70;
			this.butDelete.Text = "&Remove";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// textPatNum
			// 
			this.textPatNum.Location = new System.Drawing.Point(133, 8);
			this.textPatNum.MaxLength = 100;
			this.textPatNum.Name = "textPatNum";
			this.textPatNum.ReadOnly = true;
			this.textPatNum.Size = new System.Drawing.Size(75, 20);
			this.textPatNum.TabIndex = 71;
			this.textPatNum.Text = "";
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(11, 11);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(125, 16);
			this.label19.TabIndex = 72;
			this.label19.Text = "Patient Number";
			this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label32
			// 
			this.label32.Location = new System.Drawing.Point(256, 320);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(88, 17);
			this.label32.TabIndex = 73;
			this.label32.Text = "(if used)";
			// 
			// butAuto
			// 
			this.butAuto.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butAuto.Location = new System.Drawing.Point(190, 317);
			this.butAuto.Name = "butAuto";
			this.butAuto.Size = new System.Drawing.Size(62, 20);
			this.butAuto.TabIndex = 74;
			this.butAuto.Text = "Auto";
			this.butAuto.Click += new System.EventHandler(this.butAuto_Click);
			// 
			// textMedicaidID
			// 
			this.textMedicaidID.Location = new System.Drawing.Point(135, 274);
			this.textMedicaidID.MaxLength = 11;
			this.textMedicaidID.Name = "textMedicaidID";
			this.textMedicaidID.Size = new System.Drawing.Size(99, 20);
			this.textMedicaidID.TabIndex = 10;
			this.textMedicaidID.Text = "";
			// 
			// label31
			// 
			this.label31.Location = new System.Drawing.Point(28, 279);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(106, 14);
			this.label31.TabIndex = 75;
			this.label31.Text = "Medicaid ID";
			this.label31.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listStatus
			// 
			this.listStatus.Location = new System.Drawing.Point(62, 156);
			this.listStatus.Name = "listStatus";
			this.listStatus.Size = new System.Drawing.Size(105, 69);
			this.listStatus.TabIndex = 5;
			// 
			// listGender
			// 
			this.listGender.Location = new System.Drawing.Point(180, 156);
			this.listGender.Name = "listGender";
			this.listGender.Size = new System.Drawing.Size(105, 43);
			this.listGender.TabIndex = 6;
			// 
			// listPosition
			// 
			this.listPosition.Location = new System.Drawing.Point(298, 157);
			this.listPosition.Name = "listPosition";
			this.listPosition.Size = new System.Drawing.Size(105, 56);
			this.listPosition.TabIndex = 7;
			// 
			// textEmployer
			// 
			this.textEmployer.Location = new System.Drawing.Point(135, 400);
			this.textEmployer.MaxLength = 30;
			this.textEmployer.Name = "textEmployer";
			this.textEmployer.ReadOnly = true;
			this.textEmployer.Size = new System.Drawing.Size(212, 20);
			this.textEmployer.TabIndex = 16;
			this.textEmployer.Text = "";
			// 
			// textEmploymentNote
			// 
			this.textEmploymentNote.Location = new System.Drawing.Point(135, 421);
			this.textEmploymentNote.MaxLength = 30;
			this.textEmploymentNote.Name = "textEmploymentNote";
			this.textEmploymentNote.Size = new System.Drawing.Size(278, 20);
			this.textEmploymentNote.TabIndex = 17;
			this.textEmploymentNote.Text = "";
			// 
			// label33
			// 
			this.label33.Location = new System.Drawing.Point(12, 404);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(124, 14);
			this.label33.TabIndex = 83;
			this.label33.Text = "Employer";
			this.label33.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label34
			// 
			this.label34.Location = new System.Drawing.Point(8, 425);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(128, 14);
			this.label34.TabIndex = 82;
			this.label34.Text = "Employment Note";
			this.label34.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butChangeEmp
			// 
			this.butChangeEmp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butChangeEmp.Location = new System.Drawing.Point(349, 401);
			this.butChangeEmp.Name = "butChangeEmp";
			this.butChangeEmp.Size = new System.Drawing.Size(65, 20);
			this.butChangeEmp.TabIndex = 84;
			this.butChangeEmp.Text = "Change";
			this.butChangeEmp.Click += new System.EventHandler(this.butChangeEmp_Click);
			// 
			// FormPatientEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(968, 676);
			this.Controls.Add(this.butChangeEmp);
			this.Controls.Add(this.textEmployer);
			this.Controls.Add(this.textEmploymentNote);
			this.Controls.Add(this.textMedicaidID);
			this.Controls.Add(this.textPatNum);
			this.Controls.Add(this.textBirthdate);
			this.Controls.Add(this.textChartNumber);
			this.Controls.Add(this.textRecall);
			this.Controls.Add(this.textEmail);
			this.Controls.Add(this.textSalutation);
			this.Controls.Add(this.textAge);
			this.Controls.Add(this.textWirelessPhone);
			this.Controls.Add(this.textWkPhone);
			this.Controls.Add(this.textSSN);
			this.Controls.Add(this.textPreferred);
			this.Controls.Add(this.textMiddleI);
			this.Controls.Add(this.textFName);
			this.Controls.Add(this.textLName);
			this.Controls.Add(this.label33);
			this.Controls.Add(this.label34);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.listPosition);
			this.Controls.Add(this.listGender);
			this.Controls.Add(this.listStatus);
			this.Controls.Add(this.label31);
			this.Controls.Add(this.butAuto);
			this.Controls.Add(this.label32);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.groupNotes);
			this.Controls.Add(this.tbRefList);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label29);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.label28);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listBillType);
			this.Controls.Add(this.label26);
			this.Controls.Add(this.butClearFee);
			this.Controls.Add(this.listFeeSched);
			this.Controls.Add(this.label25);
			this.Controls.Add(this.label23);
			this.Controls.Add(this.butSecClear);
			this.Controls.Add(this.label24);
			this.Controls.Add(this.listSecProv);
			this.Controls.Add(this.listPriProv);
			this.Controls.Add(this.label22);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label21);
			this.Controls.Add(this.label20);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.labelSSN);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPatientEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Patient Information";
			this.Load += new System.EventHandler(this.FormPatientEdit_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupNotes.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormPatientEdit_Load(object sender, System.EventArgs e) {
			FillComboZip();
			if(IsNew){
				if(Patients.Cur.PriProv==0)
					Patients.Cur.PriProv=Convert.ToInt32(((Pref)Prefs.HList["PracticeDefaultProv"]).ValueString);
				if(Patients.Cur.BillingType==0)
					Patients.Cur.BillingType=Convert.ToInt32(((Pref)Prefs.HList["PracticeDefaultBillType"]).ValueString);
				//textAddrNotes.Enabled=false;
			}
			checkSame.Checked=true;
			checkNotesSame.Checked=true;
			//if(!IsNew){
			if(Patients.PatIsLoaded){
				for(int i=0;i<Patients.FamilyList.Length;i++){
					if(Patients.Cur.HmPhone!=Patients.FamilyList[i].HmPhone
						|| Patients.Cur.Address!=Patients.FamilyList[i].Address
						|| Patients.Cur.Address2!=Patients.FamilyList[i].Address2
						|| Patients.Cur.City!=Patients.FamilyList[i].City
						|| Patients.Cur.State!=Patients.FamilyList[i].State
						|| Patients.Cur.Zip!=Patients.FamilyList[i].Zip
						|| Patients.Cur.CreditType!=Patients.FamilyList[i].CreditType
						|| Patients.Cur.BillingType!=Patients.FamilyList[i].BillingType
						|| Patients.Cur.PriProv!=Patients.FamilyList[i].PriProv
						|| Patients.Cur.SecProv!=Patients.FamilyList[i].SecProv
						|| Patients.Cur.FeeSched!=Patients.FamilyList[i].FeeSched)
					{
						checkSame.Checked=false;
					}
					if(Patients.Cur.AddrNote!=Patients.FamilyList[i].AddrNote)
					{
						checkNotesSame.Checked=false;
					}
				}
			}
			textPatNum.Text=Patients.Cur.PatNum.ToString();
			textLName.Text=Patients.Cur.LName;
			textFName.Text=Patients.Cur.FName;
			textMiddleI.Text=Patients.Cur.MiddleI;
			textPreferred.Text=Patients.Cur.Preferred;
			textSalutation.Text=Patients.Cur.Salutation;
			listStatus.Items.Add(Lan.g("enumPatientStatus","Patient"));
			listStatus.Items.Add(Lan.g("enumPatientStatus","NonPatient"));
			listStatus.Items.Add(Lan.g("enumPatientStatus","Inactive"));
			listStatus.Items.Add(Lan.g("enumPatientStatus","Archived"));
			listStatus.Items.Add(Lan.g("enumPatientStatus","Deceased"));
			listGender.Items.Add(Lan.g("enumPatientGender","Male"));
			listGender.Items.Add(Lan.g("enumPatientGender","Female"));
			listGender.Items.Add(Lan.g("enumPatientGender","Unknown"));
			listPosition.Items.Add(Lan.g("enumPatientPosition","Single"));
			listPosition.Items.Add(Lan.g("enumPatientPosition","Married"));
			listPosition.Items.Add(Lan.g("enumPatientPosition","Child"));
			listPosition.Items.Add(Lan.g("enumPatientPosition","Widowed"));
			switch (Patients.Cur.PatStatus){
				case PatientStatus.Patient : listStatus.SelectedIndex=0; break;
				case PatientStatus.NonPatient : listStatus.SelectedIndex=1; break;
				case PatientStatus.Inactive : listStatus.SelectedIndex=2; break;
				case PatientStatus.Archived : listStatus.SelectedIndex=3; break;
				case PatientStatus.Deceased : listStatus.SelectedIndex=4; break;}
			switch (Patients.Cur.Gender){
				case PatientGender.Male : listGender.SelectedIndex=0; break;
				case PatientGender.Female : listGender.SelectedIndex=1; break;
				case PatientGender.Unknown : listGender.SelectedIndex=2; break;}
			switch (Patients.Cur.Position){
				case PatientPosition.Single : listPosition.SelectedIndex=0; break;
				case PatientPosition.Married : listPosition.SelectedIndex=1; break;
				case PatientPosition.Child : listPosition.SelectedIndex=2; break;
				case PatientPosition.Widowed : listPosition.SelectedIndex=3; break;}
			if(Patients.Cur.Birthdate.Year < 1880)
				textBirthdate.Text="";
			else
				textBirthdate.Text=Patients.Cur.Birthdate.ToShortDateString();
			textAge.Text=Patients.Cur.Age;
			if(CultureInfo.CurrentCulture.Name=="en-US"
				&& Patients.Cur.SSN!=null//the null catches new patients
				&& Patients.Cur.SSN.Length==9)
				textSSN.Text=Patients.Cur.SSN.Substring(0,3)+"-"
					+Patients.Cur.SSN.Substring(3,2)+"-"+Patients.Cur.SSN.Substring(5,4);
			else
				textSSN.Text=Patients.Cur.SSN;
			textMedicaidID.Text=Patients.Cur.MedicaidID;
			textAddress.Text=Patients.Cur.Address;
			textAddress2.Text=Patients.Cur.Address2;
			textCity.Text=Patients.Cur.City;
			textState.Text=Patients.Cur.State;
			textZip.Text=Patients.Cur.Zip;
			textHmPhone.Text=Patients.Cur.HmPhone;
			textWkPhone.Text=Patients.Cur.WkPhone;
			textWirelessPhone.Text=Patients.Cur.WirelessPhone;
			textEmail.Text=Patients.Cur.Email;
			textCreditType.Text=Patients.Cur.CreditType;
			textRecall.MaxVal=100;
			textRecall.Text=Patients.Cur.RecallInterval.ToString();
			textChartNumber.Text=Patients.Cur.ChartNumber;
			textEmployer.Text=Employers.GetName(Patients.Cur.EmployerNum);
			textEmploymentNote.Text=Patients.Cur.EmploymentNote;
			for(int i=0;i<Providers.List.Length;i++){
				listPriProv.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==Patients.Cur.PriProv)
					listPriProv.SelectedIndex=i;
			}
			for(int i=0;i<Providers.List.Length;i++){
				listSecProv.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==Patients.Cur.SecProv)
					listSecProv.SelectedIndex=i;
			}
			for(int i=0;i<Defs.Short[(int)DefCat.FeeSchedNames].Length;i++){
				listFeeSched.Items.Add(Defs.Short[(int)DefCat.FeeSchedNames][i].ItemName);
				if(Defs.Short[(int)DefCat.FeeSchedNames][i].DefNum==Patients.Cur.FeeSched)
					listFeeSched.SelectedIndex=i;
			}
			//MessageBox.Show(Defs.Short[(int)DefCat.BillingTypes].Length.ToString());
			for(int i=0;i<Defs.Short[(int)DefCat.BillingTypes].Length;i++){
				listBillType.Items.Add(Defs.Short[(int)DefCat.BillingTypes][i].ItemName);
				if(Defs.Short[(int)DefCat.BillingTypes][i].DefNum==Patients.Cur.BillingType)
					listBillType.SelectedIndex=i;
			}
			switch(Patients.Cur.StudentStatus){
				case "N"://non
				case "":
					radioStudentN.Checked=true;
					break;
				case "P"://parttime
					radioStudentP.Checked=true;
					break;
				case "F"://fulltime
					radioStudentF.Checked=true;
					break;
			}
			textSchool.Text=Patients.Cur.SchoolName;
			//if(!IsNew)
			//	textAddrNotes.Text=Patients.FamilyList[Patients.GuarIndex].FamAddrNote;
			textAddrNotes.Text=Patients.Cur.AddrNote;
			textLName.Select();
			FillTable();
		}

		private void FillComboZip(){
			ZipCodes.Refresh();
			comboZip.Items.Clear();
			for(int i=0;i<ZipCodes.ALFrequent.Count;i++){
				comboZip.Items.Add(((ZipCode)ZipCodes.ALFrequent[i]).ZipCodeDigits
					+"("+((ZipCode)ZipCodes.ALFrequent[i]).City+")");
			}
		}

		/*private void radioPat_Click(object sender, System.EventArgs e) {
			Patients.Cur.PatStatus=PatientStatus.Patient;
		}

		private void radioNonPt_Click(object sender, System.EventArgs e) {
			Patients.Cur.PatStatus=PatientStatus.NonPatient;
		}

		private void radioInact_Click(object sender, System.EventArgs e) {
			Patients.Cur.PatStatus=PatientStatus.Inactive;
		}

		private void radioArchiv_Click(object sender, System.EventArgs e) {
			Patients.Cur.PatStatus=PatientStatus.Archived;
		}

		private void radioMale_Click(object sender, System.EventArgs e) {
			Patients.Cur.Gender=PatientGender.Male;
		}

		private void radioFemale_Click(object sender, System.EventArgs e) {
			Patients.Cur.Gender=PatientGender.Female;
		}

		private void radioUnknown_Click(object sender, System.EventArgs e) {
			Patients.Cur.Gender=PatientGender.Unknown;
		}

		private void radioSingle_Click(object sender, System.EventArgs e) {
			Patients.Cur.Position=PatientPosition.Single;
		}

		private void radioMarried_Click(object sender, System.EventArgs e) {
			Patients.Cur.Position=PatientPosition.Married;
		}

		private void radioChild_Click(object sender, System.EventArgs e) {
			Patients.Cur.Position=PatientPosition.Child;
		}*/

		private void butSecClear_Click(object sender, System.EventArgs e) {
			listSecProv.SelectedIndex=-1;
		}

		private void butClearFee_Click(object sender, System.EventArgs e) {
			listFeeSched.SelectedIndex=-1;
		}

		private void textLName_TextChanged(object sender, System.EventArgs e) {
			if(textLName.Text.Length==1){
				textLName.Text=textLName.Text.ToUpper();
				textLName.SelectionStart=1;
			}
		}

		private void textFName_TextChanged(object sender, System.EventArgs e) {
			if(textFName.Text.Length==1){
				textFName.Text=textFName.Text.ToUpper();
				textFName.SelectionStart=1;
			}
		}

		private void textMiddleI_TextChanged(object sender, System.EventArgs e) {
			if(textMiddleI.Text.Length==1){
				textMiddleI.Text=textMiddleI.Text.ToUpper();
				textMiddleI.SelectionStart=1;
			}
		}

		private void textPreferred_TextChanged(object sender, System.EventArgs e) {
			if(textPreferred.Text.Length==1){
				textPreferred.Text=textPreferred.Text.ToUpper();
				textPreferred.SelectionStart=1;
			}
		}

		private void textSalutation_TextChanged(object sender, System.EventArgs e) {
			if(textSalutation.Text.Length==1){
				textSalutation.Text=textSalutation.Text.ToUpper();
				textSalutation.SelectionStart=1;
			}
		}

		private void textAddress_TextChanged(object sender, System.EventArgs e) {
			if(textAddress.Text.Length==1){
				textAddress.Text=textAddress.Text.ToUpper();
				textAddress.SelectionStart=1;
			}
		}

		private void textAddress2_TextChanged(object sender, System.EventArgs e) {
			if(textAddress2.Text.Length==1){
				textAddress2.Text=textAddress2.Text.ToUpper();
				textAddress2.SelectionStart=1;
			}
		}

		private void radioStudentN_Click(object sender, System.EventArgs e) {
			Patients.Cur.StudentStatus="N";
		}

		private void radioStudentF_Click(object sender, System.EventArgs e) {
			Patients.Cur.StudentStatus="F";
		}

		private void radioStudentP_Click(object sender, System.EventArgs e) {
			Patients.Cur.StudentStatus="P";
		}

		private void textSSN_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			if(CultureInfo.CurrentCulture.Name!="en-US"){
				return;
			}
			//only reformats if in USA and exactly 9 digits.
			if(textSSN.Text.Length!=9) return;
			bool SSNisValid=true;
			for(int i=0;i<textSSN.Text.Length;i++){
				if(!Char.IsNumber(textSSN.Text,i)){
					SSNisValid=false;
				}
			}
			if(!SSNisValid) return;
			textSSN.Text
				=textSSN.Text.Substring(0,3)+"-"
				+textSSN.Text.Substring(3,2)+"-"
				+textSSN.Text.Substring(5,4);	
		}

		private void textCreditType_TextChanged(object sender, System.EventArgs e) {
			textCreditType.Text=textCreditType.Text.ToUpper();
			textCreditType.SelectionStart=1;
		}

		private void textBirthdate_Validated(object sender, System.EventArgs e) {
			if(textBirthdate.errorProvider1.GetError(textBirthdate)!=""){
				textAge.Text="";
				return;
			}
			DateTime birthdate=PIn.PDate(textBirthdate.Text);
			if(birthdate>DateTime.Today){
				birthdate=birthdate.AddYears(-100);
			}
			textAge.Text=Shared.DateToAge(birthdate);
		}

		private void textZip_TextChanged(object sender, System.EventArgs e) {
			comboZip.SelectedIndex=-1;
		}

		private void comboZip_SelectionChangeCommitted(object sender, System.EventArgs e) {
			//this happens when a zipcode is selected from the combobox of frequent zips.
			//The combo box is tucked under textZip because Microsoft makes stupid controls.
			textCity.Text=((ZipCode)ZipCodes.ALFrequent[comboZip.SelectedIndex]).City;
			textState.Text=((ZipCode)ZipCodes.ALFrequent[comboZip.SelectedIndex]).State;
			textZip.Text=((ZipCode)ZipCodes.ALFrequent[comboZip.SelectedIndex]).ZipCodeDigits;
		}

		private void textZip_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			//fired as soon as control loses focus.
			//it's here to validate if zip is typed in to text box instead of picked from list.
			//if(textZip.Text=="" && (textCity.Text!="" || textState.Text!="")){
			//	if(MessageBox.Show(Lan.g(this,"Delete the City and State?"),"",MessageBoxButtons.OKCancel)
			//		==DialogResult.OK){
			//		textCity.Text="";
			//		textState.Text="";
			//	}	
			//	return;
			//}
			if(textZip.Text.Length<5){
				return;
			}
			if(comboZip.SelectedIndex!=-1){
				return;
			}
			//the autofill only works if both city and state are left blank
			if(textCity.Text!="" || textState.Text!=""){
				return;
			}
			ZipCodes.GetALMatches(textZip.Text);
			if(ZipCodes.ALMatches.Count==0){
				//No match found. Must enter info for new zipcode
				ZipCodes.Cur=new ZipCode();
				ZipCodes.Cur.ZipCodeDigits=textZip.Text;
				FormZipCodeEdit FormZE=new FormZipCodeEdit();
				FormZE.IsNew=true;
				FormZE.ShowDialog();
				FillComboZip();//does the refresh
				if(FormZE.DialogResult!=DialogResult.OK){
					return;
				}
				textCity.Text=ZipCodes.Cur.City;
				textState.Text=ZipCodes.Cur.State;
				textZip.Text=ZipCodes.Cur.ZipCodeDigits;
			}
			else if(ZipCodes.ALMatches.Count==1){
				//only one match found.  Use it.
				textCity.Text=((ZipCode)ZipCodes.ALMatches[0]).City;
				textState.Text=((ZipCode)ZipCodes.ALMatches[0]).State;
			}
			else{
				//multiple matches found.  Pick one
				FormZipSelect FormZS=new FormZipSelect();
				FormZS.ShowDialog();
				FillComboZip();
				if(FormZS.DialogResult!=DialogResult.OK){
					return;
				}
				textCity.Text=ZipCodes.Cur.City;
				textState.Text=ZipCodes.Cur.State;
				textZip.Text=ZipCodes.Cur.ZipCodeDigits;
			}
		}

		private void butEditZip_Click(object sender, System.EventArgs e) {
			if(textZip.Text.Length==0){
				MessageBox.Show(Lan.g(this,"Please enter a zipcode first."));
				return;
			}
			ZipCodes.GetALMatches(textZip.Text);
			if(ZipCodes.ALMatches.Count==0){
				ZipCodes.Cur=new ZipCode();
				ZipCodes.Cur.ZipCodeDigits=textZip.Text;
				FormZipCodeEdit FormZE=new FormZipCodeEdit();
				FormZE.IsNew=true;
				FormZE.ShowDialog();
				FillComboZip();
				if(FormZE.DialogResult!=DialogResult.OK){
					return;
				}
			}
			else{
				FormZipSelect FormZS=new FormZipSelect();
				FormZS.ShowDialog();
				FillComboZip();
				if(FormZS.DialogResult!=DialogResult.OK){
					return;
				}
			}
			textCity.Text=ZipCodes.Cur.City;
			textState.Text=ZipCodes.Cur.State;
			textZip.Text=ZipCodes.Cur.ZipCodeDigits;
		}

		private void textWirelessPhone_TextChanged(object sender, System.EventArgs e) {
			int cursor=textWirelessPhone.SelectionStart;
			int length=textWirelessPhone.Text.Length;
			textWirelessPhone.Text=TelephoneNumbers.AutoFormat(textWirelessPhone.Text);
			if(textWirelessPhone.Text.Length>length)
				cursor++;
			textWirelessPhone.SelectionStart=cursor;		
		}

		private void textWkPhone_TextChanged(object sender, System.EventArgs e) {
		 	int cursor=textWkPhone.SelectionStart;
			int length=textWkPhone.Text.Length;
			textWkPhone.Text=TelephoneNumbers.AutoFormat(textWkPhone.Text);
			if(textWkPhone.Text.Length>length)
				cursor++;
			textWkPhone.SelectionStart=cursor;		
		}

		private void textHmPhone_TextChanged(object sender, System.EventArgs e) {
		 	int cursor=textHmPhone.SelectionStart;
			int length=textHmPhone.Text.Length;
			textHmPhone.Text=TelephoneNumbers.AutoFormat(textHmPhone.Text);
			if(textHmPhone.Text.Length>length)
				cursor++;
			textHmPhone.SelectionStart=cursor;		
		}

		private void butAuto_Click(object sender, System.EventArgs e) {
			textChartNumber.Text=Patients.GetNextChartNum();
		}

		private void textCity_TextChanged(object sender, System.EventArgs e) {
			if(textCity.Text.Length==1){
				textCity.Text=textCity.Text.ToUpper();
				textCity.SelectionStart=1;
			}
		}

		private void textState_TextChanged(object sender, System.EventArgs e) {
			if(CultureInfo.CurrentCulture.Name=="en-US" //if USA or Canada, capitalize first 2 letters
				|| CultureInfo.CurrentCulture.Name.Substring(3)=="CA"){
				if(textState.Text.Length==1 || textState.Text.Length==2){
					textState.Text=textState.Text.ToUpper();
					textState.SelectionStart=2;
				}
			}
			else{
				if(textState.Text.Length==1){
					textState.Text=textState.Text.ToUpper();
					textState.SelectionStart=1;
				}
			}
		}

		#region Referrals
		private void FillTable(){
			//Referrals.Refresh();//faster to do this every time a referral is changed instead of here.
			RefAttaches.Refresh();//for this patient only
			tbRefList.ResetRows(RefAttaches.List.Length);
			tbRefList.SetGridColor(Color.Gray);
			tbRefList.SetBackGColor(Color.White);
			for(int i=0;i<RefAttaches.List.Length;i++){
				if(RefAttaches.List[i].IsFrom){ 
					tbRefList.Cell[0,i]=Lan.g(this,"From");
				}
				else{
					tbRefList.Cell[0,i]=Lan.g(this,"To");
				}
				Referrals.GetCur(RefAttaches.List[i].ReferralNum);
				tbRefList.Cell[1,i]=Referrals.Cur.LName+", "+Referrals.Cur.FName +" "+Referrals.Cur.MName;
				if(RefAttaches.List[i].RefDate.Year < 1880)
					tbRefList.Cell[2,i]="";
				else
					tbRefList.Cell[2,i]=RefAttaches.List[i].RefDate.ToShortDateString();
				tbRefList.Cell[3,i]=Referrals.Cur.Note;
			}
			tbRefList.LayoutTables();
		}

		private void tbRefList_CellDoubleClicked(object sender, CellEventArgs e){
			RefAttaches.Cur=RefAttaches.List[e.Row];
			FormRefAttachEdit FormRAE2=new FormRefAttachEdit();
			FormRAE2.ShowDialog();
			FillTable();  
		}

		private void butDelete_Click(object sender, System.EventArgs e){
			if(tbRefList.SelectedRow==-1){
				MessageBox.Show(Lan.g(this,"Please select item first."));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete Referral?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;   
			}
			RefAttaches.Cur=RefAttaches.List[tbRefList.SelectedRow];
			RefAttaches.DeleteCur(); 
			FillTable();		
		}

		private void butAdd_Click(object sender, System.EventArgs e){
			RefAttaches.Cur=new RefAttach();
			RefAttaches.Cur.PatNum=Patients.Cur.PatNum;  
			FormRefAttachEdit FormRAE2=new FormRefAttachEdit(); 
			FormReferralSelect FormReferralSelect2=new FormReferralSelect();
			FormReferralSelect2.ShowDialog();
			if(FormReferralSelect2.DialogResult!=DialogResult.OK){
				return;
			}
			FormRAE2.IsNew=true;
			FormRAE2.ShowDialog();
			if(FormRAE2.DialogResult!=DialogResult.OK){
				return;
			}
			FillTable();
		}

		#endregion

		private void butChangeEmp_Click(object sender, System.EventArgs e) {
			FormEmployers FormE=new FormEmployers();
			FormE.IsSelectMode=true;
			Employers.Cur=Employers.GetEmployer(Patients.Cur.EmployerNum);
			FormE.ShowDialog();
			if(FormE.DialogResult!=DialogResult.OK){
				return;
			}
			Patients.Cur.EmployerNum=Employers.Cur.EmployerNum;
			textEmployer.Text=Employers.Cur.EmpName;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
		
		private void butOK_Click(object sender, System.EventArgs e) {
			if(  textBirthdate.errorProvider1.GetError(textBirthdate)!=""
				|| textRecall.errorProvider1.GetError(textRecall)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(textLName.Text==""){
				MessageBox.Show(Lan.g(this,"Last Name must be entered."));
				return;
			}
			//see if chartNum is a duplicate
			if(textChartNumber.Text!=""){
				//the patNum will be 0 for new
				if(!Patients.ChartNumIsUnique(textChartNumber.Text,Patients.Cur.PatNum)){
					MessageBox.Show(Lan.g(this,"Chart number must be unique or blank."));
					return;
				}
			}
			Patients.Cur.LName=textLName.Text;
			Patients.Cur.FName=textFName.Text;
			Patients.Cur.MiddleI=textMiddleI.Text;
			Patients.Cur.Preferred=textPreferred.Text;
			Patients.Cur.Salutation=textSalutation.Text;
			switch(listStatus.SelectedIndex){
				case 0: Patients.Cur.PatStatus=PatientStatus.Patient; break;
				case 1: Patients.Cur.PatStatus=PatientStatus.NonPatient; break;
				case 2: Patients.Cur.PatStatus=PatientStatus.Inactive; break;
				case 3: Patients.Cur.PatStatus=PatientStatus.Archived; break;
				case 4: Patients.Cur.PatStatus=PatientStatus.Deceased; break;
			}
			switch(listGender.SelectedIndex){
				case 0: Patients.Cur.Gender=PatientGender.Male; break;
				case 1: Patients.Cur.Gender=PatientGender.Female; break;
				case 2: Patients.Cur.Gender=PatientGender.Unknown; break;
			}
			switch(listPosition.SelectedIndex){
				case 0: Patients.Cur.Position=PatientPosition.Single; break;
				case 1: Patients.Cur.Position=PatientPosition.Married; break;
				case 2: Patients.Cur.Position=PatientPosition.Child; break;
				case 3: Patients.Cur.Position=PatientPosition.Widowed; break;
			}
			Patients.Cur.Birthdate=PIn.PDate(textBirthdate.Text);
			if(CultureInfo.CurrentCulture.Name=="en-US"){
				if(textSSN.Text!=""){
					if(!Regex.IsMatch(textSSN.Text,@"\d\d\d-\d\d-\d\d\d\d")){
						MessageBox.Show(Lan.g(this,"SSN not valid."));
						return;
					}
					//MessageBox.Show("*"+textSSN.Text+"*");
					Patients.Cur.SSN=textSSN.Text.Substring(0,3)+textSSN.Text.Substring(4,2)
						+textSSN.Text.Substring(7,4);
				}
				else{
					Patients.Cur.SSN="";
				}
			}
			else{//other cultures
				Patients.Cur.SSN=textSSN.Text;
			}
			Patients.Cur.MedicaidID=textMedicaidID.Text;
			Patients.Cur.WkPhone=textWkPhone.Text;
			Patients.Cur.WirelessPhone=textWirelessPhone.Text;
			Patients.Cur.Email=textEmail.Text;
			Patients.Cur.RecallInterval=PIn.PInt(textRecall.Text);
			Patients.Cur.ChartNumber=textChartNumber.Text;
			Patients.Cur.SchoolName=textSchool.Text;
			//address:
			Patients.Cur.HmPhone=textHmPhone.Text;
			Patients.Cur.Address=textAddress.Text;
			Patients.Cur.Address2=textAddress2.Text;
			Patients.Cur.City=textCity.Text;
			Patients.Cur.State=textState.Text;
			Patients.Cur.Zip=textZip.Text;
			Patients.Cur.CreditType=textCreditType.Text;
			//employmentNum already handled
			Patients.Cur.EmploymentNote=textEmploymentNote.Text;
			Patients.Cur.AddrNote=textAddrNotes.Text;
			if(listPriProv.SelectedIndex==-1)
				Patients.Cur.PriProv=0;
			else{
				Patients.Cur.PriProv=Providers.List[listPriProv.SelectedIndex].ProvNum;
			}
			if(listSecProv.SelectedIndex==-1)
				Patients.Cur.SecProv=0;
			else
				Patients.Cur.SecProv=Providers.List[listSecProv.SelectedIndex].ProvNum;
			if(listFeeSched.SelectedIndex==-1)
				Patients.Cur.FeeSched=0;
			else
				Patients.Cur.FeeSched=Defs.Short[(int)DefCat.FeeSchedNames][listFeeSched.SelectedIndex].DefNum;
			if(listBillType.SelectedIndex!=-1)
				Patients.Cur.BillingType=Defs.Short[(int)DefCat.BillingTypes][listBillType.SelectedIndex].DefNum;
			if(IsNew){
				Patients.InsertCur();//also gets insertID
				if(Patients.Cur.Guarantor==0){
					Patients.Cur.Guarantor=Patients.Cur.PatNum;
					Patients.UpdateCur();
				}
			}
			else{
				Patients.UpdateCur();
			}
			if(checkSame.Checked){
				//might want to include a mechanism for comparing fields to be overwritten
				Patients.UpdateAddressForFam();
			}
			if(checkNotesSame.Checked){
				Patients.UpdateNotesForFam();
			}
			//If this patient is also a referral source,
			//keep address info synched:
			for(int i=0;i<Referrals.List.Length;i++){
				if(Referrals.List[i].PatNum==Patients.Cur.PatNum){
					Referrals.Cur=Referrals.List[i];
					Referrals.Cur.LName=Patients.Cur.LName;
					Referrals.Cur.FName=Patients.Cur.FName;
					Referrals.Cur.MName=Patients.Cur.MiddleI;
					Referrals.Cur.Address=Patients.Cur.Address;
					Referrals.Cur.Address2=Patients.Cur.Address2;
					Referrals.Cur.City=Patients.Cur.City;
					Referrals.Cur.ST=Patients.Cur.State;
					Referrals.Cur.SSN=Patients.Cur.SSN;
					Referrals.Cur.Zip=Patients.Cur.Zip;
					Referrals.Cur.Telephone=TelephoneNumbers.FormatNumbersOnly(Patients.Cur.HmPhone);
					Referrals.Cur.EMail=Patients.Cur.Email;
					Referrals.UpdateCur();
					Referrals.Refresh();
					break;
				}
			}
			DialogResult=DialogResult.OK;
		}

		

		

		

		

		

		

		

		

		

		
	}
}









