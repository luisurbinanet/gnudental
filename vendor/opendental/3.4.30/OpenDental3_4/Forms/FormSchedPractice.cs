using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormSchedPractice : System.Windows.Forms.Form{
		private OpenDental.ContrCalendar Calendar2;
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butClose;
    Color ClosedColor;//will be Def "Closed Practice" Color
    Color HolidayColor;//will be Def "Holiday" Color
    Color OpenColor; //will be Def "Open" Color

		///<summary></summary>
		public FormSchedPractice(){
			InitializeComponent();
      Calendar2.ChangeMonth +=new OpenDental.ContrCalendar.EventHandler(Calendar2_ChangeMonth);  
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if(disposing){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
			this.Calendar2 = new OpenDental.ContrCalendar();
			this.butClose = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// Calendar2
			// 
			this.Calendar2.BackColor = System.Drawing.SystemColors.Control;
			this.Calendar2.Location = new System.Drawing.Point(21, 14);
			this.Calendar2.Name = "Calendar2";
			this.Calendar2.SelectedDate = new System.DateTime(2004, 2, 22, 0, 0, 0, 0);
			this.Calendar2.Size = new System.Drawing.Size(793, 664);
			this.Calendar2.TabIndex = 0;
			this.Calendar2.Click += new System.EventHandler(this.Calendar2_Click);
			this.Calendar2.DoubleClick += new System.EventHandler(this.Calendar2_DoubleClick);
			// 
			// butClose
			// 
			this.butClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClose.Location = new System.Drawing.Point(842, 654);
			this.butClose.Name = "butClose";
			this.butClose.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.butClose.Size = new System.Drawing.Size(75, 26);
			this.butClose.TabIndex = 1;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormSchedPractice
			// 
			this.AcceptButton = this.butClose;
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(934, 692);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.Calendar2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSchedPractice";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Practice Schedule";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormSchedPractice_Closing);
			this.Load += new System.EventHandler(this.FormSchedPractice_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormSchedPractice_Load(object sender, System.EventArgs e) {
			if(!UserPermissions.CheckUserPassword("Practice Schedule")){
				DialogResult=DialogResult.Cancel;
				return;
			}
      Schedules.CurDate=Calendar2.SelectedDate;
      SchedDefaults.Refresh();
			OpenColor=Defs.Long[(int)DefCat.AppointmentColors][0].ItemColor;
			ClosedColor=Defs.Long[(int)DefCat.AppointmentColors][1].ItemColor;
			HolidayColor=Defs.Long[(int)DefCat.AppointmentColors][4].ItemColor;
      GetScheduleData(); 
   }

    private void Calendar2_ChangeMonth(object sender, System.EventArgs e){
      Schedules.CurDate=Calendar2.SelectedDate;
      GetScheduleData();  
    }

    private void GetScheduleData(){
      Schedules.RefreshMonth();
			Schedules.RefreshDay(Schedules.CurDate);//why is this here?
      Calendar2.ResetList();
      bool HasSchedDefault;
      bool HasScheduleData; 
      for(int i=1;i<Calendar2.List.Length;i++){
        HasSchedDefault=false;
        HasScheduleData=false;

        for(int j=0;j<Schedules.ListMonth.Length;j++){
          if(Calendar2.List[i].Date.ToShortDateString() == Schedules.ListMonth[j].SchedDate.ToShortDateString()){
            if(Schedules.ListMonth[j].Status == SchedStatus.Open){ 
              Calendar2.AddText(i,Schedules.ListMonth[j].StartTime.ToShortTimeString()+" - "+Schedules.ListMonth[j].StopTime.ToShortTimeString());
              Calendar2.List[i].color=OpenColor; 
              if(Schedules.ListMonth[j].Note==""){
              }
              else{
                Calendar2.AddText(i,Schedules.ListMonth[j].Note);
              }              
            }
            else if(Schedules.ListMonth[j].Status == SchedStatus.Holiday){
              if(Schedules.ListMonth[j].Note==""){                
              }
              else{  
                Calendar2.AddText(i,Schedules.ListMonth[j].Note);
              }
              Calendar2.ChangeColor(i,HolidayColor);
            }
            else{
              if(Schedules.ListMonth[j].Note==""){            
              }
              else{ 
                Calendar2.AddText(i,Schedules.ListMonth[j].Note);                
              }
              Calendar2.ChangeColor(i,ClosedColor);              
            }
            HasScheduleData=true;            
          }       
			  }      
        if(!HasScheduleData){
					for(int j=0;j<SchedDefaults.List.Length;j++){
						if((int)Calendar2.List[i].Date.DayOfWeek==SchedDefaults.List[j].DayOfWeek){
							Calendar2.AddText(i,SchedDefaults.List[j].StartTime.ToShortTimeString()+" - "
								+SchedDefaults.List[j].StopTime.ToShortTimeString());
							HasSchedDefault=true;
              Calendar2.ChangeColor(i,OpenColor); 
						}
					}
					if(!HasSchedDefault){
						Calendar2.List[i].color=ClosedColor;
					} 
        }
      }
    }

		private void Calendar2_Click(object sender, System.EventArgs e) {
      Schedules.CurDate=Calendar2.SelectedDate;	
		}

		private void Calendar2_DoubleClick(object sender, System.EventArgs e) {
			FormScheduleDay FormSD2=new FormScheduleDay();
      FormSD2.ShowDialog();
      GetScheduleData();
			Graphics grfx=this.CreateGraphics();
      Calendar2.DrawDays(grfx);
			Calendar2.Invalidate();
			grfx.Dispose();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormSchedPractice_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			//The daily schedules is refreshed everytime the Appointment screen is refreshed. Not with LocalData
			SecurityLogs.MakeLogEntry("Practice Schedule","Altered Practice Schedule");
		}

	}
}