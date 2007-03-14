using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormRpInsCo : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Label labelPatName;
		private System.Windows.Forms.TextBox textBoxCarrier;
		private System.ComponentModel.Container components = null;
		private FormQuery FormQuery2;
		private string carrier;

		public FormRpInsCo(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				labelPatName,
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
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.labelPatName = new System.Windows.Forms.Label();
			this.textBoxCarrier = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(528, 328);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "Cancel";
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(528, 296);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 1;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// labelPatName
			// 
			this.labelPatName.Location = new System.Drawing.Point(24, 32);
			this.labelPatName.Name = "labelPatName";
			this.labelPatName.Size = new System.Drawing.Size(256, 24);
			this.labelPatName.TabIndex = 37;
			this.labelPatName.Text = "Enter the first few letters of the Insurance Carrier name, or leave blank to view" +
				" all carriers:";
			this.labelPatName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// textBoxCarrier
			// 
			this.textBoxCarrier.Location = new System.Drawing.Point(281, 33);
			this.textBoxCarrier.Name = "textBoxCarrier";
			this.textBoxCarrier.TabIndex = 0;
			this.textBoxCarrier.Text = "";
			// 
			// FormRpInsCo
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(616, 366);
			this.Controls.Add(this.labelPatName);
			this.Controls.Add(this.textBoxCarrier);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpInsCo";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Insurance Company Report";
			this.ResumeLayout(false);

		}
		#endregion

		private void butOK_Click(object sender, System.EventArgs e) {
			carrier= PIn.PString(textBoxCarrier.Text);
			Queries.CurReport=new Report();

/*
SELECT insplan.subscriber,insplan.carrier,patient.hmphone,
insplan.groupname FROM insplan,patient WHERE insplan.subscriber=patient.patnum 
&& insplan.carrier like +carrier+'%'
Order By patient.lname,patient.fname

*/
			Queries.CurReport.Query= "SELECT carrier,CONCAT(LName,', ',FName,' ',MiddleI),phone,"
				+"groupname FROM insplan,patient "
				+"WHERE insplan.subscriber=patient.patnum "
				+"&& carrier LIKE '"+carrier+"%' "
				+"ORDER BY carrier";

			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			FormQuery2.SubmitReportQuery();			
			Queries.CurReport.Title="Insurance Companies List";
			Queries.CurReport.SubTitle=new string[2];
			Queries.CurReport.SubTitle[0]=((Pref)Prefs.HList["PracticeTitle"]).ValueString;

			Queries.CurReport.ColPos=new int[5];
			Queries.CurReport.ColCaption=new string[4];
			Queries.CurReport.ColAlign=new HorizontalAlignment[4];
			Queries.CurReport.ColPos[0]=20;
			Queries.CurReport.ColPos[1]=195;
			Queries.CurReport.ColPos[2]=370;
			Queries.CurReport.ColPos[3]=545;
			Queries.CurReport.ColPos[4]=720;
			Queries.CurReport.ColCaption[0]="Carrier Name";
			Queries.CurReport.ColCaption[1]="Subscriber Name";
			Queries.CurReport.ColCaption[2]="Carrier Phone#";
			Queries.CurReport.ColCaption[3]="Group Name";
			Queries.CurReport.Summary=new string[1];
			Queries.CurReport.Summary[0]=Lan.g(this,"Total: ")+Queries.TableQ.Rows.Count.ToString();
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;		
		}
	}
}


















