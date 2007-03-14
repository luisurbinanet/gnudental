using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace OpenDental{

	public class FormApptEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Label labelPatient;
		private System.Windows.Forms.TextBox textTime;
		private System.Windows.Forms.Button butCalcTime;
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ListBox listProvNum;
		private System.Windows.Forms.ListBox listProvHyg;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ListBox listConfirmed;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button butHygClear;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox textBox7;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.ListBox listQuickAdd;
		private OpenDental.TableApptProcs tbProc;// Required designer variable.
		public bool IsNew;
		private Procedure[] arrayProc;
		private ApptProc[] ApptProc2;
		private OpenDental.ValidNum textAddTime;
		private OpenDental.TableTimeBar tbTime;
		private System.Windows.Forms.Button butSlider;
		private bool mouseIsDown;
		private Point	mouseOrigin;
		private Point sliderOrigin;
		private StringBuilder strBTime;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioLabNone;
		private System.Windows.Forms.RadioButton radioLabReceived;
		private System.Windows.Forms.RadioButton radioLabSent;
		public bool IsNext=false;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.ListBox listStatus;
		private bool procsHaveChanged;

		public FormApptEdit(){
			InitializeComponent();// Required for Windows Form Designer support
			tbTime.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbTime_CellClicked);
			tbProc.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbProc_CellClicked);
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label1,
				this.label2,
				this.label3,
				this.label4,
				this.label5,
				this.label6,
				this.label10,
				this.label11,
				this.label12,
				this.label13,
				this.label16,
				this.label7,
				this.label8,
				this.label9,
				this.labelPatient,
				this.butCalcTime,
				this.butHygClear,
				this.butSlider,
				this.radioLabNone,
				this.radioLabReceived,
				this.radioLabSent,
				this.panel1,
				this.groupBox1,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
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
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.labelPatient = new System.Windows.Forms.Label();
			this.listQuickAdd = new System.Windows.Forms.ListBox();
			this.tbTime = new OpenDental.TableTimeBar();
			this.textTime = new System.Windows.Forms.TextBox();
			this.butCalcTime = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.listProvNum = new System.Windows.Forms.ListBox();
			this.listProvHyg = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.butHygClear = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.listConfirmed = new System.Windows.Forms.ListBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.tbProc = new OpenDental.TableApptProcs();
			this.panel1 = new System.Windows.Forms.Panel();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.textBox7 = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.textAddTime = new OpenDental.ValidNum();
			this.butSlider = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioLabSent = new System.Windows.Forms.RadioButton();
			this.radioLabReceived = new System.Windows.Forms.RadioButton();
			this.radioLabNone = new System.Windows.Forms.RadioButton();
			this.label8 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.listStatus = new System.Windows.Forms.ListBox();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(770, 558);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 7;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(771, 596);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 8;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// labelPatient
			// 
			this.labelPatient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelPatient.Location = new System.Drawing.Point(10, 7);
			this.labelPatient.Name = "labelPatient";
			this.labelPatient.Size = new System.Drawing.Size(220, 23);
			this.labelPatient.TabIndex = 2;
			this.labelPatient.Text = "Patient Name";
			// 
			// listQuickAdd
			// 
			this.listQuickAdd.Location = new System.Drawing.Point(155, 73);
			this.listQuickAdd.Name = "listQuickAdd";
			this.listQuickAdd.Size = new System.Drawing.Size(146, 381);
			this.listQuickAdd.TabIndex = 3;
			this.listQuickAdd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listQuickAdd_MouseDown);
			this.listQuickAdd.SelectedIndexChanged += new System.EventHandler(this.listQuickAdd_SelectedIndexChanged);
			// 
			// tbTime
			// 
			this.tbTime.BackColor = System.Drawing.SystemColors.Window;
			this.tbTime.Location = new System.Drawing.Point(7, 40);
			this.tbTime.Name = "tbTime";
			this.tbTime.SelectedIndices = new int[0];
			this.tbTime.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbTime.Size = new System.Drawing.Size(15, 561);
			this.tbTime.TabIndex = 4;
			// 
			// textTime
			// 
			this.textTime.BackColor = System.Drawing.Color.White;
			this.textTime.Location = new System.Drawing.Point(40, 627);
			this.textTime.Name = "textTime";
			this.textTime.ReadOnly = true;
			this.textTime.Size = new System.Drawing.Size(66, 20);
			this.textTime.TabIndex = 5;
			this.textTime.Text = "";
			// 
			// butCalcTime
			// 
			this.butCalcTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCalcTime.Location = new System.Drawing.Point(41, 597);
			this.butCalcTime.Name = "butCalcTime";
			this.butCalcTime.TabIndex = 6;
			this.butCalcTime.Text = "Calc Time";
			this.butCalcTime.Click += new System.EventHandler(this.butCalcTime_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(142, 630);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 14);
			this.label1.TabIndex = 7;
			this.label1.Text = "Adj Time:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listProvNum
			// 
			this.listProvNum.Location = new System.Drawing.Point(684, 37);
			this.listProvNum.Name = "listProvNum";
			this.listProvNum.Size = new System.Drawing.Size(82, 186);
			this.listProvNum.TabIndex = 4;
			// 
			// listProvHyg
			// 
			this.listProvHyg.Location = new System.Drawing.Point(781, 37);
			this.listProvHyg.Name = "listProvHyg";
			this.listProvHyg.Size = new System.Drawing.Size(82, 186);
			this.listProvHyg.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(684, 19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 16);
			this.label2.TabIndex = 11;
			this.label2.Text = "Provider";
			// 
			// butHygClear
			// 
			this.butHygClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butHygClear.Location = new System.Drawing.Point(785, 227);
			this.butHygClear.Name = "butHygClear";
			this.butHygClear.TabIndex = 12;
			this.butHygClear.Text = "clear";
			this.butHygClear.Click += new System.EventHandler(this.butHygClear_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(780, 19);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(98, 16);
			this.label3.TabIndex = 13;
			this.label3.Text = "Hygiene Provider";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(28, 51);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 18);
			this.label4.TabIndex = 15;
			this.label4.Text = "Status";
			// 
			// listConfirmed
			// 
			this.listConfirmed.Location = new System.Drawing.Point(28, 191);
			this.listConfirmed.Name = "listConfirmed";
			this.listConfirmed.Size = new System.Drawing.Size(121, 186);
			this.listConfirmed.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(28, 171);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 16);
			this.label5.TabIndex = 17;
			this.label5.Text = "Confirmed";
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(30, 468);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(258, 114);
			this.textNote.TabIndex = 2;
			this.textNote.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(32, 449);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(62, 16);
			this.label6.TabIndex = 20;
			this.label6.Text = "Note";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(153, 32);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(143, 39);
			this.label7.TabIndex = 21;
			this.label7.Text = "Single click on items in this list to add them to the treatment plan.";
			// 
			// tbProc
			// 
			this.tbProc.BackColor = System.Drawing.SystemColors.Window;
			this.tbProc.Location = new System.Drawing.Point(312, 41);
			this.tbProc.Name = "tbProc";
			this.tbProc.SelectedIndices = new int[0];
			this.tbProc.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbProc.Size = new System.Drawing.Size(364, 612);
			this.tbProc.TabIndex = 22;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.textBox1);
			this.panel1.Controls.Add(this.label16);
			this.panel1.Controls.Add(this.textBox5);
			this.panel1.Controls.Add(this.label11);
			this.panel1.Controls.Add(this.textBox7);
			this.panel1.Controls.Add(this.label12);
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.textBox3);
			this.panel1.Controls.Add(this.textBox6);
			this.panel1.Controls.Add(this.textBox2);
			this.panel1.Controls.Add(this.label10);
			this.panel1.Controls.Add(this.label13);
			this.panel1.Location = new System.Drawing.Point(702, 398);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(108, 147);
			this.panel1.TabIndex = 56;
			this.panel1.Visible = false;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(62, 116);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(40, 20);
			this.textBox1.TabIndex = 58;
			this.textBox1.Text = "85";
			this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(3, 118);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(64, 14);
			this.label16.TabIndex = 59;
			this.label16.Text = "(Acct Bal ):";
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(62, 49);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(40, 20);
			this.textBox5.TabIndex = 54;
			this.textBox5.Text = "40";
			this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(12, 51);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(54, 12);
			this.label11.TabIndex = 52;
			this.label11.Text = "+Deduct:";
			// 
			// textBox7
			// 
			this.textBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBox7.Location = new System.Drawing.Point(62, 90);
			this.textBox7.Name = "textBox7";
			this.textBox7.Size = new System.Drawing.Size(40, 20);
			this.textBox7.TabIndex = 57;
			this.textBox7.Text = "755";
			this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label12
			// 
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label12.Location = new System.Drawing.Point(1, 93);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(58, 12);
			this.label12.TabIndex = 56;
			this.label12.Text = "This Appt:";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(16, 14);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(30, 12);
			this.label9.TabIndex = 46;
			this.label9.Text = "Total";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(62, 28);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(40, 20);
			this.textBox3.TabIndex = 50;
			this.textBox3.Text = "160";
			this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBox6
			// 
			this.textBox6.Location = new System.Drawing.Point(62, 69);
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(40, 20);
			this.textBox6.TabIndex = 55;
			this.textBox6.Text = "200";
			this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(20, 28);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(40, 20);
			this.textBox2.TabIndex = 49;
			this.textBox2.Text = "500";
			this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(60, 2);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(40, 24);
			this.label10.TabIndex = 47;
			this.label10.Text = "Patient Port.";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(2, 71);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(64, 14);
			this.label13.TabIndex = 48;
			this.label13.Text = "+Over Max:";
			// 
			// textAddTime
			// 
			this.textAddTime.Location = new System.Drawing.Point(200, 626);
			this.textAddTime.Name = "textAddTime";
			this.textAddTime.Size = new System.Drawing.Size(65, 20);
			this.textAddTime.TabIndex = 3;
			this.textAddTime.Text = "";
			// 
			// butSlider
			// 
			this.butSlider.BackColor = System.Drawing.SystemColors.ControlDark;
			this.butSlider.Location = new System.Drawing.Point(9, 124);
			this.butSlider.Name = "butSlider";
			this.butSlider.Size = new System.Drawing.Size(12, 15);
			this.butSlider.TabIndex = 58;
			this.butSlider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.butSlider_MouseUp);
			this.butSlider.MouseMove += new System.Windows.Forms.MouseEventHandler(this.butSlider_MouseMove);
			this.butSlider.MouseDown += new System.Windows.Forms.MouseEventHandler(this.butSlider_MouseDown);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioLabSent);
			this.groupBox1.Controls.Add(this.radioLabReceived);
			this.groupBox1.Controls.Add(this.radioLabNone);
			this.groupBox1.Location = new System.Drawing.Point(719, 303);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(105, 91);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Lab Case";
			// 
			// radioLabSent
			// 
			this.radioLabSent.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioLabSent.Location = new System.Drawing.Point(12, 39);
			this.radioLabSent.Name = "radioLabSent";
			this.radioLabSent.Size = new System.Drawing.Size(49, 24);
			this.radioLabSent.TabIndex = 2;
			this.radioLabSent.Text = "Sent";
			this.radioLabSent.Click += new System.EventHandler(this.radioLabSent_Click);
			// 
			// radioLabReceived
			// 
			this.radioLabReceived.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioLabReceived.Location = new System.Drawing.Point(12, 63);
			this.radioLabReceived.Name = "radioLabReceived";
			this.radioLabReceived.Size = new System.Drawing.Size(69, 24);
			this.radioLabReceived.TabIndex = 1;
			this.radioLabReceived.Text = "Received";
			this.radioLabReceived.Click += new System.EventHandler(this.radioLabReceived_Click);
			// 
			// radioLabNone
			// 
			this.radioLabNone.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioLabNone.Location = new System.Drawing.Point(12, 15);
			this.radioLabNone.Name = "radioLabNone";
			this.radioLabNone.Size = new System.Drawing.Size(52, 24);
			this.radioLabNone.TabIndex = 0;
			this.radioLabNone.Text = "None";
			this.radioLabNone.Click += new System.EventHandler(this.radioLabNone_Click);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(771, 629);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(107, 32);
			this.label8.TabIndex = 60;
			this.label8.Text = "Does not cancel Procedure edits.";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(312, 9);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(345, 29);
			this.label14.TabIndex = 61;
			this.label14.Text = "From the treatment plan list below, highlight the procedures to attach to this ap" +
				"pointment";
			// 
			// listStatus
			// 
			this.listStatus.Location = new System.Drawing.Point(28, 69);
			this.listStatus.Name = "listStatus";
			this.listStatus.Size = new System.Drawing.Size(90, 69);
			this.listStatus.TabIndex = 62;
			// 
			// FormApptEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(891, 669);
			this.Controls.Add(this.listStatus);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butSlider);
			this.Controls.Add(this.textAddTime);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.textTime);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.tbProc);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.listConfirmed);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.butHygClear);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listProvHyg);
			this.Controls.Add(this.listProvNum);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butCalcTime);
			this.Controls.Add(this.tbTime);
			this.Controls.Add(this.listQuickAdd);
			this.Controls.Add(this.labelPatient);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormApptEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Appointment";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormApptEdit_Closing);
			this.Load += new System.EventHandler(this.FormApptEdit_Load);
			this.panel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormApptEdit_Load(object sender, System.EventArgs e){
			InsPlans.Refresh();
			if(IsNext) Text=Lan.g(this,"Edit Next Appointment");
			if(IsNew){
				//for this, the appointment has to be created somewhere else first.
				//It might only be temporary, and will be deleted if Cancel is clicked.
				//Appointments.Cur=new Appointment();//handled  previously
				//Appointments.SaveApt();
				if(Appointments.Cur.Confirmed==0)
					Appointments.Cur.Confirmed=Defs.Short[(int)DefCat.ApptConfirmed][0].DefNum;
				if(Appointments.Cur.ProvNum==0)
					Appointments.Cur.ProvNum=Providers.List[0].ProvNum;
			}
			labelPatient.Text=Patients.GetCurNameLF();
			for(int i=1;i<Enum.GetNames(typeof(ApptStatus)).Length;i++){//none status is not displayed
				listStatus.Items.Add(Enum.GetNames(typeof(ApptStatus))[i]);
			}
			listStatus.SelectedIndex=(int)Appointments.Cur.AptStatus-1;
			strBTime=new StringBuilder(Appointments.Cur.Pattern);
			for(int i=0;i<Defs.Short[(int)DefCat.ApptConfirmed].Length;i++){
				listConfirmed.Items.Add(Defs.Short[(int)DefCat.ApptConfirmed][i].ItemName);
				if(Defs.Short[(int)DefCat.ApptConfirmed][i].DefNum==Appointments.Cur.Confirmed)
					listConfirmed.SelectedIndex=i;
			}
			textAddTime.MinVal=-1200;
			textAddTime.MaxVal=1200;
			textAddTime.Text=POut.PInt(Appointments.Cur.AddTime*10);
			textNote.Text=Appointments.Cur.Note;
			for(int i=0;i<Defs.Short[(int)DefCat.ApptProcsQuickAdd].Length;i++){
				listQuickAdd.Items.Add(Defs.Short[(int)DefCat.ApptProcsQuickAdd][i].ItemName);
			}
			for(int i=0;i<Providers.List.Length;i++){
				listProvNum.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==Appointments.Cur.ProvNum)
					listProvNum.SelectedIndex=i;
			}
			for(int i=0;i<Providers.List.Length;i++){
				listProvHyg.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==Appointments.Cur.ProvHyg)
					listProvHyg.SelectedIndex=i;
			}
			switch(Appointments.Cur.Lab){
				case LabCase.None:
					radioLabNone.Checked=true;
					break;
				case LabCase.Sent:
					radioLabSent.Checked=true;
					break;
				case LabCase.Received:
					radioLabReceived.Checked=true;
					break;
			}
			FillProcedures();
			FillTime();
		}

		private void FillProcedures(){
			Procedures.Refresh();
			arrayProc = new Procedure[Procedures.List.Length];
			int countLine=0;
			//step through all procedures for patient and move selected ones to
			//arrayProc array as intermediate, then to ApptProc2 array for display
			if(IsNext){
				for (int i=0;i<Procedures.List.Length;i++){
					if(Procedures.List[i].NextAptNum==Appointments.Cur.AptNum){
						arrayProc[countLine]=Procedures.List[i];
						countLine++;
					}
					else if(Procedures.List[i].ProcStatus==ProcStat.TP){
						arrayProc[countLine]=Procedures.List[i];
						countLine++;
					}
				}
			}
			else{//standard appt
				for (int i=0;i<Procedures.List.Length;i++){
					if(Procedures.List[i].AptNum==Appointments.Cur.AptNum){
						arrayProc[countLine]=Procedures.List[i];
						countLine++;
					}
					else if(Procedures.List[i].AptNum!=0){
						//attached to another appt so don't show
					}
					else if(Procedures.List[i].ProcStatus==ProcStat.TP){
						arrayProc[countLine]=Procedures.List[i];
						countLine++;
					}
      		else if(Procedures.List[i].ProcStatus==ProcStat.C
						&& Procedures.List[i].ProcDate.Date==Appointments.Cur.AptDateTime.Date){
						arrayProc[countLine]=Procedures.List[i];
						countLine++;
					}
				}
			}
			//This is where to convert arrayProc to ApptProc2:
			ApptProc2 = new ApptProc[countLine];
			for (int i=0;i<ApptProc2.Length;i++){
				ApptProc2[i].Index=i;
				switch (arrayProc[i].ProcStatus){
					case ProcStat.TP: ApptProc2[i].Status="TP"; break;
					case ProcStat.C: ApptProc2[i].Status="C"; break;
					//very rare, but possible:
					case ProcStat.EC: ApptProc2[i].Status="EC"; break;
					case ProcStat.EO: ApptProc2[i].Status="EO"; break;
					case ProcStat.R: ApptProc2[i].Status="R"; break;
				}
				ApptProc2[i].ToothNum=arrayProc[i].ToothNum;
				ApptProc2[i].Surf=arrayProc[i].Surf;
				ApptProc2[i].AbbrDesc=ProcCodes.GetProcCode(arrayProc[i].ADACode).Descript;;
				ApptProc2[i].Fee=arrayProc[i].ProcFee.ToString("F");
			}
			//Then, fill tbProc from ApptProc2:
			tbProc.ResetRows(ApptProc2.Length);
			//tbProc.SetBackGColor(Color.White);
			tbProc.SetGridColor(Color.Gray);
			for (int i=0;i<ApptProc2.Length;i++){
				tbProc.Cell[0,i]=ApptProc2[i].Status;
				tbProc.Cell[1,i]=ApptProc2[i].ToothNum;
				tbProc.Cell[2,i]=ApptProc2[i].Surf;
				tbProc.Cell[3,i]=ApptProc2[i].AbbrDesc;
				tbProc.Cell[4,i]=ApptProc2[i].Fee;
				if(IsNext){
					if(arrayProc[ApptProc2[i].Index].NextAptNum==Appointments.Cur.AptNum){
						tbProc.ColorRow(i,Color.Silver);
					}
				}
				else{
					if(arrayProc[ApptProc2[i].Index].AptNum==Appointments.Cur.AptNum){
						tbProc.ColorRow(i,Color.Silver);
					}
				}
				
			}
			tbProc.LayoutTables();
		}//end FillProcedures

		private void tbProc_CellClicked(object sender, CellEventArgs e){
			if(textAddTime.errorProvider1.GetError(textAddTime)!=""
				//|| textDateTerm.errorProvider1.GetError(textDateTerm)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			procsHaveChanged=true;
			Procedures.Cur=arrayProc[ApptProc2[e.Row].Index];
			if(IsNext){
				if(Procedures.Cur.NextAptNum==Appointments.Cur.AptNum){
					Procedures.Cur.NextAptNum=0;
				}
				else{
					Procedures.Cur.NextAptNum=Appointments.Cur.AptNum;
				}
			}
			else{//not Next
				if(Procedures.Cur.AptNum==Appointments.Cur.AptNum){
					Procedures.Cur.AptNum=0;
				}
				else{
					Procedures.Cur.AptNum=Appointments.Cur.AptNum;
				}
			}
			Procedures.UpdateCur();
			FillProcedures();
			CalculateTime();
			FillTime();
		}

		private void CalculateTime(){
			int adjTimeU=PIn.PInt(textAddTime.Text)/10;
			strBTime=new StringBuilder("");
			string strT="";
			for(int i=0;i<ApptProc2.Length;i++){
				if(IsNext){
					if(arrayProc[ApptProc2[i].Index].NextAptNum!=Appointments.Cur.AptNum){
						continue;
					}
				}
				else{
					if(arrayProc[ApptProc2[i].Index].AptNum!=Appointments.Cur.AptNum){
						continue;
					}
				}
				strT=ProcCodes.GetProcCode(arrayProc[ApptProc2[i].Index].ADACode).ProcTime;
				if(strT.Length<2) continue;
				for(int n=1;n<strT.Length-1;n++){
					if(strT.Substring(n,1)=="/"){
						strBTime.Append("/");
					}
					else{
						strBTime.Insert(0,"X");
					}
				}
			}//end for
			//MessageBox.Show(strBTime.ToString());
			if(adjTimeU!=0){
				if(strBTime.Length==0){//might be useless.
					if(adjTimeU > 0){
						strBTime.Insert(0,"X",adjTimeU);
					}
				}
				else{//not length 0
					double xRatio;
					if((double)strBTime.ToString().LastIndexOf("X")==0)
						xRatio=1;
					else
						xRatio=(double)strBTime.ToString().LastIndexOf("X")/(double)(strBTime.Length-1);
					if(adjTimeU<0){//subtract time
						int xPort=(int)(-adjTimeU*xRatio);
						if(xPort > 0)
							if(xPort>=strBTime.Length)
								strBTime=new StringBuilder("");
							else
								strBTime.Remove(0,xPort);
						int iRemove=strBTime.Length-(-adjTimeU-xPort);
						if(iRemove < 0)
							strBTime=new StringBuilder("");
						else if(adjTimeU+xPort > strBTime.Length){
							strBTime=new StringBuilder("");
						}
						else
							strBTime.Remove(iRemove,-adjTimeU-xPort);
					}
					else{//add time
						//MessageBox.Show("adjTimeU:"+adjTimeU.ToString()+"xratio:"+xRatio.ToString());
						int xPort=(int)Math.Ceiling(adjTimeU*xRatio);
						//MessageBox.Show("xPort:"+xPort.ToString());
						if(xPort > 0)
							strBTime.Insert(0,"X",xPort);
						if(adjTimeU-xPort > 0)
							strBTime.Insert(strBTime.Length-1,"/",adjTimeU-xPort);
					}
				}//end else not length 0
			}
			strBTime.Insert(0,"/");
			strBTime.Append("/");
		}

		private void FillTime(){
			for (int i=0;i<strBTime.Length;i++){
				tbTime.Cell[0,i]=strBTime.ToString(i,1);
				tbTime.BackGColor[0,i]=Color.White;
			}
			for (int i=strBTime.Length;i<tbTime.MaxRows;i++){
				tbTime.Cell[0,i]="";
				tbTime.BackGColor[0,i]=Color.FromName("Control");
			}
			tbTime.Refresh();
			butSlider.Location=new Point(tbTime.Location.X+2
				,(tbTime.Location.Y+strBTime.Length*14+1));
			textTime.Text=strBTime.Length.ToString()+"0";
		}

		private void tbTime_CellClicked(object sender, CellEventArgs e){
			if(e.Row<strBTime.Length){
				if(strBTime[e.Row]=='/'){
					strBTime.Replace('/','X',e.Row,1);
				}
				else{
					strBTime.Replace(strBTime[e.Row],'/',e.Row,1);
				}
			}
			FillTime();
		}

		private void butSlider_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			mouseIsDown=true;
			mouseOrigin=new Point(e.X+butSlider.Location.X
				,e.Y+butSlider.Location.Y);
			sliderOrigin=butSlider.Location;
			
		}

		private void butSlider_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(!mouseIsDown)return;
			//tempPoint represents the new location of button of smooth dragging.
			Point tempPoint=new Point(sliderOrigin.X
				,sliderOrigin.Y+(e.Y+butSlider.Location.Y)-mouseOrigin.Y);
			int step=(int)(Math.Round((Decimal)(tempPoint.Y-tbTime.Location.Y)/14));
			if(step==strBTime.Length)return;
			if(step<1)return;
			if(step>tbTime.MaxRows-1) return;
			if(step>strBTime.Length){
				strBTime.Append('/');
			}
			if(step<strBTime.Length){
				strBTime.Remove(step,1);
			}
			FillTime();
		}

		private void butSlider_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			mouseIsDown=false;
		}

		private void butCalcTime_Click(object sender, System.EventArgs e) {
			if(textAddTime.errorProvider1.GetError(textAddTime)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			CalculateTime();
			FillTime();
		}

		private void butHygClear_Click(object sender, System.EventArgs e) {
			this.listProvHyg.SelectedIndex=-1;
		}

		private void listQuickAdd_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listQuickAdd.IndexFromPoint(e.X,e.Y)==-1){
				return;
			}
			if(textAddTime.errorProvider1.GetError(textAddTime)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			string[] procs=Defs.Short[(int)DefCat.ApptProcsQuickAdd]
				[listQuickAdd.IndexFromPoint(e.X,e.Y)].ItemValue.Split(',');
			for(int i=0;i<procs.Length;i++){
				Procedures.Cur=new Procedure();
				//maybe test codes in defs before allowing them in the first place(no tooth num)
				//if(ProcCodes.GetProcCode(Procedures.Cur.ADACode). 
				Procedures.Cur.PatNum=Appointments.Cur.PatNum;
				if(!IsNext)
					Procedures.Cur.AptNum=Appointments.Cur.AptNum;
				Procedures.Cur.ADACode=procs[i];
				Procedures.Cur.ProcDate=Appointments.Cur.AptDateTime.Date;
				Procedures.Cur.ProcFee=Fees.GetAmount(Procedures.Cur.ADACode,ContrChart.GetFeeSched());
				Procedures.Cur.OverridePri=-1;
				Procedures.Cur.OverrideSec=-1;
				//surf
				//toothnum
				//toothrange
				Procedures.Cur.NoBillIns=ProcCodes.GetProcCode(Procedures.Cur.ADACode).NoBillIns;
				//priority
				Procedures.Cur.ProcStatus=ProcStat.TP;
				//procnote
				//claimnum
				Procedures.Cur.ProvNum=Appointments.Cur.ProvNum;
				//Dx
				if(IsNext)
					Procedures.Cur.NextAptNum=Appointments.Cur.AptNum;
				if(Patients.Cur.PriPlanNum!=0)//if patient has ins
					Procedures.Cur.IsCovIns=true;
				Procedures.InsertCur();
			}
			listQuickAdd.SelectedIndex=-1;
			FillProcedures();
			CalculateTime();
			FillTime();
		}

		private void listQuickAdd_SelectedIndexChanged(object sender, System.EventArgs e) {
			//listQuickAdd.SelectedIndex=-1;
		}

		private void radioLabNone_Click(object sender, System.EventArgs e) {
			Appointments.Cur.Lab=LabCase.None;
		}

		private void radioLabSent_Click(object sender, System.EventArgs e) {
			Appointments.Cur.Lab=LabCase.Sent;
		}

		private void radioLabReceived_Click(object sender, System.EventArgs e) {
			Appointments.Cur.Lab=LabCase.Received;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textAddTime.errorProvider1.GetError(textAddTime)!=""
				//|| textDateTerm.errorProvider1.GetError(textDateTerm)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			Appointments.Cur.AptStatus=(ApptStatus)listStatus.SelectedIndex+1;
			Appointments.Cur.Pattern=strBTime.ToString();
			if(listConfirmed.SelectedIndex!=-1)
				Appointments.Cur.Confirmed=Defs.Short[(int)DefCat.ApptConfirmed][listConfirmed.SelectedIndex].DefNum;
			Appointments.Cur.AddTime=(int)(PIn.PInt(textAddTime.Text)/10);
			//Appointments.Cur.IsRecall=this.checkRecall.Checked;
			Appointments.Cur.Note=textNote.Text;
			//there should always be a non-hidden primary provider for an appt.
			if(listProvNum.SelectedIndex==-1)
				Appointments.Cur.ProvNum=Providers.List[0].ProvNum;
			else
				Appointments.Cur.ProvNum=Providers.List[listProvNum.SelectedIndex].ProvNum;
			if(listProvHyg.SelectedIndex==-1)
				Appointments.Cur.ProvHyg=0;
			else
				Appointments.Cur.ProvHyg=Providers.List[listProvHyg.SelectedIndex].ProvNum;
			Appointments.UpdateCur();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormApptEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNew){
				if(IsNext){
					Procedures.UnattachProcsInNextAppt(Appointments.Cur.AptNum);
				}
				else{
					Procedures.UnattachProcsInAppt(Appointments.Cur.AptNum);
				}
				Appointments.DeleteCur();
				DialogResult=DialogResult.Cancel;
				//return;
			}
			else if(procsHaveChanged){
				MessageBox.Show(Lan.g(this,"Warning. Changes you made to procedures have already been saved.  Other changes will not be saved."));
				DialogResult=DialogResult.OK;//so that appt module will update
				//return;
			}
		}		

	}

	public struct ApptProc{
		public int Index;//represents index within arrayProc
		public string Status;
		public string ToothNum;
		public string Surf;
		public string AbbrDesc;
		public string Fee;
	}//end struct ApptProc
}
