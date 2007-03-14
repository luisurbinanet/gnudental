using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormMisc : System.Windows.Forms.Form{
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textTreatNote;
		private System.Windows.Forms.CheckBox checkShowCC;
		private System.Windows.Forms.TextBox textMainWindowTitle;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.CheckBox checkITooth;
		private System.Windows.Forms.CheckBox checkTreatPlanShowGraphics;
		private System.Windows.Forms.CheckBox checkTreatPlanShowCompleted;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkTreatPlanShowIns;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton radioUsePatNum;
		private System.Windows.Forms.RadioButton radioUseChartNumber;
		private System.Windows.Forms.Label label2;
		private OpenDental.ValidNumber textStatementsCalcDueDate;
		private System.Windows.Forms.CheckBox checkEclaimsSeparateTreatProv;
		private System.Windows.Forms.CheckBox checkRandomPrimaryKeys;
		private System.Windows.Forms.CheckBox checkBalancesDontSubtractIns;
		private System.Windows.Forms.Label label3;
		private OpenDental.ValidNumber textSigInterval;
		private System.Windows.Forms.CheckBox checkInsurancePlansShared;
		private System.Windows.Forms.Label label1;// Required designer variable.

		///<summary></summary>
		public FormMisc(){
			InitializeComponent();
			Lan.F(this);
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
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textTreatNote = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkShowCC = new System.Windows.Forms.CheckBox();
			this.textMainWindowTitle = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.checkITooth = new System.Windows.Forms.CheckBox();
			this.checkTreatPlanShowGraphics = new System.Windows.Forms.CheckBox();
			this.checkTreatPlanShowCompleted = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkTreatPlanShowIns = new System.Windows.Forms.CheckBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.checkBalancesDontSubtractIns = new System.Windows.Forms.CheckBox();
			this.textStatementsCalcDueDate = new OpenDental.ValidNumber();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.radioUsePatNum = new System.Windows.Forms.RadioButton();
			this.radioUseChartNumber = new System.Windows.Forms.RadioButton();
			this.checkEclaimsSeparateTreatProv = new System.Windows.Forms.CheckBox();
			this.checkRandomPrimaryKeys = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textSigInterval = new OpenDental.ValidNumber();
			this.checkInsurancePlansShared = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(677, 410);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 7;
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
			this.butCancel.Location = new System.Drawing.Point(677, 448);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 8;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textTreatNote
			// 
			this.textTreatNote.AcceptsReturn = true;
			this.textTreatNote.Location = new System.Drawing.Point(17, 40);
			this.textTreatNote.Multiline = true;
			this.textTreatNote.Name = "textTreatNote";
			this.textTreatNote.Size = new System.Drawing.Size(346, 114);
			this.textTreatNote.TabIndex = 3;
			this.textTreatNote.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(18, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(268, 15);
			this.label1.TabIndex = 35;
			this.label1.Text = "Default Note";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// checkShowCC
			// 
			this.checkShowCC.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowCC.Location = new System.Drawing.Point(19, 29);
			this.checkShowCC.Name = "checkShowCC";
			this.checkShowCC.Size = new System.Drawing.Size(216, 17);
			this.checkShowCC.TabIndex = 36;
			this.checkShowCC.Text = "Show Credit Card Info";
			// 
			// textMainWindowTitle
			// 
			this.textMainWindowTitle.Location = new System.Drawing.Point(144, 337);
			this.textMainWindowTitle.Name = "textMainWindowTitle";
			this.textMainWindowTitle.Size = new System.Drawing.Size(333, 20);
			this.textMainWindowTitle.TabIndex = 38;
			this.textMainWindowTitle.Text = "";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(-7, 339);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(149, 17);
			this.label14.TabIndex = 39;
			this.label14.Text = "Main Window Title";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkITooth
			// 
			this.checkITooth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkITooth.Location = new System.Drawing.Point(48, 379);
			this.checkITooth.Name = "checkITooth";
			this.checkITooth.Size = new System.Drawing.Size(338, 21);
			this.checkITooth.TabIndex = 42;
			this.checkITooth.Text = "Use International Tooth Numbers (11-48)";
			// 
			// checkTreatPlanShowGraphics
			// 
			this.checkTreatPlanShowGraphics.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTreatPlanShowGraphics.Location = new System.Drawing.Point(16, 159);
			this.checkTreatPlanShowGraphics.Name = "checkTreatPlanShowGraphics";
			this.checkTreatPlanShowGraphics.Size = new System.Drawing.Size(339, 17);
			this.checkTreatPlanShowGraphics.TabIndex = 46;
			this.checkTreatPlanShowGraphics.Text = "Show Graphical Tooth Chart";
			// 
			// checkTreatPlanShowCompleted
			// 
			this.checkTreatPlanShowCompleted.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTreatPlanShowCompleted.Location = new System.Drawing.Point(16, 179);
			this.checkTreatPlanShowCompleted.Name = "checkTreatPlanShowCompleted";
			this.checkTreatPlanShowCompleted.Size = new System.Drawing.Size(334, 17);
			this.checkTreatPlanShowCompleted.TabIndex = 47;
			this.checkTreatPlanShowCompleted.Text = "Show Completed Work on Graphical Tooth Chart";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkTreatPlanShowIns);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.textTreatNote);
			this.groupBox1.Controls.Add(this.checkTreatPlanShowGraphics);
			this.groupBox1.Controls.Add(this.checkTreatPlanShowCompleted);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(32, 26);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(381, 222);
			this.groupBox1.TabIndex = 48;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Treatment Plans";
			// 
			// checkTreatPlanShowIns
			// 
			this.checkTreatPlanShowIns.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTreatPlanShowIns.Location = new System.Drawing.Point(16, 199);
			this.checkTreatPlanShowIns.Name = "checkTreatPlanShowIns";
			this.checkTreatPlanShowIns.Size = new System.Drawing.Size(334, 17);
			this.checkTreatPlanShowIns.TabIndex = 48;
			this.checkTreatPlanShowIns.Text = "Show Insurance Estimates";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.checkBalancesDontSubtractIns);
			this.groupBox5.Controls.Add(this.textStatementsCalcDueDate);
			this.groupBox5.Controls.Add(this.label2);
			this.groupBox5.Controls.Add(this.groupBox2);
			this.groupBox5.Controls.Add(this.checkShowCC);
			this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox5.Location = new System.Drawing.Point(433, 26);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(318, 222);
			this.groupBox5.TabIndex = 52;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Statements";
			// 
			// checkBalancesDontSubtractIns
			// 
			this.checkBalancesDontSubtractIns.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBalancesDontSubtractIns.Location = new System.Drawing.Point(19, 189);
			this.checkBalancesDontSubtractIns.Name = "checkBalancesDontSubtractIns";
			this.checkBalancesDontSubtractIns.Size = new System.Drawing.Size(288, 17);
			this.checkBalancesDontSubtractIns.TabIndex = 55;
			this.checkBalancesDontSubtractIns.Text = "Balances Don\'t Subtract Insurance Estimate";
			// 
			// textStatementsCalcDueDate
			// 
			this.textStatementsCalcDueDate.Location = new System.Drawing.Point(235, 132);
			this.textStatementsCalcDueDate.MaxVal = 255;
			this.textStatementsCalcDueDate.MinVal = 0;
			this.textStatementsCalcDueDate.Name = "textStatementsCalcDueDate";
			this.textStatementsCalcDueDate.Size = new System.Drawing.Size(60, 20);
			this.textStatementsCalcDueDate.TabIndex = 54;
			this.textStatementsCalcDueDate.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(3, 131);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(229, 50);
			this.label2.TabIndex = 53;
			this.label2.Text = "Days to calculate due date.  Usually 10 or 15.  Leave blank to show \"Due on Recei" +
				"pt\"";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.radioUsePatNum);
			this.groupBox2.Controls.Add(this.radioUseChartNumber);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(12, 57);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(200, 61);
			this.groupBox2.TabIndex = 52;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Account Numbers Use";
			// 
			// radioUsePatNum
			// 
			this.radioUsePatNum.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioUsePatNum.Location = new System.Drawing.Point(9, 34);
			this.radioUsePatNum.Name = "radioUsePatNum";
			this.radioUsePatNum.Size = new System.Drawing.Size(144, 19);
			this.radioUsePatNum.TabIndex = 1;
			this.radioUsePatNum.Text = "Patient Number";
			// 
			// radioUseChartNumber
			// 
			this.radioUseChartNumber.Checked = true;
			this.radioUseChartNumber.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioUseChartNumber.Location = new System.Drawing.Point(9, 16);
			this.radioUseChartNumber.Name = "radioUseChartNumber";
			this.radioUseChartNumber.Size = new System.Drawing.Size(144, 19);
			this.radioUseChartNumber.TabIndex = 0;
			this.radioUseChartNumber.TabStop = true;
			this.radioUseChartNumber.Text = "Chart Number";
			// 
			// checkEclaimsSeparateTreatProv
			// 
			this.checkEclaimsSeparateTreatProv.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkEclaimsSeparateTreatProv.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkEclaimsSeparateTreatProv.Location = new System.Drawing.Point(48, 361);
			this.checkEclaimsSeparateTreatProv.Name = "checkEclaimsSeparateTreatProv";
			this.checkEclaimsSeparateTreatProv.Size = new System.Drawing.Size(537, 19);
			this.checkEclaimsSeparateTreatProv.TabIndex = 53;
			this.checkEclaimsSeparateTreatProv.Text = "On e-claims, send treating provider info for each separate procedure";
			this.checkEclaimsSeparateTreatProv.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// checkRandomPrimaryKeys
			// 
			this.checkRandomPrimaryKeys.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkRandomPrimaryKeys.Location = new System.Drawing.Point(48, 311);
			this.checkRandomPrimaryKeys.Name = "checkRandomPrimaryKeys";
			this.checkRandomPrimaryKeys.Size = new System.Drawing.Size(498, 21);
			this.checkRandomPrimaryKeys.TabIndex = 55;
			this.checkRandomPrimaryKeys.Text = "Use Random Primary Keys (BE VERY CAREFUL.  THIS IS IRREVERSIBLE)";
			this.checkRandomPrimaryKeys.Click += new System.EventHandler(this.checkRandomPrimaryKeys_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(46, 265);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(696, 17);
			this.label3.TabIndex = 56;
			this.label3.Text = "Process Signal Interval in seconds.  Usually every 6 to 20 seconds.  Leave blank " +
				"to disable autorefresh.";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textSigInterval
			// 
			this.textSigInterval.Location = new System.Drawing.Point(47, 286);
			this.textSigInterval.MaxVal = 1000000;
			this.textSigInterval.MinVal = 1;
			this.textSigInterval.Name = "textSigInterval";
			this.textSigInterval.Size = new System.Drawing.Size(74, 20);
			this.textSigInterval.TabIndex = 57;
			this.textSigInterval.Text = "";
			// 
			// checkInsurancePlansShared
			// 
			this.checkInsurancePlansShared.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkInsurancePlansShared.Location = new System.Drawing.Point(48, 399);
			this.checkInsurancePlansShared.Name = "checkInsurancePlansShared";
			this.checkInsurancePlansShared.Size = new System.Drawing.Size(495, 32);
			this.checkInsurancePlansShared.TabIndex = 58;
			this.checkInsurancePlansShared.Text = "Many patients have identical insurance plans.  Change behavior of program slightl" +
				"y to optimize for identical plans.";
			// 
			// FormMisc
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(773, 486);
			this.Controls.Add(this.checkInsurancePlansShared);
			this.Controls.Add(this.textSigInterval);
			this.Controls.Add(this.textMainWindowTitle);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.checkRandomPrimaryKeys);
			this.Controls.Add(this.checkEclaimsSeparateTreatProv);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.checkITooth);
			this.Controls.Add(this.label14);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMisc";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Miscellaneous Setup";
			this.Load += new System.EventHandler(this.FormMisc_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormMisc_Load(object sender, System.EventArgs e) {
			textTreatNote.Text=Prefs.GetString("TreatmentPlanNote");
			checkTreatPlanShowGraphics.Checked=Prefs.GetBool("TreatPlanShowGraphics");
			checkTreatPlanShowCompleted.Checked=Prefs.GetBool("TreatPlanShowCompleted");
			checkTreatPlanShowIns.Checked=Prefs.GetBool("TreatPlanShowIns");
			checkShowCC.Checked=Prefs.GetBool("StatementShowCreditCard");
			if(Prefs.GetBool("StatementAccountsUseChartNumber")){
				radioUseChartNumber.Checked=true;
			}
			else{
				radioUsePatNum.Checked=true;
			}
			if(Prefs.GetInt("StatementsCalcDueDate")!=-1){
				textStatementsCalcDueDate.Text=Prefs.GetInt("StatementsCalcDueDate").ToString();
			}
			checkBalancesDontSubtractIns.Checked=Prefs.GetBool("BalancesDontSubtractIns");
			if(Prefs.GetInt("ProcessSigsIntervalInSecs")==0){
				textSigInterval.Text="";
			}
			else{
				textSigInterval.Text=Prefs.GetInt("ProcessSigsIntervalInSecs").ToString();
			}
			checkRandomPrimaryKeys.Checked=Prefs.GetBool("RandomPrimaryKeys");
			if(checkRandomPrimaryKeys.Checked){
				//not allowed to uncheck it
				checkRandomPrimaryKeys.Enabled=false;
			}
			textMainWindowTitle.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString;
			checkEclaimsSeparateTreatProv.Checked=Prefs.GetBool("EclaimsSeparateTreatProv");
			checkITooth.Checked=Prefs.GetBool("UseInternationalToothNumbers");
			checkInsurancePlansShared.Checked=Prefs.GetBool("InsurancePlansShared");
			
		}

		private void checkRandomPrimaryKeys_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show("Are you absolutely sure you want to enable random primary keys?\r\n"
				+"Advantages:\r\n"
				+"Multiple servers can stay synchronized using merge replication.\r\n"
				+"Realtime connection between servers not required.\r\n"
				+"Data can be entered on all servers and synchronized later.\r\n"
				+"Disadvantages:\r\n"
				+"Slightly slower.\r\n"
				+"Primary keys much longer, so not as user friendly.","",MessageBoxButtons.OKCancel)==DialogResult.Cancel)
			{
				checkRandomPrimaryKeys.Checked=false;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textStatementsCalcDueDate.errorProvider1.GetError(textStatementsCalcDueDate)!="")
			{
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			bool changed=false;
			if( Prefs.UpdateString("TreatmentPlanNote",textTreatNote.Text)
				|| Prefs.UpdateBool("TreatPlanShowGraphics",checkTreatPlanShowGraphics.Checked)
				|| Prefs.UpdateBool("TreatPlanShowCompleted",checkTreatPlanShowCompleted.Checked)
				|| Prefs.UpdateBool("TreatPlanShowIns",checkTreatPlanShowIns.Checked)
				|| Prefs.UpdateBool("StatementShowCreditCard",checkShowCC.Checked)
				|| Prefs.UpdateBool("StatementAccountsUseChartNumber",radioUseChartNumber.Checked)
				|| Prefs.UpdateBool("BalancesDontSubtractIns",checkBalancesDontSubtractIns.Checked)
				|| Prefs.UpdateBool("RandomPrimaryKeys",checkRandomPrimaryKeys.Checked)
				|| Prefs.UpdateString("MainWindowTitle",textMainWindowTitle.Text)
				|| Prefs.UpdateBool("EclaimsSeparateTreatProv",checkEclaimsSeparateTreatProv.Checked)
				|| Prefs.UpdateBool("UseInternationalToothNumbers",checkITooth.Checked)
				|| Prefs.UpdateBool("InsurancePlansShared",checkInsurancePlansShared.Checked))
			{
				changed=true;
			}
			if(textStatementsCalcDueDate.Text==""){
				if(Prefs.UpdateInt("StatementsCalcDueDate",-1)){
					changed=true;
				}
			}
			else{
				if(Prefs.UpdateInt("StatementsCalcDueDate",PIn.PInt(textStatementsCalcDueDate.Text))){
					changed=true;
				}
			}
			if(textSigInterval.Text==""){
				if(Prefs.UpdateInt("ProcessSigsIntervalInSecs",0)){
					changed=true;
				}
			}
			else{
				if(Prefs.UpdateInt("ProcessSigsIntervalInSecs",PIn.PInt(textSigInterval.Text))){
					changed=true;
				}
			}
			if(changed){
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

	

	}
}




