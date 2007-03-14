using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormScheduleDay : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butDefault;
		private System.Windows.Forms.ListBox listTimeBlocks;
		private System.Windows.Forms.Button butAddTime;
		private System.Windows.Forms.Button butCloseOffice;
		private System.Windows.Forms.Button butHoliday;
		private System.Windows.Forms.Label label1;
    private ArrayList ALdefaults;
		private System.Windows.Forms.Label labelDefault;  

		private System.ComponentModel.Container components = null;

		public FormScheduleDay(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				label1,
				labelDefault,
				butAddTime,
				butCloseOffice,
				butHoliday,
				butDefault,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
			});
		}

		protected override void Dispose(bool disposing){
			if(disposing){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.listTimeBlocks = new System.Windows.Forms.ListBox();
			this.butOK = new System.Windows.Forms.Button();
			this.butDefault = new System.Windows.Forms.Button();
			this.butAddTime = new System.Windows.Forms.Button();
			this.butCloseOffice = new System.Windows.Forms.Button();
			this.butHoliday = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.labelDefault = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// listTimeBlocks
			// 
			this.listTimeBlocks.Location = new System.Drawing.Point(14, 51);
			this.listTimeBlocks.Name = "listTimeBlocks";
			this.listTimeBlocks.Size = new System.Drawing.Size(192, 186);
			this.listTimeBlocks.TabIndex = 0;
			this.listTimeBlocks.DoubleClick += new System.EventHandler(this.listTimeBlocks_DoubleClick);
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(260, 214);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 2;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butDefault
			// 
			this.butDefault.Location = new System.Drawing.Point(248, 48);
			this.butDefault.Name = "butDefault";
			this.butDefault.Size = new System.Drawing.Size(92, 23);
			this.butDefault.TabIndex = 3;
			this.butDefault.Text = "Set To Default";
			this.butDefault.Click += new System.EventHandler(this.butDefault_Click);
			// 
			// butAddTime
			// 
			this.butAddTime.Location = new System.Drawing.Point(248, 80);
			this.butAddTime.Name = "butAddTime";
			this.butAddTime.Size = new System.Drawing.Size(92, 23);
			this.butAddTime.TabIndex = 4;
			this.butAddTime.Text = "Add Time Block";
			this.butAddTime.Click += new System.EventHandler(this.butAddTime_Click);
			// 
			// butCloseOffice
			// 
			this.butCloseOffice.Location = new System.Drawing.Point(248, 112);
			this.butCloseOffice.Name = "butCloseOffice";
			this.butCloseOffice.Size = new System.Drawing.Size(92, 23);
			this.butCloseOffice.TabIndex = 5;
			this.butCloseOffice.Text = "Close Office";
			this.butCloseOffice.Click += new System.EventHandler(this.butCloseOffice_Click);
			// 
			// butHoliday
			// 
			this.butHoliday.Location = new System.Drawing.Point(248, 144);
			this.butHoliday.Name = "butHoliday";
			this.butHoliday.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.butHoliday.Size = new System.Drawing.Size(92, 23);
			this.butHoliday.TabIndex = 7;
			this.butHoliday.Text = "Set as Holiday";
			this.butHoliday.Click += new System.EventHandler(this.butHoliday_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 16);
			this.label1.TabIndex = 8;
			this.label1.Text = "Time Blocks:";
			// 
			// labelDefault
			// 
			this.labelDefault.Location = new System.Drawing.Point(130, 30);
			this.labelDefault.Name = "labelDefault";
			this.labelDefault.Size = new System.Drawing.Size(66, 18);
			this.labelDefault.TabIndex = 9;
			this.labelDefault.Text = "(default)";
			this.labelDefault.Visible = false;
			// 
			// FormScheduleDay
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(352, 252);
			this.ControlBox = false;
			this.Controls.Add(this.labelDefault);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butHoliday);
			this.Controls.Add(this.butCloseOffice);
			this.Controls.Add(this.butAddTime);
			this.Controls.Add(this.butDefault);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.listTimeBlocks);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormScheduleDay";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Day";
			this.Load += new System.EventHandler(this.FormScheduleDay_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormScheduleDay_Load(object sender, System.EventArgs e) {
      FillList();      		
		}

    private void FillList(){
      Schedules.RefreshDay(Schedules.CurDate); 
      SchedDefaults.Refresh();
      listTimeBlocks.Items.Clear(); 
      ALdefaults=new ArrayList();
      labelDefault.Visible=false; 
      if(Schedules.DayList.Length==0){
        for(int i=0;i<SchedDefaults.List.Length;i++){
          if((int)Schedules.CurDate.DayOfWeek==SchedDefaults.List[i].DayOfWeek){
            ALdefaults.Add(SchedDefaults.List[i]); 
            listTimeBlocks.Items.Add(SchedDefaults.List[i].StartTime.ToShortTimeString()+" - "
              +SchedDefaults.List[i].StopTime.ToShortTimeString());
          }  
        }
        labelDefault.Visible=true;     
      }
      else{  
        if(Schedules.DayList.Length==1 && Schedules.DayList[0].Status==SchedStatus.Closed){
          listTimeBlocks.Items.Add("Office Closed "+Schedules.DayList[0].Note);
        }
        else if(Schedules.DayList.Length==1 && Schedules.DayList[0].Status==SchedStatus.Holiday){
          listTimeBlocks.Items.Add("Holiday: "+Schedules.DayList[0].Note);
        } 
        else{  
					for(int i=0;i<Schedules.DayList.Length;i++){
						listTimeBlocks.Items.Add(Schedules.DayList[i].StartTime.ToShortTimeString()+" - "
							+Schedules.DayList[i].StopTime.ToShortTimeString());
					}
        } 
      } 
    }//FillList

		private void butDefault_Click(object sender, System.EventArgs e) {
		  SetAllDefault();
		}

		private void SetAllDefault(){
			for(int i=0;i<Schedules.DayList.Length;i++){
				Schedules.Cur=Schedules.DayList[i];
				Schedules.DeleteCur();
			}
			FillList();
		}

		private void ConvertFromDefault(){
			if(!labelDefault.Visible){
				return;//already not default
      } 
			int selected=listTimeBlocks.SelectedIndex;
			for(int i=0;i<listTimeBlocks.Items.Count;i++){
				Schedules.Cur=new Schedule();
				Schedules.Cur.Status=SchedStatus.Open;
				Schedules.Cur.SchedDate=Schedules.CurDate;
				Schedules.Cur.StartTime=((SchedDefault)(ALdefaults[i])).StartTime;
				Schedules.Cur.StopTime=((SchedDefault)(ALdefaults[i])).StopTime; 
				Schedules.InsertCur();            
			}
			FillList();
			listTimeBlocks.SelectedIndex=selected;
		}

		private void butCloseOffice_Click(object sender, System.EventArgs e) {
      if(Schedules.DayList.Length==1 
				&& Schedules.DayList[0].Status==SchedStatus.Closed){
        MessageBox.Show(Lan.g(this,"Day is already Closed."));         
        return;
      }
      if(Schedules.DayList.Length > 0){
				for(int i=0;i<Schedules.DayList.Length;i++){
					Schedules.Cur=Schedules.DayList[i];
					Schedules.DeleteCur();
				}
      } 
		  Schedules.Cur=new Schedule();
      Schedules.Cur.SchedDate=Schedules.CurDate;
      Schedules.Cur.Status=SchedStatus.Closed;
		  FormScheduleDayEdit FormSDE2=new FormScheduleDayEdit();
      FormSDE2.IsNew=true;
      FormSDE2.ShowDialog();
      FillList();
		}

		private void butHoliday_Click(object sender, System.EventArgs e) {
			if(Schedules.DayList.Length==1 
				&& Schedules.DayList[0].Status==SchedStatus.Holiday){
        MessageBox.Show(Lan.g(this,"Day is already a Holiday."));         
        return;
      }
      SetAllDefault();
		  Schedules.Cur=new Schedule();
      Schedules.Cur.SchedDate=Schedules.CurDate;
      Schedules.Cur.Status=SchedStatus.Holiday;
		  FormScheduleDayEdit FormSDE2=new FormScheduleDayEdit();
      FormSDE2.IsNew=true;
      FormSDE2.ShowDialog();
      FillList();		
		}

		private void butAddTime_Click(object sender, System.EventArgs e) {
			ConvertFromDefault();
      Schedules.Cur=new Schedule();
      Schedules.Cur.SchedDate=Schedules.CurDate;
      Schedules.Cur.Status=SchedStatus.Open;
		  FormScheduleDayEdit FormSDE2=new FormScheduleDayEdit();
      FormSDE2.IsNew=true;
      FormSDE2.ShowDialog();
      labelDefault.Visible=false; 
      FillList();
		}

		private void listTimeBlocks_DoubleClick(object sender, System.EventArgs e) {
      ConvertFromDefault();
      Schedules.Cur=Schedules.DayList[listTimeBlocks.SelectedIndex];
			FormScheduleDayEdit FormSDE2=new FormScheduleDayEdit();
      FormSDE2.ShowDialog();
      FillList();
    }

		private void butOK_Click(object sender, System.EventArgs e) {
      DialogResult=DialogResult.OK;
		}

	}
}
