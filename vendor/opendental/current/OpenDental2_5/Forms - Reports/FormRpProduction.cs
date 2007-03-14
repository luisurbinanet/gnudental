using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace OpenDental{
///<summary></summary>
	public class FormRpProduction : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;

		private System.ComponentModel.Container components = null;
		//private DataTable TableDate=   new DataTable();  //hold dates
		private DataTable TableCharge= new DataTable();  //charges
		private DataTable TablePay=    new DataTable();  //payments - Patient
		private DataTable TableIns=    new DataTable();  //payments - Ins, added SPK 
		private DataTable TableAdj=    new DataTable();  //adjustments
		private DataTable TableSched=  new DataTable();
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox listProv;
		private System.Windows.Forms.Button butAll;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RadioButton radioDaily;
		private System.Windows.Forms.RadioButton radioMonthly;
		private System.Windows.Forms.Panel panelMonth;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textToday;
		private OpenDental.ValidDate textDateFrom;
		private OpenDental.ValidDate textDateTo;
		private FormQuery FormQuery2;

		///<summary></summary>
		public FormRpProduction(){
			InitializeComponent();
 			Lan.C(this, new System.Windows.Forms.Control[] {
				//date2,
				//date1,
				//labelTO,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.listProv = new System.Windows.Forms.ListBox();
			this.butAll = new System.Windows.Forms.Button();
			this.radioMonthly = new System.Windows.Forms.RadioButton();
			this.radioDaily = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.panelMonth = new System.Windows.Forms.Panel();
			this.textDateTo = new OpenDental.ValidDate();
			this.textDateFrom = new OpenDental.ValidDate();
			this.label4 = new System.Windows.Forms.Label();
			this.textToday = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.panelMonth.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(422, 298);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(422, 263);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(37, 128);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 16);
			this.label1.TabIndex = 29;
			this.label1.Text = "Providers";
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(37, 147);
			this.listProv.Name = "listProv";
			this.listProv.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listProv.Size = new System.Drawing.Size(120, 134);
			this.listProv.TabIndex = 30;
			// 
			// butAll
			// 
			this.butAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butAll.Location = new System.Drawing.Point(37, 295);
			this.butAll.Name = "butAll";
			this.butAll.Size = new System.Drawing.Size(75, 26);
			this.butAll.TabIndex = 31;
			this.butAll.Text = "&All";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// radioMonthly
			// 
			this.radioMonthly.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioMonthly.Location = new System.Drawing.Point(14, 43);
			this.radioMonthly.Name = "radioMonthly";
			this.radioMonthly.Size = new System.Drawing.Size(104, 17);
			this.radioMonthly.TabIndex = 33;
			this.radioMonthly.Text = "Monthly";
			this.radioMonthly.Visible = false;
			this.radioMonthly.Click += new System.EventHandler(this.radioMonthly_Click);
			// 
			// radioDaily
			// 
			this.radioDaily.Checked = true;
			this.radioDaily.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioDaily.Location = new System.Drawing.Point(14, 21);
			this.radioDaily.Name = "radioDaily";
			this.radioDaily.Size = new System.Drawing.Size(104, 17);
			this.radioDaily.TabIndex = 34;
			this.radioDaily.TabStop = true;
			this.radioDaily.Text = "Daily";
			this.radioDaily.Click += new System.EventHandler(this.radioDaily_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioDaily);
			this.groupBox1.Controls.Add(this.radioMonthly);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(37, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(123, 72);
			this.groupBox1.TabIndex = 35;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Report Type";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(15, 13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(82, 18);
			this.label2.TabIndex = 37;
			this.label2.Text = "From";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(13, 39);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(82, 18);
			this.label3.TabIndex = 39;
			this.label3.Text = "To";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// panelMonth
			// 
			this.panelMonth.Controls.Add(this.textDateTo);
			this.panelMonth.Controls.Add(this.label3);
			this.panelMonth.Controls.Add(this.label2);
			this.panelMonth.Controls.Add(this.textDateFrom);
			this.panelMonth.Location = new System.Drawing.Point(289, 45);
			this.panelMonth.Name = "panelMonth";
			this.panelMonth.Size = new System.Drawing.Size(212, 73);
			this.panelMonth.TabIndex = 40;
			// 
			// textDateTo
			// 
			this.textDateTo.Location = new System.Drawing.Point(99, 36);
			this.textDateTo.Name = "textDateTo";
			this.textDateTo.TabIndex = 44;
			this.textDateTo.Text = "";
			// 
			// textDateFrom
			// 
			this.textDateFrom.Location = new System.Drawing.Point(99, 9);
			this.textDateFrom.Name = "textDateFrom";
			this.textDateFrom.TabIndex = 43;
			this.textDateFrom.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(259, 10);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(127, 20);
			this.label4.TabIndex = 41;
			this.label4.Text = "Today\'s Date";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textToday
			// 
			this.textToday.Location = new System.Drawing.Point(388, 8);
			this.textToday.Name = "textToday";
			this.textToday.TabIndex = 42;
			this.textToday.Text = "";
			// 
			// FormRpProduction
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(515, 339);
			this.Controls.Add(this.textToday);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.panelMonth);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butAll);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpProduction";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Production and Income Report";
			this.Load += new System.EventHandler(this.FormProduction_Load);
			this.groupBox1.ResumeLayout(false);
			this.panelMonth.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		private void FormProduction_Load(object sender, System.EventArgs e) {
			textToday.Text=DateTime.Today.ToShortDateString();
			for(int i=0;i<Providers.List.Length;i++){
				listProv.Items.Add(Providers.List[i].Abbr+" - "+Providers.List[i].LName+", "
					+Providers.List[i].FName);
				listProv.SetSelected(i,true);
			}
			SetDates();
		}

		private void butAll_Click(object sender, System.EventArgs e) {
			for(int i=0;i<listProv.Items.Count;i++){
				listProv.SetSelected(i,true);
			}
		}

		private void radioDaily_Click(object sender, System.EventArgs e) {
			SetDates();
		}

		private void radioMonthly_Click(object sender, System.EventArgs e) {
			SetDates();
		}

		private void SetDates(){
			if(radioDaily.Checked){
				textDateFrom.Text=new DateTime(DateTime.Today.Year,DateTime.Today.Month,1).ToShortDateString();
				textDateTo.Text=new DateTime(DateTime.Today.Year,DateTime.Today.Month
					,DateTime.DaysInMonth(DateTime.Today.Year,DateTime.Today.Month)).ToShortDateString();
			}
			else{//monthly (not in use yet)
				textDateFrom.Text=new DateTime(DateTime.Today.Year,1,1).ToShortDateString();
				textDateTo.Text=new DateTime(DateTime.Today.Year,12,31).ToShortDateString();
			}
		}

/*  There are 5 temp tables  
 *  TableCharge: Holds sum of all charges for a certain date.
 *  TableSched: Holds Scheduled but not charged procedures 
 *  TablePay: Holds sum of all Patient payments for a certain date.
 *  TableIns: Holds sum of all Insurance payments for a certain date.--- added by SPK 3/16/04
 *  TableAdj: Holds sum of all adjustments for a certain date.
 * GROUP BY is used to group dates together so that amounts are summed for each date
 */
		private void butOK_Click(object sender, System.EventArgs e) {
			if(  textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateTo.errorProvider1.GetError(textDateTo)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(listProv.SelectedIndices.Count==0){
				MessageBox.Show(Lan.g(this,"You must select at least one provider."));
				return;
			}
			DateTime dateFrom=PIn.PDate(textDateFrom.Text);
			DateTime dateTo=PIn.PDate(textDateTo.Text);
//TableCharge
/*
Select procdate, sum(procfee) From procedurelog
Group By procdate Order by procdate desc  
*/
			Queries.CurReport=new Report();
			string whereProv;//used as the provider portion of the where clauses.
				//each whereProv needs to be set up separately for each query
			whereProv="&& (";
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				if(i>0){
					whereProv+=" ||";
				}
				whereProv+=" ProvNum = '"+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"'";
			}
			whereProv+=")";
			Queries.CurReport.Query="SELECT ProcDate, SUM(ProcFee) FROM procedurelog WHERE "
				+"ProcDate >= '" + dateFrom.ToString("yyyy-MM-dd")+"' "
				+"&& ProcDate <= '" + dateTo.ToString("yyyy-MM-dd")+"' "
				+"&& ProcStatus = '2' "
				+"&& CapCoPay = '-1' "
				+whereProv
				+" GROUP BY ProcDate ORDER BY ProcDate"; 
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			Queries.SubmitTemp(); //create TableTemp
      TableCharge=Queries.TableTemp; //must create datatable obj since Queries.TempTable is static


// NEXT is TableSched
/*
SELECT FROM_DAYS(TO_DAYS(Appointment.AptDateTime)) AS
SchedDate,SUM(Procedurelog.procfee) FROM Appointment, Procedurelog 
Where Appointment.aptnum = Procedurelog.aptnum && Appointment.AptStatus = 1
|| Appointment.AptStatus=4 && FROM_DAYS(TO_DAYS(Appointment.AptDateTime)) <= '2003-05-12'    
GROUP BY SchedDate
*/
			Queries.CurReport=new Report();
			whereProv="&& (";
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				if(i>0){
					whereProv+=" ||";
				}
				whereProv+=" procedurelog.provnum = '"
					+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"'";
			}
			whereProv+=")";
			Queries.CurReport.Query= "SELECT FROM_DAYS(TO_DAYS(appointment.AptDateTime)) AS "//gets rid of time
			  +"SchedDate,SUM(procedurelog.procfee) FROM appointment, procedurelog WHERE "
        +"appointment.aptnum = procedurelog.aptnum && (appointment.AptStatus = 1 || "//stat=scheduled
        +"appointment.AptStatus = 4) "//or stat=ASAP
				+"&& procedurelog.CapCoPay = '-1' "
				+"&& FROM_DAYS(TO_DAYS(appointment.AptDateTime)) >= '"
				+dateFrom.ToString("yyyy-MM-dd")+"' "
				+"&& FROM_DAYS(TO_DAYS(appointment.AptDateTime)) <= '"
				+dateTo.ToString("yyyy-MM-dd")+"' "
				+whereProv
				+" GROUP BY SchedDate ORDER BY SchedDate"; 
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			Queries.SubmitTemp(); //create TableTemp
      TableSched=Queries.TableTemp; //must create datatable obj since Queries.TempTable is static

// NEXT is TablePay
//must join the paysplit to the payment to eliminate the discounts.
/*
Select paysplit.procdate,sum(paysplit.splitamt) from paysplit,payment where paysplit.procdate < '2003-08-12'
&& paysplit.paynum = payment.paynum
group by procdate union all 
Select claimpayment.checkdate,sum(claimproc.inspayamt) from claimpayment,claimproc where 
claimproc.claimpaymentnum = claimpayment.claimpaymentnum
&& claimpayment.checkdate < '2003-08-12'
group by claimpayment.checkdate order by procdate
*/
			Queries.CurReport=new Report();
			whereProv="&& (";
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				if(i>0){
					whereProv+=" ||";
				}
				whereProv+=" paysplit.provnum = '"
					+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"'";
			}
			whereProv+=")";
			Queries.CurReport.Query= "SELECT paysplit.procdate,SUM(paysplit.splitamt) FROM paysplit "
				+"WHERE paysplit.isdiscount = '0' "
				+"&& paysplit.procdate >= '" + dateFrom.ToString("yyyy-MM-dd")+"' "
				+"&& paysplit.procdate <= '" + dateTo.ToString("yyyy-MM-dd")+"' "
				+whereProv
				+" GROUP BY paysplit.procdate ORDER BY procdate";
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			Queries.SubmitTemp(); //create TableTemp
      TablePay=Queries.TableTemp; //must create datatable obj since Queries.TempTable is static

// NEXT is TableIns, added by SPK 3/16/04
/*
Select claimpayment.checkdate,sum(claimproc.inspayamt) from claimpayment,claimproc where 
claimproc.claimpaymentnum = claimpayment.claimpaymentnum
&& claimpayment.checkdate < '2003-08-12'
group by claimpayment.checkdate order by procdate
*/
			Queries.CurReport=new Report();
			whereProv="&& (";
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				if(i>0){
					whereProv+=" ||";
				}
				whereProv+=" claimproc.provnum = '"
					+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"'";
			}
			whereProv+=")";
			Queries.CurReport.Query= "SELECT claimpayment.checkdate,SUM(claimproc.inspayamt) "
				+"FROM claimpayment,claimproc WHERE "
				+"claimproc.claimpaymentnum = claimpayment.claimpaymentnum "
				+"&& claimpayment.checkdate >= '" + dateFrom.ToString("yyyy-MM-dd")+"' "
				+"&& claimpayment.checkdate <= '" + dateTo.ToString("yyyy-MM-dd")+"' "
				+whereProv
				+" GROUP BY claimpayment.checkdate ORDER BY checkdate";
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			Queries.SubmitTemp(); //create TableIns
			TableIns=Queries.TableTemp; //must create datatable obj since Queries.TempTable is static
