using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormUnsched : System.Windows.Forms.Form{
		private System.ComponentModel.Container components = null;
		private OpenDental.TableUnsched tbApts;
		private System.Windows.Forms.Button butClose;
		public bool PinClicked=false;		
		public static string procsForCur;

		public FormUnsched(){
			InitializeComponent();// Required for Windows Form Designer support
			tbApts.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbApts_CellClicked);
			tbApts.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbApts_CellDoubleClicked);
			Lan.C("All", new System.Windows.Forms.Control[] {
				butClose,
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
			this.tbApts.SelectedIndices = new int[0];
			this.tbApts.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbApts.Size = new System.Drawing.Size(829, 600);
			this.tbApts.TabIndex = 0;
			// 
			// butClose
			// 
			this.butClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClose.Location = new System.Drawing.Point(761, 628);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 27);
			this.butClose.TabIndex = 7;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormUnsched
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
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
			tbApts.SelectedRow=-1;
			Appointments.RefreshUnsched();
			int[] aptNums=new int[Appointments.ListUn.Length];
			for(int i=0;i<Appointments.ListUn.Length;i++){
				aptNums[i]=Appointments.ListUn[i].AptNum;
			}
			Procedures.GetProcsMultApts(aptNums);
			tbApts.ResetRows(Appointments.ListUn.Length);
			tbApts.SetGridColor(Color.DarkGray);
			string procs;
			for (int i=0;i<Appointments.ListUn.Length;i++){
				Patients.GetLim(Appointments.ListUn[i].PatNum);
				tbApts.Cell[0,i]=Patients.LimName;
				if(Appointments.ListUn[i].AptDateTime.Year < 1880)
					tbApts.Cell[1,i]="";
				else 
					tbApts.Cell[1,i]=Appointments.ListUn[i].AptDateTime.ToShortDateString();
				tbApts.Cell[2,i]=Defs.GetName(DefCat.RecallUnschedStatus,Appointments.ListUn[i].UnschedStatus);
				tbApts.Cell[3,i]=Providers.GetAbbr(Appointments.ListUn[i].ProvNum);
				Procedures.GetProcsOneApt(Appointments.ListUn[i].AptNum);
				procs="";
				for(int p=0;p<Procedures.ProcsOneApt.Length;p++){
					procs+=Procedures.ProcsOneApt[p];
					if(p<Procedures.ProcsOneApt.Length-1)
						procs+=", ";
				}
				tbApts.Cell[4,i]=procs;
				tbApts.Cell[5,i]=Appointments.ListUn[i].Note;
			}
			//if(tbApts.SelectedRow!=-1){
			//	tbApts.ColorRow(tbApts.SelectedRow,Color.LightGray);
			//}
			tbApts.LayoutTables();
			int[] myAptNums=new int[Appointments.ListUn.Length];
			for(int i=0;i<myAptNums.Length;i++){
				myAptNums[i]=Appointments.ListUn[i].AptNum;
			}	
			Procedures.GetProcsMultApts(myAptNums);
		}

		private void tbApts_CellClicked(object sender, CellEventArgs e){
			Appointments.Cur=Appointments.ListUn[e.Row];
		}

		private void tbApts_CellDoubleClicked(object sender, CellEventArgs e){
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
			 FillAppointments();
		}	

		private void CreateCurInfo(){
			ContrAppt.CurInfo=new InfoApt();
			ContrAppt.CurInfo.MyApt=Appointments.Cur;
			ContrAppt.CurInfo.CreditAndIns=Patients.GetCreditIns();
			ContrAppt.CurInfo.PatientName=Patients.GetCurNameLF();
			Procedures.GetProcsOneApt(Appointments.Cur.AptNum);
			ContrAppt.CurInfo.Procs=Procedures.ProcsOneApt;
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
