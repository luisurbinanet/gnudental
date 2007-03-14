using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary></summary>
	public class ContrStaff : System.Windows.Forms.UserControl{
		private System.Windows.Forms.Button butTimeCard;
		private System.Windows.Forms.ListBox listStatus;
		private OpenDental.TableEmpClock tbEmp;
		private System.Windows.Forms.Label textTime;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Button butClockIn;
		private System.Windows.Forms.Button butClockOut;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button butClear;
		private System.Windows.Forms.Button butSend;
		private System.Windows.Forms.TextBox textMessage;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.ComponentModel.IContainer components;
		///<summary>Server time minus local computer time, usually +/- 1 or 2 minutes</summary>
		private TimeSpan TimeDelta;

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
			this.butClockIn = new System.Windows.Forms.Button();
			this.listStatus = new System.Windows.Forms.ListBox();
			this.butClockOut = new System.Windows.Forms.Button();
			this.tbEmp = new OpenDental.TableEmpClock();
			this.butTimeCard = new System.Windows.Forms.Button();
			this.textTime = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.butClear = new System.Windows.Forms.Button();
			this.butSend = new System.Windows.Forms.Button();
			this.textMessage = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// butClockIn
			// 
			this.butClockIn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClockIn.Location = new System.Drawing.Point(371, 127);
			this.butClockIn.Name = "butClockIn";
			this.butClockIn.Size = new System.Drawing.Size(120, 23);
			this.butClockIn.TabIndex = 11;
			this.butClockIn.Text = "Clock In";
			this.butClockIn.Click += new System.EventHandler(this.butClockIn_Click);
			// 
			// listStatus
			// 
			this.listStatus.Location = new System.Drawing.Point(373, 175);
			this.listStatus.Name = "listStatus";
			this.listStatus.Size = new System.Drawing.Size(120, 43);
			this.listStatus.TabIndex = 12;
			// 
			// butClockOut
			// 
			this.butClockOut.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClockOut.Location = new System.Drawing.Point(371, 150);
			this.butClockOut.Name = "butClockOut";
			this.butClockOut.Size = new System.Drawing.Size(120, 23);
			this.butClockOut.TabIndex = 14;
			this.butClockOut.Text = "Clock Out For:";
			this.butClockOut.Click += new System.EventHandler(this.butClockOut_Click);
			// 
			// tbEmp
			// 
			this.tbEmp.BackColor = System.Drawing.SystemColors.Window;
			this.tbEmp.Location = new System.Drawing.Point(58, 38);
			this.tbEmp.Name = "tbEmp";
			this.tbEmp.ScrollValue = 280;
			this.tbEmp.SelectedIndices = new int[0];
			this.tbEmp.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbEmp.Size = new System.Drawing.Size(299, 229);
			this.tbEmp.TabIndex = 15;
			this.tbEmp.CellClicked += new OpenDental.ContrTable.CellEventHandler(this.tbEmp_CellClicked);
			// 
			// butTimeCard
			// 
			this.butTimeCard.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butTimeCard.Location = new System.Drawing.Point(371, 38);
			this.butTimeCard.Name = "butTimeCard";
			this.butTimeCard.Size = new System.Drawing.Size(120, 23);
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
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.listStatus);
			this.groupBox1.Controls.Add(this.butClockOut);
			this.groupBox1.Controls.Add(this.tbEmp);
			this.groupBox1.Controls.Add(this.butTimeCard);
			this.groupBox1.Controls.Add(this.textTime);
			this.groupBox1.Controls.Add(this.butClockIn);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(171, 397);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(530, 310);
			this.groupBox1.TabIndex = 18;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Time Clock";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.butClear);
			this.groupBox2.Controls.Add(this.butSend);
			this.groupBox2.Controls.Add(this.textMessage);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(171, 90);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(530, 229);
			this.groupBox2.TabIndex = 19;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Messaging";
			// 
			// butClear
			// 
			this.butClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClear.Location = new System.Drawing.Point(167, 175);
			this.butClear.Name = "butClear";
			this.butClear.TabIndex = 3;
			this.butClear.Text = "Clear";
			this.butClear.Click += new System.EventHandler(this.butClear_Click);
			// 
			// butSend
			// 
			this.butSend.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butSend.Location = new System.Drawing.Point(30, 175);
			this.butSend.Name = "butSend";
			this.butSend.TabIndex = 2;
			this.butSend.Text = "Send";
			this.butSend.Click += new System.EventHandler(this.butSend_Click);
			// 
			// textMessage
			// 
			this.textMessage.Location = new System.Drawing.Point(28, 64);
			this.textMessage.Multiline = true;
			this.textMessage.Name = "textMessage";
			this.textMessage.Size = new System.Drawing.Size(419, 89);
			this.textMessage.TabIndex = 1;
			this.textMessage.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(28, 34);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "General Message";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
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
			// ContrStaff
			// 
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "ContrStaff";
			this.Size = new System.Drawing.Size(908, 702);
			this.Load += new System.EventHandler(this.ContrStaff_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void ContrStaff_Load(object sender, System.EventArgs e) {
		
		}

		///<summary></summary>
		public void InstantClasses(){
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.butClear,
				this.butSend,
				this.label1,
			});
		}

		///<summary></summary>
		public void ModuleSelected(){
			RefreshModuleData();
			RefreshModuleScreen();
		}

		///<summary></summary>
		public void ModuleUnselected(){
//todo: this is not getting triggered yet.
		}

		private void RefreshModuleData(){
			TimeDelta=ClockEvents.GetServerTime()-DateTime.Now;
			Employees.Refresh();
		}

		private void RefreshModuleScreen(){
			textTime.Text=(DateTime.Now+TimeDelta).ToLongTimeString();
			FillEmps();
		}

		private void butSend_Click(object sender, System.EventArgs e) {
			Messages Messages=new Messages();//move this later
			Messages.ButtonsToSend=new MessageButtons();
			Messages.MessageToSend=new MessageInvalid();//because this value is tested when processing
			Messages.ButtonsToSend.Type="Text";
			Messages.ButtonsToSend.Text=textMessage.Text;
			Messages.ButtonsToSend.Row=0;
			Messages.ButtonsToSend.Col=0;
			Messages.ButtonsToSend.Pushed=false;
			Messages.SendButtons();
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
			tbEmp.ResetRows(Employees.ListShort.Length);
			tbEmp.SetGridColor(Color.Gray);
			tbEmp.SetBackGColor(Color.White);
			for(int i=0;i<Employees.ListShort.Length;i++){
				tbEmp.Cell[0,i]=Employees.GetName(Employees.ListShort[i]);
				tbEmp.Cell[1,i]=Employees.ListShort[i].ClockStatus;
			}
			tbEmp.LayoutTables();
			listStatus.Items.Clear();
			for(int i=0;i<Enum.GetNames(typeof(TimeClockStatus)).Length;i++){
				listStatus.Items.Add(Enum.GetNames(typeof(TimeClockStatus))[i]);
			}
			butClockIn.Enabled=false;
			butClockOut.Enabled=false;
			butTimeCard.Enabled=false;
			listStatus.Enabled=false;
		}

		private void tbEmp_CellClicked(object sender, OpenDental.CellEventArgs e) {
			Employees.Cur=Employees.ListShort[e.Row];
			//MessageBox.Show("1");
			ClockEvents.Refresh();
			//MessageBox.Show("2");
			if(ClockEvents.IsClockedIn()){
				butClockIn.Enabled=false;
				butClockOut.Enabled=true;
				butTimeCard.Enabled=true;
				listStatus.Enabled=true;
			}
			else{
				butClockIn.Enabled=true;
				butClockOut.Enabled=false;
				butTimeCard.Enabled=true;
				//MessageBox.Show("3");
				listStatus.SelectedIndex=(int)ClockEvents.GetLastStatus();
				//MessageBox.Show("4");
				listStatus.Enabled=false;
			}
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
			Employees.Cur.ClockStatus="Working";
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
			Employees.Cur.ClockStatus=ClockEvents.Cur.ClockStatus.ToString();
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
			if(ClockEvents.GetLastStatus()==TimeClockStatus.Break){
				FormTC.IsBreaks=true;
			}
			FormTC.ShowDialog();
			ModuleSelected();
		}

		

		

		

		




	}
}












