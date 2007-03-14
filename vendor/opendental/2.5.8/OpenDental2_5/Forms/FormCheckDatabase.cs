using System;
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
		private System.Windows.Forms.Button butClose;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.CheckBox checkDefaultProv;
		private System.Windows.Forms.CheckBox checkInvalidTooth;
		private System.Windows.Forms.Button buttonCheck;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.CheckBox checkCorrupt;
		private System.Drawing.Printing.PrintDocument pd2;
		//private Queries Queries2;
		private string logData;

		///<summary></summary>
		public FormCheckDatabase()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.butClose = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.checkDefaultProv = new System.Windows.Forms.CheckBox();
			this.checkInvalidTooth = new System.Windows.Forms.CheckBox();
			this.buttonCheck = new System.Windows.Forms.Button();
			this.checkCorrupt = new System.Windows.Forms.CheckBox();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
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
			this.textBox1.Location = new System.Drawing.Point(34, 14);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(707, 31);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = @"This tool will check the entire database for any possible corruption or improper settings.  You will generally be prompted before any changes are made to the database.  Currently, the following tests are run.   Uncheck any items that you don't want tested:";
			// 
			// checkDefaultProv
			// 
			this.checkDefaultProv.Checked = true;
			this.checkDefaultProv.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkDefaultProv.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkDefaultProv.Location = new System.Drawing.Point(30, 62);
			this.checkDefaultProv.Name = "checkDefaultProv";
			this.checkDefaultProv.Size = new System.Drawing.Size(679, 22);
			this.checkDefaultProv.TabIndex = 2;
			this.checkDefaultProv.Text = "Verify that a Default Provider and Billing Type have been selected in Practice se" +
				"tup.";
			// 
			// checkInvalidTooth
			// 
			this.checkInvalidTooth.Checked = true;
			this.checkInvalidTooth.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkInvalidTooth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkInvalidTooth.Location = new System.Drawing.Point(30, 92);
			this.checkInvalidTooth.Name = "checkInvalidTooth";
			this.checkInvalidTooth.Size = new System.Drawing.Size(724, 31);
			this.checkInvalidTooth.TabIndex = 3;
			this.checkInvalidTooth.Text = "Check for invalid tooth numbers and fix them.  Lowercase primary tooth numbers wi" +
				"ll be changed to uppercase.  If any other invalid tooth numbers are located, you" +
				" will be prompted before any changes are made.";
			// 
			// buttonCheck
			// 
			this.buttonCheck.FlatStyle = System.Windows.Forms.FlatStyle.System;
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
			this.checkCorrupt.Location = new System.Drawing.Point(30, 134);
			this.checkCorrupt.Name = "checkCorrupt";
			this.checkCorrupt.Size = new System.Drawing.Size(709, 24);
			this.checkCorrupt.TabIndex = 6;
			this.checkCorrupt.Text = "Check all tables for file or index corruption.";
			// 
			// FormCheckDatabase
			// 
			this.AcceptButton = this.buttonCheck;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(763, 371);
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
			Queries.CurReport=new Report();
		}

		private void buttonCheck_Click(object sender, System.EventArgs e) {
			if(checkDefaultProv.Checked){
				VerifyProv();
			}
			if(checkInvalidTooth.Checked){
				VerifyToothNums();
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
						//Patients Patients2=new Patients();
						Patients.GetLim(Convert.ToInt32(Queries.TableQ.Rows[i][2].ToString()));
						switch(MessageBox.Show("Invalid tooth number found for "+Patients.LimName+". Convert "
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
