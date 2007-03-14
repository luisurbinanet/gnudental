using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using OpenDental.UI;

namespace OpenDental{

	///<summary></summary>
	public class ContrStaff : System.Windows.Forms.UserControl{
		private OpenDental.UI.Button butTimeCard;
		private System.Windows.Forms.ListBox listStatus;
		private System.Windows.Forms.Label textTime;
		private System.Windows.Forms.Timer timer1;
		private OpenDental.UI.Button butClockIn;
		private OpenDental.UI.Button butClockOut;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private OpenDental.UI.Button butClear;
		private OpenDental.UI.Button butSend;
		private System.Windows.Forms.TextBox textMessage;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.ComponentModel.IContainer components;
		private OpenDental.UI.Button butSendClaims;
		private OpenDental.UI.Button butTasks;
		private OpenDental.UI.Button butBackup;
		private OpenDental.UI.ODGrid gridEmp;
		private System.Windows.Forms.GroupBox groupBox3;
		private OpenDental.UI.Button butDeposit;
		private OpenDental.UI.Button butBreaks;
		private OpenDental.UI.Button butBilling;
		private OpenDental.UI.Button butAccounting;
		///<summary>Server time minus local computer time, usually +/- 1 or 2 minutes</summary>
		private TimeSpan TimeDelta;
		///<summary></summary>
		[Category("Data"),Description("Occurs when user changes current patient, usually by clicking on the Select Patient button.")]
		public event PatientSelectedEventHandler PatientSelected=null;

