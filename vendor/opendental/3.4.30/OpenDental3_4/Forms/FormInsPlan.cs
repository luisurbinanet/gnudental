/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
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
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.RadioButton radioDedUnkn;
		private System.Windows.Forms.RadioButton radioDedNo;
		private System.Windows.Forms.RadioButton radioDedYes;
		private System.Windows.Forms.TextBox textCarrier;
		private OpenDental.ValidDate textDateEffect;
		private OpenDental.ValidDate textDateTerm;
		private System.Windows.Forms.ListBox listSubscriber;
		///<summary>The InsPlan is always inserted before opening this form.</summary>
		public bool IsNew;
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
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkRelease;
		private System.Windows.Forms.CheckBox checkNoSendElect;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.CheckBox checkAlternateCode;
		private System.Windows.Forms.CheckBox checkClaimsUseUCR;
		private System.Windows.Forms.ListBox listPlanType;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox textSubscriber;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox checkSubOtherFam;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.TextBox textSubscriberID;
		private System.Windows.Forms.Label label2;
		private OpenDental.ValidNum textRenewMonth;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Label labelOrthoMax;
		private System.Windows.Forms.Panel panelAdvancedIns;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboLinked;
		private System.Windows.Forms.TextBox textLinkedNum;
		private System.Windows.Forms.ToolTip toolTip1;
		private OpenDental.UI.Button butEditTemplate;
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
		private OpenDental.UI.Button butSelect;
		private System.Windows.Forms.Label labelCitySTZip;
		private bool mouseIsInListCars;
		private System.Windows.Forms.Label labelDrop;
		private OpenDental.UI.Button butDrop;
		private System.Windows.Forms.GroupBox groupCoPay;
		private System.Windows.Forms.Label label3;
		private OpenDental.ODtextBox textPlanNote;
		private System.Windows.Forms.ComboBox comboCopay;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ComboBox comboFeeSched;
		private System.Windows.Forms.ComboBox comboClaimForm;
		private OpenDental.UI.Button butSearch;
		///<summary></summary>
		public bool DropButVisible;
		///<summary></summary>
		public InsPlan PlanCur;
		///<summary>This is the family of the current patient, NOT of the subscriber.</summary>
		private Family FamCur;
		///<summary>This is NOT the subscriber.</summary>
		private Patient PatCur;
		private System.Windows.Forms.TextBox textElectIDdescriptt;
		private System.Windows.Forms.ComboBox comboElectIDdescript;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ComboBox comboAllowedFeeSched;
		private OpenDental.UI.Button butLabel;
		private Carrier CarrierCur;

		///<summary>Need to pass in the current patNum which is not necessarily the subscriber. But InsPlans do not have a field for PatNum. The result is that the displayed family list might change depending on which family is open when this form is called.  Subscriber is independent of family.</summary>
		public FormInsPlan(InsPlan planCur,int patNum){
			InitializeComponent();
			PlanCur=planCur;
			FamCur=Patients.GetFamily(patNum);
			PatCur=FamCur.GetPatient(patNum);
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
			Lan.F(this);
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
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
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
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkAssign = new System.Windows.Forms.CheckBox();
			this.checkRelease = new System.Windows.Forms.CheckBox();
			this.checkNoSendElect = new System.Windows.Forms.CheckBox();
			this.label23 = new System.Windows.Forms.Label();
			this.checkAlternateCode = new System.Windows.Forms.CheckBox();
			this.checkClaimsUseUCR = new System.Windows.Forms.CheckBox();
			this.listPlanType = new System.Windows.Forms.ListBox();
			this.label14 = new System.Windows.Forms.Label();
			this.textSubscriber = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkSubOtherFam = new System.Windows.Forms.CheckBox();
			this.textSubscriberID = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.textRenewMonth = new OpenDental.ValidNum();
			this.groupSynch = new System.Windows.Forms.GroupBox();
			this.butSelect = new OpenDental.UI.Button();
			this.butEditTemplate = new OpenDental.UI.Button();
			this.comboLinked = new System.Windows.Forms.ComboBox();
			this.textLinkedNum = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.comboClaimForm = new System.Windows.Forms.ComboBox();
			this.comboFeeSched = new System.Windows.Forms.ComboBox();
			this.groupCoPay = new System.Windows.Forms.GroupBox();
			this.label12 = new System.Windows.Forms.Label();
			this.comboAllowedFeeSched = new System.Windows.Forms.ComboBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.comboCopay = new System.Windows.Forms.ComboBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.comboElectIDdescript = new System.Windows.Forms.ComboBox();
			this.butSearch = new OpenDental.UI.Button();
			this.textElectIDdescriptt = new System.Windows.Forms.TextBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.textPlanNote = new OpenDental.ODtextBox();
			this.panelAdvancedIns = new System.Windows.Forms.Panel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.labelDrop = new System.Windows.Forms.Label();
			this.butDrop = new OpenDental.UI.Button();
			this.butLabel = new OpenDental.UI.Button();
			this.panel4.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupSynch.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupCoPay.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.panelAdvancedIns.SuspendLayout();
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
			this.labelCitySTZip.Location = new System.Drawing.Point(3, 91);
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
			this.label27.Location = new System.Drawing.Point(6, 35);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(91, 16);
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
			this.textElectID.Validating += new System.ComponentModel.CancelEventHandler(this.textElectID_Validating);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(761, 666);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(851, 666);
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
			this.label30.Location = new System.Drawing.Point(11, 84);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(88, 26);
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
			this.label29.Location = new System.Drawing.Point(9, 54);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(92, 26);
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
			this.label22.Size = new System.Drawing.Size(293, 17);
			this.label22.TabIndex = 21;
			this.label22.Text = "Plan Percentages (single click to edit)";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(7, 303);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 15);
			this.label1.TabIndex = 91;
			this.label1.Text = "Fee Schedule";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.checkAssign.Size = new System.Drawing.Size(181, 16);
			this.checkAssign.TabIndex = 1;
			this.checkAssign.Text = "Assignment of Benefits";
			// 
			// checkRelease
			// 
			this.checkRelease.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkRelease.Location = new System.Drawing.Point(12, 17);
			this.checkRelease.Name = "checkRelease";
			this.checkRelease.Size = new System.Drawing.Size(179, 17);
			this.checkRelease.TabIndex = 0;
			this.checkRelease.Text = "Release of Information";
			// 
			// checkNoSendElect
			// 
			this.checkNoSendElect.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNoSendElect.Location = new System.Drawing.Point(176, 133);
			this.checkNoSendElect.Name = "checkNoSendElect";
			this.checkNoSendElect.Size = new System.Drawing.Size(213, 17);
			this.checkNoSendElect.TabIndex = 8;
			this.checkNoSendElect.Text = "Don\'t Usually Send Electronically";
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(6, 327);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(95, 14);
			this.label23.TabIndex = 96;
			this.label23.Text = "Claim Form";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkAlternateCode
			// 
			this.checkAlternateCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAlternateCode.Location = new System.Drawing.Point(104, 269);
			this.checkAlternateCode.Name = "checkAlternateCode";
			this.checkAlternateCode.Size = new System.Drawing.Size(286, 17);
			this.checkAlternateCode.TabIndex = 5;
			this.checkAlternateCode.Text = "Use Alternate Code (for some Medicaid plans)";
			// 
			// checkClaimsUseUCR
			// 
			this.checkClaimsUseUCR.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkClaimsUseUCR.Location = new System.Drawing.Point(104, 285);
			this.checkClaimsUseUCR.Name = "checkClaimsUseUCR";
			this.checkClaimsUseUCR.Size = new System.Drawing.Size(306, 17);
			this.checkClaimsUseUCR.TabIndex = 6;
			this.checkClaimsUseUCR.Text = "Claims show UCR fee, not billed fee";
			// 
			// listPlanType
			// 
			this.listPlanType.Location = new System.Drawing.Point(104, 226);
			this.listPlanType.Name = "listPlanType";
			this.listPlanType.Size = new System.Drawing.Size(143, 43);
			this.listPlanType.TabIndex = 4;
			this.listPlanType.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listPlanType_MouseDown);
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(9, 228);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(95, 15);
			this.label14.TabIndex = 104;
			this.label14.Text = "Plan Type";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(5, 665);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(81, 26);
			this.butDelete.TabIndex = 112;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// textRenewMonth
			// 
			this.textRenewMonth.Location = new System.Drawing.Point(109, 78);
			this.textRenewMonth.MaxVal = 255;
			this.textRenewMonth.MinVal = 1;
			this.textRenewMonth.Name = "textRenewMonth";
			this.textRenewMonth.Size = new System.Drawing.Size(99, 20);
			this.textRenewMonth.TabIndex = 3;
			this.textRenewMonth.Text = "";
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
			this.butSelect.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSelect.Autosize = true;
			this.butSelect.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSelect.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
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
			this.butEditTemplate.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEditTemplate.Autosize = true;
			this.butEditTemplate.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEditTemplate.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
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
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.comboClaimForm);
			this.groupBox4.Controls.Add(this.comboFeeSched);
			this.groupBox4.Controls.Add(this.groupCoPay);
			this.groupBox4.Controls.Add(this.textGroupNum);
			this.groupBox4.Controls.Add(this.groupBox3);
			this.groupBox4.Controls.Add(this.textEmployer);
			this.groupBox4.Controls.Add(this.label16);
			this.groupBox4.Controls.Add(this.textGroupName);
			this.groupBox4.Controls.Add(this.label8);
			this.groupBox4.Controls.Add(this.label9);
			this.groupBox4.Controls.Add(this.label1);
			this.groupBox4.Controls.Add(this.label23);
			this.groupBox4.Controls.Add(this.checkClaimsUseUCR);
			this.groupBox4.Controls.Add(this.listPlanType);
			this.groupBox4.Controls.Add(this.label14);
			this.groupBox4.Controls.Add(this.checkAlternateCode);
			this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox4.Location = new System.Drawing.Point(6, 145);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(413, 494);
			this.groupBox4.TabIndex = 1;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Synchronized Information";
			// 
			// comboClaimForm
			// 
			this.comboClaimForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClaimForm.Location = new System.Drawing.Point(104, 324);
			this.comboClaimForm.MaxDropDownItems = 30;
			this.comboClaimForm.Name = "comboClaimForm";
			this.comboClaimForm.Size = new System.Drawing.Size(212, 21);
			this.comboClaimForm.TabIndex = 110;
			// 
			// comboFeeSched
			// 
			this.comboFeeSched.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboFeeSched.Location = new System.Drawing.Point(104, 302);
			this.comboFeeSched.MaxDropDownItems = 30;
			this.comboFeeSched.Name = "comboFeeSched";
			this.comboFeeSched.Size = new System.Drawing.Size(212, 21);
			this.comboFeeSched.TabIndex = 109;
			// 
			// groupCoPay
			// 
			this.groupCoPay.Controls.Add(this.label12);
			this.groupCoPay.Controls.Add(this.comboAllowedFeeSched);
			this.groupCoPay.Controls.Add(this.label11);
			this.groupCoPay.Controls.Add(this.label3);
			this.groupCoPay.Controls.Add(this.comboCopay);
			this.groupCoPay.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupCoPay.Location = new System.Drawing.Point(11, 358);
			this.groupCoPay.Name = "groupCoPay";
			this.groupCoPay.Size = new System.Drawing.Size(392, 104);
			this.groupCoPay.TabIndex = 107;
			this.groupCoPay.TabStop = false;
			this.groupCoPay.Text = "Other Fee Schedules";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(6, 75);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(168, 16);
			this.label12.TabIndex = 111;
			this.label12.Text = "Carrier Allowed Amounts";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboAllowedFeeSched
			// 
			this.comboAllowedFeeSched.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboAllowedFeeSched.Location = new System.Drawing.Point(176, 72);
			this.comboAllowedFeeSched.MaxDropDownItems = 30;
			this.comboAllowedFeeSched.Name = "comboAllowedFeeSched";
			this.comboAllowedFeeSched.Size = new System.Drawing.Size(209, 21);
			this.comboAllowedFeeSched.TabIndex = 110;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(6, 53);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(168, 16);
			this.label11.TabIndex = 109;
			this.label11.Text = "Patient Co-pay Amounts";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(3, 19);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(381, 29);
			this.label3.TabIndex = 106;
			this.label3.Text = "Don\'t use these unless you understand how they will affect your estimates";
			// 
			// comboCopay
			// 
			this.comboCopay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboCopay.Location = new System.Drawing.Point(176, 50);
			this.comboCopay.MaxDropDownItems = 30;
			this.comboCopay.Name = "comboCopay";
			this.comboCopay.Size = new System.Drawing.Size(209, 21);
			this.comboCopay.TabIndex = 108;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.comboElectIDdescript);
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
			this.groupBox3.Controls.Add(this.butSearch);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(4, 71);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(397, 154);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Carrier";
			// 
			// comboElectIDdescript
			// 
			this.comboElectIDdescript.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboElectIDdescript.Location = new System.Drawing.Point(154, 109);
			this.comboElectIDdescript.MaxDropDownItems = 30;
			this.comboElectIDdescript.Name = "comboElectIDdescript";
			this.comboElectIDdescript.Size = new System.Drawing.Size(237, 21);
			this.comboElectIDdescript.TabIndex = 125;
			this.comboElectIDdescript.SelectedIndexChanged += new System.EventHandler(this.comboElectIDdescript_SelectedIndexChanged);
			// 
			// butSearch
			// 
			this.butSearch.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butSearch.Autosize = true;
			this.butSearch.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSearch.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSearch.Location = new System.Drawing.Point(73, 130);
			this.butSearch.Name = "butSearch";
			this.butSearch.Size = new System.Drawing.Size(84, 23);
			this.butSearch.TabIndex = 124;
			this.butSearch.Text = "Search IDs";
			this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
			// 
			// textElectIDdescriptt
			// 
			this.textElectIDdescriptt.Location = new System.Drawing.Point(484, 603);
			this.textElectIDdescriptt.MaxLength = 5;
			this.textElectIDdescriptt.Name = "textElectIDdescriptt";
			this.textElectIDdescriptt.ReadOnly = true;
			this.textElectIDdescriptt.Size = new System.Drawing.Size(237, 20);
			this.textElectIDdescriptt.TabIndex = 80;
			this.textElectIDdescriptt.Text = "";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.textPlanNote);
			this.groupBox5.Controls.Add(this.groupBox1);
			this.groupBox5.Controls.Add(this.tbPercent1);
			this.groupBox5.Controls.Add(this.panelAdvancedIns);
			this.groupBox5.Controls.Add(this.textAnnualMax);
			this.groupBox5.Controls.Add(this.textOrthoMax);
			this.groupBox5.Controls.Add(this.textDeductible);
			this.groupBox5.Controls.Add(this.label22);
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
			this.groupBox5.Text = "Benefit Information";
			// 
			// textPlanNote
			// 
			this.textPlanNote.AcceptsReturn = true;
			this.textPlanNote.Location = new System.Drawing.Point(109, 371);
			this.textPlanNote.Multiline = true;
			this.textPlanNote.Name = "textPlanNote";
			this.textPlanNote.QuickPasteType = OpenDental.QuickPasteType.InsPlan;
			this.textPlanNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textPlanNote.Size = new System.Drawing.Size(340, 95);
			this.textPlanNote.TabIndex = 29;
			this.textPlanNote.Text = "";
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
			// labelDrop
			// 
			this.labelDrop.Location = new System.Drawing.Point(185, 641);
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
			this.butDrop.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDrop.Autosize = true;
			this.butDrop.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDrop.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDrop.Location = new System.Drawing.Point(100, 665);
			this.butDrop.Name = "butDrop";
			this.butDrop.Size = new System.Drawing.Size(75, 26);
			this.butDrop.TabIndex = 123;
			this.butDrop.Text = "Drop";
			this.butDrop.Visible = false;
			this.butDrop.Click += new System.EventHandler(this.butDrop_Click);
			// 
			// butLabel
			// 
			this.butLabel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butLabel.Autosize = true;
			this.butLabel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLabel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLabel.Image = ((System.Drawing.Image)(resources.GetObject("butLabel.Image")));
			this.butLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butLabel.Location = new System.Drawing.Point(449, 667);
			this.butLabel.Name = "butLabel";
			this.butLabel.Size = new System.Drawing.Size(81, 26);
			this.butLabel.TabIndex = 125;
			this.butLabel.Text = "Label";
			this.butLabel.Click += new System.EventHandler(this.butLabel_Click);
			// 
			// FormInsPlan
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(961, 700);
			this.Controls.Add(this.butLabel);
			this.Controls.Add(this.labelDrop);
			this.Controls.Add(this.butDrop);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.groupSynch);
			this.Controls.Add(this.textElectIDdescriptt);
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
			this.groupCoPay.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.panelAdvancedIns.ResumeLayout(false);
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
					CovPats.Cur.PlanNum=PlanCur.PlanNum;
					CovPats.Cur.Percent=CovCats.ListShort[i].DefaultPercent;
					CovPats.InsertCur();
				}
				PlanCur.ReleaseInfo=true;
				PlanCur.AssignBen=true;
				PlanCur.PlanType="";//insurance
      }
			if(DropButVisible){
				butDrop.Visible=true;
				labelDrop.Visible=true;
			}
			if(((Pref)Prefs.HList["EasyHideCapitation"]).ValueString=="1"){
				//groupCoPay.Visible=false;
				//comboCopay.Visible=false;
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

		///<summary>Fills the form based on the data in PlanCur.  Includes calls to FillSubscriber, FillCarrier, FillPercentages, and	LayoutSynch.</summary>
		private void FillFormData(){
			FillSubscriber();
			textSubscriberID.Text=PlanCur.SubscriberID;
			if(PlanCur.DateEffective.Year < 1880)
				textDateEffect.Text="";
			else
				textDateEffect.Text=PlanCur.DateEffective.ToString("d");
			if(PlanCur.DateTerm.Year < 1880)
				textDateTerm.Text="";
			else
				textDateTerm.Text=PlanCur.DateTerm.ToString("d");
			textEmployer.Text=Employers.GetName(PlanCur.EmployerNum);
			textGroupName.Text=PlanCur.GroupName;
			textGroupNum.Text=PlanCur.GroupNum;
			listPlanType.Items.Clear();
			listPlanType.Items.Add(Lan.g(this,"Category Percentage"));
			if(PlanCur.PlanType=="")
				listPlanType.SelectedIndex=0;
			listPlanType.Items.Add(Lan.g(this,"Medicaid or Flat Co-pay"));
			if(PlanCur.PlanType=="f")
				listPlanType.SelectedIndex=1;
			if(((Pref)Prefs.HList["EasyHideCapitation"]).ValueString!="1"){
				listPlanType.Items.Add(Lan.g(this,"Capitation"));
				if(PlanCur.PlanType=="c")
					listPlanType.SelectedIndex=2;
			}
			checkAlternateCode.Checked=PlanCur.UseAltCode;
			checkClaimsUseUCR.Checked=PlanCur.ClaimsUseUCR;



			comboFeeSched.Items.Clear();
			comboFeeSched.Items.Add(Lan.g(this,"none"));
			comboFeeSched.SelectedIndex=0;
			for(int i=0;i<Defs.Short[(int)DefCat.FeeSchedNames].Length;i++){
				comboFeeSched.Items.Add(Defs.Short[(int)DefCat.FeeSchedNames][i].ItemName);
				if(Defs.Short[(int)DefCat.FeeSchedNames][i].DefNum==PlanCur.FeeSched)
					comboFeeSched.SelectedIndex=i+1;
			}
			comboCopay.Items.Clear();
			comboCopay.Items.Add(Lan.g(this,"none"));
			comboCopay.SelectedIndex=0;
			for(int i=0;i<Defs.Short[(int)DefCat.FeeSchedNames].Length;i++){
				comboCopay.Items.Add(Defs.Short[(int)DefCat.FeeSchedNames][i].ItemName);
				if(Defs.Short[(int)DefCat.FeeSchedNames][i].DefNum==PlanCur.CopayFeeSched)
					comboCopay.SelectedIndex=i+1;
			}
			comboAllowedFeeSched.Items.Clear();
			comboAllowedFeeSched.Items.Add(Lan.g(this,"none"));
			comboAllowedFeeSched.SelectedIndex=0;
			for(int i=0;i<Defs.Short[(int)DefCat.FeeSchedNames].Length;i++){
				comboAllowedFeeSched.Items.Add(Defs.Short[(int)DefCat.FeeSchedNames][i].ItemName);
				if(Defs.Short[(int)DefCat.FeeSchedNames][i].DefNum==PlanCur.AllowedFeeSched)
					comboAllowedFeeSched.SelectedIndex=i+1;
			}
			comboClaimForm.Items.Clear();
			for(int i=0;i<ClaimForms.ListShort.Length;i++){
				comboClaimForm.Items.Add(ClaimForms.ListShort[i].Description);
				if(ClaimForms.ListShort[i].ClaimFormNum==PlanCur.ClaimFormNum){
					comboClaimForm.SelectedIndex=i;
				}
			}
			if(comboClaimForm.Items.Count>0 && comboClaimForm.SelectedIndex==-1){
				comboClaimForm.SelectedIndex=0;//this will let the user rearrange the default later
			}
			textAnnualMax.MaxVal=50000;
			if(PlanCur.AnnualMax==-1)
				textAnnualMax.Text="";
			else
				textAnnualMax.Text=PlanCur.AnnualMax.ToString();
			textOrthoMax.MaxVal=50000;
			if(PlanCur.OrthoMax==-1)
				textOrthoMax.Text="";
			else
				textOrthoMax.Text=PlanCur.OrthoMax.ToString();
			textRenewMonth.MaxVal=12;
			if(PlanCur.RenewMonth==-1)
				textRenewMonth.Text="";
			else
				textRenewMonth.Text=PlanCur.RenewMonth.ToString();
			textDeductible.MaxVal=10000;
			if(PlanCur.Deductible==-1)
				textDeductible.Text="";
			else
				textDeductible.Text=PlanCur.Deductible.ToString();
			switch (PlanCur.DeductWaivPrev){
				case YN.Unknown:radioDedUnkn.Checked=true;break;
				case YN.Yes:radioDedYes.Checked=true;break;
				case YN.No:radioDedNo.Checked=true;break;
			}
			textFloToAge.MaxVal=100;
			if(PlanCur.FloToAge==-1)
				textFloToAge.Text="";
			else
				textFloToAge.Text=PlanCur.FloToAge.ToString();
			textPlanNote.Text=PlanCur.PlanNote;
			switch (PlanCur.MissToothExcl){
				case YN.Unknown:radioMissUnkn.Checked=true;break;
				case YN.Yes:radioMissYes.Checked=true;break;
				case YN.No:radioMissNo.Checked=true;break;
			}
			switch (PlanCur.MajorWait){
				case YN.Unknown:radioWaitUnkn.Checked=true;break;
				case YN.Yes:radioWaitYes.Checked=true;break;
				case YN.No:radioWaitNo.Checked=true;break;
			}
			checkRelease.Checked=PlanCur.ReleaseInfo;
			checkAssign.Checked=PlanCur.AssignBen;
			FillCarrier();
			FillPercentages();
			LayoutSynch();
		}

		///<summary>Fills the carrier fields on the form based on CarrierCur.</summary>
		private void FillCarrier(){
			CarrierCur=Carriers.GetCarrier(PlanCur.CarrierNum);
			textCarrier.Text=CarrierCur.CarrierName;
			textPhone.Text=CarrierCur.Phone;
			textAddress.Text=CarrierCur.Address;
			textAddress2.Text=CarrierCur.Address2;
			textCity.Text=CarrierCur.City;
			textState.Text=CarrierCur.State;
			textZip.Text=CarrierCur.Zip;
			textElectID.Text=CarrierCur.ElectID;
			FillPayor();
			checkNoSendElect.Checked=CarrierCur.NoSendElect;
			//MessageBox.Show(CarrierCur.CarrierNum.ToString());
		}

		///<summary>Only called from FillCarrier, butSearch_Click, and textElectID_Validating. Fills comboElectIDdescript as appropriate.</summary>
		private void FillPayor(){
			//textElectIDdescript.Text=ElectIDs.GetDescript(textElectID.Text);
			comboElectIDdescript.Items.Clear();
			string[] payorNames=ElectIDs.GetDescripts(textElectID.Text);
			if(payorNames.Length>1){
				comboElectIDdescript.Items.Add("multiple payors use this ID");
			}
			for(int i=0;i<payorNames.Length;i++){
				comboElectIDdescript.Items.Add(payorNames[i]);
			}
			if(payorNames.Length>0){
				comboElectIDdescript.SelectedIndex=0;
			}
		}

		private void comboElectIDdescript_SelectedIndexChanged(object sender, System.EventArgs e) {
			if(comboElectIDdescript.Items.Count>0){
				comboElectIDdescript.SelectedIndex=0;//always show the first item in the list
			}
		}

		///<summary>Gets percentages from the db and then displays them on the form.</summary>
		private void FillPercentages(){
			CovPats.RefreshForPlan(PlanCur);
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

		///<summary>PlanCur.Subscriber is one value that is always kept in synch with the display.  If program changes PlanCur.Subscriber, then it will run this method to update the display.  If user changes display, then _MouseDown is run to update the PlanCur.Subscriber.  On the other hand, the subscriberID text will always be updated rather than the actual subscriberID which does not get updated until OK_click.</summary>
		private void FillSubscriber(){
			listSubscriber.Items.Clear();
			for(int i=0;i<FamCur.List.Length;i++){
				listSubscriber.Items.Add(FamCur.GetNameInFamLFI(i));
				if(PlanCur.Subscriber==FamCur.List[i].PatNum){
					listSubscriber.SelectedIndex=i;
				}
			}
			//this can happen if user unchecks the "Is From Other Fam" box. Need to reset.
			if(PlanCur.Subscriber==0){
				listSubscriber.SelectedIndex=FamCur.GetIndex(PatCur.PatNum);
				//the initial subscriber will be the current patient
				textSubscriberID.Text=PatCur.SSN;
				//PlanCur.SubscriberID=PatCur.SSN;
			}
			if(listSubscriber.SelectedIndex==-1){//subscriber not in family
				checkSubOtherFam.Checked=true;
				textSubscriber.Visible=true;
				listSubscriber.Visible=false;
				textSubscriber.Text=Patients.GetLim(PlanCur.Subscriber).GetNameLF();
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
				FormPatientSelect FormPS=new FormPatientSelect();
				FormPS.SelectionModeOnly=true;//this will cause a change in the patNum only
				FormPS.ShowDialog();
				if(FormPS.DialogResult!=DialogResult.OK){
					checkSubOtherFam.Checked=false;
					return;
				}
				PlanCur.Subscriber=FormPS.SelectedPatNum;
				PlanCur.SubscriberID=Patients.GetLim(FormPS.SelectedPatNum).SSN;
			}
			else{//switch to family view
				PlanCur.Subscriber=0;//this will reset the subscriber and ID to current patient
			}
			FillSubscriber();
			//textSubscriberID.Text=PlanCur.SubscriberID;//because it will usually have changed.
		}

		private void listSubscriber_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listSubscriber.SelectedIndex==-1){
				return;
			}
			PlanCur.Subscriber=FamCur.List[listSubscriber.SelectedIndex].PatNum;
			//InsPlans.Cur.SubscriberID=Family.List[listSubscriber.SelectedIndex].SSN;
			textSubscriberID.Text=FamCur.List[listSubscriber.SelectedIndex].SSN;
				//InsPlans.Cur.SubscriberID;
		}

		private void listPlanType_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//MessageBox.Show(InsPlans.Cur.PlanType+","+listPlanType.SelectedIndex.ToString());
			if(PlanCur.PlanType==""
				&& (listPlanType.SelectedIndex==1
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
					PlanCur.PlanType="";
					break;
				case 1:
					PlanCur.PlanType="f";
					break;
				case 2:
					PlanCur.PlanType="c";
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
				CovPats.Cur.PlanNum=PlanCur.PlanNum;
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

		private void radioDedYes_Click(object sender, System.EventArgs e) {
			PlanCur.DeductWaivPrev=YN.Yes;
		}

		private void radioDedNo_Click(object sender, System.EventArgs e) {
			PlanCur.DeductWaivPrev=YN.No;
		}

		private void radioDedUnkn_Click(object sender, System.EventArgs e) {
			PlanCur.DeductWaivPrev=YN.Unknown;
		}

		private void radioMissYes_Click(object sender, System.EventArgs e) {
			PlanCur.MissToothExcl=YN.Yes;
		}

		private void radioMissNo_Click(object sender, System.EventArgs e) {
			PlanCur.MissToothExcl=YN.No;
		}

		private void radioMissUnkn_Click(object sender, System.EventArgs e) {
			PlanCur.MissToothExcl=YN.Unknown;
		}

		private void radioWaitYes_Click(object sender, System.EventArgs e) {
			PlanCur.MajorWait=YN.Yes;
		}

		private void radioWaitNo_Click(object sender, System.EventArgs e) {
			PlanCur.MajorWait=YN.No;
		}

		private void radioWaitUnkn_Click(object sender, System.EventArgs e) {
			PlanCur.MajorWait=YN.Unknown;
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
					PlanCur.CarrierNum=((Carrier)similarCars[listCars.SelectedIndex]).CarrierNum;
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
				PlanCur.CarrierNum=((Carrier)similarCars[listCars.SelectedIndex]).CarrierNum;
				FillCarrier();
			}
			listCars.Visible=false;
		}

		private void listCars_Click(object sender, System.EventArgs e){
			PlanCur.CarrierNum=((Carrier)similarCars[listCars.SelectedIndex]).CarrierNum;
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
			PlanCur.CarrierNum=((Carrier)similarCars[listCars.SelectedIndex]).CarrierNum;
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
			string[] samePlans=PlanCur.SamePlans();
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

		private void textElectID_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			if(textElectID.Text==""){
				return;
			}
			string[] electIDs=ElectIDs.GetDescripts(textElectID.Text);
			if(electIDs.Length==0){
				if(MessageBox.Show(Lan.g(this,"Electronic ID not found. Continue anyway?"),"",
					MessageBoxButtons.OKCancel)!=DialogResult.OK)
				{
					e.Cancel=true;
					return;
				}
			}
			FillPayor();
		}

		private void butSearch_Click(object sender, System.EventArgs e) {
			FormElectIDs FormE=new FormElectIDs();
			FormE.IsSelectMode=true;
			FormE.ShowDialog();
			if(FormE.DialogResult!=DialogResult.OK){
				return;
			}
			textElectID.Text=FormE.selectedID.PayorID;
			FillPayor();
			//textElectIDdescript.Text=FormE.selectedID.CarrierName;
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
			PlanCur.EmployerNum    =FormIP.SelectedPlan.EmployerNum;
			PlanCur.GroupName      =FormIP.SelectedPlan.GroupName;
			PlanCur.GroupNum       =FormIP.SelectedPlan.GroupNum;
			PlanCur.CarrierNum     =FormIP.SelectedPlan.CarrierNum;
			PlanCur.PlanType       =FormIP.SelectedPlan.PlanType;
			PlanCur.UseAltCode     =FormIP.SelectedPlan.UseAltCode;
			PlanCur.ClaimsUseUCR   =FormIP.SelectedPlan.ClaimsUseUCR;
			PlanCur.FeeSched       =FormIP.SelectedPlan.FeeSched;
			PlanCur.CopayFeeSched  =FormIP.SelectedPlan.CopayFeeSched;
			PlanCur.ClaimFormNum   =FormIP.SelectedPlan.ClaimFormNum;
			PlanCur.AllowedFeeSched=FormIP.SelectedPlan.AllowedFeeSched;
			PlanCur.Update();//updates to the db so that the synch info will show correctly
			FillFormData();
		}

		private void butEditTemplate_Click(object sender, System.EventArgs e) {
			//notice that this is called without saving any changes that have been made so far.
			//int curPlanNum=InsPlans.Cur.PlanNum;
			FormInsPlanEditAll FormIPE=new FormInsPlanEditAll(PlanCur.Copy(),PatCur.PatNum);
			FormIPE.ShowDialog();
			if(FormIPE.DialogResult!=DialogResult.OK){
				return;
			}
			//InsPlans.Refresh(curPlanNum);
			FillFormData();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete Plan?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
				return;
			Cursor=Cursors.WaitCursor;
			//The plan list is not critical.  Supplying it just helps reduce network traffic.
			//We are supplying the list for the current family, which might not include all relevant plans.
			InsPlan[] PlanList=InsPlans.Refresh(FamCur);
			if(!PlanCur.Delete(PlanList)){//checks dependencies first.
				Cursor=Cursors.Default;
				return;
			}
			PlanCur=new InsPlan();//this sets the PlanNum to 0
			Cursor=Cursors.Default;
			DialogResult=DialogResult.Cancel;//has to be cancel to trigger deletion if new
		}

		private void butDrop_Click(object sender, System.EventArgs e) {
			Patient PatOld=PatCur.Copy();
			//if dropping primary ins
			if(PatCur.PriPlanNum==PlanCur.PlanNum){
				if(PatCur.SecPlanNum!=0){//if patient has secondary ins.
					PatCur.PriPlanNum=PatCur.SecPlanNum;
					PatCur.PriRelationship=PatCur.SecRelationship;
					PatCur.SecPlanNum=0;
					PatCur.SecRelationship=Relat.Self;
				}
				else{//patient does not have secondary
					PatCur.PriPlanNum=0;
					PatCur.PriRelationship=Relat.Self;
				}
				PatCur.Update(PatOld);
			}
			//if dropping sec ins.
			else if(PatCur.SecPlanNum==PlanCur.PlanNum){
				PatCur.SecPlanNum=0;
				PatCur.SecRelationship=Relat.Self;
				PatCur.Update(PatOld);
			}
			else{
				MsgBox.Show(this,"Error. Current patient is not covered by this plan.");
				return;
			}
			//no need to Compute estimates for any but the current patient
			FamCur=Patients.GetFamily(PatCur.PatNum);//because changes were made.
			ClaimProc[] claimProcs=ClaimProcs.Refresh(PatCur.PatNum);
			Procedure[] procs=Procedures.Refresh(PatCur.PatNum);
			InsPlan[] planList=InsPlans.Refresh(FamCur);
			Procedures.ComputeEstimatesForAll(PatCur.PatNum,PatCur.PriPlanNum,
					PatCur.SecPlanNum,claimProcs,procs,PatCur,planList);
			//remember to refresh after closing this form!!!
			DialogResult=DialogResult.OK;
		}

		private void butLabel_Click(object sender, System.EventArgs e) {
			GetCarrierNum();
			LabelSingle label=new LabelSingle();
			PrintDocument pd=new PrintDocument();//only used to pass printerName
			if(!Printers.SetPrinter(pd,PrintSituation.LabelSingle)){
				return;
			}
			label.PrintIns(CarrierCur,pd.PrinterSettings.PrinterName);
		}

		///<summary>Gets an employerNum based on the name entered. Called from FillCur</summary>
		private void GetEmployerNum(){
			if(PlanCur.EmployerNum==0){//no employer was previously entered.
				if(textEmployer.Text==""){
					//no change
				}
				else{
					PlanCur.EmployerNum=Employers.GetEmployerNum(textEmployer.Text);
				}
			}
			else{//an employer was previously entered
				if(textEmployer.Text==""){
					PlanCur.EmployerNum=0;
				}
				//if text has changed
				else if(Employers.GetName(PlanCur.EmployerNum)!=textEmployer.Text){
					PlanCur.EmployerNum=Employers.GetEmployerNum(textEmployer.Text);
				}
			}
		}

		///<summary>Gets a carrierNum based on the data entered. Called from FillCur and butLabel_Click</summary>
		private void GetCarrierNum(){
			CarrierCur=new Carrier();
			CarrierCur.CarrierName=textCarrier.Text;
			CarrierCur.Phone=textPhone.Text;
			CarrierCur.Address=textAddress.Text;
			CarrierCur.Address2=textAddress2.Text;
			CarrierCur.City=textCity.Text;
			CarrierCur.State=textState.Text;
			CarrierCur.Zip=textZip.Text;
			CarrierCur.ElectID=textElectID.Text;
			CarrierCur.NoSendElect=checkNoSendElect.Checked;
			Carriers.Cur=CarrierCur;
			Carriers.GetCurSame();
			PlanCur.CarrierNum=Carriers.Cur.CarrierNum;
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
			if(textCarrier.Text==""){
				MessageBox.Show(Lan.g(this,"Carrier not allowed to be blank."));
				return false;
			}
			if(checkSubOtherFam.Checked){
				// already handled
			}
			else{
				//This is still needed because it might be zero:
				PlanCur.Subscriber=FamCur.List[listSubscriber.SelectedIndex].PatNum;
			}
			PlanCur.SubscriberID =textSubscriberID.Text;
			PlanCur.DateEffective=PIn.PDate(textDateEffect.Text);
			PlanCur.DateTerm     =PIn.PDate(textDateTerm.Text);
			//Synchronized Information:
			GetEmployerNum();
			PlanCur.GroupName=textGroupName.Text;
			PlanCur.GroupNum=textGroupNum.Text;
			GetCarrierNum();
			//plantype already handled.
			if(comboClaimForm.SelectedIndex!=-1)
				PlanCur.ClaimFormNum=ClaimForms.ListShort[comboClaimForm.SelectedIndex].ClaimFormNum;	
			PlanCur.UseAltCode=checkAlternateCode.Checked;
			PlanCur.ClaimsUseUCR=checkClaimsUseUCR.Checked;
			if(comboFeeSched.SelectedIndex==0)
				PlanCur.FeeSched=0;
			else
				PlanCur.FeeSched=Defs.Short[(int)DefCat.FeeSchedNames][comboFeeSched.SelectedIndex-1].DefNum;
			if(comboCopay.SelectedIndex==0)
				PlanCur.CopayFeeSched=0;
			else
				PlanCur.CopayFeeSched=Defs.Short[(int)DefCat.FeeSchedNames][comboCopay.SelectedIndex-1].DefNum;
			if(comboAllowedFeeSched.SelectedIndex==0)
				PlanCur.AllowedFeeSched=0;
			else
				PlanCur.AllowedFeeSched
					=Defs.Short[(int)DefCat.FeeSchedNames][comboAllowedFeeSched.SelectedIndex-1].DefNum;
			//end of Plan Information
			if(textAnnualMax.Text=="")
				PlanCur.AnnualMax=-1;
			else
				PlanCur.AnnualMax=PIn.PInt(textAnnualMax.Text);
			if(textRenewMonth.Text=="")
				PlanCur.RenewMonth=-1;
			else
				PlanCur.RenewMonth=PIn.PInt(textRenewMonth.Text);
			if(textDeductible.Text=="")
				PlanCur.Deductible=-1;
			else
				PlanCur.Deductible=PIn.PInt(textDeductible.Text);
			//skip DedWaivPrev
			if(textOrthoMax.Text=="")
				PlanCur.OrthoMax=-1;
			else
				PlanCur.OrthoMax=PIn.PInt(textOrthoMax.Text);
			if(textFloToAge.Text=="")
				PlanCur.FloToAge=-1;
			else
				PlanCur.FloToAge=PIn.PInt(textFloToAge.Text);
			PlanCur.PlanNote=textPlanNote.Text;
			//missToothExcl
			//majorwait
			PlanCur.ReleaseInfo=checkRelease.Checked;
			PlanCur.AssignBen=checkAssign.Checked;
			return true;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(!FillCur()){
				return;
			}
			PlanCur.Update();//whether new or not because plan is created from outside this form
			PlanCur.ComputeEstimatesForCur(FamCur);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormInsPlan_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNew){
				InsPlan[] PlanList=InsPlans.Refresh(FamCur);
				PlanCur.Delete(PlanList);
			}
			//remember to refresh after closing this form!!!!!
		}

		

		

		

		

		

		

		

		

		

		

		

		
	

		

		

		

		

		

		


		

		

		

		

		

		

		

		

		

	}
}
