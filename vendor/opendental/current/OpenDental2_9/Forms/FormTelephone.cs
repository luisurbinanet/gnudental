using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary></summary>
	public class FormTelephone : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butClose;
		private System.Windows.Forms.Button butReformat;
		private System.Windows.Forms.Label label1;
		/// <summary>Required designer variable.</summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormTelephone()
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
			this.butReformat = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClose.Location = new System.Drawing.Point(509, 266);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butReformat
			// 
			this.butReformat.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butReformat.Location = new System.Drawing.Point(17, 31);
			this.butReformat.Name = "butReformat";
			this.butReformat.Size = new System.Drawing.Size(108, 26);
			this.butReformat.TabIndex = 1;
			this.butReformat.Text = "&Reformat";
			this.butReformat.Click += new System.EventHandler(this.butReformat_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(137, 33);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(478, 57);
			this.label1.TabIndex = 2;
			this.label1.Text = "Reformat all phone numbers in the database to (###)###-####.  Only certain matche" +
				"s will be reformatted.  No numbers will be lost, and no trailing comments will b" +
				"e affected.";
			// 
			// FormTelephone
			// 
			this.AcceptButton = this.butClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(642, 313);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butReformat);
			this.Controls.Add(this.butClose);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormTelephone";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Telephone Tools";
			this.Load += new System.EventHandler(this.FormTelephone_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormTelephone_Load(object sender, System.EventArgs e) {
		
		}
		
		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void butReformat_Click(object sender, System.EventArgs e) {
			if(CultureInfo.CurrentCulture.Name!="en-US"){
				if(MessageBox.Show(Lan.g(this,"Are your sure?  The phone number formatting is only meant for the United States?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;
				}
			}
			Reformat();
			//refresh carriers:
			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
			MessageBox.Show(Lan.g(this,"Telephone numbers reformatted."));
		}
			
		public static void Reformat(){
			string oldTel;
			string newTel;
			string idNum;
			Queries.CurReport.Query="select * from patient";
			Queries.SubmitCur();
			for(int i=0;i<Queries.TableQ.Rows.Count;i++){
				idNum=PIn.PString(Queries.TableQ.Rows[i][0].ToString());
				//home
				oldTel=PIn.PString(Queries.TableQ.Rows[i][15].ToString());
				newTel=TelephoneNumbers.ReFormat(oldTel);
				if(oldTel!=newTel){
					Queries.CurReport.Query="UPDATE patient SET hmphone = '"
						+newTel+"' WHERE patNum = '"+idNum+"'";
					
					Queries.SubmitNonQ();
				}
				//wk:
				oldTel=PIn.PString(Queries.TableQ.Rows[i][16].ToString());
				newTel=TelephoneNumbers.ReFormat(oldTel);
				if(oldTel!=newTel){
					Queries.CurReport.Query="UPDATE patient SET wkphone = '"
						+newTel+"' WHERE patNum = '"+idNum+"'";
					Queries.SubmitNonQ();
				}
				//wireless
				oldTel=PIn.PString(Queries.TableQ.Rows[i][17].ToString());
				newTel=TelephoneNumbers.ReFormat(oldTel);
				if(oldTel!=newTel){
					Queries.CurReport.Query="UPDATE patient SET wkphone = '"
						+newTel+"' WHERE patNum = '"+idNum+"'";
					Queries.SubmitNonQ();
				}
			}
			Queries.CurReport.Query="select * from carrier";
			Queries.SubmitCur();	
			for(int i=0;i<Queries.TableQ.Rows.Count;i++){
				idNum=PIn.PString(Queries.TableQ.Rows[i][0].ToString());
				//ph
				oldTel=PIn.PString(Queries.TableQ.Rows[i][7].ToString());
				newTel=TelephoneNumbers.ReFormat(oldTel);
				if(oldTel!=newTel){
					Queries.CurReport.Query="UPDATE carrier SET Phone = '"
						+newTel+"' WHERE CarrierNum = '"+idNum+"'";
					Queries.SubmitNonQ();
				}
			}
			//this last part will only be run once during conversion to 2.8. It can be dropped from a future version.
			Queries.CurReport.Query="select * from insplan";
			Queries.SubmitCur();	
			for(int i=0;i<Queries.TableQ.Rows.Count;i++){
				idNum=PIn.PString(Queries.TableQ.Rows[i][0].ToString());
				//ph
				oldTel=PIn.PString(Queries.TableQ.Rows[i][5].ToString());
				newTel=TelephoneNumbers.ReFormat(oldTel);
				if(oldTel!=newTel){
					Queries.CurReport.Query="UPDATE insplan SET Phone = '"
						+newTel+"' WHERE PlanNum = '"+idNum+"'";
					Queries.SubmitNonQ();
				}
			}
		}//reformat

		

	}
}