		///<summary></summary>
		public ContrStaff(){
			InitializeComponent();
			this.listStatus.Click += new System.EventHandler(this.listStatus_Click);
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

		#region Component Designer generated code

		private void InitializeComponent(){
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContrStaff));
			this.listStatus = new System.Windows.Forms.ListBox();
			this.textTime = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textMessage = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.butBilling = new OpenDental.UI.Button();
			this.butAccounting = new OpenDental.UI.Button();
			this.butDeposit = new OpenDental.UI.Button();
			this.butSendClaims = new OpenDental.UI.Button();
			this.butBackup = new OpenDental.UI.Button();
			this.butTasks = new OpenDental.UI.Button();
			this.butClear = new OpenDental.UI.Button();
			this.butSend = new OpenDental.UI.Button();
			this.butBreaks = new OpenDental.UI.Button();
			this.gridEmp = new OpenDental.UI.ODGrid();
			this.butClockOut = new OpenDental.UI.Button();
			this.butTimeCard = new OpenDental.UI.Button();
			this.butClockIn = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// listStatus
			// 
			this.listStatus.Location = new System.Drawing.Point(372,182);
			this.listStatus.Name = "listStatus";
			this.listStatus.Size = new System.Drawing.Size(107,43);
			this.listStatus.TabIndex = 12;
			// 
			// textTime
			// 
			this.textTime.Font = new System.Drawing.Font("Microsoft Sans Serif",13F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textTime.Location = new System.Drawing.Point(370,103);
			this.textTime.Name = "textTime";
			this.textTime.Size = new System.Drawing.Size(109,21);
			this.textTime.TabIndex = 17;
			this.textTime.Text = "12:00:00 PM";
			this.textTime.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 1000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butBreaks);
			this.groupBox1.Controls.Add(this.gridEmp);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.listStatus);
			this.groupBox1.Controls.Add(this.butClockOut);
			this.groupBox1.Controls.Add(this.butTimeCard);
			this.groupBox1.Controls.Add(this.textTime);
			this.groupBox1.Controls.Add(this.butClockIn);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(103,352);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(530,288);
			this.groupBox1.TabIndex = 18;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Time Clock";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(381,84);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88,19);
			this.label2.TabIndex = 20;
			this.label2.Text = "Server Time";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.butClear);
			this.groupBox2.Controls.Add(this.butSend);
			this.groupBox2.Controls.Add(this.textMessage);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(103,164);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(530,164);
			this.groupBox2.TabIndex = 19;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Messaging";
			// 
			// textMessage
			// 
			this.textMessage.Location = new System.Drawing.Point(28,51);
			this.textMessage.Multiline = true;
			this.textMessage.Name = "textMessage";
			this.textMessage.Size = new System.Drawing.Size(419,61);
			this.textMessage.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(28,21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(464,23);
			this.label1.TabIndex = 0;
			this.label1.Text = "General Message  (don\'t overuse this since it can be annoying)";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.butBilling);
			this.groupBox3.Controls.Add(this.butAccounting);
			this.groupBox3.Controls.Add(this.butDeposit);
			this.groupBox3.Controls.Add(this.butSendClaims);
			this.groupBox3.Controls.Add(this.butBackup);
			this.groupBox3.Controls.Add(this.butTasks);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(103,19);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(272,112);
			this.groupBox3.TabIndex = 23;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Daily";
			// 
			// butBilling
			// 
			this.butBilling.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBilling.Autosize = true;
			this.butBilling.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBilling.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBilling.Location = new System.Drawing.Point(16,45);
			this.butBilling.Name = "butBilling";
			this.butBilling.Size = new System.Drawing.Size(104,26);
			this.butBilling.TabIndex = 25;
			this.butBilling.Text = "Billing";
			this.butBilling.Click += new System.EventHandler(this.butBilling_Click);
			// 
			// butAccounting
			// 
			this.butAccounting.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAccounting.Autosize = true;
			this.butAccounting.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAccounting.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAccounting.Image = ((System.Drawing.Image)(resources.GetObject("butAccounting.Image")));
			this.butAccounting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAccounting.Location = new System.Drawing.Point(148,71);
			this.butAccounting.Name = "butAccounting";
			this.butAccounting.Size = new System.Drawing.Size(104,26);
			this.butAccounting.TabIndex = 24;
			this.butAccounting.Text = "Accounting";
			this.butAccounting.Click += new System.EventHandler(this.butAccounting_Click);
			// 
			// butDeposit
			// 
			this.butDeposit.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDeposit.Autosize = true;
			this.butDeposit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDeposit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDeposit.Image = ((System.Drawing.Image)(resources.GetObject("butDeposit.Image")));
			this.butDeposit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDeposit.Location = new System.Drawing.Point(16,71);
			this.butDeposit.Name = "butDeposit";
			this.butDeposit.Size = new System.Drawing.Size(104,26);
			this.butDeposit.TabIndex = 23;
			this.butDeposit.Text = "Deposits";
			this.butDeposit.Click += new System.EventHandler(this.butDeposit_Click);
			// 
			// butSendClaims
			// 
			this.butSendClaims.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSendClaims.Autosize = true;
			this.butSendClaims.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSendClaims.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSendClaims.Image = ((System.Drawing.Image)(resources.GetObject("butSendClaims.Image")));
			this.butSendClaims.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butSendClaims.Location = new System.Drawing.Point(16,19);
			this.butSendClaims.Name = "butSendClaims";
			this.butSendClaims.Size = new System.Drawing.Size(104,26);
			this.butSendClaims.TabIndex = 20;
			this.butSendClaims.Text = "Send Claims";
			this.butSendClaims.Click += new System.EventHandler(this.butSendClaims_Click);
			// 
			// butBackup
			// 
			this.butBackup.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBackup.Autosize = true;
			this.butBackup.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBackup.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBackup.Image = ((System.Drawing.Image)(resources.GetObject("butBackup.Image")));
			this.butBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butBackup.Location = new System.Drawing.Point(148,45);
			this.butBackup.Name = "butBackup";
			this.butBackup.Size = new System.Drawing.Size(104,26);
			this.butBackup.TabIndex = 22;
			this.butBackup.Text = "Backup";
			this.butBackup.Click += new System.EventHandler(this.butBackup_Click);
			// 
			// butTasks
			// 
			this.butTasks.AdjustImageLocation = new System.Drawing.Point(0,1);
			this.butTasks.Autosize = true;
			this.butTasks.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTasks.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTasks.Image = ((System.Drawing.Image)(resources.GetObject("butTasks.Image")));
			this.butTasks.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butTasks.Location = new System.Drawing.Point(148,19);
			this.butTasks.Name = "butTasks";
			this.butTasks.Size = new System.Drawing.Size(104,26);
			this.butTasks.TabIndex = 21;
			this.butTasks.Text = "Tasks";
			this.butTasks.Click += new System.EventHandler(this.butTasks_Click);
			// 
			// butClear
			// 
			this.butClear.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClear.Autosize = true;
			this.butClear.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClear.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClear.Location = new System.Drawing.Point(120,124);
			this.butClear.Name = "butClear";
			this.butClear.Size = new System.Drawing.Size(75,25);
			this.butClear.TabIndex = 3;
			this.butClear.Text = "Clear";
			this.butClear.Click += new System.EventHandler(this.butClear_Click);
			// 
			// butSend
			// 
			this.butSend.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSend.Autosize = true;
			this.butSend.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSend.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSend.Location = new System.Drawing.Point(27,124);
			this.butSend.Name = "butSend";
			this.butSend.Size = new System.Drawing.Size(75,25);
			this.butSend.TabIndex = 2;
			this.butSend.Text = "Send";
			this.butSend.Click += new System.EventHandler(this.butSend_Click);
			// 
			// butBreaks
			// 
			this.butBreaks.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBreaks.Autosize = true;
			this.butBreaks.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBreaks.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBreaks.Location = new System.Drawing.Point(371,55);
			this.butBreaks.Name = "butBreaks";
			this.butBreaks.Size = new System.Drawing.Size(108,25);
			this.butBreaks.TabIndex = 22;
			this.butBreaks.Text = "View Breaks";
			this.butBreaks.Click += new System.EventHandler(this.butBreaks_Click);
			// 
			// gridEmp
			// 
			this.gridEmp.AllowSelection = false;
			this.gridEmp.HScrollVisible = false;
			this.gridEmp.Location = new System.Drawing.Point(22,26);
			this.gridEmp.Name = "gridEmp";
			this.gridEmp.ScrollValue = 0;
			this.gridEmp.Size = new System.Drawing.Size(303,238);
			this.gridEmp.TabIndex = 21;
			this.gridEmp.Title = "Employee";
			this.gridEmp.TranslationName = "TableEmpClock";
			this.gridEmp.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridEmp_CellClick);
			// 
			// butClockOut
			// 
			this.butClockOut.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClockOut.Autosize = true;
			this.butClockOut.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClockOut.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClockOut.Location = new System.Drawing.Point(371,154);
			this.butClockOut.Name = "butClockOut";
			this.butClockOut.Size = new System.Drawing.Size(108,25);
			this.butClockOut.TabIndex = 14;
			this.butClockOut.Text = "Clock Out For:";
			this.butClockOut.Click += new System.EventHandler(this.butClockOut_Click);
			// 
			// butTimeCard
			// 
			this.butTimeCard.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butTimeCard.Autosize = true;
			this.butTimeCard.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTimeCard.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTimeCard.Location = new System.Drawing.Point(371,28);
			this.butTimeCard.Name = "butTimeCard";
			this.butTimeCard.Size = new System.Drawing.Size(108,25);
			this.butTimeCard.TabIndex = 16;
			this.butTimeCard.Text = "View Timecard";
			this.butTimeCard.Click += new System.EventHandler(this.butTimeCard_Click);
			// 
			// butClockIn
			// 
			this.butClockIn.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClockIn.Autosize = true;
			this.butClockIn.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClockIn.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClockIn.Location = new System.Drawing.Point(371,127);
			this.butClockIn.Name = "butClockIn";
			this.butClockIn.Size = new System.Drawing.Size(108,25);
			this.butClockIn.TabIndex = 11;
			this.butClockIn.Text = "Clock In";
			this.butClockIn.Click += new System.EventHandler(this.butClockIn_Click);
			// 
			// ContrStaff
			// 
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "ContrStaff";
			this.Size = new System.Drawing.Size(908,702);
			this.Load += new System.EventHandler(this.ContrStaff_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void ContrStaff_Load(object sender, System.EventArgs e) {
		
		}

		///<summary></summary>
		public void InstantClasses(){
			//can't use Lan.F
			Lan.C(this,new Control[]
				{
				groupBox2,
				label1,
				butSend,
				butClear,
				groupBox1,
				butTimeCard,
				label2,
				butClockIn,
				butClockOut
				});
		}

		///<summary></summary>
		public void ModuleSelected(){
			RefreshModuleData();
			RefreshModuleScreen();
		}

		///<summary></summary>
		public void ModuleUnselected(){
			//this is not getting triggered yet.
		}

		private void RefreshModuleData(){
			TimeDelta=ClockEvents.GetServerTime()-DateTime.Now;
			Employees.Refresh();
		}

		private void RefreshModuleScreen(){
			textTime.Text=(DateTime.Now+TimeDelta).ToLongTimeString();
			FillEmps();
		}

		///<summary></summary>
		private void OnPatientSelected(int patNum){
			PatientSelectedEventArgs eArgs=new OpenDental.PatientSelectedEventArgs(patNum);
			if(PatientSelected!=null)
				PatientSelected(this,eArgs);
		}

		private void butSendClaims_Click(object sender, System.EventArgs e) {
			Cursor=Cursors.WaitCursor;
			FormClaimsSend FormCS=new FormClaimsSend();
			FormCS.ShowDialog();
			if(FormCS.GotoPatNum!=0 && FormCS.GotoClaimNum!=0) {
				OnPatientSelected(FormCS.GotoPatNum);
				GotoModule.GotoClaim(FormCS.GotoClaimNum);
			}
			Cursor=Cursors.Default;
		}

		private void butBilling_Click(object sender,System.EventArgs e) {
			//if(!Security.IsAuthorized(Permissions.Setup)) {
			//	return;
			//}
			FormBillingOptions FormBO=new FormBillingOptions();
			FormBO.ShowDialog();
			//RefreshCurrentModule();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Billing");
		}


		private void butDeposit_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.DepositSlips,DateTime.Today)){
				return;
			}
			FormDeposits FormD=new FormDeposits();
			FormD.ShowDialog();
		}

		private void butAccounting_Click(object sender,System.EventArgs e) {
			FormAccounting FormA=new FormAccounting();
			FormA.Show();
		}

		private void butBackup_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Backup)){
				return;
			}
			FormBackup FormB=new FormBackup();
			FormB.ShowDialog();
			if(FormB.DialogResult==DialogResult.Cancel){
				return;
			}
			//ok signifies that a database was restored
			OnPatientSelected(0);
			ParentForm.Text=Prefs.GetString("MainWindowTitle");
			DataValid.SetInvalid(true);
			ModuleSelected();
		}

		private void butTasks_Click(object sender, System.EventArgs e) {
			FormTasks FormT=new FormTasks();
			FormT.ShowDialog();
			if(FormT.GotoType==TaskObjectType.Patient){
				if(FormT.GotoKeyNum!=0){
					OnPatientSelected(FormT.GotoKeyNum);
					GotoModule.GotoAccount();
				}
			}
			if(FormT.GotoType==TaskObjectType.Appointment){
				if(FormT.GotoKeyNum!=0){
					Appointment apt=Appointments.GetOneApt(FormT.GotoKeyNum);
					if(apt==null){
						MsgBox.Show(this,"Appointment has been deleted, so it's not available.");
						return;
						//this could be a little better, because window has closed, but they will learn not to push that button.
					}
					DateTime dateSelected=DateTime.MinValue;
					if(apt.AptStatus==ApptStatus.Planned || apt.AptStatus==ApptStatus.UnschedList){
						//I did not add feature to put planned or unsched apt on pinboard.
						MsgBox.Show(this,"Cannot navigate to appointment.  Use the Other Appointments button.");
						//return;
					}
					else{
						dateSelected=apt.AptDateTime;
					}
					OnPatientSelected(apt.PatNum);
					GotoModule.GotoAppointment(dateSelected,apt.AptNum);
				}
			}
		}

		private void butSend_Click(object sender, System.EventArgs e){
			Signal sig=new Signal();
			sig.SigType=SignalType.Text;
			sig.SigText=textMessage.Text;
			sig.FromUser=Security.CurUser.UserNum;
			sig.Insert();
		}

		///<summary></summary>
		public void LogMsg(string text){
			textMessage.Text=text;
		}

		private void butClear_Click(object sender, System.EventArgs e) {
			textMessage.Clear();
			textMessage.Select();
		}

		private void FillEmps(){
			gridEmp.BeginUpdate();
			gridEmp.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableEmpClock","Employee"),180);
			gridEmp.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableEmpClock","Status"),104);
			gridEmp.Columns.Add(col);
			gridEmp.Rows.Clear();
			UI.ODGridRow row;
			for(int i=0;i<Employees.ListShort.Length;i++){
				row=new OpenDental.UI.ODGridRow();
				row.Cells.Add(Employees.GetName(Employees.ListShort[i]));
				row.Cells.Add(Employees.ListShort[i].ClockStatus);
				gridEmp.Rows.Add(row);
			}
			gridEmp.EndUpdate();
			listStatus.Items.Clear();
			for(int i=0;i<Enum.GetNames(typeof(TimeClockStatus)).Length;i++){
				listStatus.Items.Add(Lan.g("enumTimeClockStatus",Enum.GetNames(typeof(TimeClockStatus))[i]));
			}
			for(int i=0;i<Employees.ListShort.Length;i++){
				if(Employees.ListShort[i].EmployeeNum==Security.CurUser.EmployeeNum){
					SelectEmpI(i);
					return;
				}
			}
			SelectEmpI(-1);
		}

		///<summary>-1 is also valid.</summary>
		private void SelectEmpI(int index){
			gridEmp.SetSelected(false);
			if(index==-1){
				butClockIn.Enabled=false;
				butClockOut.Enabled=false;
				butTimeCard.Enabled=false;
				butBreaks.Enabled=false;
				listStatus.Enabled=false;
				return;
			}
			gridEmp.SetSelected(index,true);
			Employees.Cur=Employees.ListShort[index];
			if(ClockEvents.IsClockedIn(Employees.Cur.EmployeeNum)){
				butClockIn.Enabled=false;
				butClockOut.Enabled=true;
				butTimeCard.Enabled=true;
				butBreaks.Enabled=true;
				listStatus.Enabled=true;
			}
			else{
				butClockIn.Enabled=true;
				butClockOut.Enabled=false;
				butTimeCard.Enabled=true;
				butBreaks.Enabled=true;
				listStatus.SelectedIndex=(int)ClockEvents.GetLastStatus(Employees.Cur.EmployeeNum);
				listStatus.Enabled=false;
			}
		}

		private void gridEmp_CellClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			if(Prefs.GetBool("TimecardSecurityEnabled")){
				if(Security.CurUser.EmployeeNum!=Employees.ListShort[e.Row].EmployeeNum){
					if(!Security.IsAuthorized(Permissions.TimecardsEditAll)){
						SelectEmpI(-1);
						return;
					}
				}
			}
			SelectEmpI(e.Row);
		}

		private void listStatus_Click(object sender, System.EventArgs e) {
			//
		}

		private void butClockIn_Click(object sender, System.EventArgs e) {
			ClockEvent ce=new ClockEvent();
			ce.EmployeeNum=Employees.Cur.EmployeeNum;
			ce.TimeEntered=DateTime.Now+TimeDelta;
			ce.TimeDisplayed=DateTime.Now+TimeDelta;
			ce.ClockIn=true;
			ce.ClockStatus=(TimeClockStatus)listStatus.SelectedIndex;
			ce.Insert();
			Employees.Cur.ClockStatus=Lan.g(this,"Working");;
			Employees.UpdateCur();
			ModuleSelected();
		}

		private void butClockOut_Click(object sender, System.EventArgs e) {
			if(listStatus.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select a status first."));
				return;
			}
			ClockEvent ce=new ClockEvent();
			ce.EmployeeNum=Employees.Cur.EmployeeNum;
			ce.TimeEntered=DateTime.Now+TimeDelta;
			ce.TimeDisplayed=DateTime.Now+TimeDelta;
			ce.ClockIn=false;
			ce.ClockStatus=(TimeClockStatus)listStatus.SelectedIndex;
			ce.Insert();
			Employees.Cur.ClockStatus=Lan.g("enumTimeClockStatus",ce.ClockStatus.ToString());
			Employees.UpdateCur();
			ModuleSelected();
		}

		private void timer1_Tick(object sender, System.EventArgs e) {
			//this will happen once a second
			if(this.Visible){
				textTime.Text=(DateTime.Now+TimeDelta).ToLongTimeString();
			}
		}

		private void butTimeCard_Click(object sender, System.EventArgs e) {
			if(PayPeriods.List.Length==0){
				MsgBox.Show(this,"The adminstrator needs to setup pay periods first.");
				return;
			}
			FormTimeCard FormTC=new FormTimeCard();
			FormTC.ShowDialog();
			ModuleSelected();
		}

		private void butBreaks_Click(object sender,EventArgs e) {
			if(PayPeriods.List.Length==0) {
				MsgBox.Show(this,"The adminstrator needs to setup pay periods first.");
				return;
			}
			FormTimeCard FormTC=new FormTimeCard();
			FormTC.IsBreaks=true;
			FormTC.ShowDialog();
			ModuleSelected();
		}

		

		

		

		

		

		

		

		




	}
}












