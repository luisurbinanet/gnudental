using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using OpenDental.UI;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormTimeCard:System.Windows.Forms.Form {
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.ComponentModel.IContainer components;
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.TextBox textTotal;
		private System.Windows.Forms.Timer timer1;
		///<summary>True to default to viewing breaks. False for regular hours.</summary>
		public bool IsBreaks;
		///<summary>Server time minus local computer time, usually +/- 1 or 2 minutes</summary>
		private TimeSpan TimeDelta;
		private OpenDental.UI.ODGrid gridMain;
		private GroupBox groupBox1;
		private OpenDental.UI.Button butRight;
		private OpenDental.UI.Button butLeft;
		private Label label4;
		private TextBox textDateStart;
		private TextBox textDatePaycheck;
		private TextBox textDateStop;
		private ClockEvent[] ClockEventList;
		private OpenDental.UI.Button butAdj;
		private int SelectedPayPeriod;
		private Label labelOvertime;
		private TextBox textOvertime;
		private OpenDental.UI.Button butCompute;
		private OpenDental.UI.Button butPrint;
		private TimeAdjust[] TimeAdjustList;
		private PrintDocument pd;
		private int linesPrinted;
		private OpenDental.UI.PrintPreview printPreview;

		///<summary></summary>
		public FormTimeCard()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTimeCard));
			this.textTotal = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textDatePaycheck = new System.Windows.Forms.TextBox();
			this.textDateStop = new System.Windows.Forms.TextBox();
			this.textDateStart = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.labelOvertime = new System.Windows.Forms.Label();
			this.textOvertime = new System.Windows.Forms.TextBox();
			this.butCompute = new OpenDental.UI.Button();
			this.butAdj = new OpenDental.UI.Button();
			this.butRight = new OpenDental.UI.Button();
			this.butLeft = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butClose = new OpenDental.UI.Button();
			this.butPrint = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textTotal
			// 
			this.textTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textTotal.Location = new System.Drawing.Point(491,642);
			this.textTotal.Name = "textTotal";
			this.textTotal.Size = new System.Drawing.Size(66,20);
			this.textTotal.TabIndex = 3;
			this.textTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label1.Location = new System.Drawing.Point(385,643);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,17);
			this.label1.TabIndex = 4;
			this.label1.Text = "Regular Time";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(146,8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96,18);
			this.label2.TabIndex = 6;
			this.label2.Text = "Start Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(143,28);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(99,18);
			this.label3.TabIndex = 8;
			this.label3.Text = "End Date";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 1000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textDatePaycheck);
			this.groupBox1.Controls.Add(this.textDateStop);
			this.groupBox1.Controls.Add(this.textDateStart);
			this.groupBox1.Controls.Add(this.butRight);
			this.groupBox1.Controls.Add(this.butLeft);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(18,3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(659,51);
			this.groupBox1.TabIndex = 14;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Pay Period";
			// 
			// textDatePaycheck
			// 
			this.textDatePaycheck.Location = new System.Drawing.Point(473,19);
			this.textDatePaycheck.Name = "textDatePaycheck";
			this.textDatePaycheck.ReadOnly = true;
			this.textDatePaycheck.Size = new System.Drawing.Size(100,20);
			this.textDatePaycheck.TabIndex = 14;
			// 
			// textDateStop
			// 
			this.textDateStop.Location = new System.Drawing.Point(244,28);
			this.textDateStop.Name = "textDateStop";
			this.textDateStop.ReadOnly = true;
			this.textDateStop.Size = new System.Drawing.Size(100,20);
			this.textDateStop.TabIndex = 13;
			// 
			// textDateStart
			// 
			this.textDateStart.Location = new System.Drawing.Point(244,8);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.ReadOnly = true;
			this.textDateStart.Size = new System.Drawing.Size(100,20);
			this.textDateStart.TabIndex = 12;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(354,19);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(117,18);
			this.label4.TabIndex = 9;
			this.label4.Text = "Paycheck Date";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelOvertime
			// 
			this.labelOvertime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelOvertime.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelOvertime.Location = new System.Drawing.Point(385,663);
			this.labelOvertime.Name = "labelOvertime";
			this.labelOvertime.Size = new System.Drawing.Size(100,17);
			this.labelOvertime.TabIndex = 17;
			this.labelOvertime.Text = "Overtime";
			this.labelOvertime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textOvertime
			// 
			this.textOvertime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textOvertime.Location = new System.Drawing.Point(491,662);
			this.textOvertime.Name = "textOvertime";
			this.textOvertime.Size = new System.Drawing.Size(66,20);
			this.textOvertime.TabIndex = 16;
			this.textOvertime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// butCompute
			// 
			this.butCompute.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCompute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butCompute.Autosize = true;
			this.butCompute.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCompute.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCompute.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butCompute.Location = new System.Drawing.Point(145,650);
			this.butCompute.Name = "butCompute";
			this.butCompute.Size = new System.Drawing.Size(115,26);
			this.butCompute.TabIndex = 18;
			this.butCompute.Text = "Compute Overtime";
			this.butCompute.Click += new System.EventHandler(this.butCompute_Click);
			// 
			// butAdj
			// 
			this.butAdj.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAdj.Autosize = true;
			this.butAdj.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdj.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdj.Image = ((System.Drawing.Image)(resources.GetObject("butAdj.Image")));
			this.butAdj.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdj.Location = new System.Drawing.Point(18,650);
			this.butAdj.Name = "butAdj";
			this.butAdj.Size = new System.Drawing.Size(115,26);
			this.butAdj.TabIndex = 15;
			this.butAdj.Text = "Add Adjustment";
			this.butAdj.Click += new System.EventHandler(this.butAdj_Click);
			// 
			// butRight
			// 
			this.butRight.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRight.Autosize = true;
			this.butRight.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRight.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRight.Image = ((System.Drawing.Image)(resources.GetObject("butRight.Image")));
			this.butRight.Location = new System.Drawing.Point(63,18);
			this.butRight.Name = "butRight";
			this.butRight.Size = new System.Drawing.Size(39,24);
			this.butRight.TabIndex = 11;
			this.butRight.Click += new System.EventHandler(this.butRight_Click);
			// 
			// butLeft
			// 
			this.butLeft.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLeft.Autosize = true;
			this.butLeft.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLeft.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLeft.Image = ((System.Drawing.Image)(resources.GetObject("butLeft.Image")));
			this.butLeft.Location = new System.Drawing.Point(13,18);
			this.butLeft.Name = "butLeft";
			this.butLeft.Size = new System.Drawing.Size(39,24);
			this.butLeft.TabIndex = 10;
			this.butLeft.Click += new System.EventHandler(this.butLeft_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(18,60);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(851,580);
			this.gridMain.TabIndex = 13;
			this.gridMain.Title = "Time Card";
			this.gridMain.TranslationName = "TableTimeCard";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.Location = new System.Drawing.Point(794,650);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.Image = ((System.Drawing.Image)(resources.GetObject("butPrint.Image")));
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(691,650);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(86,26);
			this.butPrint.TabIndex = 19;
			this.butPrint.Text = "Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// FormTimeCard
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(891,686);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.butCompute);
			this.Controls.Add(this.labelOvertime);
			this.Controls.Add(this.textOvertime);
			this.Controls.Add(this.butAdj);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textTotal);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormTimeCard";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Time Card";
			this.Load += new System.EventHandler(this.FormTimeCard_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormTimeCard_Load(object sender, System.EventArgs e) {
			Text=Lan.g(this,"TimeCard for")+" "+Employees.Cur.FName+" "+Employees.Cur.LName;
			TimeDelta=ClockEvents.GetServerTime()-DateTime.Now;
			SelectedPayPeriod=PayPeriods.GetForDate(DateTime.Today);
			if(IsBreaks){
				textOvertime.Visible=false;
				labelOvertime.Visible=false;
				butCompute.Visible=false;
				butAdj.Visible=false;
			}
			FillPayPeriod();
			FillMain(true);
		}

		private void butLeft_Click(object sender,EventArgs e) {
			if(SelectedPayPeriod==0){
				return;
			}
			SelectedPayPeriod--;
			FillPayPeriod();
			FillMain(true);
		}

		private void butRight_Click(object sender,EventArgs e) {
			if(SelectedPayPeriod==PayPeriods.List.Length-1) {
				return;
			}
			SelectedPayPeriod++;
			FillPayPeriod();
			FillMain(true);
		}

		///<summary>SelectedPayPeriod should already be set.  This simply fills the screen with that data.</summary>
		private void FillPayPeriod(){
			textDateStart.Text=PayPeriods.List[SelectedPayPeriod].DateStart.ToShortDateString();
			textDateStop.Text=PayPeriods.List[SelectedPayPeriod].DateStop.ToShortDateString();
			if(PayPeriods.List[SelectedPayPeriod].DatePaycheck.Year<1880){
				textDatePaycheck.Text="";
			}
			else{
				textDatePaycheck.Text=PayPeriods.List[SelectedPayPeriod].DatePaycheck.ToShortDateString();
			}
		}

		///<summary>fromDB is set to false when it is refreshing every second so that there will be no extra network traffic.</summary>
		private void FillMain(bool fromDB){
			if(fromDB){
				ClockEventList=ClockEvents.Refresh(Employees.Cur.EmployeeNum,PIn.PDate(textDateStart.Text),
					PIn.PDate(textDateStop.Text),false,IsBreaks);
				if(IsBreaks){
					TimeAdjustList=new TimeAdjust[0];
				}
				else{
					TimeAdjustList=TimeAdjusts.Refresh(Employees.Cur.EmployeeNum,PIn.PDate(textDateStart.Text),
						PIn.PDate(textDateStop.Text));
				}
			}
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Date"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Weekday"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Altered"),50,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Status"),50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"In/Out"),60,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Time"),60,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			if(IsBreaks){
				col=new ODGridColumn(Lan.g(this,"Minutes"),50,HorizontalAlignment.Right);
			}
			else{
				col=new ODGridColumn(Lan.g(this,"Hours"),50,HorizontalAlignment.Right);
			}
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Overtime"),55,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Daily"),50,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Weekly"),50,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Note"),5);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			TimeSpan alteredSpan=new TimeSpan(0);//used to display altered times
			TimeSpan pairSpan=new TimeSpan(0);//used to sum one pair of clockevents
			TimeSpan daySpan=new TimeSpan(0);//used for daily totals.
			TimeSpan weekSpan=new TimeSpan(0);//used for weekly totals.
			if(ClockEventList.Length>0){
				ClockEvents.GetWeekTotal(Employees.Cur.EmployeeNum,ClockEventList[0].TimeDisplayed.Date);
			}
			//MessageBox.Show(weekSpan.TotalHours.ToString());
			TimeSpan periodSpan=new TimeSpan(0);//used to add up totals for entire page.
			TimeSpan otspan=new TimeSpan(0);//overtime for the entire period
      Calendar cal=CultureInfo.CurrentCulture.Calendar;
			CalendarWeekRule rule=CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule;
			int c=0;//clockevent index
			int a=0;//timeadjust index
			DateTime curDate=DateTime.MinValue;
			DateTime previousDate=DateTime.MinValue;
			bool isAdj;//each row will either be adj or clock event.
			while(true){
				if(a==TimeAdjustList.Length && c==ClockEventList.Length){
					break;//both indices are outside bounds of array, so we're done.  Also works for both of length 0.
				}
				row=new ODGridRow();
				//test to see if a timeadjust should be next
				if(c==ClockEventList.Length//if clockevents already maxed out or has length 0
					|| (a<TimeAdjustList.Length && TimeAdjustList[a].TimeEntry<ClockEventList[c].TimeDisplayed))//or timeadj < clockevent
				{
					isAdj=true;
					row.Tag=TimeAdjustList[a].Copy();
				}
				else{
					isAdj=false;
					row.Tag=ClockEventList[c].Copy();
				}
				//if((isAdj && a==0) || (!isAdj && c==0) ){//if this is the first row
					
				//}
				previousDate=curDate;
				if(isAdj){
					curDate=TimeAdjustList[a].TimeEntry.Date;
				}
				else{
					curDate=ClockEventList[c].TimeDisplayed.Date;
				}
				if(curDate==previousDate){
					row.Cells.Add("");
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(curDate.ToShortDateString());
					row.Cells.Add(curDate.DayOfWeek.ToString());
				}
				//altered--------------------------------------
				if(!isAdj && ClockEventList[c].TimeEntered!=ClockEventList[c].TimeDisplayed){
					alteredSpan=ClockEventList[c].TimeDisplayed-ClockEventList[c].TimeEntered;
					if(IsBreaks){
						row.Cells.Add(alteredSpan.TotalMinutes.ToString("n"));
					}
					else{
						row.Cells.Add(alteredSpan.TotalHours.ToString("n"));
					}
				}
				else{
					row.Cells.Add("");
				}
				//status--------------------------------------
				if(isAdj){
					row.Cells.Add(Lan.g(this,"Adjust"));
					row.ColorText=Color.Red;
				}
				else{
					row.Cells.Add(ClockEventList[c].ClockStatus.ToString());
				}
				//in/out------------------------------------------
				if(isAdj){
					row.Cells.Add("");
				}
				else{
					if(ClockEventList[c].ClockIn)
						row.Cells.Add(Lan.g(this,"In"));
					else
						row.Cells.Add(Lan.g(this,"Out"));
				}
				//time-----------------------------
				if(isAdj){
					row.Cells.Add(TimeAdjustList[a].TimeEntry.ToShortTimeString());
				}
				else{
					row.Cells.Add(ClockEventList[c].TimeDisplayed.ToShortTimeString());
				}
				//minutes or hours-------------------------------
				if(isAdj){
					if(TimeAdjustList[a].RegHours.TotalHours==0){
						row.Cells.Add("");
					}
					else{
						daySpan+=TimeAdjustList[a].RegHours;//might be negative
						weekSpan+=TimeAdjustList[a].RegHours;
						periodSpan+=TimeAdjustList[a].RegHours;
						row.Cells.Add(TimeAdjustList[a].RegHours.TotalHours.ToString("n"));
					}
				}
				else{
					//this tests to see if a time should be calculated
					//remember, it's opposite if checking breaks.
					if((!IsBreaks && !ClockEventList[c].ClockIn)//if regular hours and clocked out
						|| (IsBreaks && ClockEventList[c].ClockIn))//or break hours and clocked in
					{//then display the timespan of pairSpan
						if(c!=0){//unless there is not a previous entry
							pairSpan=ClockEventList[c].TimeDisplayed-ClockEventList[c-1].TimeDisplayed;
							if(IsBreaks){
								row.Cells.Add(pairSpan.TotalMinutes.ToString("n"));
							}
							else{
								row.Cells.Add(pairSpan.TotalHours.ToString("n"));
							}
							daySpan+=pairSpan;
							weekSpan+=pairSpan;
							periodSpan+=pairSpan;
						}
					}
					else{
						row.Cells.Add("");
					}
				}
				//Overtime------------------------------
				if(isAdj && TimeAdjustList[a].OTimeHours.TotalHours!=0){
					otspan+=TimeAdjustList[a].OTimeHours;
					row.Cells.Add(TimeAdjustList[a].OTimeHours.TotalHours.ToString("n"));
				}
				else{
					row.Cells.Add("");
				}
				//Daily-----------------------------------
				if(isAdj){
					//if(TimeAdjustList[a].RegHours.TotalHours==0){
						row.Cells.Add("");
					//}
					//else{
					//	row.Cells.Add(daySpan.TotalHours.ToString("n"));
					//}
				}
				else{
					//if this is the last clockevent entry for a given date
					if(c==ClockEventList.Length-1 || ClockEventList[c+1].TimeDisplayed.Date!=ClockEventList[c].TimeDisplayed.Date){
						if(IsBreaks){
							if(!ClockEventList[c].ClockIn){//if they have not clocked back in yet from break
								//display the timespan of pairSpan using current time as the other number.
								pairSpan=DateTime.Now-ClockEventList[c].TimeDisplayed+TimeDelta;
								row.Cells.Add(pairSpan.TotalMinutes.ToString("n"));
								daySpan+=pairSpan;
							}
							else{
								row.Cells.Add(daySpan.TotalMinutes.ToString("n"));
							}
						}
						else{
							row.Cells.Add(daySpan.TotalHours.ToString("n"));
						}
						daySpan=new TimeSpan(0);
					}
					else{
						row.Cells.Add("");
					}
				}
				//Weekly-------------------------------------
				if(IsBreaks){
					row.Cells.Add("");
				}
				else if(isAdj){
					row.Cells.Add("");//must leave blank, or it will screw up the automation
					//This next part fixes a specific situation in which there is an adjustment directly after the weekly total.
					//The adjustment should not be added to the following week.
					if(c>0//if there is at least one previous clockevent on this sheet
						&& cal.GetWeekOfYear(TimeAdjustList[a].TimeEntry.Date,rule,DayOfWeek.Sunday)
						== cal.GetWeekOfYear(ClockEventList[c-1].TimeDisplayed.Date,rule,DayOfWeek.Sunday))
					{
						weekSpan=new TimeSpan(0);//reset the weekly total again.
					}
				}
				else if(c==ClockEventList.Length-1 //if this is the last clockevent entry for a given week
					|| cal.GetWeekOfYear(ClockEventList[c+1].TimeDisplayed.Date,rule,DayOfWeek.Sunday)
					!= cal.GetWeekOfYear(ClockEventList[c].TimeDisplayed.Date,rule,DayOfWeek.Sunday))
				{
					row.Cells.Add(weekSpan.TotalHours.ToString("n"));
					weekSpan=new TimeSpan(0);
				}
				else {
					row.Cells.Add("");
				}
				//Note-----------------------------------------
				if(isAdj){
					row.Cells.Add(TimeAdjustList[a].Note);
				}
				else{
					row.Cells.Add(ClockEventList[c].Note);
				}
				gridMain.Rows.Add(row);
				if(isAdj){
					a++;
				}
				else{
					c++;
				}
			}
			gridMain.EndUpdate();
			if(IsBreaks){
				textTotal.Text="";
			}
			else{
				textTotal.Text=periodSpan.TotalHours.ToString("n");
				textOvertime.Text=otspan.TotalHours.ToString("n");
			}
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			timer1.Enabled=false;
			if(gridMain.Rows[e.Row].Tag.GetType()==typeof(TimeAdjust)) {
				if(!Security.IsAuthorized(Permissions.TimecardsEditAll)) {
					timer1.Enabled=true;
					return;
				}
				TimeAdjust adjust=(TimeAdjust)gridMain.Rows[e.Row].Tag;
				FormTimeAdjustEdit FormT=new FormTimeAdjustEdit(adjust);
				FormT.ShowDialog();
			}
			else {
				ClockEvent ce=(ClockEvent)gridMain.Rows[e.Row].Tag;
				FormClockEventEdit FormCEE=new FormClockEventEdit(ce);
				FormCEE.ShowDialog();
			}
			FillMain(true);
			timer1.Enabled=true;
		}

		private void butAdj_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.TimecardsEditAll)) {
				return;
			}
			TimeAdjust adjust=new TimeAdjust();
			adjust.EmployeeNum=Employees.Cur.EmployeeNum;
			DateTime dateStop=PIn.PDate(textDateStop.Text);
			if(DateTime.Today<=dateStop && DateTime.Today>=PIn.PDate(textDateStart.Text)) {
				adjust.TimeEntry=DateTime.Now;
			}
			else {
				adjust.TimeEntry=new DateTime(dateStop.Year,dateStop.Month,dateStop.Day,
					DateTime.Now.Hour,DateTime.Now.Minute,DateTime.Now.Second);
			}
			FormTimeAdjustEdit FormT=new FormTimeAdjustEdit(adjust);
			FormT.IsNew=true;
			FormT.ShowDialog();
			if(FormT.DialogResult==DialogResult.Cancel) {
				return;
			}
			FillMain(true);
		}

		private void butCompute_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.TimecardsEditAll)) {
				return;
			}
			//first, delete all existing overtime entries
			for(int i=0;i<TimeAdjustList.Length;i++) {
				if(TimeAdjustList[i].OTimeHours.TotalHours!=0) {
					TimeAdjustList[i].Delete();
				}
			}
			//then, fill grid
			FillMain(true);
			//loop through all week entries looking for numbers over 40
			double hours;
			ClockEvent foundClock=null;
			for(int i=0;i<gridMain.Rows.Count;i++) {
				if(gridMain.Rows[i].Cells[9].Text=="") {
					continue;
				}
				hours=PIn.PDouble(gridMain.Rows[i].Cells[9].Text);
				if(hours<=40) {
					continue;
				}
				foundClock=(ClockEvent)gridMain.Rows[i].Tag;
				TimeAdjust adjust=new TimeAdjust();
				adjust.EmployeeNum=Employees.Cur.EmployeeNum;
				adjust.TimeEntry=foundClock.TimeDisplayed.AddMinutes(5);
				adjust.OTimeHours=TimeSpan.FromHours(hours-40);
				adjust.RegHours=-adjust.OTimeHours;
				adjust.Insert();
			}
			FillMain(true);
		}

		private void butPrint_Click(object sender,EventArgs e) {
			linesPrinted=0;
			pd=new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
			pd.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			pd.OriginAtMargins=true;
#if DEBUG
			printPreview=new PrintPreview(PrintSituation.Default,pd,1);
			printPreview.ShowDialog();
#else
				try {
					if(Printers.SetPrinter(pd,PrintSituation.Default)) {
						pd.Print();
					}
				}
				catch {
					MessageBox.Show(Lan.g(this,"Printer not available"));
				}
#endif
		}

		///<summary>raised for each page to be printed.  One page per appointment.</summary>
		private void pd_PrintPage(object sender,PrintPageEventArgs e) {
			Graphics g=e.Graphics;
			float yPos=75;
			float xPos=55;
			string str;
			Font font=new Font(FontFamily.GenericSansSerif,8);
			Font fontTitle=new Font(FontFamily.GenericSansSerif,11,FontStyle.Bold);
			Font fontHeader=new Font(FontFamily.GenericSansSerif,8,FontStyle.Bold);
			SolidBrush brush=new SolidBrush(Color.Black);
			Pen pen=new Pen(Color.Black);
			//Title
			str=Employees.Cur.FName+" "+Employees.Cur.LName;
			g.DrawString(str,fontTitle,brush,xPos,yPos);
			yPos+=30;
			//define columns
			int[] colW=new int[11];
			colW[0]=70;//date
			colW[1]=70;//weekday
			colW[2]=50;//altered
			colW[3]=50;//status
			colW[4]=60;//in/out
			colW[5]=60;//time
			colW[6]=50;//minutes/hours
			colW[7]=55;//overtime
			colW[8]=50;//daily
			colW[9]=50;//weekly
			colW[10]=200;//note
			int[] colPos=new int[colW.Length+1];
			colPos[0]=55;
			for(int i=1;i<colPos.Length;i++) {
				colPos[i]=colPos[i-1]+colW[i-1];
			}
			string[] ColCaption=new string[11];
			ColCaption[0]=Lan.g(this,"Date");
			ColCaption[1]=Lan.g(this,"Weekday");
			ColCaption[2]=Lan.g(this,"Altered");
			ColCaption[3]=Lan.g(this,"Status");
			ColCaption[4]=Lan.g(this,"In/Out");
			ColCaption[5]=Lan.g(this,"Time");
			ColCaption[6]=Lan.g(this,"Hours");//or minutes
			ColCaption[7]=Lan.g(this,"Overtime");
			ColCaption[8]=Lan.g(this,"Daily");
			ColCaption[9]=Lan.g(this,"Weekly");
			ColCaption[10]=Lan.g(this,"Note");
			//column headers-----------------------------------------------------------------------------------------
			e.Graphics.FillRectangle(Brushes.LightGray,colPos[0],yPos,colPos[colPos.Length-1]-colPos[0],18);
			e.Graphics.DrawRectangle(pen,colPos[0],yPos,colPos[colPos.Length-1]-colPos[0],18);
			for(int i=1;i<colPos.Length;i++) {
				e.Graphics.DrawLine(new Pen(Color.Black),colPos[i],yPos,colPos[i],yPos+18);
			}
			//Prints the Column Titles
			for(int i=0;i<ColCaption.Length;i++) {
				e.Graphics.DrawString(ColCaption[i],fontHeader,brush,colPos[i]+2,yPos+1);
			}
			yPos+=18;
			while(yPos < e.PageBounds.Height-75-50-32-16 && linesPrinted < gridMain.Rows.Count) {
				for(int i=0;i<colPos.Length-1;i++) {
					e.Graphics.DrawString(gridMain.Rows[linesPrinted].Cells[i].Text,font,brush
						,new RectangleF(colPos[i]+2,yPos,colPos[i+1]-colPos[i]-5,font.GetHeight(e.Graphics)));
				}
				//Column lines		
				for(int i=0;i<colPos.Length;i++) {
					e.Graphics.DrawLine(Pens.Gray,colPos[i],yPos+16,colPos[i],yPos);
				}
				linesPrinted++;
				yPos+=16;
				e.Graphics.DrawLine(new Pen(Color.Gray),colPos[0],yPos,colPos[colPos.Length-1],yPos);
			}
			//bottom line
			//e.Graphics.DrawLine(new Pen(Color.Gray),colPos[0],yPos,colPos[colPos.Length-1],yPos);
			//totals will print on every page for simplicity
			yPos+=10;
			g.DrawString(Lan.g(this,"Regular Time")+": "+textTotal.Text,fontHeader,brush,xPos,yPos);
			yPos+=16;
			g.DrawString(Lan.g(this,"Overtime")+": "+textOvertime.Text,fontHeader,brush,xPos,yPos);
			if(linesPrinted==gridMain.Rows.Count) {
				e.HasMorePages=false;
			}
			else {
				e.HasMorePages=true;
			}
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void timer1_Tick(object sender, System.EventArgs e) {
			if(IsBreaks){
				FillMain(false);
			}
		}

	

		

		

		

		

		

		

		

		

		


	}
}





















