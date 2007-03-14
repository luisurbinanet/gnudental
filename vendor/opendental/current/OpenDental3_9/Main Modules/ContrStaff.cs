using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ContrStaff));
			this.butClockIn = new OpenDental.UI.Button();
			this.listStatus = new System.Windows.Forms.ListBox();
			this.butClockOut = new OpenDental.UI.Button();
			this.butTimeCard = new OpenDental.UI.Button();
			this.textTime = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.gridEmp = new OpenDental.UI.ODGrid();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.butClear = new OpenDental.UI.Button();
			this.butSend = new OpenDental.UI.Button();
			this.textMessage = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butSendClaims = new OpenDental.UI.Button();
			this.butTasks = new OpenDental.UI.Button();
			this.butBackup = new OpenDental.UI.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.butDeposit = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// butClockIn
			// 
			this.butClockIn.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClockIn.Autosize = true;
			this.butClockIn.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClockIn.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClockIn.Location = new System.Drawing.Point(371, 127);
			this.butClockIn.Name = "butClockIn";
			this.butClockIn.Size = new System.Drawing.Size(120, 25);
			this.butClockIn.TabIndex = 11;
			this.butClockIn.Text = "Clock In";
			this.butClockIn.Click += new System.EventHandler(this.butClockIn_Click);
			// 
			// listStatus
			// 
			this.listStatus.Location = new System.Drawing.Point(372, 182);
			this.listStatus.Name = "listStatus";
			this.listStatus.Size = new System.Drawing.Size(120, 43);
			this.listStatus.TabIndex = 12;
			// 
			// butClockOut
			// 
			this.butClockOut.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClockOut.Autosize = true;
			this.butClockOut.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClockOut.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClockOut.Location = new System.Drawing.Point(371, 154);
			this.butClockOut.Name = "butClockOut";
			this.butClockOut.Size = new System.Drawing.Size(120, 25);
			this.butClockOut.TabIndex = 14;
			this.butClockOut.Text = "Clock Out For:";
			this.butClockOut.Click += new System.EventHandler(this.butClockOut_Click);
			// 
			// butTimeCard
			// 
			this.butTimeCard.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butTimeCard.Autosize = true;
			this.butTimeCard.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTimeCard.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTimeCard.Location = new System.Drawing.Point(371, 38);
			this.butTimeCard.Name = "butTimeCard";
			this.butTimeCard.Size = new System.Drawing.Size(120, 25);
			this.butTimeCard.TabIndex = 16;
			this.butTimeCard.Text = "View Timecard";
			this.butTimeCard.Click += new System.EventHandler(this.butTimeCard_Click);
			// 
			// textTime
			// 
			this.textTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textTime.Location = new System.Drawing.Point(370, 96);
			this.textTime.Name = "textTime";
			this.textTime.Size = new System.Drawing.Size(121, 28);
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
			this.groupBox1.Controls.Add(this.gridEmp);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.listStatus);
			this.groupBox1.Controls.Add(this.butClockOut);
			this.groupBox1.Controls.Add(this.butTimeCard);
			this.groupBox1.Controls.Add(this.textTime);
			this.groupBox1.Controls.Add(this.butClockIn);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(103, 352);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(530, 288);
			this.groupBox1.TabIndex = 18;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Time Clock";
			// 
			// gridEmp
			// 
			this.gridEmp.AllowSelection = false;
			this.gridEmp.Columns.Add(new OpenDental.UI.ODGridColumn("Employee", 180, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridEmp.Columns.Add(new OpenDental.UI.ODGridColumn("Status", 104, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridEmp.HScrollVisible = false;
			this.gridEmp.Location = new System.Drawing.Point(22, 26);
			this.gridEmp.Name = "gridEmp";
			this.gridEmp.ScrollValue = 0;
			this.gridEmp.Size = new System.Drawing.Size(303, 238);
			this.gridEmp.TabIndex = 21;
			this.gridEmp.Title = "Employee";
			this.gridEmp.TranslationName = "TableEmpClock";
			this.gridEmp.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridEmp_CellClick);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(381, 84);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 19);
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
			this.groupBox2.Location = new System.Drawing.Point(103, 164);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(530, 164);
			this.groupBox2.TabIndex = 19;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Messaging";
			// 
			// butClear
			// 
			this.butClear.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClear.Autosize = true;
			this.butClear.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClear.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClear.Location = new System.Drawing.Point(120, 124);
			this.butClear.Name = "butClear";
			this.butClear.Size = new System.Drawing.Size(75, 25);
			this.butClear.TabIndex = 3;
			this.butClear.Text = "Clear";
			this.butClear.Click += new System.EventHandler(this.butClear_Click);
			// 
			// butSend
			// 
			this.butSend.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSend.Autosize = true;
			this.butSend.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSend.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSend.Location = new System.Drawing.Point(27, 124);
			this.butSend.Name = "butSend";
			this.butSend.Size = new System.Drawing.Size(75, 25);
			this.butSend.TabIndex = 2;
			this.butSend.Text = "Send";
			this.butSend.Click += new System.EventHandler(this.butSend_Click);
			// 
			// textMessage
			// 
			this.textMessage.Location = new System.Drawing.Point(28, 51);
			this.textMessage.Multiline = true;
			this.textMessage.Name = "textMessage";
			this.textMessage.Size = new System.Drawing.Size(419, 61);
			this.textMessage.TabIndex = 1;
			this.textMessage.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(28, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(464, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "General Message  (don\'t overuse this since it can be annoying)";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butSendClaims
			// 
			this.butSendClaims.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSendClaims.Autosize = true;
			this.butSendClaims.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSendClaims.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSendClaims.Location = new System.Drawing.Point(16, 19);
			this.butSendClaims.Name = "butSendClaims";
			this.butSendClaims.Size = new System.Drawing.Size(104, 26);
			this.butSendClaims.TabIndex = 20;
			this.butSendClaims.Text = "Send Claims";
			this.butSendClaims.Click += new System.EventHandler(this.butSendClaims_Click);
			// 
			// butTasks
			// 
			this.butTasks.AdjustImageLocation = new System.Drawing.Point(0, 1);
			this.butTasks.Autosize = true;
			this.butTasks.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTasks.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTasks.Image = ((System.Drawing.Image)(resources.GetObject("butTasks.Image")));
			this.butTasks.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butTasks.Location = new System.Drawing.Point(198, 19);
			this.butTasks.Name = "butTasks";
			this.butTasks.Size = new System.Drawing.Size(104, 26);
			this.butTasks.TabIndex = 21;
			this.butTasks.Text = "Tasks";
			this.butTasks.Click += new System.EventHandler(this.butTasks_Click);
			// 
			// butBackup
			// 
			this.butBackup.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butBackup.Autosize = true;
			this.butBackup.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBackup.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBackup.Location = new System.Drawing.Point(198, 56);
			this.butBackup.Name = "butBackup";
			this.butBackup.Size = new System.Drawing.Size(104, 26);
			this.butBackup.TabIndex = 22;
			this.butBackup.Text = "Backup";
			this.butBackup.Click += new System.EventHandler(this.butBackup_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.butDeposit);
			this.groupBox3.Controls.Add(this.butSendClaims);
			this.groupBox3.Controls.Add(this.butBackup);
			this.groupBox3.Controls.Add(this.butTasks);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(103, 23);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(328, 99);
			this.groupBox3.TabIndex = 23;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Daily";
			// 
			// butDeposit
			// 
			this.butDeposit.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDeposit.Autosize = true;
			this.butDeposit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDeposit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDeposit.Location = new System.Drawing.Point(16, 56);
			this.butDeposit.Name = "butDeposit";
			this.butDeposit.Size = new System.Drawing.Size(104, 26);
			this.butDeposit.TabIndex = 23;
			this.butDeposit.Text = "Deposit Slips";
			this.butDeposit.Click += new System.EventHandler(this.butDeposit_Click);
			// 
			// ContrStaff
			// 
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "ContrStaff";
			this.Size = new System.Drawing.Size(908, 702);
			this.Load += new System.EventHandler(this.ContrStaff_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
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
			Cursor=Cursors.Default;
		}

		private void butDeposit_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.DepositSlips)){
				return;
			}
			FormDeposits FormD=new FormDeposits();
			FormD.ShowDialog();
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
					GotoModule.GoNow(DateTime.MinValue,new Appointment(),0,2);//jump to Account module
				}
			}
			if(FormT.GotoType==TaskObjectType.Appointment){
				if(FormT.GotoKeyNum!=0){
					Appointment apt=Appointments.GetOneApt(FormT.GotoKeyNum);
					DateTime dateSelected=DateTime.MinValue;
					if(apt.AptStatus==ApptStatus.Planned || apt.AptStatus==ApptStatus.UnschedList){
						//I did not add feature to put planned or unsched apt on pinboard.
						MsgBox.Show(this,"Cannot navigate to appointment.  Use the Other Appointments button.");
					}
					else{
						dateSelected=apt.AptDateTime;
					}
					OnPatientSelected(apt.PatNum);
					GotoModule.GoNow(dateSelected,new Appointment(),apt.AptNum,0);
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
				listStatus.Enabled=false;
				return;
			}
			gridEmp.SetSelected(index,true);
			Employees.Cur=Employees.ListShort[index];
			if(ClockEvents.IsClockedIn(Employees.Cur.EmployeeNum)){
				butClockIn.Enabled=false;
				butClockOut.Enabled=true;
				butTimeCard.Enabled=true;
				listStatus.Enabled=true;
			}
			else{
				butClockIn.Enabled=true;
				butClockOut.Enabled=false;
				butTimeCard.Enabled=true;
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
			ClockEvents.Cur=new ClockEvent();
			ClockEvents.Cur.EmployeeNum=Employees.Cur.EmployeeNum;
			ClockEvents.Cur.TimeEntered=DateTime.Now+TimeDelta;
			ClockEvents.Cur.TimeDisplayed=DateTime.Now+TimeDelta;
			ClockEvents.Cur.ClockIn=true;
			ClockEvents.Cur.ClockStatus=(TimeClockStatus)listStatus.SelectedIndex;
			ClockEvents.InsertCur();
			Employees.Cur.ClockStatus=Lan.g(this,"Working");;
			Employees.UpdateCur();
			ModuleSelected();
		}

		private void butClockOut_Click(object sender, System.EventArgs e) {
			if(listStatus.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select a status first."));
				return;
			}
			ClockEvents.Cur=new ClockEvent();
			ClockEvents.Cur.EmployeeNum=Employees.Cur.EmployeeNum;
			ClockEvents.Cur.TimeEntered=DateTime.Now+TimeDelta;
			ClockEvents.Cur.TimeDisplayed=DateTime.Now+TimeDelta;
			ClockEvents.Cur.ClockIn=false;
			ClockEvents.Cur.ClockStatus=(TimeClockStatus)listStatus.SelectedIndex;
			ClockEvents.InsertCur();
			Employees.Cur.ClockStatus
				=Lan.g("enumTimeClockStatus",ClockEvents.Cur.ClockStatus.ToString());
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
			FormTimeCard FormTC=new FormTimeCard();
			if(ClockEvents.GetLastStatus(Employees.Cur.EmployeeNum)==TimeClockStatus.Break){
				FormTC.IsBreaks=true;
			}
			FormTC.ShowDialog();
			ModuleSelected();
		}

		

		

		

		

		

		

		

		




	}
}












