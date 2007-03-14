using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormSchedPractice : System.Windows.Forms.Form{
		private OpenDental.ContrCalendar Calendar2;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button butClose;
    Color ClosedColor;//will be Def "Closed Practice" Color
    Color HolidayColor;//will be Def "Holiday" Color
    Color OpenColor; //will be Def "Open" Color

		public FormSchedPractice(){
			InitializeComponent();
      Calendar2.ChangeMonth +=new OpenDental.ContrCalendar.EventHandler(Calendar2_ChangeMonth);  
			Lan.C("All", new System.Windows.Forms.Control[] {
				butClose,
				this
			});
		}

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
			this.butClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Calendar2
			// 
			this.Calendar2.BackColor = System.Drawing.SystemColors.Control;
			this.Calendar2.Location = new System.Drawing.Point(21, 14);
			this.Calendar2.Name = "Calendar2";
			this.Calendar2.SelectedDate = new System.DateTime(2003, 12, 1, 0, 0, 0, 0);
			this.Calendar2.Size = new System.Drawing.Size(793, 664);
			this.Calendar2.TabIndex = 0;
			this.Calendar2.Click += new System.EventHandler(this.Calendar2_Click);
			this.Calendar2.DoubleClick += new System.EventHandler(this.Calendar2_DoubleClick);
			// 
			// butClose
			// 
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(842, 654);
			this.butClose.Name = "butClose";
			this.butClose.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.butClose.TabIndex = 1;
			this.butClose.Text = "OK";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormSchedPractice
			// 
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(934, 692);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.Calendar2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSchedPractice";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Practice Schedule";
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

        for(int j=0;j<Schedules.List.Length;j++){
          if(Calendar2.List[i].Date.ToShortDateString() == Schedules.List[j].SchedDate.ToShortDateString()){
            if(Schedules.List[j].Status == SchedStatus.Open){ 
              Calendar2.AddText(i,Schedules.List[j].StartTime.ToShortTimeString()+" - "+Schedules.List[j].StopTime.ToShortTimeString());
              Calendar2.List[i].color=OpenColor; 
              if(Schedules.List[j].Note==""){
              }
              else{
                Calendar2.AddText(i,Schedules.List[j].Note);
              }              
            }
            else if(Schedules.List[j].Status == SchedStatus.Holiday){
              if(Schedules.List[j].Note==""){                
              }
              else{  
                Calendar2.AddText(i,Schedules.List[j].Note);
              }
              Calendar2.ChangeColor(i,HolidayColor);
            }
            else{
              if(Schedules.List[j].Note==""){            
              }
              else{ 
                Calendar2.AddText(i,Schedules.List[j].Note);                
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
      Calendar2.DrawDays();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
			SecurityLogs.MakeLogEntry("Practice Schedule","Altered Practice Schedule");
			DialogResult=DialogResult.OK;
		}

	}
}