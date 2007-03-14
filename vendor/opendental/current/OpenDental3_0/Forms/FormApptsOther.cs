using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	///<summary></summary>
	public class FormApptsOther : System.Windows.Forms.Form{
		private System.Windows.Forms.CheckBox checkDone;
		private System.Windows.Forms.Button butCancel;
		private System.ComponentModel.Container components = null;
		private OpenDental.TableApptsOther tbApts;
		///<summary></summary>
		public OtherResult oResult;
		private System.Windows.Forms.TextBox textApptModNote;
		private System.Windows.Forms.Label label1;
		private OpenDental.XPButton butGoTo;
		private OpenDental.XPButton butPin;
		private OpenDental.XPButton butNew;
		private System.Windows.Forms.Label label2;
		///<summary></summary>
		public bool InitialClick;
		private System.Windows.Forms.ListView listFamily;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private Appointment[] ListOth;

		///<summary></summary>
		public FormApptsOther(){
			InitializeComponent();
			tbApts.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbApts_CellDoubleClicked);
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.butGoTo,
				this.butNew,
				this.butPin,
				this.label1,
				this.label2,
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
			this.textApptModNote = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butGoTo = new OpenDental.XPButton();
			this.butPin = new OpenDental.XPButton();
			this.butNew = new OpenDental.XPButton();
			this.label2 = new System.Windows.Forms.Label();
			this.listFamily = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// checkDone
			// 
			this.checkDone.AutoCheck = false;
			this.checkDone.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkDone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.checkDone.Location = new System.Drawing.Point(29, 145);
			this.checkDone.Name = "checkDone";
			this.checkDone.Size = new System.Drawing.Size(210, 16);
			this.checkDone.TabIndex = 1;
			this.checkDone.TabStop = false;
			this.checkDone.Text = "Next Appt Done";
			// 
			// tbApts
			// 
			this.tbApts.BackColor = System.Drawing.SystemColors.Window;
			this.tbApts.Location = new System.Drawing.Point(28, 168);
			this.tbApts.Name = "tbApts";
			this.tbApts.ScrollValue = 1;
			this.tbApts.SelectedIndices = new int[0];
			this.tbApts.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbApts.Size = new System.Drawing.Size(769, 404);
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
			// textApptModNote
			// 
			this.textApptModNote.BackColor = System.Drawing.Color.White;
			this.textApptModNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textApptModNote.ForeColor = System.Drawing.Color.Red;
			this.textApptModNote.Location = new System.Drawing.Point(594, 33);
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
			this.label1.Location = new System.Drawing.Point(429, 37);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(163, 21);
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
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(29, 13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(168, 17);
			this.label2.TabIndex = 57;
			this.label2.Text = "Recall for Family";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listFamily
			// 
			this.listFamily.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																																								 this.columnHeader1,
																																								 this.columnHeader2,
																																								 this.columnHeader4,
																																								 this.columnHeader3,
																																								 this.columnHeader5});
			this.listFamily.FullRowSelect = true;
			this.listFamily.GridLines = true;
			this.listFamily.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listFamily.Location = new System.Drawing.Point(29, 36);
			this.listFamily.Name = "listFamily";
			this.listFamily.Size = new System.Drawing.Size(384, 97);
			this.listFamily.TabIndex = 58;
			this.listFamily.View = System.Windows.Forms.View.Details;
			this.listFamily.DoubleClick += new System.EventHandler(this.listFamily_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Family Member";
			this.columnHeader1.Width = 120;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Age";
			this.columnHeader2.Width = 40;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Gender";
			this.columnHeader4.Width = 50;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Due Date";
			this.columnHeader3.Width = 74;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Scheduled";
			this.columnHeader5.Width = 74;
			// 
			// FormApptsOther
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(924, 658);
			this.Controls.Add(this.listFamily);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butNew);
			this.Controls.Add(this.butPin);
			this.Controls.Add(this.butGoTo);
			this.Controls.Add(this.textApptModNote);
			this.Controls.Add(this.label1);
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
			Appointment[] aptsOnePat;
			listFamily.Items.Clear();
			ListViewItem item;
			DateTime dueDate;
			for(int i=0;i<Patients.FamilyList.Length;i++){
				item=new ListViewItem(Patients.GetNameInFamFLI(i));
				if(Patients.FamilyList[i].PatNum==Patients.Cur.PatNum){
					item.BackColor=Color.Silver;
				}
				item.SubItems.Add(Patients.FamilyList[i].Age.ToString());
				item.SubItems.Add(Patients.FamilyList[i].Gender.ToString());
				dueDate=Patients.GetRecallDue(Patients.FamilyList[i].PatNum);
				if(dueDate.Year<1880){
					item.SubItems.Add("");
				}
				else{
					item.SubItems.Add(dueDate.ToShortDateString());
				}
				if(dueDate<=DateTime.Today){
					item.ForeColor=Color.Red;
				}
				aptsOnePat=Appointments.GetForPat(Patients.FamilyList[i].PatNum);
				for(int a=0;a<aptsOnePat.Length;a++){
					if(aptsOnePat[a].AptDateTime.Date<=DateTime.Today){
						continue;//disregard old appts.
					}
					item.SubItems.Add(aptsOnePat[a].AptDateTime.ToShortDateString());
					break;//we only want one appt
					//could add condition here to add blank subitem if no date found
				}
				listFamily.Items.Add(item);
			}
			if(Patients.Cur.NextAptNum==-1){ 
        checkDone.Checked=true;
      }
			else{ 
        checkDone.Checked=false;
      }
			ListOth=Appointments.GetForPat(Patients.Cur.PatNum);
			tbApts.ResetRows(ListOth.Length);
			tbApts.SetGridColor(Color.DarkGray);
			for(int i=0;i<ListOth.Length;i++){
				tbApts.Cell[0,i]=ListOth[i].AptStatus.ToString();
				if(ListOth[i].AptDateTime.Year > 1880){
					tbApts.Cell[1,i]=ListOth[i].AptDateTime.ToString("d");
					tbApts.Cell[2,i]=ListOth[i].AptDateTime.ToString("t");
        }
				else{
          tbApts.Cell[1,i]="";
					tbApts.Cell[2,i]="";
        }
				tbApts.Cell[3,i]=(ListOth[i].Pattern.Length*5).ToString();
				tbApts.Cell[4,i]=ListOth[i].ProcDescript;
				tbApts.Cell[5,i]=ListOth[i].Note;
			}
			tbApts.LayoutTables();
		}

		private void butNew_Click(object sender, System.EventArgs e) {
			Appointments.Cur=new Appointment();
			Appointments.Cur.PatNum=Patients.Cur.PatNum;
			if(Patients.Cur.DateFirstVisit.Year<1880
				&& !Procedures.AreAnyComplete(Patients.Cur.PatNum))//this only runs if firstVisit blank
			{
				Appointments.Cur.IsNewPatient=true;
			}
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
				int minutes=(int)(ContrAppt.SheetClickedonMin/ContrApptSheet.MinPerIncr)
					*ContrApptSheet.MinPerIncr;
				Appointments.Cur.AptDateTime=new DateTime(d.Year,d.Month,d.Day
					,ContrAppt.SheetClickedonHour,minutes,0);
				Appointments.Cur.Op=ContrAppt.SheetClickedonOp;
			}
			else{
				//new appt will be placed on pinboard instead of specific time
			}
			Appointments.InsertCur();
			Appointments.CurOld=Appointments.Cur;
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
			Appointments.Cur=ListOth[tbApts.SelectedRow];
			Appointments.CurOld=Appointments.Cur;
			if(!OKtoSendToPinboard())
				return;
			CreateCurInfo();
			oResult=OtherResult.CopyToPinBoard;
			DialogResult=DialogResult.OK;
		}

		/// <summary>Tests the current appointment to see if it is acceptable to send it to the pinboard.  Also asks user appropriate questions to verify that's what they want to do.  Returns false if it will not be going to pinboard after all.</summary>
		private bool OKtoSendToPinboard(){
			if(Appointments.Cur.AptStatus==ApptStatus.Next){//if is a NEXT appointment
				bool NextIsSched=false;
				for(int i=0;i<ListOth.Length;i++){
					if(ListOth[i].NextAptNum==Patients.Cur.NextAptNum){//if the next appointment is already sched
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
			Appointments.Cur=ListOth[e.Row];
			Appointments.CurOld=Appointments.Cur;
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
			ProcDesc procsForSingle;
			if(Appointments.Cur.AptNum==Patients.Cur.NextAptNum){//if is Next apt
				procsForSingle=Procedures.GetProcsForSingle(Appointments.Cur.AptNum,true);
			}
			else{//normal apt
				procsForSingle=Procedures.GetProcsForSingle(Appointments.Cur.AptNum,false);
			}
			ContrAppt.CurInfo.Procs=procsForSingle.ProcLines;
			ContrAppt.CurInfo.Production=procsForSingle.Production;
			ContrAppt.CurInfo.MyPatient=Patients.Cur;
		}

		private void butGoTo_Click(object sender, System.EventArgs e) {
			if(tbApts.SelectedRow==-1){
				MessageBox.Show(Lan.g(this,"Please select appointment first."));
				return;
			}
			Appointments.Cur=ListOth[tbApts.SelectedRow];
			Appointments.CurOld=Appointments.Cur;
			if(Appointments.Cur.AptDateTime.Year<1880){
				MessageBox.Show(Lan.g(this,"Unable to go to unscheduled appointment."));
				return;
			}
			oResult=OtherResult.GoTo;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormApptsOther_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			oResult=OtherResult.Cancel;
		}

		private void listFamily_DoubleClick(object sender, System.EventArgs e) {
			if(listFamily.SelectedIndices.Count==0){
				return;
			}
			int originalPatNum=Patients.Cur.PatNum;
			FormRecallEdit FormRE=new FormRecallEdit();
			//FormRE.DisplayedRecallItem=(RecallItem)MainAL[e.Row];
			FormRE.PatNum=Patients.FamilyList[listFamily.SelectedIndices[0]].PatNum;
			FormRE.RecallStatus=Patients.FamilyList[listFamily.SelectedIndices[0]].RecallStatus;
			FormRE.DueDate=PIn.PDate(listFamily.Items[listFamily.SelectedIndices[0]].SubItems[2].Text);
			FormRE.ShowDialog();
			if(FormRE.PinClicked){
				oResult=OtherResult.CopyToPinBoard;
				//already created curInfo in FormRE.
				DialogResult=DialogResult.OK;
			}
			else{
				Patients.GetFamily(originalPatNum);
				Filltb();
			}
		}

	}
}
