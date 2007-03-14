using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormUnsched : System.Windows.Forms.Form{
		private System.ComponentModel.Container components = null;
		private OpenDental.TableUnsched tbApts;
		private OpenDental.UI.Button butClose;
		///<summary></summary>
		public bool PinClicked=false;		
		///<summary></summary>
		public static string procsForCur;
		private Appointment[] ListUn;

		///<summary></summary>
		public FormUnsched(){
			InitializeComponent();// Required for Windows Form Designer support
			tbApts.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbApts_CellDoubleClicked);
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
			this.tbApts = new OpenDental.TableUnsched();
			this.butClose = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// tbApts
			// 
			this.tbApts.BackColor = System.Drawing.SystemColors.Window;
			this.tbApts.Location = new System.Drawing.Point(10, 10);
			this.tbApts.Name = "tbApts";
			this.tbApts.ScrollValue = 1;
			this.tbApts.SelectedIndices = new int[0];
			this.tbApts.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbApts.Size = new System.Drawing.Size(734, 647);
			this.tbApts.TabIndex = 0;
			// 
			// butClose
			// 
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClose.Location = new System.Drawing.Point(761, 628);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 27);
			this.butClose.TabIndex = 7;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormUnsched
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(858, 672);
			this.Controls.Add(this.tbApts);
			this.Controls.Add(this.butClose);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormUnsched";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Unscheduled List";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormUnsched_Closing);
			this.Load += new System.EventHandler(this.FormUnsched_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormUnsched_Load(object sender, System.EventArgs e) {
			Patients.GetHList();
			FillAppointments();
		}

		private void FillAppointments(){
			this.Cursor=Cursors.WaitCursor;
			ListUn=Appointments.RefreshUnsched(false);
			tbApts.ResetRows(ListUn.Length);
			tbApts.SetGridColor(Color.DarkGray);
			for(int i=0;i<ListUn.Length;i++){
				tbApts.Cell[0,i]=(string)Patients.HList[ListUn[i].PatNum];
				if(ListUn[i].AptDateTime.Year < 1880)
					tbApts.Cell[1,i]="";
				else 
					tbApts.Cell[1,i]=ListUn[i].AptDateTime.ToShortDateString();
				tbApts.Cell[2,i]=Defs.GetName(DefCat.RecallUnschedStatus,ListUn[i].UnschedStatus);
				tbApts.Cell[3,i]=Providers.GetAbbr(ListUn[i].ProvNum);
				tbApts.Cell[4,i]=ListUn[i].ProcDescript;
				tbApts.Cell[5,i]=ListUn[i].Note;
			}
			tbApts.LayoutTables();
			Cursor=Cursors.Default;
		}

		private void tbApts_CellDoubleClicked(object sender, CellEventArgs e){
			int currentSelection=tbApts.SelectedRow;
			int currentScroll=tbApts.ScrollValue;
			FormApptEdit FormAE=new FormApptEdit(ListUn[e.Row]);
			FormAE.PinIsVisible=true;
			FormAE.ShowDialog();
			if(FormAE.DialogResult!=DialogResult.OK)
				return;
			if(FormAE.PinClicked){
				PinClicked=true;
				CreateCurInfo(ListUn[e.Row]);
				DialogResult=DialogResult.OK;
			}
			else{
				FillAppointments();
				tbApts.SetSelected(currentSelection,true);
				tbApts.ScrollValue=currentScroll;
			}
		}	

		private void CreateCurInfo(Appointment aptCur){
			ContrAppt.CurInfo=new InfoApt();
			ContrAppt.CurInfo.MyApt=aptCur;
			Procedure[] procs=Procedures.GetProcsForSingle(aptCur.AptNum,false);
			ContrAppt.CurInfo.Procs=procs;
			ContrAppt.CurInfo.Production=Procedures.GetProductionOneApt(aptCur.AptNum,procs,false);
			ContrAppt.CurInfo.MyPatient=Patients.GetPat(aptCur.PatNum);
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormUnsched_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			Patients.HList=null;
		}

	}
}
