using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormApptsOther : System.Windows.Forms.Form{
		private System.Windows.Forms.CheckBox checkDone;
		private System.Windows.Forms.Button butPin;
		private System.Windows.Forms.Button butNew;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.TextBox textRecallDue;
		private System.ComponentModel.Container components = null;
		private OpenDental.TableApptsOther tbApts;
		private System.Windows.Forms.Button butGoTo;
		public OtherResult oResult;
		private System.Windows.Forms.TextBox textApptModNote;
		private System.Windows.Forms.Label label1;
		public bool InitialClick;

		public FormApptsOther(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.butGoTo,
				this.butNew,
				this.butPin,
				this.label1,
				this.label4,
				this.checkDone,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butCancel,
			}); 
		}

		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormApptsOther));
			this.checkDone = new System.Windows.Forms.CheckBox();
			this.tbApts = new OpenDental.TableApptsOther();
			this.butPin = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.butNew = new System.Windows.Forms.Button();
			this.textRecallDue = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.butGoTo = new System.Windows.Forms.Button();
			this.textApptModNote = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// checkDone
			// 
			this.checkDone.AutoCheck = false;
			this.checkDone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.checkDone.Location = new System.Drawing.Point(30, 54);
			this.checkDone.Name = "checkDone";
			this.checkDone.Size = new System.Drawing.Size(112, 16);
			this.checkDone.TabIndex = 1;
			this.checkDone.TabStop = false;
			this.checkDone.Text = "Next Appt Done";
			// 
			// tbApts
			// 
			this.tbApts.BackColor = System.Drawing.SystemColors.Window;
			this.tbApts.Location = new System.Drawing.Point(28, 80);
			this.tbApts.Name = "tbApts";
			this.tbApts.SelectionMode = SelectionMode.One;//OpenDental.SelectRowsMode.OneToggle;
			this.tbApts.Size = new System.Drawing.Size(869, 492);
			this.tbApts.TabIndex = 2;
			this.tbApts.TabStop = false;
			// 
			// butPin
			// 
			this.butPin.Image = ((System.Drawing.Image)(resources.GetObject("butPin.Image")));
			this.butPin.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.butPin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.butPin.Location = new System.Drawing.Point(496, 618);
			this.butPin.Name = "butPin";
			this.butPin.Size = new System.Drawing.Size(133, 28);
			this.butPin.TabIndex = 1;
			this.butPin.Text = "          Copy to Pinboard";
			this.butPin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPin.Click += new System.EventHandler(this.butPin_Click);
			// 
			// butCancel
			// 
			this.butCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.butCancel.Location = new System.Drawing.Point(834, 618);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 28);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butNew
			// 
			this.butNew.Image = ((System.Drawing.Image)(resources.GetObject("butNew.Image")));
			this.butNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.butNew.Location = new System.Drawing.Point(648, 618);
			this.butNew.Name = "butNew";
			this.butNew.Size = new System.Drawing.Size(100, 28);
			this.butNew.TabIndex = 2;
			this.butNew.Text = "        Create New";
			this.butNew.Click += new System.EventHandler(this.butNew_Click);
			// 
			// textRecallDue
			// 
			this.textRecallDue.Location = new System.Drawing.Point(120, 10);
			this.textRecallDue.Name = "textRecallDue";
			this.textRecallDue.ReadOnly = true;
			this.textRecallDue.Size = new System.Drawing.Size(72, 20);
			this.textRecallDue.TabIndex = 40;
			this.textRecallDue.Text = "";
			this.textRecallDue.Visible = false;
			// 
			// label4
			// 
			this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label4.Location = new System.Drawing.Point(18, 14);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(96, 16);
			this.label4.TabIndex = 39;
			this.label4.Text = "Recall Due Date";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.label4.Visible = false;
			// 
			// butGoTo
			// 
			this.butGoTo.Image = ((System.Drawing.Image)(resources.GetObject("butGoTo.Image")));
			this.butGoTo.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.butGoTo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.butGoTo.Location = new System.Drawing.Point(382, 618);
			this.butGoTo.Name = "butGoTo";
			this.butGoTo.Size = new System.Drawing.Size(98, 28);
			this.butGoTo.TabIndex = 0;
			this.butGoTo.Text = "         Go To Appt";
			this.butGoTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butGoTo.Click += new System.EventHandler(this.butGoTo_Click);
			// 
			// textApptModNote
			// 
			this.textApptModNote.BackColor = System.Drawing.Color.White;
			this.textApptModNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textApptModNote.ForeColor = System.Drawing.Color.Red;
			this.textApptModNote.Location = new System.Drawing.Point(446, 12);
			this.textApptModNote.Multiline = true;
			this.textApptModNote.Name = "textApptModNote";
			this.textApptModNote.ReadOnly = true;
			this.textApptModNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textApptModNote.Size = new System.Drawing.Size(202, 36);
			this.textApptModNote.TabIndex = 44;
			this.textApptModNote.Text = "";
			// 
			// label1
			// 
			this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label1.Location = new System.Drawing.Point(306, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(142, 23);
			this.label1.TabIndex = 45;
			this.label1.Text = "Appointment Module Note";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormApptsOther
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(924, 658);
			this.ControlBox = false;
			this.Controls.Add(this.textApptModNote);
			this.Controls.Add(this.textRecallDue);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butGoTo);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.butNew);
			this.Controls.Add(this.butPin);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.tbApts);
			this.Controls.Add(this.checkDone);
			this.Name = "FormApptsOther";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = Lan.g(this,"Other Appointments");
			this.Load += new System.EventHandler(this.FormApptsOther_Load);
			this.ResumeLayout(false);

		}
		#endregion

		public OtherResult OResult{
			get{return oResult;}
		}

		private void FormApptsOther_Load(object sender, System.EventArgs e) {
			Text=Lan.g(this,"Appointments for")+" "+Patients.GetCurNameLF();
			textApptModNote.Text=Patients.Cur.ApptModNote;
			Filltb();
		}

		private void Filltb(){
			if(Patients.Cur.NextAptNum==-1){ 
        checkDone.Checked=true;
      }
			else{ 
        checkDone.Checked=false;
      }
			Appointments.RefreshOther();
			int[] aptNums=new int[Appointments.ListOth.Length];
			for(int i=0;i<Appointments.ListOth.Length;i++){
				aptNums[i]=Appointments.ListOth[i].AptNum;
			}
			Procedures.GetProcsMultApts(aptNums);
			tbApts.ResetRows(Appointments.ListOth.Length);
			tbApts.SetGridColor(Color.DarkGray);
			for(int i=0;i<Appointments.ListOth.Length;i++){
				tbApts.Cell[4,i]="";//procs
				if(Patients.Cur.NextAptNum==Appointments.ListOth[i].AptNum){//Next appt
					Procedures.GetProcsForSingle(Patients.Cur.NextAptNum,true);
					for(int j=0;j<Procedures.ProcsForSingle.Length;j++){
						tbApts.Cell[4,i]+=Procedures.ProcsForSingle[j];
						if(j<Procedures.ProcsForSingle.Length-1){
							tbApts.Cell[4,i]+=", ";
            }
					}
					tbApts.Cell[0,i]=Lan.g(this,"NEXT");
				}
				else{
					Procedures.GetProcsOneApt(Appointments.ListOth[i].AptNum);
					for(int j=0;j<Procedures.ProcsOneApt.Length;j++){
						tbApts.Cell[4,i]+=Procedures.ProcsOneApt[j];
						if(j<Procedures.ProcsOneApt.Length-1){
							tbApts.Cell[4,i]+=", ";
            }
					}
					if(Patients.Cur.NextAptNum==Appointments.ListOth[i].NextAptNum){
						tbApts.Cell[0,i]=Lan.g(this,"sched");
          }
					else{
            tbApts.Cell[0,i]="";
          }
				}
				tbApts.Cell[1,i]=Appointments.ListOth[i].AptStatus.ToString();
					//Defs.GetName(DefCat.RecallUnschedStatus,Appointments.List[i].UnschedStatus);
				if(Appointments.ListOth[i].AptDateTime.CompareTo(DateTime.Parse("1/1/1880")) > 0){
					tbApts.Cell[2,i]=Appointments.ListOth[i].AptDateTime.ToString("d");
        }
				else{
          tbApts.Cell[2,i]="";
        }
				tbApts.Cell[3,i]=Appointments.ListOth[i].Pattern.Length.ToString()+"0";
				tbApts.Cell[5,i]=Appointments.ListOth[i].Note;
			}
			//if(tbApts.SelectedRow!=-1){
			//	tbApts.ColorRow(tbApts.SelectedRow,Color.LightGray);
			//}
			tbApts.LayoutTables();
		}

		private void butNew_Click(object sender, System.EventArgs e) {
			Appointments.Cur=new Appointment();
			Appointments.Cur.PatNum=Patients.Cur.PatNum;
			Appointments.Cur.Pattern="/X/";
			if(Patients.Cur.PriProv==0){
				Appointments.Cur.ProvNum=PIn.PInt(((Pref)Prefs.HList["PracticeDefaultProv"]).ValueString);
			}
			else{			
				Appointments.Cur.ProvNum=Patients.Cur.PriProv;
			}
			Appointments.Cur.ProvHyg=Patients.Cur.SecProv;
			Appointments.Cur.AptStatus=ApptStatus.Scheduled;
			if(InitialClick){//initially double clicked on appt module
				DateTime d=Appointments.DateSelected;
				Appointments.Cur.AptDateTime=new DateTime(d.Year,d.Month,d.Day
					,ContrAppt.SheetClickedonHour,ContrAppt.SheetClickedonMin,0);
				Appointments.Cur.Op=ContrAppt.SheetClickedonOp;
			}
			//else{//new appt will be placed on pinboard instead of specific time
			Appointments.InsertCur();
			FormApptEdit FormApptEdit2=new FormApptEdit();
			FormApptEdit2.IsNew=true;
			FormApptEdit2.ShowDialog();
			if(FormApptEdit2.DialogResult!=DialogResult.OK){
				return;
			}
			if(InitialClick){
				oResult=OtherResult.CreateNew;
			}
			else{
				CreateCurInfo(true);
				oResult=OtherResult.NewToPinBoard;
//fix: will have to include a way to delete appt if user clears pinboard
			}
			Close();
		}

		private void butPin_Click(object sender, System.EventArgs e) {
			if(tbApts.SelectedRow==-1){
				MessageBox.Show(Lan.g(this,"Please select appointment first."));
				return;
			}
			Appointments.Cur=Appointments.ListOth[tbApts.SelectedRow];
			if(Patients.Cur.NextAptNum==Appointments.Cur.AptNum){//if clicked on NEXT
				bool NextIsSched=false;
				for(int i=0;i<Appointments.ListOth.Length;i++){
					if(Appointments.ListOth[i].NextAptNum==Patients.Cur.NextAptNum){//if the next appointment is already sched
						NextIsSched=true;
					}
				}
				if(NextIsSched){
					if(MessageBox.Show(Lan.g(this,"The Next appointment is already scheduled.  Do you wish to continue?"),""
						,MessageBoxButtons.OKCancel)!=DialogResult.OK){
						return;
					}
				}
			}
			else{//if clicked on any appointment except NEXT
				switch(Appointments.Cur.AptStatus){
					case ApptStatus.Complete:
						MessageBox.Show(Lan.g(this,"Not allowed to move a completed appointment from here."));
						return;
					case ApptStatus.ASAP:
					case ApptStatus.Scheduled:
						if(MessageBox.Show(Lan.g(this,"Do you really want to move a previously scheduled appointment?"),""
							,MessageBoxButtons.OKCancel)!=DialogResult.OK){
							return;
						}
						break;
					case ApptStatus.Broken:
					case ApptStatus.None:
					case ApptStatus.UnschedList:
						Appointments.Cur.AptStatus=ApptStatus.Scheduled;
						break;
				}			
			}
			CreateCurInfo(false);
			oResult=OtherResult.CopyToPinBoard;
			Close();
		}

		private void CreateCurInfo(bool isNew){
			ContrAppt.CurInfo=new InfoApt();
			ContrAppt.CurInfo.MyApt=Appointments.Cur;
			ContrAppt.CurInfo.CreditAndIns=Patients.GetCreditIns();
			ContrAppt.CurInfo.PatientName=Patients.GetCurNameLF();
			if(Appointments.Cur.AptNum==Patients.Cur.NextAptNum){//if is Next apt
				//should already be there: Procedures.GetProcsForSingle(Appointments.Cur.AptNum,true);
				ContrAppt.CurInfo.Procs=Procedures.ProcsForSingle;
			}
			else{//normal apt
				if(isNew){
					Procedures.GetProcsForSingle(Appointments.Cur.AptNum,false);
					ContrAppt.CurInfo.Procs=Procedures.ProcsForSingle;
				}
				else{//from list
					//gets one from ProcsMultApts:
					Procedures.GetProcsOneApt(Appointments.Cur.AptNum);
					ContrAppt.CurInfo.Procs=Procedures.ProcsOneApt;
				}
			}
		}

		private void butGoTo_Click(object sender, System.EventArgs e) {
			if(tbApts.SelectedRow==-1){
				MessageBox.Show(Lan.g(this,"Please select appointment first."));
				return;
			}
			Appointments.Cur=Appointments.ListOth[tbApts.SelectedRow];
			if(Appointments.Cur.AptDateTime.CompareTo(DateTime.Parse("1/1/1880")) <0){
				MessageBox.Show(Lan.g(this,"Unable to go to unscheduled appointment."));
				return;
			}
			oResult=OtherResult.GoTo;
			Close();
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			oResult=OtherResult.Cancel;
			Close();
		}

	}
}
