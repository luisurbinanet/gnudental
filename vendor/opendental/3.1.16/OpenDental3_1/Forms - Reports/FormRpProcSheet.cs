using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormRpProcSheet : System.Windows.Forms.Form{
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.MonthCalendar date2;
		private System.Windows.Forms.MonthCalendar date1;
		private System.Windows.Forms.Label labelTO;
		private OpenDental.UI.Button butAll;
		private System.Windows.Forms.ListBox listProv;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton radioIndividual;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioGrouped;
	  private FormQuery FormQuery2;
		///<summary>The where clause for the providers.</summary>
		private string whereProv;

		///<summary></summary>
		public FormRpProcSheet(){
			InitializeComponent();
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
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.date2 = new System.Windows.Forms.MonthCalendar();
			this.date1 = new System.Windows.Forms.MonthCalendar();
			this.labelTO = new System.Windows.Forms.Label();
			this.butAll = new OpenDental.UI.Button();
			this.listProv = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.radioIndividual = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioGrouped = new System.Windows.Forms.RadioButton();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(567, 372);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(567, 336);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// date2
			// 
			this.date2.Location = new System.Drawing.Point(285, 118);
			this.date2.Name = "date2";
			this.date2.TabIndex = 2;
			// 
			// date1
			// 
			this.date1.Location = new System.Drawing.Point(29, 118);
			this.date1.Name = "date1";
			this.date1.TabIndex = 1;
			// 
			// labelTO
			// 
			this.labelTO.Location = new System.Drawing.Point(245, 126);
			this.labelTO.Name = "labelTO";
			this.labelTO.Size = new System.Drawing.Size(24, 23);
			this.labelTO.TabIndex = 22;
			this.labelTO.Text = "TO";
			// 
			// butAll
			// 
			this.butAll.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAll.Autosize = true;
			this.butAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAll.Location = new System.Drawing.Point(520, 274);
			this.butAll.Name = "butAll";
			this.butAll.Size = new System.Drawing.Size(75, 26);
			this.butAll.TabIndex = 34;
			this.butAll.Text = "&All";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(519, 118);
			this.listProv.Name = "listProv";
			this.listProv.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listProv.Size = new System.Drawing.Size(120, 147);
			this.listProv.TabIndex = 33;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(519, 99);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 16);
			this.label1.TabIndex = 32;
			this.label1.Text = "Providers";
			// 
			// radioIndividual
			// 
			this.radioIndividual.Checked = true;
			this.radioIndividual.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioIndividual.Location = new System.Drawing.Point(11, 17);
			this.radioIndividual.Name = "radioIndividual";
			this.radioIndividual.Size = new System.Drawing.Size(207, 21);
			this.radioIndividual.TabIndex = 35;
			this.radioIndividual.TabStop = true;
			this.radioIndividual.Text = "Individual Procedures";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioGrouped);
			this.groupBox1.Controls.Add(this.radioIndividual);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(29, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(239, 70);
			this.groupBox1.TabIndex = 36;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Type";
			// 
			// radioGrouped
			// 
			this.radioGrouped.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioGrouped.Location = new System.Drawing.Point(11, 40);
			this.radioGrouped.Name = "radioGrouped";
			this.radioGrouped.Size = new System.Drawing.Size(215, 21);
			this.radioGrouped.TabIndex = 36;
			this.radioGrouped.Text = "Grouped By Procedure Code";
			// 
			// FormRpProcSheet
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(682, 437);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butAll);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.date2);
			this.Controls.Add(this.date1);
			this.Controls.Add(this.labelTO);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpProcSheet";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Daily Procedures Report";
			this.Load += new System.EventHandler(this.FormDailySummary_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormDailySummary_Load(object sender, System.EventArgs e) {
			date1.SelectionStart=DateTime.Today;
			date2.SelectionStart=DateTime.Today;
			for(int i=0;i<Providers.List.Length;i++){
				listProv.Items.Add(Providers.List[i].Abbr+" - "+Providers.List[i].LName+", "
					+Providers.List[i].FName);
				listProv.SetSelected(i,true);
			}
		}

		private void butAll_Click(object sender, System.EventArgs e) {
			for(int i=0;i<listProv.Items.Count;i++){
				listProv.SetSelected(i,true);
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//MessageBox.Show("This reporting feature is incomplete.");
			//return;
			if(listProv.SelectedIndices.Count==0){
				MessageBox.Show("At least one provider must be selected.");
				return;
			}
			whereProv="(";
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				if(i>0){
					whereProv+="OR ";
				}
				whereProv+="procedurelog.ProvNum = '"
					+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"' ";
			}
			whereProv+=")";
			Queries.CurReport=new ReportOld();
			if(radioIndividual.Checked){
				CreateIndividual();
			}
			else{
				CreateGrouped();
			}
		}

		private void CreateIndividual(){
			//added plfname for ordering purposes, spk 3/13/04
			// added CapCoPay for Capitation, SPK 7/15/04 (js rewrote it on 10/4/04 due to db change)
			// changed procedurecode.adacode to procedurelog.adacode & added Procnum to retrieve all codes
			Queries.CurReport.Query="SELECT procedurelog.ProcDate,CONCAT"
				+"(patient.LName,', ',patient.FName,' ',patient.MiddleI) AS plfname, procedurelog.ADACode,"
				+"procedurelog.ToothNum,procedurecode.Descript,provider.Abbr,"
				+"procedurelog.ProcFee-SUM(claimproc.WriteOff) AS $fee "// procedurelog.ProcNum  "
				+"FROM procedurelog,patient,procedurecode,provider "
				+"LEFT JOIN claimproc ON procedurelog.ProcNum=claimproc.ProcNum "
				+"AND claimproc.Status='7' "//only CapComplete writeoffs are subtracted here.
				+"WHERE procedurelog.ProcStatus = '2' "
				+"AND patient.PatNum=procedurelog.PatNum "
				+"AND procedurelog.ADACode=procedurecode.ADACode "
				+"AND provider.ProvNum=procedurelog.ProvNum "
				+"AND "+whereProv+" "
				+"AND procedurelog.ProcDate >= '" +POut.PDate(date1.SelectionStart)+"' "
				+"AND procedurelog.ProcDate <= '" +POut.PDate(date2.SelectionStart)+"' "
				+"GROUP BY procedurelog.ProcNum "
				+"ORDER BY procedurelog.ProcDate,plfname";
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			FormQuery2.SubmitReportQuery();			
			Queries.CurReport.Title="Daily Procedures";
			Queries.CurReport.SubTitle=new string[2];
			Queries.CurReport.SubTitle[0]=((Pref)Prefs.HList["PracticeTitle"]).ValueString;
			Queries.CurReport.SubTitle[1]=date1.SelectionStart.ToString("d")
				+" - "+date2.SelectionStart.ToString("d");	
			// col[5] from 590 to 640, 680, 770, spk 7/20/04
			Queries.CurReport.ColPos=new int[9];
			Queries.CurReport.ColCaption=new string[8];
			Queries.CurReport.ColAlign=new HorizontalAlignment[8];			
			Queries.CurReport.ColPos[0]=20;
			Queries.CurReport.ColPos[1]=100;
			Queries.CurReport.ColPos[2]=250;
			Queries.CurReport.ColPos[3]=325;
			Queries.CurReport.ColPos[4]=370;
			Queries.CurReport.ColPos[5]=640;
			Queries.CurReport.ColPos[6]=680;
			Queries.CurReport.ColPos[7]=770;
			Queries.CurReport.ColPos[8]=820;		// spk
			Queries.CurReport.ColCaption[0]="Date";
			Queries.CurReport.ColCaption[1]="Patient Name";			
			Queries.CurReport.ColCaption[2]="ADA Code";
			Queries.CurReport.ColCaption[3]="Tooth";
			Queries.CurReport.ColCaption[4]="Description";
			Queries.CurReport.ColCaption[5]="Provider";
			Queries.CurReport.ColCaption[6]="Fee";
			Queries.CurReport.ColAlign[6]=HorizontalAlignment.Right;
			Queries.CurReport.ColCaption[7]=" ";	// spk
			Queries.CurReport.Summary=new string[0];
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		private void CreateGrouped(){
			//this would require a temporary table to be able to handle capitation.
			Queries.CurReport.Query="SELECT definition.ItemName,procedurelog.ADACode,procedurecode.Descript,"
        +"Count(*),AVG(procedurelog.ProcFee) AS $AvgFee,SUM(procedurelog.ProcFee) AS $TotFee "
				+"FROM procedurelog,procedurecode,definition "
				+"WHERE procedurelog.ProcStatus = '2' "
				+"&& procedurelog.ADACode=procedurecode.ADACode "
				+"&& definition.DefNum=procedurecode.ProcCat "
				+"&& "+whereProv+" "
				+"&& procedurelog.ProcDate >= '" + date1.SelectionStart.ToString("yyyy-MM-dd")+"' "
				+"&& procedurelog.ProcDate <= '" + date2.SelectionStart.ToString("yyyy-MM-dd")+"' "
				+"GROUP BY procedurelog.ADACode "
				+"ORDER BY definition.ItemOrder,procedurelog.ADACode";
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			FormQuery2.SubmitReportQuery();			
			Queries.CurReport.Title="Procedures By Procedure Code";
			Queries.CurReport.SubTitle=new string[2];
			Queries.CurReport.SubTitle[0]=((Pref)Prefs.HList["PracticeTitle"]).ValueString;
			Queries.CurReport.SubTitle[1]=date1.SelectionStart.ToString("d")
				+" - "+date2.SelectionStart.ToString("d");	
			Queries.CurReport.ColPos=new int[7];
			Queries.CurReport.ColCaption=new string[6];
			Queries.CurReport.ColAlign=new HorizontalAlignment[6];			
			Queries.CurReport.ColPos[0]=20;
			Queries.CurReport.ColPos[1]=170;
			Queries.CurReport.ColPos[2]=260;
			Queries.CurReport.ColPos[3]=440;
			Queries.CurReport.ColPos[4]=490;
			Queries.CurReport.ColPos[5]=600;
			Queries.CurReport.ColPos[6]=700;
			Queries.CurReport.ColCaption[0]="Category";
			Queries.CurReport.ColCaption[1]="ADA Code";			
			Queries.CurReport.ColCaption[2]="Description";
			Queries.CurReport.ColCaption[3]="Quantity";
			Queries.CurReport.ColCaption[4]="Average Fee";
			Queries.CurReport.ColCaption[5]="Total Fees";
			Queries.CurReport.ColAlign[3]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[4]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[5]=HorizontalAlignment.Right;
			//Queries.CurReport.ColCaption[7]=" ";
			Queries.CurReport.Summary=new string[0];
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;
		}
		

		
	}
}
















