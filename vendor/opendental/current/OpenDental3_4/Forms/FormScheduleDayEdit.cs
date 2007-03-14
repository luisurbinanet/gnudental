using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormScheduleDayEdit : System.Windows.Forms.Form	{
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.TextBox textStop;
		private System.Windows.Forms.TextBox textStart;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox listStatus;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.Label label4;
		private OpenDental.UI.Button butDelete;
		///<summary></summary>
    public bool IsNew;

		///<summary></summary>
		public FormScheduleDayEdit(){
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormScheduleDayEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.textStop = new System.Windows.Forms.TextBox();
			this.textStart = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.listStatus = new System.Windows.Forms.ListBox();
			this.textNote = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(220, 216);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 14;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(220, 182);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 12;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textStop
			// 
			this.textStop.Location = new System.Drawing.Point(74, 38);
			this.textStop.Name = "textStop";
			this.textStop.Size = new System.Drawing.Size(220, 20);
			this.textStop.TabIndex = 8;
			this.textStop.Text = "";
			// 
			// textStart
			// 
			this.textStart.Location = new System.Drawing.Point(74, 10);
			this.textStart.Name = "textStart";
			this.textStart.Size = new System.Drawing.Size(220, 20);
			this.textStart.TabIndex = 6;
			this.textStart.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(66, 16);
			this.label2.TabIndex = 9;
			this.label2.Text = "Stop Time";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(4, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 16);
			this.label1.TabIndex = 7;
			this.label1.Text = "Start Time";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 106);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 16);
			this.label3.TabIndex = 10;
			this.label3.Text = "Status";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listStatus
			// 
			this.listStatus.Enabled = false;
			this.listStatus.Location = new System.Drawing.Point(74, 106);
			this.listStatus.Name = "listStatus";
			this.listStatus.Size = new System.Drawing.Size(102, 56);
			this.listStatus.TabIndex = 11;
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(74, 66);
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(220, 20);
			this.textNote.TabIndex = 15;
			this.textNote.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(10, 70);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 16);
			this.label4.TabIndex = 16;
			this.label4.Text = "Note";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(26, 216);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(84, 26);
			this.butDelete.TabIndex = 17;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormScheduleDayEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(302, 248);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.textStop);
			this.Controls.Add(this.textStart);
			this.Controls.Add(this.listStatus);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormScheduleDayEdit";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Time Block";
			this.Load += new System.EventHandler(this.FormScheduleDayEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormScheduleDayEdit_Load(object sender, System.EventArgs e) {
			string[] enumSchedStatus=Enum.GetNames(typeof(SchedStatus)); //*Ann
			for(int i=0;i<enumSchedStatus.Length;i++){ //*Ann
				listStatus.Items.Add(Lan.g("enumSchedStatus",enumSchedStatus[i])); //*Ann
			}
      //foreach(string s in Enum.GetNames(typeof(SchedStatus))){
      //  listStatus.Items.Add(s);
      //} 
      if(IsNew){
        this.Text=Lan.g(this,"Add Time Block");
        if(Schedules.Cur.Status==SchedStatus.Open){
          listStatus.SelectedIndex=(int)Schedules.Cur.Status;
          textStart.Text=Schedules.Cur.StartTime.ToShortTimeString();
          textStop.Text=Schedules.Cur.StopTime.ToShortTimeString(); 
          textStart.Select(); 
        }
        else{
          textStop.Enabled=false;
          textStart.Enabled=false;
          listStatus.SelectedIndex=(int)Schedules.Cur.Status;
          textNote.Select();
        }     
      }
      else{
        if(Schedules.Cur.Status==SchedStatus.Open){
          listStatus.SelectedIndex=(int)Schedules.Cur.Status;
          textStart.Text=Schedules.Cur.StartTime.ToShortTimeString();
          textStop.Text=Schedules.Cur.StopTime.ToShortTimeString();
          textNote.Text=Schedules.Cur.Note; 
          textStart.Select();
        }
        else{
          textStop.Enabled=false;
          textStart.Enabled=false;
          listStatus.SelectedIndex=(int)Schedules.Cur.Status;
          textNote.Text=Schedules.Cur.Note; 
          textNote.Select();
        }  
      }
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"Delete TimeBlock?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
			  return;   
			}
      if(IsNew){
        DialogResult=DialogResult.Cancel; 
      }
      else{ 
        Schedules.DeleteCur();	
      }
      DialogResult=DialogResult.Cancel;
		}

    private void butOK_Click(object sender, System.EventArgs e) { 
      Schedules.Cur.Note=textNote.Text;  
      if(textStart.Enabled==true){   
			  try{
					Schedules.Cur.StartTime=DateTime.Parse(textStart.Text);
					Schedules.Cur.StopTime=DateTime.Parse(textStop.Text);
					Schedules.Cur.Note=textNote.Text;
				}
				catch{
					MessageBox.Show(Lan.g(this,"Incorrect time format"));
					return;
				}
				if(Schedules.Cur.StartTime.TimeOfDay.CompareTo(Schedules.Cur.StopTime.TimeOfDay)>0){
					MessageBox.Show(Lan.g(this,"Stop time must be later than start time."));
					return;
				}
				for(int i=0;i<Schedules.ListDay.Length;i++){
				  if(Schedules.Cur.ScheduleNum!=Schedules.ListDay[i].ScheduleNum
						&& Schedules.Cur.StartTime.TimeOfDay.CompareTo(Schedules.ListDay[i].StartTime.TimeOfDay) >= 0
						&& Schedules.Cur.StartTime.TimeOfDay.CompareTo(Schedules.ListDay[i].StopTime.TimeOfDay) < 0
						){
						MessageBox.Show(Lan.g(this,"Cannot overlap another time block."));
						return;
					}
					if(Schedules.Cur.ScheduleNum!=Schedules.ListDay[i].ScheduleNum
						&& Schedules.Cur.StopTime.TimeOfDay.CompareTo(Schedules.ListDay[i].StartTime.TimeOfDay) > 0
						&& Schedules.Cur.StopTime.TimeOfDay.CompareTo(Schedules.ListDay[i].StopTime.TimeOfDay) <= 0
						){
						MessageBox.Show(Lan.g(this,"Cannot overlap another time block."));
						return;
					}
					if(Schedules.Cur.ScheduleNum!=Schedules.ListDay[i].ScheduleNum
						&& Schedules.Cur.StartTime.TimeOfDay.CompareTo(Schedules.ListDay[i].StartTime.TimeOfDay) <= 0
						&& Schedules.Cur.StopTime.TimeOfDay.CompareTo(Schedules.ListDay[i].StopTime.TimeOfDay) >= 0
						){
						MessageBox.Show(Lan.g(this,"Cannot overlap another time block."));
					  return;
					}
				}  
      }     
      if(IsNew){
			   Schedules.InsertCur();
			}
			else{
				 Schedules.UpdateCur();
			}
			DialogResult=DialogResult.OK;		  
    }

		private void butCancel_Click(object sender, System.EventArgs e) {
		
		}   

	}
}
