using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	///<summary></summary>
	public class FormApptsOther : System.Windows.Forms.Form{
		private System.Windows.Forms.CheckBox checkDone;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.TextBox textRecallDue;
		private System.ComponentModel.Container components = null;
		private OpenDental.TableApptsOther tbApts;
		///<summary></summary>
		public OtherResult oResult;
		private System.Windows.Forms.TextBox textApptModNote;
		private System.Windows.Forms.Label label1;
		private OpenDental.XPButton butGoTo;
		private OpenDental.XPButton butPin;
		private OpenDental.XPButton butNew;
		///<summary></summary>
		public bool InitialClick;

		///<summary></summary>
		public FormApptsOther(){
			InitializeComponent();
			tbApts.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbApts_CellDoubleClicked);
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

		private void InitializeComponent(){
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormApptsOther));
			this.checkDone = new System.Windows.Forms.CheckBox();
			this.tbApts = new OpenDental.TableApptsOther();
			this.butCancel = new System.Windows.Forms.Button();
			this.textRecallDue = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textApptModNote = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butGoTo = new OpenDental.XPButton();
			this.butPin = new OpenDental.XPButton();
			this.butNew = new OpenDental.XPButton();
			this.SuspendLayout();
			// 
			// checkDone
			// 
			this.checkDone.AutoCheck = false;
			this.checkDone.FlatStyle = System.Windows.Forms.FlatStyle.System;
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
			this.tbApts.ScrollValue = 1;
			this.tbApts.SelectedIndices = new int[0];
			this.tbApts.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbApts.Size = new System.Drawing.Size(769, 492);
			this.tbApts.TabIndex = 2;
			this.tbApts.TabStop = false;
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.butCancel.Location = new System.Drawing.Point(834, 618);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
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
			// butGoTo
			// 
			this.butGoTo.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butGoTo.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butGoTo.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butGoTo.Image = ((System.Drawing.Image)(resources.GetObject("butGoTo.Image")));
			this.butGoTo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butGoTo.Location = new System.Drawing.Point(411, 618);
			this.butGoTo.Name = "butGoTo";
			this.butGoTo.Size = new System.Drawing.Size(106, 26);
			this.butGoTo.TabIndex = 46;
			this.butGoTo.Text = "&Go To Appt";
			this.butGoTo.Click += new System.EventHandler(this.butGoTo_Click);
			// 
			// butPin
			// 
			this.butPin.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPin.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butPin.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butPin.Image = ((System.Drawing.Image)(resources.GetObject("butPin.Image")));
			this.butPin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPin.Location = new System.Drawing.Point(532, 618);
			this.butPin.Name = "butPin";
			this.butPin.Size = new System.Drawing.Size(134, 26);
			this.butPin.TabIndex = 47;
			this.butPin.Text = "Copy To &Pinboard";
			this.butPin.Click += new System.EventHandler(this.butPin_Click);
			// 
			// butNew
			// 
			this.butNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butNew.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butNew.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butNew.Image = ((System.Drawing.Image)(resources.GetObject("butNew.Image")));
			this.butNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butNew.Location = new System.Drawing.Point(681, 618);
			this.butNew.Name = "butNew";
			this.butNew.Size = new System.Drawing.Size(106, 26);
			this.butNew.TabIndex = 48;
			this.butNew.Text = "Create &New";
			this.butNew.Click += new System.EventHandler(this.butNew_Click);
			// 
			// FormApptsOther
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(924, 658);
			this.Controls.Add(this.butNew);
			this.Controls.Add(this.butPin);
			this.Controls.Add(this.butGoTo);
			this.Controls.Add(this.textApptModNote);
			this.Controls.Add(this.textRecallDue);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.tbApts);
			this.Controls.Add(this.checkDone);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormApptsOther";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Other Appointments";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormApptsOther_Closing);
			this.Load += new System.EventHandler(this.FormApptsOther_Load);
			this.ResumeLayout(false);

		}
		#endregion

		///<summary></summary>
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
			tbApts.ResetRows(Appointments.ListOth.Length);
			tbApts.SetGridColor(Color.DarkGray);
			for(int i=0;i<Appointments.ListOth.Length;i++){
				tbApts.Cell[0,i]=Appointments.ListOth[i].AptStatus.ToString();
				if(Appointments.ListOth[i].AptDateTime.Year > 1880){
					tbApts.Cell[1,i]=Appointments.ListOth[i].AptDateTime.ToString("d");
					tbApts.Cell[2,i]=Appointments.ListOth[i].AptDateTime.ToString("t");
        }
				else{
          tbApts.Cell[1,i]="";
					tbApts.Cell[2,i]="";
        }
				tbApts.Cell[3,i]=Appointments.ListOth[i].Pattern.Length.ToString()+"0";
				tbApts.Cell[4,i]=Appointments.ListOth[i].ProcDescript;
				tbApts.Cell[5,i]=Appointments.ListOth[i].Note;
			}
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
			else{
				//new appt will be placed on pinboard instead of specific time
			}
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
				CreateCurInfo();
				oResult=OtherResult.NewToPinBoard;
			}
			DialogResult=DialogResult.OK;
		}

		private void butPin_Click(object sender, System.EventArgs e) {
			if(tbApts.SelectedRow==-1){
				MessageBox.Show(Lan.g(this,"Please select appointment first."));
				return;
			}
			Appointments.Cur=Appointments.ListOth[tbApts.SelectedRow];
			if(!OKtoSendToPinboard())
				return;
			CreateCurInfo();
			oResult=OtherResult.CopyToPinBoard;
			Appointments.ListOth=null;
			DialogResult=DialogResult.OK;
		}

		/// <summary>Tests the current appointment to see if it is acceptable to send it to the pinboard.  Also asks user appropriate questions to verify that's what they want to do.  Returns false if it will not be going to pinboard after all.</summary>
		private bool OKtoSendToPinboard(){
			if(Appointments.Cur.AptStatus==ApptStatus.Next){//if is a NEXT appointment
				bool NextIsSched=false;
				for(int i=0;i<Appointments.ListOth.Length;i++){
					if(Appointments.ListOth[i].NextAptNum==Patients.Cur.NextAptNum){//if the next appointment is already sched
						NextIsSched=true;
					}
				}
				if(NextIsSched){
					if(MessageBox.Show(Lan.g(this,"The Next appointment is already scheduled.  Do you wish to continue?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
						return false;
					}
				}
			}
			else{//if appointment is not NEXT
				switch(Appointments.Cur.AptStatus){
					case ApptStatus.Complete:
						MessageBox.Show(Lan.g(this,"Not allowed to move a completed appointment from here."));
						return false;
					case ApptStatus.ASAP:
					case ApptStatus.Scheduled:
						if(MessageBox.Show(Lan.g(this,"Do you really want to move a previously scheduled appointment?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
							return false;
						}
						break;
					case ApptStatus.Broken://status gets changed after dragging off pinboard.
					case ApptStatus.None:
					case ApptStatus.UnschedList://status gets changed after dragging off pinboard.
						break;
				}			
			}
			//if it's a next appointment, the next appointment will end up on the pinboard.  The copy will be made after dragging it off the pinboard.
			return true;
		}

		private void tbApts_CellDoubleClicked(object sender, CellEventArgs e){
			int currentSelection=tbApts.SelectedRow;
			int currentScroll=tbApts.ScrollValue;
			//MessageBox.Show(currentScroll.ToString());
			Appointments.Cur=Appointments.ListOth[e.Row];
			FormApptEdit FormAE=new FormApptEdit();
			FormAE.PinIsVisible=true;
			FormAE.ShowDialog();
			if(FormAE.DialogResult!=DialogResult.OK)
				return;
			if(FormAE.PinClicked){
				if(!OKtoSendToPinboard())
					return;
				CreateCurInfo();
				oResult=OtherResult.CopyToPinBoard;
				Appointments.ListOth=null;
				DialogResult=DialogResult.OK;
			}
			else{
				Filltb();
				tbApts.SetSelected(currentSelection,true);
				tbApts.ScrollValue=currentScroll;
			}
		}	

		///<summary>Prepares the necessary info for placement of the appointment on the pinboard.</summary>
		private void CreateCurInfo(){
			ContrAppt.CurInfo=new InfoApt();
			ContrAppt.CurInfo.MyApt=Appointments.Cur;
			ContrAppt.CurInfo.CreditAndIns=Patients.GetCreditIns();
			ContrAppt.CurInfo.PatientName=Patients.GetCurNameLF();
			if(Appointments.Cur.AptNum==Patients.Cur.NextAptNum){//if is Next apt
				Procedures.GetProcsForSingle(Appointments.Cur.AptNum,true);
			}
			else{//normal apt
				Procedures.GetProcsForSingle(Appointments.Cur.AptNum,false);
			}
			ContrAppt.CurInfo.Procs=Procedures.ProcsForSingle;
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
			Appointments.ListOth=null;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormApptsOther_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			oResult=OtherResult.Cancel;
			Appointments.ListOth=null;
		}

	}
}
