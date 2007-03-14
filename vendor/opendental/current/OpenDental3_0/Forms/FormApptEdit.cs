using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace OpenDental{
	///<summary></summary>
	public class FormApptEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.TextBox textTime;
		private System.Windows.Forms.Button butCalcTime;
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ListBox listProvNum;
		private System.Windows.Forms.ListBox listProvHyg;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox listConfirmed;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button butHygClear;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ListBox listQuickAdd;
		private OpenDental.TableApptProcs tbProc;// Required designer variable.
		///<summary></summary>
		public bool IsNew;
		private Procedure[] arrayProc;
		private ApptProc[] ApptProc2;
		private OpenDental.ValidNum textAddTime;
		private OpenDental.TableTimeBar tbTime;
		private System.Windows.Forms.Button butSlider;
		private bool mouseIsDown;
		private Point	mouseOrigin;
		private Point sliderOrigin;
		private StringBuilder strBTime;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.GroupBox groupFinancial;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.TextBox textWirelessPhone;
		private System.Windows.Forms.TextBox textWkPhone;
		private System.Windows.Forms.TextBox textHmPhone;
		private System.Windows.Forms.GroupBox groupContact;
		private System.Windows.Forms.TextBox textAddrNote;
		private OpenDental.XPButton butAddComm;
		private OpenDental.TableCommLog tbCommlog;
		private bool procsHaveChanged;
		private System.Windows.Forms.CheckBox checkIsNewPatient;
		private System.Windows.Forms.Label labelStatus;
		private System.Windows.Forms.ComboBox comboStatus;
		private System.Windows.Forms.ComboBox comboUnschedStatus;
		private System.Windows.Forms.Label label4;
		private OpenDental.XPButton butPin;
		private ArrayList ALCommItems;
		///<summary>Allows the parent form to specify if the pinboard button will be visible.</summary>
		public bool PinIsVisible;
		private OpenDental.XPButton butDelete;
		private System.Windows.Forms.TextBox textBalance;
		private System.Windows.Forms.TextBox textCreditType;
		private System.Windows.Forms.TextBox textFeeTotal;
		private System.Windows.Forms.TextBox textBillingType;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textFamilyBal;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ListBox listAssistant;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.ListBox listLab;
		///<summary>If the user clicks on the pinboard button this will be set to true.</summary>
		public bool PinClicked;
	

		///<summary></summary>
		public FormApptEdit(){
			InitializeComponent();// Required for Windows Form Designer support
			tbTime.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbTime_CellClicked);
			tbProc.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbProc_CellClicked);
			tbCommlog.CellDoubleClicked+=new OpenDental.ContrTable.CellEventHandler(tbCommlog_CellDoubleClicked);
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label1,
				this.label2,
				this.label3,
				this.labelStatus,
				this.label5,
				this.label6,
				this.label10,
				this.label11,
				this.label16,
				this.label7,
				this.label9,
				this.butCalcTime,
				this.butHygClear,
				this.butSlider,
				this.groupFinancial,
				this.label13
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

		private void InitializeComponent(){
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormApptEdit));
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.listQuickAdd = new System.Windows.Forms.ListBox();
			this.tbTime = new OpenDental.TableTimeBar();
			this.textTime = new System.Windows.Forms.TextBox();
			this.butCalcTime = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.listProvNum = new System.Windows.Forms.ListBox();
			this.listProvHyg = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.butHygClear = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.labelStatus = new System.Windows.Forms.Label();
			this.listConfirmed = new System.Windows.Forms.ListBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.tbProc = new OpenDental.TableApptProcs();
			this.textBalance = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.textCreditType = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.textFeeTotal = new System.Windows.Forms.TextBox();
			this.textAddTime = new OpenDental.ValidNum();
			this.butSlider = new System.Windows.Forms.Button();
			this.label14 = new System.Windows.Forms.Label();
			this.groupFinancial = new System.Windows.Forms.GroupBox();
			this.textFamilyBal = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textBillingType = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.tbCommlog = new OpenDental.TableCommLog();
			this.groupContact = new System.Windows.Forms.GroupBox();
			this.textAddrNote = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.textWirelessPhone = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.textWkPhone = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.textHmPhone = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.butAddComm = new OpenDental.XPButton();
			this.checkIsNewPatient = new System.Windows.Forms.CheckBox();
			this.comboStatus = new System.Windows.Forms.ComboBox();
			this.comboUnschedStatus = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.butPin = new OpenDental.XPButton();
			this.butDelete = new OpenDental.XPButton();
			this.label12 = new System.Windows.Forms.Label();
			this.listAssistant = new System.Windows.Forms.ListBox();
			this.label13 = new System.Windows.Forms.Label();
			this.listLab = new System.Windows.Forms.ListBox();
			this.groupFinancial.SuspendLayout();
			this.groupContact.SuspendLayout();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(792, 661);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 7;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(886, 661);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 8;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// listQuickAdd
			// 
			this.listQuickAdd.Location = new System.Drawing.Point(155, 43);
			this.listQuickAdd.Name = "listQuickAdd";
			this.listQuickAdd.Size = new System.Drawing.Size(146, 225);
			this.listQuickAdd.TabIndex = 3;
			this.listQuickAdd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listQuickAdd_MouseDown);
			this.listQuickAdd.SelectedIndexChanged += new System.EventHandler(this.listQuickAdd_SelectedIndexChanged);
			// 
			// tbTime
			// 
			this.tbTime.BackColor = System.Drawing.SystemColors.Window;
			this.tbTime.Location = new System.Drawing.Point(3, 7);
			this.tbTime.Name = "tbTime";
			this.tbTime.ScrollValue = 150;
			this.tbTime.SelectedIndices = new int[0];
			this.tbTime.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbTime.Size = new System.Drawing.Size(15, 561);
			this.tbTime.TabIndex = 4;
			// 
			// textTime
			// 
			this.textTime.BackColor = System.Drawing.Color.White;
			this.textTime.Location = new System.Drawing.Point(23, 550);
			this.textTime.Name = "textTime";
			this.textTime.ReadOnly = true;
			this.textTime.Size = new System.Drawing.Size(66, 20);
			this.textTime.TabIndex = 5;
			this.textTime.Text = "";
			// 
			// butCalcTime
			// 
			this.butCalcTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCalcTime.Location = new System.Drawing.Point(3, 574);
			this.butCalcTime.Name = "butCalcTime";
			this.butCalcTime.Size = new System.Drawing.Size(87, 26);
			this.butCalcTime.TabIndex = 6;
			this.butCalcTime.Text = "Calc &Time";
			this.butCalcTime.Click += new System.EventHandler(this.butCalcTime_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(4, 605);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 14);
			this.label1.TabIndex = 7;
			this.label1.Text = "Adj Time:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listProvNum
			// 
			this.listProvNum.Location = new System.Drawing.Point(681, 42);
			this.listProvNum.Name = "listProvNum";
			this.listProvNum.Size = new System.Drawing.Size(82, 69);
			this.listProvNum.TabIndex = 4;
			// 
			// listProvHyg
			// 
			this.listProvHyg.Location = new System.Drawing.Point(770, 42);
			this.listProvHyg.Name = "listProvHyg";
			this.listProvHyg.Size = new System.Drawing.Size(82, 69);
			this.listProvHyg.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(680, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 16);
			this.label2.TabIndex = 11;
			this.label2.Text = "Dentist";
			// 
			// butHygClear
			// 
			this.butHygClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butHygClear.Location = new System.Drawing.Point(778, 115);
			this.butHygClear.Name = "butHygClear";
			this.butHygClear.Size = new System.Drawing.Size(66, 21);
			this.butHygClear.TabIndex = 12;
			this.butHygClear.Text = "&none";
			this.butHygClear.Click += new System.EventHandler(this.butHygClear_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(769, 26);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(98, 16);
			this.label3.TabIndex = 13;
			this.label3.Text = "Hygienist";
			// 
			// labelStatus
			// 
			this.labelStatus.Location = new System.Drawing.Point(22, 8);
			this.labelStatus.Name = "labelStatus";
			this.labelStatus.Size = new System.Drawing.Size(118, 15);
			this.labelStatus.TabIndex = 15;
			this.labelStatus.Text = "Status";
			// 
			// listConfirmed
			// 
			this.listConfirmed.Location = new System.Drawing.Point(22, 121);
			this.listConfirmed.Name = "listConfirmed";
			this.listConfirmed.Size = new System.Drawing.Size(121, 147);
			this.listConfirmed.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(22, 102);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(118, 16);
			this.label5.TabIndex = 17;
			this.label5.Text = "Confirmed";
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(22, 294);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(274, 114);
			this.textNote.TabIndex = 2;
			this.textNote.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(22, 276);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(197, 16);
			this.label6.TabIndex = 20;
			this.label6.Text = "Appointment Note";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(153, 2);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(143, 39);
			this.label7.TabIndex = 21;
			this.label7.Text = "Single click on items in this list to add them to the treatment plan.";
			// 
			// tbProc
			// 
			this.tbProc.BackColor = System.Drawing.SystemColors.Window;
			this.tbProc.Location = new System.Drawing.Point(312, 43);
			this.tbProc.Name = "tbProc";
			this.tbProc.ScrollValue = 1;
			this.tbProc.SelectedIndices = new int[0];
			this.tbProc.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbProc.Size = new System.Drawing.Size(364, 365);
			this.tbProc.TabIndex = 22;
			// 
			// textBalance
			// 
			this.textBalance.Location = new System.Drawing.Point(135, 61);
			this.textBalance.Name = "textBalance";
			this.textBalance.ReadOnly = true;
			this.textBalance.Size = new System.Drawing.Size(52, 20);
			this.textBalance.TabIndex = 58;
			this.textBalance.Text = "";
			this.textBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(28, 63);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(107, 16);
			this.label16.TabIndex = 59;
			this.label16.Text = "Patient Balance";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCreditType
			// 
			this.textCreditType.Location = new System.Drawing.Point(135, 21);
			this.textCreditType.Name = "textCreditType";
			this.textCreditType.ReadOnly = true;
			this.textCreditType.Size = new System.Drawing.Size(28, 20);
			this.textCreditType.TabIndex = 54;
			this.textCreditType.Text = "";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(32, 25);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(102, 14);
			this.label11.TabIndex = 52;
			this.label11.Text = "Credit Type";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(16, 123);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(119, 15);
			this.label9.TabIndex = 46;
			this.label9.Text = "Fee This Appt";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textFeeTotal
			// 
			this.textFeeTotal.Location = new System.Drawing.Point(135, 121);
			this.textFeeTotal.Name = "textFeeTotal";
			this.textFeeTotal.ReadOnly = true;
			this.textFeeTotal.Size = new System.Drawing.Size(52, 20);
			this.textFeeTotal.TabIndex = 49;
			this.textFeeTotal.Text = "";
			this.textFeeTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textAddTime
			// 
			this.textAddTime.Location = new System.Drawing.Point(4, 622);
			this.textAddTime.MinVal = 0;
			this.textAddTime.Name = "textAddTime";
			this.textAddTime.Size = new System.Drawing.Size(65, 20);
			this.textAddTime.TabIndex = 3;
			this.textAddTime.Text = "";
			// 
			// butSlider
			// 
			this.butSlider.BackColor = System.Drawing.SystemColors.ControlDark;
			this.butSlider.Location = new System.Drawing.Point(5, 91);
			this.butSlider.Name = "butSlider";
			this.butSlider.Size = new System.Drawing.Size(12, 15);
			this.butSlider.TabIndex = 58;
			this.butSlider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.butSlider_MouseUp);
			this.butSlider.MouseMove += new System.Windows.Forms.MouseEventHandler(this.butSlider_MouseMove);
			this.butSlider.MouseDown += new System.Windows.Forms.MouseEventHandler(this.butSlider_MouseDown);
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(312, 13);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(345, 29);
			this.label14.TabIndex = 61;
			this.label14.Text = "From the treatment plan list below, highlight the procedures to attach to this ap" +
				"pointment";
			// 
			// groupFinancial
			// 
			this.groupFinancial.Controls.Add(this.textFamilyBal);
			this.groupFinancial.Controls.Add(this.label10);
			this.groupFinancial.Controls.Add(this.textBillingType);
			this.groupFinancial.Controls.Add(this.label8);
			this.groupFinancial.Controls.Add(this.textBalance);
			this.groupFinancial.Controls.Add(this.textFeeTotal);
			this.groupFinancial.Controls.Add(this.label9);
			this.groupFinancial.Controls.Add(this.label11);
			this.groupFinancial.Controls.Add(this.textCreditType);
			this.groupFinancial.Controls.Add(this.label16);
			this.groupFinancial.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupFinancial.Location = new System.Drawing.Point(681, 227);
			this.groupFinancial.Name = "groupFinancial";
			this.groupFinancial.Size = new System.Drawing.Size(287, 185);
			this.groupFinancial.TabIndex = 63;
			this.groupFinancial.TabStop = false;
			this.groupFinancial.Text = "Financial";
			// 
			// textFamilyBal
			// 
			this.textFamilyBal.Location = new System.Drawing.Point(135, 81);
			this.textFamilyBal.Name = "textFamilyBal";
			this.textFamilyBal.ReadOnly = true;
			this.textFamilyBal.Size = new System.Drawing.Size(52, 20);
			this.textFamilyBal.TabIndex = 62;
			this.textFamilyBal.Text = "";
			this.textFamilyBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(19, 83);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(116, 16);
			this.label10.TabIndex = 63;
			this.label10.Text = "Family Balance";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textBillingType
			// 
			this.textBillingType.Location = new System.Drawing.Point(135, 41);
			this.textBillingType.Name = "textBillingType";
			this.textBillingType.ReadOnly = true;
			this.textBillingType.Size = new System.Drawing.Size(119, 20);
			this.textBillingType.TabIndex = 60;
			this.textBillingType.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(28, 43);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(107, 16);
			this.label8.TabIndex = 61;
			this.label8.Text = "Billing Type";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// tbCommlog
			// 
			this.tbCommlog.BackColor = System.Drawing.SystemColors.Window;
			this.tbCommlog.Location = new System.Drawing.Point(112, 413);
			this.tbCommlog.Name = "tbCommlog";
			this.tbCommlog.ScrollValue = 461;
			this.tbCommlog.SelectedIndices = new int[0];
			this.tbCommlog.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbCommlog.Size = new System.Drawing.Size(619, 274);
			this.tbCommlog.TabIndex = 64;
			// 
			// groupContact
			// 
			this.groupContact.Controls.Add(this.textAddrNote);
			this.groupContact.Controls.Add(this.label19);
			this.groupContact.Controls.Add(this.textWirelessPhone);
			this.groupContact.Controls.Add(this.label18);
			this.groupContact.Controls.Add(this.textWkPhone);
			this.groupContact.Controls.Add(this.label17);
			this.groupContact.Controls.Add(this.textHmPhone);
			this.groupContact.Controls.Add(this.label15);
			this.groupContact.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupContact.Location = new System.Drawing.Point(747, 415);
			this.groupContact.Name = "groupContact";
			this.groupContact.Size = new System.Drawing.Size(221, 171);
			this.groupContact.TabIndex = 65;
			this.groupContact.TabStop = false;
			this.groupContact.Text = "Contact Info";
			// 
			// textAddrNote
			// 
			this.textAddrNote.BackColor = System.Drawing.SystemColors.Control;
			this.textAddrNote.ForeColor = System.Drawing.Color.DarkRed;
			this.textAddrNote.Location = new System.Drawing.Point(5, 100);
			this.textAddrNote.Multiline = true;
			this.textAddrNote.Name = "textAddrNote";
			this.textAddrNote.Size = new System.Drawing.Size(211, 64);
			this.textAddrNote.TabIndex = 7;
			this.textAddrNote.Text = "";
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(6, 82);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(168, 13);
			this.label19.TabIndex = 6;
			this.label19.Text = "Address and Phone Notes";
			this.label19.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textWirelessPhone
			// 
			this.textWirelessPhone.Location = new System.Drawing.Point(115, 57);
			this.textWirelessPhone.Name = "textWirelessPhone";
			this.textWirelessPhone.ReadOnly = true;
			this.textWirelessPhone.TabIndex = 5;
			this.textWirelessPhone.Text = "";
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(15, 60);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(100, 13);
			this.label18.TabIndex = 4;
			this.label18.Text = "Wireless Phone";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textWkPhone
			// 
			this.textWkPhone.Location = new System.Drawing.Point(115, 37);
			this.textWkPhone.Name = "textWkPhone";
			this.textWkPhone.ReadOnly = true;
			this.textWkPhone.TabIndex = 3;
			this.textWkPhone.Text = "";
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(16, 41);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(100, 13);
			this.label17.TabIndex = 2;
			this.label17.Text = "Work Phone";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textHmPhone
			// 
			this.textHmPhone.Location = new System.Drawing.Point(115, 17);
			this.textHmPhone.Name = "textHmPhone";
			this.textHmPhone.ReadOnly = true;
			this.textHmPhone.TabIndex = 1;
			this.textHmPhone.Text = "";
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(17, 22);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(100, 13);
			this.label15.TabIndex = 0;
			this.label15.Text = "Home Phone";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butAddComm
			// 
			this.butAddComm.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddComm.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butAddComm.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butAddComm.Image = ((System.Drawing.Image)(resources.GetObject("butAddComm.Image")));
			this.butAddComm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddComm.Location = new System.Drawing.Point(22, 412);
			this.butAddComm.Name = "butAddComm";
			this.butAddComm.Size = new System.Drawing.Size(85, 26);
			this.butAddComm.TabIndex = 66;
			this.butAddComm.Text = "Co&mm";
			this.butAddComm.Click += new System.EventHandler(this.butAddComm_Click);
			// 
			// checkIsNewPatient
			// 
			this.checkIsNewPatient.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsNewPatient.Location = new System.Drawing.Point(682, 4);
			this.checkIsNewPatient.Name = "checkIsNewPatient";
			this.checkIsNewPatient.Size = new System.Drawing.Size(208, 17);
			this.checkIsNewPatient.TabIndex = 67;
			this.checkIsNewPatient.Text = "New Patient";
			// 
			// comboStatus
			// 
			this.comboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatus.Location = new System.Drawing.Point(22, 24);
			this.comboStatus.MaxDropDownItems = 10;
			this.comboStatus.Name = "comboStatus";
			this.comboStatus.Size = new System.Drawing.Size(121, 21);
			this.comboStatus.TabIndex = 68;
			// 
			// comboUnschedStatus
			// 
			this.comboUnschedStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboUnschedStatus.Location = new System.Drawing.Point(22, 69);
			this.comboUnschedStatus.MaxDropDownItems = 100;
			this.comboUnschedStatus.Name = "comboUnschedStatus";
			this.comboUnschedStatus.Size = new System.Drawing.Size(121, 21);
			this.comboUnschedStatus.TabIndex = 70;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(22, 53);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(126, 15);
			this.label4.TabIndex = 69;
			this.label4.Text = "Unscheduled Status";
			// 
			// butPin
			// 
			this.butPin.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPin.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butPin.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butPin.Image = ((System.Drawing.Image)(resources.GetObject("butPin.Image")));
			this.butPin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPin.Location = new System.Drawing.Point(792, 616);
			this.butPin.Name = "butPin";
			this.butPin.Size = new System.Drawing.Size(100, 26);
			this.butPin.TabIndex = 71;
			this.butPin.Text = "&Pinboard";
			this.butPin.Click += new System.EventHandler(this.butPin_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(3, 662);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75, 26);
			this.butDelete.TabIndex = 72;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(858, 26);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(88, 16);
			this.label12.TabIndex = 74;
			this.label12.Text = "Assistant";
			// 
			// listAssistant
			// 
			this.listAssistant.Location = new System.Drawing.Point(859, 42);
			this.listAssistant.Name = "listAssistant";
			this.listAssistant.Size = new System.Drawing.Size(82, 69);
			this.listAssistant.TabIndex = 73;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(861, 145);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(88, 16);
			this.label13.TabIndex = 76;
			this.label13.Text = "Lab Case";
			// 
			// listLab
			// 
			this.listLab.Location = new System.Drawing.Point(862, 161);
			this.listLab.Name = "listLab";
			this.listLab.Size = new System.Drawing.Size(82, 56);
			this.listLab.TabIndex = 75;
			// 
			// FormApptEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(974, 695);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.listLab);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.listAssistant);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butPin);
			this.Controls.Add(this.comboUnschedStatus);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.comboStatus);
			this.Controls.Add(this.checkIsNewPatient);
			this.Controls.Add(this.butAddComm);
			this.Controls.Add(this.groupContact);
			this.Controls.Add(this.tbCommlog);
			this.Controls.Add(this.groupFinancial);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.butSlider);
			this.Controls.Add(this.textAddTime);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.textTime);
			this.Controls.Add(this.tbProc);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.listConfirmed);
			this.Controls.Add(this.labelStatus);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.butHygClear);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listProvHyg);
			this.Controls.Add(this.listProvNum);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butCalcTime);
			this.Controls.Add(this.tbTime);
			this.Controls.Add(this.listQuickAdd);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormApptEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Appointment";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormApptEdit_Closing);
			this.Load += new System.EventHandler(this.FormApptEdit_Load);
			this.groupFinancial.ResumeLayout(false);
			this.groupContact.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormApptEdit_Load(object sender, System.EventArgs e){
			InsPlans.Refresh();//this is here to prevent a bug, but I don't remember where the bug was.
			if(!PinIsVisible)
				butPin.Visible=false;
			if(Appointments.Cur.AptStatus==ApptStatus.Next){
				Text=Lan.g(this,"Edit Next Appointment")+" - "+Patients.GetCurNameLF();
				labelStatus.Visible=false;
				comboStatus.Visible=false;
				butDelete.Visible=false;
			}
			else{
				Text=Lan.g(this,"Edit Appointment")+" - "+Patients.GetCurNameLF();
				comboStatus.Items.Add(Lan.g("enumApptStatus","Scheduled"));
				comboStatus.Items.Add(Lan.g("enumApptStatus","Complete"));
				comboStatus.Items.Add(Lan.g("enumApptStatus","UnschedList"));
				comboStatus.Items.Add(Lan.g("enumApptStatus","ASAP"));
				comboStatus.Items.Add(Lan.g("enumApptStatus","Broken"));
				comboStatus.SelectedIndex=(int)Appointments.Cur.AptStatus-1;
			}
			if(IsNew){
				//for this, the appointment has to be created somewhere else first.
				//It might only be temporary, and will be deleted if Cancel is clicked.
				//Appointments.Cur=new Appointment();//handled  previously
				//Appointments.SaveApt();
				if(Appointments.Cur.Confirmed==0)
					Appointments.Cur.Confirmed=Defs.Short[(int)DefCat.ApptConfirmed][0].DefNum;
				if(Appointments.Cur.ProvNum==0)
					Appointments.Cur.ProvNum=Providers.List[0].ProvNum;
			}
			//for(int i=1;i<Enum.GetNames(typeof(ApptStatus)).Length;i++){//none status is not displayed
			//	listStatus.Items.Add(Enum.GetNames(typeof(ApptStatus))[i]);
			//}
			strBTime=new StringBuilder(Appointments.Cur.Pattern);
			comboUnschedStatus.Items.Add(Lan.g(this,"none"));
			comboUnschedStatus.SelectedIndex=0;
			for(int i=0;i<Defs.Short[(int)DefCat.RecallUnschedStatus].Length;i++){
				comboUnschedStatus.Items.Add(Defs.Short[(int)DefCat.RecallUnschedStatus][i].ItemName);
				if(Defs.Short[(int)DefCat.RecallUnschedStatus][i].DefNum==Appointments.Cur.UnschedStatus)
					comboUnschedStatus.SelectedIndex=i+1;
			}
			for(int i=0;i<Defs.Short[(int)DefCat.ApptConfirmed].Length;i++){
				listConfirmed.Items.Add(Defs.Short[(int)DefCat.ApptConfirmed][i].ItemName);
				if(Defs.Short[(int)DefCat.ApptConfirmed][i].DefNum==Appointments.Cur.Confirmed)
					listConfirmed.SelectedIndex=i;
			}
			textAddTime.MinVal=-1200;
			textAddTime.MaxVal=1200;
			textAddTime.Text=POut.PInt(Appointments.Cur.AddTime*10);
			textNote.Text=Appointments.Cur.Note;
			for(int i=0;i<Defs.Short[(int)DefCat.ApptProcsQuickAdd].Length;i++){
				listQuickAdd.Items.Add(Defs.Short[(int)DefCat.ApptProcsQuickAdd][i].ItemName);
			}
			for(int i=0;i<Providers.List.Length;i++){
				listProvNum.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==Appointments.Cur.ProvNum)
					listProvNum.SelectedIndex=i;
			}
			for(int i=0;i<Providers.List.Length;i++){
				listProvHyg.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==Appointments.Cur.ProvHyg)
					listProvHyg.SelectedIndex=i;
			}
			listAssistant.Items.Add(Lan.g(this,"none"));
			listAssistant.SelectedIndex=0;
			for(int i=0;i<Employees.ListShort.Length;i++){
				listAssistant.Items.Add(Employees.ListShort[i].FName);
				if(Employees.ListShort[i].EmployeeNum==Appointments.Cur.Assistant)
					listAssistant.SelectedIndex=i+1;
			}
			for(int i=0;i<Enum.GetNames(typeof(LabCase)).Length;i++){
				listLab.Items.Add(Enum.GetNames(typeof(LabCase))[i]);
				if(i==(int)Appointments.Cur.Lab)
					listLab.SelectedIndex=i;
			}
			//IsNewPatient is set well before opening this form.
			checkIsNewPatient.Checked=Appointments.Cur.IsNewPatient;
			textHmPhone.Text=Patients.Cur.HmPhone;
			textWkPhone.Text=Patients.Cur.WkPhone;
			textWirelessPhone.Text=Patients.Cur.WirelessPhone;
			textAddrNote.Text=Patients.Cur.AddrNote;
			textCreditType.Text=Patients.Cur.CreditType;
			textBillingType.Text=Defs.GetName(DefCat.BillingTypes,Patients.Cur.BillingType);
			textBalance.Text=Patients.Cur.EstBalance.ToString("F");
			textFamilyBal.Text=Patients.FamilyList[0].BalTotal.ToString("F");
			FillProcedures();
			FillTime();
			FillComm();
		}

		private void FillProcedures(){
			Procedures.Refresh();
			arrayProc = new Procedure[Procedures.List.Length];
			int countLine=0;
			//step through all procedures for patient and move selected ones to
			//arrayProc array as intermediate, then to ApptProc2 array for display
			if(Appointments.Cur.AptStatus==ApptStatus.Next){
				for(int i=0;i<Procedures.List.Length;i++){
					if(Procedures.List[i].NextAptNum==Appointments.Cur.AptNum){
						arrayProc[countLine]=Procedures.List[i];
						countLine++;
					}
					else if(Procedures.List[i].ProcStatus==ProcStat.TP){
						arrayProc[countLine]=Procedures.List[i];
						countLine++;
					}
				}
			}
			else{//standard appt
				for(int i=0;i<Procedures.List.Length;i++){
					if(Procedures.List[i].AptNum==Appointments.Cur.AptNum){
						arrayProc[countLine]=Procedures.List[i];
						countLine++;
					}
					else if(Procedures.List[i].AptNum!=0){
						//attached to another appt so don't show
					}
					else if(Procedures.List[i].ProcStatus==ProcStat.TP){
						arrayProc[countLine]=Procedures.List[i];
						countLine++;
					}
      		else if(Procedures.List[i].ProcStatus==ProcStat.C
						&& Procedures.List[i].ProcDate.Date==Appointments.Cur.AptDateTime.Date){
						arrayProc[countLine]=Procedures.List[i];
						countLine++;
					}
				}
			}
			//This is where to convert arrayProc to ApptProc2:
			double feeTotal=0;
			ApptProc2 = new ApptProc[countLine];
			for (int i=0;i<ApptProc2.Length;i++){
				ApptProc2[i].Index=i;
				switch (arrayProc[i].ProcStatus){
					case ProcStat.TP: ApptProc2[i].Status="TP"; break;
					case ProcStat.C: ApptProc2[i].Status="C"; break;
					//very rare, but possible:
					case ProcStat.EC: ApptProc2[i].Status="EC"; break;
					case ProcStat.EO: ApptProc2[i].Status="EO"; break;
					case ProcStat.R: ApptProc2[i].Status="R"; break;
				}
				ApptProc2[i].ToothNum=arrayProc[i].ToothNum;
				ApptProc2[i].Surf=arrayProc[i].Surf;
				ApptProc2[i].AbbrDesc=ProcedureCodes.GetProcCode(arrayProc[i].ADACode).Descript;;
				ApptProc2[i].Fee=arrayProc[i].ProcFee.ToString("F");
			}
			//Then, fill tbProc from ApptProc2:
			tbProc.ResetRows(ApptProc2.Length);
			//tbProc.SetBackGColor(Color.White);
			tbProc.SetGridColor(Color.Gray);
			for (int i=0;i<ApptProc2.Length;i++){
				tbProc.Cell[0,i]=ApptProc2[i].Status;
				tbProc.Cell[1,i]=Tooth.ToInternat(ApptProc2[i].ToothNum);
				tbProc.Cell[2,i]=ApptProc2[i].Surf;
				tbProc.Cell[3,i]=ApptProc2[i].AbbrDesc;
				tbProc.Cell[4,i]=ApptProc2[i].Fee;
				if(Appointments.Cur.AptStatus==ApptStatus.Next){
					if(arrayProc[ApptProc2[i].Index].NextAptNum==Appointments.Cur.AptNum){
						tbProc.ColorRow(i,Color.Silver);
						feeTotal+=arrayProc[ApptProc2[i].Index].ProcFee;
					}
				}
				else{
					if(arrayProc[ApptProc2[i].Index].AptNum==Appointments.Cur.AptNum){
						tbProc.ColorRow(i,Color.Silver);
						feeTotal+=arrayProc[ApptProc2[i].Index].ProcFee;
					}
				}
				
			}
			tbProc.LayoutTables();
			textFeeTotal.Text=feeTotal.ToString("F");
		}//end FillProcedures

		private void tbProc_CellClicked(object sender, CellEventArgs e){
			if(textAddTime.errorProvider1.GetError(textAddTime)!=""
				//|| textDateTerm.errorProvider1.GetError(textDateTerm)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			procsHaveChanged=true;
			Procedures.Cur=arrayProc[ApptProc2[e.Row].Index];
			Procedures.CurOld=Procedures.Cur;
			Procedure ProcCur=Procedures.Cur;
			if(Appointments.Cur.AptStatus==ApptStatus.Next){
				if(Procedures.Cur.NextAptNum==Appointments.Cur.AptNum){
					ProcCur.NextAptNum=0;
				}
				else{
					ProcCur.NextAptNum=Appointments.Cur.AptNum;
				}
			}
			else{//not Next
				if(Procedures.Cur.AptNum==Appointments.Cur.AptNum){
					ProcCur.AptNum=0;
				}
				else{
					ProcCur.AptNum=Appointments.Cur.AptNum;
				}
			}
			Procedures.Cur=ProcCur;
			Procedures.UpdateCur();
			FillProcedures();
			CalculateTime();
			FillTime();
		}

		private void CalculateTime(){
			int adjTimeU=PIn.PInt(textAddTime.Text)/10;
			strBTime=new StringBuilder("");
			string strT="";
			for(int i=0;i<ApptProc2.Length;i++){
				if(Appointments.Cur.AptStatus==ApptStatus.Next){
					if(arrayProc[ApptProc2[i].Index].NextAptNum!=Appointments.Cur.AptNum){
						continue;
					}
				}
				else{
					if(arrayProc[ApptProc2[i].Index].AptNum!=Appointments.Cur.AptNum){
						continue;
					}
				}
				strT=ProcedureCodes.GetProcCode(arrayProc[ApptProc2[i].Index].ADACode).ProcTime;
				if(strT.Length<2) continue;
				for(int n=1;n<strT.Length-1;n++){
					if(strT.Substring(n,1)=="/"){
						strBTime.Append("/");
					}
					else{
						strBTime.Insert(0,"X");
					}
				}
			}//end for
			//MessageBox.Show(strBTime.ToString());
			if(adjTimeU!=0){
				if(strBTime.Length==0){//might be useless.
					if(adjTimeU > 0){
						strBTime.Insert(0,"X",adjTimeU);
					}
				}
				else{//not length 0
					double xRatio;
					if((double)strBTime.ToString().LastIndexOf("X")==0)
						xRatio=1;
					else
						xRatio=(double)strBTime.ToString().LastIndexOf("X")/(double)(strBTime.Length-1);
					if(adjTimeU<0){//subtract time
						int xPort=(int)(-adjTimeU*xRatio);
						if(xPort > 0)
							if(xPort>=strBTime.Length)
								strBTime=new StringBuilder("");
							else
								strBTime.Remove(0,xPort);
						int iRemove=strBTime.Length-(-adjTimeU-xPort);
						if(iRemove < 0)
							strBTime=new StringBuilder("");
						else if(adjTimeU+xPort > strBTime.Length){
							strBTime=new StringBuilder("");
						}
						else
							strBTime.Remove(iRemove,-adjTimeU-xPort);
					}
					else{//add time
						//MessageBox.Show("adjTimeU:"+adjTimeU.ToString()+"xratio:"+xRatio.ToString());
						int xPort=(int)Math.Ceiling(adjTimeU*xRatio);
						//MessageBox.Show("xPort:"+xPort.ToString());
						if(xPort > 0)
							strBTime.Insert(0,"X",xPort);
						if(adjTimeU-xPort > 0)
							strBTime.Insert(strBTime.Length-1,"/",adjTimeU-xPort);
					}
				}//end else not length 0
			}
			strBTime.Insert(0,"/");
			strBTime.Append("/");
		}

		private void FillTime(){
			for (int i=0;i<strBTime.Length;i++){
				tbTime.Cell[0,i]=strBTime.ToString(i,1);
				tbTime.BackGColor[0,i]=Color.White;
			}
			for (int i=strBTime.Length;i<tbTime.MaxRows;i++){
				tbTime.Cell[0,i]="";
				tbTime.BackGColor[0,i]=Color.FromName("Control");
			}
			tbTime.Refresh();
			butSlider.Location=new Point(tbTime.Location.X+2
				,(tbTime.Location.Y+strBTime.Length*14+1));
			textTime.Text=strBTime.Length.ToString()+"0";
		}

		private void tbTime_CellClicked(object sender, CellEventArgs e){
			if(e.Row<strBTime.Length){
				if(strBTime[e.Row]=='/'){
					strBTime.Replace('/','X',e.Row,1);
				}
				else{
					strBTime.Replace(strBTime[e.Row],'/',e.Row,1);
				}
			}
			FillTime();
		}

		private void butSlider_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			mouseIsDown=true;
			mouseOrigin=new Point(e.X+butSlider.Location.X
				,e.Y+butSlider.Location.Y);
			sliderOrigin=butSlider.Location;
			
		}

		private void butSlider_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(!mouseIsDown)return;
			//tempPoint represents the new location of button of smooth dragging.
			Point tempPoint=new Point(sliderOrigin.X
				,sliderOrigin.Y+(e.Y+butSlider.Location.Y)-mouseOrigin.Y);
			int step=(int)(Math.Round((Decimal)(tempPoint.Y-tbTime.Location.Y)/14));
			if(step==strBTime.Length)return;
			if(step<1)return;
			if(step>tbTime.MaxRows-1) return;
			if(step>strBTime.Length){
				strBTime.Append('/');
			}
			if(step<strBTime.Length){
				strBTime.Remove(step,1);
			}
			FillTime();
		}

		private void butSlider_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			mouseIsDown=false;
		}

		private void butCalcTime_Click(object sender, System.EventArgs e) {
			if(textAddTime.errorProvider1.GetError(textAddTime)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			CalculateTime();
			FillTime();
		}

		/// <summary>Fills the commlog table on this form.</summary>
		private void FillComm(){
			Commlogs.Refresh();
			ALCommItems=new ArrayList();
			for(int i=0;i<Commlogs.List.Length;i++){
				if(Commlogs.List[i].CommType==CommItemType.ApptRelated){
					ALCommItems.Add(Commlogs.List[i]);
				}
			}
			tbCommlog.ResetRows(ALCommItems.Count);
			for(int i=0;i<ALCommItems.Count;i++){
				tbCommlog.Cell[0,i]=((Commlog)ALCommItems[i]).CommDate.ToShortDateString();
				tbCommlog.Cell[1,i]=((Commlog)ALCommItems[i]).Note;
			}
			tbCommlog.SetGridColor(Color.Gray);
			tbCommlog.LayoutTables();
		}

		private void tbCommlog_CellDoubleClicked(object sender, CellEventArgs e){
			Commlogs.Cur=(Commlog)ALCommItems[e.Row];
			FormCommItem FormCI=new FormCommItem();
			FormCI.ShowDialog();
			FillComm();
		}		

		private void butHygClear_Click(object sender, System.EventArgs e) {
			this.listProvHyg.SelectedIndex=-1;
		}

		private void listQuickAdd_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listQuickAdd.IndexFromPoint(e.X,e.Y)==-1){
				return;
			}
			if(textAddTime.errorProvider1.GetError(textAddTime)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			Procedures.SetDateFirstVisit(Appointments.Cur.AptDateTime.Date,1);
			string[] procs=Defs.Short[(int)DefCat.ApptProcsQuickAdd]
				[listQuickAdd.IndexFromPoint(e.X,e.Y)].ItemValue.Split(',');
			for(int i=0;i<procs.Length;i++){
				Procedure ProcCur=new Procedure();
				//maybe test codes in defs before allowing them in the first place(no tooth num)
				//if(ProcCodes.GetProcCode(Procedures.Cur.ADACode). 
				ProcCur.PatNum=Appointments.Cur.PatNum;
				if(Appointments.Cur.AptStatus!=ApptStatus.Next)
					ProcCur.AptNum=Appointments.Cur.AptNum;
				ProcCur.ADACode=procs[i];
				ProcCur.ProcDate=Appointments.Cur.AptDateTime.Date;
				ProcCur.ProcFee=Fees.GetAmount(ProcCur.ADACode,ContrChart.GetFeeSched());
				ProcCur.OverridePri=-1;
				ProcCur.OverrideSec=-1;
				//surf
				//toothnum
				//toothrange
				ProcCur.NoBillIns=ProcedureCodes.GetProcCode(ProcCur.ADACode).NoBillIns;
				//priority
				ProcCur.ProcStatus=ProcStat.TP;
				//procnote
				ProcCur.ProvNum=Appointments.Cur.ProvNum;
				//Dx
				if(Appointments.Cur.AptStatus==ApptStatus.Next)
          ProcCur.NextAptNum=Appointments.Cur.AptNum;
				ProcCur.CapCoPay=-1;
				if(Patients.Cur.PriPlanNum!=0){//if patient has insurance
					ProcCur.IsCovIns=true;
					InsPlans.GetCur(Patients.Cur.PriPlanNum);
					if(InsPlans.Cur.PlanType=="c"){
						//also handles fine if copayfeesched=0:
						ProcCur.CapCoPay=Fees.GetAmount(ProcCur.ADACode,InsPlans.Cur.CopayFeeSched);
					}
				}
				Procedures.Cur=ProcCur;
				Procedures.InsertCur();
			}
			listQuickAdd.SelectedIndex=-1;
			FillProcedures();
			CalculateTime();
			FillTime();
		}

		private void listQuickAdd_SelectedIndexChanged(object sender, System.EventArgs e) {
			//listQuickAdd.SelectedIndex=-1;
		}

		private void butAddComm_Click(object sender, System.EventArgs e) {
			Commlogs.Cur=new Commlog();
			Commlogs.Cur.PatNum=Patients.Cur.PatNum;
			Commlogs.Cur.CommDate=DateTime.Today;
			Commlogs.Cur.CommType=CommItemType.ApptRelated;
			FormCommItem FormCI=new FormCommItem();
			FormCI.IsNew=true;
			FormCI.ShowDialog();
			FillComm();
		}	

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"Delete appointment?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			if(Appointments.Cur.AptStatus==ApptStatus.Next){
				Procedures.UnattachProcsInNextAppt(Appointments.Cur.AptNum);
			}
			else{
				Procedures.UnattachProcsInAppt(Appointments.Cur.AptNum);
			}
			Appointments.DeleteCur();
			DialogResult=DialogResult.OK;
		}

		private bool UpdateToDB(){
			if(textAddTime.errorProvider1.GetError(textAddTime)!=""
				//|| textDateTerm.errorProvider1.GetError(textDateTerm)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return false;
			}
			if(Appointments.Cur.AptStatus==ApptStatus.Next){
				;
			}
			else if(comboStatus.SelectedIndex==-1){
				Appointments.Cur.AptStatus=ApptStatus.Scheduled;
			}
			else{
				Appointments.Cur.AptStatus=(ApptStatus)comboStatus.SelectedIndex+1;
			}
			Appointments.Cur.Pattern=strBTime.ToString();
			if(comboUnschedStatus.SelectedIndex==0)//none
				Appointments.Cur.UnschedStatus=0;
			else
				Appointments.Cur.UnschedStatus
					=Defs.Short[(int)DefCat.RecallUnschedStatus][comboUnschedStatus.SelectedIndex-1].DefNum;
			if(listConfirmed.SelectedIndex!=-1)
				Appointments.Cur.Confirmed=Defs.Short[(int)DefCat.ApptConfirmed][listConfirmed.SelectedIndex].DefNum;
			Appointments.Cur.AddTime=(int)(PIn.PInt(textAddTime.Text)/10);
			Appointments.Cur.Note=textNote.Text;
			//there should always be a non-hidden primary provider for an appt.
			if(listProvNum.SelectedIndex==-1)
				Appointments.Cur.ProvNum=Providers.List[0].ProvNum;
			else
				Appointments.Cur.ProvNum=Providers.List[listProvNum.SelectedIndex].ProvNum;
			if(listProvHyg.SelectedIndex==-1)
				Appointments.Cur.ProvHyg=0;
			else
				Appointments.Cur.ProvHyg=Providers.List[listProvHyg.SelectedIndex].ProvNum;
			if(listAssistant.SelectedIndex==0)//none
				Appointments.Cur.Assistant=0;
			else
				Appointments.Cur.Assistant=Employees.ListShort[listAssistant.SelectedIndex-1].EmployeeNum;
			Appointments.Cur.Lab=(LabCase)listLab.SelectedIndex;
			Appointments.Cur.IsNewPatient=checkIsNewPatient.Checked;
			Appointments.Cur.ProcDescript="";
			if(Appointments.Cur.AptStatus==ApptStatus.Next){
				for(int i=0;i<Procedures.List.Length;i++){
					if(Procedures.List[i].NextAptNum==Appointments.Cur.AptNum){
						Appointments.Cur.ProcDescript
							+=ProcedureCodes.GetProcCode(Procedures.List[i].ADACode).AbbrDesc+", ";
					}
				}
			}
			else{//standard appt
				for(int i=0;i<Procedures.List.Length;i++){
					if(Procedures.List[i].AptNum==Appointments.Cur.AptNum){
						Appointments.Cur.ProcDescript
							+=ProcedureCodes.GetProcCode(Procedures.List[i].ADACode).AbbrDesc+", ";
					}
				}
			}
			if(Appointments.Cur.ProcDescript.Length>1){
				//trims the last space and comma
				Appointments.Cur.ProcDescript
					=Appointments.Cur.ProcDescript.Substring(0,Appointments.Cur.ProcDescript.Length-2);
			}
			Appointments.UpdateCur();
			return true;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(!UpdateToDB())
				return;
			DialogResult=DialogResult.OK;
		}

		private void butPin_Click(object sender, System.EventArgs e) {
			if(!UpdateToDB())
				return;
			PinClicked=true;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormApptEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNew){
				if(Appointments.Cur.AptStatus==ApptStatus.Next){
					Procedures.UnattachProcsInNextAppt(Appointments.Cur.AptNum);
				}
				else{
					Procedures.UnattachProcsInAppt(Appointments.Cur.AptNum);
				}
				Appointments.DeleteCur();
				DialogResult=DialogResult.Cancel;
				//return;
			}
			else if(procsHaveChanged){
				MessageBox.Show(Lan.g(this,"Warning. Changes you made to procedures have already been saved.  Other changes will not be saved."));
				DialogResult=DialogResult.OK;//so that appt module will update
				//return;
			}
		}

		

		

			

	}

	///<summary></summary>
	public struct ApptProc{
		///<summary></summary>
		public int Index;//represents index within arrayProc
		///<summary></summary>
		public string Status;
		///<summary></summary>
		public string ToothNum;
		///<summary></summary>
		public string Surf;
		///<summary></summary>
		public string AbbrDesc;
		///<summary></summary>
		public string Fee;
	}//end struct ApptProc
}
