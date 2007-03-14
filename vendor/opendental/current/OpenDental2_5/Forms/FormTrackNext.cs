using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>The Next appoinment tracking tool.</summary>
	public class FormTrackNext : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butClose;
		private OpenDental.TableUnsched tbApts;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary>Passes the pinclicked result down from the appointment to the parent form.</summary>
		public bool PinClicked;		

		///<summary></summary>
		public FormTrackNext(){
			InitializeComponent();// Required for Windows Form Designer support
			tbApts.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbApts_CellDoubleClicked);
			Lan.C("All", new System.Windows.Forms.Control[] {
				butClose,
			});
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormTrackNext));
			this.butClose = new System.Windows.Forms.Button();
			this.tbApts = new OpenDental.TableUnsched();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClose.Location = new System.Drawing.Point(872, 642);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// tbApts
			// 
			this.tbApts.BackColor = System.Drawing.SystemColors.Window;
			this.tbApts.Location = new System.Drawing.Point(13, 12);
			this.tbApts.Name = "tbApts";
			this.tbApts.ScrollValue = 1;
			this.tbApts.SelectedIndices = new int[0];
			this.tbApts.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbApts.Size = new System.Drawing.Size(734, 656);
			this.tbApts.TabIndex = 1;
			// 
			// FormTrackNext
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(971, 684);
			this.Controls.Add(this.tbApts);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormTrackNext";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Track Next Appointments";
			this.Load += new System.EventHandler(this.FormTrackNext_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormTrackNext_Load(object sender, System.EventArgs e) {
			FillAppointments();
		}

		private void FillAppointments(){
			this.Cursor=Cursors.WaitCursor;
			Patients.GetHList();
			Appointments.RefreshUnsched(true);
			tbApts.ResetRows(Appointments.ListUn.Length);
			tbApts.SetGridColor(Color.DarkGray);
			for (int i=0;i<Appointments.ListUn.Length;i++){
				tbApts.Cell[0,i]=(string)Patients.HList[Appointments.ListUn[i].PatNum];
				if(Appointments.ListUn[i].AptDateTime.Year < 1880)
					tbApts.Cell[1,i]="";
				else 
					tbApts.Cell[1,i]=Appointments.ListUn[i].AptDateTime.ToShortDateString();
				tbApts.Cell[2,i]=Defs.GetName(DefCat.RecallUnschedStatus,Appointments.ListUn[i].UnschedStatus);
				tbApts.Cell[3,i]=Providers.GetAbbr(Appointments.ListUn[i].ProvNum);
				tbApts.Cell[4,i]=Appointments.ListUn[i].ProcDescript;
				tbApts.Cell[5,i]=Appointments.ListUn[i].Note;
			}
			Patients.HList=null;
			tbApts.LayoutTables();
			Cursor=Cursors.Default;
		}

		private void tbApts_CellDoubleClicked(object sender, CellEventArgs e){
			int currentSelection=tbApts.SelectedRow;
			int currentScroll=tbApts.ScrollValue;
			Appointments.Cur=Appointments.ListUn[e.Row];
			Patients.GetFamily(Appointments.Cur.PatNum);
			FormApptEdit FormAE=new FormApptEdit();
			FormAE.PinIsVisible=true;
			FormAE.ShowDialog();
			if(FormAE.DialogResult!=DialogResult.OK)
				return;
			if(FormAE.PinClicked){
				PinClicked=true;
				CreateCurInfo();
				Appointments.ListUn=null;
				DialogResult=DialogResult.OK;
			}
			else{
				FillAppointments();
				tbApts.SetSelected(currentSelection,true);
				tbApts.ScrollValue=currentScroll;
			}
		}	

		/// <summary>This is not the best way to handle this for the long term.  There must be a better way to get some of the extra display info.</summary>
		private void CreateCurInfo(){
			ContrAppt.CurInfo=new InfoApt();
			ContrAppt.CurInfo.MyApt=Appointments.Cur;
			ContrAppt.CurInfo.CreditAndIns=Patients.GetCreditIns();
			ContrAppt.CurInfo.PatientName=Patients.GetCurNameLF();
			Procedures.GetProcsForSingle(Appointments.Cur.AptNum,true);
			ContrAppt.CurInfo.Procs=Procedures.ProcsForSingle;
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormUnsched_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			Appointments.ListUn=null;
		}
		


	}
}





















