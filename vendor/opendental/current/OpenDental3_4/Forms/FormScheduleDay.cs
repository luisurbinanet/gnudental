using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormScheduleDay : System.Windows.Forms.Form{
		private OpenDental.UI.Button butDefault;
		private System.Windows.Forms.ListBox listTimeBlocks;
		private OpenDental.UI.Button butAddTime;
		private OpenDental.UI.Button butCloseOffice;
		private OpenDental.UI.Button butHoliday;
		private System.Windows.Forms.Label label1;
    private ArrayList ALdefaults;
		private System.Windows.Forms.Label labelDefault;
		private OpenDental.UI.Button butClose;  

		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormScheduleDay(){
			InitializeComponent();
			Lan.F(this);
		}

		///<summary></summary>
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
			this.butClose = new OpenDental.UI.Button();
			this.butDefault = new OpenDental.UI.Button();
			this.butAddTime = new OpenDental.UI.Button();
			this.butCloseOffice = new OpenDental.UI.Button();
			this.butHoliday = new OpenDental.UI.Button();
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
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(265, 214);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 26);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butDefault
			// 
			this.butDefault.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butDefault.Autosize = true;
			this.butDefault.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDefault.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDefault.Location = new System.Drawing.Point(248, 48);
			this.butDefault.Name = "butDefault";
			this.butDefault.Size = new System.Drawing.Size(92, 26);
			this.butDefault.TabIndex = 3;
			this.butDefault.Text = "Set To &Default";
			this.butDefault.Click += new System.EventHandler(this.butDefault_Click);
			// 
			// butAddTime
			// 
			this.butAddTime.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAddTime.Autosize = true;
			this.butAddTime.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddTime.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddTime.Location = new System.Drawing.Point(244, 80);
			this.butAddTime.Name = "butAddTime";
			this.butAddTime.Size = new System.Drawing.Size(96, 26);
			this.butAddTime.TabIndex = 4;
			this.butAddTime.Text = "&Add Time Block";
			this.butAddTime.Click += new System.EventHandler(this.butAddTime_Click);
			// 
			// butCloseOffice
			// 
			this.butCloseOffice.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCloseOffice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butCloseOffice.Autosize = true;
			this.butCloseOffice.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCloseOffice.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCloseOffice.Location = new System.Drawing.Point(248, 112);
			this.butCloseOffice.Name = "butCloseOffice";
			this.butCloseOffice.Size = new System.Drawing.Size(92, 26);
			this.butCloseOffice.TabIndex = 5;
			this.butCloseOffice.Text = "C&lose Office";
			this.butCloseOffice.Click += new System.EventHandler(this.butCloseOffice_Click);
			// 
			// butHoliday
			// 
			this.butHoliday.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butHoliday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butHoliday.Autosize = true;
			this.butHoliday.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butHoliday.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butHoliday.Location = new System.Drawing.Point(248, 144);
			this.butHoliday.Name = "butHoliday";
			this.butHoliday.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.butHoliday.Size = new System.Drawing.Size(92, 26);
			this.butHoliday.TabIndex = 7;
			this.butHoliday.Text = "Set as &Holiday";
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
			this.AcceptButton = this.butClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(352, 252);
			this.Controls.Add(this.labelDefault);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butHoliday);
			this.Controls.Add(this.butCloseOffice);
			this.Controls.Add(this.butAddTime);
			this.Controls.Add(this.butDefault);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.listTimeBlocks);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormScheduleDay";
			this.ShowInTaskbar = false;
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
      if(Schedules.ListDay.Length==0){
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
        if(Schedules.ListDay.Length==1 && Schedules.ListDay[0].Status==SchedStatus.Closed){
          listTimeBlocks.Items.Add("Office Closed "+Schedules.ListDay[0].Note);
        }
        else if(Schedules.ListDay.Length==1 && Schedules.ListDay[0].Status==SchedStatus.Holiday){
          listTimeBlocks.Items.Add("Holiday: "+Schedules.ListDay[0].Note);
        } 
        else{  
					for(int i=0;i<Schedules.ListDay.Length;i++){
						listTimeBlocks.Items.Add(Schedules.ListDay[i].StartTime.ToShortTimeString()+" - "
							+Schedules.ListDay[i].StopTime.ToShortTimeString());
					}
        } 
      } 
    }//FillList

		private void butDefault_Click(object sender, System.EventArgs e) {
		  SetAllDefault();
		}

		private void SetAllDefault(){
			for(int i=0;i<Schedules.ListDay.Length;i++){
				Schedules.Cur=Schedules.ListDay[i];
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
      if(Schedules.ListDay.Length==1 
				&& Schedules.ListDay[0].Status==SchedStatus.Closed){
        MessageBox.Show(Lan.g(this,"Day is already Closed."));         
        return;
      }
      if(Schedules.ListDay.Length > 0){
				for(int i=0;i<Schedules.ListDay.Length;i++){
					Schedules.Cur=Schedules.ListDay[i];
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
			if(Schedules.ListDay.Length==1 
				&& Schedules.ListDay[0].Status==SchedStatus.Holiday){
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
			if(listTimeBlocks.SelectedIndex==-1){
				return;
			}
      ConvertFromDefault();
      Schedules.Cur=Schedules.ListDay[listTimeBlocks.SelectedIndex];
			FormScheduleDayEdit FormSDE2=new FormScheduleDayEdit();
      FormSDE2.ShowDialog();
      FillList();
    }

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

	}
}







