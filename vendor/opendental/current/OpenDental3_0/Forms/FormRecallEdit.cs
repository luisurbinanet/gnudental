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
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox textCreditType;
		private System.Windows.Forms.TextBox textAge;
		private System.Windows.Forms.TextBox textPriIns;
		private System.Windows.Forms.TextBox textDueDate;
		private PatientNotes PatientNotes=new PatientNotes();
		///<summary>If Pin clicked, this allows FormRecall to know about it.</summary>
		public bool PinClicked=false;
		///<summary></summary>
		public DateTime DueDate;
		private System.Windows.Forms.TextBox textBillingType;
		private OpenDental.XPButton butPin;
		private System.Windows.Forms.Label label14;
		private ArrayList ALCommItems;

		///<summary></summary>
		public FormRecallEdit(){
			InitializeComponent();
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
			this.textCreditType = new System.Windows.Forms.TextBox();
			this.textAge = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.butPin = new OpenDental.XPButton();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(852, 644);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 6;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(768, 644);
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
			this.listStatus.Size = new System.Drawing.Size(166, 173);
			this.listStatus.TabIndex = 0;
			// 
			// textWorkPhone
			// 
			this.textWorkPhone.BackColor = System.Drawing.Color.White;
			this.textWorkPhone.Location = new System.Drawing.Point(496, 33);
			this.textWorkPhone.Name = "textWorkPhone";
			this.textWorkPhone.ReadOnly = true;
			this.textWorkPhone.TabIndex = 41;
			this.textWorkPhone.Text = "";
			// 
			// textWireless
			// 
			this.textWireless.BackColor = System.Drawing.Color.White;
			this.textWireless.Location = new System.Drawing.Point(496, 53);
			this.textWireless.Name = "textWireless";
			this.textWireless.ReadOnly = true;
			this.textWireless.TabIndex = 40;
			this.textWireless.Text = "";
			// 
			// textAddrNotes
			// 
			this.textAddrNotes.BackColor = System.Drawing.Color.White;
			this.textAddrNotes.Location = new System.Drawing.Point(496, 73);
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
			this.label7.Location = new System.Drawing.Point(395, 35);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 16);
			this.label7.TabIndex = 38;
			this.label7.Text = "Work Phone";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(393, 55);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.TabIndex = 37;
			this.label2.Text = "Wireless Phone";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textHomePhone
			// 
			this.textHomePhone.BackColor = System.Drawing.Color.White;
			this.textHomePhone.Location = new System.Drawing.Point(496, 13);
			this.textHomePhone.Name = "textHomePhone";
			this.textHomePhone.ReadOnly = true;
			this.textHomePhone.TabIndex = 36;
			this.textHomePhone.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(417, 15);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 16);
			this.label5.TabIndex = 35;
			this.label5.Text = "Home Phone";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(379, 74);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(118, 16);
			this.label4.TabIndex = 34;
			this.label4.Text = "Address/Phone Notes";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// contrAccount3
			// 
			this.contrAccount3.Location = new System.Drawing.Point(4, 164);
			this.contrAccount3.Name = "contrAccount3";
			this.contrAccount3.Size = new System.Drawing.Size(931, 477);
			this.contrAccount3.TabIndex = 53;
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(6, 162);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(929, 32);
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
			this.groupBox1.Controls.Add(this.textCreditType);
			this.groupBox1.Controls.Add(this.textAge);
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
			this.groupBox1.Location = new System.Drawing.Point(182, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(749, 187);
			this.groupBox1.TabIndex = 55;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Patient Information";
			// 
			// textBillingType
			// 
			this.textBillingType.BackColor = System.Drawing.Color.White;
			this.textBillingType.Location = new System.Drawing.Point(496, 141);
			this.textBillingType.Name = "textBillingType";
			this.textBillingType.ReadOnly = true;
			this.textBillingType.Size = new System.Drawing.Size(120, 20);
			this.textBillingType.TabIndex = 54;
			this.textBillingType.Text = "";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(415, 144);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(79, 16);
			this.label14.TabIndex = 53;
			this.label14.Text = "Billing Type";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDueDate
			// 
			this.textDueDate.BackColor = System.Drawing.Color.White;
			this.textDueDate.Location = new System.Drawing.Point(18, 32);
			this.textDueDate.Name = "textDueDate";
			this.textDueDate.ReadOnly = true;
			this.textDueDate.Size = new System.Drawing.Size(76, 20);
			this.textDueDate.TabIndex = 52;
			this.textDueDate.Text = "";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(15, 18);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(70, 16);
			this.label12.TabIndex = 51;
			this.label12.Text = "Due Date";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPriIns
			// 
			this.textPriIns.BackColor = System.Drawing.Color.White;
			this.textPriIns.Location = new System.Drawing.Point(496, 161);
			this.textPriIns.Name = "textPriIns";
			this.textPriIns.ReadOnly = true;
			this.textPriIns.Size = new System.Drawing.Size(247, 20);
			this.textPriIns.TabIndex = 50;
			this.textPriIns.Text = "";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(395, 164);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(100, 16);
			this.label11.TabIndex = 49;
			this.label11.Text = "Primary Insurance";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCreditType
			// 
			this.textCreditType.BackColor = System.Drawing.Color.White;
			this.textCreditType.Location = new System.Drawing.Point(496, 121);
			this.textCreditType.Name = "textCreditType";
			this.textCreditType.ReadOnly = true;
			this.textCreditType.Size = new System.Drawing.Size(23, 20);
			this.textCreditType.TabIndex = 46;
			this.textCreditType.Text = "";
			// 
			// textAge
			// 
			this.textAge.BackColor = System.Drawing.Color.White;
			this.textAge.Location = new System.Drawing.Point(208, 33);
			this.textAge.Name = "textAge";
			this.textAge.ReadOnly = true;
			this.textAge.Size = new System.Drawing.Size(32, 20);
			this.textAge.TabIndex = 48;
			this.textAge.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(197, 18);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 16);
			this.label6.TabIndex = 44;
			this.label6.Text = "Age";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(429, 123);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 16);
			this.label1.TabIndex = 42;
			this.label1.Text = "Credit Type";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label13
			// 
			this.label13.BackColor = System.Drawing.Color.White;
			this.label13.Location = new System.Drawing.Point(474, 231);
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
			this.butPin.Location = new System.Drawing.Point(574, 644);
			this.butPin.Name = "butPin";
			this.butPin.Size = new System.Drawing.Size(98, 26);
			this.butPin.TabIndex = 57;
			this.butPin.Text = "&Pinboard";
			this.butPin.Click += new System.EventHandler(this.butPin_Click);
			// 
			// FormRecallEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(937, 677);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butPin);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.listStatus);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.contrAccount3);
			this.Controls.Add(this.label10);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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
			contrAccount3.ViewingInRecall=true;
			contrAccount3.InstantClasses();
			listStatus.Items.Add(Lan.g(this,"None"));
			listStatus.SelectedIndex=0;
			for(int i=0;i<Defs.Short[(int)DefCat.RecallUnschedStatus].Length;i++){
				listStatus.Items.Add(Defs.Short[(int)DefCat.RecallUnschedStatus][i].ItemName);
				if(Defs.Short[(int)DefCat.RecallUnschedStatus][i].DefNum==FormRecall.Cur.RecallStatus)
					listStatus.SelectedIndex=i+1;
			}
			Patients.PatIsLoaded=true;
			Patient PatCur=Patients.Cur;
			PatCur.PatNum=FormRecall.Cur.PatNum;
			Patients.Cur=PatCur;
			contrAccount3.ModuleSelected();//also refreshes patient,procedures,claims,adjustments,
				//paysplits,insplans,covpats and patientnotes.
			Text="Recall for "+Patients.GetCurNameLF();
			textCreditType.Text=Patients.Cur.CreditType;
			textBillingType.Text=Defs.GetName(DefCat.BillingTypes,Patients.Cur.BillingType);
			textAge.Text=Shared.DateToAge(Patients.Cur.Birthdate);
			textDueDate.Text=DueDate.ToString("d");
			textPriIns.Text=InsPlans.GetDescript(Patients.Cur.PriPlanNum);
      textHomePhone.Text=Patients.Cur.HmPhone;
			textWorkPhone.Text=Patients.Cur.WkPhone;
			textWireless.Text=Patients.Cur.WirelessPhone;
			textAddrNotes.Text=Patients.Cur.AddrNote;	
		}

		/// <summary>Creates appointment and appropriate procedures, and places data in ContrAppt.CurInfo so it will display on pinboard.</summary>
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
			Appointments.CurOld=Appointments.Cur;
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
			Procedure ProcCur;
			for(int i=0;i<procs.Length;i++){
				ProcCur=new Procedure();//this will be an insert
				//procnum
				ProcCur.PatNum=Patients.Cur.PatNum;
				ProcCur.AptNum=Appointments.Cur.AptNum;
				ProcCur.ADACode=procs[i];
				ProcCur.ProcDate=DateTime.Now;
				ProcCur.ProcFee=Fees.GetAmount(ProcCur.ADACode,ContrChart.GetFeeSched());
				ProcCur.OverridePri=-1;
				ProcCur.OverrideSec=-1;
				//surf
				//toothnum
				//Procedures.Cur.ToothRange="";
				ProcCur.NoBillIns=ProcedureCodes.GetProcCode(ProcCur.ADACode).NoBillIns;
				//priority
				ProcCur.ProcStatus=ProcStat.TP;
				ProcCur.ProcNote="";
				//Procedures.Cur.PriEstim=
				//Procedures.Cur.SecEstim=
				//claimnum
				ProcCur.ProvNum=Patients.Cur.PriProv;
				//Procedures.Cur.Dx=
				//nextaptnum
				if(Patients.Cur.PriPlanNum!=0){//if patient has insurance
					ProcCur.IsCovIns=true;
				}
				Procedures.Cur=ProcCur;
				Procedures.InsertCur();
			}
			ContrAppt.CurInfo.MyApt=Appointments.Cur;
			ContrAppt.CurInfo.CreditAndIns=Patients.GetCreditIns();
			ContrAppt.CurInfo.PatientName=Patients.GetCurNameLF();
			Procedures.GetProcsForSingle(Appointments.Cur.AptNum,false);
			ContrAppt.CurInfo.Procs=Procedures.ProcsForSingle;
		}

		private void SaveStatus(){
			int newStatus;
			if(listStatus.SelectedIndex==0){
				newStatus=0;
			}
			else{
				newStatus=Defs.Short[(int)DefCat.RecallUnschedStatus][listStatus.SelectedIndex-1].DefNum;
			}
			//if the status has changed, but no recall commlog entry for today, then make an auto entry.
			if(newStatus!=Patients.Cur.RecallStatus){
				bool recallEntryToday=false;
				for(int i=0;i<Commlogs.List.Length;i++){
					if(Commlogs.List[i].CommDate==DateTime.Today
						&& Commlogs.List[i].CommType==CommItemType.Recall){
						recallEntryToday=true;
					}
				}
				if(!recallEntryToday){
					Commlogs.Cur=new Commlog();
					Commlogs.Cur.CommDate=DateTime.Today;
					Commlogs.Cur.CommType=CommItemType.Recall;
					Commlogs.Cur.PatNum=Patients.Cur.PatNum;
					Commlogs.Cur.Note=Lan.g(this,"Status changed to")+" ";
					if(newStatus==0)
						Commlogs.Cur.Note+=Lan.g(this,"None");
					else
						Commlogs.Cur.Note+=Defs.GetName(DefCat.RecallUnschedStatus,newStatus);
					Commlogs.Cur.Note+=".  ";
					FormCommItem FormCI=new FormCommItem();
					FormCI.IsNew=true;
					//forces user to at least consider a commlog entry
					FormCI.ShowDialog();//typically saved in this window.
				}
			}
			Patient PatCur=Patients.Cur;
			PatCur.RecallStatus=newStatus;
			Patients.Cur=PatCur;
			Patients.UpdateCur();
		}

		private void butPin_Click(object sender, System.EventArgs e) {
			SaveStatus();
			PinClicked=true;
			CreateCurInfo();
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e){
			SaveStatus();
			//?Patients.PatIsLoaded=false;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e){
			DialogResult=DialogResult.Cancel;
		}






	}
}
