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
		private System.Windows.Forms.TextBox textBoxDaysOld;
		private System.Windows.Forms.Label labelDaysOld;
		private int daysOld=0;
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
			this.textBoxDaysOld = new System.Windows.Forms.TextBox();
			this.labelDaysOld = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(224, 176);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 1;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(224, 216);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "Cancel";
			// 
			// textBoxDaysOld
			// 
			this.textBoxDaysOld.Location = new System.Drawing.Point(88, 40);
			this.textBoxDaysOld.Name = "textBoxDaysOld";
			this.textBoxDaysOld.TabIndex = 0;
			this.textBoxDaysOld.Text = "";
			// 
			// labelDaysOld
			// 
			this.labelDaysOld.Location = new System.Drawing.Point(32, 40);
			this.labelDaysOld.Name = "labelDaysOld";
			this.labelDaysOld.Size = new System.Drawing.Size(56, 24);
			this.labelDaysOld.TabIndex = 3;
			this.labelDaysOld.Text = "Days Old";
			this.labelDaysOld.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// FormRpOutInsClaims
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(320, 254);
			this.Controls.Add(this.labelDaysOld);
			this.Controls.Add(this.textBoxDaysOld);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Name = "FormRpOutInsClaims";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Oustanding Insurance Claims Report";
			this.Load += new System.EventHandler(this.FormOutstandingInsuranceClaims_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormOutstandingInsuranceClaims_Load(object sender, System.EventArgs e) {
			textBoxDaysOld.Select();
		}
		private void butOK_Click(object sender, System.EventArgs e) {
      if(textBoxDaysOld.Text.Equals(null)||textBoxDaysOld.Text.Equals(""))  {
        MessageBox.Show(Lan.g(this,"You must enter a value"));
				return;
			}
      else  {
        char[] temp = textBoxDaysOld.Text.ToCharArray();
				if (char.IsNumber(temp[0]))
					daysOld=Convert.ToInt32(textBoxDaysOld.Text);
				else  {
					MessageBox.Show(Lan.g(this,"Invalid Input for Days Old.  Please enter a number"));
					textBoxDaysOld.Clear();
					return;
			  }
      }
			//FormQuery2.ResetGrid();//this is a method in FormQuery2;
			Queries.CurReport=new Report();
			DateTime startQDate = DateTime.Today.AddDays(-daysOld);
      Queries.CurReport.Query = "SELECT T2.Carrier,T1.ClaimNum,T1.PriClaimNum, T1.DateService,"
				+"CONCAT(T3.LName,', ',T3.FName,' ',T3.MiddleI), T1.DateSent,T1.ClaimFee "
				+"FROM claim AS T1 LEFT JOIN insplan AS T2 ON T1.PlanNum = T2.PlanNum "
				+"LEFT JOIN patient AS T3 ON T1.PatNum = T3.PatNum " 
				+"WHERE T1.ClaimStatus='S' && T1.DateSent < '"+POut.PDate(startQDate)+"'";
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
        if(PIn.PInt(Queries.TableTemp.Rows[i][1].ToString()) 
					== PIn.PInt(Queries.TableTemp.Rows[i][2].ToString()))
          row[1]="Primary";
				else
					row[1]="Secondary";
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
			Queries.CurReport.ColCaption[0]="Carrier";
			Queries.CurReport.ColCaption[1]="Pri/Sec";
			Queries.CurReport.ColCaption[2]="Procedure Date";
			Queries.CurReport.ColCaption[3]="Patient Name";
			Queries.CurReport.ColCaption[4]="Days Old";
			Queries.CurReport.ColCaption[5]="Amount";
			Queries.CurReport.ColAlign[5]=HorizontalAlignment.Right;
			Queries.CurReport.Summary=new string[3];
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;
		}
	}
}
