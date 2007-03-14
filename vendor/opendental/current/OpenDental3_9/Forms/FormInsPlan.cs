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
		///<summary>The InsPlan is always inserted before opening this form.</summary>
		public bool IsNewPlan;
		///<summary>The PatPlan is always inserted before opening this form.</summary>
		public bool IsNewPatPlan;
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
		private System.Windows.Forms.CheckBox checkClaimsUseUCR;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox textSubscriber;
		private System.Windows.Forms.GroupBox groupBox2;
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
		private OpenDental.ODtextBox textPlanNote;
		private System.Windows.Forms.ComboBox comboCopay;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ComboBox comboFeeSched;
		private System.Windows.Forms.ComboBox comboClaimForm;
		private OpenDental.UI.Button butSearch;
		///<summary></summary>
		public InsPlan PlanCur;
		private System.Windows.Forms.ComboBox comboElectIDdescript;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ComboBox comboAllowedFeeSched;
		private OpenDental.UI.Button butLabel;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox textTrojanID;
		private OpenDental.UI.Button butImportTrojan;
		private System.Windows.Forms.Label labelDivisionDash;
		private System.Windows.Forms.TextBox textDivisionNo;
		private System.Windows.Forms.Label labelElectronicID;
		private OpenDental.UI.Button butEditAll;
		private System.Windows.Forms.Label label15;
		private OpenDental.UI.Button butIapFind;
		private System.Windows.Forms.Label label24;
		private OpenDental.UI.Button butBenefitNotes;
		private System.Windows.Forms.CheckBox checkIsMedical;
		private System.Windows.Forms.CheckBox checkAlternateCode;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Panel panelSynch;
		private System.Windows.Forms.ComboBox comboPlanType;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.Label label36;
		private Carrier CarrierCur;
		private System.Windows.Forms.ComboBox comboRelationship;
		private System.Windows.Forms.CheckBox checkIsPending;
		private System.Windows.Forms.TextBox textPatID;
		private OpenDental.UI.Button butAdjAdd;
		private System.Windows.Forms.ListBox listAdj;
		private OpenDental.TablePercent tbPercentPat;
		private System.Windows.Forms.Panel panelPat;
		private OpenDental.TablePercent tbPercentPlan;
		private PatPlan PatPlanCur;
		private CovPat[] CovListForPlan;
		private ArrayList AdjAL;
		private OpenDental.ValidNumber textOrdinal;
		///<summary>Not really for 'All', but only for all patplans for this patient.  A plan might not be attached to a patient, so the list for plan is taken directly from database.</summary>
		private CovPat[] CovListForAll;
		//private CovPat[] CovListForPat;

		///<summary>Only called from ContrFamily. Must pass in both the plan and the patPlan.  They are handled separately.</summary>
		public FormInsPlan(InsPlan planCur,PatPlan patPlanCur){
			Cursor=Cursors.WaitCursor;
			InitializeComponent();
			PlanCur=planCur;
			PatPlanCur=patPlanCur;
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
			listCars.Size=new Size(700,100);
			listCars.HorizontalScrollbar=true;
			listCars.Visible=false;
			listCars.Click += new System.EventHandler(listCars_Click);
			listCars.DoubleClick += new System.EventHandler(listCars_DoubleClick);
			listCars.MouseEnter += new System.EventHandler(listCars_MouseEnter);
			listCars.MouseLeave += new System.EventHandler(listCars_MouseLeave);
			Controls.Add(listCars);
			listCars.BringToFront();
			tbPercentPlan.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbPercentPlan_CellClicked);
			tbPercentPat.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbPercentPat_CellClicked);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormInsPlan));
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.labelCitySTZip = new System.Windows.Forms.Label();
			this.labelElectronicID = new System.Windows.Forms.Label();
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
			this.textEmployer = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.textAddress2 = new System.Windows.Forms.TextBox();
			this.label21 = new System.Windows.Forms.Label();
			this.tbPercentPlan = new OpenDental.TablePercent();
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
			this.label14 = new System.Windows.Forms.Label();
			this.textSubscriber = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label25 = new System.Windows.Forms.Label();
			this.textSubscriberID = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.textRenewMonth = new OpenDental.ValidNum();
			this.butEditAll = new OpenDental.UI.Button();
			this.comboLinked = new System.Windows.Forms.ComboBox();
			this.textLinkedNum = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
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
			this.panelSynch = new System.Windows.Forms.Panel();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.textPlanNote = new OpenDental.ODtextBox();
			this.panelAdvancedIns = new System.Windows.Forms.Panel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.butImportTrojan = new OpenDental.UI.Button();
			this.butIapFind = new OpenDental.UI.Button();
			this.butBenefitNotes = new OpenDental.UI.Button();
			this.labelDrop = new System.Windows.Forms.Label();
			this.butDrop = new OpenDental.UI.Button();
			this.butLabel = new OpenDental.UI.Button();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.label24 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.textTrojanID = new System.Windows.Forms.TextBox();
			this.label26 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.comboRelationship = new System.Windows.Forms.ComboBox();
			this.label31 = new System.Windows.Forms.Label();
			this.checkIsPending = new System.Windows.Forms.CheckBox();
			this.label32 = new System.Windows.Forms.Label();
			this.label33 = new System.Windows.Forms.Label();
			this.butAdjAdd = new OpenDental.UI.Button();
			this.listAdj = new System.Windows.Forms.ListBox();
			this.label35 = new System.Windows.Forms.Label();
			this.tbPercentPat = new OpenDental.TablePercent();
			this.label34 = new System.Windows.Forms.Label();
			this.textPatID = new System.Windows.Forms.TextBox();
			this.label36 = new System.Windows.Forms.Label();
			this.panelPat = new System.Windows.Forms.Panel();
			this.textOrdinal = new OpenDental.ValidNumber();
			this.panel4.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupCoPay.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.panelSynch.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.panelAdvancedIns.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.panelPat.SuspendLayout();
			this.SuspendLayout();
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(7, 53);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 15);
			this.label5.TabIndex = 5;
			this.label5.Text = "Effective Dates";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(187, 53);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(36, 15);
			this.label6.TabIndex = 6;
			this.label6.Text = "To";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(3, 32);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(95, 15);
			this.label7.TabIndex = 7;
			this.label7.Text = "Phone";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(13, 59);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(95, 15);
			this.label8.TabIndex = 8;
			this.label8.Text = "Group Name";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(13, 79);
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
			// labelElectronicID
			// 
			this.labelElectronicID.Location = new System.Drawing.Point(2, 111);
			this.labelElectronicID.Name = "labelElectronicID";
			this.labelElectronicID.Size = new System.Drawing.Size(95, 15);
			this.labelElectronicID.TabIndex = 15;
			this.labelElectronicID.Text = "Electronic ID";
			this.labelElectronicID.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
			this.label28.Location = new System.Drawing.Point(11, 356);
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
			this.textGroupName.Location = new System.Drawing.Point(109, 56);
			this.textGroupName.MaxLength = 50;
			this.textGroupName.Name = "textGroupName";
			this.textGroupName.Size = new System.Drawing.Size(193, 20);
			this.textGroupName.TabIndex = 1;
			this.textGroupName.Text = "";
			// 
			// textGroupNum
			// 
			this.textGroupNum.Location = new System.Drawing.Point(109, 76);
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
			this.butOK.Location = new System.Drawing.Point(811, 671);
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
			this.butCancel.Location = new System.Drawing.Point(901, 671);
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
			this.textDateEffect.Location = new System.Drawing.Point(109, 50);
			this.textDateEffect.Name = "textDateEffect";
			this.textDateEffect.Size = new System.Drawing.Size(72, 20);
			this.textDateEffect.TabIndex = 1;
			this.textDateEffect.Text = "";
			// 
			// textDateTerm
			// 
			this.textDateTerm.Location = new System.Drawing.Point(227, 50);
			this.textDateTerm.Name = "textDateTerm";
			this.textDateTerm.Size = new System.Drawing.Size(72, 20);
			this.textDateTerm.TabIndex = 2;
			this.textDateTerm.Text = "";
			// 
			// textEmployer
			// 
			this.textEmployer.Location = new System.Drawing.Point(109, 36);
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
			this.label16.Location = new System.Drawing.Point(13, 39);
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
			// tbPercentPlan
			// 
			this.tbPercentPlan.BackColor = System.Drawing.SystemColors.Window;
			this.tbPercentPlan.Location = new System.Drawing.Point(109, 100);
			this.tbPercentPlan.Name = "tbPercentPlan";
			this.tbPercentPlan.ScrollValue = 1;
			this.tbPercentPlan.SelectedIndices = new int[0];
			this.tbPercentPlan.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbPercentPlan.Size = new System.Drawing.Size(242, 86);
			this.tbPercentPlan.TabIndex = 22;
			// 
			// textAnnualMax
			// 
			this.textAnnualMax.Location = new System.Drawing.Point(109, 18);
			this.textAnnualMax.MaxVal = 255;
			this.textAnnualMax.MinVal = 0;
			this.textAnnualMax.Name = "textAnnualMax";
			this.textAnnualMax.Size = new System.Drawing.Size(60, 20);
			this.textAnnualMax.TabIndex = 0;
			this.textAnnualMax.Text = "";
			// 
			// textOrthoMax
			// 
			this.textOrthoMax.Location = new System.Drawing.Point(109, 38);
			this.textOrthoMax.MaxVal = 255;
			this.textOrthoMax.MinVal = 0;
			this.textOrthoMax.Name = "textOrthoMax";
			this.textOrthoMax.Size = new System.Drawing.Size(60, 20);
			this.textOrthoMax.TabIndex = 1;
			this.textOrthoMax.Text = "";
			// 
			// textDeductible
			// 
			this.textDeductible.Location = new System.Drawing.Point(109, 58);
			this.textDeductible.MaxVal = 255;
			this.textDeductible.MinVal = 0;
			this.textDeductible.Name = "textDeductible";
			this.textDeductible.Size = new System.Drawing.Size(45, 20);
			this.textDeductible.TabIndex = 2;
			this.textDeductible.Text = "";
			// 
			// textFloToAge
			// 
			this.textFloToAge.Location = new System.Drawing.Point(105, 32);
			this.textFloToAge.MaxVal = 255;
			this.textFloToAge.MinVal = 0;
			this.textFloToAge.Name = "textFloToAge";
			this.textFloToAge.Size = new System.Drawing.Size(36, 20);
			this.textFloToAge.TabIndex = 1;
			this.textFloToAge.Text = "";
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(6, 98);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(100, 53);
			this.label22.TabIndex = 21;
			this.label22.Text = "Plan Percentages (single click to edit)";
			this.label22.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 317);
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
			this.groupBox1.Location = new System.Drawing.Point(110, 297);
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
			this.label23.Location = new System.Drawing.Point(11, 341);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(95, 14);
			this.label23.TabIndex = 96;
			this.label23.Text = "Claim Form";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkAlternateCode
			// 
			this.checkAlternateCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAlternateCode.Location = new System.Drawing.Point(109, 269);
			this.checkAlternateCode.Name = "checkAlternateCode";
			this.checkAlternateCode.Size = new System.Drawing.Size(286, 17);
			this.checkAlternateCode.TabIndex = 5;
			this.checkAlternateCode.Text = "Use Alternate Code (for some Medicaid plans)";
			// 
			// checkClaimsUseUCR
			// 
			this.checkClaimsUseUCR.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkClaimsUseUCR.Location = new System.Drawing.Point(109, 299);
			this.checkClaimsUseUCR.Name = "checkClaimsUseUCR";
			this.checkClaimsUseUCR.Size = new System.Drawing.Size(302, 17);
			this.checkClaimsUseUCR.TabIndex = 6;
			this.checkClaimsUseUCR.Text = "Claims show UCR fee, not billed fee";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(14, 248);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(95, 15);
			this.label14.TabIndex = 104;
			this.label14.Text = "Plan Type";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textSubscriber
			// 
			this.textSubscriber.Location = new System.Drawing.Point(109, 10);
			this.textSubscriber.Name = "textSubscriber";
			this.textSubscriber.ReadOnly = true;
			this.textSubscriber.Size = new System.Drawing.Size(278, 20);
			this.textSubscriber.TabIndex = 109;
			this.textSubscriber.Text = "";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label25);
			this.groupBox2.Controls.Add(this.textSubscriber);
			this.groupBox2.Controls.Add(this.textSubscriberID);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.textDateEffect);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.textDateTerm);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(6, 139);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(413, 74);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Subscriber";
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(8, 14);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(99, 15);
			this.label25.TabIndex = 115;
			this.label25.Text = "Name";
			this.label25.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textSubscriberID
			// 
			this.textSubscriberID.Location = new System.Drawing.Point(109, 30);
			this.textSubscriberID.MaxLength = 20;
			this.textSubscriberID.Name = "textSubscriberID";
			this.textSubscriberID.Size = new System.Drawing.Size(129, 20);
			this.textSubscriberID.TabIndex = 0;
			this.textSubscriberID.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 32);
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
			this.butDelete.Location = new System.Drawing.Point(5, 671);
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
			// butEditAll
			// 
			this.butEditAll.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEditAll.Autosize = true;
			this.butEditAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEditAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEditAll.Location = new System.Drawing.Point(351, -1);
			this.butEditAll.Name = "butEditAll";
			this.butEditAll.Size = new System.Drawing.Size(58, 22);
			this.butEditAll.TabIndex = 71;
			this.butEditAll.Text = "Edit";
			this.toolTip1.SetToolTip(this.butEditAll, "Edit all the similar plans at once");
			this.butEditAll.Click += new System.EventHandler(this.butEditAll_Click);
			// 
			// comboLinked
			// 
			this.comboLinked.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboLinked.Location = new System.Drawing.Point(144, 0);
			this.comboLinked.MaxDropDownItems = 30;
			this.comboLinked.Name = "comboLinked";
			this.comboLinked.Size = new System.Drawing.Size(207, 21);
			this.comboLinked.TabIndex = 68;
			// 
			// textLinkedNum
			// 
			this.textLinkedNum.BackColor = System.Drawing.Color.White;
			this.textLinkedNum.Location = new System.Drawing.Point(111, 0);
			this.textLinkedNum.Name = "textLinkedNum";
			this.textLinkedNum.ReadOnly = true;
			this.textLinkedNum.Size = new System.Drawing.Size(35, 20);
			this.textLinkedNum.TabIndex = 67;
			this.textLinkedNum.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(1, 1);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(108, 17);
			this.label4.TabIndex = 66;
			this.label4.Text = "Identical Plans";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.comboPlanType);
			this.groupBox4.Controls.Add(this.checkIsMedical);
			this.groupBox4.Controls.Add(this.textDivisionNo);
			this.groupBox4.Controls.Add(this.labelDivisionDash);
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
			this.groupBox4.Controls.Add(this.label14);
			this.groupBox4.Controls.Add(this.checkAlternateCode);
			this.groupBox4.Controls.Add(this.panelSynch);
			this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox4.Location = new System.Drawing.Point(6, 216);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(413, 450);
			this.groupBox4.TabIndex = 1;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Synchronized Information";
			// 
			// comboPlanType
			// 
			this.comboPlanType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPlanType.Location = new System.Drawing.Point(109, 247);
			this.comboPlanType.Name = "comboPlanType";
			this.comboPlanType.Size = new System.Drawing.Size(212, 21);
			this.comboPlanType.TabIndex = 128;
			this.comboPlanType.SelectionChangeCommitted += new System.EventHandler(this.comboPlanType_SelectionChangeCommitted);
			// 
			// checkIsMedical
			// 
			this.checkIsMedical.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsMedical.Location = new System.Drawing.Point(109, 284);
			this.checkIsMedical.Name = "checkIsMedical";
			this.checkIsMedical.Size = new System.Drawing.Size(286, 17);
			this.checkIsMedical.TabIndex = 113;
			this.checkIsMedical.Text = "Medical Insurance";
			// 
			// textDivisionNo
			// 
			this.textDivisionNo.Location = new System.Drawing.Point(256, 77);
			this.textDivisionNo.MaxLength = 20;
			this.textDivisionNo.Name = "textDivisionNo";
			this.textDivisionNo.Size = new System.Drawing.Size(107, 20);
			this.textDivisionNo.TabIndex = 112;
			this.textDivisionNo.Text = "";
			// 
			// labelDivisionDash
			// 
			this.labelDivisionDash.Location = new System.Drawing.Point(241, 80);
			this.labelDivisionDash.Name = "labelDivisionDash";
			this.labelDivisionDash.Size = new System.Drawing.Size(31, 16);
			this.labelDivisionDash.TabIndex = 111;
			this.labelDivisionDash.Text = "--";
			// 
			// comboClaimForm
			// 
			this.comboClaimForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClaimForm.Location = new System.Drawing.Point(109, 338);
			this.comboClaimForm.MaxDropDownItems = 30;
			this.comboClaimForm.Name = "comboClaimForm";
			this.comboClaimForm.Size = new System.Drawing.Size(212, 21);
			this.comboClaimForm.TabIndex = 110;
			// 
			// comboFeeSched
			// 
			this.comboFeeSched.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboFeeSched.Location = new System.Drawing.Point(109, 316);
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
			this.groupCoPay.Location = new System.Drawing.Point(9, 361);
			this.groupCoPay.Name = "groupCoPay";
			this.groupCoPay.Size = new System.Drawing.Size(397, 85);
			this.groupCoPay.TabIndex = 107;
			this.groupCoPay.TabStop = false;
			this.groupCoPay.Text = "Other Fee Schedules";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(6, 63);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(168, 16);
			this.label12.TabIndex = 111;
			this.label12.Text = "Carrier Allowed Amounts";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboAllowedFeeSched
			// 
			this.comboAllowedFeeSched.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboAllowedFeeSched.Location = new System.Drawing.Point(176, 60);
			this.comboAllowedFeeSched.MaxDropDownItems = 30;
			this.comboAllowedFeeSched.Name = "comboAllowedFeeSched";
			this.comboAllowedFeeSched.Size = new System.Drawing.Size(209, 21);
			this.comboAllowedFeeSched.TabIndex = 110;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(6, 41);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(168, 16);
			this.label11.TabIndex = 109;
			this.label11.Text = "Patient Co-pay Amounts";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(1, 19);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(390, 17);
			this.label3.TabIndex = 106;
			this.label3.Text = "Don\'t use these unless you understand how they will affect your estimates";
			// 
			// comboCopay
			// 
			this.comboCopay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboCopay.Location = new System.Drawing.Point(176, 38);
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
			this.groupBox3.Controls.Add(this.labelElectronicID);
			this.groupBox3.Controls.Add(this.checkNoSendElect);
			this.groupBox3.Controls.Add(this.textPhone);
			this.groupBox3.Controls.Add(this.butSearch);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(9, 91);
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
			// panelSynch
			// 
			this.panelSynch.Controls.Add(this.butEditAll);
			this.panelSynch.Controls.Add(this.comboLinked);
			this.panelSynch.Controls.Add(this.textLinkedNum);
			this.panelSynch.Controls.Add(this.label4);
			this.panelSynch.Location = new System.Drawing.Point(-2, 16);
			this.panelSynch.Name = "panelSynch";
			this.panelSynch.Size = new System.Drawing.Size(409, 20);
			this.panelSynch.TabIndex = 127;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.textPlanNote);
			this.groupBox5.Controls.Add(this.groupBox1);
			this.groupBox5.Controls.Add(this.tbPercentPlan);
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
			this.groupBox5.Location = new System.Drawing.Point(457, 203);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(469, 462);
			this.groupBox5.TabIndex = 2;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Benefit Information";
			// 
			// textPlanNote
			// 
			this.textPlanNote.AcceptsReturn = true;
			this.textPlanNote.Location = new System.Drawing.Point(109, 355);
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
			this.panelAdvancedIns.Location = new System.Drawing.Point(6, 185);
			this.panelAdvancedIns.Name = "panelAdvancedIns";
			this.panelAdvancedIns.Size = new System.Drawing.Size(335, 112);
			this.panelAdvancedIns.TabIndex = 4;
			// 
			// butImportTrojan
			// 
			this.butImportTrojan.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butImportTrojan.Autosize = true;
			this.butImportTrojan.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butImportTrojan.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butImportTrojan.Location = new System.Drawing.Point(132, 10);
			this.butImportTrojan.Name = "butImportTrojan";
			this.butImportTrojan.Size = new System.Drawing.Size(82, 21);
			this.butImportTrojan.TabIndex = 72;
			this.butImportTrojan.Text = "Import";
			this.toolTip1.SetToolTip(this.butImportTrojan, "Edit all the similar plans at once");
			this.butImportTrojan.Click += new System.EventHandler(this.butImportTrojan_Click);
			// 
			// butIapFind
			// 
			this.butIapFind.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butIapFind.Autosize = true;
			this.butIapFind.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butIapFind.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butIapFind.Location = new System.Drawing.Point(132, 33);
			this.butIapFind.Name = "butIapFind";
			this.butIapFind.Size = new System.Drawing.Size(82, 21);
			this.butIapFind.TabIndex = 74;
			this.butIapFind.Text = "Find Plan";
			this.toolTip1.SetToolTip(this.butIapFind, "Edit all the similar plans at once");
			this.butIapFind.Click += new System.EventHandler(this.butIapFind_Click);
			// 
			// butBenefitNotes
			// 
			this.butBenefitNotes.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butBenefitNotes.Autosize = true;
			this.butBenefitNotes.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBenefitNotes.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBenefitNotes.Location = new System.Drawing.Point(378, 22);
			this.butBenefitNotes.Name = "butBenefitNotes";
			this.butBenefitNotes.Size = new System.Drawing.Size(82, 21);
			this.butBenefitNotes.TabIndex = 76;
			this.butBenefitNotes.Text = "View Benefits";
			this.toolTip1.SetToolTip(this.butBenefitNotes, "Edit all the similar plans at once");
			this.butBenefitNotes.Click += new System.EventHandler(this.butBenefitNotes_Click);
			// 
			// labelDrop
			// 
			this.labelDrop.Location = new System.Drawing.Point(74, 98);
			this.labelDrop.Name = "labelDrop";
			this.labelDrop.Size = new System.Drawing.Size(554, 15);
			this.labelDrop.TabIndex = 124;
			this.labelDrop.Text = "Drop a plan when a patient changes carriers or is no longer covered.  This does n" +
				"ot delete the plan.";
			this.labelDrop.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butDrop
			// 
			this.butDrop.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDrop.Autosize = true;
			this.butDrop.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDrop.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDrop.Location = new System.Drawing.Point(0, 93);
			this.butDrop.Name = "butDrop";
			this.butDrop.Size = new System.Drawing.Size(72, 21);
			this.butDrop.TabIndex = 123;
			this.butDrop.Text = "Drop";
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
			this.butLabel.Location = new System.Drawing.Point(449, 671);
			this.butLabel.Name = "butLabel";
			this.butLabel.Size = new System.Drawing.Size(81, 26);
			this.butLabel.TabIndex = 125;
			this.butLabel.Text = "Label";
			this.butLabel.Click += new System.EventHandler(this.butLabel_Click);
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.butBenefitNotes);
			this.groupBox6.Controls.Add(this.label24);
			this.groupBox6.Controls.Add(this.butIapFind);
			this.groupBox6.Controls.Add(this.label15);
			this.groupBox6.Controls.Add(this.butImportTrojan);
			this.groupBox6.Controls.Add(this.label13);
			this.groupBox6.Controls.Add(this.textTrojanID);
			this.groupBox6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox6.Location = new System.Drawing.Point(457, 132);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(468, 67);
			this.groupBox6.TabIndex = 126;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Request Benefits";
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(59, 15);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(71, 15);
			this.label24.TabIndex = 75;
			this.label24.Text = "Trojan:";
			this.label24.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(2, 36);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(128, 15);
			this.label15.TabIndex = 73;
			this.label15.Text = "Insurance Answers Plus:";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(218, 14);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(23, 15);
			this.label13.TabIndex = 9;
			this.label13.Text = "ID";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textTrojanID
			// 
			this.textTrojanID.Location = new System.Drawing.Point(245, 10);
			this.textTrojanID.MaxLength = 30;
			this.textTrojanID.Name = "textTrojanID";
			this.textTrojanID.Size = new System.Drawing.Size(109, 20);
			this.textTrojanID.TabIndex = 8;
			this.textTrojanID.Text = "";
			// 
			// label26
			// 
			this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label26.Location = new System.Drawing.Point(1, 57);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(148, 14);
			this.label26.TabIndex = 127;
			this.label26.Text = "Relationship to Subscriber";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ControlText;
			this.panel1.Location = new System.Drawing.Point(0, 116);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(988, 2);
			this.panel1.TabIndex = 128;
			// 
			// comboRelationship
			// 
			this.comboRelationship.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboRelationship.Location = new System.Drawing.Point(151, 53);
			this.comboRelationship.MaxDropDownItems = 30;
			this.comboRelationship.Name = "comboRelationship";
			this.comboRelationship.Size = new System.Drawing.Size(151, 21);
			this.comboRelationship.TabIndex = 129;
			// 
			// label31
			// 
			this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label31.Location = new System.Drawing.Point(0, 19);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(148, 14);
			this.label31.TabIndex = 130;
			this.label31.Text = "Order";
			this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkIsPending
			// 
			this.checkIsPending.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsPending.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsPending.Location = new System.Drawing.Point(39, 38);
			this.checkIsPending.Name = "checkIsPending";
			this.checkIsPending.Size = new System.Drawing.Size(125, 16);
			this.checkIsPending.TabIndex = 133;
			this.checkIsPending.Text = "Pending";
			this.checkIsPending.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label32
			// 
			this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label32.Location = new System.Drawing.Point(5, 121);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(304, 19);
			this.label32.TabIndex = 134;
			this.label32.Text = "Insurance Plan Information";
			// 
			// label33
			// 
			this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label33.Location = new System.Drawing.Point(0, 0);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(249, 19);
			this.label33.TabIndex = 135;
			this.label33.Text = "Patient Information";
			// 
			// butAdjAdd
			// 
			this.butAdjAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdjAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdjAdd.Autosize = true;
			this.butAdjAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdjAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdjAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butAdjAdd.Location = new System.Drawing.Point(608, 6);
			this.butAdjAdd.Name = "butAdjAdd";
			this.butAdjAdd.Size = new System.Drawing.Size(59, 21);
			this.butAdjAdd.TabIndex = 139;
			this.butAdjAdd.Text = "Add";
			this.butAdjAdd.Click += new System.EventHandler(this.butAdjAdd_Click);
			// 
			// listAdj
			// 
			this.listAdj.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.listAdj.Items.AddRange(new object[] {
																								 "03/05/2001       Ins Used:  $124.00       Ded Used:  $50.00",
																								 "03/05/2002       Ins Used:  $0.00       Ded Used:  $50.00"});
			this.listAdj.Location = new System.Drawing.Point(326, 28);
			this.listAdj.Name = "listAdj";
			this.listAdj.Size = new System.Drawing.Size(341, 56);
			this.listAdj.TabIndex = 137;
			this.listAdj.DoubleClick += new System.EventHandler(this.listAdj_DoubleClick);
			// 
			// label35
			// 
			this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label35.Location = new System.Drawing.Point(326, 8);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(218, 17);
			this.label35.TabIndex = 138;
			this.label35.Text = "Adjustments to Insurance Benefits: ";
			this.label35.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// tbPercentPat
			// 
			this.tbPercentPat.BackColor = System.Drawing.SystemColors.Window;
			this.tbPercentPat.Location = new System.Drawing.Point(691, 20);
			this.tbPercentPat.Name = "tbPercentPat";
			this.tbPercentPat.ScrollValue = 1;
			this.tbPercentPat.SelectedIndices = new int[0];
			this.tbPercentPat.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbPercentPat.Size = new System.Drawing.Size(242, 86);
			this.tbPercentPat.TabIndex = 141;
			// 
			// label34
			// 
			this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label34.Location = new System.Drawing.Point(691, -1);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(282, 18);
			this.label34.TabIndex = 142;
			this.label34.Text = "Override percentages for patient (single click to edit):";
			this.label34.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textPatID
			// 
			this.textPatID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textPatID.Location = new System.Drawing.Point(151, 75);
			this.textPatID.MaxLength = 100;
			this.textPatID.Name = "textPatID";
			this.textPatID.Size = new System.Drawing.Size(151, 20);
			this.textPatID.TabIndex = 144;
			this.textPatID.Text = "";
			// 
			// label36
			// 
			this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label36.Location = new System.Drawing.Point(11, 77);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(138, 16);
			this.label36.TabIndex = 143;
			this.label36.Text = "Optional Patient ID";
			this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panelPat
			// 
			this.panelPat.Controls.Add(this.textOrdinal);
			this.panelPat.Controls.Add(this.butAdjAdd);
			this.panelPat.Controls.Add(this.listAdj);
			this.panelPat.Controls.Add(this.label35);
			this.panelPat.Controls.Add(this.tbPercentPat);
			this.panelPat.Controls.Add(this.label34);
			this.panelPat.Controls.Add(this.textPatID);
			this.panelPat.Controls.Add(this.label36);
			this.panelPat.Controls.Add(this.labelDrop);
			this.panelPat.Controls.Add(this.butDrop);
			this.panelPat.Controls.Add(this.label26);
			this.panelPat.Controls.Add(this.comboRelationship);
			this.panelPat.Controls.Add(this.label31);
			this.panelPat.Controls.Add(this.checkIsPending);
			this.panelPat.Controls.Add(this.label33);
			this.panelPat.Location = new System.Drawing.Point(0, 0);
			this.panelPat.Name = "panelPat";
			this.panelPat.Size = new System.Drawing.Size(982, 116);
			this.panelPat.TabIndex = 145;
			// 
			// textOrdinal
			// 
			this.textOrdinal.Location = new System.Drawing.Point(151, 18);
			this.textOrdinal.MaxVal = 10;
			this.textOrdinal.MinVal = 1;
			this.textOrdinal.Name = "textOrdinal";
			this.textOrdinal.Size = new System.Drawing.Size(45, 20);
			this.textOrdinal.TabIndex = 145;
			this.textOrdinal.Text = "";
			// 
			// FormInsPlan
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(982, 700);
			this.Controls.Add(this.panelPat);
			this.Controls.Add(this.butLabel);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label32);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.groupBox6);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox2);
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
			this.groupBox4.ResumeLayout(false);
			this.groupCoPay.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.panelSynch.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.panelAdvancedIns.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.panelPat.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormInsPlan_Load(object sender, System.EventArgs e) {
			Cursor=Cursors.WaitCursor;
			if(IsNewPlan){
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
				panelSynch.Visible=false;
			}
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="CA"){//en-CA or fr-CA
				labelCitySTZip.Text="City,Prov,Post";
				labelElectronicID.Text="EDI Code";
			}
			else{
				labelDivisionDash.Visible=false;
				textDivisionNo.Visible=false;
			}
			FillPatData();
			FillAllPlanData();
			if(PlanCur.BenefitNotes==""){
				butBenefitNotes.Enabled=false;
			}
			Cursor=Cursors.Default;
		}

		///<summary>Fills the top patient portion of the form based on the data in PatPlanCur.</summary>
		private void FillPatData(){
			if(PatPlanCur==null){
				panelPat.Visible=false;
				return;
			}
			textOrdinal.Text=PatPlanCur.Ordinal.ToString();
			checkIsPending.Checked=PatPlanCur.IsPending;
			comboRelationship.Items.Clear();
			for(int i=0;i<Enum.GetNames(typeof(Relat)).Length;i++){
				comboRelationship.Items.Add(Lan.g("enumRelat",Enum.GetNames(typeof(Relat))[i]));
				if((int)PatPlanCur.Relationship==i){
					comboRelationship.SelectedIndex=i;
				}
			}
			textPatID.Text=PatPlanCur.PatID;
			//Fill Adjustments
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
			//Fill tbPercentPat
			tbPercentPat.ResetRows(CovPats.PriList.Length);//same as covcats.listshort.length
			tbPercentPat.SetGridColor(Color.LightGray);
			for(int i=0;i<CovCats.ListShort.Length;i++){
				tbPercentPat.Cell[0,i]=CovCats.ListShort[i].Description;
				tbPercentPat.Cell[1,i]="";
			}
			PatPlan[] patPlanList=PatPlans.Refresh(PatPlanCur.PatNum);
			CovListForAll=CovPats.GetList(patPlanList);
			for(int i=0;i<CovListForAll.Length;i++){
				if(CovListForAll[i].PatPlanNum==PatPlanCur.PatPlanNum){
					if(CovCats.GetOrderShort(CovListForAll[i].CovCatNum)!=-1){
						tbPercentPat.Cell[1,CovCats.GetOrderShort(CovListForAll[i].CovCatNum)]
							=CovListForAll[i].Percent.ToString();
					}
				}
			}
			tbPercentPat.LayoutTables();
		}

		///<summary>Fills the form based on the data in PlanCur.  Includes calls to FillSubscriber, FillCarrier, FillPercentages, and	LayoutSynch.</summary>
		private void FillAllPlanData(){
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
			FillSynchFormData();
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
			textPlanNote.Text=PlanCur.PlanNote;
			FillPercentagesPlan();
		}

		private void FillSynchFormData(){
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
			if(Prefs.GetBool("EasyHideCapitation")){
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
			FillCarrier();
			LayoutSynch();
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

		///<summary>Gets percentages from the db and then displays them on the form.</summary>
		private void FillPercentagesPlan(){
			CovListForPlan=CovPats.GetListForPlan(PlanCur.PlanNum);
			tbPercentPlan.ResetRows(CovCats.ListShort.Length);
			tbPercentPlan.SetGridColor(Color.LightGray);
			for(int i=0;i<CovCats.ListShort.Length;i++){
				tbPercentPlan.Cell[0,i]=CovCats.ListShort[i].Description;
				tbPercentPlan.Cell[1,i]="";
			}
			for(int i=0;i<CovListForPlan.Length;i++){
				if(CovCats.GetOrderShort(CovListForPlan[i].CovCatNum)!=-1){
					tbPercentPlan.Cell[1,CovCats.GetOrderShort(CovListForPlan[i].CovCatNum)]=CovListForPlan[i].Percent.ToString();
				}
			}
			tbPercentPlan.LayoutTables();
		}

		private void comboPlanType_SelectionChangeCommitted(object sender, System.EventArgs e) {
			//MessageBox.Show(InsPlans.Cur.PlanType+","+listPlanType.SelectedIndex.ToString());
			if(PlanCur.PlanType=="" && (comboPlanType.SelectedIndex==1 || comboPlanType.SelectedIndex==2)) {
				if(!MsgBox.Show(this,true,"This will clear all percentages. Continue?")){
					comboPlanType.SelectedIndex=0;
					return;
				}
				for(int i=0;i<CovListForPlan.Length;i++){
					CovPats.Cur=CovListForPlan[i];
					CovPats.DeleteCur();
				}
				FillPercentagesPlan();
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

		private void tbPercentPat_CellClicked(object sender, CellEventArgs e){
			bool isNew=true;
			//CovCats.GetOrderShort(CovPats.List[i].CovCatNum)
			for(int i=0;i<CovListForAll.Length;i++){
				if(CovListForAll[i].PatPlanNum==PatPlanCur.PatPlanNum
					&& CovCats.GetOrderShort(CovListForAll[i].CovCatNum)==e.Row)
				{
					isNew=false;
					CovPats.Cur=CovListForAll[i];
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
				CovPats.Cur.PatPlanNum=PatPlanCur.PatPlanNum;
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
			FillPatData();
		}

		private void tbPercentPlan_CellClicked(object sender, CellEventArgs e){
			bool isNew=true;
			for(int i=0;i<CovListForPlan.Length;i++){
				if(CovCats.GetOrderShort(CovListForPlan[i].CovCatNum)==e.Row){
					isNew=false;
					CovPats.Cur=CovListForPlan[i];
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
			FillPercentagesPlan();
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
			FillPatData();
		}

		private void listAdj_DoubleClick(object sender, System.EventArgs e) {
			if(listAdj.SelectedIndex==-1){
				return;
			}
			FormInsAdj FormIA=new FormInsAdj((ClaimProc)AdjAL[listAdj.SelectedIndex]);
			FormIA.ShowDialog();
			FillPatData();
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

		/*
		private void butSelect_Click(object sender, System.EventArgs e) {
			//no longer need to save entered info into PlanCur because we will only fill synch data, leaving the rest alone
			//if(textSubscriberID.Text!="" && textCarrier.Text!=""){
			//	if(!FillCur())
			//		return;
			//}
			FormInsPlans FormIP=new FormInsPlans();
			FormIP.IsSelectMode=true;
			FormIP.ShowDialog();
			if(FormIP.DialogResult!=DialogResult.OK){
				return;
			}
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
			PlanCur.Update();//updates to the db so that the synch info will show correctly
			FillSynchFormData();
		}*/

		private void butEditAll_Click(object sender, System.EventArgs e) {
			//notice that this is called without saving any changes that have been made so far.
			//int curPlanNum=InsPlans.Cur.PlanNum;
			FormInsPlanEditAll FormIPE=new FormInsPlanEditAll(PlanCur.Copy());
			FormIPE.ShowDialog();
			if(FormIPE.DialogResult!=DialogResult.OK){
				return;
			}
			PlanCur=InsPlans.GetPlan(PlanCur.PlanNum,new InsPlan[] {});
			FillSynchFormData();
		}

		private void butImportTrojan_Click(object sender, System.EventArgs e) {
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
			//clear exising percentages:
			for(int i=0;i<CovListForPlan.Length;i++){
				CovPats.Cur=CovListForPlan[i];
				CovPats.DeleteCur();
			}
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
								textAnnualMax.Text=fields[2];
								break;
							case "PLANYR"://eg Calendar year or Anniversary year
								if(fields[2]!="Calendar year"){
									MessageBox.Show("Warning.  Plan uses Anniversary year rather than Calendar year.  Please verify the Renew Month.");
								}
								break;
							case "DEDUCT"://eg There is no deductible
								if(!fields[2].StartsWith("$")){
									textDeductible.Text="0";
									break;
								}
								fields[2]=fields[2].Remove(0,1);
								fields[2]=fields[2].Split(new char[] {' '})[0];
								textDeductible.Text=fields[2];
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
								CovPats.Cur=new CovPat();
								CovPats.Cur.CovCatNum=CovCats.ListShort[0].CovCatNum;
								CovPats.Cur.PlanNum=PlanCur.PlanNum;
								CovPats.Cur.Percent=percent;
								CovPats.InsertCur();
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
								CovPats.Cur=new CovPat();
								CovPats.Cur.CovCatNum=CovCats.ListShort[1].CovCatNum;//basic
								CovPats.Cur.PlanNum=PlanCur.PlanNum;
								CovPats.Cur.Percent=percent;
								CovPats.InsertCur();
								CovPats.Cur=new CovPat();
								CovPats.Cur.CovCatNum=CovCats.ListShort[3].CovCatNum;//endo
								CovPats.Cur.PlanNum=PlanCur.PlanNum;
								CovPats.Cur.Percent=percent;
								CovPats.InsertCur();
								CovPats.Cur=new CovPat();
								CovPats.Cur.CovCatNum=CovCats.ListShort[4].CovCatNum;//perio
								CovPats.Cur.PlanNum=PlanCur.PlanNum;
								CovPats.Cur.Percent=percent;
								CovPats.InsertCur();
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
								CovPats.Cur=new CovPat();
								CovPats.Cur.CovCatNum=CovCats.ListShort[2].CovCatNum;
								CovPats.Cur.PlanNum=PlanCur.PlanNum;
								CovPats.Cur.Percent=percent;
								CovPats.InsertCur();
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
			FillPercentagesPlan();
		}

		private void butIapFind_Click(object sender, System.EventArgs e) {
			FormIap FormI=new FormIap();
			FormI.ShowDialog();
			if(FormI.DialogResult==DialogResult.Cancel){
				return;
			}
			//clear exising percentages:
			for(int i=0;i<CovListForPlan.Length;i++){
				CovPats.Cur=CovListForPlan[i];
				CovPats.DeleteCur();
			}
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
								textDeductible.Text=splitField[0].Remove(0,1);//removes the $
							}
							break;
						case Iap.FamilyDed:
							PlanCur.BenefitNotes+="\r\n"+"FamilyDed: "+field;
							break;
						case Iap.Maximum:
							PlanCur.BenefitNotes+="\r\n"+"Maximum: "+field;
							if(field.StartsWith("$")){
								splitField=field.Split(new char[] {' '});
								textAnnualMax.Text=splitField[0].Remove(0,1);//removes the $
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
							CovPats.Cur=new CovPat();
							CovPats.Cur.CovCatNum=CovCats.ListShort[0].CovCatNum;
							CovPats.Cur.PlanNum=PlanCur.PlanNum;
							CovPats.Cur.Percent=percent;
							CovPats.InsertCur();
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
							CovPats.Cur=new CovPat();
							CovPats.Cur.CovCatNum=CovCats.ListShort[1].CovCatNum;//basic
							CovPats.Cur.PlanNum=PlanCur.PlanNum;
							CovPats.Cur.Percent=percent;
							CovPats.InsertCur();
							CovPats.Cur=new CovPat();
							CovPats.Cur.CovCatNum=CovCats.ListShort[3].CovCatNum;//endo
							CovPats.Cur.PlanNum=PlanCur.PlanNum;
							CovPats.Cur.Percent=percent;
							CovPats.InsertCur();
							CovPats.Cur=new CovPat();
							CovPats.Cur.CovCatNum=CovCats.ListShort[4].CovCatNum;//perio
							CovPats.Cur.PlanNum=PlanCur.PlanNum;
							CovPats.Cur.Percent=percent;
							CovPats.InsertCur();
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
							CovPats.Cur=new CovPat();
							CovPats.Cur.CovCatNum=CovCats.ListShort[2].CovCatNum;
							CovPats.Cur.PlanNum=PlanCur.PlanNum;
							CovPats.Cur.Percent=percent;
							CovPats.InsertCur();
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
							CovPats.Cur=new CovPat();
							CovPats.Cur.CovCatNum=CovCats.ListShort[5].CovCatNum;
							CovPats.Cur.PlanNum=PlanCur.PlanNum;
							CovPats.Cur.Percent=percent;
							CovPats.InsertCur();
							break;
						case Iap.Deductible2:
							PlanCur.BenefitNotes+="\r\n"+"Deductible2: "+field;
							break;
						case Iap.Maximum2://ortho Max
							PlanCur.BenefitNotes+="\r\n"+"Maximum2: "+field;
							if(field.StartsWith("$")){
								splitField=field.Split(new char[] {' '});
								textOrthoMax.Text=splitField[0].Remove(0,1);//removes the $
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
			FillPercentagesPlan();
		}

		private void butBenefitNotes_Click(object sender, System.EventArgs e) {
			FormInsBenefitNotes FormI=new FormInsBenefitNotes();
			FormI.BenefitNotes=PlanCur.BenefitNotes;
			FormI.ShowDialog();
			if(FormI.DialogResult==DialogResult.Cancel){
				return;
			}
			PlanCur.BenefitNotes=FormI.BenefitNotes;
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
				PlanCur.Delete();//checks dependencies first. Also deletes patplan and recomputes all estimates.
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
		private bool FillPlanCur(){
			if(textDateEffect.errorProvider1.GetError(textDateEffect)!=""
				|| textDateTerm.errorProvider1.GetError(textDateTerm)!=""
				|| textAnnualMax.errorProvider1.GetError(textAnnualMax)!=""
				|| textRenewMonth.errorProvider1.GetError(textRenewMonth)!=""
				|| textDeductible.errorProvider1.GetError(textDeductible)!=""
				|| textOrthoMax.errorProvider1.GetError(textOrthoMax)!=""
				|| textFloToAge.errorProvider1.GetError(textFloToAge)!=""
				){
				MsgBox.Show(this,"Please fix data entry errors first.");
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
			//Subscriber: Can never be changed once a plan is created.
			PlanCur.SubscriberID =textSubscriberID.Text;
			PlanCur.DateEffective=PIn.PDate(textDateEffect.Text);
			PlanCur.DateTerm     =PIn.PDate(textDateTerm.Text);
			//Synchronized Information:
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
				PlanCur.AllowedFeeSched
					=Defs.Short[(int)DefCat.FeeSchedNames][comboAllowedFeeSched.SelectedIndex-1].DefNum;
			//end of Plan Information
			PlanCur.TrojanID=textTrojanID.Text;
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
			PlanCur.Update();
			if(PatPlanCur!=null){
				if(!FillPatPlanCur()){
					Cursor=Cursors.Default;
					return;
				}
				PatPlanCur.Update();
			}
			PlanCur.ComputeEstimatesForCur();
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
