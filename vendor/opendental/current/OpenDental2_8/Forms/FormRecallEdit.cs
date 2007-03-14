using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormRecallEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ListBox listStatus;
		private System.Windows.Forms.TextBox textWorkPhone;
		private System.Windows.Forms.TextBox textWireless;
		private System.Windows.Forms.TextBox textAddrNotes;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textHomePhone;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Panel panel1;
		private OpenDental.ContrAccount contrAccount3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox textCreditType;
		private System.Windows.Forms.TextBox textAge;
		private System.Windows.Forms.TextBox textRecallInterval;
		private System.Windows.Forms.TextBox textPriIns;
		private System.Windows.Forms.TextBox textDueDate;
		private PatientNotes PatientNotes=new PatientNotes();
		///<summary></summary>
		public bool PinClicked=false;
		///<summary></summary>
		public DateTime DueDate;
		private System.Windows.Forms.TextBox textBillingType;
		private OpenDental.XPButton butPin;
		private OpenDental.TableCommLog tbCommlog;
		private OpenDental.XPButton butAddComm;
		private System.Windows.Forms.Label label14;
		private ArrayList ALCommItems;

		///<summary></summary>
		public FormRecallEdit(){
			InitializeComponent();
			tbCommlog.CellDoubleClicked+=new OpenDental.ContrTable.CellEventHandler(tbCommlog_CellDoubleClicked);
			Lan.C(this, new System.Windows.Forms.Control[] {
				label10,
				label7,
				label2,
				label5,
				label4,
				panel1,
				groupBox1,
				label1,
				label6,
				label3,
				label11,
				label12,
				label13,
				butPin,
				label14,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
			});
		}

		///<summary></summary>
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormRecallEdit));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.label10 = new System.Windows.Forms.Label();
			this.listStatus = new System.Windows.Forms.ListBox();
			this.textWorkPhone = new System.Windows.Forms.TextBox();
			this.textWireless = new System.Windows.Forms.TextBox();
			this.textAddrNotes = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textHomePhone = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.contrAccount3 = new OpenDental.ContrAccount();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBillingType = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.textDueDate = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.textPriIns = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.textRecallInterval = new System.Windows.Forms.TextBox();
			this.textCreditType = new System.Windows.Forms.TextBox();
			this.textAge = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.butPin = new OpenDental.XPButton();
			this.tbCommlog = new OpenDental.TableCommLog();
			this.butAddComm = new OpenDental.XPButton();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(838, 264);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 6;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(838, 226);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 5;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(5, 2);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(63, 18);
			this.label10.TabIndex = 46;
			this.label10.Text = "Status";
			// 
			// listStatus
			// 
			this.listStatus.Location = new System.Drawing.Point(5, 18);
			this.listStatus.Name = "listStatus";
			this.listStatus.Size = new System.Drawing.Size(166, 160);
			this.listStatus.TabIndex = 0;
			// 
			// textWorkPhone
			// 
			this.textWorkPhone.BackColor = System.Drawing.Color.White;
			this.textWorkPhone.Location = new System.Drawing.Point(431, 31);
			this.textWorkPhone.Name = "textWorkPhone";
			this.textWorkPhone.ReadOnly = true;
			this.textWorkPhone.TabIndex = 41;
			this.textWorkPhone.Text = "";
			this.textWorkPhone.TextChanged += new System.EventHandler(this.textWorkPhone_TextChanged);
			// 
			// textWireless
			// 
			this.textWireless.BackColor = System.Drawing.Color.White;
			this.textWireless.Location = new System.Drawing.Point(431, 51);
			this.textWireless.Name = "textWireless";
			this.textWireless.ReadOnly = true;
			this.textWireless.TabIndex = 40;
			this.textWireless.Text = "";
			this.textWireless.TextChanged += new System.EventHandler(this.textWireless_TextChanged);
			// 
			// textAddrNotes
			// 
			this.textAddrNotes.BackColor = System.Drawing.Color.White;
			this.textAddrNotes.Location = new System.Drawing.Point(431, 71);
			this.textAddrNotes.Multiline = true;
			this.textAddrNotes.Name = "textAddrNotes";
			this.textAddrNotes.ReadOnly = true;
			this.textAddrNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textAddrNotes.Size = new System.Drawing.Size(240, 48);
			this.textAddrNotes.TabIndex = 39;
			this.textAddrNotes.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(330, 33);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 16);
			this.label7.TabIndex = 38;
			this.label7.Text = "Work Phone";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(328, 53);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.TabIndex = 37;
			this.label2.Text = "Wireless Phone";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textHomePhone
			// 
			this.textHomePhone.BackColor = System.Drawing.Color.White;
			this.textHomePhone.Location = new System.Drawing.Point(431, 11);
			this.textHomePhone.Name = "textHomePhone";
			this.textHomePhone.ReadOnly = true;
			this.textHomePhone.TabIndex = 36;
			this.textHomePhone.Text = "";
			this.textHomePhone.TextChanged += new System.EventHandler(this.textHomePhone_TextChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(352, 13);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 16);
			this.label5.TabIndex = 35;
			this.label5.Text = "Home Phone";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(314, 72);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(118, 16);
			this.label4.TabIndex = 34;
			this.label4.Text = "Address/Phone Notes";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// contrAccount3
			// 
			this.contrAccount3.Location = new System.Drawing.Point(4, 282);
			this.contrAccount3.Name = "contrAccount3";
			this.contrAccount3.Size = new System.Drawing.Size(918, 378);
			this.contrAccount3.TabIndex = 53;
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(4, 280);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(909, 32);
			this.panel1.TabIndex = 54;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBillingType);
			this.groupBox1.Controls.Add(this.label14);
			this.groupBox1.Controls.Add(this.textDueDate);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.textPriIns);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.textAddrNotes);
			this.groupBox1.Controls.Add(this.textRecallInterval);
			this.groupBox1.Controls.Add(this.textCreditType);
			this.groupBox1.Controls.Add(this.textAge);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.textWireless);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textHomePhone);
			this.groupBox1.Controls.Add(this.textWorkPhone);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(216, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(676, 143);
			this.groupBox1.TabIndex = 55;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Patient Information";
			this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
			// 
			// textBillingType
			// 
			this.textBillingType.BackColor = System.Drawing.Color.White;
			this.textBillingType.Location = new System.Drawing.Point(110, 37);
			this.textBillingType.Name = "textBillingType";
			this.textBillingType.ReadOnly = true;
			this.textBillingType.Size = new System.Drawing.Size(120, 20);
			this.textBillingType.TabIndex = 54;
			this.textBillingType.Text = "";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(29, 40);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(79, 16);
			this.label14.TabIndex = 53;
			this.label14.Text = "Billing Type";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDueDate
			// 
			this.textDueDate.BackColor = System.Drawing.Color.White;
			this.textDueDate.Location = new System.Drawing.Point(110, 97);
			this.textDueDate.Name = "textDueDate";
			this.textDueDate.ReadOnly = true;
			this.textDueDate.Size = new System.Drawing.Size(76, 20);
			this.textDueDate.TabIndex = 52;
			this.textDueDate.Text = "";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(39, 100);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(70, 16);
			this.label12.TabIndex = 51;
			this.label12.Text = "Due Date";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPriIns
			// 
			this.textPriIns.BackColor = System.Drawing.Color.White;
			this.textPriIns.Location = new System.Drawing.Point(110, 117);
			this.textPriIns.Name = "textPriIns";
			this.textPriIns.ReadOnly = true;
			this.textPriIns.Size = new System.Drawing.Size(247, 20);
			this.textPriIns.TabIndex = 50;
			this.textPriIns.Text = "";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(9, 120);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(100, 16);
			this.label11.TabIndex = 49;
			this.label11.Text = "Primary Insurance";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textRecallInterval
			// 
			this.textRecallInterval.BackColor = System.Drawing.Color.White;
			this.textRecallInterval.Location = new System.Drawing.Point(110, 77);
			this.textRecallInterval.Name = "textRecallInterval";
			this.textRecallInterval.ReadOnly = true;
			this.textRecallInterval.Size = new System.Drawing.Size(32, 20);
			this.textRecallInterval.TabIndex = 47;
			this.textRecallInterval.Text = "";
			// 
			// textCreditType
			// 
			this.textCreditType.BackColor = System.Drawing.Color.White;
			this.textCreditType.Location = new System.Drawing.Point(110, 17);
			this.textCreditType.Name = "textCreditType";
			this.textCreditType.ReadOnly = true;
			this.textCreditType.Size = new System.Drawing.Size(23, 20);
			this.textCreditType.TabIndex = 46;
			this.textCreditType.Text = "";
			// 
			// textAge
			// 
			this.textAge.BackColor = System.Drawing.Color.White;
			this.textAge.Location = new System.Drawing.Point(110, 57);
			this.textAge.Name = "textAge";
			this.textAge.ReadOnly = true;
			this.textAge.Size = new System.Drawing.Size(32, 20);
			this.textAge.TabIndex = 48;
			this.textAge.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(29, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(79, 16);
			this.label3.TabIndex = 45;
			this.label3.Text = "Recall Interval";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(68, 60);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 16);
			this.label6.TabIndex = 44;
			this.label6.Text = "Age";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(43, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 16);
			this.label1.TabIndex = 42;
			this.label1.Text = "Credit Type";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label13
			// 
			this.label13.BackColor = System.Drawing.Color.White;
			this.label13.Location = new System.Drawing.Point(426, 349);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(203, 15);
			this.label13.TabIndex = 56;
			this.label13.Text = "(view only mode - no editing allowed)";
			// 
			// butPin
			// 
			this.butPin.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPin.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butPin.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butPin.Image = ((System.Drawing.Image)(resources.GetObject("butPin.Image")));
			this.butPin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPin.Location = new System.Drawing.Point(815, 186);
			this.butPin.Name = "butPin";
			this.butPin.Size = new System.Drawing.Size(98, 26);
			this.butPin.TabIndex = 57;
			this.butPin.Text = "&Pinboard";
			this.butPin.Click += new System.EventHandler(this.butPin_Click);
			// 
			// tbCommlog
			// 
			this.tbCommlog.BackColor = System.Drawing.SystemColors.Window;
			this.tbCommlog.Location = new System.Drawing.Point(184, 153);
			this.tbCommlog.Name = "tbCommlog";
			this.tbCommlog.ScrollValue = 700;
			this.tbCommlog.SelectedIndices = new int[0];
			this.tbCommlog.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbCommlog.Size = new System.Drawing.Size(619, 156);
			this.tbCommlog.TabIndex = 65;
			// 
			// butAddComm
			// 
			this.butAddComm.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddComm.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butAddComm.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butAddComm.Image = ((System.Drawing.Image)(resources.GetObject("butAddComm.Image")));
			this.butAddComm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddComm.Location = new System.Drawing.Point(87, 191);
			this.butAddComm.Name = "butAddComm";
			this.butAddComm.Size = new System.Drawing.Size(85, 26);
			this.butAddComm.TabIndex = 67;
			this.butAddComm.Text = "Co&mm";
			this.butAddComm.Click += new System.EventHandler(this.butAddComm_Click);
			// 
			// FormRecallEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(920, 663);
			this.Controls.Add(this.butAddComm);
			this.Controls.Add(this.tbCommlog);
			this.Controls.Add(this.butPin);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.listStatus);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.contrAccount3);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label10);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRecallEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Recall Status";
			this.Load += new System.EventHandler(this.FormRecallEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRecallEdit_Load(object sender, System.EventArgs e) {
			contrAccount3.InstantClasses();
			contrAccount3.ViewOnly=true;
			//for(int i=0;i<Defs.Short[(int)DefCat.ApptPhoneNotes].Length;i++){
			//	this.listQuickAdd.Items.Add(Defs.Short[(int)DefCat.ApptPhoneNotes][i].ItemName);
			//}
			for(int i=0;i<Defs.Short[(int)DefCat.RecallUnschedStatus].Length;i++){
				this.listStatus.Items.Add(Defs.Short[(int)DefCat.RecallUnschedStatus][i].ItemName);
				if(Defs.Short[(int)DefCat.RecallUnschedStatus][i].DefNum==FormRecall.Cur.RecallStatus)
					listStatus.SelectedIndex=i;
			}
			Patients.PatIsLoaded=true;
			Patients.Cur.PatNum=FormRecall.Cur.PatNum;
			contrAccount3.ModuleSelected();//also refreshes patient,procedures,claims,adjustments,
				//paysplits,insplans,covpats and patientnotes.
			Text="Recall for "+Patients.GetCurNameLF();
			//textCalls.Text=PatientNotes.Cur.ApptPhone;
			//textCalls.SelectionStart=textCalls.Text.Length+2;
			textCreditType.Text=Patients.Cur.CreditType;
			textBillingType.Text=Defs.GetName(DefCat.BillingTypes,Patients.Cur.BillingType);
			textAge.Text=Shared.DateToAge(Patients.Cur.Birthdate);
			textRecallInterval.Text=Patients.Cur.RecallInterval.ToString();
			textDueDate.Text=DueDate.ToString("d");
			textPriIns.Text=InsPlans.GetDescript(Patients.Cur.PriPlanNum);
      textHomePhone.Text=Patients.Cur.HmPhone;
			textWorkPhone.Text=Patients.Cur.WkPhone;
			textWireless.Text=Patients.Cur.WirelessPhone;
			textAddrNotes.Text=Patients.Cur.AddrNote;	
			FillComm();
		}

		/// <summary>Fills the commlog table on this form.</summary>
		private void FillComm(){
			Commlogs.Refresh();
			ALCommItems=new ArrayList();
			for(int i=0;i<Commlogs.List.Length;i++){
				if(Commlogs.List[i].CommType==CommItemType.AppointmentScheduling){
					ALCommItems.Add(Commlogs.List[i]);
				}
			}
			tbCommlog.ResetRows(ALCommItems.Count);
			for(int i=0;i<ALCommItems.Count;i++){
				tbCommlog.Cell[0,i]=((Commlog)ALCommItems[i]).CommDate.ToShortDateString();
				tbCommlog.Cell[1,i]=((Commlog)ALCommItems[i]).Note;
			}
			tbCommlog.SetGridColor(Color.Gray);
			tbCommlog.LayoutTables();
		}

		private void tbCommlog_CellDoubleClicked(object sender, CellEventArgs e){
			Commlogs.Cur=(Commlog)ALCommItems[e.Row];
			FormCommItem FormCI=new FormCommItem();
			FormCI.ShowDialog();
			FillComm();
		}		

		/*private void listQuickAdd_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
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
		}*/

		/*private void butDateLine_Click(object sender, System.EventArgs e) {
			int caret=textCalls.SelectionStart;
			string strPaste;
			strPaste="**"+DateTime.Today.ToString("d")+"**";
			textCalls.Text=textCalls.Text.Insert(caret,strPaste);
			textCalls.Select();
			textCalls.SelectionStart=caret+strPaste.Length;
			textCalls.SelectionLength=0;
		}*/

		private void SaveChanges(){
			if(listStatus.SelectedIndex!=-1){
				Patients.Cur.RecallStatus=Defs.Short[(int)DefCat.RecallUnschedStatus][listStatus.SelectedIndex].DefNum;
				Patients.UpdateCur();
			}
			//PatientNotes.Cur.ApptPhone=textCalls.Text;
			PatientNotes.UpdateCur();
		}

		private void butOK_Click(object sender, System.EventArgs e){
			SaveChanges();
			//?Patients.PatIsLoaded=false;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e){
			DialogResult=DialogResult.Cancel;
		}

		private void groupBox1_Enter(object sender, System.EventArgs e){
		
		}

		private void butAddComm_Click(object sender, System.EventArgs e) {
			Commlogs.Cur=new Commlog();
			Commlogs.Cur.PatNum=Patients.Cur.PatNum;
			Commlogs.Cur.CommDate=DateTime.Today;
			Commlogs.Cur.CommType=CommItemType.AppointmentScheduling;
			FormCommItem FormCI=new FormCommItem();
			FormCI.IsNew=true;
			FormCI.ShowDialog();
			FillComm();
		}

		private void CreateCurInfo(){
			ContrAppt.CurInfo=new InfoApt();
			Appointments.Cur=new Appointment();
			Appointments.Cur.PatNum=Patients.Cur.PatNum;
			Appointments.Cur.AptStatus=ApptStatus.Scheduled;
			Appointments.Cur.Pattern=((Pref)Prefs.HList["RecallPattern"]).ValueString;
			Appointments.Cur.Note="";
			if(Patients.Cur.PriProv==0)
				Appointments.Cur.ProvNum=PIn.PInt(((Pref)Prefs.HList["PracticeDefaultProv"]).ValueString);
			else
				Appointments.Cur.ProvNum=Patients.Cur.PriProv;
			Appointments.Cur.ProvHyg=Patients.Cur.SecProv;
			Appointments.InsertCur();
      string[] procs=((Pref)Prefs.HList["RecallProcedures"]).ValueString.Split(',');
			if(((Pref)Prefs.HList["RecallBW"]).ValueString!=""){//BWs
				bool dueBW=true;
				for(int i=0;i<Procedures.List.Length;i++){
					if(((Pref)Prefs.HList["RecallBW"]).ValueString==Procedures.List[i].ADACode
						&& Procedures.List[i].ProcDate > DueDate.AddYears(-1)){
						dueBW=false;
					}
				}
				if(dueBW){
					string[] procs2=new string[procs.Length+1];
					procs.CopyTo(procs2,0);
					procs2[procs2.Length-1]=((Pref)Prefs.HList["RecallBW"]).ValueString;
					procs=new string[procs2.Length];
					procs2.CopyTo(procs,0);
				}
			}
			for(int i=0;i<procs.Length;i++){
				Procedures.Cur=new Procedure();
				//procnum
				Procedures.Cur.PatNum=Patients.Cur.PatNum;
				Procedures.Cur.AptNum=Appointments.Cur.AptNum;
				Procedures.Cur.ADACode=procs[i];
				Procedures.Cur.ProcDate=DateTime.Now;
				Procedures.Cur.ProcFee=Fees.GetAmount(Procedures.Cur.ADACode,ContrChart.GetFeeSched());
				Procedures.Cur.OverridePri=-1;
				Procedures.Cur.OverrideSec=-1;
				//surf
				//toothnum
				//Procedures.Cur.ToothRange="";
				Procedures.Cur.NoBillIns=ProcedureCodes.GetProcCode(Procedures.Cur.ADACode).NoBillIns;
				//priority
				Procedures.Cur.ProcStatus=ProcStat.TP;
				Procedures.Cur.ProcNote="";
				//Procedures.Cur.PriEstim=
				//Procedures.Cur.SecEstim=
				//claimnum
				Procedures.Cur.ProvNum=Patients.Cur.PriProv;
				//Procedures.Cur.Dx=
				//nextaptnum
				if(Patients.Cur.PriPlanNum!=0){//if patient has insurance
					Procedures.Cur.IsCovIns=true;
				}
				Procedures.InsertCur();
			}
			ContrAppt.CurInfo.MyApt=Appointments.Cur;
			ContrAppt.CurInfo.CreditAndIns=Patients.GetCreditIns();
			ContrAppt.CurInfo.PatientName=Patients.GetCurNameLF();
			Procedures.GetProcsForSingle(Appointments.Cur.AptNum,false);
			ContrAppt.CurInfo.Procs=Procedures.ProcsForSingle;
		}

		private void butPin_Click(object sender, System.EventArgs e) {
			PinClicked=true;
			SaveChanges();
			CreateCurInfo();
			DialogResult=DialogResult.OK;
		}

		private void textWireless_TextChanged(object sender, System.EventArgs e) {
			textWireless.Text=TelephoneNumbers.AutoFormat(textWireless.Text);
			textWireless.SelectionStart=textWireless.Text.Length;
		}

		private void textWorkPhone_TextChanged(object sender, System.EventArgs e) {
			textWorkPhone.Text=TelephoneNumbers.AutoFormat(textWorkPhone.Text);
			textWorkPhone.SelectionStart=textWorkPhone.Text.Length;
		}

		private void textHomePhone_TextChanged(object sender, System.EventArgs e) {
			textHomePhone.Text=TelephoneNumbers.AutoFormat(textHomePhone.Text);
			textHomePhone.SelectionStart=textHomePhone.Text.Length;	
		}

		






	}
}
