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
		private System.Windows.Forms.Button butClose;
		///<summary></summary>
		public bool PinClicked=false;		
		///<summary></summary>
		public static string procsForCur;

		///<summary></summary>
		public FormUnsched(){
			InitializeComponent();// Required for Windows Form Designer support
			tbApts.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbApts_CellDoubleClicked);
			Lan.C("All", new System.Windows.Forms.Control[] {
				butClose,
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tbApts = new OpenDental.TableUnsched();
			this.butClose = new System.Windows.Forms.Button();
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
			FillAppointments();
		}

		private void FillAppointments(){
			this.Cursor=Cursors.WaitCursor;
			Patients.GetHList();
			Appointments.RefreshUnsched(false);
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
			/*
			Procedures.GetProcsOneApt(Appointments.Cur.AptNum);
			string procs="";
				for(int p=0;p<Procedures.ProcsOneApt.Length;p++){
					procs+=Procedures.ProcsOneApt[p];
					if(p<Procedures.ProcsOneApt.Length-1)
						procs+=", ";
				}
			procsForCur=procs;
			FormUnschedEdit FormUE=new FormUnschedEdit();
			FormUE.ShowDialog();
			if(FormUE.DialogResult!=DialogResult.OK)
				return;
			if(FormUE.PinClicked){
				PinClicked=true;
				Patients.GetFamily(Appointments.Cur.PatNum);
				CreateCurInfo();
				Appointments.ListUn=null;
				DialogResult=DialogResult.OK;
			}
			else
			 FillAppointments();*/
		}	

		private void CreateCurInfo(){
			ContrAppt.CurInfo=new InfoApt();
			ContrAppt.CurInfo.MyApt=Appointments.Cur;
			ContrAppt.CurInfo.CreditAndIns=Patients.GetCreditIns();
			ContrAppt.CurInfo.PatientName=Patients.GetCurNameLF();
			Procedures.GetProcsForSingle(Appointments.Cur.AptNum,false);
			ContrAppt.CurInfo.Procs=Procedures.ProcsForSingle;
		}

		/*delete
			if(tbApts.SelectedRow==-1){
				MessageBox.Show("Please select appointment first.");
				return;
			}
			if(MessageBox.Show("Delete appointment?","",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			Procedures.UnattachProcsInAppt(Appointments.Cur.AptNum);
			Appointments.DeleteCur();
			FillAppointments();
		}*/

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormUnsched_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			Appointments.ListUn=null;
		}

	}
}
