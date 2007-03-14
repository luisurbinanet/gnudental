using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormUnschedEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.TextBox textWorkPhone;
		private System.Windows.Forms.TextBox textWireless;
		private System.Windows.Forms.TextBox textAddrNotes;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textHomePhone;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ListBox listStatus;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ListBox listQuickAdd;
		private System.Windows.Forms.TextBox textCalls;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butDateLine;
		private System.ComponentModel.Container components = null;// Required designer variable.
		private System.Windows.Forms.Label labelProcs;
		private System.Windows.Forms.Label labelNote;
		private System.Windows.Forms.Label labelLab;
		private System.Windows.Forms.GroupBox groupAppt;
		private System.Windows.Forms.Label labelPatient;
		private OpenDental.XPButton butDelete;
		private OpenDental.XPButton butPin;
		public bool PinClicked=false;

		public FormUnschedEdit(){
			InitializeComponent();// Required for Windows Form Designer support
			Lan.C(this, new System.Windows.Forms.Control[] {
				label7,
				label2,
				label5,
				label4,
				label8,
				label10,
				label9,
				labelProcs,
				labelNote,
				this.labelLab,
				labelPatient,
				butPin,
				butDateLine,
				this.groupAppt,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
				butDelete,
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormUnschedEdit));
			this.textWorkPhone = new System.Windows.Forms.TextBox();
			this.textWireless = new System.Windows.Forms.TextBox();
			this.textAddrNotes = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textHomePhone = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.listStatus = new System.Windows.Forms.ListBox();
			this.label9 = new System.Windows.Forms.Label();
			this.listQuickAdd = new System.Windows.Forms.ListBox();
			this.butDateLine = new System.Windows.Forms.Button();
			this.textCalls = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.labelProcs = new System.Windows.Forms.Label();
			this.groupAppt = new System.Windows.Forms.GroupBox();
			this.labelPatient = new System.Windows.Forms.Label();
			this.labelLab = new System.Windows.Forms.Label();
			this.labelNote = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.XPButton();
			this.butPin = new OpenDental.XPButton();
			this.groupAppt.SuspendLayout();
			this.SuspendLayout();
			// 
			// textWorkPhone
			// 
			this.textWorkPhone.Location = new System.Drawing.Point(514, 42);
			this.textWorkPhone.Name = "textWorkPhone";
			this.textWorkPhone.ReadOnly = true;
			this.textWorkPhone.TabIndex = 22;
			this.textWorkPhone.Text = "";
			// 
			// textWireless
			// 
			this.textWireless.Location = new System.Drawing.Point(514, 74);
			this.textWireless.Name = "textWireless";
			this.textWireless.ReadOnly = true;
			this.textWireless.TabIndex = 21;
			this.textWireless.Text = "";
			// 
			// textAddrNotes
			// 
			this.textAddrNotes.Location = new System.Drawing.Point(514, 104);
			this.textAddrNotes.Multiline = true;
			this.textAddrNotes.Name = "textAddrNotes";
			this.textAddrNotes.ReadOnly = true;
			this.textAddrNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textAddrNotes.Size = new System.Drawing.Size(240, 48);
			this.textAddrNotes.TabIndex = 20;
			this.textAddrNotes.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(412, 46);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 16);
			this.label7.TabIndex = 19;
			this.label7.Text = "Work Phone";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(412, 76);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.TabIndex = 18;
			this.label2.Text = "Wireless Phone";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textHomePhone
			// 
			this.textHomePhone.Location = new System.Drawing.Point(514, 12);
			this.textHomePhone.Name = "textHomePhone";
			this.textHomePhone.ReadOnly = true;
			this.textHomePhone.TabIndex = 17;
			this.textHomePhone.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(414, 14);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 16);
			this.label5.TabIndex = 16;
			this.label5.Text = "Home Phone";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(428, 108);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(84, 34);
			this.label4.TabIndex = 15;
			this.label4.Text = "Address/Phone Notes";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(20, 40);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(63, 18);
			this.label10.TabIndex = 29;
			this.label10.Text = "Status";
			// 
			// listStatus
			// 
			this.listStatus.Location = new System.Drawing.Point(20, 58);
			this.listStatus.Name = "listStatus";
			this.listStatus.Size = new System.Drawing.Size(166, 238);
			this.listStatus.TabIndex = 0;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(20, 361);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(106, 26);
			this.label9.TabIndex = 27;
			this.label9.Text = "Appt Phone Call Notes quick add ->";
			// 
			// listQuickAdd
			// 
			this.listQuickAdd.Location = new System.Drawing.Point(22, 391);
			this.listQuickAdd.Name = "listQuickAdd";
			this.listQuickAdd.Size = new System.Drawing.Size(189, 238);
			this.listQuickAdd.TabIndex = 2;
			this.listQuickAdd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listQuickAdd_MouseDown);
			// 
			// butDateLine
			// 
			this.butDateLine.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butDateLine.Location = new System.Drawing.Point(120, 327);
			this.butDateLine.Name = "butDateLine";
			this.butDateLine.Size = new System.Drawing.Size(90, 26);
			this.butDateLine.TabIndex = 1;
			this.butDateLine.Text = "Insert Date ->";
			this.butDateLine.Click += new System.EventHandler(this.butDateLine_Click);
			// 
			// textCalls
			// 
			this.textCalls.Location = new System.Drawing.Point(236, 188);
			this.textCalls.Multiline = true;
			this.textCalls.Name = "textCalls";
			this.textCalls.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textCalls.Size = new System.Drawing.Size(420, 460);
			this.textCalls.TabIndex = 3;
			this.textCalls.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(238, 168);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(244, 22);
			this.label8.TabIndex = 30;
			this.label8.Text = "Appointment Phone Call Notes (for patient)";
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(709, 584);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 6;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(709, 621);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 7;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// labelProcs
			// 
			this.labelProcs.Location = new System.Drawing.Point(6, 54);
			this.labelProcs.Name = "labelProcs";
			this.labelProcs.Size = new System.Drawing.Size(194, 36);
			this.labelProcs.TabIndex = 35;
			this.labelProcs.Text = "Procedures";
			// 
			// groupAppt
			// 
			this.groupAppt.Controls.Add(this.labelPatient);
			this.groupAppt.Controls.Add(this.labelLab);
			this.groupAppt.Controls.Add(this.labelNote);
			this.groupAppt.Controls.Add(this.labelProcs);
			this.groupAppt.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupAppt.Location = new System.Drawing.Point(198, 6);
			this.groupAppt.Name = "groupAppt";
			this.groupAppt.Size = new System.Drawing.Size(212, 148);
			this.groupAppt.TabIndex = 36;
			this.groupAppt.TabStop = false;
			this.groupAppt.Text = "Appointment";
			// 
			// labelPatient
			// 
			this.labelPatient.Location = new System.Drawing.Point(6, 18);
			this.labelPatient.Name = "labelPatient";
			this.labelPatient.Size = new System.Drawing.Size(198, 15);
			this.labelPatient.TabIndex = 38;
			this.labelPatient.Text = "Patient";
			// 
			// labelLab
			// 
			this.labelLab.Location = new System.Drawing.Point(6, 37);
			this.labelLab.Name = "labelLab";
			this.labelLab.Size = new System.Drawing.Size(161, 12);
			this.labelLab.TabIndex = 37;
			this.labelLab.Text = "LAB";
			// 
			// labelNote
			// 
			this.labelNote.Location = new System.Drawing.Point(6, 96);
			this.labelNote.Name = "labelNote";
			this.labelNote.Size = new System.Drawing.Size(196, 41);
			this.labelNote.TabIndex = 36;
			this.labelNote.Text = "Notes";
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(701, 470);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(90, 26);
			this.butDelete.TabIndex = 50;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butPin
			// 
			this.butPin.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPin.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butPin.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butPin.Image = ((System.Drawing.Image)(resources.GetObject("butPin.Image")));
			this.butPin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPin.Location = new System.Drawing.Point(696, 521);
			this.butPin.Name = "butPin";
			this.butPin.Size = new System.Drawing.Size(100, 26);
			this.butPin.TabIndex = 49;
			this.butPin.Text = "Pinboard";
			this.butPin.Click += new System.EventHandler(this.butPin_Click);
			// 
			// FormUnschedEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(823, 660);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butPin);
			this.Controls.Add(this.groupAppt);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.textCalls);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.listStatus);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.listQuickAdd);
			this.Controls.Add(this.butDateLine);
			this.Controls.Add(this.textWorkPhone);
			this.Controls.Add(this.textWireless);
			this.Controls.Add(this.textAddrNotes);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textHomePhone);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormUnschedEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Unscheduled Status";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormUnschedEdit_Closing);
			this.Load += new System.EventHandler(this.FormUnschedEdit_Load);
			this.groupAppt.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormUnschedEdit_Load(object sender, System.EventArgs e) {
			for(int i=0;i<Defs.Short[(int)DefCat.ApptPhoneNotes].Length;i++){
				this.listQuickAdd.Items.Add(Defs.Short[(int)DefCat.ApptPhoneNotes][i].ItemName);
			}
			for(int i=0;i<Defs.Short[(int)DefCat.RecallUnschedStatus].Length;i++){
				this.listStatus.Items.Add(Defs.Short[(int)DefCat.RecallUnschedStatus][i].ItemName);
				if(Defs.Short[(int)DefCat.RecallUnschedStatus][i].DefNum==Appointments.Cur.UnschedStatus)
					listStatus.SelectedIndex=i;
			}
			Patients.GetFamily(Appointments.Cur.PatNum);
			labelPatient.Text=Patients.GetCurNameLF();
			switch(Appointments.Cur.Lab){
				case LabCase.None:
					labelLab.Text="";
					break;
				case LabCase.Sent:
					labelLab.Text="LAB SENT";
					break;
				case LabCase.Received:
					labelLab.Text="LAB RECEIVED";
					break;
			}
			labelProcs.Text=FormUnsched.procsForCur;
			labelNote.Text=Appointments.Cur.Note;
			PatientNotes.Refresh();
			textCalls.Text=PatientNotes.Cur.ApptPhone;
			textCalls.SelectionStart=textCalls.Text.Length+2;
			textHomePhone.Text=Patients.Cur.HmPhone;
			textWorkPhone.Text=Patients.Cur.WkPhone;
			textWireless.Text=Patients.Cur.WirelessPhone;
			textAddrNotes.Text=Patients.Cur.AddrNote;
		}

		private void listQuickAdd_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listQuickAdd.IndexFromPoint(e.X,e.Y)==-1){
				return;
			}
			int caret=textCalls.SelectionStart;
			string strPaste;
			strPaste=Defs.Short[(int)DefCat.ApptPhoneNotes][listQuickAdd.IndexFromPoint(e.X,e.Y)].ItemName;
			textCalls.Text=textCalls.Text.Insert(caret,strPaste);
			textCalls.Select();
			textCalls.SelectionStart=caret+strPaste.Length;
			textCalls.SelectionLength=0;
			listQuickAdd.SelectedIndex=-1;
		}

		private void butDateLine_Click(object sender, System.EventArgs e) {
			int caret=textCalls.SelectionStart;
			string strPaste;
			strPaste="**"+DateTime.Today.ToString("d")+"**";
			textCalls.Text=textCalls.Text.Insert(caret,strPaste);
			textCalls.Select();
			textCalls.SelectionStart=caret+strPaste.Length;
			textCalls.SelectionLength=0;
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"Delete appointment?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			SaveChanges();
			Procedures.UnattachProcsInAppt(Appointments.Cur.AptNum);
			Appointments.DeleteCur();
			DialogResult=DialogResult.OK;
		}

		private void butPin_Click(object sender, System.EventArgs e) {
			SaveChanges();
			PinClicked=true;
			DialogResult=DialogResult.OK;
		}

		private void SaveChanges(){
			if(listStatus.SelectedIndex!=-1){
				Appointments.Cur.UnschedStatus=Defs.Short[(int)DefCat.RecallUnschedStatus][listStatus.SelectedIndex].DefNum;
				Appointments.UpdateCur();
			}
			PatientNotes.Cur.ApptPhone=textCalls.Text;
			PatientNotes.UpdateCur();
			Patients.PatIsLoaded=false;
			
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			SaveChanges();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormUnschedEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			Patients.PatIsLoaded=false;
		}

	}
}
