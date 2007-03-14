using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental
{
	/// <summary>
	/// Summary description for FormClaimPayEnter.
	/// </summary>
	public class FormClaimPayWizard : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private OpenDental.ValidDouble textInsPayAmt;
		private OpenDental.ValidDouble textDedApplied;
		private System.Windows.Forms.TextBox textReasonUnder;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label3;
		private OpenDental.ValidDate textDateRec;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.Container components = null;

		public FormClaimPayWizard(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label10,
				this.label2,
				this.label3,
				this.label4,
				this.label13,
				this.label6,
				this.label12,
				this.label8,
				this.label9
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
				button1,
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
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.textInsPayAmt = new OpenDental.ValidDouble();
			this.textDedApplied = new OpenDental.ValidDouble();
			this.textReasonUnder = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textDateRec = new OpenDental.ValidDate();
			this.label6 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.label12 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(414, 116);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 4;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.Location = new System.Drawing.Point(414, 150);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 5;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(13, 239);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(472, 28);
			this.label2.TabIndex = 46;
			this.label2.Text = "1.  Enter information for this claim only.  If you need to change it later, you c" +
				"an do so from here or directly from the Claim window.";
			// 
			// textInsPayAmt
			// 
			this.textInsPayAmt.Location = new System.Drawing.Point(186, 77);
			this.textInsPayAmt.Name = "textInsPayAmt";
			this.textInsPayAmt.Size = new System.Drawing.Size(66, 20);
			this.textInsPayAmt.TabIndex = 2;
			this.textInsPayAmt.Text = "";
			this.textInsPayAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textDedApplied
			// 
			this.textDedApplied.Location = new System.Drawing.Point(186, 47);
			this.textDedApplied.Name = "textDedApplied";
			this.textDedApplied.Size = new System.Drawing.Size(66, 20);
			this.textDedApplied.TabIndex = 1;
			this.textDedApplied.Text = "";
			this.textDedApplied.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textDedApplied.TextChanged += new System.EventHandler(this.textDedApplied_TextChanged);
			// 
			// textReasonUnder
			// 
			this.textReasonUnder.Location = new System.Drawing.Point(38, 132);
			this.textReasonUnder.Multiline = true;
			this.textReasonUnder.Name = "textReasonUnder";
			this.textReasonUnder.Size = new System.Drawing.Size(349, 39);
			this.textReasonUnder.TabIndex = 3;
			this.textReasonUnder.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(36, 114);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(360, 23);
			this.label4.TabIndex = 76;
			this.label4.Text = "Other reasons underpaid:  (Will show on patient bill in a future version)";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(35, 51);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(150, 16);
			this.label13.TabIndex = 66;
			this.label13.Text = "Deductible Applied";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(42, 79);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(145, 16);
			this.label3.TabIndex = 65;
			this.label3.Text = "Payment Amount";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDateRec
			// 
			this.textDateRec.Location = new System.Drawing.Point(186, 17);
			this.textDateRec.Name = "textDateRec";
			this.textDateRec.Size = new System.Drawing.Size(82, 20);
			this.textDateRec.TabIndex = 0;
			this.textDateRec.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(39, 21);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(145, 16);
			this.label6.TabIndex = 82;
			this.label6.Text = "Date Received";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(11, 344);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(480, 38);
			this.label8.TabIndex = 85;
			this.label8.Text = "(If you need to make a note that you don\'t want to show on the patient bill, it s" +
				"hould go in the main Account notes.)";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(13, 280);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(437, 32);
			this.label9.TabIndex = 86;
			this.label9.Text = "2.  When  you click OK, the status for the claim will be changed to Received.";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(12, 312);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(466, 34);
			this.label10.TabIndex = 87;
			this.label10.Text = "3.  After entering in the payment for each patient, then you can create the insur" +
				"ance check.";
			// 
			// button1
			// 
			this.button1.Enabled = false;
			this.button1.Location = new System.Drawing.Point(6, 198);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(490, 3);
			this.button1.TabIndex = 88;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(20, 210);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(170, 20);
			this.label12.TabIndex = 89;
			this.label12.Text = "Instructions if Needed:";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(260, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(204, 66);
			this.label1.TabIndex = 90;
			this.label1.Text = "THIS FORM IS OBSOLETE";
			// 
			// FormClaimPayWizard
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(516, 384);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.textDateRec);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textInsPayAmt);
			this.Controls.Add(this.textDedApplied);
			this.Controls.Add(this.textReasonUnder);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Name = "FormClaimPayWizard";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Claim Payment Entry Wizard";
			this.Load += new System.EventHandler(this.FormClaimPayWizard_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormClaimPayWizard_Load(object sender, System.EventArgs e) {
			if(DateTime.Compare(Claims.Cur.DateReceived,new DateTime(1860,1,1))<0)
				textDateRec.Text=DateTime.Now.ToString("d");
			else
				textDateRec.Text=Claims.Cur.DateReceived.ToString("d");
			//textClaimFee.Text=ClaimFee.ToString("F");
			//if(IsPrimary){
			//	textInsPayEstSubtotal.Text=PriInsPayEstSubtotal.ToString("F");
			//}
			//else{
			//	textInsPayEstSubtotal.Text=SecInsPayEstSubtotal.ToString("F");
			//}
			textDedApplied.Text=Claims.Cur.DedApplied.ToString("F");
			//textOverMax.Text=Claims.Cur.OverMax.ToString("F");
			//ComputeTotals();
			if(Claims.Cur.InsPayAmt==0){
				textInsPayAmt.Text=Claims.Cur.InsPayEst.ToString("F");
			}
			else
				textInsPayAmt.Text=Claims.Cur.InsPayAmt.ToString("F");
		}

		private void ComputeTotals(){
			/*
			if(textDedApplied.Text=="")
				Claims.Cur.DedApplied=0;
			else{
				try{
					Claims.Cur.DedApplied=PIn.PDouble(textDedApplied.Text);
				}
				catch{
					MessageBox.Show("Invalid character");
					return;
				}
			}
			double dedAdj=Claims.Cur.DedApplied*.2;
			textDedAdj.Text=dedAdj.ToString("F");
			if(textOverMax.Text=="")
				Claims.Cur.OverMax=0;
			else{
				try{
					Claims.Cur.OverMax=PIn.PDouble(textOverMax.Text);
				}
				catch{
					MessageBox.Show("Invalid character");
					return;
				}
			}
			PriInsPayEst=PriInsPayEstSubtotal-Claims.Cur.DedApplied+dedAdj-Claims.Cur.OverMax;
			SecInsPayEst=SecInsPayEstSubtotal-Claims.Cur.DedApplied+dedAdj-Claims.Cur.OverMax;
			if(PriInsPayEst < 0) PriInsPayEst=0;
			if(SecInsPayEst < 0) SecInsPayEst=0;
			if(IsPrimary)
				textInsPayEst.Text=PriInsPayEst.ToString("F");
			else
				textInsPayEst.Text=SecInsPayEst.ToString("F");
			*/
		}

		private void textDedApplied_TextChanged(object sender, System.EventArgs e) {
			//ComputeTotals();
		}

		private void textOverMax_TextChanged(object sender, System.EventArgs e) {
			//ComputeTotals();
		}

		private void textInsPayAmt_TextChanged(object sender, System.EventArgs e) {
			//computeTotals();
		}

		private bool ClaimIsValid(){
			if( textDateRec.errorProvider1.GetError(textDateRec)!=""
				|| textDedApplied.errorProvider1.GetError(textDedApplied)!=""
				//|| textOverMax.errorProvider1.GetError(textOverMax)!=""
				|| textInsPayAmt.errorProvider1.GetError(textInsPayAmt)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return false;
			}
			else
				return true;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(!ClaimIsValid())
				return;
			if(textDateRec.Text=="")
				Claims.Cur.DateReceived=DateTime.MinValue;
			else Claims.Cur.DateReceived=PIn.PDate(textDateRec.Text);
			if(textDedApplied.Text=="")
				Claims.Cur.DedApplied=0;
			else
				Claims.Cur.DedApplied=PIn.PDouble(textDedApplied.Text);
			//if(textOverMax.Text=="")
			//	Claims.Cur.OverMax=0;
			//else
			//	Claims.Cur.OverMax=PIn.PDouble(textOverMax.Text);
			if(textInsPayAmt.Text=="")
				Claims.Cur.InsPayAmt=0;
			else
				Claims.Cur.InsPayAmt=PIn.PDouble(textInsPayAmt.Text);
			Claims.Cur.ReasonUnderPaid=textReasonUnder.Text;
			Claims.Cur.ClaimStatus="R";
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}	

		
	}
}
