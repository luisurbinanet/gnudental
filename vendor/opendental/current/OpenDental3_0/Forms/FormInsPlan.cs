/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace OpenDental{
///<summary>Must refresh Patients.GetFamily after exiting this form because a lot could have changed including drop or delete.</summary>
	public class FormInsPlan : System.Windows.Forms.Form{
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
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
		///<summary></summary>
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
		private OpenDental.ValidNumber textDeductible;
		private OpenDental.ValidNumber textFloToAge;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.ListBox listFeeSched;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkRelease;
		private System.Windows.Forms.CheckBox checkNoSendElect;
		private System.Windows.Forms.ListBox listClaimForm;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.CheckBox checkAlternateCode;
		private System.Windows.Forms.CheckBox checkClaimsUseUCR;
		private System.Windows.Forms.ListBox listCopay;
		private System.Windows.Forms.ListBox listPlanType;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Button butCopayNone;
		private System.Windows.Forms.TextBox textSubscriber;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox checkSubOtherFam;
		private OpenDental.XPButton butDelete;
		private System.Windows.Forms.TextBox textSubscriberID;
		private System.Windows.Forms.Label label2;
		private OpenDental.ValidNum textRenewMonth;
		private System.Windows.Forms.Button butChangeEmp;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Label labelCopayFee;
		private System.Windows.Forms.Label labelCopayAdvice;
		private System.Windows.Forms.Label labelOrthoMax;
		private System.Windows.Forms.Panel panelAdvancedIns;
		private System.Windows.Forms.Button butChangeCarrier;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboLinked;
		private System.Windows.Forms.TextBox textLinkedNum;
		private System.Windows.Forms.Button butDetach;
		private System.Windows.Forms.Button butChangeLink;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button butEdit;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.Button butEditTemplate;
		private System.Windows.Forms.CheckBox checkAssign;
		private ArrayList similarEmps;
		private string empOriginal;//used in the emp dropdown logic
		private System.Windows.Forms.ListBox listEmps;//displayed from within code, not designer
		private bool mouseIsInListEmps;
		private System.Windows.Forms.GroupBox groupSynch;
		private ArrayList similarCars;
		private string carOriginal;
		private System.Windows.Forms.ListBox listCars;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button butSelect;
		private System.Windows.Forms.Label labelCitySTZip;
		private bool mouseIsInListCars;
		private System.Windows.Forms.Label labelDrop;
		private System.Windows.Forms.Button butDrop;
		///<summary></summary>
		public bool DropButVisible;

		///<summary></summary>
		public FormInsPlan(){
			InitializeComponent();
			listEmps=new ListBox();
			listEmps.Location=new Point(groupBox4.Left+textEmployer.Left,groupBox4.Top+textEmployer.Bottom);
			listEmps.Size=new Size(231,100);
			listEmps.Visible=false;
			listEmps.Click += new System.EventHandler(listEmps_Click);
			listEmps.DoubleClick += new System.EventHandler(listEmps_DoubleClick);
			listEmps.MouseEnter += new System.EventHandler(listEmps_MouseEnter);
			listEmps.MouseLeave += new System.EventHandler(listEmps_MouseLeave);
			Controls.Add(listEmps);
			listEmps.BringToFront();
			listCars=new ListBox();
			listCars.Location=new Point(groupBox4.Left+groupBox3.Left+textCarrier.Left
				,groupBox4.Top+groupBox3.Top+textCarrier.Bottom);
			listCars.Size=new Size(291,100);
			listCars.Visible=false;
			listCars.Click += new System.EventHandler(listCars_Click);
			listCars.DoubleClick += new System.EventHandler(listCars_DoubleClick);
			listCars.MouseEnter += new System.EventHandler(listCars_MouseEnter);
			listCars.MouseLeave += new System.EventHandler(listCars_MouseLeave);
			Controls.Add(listCars);
			listCars.BringToFront();
			tbPercent1.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbPercent1_CellClicked);
			Lan.C(this, new System.Windows.Forms.Control[] {
				label2,
				groupBox3,
				label5,
				label6,
				label7,
				label8,
				label9,
				label10,
				labelCitySTZip,
				label14,
				label15,
				label17,
				label18,
				label19,
				label20,
				labelOrthoMax,
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
				labelCopayAdvice,
				label1,
				groupBox1,
				groupBox2,
				checkRelease,
				checkAssign,
				label23,
				checkSubOtherFam,
				//checkWriteOff,
				checkAlternateCode,
				checkClaimsUseUCR,
				butCopayNone,
				labelCopayFee,
				butChangeEmp
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel
			});
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="CA"){//en-CA or fr-CA
				labelCitySTZip.Text="City,Prov,Post";   //Postal Code";
			}
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="GB"){//en-GB
				labelCitySTZip.Text="City,Postcode";//Postcode";
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormInsPlan));
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.labelCitySTZip = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.labelOrthoMax = new System.Windows.Forms.Label();
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
			this.textEmployer = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.textAddress2 = new System.Windows.Forms.TextBox();
			this.label21 = new System.Windows.Forms.Label();
			this.tbPercent1 = new OpenDental.TablePercent();
			this.textAnnualMax = new OpenDental.ValidNumber();
			this.textOrthoMax = new OpenDental.ValidNumber();
			this.textDeductible = new OpenDental.ValidNumber();
			this.textFloToAge = new OpenDental.ValidNumber();
			this.label22 = new System.Windows.Forms.Label();
			this.listFeeSched = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkAssign = new System.Windows.Forms.CheckBox();
			this.checkRelease = new System.Windows.Forms.CheckBox();
			this.checkNoSendElect = new System.Windows.Forms.CheckBox();
			this.listClaimForm = new System.Windows.Forms.ListBox();
			this.label23 = new System.Windows.Forms.Label();
			this.checkAlternateCode = new System.Windows.Forms.CheckBox();
			this.checkClaimsUseUCR = new System.Windows.Forms.CheckBox();
			this.labelCopayFee = new System.Windows.Forms.Label();
			this.listCopay = new System.Windows.Forms.ListBox();
			this.listPlanType = new System.Windows.Forms.ListBox();
			this.label14 = new System.Windows.Forms.Label();
			this.butCopayNone = new System.Windows.Forms.Button();
			this.labelCopayAdvice = new System.Windows.Forms.Label();
			this.textSubscriber = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkSubOtherFam = new System.Windows.Forms.CheckBox();
			this.textSubscriberID = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.XPButton();
			this.textRenewMonth = new OpenDental.ValidNum();
			this.butChangeEmp = new System.Windows.Forms.Button();
			this.groupSynch = new System.Windows.Forms.GroupBox();
			this.butSelect = new System.Windows.Forms.Button();
			this.butEditTemplate = new System.Windows.Forms.Button();
			this.comboLinked = new System.Windows.Forms.ComboBox();
			this.textLinkedNum = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.butChangeLink = new System.Windows.Forms.Button();
			this.butDetach = new System.Windows.Forms.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.butEdit = new System.Windows.Forms.Button();
			this.butChangeCarrier = new System.Windows.Forms.Button();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.panelAdvancedIns = new System.Windows.Forms.Panel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.labelDrop = new System.Windows.Forms.Label();
			this.butDrop = new System.Windows.Forms.Button();
			this.panel4.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupSynch.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.panelAdvancedIns.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.SuspendLayout();
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(7, 91);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 15);
			this.label5.TabIndex = 5;
			this.label5.Text = "Date Effective";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(5, 112);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(103, 15);
			this.label6.TabIndex = 6;
			this.label6.Text = "Date Terminated";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(3, 31);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(95, 15);
			this.label7.TabIndex = 7;
			this.label7.Text = "Phone";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 39);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(95, 15);
			this.label8.TabIndex = 8;
			this.label8.Text = "Group Name";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(8, 59);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(95, 15);
			this.label9.TabIndex = 9;
			this.label9.Text = "Group Num";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(3, 51);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(95, 15);
			this.label10.TabIndex = 10;
			this.label10.Text = "Address";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelCitySTZip
			// 
			this.labelCitySTZip.Location = new System.Drawing.Point(1, 91);
			this.labelCitySTZip.Name = "labelCitySTZip";
			this.labelCitySTZip.Size = new System.Drawing.Size(95, 15);
			this.labelCitySTZip.TabIndex = 11;
			this.labelCitySTZip.Text = "City,ST,Zip";
			this.labelCitySTZip.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(2, 111);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(95, 15);
			this.label15.TabIndex = 15;
			this.label15.Text = "Electronic ID";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(16, 20);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(91, 12);
			this.label17.TabIndex = 17;
			this.label17.Text = "Annual Max $";
			this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(14, 80);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(94, 12);
			this.label18.TabIndex = 18;
			this.label18.Text = "Renew Month";
			this.label18.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(13, 61);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(93, 12);
			this.label19.TabIndex = 19;
			this.label19.Text = "Deductible $";
			this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(3, 2);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(96, 26);
			this.label20.TabIndex = 20;
			this.label20.Text = "Deduct waived on Prevent?";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelOrthoMax
			// 
			this.labelOrthoMax.Location = new System.Drawing.Point(19, 40);
			this.labelOrthoMax.Name = "labelOrthoMax";
			this.labelOrthoMax.Size = new System.Drawing.Size(87, 12);
			this.labelOrthoMax.TabIndex = 24;
			this.labelOrthoMax.Text = "Ortho Max $";
			this.labelOrthoMax.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label27
			// 
			this.label27.Location = new System.Drawing.Point(24, 35);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(72, 16);
			this.label27.TabIndex = 27;
			this.label27.Text = "Flo to Age";
			this.label27.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label28
			// 
			this.label28.Location = new System.Drawing.Point(11, 372);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(98, 16);
			this.label28.TabIndex = 28;
			this.label28.Text = "Ins Plan Note";
			this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCarrier
			// 
			this.textCarrier.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textCarrier.Location = new System.Drawing.Point(100, 8);
			this.textCarrier.MaxLength = 50;
			this.textCarrier.Name = "textCarrier";
			this.textCarrier.Size = new System.Drawing.Size(291, 21);
			this.textCarrier.TabIndex = 0;
			this.textCarrier.Text = "";
			this.textCarrier.Leave += new System.EventHandler(this.textCarrier_Leave);
			this.textCarrier.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textCarrier_KeyUp);
			// 
			// textPhone
			// 
			this.textPhone.Location = new System.Drawing.Point(100, 29);
			this.textPhone.MaxLength = 30;
			this.textPhone.Name = "textPhone";
			this.textPhone.Size = new System.Drawing.Size(157, 20);
			this.textPhone.TabIndex = 1;
			this.textPhone.Text = "";
			this.textPhone.TextChanged += new System.EventHandler(this.textPhone_TextChanged);
			// 
			// textGroupName
			// 
			this.textGroupName.Location = new System.Drawing.Point(104, 36);
			this.textGroupName.MaxLength = 50;
			this.textGroupName.Name = "textGroupName";
			this.textGroupName.Size = new System.Drawing.Size(193, 20);
			this.textGroupName.TabIndex = 1;
			this.textGroupName.Text = "";
			// 
			// textGroupNum
			// 
			this.textGroupNum.Location = new System.Drawing.Point(104, 56);
			this.textGroupNum.MaxLength = 20;
			this.textGroupNum.Name = "textGroupNum";
			this.textGroupNum.Size = new System.Drawing.Size(129, 20);
			this.textGroupNum.TabIndex = 2;
			this.textGroupNum.Text = "";
			// 
			// textAddress
			// 
			this.textAddress.Location = new System.Drawing.Point(100, 49);
			this.textAddress.MaxLength = 60;
			this.textAddress.Name = "textAddress";
			this.textAddress.Size = new System.Drawing.Size(291, 20);
			this.textAddress.TabIndex = 2;
			this.textAddress.Text = "";
			this.textAddress.TextChanged += new System.EventHandler(this.textAddress_TextChanged);
			// 
			// textCity
			// 
			this.textCity.Location = new System.Drawing.Point(100, 89);
			this.textCity.MaxLength = 40;
			this.textCity.Name = "textCity";
			this.textCity.Size = new System.Drawing.Size(155, 20);
			this.textCity.TabIndex = 4;
			this.textCity.Text = "";
			this.textCity.TextChanged += new System.EventHandler(this.textCity_TextChanged);
			// 
			// textState
			// 
			this.textState.Location = new System.Drawing.Point(255, 89);
			this.textState.MaxLength = 2;
			this.textState.Name = "textState";
			this.textState.Size = new System.Drawing.Size(65, 20);
			this.textState.TabIndex = 5;
			this.textState.Text = "";
			this.textState.TextChanged += new System.EventHandler(this.textState_TextChanged);
			// 
			// textZip
			// 
			this.textZip.Location = new System.Drawing.Point(320, 89);
			this.textZip.MaxLength = 10;
			this.textZip.Name = "textZip";
			this.textZip.Size = new System.Drawing.Size(71, 20);
			this.textZip.TabIndex = 6;
			this.textZip.Text = "";
			// 
			// textElectID
			// 
			this.textElectID.Location = new System.Drawing.Point(100, 109);
			this.textElectID.MaxLength = 5;
			this.textElectID.Name = "textElectID";
			this.textElectID.Size = new System.Drawing.Size(54, 20);
			this.textElectID.TabIndex = 7;
			this.textElectID.Text = "";
			// 
			// textPlanNote
			// 
			this.textPlanNote.AcceptsReturn = true;
			this.textPlanNote.Location = new System.Drawing.Point(109, 371);
			this.textPlanNote.Multiline = true;
			this.textPlanNote.Name = "textPlanNote";
			this.textPlanNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textPlanNote.Size = new System.Drawing.Size(350, 95);
			this.textPlanNote.TabIndex = 6;
			this.textPlanNote.Text = "";
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(761, 637);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(851, 637);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.radioWaitUnkn);
			this.panel4.Controls.Add(this.radioWaitNo);
			this.panel4.Controls.Add(this.radioWaitYes);
			this.panel4.Location = new System.Drawing.Point(103, 92);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(203, 14);
			this.panel4.TabIndex = 3;
			// 
			// radioWaitUnkn
			// 
			this.radioWaitUnkn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioWaitUnkn.Location = new System.Drawing.Point(104, -1);
			this.radioWaitUnkn.Name = "radioWaitUnkn";
			this.radioWaitUnkn.Size = new System.Drawing.Size(80, 15);
			this.radioWaitUnkn.TabIndex = 2;
			this.radioWaitUnkn.Text = "Unknown";
			this.radioWaitUnkn.Click += new System.EventHandler(this.radioWaitUnkn_Click);
			// 
			// radioWaitNo
			// 
			this.radioWaitNo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioWaitNo.Location = new System.Drawing.Point(52, -1);
			this.radioWaitNo.Name = "radioWaitNo";
			this.radioWaitNo.Size = new System.Drawing.Size(48, 15);
			this.radioWaitNo.TabIndex = 1;
			this.radioWaitNo.Text = "No";
			this.radioWaitNo.Click += new System.EventHandler(this.radioWaitNo_Click);
			// 
			// radioWaitYes
			// 
			this.radioWaitYes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioWaitYes.Location = new System.Drawing.Point(0, -1);
			this.radioWaitYes.Name = "radioWaitYes";
			this.radioWaitYes.Size = new System.Drawing.Size(46, 15);
			this.radioWaitYes.TabIndex = 0;
			this.radioWaitYes.Text = "Yes";
			this.radioWaitYes.Click += new System.EventHandler(this.radioWaitYes_Click);
			// 
			// label30
			// 
			this.label30.Location = new System.Drawing.Point(27, 84);
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
			this.panel2.Location = new System.Drawing.Point(103, 60);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(203, 14);
			this.panel2.TabIndex = 2;
			// 
			// radioMissUnkn
			// 
			this.radioMissUnkn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioMissUnkn.Location = new System.Drawing.Point(104, -1);
			this.radioMissUnkn.Name = "radioMissUnkn";
			this.radioMissUnkn.Size = new System.Drawing.Size(80, 15);
			this.radioMissUnkn.TabIndex = 2;
			this.radioMissUnkn.Text = "Unknown";
			this.radioMissUnkn.Click += new System.EventHandler(this.radioMissUnkn_Click);
			// 
			// radioMissNo
			// 
			this.radioMissNo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioMissNo.Location = new System.Drawing.Point(52, -1);
			this.radioMissNo.Name = "radioMissNo";
			this.radioMissNo.Size = new System.Drawing.Size(48, 15);
			this.radioMissNo.TabIndex = 1;
			this.radioMissNo.Text = "No";
			this.radioMissNo.Click += new System.EventHandler(this.radioMissNo_Click);
			// 
			// radioMissYes
			// 
			this.radioMissYes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioMissYes.Location = new System.Drawing.Point(0, -1);
			this.radioMissYes.Name = "radioMissYes";
			this.radioMissYes.Size = new System.Drawing.Size(46, 15);
			this.radioMissYes.TabIndex = 0;
			this.radioMissYes.Text = "Yes";
			this.radioMissYes.Click += new System.EventHandler(this.radioMissYes_Click);
			// 
			// label29
			// 
			this.label29.Location = new System.Drawing.Point(17, 54);
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
			this.panel3.Location = new System.Drawing.Point(103, 8);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(200, 14);
			this.panel3.TabIndex = 0;
			// 
			// radioDedUnkn
			// 
			this.radioDedUnkn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioDedUnkn.Location = new System.Drawing.Point(104, -1);
			this.radioDedUnkn.Name = "radioDedUnkn";
			this.radioDedUnkn.Size = new System.Drawing.Size(80, 15);
			this.radioDedUnkn.TabIndex = 2;
			this.radioDedUnkn.Text = "Unknown";
			this.radioDedUnkn.Click += new System.EventHandler(this.radioDedUnkn_Click);
			// 
			// radioDedNo
			// 
			this.radioDedNo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioDedNo.Location = new System.Drawing.Point(52, -1);
			this.radioDedNo.Name = "radioDedNo";
			this.radioDedNo.Size = new System.Drawing.Size(48, 15);
			this.radioDedNo.TabIndex = 1;
			this.radioDedNo.Text = "No";
			this.radioDedNo.Click += new System.EventHandler(this.radioDedNo_Click);
			// 
			// radioDedYes
			// 
			this.radioDedYes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioDedYes.Location = new System.Drawing.Point(0, -1);
			this.radioDedYes.Name = "radioDedYes";
			this.radioDedYes.Size = new System.Drawing.Size(46, 15);
			this.radioDedYes.TabIndex = 0;
			this.radioDedYes.Text = "Yes";
			this.radioDedYes.Click += new System.EventHandler(this.radioDedYes_Click);
			// 
			// textDateEffect
			// 
			this.textDateEffect.Location = new System.Drawing.Point(109, 89);
			this.textDateEffect.Name = "textDateEffect";
			this.textDateEffect.Size = new System.Drawing.Size(72, 20);
			this.textDateEffect.TabIndex = 1;
			this.textDateEffect.Text = "";
			// 
			// textDateTerm
			// 
			this.textDateTerm.Location = new System.Drawing.Point(109, 109);
			this.textDateTerm.Name = "textDateTerm";
			this.textDateTerm.Size = new System.Drawing.Size(72, 20);
			this.textDateTerm.TabIndex = 2;
			this.textDateTerm.Text = "";
			// 
			// listSubscriber
			// 
			this.listSubscriber.Items.AddRange(new object[] {
																												""});
			this.listSubscriber.Location = new System.Drawing.Point(109, 26);
			this.listSubscriber.Name = "listSubscriber";
			this.listSubscriber.ScrollAlwaysVisible = true;
			this.listSubscriber.Size = new System.Drawing.Size(238, 43);
			this.listSubscriber.TabIndex = 1;
			this.listSubscriber.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listSubscriber_MouseDown);
			// 
			// textEmployer
			// 
			this.textEmployer.Location = new System.Drawing.Point(104, 16);
			this.textEmployer.MaxLength = 40;
			this.textEmployer.Name = "textEmployer";
			this.textEmployer.Size = new System.Drawing.Size(231, 20);
			this.textEmployer.TabIndex = 0;
			this.textEmployer.Text = "";
			this.textEmployer.Leave += new System.EventHandler(this.textEmployer_Leave);
			this.textEmployer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textEmployer_KeyUp);
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(8, 19);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(95, 15);
			this.label16.TabIndex = 73;
			this.label16.Text = "Employer";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textAddress2
			// 
			this.textAddress2.Location = new System.Drawing.Point(100, 69);
			this.textAddress2.MaxLength = 60;
			this.textAddress2.Name = "textAddress2";
			this.textAddress2.Size = new System.Drawing.Size(291, 20);
			this.textAddress2.TabIndex = 3;
			this.textAddress2.Text = "";
			this.textAddress2.TextChanged += new System.EventHandler(this.textAddress2_TextChanged);
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(3, 72);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(95, 15);
			this.label21.TabIndex = 79;
			this.label21.Text = "Address 2";
			this.label21.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// tbPercent1
			// 
			this.tbPercent1.BackColor = System.Drawing.SystemColors.Window;
			this.tbPercent1.Location = new System.Drawing.Point(109, 116);
			this.tbPercent1.Name = "tbPercent1";
			this.tbPercent1.ScrollValue = 1;
			this.tbPercent1.SelectedIndices = new int[0];
			this.tbPercent1.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbPercent1.Size = new System.Drawing.Size(242, 86);
			this.tbPercent1.TabIndex = 22;
			// 
			// textAnnualMax
			// 
			this.textAnnualMax.Location = new System.Drawing.Point(109, 18);
			this.textAnnualMax.Name = "textAnnualMax";
			this.textAnnualMax.Size = new System.Drawing.Size(60, 20);
			this.textAnnualMax.TabIndex = 0;
			this.textAnnualMax.Text = "";
			// 
			// textOrthoMax
			// 
			this.textOrthoMax.Location = new System.Drawing.Point(109, 38);
			this.textOrthoMax.Name = "textOrthoMax";
			this.textOrthoMax.Size = new System.Drawing.Size(60, 20);
			this.textOrthoMax.TabIndex = 1;
			this.textOrthoMax.Text = "";
			// 
			// textDeductible
			// 
			this.textDeductible.Location = new System.Drawing.Point(109, 58);
			this.textDeductible.Name = "textDeductible";
			this.textDeductible.Size = new System.Drawing.Size(45, 20);
			this.textDeductible.TabIndex = 2;
			this.textDeductible.Text = "";
			// 
			// textFloToAge
			// 
			this.textFloToAge.Location = new System.Drawing.Point(105, 32);
			this.textFloToAge.Name = "textFloToAge";
			this.textFloToAge.Size = new System.Drawing.Size(36, 20);
			this.textFloToAge.TabIndex = 1;
			this.textFloToAge.Text = "";
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(108, 101);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(206, 17);
			this.label22.TabIndex = 21;
			this.label22.Text = "Plan Percentages (single click to edit)";
			// 
			// listFeeSched
			// 
			this.listFeeSched.Location = new System.Drawing.Point(16, 297);
			this.listFeeSched.Name = "listFeeSched";
			this.listFeeSched.Size = new System.Drawing.Size(108, 82);
			this.listFeeSched.TabIndex = 7;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(13, 279);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(114, 15);
			this.label1.TabIndex = 91;
			this.label1.Text = "Fee Schedule";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkAssign);
			this.groupBox1.Controls.Add(this.checkRelease);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(110, 313);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 55);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Signatures on File";
			// 
			// checkAssign
			// 
			this.checkAssign.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAssign.Location = new System.Drawing.Point(12, 35);
			this.checkAssign.Name = "checkAssign";
			this.checkAssign.Size = new System.Drawing.Size(150, 16);
			this.checkAssign.TabIndex = 1;
			this.checkAssign.Text = "Assignment of Benefits";
			// 
			// checkRelease
			// 
			this.checkRelease.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkRelease.Location = new System.Drawing.Point(12, 17);
			this.checkRelease.Name = "checkRelease";
			this.checkRelease.Size = new System.Drawing.Size(140, 17);
			this.checkRelease.TabIndex = 0;
			this.checkRelease.Text = "Release of Information";
			// 
			// checkNoSendElect
			// 
			this.checkNoSendElect.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNoSendElect.Location = new System.Drawing.Point(158, 110);
			this.checkNoSendElect.Name = "checkNoSendElect";
			this.checkNoSendElect.Size = new System.Drawing.Size(234, 17);
			this.checkNoSendElect.TabIndex = 8;
			this.checkNoSendElect.Text = "Don\'t Usually Send Electronically";
			// 
			// listClaimForm
			// 
			this.listClaimForm.Location = new System.Drawing.Point(252, 297);
			this.listClaimForm.Name = "listClaimForm";
			this.listClaimForm.Size = new System.Drawing.Size(147, 82);
			this.listClaimForm.TabIndex = 9;
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(250, 279);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(121, 12);
			this.label23.TabIndex = 96;
			this.label23.Text = "Claim Form";
			// 
			// checkAlternateCode
			// 
			this.checkAlternateCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAlternateCode.Location = new System.Drawing.Point(104, 246);
			this.checkAlternateCode.Name = "checkAlternateCode";
			this.checkAlternateCode.Size = new System.Drawing.Size(286, 17);
			this.checkAlternateCode.TabIndex = 5;
			this.checkAlternateCode.Text = "Use Alternate Code (for some Medicaid plans)";
			// 
			// checkClaimsUseUCR
			// 
			this.checkClaimsUseUCR.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkClaimsUseUCR.Location = new System.Drawing.Point(104, 262);
			this.checkClaimsUseUCR.Name = "checkClaimsUseUCR";
			this.checkClaimsUseUCR.Size = new System.Drawing.Size(306, 17);
			this.checkClaimsUseUCR.TabIndex = 6;
			this.checkClaimsUseUCR.Text = "Claims show UCR fee, not billed fee";
			// 
			// labelCopayFee
			// 
			this.labelCopayFee.Location = new System.Drawing.Point(126, 279);
			this.labelCopayFee.Name = "labelCopayFee";
			this.labelCopayFee.Size = new System.Drawing.Size(121, 17);
			this.labelCopayFee.TabIndex = 100;
			this.labelCopayFee.Text = "Co-pay Fee Schedule ";
			// 
			// listCopay
			// 
			this.listCopay.Location = new System.Drawing.Point(128, 297);
			this.listCopay.Name = "listCopay";
			this.listCopay.Size = new System.Drawing.Size(108, 82);
			this.listCopay.TabIndex = 8;
			// 
			// listPlanType
			// 
			this.listPlanType.Location = new System.Drawing.Point(104, 203);
			this.listPlanType.Name = "listPlanType";
			this.listPlanType.Size = new System.Drawing.Size(143, 43);
			this.listPlanType.TabIndex = 4;
			this.listPlanType.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listPlanType_MouseDown);
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(9, 205);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(95, 15);
			this.label14.TabIndex = 104;
			this.label14.Text = "Plan Type";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butCopayNone
			// 
			this.butCopayNone.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCopayNone.Location = new System.Drawing.Point(129, 380);
			this.butCopayNone.Name = "butCopayNone";
			this.butCopayNone.Size = new System.Drawing.Size(75, 24);
			this.butCopayNone.TabIndex = 105;
			this.butCopayNone.Text = "&No CoPay";
			this.butCopayNone.Click += new System.EventHandler(this.butCopayNone_Click);
			// 
			// labelCopayAdvice
			// 
			this.labelCopayAdvice.Location = new System.Drawing.Point(209, 380);
			this.labelCopayAdvice.Name = "labelCopayAdvice";
			this.labelCopayAdvice.Size = new System.Drawing.Size(202, 39);
			this.labelCopayAdvice.TabIndex = 106;
			this.labelCopayAdvice.Text = "To indicate 100% coverage, set plan type to flat co-pay, and do not select a co-p" +
				"ay fee schedule.";
			// 
			// textSubscriber
			// 
			this.textSubscriber.Location = new System.Drawing.Point(109, 28);
			this.textSubscriber.Name = "textSubscriber";
			this.textSubscriber.Size = new System.Drawing.Size(238, 20);
			this.textSubscriber.TabIndex = 109;
			this.textSubscriber.Text = "";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textSubscriber);
			this.groupBox2.Controls.Add(this.listSubscriber);
			this.groupBox2.Controls.Add(this.checkSubOtherFam);
			this.groupBox2.Controls.Add(this.textSubscriberID);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.textDateEffect);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.textDateTerm);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(6, 6);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(413, 135);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Subscriber";
			// 
			// checkSubOtherFam
			// 
			this.checkSubOtherFam.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkSubOtherFam.Location = new System.Drawing.Point(109, 9);
			this.checkSubOtherFam.Name = "checkSubOtherFam";
			this.checkSubOtherFam.Size = new System.Drawing.Size(192, 17);
			this.checkSubOtherFam.TabIndex = 0;
			this.checkSubOtherFam.TabStop = false;
			this.checkSubOtherFam.Text = "Is from another family";
			this.checkSubOtherFam.Click += new System.EventHandler(this.checkSubOtherFam_Click);
			// 
			// textSubscriberID
			// 
			this.textSubscriberID.Location = new System.Drawing.Point(109, 69);
			this.textSubscriberID.MaxLength = 20;
			this.textSubscriberID.Name = "textSubscriberID";
			this.textSubscriberID.Size = new System.Drawing.Size(129, 20);
			this.textSubscriberID.TabIndex = 0;
			this.textSubscriberID.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 71);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(99, 15);
			this.label2.TabIndex = 114;
			this.label2.Text = "Subscriber ID";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(5, 639);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(81, 26);
			this.butDelete.TabIndex = 112;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// textRenewMonth
			// 
			this.textRenewMonth.Location = new System.Drawing.Point(109, 78);
			this.textRenewMonth.MinVal = 1;
			this.textRenewMonth.Name = "textRenewMonth";
			this.textRenewMonth.Size = new System.Drawing.Size(99, 20);
			this.textRenewMonth.TabIndex = 3;
			this.textRenewMonth.Text = "";
			// 
			// butChangeEmp
			// 
			this.butChangeEmp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butChangeEmp.Location = new System.Drawing.Point(37, 11);
			this.butChangeEmp.Name = "butChangeEmp";
			this.butChangeEmp.Size = new System.Drawing.Size(75, 22);
			this.butChangeEmp.TabIndex = 116;
			this.butChangeEmp.Text = "Change";
			this.butChangeEmp.Visible = false;
			// 
			// groupSynch
			// 
			this.groupSynch.Controls.Add(this.butSelect);
			this.groupSynch.Controls.Add(this.butEditTemplate);
			this.groupSynch.Controls.Add(this.comboLinked);
			this.groupSynch.Controls.Add(this.textLinkedNum);
			this.groupSynch.Controls.Add(this.label4);
			this.groupSynch.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupSynch.Location = new System.Drawing.Point(457, 6);
			this.groupSynch.Name = "groupSynch";
			this.groupSynch.Size = new System.Drawing.Size(468, 92);
			this.groupSynch.TabIndex = 117;
			this.groupSynch.TabStop = false;
			this.groupSynch.Text = "Synchronization";
			// 
			// butSelect
			// 
			this.butSelect.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butSelect.Location = new System.Drawing.Point(214, 53);
			this.butSelect.Name = "butSelect";
			this.butSelect.Size = new System.Drawing.Size(91, 23);
			this.butSelect.TabIndex = 72;
			this.butSelect.Text = "Select New";
			this.toolTip1.SetToolTip(this.butSelect, "Select a plan from the main list");
			this.butSelect.Click += new System.EventHandler(this.butSelect_Click);
			// 
			// butEditTemplate
			// 
			this.butEditTemplate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butEditTemplate.Location = new System.Drawing.Point(358, 53);
			this.butEditTemplate.Name = "butEditTemplate";
			this.butEditTemplate.Size = new System.Drawing.Size(90, 23);
			this.butEditTemplate.TabIndex = 71;
			this.butEditTemplate.Text = "Edit All";
			this.toolTip1.SetToolTip(this.butEditTemplate, "Edit all the similar plans at once");
			this.butEditTemplate.Click += new System.EventHandler(this.butEditTemplate_Click);
			// 
			// comboLinked
			// 
			this.comboLinked.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboLinked.Location = new System.Drawing.Point(253, 19);
			this.comboLinked.MaxDropDownItems = 30;
			this.comboLinked.Name = "comboLinked";
			this.comboLinked.Size = new System.Drawing.Size(197, 21);
			this.comboLinked.TabIndex = 68;
			// 
			// textLinkedNum
			// 
			this.textLinkedNum.BackColor = System.Drawing.Color.White;
			this.textLinkedNum.Location = new System.Drawing.Point(214, 19);
			this.textLinkedNum.Name = "textLinkedNum";
			this.textLinkedNum.ReadOnly = true;
			this.textLinkedNum.Size = new System.Drawing.Size(35, 20);
			this.textLinkedNum.TabIndex = 67;
			this.textLinkedNum.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(9, 21);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(202, 17);
			this.label4.TabIndex = 66;
			this.label4.Text = "These plans are the same";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butChangeLink
			// 
			this.butChangeLink.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butChangeLink.Location = new System.Drawing.Point(156, 51);
			this.butChangeLink.Name = "butChangeLink";
			this.butChangeLink.Size = new System.Drawing.Size(90, 23);
			this.butChangeLink.TabIndex = 70;
			this.butChangeLink.Text = "Change Link";
			this.toolTip1.SetToolTip(this.butChangeLink, "Link this plan to a different template");
			// 
			// butDetach
			// 
			this.butDetach.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butDetach.Location = new System.Drawing.Point(172, 23);
			this.butDetach.Name = "butDetach";
			this.butDetach.Size = new System.Drawing.Size(85, 23);
			this.butDetach.TabIndex = 69;
			this.butDetach.Text = "Detach";
			this.toolTip1.SetToolTip(this.butDetach, "Detach this plan so that it will not be synchronized with any others");
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.textGroupNum);
			this.groupBox4.Controls.Add(this.groupBox3);
			this.groupBox4.Controls.Add(this.textEmployer);
			this.groupBox4.Controls.Add(this.label16);
			this.groupBox4.Controls.Add(this.textGroupName);
			this.groupBox4.Controls.Add(this.label8);
			this.groupBox4.Controls.Add(this.label9);
			this.groupBox4.Controls.Add(this.listFeeSched);
			this.groupBox4.Controls.Add(this.label1);
			this.groupBox4.Controls.Add(this.listClaimForm);
			this.groupBox4.Controls.Add(this.label23);
			this.groupBox4.Controls.Add(this.checkClaimsUseUCR);
			this.groupBox4.Controls.Add(this.labelCopayFee);
			this.groupBox4.Controls.Add(this.listCopay);
			this.groupBox4.Controls.Add(this.listPlanType);
			this.groupBox4.Controls.Add(this.label14);
			this.groupBox4.Controls.Add(this.butCopayNone);
			this.groupBox4.Controls.Add(this.labelCopayAdvice);
			this.groupBox4.Controls.Add(this.checkAlternateCode);
			this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox4.Location = new System.Drawing.Point(6, 145);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(413, 449);
			this.groupBox4.TabIndex = 1;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Synchronized Information";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.textAddress);
			this.groupBox3.Controls.Add(this.textCity);
			this.groupBox3.Controls.Add(this.textState);
			this.groupBox3.Controls.Add(this.label21);
			this.groupBox3.Controls.Add(this.textCarrier);
			this.groupBox3.Controls.Add(this.label10);
			this.groupBox3.Controls.Add(this.textAddress2);
			this.groupBox3.Controls.Add(this.textElectID);
			this.groupBox3.Controls.Add(this.textZip);
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Controls.Add(this.labelCitySTZip);
			this.groupBox3.Controls.Add(this.label15);
			this.groupBox3.Controls.Add(this.checkNoSendElect);
			this.groupBox3.Controls.Add(this.textPhone);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(4, 71);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(397, 131);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Carrier";
			// 
			// butEdit
			// 
			this.butEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butEdit.Location = new System.Drawing.Point(36, 63);
			this.butEdit.Name = "butEdit";
			this.butEdit.Size = new System.Drawing.Size(75, 22);
			this.butEdit.TabIndex = 118;
			this.butEdit.Text = "Edit";
			this.butEdit.Visible = false;
			// 
			// butChangeCarrier
			// 
			this.butChangeCarrier.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butChangeCarrier.Location = new System.Drawing.Point(36, 41);
			this.butChangeCarrier.Name = "butChangeCarrier";
			this.butChangeCarrier.Size = new System.Drawing.Size(75, 22);
			this.butChangeCarrier.TabIndex = 117;
			this.butChangeCarrier.Text = "Change";
			this.butChangeCarrier.Visible = false;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.groupBox1);
			this.groupBox5.Controls.Add(this.tbPercent1);
			this.groupBox5.Controls.Add(this.panelAdvancedIns);
			this.groupBox5.Controls.Add(this.textAnnualMax);
			this.groupBox5.Controls.Add(this.textOrthoMax);
			this.groupBox5.Controls.Add(this.textDeductible);
			this.groupBox5.Controls.Add(this.label22);
			this.groupBox5.Controls.Add(this.textPlanNote);
			this.groupBox5.Controls.Add(this.label28);
			this.groupBox5.Controls.Add(this.textRenewMonth);
			this.groupBox5.Controls.Add(this.label17);
			this.groupBox5.Controls.Add(this.label18);
			this.groupBox5.Controls.Add(this.label19);
			this.groupBox5.Controls.Add(this.labelOrthoMax);
			this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox5.Location = new System.Drawing.Point(457, 100);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(469, 474);
			this.groupBox5.TabIndex = 2;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Coverage Information";
			// 
			// panelAdvancedIns
			// 
			this.panelAdvancedIns.Controls.Add(this.panel4);
			this.panelAdvancedIns.Controls.Add(this.panel3);
			this.panelAdvancedIns.Controls.Add(this.label27);
			this.panelAdvancedIns.Controls.Add(this.label30);
			this.panelAdvancedIns.Controls.Add(this.panel2);
			this.panelAdvancedIns.Controls.Add(this.textFloToAge);
			this.panelAdvancedIns.Controls.Add(this.label20);
			this.panelAdvancedIns.Controls.Add(this.label29);
			this.panelAdvancedIns.Location = new System.Drawing.Point(6, 201);
			this.panelAdvancedIns.Name = "panelAdvancedIns";
			this.panelAdvancedIns.Size = new System.Drawing.Size(335, 112);
			this.panelAdvancedIns.TabIndex = 4;
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.butChangeEmp);
			this.groupBox6.Controls.Add(this.butEdit);
			this.groupBox6.Controls.Add(this.butChangeCarrier);
			this.groupBox6.Controls.Add(this.butDetach);
			this.groupBox6.Controls.Add(this.butChangeLink);
			this.groupBox6.Location = new System.Drawing.Point(460, 587);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(266, 84);
			this.groupBox6.TabIndex = 120;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Not visible";
			this.groupBox6.Visible = false;
			// 
			// labelDrop
			// 
			this.labelDrop.Location = new System.Drawing.Point(185, 615);
			this.labelDrop.Name = "labelDrop";
			this.labelDrop.Size = new System.Drawing.Size(210, 51);
			this.labelDrop.TabIndex = 124;
			this.labelDrop.Text = "Drop a plan when a patient changes carriers or is no longer covered.  This does n" +
				"ot delete the plan.";
			this.labelDrop.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.labelDrop.Visible = false;
			// 
			// butDrop
			// 
			this.butDrop.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butDrop.Location = new System.Drawing.Point(100, 639);
			this.butDrop.Name = "butDrop";
			this.butDrop.Size = new System.Drawing.Size(75, 26);
			this.butDrop.TabIndex = 123;
			this.butDrop.Text = "Drop";
			this.butDrop.Visible = false;
			this.butDrop.Click += new System.EventHandler(this.butDrop_Click);
			// 
			// FormInsPlan
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(961, 682);
			this.Controls.Add(this.labelDrop);
			this.Controls.Add(this.butDrop);
			this.Controls.Add(this.groupBox6);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.groupSynch);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormInsPlan";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Insurance Plan";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormInsPlan_Closing);
			this.Load += new System.EventHandler(this.FormInsPlan_Load);
			this.panel4.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupSynch.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.panelAdvancedIns.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormInsPlan_Load(object sender, System.EventArgs e) {
			//if(InsPlans.Cur.TemplateNum>0){
			//	InsTemplates.Refresh(InsPlans.Cur.TemplateNum);
			//}
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
				InsPlans.Cur.PlanType="";//insurance
      }
			if(DropButVisible){
				butDrop.Visible=true;
				labelDrop.Visible=true;
			}
			if(((Pref)Prefs.HList["EasyHideCapitation"]).ValueString=="1"){
				labelCopayFee.Visible=false;
				listCopay.Visible=false;
				butCopayNone.Visible=false;
				labelCopayAdvice.Visible=false;
			}
			if(((Pref)Prefs.HList["EasyHideMedicaid"]).ValueString=="1"){
				checkAlternateCode.Visible=false;
			}
			if(((Pref)Prefs.HList["EasyHideAdvancedIns"]).ValueString=="1"){
				textOrthoMax.Visible=false;
				labelOrthoMax.Visible=false;
				panelAdvancedIns.Visible=false;
				groupSynch.Visible=false;
			}
			FillFormData();
		}

		private void FillFormData(){
			FillSubscriber();
			textSubscriberID.Text=InsPlans.Cur.SubscriberID;
			if(InsPlans.Cur.DateEffective.Year < 1880)
				textDateEffect.Text="";
			else
				textDateEffect.Text=InsPlans.Cur.DateEffective.ToString("d");
			if(InsPlans.Cur.DateTerm.Year < 1880)
				textDateTerm.Text="";
			else
				textDateTerm.Text=InsPlans.Cur.DateTerm.ToString("d");
			textEmployer.Text=Employers.GetName(InsPlans.Cur.EmployerNum);
			textGroupName.Text=InsPlans.Cur.GroupName;
			textGroupNum.Text=InsPlans.Cur.GroupNum;
			listPlanType.Items.Clear();
			listPlanType.Items.Add(Lan.g(this,"Category Percentage"));
			if(InsPlans.Cur.PlanType=="")
				listPlanType.SelectedIndex=0;
			listPlanType.Items.Add(Lan.g(this,"Flat Co-pay"));
			if(InsPlans.Cur.PlanType=="f")
				listPlanType.SelectedIndex=1;
			if(((Pref)Prefs.HList["EasyHideCapitation"]).ValueString!="1"){
				listPlanType.Items.Add(Lan.g(this,"Capitation"));
				if(InsPlans.Cur.PlanType=="c")
					listPlanType.SelectedIndex=2;
			}
			checkAlternateCode.Checked=InsPlans.Cur.UseAltCode;
			checkClaimsUseUCR.Checked=InsPlans.Cur.ClaimsUseUCR;
			listFeeSched.Items.Clear();
			for(int i=0;i<Defs.Short[(int)DefCat.FeeSchedNames].Length;i++){
				listFeeSched.Items.Add(Defs.Short[(int)DefCat.FeeSchedNames][i].ItemName);
				if(Defs.Short[(int)DefCat.FeeSchedNames][i].DefNum==InsPlans.Cur.FeeSched)
					listFeeSched.SelectedIndex=i;
			}
			listCopay.Items.Clear();
			for(int i=0;i<Defs.Short[(int)DefCat.FeeSchedNames].Length;i++){
				listCopay.Items.Add(Defs.Short[(int)DefCat.FeeSchedNames][i].ItemName);
				if(Defs.Short[(int)DefCat.FeeSchedNames][i].DefNum==InsPlans.Cur.CopayFeeSched)
					listCopay.SelectedIndex=i;
			}
			listClaimForm.Items.Clear();
			for(int i=0;i<ClaimForms.ListShort.Length;i++){
				listClaimForm.Items.Add(ClaimForms.ListShort[i].Description);
				if(ClaimForms.ListShort[i].ClaimFormNum==InsPlans.Cur.ClaimFormNum){
					listClaimForm.SelectedIndex=i;
				}
			}
			if(listClaimForm.Items.Count>0 && listClaimForm.SelectedIndex==-1){
				listClaimForm.SelectedIndex=0;//this will let the user rearrange the default later
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
			FillCarrier();
			FillPercentages();
			LayoutSynch();
		}

		private void FillCarrier(){
			Carriers.GetCur(InsPlans.Cur.CarrierNum);
			textCarrier.Text=Carriers.Cur.CarrierName;
			textPhone.Text=Carriers.Cur.Phone;
			textAddress.Text=Carriers.Cur.Address;
			textAddress2.Text=Carriers.Cur.Address2;
			textCity.Text=Carriers.Cur.City;
			textState.Text=Carriers.Cur.State;
			textZip.Text=Carriers.Cur.Zip;
			textElectID.Text=Carriers.Cur.ElectID;
			checkNoSendElect.Checked=Carriers.Cur.NoSendElect;
			//MessageBox.Show(Carriers.Cur.CarrierNum.ToString());
		}

		private void FillPercentages(){
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

		private void FillSubscriber(){
			listSubscriber.Items.Clear();
			for(int i=0;i<Patients.FamilyList.Length;i++){
				listSubscriber.Items.Add(Patients.GetNameInFamLFI(i));
				if(InsPlans.Cur.Subscriber==Patients.FamilyList[i].PatNum){
					listSubscriber.SelectedIndex=i;
				}
			}
			if(InsPlans.Cur.Subscriber==0){
				listSubscriber.SelectedIndex=Patients.GetIndex(Patients.Cur.PatNum);
				//the initial subscriber will be the current patient
				InsPlans.Cur.SubscriberID=Patients.Cur.SSN;
			}
			if(listSubscriber.SelectedIndex==-1){//subscriber not in family
				checkSubOtherFam.Checked=true;
				textSubscriber.Visible=true;
				listSubscriber.Visible=false;
				Patients.GetLim(InsPlans.Cur.Subscriber);
				textSubscriber.Text=Patients.LimName;
			}
			else{//show the family list that was just filled
				checkSubOtherFam.Checked=false;
				textSubscriber.Visible=false;
				listSubscriber.Visible=true;
			}
		}

		private void checkSubOtherFam_Click(object sender, System.EventArgs e) {
			//this happens after the check change has been registered
			if(checkSubOtherFam.Checked){
				int curPatNum=Patients.Cur.PatNum;
				FormPatientSelect FormPS=new FormPatientSelect();
				FormPS.SelectionModeOnly=true;//this will cause a change in the patNum only
				FormPS.ShowDialog();
				if(FormPS.DialogResult!=DialogResult.OK){
					checkSubOtherFam.Checked=false;
					return;
				}
				InsPlans.Cur.Subscriber=Patients.Cur.PatNum;
				Patients.GetLim(Patients.Cur.PatNum);
				InsPlans.Cur.SubscriberID=Patients.Lim.SSN;
				Patient PatCur=Patients.Cur;
				PatCur.PatNum=curPatNum;//this preserves the current PatNum
				Patients.Cur=PatCur;
			}
			else{//switch to family view
				InsPlans.Cur.Subscriber=0;//this will reset the subscriber and ID to current patient
			}
			FillSubscriber();
			textSubscriberID.Text=InsPlans.Cur.SubscriberID;//because it will usually have changed.
		}

		private void listSubscriber_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listSubscriber.SelectedIndex==-1){
				return;
			}
			InsPlans.Cur.Subscriber=Patients.FamilyList[listSubscriber.SelectedIndex].PatNum;
			InsPlans.Cur.SubscriberID=Patients.FamilyList[listSubscriber.SelectedIndex].SSN;
			textSubscriberID.Text=InsPlans.Cur.SubscriberID;
		}

		/*
		private void butChangeEmp_Click(object sender, System.EventArgs e) {
			FormEmployers FormE=new FormEmployers();
			FormE.IsSelectMode=true;
			Employers.Cur=Employers.GetEmployer(InsPlans.Cur.EmployerNum);
			FormE.ShowDialog();
			if(FormE.DialogResult!=DialogResult.OK){
				return;
			}
			InsPlans.Cur.EmployerNum=Employers.Cur.EmployerNum;
			textEmployer.Text=Employers.Cur.EmpName;
		}*/

		/*
		private void butChangeCarrier_Click(object sender, System.EventArgs e) {
			FormCarriers FormC=new FormCarriers();
			FormC.IsSelectMode=true;
			FormC.ShowDialog();
			if(FormC.DialogResult!=DialogResult.OK){
				//this changes the carrier.cur back to that of the insplan
				//in case a different carrier was edited in the formCarriers.
				Carriers.GetCur(InsPlans.Cur.CarrierNum);
				return;
			}
			InsPlans.Cur.CarrierNum=Carriers.Cur.CarrierNum;
			FillCarrier();
		}*/

		/*
		private void butEdit_Click(object sender, System.EventArgs e) {
			FormCarrierEdit FormCE=new FormCarrierEdit();
			FormCE.ShowDialog();
			if(FormCE.DialogResult!=DialogResult.OK){
				return;
			}
			//update carriers table on all computers:
			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
			FillCarrier();
		}*/

		private void listPlanType_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//MessageBox.Show(InsPlans.Cur.PlanType+","+listPlanType.SelectedIndex.ToString());
			if(InsPlans.Cur.PlanType=="" && (
				listPlanType.SelectedIndex==1
				|| listPlanType.SelectedIndex==2))
			{
				if(MessageBox.Show(Lan.g(this,"This will clear all percentages. Continue?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
					listPlanType.SelectedIndex=0;
					return;
				}
				for(int i=0;i<CovPats.ListForPlan.Length;i++){
					CovPats.Cur=CovPats.ListForPlan[i];
					CovPats.DeleteCur();
				}
				FillPercentages();
			}
			switch(listPlanType.SelectedIndex){
				case 0:
					InsPlans.Cur.PlanType="";
					break;
				case 1:
					InsPlans.Cur.PlanType="f";
					break;
				case 2:
					InsPlans.Cur.PlanType="c";
					break;
			}
			
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
			FillPercentages();
		}

		private void butCopayNone_Click(object sender, System.EventArgs e) {
			listCopay.SelectedIndex=-1;
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

		private void textEmployer_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			//key up is used because that way it will trigger AFTER the textBox has been changed.
			if(e.KeyCode==Keys.Return){
				listEmps.Visible=false;
				textGroupName.Focus();
				return;
			}
			if(textEmployer.Text==""){
				listEmps.Visible=false;
				return;
			}
			if(e.KeyCode==Keys.Down){
				if(listEmps.Items.Count==0){
					return;
				}
				if(listEmps.SelectedIndex==-1){
					listEmps.SelectedIndex=0;
					textEmployer.Text=listEmps.SelectedItem.ToString();
				}
				else if(listEmps.SelectedIndex==listEmps.Items.Count-1){
					listEmps.SelectedIndex=-1;
					textEmployer.Text=empOriginal;
				}
				else{
					listEmps.SelectedIndex++;
					textEmployer.Text=listEmps.SelectedItem.ToString();
				}
				textEmployer.SelectionStart=textEmployer.Text.Length;
				return;
			}
			if(e.KeyCode==Keys.Up){
				if(listEmps.Items.Count==0){
					return;
				}
				if(listEmps.SelectedIndex==-1){
					listEmps.SelectedIndex=listEmps.Items.Count-1;
					textEmployer.Text=listEmps.SelectedItem.ToString();
				}
				else if(listEmps.SelectedIndex==0){
					listEmps.SelectedIndex=-1;
					textEmployer.Text=empOriginal;
				}
				else{
					listEmps.SelectedIndex--;
					textEmployer.Text=listEmps.SelectedItem.ToString();
				}
				textEmployer.SelectionStart=textEmployer.Text.Length;
				return;
			}
			if(textEmployer.Text.Length==1){
				textEmployer.Text=textEmployer.Text.ToUpper();
				textEmployer.SelectionStart=1;
			}
			empOriginal=textEmployer.Text;//the original text is preserved when using up and down arrows
			listEmps.Items.Clear();
			similarEmps=Employers.GetSimilarNames(textEmployer.Text);
			for(int i=0;i<similarEmps.Count;i++){
				listEmps.Items.Add(((Employer)similarEmps[i]).EmpName);
			}
			int h=13*similarEmps.Count+5;
			if(h > ClientSize.Height-listEmps.Top)
				h=ClientSize.Height-listEmps.Top;
			listEmps.Size=new Size(231,h);
			listEmps.Visible=true;
		}

		private void textEmployer_Leave(object sender, System.EventArgs e) {
			if(mouseIsInListEmps){
				return;
			}
			listEmps.Visible=false;
		}

		private void listEmps_Click(object sender, System.EventArgs e){
			textEmployer.Text=listEmps.SelectedItem.ToString();
			textEmployer.Focus();
			textEmployer.SelectionStart=textEmployer.Text.Length;
			listEmps.Visible=false;
		}

		private void listEmps_DoubleClick(object sender, System.EventArgs e){
			//no longer used
			textEmployer.Text=listEmps.SelectedItem.ToString();
			textEmployer.Focus();
			textEmployer.SelectionStart=textEmployer.Text.Length;
			listEmps.Visible=false;
		}

		private void listEmps_MouseEnter(object sender, System.EventArgs e){
			mouseIsInListEmps=true;
		}

		private void listEmps_MouseLeave(object sender, System.EventArgs e){
			mouseIsInListEmps=false;
		}

		private void textCarrier_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			if(e.KeyCode==Keys.Return){
				if(listCars.SelectedIndex==-1){
					textPhone.Focus();
				}
				else{
					InsPlans.Cur.CarrierNum=((Carrier)similarCars[listCars.SelectedIndex]).CarrierNum;
					FillCarrier();
					textCarrier.Focus();
					textCarrier.SelectionStart=textCarrier.Text.Length;
				}
				listCars.Visible=false;
				return;
			}
			if(textCarrier.Text==""){
				listCars.Visible=false;
				return;
			}
			if(e.KeyCode==Keys.Down){
				if(listCars.Items.Count==0){
					return;
				}
				if(listCars.SelectedIndex==-1){
					listCars.SelectedIndex=0;
					textCarrier.Text=listCars.SelectedItem.ToString();
				}
				else if(listCars.SelectedIndex==listCars.Items.Count-1){
					listCars.SelectedIndex=-1;
					textCarrier.Text=carOriginal;
				}
				else{
					listCars.SelectedIndex++;
					textCarrier.Text=listCars.SelectedItem.ToString();
				}
				textCarrier.SelectionStart=textCarrier.Text.Length;
				return;
			}
			if(e.KeyCode==Keys.Up){
				if(listCars.Items.Count==0){
					return;
				}
				if(listCars.SelectedIndex==-1){
					listCars.SelectedIndex=listCars.Items.Count-1;
					textCarrier.Text=listCars.SelectedItem.ToString();
				}
				else if(listCars.SelectedIndex==0){
					listCars.SelectedIndex=-1;
					textCarrier.Text=carOriginal;
				}
				else{
					listCars.SelectedIndex--;
					textCarrier.Text=listCars.SelectedItem.ToString();
				}
				textCarrier.SelectionStart=textCarrier.Text.Length;
				return;
			}
			if(textCarrier.Text.Length==1){
				textCarrier.Text=textCarrier.Text.ToUpper();
				textCarrier.SelectionStart=1;
			}
			carOriginal=textCarrier.Text;//the original text is preserved when using up and down arrows
			listCars.Items.Clear();
			similarCars=Carriers.GetSimilarNames(textCarrier.Text);
			for(int i=0;i<similarCars.Count;i++){
				listCars.Items.Add(((Carrier)similarCars[i]).CarrierName);
			}
			int h=13*similarCars.Count+5;
			if(h > ClientSize.Height-listCars.Top)
				h=ClientSize.Height-listCars.Top;
			listCars.Size=new Size(291,h);
			listCars.Visible=true;
		}

		private void textCarrier_Leave(object sender, System.EventArgs e) {
			if(mouseIsInListCars){
				return;
			}
			//or if user clicked on a different text box.
			if(listCars.SelectedIndex!=-1){
				InsPlans.Cur.CarrierNum=((Carrier)similarCars[listCars.SelectedIndex]).CarrierNum;
				FillCarrier();
			}
			listCars.Visible=false;
		}

		private void listCars_Click(object sender, System.EventArgs e){
			InsPlans.Cur.CarrierNum=((Carrier)similarCars[listCars.SelectedIndex]).CarrierNum;
			FillCarrier();
			textCarrier.Focus();
			textCarrier.SelectionStart=textCarrier.Text.Length;
			listCars.Visible=false;
			//textCarrier.Text=listCars.SelectedItem.ToString();
			//textCarrier.Focus();
			//textCarrier.SelectionStart=textCarrier.Text.Length;
		}

		private void listCars_DoubleClick(object sender, System.EventArgs e){
			//no longer used
			InsPlans.Cur.CarrierNum=((Carrier)similarCars[listCars.SelectedIndex]).CarrierNum;
			FillCarrier();
			textCarrier.Focus();
			textCarrier.SelectionStart=textCarrier.Text.Length;
			listCars.Visible=false;
		}

		private void listCars_MouseEnter(object sender, System.EventArgs e){
			mouseIsInListCars=true;
		}

		private void listCars_MouseLeave(object sender, System.EventArgs e){
			mouseIsInListCars=false;
		}

		private void LayoutSynch(){
			string[] samePlans=InsPlans.SamePlans();
			textLinkedNum.Text=samePlans.Length.ToString();
			comboLinked.Items.Clear();
			for(int i=0;i<samePlans.Length;i++){
				comboLinked.Items.Add(samePlans[i]);
			}
			if(samePlans.Length>0)
				comboLinked.SelectedIndex=0;
		}

		private void textPhone_TextChanged(object sender, System.EventArgs e) {
			int cursor=textPhone.SelectionStart;
			int length=textPhone.Text.Length;
			textPhone.Text=TelephoneNumbers.AutoFormat(textPhone.Text);
			if(textPhone.Text.Length>length)
				cursor++;
			textPhone.SelectionStart=cursor;		
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

		private void butSelect_Click(object sender, System.EventArgs e) {
			if(!FillCur())
				return;
			FormInsPlans FormIP=new FormInsPlans();
			FormIP.IsSelectMode=true;
			FormIP.ShowDialog();
			if(FormIP.DialogResult!=DialogResult.OK){
				return;
			}
			InsPlans.UpdateCur();//updates to the db so that the synch info will show correctly
			LayoutSynch();
			FillFormData();
		}

		private void butEditTemplate_Click(object sender, System.EventArgs e) {
			//notice that this is called without saving any changes that have been made so far.
			int curPlanNum=InsPlans.Cur.PlanNum;
			FormInsPlanEditAll FormIPE=new FormInsPlanEditAll();
			FormIPE.ShowDialog();
			//if(FormIPE.DialogResult!=DialogResult.OK){
			//	return;
			//}
			InsPlans.Refresh(curPlanNum);
			FillFormData();
		}

#region Old Template Stuff
		

		/*
		private void butDetach_Click(object sender, System.EventArgs e) {
			if(comboLinked.Items.Count==1){
				MessageBox.Show(Lan.g(this,"This plan is already detached from all other plans."));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Detach this plan from all links with other plans?")
				,"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
				return;
			if(!FillCur()){
				return;
			}
			InsTemplates.Cur=new InsTemplate();
			FillSynchTemplate();
			InsTemplates.InsertCur();
			InsPlans.Cur.TemplateNum=InsTemplates.Cur.TemplateNum;
			InsPlans.UpdateCur();//updates to the db so that the synch info will show correctly
			LayoutSynch();
		}*/

		/*
		private void butChangeLink_Click(object sender, System.EventArgs e) {
			FormInsTemplates FormIT=new FormInsTemplates();
			FormIT.IsLinkMode=true;
			FormIT.ShowDialog();
			if(FormIT.DialogResult!=DialogResult.OK){
				return;
			}
			//if this was the only plan using the previous template
			//then this is in essence a combine action, and the previous template needs to be deleted.
			if(textLinkedNum.Text=="1"
				&& InsPlans.Cur.TemplateNum!=InsTemplates.Cur.TemplateNum){//unless they switched to the same one
				InsTemplates.Delete(InsPlans.Cur.TemplateNum);
			}
			GetSynchTemplate();
			InsPlans.Cur.TemplateNum=InsTemplates.Cur.TemplateNum;
			InsPlans.UpdateCur();//updates to the db so that the synch info will show correctly
			LayoutSynch();
		}*/

		

		/*
		///<summary>Only called from butChangeLink.  Displays the information for InsTemplates.Cur on this form.</summary>
		private void GetSynchTemplate(){
			//this whole process would be slightly more elegant if it only set the insplans.cur,
			//and a separate process then filled the elements of the UI.
			InsPlans.Cur.CarrierNum=InsTemplates.Cur.CarrierNum;
			FillCarrier();
			textGroupName.Text=InsTemplates.Cur.GroupName;
			textGroupNum.Text=InsTemplates.Cur.GroupNum;
			InsPlans.Cur.EmployerNum=InsTemplates.Cur.EmployerNum;
			textEmployer.Text=Employers.GetName(InsPlans.Cur.EmployerNum);
			switch(InsTemplates.Cur.PlanType){
				default: listPlanType.SelectedIndex=0; break;
				case "f": listPlanType.SelectedIndex=1; break;
				case "c": listPlanType.SelectedIndex=2; break;
			}
			checkAlternateCode.Checked=InsTemplates.Cur.UseAltCode;
			checkClaimsUseUCR.Checked=InsTemplates.Cur.ClaimsUseUCR;
			for(int i=0;i<Defs.Short[(int)DefCat.FeeSchedNames].Length;i++){
				if(Defs.Short[(int)DefCat.FeeSchedNames][i].DefNum==InsTemplates.Cur.FeeSched)
					listFeeSched.SelectedIndex=i;
			}
			for(int i=0;i<Defs.Short[(int)DefCat.FeeSchedNames].Length;i++){
				if(Defs.Short[(int)DefCat.FeeSchedNames][i].DefNum==InsTemplates.Cur.CopayFeeSched)
					listCopay.SelectedIndex=i;
			}
			for(int i=0;i<ClaimForms.ListShort.Length;i++){
				if(ClaimForms.ListShort[i].ClaimFormNum==InsTemplates.Cur.ClaimFormNum){
					listClaimForm.SelectedIndex=i;
				}
			}
			if(listClaimForm.SelectedIndex==-1){
				listClaimForm.SelectedIndex=0;//this will let the user rearrange the default later
			}
		}*/

		/*
		Templates are no longer able to be affected from editing an insplan.  You have to explicitly edit the template.
		/// <summary>Fills the current template with data from the current plan.  Gets run just before form closes.  Also gets run when detaching.</summary>
		private void FillSynchTemplate(){
			InsTemplates.Cur.CarrierNum= InsPlans.Cur.CarrierNum;
			InsTemplates.Cur.GroupName= InsPlans.Cur.GroupName;
			InsTemplates.Cur.GroupNum= InsPlans.Cur.GroupNum;
			InsTemplates.Cur.PlanType=InsPlans.Cur.PlanType;
			InsTemplates.Cur.UseAltCode= InsPlans.Cur.UseAltCode;
			InsTemplates.Cur.ClaimsUseUCR= InsPlans.Cur.ClaimsUseUCR;
			InsTemplates.Cur.EmployerNum=InsPlans.Cur.EmployerNum;
			InsTemplates.Cur.FeeSched =	InsPlans.Cur.FeeSched;
			InsTemplates.Cur.CopayFeeSched =	InsPlans.Cur.CopayFeeSched;
			InsTemplates.Cur.ClaimFormNum =InsPlans.Cur.ClaimFormNum;
		}*/

#endregion

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"Delete Plan?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
				return;
			if(!InsPlans.DeleteCur()){//checks dependencies first
				return;
			}
			InsPlans.Cur=new InsPlan();//this sets the PlanNum to 0
			DialogResult=DialogResult.OK;
		}

		private void butDrop_Click(object sender, System.EventArgs e) {
			Patient PatCur=Patients.Cur;
			//if dropping primary ins
			if(Patients.Cur.PriPlanNum==InsPlans.Cur.PlanNum){
				if(Patients.Cur.SecPlanNum!=0){//if patient has secondary ins.
					PatCur.PriPlanNum=Patients.Cur.SecPlanNum;
					PatCur.PriRelationship=Patients.Cur.SecRelationship;
					PatCur.SecPlanNum=0;
					PatCur.SecRelationship=Relat.Self;
				}
				else{//patient does not have secondary
					PatCur.PriPlanNum=0;
					PatCur.PriRelationship=Relat.Self;
				}
				Patients.Cur=PatCur;
				Patients.UpdateCur();
			}
			//if dropping sec ins.
			else if(Patients.Cur.SecPlanNum==InsPlans.Cur.PlanNum){
				PatCur.SecPlanNum=0;
				PatCur.SecRelationship=Relat.Self;
				Patients.Cur=PatCur;
				Patients.UpdateCur();
			}
			else{
				MessageBox.Show("Error. Current patient is not covered by this plan.");
				return;
			}
			//remember to refresh after closing this form!!!
			DialogResult=DialogResult.OK;
		}

		///<summary>Gets an employerNum based on the name entered. Called from FillCur</summary>
		private void GetEmployerNum(){
			if(InsPlans.Cur.EmployerNum==0){//no employer was previously entered.
				if(textEmployer.Text==""){
					//no change
				}
				else{
					InsPlans.Cur.EmployerNum=Employers.GetEmployerNum(textEmployer.Text);
				}
			}
			else{//an employer was previously entered
				if(textEmployer.Text==""){
					InsPlans.Cur.EmployerNum=0;
				}
				//if text has changed
				else if(Employers.GetName(InsPlans.Cur.EmployerNum)!=textEmployer.Text){
					InsPlans.Cur.EmployerNum=Employers.GetEmployerNum(textEmployer.Text);
				}
			}
		}

		///<summary>Gets a carrierNum based on the data entered. Called from FillCur</summary>
		private void GetCarrierNum(){
			Carriers.Cur=new Carrier();
			Carriers.Cur.CarrierName=textCarrier.Text;
			Carriers.Cur.Phone=textPhone.Text;
			Carriers.Cur.Address=textAddress.Text;
			Carriers.Cur.Address2=textAddress2.Text;
			Carriers.Cur.City=textCity.Text;
			Carriers.Cur.State=textState.Text;
			Carriers.Cur.Zip=textZip.Text;
			Carriers.Cur.ElectID=textElectID.Text;
			Carriers.Cur.NoSendElect=checkNoSendElect.Checked;
			Carriers.GetCurSame();
			InsPlans.Cur.CarrierNum=Carriers.Cur.CarrierNum;
		}

		///<summary>Fills the current insplan with data from the form.  Validates first. Only called on butOK click.</summary>
		private bool FillCur(){
			if(textDateEffect.errorProvider1.GetError(textDateEffect)!=""
				|| textDateTerm.errorProvider1.GetError(textDateTerm)!=""
				|| textAnnualMax.errorProvider1.GetError(textAnnualMax)!=""
				|| textRenewMonth.errorProvider1.GetError(textRenewMonth)!=""
				|| textDeductible.errorProvider1.GetError(textDeductible)!=""
				|| textOrthoMax.errorProvider1.GetError(textOrthoMax)!=""
				|| textFloToAge.errorProvider1.GetError(textFloToAge)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return false;
			}
			if(textSubscriberID.Text==""){
				MessageBox.Show(Lan.g(this,"Subscriber ID not allowed to be blank."));
				return false;
			}
			if(checkSubOtherFam.Checked){
				// already handled
			}
			else{
				InsPlans.Cur.Subscriber=Patients.FamilyList[listSubscriber.SelectedIndex].PatNum;
			}
			InsPlans.Cur.SubscriberID =textSubscriberID.Text;
			InsPlans.Cur.DateEffective=PIn.PDate(textDateEffect.Text);
			InsPlans.Cur.DateTerm     =PIn.PDate(textDateTerm.Text);
			//Synchronized Information:
			GetEmployerNum();
			InsPlans.Cur.GroupName=textGroupName.Text;
			InsPlans.Cur.GroupNum=textGroupNum.Text;
			GetCarrierNum();
			//plantype already handled.
			if(listClaimForm.SelectedIndex!=-1)
				InsPlans.Cur.ClaimFormNum=ClaimForms.ListShort[listClaimForm.SelectedIndex].ClaimFormNum;	
			InsPlans.Cur.UseAltCode=checkAlternateCode.Checked;
			InsPlans.Cur.ClaimsUseUCR=checkClaimsUseUCR.Checked;
			if(listFeeSched.SelectedIndex==-1)
				InsPlans.Cur.FeeSched=0;
			else
				InsPlans.Cur.FeeSched=Defs.Short[(int)DefCat.FeeSchedNames][listFeeSched.SelectedIndex].DefNum;
			if(listCopay.SelectedIndex==-1)
				InsPlans.Cur.CopayFeeSched=0;
			else
				InsPlans.Cur.CopayFeeSched=Defs.Short[(int)DefCat.FeeSchedNames][listCopay.SelectedIndex].DefNum;
			//end of Plan Information
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
			return true;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(!FillCur()){
				return;
			}
			InsPlans.UpdateCur();//whether new or not because plan is created from outside this form
			//FillSynchTemplate();
			//InsTemplates.UpdateCur();//this is the only place this can be run.
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormInsPlan_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			//this form is designed to not accidently change the Patients.Cur.PatNum.
			//Obviously, because of the complexity, all family info should be refreshed after closing.
			if(IsNew){
				InsPlans.DeleteCur();
				//if(comboLinked.Items.Count==1){
				//	InsTemplates.DeleteCur();
				//}
			}
			//remember to refresh after closing this form!!!!!
		}

		

		

		

		

		

		

		

		
	

		

		

		

		

		

		


		

		

		

		

		

		

		

		

		

	}
}