// End TableIns, SPK 3/16/04


// LAST is TableAdj
/*
SELECT adjustment.adjdate,CONCAT(patient.LName,', ',patient.FName,' ',patient.MiddleI),adjustment.adjtype,adjustment.adjnote,adjustment.adjamt
FROM adjustment,patient,definition 
WHERE adjustment.adjtype=definition.defnum && patient.patnum=adjustment.patnum
ORDER BY adjdate DESC
*/ 
  		Queries.CurReport=new Report();
			whereProv="&& (";
			for(int i=0;i<listProv.SelectedIndices.Count;i++){
				if(i>0){
					whereProv+=" ||";
				}
				whereProv+=" provnum = '"
					+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"'";
			}
			whereProv+=")";
			Queries.CurReport.Query="SELECT adjdate, SUM(adjamt) FROM adjustment WHERE "
				+"adjdate >= '" + dateFrom.ToString("yyyy-MM-dd")+"' "
				+"&& adjdate <= '" + dateTo.ToString("yyyy-MM-dd")+"' "
				+whereProv
				+" GROUP BY adjdate ORDER BY adjdate"; 
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			Queries.SubmitTemp(); //create TableTemp
      TableAdj=Queries.TableTemp; //must create datatable obj since Queries.TempTable is static 
//Now to fill Table Q from the 5 temp tables
			Queries.TableQ=new DataTable(null);//new table with 7 columns
			for(int i=0;i<7;i++){ //add columns
				Queries.TableQ.Columns.Add(new System.Data.DataColumn());//blank columns
			}
			Queries.CurReport.ColTotal=new double[Queries.TableQ.Columns.Count];
			double ptincome;//just used below in the tablepay loop for patient payments
			double insincome;//just used below in the tableIns loop for ins payments
			double adjust;
			DateTime[] dates=new DateTime[(dateTo-dateFrom).Days+1];
			//MessageBox.Show(dates.Length.ToString());
				//.ToString("yyyy-MM-dd")+"' "
				//	+"&& procdate <= '" + datePickerTo.Value
			for(int i=0;i<dates.Length;i++){//usually 31 days in loop
				dates[i]=dateFrom.AddDays(i);
				//create new row called 'row' based on structure of TableQ
				DataRow row = Queries.TableQ.NewRow();
				row[0]=dates[i].ToShortDateString();
				row[1]=dates[i].DayOfWeek.ToString();
				for(int j=0;j<TableCharge.Rows.Count;j++)  {
				  if(dates[i]
						//PIn.PDate(TableDate.Rows[i][0].ToString())
						==(PIn.PDate(TableCharge.Rows[j][0].ToString()))){
		 			  row[2]=PIn.PDouble(TableCharge.Rows[j][1].ToString()).ToString("F");
						Queries.CurReport.ColTotal[2]+=PIn.PDouble(TableCharge.Rows[j][1].ToString());
					}
   			}
        if (row[2].ToString().Equals(""))
          row[2]="0.00";
				for(int j=0; j<TableSched.Rows.Count; j++)  {
				  if(dates[i]
						//PIn.PDate(TableDate.Rows[i][0].ToString())
						==(PIn.PDate(TableSched.Rows[j][0].ToString()))){
			 	    row[3]=PIn.PDouble(TableSched.Rows[j][1].ToString()).ToString("F");
						Queries.CurReport.ColTotal[3]+=PIn.PDouble(TableSched.Rows[j][1].ToString());
					}
   			}
        if (row[3].ToString().Equals(""))
          row[3]="0.00";
				//INCOME AND ADJUSTMENTS:
				ptincome=0;						// spk
				insincome=0;
				adjust=0;
				for(int j=0; j<TablePay.Rows.Count; j++){
				  if(dates[i]
						//PIn.PDate(TableDate.Rows[i][0].ToString())
						==(PIn.PDate(TablePay.Rows[j][0].ToString()))){
						ptincome+=PIn.PDouble(TablePay.Rows[j][1].ToString());
					}																																						 
   			}
				// new TableIns, SPK
				for(int j=0; j<TableIns.Rows.Count; j++){
					if(dates[i]
						==(PIn.PDate(TableIns.Rows[j][0].ToString()))){
						insincome+=PIn.PDouble(TableIns.Rows[j][1].ToString());
					}																																						 
				}
				for(int j=0; j<TableAdj.Rows.Count; j++){
				  if(dates[i]
					//PIn.PDate(TableDate.Rows[i][0].ToString())
						==(PIn.PDate(TableAdj.Rows[j][0].ToString()))){
						adjust+=PIn.PDouble(TableAdj.Rows[j][1].ToString());
			 	    //row[5]=PIn.PDouble(TableAdj.Rows[j][1].ToString()).ToString("F");
						//Queries.CurReport.ColTotal[5]+=PIn.PDouble(TableAdj.Rows[j][1].ToString());
					}
   			}
				row[4]=ptincome.ToString("F");				// spk
				row[5]=insincome.ToString("F");				// spk
				row[6]=adjust.ToString("F");				// spk
				Queries.CurReport.ColTotal[4]+=ptincome;	// spk
				Queries.CurReport.ColTotal[5]+=insincome;	// spk
				Queries.CurReport.ColTotal[6]+=adjust;		// spk	
        if (row[4].ToString().Equals(""))
          row[4]="0.00";
				if (row[5].ToString().Equals(""))
          row[5]="0.00";
				if (row[6].ToString().Equals(""))
          row[6]="0.00";
				Queries.TableQ.Rows.Add(row);  //adds row to table Q
      }//end for row
			//done filling now set up table
			Queries.CurReport.ColWidth=new int[Queries.TableQ.Columns.Count];
			Queries.CurReport.ColPos=new int[Queries.TableQ.Columns.Count+1];
			Queries.CurReport.ColPos[0]=0;
			Queries.CurReport.ColCaption=new string[Queries.TableQ.Columns.Count];
			Queries.CurReport.ColAlign=new HorizontalAlignment[Queries.TableQ.Columns.Count];
			FormQuery2.ResetGrid();//necessary won't work without
			Queries.CurReport.Title="Production and Income";
			Queries.CurReport.SubTitle=new string[3];
			Queries.CurReport.SubTitle[0]=((Pref)Prefs.HList["PracticeTitle"]).ValueString;
			Queries.CurReport.SubTitle[1]=textDateFrom.Text+" - "
				+textDateTo.Text;
			bool allProv=true;
			string sProv="";
			for(int i=0;i<listProv.Items.Count;i++){
				if(listProv.SelectedIndices.Contains(i)){
					if(sProv!="")
						sProv+=", ";
					sProv+=Providers.List[i].Abbr;
				}
				else{
					allProv=false;
				}
			}
			if(allProv){
				Queries.CurReport.SubTitle[2]="All Providers";
			}
			else{
				Queries.CurReport.SubTitle[2]=sProv;
			}
			Queries.CurReport.Summary=new string[3];
			Queries.CurReport.Summary[0]=Lan.g(this,"Total production:")+" "
				+(Queries.CurReport.ColTotal[2]+Queries.CurReport.ColTotal[3]).ToString("c");
			Queries.CurReport.Summary[2]=Lan.g(this,"Total income:")+" "
				+(Queries.CurReport.ColTotal[4]+Queries.CurReport.ColTotal[5]).ToString("c");
			Queries.CurReport.ColPos[0]=20;
			Queries.CurReport.ColPos[1]=120;
			Queries.CurReport.ColPos[2]=200;
			Queries.CurReport.ColPos[3]=300;
			Queries.CurReport.ColPos[4]=400;
			Queries.CurReport.ColPos[5]=500;
			Queries.CurReport.ColPos[6]=600;
			Queries.CurReport.ColPos[7]=700;
			Queries.CurReport.ColCaption[0]="Date";
			Queries.CurReport.ColCaption[1]="Weekday";
			Queries.CurReport.ColCaption[2]="Production";
			Queries.CurReport.ColCaption[3]="Scheduled";
			Queries.CurReport.ColCaption[4]="Pt. Income";		// spk
			Queries.CurReport.ColCaption[5]="Ins. Income";		// spk
			Queries.CurReport.ColCaption[6]="Adjustments";		// spk
      Queries.CurReport.ColAlign[2]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[3]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[4]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[5]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[6]=HorizontalAlignment.Right;
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;		
		}

		

		
		//private void radioSingle_CheckedChanged(object sender, System.EventArgs e) {
		//	if(radioSingle.Checked==true){
		//		date2.Visible=false;
		//		labelTO.Visible=false;
		//	}
		//	else{
		//		date2.Visible=true;
		//		labelTO.Visible=true;
		//	}		
		//}

		
	}
}








