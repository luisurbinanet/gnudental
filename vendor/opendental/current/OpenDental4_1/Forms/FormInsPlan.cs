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
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;
using OpenDental.Bridges;
using OpenDental.UI;

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
		private System.Windows.Forms.TextBox textCarrier;
		private OpenDental.ValidDate textDateEffect;
		private OpenDental.ValidDate textDateTerm;
		///<summary>The InsPlan is always inserted before opening this form.</summary>
		public bool IsNewPlan;
		///<summary>The PatPlan is always inserted before opening this form.</summary>
		public bool IsNewPatPlan;
		private System.Windows.Forms.TextBox textEmployer;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox textAddress2;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkRelease;
		private System.Windows.Forms.CheckBox checkNoSendElect;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.CheckBox checkClaimsUseUCR;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox textSubscriber;
		private System.Windows.Forms.GroupBox groupSubscriber;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.TextBox textSubscriberID;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboLinked;
		private System.Windows.Forms.TextBox textLinkedNum;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.CheckBox checkAssign;
		private ArrayList similarEmps;
		private string empOriginal;//used in the emp dropdown logic
		private System.Windows.Forms.ListBox listEmps;//displayed from within code, not designer
		private bool mouseIsInListEmps;
		private ArrayList similarCars;
		private string carOriginal;
		private System.Windows.Forms.ListBox listCars;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label labelCitySTZip;
		private bool mouseIsInListCars;
		private System.Windows.Forms.Label labelDrop;
		private OpenDental.UI.Button butDrop;
		private System.Windows.Forms.GroupBox groupCoPay;
		private System.Windows.Forms.Label label3;
		private OpenDental.ODtextBox textSubscNote;
		private System.Windows.Forms.ComboBox comboCopay;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ComboBox comboFeeSched;
		private System.Windows.Forms.ComboBox comboClaimForm;
		private OpenDental.UI.Button butSearch;
		///<summary></summary>
		private InsPlan PlanCur;
		///<summary>This is a copy of PlanCur as it was originally when this form was openned.  It is only needed if user decides to apply changes to all identical plans.</summary>
		private InsPlan PlanCurOld;
		private System.Windows.Forms.ComboBox comboElectIDdescript;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ComboBox comboAllowedFeeSched;
		private OpenDental.UI.Button butLabel;
		private System.Windows.Forms.GroupBox groupRequestBen;
		private System.Windows.Forms.Label labelTrojanID;
		private System.Windows.Forms.TextBox textTrojanID;
		private OpenDental.UI.Button butImportTrojan;
		private System.Windows.Forms.Label labelDivisionDash;
		private System.Windows.Forms.TextBox textDivisionNo;
		private System.Windows.Forms.Label labelElectronicID;
		private System.Windows.Forms.Label labelIAP;
		private OpenDental.UI.Button butIapFind;
		private System.Windows.Forms.Label labelTrojan;
		private OpenDental.UI.Button butBenefitNotes;
		private System.Windows.Forms.CheckBox checkIsMedical;
		private System.Windows.Forms.CheckBox checkAlternateCode;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.ComboBox comboPlanType;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.Label label36;
		private Carrier CarrierCur;
		private System.Windows.Forms.ComboBox comboRelationship;
		private System.Windows.Forms.CheckBox checkIsPending;
		private System.Windows.Forms.TextBox textPatID;
		private OpenDental.UI.Button butAdjAdd;
		private System.Windows.Forms.ListBox listAdj;
		private System.Windows.Forms.Panel panelPat;
		private PatPlan PatPlanCur;
		private ArrayList AdjAL;
		private OpenDental.ValidNumber textOrdinal;
		private OpenDental.UI.ODGrid gridBenefits;
		private OpenDental.UI.Button butAddBenefit;
		private TextBox textPlanNum;
		private OpenDental.UI.Button butClear;
		//private CovPat[] CovListForPat;
		///<summary>This is the current benefit list that displays on the form.  It does not get saved to the database until this form closes.</summary>
		private ArrayList benefitList;
		private CheckBox checkCalendarYear;
		private OpenDental.UI.Button butAnnualMax;//each item is a Benefit
		private ArrayList benefitListOld;
		private bool usesAnnivers;
		private Label label17;
		private OpenDental.UI.Button butPick;
		private CheckBox checkApplyAll;
		private ODtextBox textPlanNote;
		private Label label18;
		///<summary>Set to true if called from the list of insurance templates.  In this case, the planNum will be 0.  There will be no subscriber.  Benefits will be 'typical' rather than from one specific plan.  Upon saving, all similar plans will be set to be exactly the same as PlanCur.</summary>
		public bool IsForAll;

		///<summary>Only called from ContrFamily. Must pass in both the plan and the patPlan.  They are handled separately.</summary>
		public FormInsPlan(InsPlan planCur,PatPlan patPlanCur){
			Cursor=Cursors.WaitCursor;
			InitializeComponent();
			PlanCur=planCur;
			PatPlanCur=patPlanCur;
			listEmps=new ListBox();
			listEmps.Location=new Point(textEmployer.Left,textEmployer.Bottom);
			listEmps.Size=new Size(231,100);
			listEmps.Visible=false;
			listEmps.Click += new System.EventHandler(listEmps_Click);
			listEmps.DoubleClick += new System.EventHandler(listEmps_DoubleClick);
			listEmps.MouseEnter += new System.EventHandler(listEmps_MouseEnter);
			listEmps.MouseLeave += new System.EventHandler(listEmps_MouseLeave);
			Controls.Add(listEmps);
			listEmps.BringToFront();
			listCars=new ListBox();
			listCars.Location=new Point(textCarrier.Left,textCarrier.Bottom);
			listCars.Size=new Size(700,100);
			listCars.HorizontalScrollbar=true;
			listCars.Visible=false;
			listCars.Click += new System.EventHandler(listCars_Click);
			listCars.DoubleClick += new System.EventHandler(listCars_DoubleClick);
			listCars.MouseEnter += new System.EventHandler(listCars_MouseEnter);
			listCars.MouseLeave += new System.EventHandler(listCars_MouseLeave);
			Controls.Add(listCars);
			listCars.BringToFront();
			//tbPercentPlan.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbPercentPlan_CellClicked);
			//tbPercentPat.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbPercentPat_CellClicked);
			Lan.F(this);
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="CA"){//en-CA or fr-CA
				labelCitySTZip.Text="City,Prov,Post";   //Postal Code";
			}
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="GB"){//en-GB
				labelCitySTZip.Text="City,Postcode";//Postcode";
			}
			panelPat.BackColor=Defs.Long[(int)DefCat.MiscColors][0].ItemColor;
			Cursor=Cursors.Default;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInsPlan));
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.labelCitySTZip = new System.Windows.Forms.Label();
			this.labelElectronicID = new System.Windows.Forms.Label();
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
			this.textEmployer = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.textAddress2 = new System.Windows.Forms.TextBox();
			this.label21 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.checkAssign = new System.Windows.Forms.CheckBox();
			this.checkRelease = new System.Windows.Forms.CheckBox();
			this.checkNoSendElect = new System.Windows.Forms.CheckBox();
			this.label23 = new System.Windows.Forms.Label();
			this.checkAlternateCode = new System.Windows.Forms.CheckBox();
			this.checkClaimsUseUCR = new System.Windows.Forms.CheckBox();
			this.label14 = new System.Windows.Forms.Label();
			this.textSubscriber = new System.Windows.Forms.TextBox();
			this.groupSubscriber = new System.Windows.Forms.GroupBox();
			this.label25 = new System.Windows.Forms.Label();
			this.textSubscriberID = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textDateEffect = new OpenDental.ValidDate();
			this.textDateTerm = new OpenDental.ValidDate();
			this.textSubscNote = new OpenDental.ODtextBox();
			this.comboLinked = new System.Windows.Forms.ComboBox();
			this.textLinkedNum = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.comboPlanType = new System.Windows.Forms.ComboBox();
			this.checkIsMedical = new System.Windows.Forms.CheckBox();
			this.textDivisionNo = new System.Windows.Forms.TextBox();
			this.labelDivisionDash = new System.Windows.Forms.Label();
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
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.butBenefitNotes = new OpenDental.UI.Button();
			this.butIapFind = new OpenDental.UI.Button();
			this.butImportTrojan = new OpenDental.UI.Button();
			this.labelDrop = new System.Windows.Forms.Label();
			this.groupRequestBen = new System.Windows.Forms.GroupBox();
			this.labelTrojan = new System.Windows.Forms.Label();
			this.labelIAP = new System.Windows.Forms.Label();
			this.labelTrojanID = new System.Windows.Forms.Label();
			this.textTrojanID = new System.Windows.Forms.TextBox();
			this.label26 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.comboRelationship = new System.Windows.Forms.ComboBox();
			this.label31 = new System.Windows.Forms.Label();
			this.checkIsPending = new System.Windows.Forms.CheckBox();
			this.label32 = new System.Windows.Forms.Label();
			this.label33 = new System.Windows.Forms.Label();
			this.listAdj = new System.Windows.Forms.ListBox();
			this.label35 = new System.Windows.Forms.Label();
			this.textPatID = new System.Windows.Forms.TextBox();
			this.label36 = new System.Windows.Forms.Label();
			this.panelPat = new System.Windows.Forms.Panel();
			this.textOrdinal = new OpenDental.ValidNumber();
			this.butAdjAdd = new OpenDental.UI.Button();
			this.butDrop = new OpenDental.UI.Button();
			this.textPlanNum = new System.Windows.Forms.TextBox();
			this.checkCalendarYear = new System.Windows.Forms.CheckBox();
			this.butAnnualMax = new OpenDental.UI.Button();
			this.butClear = new OpenDental.UI.Button();
			this.butAddBenefit = new OpenDental.UI.Button();
			this.gridBenefits = new OpenDental.UI.ODGrid();
			this.butLabel = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label17 = new System.Windows.Forms.Label();
			this.butPick = new OpenDental.UI.Button();
			this.checkApplyAll = new System.Windows.Forms.CheckBox();
			this.textPlanNote = new OpenDental.ODtextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.groupSubscriber.SuspendLayout();
			this.groupCoPay.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupRequestBen.SuspendLayout();
			this.panelPat.SuspendLayout();
			this.SuspendLayout();
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(7,53);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100,15);
			this.label5.TabIndex = 5;
			this.label5.Text = "Effective Dates";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(187,53);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(36,15);
			this.label6.TabIndex = 6;
			this.label6.Text = "To";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(3,13);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(95,15);
			this.label7.TabIndex = 7;
			this.label7.Text = "Phone";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(18,322);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(95,15);
			this.label8.TabIndex = 8;
			this.label8.Text = "Group Name";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(18,342);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(95,15);
			this.label9.TabIndex = 9;
			this.label9.Text = "Group Num";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(3,32);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(95,15);
			this.label10.TabIndex = 10;
			this.label10.Text = "Address";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelCitySTZip
			// 
			this.labelCitySTZip.Location = new System.Drawing.Point(3,72);
			this.labelCitySTZip.Name = "labelCitySTZip";
			this.labelCitySTZip.Size = new System.Drawing.Size(95,15);
			this.labelCitySTZip.TabIndex = 11;
			this.labelCitySTZip.Text = "City,ST,Zip";
			this.labelCitySTZip.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelElectronicID
			// 
			this.labelElectronicID.Location = new System.Drawing.Point(2,92);
			this.labelElectronicID.Name = "labelElectronicID";
			this.labelElectronicID.Size = new System.Drawing.Size(95,15);
			this.labelElectronicID.TabIndex = 15;
			this.labelElectronicID.Text = "Electronic ID";
			this.labelElectronicID.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label28
			// 
			this.label28.Location = new System.Drawing.Point(2,74);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(55,41);
			this.label28.TabIndex = 28;
			this.label28.Text = "Note";
			this.label28.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCarrier
			// 
			this.textCarrier.Font = new System.Drawing.Font("Microsoft Sans Serif",9F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textCarrier.Location = new System.Drawing.Point(79,134);
			this.textCarrier.MaxLength = 50;
			this.textCarrier.Multiline = true;
			this.textCarrier.Name = "textCarrier";
			this.textCarrier.Size = new System.Drawing.Size(235,20);
			this.textCarrier.TabIndex = 0;
			this.textCarrier.Leave += new System.EventHandler(this.textCarrier_Leave);
			this.textCarrier.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textCarrier_KeyUp);
			// 
			// textPhone
			// 
			this.textPhone.Location = new System.Drawing.Point(100,10);
			this.textPhone.MaxLength = 30;
			this.textPhone.Name = "textPhone";
			this.textPhone.Size = new System.Drawing.Size(157,20);
			this.textPhone.TabIndex = 1;
			this.textPhone.TextChanged += new System.EventHandler(this.textPhone_TextChanged);
			// 
			// textGroupName
			// 
			this.textGroupName.Location = new System.Drawing.Point(114,319);
			this.textGroupName.MaxLength = 50;
			this.textGroupName.Name = "textGroupName";
			this.textGroupName.Size = new System.Drawing.Size(193,20);
			this.textGroupName.TabIndex = 1;
			// 
			// textGroupNum
			// 
			this.textGroupNum.Location = new System.Drawing.Point(114,339);
			this.textGroupNum.MaxLength = 20;
			this.textGroupNum.Name = "textGroupNum";
			this.textGroupNum.Size = new System.Drawing.Size(129,20);
			this.textGroupNum.TabIndex = 2;
			// 
			// textAddress
			// 
			this.textAddress.Location = new System.Drawing.Point(100,30);
			this.textAddress.MaxLength = 60;
			this.textAddress.Name = "textAddress";
			this.textAddress.Size = new System.Drawing.Size(291,20);
			this.textAddress.TabIndex = 2;
			this.textAddress.TextChanged += new System.EventHandler(this.textAddress_TextChanged);
			// 
			// textCity
			// 
			this.textCity.Location = new System.Drawing.Point(100,70);
			this.textCity.MaxLength = 40;
			this.textCity.Name = "textCity";
			this.textCity.Size = new System.Drawing.Size(155,20);
			this.textCity.TabIndex = 4;
			this.textCity.TextChanged += new System.EventHandler(this.textCity_TextChanged);
			// 
			// textState
			// 
			this.textState.Location = new System.Drawing.Point(255,70);
			this.textState.MaxLength = 2;
			this.textState.Name = "textState";
			this.textState.Size = new System.Drawing.Size(65,20);
			this.textState.TabIndex = 5;
			this.textState.TextChanged += new System.EventHandler(this.textState_TextChanged);
			// 
			// textZip
			// 
			this.textZip.Location = new System.Drawing.Point(320,70);
			this.textZip.MaxLength = 10;
			this.textZip.Name = "textZip";
			this.textZip.Size = new System.Drawing.Size(71,20);
			this.textZip.TabIndex = 6;
			// 
			// textElectID
			// 
			this.textElectID.Location = new System.Drawing.Point(100,90);
			this.textElectID.MaxLength = 5;
			this.textElectID.Name = "textElectID";
			this.textElectID.Size = new System.Drawing.Size(54,20);
			this.textElectID.TabIndex = 7;
			this.textElectID.Validating += new System.ComponentModel.CancelEventHandler(this.textElectID_Validating);
			// 
			// textEmployer
			// 
			this.textEmployer.Location = new System.Drawing.Point(79,114);
			this.textEmployer.MaxLength = 40;
			this.textEmployer.Name = "textEmployer";
			this.textEmployer.Size = new System.Drawing.Size(235,20);
			this.textEmployer.TabIndex = 0;
			this.textEmployer.Leave += new System.EventHandler(this.textEmployer_Leave);
			this.textEmployer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textEmployer_KeyUp);
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(0,116);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(78,15);
			this.label16.TabIndex = 73;
			this.label16.Text = "Employer";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textAddress2
			// 
			this.textAddress2.Location = new System.Drawing.Point(100,50);
			this.textAddress2.MaxLength = 60;
			this.textAddress2.Name = "textAddress2";
			this.textAddress2.Size = new System.Drawing.Size(291,20);
			this.textAddress2.TabIndex = 3;
			this.textAddress2.TextChanged += new System.EventHandler(this.textAddress2_TextChanged);
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(3,53);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(95,15);
			this.label21.TabIndex = 79;
			this.label21.Text = "Address 2";
			this.label21.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(17,429);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96,15);
			this.label1.TabIndex = 91;
			this.label1.Text = "Fee Schedule";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkAssign
			// 
			this.checkAssign.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAssign.Location = new System.Drawing.Point(340,50);
			this.checkAssign.Name = "checkAssign";
			this.checkAssign.Size = new System.Drawing.Size(198,20);
			this.checkAssign.TabIndex = 1;
			this.checkAssign.Text = "Assignment of Benefits";
			// 
			// checkRelease
			// 
			this.checkRelease.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkRelease.Location = new System.Drawing.Point(340,32);
			this.checkRelease.Name = "checkRelease";
			this.checkRelease.Size = new System.Drawing.Size(198,20);
			this.checkRelease.TabIndex = 0;
			this.checkRelease.Text = "Release of Information";
			// 
			// checkNoSendElect
			// 
			this.checkNoSendElect.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNoSendElect.Location = new System.Drawing.Point(176,114);
			this.checkNoSendElect.Name = "checkNoSendElect";
			this.checkNoSendElect.Size = new System.Drawing.Size(213,17);
			this.checkNoSendElect.TabIndex = 8;
			this.checkNoSendElect.Text = "Don\'t Usually Send Electronically";
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(16,453);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(95,14);
			this.label23.TabIndex = 96;
			this.label23.Text = "Claim Form";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkAlternateCode
			// 
			this.checkAlternateCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAlternateCode.Location = new System.Drawing.Point(114,381);
			this.checkAlternateCode.Name = "checkAlternateCode";
			this.checkAlternateCode.Size = new System.Drawing.Size(286,17);
			this.checkAlternateCode.TabIndex = 5;
			this.checkAlternateCode.Text = "Use Alternate Code (for some Medicaid plans)";
			// 
			// checkClaimsUseUCR
			// 
			this.checkClaimsUseUCR.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkClaimsUseUCR.Location = new System.Drawing.Point(114,411);
			this.checkClaimsUseUCR.Name = "checkClaimsUseUCR";
			this.checkClaimsUseUCR.Size = new System.Drawing.Size(302,17);
			this.checkClaimsUseUCR.TabIndex = 6;
			this.checkClaimsUseUCR.Text = "Claims show UCR fee, not billed fee";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(19,360);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(95,15);
			this.label14.TabIndex = 104;
			this.label14.Text = "Plan Type";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textSubscriber
			// 
			this.textSubscriber.Location = new System.Drawing.Point(109,10);
			this.textSubscriber.Name = "textSubscriber";
			this.textSubscriber.ReadOnly = true;
			this.textSubscriber.Size = new System.Drawing.Size(278,20);
			this.textSubscriber.TabIndex = 109;
			// 
			// groupSubscriber
			// 
			this.groupSubscriber.Controls.Add(this.checkAssign);
			this.groupSubscriber.Controls.Add(this.label25);
			this.groupSubscriber.Controls.Add(this.checkRelease);
			this.groupSubscriber.Controls.Add(this.textSubscriber);
			this.groupSubscriber.Controls.Add(this.textSubscriberID);
			this.groupSubscriber.Controls.Add(this.label2);
			this.groupSubscriber.Controls.Add(this.textDateEffect);
			this.groupSubscriber.Controls.Add(this.label5);
			this.groupSubscriber.Controls.Add(this.textDateTerm);
			this.groupSubscriber.Controls.Add(this.label6);
			this.groupSubscriber.Controls.Add(this.textSubscNote);
			this.groupSubscriber.Controls.Add(this.label28);
			this.groupSubscriber.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupSubscriber.Location = new System.Drawing.Point(423,93);
			this.groupSubscriber.Name = "groupSubscriber";
			this.groupSubscriber.Size = new System.Drawing.Size(547,175);
			this.groupSubscriber.TabIndex = 0;
			this.groupSubscriber.TabStop = false;
			this.groupSubscriber.Text = "Subscriber";
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(8,14);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(99,15);
			this.label25.TabIndex = 115;
			this.label25.Text = "Name";
			this.label25.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textSubscriberID
			// 
			this.textSubscriberID.Location = new System.Drawing.Point(109,30);
			this.textSubscriberID.MaxLength = 20;
			this.textSubscriberID.Name = "textSubscriberID";
			this.textSubscriberID.Size = new System.Drawing.Size(129,20);
			this.textSubscriberID.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8,32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(99,15);
			this.label2.TabIndex = 114;
			this.label2.Text = "Subscriber ID";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDateEffect
			// 
			this.textDateEffect.Location = new System.Drawing.Point(109,50);
			this.textDateEffect.Name = "textDateEffect";
			this.textDateEffect.Size = new System.Drawing.Size(72,20);
			this.textDateEffect.TabIndex = 1;
			// 
			// textDateTerm
			// 
			this.textDateTerm.Location = new System.Drawing.Point(227,50);
			this.textDateTerm.Name = "textDateTerm";
			this.textDateTerm.Size = new System.Drawing.Size(72,20);
			this.textDateTerm.TabIndex = 2;
			// 
			// textSubscNote
			// 
			this.textSubscNote.AcceptsReturn = true;
			this.textSubscNote.Location = new System.Drawing.Point(57,71);
			this.textSubscNote.Multiline = true;
			this.textSubscNote.Name = "textSubscNote";
			this.textSubscNote.QuickPasteType = OpenDental.QuickPasteType.InsPlan;
			this.textSubscNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textSubscNote.Size = new System.Drawing.Size(485,98);
			this.textSubscNote.TabIndex = 29;
			this.textSubscNote.Text = "1\r\n2\r\n3 lines will show here in 46 vert.\r\n4 lines will show here in 59 vert.\r\n5 l" +
    "ines in 72 vert\r\n6 lines in 85 vert\r\n7 lines in 98";
			// 
			// comboLinked
			// 
			this.comboLinked.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboLinked.Location = new System.Drawing.Point(149,155);
			this.comboLinked.MaxDropDownItems = 30;
			this.comboLinked.Name = "comboLinked";
			this.comboLinked.Size = new System.Drawing.Size(256,21);
			this.comboLinked.TabIndex = 68;
			// 
			// textLinkedNum
			// 
			this.textLinkedNum.BackColor = System.Drawing.Color.White;
			this.textLinkedNum.Location = new System.Drawing.Point(114,155);
			this.textLinkedNum.Multiline = true;
			this.textLinkedNum.Name = "textLinkedNum";
			this.textLinkedNum.ReadOnly = true;
			this.textLinkedNum.Size = new System.Drawing.Size(35,21);
			this.textLinkedNum.TabIndex = 67;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(0,157);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(114,17);
			this.label4.TabIndex = 66;
			this.label4.Text = "Identical Plans";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboPlanType
			// 
			this.comboPlanType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPlanType.Location = new System.Drawing.Point(114,359);
			this.comboPlanType.Name = "comboPlanType";
			this.comboPlanType.Size = new System.Drawing.Size(212,21);
			this.comboPlanType.TabIndex = 128;
			this.comboPlanType.SelectionChangeCommitted += new System.EventHandler(this.comboPlanType_SelectionChangeCommitted);
			// 
			// checkIsMedical
			// 
			this.checkIsMedical.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsMedical.Location = new System.Drawing.Point(114,396);
			this.checkIsMedical.Name = "checkIsMedical";
			this.checkIsMedical.Size = new System.Drawing.Size(286,17);
			this.checkIsMedical.TabIndex = 113;
			this.checkIsMedical.Text = "Medical Insurance";
			// 
			// textDivisionNo
			// 
			this.textDivisionNo.Location = new System.Drawing.Point(261,339);
			this.textDivisionNo.MaxLength = 20;
			this.textDivisionNo.Name = "textDivisionNo";
			this.textDivisionNo.Size = new System.Drawing.Size(107,20);
			this.textDivisionNo.TabIndex = 112;
			// 
			// labelDivisionDash
			// 
			this.labelDivisionDash.Location = new System.Drawing.Point(246,343);
			this.labelDivisionDash.Name = "labelDivisionDash";
			this.labelDivisionDash.Size = new System.Drawing.Size(31,16);
			this.labelDivisionDash.TabIndex = 111;
			this.labelDivisionDash.Text = "--";
			// 
			// comboClaimForm
			// 
			this.comboClaimForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClaimForm.Location = new System.Drawing.Point(114,450);
			this.comboClaimForm.MaxDropDownItems = 30;
			this.comboClaimForm.Name = "comboClaimForm";
			this.comboClaimForm.Size = new System.Drawing.Size(212,21);
			this.comboClaimForm.TabIndex = 110;
			// 
			// comboFeeSched
			// 
			this.comboFeeSched.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboFeeSched.Location = new System.Drawing.Point(114,428);
			this.comboFeeSched.MaxDropDownItems = 30;
			this.comboFeeSched.Name = "comboFeeSched";
			this.comboFeeSched.Size = new System.Drawing.Size(212,21);
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
			this.groupCoPay.Location = new System.Drawing.Point(14,476);
			this.groupCoPay.Name = "groupCoPay";
			this.groupCoPay.Size = new System.Drawing.Size(397,85);
			this.groupCoPay.TabIndex = 107;
			this.groupCoPay.TabStop = false;
			this.groupCoPay.Text = "Other Fee Schedules";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(6,63);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(168,16);
			this.label12.TabIndex = 111;
			this.label12.Text = "Carrier Allowed Amounts";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboAllowedFeeSched
			// 
			this.comboAllowedFeeSched.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboAllowedFeeSched.Location = new System.Drawing.Point(176,60);
			this.comboAllowedFeeSched.MaxDropDownItems = 30;
			this.comboAllowedFeeSched.Name = "comboAllowedFeeSched";
			this.comboAllowedFeeSched.Size = new System.Drawing.Size(215,21);
			this.comboAllowedFeeSched.TabIndex = 110;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(6,41);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(168,16);
			this.label11.TabIndex = 109;
			this.label11.Text = "Patient Co-pay Amounts";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(1,19);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(390,17);
			this.label3.TabIndex = 106;
			this.label3.Text = "Don\'t use these unless you understand how they will affect your estimates";
			// 
			// comboCopay
			// 
			this.comboCopay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboCopay.Location = new System.Drawing.Point(176,38);
			this.comboCopay.MaxDropDownItems = 30;
			this.comboCopay.Name = "comboCopay";
			this.comboCopay.Size = new System.Drawing.Size(215,21);
			this.comboCopay.TabIndex = 108;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.comboElectIDdescript);
			this.groupBox3.Controls.Add(this.textAddress);
			this.groupBox3.Controls.Add(this.textCity);
			this.groupBox3.Controls.Add(this.textState);
			this.groupBox3.Controls.Add(this.label21);
			this.groupBox3.Controls.Add(this.label10);
			this.groupBox3.Controls.Add(this.textAddress2);
			this.groupBox3.Controls.Add(this.textElectID);
			this.groupBox3.Controls.Add(this.textZip);
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Controls.Add(this.labelCitySTZip);
			this.groupBox3.Controls.Add(this.labelElectronicID);
			this.groupBox3.Controls.Add(this.checkNoSendElect);
			this.groupBox3.Controls.Add(this.textPhone);
			this.groupBox3.Controls.Add(this.butSearch);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(14,179);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(397,135);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Carrier Info";
			// 
			// comboElectIDdescript
			// 
			this.comboElectIDdescript.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboElectIDdescript.Location = new System.Drawing.Point(154,90);
			this.comboElectIDdescript.MaxDropDownItems = 30;
			this.comboElectIDdescript.Name = "comboElectIDdescript";
			this.comboElectIDdescript.Size = new System.Drawing.Size(237,21);
			this.comboElectIDdescript.TabIndex = 125;
			this.comboElectIDdescript.SelectedIndexChanged += new System.EventHandler(this.comboElectIDdescript_SelectedIndexChanged);
			// 
			// butSearch
			// 
			this.butSearch.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butSearch.Autosize = true;
			this.butSearch.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSearch.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSearch.Location = new System.Drawing.Point(73,111);
			this.butSearch.Name = "butSearch";
			this.butSearch.Size = new System.Drawing.Size(84,23);
			this.butSearch.TabIndex = 124;
			this.butSearch.Text = "Search IDs";
			this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
			// 
			// butBenefitNotes
			// 
			this.butBenefitNotes.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBenefitNotes.Autosize = true;
			this.butBenefitNotes.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBenefitNotes.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBenefitNotes.Location = new System.Drawing.Point(378,22);
			this.butBenefitNotes.Name = "butBenefitNotes";
			this.butBenefitNotes.Size = new System.Drawing.Size(82,21);
			this.butBenefitNotes.TabIndex = 76;
			this.butBenefitNotes.Text = "View Benefits";
			this.toolTip1.SetToolTip(this.butBenefitNotes,"Edit all the similar plans at once");
			this.butBenefitNotes.Click += new System.EventHandler(this.butBenefitNotes_Click);
			// 
			// butIapFind
			// 
			this.butIapFind.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butIapFind.Autosize = true;
			this.butIapFind.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butIapFind.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butIapFind.Location = new System.Drawing.Point(132,33);
			this.butIapFind.Name = "butIapFind";
			this.butIapFind.Size = new System.Drawing.Size(82,21);
			this.butIapFind.TabIndex = 74;
			this.butIapFind.Text = "Find Plan";
			this.toolTip1.SetToolTip(this.butIapFind,"Edit all the similar plans at once");
			this.butIapFind.Click += new System.EventHandler(this.butIapFind_Click);
			// 
			// butImportTrojan
			// 
			this.butImportTrojan.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butImportTrojan.Autosize = true;
			this.butImportTrojan.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butImportTrojan.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butImportTrojan.Location = new System.Drawing.Point(132,10);
			this.butImportTrojan.Name = "butImportTrojan";
			this.butImportTrojan.Size = new System.Drawing.Size(82,21);
			this.butImportTrojan.TabIndex = 72;
			this.butImportTrojan.Text = "Import";
			this.toolTip1.SetToolTip(this.butImportTrojan,"Edit all the similar plans at once");
			this.butImportTrojan.Click += new System.EventHandler(this.butImportTrojan_Click);
			// 
			// labelDrop
			// 
			this.labelDrop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelDrop.Location = new System.Drawing.Point(101,72);
			this.labelDrop.Name = "labelDrop";
			this.labelDrop.Size = new System.Drawing.Size(554,15);
			this.labelDrop.TabIndex = 124;
			this.labelDrop.Text = "Drop a plan when a patient changes carriers or is no longer covered.  This does n" +
    "ot delete the plan.";
			this.labelDrop.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// groupRequestBen
			// 
			this.groupRequestBen.Controls.Add(this.butBenefitNotes);
			this.groupRequestBen.Controls.Add(this.labelTrojan);
			this.groupRequestBen.Controls.Add(this.butIapFind);
			this.groupRequestBen.Controls.Add(this.labelIAP);
			this.groupRequestBen.Controls.Add(this.butImportTrojan);
			this.groupRequestBen.Controls.Add(this.labelTrojanID);
			this.groupRequestBen.Controls.Add(this.textTrojanID);
			this.groupRequestBen.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupRequestBen.Location = new System.Drawing.Point(423,271);
			this.groupRequestBen.Name = "groupRequestBen";
			this.groupRequestBen.Size = new System.Drawing.Size(547,59);
			this.groupRequestBen.TabIndex = 126;
			this.groupRequestBen.TabStop = false;
			this.groupRequestBen.Text = "Request Benefits";
			// 
			// labelTrojan
			// 
			this.labelTrojan.Location = new System.Drawing.Point(59,15);
			this.labelTrojan.Name = "labelTrojan";
			this.labelTrojan.Size = new System.Drawing.Size(71,15);
			this.labelTrojan.TabIndex = 75;
			this.labelTrojan.Text = "Trojan:";
			this.labelTrojan.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelIAP
			// 
			this.labelIAP.Location = new System.Drawing.Point(2,36);
			this.labelIAP.Name = "labelIAP";
			this.labelIAP.Size = new System.Drawing.Size(128,15);
			this.labelIAP.TabIndex = 73;
			this.labelIAP.Text = "Insurance Answers Plus:";
			this.labelIAP.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelTrojanID
			// 
			this.labelTrojanID.Location = new System.Drawing.Point(218,14);
			this.labelTrojanID.Name = "labelTrojanID";
			this.labelTrojanID.Size = new System.Drawing.Size(23,15);
			this.labelTrojanID.TabIndex = 9;
			this.labelTrojanID.Text = "ID";
			this.labelTrojanID.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textTrojanID
			// 
			this.textTrojanID.Location = new System.Drawing.Point(245,10);
			this.textTrojanID.MaxLength = 30;
			this.textTrojanID.Name = "textTrojanID";
			this.textTrojanID.Size = new System.Drawing.Size(109,20);
			this.textTrojanID.TabIndex = 8;
			// 
			// label26
			// 
			this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label26.Location = new System.Drawing.Point(20,22);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(148,14);
			this.label26.TabIndex = 127;
			this.label26.Text = "Relationship to Subscriber";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ControlText;
			this.panel1.Location = new System.Drawing.Point(0,90);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(988,2);
			this.panel1.TabIndex = 128;
			// 
			// comboRelationship
			// 
			this.comboRelationship.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboRelationship.Location = new System.Drawing.Point(170,18);
			this.comboRelationship.MaxDropDownItems = 30;
			this.comboRelationship.Name = "comboRelationship";
			this.comboRelationship.Size = new System.Drawing.Size(151,21);
			this.comboRelationship.TabIndex = 129;
			// 
			// label31
			// 
			this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label31.Location = new System.Drawing.Point(396,23);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(109,14);
			this.label31.TabIndex = 130;
			this.label31.Text = "Order";
			this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkIsPending
			// 
			this.checkIsPending.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsPending.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsPending.Location = new System.Drawing.Point(396,42);
			this.checkIsPending.Name = "checkIsPending";
			this.checkIsPending.Size = new System.Drawing.Size(125,16);
			this.checkIsPending.TabIndex = 133;
			this.checkIsPending.Text = "Pending";
			this.checkIsPending.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label32
			// 
			this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif",10F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label32.Location = new System.Drawing.Point(5,95);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(304,19);
			this.label32.TabIndex = 134;
			this.label32.Text = "Insurance Plan Information";
			// 
			// label33
			// 
			this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif",10F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label33.Location = new System.Drawing.Point(5,0);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(188,19);
			this.label33.TabIndex = 135;
			this.label33.Text = "Patient Information";
			// 
			// listAdj
			// 
			this.listAdj.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.listAdj.Items.AddRange(new object[] {
            "03/05/2001       Ins Used:  $124.00       Ded Used:  $50.00",
            "03/05/2002       Ins Used:  $0.00       Ded Used:  $50.00"});
			this.listAdj.Location = new System.Drawing.Point(613,28);
			this.listAdj.Name = "listAdj";
			this.listAdj.Size = new System.Drawing.Size(341,56);
			this.listAdj.TabIndex = 137;
			this.listAdj.DoubleClick += new System.EventHandler(this.listAdj_DoubleClick);
			// 
			// label35
			// 
			this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label35.Location = new System.Drawing.Point(613,8);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(218,17);
			this.label35.TabIndex = 138;
			this.label35.Text = "Adjustments to Insurance Benefits: ";
			this.label35.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textPatID
			// 
			this.textPatID.Font = new System.Drawing.Font("Microsoft Sans Serif",8F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textPatID.Location = new System.Drawing.Point(170,40);
			this.textPatID.MaxLength = 100;
			this.textPatID.Name = "textPatID";
			this.textPatID.Size = new System.Drawing.Size(151,20);
			this.textPatID.TabIndex = 144;
			// 
			// label36
			// 
			this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif",8F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label36.Location = new System.Drawing.Point(30,42);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(138,16);
			this.label36.TabIndex = 143;
			this.label36.Text = "Optional Patient ID";
			this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panelPat
			// 
			this.panelPat.Controls.Add(this.comboRelationship);
			this.panelPat.Controls.Add(this.label33);
			this.panelPat.Controls.Add(this.textOrdinal);
			this.panelPat.Controls.Add(this.butAdjAdd);
			this.panelPat.Controls.Add(this.listAdj);
			this.panelPat.Controls.Add(this.label35);
			this.panelPat.Controls.Add(this.textPatID);
			this.panelPat.Controls.Add(this.label36);
			this.panelPat.Controls.Add(this.labelDrop);
			this.panelPat.Controls.Add(this.butDrop);
			this.panelPat.Controls.Add(this.label26);
			this.panelPat.Controls.Add(this.label31);
			this.panelPat.Controls.Add(this.checkIsPending);
			this.panelPat.Location = new System.Drawing.Point(0,0);
			this.panelPat.Name = "panelPat";
			this.panelPat.Size = new System.Drawing.Size(982,90);
			this.panelPat.TabIndex = 145;
			// 
			// textOrdinal
			// 
			this.textOrdinal.Location = new System.Drawing.Point(508,22);
			this.textOrdinal.MaxVal = 10;
			this.textOrdinal.MinVal = 1;
			this.textOrdinal.Name = "textOrdinal";
			this.textOrdinal.Size = new System.Drawing.Size(45,20);
			this.textOrdinal.TabIndex = 145;
			// 
			// butAdjAdd
			// 
			this.butAdjAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdjAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdjAdd.Autosize = true;
			this.butAdjAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdjAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdjAdd.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butAdjAdd.Location = new System.Drawing.Point(895,6);
			this.butAdjAdd.Name = "butAdjAdd";
			this.butAdjAdd.Size = new System.Drawing.Size(59,21);
			this.butAdjAdd.TabIndex = 139;
			this.butAdjAdd.Text = "Add";
			this.butAdjAdd.Click += new System.EventHandler(this.butAdjAdd_Click);
			// 
			// butDrop
			// 
			this.butDrop.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDrop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDrop.Autosize = true;
			this.butDrop.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDrop.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDrop.Location = new System.Drawing.Point(7,67);
			this.butDrop.Name = "butDrop";
			this.butDrop.Size = new System.Drawing.Size(72,21);
			this.butDrop.TabIndex = 123;
			this.butDrop.Text = "Drop";
			this.butDrop.Click += new System.EventHandler(this.butDrop_Click);
			// 
			// textPlanNum
			// 
			this.textPlanNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textPlanNum.Location = new System.Drawing.Point(310,676);
			this.textPlanNum.Name = "textPlanNum";
			this.textPlanNum.Size = new System.Drawing.Size(100,20);
			this.textPlanNum.TabIndex = 148;
			// 
			// checkCalendarYear
			// 
			this.checkCalendarYear.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkCalendarYear.Location = new System.Drawing.Point(787,639);
			this.checkCalendarYear.Name = "checkCalendarYear";
			this.checkCalendarYear.Size = new System.Drawing.Size(121,21);
			this.checkCalendarYear.TabIndex = 150;
			this.checkCalendarYear.Text = "Calendar Year";
			this.checkCalendarYear.ThreeState = true;
			this.checkCalendarYear.UseVisualStyleBackColor = true;
			this.checkCalendarYear.Click += new System.EventHandler(this.checkCalendarYear_Click);
			// 
			// butAnnualMax
			// 
			this.butAnnualMax.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAnnualMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAnnualMax.Autosize = true;
			this.butAnnualMax.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAnnualMax.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAnnualMax.Image = ((System.Drawing.Image)(resources.GetObject("butAnnualMax.Image")));
			this.butAnnualMax.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAnnualMax.Location = new System.Drawing.Point(423,642);
			this.butAnnualMax.Name = "butAnnualMax";
			this.butAnnualMax.Size = new System.Drawing.Size(120,26);
			this.butAnnualMax.TabIndex = 151;
			this.butAnnualMax.Text = "Add Annual Max";
			this.butAnnualMax.Click += new System.EventHandler(this.butAnnualMax_Click);
			// 
			// butClear
			// 
			this.butClear.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butClear.Autosize = true;
			this.butClear.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClear.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClear.Image = ((System.Drawing.Image)(resources.GetObject("butClear.Image")));
			this.butClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butClear.Location = new System.Drawing.Point(663,642);
			this.butClear.Name = "butClear";
			this.butClear.Size = new System.Drawing.Size(114,26);
			this.butClear.TabIndex = 149;
			this.butClear.Text = "Clear Benefits";
			this.butClear.Click += new System.EventHandler(this.butClear_Click);
			// 
			// butAddBenefit
			// 
			this.butAddBenefit.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddBenefit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAddBenefit.Autosize = true;
			this.butAddBenefit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddBenefit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddBenefit.Image = ((System.Drawing.Image)(resources.GetObject("butAddBenefit.Image")));
			this.butAddBenefit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddBenefit.Location = new System.Drawing.Point(554,642);
			this.butAddBenefit.Name = "butAddBenefit";
			this.butAddBenefit.Size = new System.Drawing.Size(90,26);
			this.butAddBenefit.TabIndex = 147;
			this.butAddBenefit.Text = "Add Benefit";
			this.butAddBenefit.Click += new System.EventHandler(this.butAddBenefit_Click);
			// 
			// gridBenefits
			// 
			this.gridBenefits.HScrollVisible = false;
			this.gridBenefits.Location = new System.Drawing.Point(423,334);
			this.gridBenefits.Name = "gridBenefits";
			this.gridBenefits.ScrollValue = 0;
			this.gridBenefits.Size = new System.Drawing.Size(547,305);
			this.gridBenefits.TabIndex = 146;
			this.gridBenefits.Title = "Benefit Information";
			this.gridBenefits.TranslationName = "TableBenefits";
			this.gridBenefits.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridBenefits_CellDoubleClick);
			// 
			// butLabel
			// 
			this.butLabel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butLabel.Autosize = true;
			this.butLabel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLabel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLabel.Image = ((System.Drawing.Image)(resources.GetObject("butLabel.Image")));
			this.butLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butLabel.Location = new System.Drawing.Point(131,671);
			this.butLabel.Name = "butLabel";
			this.butLabel.Size = new System.Drawing.Size(81,26);
			this.butLabel.TabIndex = 125;
			this.butLabel.Text = "Label";
			this.butLabel.Click += new System.EventHandler(this.butLabel_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(13,671);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(81,26);
			this.butDelete.TabIndex = 112;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(896,671);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(801,671);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(0,136);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(78,15);
			this.label17.TabIndex = 152;
			this.label17.Text = "Carrier";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butPick
			// 
			this.butPick.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butPick.Autosize = true;
			this.butPick.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPick.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPick.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPick.Location = new System.Drawing.Point(315,122);
			this.butPick.Name = "butPick";
			this.butPick.Size = new System.Drawing.Size(90,24);
			this.butPick.TabIndex = 153;
			this.butPick.Text = "Pick From List";
			this.butPick.Click += new System.EventHandler(this.butPick_Click);
			// 
			// checkApplyAll
			// 
			this.checkApplyAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkApplyAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkApplyAll.Location = new System.Drawing.Point(468,679);
			this.checkApplyAll.Name = "checkApplyAll";
			this.checkApplyAll.Size = new System.Drawing.Size(323,21);
			this.checkApplyAll.TabIndex = 154;
			this.checkApplyAll.Text = "Apply changes to all identical insurance plans";
			this.checkApplyAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkApplyAll.UseVisualStyleBackColor = true;
			// 
			// textPlanNote
			// 
			this.textPlanNote.AcceptsReturn = true;
			this.textPlanNote.Location = new System.Drawing.Point(14,583);
			this.textPlanNote.Multiline = true;
			this.textPlanNote.Name = "textPlanNote";
			this.textPlanNote.QuickPasteType = OpenDental.QuickPasteType.InsPlan;
			this.textPlanNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textPlanNote.Size = new System.Drawing.Size(397,85);
			this.textPlanNote.TabIndex = 157;
			this.textPlanNote.Text = "1\r\n2\r\n3 lines will show here in 46 vert.\r\n4 lines will show here in 59 vert.\r\n5 l" +
    "ines in 72 vert\r\n6 in 85";
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(11,565);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(399,15);
			this.label18.TabIndex = 156;
			this.label18.Text = "Plan Note.  Always applies to all similar plans, not just this one.";
			this.label18.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormInsPlan
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(982,700);
			this.Controls.Add(this.textPlanNote);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.checkApplyAll);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butPick);
			this.Controls.Add(this.textCarrier);
			this.Controls.Add(this.textEmployer);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.comboLinked);
			this.Controls.Add(this.textLinkedNum);
			this.Controls.Add(this.butAnnualMax);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.checkCalendarYear);
			this.Controls.Add(this.butClear);
			this.Controls.Add(this.comboPlanType);
			this.Controls.Add(this.checkIsMedical);
			this.Controls.Add(this.textDivisionNo);
			this.Controls.Add(this.labelDivisionDash);
			this.Controls.Add(this.textPlanNum);
			this.Controls.Add(this.comboClaimForm);
			this.Controls.Add(this.butAddBenefit);
			this.Controls.Add(this.comboFeeSched);
			this.Controls.Add(this.gridBenefits);
			this.Controls.Add(this.groupCoPay);
			this.Controls.Add(this.panelPat);
			this.Controls.Add(this.textGroupNum);
			this.Controls.Add(this.butLabel);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.textGroupName);
			this.Controls.Add(this.label32);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.groupRequestBen);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label23);
			this.Controls.Add(this.groupSubscriber);
			this.Controls.Add(this.checkClaimsUseUCR);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.checkAlternateCode);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormInsPlan";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Insurance Plan";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormInsPlan_Closing);
			this.Load += new System.EventHandler(this.FormInsPlan_Load);
			this.groupSubscriber.ResumeLayout(false);
			this.groupSubscriber.PerformLayout();
			this.groupCoPay.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupRequestBen.ResumeLayout(false);
			this.groupRequestBen.PerformLayout();
			this.panelPat.ResumeLayout(false);
			this.panelPat.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormInsPlan_Load(object sender, System.EventArgs e) {
			Cursor=Cursors.WaitCursor;
			PlanCurOld=PlanCur.Copy();
			int patPlanNum=0;
			if(PatPlanCur!=null) {
				patPlanNum=PatPlanCur.PatPlanNum;
			}
			if(IsForAll){
				butPick.Visible=false;//This prevents an infinite loop
				groupRequestBen.Visible=false;//might try to make this functional later, but not now.
				groupSubscriber.Visible=false;
				checkApplyAll.Checked=true;
				checkApplyAll.Visible=false;
				benefitList=Benefits.RefreshForAll(PlanCur);
			}
			else{
				benefitList=Benefits.RefreshForPlan(PlanCur.PlanNum,patPlanNum);
			}
			benefitListOld=new ArrayList();
			for(int i=0;i<benefitList.Count;i++){
				benefitListOld.Add(((Benefit)benefitList[i]).Copy());
			}
			#if DEBUG
				textPlanNum.Text=PlanCur.PlanNum.ToString();
			#else
				textPlanNum.Visible=false;
			#endif
			if(((Pref)Prefs.HList["EasyHideCapitation"]).ValueString=="1"){
				//groupCoPay.Visible=false;
				//comboCopay.Visible=false;
			}
			if(((Pref)Prefs.HList["EasyHideMedicaid"]).ValueString=="1"){
				checkAlternateCode.Visible=false;
			}
			if(((Pref)Prefs.HList["EasyHideAdvancedIns"]).ValueString=="1"){
				//textOrthoMax.Visible=false;
				//labelOrthoMax.Visible=false;
				//panelAdvancedIns.Visible=false;
				//panelSynch.Visible=false;
			}
			if(Prefs.GetBool("InsurancePlansShared")){
				checkApplyAll.Checked=true;
			}
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="CA"){//en-CA or fr-CA
				labelCitySTZip.Text="City,Prov,Post";
				labelElectronicID.Text="EDI Code";
			}
			else{
				labelDivisionDash.Visible=false;
				textDivisionNo.Visible=false;
			}
			Programs.GetCur("Trojan");
			if(Programs.Cur.Enabled){
				textTrojanID.Text=PlanCur.TrojanID;
			}
			else{
				labelTrojan.Visible=false;
				labelTrojanID.Visible=false;
				butImportTrojan.Visible=false;
				textTrojanID.Visible=false;
			}
			Programs.GetCur("IAP");
			if(!Programs.Cur.Enabled){
				labelIAP.Visible=false;
				butIapFind.Visible=false;
			}
			if(!butIapFind.Visible && !butImportTrojan.Visible){
				butBenefitNotes.Visible=false;
			}
			//FillPatData------------------------------
			if(PatPlanCur==null) {
				panelPat.Visible=false;
			}
			else{
				textOrdinal.Text=PatPlanCur.Ordinal.ToString();
				checkIsPending.Checked=PatPlanCur.IsPending;
				comboRelationship.Items.Clear();
				for(int i=0;i<Enum.GetNames(typeof(Relat)).Length;i++) {
					comboRelationship.Items.Add(Lan.g("enumRelat",Enum.GetNames(typeof(Relat))[i]));
					if((int)PatPlanCur.Relationship==i) {
						comboRelationship.SelectedIndex=i;
					}
				}
				textPatID.Text=PatPlanCur.PatID;
				FillPatientAdjustments();
			}
			if(!IsForAll){
				textSubscriber.Text=Patients.GetLim(PlanCur.Subscriber).GetNameLF();
				textSubscriberID.Text=PlanCur.SubscriberID;
				if(PlanCur.DateEffective.Year < 1880)
					textDateEffect.Text="";
				else
					textDateEffect.Text=PlanCur.DateEffective.ToString("d");
				if(PlanCur.DateTerm.Year < 1880)
					textDateTerm.Text="";
				else
					textDateTerm.Text=PlanCur.DateTerm.ToString("d");
			}
			FillFormWithPlanCur();
			FillBenefits();
			Cursor=Cursors.Default;
		}

		///<summary>Uses PlanCur to fill out the information on the form.  Called once on startup and also if user picks a plan from template list.</summary>
		private void FillFormWithPlanCur(){
			Cursor=Cursors.WaitCursor;
			textEmployer.Text=Employers.GetName(PlanCur.EmployerNum);
			textGroupName.Text=PlanCur.GroupName;
			textGroupNum.Text=PlanCur.GroupNum;
			textDivisionNo.Text=PlanCur.DivisionNo;//only visible in Canada
			comboPlanType.Items.Clear();
			comboPlanType.Items.Add(Lan.g(this,"Category Percentage"));
			if(PlanCur.PlanType=="")
				comboPlanType.SelectedIndex=0;
			comboPlanType.Items.Add(Lan.g(this,"Medicaid or Flat Co-pay"));
			if(PlanCur.PlanType=="f")
				comboPlanType.SelectedIndex=1;
			if(Prefs.GetBool("EasyHideCapitation")) {
				comboPlanType.Items.Add(Lan.g(this,"Capitation"));
				if(PlanCur.PlanType=="c")
					comboPlanType.SelectedIndex=2;
			}
			checkAlternateCode.Checked=PlanCur.UseAltCode;
			checkIsMedical.Checked=PlanCur.IsMedical;
			checkClaimsUseUCR.Checked=PlanCur.ClaimsUseUCR;
			comboFeeSched.Items.Clear();
			comboFeeSched.Items.Add(Lan.g(this,"none"));
			comboFeeSched.SelectedIndex=0;
			for(int i=0;i<Defs.Short[(int)DefCat.FeeSchedNames].Length;i++) {
				comboFeeSched.Items.Add(Defs.Short[(int)DefCat.FeeSchedNames][i].ItemName);
				if(Defs.Short[(int)DefCat.FeeSchedNames][i].DefNum==PlanCur.FeeSched)
					comboFeeSched.SelectedIndex=i+1;
			}
			comboCopay.Items.Clear();
			comboCopay.Items.Add(Lan.g(this,"none"));
			comboCopay.SelectedIndex=0;
			for(int i=0;i<Defs.Short[(int)DefCat.FeeSchedNames].Length;i++) {
				comboCopay.Items.Add(Defs.Short[(int)DefCat.FeeSchedNames][i].ItemName);
				if(Defs.Short[(int)DefCat.FeeSchedNames][i].DefNum==PlanCur.CopayFeeSched)
					comboCopay.SelectedIndex=i+1;
			}
			comboAllowedFeeSched.Items.Clear();
			comboAllowedFeeSched.Items.Add(Lan.g(this,"none"));
			comboAllowedFeeSched.SelectedIndex=0;
			for(int i=0;i<Defs.Short[(int)DefCat.FeeSchedNames].Length;i++) {
				comboAllowedFeeSched.Items.Add(Defs.Short[(int)DefCat.FeeSchedNames][i].ItemName);
				if(Defs.Short[(int)DefCat.FeeSchedNames][i].DefNum==PlanCur.AllowedFeeSched)
					comboAllowedFeeSched.SelectedIndex=i+1;
			}
			comboClaimForm.Items.Clear();
			for(int i=0;i<ClaimForms.ListShort.Length;i++) {
				comboClaimForm.Items.Add(ClaimForms.ListShort[i].Description);
				if(ClaimForms.ListShort[i].ClaimFormNum==PlanCur.ClaimFormNum) {
					comboClaimForm.SelectedIndex=i;
				}
			}
			if(comboClaimForm.Items.Count>0 && comboClaimForm.SelectedIndex==-1) {
				comboClaimForm.SelectedIndex=0;//this will let the user rearrange the default later
			}
			FillCarrier();
			string[] samePlans=PlanCur.GetSubscribersForSamePlans();
			textLinkedNum.Text=samePlans.Length.ToString();
			comboLinked.Items.Clear();
			for(int i=0;i<samePlans.Length;i++) {
				comboLinked.Items.Add(samePlans[i]);
			}
			if(samePlans.Length>0)
				comboLinked.SelectedIndex=0;
			checkRelease.Checked=PlanCur.ReleaseInfo;
			checkAssign.Checked=PlanCur.AssignBen;
			textPlanNote.Text=PlanCur.PlanNote;
			textSubscNote.Text=PlanCur.SubscNote;
			//if(PlanCur.BenefitNotes==""){
			//	butBenefitNotes.Enabled=false;
			//}
			Cursor=Cursors.Default;
		}

		private void FillPatientAdjustments(){
			ClaimProc[] ClaimProcList=ClaimProcs.Refresh(PatPlanCur.PatNum);
			AdjAL=new ArrayList();//move selected claimprocs into ALAdj
			for(int i=0;i<ClaimProcList.Length;i++){
				if(ClaimProcList[i].PlanNum==PlanCur.PlanNum
					&& ClaimProcList[i].Status==ClaimProcStatus.Adjustment)
				{
					AdjAL.Add(ClaimProcList[i]);
				}
			}
			listAdj.Items.Clear();
			string s;
			for(int i=0;i<AdjAL.Count;i++){
				s=((ClaimProc)AdjAL[i]).ProcDate.ToShortDateString()+"       Ins Used:  "
					+((ClaimProc)AdjAL[i]).InsPayAmt.ToString("F")+"       Ded Used:  "
					+((ClaimProc)AdjAL[i]).DedApplied.ToString("F");
				listAdj.Items.Add(s);
			}
		}

		///<summary>Fills the carrier fields on the form based on PlanCur.CarrierNum.</summary>
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

		///<summary>Only called from FillCarrier and textElectID_Validating. Fills comboElectIDdescript as appropriate.</summary>
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

		private void comboPlanType_SelectionChangeCommitted(object sender, System.EventArgs e) {
			//MessageBox.Show(InsPlans.Cur.PlanType+","+listPlanType.SelectedIndex.ToString());
			if(PlanCur.PlanType=="" && (comboPlanType.SelectedIndex==1 || comboPlanType.SelectedIndex==2)) {
				if(!MsgBox.Show(this,true,"This will clear all percentages. Continue?")){
					comboPlanType.SelectedIndex=0;
					return;
				}
				//Loop through the list backwards so i will be valid.
				for(int i=benefitList.Count-1;i>=0;i--){
					if(((Benefit)benefitList[i]).BenefitType==InsBenefitType.Percentage){
						benefitList.RemoveAt(i);
					}
				}
				//benefitList=new ArrayList();
				FillBenefits();
			}
			switch(comboPlanType.SelectedIndex){
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

		private void butAdjAdd_Click(object sender, System.EventArgs e) {
			ClaimProc ClaimProcCur=new ClaimProc();
			ClaimProcCur.PatNum=PatPlanCur.PatNum;
			ClaimProcCur.ProcDate=DateTime.Today;
			ClaimProcCur.Status=ClaimProcStatus.Adjustment;
			ClaimProcCur.PlanNum=PlanCur.PlanNum;
			FormInsAdj FormIA=new FormInsAdj(ClaimProcCur);
			FormIA.IsNew=true;
			FormIA.ShowDialog();
			FillPatientAdjustments();
		}

		private void listAdj_DoubleClick(object sender, System.EventArgs e) {
			if(listAdj.SelectedIndex==-1){
				return;
			}
			FormInsAdj FormIA=new FormInsAdj((ClaimProc)AdjAL[listAdj.SelectedIndex]);
			FormIA.ShowDialog();
			FillPatientAdjustments();
		}

		///<summary></summary>
		private void butPick_Click(object sender,EventArgs e) {
			FormInsPlans FormIP=new FormInsPlans();
			FormIP.empText=textEmployer.Text;
			FormIP.carrierText=textCarrier.Text;
			FormIP.IsSelectMode=true;
			FormIP.ShowDialog();
			if(FormIP.DialogResult==DialogResult.Cancel){
				return;
			}
			if(!IsNewPlan && !MsgBox.Show(this,true,"Are you sure you want to use the selected plan?  The change will now be saved permanently.  You should NOT use this if the patient is changing insurance.  Use the Drop button instead."))
			{
				return;
			}
			FillPlanCur();//this catches the non-synch fields
			PlanCur.EmployerNum    =FormIP.SelectedPlan.EmployerNum;
			PlanCur.GroupName      =FormIP.SelectedPlan.GroupName;
			PlanCur.GroupNum       =FormIP.SelectedPlan.GroupNum;
			PlanCur.DivisionNo     =FormIP.SelectedPlan.DivisionNo;
			PlanCur.CarrierNum     =FormIP.SelectedPlan.CarrierNum;
			PlanCur.PlanType       =FormIP.SelectedPlan.PlanType;
			PlanCur.UseAltCode     =FormIP.SelectedPlan.UseAltCode;
			PlanCur.IsMedical      =FormIP.SelectedPlan.IsMedical;
			PlanCur.ClaimsUseUCR   =FormIP.SelectedPlan.ClaimsUseUCR;
			PlanCur.FeeSched       =FormIP.SelectedPlan.FeeSched;
			PlanCur.CopayFeeSched  =FormIP.SelectedPlan.CopayFeeSched;
			PlanCur.ClaimFormNum   =FormIP.SelectedPlan.ClaimFormNum;
			PlanCur.AllowedFeeSched=FormIP.SelectedPlan.AllowedFeeSched;
			PlanCur.TrojanID       =FormIP.SelectedPlan.TrojanID;
			PlanCur.PlanNote       =FormIP.SelectedPlan.PlanNote;
			PlanCur.Update();//updates to the db so that the synch info will show correctly
			FillFormWithPlanCur();
			//need to clear benefits for this plan now.  That way they won't be available as benefits of an 'identical' plan.
			Benefits.DeleteForPlan(PlanCur.PlanNum);
			benefitList=Benefits.RefreshForAll(PlanCur);//these benefits are only typical, not precise.
			Benefits.UpdateList(benefitListOld,benefitList);//replaces all benefits for this plan in the database
			FillBenefits();
			//The new data on the form should be the basis for any 'changes to all'.
			PlanCurOld=PlanCur.Copy();
			benefitListOld=new ArrayList();
			for(int i=0;i<benefitList.Count;i++) {
				benefitListOld.Add(((Benefit)benefitList[i]).Copy());
			}
			//It's as if we've just opened a new form now.
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
					textCarrier.Text=((Carrier)similarCars[listCars.SelectedIndex]).CarrierName;
				}
				else if(listCars.SelectedIndex==listCars.Items.Count-1){
					listCars.SelectedIndex=-1;
					textCarrier.Text=carOriginal;
				}
				else{
					listCars.SelectedIndex++;
					textCarrier.Text=((Carrier)similarCars[listCars.SelectedIndex]).CarrierName;
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
					textCarrier.Text=((Carrier)similarCars[listCars.SelectedIndex]).CarrierName;
				}
				else if(listCars.SelectedIndex==0){
					listCars.SelectedIndex=-1;
					textCarrier.Text=carOriginal;
				}
				else{
					listCars.SelectedIndex--;
					textCarrier.Text=((Carrier)similarCars[listCars.SelectedIndex]).CarrierName;
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
				listCars.Items.Add(((Carrier)similarCars[i]).CarrierName+", "
					+((Carrier)similarCars[i]).Phone+", "
					+((Carrier)similarCars[i]).Address+", "
					+((Carrier)similarCars[i]).Address2+", "
					+((Carrier)similarCars[i]).City+", "
					+((Carrier)similarCars[i]).State+", "
					+((Carrier)similarCars[i]).Zip);
			}
			int h=13*similarCars.Count+5;
			if(h > ClientSize.Height-listCars.Top)
				h=ClientSize.Height-listCars.Top;
			listCars.Size=new Size(listCars.Width,h);
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

		private void butImportTrojan_Click(object sender, System.EventArgs e) {
			if(CovCats.GetForEbenCat(EbenefitCategory.RoutinePreventive)==null
				|| CovCats.GetForEbenCat(EbenefitCategory.Restorative)==null
				|| CovCats.GetForEbenCat(EbenefitCategory.Endodontics)==null
				|| CovCats.GetForEbenCat(EbenefitCategory.Periodontics)==null
				|| CovCats.GetForEbenCat(EbenefitCategory.Prosthodontics)==null)
			{
				MsgBox.Show(this,"You must first set up your insurance categories with corresponding electronic benefit categories: RoutinePreventive, Restorative, Endodontics, Periodontics, and Prosthodontics");
				return;
			}
			RegistryKey regKey=Registry.LocalMachine.OpenSubKey("Software\\TROJAN BENEFIT SERVICE");
			if(regKey==null){
				MessageBox.Show("Trojan not installed properly.");
				return;
			}
			string file=regKey.GetValue("INSTALLDIR").ToString()//C:\ETW
				+@"\Planout.txt";
			if(!File.Exists(file)){
				MessageBox.Show(file+" not found.  You should export from Trojan first.");
				return;
			}
			usesAnnivers=false;
			Benefit ben;
			//clear exising benefits from screen, not db:
			benefitList=new ArrayList();
			try{
				using(StreamReader sr=new StreamReader(file)){
					string line;
					string[] fields;
					int percent;
					string[] splitField;//if a field is a sentence with more than one word, we can split it for analysis
          while((line=sr.ReadLine())!=null){
						fields=line.Split(new char[] {'\t'});
						if(fields.Length!=3){
							continue;
						}
						//remove any trailing or leading spaces:
						fields[0]=fields[0].Trim();
						fields[1]=fields[1].Trim();
						fields[2]=fields[2].Trim();
						if(fields[2]==""){
							continue;
						}
						else{//as long as there is data, add it to the notes
							if(PlanCur.BenefitNotes!=""){
								PlanCur.BenefitNotes+="\r\n";
							}
							PlanCur.BenefitNotes+=fields[1]+": "+fields[2];
						}
						switch(fields[0]){
							//default://for all rows that are not handled below
							case "TROJANID":
								textTrojanID.Text=fields[2];
								break;
							case "ENAME":
								textEmployer.Text=fields[2];
								break;
							case "PLANDESC":
								textGroupName.Text=fields[2];
								break;
							case "ELIGPHONE":
								textPhone.Text=fields[2];
								break;
							case "POLICYNO":
								textGroupNum.Text=fields[2];
								break;
							case "ECLAIMS":
								if(fields[2]=="YES"){//accepts eclaims
									checkNoSendElect.Checked=false;
								}
								else{
									checkNoSendElect.Checked=true;
								}
								break;
							case "PAYERID":
								textElectID.Text=fields[2];
								break;
							case "MAILTO":
								textCarrier.Text=fields[2];
								break;
							case "MAILTOST":
								textAddress.Text=fields[2];
								break;
							case "MAILCITYONLY":
								textCity.Text=fields[2];
								break;
							case "MAILSTATEONLY":
								textState.Text=fields[2];
								break;
							case "MAILZIPONLY":
								textZip.Text=fields[2];
								break;
							case "PLANMAX"://eg $3000 per person per year
								if(!fields[2].StartsWith("$"))
									break;
								fields[2]=fields[2].Remove(0,1);
								fields[2]=fields[2].Split(new char[] {' '})[0];
								if(CovCats.ListShort.Length>0){
									ben=new Benefit();
									ben.BenefitType=InsBenefitType.Limitations;
									ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.General).CovCatNum;
									ben.MonetaryAmt=PIn.PDouble(fields[2]);
									ben.PlanNum=PlanCur.PlanNum;
									ben.TimePeriod=BenefitTimePeriod.CalendarYear;
									benefitList.Add(ben.Copy());
								}
								break;
							case "PLANYR"://eg Calendar year or Anniversary year
								if(fields[2]!="Calendar year"){
									usesAnnivers=true;
									MessageBox.Show("Warning.  Plan uses Anniversary year rather than Calendar year.  Please verify the Plan Start Date.");
								}
								break;
							case "DEDUCT"://eg There is no deductible
								ben=new Benefit();
								ben.BenefitType=InsBenefitType.Deductible;
								ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.General).CovCatNum;
								ben.PlanNum=PlanCur.PlanNum;
								ben.TimePeriod=BenefitTimePeriod.CalendarYear;
								if(!fields[2].StartsWith("$")){
									ben.MonetaryAmt=0;
								}
								else{
									fields[2]=fields[2].Remove(0,1);
									fields[2]=fields[2].Split(new char[] { ' ' })[0];
									ben.MonetaryAmt=PIn.PDouble(fields[2]);
								}					
								benefitList.Add(ben.Copy());
								break;
							case "PREV"://eg 100%
								splitField=fields[2].Split(new char[] {' '});
								if(splitField.Length==0 || !splitField[0].EndsWith("%")){
									break;
								}
								splitField[0]=splitField[0].Remove(splitField[0].Length-1,1);//remove %
								percent=PIn.PInt(splitField[0]);
								if(percent<0 || percent>100){
									break;
								}
								ben=new Benefit();
								ben.BenefitType=InsBenefitType.Percentage;
								ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.RoutinePreventive).CovCatNum;
								ben.Percent=percent;
								ben.PlanNum=PlanCur.PlanNum;
								ben.TimePeriod=BenefitTimePeriod.CalendarYear;
								benefitList.Add(ben.Copy());
								break;
							case "BASIC":
								splitField=fields[2].Split(new char[] {' '});
								if(splitField.Length==0 || !splitField[0].EndsWith("%")){
									break;
								}
								splitField[0]=splitField[0].Remove(splitField[0].Length-1,1);//remove %
								percent=PIn.PInt(splitField[0]);
								if(percent<0 || percent>100){
									break;
								}
								ben=new Benefit();
								ben.BenefitType=InsBenefitType.Percentage;
								ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Restorative).CovCatNum;
								ben.Percent=percent;
								ben.PlanNum=PlanCur.PlanNum;
								ben.TimePeriod=BenefitTimePeriod.CalendarYear;
								benefitList.Add(ben.Copy());
								ben=new Benefit();
								ben.BenefitType=InsBenefitType.Percentage;
								ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Endodontics).CovCatNum;
								ben.Percent=percent;
								ben.PlanNum=PlanCur.PlanNum;
								ben.TimePeriod=BenefitTimePeriod.CalendarYear;
								benefitList.Add(ben.Copy());
								ben=new Benefit();
								ben.BenefitType=InsBenefitType.Percentage;
								ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Periodontics).CovCatNum;
								ben.Percent=percent;
								ben.PlanNum=PlanCur.PlanNum;
								ben.TimePeriod=BenefitTimePeriod.CalendarYear;
								benefitList.Add(ben.Copy());
								break;
							case "MAJOR":
								splitField=fields[2].Split(new char[] {' '});
								if(splitField.Length==0 || !splitField[0].EndsWith("%")){
									break;
								}
								splitField[0]=splitField[0].Remove(splitField[0].Length-1,1);//remove %
								percent=PIn.PInt(splitField[0]);
								if(percent<0 || percent>100){
									break;
								}
								ben=new Benefit();
								ben.BenefitType=InsBenefitType.Percentage;
								ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Prosthodontics).CovCatNum;
								ben.Percent=percent;
								ben.PlanNum=PlanCur.PlanNum;
								ben.TimePeriod=BenefitTimePeriod.CalendarYear;
								benefitList.Add(ben.Copy());
								//does prosthodontics include crowns?
								break;
						}
          }
				}
				File.Delete(file);
				butBenefitNotes.Enabled=true;
      }
      catch(Exception ex){
				MessageBox.Show("Error: "+ex.Message);
      }
			if(usesAnnivers){
				for(int i=0;i<benefitList.Count;i++) {
					if(((Benefit)benefitList[i]).TimePeriod==BenefitTimePeriod.CalendarYear) {
						((Benefit)benefitList[i]).TimePeriod=BenefitTimePeriod.ServiceYear;
					}
				}
			}
			FillBenefits();
		}

		private void butIapFind_Click(object sender, System.EventArgs e) {
			FormIap FormI=new FormIap();
			FormI.ShowDialog();
			if(FormI.DialogResult==DialogResult.Cancel){
				return;
			}
			Benefit ben;
			//clear exising benefits from screen, not db:
			benefitList=new ArrayList();
			string emp=FormI.selectedEmployer;
			string field;
			string[] splitField;//if a field is a sentence with more than one word, we can split it for analysis
			int percent;
			try{
				Iap.ReadRecord(emp);
				for(int i=1;i<122;i++){
					field=Iap.ReadField(i);
					switch(i){
						default:
							//do nothing
							break;
						case Iap.Employer:
							if(PlanCur.BenefitNotes!=""){
								PlanCur.BenefitNotes+="\r\n";
							}
							PlanCur.BenefitNotes+="Employer: "+field;
							textEmployer.Text=field;
							break;
						case Iap.Phone:
							PlanCur.BenefitNotes+="\r\n"+"Phone: "+field;
							break;
						case Iap.InsUnder:
							PlanCur.BenefitNotes+="\r\n"+"InsUnder: "+field;
							break;
						case Iap.Carrier:
							PlanCur.BenefitNotes+="\r\n"+"Carrier: "+field;
							textCarrier.Text=field;
							break;
						case Iap.CarrierPh:
							PlanCur.BenefitNotes+="\r\n"+"CarrierPh: "+field;
							textPhone.Text=field;
							break;
						case Iap.Group://seems to be used as groupnum
							PlanCur.BenefitNotes+="\r\n"+"Group: "+field;
							textGroupNum.Text=field;
							break;
						case Iap.MailTo://the carrier name again
							PlanCur.BenefitNotes+="\r\n"+"MailTo: "+field;
							break;
						case Iap.MailTo2://address
							PlanCur.BenefitNotes+="\r\n"+"MailTo2: "+field;
							textAddress.Text=field;
							break;
						case Iap.MailTo3://address2
							PlanCur.BenefitNotes+="\r\n"+"MailTo3: "+field;
							textAddress2.Text=field;
							break;
						case Iap.EClaims:
							PlanCur.BenefitNotes+="\r\n"+"EClaims: "+field;//this contains the PayorID at the end, but also a bunch of other drivel.
							int payorIDloc=field.LastIndexOf("Payor ID#:");
							if(payorIDloc!=-1 && field.Length>payorIDloc+10){
								textElectID.Text=field.Substring(payorIDloc+10);
							}
							break;
						case Iap.FAXClaims:
							PlanCur.BenefitNotes+="\r\n"+"FAXClaims: "+field;
							break;
						case Iap.DMOOption:
							PlanCur.BenefitNotes+="\r\n"+"DMOOption: "+field;
							break;
						case Iap.Medical:
							PlanCur.BenefitNotes+="\r\n"+"Medical: "+field;
							break;
						case Iap.GroupNum://not used.  They seem to use the group field instead
							PlanCur.BenefitNotes+="\r\n"+"GroupNum: "+field;
							break;
						case Iap.Phone2://?
							PlanCur.BenefitNotes+="\r\n"+"Phone2: "+field;
							break;
						case Iap.Deductible:
							PlanCur.BenefitNotes+="\r\n"+"Deductible: "+field;
							if(field.StartsWith("$")){
								splitField=field.Split(new char[] {' '});
								ben=new Benefit();
								ben.BenefitType=InsBenefitType.Deductible;
								ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.General).CovCatNum;
								ben.PlanNum=PlanCur.PlanNum;
								ben.TimePeriod=BenefitTimePeriod.CalendarYear;
								ben.MonetaryAmt=PIn.PDouble(splitField[0].Remove(0,1));//removes the $
								benefitList.Add(ben.Copy());
							}
							break;
						case Iap.FamilyDed:
							PlanCur.BenefitNotes+="\r\n"+"FamilyDed: "+field;
							break;
						case Iap.Maximum:
							PlanCur.BenefitNotes+="\r\n"+"Maximum: "+field;
							if(field.StartsWith("$")){
								splitField=field.Split(new char[] {' '});
								ben=new Benefit();
								ben.BenefitType=InsBenefitType.Limitations;
								ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.General).CovCatNum;
								ben.PlanNum=PlanCur.PlanNum;
								ben.TimePeriod=BenefitTimePeriod.CalendarYear;
								ben.MonetaryAmt=PIn.PDouble(splitField[0].Remove(0,1));//removes the $
								benefitList.Add(ben.Copy());
							}
							break;
						case Iap.BenefitYear://text is too complex to parse
							PlanCur.BenefitNotes+="\r\n"+"BenefitYear: "+field;
							break;
						case Iap.DependentAge://too complex to parse
							PlanCur.BenefitNotes+="\r\n"+"DependentAge: "+field;
							break;
						case Iap.Preventive:
							PlanCur.BenefitNotes+="\r\n"+"Preventive: "+field;
							splitField=field.Split(new char[] {' '});
							if(splitField.Length==0 || !splitField[0].EndsWith("%")){
								break;
							}
							splitField[0]=splitField[0].Remove(splitField[0].Length-1,1);//remove %
							percent=PIn.PInt(splitField[0]);
							if(percent<0 || percent>100){
								break;
							}
							ben=new Benefit();
							ben.BenefitType=InsBenefitType.Percentage;
							ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.RoutinePreventive).CovCatNum;
							ben.PlanNum=PlanCur.PlanNum;
							ben.TimePeriod=BenefitTimePeriod.CalendarYear;
							ben.Percent=percent;
							benefitList.Add(ben.Copy());
							break;
						case Iap.Basic:
							PlanCur.BenefitNotes+="\r\n"+"Basic: "+field;
							splitField=field.Split(new char[] {' '});
							if(splitField.Length==0 || !splitField[0].EndsWith("%")){
								break;
							}
							splitField[0]=splitField[0].Remove(splitField[0].Length-1,1);//remove %
							percent=PIn.PInt(splitField[0]);
							if(percent<0 || percent>100){
								break;
							}
							ben=new Benefit();
							ben.BenefitType=InsBenefitType.Percentage;
							ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Restorative).CovCatNum;
							ben.PlanNum=PlanCur.PlanNum;
							ben.TimePeriod=BenefitTimePeriod.CalendarYear;
							ben.Percent=percent;
							benefitList.Add(ben.Copy());
							ben=new Benefit();
							ben.BenefitType=InsBenefitType.Percentage;
							ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Endodontics).CovCatNum;
							ben.PlanNum=PlanCur.PlanNum;
							ben.TimePeriod=BenefitTimePeriod.CalendarYear;
							ben.Percent=percent;
							benefitList.Add(ben.Copy());
							ben=new Benefit();
							ben.BenefitType=InsBenefitType.Percentage;
							ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Periodontics).CovCatNum;
							ben.PlanNum=PlanCur.PlanNum;
							ben.TimePeriod=BenefitTimePeriod.CalendarYear;
							ben.Percent=percent;
							benefitList.Add(ben.Copy());
							ben=new Benefit();
							ben.BenefitType=InsBenefitType.Percentage;
							ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.OralSurgery).CovCatNum;
							ben.PlanNum=PlanCur.PlanNum;
							ben.TimePeriod=BenefitTimePeriod.CalendarYear;
							ben.Percent=percent;
							benefitList.Add(ben.Copy());
							break;
						case Iap.Major:
							PlanCur.BenefitNotes+="\r\n"+"Major: "+field;
							splitField=field.Split(new char[] {' '});
							if(splitField.Length==0 || !splitField[0].EndsWith("%")){
								break;
							}
							splitField[0]=splitField[0].Remove(splitField[0].Length-1,1);//remove %
							percent=PIn.PInt(splitField[0]);
							if(percent<0 || percent>100){
								break;
							}
							ben=new Benefit();
							ben.BenefitType=InsBenefitType.Percentage;
							ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Prosthodontics).CovCatNum;//includes crowns?
							ben.PlanNum=PlanCur.PlanNum;
							ben.TimePeriod=BenefitTimePeriod.CalendarYear;
							ben.Percent=percent;
							benefitList.Add(ben.Copy());
							break;
						case Iap.InitialPlacement:
							PlanCur.BenefitNotes+="\r\n"+"InitialPlacement: "+field;
							break;
						case Iap.ExtractionClause:
							PlanCur.BenefitNotes+="\r\n"+"ExtractionClause: "+field;
							break;
						case Iap.Replacement:
							PlanCur.BenefitNotes+="\r\n"+"Replacement: "+field;
							break;
						case Iap.Other:
							PlanCur.BenefitNotes+="\r\n"+"Other: "+field;
							break;
						case Iap.Orthodontics:
							PlanCur.BenefitNotes+="\r\n"+"Orthodontics: "+field;
							splitField=field.Split(new char[] {' '});
							if(splitField.Length==0 || !splitField[0].EndsWith("%")){
								break;
							}
							splitField[0]=splitField[0].Remove(splitField[0].Length-1,1);//remove %
							percent=PIn.PInt(splitField[0]);
							if(percent<0 || percent>100){
								break;
							}
							ben=new Benefit();
							ben.BenefitType=InsBenefitType.Percentage;
							ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Orthodontics).CovCatNum;
							ben.PlanNum=PlanCur.PlanNum;
							ben.TimePeriod=BenefitTimePeriod.CalendarYear;
							ben.Percent=percent;
							benefitList.Add(ben.Copy());
							break;
						case Iap.Deductible2:
							PlanCur.BenefitNotes+="\r\n"+"Deductible2: "+field;
							break;
						case Iap.Maximum2://ortho Max
							PlanCur.BenefitNotes+="\r\n"+"Maximum2: "+field;
							if(field.StartsWith("$")){
								splitField=field.Split(new char[] {' '});							
								ben=new Benefit();
								ben.BenefitType=InsBenefitType.Limitations;
								ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Orthodontics).CovCatNum;
								ben.PlanNum=PlanCur.PlanNum;
								ben.TimePeriod=BenefitTimePeriod.CalendarYear;
								ben.MonetaryAmt=PIn.PDouble(splitField[0].Remove(0,1));//removes the $
								benefitList.Add(ben.Copy());
							}
							break;
						case Iap.PymtSchedule:
							PlanCur.BenefitNotes+="\r\n"+"PymtSchedule: "+field;
							break;
						case Iap.AgeLimit:
							PlanCur.BenefitNotes+="\r\n"+"AgeLimit: "+field;
							break;
						case Iap.SignatureonFile:
							PlanCur.BenefitNotes+="\r\n"+"SignatureonFile: "+field;
							break;
						case Iap.StandardADAForm:
							PlanCur.BenefitNotes+="\r\n"+"StandardADAForm: "+field;
							break;
						case Iap.CoordinationRule:
							PlanCur.BenefitNotes+="\r\n"+"CoordinationRule: "+field;
							break;
						case Iap.CoordinationCOB:
							PlanCur.BenefitNotes+="\r\n"+"CoordinationCOB: "+field;
							break;
						case Iap.NightguardsforBruxism:
							PlanCur.BenefitNotes+="\r\n"+"NightguardsforBruxism: "+field;
							break;
						case Iap.OcclusalAdjustments:
							PlanCur.BenefitNotes+="\r\n"+"OcclusalAdjustments: "+field;
							break;
						case Iap.XXXXXX:
							PlanCur.BenefitNotes+="\r\n"+"XXXXXX: "+field;
							break;
						case Iap.TMJNonSurgical:
							PlanCur.BenefitNotes+="\r\n"+"TMJNonSurgical: "+field;
							break;
						case Iap.Implants:
							PlanCur.BenefitNotes+="\r\n"+"Implants: "+field;
							break;
						case Iap.InfectionControl:
							PlanCur.BenefitNotes+="\r\n"+"InfectionControl: "+field;
							break;
						case Iap.Cleanings:
							PlanCur.BenefitNotes+="\r\n"+"Cleanings: "+field;
							break;
						case Iap.OralEvaluation:
							PlanCur.BenefitNotes+="\r\n"+"OralEvaluation: "+field;
							break;
						case Iap.Fluoride1200s:
							PlanCur.BenefitNotes+="\r\n"+"Fluoride1200s: "+field;
							break;
						case Iap.Code0220:
							PlanCur.BenefitNotes+="\r\n"+"Code0220: "+field;
							break;
						case Iap.Code0272_0274:
							PlanCur.BenefitNotes+="\r\n"+"Code0272_0274: "+field;
							break;
						case Iap.Code0210:
							PlanCur.BenefitNotes+="\r\n"+"Code0210: "+field;
							break;
						case Iap.Code0330:
							PlanCur.BenefitNotes+="\r\n"+"Code0330: "+field;
							break;
						case Iap.SpaceMaintainers:
							PlanCur.BenefitNotes+="\r\n"+"SpaceMaintainers: "+field;
							break;
						case Iap.EmergencyExams:
							PlanCur.BenefitNotes+="\r\n"+"EmergencyExams: "+field;
							break;
						case Iap.EmergencyTreatment:
							PlanCur.BenefitNotes+="\r\n"+"EmergencyTreatment: "+field;
							break;
						case Iap.Sealants1351:
							PlanCur.BenefitNotes+="\r\n"+"Sealants1351: "+field;
							break;
						case Iap.Fillings2100:
							PlanCur.BenefitNotes+="\r\n"+"Fillings2100: "+field;
							break;
						case Iap.Extractions:
							PlanCur.BenefitNotes+="\r\n"+"Extractions: "+field;
							break;
						case Iap.RootCanals:
							PlanCur.BenefitNotes+="\r\n"+"RootCanals: "+field;
							break;
						case Iap.MolarRootCanal:
							PlanCur.BenefitNotes+="\r\n"+"MolarRootCanal: "+field;
							break;
						case Iap.OralSurgery:
							PlanCur.BenefitNotes+="\r\n"+"OralSurgery: "+field;
							break;
						case Iap.ImpactionSoftTissue:
							PlanCur.BenefitNotes+="\r\n"+"ImpactionSoftTissue: "+field;
							break;
						case Iap.ImpactionPartialBony:
							PlanCur.BenefitNotes+="\r\n"+"ImpactionPartialBony: "+field;
							break;
						case Iap.ImpactionCompleteBony:
							PlanCur.BenefitNotes+="\r\n"+"ImpactionCompleteBony: "+field;
							break;
						case Iap.SurgicalProceduresGeneral:
							PlanCur.BenefitNotes+="\r\n"+"SurgicalProceduresGeneral: "+field;
							break;
						case Iap.PerioSurgicalPerioOsseous:
							PlanCur.BenefitNotes+="\r\n"+"PerioSurgicalPerioOsseous: "+field;
							break;
						case Iap.SurgicalPerioOther:
							PlanCur.BenefitNotes+="\r\n"+"SurgicalPerioOther: "+field;
							break;
						case Iap.RootPlaning:
							PlanCur.BenefitNotes+="\r\n"+"RootPlaning: "+field;
							break;
						case Iap.Scaling4345:
							PlanCur.BenefitNotes+="\r\n"+"Scaling4345: "+field;
							break;
						case Iap.PerioPx:
							PlanCur.BenefitNotes+="\r\n"+"PerioPx: "+field;
							break;
						case Iap.PerioComment:
							PlanCur.BenefitNotes+="\r\n"+"PerioComment: "+field;
							break;
						case Iap.IVSedation:
							PlanCur.BenefitNotes+="\r\n"+"IVSedation: "+field;
							break;
						case Iap.General9220:
							PlanCur.BenefitNotes+="\r\n"+"General9220: "+field;
							break;
						case Iap.Relines5700s:
							PlanCur.BenefitNotes+="\r\n"+"Relines5700s: "+field;
							break;
						case Iap.StainlessSteelCrowns:
							PlanCur.BenefitNotes+="\r\n"+"StainlessSteelCrowns: "+field;
							break;
						case Iap.Crowns2700s:
							PlanCur.BenefitNotes+="\r\n"+"Crowns2700s: "+field;
							break;
						case Iap.Bridges6200:
							PlanCur.BenefitNotes+="\r\n"+"Bridges6200: "+field;
							break;
						case Iap.Partials5200s:
							PlanCur.BenefitNotes+="\r\n"+"Partials5200s: "+field;
							break;
						case Iap.Dentures5100s:
							PlanCur.BenefitNotes+="\r\n"+"Dentures5100s: "+field;
							break;
						case Iap.EmpNumberXXX:
							PlanCur.BenefitNotes+="\r\n"+"EmpNumberXXX: "+field;
							break;
						case Iap.DateXXX:
							PlanCur.BenefitNotes+="\r\n"+"DateXXX: "+field;
							break;
						case Iap.Line4://city state
							PlanCur.BenefitNotes+="\r\n"+"Line4: "+field;
							field=field.Replace("  "," ");//get rid of double space before zip
							splitField=field.Split(new char[] {' '});
							if(splitField.Length<3){
								break;
							}
							textCity.Text=splitField[0].Replace(",","");//gets rid of the comma on the end of city
							textState.Text=splitField[1];
							textZip.Text=splitField[2];
							break;
						case Iap.Note:
							PlanCur.BenefitNotes+="\r\n"+"Note: "+field;
							break;
						case Iap.Plan://?
							PlanCur.BenefitNotes+="\r\n"+"Plan: "+field;
							break;
						case Iap.BuildUps:
							PlanCur.BenefitNotes+="\r\n"+"BuildUps: "+field;
							break;
						case Iap.PosteriorComposites:
							PlanCur.BenefitNotes+="\r\n"+"PosteriorComposites: "+field;
							break;
					}
				}
				Iap.CloseDatabase();
				butBenefitNotes.Enabled=true;
			}
			catch(ApplicationException ex){
				Iap.CloseDatabase();
				MessageBox.Show(ex.Message);
			}
			catch(Exception ex){
				Iap.CloseDatabase();
				MessageBox.Show("Error: "+ex.Message);
			}
			FillBenefits();
		}

		///<summary>This button is always active.</summary>
		private void butBenefitNotes_Click(object sender, System.EventArgs e) {
			string benNote=PlanCur.BenefitNotes;
			if(PlanCur.BenefitNotes==""){
				//try to find some other similar notes.
				FillPlanCur();
				int[] samePlans=PlanCur.GetPlanNumsOfSamePlans();//this might not include the current plan, because it's not saved to db yet.
				benNote=InsPlans.GetBenefitNotes(samePlans);
				if(benNote==""){
					MsgBox.Show(this,"No benefit note found.  Benefit notes are created when electronically requesting benefit information.  Store your own benefit notes in the subscriber note instead.");
					return;
				}
				MsgBox.Show(this,"This plan does not have a benefit note, but a note was found for an identical plan.  You will be able to view this note, but not change it.");
			}
			FormInsBenefitNotes FormI=new FormInsBenefitNotes();
			FormI.BenefitNotes=benNote;
			FormI.ShowDialog();
			if(FormI.DialogResult==DialogResult.Cancel){
				return;
			}
			if(PlanCur.BenefitNotes!=""){
				PlanCur.BenefitNotes=FormI.BenefitNotes;
			}
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(IsNewPlan){
				DialogResult=DialogResult.Cancel;//will get deleted in closing event
				return;
			}
			if(!MsgBox.Show(this,true,"Delete Plan?")){
				return;
			}
			try{
				PlanCur.Delete();//checks dependencies first. Also deletes benefits,claimprocs,patplan and recomputes all estimates.
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
			}
			DialogResult=DialogResult.OK;			
		}

		private void butDrop_Click(object sender, System.EventArgs e) {
			//should we save the plan info first?  Probably not.
			PatPlans.Delete(PatPlanCur.PatPlanNum);//Estimates recomputed within Delete()
			//PlanCur.ComputeEstimatesForCur();
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

		///<summary>This only fills the grid on the screen.  It does not get any data from the database.</summary>
		private void FillBenefits(){
			benefitList.Sort();
			gridBenefits.BeginUpdate();
			gridBenefits.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Pat",35);
			gridBenefits.Columns.Add(col);
			col=new ODGridColumn("Type",90);
			gridBenefits.Columns.Add(col);
			col=new ODGridColumn("Category",90);
			gridBenefits.Columns.Add(col);
			col=new ODGridColumn("%",40);//,HorizontalAlignment.Right);
			gridBenefits.Columns.Add(col);
			col=new ODGridColumn("Amt",60);//,HorizontalAlignment.Right);
			gridBenefits.Columns.Add(col);
			col=new ODGridColumn("Time Period",80);
			gridBenefits.Columns.Add(col);
			col=new ODGridColumn("Quantity",115);
			gridBenefits.Columns.Add(col);
			gridBenefits.Rows.Clear();
			ODGridRow row;
			bool allCalendarYear=true;
			bool allServiceYear=true;
			for(int i=0;i<benefitList.Count;i++){
				if(((Benefit)benefitList[i]).TimePeriod==BenefitTimePeriod.CalendarYear){
					allServiceYear=false;
				}
				if(((Benefit)benefitList[i]).TimePeriod==BenefitTimePeriod.ServiceYear) {
					allCalendarYear=false;
				}
				row=new ODGridRow();
				if(((Benefit)benefitList[i]).PatPlanNum==0){//attached to plan
					row.Cells.Add("");
				}
				else{
					row.Cells.Add("X");
				}
				if(((Benefit)benefitList[i]).BenefitType==InsBenefitType.Percentage){
					row.Cells.Add("%");
				}
				else if(((Benefit)benefitList[i]).BenefitType==InsBenefitType.Limitations
					&& (((Benefit)benefitList[i]).TimePeriod==BenefitTimePeriod.ServiceYear
					|| ((Benefit)benefitList[i]).TimePeriod==BenefitTimePeriod.CalendarYear)
					&& ((Benefit)benefitList[i]).QuantityQualifier==BenefitQuantity.None)
				{//annual max
					row.Cells.Add(Lan.g(this,"Annual Max"));
				}
				else{
					row.Cells.Add(Lan.g("enumInsBenefitType",((Benefit)benefitList[i]).BenefitType.ToString()));
				}
				if(((Benefit)benefitList[i]).ADACode==null || ((Benefit)benefitList[i]).ADACode==""){
					row.Cells.Add(CovCats.GetDesc(((Benefit)benefitList[i]).CovCatNum));
				}
				else{
					row.Cells.Add(ProcedureCodes.GetProcCode(((Benefit)benefitList[i]).ADACode).AbbrDesc);
				}
				if(((Benefit)benefitList[i]).BenefitType==InsBenefitType.Percentage){
					row.Cells.Add(((Benefit)benefitList[i]).Percent.ToString());
				}
				else{
					row.Cells.Add("");
				}
				if(((Benefit)benefitList[i]).MonetaryAmt==0){
					if(((Benefit)benefitList[i]).BenefitType==InsBenefitType.Deductible){
						row.Cells.Add(((Benefit)benefitList[i]).MonetaryAmt.ToString("n0"));
					}
					else{
						row.Cells.Add("");
					}
				}
				else {
					row.Cells.Add(((Benefit)benefitList[i]).MonetaryAmt.ToString("n0"));
				}
				if(((Benefit)benefitList[i]).TimePeriod==BenefitTimePeriod.None) {
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(Lan.g("enumBenefitTimePeriod",((Benefit)benefitList[i]).TimePeriod.ToString()));
				}
				if(((Benefit)benefitList[i]).Quantity>0){
					row.Cells.Add(((Benefit)benefitList[i]).Quantity.ToString()+" "
						+Lan.g("enumBenefitQuantity",((Benefit)benefitList[i]).QuantityQualifier.ToString()));
				}
				else{
					row.Cells.Add("");
				}
				gridBenefits.Rows.Add(row);
			}
			gridBenefits.EndUpdate();
			if(allCalendarYear){
				checkCalendarYear.CheckState=CheckState.Checked;
			}
			else if(allServiceYear){
				checkCalendarYear.CheckState=CheckState.Unchecked;
			}
			else{
				checkCalendarYear.CheckState=CheckState.Indeterminate;
			}
		}		

		private void gridBenefits_CellDoubleClick(object sender,OpenDental.UI.ODGridClickEventArgs e){
			int patPlanNum=0;
			if(PatPlanCur!=null) {
				patPlanNum=PatPlanCur.PatPlanNum;
			}
			FormBenefitEdit FormB=new FormBenefitEdit(patPlanNum,PlanCur.PlanNum);
			FormB.BenCur=(Benefit)benefitList[e.Row];
			FormB.ShowDialog();
			if(FormB.BenCur==null){//user deleted
				benefitList.RemoveAt(e.Row);
			}
			FillBenefits();
		}

		private void checkCalendarYear_Click(object sender,EventArgs e) {
			//checkstate will have already changed.
			if(checkCalendarYear.CheckState==CheckState.Checked){//change all to calendarYear
				for(int i=0;i<benefitList.Count;i++){
					if(((Benefit)benefitList[i]).TimePeriod==BenefitTimePeriod.ServiceYear){
						((Benefit)benefitList[i]).TimePeriod=BenefitTimePeriod.CalendarYear;
					}
				}
			}
			if(checkCalendarYear.CheckState==CheckState.Indeterminate
				|| checkCalendarYear.CheckState==CheckState.Unchecked) {//change all to serviceYear
				for(int i=0;i<benefitList.Count;i++) {
					if(((Benefit)benefitList[i]).TimePeriod==BenefitTimePeriod.CalendarYear) {
						((Benefit)benefitList[i]).TimePeriod=BenefitTimePeriod.ServiceYear;
					}
				}
			}
			FillBenefits();
		}

		private void butAnnualMax_Click(object sender,EventArgs e) {
			Benefit ben=new Benefit();
			ben.PlanNum=PlanCur.PlanNum;
			if(checkCalendarYear.CheckState==CheckState.Checked){
				ben.TimePeriod=BenefitTimePeriod.CalendarYear;
			}
			if(checkCalendarYear.CheckState==CheckState.Unchecked) {
				ben.TimePeriod=BenefitTimePeriod.ServiceYear;
			}
			if(CovCats.ListShort.Length>0) {
				ben.CovCatNum=CovCats.ListShort[0].CovCatNum;
			}
			ben.BenefitType=InsBenefitType.Limitations;
			int patPlanNum=0;
			if(PatPlanCur!=null) {
				patPlanNum=PatPlanCur.PatPlanNum;
			}
			FormBenefitEdit FormB=new FormBenefitEdit(patPlanNum,PlanCur.PlanNum);
			FormB.IsNew=true;
			FormB.BenCur=ben;
			FormB.ShowDialog();
			if(FormB.DialogResult==DialogResult.OK) {
				benefitList.Add(FormB.BenCur);
			}
			FillBenefits();
		}

		private void butAddBenefit_Click(object sender,EventArgs e) {
			Benefit ben=new Benefit();
			ben.PlanNum=PlanCur.PlanNum;
			if(checkCalendarYear.CheckState==CheckState.Checked) {
				ben.TimePeriod=BenefitTimePeriod.CalendarYear;
			}
			if(checkCalendarYear.CheckState==CheckState.Unchecked) {
				ben.TimePeriod=BenefitTimePeriod.ServiceYear;
			}
			if(CovCats.ListShort.Length>0){
				ben.CovCatNum=CovCats.ListShort[0].CovCatNum;
			}
			ben.BenefitType=InsBenefitType.Percentage;
			int patPlanNum=0;
			if(PatPlanCur!=null){
				patPlanNum=PatPlanCur.PatPlanNum;
			}
			FormBenefitEdit FormB=new FormBenefitEdit(patPlanNum,PlanCur.PlanNum);
			FormB.IsNew=true;
			FormB.BenCur=ben;
			FormB.ShowDialog();
			if(FormB.DialogResult==DialogResult.OK){
				benefitList.Add(FormB.BenCur);	
			}
			FillBenefits();
		}

		private void butClear_Click(object sender,EventArgs e) {
			benefitList=new ArrayList();
			FillBenefits();
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

		///<summary>Gets a carrierNum based on the data entered. Called from FillPlanCur and butLabel_Click</summary>
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

		///<summary>Fills the current insplan with data from the form.  Validates first. Called from butOK click, butPick_Click, and butBenefitNotes_Click. Does not save to database.</summary>
		private bool FillPlanCur(){
			if(textDateEffect.errorProvider1.GetError(textDateEffect)!=""
				|| textDateTerm.errorProvider1.GetError(textDateTerm)!=""
				){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return false;
			}
			if(textSubscriberID.Text=="" && !IsForAll){
				MessageBox.Show(Lan.g(this,"Subscriber ID not allowed to be blank."));
				return false;
			}
			if(textCarrier.Text==""){
				MessageBox.Show(Lan.g(this,"Carrier not allowed to be blank."));
				return false;
			}
			//Subscriber: Can never be changed once a plan is created.
			PlanCur.SubscriberID =textSubscriberID.Text;
			PlanCur.DateEffective=PIn.PDate(textDateEffect.Text);
			PlanCur.DateTerm     =PIn.PDate(textDateTerm.Text);
			GetEmployerNum();
			PlanCur.GroupName=textGroupName.Text;
			PlanCur.GroupNum=textGroupNum.Text;
			PlanCur.DivisionNo=textDivisionNo.Text;//only visible in Canada
			GetCarrierNum();
			//plantype already handled.
			if(comboClaimForm.SelectedIndex!=-1)
				PlanCur.ClaimFormNum=ClaimForms.ListShort[comboClaimForm.SelectedIndex].ClaimFormNum;	
			PlanCur.UseAltCode=checkAlternateCode.Checked;
			PlanCur.IsMedical=checkIsMedical.Checked;
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
				PlanCur.AllowedFeeSched=Defs.Short[(int)DefCat.FeeSchedNames][comboAllowedFeeSched.SelectedIndex-1].DefNum;
			PlanCur.TrojanID=textTrojanID.Text;
			PlanCur.PlanNote=textPlanNote.Text;
			PlanCur.ReleaseInfo=checkRelease.Checked;
			PlanCur.AssignBen=checkAssign.Checked;
			PlanCur.SubscNote=textSubscNote.Text;
			return true;
		}

		///<summary>Only called from butOK_Click</summary>
		private bool FillPatPlanCur(){
			if(textOrdinal.errorProvider1.GetError(textOrdinal)!=""
				){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return false;
			}
			
			if(PIn.PInt(textOrdinal.Text)!=PatPlanCur.Ordinal){//if ordinal changed
				PatPlans.SetOrdinal(PatPlanCur.PatPlanNum,PIn.PInt(textOrdinal.Text));
			}
			PatPlanCur.IsPending=checkIsPending.Checked;
			PatPlanCur.Relationship=(Relat)comboRelationship.SelectedIndex;
			PatPlanCur.PatID=textPatID.Text;
			return true;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			Cursor=Cursors.WaitCursor;
			if(!FillPlanCur()){
				Cursor=Cursors.Default;
				return;
			}
			if(PatPlanCur!=null) {
				if(!FillPatPlanCur()) {
					Cursor=Cursors.Default;
					return;
				}
				PatPlanCur.Update();
			}
			if(!IsForAll){
				PlanCur.Update();//this has to be done to catch the extra fields which are not sychronized
			}
			int[] planNums=PlanCur.GetPlanNumsOfSamePlans();
			if(checkApplyAll.Checked){//also triggered when IsForAll, because box is checked but hidden
				PlanCur.UpdateForLike(PlanCurOld);
				Benefits.UpdateListForIdentical(benefitListOld,benefitList,PlanCur);
				for(int i=0;i<planNums.Length;i++){
					InsPlans.ComputeEstimatesForPlan(planNums[i]);
				}
			}
			else{
				Benefits.UpdateList(benefitListOld,benefitList);
				InsPlans.ComputeEstimatesForPlan(PlanCur.PlanNum);
			}
			//Synchronize PlanNote----------------------------------------------------------------------------------------------
			if(planNums.Length<=1){//if no similar plans
				//note has already been saved
				Cursor=Cursors.Default;
				DialogResult=DialogResult.OK;
			}
			//Get a list of all distinct notes, not including this one
			string[] notesSimilar=InsPlans.GetNotesForPlans(planNums,PlanCur.PlanNum);//excludes PlanCur
			bool curNoteInSimilar=false;
			for(int i=0;i<notesSimilar.Length;i++){
				if(notesSimilar[i]==PlanCur.PlanNote){
					curNoteInSimilar=true;
				}
			}
			string[] notesAll;//this has all distinct notes INCLUDING PlanCur.PlanNote
			if(curNoteInSimilar){//the curNote is alread in notesSimilar
				notesAll=new string[notesSimilar.Length];
				notesSimilar.CopyTo(notesAll,0);
			}
			else{
				notesAll=new string[notesSimilar.Length+1];
				notesSimilar.CopyTo(notesAll,1);
				notesAll[0]=PlanCur.PlanNote;//curNote will be at position 0
			}
			if(notesSimilar.Length==0){
				//probably because there are not even any other similar plans
				//note has already been saved
			}
			else if(notesSimilar.Length==1){
				if(notesSimilar[0]==PlanCur.PlanNote){//all notes are already the same
					//note has already been saved
				}
				else if(notesSimilar[0]==PlanCurOld.PlanNote){//notes were all the same until user just changed it
					//this also handles 'deleting' a note
					InsPlans.UpdateNoteForPlans(planNums,PlanCur.PlanNote);
				}
				//this note is different than the other notes
				else if(IsNewPlan){//but it's a new plan, so user simply isn't aware of difference
					//if(PlanCur.PlanNote==""){//user never entered a note for the new plan. Still must inform user of note.
					//must give user a choice of notes
					FormNotePick FormN=new FormNotePick(notesAll);
					FormN.ShowDialog();
					if(FormN.DialogResult==DialogResult.OK){
						InsPlans.UpdateNoteForPlans(planNums,FormN.SelectedNote);
					}
					//If user cancels, the note for this insplan is already saved, just no synch happens.
				}
			}
			else{//notesSimilar must be >1.  Can't really think of how this could happen except for improper conversion
				//must give user a choice of notes
				FormNotePick FormN=new FormNotePick(notesAll);
				FormN.ShowDialog();
				if(FormN.DialogResult==DialogResult.OK){
					InsPlans.UpdateNoteForPlans(planNums,FormN.SelectedNote);
				}
				//If user cancels, the note for this insplan is already saved, just no synch happens.				
			}
			Cursor=Cursors.Default;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormInsPlan_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNewPlan){//this would also be new coverage
				PlanCur.Delete();//also drops
			}
			else if(IsNewPatPlan){//but plan is not new
				PatPlans.Delete(PatPlanCur.PatPlanNum);//no need to check dependencies.  Maintains ordinals and recomputes estimates.
			}
		}

		

		

		

	}
}
