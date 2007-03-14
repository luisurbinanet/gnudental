using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormRpOutInsClaims : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private FormQuery FormQuery2;
		private System.Windows.Forms.Label labelDaysOld;
		private int daysOld=0;
		private OpenDental.ValidNum textDaysOld;
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.Container components = null;

		public FormRpOutInsClaims(){
			InitializeComponent();
 			Lan.C(this, new System.Windows.Forms.Control[] {
				labelDaysOld,
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
			this.labelDaysOld = new System.Windows.Forms.Label();
			this.textDaysOld = new OpenDental.ValidNum();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(365, 144);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 1;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(365, 184);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "Cancel";
			// 
			// labelDaysOld
			// 
			this.labelDaysOld.Location = new System.Drawing.Point(40, 66);
			this.labelDaysOld.Name = "labelDaysOld";
			this.labelDaysOld.Size = new System.Drawing.Size(56, 24);
			this.labelDaysOld.TabIndex = 3;
			this.labelDaysOld.Text = "Days Old";
			this.labelDaysOld.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// textDaysOld
			// 
			this.textDaysOld.Location = new System.Drawing.Point(97, 69);
			this.textDaysOld.Name = "textDaysOld";
			this.textDaysOld.Size = new System.Drawing.Size(60, 20);
			this.textDaysOld.TabIndex = 4;
			this.textDaysOld.Text = "30";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(37, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(406, 23);
			this.label1.TabIndex = 5;
			this.label1.Text = "Report on all insurance claims that have been sent but not received.";
			// 
			// FormRpOutInsClaims
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(464, 233);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textDaysOld);
			this.Controls.Add(this.labelDaysOld);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpOutInsClaims";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Oustanding Insurance Claims Report";
			this.Load += new System.EventHandler(this.FormOutstandingInsuranceClaims_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormOutstandingInsuranceClaims_Load(object sender, System.EventArgs e) {
			
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDaysOld.errorProvider1.GetError(textDaysOld) != ""){
				MessageBox.Show(Lan.g("All","Please correct data entry errors first."));
				return;
			}
			int daysOld=PIn.PInt(textDaysOld.Text);
			//FormQuery2.ResetGrid();//this is a method in FormQuery2;
			Queries.CurReport=new Report();
			DateTime startQDate = DateTime.Today.AddDays(-daysOld);
      Queries.CurReport.Query = "SELECT T2.Carrier,T1.ClaimNum,T1.ClaimType, T1.DateService,"
				+"CONCAT(T3.LName,', ',T3.FName,' ',T3.MiddleI), T1.DateSent,T1.ClaimFee "
				+"FROM claim AS T1 LEFT JOIN insplan AS T2 ON T1.PlanNum = T2.PlanNum "
				+"LEFT JOIN patient AS T3 ON T1.PatNum = T3.PatNum " 
				+"WHERE T1.ClaimStatus='S' && T1.DateSent < '"+POut.PDate(startQDate)+"' "
				+"ORDER BY T1.DateService";
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;

			Queries.SubmitTemp();//create TableTemp
			Queries.TableQ=new DataTable(null);//new table no name
			for(int i=0;i<6;i++){//add columns
				Queries.TableQ.Columns.Add(new System.Data.DataColumn());//blank columns
			}
			Queries.CurReport.ColTotal=new double[Queries.TableQ.Columns.Count];
			for(int i=0;i<Queries.TableTemp.Rows.Count;i++){//loop through data rows
				DataRow row = Queries.TableQ.NewRow();//create new row called 'row' based on structure of TableQ
				row[0]=Queries.TableTemp.Rows[i][0];//start filling 'row'
        if(PIn.PString(Queries.TableTemp.Rows[i][2].ToString())=="P")
          row[1]="Primary";
				if(PIn.PString(Queries.TableTemp.Rows[i][2].ToString())=="S")
          row[1]="Secondary";
				if(PIn.PString(Queries.TableTemp.Rows[i][2].ToString())=="PreAuth")
          row[1]="PreAuth";
				if(PIn.PString(Queries.TableTemp.Rows[i][2].ToString())=="Other")
          row[1]="Other";
				row[2]=(PIn.PDate(Queries.TableTemp.Rows[i][3].ToString())).ToString("d");//start filling 'row'
				row[3]=Queries.TableTemp.Rows[i][4];//start filling 'row'
        TimeSpan d = DateTime.Today.Subtract((PIn.PDate(Queries.TableTemp.Rows[i][5].ToString())));
        row[4]=d.Days.ToString();
				row[5]=PIn.PDouble(Queries.TableTemp.Rows[i][6].ToString()).ToString("F");//start filling 'row'
				Queries.CurReport.ColTotal[5]+=PIn.PDouble(Queries.TableTemp.Rows[i][6].ToString());
				Queries.TableQ.Rows.Add(row);
      }
			Queries.CurReport.ColWidth=new int[Queries.TableQ.Columns.Count];
			Queries.CurReport.ColPos=new int[Queries.TableQ.Columns.Count+1];
			Queries.CurReport.ColPos[0]=0;
			Queries.CurReport.ColCaption=new string[Queries.TableQ.Columns.Count];
			Queries.CurReport.ColAlign=new HorizontalAlignment[Queries.TableQ.Columns.Count];
			FormQuery2.ResetGrid();//this is a method in FormQuery2;
			Queries.CurReport.Title="OUTSTANDING INSURANCE CLAIMS";
			Queries.CurReport.SubTitle=new string[3];
			Queries.CurReport.SubTitle[0]=((Pref)Prefs.HList["PracticeTitle"]).ValueString;
			Queries.CurReport.SubTitle[1]="Days Outstanding: " + daysOld;			
			Queries.CurReport.ColPos[0]=20;
			Queries.CurReport.ColPos[1]=170;
			Queries.CurReport.ColPos[2]=270;
			Queries.CurReport.ColPos[3]=395;
			Queries.CurReport.ColPos[4]=595;
			Queries.CurReport.ColPos[5]=670;
			Queries.CurReport.ColPos[6]=770;
			Queries.CurReport.ColCaption[0]=Lan.g(this,"Carrier");
			Queries.CurReport.ColCaption[1]=Lan.g(this,"Type");
			Queries.CurReport.ColCaption[2]=Lan.g(this,"Date of Service");
			Queries.CurReport.ColCaption[3]=Lan.g(this,"Patient Name");
			Queries.CurReport.ColCaption[4]=Lan.g(this,"Days Old");
			Queries.CurReport.ColCaption[5]=Lan.g(this,"Amount");
			Queries.CurReport.ColAlign[5]=HorizontalAlignment.Right;
			Queries.CurReport.Summary=new string[3];
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;
		}
	}
}