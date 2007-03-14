using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace OpenDental
{
	/// <summary>
	/// Summary description for FormCheckDatabase.
	/// </summary>
	public class FormCheckDatabase : System.Windows.Forms.Form
	{
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.CheckBox checkDefaultProv;
		private System.Windows.Forms.CheckBox checkInvalidTooth;
		private OpenDental.UI.Button buttonCheck;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.CheckBox checkCorrupt;
		private System.Drawing.Printing.PrintDocument pd2;
		private System.Windows.Forms.CheckBox checkCodes;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.CheckBox checkDates;
		private System.Windows.Forms.CheckBox checkInsPlans;
		//private Queries Queries2;
		private string logData;

		///<summary></summary>
		public FormCheckDatabase()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[]{
				this.textBox1,
				this.textBox2
			}); //*Ann
			Lan.F(this);
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
			this.butClose = new OpenDental.UI.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.checkDefaultProv = new System.Windows.Forms.CheckBox();
			this.checkInvalidTooth = new System.Windows.Forms.CheckBox();
			this.buttonCheck = new OpenDental.UI.Button();
			this.checkCorrupt = new System.Windows.Forms.CheckBox();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.checkCodes = new System.Windows.Forms.CheckBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.checkDates = new System.Windows.Forms.CheckBox();
			this.checkInsPlans = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(655, 317);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(87, 26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(32, 14);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(707, 31);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "This tool will check the entire database for any possible corruption or improper " +
				"settings.  You will generally be prompted before any changes are made to the dat" +
				"abase.";
			// 
			// checkDefaultProv
			// 
			this.checkDefaultProv.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkDefaultProv.Location = new System.Drawing.Point(30, 85);
			this.checkDefaultProv.Name = "checkDefaultProv";
			this.checkDefaultProv.Size = new System.Drawing.Size(679, 22);
			this.checkDefaultProv.TabIndex = 2;
			this.checkDefaultProv.Text = "Verify that a Default Provider and Billing Type have been selected in Practice se" +
				"tup.";
			// 
			// checkInvalidTooth
			// 
			this.checkInvalidTooth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkInvalidTooth.Location = new System.Drawing.Point(30, 110);
			this.checkInvalidTooth.Name = "checkInvalidTooth";
			this.checkInvalidTooth.Size = new System.Drawing.Size(724, 31);
			this.checkInvalidTooth.TabIndex = 3;
			this.checkInvalidTooth.Text = "Check for invalid tooth numbers and fix them.  Lowercase primary tooth numbers wi" +
				"ll be changed to uppercase.  If any other invalid tooth numbers are located, you" +
				" will be prompted before any changes are made.";
			// 
			// buttonCheck
			// 
			this.buttonCheck.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.buttonCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCheck.Autosize = true;
			this.buttonCheck.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonCheck.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonCheck.Location = new System.Drawing.Point(655, 278);
			this.buttonCheck.Name = "buttonCheck";
			this.buttonCheck.Size = new System.Drawing.Size(87, 26);
			this.buttonCheck.TabIndex = 5;
			this.buttonCheck.Text = "C&heck Now";
			this.buttonCheck.Click += new System.EventHandler(this.buttonCheck_Click);
			// 
			// checkCorrupt
			// 
			this.checkCorrupt.Checked = true;
			this.checkCorrupt.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkCorrupt.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkCorrupt.Location = new System.Drawing.Point(30, 225);
			this.checkCorrupt.Name = "checkCorrupt";
			this.checkCorrupt.Size = new System.Drawing.Size(709, 24);
			this.checkCorrupt.TabIndex = 6;
			this.checkCorrupt.Text = "Check all tables for file or index corruption.";
			// 
			// checkCodes
			// 
			this.checkCodes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkCodes.Location = new System.Drawing.Point(30, 171);
			this.checkCodes.Name = "checkCodes";
			this.checkCodes.Size = new System.Drawing.Size(709, 24);
			this.checkCodes.TabIndex = 8;
			this.checkCodes.Text = "Add any missing procedure codes.";
			// 
			// textBox2
			// 
			this.textBox2.BackColor = System.Drawing.SystemColors.Control;
			this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox2.Location = new System.Drawing.Point(32, 53);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(707, 18);
			this.textBox2.TabIndex = 9;
			this.textBox2.Text = "Currently, the following tests are run.   Uncheck any items that you don\'t want t" +
				"ested:";
			// 
			// checkDates
			// 
			this.checkDates.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkDates.Location = new System.Drawing.Point(30, 144);
			this.checkDates.Name = "checkDates";
			this.checkDates.Size = new System.Drawing.Size(709, 24);
			this.checkDates.TabIndex = 10;
			this.checkDates.Text = "Fix any invalid dates.  This will speed up the program after converting from anot" +
				"her dental software.";
			// 
			// checkInsPlans
			// 
			this.checkInsPlans.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkInsPlans.Location = new System.Drawing.Point(30, 198);
			this.checkInsPlans.Name = "checkInsPlans";
			this.checkInsPlans.Size = new System.Drawing.Size(709, 24);
			this.checkInsPlans.TabIndex = 11;
			this.checkInsPlans.Text = "Look for insurance plans that no longer exist.";
			// 
			// FormCheckDatabase
			// 
			this.AcceptButton = this.buttonCheck;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(763, 371);
			this.Controls.Add(this.checkInsPlans);
			this.Controls.Add(this.checkDates);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.checkCodes);
			this.Controls.Add(this.checkCorrupt);
			this.Controls.Add(this.buttonCheck);
			this.Controls.Add(this.checkInvalidTooth);
			this.Controls.Add(this.checkDefaultProv);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.butClose);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormCheckDatabase";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Check Database Integrity";
			this.Load += new System.EventHandler(this.FormCheckDatabase_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormCheckDatabase_Load(object sender, System.EventArgs e) {
			//Queries2=new Queries();
			Queries.CurReport=new ReportOld();
		}

		private void buttonCheck_Click(object sender, System.EventArgs e) {
			if(checkDefaultProv.Checked){
				VerifyProv();
			}
			if(checkInvalidTooth.Checked){
				VerifyToothNums();
			}
			if(checkDates.Checked){
				VerifyDates();
			}
			if(checkCodes.Checked){
				AddCodes();
			}
			if(checkInsPlans.Checked){
				VerifyInsPlans();
			}
			if(checkCorrupt.Checked){
				VerifyTables();
			}
			MessageBox.Show(Lan.g(this,"Done"));
		}

		private void VerifyProv(){
			Queries.CurReport.Query="SELECT valuestring FROM preference WHERE "
				+"prefname = 'PracticeDefaultProv'";
			Queries.SubmitCur();
			if(Queries.TableQ.Rows[0][0].ToString()==""){
				if(MessageBox.Show("Default provider not set.  Click OK to set the default provider to the first provider in the list.","",MessageBoxButtons.OKCancel)==DialogResult.OK){
					Queries.CurReport.Query="SELECT provnum FROM provider "
						+"WHERE ishidden = 0 "
						+"ORDER BY itemorder LIMIT 1";
					Queries.SubmitCur();
					Queries.CurReport.Query="UPDATE preference SET valuestring = '"
						+Queries.TableQ.Rows[0][0].ToString()+"' "
						+"WHERE prefname = 'PracticeDefaultProv'";
					Queries.SubmitNonQ();
				}
			}
			Queries.CurReport.Query="SELECT valuestring FROM preference WHERE "
				+"prefname = 'PracticeDefaultBillType'";
			Queries.SubmitCur();
			if(Queries.TableQ.Rows[0][0].ToString()==""){
				if(MessageBox.Show("Default Billing Type not set.  Click OK to set the billing type to the first item in the list.","",MessageBoxButtons.OKCancel)==DialogResult.OK){
					Queries.CurReport.Query="SELECT defnum FROM definition "
						+"WHERE category = 4 AND ishidden = 0 "
						+"ORDER BY itemorder LIMIT 1";
					Queries.SubmitCur();
					Queries.CurReport.Query="UPDATE preference SET valuestring = '"
						+Queries.TableQ.Rows[0][0].ToString()+"' "
						+"WHERE prefname = 'PracticeDefaultBillType'";
					Queries.SubmitNonQ();
				}
			}
			MessageBox.Show("Default Provider and Billing Type verified");
		}

		private void VerifyToothNums(){
			Patient Lim;
			Queries.CurReport.Query="SELECT procnum,toothnum,patnum FROM procedurelog";
			Queries.SubmitCur();
			string toothNum;
			for(int i=0;i<Queries.TableQ.Rows.Count;i++){
				toothNum=Queries.TableQ.Rows[i][1].ToString();
				if(toothNum == "")
					continue;
				if(!Tooth.IsValidDB(toothNum)){
					if(string.CompareOrdinal(toothNum,"a")>=0
						&& string.CompareOrdinal(toothNum,"t")<=0){
						UpdateToothNum(Queries.TableQ.Rows[i][0].ToString(),toothNum.ToUpper());
						continue;
					}//lower case to upper
					else{
						Lim=Patients.GetLim(Convert.ToInt32(Queries.TableQ.Rows[i][2].ToString()));
						switch(MessageBox.Show("Invalid tooth number found for "
							+Lim.GetNameLF()+". Convert "
							+"tooth number "+toothNum+" to tooth number 1?","",MessageBoxButtons.YesNoCancel)){
							case DialogResult.Cancel:
                return;
							case DialogResult.No:
								continue;
							case DialogResult.Yes:
								UpdateToothNum(Queries.TableQ.Rows[i][0].ToString(),"1");
								break;
						}//switch convert?
					}//not lowercase primary
				}//not valid
			}//for i
			MessageBox.Show("Check for invalid tooth numbers complete.  Number of records checked: "
				+Queries.TableQ.Rows.Count.ToString());
		}

		private void UpdateToothNum(string procNum,string newToothNum){
			Queries.CurReport.Query="UPDATE procedurelog SET toothnum = '"
				+newToothNum+"' WHERE procnum = '"+procNum+"'";
			Queries.SubmitNonQ();
		}

		private void VerifyDates(){
			string[] commands=new string[]
			{
				"UPDATE adjustment SET AdjDate='0001-01-01' WHERE AdjDate='0000-00-00'"
				,"UPDATE appointment SET AptDateTime='0001-01-01 00:00:00' "
					+"WHERE AptDateTime LIKE '0000-00-00%'"
				,"UPDATE claim SET DateService='0001-01-01' WHERE DateService='0000-00-00'"
				,"UPDATE claim SET DateSent='0001-01-01' WHERE DateSent='0000-00-00'"
				,"UPDATE claim SET DateReceived='0001-01-01' WHERE DateReceived='0000-00-00'"
				,"UPDATE claim SET PriorDate='0001-01-01' WHERE PriorDate='0000-00-00'"
				,"UPDATE claim SET AccidentDate='0001-01-01' WHERE AccidentDate='0000-00-00'"
				,"UPDATE claim SET OrthoDate='0001-01-01' WHERE OrthoDate='0000-00-00'"
				,"UPDATE claimpayment SET CheckDate='0001-01-01' WHERE CheckDate='0000-00-00'"
				,"UPDATE claimproc SET DateCP='0001-01-01' WHERE DateCP='0000-00-00'"
				,"UPDATE claimproc SET ProcDate='0001-01-01' WHERE ProcDate='0000-00-00'"
				,"UPDATE insplan SET DateEffective='0001-01-01' WHERE DateEffective='0000-00-00'"
				,"UPDATE insplan SET DateTerm='0001-01-01' WHERE DateTerm='0000-00-00'"
				,"UPDATE insplan SET RenewMonth='1' WHERE RenewMonth='0'"
				,"UPDATE patient SET Birthdate='0001-01-01' WHERE Birthdate='0000-00-00'"
				,"UPDATE patient SET DateFirstVisit='0001-01-01' WHERE DateFirstVisit='0000-00-00'"
				,"UPDATE procedurelog SET ProcDate='0001-01-01' WHERE ProcDate='0000-00-00'"
				,"UPDATE procedurelog SET DateOriginalProsth='0001-01-01' "
					+"WHERE DateOriginalProsth='0000-00-00'"
				,"UPDATE procedurelog SET DateLocked='0001-01-01' WHERE DateLocked='0000-00-00'"
				,"UPDATE recall SET DateDueCalc='0001-01-01' WHERE DateDueCalc='0000-00-00'"
				,"UPDATE recall SET DateDue='0001-01-01' WHERE DateDue='0000-00-00'"
				,"UPDATE recall SET DatePrevious='0001-01-01' WHERE DatePrevious='0000-00-00'"
			};
			DataConnection dcon=new DataConnection();
			int rowsChanged=dcon.NonQ(commands);
			MessageBox.Show(Lan.g(this,"Dates fixed. Rows changed:")+" "+rowsChanged.ToString());
		}

		private void AddCodes(){
			Conversions.SelectText=@"SELECT DISTINCT procedurelog.ADACode
				FROM procedurelog
				LEFT JOIN procedurecode ON procedurelog.ADACode=procedurecode.ADACode
				WHERE procedurecode.ADACode IS NULL";
			Conversions.SubmitSelect();
			string myADA;
			for(int i=0;i<Conversions.TableQ.Rows.Count;i++){
				myADA=PIn.PString(Conversions.TableQ.Rows[i][0].ToString());
				ProcedureCodes.Cur=new ProcedureCode();
				ProcedureCodes.Cur.ADACode=myADA;
				ProcedureCodes.Cur.Descript=myADA;
				ProcedureCodes.Cur.AbbrDesc=myADA;
				ProcedureCodes.Cur.ProcTime="/X/";
				ProcedureCodes.Cur.ProcCat=Defs.Short[(int)DefCat.ProcCodeCats][0].DefNum;
				ProcedureCodes.Cur.TreatArea=TreatmentArea.Mouth;
				ProcedureCodes.InsertCur();
			}
			MessageBox.Show("Codes added: "+Conversions.TableQ.Rows.Count.ToString());
			if(Conversions.TableQ.Rows.Count>0){
				DataValid.SetInvalid(InvalidTypes.ProcCodes);
			}
		}

		private void VerifyInsPlans(){
			string command=@"SELECT PatNum FROM patient
				LEFT JOIN insplan on patient.PriPlanNum=insplan.PlanNum
				WHERE patient.PriPlanNum != 0
				AND insplan.PlanNum IS NULL";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++){
				command="UPDATE patient set PriPlanNum=0 "
					+"WHERE PatNum="+table.Rows[i][0].ToString();
				dcon.NonQ(command);
			}
			command=@"SELECT ClaimProcNum FROM claimproc
				LEFT JOIN insplan ON claimproc.PlanNum=insplan.PlanNum
				WHERE insplan.PlanNum IS NULL";
			table=dcon.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++){
				command="DELETE FROM claimproc "
					+"WHERE ClaimProcNum="+table.Rows[i][0].ToString();
				dcon.NonQ(command);
			}
			command=@"SELECT ClaimNum FROM claim
				LEFT JOIN insplan ON claim.PlanNum=insplan.PlanNum
				WHERE insplan.PlanNum IS NULL";
			table=dcon.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++){
				command="DELETE FROM claim "
					+"WHERE ClaimNum="+table.Rows[i][0].ToString();
				dcon.NonQ(command);
			}
			MessageBox.Show("Missing plans fixed.");
		}

		private void VerifyTables(){
			Queries.CurReport.Query="SHOW TABLES";
			Queries.SubmitCur();
			string[] tableName=new string[Queries.TableQ.Rows.Count];
			int lastRow;
			ArrayList corruptTables=new ArrayList();
			for(int i=0;i<Queries.TableQ.Rows.Count;i++){
				tableName[i]=Queries.TableQ.Rows[i][0].ToString();
			}
			for(int i=0;i<tableName.Length;i++){
				Queries.CurReport.Query="CHECK TABLE "+tableName[i];
				Queries.SubmitCur();
				lastRow=Queries.TableQ.Rows.Count-1;
				if(Queries.TableQ.Rows[lastRow][3].ToString()!="OK"){
					corruptTables.Add(tableName[i]);
				}
			}
			if(corruptTables.Count==0){
				MessageBox.Show(Lan.g(this,"You have no corrupted tables."));
				return;
			}
			string corruptS="";
			for(int i=0;i<corruptTables.Count;i++){
				corruptS+=corruptTables[i]+"\r";
			}
			if(MessageBox.Show(Lan.g(this,"You have the following corrupt tables:")+"\r"
				+corruptS
				+Lan.g(this,"It is strongly suggested that you select Cancel and make a backup before continuing.  Select OK to repair tables."),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;	
			}
			for(int i=0;i<corruptTables.Count;i++){
				Queries.CurReport.Query="REPAIR TABLE "+corruptTables[i];
				Queries.SubmitCur();
				SaveToLog((string)corruptTables[i]);
			}
			PrintLog();
			MessageBox.Show(Lan.g(this,"The tables have probably been repaired, but the repairlog must be analyzed.  Please go to www.open-dent.com and do a search for 'corruption'.  Please follow those instructions.  Do not click OK until you write down this information."));
		}

		private void SaveToLog(string corruptTable){
			FileStream fs=new FileStream("RepairLog.txt",FileMode.Append,FileAccess.Write,FileShare.Read);
			StreamWriter sw=new StreamWriter(fs);
      String line=""; 
			line=corruptTable+" "+DateTime.Now.ToString()+"\r\n";
			sw.Write(line);
			logData+=line;
			for(int i=0;i<Queries.TableQ.Rows.Count;i++){
				line="";
				for(int j=0;j<Queries.TableQ.Columns.Count;j++){
					line+=Queries.TableQ.Rows[i][j].ToString()+",";
				}
				line+="\r\n";
				sw.Write(line);
				logData+=line;
			}
			sw.Close();
			sw=null;
			fs.Close();
			fs=null;
		}

		private void PrintLog(){
			pd2 = new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			pd2.DefaultPageSettings.Margins=new Margins(40,50,50,60);
			//pagesPrinted=0;
			//linesPrinted=0;
			try{
				pd2.Print();
			}
			catch{
				MessageBox.Show("Printer not available");
			}
		}

		private void pd2_PrintPage(object sender, PrintPageEventArgs ev){//raised for each page to be printed.
			int yPos = ev.MarginBounds.Top;
			int xPos=ev.MarginBounds.Left;
			ev.Graphics.DrawString(logData,new Font("Arial",11),Brushes.Black,xPos,yPos);
			ev.HasMorePages = false;
		}
		
		

		


	}
}
