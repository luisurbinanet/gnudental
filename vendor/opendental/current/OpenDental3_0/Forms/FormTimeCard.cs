using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormTimeCard : System.Windows.Forms.Form{
		private OpenDental.Forms.TableTimeCard tbMain;
		private System.Windows.Forms.Label label1;
		private OpenDental.ValidDate textDateFrom;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private OpenDental.ValidDate textDateTo;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.RadioButton radioRegular;
		private System.Windows.Forms.RadioButton radioBreaks;
		private System.Windows.Forms.Button butRefresh;
		private System.Windows.Forms.Button butClose;
		private System.Windows.Forms.TextBox textTotal;
		private System.Windows.Forms.CheckBox checkRefresh;
		private System.Windows.Forms.Timer timer1;
		///<summary>True to default to viewing breaks. False for regular hours.</summary>
		public bool IsBreaks;
		///<summary>Server time minus local computer time, usually +/- 1 or 2 minutes</summary>
		private TimeSpan TimeDelta;

		///<summary></summary>
		public FormTimeCard()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.C("All", new System.Windows.Forms.Control[] {
				butClose
			});
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormTimeCard));
			this.butClose = new System.Windows.Forms.Button();
			this.tbMain = new OpenDental.Forms.TableTimeCard();
			this.textTotal = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textDateFrom = new OpenDental.ValidDate();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textDateTo = new OpenDental.ValidDate();
			this.butRefresh = new System.Windows.Forms.Button();
			this.radioRegular = new System.Windows.Forms.RadioButton();
			this.radioBreaks = new System.Windows.Forms.RadioButton();
			this.checkRefresh = new System.Windows.Forms.CheckBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClose.Location = new System.Drawing.Point(738, 650);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// tbMain
			// 
			this.tbMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.tbMain.BackColor = System.Drawing.SystemColors.Window;
			this.tbMain.Location = new System.Drawing.Point(18, 50);
			this.tbMain.Name = "tbMain";
			this.tbMain.ScrollValue = 1;
			this.tbMain.SelectedIndices = new int[0];
			this.tbMain.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbMain.Size = new System.Drawing.Size(659, 582);
			this.tbMain.TabIndex = 2;
			this.tbMain.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(this.tbMain_CellDoubleClicked);
			// 
			// textTotal
			// 
			this.textTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textTotal.Location = new System.Drawing.Point(348, 648);
			this.textTotal.Name = "textTotal";
			this.textTotal.Size = new System.Drawing.Size(66, 20);
			this.textTotal.TabIndex = 3;
			this.textTotal.Text = "";
			this.textTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(242, 649);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 17);
			this.label1.TabIndex = 4;
			this.label1.Text = "Total Time";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateFrom
			// 
			this.textDateFrom.Location = new System.Drawing.Point(108, 12);
			this.textDateFrom.Name = "textDateFrom";
			this.textDateFrom.Size = new System.Drawing.Size(89, 20);
			this.textDateFrom.TabIndex = 5;
			this.textDateFrom.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(20, 13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(86, 18);
			this.label2.TabIndex = 6;
			this.label2.Text = "From Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(211, 13);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 18);
			this.label3.TabIndex = 8;
			this.label3.Text = "To Date";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateTo
			// 
			this.textDateTo.Location = new System.Drawing.Point(293, 12);
			this.textDateTo.Name = "textDateTo";
			this.textDateTo.Size = new System.Drawing.Size(89, 20);
			this.textDateTo.TabIndex = 7;
			this.textDateTo.Text = "";
			// 
			// butRefresh
			// 
			this.butRefresh.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butRefresh.Location = new System.Drawing.Point(614, 9);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(87, 26);
			this.butRefresh.TabIndex = 11;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// radioRegular
			// 
			this.radioRegular.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioRegular.Location = new System.Drawing.Point(468, 7);
			this.radioRegular.Name = "radioRegular";
			this.radioRegular.Size = new System.Drawing.Size(115, 16);
			this.radioRegular.TabIndex = 0;
			this.radioRegular.Text = "Regular Hours";
			this.radioRegular.Click += new System.EventHandler(this.radioRegular_Click);
			// 
			// radioBreaks
			// 
			this.radioBreaks.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioBreaks.Location = new System.Drawing.Point(468, 26);
			this.radioBreaks.Name = "radioBreaks";
			this.radioBreaks.Size = new System.Drawing.Size(117, 16);
			this.radioBreaks.TabIndex = 1;
			this.radioBreaks.Text = "Breaks";
			this.radioBreaks.Click += new System.EventHandler(this.radioBreaks_Click);
			// 
			// checkRefresh
			// 
			this.checkRefresh.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkRefresh.Location = new System.Drawing.Point(712, 4);
			this.checkRefresh.Name = "checkRefresh";
			this.checkRefresh.Size = new System.Drawing.Size(104, 36);
			this.checkRefresh.TabIndex = 12;
			this.checkRefresh.Text = "Refresh once every second";
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 1000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// FormTimeCard
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(835, 686);
			this.Controls.Add(this.checkRefresh);
			this.Controls.Add(this.butRefresh);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textDateTo);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textDateFrom);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textTotal);
			this.Controls.Add(this.tbMain);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.radioRegular);
			this.Controls.Add(this.radioBreaks);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormTimeCard";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Time Card";
			this.Load += new System.EventHandler(this.FormTimeCard_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormTimeCard_Load(object sender, System.EventArgs e) {
			Text=Lan.g(this,"TimeCard for")+" "+Employees.Cur.FName+" "+Employees.Cur.LName;
			TimeDelta=ClockEvents.GetServerTime()-DateTime.Now;
			if(DateTime.Today.Day<16){//first half of month
				textDateFrom.Text=new DateTime(DateTime.Today.Year,DateTime.Today.Month,1).ToShortDateString();
				textDateTo.Text=new DateTime(DateTime.Today.Year,DateTime.Today.Month,15).ToShortDateString();
			}
			else{
				textDateFrom.Text=new DateTime(DateTime.Today.Year,DateTime.Today.Month,16).ToShortDateString();
				//Calendar cal=new Calendar();
				GregorianCalendar cal=new GregorianCalendar();
				textDateTo.Text=new DateTime(DateTime.Today.Year,DateTime.Today.Month,cal.GetDaysInMonth(DateTime.Today.Year,DateTime.Today.Month)).ToShortDateString();
			}
			if(IsBreaks){
				radioBreaks.Checked=true;
				checkRefresh.Checked=true;
			}
			else
				radioRegular.Checked=true;
			FillMain(true);
		}

		///<summary>fromDB is set to false when it is refreshing every second so that there will be no extra network traffic.</summary>
		private void FillMain(bool fromDB){
			if(fromDB){
				ClockEvents.Refresh(PIn.PDate(textDateFrom.Text),PIn.PDate(textDateTo.Text),false,radioBreaks.Checked);
			}
			tbMain.ResetRows(ClockEvents.List.Length);
			if(radioBreaks.Checked){
				tbMain.Fields[5]=Lan.g(this,"Minutes");
			}
			else{
				tbMain.Fields[5]=Lan.g(this,"Hours");
			}
			tbMain.SetGridColor(Color.Gray);
			tbMain.SetBackGColor(Color.White);
			TimeSpan alteredSpan=new TimeSpan(0);//used to display altered times
			TimeSpan pairSpan=new TimeSpan(0);//used to sum one pair of clockevents
			TimeSpan daySpan=new TimeSpan(0);//used for daily totals.
			TimeSpan periodSpan=new TimeSpan(0);//used to add up totals for entire page.
			for(int i=0;i<ClockEvents.List.Length;i++){
				//if this is the first entry for a given date
				if(i==0 || ClockEvents.List[i-1].TimeDisplayed.Date!=ClockEvents.List[i].TimeDisplayed.Date){
					tbMain.Cell[0,i]=ClockEvents.List[i].TimeDisplayed.ToShortDateString();
				}
				if(ClockEvents.List[i].TimeEntered!=ClockEvents.List[i].TimeDisplayed){
					alteredSpan=ClockEvents.List[i].TimeDisplayed
						-ClockEvents.List[i].TimeEntered;
					if(radioBreaks.Checked){
						tbMain.Cell[1,i]=alteredSpan.TotalMinutes.ToString("n");
					}
					else{
						tbMain.Cell[1,i]=alteredSpan.TotalHours.ToString("n");
					}
				}
				tbMain.Cell[2,i]=ClockEvents.List[i].ClockStatus.ToString();
				if(ClockEvents.List[i].ClockIn)
					tbMain.Cell[3,i]="In";
				else
					tbMain.Cell[3,i]="Out";
				tbMain.Cell[4,i]=ClockEvents.List[i].TimeDisplayed.ToShortTimeString();
				//this tests to see if a time should be calculated
				//remember, it's opposite if checking breaks.
				if((radioRegular.Checked && !ClockEvents.List[i].ClockIn)//if regular hours and clocked out
					|| (radioBreaks.Checked && ClockEvents.List[i].ClockIn))//or break hours and clocked in
				{//then display the timespan of pairSpan
					if(i!=0){//unless there is not a previous entry
						pairSpan=ClockEvents.List[i].TimeDisplayed
							-ClockEvents.List[i-1].TimeDisplayed;
						if(radioBreaks.Checked){
							tbMain.Cell[5,i]=pairSpan.TotalMinutes.ToString("n");
						}
						else{
							tbMain.Cell[5,i]=pairSpan.TotalHours.ToString("n");
						}
						daySpan+=pairSpan;
						periodSpan+=pairSpan;
					}
				}
				//if this is the last entry for a given date
				if(i==ClockEvents.List.Length-1
					|| ClockEvents.List[i+1].TimeDisplayed.Date!=ClockEvents.List[i].TimeDisplayed.Date){
					if(radioBreaks.Checked){
						if(!ClockEvents.List[i].ClockIn){//if they have not clocked back in yet from break
							//display the timespan of pairSpan using current time as the other number.
							pairSpan=DateTime.Now-ClockEvents.List[i].TimeDisplayed+TimeDelta;
							tbMain.Cell[5,i]=pairSpan.TotalMinutes.ToString("n");
							daySpan+=pairSpan;
						}
						tbMain.Cell[6,i]=daySpan.TotalMinutes.ToString("n");
					}
					else{
						tbMain.Cell[6,i]=daySpan.TotalHours.ToString("n");
					}
					daySpan=new TimeSpan(0);
				}
				tbMain.Cell[7,i]=ClockEvents.List[i].Note;
			}
			tbMain.LayoutTables();
			if(radioBreaks.Checked){
				textTotal.Text="";
			}
			else{
				textTotal.Text=periodSpan.TotalHours.ToString("n");
			}
		}

		private void butRefresh_Click(object sender, System.EventArgs e) {
			if(!DataIsValid()){
				return;
			}
			FillMain(true);
		}

		private void radioRegular_Click(object sender, System.EventArgs e) {
			if(!DataIsValid()){
				return;
			}
			checkRefresh.Checked=false;
			FillMain(true);
		}

		private void radioBreaks_Click(object sender, System.EventArgs e) {
			if(!DataIsValid()){
				return;
			}
			checkRefresh.Checked=true;
			FillMain(true);
		}

		private bool DataIsValid(){
			if(textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateTo.errorProvider1.GetError(textDateTo)!=""){
				MessageBox.Show(Lan.g(this,"Please fix errors first."));
				return false;
			}
			return true;
		}

		private void tbMain_CellDoubleClicked(object sender, OpenDental.CellEventArgs e) {
			timer1.Enabled=false;
			ClockEvents.Cur=ClockEvents.List[e.Row];
			FormClockEventEdit FormCEE=new FormClockEventEdit();
			FormCEE.ShowDialog();
			FillMain(true);
			timer1.Enabled=true;
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void timer1_Tick(object sender, System.EventArgs e) {
			if(checkRefresh.Checked){
				FillMain(false);
			}
		}

		

		

		

		


	}
}





















