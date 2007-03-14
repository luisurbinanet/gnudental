using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace OpenDental{

	public class FormRpProduction : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RadioButton radioRange;
		private System.Windows.Forms.RadioButton radioSingle;
		private System.Windows.Forms.MonthCalendar date2;
		private System.Windows.Forms.MonthCalendar date1;
		private System.Windows.Forms.Label labelTO;

		private System.ComponentModel.Container components = null;
		private DataTable TableDate=   new DataTable();  //hold dates
		private DataTable TableCharge= new DataTable();  //charges
		private DataTable TablePay=    new DataTable();  //payments
		private DataTable TableAdj=    new DataTable();  //adjments
		private DataTable TableSched=  new DataTable();  //adjments
		private FormQuery FormQuery2;

		public FormRpProduction(){
			InitializeComponent();
 			Lan.C(this, new System.Windows.Forms.Control[] {
				panel1,
				radioRange,
				radioSingle,
				date2,
				date1,
				labelTO,
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.radioRange = new System.Windows.Forms.RadioButton();
			this.radioSingle = new System.Windows.Forms.RadioButton();
			this.date2 = new System.Windows.Forms.MonthCalendar();
			this.date1 = new System.Windows.Forms.MonthCalendar();
			this.labelTO = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(531, 328);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "Cancel";
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(531, 296);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 3;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.radioRange);
			this.panel1.Controls.Add(this.radioSingle);
			this.panel1.Location = new System.Drawing.Point(27, 16);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(104, 60);
			this.panel1.TabIndex = 0;
			// 
			// radioRange
			// 
			this.radioRange.Location = new System.Drawing.Point(8, 32);
			this.radioRange.Name = "radioRange";
			this.radioRange.Size = new System.Drawing.Size(88, 24);
			this.radioRange.TabIndex = 1;
			this.radioRange.Text = "Date Range";
			// 
			// radioSingle
			// 
			this.radioSingle.Checked = true;
			this.radioSingle.Location = new System.Drawing.Point(8, 8);
			this.radioSingle.Name = "radioSingle";
			this.radioSingle.Size = new System.Drawing.Size(88, 24);
			this.radioSingle.TabIndex = 0;
			this.radioSingle.TabStop = true;
			this.radioSingle.Text = "Single Date";
			this.radioSingle.CheckedChanged += new System.EventHandler(this.radioSingle_CheckedChanged);
			// 
			// date2
			// 
			this.date2.Location = new System.Drawing.Point(299, 112);
			this.date2.Name = "date2";
			this.date2.TabIndex = 2;
			this.date2.Visible = false;
			// 
			// date1
			// 
			this.date1.Location = new System.Drawing.Point(43, 112);
			this.date1.Name = "date1";
			this.date1.TabIndex = 1;
			// 
			// labelTO
			// 
			this.labelTO.Location = new System.Drawing.Point(259, 120);
			this.labelTO.Name = "labelTO";
			this.labelTO.Size = new System.Drawing.Size(24, 23);
			this.labelTO.TabIndex = 28;
			this.labelTO.Text = "TO";
			this.labelTO.Visible = false;
			// 
			// FormRpProduction
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(632, 366);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.date2);
			this.Controls.Add(this.date1);
			this.Controls.Add(this.labelTO);
			this.Name = "FormRpProduction";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Production and Income Report";
			this.Load += new System.EventHandler(this.FormProduction_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		private void FormProduction_Load(object sender, System.EventArgs e) {
			date1.SelectionStart=DateTime.Today;
			date2.SelectionStart=DateTime.Today;				
		}
/*  There are 5 temp tables with 5 ugly query statements.  
 *  TableDate: Using 4 Unions I have data holding all dates with payment/charge/adjustment. 
 *  TableCharge: Holds sum of all charges for a certain date. Must test dates to queriesDate 
 *  TableSched: Holds Scheduled but not charged procedures 
 *  TablePay: Holds sum of all payments (patient and insurance) for a certain date.
 *            Must test dates to queriesDate
 *  TableAdj: Holds sum of all adjustments for a certain date.  Must test dates to queriesDate
 * GROUP BY is used to group dates together so that amounts are summed for each date
 */
		private void butOK_Click(object sender, System.EventArgs e) {
      //double nothing = 0;  //nothing is value if there is no payment/adj/charge for a spec date
			//FIRST is TableDate
/*
Select procdate From procedurelog 
where procdate <= '2003-05-12' Group By procdate
union
select FROM_DAYS(TO_DAYS(Appointment.AptDateTime)) AS schedDate FROM Appointment
where FROM_DAYS(TO_DAYS(Appointment.AptDateTime)) <= '2003-05-12' Group By schedDate
union 
select paydate from payment
where paydate <= '2003-05-12' Group By paydate
union 
select checkdate from claimpayment
where checkdate <= '2003-05-12' Group By checkdate
union 
select adjdate from adjustment
where adjdate <= '2003-05-12'
order by procdate desc			
*/	 
			Queries.CurReport=new Report();
			Queries.CurReport.Query= "SELECT procdate FROM procedurelog WHERE ";
			if(radioRange.Checked){
				Queries.CurReport.Query
					+="procdate >= '" + date1.SelectionStart.ToString("yyyy-MM-dd")+"' "
					+"&& procdate <= '" + date2.SelectionStart.ToString("yyyy-MM-dd")+"'";
			}
			else{
				Queries.CurReport.Query
					+="procdate = '" + date1.SelectionStart.ToString("yyyy-MM-dd")+"'";
			}
 			Queries.CurReport.Query += " GROUP BY procdate UNION SELECT "
				+"FROM_DAYS(TO_DAYS(appointment.AptDateTime)) AS schedDate FROM appointment WHERE "; 
			if(radioRange.Checked){
				Queries.CurReport.Query
					+="FROM_DAYS(TO_DAYS(appointment.AptDateTime)) >= '" 
					+date1.SelectionStart.ToString("yyyy-MM-dd")+"' "
					+"&& FROM_DAYS(TO_DAYS(appointment.AptDateTime)) <= '" 
					+date2.SelectionStart.ToString("yyyy-MM-dd")+"'";
			}
			else{
				Queries.CurReport.Query
					+="FROM_DAYS(TO_DAYS(appointment.AptDateTime)) = '" 
					+date1.SelectionStart.ToString("yyyy-MM-dd")+"'";
			}					
			Queries.CurReport.Query += " GROUP BY schedDate UNION SELECT paydate FROM payment WHERE "; 
			if(radioRange.Checked){
				Queries.CurReport.Query
					+="paydate >= '"+date1.SelectionStart.ToString("yyyy-MM-dd")+"' "
					+"&& paydate <= '"+date2.SelectionStart.ToString("yyyy-MM-dd")+"'";
			}
			else{
				Queries.CurReport.Query
					+="paydate = '" + date1.SelectionStart.ToString("yyyy-MM-dd")+"'";
			}
      Queries.CurReport.Query += " GROUP BY paydate UNION SELECT checkdate FROM claimpayment WHERE "; 
			if(radioRange.Checked){
				Queries.CurReport.Query
					+="checkdate >= '" + date1.SelectionStart.ToString("yyyy-MM-dd")+"' "
					+"&& checkdate <= '" + date2.SelectionStart.ToString("yyyy-MM-dd")+"'";
			}
			else{
				Queries.CurReport.Query
					+="checkdate = '" + date1.SelectionStart.ToString("yyyy-MM-dd")+"'";
			}
      Queries.CurReport.Query += " GROUP BY checkdate UNION SELECT adjdate FROM adjustment WHERE "; 
			if(radioRange.Checked==true){
				Queries.CurReport.Query
					+="adjdate >= '" + date1.SelectionStart.ToString("yyyy-MM-dd")+"' "
					+"&& adjdate <= '" + date2.SelectionStart.ToString("yyyy-MM-dd")+"'";
			}
			else{
				Queries.CurReport.Query
					+="adjdate = '" + date1.SelectionStart.ToString("yyyy-MM-dd")+"'";
			}
			Queries.CurReport.Query += " ORDER BY procdate ";
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			Queries.SubmitTemp(); //create TableTemp
      TableDate=Queries.TableTemp; //must create datatable obj since Queries.TempTable is static

// NEXT is TableCharge
/*
Select procdate, sum(procfee) From procedurelog
Group By procdate Order by procdate desc  
*/
			Queries.CurReport=new Report();
			Queries.CurReport.Query="SELECT procdate, SUM(procfee) FROM procedurelog WHERE ";
			if(radioRange.Checked){
				Queries.CurReport.Query
					+="procdate >= '" + date1.SelectionStart.ToString("yyyy-MM-dd")+"' "
					+"&& procdate <= '" + date2.SelectionStart.ToString("yyyy-MM-dd")+"'";
			}
			else{
				Queries.CurReport.Query
					+="procdate = '" + date1.SelectionStart.ToString("yyyy-MM-dd")+"'";
			}
			Queries.CurReport.Query+=" && procstatus = '2'"
				+" GROUP BY procdate ORDER BY procdate"; 
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
			Queries.CurReport.Query= "SELECT FROM_DAYS(TO_DAYS(appointment.AptDateTime)) AS "
			  +"SchedDate,SUM(procedurelog.procfee) FROM appointment, procedurelog WHERE "
        +"appointment.aptnum = procedurelog.aptnum && appointment.AptStatus = 1 || "//stat=scheduled
        +"appointment.AptStatus = 4 && ";//or stat=ASAP
			if(radioRange.Checked==true){
				Queries.CurReport.Query
					+="FROM_DAYS(TO_DAYS(appointment.AptDateTime)) >= '"
					+date1.SelectionStart.ToString("yyyy-MM-dd")+"' "
					+"&& FROM_DAYS(TO_DAYS(appointment.AptDateTime)) <= '"
					+date2.SelectionStart.ToString("yyyy-MM-dd")+"'";
			}
			else{
				Queries.CurReport.Query
					+="FROM_DAYS(TO_DAYS(appointment.AptDateTime)) = '"
					+date1.SelectionStart.ToString("yyyy-MM-dd")+"'";
			}
      Queries.CurReport.Query+=" GROUP BY SchedDate ORDER BY SchedDate"; 
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			Queries.SubmitTemp(); //create TableTemp
      TableSched=Queries.TableTemp; //must create datatable obj since Queries.TempTable is static

// NEXT is TablePay
/*
Select paydate,sum(payamt) from payment where paydate < '2003-05-12'
group by paydate union all 
Select checkdate,sum(checkamt) from claimpayment where checkdate < '2003-05-12'
group by checkdate order by paydate desc
*/
			Queries.CurReport=new Report();
			Queries.CurReport.Query= "SELECT paydate,SUM(payamt) FROM payment WHERE ";
			if(radioRange.Checked==true){
				Queries.CurReport.Query
					+="paydate >= '" + date1.SelectionStart.ToString("yyyy-MM-dd")+"' "
					+"&& paydate <= '" + date2.SelectionStart.ToString("yyyy-MM-dd")+"'";
			}
			else{
				Queries.CurReport.Query
					+="paydate = '" + date1.SelectionStart.ToString("yyyy-MM-dd")+"'";
			}
      Queries.CurReport.Query+= " GROUP BY paydate UNION ALL SELECT checkdate,SUM(checkamt) FROM claimpayment WHERE "; 
			if(radioRange.Checked==true){
				Queries.CurReport.Query
					+="checkdate >= '" + date1.SelectionStart.ToString("yyyy-MM-dd")+"' "
					+"&& checkdate <= '" + date2.SelectionStart.ToString("yyyy-MM-dd")+"'";
			}
			else{
				Queries.CurReport.Query
					+="checkdate = '" + date1.SelectionStart.ToString("yyyy-MM-dd")+"'";
			}
      Queries.CurReport.Query+= " GROUP BY checkdate ORDER BY paydate";

			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			Queries.SubmitTemp(); //create TableTemp
      TablePay=Queries.TableTemp; //must create datatable obj since Queries.TempTable is static

// LAST is TableAdj
/*
SELECT adjustment.adjdate,CONCAT(patient.LName,', ',patient.FName,' ',patient.MiddleI),adjustment.adjtype,adjustment.adjnote,adjustment.adjamt
FROM adjustment,patient,definition 
WHERE adjustment.adjtype=definition.defnum && patient.patnum=adjustment.patnum
ORDER BY adjdate DESC
*/ 
  		Queries.CurReport=new Report();
			Queries.CurReport.Query="SELECT adjdate, SUM(adjamt) FROM adjustment WHERE ";
			if(radioRange.Checked==true){
				Queries.CurReport.Query
					+="adjdate >= '" + date1.SelectionStart.ToString("yyyy-MM-dd")+"' "
					+"&& adjdate <= '" + date2.SelectionStart.ToString("yyyy-MM-dd")+"'";
			}
			else{
				Queries.CurReport.Query
					+="adjdate = '" + date1.SelectionStart.ToString("yyyy-MM-dd")+"'";
			}
      Queries.CurReport.Query+= " GROUP BY adjdate ORDER BY adjdate"; 

			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			Queries.SubmitTemp(); //create TableTemp
      TableAdj=Queries.TableTemp; //must create datatable obj since Queries.TempTable is static 
			//Now to fill Table Q from the 4 temp tables
			Queries.TableQ=new DataTable(null);//new table with 4 columns
			for(int i=0;i<6;i++){ //add columns
				Queries.TableQ.Columns.Add(new System.Data.DataColumn());//blank columns
			}
			Queries.CurReport.ColTotal=new double[Queries.TableQ.Columns.Count];
			for(int i=0;i<TableDate.Rows.Count;i++){//loop through data rows
				//create new row called 'row' based on structure of TableQ
				DataRow row = Queries.TableQ.NewRow();
				row[0]=PIn.PDate(TableDate.Rows[i][0].ToString()).ToString("d");
				row[1]=PIn.PDate(TableDate.Rows[i][0].ToString()).DayOfWeek.ToString();
				for(int j=0;j<TableCharge.Rows.Count;j++)  {
				  if(PIn.PDate(TableDate.Rows[i][0].ToString())
						==(PIn.PDate(TableCharge.Rows[j][0].ToString()))){
		 			  row[2]=PIn.PDouble(TableCharge.Rows[j][1].ToString()).ToString("F");
						Queries.CurReport.ColTotal[2]+=PIn.PDouble(TableCharge.Rows[j][1].ToString());
					}
   			}
        if (row[2].ToString().Equals(""))
          row[2]="0.00";
				for(int j=0; j<TableSched.Rows.Count; j++)  {
				  if (PIn.PDate(TableDate.Rows[i][0].ToString())
						==(PIn.PDate(TableSched.Rows[j][0].ToString()))){
			 	    row[3]=PIn.PDouble(TableSched.Rows[j][1].ToString()).ToString("F");
						Queries.CurReport.ColTotal[3]+=PIn.PDouble(TableSched.Rows[j][1].ToString());
					}
   			}
        if (row[3].ToString().Equals(""))
          row[3]="0.00";
				for(int j=0; j<TablePay.Rows.Count; j++)  {
				  if (PIn.PDate(TableDate.Rows[i][0].ToString())
						==(PIn.PDate(TablePay.Rows[j][0].ToString()))){
			 	    row[4]=PIn.PDouble(TablePay.Rows[j][1].ToString()).ToString("F");
						Queries.CurReport.ColTotal[4]+=PIn.PDouble(TablePay.Rows[j][1].ToString());
					}																																						 
   			}
        if (row[4].ToString().Equals(""))
          row[4]="0.00";
        for(int j=0; j<TableAdj.Rows.Count; j++)  {
				  if (PIn.PDate(TableDate.Rows[i][0].ToString())
						==(PIn.PDate(TableAdj.Rows[j][0].ToString()))){
			 	    row[5]=PIn.PDouble(TableAdj.Rows[j][1].ToString()).ToString("F");
						Queries.CurReport.ColTotal[5]+=PIn.PDouble(TableAdj.Rows[j][1].ToString());
					}
   			}
        if (row[5].ToString().Equals(""))
          row[5]="0.00";
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
			if(radioRange.Checked==true){
				Queries.CurReport.SubTitle[1]=date1.SelectionStart.ToString("d")+" - "
					+date2.SelectionStart.ToString("d");
			}
			else{
				Queries.CurReport.SubTitle[1]=date1.SelectionStart.ToString("d");
			}
			Queries.CurReport.ColPos[0]=20;
			Queries.CurReport.ColPos[1]=120;
			Queries.CurReport.ColPos[2]=180;
			Queries.CurReport.ColPos[3]=320;
			Queries.CurReport.ColPos[4]=460;
			Queries.CurReport.ColPos[5]=600;
			Queries.CurReport.ColPos[6]=740;
			Queries.CurReport.ColCaption[0]="Date";
			Queries.CurReport.ColCaption[1]="Weekday";
			Queries.CurReport.ColCaption[2]="Production";
			Queries.CurReport.ColCaption[3]="Scheduled";
			Queries.CurReport.ColCaption[4]="Income";
			Queries.CurReport.ColCaption[5]="Adjustments";
      Queries.CurReport.ColAlign[2]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[3]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[4]=HorizontalAlignment.Right;
			Queries.CurReport.ColAlign[5]=HorizontalAlignment.Right;
			Queries.CurReport.Summary=new string[3];
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;		
		}
		private void radioSingle_CheckedChanged(object sender, System.EventArgs e) {
			if(radioSingle.Checked==true){
				date2.Visible=false;
				labelTO.Visible=false;
			}
			else{
				date2.Visible=true;
				labelTO.Visible=true;
			}		
		}
	}
}
